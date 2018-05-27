using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Amadeus_WS
{
    public class ReporteDiario
    {
        public string Fecha { get; set; }
        public string Error { get; set; }
        public string Oficina { get; set; }
        public List<Boleto> Boletos { get; set; }

    }

    public class Boleto
    {
        public string PNR { get; set; }
        public string Agente { get; set; }
        public string Estado { get; set; }
        public string NombrePasajero { get; set; }
        public string NumBoleto { get; set; }
        public string FormaPago { get; set; }

    }
}
