<Serializable()> _
Public Class classAsientos
    Private intSegmento As Integer = -1
    Private strSegmentStatus As String = Nothing
    Private strNumber As String = Nothing
    Private strOriginLocation As String = Nothing
    Private strDestinationLocation As String = Nothing

    Public Property Segmento() As Integer
        Get
            Return intSegmento
        End Get
        Set(ByVal value As Integer)
            intSegmento = value
        End Set
    End Property
    Public Property SegmentStatus() As String
        Get
            Return strSegmentStatus
        End Get
        Set(ByVal value As String)
            strSegmentStatus = value
        End Set
    End Property
    Public Property Number() As String
        Get
            Return strNumber
        End Get
        Set(ByVal value As String)
            strNumber = value
        End Set
    End Property
    Public Property OriginLocation() As String
        Get
            Return strOriginLocation
        End Get
        Set(ByVal value As String)
            strOriginLocation = value
        End Set
    End Property
    Public Property DestinationLocation() As String
        Get
            Return strDestinationLocation
        End Get
        Set(ByVal value As String)
            strDestinationLocation = value
        End Set
    End Property

End Class
