Imports GDS_NuevoMundoPersistencia
Imports IWebServices = GDS_NM_WebServicesSabre.IWebServices
Imports objSabreWS = GDS_NM_WebServicesSabre
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Imports EscribeLog = GDS_MuevoMundoLog.EscribeLog
Imports System.IO
Imports System.Configuration
Imports System.Globalization

Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Private objIWebServices As IWebServices = Nothing
        Private objEscribeLog As New EscribeLog
        Dim lstTheadFare As New List(Of classTheadFare)
        Public Function CrearSession(ByVal strCodigoSeguimiento As String, _
                                     ByVal intGDS As Integer, _
                                     ByVal intFirmaGDS As Integer, _
                                     ByVal intFirmaDB As Integer) As classSession

            Dim objSessionSabre As classSession = Nothing
            Try
                objIWebServices = New IWebServices
                objSessionSabre = objIWebServices._SessionCreate(strCodigoSeguimiento, _
                                                                 intGDS, _
                                                                 intFirmaGDS, _
                                                                 intFirmaDB)
            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objIWebServices = Nothing
            End Try
            Return objSessionSabre
        End Function
        Public Function SessionClose(ByVal strCodigoSeguimiento As String, _
                                     ByVal intGDS As Integer, _
                                     ByVal intFirmaGDS As Integer, _
                                     ByVal intFirmaDB As Integer, _
                                     ByVal objSession As classSession) As String

            Dim objSessionClose As objSabreWS.SessionClose.SessionCloseRS = Nothing
            Dim strRespuesta As String = Nothing
            Try
                objIWebServices = New IWebServices
                objSessionClose = objIWebServices._SessionClose(strCodigoSeguimiento, _
                                                                intGDS, _
                                                                intFirmaGDS, _
                                                                intFirmaDB, _
                                                                objSession)
                If Not String.IsNullOrEmpty(objSessionClose.status) Then
                    strRespuesta = objSessionClose.status
                End If


            Catch ex As Exception
                If ex.Message.Contains("Invalid or Expired binary security token") Then
                    strRespuesta = "Invalid or Expired binary security token"
                Else
                    Throw New Exception(ex.ToString)
                End If

            Finally
                objIWebServices = Nothing
            End Try
            Return strRespuesta
        End Function
        Public Function SabreCommand(ByVal strComando As String, _
                                     ByVal strNombre As String, _
                                     ByVal strCodigoSeguimiento As String, _
                                     ByVal intGDS As Integer, _
                                     ByVal intFirmaGDS As Integer, _
                                     ByVal intFirmaDB As Integer, _
                                     ByVal objSession As classSession) As String

            Dim SabreCommandRS As objSabreWS.SabreCommand.SabreCommandLLSRS = Nothing
            Dim strRespuesta As String = Nothing

            Try



                objIWebServices = New IWebServices
                SabreCommandRS = objIWebServices._SabreCommand(strComando, _
                                                               strNombre, _
                                                               strCodigoSeguimiento, _
                                                               intGDS, _
                                                               intFirmaGDS, _
                                                               intFirmaDB, _
                                                               objSession)

                If SabreCommandRS IsNot Nothing Then
                    If SabreCommandRS.Response IsNot Nothing Then
                        strRespuesta = SabreCommandRS.Response.ToString
                    End If
                Else
                    strRespuesta = "No se proceso la solicitud"
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strComando = Nothing
                strNombre = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try

            Return strRespuesta

        End Function
        Public Function CambiarPseudo(ByVal strPseudo As String, _
                                      ByVal strCodigoSeguimiento As String, _
                                      ByVal intGDS As Integer, _
                                      ByVal intFirmaGDS As Integer, _
                                      ByVal intFirmaDB As Integer, _
                                      ByVal objSession As classSession) As String()

            Dim ContextChangeRS As objSabreWS.ContextChange.ContextChangeRS = Nothing
            Dim strRespuesta() As String = Nothing

            Try

                objIWebServices = New IWebServices
                ContextChangeRS = objIWebServices._ContextChange(strPseudo, _
                                                                 strCodigoSeguimiento, _
                                                                 intGDS, _
                                                                 intFirmaGDS, _
                                                                 intFirmaDB, _
                                                                 objSession)

                If ContextChangeRS IsNot Nothing Then
                    If ContextChangeRS.ApplicationResults IsNot Nothing Then

                        If ContextChangeRS.Text IsNot Nothing Then
                            For i As Integer = 0 To ContextChangeRS.Text.Length - 1
                                If strRespuesta Is Nothing Then
                                    ReDim strRespuesta(0)
                                Else
                                    ReDim Preserve strRespuesta(strRespuesta.Length)
                                End If
                                strRespuesta(strRespuesta.Length - 1) = "CONFIRMACION: " & ContextChangeRS.Text(i).ToString
                            Next
                        End If

                        If ContextChangeRS.ApplicationResults.Error IsNot Nothing Then
                            For i As Integer = 0 To ContextChangeRS.ApplicationResults.Error.Length - 1
                                If ContextChangeRS.ApplicationResults.Error(i).SystemSpecificResults IsNot Nothing Then
                                    For y As Integer = 0 To ContextChangeRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                        If ContextChangeRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message IsNot Nothing Then
                                            For z As Integer = 0 To ContextChangeRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message.Length - 1
                                                If ContextChangeRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value IsNot Nothing Then

                                                    If strRespuesta Is Nothing Then
                                                        ReDim strRespuesta(0)
                                                    Else
                                                        ReDim Preserve strRespuesta(strRespuesta.Length)
                                                    End If
                                                    strRespuesta(strRespuesta.Length - 1) = "ERROR: " & ContextChangeRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value

                                                End If
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If

                        If ContextChangeRS.ApplicationResults.Warning IsNot Nothing Then
                            For i As Integer = 0 To ContextChangeRS.ApplicationResults.Warning.Length - 1
                                If ContextChangeRS.ApplicationResults.Warning(i).SystemSpecificResults IsNot Nothing Then
                                    For y As Integer = 0 To ContextChangeRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                                        If ContextChangeRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message IsNot Nothing Then
                                            For z As Integer = 0 To ContextChangeRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message.Length - 1
                                                If ContextChangeRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value IsNot Nothing Then

                                                    If strRespuesta Is Nothing Then
                                                        ReDim strRespuesta(0)
                                                    Else
                                                        ReDim Preserve strRespuesta(strRespuesta.Length)
                                                    End If
                                                    strRespuesta(strRespuesta.Length - 1) = "WARNING: " & ContextChangeRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value

                                                End If
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If

                    End If
                Else

                    ReDim strRespuesta(0)
                    strRespuesta(strRespuesta.Length - 1) = "ERROR: " & Constantes.ProblemasVOID_Ticket
                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strPseudo = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try

            Return strRespuesta

        End Function

        Public Function DQB(ByVal strFecha As String, _
                                ByVal strCadenaPseudos As String, _
                                ByVal strPNR As String, _
                                ByVal strCodigoSeguimiento As String, _
                                ByVal intGDS As Integer, _
                                ByVal intFirmaGDS As Integer, _
                                ByVal intFirmaDB As Integer, _
                                ByVal objSession As classSession) As List(Of classReporteVentas)

            Return fDQB(strFecha, _
                        strCadenaPseudos, _
                        strPNR, _
                        strCodigoSeguimiento, _
                        intGDS, _
                        intFirmaGDS, _
                        intFirmaDB, _
                        objSession)

        End Function
        Public Function DQB(ByVal strFecha As String, _
                            ByVal strCadenaPseudos As String, _
                            ByVal strCodigoSeguimiento As String, _
                            ByVal intGDS As Integer, _
                            ByVal intFirmaGDS As Integer, _
                            ByVal intFirmaDB As Integer, _
                            ByVal objSession As classSession) As List(Of classReporteVentas)

            Return fDQB(strFecha, _
                        strCadenaPseudos, _
                        Nothing, _
                        strCodigoSeguimiento, _
                        intGDS, _
                        intFirmaGDS, _
                        intFirmaDB, _
                        objSession)

        End Function


        Private Function fDQB(ByVal strFecha As String, _
                              ByVal strCadenaPseudos As String, _
                              ByVal strPNR As String, _
                              ByVal strCodigoSeguimiento As String, _
                              ByVal intGDS As Integer, _
                              ByVal intFirmaGDS As Integer, _
                              ByVal intFirmaDB As Integer, _
                              ByVal objSession As classSession) As List(Of classReporteVentas)

            Dim DailySalesReportRS As objSabreWS.DailySalesReport.DailySalesReportRS = Nothing
            Dim objReporteVentas As List(Of classReporteVentas) = Nothing
            Dim auxReposteVentas As classReporteVentas = Nothing
            Dim auxBoleto As classReporteVentas.classBoleto = Nothing
            Dim bolFlagBoleto As Boolean = True

            Try

                If Not String.IsNullOrEmpty(strCadenaPseudos) Then
                    For i As Integer = 0 To strCadenaPseudos.Split(Constantes.Slash.ToString).Length - 1

                        objIWebServices = New IWebServices
                        'objIWebServices._ContextChange(strCadenaPseudos.Split(Constantes.Slash.ToString)(i).ToString, strCodigoSeguimiento, oGDS, objSession, oOp_Firma)
                        DailySalesReportRS = objIWebServices._DailySalesReport(strFecha, _
                                                                               strCadenaPseudos.Split(Constantes.Slash.ToString)(i).ToString, _
                                                                               strCodigoSeguimiento, _
                                                                               intGDS, _
                                                                               intFirmaGDS, _
                                                                               intFirmaDB, _
                                                                               objSession)

                        If DailySalesReportRS IsNot Nothing Then

                            If Not DailySalesReportRS.SalesReport Is Nothing Then
                                If Not DailySalesReportRS.SalesReport.CreationDetails Is Nothing Then
                                    If Not DailySalesReportRS.SalesReport.CreationDetails.Source Is Nothing Then


                                        auxReposteVentas = New classReporteVentas

                                        auxReposteVentas.Pseudo = DailySalesReportRS.SalesReport.CreationDetails.Source.PseudoCityCode.ToString
                                        auxReposteVentas.Fecha = DailySalesReportRS.SalesReport.CreationDetails.Source.CreateDateTime.ToString
                                        auxReposteVentas.NombrePseudo = DailySalesReportRS.SalesReport.CreationDetails.Source.AgencyName.ToString

                                        If Not DailySalesReportRS.SalesReport.IssuanceData Is Nothing Then

                                            For x As Integer = 0 To DailySalesReportRS.SalesReport.IssuanceData.Length - 1

                                                auxBoleto = New classReporteVentas.classBoleto

                                                If Not String.IsNullOrEmpty(DailySalesReportRS.SalesReport.IssuanceData(x).ItineraryRef) Then
                                                    auxBoleto.PNR = DailySalesReportRS.SalesReport.IssuanceData(x).ItineraryRef

                                                    If Not String.IsNullOrEmpty(strPNR) Then
                                                        If Not auxBoleto.PNR.Equals(strPNR.Trim) Then
                                                            bolFlagBoleto = False
                                                        End If
                                                    End If

                                                End If

                                                If bolFlagBoleto Then



                                                    If Not String.IsNullOrEmpty(DailySalesReportRS.SalesReport.IssuanceData(x).AgentSine) Then
                                                        auxBoleto.Agente = DailySalesReportRS.SalesReport.IssuanceData(x).AgentSine
                                                    End If


                                                    If DailySalesReportRS.SalesReport.IssuanceData(x).IndicatorOne IsNot Nothing Then
                                                        If DailySalesReportRS.SalesReport.IssuanceData(x).IndicatorOne.ToString = "V" Then
                                                            auxBoleto.Estado = "VOID"
                                                        ElseIf DailySalesReportRS.SalesReport.IssuanceData(x).IndicatorOne.ToString = "E" Then
                                                            auxBoleto.Estado = "REMISION"
                                                        ElseIf DailySalesReportRS.SalesReport.IssuanceData(x).IndicatorOne.ToString = "A" Then
                                                            auxBoleto.Estado = "REMISION"
                                                        Else
                                                            auxBoleto.Estado = "ACTIVO"
                                                        End If
                                                    Else
                                                        auxBoleto.Estado = "ACTIVO"
                                                    End If

                                                    If Not DailySalesReportRS.SalesReport.IssuanceData(x).PersonName Is Nothing Then
                                                        auxBoleto.NombrePasajero = DailySalesReportRS.SalesReport.IssuanceData(x).PersonName.ToString
                                                    End If

                                                    If Not DailySalesReportRS.SalesReport.IssuanceData(x).Commission Is Nothing Then
                                                        auxBoleto.Comision = DailySalesReportRS.SalesReport.IssuanceData(x).Commission.ToString
                                                    End If



                                                    If Not DailySalesReportRS.SalesReport.IssuanceData(x).TicketingInfo(0) Is Nothing Then
                                                        If DailySalesReportRS.SalesReport.IssuanceData(x).TicketingInfo(0).eTicketNumber IsNot Nothing Then
                                                            auxBoleto.NumBoleto = DailySalesReportRS.SalesReport.IssuanceData(x).TicketingInfo(0).eTicketNumber
                                                        End If

                                                        If DailySalesReportRS.SalesReport.IssuanceData(x).TicketingInfo(0).ConjunctiveCount IsNot Nothing Then
                                                            auxBoleto.Conjuncion = DailySalesReportRS.SalesReport.IssuanceData(x).TicketingInfo(0).ConjunctiveCount
                                                        End If
                                                    End If

                                                    If Not DailySalesReportRS.SalesReport.IssuanceData(x).IssueTime Is Nothing Then
                                                        auxBoleto.Hora = DailySalesReportRS.SalesReport.IssuanceData(x).IssueTime
                                                    End If

                                                    If Not DailySalesReportRS.SalesReport.IssuanceData(x).DomesticInternational Is Nothing Then
                                                        auxBoleto.Domestico = DailySalesReportRS.SalesReport.IssuanceData(x).DomesticInternational
                                                    End If

                                                    If Not DailySalesReportRS.SalesReport.IssuanceData(x).Payment Is Nothing Then
                                                        For y As Integer = 0 To DailySalesReportRS.SalesReport.IssuanceData(x).Payment.Length - 1
                                                            If Not DailySalesReportRS.SalesReport.IssuanceData(x).Payment(y).Form.Value Is Nothing Then
                                                                auxBoleto.FormaPago &= IIf(String.IsNullOrEmpty(auxBoleto.FormaPago), "", "\") & DailySalesReportRS.SalesReport.IssuanceData(x).Payment(y).Form.Value.ToString
                                                            End If
                                                        Next
                                                    End If


                                                    If auxReposteVentas.Boletos Is Nothing Then auxReposteVentas.Boletos = New List(Of classReporteVentas.classBoleto)
                                                    auxReposteVentas.Boletos.Add(auxBoleto)

                                                End If

                                                auxBoleto = Nothing
                                                bolFlagBoleto = True
                                            Next
                                        End If

                                        If objReporteVentas Is Nothing Then objReporteVentas = New List(Of classReporteVentas)
                                        objReporteVentas.Add(auxReposteVentas)
                                        auxReposteVentas = Nothing

                                    End If
                                End If
                            End If
                        Else
                            auxReposteVentas = New classReporteVentas

                            auxReposteVentas.Pseudo = DailySalesReportRS.SalesReport.CreationDetails.Source.PseudoCityCode.ToString
                            auxReposteVentas.Fecha = DailySalesReportRS.SalesReport.CreationDetails.Source.CreateDateTime.ToString
                            auxReposteVentas.NombrePseudo = DailySalesReportRS.SalesReport.CreationDetails.Source.AgencyName.ToString
                            auxReposteVentas.MSGError = Constantes.ProblemasDQB
                        End If
                    Next
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strFecha = Nothing
                strCadenaPseudos = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
                objIWebServices = Nothing
                objIWebServices = Nothing
            End Try

            Return objReporteVentas
        End Function
        Public Function DQB_EMD(ByVal strFecha As String, _
                                ByVal strCadenaPseudos As String, _
                                ByVal strCodigoSeguimiento As String, _
                                ByVal intGDS As Integer, _
                                ByVal intFirmaGDS As Integer, _
                                ByVal intFirmaDB As Integer, _
                                ByVal objSession As classSession) As List(Of classReporteVentas)

            Dim DailyEMD_ReportRS As objSabreWS.DailyEMD_Report.DailyEMD_ReportRS = Nothing
            Dim objReporteVentas As List(Of classReporteVentas) = Nothing
            Dim auxReposteVentas As classReporteVentas = Nothing
            Dim auxBoleto As classReporteVentas.classBoleto = Nothing

            Try

                If Not String.IsNullOrEmpty(strCadenaPseudos) Then
                    For i As Integer = 0 To strCadenaPseudos.Split(Constantes.Slash.ToString).Length - 1

                        objIWebServices = New IWebServices
                        objIWebServices._IgnoreTransaction(strCodigoSeguimiento, _
                                                           intGDS, _
                                                           intFirmaGDS, _
                                                           intFirmaDB, _
                                                           objSession)

                        objIWebServices._ContextChange(strCadenaPseudos.Split(Constantes.Slash.ToString)(i).ToString, _
                                                       strCodigoSeguimiento, _
                                                       intGDS, _
                                                       intFirmaGDS, _
                                                       intFirmaDB, _
                                                       objSession)

                        DailyEMD_ReportRS = objIWebServices._DailyEMD_Report(strFecha, _
                                                                             strCadenaPseudos.Split(Constantes.Slash.ToString)(i).ToString, _
                                                                             strCodigoSeguimiento, _
                                                                             intGDS, _
                                                                             intFirmaGDS, _
                                                                             intFirmaDB, _
                                                                             objSession)

                        If DailyEMD_ReportRS IsNot Nothing Then

                            If Not DailyEMD_ReportRS.EMD_Report Is Nothing Then
                                If Not DailyEMD_ReportRS.EMD_Report.CreationDetails Is Nothing Then
                                    If Not DailyEMD_ReportRS.EMD_Report.CreationDetails(0) Is Nothing Then


                                        auxReposteVentas = New classReporteVentas

                                        auxReposteVentas.Pseudo = DailyEMD_ReportRS.EMD_Report.CreationDetails(0).PseudoCityCode.ToString
                                        auxReposteVentas.Fecha = DailyEMD_ReportRS.EMD_Report.CreationDetails(0).CreateDateTime.ToString
                                        auxReposteVentas.NombrePseudo = DailyEMD_ReportRS.EMD_Report.CreationDetails(0).AgencyName.ToString

                                        If Not DailyEMD_ReportRS.EMD_Report.IssuanceData Is Nothing Then

                                            For x As Integer = 0 To DailyEMD_ReportRS.EMD_Report.IssuanceData.Length - 1

                                                auxBoleto = New classReporteVentas.classBoleto

                                                If Not String.IsNullOrEmpty(DailyEMD_ReportRS.EMD_Report.IssuanceData(x).ItineraryRef) Then
                                                    auxBoleto.PNR = DailyEMD_ReportRS.EMD_Report.IssuanceData(x).ItineraryRef
                                                End If

                                                If Not String.IsNullOrEmpty(DailyEMD_ReportRS.EMD_Report.IssuanceData(x).AgentSine) Then
                                                    auxBoleto.Agente = DailyEMD_ReportRS.EMD_Report.IssuanceData(x).AgentSine
                                                End If


                                                If DailyEMD_ReportRS.EMD_Report.IssuanceData(x).Ind IsNot Nothing Then
                                                    If DailyEMD_ReportRS.EMD_Report.IssuanceData(x).Ind.ToString = "V" Then
                                                        auxBoleto.Estado = "VOID"
                                                    ElseIf DailyEMD_ReportRS.EMD_Report.IssuanceData(x).Ind.ToString.ToUpper.Contains("EXCHANGE") Then
                                                        auxBoleto.Estado = "REMISION"
                                                    Else
                                                        auxBoleto.Estado = "ACTIVO"
                                                    End If
                                                Else
                                                    auxBoleto.Estado = "ACTIVO"
                                                End If

                                                If Not DailyEMD_ReportRS.EMD_Report.IssuanceData(x).PersonName Is Nothing Then
                                                    If Not DailyEMD_ReportRS.EMD_Report.IssuanceData(x).PersonName.Surname Is Nothing Then
                                                        auxBoleto.NombrePasajero = DailyEMD_ReportRS.EMD_Report.IssuanceData(x).PersonName.Surname
                                                    End If
                                                End If

                                                If Not DailyEMD_ReportRS.EMD_Report.IssuanceData(x).Commission Is Nothing Then
                                                    auxBoleto.Comision = DailyEMD_ReportRS.EMD_Report.IssuanceData(x).Commission.ToString
                                                End If



                                                If Not DailyEMD_ReportRS.EMD_Report.IssuanceData(x).TicketingInfo(0) Is Nothing Then
                                                    If DailyEMD_ReportRS.EMD_Report.IssuanceData(x).TicketingInfo(0).eTicketNumber IsNot Nothing Then
                                                        auxBoleto.NumBoleto = DailyEMD_ReportRS.EMD_Report.IssuanceData(x).TicketingInfo(0).eTicketNumber
                                                    End If

                                                    If DailyEMD_ReportRS.EMD_Report.IssuanceData(x).TicketingInfo(0).ConjunctiveCount IsNot Nothing Then
                                                        auxBoleto.Conjuncion = DailyEMD_ReportRS.EMD_Report.IssuanceData(x).TicketingInfo(0).ConjunctiveCount
                                                    End If
                                                End If

                                                If Not DailyEMD_ReportRS.EMD_Report.IssuanceData(x).DateTime Is Nothing Then
                                                    auxBoleto.Hora = DailyEMD_ReportRS.EMD_Report.IssuanceData(x).DateTime
                                                End If

                                                If Not DailyEMD_ReportRS.EMD_Report.IssuanceData(x).DomesticInternational Is Nothing Then
                                                    auxBoleto.Domestico = DailyEMD_ReportRS.EMD_Report.IssuanceData(x).DomesticInternational
                                                End If

                                                If Not DailyEMD_ReportRS.EMD_Report.IssuanceData(x).Payment Is Nothing Then
                                                    For y As Integer = 0 To DailyEMD_ReportRS.EMD_Report.IssuanceData(x).Payment.Length - 1
                                                        If Not DailyEMD_ReportRS.EMD_Report.IssuanceData(x).Payment(y).Form.Value Is Nothing Then
                                                            auxBoleto.FormaPago = DailyEMD_ReportRS.EMD_Report.IssuanceData(x).Payment(y).Form.Value.ToString
                                                        End If
                                                    Next
                                                End If


                                                If auxReposteVentas.Boletos Is Nothing Then auxReposteVentas.Boletos = New List(Of classReporteVentas.classBoleto)
                                                auxReposteVentas.Boletos.Add(auxBoleto)
                                                auxBoleto = Nothing
                                            Next
                                        End If

                                        If objReporteVentas Is Nothing Then objReporteVentas = New List(Of classReporteVentas)
                                        objReporteVentas.Add(auxReposteVentas)
                                        auxReposteVentas = Nothing

                                    End If
                                End If
                            End If
                        Else
                            auxReposteVentas = New classReporteVentas

                            auxReposteVentas.Pseudo = DailyEMD_ReportRS.EMD_Report.CreationDetails(0).PseudoCityCode.ToString
                            auxReposteVentas.Fecha = DailyEMD_ReportRS.EMD_Report.CreationDetails(0).CreateDateTime.ToString
                            auxReposteVentas.NombrePseudo = DailyEMD_ReportRS.EMD_Report.CreationDetails(0).AgencyName.ToString
                            auxReposteVentas.MSGError = Constantes.ProblemasDQB
                        End If
                    Next
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strFecha = Nothing
                strCadenaPseudos = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
                objIWebServices = Nothing
                objIWebServices = Nothing
            End Try

            Return objReporteVentas
        End Function
        Public Function RecuperarPNR(ByVal strPNR As String, _
                                     ByVal strCodigoSeguimiento As String, _
                                     ByVal intGDS As Integer, _
                                     ByVal intFirmaGDS As Integer, _
                                     ByVal intFirmaDB As Integer, _
                                     ByVal intEsquema As Integer, _
                                     ByVal objSession As classSession) As classPNR

            Dim TravelItineraryReadRS As objSabreWS.TravelItineraryReadRQ.TravelItineraryReadRS = Nothing
            Dim objPNR As classPNR = Nothing
            Dim objPasajeros As List(Of classPasajeros) = Nothing
            Dim auxPasajeros As classPasajeros = Nothing

            Dim objSegmentos As List(Of classSegmentos) = Nothing
            Dim auxSegmentos As classSegmentos = Nothing

            Dim objDocumento As New List(Of classDocumento)
            Dim auxDocumento As classDocumento = Nothing
            Dim oCadenaError As New System.Text.StringBuilder
            oCadenaError.Append("")

            Dim SabreCommandLLSRS As objSabreWS.SabreCommand.SabreCommandLLSRS = Nothing
            Dim EndTransactionRS As objSabreWS.EndTransaction.EndTransactionRS = Nothing

            Dim oDate As Date = Nothing
            Dim oAuxMesUP As String = Nothing
            Dim oMesDeparture As String = Nothing
            Dim oAnoDeparture As String = Nothing
            Dim oAnoArrival As String = Nothing
            Dim oMesArrival As String = Nothing
            Dim AuxArrivalDateTime As String = Nothing
            Dim AuxUpdatedDepartureTime As String = Nothing
            Dim AuxUpdatedArrivalTime As String = Nothing

            Dim objDAO As DAO = Nothing
            Dim objCiudad As classCiudad = Nothing

            'Dim objWSBusiness As WSBusiness = Nothing
            Dim intContadorSegmento As Integer = 1

            Dim auxAsientos As classAsientos = Nothing
            Dim auxPax As String = Nothing

            Dim auxBoleto As classPNR.classBoletos = Nothing

            Try

                If Not String.IsNullOrEmpty(strPNR) Then


                    objIWebServices = New IWebServices



                    TravelItineraryReadRS = objIWebServices._TravelItineraryReadRQ(strPNR, _
                                                                                   strCodigoSeguimiento, _
                                                                                   intGDS, _
                                                                                   intFirmaGDS, _
                                                                                   intFirmaDB, _
                                                                                   objSession)


                    If intGDS = 5 Then GoTo marcaTURBO 'GENERARDOR DE PNR


                    SabreCommandLLSRS = objIWebServices._SabreCommand("6WEB" & Constantes.IWS_Sumarotia & "ER", _
                                                                      "GRABAR_Y_RECUPERAR", _
                                                                      strCodigoSeguimiento, _
                                                                      intGDS, _
                                                                      intFirmaGDS, _
                                                                      intFirmaDB, _
                                                                      objSession)


                    If SabreCommandLLSRS IsNot Nothing Then
                        If SabreCommandLLSRS.ErrorRS Is Nothing Then

                            If SabreCommandLLSRS.Response.IndexOf("*WARNING EDITS*") > -1 Or SabreCommandLLSRS.Response.IndexOf("*MANDATORY EDITS*") > -1 Then

                                SabreCommandLLSRS = objIWebServices._SabreCommand("XPG", _
                                                                                  "XPG", _
                                                                                  strCodigoSeguimiento, _
                                                                                  intGDS, _
                                                                                  intFirmaGDS, _
                                                                                  intFirmaDB, _
                                                                                  objSession)

                                If SabreCommandLLSRS IsNot Nothing Then
                                    If SabreCommandLLSRS.ErrorRS Is Nothing Then

                                        EndTransactionRS = objIWebServices._EndTransaction("6WEB", _
                                                                                           Nothing, _
                                                                                           strCodigoSeguimiento, _
                                                                                           intGDS, _
                                                                                           intFirmaGDS, _
                                                                                           intFirmaDB, _
                                                                                           objSession)

                                        If EndTransactionRS IsNot Nothing Then
                                            If EndTransactionRS.ApplicationResults IsNot Nothing Then
                                                If EndTransactionRS.ApplicationResults.Error Is Nothing Then
                                                    If EndTransactionRS.ApplicationResults.Warning Is Nothing Then


                                                        SabreCommandLLSRS = objIWebServices._SabreCommand("ER", _
                                                                                                          "FINALIZAR_Y_RECUPERAR", _
                                                                                                          strCodigoSeguimiento, _
                                                                                                          intGDS, _
                                                                                                          intFirmaGDS, _
                                                                                                          intFirmaDB, _
                                                                                                          objSession)


                                                        If SabreCommandLLSRS IsNot Nothing Then
                                                            If SabreCommandLLSRS.ErrorRS Is Nothing Then

                                                                If SabreCommandLLSRS.Response.IndexOf("VERIFY ORDER OF ITINERARY SEGMENTS") > -1 Then


marcaARUNK:                                                         SabreCommandLLSRS = objIWebServices._SabreCommand("0AA", _
                                                                                                                      "ARUNK", _
                                                                                                                      strCodigoSeguimiento, _
                                                                                                                      intGDS, _
                                                                                                                      intFirmaGDS, _
                                                                                                                      intFirmaDB, _
                                                                                                                      objSession)

                                                                    If SabreCommandLLSRS IsNot Nothing Then
                                                                        If SabreCommandLLSRS.ErrorRS Is Nothing Then

                                                                            EndTransactionRS = objIWebServices._EndTransaction("6WEB", _
                                                                                                                               "E", _
                                                                                                                               strCodigoSeguimiento, _
                                                                                                                               intGDS, _
                                                                                                                               intFirmaGDS, _
                                                                                                                               intFirmaDB, _
                                                                                                                               objSession)

                                                                            If EndTransactionRS IsNot Nothing Then
                                                                                If EndTransactionRS.ApplicationResults IsNot Nothing Then
                                                                                    If EndTransactionRS.ApplicationResults.Error Is Nothing Then
                                                                                        If EndTransactionRS.ApplicationResults.Warning Is Nothing Then

marcaRecuperar:

                                                                                            TravelItineraryReadRS = objIWebServices._TravelItineraryReadRQ(strPNR, _
                                                                                                                                                           strCodigoSeguimiento, _
                                                                                                                                                           intGDS, _
                                                                                                                                                           intFirmaGDS, _
                                                                                                                                                           intFirmaDB, _
                                                                                                                                                           objSession)

                                                                                        Else
                                                                                            If EndTransactionRS.ItineraryRef IsNot Nothing Then
                                                                                                If EndTransactionRS.ItineraryRef.ID = strPNR Then

                                                                                                    TravelItineraryReadRS = objIWebServices._TravelItineraryReadRQ(strPNR, _
                                                                                                                                                                   strCodigoSeguimiento, _
                                                                                                                                                                   intGDS, _
                                                                                                                                                                   intFirmaGDS, _
                                                                                                                                                                   intFirmaDB, _
                                                                                                                                                                   objSession)

                                                                                                End If
                                                                                            End If

                                                                                        End If
                                                                                    Else
                                                                                        For i As Integer = 0 To EndTransactionRS.ApplicationResults.Error.Length - 1
                                                                                            If EndTransactionRS.ApplicationResults.Error(i).SystemSpecificResults IsNot Nothing Then
                                                                                                For x As Integer = 0 To EndTransactionRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                                                                                    If EndTransactionRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message IsNot Nothing Then
                                                                                                        For z As Integer = 0 To EndTransactionRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message.Length - 1
                                                                                                            If EndTransactionRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(z).Value IsNot Nothing Then
                                                                                                                If EndTransactionRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(z).Value.Contains(Constantes.msgINFANT_DETAILS_REQUIRED_SSR) Then



                                                                                                                    SabreCommandLLSRS = objIWebServices._SabreCommand("ER", _
                                                                                                                                                                      "FINALIZAR_Y_RECUPERAR", _
                                                                                                                                                                      strCodigoSeguimiento, _
                                                                                                                                                                      intGDS, _
                                                                                                                                                                      intFirmaGDS, _
                                                                                                                                                                      intFirmaDB, _
                                                                                                                                                                      objSession)

                                                                                                                    TravelItineraryReadRS = Nothing
                                                                                                                    GoTo marcaRecuperar
                                                                                                                    Exit For
                                                                                                                End If
                                                                                                            End If
                                                                                                        Next
                                                                                                    End If

                                                                                                    If TravelItineraryReadRS Is Nothing Then
                                                                                                        Exit For
                                                                                                    End If
                                                                                                Next
                                                                                            End If

                                                                                            If TravelItineraryReadRS Is Nothing Then
                                                                                                Exit For
                                                                                            End If
                                                                                        Next
                                                                                    End If
                                                                                Else
                                                                                    TravelItineraryReadRS = Nothing
                                                                                End If
                                                                            Else
                                                                                TravelItineraryReadRS = Nothing
                                                                            End If
                                                                        Else
                                                                            TravelItineraryReadRS = Nothing
                                                                        End If
                                                                    Else
                                                                        TravelItineraryReadRS = Nothing
                                                                    End If

                                                                Else
                                                                    'recupewra

                                                                End If
                                                            Else
                                                                TravelItineraryReadRS = Nothing
                                                            End If
                                                        Else
                                                            TravelItineraryReadRS = Nothing
                                                        End If

                                                    End If
                                                Else
                                                    TravelItineraryReadRS = Nothing
                                                End If
                                            Else
                                                TravelItineraryReadRS = Nothing
                                            End If
                                        Else
                                            TravelItineraryReadRS = Nothing

                                        End If
                                    Else
                                        TravelItineraryReadRS = Nothing
                                    End If
                                Else
                                    TravelItineraryReadRS = Nothing
                                End If

                            ElseIf SabreCommandLLSRS.Response.IndexOf("VERIFY ORDER OF ITINERARY SEGMENTS") > -1 Then
                                TravelItineraryReadRS = Nothing
                                GoTo marcaARUNK
                            ElseIf SabreCommandLLSRS.Response.Contains(Constantes.msgINFANT_DETAILS_REQUIRED_SSR) Then

                                SabreCommandLLSRS = objIWebServices._SabreCommand("ER", _
                                                                                  "FINALIZAR_Y_RECUPERAR", _
                                                                                  strCodigoSeguimiento, _
                                                                                  intGDS, _
                                                                                  intFirmaGDS, _
                                                                                  intFirmaDB, _
                                                                                  objSession)


                                TravelItineraryReadRS = Nothing
                                GoTo marcaRecuperar
                            End If
                        Else
                            TravelItineraryReadRS = Nothing
                        End If
                    Else
                        TravelItineraryReadRS = Nothing
                    End If


marcaTURBO:

                    If TravelItineraryReadRS IsNot Nothing Then


                        If TravelItineraryReadRS.ApplicationResults IsNot Nothing Then
                            objPNR = New classPNR

                            If TravelItineraryReadRS.ApplicationResults.Error IsNot Nothing Then
                                For i As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Error.Length - 1
                                    For x As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                        'TravelItineraryReadRS.ApplicationResults.Error(i).SystemSpecificResults(x).HostCommand.LNIATA.ToString()
                                        'TravelItineraryReadRS.ApplicationResults.Error(i).SystemSpecificResults(x).HostCommand.Value.ToString()
                                        For y As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message.Length - 1
                                            objPNR.MSGError.Add("ERROR : " & TravelItineraryReadRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(y).Value.ToString())
                                        Next
                                    Next
                                Next
                            End If

                            If TravelItineraryReadRS.ApplicationResults.Warning IsNot Nothing Then
                                For i As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Warning.Length - 1
                                    For x As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                        'TravelItineraryReadRS.ApplicationResults.Error(i).SystemSpecificResults(x).HostCommand.LNIATA.ToString()
                                        'TravelItineraryReadRS.ApplicationResults.Error(i).SystemSpecificResults(x).HostCommand.Value.ToString()
                                        For y As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message.Length - 1
                                            objPNR.MSGError.Add("WARNING : " & TravelItineraryReadRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message(y).Value.ToString())
                                        Next
                                    Next
                                Next
                            End If


                            If TravelItineraryReadRS.TravelItinerary IsNot Nothing Then
                                Dim SegmentNumber As String = Nothing

                                Dim _ArrivalDateTime As String
                                Dim _ArrivalAirportLocationCode As String
                                Dim _UpdatedArrivalTime As String
                                Dim InfoItem() As objSabreWS.TravelItineraryReadRQ.TravelItineraryReadRSTravelItineraryItineraryInfoItem = Nothing

                                Dim Seat() As objSabreWS.TravelItineraryReadRQ.TravelItineraryReadRSTravelItineraryItineraryInfoItemSeat = Nothing

                                Dim oFlag As Boolean = False

                                '****** REORDENAMOS LOS VUELOS ********
                                If TravelItineraryReadRS.TravelItinerary.ItineraryInfo IsNot Nothing Then

                                    If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems IsNot Nothing Then

                                        For i As Integer = 0 To TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems.Length - 1

                                            '========================================================

                                            If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment IsNot Nothing Then

                                                If Not oFlag Then

                                                    SegmentNumber = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).SegmentNumber.Trim

                                                    ReDim InfoItem(0)
                                                    InfoItem(0) = New objSabreWS.TravelItineraryReadRQ.TravelItineraryReadRSTravelItineraryItineraryInfoItem
                                                    InfoItem(0).RPH = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).RPH
                                                    ReDim InfoItem(0).FlightSegment(0)
                                                    InfoItem(0).FlightSegment(0) = New objSabreWS.TravelItineraryReadRQ.TravelItineraryReadRSTravelItineraryItineraryInfoItemFlightSegment
                                                    InfoItem(0).FlightSegment = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment

                                                    oFlag = True
                                                    i += 1

                                                End If

                                                If i < TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems.Length - 1 Then
                                                    If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment IsNot Nothing Then

                                                        If SegmentNumber = CStr(CInt(TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).SegmentNumber.Trim)) Then

                                                            _ArrivalDateTime = Nothing
                                                            _ArrivalAirportLocationCode = Nothing
                                                            _UpdatedArrivalTime = Nothing

                                                            _ArrivalDateTime = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).ArrivalDateTime
                                                            _ArrivalAirportLocationCode = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).DestinationLocation.LocationCode.Trim
                                                            If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).UpdatedArrivalTime IsNot Nothing Then
                                                                _UpdatedArrivalTime = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).UpdatedArrivalTime.Trim
                                                            End If

                                                            InfoItem(InfoItem.Length - 1).FlightSegment(0).ArrivalDateTime = _ArrivalDateTime
                                                            InfoItem(InfoItem.Length - 1).FlightSegment(0).DestinationLocation.LocationCode = _ArrivalAirportLocationCode
                                                            InfoItem(InfoItem.Length - 1).FlightSegment(0).UpdatedArrivalTime = _UpdatedArrivalTime

                                                        Else
                                                            ReDim Preserve InfoItem(InfoItem.Length)
                                                            InfoItem(InfoItem.Length - 1) = New objSabreWS.TravelItineraryReadRQ.TravelItineraryReadRSTravelItineraryItineraryInfoItem
                                                            InfoItem(InfoItem.Length - 1).RPH = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).RPH.Trim
                                                            ReDim InfoItem(InfoItem.Length - 1).FlightSegment(0)
                                                            InfoItem(InfoItem.Length - 1).FlightSegment(0) = New objSabreWS.TravelItineraryReadRQ.TravelItineraryReadRSTravelItineraryItineraryInfoItemFlightSegment
                                                            InfoItem(InfoItem.Length - 1).FlightSegment = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment

                                                        End If

                                                        SegmentNumber = CStr(CInt(TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).SegmentNumber.Trim))

                                                    End If
                                                End If

                                            End If

                                            '========================================================


                                            If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).Seats IsNot Nothing Then
                                                If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).Seats IsNot Nothing Then

                                                    For j As Integer = 0 To TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).Seats.Length - 1

                                                        If Seat Is Nothing Then
                                                            ReDim Seat(0)
                                                        Else
                                                            ReDim Preserve Seat(Seat.Length)
                                                        End If

                                                        Seat(Seat.Length - 1) = New objSabreWS.TravelItineraryReadRQ.TravelItineraryReadRSTravelItineraryItineraryInfoItemSeat
                                                        Seat(Seat.Length - 1) = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).Seats(j)

                                                    Next

                                                End If
                                            End If


                                        Next

                                        If Seat IsNot Nothing Then
                                            If InfoItem Is Nothing Then
                                                ReDim Preserve InfoItem(0)
                                            Else
                                                ReDim Preserve InfoItem(InfoItem.Length)
                                            End If
                                            InfoItem(InfoItem.Length - 1) = New objSabreWS.TravelItineraryReadRQ.TravelItineraryReadRSTravelItineraryItineraryInfoItem
                                            InfoItem(InfoItem.Length - 1).Seats = Seat
                                        End If



                                        TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems = InfoItem
                                    Else
                                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                        objPNR.MSGError.Add("WARNING : NO EXISTE ITINERARIO PARA LA RESERVA")
                                        Exit Try
                                    End If

                                End If


                                If TravelItineraryReadRS.TravelItinerary.ItineraryRef IsNot Nothing Then

                                    If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.CustomerIdentifier) Then
                                        objPNR.CustomerId = TravelItineraryReadRS.TravelItinerary.ItineraryRef.CustomerIdentifier
                                    End If

                                    If TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source IsNot Nothing Then


                                        If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.HomePseudoCityCode) Then
                                            objPNR.PseudoHome = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.HomePseudoCityCode.ToString
                                        End If

                                        If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.AAA_PseudoCityCode) Then
                                            objPNR.PseudoAAA = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.AAA_PseudoCityCode.ToString
                                        End If

                                        If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.PseudoCityCode) Then
                                            objPNR.PseudoRelease = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.PseudoCityCode.ToString
                                        End If

                                        If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.CreateDateTime) Then
                                            objPNR.FechaCreacion = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.CreateDateTime.ToString
                                        End If

                                        If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.CreationAgent) Then
                                            objPNR.AgenteCreador = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.CreationAgent.ToString
                                        End If

                                    End If
                                End If

                                '****** PASAJEROS ****
                                If TravelItineraryReadRS.TravelItinerary.CustomerInfo IsNot Nothing Then
                                    With TravelItineraryReadRS.TravelItinerary.CustomerInfo

                                        If .PersonName IsNot Nothing Then

                                            For i As Integer = 0 To .PersonName.Length - 1

                                                auxPasajeros = New classPasajeros

                                                If Not String.IsNullOrEmpty(.PersonName(i).NameNumber) Then
                                                    auxPasajeros.NumeroPasajero = Convert.ToString(Convert.ToInt64(.PersonName(i).NameNumber.Split(Constantes.Punto)(0))) & _
                                                                                  Constantes.Punto & _
                                                                                  Convert.ToString(Convert.ToInt64(.PersonName(i).NameNumber.Split(Constantes.Punto)(1)))
                                                End If


                                                If Not String.IsNullOrEmpty(.PersonName(i).GivenName) Then
                                                    auxPasajeros.NombrePasajero = .PersonName(i).GivenName.ToString
                                                Else
                                                    If oCadenaError.ToString.IndexOf(Constantes.PasajeroSinNombre & " número " & auxPasajeros.NumeroPasajero) = -1 Then
                                                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                                        objPNR.MSGError.Add(Constantes.PasajeroSinNombre & " número " & auxPasajeros.NumeroPasajero)
                                                        oCadenaError.Append(Constantes.PasajeroSinNombre & " número " & auxPasajeros.NumeroPasajero)
                                                    End If
                                                End If

                                                If Not String.IsNullOrEmpty(.PersonName(i).Surname) Then
                                                    auxPasajeros.ApellidoPaterno = .PersonName(i).Surname.ToString
                                                Else
                                                    If oCadenaError.ToString.IndexOf(Constantes.PasajeroSinApellido & " número " & auxPasajeros.NumeroPasajero) = -1 Then
                                                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                                        objPNR.MSGError.Add(Constantes.PasajeroSinApellido & " número " & auxPasajeros.NumeroPasajero)
                                                        oCadenaError.Append(Constantes.PasajeroSinApellido & " número " & auxPasajeros.NumeroPasajero)
                                                    End If
                                                End If

                                                If Not String.IsNullOrEmpty(.PersonName(i).PassengerType) Then
                                                    auxPasajeros.TipoPasajero = .PersonName(i).PassengerType.ToString
                                                End If

                                                If Not String.IsNullOrEmpty(.PersonName(i).WithInfant) Then
                                                    auxPasajeros.Infante = .PersonName(i).WithInfant
                                                End If



                                                If .PersonName(i).NameReference IsNot Nothing Then
                                                    'Dim PRUEBA As String = Nothing
                                                    'PRUEBA = .PersonName(i).NameReference.Substring(0, 1)
                                                    'PRUEBA = Nothing
                                                    If .PersonName(i).NameReference.Substring(0, 1) = Constantes.Id_DNI Then

                                                        auxDocumento = New classDocumento
                                                        auxDocumento.Tipo = Constantes.Id_DNI
                                                        auxDocumento.Num_Nombre = Constantes.Nombre_DNI
                                                        auxDocumento.Numero = .PersonName(i).NameReference.Substring(1, .PersonName(i).NameReference.Length - 1)

                                                        If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                        objDocumento.Add(auxDocumento)
                                                        auxDocumento = Nothing

                                                        auxPasajeros.Documento = New List(Of classDocumento)
                                                        auxPasajeros.Documento = objDocumento
                                                        objDocumento = Nothing

                                                    ElseIf .PersonName(i).NameReference.Substring(0, 1) = Constantes.Id_PASS Then

                                                        auxDocumento = New classDocumento
                                                        auxDocumento.Tipo = Constantes.Id_PASS
                                                        auxDocumento.Num_Nombre = Constantes.Nombre_Pasaporte
                                                        auxDocumento.Numero = Trim(.PersonName(i).NameReference.Substring(1, .PersonName(i).NameReference.Length - 1))

                                                        If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                        objDocumento.Add(auxDocumento)
                                                        auxDocumento = Nothing

                                                        auxPasajeros.Documento = New List(Of classDocumento)
                                                        auxPasajeros.Documento = objDocumento
                                                        objDocumento = Nothing

                                                    ElseIf .PersonName(i).NameReference.Substring(0, 2) = Constantes.Id_CE Then

                                                        auxDocumento = New classDocumento
                                                        auxDocumento.Tipo = Constantes.Id_CE
                                                        auxDocumento.Num_Nombre = Constantes.Nombre_CE
                                                        auxDocumento.Numero = Trim(.PersonName(i).NameReference.Substring(2, .PersonName(i).NameReference.Length - 2))

                                                        If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                        objDocumento.Add(auxDocumento)
                                                        auxDocumento = Nothing

                                                        auxPasajeros.Documento = New List(Of classDocumento)
                                                        auxPasajeros.Documento = objDocumento
                                                        objDocumento = Nothing

                                                    ElseIf .PersonName(i).NameReference.Substring(0, 3) = Constantes.Id_RUC Then

                                                        auxDocumento = New classDocumento
                                                        auxDocumento.Tipo = Constantes.Id_RUC
                                                        auxDocumento.Num_Nombre = Constantes.Nombre_RUC
                                                        auxDocumento.Numero = .PersonName(i).NameReference.Substring(3, .PersonName(i).NameReference.Length - 3)

                                                        If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                        objDocumento.Add(auxDocumento)
                                                        auxDocumento = Nothing

                                                        auxPasajeros.Documento = New List(Of classDocumento)
                                                        auxPasajeros.Documento = objDocumento
                                                        objDocumento = Nothing

                                                    Else
                                                        auxDocumento = Nothing
                                                        auxPasajeros.Documento = Nothing
                                                    End If
                                                End If

                                                If objPasajeros Is Nothing Then objPasajeros = New List(Of classPasajeros)
                                                objPasajeros.Add(auxPasajeros)
                                                auxPasajeros = Nothing

                                            Next

                                            objPNR.Pasajeros = New List(Of classPasajeros)
                                            objPNR.Pasajeros = objPasajeros
                                            objPasajeros = Nothing

                                        End If
                                    End With
                                End If

                                '**** ASIENTOS ASIGNADOS ****


                                If TravelItineraryReadRS.TravelItinerary.ItineraryInfo IsNot Nothing Then
                                    If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems IsNot Nothing Then

                                        If objPNR IsNot Nothing Then
                                            For z As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                auxPax = objPNR.Pasajeros.Item(z).NumeroPasajero

                                                With TravelItineraryReadRS.TravelItinerary.ItineraryInfo
                                                    For i As Integer = 0 To .ReservationItems.Length - 1
                                                        If .ReservationItems(i).Seats IsNot Nothing Then
                                                            If .ReservationItems(i).Seats IsNot Nothing Then
                                                                For w As Integer = 0 To .ReservationItems(i).Seats.Length - 1

                                                                    If auxPax = CInt(.ReservationItems(i).Seats(w).NameNumber.Split(".")(0)) & "." & CInt(.ReservationItems(i).Seats(w).NameNumber.Split(".")(1)) Then

                                                                        auxAsientos = New classAsientos

                                                                        'SegmentNumber
                                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).Seats(w).SegmentNumber) Then
                                                                            auxAsientos.Segmento = .ReservationItems(i).Seats(w).SegmentNumber
                                                                        End If

                                                                        'Number
                                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).Seats(w).Number) Then
                                                                            auxAsientos.Number = .ReservationItems(i).Seats(w).Number
                                                                        End If

                                                                        'FlightSegment
                                                                        If .ReservationItems(i).Seats(w).FlightSegment IsNot Nothing Then

                                                                            'OriginLocation
                                                                            If .ReservationItems(i).Seats(w).FlightSegment.OriginLocation IsNot Nothing Then
                                                                                If Not String.IsNullOrEmpty(.ReservationItems(i).Seats(w).FlightSegment.OriginLocation.LocationCode) Then
                                                                                    auxAsientos.OriginLocation = .ReservationItems(i).Seats(w).FlightSegment.OriginLocation.LocationCode
                                                                                End If
                                                                            End If

                                                                            'DestinationLocation
                                                                            If .ReservationItems(i).Seats(w).FlightSegment.DestinationLocation IsNot Nothing Then
                                                                                If Not String.IsNullOrEmpty(.ReservationItems(i).Seats(w).FlightSegment.DestinationLocation.LocationCode) Then
                                                                                    auxAsientos.DestinationLocation = .ReservationItems(i).Seats(w).FlightSegment.DestinationLocation.LocationCode
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If objPNR.Pasajeros.Item(z).Asientos Is Nothing Then objPNR.Pasajeros.Item(z).Asientos = New List(Of classAsientos)
                                                                        objPNR.Pasajeros.Item(z).Asientos.Add(auxAsientos)
                                                                    End If

                                                                Next
                                                            End If
                                                        End If
                                                    Next
                                                End With
                                            Next
                                        End If
                                    End If
                                End If

                                '****************************


                                '***** SEGMENTOS *****
                                If TravelItineraryReadRS.TravelItinerary.ItineraryInfo IsNot Nothing Then
                                    If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems IsNot Nothing Then
                                        With TravelItineraryReadRS.TravelItinerary.ItineraryInfo
                                            For i As Integer = 0 To .ReservationItems.Length - 1
                                                If .ReservationItems(i).FlightSegment IsNot Nothing Then

                                                    'If Not .ReservationItems(i).FlightSegment(0).IsPast Then

                                                    auxSegmentos = New classSegmentos
                                                    auxSegmentos.RPH = .ReservationItems(i).RPH.ToString
                                                    auxSegmentos.Segmento = intContadorSegmento


                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).MarketingAirline.Code) Then
                                                        auxSegmentos.Aerolinea = .ReservationItems(i).FlightSegment(0).MarketingAirline.Code
                                                    End If

                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).FlightNumber) Then
                                                        auxSegmentos.NumVuelo = .ReservationItems(i).FlightSegment(0).FlightNumber
                                                    End If

                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).ResBookDesigCode) Then
                                                        auxSegmentos.ClaseServicio = .ReservationItems(i).FlightSegment(0).ResBookDesigCode
                                                    End If

                                                    'Equipment
                                                    If .ReservationItems(i).FlightSegment(0).Equipment IsNot Nothing Then
                                                        auxSegmentos.Equipo = .ReservationItems(i).FlightSegment(0).Equipment.AirEquipType.ToString
                                                        objDAO = New DAO
                                                        auxSegmentos.NombreEquipo = objDAO.ObtenerNombreEquipo(auxSegmentos.Equipo, strCodigoSeguimiento, intFirmaDB, intEsquema)
                                                    End If

                                                    '2014-08-10T10:20:00
                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).DepartureDateTime) Then
                                                        auxSegmentos.FechaHoraSalida = .ReservationItems(i).FlightSegment(0).DepartureDateTime.ToString
                                                        oDate = New Date
                                                        oDate = .ReservationItems(i).FlightSegment(0).DepartureDateTime.Split("T")(0)
                                                        oMesDeparture = oDate.Month.ToString
                                                        oAnoDeparture = oDate.Year.ToString

                                                    End If


                                                    '08-10T17:10:00
                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).ArrivalDateTime) Then
                                                        AuxArrivalDateTime = .ReservationItems(i).FlightSegment(0).ArrivalDateTime.ToString

                                                        If AuxArrivalDateTime.Split("T")(0).Length = 5 Then

                                                            AuxArrivalDateTime = oAnoDeparture & "-" & AuxArrivalDateTime

                                                            oDate = New Date
                                                            oDate = AuxArrivalDateTime.Split("T")(0)
                                                            oMesArrival = oDate.Month

                                                            If CInt(oMesDeparture) > CInt(oMesArrival) Then
                                                                auxSegmentos.FechaHoraLlegada = CStr(CInt(oAnoDeparture) + 1) & "-" & .ReservationItems(i).FlightSegment(0).ArrivalDateTime.ToString
                                                            Else
                                                                auxSegmentos.FechaHoraLlegada = oAnoDeparture & "-" & .ReservationItems(i).FlightSegment(0).ArrivalDateTime.ToString
                                                            End If

                                                        Else
                                                            auxSegmentos.FechaHoraLlegada = .ReservationItems(i).FlightSegment(0).ArrivalDateTime.ToString
                                                        End If

                                                    End If

                                                    '08-10T17:10:00
                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).UpdatedDepartureTime) Then

                                                        AuxUpdatedDepartureTime = .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime

                                                        If AuxUpdatedDepartureTime.Split("T")(0).Length = 5 Then

                                                            oDate = New Date
                                                            oDate = auxSegmentos.FechaHoraSalida
                                                            oMesArrival = oDate.Month
                                                            oAnoArrival = oDate.Year

                                                            oDate = New Date
                                                            oDate = oAnoArrival & "-" & AuxUpdatedDepartureTime
                                                            oAuxMesUP = oDate.Month

                                                            If CInt(oAuxMesUP) < CInt(oMesArrival) Then
                                                                auxSegmentos.UpDateFechaHoraSalida = CStr(CInt(oAnoArrival) + 1) & "-" & .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime.ToString
                                                                auxSegmentos.FechaHoraSalida = CStr(CInt(oAnoArrival) + 1) & "-" & .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime.ToString
                                                            Else
                                                                auxSegmentos.UpDateFechaHoraSalida = oAnoDeparture & "-" & .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime.ToString
                                                                auxSegmentos.FechaHoraSalida = oAnoDeparture & "-" & .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime.ToString
                                                            End If

                                                        Else
                                                            auxSegmentos.UpDateFechaHoraSalida = .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime
                                                            auxSegmentos.FechaHoraSalida = .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime
                                                        End If

                                                    End If

                                                    '08-10T17:10:00
                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).UpdatedArrivalTime) Then
                                                        AuxUpdatedArrivalTime = .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime

                                                        If AuxUpdatedArrivalTime.Split("T")(0).Length = 5 Then

                                                            oDate = New Date
                                                            oDate = auxSegmentos.FechaHoraLlegada
                                                            oMesArrival = oDate.Month
                                                            oAnoArrival = oDate.Year

                                                            oDate = New Date
                                                            oDate = oAnoArrival & "-" & AuxUpdatedArrivalTime
                                                            oAuxMesUP = oDate.Month


                                                            If CInt(oAuxMesUP) < CInt(oMesArrival) Then
                                                                auxSegmentos.UpDateFechaHoraLlegada = CStr(CInt(oAnoArrival) + 1) & "-" & .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime.ToString
                                                                auxSegmentos.FechaHoraLlegada = CStr(CInt(oAnoArrival) + 1) & "-" & .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime.ToString
                                                            Else
                                                                auxSegmentos.UpDateFechaHoraLlegada = oAnoDeparture & "-" & .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime.ToString
                                                                auxSegmentos.FechaHoraLlegada = oAnoDeparture & "-" & .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime.ToString
                                                            End If

                                                        Else
                                                            auxSegmentos.UpDateFechaHoraLlegada = .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime
                                                            auxSegmentos.FechaHoraLlegada = .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime
                                                        End If

                                                    End If

                                                    If .ReservationItems(i).FlightSegment(0).OriginLocation.LocationCode IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).OriginLocation.LocationCode) Then

                                                            objDAO = New DAO
                                                            objCiudad = New classCiudad
                                                            objCiudad = objDAO.ObtenerDatosCiudad(.ReservationItems(i).FlightSegment(0).OriginLocation.LocationCode, _
                                                                                                           strCodigoSeguimiento, _
                                                                                                           intFirmaDB, _
                                                                                                           intEsquema)


                                                            auxSegmentos.Salida = New classCiudad
                                                            auxSegmentos.Salida = objCiudad


                                                        End If
                                                    End If

                                                    If .ReservationItems(i).FlightSegment(0).DestinationLocation.LocationCode IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).DestinationLocation.LocationCode) Then

                                                            objDAO = New DAO
                                                            objCiudad = New classCiudad

                                                            objCiudad = objDAO.ObtenerDatosCiudad(.ReservationItems(i).FlightSegment(0).DestinationLocation.LocationCode, _
                                                                                                           strCodigoSeguimiento, _
                                                                                                           intFirmaDB, _
                                                                                                           intEsquema)


                                                            auxSegmentos.Llegada = New classCiudad
                                                            auxSegmentos.Llegada = objCiudad
                                                        End If
                                                    End If

                                                    If .ReservationItems(i).FlightSegment(0).OperatingAirline IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).OperatingAirline(0).Code) Then
                                                            auxSegmentos.OperadoPor = .ReservationItems(i).FlightSegment(0).OperatingAirline(0).Code
                                                        ElseIf Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).OperatingAirline(0).CompanyShortName) Then
                                                            auxSegmentos.OperadoPor = .ReservationItems(i).FlightSegment(0).OperatingAirline(0).CompanyShortName
                                                        End If

                                                        If Not String.IsNullOrEmpty(auxSegmentos.OperadoPor) Then
                                                            auxSegmentos.OperadoPor = ObtieneCodigoOperador(auxSegmentos.OperadoPor, _
                                                                                                            strCodigoSeguimiento, _
                                                                                                            intFirmaDB, _
                                                                                                            intEsquema)
                                                        End If
                                                    End If

                                                    If .ReservationItems(i).FlightSegment(0).SupplierRef IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).SupplierRef.ID) Then
                                                            If .ReservationItems(i).FlightSegment(0).SupplierRef.ID.Contains(Constantes.Asterisco) Then
                                                                auxSegmentos.CodigoAerolinea = .ReservationItems(i).FlightSegment(0).SupplierRef.ID.Split(Constantes.Asterisco)(1).ToString
                                                            Else
                                                                auxSegmentos.CodigoAerolinea = .ReservationItems(i).FlightSegment(0).SupplierRef.ID.ToString
                                                            End If
                                                        End If
                                                    Else
                                                        If oCadenaError.ToString.IndexOf(Constantes.LocalizaroLineaAerea) = -1 Then
                                                            If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                                            objPNR.MSGError.Add(Constantes.LocalizaroLineaAerea)
                                                            oCadenaError.Append(Constantes.LocalizaroLineaAerea)
                                                        End If
                                                    End If


                                                    If .ReservationItems(i).FlightSegment(0).Status IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).Status.ToString) Then
                                                            auxSegmentos.Status = .ReservationItems(i).FlightSegment(0).Status.ToString
                                                            If Not .ReservationItems(i).FlightSegment(0).Status.ToString.Equals(Constantes.IWS_STATUS_SEGMENT) Then
                                                                'If oCadenaError.ToString.IndexOf(Constantes.StatusDiferente) = -1 Then
                                                                'If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                                                'objPNR.MSGError.Add(Constantes.StatusDiferente)
                                                                'oCadenaError.Append(Constantes.StatusDiferente)
                                                                'End If
                                                            End If
                                                        End If
                                                    End If

                                                    If objSegmentos Is Nothing Then objSegmentos = New List(Of classSegmentos)


                                                    If Not String.IsNullOrEmpty(auxSegmentos.FechaHoraSalida) Then auxSegmentos.FechaHoraSalida = CType(auxSegmentos.FechaHoraSalida.ToString.Replace("T", " "), Date).ToString(Constantes.IWS_DATE_FORMAT_INSERT_2)
                                                    If Not String.IsNullOrEmpty(auxSegmentos.FechaHoraLlegada) Then auxSegmentos.FechaHoraLlegada = CType(auxSegmentos.FechaHoraLlegada.ToString.Replace("T", " "), Date).ToString(Constantes.IWS_DATE_FORMAT_INSERT_2)

                                                    If Not String.IsNullOrEmpty(auxSegmentos.UpDateFechaHoraSalida) Then auxSegmentos.UpDateFechaHoraSalida = CType(auxSegmentos.UpDateFechaHoraSalida.ToString.Replace("T", " "), Date).ToString(Constantes.IWS_DATE_FORMAT_INSERT_2)
                                                    If Not String.IsNullOrEmpty(auxSegmentos.UpDateFechaHoraLlegada) Then auxSegmentos.UpDateFechaHoraLlegada = CType(auxSegmentos.UpDateFechaHoraLlegada.ToString.Replace("T", " "), Date).ToString(Constantes.IWS_DATE_FORMAT_INSERT_2)

                                                    objSegmentos.Add(auxSegmentos)
                                                    auxSegmentos = Nothing


                                                    intContadorSegmento += 1

                                                    'End If
                                                End If

                                            Next

                                            objPNR.Segmentos = New List(Of classSegmentos)
                                            objPNR.Segmentos = objSegmentos
                                            objSegmentos = Nothing
                                        End With
                                    Else
                                        If oCadenaError.ToString.IndexOf(Constantes.NoExisteSegmentoActivo) = -1 Then
                                            If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                            objPNR.MSGError.Add(Constantes.NoExisteSegmentoActivo)
                                            oCadenaError.Append(Constantes.NoExisteSegmentoActivo)
                                        End If
                                    End If
                                End If


                                '*******SSR - DOCS *******
                                If TravelItineraryReadRS.TravelItinerary.SpecialServiceInfo IsNot Nothing Then
                                    With TravelItineraryReadRS.TravelItinerary
                                        For i As Integer = 0 To .SpecialServiceInfo.Length - 1
                                            If .SpecialServiceInfo(i).Service IsNot Nothing Then
                                                If .SpecialServiceInfo(i).Service.SSR_Code IsNot Nothing And .SpecialServiceInfo(i).Service.SSR_Type IsNot Nothing Then

                                                    Dim strNumeroPasajero As String = Nothing
                                                    Dim strNombrePasajero As String = Nothing
                                                    Dim strTexto As String = Nothing
                                                    Dim strAuxTexto As String = Nothing
                                                    Dim strFechaNacimiento As String = Nothing
                                                    Dim strFechaExpiracion As String = Nothing
                                                    Dim strApellidoPaterno As String = Nothing
                                                    Dim strNacionalidad As String = Nothing
                                                    Dim strNacPasaporte As String = Nothing
                                                    Dim strPasaporte As String = Nothing
                                                    Dim strNacPasajero As String = Nothing
                                                    Dim strNombre As String = Nothing
                                                    Dim strGenero As String = Nothing
                                                    Dim strIndInfante As String = Nothing


                                                    If .SpecialServiceInfo(i).Service.SSR_Code.ToString = "SSR" And _
                                                       .SpecialServiceInfo(i).Service.SSR_Type.ToString = "DOCS" Then

                                                        With .SpecialServiceInfo(i).Service
                                                            strNumeroPasajero = Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(0))) & _
                                                                             Constantes.Punto & _
                                                                             Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(1)))

                                                            strNombrePasajero = .PersonName(0).Value
                                                            strAuxTexto = .Text.ToString
                                                        End With


                                                        'DB/04AUG78/M/PANDURO/OMAR
                                                        If strAuxTexto.IndexOf("DB/") > -1 Then
                                                            strFechaNacimiento = Nothing
                                                            strApellidoPaterno = Nothing
                                                            strNombre = Nothing
                                                            strGenero = Nothing

                                                            strTexto = strAuxTexto.Substring(strAuxTexto.IndexOf("DB/"), Len(strAuxTexto) - strAuxTexto.IndexOf("DB/"))

                                                            strFechaNacimiento = strTexto.Split("/")(1)
                                                            strGenero = strTexto.Split("/")(2)
                                                            If Len(strGenero) = 1 Then
                                                                strIndInfante = Nothing
                                                            Else
                                                                strIndInfante = strGenero.Substring(1, 1)
                                                                strGenero = strGenero.Substring(0, 1)
                                                            End If

                                                            strApellidoPaterno = strTexto.Split("/")(3)
                                                            strNombre = strTexto.Split("/")(4)

                                                            If strNombrePasajero = strApellidoPaterno & "/" & strNombre Then
                                                                For x As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                                    If strNumeroPasajero = objPNR.Pasajeros.Item(x).NumeroPasajero Then
                                                                        If Not String.IsNullOrEmpty(strFechaNacimiento) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            oDate = New Date
                                                                            oDate = CDate(strFechaNacimiento)
                                                                            objPNR.Pasajeros.Item(x).SSR.FechaNacimiento = oDate.ToString("dd/MM/yyyy")
                                                                        End If
                                                                        If Not String.IsNullOrEmpty(strGenero) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.Genero = strGenero
                                                                        End If
                                                                    End If
                                                                Next



                                                            ElseIf Not String.IsNullOrEmpty(strIndInfante) Then
                                                                If strIndInfante = "I" Then
                                                                    For x As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                                        If strNombrePasajero = objPNR.Pasajeros.Item(x).ApellidoPaterno & "/" & objPNR.Pasajeros.Item(x).NombrePasajero Then
                                                                            If Not objPNR.Pasajeros.Item(x).Infante Then
                                                                                objPNR.Pasajeros.Item(x).Infante = True
                                                                                If Not String.IsNullOrEmpty(strFechaNacimiento) Then
                                                                                    If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                    oDate = New Date
                                                                                    oDate = CDate(strFechaNacimiento)
                                                                                    objPNR.Pasajeros.Item(x).SSR.FechaNacimiento = oDate.ToString("dd/MM/yyyy")
                                                                                End If
                                                                                If Not String.IsNullOrEmpty(strGenero) Then
                                                                                    If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                    objPNR.Pasajeros.Item(x).SSR.Genero = strGenero
                                                                                End If
                                                                            End If
                                                                        End If
                                                                    Next
                                                                End If
                                                            End If


                                                        ElseIf strAuxTexto.IndexOf("P/") > -1 Then
                                                            'P/PE/45213321/PE/20MAR79/M/20MAR11/DE LA CRUZ/ROLANDO

                                                            strTexto = strAuxTexto.Substring(strAuxTexto.IndexOf("P/"), Len(strAuxTexto) - strAuxTexto.IndexOf("P/"))

                                                            strNacPasaporte = strTexto.Split("/")(1)
                                                            strPasaporte = strTexto.Split("/")(2)
                                                            strNacPasajero = strTexto.Split("/")(3)
                                                            strFechaNacimiento = strTexto.Split("/")(4)
                                                            strGenero = strTexto.Split("/")(5)
                                                            If strGenero.Length > 1 Then strIndInfante = strGenero.Substring(1, 1)
                                                            strFechaExpiracion = strTexto.Split("/")(6)
                                                            strApellidoPaterno = strTexto.Split("/")(7)
                                                            strNombre = strTexto.Split("/")(8)


                                                            If strNombrePasajero = strApellidoPaterno & "/" & strNombre Then
                                                                For x As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                                    If strNumeroPasajero = objPNR.Pasajeros.Item(x).NumeroPasajero Then
                                                                        oDate = New Date
                                                                        oDate = CDate(strFechaNacimiento)
                                                                        If Not String.IsNullOrEmpty(strFechaNacimiento) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            oDate = New Date
                                                                            oDate = CDate(strFechaNacimiento)
                                                                            objPNR.Pasajeros.Item(x).SSR.FechaNacimiento = oDate.ToString("dd/MM/yyyy")
                                                                        End If
                                                                        If Not String.IsNullOrEmpty(strGenero) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.Genero = strGenero
                                                                        End If
                                                                        If Not String.IsNullOrEmpty(strFechaExpiracion) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.FechaExpiracion = strFechaExpiracion
                                                                        End If
                                                                        '---
                                                                        If Not String.IsNullOrEmpty(strNacPasaporte) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.NacPasaporte = strNacPasaporte
                                                                        End If
                                                                        If Not String.IsNullOrEmpty(strPasaporte) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.Pasaporte = strPasaporte
                                                                        End If
                                                                        If Not String.IsNullOrEmpty(strNacPasajero) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.NacPasajero = strNacPasajero
                                                                        End If
                                                                    End If
                                                                Next
                                                            ElseIf Not String.IsNullOrEmpty(strIndInfante) Then
                                                                If strIndInfante = "I" Then
                                                                    For x As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                                        If strNombrePasajero = objPNR.Pasajeros.Item(x).ApellidoPaterno & "/" & objPNR.Pasajeros.Item(x).NombrePasajero Then

                                                                            objPNR.Pasajeros.Item(x).Infante = True
                                                                            If Not String.IsNullOrEmpty(strFechaNacimiento) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                oDate = New Date
                                                                                oDate = CDate(strFechaNacimiento)
                                                                                objPNR.Pasajeros.Item(x).SSR.FechaNacimiento = oDate.ToString("dd/MM/yyyy")
                                                                            End If
                                                                            If Not String.IsNullOrEmpty(strGenero) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                objPNR.Pasajeros.Item(x).SSR.Genero = strGenero
                                                                            End If
                                                                            If Not String.IsNullOrEmpty(strFechaExpiracion) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                objPNR.Pasajeros.Item(x).SSR.FechaExpiracion = strFechaExpiracion
                                                                            End If
                                                                            '-----
                                                                            If Not String.IsNullOrEmpty(strNacPasaporte) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                objPNR.Pasajeros.Item(x).SSR.NacPasaporte = strNacPasaporte
                                                                            End If
                                                                            If Not String.IsNullOrEmpty(strPasaporte) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                objPNR.Pasajeros.Item(x).SSR.Pasaporte = strPasaporte
                                                                            End If
                                                                            If Not String.IsNullOrEmpty(strNacPasajero) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                objPNR.Pasajeros.Item(x).SSR.NacPasajero = strNacPasajero
                                                                            End If
                                                                        End If

                                                                    Next
                                                                End If
                                                            End If

                                                        End If
                                                    End If


                                                    If .SpecialServiceInfo(i).Service.SSR_Code.ToString = "SSR" And _
                                                       .SpecialServiceInfo(i).Service.SSR_Type.ToString = "INFT" Then


                                                        With .SpecialServiceInfo(i).Service
                                                            strNumeroPasajero = Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(0))) & _
                                                                             Constantes.Punto & _
                                                                             Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(1)))

                                                            strNombrePasajero = .PersonName(0).Value
                                                            strAuxTexto = .Text(0).ToString
                                                        End With


                                                        Dim Parceo() As String = strAuxTexto.Split("/")

                                                        oDate = New Date
                                                        oDate = CDate(Parceo(Parceo.Length - 1).Substring(0, 7))
                                                        strFechaNacimiento = oDate.ToString("dd/MM/yyyy")
                                                        strNombre = Parceo(Parceo.Length - 2)
                                                        strApellidoPaterno = Parceo(Parceo.Length - 3)

                                                        For x As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                            If strApellidoPaterno & Constantes.Slash & strNombre = objPNR.Pasajeros.Item(x).ApellidoPaterno & Constantes.Slash & objPNR.Pasajeros.Item(x).NombrePasajero Then
                                                                objPNR.Pasajeros.Item(x).Infante = True
                                                                If Not String.IsNullOrEmpty(strFechaNacimiento) Then
                                                                    If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                    oDate = New Date
                                                                    oDate = CDate(strFechaNacimiento)
                                                                    objPNR.Pasajeros.Item(x).SSR.FechaNacimiento = strFechaNacimiento
                                                                    objPNR.Pasajeros.Item(x).AdultoAsociado = strNumeroPasajero
                                                                    Exit For
                                                                End If
                                                            End If
                                                        Next

                                                    End If


                                                    If .SpecialServiceInfo(i).Service.SSR_Code.ToString = "SSR" And _
                                                       .SpecialServiceInfo(i).Service.SSR_Type.ToString = "FOID" Then


                                                        With .SpecialServiceInfo(i).Service
                                                            strNumeroPasajero = Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(0))) & _
                                                                             Constantes.Punto & _
                                                                             Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(1)))

                                                            strNombrePasajero = .PersonName(0).Value
                                                            strAuxTexto = .Text(0).Split("/")(1)
                                                        End With

                                                        For x As Integer = 0 To objPNR.Pasajeros.Count - 1

                                                            If strNumeroPasajero = objPNR.Pasajeros.Item(x).NumeroPasajero Then
                                                                If strAuxTexto.Substring(0, 2) = "NI" Then
                                                                    If strAuxTexto.Substring(0, 4) = "NICE" Then
                                                                        If objPNR.Pasajeros.Item(x).Documento Is Nothing Then

                                                                            auxDocumento = New classDocumento
                                                                            auxDocumento.Numero = strAuxTexto.Replace("NICE", "")
                                                                            auxDocumento.Tipo = Constantes.Id_CE
                                                                            auxDocumento.Num_Nombre = Constantes.Nombre_CE

                                                                            If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                                            objDocumento.Add(auxDocumento)

                                                                            If objPNR.Pasajeros.Item(x).Documento Is Nothing Then objPNR.Pasajeros.Item(x).Documento = New List(Of classDocumento)
                                                                            objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                            auxDocumento = Nothing
                                                                        Else

                                                                            Dim flag As Boolean = False

                                                                            auxDocumento = New classDocumento
                                                                            auxDocumento.Numero = strAuxTexto.Replace("NICE", "")
                                                                            auxDocumento.Tipo = Constantes.Id_CE
                                                                            auxDocumento.Num_Nombre = Constantes.Nombre_CE

                                                                            For y As Integer = 0 To objPNR.Pasajeros.Item(x).Documento.Count - 1
                                                                                If objPNR.Pasajeros.Item(x).Documento.Item(y).Tipo = auxDocumento.Tipo Then
                                                                                    flag = True
                                                                                    Exit For
                                                                                End If
                                                                            Next

                                                                            If Not flag Then
                                                                                objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                                auxDocumento = Nothing
                                                                            End If
                                                                        End If

                                                                    Else

                                                                        If objPNR.Pasajeros.Item(x).Documento Is Nothing Then
                                                                            auxDocumento = New classDocumento
                                                                            auxDocumento.Numero = strAuxTexto.Replace("NI", "")
                                                                            auxDocumento.Tipo = Constantes.Id_DNI
                                                                            auxDocumento.Num_Nombre = Constantes.Nombre_DNI

                                                                            If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                                            objDocumento.Add(auxDocumento)

                                                                            If objPNR.Pasajeros.Item(x).Documento Is Nothing Then objPNR.Pasajeros.Item(x).Documento = New List(Of classDocumento)
                                                                            objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                            auxDocumento = Nothing
                                                                        Else
                                                                            Dim flag As Boolean = False

                                                                            auxDocumento = New classDocumento
                                                                            auxDocumento.Numero = strAuxTexto.Replace("NI", "")
                                                                            auxDocumento.Tipo = Constantes.Id_DNI
                                                                            auxDocumento.Num_Nombre = Constantes.Nombre_DNI

                                                                            For y As Integer = 0 To objPNR.Pasajeros.Item(x).Documento.Count - 1
                                                                                If objPNR.Pasajeros.Item(x).Documento.Item(y).Tipo = auxDocumento.Tipo Then
                                                                                    flag = True
                                                                                    Exit For
                                                                                End If
                                                                            Next

                                                                            If Not flag Then
                                                                                objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                                auxDocumento = Nothing
                                                                            End If

                                                                        End If

                                                                    End If

                                                                ElseIf strAuxTexto.Substring(0, 2) = "ID" Then

                                                                    If objPNR.Pasajeros.Item(x).Documento Is Nothing Then
                                                                        auxDocumento = New classDocumento
                                                                        auxDocumento.Numero = strAuxTexto.Replace("IDPP", "")
                                                                        auxDocumento.Tipo = Constantes.Id_PASS
                                                                        auxDocumento.Num_Nombre = Constantes.Nombre_Pasaporte

                                                                        If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                                        objDocumento.Add(auxDocumento)

                                                                        If objPNR.Pasajeros.Item(x).Documento Is Nothing Then objPNR.Pasajeros.Item(x).Documento = New List(Of classDocumento)
                                                                        objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                        auxDocumento = Nothing
                                                                    Else
                                                                        Dim flag As Boolean = False

                                                                        auxDocumento = New classDocumento
                                                                        auxDocumento.Numero = strAuxTexto.Replace("IDPP", "")
                                                                        auxDocumento.Tipo = Constantes.Id_PASS
                                                                        auxDocumento.Num_Nombre = Constantes.Nombre_Pasaporte

                                                                        For y As Integer = 0 To objPNR.Pasajeros.Item(x).Documento.Count - 1
                                                                            If objPNR.Pasajeros.Item(x).Documento.Item(y).Tipo = auxDocumento.Tipo Then
                                                                                flag = True
                                                                                Exit For
                                                                            End If
                                                                        Next

                                                                        If Not flag Then
                                                                            objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                            auxDocumento = Nothing
                                                                        End If

                                                                    End If

                                                                End If
                                                                Exit For
                                                            End If
                                                        Next
                                                    End If


                                                    strNumeroPasajero = Nothing
                                                    strNombrePasajero = Nothing
                                                    strTexto = Nothing
                                                    strAuxTexto = Nothing
                                                    strFechaNacimiento = Nothing
                                                    strFechaExpiracion = Nothing
                                                    strApellidoPaterno = Nothing
                                                    strNacionalidad = Nothing
                                                    strNombre = Nothing
                                                    strGenero = Nothing
                                                    strIndInfante = Nothing
                                                    oDate = Nothing


                                                End If
                                            End If
                                        Next

                                    End With
                                End If

                                '***** BOLETOS EMITIDOS ****

                                If TravelItineraryReadRS IsNot Nothing Then
                                    If TravelItineraryReadRS.TravelItinerary IsNot Nothing Then
                                        If TravelItineraryReadRS.TravelItinerary.ItineraryInfo IsNot Nothing Then
                                            If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.Ticketing IsNot Nothing Then
                                                With TravelItineraryReadRS.TravelItinerary.ItineraryInfo
                                                    For i As Integer = 0 To .Ticketing.Length - 1


                                                        If .Ticketing(i).eTicketNumber IsNot Nothing Then

                                                            If objPNR.Boletos Is Nothing Then objPNR.Boletos = New List(Of classPNR.classBoletos)
                                                            auxBoleto = New classPNR.classBoletos

                                                            If Not String.IsNullOrEmpty(.Ticketing(i).RPH) Then
                                                                auxBoleto.ID = .Ticketing(i).RPH
                                                            End If

                                                            If Not String.IsNullOrEmpty(.Ticketing(i).eTicketNumber) Then
                                                                auxBoleto.Ticket = .Ticketing(i).eTicketNumber
                                                            End If

                                                            objPNR.Boletos.Add(auxBoleto)
                                                            auxBoleto = Nothing

                                                        End If

                                                    Next
                                                End With

                                            End If
                                        End If
                                    End If
                                End If

                            End If
                        End If


                    Else

                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                        objPNR.MSGError.Add("Se produjo un error al momento de recuperar el código")

                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strPNR = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objSession = Nothing
                objIWebServices = Nothing
                objIWebServices = Nothing
                intContadorSegmento = Nothing

                auxAsientos = Nothing
                auxPax = Nothing
            End Try

            Return objPNR
        End Function
        Public Function RecuperarPNRSinRestriccion(ByVal strPNR As String, _
                                                    ByVal strTypeRemark As String, _
                                                    ByVal strCodigoSeguimiento As String, _
                                                    ByVal intGDS As Integer, _
                                                    ByVal intFirmaGDS As Integer, _
                                                    ByVal intFirmaDB As Integer, _
                                                    ByVal intEsquema As Integer, _
                                                    ByVal objSession As classSession, Optional ByVal EsConsumidoxRobotVoid As Boolean = False) As classPNR

            Dim TravelItineraryReadRS As objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRS = Nothing
            Dim objPNR As classPNR = Nothing
            Dim objPasajeros As List(Of classPasajeros) = Nothing
            Dim auxPasajeros As classPasajeros = Nothing

            Dim objSegmentos As List(Of classSegmentos) = Nothing
            Dim auxSegmentos As classSegmentos = Nothing

            Dim objRemarks As List(Of classRemark) = Nothing
            Dim auxRemarks As classRemark = Nothing

            Dim objDocumento As New List(Of classDocumento)
            Dim auxDocumento As classDocumento = Nothing
            Dim oCadenaError As New System.Text.StringBuilder
            oCadenaError.Append("")

            Dim SabreCommandLLSRS As objSabreWS.SabreCommand.SabreCommandLLSRS = Nothing
            Dim EndTransactionRS As objSabreWS.EndTransaction.EndTransactionRS = Nothing

            Dim oDate As Date = Nothing
            Dim oAuxMesUP As String = Nothing
            Dim oMesDeparture As String = Nothing
            Dim oAnoDeparture As String = Nothing
            Dim oAnoArrival As String = Nothing
            Dim oMesArrival As String = Nothing
            Dim AuxArrivalDateTime As String = Nothing
            Dim AuxUpdatedDepartureTime As String = Nothing
            Dim AuxUpdatedArrivalTime As String = Nothing

            Dim objDAO As DAO = Nothing
            Dim objCiudad As classCiudad = Nothing

            'Dim objWSBusiness As WSBusiness = Nothing
            Dim intContadorSegmento As Integer = 1

            Dim auxAsientos As classAsientos = Nothing
            Dim auxPax As String = Nothing

            Dim auxBoleto As classPNR.classBoletos = Nothing

            Try

                If Not String.IsNullOrEmpty(strPNR) Then


                    objIWebServices = New IWebServices



                    TravelItineraryReadRS = objIWebServices._TravelItineraryReadRQ360(strPNR, _
                                                                                      strTypeRemark, _
                                                                                    strCodigoSeguimiento, _
                                                                                    intGDS, _
                                                                                    intFirmaGDS, _
                                                                                    intFirmaDB, _
                                                                                    objSession)


                    If TravelItineraryReadRS IsNot Nothing Then


                        If TravelItineraryReadRS.ApplicationResults IsNot Nothing Then
                            objPNR = New classPNR

                            If TravelItineraryReadRS.ApplicationResults.Error IsNot Nothing Then
                                For i As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Error.Length - 1
                                    For x As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                        For y As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message.Length - 1
                                            objPNR.MSGError.Add("ERROR : " & TravelItineraryReadRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(y).Value.ToString())
                                        Next
                                    Next
                                Next
                            End If

                            If TravelItineraryReadRS.ApplicationResults.Warning IsNot Nothing Then
                                For i As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Warning.Length - 1
                                    For x As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                        For y As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message.Length - 1
                                            objPNR.MSGError.Add("WARNING : " & TravelItineraryReadRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message(y).Value.ToString())
                                        Next
                                    Next
                                Next
                            End If


                            If TravelItineraryReadRS.TravelItinerary IsNot Nothing Then
                                Dim SegmentNumber As String = Nothing

                                Dim _ArrivalDateTime As String
                                Dim _ArrivalAirportLocationCode As String
                                Dim _UpdatedArrivalTime As String
                                Dim InfoItem() As objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRSTravelItineraryItineraryInfoItem = Nothing

                                Dim Seat() As objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRSTravelItineraryItineraryInfoItemSeat = Nothing

                                Dim oFlag As Boolean = False

                                If EsConsumidoxRobotVoid Then
                                    GoTo ConsumeRobotVoid
                                End If


                                '****** REORDENAMOS LOS VUELOS ********
                                If TravelItineraryReadRS.TravelItinerary.ItineraryInfo IsNot Nothing Then

                                    If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems IsNot Nothing Then

                                        For i As Integer = 0 To TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems.Length - 1

                                            '========================================================
                                            If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).Seats IsNot Nothing Then
                                                If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).Seats IsNot Nothing Then

                                                    For j As Integer = 0 To TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).Seats.Length - 1

                                                        If Seat Is Nothing Then
                                                            ReDim Seat(0)
                                                        Else
                                                            ReDim Preserve Seat(Seat.Length)
                                                        End If

                                                        Seat(Seat.Length - 1) = New objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRSTravelItineraryItineraryInfoItemSeat
                                                        Seat(Seat.Length - 1) = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).Seats(j)

                                                    Next

                                                End If
                                            End If

                                            If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment IsNot Nothing Then

                                                If Not TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).IsPast Then


                                                    If Not oFlag Then

                                                        SegmentNumber = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).SegmentNumber.Trim

                                                        ReDim InfoItem(0)
                                                        InfoItem(0) = New objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRSTravelItineraryItineraryInfoItem
                                                        InfoItem(0).RPH = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).RPH
                                                        ReDim InfoItem(0).FlightSegment(0)
                                                        InfoItem(0).FlightSegment(0) = New objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRSTravelItineraryItineraryInfoItemFlightSegment
                                                        InfoItem(0).FlightSegment = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment

                                                        oFlag = True
                                                        i += 1

                                                    End If

                                                    If i < TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems.Length - 1 Then
                                                        If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment IsNot Nothing Then

                                                            If SegmentNumber = CStr(CInt(TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).SegmentNumber.Trim)) Then

                                                                _ArrivalDateTime = Nothing
                                                                _ArrivalAirportLocationCode = Nothing
                                                                _UpdatedArrivalTime = Nothing

                                                                _ArrivalDateTime = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).ArrivalDateTime
                                                                _ArrivalAirportLocationCode = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).DestinationLocation.LocationCode.Trim
                                                                If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).UpdatedArrivalTime IsNot Nothing Then
                                                                    _UpdatedArrivalTime = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).UpdatedArrivalTime.Trim
                                                                End If

                                                                InfoItem(InfoItem.Length - 1).FlightSegment(0).ArrivalDateTime = _ArrivalDateTime
                                                                InfoItem(InfoItem.Length - 1).FlightSegment(0).DestinationLocation.LocationCode = _ArrivalAirportLocationCode
                                                                InfoItem(InfoItem.Length - 1).FlightSegment(0).UpdatedArrivalTime = _UpdatedArrivalTime

                                                            Else
                                                                ReDim Preserve InfoItem(InfoItem.Length)
                                                                InfoItem(InfoItem.Length - 1) = New objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRSTravelItineraryItineraryInfoItem
                                                                InfoItem(InfoItem.Length - 1).RPH = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).RPH.Trim
                                                                ReDim InfoItem(InfoItem.Length - 1).FlightSegment(0)
                                                                InfoItem(InfoItem.Length - 1).FlightSegment(0) = New objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRSTravelItineraryItineraryInfoItemFlightSegment
                                                                InfoItem(InfoItem.Length - 1).FlightSegment = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment

                                                            End If

                                                            SegmentNumber = CStr(CInt(TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).SegmentNumber.Trim))

                                                        End If
                                                    End If

                                                End If

                                            End If

                                            '========================================================





                                        Next

                                        If Seat IsNot Nothing Then
                                            If InfoItem Is Nothing Then
                                                ReDim Preserve InfoItem(0)
                                            Else
                                                ReDim Preserve InfoItem(InfoItem.Length)
                                            End If
                                            InfoItem(InfoItem.Length - 1) = New objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRSTravelItineraryItineraryInfoItem
                                            InfoItem(InfoItem.Length - 1).Seats = Seat
                                        End If



                                        TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems = InfoItem
                                    Else
                                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                        objPNR.MSGError.Add("WARNING : NO EXISTE ITINERARIO PARA LA RESERVA")
                                        Exit Try
                                    End If

                                End If

ConsumeRobotVoid:
                                If TravelItineraryReadRS.TravelItinerary.ItineraryRef IsNot Nothing Then

                                    If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.CustomerIdentifier) Then
                                        objPNR.CustomerId = TravelItineraryReadRS.TravelItinerary.ItineraryRef.CustomerIdentifier
                                    End If

                                    If TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source IsNot Nothing Then

                                        'If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.PseudoCityCode) Then
                                        '    objPNR.PseudoRelease = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.PseudoCityCode.ToString
                                        'End If

                                        'If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.AAA_PseudoCityCode) Then
                                        '    objPNR.PseudoAAA = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.AAA_PseudoCityCode.ToString
                                        'End If
                                        '---------------
                                        If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.HomePseudoCityCode) Then
                                            objPNR.PseudoHome = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.HomePseudoCityCode.ToString
                                        End If

                                        If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.AAA_PseudoCityCode) Then
                                            objPNR.PseudoRelease = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.HomePseudoCityCode.ToString
                                            'objPNR.PseudoRelease = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.AAA_PseudoCityCode.ToString
                                        End If

                                        If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.PseudoCityCode) Then
                                            objPNR.PseudoAAA = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.PseudoCityCode.ToString
                                        End If
                                        '-------------------

                                        If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.CreateDateTime) Then
                                            objPNR.FechaCreacion = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.CreateDateTime.ToString
                                        End If

                                        If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.CreationAgent) Then
                                            objPNR.AgenteCreador = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.CreationAgent.ToString
                                        End If

                                    End If
                                End If
                                If EsConsumidoxRobotVoid Then
                                    Return objPNR
                                End If
                                '****** PASAJEROS ****
                                If TravelItineraryReadRS.TravelItinerary.CustomerInfo IsNot Nothing Then
                                    With TravelItineraryReadRS.TravelItinerary.CustomerInfo

                                        If .PersonName IsNot Nothing Then

                                            For i As Integer = 0 To .PersonName.Length - 1

                                                auxPasajeros = New classPasajeros

                                                If Not String.IsNullOrEmpty(.PersonName(i).NameNumber) Then
                                                    auxPasajeros.NumeroPasajero = Convert.ToString(Convert.ToInt64(.PersonName(i).NameNumber.Split(Constantes.Punto)(0))) & _
                                                                                  Constantes.Punto & _
                                                                                  Convert.ToString(Convert.ToInt64(.PersonName(i).NameNumber.Split(Constantes.Punto)(1)))
                                                End If


                                                If Not String.IsNullOrEmpty(.PersonName(i).GivenName) Then
                                                    auxPasajeros.NombrePasajero = .PersonName(i).GivenName.ToString
                                                Else
                                                    If oCadenaError.ToString.IndexOf(Constantes.PasajeroSinNombre & " número " & auxPasajeros.NumeroPasajero) = -1 Then
                                                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                                        objPNR.MSGError.Add(Constantes.PasajeroSinNombre & " número " & auxPasajeros.NumeroPasajero)
                                                        oCadenaError.Append(Constantes.PasajeroSinNombre & " número " & auxPasajeros.NumeroPasajero)
                                                    End If
                                                End If

                                                If Not String.IsNullOrEmpty(.PersonName(i).Surname) Then
                                                    auxPasajeros.ApellidoPaterno = .PersonName(i).Surname.ToString
                                                Else
                                                    If oCadenaError.ToString.IndexOf(Constantes.PasajeroSinApellido & " número " & auxPasajeros.NumeroPasajero) = -1 Then
                                                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                                        objPNR.MSGError.Add(Constantes.PasajeroSinApellido & " número " & auxPasajeros.NumeroPasajero)
                                                        oCadenaError.Append(Constantes.PasajeroSinApellido & " número " & auxPasajeros.NumeroPasajero)
                                                    End If
                                                End If

                                                If Not String.IsNullOrEmpty(.PersonName(i).PassengerType) Then
                                                    auxPasajeros.TipoPasajero = .PersonName(i).PassengerType.ToString
                                                End If

                                                If Not String.IsNullOrEmpty(.PersonName(i).WithInfant) Then
                                                    auxPasajeros.Infante = .PersonName(i).WithInfant
                                                End If



                                                If .PersonName(i).NameReference IsNot Nothing Then
                                                    'Dim PRUEBA As String = Nothing
                                                    'PRUEBA = .PersonName(i).NameReference.Substring(0, 1)
                                                    'PRUEBA = Nothing
                                                    If .PersonName(i).NameReference.Substring(0, 1) = Constantes.Id_DNI Then

                                                        auxDocumento = New classDocumento
                                                        auxDocumento.Tipo = Constantes.Id_DNI
                                                        auxDocumento.Num_Nombre = Constantes.Nombre_DNI
                                                        auxDocumento.Numero = .PersonName(i).NameReference.Substring(1, .PersonName(i).NameReference.Length - 1)

                                                        If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                        objDocumento.Add(auxDocumento)
                                                        auxDocumento = Nothing

                                                        auxPasajeros.Documento = New List(Of classDocumento)
                                                        auxPasajeros.Documento = objDocumento
                                                        objDocumento = Nothing

                                                    ElseIf .PersonName(i).NameReference.Substring(0, 1) = Constantes.Id_PASS Then

                                                        auxDocumento = New classDocumento
                                                        auxDocumento.Tipo = Constantes.Id_PASS
                                                        auxDocumento.Num_Nombre = Constantes.Nombre_Pasaporte
                                                        auxDocumento.Numero = Trim(.PersonName(i).NameReference.Substring(1, .PersonName(i).NameReference.Length - 1))

                                                        If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                        objDocumento.Add(auxDocumento)
                                                        auxDocumento = Nothing

                                                        auxPasajeros.Documento = New List(Of classDocumento)
                                                        auxPasajeros.Documento = objDocumento
                                                        objDocumento = Nothing

                                                    ElseIf .PersonName(i).NameReference.Substring(0, 2) = Constantes.Id_CE Then

                                                        auxDocumento = New classDocumento
                                                        auxDocumento.Tipo = Constantes.Id_CE
                                                        auxDocumento.Num_Nombre = Constantes.Nombre_CE
                                                        auxDocumento.Numero = Trim(.PersonName(i).NameReference.Substring(2, .PersonName(i).NameReference.Length - 2))

                                                        If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                        objDocumento.Add(auxDocumento)
                                                        auxDocumento = Nothing

                                                        auxPasajeros.Documento = New List(Of classDocumento)
                                                        auxPasajeros.Documento = objDocumento
                                                        objDocumento = Nothing

                                                    ElseIf .PersonName(i).NameReference.Substring(0, 3) = Constantes.Id_RUC Then

                                                        auxDocumento = New classDocumento
                                                        auxDocumento.Tipo = Constantes.Id_RUC
                                                        auxDocumento.Num_Nombre = Constantes.Nombre_RUC
                                                        auxDocumento.Numero = .PersonName(i).NameReference.Substring(3, .PersonName(i).NameReference.Length - 3)

                                                        If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                        objDocumento.Add(auxDocumento)
                                                        auxDocumento = Nothing

                                                        auxPasajeros.Documento = New List(Of classDocumento)
                                                        auxPasajeros.Documento = objDocumento
                                                        objDocumento = Nothing

                                                    Else
                                                        auxDocumento = Nothing
                                                        auxPasajeros.Documento = Nothing
                                                    End If
                                                End If

                                                If objPasajeros Is Nothing Then objPasajeros = New List(Of classPasajeros)
                                                objPasajeros.Add(auxPasajeros)
                                                auxPasajeros = Nothing

                                            Next

                                            objPNR.Pasajeros = New List(Of classPasajeros)
                                            objPNR.Pasajeros = objPasajeros
                                            objPasajeros = Nothing

                                        End If
                                    End With
                                End If

                                '**** ASIENTOS ASIGNADOS ****


                                If TravelItineraryReadRS.TravelItinerary.ItineraryInfo IsNot Nothing Then
                                    If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems IsNot Nothing Then

                                        If objPNR IsNot Nothing Then
                                            For z As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                auxPax = objPNR.Pasajeros.Item(z).NumeroPasajero

                                                With TravelItineraryReadRS.TravelItinerary.ItineraryInfo
                                                    For i As Integer = 0 To .ReservationItems.Length - 1
                                                        If .ReservationItems(i).Seats IsNot Nothing Then
                                                            If .ReservationItems(i).Seats IsNot Nothing Then
                                                                For w As Integer = 0 To .ReservationItems(i).Seats.Length - 1

                                                                    If auxPax = CInt(.ReservationItems(i).Seats(w).NameNumber.Split(".")(0)) & "." & CInt(.ReservationItems(i).Seats(w).NameNumber.Split(".")(1)) Then

                                                                        auxAsientos = New classAsientos

                                                                        'SegmentNumber
                                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).Seats(w).SegmentNumber) Then
                                                                            auxAsientos.Segmento = .ReservationItems(i).Seats(w).SegmentNumber
                                                                        End If

                                                                        'Number
                                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).Seats(w).Number) Then
                                                                            auxAsientos.Number = .ReservationItems(i).Seats(w).Number
                                                                        End If

                                                                        'FlightSegment
                                                                        If .ReservationItems(i).Seats(w).FlightSegment IsNot Nothing Then

                                                                            'OriginLocation
                                                                            If .ReservationItems(i).Seats(w).FlightSegment.OriginLocation IsNot Nothing Then
                                                                                If Not String.IsNullOrEmpty(.ReservationItems(i).Seats(w).FlightSegment.OriginLocation.LocationCode) Then
                                                                                    auxAsientos.OriginLocation = .ReservationItems(i).Seats(w).FlightSegment.OriginLocation.LocationCode
                                                                                End If
                                                                            End If

                                                                            'DestinationLocation
                                                                            If .ReservationItems(i).Seats(w).FlightSegment.DestinationLocation IsNot Nothing Then
                                                                                If Not String.IsNullOrEmpty(.ReservationItems(i).Seats(w).FlightSegment.DestinationLocation.LocationCode) Then
                                                                                    auxAsientos.DestinationLocation = .ReservationItems(i).Seats(w).FlightSegment.DestinationLocation.LocationCode
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If objPNR.Pasajeros.Item(z).Asientos Is Nothing Then objPNR.Pasajeros.Item(z).Asientos = New List(Of classAsientos)
                                                                        objPNR.Pasajeros.Item(z).Asientos.Add(auxAsientos)
                                                                    End If

                                                                Next
                                                            End If
                                                        End If
                                                    Next
                                                End With
                                            Next
                                        End If
                                    End If
                                End If

                                '****************************


                                '***** SEGMENTOS *****
                                If TravelItineraryReadRS.TravelItinerary.ItineraryInfo IsNot Nothing Then
                                    If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems IsNot Nothing Then
                                        With TravelItineraryReadRS.TravelItinerary.ItineraryInfo
                                            For i As Integer = 0 To .ReservationItems.Length - 1
                                                If .ReservationItems(i).FlightSegment IsNot Nothing Then

                                                    'If Not .ReservationItems(i).FlightSegment(0).IsPast Then

                                                    auxSegmentos = New classSegmentos
                                                    auxSegmentos.RPH = .ReservationItems(i).RPH.ToString
                                                    auxSegmentos.Segmento = intContadorSegmento


                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).MarketingAirline.Code) Then
                                                        auxSegmentos.Aerolinea = .ReservationItems(i).FlightSegment(0).MarketingAirline.Code
                                                    End If

                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).FlightNumber) Then
                                                        auxSegmentos.NumVuelo = .ReservationItems(i).FlightSegment(0).FlightNumber
                                                    End If

                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).ResBookDesigCode) Then
                                                        auxSegmentos.ClaseServicio = .ReservationItems(i).FlightSegment(0).ResBookDesigCode
                                                    End If

                                                    'Equipment
                                                    If .ReservationItems(i).FlightSegment(0).Equipment IsNot Nothing Then
                                                        auxSegmentos.Equipo = .ReservationItems(i).FlightSegment(0).Equipment.AirEquipType.ToString
                                                        objDAO = New DAO
                                                        auxSegmentos.NombreEquipo = objDAO.ObtenerNombreEquipo(auxSegmentos.Equipo, strCodigoSeguimiento, intFirmaDB, intEsquema)
                                                    End If

                                                    '2014-08-10T10:20:00
                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).DepartureDateTime) Then
                                                        auxSegmentos.FechaHoraSalida = .ReservationItems(i).FlightSegment(0).DepartureDateTime.ToString
                                                        oDate = New Date
                                                        oDate = .ReservationItems(i).FlightSegment(0).DepartureDateTime.Split("T")(0)
                                                        oMesDeparture = oDate.Month.ToString
                                                        oAnoDeparture = oDate.Year.ToString

                                                    End If


                                                    '08-10T17:10:00
                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).ArrivalDateTime) Then
                                                        AuxArrivalDateTime = .ReservationItems(i).FlightSegment(0).ArrivalDateTime.ToString

                                                        If AuxArrivalDateTime.Split("T")(0).Length = 5 Then

                                                            AuxArrivalDateTime = oAnoDeparture & "-" & AuxArrivalDateTime

                                                            oDate = New Date
                                                            oDate = AuxArrivalDateTime.Split("T")(0)
                                                            oMesArrival = oDate.Month

                                                            If CInt(oMesDeparture) > CInt(oMesArrival) Then
                                                                auxSegmentos.FechaHoraLlegada = CStr(CInt(oAnoDeparture) + 1) & "-" & .ReservationItems(i).FlightSegment(0).ArrivalDateTime.ToString
                                                            Else
                                                                auxSegmentos.FechaHoraLlegada = oAnoDeparture & "-" & .ReservationItems(i).FlightSegment(0).ArrivalDateTime.ToString
                                                            End If

                                                        Else
                                                            auxSegmentos.FechaHoraLlegada = .ReservationItems(i).FlightSegment(0).ArrivalDateTime.ToString
                                                        End If

                                                    End If

                                                    '08-10T17:10:00
                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).UpdatedDepartureTime) Then

                                                        AuxUpdatedDepartureTime = .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime

                                                        If AuxUpdatedDepartureTime.Split("T")(0).Length = 5 Then

                                                            oDate = New Date
                                                            oDate = auxSegmentos.FechaHoraSalida
                                                            oMesArrival = oDate.Month
                                                            oAnoArrival = oDate.Year

                                                            oDate = New Date
                                                            oDate = oAnoArrival & "-" & AuxUpdatedDepartureTime
                                                            oAuxMesUP = oDate.Month

                                                            If CInt(oAuxMesUP) < CInt(oMesArrival) Then
                                                                auxSegmentos.UpDateFechaHoraSalida = CStr(CInt(oAnoArrival) + 1) & "-" & .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime.ToString
                                                                auxSegmentos.FechaHoraSalida = CStr(CInt(oAnoArrival) + 1) & "-" & .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime.ToString
                                                            Else
                                                                auxSegmentos.UpDateFechaHoraSalida = oAnoDeparture & "-" & .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime.ToString
                                                                auxSegmentos.FechaHoraSalida = oAnoDeparture & "-" & .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime.ToString
                                                            End If

                                                        Else
                                                            auxSegmentos.UpDateFechaHoraSalida = .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime
                                                            auxSegmentos.FechaHoraSalida = .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime
                                                        End If

                                                    End If

                                                    '08-10T17:10:00
                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).UpdatedArrivalTime) Then
                                                        AuxUpdatedArrivalTime = .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime

                                                        If AuxUpdatedArrivalTime.Split("T")(0).Length = 5 Then

                                                            oDate = New Date
                                                            oDate = auxSegmentos.FechaHoraLlegada
                                                            oMesArrival = oDate.Month
                                                            oAnoArrival = oDate.Year

                                                            oDate = New Date
                                                            oDate = oAnoArrival & "-" & AuxUpdatedArrivalTime
                                                            oAuxMesUP = oDate.Month


                                                            If CInt(oAuxMesUP) < CInt(oMesArrival) Then
                                                                auxSegmentos.UpDateFechaHoraLlegada = CStr(CInt(oAnoArrival) + 1) & "-" & .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime.ToString
                                                                auxSegmentos.FechaHoraLlegada = CStr(CInt(oAnoArrival) + 1) & "-" & .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime.ToString
                                                            Else
                                                                auxSegmentos.UpDateFechaHoraLlegada = oAnoDeparture & "-" & .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime.ToString
                                                                auxSegmentos.FechaHoraLlegada = oAnoDeparture & "-" & .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime.ToString
                                                            End If

                                                        Else
                                                            auxSegmentos.UpDateFechaHoraLlegada = .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime
                                                            auxSegmentos.FechaHoraLlegada = .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime
                                                        End If

                                                    End If

                                                    If .ReservationItems(i).FlightSegment(0).OriginLocation.LocationCode IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).OriginLocation.LocationCode) Then

                                                            objDAO = New DAO
                                                            objCiudad = New classCiudad
                                                            objCiudad = objDAO.ObtenerDatosCiudad(.ReservationItems(i).FlightSegment(0).OriginLocation.LocationCode, _
                                                                                                           strCodigoSeguimiento, _
                                                                                                           intFirmaDB, _
                                                                                                           intEsquema)


                                                            auxSegmentos.Salida = New classCiudad
                                                            auxSegmentos.Salida = objCiudad


                                                        End If
                                                    End If

                                                    If .ReservationItems(i).FlightSegment(0).DestinationLocation.LocationCode IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).DestinationLocation.LocationCode) Then

                                                            objDAO = New DAO
                                                            objCiudad = New classCiudad

                                                            objCiudad = objDAO.ObtenerDatosCiudad(.ReservationItems(i).FlightSegment(0).DestinationLocation.LocationCode, _
                                                                                                           strCodigoSeguimiento, _
                                                                                                           intFirmaDB, _
                                                                                                           intEsquema)


                                                            auxSegmentos.Llegada = New classCiudad
                                                            auxSegmentos.Llegada = objCiudad
                                                        End If
                                                    End If

                                                    If .ReservationItems(i).FlightSegment(0).OperatingAirline IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).OperatingAirline(0).Code) Then
                                                            auxSegmentos.OperadoPor = .ReservationItems(i).FlightSegment(0).OperatingAirline(0).Code
                                                        ElseIf Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).OperatingAirline(0).CompanyShortName) Then
                                                            auxSegmentos.OperadoPor = .ReservationItems(i).FlightSegment(0).OperatingAirline(0).CompanyShortName
                                                        End If

                                                        If Not String.IsNullOrEmpty(auxSegmentos.OperadoPor) Then
                                                            auxSegmentos.OperadoPor = ObtieneCodigoOperador(auxSegmentos.OperadoPor, _
                                                                                                            strCodigoSeguimiento, _
                                                                                                            intFirmaDB, _
                                                                                                            intEsquema)
                                                        End If
                                                    End If

                                                    If .ReservationItems(i).FlightSegment(0).SupplierRef IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).SupplierRef.ID) Then
                                                            If Not .ReservationItems(i).FlightSegment(0).SupplierRef.ID.Contains("*") Then
                                                                If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                                                objPNR.MSGError.Add("NO EXISTE CODIGO DE LINEA AEREA EN EL SEGMENTO " & i)
                                                                oCadenaError.Append("NO EXISTE CODIGO DE LINEA AEREA EN EL SEGMENTO " & i)
                                                            Else
                                                                auxSegmentos.CodigoAerolinea = .ReservationItems(i).FlightSegment(0).SupplierRef.ID.Split(Constantes.Asterisco)(1).ToString
                                                            End If

                                                        End If
                                                    Else
                                                        If oCadenaError.ToString.IndexOf(Constantes.LocalizaroLineaAerea) = -1 Then
                                                            If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                                            objPNR.MSGError.Add(Constantes.LocalizaroLineaAerea)
                                                            oCadenaError.Append(Constantes.LocalizaroLineaAerea)
                                                        End If
                                                    End If


                                                    If .ReservationItems(i).FlightSegment(0).Status IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).Status.ToString) Then
                                                            auxSegmentos.Status = .ReservationItems(i).FlightSegment(0).Status.ToString
                                                            If Not .ReservationItems(i).FlightSegment(0).Status.ToString.Equals(Constantes.IWS_STATUS_SEGMENT) Then
                                                                If oCadenaError.ToString.IndexOf(Constantes.StatusDiferente) = -1 Then
                                                                    If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                                                    'objPNR.MSGError.Add(Constantes.StatusDiferente)
                                                                    'oCadenaError.Append(Constantes.StatusDiferente)
                                                                End If
                                                            End If
                                                        End If
                                                    End If

                                                    If objSegmentos Is Nothing Then objSegmentos = New List(Of classSegmentos)
                                                    objSegmentos.Add(auxSegmentos)
                                                    auxSegmentos = Nothing


                                                    intContadorSegmento += 1

                                                    'End If
                                                End If

                                            Next

                                            objPNR.Segmentos = New List(Of classSegmentos)
                                            objPNR.Segmentos = objSegmentos
                                            objSegmentos = Nothing
                                        End With
                                    Else
                                        If oCadenaError.ToString.IndexOf(Constantes.NoExisteSegmentoActivo) = -1 Then
                                            If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                            objPNR.MSGError.Add(Constantes.NoExisteSegmentoActivo)
                                            oCadenaError.Append(Constantes.NoExisteSegmentoActivo)
                                        End If
                                    End If
                                End If


                                '*******SSR - DOCS *******
                                If TravelItineraryReadRS.TravelItinerary.SpecialServiceInfo IsNot Nothing Then
                                    With TravelItineraryReadRS.TravelItinerary
                                        For i As Integer = 0 To .SpecialServiceInfo.Length - 1
                                            If .SpecialServiceInfo(i).Service IsNot Nothing Then
                                                If .SpecialServiceInfo(i).Service.SSR_Code IsNot Nothing And .SpecialServiceInfo(i).Service.SSR_Type IsNot Nothing Then

                                                    Dim strNumeroPasajero As String = Nothing
                                                    Dim strNombrePasajero As String = Nothing
                                                    Dim strTexto As String = Nothing
                                                    Dim strAuxTexto As String = Nothing
                                                    Dim strFechaNacimiento As String = Nothing
                                                    Dim strFechaExpiracion As String = Nothing
                                                    Dim strApellidoPaterno As String = Nothing
                                                    Dim strNacionalidad As String = Nothing
                                                    Dim strNacPasaporte As String = Nothing
                                                    Dim strPasaporte As String = Nothing
                                                    Dim strNacPasajero As String = Nothing
                                                    Dim strNombre As String = Nothing
                                                    Dim strGenero As String = Nothing
                                                    Dim strIndInfante As String = Nothing


                                                    If .SpecialServiceInfo(i).Service.SSR_Code.ToString = "SSR" And _
                                                       .SpecialServiceInfo(i).Service.SSR_Type.ToString = "DOCS" Then

                                                        With .SpecialServiceInfo(i).Service
                                                            strNumeroPasajero = Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(0))) & _
                                                                             Constantes.Punto & _
                                                                             Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(1)))

                                                            strNombrePasajero = .PersonName(0).Value
                                                            strAuxTexto = .Text.ToString
                                                        End With


                                                        'DB/04AUG78/M/PANDURO/OMAR
                                                        If strAuxTexto.IndexOf("DB/") > -1 Then
                                                            strFechaNacimiento = Nothing
                                                            strApellidoPaterno = Nothing
                                                            strNombre = Nothing
                                                            strGenero = Nothing

                                                            strTexto = strAuxTexto.Substring(strAuxTexto.IndexOf("DB/"), Len(strAuxTexto) - strAuxTexto.IndexOf("DB/"))

                                                            strFechaNacimiento = strTexto.Split("/")(1)
                                                            strGenero = strTexto.Split("/")(2)
                                                            If Len(strGenero) = 1 Then
                                                                strIndInfante = Nothing
                                                            Else
                                                                strIndInfante = strGenero.Substring(1, 1)
                                                                strGenero = strGenero.Substring(0, 1)
                                                            End If

                                                            strApellidoPaterno = strTexto.Split("/")(3)
                                                            strNombre = strTexto.Split("/")(4)

                                                            If strNombrePasajero = strApellidoPaterno & "/" & strNombre Then
                                                                For x As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                                    If strNumeroPasajero = objPNR.Pasajeros.Item(x).NumeroPasajero Then
                                                                        If Not String.IsNullOrEmpty(strFechaNacimiento) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            oDate = New Date
                                                                            oDate = CDate(strFechaNacimiento)
                                                                            objPNR.Pasajeros.Item(x).SSR.FechaNacimiento = oDate.ToString("dd/MM/yyyy")
                                                                        End If
                                                                        If Not String.IsNullOrEmpty(strGenero) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.Genero = strGenero
                                                                        End If
                                                                    End If
                                                                Next



                                                            ElseIf Not String.IsNullOrEmpty(strIndInfante) Then
                                                                If strIndInfante = "I" Then
                                                                    For x As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                                        If strNombrePasajero = objPNR.Pasajeros.Item(x).ApellidoPaterno & "/" & objPNR.Pasajeros.Item(x).NombrePasajero Then
                                                                            If Not objPNR.Pasajeros.Item(x).Infante Then
                                                                                objPNR.Pasajeros.Item(x).Infante = True
                                                                                If Not String.IsNullOrEmpty(strFechaNacimiento) Then
                                                                                    If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                    oDate = New Date
                                                                                    oDate = CDate(strFechaNacimiento)
                                                                                    objPNR.Pasajeros.Item(x).SSR.FechaNacimiento = oDate.ToString("dd/MM/yyyy")
                                                                                End If
                                                                                If Not String.IsNullOrEmpty(strGenero) Then
                                                                                    If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                    objPNR.Pasajeros.Item(x).SSR.Genero = strGenero
                                                                                End If
                                                                            End If
                                                                        End If
                                                                    Next
                                                                End If
                                                            End If


                                                        ElseIf strAuxTexto.IndexOf("P/") > -1 Then
                                                            'P/PE/45213321/PE/20MAR79/M/20MAR11/DE LA CRUZ/ROLANDO

                                                            strTexto = strAuxTexto.Substring(strAuxTexto.IndexOf("P/"), Len(strAuxTexto) - strAuxTexto.IndexOf("P/"))

                                                            strNacPasaporte = strTexto.Split("/")(1)
                                                            strPasaporte = strTexto.Split("/")(2)
                                                            strNacPasajero = strTexto.Split("/")(3)
                                                            strFechaNacimiento = strTexto.Split("/")(4)
                                                            strGenero = strTexto.Split("/")(5)
                                                            If strGenero.Length > 1 Then strIndInfante = strGenero.Substring(1, 1)
                                                            strFechaExpiracion = strTexto.Split("/")(6)
                                                            strApellidoPaterno = strTexto.Split("/")(7)
                                                            strNombre = strTexto.Split("/")(8)


                                                            If strNombrePasajero = strApellidoPaterno & "/" & strNombre Then
                                                                For x As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                                    If strNumeroPasajero = objPNR.Pasajeros.Item(x).NumeroPasajero Then
                                                                        oDate = New Date
                                                                        oDate = CDate(strFechaNacimiento)
                                                                        If Not String.IsNullOrEmpty(strFechaNacimiento) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            oDate = New Date
                                                                            oDate = CDate(strFechaNacimiento)
                                                                            objPNR.Pasajeros.Item(x).SSR.FechaNacimiento = oDate.ToString("dd/MM/yyyy")
                                                                        End If
                                                                        If Not String.IsNullOrEmpty(strGenero) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.Genero = strGenero
                                                                        End If
                                                                        If Not String.IsNullOrEmpty(strFechaExpiracion) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.FechaExpiracion = strFechaExpiracion
                                                                        End If
                                                                        '---
                                                                        If Not String.IsNullOrEmpty(strNacPasaporte) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.NacPasaporte = strNacPasaporte
                                                                        End If
                                                                        If Not String.IsNullOrEmpty(strPasaporte) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.Pasaporte = strPasaporte
                                                                        End If
                                                                        If Not String.IsNullOrEmpty(strNacPasajero) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.NacPasajero = strNacPasajero
                                                                        End If
                                                                    End If
                                                                Next
                                                            ElseIf Not String.IsNullOrEmpty(strIndInfante) Then
                                                                If strIndInfante = "I" Then
                                                                    For x As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                                        If strNombrePasajero = objPNR.Pasajeros.Item(x).ApellidoPaterno & "/" & objPNR.Pasajeros.Item(x).NombrePasajero Then

                                                                            objPNR.Pasajeros.Item(x).Infante = True
                                                                            If Not String.IsNullOrEmpty(strFechaNacimiento) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                oDate = New Date
                                                                                oDate = CDate(strFechaNacimiento)
                                                                                objPNR.Pasajeros.Item(x).SSR.FechaNacimiento = oDate.ToString("dd/MM/yyyy")
                                                                            End If
                                                                            If Not String.IsNullOrEmpty(strGenero) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                objPNR.Pasajeros.Item(x).SSR.Genero = strGenero
                                                                            End If
                                                                            If Not String.IsNullOrEmpty(strFechaExpiracion) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                objPNR.Pasajeros.Item(x).SSR.FechaExpiracion = strFechaExpiracion
                                                                            End If
                                                                            '-----
                                                                            If Not String.IsNullOrEmpty(strNacPasaporte) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                objPNR.Pasajeros.Item(x).SSR.NacPasaporte = strNacPasaporte
                                                                            End If
                                                                            If Not String.IsNullOrEmpty(strPasaporte) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                objPNR.Pasajeros.Item(x).SSR.Pasaporte = strPasaporte
                                                                            End If
                                                                            If Not String.IsNullOrEmpty(strNacPasajero) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                objPNR.Pasajeros.Item(x).SSR.NacPasajero = strNacPasajero
                                                                            End If
                                                                        End If

                                                                    Next
                                                                End If
                                                            End If

                                                        End If
                                                    End If


                                                    If .SpecialServiceInfo(i).Service.SSR_Code.ToString = "SSR" And _
                                                       .SpecialServiceInfo(i).Service.SSR_Type.ToString = "INFT" Then


                                                        With .SpecialServiceInfo(i).Service
                                                            strNumeroPasajero = Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(0))) & _
                                                                             Constantes.Punto & _
                                                                             Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(1)))

                                                            strNombrePasajero = .PersonName(0).Value
                                                            strAuxTexto = .Text(0).ToString
                                                        End With


                                                        Dim Parceo() As String = strAuxTexto.Split("/")

                                                        oDate = New Date
                                                        oDate = CDate(Parceo(Parceo.Length - 1).Substring(0, 7))
                                                        Try
                                                            strFechaNacimiento = oDate.ToString("dd/MM/yyyy")
                                                        Catch ex As Exception
                                                            strFechaNacimiento = String.Format("{0:dd/MM/yyyy}", DateTime.Parse(oDate, CultureInfo.InvariantCulture))
                                                        End Try

                                                        strNombre = Parceo(Parceo.Length - 2)
                                                        strApellidoPaterno = Parceo(Parceo.Length - 3)

                                                        For x As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                            If strApellidoPaterno & Constantes.Slash & strNombre = objPNR.Pasajeros.Item(x).ApellidoPaterno & Constantes.Slash & objPNR.Pasajeros.Item(x).NombrePasajero Then
                                                                objPNR.Pasajeros.Item(x).Infante = True
                                                                If Not String.IsNullOrEmpty(strFechaNacimiento) Then
                                                                    If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                    oDate = New Date
                                                                    oDate = CDate(strFechaNacimiento)
                                                                    objPNR.Pasajeros.Item(x).SSR.FechaNacimiento = strFechaNacimiento
                                                                    objPNR.Pasajeros.Item(x).AdultoAsociado = strNumeroPasajero
                                                                    Exit For
                                                                End If
                                                            End If
                                                        Next

                                                    End If


                                                    If .SpecialServiceInfo(i).Service.SSR_Code.ToString = "SSR" And _
                                                       .SpecialServiceInfo(i).Service.SSR_Type.ToString = "FOID" Then


                                                        With .SpecialServiceInfo(i).Service
                                                            strNumeroPasajero = Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(0))) & _
                                                                             Constantes.Punto & _
                                                                             Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(1)))

                                                            strNombrePasajero = .PersonName(0).Value
                                                            strAuxTexto = .Text(0).Split("/")(1)
                                                        End With

                                                        For x As Integer = 0 To objPNR.Pasajeros.Count - 1

                                                            If strNumeroPasajero = objPNR.Pasajeros.Item(x).NumeroPasajero Then
                                                                If strAuxTexto.Substring(0, 2) = "NI" Then
                                                                    If strAuxTexto.Substring(0, 4) = "NICE" Then
                                                                        If objPNR.Pasajeros.Item(x).Documento Is Nothing Then

                                                                            auxDocumento = New classDocumento
                                                                            auxDocumento.Numero = strAuxTexto.Replace("NICE", "")
                                                                            auxDocumento.Tipo = Constantes.Id_CE
                                                                            auxDocumento.Num_Nombre = Constantes.Nombre_CE

                                                                            If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                                            objDocumento.Add(auxDocumento)

                                                                            If objPNR.Pasajeros.Item(x).Documento Is Nothing Then objPNR.Pasajeros.Item(x).Documento = New List(Of classDocumento)
                                                                            objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                            auxDocumento = Nothing
                                                                        Else

                                                                            Dim flag As Boolean = False

                                                                            auxDocumento = New classDocumento
                                                                            auxDocumento.Numero = strAuxTexto.Replace("NICE", "")
                                                                            auxDocumento.Tipo = Constantes.Id_CE
                                                                            auxDocumento.Num_Nombre = Constantes.Nombre_CE

                                                                            For y As Integer = 0 To objPNR.Pasajeros.Item(x).Documento.Count - 1
                                                                                If objPNR.Pasajeros.Item(x).Documento.Item(y).Tipo = auxDocumento.Tipo Then
                                                                                    flag = True
                                                                                    Exit For
                                                                                End If
                                                                            Next

                                                                            If Not flag Then
                                                                                objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                                auxDocumento = Nothing
                                                                            End If
                                                                        End If

                                                                    Else

                                                                        If objPNR.Pasajeros.Item(x).Documento Is Nothing Then
                                                                            auxDocumento = New classDocumento
                                                                            auxDocumento.Numero = strAuxTexto.Replace("NI", "")
                                                                            auxDocumento.Tipo = Constantes.Id_DNI
                                                                            auxDocumento.Num_Nombre = Constantes.Nombre_DNI

                                                                            If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                                            objDocumento.Add(auxDocumento)

                                                                            If objPNR.Pasajeros.Item(x).Documento Is Nothing Then objPNR.Pasajeros.Item(x).Documento = New List(Of classDocumento)
                                                                            objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                            auxDocumento = Nothing
                                                                        Else
                                                                            Dim flag As Boolean = False

                                                                            auxDocumento = New classDocumento
                                                                            auxDocumento.Numero = strAuxTexto.Replace("NI", "")
                                                                            auxDocumento.Tipo = Constantes.Id_DNI
                                                                            auxDocumento.Num_Nombre = Constantes.Nombre_DNI

                                                                            For y As Integer = 0 To objPNR.Pasajeros.Item(x).Documento.Count - 1
                                                                                If objPNR.Pasajeros.Item(x).Documento.Item(y).Tipo = auxDocumento.Tipo Then
                                                                                    flag = True
                                                                                    Exit For
                                                                                End If
                                                                            Next

                                                                            If Not flag Then
                                                                                objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                                auxDocumento = Nothing
                                                                            End If

                                                                        End If

                                                                    End If

                                                                ElseIf strAuxTexto.Substring(0, 2) = "ID" Then

                                                                    If objPNR.Pasajeros.Item(x).Documento Is Nothing Then
                                                                        auxDocumento = New classDocumento
                                                                        auxDocumento.Numero = strAuxTexto.Replace("IDPP", "")
                                                                        auxDocumento.Tipo = Constantes.Id_PASS
                                                                        auxDocumento.Num_Nombre = Constantes.Nombre_Pasaporte

                                                                        If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                                        objDocumento.Add(auxDocumento)

                                                                        If objPNR.Pasajeros.Item(x).Documento Is Nothing Then objPNR.Pasajeros.Item(x).Documento = New List(Of classDocumento)
                                                                        objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                        auxDocumento = Nothing
                                                                    Else
                                                                        Dim flag As Boolean = False

                                                                        auxDocumento = New classDocumento
                                                                        auxDocumento.Numero = strAuxTexto.Replace("IDPP", "")
                                                                        auxDocumento.Tipo = Constantes.Id_PASS
                                                                        auxDocumento.Num_Nombre = Constantes.Nombre_Pasaporte

                                                                        For y As Integer = 0 To objPNR.Pasajeros.Item(x).Documento.Count - 1
                                                                            If objPNR.Pasajeros.Item(x).Documento.Item(y).Tipo = auxDocumento.Tipo Then
                                                                                flag = True
                                                                                Exit For
                                                                            End If
                                                                        Next

                                                                        If Not flag Then
                                                                            objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                            auxDocumento = Nothing
                                                                        End If

                                                                    End If

                                                                End If
                                                                Exit For
                                                            End If
                                                        Next
                                                    End If


                                                    strNumeroPasajero = Nothing
                                                    strNombrePasajero = Nothing
                                                    strTexto = Nothing
                                                    strAuxTexto = Nothing
                                                    strFechaNacimiento = Nothing
                                                    strFechaExpiracion = Nothing
                                                    strApellidoPaterno = Nothing
                                                    strNacionalidad = Nothing
                                                    strNombre = Nothing
                                                    strGenero = Nothing
                                                    strIndInfante = Nothing
                                                    oDate = Nothing


                                                End If
                                            End If
                                        Next

                                    End With
                                End If

                                '***** BOLETOS EMITIDOS ****

                                If TravelItineraryReadRS IsNot Nothing Then
                                    If TravelItineraryReadRS.TravelItinerary IsNot Nothing Then
                                        If TravelItineraryReadRS.TravelItinerary.ItineraryInfo IsNot Nothing Then
                                            If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.Ticketing IsNot Nothing Then
                                                With TravelItineraryReadRS.TravelItinerary.ItineraryInfo
                                                    For i As Integer = 0 To .Ticketing.Length - 1


                                                        If .Ticketing(i).eTicketNumber IsNot Nothing Then

                                                            If objPNR.Boletos Is Nothing Then objPNR.Boletos = New List(Of classPNR.classBoletos)
                                                            auxBoleto = New classPNR.classBoletos

                                                            If Not String.IsNullOrEmpty(.Ticketing(i).RPH) Then
                                                                auxBoleto.ID = .Ticketing(i).RPH
                                                            End If

                                                            If Not String.IsNullOrEmpty(.Ticketing(i).eTicketNumber) Then
                                                                auxBoleto.Ticket = .Ticketing(i).eTicketNumber
                                                            End If

                                                            objPNR.Boletos.Add(auxBoleto)
                                                            auxBoleto = Nothing

                                                        End If

                                                    Next
                                                End With

                                            End If
                                        End If
                                    End If
                                End If

                                '***** REMARKS ****
                                If TravelItineraryReadRS IsNot Nothing Then
                                    If TravelItineraryReadRS.TravelItinerary IsNot Nothing Then
                                        If TravelItineraryReadRS.TravelItinerary.RemarkInfo IsNot Nothing Then
                                            For i As Integer = 0 To TravelItineraryReadRS.TravelItinerary.RemarkInfo.Length - 1
                                                If String.IsNullOrEmpty(strTypeRemark) Then
                                                    If objPNR.Remarks Is Nothing Then objPNR.Remarks = New List(Of classRemark)
                                                    If objRemarks Is Nothing Then objRemarks = New List(Of classRemark)
                                                    auxRemarks = New classRemark

                                                    auxRemarks.TypeRemark = TravelItineraryReadRS.TravelItinerary.RemarkInfo(i).Type
                                                    auxRemarks.Code = TravelItineraryReadRS.TravelItinerary.RemarkInfo(i).Code
                                                    auxRemarks.Text = TravelItineraryReadRS.TravelItinerary.RemarkInfo(i).Text
                                                    auxRemarks.NroLinea = TravelItineraryReadRS.TravelItinerary.RemarkInfo(i).RPH


                                                    objPNR.Remarks.Add(auxRemarks)
                                                    auxBoleto = Nothing
                                                ElseIf TravelItineraryReadRS.TravelItinerary.RemarkInfo(i).Type = strTypeRemark Then
                                                    If objPNR.Remarks Is Nothing Then objPNR.Remarks = New List(Of classRemark)
                                                    If objRemarks Is Nothing Then objRemarks = New List(Of classRemark)
                                                    auxRemarks = New classRemark

                                                    auxRemarks.TypeRemark = TravelItineraryReadRS.TravelItinerary.RemarkInfo(i).Type
                                                    auxRemarks.Code = TravelItineraryReadRS.TravelItinerary.RemarkInfo(i).Code
                                                    auxRemarks.Text = TravelItineraryReadRS.TravelItinerary.RemarkInfo(i).Text
                                                    auxRemarks.NroLinea = TravelItineraryReadRS.TravelItinerary.RemarkInfo(i).RPH


                                                    objPNR.Remarks.Add(auxRemarks)
                                                    auxBoleto = Nothing

                                                End If
                                            Next
                                        End If
                                    End If


                                End If

                            End If
                        End If


                    Else

                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                        objPNR.MSGError.Add("Se produjo un error al momento de recuperar el código")

                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strPNR = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objSession = Nothing
                objIWebServices = Nothing
                objIWebServices = Nothing
                intContadorSegmento = Nothing

                auxAsientos = Nothing
                auxPax = Nothing
            End Try

            Return objPNR
        End Function
        Public Function RecuperarPNR_EMD(ByVal strPNR As String, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intGDS As Integer, _
                                         ByVal intFirmaGDS As Integer, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer, _
                                         ByVal objSession As classSession) As classPNR

            Dim TravelItineraryReadRS As objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRS = Nothing
            Dim objPNR As classPNR = Nothing
            Dim objPasajeros As List(Of classPasajeros) = Nothing
            Dim auxPasajeros As classPasajeros = Nothing

            Dim objSegmentos As List(Of classSegmentos) = Nothing
            Dim auxSegmentos As classSegmentos = Nothing

            Dim objDocumento As New List(Of classDocumento)
            Dim auxDocumento As classDocumento = Nothing
            Dim oCadenaError As New System.Text.StringBuilder
            oCadenaError.Append("")

            Dim SabreCommandLLSRS As objSabreWS.SabreCommand.SabreCommandLLSRS = Nothing
            Dim EndTransactionRS As objSabreWS.EndTransaction.EndTransactionRS = Nothing

            Dim oDate As Date = Nothing
            Dim oAuxMesUP As String = Nothing
            Dim oMesDeparture As String = Nothing
            Dim oAnoDeparture As String = Nothing
            Dim oAnoArrival As String = Nothing
            Dim oMesArrival As String = Nothing
            Dim AuxArrivalDateTime As String = Nothing
            Dim AuxUpdatedDepartureTime As String = Nothing
            Dim AuxUpdatedArrivalTime As String = Nothing

            Dim objDAO As DAO = Nothing
            Dim objCiudad As classCiudad = Nothing

            'Dim objWSBusiness As WSBusiness = Nothing
            Dim intContadorSegmento As Integer = 1

            Dim auxAsientos As classAsientos = Nothing
            Dim auxPax As String = Nothing

            Dim auxBoleto As classPNR.classBoletos = Nothing

            Try

                If Not String.IsNullOrEmpty(strPNR) Then


                    objIWebServices = New IWebServices



                    TravelItineraryReadRS = objIWebServices._TravelItineraryReadEMD(strPNR, _
                                                                                    strCodigoSeguimiento, _
                                                                                    intGDS, _
                                                                                    intFirmaGDS, _
                                                                                    intFirmaDB, _
                                                                                    objSession)


                    If TravelItineraryReadRS IsNot Nothing Then


                        If TravelItineraryReadRS.ApplicationResults IsNot Nothing Then
                            objPNR = New classPNR

                            If TravelItineraryReadRS.ApplicationResults.Error IsNot Nothing Then
                                For i As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Error.Length - 1
                                    For x As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                        For y As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message.Length - 1
                                            objPNR.MSGError.Add("ERROR : " & TravelItineraryReadRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(y).Value.ToString())
                                        Next
                                    Next
                                Next
                            End If

                            If TravelItineraryReadRS.ApplicationResults.Warning IsNot Nothing Then
                                For i As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Warning.Length - 1
                                    For x As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                        For y As Integer = 0 To TravelItineraryReadRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message.Length - 1
                                            objPNR.MSGError.Add("WARNING : " & TravelItineraryReadRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message(y).Value.ToString())
                                        Next
                                    Next
                                Next
                            End If


                            If TravelItineraryReadRS.TravelItinerary IsNot Nothing Then
                                Dim SegmentNumber As String = Nothing

                                Dim _ArrivalDateTime As String
                                Dim _ArrivalAirportLocationCode As String
                                Dim _UpdatedArrivalTime As String
                                Dim InfoItem() As objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRSTravelItineraryItineraryInfoItem = Nothing

                                Dim Seat() As objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRSTravelItineraryItineraryInfoItemSeat = Nothing

                                Dim oFlag As Boolean = False

                                '****** REORDENAMOS LOS VUELOS ********
                                If TravelItineraryReadRS.TravelItinerary.ItineraryInfo IsNot Nothing Then

                                    If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems IsNot Nothing Then

                                        For i As Integer = 0 To TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems.Length - 1

                                            '========================================================

                                            If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment IsNot Nothing Then

                                                If Not oFlag Then

                                                    SegmentNumber = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).SegmentNumber.Trim

                                                    ReDim InfoItem(0)
                                                    InfoItem(0) = New objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRSTravelItineraryItineraryInfoItem
                                                    InfoItem(0).RPH = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).RPH
                                                    ReDim InfoItem(0).FlightSegment(0)
                                                    InfoItem(0).FlightSegment(0) = New objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRSTravelItineraryItineraryInfoItemFlightSegment
                                                    InfoItem(0).FlightSegment = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment

                                                    oFlag = True
                                                    i += 1

                                                End If

                                                If i < TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems.Length - 1 Then
                                                    If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment IsNot Nothing Then

                                                        If SegmentNumber = CStr(CInt(TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).SegmentNumber.Trim)) Then

                                                            _ArrivalDateTime = Nothing
                                                            _ArrivalAirportLocationCode = Nothing
                                                            _UpdatedArrivalTime = Nothing

                                                            _ArrivalDateTime = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).ArrivalDateTime
                                                            _ArrivalAirportLocationCode = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).DestinationLocation.LocationCode.Trim
                                                            If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).UpdatedArrivalTime IsNot Nothing Then
                                                                _UpdatedArrivalTime = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).UpdatedArrivalTime.Trim
                                                            End If

                                                            InfoItem(InfoItem.Length - 1).FlightSegment(0).ArrivalDateTime = _ArrivalDateTime
                                                            InfoItem(InfoItem.Length - 1).FlightSegment(0).DestinationLocation.LocationCode = _ArrivalAirportLocationCode
                                                            InfoItem(InfoItem.Length - 1).FlightSegment(0).UpdatedArrivalTime = _UpdatedArrivalTime

                                                        Else
                                                            ReDim Preserve InfoItem(InfoItem.Length)
                                                            InfoItem(InfoItem.Length - 1) = New objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRSTravelItineraryItineraryInfoItem
                                                            InfoItem(InfoItem.Length - 1).RPH = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).RPH.Trim
                                                            ReDim InfoItem(InfoItem.Length - 1).FlightSegment(0)
                                                            InfoItem(InfoItem.Length - 1).FlightSegment(0) = New objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRSTravelItineraryItineraryInfoItemFlightSegment
                                                            InfoItem(InfoItem.Length - 1).FlightSegment = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment

                                                        End If

                                                        SegmentNumber = CStr(CInt(TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).FlightSegment(0).SegmentNumber.Trim))

                                                    End If
                                                End If

                                            End If

                                            '========================================================


                                            If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).Seats IsNot Nothing Then
                                                If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).Seats IsNot Nothing Then

                                                    For j As Integer = 0 To TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).Seats.Length - 1

                                                        If Seat Is Nothing Then
                                                            ReDim Seat(0)
                                                        Else
                                                            ReDim Preserve Seat(Seat.Length)
                                                        End If

                                                        Seat(Seat.Length - 1) = New objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRSTravelItineraryItineraryInfoItemSeat
                                                        Seat(Seat.Length - 1) = TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems(i).Seats(j)

                                                    Next

                                                End If
                                            End If


                                        Next

                                        If Seat IsNot Nothing Then
                                            If InfoItem Is Nothing Then
                                                ReDim Preserve InfoItem(0)
                                            Else
                                                ReDim Preserve InfoItem(InfoItem.Length)
                                            End If
                                            InfoItem(InfoItem.Length - 1) = New objSabreWS.TravelItineraryReadRQ360.TravelItineraryReadRSTravelItineraryItineraryInfoItem
                                            InfoItem(InfoItem.Length - 1).Seats = Seat
                                        End If



                                        TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems = InfoItem
                                    Else
                                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                        objPNR.MSGError.Add("WARNING : NO EXISTE ITINERARIO PARA LA RESERVA")
                                        Exit Try
                                    End If

                                End If


                                If TravelItineraryReadRS.TravelItinerary.ItineraryRef IsNot Nothing Then

                                    If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.CustomerIdentifier) Then
                                        objPNR.CustomerId = TravelItineraryReadRS.TravelItinerary.ItineraryRef.CustomerIdentifier
                                    End If

                                    If TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source IsNot Nothing Then


                                        If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.HomePseudoCityCode) Then
                                            objPNR.PseudoHome = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.HomePseudoCityCode.ToString
                                        End If

                                        If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.AAA_PseudoCityCode) Then
                                            objPNR.PseudoRelease = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.HomePseudoCityCode.ToString
                                            'objPNR.PseudoRelease = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.AAA_PseudoCityCode.ToString
                                        End If

                                        If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.PseudoCityCode) Then
                                            objPNR.PseudoAAA = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.PseudoCityCode.ToString
                                        End If

                                        If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.CreateDateTime) Then
                                            objPNR.FechaCreacion = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.CreateDateTime.ToString
                                        End If

                                        If Not String.IsNullOrEmpty(TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.CreationAgent) Then
                                            objPNR.AgenteCreador = TravelItineraryReadRS.TravelItinerary.ItineraryRef.Source.CreationAgent.ToString
                                        End If

                                    End If
                                End If

                                '****** PASAJEROS ****
                                If TravelItineraryReadRS.TravelItinerary.CustomerInfo IsNot Nothing Then
                                    With TravelItineraryReadRS.TravelItinerary.CustomerInfo

                                        If .PersonName IsNot Nothing Then

                                            For i As Integer = 0 To .PersonName.Length - 1

                                                auxPasajeros = New classPasajeros

                                                If Not String.IsNullOrEmpty(.PersonName(i).NameNumber) Then
                                                    auxPasajeros.NumeroPasajero = Convert.ToString(Convert.ToInt64(.PersonName(i).NameNumber.Split(Constantes.Punto)(0))) & _
                                                                                  Constantes.Punto & _
                                                                                  Convert.ToString(Convert.ToInt64(.PersonName(i).NameNumber.Split(Constantes.Punto)(1)))
                                                End If


                                                If Not String.IsNullOrEmpty(.PersonName(i).GivenName) Then
                                                    auxPasajeros.NombrePasajero = .PersonName(i).GivenName.ToString
                                                Else
                                                    If oCadenaError.ToString.IndexOf(Constantes.PasajeroSinNombre & " número " & auxPasajeros.NumeroPasajero) = -1 Then
                                                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                                        objPNR.MSGError.Add(Constantes.PasajeroSinNombre & " número " & auxPasajeros.NumeroPasajero)
                                                        oCadenaError.Append(Constantes.PasajeroSinNombre & " número " & auxPasajeros.NumeroPasajero)
                                                    End If
                                                End If

                                                If Not String.IsNullOrEmpty(.PersonName(i).Surname) Then
                                                    auxPasajeros.ApellidoPaterno = .PersonName(i).Surname.ToString
                                                Else
                                                    If oCadenaError.ToString.IndexOf(Constantes.PasajeroSinApellido & " número " & auxPasajeros.NumeroPasajero) = -1 Then
                                                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                                        objPNR.MSGError.Add(Constantes.PasajeroSinApellido & " número " & auxPasajeros.NumeroPasajero)
                                                        oCadenaError.Append(Constantes.PasajeroSinApellido & " número " & auxPasajeros.NumeroPasajero)
                                                    End If
                                                End If

                                                If Not String.IsNullOrEmpty(.PersonName(i).PassengerType) Then
                                                    auxPasajeros.TipoPasajero = .PersonName(i).PassengerType.ToString
                                                End If

                                                If Not String.IsNullOrEmpty(.PersonName(i).WithInfant) Then
                                                    auxPasajeros.Infante = .PersonName(i).WithInfant
                                                End If



                                                If .PersonName(i).NameReference IsNot Nothing Then
                                                    'Dim PRUEBA As String = Nothing
                                                    'PRUEBA = .PersonName(i).NameReference.Substring(0, 1)
                                                    'PRUEBA = Nothing
                                                    If .PersonName(i).NameReference.Substring(0, 1) = Constantes.Id_DNI Then

                                                        auxDocumento = New classDocumento
                                                        auxDocumento.Tipo = Constantes.Id_DNI
                                                        auxDocumento.Num_Nombre = Constantes.Nombre_DNI
                                                        auxDocumento.Numero = .PersonName(i).NameReference.Substring(1, .PersonName(i).NameReference.Length - 1)

                                                        If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                        objDocumento.Add(auxDocumento)
                                                        auxDocumento = Nothing

                                                        auxPasajeros.Documento = New List(Of classDocumento)
                                                        auxPasajeros.Documento = objDocumento
                                                        objDocumento = Nothing

                                                    ElseIf .PersonName(i).NameReference.Substring(0, 1) = Constantes.Id_PASS Then

                                                        auxDocumento = New classDocumento
                                                        auxDocumento.Tipo = Constantes.Id_PASS
                                                        auxDocumento.Num_Nombre = Constantes.Nombre_Pasaporte
                                                        auxDocumento.Numero = Trim(.PersonName(i).NameReference.Substring(1, .PersonName(i).NameReference.Length - 1))

                                                        If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                        objDocumento.Add(auxDocumento)
                                                        auxDocumento = Nothing

                                                        auxPasajeros.Documento = New List(Of classDocumento)
                                                        auxPasajeros.Documento = objDocumento
                                                        objDocumento = Nothing

                                                    ElseIf .PersonName(i).NameReference.Substring(0, 2) = Constantes.Id_CE Then

                                                        auxDocumento = New classDocumento
                                                        auxDocumento.Tipo = Constantes.Id_CE
                                                        auxDocumento.Num_Nombre = Constantes.Nombre_CE
                                                        auxDocumento.Numero = Trim(.PersonName(i).NameReference.Substring(2, .PersonName(i).NameReference.Length - 2))

                                                        If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                        objDocumento.Add(auxDocumento)
                                                        auxDocumento = Nothing

                                                        auxPasajeros.Documento = New List(Of classDocumento)
                                                        auxPasajeros.Documento = objDocumento
                                                        objDocumento = Nothing

                                                    ElseIf .PersonName(i).NameReference.Substring(0, 3) = Constantes.Id_RUC Then

                                                        auxDocumento = New classDocumento
                                                        auxDocumento.Tipo = Constantes.Id_RUC
                                                        auxDocumento.Num_Nombre = Constantes.Nombre_RUC
                                                        auxDocumento.Numero = .PersonName(i).NameReference.Substring(3, .PersonName(i).NameReference.Length - 3)

                                                        If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                        objDocumento.Add(auxDocumento)
                                                        auxDocumento = Nothing

                                                        auxPasajeros.Documento = New List(Of classDocumento)
                                                        auxPasajeros.Documento = objDocumento
                                                        objDocumento = Nothing

                                                    Else
                                                        auxDocumento = Nothing
                                                        auxPasajeros.Documento = Nothing
                                                    End If
                                                End If

                                                If objPasajeros Is Nothing Then objPasajeros = New List(Of classPasajeros)
                                                objPasajeros.Add(auxPasajeros)
                                                auxPasajeros = Nothing

                                            Next

                                            objPNR.Pasajeros = New List(Of classPasajeros)
                                            objPNR.Pasajeros = objPasajeros
                                            objPasajeros = Nothing

                                        End If
                                    End With
                                End If

                                '**** ASIENTOS ASIGNADOS ****


                                If TravelItineraryReadRS.TravelItinerary.ItineraryInfo IsNot Nothing Then
                                    If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems IsNot Nothing Then

                                        If objPNR IsNot Nothing Then
                                            For z As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                auxPax = objPNR.Pasajeros.Item(z).NumeroPasajero

                                                With TravelItineraryReadRS.TravelItinerary.ItineraryInfo
                                                    For i As Integer = 0 To .ReservationItems.Length - 1
                                                        If .ReservationItems(i).Seats IsNot Nothing Then
                                                            If .ReservationItems(i).Seats IsNot Nothing Then
                                                                For w As Integer = 0 To .ReservationItems(i).Seats.Length - 1

                                                                    If auxPax = CInt(.ReservationItems(i).Seats(w).NameNumber.Split(".")(0)) & "." & CInt(.ReservationItems(i).Seats(w).NameNumber.Split(".")(1)) Then

                                                                        auxAsientos = New classAsientos

                                                                        'SegmentNumber
                                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).Seats(w).SegmentNumber) Then
                                                                            auxAsientos.Segmento = .ReservationItems(i).Seats(w).SegmentNumber
                                                                        End If

                                                                        'Number
                                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).Seats(w).Number) Then
                                                                            auxAsientos.Number = .ReservationItems(i).Seats(w).Number
                                                                        End If

                                                                        'FlightSegment
                                                                        If .ReservationItems(i).Seats(w).FlightSegment IsNot Nothing Then

                                                                            'OriginLocation
                                                                            If .ReservationItems(i).Seats(w).FlightSegment.OriginLocation IsNot Nothing Then
                                                                                If Not String.IsNullOrEmpty(.ReservationItems(i).Seats(w).FlightSegment.OriginLocation.LocationCode) Then
                                                                                    auxAsientos.OriginLocation = .ReservationItems(i).Seats(w).FlightSegment.OriginLocation.LocationCode
                                                                                End If
                                                                            End If

                                                                            'DestinationLocation
                                                                            If .ReservationItems(i).Seats(w).FlightSegment.DestinationLocation IsNot Nothing Then
                                                                                If Not String.IsNullOrEmpty(.ReservationItems(i).Seats(w).FlightSegment.DestinationLocation.LocationCode) Then
                                                                                    auxAsientos.DestinationLocation = .ReservationItems(i).Seats(w).FlightSegment.DestinationLocation.LocationCode
                                                                                End If
                                                                            End If
                                                                        End If

                                                                        If objPNR.Pasajeros.Item(z).Asientos Is Nothing Then objPNR.Pasajeros.Item(z).Asientos = New List(Of classAsientos)
                                                                        objPNR.Pasajeros.Item(z).Asientos.Add(auxAsientos)
                                                                    End If

                                                                Next
                                                            End If
                                                        End If
                                                    Next
                                                End With
                                            Next
                                        End If
                                    End If
                                End If

                                '****************************


                                '***** SEGMENTOS *****
                                If TravelItineraryReadRS.TravelItinerary.ItineraryInfo IsNot Nothing Then
                                    If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.ReservationItems IsNot Nothing Then
                                        With TravelItineraryReadRS.TravelItinerary.ItineraryInfo
                                            For i As Integer = 0 To .ReservationItems.Length - 1
                                                If .ReservationItems(i).FlightSegment IsNot Nothing Then

                                                    'If Not .ReservationItems(i).FlightSegment(0).IsPast Then

                                                    auxSegmentos = New classSegmentos
                                                    auxSegmentos.RPH = .ReservationItems(i).RPH.ToString
                                                    auxSegmentos.Segmento = intContadorSegmento


                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).MarketingAirline.Code) Then
                                                        auxSegmentos.Aerolinea = .ReservationItems(i).FlightSegment(0).MarketingAirline.Code
                                                    End If

                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).FlightNumber) Then
                                                        auxSegmentos.NumVuelo = .ReservationItems(i).FlightSegment(0).FlightNumber
                                                    End If

                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).ResBookDesigCode) Then
                                                        auxSegmentos.ClaseServicio = .ReservationItems(i).FlightSegment(0).ResBookDesigCode
                                                    End If

                                                    'Equipment
                                                    If .ReservationItems(i).FlightSegment(0).Equipment IsNot Nothing Then
                                                        auxSegmentos.Equipo = .ReservationItems(i).FlightSegment(0).Equipment.AirEquipType.ToString
                                                        objDAO = New DAO
                                                        auxSegmentos.NombreEquipo = objDAO.ObtenerNombreEquipo(auxSegmentos.Equipo, strCodigoSeguimiento, intFirmaDB, intEsquema)
                                                    End If

                                                    '2014-08-10T10:20:00
                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).DepartureDateTime) Then
                                                        auxSegmentos.FechaHoraSalida = .ReservationItems(i).FlightSegment(0).DepartureDateTime.ToString
                                                        oDate = New Date
                                                        oDate = .ReservationItems(i).FlightSegment(0).DepartureDateTime.Split("T")(0)
                                                        oMesDeparture = oDate.Month.ToString
                                                        oAnoDeparture = oDate.Year.ToString

                                                    End If


                                                    '08-10T17:10:00
                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).ArrivalDateTime) Then
                                                        AuxArrivalDateTime = .ReservationItems(i).FlightSegment(0).ArrivalDateTime.ToString

                                                        If AuxArrivalDateTime.Split("T")(0).Length = 5 Then

                                                            AuxArrivalDateTime = oAnoDeparture & "-" & AuxArrivalDateTime

                                                            oDate = New Date
                                                            oDate = AuxArrivalDateTime.Split("T")(0)
                                                            oMesArrival = oDate.Month

                                                            If CInt(oMesDeparture) > CInt(oMesArrival) Then
                                                                auxSegmentos.FechaHoraLlegada = CStr(CInt(oAnoDeparture) + 1) & "-" & .ReservationItems(i).FlightSegment(0).ArrivalDateTime.ToString
                                                            Else
                                                                auxSegmentos.FechaHoraLlegada = oAnoDeparture & "-" & .ReservationItems(i).FlightSegment(0).ArrivalDateTime.ToString
                                                            End If

                                                        Else
                                                            auxSegmentos.FechaHoraLlegada = .ReservationItems(i).FlightSegment(0).ArrivalDateTime.ToString
                                                        End If

                                                    End If

                                                    '08-10T17:10:00
                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).UpdatedDepartureTime) Then

                                                        AuxUpdatedDepartureTime = .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime

                                                        If AuxUpdatedDepartureTime.Split("T")(0).Length = 5 Then

                                                            oDate = New Date
                                                            oDate = auxSegmentos.FechaHoraSalida
                                                            oMesArrival = oDate.Month
                                                            oAnoArrival = oDate.Year

                                                            oDate = New Date
                                                            oDate = oAnoArrival & "-" & AuxUpdatedDepartureTime
                                                            oAuxMesUP = oDate.Month

                                                            If CInt(oAuxMesUP) < CInt(oMesArrival) Then
                                                                auxSegmentos.UpDateFechaHoraSalida = CStr(CInt(oAnoArrival) + 1) & "-" & .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime.ToString
                                                                auxSegmentos.FechaHoraSalida = CStr(CInt(oAnoArrival) + 1) & "-" & .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime.ToString
                                                            Else
                                                                auxSegmentos.UpDateFechaHoraSalida = oAnoDeparture & "-" & .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime.ToString
                                                                auxSegmentos.FechaHoraSalida = oAnoDeparture & "-" & .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime.ToString
                                                            End If

                                                        Else
                                                            auxSegmentos.UpDateFechaHoraSalida = .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime
                                                            auxSegmentos.FechaHoraSalida = .ReservationItems(i).FlightSegment(0).UpdatedDepartureTime
                                                        End If

                                                    End If

                                                    '08-10T17:10:00
                                                    If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).UpdatedArrivalTime) Then
                                                        AuxUpdatedArrivalTime = .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime

                                                        If AuxUpdatedArrivalTime.Split("T")(0).Length = 5 Then

                                                            oDate = New Date
                                                            oDate = auxSegmentos.FechaHoraLlegada
                                                            oMesArrival = oDate.Month
                                                            oAnoArrival = oDate.Year

                                                            oDate = New Date
                                                            oDate = oAnoArrival & "-" & AuxUpdatedArrivalTime
                                                            oAuxMesUP = oDate.Month


                                                            If CInt(oAuxMesUP) < CInt(oMesArrival) Then
                                                                auxSegmentos.UpDateFechaHoraLlegada = CStr(CInt(oAnoArrival) + 1) & "-" & .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime.ToString
                                                                auxSegmentos.FechaHoraLlegada = CStr(CInt(oAnoArrival) + 1) & "-" & .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime.ToString
                                                            Else
                                                                auxSegmentos.UpDateFechaHoraLlegada = oAnoDeparture & "-" & .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime.ToString
                                                                auxSegmentos.FechaHoraLlegada = oAnoDeparture & "-" & .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime.ToString
                                                            End If

                                                        Else
                                                            auxSegmentos.UpDateFechaHoraLlegada = .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime
                                                            auxSegmentos.FechaHoraLlegada = .ReservationItems(i).FlightSegment(0).UpdatedArrivalTime
                                                        End If

                                                    End If

                                                    If .ReservationItems(i).FlightSegment(0).OriginLocation.LocationCode IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).OriginLocation.LocationCode) Then

                                                            objDAO = New DAO
                                                            objCiudad = New classCiudad
                                                            objCiudad = objDAO.ObtenerDatosCiudad(.ReservationItems(i).FlightSegment(0).OriginLocation.LocationCode, _
                                                                                                           strCodigoSeguimiento, _
                                                                                                           intFirmaDB, _
                                                                                                           intEsquema)


                                                            auxSegmentos.Salida = New classCiudad
                                                            auxSegmentos.Salida = objCiudad


                                                        End If
                                                    End If

                                                    If .ReservationItems(i).FlightSegment(0).DestinationLocation.LocationCode IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).DestinationLocation.LocationCode) Then

                                                            objDAO = New DAO
                                                            objCiudad = New classCiudad

                                                            objCiudad = objDAO.ObtenerDatosCiudad(.ReservationItems(i).FlightSegment(0).DestinationLocation.LocationCode, _
                                                                                                           strCodigoSeguimiento, _
                                                                                                           intFirmaDB, _
                                                                                                           intEsquema)


                                                            auxSegmentos.Llegada = New classCiudad
                                                            auxSegmentos.Llegada = objCiudad
                                                        End If
                                                    End If

                                                    If .ReservationItems(i).FlightSegment(0).OperatingAirline IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).OperatingAirline(0).Code) Then
                                                            auxSegmentos.OperadoPor = .ReservationItems(i).FlightSegment(0).OperatingAirline(0).Code
                                                        ElseIf Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).OperatingAirline(0).CompanyShortName) Then
                                                            auxSegmentos.OperadoPor = .ReservationItems(i).FlightSegment(0).OperatingAirline(0).CompanyShortName
                                                        End If

                                                        If Not String.IsNullOrEmpty(auxSegmentos.OperadoPor) Then
                                                            auxSegmentos.OperadoPor = ObtieneCodigoOperador(auxSegmentos.OperadoPor, _
                                                                                                            strCodigoSeguimiento, _
                                                                                                            intFirmaDB, _
                                                                                                            intEsquema)
                                                        End If
                                                    End If

                                                    If .ReservationItems(i).FlightSegment(0).SupplierRef IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).SupplierRef.ID) Then
                                                            auxSegmentos.CodigoAerolinea = .ReservationItems(i).FlightSegment(0).SupplierRef.ID.Split(Constantes.Asterisco)(1).ToString
                                                        End If
                                                    Else
                                                        If oCadenaError.ToString.IndexOf(Constantes.LocalizaroLineaAerea) = -1 Then
                                                            If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                                            objPNR.MSGError.Add(Constantes.LocalizaroLineaAerea)
                                                            oCadenaError.Append(Constantes.LocalizaroLineaAerea)
                                                        End If
                                                    End If


                                                    If .ReservationItems(i).FlightSegment(0).Status IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(.ReservationItems(i).FlightSegment(0).Status.ToString) Then
                                                            auxSegmentos.Status = .ReservationItems(i).FlightSegment(0).Status.ToString
                                                            If Not .ReservationItems(i).FlightSegment(0).Status.ToString.Equals(Constantes.IWS_STATUS_SEGMENT) Then
                                                                If oCadenaError.ToString.IndexOf(Constantes.StatusDiferente) = -1 Then
                                                                    If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                                                    objPNR.MSGError.Add(Constantes.StatusDiferente)
                                                                    oCadenaError.Append(Constantes.StatusDiferente)
                                                                End If
                                                            End If
                                                        End If
                                                    End If

                                                    If objSegmentos Is Nothing Then objSegmentos = New List(Of classSegmentos)
                                                    objSegmentos.Add(auxSegmentos)
                                                    auxSegmentos = Nothing


                                                    intContadorSegmento += 1

                                                    'End If
                                                End If

                                            Next

                                            objPNR.Segmentos = New List(Of classSegmentos)
                                            objPNR.Segmentos = objSegmentos
                                            objSegmentos = Nothing
                                        End With
                                    Else
                                        If oCadenaError.ToString.IndexOf(Constantes.NoExisteSegmentoActivo) = -1 Then
                                            If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                                            objPNR.MSGError.Add(Constantes.NoExisteSegmentoActivo)
                                            oCadenaError.Append(Constantes.NoExisteSegmentoActivo)
                                        End If
                                    End If
                                End If


                                '*******SSR - DOCS *******
                                If TravelItineraryReadRS.TravelItinerary.SpecialServiceInfo IsNot Nothing Then
                                    With TravelItineraryReadRS.TravelItinerary
                                        For i As Integer = 0 To .SpecialServiceInfo.Length - 1
                                            If .SpecialServiceInfo(i).Service IsNot Nothing Then
                                                If .SpecialServiceInfo(i).Service.SSR_Code IsNot Nothing And .SpecialServiceInfo(i).Service.SSR_Type IsNot Nothing Then

                                                    Dim strNumeroPasajero As String = Nothing
                                                    Dim strNombrePasajero As String = Nothing
                                                    Dim strTexto As String = Nothing
                                                    Dim strAuxTexto As String = Nothing
                                                    Dim strFechaNacimiento As String = Nothing
                                                    Dim strFechaExpiracion As String = Nothing
                                                    Dim strApellidoPaterno As String = Nothing
                                                    Dim strNacionalidad As String = Nothing
                                                    Dim strNacPasaporte As String = Nothing
                                                    Dim strPasaporte As String = Nothing
                                                    Dim strNacPasajero As String = Nothing
                                                    Dim strNombre As String = Nothing
                                                    Dim strGenero As String = Nothing
                                                    Dim strIndInfante As String = Nothing


                                                    If .SpecialServiceInfo(i).Service.SSR_Code.ToString = "SSR" And _
                                                       .SpecialServiceInfo(i).Service.SSR_Type.ToString = "DOCS" Then

                                                        With .SpecialServiceInfo(i).Service
                                                            strNumeroPasajero = Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(0))) & _
                                                                             Constantes.Punto & _
                                                                             Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(1)))

                                                            strNombrePasajero = .PersonName(0).Value
                                                            strAuxTexto = .Text.ToString
                                                        End With


                                                        'DB/04AUG78/M/PANDURO/OMAR
                                                        If strAuxTexto.IndexOf("DB/") > -1 Then
                                                            strFechaNacimiento = Nothing
                                                            strApellidoPaterno = Nothing
                                                            strNombre = Nothing
                                                            strGenero = Nothing

                                                            strTexto = strAuxTexto.Substring(strAuxTexto.IndexOf("DB/"), Len(strAuxTexto) - strAuxTexto.IndexOf("DB/"))

                                                            strFechaNacimiento = strTexto.Split("/")(1)
                                                            strGenero = strTexto.Split("/")(2)
                                                            If Len(strGenero) = 1 Then
                                                                strIndInfante = Nothing
                                                            Else
                                                                strIndInfante = strGenero.Substring(1, 1)
                                                                strGenero = strGenero.Substring(0, 1)
                                                            End If

                                                            strApellidoPaterno = strTexto.Split("/")(3)
                                                            strNombre = strTexto.Split("/")(4)

                                                            If strNombrePasajero = strApellidoPaterno & "/" & strNombre Then
                                                                For x As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                                    If strNumeroPasajero = objPNR.Pasajeros.Item(x).NumeroPasajero Then
                                                                        If Not String.IsNullOrEmpty(strFechaNacimiento) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            oDate = New Date
                                                                            oDate = CDate(strFechaNacimiento)
                                                                            objPNR.Pasajeros.Item(x).SSR.FechaNacimiento = oDate.ToString("dd/MM/yyyy")
                                                                        End If
                                                                        If Not String.IsNullOrEmpty(strGenero) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.Genero = strGenero
                                                                        End If
                                                                    End If
                                                                Next



                                                            ElseIf Not String.IsNullOrEmpty(strIndInfante) Then
                                                                If strIndInfante = "I" Then
                                                                    For x As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                                        If strNombrePasajero = objPNR.Pasajeros.Item(x).ApellidoPaterno & "/" & objPNR.Pasajeros.Item(x).NombrePasajero Then
                                                                            If Not objPNR.Pasajeros.Item(x).Infante Then
                                                                                objPNR.Pasajeros.Item(x).Infante = True
                                                                                If Not String.IsNullOrEmpty(strFechaNacimiento) Then
                                                                                    If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                    oDate = New Date
                                                                                    oDate = CDate(strFechaNacimiento)
                                                                                    objPNR.Pasajeros.Item(x).SSR.FechaNacimiento = oDate.ToString("dd/MM/yyyy")
                                                                                End If
                                                                                If Not String.IsNullOrEmpty(strGenero) Then
                                                                                    If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                    objPNR.Pasajeros.Item(x).SSR.Genero = strGenero
                                                                                End If
                                                                            End If
                                                                        End If
                                                                    Next
                                                                End If
                                                            End If


                                                        ElseIf strAuxTexto.IndexOf("P/") > -1 Then
                                                            'P/PE/45213321/PE/20MAR79/M/20MAR11/DE LA CRUZ/ROLANDO

                                                            strTexto = strAuxTexto.Substring(strAuxTexto.IndexOf("P/"), Len(strAuxTexto) - strAuxTexto.IndexOf("P/"))

                                                            strNacPasaporte = strTexto.Split("/")(1)
                                                            strPasaporte = strTexto.Split("/")(2)
                                                            strNacPasajero = strTexto.Split("/")(3)
                                                            strFechaNacimiento = strTexto.Split("/")(4)
                                                            strGenero = strTexto.Split("/")(5)
                                                            If strGenero.Length > 1 Then strIndInfante = strGenero.Substring(1, 1)
                                                            strFechaExpiracion = strTexto.Split("/")(6)
                                                            strApellidoPaterno = strTexto.Split("/")(7)
                                                            strNombre = strTexto.Split("/")(8)


                                                            If strNombrePasajero = strApellidoPaterno & "/" & strNombre Then
                                                                For x As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                                    If strNumeroPasajero = objPNR.Pasajeros.Item(x).NumeroPasajero Then
                                                                        oDate = New Date
                                                                        oDate = CDate(strFechaNacimiento)
                                                                        If Not String.IsNullOrEmpty(strFechaNacimiento) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            oDate = New Date
                                                                            oDate = CDate(strFechaNacimiento)
                                                                            objPNR.Pasajeros.Item(x).SSR.FechaNacimiento = oDate.ToString("dd/MM/yyyy")
                                                                        End If
                                                                        If Not String.IsNullOrEmpty(strGenero) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.Genero = strGenero
                                                                        End If
                                                                        If Not String.IsNullOrEmpty(strFechaExpiracion) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.FechaExpiracion = strFechaExpiracion
                                                                        End If
                                                                        '---
                                                                        If Not String.IsNullOrEmpty(strNacPasaporte) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.NacPasaporte = strNacPasaporte
                                                                        End If
                                                                        If Not String.IsNullOrEmpty(strPasaporte) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.Pasaporte = strPasaporte
                                                                        End If
                                                                        If Not String.IsNullOrEmpty(strNacPasajero) Then
                                                                            If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                            objPNR.Pasajeros.Item(x).SSR.NacPasajero = strNacPasajero
                                                                        End If
                                                                    End If
                                                                Next
                                                            ElseIf Not String.IsNullOrEmpty(strIndInfante) Then
                                                                If strIndInfante = "I" Then
                                                                    For x As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                                        If strNombrePasajero = objPNR.Pasajeros.Item(x).ApellidoPaterno & "/" & objPNR.Pasajeros.Item(x).NombrePasajero Then

                                                                            objPNR.Pasajeros.Item(x).Infante = True
                                                                            If Not String.IsNullOrEmpty(strFechaNacimiento) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                oDate = New Date
                                                                                oDate = CDate(strFechaNacimiento)
                                                                                objPNR.Pasajeros.Item(x).SSR.FechaNacimiento = oDate.ToString("dd/MM/yyyy")
                                                                            End If
                                                                            If Not String.IsNullOrEmpty(strGenero) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                objPNR.Pasajeros.Item(x).SSR.Genero = strGenero
                                                                            End If
                                                                            If Not String.IsNullOrEmpty(strFechaExpiracion) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                objPNR.Pasajeros.Item(x).SSR.FechaExpiracion = strFechaExpiracion
                                                                            End If
                                                                            '-----
                                                                            If Not String.IsNullOrEmpty(strNacPasaporte) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                objPNR.Pasajeros.Item(x).SSR.NacPasaporte = strNacPasaporte
                                                                            End If
                                                                            If Not String.IsNullOrEmpty(strPasaporte) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                objPNR.Pasajeros.Item(x).SSR.Pasaporte = strPasaporte
                                                                            End If
                                                                            If Not String.IsNullOrEmpty(strNacPasajero) Then
                                                                                If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                                objPNR.Pasajeros.Item(x).SSR.NacPasajero = strNacPasajero
                                                                            End If
                                                                        End If

                                                                    Next
                                                                End If
                                                            End If

                                                        End If
                                                    End If


                                                    If .SpecialServiceInfo(i).Service.SSR_Code.ToString = "SSR" And _
                                                       .SpecialServiceInfo(i).Service.SSR_Type.ToString = "INFT" Then


                                                        With .SpecialServiceInfo(i).Service
                                                            strNumeroPasajero = Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(0))) & _
                                                                             Constantes.Punto & _
                                                                             Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(1)))

                                                            strNombrePasajero = .PersonName(0).Value
                                                            strAuxTexto = .Text(0).ToString
                                                        End With


                                                        Dim Parceo() As String = strAuxTexto.Split("/")

                                                        oDate = New Date
                                                        oDate = CDate(Parceo(Parceo.Length - 1))
                                                        strFechaNacimiento = oDate.ToString("dd/MM/yyyy")
                                                        strNombre = Parceo(Parceo.Length - 2)
                                                        strApellidoPaterno = Parceo(Parceo.Length - 3)

                                                        For x As Integer = 0 To objPNR.Pasajeros.Count - 1
                                                            If strApellidoPaterno & Constantes.Slash & strNombre = objPNR.Pasajeros.Item(x).ApellidoPaterno & Constantes.Slash & objPNR.Pasajeros.Item(x).NombrePasajero Then
                                                                objPNR.Pasajeros.Item(x).Infante = True
                                                                If Not String.IsNullOrEmpty(strFechaNacimiento) Then
                                                                    If objPNR.Pasajeros.Item(x).SSR Is Nothing Then objPNR.Pasajeros.Item(x).SSR = New classSSR
                                                                    oDate = New Date
                                                                    oDate = CDate(strFechaNacimiento)
                                                                    objPNR.Pasajeros.Item(x).SSR.FechaNacimiento = strFechaNacimiento
                                                                    objPNR.Pasajeros.Item(x).AdultoAsociado = strNumeroPasajero
                                                                    Exit For
                                                                End If
                                                            End If
                                                        Next

                                                    End If


                                                    If .SpecialServiceInfo(i).Service.SSR_Code.ToString = "SSR" And _
                                                       .SpecialServiceInfo(i).Service.SSR_Type.ToString = "FOID" Then


                                                        With .SpecialServiceInfo(i).Service
                                                            strNumeroPasajero = Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(0))) & _
                                                                             Constantes.Punto & _
                                                                             Convert.ToString(Convert.ToInt64(.PersonName(0).NameNumber.ToString.Split(".")(1)))

                                                            strNombrePasajero = .PersonName(0).Value
                                                            strAuxTexto = .Text(0).Split("/")(1)
                                                        End With

                                                        For x As Integer = 0 To objPNR.Pasajeros.Count - 1

                                                            If strNumeroPasajero = objPNR.Pasajeros.Item(x).NumeroPasajero Then
                                                                If strAuxTexto.Substring(0, 2) = "NI" Then
                                                                    If strAuxTexto.Substring(0, 4) = "NICE" Then
                                                                        If objPNR.Pasajeros.Item(x).Documento Is Nothing Then

                                                                            auxDocumento = New classDocumento
                                                                            auxDocumento.Numero = strAuxTexto.Replace("NICE", "")
                                                                            auxDocumento.Tipo = Constantes.Id_CE
                                                                            auxDocumento.Num_Nombre = Constantes.Nombre_CE

                                                                            If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                                            objDocumento.Add(auxDocumento)

                                                                            If objPNR.Pasajeros.Item(x).Documento Is Nothing Then objPNR.Pasajeros.Item(x).Documento = New List(Of classDocumento)
                                                                            objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                            auxDocumento = Nothing
                                                                        Else

                                                                            Dim flag As Boolean = False

                                                                            auxDocumento = New classDocumento
                                                                            auxDocumento.Numero = strAuxTexto.Replace("NICE", "")
                                                                            auxDocumento.Tipo = Constantes.Id_CE
                                                                            auxDocumento.Num_Nombre = Constantes.Nombre_CE

                                                                            For y As Integer = 0 To objPNR.Pasajeros.Item(x).Documento.Count - 1
                                                                                If objPNR.Pasajeros.Item(x).Documento.Item(y).Tipo = auxDocumento.Tipo Then
                                                                                    flag = True
                                                                                    Exit For
                                                                                End If
                                                                            Next

                                                                            If Not flag Then
                                                                                objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                                auxDocumento = Nothing
                                                                            End If
                                                                        End If

                                                                    Else

                                                                        If objPNR.Pasajeros.Item(x).Documento Is Nothing Then
                                                                            auxDocumento = New classDocumento
                                                                            auxDocumento.Numero = strAuxTexto.Replace("NI", "")
                                                                            auxDocumento.Tipo = Constantes.Id_DNI
                                                                            auxDocumento.Num_Nombre = Constantes.Nombre_DNI

                                                                            If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                                            objDocumento.Add(auxDocumento)

                                                                            If objPNR.Pasajeros.Item(x).Documento Is Nothing Then objPNR.Pasajeros.Item(x).Documento = New List(Of classDocumento)
                                                                            objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                            auxDocumento = Nothing
                                                                        Else
                                                                            Dim flag As Boolean = False

                                                                            auxDocumento = New classDocumento
                                                                            auxDocumento.Numero = strAuxTexto.Replace("NI", "")
                                                                            auxDocumento.Tipo = Constantes.Id_DNI
                                                                            auxDocumento.Num_Nombre = Constantes.Nombre_DNI

                                                                            For y As Integer = 0 To objPNR.Pasajeros.Item(x).Documento.Count - 1
                                                                                If objPNR.Pasajeros.Item(x).Documento.Item(y).Tipo = auxDocumento.Tipo Then
                                                                                    flag = True
                                                                                    Exit For
                                                                                End If
                                                                            Next

                                                                            If Not flag Then
                                                                                objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                                auxDocumento = Nothing
                                                                            End If

                                                                        End If

                                                                    End If

                                                                ElseIf strAuxTexto.Substring(0, 2) = "ID" Then

                                                                    If objPNR.Pasajeros.Item(x).Documento Is Nothing Then
                                                                        auxDocumento = New classDocumento
                                                                        auxDocumento.Numero = strAuxTexto.Replace("IDPP", "")
                                                                        auxDocumento.Tipo = Constantes.Id_PASS
                                                                        auxDocumento.Num_Nombre = Constantes.Nombre_Pasaporte

                                                                        If objDocumento Is Nothing Then objDocumento = New List(Of classDocumento)
                                                                        objDocumento.Add(auxDocumento)

                                                                        If objPNR.Pasajeros.Item(x).Documento Is Nothing Then objPNR.Pasajeros.Item(x).Documento = New List(Of classDocumento)
                                                                        objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                        auxDocumento = Nothing
                                                                    Else
                                                                        Dim flag As Boolean = False

                                                                        auxDocumento = New classDocumento
                                                                        auxDocumento.Numero = strAuxTexto.Replace("IDPP", "")
                                                                        auxDocumento.Tipo = Constantes.Id_PASS
                                                                        auxDocumento.Num_Nombre = Constantes.Nombre_Pasaporte

                                                                        For y As Integer = 0 To objPNR.Pasajeros.Item(x).Documento.Count - 1
                                                                            If objPNR.Pasajeros.Item(x).Documento.Item(y).Tipo = auxDocumento.Tipo Then
                                                                                flag = True
                                                                                Exit For
                                                                            End If
                                                                        Next

                                                                        If Not flag Then
                                                                            objPNR.Pasajeros.Item(x).Documento.Add(auxDocumento)
                                                                            auxDocumento = Nothing
                                                                        End If

                                                                    End If

                                                                End If
                                                                Exit For
                                                            End If
                                                        Next
                                                    End If


                                                    strNumeroPasajero = Nothing
                                                    strNombrePasajero = Nothing
                                                    strTexto = Nothing
                                                    strAuxTexto = Nothing
                                                    strFechaNacimiento = Nothing
                                                    strFechaExpiracion = Nothing
                                                    strApellidoPaterno = Nothing
                                                    strNacionalidad = Nothing
                                                    strNombre = Nothing
                                                    strGenero = Nothing
                                                    strIndInfante = Nothing
                                                    oDate = Nothing


                                                End If
                                            End If
                                        Next

                                    End With
                                End If

                                '***** BOLETOS EMITIDOS ****

                                If TravelItineraryReadRS IsNot Nothing Then
                                    If TravelItineraryReadRS.TravelItinerary IsNot Nothing Then
                                        If TravelItineraryReadRS.TravelItinerary.ItineraryInfo IsNot Nothing Then
                                            If TravelItineraryReadRS.TravelItinerary.ItineraryInfo.Ticketing IsNot Nothing Then
                                                With TravelItineraryReadRS.TravelItinerary.ItineraryInfo
                                                    For i As Integer = 0 To .Ticketing.Length - 1


                                                        If .Ticketing(i).eTicketNumber IsNot Nothing Then

                                                            If objPNR.Boletos Is Nothing Then objPNR.Boletos = New List(Of classPNR.classBoletos)
                                                            auxBoleto = New classPNR.classBoletos

                                                            If Not String.IsNullOrEmpty(.Ticketing(i).RPH) Then
                                                                auxBoleto.ID = .Ticketing(i).RPH
                                                            End If

                                                            If Not String.IsNullOrEmpty(.Ticketing(i).eTicketNumber) Then
                                                                auxBoleto.Ticket = .Ticketing(i).eTicketNumber
                                                            End If

                                                            objPNR.Boletos.Add(auxBoleto)
                                                            auxBoleto = Nothing

                                                        End If

                                                    Next
                                                End With

                                            End If
                                        End If
                                    End If
                                End If

                            End If
                        End If


                    Else

                        If objPNR.MSGError Is Nothing Then objPNR.MSGError = New List(Of String)
                        objPNR.MSGError.Add("Se produjo un error al momento de recuperar el código")

                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strPNR = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objSession = Nothing
                objIWebServices = Nothing
                objIWebServices = Nothing
                intContadorSegmento = Nothing

                auxAsientos = Nothing
                auxPax = Nothing
            End Try

            Return objPNR
        End Function
        Public Function ModificarPNR(ByVal strDK As String, _
                                     ByVal objPasajeros As List(Of classPasajeros), _
                                     ByVal strCodigoSeguimiento As String, _
                                     ByVal intGDS As Integer, _
                                     ByVal intFirmaGDS As Integer, _
                                     ByVal intFirmaDB As Integer, _
                                     ByVal objSession As classSession) As classRespuesta

            Dim TravelItineraryModifyInfoRS As objSabreWS.TravelItineraryModifyInfo.TravelItineraryModifyInfoRS = Nothing
            Dim strRespuesta As String = Nothing

            Dim objRespuesta As classRespuesta = Nothing

            Try



                objIWebServices = New IWebServices
                TravelItineraryModifyInfoRS = objIWebServices._TravelItineraryModifyInfo(strDK, _
                                                                                         objPasajeros, _
                                                                                         strCodigoSeguimiento, _
                                                                                         intGDS, _
                                                                                         intFirmaGDS, _
                                                                                         intFirmaDB, _
                                                                                         objSession)

                If TravelItineraryModifyInfoRS IsNot Nothing Then

                    If TravelItineraryModifyInfoRS.ApplicationResults IsNot Nothing Then
                        If TravelItineraryModifyInfoRS.ApplicationResults.Success IsNot Nothing Then
                            If TravelItineraryModifyInfoRS.ApplicationResults.Success(0).SystemSpecificResults IsNot Nothing Then
                                With TravelItineraryModifyInfoRS.ApplicationResults.Success(0)
                                    For i As Integer = 0 To .SystemSpecificResults.Length - 1
                                        If .SystemSpecificResults(i).HostCommand IsNot Nothing Then
                                            If objRespuesta Is Nothing Then objRespuesta = New classRespuesta
                                            objRespuesta.Respuesta = New List(Of String)
                                            objRespuesta.Respuesta.Add(.SystemSpecificResults(i).HostCommand.Value)
                                        End If
                                    Next
                                End With
                            End If
                        End If


                        If TravelItineraryModifyInfoRS.ApplicationResults.Error IsNot Nothing Then
                            With TravelItineraryModifyInfoRS.ApplicationResults
                                For i As Integer = 0 To .Error.Length - 1
                                    If .Error(i).SystemSpecificResults IsNot Nothing Then
                                        For y As Integer = 0 To .Error(i).SystemSpecificResults.Length - 1

                                            Dim oRespuesta As String = ""

                                            If .Error(i).SystemSpecificResults(y).HostCommand IsNot Nothing Then
                                                oRespuesta &= IIf(oRespuesta = "", "", " / ") & "FORMATO: " & .Error(i).SystemSpecificResults(y).HostCommand.Value
                                            End If

                                            If .Error(i).SystemSpecificResults(y).Message IsNot Nothing Then
                                                If .Error(i).SystemSpecificResults(y).Message(0).Value IsNot Nothing Then
                                                    oRespuesta &= IIf(oRespuesta = "", "", " / ") & "ERROR: " & .Error(i).SystemSpecificResults(y).Message(0).Value
                                                End If
                                            End If

                                            If objRespuesta Is Nothing Then objRespuesta = New classRespuesta
                                            If objRespuesta.ErroresAlertas Is Nothing Then objRespuesta.ErroresAlertas = New classErroresAlertas
                                            If objRespuesta.ErroresAlertas.Errores Is Nothing Then objRespuesta.ErroresAlertas.Errores = New List(Of String)
                                            objRespuesta.ErroresAlertas.Errores.Add(oRespuesta)

                                        Next
                                    End If
                                Next
                            End With
                        End If


                        If TravelItineraryModifyInfoRS.ApplicationResults.Warning IsNot Nothing Then
                            With TravelItineraryModifyInfoRS.ApplicationResults
                                For i As Integer = 0 To .Error.Length - 1
                                    If .Warning(i).SystemSpecificResults IsNot Nothing Then
                                        For y As Integer = 0 To .Warning(i).SystemSpecificResults.Length - 1

                                            Dim oRespuesta As String = ""

                                            If .Warning(i).SystemSpecificResults(y).HostCommand IsNot Nothing Then
                                                oRespuesta &= IIf(oRespuesta = "", "", " / ") & "FORMATO: " & .Warning(i).SystemSpecificResults(y).HostCommand.Value
                                            End If

                                            If .Warning(i).SystemSpecificResults(y).Message IsNot Nothing Then
                                                If .Warning(i).SystemSpecificResults(y).Message(0).Value IsNot Nothing Then
                                                    oRespuesta &= IIf(oRespuesta = "", "", " / ") & "WARNING: " & .Warning(i).SystemSpecificResults(y).Message(0).Value
                                                End If
                                            End If

                                            If objRespuesta Is Nothing Then objRespuesta = New classRespuesta
                                            If objRespuesta.ErroresAlertas Is Nothing Then objRespuesta.ErroresAlertas = New classErroresAlertas
                                            If objRespuesta.ErroresAlertas.Alertas Is Nothing Then objRespuesta.ErroresAlertas.Alertas = New List(Of String)
                                            objRespuesta.ErroresAlertas.Alertas.Add(oRespuesta)

                                        Next
                                    End If
                                Next
                            End With
                        End If

                    Else
                        If objRespuesta Is Nothing Then objRespuesta = New classRespuesta
                        If objRespuesta.ErroresAlertas Is Nothing Then objRespuesta.ErroresAlertas = New classErroresAlertas
                        If objRespuesta.ErroresAlertas.Errores Is Nothing Then objRespuesta.ErroresAlertas.Errores = New List(Of String)
                        objRespuesta.ErroresAlertas.Errores.Add(Constantes.ProblemasModifyInfo)
                    End If
                Else
                    If objRespuesta Is Nothing Then objRespuesta = New classRespuesta
                    If objRespuesta.ErroresAlertas Is Nothing Then objRespuesta.ErroresAlertas = New classErroresAlertas
                    If objRespuesta.ErroresAlertas.Errores Is Nothing Then objRespuesta.ErroresAlertas.Errores = New List(Of String)
                    objRespuesta.ErroresAlertas.Errores.Add(Constantes.ProblemasModifyInfo)
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strDK = Nothing
                objPasajeros = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try

            Return objRespuesta

        End Function
        Public Function CotizarPNR(ByVal listNumeroPasajeros As List(Of String), _
                                   ByVal listTipoPasajeros As List(Of String), _
                                   ByVal listNumeroSegmentos As List(Of String), _
                                   ByVal strTipoTarifa As String, _
                                     ByVal strCodigoSeguimiento As String, _
                                     ByVal intGDS As Integer, _
                                     ByVal intFirmaGDS As Integer, _
                                     ByVal intFirmaDB As Integer, _
                                     ByVal intEsquema As Integer, _
                                   ByVal objSession As classSession, _
                                   ByVal strAccount As String, _
                                   ByVal strCorporateID As String, _
                                   ByVal strTouCode As String, _
                                   ByVal strNetRemit As String, _
                                   ByVal intRetenerTarifa As Integer) As classTarifa

            Dim OTA_AirPriceRS As objSabreWS.OTA_AirPrice.OTA_AirPriceRS = Nothing
            Dim objTarifa As classTarifa = Nothing
            Dim objTarifa_x_Tipo As classTarifa_x_Tipo = Nothing
            Dim auxobjFareBasis As classCorteTarifario.classFareBasis = Nothing
            Dim objDetTax As classMonto = Nothing
            Dim Queue As classMonto = Nothing
            Dim Fare As classMonto = Nothing

            Dim objFareBasis As classCorteTarifario.classFareBasis = Nothing
            Dim auxCiudadCorte As String = String.Empty
            Dim auxFareBasis As String = String.Empty

            Dim objauxclassCorte As classCorteTarifario.classCorte = Nothing
            Dim objCiudad As classCiudad = Nothing
            Dim objDAO As DAO = Nothing

            Try

                objIWebServices = New IWebServices
                OTA_AirPriceRS = objIWebServices._OTA_AirPrice(listNumeroPasajeros, _
                                                               listTipoPasajeros, _
                                                               listNumeroSegmentos, _
                                                               strTipoTarifa, _
                                                               strCodigoSeguimiento, _
                                                               intGDS, _
                                                               intFirmaGDS, _
                                                               intFirmaDB, _
                                                               objSession, _
                                                               strAccount, _
                                                               strCorporateID, _
                                                               strTouCode, _
                                                               strNetRemit, _
                                                               intRetenerTarifa)

                If OTA_AirPriceRS IsNot Nothing Then
                    If OTA_AirPriceRS.ApplicationResults IsNot Nothing Then


                        If OTA_AirPriceRS.ApplicationResults.Warning IsNot Nothing Then

                            For i As Integer = 0 To OTA_AirPriceRS.ApplicationResults.Warning.Length - 1
                                If OTA_AirPriceRS.ApplicationResults.Warning(i).SystemSpecificResults IsNot Nothing Then
                                    For y As Integer = 0 To OTA_AirPriceRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1

                                        Dim oRespuesta As String = ""

                                        With OTA_AirPriceRS.ApplicationResults.Warning(i)

                                            If .SystemSpecificResults(y).HostCommand IsNot Nothing Then
                                                oRespuesta &= IIf(oRespuesta = "", "", " / ") & "FORMATO: " & .SystemSpecificResults(y).HostCommand.Value.ToString
                                            End If

                                            If .SystemSpecificResults(y).Message IsNot Nothing Then
                                                oRespuesta &= IIf(oRespuesta = "", "", " / ") & "WARNING: " & .SystemSpecificResults(y).Message(0).Value
                                            End If

                                            If objTarifa Is Nothing Then objTarifa = New classTarifa
                                            If objTarifa.ErroresAlertas Is Nothing Then objTarifa.ErroresAlertas = New classErroresAlertas
                                            If objTarifa.ErroresAlertas.Alertas Is Nothing Then objTarifa.ErroresAlertas.Alertas = New List(Of String)
                                            objTarifa.ErroresAlertas.Alertas.Add(oRespuesta)

                                        End With
                                    Next
                                End If
                            Next

                        End If

                        If OTA_AirPriceRS.ApplicationResults.Error IsNot Nothing Then
                            For i As Integer = 0 To OTA_AirPriceRS.ApplicationResults.Error.Length - 1
                                If OTA_AirPriceRS.ApplicationResults.Error(i).SystemSpecificResults IsNot Nothing Then
                                    For y As Integer = 0 To OTA_AirPriceRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1

                                        Dim oRespuesta As String = ""

                                        With OTA_AirPriceRS.ApplicationResults.Error(i)

                                            If .SystemSpecificResults(y).HostCommand IsNot Nothing Then
                                                oRespuesta &= IIf(oRespuesta = "", "", " / ") & "FORMATO: " & .SystemSpecificResults(y).HostCommand.Value.ToString
                                            End If

                                            If .SystemSpecificResults(y).Message IsNot Nothing Then
                                                oRespuesta &= IIf(oRespuesta = "", "", " / ") & "ERROR: " & .SystemSpecificResults(y).Message(0).Value
                                            End If

                                            If objTarifa Is Nothing Then objTarifa = New classTarifa
                                            If objTarifa.ErroresAlertas Is Nothing Then objTarifa.ErroresAlertas = New classErroresAlertas
                                            If objTarifa.ErroresAlertas.Errores Is Nothing Then objTarifa.ErroresAlertas.Errores = New List(Of String)
                                            objTarifa.ErroresAlertas.Errores.Add(oRespuesta)

                                        End With
                                    Next
                                End If
                            Next
                        End If


                        If OTA_AirPriceRS.ApplicationResults.Success IsNot Nothing Then
                            If OTA_AirPriceRS.ApplicationResults.status = GDS_NM_WebServicesSabre.OTA_AirPrice.CompletionCodes.Complete Then


                                If objTarifa Is Nothing Then objTarifa = New classTarifa

                                For i As Integer = 0 To OTA_AirPriceRS.ApplicationResults.Success.Length - 1
                                    objTarifa.TimeStamp = OTA_AirPriceRS.ApplicationResults.Success(i).timeStamp.ToString

                                    For x As Integer = 0 To OTA_AirPriceRS.ApplicationResults.Success(i).SystemSpecificResults.Length - 1
                                        If OTA_AirPriceRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand IsNot Nothing Then
                                            objTarifa.LNIATA = OTA_AirPriceRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.LNIATA
                                            objTarifa.HostCommand = OTA_AirPriceRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.Value
                                        End If
                                    Next
                                Next


                                If OTA_AirPriceRS.PriceQuote IsNot Nothing Then
                                    If OTA_AirPriceRS.PriceQuote.PricedItinerary IsNot Nothing Then
                                        With OTA_AirPriceRS.PriceQuote.PricedItinerary



                                            objTarifa.TotalReserva = New classMonto

                                            If OTA_AirPriceRS.PriceQuote.MiscInformation IsNot Nothing Then
                                                If OTA_AirPriceRS.PriceQuote.MiscInformation.HeaderInformation IsNot Nothing Then
                                                    If OTA_AirPriceRS.PriceQuote.MiscInformation.HeaderInformation.ValidatingCarrier IsNot Nothing Then
                                                        If OTA_AirPriceRS.PriceQuote.MiscInformation.HeaderInformation.ValidatingCarrier.Code IsNot Nothing Then
                                                            objTarifa.LineaValidadora = OTA_AirPriceRS.PriceQuote.MiscInformation.HeaderInformation.ValidatingCarrier.Code.ToString
                                                        End If
                                                    End If
                                                End If
                                            End If

                                            If OTA_AirPriceRS.PriceQuote.MiscInformation IsNot Nothing Then
                                                If OTA_AirPriceRS.PriceQuote.MiscInformation.HeaderInformation IsNot Nothing Then
                                                    If OTA_AirPriceRS.PriceQuote.MiscInformation.HeaderInformation.LastTicketingDate IsNot Nothing Then
                                                        objTarifa.LastTicketing = OTA_AirPriceRS.PriceQuote.MiscInformation.HeaderInformation.LastTicketingDate.ToString
                                                    End If
                                                End If
                                            End If


                                            If .CurrencyCode IsNot Nothing Then
                                                objTarifa.TotalReserva.Moneda = .CurrencyCode.ToString
                                            End If

                                            If .TotalAmount IsNot Nothing Then
                                                objTarifa.TotalReserva.Monto = .TotalAmount.ToString
                                            End If


                                            If .AirItineraryPricingInfo IsNot Nothing Then
                                                For i As Integer = 0 To .AirItineraryPricingInfo.Length - 1
                                                    objTarifa_x_Tipo = New classTarifa_x_Tipo

                                                    If .AirItineraryPricingInfo(i).FareCalculation IsNot Nothing Then
                                                        objTarifa_x_Tipo.FareCalculation = .AirItineraryPricingInfo(i).FareCalculation.Text.ToString
                                                    End If

                                                    If .AirItineraryPricingInfo(i).PassengerTypeQuantity IsNot Nothing Then
                                                        If .AirItineraryPricingInfo(i).PassengerTypeQuantity.Code IsNot Nothing Then
                                                            objTarifa_x_Tipo.TipoPax = .AirItineraryPricingInfo(i).PassengerTypeQuantity.Code.ToString
                                                        End If
                                                        If .AirItineraryPricingInfo(i).PassengerTypeQuantity.Quantity IsNot Nothing Then
                                                            objTarifa_x_Tipo.CantidadPax = .AirItineraryPricingInfo(i).PassengerTypeQuantity.Quantity.ToString
                                                        End If
                                                    End If

                                                    If .AirItineraryPricingInfo(i).ItinTotalFare IsNot Nothing Then
                                                        If .AirItineraryPricingInfo(i).ItinTotalFare.BaseFare IsNot Nothing Then
                                                            objTarifa_x_Tipo.TarifaNeta = New classMonto

                                                            If .AirItineraryPricingInfo(i).ItinTotalFare.BaseFare.Amount IsNot Nothing Then
                                                                objTarifa_x_Tipo.TarifaNeta.Monto = .AirItineraryPricingInfo(i).ItinTotalFare.BaseFare.Amount.ToString
                                                            End If

                                                            If .AirItineraryPricingInfo(i).ItinTotalFare.BaseFare.CurrencyCode IsNot Nothing Then
                                                                objTarifa_x_Tipo.TarifaNeta.Moneda = .AirItineraryPricingInfo(i).ItinTotalFare.BaseFare.CurrencyCode.ToString
                                                            End If
                                                        End If

                                                        If .AirItineraryPricingInfo(i).ItinTotalFare.TotalFare IsNot Nothing Then
                                                            objTarifa_x_Tipo.TotalPax = New classMonto
                                                            If .AirItineraryPricingInfo(i).ItinTotalFare.TotalFare.CurrencyCode IsNot Nothing Then
                                                                objTarifa_x_Tipo.TotalPax.Moneda = .AirItineraryPricingInfo(i).ItinTotalFare.TotalFare.CurrencyCode.ToString
                                                            End If

                                                            If .AirItineraryPricingInfo(i).ItinTotalFare.TotalFare.Amount IsNot Nothing Then
                                                                objTarifa_x_Tipo.TotalPax.Monto = .AirItineraryPricingInfo(i).ItinTotalFare.TotalFare.Amount.ToString
                                                            End If
                                                        End If

                                                        If .AirItineraryPricingInfo(i).ItinTotalFare.Taxes IsNot Nothing Then

                                                            If .AirItineraryPricingInfo(i).ItinTotalFare.Taxes.TotalAmount IsNot Nothing Then
                                                                objTarifa_x_Tipo.TotalImpuestos = .AirItineraryPricingInfo(i).ItinTotalFare.Taxes.TotalAmount.ToString
                                                            End If

                                                            If .AirItineraryPricingInfo(i).ItinTotalFare.Taxes.Tax IsNot Nothing Then

                                                                For z As Integer = 0 To .AirItineraryPricingInfo(i).ItinTotalFare.Taxes.Tax.Length - 1

                                                                    objDetTax = New classMonto
                                                                    If .AirItineraryPricingInfo(i).ItinTotalFare.Taxes.Tax(z).TicketingTaxCode IsNot Nothing Then
                                                                        objDetTax.Codigo = .AirItineraryPricingInfo(i).ItinTotalFare.Taxes.Tax(z).TicketingTaxCode.ToString
                                                                    End If

                                                                    If .AirItineraryPricingInfo(i).ItinTotalFare.Taxes.Tax(z).Amount IsNot Nothing Then
                                                                        objDetTax.Monto = .AirItineraryPricingInfo(i).ItinTotalFare.Taxes.Tax(z).Amount.ToString
                                                                    End If

                                                                    If .AirItineraryPricingInfo(i).ItinTotalFare.Taxes.Tax(z).TicketingTaxCode.Equals("PE") Then
                                                                        objTarifa_x_Tipo.IGV = objDetTax
                                                                    Else
                                                                        If objTarifa_x_Tipo.DetalleImpuestos Is Nothing Then objTarifa_x_Tipo.DetalleImpuestos = New List(Of classMonto)
                                                                        objTarifa_x_Tipo.DetalleImpuestos.Add(objDetTax)
                                                                    End If

                                                                    objDetTax = Nothing

                                                                Next
                                                            End If

                                                            If objTarifa_x_Tipo.IGV IsNot Nothing Then
                                                                If objTarifa_x_Tipo.IGV.Codigo.Equals("PE") Then
                                                                    objTarifa_x_Tipo.TotalImpuestos -= objTarifa_x_Tipo.IGV.Monto
                                                                End If
                                                            End If

                                                        End If

                                                    End If


                                                    If .AirItineraryPricingInfo(i).PTC_FareBreakdown IsNot Nothing Then

                                                        For y As Integer = 0 To .AirItineraryPricingInfo(i).PTC_FareBreakdown.Length - 1
                                                            If .AirItineraryPricingInfo(i).PTC_FareBreakdown(y).Surcharges IsNot Nothing Then
                                                                For z As Integer = 0 To .AirItineraryPricingInfo(i).PTC_FareBreakdown(y).Surcharges.Length - 1
                                                                    Queue = New classMonto

                                                                    If .AirItineraryPricingInfo(i).PTC_FareBreakdown(y).Surcharges(z).Ind IsNot Nothing Then
                                                                        Queue.Codigo = .AirItineraryPricingInfo(i).PTC_FareBreakdown(y).Surcharges(z).Ind.ToString
                                                                    End If

                                                                    If .AirItineraryPricingInfo(i).PTC_FareBreakdown(y).Surcharges(z).Value IsNot Nothing Then
                                                                        Queue.Monto = .AirItineraryPricingInfo(i).PTC_FareBreakdown(y).Surcharges(z).Value.ToString
                                                                    End If


                                                                    If objTarifa_x_Tipo.Queue Is Nothing Then objTarifa_x_Tipo.Queue = New List(Of classMonto)
                                                                    objTarifa_x_Tipo.Queue.Add(Queue)
                                                                    Queue = Nothing
                                                                    '----------
                                                                Next
                                                            End If

                                                            If .AirItineraryPricingInfo(i).PTC_FareBreakdown(y).FareBasis IsNot Nothing Then

                                                                If .AirItineraryPricingInfo(i).PTC_FareBreakdown(y).FareBasis.FareAmount IsNot Nothing Then
                                                                    Fare = New classMonto
                                                                    Fare.Monto = .AirItineraryPricingInfo(i).PTC_FareBreakdown(y).FareBasis.FareAmount
                                                                    If objTarifa_x_Tipo.Fare Is Nothing Then objTarifa_x_Tipo.Fare = New List(Of classMonto)
                                                                    objTarifa_x_Tipo.Fare.Add(Fare)
                                                                    Fare = Nothing
                                                                End If

                                                                If .AirItineraryPricingInfo(i).PTC_FareBreakdown(y).FareBasis.Market IsNot Nothing Then

                                                                    If auxCiudadCorte.IndexOf(.AirItineraryPricingInfo(i).PTC_FareBreakdown(y).FareBasis.Market.ToString) = -1 Then
                                                                        If objTarifa_x_Tipo.CorteTarifario Is Nothing Then objTarifa_x_Tipo.CorteTarifario = New classCorteTarifario
                                                                        If objTarifa_x_Tipo.CorteTarifario.CiudadesCorte Is Nothing Then objTarifa_x_Tipo.CorteTarifario.CiudadesCorte = New List(Of classCorteTarifario.classCorte)

                                                                        objauxclassCorte = New classCorteTarifario.classCorte
                                                                        objCiudad = New classCiudad
                                                                        Dim FDSFDS As String = .AirItineraryPricingInfo(i).PTC_FareBreakdown(y).FareBasis.Market.ToString.Substring(3, 3)

                                                                        objDAO = New DAO
                                                                        objCiudad = objDAO.ObtenerDatosCiudad(.AirItineraryPricingInfo(i).PTC_FareBreakdown(y).FareBasis.Market.ToString.Substring(3, 3), _
                                                                                                                      strCodigoSeguimiento, _
                                                                                                                      intFirmaDB, _
                                                                                                                      intEsquema)

                                                                        objauxclassCorte.CiudadesCorte = .AirItineraryPricingInfo(i).PTC_FareBreakdown(y).FareBasis.Market.ToString
                                                                        objauxclassCorte.Ciudad = objCiudad

                                                                        objTarifa_x_Tipo.CorteTarifario.CiudadesCorte.Add(objauxclassCorte)
                                                                        auxCiudadCorte &= IIf(auxCiudadCorte = "", "", Constantes.Slash) & .AirItineraryPricingInfo(i).PTC_FareBreakdown(y).FareBasis.Market.ToString
                                                                    End If

                                                                End If

                                                                If .AirItineraryPricingInfo(i).PTC_FareBreakdown(y).FareBasis.Code IsNot Nothing Then

                                                                    If objTarifa_x_Tipo.CorteTarifario Is Nothing Then objTarifa_x_Tipo.CorteTarifario = New classCorteTarifario

                                                                    auxobjFareBasis = New classCorteTarifario.classFareBasis
                                                                    auxobjFareBasis.sFareBasis = .AirItineraryPricingInfo(i).PTC_FareBreakdown(y).FareBasis.Code
                                                                    auxobjFareBasis.RPH = (y + 1)

                                                                    If objTarifa_x_Tipo.CorteTarifario.FareBasis Is Nothing Then objTarifa_x_Tipo.CorteTarifario.FareBasis = New List(Of classCorteTarifario.classFareBasis)
                                                                    objTarifa_x_Tipo.CorteTarifario.FareBasis.Add(auxobjFareBasis)

                                                                    If auxFareBasis.IndexOf(.AirItineraryPricingInfo(i).PTC_FareBreakdown(y).FareBasis.Code) = -1 Then
                                                                        auxFareBasis &= IIf(auxFareBasis = "", "", Constantes.PuntoComa) & .AirItineraryPricingInfo(i).PTC_FareBreakdown(y).FareBasis.Code.ToString
                                                                    End If

                                                                End If

                                                            End If
                                                        Next



                                                    End If

                                                    If objTarifa.Tarifa_x_Pax Is Nothing Then objTarifa.Tarifa_x_Pax = New List(Of classTarifa_x_Tipo)
                                                    objTarifa.Tarifa_x_Pax.Add(objTarifa_x_Tipo)
                                                    objTarifa_x_Tipo = Nothing
                                                    auxFareBasis = String.Empty
                                                    auxCiudadCorte = String.Empty
                                                    auxobjFareBasis = Nothing
                                                Next

                                            End If

                                        End With
                                    End If
                                End If
                            End If
                        End If



                    End If

                Else
                    objTarifa = New classTarifa
                    objTarifa.ErroresAlertas = New classErroresAlertas
                    If objTarifa.ErroresAlertas.Errores Is Nothing Then objTarifa.ErroresAlertas.Errores = New List(Of String)
                    objTarifa.ErroresAlertas.Errores.Add(Constantes.ProblemasOTA_AirPrice)
                End If



            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                listNumeroPasajeros = Nothing
                listTipoPasajeros = Nothing
                listNumeroSegmentos = Nothing
                strTipoTarifa = Nothing
                strAccount = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objSession = Nothing

                OTA_AirPriceRS = Nothing
                objTarifa_x_Tipo = Nothing
                auxobjFareBasis = Nothing
                objDetTax = Nothing
                Queue = Nothing
                Fare = Nothing
                objFareBasis = Nothing
                auxCiudadCorte = Nothing
                auxFareBasis = Nothing
            End Try

            Return objTarifa

        End Function
        Public Function VoidTicket(ByVal strLinea As String, _
                                   ByVal strCodigoSeguimiento As String, _
                                   ByVal intGDS As Integer, _
                                   ByVal intFirmaGDS As Integer, _
                                   ByVal intFirmaDB As Integer, _
                                   ByVal objSession As classSession) As String()

            Dim VoidTicketRS As objSabreWS.VoidTicket.VoidTicketRS = Nothing
            Dim strRespuesta() As String = Nothing
            Dim strMensaje As String = Nothing

            Try



                objIWebServices = New IWebServices
                VoidTicketRS = objIWebServices._VoidTicket(strLinea, _
                                                           strCodigoSeguimiento, _
                                                           intGDS, _
                                                           intFirmaGDS, _
                                                           intFirmaDB, _
                                                           objSession)

                If VoidTicketRS IsNot Nothing Then
                    If VoidTicketRS.ApplicationResults IsNot Nothing Then

                        If VoidTicketRS.Text IsNot Nothing Then
                            ReDim strRespuesta(0)
                            strRespuesta(strRespuesta.Length - 1) = "CONFIRMACION: " & VoidTicketRS.Text.ToString
                        End If

                        If VoidTicketRS.ApplicationResults.Error IsNot Nothing Then
                            For i As Integer = 0 To VoidTicketRS.ApplicationResults.Error.Length - 1
                                If VoidTicketRS.ApplicationResults.Error(i).SystemSpecificResults IsNot Nothing Then
                                    For y As Integer = 0 To VoidTicketRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                        If VoidTicketRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message IsNot Nothing Then
                                            For z As Integer = 0 To VoidTicketRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message.Length - 1
                                                If VoidTicketRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value IsNot Nothing Then

                                                    If strRespuesta Is Nothing Then
                                                        ReDim strRespuesta(0)
                                                    Else
                                                        ReDim Preserve strRespuesta(strRespuesta.Length)
                                                    End If
                                                    strRespuesta(strRespuesta.Length - 1) = "ERROR: " & VoidTicketRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value

                                                End If
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If

                        If VoidTicketRS.ApplicationResults.Warning IsNot Nothing Then
                            For i As Integer = 0 To VoidTicketRS.ApplicationResults.Warning.Length - 1
                                If VoidTicketRS.ApplicationResults.Warning(i).SystemSpecificResults IsNot Nothing Then
                                    For y As Integer = 0 To VoidTicketRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                                        If VoidTicketRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message IsNot Nothing Then
                                            For z As Integer = 0 To VoidTicketRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message.Length - 1
                                                If VoidTicketRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value IsNot Nothing Then

                                                    If strRespuesta Is Nothing Then
                                                        ReDim strRespuesta(0)
                                                    Else
                                                        ReDim Preserve strRespuesta(strRespuesta.Length)
                                                    End If
                                                    strRespuesta(strRespuesta.Length - 1) = "WARNING: " & VoidTicketRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value

                                                End If
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If

                    End If
                Else

                    ReDim strRespuesta(0)
                    strRespuesta(strRespuesta.Length - 1) = "ERROR: " & Constantes.ProblemasVOID_Ticket
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                VoidTicketRS = Nothing
                strLinea = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try

            Return strRespuesta

        End Function
        Public Function AddRemark(ByVal listRemark As List(Of String), _
                                  ByVal strTipo As String, _
                                  ByVal strCodigoSeguimiento As String, _
                                  ByVal intGDS As Integer, _
                                  ByVal intFirmaGDS As Integer, _
                                  ByVal intFirmaDB As Integer, _
                                  ByVal objSession As classSession) As String()

            Dim AddRemarkRS As objSabreWS.AddRemark.AddRemarkRS = Nothing
            Dim strRespuesta() As String = Nothing
            Dim strMensaje As String = Nothing

            Try



                objIWebServices = New IWebServices
                AddRemarkRS = objIWebServices._AddRemark(listRemark, _
                                                         strTipo, _
                                                         strCodigoSeguimiento, _
                                                         intGDS, _
                                                         intFirmaGDS, _
                                                         intFirmaDB, _
                                                         objSession)

                If AddRemarkRS IsNot Nothing Then
                    If AddRemarkRS.ApplicationResults IsNot Nothing Then

                        If AddRemarkRS.ApplicationResults.Success IsNot Nothing Then
                            For i As Integer = 0 To AddRemarkRS.ApplicationResults.Success.Length - 1
                                For y As Integer = 0 To AddRemarkRS.ApplicationResults.Success(i).SystemSpecificResults.Length - 1
                                    If AddRemarkRS.ApplicationResults.Success(i).SystemSpecificResults IsNot Nothing Then

                                        If strRespuesta Is Nothing Then
                                            ReDim strRespuesta(0)
                                        Else
                                            ReDim Preserve strRespuesta(strRespuesta.Length)
                                        End If
                                        strRespuesta(strRespuesta.Length - 1) = AddRemarkRS.ApplicationResults.Success(i).SystemSpecificResults(y).HostCommand.Value
                                    End If
                                Next
                            Next

                        End If


                        If AddRemarkRS.ApplicationResults.Error IsNot Nothing Then
                            For i As Integer = 0 To AddRemarkRS.ApplicationResults.Error.Length - 1
                                If AddRemarkRS.ApplicationResults.Error(i).SystemSpecificResults IsNot Nothing Then
                                    For y As Integer = 0 To AddRemarkRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                        If AddRemarkRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message IsNot Nothing Then
                                            For z As Integer = 0 To AddRemarkRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message.Length - 1
                                                If AddRemarkRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value IsNot Nothing Then

                                                    If strRespuesta Is Nothing Then
                                                        ReDim strRespuesta(0)
                                                    Else
                                                        ReDim Preserve strRespuesta(strRespuesta.Length)
                                                    End If
                                                    strRespuesta(strRespuesta.Length - 1) = "ERROR: " & AddRemarkRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value

                                                End If
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If

                        If AddRemarkRS.ApplicationResults.Warning IsNot Nothing Then
                            For i As Integer = 0 To AddRemarkRS.ApplicationResults.Warning.Length - 1
                                If AddRemarkRS.ApplicationResults.Warning(i).SystemSpecificResults IsNot Nothing Then
                                    For y As Integer = 0 To AddRemarkRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                                        If AddRemarkRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message IsNot Nothing Then
                                            For z As Integer = 0 To AddRemarkRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message.Length - 1
                                                If AddRemarkRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value IsNot Nothing Then

                                                    If strRespuesta Is Nothing Then
                                                        ReDim strRespuesta(0)
                                                    Else
                                                        ReDim Preserve strRespuesta(strRespuesta.Length)
                                                    End If
                                                    strRespuesta(strRespuesta.Length - 1) = "WARNING: " & AddRemarkRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value

                                                End If
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If

                    End If
                Else

                    ReDim strRespuesta(0)
                    strRespuesta(strRespuesta.Length - 1) = "ERROR: " & Constantes.ProblemasRemark
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                AddRemarkRS = Nothing
                listRemark = Nothing
                strTipo = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try

            Return strRespuesta

        End Function
        Public Function ModifyRemark(ByVal strTipo As String, _
                                  ByVal intLinea As String, _
                                  ByVal strRemark As String, _
                                  ByVal strCodigoSeguimiento As String, _
                                  ByVal intGDS As Integer, _
                                  ByVal intFirmaGDS As Integer, _
                                  ByVal intFirmaDB As Integer, _
                                  ByVal objSession As classSession) As String()

            Dim ModifyRemarkRS As objSabreWS.ModifyRemark.ModifyRemarkRS = Nothing
            Dim strRespuesta() As String = Nothing
            Dim strMensaje As String = Nothing

            Try



                objIWebServices = New IWebServices
                ModifyRemarkRS = objIWebServices._ModifyRemark(strTipo, _
                                                               intLinea, _
                                                               strRemark, _
                                                               strCodigoSeguimiento, _
                                                               intGDS, _
                                                               intFirmaGDS, _
                                                               intFirmaDB, _
                                                               objSession)

                If ModifyRemarkRS IsNot Nothing Then
                    If ModifyRemarkRS.ApplicationResults IsNot Nothing Then

                        If ModifyRemarkRS.ApplicationResults.Success IsNot Nothing Then
                            For i As Integer = 0 To ModifyRemarkRS.ApplicationResults.Success.Length - 1
                                For y As Integer = 0 To ModifyRemarkRS.ApplicationResults.Success(i).SystemSpecificResults.Length - 1
                                    If ModifyRemarkRS.ApplicationResults.Success(i).SystemSpecificResults IsNot Nothing Then

                                        If strRespuesta Is Nothing Then
                                            ReDim strRespuesta(0)
                                        Else
                                            ReDim Preserve strRespuesta(strRespuesta.Length)
                                        End If
                                        strRespuesta(strRespuesta.Length - 1) = ModifyRemarkRS.ApplicationResults.Success(i).SystemSpecificResults(y).HostCommand.Value
                                    End If
                                Next
                            Next

                        End If


                        If ModifyRemarkRS.ApplicationResults.Error IsNot Nothing Then
                            For i As Integer = 0 To ModifyRemarkRS.ApplicationResults.Error.Length - 1
                                If ModifyRemarkRS.ApplicationResults.Error(i).SystemSpecificResults IsNot Nothing Then
                                    For y As Integer = 0 To ModifyRemarkRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                        If ModifyRemarkRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message IsNot Nothing Then
                                            For z As Integer = 0 To ModifyRemarkRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message.Length - 1
                                                If ModifyRemarkRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value IsNot Nothing Then

                                                    If strRespuesta Is Nothing Then
                                                        ReDim strRespuesta(0)
                                                    Else
                                                        ReDim Preserve strRespuesta(strRespuesta.Length)
                                                    End If
                                                    strRespuesta(strRespuesta.Length - 1) = "ERROR: " & ModifyRemarkRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value

                                                End If
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If

                        If ModifyRemarkRS.ApplicationResults.Warning IsNot Nothing Then
                            For i As Integer = 0 To ModifyRemarkRS.ApplicationResults.Warning.Length - 1
                                If ModifyRemarkRS.ApplicationResults.Warning(i).SystemSpecificResults IsNot Nothing Then
                                    For y As Integer = 0 To ModifyRemarkRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                                        If ModifyRemarkRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message IsNot Nothing Then
                                            For z As Integer = 0 To ModifyRemarkRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message.Length - 1
                                                If ModifyRemarkRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value IsNot Nothing Then

                                                    If strRespuesta Is Nothing Then
                                                        ReDim strRespuesta(0)
                                                    Else
                                                        ReDim Preserve strRespuesta(strRespuesta.Length)
                                                    End If
                                                    strRespuesta(strRespuesta.Length - 1) = "WARNING: " & ModifyRemarkRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value

                                                End If
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If

                    End If
                Else

                    ReDim strRespuesta(0)
                    strRespuesta(strRespuesta.Length - 1) = "ERROR: " & Constantes.ProblemasRemark
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                ModifyRemarkRS = Nothing
                intLinea = Nothing
                strRemark = Nothing
                strTipo = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try

            Return strRespuesta

        End Function
        Public Function SpecialService(ByVal listPasajero As List(Of classPasajeros), _
                                       ByVal strTipo As String, _
                                       ByVal bolAmerican As Boolean, _
                                       ByVal strCodigoSeguimiento As String, _
                                       ByVal intGDS As Integer, _
                                       ByVal intFirmaGDS As Integer, _
                                       ByVal intFirmaDB As Integer, _
                                       ByVal objSession As classSession) As String()

            Dim SpecialServiceRS As objSabreWS.SpecialService.SpecialServiceRS = Nothing
            Dim strRespuesta() As String = Nothing
            Dim strMensaje As String = Nothing

            Try



                objIWebServices = New IWebServices
                SpecialServiceRS = objIWebServices._SpecialService(listPasajero, _
                                                                   strTipo, _
                                                                   bolAmerican, _
                                                                   strCodigoSeguimiento, _
                                                                   intGDS, _
                                                                   intFirmaGDS, _
                                                                   intFirmaDB, _
                                                                   objSession)

                If SpecialServiceRS IsNot Nothing Then
                    If SpecialServiceRS.ApplicationResults IsNot Nothing Then

                        If SpecialServiceRS.ApplicationResults.Success IsNot Nothing Then
                            For i As Integer = 0 To SpecialServiceRS.ApplicationResults.Success.Length - 1
                                For y As Integer = 0 To SpecialServiceRS.ApplicationResults.Success(i).SystemSpecificResults.Length - 1
                                    If SpecialServiceRS.ApplicationResults.Success(i).SystemSpecificResults IsNot Nothing Then

                                        If strRespuesta Is Nothing Then
                                            ReDim strRespuesta(0)
                                        Else
                                            ReDim Preserve strRespuesta(strRespuesta.Length)
                                        End If
                                        strRespuesta(strRespuesta.Length - 1) = SpecialServiceRS.ApplicationResults.Success(i).SystemSpecificResults(y).HostCommand.Value
                                    End If
                                Next
                            Next

                        End If


                        If SpecialServiceRS.ApplicationResults.Error IsNot Nothing Then
                            For i As Integer = 0 To SpecialServiceRS.ApplicationResults.Error.Length - 1
                                If SpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults IsNot Nothing Then
                                    For y As Integer = 0 To SpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                        If SpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message IsNot Nothing Then
                                            For z As Integer = 0 To SpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message.Length - 1
                                                If SpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value IsNot Nothing Then

                                                    If strRespuesta Is Nothing Then
                                                        ReDim strRespuesta(0)
                                                    Else
                                                        ReDim Preserve strRespuesta(strRespuesta.Length)
                                                    End If
                                                    strRespuesta(strRespuesta.Length - 1) = "ERROR: " & SpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value

                                                End If
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If

                        If SpecialServiceRS.ApplicationResults.Warning IsNot Nothing Then
                            For i As Integer = 0 To SpecialServiceRS.ApplicationResults.Warning.Length - 1
                                If SpecialServiceRS.ApplicationResults.Warning(i).SystemSpecificResults IsNot Nothing Then
                                    For y As Integer = 0 To SpecialServiceRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                                        If SpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message IsNot Nothing Then
                                            For z As Integer = 0 To SpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message.Length - 1
                                                If SpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value IsNot Nothing Then

                                                    If strRespuesta Is Nothing Then
                                                        ReDim strRespuesta(0)
                                                    Else
                                                        ReDim Preserve strRespuesta(strRespuesta.Length)
                                                    End If
                                                    strRespuesta(strRespuesta.Length - 1) = "WARNING: " & SpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value

                                                End If
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If

                    End If
                Else

                    ReDim strRespuesta(0)
                    strRespuesta(strRespuesta.Length - 1) = "ERROR: " & Constantes.ProblemasRemark
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                SpecialServiceRS = Nothing
                listPasajero = Nothing
                strTipo = Nothing
                bolAmerican = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try

            Return strRespuesta

        End Function
        Public Function DeleteSpecialService(ByVal lstDeleteSpecialService As List(Of classDeleteSpecialService), _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intGDS As Integer, _
                                             ByVal intFirmaGDS As Integer, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal objSession As classSession) As String()

            Dim DeleteSpecialServiceRS As objSabreWS.DeleteSpecialService.DeleteSpecialServiceRS = Nothing
            Dim strRespuesta() As String = Nothing
            Dim strMensaje As String = Nothing

            Try



                objIWebServices = New IWebServices
                DeleteSpecialServiceRS = objIWebServices._DeleteSpecialService(lstDeleteSpecialService, _
                                                                                 strCodigoSeguimiento, _
                                                                                 intGDS, _
                                                                                 intFirmaGDS, _
                                                                                 intFirmaDB, _
                                                                               objSession)

                If DeleteSpecialServiceRS IsNot Nothing Then
                    If DeleteSpecialServiceRS.ApplicationResults IsNot Nothing Then

                        If DeleteSpecialServiceRS.ApplicationResults.Success IsNot Nothing Then
                            For i As Integer = 0 To DeleteSpecialServiceRS.ApplicationResults.Success.Length - 1
                                For y As Integer = 0 To DeleteSpecialServiceRS.ApplicationResults.Success(i).SystemSpecificResults.Length - 1
                                    If DeleteSpecialServiceRS.ApplicationResults.Success(i).SystemSpecificResults IsNot Nothing Then

                                        If strRespuesta Is Nothing Then
                                            ReDim strRespuesta(0)
                                        Else
                                            ReDim Preserve strRespuesta(strRespuesta.Length)
                                        End If
                                        strRespuesta(strRespuesta.Length - 1) = DeleteSpecialServiceRS.ApplicationResults.Success(i).SystemSpecificResults(y).HostCommand.Value
                                    End If
                                Next
                            Next

                        End If


                        If DeleteSpecialServiceRS.ApplicationResults.Error IsNot Nothing Then
                            For i As Integer = 0 To DeleteSpecialServiceRS.ApplicationResults.Error.Length - 1
                                If DeleteSpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults IsNot Nothing Then
                                    For y As Integer = 0 To DeleteSpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                        If DeleteSpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message IsNot Nothing Then
                                            For z As Integer = 0 To DeleteSpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message.Length - 1
                                                If DeleteSpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value IsNot Nothing Then

                                                    If strRespuesta Is Nothing Then
                                                        ReDim strRespuesta(0)
                                                    Else
                                                        ReDim Preserve strRespuesta(strRespuesta.Length)
                                                    End If

                                                    Dim Host As String = String.Empty
                                                    If DeleteSpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).HostCommand IsNot Nothing Then
                                                        Host = DeleteSpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).HostCommand.Value
                                                    End If

                                                    strRespuesta(strRespuesta.Length - 1) = "ERROR: " & IIf(Host = "", "", Host & " ") & DeleteSpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value

                                                End If
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If

                        If DeleteSpecialServiceRS.ApplicationResults.Warning IsNot Nothing Then
                            For i As Integer = 0 To DeleteSpecialServiceRS.ApplicationResults.Warning.Length - 1
                                If DeleteSpecialServiceRS.ApplicationResults.Warning(i).SystemSpecificResults IsNot Nothing Then
                                    For y As Integer = 0 To DeleteSpecialServiceRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                                        If DeleteSpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message IsNot Nothing Then
                                            For z As Integer = 0 To DeleteSpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message.Length - 1

                                                If DeleteSpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value IsNot Nothing Then

                                                    If strRespuesta Is Nothing Then
                                                        ReDim strRespuesta(0)
                                                    Else
                                                        ReDim Preserve strRespuesta(strRespuesta.Length)
                                                    End If
                                                    Dim Host As String = String.Empty
                                                    If DeleteSpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).HostCommand IsNot Nothing Then
                                                        Host = DeleteSpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).HostCommand.Value
                                                    End If
                                                    strRespuesta(strRespuesta.Length - 1) = "WARNING: " & IIf(Host = "", "", Host & " ") & DeleteSpecialServiceRS.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value

                                                End If
                                            Next
                                        End If
                                    Next
                                End If
                            Next
                        End If

                    End If
                Else

                    ReDim strRespuesta(0)
                    strRespuesta(strRespuesta.Length - 1) = "ERROR: " & Constantes.ProblemasRemark
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                DeleteSpecialServiceRS = Nothing
                lstDeleteSpecialService = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try

            Return strRespuesta

        End Function
        Public Function AirTicket(ByVal strDK As String, _
                                   ByVal strTipoEmision As String, _
                                   ByVal strNumeroEMD As String, _
                                   ByVal objFormaPago As classFormaPago, _
                                   ByVal strCodigoSeguimiento As String, _
                                   ByVal intGDS As Integer, _
                                   ByVal intFirmaGDS As Integer, _
                                   ByVal intFirmaDB As Integer, _
                                   ByVal objSession As classSession) As classAirTicketRS

            Dim objAirTicketRS As objSabreWS.AirTicketLLS230.AirTicketRS = Nothing
            Dim AirTicketRS As classAirTicketRS = Nothing
            Try

                objIWebServices = New IWebServices
                objAirTicketRS = objIWebServices._AirTicket230(strDK, _
                                                               strTipoEmision, _
                                                               strNumeroEMD, _
                                                               objFormaPago, _
                                                                 strCodigoSeguimiento, _
                                                                 intGDS, _
                                                                 intFirmaGDS, _
                                                                 intFirmaDB, _
                                                               objSession)

                If objAirTicketRS IsNot Nothing Then

                    AirTicketRS = New classAirTicketRS

                    If objAirTicketRS.ApplicationResults IsNot Nothing Then

                        If objAirTicketRS.ApplicationResults.Error IsNot Nothing Then
                            If objAirTicketRS.ApplicationResults.Error(0).SystemSpecificResults IsNot Nothing Then
                                If objAirTicketRS.ApplicationResults.Error(0).SystemSpecificResults(0).Message IsNot Nothing Then

                                    AirTicketRS.ErroresAlertas = New classErroresAlertas
                                    AirTicketRS.ErroresAlertas.Errores = New List(Of String)

                                    For i As Integer = 0 To objAirTicketRS.ApplicationResults.Error(0).SystemSpecificResults(0).Message.Length - 1
                                        AirTicketRS.ErroresAlertas.Errores.Add(objAirTicketRS.ApplicationResults.Error(0).SystemSpecificResults(0).Message(i).Value)
                                    Next

                                    If objAirTicketRS.Text IsNot Nothing Then
                                        AirTicketRS.Respuesta = New List(Of String)
                                        For i As Integer = 0 To objAirTicketRS.Text.Length - 1
                                            AirTicketRS.Respuesta.Add(objAirTicketRS.Text(i).ToString)
                                        Next
                                    End If

                                End If
                            End If


                        ElseIf objAirTicketRS.ApplicationResults.Warning IsNot Nothing Then

                            If objAirTicketRS.ApplicationResults.Warning(0).SystemSpecificResults IsNot Nothing Then
                                If objAirTicketRS.ApplicationResults.Warning(0).SystemSpecificResults(0).Message IsNot Nothing Then


                                    AirTicketRS.ErroresAlertas = New classErroresAlertas
                                    AirTicketRS.ErroresAlertas.Alertas = New List(Of String)

                                    For i As Integer = 0 To objAirTicketRS.ApplicationResults.Warning(0).SystemSpecificResults(0).Message.Length - 1
                                        AirTicketRS.ErroresAlertas.Alertas.Add(objAirTicketRS.ApplicationResults.Warning(0).SystemSpecificResults(0).Message(i).Value)
                                    Next

                                    If objAirTicketRS.Text IsNot Nothing Then
                                        AirTicketRS.Respuesta = New List(Of String)
                                        For i As Integer = 0 To objAirTicketRS.Text.Length - 1
                                            AirTicketRS.Respuesta.Add(objAirTicketRS.Text(i).ToString)
                                        Next
                                    End If

                                End If

                            End If

                        Else
                            AirTicketRS.Status = objAirTicketRS.ApplicationResults.status
                            If objAirTicketRS.ApplicationResults.Success(0) IsNot Nothing Then
                                If objAirTicketRS.ApplicationResults.Success(0).SystemSpecificResults IsNot Nothing Then
                                    AirTicketRS.HostCommand = objAirTicketRS.ApplicationResults.Success(0).SystemSpecificResults(0).HostCommand.Value
                                End If
                            End If
                            If objAirTicketRS.Text IsNot Nothing Then
                                AirTicketRS.Respuesta = New List(Of String)
                                For i As Integer = 0 To objAirTicketRS.Text.Length - 1
                                    AirTicketRS.Respuesta.Add(objAirTicketRS.Text(i).ToString)
                                Next
                            End If
                        End If

                    Else

                        AirTicketRS.ErroresAlertas = New classErroresAlertas
                        AirTicketRS.ErroresAlertas.Errores = New List(Of String)
                        AirTicketRS.ErroresAlertas.Errores.Add("Problemas en el servicio")
                    End If


                Else

                    AirTicketRS.ErroresAlertas = New classErroresAlertas
                    AirTicketRS.ErroresAlertas.Errores = New List(Of String)
                    AirTicketRS.ErroresAlertas.Errores.Add("No hay respuesta del servicio")
                End If

                'PAC(strCodigoSeguimiento, _
                '    oGDS, _
                '    objSession, _
                '    oOp_Firma)

            Catch ex As Exception
                If AirTicketRS Is Nothing Then AirTicketRS = New classAirTicketRS
                If AirTicketRS.ErroresAlertas Is Nothing Then AirTicketRS.ErroresAlertas = New classErroresAlertas
                If AirTicketRS.ErroresAlertas.Errores Is Nothing Then AirTicketRS.ErroresAlertas.Errores = New List(Of String)
                AirTicketRS.ErroresAlertas.Errores.Add(ex.InnerException.ToString)
                AirTicketRS.ErroresAlertas.Errores.Add(ex.Message.ToString)
            Finally
                objAirTicketRS = Nothing
                strDK = Nothing
                strTipoEmision = Nothing
                strNumeroEMD = Nothing
                objFormaPago = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try

            Return AirTicketRS

        End Function
        Public Function AirTicketNEW(ByVal strDK As String, _
                                  ByVal strTipoEmision As String, _
                                  ByVal strNumeroEMD As String, _
                                  ByVal strNumeroPQ As String, _
                                  ByVal bolReissue As Boolean, _
                                  ByVal objEasyOnLine As classEasyOnLine, _
                                  ByVal strCodigoSeguimiento As String, _
                                  ByVal intGDS As Integer, _
                                  ByVal intFirmaGDS As Integer, _
                                  ByVal intFirmaDB As Integer, _
                                  ByVal objSession As classSession) As classAirTicketRS

            Dim objAirTicketRS As objSabreWS.AirTicketLLS270.AirTicketRS = Nothing
            Dim AirTicketRS As classAirTicketRS = Nothing
            Try

                objIWebServices = New IWebServices
                objAirTicketRS = objIWebServices._AirTicketNEW(strDK, _
                                                               strTipoEmision, _
                                                               strNumeroEMD, _
                                                               strNumeroPQ, _
                                                               bolReissue, _
                                                               objEasyOnLine, _
                                                               strCodigoSeguimiento, _
                                                               intGDS, _
                                                               intFirmaGDS, _
                                                               intFirmaDB, _
                                                               objSession)

                If objAirTicketRS IsNot Nothing Then

                    AirTicketRS = New classAirTicketRS

                    If objAirTicketRS.ApplicationResults IsNot Nothing Then

                        If objAirTicketRS.ApplicationResults.Error IsNot Nothing Then
                            If objAirTicketRS.ApplicationResults.Error(0).SystemSpecificResults IsNot Nothing Then
                                If objAirTicketRS.ApplicationResults.Error(0).SystemSpecificResults(0).Message IsNot Nothing Then

                                    AirTicketRS.ErroresAlertas = New classErroresAlertas
                                    AirTicketRS.ErroresAlertas.Errores = New List(Of String)

                                    For i As Integer = 0 To objAirTicketRS.ApplicationResults.Error(0).SystemSpecificResults(0).Message.Length - 1
                                        AirTicketRS.ErroresAlertas.Errores.Add(objAirTicketRS.ApplicationResults.Error(0).SystemSpecificResults(0).Message(i).Value)
                                    Next

                                    If objAirTicketRS.Text IsNot Nothing Then
                                        AirTicketRS.Respuesta = New List(Of String)
                                        For i As Integer = 0 To objAirTicketRS.Text.Length - 1
                                            AirTicketRS.Respuesta.Add(objAirTicketRS.Text(i).ToString)
                                        Next
                                    End If

                                End If
                            End If


                        ElseIf objAirTicketRS.ApplicationResults.Warning IsNot Nothing Then

                            If objAirTicketRS.ApplicationResults.Warning(0).SystemSpecificResults IsNot Nothing Then
                                If objAirTicketRS.ApplicationResults.Warning(0).SystemSpecificResults(0).Message IsNot Nothing Then


                                    AirTicketRS.ErroresAlertas = New classErroresAlertas
                                    AirTicketRS.ErroresAlertas.Alertas = New List(Of String)

                                    For i As Integer = 0 To objAirTicketRS.ApplicationResults.Warning(0).SystemSpecificResults(0).Message.Length - 1
                                        AirTicketRS.ErroresAlertas.Alertas.Add(objAirTicketRS.ApplicationResults.Warning(0).SystemSpecificResults(0).Message(i).Value)
                                    Next

                                    If objAirTicketRS.Text IsNot Nothing Then
                                        AirTicketRS.Respuesta = New List(Of String)
                                        For i As Integer = 0 To objAirTicketRS.Text.Length - 1
                                            AirTicketRS.Respuesta.Add(objAirTicketRS.Text(i).ToString)
                                        Next
                                    End If

                                End If

                            End If

                        Else
                            AirTicketRS.Status = objAirTicketRS.ApplicationResults.status
                            If objAirTicketRS.ApplicationResults.Success(0) IsNot Nothing Then
                                If objAirTicketRS.ApplicationResults.Success(0).SystemSpecificResults IsNot Nothing Then
                                    AirTicketRS.HostCommand = objAirTicketRS.ApplicationResults.Success(0).SystemSpecificResults(0).HostCommand.Value
                                End If
                            End If
                            If objAirTicketRS.Text IsNot Nothing Then
                                AirTicketRS.Respuesta = New List(Of String)
                                For i As Integer = 0 To objAirTicketRS.Text.Length - 1
                                    AirTicketRS.Respuesta.Add(objAirTicketRS.Text(i).ToString)
                                Next
                            End If
                        End If

                    Else

                        AirTicketRS.ErroresAlertas = New classErroresAlertas
                        AirTicketRS.ErroresAlertas.Errores = New List(Of String)
                        AirTicketRS.ErroresAlertas.Errores.Add("Problemas en el servicio")
                    End If


                Else

                    AirTicketRS.ErroresAlertas = New classErroresAlertas
                    AirTicketRS.ErroresAlertas.Errores = New List(Of String)
                    AirTicketRS.ErroresAlertas.Errores.Add("No hay respuesta del servicio")
                End If

                'PAC(strCodigoSeguimiento, _
                '    oGDS, _
                '    objSession, _
                '    oOp_Firma)

            Catch ex As Exception
                If AirTicketRS Is Nothing Then AirTicketRS = New classAirTicketRS
                If AirTicketRS.ErroresAlertas Is Nothing Then AirTicketRS.ErroresAlertas = New classErroresAlertas
                If AirTicketRS.ErroresAlertas.Errores Is Nothing Then AirTicketRS.ErroresAlertas.Errores = New List(Of String)
                AirTicketRS.ErroresAlertas.Errores.Add(ex.InnerException.ToString)
                AirTicketRS.ErroresAlertas.Errores.Add(ex.Message.ToString)
            Finally
                objAirTicketRS = Nothing
                strDK = Nothing
                strTipoEmision = Nothing
                strNumeroEMD = Nothing
                strNumeroPQ = Nothing
                bolReissue = Nothing
                objEasyOnLine = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try

            Return AirTicketRS

        End Function
        Public Function TicketCupon(ByVal strNumeroBoleto As String, _
                                    ByVal strCodigoSeguimiento As String, _
                                    ByVal intGDS As Integer, _
                                    ByVal intFirmaGDS As Integer, _
                                    ByVal intFirmaDB As Integer, _
                                    ByVal objSession As classSession) As classTicketCoupon.TCTicket

            Dim eTicketCouponRS As objSabreWS.TicketCoupon.eTicketCouponRS = Nothing

            Dim strRespuesta() As String = Nothing
            Dim strMensaje As String = Nothing

            Dim objTicketCoupon As classTicketCoupon.TCTicket = Nothing
            Dim objCouponData As classTicketCoupon.TCCouponData = Nothing
            Dim AirItineraryPricing As classTicketCoupon.TCAirItineraryPricing = Nothing
            Dim ItinTotalFare As classTicketCoupon.TCItinTotalFare = Nothing
            Dim BaseFare As classTicketCoupon.TCBaseFare = Nothing
            Dim EquivalenteFare As classTicketCoupon.TCEquivalenteFare = Nothing
            Dim NetFare As classTicketCoupon.TCNetFare = Nothing
            Dim Taxes As classTicketCoupon.TCTaxes = Nothing
            Dim Tax As classTicketCoupon.TCTax = Nothing
            Dim TotalFare As classTicketCoupon.TCTotalFare = Nothing
            Dim TCCoupon As List(Of classTicketCoupon.TCCoupon) = Nothing
            Dim auxTCCoupon As classTicketCoupon.TCCoupon = Nothing
            Dim FlightSegment As classTicketCoupon.TCFlightSegment = Nothing
            Dim FareBasis As classTicketCoupon.TCFareBasis = Nothing
            Dim MarketingAirline As classTicketCoupon.TCMarketingAirline = Nothing
            Dim Customer As classTicketCoupon.TCCustomer = Nothing
            Dim Payment As List(Of classTicketCoupon.TCPayment) = Nothing
            Dim auxPayment As classTicketCoupon.TCPayment = Nothing
            Dim auxPaymentCard As classTicketCoupon.TCPaymentCard = Nothing
            Dim PersonName As classTicketCoupon.TCPersonName = Nothing
            Dim ItineraryRef As classTicketCoupon.TCItineraryRef = Nothing
            Dim ExchangeData As classTicketCoupon.TCExchangeData = Nothing

            Dim objErroresAlertas As classErroresAlertas = Nothing

            Try

                objIWebServices = New IWebServices
                eTicketCouponRS = objIWebServices._eTicketCoupon(strNumeroBoleto, _
                                                                 strCodigoSeguimiento, _
                                                                 intGDS, _
                                                                 intFirmaGDS, _
                                                                 intFirmaDB, _
                                                                 objSession)

                If eTicketCouponRS IsNot Nothing Then

                    If eTicketCouponRS.ApplicationResults IsNot Nothing Then

                        objTicketCoupon = New classTicketCoupon.TCTicket

                        If eTicketCouponRS.ApplicationResults.status.ToString IsNot Nothing Then
                            objTicketCoupon.Status = eTicketCouponRS.ApplicationResults.status.ToString
                        End If

                        If eTicketCouponRS.ApplicationResults.Success IsNot Nothing Then
                            For i As Integer = 0 To eTicketCouponRS.ApplicationResults.Success.Length - 1

                                If Not String.IsNullOrEmpty(eTicketCouponRS.ApplicationResults.Success(i).timeStamp) Then
                                    objTicketCoupon.TimeStamp = eTicketCouponRS.ApplicationResults.Success(i).timeStamp.ToString()
                                End If

                                If eTicketCouponRS.ApplicationResults.Success(i).SystemSpecificResults IsNot Nothing Then

                                    For x As Integer = 0 To eTicketCouponRS.ApplicationResults.Success(i).SystemSpecificResults.Length - 1

                                        If eTicketCouponRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand IsNot Nothing Then

                                            If Not String.IsNullOrEmpty(eTicketCouponRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.LNIATA) Then
                                                objTicketCoupon.IATA_Number = eTicketCouponRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.LNIATA.ToString()
                                            End If

                                            If Not String.IsNullOrEmpty(eTicketCouponRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.Value) Then
                                                objTicketCoupon.HostCommand = eTicketCouponRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.Value.ToString()
                                            End If

                                        End If
                                    Next

                                End If


                            Next

                            If eTicketCouponRS.ApplicationResults.Error IsNot Nothing Then
                                objErroresAlertas = New classErroresAlertas
                                objErroresAlertas.Errores = New List(Of String)
                                For i As Integer = 0 To eTicketCouponRS.ApplicationResults.Error.Length - 1
                                    If eTicketCouponRS.ApplicationResults.Error(i).SystemSpecificResults IsNot Nothing Then
                                        For x As Integer = 0 To eTicketCouponRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                            For y As Integer = 0 To eTicketCouponRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message.Length - 1
                                                If Not String.IsNullOrEmpty(eTicketCouponRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(y).Value) Then
                                                    objErroresAlertas.Errores.Add("ERROR: " & eTicketCouponRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(y).Value.ToString)
                                                End If
                                            Next
                                        Next
                                    End If
                                Next
                                objTicketCoupon.ErroresAlertas = New classErroresAlertas
                                objTicketCoupon.ErroresAlertas = objErroresAlertas
                            End If

                            If eTicketCouponRS.ApplicationResults.Warning IsNot Nothing Then
                                objErroresAlertas = New classErroresAlertas
                                objErroresAlertas.Alertas = New List(Of String)
                                For i As Integer = 0 To eTicketCouponRS.ApplicationResults.Warning.Length - 1
                                    If eTicketCouponRS.ApplicationResults.Warning(i).SystemSpecificResults IsNot Nothing Then
                                        For x As Integer = 0 To eTicketCouponRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                                            For y As Integer = 0 To eTicketCouponRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message.Length - 1
                                                If Not String.IsNullOrEmpty(eTicketCouponRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message(y).Value) Then
                                                    objErroresAlertas.Alertas.Add("WARNING: " & eTicketCouponRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message(y).Value.ToString)
                                                End If
                                            Next
                                        Next
                                    End If
                                Next
                                objTicketCoupon.ErroresAlertas = New classErroresAlertas
                                objTicketCoupon.ErroresAlertas = objErroresAlertas
                            End If

                        End If

                        If eTicketCouponRS.TicketingInfos IsNot Nothing Then
                            If eTicketCouponRS.TicketingInfos.TicketingInfo IsNot Nothing Then

                                With eTicketCouponRS.TicketingInfos.TicketingInfo
                                    If .Ticketing IsNot Nothing Then

                                        objTicketCoupon.TicketNumber = strNumeroBoleto

                                        If Not String.IsNullOrEmpty(.Ticketing.AgencyCity) Then
                                            objTicketCoupon.AgencyCity = .Ticketing.AgencyCity.ToString()
                                        End If

                                        If Not String.IsNullOrEmpty(.Ticketing.AgentWorkArea) Then
                                            objTicketCoupon.AgentWorkArea = .Ticketing.AgentWorkArea.ToString()
                                        End If

                                        If Not String.IsNullOrEmpty(.Ticketing.IATA_Number) Then
                                            objTicketCoupon.IATA_Number = .Ticketing.IATA_Number.ToString()
                                        End If

                                        If Not String.IsNullOrEmpty(.Ticketing.IssuingAgent) Then
                                            objTicketCoupon.IssuingAgent = .Ticketing.IssuingAgent.ToString()
                                        End If

                                        If Not String.IsNullOrEmpty(.Ticketing.PrimeHostID) Then
                                            objTicketCoupon.PrimeHostID = .Ticketing.PrimeHostID.ToString()
                                        End If

                                        If Not String.IsNullOrEmpty(.Ticketing.PseudoCityCode) Then
                                            objTicketCoupon.PseudoCityCode = .Ticketing.PseudoCityCode.ToString()
                                        End If

                                        If Not String.IsNullOrEmpty(.Ticketing.TransactionDateTime) Then
                                            objTicketCoupon.TransactionDateTime = .Ticketing.TransactionDateTime.ToString()
                                        End If

                                        If .Ticketing.CouponData IsNot Nothing Then
                                            objCouponData = New classTicketCoupon.TCCouponData

                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.InformationSource) Then
                                                objCouponData.InformationSource = .Ticketing.CouponData.InformationSource.ToString()
                                            End If

                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.IssueDate) Then
                                                objCouponData.IssueDate = .Ticketing.CouponData.IssueDate.ToString()
                                            End If

                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.NumBooklets) Then
                                                objCouponData.NumBooklets = .Ticketing.CouponData.NumBooklets.ToString()
                                            End If

                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.ProductID) Then
                                                objCouponData.ProductID = .Ticketing.CouponData.ProductID.ToString()
                                            End If

                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.TicketMedia) Then
                                                objCouponData.TicketMedia = .Ticketing.CouponData.TicketMedia.ToString()
                                            End If

                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.TicketMode) Then
                                                objCouponData.TicketMode = .Ticketing.CouponData.TicketMode.ToString()
                                            End If

                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.ConjunctiveTicketNumbers) Then
                                                objCouponData.ConjunctiveTicketNumbers = .Ticketing.CouponData.ConjunctiveTicketNumbers.ToString()
                                            End If

                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.ExchangeInd) Then
                                                objCouponData.ExchangeInd = .Ticketing.CouponData.ExchangeInd.ToString()
                                            End If

                                            AirItineraryPricing = New classTicketCoupon.TCAirItineraryPricing
                                            If .Ticketing.CouponData.AirItineraryPricingInfo IsNot Nothing Then

                                                If .Ticketing.CouponData.AirItineraryPricingInfo.Endorsements IsNot Nothing Then
                                                    AirItineraryPricing.Endorsements = New List(Of String)
                                                    For x As Integer = 0 To .Ticketing.CouponData.AirItineraryPricingInfo.Endorsements.Length - 1
                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.AirItineraryPricingInfo.Endorsements(x).ToString) Then
                                                            AirItineraryPricing.Endorsements.Add(.Ticketing.CouponData.AirItineraryPricingInfo.Endorsements(x).ToString())
                                                        End If
                                                    Next
                                                End If

                                                If .Ticketing.CouponData.AirItineraryPricingInfo.FareCalculation IsNot Nothing Then
                                                    If Not String.IsNullOrEmpty(.Ticketing.CouponData.AirItineraryPricingInfo.FareCalculation.Text) Then
                                                        AirItineraryPricing.FareCalculation = .Ticketing.CouponData.AirItineraryPricingInfo.FareCalculation.Text.ToString()
                                                    End If
                                                End If

                                                If .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare IsNot Nothing Then
                                                    ItinTotalFare = New classTicketCoupon.TCItinTotalFare
                                                    If .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.BaseFare IsNot Nothing Then
                                                        BaseFare = New classTicketCoupon.TCBaseFare

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.BaseFare.Amount) Then
                                                            BaseFare.Amount = .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.BaseFare.Amount.ToString()
                                                        End If

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.BaseFare.CurrencyCode) Then
                                                            BaseFare.CurrencyCode = .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.BaseFare.CurrencyCode.ToString()
                                                        End If

                                                        ItinTotalFare.TCBaseFare = BaseFare
                                                    End If

                                                    If .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.EquivFare IsNot Nothing Then
                                                        EquivalenteFare = New classTicketCoupon.TCEquivalenteFare

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.EquivFare.Amount) Then
                                                            EquivalenteFare.Amount = .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.EquivFare.Amount.ToString()
                                                        End If

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.EquivFare.CurrencyCode) Then
                                                            EquivalenteFare.CurrencyCode = .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.EquivFare.CurrencyCode.ToString()
                                                        End If

                                                        ItinTotalFare.TCEquivalenteFare = EquivalenteFare
                                                    End If

                                                    If .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.NetFare IsNot Nothing Then
                                                        NetFare = New classTicketCoupon.TCNetFare

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.NetFare.Amount) Then
                                                            NetFare.Amount = .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.NetFare.Amount.ToString()
                                                        End If

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.NetFare.AmountType) Then
                                                            NetFare.AmountType = .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.NetFare.AmountType.ToString()
                                                        End If

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.NetFare.CreditCardAmount) Then
                                                            NetFare.CreditCardAmount = .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.NetFare.CreditCardAmount.ToString()
                                                        End If

                                                        ItinTotalFare.TCNetFare = NetFare
                                                    End If

                                                    If .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.Taxes IsNot Nothing Then
                                                        Taxes = New classTicketCoupon.TCTaxes

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.Taxes.Total) Then
                                                            Taxes.Total = .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.Taxes.Total.ToString()
                                                        End If

                                                        Taxes.TCTax = New List(Of classTicketCoupon.TCTax)
                                                        For i As Integer = 0 To .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax.Length - 1
                                                            Tax = New classTicketCoupon.TCTax

                                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax(i).Amount) Then
                                                                Tax.Amount = .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax(i).Amount.ToString()
                                                            End If

                                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax(i).TaxCode) Then
                                                                Tax.TaxCode = .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax(i).TaxCode.ToString()
                                                            End If

                                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax(i).Paid) Then
                                                                Tax.CodePaid = .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.Taxes.Tax(i).Paid.ToString()
                                                            End If

                                                            Taxes.TCTax.Add(Tax)
                                                        Next

                                                        ItinTotalFare.TCTaxes = Taxes
                                                    End If

                                                    If .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.TotalFare IsNot Nothing Then
                                                        TotalFare = New classTicketCoupon.TCTotalFare
                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.TotalFare.Amount) Then
                                                            TotalFare.Amount = .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.TotalFare.Amount.ToString()
                                                        End If

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.TotalFare.CurrencyCode) Then
                                                            TotalFare.CurrencyCode = .Ticketing.CouponData.AirItineraryPricingInfo.ItinTotalFare.TotalFare.CurrencyCode.ToString()
                                                        End If

                                                        ItinTotalFare.TCTotalFare = TotalFare
                                                    End If

                                                End If
                                                AirItineraryPricing.TCItinTotalFare = New classTicketCoupon.TCItinTotalFare
                                                AirItineraryPricing.TCItinTotalFare = ItinTotalFare

                                                If Not String.IsNullOrEmpty(.Ticketing.CouponData.AirItineraryPricingInfo.PassengerTypeQuantity.Code) Then
                                                    AirItineraryPricing.PassengerTypeQuantity = .Ticketing.CouponData.AirItineraryPricingInfo.PassengerTypeQuantity.Code.ToString()
                                                End If

                                                objCouponData.AirItineraryPricing = AirItineraryPricing
                                            End If


                                            If .Ticketing.CouponData.Coupons IsNot Nothing Then
                                                TCCoupon = New List(Of classTicketCoupon.TCCoupon)
                                                For i As Integer = 0 To .Ticketing.CouponData.Coupons.Length - 1
                                                    auxTCCoupon = New classTicketCoupon.TCCoupon

                                                    If Not String.IsNullOrEmpty(.Ticketing.CouponData.Coupons(i).CodedStatus) Then
                                                        auxTCCoupon.CodedStatus = .Ticketing.CouponData.Coupons(i).CodedStatus.ToString()
                                                    End If

                                                    If Not String.IsNullOrEmpty(.Ticketing.CouponData.Coupons(i).Number) Then
                                                        auxTCCoupon.Number = .Ticketing.CouponData.Coupons(i).Number.ToString()
                                                    End If

                                                    If Not String.IsNullOrEmpty(.Ticketing.CouponData.Coupons(i).StatusCode) Then
                                                        auxTCCoupon.StatusCode = .Ticketing.CouponData.Coupons(i).StatusCode.ToString()
                                                    End If

                                                    If .Ticketing.CouponData.Coupons(i).FlightSegment IsNot Nothing Then
                                                        FlightSegment = New classTicketCoupon.TCFlightSegment

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.Coupons(i).FlightSegment.DepartureDateTime) Then
                                                            FlightSegment.DepartureDateTime = .Ticketing.CouponData.Coupons(i).FlightSegment.DepartureDateTime.ToString()
                                                        End If

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.Coupons(i).FlightSegment.FlightNumber) Then
                                                            FlightSegment.FlightNumber = .Ticketing.CouponData.Coupons(i).FlightSegment.FlightNumber.ToString()
                                                        End If

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.Coupons(i).FlightSegment.ResBookDesigCode) Then
                                                            FlightSegment.ResBookDesigCode = .Ticketing.CouponData.Coupons(i).FlightSegment.ResBookDesigCode.ToString()
                                                        End If

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.Coupons(i).FlightSegment.RPH) Then
                                                            FlightSegment.RPH = .Ticketing.CouponData.Coupons(i).FlightSegment.RPH.ToString()
                                                        End If

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.Coupons(i).FlightSegment.ConnectionInd) Then
                                                            FlightSegment.ConnectionInd = .Ticketing.CouponData.Coupons(i).FlightSegment.ConnectionInd.ToString()
                                                        End If

                                                        If .Ticketing.CouponData.Coupons(i).FlightSegment.DestinationLocation IsNot Nothing Then
                                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.Coupons(i).FlightSegment.DestinationLocation.LocationCode) Then
                                                                FlightSegment.DestinationLocation = .Ticketing.CouponData.Coupons(i).FlightSegment.DestinationLocation.LocationCode.ToString()
                                                            End If
                                                        End If

                                                        If .Ticketing.CouponData.Coupons(i).FlightSegment.FareBasis IsNot Nothing Then
                                                            FareBasis = New classTicketCoupon.TCFareBasis

                                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.Coupons(i).FlightSegment.FareBasis.Code) Then
                                                                FareBasis.Code = .Ticketing.CouponData.Coupons(i).FlightSegment.FareBasis.Code.ToString()

                                                                If .Ticketing.CouponData.Coupons(i).FlightSegment.FareBasis.Code.Split("/").Length > 1 Then
                                                                    FareBasis.FareBasisTD = .Ticketing.CouponData.Coupons(i).FlightSegment.FareBasis.Code.Split("/")(1)
                                                                Else
                                                                    FareBasis.FareBasisTD = Nothing
                                                                End If

                                                                FlightSegment.FareBasis = FareBasis
                                                            End If

                                                        End If

                                                        If .Ticketing.CouponData.Coupons(i).FlightSegment.MarketingAirline IsNot Nothing Then
                                                            MarketingAirline = New classTicketCoupon.TCMarketingAirline

                                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.Coupons(i).FlightSegment.MarketingAirline.Code) Then
                                                                MarketingAirline.Code = .Ticketing.CouponData.Coupons(i).FlightSegment.MarketingAirline.Code.ToString()
                                                            End If

                                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.Coupons(i).FlightSegment.MarketingAirline.FlightNumber) Then
                                                                MarketingAirline.FlightNumber = .Ticketing.CouponData.Coupons(i).FlightSegment.MarketingAirline.FlightNumber.ToString()
                                                            End If
                                                            FlightSegment.MarketingAirline = MarketingAirline
                                                        End If


                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.Coupons(i).FlightSegment.OperatingAirline.Code) Then
                                                            FlightSegment.OperatingAirline = .Ticketing.CouponData.Coupons(i).FlightSegment.OperatingAirline.Code.ToString()
                                                        End If

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.Coupons(i).FlightSegment.OriginLocation.LocationCode) Then
                                                            FlightSegment.OriginLocation = .Ticketing.CouponData.Coupons(i).FlightSegment.OriginLocation.LocationCode.ToString()
                                                        End If

                                                        auxTCCoupon.TCFlightSegment = FlightSegment
                                                    End If

                                                    TCCoupon.Add(auxTCCoupon)

                                                Next

                                                objCouponData.TCCoupon = TCCoupon

                                            End If

                                            If .Ticketing.CouponData.CustomerInfo IsNot Nothing Then
                                                Customer = New classTicketCoupon.TCCustomer

                                                If .Ticketing.CouponData.CustomerInfo.Customer IsNot Nothing Then
                                                    If .Ticketing.CouponData.CustomerInfo.Customer.Payment IsNot Nothing Then
                                                        Payment = New List(Of classTicketCoupon.TCPayment)
                                                        For i As Integer = 0 To .Ticketing.CouponData.CustomerInfo.Customer.Payment.Length - 1
                                                            auxPayment = New classTicketCoupon.TCPayment

                                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.CustomerInfo.Customer.Payment(i).ApprovalID) Then
                                                                auxPayment.ApprovalID = .Ticketing.CouponData.CustomerInfo.Customer.Payment(i).ApprovalID.ToString()
                                                            End If

                                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.CustomerInfo.Customer.Payment(i).ReferenceNumber) Then
                                                                auxPayment.ReferenceNumber = .Ticketing.CouponData.CustomerInfo.Customer.Payment(i).ReferenceNumber.ToString()
                                                            End If

                                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.CustomerInfo.Customer.Payment(i).RPH) Then
                                                                auxPayment.RPH = .Ticketing.CouponData.CustomerInfo.Customer.Payment(i).RPH.ToString()
                                                            End If

                                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.CustomerInfo.Customer.Payment(i).Type) Then
                                                                auxPayment.Type = .Ticketing.CouponData.CustomerInfo.Customer.Payment(i).Type.ToString()
                                                            End If

                                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.CustomerInfo.Customer.Payment(i).Text) Then
                                                                auxPayment.Text = .Ticketing.CouponData.CustomerInfo.Customer.Payment(i).Text.ToString()
                                                            End If

                                                            If Not String.IsNullOrEmpty(.Ticketing.CouponData.CustomerInfo.Customer.Payment(i).Conditions) Then
                                                                auxPayment.Conditions = .Ticketing.CouponData.CustomerInfo.Customer.Payment(i).Conditions.ToString()
                                                            End If

                                                            If .Ticketing.CouponData.CustomerInfo.Customer.Payment(i).CC_Info IsNot Nothing Then
                                                                If .Ticketing.CouponData.CustomerInfo.Customer.Payment(i).CC_Info.PaymentCard IsNot Nothing Then

                                                                    auxPaymentCard = New classTicketCoupon.TCPaymentCard

                                                                    If Not String.IsNullOrEmpty(.Ticketing.CouponData.CustomerInfo.Customer.Payment(i).CC_Info.PaymentCard.Code) Then
                                                                        auxPaymentCard.Code = .Ticketing.CouponData.CustomerInfo.Customer.Payment(i).CC_Info.PaymentCard.Code.ToString()
                                                                    End If

                                                                    If Not String.IsNullOrEmpty(.Ticketing.CouponData.CustomerInfo.Customer.Payment(i).CC_Info.PaymentCard.Amount) Then
                                                                        auxPaymentCard.Amount = .Ticketing.CouponData.CustomerInfo.Customer.Payment(i).CC_Info.PaymentCard.Amount.ToString()
                                                                    End If

                                                                    If Not String.IsNullOrEmpty(.Ticketing.CouponData.CustomerInfo.Customer.Payment(i).CC_Info.PaymentCard.ExpirationDate) Then
                                                                        auxPaymentCard.ExpirationDate = .Ticketing.CouponData.CustomerInfo.Customer.Payment(i).CC_Info.PaymentCard.ExpirationDate.ToString()
                                                                    End If

                                                                    auxPayment.TCPaymentCard = New classTicketCoupon.TCPaymentCard
                                                                    auxPayment.TCPaymentCard = auxPaymentCard
                                                                End If
                                                            End If

                                                            Payment.Add(auxPayment)
                                                        Next
                                                    End If

                                                    Customer.TCPayment = New List(Of classTicketCoupon.TCPayment)
                                                    Customer.TCPayment = Payment

                                                    If .Ticketing.CouponData.CustomerInfo.Customer.PersonName IsNot Nothing Then
                                                        PersonName = New classTicketCoupon.TCPersonName

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.CustomerInfo.Customer.PersonName.NameReference) Then
                                                            PersonName.NameReference = .Ticketing.CouponData.CustomerInfo.Customer.PersonName.NameReference.ToString()
                                                        End If

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.CustomerInfo.Customer.PersonName.PassengerType) Then
                                                            PersonName.PassengerType = .Ticketing.CouponData.CustomerInfo.Customer.PersonName.PassengerType.ToString()
                                                        End If

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.CustomerInfo.Customer.PersonName.GivenName) Then
                                                            PersonName.GivenName = .Ticketing.CouponData.CustomerInfo.Customer.PersonName.GivenName.ToString()
                                                        End If

                                                        If Not String.IsNullOrEmpty(.Ticketing.CouponData.CustomerInfo.Customer.PersonName.Surname) Then
                                                            PersonName.Surname = .Ticketing.CouponData.CustomerInfo.Customer.PersonName.Surname.ToString()
                                                        End If

                                                        Customer.TCPersonName = New classTicketCoupon.TCPersonName
                                                        Customer.TCPersonName = PersonName
                                                    End If

                                                    objCouponData.TCCustomer = New classTicketCoupon.TCCustomer
                                                    objCouponData.TCCustomer = Customer

                                                End If

                                            End If

                                            If .Ticketing.CouponData.ItineraryRef IsNot Nothing Then

                                                ItineraryRef = New classTicketCoupon.TCItineraryRef

                                                If Not String.IsNullOrEmpty(.Ticketing.CouponData.ItineraryRef.CustomerIdentifier) Then
                                                    ItineraryRef.CustomerIdentifier = .Ticketing.CouponData.ItineraryRef.CustomerIdentifier.ToString()
                                                End If

                                                If Not String.IsNullOrEmpty(.Ticketing.CouponData.ItineraryRef.ID) Then
                                                    ItineraryRef.ID = .Ticketing.CouponData.ItineraryRef.ID.ToString()
                                                End If

                                                objCouponData.ItineraryRef = New classTicketCoupon.TCItineraryRef
                                                objCouponData.ItineraryRef = ItineraryRef
                                            End If

                                        End If

                                        If .Ticketing.ExchangeData IsNot Nothing Then
                                            If .Ticketing.ExchangeData.CouponData IsNot Nothing Then

                                                ExchangeData = New classTicketCoupon.TCExchangeData
                                                If Not String.IsNullOrEmpty(.Ticketing.ExchangeData.CouponData.IssueDate) Then
                                                    ExchangeData.IssueDate = .Ticketing.ExchangeData.CouponData.IssueDate
                                                End If

                                                If Not String.IsNullOrEmpty(.Ticketing.ExchangeData.CouponData.OriginalTicketNumber) Then
                                                    ExchangeData.OriginalTicketNumber = .Ticketing.ExchangeData.CouponData.OriginalTicketNumber
                                                End If

                                                If Not String.IsNullOrEmpty(.Ticketing.ExchangeData.CouponData.IATA_Code) Then
                                                    ExchangeData.IATA_Code = .Ticketing.ExchangeData.CouponData.IATA_Code
                                                End If

                                                If Not String.IsNullOrEmpty(.Ticketing.ExchangeData.CouponData.CouponNumbers) Then
                                                    ExchangeData.CouponNumbers = .Ticketing.ExchangeData.CouponData.CouponNumbers
                                                End If

                                                If Not String.IsNullOrEmpty(.Ticketing.ExchangeData.CouponData.BoardPoint) Then
                                                    ExchangeData.BoardPoint = .Ticketing.ExchangeData.CouponData.BoardPoint
                                                End If

                                                If .Ticketing.ExchangeData.CouponData.Payment IsNot Nothing Then
                                                    ExchangeData.TCPayment = New classTicketCoupon.TCPayment

                                                    If Not String.IsNullOrEmpty(.Ticketing.ExchangeData.CouponData.Payment.Code) Then
                                                        ExchangeData.TCPayment.Code = .Ticketing.ExchangeData.CouponData.Payment.Code
                                                    End If

                                                    If Not String.IsNullOrEmpty(.Ticketing.ExchangeData.CouponData.Payment.Type) Then
                                                        ExchangeData.TCPayment.Type = .Ticketing.ExchangeData.CouponData.Payment.Type
                                                    End If

                                                    If Not String.IsNullOrEmpty(.Ticketing.ExchangeData.CouponData.Payment.ReferenceNumber) Then
                                                        ExchangeData.TCPayment.ReferenceNumber = .Ticketing.ExchangeData.CouponData.Payment.ReferenceNumber
                                                    End If

                                                End If

                                                objCouponData.ExchangeData = New classTicketCoupon.TCExchangeData
                                                objCouponData.ExchangeData = ExchangeData
                                            End If
                                        End If

                                    End If
                                End With

                            End If

                            objTicketCoupon.TCCouponData = New classTicketCoupon.TCCouponData
                            objTicketCoupon.TCCouponData = objCouponData
                        End If
                    End If
                End If




            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strNumeroBoleto = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing

                eTicketCouponRS = Nothing

                strRespuesta = Nothing
                strMensaje = Nothing

                objCouponData = Nothing
                AirItineraryPricing = Nothing
                ItinTotalFare = Nothing
                BaseFare = Nothing
                NetFare = Nothing
                Taxes = Nothing
                Tax = Nothing
                TotalFare = Nothing
                TCCoupon = Nothing
                auxTCCoupon = Nothing
                FlightSegment = Nothing
                FareBasis = Nothing
                MarketingAirline = Nothing
                Customer = Nothing
                Payment = Nothing
                auxPayment = Nothing
                auxPaymentCard = Nothing
                PersonName = Nothing
                ItineraryRef = Nothing

                objErroresAlertas = Nothing

            End Try

            Return objTicketCoupon

        End Function
        Public Function EMDCupon(ByVal strNumeroBoleto As String, _
                                 ByVal strCodigoSeguimiento As String, _
                                 ByVal intGDS As Integer, _
                                 ByVal intFirmaGDS As Integer, _
                                 ByVal intFirmaDB As Integer, _
                                 ByVal objSession As classSession) As classTicketCoupon.TCTicket

            Dim EMD_DisplayRS As objSabreWS.EMD_Display.EMD_DisplayRS = Nothing

            Dim strRespuesta() As String = Nothing
            Dim strMensaje As String = Nothing

            Dim objTicketCoupon As classTicketCoupon.TCTicket = Nothing
            Dim objCouponData As classTicketCoupon.TCCouponData = Nothing
            Dim AirItineraryPricing As classTicketCoupon.TCAirItineraryPricing = Nothing
            Dim ItinTotalFare As classTicketCoupon.TCItinTotalFare = Nothing
            Dim BaseFare As classTicketCoupon.TCBaseFare = Nothing
            Dim EquivalenteFare As classTicketCoupon.TCEquivalenteFare = Nothing
            Dim NetFare As classTicketCoupon.TCNetFare = Nothing
            Dim Taxes As classTicketCoupon.TCTaxes = Nothing
            Dim Tax As classTicketCoupon.TCTax = Nothing
            Dim TotalFare As classTicketCoupon.TCTotalFare = Nothing
            Dim TCCoupon As List(Of classTicketCoupon.TCCoupon) = Nothing
            Dim auxTCCoupon As classTicketCoupon.TCCoupon = Nothing
            Dim FlightSegment As classTicketCoupon.TCFlightSegment = Nothing
            Dim FareBasis As classTicketCoupon.TCFareBasis = Nothing
            Dim MarketingAirline As classTicketCoupon.TCMarketingAirline = Nothing
            Dim Customer As classTicketCoupon.TCCustomer = Nothing
            Dim Payment As List(Of classTicketCoupon.TCPayment) = Nothing
            Dim auxPayment As classTicketCoupon.TCPayment = Nothing
            Dim auxPaymentCard As classTicketCoupon.TCPaymentCard = Nothing
            Dim PersonName As classTicketCoupon.TCPersonName = Nothing
            Dim ItineraryRef As classTicketCoupon.TCItineraryRef = Nothing
            Dim ExchangeData As classTicketCoupon.TCExchangeData = Nothing

            Dim objErroresAlertas As classErroresAlertas = Nothing

            Try

                objIWebServices = New IWebServices
                EMD_DisplayRS = objIWebServices._EMD_Display(strNumeroBoleto, _
                                                             strCodigoSeguimiento, _
                                                             intGDS, _
                                                             intFirmaGDS, _
                                                             intFirmaDB, _
                                                             objSession)

                If EMD_DisplayRS IsNot Nothing Then

                    If EMD_DisplayRS.ApplicationResults IsNot Nothing Then

                        objTicketCoupon = New classTicketCoupon.TCTicket

                        If EMD_DisplayRS.ApplicationResults.status.ToString IsNot Nothing Then
                            objTicketCoupon.Status = EMD_DisplayRS.ApplicationResults.status.ToString
                        End If

                        If EMD_DisplayRS.ApplicationResults.Success IsNot Nothing Then
                            For i As Integer = 0 To EMD_DisplayRS.ApplicationResults.Success.Length - 1

                                If Not String.IsNullOrEmpty(EMD_DisplayRS.ApplicationResults.Success(i).timeStamp) Then
                                    objTicketCoupon.TimeStamp = EMD_DisplayRS.ApplicationResults.Success(i).timeStamp.ToString()
                                End If

                                If EMD_DisplayRS.ApplicationResults.Success(i).SystemSpecificResults IsNot Nothing Then

                                    For x As Integer = 0 To EMD_DisplayRS.ApplicationResults.Success(i).SystemSpecificResults.Length - 1

                                        If EMD_DisplayRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand IsNot Nothing Then

                                            If Not String.IsNullOrEmpty(EMD_DisplayRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.LNIATA) Then
                                                objTicketCoupon.IATA_Number = EMD_DisplayRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.LNIATA.ToString()
                                            End If

                                            If Not String.IsNullOrEmpty(EMD_DisplayRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.Value) Then
                                                objTicketCoupon.HostCommand = EMD_DisplayRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.Value.ToString()
                                            End If

                                        End If
                                    Next

                                End If


                            Next

                            If EMD_DisplayRS.ApplicationResults.Error IsNot Nothing Then
                                objErroresAlertas = New classErroresAlertas
                                objErroresAlertas.Errores = New List(Of String)
                                For i As Integer = 0 To EMD_DisplayRS.ApplicationResults.Error.Length - 1
                                    If EMD_DisplayRS.ApplicationResults.Error(i).SystemSpecificResults IsNot Nothing Then
                                        For x As Integer = 0 To EMD_DisplayRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                            For y As Integer = 0 To EMD_DisplayRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message.Length - 1
                                                If Not String.IsNullOrEmpty(EMD_DisplayRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(y).Value) Then
                                                    objErroresAlertas.Errores.Add("ERROR: " & EMD_DisplayRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(y).Value.ToString)
                                                End If
                                            Next
                                        Next
                                    End If
                                Next
                                objTicketCoupon.ErroresAlertas = New classErroresAlertas
                                objTicketCoupon.ErroresAlertas = objErroresAlertas
                            End If

                            If EMD_DisplayRS.ApplicationResults.Warning IsNot Nothing Then
                                objErroresAlertas = New classErroresAlertas
                                objErroresAlertas.Alertas = New List(Of String)
                                For i As Integer = 0 To EMD_DisplayRS.ApplicationResults.Warning.Length - 1
                                    If EMD_DisplayRS.ApplicationResults.Warning(i).SystemSpecificResults IsNot Nothing Then
                                        For x As Integer = 0 To EMD_DisplayRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                                            For y As Integer = 0 To EMD_DisplayRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message.Length - 1
                                                If Not String.IsNullOrEmpty(EMD_DisplayRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message(y).Value) Then
                                                    objErroresAlertas.Alertas.Add("WARNING: " & EMD_DisplayRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message(y).Value.ToString)
                                                End If
                                            Next
                                        Next
                                    End If
                                Next
                                objTicketCoupon.ErroresAlertas = New classErroresAlertas
                                objTicketCoupon.ErroresAlertas = objErroresAlertas
                            End If

                        End If

                        If EMD_DisplayRS.EMD_Info IsNot Nothing Then


                            objCouponData = New classTicketCoupon.TCCouponData
                            objCouponData.IssueDate = EMD_DisplayRS.EMD_Info.IssueDate

                            ItineraryRef = New classTicketCoupon.TCItineraryRef
                            If Not String.IsNullOrEmpty(EMD_DisplayRS.EMD_Info.CustomerIdentifier) Then
                                ItineraryRef.CustomerIdentifier = EMD_DisplayRS.EMD_Info.CustomerIdentifier
                            End If

                            If Not String.IsNullOrEmpty(EMD_DisplayRS.EMD_Info.ItineraryRef) Then
                                ItineraryRef.ID = EMD_DisplayRS.EMD_Info.ItineraryRef.ToString()
                            End If

                            objCouponData.ItineraryRef = ItineraryRef



                            If EMD_DisplayRS.EMD_Info.CustomerInfo IsNot Nothing Then

                                Customer = New classTicketCoupon.TCCustomer

                                If EMD_DisplayRS.EMD_Info.CustomerInfo.PersonName IsNot Nothing Then

                                    PersonName = New classTicketCoupon.TCPersonName
                                    If Not String.IsNullOrEmpty(EMD_DisplayRS.EMD_Info.CustomerInfo.PersonName.GivenName) Then
                                        PersonName.GivenName = EMD_DisplayRS.EMD_Info.CustomerInfo.PersonName.GivenName
                                    End If

                                    If Not String.IsNullOrEmpty(EMD_DisplayRS.EMD_Info.CustomerInfo.PersonName.Surname) Then
                                        PersonName.Surname = EMD_DisplayRS.EMD_Info.CustomerInfo.PersonName.Surname
                                    End If

                                    If Not String.IsNullOrEmpty(EMD_DisplayRS.EMD_Info.CustomerInfo.PersonName.NameReference) Then
                                        PersonName.NameReference = EMD_DisplayRS.EMD_Info.CustomerInfo.PersonName.NameReference
                                    End If

                                    Customer.TCPersonName = New classTicketCoupon.TCPersonName
                                    Customer.TCPersonName = PersonName
                                End If

                                If EMD_DisplayRS.EMD_Info.CustomerInfo.PaymentInfo IsNot Nothing Then
                                    Payment = New List(Of classTicketCoupon.TCPayment)

                                    auxPayment = New classTicketCoupon.TCPayment

                                    For i As Integer = 0 To EMD_DisplayRS.EMD_Info.CustomerInfo.PaymentInfo.Length - 1

                                        If EMD_DisplayRS.EMD_Info.CustomerInfo.PaymentInfo(i).CC_Info IsNot Nothing Then

                                            For x As Integer = 0 To EMD_DisplayRS.EMD_Info.CustomerInfo.PaymentInfo(i).CC_Info.Length - 1

                                                If EMD_DisplayRS.EMD_Info.CustomerInfo.PaymentInfo(i).CC_Info(x).PaymentCard IsNot Nothing Then
                                                    auxPaymentCard = New classTicketCoupon.TCPaymentCard
                                                    If Not String.IsNullOrEmpty(EMD_DisplayRS.EMD_Info.CustomerInfo.PaymentInfo(i).CC_Info(x).PaymentCard.Code) Then
                                                        auxPaymentCard.Code = EMD_DisplayRS.EMD_Info.CustomerInfo.PaymentInfo(i).CC_Info(x).PaymentCard.Code
                                                    End If

                                                    If Not String.IsNullOrEmpty(EMD_DisplayRS.EMD_Info.CustomerInfo.PaymentInfo(i).CC_Info(x).PaymentCard.ExpireDate) Then
                                                        auxPaymentCard.ExpirationDate = EMD_DisplayRS.EMD_Info.CustomerInfo.PaymentInfo(i).CC_Info(x).PaymentCard.ExpireDate
                                                    End If

                                                    auxPayment.TCPaymentCard = New classTicketCoupon.TCPaymentCard
                                                    auxPayment.TCPaymentCard = auxPaymentCard
                                                End If

                                            Next
                                        End If

                                        If EMD_DisplayRS.EMD_Info.CustomerInfo.PaymentInfo(i).Payment IsNot Nothing Then

                                            If EMD_DisplayRS.EMD_Info.CustomerInfo.PaymentInfo(i).Payment.Form IsNot Nothing Then
                                                auxPayment.Type = EMD_DisplayRS.EMD_Info.CustomerInfo.PaymentInfo(i).Payment.Form.ToString()
                                            End If

                                        End If

                                        If EMD_DisplayRS.EMD_Info.CustomerInfo.PaymentInfo(i).Text IsNot Nothing Then
                                            If Not String.IsNullOrEmpty(EMD_DisplayRS.EMD_Info.CustomerInfo.PaymentInfo(i).Text) Then
                                                auxPayment.Type = EMD_DisplayRS.EMD_Info.CustomerInfo.PaymentInfo(i).Text.ToString
                                            End If
                                        End If


                                        If Payment Is Nothing Then Payment = New List(Of classTicketCoupon.TCPayment)
                                        Payment.Add(auxPayment)
                                    Next


                                    Customer.TCPayment = New List(Of classTicketCoupon.TCPayment)
                                    Customer.TCPayment = Payment

                                End If

                                objCouponData.TCCustomer = New classTicketCoupon.TCCustomer
                                objCouponData.TCCustomer = Customer

                            End If

                            If EMD_DisplayRS.EMD_Coupons IsNot Nothing Then
                                TCCoupon = New List(Of classTicketCoupon.TCCoupon)
                                For I As Integer = 0 To EMD_DisplayRS.EMD_Coupons.Length - 1
                                    auxTCCoupon = New classTicketCoupon.TCCoupon
                                    auxTCCoupon.Number = EMD_DisplayRS.EMD_Coupons(I).Number
                                    auxTCCoupon.StatusCode = EMD_DisplayRS.EMD_Coupons(I).StatusCode
                                    TCCoupon.Add(auxTCCoupon)
                                Next
                                objCouponData.TCCoupon = TCCoupon

                            End If

                            objTicketCoupon.TCCouponData = New classTicketCoupon.TCCouponData
                            objTicketCoupon.TCCouponData = objCouponData


                            If EMD_DisplayRS.EMD_Info.Source IsNot Nothing Then

                                If Not String.IsNullOrEmpty(EMD_DisplayRS.EMD_Info.Source.AgencyCity) Then
                                    objTicketCoupon.AgencyCity = EMD_DisplayRS.EMD_Info.Source.AgencyCity
                                End If

                                If Not String.IsNullOrEmpty(EMD_DisplayRS.EMD_Info.Source.PseudoCityCode) Then
                                    objTicketCoupon.PseudoCityCode = EMD_DisplayRS.EMD_Info.Source.PseudoCityCode
                                End If

                                If Not String.IsNullOrEmpty(EMD_DisplayRS.EMD_Info.Source.IATA_Number) Then
                                    objTicketCoupon.IATA_Number = EMD_DisplayRS.EMD_Info.Source.IATA_Number
                                End If

                                If Not String.IsNullOrEmpty(EMD_DisplayRS.EMD_Info.Source.AgentWorkArea) Then
                                    objTicketCoupon.AgentWorkArea = EMD_DisplayRS.EMD_Info.Source.AgentWorkArea
                                End If

                                If Not String.IsNullOrEmpty(EMD_DisplayRS.EMD_Info.Source.IssuingAgent) Then
                                    objTicketCoupon.IssuingAgent = EMD_DisplayRS.EMD_Info.Source.IssuingAgent
                                End If

                                If Not String.IsNullOrEmpty(EMD_DisplayRS.EMD_Info.Source.PrimeHostID) Then
                                    objTicketCoupon.PrimeHostID = EMD_DisplayRS.EMD_Info.Source.PrimeHostID
                                End If

                                If Not String.IsNullOrEmpty(EMD_DisplayRS.EMD_Info.Source.TransactionDateTime) Then
                                    objTicketCoupon.TransactionDateTime = EMD_DisplayRS.EMD_Info.Source.TransactionDateTime
                                End If

                            End If



                        End If
                    End If
                End If




            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strNumeroBoleto = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing

                EMD_DisplayRS = Nothing

                strRespuesta = Nothing
                strMensaje = Nothing

                objCouponData = Nothing
                AirItineraryPricing = Nothing
                ItinTotalFare = Nothing
                BaseFare = Nothing
                NetFare = Nothing
                Taxes = Nothing
                Tax = Nothing
                TotalFare = Nothing
                TCCoupon = Nothing
                auxTCCoupon = Nothing
                FlightSegment = Nothing
                FareBasis = Nothing
                MarketingAirline = Nothing
                Customer = Nothing
                Payment = Nothing
                auxPayment = Nothing
                auxPaymentCard = Nothing
                PersonName = Nothing
                ItineraryRef = Nothing

                objErroresAlertas = Nothing

            End Try

            Return objTicketCoupon

        End Function
        Public Function EndTransaction(ByVal strReceivedFrom As String, _
                                       ByVal strEnd As String, _
                                       ByVal strCodigoSeguimiento As String, _
                                       ByVal intGDS As Integer, _
                                       ByVal intFirmaGDS As Integer, _
                                       ByVal intFirmaDB As Integer, _
                                       ByVal objSession As classSession) As classEndTransaction.classTransaction

            Dim EndTransactionRS As objSabreWS.EndTransaction.EndTransactionRS = Nothing

            Dim strMensaje As String = Nothing

            Dim objTransaction As classEndTransaction.classTransaction = Nothing
            Dim objErroresAlertas As classErroresAlertas = Nothing
            Try

                objIWebServices = New IWebServices
                EndTransactionRS = objIWebServices._EndTransaction(strReceivedFrom, _
                                                                   strEnd, _
                                                                   strCodigoSeguimiento, _
                                                                   intGDS, _
                                                                   intFirmaGDS, _
                                                                   intFirmaDB, _
                                                                   objSession)

                If EndTransactionRS IsNot Nothing Then
                    objTransaction = New classEndTransaction.classTransaction

                    If EndTransactionRS.ApplicationResults.Success IsNot Nothing Then
                        For i As Integer = 0 To EndTransactionRS.ApplicationResults.Success.Length - 1

                            If Not String.IsNullOrEmpty(EndTransactionRS.ApplicationResults.Success(i).timeStamp) Then
                                objTransaction.TimeStamp = EndTransactionRS.ApplicationResults.Success(i).timeStamp.ToString()
                            End If

                            If EndTransactionRS.ApplicationResults.Success(i).SystemSpecificResults IsNot Nothing Then

                                For x As Integer = 0 To EndTransactionRS.ApplicationResults.Success(i).SystemSpecificResults.Length - 1

                                    If EndTransactionRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand IsNot Nothing Then

                                        If Not String.IsNullOrEmpty(EndTransactionRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.LNIATA) Then
                                            objTransaction.LNIATA = EndTransactionRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.LNIATA.ToString()
                                        End If

                                        If Not String.IsNullOrEmpty(EndTransactionRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.Value) Then
                                            objTransaction.HostCommand = EndTransactionRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.Value.ToString()
                                        End If

                                    End If
                                Next

                            End If


                        Next
                    End If

                    If EndTransactionRS.ApplicationResults.Error IsNot Nothing Then
                        objErroresAlertas = New classErroresAlertas
                        objErroresAlertas.Errores = New List(Of String)
                        For i As Integer = 0 To EndTransactionRS.ApplicationResults.Error.Length - 1
                            If EndTransactionRS.ApplicationResults.Error(i).SystemSpecificResults IsNot Nothing Then
                                For x As Integer = 0 To EndTransactionRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                    For y As Integer = 0 To EndTransactionRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message.Length - 1
                                        If Not String.IsNullOrEmpty(EndTransactionRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(y).Value) Then
                                            objErroresAlertas.Errores.Add("ERROR: " & EndTransactionRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(y).Value.ToString)
                                        End If
                                    Next
                                Next
                            End If
                        Next
                        objTransaction.ErroresAlertas = New classErroresAlertas
                        objTransaction.ErroresAlertas = objErroresAlertas
                    End If

                    If EndTransactionRS.ApplicationResults.Warning IsNot Nothing Then
                        objErroresAlertas = New classErroresAlertas
                        objErroresAlertas.Alertas = New List(Of String)
                        For i As Integer = 0 To EndTransactionRS.ApplicationResults.Warning.Length - 1
                            If EndTransactionRS.ApplicationResults.Warning(i).SystemSpecificResults IsNot Nothing Then
                                For x As Integer = 0 To EndTransactionRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                                    For y As Integer = 0 To EndTransactionRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message.Length - 1
                                        If Not String.IsNullOrEmpty(EndTransactionRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message(y).Value) Then
                                            objErroresAlertas.Alertas.Add("WARNING: " & EndTransactionRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message(y).Value.ToString)
                                        End If
                                    Next
                                Next
                            End If
                        Next
                        objTransaction.ErroresAlertas = New classErroresAlertas
                        objTransaction.ErroresAlertas = objErroresAlertas
                    End If


                    If EndTransactionRS.ItineraryRef IsNot Nothing Then
                        objTransaction.ItineraryRef = New classEndTransaction.classItineraryRef
                        objTransaction.ItineraryRef.ID = EndTransactionRS.ItineraryRef.ID.ToString
                        If EndTransactionRS.ItineraryRef.Source IsNot Nothing Then
                            objTransaction.ItineraryRef.CreateDateTime = EndTransactionRS.ItineraryRef.Source.CreateDateTime.ToString
                        End If
                    End If


                    If EndTransactionRS.Text IsNot Nothing Then
                        objTransaction.Text = EndTransactionRS.Text
                    End If


                Else
                    objErroresAlertas = New classErroresAlertas
                    objErroresAlertas.Errores = New List(Of String)
                    objErroresAlertas.Errores.Add("ERROR: " & Constantes.ProblemasET)

                    objTransaction.ErroresAlertas = New classErroresAlertas
                    objTransaction.ErroresAlertas = objErroresAlertas
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strReceivedFrom = Nothing
                strEnd = Nothing

                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing

                EndTransactionRS = Nothing

                strMensaje = Nothing

            End Try

            Return objTransaction

        End Function
        Public Function AirRules(ByVal strCiudadOrigen As String, _
                                 ByVal strCiudadDestino As String, _
                                 ByVal strFechaSalida As String, _
                                 ByVal strFareBasis As String, _
                                 ByVal strTicketDesignator As String, _
                                 ByVal strAerolinea As String, _
                                 ByVal strAccount As String, _
                                 ByVal strCategory As String, _
                                 ByVal strRPH As String, _
                                 ByVal strCodigoSeguimiento As String, _
                                 ByVal intGDS As Integer, _
                                 ByVal intFirmaGDS As Integer, _
                                 ByVal intFirmaDB As Integer, _
                                 ByVal objSession As classSession) As classRegulaciones.classRegulacionTarifa

            Dim OTA_AirRulesRS As objSabreWS.OTA_AirRules.OTA_AirRulesRS = Nothing

            Dim objRegulaciones As List(Of classRegulaciones.classReglas) = Nothing
            Dim auxRegulaciones As classRegulaciones.classReglas = Nothing
            Dim strMensaje As String = Nothing

            Dim objRegulacionTarifa As classRegulaciones.classRegulacionTarifa = Nothing
            Dim objErroresAlertas As classErroresAlertas = Nothing
            Try

                objIWebServices = New IWebServices
                OTA_AirRulesRS = objIWebServices._OTA_AirRules(strCiudadOrigen, _
                                                               strCiudadDestino, _
                                                               strFechaSalida, _
                                                               strFareBasis, _
                                                               strTicketDesignator, _
                                                               strAerolinea, _
                                                               strAccount, _
                                                               strCategory, _
                                                               strRPH, _
                                                               strCodigoSeguimiento, _
                                                               intGDS, _
                                                               intFirmaGDS, _
                                                               intFirmaDB, _
                                                               objSession)

                If OTA_AirRulesRS IsNot Nothing Then
                    objRegulacionTarifa = New classRegulaciones.classRegulacionTarifa

                    If OTA_AirRulesRS.ApplicationResults.Success IsNot Nothing Then

                        If OTA_AirRulesRS.DuplicateFareInfo IsNot Nothing Then
                            objRegulacionTarifa.DuplicateFareInfo = OTA_AirRulesRS.DuplicateFareInfo.Text
                        Else
                            For i As Integer = 0 To OTA_AirRulesRS.ApplicationResults.Success.Length - 1

                                If Not String.IsNullOrEmpty(OTA_AirRulesRS.ApplicationResults.Success(i).timeStamp) Then
                                    objRegulacionTarifa.TimeStamp = OTA_AirRulesRS.ApplicationResults.Success(i).timeStamp.ToString()
                                End If

                                If OTA_AirRulesRS.ApplicationResults.Success(i).SystemSpecificResults IsNot Nothing Then

                                    For x As Integer = 0 To OTA_AirRulesRS.ApplicationResults.Success(i).SystemSpecificResults.Length - 1

                                        If OTA_AirRulesRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand IsNot Nothing Then

                                            If Not String.IsNullOrEmpty(OTA_AirRulesRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.LNIATA) Then
                                                objRegulacionTarifa.IATA_Number = OTA_AirRulesRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.LNIATA.ToString()
                                            End If

                                            If Not String.IsNullOrEmpty(OTA_AirRulesRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.Value) Then
                                                objRegulacionTarifa.HostCommand = OTA_AirRulesRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.Value.ToString()
                                            End If

                                        End If
                                    Next

                                End If

                            Next
                        End If
                    End If

                    If OTA_AirRulesRS.ApplicationResults.Error IsNot Nothing Then
                        objErroresAlertas = New classErroresAlertas
                        objErroresAlertas.Errores = New List(Of String)
                        For i As Integer = 0 To OTA_AirRulesRS.ApplicationResults.Error.Length - 1
                            If OTA_AirRulesRS.ApplicationResults.Error(i).SystemSpecificResults IsNot Nothing Then
                                For x As Integer = 0 To OTA_AirRulesRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                    For y As Integer = 0 To OTA_AirRulesRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message.Length - 1
                                        If Not String.IsNullOrEmpty(OTA_AirRulesRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(y).Value) Then
                                            objErroresAlertas.Errores.Add("ERROR: " & OTA_AirRulesRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(y).Value.ToString)
                                        End If
                                    Next
                                Next
                            End If
                        Next
                        objRegulacionTarifa.ErroresAlertas = New classErroresAlertas
                        objRegulacionTarifa.ErroresAlertas = objErroresAlertas
                    End If

                    If OTA_AirRulesRS.ApplicationResults.Warning IsNot Nothing Then
                        objErroresAlertas = New classErroresAlertas
                        objErroresAlertas.Alertas = New List(Of String)
                        For i As Integer = 0 To OTA_AirRulesRS.ApplicationResults.Warning.Length - 1
                            If OTA_AirRulesRS.ApplicationResults.Warning(i).SystemSpecificResults IsNot Nothing Then
                                For x As Integer = 0 To OTA_AirRulesRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                                    For y As Integer = 0 To OTA_AirRulesRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message.Length - 1
                                        If Not String.IsNullOrEmpty(OTA_AirRulesRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message(y).Value) Then
                                            objErroresAlertas.Alertas.Add("WARNING: " & OTA_AirRulesRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message(y).Value.ToString)
                                        End If
                                    Next
                                Next
                            End If
                        Next
                        objRegulacionTarifa.ErroresAlertas = New classErroresAlertas
                        objRegulacionTarifa.ErroresAlertas = objErroresAlertas
                    End If



                    If OTA_AirRulesRS.FareRuleInfo IsNot Nothing Then
                        If OTA_AirRulesRS.FareRuleInfo.Rules IsNot Nothing Then

                            objRegulaciones = New List(Of classRegulaciones.classReglas)
                            For i As Integer = 0 To OTA_AirRulesRS.FareRuleInfo.Rules.Length - 1
                                auxRegulaciones = New classRegulaciones.classReglas
                                auxRegulaciones.NumRegulacion = OTA_AirRulesRS.FareRuleInfo.Rules(i).RPH.ToString()
                                auxRegulaciones.Titulo = OTA_AirRulesRS.FareRuleInfo.Rules(i).Title.ToString()
                                auxRegulaciones.Text = OTA_AirRulesRS.FareRuleInfo.Rules(i).Text.Split(Constantes.FinLinea)
                                objRegulaciones.Add(auxRegulaciones)

                            Next

                            objRegulacionTarifa.Reglas = New List(Of classRegulaciones.classReglas)
                            objRegulacionTarifa.Reglas = objRegulaciones
                        End If
                    End If

                Else
                    objErroresAlertas = New classErroresAlertas
                    objErroresAlertas.Errores = New List(Of String)
                    objErroresAlertas.Errores.Add("ERROR: " & Constantes.ProblemasAirRules)

                    objRegulacionTarifa.ErroresAlertas = New classErroresAlertas
                    objRegulacionTarifa.ErroresAlertas = objErroresAlertas
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCiudadOrigen = Nothing
                strCiudadDestino = Nothing
                strFechaSalida = Nothing
                strFareBasis = Nothing
                strTicketDesignator = Nothing
                strAerolinea = Nothing
                strAccount = Nothing
                strCategory = Nothing
                strRPH = Nothing

                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing

                OTA_AirRulesRS = Nothing

                strMensaje = Nothing

            End Try

            Return objRegulacionTarifa

        End Function
        Public Function ARUNK(ByVal strCodigoSeguimiento As String, _
                              ByVal intGDS As Integer, _
                              ByVal intFirmaGDS As Integer, _
                              ByVal intFirmaDB As Integer, _
                              ByVal objSession As classSession) As classArunk

            Dim ArunkRS As objSabreWS.ARUNK.ARUNK_RS = Nothing

            Dim strMensaje As String = Nothing

            Dim objArunk As classArunk = Nothing
            Dim objErroresAlertas As classErroresAlertas = Nothing
            Try

                objIWebServices = New IWebServices
                ArunkRS = objIWebServices._ARUNK(strCodigoSeguimiento, _
                                                 intGDS, _
                                                 intFirmaGDS, _
                                                 intFirmaDB, _
                                                 objSession)

                If ArunkRS IsNot Nothing Then
                    objArunk = New classArunk

                    If ArunkRS.ApplicationResults.Success IsNot Nothing Then
                        For i As Integer = 0 To ArunkRS.ApplicationResults.Success.Length - 1

                            If Not String.IsNullOrEmpty(ArunkRS.ApplicationResults.Success(i).timeStamp) Then
                                objArunk.TimeStamp = ArunkRS.ApplicationResults.Success(i).timeStamp.ToString()
                            End If

                            If ArunkRS.ApplicationResults.Success(i).SystemSpecificResults IsNot Nothing Then

                                For x As Integer = 0 To ArunkRS.ApplicationResults.Success(i).SystemSpecificResults.Length - 1

                                    If ArunkRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand IsNot Nothing Then

                                        If Not String.IsNullOrEmpty(ArunkRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.LNIATA) Then
                                            objArunk.LNIATA = ArunkRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.LNIATA.ToString()
                                        End If

                                        If Not String.IsNullOrEmpty(ArunkRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.Value) Then
                                            objArunk.HostCommand = ArunkRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.Value.ToString()
                                        End If

                                    End If
                                Next

                            End If


                        Next
                    End If

                    If ArunkRS.ApplicationResults.Error IsNot Nothing Then
                        objErroresAlertas = New classErroresAlertas
                        objErroresAlertas.Errores = New List(Of String)
                        For i As Integer = 0 To ArunkRS.ApplicationResults.Error.Length - 1
                            If ArunkRS.ApplicationResults.Error(i).SystemSpecificResults IsNot Nothing Then
                                For x As Integer = 0 To ArunkRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                    For y As Integer = 0 To ArunkRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message.Length - 1
                                        If Not String.IsNullOrEmpty(ArunkRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(y).Value) Then
                                            objErroresAlertas.Errores.Add("ERROR: " & ArunkRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(y).Value.ToString)
                                        End If
                                    Next
                                Next
                            End If
                        Next
                        objArunk.ErroresAlertas = New classErroresAlertas
                        objArunk.ErroresAlertas = objErroresAlertas
                    End If

                    If ArunkRS.ApplicationResults.Warning IsNot Nothing Then
                        objErroresAlertas = New classErroresAlertas
                        objErroresAlertas.Alertas = New List(Of String)
                        For i As Integer = 0 To ArunkRS.ApplicationResults.Warning.Length - 1
                            If ArunkRS.ApplicationResults.Warning(i).SystemSpecificResults IsNot Nothing Then
                                For x As Integer = 0 To ArunkRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                                    For y As Integer = 0 To ArunkRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message.Length - 1
                                        If Not String.IsNullOrEmpty(ArunkRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message(y).Value) Then
                                            objErroresAlertas.Alertas.Add("WARNING: " & ArunkRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message(y).Value.ToString)
                                        End If
                                    Next
                                Next
                            End If
                        Next
                        objArunk.ErroresAlertas = New classErroresAlertas
                        objArunk.ErroresAlertas = objErroresAlertas
                    End If


                    If ArunkRS.Text IsNot Nothing Then
                        objArunk.Text = ArunkRS.Text
                    End If

                Else
                    objErroresAlertas = New classErroresAlertas
                    objErroresAlertas.Errores = New List(Of String)
                    objErroresAlertas.Errores.Add("ERROR: " & Constantes.ProblemasET)

                    objArunk.ErroresAlertas = New classErroresAlertas
                    objArunk.ErroresAlertas = objErroresAlertas
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally

                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing

                ArunkRS = Nothing

                strMensaje = Nothing

            End Try

            Return objArunk

        End Function
        Public Function IgnoreTransaction(ByVal strCodigoSeguimiento As String, _
                                          ByVal intGDS As Integer, _
                                          ByVal intFirmaGDS As Integer, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal objSession As classSession) As classIgnoreTransaction

            Dim IgnoreTransactionRS As objSabreWS.IgnoreTransaction.IgnoreTransactionRS = Nothing

            Dim strMensaje As String = Nothing

            Dim objIgnoreTransaction As classIgnoreTransaction = Nothing
            Dim objErroresAlertas As classErroresAlertas = Nothing
            Try

                objIWebServices = New IWebServices
                IgnoreTransactionRS = objIWebServices._IgnoreTransaction(strCodigoSeguimiento, _
                                                                         intGDS, _
                                                                         intFirmaGDS, _
                                                                         intFirmaDB, _
                                                                         objSession)



                If Not IgnoreTransactionRS Is Nothing Then
                    If Not IgnoreTransactionRS.ApplicationResults Is Nothing Then
                        objIgnoreTransaction = New classIgnoreTransaction

                        '*** Error
                        If IgnoreTransactionRS.ApplicationResults.Error IsNot Nothing Then
                            For i As Integer = 0 To IgnoreTransactionRS.ApplicationResults.Error.Length - 1
                                For x As Integer = 0 To IgnoreTransactionRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                    objIgnoreTransaction.LNIATA = IgnoreTransactionRS.ApplicationResults.Error(i).SystemSpecificResults(x).HostCommand.LNIATA
                                    objIgnoreTransaction.HostCommand = IgnoreTransactionRS.ApplicationResults.Error(i).SystemSpecificResults(x).HostCommand.Value

                                    objErroresAlertas = New classErroresAlertas
                                    objErroresAlertas.Errores = New List(Of String)
                                    For z As Integer = 0 To IgnoreTransactionRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message.Length - 1
                                        objErroresAlertas.Errores.Add("ERROR : " & IgnoreTransactionRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(z).Value)
                                    Next
                                    objIgnoreTransaction.ErroresAlertas = New classErroresAlertas
                                    objIgnoreTransaction.ErroresAlertas = objErroresAlertas

                                    Exit Try
                                Next
                            Next
                        End If

                        '*** Error
                        If IgnoreTransactionRS.ApplicationResults.Warning IsNot Nothing Then
                            For i As Integer = 0 To IgnoreTransactionRS.ApplicationResults.Warning.Length - 1
                                For x As Integer = 0 To IgnoreTransactionRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                                    objIgnoreTransaction.LNIATA = IgnoreTransactionRS.ApplicationResults.Warning(i).SystemSpecificResults(x).HostCommand.LNIATA
                                    objIgnoreTransaction.HostCommand = IgnoreTransactionRS.ApplicationResults.Warning(i).SystemSpecificResults(x).HostCommand.Value

                                    objErroresAlertas = New classErroresAlertas
                                    objErroresAlertas.Alertas = New List(Of String)
                                    For z As Integer = 0 To IgnoreTransactionRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message.Length - 1
                                        objErroresAlertas.Alertas.Add("ERROR : " & IgnoreTransactionRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message(z).Value)
                                    Next
                                    objIgnoreTransaction.ErroresAlertas = New classErroresAlertas
                                    objIgnoreTransaction.ErroresAlertas = objErroresAlertas

                                    Exit Try
                                Next
                            Next
                        End If


                        '*** Resultados
                        If Not IgnoreTransactionRS.ApplicationResults.Success Is Nothing Then
                            objIgnoreTransaction.Status = IgnoreTransactionRS.ApplicationResults.status

                            For i As Integer = 0 To IgnoreTransactionRS.ApplicationResults.Success.Length - 1
                                For x As Integer = 0 To IgnoreTransactionRS.ApplicationResults.Success(i).SystemSpecificResults.Length - 1
                                    objIgnoreTransaction.LNIATA = IgnoreTransactionRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.LNIATA
                                    objIgnoreTransaction.HostCommand = IgnoreTransactionRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.Value
                                Next
                            Next


                        End If
                    Else
                        Err.Raise(-1111, "WSSabre.IgnoreTransaction", "No se encontro resultado en IgnoreTransactionRS.ApplicationResults")
                    End If
                Else
                    Err.Raise(-1111, "WSSabre.IgnoreTransaction", "No se encontro resultado en IgnoreTransactionRS")
                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally

                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing

                IgnoreTransactionRS = Nothing

                strMensaje = Nothing

            End Try

            Return objIgnoreTransaction

        End Function
        Public Function DesignatePrinter(ByVal strHardcopy As String, _
                                         ByVal strTicket As String, _
                                         ByVal strProfile As String, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intGDS As Integer, _
                                         ByVal intFirmaGDS As Integer, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal objSession As classSession) As classDesignatePrinter

            Dim DesignatePrinterRS As objSabreWS.DesignatePrinter.DesignatePrinterRS = Nothing

            Dim strMensaje As String = Nothing

            Dim objDesignatePrinter As classDesignatePrinter = Nothing
            Dim objErroresAlertas As classErroresAlertas = Nothing
            Try

                objIWebServices = New IWebServices
                DesignatePrinterRS = objIWebServices._DesignatePrinter(strHardcopy, _
                                                                       strTicket, _
                                                                       strProfile, _
                                                                       strCodigoSeguimiento, _
                                                                       intGDS, _
                                                                       intFirmaGDS, _
                                                                       intFirmaDB, _
                                                                       objSession)


                If Not DesignatePrinterRS Is Nothing Then
                    If Not DesignatePrinterRS.ApplicationResults Is Nothing Then
                        objDesignatePrinter = New classDesignatePrinter

                        '*** Error
                        If DesignatePrinterRS.ApplicationResults.Error IsNot Nothing Then
                            For i As Integer = 0 To DesignatePrinterRS.ApplicationResults.Error.Length - 1
                                For x As Integer = 0 To DesignatePrinterRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                    objDesignatePrinter.LNIATA = DesignatePrinterRS.ApplicationResults.Error(i).SystemSpecificResults(x).HostCommand.LNIATA
                                    objDesignatePrinter.HostCommand = DesignatePrinterRS.ApplicationResults.Error(i).SystemSpecificResults(x).HostCommand.Value

                                    objErroresAlertas = New classErroresAlertas
                                    objErroresAlertas.Errores = New List(Of String)
                                    For z As Integer = 0 To DesignatePrinterRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message.Length - 1
                                        objErroresAlertas.Errores.Add("ERROR : " & DesignatePrinterRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(z).Value)
                                    Next
                                    objDesignatePrinter.ErroresAlertas = New classErroresAlertas
                                    objDesignatePrinter.ErroresAlertas = objErroresAlertas

                                    Exit Try
                                Next
                            Next
                        End If

                        '*** Error
                        If DesignatePrinterRS.ApplicationResults.Warning IsNot Nothing Then
                            For i As Integer = 0 To DesignatePrinterRS.ApplicationResults.Warning.Length - 1
                                For x As Integer = 0 To DesignatePrinterRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                                    objDesignatePrinter.LNIATA = DesignatePrinterRS.ApplicationResults.Warning(i).SystemSpecificResults(x).HostCommand.LNIATA
                                    objDesignatePrinter.HostCommand = DesignatePrinterRS.ApplicationResults.Warning(i).SystemSpecificResults(x).HostCommand.Value

                                    objErroresAlertas = New classErroresAlertas
                                    objErroresAlertas.Alertas = New List(Of String)
                                    For z As Integer = 0 To DesignatePrinterRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message.Length - 1
                                        objErroresAlertas.Alertas.Add("ERROR : " & DesignatePrinterRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message(z).Value)
                                    Next
                                    objDesignatePrinter.ErroresAlertas = New classErroresAlertas
                                    objDesignatePrinter.ErroresAlertas = objErroresAlertas

                                    Exit Try
                                Next
                            Next
                        End If


                        '*** Resultados
                        If Not DesignatePrinterRS.ApplicationResults.Success Is Nothing Then
                            objDesignatePrinter.Status = DesignatePrinterRS.ApplicationResults.status

                            For i As Integer = 0 To DesignatePrinterRS.ApplicationResults.Success.Length - 1
                                For x As Integer = 0 To DesignatePrinterRS.ApplicationResults.Success(i).SystemSpecificResults.Length - 1
                                    objDesignatePrinter.LNIATA = DesignatePrinterRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.LNIATA
                                    objDesignatePrinter.HostCommand = DesignatePrinterRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.Value
                                Next
                            Next


                        End If
                    Else
                        Err.Raise(-1111, "WSSabre.DesignatePrinter", "No se encontro resultado en DesignatePrinterRS.ApplicationResults")
                    End If
                Else
                    Err.Raise(-1111, "WSSabre.DesignatePrinter", "No se encontro resultado en DesignatePrinterRS")
                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strHardcopy = Nothing
                strTicket = Nothing
                strProfile = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing

                DesignatePrinterRS = Nothing

                strMensaje = Nothing

            End Try

            Return objDesignatePrinter

        End Function
        Public Function RulesFromPrice(ByVal strProfile As String, _
                                       ByVal strCodigoSeguimiento As String, _
                                       ByVal intGDS As Integer, _
                                       ByVal intFirmaGDS As Integer, _
                                       ByVal intFirmaDB As Integer, _
                                       ByVal objSession As classSession) As String

            Dim DesignatePrinterRS As objSabreWS.RulesFromPrice.RulesFromPriceRS = Nothing

            Dim strMensaje As String = Nothing

            Dim objDesignatePrinter As classDesignatePrinter = Nothing
            Dim objErroresAlertas As classErroresAlertas = Nothing
            Try

                objIWebServices = New IWebServices
                'DesignatePrinterRS = 
                objIWebServices._RulesFromPrice(strProfile, _
                                                 strCodigoSeguimiento, _
                                                 intGDS, _
                                                 intFirmaGDS, _
                                                 intFirmaDB, _
                                                 objSession)


                'If Not DesignatePrinterRS Is Nothing Then
                '    If Not DesignatePrinterRS.ApplicationResults Is Nothing Then
                '        objDesignatePrinter = New classDesignatePrinter

                '        '*** Error
                '        If DesignatePrinterRS.ApplicationResults.Error IsNot Nothing Then
                '            For i As Integer = 0 To DesignatePrinterRS.ApplicationResults.Error.Length - 1
                '                For x As Integer = 0 To DesignatePrinterRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                '                    objDesignatePrinter.LNIATA = DesignatePrinterRS.ApplicationResults.Error(i).SystemSpecificResults(x).HostCommand.LNIATA
                '                    objDesignatePrinter.HostCommand = DesignatePrinterRS.ApplicationResults.Error(i).SystemSpecificResults(x).HostCommand.Value

                '                    objErroresAlertas = New classErroresAlertas
                '                    objErroresAlertas.Errores = New List(Of String)
                '                    For z As Integer = 0 To DesignatePrinterRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message.Length - 1
                '                        objErroresAlertas.Errores.Add("ERROR : " & DesignatePrinterRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(z).Value)
                '                    Next
                '                    objDesignatePrinter.ErroresAlertas = New classErroresAlertas
                '                    objDesignatePrinter.ErroresAlertas = objErroresAlertas

                '                    Exit Try
                '                Next
                '            Next
                '        End If

                '        '*** Error
                '        If DesignatePrinterRS.ApplicationResults.Warning IsNot Nothing Then
                '            For i As Integer = 0 To DesignatePrinterRS.ApplicationResults.Warning.Length - 1
                '                For x As Integer = 0 To DesignatePrinterRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                '                    objDesignatePrinter.LNIATA = DesignatePrinterRS.ApplicationResults.Warning(i).SystemSpecificResults(x).HostCommand.LNIATA
                '                    objDesignatePrinter.HostCommand = DesignatePrinterRS.ApplicationResults.Warning(i).SystemSpecificResults(x).HostCommand.Value

                '                    objErroresAlertas = New classErroresAlertas
                '                    objErroresAlertas.Alertas = New List(Of String)
                '                    For z As Integer = 0 To DesignatePrinterRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message.Length - 1
                '                        objErroresAlertas.Alertas.Add("ERROR : " & DesignatePrinterRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message(z).Value)
                '                    Next
                '                    objDesignatePrinter.ErroresAlertas = New classErroresAlertas
                '                    objDesignatePrinter.ErroresAlertas = objErroresAlertas

                '                    Exit Try
                '                Next
                '            Next
                '        End If


                '        '*** Resultados
                '        If Not DesignatePrinterRS.ApplicationResults.Success Is Nothing Then
                '            objDesignatePrinter.Status = DesignatePrinterRS.ApplicationResults.status

                '            For i As Integer = 0 To DesignatePrinterRS.ApplicationResults.Success.Length - 1
                '                For x As Integer = 0 To DesignatePrinterRS.ApplicationResults.Success(i).SystemSpecificResults.Length - 1
                '                    objDesignatePrinter.LNIATA = DesignatePrinterRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.LNIATA
                '                    objDesignatePrinter.HostCommand = DesignatePrinterRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.Value
                '                Next
                '            Next


                '        End If
                '    Else
                '        Err.Raise(-1111, "WSSabre.DesignatePrinter", "No se encontro resultado en DesignatePrinterRS.ApplicationResults")
                '    End If
                'Else
                '    Err.Raise(-1111, "WSSabre.DesignatePrinter", "No se encontro resultado en DesignatePrinterRS")
                'End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally

                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing

                objSession = Nothing

                DesignatePrinterRS = Nothing

                strMensaje = Nothing

            End Try

            Return ""

        End Function
        Public Function TravelItineraryAddInfo(ByVal strProfile As String, _
                                               ByVal strCodigoSeguimiento As String, _
                                               ByVal intGDS As Integer, _
                                               ByVal intFirmaGDS As Integer, _
                                               ByVal intFirmaDB As Integer, _
                                               ByVal objSession As classSession) As String

            Dim DesignatePrinterRS As objSabreWS.TravelItineraryAddInfo.TravelItineraryAddInfoRS = Nothing

            Dim strMensaje As String = Nothing

            Dim objDesignatePrinter As classDesignatePrinter = Nothing
            Dim objErroresAlertas As classErroresAlertas = Nothing
            Try

                objIWebServices = New IWebServices
                'DesignatePrinterRS = 
                objIWebServices._TravelItineraryAddInfo(strProfile, _
                                                        strCodigoSeguimiento, _
                                                        intGDS, _
                                                        intFirmaGDS, _
                                                        intFirmaDB, _
                                                        objSession)


                'If Not DesignatePrinterRS Is Nothing Then
                '    If Not DesignatePrinterRS.ApplicationResults Is Nothing Then
                '        objDesignatePrinter = New classDesignatePrinter

                '        '*** Error
                '        If DesignatePrinterRS.ApplicationResults.Error IsNot Nothing Then
                '            For i As Integer = 0 To DesignatePrinterRS.ApplicationResults.Error.Length - 1
                '                For x As Integer = 0 To DesignatePrinterRS.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                '                    objDesignatePrinter.LNIATA = DesignatePrinterRS.ApplicationResults.Error(i).SystemSpecificResults(x).HostCommand.LNIATA
                '                    objDesignatePrinter.HostCommand = DesignatePrinterRS.ApplicationResults.Error(i).SystemSpecificResults(x).HostCommand.Value

                '                    objErroresAlertas = New classErroresAlertas
                '                    objErroresAlertas.Errores = New List(Of String)
                '                    For z As Integer = 0 To DesignatePrinterRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message.Length - 1
                '                        objErroresAlertas.Errores.Add("ERROR : " & DesignatePrinterRS.ApplicationResults.Error(i).SystemSpecificResults(x).Message(z).Value)
                '                    Next
                '                    objDesignatePrinter.ErroresAlertas = New classErroresAlertas
                '                    objDesignatePrinter.ErroresAlertas = objErroresAlertas

                '                    Exit Try
                '                Next
                '            Next
                '        End If

                '        '*** Error
                '        If DesignatePrinterRS.ApplicationResults.Warning IsNot Nothing Then
                '            For i As Integer = 0 To DesignatePrinterRS.ApplicationResults.Warning.Length - 1
                '                For x As Integer = 0 To DesignatePrinterRS.ApplicationResults.Warning(i).SystemSpecificResults.Length - 1
                '                    objDesignatePrinter.LNIATA = DesignatePrinterRS.ApplicationResults.Warning(i).SystemSpecificResults(x).HostCommand.LNIATA
                '                    objDesignatePrinter.HostCommand = DesignatePrinterRS.ApplicationResults.Warning(i).SystemSpecificResults(x).HostCommand.Value

                '                    objErroresAlertas = New classErroresAlertas
                '                    objErroresAlertas.Alertas = New List(Of String)
                '                    For z As Integer = 0 To DesignatePrinterRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message.Length - 1
                '                        objErroresAlertas.Alertas.Add("ERROR : " & DesignatePrinterRS.ApplicationResults.Warning(i).SystemSpecificResults(x).Message(z).Value)
                '                    Next
                '                    objDesignatePrinter.ErroresAlertas = New classErroresAlertas
                '                    objDesignatePrinter.ErroresAlertas = objErroresAlertas

                '                    Exit Try
                '                Next
                '            Next
                '        End If


                '        '*** Resultados
                '        If Not DesignatePrinterRS.ApplicationResults.Success Is Nothing Then
                '            objDesignatePrinter.Status = DesignatePrinterRS.ApplicationResults.status

                '            For i As Integer = 0 To DesignatePrinterRS.ApplicationResults.Success.Length - 1
                '                For x As Integer = 0 To DesignatePrinterRS.ApplicationResults.Success(i).SystemSpecificResults.Length - 1
                '                    objDesignatePrinter.LNIATA = DesignatePrinterRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.LNIATA
                '                    objDesignatePrinter.HostCommand = DesignatePrinterRS.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.Value
                '                Next
                '            Next


                '        End If
                '    Else
                '        Err.Raise(-1111, "WSSabre.DesignatePrinter", "No se encontro resultado en DesignatePrinterRS.ApplicationResults")
                '    End If
                'Else
                '    Err.Raise(-1111, "WSSabre.DesignatePrinter", "No se encontro resultado en DesignatePrinterRS")
                'End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally

                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing

                DesignatePrinterRS = Nothing

                strMensaje = Nothing

            End Try

            Return ""

        End Function
        Public Function PromotionalShopping(ByVal strPseudoCityCode As String, _
                                            ByVal intRequestStep As Integer, _
                                            ByVal strDepartureCity As String, _
                                            ByVal strArrivalCity As String, _
                                            ByVal strCarrier As String, _
                                            ByVal strRequestCode As String, _
                                            ByVal strRequestType As String, _
                                            ByVal strOutboundDateTime As String, _
                                            ByVal strInboundDateTime As String, _
                                            ByVal intDateRange As Integer, _
                                            ByVal strFareBasisCode As String, _
                                            ByVal strFareAmount As String, _
                                            ByVal strFareCurrency As String, _
                                            ByVal strPassengerType As String, _
                                            ByVal strPassengerCount As String, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intGDS As Integer, _
                                            ByVal intFirmaGDS As Integer, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal objSession As classSession) As PromotionalShopping.classPromotionalShopping

            Dim objOTA_BestFareFinderRS As objSabreWS.PromotionalShopping.OTA_BestFareFinderRS = Nothing
            Dim objOTA_BestFareFinderRSSolutions As objSabreWS.PromotionalShopping.OTA_BestFareFinderRSSolutions = Nothing
            Dim objOTA_BestFareFinderRSErrors As objSabreWS.PromotionalShopping.OTA_BestFareFinderRSErrors = Nothing
            Dim objPromotionalShopping As PromotionalShopping.classPromotionalShopping = Nothing
            Dim auxFechasDisponibles As PromotionalShopping.classFechasDisponibles = Nothing
            Dim lstFechasDisponibles As List(Of PromotionalShopping.classFechasDisponibles) = Nothing
            Dim intIndex As Integer = -1
            '---
            Dim auxSegmento As PromotionalShopping.classSegmento = Nothing
            Dim lstSegmento As List(Of PromotionalShopping.classSegmento) = Nothing
            Dim auxDetalleVuelosDisponibles As PromotionalShopping.classDetalleVuelosDisponibles = Nothing
            Dim lstDetalleVuelosDisponibles As List(Of PromotionalShopping.classDetalleVuelosDisponibles) = Nothing
            Dim auxDepartureArrival As PromotionalShopping.classDepartureArrival = Nothing
            Dim auxAerolinea As PromotionalShopping.classAerolinea = Nothing
            '----
            Dim objInbound As PromotionalShopping.classInbound = Nothing
            '----
            Dim auxErroresAlertas As classErroresAlertas = Nothing
            Dim strFechaOut As String = Nothing
            Dim auxFecha As String = Nothing
            Try

                objIWebServices = New IWebServices
                objOTA_BestFareFinderRS = objIWebServices._PromotionalShopping(strPseudoCityCode, _
                                                                                intRequestStep, _
                                                                                strDepartureCity, _
                                                                                strArrivalCity, _
                                                                                strCarrier, _
                                                                                strRequestCode, _
                                                                                strRequestType, _
                                                                                strOutboundDateTime, _
                                                                                strInboundDateTime, _
                                                                                intDateRange, _
                                                                                strFareBasisCode, _
                                                                                strFareAmount, _
                                                                                strFareCurrency, _
                                                                                strPassengerType, _
                                                                                strPassengerCount, _
                                                                                strCodigoSeguimiento, _
                                                                                intGDS, _
                                                                                intFirmaGDS, _
                                                                                intFirmaDB, _
                                                                                objSession)

                If objOTA_BestFareFinderRS IsNot Nothing Then

                    If objOTA_BestFareFinderRS.ItemsElementName IsNot Nothing Then
                        For i As Integer = 0 To objOTA_BestFareFinderRS.ItemsElementName.Length - 1
                            If objOTA_BestFareFinderRS.ItemsElementName(i) = GDS_NM_WebServicesSabre.PromotionalShopping.ItemsChoiceType2.Solutions Then
                                intIndex = i
                            End If

                        Next

                        If intIndex = -1 Then
                            '****** Errores
                            If objOTA_BestFareFinderRS.Items IsNot Nothing Then
                                If objOTA_BestFareFinderRS.Items(0) IsNot Nothing Then

                                    objOTA_BestFareFinderRSErrors = New objSabreWS.PromotionalShopping.OTA_BestFareFinderRSErrors
                                    objOTA_BestFareFinderRSErrors = objOTA_BestFareFinderRS.Items(0)

                                    If objOTA_BestFareFinderRSErrors.Error IsNot Nothing Then
                                        For i As Integer = 0 To objOTA_BestFareFinderRSErrors.Error.Length - 1
                                            If objPromotionalShopping Is Nothing Then objPromotionalShopping = New PromotionalShopping.classPromotionalShopping
                                            If objPromotionalShopping.Errores Is Nothing Then objPromotionalShopping.Errores = New classErroresAlertas
                                            If objPromotionalShopping.Errores.Errores Is Nothing Then objPromotionalShopping.Errores.Errores = New List(Of String)
                                            objPromotionalShopping.Errores.Errores.Add(objOTA_BestFareFinderRSErrors.Error(i).Text)
                                        Next
                                    End If
                                End If
                            End If


                        ElseIf intIndex > 0 Then


                            If objOTA_BestFareFinderRS.Items IsNot Nothing Then
                                If objOTA_BestFareFinderRS.Items(intIndex) IsNot Nothing Then

                                    objOTA_BestFareFinderRSSolutions = New objSabreWS.PromotionalShopping.OTA_BestFareFinderRSSolutions
                                    objOTA_BestFareFinderRSSolutions = objOTA_BestFareFinderRS.Items(intIndex)

                                    If objOTA_BestFareFinderRSSolutions IsNot Nothing Then
                                        If objOTA_BestFareFinderRSSolutions.Outbound IsNot Nothing Then

                                            For i As Integer = 0 To objOTA_BestFareFinderRSSolutions.Outbound.Length - 1

                                                auxFecha = Format(objOTA_BestFareFinderRSSolutions.Outbound(i).DepartureDate, Constantes.IWS_DATE_FORMAT_FILE5)
                                                strFechaOut = auxFecha
                                                auxFecha = Nothing

                                                If objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound IsNot Nothing Then

                                                    For x As Integer = 0 To objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound.Length - 1

                                                        objInbound = New PromotionalShopping.classInbound
                                                        auxFechasDisponibles = New PromotionalShopping.classFechasDisponibles

                                                        If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).FareBasisCode) Then
                                                            objInbound.FareBasis = objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).FareBasisCode
                                                            auxFechasDisponibles.FareBasis = objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).FareBasisCode
                                                        End If


                                                        auxFecha = Format(objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).DepartureDate, Constantes.IWS_DATE_FORMAT_FILE5)
                                                        objInbound.FechaIn = auxFecha
                                                        auxFechasDisponibles.Fechas = auxFecha
                                                        auxFecha = Nothing


                                                        If objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(0).Availability Then
                                                            objInbound.Availability = 1
                                                            auxFechasDisponibles.Disponible = 1
                                                        End If


                                                        If objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules IsNot Nothing Then

                                                            For y As Integer = 0 To objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules.Length - 1
                                                                lstSegmento = New List(Of PromotionalShopping.classSegmento)

                                                                If objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y) IsNot Nothing Then

                                                                    For z As Integer = 0 To objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y).Length - 1

                                                                        auxSegmento = New PromotionalShopping.classSegmento

                                                                        If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).MarriageGrp) Then
                                                                            auxSegmento.SegmentoCasado = objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).MarriageGrp
                                                                        End If

                                                                        If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).Equipment) Then
                                                                            auxSegmento.Equipo = objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).Equipment
                                                                        End If

                                                                        If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).ElapsedTime) Then
                                                                            auxSegmento.Tiempo = objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).ElapsedTime
                                                                        End If

                                                                        If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).ResBookDesigCode) Then
                                                                            auxSegmento.ClaseReserva = objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).ResBookDesigCode
                                                                        End If

                                                                        If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).StopQuantity) Then
                                                                            auxSegmento.StopQuantity = objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).StopQuantity
                                                                        End If

                                                                        'objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(i).Schedules(y)(z).Stops(0)

                                                                        If objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).MarketingAirline IsNot Nothing Then
                                                                            auxAerolinea = New PromotionalShopping.classAerolinea

                                                                            If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).MarketingAirline.Code) Then
                                                                                auxAerolinea.Codigo = objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).MarketingAirline.Code
                                                                            End If

                                                                            If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).MarketingAirline.FlightNumber) Then
                                                                                auxAerolinea.NumeroVuelo = objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).MarketingAirline.FlightNumber
                                                                            End If

                                                                            If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).MarketingAirline.ShortName) Then
                                                                                auxAerolinea.Nombre = objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).MarketingAirline.ShortName
                                                                            End If

                                                                            auxSegmento.Aerolinea = New PromotionalShopping.classAerolinea
                                                                            auxSegmento.Aerolinea = auxAerolinea
                                                                            auxAerolinea = Nothing
                                                                        End If

                                                                        If objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).OperatingAirline IsNot Nothing Then
                                                                            auxAerolinea = New PromotionalShopping.classAerolinea

                                                                            If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).OperatingAirline.Code) Then
                                                                                auxAerolinea.Codigo = objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).OperatingAirline.Code
                                                                            End If

                                                                            If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).OperatingAirline.FlightNumber) Then
                                                                                auxAerolinea.NumeroVuelo = objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).OperatingAirline.FlightNumber
                                                                            End If

                                                                            If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).OperatingAirline.ShortName) Then
                                                                                auxAerolinea.Nombre = objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).OperatingAirline.ShortName
                                                                            End If

                                                                            auxSegmento.Operadora = New PromotionalShopping.classAerolinea
                                                                            auxSegmento.Operadora = auxAerolinea
                                                                            auxAerolinea = Nothing
                                                                        End If

                                                                        'Departure
                                                                        If objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).Departure IsNot Nothing Then
                                                                            auxDepartureArrival = New PromotionalShopping.classDepartureArrival

                                                                            If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).Departure.Airport) Then
                                                                                auxDepartureArrival.Airport = objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).Departure.Airport
                                                                            End If

                                                                            auxFecha = Format(objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).Departure.DateTime, Constantes.IWS_DATE_FORMAT_INSERT_2)
                                                                            auxDepartureArrival.DateTime = auxFecha
                                                                            auxFecha = Nothing

                                                                            auxDepartureArrival.GMTOffset = objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).Departure.GMTOffset

                                                                            auxSegmento.Departure = New PromotionalShopping.classDepartureArrival
                                                                            auxSegmento.Departure = auxDepartureArrival
                                                                        End If

                                                                        'Arrival
                                                                        If objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).Arrival IsNot Nothing Then
                                                                            auxDepartureArrival = New PromotionalShopping.classDepartureArrival

                                                                            If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).Arrival.Airport) Then
                                                                                auxDepartureArrival.Airport = objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).Arrival.Airport
                                                                            End If

                                                                            auxFecha = Format(objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).Arrival.DateTime, Constantes.IWS_DATE_FORMAT_INSERT_2)
                                                                            auxDepartureArrival.DateTime = auxFecha
                                                                            auxFecha = Nothing

                                                                            auxDepartureArrival.GMTOffset = objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(x).Schedules(y)(z).Arrival.GMTOffset

                                                                            auxSegmento.Arrival = New PromotionalShopping.classDepartureArrival
                                                                            auxSegmento.Arrival = auxDepartureArrival
                                                                        End If

                                                                        If lstSegmento Is Nothing Then lstSegmento = New List(Of PromotionalShopping.classSegmento)
                                                                        lstSegmento.Add(auxSegmento)
                                                                        auxSegmento = Nothing

                                                                    Next

                                                                End If

                                                                auxDetalleVuelosDisponibles = New PromotionalShopping.classDetalleVuelosDisponibles
                                                                auxDetalleVuelosDisponibles.Segmentos = New List(Of PromotionalShopping.classSegmento)
                                                                auxDetalleVuelosDisponibles.Segmentos = lstSegmento
                                                                lstSegmento = Nothing

                                                                If lstDetalleVuelosDisponibles Is Nothing Then lstDetalleVuelosDisponibles = New List(Of PromotionalShopping.classDetalleVuelosDisponibles)
                                                                lstDetalleVuelosDisponibles.Add(auxDetalleVuelosDisponibles)
                                                                auxDetalleVuelosDisponibles = Nothing
                                                            Next

                                                            If objPromotionalShopping Is Nothing Then objPromotionalShopping = New PromotionalShopping.classPromotionalShopping
                                                            If objPromotionalShopping.VuelosDisponibles Is Nothing Then objPromotionalShopping.VuelosDisponibles = New PromotionalShopping.classVuelosDisponibilidad
                                                            If objPromotionalShopping.VuelosDisponibles.DetalleVuelosDisponibles Is Nothing Then objPromotionalShopping.VuelosDisponibles.DetalleVuelosDisponibles = New List(Of PromotionalShopping.classDetalleVuelosDisponibles)

                                                            objPromotionalShopping.VuelosDisponibles.DetalleVuelosDisponibles = lstDetalleVuelosDisponibles
                                                            objPromotionalShopping.VuelosDisponibles.FechaOut = strFechaOut

                                                            If objInbound IsNot Nothing Then
                                                                If objPromotionalShopping.VuelosDisponibles.InBound Is Nothing Then objPromotionalShopping.VuelosDisponibles.InBound = New PromotionalShopping.classInbound
                                                                objPromotionalShopping.VuelosDisponibles.InBound = objInbound
                                                                objInbound = Nothing
                                                            End If

                                                            lstDetalleVuelosDisponibles = Nothing

                                                        Else
                                                            If lstFechasDisponibles Is Nothing Then lstFechasDisponibles = New List(Of PromotionalShopping.classFechasDisponibles)
                                                            lstFechasDisponibles.Add(auxFechasDisponibles)

                                                            auxFechasDisponibles = Nothing

                                                        End If
                                                    Next

                                                    If lstFechasDisponibles IsNot Nothing Then
                                                        If objPromotionalShopping Is Nothing Then objPromotionalShopping = New PromotionalShopping.classPromotionalShopping
                                                        If objPromotionalShopping.FechasDisponibles Is Nothing Then objPromotionalShopping.FechasDisponibles = New List(Of PromotionalShopping.classFechasDisponibles)
                                                        objPromotionalShopping.FechasDisponibles = lstFechasDisponibles
                                                    End If

                                                Else

                                                    auxFechasDisponibles = New PromotionalShopping.classFechasDisponibles
                                                    If objOTA_BestFareFinderRSSolutions.Outbound(i).Availability Then
                                                        auxFechasDisponibles.Disponible = 1
                                                    End If

                                                    If objOTA_BestFareFinderRSSolutions.Outbound(i).BookingCodeOverride IsNot Nothing Then

                                                    End If


                                                    auxFecha = Format(objOTA_BestFareFinderRSSolutions.Outbound(i).DepartureDate, Constantes.IWS_DATE_FORMAT_FILE5)
                                                    auxFechasDisponibles.Fechas = auxFecha
                                                    auxFecha = Nothing


                                                    If objOTA_BestFareFinderRSSolutions.Outbound(i).FareBasisCode IsNot Nothing Then
                                                        auxFechasDisponibles.FareBasis = objOTA_BestFareFinderRSSolutions.Outbound(i).FareBasisCode
                                                    End If

                                                    If lstFechasDisponibles Is Nothing Then lstFechasDisponibles = New List(Of PromotionalShopping.classFechasDisponibles)
                                                    lstFechasDisponibles.Add(auxFechasDisponibles)

                                                    auxFechasDisponibles = Nothing




                                                    If objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules IsNot Nothing Then

                                                        For y As Integer = 0 To objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules.Length - 1
                                                            lstSegmento = New List(Of PromotionalShopping.classSegmento)

                                                            If objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y) IsNot Nothing Then

                                                                For z As Integer = 0 To objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y).Length - 1

                                                                    auxSegmento = New PromotionalShopping.classSegmento

                                                                    If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).MarriageGrp) Then
                                                                        auxSegmento.SegmentoCasado = objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).MarriageGrp
                                                                    End If

                                                                    If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).Equipment) Then
                                                                        auxSegmento.Equipo = objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).Equipment
                                                                    End If

                                                                    If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).ElapsedTime) Then
                                                                        auxSegmento.Tiempo = objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).ElapsedTime
                                                                    End If

                                                                    If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).ResBookDesigCode) Then
                                                                        auxSegmento.ClaseReserva = objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).ResBookDesigCode
                                                                    End If

                                                                    If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).StopQuantity) Then
                                                                        auxSegmento.StopQuantity = objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).StopQuantity
                                                                    End If

                                                                    'objOTA_BestFareFinderRSSolutions.Outbound(i).Inbound(i).Schedules(y)(z).Stops(0)

                                                                    If objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).MarketingAirline IsNot Nothing Then
                                                                        auxAerolinea = New PromotionalShopping.classAerolinea

                                                                        If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).MarketingAirline.Code) Then
                                                                            auxAerolinea.Codigo = objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).MarketingAirline.Code
                                                                        End If

                                                                        If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).MarketingAirline.FlightNumber) Then
                                                                            auxAerolinea.NumeroVuelo = objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).MarketingAirline.FlightNumber
                                                                        End If

                                                                        If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).MarketingAirline.ShortName) Then
                                                                            auxAerolinea.Nombre = objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).MarketingAirline.ShortName
                                                                        End If

                                                                        auxSegmento.Aerolinea = New PromotionalShopping.classAerolinea
                                                                        auxSegmento.Aerolinea = auxAerolinea
                                                                        auxAerolinea = Nothing
                                                                    End If

                                                                    If objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).OperatingAirline IsNot Nothing Then
                                                                        auxAerolinea = New PromotionalShopping.classAerolinea

                                                                        If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).OperatingAirline.Code) Then
                                                                            auxAerolinea.Codigo = objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).OperatingAirline.Code
                                                                        End If

                                                                        If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).OperatingAirline.FlightNumber) Then
                                                                            auxAerolinea.NumeroVuelo = objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).OperatingAirline.FlightNumber
                                                                        End If

                                                                        If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).OperatingAirline.ShortName) Then
                                                                            auxAerolinea.Nombre = objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).OperatingAirline.ShortName
                                                                        End If

                                                                        auxSegmento.Operadora = New PromotionalShopping.classAerolinea
                                                                        auxSegmento.Operadora = auxAerolinea
                                                                        auxAerolinea = Nothing
                                                                    End If

                                                                    'Departure
                                                                    If objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).Departure IsNot Nothing Then
                                                                        auxDepartureArrival = New PromotionalShopping.classDepartureArrival

                                                                        If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).Departure.Airport) Then
                                                                            auxDepartureArrival.Airport = objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).Departure.Airport
                                                                        End If

                                                                        auxFecha = Format(objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).Departure.DateTime, Constantes.IWS_DATE_FORMAT_INSERT_2)
                                                                        auxDepartureArrival.DateTime = auxFecha
                                                                        auxFecha = Nothing

                                                                        auxDepartureArrival.GMTOffset = objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).Departure.GMTOffset

                                                                        auxSegmento.Departure = New PromotionalShopping.classDepartureArrival
                                                                        auxSegmento.Departure = auxDepartureArrival
                                                                    End If

                                                                    'Arrival
                                                                    If objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).Arrival IsNot Nothing Then
                                                                        auxDepartureArrival = New PromotionalShopping.classDepartureArrival

                                                                        If Not String.IsNullOrEmpty(objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).Arrival.Airport) Then
                                                                            auxDepartureArrival.Airport = objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).Arrival.Airport
                                                                        End If

                                                                        auxFecha = Format(objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).Arrival.DateTime, Constantes.IWS_DATE_FORMAT_INSERT_2)
                                                                        auxDepartureArrival.DateTime = auxFecha
                                                                        auxFecha = Nothing

                                                                        auxDepartureArrival.GMTOffset = objOTA_BestFareFinderRSSolutions.Outbound(i).Schedules(y)(z).Arrival.GMTOffset

                                                                        auxSegmento.Arrival = New PromotionalShopping.classDepartureArrival
                                                                        auxSegmento.Arrival = auxDepartureArrival
                                                                    End If

                                                                    If lstSegmento Is Nothing Then lstSegmento = New List(Of PromotionalShopping.classSegmento)
                                                                    lstSegmento.Add(auxSegmento)
                                                                    auxSegmento = Nothing

                                                                Next

                                                            End If

                                                            auxDetalleVuelosDisponibles = New PromotionalShopping.classDetalleVuelosDisponibles
                                                            auxDetalleVuelosDisponibles.Segmentos = New List(Of PromotionalShopping.classSegmento)
                                                            auxDetalleVuelosDisponibles.Segmentos = lstSegmento
                                                            lstSegmento = Nothing

                                                            If lstDetalleVuelosDisponibles Is Nothing Then lstDetalleVuelosDisponibles = New List(Of PromotionalShopping.classDetalleVuelosDisponibles)
                                                            lstDetalleVuelosDisponibles.Add(auxDetalleVuelosDisponibles)
                                                            auxDetalleVuelosDisponibles = Nothing
                                                        Next

                                                    End If

                                                End If

                                            Next

                                        End If

                                        If objPromotionalShopping Is Nothing Then objPromotionalShopping = New PromotionalShopping.classPromotionalShopping
                                        If objPromotionalShopping.FechasDisponibles Is Nothing Then objPromotionalShopping.FechasDisponibles = New List(Of PromotionalShopping.classFechasDisponibles)
                                        objPromotionalShopping.FechasDisponibles = lstFechasDisponibles

                                        lstFechasDisponibles = Nothing

                                        If lstDetalleVuelosDisponibles IsNot Nothing Then

                                            If objPromotionalShopping.VuelosDisponibles Is Nothing Then objPromotionalShopping.VuelosDisponibles = New PromotionalShopping.classVuelosDisponibilidad
                                            If objPromotionalShopping.VuelosDisponibles.DetalleVuelosDisponibles Is Nothing Then objPromotionalShopping.VuelosDisponibles.DetalleVuelosDisponibles = New List(Of PromotionalShopping.classDetalleVuelosDisponibles)

                                            objPromotionalShopping.VuelosDisponibles.DetalleVuelosDisponibles = lstDetalleVuelosDisponibles
                                            objPromotionalShopping.VuelosDisponibles.FechaOut = strFechaOut

                                            If objInbound IsNot Nothing Then
                                                If objPromotionalShopping.VuelosDisponibles.InBound Is Nothing Then objPromotionalShopping.VuelosDisponibles.InBound = New PromotionalShopping.classInbound
                                                objPromotionalShopping.VuelosDisponibles.InBound = objInbound
                                                objInbound = Nothing
                                            End If

                                            lstDetalleVuelosDisponibles = Nothing

                                        End If


                                    End If
                                End If
                            End If

                        End If

                    End If
                End If



            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strPseudoCityCode = Nothing
                intRequestStep = Nothing
                strDepartureCity = Nothing
                strArrivalCity = Nothing
                strCarrier = Nothing
                strRequestCode = Nothing
                strRequestType = Nothing
                strOutboundDateTime = Nothing
                strInboundDateTime = Nothing
                intDateRange = Nothing
                strFareBasisCode = Nothing
                strFareAmount = Nothing
                strFareCurrency = Nothing
                strPassengerType = Nothing
                strPassengerCount = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
            End Try

            Return objPromotionalShopping

        End Function
        Public Function InvoiceItinerary(ByVal strCodigoSeguimiento As String, _
                                         ByVal intGDS As Integer, _
                                         ByVal intFirmaGDS As Integer, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal objSession As classSession) As String

            Dim strRespuesta As objSabreWS.InvoiceItinerary.InvoiceItineraryRS = Nothing

            Try

                objIWebServices = New IWebServices
                strRespuesta = objIWebServices._InvoiceItinerary(strCodigoSeguimiento, _
                                                                 intGDS, _
                                                                 intFirmaGDS, _
                                                                 intFirmaDB, _
                                                                 objSession)



            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally

                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
            End Try

            Return ""

        End Function
        Private Sub BuscarTarifas(ByVal ThreadFare As Object)
            'Dim ThreadFare As classTheadFare
            Dim auxTheadFare As classTheadFare = Nothing
            Dim Session As classSession = Nothing

            Try

                If ThreadFare.ID > 0 Then
                    Session = objIWebServices._SessionCreate(ThreadFare.CodigoSeguimiento & "#" & ThreadFare.Pseudo, _
                                                             ThreadFare.GDS, _
                                                             ThreadFare.FirmaGDS, _
                                                             ThreadFare.FirmaDB)

                    ThreadFare.Session = New classSession
                    ThreadFare.Session = Session
                End If

                objIWebServices._ContextChange(ThreadFare.Pseudo, _
                                               ThreadFare.CodigoSeguimiento & "#" & ThreadFare.Pseudo, _
                                               ThreadFare.GDS, _
                                               ThreadFare.FirmaGDS, _
                                               ThreadFare.FirmaDB, _
                                               ThreadFare.Session)

                auxTheadFare = New classTheadFare
                auxTheadFare.ID = ThreadFare.ID
                auxTheadFare.Pseudo = ThreadFare.Pseudo
                auxTheadFare.FareRS = objIWebServices._Fare(ThreadFare.Origen, _
                                                              ThreadFare.Destino, _
                                                              ThreadFare.FechaSalida, _
                                                              ThreadFare.FechaRetorno, _
                                                              ThreadFare.Pseudo, _
                                                              ThreadFare.Orientacion, _
                                                              ThreadFare.Aerolinea, _
                                                              ThreadFare.TipoTarifa, _
                                                              ThreadFare.TipoPasajero, _
                                                              ThreadFare.CodigoSeguimiento & "#" & ThreadFare.Pseudo, _
                                                               ThreadFare.GDS, _
                                                               ThreadFare.FirmaGDS, _
                                                               ThreadFare.FirmaDB, _
                                                               ThreadFare.Session)

                objIWebServices._IgnoreTransaction(ThreadFare.CodigoSeguimiento & "#" & ThreadFare.Pseudo, _
                                                   ThreadFare.GDS, _
                                                   ThreadFare.FirmaGDS, _
                                                   ThreadFare.FirmaDB, _
                                                   ThreadFare.Session)

                lstTheadFare.Add(auxTheadFare)

                If ThreadFare.ID > 0 Then
                    objIWebServices._SessionClose(ThreadFare.CodigoSeguimiento & "#" & ThreadFare.Pseudo, _
                                                   ThreadFare.GDS, _
                                                   ThreadFare.FirmaGDS, _
                                                   ThreadFare.FirmaDB, _
                                                   ThreadFare.Session)
                End If



            Catch ex As Exception
                Throw New Exception(ex.ToString)
            End Try
        End Sub
        Public Function Fare(ByVal strDK As String, _
                             ByVal strPNR As String, _
                             ByVal strOrigen As String, _
                             ByVal strDestino As String, _
                             ByVal strFechaSalida As String, _
                             ByVal strFechaRetorno As String, _
                             ByVal strPseudoConsulta As String, _
                             ByVal strOrientacion As String, _
                             ByVal strAerolinea As String, _
                             ByVal strTipoTarifa As String, _
                             ByVal strTipoPasajero As String, _
                             ByVal strCodigoSeguimiento As String, _
                             ByVal intGDS As Integer, _
                             ByVal intFirmaGDS As Integer, _
                             ByVal intFirmaDB As Integer, _
                             ByVal intEsquema As Integer, _
                             ByVal objSession As classSession) As classFQ.classFare

            Dim objFare As objSabreWS.Fare.FareRS = Nothing
            Dim objFQ As List(Of classFQ.classFareRS) = Nothing
            Dim objAuxFare As classFQ.classFareRS = Nothing
            Dim objPseudos As classFQ.classPseudoFare = Nothing
            Dim objAuxTipoPasajero As classDatosTipoPasajero = Nothing
            Dim objTipoPaxVuelo As classTipoPaxVuelo = Nothing
            Dim objRespuesta As classFQ.classFare = Nothing
            Dim lstRespuesta As List(Of classFQ.classFare) = Nothing
            '
            'Dim strCadenaPseudos As String = "QF05/QP75/HW57/QQ05"
            Dim strCadenaReglas As String = String.Empty

            Dim ListaThread As List(Of classThreadNM) = Nothing
            Dim xListaThread As classThreadNM = Nothing
            Dim ThreadFare As classTheadFare = Nothing

            Dim PseudosTarifasNegociadas As classPseudosTarifasNegociadas = Nothing
            Dim Pseudos As List(Of String) = Nothing
            Dim objModuloComercial As classFeeTarifaBulkResultado() = Nothing


            Try

                objIWebServices = New IWebServices

                If Not String.IsNullOrEmpty(strTipoTarifa) Then
                    If strTipoTarifa.Equals("PL") Then
                        If PseudosTarifasNegociadas Is Nothing Then PseudosTarifasNegociadas = New classPseudosTarifasNegociadas
                        Pseudos = New List(Of String)
                        Pseudos.Add(strPseudoConsulta)
                        PseudosTarifasNegociadas.Pseudos = Pseudos
                    ElseIf strTipoTarifa.Equals("PV") Then
                        PseudosTarifasNegociadas = ObtenerPseudosTarifasNegociadas(strPNR, strDK, strAerolinea, strPseudoConsulta, _
                                                                                   strOrientacion.Substring(1), strOrigen, strDestino, _
                                                                                   strFechaSalida, strFechaRetorno, _
                                                                                   strCodigoSeguimiento, intFirmaDB, intEsquema)
                    End If

                    '***** HILOS ******

                    If PseudosTarifasNegociadas IsNot Nothing Then
                        If PseudosTarifasNegociadas.Pseudos IsNot Nothing Then

                            For h As Integer = 0 To PseudosTarifasNegociadas.Pseudos.Count - 1 'strCadenaPseudos.Split(Constantes.Slash).Length - 1

                                ThreadFare = New classTheadFare
                                ThreadFare.Pseudo = PseudosTarifasNegociadas.Pseudos.Item(h).ToString 'strCadenaPseudos.Split(Constantes.Slash)(h)
                                ThreadFare.ID = h
                                If h = 0 Then
                                    ThreadFare.Session = objSession
                                End If

                                ThreadFare.GDS = intFirmaDB

                                ThreadFare.Origen = strOrigen
                                ThreadFare.Destino = strDestino
                                ThreadFare.FechaSalida = strFechaSalida
                                ThreadFare.FechaRetorno = strFechaRetorno
                                ThreadFare.Orientacion = strOrientacion
                                ThreadFare.Aerolinea = strAerolinea
                                ThreadFare.TipoTarifa = strTipoTarifa
                                ThreadFare.TipoPasajero = strTipoPasajero
                                ThreadFare.CodigoSeguimiento = strCodigoSeguimiento


                                xListaThread = New classThreadNM
                                xListaThread.ThreadNM = New Threading.Thread(AddressOf BuscarTarifas)
                                xListaThread.ID = h
                                xListaThread.PCC = PseudosTarifasNegociadas.Pseudos.Item(h).ToString 'strCadenaPseudos.Split(Constantes.Slash)(h)
                                xListaThread.ThreadNM.IsBackground = True
                                xListaThread.ThreadNM.Start(ThreadFare)
                                'Threading.Thread.Sleep(100)

                                If ListaThread Is Nothing Then ListaThread = New List(Of classThreadNM)
                                ListaThread.Add(xListaThread)

                            Next

                            Dim bolThread As Boolean = False
                            Dim intContador As Integer = 0
                            While bolThread = False
                                For i As Integer = 0 To ListaThread.Count - 1
                                    If Not ListaThread.Item(i).ThreadNM.IsAlive Then
                                        ListaThread.Item(i).ThreadNM.Abort()
                                        intContador += 1
                                    End If
                                Next

                                If intContador = ListaThread.Count Then
                                    bolThread = True
                                End If
                            End While

                            '****************

                            For h As Integer = 0 To lstTheadFare.Count - 1

                                objFare = lstTheadFare.Item(h).FareRS

                                If objFare IsNot Nothing Then
                                    objRespuesta = New classFQ.classFare

                                    If objFare.ApplicationResults IsNot Nothing Then
                                        If objFare.ApplicationResults.Success IsNot Nothing Then
                                            For i As Integer = 0 To objFare.ApplicationResults.Success.Length - 1
                                                For x As Integer = 0 To objFare.ApplicationResults.Success(i).SystemSpecificResults.Length - 1
                                                    objRespuesta.HostCommand = objFare.ApplicationResults.Success(i).SystemSpecificResults(x).HostCommand.Value
                                                Next
                                            Next
                                        End If


                                        If objFare.ApplicationResults.Error IsNot Nothing Then
                                            objRespuesta.ErroresAlertas = New classErroresAlertas
                                            objRespuesta.ErroresAlertas.Errores = New List(Of String)

                                            For i As Integer = 0 To objFare.ApplicationResults.Error.Length - 1
                                                If objFare.ApplicationResults.Error(i).SystemSpecificResults IsNot Nothing Then
                                                    For y As Integer = 0 To objFare.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                                        If objFare.ApplicationResults.Error(i).SystemSpecificResults(y).Message IsNot Nothing Then
                                                            For z As Integer = 0 To objFare.ApplicationResults.Error(i).SystemSpecificResults(y).Message.Length - 1
                                                                If objFare.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value IsNot Nothing Then

                                                                    objRespuesta.ErroresAlertas.Errores.Add(objFare.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value)

                                                                End If
                                                            Next
                                                        End If
                                                    Next
                                                End If
                                            Next
                                        End If


                                        If objFare.ApplicationResults.Warning IsNot Nothing Then
                                            objRespuesta.ErroresAlertas = New classErroresAlertas
                                            objRespuesta.ErroresAlertas.Alertas = New List(Of String)

                                            For i As Integer = 0 To objFare.ApplicationResults.Error.Length - 1
                                                If objFare.ApplicationResults.Error(i).SystemSpecificResults IsNot Nothing Then
                                                    For y As Integer = 0 To objFare.ApplicationResults.Error(i).SystemSpecificResults.Length - 1
                                                        If objFare.ApplicationResults.Error(i).SystemSpecificResults(y).Message IsNot Nothing Then
                                                            For z As Integer = 0 To objFare.ApplicationResults.Error(i).SystemSpecificResults(y).Message.Length - 1
                                                                If objFare.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value IsNot Nothing Then

                                                                    objRespuesta.ErroresAlertas.Alertas.Add(objFare.ApplicationResults.Error(i).SystemSpecificResults(y).Message(z).Value)

                                                                End If
                                                            Next
                                                        End If
                                                    Next
                                                End If
                                            Next
                                        End If
                                    End If

                                    'If objFare.FareBasis IsNot Nothing Then
                                    '    objRespuesta = HilosTarifasBulk(strDK, _
                                    '                                    strPNR, _
                                    '                                    strOrigen, _
                                    '                                    strDestino, _
                                    '                                    strFechaSalida, _
                                    '                                    strFechaRetorno, _
                                    '                                    strPseudoConsulta, _
                                    '                                    strOrientacion, _
                                    '                                    strCodigoSeguimiento, _
                                    '                                    oGDS, _
                                    '                                    objFare, _
                                    '                                    h, _
                                    '                                    "PL") ' strTipoTarifa

                                    '    If lstRespuesta Is Nothing Then lstRespuesta = New List(Of classFQ.classFare)
                                    '    lstRespuesta.Add(objRespuesta)
                                    'End If


                                    If objFare.FareBasis IsNot Nothing Then
                                        objRespuesta = HilosTarifasBulk(strDK, _
                                                                        strPNR, _
                                                                        strOrigen, _
                                                                        strDestino, _
                                                                        strFechaSalida, _
                                                                        strFechaRetorno, _
                                                                        strPseudoConsulta, _
                                                                        strOrientacion, _
                                                                        strCodigoSeguimiento, _
                                                                        intFirmaDB, _
                                                                        intEsquema,
                                                                        objFare, _
                                                                        h, _
                                                                        strTipoTarifa) ' strTipoTarifa

                                        If lstRespuesta Is Nothing Then lstRespuesta = New List(Of classFQ.classFare)
                                        lstRespuesta.Add(objRespuesta)
                                    End If



                                    'If objFare.FareBasis IsNot Nothing Then
                                    '    Dim strCadenaAerolineas As String = String.Empty
                                    '    For i As Integer = 0 To objFare.FareBasis.Length - 1
                                    '        objAuxFare = New classFQ.classFareRS


                                    '        objAuxFare.ID = i

                                    '        objAuxFare.DK = strDK

                                    '        objAuxFare.PNR = strPNR

                                    'Airline
                                    'If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                                    '    If objFare.FareBasis(i).AdditionalInformation.Airline IsNot Nothing Then
                                    '        objAuxFare.AirLines = objFare.FareBasis(i).AdditionalInformation.Airline.Code

                                    '        If Not strCadenaAerolineas.Contains(objFare.FareBasis(i).AdditionalInformation.Airline.Code) Then
                                    '            strCadenaAerolineas &= IIf(String.IsNullOrEmpty(strCadenaAerolineas), "", "\") & objFare.FareBasis(i).AdditionalInformation.Airline.Code
                                    '        End If

                                    '    End If
                                    'End If

                                    ''PassengerType
                                    'If objFare.FareBasis(i).PassengerType IsNot Nothing Then
                                    '    For x As Integer = 0 To objFare.FareBasis(i).PassengerType.Length - 1
                                    '        objAuxTipoPasajero = New classDatosTipoPasajero
                                    '        objTipoPaxVuelo = New classTipoPaxVuelo
                                    '        objTipoPaxVuelo = ObtenerTipoPaxEspecial(objFare.FareBasis(i).PassengerType(x).Code, strCodigoSeguimiento, intFirmaDB, intEsquema)

                                    '        If objTipoPaxVuelo IsNot Nothing Then
                                    '            objAuxTipoPasajero.ID = objTipoPaxVuelo.IdTipoDePax
                                    '            If Not String.IsNullOrEmpty(objTipoPaxVuelo.Equivale) Then
                                    '                objAuxTipoPasajero.Equivale = objTipoPaxVuelo.Equivale
                                    '            Else
                                    '                GoTo SIGUIENTE
                                    '            End If

                                    '            If Not String.IsNullOrEmpty(objTipoPaxVuelo.Pertenece) Then
                                    '                objAuxTipoPasajero.Pertenece = objTipoPaxVuelo.Pertenece
                                    '            Else
                                    '                GoTo SIGUIENTE
                                    '            End If

                                    '        Else
                                    '            GoTo SIGUIENTE
                                    '            objAuxTipoPasajero.ID = objFare.FareBasis(i).PassengerType(x).Code
                                    '        End If

                                    '        If objAuxFare.PassengerType Is Nothing Then objAuxFare.PassengerType = New List(Of classDatosTipoPasajero)
                                    '        objAuxFare.PassengerType.Add(objAuxTipoPasajero)
                                    '    Next
                                    'End If
                                    'objAuxTipoPasajero = Nothing

                                    ''FareBasis
                                    'objAuxFare.FareBasis = objFare.FareBasis(i).Code

                                    ''Cabin
                                    'If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                                    '    objAuxFare.Cabin = objFare.FareBasis(i).AdditionalInformation.Cabin
                                    'End If

                                    ''BookingClass
                                    'If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                                    '    objAuxFare.BookingClass = objFare.FareBasis(i).AdditionalInformation.ResBookDesigCode
                                    'End If

                                    ''Currency
                                    'If objFare.FareBasis(i).BaseFare IsNot Nothing Then
                                    '    objAuxFare.Currency = objFare.FareBasis(i).BaseFare.CurrencyCode
                                    'End If


                                    'If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                                    '    If strOrientacion = "RT" Then
                                    '        If objFare.FareBasis(i).AdditionalInformation.OneWayRoundTrip(0).Ind = "R" Or _
                                    '           objFare.FareBasis(i).AdditionalInformation.OneWayRoundTrip(0).Ind = "X" Then
                                    '            If objFare.FareBasis(i).AdditionalInformation.Fare IsNot Nothing Then
                                    '                'If objFare.FareBasis(i).AdditionalInformation.Fare.Length > 1 Then
                                    '                'BaseFare
                                    '                objAuxFare.BaseFare = objFare.FareBasis(i).AdditionalInformation.Fare(0).Amount
                                    '                'End If
                                    '            End If
                                    '        End If
                                    '    Else
                                    '        If objFare.FareBasis(i).BaseFare IsNot Nothing Then
                                    '            'BaseFare
                                    '            objAuxFare.BaseFare = objFare.FareBasis(i).BaseFare.Amount
                                    '        End If
                                    '    End If
                                    'End If

                                    ''ExpirationDate
                                    'If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                                    '    objAuxFare.ExpirationDate = objFare.FareBasis(i).AdditionalInformation.ExpirationDate
                                    '    If objAuxFare.ExpirationDate.Contains("12:00:00") Then objAuxFare.ExpirationDate = String.Empty
                                    'End If

                                    ''EffectiveDate
                                    'If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                                    '    objAuxFare.EffectiveDate = objFare.FareBasis(i).AdditionalInformation.EffectiveDate
                                    '    If objAuxFare.EffectiveDate.Contains("12:00:00") Then objAuxFare.EffectiveDate = String.Empty
                                    'End If

                                    ''TicketDate
                                    'If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                                    '    objAuxFare.TicketDate = objFare.FareBasis(i).AdditionalInformation.TicketDate
                                    '    If objAuxFare.TicketDate.Contains("12:00:00") Then objAuxFare.TicketDate = String.Empty
                                    'End If

                                    ''AdvancePurchase
                                    'If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                                    '    objAuxFare.AdvancePurchase = objFare.FareBasis(i).AdditionalInformation.AdvancePurchase
                                    'End If

                                    ''MinStay
                                    'If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                                    '    objAuxFare.MinStay = objFare.FareBasis(i).AdditionalInformation.MinStay
                                    'End If

                                    ''MaxStay
                                    'If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                                    '    objAuxFare.MaxStay = objFare.FareBasis(i).AdditionalInformation.MaxStay
                                    'End If

                                    ''MaxStay
                                    'If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                                    '    objAuxFare.MaxStay = objFare.FareBasis(i).AdditionalInformation.MaxStay
                                    'End If

                                    ''Pseudo
                                    'If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                                    '    objPseudos = New classFQ.classPseudoFare
                                    '    objPseudos.Pseudo = lstTheadFare.Item(h).Pseudo 'strCadenaPseudos.Split(Constantes.Slash)(h)
                                    '    objPseudos.IDs = h

                                    '    objAuxFare.Pseudos = New List(Of classFQ.classPseudoFare)
                                    '    objAuxFare.Pseudos.Add(objPseudos)
                                    'End If

                                    'objAuxFare.tipoTarifa = strTipoTarifa
                                    ''SeasonalApplication
                                    'If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                                    '    If objFare.FareBasis(i).AdditionalInformation.SeasonalApplication IsNot Nothing Then
                                    '        objAuxFare.SeasonalApplication = objFare.FareBasis(i).AdditionalInformation.SeasonalApplication(0)
                                    '    End If
                                    'End If

                                    ''RuleCategory
                                    'If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                                    '    If objFare.FareBasis(i).AdditionalInformation.Rule IsNot Nothing Then
                                    '        strCadenaReglas = String.Empty
                                    '        For x As Integer = 0 To objFare.FareBasis(i).AdditionalInformation.Rule.Length - 1
                                    '            strCadenaReglas &= IIf(String.IsNullOrEmpty(strCadenaReglas), "", "/") & objFare.FareBasis(i).AdditionalInformation.Rule(x)
                                    '            'If objAuxFare.RuleCategory Is Nothing Then objAuxFare.RuleCategory = New List(Of String)
                                    '            'objAuxFare.RuleCategory.Add(objFare.FareBasis(i).AdditionalInformation.Rule(x).ToString)
                                    '        Next
                                    'If objAuxFare.RuleCategory Is Nothing Then objAuxFare.RuleCategory = New List(Of String)
                                    '        objAuxFare.RuleCategory.Add(strCadenaReglas)
                                    '    End If
                                    'End If


                                    '**************************************************************************
                                    '*****            EVALUAMOS REGLAS DE FEE                              ****
                                    '**************************************************************************

                                    'objModuloComercial = ObtenerReglasTarifasNegociadas(objAuxFare, _
                                    '                                                    strPseudoConsulta, _
                                    '                                                    strOrientacion, _
                                    '                                                    strOrigen, _
                                    '                                                    strDestino, _
                                    '                                                    strFechaSalida, _
                                    '                                                    strFechaRetorno, _
                                    '                                                    strCodigoSeguimiento, _
                                    '                                                    intGDS,
                                    '                                                    intFirmaDB,
                                    '                                                    intEsquema)

                                    '**************************************************************************

                                    'If objModuloComercial IsNot Nothing And strTipoTarifa = "PV" Then

                                    '    If objModuloComercial(0).Fee_Minimo >= 0 Then
                                    '        If objModuloComercial(0).Fee_Maximo >= 0 Then

                                    '            If objModuloComercial(0).Es_porcentaje = 0 Then
                                    '                objAuxFare.FeeMinino = objModuloComercial(0).Fee_Minimo
                                    '                objAuxFare.FeeMaximo = objModuloComercial(0).Fee_Maximo
                                    '            Else
                                    '                objAuxFare.FeeMinino = (objModuloComercial(0).Fee_Minimo / 100) * objAuxFare.BaseFare
                                    '                objAuxFare.FeeMaximo = (objModuloComercial(0).Fee_Maximo / 100) * objAuxFare.BaseFare
                                    '            End If
                                    '       End If
                                    '   End If

                                    'End If

                                    '                                    If objFQ Is Nothing Then objFQ = New List(Of classFQ.classFareRS)
                                    '                                    objFQ.Add(objAuxFare)
                                    '                                    objAuxFare = Nothing
                                    'SIGUIENTE:

                                    '                                        Next

                                    '                            objRespuesta.FareRS = objFQ
                                    '                            If lstRespuesta Is Nothing Then lstRespuesta = New List(Of classFQ.classFare)
                                    '                            lstRespuesta.Add(objRespuesta)

                                    '                        End If

                                    If objFare.YY_FareMessage IsNot Nothing Then

                                        objRespuesta.ErroresAlertas = New classErroresAlertas
                                        objRespuesta.ErroresAlertas.Alertas = New List(Of String)
                                        Dim Alerta As New List(Of String)

                                        For i As Integer = 0 To objFare.YY_FareMessage.Length - 1
                                            For x As Integer = 0 To objFare.YY_FareMessage(i).Text.Length - 1
                                                objRespuesta.ErroresAlertas.Alertas.Add(objFare.YY_FareMessage(i).Text(x).ToString)
                                            Next
                                        Next

                                    End If
                                End If

                            Next

                            'Cargar los datos insertados en la tabla 

                            If lstRespuesta IsNot Nothing Then
                                For i As Integer = 0 To lstRespuesta.Count - 1
                                    If lstRespuesta.Item(i).FareRS IsNot Nothing Then
                                        InsertaTarifasFQ(lstRespuesta.Item(i).FareRS, strCodigoSeguimiento, intFirmaDB, intEsquema)
                                    End If
                                Next

                                objRespuesta.FareRS = New List(Of classFQ.classFareRS)
                                objRespuesta.FareRS = ObtenerTarifasFQ(strPNR, strDK, strCodigoSeguimiento, intFirmaDB, intEsquema)
                            End If


                        End If
                    End If

                End If
            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strOrigen = Nothing
                strDestino = Nothing
                strFechaSalida = Nothing
                strFechaRetorno = Nothing
                strPseudoConsulta = Nothing
                strOrientacion = Nothing
                strAerolinea = Nothing
                strTipoTarifa = Nothing
                strTipoPasajero = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objRespuesta

        End Function

        Public Function TravelItineraryRead(ByVal strPNR As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intGDS As Integer, _
                                             ByVal intFirmaGDS As Integer, _
                                             ByVal intFirmaDB As Integer, _
                                            ByVal objSession As classSession) As objSabreWS.TravelItineraryReadRQ.TravelItineraryReadRS

            Dim TravelItineraryRead330 As objSabreWS.TravelItineraryReadRQ.TravelItineraryReadRS = Nothing

            Try

                objIWebServices = New IWebServices
                TravelItineraryRead330 = objIWebServices._TravelItineraryReadRQ(strPNR, _
                                                                                strCodigoSeguimiento, _
                                                                                intGDS, _
                                                                                intFirmaGDS, _
                                                                                intFirmaDB, _
                                                                                objSession)


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strPNR = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing

                objSession = Nothing

            End Try

            Return TravelItineraryRead330

        End Function
        Public Function OTA_AirBook(ByVal lstSegmentos As List(Of classSegmentos), _
                                    ByVal intCantidadPasajeros As Integer, _
                                    ByVal strCodigoSeguimiento As String, _
                                    ByVal intGDS As Integer, _
                                    ByVal intFirmaGDS As Integer, _
                                    ByVal intFirmaDB As Integer, _
                                    ByVal objSession As classSession) As List(Of classSegmentos)

            Dim OTA_AirBookRS As objSabreWS.OTA_AirBookLLS200.OTA_AirBookRS = Nothing
            Dim lstRespuestaSegmentos As List(Of classSegmentos) = Nothing
            Dim AuxSegmentos As classSegmentos = Nothing
            Try

                objIWebServices = New IWebServices
                OTA_AirBookRS = objIWebServices._OTA_AirBook(lstSegmentos, _
                                                             intCantidadPasajeros, _
                                                             strCodigoSeguimiento, _
                                                             intGDS, _
                                                             intFirmaGDS, _
                                                             intFirmaDB, _
                                                             objSession)


                If OTA_AirBookRS.OriginDestinationOption IsNot Nothing Then

                    For i As Integer = 0 To OTA_AirBookRS.OriginDestinationOption.Length - 1
                        AuxSegmentos = New classSegmentos

                        If OTA_AirBookRS.OriginDestinationOption(i).MarketingAirline IsNot Nothing Then

                            If OTA_AirBookRS.OriginDestinationOption(i).MarketingAirline.Code IsNot Nothing Then
                                AuxSegmentos.Aerolinea = OTA_AirBookRS.OriginDestinationOption(i).MarketingAirline.Code
                            End If

                            If OTA_AirBookRS.OriginDestinationOption(i).MarketingAirline.FlightNumber IsNot Nothing Then
                                AuxSegmentos.NumVuelo = OTA_AirBookRS.OriginDestinationOption(i).MarketingAirline.FlightNumber
                            End If

                            If OTA_AirBookRS.OriginDestinationOption(i).ResBookDesigCode IsNot Nothing Then
                                AuxSegmentos.ClaseServicio = OTA_AirBookRS.OriginDestinationOption(i).ResBookDesigCode
                            End If

                            If Not String.IsNullOrEmpty(OTA_AirBookRS.OriginDestinationOption(i).DepartureDateTime) Then
                                AuxSegmentos.FechaHoraSalida = OTA_AirBookRS.OriginDestinationOption(i).DepartureDateTime
                            End If

                            If Not String.IsNullOrEmpty(OTA_AirBookRS.OriginDestinationOption(i).ArrivalDateTime) Then
                                AuxSegmentos.FechaHoraLlegada = OTA_AirBookRS.OriginDestinationOption(i).ArrivalDateTime
                            End If

                            If OTA_AirBookRS.OriginDestinationOption(i).OriginLocation IsNot Nothing Then
                                AuxSegmentos.Salida = New classCiudad
                                AuxSegmentos.Salida.Codigo = OTA_AirBookRS.OriginDestinationOption(i).OriginLocation.LocationCode
                            End If

                            If OTA_AirBookRS.OriginDestinationOption(i).DestinationLocation IsNot Nothing Then
                                AuxSegmentos.Llegada = New classCiudad
                                AuxSegmentos.Llegada.Codigo = OTA_AirBookRS.OriginDestinationOption(i).DestinationLocation.LocationCode
                            End If

                            If Not String.IsNullOrEmpty(OTA_AirBookRS.OriginDestinationOption(i).Status) Then
                                AuxSegmentos.Status = OTA_AirBookRS.OriginDestinationOption(i).Status
                            End If

                        End If

                        If lstRespuestaSegmentos Is Nothing Then lstRespuestaSegmentos = New List(Of classSegmentos)
                        lstRespuestaSegmentos.Add(AuxSegmentos)

                    Next

                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
                AuxSegmentos = Nothing
            End Try

            Return lstRespuestaSegmentos

        End Function
        Public Function SWS_AutomatedExchanges(ByVal strCodigoAerolinea As String,
                                               ByVal strNumeroParajero As String,
                                               ByVal strTicketOriginal As String,
                                               ByVal lstSegmentos As List(Of String),
                                               ByVal strTourCode As String,
                                               ByVal strAccount As String,
                                               ByVal strCorporateId As String,
                                               ByVal strTipoTarifa As String,
                                               ByVal strPassengerType As String,
                                               ByVal strCodigoSeguimiento As String,
                                               ByVal intGDS As Integer,
                                               ByVal intFirmaGDS As Integer,
                                               ByVal intFirmaDB As Integer,
                                               ByVal objSession As classSession) As String
            Try
                objIWebServices = New IWebServices
                objIWebServices._AutomatedExchanges(strCodigoAerolinea,
                                                    strNumeroParajero,
                                                    strTicketOriginal,
                                                    lstSegmentos,
                                                    strTourCode,
                                                    strAccount,
                                                    strCorporateId,
                                                    strTipoTarifa,
                                                    strPassengerType,
                                                    strCodigoSeguimiento,
                                                    intGDS,
                                                    intFirmaGDS,
                                                    intFirmaDB,
                                                    objSession)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoAerolinea = Nothing
                strNumeroParajero = Nothing
                strTicketOriginal = Nothing
                strTourCode = Nothing
                strAccount = Nothing
                strCorporateId = Nothing
                strTipoTarifa = Nothing
                strPassengerType = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try
            Return ""
        End Function
        Public Function SWS_ExchangeConfirmation(ByVal strPQ As String, _
                                                 ByVal strBaggage As String, _
                                                 ByVal strCommission As String, _
                                                 ByVal objFormaPago As classFormaPago, _
                                                 ByVal strCodigoSeguimiento As String,
                                                 ByVal intGDS As Integer,
                                                 ByVal intFirmaGDS As Integer,
                                                 ByVal intFirmaDB As Integer,
                                                 ByVal objSession As classSession) As String
            Try
                objIWebServices = New IWebServices
                objIWebServices._ExchangeConfirmation(strPQ,
                                                      strBaggage,
                                                      strCommission,
                                                      objFormaPago,
                                                      strCodigoSeguimiento,
                                                      intGDS,
                                                      intFirmaGDS,
                                                      intFirmaDB,
                                                      objSession)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strPQ = Nothing
                strBaggage = Nothing
                strCommission = Nothing
                objFormaPago = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try
            Return ""
        End Function
        Public Function SWS_DisplayPriceQuote(ByVal strCodigoSeguimiento As String,
                                              ByVal intGDS As Integer,
                                              ByVal intFirmaGDS As Integer,
                                              ByVal intFirmaDB As Integer,
                                              ByVal intEsquema As Integer,
                                              ByVal objSession As classSession) As classTarifaRetenida

            Dim DisplayPriceQuoteRS As objSabreWS.DisplayPriceQuote.DisplayPriceQuoteRS = Nothing
            Dim objTarifaRetenida As classTarifaRetenida = Nothing
            Dim auxSegmentos As classSegmentos = Nothing
            Dim lstSegmento As List(Of classSegmentos) = Nothing
            Dim intContadorSegmento As Integer = 0
            Dim objDAO As DAO = Nothing
            Dim objCiudad As classCiudad = Nothing

            Dim intAnioActual As Integer = 0
            Dim oDate As Date = Nothing
            Dim oMesDeparture As String = Nothing
            Dim oAnoDeparture As String = Nothing

            Try
                objIWebServices = New IWebServices
                DisplayPriceQuoteRS = objIWebServices._DisplayPriceQuote(strCodigoSeguimiento,
                                                                         intGDS,
                                                                         intFirmaGDS,
                                                                         intFirmaDB,
                                                                         objSession)



                If DisplayPriceQuoteRS IsNot Nothing Then
                    objTarifaRetenida = New classTarifaRetenida

                    If DisplayPriceQuoteRS.ApplicationResults IsNot Nothing Then
                        If DisplayPriceQuoteRS.ApplicationResults.Error IsNot Nothing Then
                            objTarifaRetenida.ErroresAlertas = New classErroresAlertas

                            For a As Integer = 0 To DisplayPriceQuoteRS.ApplicationResults.Error.Length - 1
                                If DisplayPriceQuoteRS.ApplicationResults.Error(a).SystemSpecificResults IsNot Nothing Then
                                    For b As Integer = 0 To DisplayPriceQuoteRS.ApplicationResults.Error(a).SystemSpecificResults.Length - 1
                                        If DisplayPriceQuoteRS.ApplicationResults.Error(a).SystemSpecificResults(b).Message IsNot Nothing Then
                                            For c As Integer = 0 To DisplayPriceQuoteRS.ApplicationResults.Error(a).SystemSpecificResults(b).Message.Length - 1
                                                If objTarifaRetenida.ErroresAlertas.Errores Is Nothing Then objTarifaRetenida.ErroresAlertas.Errores = New List(Of String)
                                                objTarifaRetenida.ErroresAlertas.Errores.Add(DisplayPriceQuoteRS.ApplicationResults.Error(a).SystemSpecificResults(b).Message(c).Value)
                                            Next
                                        End If
                                    Next
                                End If
                            Next

                        ElseIf DisplayPriceQuoteRS.ApplicationResults.Warning IsNot Nothing Then

                        ElseIf DisplayPriceQuoteRS.PriceQuoteReissue IsNot Nothing Then



                            objTarifaRetenida.PQ = DisplayPriceQuoteRS.PriceQuoteReissue(DisplayPriceQuoteRS.PriceQuoteReissue.Length - 1).RPH

                            If DisplayPriceQuoteRS.PriceQuoteReissue(DisplayPriceQuoteRS.PriceQuoteReissue.Length - 1).PricedItinerary IsNot Nothing Then
                                If DisplayPriceQuoteRS.PriceQuoteReissue(DisplayPriceQuoteRS.PriceQuoteReissue.Length - 1).PricedItinerary.AirItineraryPricingInfo IsNot Nothing Then
                                    If DisplayPriceQuoteRS.PriceQuoteReissue(DisplayPriceQuoteRS.PriceQuoteReissue.Length - 1).PricedItinerary.AirItineraryPricingInfo.PTC_FareBreakdown IsNot Nothing Then

                                        With DisplayPriceQuoteRS.PriceQuoteReissue(DisplayPriceQuoteRS.PriceQuoteReissue.Length - 1).PricedItinerary.AirItineraryPricingInfo

                                            For i As Integer = 0 To .PTC_FareBreakdown.Length - 1
                                                If Not String.IsNullOrEmpty(.PTC_FareBreakdown(i).RPH) Then

                                                    auxSegmentos = New classSegmentos

                                                    auxSegmentos.RPH = .PTC_FareBreakdown(i).RPH
                                                    auxSegmentos.Segmento = intContadorSegmento

                                                    If Not String.IsNullOrEmpty(.PTC_FareBreakdown(i).MarketingAirline.Code) Then
                                                        auxSegmentos.Aerolinea = .PTC_FareBreakdown(i).MarketingAirline.Code
                                                    End If

                                                    If Not String.IsNullOrEmpty(.PTC_FareBreakdown(i).FlightNumber) Then
                                                        auxSegmentos.NumVuelo = .PTC_FareBreakdown(i).FlightNumber
                                                    End If

                                                    If Not String.IsNullOrEmpty(.PTC_FareBreakdown(i).ResBookDesigCode) Then
                                                        auxSegmentos.ClaseServicio = .PTC_FareBreakdown(i).ResBookDesigCode
                                                    End If


                                                    '2014-08-10T10:20:00
                                                    If Not String.IsNullOrEmpty(.PTC_FareBreakdown(i).DepartureDateTime) Then
                                                        auxSegmentos.FechaHoraSalida = .PTC_FareBreakdown(i).DepartureDateTime.ToString
                                                        oDate = New Date
                                                        oDate = "1900-" & .PTC_FareBreakdown(i).DepartureDateTime.Split("T")(0)
                                                        oMesDeparture = oDate.Month.ToString

                                                        If CInt(oMesDeparture) = Now.Month Or CInt(oMesDeparture) > Now.Month Then
                                                            auxSegmentos.FechaHoraSalida = Now.Year & "-" & .PTC_FareBreakdown(i).DepartureDateTime.Split("T")(0)
                                                        ElseIf CInt(oMesDeparture) < Now.Month Then
                                                            auxSegmentos.FechaHoraSalida = Now.AddYears(1).Year & "-" & .PTC_FareBreakdown(i).DepartureDateTime.Split("T")(0)
                                                        End If

                                                    End If



                                                    If .PTC_FareBreakdown(i).OriginLocation.LocationCode IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(.PTC_FareBreakdown(i).OriginLocation.LocationCode) Then

                                                            objDAO = New DAO
                                                            objCiudad = New classCiudad
                                                            objCiudad = objDAO.ObtenerDatosCiudad(.PTC_FareBreakdown(i).OriginLocation.LocationCode, _
                                                                                                           strCodigoSeguimiento, _
                                                                                                           intFirmaDB, _
                                                                                                           intEsquema)


                                                            auxSegmentos.Salida = New classCiudad
                                                            auxSegmentos.Salida = objCiudad


                                                        End If
                                                    End If


                                                    If .PTC_FareBreakdown(i).FareBasis IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(.PTC_FareBreakdown(i).FareBasis.Code) Then
                                                            auxSegmentos.FareBasis = .PTC_FareBreakdown(i).FareBasis.Code
                                                        End If
                                                    End If


                                                    If DisplayPriceQuoteRS.PriceQuoteReissue(DisplayPriceQuoteRS.PriceQuoteReissue.Length - 1).PricedItinerary.AirItineraryPricingInfo.PTC_FareBreakdown(i).BaggageAllowance IsNot Nothing Then
                                                        If Not String.IsNullOrEmpty(DisplayPriceQuoteRS.PriceQuoteReissue(DisplayPriceQuoteRS.PriceQuoteReissue.Length - 1).PricedItinerary.AirItineraryPricingInfo.PTC_FareBreakdown(i).BaggageAllowance.Number) Then
                                                            If Not objTarifaRetenida.Equipaje.Contains(DisplayPriceQuoteRS.PriceQuoteReissue(DisplayPriceQuoteRS.PriceQuoteReissue.Length - 1).PricedItinerary.AirItineraryPricingInfo.PTC_FareBreakdown(i).BaggageAllowance.Number) Then
                                                                objTarifaRetenida.Equipaje &= IIf(String.IsNullOrEmpty(objTarifaRetenida.Equipaje), "", "/") & DisplayPriceQuoteRS.PriceQuoteReissue(DisplayPriceQuoteRS.PriceQuoteReissue.Length - 1).PricedItinerary.AirItineraryPricingInfo.PTC_FareBreakdown(i).BaggageAllowance.Number
                                                            End If
                                                        End If
                                                    End If

                                                End If

                                                intContadorSegmento += 1
                                            Next

                                        End With

                                    End If
                                End If
                            End If

                        End If


                    End If
                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try

            Return objTarifaRetenida
        End Function
        Public Function SWS_ExchangeShopping(ByVal strCodigoAerolinea As String,
                                             ByVal strNumeroParajero As String,
                                             ByVal strTicketOriginal As String,
                                             ByVal lstSegmentos As List(Of String),
                                             ByVal strTourCode As String,
                                             ByVal strAccount As String,
                                             ByVal strCorporateId As String,
                                             ByVal strTipoTarifa As String,
                                             ByVal strPassengerType As String,
                                             ByVal strCodigoSeguimiento As String,
                                             ByVal intGDS As Integer,
                                             ByVal intFirmaGDS As Integer,
                                             ByVal intFirmaDB As Integer,
                                             ByVal objSession As classSession) As String
            Try
                objIWebServices = New IWebServices
                objIWebServices._ExchangeShopping(strCodigoAerolinea,
                                                  strNumeroParajero,
                                                  strTicketOriginal,
                                                  lstSegmentos,
                                                  strTourCode,
                                                  strAccount,
                                                  strCorporateId,
                                                  strTipoTarifa,
                                                  strPassengerType,
                                                  strCodigoSeguimiento,
                                                  intGDS,
                                                  intFirmaGDS,
                                                  intFirmaDB,
                                                  objSession)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoAerolinea = Nothing
                strNumeroParajero = Nothing
                strTicketOriginal = Nothing
                strTourCode = Nothing
                strAccount = Nothing
                strCorporateId = Nothing
                strTipoTarifa = Nothing
                strPassengerType = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try
            Return ""
        End Function
        '==============================================
        Public Function Finaliza_y_Recupera(ByVal strCodigoSeguimiento As String, _
                                            ByVal intGDS As Integer, _
                                            ByVal intFirmaGDS As Integer, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal objSession As classSession) As String

            Dim strResultado As String = Nothing

            Try
                strResultado = SabreCommand("ER", _
                                            "Finaliza_y_Recupera", _
                                            strCodigoSeguimiento, _
                                            intGDS, _
                                            intFirmaGDS, _
                                            intFirmaDB, _
                                            objSession)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try

            Return strResultado

        End Function
        Public Function XPG(ByVal strCodigoSeguimiento As String, _
                            ByVal intGDS As Integer, _
                            ByVal intFirmaGDS As Integer, _
                            ByVal intFirmaDB As Integer, _
                            ByVal objSession As classSession) As String

            Dim strResultado As String = Nothing

            Try


                strResultado = SabreCommand("XPG", _
                                            "XPG", _
                                             strCodigoSeguimiento, _
                                             intGDS, _
                                             intFirmaGDS, _
                                             intFirmaDB, _
                                             objSession)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try

            Return strResultado

        End Function
        Public Function BorrarRemarkInterface(ByVal NumeroLinea As Integer, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intGDS As Integer, _
                                              ByVal intFirmaGDS As Integer, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal objSession As classSession) As String

            Dim strResultado As String = Nothing

            Try


                strResultado = SabreCommand("5" & Constantes.Change & NumeroLinea, _
                                            "BorrarRemarkInterface", _
                                             strCodigoSeguimiento, _
                                             intGDS, _
                                             intFirmaGDS, _
                                             intFirmaDB, _
                                            objSession)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try

            Return strResultado

        End Function

        Public Function PAC(ByVal strCodigoSeguimiento As String, _
                            ByVal intGDS As Integer, _
                            ByVal intFirmaGDS As Integer, _
                            ByVal intFirmaDB As Integer, _
                            ByVal objSession As classSession) As List(Of classBoletoEmitido)

            Dim strRespuestaSabreComamand As String = Nothing
            Dim oPac As String = Nothing
            Dim arrBoletos() As String = Nothing
            Dim strAuxNumeroBoleto As String = Nothing
            Dim strAuxNumeroPasajero As String = Nothing

            Dim strNumeroBoleto As String = Nothing
            Dim strPrefijo As String = Nothing
            Dim strNumeroPasajero As String = Nothing

            Dim strNumeroPax1 As String = Nothing
            Dim intEnConexion As Integer = 0
            Dim strNumeroPax2 As String = Nothing

            Dim intLongitud As Integer = 0

            Dim classBoletoEmitido As classBoletoEmitido = Nothing
            Dim lstBoletoEmitido As List(Of classBoletoEmitido) = Nothing

            Try

                strRespuestaSabreComamand = SabreCommand("*PAC", _
                                                         "VerificaBoletosEmitidos", _
                                                         strCodigoSeguimiento, _
                                                         intGDS, _
                                                         intFirmaGDS, _
                                                         intFirmaDB, _
                                                         objSession)

                'ACCOUNTING DATA
                '1.  T0?9556759078/   1.04/USD  104.00/  12.79/D18.72/ONE/CA 1
                '.1VANDERGRIFF MABEL/1/D/E

                'ACCOUNTING DATA
                '1.  LA¥9556761200/  33.14/USD 3314.00/ 117.94/D596.52/ONE/CCV 
                '    IXXXXXXXXXXXX1942 1.1BARUA RAMON/2/F/E  

                If Not String.IsNullOrEmpty(strRespuestaSabreComamand) Then

                    strRespuestaSabreComamand = Trim(strRespuestaSabreComamand.Replace("ACCOUNTING DATA", ""))
                    strRespuestaSabreComamand = Trim(strRespuestaSabreComamand.Replace("/ONE/", "$"))
                    strRespuestaSabreComamand = Trim(strRespuestaSabreComamand.Replace("/E", "%"))

                    arrBoletos = strRespuestaSabreComamand.Split("%")

                    For i As Integer = 0 To arrBoletos.Length - 1

                        strPrefijo = Nothing
                        strNumeroBoleto = Nothing
                        strNumeroPax1 = Nothing
                        strNumeroPax2 = Nothing
                        strNumeroPasajero = Nothing

                        strAuxNumeroBoleto = arrBoletos(i).Split("$")(0)
                        strAuxNumeroPasajero = arrBoletos(i).Split("$")(1)

                        If Not String.IsNullOrEmpty(strAuxNumeroBoleto) Then
                            If strAuxNumeroBoleto.Trim <> "-" Then

                                strNumeroBoleto = Right(strAuxNumeroBoleto.Split("/")(0), 10)
                                strPrefijo = Mid(strAuxNumeroBoleto.Split("/")(0), Len(strAuxNumeroBoleto.Split("/")(0)) - 13, 3)

                                If Not String.IsNullOrEmpty(strAuxNumeroPasajero) Then

                                    classBoletoEmitido = New classBoletoEmitido

                                    strNumeroPasajero = strAuxNumeroPasajero.Split("/")(0)

                                    strNumeroPax2 = strNumeroPasajero.Split(".")(1)

                                    intLongitud = strNumeroPasajero.Split(".")(1).Length
                                    For x As Integer = 0 To intLongitud - 1
                                        strNumeroPax1 = Mid(strNumeroPasajero.Split(".")(0), intLongitud - x, 1)
                                        If IsNumeric(strNumeroPax1) Then
                                            Exit For
                                        End If
                                    Next

                                    strNumeroPasajero = strNumeroPax1 & "." & strNumeroPax2

                                    intEnConexion = strAuxNumeroPasajero.Split("/")(1)

                                    classBoletoEmitido.IDSesuenciaPax = strNumeroPax2
                                    classBoletoEmitido.PrefijoBoleto = ""
                                    classBoletoEmitido.NumeroPasajero = strNumeroPasajero
                                    classBoletoEmitido.NumeroBoleto = strNumeroBoleto
                                    classBoletoEmitido.CodigoAerolinea = strPrefijo
                                    classBoletoEmitido.EnConexion = intEnConexion

                                    If lstBoletoEmitido Is Nothing Then lstBoletoEmitido = New List(Of classBoletoEmitido)
                                    lstBoletoEmitido.Add(classBoletoEmitido)

                                End If
                            End If
                        End If
                    Next

                Else
                    oPac = "NO SE PROCESA EL COMANDO *PAC"
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
            End Try

            Return lstBoletoEmitido

        End Function
        'Public Function _OTA_AirLowFareSearch(ByVal strCodigoSeguimiento As String, _
        '                                            ByVal intGDS As Integer, _
        '                                            ByVal intFirmaGDS As Integer, _
        '                                            ByVal intFirmaDB As Integer, _
        '                                            ByVal objSession As classSession,
        '                                            ByVal strciudadOrigen As String, _
        '                                            ByVal strciudadDestino As String, _
        '                                            ByVal strFechaInicioViaje As String, _
        '                                            ByVal strFechaFinViaje As String, _
        '                                            ByVal strCod_Aerolinea As String, _
        '                                            ByVal strClase As String, _
        '                                            ByVal Orientacion As String, _
        '                                            ByVal oTipoPax As List(Of classTipoPaxVuelo),
        '                                            ByVal strTipoTarifa As String) As Object
        '    'ByVal oTipoPax() As Object) As Object

        '    Dim objSessionSabre As classSession = Nothing
        '    Try
        '        objIWebServices = New IWebServices
        '        Dim OTA_AirLowFareSearch As Object = Nothing
        '        OTA_AirLowFareSearch = objIWebServices.OTA_AirLowFareSearch_1_13_1(strCodigoSeguimiento,
        '                                                                intGDS, _
        '                                                                intFirmaGDS, _
        '                                                                intFirmaDB, _
        '                                                                objSession,
        '                                                                strciudadOrigen, _
        '                                                                strciudadDestino, _
        '                                                                strFechaInicioViaje, _
        '                                                                strFechaFinViaje, _
        '                                                                strCod_Aerolinea, _
        '                                                                strClase,
        '                                                                Orientacion, _
        '                                                                oTipoPax,
        '                                                                strTipoTarifa)
        '        Return OTA_AirLowFareSearch
        '    Catch ex As Exception
        '        Throw New Exception(ex.ToString)
        '    Finally
        '        strCodigoSeguimiento = Nothing
        '        intGDS = Nothing
        '        intFirmaGDS = Nothing
        '        intFirmaDB = Nothing
        '        objIWebServices = Nothing
        '    End Try

        'End Function
        Public Function OTA_AirLowFareSearch_1_13_1(ByVal strCodigoSeguimiento As String, _
                                            ByVal objSession As classSession,
                                            ByVal strciudadOrigen As String, _
                                            ByVal strciudadDestino As String, _
                                            ByVal strFechaInicioViaje As String, _
                                            ByVal strFechaFinViaje As String, _
                                            ByVal strCod_Aerolinea As String, _
                                            ByVal strClase As String, _
                                            ByVal Orientacion As String, _
                                            ByVal oTipoPax() As String,
                                            ByVal strTipoTarifa As String) As Object
            'ByVal oTipoPax() As Object) As Object

            Dim objSessionSabre As classSession = Nothing
            Try
                objIWebServices = New IWebServices

                Dim OTA_AirLowFareSearch As Object = Nothing

                OTA_AirLowFareSearch = objIWebServices.OTA_AirLowFareSearch_1_13_1(strCodigoSeguimiento,
                                                                        objSession,
                                                                        strciudadOrigen, _
                                                                        strciudadDestino, _
                                                                        strFechaInicioViaje, _
                                                                        strFechaFinViaje, _
                                                                        strCod_Aerolinea, _
                                                                        strClase,
                                                                        Orientacion, _
                                                                        oTipoPax,
                                                                        strTipoTarifa)
                Return OTA_AirLowFareSearch
            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                objIWebServices = Nothing
            End Try

        End Function
        Public Function QueuePlace(ByVal strNombre As String, _
                                   ByVal strNumero As String, _
                                   ByVal strPrefatoryInstructionCode As String, _
                                   ByVal strPseudoCityCode As String, _
                                   ByVal strCodigoSeguimiento As String, _
                                   ByVal intGDS As Integer, _
                                   ByVal intFirmaGDS As Integer, _
                                   ByVal intFirmaDB As Integer, _
                                   ByVal objSession As classSession) As String() 'objSabreWS.QueuePlaceLLS204.QueuePlaceRS

            Dim QueuePlace204 As objSabreWS.QueuePlaceLLS204.QueuePlaceRS = Nothing
            Dim objQueuePlaceRS As classQueuePlaceRS = Nothing
            Dim objQueueInfo As classQueueInfo = Nothing
            Dim strRespuesta() As String = Nothing
            Try
                objIWebServices = New IWebServices
                QueuePlace204 = objIWebServices._QueuePlace204(strNombre, strNumero, strPrefatoryInstructionCode, strPseudoCityCode, strCodigoSeguimiento, intFirmaGDS, intFirmaDB, objSession)

                'objQueuePlaceRS = New classQueuePlaceRS
                'objQueueInfo.Name = ""
                '' objQueuePlaceRS.QueueInfo = 
                If QueuePlace204 IsNot Nothing Then
                    If QueuePlace204.Text IsNot Nothing Then
                        strRespuesta = QueuePlace204.Text
                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing

            End Try

            Return strRespuesta

        End Function
    End Class
End Namespace
