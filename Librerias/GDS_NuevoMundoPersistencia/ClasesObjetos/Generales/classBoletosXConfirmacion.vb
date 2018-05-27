Public Class classBoletosXConfirmacion

    Private intIdEmitido As Integer = 0
    Private strIdFirmaCreaPnr As String = Nothing
    Private strIdReferencia As String = Nothing
    Private strIdSecuencia As String = Nothing

    Public Property IdEmitido() As Integer
        Get
            Return intIdEmitido
        End Get
        Set(ByVal value As Integer)
            intIdEmitido = value
        End Set
    End Property
    Public Property IdFirmaCreaPnr() As String
        Get
            Return strIdFirmaCreaPnr
        End Get
        Set(ByVal value As String)
            strIdFirmaCreaPnr = value
        End Set
    End Property
    Public Property IdReferencia() As String
        Get
            Return strIdReferencia
        End Get
        Set(ByVal value As String)
            strIdReferencia = value
        End Set
    End Property
    Public Property IdSecuencia() As String
        Get
            Return strIdSecuencia
        End Get
        Set(ByVal value As String)
            strIdSecuencia = value
        End Set
    End Property
End Class
