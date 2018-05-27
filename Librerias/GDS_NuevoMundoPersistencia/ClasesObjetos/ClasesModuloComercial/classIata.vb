<Serializable()> _
Public Class classIata
    Private strIata As String = Nothing
    Private intSucursal As Integer = -1
    Private strNombreIata As String = String.Empty
    Private strCiudadIata As String = String.Empty
    Private strPaisIata As String = String.Empty
    Public Property Iata() As String
        Get
            Return strIata
        End Get
        Set(ByVal value As String)
            strIata = value
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
    Public Property NombreIata() As String
        Get
            Return strNombreIata
        End Get
        Set(ByVal value As String)
            strNombreIata = value
        End Set
    End Property
    Public Property CiudadIata() As String
        Get
            Return strCiudadIata
        End Get
        Set(ByVal value As String)
            strCiudadIata = value
        End Set
    End Property
    Public Property PaisIata() As String
        Get
            Return strPaisIata
        End Get
        Set(ByVal value As String)
            strPaisIata = value
        End Set
    End Property
End Class
