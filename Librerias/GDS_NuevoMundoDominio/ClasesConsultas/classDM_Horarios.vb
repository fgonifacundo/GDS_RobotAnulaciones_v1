Imports GDS_NuevoMundoPersistencia
Imports GDS_NM_Mensajeria
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function ObtieneHorarioConsultaAutomatedExchange(ByVal strTipoCliente As String, _
                                                         ByVal intCodigoPermiso As Integer, _
                                                         ByVal strCodigoSeguimiento As String, _
                                                         ByVal intFirmaDB As Integer, _
                                                         ByVal intEsquema As Integer) As List(Of classDato)

            Dim objDAO As New DAO
            Dim lstRespuesta = New List(Of classDato)
            Try
                lstRespuesta = objDAO.ObtieneHorarioConsultaAutomatedExchange(strTipoCliente,
                                                                       intCodigoPermiso,
                                                                       strCodigoSeguimiento,
                                                                       intFirmaDB,
                                                                       intEsquema)
            Catch ex As Exception
                Throw New Exception(ex.ToString)
            End Try

            Return lstRespuesta

        End Function
    End Class
End Namespace