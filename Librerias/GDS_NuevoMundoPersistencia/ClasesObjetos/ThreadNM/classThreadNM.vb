Imports System.Threading
<Serializable()> _
Public Class classThreadNM
    Private intID As String = -1
    Private strPCC As String = String.Empty
    Private objThreadNM As Thread = Nothing
    Public Property ID() As Integer
        Get
            Return intID
        End Get
        Set(ByVal value As Integer)
            intID = value
        End Set
    End Property
    Public Property PCC() As String
        Get
            Return strPCC
        End Get
        Set(ByVal value As String)
            strPCC = value
        End Set
    End Property
    Public Property ThreadNM() As Thread
        Get
            Return objThreadNM
        End Get
        Set(ByVal value As Thread)
            objThreadNM = value
        End Set
    End Property
End Class
