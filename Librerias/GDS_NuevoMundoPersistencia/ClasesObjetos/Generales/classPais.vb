<Serializable()> _
Public Class classPais
    Private strCodigo As String = Nothing
    Private strValor As String = Nothing
    Public Property Codigo() As String
        Get
            Return strCodigo
        End Get
        Set(ByVal value As String)
            strCodigo = value
        End Set
    End Property
    Public Property Valor() As String
        Get
            Return strValor
        End Get
        Set(ByVal value As String)
            strValor = value
        End Set
    End Property
End Class
