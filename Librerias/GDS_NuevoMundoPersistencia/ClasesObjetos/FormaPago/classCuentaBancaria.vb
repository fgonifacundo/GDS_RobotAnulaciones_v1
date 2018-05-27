<Serializable()> _
Public Class classCuentaBancaria
    Private strIDCuenta As String = Nothing
    Private strNombreBanco As String = Nothing
    Private strNumeroCuenta As String = Nothing
    Sub New()

    End Sub
    Sub New(ByVal xstrIDCuenta As String, _
            ByVal xstrNombreBanco As String, _
            ByVal xstrNumeroCuenta As String)
        strIDCuenta = xstrIDCuenta
        strNombreBanco = xstrNombreBanco
        strNumeroCuenta = xstrNumeroCuenta
    End Sub
    Property IDCuenta() As String
        Get
            Return strIDCuenta
        End Get
        Set(ByVal value As String)
            strIDCuenta = value
        End Set
    End Property
    Property NombreBanco() As String
        Get
            Return strNombreBanco
        End Get
        Set(ByVal value As String)
            strNombreBanco = value
        End Set
    End Property
    Property NumeroCuenta() As String
        Get
            Return strNumeroCuenta
        End Get
        Set(ByVal value As String)
            strNumeroCuenta = value
        End Set
    End Property
End Class
