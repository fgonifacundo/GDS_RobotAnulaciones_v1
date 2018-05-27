Imports GDS_NuevoMundoPersistencia
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function DiferenciaFecha(ByVal strFechaComparar As String, _
                                        ByVal strCodigoSeguimiento As String, _
                                        ByVal intFirmaDB As Integer, _
                                        ByVal intEsquema As Integer) As Integer

            Dim objDAO As DAO = Nothing
            Dim intRespuesta As Integer = Nothing
            Try

                objDAO = New DAO
                intRespuesta = objDAO.DiferenciaFecha(strFechaComparar, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strFechaComparar = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return intRespuesta
        End Function
        Public Function ObtenerEjecutivo(ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As List(Of classEjecutivo)

            Dim objDAO As DAO = Nothing
            Dim lstEjecutivo As List(Of classEjecutivo) = Nothing
            Try

                objDAO = New DAO
                lstEjecutivo = objDAO.ObtenerEjecutivo(strCodigoSeguimiento, _
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

            Return lstEjecutivo

        End Function

        Public Function ObtenerCorreosEnviar(ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As List(Of classEnvioCorreo)

            Dim objDAO As DAO = Nothing
            Dim objCorreosEnviar As List(Of classEnvioCorreo)
            Try

                objDAO = New DAO
                objCorreosEnviar = objDAO.ObtenerCorreosEnviar(strCodigoSeguimiento, _
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

            Return objCorreosEnviar

        End Function

        Public Function ActualizarCorreosEnviar(ByVal strIdCorreo As String, _
                                                ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False

            Try
                objDAO = New DAO

                bolRespuesta = objDAO.ActualizarCorreosEnviar(strIdCorreo, _
                                                              strCodigoSeguimiento, _
                                                              intFirmaDB, _
                                                              intEsquema)

            Catch ex As Exception
                bolRespuesta = False
                Throw New Exception(ex.ToString)

            Finally

                strIdCorreo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return bolRespuesta

        End Function

        Public Function ActualizarBoletoPax(ByVal strNumeroBoleto As String, _
                                             ByVal strIdProveedor As String, _
                                             ByVal strIdSucursal As String, _
                                             ByVal strQuienAnula As String, _
                                             ByVal strIdMotivoAnulacion As String, _
                                             ByVal strFcVoidCliente As String, _
                                             ByVal strSinRefacturaXVoid As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False

            Try
                objDAO = New DAO

                bolRespuesta = objDAO.ActualizarBoletoPax(strNumeroBoleto, _
                                                          strIdProveedor, _
                                                          strIdSucursal, _
                                                          strQuienAnula, _
                                                          strIdMotivoAnulacion, _
                                                          strFcVoidCliente, _
                                                          strSinRefacturaXVoid, _
                                                          strCodigoSeguimiento, _
                                                          intFirmaDB, _
                                                          intEsquema)

            Catch ex As Exception
                bolRespuesta = False
                Throw New Exception(ex.ToString)

            Finally
                strIdProveedor = Nothing
                strIdSucursal = Nothing
                strQuienAnula = Nothing
                strIdMotivoAnulacion = Nothing
                strFcVoidCliente = Nothing
                strSinRefacturaXVoid = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return bolRespuesta

        End Function

        Public Function InsertarTextoFile(ByVal intIdFile As Integer, _
                                          ByVal intIdSucursal As Integer, _
                                          ByVal strRenglonTexto As String, _
                                          ByVal strQuienEscribio As String, _
                                          ByVal intIdEmpresa As Integer, _
                                          ByVal strCodigoSeguimiento As String, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False

            Try

                objDAO = New DAO
                bolRespuesta = objDAO.InsertarTextoFile(intIdFile, _
                                                            intIdSucursal, _
                                                            strRenglonTexto, _
                                                            strQuienEscribio, _
                                                            intIdEmpresa, _
                                                            strCodigoSeguimiento, _
                                                            intFirmaDB, _
                                                            intEsquema)



            Catch ex As Exception
                bolRespuesta = False
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                intIdFile = Nothing
                intIdSucursal = Nothing
                strRenglonTexto = Nothing
                strQuienEscribio = Nothing
                intIdEmpresa = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return bolRespuesta

        End Function

        Public Function ObtenerDatosFacturacion(ByVal strNumeroBoleto As String, _
                                                ByVal strIdProveedor As String, _
                                                ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As List(Of classBoletoPax)

            Dim objDAO As DAO = Nothing
            Dim objBoletoPax As List(Of classBoletoPax)
            Try

                objDAO = New DAO
                objBoletoPax = objDAO.ObtenerDatosFacturacion(strNumeroBoleto, _
                                                                strIdProveedor, _
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

            Return objBoletoPax

        End Function

        Public Function ObtenerMotivosVoid(ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As List(Of classMotivoVoid)

            Dim objDAO As DAO = Nothing
            Dim objMotivoVoid As List(Of classMotivoVoid)
            Try

                objDAO = New DAO
                objMotivoVoid = objDAO.ObtenerMotivosVoid(strCodigoSeguimiento, _
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

            Return objMotivoVoid

        End Function

        Public Function ObtenerBoletosXConfirmacion(ByVal strCodigoPNR As String, _
                                                    ByVal strCodigoSeguimiento As String, _
                                                    ByVal intFirmaDB As Integer, _
                                                    ByVal intEsquema As Integer) As List(Of classBoletosXConfirmacion)

            Dim objDAO As DAO = Nothing
            Dim objBoletosXConfirmacion As List(Of classBoletosXConfirmacion)
            Try

                objDAO = New DAO
                objBoletosXConfirmacion = objDAO.ObtenerBoletosXConfirmacion(strCodigoPNR, _
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

            Return objBoletosXConfirmacion

        End Function

        Public Function ActualizarBoletoPaxVoidEMD(ByVal strNumeroBoleto As String, _
                                                   ByVal strIdProveedor As String, _
                                                   ByVal strIdSucursal As String, _
                                                   ByVal strQuienAnula As String, _
                                                   ByVal strIdMotivoAnulacion As String, _
                                                   ByVal strFcVoidCliente As String, _
                                                   ByVal strCodigoSeguimiento As String, _
                                                   ByVal intFirmaDB As Integer, _
                                                   ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False

            Try
                objDAO = New DAO

                bolRespuesta = objDAO.ActualizarBoletoPaxVoidEMD(strNumeroBoleto, _
                                                                 strIdProveedor, _
                                                                 strIdSucursal, _
                                                                 strQuienAnula, _
                                                                 strIdMotivoAnulacion, _
                                                                 strFcVoidCliente, _
                                                                 strCodigoSeguimiento, _
                                                                 intFirmaDB, _
                                                                 intEsquema)

            Catch ex As Exception
                bolRespuesta = False
                Throw New Exception(ex.ToString)

            Finally
                strIdProveedor = Nothing
                strIdSucursal = Nothing
                strQuienAnula = Nothing
                strIdMotivoAnulacion = Nothing
                strFcVoidCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return bolRespuesta

        End Function
        Public Function ObtenerTurboPassengerReceipt(ByVal strTicketNumber As String, _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As classTurboPassengerRecipt

            Dim objDAO As DAO = Nothing
            Dim objTurboPassengerRecipt As classTurboPassengerRecipt
            Try

                objDAO = New DAO
                objTurboPassengerRecipt = objDAO.ObtenerTurboPassengerReceipt(strTicketNumber, _
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

            Return objTurboPassengerRecipt

        End Function


        Public Function ObtenerTurboCcChargeForm(ByVal strTicketNumber As String, _
                                                 ByVal strCodigoSeguimiento As String, _
                                                 ByVal intFirmaDB As Integer, _
                                                 ByVal intEsquema As Integer) As classTurboCcChargeForm

            Dim objDAO As DAO = Nothing
            Dim objTurboCcChargeForm As classTurboCcChargeForm
            Try

                objDAO = New DAO
                objTurboCcChargeForm = objDAO.ObtenerTurboCcChargeForm(strTicketNumber, _
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

            Return objTurboCcChargeForm

        End Function
        Public Function ObtenerDatosClienteEmpresa(ByVal strTipo_Documento As String, _
                                                   ByVal strDocumento As String, _
                                                   ByVal strCodigoSeguimiento As String, _
                                                   ByVal intFirmaDB As Integer, _
                                                   ByVal intEsquema As Integer) As classCliente

            Dim objDAO As DAO = Nothing
            Dim objCliente As classCliente
            Try

                objDAO = New DAO
                objCliente = objDAO.ObtenerDatosClienteEmpresa(strTipo_Documento, _
                                                               strDocumento, _
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

            Return objCliente

        End Function

        Public Function ObtenerReporteConfirmaciones(ByVal strFechaInicio As String, _
                                                     ByVal strFechaFinal As String, _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As List(Of classReporteConfirmaciones)

            Dim objDAO As DAO = Nothing
            Dim objReporteConfirmaciones As List(Of classReporteConfirmaciones)
            Try

                objDAO = New DAO
                objReporteConfirmaciones = objDAO.ObtenerReporteConfirmaciones(strFechaInicio, _
                                                                               strFechaFinal, _
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

            Return objReporteConfirmaciones

        End Function

        Public Function ObtenerReporteConfirmacionesDM(ByVal strFechaInicio As String, _
                                                        ByVal strFechaFinal As String, _
                                                        ByVal strCodigoSeguimiento As String, _
                                                        ByVal intFirmaDB As Integer, _
                                                        ByVal intEsquema As Integer) As List(Of classReporteConfirmaciones)

            Dim objDAO As DAO = Nothing
            Dim objReporteConfirmaciones As List(Of classReporteConfirmaciones)
            Try

                objDAO = New DAO
                objReporteConfirmaciones = objDAO.ObtenerReporteConfirmacionesDM(strFechaInicio, _
                                                                               strFechaFinal, _
                                                                               strCodigoSeguimiento, _
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

            Return objReporteConfirmaciones

        End Function
        Public Function InsertarEnvioMensajesEA(ByVal strDesde As String, _
                                                ByVal strPara As String, _
                                                ByVal strAsunto As String, _
                                                ByVal strMensaje As String, _
                                                ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False

            Try

                objDAO = New DAO
                bolRespuesta = objDAO.InsertarEnvioMensajesEA(strDesde, _
                                                              strPara, _
                                                              strAsunto, _
                                                              strMensaje, _
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

        Public Function ObtenerDatosFacturaComision(ByVal strNumeroBoleto As String, _
                                                    ByVal strCodigoSeguimiento As String, _
                                                    ByVal intFirmaDB As Integer, _
                                                    ByVal intEsquema As Integer) As classFacturaComision
            Dim objDAO As DAO = Nothing
            Dim objFacturaComision As classFacturaComision
            Try

                objDAO = New DAO
                objFacturaComision = objDAO.ObtenerDatosFacturaComision(strNumeroBoleto, _
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

            Return objFacturaComision
        End Function

        Public Function ObtenerDatosAutorizacionNoVoid(ByVal intCliente As Integer, _
                                                       ByVal strCodReserva As String, _
                                                       ByVal strCodigoSeguimiento As String, _
                                                       ByVal intFirmaDB As Integer, _
                                                       ByVal intEsquema As Integer) As String
            Dim objDAO As DAO = Nothing
            Dim strAnula As String = Nothing
            Try

                objDAO = New DAO
                strAnula = objDAO.ObtenerDatosAutorizacionNoVoid(intCliente, _
                                                                 strCodReserva, _
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
            Return strAnula
        End Function

        Public Function ObtenerDatosAgenciaPromotorVendedor(ByVal intNumBoleto As Integer, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As classDatosAgente
            Dim objDAO As DAO = Nothing
            Dim objDatosAgente As classDatosAgente = Nothing
            Try

                objDAO = New DAO
                objDatosAgente = objDAO.ObtenerDatosAgenciaPromotorVendedor(intNumBoleto, _
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
            Return objDatosAgente
        End Function
    End Class
End Namespace