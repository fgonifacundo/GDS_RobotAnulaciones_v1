<Serializable()> _
Public Class classCotizacion
    Private intDK As Integer = 0
    Private strFechaAlta As String = Nothing
    Private strEstado As String = Nothing
    Private strGrupo As String = Nothing
    Private strVendedor As String = Nothing
    Private lstPasajeros As List(Of classPaxCotizacion) = Nothing
    Public Property DK() As Integer
        Get
            Return intDK
        End Get
        Set(ByVal value As Integer)
            intDK = value
        End Set
    End Property
    Public Property Pasajeros() As List(Of classPaxCotizacion)
        Get
            Return lstPasajeros
        End Get
        Set(ByVal value As List(Of classPaxCotizacion))
            lstPasajeros = value
        End Set
    End Property
    Public Property Estado() As String
        Get
            Return strEstado
        End Get
        Set(ByVal value As String)
            strEstado = value
        End Set
    End Property
    Public Property FechaAlta() As String
        Get
            Return strFechaAlta
        End Get
        Set(ByVal value As String)
            strFechaAlta = value
        End Set
    End Property
    Public Property Grupo() As String
        Get
            Return strGrupo
        End Get
        Set(ByVal value As String)
            strGrupo = value
        End Set
    End Property
    Public Property Vendedor() As String
        Get
            Return strVendedor
        End Get
        Set(ByVal value As String)
            strVendedor = value
        End Set
    End Property
End Class
