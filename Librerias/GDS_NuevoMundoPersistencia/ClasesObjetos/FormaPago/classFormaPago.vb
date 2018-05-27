<Serializable()> _
Public Class classFormaPago
    Private strTipo As String = Nothing
    Private objCash As classMonto = Nothing
    Private objDeposito As List(Of classDeposito_) = Nothing
    Private objCardCash As classCardCash = Nothing
    Private objDepositoTarjeta As classDepositoTarjeta = Nothing
    Private objTarjeta As List(Of classTarjeta_) = Nothing
    Public Property CardCash() As classCardCash
        Get
            Return objCardCash
        End Get
        Set(ByVal value As classCardCash)
            objCardCash = value
        End Set
    End Property
    Public Property Cash() As classMonto
        Get
            Return objCash
        End Get
        Set(ByVal value As classMonto)
            objCash = value
        End Set
    End Property
    Public Property Deposito() As List(Of classDeposito_)
        Get
            Return objDeposito
        End Get
        Set(ByVal value As List(Of classDeposito_))
            objDeposito = value
        End Set
    End Property
    Public Property DepositoTarjeta() As classDepositoTarjeta
        Get
            Return objDepositoTarjeta
        End Get
        Set(ByVal value As classDepositoTarjeta)
            objDepositoTarjeta = value
        End Set
    End Property
    Public Property Tarjeta() As List(Of classTarjeta_)
        Get
            Return objTarjeta
        End Get
        Set(ByVal value As List(Of classTarjeta_))
            objTarjeta = value
        End Set
    End Property
    Public Property Tipo() As String
        Get
            Return strTipo
        End Get
        Set(ByVal value As String)
            strTipo = value
        End Set
    End Property
End Class
