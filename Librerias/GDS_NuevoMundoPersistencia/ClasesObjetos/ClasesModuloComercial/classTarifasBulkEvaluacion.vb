Public Class classTarifasBulkEvaluacion
    Private strCodigoReserva As String
    Private intCodigoConcepto As Integer
    Private strValor As String
    Public Property CodigoReserva() As String
        Get
            Return strCodigoReserva
        End Get
        Set(ByVal value As String)
            strCodigoReserva = value
        End Set
    End Property
    Public Property CodigoConcepto() As Integer
        Get
            Return intCodigoConcepto
        End Get
        Set(ByVal value As Integer)
            intCodigoConcepto = value
        End Set
    End Property
    Public Property Valor() As String
        Get
            Return strValor
        End Get
        Set(ByVal value As String)
            strValor = value
        End Set
    End Property
End Class
