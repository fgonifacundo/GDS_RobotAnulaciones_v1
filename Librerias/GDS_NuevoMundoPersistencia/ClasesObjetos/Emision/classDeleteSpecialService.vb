<Serializable()> _
Public Class classDeleteSpecialService
    Private oItem As String
    Private oAirline As String
    Public Property Item() As String
        Get
            Return oItem
        End Get
        Set(ByVal value As String)
            oItem = value
        End Set
    End Property
    Public Property Airline() As String
        Get
            Return oAirline
        End Get
        Set(ByVal value As String)
            oAirline = value
        End Set
    End Property
End Class
