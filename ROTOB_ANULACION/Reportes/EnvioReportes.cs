using ROTOB_ANULACION.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ROTOB_ANULACION.Reportes
{
    public class EnvioReportes
    {
        private GestorLog logger = new GestorLog();
        private TipoProceso tipoProceso;
        private List<BoletoRobotDTO> boletos;
        private StringBuilder mensaje = null;
        private string asunto = string.Empty;
        private string display = string.Empty;

        public EnvioReportes(TipoProceso tipoProceso, List<BoletoRobotDTO> lboletos)
        {
            this.tipoProceso = tipoProceso;
            this.boletos = lboletos;
        }

        public bool procesar()
        {
            var lrespuesta = false;
            switch (tipoProceso)
            {
                case TipoProceso.AVISO_ANULACION:
                    ProcesoEnvioAvisoAnulacion();
                    break;
                case TipoProceso.ANULACION:
                    ProcesoEnvioAnulacion();
                    break;
                case TipoProceso.AVISO_NO_EN_PTA:
                    ProcesoEnvioNoEnPTA();
                    break;
                case TipoProceso.AVISO_NO_FACTURADOS:
                    ProcesoEnvioNoFacturados();
                    break;
            }
            return lrespuesta;
        }

        #region ProcesoEnvioNoEnPTA
        public void ProcesoEnvioNoEnPTA()
        {
            var lvendedores = boletos.GroupBy(p => new { p.Vendedor.Id }).Select(s => s.Key.Id).ToList();
            lvendedores.ForEach(lvendedor =>
            {
                var lboletosPorVendedor = boletos.Where(boleto => boleto.Vendedor.Id.Equals(lvendedor)).ToList();

                var lpseudos = lboletosPorVendedor.GroupBy(p => new { p.Pseudo }).Select(s => s.Key.Pseudo).ToList();
                lpseudos.ForEach(lpseudo =>
                {
                    var lboletosPorPseudo = lboletosPorVendedor.Where(boleto => boleto.Pseudo.Equals(lpseudo)).ToList();

                    var lencabezado = string.Format("Estimado(a): {0}, ", lboletosPorPseudo[0].Vendedor.Nombre);

                    mensaje = new StringBuilder();
                    mensaje.Append("Se hace de tu conocimiento que los siguientes boletos emitidos no se encuentran en PTA - BACKOFFICE. ");

                    var factoryReporte = ReporteFactory.getReporteFactory(tipoProceso, lboletosPorPseudo);
                    var lcorreoBody = factoryReporte.ConstruirCorreo(lencabezado, mensaje.ToString());

                    var lcorreoDisplay = string.Format("Alertas GDS [{0}]", lboletosPorPseudo[0].Pseudo);
                    var lcorreoFrom = string.Format("{0}.alertas@expertiatravel.com", lboletosPorPseudo[0].Pseudo);

                    var lscorreo = new GestorCorreo
                    {
                        EmailFrom = lcorreoFrom,
                        EmailTo = lboletosPorPseudo[0].Vendedor.Correo,
                        EmailCC = lboletosPorPseudo[0].Vendedor.CorreoJefe,
                        EmailDisplay = lcorreoDisplay,
                        EmailBody = lcorreoBody,
                        EmailSubject = "Boletos no figuran en PTA - BACKOFFICE"
                    };
                    lscorreo.Enviar();

                });
            });
        }
        #endregion

        #region ProcesoEnvioNoFacturados
        public void ProcesoEnvioNoFacturados()
        {
            var lvendedores = boletos.GroupBy(p => new { p.IdVendedor }).Select(s => s.Key.IdVendedor).ToList();
            lvendedores.ForEach(lvendedor =>
            {
                var lboletosPorVendedor = boletos.Where(boleto => boleto.IdVendedor.Equals(lvendedor)).ToList();

                var lpseudos = lboletosPorVendedor.GroupBy(p => new { p.Pseudo }).Select(s => s.Key.Pseudo).ToList();
                lpseudos.ForEach(lpseudo =>
                {
                    var lboletosPorPseudo = lboletosPorVendedor.Where(boleto => boleto.Pseudo.Equals(lpseudo)).ToList();

                    var lencabezado = string.Format("Estimado(a): {0}, ", lboletosPorPseudo[0].Vendedor.Nombre);

                    mensaje = new StringBuilder();
                    mensaje.Append("Se hace de tu conocimiento que los siguientes boletos emitidos no están facturados.");

                    var factoryReporte = ReporteFactory.getReporteFactory(tipoProceso, lboletosPorPseudo);
                    var lcorreoBody = factoryReporte.ConstruirCorreo(lencabezado, mensaje.ToString());

                    var lcorreoDisplay = string.Format("Alertas GDS [{0}]", lboletosPorPseudo[0].Pseudo);
                    var lcorreoFrom = string.Format("{0}.alertas@expertiatravel.com", lboletosPorPseudo[0].Pseudo);


                    var lscorreo = new GestorCorreo
                    {
                        EmailFrom = lcorreoFrom,
                        EmailTo = lboletosPorPseudo[0].Vendedor.Correo,
                        EmailCC = lboletosPorPseudo[0].Vendedor.CorreoJefe,
                        EmailDisplay = lcorreoDisplay,
                        EmailBody = lcorreoBody,
                        EmailSubject = "Boletos No Facturados"
                    };
                    lscorreo.Enviar();

                });
            });
        }
        #endregion

        #region ProcesoEnvioAnuladosCorrectamente
        public void ProcesoEnvioAnuladosCorrectamente(List<BoletoRobotDTO> boletosAnulados)
        {
            mensaje = new StringBuilder();
            mensaje.Append(string.Format("El Robot de anulaciones {0} anuló los siguientes boletos que tiene ", Configuracion.Gds));
            mensaje.Append("condición de pago CONTADO y el boleto NO FUE PAGADO A LA HORA INDICADA. Por favor, tomar en cuenta esta acción. ");
            mensaje.Append("Si pagaste antes de la hora de anulacion indicada, por favor, contacta con tu asesor comercial en el correo aquí copiado o búscalo en: ");
            mensaje.Append(Configuracion.correo_WebContacto);
            display = string.Format("{0} - Robot Anulaciones Avisos  - Counter", Configuracion.Gds);
            asunto = "Boletos anulados, Counter: {0}";
            ProcesarEnvioAVendedores(boletosAnulados, mensaje.ToString(), asunto, display);

            display = string.Format("{0} - Robot Anulaciones Avisos  - Caja", Configuracion.Gds);
            asunto = "Boletos anulados, Caja: {0}";
            ProcesarEnvioACajas(boletosAnulados, mensaje.ToString(), asunto, display);

            display = string.Format("{0} - Robot Anulaciones Avisos  - Promotor", Configuracion.Gds);
            asunto = "Boletos anulados, Promotor: {0}";
            ProcesarEnvioPromotores(boletosAnulados, mensaje.ToString(), asunto, display);

            display = string.Format("{0} - Robot Anulaciones Avisos - Agencia", Configuracion.Gds);
            asunto = "Boletos anulados, Cliente: {0} - DK: {1}";
            ProcesarEnvioAAgencias(boletosAnulados, mensaje.ToString(), asunto, display);
        }

        #endregion

        #region  ProcesoEnvioNOAnulados
        public void ProcesoEnvioNOAnulados(List<BoletoRobotDTO> boletosNoAnulados)
        {
            display = string.Format("{0} - Robot Anulaciones Avisos", Configuracion.Gds);
            asunto = string.Format("El Robot de Anulaciones {0} NO PUDO ANULAR boletos pendientes de pago.", Configuracion.Gds);
            mensaje = new StringBuilder();
            mensaje.Append(string.Format("El robot de Anulaciones {0} NO PUDO ANULAR los siguientes boletos. Verificar los mensajes de error devueltos por el GDS : ", Configuracion.Gds));

            var factoryReporte = ReporteFactory.getReporteFactory(tipoProceso, boletosNoAnulados);
            var lcorreoBody = factoryReporte.ConstruirCorreo("", mensaje.ToString());
            var lcorreoFrom = string.Format("{0}_procesos@nmviajes.com", Configuracion.Gds).ToLower();

            var lscorreo = new GestorCorreo
              {
                  EmailFrom = lcorreoFrom,
                  EmailTo = Configuracion.correo_EmailCoordConsolidador,
                  EmailCC = Configuracion.correo_EmailCounterTurno + ";" + Configuracion.correo_EmailSupervisorCounterIA,
                  EmailBCC = Configuracion.correo_EmailRobotAlertas + ";" + Configuracion.correo_EmailNotificacion,
                  EmailDisplay = display,
                  EmailBody = lcorreoBody,
                  EmailSubject = asunto
              };
            lscorreo.Enviar();
        }
        #endregion

        #region ProcesoEnvioAnulacion
        public void ProcesoEnvioAnulacion()
        {
            var boletosAnulados = boletos.Where(boleto => boleto.AnulacionOkPorRobot).ToList();
            if (boletosAnulados.Any())
            {
                ProcesoEnvioAnuladosCorrectamente(boletosAnulados);
            }
            var boletosNoAnulados = boletos.Where(boleto => !boleto.AnulacionOkPorRobot).ToList();
            if (boletosNoAnulados.Any())
            {
                ProcesoEnvioNOAnulados(boletosNoAnulados);
            }
        }
        #endregion

        #region ProcesoEnvioAvisoAnulacion
        public void ProcesoEnvioAvisoAnulacion()
        {
            var boletosDeudaPendienteMayor20 = boletos.Where(boleto => boleto.TotalPendiente >= 20).ToList();

            if (boletosDeudaPendienteMayor20.Any())
            {
                mensaje = new StringBuilder(string.Format("Se hace de tu conocimiento que los siguientes boletos serán anulados a las {0}, por FALTA DE PAGO.", Configuracion.correo_HoraAnulacion));
                display = string.Format("{0} - Robot Anulaciones Avisos  - Counter", Configuracion.Gds);
                asunto = "Boletos sin pago serán anulados, Counter: {0}";
                ProcesarEnvioAVendedores(boletosDeudaPendienteMayor20, mensaje.ToString(), asunto, display);

                display = string.Format("{0} - Robot Anulaciones Avisos  - Caja", Configuracion.Gds);
                asunto = "Boletos sin pago serán anulados, Caja: {0}";
                ProcesarEnvioACajas(boletosDeudaPendienteMayor20, mensaje.ToString(), asunto, display);

                display = string.Format("{0} - Robot Anulaciones Avisos  - Promotor", Configuracion.Gds);
                asunto = "Boletos sin pago serán anulados, Promotor: {0}";
                ProcesarEnvioPromotores(boletosDeudaPendienteMayor20, mensaje.ToString(), asunto, display);

                mensaje = new StringBuilder();
                mensaje.Append(string.Format("Se hace de tu conocimiento que los siguientes boletos serán anulados a las {0}, por FALTA DE PAGO.", Configuracion.correo_HoraAnulacion));
                mensaje.Append(string.Format("Puedes hacer el pago y registrarlo antes de las {0} pm en nuestro portal web: {1}, función: <span class='textContenidoRegistra'>REGISTRA TU PAGO.</span> <br>", Configuracion.correo_HoraLimitePago, Configuracion.correo_WebPortal));
                mensaje.Append("Si ya realizaste el registro antes de que llegue este mensaje, revisa tu <span class='textContenidoRegistra'>Reporte Administrativo</span> en nuestro portal");
                display = string.Format("{0} - Robot Anulaciones Avisos - Agencia", Configuracion.Gds);
                asunto = "Boletos sin pago sera anulado, Cliente: {0} - DK: {1}";
                ProcesarEnvioAAgencias(boletosDeudaPendienteMayor20, mensaje.ToString(), asunto, display);
            }

            var boletosDeudaPendienteMenor20 = boletos.Where(boleto => boleto.TotalPendiente > 0.1 && boleto.TotalPendiente < 20).ToList();
            if (boletosDeudaPendienteMenor20.Any())
            {
                mensaje = new StringBuilder();
                mensaje.Append("Se hace de tu conocimiento que los siguientes boletos aún se encuentran pendientes de pago. <br>");
                mensaje.Append("No se anulará automáticamente ya que el saldo pendiente es menor a $20.00, por favor regularizar a la brevedad posible. ");
                display = string.Format("{0} - Robot Anulaciones Avisos  - Counter", Configuracion.Gds);
                asunto = "Boletos sin pago serán anulados, Counter: {0}";
                ProcesarEnvioAVendedores(boletosDeudaPendienteMenor20, mensaje.ToString(), asunto, display);
            }

        }
        #endregion

        #region ProcesarEnvioAVendedores
        public void ProcesarEnvioAVendedores(List<BoletoRobotDTO> lboletosDeudaPendiente, string mensaje, string asunto, string lcorreoDisplay)
        {
            var lcorreoFrom = string.Format("{0}_procesos@nmviajes.com", Configuracion.Gds).ToLower();
            var lvendedores = lboletosDeudaPendiente.GroupBy(p => new { p.IdVendedor }).Select(s => s.Key.IdVendedor).ToList();
            lvendedores.ForEach(lvendedor =>
            {
                var lboletosPorVendedor = lboletosDeudaPendiente.Where(boleto => boleto.IdVendedor.Equals(lvendedor)).ToList();

                var lencabezado = string.Format("Estimado(a): {0}, ", lboletosPorVendedor[0].Vendedor.Nombre);

                var factoryReporte = ReporteFactory.getReporteFactory(tipoProceso, lboletosPorVendedor);
                var lcorreoBody = factoryReporte.ConstruirCorreo(lencabezado, mensaje.ToString());
                var lcorreoAsunto = string.Format(asunto, lboletosPorVendedor[0].Vendedor.Nombre);

                var lscorreo = new GestorCorreo
                {
                    EmailFrom = lcorreoFrom,
                    EmailTo = lboletosPorVendedor[0].Vendedor.Correo,
                    EmailCC = (string.IsNullOrEmpty(lboletosPorVendedor[0].CorreoPromotor) ? Configuracion.correo_EmailPromotorAlternativo : lboletosPorVendedor[0].CorreoPromotor),
                    EmailDisplay = lcorreoDisplay,
                    EmailBody = lcorreoBody,
                    EmailSubject = lcorreoAsunto
                };
                lscorreo.Enviar();

            });
        }
        #endregion

        #region ProcesarEnvioAAgencias
        public void ProcesarEnvioAAgencias(List<BoletoRobotDTO> lboletosDeudaPendiente, string mensaje, string asunto, string lcorreoDisplay)
        {
            var lcorreoFrom = string.Format("{0}_procesos@nmviajes.com", Configuracion.Gds).ToLower();

            var lclientes = lboletosDeudaPendiente.GroupBy(p => new { p.IdCliente }).Select(s => s.Key.IdCliente).ToList();
            lclientes.ForEach(lcliente =>
            {
                var lboletosPorCliente = lboletosDeudaPendiente.Where(boleto => boleto.IdCliente == lcliente).ToList();
                var lencabezado = string.Format("Estimado Cliente : {0}, ", lboletosPorCliente[0].NombreCliente);
                var lcorreoAsunto = string.Format(asunto, lboletosPorCliente[0].NombreCliente, lboletosPorCliente[0].IdCliente);

                lcorreoDisplay = string.Format("{0} - Robot Anulaciones Avisos - Agencia", Configuracion.Gds);
                var factoryReporte = ReporteFactory.getReporteFactory(tipoProceso, lboletosPorCliente);
                var lcorreoBody = factoryReporte.ConstruirCorreo(lencabezado, mensaje.ToString());


                var lscorreo = new GestorCorreo
                {
                    EmailFrom = lcorreoFrom,
                    EmailTo = lboletosPorCliente[0].CorreoCliente,
                    EmailCC = (string.IsNullOrEmpty(lboletosPorCliente[0].CorreoPromotor) ? Configuracion.correo_EmailPromotorAlternativo : lboletosPorCliente[0].CorreoPromotor) + ";" + Configuracion.correo_EmailCoordConsolidador,
                    EmailDisplay = lcorreoDisplay,
                    EmailBody = lcorreoBody,
                    EmailSubject = lcorreoAsunto
                };
                lscorreo.Enviar();
            });
        }
        #endregion

        #region ProcesarEnvioACajas
        public void ProcesarEnvioACajas(List<BoletoRobotDTO> lboletosDeudaPendiente, string mensaje, string asunto, string lcorreoDisplay)
        {
            var lcorreoFrom = string.Format("{0}_procesos@nmviajes.com", Configuracion.Gds).ToLower();
            var lcajas = lboletosDeudaPendiente.GroupBy(p => new { p.CorreoCaja }).Select(s => s.Key.CorreoCaja).ToList();
            lcajas.ForEach(lcaja =>
            {
                var lboletosPorCaja = lboletosDeudaPendiente.Where(boleto => boleto.CorreoCaja.Equals(lcaja)).ToList();
                var lencabezado = string.Format("Caja: {0}, ", lboletosPorCaja[0].DescripcionSucursal);
                var factoryReporte = ReporteFactory.getReporteFactory(tipoProceso, lboletosPorCaja);
                var lcorreoBody = factoryReporte.ConstruirCorreo(lencabezado, mensaje.ToString());
                var lcorreoAsunto = string.Format(asunto, lboletosPorCaja[0].DescripcionSucursal);

                var lscorreo = new GestorCorreo
                {
                    EmailFrom = lcorreoFrom,
                    EmailTo = lboletosPorCaja[0].CorreoCaja,
                    EmailCC = "cajaconsolidador@nmviajes.com",
                    EmailBCC = Configuracion.correo_EmailRobotAlertas,
                    EmailDisplay = lcorreoDisplay,
                    EmailBody = lcorreoBody,
                    EmailSubject = lcorreoAsunto
                };
                lscorreo.Enviar();

            });
        }
        #endregion

        #region ProcesarEnvioPromotores
        public void ProcesarEnvioPromotores(List<BoletoRobotDTO> lboletosDeudaPendiente, string mensaje, string asunto, string lcorreoDisplay)
        {
            var lcorreoFrom = string.Format("{0}_procesos@nmviajes.com", Configuracion.Gds).ToLower();
            ReporteFactory lreporteFactory = null;
            var lpromotores = lboletosDeudaPendiente.GroupBy(p => new { p.IdPromotor }).Select(s => s.Key.IdPromotor).ToList();
            lpromotores.ForEach(lpromotor =>
            {
                var lboletosPorPomotor = lboletosDeudaPendiente.Where(boleto => boleto.IdPromotor == lpromotor).ToList();
                var lcorreoAsunto = string.Format(asunto, lboletosPorPomotor[0].NombrePromotor);
                var lencabezado = string.Format("Estimado(a): {0}, ", lboletosPorPomotor[0].NombrePromotor);
                var lconsolidado = new StringBuilder();
                var lclientesPromotor = lboletosPorPomotor.GroupBy(p => new { p.IdCliente }).Select(s => s.Key.IdCliente).ToList();
                lclientesPromotor.ForEach(lcliente =>
                {
                    var lboletosPorCliente = lboletosDeudaPendiente.Where(boleto => boleto.IdCliente == lcliente).ToList();
                    lreporteFactory = ReporteFactory.getReporteFactory(tipoProceso, lboletosPorCliente);
                    var ltabla = lreporteFactory.ConstruirTablaReporte();
                    lconsolidado.AppendLine(string.Format("<span class='textContenidoNegrita'> AGENCIA : {0} DK : {1}  </span>", lboletosPorCliente[0].NombreCliente, lboletosPorCliente[0].IdCliente));
                    lconsolidado.AppendLine(string.Format("<br>{0}<br>", ltabla));
                });

                var lcorreoBody = lreporteFactory.ConstruirCorreo(lencabezado, mensaje.ToString(), lconsolidado.ToString());

                var lscorreo = new GestorCorreo
                {
                    EmailFrom = lcorreoFrom,
                    EmailTo = lboletosPorPomotor[0].CorreoPromotor,
                    EmailCC = (string.IsNullOrEmpty(lboletosPorPomotor[0].CorreoJefePromotor) ? string.Empty : lboletosPorPomotor[0].CorreoJefePromotor),
                    EmailDisplay = lcorreoDisplay,
                    EmailBody = lcorreoBody,
                    EmailSubject = lcorreoAsunto
                };
                lscorreo.Enviar();
            });
        }
        #endregion
    }
}
