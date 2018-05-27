using ROTOB_ANULACION.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Procesos
{
    public class ProcesadorReporteNoFacturado
    {
        FileUtils fileUtils = null;
        string classCenter = " class='tarifario_fila_a' align='center'";

        public ProcesadorReporteNoFacturado()
        {
            fileUtils = FileUtils.getInstance();
        }

        private bool procesoNoFacturado(List<BoletoRobotDTO> boletosProcesar)
        {
            var lresultado = false;
            var boletosNoFacturados = boletosProcesar.Where(boleto => boleto.ExisteEnPTA && !boleto.EsAnuladoPTA && !boleto.EsFacturado).ToList();
            if (boletosNoFacturados.Any())
            {
                var boletosAgrupados = agruparBoletosNoFacturados(boletosNoFacturados);
                var lhtml = new StringBuilder();
                boletosAgrupados.ForEach(lPseudo =>
                {
                    var lrowspanPseudo = lPseudo.Sucursales.Sum(s => s.Files.Sum(c => c.Reservas.Sum(r => r.Boletos.Count)));
                    var lhtmlPseudo = string.Format("<td height='30' rowspan='{0}'> {1} </td>", lrowspanPseudo, lPseudo.IdPseudo);
                    lPseudo.Sucursales.ForEach(lsucursal =>
                    {
                        var lrowspanSucursal = lsucursal.Files.Sum(f => f.Reservas.Sum(p => p.Boletos.Count));
                        var lhtmlSucursal = string.Format("<td rowspan='{0}'> {1} </td>", lrowspanSucursal, lsucursal.DescripcionSucursal);
                        lsucursal.Files.ForEach(lfile =>
                        {
                            var lrowspanFile = lfile.Reservas.Sum(r => r.Boletos.Count);
                            var lhtmlFile = string.Format("<td rowspan='{0}'> {1} </td>", lrowspanFile, lfile.IdFile);
                            lfile.Reservas.ForEach(lreserva =>
                            {
                                var lrowspanReserva = lreserva.Boletos.Count;
                                var lhtmlReserva = string.Format("<td rowspan='{0}'> {1} </td>", lrowspanReserva, lreserva.PNR);
                                lreserva.Boletos.ForEach(lboleto =>
                                {
                                    lhtml.AppendLine("<tr>");
                                    lhtml.AppendLine(lhtmlPseudo);
                                    lhtml.AppendLine(lhtmlSucursal);
                                    lhtml.AppendLine(lhtmlFile);
                                    lhtml.AppendLine(lhtmlReserva);
                                    lhtml.AppendLine(string.Format("<td> {0} </td>", lboleto.BoletoFull));
                                    lhtml.AppendLine(string.Format("<td> {0} </td>", lboleto.NombrePasajero));
                                    lhtml.AppendLine(string.Format("<td> {0} </td>", lboleto.NombrePromotor));
                                    lhtml.AppendLine(string.Format("<td> {0} </td>", lboleto.Estado));
                                    lhtmlPseudo = string.Empty;
                                    lhtmlSucursal = string.Empty;
                                    lhtmlFile = string.Empty;
                                    lhtmlReserva = string.Empty;
                                    lhtml.AppendLine("</tr>");
                                });
                            });
                        });
                    });
                });

                var lnombreCounter = "Flavio Goni";
                var lencabezado = string.Format("Estimado(a): {0}, ", lnombreCounter);

                var lestructuraHtml = fileUtils.read(fileUtils.getPath(@"HTML\EstructuraBaseCorreo.html"));

                var lcorreoHtml = fileUtils.read(fileUtils.getPath(@"HTML\AvisoNoFacturados.html"));
                lcorreoHtml = lcorreoHtml.Replace("$Fecha", DateTime.Now.ToString("F"));
                lcorreoHtml = lcorreoHtml.Replace("$encabezado", lencabezado);
                lcorreoHtml = lcorreoHtml.Replace("$tbody", lhtml.ToString());
                lcorreoHtml = lcorreoHtml.Replace("$PSEUDO", Configuracion.idGDS == 0 ? "OFICINA" : "PSEUDO");
                lestructuraHtml = lestructuraHtml.Replace("$bodyHTML", lcorreoHtml);
            }
            return lresultado;
        }

        public List<PseudoDTO> agruparBoletosNoFacturados(List<BoletoRobotDTO> boletosProcesar)
        {
            return boletosProcesar
                     .GroupBy(ps => new { ps.Pseudo })
                     .Select(gps => new PseudoDTO
                     {
                         IdPseudo = gps.Key.Pseudo,
                         Sucursales = boletosProcesar
                             .Where(s => s.Pseudo.Equals(gps.Key.Pseudo))
                             .GroupBy(s => new { s.IdSucursal, s.DescripcionSucursal })
                             .Select(gs => new SucursalDTO
                             {
                                 IdSucursal = gs.Key.IdSucursal,
                                 DescripcionSucursal = gs.Key.DescripcionSucursal,
                                 Files = boletosProcesar
                                     .Where(b => b.Pseudo.Equals(gps.Key.Pseudo) && b.IdSucursal == gs.Key.IdSucursal)
                                     .GroupBy(f => new { f.NroFile })
                                     .Select(gf => new FileDTO
                                     {
                                         IdFile = gf.Key.NroFile,
                                         Reservas = boletosProcesar
                                             .Where(b => b.Pseudo.Equals(gps.Key.Pseudo) && b.IdSucursal == gs.Key.IdSucursal && b.NroFile.Equals(gf.Key.NroFile))
                                             .GroupBy(r => new { r.PNR })
                                             .Select(gr => new ReservaDTO
                                             {
                                                 PNR = gr.Key.PNR,
                                                 Boletos = boletosProcesar
                                                     .Where(b => b.Pseudo.Equals(gps.Key.Pseudo) && b.IdSucursal == gs.Key.IdSucursal && b.NroFile.Equals(gf.Key.NroFile) && b.PNR.Equals(gr.Key.PNR))
                                                     .GroupBy(p => new { p.NumeroBoleto, p.NombrePasajero, p.Estado, p.NombrePromotor, p.BoletoFull })
                                                     .Select(gb => new BoletoDTO
                                                     {
                                                         NumeroBoleto = gb.Key.NumeroBoleto,
                                                         NombrePasajero = gb.Key.NombrePasajero,
                                                         Estado = gb.Key.Estado,
                                                         NombrePromotor = gb.Key.NombrePromotor,
                                                         BoletoFull = gb.Key.BoletoFull
                                                     }).ToList()
                                             }).ToList()
                                     }).ToList()
                             }).ToList()
                     }).ToList();

        }
    }

}
