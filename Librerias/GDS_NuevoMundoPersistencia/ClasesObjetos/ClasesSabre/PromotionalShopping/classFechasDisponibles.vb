Namespace PromotionalShopping
    <Serializable()> _
    Public Class classFechasDisponibles
        Private strFareBasis As String = Nothing
        Private strFechas As String = Nothing
        Private intDisponible As Integer = 0
        Property FareBasis() As String
            Get
                Return strFareBasis
            End Get
            Set(ByVal value As String)
                strFareBasis = value
            End Set
        End Property
        Property Fechas() As String
            Get
                Return strFechas
            End Get
            Set(ByVal value As String)
                strFechas = value
            End Set
        End Property
        Property Disponible() As Integer
            Get
                Return intDisponible
            End Get
            Set(ByVal value As Integer)
                intDisponible = value
            End Set
        End Property
    End Class
End Namespace
