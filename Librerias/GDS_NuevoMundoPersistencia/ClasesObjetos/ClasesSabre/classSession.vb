<Serializable()> _
Public Class classSession
    Private strToken As String = Nothing
    Private strConversationID As String = Nothing
    Property Token() As String
        Get
            Return strToken
        End Get
        Set(ByVal value As String)
            strToken = value
        End Set
    End Property
    Property ConversationID() As String
        Get
            Return strConversationID
        End Get
        Set(ByVal value As String)
            strConversationID = value
        End Set
    End Property
End Class
