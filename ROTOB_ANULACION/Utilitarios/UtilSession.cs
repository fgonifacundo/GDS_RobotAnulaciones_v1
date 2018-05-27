using GDS_NuevoMundoPersistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ROTOB_ANULACION
{
   public class UtilSession
    {

       private JavaScriptSerializer jsonSerialize;
       
       public UtilSession()
       {
           jsonSerialize = new JavaScriptSerializer();
       }

        public classSession getSession()
        {
            return jsonSerialize.Deserialize<classSession>(new GNM_ConsultaPTA.gnm().SWS_CrearSessionJSON(Configuracion.codigoSeguimiento, Configuracion.idGDS, Configuracion.firmaGDS, Configuracion.firmaBD));
        }

        public void closeSession(classSession session)
        {
            if (session != null)
            {
                new GNM_ConsultaPTA.gnm().SWS_SessionClose(session.Token, session.ConversationID, Configuracion.codigoSeguimiento, Configuracion.idGDS, Configuracion.firmaGDS, Configuracion.firmaBD);
            }
        }

    }
}
