<Serializable()> _
Public Class classDatosTipoPasajero
    Private strID As String = Nothing
    Private strDescripcion As String = Nothing
    Private strPertenece As String = Nothing
    Private strEquivale As String = Nothing
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
    Public Property Pertenece() As String
        Get
            Return strPertenece
        End Get
        Set(ByVal value As String)
            strPertenece = value
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
End Class
