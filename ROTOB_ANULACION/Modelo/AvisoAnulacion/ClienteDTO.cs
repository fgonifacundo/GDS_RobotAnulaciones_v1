using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Modelo
{
    public sealed class ClienteDTO
    {
        #region "auto propiedades"

        public int? IdCliente { get; set; }
        public string Nombre { get; set; }
        public string CondicionPago { get; set; }
        public string  Correo { get; set; }
        public int? IdPromotor { get; set; }
        public string NombrePromotor { get; set; }
        public string CorreoPromotor  { get; set; }
        public string CorreoJefePromotor { get; set; }

        #endregion "auto propiedades"
    }
}
