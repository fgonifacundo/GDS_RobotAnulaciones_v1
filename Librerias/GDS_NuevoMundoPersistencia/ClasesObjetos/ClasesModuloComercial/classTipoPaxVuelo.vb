Public Class classTipoPaxVuelo

    Private strIdTipoDePax As String = Nothing
    Private strTipoPax As String = Nothing
    Private intEsDesuso As Integer = 0
    Private strEquivale As String = Nothing
    Private strPertenece As String = Nothing
    Private intIdOrden As Integer = 0
    Private strPublicadas As String = Nothing
    Private strPrivadas As String = Nothing
    Private strAerolinea As String = Nothing
    Private intCountable As Integer = 0
    Private strPerteneceAmadeus As String = Nothing



    Public Property IdTipoDePax() As String

        Get
            Return strIdTipoDePax
        End Get
        Set(ByVal value As String)
            strIdTipoDePax = value
        End Set
    End Property

    Public Property TipoPax() As String

        Get
            Return strTipoPax
        End Get
        Set(ByVal value As String)
            strTipoPax = value
        End Set
    End Property
    Public Property EsDesuso() As Integer

        Get
            Return intEsDesuso
        End Get
        Set(ByVal value As Integer)
            intEsDesuso = value
        End Set
    End Property
    Public Property Equivale() As String

        Get
            Return strEquivale
        End Get
        Set(ByVal value As String)
            strEquivale = value
        End Set
    End Property
    Public Property Pertenece() As String

        Get
            Return strPertenece
        End Get
        Set(ByVal value As String)
            strPertenece = value
        End Set
    End Property
    Public Property IdOrden() As Integer

        Get
            Return intIdOrden
        End Get
        Set(ByVal value As Integer)
            intIdOrden = value
        End Set
    End Property
    Public Property Publicadas() As String

        Get
            Return strPublicadas
        End Get
        Set(ByVal value As String)
            strPublicadas = value
        End Set
    End Property
    Public Property Privadas() As String

        Get
            Return strPrivadas
        End Get
        Set(ByVal value As String)
            strPrivadas = value
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
    Public Property Countable() As Integer

        Get
            Return intCountable
        End Get
        Set(ByVal value As Integer)
            intCountable = value
        End Set
    End Property

    Public Property PerteneceAmadeus() As String

        Get
            Return strPerteneceAmadeus
        End Get
        Set(ByVal value As String)
            strPerteneceAmadeus = value
        End Set
    End Property
End Class
