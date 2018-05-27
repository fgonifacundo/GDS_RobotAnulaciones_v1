using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.ClienteRest
{
    public class CE_RequestBase
    {
        // =============================
        // variables

        #region "variables"

        protected bool _disposing;

        #endregion

        // =============================
        // constructores y destructores

        #region "constructores y destructores"

        ~CE_RequestBase()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            if (!_disposing)
            {
                if (disposing)
                {
                    Aplicacion = null;
                    CodigoSeguimiento = null;
                }
            }

            _disposing = true;
        }

        #endregion

        // =============================
        // auto propiedades

        #region "auto propiedades"

        public EnumAplicaciones? Aplicacion { set; get; }
        public string CodigoSeguimiento { set; get; }

        #endregion
    }
}
