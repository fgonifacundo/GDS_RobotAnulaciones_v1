using Oracle.DataAccess.Client;
using ROTOB_ANULACION.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Persistencia
{
    public interface BoletoDAO
    {
        List<BoletoRobotDTO> obtenerBoletosPTA(int esquema, string fecha, int idGds, string proveedores);

        List<VendedorDTO> obtenerVendedores(int esquema);
      
    }
}
