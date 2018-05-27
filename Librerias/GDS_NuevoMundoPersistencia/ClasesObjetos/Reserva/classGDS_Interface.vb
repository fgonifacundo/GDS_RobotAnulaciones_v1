<Serializable()> _
Public Class classGDS_Interface
    Private intIdPTA As Integer = -1
    Private strNombreBaseDatos As String = Nothing
    Private strPseudo As String = Nothing
    Private intDkPaxDirecto As Integer = -1
    Private intConCotizador As Integer = -1
    Private intSucursal As Integer = -1
    Public Property ConCotizador() As Integer
        Get
            Return intConCotizador
        End Get
        Set(ByVal value As Integer)
            intConCotizador = value
        End Set
    End Property
    Public Property DkPaxDirecto() As Integer
        Get
            Return intDkPaxDirecto
        End Get
        Set(ByVal value As Integer)
            intDkPaxDirecto = value
        End Set
    End Property
    Public Property IdPTA() As Integer
        Get
            Return intIdPTA
        End Get
        Set(ByVal value As Integer)
            intIdPTA = value
        End Set
    End Property
    Public Property Sucursal() As Integer
        Get
            Return intSucursal
        End Get
        Set(ByVal value As Integer)
            intSucursal = value
        End Set
    End Property
    Public Property NombreBaseDatos() As String
        Get
            Return strNombreBaseDatos
        End Get
        Set(ByVal value As String)
            strNombreBaseDatos = value
        End Set
    End Property
    Public Property Pseudo() As String
        Get
            Return strPseudo
        End Get
        Set(ByVal value As String)
            strPseudo = value
        End Set
    End Property
End Class
