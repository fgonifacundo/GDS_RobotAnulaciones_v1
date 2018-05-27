using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION
{
    public class GestorCorreo
    {
        GestorLog logger = new GestorLog();

        private string servidor = "10.75.102.2";
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string EmailDisplay { get; set; }
        public string[] EmailAttachments { get; set; }

        public void Enviar()
        {
            Enviar(null, null, null, null, null, null);
        }

        public void Enviar(string para, string copia, string copiaOculta, string asunto, string contenido, string display)
        {
            try
            {
                using (var lcorreo = new MailMessage())
                {
                    using (var lcliente = new SmtpClient())
                    {
                        var lpara = string.IsNullOrEmpty(para) ? EmailTo : para;
                        var lcopia = string.IsNullOrEmpty(copia) ? EmailCC : copia;
                        var lcopiaOculta = string.IsNullOrEmpty(copiaOculta) ? EmailBCC : copiaOculta;
                        var lasunto = string.IsNullOrEmpty(asunto) ? EmailSubject : asunto;
                        var lcontenido = string.IsNullOrEmpty(contenido) ? EmailBody : contenido;
                        var ldisplay = string.IsNullOrEmpty(display) ? EmailDisplay : display;

                        lpara = string.IsNullOrEmpty(lpara) ? Configuracion.correo_EmailPruebas : lpara;

                        if (Configuracion.IsTest)
                        {
                            lpara = Configuracion.correo_EmailPruebas;
                            lcopia = string.Empty;
                            lcopiaOculta = string.Empty;
                        }

                        lcliente.Host = servidor;
                        lcorreo.From = new MailAddress(EmailFrom, ldisplay);

                        lpara.Trim()
                            .Split(';')
                            .ToList()
                            .ForEach(
                                e => lcorreo.To.Add(new MailAddress(e))
                             );

                        if (!string.IsNullOrWhiteSpace(lcopia))
                        {
                            lcopia.Trim()
                                .Split(';')
                                .ToList()
                                .ForEach(
                                    e => lcorreo.CC.Add(new MailAddress(e))
                                 );
                        }

                        if (!string.IsNullOrWhiteSpace(lcopiaOculta))
                        {
                            lcopiaOculta.Trim()
                                    .Split(';')
                                    .ToList()
                                    .ForEach(
                                        e => lcorreo.Bcc.Add(new MailAddress(e))
                                    );
                        }

                        if (EmailAttachments != null)
                        {
                            EmailAttachments.ToList().ForEach(at => lcorreo.Attachments.Add(new Attachment(at)));
                        }
                        lcorreo.BodyEncoding = Encoding.UTF8;
                        lcorreo.IsBodyHtml = true;
                        lcorreo.Subject = lasunto.Trim();
                        lcorreo.Body = lcontenido;
                        lcliente.Send(lcorreo);
                    }
                }
            }
            catch (Exception e)
            {
                logger.info(e.ToString());
            }
        }
    }
}
