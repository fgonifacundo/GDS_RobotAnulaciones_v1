using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Modelo
{
    public sealed class ComprobanteDTO
    {

        #region "auto propiedades"
        public string IdFacturaCabeza { get; set; }
        public string NumeroSerie { get; set; }
        public bool? Anulado { get; set; }
        public string Punto { get; set; }
        public string CorreoCaja { get; set; }
        public string IdTipoComprobante { get; set; }
        public string ComprobanteFull { get; set; }
        public List<ReservaDTO> Reservas { get; set; }
        

        #endregion "auto propiedades"

    }
}
