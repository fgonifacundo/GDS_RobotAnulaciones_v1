Imports GDS_NuevoMundoPersistencia
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports objSabreWS = GDS_NM_WebServicesSabre
Imports EscribeLog = GDS_MuevoMundoLog.EscribeLog
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function PrepararCuponAuditor(ByVal strNumeroBoleto As String, _
                                             ByVal TCTicket As classTicketCoupon.TCTicket, _
                                             ByVal strNombreArchivoLog As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intGDS As Integer, _
                                             ByVal intFirmaGDS As Integer, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal objSession As classSession) As String

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


                                            '*** AGREGAMOS IMPUESTOS ORIGINALES
                                            '*** CONTAMOS EL TIPO DE IMPUESTOS ***
                                            For i As Integer = 0 To TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Count - 1

                                                Taxes = New classTaxes

                                                If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).CodePaid) Then

                                                    Taxes.Paid = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).CodePaid
                                                    Taxes.Codigo = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).TaxCode
                                                    If TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).TaxCode.Equals("PE") Then
                                                        contadorIGVPaid += 1
                                                    End If
                                                    contadorTaxPaid += 1

                                                Else
                                                    Taxes.Codigo = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).TaxCode

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

                                                Taxes.Monto = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).Amount

                                                If objCCCF.TaxesOriginales Is Nothing Then objCCCF.TaxesOriginales = New List(Of classTaxes)
                                                objCCCF.TaxesOriginales.Add(Taxes)


                                            Next

                                            Dim lstTaxes As List(Of classTaxes) = Nothing
                                            Taxes = Nothing

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
                                objEscribeLog.WriteLog("Iata " & objCCCF.IATA & " no registrada " & objCCCF.Prefijo & objCCCF.NumeroBoleto, strNombreArchivoLog, strCodigoSeguimiento)
                            End If

                        End If

                        If Not String.IsNullOrEmpty(TCTicket.PrimeHostID) Then
                            objCCCF.CodigoGDS = TCTicket.PrimeHostID
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
        Private Function AgregarListaImpuestos(ByVal TCTaxes As classTicketCoupon.TCTaxes, _
                                                ByVal bolPE As Boolean, _
                                                ByVal bolSumar As Boolean, _
                                                ByVal bolTaxPaid As Boolean, _
                                                Optional ByVal strExcluirTax As String = "", _
                                                Optional ByVal strAgregarTax As String = "") As List(Of classTaxes)

            Dim Taxes As classTaxes = Nothing
            Dim lstTaxes As List(Of classTaxes) = Nothing
            Dim bolAgregar As Boolean = False

            Try

                If Not bolSumar Then
                    For i As Integer = 0 To TCTaxes.TCTax.Count - 1
                        Taxes = New classTaxes
                        bolAgregar = False

                        If bolPE Then
                            If TCTaxes.TCTax.Item(i).TaxCode.Equals("PE") Then
                                If bolTaxPaid Then
                                    If Not String.IsNullOrEmpty(TCTaxes.TCTax.Item(i).CodePaid) Then
                                        Taxes.Paid = TCTaxes.TCTax.Item(i).CodePaid
                                        Taxes.Codigo = TCTaxes.TCTax.Item(i).TaxCode
                                        Taxes.Monto = TCTaxes.TCTax.Item(i).Amount
                                        bolAgregar = True
                                    End If
                                Else
                                    If String.IsNullOrEmpty(TCTaxes.TCTax.Item(i).CodePaid) Then
                                        Taxes.Paid = "  "
                                        Taxes.Codigo = TCTaxes.TCTax.Item(i).TaxCode
                                        Taxes.Monto = TCTaxes.TCTax.Item(i).Amount
                                        bolAgregar = True
                                    End If
                                End If
                            End If
                        Else
                            If Not TCTaxes.TCTax.Item(i).TaxCode.Equals("PE") Then
                                If bolTaxPaid Then
                                    If Not String.IsNullOrEmpty(TCTaxes.TCTax.Item(i).CodePaid) Then
                                        Taxes.Paid = TCTaxes.TCTax.Item(i).CodePaid
                                        Taxes.Codigo = TCTaxes.TCTax.Item(i).TaxCode
                                        Taxes.Monto = TCTaxes.TCTax.Item(i).Amount
                                        bolAgregar = True
                                    End If
                                Else
                                    If String.IsNullOrEmpty(TCTaxes.TCTax.Item(i).CodePaid) Then
                                        Taxes.Paid = "  "
                                        Taxes.Codigo = TCTaxes.TCTax.Item(i).TaxCode
                                        Taxes.Monto = TCTaxes.TCTax.Item(i).Amount
                                        bolAgregar = True
                                    End If
                                End If
                            End If
                        End If


                        If Not String.IsNullOrEmpty(strAgregarTax) Then
                            If Not strAgregarTax.Contains(TCTaxes.TCTax.Item(i).TaxCode) Then
                                bolAgregar = False
                            End If
                        End If

                        If bolAgregar Then
                            If lstTaxes Is Nothing Then lstTaxes = New List(Of classTaxes)
                            lstTaxes.Add(Taxes)
                            If Not String.IsNullOrEmpty(strAgregarTax) Then
                                Exit For
                            End If
                        End If

                        Taxes = Nothing

                    Next
                Else

                    Taxes = New classTaxes


                    For i As Integer = 0 To TCTaxes.TCTax.Count - 1
                        If bolTaxPaid Then
                            If Not String.IsNullOrEmpty(TCTaxes.TCTax.Item(i).CodePaid) Then
                                If Not strExcluirTax.Contains(TCTaxes.TCTax.Item(i).TaxCode) Then
                                    Taxes.Paid = "PD"
                                    Taxes.Codigo = "XT"
                                    Taxes.Monto = (CDbl(IIf(String.IsNullOrEmpty(Taxes.Monto), "0.00", Taxes.Monto)) + CDbl(TCTaxes.TCTax.Item(i).Amount)).ToString("###,###,###.00")
                                    bolAgregar = True
                                End If
                            End If
                        Else
                            If String.IsNullOrEmpty(TCTaxes.TCTax.Item(i).CodePaid) Then
                                If Not strExcluirTax.Contains(TCTaxes.TCTax.Item(i).TaxCode) Then
                                    Taxes.Paid = Constantes.Espacio & Constantes.Espacio
                                    Taxes.Codigo = "XT"
                                    Taxes.Monto = (CDbl(IIf(String.IsNullOrEmpty(Taxes.Monto), "0.00", Taxes.Monto)) + CDbl(TCTaxes.TCTax.Item(i).Amount)).ToString("###,###,###.00")
                                    bolAgregar = True
                                End If
                            End If
                        End If
                    Next

                    If bolAgregar Then
                        If lstTaxes Is Nothing Then lstTaxes = New List(Of classTaxes)
                        lstTaxes.Add(Taxes)
                    End If
                    Taxes = Nothing

                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally

            End Try

            Return lstTaxes

        End Function
    End Class
End Namespace