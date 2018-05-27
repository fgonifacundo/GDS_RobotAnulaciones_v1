Public Class classPseudoBulkConceptos
    Private strCodigoPNR As String = Nothing
    Private intCodigoConcepto As Integer = 0
    Private strIdTransportador As String = Nothing
    Public Property CodigoPNR() As String

        Get
            Return strCodigoPNR
        End Get
        Set(ByVal value As String)
            strCodigoPNR = value
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
    Public Property IdTransportador() As String

        Get
            Return strIdTransportador
        End Get
        Set(ByVal value As String)
            strIdTransportador = value
        End Set
    End Property
End Class
