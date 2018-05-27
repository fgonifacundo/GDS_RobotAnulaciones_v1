Public Class classPseudosBulkEvaluacion

    Private strRegla As String = Nothing
    Private strPseudosVenta As String = Nothing
    Private strPseudosEmision As String = Nothing

    Public Property Regla() As String

        Get
            Return strRegla
        End Get
        Set(ByVal value As String)
            strRegla = value
        End Set
    End Property

    Public Property PseudosVenta() As String

        Get
            Return strPseudosVenta
        End Get
        Set(ByVal value As String)
            strPseudosVenta = value
        End Set
    End Property

    Public Property PseudosEmision() As String

        Get
            Return strPseudosEmision
        End Get
        Set(ByVal value As String)
            strPseudosEmision = value
        End Set
    End Property

End Class
