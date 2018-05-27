using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Modelo
{
    public class BoletoRobotDTO
    {
        public int IdSucursal { get; set; }
        public string DescripcionSucursal { get; set; }
        public string PrefijoBoleto { get; set; }
        public string NumeroBoleto { get; set; }
        public int IdCliente { get; set; }
        public int TipoDeCliente { get; set; }
        public string NombreCliente { get; set; }
        public string CorreoCliente { get; set; }
        public string CondicionPagoCliente { get; set; }
        public int IdPromotor { get; set; }
        public string IdVendedor { get; set; }
        public string NombrePromotor { get; set; }
        public string NombrePasajero { get; set; }
        public string CorreoPromotor { get; set; }
        public string CorreoJefePromotor { get; set; }
        public string Comprobante { get; set; }
        public string CorreoCaja { get; set; }
        public string NroFile { get; set; }
        public string IdFacturaCabeza { get; set; }
        public string NumeroSerie1 { get; set; }
        public string IdTipoComprobante { get; set; }
        public string PNR { get; set; }
        public int IdProveedor { get; set; }
        public string FechaAltaPTA { get; set; }
        public string FechaEmision { get; set; }
        public double MontoVenta { get; set; }
        public double MontoAplicado { get; set; }
        public double MontoPendiente { get; set; }
        public double MontoOtroDK { get; set; }
        public double TotalPendiente { get; set; }
        public bool EsAnuladoPTA { get; set; }
        public string Estado { get; set; }
        public bool EsFacturado { get; set; }
        public bool DebeAnular  { get; set; }
        public string FirmaAgente { get; set; }
        public string BoletoFull { get; set; }
        public string HoraEmision { get; set; }
        public string Pseudo { get; set; }
        public bool ExisteEnPTA { get; set; }
        public bool EsEmpresaGrupo { get; set; }
        public VendedorDTO Vendedor { get; set; }
        public bool AnulacionOkPorRobot { get; set; }
        public string MensajeError { get; set; }

    }
}
