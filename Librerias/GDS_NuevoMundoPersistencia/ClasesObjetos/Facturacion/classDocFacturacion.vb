<Serializable()> _
Public Class classDocFacturacion
    Private strTipoDocumento As String = Nothing
    Private strNumDocumento As String = Nothing
    Public Property NumDocumento() As String
        Get
            Return strNumDocumento
        End Get
        Set(ByVal value As String)
            strNumDocumento = value
        End Set
    End Property
    Public Property TipoDocumento() As String
        Get
            Return strTipoDocumento
        End Get
        Set(ByVal value As String)
            strTipoDocumento = value
        End Set
    End Property
End Class
