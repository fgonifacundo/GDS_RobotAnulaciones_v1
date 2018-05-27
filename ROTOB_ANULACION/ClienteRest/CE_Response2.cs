using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.ClienteRest
{
    public sealed class CE_Response2 : IDisposable
    {
        // =============================
        // variables

        #region "variables"

        private bool _disposing;

        #endregion

        // =============================
        // constructores y destructores

        #region "constructores y destructores"

        public CE_Response2(CE_Estatus estatus,
                            CE_Session sesion)
        {
            Estatus = estatus;
            Sesion = sesion;
        }

        public CE_Response2(CE_Estatus estatus)
            : this(estatus, null)
        {
        }

        public CE_Response2(InternalException excepcion)
            : this((new CE_Estatus(excepcion)))
        {
        }

        public CE_Response2(Exception excepcion)
            : this((new CE_Estatus(excepcion)))
        {
        }

        public CE_Response2(bool ok)
            : this(new CE_Estatus(ok))
        {
        }

        public CE_Response2()
            : this(true)
        {
        }

        ~CE_Response2()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposing)
            {
                if (disposing)
                {
                    Estatus = null;
                    Sesion = null;
                }
            }

            _disposing = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        // =============================
        // auto propiedades

        #region "auto propiedades"

        public CE_Estatus Estatus { set; get; }
        public CE_Session Sesion { set; get; }

        #endregion
    }
}
