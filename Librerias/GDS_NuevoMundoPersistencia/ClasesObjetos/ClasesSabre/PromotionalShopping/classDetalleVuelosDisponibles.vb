Namespace PromotionalShopping
    <Serializable()> _
    Public Class classDetalleVuelosDisponibles
        Private lstSegmentos As List(Of PromotionalShopping.classSegmento)
        Property Segmentos() As List(Of PromotionalShopping.classSegmento)
            Get
                Return lstSegmentos
            End Get
            Set(ByVal value As List(Of PromotionalShopping.classSegmento))
                lstSegmentos = value
            End Set
        End Property
    End Class
    <Serializable()> _
   Public Class classSegmento
        Private strSegmentoCasado As String = Nothing
        Private strEquipo As String = Nothing
        Private intETicket As Integer = 0
        Private strTiempo As String = Nothing
        Private strClaseReserva As String = Nothing
        Private objDeparture As PromotionalShopping.classDepartureArrival = Nothing
        Private objArrival As PromotionalShopping.classDepartureArrival = Nothing
        Private objAerolinea As PromotionalShopping.classAerolinea = Nothing
        Private objOperadora As PromotionalShopping.classAerolinea = Nothing
        Private intStopQuantity As Integer = 0
        'Private Stops
        Property SegmentoCasado() As String
            Get
                Return strSegmentoCasado
            End Get
            Set(ByVal value As String)
                strSegmentoCasado = value
            End Set
        End Property
        Property Equipo() As String
            Get
                Return strEquipo
            End Get
            Set(ByVal value As String)
                strEquipo = value
            End Set
        End Property
        Property ETicket() As Integer
            Get
                Return intETicket
            End Get
            Set(ByVal value As Integer)
                intETicket = value
            End Set
        End Property
        Property Tiempo() As String
            Get
                Return strTiempo
            End Get
            Set(ByVal value As String)
                strTiempo = value
            End Set
        End Property
        Property ClaseReserva() As String
            Get
                Return strClaseReserva
            End Get
            Set(ByVal value As String)
                strClaseReserva = value
            End Set
        End Property
        Property Arrival() As PromotionalShopping.classDepartureArrival
            Get
                Return objArrival
            End Get
            Set(ByVal value As PromotionalShopping.classDepartureArrival)
                objArrival = value
            End Set
        End Property
        Property Departure() As PromotionalShopping.classDepartureArrival
            Get
                Return objDeparture
            End Get
            Set(ByVal value As PromotionalShopping.classDepartureArrival)
                objDeparture = value
            End Set
        End Property
        Property Aerolinea() As PromotionalShopping.classAerolinea
            Get
                Return objAerolinea
            End Get
            Set(ByVal value As PromotionalShopping.classAerolinea)
                objAerolinea = value
            End Set
        End Property
        Property Operadora() As PromotionalShopping.classAerolinea
            Get
                Return objOperadora
            End Get
            Set(ByVal value As PromotionalShopping.classAerolinea)
                objOperadora = value
            End Set
        End Property
        Property StopQuantity() As Integer
            Get
                Return intStopQuantity
            End Get
            Set(ByVal value As Integer)
                intStopQuantity = value
            End Set
        End Property
    End Class
End Namespace