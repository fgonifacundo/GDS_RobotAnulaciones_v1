using ROTOB_ANULACION.WebServiceAmadeus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROTOB_ANULACION.Amadeus_WS
{
    public class WS_BoletoAmadeus
    {
        GestorLog logger = new GestorLog();

        public List<ReporteDiario> ObtenerReporteDiario(string officeId, string fecha)
        {
            var reportes = new List<ReporteDiario>();
            string[] officesId = officeId.Split('/');
            foreach (string lofficeId in officesId)
            {
                var lReporte = ObtenerReporteDiarioPorOfficeIDYFecha(lofficeId, fecha);
                reportes.Add(lReporte);
            }
            return reportes;
        }

        private ReporteDiario ObtenerReporteDiarioPorOfficeIDYFecha(string officeId, string fecha)
        {
            logger.info(string.Format("[ObtenerReporteDiarioPorOfficeIDYFecha] OfficeID: {0} - Fecha: {1}", officeId, fecha));
            var reporteDiario = new ReporteDiario();
            reporteDiario.Fecha = fecha;
            reporteDiario.Oficina = officeId;

            var reporte_RQ = buildRequestWS(officeId, fecha);
            var reporte_RS = new Service().obtenerReporte(officeId, reporte_RQ);
            if (reporte_RS != null)
            {
                if (reporte_RS.errorGroup == null)
                {
                    if (reporte_RS.queryReportDataDetails != null)
                    {
                        if (reporte_RS.queryReportDataDetails.queryReportDataOfficeGroup != null)
                        {
                            foreach (var dataOfficeGroup in reporte_RS.queryReportDataDetails.queryReportDataOfficeGroup)
                            {
                                if (dataOfficeGroup.documentData != null)
                                {
                                    var statusValid = "TKTT/TKTA/CANX";
                                    var documentData = dataOfficeGroup.documentData.Where(data => statusValid.Contains(data.transactionDataDetails.transactionDetails.code)).ToList();
                                    var boletos = new List<Boleto>();
                                    Boleto boleto = null;
                                    foreach (var item in documentData)
                                    {
                                        var originatorId = item.bookingAgent.originIdentification.originatorId;
                                        boleto = new Boleto();
                                        boleto.PNR = item.reservationInformation[0].controlNumber;
                                        boleto.NumBoleto = item.documentNumber.documentDetails.number;
                                        boleto.Agente = originatorId.Substring(4);
                                        boleto.Estado = item.transactionDataDetails.transactionDetails.code;
                                        boleto.NombrePasajero = item.passengerName.paxDetails.surname;
                                        boleto.FormaPago = item.fopDetails.fopDescription.formOfPayment.type;
                                        boletos.Add(boleto);
                                    }
                                    reporteDiario.Boletos = boletos;
                                }
                            }
                        }
                    }
                }
                else
                {
                    var errorDetails = reporte_RS.errorGroup.errorOrWarningCodeDetails.errorDetails;
                    var errorWarningDescription = reporte_RS.errorGroup.errorWarningDescription;
                    reporteDiario.Error = string.Format("[{0}] :: Error Code: {1} - Error Category: {2} - Descripcion: {3}", officeId, errorDetails.errorCode, errorDetails.errorCategory, string.Join("/", errorWarningDescription.freeText));
                }
            }
            return reporteDiario;
        }

        private SalesReports_DisplayQueryReport buildRequestWS(string officeId, string fecha)
        {
            var iata = new Service().obtenerIata(officeId);

            var dia = fecha.Split('/')[0];
            var mes = fecha.Split('/')[1];
            var anio = fecha.Split('/')[2];

            var reporte_RQ = new SalesReports_DisplayQueryReport();
            reporte_RQ.agencyDetails = new AdditionalBusinessSourceInformationTypeI();
            reporte_RQ.agencyDetails.sourceType = new SourceTypeDetailsTypeI();
            reporte_RQ.agencyDetails.sourceType.sourceQualifier1 = "REP";
            reporte_RQ.agencyDetails.originatorDetails = new OriginatorIdentificationDetailsTypeI();
            reporte_RQ.agencyDetails.originatorDetails.originatorId = iata;
            reporte_RQ.agencyDetails.originatorDetails.inHouseIdentification1 = officeId;
            reporte_RQ.requestOption = new SelectionDetailsTypeI();
            reporte_RQ.requestOption.selectionDetails = new SelectionDetailsInformationTypeI();
            reporte_RQ.requestOption.selectionDetails.option = "SOF";
            reporte_RQ.dateDetails = new StructuredDateTimeInformationType();
            reporte_RQ.dateDetails.businessSemantic = "S";
            reporte_RQ.dateDetails.dateTime = new StructuredDateTimeType();
            reporte_RQ.dateDetails.dateTime.year = anio;
            reporte_RQ.dateDetails.dateTime.month = mes;
            reporte_RQ.dateDetails.dateTime.day = dia;
            return reporte_RQ;
        }
    }
}
