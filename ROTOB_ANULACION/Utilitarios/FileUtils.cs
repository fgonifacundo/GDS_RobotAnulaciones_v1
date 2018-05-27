using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION
{
    public class FileUtils
    {
        private static FileUtils instance = null;

        private FileUtils()
        {
        }

        public string getPath(string target)
        {
            return AppDomain.CurrentDomain.BaseDirectory + target;
        }

        public static FileUtils getInstance(){
            if (instance == null) {
                instance = new FileUtils();
            }
            return instance;
        }

        public string read(string filePath) {
            if (!File.Exists(filePath)) {
                return "";
            }
            var dataBuilder = new StringBuilder();
            using (var streamReader = new StreamReader(filePath)) {
                string line = "";
                while ((line = streamReader.ReadLine()) != null) {
                    dataBuilder.Append(line);
                }
            }
            return dataBuilder.ToString();
        }

        public bool write(string filePath, string target)
        {
            if (!string.IsNullOrEmpty(target)) {
                using (var oStreamWriter = new StreamWriter(filePath, true))
                {
                    oStreamWriter.WriteLine(target);
                    return true;
                }
            }
            return false;
        }

        public void crearDirectorioSiNoExiste(string strCarpeta)
        {
            if (!Directory.Exists(strCarpeta))
            {
                Directory.CreateDirectory(strCarpeta);
            }
        }
    }
}
