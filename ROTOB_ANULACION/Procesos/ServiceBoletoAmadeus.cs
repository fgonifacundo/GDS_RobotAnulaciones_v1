using ROTOB_ANULACION.Amadeus_WS;
using ROTOB_ANULACION.Modelo;
using ROTOB_ANULACION.Persistencia;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ROTOB_ANULACION.Procesos
{
    public class ServiceBoletoAmadeus : BoletoFactory
    {
        private TipoProceso tipoProceso;
        private string fecha;
        private const int DK_AGCORP_EN_NM = 49946;

        public ServiceBoletoAmadeus(TipoProceso tipoProceso, string fecha)
        {
            this.tipoProceso = tipoProceso;
            this.fecha = fecha;
            logger.info(string.Format("[TipoProceso: {0} - Fecha: {1}]", tipoProceso, fecha));
        }

        public override List<BoletoRobotDTO> ObtenerBoletosGDS()
        {
            List<BoletoRobotDTO> lboletosEmitidosDQB = null;
            if (tipoProceso == TipoProceso.ANULACION)
            {
                lboletosEmitidosDQB = ObtenerReportDiarioAlmacenado();
            }
            else 
            {
                lboletosEmitidosDQB = ObtenerReporteDiarioGDS();
            }
            logger.info(string.Format("=> [ObtenerBoletosGDS] : se encontraron {0} boletos", lboletosEmitidosDQB.Count));
            return lboletosEmitidosDQB;
        }

        public List<BoletoRobotDTO> ObtenerReportDiarioAlmacenado()
        {
            var lboletosAlmacenados = new List<BoletoRobotDTO>();
            var ljsonBoletosEmitidos = fileUtils.read(ObtenerRutaBoletosAmadeus());
            if (!string.IsNullOrEmpty(ljsonBoletosEmitidos))
            {
                lboletosAlmacenados = new JavaScriptSerializer().Deserialize<List<BoletoRobotDTO>>(ljsonBoletosEmitidos);
            }
            logger.info(string.Format("=> [ObtenerReportDiarioAlmacenado] : se encontraron {0} boletos almacenados", lboletosAlmacenados.Count));
            return lboletosAlmacenados;
        }

        private string ObtenerRutaBoletosAmadeus()
        {
            var lfecha = Regex.Replace(dateUtils.obtenerFechaHoy(), "/", "-");
            var lruta = string.Format("{0}/boletos_amadeus_{1}.txt", Configuracion.RutaFileBoletosAmadeus, lfecha);
            logger.info(string.Format("=> [ObtenerRutaBoletosAmadeus] : Ruta almacena boletos {0} ", lruta));
            return lruta;
        }

        public override List<BoletoRobotDTO> ObtenerReporteDiarioGDS()
        {
            var lboletosEmitidosDQB = new List<BoletoRobotDTO>();
            var reporteDiario = new WS_BoletoAmadeus().ObtenerReporteDiario(Configuracion.OficinasAmadeus, fecha);
            if (reporteDiario != null)
            {
                reporteDiario.ForEach(reporte =>
                {
                    if (reporte.Error == null)
                    {
                        lboletosEmitidosDQB.AddRange(
                            reporte.Boletos.Select(boleto =>
                            {
                                return new BoletoRobotDTO
                                {
                                    PrefijoBoleto = boleto.NumBoleto.Substring(0, 3),
                                    NumeroBoleto = boleto.NumBoleto.Substring(3),
                                    PNR = boleto.PNR,
                                    NombrePasajero = boleto.NombrePasajero,
                                    Estado = boleto.Estado.Equals("CANX") ? "VOID" : "ACTIVO",
                                    HoraEmision = "0",
                                    FirmaAgente = boleto.Agente,
                                    Pseudo = reporte.Oficina,
                                    BoletoFull = boleto.NumBoleto
                                };
                            }).ToList()
                           );
                    }
                    else
                    {
                        logger.info(string.Format("Error en reporte Amadeus: {0}", reporte.Error));
                    }
                });
            }
            logger.info(string.Format("=> [ObtenerReporteDiarioGDS] : se encontraron {0} boletos en el GDS ", lboletosEmitidosDQB.Count));
            return lboletosEmitidosDQB;
        }

        private void mostrarBoletosPosiblementeAAnularse() {
            var boletosConDeudaPendiente = ObtenerBoletosConDeudaPendiente();
            if (boletosConDeudaPendiente.Any()) {
                logger.info("Boletos con deuda pendiente hasta este momento:");
                boletosConDeudaPendiente.ForEach(boleto =>
               {
                   logger.info(string.Format("[ DEUDA_PENDIENTE ] :: [{0} - {1} - {2}] ::  Total Pendiente:{3} | Emisor: {4} - {5} | DK:{6} | Estado:{7}",
                    boleto.Pseudo, boleto.PNR, boleto.BoletoFull, boleto.TotalPendiente.ToString().PadLeft(8), boleto.Vendedor.Firma, boleto.Vendedor.Id, boleto.IdCliente.ToString().PadLeft(7), boleto.Estado.PadLeft(7)));
               });
            }
        }

        public override bool AlmacenarBoletos()
        {
            bool lalmacenadoConExito = false;
            var boletosConsolidados = ObtenerReporteDiarioGDS(); 
            if (boletosConsolidados.Any()) 
            {
                AlmacenarReporteHoraLimiteEmision(boletosConsolidados);
                lalmacenadoConExito = File.Exists(ObtenerRutaBoletosAmadeus());
                mostrarBoletosPosiblementeAAnularse();
            }
            logger.info(string.Format("=> [AlmacenarBoletos] : se almacenaron {0} boletos emitidos hasta ahora!", boletosConsolidados.Count));
            return lalmacenadoConExito;
        }

        public void AlmacenarReporteHoraLimiteEmision(List<BoletoRobotDTO> boletosEmitidosDQB)
        {
            if (Directory.Exists(Configuracion.RutaFileBoletosAmadeus))
            {
                Directory.GetFiles(Configuracion.RutaFileBoletosAmadeus).ToList().ForEach(File.Delete);      /* Elimina archivos anteriores */
            }
            else
            {
                fileUtils.crearDirectorioSiNoExiste(Configuracion.RutaFileBoletosAmadeus);
            }
            var ljsonBoletosDQB = Utilitario.Serializar(boletosEmitidosDQB);
            fileUtils.write(ObtenerRutaBoletosAmadeus(), ljsonBoletosDQB);
        }

        public override List<BoletoRobotDTO> ObtenerBoletosPTA(int esquema, List<string> proveedores)
        {
            var lboletosEnPTA = new BoletoDAOImpl().obtenerBoletosPTA(esquema, fecha, Configuracion.idGDS, string.Join(",", proveedores));
            logger.info(string.Format("=> [ObtenerBoletosPTA] : se encontraron {0} boletos en PTA esquema {1} ", lboletosEnPTA.Count, esquema));
            return lboletosEnPTA;
        }

        public List<VendedorDTO> ObtenerVendedores(int esquema)
        {
            BoletoDAO boletoDAO = new BoletoDAOImpl();
            return boletoDAO.obtenerVendedores(esquema);
        }


        public override List<BoletoRobotDTO> UnificarBoletos()
        {

            throw new NotImplementedException();
        }

        public List<BoletoRobotDTO> ObtenerBoletosConsolidados()
        {
            return ObtenerBoletosConsolidados(false);
        }

        public override List<BoletoRobotDTO> ObtenerBoletosConsolidados(bool incluirVOIDenGDS)
        {
            var lboletosConsolidados = new List<BoletoRobotDTO>();
            var lboletosEnGDS = ObtenerBoletosGDS();

            if (!incluirVOIDenGDS)
            {
                lboletosEnGDS = lboletosEnGDS.Where(boleto => boleto.Estado.Equals("ACTIVO")).ToList();
            }

            var lboletosEnPTA = ObtenerBoletosPTA(Configuracion.idGDS, Configuracion.Proveedor.proveedoresActual);
            var lvendedores = ObtenerVendedores(Configuracion.EsquemaDB.Actual);
            lboletosEnGDS.ForEach(lboletoGDS =>
            {
                try
                {
                    lboletoGDS.Vendedor = lvendedores.Find(vendedor => vendedor.Firma.Equals(lboletoGDS.FirmaAgente));
                    var lboletoEnPTA = lboletosEnPTA.Find(boleto => boleto.NumeroBoleto.Equals(lboletoGDS.NumeroBoleto));
                    if (lboletoEnPTA != null)
                    {
                        if (Configuracion.empresa.Equals("NM"))
                        {
                            if (lboletoEnPTA.IdCliente == DK_AGCORP_EN_NM) {
                                return;
                            }
                        }
                        lboletoGDS.ExisteEnPTA = true;
                        lboletoGDS.IdVendedor = lboletoEnPTA.IdVendedor;
                        lboletoGDS.IdCliente = lboletoEnPTA.IdCliente;
                        lboletoGDS.TipoDeCliente = lboletoEnPTA.TipoDeCliente;
                        lboletoGDS.NombreCliente = lboletoEnPTA.NombreCliente;
                        lboletoGDS.CorreoCliente = lboletoEnPTA.CorreoCliente;
                        lboletoGDS.CondicionPagoCliente = lboletoEnPTA.CondicionPagoCliente;

                        lboletoGDS.IdPromotor = lboletoEnPTA.IdPromotor;
                        lboletoGDS.NombrePromotor = lboletoEnPTA.NombrePromotor;
                        lboletoGDS.CorreoPromotor = lboletoEnPTA.CorreoPromotor;

                        var vendedorPromotor = lvendedores.Find(ven => ven.Id.Equals(lboletoGDS.IdPromotor));
                        lboletoGDS.CorreoJefePromotor = vendedorPromotor != null ? vendedorPromotor.CorreoJefe : string.Empty;

                        lboletoGDS.IdSucursal = lboletoEnPTA.IdSucursal;
                        lboletoGDS.DescripcionSucursal = lboletoEnPTA.DescripcionSucursal;
                        lboletoGDS.NroFile = lboletoEnPTA.NroFile;
                        lboletoGDS.IdProveedor = lboletoEnPTA.IdProveedor;
                        lboletoGDS.EsAnuladoPTA = lboletoEnPTA.EsAnuladoPTA;
                        lboletoGDS.DebeAnular = lboletoEnPTA.DebeAnular;
                        lboletoGDS.EsFacturado = lboletoEnPTA.EsFacturado;
                        lboletoGDS.IdFacturaCabeza = lboletoEnPTA.IdFacturaCabeza;
                        lboletoGDS.IdTipoComprobante = lboletoEnPTA.IdTipoComprobante;
                        lboletoGDS.NumeroSerie1 = lboletoEnPTA.NumeroSerie1;
                        lboletoGDS.IdTipoComprobante = lboletoEnPTA.IdTipoComprobante;
                        lboletoGDS.Comprobante = lboletoEnPTA.IdTipoComprobante + "-" + lboletoEnPTA.NumeroSerie1 + "-" + lboletoEnPTA.IdFacturaCabeza;
                        lboletoGDS.MontoPendiente = lboletoEnPTA.MontoPendiente;
                        lboletoGDS.MontoOtroDK = lboletoEnPTA.MontoOtroDK;
                        lboletoGDS.TotalPendiente = lboletoEnPTA.MontoPendiente - lboletoEnPTA.MontoOtroDK;

                        lboletoGDS.EsEmpresaGrupo = lboletoEnPTA.EsEmpresaGrupo;
                        lboletoGDS.CorreoCaja = lboletoEnPTA.CorreoCaja;
                    }
                    lboletosConsolidados.Add(lboletoGDS);
                }
                catch (Exception e)
                {
                    logger.info(e.ToString());
                }
            });
            return lboletosConsolidados;
        }


        public List<BoletoRobotDTO> ObtenerBoletosConDeudaPendiente(List<BoletoRobotDTO> lboletosConsolidados)
        {
            if (lboletosConsolidados.Any())
            {
                return lboletosConsolidados.Where(boleto =>
                    (boleto.IdCliente == Configuracion.dkPrueba) ||
                    (
                        boleto.EsFacturado
                        && !boleto.EsAnuladoPTA
                        && !boleto.EsEmpresaGrupo
                        && boleto.CondicionPagoCliente.Equals(CONDICION_CONTADO)
                        && Configuracion.sucursales.Contains(boleto.IdSucursal)
                        && boleto.DebeAnular
                        && boleto.TotalPendiente > 0
                        && (boleto.TipoDeCliente == TIPO_CLIENTE_AGENCIA || boleto.IdCliente == DK_PASAJERO_DIRECTO)
                    )
                ).ToList();
            }
            return lboletosConsolidados;
        }

        public override List<BoletoRobotDTO> ObtenerBoletosConDeudaPendiente()
        {
            var lboletosConsolidados = ObtenerBoletosConsolidados();
            return ObtenerBoletosConDeudaPendiente(lboletosConsolidados);
        }

        public override List<BoletoRobotDTO> ObtenerBoletosNoEncuentranEnPTA()
        {
            var lboletosConsolidados = ObtenerBoletosConsolidados();
            return lboletosConsolidados.Where(boleto => !boleto.ExisteEnPTA).ToList();
        }

        public override List<BoletoRobotDTO> ObtenerBoletosNoFacturados()
        {
            var lboletosConsolidados = ObtenerBoletosConsolidados();
            return lboletosConsolidados.Where(boleto => !boleto.EsAnuladoPTA && boleto.ExisteEnPTA && !boleto.EsFacturado).ToList();
        }
    }
}
