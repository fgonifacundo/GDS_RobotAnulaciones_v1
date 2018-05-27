<Serializable()> _
Public Class classTarjeta_
    Private intID As Integer = -1
    Private strCodigoTarjera As String = Nothing
    Private strCodPais As String = Nothing
    Private strNumTarjeta As String = Nothing
    Private strNombreBanco As String = Nothing
    Private strFechaVencimiento As String = Nothing
    Private strTitular As String = Nothing
    Private objDocumento As classDocumento = Nothing
    Private strCodSeguridad As String = Nothing
    Private strCodAutorizacion As String = Nothing
    Public Property ID() As Integer
        Get
            Return intID
        End Get
        Set(ByVal value As Integer)
            intID = value
        End Set
    End Property
    Public Property Documento() As classDocumento
        Get
            Return objDocumento
        End Get
        Set(ByVal value As classDocumento)
            objDocumento = value
        End Set
    End Property
    Public Property CodigoTarjera() As String
        Get
            Return strCodigoTarjera
        End Get
        Set(ByVal value As String)
            strCodigoTarjera = value
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
    Public Property CodSeguridad() As String
        Get
            Return strCodSeguridad
        End Get
        Set(ByVal value As String)
            strCodSeguridad = value
        End Set
    End Property
    Public Property CodAutorizacion() As String
        Get
            Return strCodAutorizacion
        End Get
        Set(ByVal value As String)
            strCodAutorizacion = value
        End Set
    End Property
    Public Property NumTarjeta() As String
        Get
            Return strNumTarjeta
        End Get
        Set(ByVal value As String)
            strNumTarjeta = value
        End Set
    End Property
    Public Property NombreBanco() As String
        Get
            Return strNombreBanco
        End Get
        Set(ByVal value As String)
            strNombreBanco = value
        End Set
    End Property
    Public Property FechaVencimiento() As String
        Get
            Return strFechaVencimiento
        End Get
        Set(ByVal value As String)
            strFechaVencimiento = value
        End Set
    End Property
    Public Property Titular() As String
        Get
            Return strTitular
        End Get
        Set(ByVal value As String)
            strTitular = value
        End Set
    End Property
End Class
