<Serializable()> _
Public Class classErroresAlertas
    Private strErrores As List(Of String) = Nothing
    Private strAlertas As List(Of String) = Nothing
    Public Property Errores() As List(Of String)
        Get
            Return strErrores
        End Get
        Set(ByVal value As List(Of String))
            strErrores = value
        End Set
    End Property
    Public Property Alertas() As List(Of String)
        Get
            Return strAlertas
        End Get
        Set(ByVal value As List(Of String))
            strAlertas = value
        End Set
    End Property
End Class
