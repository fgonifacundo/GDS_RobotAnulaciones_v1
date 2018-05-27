<Serializable()> _
Public Class classPaxCotizacion
    Private intSecuencia As Integer = 0
    Private strNombre As String = Nothing
    Private strApellido As String = Nothing
    Private intGrupo As String = Nothing
    Private objNeto As classMonto = Nothing
    Private strDescuento As String = Nothing
    Public Property Grupo() As String
        Get
            Return intGrupo
        End Get
        Set(ByVal value As String)
            intGrupo = value
        End Set
    End Property
    Public Property Secuencia() As Integer
        Get
            Return intSecuencia
        End Get
        Set(ByVal value As Integer)
            intSecuencia = value
        End Set
    End Property
    Public Property Apellido() As String
        Get
            Return strApellido
        End Get
        Set(ByVal value As String)
            strApellido = value
        End Set
    End Property
    Public Property Descuento() As String
        Get
            Return strDescuento
        End Get
        Set(ByVal value As String)
            strDescuento = value
        End Set
    End Property
    Public Property Neto() As classMonto
        Get
            Return objNeto
        End Get
        Set(ByVal value As classMonto)
            objNeto = value
        End Set
    End Property
    Public Property Nombre() As String
        Get
            Return strNombre
        End Get
        Set(ByVal value As String)
            strNombre = value
        End Set
    End Property
End Class
