<Serializable()> _
Public Class classTurboDatosPax
    Private strIdTipoDocumento As String = Nothing
    Private strNumeroDocumento As String = Nothing
    Private strNombrePasajero As String = Nothing
    Private strApellidoPaterno As String = Nothing
    Private strApellidoMarterno As String = Nothing
    Private strDireccion As String = Nothing
    Private strTelefonoCasa As String = Nothing
    Private strTelefonoCelular As String = Nothing
    Public Property IdTipoDocumento() As String
        Get
            Return strIdTipoDocumento
        End Get
        Set(ByVal value As String)
            strIdTipoDocumento = value
        End Set
    End Property
    Public Property NumeroDocumento() As String
        Get
            Return strNumeroDocumento
        End Get
        Set(ByVal value As String)
            strNumeroDocumento = value
        End Set
    End Property
    Public Property NombrePasajero() As String
        Get
            Return strNombrePasajero
        End Get
        Set(ByVal value As String)
            strNombrePasajero = value
        End Set
    End Property
    Public Property ApellidoPaterno() As String
        Get
            Return strApellidoPaterno
        End Get
        Set(ByVal value As String)
            strApellidoPaterno = value
        End Set
    End Property
    Public Property ApellidoMarterno() As String
        Get
            Return strApellidoMarterno
        End Get
        Set(ByVal value As String)
            strApellidoMarterno = value
        End Set
    End Property
    Public Property Direccion() As String
        Get
            Return strDireccion
        End Get
        Set(ByVal value As String)
            strDireccion = value
        End Set
    End Property
    Public Property TelefonoCasa() As String
        Get
            Return strTelefonoCasa
        End Get
        Set(ByVal value As String)
            strTelefonoCasa = value
        End Set
    End Property
    Public Property TelefonoCelular() As String
        Get
            Return strTelefonoCelular
        End Get
        Set(ByVal value As String)
            strTelefonoCelular = value
        End Set
    End Property
End Class
