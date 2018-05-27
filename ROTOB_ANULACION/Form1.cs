using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using GDS_NuevoMundoPersistencia;
using Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes;
using GDS_NuevoMundoDominio;
using classBO = GDS_NuevoMundoDominio.ObjetoDominioNegocio.classBO;
using GDS_MuevoMundoLog;

using GDS_NM_Mensajeria;
using System.Globalization;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Threading;
using System.Text.RegularExpressions;
using ROTOB_ANULACION.Procesos;
using ROTOB_ANULACION.Modelo;
using ROTOB_ANULACION.Utilitarios;
using ROTOB_ANULACION.Persistencia;
using System.Reflection;
using ROTOB_ANULACION.Reportes;
using System.Net.Http;
using System.Net.Http.Headers;
using ROTOB_ANULACION.ClienteRest;
using Newtonsoft.Json;
namespace ROTOB_ANULACION
{
    public partial class Form1 : Form
    {
        private GestorLog logger = new GestorLog();
        private DateUtils dateUtils = DateUtils.getInstance();
        private Main mainProcess;
        public Form1()
        {
            try
            {
                InitializeComponent();
                mainProcess = new Main();
                Text = string.Format(" {0} - {1} - Robot Anulaciones ", Configuracion.Gds, Configuracion.empresa);

                if (Configuracion.IsTest)
                {
                    Text += " - TEST";
                }

                cargarComboProcesos();
            }
            catch (Exception e)
            {
                var msg = string.Format("Ocurrio un error al inicializar el formulario Principal. Desc: {0} ", e.ToString());
                logger.info(msg);
                MessageBox.Show(msg);
            }
            myTimer.Interval = 1000;
            myTimer.Enabled = true;
        }

        private void cargarComboProcesos()
        {
            cboProcesos.Items.Add(TipoProceso.AVISO_NO_FACTURADOS);
            cboProcesos.Items.Add(TipoProceso.AVISO_NO_EN_PTA);
            cboProcesos.Items.Add(TipoProceso.AVISO_NO_FACTURADOS_AYER);
            cboProcesos.Items.Add(TipoProceso.AVISO_ANULACION);
            cboProcesos.Items.Add(TipoProceso.ANULACION);
            cboProcesos.Items.Add(TipoProceso.ALMACENAR_BOLETOS_EMITIDOS_AMADEUS);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myTimer.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
            {
                var horaSistema = dateUtils.obtenerHoraFormat24();
                lblHora.Text = horaSistema;

                var horarioRobot = Configuracion.obtenerHorarioRobot();

                if (Configuracion.Gds == GDS.SABRE)
                {
                    if (horarioRobot.horaAvisoNoPTA_NoFacturado.Contains(horaSistema))
                    {
                        mainProcess.procesar(TipoProceso.AVISO_NO_FACTURADOS);
                    }
                    if (horarioRobot.horaAvisoAnulacion.Contains(horaSistema))
                    {
                        mainProcess.procesar(TipoProceso.AVISO_ANULACION);
                    }
                    else if (horarioRobot.horaAnulacion.Contains(horaSistema))
                    {
                        mainProcess.procesar(TipoProceso.ANULACION);
                    }
                    else if (horarioRobot.horaAvisoNoFacturadoAyer.Contains(horaSistema))
                    {
                        mainProcess.procesar(TipoProceso.AVISO_NO_FACTURADOS_AYER);
                    }
                    else if (horarioRobot.horaAvisoVoidDQBNoEnPTA.Contains(horaSistema))
                    {
                        mainProcess.procesar(TipoProceso.AVISO_VOID_DQB_NO_EN_PTA);
                    }
                }
                else if (Configuracion.Gds == GDS.AMADEUS)
                {
                    if (horarioRobot.horaAvisoNoPTA_NoFacturado.Contains(horaSistema))
                    {
                        procesarAmadeus(TipoProceso.AVISO_NO_FACTURADOS);
                        procesarAmadeus(TipoProceso.AVISO_NO_EN_PTA);
                    }
                    if (horarioRobot.horaAvisoAnulacion.Contains(horaSistema))
                    {
                        procesarAmadeus(TipoProceso.AVISO_ANULACION);
                    }
                    else if (horarioRobot.horaAnulacion.Contains(horaSistema))
                    {
                        procesarAmadeus(TipoProceso.ANULACION);
                    }
                    else if (horarioRobot.horaAvisoVoidDQBNoEnPTA.Contains(horaSistema))
                    {
                        procesarAmadeus(TipoProceso.AVISO_VOID_DQB_NO_EN_PTA);
                    }
                    else if (horarioRobot.horaAlmacenarBoletosAnulacion.Contains(horaSistema))
                    {
                        procesarAmadeus(TipoProceso.ALMACENAR_BOLETOS_EMITIDOS_AMADEUS);
                    }
                }
            }
        }

