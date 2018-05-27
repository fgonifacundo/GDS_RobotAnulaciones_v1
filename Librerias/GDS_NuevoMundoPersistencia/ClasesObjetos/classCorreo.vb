
Public Class classCorreo
    Private intIdCorreo As Integer
    Private strNomCorreo As String = ""
    Private strFromCorreo As String = ""
    Private strDisplayFromCorreo As String = ""
    Private strToCorreo As String = ""
    Private strCCCorreo As String = ""
    Private strBCCCorreo As String = ""
    Private strStyleAddressCorreo As String = ""
    Private strSubjectCorreo As String = ""
    Private strFormatoCorreo As String = ""
    Private strHeaderCorreo As String = ""
    Private strFooterCorreo As String = ""
    Private strHostCorreo As String = ""
    Private strStyleFileCorreo As String = ""
    Private strLogoCorreo As String = ""
    Private strBodyCorreo As String = ""
    Private intIdWeb As Integer
    Private intIdLang As Integer

    Private strBCCCorreoAgy As String = ""
    Private strBCCCorreoSist As String = ""

#Region "Propiedades"
    Public Property IdCorreo() As Integer
        Get
            Return intIdCorreo
        End Get
        Set(ByVal value As Integer)
            intIdCorreo = value
        End Set
    End Property

    Public Property NombreCorreo() As String
        Get
            Return strNomCorreo
        End Get
        Set(ByVal value As String)
            strNomCorreo = Trim(value)
        End Set
    End Property

    Public Property FromCorreo() As String
        Get
            Return strFromCorreo
        End Get
        Set(ByVal value As String)
            strFromCorreo = Trim(value)
        End Set
    End Property

    Public Property DisplayFromCorreo() As String
        Get
            Return strDisplayFromCorreo
        End Get
        Set(ByVal value As String)
            strDisplayFromCorreo = Trim(value)
        End Set
    End Property

    Public Property ToCorreo() As String
        Get
            Return strToCorreo
        End Get
        Set(ByVal value As String)
            strToCorreo = Trim(value)
        End Set
    End Property

    Public Property CCCorreo() As String
        Get
            Return strCCCorreo
        End Get
        Set(ByVal value As String)
            strCCCorreo = Trim(value)
        End Set
    End Property

    Public Property BCCCorreo() As String
        Get
            Return strBCCCorreo
        End Get
        Set(ByVal value As String)
            strBCCCorreo = Trim(value)
        End Set
    End Property

    Public Property StyleAddressCorreo() As String
        Get
            Return strStyleAddressCorreo
        End Get
        Set(ByVal value As String)
            strStyleAddressCorreo = Trim(value)
        End Set
    End Property

    Public Property SubjectCorreo() As String
        Get
            Return strSubjectCorreo
        End Get
        Set(ByVal value As String)
            strSubjectCorreo = Trim(value)
        End Set
    End Property

    Public Property FormatoCorreo() As String
        Get
            Return strFormatoCorreo
        End Get
        Set(ByVal value As String)
            strFormatoCorreo = Trim(value)
        End Set
    End Property

    Public Property HeaderCorreo() As String
        Get
            Return strHeaderCorreo
        End Get
        Set(ByVal value As String)
            strHeaderCorreo = Trim(value)
        End Set
    End Property

    Public Property FooterCorreo() As String
        Get
            Return strFooterCorreo
        End Get
        Set(ByVal value As String)
            strFooterCorreo = Trim(value)
        End Set
    End Property

    Public Property HostCorreo() As String
        Get
            Return strHostCorreo
        End Get
        Set(ByVal value As String)
            strHostCorreo = Trim(value)
        End Set
    End Property

    Public Property StyleFileCorreo() As String
        Get
            Return strStyleFileCorreo
        End Get
        Set(ByVal value As String)
            strStyleFileCorreo = Trim(value)
        End Set
    End Property

    Public Property LogoCorreo() As String
        Get
            Return strLogoCorreo
        End Get
        Set(ByVal value As String)
            strLogoCorreo = Trim(value)
        End Set
    End Property

    Public Property BodyCorreo() As String
        Get
            Return strBodyCorreo
        End Get
        Set(ByVal value As String)
            strBodyCorreo = Trim(value)
        End Set
    End Property

    Public Property IdWeb() As Integer
        Get
            Return intIdWeb
        End Get
        Set(ByVal value As Integer)
            intIdWeb = value
        End Set
    End Property

    Public Property IdLang() As Integer
        Get
            Return intIdLang
        End Get
        Set(ByVal value As Integer)
            intIdLang = value
        End Set
    End Property

    Public Property BCCCorreoAgy() As String
        Get
            Return strBCCCorreoAgy
        End Get
        Set(ByVal value As String)
            strBCCCorreoAgy = Trim(value)
        End Set
    End Property

    Public Property BCCCorreoSist() As String
        Get
            Return strBCCCorreoSist
        End Get
        Set(ByVal value As String)
            strBCCCorreoSist = Trim(value)
        End Set
    End Property
#End Region
End Class


