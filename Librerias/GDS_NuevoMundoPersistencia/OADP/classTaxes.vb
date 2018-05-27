<Serializable()> _
Public Class classTaxes
    Private strPaid As String = String.Empty
    Private strMonto As String = String.Empty
    Private strCodigo As String = String.Empty
    Public Property Paid() As String
        Get
            Return strPaid
        End Get
        Set(ByVal value As String)
            strPaid = value
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
    Public Property Codigo() As String
        Get
            Return strCodigo
        End Get
        Set(ByVal value As String)
            strCodigo = value
        End Set
    End Property
End Class
