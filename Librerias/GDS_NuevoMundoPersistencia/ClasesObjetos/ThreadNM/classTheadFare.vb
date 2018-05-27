Option Explicit On
Option Strict On
<Serializable()> _
Public Class classTheadFare
    Private strOrigen As String = String.Empty
    Private strDestino As String = String.Empty
    Private strFechaSalida As String = String.Empty
    Private strFechaRetorno As String = String.Empty
    Private strOrientacion As String = String.Empty
    Private strAerolinea As String = String.Empty
    Private strTipoTarifa As String = String.Empty
    Private strTipoPasajero As String = String.Empty

    Private intID As Integer = 0
    Private strPseudo As String = String.Empty
    Private strCodigoSeguimiento As String = String.Empty
    Private intGDS As Integer = 0
    Private intFirmaGDS As Integer = 0
    Private intFirmaDB As Integer = 0
    Private intEsquema As Integer = 0

    Private objSession As classSession = Nothing
    Private objFareRS As Object = Nothing
    Private objFeeTarifaBulkResultado As classFeeTarifaBulkResultado() = Nothing
    Public Property Origen() As String
        Get
            Return strOrigen
        End Get
        Set(ByVal value As String)
            strOrigen = value
        End Set
    End Property
    Public Property Destino() As String
        Get
            Return strDestino
        End Get
        Set(ByVal value As String)
            strDestino = value
        End Set
    End Property
    Public Property FechaSalida() As String
        Get
            Return strFechaSalida
        End Get
        Set(ByVal value As String)
            strFechaSalida = value
        End Set
    End Property
    Public Property FechaRetorno() As String
        Get
            Return strFechaRetorno
        End Get
        Set(ByVal value As String)
            strFechaRetorno = value
        End Set
    End Property
    Public Property Orientacion() As String
        Get
            Return strOrientacion
        End Get
        Set(ByVal value As String)
            strOrientacion = value
        End Set
    End Property
    Public Property Aerolinea() As String
        Get
            Return strAerolinea
        End Get
        Set(ByVal value As String)
            strAerolinea = value
        End Set
    End Property
    Public Property TipoTarifa() As String
        Get
            Return strTipoTarifa
        End Get
        Set(ByVal value As String)
            strTipoTarifa = value
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



    Public Property ID() As Integer
        Get
            Return intID
        End Get
        Set(ByVal value As Integer)
            intID = value
        End Set
    End Property
    Public Property Pseudo() As String
        Get
            Return strPseudo
        End Get
        Set(ByVal value As String)
            strPseudo = value
        End Set
    End Property
    Public Property CodigoSeguimiento() As String
        Get
            Return strCodigoSeguimiento
        End Get
        Set(ByVal value As String)
            strCodigoSeguimiento = value
        End Set
    End Property
    Public Property GDS() As Integer
        Get
            Return intGDS
        End Get
        Set(ByVal value As Integer)
            intGDS = value
        End Set
    End Property
    Public Property FirmaGDS() As Integer
        Get
            Return intFirmaGDS
        End Get
        Set(ByVal value As Integer)
            intFirmaGDS = value
        End Set
    End Property
    Public Property FirmaDB() As Integer
        Get
            Return intFirmaDB
        End Get
        Set(ByVal value As Integer)
            intFirmaDB = value
        End Set
    End Property
    Public Property Esquema() As Integer
        Get
            Return intEsquema
        End Get
        Set(ByVal value As Integer)
            intEsquema = value
        End Set
    End Property

    Public Property Session() As classSession
        Get
            Return objSession
        End Get
        Set(ByVal value As classSession)
            objSession = value
        End Set
    End Property
    Public Property FareRS() As Object
        Get
            Return objFareRS
        End Get
        Set(ByVal value As Object)
            objFareRS = value
        End Set
    End Property
    Public Property FeeTarifaBulkResultado() As classFeeTarifaBulkResultado()
        Get
            Return objFeeTarifaBulkResultado
        End Get
        Set(ByVal value As classFeeTarifaBulkResultado())
            objFeeTarifaBulkResultado = value
        End Set
    End Property


End Class
