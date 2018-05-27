<Serializable()> _
Public Class classCiudad
    Private strCodigo As String = Nothing
    Private intTipo As Integer = -1
    Private strCodCiudad As String = Nothing
    Private strNomCiudad As String = Nothing
    Private strCodPais As String = Nothing
    Private strNomPais As String = Nothing
    Private strCodRegion As String = Nothing
    Private intEsNacional As Integer = -1
    Public Property CodCiudad() As String
        Get
            Return strCodCiudad
        End Get
        Set(ByVal value As String)
            strCodCiudad = value
        End Set
    End Property
    Public Property NomCiudad() As String
        Get
            Return strNomCiudad
        End Get
        Set(ByVal value As String)
            strNomCiudad = value
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
    Public Property CodPais() As String
        Get
            Return strCodPais
        End Get
        Set(ByVal value As String)
            strCodPais = value
        End Set
    End Property
    Public Property NomPais() As String
        Get
            Return strNomPais
        End Get
        Set(ByVal value As String)
            strNomPais = value
        End Set
    End Property
    Public Property CodRegion() As String
        Get
            Return strCodRegion
        End Get
        Set(ByVal value As String)
            strCodRegion = value
        End Set
    End Property
    Public Property Tipo() As Integer
        Get
            Return intTipo
        End Get
        Set(ByVal value As Integer)
            intTipo = value
        End Set
    End Property
    Public Property EsNacional() As Integer
        Get
            Return intEsNacional
        End Get
        Set(ByVal value As Integer)
            intEsNacional = value
        End Set
    End Property
End Class
