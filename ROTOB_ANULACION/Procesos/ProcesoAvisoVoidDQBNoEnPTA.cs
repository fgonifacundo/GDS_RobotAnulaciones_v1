using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Procesos
{
    public class ProcesoAvisoVoidDQBNoEnPTA: IProcesoRobot
    {
        public void execute(GDS_NuevoMundoPersistencia.classSession session, List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletos, string fecha)
        {
            throw new NotImplementedException();
        }

        public void execute(GDS_NuevoMundoPersistencia.classSession session, List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletos)
        {
            

        }
    }
}
