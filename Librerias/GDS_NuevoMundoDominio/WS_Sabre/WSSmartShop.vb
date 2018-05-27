Imports GDS_NuevoMundoPersistencia
Imports objSabreWS = GDS_NM_WebServicesSabre
Imports EscribeLog = GDS_MuevoMundoLog.EscribeLog
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Imports GDS_NM_Mensajeria

Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Dim lstTheadFee As New List(Of classTheadFare)
        Public Function BorrarTablas(ByVal strPNR As String, _
                                     ByVal intDK As Integer, _
                                     ByVal strCiudadDestino As String, _
                                     ByVal strCodigoSeguimiento As String, _
                                     ByVal intFirmaDB As Integer, _
                                     ByVal intEsquema As Integer)

            Dim objDAO As DAO = Nothing
            Try


                If Not String.IsNullOrEmpty(strPNR) Then
                    If intDK > 0 Then
                        objDAO = New DAO
                        If Not objDAO.EliminarTarifasBulkFq(strPNR, intDK, strCodigoSeguimiento, intFirmaDB, intEsquema) Then
                            'Enviar alerta
                        End If

                        If objDAO.EliminarTarifasBulkCombinaciones(strPNR, intDK, strCodigoSeguimiento, intFirmaDB, intEsquema) Then
                            'Enviar alerta
                        End If

                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally

            End Try
        End Function
        Public Function GenerarPNR() As String
            Dim pnr2 As String = Nothing

            Try

                Randomize()
                For i As Integer = 0 To 5
                    pnr2 &= CStr(CInt(9 * Rnd()))
                Next

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            End Try

            Return pnr2

        End Function
        Public Function ObtenerPseudosTarifasNegociadas(ByVal strPNR As String, _
                                                       ByVal strDK As String, _
                                                       ByVal strAerolinea As String, _
                                                       ByVal strPseudoBusqueda As String, _
                                                       ByVal strOrientacion As String, _
                                                       ByVal strCiudadOrigen As String, _
                                                       ByVal strCiudadDestino As String, _
                                                       ByVal strFechaSalida As String, _
                                                       ByVal strFechaRetorno As String, _
                                                       ByVal strCodigoSeguimiento As String, _
                                                       ByVal intFirmaDB As Integer, _
                                                       ByVal intEsquema As Integer) As classPseudosTarifasNegociadas

            Dim objPseudosTarifasNegociadas As classPseudosTarifasNegociadas = Nothing
            Dim oPesudos As List(Of String) = Nothing
            Dim oAerolineas As List(Of String) = Nothing
            Dim objCiudadOrigen As classCiudad = Nothing
            Dim objCiudadDestino As classCiudad = Nothing
            Dim objAerolinea As classAerolineaC = Nothing
            Dim lstAerolinea As List(Of classAerolineaC) = Nothing
            Dim lstPseudobulkConceptos As List(Of classPseudoBulkConceptos) = Nothing
            Dim lstTourCodesConceptos As List(Of classTourCodesConceptos) = Nothing
            Dim lstIata As classIata = Nothing
            Dim fecha As Date = Nothing
            Dim strPseudoEmision As String = strPseudoBusqueda
            Dim strAerolineas As String = String.Empty
            Dim objDAO As DAO = Nothing
            Try
                objDAO = New DAO


                objCiudadOrigen = objDAO.ObtenerDatosCiudad(strCiudadOrigen, strCodigoSeguimiento, intFirmaDB, intEsquema)
                objCiudadDestino = objDAO.ObtenerDatosCiudad(strCiudadDestino, strCodigoSeguimiento, intFirmaDB, intEsquema)
                lstIata = objDAO.ObtenerIATA(strPseudoBusqueda, strCodigoSeguimiento, intFirmaDB, intEsquema)

                '*** SI NO INGRESO AEROLINEA OBTENEMOS TODAS LAS AEROLÍNEA QUE TENGAN REGLAS CARGADAS
                If String.IsNullOrEmpty(strAerolinea) Then
                    lstAerolinea = objDAO.ObtenerPosiblesAerolineasCC(strCodigoSeguimiento, intFirmaDB, intEsquema)
                Else
                    lstAerolinea = New List(Of classAerolineaC)
                    objAerolinea = New classAerolineaC
                    objAerolinea.IdTransportador = strAerolinea
                    lstAerolinea.Add(objAerolinea)
                End If

                If oPesudos Is Nothing Then oPesudos = New List(Of String)
                oPesudos.Add(strPseudoBusqueda)
                strPseudoEmision = strPseudoBusqueda

                For i As Integer = 0 To lstAerolinea.Count - 1
                    'INSERTA EN LA TABLA PseudosBulk_Conceptos
                    objDAO.ObtenerConceptosPseudosBulk(lstAerolinea.Item(i).IdTransportador, _
                                                       objCiudadDestino.CodCiudad, _
                                                       strPNR, _
                                                       strCodigoSeguimiento, _
                                                       intFirmaDB, _
                                                       intEsquema)
                    'LEE LA TABLA PseudosBulk_Conceptos
                    lstPseudobulkConceptos = objDAO.ObtenerPseudosbulkConceptos(strPNR, strCodigoSeguimiento, intFirmaDB, intEsquema)

                    If Not lstPseudobulkConceptos Is Nothing Then
                        If lstPseudobulkConceptos.Count > 0 Then
                            'lee la tabla TOURCODES_CONCEPTOS
                            lstTourCodesConceptos = objDAO.ObtenerTourCodesConceptos(strCodigoSeguimiento, intFirmaDB, intEsquema)

                            For j As Integer = 0 To lstPseudobulkConceptos.Count - 1
                                'JMATTO ADD 
                                Dim k As Integer = lstTourCodesConceptos.FindIndex(Function(invoice)
                                                                                       Return (invoice.CodigoConcepto = lstPseudobulkConceptos.Item(j).CodigoConcepto)
                                                                                   End Function)
                                If Not (k = -1) Then
                                    'Select Case lstTourCodesConceptos.Item(k).CodigoConcepto
                                        Select Case lstTourCodesConceptos.Item(k).CodigoConcepto

                                            Case 1 ' 1 = FAREBASIS

                                            Case 2 ' 2 = CLASRESERV

                                            Case 3 ' 3 = SUCURSAL
                                                lstTourCodesConceptos.Item(k).Valor = "'" & lstIata.Sucursal & "'"
                                            Case 4 ' 4 = TIPOSTOCK
                                                lstTourCodesConceptos.Item(k).Valor = "'ELE'"
                                            Case 5 ' 5 = 1raLETFARE-SAL

                                            Case 6 ' 6 = CIUORIGEN
                                                lstTourCodesConceptos.Item(k).Valor = "'" & objCiudadOrigen.CodCiudad & "'"
                                            Case 7 ' 7 = CIUREGRESO
                                                If strOrientacion = "R" Then
                                                    lstTourCodesConceptos.Item(k).Valor = "'" & objCiudadDestino.CodCiudad & "'"
                                                End If
                                            Case 8 ' 8 = CIUDESTINO
                                                lstTourCodesConceptos.Item(k).Valor = "'" & objCiudadDestino.CodCiudad & "'"

                                            Case 9 ' 9 = PAISDESTINO
                                                lstTourCodesConceptos.Item(k).Valor = "'" & objCiudadDestino.CodPais & "'"

                                            Case 10 ' 10 = FECRETORNO DD-MM-AAAA
                                                If strOrientacion = "O" Then
                                                    lstTourCodesConceptos.Item(k).Valor = ""
                                                Else
                                                    fecha = New Date
                                                    fecha = strFechaRetorno
                                                    lstTourCodesConceptos.Item(k).Valor = "'" & fecha.ToString("dd-MM-yyyy") & "'"
                                                End If

                                            Case 11 ' 11 = 1raLETFARE-RET

                                            Case 12 ' 12 = TIPOPAX (A,C,I)
                                                lstTourCodesConceptos.Item(k).Valor = "'A'"

                                            Case 13 ' 13 = PAISRETORNO
                                                If strOrientacion = "R" Then
                                                    lstTourCodesConceptos.Item(k).Valor = "'" & objCiudadDestino.CodPais & "'"
                                                End If
                                            Case 14 ' 14 = LIBRE

                                            Case 15 ' 15 = LIBRE

                                            Case 16 ' 16 = CODSHARE
                                                lstTourCodesConceptos.Item(k).Valor = "'NO'"

                                            Case 17 ' 17 = TIPORUTA
                                                If Not objCiudadOrigen.CodPais.Equals("PE") Or Not objCiudadDestino.CodPais.Equals("PE") Then
                                                    lstTourCodesConceptos.Item(k).Valor = "'I'"
                                                Else
                                                    lstTourCodesConceptos.Item(k).Valor = "'C'"
                                                End If

                                            Case 18 ' 18 = UNIREGULA
                                                lstTourCodesConceptos.Item(k).Valor = 1

                                            Case 19 ' 19 = TIPOVUELO(ON-OFF)
                                                lstTourCodesConceptos.Item(k).Valor = "'ON'"

                                            Case 20 ' 20 = FECSALIDA (DD-MM-AAAA)
                                                fecha = New Date
                                                fecha = strFechaSalida
                                                lstTourCodesConceptos.Item(k).Valor = "'" & fecha.ToString("dd-MM-yyyy") & "'"

                                            Case 21 ' 21 = PAISORIGEN"
                                                lstTourCodesConceptos.Item(k).Valor = "'" & objCiudadOrigen.CodPais & "'"

                                            Case 22 ' 22 = LINAEREAAUX

                                            Case 23 ' 23 = CODSHARE-LINAEREA

                                            Case 24 ' 24 = CLASCABINA

                                            Case 25 ' 25 = CANTFARE

                                            Case 26 ' 26 = CLASCAB-SAL

                                            Case 27 ' 27 = CLASCAB-RET

                                            Case 28 ' 28 = 1raLETFARE-ALL

                                            Case 29 ' 29 = PAIS-FINVIAJE
                                                If strOrientacion = "O" Then
                                                    lstTourCodesConceptos.Item(k).Valor = "'" & objCiudadDestino.CodPais & "'"
                                                Else
                                                    lstTourCodesConceptos.Item(k).Valor = "'" & objCiudadOrigen.CodPais & "'"
                                                End If

                                            Case 30 ' 30 = CIU-FINVIAJE
                                                If strOrientacion = "O" Then
                                                    lstTourCodesConceptos.Item(k).Valor = "'" & objCiudadDestino.CodCiudad & "'"
                                                Else
                                                    lstTourCodesConceptos.Item(k).Valor = "'" & objCiudadOrigen.CodCiudad & "'"
                                                End If

                                            Case 31 ' 31 = REG-DES
                                                lstTourCodesConceptos.Item(k).Valor = "'" & objCiudadDestino.CodRegion & "'"

                                            Case 32 ' 32 = REG-RET
                                                lstTourCodesConceptos.Item(k).Valor = "'" & objCiudadDestino.CodRegion & "'"

                                            Case 33 ' 33 = CIUAUX-SAL

                                            Case 34 ' 34 = CIUAUX-RET

                                            Case 35 ' 35 = TIPOVIAJE
                                                If strOrientacion = "O" Then
                                                    lstTourCodesConceptos.Item(k).Valor = "'OW'"
                                                Else
                                                    lstTourCodesConceptos.Item(k).Valor = "'RT'"
                                                End If

                                            Case 36 ' 36 = FORMA-PAGO

                                            Case 37 ' 37 = PAX-CLERO

                                            Case 38 ' 38 = TIPO-PAXESPECIAL                                      

                                                ''''''''''''''''''''

                                            Case 39 ' 39 = ES-REEMISION

                                            Case 40 ' 40 = NO-VUELO

                                            Case 41 ' 41 = PSEUDO
                                                lstTourCodesConceptos.Item(k).Valor = "'" & strPseudoBusqueda & "'"

                                            Case 42 ' 42 = IATA
                                                lstTourCodesConceptos.Item(k).Valor = "'" & lstIata.Iata & "'"

                                            Case 43 ' 43 = CLIENTE
                                                lstTourCodesConceptos.Item(k).Valor = "'" & strDK & "'"

                                            Case 44 ' 44 = INCLUIR YQ

                                            Case 45 ' 45 = No de VUELO SALIDA

                                            Case 46 ' 46 = No de VUELO RETORNO

                                            Case 47 ' 47 = CON RUC

                                            Case 48 ' 48 = SUBCODIGO

                                            Case 49 ' 49 = ACCOUNT CODE

                                            Case 50 ' 50 = FB SIN TKT DESIG

                                            Case 51 ' 51 = TKT EN CONJUNCION

                                            Case 52 ' 52 = Ciudad CONEX Salida

                                            Case 53 ' 53 = Ciudad CONEX Retorno

                                            Case 54 ' 54 = CIUDAD CONEX APLICA

                                            Case 55 ' 55 = TARIFAS CORPORATIVAS

                                        End Select
                                    End If
                                Next
  
                            'INSERTA EN LA TABLA PSEUDOSBULK_EVALUACION
                            objDAO.InsertarPseudosBulkEvaluacion(strPNR, strDK, lstTourCodesConceptos, strCodigoSeguimiento, intFirmaDB, intEsquema)

                            '------------------PRUEBA----------------

                            Dim objPseudosBulkEvaluacion As List(Of classPseudosBulkEvaluacion) = Nothing
                            objPseudosBulkEvaluacion = objDAO.SP_NEW_PseudosBulkEvaluacion(lstAerolinea.Item(i).IdTransportador, _
                                                                                           objCiudadDestino.CodCiudad, _
                                                                                           strPNR, _
                                                                                           strDK, _
                                                                                           strPseudoBusqueda, _
                                                                                           strCodigoSeguimiento, _
                                                                                           intFirmaDB, _
                                                                                           intEsquema)

                            If objPseudosBulkEvaluacion IsNot Nothing Then

                                If Not strAerolineas.Contains(lstAerolinea.Item(i).IdTransportador) Then
                                    If oAerolineas Is Nothing Then oAerolineas = New List(Of String)
                                    oAerolineas.Add(lstAerolinea.Item(i).IdTransportador)
                                    strAerolineas &= IIf(String.IsNullOrEmpty(strAerolineas), "", "/") & lstAerolinea.Item(i).IdTransportador
                                End If

                                '

                                For k As Integer = 0 To objPseudosBulkEvaluacion.Count - 1

                                    Dim oReglaX As String = IIf(objPseudosBulkEvaluacion.Item(k).Regla Is System.DBNull.Value, "", objPseudosBulkEvaluacion.Item(k).Regla)
                                    Dim oPseudoAutorizadoX As String = IIf(objPseudosBulkEvaluacion.Item(k).PseudosVenta Is System.DBNull.Value, "", objPseudosBulkEvaluacion.Item(k).PseudosVenta)
                                    Dim oPseudoEmisorX As String = IIf(objPseudosBulkEvaluacion.Item(k).PseudosEmision Is System.DBNull.Value, "", objPseudosBulkEvaluacion.Item(k).PseudosEmision)


                                    'QF05;HW57;"

                                    If oPseudoAutorizadoX.Contains(strPseudoBusqueda) Then
                                        For m As Integer = 0 To oPseudoEmisorX.Split(";").Length - 1
                                            If oPseudoEmisorX.Split(";")(m) IsNot Nothing Then
                                                If oPseudoEmisorX.Split(";")(m).Length = 4 Then
                                                    If strPseudoEmision.IndexOf(oPseudoEmisorX.Split(";")(m)) = -1 Then
                                                        If oPesudos Is Nothing Then oPesudos = New List(Of String)
                                                        oPesudos.Add(oPseudoEmisorX.Split(";")(m))
                                                        strPseudoEmision &= IIf(strPseudoEmision = "", "", "/") & oPseudoEmisorX.Split(";")(m)
                                                    End If
                                                End If
                                            End If
                                        Next

                                    End If

                                Next


                            End If

                        End If
                    End If

                Next


                If objPseudosTarifasNegociadas Is Nothing Then objPseudosTarifasNegociadas = New classPseudosTarifasNegociadas
                objPseudosTarifasNegociadas.Pseudos = New List(Of String)
                objPseudosTarifasNegociadas.Pseudos = oPesudos


                objPseudosTarifasNegociadas.Aerolineas = New List(Of String)
                objPseudosTarifasNegociadas.Aerolineas = oAerolineas


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strPNR = Nothing
                strDK = Nothing
                strAerolinea = Nothing
                strPseudoBusqueda = Nothing
                strOrientacion = Nothing
                strCiudadOrigen = Nothing
                strCiudadDestino = Nothing
                strFechaSalida = Nothing
                strFechaRetorno = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
            End Try

            Return objPseudosTarifasNegociadas
            'Return strPseudoEmision & "#" & strAerolineas

        End Function
        Public Function HilosTarifasBulk(ByVal strDK As String, _
                                                 ByVal strPNR As String, _
                                                 ByVal strOrigen As String, _
                                                 ByVal strDestino As String, _
                                                 ByVal strFechaSalida As String, _
                                                 ByVal strFechaRetorno As String, _
                                                 ByVal strPseudoConsulta As String, _
                                                 ByVal strOrientacion As String, _
                                                 ByVal strCodigoSeguimiento As String, _
                                                 ByVal intFirmaDB As Integer, _
                                                 ByVal intEsquema As Integer, _
                                                 ByVal objFare As objSabreWS.Fare.FareRS, _
                                                 ByVal h As Integer, _
                                                 ByVal strTipoTarifa As String) As classFQ.classFare

            Dim objAuxFare As classFQ.classFareRS = Nothing
            Dim strAuxTarifasNoProcesadas As String = String.Empty
            Dim objAuxTipoPasajero As classDatosTipoPasajero = Nothing
            Dim objTipoPaxVuelo As classTipoPaxVuelo = Nothing
            Dim objPseudos As classFQ.classPseudoFare = Nothing
            Dim strCadenaReglas As String = String.Empty
            Dim objModuloComercial As classFeeTarifaBulkResultado() = Nothing
            Dim objFQ As List(Of classFQ.classFareRS) = Nothing

            Dim ListaThreadFee As List(Of classThreadNM) = Nothing
            Dim ThreadFee As classTheadFare = Nothing
            Dim xListaThread As classThreadNM = Nothing

            Dim objRespuesta As classFQ.classFare = Nothing

            Dim auxContador As Integer = 0

            Try

                'Threading.Thread.Sleep(1000)

                For i As Integer = 0 To objFare.FareBasis.Length - 1
                    objAuxFare = New classFQ.classFareRS

                    'If auxContador = 50 Then
                    '    Threading.Thread.Sleep(2000)
                    '    auxContador = 0
                    'End If

                    objAuxFare.ID = i
                    strAuxTarifasNoProcesadas = objAuxFare.ID & "/"

                    objAuxFare.DK = strDK
                    strAuxTarifasNoProcesadas &= objAuxFare.DK & "/"

                    If Not strTipoTarifa.Equals("PL") Then
                        objAuxFare.PNR = strPNR & CStr(h) & CStr(i)

                        strAuxTarifasNoProcesadas &= objAuxFare.PNR & "/"
                    Else
                        objAuxFare.PNR = strPNR
                    End If

                    'Airline
                    If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                        If objFare.FareBasis(i).AdditionalInformation.Airline IsNot Nothing Then
                            objAuxFare.AirLines = objFare.FareBasis(i).AdditionalInformation.Airline.Code
                            strAuxTarifasNoProcesadas &= objAuxFare.AirLines & "/"
                        End If
                    End If

                    'PassengerType
                    If objFare.FareBasis(i).PassengerType IsNot Nothing Then
                        For x As Integer = 0 To objFare.FareBasis(i).PassengerType.Length - 1
                            objAuxTipoPasajero = New classDatosTipoPasajero
                            objTipoPaxVuelo = New classTipoPaxVuelo
                            objTipoPaxVuelo = ObtenerTipoPaxEspecial(objFare.FareBasis(i).PassengerType(x).Code, strCodigoSeguimiento, intFirmaDB, intEsquema)

                            If objTipoPaxVuelo IsNot Nothing Then
                                objAuxTipoPasajero.ID = objTipoPaxVuelo.IdTipoDePax
                                If Not String.IsNullOrEmpty(objTipoPaxVuelo.Equivale) Then
                                    objAuxTipoPasajero.Equivale = objTipoPaxVuelo.Equivale
                                Else
                                    GoTo SIGUIENTE
                                End If

                                If Not String.IsNullOrEmpty(objTipoPaxVuelo.Pertenece) Then
                                    objAuxTipoPasajero.Pertenece = objTipoPaxVuelo.Pertenece
                                Else
                                    GoTo SIGUIENTE
                                End If


                                strAuxTarifasNoProcesadas &= objAuxTipoPasajero.ID & "/"
                            Else
                                GoTo SIGUIENTE
                                objAuxTipoPasajero.ID = objFare.FareBasis(i).PassengerType(x).Code
                                strAuxTarifasNoProcesadas &= objAuxTipoPasajero.ID & "/"
                            End If

                            If objAuxFare.PassengerType Is Nothing Then objAuxFare.PassengerType = New List(Of classDatosTipoPasajero)
                            objAuxFare.PassengerType.Add(objAuxTipoPasajero)
                        Next
                    End If
                    objAuxTipoPasajero = Nothing

                    'FareBasis
                    objAuxFare.FareBasis = objFare.FareBasis(i).Code
                    strAuxTarifasNoProcesadas &= objAuxFare.FareBasis & "/"

                    'Cabin
                    If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                        objAuxFare.Cabin = objFare.FareBasis(i).AdditionalInformation.Cabin
                        strAuxTarifasNoProcesadas &= objAuxFare.Cabin & "/"
                    End If

                    'BookingClass
                    If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                        objAuxFare.BookingClass = objFare.FareBasis(i).AdditionalInformation.ResBookDesigCode
                        strAuxTarifasNoProcesadas &= objAuxFare.BookingClass & "/"
                    End If

                    'Currency
                    If objFare.FareBasis(i).BaseFare IsNot Nothing Then
                        objAuxFare.Currency = objFare.FareBasis(i).BaseFare.CurrencyCode
                        strAuxTarifasNoProcesadas &= objAuxFare.Currency & "/"
                    End If


                    If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                        If strOrientacion = "RT" Then
                            If objFare.FareBasis(i).AdditionalInformation.OneWayRoundTrip(0).Ind = "R" Or _
                               objFare.FareBasis(i).AdditionalInformation.OneWayRoundTrip(0).Ind = "X" Then
                                If objFare.FareBasis(i).AdditionalInformation.Fare IsNot Nothing Then
                                    'If objFare.FareBasis(i).AdditionalInformation.Fare.Length > 1 Then
                                    'BaseFare
                                    objAuxFare.BaseFare = objFare.FareBasis(i).AdditionalInformation.Fare(0).Amount
                                    strAuxTarifasNoProcesadas &= objAuxFare.BaseFare & "/"
                                    'End If
                                End If
                            End If
                        Else
                            If objFare.FareBasis(i).BaseFare IsNot Nothing Then
                                'BaseFare
                                objAuxFare.BaseFare = objFare.FareBasis(i).BaseFare.Amount
                                strAuxTarifasNoProcesadas &= objAuxFare.BaseFare & "/"
                            End If
                        End If
                    End If

                    'ExpirationDate
                    If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                        objAuxFare.ExpirationDate = objFare.FareBasis(i).AdditionalInformation.ExpirationDate
                        If objAuxFare.ExpirationDate.Equals("0:00:00") Then objAuxFare.ExpirationDate = String.Empty
                        If objAuxFare.ExpirationDate.Contains("12:00:00") Then objAuxFare.ExpirationDate = String.Empty
                        strAuxTarifasNoProcesadas &= objAuxFare.ExpirationDate & "/"
                    End If

                    'EffectiveDate
                    If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                        objAuxFare.EffectiveDate = objFare.FareBasis(i).AdditionalInformation.EffectiveDate
                        If objAuxFare.EffectiveDate.Equals("0:00:00") Then objAuxFare.EffectiveDate = String.Empty
                        If objAuxFare.EffectiveDate.Contains("12:00:00") Then objAuxFare.EffectiveDate = String.Empty
                        strAuxTarifasNoProcesadas &= objAuxFare.EffectiveDate & "/"
                    End If

                    'TicketDate
                    If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                        objAuxFare.TicketDate = objFare.FareBasis(i).AdditionalInformation.TicketDate
                        If objAuxFare.TicketDate.Equals("0:00:00") Then objAuxFare.TicketDate = String.Empty
                        If objAuxFare.TicketDate.Contains("12:00:00") Then objAuxFare.TicketDate = String.Empty
                        strAuxTarifasNoProcesadas &= objAuxFare.TicketDate & "/"
                    End If

                    'AdvancePurchase
                    If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                        objAuxFare.AdvancePurchase = objFare.FareBasis(i).AdditionalInformation.AdvancePurchase
                        strAuxTarifasNoProcesadas &= objAuxFare.AdvancePurchase & "/"
                    End If

                    'MinStay
                    If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                        objAuxFare.MinStay = objFare.FareBasis(i).AdditionalInformation.MinStay
                        strAuxTarifasNoProcesadas &= objAuxFare.MinStay & "/"
                    End If

                    'MaxStay
                    If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                        objAuxFare.MaxStay = objFare.FareBasis(i).AdditionalInformation.MaxStay
                        strAuxTarifasNoProcesadas &= objAuxFare.MaxStay & "/"
                    End If

                    'MaxStay
                    If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                        objAuxFare.MaxStay = objFare.FareBasis(i).AdditionalInformation.MaxStay
                        strAuxTarifasNoProcesadas &= objAuxFare.MaxStay & "/"
                    End If

                    'Pseudo
                    If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                        objPseudos = New classFQ.classPseudoFare
                        objPseudos.Pseudo = lstTheadFare.Item(h).Pseudo 'strCadenaPseudos.Split(Constantes.Slash)(h)
                        strAuxTarifasNoProcesadas &= objAuxFare.MaxStay & "/"
                        objPseudos.IDs = h

                        objAuxFare.Pseudos = New List(Of classFQ.classPseudoFare)
                        objAuxFare.Pseudos.Add(objPseudos)
                    End If


                    'SeasonalApplication
                    If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                        If objFare.FareBasis(i).AdditionalInformation.SeasonalApplication IsNot Nothing Then
                            objAuxFare.SeasonalApplication = objFare.FareBasis(i).AdditionalInformation.SeasonalApplication(0)
                        End If
                    End If

                    'RuleCategory
                    If objFare.FareBasis(i).AdditionalInformation IsNot Nothing Then
                        If objFare.FareBasis(i).AdditionalInformation.Rule IsNot Nothing Then
                            strCadenaReglas = String.Empty
                            For x As Integer = 0 To objFare.FareBasis(i).AdditionalInformation.Rule.Length - 1
                                strCadenaReglas &= IIf(String.IsNullOrEmpty(strCadenaReglas), "", "/") & objFare.FareBasis(i).AdditionalInformation.Rule(x)
                                'If objAuxFare.RuleCategory Is Nothing Then objAuxFare.RuleCategory = New List(Of String)
                                'objAuxFare.RuleCategory.Add(objFare.FareBasis(i).AdditionalInformation.Rule(x).ToString)
                            Next
                            If objAuxFare.RuleCategory Is Nothing Then objAuxFare.RuleCategory = New List(Of String)
                            objAuxFare.RuleCategory.Add(strCadenaReglas)
                        End If
                    End If
                    objAuxFare.tipoTarifa = strTipoTarifa


                    If Not strTipoTarifa.Equals("PL") Then


                        '**************************************************************************
                        '*****            EVALUAMOS REGLAS DE FEE                              ****
                        '**************************************************************************



                        ThreadFee = New classTheadFare
                        ThreadFee.FareRS = objAuxFare
                        ThreadFee.Pseudo = strPseudoConsulta
                        ThreadFee.Orientacion = strOrientacion
                        ThreadFee.Origen = strOrigen
                        ThreadFee.Destino = strDestino
                        ThreadFee.FechaSalida = strFechaSalida
                        ThreadFee.FechaRetorno = strFechaRetorno
                        ThreadFee.CodigoSeguimiento = strCodigoSeguimiento
                        ThreadFee.FirmaDB = intFirmaDB
                        ThreadFee.Esquema = intEsquema


                        xListaThread = New classThreadNM

                        xListaThread.ThreadNM = New Threading.Thread(AddressOf ObtenerReglasTarifasNegociadas)
                        xListaThread.ID = h
                        xListaThread.ThreadNM.IsBackground = True
                        xListaThread.ThreadNM.Start(ThreadFee)

                        If ListaThreadFee Is Nothing Then ListaThreadFee = New List(Of classThreadNM)
                        'ObtenerReglasTarifasNegociadas(ThreadFee)
                        ListaThreadFee.Add(xListaThread)
                    Else

                        If objFQ Is Nothing Then objFQ = New List(Of classFQ.classFareRS)
                        objFQ.Add(objAuxFare)

                    End If
