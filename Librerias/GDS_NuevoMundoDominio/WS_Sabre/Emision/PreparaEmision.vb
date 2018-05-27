Imports GDS_NuevoMundoPersistencia
Imports IWebServices = GDS_NM_WebServicesSabre.IWebServices
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Imports objSabreWS = GDS_NM_WebServicesSabre
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Private lstMensajes As List(Of String) = Nothing
        Private lstRemarkReferencia As List(Of String) = Nothing
        Public Function PreparaEmision(ByVal objEasyOnLine As classEasyOnLine, _
                                       ByVal intEsquema As Integer) As List(Of String)

            Dim strRespuesta As String = Nothing

            Try

                If objEasyOnLine IsNot Nothing Then



                    FinalizaRecupera(objEasyOnLine.PNR, _
                                     objEasyOnLine.CodigoSegimiento, _
                                     objEasyOnLine.GDS.Codigo, _
                                     0, _
                                     2, _
                                     objEasyOnLine.Session, _
                                     True, _
                                     strRespuesta)

                    If strRespuesta <> Constantes.msgEnvioAlCounter Then

                        AsignacionImpresoras(objEasyOnLine.Reserva.Codigo, _
                                             objEasyOnLine.PseudoEmision, _
                                             objEasyOnLine.CodigoSegimiento, _
                                             objEasyOnLine.GDS.Codigo, _
                                             0, _
                                             2, _
                                             intEsquema, _
                                             objEasyOnLine.Session, _
                                             True, _
                                             strRespuesta)

                        If strRespuesta <> Constantes.msgEnvioAlCounter Then

                        End If

                    End If


                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally

            End Try
        End Function
        Public Function FinalizaRecupera(ByVal strCodigoReserva As String, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intGDS As Integer, _
                                         ByVal intFirmaGDS As Integer, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal objSession As classSession, _
                                         ByVal bolFlag As Boolean, _
                                         ByRef strRespuesta As String) As Boolean

            Dim strResultado As String = Nothing
            Dim bolRespuesta As Boolean = False
            Dim intContador As Integer = 0

            Try

