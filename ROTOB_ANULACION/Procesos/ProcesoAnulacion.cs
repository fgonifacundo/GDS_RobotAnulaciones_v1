using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using classBO = GDS_NuevoMundoDominio.ObjetoDominioNegocio.classBO;

namespace ROTOB_ANULACION
{
    public class ProcesoAnulacion: IProcesoRobot
    {
        public void execute(GDS_NuevoMundoPersistencia.classSession session, List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletos)
        {
            var logger = new GestorLog();
            logger.info("Ejecutando ProcesoAvisoAnulacion...");
            try
            {
                double MONTO_MAXIMO_DEUDA_PENDIENTE = 20;
                // Crear instancia de service boleto para obtener boletos con deuda pendiente
                var service = new ServiceBoleto(TipoProceso.ANULACION, null);

                // Filtrar boletos con deuda Pendiente
                boletos = service.obtenerBoletosDeudaPendiente(boletos);
                
                // Si existen boletos con deuda pendiente.
                if (boletos != null && boletos.Any()) {
                
                    // Anular solo Si los boletos tienen una deuda pendiente mayor al máximo de deuda indicado por la empresa (en este caso 20.00) o el DK es de prueba 
                    var boletosAAnular = boletos.Where(boleto => boleto.TotalPendiente >= MONTO_MAXIMO_DEUDA_PENDIENTE || boleto.IdCliente == Configuracion.dkPrueba).ToList();
                    if (boletosAAnular.Any())
                    {
                        logger.info(string.Format("Se encontraron {0} boletos a Anular...", boletosAAnular.Count));
                        
                        // Grabar log de los boletos que va a anular el robot
                        boletosAAnular.ForEach(obj => logger.info(string.Format("[{0} - {1} - {2} - {3}] :: [ Anulacion ]  TotalPendiente: {5} | DK: {6}", obj.Pseudo, obj.PNR, obj.NumeroBoleto, obj.Vendedor.FirmaAgente, obj.MarcaFacturado, obj.TotalPendiente, obj.Cliente.DK)));
                        var asunto = "Boletos anulados";
                        var lstLog = new List<string> { };

                        // Ejecutar el proceso de anulación
                       new classBO().EjecutarProcesoAvisoVoideo(boletosAAnular, "VOI", asunto, ref lstLog, Configuracion.codigoSeguimiento, Configuracion.idGDS, Configuracion.firmaGDS, Configuracion.firmaBD, Configuracion.EsquemaDB.Actual, session);
                        
                        // Grabar log de las respuestas de los comandos de sabre.
                        lstLog.ForEach(data => logger.info(data));
                    }
                }
                MailUtils.getInstance().sendMailConfirmacion(TipoProceso.ANULACION);
            }
            catch (Exception e)
            {
                logger.info("Ocurrió un error en el proceso de Anulacion");
                throw e;
            }
        }


        public void validarBoletosAnulados() { 
            
        }

          public void execute(GDS_NuevoMundoPersistencia.classSession session, List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletos, string fecha)
          {
              throw new NotImplementedException();
          }
    }
}
