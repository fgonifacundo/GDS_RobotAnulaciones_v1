Namespace PromotionalShopping
    <Serializable()> _
    Public Class classVuelosDisponibilidad
        Private strFechaOut As String = Nothing
        Private objInBound As PromotionalShopping.classInbound = Nothing
        Private lstDetalleVuelosDisponibles As List(Of PromotionalShopping.classDetalleVuelosDisponibles) = Nothing
        Property FechaOut() As String
            Get
                Return strFechaOut
            End Get
            Set(ByVal value As String)
                strFechaOut = value
            End Set
        End Property
        Property InBound() As PromotionalShopping.classInbound
            Get
                Return objInBound
            End Get
            Set(ByVal value As PromotionalShopping.classInbound)
                objInBound = value
            End Set
        End Property
        Property DetalleVuelosDisponibles() As List(Of PromotionalShopping.classDetalleVuelosDisponibles)
            Get
                Return lstDetalleVuelosDisponibles
            End Get
            Set(ByVal value As List(Of PromotionalShopping.classDetalleVuelosDisponibles))
                lstDetalleVuelosDisponibles = value
            End Set
        End Property
    End Class
   
End Namespace
