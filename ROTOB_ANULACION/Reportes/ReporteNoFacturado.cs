using ROTOB_ANULACION.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Reportes
{
    public class ReporteNoFacturado : ReporteFactory
    {
        public ReporteNoFacturado(List<Modelo.BoletoRobotDTO> boletosProcesar)
        {
            this.boletosProcesar = boletosProcesar;
        }

        public override string ConstruirTablaReporte()
        {
            var boletosAgrupados = AgruparBoletos();
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
                                lhtml.AppendLine("</tr>");;
                            });
                        });
                    });
                });
            });
            var ltablaReporte = fileUtils.read(fileUtils.getPath(@"Reportes\HTML\AvisoNoFacturados_Tabla.html"));
            ltablaReporte = ltablaReporte.Replace("$tbody", lhtml.ToString());
            return ltablaReporte;
        }

        public override string ConstruirCorreo(string encabezado, string mensaje, string cuerpo)
        {
            var lestructuraHtml = fileUtils.read(fileUtils.getPath(@"Reportes\HTML\EstructuraBaseCorreo.html"));
            var lcorreoHtml = fileUtils.read(fileUtils.getPath(@"Reportes\HTML\AvisoNoFacturados.html"));
            lcorreoHtml = lcorreoHtml.Replace("$Fecha", DateTime.Now.ToString("F"));
            lcorreoHtml = lcorreoHtml.Replace("$encabezado", encabezado);
            lcorreoHtml = lcorreoHtml.Replace("$mensaje", mensaje);
            lcorreoHtml = lcorreoHtml.Replace("$tablaContenido", cuerpo);
            lcorreoHtml = lcorreoHtml.Replace("$PSEUDO", Configuracion.idGDS == 0 ? "OFICINA" : "PSEUDO");
            return lestructuraHtml.Replace("$bodyHTML", lcorreoHtml);
        }

        public override List<Modelo.PseudoDTO> AgruparBoletos()
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

        public override string ConstruirCorreo(string encabezado, string mensaje)
        {
            var cuerpo = ConstruirTablaReporte();
            return ConstruirCorreo(encabezado, mensaje, cuerpo);
        }
    }
}
