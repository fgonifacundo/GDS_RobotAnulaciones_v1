using GDS_NM_Mensajeria;
using GDS_NuevoMundoPersistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION
{
    public class MailUtils
    {
        private GestorLog logger;
        private EnviarEmail objEnviarEmail;

        private static MailUtils instance;

        private MailUtils()
        {
            logger = new GestorLog();
            objEnviarEmail = new EnviarEmail();
        }

        public static MailUtils getInstance() {
            if (instance == null) {
                
                instance = new MailUtils();
            }
            return instance;
        }

        /// <summary>
        ///     Envía mail de confirmación al terminar un proceso en concreto.
        /// </summary>
        /// <param name="nombreCorreo"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        private void sendMail(string nombreCorreo, string subject, string body)
        {
            try
            {
                var objCorreo = new classCorreo();
                objCorreo.ToCorreo = Configuracion.correo_EmailRobotAlertas;
                objCorreo.BCCCorreo = Configuracion.correo_EmailNotificacion;
                objCorreo.NombreCorreo = string.Format("{0} - {1}", Configuracion.empresa, nombreCorreo);
                objCorreo.SubjectCorreo = string.Format("{0} - {1}", Configuracion.empresa, subject);
                objCorreo.BodyCorreo = body;
                var pathLog = logger.getRutaArchivoLog();
                objEnviarEmail.SendAttachment(objCorreo, true, "ROBOTANULACIONES", 3, new List<string> { pathLog });
            }
            catch (Exception e)
            {
                logger.info("Ocurrió un error al enviar mail : " + e.ToString());
            }
        }

        /// <summary>
        ///     Envía un correo genérico informando de algún error ocurrido en un proceso
        /// </summary>
        /// <param name="exception">Exception generada que se notificará</param>
        public void sendMailError(Exception exception, TipoProceso tipoProceso)
        {
            logger.info("Enviando mail de error...");
            var nombreCorreo = string.Format("ERROR EN ROBOT DE ANULACIONES, Proceso: {0}", tipoProceso.ToString());
            var subjectCorreo = "ERROR EN ROBOT DE ANULACIONES ";
            var bodyCorreo = "ERROR AL EJECUTAR EL ROBOT DE ANULACIONES " + exception;
            sendMail(nombreCorreo, subjectCorreo, bodyCorreo);
        }

        /// <summary>
        ///     Envía un correo genérico informando la conformidad de un proceso
        /// </summary>
        /// <param name="tipoProceso"></param>
        public void sendMailConfirmacion(TipoProceso tipoProceso)
        {
            logger.info("Enviando mail de Confirmacion...");
            var nombreCorreo = string.Format("EL ROBOT DE {0} SABRE SE EJECUTÓ SATISFACTORIAMENTE", tipoProceso.ToString());
            var subjectCorreo = string.Format("EL ROBOT DE {0} SABRE SE EJECUTÓ SATISFACTORIAMENTE ", tipoProceso.ToString());
            var bodyCorreo = string.Format("EL ROBOT DE {0} SABRE SE EJECUTÓ SATISFACTORIAMENTE ", tipoProceso.ToString());
            sendMail(nombreCorreo, subjectCorreo, bodyCorreo);
        }

    }
}
