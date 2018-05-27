Imports GDS_NuevoMundoPersistencia
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports objSabreWS = GDS_NM_WebServicesSabre
Imports EscribeLog = GDS_MuevoMundoLog.EscribeLog
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function GeneraVoucherDePseudo(ByVal strFecha As String, _
                                              ByVal strCadenaPseudos As String, _
                                              ByVal strNombreArchivoLog As String, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intGDS As Integer, _
                                              ByVal intFirmaGDS As Integer, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal objSession As classSession) As List(Of String)

            Dim objReporteVentas As List(Of classReporteVentas) = Nothing
            Dim lstRespuesta As List(Of String) = Nothing
            Dim strRespuesta As String = Nothing

            Try

                objReporteVentas = DQB(strFecha, _
                                       strCadenaPseudos, _
                                       strCodigoSeguimiento, _
                                       intGDS, _
                                       intFirmaGDS, _
                                       intFirmaDB, _
                                       objSession)

                If objReporteVentas IsNot Nothing Then
                    For i As Integer = 0 To objReporteVentas.Count - 1
                        If objReporteVentas.Item(i).MSGError Is Nothing Then
                            For x As Integer = 0 To objReporteVentas.Item(i).Boletos.Count - 1
                                If objReporteVentas.Item(i).Boletos.Item(x).FormaPago IsNot Nothing Then
                                    If Not String.IsNullOrEmpty(objReporteVentas.Item(i).Boletos.Item(x).FormaPago) Then
                                        If objReporteVentas.Item(i).Boletos.Item(x).FormaPago.Equals("CC") Then

                                            strRespuesta = GeneraVoucherSabre(objReporteVentas.Item(i).Boletos.Item(x).NumBoleto, _
                                                                              strNombreArchivoLog, _
                                                                              strCodigoSeguimiento, _
                                                                              intGDS, _
                                                                              intFirmaGDS, _
                                                                              intFirmaDB, _
                                                                              objSession)


                                            If lstRespuesta Is Nothing Then lstRespuesta = New List(Of String)
                                            lstRespuesta.Add(strRespuesta)

                                        End If
                                    End If
                                End If
                            Next
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

                objReporteVentas = Nothing
                lstRespuesta = Nothing
            End Try

            Return lstRespuesta
        End Function
        Public Function GeneraVoucherSabre(ByVal strNumeroBoleto As String, _
                                           ByVal strNombreArchivoLog As String, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intGDS As Integer, _
                                           ByVal intFirmaGDS As Integer, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal objSession As classSession) As String

            Dim TCTicket As classTicketCoupon.TCTicket = Nothing
            Dim strRespuesta As String = Nothing
            Dim fecha As Date = Nothing

            Dim objCCCF As classCCCF = Nothing

            Dim contadorTax As Integer = 0
            Dim contadorTaxPaid As Integer = 0
            Dim contadorDYTax As Integer = 0
            Dim contadorIGVTax As Integer = 0
            Dim contadorIGVPaid As Integer = 0

            Dim bolExchange As Boolean = False
            Dim bolIGV As Boolean = True
            Dim Taxes As classTaxes = Nothing
            Dim srtCadenaArchivoVoucher As String = String.Empty
            Dim strNombreAerolinea As String = String.Empty

            Dim strCadenaTaxes As String = String.Empty
            Dim strCadenaTaxesExcluir As String = String.Empty

            Try

                TCTicket = TicketCupon(strNumeroBoleto, _
                                       strCodigoSeguimiento, _
                                       intGDS, _
                                       intFirmaGDS, _
                                       intFirmaDB, _
                                       objSession)


                If TCTicket IsNot Nothing Then
                    If TCTicket.ErroresAlertas Is Nothing Then

                        If TCTicket.TCCouponData IsNot Nothing Then

                            objCCCF = New classCCCF

                            If Not String.IsNullOrEmpty(TCTicket.TCCouponData.ExchangeInd) Then
                                If TCTicket.TCCouponData.ExchangeInd.ToUpper.Equals("X") Then
                                    objCCCF.Exchange = True
                                End If
                            End If


                            objCCCF.Prefijo = strNumeroBoleto.Substring(0, 3)
                            strNombreAerolinea = SabreCommand("W/*" & objCCCF.Prefijo, _
                                                              "NOMBREAEROLINEA", _
                                                              strCodigoSeguimiento, _
                                                              intGDS, _
                                                              intFirmaGDS, _
                                                              intFirmaDB, _
                                                              objSession)

                            If Not String.IsNullOrEmpty(strNombreAerolinea) Then
                                strNombreAerolinea = strNombreAerolinea.Substring(7, strNombreAerolinea.Length - 7)
                                strNombreAerolinea = Trim(strNombreAerolinea.Split(Constantes.Slash)(0)).ToUpper
                                objCCCF.NombreAerolinea = strNombreAerolinea
                            End If


                            objCCCF.NumeroBoleto = strNumeroBoleto.Substring(3, 10)

                            '"6" 'W/TATKT[NUMERO BOLETO]
                            objCCCF.DigitoVerificacion = SabreCommand("W/TATKT" & strNumeroBoleto, _
                                                                      "TATKT", _
                                                                      strCodigoSeguimiento, _
                                                                      intGDS, _
                                                                      intFirmaGDS, _
                                                                      intFirmaDB, _
                                                                      objSession).Replace("CK DIGIT IS ", "")

                            If TCTicket.TCCouponData.TCCoupon IsNot Nothing Then
                                objCCCF.Origen = TCTicket.TCCouponData.TCCoupon(0).TCFlightSegment.OriginLocation
                                objCCCF.Destino = TCTicket.TCCouponData.TCCoupon(TCTicket.TCCouponData.TCCoupon.Count - 1).TCFlightSegment.DestinationLocation
                            End If

                            If TCTicket.TCCouponData.AirItineraryPricing IsNot Nothing Then
                                If TCTicket.TCCouponData.AirItineraryPricing.Endorsements IsNot Nothing Then

                                    For i As Integer = 0 To TCTicket.TCCouponData.AirItineraryPricing.Endorsements.Count - 1
                                        If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.Endorsements.Item(0)) Then
                                            objCCCF.Endorsements &= IIf(String.IsNullOrEmpty(objCCCF.Endorsements), "", vbCr) & TCTicket.TCCouponData.AirItineraryPricing.Endorsements.Item(0).ToString
                                        End If
                                    Next

                                End If

                                If TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare IsNot Nothing Then
                                    'Neto
                                    If TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCBaseFare IsNot Nothing Then
                                        If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCBaseFare.CurrencyCode) Then
                                            objCCCF.SimboloMonedaNeto = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCBaseFare.CurrencyCode
                                        End If

                                        If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCBaseFare.Amount) Then
                                            objCCCF.Neto = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCBaseFare.Amount
                                        End If
                                    End If

                                    'NET
                                    If TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCNetFare IsNot Nothing Then
                                        If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCNetFare.AmountType) Then
                                            objCCCF.NetAmountType = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCNetFare.AmountType
                                        End If

                                        If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCNetFare.Amount) Then
                                            objCCCF.NetAmount = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCNetFare.Amount
                                        End If

                                        If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCNetFare.CreditCardAmount) Then
                                            objCCCF.NetCreditCardAmount = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCNetFare.CreditCardAmount
                                        End If

                                        objCCCF.Net = True
                                    End If

                                    'Equivalente
                                    If TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCEquivalenteFare IsNot Nothing Then
                                        If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCEquivalenteFare.CurrencyCode) Then
                                            objCCCF.SimboloMonedaEquivalente = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCEquivalenteFare.CurrencyCode
                                        End If

                                        If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCEquivalenteFare.Amount) Then
                                            objCCCF.Equivalente = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCEquivalenteFare.Amount
                                        End If
                                    End If

                                    'Impuestos
                                    If TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes IsNot Nothing Then
                                        If TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax IsNot Nothing Then

                                            If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.Total) Then
                                                objCCCF.TotalTaxes = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.Total
                                            End If

                                            '*** CONTAMOS EL TIPO DE IMPUESTOS ***
                                            For i As Integer = 0 To TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Count - 1
                                                If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).CodePaid) Then
                                                    If TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).TaxCode.Equals("PE") Then
                                                        contadorIGVPaid += 1
                                                    End If
                                                    contadorTaxPaid += 1
                                                Else
                                                    If TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).TaxCode.Equals("PE") Then
                                                        contadorIGVTax += 1
                                                    ElseIf TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).TaxCode.Equals("DY") Then
                                                        contadorDYTax += 1
                                                    End If
                                                    contadorTax += 1

                                                    If Not strCadenaTaxes.Contains(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).TaxCode) Then
                                                        strCadenaTaxes &= IIf(String.IsNullOrEmpty(strCadenaTaxes), "", "/") & TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).TaxCode
                                                    End If
                                                End If
                                            Next


                                            Dim lstTaxes As List(Of classTaxes) = Nothing

                                            If contadorTaxPaid = 0 And contadorTax = 2 Then
                                                If contadorIGVTax = 1 Then
                                                    lstTaxes = New List(Of classTaxes)
                                                    'Agregamos IGV
                                                    lstTaxes = AgregarListaImpuestos(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes, True, False, False)
                                                    If objCCCF.Taxes Is Nothing Then objCCCF.Taxes = New List(Of classTaxes)
                                                    objCCCF.Taxes.AddRange(lstTaxes)
                                                    'Agregamos Otro Tax
                                                    lstTaxes = AgregarListaImpuestos(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes, False, False, False)
                                                    objCCCF.Taxes.AddRange(lstTaxes)
                                                Else
                                                    objEscribeLog = New EscribeLog
                                                    objEscribeLog.WriteLog("Caso #2 no contemplado TKT " & objCCCF.Prefijo & objCCCF.NumeroBoleto, strNombreArchivoLog, strCodigoSeguimiento)
                                                End If
                                            ElseIf contadorTaxPaid = 0 And contadorTax > 3 Then
                                                If contadorIGVTax = 1 Then
                                                    '*** RESTAR EL IGV DEL TOTAL
                                                    lstTaxes = New List(Of classTaxes)
                                                    'Sumamos Otros Taxes
                                                    lstTaxes = AgregarListaImpuestos(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes, False, True, False, "PE")
                                                    If objCCCF.Taxes Is Nothing Then objCCCF.Taxes = New List(Of classTaxes)
                                                    objCCCF.Taxes.AddRange(lstTaxes)
                                                    'Agregamos IGV
                                                    lstTaxes = AgregarListaImpuestos(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes, True, False, False)
                                                    objCCCF.Taxes.AddRange(lstTaxes)
                                                Else
                                                    If contadorDYTax = 1 Then
                                                        lstTaxes = New List(Of classTaxes)
                                                        'Sumamos Otros Taxes
                                                        lstTaxes = AgregarListaImpuestos(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes, False, True, False, "DY", String.Empty)
                                                        If objCCCF.Taxes Is Nothing Then objCCCF.Taxes = New List(Of classTaxes)
                                                        objCCCF.Taxes.AddRange(lstTaxes)
                                                        'Agregamos DY
                                                        strCadenaTaxesExcluir &= IIf(String.IsNullOrEmpty(strCadenaTaxesExcluir), "", Constantes.Slash) & strCadenaTaxes.Split(Constantes.Slash)(0)
                                                        lstTaxes = AgregarListaImpuestos(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes, False, False, False, String.Empty, "DY")
                                                        If objCCCF.Taxes Is Nothing Then objCCCF.Taxes = New List(Of classTaxes)
                                                        objCCCF.Taxes.AddRange(lstTaxes)
                                                    Else
                                                        lstTaxes = New List(Of classTaxes)
                                                        'Agregamos el primer impuesto
                                                        strCadenaTaxesExcluir &= IIf(String.IsNullOrEmpty(strCadenaTaxesExcluir), "", Constantes.Slash) & strCadenaTaxes.Split(Constantes.Slash)(0)
                                                        lstTaxes = AgregarListaImpuestos(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes, False, False, False, String.Empty, strCadenaTaxes.Split(Constantes.Slash)(0))
                                                        If objCCCF.Taxes Is Nothing Then objCCCF.Taxes = New List(Of classTaxes)
                                                        objCCCF.Taxes.AddRange(lstTaxes)
                                                        'Agregamos el segundo impuesto
                                                        strCadenaTaxesExcluir &= IIf(String.IsNullOrEmpty(strCadenaTaxesExcluir), "", Constantes.Slash) & strCadenaTaxes.Split(Constantes.Slash)(1)
                                                        lstTaxes = AgregarListaImpuestos(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes, False, False, False, String.Empty, strCadenaTaxes.Split(Constantes.Slash)(1))
                                                        If objCCCF.Taxes Is Nothing Then objCCCF.Taxes = New List(Of classTaxes)
                                                        objCCCF.Taxes.AddRange(lstTaxes)
                                                        'Sumamos Otros Taxes
                                                        lstTaxes = AgregarListaImpuestos(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes, False, True, False, strCadenaTaxesExcluir, String.Empty)
                                                        If objCCCF.Taxes Is Nothing Then objCCCF.Taxes = New List(Of classTaxes)
                                                        objCCCF.Taxes.AddRange(lstTaxes)
                                                    End If
                                                End If
                                            ElseIf contadorTaxPaid = 2 And contadorTax = 1 Then
                                                If contadorIGVPaid = 1 Then
                                                    lstTaxes = New List(Of classTaxes)
                                                    'Agrega IGV PD
                                                    lstTaxes = AgregarListaImpuestos(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes, True, False, True)
                                                    If objCCCF.Taxes Is Nothing Then objCCCF.Taxes = New List(Of classTaxes)
                                                    objCCCF.Taxes.AddRange(lstTaxes)
                                                    'Agrega Otro impuesto PD
                                                    lstTaxes = AgregarListaImpuestos(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes, False, False, True)
                                                    If objCCCF.Taxes Is Nothing Then objCCCF.Taxes = New List(Of classTaxes)
                                                    objCCCF.Taxes.AddRange(lstTaxes)
                                                    'Agrega Otro impuesto 
                                                    lstTaxes = AgregarListaImpuestos(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes, False, False, False)
                                                    If objCCCF.Taxes Is Nothing Then objCCCF.Taxes = New List(Of classTaxes)
                                                    objCCCF.Taxes.AddRange(lstTaxes)
                                                Else
                                                    objEscribeLog = New EscribeLog
                                                    objEscribeLog.WriteLog("Caso #3 no contemplado TKT " & objCCCF.Prefijo & objCCCF.NumeroBoleto, strNombreArchivoLog, strCodigoSeguimiento)
                                                End If
                                            ElseIf contadorTaxPaid > 2 And contadorTax > 2 Then
                                                lstTaxes = New List(Of classTaxes)
                                                'Sumamos PD
                                                lstTaxes = AgregarListaImpuestos(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes, False, True, True)
                                                If objCCCF.Taxes Is Nothing Then objCCCF.Taxes = New List(Of classTaxes)
                                                objCCCF.Taxes.AddRange(lstTaxes)
                                                'Sumamos Otros Tax
                                                lstTaxes = AgregarListaImpuestos(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes, False, True, False)
                                                objCCCF.Taxes.AddRange(lstTaxes)
                                            Else
                                                objEscribeLog = New EscribeLog
                                                objEscribeLog.WriteLog("Caso #4 no contemplado TKT " & objCCCF.Prefijo & objCCCF.NumeroBoleto, strNombreArchivoLog, strCodigoSeguimiento)
                                            End If

                                        End If
                                    End If

                                    'Total
                                    If TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTotalFare IsNot Nothing Then
                                        If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTotalFare.CurrencyCode) Then
                                            objCCCF.SimboloMonedaTotal = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTotalFare.CurrencyCode
                                        End If

                                        If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTotalFare.Amount) Then
                                            objCCCF.Total = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTotalFare.Amount
                                        End If
                                    End If
                                End If

                            End If

                            If Not String.IsNullOrEmpty(TCTicket.TCCouponData.IssueDate) Then
                                fecha = New Date
                                fecha = TCTicket.TCCouponData.IssueDate.ToString
                                objCCCF.FechaEmision = TraducirMes(fecha.ToString("ddMMMyy").ToUpper)
                                fecha = Nothing
                            End If

                            If TCTicket.TCCouponData.ItineraryRef IsNot Nothing Then
                                If Not String.IsNullOrEmpty(TCTicket.TCCouponData.ItineraryRef.ID) Then
                                    objCCCF.CodigoReserva = TCTicket.TCCouponData.ItineraryRef.ID
                                End If
                            End If

                            If TCTicket.TCCouponData.TCCustomer IsNot Nothing Then
                                If TCTicket.TCCouponData.TCCustomer.TCPersonName IsNot Nothing Then
                                    If Not String.IsNullOrEmpty(TCTicket.TCCouponData.TCCustomer.TCPersonName.Surname) Then
                                        objCCCF.NombrePasajero = TCTicket.TCCouponData.TCCustomer.TCPersonName.Surname
                                    End If

                                    If Not String.IsNullOrEmpty(TCTicket.TCCouponData.TCCustomer.TCPersonName.GivenName) Then
                                        objCCCF.NombrePasajero &= "/" & TCTicket.TCCouponData.TCCustomer.TCPersonName.GivenName
                                    End If

                                    If Not String.IsNullOrEmpty(TCTicket.TCCouponData.TCCustomer.TCPersonName.NameReference) Then
                                        objCCCF.DocumentoPasajero = TCTicket.TCCouponData.TCCustomer.TCPersonName.NameReference
                                    End If

                                End If

                                If TCTicket.TCCouponData.TCCustomer.TCPayment IsNot Nothing Then
                                    For i As Integer = 0 To TCTicket.TCCouponData.TCCustomer.TCPayment.Count - 1

                                        If TCTicket.TCCouponData.TCCustomer.TCPayment.Item(i).TCPaymentCard IsNot Nothing Then
                                            If Not String.IsNullOrEmpty(TCTicket.TCCouponData.TCCustomer.TCPayment.Item(i).TCPaymentCard.Code) Then
                                                objCCCF.CodigoTarjeta = TCTicket.TCCouponData.TCCustomer.TCPayment.Item(i).TCPaymentCard.Code.ToString
                                            End If

                                            If Not String.IsNullOrEmpty(TCTicket.TCCouponData.TCCustomer.TCPayment.Item(i).TCPaymentCard.ExpirationDate) Then
                                                objCCCF.FechaExpiracion = TCTicket.TCCouponData.TCCustomer.TCPayment.Item(i).TCPaymentCard.ExpirationDate.ToString
                                            End If
                                        End If

                                        If Not String.IsNullOrEmpty(TCTicket.TCCouponData.TCCustomer.TCPayment.Item(i).ReferenceNumber) Then
                                            objCCCF.NumeroTarjeta = TCTicket.TCCouponData.TCCustomer.TCPayment.Item(i).ReferenceNumber.ToString
                                        End If

                                        If Not String.IsNullOrEmpty(TCTicket.TCCouponData.TCCustomer.TCPayment.Item(i).ApprovalID) Then
                                            objCCCF.CodigoAprobacion = TCTicket.TCCouponData.TCCustomer.TCPayment.Item(i).ApprovalID.ToString
                                        End If

                                    Next
                                End If



                            End If

                        End If

                        If Not String.IsNullOrEmpty(TCTicket.IATA_Number) Then
                            objCCCF.IATA = TCTicket.IATA_Number

                            If objCCCF.IATA.Equals("91500286") Then
                                objCCCF.NombreIata = "NM"
                                objCCCF.CiudadIata = "LIMA"
                                objCCCF.PaisIata = "PE"
                            ElseIf objCCCF.IATA.Equals("91500312") Then
                                objCCCF.NombreIata = "INTERAGENCIAS"
                                objCCCF.CiudadIata = "LIMA"
                                objCCCF.PaisIata = "PE"
                            Else
                                objEscribeLog = New EscribeLog
                                objEscribeLog.WriteLog("Iata " & objCCCF.IATA & " no registrada " & objCCCF.Prefijo & objCCCF.NumeroBoleto, strNombreArchivoLog, strCodigoSeguimiento)
                            End If

                        End If

                        If Not String.IsNullOrEmpty(TCTicket.PrimeHostID) Then
                            objCCCF.CodigoGDS = TCTicket.PrimeHostID
                        End If



                        srtCadenaArchivoVoucher = CuerpoVoucher(objCCCF)
                        If Not String.IsNullOrEmpty(srtCadenaArchivoVoucher) Then
                            objEscribeLog = New EscribeLog
                            objEscribeLog.WriteArchivo(srtCadenaArchivoVoucher, "\\sistemas21san\vouchersprueba\" & objCCCF.CodigoReserva, strNumeroBoleto)
                            objEscribeLog.WriteLog("Archivo generado \\sistemas21san\vouchersprueba\" & objCCCF.CodigoReserva, strNombreArchivoLog, strCodigoSeguimiento)

                            strRespuesta = objCCCF.Prefijo & objCCCF.NumeroBoleto
                            'C:\inetpub\wwwroot\GNM_DiagramaClases\GNM_Integracion\GDS_MuevoMundoLog\archivosTXT\Voucher\
                            '\\sistemas21san\vouchersprueba\QF05\20141229\
                            '\\sistemas21san\vouchersprueba
                        End If


                    Else
                        strRespuesta = "Error"
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

                TCTicket = Nothing
                fecha = Nothing

                objCCCF = Nothing
                strNombreAerolinea = Nothing
            End Try

            Return strRespuesta

        End Function

        Private Function CuerpoVoucher(ByVal objCCCF As classCCCF) As String

            Dim strCuerpoVoucher As System.Text.StringBuilder = Nothing
            Dim auxEndorsements As String = String.Empty
            Dim intContador As Integer = 0
            Dim strAuxNeto As String = String.Empty
            Try

                If objCCCF IsNot Nothing Then
                    strCuerpoVoucher = New System.Text.StringBuilder
                    '===============================================
                    'ETKT                CREDIT CARD CHARGE FORM
                    strCuerpoVoucher.Append(CompletarEspacios(20, "ETKT", "D") & "CREDIT CARD CHARGE FORM" & vbCrLf)
                    '===============================================
                    'LAN PERU S.A.                      PCL/PCL       INTERAGENCIAS
                    strCuerpoVoucher.Append(CompletarEspacios(35, objCCCF.NombreAerolinea, "D") & _
                                            objCCCF.Origen & Constantes.Slash & objCCCF.Destino & _
                                            CompletarEspacios(7, Nothing, Nothing) & objCCCF.NombreIata & vbCrLf)
                    '===============================================
                    'RUC20341841357 PAYONLYINUSD/NONREF 18DEC14       LIM           PE
                    '/CHNGS NOT PERMITTED                             91500312    RCIMEV/1S
                    If objCCCF.Endorsements.Length <= 34 Then
                        strCuerpoVoucher.Append(CompletarEspacios(35, objCCCF.Endorsements, "D") & _
                                                CompletarEspacios(14, objCCCF.FechaEmision, "D") & _
                                                CompletarEspacios(15, objCCCF.CiudadIata, "D") & objCCCF.PaisIata & vbCrLf)
                        '===============================================
                        strCuerpoVoucher.Append(CompletarEspacios(49, Nothing, Nothing) & _
                                                CompletarEspacios(12, objCCCF.IATA, "D") & _
                                                objCCCF.CodigoReserva & Constantes.Slash & objCCCF.CodigoGDS & vbCrLf)
                    Else
                        strCuerpoVoucher.Append(CompletarEspacios(35, objCCCF.Endorsements.Substring(0, 34), "D") & _
                                                CompletarEspacios(14, objCCCF.FechaEmision, "D") & _
                                                CompletarEspacios(15, objCCCF.CiudadIata, "D") & objCCCF.PaisIata & vbCrLf)
                        '===============================================
                        auxEndorsements = objCCCF.Endorsements.Substring(34, (objCCCF.Endorsements.Length - 34))
                        If auxEndorsements.Length > 34 Then
                            auxEndorsements = auxEndorsements.Substring(0, 34)
                        End If
                        strCuerpoVoucher.Append(CompletarEspacios(49, auxEndorsements, "D") & _
                                                CompletarEspacios(12, objCCCF.IATA, "D") & _
                                                objCCCF.CodigoReserva & Constantes.Slash & objCCCF.CodigoGDS & vbCrLf)

                    End If
                    '===============================================
                    'BRICENO/LUIS
                    strCuerpoVoucher.Append(objCCCF.NombrePasajero & vbCrLf)
                    '===============================================
                    'RUC20393663988
                    strCuerpoVoucher.Append(objCCCF.DocumentoPasajero & vbCrLf)
                    '===============================================
                    strCuerpoVoucher.Append("SIGNATURE X-" & vbCrLf)
                    '===============================================
                    strCuerpoVoucher.Append("I ACKNOWLEDGE PURCHASE OF TRANSPORTATION RELATED SERVICES AND OR" & vbCrLf)
                    strCuerpoVoucher.Append("GOODS AND AM AWARW OF APPLICABLE RESTRICTIONS AND/OR PENALTIES" & vbCrLf)
                    strCuerpoVoucher.Append("ASSOCIATED WITH THE PURCHASE AS SHOWN ON THIS RECEIPT" & vbCrLf)
                    '===============================================
                    strCuerpoVoucher.Append(vbCrLf)
                    '===============================================
                    If Not String.IsNullOrEmpty(objCCCF.SimboloMonedaEquivalente) Then
                        'EUR    113.00 neto
                        strCuerpoVoucher.Append(objCCCF.SimboloMonedaNeto & CompletarEspacios(11, objCCCF.Neto, "I") & vbCrLf)
                        'USD    113.00 equivalente
                        strCuerpoVoucher.Append(objCCCF.SimboloMonedaEquivalente & CompletarEspacios(11, objCCCF.Equivalente, "I") & vbCrLf)
                    Else
                        'USD    113.00 neto
                        If objCCCF.Net Then
                            strAuxNeto = (CDbl(objCCCF.NetCreditCardAmount) - CDbl(objCCCF.TotalTaxes)).ToString("###,###,###.00")
                            strCuerpoVoucher.Append(objCCCF.SimboloMonedaNeto & CompletarEspacios(11, strAuxNeto, "I") & vbCrLf)
                        Else
                            strCuerpoVoucher.Append(objCCCF.SimboloMonedaNeto & CompletarEspacios(11, objCCCF.Neto, "I") & vbCrLf)
                        End If
                        strCuerpoVoucher.Append(vbCrLf)
                    End If
                    '===============================================
                    'Impuestos
                    For i As Integer = 0 To objCCCF.Taxes.Count - 1

                        strCuerpoVoucher.Append(objCCCF.Taxes.Item(i).Paid & CompletarEspacios(12, objCCCF.Taxes.Item(i).Monto & objCCCF.Taxes.Item(i).Codigo, "I") & Constantes.Espacio)

                        If i = 1 Then
                            strCuerpoVoucher.Append(Constantes.Espacio)

                            If objCCCF.Exchange Then
                                strCuerpoVoucher.Append("CC")
                            End If

                            strCuerpoVoucher.Append(objCCCF.CodigoTarjeta & _
                                                    objCCCF.NumeroTarjeta & _
                                                    Constantes.Espacio & Constantes.Espacio & _
                                                    CompletarEspacios(11, objCCCF.FechaExpiracion.Replace(Constantes.Guion, Constantes.Slash), "D"))
                            strCuerpoVoucher.Append("APVL" & Constantes.Espacio & _
                                                    objCCCF.CodigoAprobacion.Split(Constantes.Espacio)(1) & _
                                                    Constantes.Espacio & _
                                                    CompletarCeros(objCCCF.CodigoAprobacion.Split(Constantes.Espacio)(0)))

                        End If
                        strCuerpoVoucher.Append(vbCrLf)
                        intContador += 1
                    Next
                    '===============================================
                    If intContador = 0 Then
                        strCuerpoVoucher.Append(vbCrLf)
                        strCuerpoVoucher.Append(vbCrLf)
                        strCuerpoVoucher.Append(vbCrLf)
                    ElseIf intContador = 1 Then
                        strCuerpoVoucher.Append(vbCrLf)
                        strCuerpoVoucher.Append(vbCrLf)
                    ElseIf intContador = 2 Then
                        strCuerpoVoucher.Append(vbCrLf)
                    End If
                    '===============================================
                    'USD       147.63  0011/    544  6767618284 6
                    strCuerpoVoucher.Append(objCCCF.SimboloMonedaTotal)
                    If objCCCF.Net Then
                        strCuerpoVoucher.Append(CompletarEspacios(11, objCCCF.NetCreditCardAmount, "I"))
                    Else
                        strCuerpoVoucher.Append(CompletarEspacios(11, objCCCF.Total, "I"))
                    End If
                    strCuerpoVoucher.Append(Constantes.Espacio & Constantes.Espacio & _
                                            CompletarEspacios(9, "0011/", "D") & _
                                            objCCCF.Prefijo & Constantes.Espacio & objCCCF.NumeroBoleto & Constantes.Espacio & objCCCF.DigitoVerificacion & vbCrLf)

                    '===============================================
                    strCuerpoVoucher.Append(vbCrLf)
                    '===============================================
                    strCuerpoVoucher.Append("***EOM***")

                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objCCCF = Nothing
                auxEndorsements = Nothing
            End Try

            Return strCuerpoVoucher.ToString

        End Function
    End Class
End Namespace
