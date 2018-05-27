<Serializable()> _
Public Class classTipoDocumentoEmision
    Private strID As String = Nothing
    Private strDescripcion As String = Nothing
    Private intLongitud As Integer = -1
    Private intParaEmision As Integer = -1
    Private intParaTarjeta As Integer = -1
    Private intParaBusqueda As Integer = -1
    Public Property ID() As String
        Get
            Return strID
        End Get
        Set(ByVal value As String)
            strID = value
        End Set
    End Property
    Public Property Descripcion() As String
        Get
            Return strDescripcion
        End Get
        Set(ByVal value As String)
            strDescripcion = value
        End Set
    End Property
    Public Property Longitud() As Integer
        Get
            Return intLongitud
        End Get
        Set(ByVal value As Integer)
            intLongitud = value
        End Set
    End Property

    Public Property ParaEmision() As Integer
        Get
            Return intParaEmision
        End Get
        Set(ByVal value As Integer)
            intParaEmision = value
        End Set
    End Property
    Public Property ParaTarjeta() As Integer
        Get
            Return intParaTarjeta
        End Get
        Set(ByVal value As Integer)
            intParaTarjeta = value
        End Set
    End Property
    Public Property ParaBusqueda() As Integer
        Get
            Return intParaBusqueda
        End Get
        Set(ByVal value As Integer)
            intParaBusqueda = value
        End Set
    End Property
End Class
