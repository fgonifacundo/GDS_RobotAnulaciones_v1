using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION
{
    public static class Configuracion
    {
        public static Horario horarioRobot { get; set; }
        public static string pseudosPublicada { get; set; }
        public static string pseudosPrivada { get; set; }
        public static string pseudosNoVoid { get; set; }
        public static string pseudosUnificados { get; set; }
        public static string pseudosEmpresa { get; set; }
        public static List<int> sucursales { get; set; }
        public static int dkPrueba { get; set; }
        public static string correo_EmailRobotAlertas { get; set; }
        public static int firmaGDS { get; set; }
        public static int idGDS { get; set; }
        public static string empresa { get; set; }
        public static string firmaRobot { get; set; }
        public static string dptoCounter { get; set; }
        public static string codigoSeguimiento { get; set; }
        public static int firmaBD { get; set; }
        public static string[] contactosEnvioSMS { get; set; }
        private static string HoraAvisoAnulacion_LV { get; set; }
        private static string HoraAnulacion_LV { get; set; }
        private static string HoraAvisoNoFacturado_LV { get; set; }
        private static string HoraAvisoVoidEnDQBNoEnPTA_LV { get; set; }
        public static string HoraAlmacenarBoletosAnulacion_LV { get; set; }
        private static int HoraMaximaEvaluacion_LV { get; set; }
        private static string HoraAvisoAnulacion_Sabado { get; set; }
        private static string HoraAnulacion_Sabado { get; set; }
        private static string HoraAvisoNoFacturado_Sabado { get; set; }
        private static string HoraAvisoVoidEnDQBNoEnPTA_Sabado { get; set; }
        public static string HoraAlmacenarBoletosAnulacion_Sabado { get; set; }
        private static string HoraAvisoNoFacturadoAyer { get; set; }
        private static int HoraMaximaEvaluacion_Sabado { get; set; }
        public static bool IsTest { get; set; }
        public static GDS Gds { get; set; }
        public static string correo_WebContacto { get; set; }
        public static string correo_WebPortal { get; set; }
        public static string correo_HoraAnulacion { get; set; }
        public static string correo_HoraLimitePago { get; set; }

        public static string correo_EmailCoordConsolidador { get; set; }
        public static string correo_EmailCounterTurno { get; set; }
        public static string correo_EmailSupervisorCounterIA { get; set; }
        public static string correo_EmailPromotorAlternativo { get; set; }
        public static string correo_EmailNotificacion { get; set; }
        public static string correo_EmailPruebas { get; set; }
        
        public static string RutaArchivoLog { get; set; }
        public static string OficinasAmadeus { get; set; }
        public static string RutaFileBoletosAmadeus { get; set; }

        static Configuracion()
        {
            Gds = idGDS == 0 ? GDS.AMADEUS : GDS.SABRE;

            empresa = ConfigurationManager.AppSettings["EMPRESA"].ToString();
            
            firmaRobot = ConfigurationManager.AppSettings["FIRMA_ROBOT"].ToString();
            sucursales = ConfigurationManager.AppSettings["SUCURSAL"].ToString().Split('/').Select(int.Parse).ToList();
            dptoCounter = ConfigurationManager.AppSettings["DPTO_COUNTER"].ToString();

            idGDS = int.Parse(ConfigurationManager.AppSettings["ID_GDS"].ToString());
            firmaBD = int.Parse(ConfigurationManager.AppSettings["FIRMA_BD"].ToString());
            dkPrueba = int.Parse(ConfigurationManager.AppSettings["DK_PRUEBA"].ToString());
            firmaGDS = int.Parse(ConfigurationManager.AppSettings["FIRMA_GDS"].ToString());
            codigoSeguimiento = ConfigurationManager.AppSettings["CodigoSeguimiento"].ToString() + "_" + Gds;
            correo_EmailNotificacion = ConfigurationManager.AppSettings["MAIL_NOTIFICACION"].ToString();
            
            contactosEnvioSMS = ConfigurationManager.AppSettings["NUMEROS_ENVIAR_SMS"].ToString().Split('/');

            HoraAvisoAnulacion_LV = ConfigurationManager.AppSettings["HORA_AVISO_ANULACION_LV"].ToString();
            HoraAnulacion_LV = ConfigurationManager.AppSettings["HORA_ANULACION_LV"].ToString();
            HoraAvisoNoFacturado_LV = ConfigurationManager.AppSettings["HORA_AVISO_NO_FACTURADO_LV"].ToString();
            HoraAvisoVoidEnDQBNoEnPTA_LV = ConfigurationManager.AppSettings["HORA_AVISO_BOLETOS_VOID_DQB_NO_EN_PT_LV"].ToString();
            HoraMaximaEvaluacion_LV = int.Parse(ConfigurationManager.AppSettings["HORA_MAX_EVALUA_EMISION_LV"].ToString());
            HoraAlmacenarBoletosAnulacion_LV = ConfigurationManager.AppSettings["HORA_ALMACENAR_BOLETOS_ANULACION"].ToString();
            

            HoraAvisoAnulacion_Sabado = ConfigurationManager.AppSettings["HORA_AVISO_ANULACION_SABADO"].ToString();
            HoraAnulacion_Sabado = ConfigurationManager.AppSettings["HORA_ANULACION_SABADO"].ToString();
            HoraAvisoNoFacturado_Sabado = ConfigurationManager.AppSettings["HORA_AVISO_NO_FACTURADO_SABADO"].ToString();
            HoraAvisoVoidEnDQBNoEnPTA_Sabado = ConfigurationManager.AppSettings["HORA_AVISO_BOLETOS_VOID_DQB_NO_EN_PT_SABADO"].ToString();
            HoraMaximaEvaluacion_Sabado = int.Parse(ConfigurationManager.AppSettings["HORA_MAX_EVALUA_EMISION_SABADO"].ToString());
            HoraAvisoNoFacturadoAyer = ConfigurationManager.AppSettings["HORA_AVISO_NO_FACTURADO_AYER"].ToString();
            HoraAlmacenarBoletosAnulacion_Sabado = ConfigurationManager.AppSettings["HORA_ALMACENAR_BOLETOS_ANULACION_SABADO"].ToString();
            
            correo_EmailCoordConsolidador = ConfigurationManager.AppSettings["MAIL_COORD_CONSOLIDADOR"].ToString();
            correo_EmailCounterTurno = ConfigurationManager.AppSettings["MAIL_COUNTER_TURNO"].ToString();
            correo_EmailSupervisorCounterIA = ConfigurationManager.AppSettings["MAIL_SUPERVISOR_COUNTER_IA"].ToString();
            correo_EmailPromotorAlternativo = ConfigurationManager.AppSettings["MAIL_PROMOTORES"].ToString();
            correo_EmailRobotAlertas = ConfigurationManager.AppSettings["MAIL_ROBOT_DE_ALERTAS"].ToString();
            correo_EmailPruebas = ConfigurationManager.AppSettings["MAIL_PRUEBA"].ToString(); 

            IsTest = ConfigurationManager.AppSettings["ES_PRUEBA"].ToString().Equals("1");

            if (Gds == GDS.SABRE)
            {
                pseudosPrivada = ConfigurationManager.AppSettings["PSEUDOS_PRIVADA"].ToString();
                pseudosPublicada = ConfigurationManager.AppSettings["PSEUDOS_PUBLICADA"].ToString();
                pseudosEmpresa = ConfigurationManager.AppSettings["PSEUDOS_EMPRESA"].ToString();
                var lpseudosNoVoid = ConfigurationManager.AppSettings["PSEUDOS_NO_VOID"].ToString();
                lpseudosNoVoid = lpseudosNoVoid != null ? lpseudosNoVoid : "";
                pseudosNoVoid = lpseudosNoVoid;

                var lpseudosUnificados = string.Format("{0}/{1}", pseudosPublicada, pseudosPrivada);
                if (!string.IsNullOrEmpty(pseudosNoVoid))
                {
                    lpseudosUnificados += "/" + pseudosNoVoid;
                }
                pseudosUnificados = lpseudosUnificados;
            }
            else
            {
                OficinasAmadeus = ConfigurationManager.AppSettings["OFICINAS_AMADEUS"].ToString();
                RutaFileBoletosAmadeus = ConfigurationManager.AppSettings["RUTA_FILE_BOLETOS_AMADEUS"].ToString();
            }

            RutaArchivoLog = ConfigurationManager.AppSettings["RUTA_LOG_ROBOT_ANULACIONES"].ToString();
            RutaArchivoLog = string.Format(RutaArchivoLog, Gds, empresa);

            RutaFileBoletosAmadeus = ConfigurationManager.AppSettings["RUTA_BOLETOS_EMITIDOS_LIMITE"].ToString();
            RutaFileBoletosAmadeus = string.Format(RutaFileBoletosAmadeus, empresa);

            correo_WebContacto = ConfigurationManager.AppSettings["WEB_CONTACTO"].ToString();
            correo_WebPortal = ConfigurationManager.AppSettings["WEB_PORTAL"].ToString();

        }

        public static class EsquemaDB
        {
            public static int NM { get; set; }
            public static int Agcorp { get; set; }
            public static int Destinos { get; set; }
            public static int Actual { get; set; }

            static EsquemaDB()
            {
                Actual   = int.Parse(ConfigurationManager.AppSettings["ESQUEMA"].ToString());
                NM       = int.Parse(ConfigurationManager.AppSettings["ESQUEMA_NM"].ToString());
                Destinos = int.Parse(ConfigurationManager.AppSettings["ESQUEMA_DM"].ToString());
                Agcorp   = int.Parse(ConfigurationManager.AppSettings["ESQUEMA_AGCORP"].ToString());
            }    
        }
        public static class Proveedor
        {
            static Proveedor() {
                proveedoresActual = ConfigurationManager.AppSettings["PROVEEDORES"].ToString().Split('/').ToList();
                proveedoresNM = ConfigurationManager.AppSettings["PROVEEDORES_NM"].ToString().Split('/').ToList();
                proveedoresAGCORP = ConfigurationManager.AppSettings["PROVEEDORES_AGCORP"].ToString().Split('/').ToList();
                proveedoresDM = ConfigurationManager.AppSettings["PROVEEDORES_DM"].ToString().Split('/').ToList();
            }
            public static List<string> proveedoresActual { get; set; }
            public static List<string> proveedoresNM { get; set; }
            public static List<string> proveedoresAGCORP { get; set; }
            public static List<string> proveedoresDM { get; set; }
        }

        public static Horario obtenerHorarioRobot()
        {
            var objHorario = new Horario();
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    objHorario.horaAvisoAnulacion = HoraAvisoAnulacion_Sabado;
                    objHorario.horaAnulacion = HoraAnulacion_Sabado;
                    objHorario.horaAvisoNoPTA_NoFacturado = HoraAvisoNoFacturado_Sabado;
                    objHorario.horaAvisoVoidDQBNoEnPTA = HoraAvisoVoidEnDQBNoEnPTA_Sabado;
                    objHorario.horaAvisoNoFacturadoAyer = HoraAvisoNoFacturadoAyer;
                    objHorario.horaMaximaEvaluacion = HoraMaximaEvaluacion_Sabado;
                    objHorario.horaAlmacenarBoletosAnulacion = HoraAlmacenarBoletosAnulacion_Sabado;
                    correo_HoraAnulacion = ConfigurationManager.AppSettings["MAIL_HORA_ANULACION_SABADO"].ToString();
                    correo_HoraLimitePago = ConfigurationManager.AppSettings["MAIL_HORA_MAX_PAGO_SABADO"].ToString();
                    break;
                default:
                    objHorario.horaAvisoAnulacion = HoraAvisoAnulacion_LV;
                    objHorario.horaAnulacion = HoraAnulacion_LV;
                    objHorario.horaAvisoNoPTA_NoFacturado = HoraAvisoNoFacturado_LV;
                    objHorario.horaAvisoVoidDQBNoEnPTA = HoraAvisoVoidEnDQBNoEnPTA_LV;
                    objHorario.horaAvisoNoFacturadoAyer = HoraAvisoNoFacturadoAyer;
                    objHorario.horaMaximaEvaluacion = HoraMaximaEvaluacion_LV;
                    objHorario.horaAlmacenarBoletosAnulacion = HoraAlmacenarBoletosAnulacion_LV;
                     correo_HoraAnulacion = ConfigurationManager.AppSettings["MAIL_HORA_ANULACION_LV"].ToString();
                    correo_HoraLimitePago = ConfigurationManager.AppSettings["MAIL_HORA_MAX_PAGO_LV"].ToString();
                    break;
            }
            return objHorario;
        }
    }
}
