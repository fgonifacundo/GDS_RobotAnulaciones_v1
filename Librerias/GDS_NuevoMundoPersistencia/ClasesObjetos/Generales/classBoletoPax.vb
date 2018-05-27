Public Class classBoletoPax

    Private datFechaEmision As String
    Private strIdFacturaCabeza As String = Nothing
    Private strIdTipoComprobante As String = Nothing
    Private intEmitido As Integer = 0
    Private strIdFile As String = Nothing
    Private intMarcaVoid As Integer = 0
    Private intNumeroSerie As Integer = 0
    Private intIdSucursal As Integer = 0
    Private strCodigoPNR As String = Nothing
    Private strIdCliente As String = Nothing
    Private intIdPaxReserva As Integer = 0
    Private strIdIata As String = Nothing
    Private strBoletoTcAsociado As String = Nothing

    Public Property FechaEmision() As String
        Get
            Return datFechaEmision
        End Get
        Set(ByVal value As String)
            datFechaEmision = value
        End Set
    End Property
    Public Property IdFacturaCabeza() As String
        Get
            Return strIdFacturaCabeza
        End Get
        Set(ByVal value As String)
            strIdFacturaCabeza = value
        End Set
    End Property
    Public Property IdTipoComprobante() As String
        Get
            Return strIdTipoComprobante
        End Get
        Set(ByVal value As String)
            strIdTipoComprobante = value
        End Set
    End Property
    Public Property Emitido() As Integer
        Get
            Return intEmitido
        End Get
        Set(ByVal value As Integer)
            intEmitido = value
        End Set
    End Property
    Public Property IdFile() As String
        Get
            Return strIdFile
        End Get
        Set(ByVal value As String)
            strIdFile = value
        End Set
    End Property
    Public Property MarcaVoid() As Integer
        Get
            Return intMarcaVoid
        End Get
        Set(ByVal value As Integer)
            intMarcaVoid = value
        End Set
    End Property
    Public Property NumeroSerie() As Integer
        Get
            Return intNumeroSerie
        End Get
        Set(ByVal value As Integer)
            intNumeroSerie = value
        End Set
    End Property
    Public Property IdSucursal() As Integer
        Get
            Return intIdSucursal
        End Get
        Set(ByVal value As Integer)
            intIdSucursal = value
        End Set
    End Property

    Public Property CodigoPNR() As String
        Get
            Return strCodigoPNR
        End Get
        Set(ByVal value As String)
            strCodigoPNR = value
        End Set
    End Property

    Public Property IdCliente() As String
        Get
            Return strIdCliente
        End Get
        Set(ByVal value As String)
            strIdCliente = value
        End Set
    End Property

    Public Property IdPaxReserva() As Integer
        Get
            Return intIdPaxReserva
        End Get
        Set(ByVal value As Integer)
            intIdPaxReserva = value
        End Set
    End Property

    Public Property IdIata() As String
        Get
            Return strIdIata
        End Get
        Set(ByVal value As String)
            strIdIata = value
        End Set
    End Property

    Public Property BoletoTcAsociado() As String
        Get
            Return strBoletoTcAsociado
        End Get
        Set(ByVal value As String)
            strBoletoTcAsociado = value
        End Set
    End Property

End Class
