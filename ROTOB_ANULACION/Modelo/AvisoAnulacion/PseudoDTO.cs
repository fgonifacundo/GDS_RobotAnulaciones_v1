using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Modelo
{
    public sealed class PseudoDTO
    {
        public string IdPseudo { get; set; }

        public List<SucursalDTO> Sucursales { get; set; }

        public List<ReservaDTO> Reservas { get; set; }
    }
}
