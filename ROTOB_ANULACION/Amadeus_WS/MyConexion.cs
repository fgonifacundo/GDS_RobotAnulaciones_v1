using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Amadeus_WS
{
    public class MyConexion
    {
        private string IP = "10.75.102.15";
        private string Puerto = "1521";
        private string usuario = "usr_amadeus";
        private string password = "6109409";

        public string cadenaConexion()
        {
            var lcadena = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));User Id={2};Password={3}";
            return string.Format(lcadena, IP, Puerto, usuario, password);
        }
    }
}
