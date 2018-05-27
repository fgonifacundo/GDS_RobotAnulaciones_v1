using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Modelo
{
    public class BoletoDTO
    {
        public string IdPrefijo { set; get; }
        public string NumeroBoleto { set; get; }
        public string BoletoFull { get; set; }
        public string NombrePasajero { set; get; }
        public string NoAnular { set; get; }
        public double TotalPendiente { get; set; }
        public string Estado { get; set; }
        public bool? Voideado { set; get; }
        public bool? Facturado { set; get; }
        public VendedorDTO Vendedor { get; set; }
        public string NombrePromotor { get; set; }

        public string MensajeError { get; set; }
      
    }
}
