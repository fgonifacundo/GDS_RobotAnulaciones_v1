<Serializable()> _
Public Class classFeeTarifaBulkResultado
    Private strPseudoOficina As String = Nothing
    Private intEs_porcentaje As Integer = -1
    Private strFee_Minimo As String = Nothing
    Private strFee_Maximo As String = Nothing
    Private intVenta_X_Web As Integer = -1
    Private intMuestra_Web_Agencia As Integer = -1
    Private intNo_permite_RUC As Integer = -1
    Private intEmite_con_TarjetaCredito As Integer = -1

    Public Property PseudoOficina() As String
        Get
            Return strPseudoOficina
        End Get
        Set(ByVal value As String)
            strPseudoOficina = value
        End Set
    End Property

    Public Property Es_porcentaje() As Integer
        Get
            Return intEs_porcentaje
        End Get
        Set(ByVal value As Integer)
            intEs_porcentaje = value
        End Set
    End Property

    Public Property Fee_Minimo() As String
        Get
            Return strFee_Minimo
        End Get
        Set(ByVal value As String)
            strFee_Minimo = value
        End Set
    End Property

    Public Property Fee_Maximo() As String
        Get
            Return strFee_Maximo
        End Get
        Set(ByVal value As String)
            strFee_Maximo = value
        End Set
    End Property

    Public Property Venta_X_Web() As Integer
        Get
            Return intVenta_X_Web
        End Get
        Set(ByVal value As Integer)
            intVenta_X_Web = value
        End Set
    End Property

    Public Property Muestra_Web_Agencia() As Integer
        Get
            Return intMuestra_Web_Agencia
        End Get
        Set(ByVal value As Integer)
            intMuestra_Web_Agencia = value
        End Set
    End Property

    Public Property No_permite_RUC() As Integer
        Get
            Return intNo_permite_RUC
        End Get
        Set(ByVal value As Integer)
            intNo_permite_RUC = value
        End Set
    End Property

    Public Property Emite_con_TarjetaCredito() As Integer
        Get
            Return intEmite_con_TarjetaCredito
        End Get
        Set(ByVal value As Integer)
            intEmite_con_TarjetaCredito = value
        End Set
    End Property

End Class
