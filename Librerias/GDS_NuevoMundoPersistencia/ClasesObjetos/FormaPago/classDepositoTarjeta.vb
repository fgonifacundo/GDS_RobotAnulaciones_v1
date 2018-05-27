<Serializable()> _
Public Class classDepositoTarjeta
    Private objDeposito As List(Of classDeposito_) = Nothing
    Private objTarjeta As classTarjeta_ = Nothing
    Public Property Deposito() As List(Of classDeposito_)
        Get
            Return objDeposito
        End Get
        Set(ByVal value As List(Of classDeposito_))
            objDeposito = value
        End Set
    End Property
    Public Property Tarjeta() As classTarjeta_
        Get
            Return objTarjeta
        End Get
        Set(ByVal value As classTarjeta_)
            objTarjeta = value
        End Set
    End Property
End Class