        private WS_ProcesosGDS.Credenciales getCredencialesWS()
        {
            var lsecurity = new NuevoMundoSecurity.EncriptaCadena();
            var lusuario = "gd5_3m1s10n3s";
            var lcontrasenia = "pw_gd5_3m1s10n3s";
            var lkey_encrypt = "GDS_S1S73M45";
            return new WS_ProcesosGDS.Credenciales
            {
                username = lsecurity.DES_Encrypt(lusuario, lkey_encrypt),
                password = lsecurity.DES_Encrypt(lcontrasenia, lkey_encrypt)
            };
        }


        #region procesarAnulacion
        private void procesarAnulacion(BoletoRobotDTO boleto)
        {
            logger.info("Anular boleto: " + boleto.BoletoFull);
            var lwsProcesosGDS = new WS_ProcesosGDS.ProcesosGDS();
            lwsProcesosGDS.CredencialesValue = getCredencialesWS();

            var ldatosAplicacion = new WS_ProcesosGDS.CE_DatosAplicacion();
            ldatosAplicacion.strGDS = WS_ProcesosGDS.CE_TipoGDS.GDS_AMADEUS;
            ldatosAplicacion.strNameApp = WS_ProcesosGDS.CE_Aplicaciones.SabreRed;
            ldatosAplicacion.strPNR = boleto.PNR;
            ldatosAplicacion.strPseudo = boleto.Pseudo;
            ldatosAplicacion.strPseudoVenta = boleto.Pseudo;
            ldatosAplicacion.strCodigoSeguimiento = "robot_anulaciones_amadeus@expertiatravel.com";

            var ldatosUsuario = new WS_ProcesosGDS.CE_DatosUsuario();
            ldatosUsuario.idUsuarioPTA = "123";
            ldatosUsuario.strCorreoUsuario = "localhost@nmviajes.com";

            var ldatosCliente = new WS_ProcesosGDS.CE_DatosCliente();
            ldatosCliente.intTipoCliente = boleto.TipoDeCliente;
            ldatosCliente.strDKCliente = Convert.ToString(boleto.IdCliente);
            ldatosCliente.strDireccion = "Lima";

            var oRecuperarReservaRQ = new WS_ProcesosGDS.CE_ReservaRQ();
            oRecuperarReservaRQ.oDatosAplicacion = ldatosAplicacion;
            oRecuperarReservaRQ.oDatosUsuario = ldatosUsuario;
            oRecuperarReservaRQ.oDatosCliente = ldatosCliente;
            oRecuperarReservaRQ.CambiarPseudo = true;
            oRecuperarReservaRQ.RealizarDQB = true;

            var oRecuperarReservaRS = lwsProcesosGDS.RecuperarReservaGDS(oRecuperarReservaRQ);
            if (oRecuperarReservaRS != null)
            {
                if (oRecuperarReservaRS.lstBoletos != null && oRecuperarReservaRS.lstBoletos.Any())
                {
                    var currentBoleto = oRecuperarReservaRS.lstBoletos.Where(b => b.eTicketNumber.Equals(boleto.BoletoFull)).FirstOrDefault();
                    if (currentBoleto != null)
                    {
                        currentBoleto.VoidearBoletoEnServicio = true;
                        var oAnulacioBoletoRQ = new WS_ProcesosGDS.CE_VoidearBoletoRQ
                        {
                            oDatosAplicacion = ldatosAplicacion,
                            oDatosCliente = ldatosCliente,
                            oDatosUsuario = ldatosUsuario,
                            lstBoletos = new WS_ProcesosGDS.CE_Boleto[] { currentBoleto },
                            TipoAnulacion = WS_ProcesosGDS.CE_Anulacion.Parcial,
                            SESSION = oRecuperarReservaRS.SESSION
                        };
                        var oAnulacionBoletoRS = lwsProcesosGDS.VoidearBoletosGDS(oAnulacioBoletoRQ);
                        if (oAnulacionBoletoRS != null)
                        {
                            if (oAnulacionBoletoRS.lstMensajeError == null && oAnulacionBoletoRS.CodigoError == 0)
                            {
                                logger.info("Anulación exitosa");
                            }
                            else
                            {
                                logger.info("Ocurrió un error al anular boleto:" + oAnulacionBoletoRS.lstMensajeError);
                            }
                        }
                    }
                }
            }
            logger.info("Response: ");
        }
        #endregion

