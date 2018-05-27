using ROTOB_ANULACION.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using classBO = GDS_NuevoMundoDominio.ObjetoDominioNegocio.classBO;

namespace ROTOB_ANULACION
{
    public class ProcesoAvisoAnulacion: IProcesoRobot
    {
        public void execute(GDS_NuevoMundoPersistencia.classSession session, List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletos, string fecha)
        {
            throw new NotImplementedException();
        }

        public void execute(GDS_NuevoMundoPersistencia.classSession session, List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletos)
        {
            var logger = new GestorLog();
            const double MONTO_MAXIMO_DEUDA_PENDIENTE = 20;
            try
            {
                var lstLog = new List<string> { };

                // Crea una instancia de serviceBoleto para obtener los boletos con deuda pendiente
                var service = new ServiceBoleto(TipoProceso.AVISO_ANULACION, null);

                // Obtiene los boletos con deuda pendiente
                boletos = service.obtenerBoletosDeudaPendiente(boletos);

                // Si existen boletos con deuda pendiente
                if (boletos != null && boletos.Any()) {
                    // Envía aviso solo Si los boletos tienen una deuda pendiente mayor al máximo de deuda indicado por la empresa (en este caso 20.00) o el DK es de prueba 
                    var boletosAvisoAnulacion = boletos.Where(boleto => boleto.TotalPendiente >= MONTO_MAXIMO_DEUDA_PENDIENTE || boleto.IdCliente == Configuracion.dkPrueba).ToList();
                    if (boletosAvisoAnulacion.Any())
                    {
                        logger.info(string.Format("Se encontraron {0} boletos a enviar Aviso de anulación...", boletosAvisoAnulacion.Count));
                        boletosAvisoAnulacion.ForEach(obj => logger.info(string.Format("[{0} - {1} - {2} - {3}] :: [ Aviso Anulacion ]  TotalPendiente: {5} | DK: {6}", obj.Pseudo, obj.PNR, obj.NumeroBoleto, obj.Vendedor.FirmaAgente, obj.MarcaFacturado, obj.TotalPendiente, obj.Cliente.DK)));
                        
                        // Si es AGCORP envía un asunto distinto, ya que por el momento no se está realizando el proceso de voideo a sus clientes
                        var asuntoMailAviso = "NM".Equals(Configuracion.empresa) ? "Boletos sin pago sera anulado" : "Boletos pendientes de pago ";

                        var valor = Configuracion.Gds == GDS.SABRE ? "AVI" : "AVI_A";
                        
                        // Ejecutar el proceso de envio de correos
                         new classBO().EjecutarProcesoAvisoVoideo(boletosAvisoAnulacion, valor, asuntoMailAviso, ref lstLog, Configuracion.codigoSeguimiento, Configuracion.idGDS, Configuracion.firmaGDS, Configuracion.firmaBD, Configuracion.EsquemaDB.Actual, session);
                    }

                    if ("NM".Equals(Configuracion.empresa)) // Solo para NM
                    {
                        // Si tienen una deuda pendiente menor que 20 se envía un correo solo avisando que debe cancelar la deuda pero no se anulará el boleto
                        var boletosConPendienteMinimo = boletos.Where(boleto => boleto.TotalPendiente < MONTO_MAXIMO_DEUDA_PENDIENTE).ToList();
                        if (boletosConPendienteMinimo.Any())
                        {
                            logger.info(string.Format("Se encontraron {0} boletos con deudar menor a {1} ...", boletosConPendienteMinimo.Count, MONTO_MAXIMO_DEUDA_PENDIENTE));
                            boletosConPendienteMinimo.ForEach(obj => logger.info(string.Format("[{0} - {1} - {2} - {3}] :: [ Aviso Anulacion - Pendiente menor de {7} ]  TotalPendiente: {5} | DK: {6}", obj.Pseudo, obj.PNR, obj.NumeroBoleto, obj.Vendedor.FirmaAgente, obj.MarcaFacturado, obj.TotalPendiente, obj.Cliente.DK, MONTO_MAXIMO_DEUDA_PENDIENTE)));
                            var asunto = "Boletos pendientes de Pago ";
                            new classBO().EvaluarBoletosXCliente(boletosConPendienteMinimo, "AVIC", asunto, ref lstLog, Configuracion.codigoSeguimiento, Configuracion.idGDS, Configuracion.firmaGDS, Configuracion.firmaBD, Configuracion.EsquemaDB.Actual, session);
                        }
                    }
                }
                MailUtils.getInstance().sendMailConfirmacion(TipoProceso.AVISO_ANULACION);
                
            }
            catch (Exception ex)
            {
                logger.info("Ocurrió un error al ejecutar el ProcesoAvisoAnulacion ");
                logger.info("Exception: " + ex.ToString());
                throw ex;
            }
        }


    


    }
}
