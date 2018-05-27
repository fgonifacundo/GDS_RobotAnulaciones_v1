using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION
{
    public class DateUtils
    {

        private static DateUtils instance = null;
        private DateUtils()
        {
        }

        public static DateUtils getInstance() {
            if (instance == null)
            {
                instance = new DateUtils();
            }
            return instance;
        }

        const string FORMATO_HORA_24 = "HH:mm:ss";

        public string obtenerHoraFormat24()
        {
            return DateTime.Now.ToString(FORMATO_HORA_24, CultureInfo.CurrentCulture);
        }

        public string obtenerFechaHoy() {
            return DateTime.Now.ToShortDateString();
        }
    }
}
