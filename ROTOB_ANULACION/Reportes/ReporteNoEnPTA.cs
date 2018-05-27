using ROTOB_ANULACION.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Reportes
{
    public class ReporteNoEnPTA : ReporteFactory
    {
        public ReporteNoEnPTA(List<Modelo.BoletoRobotDTO> boletosProcesar)
        {
            this.boletosProcesar = boletosProcesar;
        }

        public override string ConstruirTablaReporte()
        {
            var boletosAgrupados = AgruparBoletos();
            var lhtml = new StringBuilder();
            boletosAgrupados.ForEach(lPseudo =>
            {
                //PSEUDO	PNR	NÚMERO BOLETO	NOMBRE PASAJERO	ESTADO
                var lhtmlPseudo = string.Format("<td height='30' rowspan='{0}'> {1} </td>", lPseudo.Reservas.Sum(r => r.Boletos.Count), lPseudo.IdPseudo);
                lPseudo.Reservas.ForEach(lreserva =>
                {
                    var lrowspanReserva = lreserva.Boletos.Count;
                    var lhtmlReserva = string.Format("<td rowspan='{0}'> {1} </td>", lrowspanReserva, lreserva.PNR);
                    lreserva.Boletos.ForEach(lboleto =>
                    {
                        lhtml.AppendLine("<tr>");
                        lhtml.AppendLine(lhtmlPseudo);
                        lhtml.AppendLine(lhtmlReserva);
                        lhtml.AppendLine(string.Format("<td> {0} </td>", lboleto.BoletoFull));
                        lhtml.AppendLine(string.Format("<td> {0} </td>", lboleto.NombrePasajero));
                        lhtml.AppendLine(string.Format("<td> {0} </td>", lboleto.Estado));
                        lhtmlPseudo = string.Empty;
                        lhtmlReserva = string.Empty;
                        lhtml.AppendLine("</tr>");
                    });
                });
            });

            var ltablaReporte = fileUtils.read(fileUtils.getPath(@"Reportes\HTML\AvisoNoEnPTA_Tabla.html"));
            
            ltablaReporte = ltablaReporte.Replace("$tbody", lhtml.ToString());
            return ltablaReporte;
        }

        public override string ConstruirCorreo(string encabezado, string mensaje, string cuerpo)
        {
            var lestructuraHtml = fileUtils.read(fileUtils.getPath(@"Reportes\HTML\EstructuraBaseCorreo.html"));
            var lcorreoHtml = fileUtils.read(fileUtils.getPath(@"Reportes\HTML\AvisoNoEnPTA.html"));
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
                    Reservas = boletosProcesar
                       .Where(b => b.Pseudo.Equals(gps.Key.Pseudo))
                       .GroupBy(r => new { r.PNR })
                       .Select(gr => new ReservaDTO
                       {
                           PNR = gr.Key.PNR,
                           Boletos = boletosProcesar
                                .Where(b => b.Pseudo.Equals(gps.Key.Pseudo) && b.PNR.Equals(gr.Key.PNR))
                                .GroupBy(p => new { p.NumeroBoleto, p.NombrePasajero, p.Estado, p.NombrePromotor, p.BoletoFull })
                                .Select(gb => new BoletoDTO
                                {
                                    NombrePasajero = gb.Key.NombrePasajero,
                                    Estado = gb.Key.Estado,
                                    NombrePromotor = gb.Key.NombrePromotor,
                                    BoletoFull = gb.Key.BoletoFull
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
