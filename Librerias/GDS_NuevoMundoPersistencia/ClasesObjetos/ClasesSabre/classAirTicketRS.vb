<Serializable()> _
Public Class classAirTicketRS
    Private strStatus As String = Nothing
    Private strHostCommand As String = Nothing
    Private lstRespuesta As List(Of String) = Nothing
    Private objErroresAlertas As classErroresAlertas = Nothing
    Public Property Status() As String
        Get
            Return strStatus
        End Get
        Set(ByVal value As String)
            strStatus = value
        End Set
    End Property
    Public Property HostCommand() As String
        Get
            Return strHostCommand
        End Get
        Set(ByVal value As String)
            strHostCommand = value
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
    Public Property Respuesta() As List(Of String)
        Get
            Return lstRespuesta
        End Get
        Set(ByVal value As List(Of String))
            lstRespuesta = value
        End Set
    End Property
End Class
