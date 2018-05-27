Namespace classTicketCoupon
    <Serializable()> _
    Public Class TCTicket
        Private strTicketNumber As String = Nothing
        Private strAgencyCity As String = Nothing
        Private strAgentWorkArea As String = Nothing
        Private strIATA_Number As String = Nothing
        Private strIssuingAgent As String = Nothing
        Private strPrimeHostID As String = Nothing
        Private strPseudoCityCode As String = Nothing
        Private strTransactionDateTime As String = Nothing
        Private objTCCouponData As TCCouponData = Nothing
        Private strTimeStamp As String = Nothing
        Private strStatus As String = Nothing
        Private strLNIATA As String = Nothing
        Private strHostCommand As String = Nothing
        Private objErroresAlertas As classErroresAlertas = Nothing
        Property TicketNumber() As String
            Get
                Return strTicketNumber
            End Get
            Set(ByVal value As String)
                strTicketNumber = value
            End Set
        End Property
        Property AgencyCity() As String
            Get
                Return strAgencyCity
            End Get
            Set(ByVal value As String)
                strAgencyCity = value
            End Set
        End Property
        Property AgentWorkArea() As String
            Get
                Return strAgentWorkArea
            End Get
            Set(ByVal value As String)
                strAgentWorkArea = value
            End Set
        End Property
        Property IATA_Number() As String
            Get
                Return strIATA_Number
            End Get
            Set(ByVal value As String)
                strIATA_Number = value
            End Set
        End Property
        Property IssuingAgent() As String
            Get
                Return strIssuingAgent
            End Get
            Set(ByVal value As String)
                strIssuingAgent = value
            End Set
        End Property
        Property PrimeHostID() As String
            Get
                Return strPrimeHostID
            End Get
            Set(ByVal value As String)
                strPrimeHostID = value
            End Set
        End Property
        Property PseudoCityCode() As String
            Get
                Return strPseudoCityCode
            End Get
            Set(ByVal value As String)
                strPseudoCityCode = value
            End Set
        End Property
        Property TransactionDateTime() As String
            Get
                Return strTransactionDateTime
            End Get
            Set(ByVal value As String)
                strTransactionDateTime = value
            End Set
        End Property
        Property TCCouponData() As TCCouponData
            Get
                Return objTCCouponData
            End Get
            Set(ByVal value As TCCouponData)
                objTCCouponData = value
            End Set
        End Property
        Public Property Status() As String
            Get
                Return strStatus
            End Get
            Set(ByVal value As String)
                strStatus = value
            End Set
        End Property
        Public Property TimeStamp() As String
            Get
                Return strTimeStamp
            End Get
            Set(ByVal value As String)
                strTimeStamp = value
            End Set
        End Property
        Public Property LNIATA() As String
            Get
                Return strLNIATA
            End Get
            Set(ByVal value As String)
                strLNIATA = value
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
        Public Property ErroresAlertas() As classErroresAlertas
            Get
                Return objErroresAlertas
            End Get
            Set(ByVal value As classErroresAlertas)
                objErroresAlertas = value
            End Set
        End Property
    End Class



    <Serializable()> _
    Public Class TCCouponData
        Private strInformationSource As String = Nothing
        Private strIssueDate As String = Nothing
        Private strNumBooklets As String = Nothing
        Private strProductID As String = Nothing
        Private strTicketMedia As String = Nothing
        Private strTicketMode As String = Nothing
        Private strConjunctiveTicketNumbers As String = Nothing
        Private strExchangeInd As String = Nothing
        Private objAirItineraryPricing As TCAirItineraryPricing = Nothing
        Private objTCCoupon As List(Of TCCoupon)
        Private objTCCustomer As TCCustomer = Nothing
        Private objItineraryRef As TCItineraryRef = Nothing
        Private objExchangeData As TCExchangeData = Nothing
        Property InformationSource() As String
            Get
                Return strInformationSource
            End Get
            Set(ByVal value As String)
                strInformationSource = value
            End Set
        End Property
        Property IssueDate() As String
            Get
                Return strIssueDate
            End Get
            Set(ByVal value As String)
                strIssueDate = value
            End Set
        End Property
        Property NumBooklets() As String
            Get
                Return strNumBooklets
            End Get
            Set(ByVal value As String)
                strNumBooklets = value
            End Set
        End Property
        Property ProductID() As String
            Get
                Return strProductID
            End Get
            Set(ByVal value As String)
                strProductID = value
            End Set
        End Property
        Property TicketMedia() As String
            Get
                Return strTicketMedia
            End Get
            Set(ByVal value As String)
                strTicketMedia = value
            End Set
        End Property
        Property TicketMode() As String
            Get
                Return strTicketMode
            End Get
            Set(ByVal value As String)
                strTicketMode = value
            End Set
        End Property
        Property ConjunctiveTicketNumbers() As String
            Get
                Return strConjunctiveTicketNumbers
            End Get
            Set(ByVal value As String)
                strConjunctiveTicketNumbers = value
            End Set
        End Property
        Property ExchangeInd() As String
            Get
                Return strExchangeInd
            End Get
            Set(ByVal value As String)
                strExchangeInd = value
            End Set
        End Property
        Property AirItineraryPricing() As TCAirItineraryPricing
            Get
                Return objAirItineraryPricing
            End Get
            Set(ByVal value As TCAirItineraryPricing)
                objAirItineraryPricing = value
            End Set
        End Property
        Property TCCoupon() As List(Of TCCoupon)
            Get
                Return objTCCoupon
            End Get
            Set(ByVal value As List(Of TCCoupon))
                objTCCoupon = value
            End Set
        End Property
        Property TCCustomer() As TCCustomer
            Get
                Return objTCCustomer
            End Get
            Set(ByVal value As TCCustomer)
                objTCCustomer = value
            End Set
        End Property
        Property ItineraryRef() As TCItineraryRef
            Get
                Return objItineraryRef
            End Get
            Set(ByVal value As TCItineraryRef)
                objItineraryRef = value
            End Set
        End Property
        Property ExchangeData() As TCExchangeData
            Get
                Return objExchangeData
            End Get
            Set(ByVal value As TCExchangeData)
                objExchangeData = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class TCCoupon
        Private strCodedStatus As String = Nothing
        Private intNumber As Integer = Nothing
        Private strStatusCode As String = Nothing
        Private objTCFlightSegment As TCFlightSegment

        Property CodedStatus() As String
            Get
                Return strCodedStatus
            End Get
            Set(ByVal value As String)
                strCodedStatus = value
            End Set
        End Property
        Property Number() As Integer
            Get
                Return intNumber
            End Get
            Set(ByVal value As Integer)
                intNumber = value
            End Set
        End Property
        Property StatusCode() As String
            Get
                Return strStatusCode
            End Get
            Set(ByVal value As String)
                strStatusCode = value
            End Set
        End Property
        Property TCFlightSegment() As TCFlightSegment
            Get
                Return objTCFlightSegment
            End Get
            Set(ByVal value As TCFlightSegment)
                objTCFlightSegment = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class TCFlightSegment
        Private strDepartureDateTime As String = Nothing
        Private strFlightNumber As String = Nothing
        Private strResBookDesigCode As String = Nothing
        Private strRPH As String = Nothing
        Private strConnectionInd As String = Nothing
        Private strDestinationLocation As String = Nothing
        Private objFareBasis As TCFareBasis = Nothing
        Private objMarketingAirline As TCMarketingAirline = Nothing
        Private strOperatingAirline As String = Nothing
        Private strOriginLocation As String = Nothing
        Property DepartureDateTime() As String
            Get
                Return strDepartureDateTime
            End Get
            Set(ByVal value As String)
                strDepartureDateTime = value
            End Set
        End Property
        Property FlightNumber() As String
            Get
                Return strFlightNumber
            End Get
            Set(ByVal value As String)
                strFlightNumber = value
            End Set
        End Property
        Property ResBookDesigCode() As String
            Get
                Return strResBookDesigCode
            End Get
            Set(ByVal value As String)
                strResBookDesigCode = value
            End Set
        End Property
        Property RPH() As String
            Get
                Return strRPH
            End Get
            Set(ByVal value As String)
                strRPH = value
            End Set
        End Property
        Property ConnectionInd() As String
            Get
                Return strConnectionInd
            End Get
            Set(ByVal value As String)
                strConnectionInd = value
            End Set
        End Property
        Property DestinationLocation() As String
            Get
                Return strDestinationLocation
            End Get
            Set(ByVal value As String)
                strDestinationLocation = value
            End Set
        End Property
        Property FareBasis() As TCFareBasis
            Get
                Return objFareBasis
            End Get
            Set(ByVal value As TCFareBasis)
                objFareBasis = value
            End Set
        End Property
        Property MarketingAirline() As TCMarketingAirline
            Get
                Return objMarketingAirline
            End Get
            Set(ByVal value As TCMarketingAirline)
                objMarketingAirline = value
            End Set
        End Property
        Property OperatingAirline() As String
            Get
                Return strOperatingAirline
            End Get
            Set(ByVal value As String)
                strOperatingAirline = value
            End Set
        End Property
        Property OriginLocation() As String
            Get
                Return strOriginLocation
            End Get
            Set(ByVal value As String)
                strOriginLocation = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class TCFareBasis
        Private strCode As String = Nothing
        Private strFareBasisTD As String = Nothing
        Property Code() As String
            Get
                Return strCode
            End Get
            Set(ByVal value As String)
                strCode = value
            End Set
        End Property
        Property FareBasisTD() As String
            Get
                Return strFareBasisTD
            End Get
            Set(ByVal value As String)
                strFareBasisTD = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class TCMarketingAirline
        Private strCode As String = Nothing
        Private strFlightNumber As String = Nothing
        Property Code() As String
            Get
                Return strCode
            End Get
            Set(ByVal value As String)
                strCode = value
            End Set
        End Property
        Property FlightNumber() As String
            Get
                Return strFlightNumber
            End Get
            Set(ByVal value As String)
                strFlightNumber = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class TCCustomer
        Private objTCPayment As List(Of TCPayment) = Nothing
        Private objTCPersonName As TCPersonName = Nothing
        Property TCPayment() As List(Of TCPayment)
            Get
                Return objTCPayment
            End Get
            Set(ByVal value As List(Of TCPayment))
                objTCPayment = value
            End Set
        End Property
        Property TCPersonName() As TCPersonName
            Get
                Return objTCPersonName
            End Get
            Set(ByVal value As TCPersonName)
                objTCPersonName = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class TCPayment
        Private strCode As String = Nothing
        Private strApprovalID As String = Nothing
        Private strReferenceNumber As String = Nothing
        Private strRPH As String = Nothing
        Private strType As String = Nothing
        Private strText As String = Nothing
        Private strConditions As String = Nothing
        Private objTCPaymentCard As TCPaymentCard = Nothing
        Property Code() As String
            Get
                Return strCode
            End Get
            Set(ByVal value As String)
                strCode = value
            End Set
        End Property
        Property ApprovalID() As String
            Get
                Return strApprovalID
            End Get
            Set(ByVal value As String)
                strApprovalID = value
            End Set
        End Property
        Property ReferenceNumber() As String
            Get
                Return strReferenceNumber
            End Get
            Set(ByVal value As String)
                strReferenceNumber = value
            End Set
        End Property
        Property RPH() As String
            Get
                Return strRPH
            End Get
            Set(ByVal value As String)
                strRPH = value
            End Set
        End Property
        Property Type() As String
            Get
                Return strType
            End Get
            Set(ByVal value As String)
                strType = value
            End Set
        End Property
        Property Text() As String
            Get
                Return strText
            End Get
            Set(ByVal value As String)
                strText = value
            End Set
        End Property
        Property Conditions() As String
            Get
                Return strConditions
            End Get
            Set(ByVal value As String)
                strConditions = value
            End Set
        End Property
        Property TCPaymentCard() As TCPaymentCard
            Get
                Return objTCPaymentCard
            End Get
            Set(ByVal value As TCPaymentCard)
                objTCPaymentCard = value
            End Set
        End Property
    End Class
    <Serializable()> _
        Public Class TCPaymentCard
        Private strCode As String = Nothing
        Private strAmount As String = Nothing
        Private strExpirationDate As String = Nothing
        Property Code() As String
            Get
                Return strCode
            End Get
            Set(ByVal value As String)
                strCode = value
            End Set
        End Property
        Property Amount() As String
            Get
                Return strAmount
            End Get
            Set(ByVal value As String)
                strAmount = value
            End Set
        End Property
        Property ExpirationDate() As String
            Get
                Return strExpirationDate
            End Get
            Set(ByVal value As String)
                strExpirationDate = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class TCPersonName
        Private strNameReference As String = Nothing
        Private strPassengerType As String = Nothing
        Private strGivenName As String = Nothing
        Private strSurname As String = Nothing
        Property NameReference() As String
            Get
                Return strNameReference
            End Get
            Set(ByVal value As String)
                strNameReference = value
            End Set
        End Property
        Property PassengerType() As String
            Get
                Return strPassengerType
            End Get
            Set(ByVal value As String)
                strPassengerType = value
            End Set
        End Property
        Property GivenName() As String
            Get
                Return strGivenName
            End Get
            Set(ByVal value As String)
                strGivenName = value
            End Set
        End Property
        Property Surname() As String
            Get
                Return strSurname
            End Get
            Set(ByVal value As String)
                strSurname = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class TCItineraryRef
        Private strCustomerIdentifier As String = Nothing
        Private strID As String = Nothing
        Property CustomerIdentifier() As String
            Get
                Return strCustomerIdentifier
            End Get
            Set(ByVal value As String)
                strCustomerIdentifier = value
            End Set
        End Property
        Property ID() As String
            Get
                Return strID
            End Get
            Set(ByVal value As String)
                strID = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class TCAirItineraryPricing
        Private strEndorsements As List(Of String) = Nothing
        Private strFareCalculation As String = Nothing
        Private objTCItinTotalFare As TCItinTotalFare = Nothing
        Private strPassengerTypeQuantity As String
        Property Endorsements() As List(Of String)
            Get
                Return strEndorsements
            End Get
            Set(ByVal value As List(Of String))
                strEndorsements = value
            End Set
        End Property
        Property FareCalculation() As String
            Get
                Return strFareCalculation
            End Get
            Set(ByVal value As String)
                strFareCalculation = value
            End Set
        End Property
        Property TCItinTotalFare() As TCItinTotalFare
            Get
                Return objTCItinTotalFare
            End Get
            Set(ByVal value As TCItinTotalFare)
                objTCItinTotalFare = value
            End Set
        End Property
        Property PassengerTypeQuantity() As String
            Get
                Return strPassengerTypeQuantity
            End Get
            Set(ByVal value As String)
                strPassengerTypeQuantity = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class TCItinTotalFare
        Private objTCBaseFare As TCBaseFare = Nothing
        Private objTCEquivalenteFare As TCEquivalenteFare = Nothing
        Private objTCNetFare As TCNetFare = Nothing
        Private objTCTaxes As TCTaxes = Nothing
        Private objTCTotalFare As TCTotalFare = Nothing
        Property TCBaseFare() As TCBaseFare
            Get
                Return objTCBaseFare
            End Get
            Set(ByVal value As TCBaseFare)
                objTCBaseFare = value
            End Set
        End Property
        Property TCEquivalenteFare() As TCEquivalenteFare
            Get
                Return objTCEquivalenteFare
            End Get
            Set(ByVal value As TCEquivalenteFare)
                objTCEquivalenteFare = value
            End Set
        End Property
        Property TCNetFare() As TCNetFare
            Get
                Return objTCNetFare
            End Get
            Set(ByVal value As TCNetFare)
                objTCNetFare = value
            End Set
        End Property
        Property TCTaxes() As TCTaxes
            Get
                Return objTCTaxes
            End Get
            Set(ByVal value As TCTaxes)
                objTCTaxes = value
            End Set
        End Property
        Property TCTotalFare() As TCTotalFare
            Get
                Return objTCTotalFare
            End Get
            Set(ByVal value As TCTotalFare)
                objTCTotalFare = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class TCBaseFare
        Private strAmount As String = Nothing
        Private strCurrencyCode As String = Nothing
        Property Amount() As String
            Get
                Return strAmount
            End Get
            Set(ByVal value As String)
                strAmount = value
            End Set
        End Property
        Property CurrencyCode() As String
            Get
                Return strCurrencyCode
            End Get
            Set(ByVal value As String)
                strCurrencyCode = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class TCEquivalenteFare
        Private strAmount As String = Nothing
        Private strCurrencyCode As String = Nothing
        Property Amount() As String
            Get
                Return strAmount
            End Get
            Set(ByVal value As String)
                strAmount = value
            End Set
        End Property
        Property CurrencyCode() As String
            Get
                Return strCurrencyCode
            End Get
            Set(ByVal value As String)
                strCurrencyCode = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class TCNetFare
        Private strAmount As String = Nothing
        Private strAmountType As String = Nothing
        Private strCreditCardAmount As String = Nothing
        Property Amount() As String
            Get
                Return strAmount
            End Get
            Set(ByVal value As String)
                strAmount = value
            End Set
        End Property
        Property AmountType() As String
            Get
                Return strAmountType
            End Get
            Set(ByVal value As String)
                strAmountType = value
            End Set
        End Property
        Property CreditCardAmount() As String
            Get
                Return strCreditCardAmount
            End Get
            Set(ByVal value As String)
                strCreditCardAmount = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class TCTaxes
        Private strTotal As String = Nothing
        Private objTCTax As List(Of TCTax) = Nothing
        Property Total() As String
            Get
                Return strTotal
            End Get
            Set(ByVal value As String)
                strTotal = value
            End Set
        End Property
        Property TCTax() As List(Of TCTax)
            Get
                Return objTCTax
            End Get
            Set(ByVal value As List(Of TCTax))
                objTCTax = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class TCTax
        Private strAmount As String = Nothing
        Private strTaxCode As String = Nothing
        Private strCodePaid As String = Nothing
        Property Amount() As String
            Get
                Return strAmount
            End Get
            Set(ByVal value As String)
                strAmount = value
            End Set
        End Property
        Property TaxCode() As String
            Get
                Return strTaxCode
            End Get
            Set(ByVal value As String)
                strTaxCode = value
            End Set
        End Property
        Property CodePaid() As String
            Get
                Return strCodePaid
            End Get
            Set(ByVal value As String)
                strCodePaid = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class TCTotalFare
        Private strAmount As String = Nothing
        Private strCurrencyCode As String = Nothing
        Property Amount() As String
            Get
                Return strAmount
            End Get
            Set(ByVal value As String)
                strAmount = value
            End Set
        End Property
        Property CurrencyCode() As String
            Get
                Return strCurrencyCode
            End Get
            Set(ByVal value As String)
                strCurrencyCode = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class TCExchangeData
        Private strIssueDate As String = Nothing
        Private strOriginalTicketNumber As String = Nothing
        Private strIATA_Code As String = Nothing
        Private strCouponNumbers As String = Nothing
        Private strBoardPoint As String = Nothing
        Private objTCPayment As TCPayment = Nothing
        Property IssueDate() As String
            Get
                Return strIssueDate
            End Get
            Set(ByVal value As String)
                strIssueDate = value
            End Set
        End Property
        Property OriginalTicketNumber() As String
            Get
                Return strOriginalTicketNumber
            End Get
            Set(ByVal value As String)
                strOriginalTicketNumber = value
            End Set
        End Property
        Property IATA_Code() As String
            Get
                Return strIATA_Code
            End Get
            Set(ByVal value As String)
                strIATA_Code = value
            End Set
        End Property
        Property CouponNumbers() As String
            Get
                Return strCouponNumbers
            End Get
            Set(ByVal value As String)
                strCouponNumbers = value
            End Set
        End Property
        Property BoardPoint() As String
            Get
                Return strBoardPoint
            End Get
            Set(ByVal value As String)
                strBoardPoint = value
            End Set
        End Property
        Property TCPayment() As TCPayment
            Get
                Return objTCPayment
            End Get
            Set(ByVal value As TCPayment)
                objTCPayment = value
            End Set
        End Property
    End Class

End Namespace