Imports GDS_NuevoMundoPersistencia
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function InsertaXMLaTabla(ByVal strNombreTabla As String, _
                                        ByVal strXML As String, _
                                          ByVal strCodigoSeguimiento As String, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal intEsquema As Integer, _
                                          ByVal delete_table As Boolean, _
                                          ByVal strNameSP_delete As String) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolResultado As Boolean
            Try
                objDAO = New DAO
                bolResultado = objDAO.InsertaXMLaTabla(strNombreTabla, strXML, strCodigoSeguimiento, intFirmaDB, intEsquema, delete_table, strNameSP_delete)
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
    End Class
End Namespace
