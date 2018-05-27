<Serializable()> _
Public Class classSubCodigo
    Private intID As Integer = 0
    Private strNombre As String = Nothing
    Private bolMarca As Boolean = False
    Private intRequiereEjecutiva As Integer = 0
    Sub New()
        MyBase.New()
    End Sub
    Sub New(ByVal _intID As Integer, ByVal _strNombre As String, ByVal _bolMarca As Boolean, ByVal _intRequiereEjecutiva As Integer)
        MyBase.New()
        intID = _intID
        strNombre = _strNombre
        bolMarca = _bolMarca
        intRequiereEjecutiva = _intRequiereEjecutiva

    End Sub

    Public Property Marca() As Boolean
        Get
            Return bolMarca
        End Get
        Set(ByVal value As Boolean)
            bolMarca = value
        End Set
    End Property
    Public Property ID() As Integer
        Get
            Return intID
        End Get
        Set(ByVal value As Integer)
            intID = value
        End Set
    End Property
    Public Property Nombre() As String
        Get
            Return strNombre
        End Get
        Set(ByVal value As String)
            strNombre = value
        End Set
    End Property
    Public Property RequiereEjecutiva() As Integer
        Get
            Return intRequiereEjecutiva
        End Get
        Set(ByVal value As Integer)
            intRequiereEjecutiva = value
        End Set
    End Property
End Class
