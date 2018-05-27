Imports GDS_NuevoMundoPersistencia
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function ObtieneBoletoDWLIST(ByVal objDWLIST As classDWLIST, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False

            Try
                objDAO = New DAO

                bolRespuesta = objDAO.ObtieneBoletoDWLIST(objDWLIST.PSEUDO, _
                                                          objDWLIST.ITEM, _
                                                          objDWLIST.TIPO, _
                                                          objDWLIST.PNR, _
                                                          objDWLIST.DK, _
                                                          objDWLIST.INDICADOR, _
                                                          objDWLIST.FECHA, _
                                                          objDWLIST.NUM_TICKET, _
                                                          strCodigoSeguimiento, _
                                                          intFirmaDB, _
                                                          intEsquema)

            Catch ex As Exception
                bolRespuesta = False
                Throw New Exception(ex.ToString)
            Finally
                objDWLIST = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return bolRespuesta

        End Function
        Public Function ObtieneListadoDWLIST(ByVal strFiltro As String, _
                                             ByVal strFecha As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As List(Of classDWLIST)

            Dim objDAO As DAO = Nothing
            Dim objDWLIST As List(Of classDWLIST) = Nothing

            Try
                objDAO = New DAO

                objDWLIST = objDAO.ObtieneListadoDWLIST(strFiltro, _
                                                        strFecha, _
                                                        strCodigoSeguimiento, _
                                                        intFirmaDB, _
                                                        intEsquema)

            Catch ex As Exception
                objDWLIST = Nothing
                Throw New Exception(ex.ToString)
            Finally
                strFiltro = Nothing
                strFecha = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return objDWLIST

        End Function
        Public Function ActualizaMarcaGDS_DWLIST_OADP(ByVal objDWLIST As classDWLIST, _
                                                      ByVal strCampo As String, _
                                                      ByVal intValor As Integer, _
                                                      ByVal strCodigoSeguimiento As String, _
                                                      ByVal intFirmaDB As Integer, _
                                                      ByVal intEsquema As Integer) As Boolean


            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False
            Try

                objDAO = New DAO
                bolRespuesta = objDAO.ActualizaMarcaGDS_DWLIST_OADP(objDWLIST, _
                                                                    strCampo, _
                                                                    intValor, _
                                                                    strCodigoSeguimiento, _
                                                                    intFirmaDB, _
                                                                    intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDWLIST = Nothing
                strCampo = Nothing
                intValor = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return bolRespuesta

        End Function
        Public Function VerificaRegistroDWLIST(ByVal strPseudo As String, _
                                               ByVal strDocument As String, _
                                               ByVal strTipo As String, _
                                               ByVal strPNR As String, _
                                               ByVal strCodigoCliente As String, _
                                               ByVal strIndicador As String, _
                                               ByVal strFecha As Date, _
                                               ByVal strCodigoSeguimiento As String, _
                                               ByVal intFirmaDB As Integer, _
                                               ByVal intEsquema As Integer) As Integer


            Dim objDAO As DAO = Nothing
            Dim intRespuesta As Integer = 0

            Try
                objDAO = New DAO

                intRespuesta = objDAO.VerificaRegistroDWLIST(strPseudo, _
                                                                      strDocument, _
                                                                     strTipo, _
                                                                     strPNR, _
                                                                     strCodigoCliente, _
                                                                     strIndicador, _
                                                                     strFecha, _
                                                                     strCodigoSeguimiento, _
                                                                     intFirmaDB, _
                                                                     intEsquema)

            Catch ex As Exception
                intRespuesta = 0
                Throw New Exception(ex.ToString)
            Finally
                strPseudo = Nothing
                strDocument = Nothing
                strTipo = Nothing
                strPNR = Nothing
                strCodigoCliente = Nothing
                strIndicador = Nothing
                strFecha = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return intRespuesta

        End Function
        Public Function VerificaTURBO_PASSENGER_RECEIPT(ByVal strTicketNumber As String, _
                                                        ByVal strPnrCode As String, _
                                                        ByVal strDkNumber As String, _
                                                        ByVal strCodigoSeguimiento As String, _
                                                        ByVal intFirmaDB As Integer, _
                                                        ByVal intEsquema As Integer) As Integer


            Dim objDAO As DAO = Nothing
            Dim intRespuesta As Integer = 0

            Try
                objDAO = New DAO

                intRespuesta = objDAO.VerificaTURBO_PASSENGER_RECEIPT(strTicketNumber, _
                                                                      strPnrCode, _
                                                                      CLng(strDkNumber), _
                                                                      strCodigoSeguimiento, _
                                                                      intFirmaDB, _
                                                                      intEsquema)

            Catch ex As Exception
                intRespuesta = 0
                Throw New Exception(ex.ToString)
            Finally
                strTicketNumber = Nothing
                strPnrCode = Nothing
                strDkNumber = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return intRespuesta

        End Function
        Public Function ExisteTURBO_PASSENGER_RECEIPT(ByVal strTicketNumber As String, _
                                                      ByVal strPnrCode As String, _
                                                      ByVal strDkNumber As String, _
                                                      ByVal strCodigoSeguimiento As String, _
                                                      ByVal intFirmaDB As Integer, _
                                                      ByVal intEsquema As Integer) As Integer


            Dim objDAO As DAO = Nothing
            Dim intRespuesta As Integer = 0

            Try
                objDAO = New DAO

                intRespuesta = objDAO.SinDocumentoTURBO_PASSENGER_RECEIPT(strTicketNumber, _
                                                                          strPnrCode, _
                                                                          CLng(strDkNumber), _
                                                                          strCodigoSeguimiento, _
                                                                          intFirmaDB, _
                                                                          intEsquema)

            Catch ex As Exception
                intRespuesta = 0
                Throw New Exception(ex.ToString)
            Finally
                strTicketNumber = Nothing
                strPnrCode = Nothing
                strDkNumber = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return intRespuesta

        End Function
        Public Function VerificaTURBO_CC_CHARGE_FORM(ByVal strTicketNumber As String, _
                                                     ByVal strPnrCode As String, _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As Integer


            Dim objDAO As DAO = Nothing
            Dim intRespuesta As Integer = 0

            Try
                objDAO = New DAO

                intRespuesta = objDAO.VerificaTURBO_CC_CHARGE_FORM(strTicketNumber, _
                                                                   strPnrCode, _
                                                                   strCodigoSeguimiento, _
                                                                   intFirmaDB, _
                                                                   intEsquema)

            Catch ex As Exception
                intRespuesta = 0
                Throw New Exception(ex.ToString)
            Finally
                strTicketNumber = Nothing
                strPnrCode = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return intRespuesta

        End Function
        Public Function InsertaTURBO_PASSENGER_RECEIPT(ByVal strTicketNumber As String, _
                                                       ByVal strPnrCode As String, _
                                                       ByVal strDkNumber As String, _
                                                       ByVal strRucNumber As String, _
                                                       ByVal strPcc As String, _
                                                       ByVal strCounterTA As String, _
                                                       ByVal strCuerpoDocumento As String, _
                                                       ByVal strPasajeroNombre As String, _
                                                       ByVal strPasajeroApellido As String, _
                                                       ByVal intIdHeader As Integer, _
                                                       ByVal strCounterEmail As String, _
                                                       ByVal strFreqTravel As String, _
                                                       ByVal strRuta As String, _
                                                       ByVal strCodigoSeguimiento As String, _
                                                       ByVal intFirmaDB As Integer, _
                                                       ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False

            Try
                objDAO = New DAO

                bolRespuesta = objDAO.InsertaTURBO_PASSENGER_RECEIPT(strTicketNumber, _
                                                                     strPnrCode, _
                                                                     CLng(strDkNumber), _
                                                                     strRucNumber, _
                                                                     strPcc, _
                                                                     strCounterTA, _
                                                                     strCuerpoDocumento, _
                                                                     strPasajeroNombre, _
                                                                     strPasajeroApellido, _
                                                                     intIdHeader, _
                                                                     strCounterEmail, _
                                                                     strFreqTravel, _
                                                                     strRuta, _
                                                                     strCodigoSeguimiento, _
                                                                     intFirmaDB, _
                                                                     intEsquema)

            Catch ex As Exception
                bolRespuesta = False
                Throw New Exception(ex.ToString)
            Finally
                strTicketNumber = Nothing
                strPnrCode = Nothing
                strDkNumber = Nothing
                strRucNumber = Nothing
                strPcc = Nothing
                strCounterTA = Nothing
                strCuerpoDocumento = Nothing
                strPasajeroNombre = Nothing
                strPasajeroApellido = Nothing
                intIdHeader = Nothing
                strCounterEmail = Nothing
                strFreqTravel = Nothing
                strRuta = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return bolRespuesta

        End Function
        Public Function ActualizaTURBO_PASSENGER_RECEIPT(ByVal strTicketNumber As String, _
                                                         ByVal strPnrCode As String, _
                                                         ByVal strDkNumber As String, _
                                                         ByVal strCuerpoDocumento As String, _
                                                         ByVal strRuta As String, _
                                                         ByVal strCodigoSeguimiento As String, _
                                                         ByVal intFirmaDB As Integer, _
                                                         ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False

            Try
                objDAO = New DAO

                bolRespuesta = objDAO.ActualizaTURBO_PASSENGER_RECEIPT(strTicketNumber, _
                                                                       strPnrCode, _
                                                                       CLng(strDkNumber), _
                                                                       strCuerpoDocumento, _
                                                                       strRuta, _
                                                                       strCodigoSeguimiento, _
                                                                       intFirmaDB, _
                                                                       intEsquema)

            Catch ex As Exception
                bolRespuesta = False
                Throw New Exception(ex.ToString)
            Finally
                strTicketNumber = Nothing
                strPnrCode = Nothing
                strDkNumber = Nothing
                strCodigoSeguimiento = Nothing
                strCuerpoDocumento = Nothing
                strRuta = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return bolRespuesta

        End Function
        Public Function TPR_BUSCAR(ByVal strPnr As String, _
                                   ByVal strDK As String, _
                                   ByVal strTicketNumber As String, _
                                   ByVal strOpcion As String, _
                                   ByVal strCodigoSeguimiento As String, _
                                   ByVal intFirmaDB As Integer, _
                                   ByVal intEsquema As Integer) As List(Of classTurboPassengerRecipt)

            Dim objDAO As DAO = Nothing
            Dim lstTurboPassengerRecipt As List(Of classTurboPassengerRecipt) = Nothing

            Try
                objDAO = New DAO

                lstTurboPassengerRecipt = objDAO.TPR_BUSCAR(strPnr, _
                                                            strDK, _
                                                            strTicketNumber, _
                                                            strOpcion, _
                                                            strCodigoSeguimiento, _
                                                            intFirmaDB, _
                                                            intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
                strPnr = Nothing
                strDK = Nothing
                strTicketNumber = Nothing
                strOpcion = Nothing
            End Try

            Return lstTurboPassengerRecipt

        End Function
        Public Function TPR_ACTUALIZA_IMPRESION(ByVal strPnr As String, _
                                                ByVal strDK As String, _
                                                ByVal strTicketNumber As String, _
                                                ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As Integer

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False
            Dim intRespuesta As Integer = 0

            Try
                objDAO = New DAO

                bolRespuesta = objDAO.TPR_ACTUALIZA_IMPRESION(strPnr, _
                                                              strDK, _
                                                              strTicketNumber, _
                                                              strCodigoSeguimiento, _
                                                              intFirmaDB, _
                                                              intEsquema)

                If bolRespuesta Then
                    intRespuesta = 1
                Else
                    intRespuesta = 0
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
                strPnr = Nothing
                strDK = Nothing
                strTicketNumber = Nothing
                bolRespuesta = Nothing
            End Try

            Return intRespuesta

        End Function
        Public Function TPR_ACTUALIZA_CORREO(ByVal strPnr As String, _
                                             ByVal strDK As String, _
                                             ByVal strTicketNumber As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As Integer

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False
            Dim intRespuesta As Integer = 0

            Try
                objDAO = New DAO

                bolRespuesta = objDAO.TPR_ACTUALIZA_CORREO(strPnr, _
                                                           strDK, _
                                                           strTicketNumber, _
                                                           strCodigoSeguimiento, _
                                                           intFirmaDB, _
                                                           intEsquema)

                If bolRespuesta Then
                    intRespuesta = 1
                Else
                    intRespuesta = 0
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
                strPnr = Nothing
                strDK = Nothing
                strTicketNumber = Nothing
                bolRespuesta = Nothing
            End Try

            Return intRespuesta

        End Function

        Public Function InsertaGDS_DWLIST_OADP(ByVal objDWLIST As classDWLIST, _
                                               ByVal strCodigoSeguimiento As String, _
                                               ByVal intFirmaDB As Integer, _
                                               ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False
            Dim intRespuesta As Integer = 0

            Try

                objDAO = New DAO

                bolRespuesta = objDAO.InsertaGDS_DWLIST_OADP(objDWLIST, _
                                                             strCodigoSeguimiento, _
                                                             intFirmaDB, _
                                                             intEsquema)

                If bolRespuesta Then
                    intRespuesta = 1
                Else
                    intRespuesta = 0
                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                bolRespuesta = Nothing
                objDWLIST = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return intRespuesta

        End Function
        Public Function SinDocumentoTURBO_PASSENGER_RECEIPT(ByVal strTicketNumber As String, _
                                                            ByVal strPnrCode As String, _
                                                            ByVal strDkNumber As String, _
                                                            ByVal strCodigoSeguimiento As String, _
                                                            ByVal intFirmaDB As Integer, _
                                                            ByVal intEsquema As Integer) As Integer

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False
            Dim intRespuesta As Integer = 0

            Try

                objDAO = New DAO

                bolRespuesta = objDAO.SinDocumentoTURBO_PASSENGER_RECEIPT(strTicketNumber, _
                                                                          strPnrCode, _
                                                                          strDkNumber, _
                                                                          strCodigoSeguimiento, _
                                                                          intFirmaDB, _
                                                                          intEsquema)

                If bolRespuesta Then
                    intRespuesta = 1
                Else
                    intRespuesta = 0
                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strTicketNumber = Nothing
                strPnrCode = Nothing
                strDkNumber = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
                bolRespuesta = False
            End Try

            Return intRespuesta

        End Function
    End Class
End Namespace