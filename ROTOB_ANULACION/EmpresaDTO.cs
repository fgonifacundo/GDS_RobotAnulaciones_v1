using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION
{
    public class EmpresaDTO
    {

        public Horario horarioRobot { get; set; }
        public string pseudosPublicada { get; set; }
        public string pseudosPrivada { get; set; }
        public int esquema { get; set; }
        public List<int> sucursales { get; set; }
        public List<string> proveedores { get; set; }
        public int dkPrueba { get; set; }
        public string mailRobot { get; set; }
        public int firmaGDS { get; set; }
        public int idGDS { get; set; }
        public string empresa { get; set; }
        public string firmaRobot { get; set; }
        public string dptoCounter { get; set; }
        public int firmaBD { get; set; }

        public override string ToString()
        {
            return "EmpresaDTO =>  " + empresa + " || Firma Robot: " + firmaRobot + "|| FirmaGDS: " + firmaGDS + "|| IdGDS: " + idGDS + " || Esquema: " + esquema + " || PseudosPublicada: " + pseudosPublicada + " || PseudosPrivada: " + pseudosPrivada + " || DK Prueba: " + dkPrueba + " || Sucursales: " + String.Join("/", sucursales.ToArray()) + " || Horario Robot => Aviso: " + horarioRobot.horaAvisoAnulacion + " || Voideo: " + horarioRobot.horaAnulacion;
        }
    }
}
