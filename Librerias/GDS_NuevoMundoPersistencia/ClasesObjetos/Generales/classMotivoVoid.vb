Public Class classMotivoVoid

    Private strIdMotivo As String = Nothing
    Private strMotivo As String = Nothing

    Public Property IdMotivo() As String
        Get
            Return strIdMotivo
        End Get
        Set(ByVal value As String)
            strIdMotivo = value
        End Set
    End Property

    Public Property Motivo() As String
        Get
            Return strMotivo
        End Get
        Set(ByVal value As String)
            strMotivo = value
        End Set
    End Property


End Class
