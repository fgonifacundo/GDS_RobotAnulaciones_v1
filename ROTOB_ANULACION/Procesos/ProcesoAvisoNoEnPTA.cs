using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using classBO = GDS_NuevoMundoDominio.ObjetoDominioNegocio.classBO;

namespace ROTOB_ANULACION
{
    public class ProcesoAvisoNoEnPTA : IProcesoRobot
    {
        public void execute(GDS_NuevoMundoPersistencia.classSession session, List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletos, string fecha)
        {
            var logger = new GestorLog();
            logger.info(string.Format("Ejecutando ProcesoAvisoNoEnPTA [{0}] ", fecha));
            try
            {
                var boletosNoFiguranPTA = boletos.Where(obj => !obj.ExistePTA).ToList();
                logger.info(string.Format("Se encontraron {0} boletos No En PTA de ...", boletosNoFiguranPTA.Count));
                if (boletosNoFiguranPTA.Any())
                {
                    var service = new ServiceBoleto(TipoProceso.AVISO_NO_EN_PTA, fecha);
                    boletosNoFiguranPTA = service.filtrarBoletosNoFiguranEnOtroPTA(boletosNoFiguranPTA);
                    logger.info(string.Format("Se encontraron {0} boletos Que No están en Ningún PTA...", boletosNoFiguranPTA.Count));
                    boletosNoFiguranPTA.ForEach(obj => logger.info(string.Format("[{0} - {1} - {2} - {3}] :: No figura en PTA", obj.Pseudo, obj.PNR, obj.NumeroBoleto, obj.Vendedor.FirmaAgente)));
                    string asuntoMail = "Boletos no figuran en PTA - BACKOFFICE ";
                    var lstLog = new List<string> { };

                    var lProceso = Configuracion.Gds == GDS.SABRE ? "NBD" : "NBD_A";

                    var pseudosEmitenBoletos = boletosNoFiguranPTA.Select(x => x.Pseudo).Distinct().ToList();
                    foreach (var pseudo in pseudosEmitenBoletos)
                    {
                        var boletosPorPseudo = boletosNoFiguranPTA.Where(x => x.Pseudo.Equals(pseudo)).ToList();
                        new classBO().EjecutarProcesoAvisoVoideo(boletosPorPseudo, lProceso, asuntoMail, ref lstLog, Configuracion.codigoSeguimiento, Configuracion.idGDS, Configuracion.firmaGDS, Configuracion.firmaBD, Configuracion.EsquemaDB.Actual, session);
                        //new classBO().EjecutarProcesoAvisoVoideo(boletosNoFiguranPTA, "NBD", asuntoMail, ref lstLog, Configuracion.codigoSeguimiento, Configuracion.idGDS, Configuracion.firmaGDS, Configuracion.firmaBD, Configuracion.EsquemaDB.Actual, session);
                    }

                }
            }
            catch (Exception ex)
            {
                logger.info("Ocurrió un error al ejecutar el ProcesoAvisoNoEnPTA ");
                logger.info("Exception: " + ex.ToString());
                throw ex;
            }   
        }

        public void execute(GDS_NuevoMundoPersistencia.classSession session, List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletos)
        {
            throw new NotImplementedException();
        }
    }
}
