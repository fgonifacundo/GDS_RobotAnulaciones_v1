<Serializable()> _
Public Class classBoletoEmitido
    Private intIDSesuenciaPax As Integer = 0
    Private strNumeroPasajero As String = Nothing
    Private strPrefijoBoleto As String = Nothing
    Private strCodigoAerolinea As String = Nothing
    Private strNumeroBoleto As String = Nothing
    Private bolEnConexion As Boolean = False
    Private intIDCliente As Integer = 0
    Private strIDProveedor As String = Nothing
    Private strIDSucursal As String = Nothing
    Private strIDVendedor As String = Nothing
    Private strCodigoPNR As String = Nothing
    Private dblDeuda As Double = Nothing
    Public Property IDSesuenciaPax() As Integer
        Get
            Return intIDSesuenciaPax
        End Get
        Set(ByVal value As Integer)
            intIDSesuenciaPax = value
        End Set
    End Property
    Public Property NumeroPasajero() As String
        Get
            Return strNumeroPasajero
        End Get
        Set(ByVal value As String)
            strNumeroPasajero = value
        End Set
    End Property
    Public Property PrefijoBoleto() As String
        Get
            Return strPrefijoBoleto
        End Get
        Set(ByVal value As String)
            strPrefijoBoleto = value
        End Set
    End Property
    Public Property CodigoAerolinea() As String
        Get
            Return strCodigoAerolinea
        End Get
        Set(ByVal value As String)
            strCodigoAerolinea = value
        End Set
    End Property
    Public Property NumeroBoleto() As String
        Get
            Return strNumeroBoleto
        End Get
        Set(ByVal value As String)
            strNumeroBoleto = value
        End Set
    End Property
    Public Property EnConexion() As Boolean
        Get
            Return bolEnConexion
        End Get
        Set(ByVal value As Boolean)
            bolEnConexion = value
        End Set
    End Property

    Public Property IDCliente() As Integer
        Get
            Return intIDCliente
        End Get
        Set(ByVal value As Integer)
            intIDCliente = value
        End Set
    End Property

    Public Property IDProveedor() As String
        Get
            Return strIDProveedor
        End Get
        Set(ByVal value As String)
            strIDProveedor = value
        End Set
    End Property

    Public Property IDSucursal() As String
        Get
            Return strIDSucursal
        End Get
        Set(ByVal value As String)
            strIDSucursal = value
        End Set
    End Property

    Public Property IDVendedor() As String
        Get
            Return strIDVendedor
        End Get
        Set(ByVal value As String)
            strIDVendedor = value
        End Set
    End Property

    Public Property CodigoPNR() As String
        Get
            Return strCodigoPNR
        End Get
        Set(value As String)
            strCodigoPNR = value
        End Set
    End Property
    Public Property Deuda() As Double
        Get
            Return dblDeuda
        End Get
        Set(value As Double)
            dblDeuda = value
        End Set
    End Property

End Class
