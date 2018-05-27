using ROTOB_ANULACION.Modelo;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using classBO = GDS_NuevoMundoDominio.ObjetoDominioNegocio.classBO;

namespace ROTOB_ANULACION
{
    public class ProcesoAvisoNoFacturado: IProcesoRobot
    {
       public void execute(GDS_NuevoMundoPersistencia.classSession session, List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletos, string fecha)
       {
           var logger = new GestorLog();
           logger.info(string.Format("Ejecutando ProcesoAvisoNoFacturados [{0}] ", fecha));
           try
           {
               var boletosNoFacturados = boletos.Where(boleto => boleto.ExistePTA && boleto.MarcaFacturado == 0).ToList();
               logger.info(string.Format("Se encontraron {0} boletos No Facturados...", boletosNoFacturados.Count));
               if (boletosNoFacturados.Any())
               {
                   boletosNoFacturados.ForEach(boleto => logger.info(string.Format("[{0} - {1} - {2} - {3}] :: No Facturado ", boleto.Pseudo, boleto.PNR, boleto.NumeroBoleto, boleto.Vendedor.FirmaAgente, boleto.MarcaFacturado)));
                   string asuntoMail = "Boletos No Facturados [ " + fecha + " ]";
                   var lstLog = new List<string>();
                   var lproceso = getReferenciaProceso(fecha);
                   var lPseudosEmisores = boletosNoFacturados.Select(x => x.Pseudo).Distinct().ToList();
                   foreach (var pseudo in lPseudosEmisores)
                   {
                        var boletosPorPseudo = boletosNoFacturados.Where(x => x.Pseudo.Equals(pseudo)).ToList();
                        new classBO().EjecutarProcesoAvisoVoideo(boletosPorPseudo, lproceso, asuntoMail, ref lstLog, Configuracion.codigoSeguimiento, Configuracion.idGDS, Configuracion.firmaGDS, Configuracion.firmaBD, Configuracion.EsquemaDB.Actual, session);
                   }
               }
           }
           catch (Exception ex)
           {
               logger.info("Ocurrió un error al ejecutar el ProcesoAvisoNoFacturado ");
               logger.info("Exception: " + ex.ToString());
               throw ex;
           }
       }

       private string getReferenciaProceso(string fecha) {
           var fechaActual = DateTime.Today.ToShortDateString();
           var lproceso = string.Empty;
           if (Configuracion.Gds == GDS.AMADEUS)
           {
               lproceso = fecha.Equals(fechaActual) ? "NFA_A" : "NFA_AYER_A";
           }
           else
           {
               lproceso = fecha.Equals(fechaActual) ? "NFA" : "NFA_AYER";
           }
           return lproceso;
       }

        /*
       public void execute(ConfiguracionRobotDTO configuracion, List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletos, string fecha)
       {
           logger.info(string.Format("Ejecutando ProcesoAvisoNoFacturado... {0} ", fecha));
           var utilSession = new UtilSession(configuracion);
           GDS_NuevoMundoPersistencia.classSession session = null;
           try
           {
               var boletosNoFacturados = boletos.Where(boleto => boleto.ExistePTA && boleto.MarcaFacturado == 0).ToList();
               logger.info(string.Format("Se encontraron {0} boletos No Facturados...", boletosNoFacturados.Count));
               if (boletosNoFacturados.Any())
               {
                   // Probar con los boletos emitidos por mis iniciales
                   // boletosNoFacturados = boletosNoFacturados.Where(boleto => boleto.Vendedor.FirmaAgente.Contains("N1")).ToList();
                   boletosNoFacturados.ForEach(boleto => logger.info(string.Format("[{0} - {1} - {2} - {3}] :: No Facturado ", boleto.Pseudo, boleto.PNR, boleto.NumeroBoleto, boleto.Vendedor.FirmaAgente, boleto.MarcaFacturado)));
                   session = utilSession.getSession();
                   var fechaActual = DateTime.Today.ToShortDateString();
                   string asuntoMail = "Boletos No Facturados [ " + fecha + " ]";
                   var proceso = fecha.Equals(fechaActual) ? "NFA" : "NFA_AYER";
                   var lstLog = new List<string>();
                   new classBO().EjecutarProcesoAvisoVoideo(boletosNoFacturados, proceso, asuntoMail, ref lstLog, configuracion.codigoSeguimiento, configuracion.idGDS, configuracion.firmaGDS, configuracion.firmaBD, configuracion.esquemaDB.Actual, session);
               }
           }
           catch (Exception ex)
           {
               logger.info("Ocurrió un error al ejecutar el ProcesoAvisoNoFacturado ");
               logger.info("Exception: " + ex.ToString());
           }
           finally
           {
               utilSession.closeSession(session);
           }
       }
        */

       public void execute(GDS_NuevoMundoPersistencia.classSession session, List<GDS_NuevoMundoPersistencia.robotBoletoPendientePago.robotBoletoPendiente> boletos)
       {
           throw new NotImplementedException();
       }


     
    }
}
