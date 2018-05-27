Imports GDS_NuevoMundoPersistencia
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function ObtenerDatosPseudo(ByVal strCodigoSeguimiento As String, _
                                           ByVal intGDS As Integer, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As List(Of classDato)

            Dim objDAO As DAO = Nothing
            Dim objResultado As List(Of classDato) = Nothing
            Try
                objDAO = New DAO
                objResultado = objDAO.ObtenerDatosPseudo(strCodigoSeguimiento, intGDS, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objResultado

        End Function
        Public Function ObtenerExistePseudoNM(ByVal srtPseudo As String, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As Integer

            Dim objDAO As DAO = Nothing
            Dim intCantidad As Integer = 0

            Try
                objDAO = New DAO
                intCantidad = objDAO.ObtenerExistePseudoNM(srtPseudo, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                srtPseudo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return intCantidad

        End Function

        Public Function ObtenerExistePseudoNMundo(ByVal srtPseudo As String, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As Integer

            Dim objDAO As DAO = Nothing
            Dim intCantidad As Integer = 0

            Try
                objDAO = New DAO
                intCantidad = objDAO.ObtenerExistePseudoNMundo(srtPseudo, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                srtPseudo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return intCantidad

        End Function
    End Class
End Namespace