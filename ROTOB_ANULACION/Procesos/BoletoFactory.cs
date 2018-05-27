using ROTOB_ANULACION.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Procesos
{
    public abstract class BoletoFactory
    {
        protected DateUtils dateUtils = DateUtils.getInstance();
        protected FileUtils fileUtils = FileUtils.getInstance();
        protected string CONDICION_CONTADO = "CON";
        protected int TIPO_CLIENTE_AGENCIA = 1;
        protected int DK_PASAJERO_DIRECTO = 339;
        protected GestorLog logger = new GestorLog();

        public BoletoFactory()
        {
        }

        public abstract List<BoletoRobotDTO> ObtenerBoletosGDS();
        public abstract List<BoletoRobotDTO> ObtenerBoletosPTA(int esquema, List<string> proveedores);
        public abstract List<BoletoRobotDTO> UnificarBoletos();
        public abstract List<BoletoRobotDTO> ObtenerBoletosConsolidados(bool incluirVOIDenGDS);
        public abstract List<BoletoRobotDTO> ObtenerBoletosConDeudaPendiente();
        public abstract List<BoletoRobotDTO> ObtenerBoletosNoEncuentranEnPTA();
        public abstract List<BoletoRobotDTO> ObtenerBoletosNoFacturados();

        public abstract List<BoletoRobotDTO> ObtenerReporteDiarioGDS();
        public abstract bool AlmacenarBoletos();

        public static BoletoFactory getBoletoFactory(TipoProceso tipoProceso, string fecha)
        {
            switch (Configuracion.Gds)
            {
                //case GDS.SABRE:
                //    return new ServiceBoletoSabre();
                case GDS.AMADEUS:
                    return new ServiceBoletoAmadeus(tipoProceso, fecha);
                default:
                    return null;
            }
        }

    }
}
