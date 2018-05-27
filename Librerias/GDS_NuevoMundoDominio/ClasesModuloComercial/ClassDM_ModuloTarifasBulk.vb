Imports GDS_NuevoMundoPersistencia
Imports GDS_NM_Mensajeria
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports System.Text

Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function ObtieneResultadosTarifaBulkFee(ByVal strCodigoPNR As String, _
                                                       ByVal strPseudoConsulta As String, _
                                                       ByVal strAerolinea As String, _
                                                       ByVal strCiudadDestino As String, _
                                                       ByVal strCodigoSeguimiento As String, _
                                                       ByVal intFirmaDB As Integer, _
                                                       ByVal intEsquema As Integer) As classFeeTarifaBulkResultado()

            Dim objDAO As DAO = Nothing
            Dim objResultado As classFeeTarifaBulkResultado() = Nothing
            Try

                objDAO = New DAO
                objResultado = objDAO.ObtieneTarifaBulkFee(strCodigoPNR, _
                                                           strPseudoConsulta, _
                                                           strAerolinea, _
                                                           strCiudadDestino, _
                                                           strCodigoSeguimiento, _
                                                           intFirmaDB, _
                                                           intEsquema)




            Catch ex As Exception
                objResultado = Nothing
                Throw New Exception(ex.ToString)
            Finally
                strCodigoPNR = Nothing
                strPseudoConsulta = Nothing
                strAerolinea = Nothing
                strCiudadDestino = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing

                objDAO = Nothing
            End Try

            Return objResultado

        End Function
        'richard
        Public Function ObtenerConceptosTarifasBulk(ByVal intIdEmpresa As Integer, _
                                 ByVal strCiudad_Destino As String, _
                                 ByVal strTransportador As String, _
                                 ByVal strCodigoPNR As String, _
                                 ByVal intGrupo As Integer, _
                                 ByVal strCodigoSeguimiento As String, _
                                 ByVal intFirmaDB As Integer, _
                                 ByVal intEsquema As Integer) As List(Of classDato)


            Dim objDAO As DAO = Nothing
            Dim objResultado As List(Of classDato) = Nothing

            Try
                objDAO = New DAO
                If objDAO.ObtieneConceptos_TarifaBulk(intIdEmpresa, _
                                                     strCiudad_Destino, _
                                                     strCodigoPNR, _
                                                     strCodigoSeguimiento, _
                                                     intFirmaDB, _
                                                     intEsquema) Then


                    objResultado = objDAO.ObtenerConceptosTarifasBulk(strTransportador, _
                                                                      strCodigoPNR, _
                                                                      strCodigoSeguimiento, _
                                                                      intFirmaDB, _
                                                                      intEsquema)

                End If

            Catch ex As Exception
                objResultado = Nothing
                Throw New Exception(ex.ToString)
            Finally
                strTransportador = Nothing
                strCodigoPNR = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return objResultado

        End Function
        Public Function ObtenerSecuenciaPseudobulkEvaluacion(ByVal strCodigoPNR As String, _
                                                             ByVal strIdCliente As String, _
                                                             ByVal strCodigoSeguimiento As String, _
                                                             ByVal intFirmaDB As Integer, _
                                                             ByVal intEsquema As Integer) As List(Of classSecuenciaPseudobulk)

            Dim objDAO As DAO = Nothing
            Dim objSecuenciaPseudobulk As List(Of classSecuenciaPseudobulk)
            Try

                objDAO = New DAO
                objSecuenciaPseudobulk = objDAO.ObtenerSecuenciaPseudobulkEvaluacion(strCodigoPNR, _
                                                                                     strIdCliente, _
                                                                                     strCodigoSeguimiento, _
                                                                                     intFirmaDB, _
                                                                                     intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objSecuenciaPseudobulk

        End Function
        Public Function ObtenerAerolineas(ByVal strCodigoSeguimiento As String, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal intEsquema As Integer) As List(Of classAerolineaC)

            Dim objDAO As DAO = Nothing
            Dim objAerolineaC As List(Of classAerolineaC)
            Try

                objDAO = New DAO
                objAerolineaC = objDAO.ObtenerAerolineas(strCodigoSeguimiento, _
                                                         intFirmaDB, _
                                                         intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objAerolineaC

        End Function

        Public Function listarAerolineas(ByVal strCodigoSeguimiento As String, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal intEsquema As Integer) As List(Of ClsAerolinea)

            Dim objDAO As DAO = Nothing
            Dim listAerolinea As List(Of ClsAerolinea)
            Try

                objDAO = New DAO
                listAerolinea = objDAO.ListarAerolineas(strCodigoSeguimiento, _
                                                         intFirmaDB, _
                                                         intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return listAerolinea

        End Function
        Public Function EliminarTarifasBulkFq(ByVal strCodigoPNR As String, _
                                              ByVal intCliente As Integer, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim objResultado As Boolean = False
            Try

                objDAO = New DAO
                objResultado = objDAO.EliminarTarifasBulkFq(strCodigoPNR, _
                                                            intCliente, _
                                                            strCodigoSeguimiento, _
                                                            intFirmaDB, _
                                                            intEsquema)


            Catch ex As Exception
                objResultado = Nothing
                Throw New Exception(ex.ToString)
            Finally
                strCodigoPNR = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return objResultado

        End Function
        Public Function EliminarTarifasBulkCombinaciones(ByVal strCodigoPNR As String, _
                                                         ByVal intCliente As Integer, _
                                                         ByVal strCodigoSeguimiento As String, _
                                                         ByVal intFirmaDB As Integer, _
                                                         ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim objResultado As Boolean = False
            Try

                objDAO = New DAO
                objResultado = objDAO.EliminarTarifasBulkCombinaciones(strCodigoPNR, _
                                                                       intCliente, _
                                                                       strCodigoSeguimiento, _
                                                                       intFirmaDB, _
                                                                       intEsquema)


            Catch ex As Exception
                objResultado = Nothing
                Throw New Exception(ex.ToString)
            Finally
                strCodigoPNR = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return objResultado

        End Function
        Public Function ObtenerPosiblesAerolineasCC(ByVal strCodigoSeguimiento As String, _
                                                    ByVal intFirmaDB As Integer, _
                                                    ByVal intEsquema As Integer) As List(Of classAerolineaC)

            Dim objDAO As DAO = Nothing
            Dim objAerolineaC As List(Of classAerolineaC)
            Try

                objDAO = New DAO
                objAerolineaC = objDAO.ObtenerPosiblesAerolineasCC(strCodigoSeguimiento, _
                                                                   intFirmaDB, _
                                                                   intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objAerolineaC

        End Function
        Public Function ObtenerDtTarifabulkFq(ByVal strCodigoPNR As String, _
                                              ByVal strCliente As String, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As List(Of classTarifabulkFq)

            Dim objDAO As DAO = Nothing
            Dim objTarifabulkFq As List(Of classTarifabulkFq)
            Try

                objDAO = New DAO
                objTarifabulkFq = objDAO.ObtenerDtTarifabulkFq(strCodigoPNR, _
                                                               strCliente, _
                                                               strCodigoSeguimiento, _
                                                               intFirmaDB, _
                                                               intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objTarifabulkFq

        End Function
        Public Function ObtenerAerolineaTarifa(ByVal strCodigoPNR As String, _
                                               ByVal strCliente As String, _
                                               ByVal strCodigoSeguimiento As String, _
                                               ByVal intFirmaDB As Integer, _
                                               ByVal intEsquema As Integer) As List(Of String)

            Dim objDAO As DAO = Nothing
            Dim objResultado As List(Of String)
            Try

                objDAO = New DAO
                objResultado = objDAO.ObtenerAerolineaTarifa(strCodigoPNR, _
                                                             strCliente, _
                                                             strCodigoSeguimiento, _
                                                             intFirmaDB, _
                                                             intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objResultado

        End Function
        Public Function InsertarTARIFABULK_FQ(ByVal strDk As String, _
                                              ByVal strCodigoPNR As String, _
                                              ByVal strAirlines As String, _
                                              ByVal strPassengerType As String, _
                                              ByVal strFareBasis As String, _
                                              ByVal strBookingClass As String, _
                                              ByVal strCurrency As String, _
                                              ByVal douBaseFare As Double, _
                                              ByVal strExpirationDate As String, _
                                              ByVal strEffectiveDate As String, _
                                              ByVal strTicketDate As String, _
                                              ByVal strAdvancePurchase As String, _
                                              ByVal strMinStay As String, _
                                              ByVal strMaxStay As String, _
                                              ByVal strPseudo As String, _
                                              ByVal strIds As String, _
                                              ByVal strNumero As String, _
                                              ByVal douFeeMinimo As Double, _
                                              ByVal douFeeMaximo As Double, _
                                              ByVal strAccountCode As String, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False

            Try

                objDAO = New DAO
                bolRespuesta = objDAO.InsertarTARIFABULK_FQ(strDk, _
                                                            strCodigoPNR, _
                                                            strAirlines, _
                                                            strPassengerType, _
                                                            strFareBasis, _
                                                            strBookingClass, _
                                                            strCurrency, _
                                                            douBaseFare, _
                                                            strExpirationDate, _
                                                            strEffectiveDate, _
                                                            strTicketDate, _
                                                            strAdvancePurchase, _
                                                            strMaxStay, _
                                                            strMaxStay, _
                                                            strPseudo, _
                                                            strIds, _
                                                            strNumero, _
                                                            douFeeMinimo, _
                                                            douFeeMaximo, _
                                                            strAccountCode, _
                                                            strCodigoSeguimiento, _
                                                            intFirmaDB, _
                                                            intEsquema)



            Catch ex As Exception
                bolRespuesta = False
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strDk = Nothing
                strCodigoPNR = Nothing
                strAirlines = Nothing
                strPassengerType = Nothing
                strFareBasis = Nothing
                strBookingClass = Nothing
                strCurrency = Nothing
                douBaseFare = Nothing
                strExpirationDate = Nothing
                strEffectiveDate = Nothing
                strTicketDate = Nothing
                strAdvancePurchase = Nothing
                strMinStay = Nothing
                strMaxStay = Nothing
                strPseudo = Nothing
                strIds = Nothing
                strNumero = Nothing
                douFeeMinimo = Nothing
                douFeeMaximo = Nothing
                strAccountCode = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return bolRespuesta

        End Function
        Public Function ObtenerTipoPaxEspecial(ByVal strIdTipoPaxVuelo As String, _
                                               ByVal strCodigoSeguimiento As String, _
                                               ByVal intFirmaDB As Integer, _
                                               ByVal intEsquema As Integer) As classTipoPaxVuelo

            Dim objDAO As DAO = Nothing
            Dim objResultado As classTipoPaxVuelo
            Try

                objDAO = New DAO
                objResultado = objDAO.ObtenerTipoPaxEspecial(strIdTipoPaxVuelo, _
                                                             strCodigoSeguimiento, _
                                                             intFirmaDB, _
                                                             intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objResultado

        End Function
        'Public Function ObtenerTipoPaxEspecial_ALL(ByVal strCodigoSeguimiento As String, _
        '                                           ByVal intFirmaDB As Integer, _
        '                                           ByVal intEsquema As Integer) As List(Of classTipoPaxVuelo)

        '    Dim objDAO As DAO = Nothing
        '    Try
        '        objDAO = New DAO
        '        Return objDAO.ObtenerTipoPaxEspecial_ALL(strCodigoSeguimiento, _
        '                                                        intFirmaDB, _
        '                                                        intEsquema)
        '    Catch ex As Exception
        '        Throw New Exception(ex.ToString)
        '    Finally
        '        objDAO = Nothing
        '        strCodigoSeguimiento = Nothing
        '        intFirmaDB = Nothing
        '        intEsquema = Nothing
        '    End Try
        'End Function
        Public Function ComparaTipoPaxEspecial(ByVal strIdTipoPaxVuelo As String, _
                                               ByVal strCodigoSeguimiento As String, _
                                               ByVal intFirmaDB As Integer, _
                                               ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim objResultado As Boolean
            Try

                objDAO = New DAO
                objResultado = objDAO.ComparaTipoPaxEspecial(strIdTipoPaxVuelo, _
                                                             strCodigoSeguimiento, _
                                                             intFirmaDB, _
                                                             intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objResultado
        End Function
#Region "Pseudos"
        Public Function ObtenerTourCodesConceptos(ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As List(Of classTourCodesConceptos)

            Dim objDAO As DAO = Nothing
            Dim objTourCodesConceptos As List(Of classTourCodesConceptos)
            Try

                objDAO = New DAO
                objTourCodesConceptos = objDAO.ObtenerTourCodesConceptos(strCodigoSeguimiento, _
                                                                         intFirmaDB, _
                                                                         intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.Source.ToString & " " & ex.Message.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objTourCodesConceptos

        End Function
        Public Function SP_NEW_PseudosBulkEvaluacion(ByVal strAerolinea As String, _
                                                        ByVal strCiudadDestino As String, _
                                                        ByVal strCodigoPNR As String, _
                                                        ByVal strIdCliente As Integer, _
                                                        ByVal strPseudo As String, _
                                                        ByVal strCodigoSeguimiento As String, _
                                                        ByVal intFirmaDB As Integer, _
                                                        ByVal intEsquema As Integer) As List(Of classPseudosBulkEvaluacion)

            Dim objDAO As DAO = Nothing
            Dim objPseudosBulkEvaluacion As List(Of classPseudosBulkEvaluacion)
            Try

                objDAO = New DAO
                objPseudosBulkEvaluacion = objDAO.SP_NEW_PseudosBulkEvaluacion(strAerolinea, _
                                                                        strCiudadDestino, _
                                                                        strCodigoPNR, _
                                                                        strIdCliente, _
                                                                        strPseudo, _
                                                                        strCodigoSeguimiento, _
                                                                        intFirmaDB, _
                                                                        intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objPseudosBulkEvaluacion
        End Function
        Public Function InsertarPseudosBulkEvaluacion(ByVal strCodigoPNR As String, _
                                                        ByVal strIdCliente As String, _
                                                        ByVal lstTourCodesConceptos As List(Of classTourCodesConceptos), _
                                                        ByVal strCodigoSeguimiento As String, _
                                                        ByVal intFirmaDB As Integer, _
                                                        ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False

            Try

                objDAO = New DAO
                bolRespuesta = objDAO.InsertarPseudosBulkEvaluacion(strCodigoPNR, _
                                                            strIdCliente, _
                                                            lstTourCodesConceptos, _
                                                            strCodigoSeguimiento, _
                                                            intFirmaDB, _
                                                            intEsquema)

            Catch ex As Exception
                bolRespuesta = False
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return bolRespuesta

        End Function
        Public Function EliminarPseudosBulkConceptos(ByVal strCodigoPNR As String, _
                                                     ByVal strAerolinea As String, _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim objResultado As Boolean = False
            Try
                objDAO = New DAO
                objResultado = objDAO.EliminarPseudosBulkConceptos(strCodigoPNR, _
                                                                   strAerolinea, _
                                                                   strCodigoSeguimiento, _
                                                                   intFirmaDB, _
                                                                   intEsquema)
            Catch ex As Exception
                objResultado = Nothing
                Throw New Exception(ex.ToString)
            Finally
                strCodigoPNR = Nothing
                strAerolinea = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return objResultado

        End Function
        Public Function EliminarPseudosBulkEvaluacion(ByVal strCodigoPNR As String, _
                                                      ByVal strIdCliente As String, _
                                                      ByVal strCodigoSeguimiento As String, _
                                                      ByVal intFirmaDB As Integer, _
                                                      ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim objResultado As Boolean = False
            Try

                objDAO = New DAO
                objResultado = objDAO.EliminarPseudosBulkEvaluacion(strCodigoPNR, _
                                                                    strIdCliente, _
                                                                    strCodigoSeguimiento, _
                                                                    intFirmaDB, _
                                                                    intEsquema)
            Catch ex As Exception
                objResultado = Nothing
                Throw New Exception(ex.ToString)
            Finally
                strCodigoPNR = Nothing
                strIdCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return objResultado
        End Function
        Public Function ObtenerPseudosbulkConceptos(ByVal strCodigoPNR As String, _
                                                    ByVal strCodigoSeguimiento As String, _
                                                    ByVal intFirmaDB As Integer, _
                                                    ByVal intEsquema As Integer) As List(Of classPseudoBulkConceptos)

            Dim objDAO As DAO = Nothing
            Dim objPseudobulkConceptos As List(Of classPseudoBulkConceptos)
            Try

                objDAO = New DAO
                objPseudobulkConceptos = objDAO.ObtenerPseudosbulkConceptos(strCodigoPNR, _
                                                                            strCodigoSeguimiento, _
                                                                            intFirmaDB, _
                                                                            intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objPseudobulkConceptos

        End Function
#End Region
#Region "TarifasBulkHugo"
        Public Function ObtenerConceptosTarifasBulk_HS(ByVal strTransportador As String, _
                                                       ByVal strCiudadDestino As String, _
                                                       ByVal strCodigoPnr As String, _
                                                       ByVal strCodigoSeguimiento As String, _
                                                       ByVal intFirmaDB As Integer, _
                                                       ByVal intEsquema As Integer) As List(Of classPseudoBulkConceptos)

            Dim objDAO As DAO = Nothing
            Dim objPseudobulkConceptos As List(Of classPseudoBulkConceptos) = Nothing

            Try
                objDAO = New DAO

                objPseudobulkConceptos = objDAO.ObtenerConceptosTarifasBulk_HS(strTransportador, _
                                                                               strCiudadDestino, _
                                                                               strCodigoPnr, _
                                                                               strCodigoSeguimiento, _
                                                                               intFirmaDB, _
                                                                               intEsquema)

            Catch ex As Exception

                If ex.ToString.Contains("Error interno de proveedor de datos") Then
                    objPseudobulkConceptos = Nothing
                Else
                    Throw New Exception(ex.ToString)
                End If
            Finally
                strTransportador = Nothing
                strCiudadDestino = Nothing
                strCodigoPnr = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return objPseudobulkConceptos

        End Function
        Public Function InsertarConceptosTarifasBulk(ByVal strCodigoPNR As String, _
                                                     ByVal strPseudoConsulta As String, _
                                                     ByVal strAerolinea As String, _
                                                     ByVal strCiudadDestino As String, _
                                                     ByVal lstTourCodesConceptos As List(Of classTourCodesConceptos), _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As classFeeTarifaBulkResultado()

            Dim objDAO As DAO = Nothing
            Dim objFeeTarifaBulkResultado As classFeeTarifaBulkResultado() = Nothing

            Try

                objDAO = New DAO
                objFeeTarifaBulkResultado = objDAO.InsertarConceptosTarifasBulk(strCodigoPNR, _
                                                                                strPseudoConsulta, _
                                                                                strAerolinea, _
                                                                                strCiudadDestino, _
                                                                                lstTourCodesConceptos, _
                                                                                strCodigoSeguimiento, _
                                                                                intFirmaDB, _
                                                                                intEsquema)

            Catch ex As Exception
                objFeeTarifaBulkResultado = Nothing
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objFeeTarifaBulkResultado
        End Function
        Public Function BorrarTarifaBulkEvaluacion(ByVal strCodigoPNR As String, _
                                                   ByVal strIdCliente As String, _
                                                   ByVal strCodigoSeguimiento As String, _
                                                   ByVal intFirmaDB As Integer, _
                                                   ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim objResultado As Boolean = False
            Try

                objDAO = New DAO
                objResultado = objDAO.BorrarTarifaBulkEvaluacion(strCodigoPNR, _
                                                                 strCodigoSeguimiento, _
                                                                 intFirmaDB, _
                                                                 intEsquema)
            Catch ex As Exception
                objResultado = Nothing
                Throw New Exception(ex.ToString)
            Finally
                strCodigoPNR = Nothing
                strIdCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return objResultado
        End Function
        Public Function BorrarTarifaBulkConceptos(ByVal strCodigoPNR As String, _
                                                  ByVal strID_Transportador As String, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim objResultado As Boolean = False
            Try

                objDAO = New DAO
                objResultado = objDAO.BorrarTarifaBulkConceptos(strCodigoPNR, _
                                                                strID_Transportador, _
                                                                strCodigoSeguimiento, _
                                                                intFirmaDB, _
                                                                intEsquema)
            Catch ex As Exception
                objResultado = Nothing
                Throw New Exception(ex.ToString)
            Finally
                strCodigoPNR = Nothing
                strID_Transportador = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return objResultado
        End Function
        Public Function ListaTarifasPromocionales(ByVal strPseudo As String, _
                                                   ByVal strCodigoSeguimiento As String, _
                                                   ByVal intFirmaDB As Integer, _
                                                   ByVal intEsquema As Integer) As List(Of ClsTarifasMain_ODD)

            Dim objDAO As DAO = Nothing
            Dim listaReporteTarifa As List(Of ClsTarifasMain_ODD)
            Try
                objDAO = New DAO
                listaReporteTarifa = objDAO.ListaTarifasPromocionales(strPseudo, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try
            Return listaReporteTarifa
        End Function
        Public Function ODD_InsertarItin_Ind(ByVal id_ciudad_or As String, _
                                     ByVal id_ciudad_des As String, _
                                     ByVal strAirlines As String, _
                                     ByVal strpseudo_reg As String, _
                                     ByVal strCodigoSeguimiento As String, _
                                     ByVal intFirmaDB As Integer, _
                                     ByVal intEsquema As Integer) As Boolean
            Dim objDAO As DAO = Nothing
            Dim bolResultado As Boolean
            Try
                objDAO = New DAO
                bolResultado = objDAO.ODD_InsertarItin_Ind(id_ciudad_or, id_ciudad_des, strAirlines, strpseudo_reg, strCodigoSeguimiento, intFirmaDB, intEsquema)
            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try
            Return bolResultado
        End Function
        Public Function ODD_ListarItinerario(ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As List(Of ClsItinerario_ODD)
            Dim objDAO As DAO = Nothing
            Dim listaItinerario As List(Of ClsItinerario_ODD)
            Try
                objDAO = New DAO
                listaItinerario = objDAO.ODD_ListarItinerario(strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try
            Return listaItinerario
        End Function
        Public Function ODD_CargarExcel_ODD(ByVal strXML As String, _
                                          ByVal strCodigoSeguimiento As String, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal intEsquema As Integer) As Boolean
            Dim objDAO As DAO = Nothing
            Dim bolResultado As Boolean
            Try
                objDAO = New DAO
                bolResultado = objDAO.ODD_CargarExcel_ODD(strXML, strCodigoSeguimiento, intFirmaDB, intEsquema)
            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try
            Return bolResultado
        End Function
        Public Function ODD_DeleteItinerario_ALL(ByVal strCodigoSeguimiento As String, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal intEsquema As Integer) As Boolean
            Dim objDAO As DAO = Nothing
            Dim bolResultado As Boolean
            Try
                objDAO = New DAO
                bolResultado = objDAO.ODD_DeleteItinerario_ALL(strCodigoSeguimiento, intFirmaDB, intEsquema)
            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try
            Return bolResultado
        End Function

        Public Function ODD_ObtenerTipoPax(ByVal strCodigoSeguimiento As String, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal intEsquema As Integer) As List(Of classTipoPaxVuelo)
            Dim objDAO As DAO = Nothing
            Try
                objDAO = New DAO
                Return objDAO.ODD_ObtenerTipoPax(strCodigoSeguimiento, intFirmaDB, intEsquema)
            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try
        End Function
#End Region
    End Class
End Namespace