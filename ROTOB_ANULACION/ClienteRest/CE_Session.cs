using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.ClienteRest
{
    public sealed class CE_Session
    {
        // =============================
        // auto propiedades

        #region "auto propiedades"

        public string ConversationId { set; get; }
        public string Token { set; get; }
        public string SignatureUser { set; get; }
        public string Pseudo { set; get; }

        #endregion
    }
}
