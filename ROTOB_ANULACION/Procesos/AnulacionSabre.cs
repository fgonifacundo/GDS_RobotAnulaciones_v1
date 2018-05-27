using ROTOB_ANULACION.ClienteRest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Procesos
{
    public class AnulacionSabre
    {
        private string session;
        private string pnr;
        private string pseudo;

        public AnulacionSabre(string session, string pnr, string pseudo)
        {
            this.session = session;
            this.pnr = pnr;
        }

        private CE_Request3<string> buildRequest(string command)
        {
            var lcodigoSeguimiento = string.Format("Robot-Anulaciones-{0}", pnr).ToUpper();
            return new CE_Request3<string>
            {
                Aplicacion = EnumAplicaciones.SabreRed,
                CodigoSeguimiento = lcodigoSeguimiento,
                Parametros = command,
                Sesion = new CE_Session
                {
                    Token = session,
                    SignatureUser = lcodigoSeguimiento,
                    ConversationId = lcodigoSeguimiento,
                    Pseudo = pseudo
                }
            };
        }

        public async Task<CE_Response2> sendCommand(string command)
        {
            var request = buildRequest(command);
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("http://gds.webfarefinder.com/gds_serviciosgds/");
                HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/ServicioHerramientas.json/EjecutarComando", request);
                return await response.Content.ReadAsAsync<CE_Response2>();
            }
        }

        public bool procesar() {

            return false;
        }


    }
}
