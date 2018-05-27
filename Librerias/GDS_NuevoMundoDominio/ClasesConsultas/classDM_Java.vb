Imports GDS_NuevoMundoPersistencia
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function ObtieneTokenJava(ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As classSession

            Dim objDAO As DAO = Nothing
            Dim objRespuesta As classSession = Nothing
            Try

                objDAO = New DAO
                objRespuesta = objDAO.ObtieneTokenJava(strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objRespuesta

        End Function
        Public Function ObtenerControlMorosidad(ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As List(Of classControlMorosidad)

            Dim objDAO As DAO = Nothing
            Dim objRespuesta As List(Of classControlMorosidad) = Nothing
            Try

                objDAO = New DAO
                objRespuesta = objDAO.ObtenerControlMorosidad(strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objRespuesta

        End Function
        Public Function EliminarCuentaMorosa(ByVal strNumeroBoleto As String, _
                                             ByVal strIdCliente As String, _
                                             ByVal strInicialesCounter As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As String

            Dim objDAO As DAO = Nothing
            Dim objRespuesta As String = Nothing

            Try

                objDAO = New DAO
                objRespuesta = objDAO.EliminarCuentaMorosa(strNumeroBoleto, _
                                                           strIdCliente, _
                                                           strInicialesCounter, _
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

            Return objRespuesta
        End Function

        Public Function ObtenerCorreoEjecutivoCobranza(ByVal strIdCliente As String, _
                                                       ByVal strCodigoSeguimiento As String, _
                                                       ByVal intFirmaDB As Integer, _
                                                       ByVal intEsquema As Integer) As classDato

            Dim objDAO As DAO = Nothing
            Dim objRespuesta As classDato = Nothing
            Try

                objDAO = New DAO
                objRespuesta = objDAO.ObtenerCorreoEjecutivoCobranza(strIdCliente, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objRespuesta

        End Function

        Public Function ConsultarCCCF(ByVal strPnr As String,
                             ByVal strBoleto As String,
                             ByVal strCodigoSeguimiento As String,
                             ByVal intFirmaDB As Integer, _
                             ByVal intEsquema As Integer) As Integer
            Dim intCodigo As New Integer
            Try
                Dim objAD As New DAO
                intCodigo = objAD.ConsultarExisteCCCF(strPnr, strBoleto, strCodigoSeguimiento, intFirmaDB, intEsquema)
            Catch ex As Exception
                intCodigo = 0
                Throw New Exception(ex.ToString)
            Finally
                strPnr = Nothing
                strBoleto = Nothing
                strCodigoSeguimiento = Nothing
            End Try
            Return intCodigo
        End Function
    End Class
End Namespace