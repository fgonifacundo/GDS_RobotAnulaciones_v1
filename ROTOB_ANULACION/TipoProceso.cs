using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION
{
    public enum TipoProceso
    {
        AVISO_NO_FACTURADOS,
        AVISO_NO_EN_PTA,   
        AVISO_ANULACION,
        ANULACION,
        AVISO_NO_FACTURADOS_AYER,
        AVISO_VOID_DQB_NO_EN_PTA,
        ALMACENAR_BOLETOS_EMITIDOS_AMADEUS
    }
}
