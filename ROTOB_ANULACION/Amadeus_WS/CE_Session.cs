using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Amadeus_WS
{
    [Serializable]
    public class CE_Session
    {
        public string strConversationID { get; set; } //AMADEUS:SessionId
        public string strToken { get; set; }
        public string secuenciaId { get; set; }
        public CE_Session()
        {
        }
        public CE_Session(string strConversationID, string strToken, string secuenciaId = "0")
        {
            this.strConversationID = strConversationID;
            this.strToken = strToken;
            this.secuenciaId = secuenciaId;
        }


    }
}
