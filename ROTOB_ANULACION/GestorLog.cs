using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using System.Text.RegularExpressions;
namespace ROTOB_ANULACION
{
    public class GestorLog
    {
        private FileUtils fileUtils = FileUtils.getInstance();
        
        public void crearDirectorioSiNoExiste(string strCarpeta) {
            if (!Directory.Exists(strCarpeta))
            {
                Directory.CreateDirectory(strCarpeta);
            }
        }

        public void info(string data)
        {
            string strCarpeta = Configuracion.RutaArchivoLog;
            string filePath = getRutaArchivoLog();
            crearDirectorioSiNoExiste(strCarpeta);
            fileUtils.write(filePath, string.Format("{0} :: {1} ", System.DateTime.Now.ToString("HH:mm:ss"), data));
        }

        public string getRutaArchivoLog() {
            var lfecha = System.DateTime.Now.ToString("yyyyMMdd");
            return Configuracion.RutaArchivoLog + lfecha + "_LOG_ROBOT_ANULACIONES.txt";
        }

     }
}

