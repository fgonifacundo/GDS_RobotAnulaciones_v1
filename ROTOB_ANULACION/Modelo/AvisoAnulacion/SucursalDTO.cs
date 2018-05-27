using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Modelo
{
    public sealed class SucursalDTO
    {
        public int? IdSucursal { get; set; }
        public string DescripcionSucursal { get; set; }
        public List<FileDTO> Files { get; set; }

        //SUCURSAL	FILE	COMPROBANTE	PNR	NÚMERO BOLETO	NOMBRE PASAJERO	PROMOTOR	DEUDA PENDIENTE	ESTADO
    }
}
