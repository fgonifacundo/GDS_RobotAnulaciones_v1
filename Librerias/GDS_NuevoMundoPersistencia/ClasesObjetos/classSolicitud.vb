<Serializable()> _
Public Class classSolicitud
    Private strComentarios As String = Nothing
    Private objNeto As classMonto = Nothing
    Private objTotal As classMonto = Nothing
    Private bolWaiver As Boolean = False
    Public Property Waiver() As Boolean
        Get
            Return bolWaiver
        End Get
        Set(ByVal value As Boolean)
            bolWaiver = value
        End Set
    End Property
    Public Property Neto() As classMonto
        Get
            Return objNeto
        End Get
        Set(ByVal value As classMonto)
            objNeto = value
        End Set
    End Property
    Public Property Total() As classMonto
        Get
            Return objTotal
        End Get
        Set(ByVal value As classMonto)
            objTotal = value
        End Set
    End Property
    Public Property Comentarios() As String
        Get
            Return strComentarios
        End Get
        Set(ByVal value As String)
            strComentarios = value
        End Set
    End Property
End Class
