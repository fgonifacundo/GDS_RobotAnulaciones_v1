Public Class classSecuenciaPseudobulk

    Private intSecuencia As Integer = 0

    Public Property Secuencia() As String

        Get
            Return intSecuencia
        End Get
        Set(ByVal value As String)
            intSecuencia = value
        End Set
    End Property
End Class
