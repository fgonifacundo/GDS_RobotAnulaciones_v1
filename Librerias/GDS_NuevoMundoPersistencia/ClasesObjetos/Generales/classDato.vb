<Serializable()> _
Public Class classDato
    Private intCodigo As Integer = -1
    Private strCodigo As String = -1
    Private strAuxiliar As String = Nothing
    Private strValor As String = Nothing
    Sub New()
    End Sub
    Sub New(ByVal intCodigo As Integer, _
            ByVal strCodigo As String, _
            ByVal strAuxiliar As String, _
            ByVal strValor As String)
        Me.intCodigo = intCodigo
        Me.strValor = strValor
        Me.strAuxiliar = strAuxiliar
        Me.strValor = strValor
    End Sub
    Public Property Codigo() As Integer
        Get
            Return intCodigo
        End Get
        Set(ByVal value As Integer)
            intCodigo = value
        End Set
    End Property
    Public Property sCodigo() As String
        Get
            Return strCodigo
        End Get
        Set(ByVal value As String)
            strCodigo = value
        End Set
    End Property
    Public Property sAuxiliar() As String
        Get
            Return strAuxiliar
        End Get
        Set(ByVal value As String)
            strAuxiliar = value
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
