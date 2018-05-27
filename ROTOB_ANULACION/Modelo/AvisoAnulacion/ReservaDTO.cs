using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Modelo
{
    public class ReservaDTO
    {

        public string PNR { get; set; }

        public List<BoletoDTO> Boletos { get; set; }
    }
}
