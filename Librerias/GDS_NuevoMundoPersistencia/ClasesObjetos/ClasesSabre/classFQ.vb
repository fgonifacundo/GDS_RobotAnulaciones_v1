Namespace classFQ
    <Serializable()> _
    Public Class classFare
        Private lstFareRS As List(Of classFareRS) = Nothing
        Private strHostCommand As String = Nothing
        Private objErroresAlertas As classErroresAlertas = Nothing
        Public Property FareRS() As List(Of classFareRS)
            Get
                Return lstFareRS
            End Get
            Set(ByVal value As List(Of classFareRS))
                lstFareRS = value
            End Set
        End Property
        Public Property ErroresAlertas() As classErroresAlertas
            Get
                Return objErroresAlertas
            End Get
            Set(ByVal value As classErroresAlertas)
                objErroresAlertas = value
            End Set
        End Property
        Public Property HostCommand() As String
            Get
                Return strHostCommand
            End Get
            Set(ByVal value As String)
                strHostCommand = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class classFareRS
        Private intID As Integer = -1
        Private intNumero As Integer = -1
        Private intDK As Integer = 0
        Private strPNR As String = String.Empty
        Private strAirLines As String = String.Empty
        Private strPassengerType As List(Of classDatosTipoPasajero) = Nothing
        Private strFareBasis As String = "0.00"
        Private strCabin As String = String.Empty
        Private strBookingClass As String = String.Empty
        Private strCurrency As String = String.Empty
        Private strBaseFare As String = String.Empty
        Private strTaxesFare As String = String.Empty
        Private strTotalFare As String = String.Empty
        Private strExpirationDate As String = String.Empty
        Private strEffectiveDate As String = String.Empty
        Private strTicketDate As String = String.Empty
        Private strAdvancePurchase As String = String.Empty
        Private strMinStay As String = String.Empty
        Private strMaxStay As String = String.Empty
        Private strFeeMinino As String = String.Empty
        Private strFeeMaximo As String = String.Empty
        Private strSeasonalApplication As String = String.Empty
        Private lstPseudos As List(Of classPseudoFare) = Nothing
        Private lstRuleCategory As List(Of String) = Nothing
        Private strTipoTarifa As String = Nothing
        Public Property ID() As Integer
            Get
                Return intID
            End Get
            Set(ByVal value As Integer)
                intID = value
            End Set
        End Property
        Public Property Numero() As Integer
            Get
                Return intNumero
            End Get
            Set(ByVal value As Integer)
                intNumero = value
            End Set
        End Property
        Public Property DK() As Integer
            Get
                Return intDK
            End Get
            Set(ByVal value As Integer)
                intDK = value
            End Set
        End Property
        Public Property PNR() As String
            Get
                Return strPNR
            End Get
            Set(ByVal value As String)
                strPNR = value
            End Set
        End Property
        Public Property AirLines() As String
            Get
                Return strAirLines
            End Get
            Set(ByVal value As String)
                strAirLines = value
            End Set
        End Property
        Public Property PassengerType() As List(Of classDatosTipoPasajero)
            Get
                Return strPassengerType
            End Get
            Set(ByVal value As List(Of classDatosTipoPasajero))
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
        Public Property Cabin() As String
            Get
                Return strCabin
            End Get
            Set(ByVal value As String)
                strCabin = value
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
        Public Property BaseFare() As String
            Get
                Return strBaseFare
            End Get
            Set(ByVal value As String)
                strBaseFare = value
                'If Not String.IsNullOrEmpty(value) Then
                '    strBaseFare = Format(CDbl(value), "####.00")
                'Else
                '    strBaseFare = "0.00"
                'End If
            End Set
        End Property
        Public Property TaxesFare() As String
            Get
                Return strTaxesFare
            End Get
            Set(ByVal value As String)
                strTaxesFare = value
            End Set
        End Property
        Public Property TotalFare() As String
            Get
                Return strTotalFare
            End Get
            Set(ByVal value As String)
                strTotalFare = value
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
        Public Property FeeMinino() As String
            Get
                Return strFeeMinino
            End Get
            Set(ByVal value As String)
                strFeeMinino = value
            End Set
        End Property
        Public Property FeeMaximo() As String
            Get
                Return strFeeMaximo
            End Get
            Set(ByVal value As String)
                strFeeMaximo = value
            End Set
        End Property
        Public Property SeasonalApplication() As String
            Get
                Return strSeasonalApplication
            End Get
            Set(ByVal value As String)
                strSeasonalApplication = value
            End Set
        End Property
        Public Property Pseudos() As List(Of classPseudoFare)
            Get
                Return lstPseudos
            End Get
            Set(ByVal value As List(Of classPseudoFare))
                lstPseudos = value
            End Set
        End Property
        Public Property RuleCategory() As List(Of String)
            Get
                Return lstRuleCategory
            End Get
            Set(ByVal value As List(Of String))
                lstRuleCategory = value
            End Set
        End Property
        Public Property tipoTarifa() As String
            Get
                Return strTipoTarifa
            End Get
            Set(ByVal value As String)
                strTipoTarifa = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class classPseudoFare
        Private strPseudo As String = String.Empty
        Private strIDs As String = String.Empty
        Private strNumero As String = String.Empty
        Private strFeeMinimo As String = String.Empty
        Private strFeeMaximo As String = String.Empty
        Private strAccountCode As String = String.Empty
        Public Property Pseudo() As String
            Get
                Return strPseudo
            End Get
            Set(ByVal value As String)
                strPseudo = value
            End Set
        End Property
        Public Property IDs() As String
            Get
                Return strIDs
            End Get
            Set(ByVal value As String)
                strIDs = value
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
        Public Property FeeMinimo() As String
            Get
                Return strFeeMinimo
            End Get
            Set(ByVal value As String)
                strFeeMinimo = value
            End Set
        End Property
        Public Property FeeMaximo() As String
            Get
                Return strFeeMaximo
            End Get
            Set(ByVal value As String)
                strFeeMaximo = value
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
    End Class
End Namespace

