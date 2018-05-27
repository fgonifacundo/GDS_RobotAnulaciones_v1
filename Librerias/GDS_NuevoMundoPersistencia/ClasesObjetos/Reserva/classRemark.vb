Public Class classRemark
    Private strTypeRemark As String
    Private strCode As String
    Private strText As String
    Private strNroLinea As String

    Public Property TypeRemark() As String
        Get
            Return strTypeRemark
        End Get
        Set(ByVal value As String)
            strTypeRemark = value
        End Set
    End Property
    Public Property Code() As String
        Get
            Return strCode
        End Get
        Set(ByVal value As String)
            strCode = value
        End Set
    End Property
    Public Property Text() As String
        Get
            Return strText
        End Get
        Set(ByVal value As String)
            strText = value
        End Set
    End Property

    Public Property NroLinea() As String
        Get
            Return strNroLinea
        End Get
        Set(ByVal value As String)
            strNroLinea = value
        End Set
    End Property
    
End Class
