<Serializable()> _
Public Class classTourCodeResultado
    Private strTipoPasajero As String = Nothing
    Private strTourCode As String = Nothing
    Private strTarifario As Integer = -1
    Private intRegla As Integer = -1
    Private strTipoNet_Tour As String = Nothing
    Private intComNM As Integer = -1
    Private intComAgencia As Integer = -1
    Private intFactorMeta As Integer = -1
    Private intOver As Integer = -1
    Private intOverNacCancelado As Integer = -1
    Private intEmisionWeb As Integer = -1
    Public Property ComAgencia() As Integer
        Get
            Return intComAgencia
        End Get
        Set(ByVal value As Integer)
            intComAgencia = value
        End Set
    End Property
    Public Property ComNM() As Integer
        Get
            Return intComNM
        End Get
        Set(ByVal value As Integer)
            intComNM = value
        End Set
    End Property
    Public Property EmisionWeb() As Integer
        Get
            Return intEmisionWeb
        End Get
        Set(ByVal value As Integer)
            intEmisionWeb = value
        End Set
    End Property
    Public Property FactorMeta() As Integer
        Get
            Return intFactorMeta
        End Get
        Set(ByVal value As Integer)
            intFactorMeta = value
        End Set
    End Property
    Public Property Over() As Integer
        Get
            Return intOver
        End Get
        Set(ByVal value As Integer)
            intOver = value
        End Set
    End Property
    Public Property OverNacCancelado() As Integer
        Get
            Return intOverNacCancelado
        End Get
        Set(ByVal value As Integer)
            intOverNacCancelado = value
        End Set
    End Property
    Public Property Regla() As Integer
        Get
            Return intRegla
        End Get
        Set(ByVal value As Integer)
            intRegla = value
        End Set
    End Property
    Public Property Tarifario() As Integer
        Get
            Return strTarifario
        End Get
        Set(ByVal value As Integer)
            strTarifario = value
        End Set
    End Property
    Public Property TipoNet_Tour() As String
        Get
            Return strTipoNet_Tour
        End Get
        Set(ByVal value As String)
            strTipoNet_Tour = value
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
    Public Property TipoPasajero() As String
        Get
            Return strTipoPasajero
        End Get
        Set(ByVal value As String)
            strTipoPasajero = value
        End Set
    End Property
End Class
