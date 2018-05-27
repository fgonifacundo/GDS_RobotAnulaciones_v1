Imports GDS_NuevoMundoPersistencia
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function ObtenerWebDatosSucursalPunto(ByVal intSucursal As Integer, _
                                                     ByVal intPunto As Integer, _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As classSucursalPunto



            Dim objDAO As DAO = Nothing
            Dim objSucursalPunto As classSucursalPunto = Nothing
            Try
                objDAO = New DAO
                objSucursalPunto = objDAO.ObtenerWebDatosSucursalPunto(intSucursal, intPunto, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                intSucursal = Nothing
                intPunto = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objSucursalPunto

        End Function
        Public Function ObtenerGDSInterface(ByVal intAplicacion As Integer, _
                                            ByVal intOrigen As Integer, _
                                            ByVal intCliente As Integer, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As classGDS_Interface


            Dim objDAO As DAO = Nothing
            Dim objGDS_Interface As classGDS_Interface = Nothing
            Try

                objDAO = New DAO
                objGDS_Interface = objDAO.ObtenerGDSInterface(intAplicacion, intOrigen, intCliente, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                intAplicacion = Nothing
                intOrigen = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objGDS_Interface

        End Function
        Public Function ObtenerDocTarjeta(ByVal strCodigoSeguimiento As String, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal intEsquema As Integer) As List(Of classDocumento)

            Dim objDAO As DAO = Nothing
            Dim objDocTarjeta As List(Of classDocumento) = Nothing
            Try

                objDAO = New DAO
                objDocTarjeta = objDAO.ObtenerDocTarjeta(strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objDocTarjeta

        End Function
        Public Function ObtenerDsctoExta(ByVal strPNR As String, _
                                         ByVal intCliente As Integer, _
                                         ByVal strAerolinea As String, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As classDsctExtra

            Dim objDAO As DAO = Nothing
            Dim objDsctExtra As classDsctExtra = Nothing
            Try

                objDAO = Nothing
                objDsctExtra = objDAO.ObtenerDsctoExta(strPNR, intCliente, strAerolinea, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strPNR = Nothing
                intCliente = Nothing
                strAerolinea = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objDsctExtra

        End Function
        Public Function ObtenerTipoPasajero(ByVal strTipoTarifa As String, _
                                            ByVal strAerolineas As String, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As List(Of classDatosTipoPasajero)


            Dim objDAO As DAO = Nothing
            Dim objDatosTipoPasajero As List(Of classDatosTipoPasajero) = Nothing
            Try

                objDAO = New DAO
                objDatosTipoPasajero = objDAO.ObtenerTipoPasajero(strTipoTarifa, _
                                                                    strAerolineas, _
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

            Return objDatosTipoPasajero

        End Function
        Public Function ObtenerDocumentoEmision(ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As List(Of classTipoDocumentoEmision)


            Dim objDAO As DAO = Nothing
            Dim objTipoDocumentoEmision As List(Of classTipoDocumentoEmision) = Nothing
            Try

                objDAO = New DAO
                objTipoDocumentoEmision = objDAO.ObtenerDocumentoEmision(strCodigoSeguimiento, _
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

            Return objTipoDocumentoEmision

        End Function
        Public Function ObtenerFormaPago(ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As List(Of classDato)


            Dim objDAO As DAO = Nothing
            Dim objFormaPago As List(Of classDato) = Nothing
            Try

                objDAO = New DAO
                objFormaPago = objDAO.ObtenerFormaPago(strCodigoSeguimiento, _
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

            Return objFormaPago

        End Function
        Public Function ObtenerOperaciones(ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As List(Of classDato)


            Dim objDAO As DAO = Nothing
            Dim objFormaPago As List(Of classDato) = Nothing
            Try

                objDAO = New DAO
                objFormaPago = objDAO.ObtenerOperaciones(strCodigoSeguimiento, _
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

            Return objFormaPago

        End Function
        Public Function ObtenerCuentaBancaria(ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As List(Of classCuentaBancaria)


            Dim objDAO As DAO = Nothing
            Dim objCuentaBancaria As List(Of classCuentaBancaria) = Nothing
            Try

                objDAO = New DAO
                objCuentaBancaria = objDAO.ObtenerCuentaBancaria(strCodigoSeguimiento, _
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

            Return objCuentaBancaria

        End Function
        Public Function ObtenerTipoTarifa(ByVal strCodigoSeguimiento As String, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal intEsquema As Integer) As List(Of classDato)


            Dim objDAO As DAO = Nothing
            Dim objTipoTarifa As List(Of classDato) = Nothing

            Try

                objDAO = New DAO
                objTipoTarifa = objDAO.ObtenerTipoTarifa(strCodigoSeguimiento, _
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

            Return objTipoTarifa

        End Function
        Public Function ObtenerTipoReserva(ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As List(Of classDato)


            Dim objDAO As DAO = Nothing
            Dim objTipoReserva As List(Of classDato) = Nothing

            Try

                objDAO = New DAO
                objTipoReserva = objDAO.ObtenerTipoReserva(strCodigoSeguimiento, _
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

            Return objTipoReserva

        End Function
        Public Function ObtenerFeeOpt(ByVal intCliente As Integer, _
                                      ByVal strPNR As String, _
                                      ByVal strCodigoSeguimiento As String, _
                                      ByVal intFirmaDB As Integer, _
                                      ByVal intEsquema As Integer) As classFeeOpt.FeeOpt


            Dim objDAO As DAO = Nothing
            Dim objFeeOpt As classFeeOpt.FeeOpt = Nothing
            Dim Suma As Double = 0.0

            Try

                objDAO = New DAO
                objFeeOpt = objDAO.ObtenerFeeOPT(intCliente, _
                                                 strPNR, _
                                                 strCodigoSeguimiento, _
                                                 intFirmaDB, _
                                                 intEsquema)


                If objFeeOpt IsNot Nothing Then
                    If objFeeOpt.FeeCallCenter IsNot Nothing Then
                        For i As Integer = 0 To objFeeOpt.FeeCallCenter.Count - 1
                            If objFeeOpt.FeeCallCenter.Item(i).Monto IsNot Nothing Then
                                Suma += CDbl(objFeeOpt.FeeCallCenter.Item(i).Monto.Monto())
                            End If
                        Next

                        objFeeOpt.TotalFeeOpt = Format(Suma, "####.00")
                    End If
                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objFeeOpt

        End Function
        Public Function ObtenerTipoTarjeta(ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As List(Of classDocumento)

            Dim objDAO As DAO = Nothing
            Dim lstDocumento As List(Of classDocumento) = Nothing

            Try

                objDAO = New DAO
                lstDocumento = objDAO.ObtenerTipoTarjeta(strCodigoSeguimiento, _
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

            Return lstDocumento

        End Function
        Public Function ObtenerTURBO_PuntoFacturacion(ByVal strPCC_AAA As String, _
                                                      ByVal strPCC_Firma As String, _
                                                      ByVal strCodigoSeguimiento As String, _
                                                      ByVal intFirmaDB As Integer, _
                                                      ByVal intEsquema As Integer) As List(Of classSucursalPunto)

            Dim objDAO As DAO = Nothing
            Dim lstSucursalPunto As List(Of classSucursalPunto) = Nothing

            Try

                objDAO = New DAO
                lstSucursalPunto = objDAO.ObtenerTURBO_PuntoFacturacion(strPCC_AAA, _
                                                                        strPCC_Firma, _
                                                                        strCodigoSeguimiento, _
                                                                        intFirmaDB, _
                                                                        intEsquema)



            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strPCC_AAA = Nothing
                strPCC_Firma = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return lstSucursalPunto

        End Function
        Public Function ObtenerTURBO_DatosPax(ByVal strNUMERO_DOCUMENTO As String, _
                                              ByVal strTIPO_DOCUMENTO As String, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As classTurboDatosPax

            Dim objDAO As DAO = Nothing
            Dim objPasajeros As classTurboDatosPax = Nothing

            Try

                objDAO = New DAO
                objPasajeros = objDAO.ObtenerTURBO_DatosPax(strNUMERO_DOCUMENTO, _
                                                            strTIPO_DOCUMENTO, _
                                                            strCodigoSeguimiento, _
                                                            intFirmaDB, _
                                                            intEsquema)



            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strNUMERO_DOCUMENTO = Nothing
                strTIPO_DOCUMENTO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objPasajeros

        End Function
        Public Function ObtenerTURBO_EmisionSolicitud(ByVal strFiltro As String, _
                                                      ByVal strCodigoSeguimiento As String, _
                                                      ByVal intFirmaDB As Integer, _
                                                      ByVal intEsquema As Integer) As List(Of classDato)

            Dim objDAO As DAO = Nothing
            Dim lstDatos As List(Of classDato) = Nothing

            Try

                objDAO = New DAO
                lstDatos = objDAO.ObtenerTURBO_EmisionSolicitud(strFiltro, _
                                                                strCodigoSeguimiento, _
                                                                intFirmaDB, _
                                                                intEsquema)



            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strFiltro = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return lstDatos

        End Function
        Public Function InsertaTURBO_DatosPax(ByVal objTurboDatosPax As classTurboDatosPax, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False

            Try

                objDAO = New DAO
                bolRespuesta = objDAO.InsertaTURBO_DatosPax(objTurboDatosPax, _
                                                            strCodigoSeguimiento, _
                                                            intFirmaDB, _
                                                            intEsquema)



            Catch ex As Exception
                bolRespuesta = False
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                objTurboDatosPax = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return bolRespuesta

        End Function

        Public Function Inserta_Interface_General(ByVal objIG As classInterfaceGeneral, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False

            Try

                objDAO = New DAO
                bolRespuesta = objDAO.Inserta_Interface_General(objIG, _
                                                                strCodigoSeguimiento, _
                                                                intFirmaDB, _
                                                                intEsquema)



            Catch ex As Exception
                bolRespuesta = False
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                objIG = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return bolRespuesta

        End Function

        Public Function Inserta_Interface_Detalle(ByVal objID As classInterfaceDetalle, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False

            Try

                objDAO = New DAO
                bolRespuesta = objDAO.Inserta_Interface_Detalle(objID, _
                                                                strCodigoSeguimiento, _
                                                                intFirmaDB, _
                                                                intEsquema)



            Catch ex As Exception
                bolRespuesta = False
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                objID = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return bolRespuesta

        End Function

        Public Function Obtener_Referencia_Secuencia(ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As String

            Dim objDAO As DAO = Nothing
            Dim strRpta As String = ""

            Try

                objDAO = New DAO
                strRpta = objDAO.ObtenerSecuenciaReferencia(strCodigoSeguimiento, _
                                                            intFirmaDB, _
                                                            intEsquema)

            Catch ex As Exception
                strRpta = ""
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return strRpta

        End Function

        Public Function Obtener_Sucursal_XPseudo(ByVal Pseudo As String, ByVal strCodigoSeguimiento As String, _
                                                 ByVal intFirmaDB As Integer, _
                                                 ByVal intEsquema As Integer) As String

            Dim objDAO As DAO = Nothing
            Dim strRpta As String = ""

            Try

                objDAO = New DAO
                strRpta = objDAO.ObtenerSucursalXPseudo(Pseudo, _
                                                        strCodigoSeguimiento, _
                                                        intFirmaDB, _
                                                        intEsquema)

            Catch ex As Exception
                strRpta = ""
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return strRpta

        End Function
        Public Function ObtenerPseudo(ByVal strID_TRANSPORTADOR As String, _
                                      ByVal strPseudoOriginal As String, _
                                      ByVal strCodigoSeguimiento As String, _
                                      ByVal intFirmaDB As Integer, _
                                      ByVal intEsquema As Integer) As String
            Dim objDAO As DAO = Nothing
            Dim LSTPSEUDO As String = Nothing
            Try
                objDAO = New DAO
                LSTPSEUDO = objDAO.ObtenerPseudo(strID_TRANSPORTADOR, strPseudoOriginal, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strID_TRANSPORTADOR = Nothing
                strPseudoOriginal = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing

            End Try

            Return LSTPSEUDO
        End Function

        Public Function ObtenerIataPorPseudo(ByVal strPseudo As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As String
            Dim objDAO As DAO = Nothing
            Dim STRIATA As String = Nothing
            Try
                objDAO = New DAO
                STRIATA = objDAO.ObtenerIataPorPseudo(strPseudo, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                intFirmaDB = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing

            End Try

            Return STRIATA
        End Function

        Public Function ObtenerStockBoleto(ByVal strPseudoConsulta As String, _
                                           ByVal strPseudoEmision As String, _
                                           ByVal intIdWeb As Integer, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As String
            Dim objDAO As DAO = Nothing
            Dim STRPERFIL As String = Nothing
            Try
                objDAO = New DAO
                STRPERFIL = objDAO.ObtenerStockBoleto(strPseudoConsulta, strPseudoEmision, intIdWeb, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                intFirmaDB = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing

            End Try

            Return STRPERFIL
        End Function


        Public Function ObtenerPerfilImpresoraPseudo(ByVal strPseudo As String, _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As String
            Dim objDAO As DAO = Nothing
            Dim STRPERFIL As String = Nothing
            Try
                objDAO = New DAO
                STRPERFIL = objDAO.ObtenerPerfilImpresoraPseudo(strPseudo, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                intFirmaDB = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing

            End Try

            Return STRPERFIL
        End Function
#Region "BD_WEB"
        Public Function ObtenerWebSucursalPunto(ByVal intUsuarioWeb As Integer, _
                                                ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As classSucursalPunto


            Dim objDAO As DAO = Nothing
            Dim objSucursalPunto As classSucursalPunto = Nothing
            Try
                objDAO = New DAO
                objSucursalPunto = objDAO.ObtenerWebSucursalPunto(intUsuarioWeb, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                intUsuarioWeb = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objSucursalPunto

        End Function
        Public Function ObtenerHorariosWeb(ByVal strCondicionCliente As String, _
                                           ByVal intTipoConsulta As Integer, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As String


            Dim objDAO As DAO = Nothing
            Dim strRespuesta As String = Nothing
            Try
                objDAO = New DAO
                strRespuesta = objDAO.ObtenerHorariosWeb(strCondicionCliente, _
                                                         intTipoConsulta, _
                                                         strCodigoSeguimiento, _
                                                         intFirmaDB, _
                                                         intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCondicionCliente = Nothing
                intTipoConsulta = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return strRespuesta

        End Function
#End Region
    End Class
End Namespace