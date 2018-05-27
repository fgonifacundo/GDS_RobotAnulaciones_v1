Public Class ClsAerolinea
    Private stridtransportador As String
    Private strnombre As String
    Private stridprefijo As String
    Private stridiatatransportador As String

    Public Property idtransportador() As String
        Get
            Return stridtransportador
        End Get
        ''-----------------------------------
        Set(ByVal value As String)
            stridtransportador = value
        End Set
        ''--------------------------------
    End Property

    Public Property nombre() As String
        Get
            Return strnombre
        End Get
        ''-----------------------------------
        Set(ByVal value As String)
            strnombre = value
        End Set
        ''--------------------------------
    End Property

    Public Property idprefijo() As String
        Get
            Return stridprefijo
        End Get
        ''-----------------------------------
        Set(ByVal value As String)
            stridprefijo = value
        End Set
        ''--------------------------------
    End Property

    Public Property idiatatransportador() As String
        Get
            Return stridiatatransportador
        End Get
        ''-----------------------------------
        Set(ByVal value As String)
            stridiatatransportador = value
        End Set
        ''--------------------------------
    End Property

End Class
