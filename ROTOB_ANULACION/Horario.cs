using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION
{
    public class Horario
    {
        public string horaAvisoAnulacion { get; set; }
        public string horaAnulacion { get; set; }
        public string horaAvisoNoPTA_NoFacturado { get; set; }
        public string horaAvisoNoFacturadoAyer { get; set; }
        public string horaAvisoVoidDQBNoEnPTA { get; set; }
        public string horaAlmacenarBoletosAnulacion { get; set; }
        public int horaMaximaEvaluacion { get; set; }
        
    }
}
