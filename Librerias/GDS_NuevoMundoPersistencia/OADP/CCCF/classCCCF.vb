<Serializable()> _
Public Class classCCCF
    Private bolExchange As Boolean = False
    Private strPrefijo As String = String.Empty
    Private strNombreAerolinea As String = String.Empty
    Private strNumeroBoleto As String = String.Empty
    Private strDigitoVerificacion As String = String.Empty
    Private strOrigen As String = String.Empty
    Private strDestino As String = String.Empty
    Private strEndorsements As String = String.Empty
    Private strFechaEmision As String = String.Empty

    Private bolNet As Boolean = False
    Private strNetAmount As String = String.Empty
    Private strNetCreditCardAmount As String = String.Empty
    Private strNetAmountType As String = String.Empty

    Private strIATA As String = String.Empty
    Private strNombreIata As String = String.Empty
    Private strCiudadIata As String = String.Empty
    Private strPaisIata As String = String.Empty

    Private strCodigoReserva As String = String.Empty
    Private strCodigoGDS As String = String.Empty
    Private strNombrePasajero As String = String.Empty
    Private strDocumentoPasajero As String = String.Empty

    Private strSimboloMonedaNeto As String = String.Empty
    Private strNeto As String = String.Empty
    Private strSimboloMonedaEquivalente As String = String.Empty
    Private strEquivalente As String = String.Empty
    Private strSimboloMonedaTotal As String = String.Empty
    Private strTotal As String = String.Empty

    Private strTotalTaxes As String = String.Empty
    Private objTaxesOriginales As List(Of classTaxes) = Nothing
    Private objTaxes As List(Of classTaxes) = Nothing


    Private strCodigoTarjeta As String = String.Empty
    Private strNumeroTarjeta As String = String.Empty
    Private strFechaExpiracion As String = String.Empty
    Private strCodigoAprobacion As String = String.Empty
    Public Property Exchange() As Boolean
        Get
            Return bolExchange
        End Get
        Set(ByVal value As Boolean)
            bolExchange = value
        End Set
    End Property
    Public Property Prefijo() As String
        Get
            Return strPrefijo
        End Get
        Set(ByVal value As String)
            strPrefijo = value
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
    Public Property NumeroBoleto() As String
        Get
            Return strNumeroBoleto
        End Get
        Set(ByVal value As String)
            strNumeroBoleto = value
        End Set
    End Property
    Public Property DigitoVerificacion() As String
        Get
            Return strDigitoVerificacion
        End Get
        Set(ByVal value As String)
            strDigitoVerificacion = value
        End Set
    End Property
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
    Public Property Endorsements() As String
        Get
            Return strEndorsements
        End Get
        Set(ByVal value As String)
            strEndorsements = value
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

    Public Property Net() As Boolean
        Get
            Return bolNet
        End Get
        Set(ByVal value As Boolean)
            bolNet = value
        End Set
    End Property
    Public Property NetAmount() As String
        Get
            Return strNetAmount
        End Get
        Set(ByVal value As String)
            strNetAmount = value
        End Set
    End Property
    Public Property NetCreditCardAmount() As String
        Get
            Return strNetCreditCardAmount
        End Get
        Set(ByVal value As String)
            strNetCreditCardAmount = value
        End Set
    End Property
    Public Property NetAmountType() As String
        Get
            Return strNetAmountType
        End Get
        Set(ByVal value As String)
            strNetAmountType = value
        End Set
    End Property

    Public Property IATA() As String
        Get
            Return strIATA
        End Get
        Set(ByVal value As String)
            strIATA = value
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

    Public Property CodigoReserva() As String
        Get
            Return strCodigoReserva
        End Get
        Set(ByVal value As String)
            strCodigoReserva = value
        End Set
    End Property
    Public Property CodigoGDS() As String
        Get
            Return strCodigoGDS
        End Get
        Set(ByVal value As String)
            strCodigoGDS = value
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

    Public Property SimboloMonedaNeto() As String
        Get
            Return strSimboloMonedaNeto
        End Get
        Set(ByVal value As String)
            strSimboloMonedaNeto = value
        End Set
    End Property
    Public Property Neto() As String
        Get
            Return strNeto
        End Get
        Set(ByVal value As String)
            strNeto = value
        End Set
    End Property
    Public Property SimboloMonedaEquivalente() As String
        Get
            Return strSimboloMonedaEquivalente
        End Get
        Set(ByVal value As String)
            strSimboloMonedaEquivalente = value
        End Set
    End Property
    Public Property Equivalente() As String
        Get
            Return strEquivalente
        End Get
        Set(ByVal value As String)
            strEquivalente = value
        End Set
    End Property
    Public Property SimboloMonedaTotal() As String
        Get
            Return strSimboloMonedaTotal
        End Get
        Set(ByVal value As String)
            strSimboloMonedaTotal = value
        End Set
    End Property
    Public Property Total() As String
        Get
            Return strTotal
        End Get
        Set(ByVal value As String)
            strTotal = value
        End Set
    End Property

    Public Property TotalTaxes() As String
        Get
            Return strTotalTaxes
        End Get
        Set(ByVal value As String)
            strTotalTaxes = value
        End Set
    End Property
    Public Property TaxesOriginales() As List(Of classTaxes)
        Get
            Return objTaxesOriginales
        End Get
        Set(ByVal value As List(Of classTaxes))
            objTaxesOriginales = value
        End Set
    End Property
    Public Property Taxes() As List(Of classTaxes)
        Get
            Return objTaxes
        End Get
        Set(ByVal value As List(Of classTaxes))
            objTaxes = value
        End Set
    End Property


    Public Property CodigoTarjeta() As String
        Get
            Return strCodigoTarjeta
        End Get
        Set(ByVal value As String)
            strCodigoTarjeta = value
        End Set
    End Property
    Public Property NumeroTarjeta() As String
        Get
            Return strNumeroTarjeta
        End Get
        Set(ByVal value As String)
            strNumeroTarjeta = value
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
    Public Property CodigoAprobacion() As String
        Get
            Return strCodigoAprobacion
        End Get
        Set(ByVal value As String)
            strCodigoAprobacion = value
        End Set
    End Property

End Class

