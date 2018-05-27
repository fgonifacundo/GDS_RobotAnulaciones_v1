using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using GDS_NuevoMundoPersistencia;
using Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes;
using GDS_NuevoMundoDominio;
using classBO = GDS_NuevoMundoDominio.ObjetoDominioNegocio.classBO;
using GDS_MuevoMundoLog;

using GDS_NM_Mensajeria;
using System.Globalization;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using System.Threading;
using System.Text.RegularExpressions;
using ROTOB_ANULACION.WS_ProcesosGDS;
using NuevoMundoSecurity;
using ROTOB_ANULACION.Modelo;
namespace ROTOB_ANULACION.Procesos
{
    public class Main
    {
        private GestorLog logger;
        private DateUtils dateUtils = DateUtils.getInstance();
        private FileUtils fileUtils = FileUtils.getInstance();

        public Main()
        {
            this.logger = new GestorLog();
        }

        public void procesar(TipoProceso tipoProceso)
        {
            logger.info(string.Format("Ejecutando proceso: {0} ", tipoProceso));
            var utilSession = new UtilSession();
            classSession session = null;
            string cuerpoSMS = "";
            try
            {
                var fecha = getFechaPorTipoProceso(tipoProceso);

                // Crea una instancia de Service Boleto con la configuración según el proceso
                var service = new ServiceBoleto(tipoProceso, fecha);

                // Obtener todos los boletos Emitidos en el DQB
                var boletosEmitidosDQB = service.ObtenerListaBoletosDQB(tipoProceso);

                // Filtra los boletos que no se encuentren VOID
                var boletosAProcesar = boletosEmitidosDQB.Where(boleto => !boleto.Estado.Equals("VOID")).ToList();

                // Obtener todos los boletos Registrados en el PTA
                var boletosRegistradosPTA = service.obtenerBoletoRegistradosPTA(Configuracion.EsquemaDB.Actual);

                // Consolida los boletos en DQB Activos con los boletos en PTA
                var boletosConsolidados = service.consolidarBoletos(boletosAProcesar, boletosRegistradosPTA);

                session = utilSession.getSession();

                // Crea una instancia del Gestor e inyectar session
                GestorProceso gestorProceso = new GestorProceso(session);

                if (boletosConsolidados.Any())
                {
                    if (tipoProceso == TipoProceso.AVISO_NO_FACTURADOS || tipoProceso == TipoProceso.AVISO_NO_FACTURADOS_AYER)
                    {
                        gestorProceso.ejecutarProceso(new ProcesoAvisoNoFacturado(), boletosConsolidados, fecha);
                        gestorProceso.ejecutarProceso(new ProcesoAvisoNoEnPTA(), boletosConsolidados, fecha);
                    }
                    else if (tipoProceso == TipoProceso.AVISO_ANULACION)
                    {
                        gestorProceso.ejecutarProceso(new ProcesoAvisoAnulacion(), boletosConsolidados);
                    }
                    else if (tipoProceso == TipoProceso.ANULACION && "NM".Equals(Configuracion.empresa))
                    {
                        gestorProceso.ejecutarProceso(new ProcesoAnulacion(), boletosConsolidados);
                    }
                    else if (tipoProceso == TipoProceso.AVISO_VOID_DQB_NO_EN_PTA)
                    {
                        boletosAProcesar = boletosEmitidosDQB.Where(boleto => boleto.Estado.Equals("VOID")).ToList();
                        if (boletosAProcesar.Any())
                        {

                        }
                    }
                }
                cuerpoSMS = string.Format("{0} - {1} - Se ejecuto correctamente el proceso {2} del Robot de Anulaciones", Configuracion.Gds, Configuracion.empresa, tipoProceso);
            }
            catch (Exception ex)
            {
                cuerpoSMS = string.Format("{0} - {1} - Ocurrió un error en el proceso {2} del Robot de Anulaciones, Por favor revisar su mail para más detalles.", Configuracion.Gds, Configuracion.empresa, tipoProceso);
                MailUtils.getInstance().sendMailError(ex, tipoProceso);
                logger.info(string.Format("Ocurrió un error inesperado: {0}", ex.ToString()));
            }
            finally
            {
                utilSession.closeSession(session);
                if (tipoProceso == TipoProceso.AVISO_ANULACION || tipoProceso == TipoProceso.ANULACION)
                {
                    new Utilitario().envioSMS(cuerpoSMS, "ROBOT_AVISO_SABRE", Configuracion.contactosEnvioSMS);
                }
            }
        }




        /// <summary>
        /// Retorna la fecha dependiendo del tipo de Proceso
        /// </summary>
        /// <param name="proceso">Proceso a Ejecutar</param>
        /// <returns>Si el proceso es no Facturado Ayer retorna la fecha de Ayer, caso contrario retorna la fecha del sistema</returns>
        private string getFechaPorTipoProceso(TipoProceso tipoProceso)
        {
            return (tipoProceso == TipoProceso.AVISO_NO_FACTURADOS_AYER) ? DateTime.Today.AddDays(-1).ToShortDateString() : DateTime.Now.ToShortDateString();
        }
    }
}
