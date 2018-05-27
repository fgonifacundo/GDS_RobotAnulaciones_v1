Public Class classControlMorosidad
    Private strNumeroBoleto As String = Nothing
    Private intIdCliente As Integer = 0
    Private strInicialesCounter As String = Nothing

    Public Property NumeroBoleto() As String
        Get
            Return strNumeroBoleto
        End Get
        Set(ByVal value As String)
            strNumeroBoleto = value
        End Set
    End Property

    Public Property IdCliente() As Integer
        Get
            Return intIdCliente
        End Get
        Set(ByVal value As Integer)
            intIdCliente = value
        End Set
    End Property

    Public Property InicialesCounter() As String
        Get
            Return strInicialesCounter
        End Get
        Set(ByVal value As String)
            strInicialesCounter = value
        End Set
    End Property
End Class
