Public Class classEnvioCorreo

    Private strIdCorreo As String = ""
    Private strRemite As String = ""
    Private strDestino As String = ""
    Private strMensaje As String = ""
    Private strAsunto As String = ""
    Private strFechaAlta As String = ""
    Private intToSend As Integer = 0
    Private strCopiaOculta As String = ""

    Public Property IdCorreo() As String
        Get
            Return strIdCorreo
        End Get
        Set(ByVal value As String)
            strIdCorreo = value
        End Set
    End Property

    Public Property Remite() As String
        Get
            Return strRemite
        End Get
        Set(ByVal value As String)
            strRemite = value
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

    Public Property Mensaje() As String
        Get
            Return strMensaje
        End Get
        Set(ByVal value As String)
            strMensaje = value
        End Set
    End Property

    Public Property Asunto() As String
        Get
            Return strAsunto
        End Get
        Set(ByVal value As String)
            strAsunto = value
        End Set
    End Property

    Public Property FechaAlta() As String
        Get
            Return strFechaAlta
        End Get
        Set(ByVal value As String)
            strFechaAlta = value
        End Set
    End Property

    Public Property ToSend() As Integer
        Get
            Return intToSend
        End Get
        Set(ByVal value As Integer)
            intToSend = value
        End Set
    End Property

    Public Property CopiaOculta() As String
        Get
            Return strCopiaOculta
        End Get
        Set(ByVal value As String)
            strCopiaOculta = value
        End Set
    End Property





End Class
