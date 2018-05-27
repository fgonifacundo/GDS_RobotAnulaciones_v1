using ROTOB_ANULACION.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Reportes
{
    public abstract class ReporteFactory
    {

        protected FileUtils fileUtils = FileUtils.getInstance();

        protected List<BoletoRobotDTO> boletosProcesar;

        public abstract string ConstruirTablaReporte();

        public abstract string ConstruirCorreo(string encabezado, string mensaje, string cuerpo);

        public abstract string ConstruirCorreo(string encabezado, string mensaje);

        public abstract List<PseudoDTO> AgruparBoletos();

        public static ReporteFactory getReporteFactory(TipoProceso tipoProceso, List<BoletoRobotDTO> boletosProcesar)
        {
            switch (tipoProceso)
            {
                case TipoProceso.AVISO_ANULACION:
                case TipoProceso.ANULACION:
                    return new ReporteAvisoAnulacion(boletosProcesar);
                
                case TipoProceso.AVISO_NO_EN_PTA:
                    return new ReporteNoEnPTA(boletosProcesar);

                case TipoProceso.AVISO_NO_FACTURADOS:
                    return new ReporteNoFacturado(boletosProcesar);

                default:
                    return null;

            }
        }
    }
}
