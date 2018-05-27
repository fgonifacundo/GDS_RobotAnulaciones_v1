using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Persistencia
{
    public class MyConexionOracle
    {
        private GDS gds;

        public MyConexionOracle(GDS gds) {
            this.gds = gds;
        }

        public OracleConnection getConexion() {
            return new OracleConnection(getCadenaConexion());
        }

        private string getCadenaConexion() {
            switch (gds) { 
                case GDS.SABRE:
                    return "Data Source=tn_pta;User ID=usr_turbo;Password=usr_turbo";
                case GDS.AMADEUS:
                    return "Data Source=tn_pta;User ID=usr_emisiones;Password=s3rv3r";
                default:
                    return null;
            }
        }

        public string getEsquema(int idEsquema) {
            switch (idEsquema) { 
                case 5:
                    return "NUEVOMUNDO";
                case 6:
                    return "DESTINOS_TRP";
                case 7:
                    return "AGCORP";
                default:
                    return "NUEVOMUNDO";
            }
        }

    }
}