        private List<BoletoRobotDTO> anularBoletos(List<BoletoRobotDTO> boletosAAnular)
        {
            List<BoletoRobotDTO> lrespuesta = new List<BoletoRobotDTO>();
            foreach (var lboleto in boletosAAnular)
            {
                try
                {
                    logger.info(string.Format("Anular boleto por servicio: {0} - {1}", lboleto.PNR, lboleto.BoletoFull));

                    var lwsProcesosGDS = new WS_ProcesosGDS.ProcesosGDS();
                    lwsProcesosGDS.CredencialesValue = getCredencialesWS();

                    var ldatosAplicacion = new WS_ProcesosGDS.CE_DatosAplicacion();
                    ldatosAplicacion.strGDS = WS_ProcesosGDS.CE_TipoGDS.GDS_AMADEUS;
                    ldatosAplicacion.strNameApp = WS_ProcesosGDS.CE_Aplicaciones.SabreRed;
                    ldatosAplicacion.strPNR = lboleto.PNR;
                    ldatosAplicacion.strPseudo = lboleto.Pseudo;
                    ldatosAplicacion.strPseudoVenta = lboleto.Pseudo;
                    ldatosAplicacion.strCodigoSeguimiento = "robot_anulaciones_amadeus@expertiatravel.com";

                    var ldatosUsuario = new WS_ProcesosGDS.CE_DatosUsuario();
                    ldatosUsuario.idUsuarioPTA = "123";
                    ldatosUsuario.strCorreoUsuario = "localhost@nmviajes.com";

                    var ldatosCliente = new WS_ProcesosGDS.CE_DatosCliente();
                    ldatosCliente.intTipoCliente = lboleto.TipoDeCliente;
                    ldatosCliente.strDKCliente = Convert.ToString(lboleto.IdCliente);
                    ldatosCliente.strDireccion = "Lima";

                    var oRecuperarReservaRQ = new WS_ProcesosGDS.CE_ReservaRQ();
                    oRecuperarReservaRQ.oDatosAplicacion = ldatosAplicacion;
                    oRecuperarReservaRQ.oDatosUsuario = ldatosUsuario;
                    oRecuperarReservaRQ.oDatosCliente = ldatosCliente;
                    oRecuperarReservaRQ.CambiarPseudo = true;
                    oRecuperarReservaRQ.RealizarDQB = true;

                    var oRecuperarReservaRS = lwsProcesosGDS.RecuperarReservaGDS(oRecuperarReservaRQ);
                    if (oRecuperarReservaRS != null)
                    {
                        if (oRecuperarReservaRS.lstMensajeError != null)
                        {
                            lboleto.AnulacionOkPorRobot = false;
                            lboleto.MensajeError = string.Join("/", oRecuperarReservaRS.lstMensajeError);
                        }
                        else
                        {
                            if (oRecuperarReservaRS.lstBoletos != null && oRecuperarReservaRS.lstBoletos.Any())
                            {
                                var currentBoleto = oRecuperarReservaRS.lstBoletos.Where(b => b.eTicketNumber.Equals(lboleto.BoletoFull)).FirstOrDefault();
                                if (currentBoleto != null)
                                {
                                    currentBoleto.VoidearBoletoEnServicio = true;
                                    var oAnulacionBoletoRQ = new WS_ProcesosGDS.CE_VoidearBoletoRQ
                                    {
                                        oDatosAplicacion = ldatosAplicacion,
                                        oDatosCliente = ldatosCliente,
                                        oDatosUsuario = ldatosUsuario,
                                        lstBoletos = new WS_ProcesosGDS.CE_Boleto[] { currentBoleto },
                                        TipoAnulacion = WS_ProcesosGDS.CE_Anulacion.Parcial,
                                        SESSION = oRecuperarReservaRS.SESSION
                                    };
                                    var oAnulacionBoletoRS = lwsProcesosGDS.VoidearBoletosGDS(oAnulacionBoletoRQ);

                                    if (oAnulacionBoletoRS != null)
                                    {
                                        if (oAnulacionBoletoRS.lstMensajeError == null && oAnulacionBoletoRS.lstBoletosVoid != null && oAnulacionBoletoRS.lstBoletosVoid.Any())
                                        {
                                            lboleto.AnulacionOkPorRobot = true;
                                            lboleto.Estado = oAnulacionBoletoRS.lstBoletosVoid.Where(b => b.eTicketNumber.Equals(lboleto.BoletoFull)).First().Estado;
                                        }
                                        else
                                        {
                                            lboleto.AnulacionOkPorRobot = false;
                                            lboleto.MensajeError = string.Join("/", oAnulacionBoletoRS.lstMensajeError);
                                            logger.info(string.Format("Mensaje de error devuelto por el servicio de anulacion: ", lboleto.MensajeError));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        lboleto.AnulacionOkPorRobot = false;
                        lboleto.MensajeError = "No se pudo recuperar la reserva para proceder con la anulacion del boleto";
                        logger.info(lboleto.MensajeError);
                    }
                }
                catch (Exception e)
                {
                    logger.info(string.Format("Ocurrió un error al ejecutar el servicio de anulación Amadeus,  boleto {0} - {1}", lboleto.PNR, lboleto.BoletoFull));
                    logger.info(string.Format("Mensaje error: ", e.ToString()));
                }
                lrespuesta.Add(lboleto);
            }

            logger.info("Se realizara nuevamente el reporte Diario para validar el estado real de los boletos");
            var service = BoletoFactory.getBoletoFactory(TipoProceso.ANULACION, getFechaPorTipoProceso(TipoProceso.ANULACION));
            var boletosGDSActual = service.ObtenerReporteDiarioGDS();
            foreach (var lboletoAnulado in lrespuesta)
            {
                var lcurrentBoletoGDS = boletosGDSActual.Where(b => b.BoletoFull.Equals(lboletoAnulado.BoletoFull)).First();
                lboletoAnulado.Estado = lcurrentBoletoGDS.Estado;
                if (lcurrentBoletoGDS.Estado.Equals("VOID"))
                {
                    lboletoAnulado.AnulacionOkPorRobot = true;
                    lboletoAnulado.MensajeError = "";
                }
                else
                {
                    lboletoAnulado.AnulacionOkPorRobot = false;
                    lboletoAnulado.MensajeError = string.IsNullOrEmpty(lboletoAnulado.MensajeError) ? "Error desconocido" : lboletoAnulado.MensajeError;
                }
            }
            return lrespuesta;
        }

        private string getFechaPorTipoProceso(TipoProceso tipoProceso)
        {
            return (tipoProceso == TipoProceso.AVISO_NO_FACTURADOS_AYER) ? DateTime.Today.AddDays(-1).ToShortDateString() : DateTime.Now.ToShortDateString();
        }

        #region getBoletosMock
        public List<BoletoRobotDTO> getBoletosMock(TipoProceso tipoProceso)
        {
            List<BoletoRobotDTO> boletos = null;
            var service = BoletoFactory.getBoletoFactory(tipoProceso, getFechaPorTipoProceso(tipoProceso));

            var pathNoFacturados = @"C:\ROBOT_ANULACION_AMADEUS\JSON\BoletosNoFacturados.json";
            if (!File.Exists(pathNoFacturados))
            {
                boletos = service.ObtenerBoletosConDeudaPendiente();
                var ljsonBoletosDQB = Utilitario.Serializar(boletos);
                FileUtils.getInstance().write(pathNoFacturados, ljsonBoletosDQB);
            }
            else
            {
                var ljsonBoletosEmitidos = FileUtils.getInstance().read(pathNoFacturados);
                boletos = new JavaScriptSerializer().Deserialize<List<BoletoRobotDTO>>(ljsonBoletosEmitidos);
            }
            return boletos;
        }
        #endregion


        private void enviarLogProceso()
        {
            var pathLog = logger.getRutaArchivoLog();
            var gestorCorreo = new GestorCorreo
            {
                EmailTo = Configuracion.correo_EmailNotificacion + ";" + "flavio.goni@expertiatravel.com",
                EmailFrom = "robot_anulaciones@nmviajes.com",
                EmailSubject = string.Format("{0} Log Robot Anulaciones", Configuracion.Gds),
                EmailAttachments = new string[] { pathLog }
            };
            gestorCorreo.Enviar();
        }

        private void procesarAmadeus(TipoProceso tipoProceso)
        {
            logger.info("Inicio proceso Amadeus: " + tipoProceso);
            var service = BoletoFactory.getBoletoFactory(tipoProceso, getFechaPorTipoProceso(tipoProceso));
            List<BoletoRobotDTO> lboletosAProcesar = null;
            switch (tipoProceso)
            {
                case TipoProceso.ALMACENAR_BOLETOS_EMITIDOS_AMADEUS:
                    service.AlmacenarBoletos();
                    enviarLogProceso();
                    break;

                case TipoProceso.ANULACION:
                    lboletosAProcesar = service.ObtenerBoletosConDeudaPendiente();
                    lboletosAProcesar = lboletosAProcesar.Where(boleto => boleto.TotalPendiente >= 20).ToList();
                    logger.info(string.Format("Se envía a anular {0} boletos", lboletosAProcesar.Count));
                    if (lboletosAProcesar.Any())
                    {
                        List<BoletoRobotDTO> boletosResultadoAnulacion = anularBoletos(lboletosAProcesar);
                        if (boletosResultadoAnulacion.Any())
                        {
                            logger.info("Resultado anulacion:");
                            boletosResultadoAnulacion.ForEach(boleto =>
                            {
                                logger.info(string.Format("[ Anulacion ] :: [{0} - {1} - {2}] ::  Total Pendiente:{3} | Emisor: {4} - {5} | DK:{6} | Estado:{7} | Anulado? {8} - Mensaje: {9}",
                                boleto.Pseudo, boleto.PNR, boleto.BoletoFull, boleto.TotalPendiente.ToString().PadLeft(8), boleto.Vendedor.Firma, boleto.Vendedor.Id, boleto.IdCliente.ToString().PadLeft(7), boleto.Estado.PadLeft(7), boleto.AnulacionOkPorRobot.ToString().PadLeft(6), boleto.MensajeError));
                            });
                        }
                        new EnvioReportes(tipoProceso, boletosResultadoAnulacion).procesar();
                    }
                    enviarLogProceso();
                    break;

                case TipoProceso.AVISO_ANULACION:
                    lboletosAProcesar = service.ObtenerBoletosConDeudaPendiente();
                    logger.info(string.Format("Se envía aviso de anulación a {0} boletos", lboletosAProcesar.Count));
                    if (lboletosAProcesar.Any())
                    {
                        lboletosAProcesar.ForEach(boleto =>
                        {
                            logger.info(string.Format("[ Aviso Anulacion ] :: [{0} - {1} - {2}] ::  Total Pendiente:{3} | Emisor: {4} - {5} | DK:{6}",
                                  boleto.Pseudo, boleto.PNR, boleto.BoletoFull, boleto.TotalPendiente.ToString().PadLeft(8), boleto.Vendedor.Firma, boleto.Vendedor.Id, boleto.IdCliente.ToString().PadLeft(7)));
                        });
                        new EnvioReportes(tipoProceso, lboletosAProcesar).procesar();
                    }
                    enviarLogProceso();
                    break;

                case TipoProceso.AVISO_NO_EN_PTA:
                    lboletosAProcesar = service.ObtenerBoletosNoEncuentranEnPTA();
                    logger.info(string.Format("Se envía aviso No En PTA a {0} boletos", lboletosAProcesar.Count));
                    if (lboletosAProcesar.Any())
                    {
                        new EnvioReportes(tipoProceso, lboletosAProcesar).procesar();
                    }
                    break;

                case TipoProceso.AVISO_NO_FACTURADOS:
                    lboletosAProcesar = service.ObtenerBoletosNoFacturados();
                    logger.info(string.Format("Se envía aviso de no facturados a {0} boletos", lboletosAProcesar.Count));
                    if (lboletosAProcesar.Any())
                    {
                        new EnvioReportes(tipoProceso, lboletosAProcesar).procesar();
                    }
                    break;
            }

        }

        private void btnProcesar_Click(object sender, EventArgs e)
        {
            var selectedIndex = cboProcesos.SelectedIndex;
            if (selectedIndex != -1)
            {
                var tipoProcesoSeleccionado = (TipoProceso)cboProcesos.SelectedItem;
                DialogResult ldialog = MessageBox.Show("Seguro que desea ejecutar el proceso " + tipoProcesoSeleccionado, "Atención!!!", MessageBoxButtons.YesNo);
                if (ldialog == DialogResult.Yes)
                {
                    if (tipoProcesoSeleccionado != TipoProceso.AVISO_VOID_DQB_NO_EN_PTA)
                    {
                        logger.info(string.Format("[Click] Se ha forzado la ejecución del proceso {0}.", tipoProcesoSeleccionado.ToString()));
                        if (Configuracion.Gds == GDS.SABRE)
                        {
                            mainProcess.procesar(tipoProcesoSeleccionado);
                        }
                        else
                        {
                            procesarAmadeus(tipoProcesoSeleccionado);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Seleccione un proceso a ejecutar!");
            }
        }

        //public async Task<PersonaDTO> agregarPersona(PersonaDTO persona)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        httpClient.BaseAddress = new Uri("http://localhost:8080");
        //        HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/persons", persona);
        //        return await response.Content.ReadAsAsync<PersonaDTO>();
        //    };
        //}


        private async void button1_Click(object sender, EventArgs e)
        {


            ExeConfigurationFileMap configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = "APP_SABRE_AGCORP.config";

            var config = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);

            AppSettingsSection section = (AppSettingsSection)config.GetSection("appSettings");
            var keyValueConfigElement = section.Settings["NombreEmpresa"].Value;

            
            var anulacionSabre = new AnulacionSabre("", "", "");
            anulacionSabre.procesar();

            //var response = await sendCommand("*T", "GEZNJG", "Shared/IDL:IceSess\\/SessMgr:1\\.0.IDL/Common/!ICESMS\\/RESE!ICESMSLB\\/RES.LB!-3154379077172252028!1391251!0!2!E2E-1");
            //if (response.Estatus.Ok)
            //{
            //    var message = response.Estatus.Mensajes.Select(x => x.Valor).Aggregate((x, y) => x + " / " + y);
            //}
        }

        static async Task<Uri> CreateProductAsync(string product, HttpClient client)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/ServicioHerramientas.json/EjecutarComando", product);
            response.EnsureSuccessStatusCode();
            return response.Headers.Location;
        }

    }

}
