<Serializable()> _
Public Class classDatosAerolinea
    Private strNombre As String = Nothing
    Private strPrefijo As String = Nothing
    Private strIATA As String = Nothing
    Public Property Nombre() As String
        Get
            Return strNombre
        End Get
        Set(ByVal value As String)
            strNombre = value
        End Set
    End Property
    Public Property Prefijo() As String
        Get
            Return strPrefijo
        End Get
        Set(ByVal value As String)
            strPrefijo = value
        End Set
    End Property
    Public Property IATA() As String
        Get
            Return strIATA
        End Get
        Set(ByVal value As String)
            strIATA = value
        End Set
    End Property
End Class
