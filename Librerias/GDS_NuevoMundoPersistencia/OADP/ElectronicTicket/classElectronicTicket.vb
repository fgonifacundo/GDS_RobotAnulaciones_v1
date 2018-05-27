<Serializable()> _
Public Class classElectronicTicket
    Private strPrefijo As String = String.Empty
    Private strNumeroBoleto As String = String.Empty
    Private strNombrePasajero As String = String.Empty
    Private strDocumentoPasajero As String = String.Empty
    Private strNombreAerolinea As String = String.Empty
    Private strAgenteEmisor As String = String.Empty
    Private strAgenteCreador As String = String.Empty
    Private strFechaEmision As String = String.Empty
    Private objIATA As classIata = Nothing
    Private strDK As String = Nothing
    Private strTourCode As String = Nothing
    Private strCalculoTarifa As String = Nothing
    Private strEndosos As String = Nothing
    Private strTicketOriginal As String = Nothing
    Private strFormaPago As String = Nothing
    Private bolExchange As Boolean = False
    Private objTaxesPaid As List(Of classTaxes) = Nothing
    Private objTotal As classMonto = Nothing
    Private objFare As classMonto = Nothing

    Public Property Prefijo() As String
        Get
            Return strPrefijo
        End Get
        Set(ByVal value As String)
            strPrefijo = value
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
    Public Property NombrePasajero() As String
        Get
            Return strNombrePasajero
        End Get
        Set(ByVal value As String)
            strNombrePasajero = value
        End Set
    End Property
    Public Property DocumentoPasajero() As String
        Get
            Return strDocumentoPasajero
        End Get
        Set(ByVal value As String)
            strDocumentoPasajero = value
        End Set
    End Property
    Public Property NombreAerolinea() As String
        Get
            Return strNombreAerolinea
        End Get
        Set(ByVal value As String)
            strNombreAerolinea = value
        End Set
    End Property
    Public Property AgenteEmisor() As String
        Get
            Return strAgenteEmisor
        End Get
        Set(ByVal value As String)
            strAgenteEmisor = value
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
    Public Property FechaEmision() As String
        Get
            Return strFechaEmision
        End Get
        Set(ByVal value As String)
            strFechaEmision = value
        End Set
    End Property
    Public Property IATA() As classIata
        Get
            Return objIATA
        End Get
        Set(ByVal value As classIata)
            objIATA = value
        End Set
    End Property
    Public Property DK() As String
        Get
            Return strDK
        End Get
        Set(ByVal value As String)
            strDK = value
        End Set
    End Property
    Public Property TourCode() As String
        Get
            Return strTourCode
        End Get
        Set(ByVal value As String)
            strTourCode = value
        End Set
    End Property
    Public Property CalculoTarifa() As String
        Get
            Return strCalculoTarifa
        End Get
        Set(ByVal value As String)
            strCalculoTarifa = value
        End Set
    End Property
    Public Property Endosos() As String
        Get
            Return strEndosos
        End Get
        Set(ByVal value As String)
            strEndosos = value
        End Set
    End Property
    Public Property TicketOriginal() As String
        Get
            Return strTicketOriginal
        End Get
        Set(ByVal value As String)
            strTicketOriginal = value
        End Set
    End Property
    Public Property FormaPago() As String
        Get
            Return strFormaPago
        End Get
        Set(ByVal value As String)
            strFormaPago = value
        End Set
    End Property
    Public Property Exchange() As Boolean
        Get
            Return bolExchange
        End Get
        Set(ByVal value As Boolean)
            bolExchange = value
        End Set
    End Property
    Public Property TaxesPaid() As List(Of classTaxes)
        Get
            Return objTaxesPaid
        End Get
        Set(ByVal value As List(Of classTaxes))
            objTaxesPaid = value
        End Set
    End Property
    Public Property Total() As classMonto
        Get
            Return objTotal
        End Get
        Set(ByVal value As classMonto)
            objTotal = value
        End Set
    End Property
    Public Property Fare() As classMonto
        Get
            Return objFare
        End Get
        Set(ByVal value As classMonto)
            objFare = value
        End Set
    End Property
End Class