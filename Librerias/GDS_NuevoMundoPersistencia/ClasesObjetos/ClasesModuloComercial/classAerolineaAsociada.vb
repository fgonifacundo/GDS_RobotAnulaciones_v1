<Serializable()> _
Public Class classAerolineaAsociada
    Private strID As String = Nothing
    Private strDescripcion As String = Nothing
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
End Class
