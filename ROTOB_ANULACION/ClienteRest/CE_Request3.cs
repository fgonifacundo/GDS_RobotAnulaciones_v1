using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.ClienteRest
{
    public sealed class CE_Request3<TParameters> : CE_RequestBase
    {

        // =============================
        // constructores y destructores

        #region "constructores y destructores"

        ~CE_Request3()
        {
            Dispose(false);
        }

        public new void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public new void Dispose(bool disposing)
        {
            if (!_disposing)
            {
                if (disposing)
                {
                    CodigosEntorno = null;
                    Sesion = null;
                    Parametros = default(TParameters);

                    base.Dispose(true);
                }
            }

            _disposing = true;
        }

        #endregion

        // =============================
        // auto propiedades

        #region "auto propiedades"

        public string CodigosEntorno { set; get; }
        public CE_Session Sesion { set; get; }
        public TParameters Parametros { set; get; }

        #endregion

    }
}
