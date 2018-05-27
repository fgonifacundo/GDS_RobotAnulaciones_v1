<Serializable()> _
Public Class classDocTarjeta
    Private objDocumento As List(Of classDocumento) = Nothing
    Public Property Documento() As List(Of classDocumento)
        Get
            Return objDocumento
        End Get
        Set(ByVal value As List(Of classDocumento))
            objDocumento = value
        End Set
    End Property
End Class
