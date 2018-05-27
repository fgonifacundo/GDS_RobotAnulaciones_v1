<Serializable()> _
Public Class classMonto
    Private strMoneda As String = Nothing
    Private strCodigo As String = Nothing
    Private strMonto As String = Nothing
    Public Property Codigo() As String
        Get
            Return strCodigo
        End Get
        Set(ByVal value As String)
            strCodigo = value
        End Set
    End Property
    Public Property Moneda() As String
        Get
            Return strMoneda
        End Get
        Set(ByVal value As String)
            strMoneda = value
        End Set
    End Property
    Public Property Monto() As String
        Get
            Return strMonto
        End Get
        Set(ByVal value As String)
            strMonto = value
        End Set
    End Property
End Class
