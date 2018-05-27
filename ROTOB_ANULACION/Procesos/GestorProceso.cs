using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION
{
    public class GestorProceso
    {
        private GDS_NuevoMundoPersistencia.classSession session;

        public GestorProceso(GDS_NuevoMundoPersistencia.classSession session)
        {
            this.session = session;
        }

        public void ejecutarProceso(IProcesoRobot proceso, List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletos, string fecha)
        {
            proceso.execute(session, boletos, fecha);
        }

        public void ejecutarProceso(IProcesoRobot proceso, List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletos)
        {
            proceso.execute(session, boletos);
        }
    }
}
