using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.ClienteRest
{
    public sealed class InternalException : Exception, IDisposable
    {
        // =============================
        // variables

        #region "variables"

        private bool _disposing;

        #endregion

        // =============================
        // constructores y destructores

        #region "constructores y destructores"

        public InternalException(string message,
                                 CE_Mensaje[] more)
            : base(message)
        {
            if ((more != null) && more.Any(m => (!string.IsNullOrWhiteSpace(m.Valor))))
            {
                More = string.Join(
                        "\n",
                        more.Where(m => (!string.IsNullOrWhiteSpace(m.Valor)))
                            .Select(m => m.Valor)
                                .ToArray()
                    );
            }
        }

        public InternalException(string message,
                                 string more)
            : base(message)
        {
            More = more;
        }

        public InternalException(string message)
            : this(message, ((string)null))
        {
        }

        ~InternalException()
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
                    More = null;
                }
            }

            _disposing = true;
        }

        #endregion

        // =============================
        // auto propiedades

        #region "auto propiedades"

        public string More { set; get; }

        #endregion
    }
}
