using ROTOB_ANULACION.GNM_ConsultaPTA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using classBO = GDS_NuevoMundoDominio.ObjetoDominioNegocio.classBO;
using GDS_NuevoMundoPersistencia;
using System.Globalization;
using System.Web.Script.Serialization;
using System.IO;
using System.Text.RegularExpressions;
using ROTOB_ANULACION.Amadeus_WS;


namespace ROTOB_ANULACION
{
    public class ServiceBoleto
    {
        private const string PSEUDO_DM = "QQ05";
        private const string ID_DEPARTAMENTO_COUNTER_IA_EN_DM = "CAG";
        private const int DK_NM_EN_DM = 56892;
        private const int DK_AGCORP_EN_NM = 49946;


        private classBO objDominio;
        private GestorLog logger;
        private string fechaReporte;
        private UtilSession utilSession;
        private TipoProceso tipoProceso;
        private FileUtils fileUtils;
        private DateUtils dateUtils;

        public ServiceBoleto(TipoProceso tipoProceso, string fecha)
        {
            this.objDominio = new classBO();
            this.logger = new GestorLog();
            this.utilSession = new UtilSession();
            this.tipoProceso = tipoProceso;
            this.fechaReporte = fecha;
            this.fileUtils = FileUtils.getInstance();
            this.dateUtils = DateUtils.getInstance();
        }

        /// <summary>
        /// Obtener todos los boletos Registrados en PTA, según lo especificado en el archivo de configuración
        /// </summary>
        /// <returns>Boletos en PTA</returns>
        public List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> obtenerBoletoRegistradosPTA(int esquema)
        {
            var strProveedores = string.Join(",", Configuracion.Proveedor.proveedoresActual);
            var boletosRegistradosPTA = objDominio.ReporteBoletosEmitidos_X(fechaReporte,
                                                                             Configuracion.idGDS,
                                                                             strProveedores,
                                                                             Configuracion.codigoSeguimiento,
                                                                             Configuracion.firmaBD,
                                                                             esquema);
            return boletosRegistradosPTA;
        }

