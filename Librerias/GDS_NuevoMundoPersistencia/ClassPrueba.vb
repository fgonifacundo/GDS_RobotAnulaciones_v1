<Serializable()> _
Public Class ClassPrueba
    Private strValorA As String
    Private dtFechas As Date
    Private strValorB As String
    Public Property ValorA() As String
        Get
            Return strValorA
        End Get
        Set(ByVal value As String)
            strValorA = value
        End Set
    End Property
    Public Property Fechas() As Date
        Get
            Return dtFechas
        End Get
        Set(ByVal value As Date)
            dtFechas = value
        End Set
    End Property
    Public Property ValorB() As String
        Get
            Return strValorB
        End Get
        Set(ByVal value As String)
            strValorB = value
        End Set
    End Property
End Class
