<Serializable()> _
Public Class classDeposito_
    Private intID As Integer = -1
    Private intTipoOperacion As Integer = -1
    Private intBanco As Integer = -1
    Private strSucursal As String = Nothing
    Private strOperacion As String = Nothing
    Private strReferencia As String = Nothing
    Private strImporte As String = "0.00"
    Private strFecha As String = Nothing
    Private strHora As String = Nothing
    Private strRuta As String = Nothing
    Private strPeso As String = "0"
    <NonSerialized()> _
    Private objFileUpload As Object = Nothing
    Public Property ID() As Integer
        Get
            Return intID
        End Get
        Set(ByVal value As Integer)
            intID = value
        End Set
    End Property
    Public Property TipoOperacion() As Integer
        Get
            Return intTipoOperacion
        End Get
        Set(ByVal value As Integer)
            intTipoOperacion = value
        End Set
    End Property
    Public Property Banco() As Integer
        Get
            Return intBanco
        End Get
        Set(ByVal value As Integer)
            intBanco = value
        End Set
    End Property
    Public Property Sucursal() As String
        Get
            Return strSucursal
        End Get
        Set(ByVal value As String)
            strSucursal = value
        End Set
    End Property
    Public Property Operacion() As String
        Get
            Return strOperacion
        End Get
        Set(ByVal value As String)
            strOperacion = value
        End Set
    End Property
    Public Property Referencia() As String
        Get
            Return strReferencia
        End Get
        Set(ByVal value As String)
            strReferencia = value
        End Set
    End Property
    Public Property Importe() As String
        Get
            Return strImporte
        End Get
        Set(ByVal value As String)
            strImporte = value
        End Set
    End Property
    Public Property Fecha() As String
        Get
            Return strFecha
        End Get
        Set(ByVal value As String)
            strFecha = value
        End Set
    End Property
    Public Property Hora() As String
        Get
            Return strHora
        End Get
        Set(ByVal value As String)
            strHora = value
        End Set
    End Property
    Public Property Ruta() As String
        Get
            Return strRuta
        End Get
        Set(ByVal value As String)
            strRuta = value
        End Set
    End Property
    Public Property Peso() As String
        Get
            Return strPeso
        End Get
        Set(ByVal value As String)
            strPeso = value
        End Set
    End Property
    Public Property FileUpload() As Object
        Get
            Return objFileUpload
        End Get
        Set(ByVal value As Object)
            objFileUpload = value
        End Set
    End Property
End Class
