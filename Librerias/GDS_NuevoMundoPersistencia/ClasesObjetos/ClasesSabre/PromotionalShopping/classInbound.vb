Namespace PromotionalShopping
    <Serializable()> _
    Public Class classInbound
        Private strFechaIn As String = Nothing
        Private strFareBasis As String = Nothing
        Private intAvailability As Integer = 0
        Property FechaIn() As String
            Get
                Return strFechaIn
            End Get
            Set(ByVal value As String)
                strFechaIn = value
            End Set
        End Property
        Property FareBasis() As String
            Get
                Return strFareBasis
            End Get
            Set(ByVal value As String)
                strFareBasis = value
            End Set
        End Property
        Property Availability() As Integer
            Get
                Return intAvailability
            End Get
            Set(ByVal value As Integer)
                intAvailability = value
            End Set
        End Property
    End Class
End Namespace
