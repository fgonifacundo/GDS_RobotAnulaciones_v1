<Serializable()> _
Public Class classDsctExtra
    Private strDescuento As String = Nothing
    Private intCodigoAut As Integer = -1
    Private strNombreAut As String = Nothing
    Public Property Descuento() As String
        Get
            Return strDescuento
        End Get
        Set(ByVal value As String)
            strDescuento = value
        End Set
    End Property
    Public Property CodigoAut() As Integer
        Get
            Return intCodigoAut
        End Get
        Set(ByVal value As Integer)
            intCodigoAut = value
        End Set
    End Property
    Public Property NombreAut() As String
        Get
            Return strNombreAut
        End Get
        Set(ByVal value As String)
            strNombreAut = value
        End Set
    End Property
End Class
