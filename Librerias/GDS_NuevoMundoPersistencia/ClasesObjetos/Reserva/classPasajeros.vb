<Serializable()> _
Public Class classPasajeros
    Private intID As Integer = -1
    Private strNumeroPasajero As String = Nothing
    Private intIdLinea As Integer = -1
    Private strNombrePasajero As String = Nothing
    Private strApellidoPaterno As String = Nothing
    Private objDocumento As List(Of classDocumento) = Nothing
    Private strTipoPasajero As String = Nothing
    Private bolMarca As Boolean = False
    Private bolInfante As Boolean = False
    Private strAdultoAsociado As String = Nothing
    Private objSSR As classSSR = Nothing
    Private objAsientos As List(Of classAsientos) = Nothing
    Public Property Marca() As Boolean
        Get
            Return bolMarca
        End Get
        Set(ByVal value As Boolean)
            bolMarca = value
        End Set
    End Property
    Public Property Infante() As Boolean
        Get
            Return bolInfante
        End Get
        Set(ByVal value As Boolean)
            bolInfante = value
        End Set
    End Property
    Public Property ID() As Integer
        Get
            Return intID
        End Get
        Set(ByVal value As Integer)
            intID = value
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
    Public Property IdLinea() As Integer
        Get
            Return intIdLinea
        End Get
        Set(ByVal value As Integer)
            intIdLinea = value
        End Set
    End Property
    Public Property Documento() As List(Of classDocumento)
        Get
            Return objDocumento
        End Get
        Set(ByVal value As List(Of classDocumento))
            objDocumento = value
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
    Public Property NombrePasajero() As String
        Get
            Return strNombrePasajero
        End Get
        Set(ByVal value As String)
            strNombrePasajero = value
        End Set
    End Property
    Public Property TipoPasajero() As String
        Get
            Return strTipoPasajero
        End Get
        Set(ByVal value As String)
            strTipoPasajero = value
        End Set
    End Property
    Public Property AdultoAsociado() As String
        Get
            Return strAdultoAsociado
        End Get
        Set(ByVal value As String)
            strAdultoAsociado = value
        End Set
    End Property
    Public Property SSR() As classSSR
        Get
            Return objSSR
        End Get
        Set(ByVal value As classSSR)
            objSSR = value
        End Set
    End Property
    Public Property Asientos() As List(Of classAsientos)
        Get
            Return objAsientos
        End Get
        Set(ByVal value As List(Of classAsientos))
            objAsientos = value
        End Set
    End Property
End Class
