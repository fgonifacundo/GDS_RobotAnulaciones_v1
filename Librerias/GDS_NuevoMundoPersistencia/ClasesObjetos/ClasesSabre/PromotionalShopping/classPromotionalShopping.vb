Namespace PromotionalShopping
    <Serializable()> _
    Public Class classPromotionalShopping
        Private lstFechasDisponibles As List(Of PromotionalShopping.classFechasDisponibles) = Nothing
        Private objVuelosDisponibles As PromotionalShopping.classVuelosDisponibilidad = Nothing
        Private objErrores As classErroresAlertas = Nothing
        Public Property FechasDisponibles() As List(Of PromotionalShopping.classFechasDisponibles)
            Get
                Return lstFechasDisponibles
            End Get
            Set(ByVal value As List(Of PromotionalShopping.classFechasDisponibles))
                lstFechasDisponibles = value
            End Set
        End Property
        Public Property VuelosDisponibles() As PromotionalShopping.classVuelosDisponibilidad
            Get
                Return objVuelosDisponibles
            End Get
            Set(ByVal value As PromotionalShopping.classVuelosDisponibilidad)
                objVuelosDisponibles = value
            End Set
        End Property
        Public Property Errores() As classErroresAlertas
            Get
                Return objErrores
            End Get
            Set(ByVal value As classErroresAlertas)
                objErrores = value
            End Set
        End Property
    End Class
End Namespace
