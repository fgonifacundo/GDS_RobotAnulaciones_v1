Imports GDS_NuevoMundoPersistencia
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function InsertaSolicitudReemision(ByVal objSolicitudReemision As classSolicitudReemision, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As Integer

            Dim objDAO As DAO = Nothing
            Dim intRespuesta As Integer = Nothing

            Try

                objDAO = New DAO
                intRespuesta = objDAO.InsertaSolicitudReemision(objSolicitudReemision, strCodigoSeguimiento, intFirmaDB, intEsquema)


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objSolicitudReemision = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return intRespuesta
        End Function
        Public Function ObtieneSolicitudReemision(ByVal intCodigoReemision As Integer, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As classSolicitudReemision

            Dim objDAO As DAO = Nothing
            Dim objSolicitudReemision As classSolicitudReemision = Nothing

            Try

                objDAO = New DAO
                objSolicitudReemision = objDAO.ObtieneSolicitudReemision(intCodigoReemision, strCodigoSeguimiento, intFirmaDB, intEsquema)


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objSolicitudReemision
        End Function
        Public Function ObtieneMensajeIGVAutomatedExchange(ByVal srtAerolinea As String, _
                                                           ByVal strCodigoSeguimiento As String, _
                                                           ByVal intFirmaDB As Integer, _
                                                           ByVal intEsquema As Integer) As Integer

            Dim objDAO As DAO = Nothing
            Dim intRespuesta As Integer = 0

            Try

                objDAO = New DAO
                intRespuesta = objDAO.ObtieneMensajeIGVAutomatedExchange(srtAerolinea, strCodigoSeguimiento, intFirmaDB, intEsquema)


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                srtAerolinea = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return intRespuesta
        End Function
    End Class
End Namespace
