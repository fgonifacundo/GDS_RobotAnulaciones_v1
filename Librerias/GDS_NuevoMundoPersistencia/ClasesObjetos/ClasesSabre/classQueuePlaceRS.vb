<Serializable()> _
Public Class classQueuePlaceRS
    Private objQueueInfo As classQueueInfo = Nothing

    Public Property QueueInfo() As classQueueInfo
        Get
            Return objQueueInfo
        End Get
        Set(value As classQueueInfo)
            objQueueInfo = value
        End Set
    End Property

End Class
