using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Modelo
{
    public sealed class FileDTO
    {
        #region "auto propiedades"
        public string IdFile { get; set; }
        public List<ReservaDTO> Reservas { get; set; }
        public List<ComprobanteDTO> Comprobantes { get; set; }
        public ClienteDTO Cliente { get; set; }

        #endregion "auto propiedades"

        //SUCURSAL	FILE	COMPROBANTE	PNR	NÚMERO BOLETO	NOMBRE PASAJERO	PROMOTOR	DEUDA PENDIENTE	ESTADO

    }
}
