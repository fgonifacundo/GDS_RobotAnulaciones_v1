Imports GDS_NuevoMundoPersistencia
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function ActualizaImpresionTurboPassengerReceipt(ByVal strNombreArchivo As String, _
                                                                ByVal strNombreImpresora As String, _
                                                                ByVal strCodigoSeguimiento As String, _
                                                                ByVal intFirmaDB As Integer, _
                                                                ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = Nothing
            Try

                objDAO = New DAO
                bolRespuesta = objDAO.ActualizaImpresionTurboPassengerReceipt(strNombreArchivo, _
                                                                              strNombreImpresora, _
                                                                              strCodigoSeguimiento, _
                                                                              intFirmaDB, _
                                                                              intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strNombreArchivo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return bolRespuesta

        End Function
    End Class
End Namespace