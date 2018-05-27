using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.ClienteRest
{
    public sealed class CE_Estatus : IDisposable
    {
        // =============================
        // variables

        #region "variables"

        private bool _disposing;

        #endregion

        // =============================
        // constructores y destructores

        #region "constructores y destructores"

        public CE_Estatus(InternalException excepcion)
            : this()
        {
            if (string.IsNullOrWhiteSpace(excepcion.More))
            {
                Mensajes = new[] { new CE_Mensaje(excepcion) };

            }
            else
            {
                Mensajes = new[] { new CE_Mensaje(excepcion), new CE_Mensaje(excepcion.More) };
            }
        }

        public CE_Estatus(Exception excepcion)
            : this()
        {
            Mensajes = new[] { new CE_Mensaje(excepcion) };
        }

        public CE_Estatus(string valor)
            : this()
        {
            Mensajes = new[] { new CE_Mensaje(valor) };
        }

        public CE_Estatus(bool ok)
            : this()
        {
            Ok = ok;
        }

        public CE_Estatus()
        {
            Ok = false;
            Mensajes = null;
        }

        ~CE_Estatus()
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
                    Mensajes = null;
                }
            }

            _disposing = true;
        }

        #endregion

        // =============================
        // auto propiedades

        #region "auto propiedades"

        public bool Ok { set; get; }
        public CE_Mensaje[] Mensajes { set; get; }

        #endregion

        // =============================
        // auto propiedades

        #region "auto propiedades"

        public void Registrar(IEnumerable<string> textos)
        {
            if ((textos != null) && textos.Any(t => (!string.IsNullOrWhiteSpace(t))))
            {
                // inicializando los mensajes
                Mensajes = (Mensajes ?? new CE_Mensaje[0]);

                // actualizando mensajes
                Mensajes = Mensajes.Union(textos
                    .Where(t => (!string.IsNullOrWhiteSpace(t)))
                    .Select(t => new CE_Mensaje { Valor = t })
                ).ToArray();
            }
        }

        public void Registrar(string texto)
        {
            Registrar(new[] { texto });
        }

        public void RegistrarAlertas(IEnumerable<string> textos)
        {
            if ((textos != null) && textos.Any(t => (!string.IsNullOrWhiteSpace(t))))
            {
                // inicializando los mensajes
                Mensajes = (Mensajes ?? new CE_Mensaje[0]);

                // actualizando mensajes
                Mensajes = Mensajes.Union(textos
                    .Where(t => (!string.IsNullOrWhiteSpace(t)))
                    .Select(t => new CE_Mensaje
                    {
                        Tipo = EnumTipoMensaje.Alerta,
                        Valor = t
                    })).ToArray();
            }
        }

        public void RegistrarAlerta(string texto)
        {
            RegistrarAlertas(new[] { texto });
        }

        public void RegistrarErrores(IEnumerable<string> textos)
        {
            if ((textos != null) && textos.Any(t => (!string.IsNullOrWhiteSpace(t))))
            {
                // inicializando los mensajes
                Mensajes = (Mensajes ?? new CE_Mensaje[0]);

                // actualizando mensajes
                Mensajes = Mensajes.Union(textos
                    .Where(t => (!string.IsNullOrWhiteSpace(t)))
                    .Select(t => new CE_Mensaje
                    {
                        Tipo = EnumTipoMensaje.Error,
                        Valor = t
                    })).ToArray();
            }
        }

        public void RegistrarError(string texto)
        {
            RegistrarErrores(new[] { texto });
        }

        #endregion
    }
}
