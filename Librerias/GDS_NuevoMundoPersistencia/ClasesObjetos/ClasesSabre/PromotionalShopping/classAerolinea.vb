Namespace PromotionalShopping
    <Serializable()> _
    Public Class classAerolinea
        Private strCodigo As String = Nothing
        Private strNumeroVuelo As String = Nothing
        Private strNombre As String = Nothing
        Property Codigo() As String
            Get
                Return strCodigo
            End Get
            Set(ByVal value As String)
                strCodigo = value
            End Set
        End Property
        Property NumeroVuelo() As String
            Get
                Return strNumeroVuelo
            End Get
            Set(ByVal value As String)
                strNumeroVuelo = value
            End Set
        End Property
        Property Nombre() As String
            Get
                Return strNombre
            End Get
            Set(ByVal value As String)
                strNombre = value
            End Set
        End Property
    End Class
End Namespace
