using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION
{
    public interface IProcesoRobot
    {
        void execute(GDS_NuevoMundoPersistencia.classSession session,
                    List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletos);

        void execute(GDS_NuevoMundoPersistencia.classSession session,
                    List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletos,
                    string fecha);
        
    }
}
