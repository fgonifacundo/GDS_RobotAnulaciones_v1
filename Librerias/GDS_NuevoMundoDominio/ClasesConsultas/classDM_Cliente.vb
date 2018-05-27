Imports GDS_NuevoMundoPersistencia
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function ObtieneLineaCredito(ByVal intCliente As Integer, _
                                            ByVal intIdEmpresa As Integer, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As classLineaCredito

            Dim objDAO As DAO = Nothing
            Dim objLineaCredito As classLineaCredito = Nothing
            Try

                objDAO = New DAO
                objLineaCredito = objDAO.ObtenerLineaCredito(intCliente, intIdEmpresa, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.Source.ToString & " " & ex.Message.ToString)
            Finally
                objDAO = Nothing
                intCliente = Nothing
                intIdEmpresa = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objLineaCredito

        End Function
        Public Function ObtenerDocumentosVencidos(ByVal intCliente As Integer, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As classDocumentosVencidos


            Dim objDAO As DAO = Nothing
            Dim objDocumentosVencidos As classDocumentosVencidos = Nothing
            Dim TotalVencidos As Decimal = 0.0
            Dim TotalEmitido As Decimal = 0.0

            Try

                objDAO = New DAO
                objDocumentosVencidos = objDAO.ObtenerDocumentosVencidos(intCliente, strCodigoSeguimiento, intFirmaDB, intEsquema)

                If objDocumentosVencidos.DocumentosEmitidos IsNot Nothing Then
                    For Each items As classDocumentosVencidos.classDocumentosEmitidos In objDocumentosVencidos.DocumentosEmitidos
                        TotalVencidos += CDbl(items.Pendiente)
                        TotalEmitido += CDbl(items.Total)
                    Next
                    objDocumentosVencidos.TotalEmision = TotalEmitido.ToString("##0.00")
                    objDocumentosVencidos.TotalVencidos = TotalVencidos.ToString("##0.00")
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                TotalVencidos = Nothing
                TotalEmitido = Nothing
            End Try

            Return objDocumentosVencidos

        End Function
        Public Function ObtenerPermisos(ByVal intCliente As Integer, _
                                        ByVal intUsuarioWeb As Integer, _
                                        ByVal strCodigoSeguimiento As String, _
                                        ByVal intFirmaDB As Integer, _
                                        ByVal intEsquema As Integer) As classPermisos


            Dim objDAO As DAO = Nothing
            Dim objPermisos As classPermisos = Nothing
            Try

                objDAO = New DAO
                objPermisos = objDAO.ObtenerPermisos(intCliente, intUsuarioWeb, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                intCliente = Nothing
                intUsuarioWeb = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objPermisos

        End Function
        Public Function ObtenerDatosCliente(ByVal intCliente As Integer, _
                                            ByVal strDepartamento As String, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As classCliente


            Dim objDAO As DAO = Nothing
            Dim objCliente As classCliente = Nothing
            Try

                objDAO = New DAO
                objCliente = objDAO.ObtenerDatosCliente(intCliente, strDepartamento, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                intCliente = Nothing
                strDepartamento = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objCliente

        End Function
        Public Function ObtenerEmailEmisionEasyOnLine(ByVal strDK As String, _
                                                      ByVal strPNR As String, _
                                                      ByVal strNumeroBoleto As String, _
                                                      ByVal strNumeroBoletoFULL As String, _
                                                      ByVal strDepartamento As String, _
                                                      ByVal strCodigoSeguimiento As String, _
                                                      ByVal intFirmaDB As Integer, _
                                                      ByVal intEsquema As Integer) As String


            Dim objDAO As DAO = Nothing
            Dim strEmail As String = Nothing
            Try

                objDAO = New DAO
                strEmail = objDAO.ObtenerEmailEmisionEasyOnLine(strDK, _
                                                                strPNR, _
                                                                strNumeroBoleto, _
                                                                strNumeroBoletoFULL, _
                                                                strDepartamento, _
                                                                strCodigoSeguimiento, _
                                                                intFirmaDB, _
                                                                intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strDK = Nothing
                strPNR = Nothing
                strNumeroBoleto = Nothing
                strNumeroBoletoFULL = Nothing
                strCodigoSeguimiento = Nothing
                strDepartamento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return strEmail

        End Function
        Public Function ObtenerSubCodigo(ByVal intCliente As Integer, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As List(Of classSubCodigo)

            Dim objDAO As DAO = Nothing
            Dim objSubCodigo As List(Of classSubCodigo) = Nothing
            Try

                objDAO = New DAO
                objSubCodigo = objDAO.ObtenerSubCodigo(intCliente, _
                                                       strCodigoSeguimiento, _
                                                       intFirmaDB, _
                                                       intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objSubCodigo

        End Function
        Public Function ObtenerGrupoInterno(ByVal intCliente As Integer, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As classGrupoInterno


            Dim objDAO As DAO = Nothing
            Dim objGrupoInterno As classGrupoInterno = Nothing
            Try

                objDAO = New DAO
                objGrupoInterno = objDAO.ObtenerGrupoInterno(intCliente, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objGrupoInterno

        End Function
        Public Function ObtenerTipoCliente(ByVal intCliente As Integer, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                        ByVal intEsquema As Integer) As classCliente
            Dim objDAO As DAO = Nothing
            Dim objCliente As classCliente = Nothing
            Try

                objDAO = New DAO
                objCliente = objDAO.ObtenerTipoCliente(intCliente, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objCliente
        End Function
    End Class
End Namespace