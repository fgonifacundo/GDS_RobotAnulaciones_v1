<Serializable()> _
Public Class classSucursalPunto
    Private intSucursal As Integer = -1
    Private intPunto As Integer = -1
    Private strNombre As String = Nothing
    Private strEmailCaja As String = Nothing
    Private strPseudoCity As String = Nothing

    Public Property Punto() As Integer
        Get
            Return intPunto
        End Get
        Set(ByVal value As Integer)
            intPunto = value
        End Set
    End Property
    Public Property Sucursal() As Integer
        Get
            Return intSucursal
        End Get
        Set(ByVal value As Integer)
            intSucursal = value
        End Set
    End Property
    Public Property Nombre() As String
        Get
            Return strNombre
        End Get
        Set(ByVal value As String)
            strNombre = value
        End Set
    End Property
    Public Property EmailCaja() As String
        Get
            Return strEmailCaja
        End Get
        Set(ByVal value As String)
            strEmailCaja = value
        End Set
    End Property

    Public Property PseudoCity() As String
        Get
            Return strPseudoCity
        End Get
        Set(ByVal value As String)
            strPseudoCity = value
        End Set
    End Property
End Class
