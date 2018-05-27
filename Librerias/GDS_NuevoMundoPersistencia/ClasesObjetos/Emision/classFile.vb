Public Class classFile

    Private intSucursal As Integer = 0
    Private intDK As Integer = 0
    Private intNumeroFile As Integer = 0
    Private strPNR As String = Nothing
    Private intPrefijo As Integer = 0
    Private strBoleto As String = Nothing
    Private strEstado As String = Nothing
    Private strStock As String = Nothing

    Public Property Sucursal() As Integer
        Get
            Return intSucursal
        End Get
        Set(ByVal value As Integer)
            intSucursal = value
        End Set
    End Property

    Public Property DK() As Integer
        Get
            Return intDK
        End Get
        Set(ByVal value As Integer)
            intDK = value
        End Set
    End Property

    Public Property NumeroFile() As Integer
        Get
            Return intNumeroFile
        End Get
        Set(ByVal value As Integer)
            intNumeroFile = value
        End Set
    End Property

    Public Property PNR() As String
        Get
            Return strPNR
        End Get
        Set(ByVal value As String)
            strPNR = value
        End Set
    End Property

    Public Property Prefijo() As Integer
        Get
            Return intPrefijo
        End Get
        Set(ByVal value As Integer)
            intPrefijo = value
        End Set
    End Property

    Public Property Boleto() As String
        Get
            Return strBoleto
        End Get
        Set(ByVal value As String)
            strBoleto = value
        End Set
    End Property

    Public Property Estado() As String
        Get
            Return strEstado
        End Get
        Set(ByVal value As String)
            strEstado = value
        End Set
    End Property

    Public Property Stock() As String
        Get
            Return strStock
        End Get
        Set(ByVal value As String)
            strStock = value
        End Set
    End Property


End Class
