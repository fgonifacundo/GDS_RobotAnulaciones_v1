<Serializable()> _
Public Class classSSR
    Private strGenero As String = Nothing
    Private strPasaporte As String = Nothing
    Private strNacPasaporte As String = Nothing
    Private strNacPasajero As String = Nothing
    Private strFechaNacimiento As String = Nothing
    Private strFechaExpedicion As String = Nothing
    Private strFechaExpiracion As String = Nothing
    Public Property Genero() As String
        Get
            Return strGenero
        End Get
        Set(ByVal value As String)
            strGenero = value
        End Set
    End Property
    Public Property Pasaporte() As String
        Get
            Return strPasaporte
        End Get
        Set(ByVal value As String)
            strPasaporte = value
        End Set
    End Property
    Public Property NacPasaporte() As String
        Get
            Return strNacPasaporte
        End Get
        Set(ByVal value As String)
            strNacPasaporte = value
        End Set
    End Property
    Public Property NacPasajero() As String
        Get
            Return strNacPasajero
        End Get
        Set(ByVal value As String)
            strNacPasajero = value
        End Set
    End Property
    Public Property FechaNacimiento() As String
        Get
            Return strFechaNacimiento
        End Get
        Set(ByVal value As String)
            strFechaNacimiento = value
        End Set
    End Property
    Public Property FechaExpedicion() As String
        Get
            Return strFechaExpedicion
        End Get
        Set(ByVal value As String)
            strFechaExpedicion = value
        End Set
    End Property
    Public Property FechaExpiracion() As String
        Get
            Return strFechaExpiracion
        End Get
        Set(ByVal value As String)
            strFechaExpiracion = value
        End Set
    End Property
End Class