Recupera:
                If intContador <= 2 Then

                    intContador += 1

                    strResultado = Finaliza_y_Recupera(strCodigoSeguimiento, _
                                                       intGDS, _
                                                       intFirmaGDS, _
                                                       intFirmaDB, _
                                                       objSession)

                    If Not String.IsNullOrEmpty(strResultado) Then
                        strResultado = Trim(strResultado)

                        If Not strResultado.Equals(Constantes.Asterisco) Then

                            If strResultado.IndexOf(strCodigoReserva) > -1 Then
                                bolRespuesta = True
                                strRespuesta = String.Empty
                            ElseIf strResultado.IndexOf(Constantes.msg_SIMULTANEOUS_CHANGES) > -1 Then
                                bolRespuesta = False
                                strRespuesta = Constantes.msgEnvioAlCounter
                            ElseIf strResultado.IndexOf(Constantes.msg_NO_CHANGES_MADE_TO_PNR_UPDATE_OR_IGNORE) > -1 Then
                                bolRespuesta = True
                                strRespuesta = String.Empty

                                EndTransaction(Constantes.FirmaMotorEmisionWeb, _
                                                Nothing, _
                                                strCodigoSeguimiento, _
                                                intGDS, _
                                                intFirmaGDS, _
                                                intFirmaDB, _
                                                objSession)

                            ElseIf strResultado.IndexOf(Constantes.msg_VERIFY_ORDER_OF_ITINERARY_SEGMENTS) > -1 Then

                                ARUNK(strCodigoSeguimiento, _
                                      intGDS, _
                                      intFirmaGDS, _
                                      intFirmaDB, _
                                      objSession)

                                EndTransaction(Constantes.FirmaMotorEmisionWeb, _
                                               Nothing, _
                                               strCodigoSeguimiento, _
                                               intGDS, _
                                               intFirmaGDS, _
                                               intFirmaDB, _
                                               objSession)


                                GoTo Recupera
                            ElseIf strResultado.IndexOf(Constantes.msg_PASSENGER_MUST_HAVE_SSR_FOID) > -1 Then
                                If bolFlag Then
                                    GoTo Recupera
                                Else
                                    bolRespuesta = False
                                    strRespuesta = Constantes.msgEnvioAlCounter
                                End If

                            ElseIf strResultado.IndexOf(Constantes.msg_SECURITY_DATA_REQUIRED) > -1 Then
                                If bolFlag Then
                                    GoTo Recupera
                                Else
                                    bolRespuesta = False
                                    strRespuesta = Constantes.msgEnvioAlCounter
                                End If

                            ElseIf strResultado.IndexOf(Constantes.msg_INFANT_DETAILS_REQUIRED_IN_SSR) > -1 Then
                                If bolFlag Then
                                    GoTo Recupera
                                Else
                                    bolRespuesta = False
                                    strRespuesta = Constantes.msgEnvioAlCounter
                                End If

                            ElseIf strResultado.IndexOf(Constantes.msg_INCORRECT_TIME_LIMIT) > -1 Then
                                GoTo Recupera

                            ElseIf strResultado.IndexOf(Constantes.msg_WARNING_EDITS) > -1 Then
                                XPG(strCodigoSeguimiento, _
                                    intGDS, _
                                    intFirmaGDS, _
                                    intFirmaDB, _
                                    objSession)

                                EndTransaction(Constantes.FirmaMotorEmisionWeb, _
                                               Nothing, _
                                               strCodigoSeguimiento, _
                                               intGDS, _
                                               intFirmaGDS, _
                                               intFirmaDB, _
                                               objSession)

                                GoTo Recupera

                            End If

                        Else
                            bolRespuesta = True
                            strRespuesta = String.Empty
                        End If

                    End If

                Else
                    bolRespuesta = False
                    strRespuesta = Constantes.msgEnvioAlCounter
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

            Return bolRespuesta

        End Function
        Public Function LeerDOCS(ByVal TravelItineraryReadRS As objSabreWS.TravelItineraryReadRQ.TravelItineraryReadRS, _
                                 ByVal objPasajeros As List(Of classPasajeros)) As List(Of classDeleteSpecialService)


            Dim lstDeleteSpecialService As List(Of classDeleteSpecialService) = Nothing
            Dim DeleteSpecialService As classDeleteSpecialService = Nothing

            Dim xNumPax As String = Nothing
            Dim xTexto As String = Nothing

            Try

                If objPasajeros IsNot Nothing Then

                    If TravelItineraryReadRS.TravelItinerary.SpecialServiceInfo IsNot Nothing Then
                        With TravelItineraryReadRS.TravelItinerary
                            For i As Integer = 0 To .SpecialServiceInfo.Length - 1
                                If .SpecialServiceInfo(i).Service IsNot Nothing Then
                                    If .SpecialServiceInfo(i).Service.SSR_Code IsNot Nothing Then
                                        If .SpecialServiceInfo(i).Service.SSR_Type IsNot Nothing Then

                                            If .SpecialServiceInfo(i).Service.SSR_Code.ToString = "SSR" And _
                                               .SpecialServiceInfo(i).Service.SSR_Type.ToString = "DOCS" Then

                                                Dim Aux1NameNumber As String = CStr(.SpecialServiceInfo(i).Service.PersonName(0).NameNumber)
                                                Dim Aux2NameNumber As String = CStr(CInt(Aux1NameNumber.Split(".")(0))) & "." & CStr(CInt(Aux1NameNumber.Split(".")(1)))


                                                For b As Integer = 0 To objPasajeros.Count - 1
                                                    If objPasajeros.Item(b).Marca = True Then

                                                        If Aux2NameNumber = objPasajeros.Item(b).NumeroPasajero Then

                                                            'Verificamos si pertenece a American Airlines
                                                            DeleteSpecialService = New classDeleteSpecialService
                                                            DeleteSpecialService.Item = CStr(CInt(.SpecialServiceInfo(i).RPH))


                                                            'DB/04AUG78/M/PANDURO/OMAR
                                                            If .SpecialServiceInfo(i).Service.Airline Is Nothing Then
                                                                DeleteSpecialService.Airline = "AA"
                                                            End If

                                                            If lstDeleteSpecialService Is Nothing Then lstDeleteSpecialService = New List(Of classDeleteSpecialService)
                                                            lstDeleteSpecialService.Add(DeleteSpecialService)

                                                        End If

                                                    End If
                                                Next

                                            End If

                                        End If
                                    End If
                                End If
                            Next
                        End With
                    End If

                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                TravelItineraryReadRS = Nothing
                objPasajeros = Nothing
                DeleteSpecialService = Nothing
                xNumPax = Nothing
                xTexto = Nothing
            End Try

            Return lstDeleteSpecialService

        End Function
        Private Sub AsignacionImpresoras(ByVal strCodigoReserva As String, _
                                         ByVal strPseudoEmisor As String, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intGDS As Integer, _
                                         ByVal intFirmaGDS As Integer, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer, _
                                         ByVal objSession As classSession, _
                                         ByVal bolIngnore As Boolean, _
                                         ByRef strRespuesta As String)


            Dim objDesignatePrinter As classDesignatePrinter = Nothing
            Dim strProFile As String = Nothing
            Dim TravelItineraryRead330 As objSabreWS.TravelItineraryReadRQ.TravelItineraryReadRS = Nothing

            Dim objDAO As New DAO

            Try

                If bolIngnore Then
                    IgnoreTransaction(strCodigoSeguimiento, _
                                      intGDS, _
                                      intFirmaGDS, _
                                      intFirmaDB, _
                                      objSession)
                End If

                strProFile = objDAO.ObtenerProFile(strPseudoEmisor, strCodigoSeguimiento, intFirmaDB, intEsquema)

                If Not String.IsNullOrEmpty(strProFile) Then
                    'SabreCommand("PPS" & strProFile, "DesignatePrinter", strCodigoSeguimiento, oGDS, objSession, oOp_Firma)
                    objDesignatePrinter = DesignatePrinter(Nothing, Nothing, strProFile, _
                                                           strCodigoSeguimiento, _
                                                           intGDS, _
                                                           intFirmaGDS, _
                                                           intFirmaDB, _
                                                           objSession)
                End If

                If objDesignatePrinter IsNot Nothing Then
                    If objDesignatePrinter.ErroresAlertas IsNot Nothing Then
                        If bolIngnore Then
                            If Not String.IsNullOrEmpty(strCodigoReserva) Then
                                TravelItineraryRead330 = TravelItineraryRead(strCodigoReserva, _
                                                                             strCodigoSeguimiento, _
                                                                             intGDS, _
                                                                             intFirmaGDS, _
                                                                             intFirmaDB, _
                                                                             objSession)
                                RecuperarLineaRemarkReferencia(TravelItineraryRead330)
                            End If
                        End If
                    Else
                        strRespuesta = Constantes.msgEnvioAlCounter
                    End If
                Else
                    strRespuesta = Constantes.msgEnvioAlCounter
                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoReserva = Nothing
                strPseudoEmisor = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing

                objSession = Nothing
                bolIngnore = Nothing

                objDesignatePrinter = Nothing
                strProFile = Nothing
                objDAO = Nothing
            End Try

        End Sub
        Private Sub BorraLineasContables(ByVal strCodigoSeguimiento As String, _
                                         ByVal intGDS As Integer, _
                                         ByVal intFirmaGDS As Integer, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal objSession As classSession, _
                                         ByVal bolIngnore As Boolean, _
                                         ByRef strRespuesta As String)

            Dim strRespuestaSabreCommand As String = Nothing

            Try

                strRespuestaSabreCommand = SabreCommand("AC" & Constantes.Change & "ALL", _
                                                        "BorraLineasContables", _
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
                bolIngnore = Nothing
                strRespuesta = Nothing
            End Try
        End Sub
        Private Sub BorraRemarkDeInterface(ByVal strCodigoReserva As String, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intGDS As Integer, _
                                           ByVal intFirmaGDS As Integer, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal objSession As classSession, _
                                           ByVal bolIngnore As Boolean, _
                                           ByRef strRespuesta As String)

            Dim strRespuestaSabreCommand As String = Nothing

            Try

                If lstRemarkReferencia IsNot Nothing Then
                    For i As Integer = 0 To lstRemarkReferencia.Count - 1
                        BorrarRemarkInterface(lstRemarkReferencia.Item(i).ToString, _
                                              strCodigoSeguimiento, _
                                              intGDS, _
                                              intFirmaGDS, _
                                              intFirmaDB, _
                                              objSession)
                    Next

                    FinalizaRecupera(strCodigoReserva, _
                                     strCodigoSeguimiento, _
                                     intGDS, _
                                     intFirmaGDS, _
                                     intFirmaDB, _
                                     objSession, _
                                     False, _
                                     strRespuesta)

                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing

                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing

                bolIngnore = Nothing
                strRespuesta = Nothing
            End Try
        End Sub
        Private Sub RecuperarLineaRemarkReferencia(ByVal TravelItineraryRead330 As objSabreWS.TravelItineraryReadRQ.TravelItineraryReadRS)
            Try

                If TravelItineraryRead330 IsNot Nothing Then
                    If TravelItineraryRead330.TravelItinerary IsNot Nothing Then
                        If TravelItineraryRead330.TravelItinerary.RemarkInfo IsNot Nothing Then


                            For i As Integer = 0 To TravelItineraryRead330.TravelItinerary.RemarkInfo.Length - 1
                                If TravelItineraryRead330.TravelItinerary.RemarkInfo(i).Type = "General" Then
                                    If TravelItineraryRead330.TravelItinerary.RemarkInfo(i).Text.Length > 4 Then
                                        If TravelItineraryRead330.TravelItinerary.RemarkInfo(i).Text.Substring(0, 3) = "X/-" Then
                                            If lstRemarkReferencia Is Nothing Then lstRemarkReferencia = New List(Of String)
                                            lstRemarkReferencia.Add(CInt(TravelItineraryRead330.TravelItinerary.RemarkInfo(i).RPH))
                                        End If
                                    End If
                                End If
                            Next


                        End If
                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                TravelItineraryRead330 = Nothing
            End Try
        End Sub

    End Class
End Namespace

