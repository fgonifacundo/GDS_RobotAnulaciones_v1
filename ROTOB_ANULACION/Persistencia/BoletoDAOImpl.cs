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
    public class BoletoDAOImpl : BoletoDAO
    {

        public List<VendedorDTO> obtenerVendedores(int esquema)
        {
            var vendedores = new List<VendedorDTO>();
            try
            {
                var myConexionOracle = new MyConexionOracle(Configuracion.Gds);
                using (var connection = myConexionOracle.getConexion())
                {
                    connection.Open();
                    var sp = string.Format("{0}.PKG_GDS_ROBOT_ANULACIONES.GDS_DATOS_VENDEDOR", myConexionOracle.getEsquema(esquema));
                    var cmd = new OracleCommand(sp, connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_Cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        vendedores.Add(new VendedorDTO
                        {
                            Id = getValueDataReader(reader, "ID_VENDEDOR", string.Empty),
                            Firma = getValueDataReader(reader, "ID_FIRMA", string.Empty),
                            Correo = getValueDataReader(reader, "CORREO_VENDEDOR", string.Empty),
                            CorreoJefe = getValueDataReader(reader, "CORREO_JEFE", string.Empty),
                            IdDepartamento = getValueDataReader(reader, "ID_DEPARTAMENTO", string.Empty),
                            Nombre = getValueDataReader(reader, "NOMBRE_VENDEDOR", string.Empty)
                        });
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return vendedores;
        }

        public List<BoletoRobotDTO> obtenerBoletosPTA(int esquema, string fecha, int idGds, string proveedores)
        {
            var boletosEnPTA = new List<BoletoRobotDTO>();
            try
            {
                var myConexionOracle = new MyConexionOracle(Configuracion.Gds);
                using (var connection = myConexionOracle.getConexion())
                {
                    connection.Open();
                    var sp = string.Format("{0}.PKG_GDS_ROBOT_ANULACIONES.GDS_BOLETO_EMITIDO", myConexionOracle.getEsquema(esquema));
                    var cmd = new OracleCommand(sp, connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("p_fecha", OracleDbType.Varchar2, fecha.Length);
                    cmd.Parameters["p_fecha"].Direction = ParameterDirection.Input;
                    cmd.Parameters["p_fecha"].Value = fecha;

                    cmd.Parameters.Add("p_GDS", OracleDbType.Int32);
                    cmd.Parameters["p_GDS"].Direction = ParameterDirection.Input;
                    cmd.Parameters["p_GDS"].Value = idGds;

                    cmd.Parameters.Add("p_Proveedores", OracleDbType.Varchar2);
                    cmd.Parameters["p_Proveedores"].Direction = ParameterDirection.Input;
                    cmd.Parameters["p_Proveedores"].Value = proveedores;

                    cmd.Parameters.Add("p_Cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        boletosEnPTA.Add(new BoletoRobotDTO
                        {
                            IdSucursal = int.Parse(getValueDataReader(reader, "ID_SUCURSAL", string.Empty)),
                            DescripcionSucursal = getValueDataReader(reader, "DESCRIPCION", string.Empty),
                            IdCliente = int.Parse(getValueDataReader(reader, "ID_CLIENTE", string.Empty)),
                            TipoDeCliente = int.Parse(getValueDataReader(reader, "ID_TIPO_DE_CLIENTE", "0")),
                            NombreCliente = getValueDataReader(reader, "NOMBRE", string.Empty),
                            CorreoCliente = getValueDataReader(reader, "EMAIL_AGENCIA", string.Empty),
                            CondicionPagoCliente = getValueDataReader(reader, "CONDICION_PAGO_AGENCIA", string.Empty),
                            IdPromotor = int.Parse(getValueDataReader(reader, "ID_PROMOTOR", "0")),
                            NombrePromotor = getValueDataReader(reader, "PROMO_NOMBRE", string.Empty),
                            CorreoPromotor = getValueDataReader(reader, "PROMO_EMAIL", string.Empty),

                            IdVendedor = getValueDataReader(reader, "ID_VENDEDOR", string.Empty),
                            CorreoCaja = getValueDataReader(reader, "EMAIL_CAJA", string.Empty),
                            NroFile = getValueDataReader(reader, "ID_FILE", "0"),

                            IdFacturaCabeza = getValueDataReader(reader, "ID_FACTURA_CABEZA", string.Empty),
                            NumeroSerie1 = getValueDataReader(reader, "NUMERO_SERIE1", string.Empty),
                            IdTipoComprobante = getValueDataReader(reader, "ID_TIPO_DE_COMPROBANTE", string.Empty),
                            PNR = getValueDataReader(reader, "COD_RESERVA", ""),
                            IdProveedor = int.Parse(getValueDataReader(reader, "ID_PROVEEDOR", string.Empty)),
                            FechaAltaPTA = getValueDataReader(reader, "FECHA_DE_ALTA", string.Empty),
                            FechaEmision = getValueDataReader(reader, "FECHA_EMISION", string.Empty),

                            PrefijoBoleto = getValueDataReader(reader, "ID_PREFIJO", string.Empty),
                            NumeroBoleto = getValueDataReader(reader, "NUMERO_DE_BOLETO", string.Empty),

                            EsAnuladoPTA = int.Parse(getValueDataReader(reader, "MARCA_VOID", "0")) == 1, // Si es 1 es void en PTA
                            EsFacturado = int.Parse(getValueDataReader(reader, "MARCA_FACTURADO", "0")) == 1, // Si es 1 es Facturado en PTA
                            EsEmpresaGrupo = int.Parse(getValueDataReader(reader, "ES_EMPRESA_DEL_GRUPO", "0")) == 1, // Si es 1 es empresa del grupo

                            DebeAnular = getValueDataReader(reader, "NO_ANULAR", "").Equals("VOIDEAR"),

                            MontoVenta = double.Parse(getValueDataReader(reader, "VENTA", "0")),
                            MontoAplicado = double.Parse(getValueDataReader(reader, "APLICADO", "0")),
                            MontoPendiente = double.Parse(getValueDataReader(reader, "PENDIENTE", "0")),
                            MontoOtroDK = double.Parse(getValueDataReader(reader, "PAGA_OTRO_DK", "0")),
                            TotalPendiente = double.Parse(getValueDataReader(reader, "VENTA", "0")) - double.Parse(getValueDataReader(reader, "PAGA_OTRO_DK", "0"))
                        });
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return boletosEnPTA;
        }


        private string getValueDataReader(OracleDataReader reader, string nombreColumna, string valorDefault)
        {
            string lrespuesta = string.Empty;
            try
            {
                lrespuesta = reader[nombreColumna] != DBNull.Value ? reader[nombreColumna].ToString().Trim() : valorDefault.Trim();
            }
            catch (Exception e) 
            {
                lrespuesta = e.ToString();
            }
            return lrespuesta;
        }


    }
}
