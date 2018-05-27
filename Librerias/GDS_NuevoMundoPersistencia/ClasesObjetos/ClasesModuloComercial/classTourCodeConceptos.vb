<Serializable()> _
    Public Class classTourCodeConceptos
    Private strLineaValidadora As String = Nothing
    Private strPNR As String = Nothing
    Private intDK As Integer = 0
    Private strIATA As String = Nothing
    Private lstConcepto As List(Of classDato) = Nothing
    Public Property DK() As Integer
        Get
            Return intDK
        End Get
        Set(ByVal value As Integer)
            intDK = value
        End Set
    End Property
    Public Property Concepto() As List(Of classDato)
        Get
            Return lstConcepto
        End Get
        Set(ByVal value As List(Of classDato))
            lstConcepto = value
        End Set
    End Property
    Public Property IATA() As String
        Get
            Return strIATA
        End Get
        Set(ByVal value As String)
            strIATA = value
        End Set
    End Property
    Public Property LineaValidadora() As String
        Get
            Return strLineaValidadora
        End Get
        Set(ByVal value As String)
            strLineaValidadora = value
        End Set
    End Property
    Public Property PNR() As String
        Get
            Return strPNR
        End Get
        Set(ByVal value As String)
            strPNR = value
        End Set
    End Property
End Class
