<Serializable()> _
Public Class classPromotor
    Private intCodigo As Integer = 0
    Private strNombrePromotor As String = Nothing
    Private strEmailPromotor As String = Nothing
    Private strCodigoVendedor As String = Nothing
    Private strCodigoJefePromotor As String = Nothing
    Private strCorreoJefePromotor As String = Nothing
    Private intActivo As Integer = 0
    Private intAnulada As Integer = 0
    Public Property Codigo() As Integer
        Get
            Return intCodigo
        End Get
        Set(ByVal value As Integer)
            intCodigo = value
        End Set
    End Property
    Public Property EmailPromotor() As String
        Get
            Return strEmailPromotor
        End Get
        Set(ByVal value As String)
            strEmailPromotor = value
        End Set
    End Property
    'Private intCodigoVendedor As String = Nothing
    Public Property CodigoVendedor() As String
        Get
            Return strCodigoVendedor
        End Get
        Set(ByVal value As String)
            strCodigoVendedor = value
        End Set
    End Property

    'Private strCodigoJefePromotor As String = Nothing
    'Private strCorreoJefePromotor As String = Nothing

    Public Property CodigoJefePromotor() As String
        Get
            Return strCodigoJefePromotor
        End Get
        Set(ByVal value As String)
            strCodigoJefePromotor = value
        End Set
    End Property

    Public Property CorreoJefePromotor() As String
        Get
            Return strCorreoJefePromotor
        End Get
        Set(ByVal value As String)
            strCorreoJefePromotor = value
        End Set
    End Property

    Public Property NombrePromotor() As String
        Get
            Return strNombrePromotor
        End Get
        Set(ByVal value As String)
            strNombrePromotor = value
        End Set
    End Property
    Public Property Activo() As Integer
        Get
            Return intActivo
        End Get
        Set(ByVal value As Integer)
            intActivo = value
        End Set
    End Property

    Public Property Anulada() As Integer
        Get
            Return intAnulada
        End Get
        Set(ByVal value As Integer)
            intAnulada = value
        End Set
    End Property
End Class
