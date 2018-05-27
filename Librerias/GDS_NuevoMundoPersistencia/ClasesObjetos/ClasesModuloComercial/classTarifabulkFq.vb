Public Class classTarifabulkFq

    Private strCodigoPNR As String = Nothing
    Private strAirlines As String = Nothing
    Private strPassengerType As String = Nothing
    Private strFareBasis As String = Nothing
    Private strBookingClass As String = Nothing
    Private strCurrency As String = Nothing
    Private douBaseFare As Double = 0
    Private strExpirationDate As String = Nothing
    Private strEffectiveDate As String = Nothing
    Private strTicketDate As String = Nothing
    Private strAdvancePurchase As String = Nothing
    Private strMinStay As String = Nothing
    Private strMaxStay As String = Nothing
    Private strPseudo As String = Nothing
    Private strIds As String = Nothing
    Private strNumero As String = Nothing
    Private douFeeMaximo As Double = 0
    Private douFeeMinimo As Double = 0
    Private strDk As String = Nothing
    Private strAccountCode As String = Nothing
    Private strCabina As String = Nothing
    Private strPaxEquivalente As String = Nothing
    Private strReglas As String = Nothing
    Private strSeasonAlApplic As String = Nothing
    Private strIdPseudo As String = Nothing

    Public Property CodigoPNR() As String
        Get
            Return strCodigoPNR
        End Get
        Set(ByVal value As String)
            strCodigoPNR = value
        End Set
    End Property
    Public Property Airlines() As String
        Get
            Return strAirlines
        End Get
        Set(ByVal value As String)
            strAirlines = value
        End Set
    End Property

    Public Property PassengerType() As String
        Get
            Return strPassengerType
        End Get
        Set(ByVal value As String)
            strPassengerType = value
        End Set
    End Property

    Public Property FareBasis() As String
        Get
            Return strFareBasis
        End Get
        Set(ByVal value As String)
            strFareBasis = value
        End Set
    End Property

    Public Property BookingClass() As String
        Get
            Return strBookingClass
        End Get
        Set(ByVal value As String)
            strBookingClass = value
        End Set
    End Property

    Public Property Currency() As String
        Get
            Return strCurrency
        End Get
        Set(ByVal value As String)
            strCurrency = value
        End Set
    End Property

    Public Property BaseFare() As Double
        Get
            Return douBaseFare
        End Get
        Set(ByVal value As Double)
            douBaseFare = value
        End Set
    End Property

    Public Property ExpirationDate() As String
        Get
            Return strExpirationDate
        End Get
        Set(ByVal value As String)
            strExpirationDate = value
        End Set
    End Property

    Public Property EffectiveDate() As String
        Get
            Return strEffectiveDate
        End Get
        Set(ByVal value As String)
            strEffectiveDate = value
        End Set
    End Property

    Public Property TicketDate() As String
        Get
            Return strTicketDate
        End Get
        Set(ByVal value As String)
            strTicketDate = value
        End Set
    End Property

    Public Property AdvancePurchase() As String
        Get
            Return strAdvancePurchase
        End Get
        Set(ByVal value As String)
            strAdvancePurchase = value
        End Set
    End Property

    Public Property MinStay() As String
        Get
            Return strMinStay
        End Get
        Set(ByVal value As String)
            strMinStay = value
        End Set
    End Property

    Public Property MaxStay() As String
        Get
            Return strMaxStay
        End Get
        Set(ByVal value As String)
            strMaxStay = value
        End Set
    End Property

    Public Property Pseudo() As String
        Get
            Return strPseudo
        End Get
        Set(ByVal value As String)
            strPseudo = value
        End Set
    End Property

    Public Property Ids() As String
        Get
            Return strIds
        End Get
        Set(ByVal value As String)
            strIds = value
        End Set
    End Property

    Public Property Numero() As String
        Get
            Return strNumero
        End Get
        Set(ByVal value As String)
            strNumero = value
        End Set
    End Property

    Public Property FeeMaximo() As Double
        Get
            Return douFeeMaximo
        End Get
        Set(ByVal value As Double)
            douFeeMaximo = value
        End Set
    End Property
    Public Property FeeMinimo() As Double
        Get
            Return douFeeMinimo
        End Get
        Set(ByVal value As Double)
            douFeeMinimo = value
        End Set
    End Property

    Public Property Dk() As String
        Get
            Return strDk
        End Get
        Set(ByVal value As String)
            strDk = value
        End Set
    End Property

    Public Property AccountCode() As String
        Get
            Return strAccountCode
        End Get
        Set(ByVal value As String)
            strAccountCode = value
        End Set
    End Property

    Public Property Cabina() As String
        Get
            Return strCabina
        End Get
        Set(ByVal value As String)
            strCabina = value
        End Set
    End Property

    Public Property PaxEquivalente() As String
        Get
            Return strPaxEquivalente
        End Get
        Set(ByVal value As String)
            strPaxEquivalente = value
        End Set
    End Property

    Public Property Reglas() As String
        Get
            Return strReglas
        End Get
        Set(ByVal value As String)
            strReglas = value
        End Set
    End Property

    Public Property SeasonAlApplic() As String
        Get
            Return strSeasonAlApplic
        End Get
        Set(ByVal value As String)
            strSeasonAlApplic = value
        End Set
    End Property

    Public Property IdPseudo() As String
        Get
            Return strIdPseudo
        End Get
        Set(ByVal value As String)
            strIdPseudo = value
        End Set
    End Property

End Class
