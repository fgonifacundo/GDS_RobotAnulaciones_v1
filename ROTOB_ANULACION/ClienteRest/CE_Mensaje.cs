using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.ClienteRest
{
    public sealed class CE_Mensaje : IDisposable
    {
        // =============================
        // variables

        #region "variables"

        private bool _disposing;

        #endregion

        // =============================
        // constructores y destructores

        #region "constructores y destructores"

        public CE_Mensaje()
        {
            Tipo = null;
            Valor = null;
        }

        public CE_Mensaje(string valor)
            : this()
        {
            Valor = valor;
        }

        public CE_Mensaje(EnumTipoMensaje tipo,
                           string valor)
        {
            Tipo = tipo;
            Valor = valor;
        }

        public CE_Mensaje(InternalException excepcion)
            : this(EnumTipoMensaje.Alerta, excepcion.Message)
        {
        }

        public CE_Mensaje(Exception excepcion)
            : this(EnumTipoMensaje.Error, excepcion.Message)
        {
        }

        ~CE_Mensaje()
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
                    Tipo = null;
                    Valor = null;
                }
            }

            _disposing = true;
        }

        #endregion

        // =============================
        // auto propiedades

        #region "auto propiedades"

        public EnumTipoMensaje? Tipo { set; get; }
        public string Valor { set; get; }

        #endregion
    }
}
