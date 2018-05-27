using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ROTOB_ANULACION
{
    public class Utilitario
    {

        private GestorLog logger = new GestorLog();

        public void envioSMS(string strMensaje, string strNomRemitente, string[] nrosCelulares)
        {
            nrosCelulares.ToList().ForEach( numero => envioSMS(strMensaje, strNomRemitente, numero));
        }

        public static String Serializar(Object target)
        {
           return new JavaScriptSerializer().Serialize(target);
        }

        private void envioSMS(string mensaje, string remitente, string nroCelular)
        {
            try
            {
                var endPointSMS = "http://10.72.102.52/cgi-bin/exec?";
                var builderdURL = new StringBuilder();
                builderdURL.Append(endPointSMS);
                builderdURL.Append("cmd=api_queue_sms").Append("&");
                builderdURL.Append("username=lyric_api").Append("&");
                builderdURL.Append("password=lyric_api").Append("&");
                builderdURL.Append("content=").Append(mensaje).Append(" - ").Append(remitente).Append("&");
                builderdURL.Append("destination=").Append(nroCelular).Append("&");
                builderdURL.Append("api_version=0.08&channel=8");

                var objHttpWebRequest = System.Net.WebRequest.Create(builderdURL.ToString());
                var objProxy = new WebProxy();
                objProxy.Credentials = new NetworkCredential("admin", "admin");
                objHttpWebRequest.Method = "GET";
                objHttpWebRequest.Proxy = objProxy;
                objHttpWebRequest.Credentials = new NetworkCredential("admin", "admin");
                objHttpWebRequest.ContentType = "application/x-www-form-urlencoded";

                var objHttpWebResponse = objHttpWebRequest.GetResponse();
                var objStreamReader = new StreamReader(objHttpWebResponse.GetResponseStream());
                objStreamReader.ReadToEnd();
            }
            catch (Exception ex)
            {
                logger.info(string.Format("Ocurrió un error al enviar SMS a {0} | Mensaje: {1} ", nroCelular, mensaje));
                logger.info(string.Format("Exception: {0}", ex.ToString()));
            }
        }

    }
}
