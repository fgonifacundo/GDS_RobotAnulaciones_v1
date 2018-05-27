Namespace PromotionalShopping
    <Serializable()> _
    Public Class classDepartureArrival
        Private strGMTOffset As String = Nothing
        Private strDateTime As String = Nothing
        Private strAirport As String = Nothing
        Property GMTOffset() As String
            Get
                Return strGMTOffset
            End Get
            Set(ByVal value As String)
                strGMTOffset = value
            End Set
        End Property
        Property DateTime() As String
            Get
                Return strDateTime
            End Get
            Set(ByVal value As String)
                strDateTime = value
            End Set
        End Property
        Property Airport() As String
            Get
                Return strAirport
            End Get
            Set(ByVal value As String)
                strAirport = value
            End Set
        End Property
    End Class
End Namespace

