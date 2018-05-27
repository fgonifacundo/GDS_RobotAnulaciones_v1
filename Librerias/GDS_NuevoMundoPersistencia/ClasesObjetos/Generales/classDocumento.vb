<Serializable()> _
Public Class classDocumento
    Private strTipo As String = Nothing
    Private strNum_Nombre As String = Nothing
    Private strNumero As String = Nothing
    Public Property Num_Nombre() As String
        Get
            Return strNum_Nombre
        End Get
        Set(ByVal value As String)
            strNum_Nombre = value
        End Set
    End Property
    Public Property Tipo() As String
        Get
            Return strTipo
        End Get
        Set(ByVal value As String)
            strTipo = value
        End Set
    End Property
    Public Property Numero() As String
        Get
            Return strNumero
        End Get
        Set(ByVal value As String)
            strNumero = value
        End Set
    End Property
End Class
