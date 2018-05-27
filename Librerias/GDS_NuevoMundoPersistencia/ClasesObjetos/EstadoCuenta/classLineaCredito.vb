<Serializable()> _
Public Class classLineaCredito
    Private strCreditoBase As String = Nothing
    Private strPendiente As String = Nothing
    Private strSobreGiro As String = Nothing
    Private strExtAcumulada As String = Nothing
    Private strExtConsumida As String = Nothing
    Private strDisponible As String = Nothing
    Public Property CreditoBase() As String
        Get
            Return strCreditoBase
        End Get
        Set(ByVal value As String)
            strCreditoBase = value
        End Set
    End Property
    Public Property Disponible() As String
        Get
            Return strDisponible
        End Get
        Set(ByVal value As String)
            strDisponible = value
        End Set
    End Property
    Public Property ExtAcumulada() As String
        Get
            Return strExtAcumulada
        End Get
        Set(ByVal value As String)
            strExtAcumulada = value
        End Set
    End Property
    Public Property ExtConsumida() As String
        Get
            Return strExtConsumida
        End Get
        Set(ByVal value As String)
            strExtConsumida = value
        End Set
    End Property
    Public Property Pendiente() As String
        Get
            Return strPendiente
        End Get
        Set(ByVal value As String)
            strPendiente = value
        End Set
    End Property
    Public Property SobreGiro() As String
        Get
            Return strSobreGiro
        End Get
        Set(ByVal value As String)
            strSobreGiro = value
        End Set
    End Property
End Class
