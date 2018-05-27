<Serializable()> _
    Public Class classArunk
    Private strTimeStamp As String = Nothing
    Private strStatus As String = Nothing
    Private strLNIATA As String = Nothing
    Private strHostCommand As String = Nothing
    Private strText() As String = Nothing
    Private objErroresAlertas As classErroresAlertas = Nothing
    Public Property Status() As String
        Get
            Return strStatus
        End Get
        Set(ByVal value As String)
            strStatus = value
        End Set
    End Property
    Public Property TimeStamp() As String
        Get
            Return strTimeStamp
        End Get
        Set(ByVal value As String)
            strTimeStamp = value
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
    Public Property Text() As String()
        Get
            Return strText
        End Get
        Set(ByVal value As String())
            strText = value
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

