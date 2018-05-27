<Serializable()> _
Public Class classPNR
    Private lstSegmentos As List(Of classSegmentos)
    Private strCodigo As String = Nothing
    Private strPseudoHome As String = Nothing
    Private strPseudoRelease As String = Nothing
    Private strPseudoAAA As String = Nothing
    Private strFechaCreacion As String = Nothing
    Private strAgenteCreador As String = Nothing
    Private strCustomerId As String = Nothing
    Private bolMotorWeb As Boolean = False
    Private lstPasajeros As List(Of classPasajeros) = Nothing
    Private objTarifa As classTarifa = Nothing
    Private strCiudadDestino As classCiudad
    Private objDsctExtra As classDsctExtra = Nothing
    Private objMSGError As List(Of String) = Nothing
    Private objBoletos As List(Of classPNR.classBoletos) = Nothing
    Private objRemarks As List(Of classRemark) = Nothing

    Public Property PseudoHome() As String
        Get
            Return strPseudoHome
        End Get
        Set(ByVal value As String)
            strPseudoHome = value
        End Set
    End Property
    Public Property PseudoAAA() As String
        Get
            Return strPseudoAAA
        End Get
        Set(ByVal value As String)
            strPseudoAAA = value
        End Set
    End Property
    Public Property PseudoRelease() As String
        Get
            Return strPseudoRelease
        End Get
        Set(ByVal value As String)
            strPseudoRelease = value
        End Set
    End Property
    Public Property FechaCreacion() As String
        Get
            Return strFechaCreacion
        End Get
        Set(ByVal value As String)
            strFechaCreacion = value
        End Set
    End Property
    Public Property AgenteCreador() As String
        Get
            Return strAgenteCreador
        End Get
        Set(ByVal value As String)
            strAgenteCreador = value
        End Set
    End Property
    Public Property CustomerId() As String
        Get
            Return strCustomerId
        End Get
        Set(ByVal value As String)
            strCustomerId = value
        End Set
    End Property
    Public Property MotorWeb() As Boolean
        Get
            Return bolMotorWeb
        End Get
        Set(ByVal value As Boolean)
            bolMotorWeb = value
        End Set
    End Property
    Public Property Pasajeros() As List(Of classPasajeros)
        Get
            Return lstPasajeros
        End Get
        Set(ByVal value As List(Of classPasajeros))
            lstPasajeros = value
        End Set
    End Property
    Public Property Segmentos() As List(Of classSegmentos)
        Get
            Return lstSegmentos
        End Get
        Set(ByVal value As List(Of classSegmentos))
            lstSegmentos = value
        End Set
    End Property
    Public Property Tarifa() As classTarifa
        Get
            Return objTarifa
        End Get
        Set(ByVal value As classTarifa)
            objTarifa = value
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
    Public Property CiudadDestino() As classCiudad
        Get
            Return strCiudadDestino
        End Get
        Set(ByVal value As classCiudad)
            strCiudadDestino = value
        End Set
    End Property
    Public Property MSGError() As List(Of String)
        Get
            Return objMSGError
        End Get
        Set(ByVal value As List(Of String))
            objMSGError = value
        End Set
    End Property
    Public Property Boletos() As List(Of classPNR.classBoletos)
        Get
            Return objBoletos
        End Get
        Set(ByVal value As List(Of classPNR.classBoletos))
            objBoletos = value
        End Set
    End Property
    Public Property Remarks() As List(Of classRemark)
        Get
            Return objRemarks
        End Get
        Set(ByVal value As List(Of classRemark))
            objRemarks = value
        End Set
    End Property
    <Serializable()> _
    Public Class classBoletos
        Private strID As String = Nothing
        Private strTicket As String = Nothing
        Public Property ID() As String
            Get
                Return strID
            End Get
            Set(ByVal value As String)
                strID = value
            End Set
        End Property
        Public Property Ticket() As String
            Get
                Return strTicket
            End Get
            Set(ByVal value As String)
                strTicket = value
            End Set
        End Property
    End Class
End Class
