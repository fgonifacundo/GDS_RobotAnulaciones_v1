Option Explicit On
Option Strict On
Namespace WS_Login_SOAPEnvelope
    <Serializable()> _
    Public Class Login
        Private sUser As String = "5555"
        Private sPassword As String = "ws071707"
        Private sOrganization As String = "V70C"
        Private sConversation As String = "nuevomundoviajes.com"
        Public ReadOnly Property User() As String
            Get
                Return sUser
            End Get
        End Property
        Public ReadOnly Property Password() As String
            Get
                Return sPassword
            End Get
        End Property
        Public ReadOnly Property Organization() As String
            Get
                Return sOrganization
            End Get
        End Property
        Public ReadOnly Property Conversation() As String
            Get
                Return sConversation
            End Get
        End Property
    End Class

    <Serializable()> _
    Public Class LoginAgcorp
        Private sUser As String = "6666"
        Private sPassword As String = "red4521"
        Private sOrganization As String = "90JH"
        Private sConversation As String = "nuevomundoviajes.com"
        Public ReadOnly Property User() As String
            Get
                Return sUser
            End Get
        End Property
        Public ReadOnly Property Password() As String
            Get
                Return sPassword
            End Get
        End Property
        Public ReadOnly Property Organization() As String
            Get
                Return sOrganization
            End Get
        End Property
        Public ReadOnly Property Conversation() As String
            Get
                Return sConversation
            End Get
        End Property
    End Class
    <Serializable()> _
Public Class LoginResert
        Private sUser As String = "9623"
        Private sPassword As String = "ROBJAVA1"
        Private sOrganization As String = "W7H7"
        Private sConversation As String = "nuevomundoviajes.com"
        Public ReadOnly Property User() As String
            Get
                Return sUser
            End Get
        End Property
        Public ReadOnly Property Password() As String
            Get
                Return sPassword
            End Get
        End Property
        Public ReadOnly Property Organization() As String
            Get
                Return sOrganization
            End Get
        End Property
        Public ReadOnly Property Conversation() As String
            Get
                Return sConversation
            End Get
        End Property
    End Class
    <Serializable()> _
    Public Class SOAP_Envelope
        Private SE_CID As String = "cid:"
        Private SE_VERSION As String = "2003A.TsabreXML"
        Private sId As String
        Private sName As String
        Private sService As String
        Private sAction As String
        Private sCid As String
        Private sVersion As String
        Private sType As String = "sabreXML"
        Public Sub New(ByVal pId As String, _
                    Optional ByVal pName As String = Nothing, _
                    Optional ByVal pService As String = Nothing, _
                    Optional ByVal pAction As String = Nothing, _
                    Optional ByVal pCid As String = Nothing, _
                    Optional ByVal pVersion As String = Nothing)
            sName = pName
            sId = pId
            sService = pService
            sAction = pAction
            If Not pCid Is Nothing Then
                sCid = SE_CID & pCid
            End If
            sVersion = pVersion
        End Sub
        Public ReadOnly Property Id() As String
            Get
                Return sId
            End Get
        End Property
        Public Property Name() As String
            Get
                Return sName
            End Get
            Set(ByVal value As String)
                sName = value
            End Set
        End Property
        Public Property Service() As String
            Get
                Return sService
            End Get
            Set(ByVal value As String)
                sService = value
            End Set
        End Property
        Public Property Action() As String
            Get
                Return sAction
            End Get
            Set(ByVal value As String)
                sAction = value
            End Set
        End Property
        Public Property Cid() As String
            Get
                Return sCid
            End Get
            Set(ByVal value As String)
                sCid = SE_CID & value
            End Set
        End Property
        Public Property Version() As String
            Get
                Return sVersion
            End Get
            Set(ByVal value As String)
                If value <> vbNullString Then
                    sVersion = SE_VERSION & value
                End If
            End Set
        End Property
        Public Property Type() As String
            Get
                Return sType
            End Get
            Set(ByVal value As String)
                sType = value
            End Set
        End Property
    End Class
End Namespace