        /// <summary>
        /// Obtener boletos emitidos en el DQB
        /// </summary>
        /// <returns></returns>
        public List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> ObtenerListaBoletosDQB(TipoProceso tipoProceso)
        {
            var lstBoletoEmitidoDQB = new List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente>();

            string lpseudosEvaluacion = "";

            if (this.tipoProceso == TipoProceso.ANULACION || this.tipoProceso == TipoProceso.AVISO_ANULACION)
            {
                lpseudosEvaluacion = Configuracion.pseudosUnificados;
            }
            else
            {
                lpseudosEvaluacion = Configuracion.pseudosEmpresa;
            }

            var lstReporteVentas = obtenerReporteDQB(lpseudosEvaluacion);
            if (lstReporteVentas != null)
            {
                foreach (var reporteDQB in lstReporteVentas)
                {
                    if (reporteDQB.Boletos != null)
                    {
                        // Solo se filtran los boletos que nacieron VOID, los boletos que están VOID por el usuario NO porque se necesitarán para el reporte de AVISO_VOID_DQB_NO_EN_PTA
                        var lstBoletoReporteVentaDQB = reporteDQB.Boletos.Where(boleto => !boleto.PNR.Equals("NO PNR")).ToList();
                        if (lstBoletoReporteVentaDQB.Any())
                        {
                            // Solo para el caso de DM necesitamos filtrar estos boletos VOID
                            lstBoletoReporteVentaDQB = lstBoletoReporteVentaDQB.Where(boleto => !boleto.Estado.Equals("VOID")).ToList();
                            // Boletos emitidos en el pseudo QQ05
                            if (reporteDQB.Pseudo.Equals(PSEUDO_DM))
                            {
                                if (lstBoletoReporteVentaDQB.Any())
                                {
                                    //Vendedores DM
                                    var vendedoresDM = objDominio.ObtenerVendedoresPorEmpresa(Configuracion.codigoSeguimiento, Configuracion.firmaBD, Configuracion.EsquemaDB.Destinos);
                                    var vendedoresDM_CounterIA = vendedoresDM.Where(v => ID_DEPARTAMENTO_COUNTER_IA_EN_DM.Equals(v.IdDepartamentoVendedor)).ToList();
                                    // Obtener todos los boletos Emitidos en Destinos Mundiales y filtramos los que no están VOID
                                    var boletosRegistradosPTA_DM = obtenerBoletoRegistradosPTA(Configuracion.EsquemaDB.Destinos);
                                    var boletosRegistradosPTA_Activos = new List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente>();
                                    if (boletosRegistradosPTA_DM != null && boletosRegistradosPTA_DM.Any())
                                    {
                                        boletosRegistradosPTA_Activos = boletosRegistradosPTA_DM.Where(boleto => boleto.MarcaVoid == 0).ToList();
                                    }
                                    //Recorrer todos los boletos emitidos en QQ05
                                    foreach (var boletoEnDQB in lstBoletoReporteVentaDQB)
                                    {
                                        // Todos los boletos Emitidos al DK de Nuevo Mundo en Destinos por qué son doble facturación NM => DM
                                        var boletoEsDobleFacturacion = boletosRegistradosPTA_Activos.Where(boleto => boletoEnDQB.NumBoleto.EndsWith(boleto.NumeroBoleto) && boleto.Cliente.DK == DK_NM_EN_DM).FirstOrDefault() != null;
                                        //vendedoresCounterIA.Where(v => v.IdFirmaVendedor.EndsWith(boleto.Vendedor.FirmaAgente.Substring(1))).ToList().Any())
                                        // Boleto fue emitido por counter IA en DM
                                        var boletoEsEmitidoPorCounterIA = vendedoresDM_CounterIA.Where(v => v.IdFirmaVendedor.Equals(boletoEnDQB.Agente.Substring(1, 2))).ToList().Any();
                                        if (boletoEsDobleFacturacion || boletoEsEmitidoPorCounterIA)
                                        {
                                            var objBoleto = new GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente();
                                            objBoleto.Pseudo = reporteDQB.Pseudo;
                                            objBoleto.NombrePseudo = reporteDQB.NombrePseudo;
                                            objBoleto.PrefijoBoleto = boletoEnDQB.NumBoleto.Substring(0, 3);
                                            objBoleto.Hora = boletoEnDQB.Hora;
                                            objBoleto.NumeroBoleto = boletoEnDQB.NumBoleto.Substring(3, 10);
                                            objBoleto.PNR = boletoEnDQB.PNR;
                                            objBoleto.NombrePasajero = boletoEnDQB.NombrePasajero;
                                            var auxAgente = new GDS_NuevoMundoPersistencia.classDatosAgente();
                                            auxAgente.FirmaAgente = boletoEnDQB.Agente;
                                            objBoleto.Vendedor = auxAgente;
                                            objBoleto.Estado = boletoEnDQB.Estado;
                                            objBoleto.EmisionPseudoNM = boletoEsDobleFacturacion;
                                            lstBoletoEmitidoDQB.Add(objBoleto);
                                        }
                                    }
                                }
                            }
                            else
                            {
                                foreach (var boleto in lstBoletoReporteVentaDQB)
                                {
                                    var objBoleto = new GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente();
                                    objBoleto.Pseudo = reporteDQB.Pseudo;
                                    objBoleto.NombrePseudo = reporteDQB.NombrePseudo;
                                    objBoleto.PrefijoBoleto = boleto.NumBoleto.Substring(0, 3);
                                    objBoleto.Hora = boleto.Hora;
                                    objBoleto.NumeroBoleto = boleto.NumBoleto.Substring(3, 10);
                                    objBoleto.PNR = boleto.PNR;
                                    objBoleto.NombrePasajero = boleto.NombrePasajero;
                                    var auxAgente = new GDS_NuevoMundoPersistencia.classDatosAgente();
                                    auxAgente.FirmaAgente = boleto.Agente;
                                    objBoleto.Vendedor = auxAgente;
                                    objBoleto.EmisionPseudoNM = false;
                                    objBoleto.Estado = boleto.Estado;
                                    lstBoletoEmitidoDQB.Add(objBoleto);
                                }
                            }
                        }
                    }
                }
            }
            return lstBoletoEmitidoDQB;
        }


