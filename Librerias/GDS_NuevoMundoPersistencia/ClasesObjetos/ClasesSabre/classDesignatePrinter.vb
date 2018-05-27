Public Class classDesignatePrinter
    Private strStatus As String = Nothing
    Private strLNIATA As String = Nothing
    Private strHostCommand As String = Nothing
    Private strErroresAlertas As classErroresAlertas
    Public Property Status() As String
        Get
            Return strStatus
        End Get
        Set(ByVal value As String)
            strStatus = value
        End Set
    End Property
    Public Property LNIATA() As String
        Get
            Return strLNIATA
        End Get
        Set(ByVal value As String)
            strLNIATA = value
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
            Return strErroresAlertas
        End Get
        Set(ByVal value As classErroresAlertas)
            strErroresAlertas = value
        End Set
    End Property
End Class
