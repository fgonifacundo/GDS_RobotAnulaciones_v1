Public Class classQueueInfo
    Private strName As String = Nothing
    Private strNumber As String = Nothing
    Private strPrefatoryInstructionCode As String = Nothing
    Private strPseudoCityCode As String = Nothing

    Public Property Name() As String
        Get
            Return strName
        End Get
        Set(ByVal value As String)
            strName = value
        End Set
    End Property

    Public Property Number() As String
        Get
            Return strNumber
        End Get
        Set(ByVal value As String)
            strNumber = value
        End Set
    End Property

    Public Property PrefatoryInstructionCode() As String
        Get
            Return strPrefatoryInstructionCode
        End Get
        Set(ByVal value As String)
            strPrefatoryInstructionCode = value
        End Set
    End Property

    Public Property PseudoCityCode() As String
        Get
            Return strPseudoCityCode
        End Get
        Set(ByVal value As String)
            strPseudoCityCode = value
        End Set
    End Property
End Class
