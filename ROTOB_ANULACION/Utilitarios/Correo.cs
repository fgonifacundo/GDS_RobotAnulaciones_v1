using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Utilitarios
{
    public sealed class Correo
    {
        // =============================
        // auto propiedades

        #region "auto propiedades"
        public string EmailServer { set; get; }
        public string EmailFrom { set; get; }
        public string EmailToTest { set; get; }
        public string EmailCC { set; get; }
        public string EmailBCC { set; get; }
        public string EmailSubjectBegin { set; get; }

        #endregion

        #region "metodos"

        public void Enviar(string servidor,
                           string de,
                           string para,
                           string paraTest,
                           string copia,
                           string copiaOculta,
                           string asunto,
                           string contenido)
        {
            var lservidor    = ( string.IsNullOrWhiteSpace(servidor)    ? EmailServer : servidor).Trim();
            var lde          = ( string.IsNullOrWhiteSpace(de)          ? EmailFrom   : de).Trim();
            var lcopia       = ( string.IsNullOrWhiteSpace(copia)       ? EmailCC     : copia).Trim();
            var lcopiaOculta = ( string.IsNullOrWhiteSpace(copiaOculta) ? EmailBCC    : copiaOculta).Trim();

            using (var lcorreo = new MailMessage())
            {
                using (var lcliente = new SmtpClient())
                {
                    // configurando cliente para envio
                    lcliente.Host = lservidor;
                    lcorreo.From = new MailAddress(lde, "Alertas GDS");
                    
                    // evaluando 
                    para.Replace(" ", string.Empty)
                                .Split(';')
                                .ToList()
                                .ForEach(
                                    e => lcorreo.To.Add(new MailAddress(e))
                                );

                    // evaluando que se haya indicado correos para copia
                    if (!string.IsNullOrWhiteSpace(lcopia))
                    {
                        lcopia.Replace(" ", string.Empty)
                                    .Split(';')
                                    .ToList()
                                    .ForEach(
                                        e => lcorreo.CC.Add(new MailAddress(e))
                                    );
                    }
                    
                    // evaluando que se haya indicado correos para copia oculta
                    if (!string.IsNullOrWhiteSpace(lcopiaOculta))
                    {
                        lcopiaOculta.Replace(" ", string.Empty)
                                .Split(';')
                                .ToList()
                                .ForEach(
                                    e => lcorreo.Bcc.Add(new MailAddress(e))
                                );
                    }

                    lcorreo.BodyEncoding = Encoding.UTF8;
                    lcorreo.IsBodyHtml = true;
                    lcorreo.Subject = (string.IsNullOrWhiteSpace(EmailSubjectBegin) ? asunto : string.Format("{0} {1}", EmailSubjectBegin, asunto).Trim());
                    lcorreo.Body = contenido;

                    // realizando envio del correo
                    lcliente.Send(lcorreo);
                }
            }
        }

        public void Enviar(string para,
                           string asunto,
                           string contenido)
        {
            Enviar(null, null, para, null, null, null, asunto, contenido);
        }

        public void Enviar(string asunto,
                           string contenido)
        {
            Enviar(null, null, null, null, null, null, asunto, contenido);
        }

        #endregion
    }
}