        /// <summary>
        /// Realiza el Reporte diario de la fecha especificada
        /// </summary>
        /// <returns></returns>
        public List<GDS_NuevoMundoPersistencia.classReporteVentas> obtenerReporteDQB(string pseudosAEvaluar)
        {
            var session = utilSession.getSession();
            var jsonSerialize = new System.Web.Script.Serialization.JavaScriptSerializer();
            var GNM_ConsultasPTA = new GNM_ConsultaPTA.gnm();
            var lstReporteVentas = new List<GDS_NuevoMundoPersistencia.classReporteVentas>();
            foreach (string pseudo in pseudosAEvaluar.Split('/'))
            {
                int intContadorIntentos = 1;
                while (intContadorIntentos < 4)
                {
                    try
                    {
                        logger.info("Consultando Reporte del pseudo : " + pseudo);
                        var reporteAuxiliar = jsonSerialize.Deserialize<List<GDS_NuevoMundoPersistencia.classReporteVentas>>(GNM_ConsultasPTA.SWS_DQB(fechaReporte, pseudo, session.Token, session.ConversationID, Configuracion.codigoSeguimiento, Configuracion.idGDS, Configuracion.firmaGDS, Configuracion.firmaBD));

                        if (reporteAuxiliar != null)
                        {
                            if (reporteAuxiliar[0].MSGError == null)
                            {
                                logger.info("Se encontraron " + reporteAuxiliar[0].Boletos.Count + " boletos...");
                                lstReporteVentas.AddRange(reporteAuxiliar);
                            }
                            else
                            {
                                logger.info("Se encontró un error en reporte diario " + reporteAuxiliar[0].MSGError);
                            }
                        }
                        else
                        {
                            logger.info("No se encontraron boletos emitidos");
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        Thread.Sleep(3000);
                        logger.info("Ocurrió un error al ejecutar el reporte diario, Nro de Intentos : " + intContadorIntentos);
                        logger.info(ex.ToString());
                        intContadorIntentos += 1;
                        new Utilitario().envioSMS("Verificar! Ocurrió un error al realizar el reporte diario del pseudo: " + pseudo, "ROBOT_AVISO_GDS", Configuracion.contactosEnvioSMS);
                    }
                }
            }
            utilSession.closeSession(session);
            return lstReporteVentas;
        }

        /// <summary>
        ///     Retorna los boletos en el DQB que deben ser evaluados en el proceso
        /// </summary>
        /// <param name="boletosEnDQB"></param>
        /// <param name="vendedores"></param>
        /// <returns></returns>
        private List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> filtrarBoletosAEvaluar(List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletosEnDQB, List<GDS_NuevoMundoPersistencia.classDatosAgente> vendedores)
        {
            // Si no es el proceso de anulación entonces solo debe evaluar los boletos emitidos 10 minutos antes del proceso
            // Si el proceso inicia a las 6.00 PM solo evaluará boletos emitidos hasta las 5.50 PM
            // Esto debido a que 10 minutos es el tiempro promedio en que caen los archivos PNR y PTA los procesa
            // Solo GDS Sabre
            if (tipoProceso != TipoProceso.ANULACION && Configuracion.Gds == GDS.SABRE)
            {
                int horaAuxiliar = int.Parse(DateTime.Now.AddMinutes(-10).ToString("HHmm", CultureInfo.CurrentCulture));
                boletosEnDQB = boletosEnDQB.Where(boleto => int.Parse(boleto.Hora) <= horaAuxiliar).ToList();
            }

            // Si es Aviso No Facturados Evaluar Todos los boletos en DQB

            if (tipoProceso == TipoProceso.AVISO_NO_FACTURADOS || tipoProceso == TipoProceso.AVISO_NO_EN_PTA)
            {
                return boletosEnDQB;
            }

            //Vendedoras del Counter IA
            var vendedoresCounterIA = vendedores.Where(v => v.IdDepartamentoVendedor.Equals(Configuracion.dptoCounter)).ToList();

            var boletosAEvaluar = new List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente>();

            foreach (var boleto in boletosEnDQB)
            {
                // Evaluar todos los boletos emitidos por los pseudos publicada y por los pseudos no VOID
                if (Configuracion.pseudosPublicada.Contains(boleto.Pseudo))
                {
                    boletosAEvaluar.Add(boleto);
                }
                else if (boleto.EmisionPseudoNM)
                {  // Si es un boleto emitido al Pseudo NM - del QQ05
                    boletosAEvaluar.Add(boleto);
                }
                else if (boleto.Vendedor.FirmaAgente.EndsWith(Configuracion.firmaRobot))
                { //Evaluar todos los Boletos emitidos por firma robot
                    boletosAEvaluar.Add(boleto);
                }
                // Si el boleto fue emitido en un pseudo privado filtramos a los que fueron emitidos por las counter IA
                else if (Configuracion.pseudosPrivada.Contains(boleto.Pseudo) && vendedoresCounterIA.Where(v => v.IdFirmaVendedor.EndsWith(boleto.Vendedor.FirmaAgente.Substring(1))).ToList().Any())
                {
                    boletosAEvaluar.Add(boleto);
                }
            }
            return boletosAEvaluar;
        }

        /// <summary>
        /// Consolida / Unifica los datos de los boletos del DQB y los boletos PTA
        /// </summary>
        /// <param name="boletosEnDQB">Boletos DQB</param>
        /// <param name="lstReporteBoletoEmitidoPTA">Boletos PTA</param>
        /// <returns></returns>
        public List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> consolidarBoletos(List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletosEnDQB, List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> lstReporteBoletoEmitidoPTA)
        {

            if (boletosEnDQB == null || !boletosEnDQB.Any() || lstReporteBoletoEmitidoPTA == null || !lstReporteBoletoEmitidoPTA.Any())
            {
                return new List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente>();
            }

            var boletosConsolidados = new List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente>();

            var vendedores = objDominio.ObtenerVendedoresPorEmpresa(Configuracion.codigoSeguimiento, Configuracion.firmaBD, Configuracion.EsquemaDB.Actual);

            var boletosAEvaluar = filtrarBoletosAEvaluar(boletosEnDQB, vendedores);

            if (boletosAEvaluar.Any())
            {
                foreach (var boletoEnDQB in boletosAEvaluar)
                {
                    try
                    {
                        var boletoPTA = lstReporteBoletoEmitidoPTA.Where(boleto => boleto.NumeroBoleto.Equals(boletoEnDQB.NumeroBoleto)).FirstOrDefault();

                        // No evaluar los boletos Emitidos al DK de Agcorp en NM, doble facturación
                        if (Configuracion.empresa.Equals("NM") && boletoPTA != null && boletoPTA.Cliente.DK == DK_AGCORP_EN_NM)
                        {
                            continue;
                        }

                        var eBoleto = new GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente();
                        eBoleto.NumeroBoleto = boletoEnDQB.NumeroBoleto;
                        eBoleto.PrefijoBoleto = boletoEnDQB.PrefijoBoleto;
                        eBoleto.PNR = boletoEnDQB.PNR;
                        eBoleto.Pseudo = boletoEnDQB.Pseudo;
                        eBoleto.NombrePseudo = boletoEnDQB.NombrePseudo;
                        eBoleto.NombrePasajero = boletoEnDQB.NombrePasajero;
                        eBoleto.Hora = boletoEnDQB.Hora;
                        eBoleto.Estado = boletoEnDQB.Estado;

                        var auxAgente = new GDS_NuevoMundoPersistencia.classDatosAgente();
                        auxAgente.FirmaAgente = boletoEnDQB.Vendedor.FirmaAgente;
                        var objVendedor = vendedores.Where(ven => ven.IdFirmaVendedor.EndsWith(auxAgente.FirmaAgente.Substring(1))).FirstOrDefault();
                        if (objVendedor != null)
                        {
                            auxAgente.CorreoVendedor = objVendedor.CorreoVendedor;
                            auxAgente.CorreoJefe = objVendedor.CorreoJefe;
                            auxAgente.NombreVendedor = objVendedor.NombreVendedor;
                            auxAgente.IdVendedor = objVendedor.IdVendedor;
                        }

                        if (boletoPTA == null)
                        {
                            // Boleto no está en PTA
                            eBoleto.Vendedor = auxAgente;
                            eBoleto.ExistePTA = false;
                        }
                        else
                        {
                            auxAgente.CorreoCaja = boletoPTA.Vendedor.CorreoCaja;
                            eBoleto.Vendedor = auxAgente;
                            eBoleto.ExistePTA = true;

                            var auxCliente = new GDS_NuevoMundoPersistencia.classCliente();
                            auxCliente.DK = boletoPTA.Cliente.DK;
                            auxCliente.TipoDeCliente = boletoPTA.Cliente.TipoDeCliente;
                            auxCliente.NombreComercial = boletoPTA.Cliente.NombreComercial;
                            auxCliente.EmailAgencia = boletoPTA.Cliente.EmailAgencia;
                            auxCliente.Condicion = boletoPTA.Cliente.Condicion;
                            eBoleto.Cliente = new GDS_NuevoMundoPersistencia.classCliente();
                            eBoleto.Cliente = auxCliente;

                            var auxPromotor = new GDS_NuevoMundoPersistencia.classPromotor();
                            auxPromotor.NombrePromotor = boletoPTA.Promotor.NombrePromotor;
                            auxPromotor.EmailPromotor = boletoPTA.Promotor.EmailPromotor;
                            auxPromotor.CodigoVendedor = boletoPTA.Promotor.CodigoVendedor;

                            var objVendedorByCode = vendedores.Where(ven => ven.IdVendedor.Equals(boletoPTA.Promotor.CodigoVendedor)).FirstOrDefault();
                            if (objVendedorByCode != null)
                            {
                                auxPromotor.CorreoJefePromotor = objVendedorByCode.CorreoJefe;
                            }
                            eBoleto.Promotor = new GDS_NuevoMundoPersistencia.classPromotor();
                            eBoleto.Promotor = auxPromotor;

                            eBoleto.IdSucursal = boletoPTA.IdSucursal;
                            eBoleto.Descripcion = boletoPTA.Descripcion;
                            eBoleto.File = boletoPTA.File;
                            eBoleto.IdProveedor = boletoPTA.IdProveedor;
                            eBoleto.FechaAltaPTA = boletoPTA.FechaAltaPTA;
                            eBoleto.FechaEmision = boletoPTA.FechaEmision;
                            eBoleto.Gds = boletoPTA.Gds;
                            eBoleto.Ruta = boletoPTA.Ruta;
                            eBoleto.MarcaVoid = boletoPTA.MarcaVoid;
                            eBoleto.NoAnular = boletoPTA.NoAnular;
                            eBoleto.MontoVenta = boletoPTA.MontoVenta;
                            eBoleto.MontoAplicado = boletoPTA.MontoAplicado;
                            eBoleto.MarcaFacturado = boletoPTA.MarcaFacturado;
                            eBoleto.ID_FacturaCabeza = boletoPTA.ID_FacturaCabeza;
                            eBoleto.IdTipoComprobante = boletoPTA.IdTipoComprobante;
                            eBoleto.NumeroSerie1 = boletoPTA.NumeroSerie1;
                            eBoleto.Comprobante = boletoPTA.IdTipoComprobante + "-" + boletoPTA.NumeroSerie1 + "-" + boletoPTA.ID_FacturaCabeza;
                            eBoleto.IdCliente = boletoPTA.Cliente.DK;
                            eBoleto.MontoPendiente = boletoPTA.MontoPendiente;
                            eBoleto.MontoOtroDK = boletoPTA.MontoOtroDK;
                            eBoleto.TotalPendiente = double.Parse(eBoleto.MontoPendiente) - double.Parse(eBoleto.MontoOtroDK);
                            eBoleto.File = boletoPTA.File;
                        }
                        boletosConsolidados.Add(eBoleto);
                    }
                    catch (Exception e)
                    {
                        logger.info(string.Format("Error Metodo consolidarBoletos, Evaluando el boleto {0} - {1} , descripcion : {2} ", boletoEnDQB.PNR, boletoEnDQB.NumeroBoleto, e.ToString()));
                        throw e;
                    }
                };
            }
            return boletosConsolidados;
        }

        /// <summary>
        /// Verifica que los boletos que no están en el PTA actual tampoco estén en otro PTA
        /// </summary>
        /// <param name="boletosNoFiguranPTA">Boletos que no figuran en PTA actual</param>
        /// <returns>Boletos filtrados</returns>
        public List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> filtrarBoletosNoFiguranEnOtroPTA(List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletosNoFiguranPTA)
        {
            if (string.IsNullOrEmpty(fechaReporte))
            {
                throw new Exception("No se ha especificado la fecha del Reporte ");
            }
            try
            {
                if (Configuracion.empresa.Equals("NM"))
                {
                    // Verificar Boletos de AGCORP
                    var strProveedoresAGCORP = string.Join(",", Configuracion.Proveedor.proveedoresAGCORP);
                    var boletosEmitidosAGCORP = objDominio.ReporteBoletosEmitidos_X(fechaReporte, Configuracion.idGDS, strProveedoresAGCORP, Configuracion.codigoSeguimiento, Configuracion.firmaBD, Configuracion.EsquemaDB.Agcorp);
                    if (boletosEmitidosAGCORP != null && boletosEmitidosAGCORP.Any())
                    {
                        boletosNoFiguranPTA = boletosNoFiguranPTA.Where(boleto => !boletosEmitidosAGCORP.Any(boletoOtroPTA => boletoOtroPTA.NumeroBoleto == boleto.NumeroBoleto)).ToList();
                    }

                    // Verificar Boletos de DM
                    var strProveedoresDM = string.Join(",", Configuracion.Proveedor.proveedoresDM);
                    var boletosEmitidosDM = objDominio.ReporteBoletosEmitidos_X(fechaReporte, Configuracion.idGDS, strProveedoresDM, Configuracion.codigoSeguimiento, Configuracion.firmaBD, Configuracion.EsquemaDB.Destinos);
                    if (boletosEmitidosDM != null && boletosEmitidosDM.Any())
                    {
                        boletosNoFiguranPTA = boletosNoFiguranPTA.Where(boleto => !boletosEmitidosDM.Any(boletoOtroPTA => boletoOtroPTA.NumeroBoleto == boleto.NumeroBoleto)).ToList();
                    }
                }
                else if (Configuracion.empresa.Equals("AGCORP"))
                {
                    // Verificar Boletos en NM
                    var strProveedoresNM = string.Join(",", Configuracion.Proveedor.proveedoresNM);
                    var boletosEmitidosNM = objDominio.ReporteBoletosEmitidos_X(fechaReporte, Configuracion.idGDS, strProveedoresNM, Configuracion.codigoSeguimiento, Configuracion.firmaBD, Configuracion.EsquemaDB.NM);
                    if (boletosEmitidosNM != null && boletosEmitidosNM.Any())
                    {
                        boletosNoFiguranPTA = boletosNoFiguranPTA.Where(boleto => !boletosEmitidosNM.Any(boletoOtroPTA => boletoOtroPTA.NumeroBoleto == boleto.NumeroBoleto)).ToList();
                    }
                }
                else
                {
                    throw new Exception("Empresa No Soportada, Solo disponibles NM y AGCORP");
                }
            }
            catch (Exception e)
            {
                writeLogException("filtrarBoletosNoFiguranEnOtroPTA", e);
                throw e;
            }
            return boletosNoFiguranPTA;
        }


        /// <summary>
        ///     Filtrar boletos con deuda pendiente 
        /// </summary>
        /// <param name="boletosConsolidados">Boletos a Filtrar</param>
        /// <returns>Lista boletos con deuda pendiente</returns>
        public List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> obtenerBoletosDeudaPendiente(List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletosConsolidados)
        {
            logger.info("Metodo: obtenerBoletosDeudaPendiente...");
            var boletosTienenDeudaPendiente = new List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente>();

            try
            {
                const string CONDICION_CONTADO = "CON";
                const int TIPO_CLIENTE_AGENCIA = 1;
                const int DK_PASAJERO_DIRECTO = 339;
                int HoraMaximaEvalucionHoy = Configuracion.obtenerHorarioRobot().horaMaximaEvaluacion;

                boletosTienenDeudaPendiente = boletosConsolidados.Where(boleto =>
                                                                                   (boleto.IdCliente == Configuracion.dkPrueba) ||
                                                                                   (
                                                                                      boleto.MarcaFacturado == 1
                                                                                      && double.Parse(boleto.Hora) <= HoraMaximaEvalucionHoy
                                                                                      && boleto.MarcaVoid == 0
                                                                                      && Configuracion.sucursales.Contains(boleto.IdSucursal)
                                                                                      && boleto.Cliente.Condicion.Equals(CONDICION_CONTADO)
                                                                                      && boleto.NoAnular.Equals("VOIDEAR")
                                                                                      && boleto.TotalPendiente > 0
                                                                                      && (boleto.Cliente.TipoDeCliente == TIPO_CLIENTE_AGENCIA || boleto.IdCliente == DK_PASAJERO_DIRECTO)
                                                                                   )
                                                                                   ).ToList();
                logger.info(string.Format("Boletos con deuda pendiente - Count: {0}", boletosTienenDeudaPendiente.Count()));
            }
            catch (Exception e)
            {
                writeLogException("obtenerBoletosDeudaPendiente", e);
                throw e;
            }

            return boletosTienenDeudaPendiente;
        }

        public void writeLogException(string nombreMetodo, Exception ex)
        {
            logger.info(string.Format("ServiceBoleto. Ocurrió un error al ejecutar el método {0}(), descripcion: {1}", nombreMetodo, ex.ToString()));
        }
    }
}
