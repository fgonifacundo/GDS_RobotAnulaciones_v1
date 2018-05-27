using ROTOB_ANULACION.Modelo;
using ROTOB_ANULACION.Utilitarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Reportes
{
    public class ProcesadorReporteAvisoAnulacion
    {
        FileUtils fileUtils = null;

        public ProcesadorReporteAvisoAnulacion()
        {
            fileUtils = FileUtils.getInstance();
        }

        public string ConstruirTablaReporte(List<BoletoRobotDTO> boletosProcesar)
        {
            var ltablaReporte = fileUtils.read(fileUtils.getPath(@"HTML\AvisoAnulacion_Tabla.html"));

            var lhtml = new StringBuilder();
            var boletosDeudaPendiente = boletosProcesar.Where(boleto => boleto.ExisteEnPTA && !boleto.EsAnuladoPTA && boleto.EsFacturado).ToList();
            if (boletosDeudaPendiente.Any())
            {
                var boletosAgrupados = AgruparBoletosAviso_Anulacion(boletosDeudaPendiente);
                boletosAgrupados.ForEach(lPseudo =>
                {
                    var lrowspanPseudo = lPseudo.Sucursales.Sum(s => s.Files.Sum(f => f.Comprobantes.Sum(c => c.Reservas.Sum(r => r.Boletos.Count))));
                    var lhtmlPseudo = string.Format("<td rowspan='{0}'> {1} </td>", lrowspanPseudo, lPseudo.IdPseudo);
                    lPseudo.Sucursales.ForEach(lsucursal =>
                    {
                        var lrowspanSucursal = lsucursal.Files.Sum(f => f.Comprobantes.Sum(c => c.Reservas.Sum(r => r.Boletos.Count)));
                        var lhtmlSucursal = string.Format("<td rowspan='{0}'> {1} </td>", lrowspanSucursal, lsucursal.DescripcionSucursal);
                        lsucursal.Files.ForEach(lfile =>
                        {
                            var lrowspanFile = lfile.Comprobantes.Sum(s => s.Reservas.Sum(c => c.Boletos.Count));
                            var lhtmlFile = string.Format("<td rowspan='{0}'> {1} </td>", lrowspanFile, lfile.IdFile);
                            lhtmlFile += string.Format("<td rowspan='{0}'> {1} </td>", lrowspanFile, lfile.Cliente.IdCliente);
                            lfile.Comprobantes.ForEach(lcomprobante =>
                            {
                                var lrowspanComprobante = lcomprobante.Reservas.Sum(c => c.Boletos.Count);
                                var lhtmlComprobante = string.Format("<td rowspan='{0}'> {1} </td>", lrowspanComprobante, lcomprobante.ComprobanteFull);
                                lcomprobante.Reservas.ForEach(lreserva =>
                                {
                                    var lrowspanReserva = lreserva.Boletos.Count;
                                    var lhtmlReserva = string.Format("<td rowspan='{0}'> {1} </td>", lrowspanReserva, lreserva.PNR);
                                    lreserva.Boletos.ForEach(lboleto =>
                                    {
                                        lhtml.AppendLine("<tr>");
                                        lhtml.AppendLine(lhtmlPseudo);
                                        lhtml.AppendLine(lhtmlSucursal);
                                        lhtml.AppendLine(lhtmlFile);
                                        lhtml.AppendLine(lhtmlComprobante);
                                        lhtml.AppendLine(lhtmlReserva);
                                        lhtml.AppendLine(string.Format("<td> {0} </td>", lboleto.BoletoFull));
                                        lhtml.AppendLine(string.Format("<td> {0} </td>", lboleto.NombrePasajero));
                                        lhtml.AppendLine(string.Format("<td> {0} </td>", (!string.IsNullOrEmpty(lboleto.NombrePromotor)) ? lboleto.NombrePromotor : "-"));
                                        lhtml.AppendLine(string.Format("<td> {0} </td>", lboleto));
                                        lhtml.AppendLine(string.Format("<td> {0} </td>", lboleto.Estado));
                                        lhtmlPseudo = string.Empty;
                                        lhtmlSucursal = string.Empty;
                                        lhtmlComprobante = string.Empty;
                                        lhtmlFile = string.Empty;
                                        lhtmlReserva = string.Empty;
                                        lhtml.AppendLine("</tr>");
                                    });
                                });
                            });
                        });
                    });
                });
            }
            ltablaReporte = ltablaReporte.Replace("$tbody", lhtml.ToString());
            return ltablaReporte;
        }

        public string ConstruirCorreo(string encabezado, string mensaje, string cuerpo)
        {
            var lestructuraHtml = fileUtils.read(fileUtils.getPath(@"HTML\EstructuraBaseCorreo.html"));
            var lcorreoHtml = fileUtils.read(fileUtils.getPath(@"HTML\AvisoAnulacion.html"));
            lcorreoHtml = lcorreoHtml.Replace("$Fecha", DateTime.Now.ToString("F"));
            lcorreoHtml = lcorreoHtml.Replace("$encabezado", encabezado);
            lcorreoHtml = lcorreoHtml.Replace("$mensaje", mensaje);
            lcorreoHtml = lcorreoHtml.Replace("$tablaContenido", cuerpo);
            lcorreoHtml = lcorreoHtml.Replace("$PSEUDO", Configuracion.idGDS == 0 ? "OFICINA" : "PSEUDO");
            return lestructuraHtml.Replace("$bodyHTML", lcorreoHtml);
        }

        private List<PseudoDTO> AgruparBoletosAviso_Anulacion(List<BoletoRobotDTO> boletosPendientesPago)
        {
            return boletosPendientesPago
                .GroupBy(g => new { g.Pseudo })
                .Select(gp => new PseudoDTO
                {
                    IdPseudo = gp.Key.Pseudo,
                    Sucursales = boletosPendientesPago.Where(b => b.Pseudo.Equals(gp.Key.Pseudo))
                        .GroupBy(g => new { g.IdSucursal, g.DescripcionSucursal })
                        .Select(gs => new SucursalDTO
                        {
                            IdSucursal = gs.Key.IdSucursal,
                            DescripcionSucursal = gs.Key.DescripcionSucursal,
                            Files = boletosPendientesPago.Where(b => b.Pseudo.Equals(gp.Key.Pseudo) && b.IdSucursal == gs.Key.IdSucursal)
                                .GroupBy(g => new { g.NroFile, g.IdCliente })
                                .Select(gf => new FileDTO
                                {
                                    IdFile = gf.Key.NroFile,
                                    Cliente = new ClienteDTO
                                    {
                                        IdCliente = gf.Key.IdCliente
                                    },
                                    Comprobantes = boletosPendientesPago.Where(b => b.Pseudo.Equals(gp.Key.Pseudo) && b.IdSucursal == gs.Key.IdSucursal && b.NroFile.Equals(gf.Key.NroFile))
                                        .GroupBy(g => new { g.IdTipoComprobante, g.IdFacturaCabeza, g.NumeroSerie1, g.Comprobante })
                                        .Select(gc => new ComprobanteDTO
                                        {
                                            ComprobanteFull = gc.Key.Comprobante,
                                            IdTipoComprobante = gc.Key.IdTipoComprobante,
                                            NumeroSerie = gc.Key.NumeroSerie1,
                                            IdFacturaCabeza = gc.Key.IdFacturaCabeza,
                                            Reservas = boletosPendientesPago.Where(b => b.Pseudo.Equals(gp.Key.Pseudo) && b.IdSucursal == gs.Key.IdSucursal && b.NroFile.Equals(gf.Key.NroFile) && b.Comprobante.Equals(gc.Key.Comprobante))
                                                 .GroupBy(g => new { g.PNR })
                                                 .Select(gr => new ReservaDTO
                                                 {
                                                     PNR = gr.Key.PNR,
                                                     Boletos = boletosPendientesPago.Where(b => b.Pseudo.Equals(gp.Key.Pseudo) && b.IdSucursal == gs.Key.IdSucursal && b.NroFile.Equals(gf.Key.NroFile) && b.Comprobante.Equals(gc.Key.Comprobante) && b.PNR.Equals(gr.Key.PNR))
                                                        .GroupBy(g => new { g.BoletoFull, g.IdCliente, g.NombrePasajero, g.NombrePromotor, g.TotalPendiente, g.Estado })
                                                        .Select(gpnr => new BoletoDTO
                                                        {
                                                            BoletoFull = gpnr.Key.BoletoFull,
                                                            NombrePasajero = gpnr.Key.NombrePasajero,
                                                            NombrePromotor = gpnr.Key.NombrePromotor,
                                                            TotalPendiente = gpnr.Key.TotalPendiente,
                                                            Estado = gpnr.Key.Estado
                                                        }).ToList()
                                                 }).ToList()
                                        }).ToList()
                                }).ToList()
                        }).ToList()
                }
                ).ToList();
        }
    
    }
}
