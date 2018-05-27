<Serializable()> _
Public Class classUsuario
    Private intIdUsuarioWeb As Integer = 0
    Private strNombre As String = Nothing
    Private strApellido As String = Nothing
    Private strEmail As String = Nothing
    Private strCargo As String = Nothing
    Private strIdVendedorWeb As String = Nothing
    Property IdUsuarioWeb() As Integer
        Get
            Return intIdUsuarioWeb
        End Get
        Set(ByVal value As Integer)
            intIdUsuarioWeb = value
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
    Property Apellido() As String
        Get
            Return strApellido
        End Get
        Set(ByVal value As String)
            strApellido = value
        End Set
    End Property
    Property Email() As String
        Get
            Return strEmail
        End Get
        Set(ByVal value As String)
            strEmail = value
        End Set
    End Property
    Property Cargo() As String
        Get
            Return strCargo
        End Get
        Set(ByVal value As String)
            strCargo = value
        End Set
    End Property
    Property IdVendedorWeb() As String
        Get
            Return strIdVendedorWeb
        End Get
        Set(ByVal value As String)
            strIdVendedorWeb = value
        End Set
    End Property
End Class