SIGUIENTE:

                    auxContador += 1

                Next



                If Not strTipoTarifa.Equals("PL") Then
                    '***************************************************************
                    Dim bolThread As Boolean = False
                    While bolThread = False
                        Dim intContador As Integer = 0
                        For z As Integer = 0 To ListaThreadFee.Count - 1
                            If Not ListaThreadFee.Item(z).ThreadNM.IsAlive Then
                                ListaThreadFee.Item(z).ThreadNM.Abort()
                                intContador += 1
                            End If
                        Next

                        If intContador = ListaThreadFee.Count Then
                            bolThread = True
                        End If
                    End While
                    '***************************************************************



                    For i As Integer = 0 To lstTheadFee.Count - 1


                        If lstTheadFee.Item(i).FeeTarifaBulkResultado IsNot Nothing Then
                            If CDbl(lstTheadFee.Item(i).FeeTarifaBulkResultado(0).Fee_Minimo) >= 0 Then
                                If CDbl(lstTheadFee.Item(i).FeeTarifaBulkResultado(0).Fee_Maximo) >= 0 Then

                                    objAuxFare = New classFQ.classFareRS
                                    objAuxFare = lstTheadFee.Item(i).FareRS
                                    objAuxFare.PNR = strPNR

                                    If lstTheadFee.Item(i).FeeTarifaBulkResultado(0).Es_porcentaje = 0 Then
                                        objAuxFare.FeeMinino = lstTheadFee.Item(i).FeeTarifaBulkResultado(0).Fee_Minimo
                                        objAuxFare.FeeMaximo = lstTheadFee.Item(i).FeeTarifaBulkResultado(0).Fee_Maximo
                                    Else
                                        objAuxFare.FeeMinino = (lstTheadFee.Item(i).FeeTarifaBulkResultado(0).Fee_Minimo / 100) * objAuxFare.BaseFare
                                        objAuxFare.FeeMaximo = (lstTheadFee.Item(i).FeeTarifaBulkResultado(0).Fee_Maximo / 100) * objAuxFare.BaseFare
                                    End If


                                    If objFQ Is Nothing Then objFQ = New List(Of classFQ.classFareRS)
                                    objFQ.Add(objAuxFare)
                                    objAuxFare = Nothing

                                End If
                            End If
                        End If

                    Next

                End If


                If objRespuesta Is Nothing Then objRespuesta = New classFQ.classFare
                objRespuesta.FareRS = objFQ

            Catch ex As Exception
                objRespuesta = Nothing
                Throw New Exception(ex.ToString)
            Finally

            End Try

            Return objRespuesta

        End Function
        Public Function EnviarCorreoRegulaciones(ByVal objRegulacionTarifa As classRegulaciones.classRegulacionTarifa, _
                                                  ByVal numLineas As List(Of String), _
                                                  ByVal strOrigen As String, _
                                                  ByVal strDestino As String, _
                                                  ByVal strFechaSalida As String, _
                                                  ByVal strFareBasis As String, _
                                                  ByVal strAerolinea As String, _
                                                  ByVal strPara As String, _
                                                  ByVal strCC As String, _
                                                  ByVal strBCC As String, _
                                                  ByVal strSubject As String, _
                                                  ByVal strNombreCorreo As String, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intCuenta As Integer, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As Integer

            Dim strCadena As System.Text.StringBuilder = Nothing

            Dim objCorreo As New classCorreo
            Dim objEnviarEmail As New EnviarEmail
            Dim intRespuesta As Integer = 0
            Dim color As String = Nothing

            Try

                strCadena = New System.Text.StringBuilder

                strCadena.Append("<html>" & vbCrLf)
                strCadena.Append("<body>" & vbCrLf)
                strCadena.Append("<table width='450' height='134' border='0'  class='regulacion'>" & vbCrLf)
                strCadena.Append("<tr bgcolor='#EBEBEB'>" & vbCrLf)
                strCadena.Append("<td width='80'><strong>Origen</strong></td>" & vbCrLf)
                strCadena.Append("<td width='11'>:</td>" & vbCrLf)
                strCadena.Append("<td width='259'>" & strOrigen & vbCrLf)
                strCadena.Append("</tr>" & vbCrLf)
                strCadena.Append("<tr>" & vbCrLf)
                strCadena.Append("<td><strong>Destino</strong></td>" & vbCrLf)
                strCadena.Append("<td>:</td>" & vbCrLf)
                strCadena.Append("<td>" & strDestino & vbCrLf)
                strCadena.Append("</tr>" & vbCrLf)
                strCadena.Append("<tr bgcolor='#EBEBEB'>" & vbCrLf)
                strCadena.Append("<td><strong>Fecha Salida </strong></td>" & vbCrLf)
                strCadena.Append("<td>:</td>" & vbCrLf)
                strCadena.Append("<td>" & strFechaSalida & vbCrLf)
                strCadena.Append("</tr>" & vbCrLf)
                strCadena.Append("<tr>" & vbCrLf)
                strCadena.Append("<td><strong>Fare Basis </strong></td>" & vbCrLf)
                strCadena.Append("<td>:</td>" & vbCrLf)
                strCadena.Append("<td>" & strFareBasis & vbCrLf)
                strCadena.Append("</tr>" & vbCrLf)
                strCadena.Append("<tr bgcolor='#EBEBEB'>" & vbCrLf)
                strCadena.Append("<td><strong>Aerolínea </strong></td>" & vbCrLf)
                strCadena.Append("<td>:</td>" & vbCrLf)
                strCadena.Append("<td>" & strAerolinea & vbCrLf)
                strCadena.Append("</tr>" & vbCrLf)
                strCadena.Append("</table>" & vbCrLf)
                strCadena.Append("</br>" & vbCrLf)
                strCadena.Append("</br>" & vbCrLf)

                strCadena.Append("<table width='450' height='134' border='0'  class='regulacion'>" & vbCrLf)

                For i As Integer = 0 To numLineas.Count - 1
                    For j As Integer = 0 To objRegulacionTarifa.Reglas.Count - 1
                        If numLineas.Item(i) = objRegulacionTarifa.Reglas.Item(j).NumRegulacion Then
                            strCadena.Append("<tr>" & vbCrLf)
                            strCadena.Append("<td width='30px'><strong>" & vbCrLf)
                            strCadena.Append(objRegulacionTarifa.Reglas.Item(j).NumRegulacion)
                            strCadena.Append("</strong></td>" & vbCrLf)
                            strCadena.Append("<td width='400px'><strong>" & vbCrLf)
                            strCadena.Append(objRegulacionTarifa.Reglas.Item(j).Titulo)
                            strCadena.Append("</strong></td>" & vbCrLf)
                            strCadena.Append("<td>" & vbCrLf)
                            strCadena.Append("</tr>" & vbCrLf)

                            For k As Integer = 0 To objRegulacionTarifa.Reglas.Item(j).Text.Length - 1
                                color = "bgcolor='#EBEBEB'"

                                If k Mod 2 <> 0 Then
                                    color = ""
                                End If

                                strCadena.Append("<td" & vbCrLf)
                                strCadena.Append(" >&nbsp;</td>" & vbCrLf)

                                strCadena.Append("<td " & vbCrLf)
                                strCadena.Append(color & vbCrLf)
                                strCadena.Append(">" + objRegulacionTarifa.Reglas.Item(j).Text(k).ToString() + "</td>" & vbCrLf)
                                strCadena.Append("</tr>")
                            Next

                            strCadena.Append("</td>" & vbCrLf)
                            strCadena.Append("</tr>" & vbCrLf)
                        End If
                    Next
                Next



                strCadena.Append("</table>" & vbCrLf)

                strCadena.Append("</body>" & vbCrLf)
                strCadena.Append("</html>" & vbCrLf)




                If Not String.IsNullOrEmpty(strNombreCorreo) And _
                   Not String.IsNullOrEmpty(strPara) And _
                   Not String.IsNullOrEmpty(strCadena.ToString) And _
                   Not String.IsNullOrEmpty(strSubject) Then

                    objCorreo.NombreCorreo = strNombreCorreo
                    objCorreo.ToCorreo = strPara
                    objCorreo.CCCorreo = strCC
                    objCorreo.BCCCorreo = strBCC
                    objCorreo.BodyCorreo = strCadena.ToString
                    objCorreo.SubjectCorreo = strSubject

                    objEnviarEmail.Send(objCorreo, _
                                        True, _
                                        strCodigoSeguimiento, _
                                        intCuenta)

                    intRespuesta = 1
                Else
                    intRespuesta = 0
                    Err.Raise(-1111, "classDM_Email.EnviarCorreo", "Alguno de los valores OBLIGATORIOS llegaron vacios")
                End If



            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strDestino = Nothing
                strFechaSalida = Nothing
                strFareBasis = Nothing
                strAerolinea = Nothing
                strCadena = Nothing
                numLineas = Nothing
                objRegulacionTarifa = Nothing
                objCorreo = Nothing
                objEnviarEmail = Nothing
                strPara = Nothing
                strCC = Nothing
                strBCC = Nothing
                strSubject = Nothing
                strNombreCorreo = Nothing
                strCodigoSeguimiento = Nothing
                intCuenta = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return intRespuesta

        End Function
        Private Sub ObtenerReglasTarifasNegociadas(ByVal objThreadFee As Object)
            Dim ThreadFee As classTheadFare = CType(objThreadFee, classTheadFare)
            Dim objAuxFare As classFQ.classFareRS = CType(ThreadFee.FareRS, classFQ.classFareRS)

            Dim objConceptosEvaluacion As classConceptosEvaluacion = Nothing
            Dim objConceptosTarifa As classCETarifa = Nothing
            Dim fecha As Date = Nothing

            Dim objCiudadOrigen As classCiudad = Nothing
            Dim objCiudadDestino As classCiudad = Nothing
            Dim objIata As classIata = Nothing

            Dim objDAO As DAO = Nothing
            Dim objFeeTarifaBulkResultado As classFeeTarifaBulkResultado() = Nothing


            Try

                objDAO = New DAO

                objCiudadOrigen = objDAO.ObtenerDatosCiudad(ThreadFee.Origen, ThreadFee.CodigoSeguimiento, ThreadFee.FirmaDB, ThreadFee.Esquema)
                objCiudadDestino = objDAO.ObtenerDatosCiudad(ThreadFee.Destino, ThreadFee.CodigoSeguimiento, ThreadFee.FirmaDB, ThreadFee.Esquema)
                objIata = objDAO.ObtenerIATA(objAuxFare.Pseudos.Item(0).Pseudo, ThreadFee.CodigoSeguimiento, ThreadFee.FirmaDB, ThreadFee.Esquema)

                objConceptosEvaluacion = New classConceptosEvaluacion
                objConceptosTarifa = New classCETarifa

                objConceptosEvaluacion.LINEAVALIDADORA = CambioAerolinea(objAuxFare.AirLines, ThreadFee.CodigoSeguimiento, ThreadFee.FirmaDB, ThreadFee.Esquema)
                objConceptosEvaluacion.IATAEMISION = objIata.Iata

                '1 = FAREBASIS
                objConceptosTarifa.FAREBASIS = objAuxFare.FareBasis

                ' 2 = CLASRESERV
                objConceptosEvaluacion.CLASRESERV = objAuxFare.BookingClass

                ' 3 = SUCURSAL
                objConceptosEvaluacion.SUCURSAL = objIata.Sucursal

                ' 4 = TIPOSTOCK
                objConceptosEvaluacion.TIPOSTOCK = "ELE"

                ' 5 = 1raLETFARE-SAL
                objConceptosTarifa.PrimerLETFARESAL = objAuxFare.FareBasis.Substring(0, 1)

                ' 6 = CIUORIGEN
                objConceptosEvaluacion.CIUORIGEN = objCiudadOrigen.CodCiudad

                ' 7 = CIUREGRESO
                If ThreadFee.Orientacion = "RT" Then
                    objConceptosEvaluacion.CIUREGRESO = objCiudadDestino.CodCiudad
                End If
                ' 8 = CIUDESTINO
                objConceptosEvaluacion.CIUDESTINO = objCiudadDestino.CodCiudad

                ' 9 = PAISDESTINO
                objConceptosEvaluacion.PAISDESTINO = objCiudadDestino.CodPais

                ' 10 = FECRETORNO DD-MM-AAAA
                If ThreadFee.Orientacion = "RT" Then
                    fecha = New Date
                    fecha = ThreadFee.FechaRetorno
                    objConceptosEvaluacion.FECRETORNO = fecha.ToString("dd-MM-yyyy")
                End If

                ' 11 = 1raLETFARE-RET
                objConceptosTarifa.PrimeraLETFARERET = objAuxFare.FareBasis.Substring(0, 1)

                ' 12 = TIPOPAX (A,C,I)
                objConceptosTarifa.TIPOPAX = objAuxFare.PassengerType.Item(0).Equivale.Substring(0, 1)
                'cambio en WSSabre, para obtener tipos de pasajeros.......


                ' 13 = PAISRETORNO
                If ThreadFee.Orientacion = "RT" Then
                    objConceptosEvaluacion.PAISRETORNO = objCiudadDestino.CodPais
                End If
                ' 14 = LIBRE

                ' 15 = LIBRE

                ' 16 = CODSHARE
                objConceptosEvaluacion.CODSHARE = "NO"

                ' 17 = TIPORUTA

                If Not objCiudadOrigen.CodPais.Equals("PE") Or Not objCiudadDestino.CodPais.Equals("PE") Then
                    objConceptosEvaluacion.TIPORUTA = "I"
                Else
                    objConceptosEvaluacion.TIPORUTA = "C"
                End If

                ' 18 = UNIREGULA
                objConceptosEvaluacion.UNIREGULA = 1

                ' 19 = TIPOVUELO(ON-OFF)
                objConceptosEvaluacion.TIPOVUELO = "ON"

                ' 20 = FECSALIDA (DD-MM-AAAA)
                fecha = New Date
                fecha = ThreadFee.FechaSalida
                objConceptosEvaluacion.FECSALIDA = fecha.ToString("dd-MM-yyyy")

                ' 21 = PAISORIGEN"
                objConceptosEvaluacion.PAISORIGEN = objCiudadOrigen.CodPais

                ' 22 = LINAEREAAUX

                ' 23 = CODSHARE-LINAEREA

                ' 24 = CLASCABINA
                objConceptosEvaluacion.CLASCABINA = objAuxFare.Cabin

                ' 25 = CANTFARE
                objConceptosTarifa.CANTFARE = 1

                ' 26 = CLASCAB-SAL
                objConceptosEvaluacion.CLASCABINA = objAuxFare.Cabin

                ' 27 = CLASCAB-RET
                objConceptosEvaluacion.CLASCABINA = objAuxFare.Cabin

                ' 28 = 1raLETFARE-ALL
                objConceptosTarifa.PrimeraLETFARERET = objAuxFare.FareBasis.Substring(0, 1)

                ' 29 = PAIS-FINVIAJE
                If ThreadFee.Orientacion = "OW" Then
                    objConceptosEvaluacion.PAISFINVIAJE = objCiudadDestino.CodPais
                Else
                    objConceptosEvaluacion.PAISFINVIAJE = objCiudadOrigen.CodPais
                End If

                ' 30 = CIU-FINVIAJE
                If ThreadFee.Orientacion = "OW" Then
                    objConceptosEvaluacion.CIUFINVIAJE = objCiudadDestino.CodCiudad
                Else
                    objConceptosEvaluacion.CIUFINVIAJE = objCiudadOrigen.CodCiudad
                End If

                ' 31 = REG-DES
                objConceptosEvaluacion.REGDES = objCiudadDestino.CodRegion

                ' 32 = REG-RET
                objConceptosEvaluacion.REGRET = objCiudadDestino.CodRegion

                ' 33 = CIUAUX-SAL
                objConceptosEvaluacion.CIUORIGEN = objCiudadOrigen.CodCiudad

                ' 34 = CIUAUX-RET
                If ThreadFee.Orientacion = "RT" Then
                    objConceptosEvaluacion.CIUREGRESO = objCiudadDestino.CodCiudad
                End If

                ' 35 = TIPOVIAJE
                If ThreadFee.Orientacion = "OW" Then
                    objConceptosEvaluacion.TIPOVIAJE = "OW"
                Else
                    objConceptosEvaluacion.TIPOVIAJE = "RT"
                End If

                ' 36 = FORMA-PAGO

                ' 37 = PAX-CLERO

                ' 38 = TIPO-PAXESPECIAL                                      
                objConceptosTarifa.TIPOPAXESPECIAL = objAuxFare.PassengerType.Item(0).ID

                ' 39 = ES-REEMISION

                ' 40 = NO-VUELO

                ' 41 = PSEUDO
                objConceptosEvaluacion.PSEUDO = objAuxFare.Pseudos.Item(0).Pseudo

                ' 42 = IATA

                'Por Analizar
                'objConceptosEvaluacion.IATACONSULTA = objIata.Iata
                'objConceptosEvaluacion.IATAEMISION = objIata.Iata

                ' 43 = CLIENTE
                objConceptosEvaluacion.CLIENTE = objAuxFare.DK

                ' 44 = INCLUIR YQ

                ' 45 = No de VUELO SALIDA

                ' 46 = No de VUELO RETORNO

                ' 47 = CON RUC

                ' 48 = SUBCODIGO

                ' 49 = ACCOUNT CODE

                ' 50 = FB SIN TKT DESIG
                objConceptosTarifa.FBSINTKTDESIG = objAuxFare.FareBasis.Split("/")(0)

                ' 51 = TKT EN CONJUNCION

                ' 52 = Ciudad CONEX Salida

                ' 53 = Ciudad CONEX Retorno

                ' 54 = CIUDAD CONEX APLICA

                ' 55 = TARIFAS CORPORATIVAS
                objConceptosEvaluacion.TARIFASCORPORATIVAS = "NO"


                If objConceptosEvaluacion.ConceptosTarifa Is Nothing Then objConceptosEvaluacion.ConceptosTarifa = New List(Of classCETarifa)
                objConceptosEvaluacion.ConceptosTarifa.Add(objConceptosTarifa)

                objFeeTarifaBulkResultado = ModuloTarifaBulk_HS(objAuxFare.PNR, _
                                                                ThreadFee.Pseudo, _
                                                                objConceptosEvaluacion, _
                                                                1, _
                                                                ThreadFee.CodigoSeguimiento, _
                                                                ThreadFee.FirmaDB, _
                                                                ThreadFee.Esquema)

                If objFeeTarifaBulkResultado IsNot Nothing Then
                    ThreadFee.FareRS = objAuxFare
                    ThreadFee.FeeTarifaBulkResultado = objFeeTarifaBulkResultado
                Else
                    ThreadFee.FeeTarifaBulkResultado = Nothing
                End If

                lstTheadFee.Add(ThreadFee)

            Catch ex As Exception
                objFeeTarifaBulkResultado = Nothing
                Throw New Exception(ex.ToString)

            Finally

            End Try

        End Sub
    End Class
End Namespace