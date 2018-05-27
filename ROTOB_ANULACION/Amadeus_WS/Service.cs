using Oracle.DataAccess.Client;
using ROTOB_ANULACION.WebServiceAmadeus;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Amadeus_WS
{
    public class Service
    {
        AmadeusWebServices WSCliente;

        public void signOut()
        {
            var sec_SignOut = new Security_SignOut();
            try
            {
                var response = WSCliente.Security_SignOut(sec_SignOut);
                WSCliente = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error signOut: " + ex.Message.ToString());
            }
        }

        public void autenticarse(string OfficeId)
        {
            //var WSCliente = new AmadeusWebServices();
            if (WSCliente == null)
            {
                WSCliente = new AmadeusWebServices();
                var oALogin = new WSALogin();

                //Crea la sesión authentic
                var sec_auth = new Security_Authenticate();
                var sec_auth_reply = new Security_AuthenticateReply();

                var oSession = new Session();
                oSession.SecurityToken = "";
                oSession.SequenceNumber = "";
                oSession.SessionId = "";
                WSCliente.SessionValue = oSession;

                var oPasswordInfo = new Security_AuthenticatePasswordInfo[1];
                var oUserIdentifier = new Security_AuthenticateUserIdentifier[1];

                sec_auth.dutyCode = new Security_AuthenticateDutyCode();
                sec_auth.dutyCode.dutyCodeDetails = new Security_AuthenticateDutyCodeDutyCodeDetails();
                sec_auth.dutyCode.dutyCodeDetails.referenceIdentifier = oALogin.dutyCodeId;
                sec_auth.dutyCode.dutyCodeDetails.referenceQualifier = oALogin.dutyCodeQu;

                oPasswordInfo[0] = new Security_AuthenticatePasswordInfo();
                oPasswordInfo[0].dataLength = oALogin.pwdLen;
                oPasswordInfo[0].dataType = oALogin.pwdType;
                oPasswordInfo[0].binaryData = oALogin.pwd;
                sec_auth.passwordInfo = oPasswordInfo;

                sec_auth.systemDetails = new Security_AuthenticateSystemDetails();
                sec_auth.systemDetails.organizationDetails = new Security_AuthenticateSystemDetailsOrganizationDetails();
                sec_auth.systemDetails.organizationDetails.organizationId = oALogin.organization;

                oUserIdentifier[0] = new Security_AuthenticateUserIdentifier();
                oUserIdentifier[0].originator = oALogin.uorigin;
                oUserIdentifier[0].originatorTypeCode = oALogin.uoriginType;
                oUserIdentifier[0].originIdentification = new Security_AuthenticateUserIdentifierOriginIdentification();
                oUserIdentifier[0].originIdentification.sourceOffice = OfficeId;
                sec_auth.userIdentifier = oUserIdentifier;

                sec_auth.applicationId = new Security_AuthenticateApplicationId();
                sec_auth.applicationId.applicationDetails = new Security_AuthenticateApplicationIdApplicationDetails();
                sec_auth.applicationId.applicationDetails.internalId = oALogin.aplicationId;

                sec_auth_reply = WSCliente.Security_Authenticate(sec_auth);

                if (sec_auth_reply.errorSection != null)
                {
                    var mError = sec_auth_reply.errorSection.applicationError.errorDetails.errorCode;
                    Console.Write(mError);
                }

            }
        }

        public SalesReports_DisplayQueryReportReply obtenerReporte(string officeId, SalesReports_DisplayQueryReport RQ)
        {
            var reporte_RS = new SalesReports_DisplayQueryReportReply();
            autenticarse(officeId);
            reporte_RS = WSCliente.SalesReports_DisplayQueryReport(RQ);
            signOut();
            return reporte_RS;
        }


        public String obtenerIata(string officeId)
        {
            string iata = "";
            string esquema = obtenerEsquema(officeId);
            string sp = string.Format("{0}.PKG_GDS_WEBSERVICEPTA.GDS_BUSCAR_IATA", esquema);
            try
            {
                using (var connection = new OracleConnection(new MyConexion().cadenaConexion()))
                {
                    connection.Open();
                    var cmd = new OracleCommand(sp, connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    officeId = officeId.Equals("LIMPE32PN") ? officeId.Substring(5) : officeId;

                    cmd.Parameters.Add("p_Pseudo", OracleDbType.Varchar2, officeId.Length);
                    cmd.Parameters["p_Pseudo"].Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Pseudo"].Value = officeId;

                    cmd.Parameters.Add("p_Opcion", OracleDbType.Int32);
                    cmd.Parameters["p_Opcion"].Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Opcion"].Value = 0;

                    cmd.Parameters.Add("p_Cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    var m_Reader = cmd.ExecuteReader();
                    if (m_Reader.Read())
                    {
                        if (m_Reader["ID_IATA_PUNTO"] != DBNull.Value)
                        {
                            iata = m_Reader["ID_IATA_PUNTO"].ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message.ToString());
            }
            return iata;
        }

        public string obtenerEsquema(string officeId)
        {
            var schema = "";
            var defaultSchema = "NUEVOMUNDO";
            var schemas = new Dictionary<string, string>();
            schemas.Add("LIMPE31UC", "DESTINOS_TRP");
            schemas.Add("LIMPE212V", "DEMONUEVOMUNDO");
            schemas.Add("LIMPE32PN", "AGCORP");
            return schemas.TryGetValue(officeId, out schema) ? schema : defaultSchema;
        }
    }
}
