using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Amadeus_WS
{
    public class WSALogin
    {
        private string strPwd = "RG1rSm1Ld1Q=";
        private Decimal dcmPwdLen = 8;
        private string strPwdType = "E";
        private string strDutyCodeId = "SU";
        private string strDutyCodeQu = "DUT";
        private string strOrganization = "LATAM";
        private string strUorigin = "WSNUETEN";
        private string strUoriginType = "U";
        private string strSourceOffice = "LIMPE2390";
        private string strAplicationId = "1ASIWTENNUE";

        public string pwd
        {
            get { return strPwd; }
            set { strPwd = value; }
        }

        public Decimal pwdLen
        {
            get { return dcmPwdLen; }
            set { dcmPwdLen = value; }
        }

        public string pwdType
        {
            get { return strPwdType; }
            set { strPwdType = value; }
        }

        public string dutyCodeId
        {
            get { return strDutyCodeId; }
            set { strDutyCodeId = value; }
        }

        public string dutyCodeQu
        {
            get { return strDutyCodeQu; }
            set { strDutyCodeQu = value; }
        }

        public string organization
        {
            get { return strOrganization; }
            set { strOrganization = value; }
        }

        public string uorigin
        {
            get { return strUorigin; }
            set { strUorigin = value; }
        }

        public string uoriginType
        {
            get { return strUoriginType; }
            set { strUoriginType = value; }
        }

        public string sourceOffice
        {
            get { return strSourceOffice; }
            set { strSourceOffice = value; }
        }

        public string aplicationId
        {
            get { return strAplicationId; }
            set { strAplicationId = value; }
        }
    }
}
