using ROTOB_ANULACION.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Procesos
{
    public class ProcesadorReporteNoEnPTA
    {
        FileUtils fileUtils = null;

        public ProcesadorReporteNoEnPTA()
        {
            fileUtils = FileUtils.getInstance();
        }

        public bool procesoNoEnPTA(List<BoletoRobotDTO> boletosProcesar)
        {
            var lresultado = false;
            var boletosNoExistePTA = boletosProcesar.Where(boleto => !boleto.ExisteEnPTA).ToList();
            if (boletosNoExistePTA.Any())
            {
                var boletosAgrupados = agruparBoletosNoEnPTA(boletosProcesar);
                var lhtml = new StringBuilder();
                boletosAgrupados.ForEach(lPseudo =>
                {
                    var lhtmlPseudo = string.Format("<td height='30' rowspan='{0}'> {1} </td> \n", lPseudo.Sucursales.Sum(s => s.Files.Sum(c => c.Reservas.Sum(r => r.Boletos.Count))), lPseudo.IdPseudo);
                    lPseudo.Reservas.ForEach(lreserva =>
                    {
                        var lrowspanReserva = lreserva.Boletos.Count;
                        var lhtmlReserva = string.Format("<td rowspan='{0}'> {1} </td> \n", lrowspanReserva, lreserva.PNR);
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
            }
            return lresultado;
        }

        public List<PseudoDTO> agruparBoletosNoEnPTA(List<BoletoRobotDTO> boletosProcesar)
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

    }
}
