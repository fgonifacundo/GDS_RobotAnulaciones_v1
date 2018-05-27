using NuevoMundoSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using classBO = GDS_NuevoMundoDominio.ObjetoDominioNegocio.classBO;

namespace ROTOB_ANULACION.Procesos
{
   public class ProcesoAnulacionAmadeus : IProcesoRobot
    {
        public void execute(GDS_NuevoMundoPersistencia.classSession session, List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletos)
        {

            var logger = new GestorLog();

            var encriptador = new EncriptaCadena();
            var CLAVE_ENCRYPT = "GDS_S1S73M45";
            var wsProcesosGDS = new WS_ProcesosGDS.ProcesosGDS();
            wsProcesosGDS.CredencialesValue = new WS_ProcesosGDS.Credenciales
            {
                username = encriptador.DES_Encrypt("gd5_3m1s10n3s", CLAVE_ENCRYPT),
                password = encriptador.DES_Encrypt("pw_gd5_3m1s10n3s", CLAVE_ENCRYPT)
            };

            var boletosVoideados = new List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente>();

            var agentes = boletos.Select(x => x.Vendedor.FirmaAgente).Distinct().ToList();

            foreach (var agente in agentes)
            {
                var boletosPorVendedor = boletos.Where(x => x.Vendedor.FirmaAgente.Equals(agente)).ToList();

                foreach (var boleto in boletosPorVendedor)
                {
                    var reservaRQ = new WS_ProcesosGDS.CE_ReservaRQ();
                    reservaRQ.oDatosAplicacion = new WS_ProcesosGDS.CE_DatosAplicacion();
                    reservaRQ.oDatosUsuario = new WS_ProcesosGDS.CE_DatosUsuario();
                    reservaRQ.oDatosAplicacion.strGDS = WS_ProcesosGDS.CE_TipoGDS.GDS_AMADEUS;
                    reservaRQ.oDatosAplicacion.strNameApp = WS_ProcesosGDS.CE_Aplicaciones.InteragenciaExtranet;
                    reservaRQ.oDatosAplicacion.strPNR = boleto.PNR;
                    reservaRQ.oDatosAplicacion.strPseudo = boleto.Pseudo;
                    reservaRQ.oDatosAplicacion.strPseudoVenta = boleto.Pseudo;
                    reservaRQ.oDatosAplicacion.strCodigoSeguimiento = "robot_anulaciones_amadeus@nmviajes.com";

                    reservaRQ.oDatosUsuario.strCorreoUsuario = "localhost@nmviajes.com";
                    reservaRQ.oDatosUsuario.idUsuarioPTA = "123";
                    reservaRQ.CambiarPseudo = true;
                    reservaRQ.RealizarDQB = true;

                    reservaRQ.oDatosCliente = new WS_ProcesosGDS.CE_DatosCliente();
                    reservaRQ.oDatosCliente.strDKCliente = Convert.ToString(boleto.IdCliente);
                    reservaRQ.oDatosCliente.strDireccion = "Lima";

                    var dataReserva = wsProcesosGDS.RecuperarReservaGDS(reservaRQ);

                    if (dataReserva != null)
                    {
                        if (dataReserva.CodigoError == 0)
                        {
                            try
                            {
                                var lEstadoActivo = "ACTIVO";
                                var boletoVoid = dataReserva.lstBoletos.Where(b => b.eTicketNumber.EndsWith(boleto.NumeroBoleto) && b.Estado.Equals(lEstadoActivo)).FirstOrDefault();
                                if (boletoVoid != null)
                                {
                                    boletoVoid.VoidearBoletoEnServicio = true;
                                    var listaBoletos = new List<WS_ProcesosGDS.CE_Boleto>();
                                    listaBoletos.Add(boletoVoid);

                                    //boleto.NumeroBoleto
                                    var objAnulacionBoletoRQ = new WS_ProcesosGDS.CE_VoidearBoletoRQ();
                                    objAnulacionBoletoRQ.oDatosAplicacion = reservaRQ.oDatosAplicacion;
                                    objAnulacionBoletoRQ.oDatosCliente = reservaRQ.oDatosCliente;
                                    objAnulacionBoletoRQ.oDatosUsuario = reservaRQ.oDatosUsuario;
                                    objAnulacionBoletoRQ.lstBoletos = listaBoletos.ToArray();
                                    objAnulacionBoletoRQ.SESSION = dataReserva.SESSION;
                                    objAnulacionBoletoRQ.TipoAnulacion = WS_ProcesosGDS.CE_Anulacion.Parcial;

                                    var response = wsProcesosGDS.VoidearBoletosGDS(objAnulacionBoletoRQ);
                                    if (response != null)
                                    {
                                        if (response.lstMensajeError == null && response.lstWarning == null)
                                        {
                                            boletosVoideados.Add(boleto);
                                            logger.info("Anulación correcta: " + boleto.NumeroBoleto);
                                        }
                                        else
                                        {
                                            logger.info("Ocurrió una incidencia al intentar voidear el boleto : " + boleto.NumeroBoleto);
                                        }
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                logger.info(e.ToString());
                            }
                        }

                    }
                }
                if (boletosVoideados.Any())
                {
                    new classBO().EnviarCorreoAvisoVoideo(boletosVoideados, "Counter", "VOI", "Boletos anulados", Configuracion.codigoSeguimiento);
                }

            }
        }

        public void execute(GDS_NuevoMundoPersistencia.classSession session, List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletos, string fecha)
        {
            throw new NotImplementedException();
        }
    }
}
