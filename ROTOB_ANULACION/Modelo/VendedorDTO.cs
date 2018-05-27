using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Modelo
{
    public class VendedorDTO
    {
        #region "auto propiedades"

        public string Id { get; set; }

        public string Firma { get; set; }
        
        public string Nombre { get; set; }

        public string Correo { get; set; }
        
        public string CorreoJefe { get; set; }

        public string IdDepartamento { get; set; }

        #endregion "auto propiedades"
    }
}
