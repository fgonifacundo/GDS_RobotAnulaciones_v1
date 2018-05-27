<Serializable()> _
Public Class classRespuesta
    Private strRespuesta As List(Of String) = Nothing
    Private objErroresAlertas As classErroresAlertas = Nothing
    Public Property Respuesta() As List(Of String)
        Get
            Return strRespuesta
        End Get
        Set(ByVal value As List(Of String))
            strRespuesta = value
        End Set
    End Property
    Public Property ErroresAlertas() As classErroresAlertas
        Get
            Return objErroresAlertas
        End Get
        Set(ByVal value As classErroresAlertas)
            objErroresAlertas = value
        End Set
    End Property
End Class
