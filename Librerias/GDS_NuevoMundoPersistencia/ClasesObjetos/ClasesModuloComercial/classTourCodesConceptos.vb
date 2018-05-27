Public Class classTourCodesConceptos
    Private intCodigoConcepto As Integer
    Private strDescripcionConcepto As String
    Private intCodigoTipoDato As Integer
    Private strValor As String

    Public Property CodigoConcepto() As Integer
        Get
            Return intCodigoConcepto
        End Get
        Set(ByVal value As Integer)
            intCodigoConcepto = value
        End Set
    End Property

    Public Property DescripcionConcepto() As String
        Get
            Return strDescripcionConcepto
        End Get
        Set(ByVal value As String)
            strDescripcionConcepto = value
        End Set
    End Property

    Public Property CodigoTipoDato() As Integer
        Get
            Return intCodigoTipoDato
        End Get
        Set(ByVal value As Integer)
            intCodigoTipoDato = value
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
