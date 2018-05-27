Imports GDS_NM_WebServicesSabre
Imports GDS_NuevoMundoPersistencia
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports GDS_MuevoMundoLog
Imports System.Linq
<Serializable()> _
Public Class IWebServices

    Private objEscribeLog As New GDS_MuevoMundoLog.EscribeLog
    Private oSabreWebService As New SabreWebService
    Private oSWS_Login As New WS_Login_SOAPEnvelope.Login
    Private oSWS_LoginAgcorp As New WS_Login_SOAPEnvelope.LoginAgcorp
    Private oSWS_LoginResert As New WS_Login_SOAPEnvelope.LoginResert
    Private dHoy As Date = Now
    Private strLog As String = Nothing

#Region "Servicios"
    Public Function _SessionCreate(ByVal strCodigoSeguimiento As String, _
                                   ByVal intGDS As Integer, _
                                   ByVal intFirmaGDS As Integer, _
                                   ByVal intFirmaDB As Integer) As GDS_NuevoMundoPersistencia.classSession

        Const ID_SABRE_WEB_SERVICE As String = "01"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity As SessionCreate.Security = Nothing
        Dim oPos As SessionCreate.SessionCreateRQPOS = Nothing
        Dim oSource As SessionCreate.SessionCreateRQPOSSource = Nothing
        Dim oUsernameToken As SessionCreate.SecurityUsernameToken = Nothing

        Dim oMessageHeader As SessionCreate.MessageHeader = Nothing
        Dim oFromPartyId As SessionCreate.PartyId = Nothing
        Dim oFromPartyIdArr(0) As SessionCreate.PartyId
        Dim oFrom As SessionCreate.From = Nothing


        Dim oToPartyId As SessionCreate.PartyId = Nothing
        Dim oToPartyIdArr(0) As SessionCreate.PartyId
        Dim oTo As SessionCreate.[To] = Nothing
        Dim oMessageData As SessionCreate.MessageData = Nothing
        Dim oService As SessionCreate.Service = Nothing
        '
        Dim oSessionCreateRQ As SessionCreate.SessionCreateRQ = Nothing
        Dim oSessionCreateService As SessionCreate.SessionCreateRQService = Nothing
        Dim oSessionCreateRS As SessionCreate.SessionCreateRS = Nothing

        Dim oRQInicioSesionSabre As GDS_NuevoMundoPersistencia.classSession = Nothing

        Dim oConversationID As String = strCodigoSeguimiento & "@nuevomundoviajes.com"
        Dim oPCC As String = Nothing
        Dim oToken As String = Nothing

        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try
            oMessageHeader = New SessionCreate.MessageHeader
            oFromPartyId = New SessionCreate.PartyId
            oFrom = New SessionCreate.From
            oToPartyId = New SessionCreate.PartyId
            oTo = New SessionCreate.[To]
            oService = New SessionCreate.Service
            oMessageData = New SessionCreate.MessageData
            oSessionCreateService = New SessionCreate.SessionCreateRQService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         oConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oSessionCreateService)

            oUsernameToken = New SessionCreate.SecurityUsernameToken
            oSecurity = New SessionCreate.Security
            oSource = New SessionCreate.SessionCreateRQPOSSource
            oPos = New SessionCreate.SessionCreateRQPOS
            oSessionCreateRQ = New SessionCreate.SessionCreateRQ

            obj_Security(oPCC, _
                         oToken, _
                         oUsernameToken, _
                         oSecurity, _
                         oSource, _
                         oPos, _
                         oSessionCreateRQ, _
                         False, _
                         oSessionCreateService, _
                         intFirmaGDS)


            oSessionCreateRQ.returnContextID = True
            oSessionCreateRQ.returnContextIDSpecified = True

            oSessionCreateRS = New SessionCreate.SessionCreateRS
            oSessionCreateRS = oSessionCreateService.SessionCreateRQ(oSessionCreateRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(SessionCreate.SessionCreateRQ), _
                                        GetType(SessionCreate.SessionCreateRS), _
                                        oSessionCreateRQ, _
                                        oSessionCreateRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)


            oRQInicioSesionSabre = New GDS_NuevoMundoPersistencia.classSession
            oRQInicioSesionSabre.ConversationID = oSessionCreateRS.ConversationId
            oRQInicioSesionSabre.Token = oSessionCreateService.SecurityValue.BinarySecurityToken


        Catch ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_SessionCreate" & vbCrLf
            If ex.Message IsNot Nothing Then
                strLog &= Constantes.TabEspacios & "Message : " & ex.Message & vbCrLf
            End If
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(ex.ToString)
        Finally

            FUNCTION_NAME = Nothing
            oSecurity = Nothing
            oPos = Nothing
            oSource = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFromPartyIdArr = Nothing
            oFrom = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oSessionCreateRQ = Nothing
            oSessionCreateService = Nothing
            oSessionCreateRS = Nothing
            oConversationID = Nothing
            oPCC = Nothing
            oToken = Nothing
            oGeneraPayLoadXML = Nothing

        End Try

        Return oRQInicioSesionSabre

    End Function
    Public Function _SessionClose(ByVal strCodigoSeguimiento As String, _
                                   ByVal intGDS As Integer, _
                                   ByVal intFirmaGDS As Integer, _
                                   ByVal intFirmaDB As Integer, _
                                   ByVal objSession As classSession) As GDS_NM_WebServicesSabre.SessionClose.SessionCloseRS

        Const ID_SABRE_WEB_SERVICE As String = "02"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity As SessionClose.Security = Nothing
        Dim oPos As SessionClose.SessionCloseRQPOS = Nothing
        Dim oSource As SessionClose.SessionCloseRQPOSSource = Nothing
        Dim oUsernameToken As SessionClose.SecurityUsernameToken = Nothing

        Dim oMessageHeader As SessionClose.MessageHeader = Nothing
        Dim oFromPartyId As SessionClose.PartyId = Nothing
        Dim oFromPartyIdArr(0) As SessionClose.PartyId
        Dim oFrom As SessionClose.From = Nothing


        Dim oToPartyId As SessionClose.PartyId = Nothing
        Dim oToPartyIdArr(0) As SessionClose.PartyId
        Dim oTo As SessionClose.[To] = Nothing
        Dim oMessageData As SessionClose.MessageData = Nothing
        Dim oService As SessionClose.Service = Nothing
        '
        Dim oSessionCloseRQ As SessionClose.SessionCloseRQ = Nothing
        Dim oSessionCloseService As SessionClose.SessionCloseRQService = Nothing
        Dim oSessionCloseRS As SessionClose.SessionCloseRS = Nothing

        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try
            oMessageHeader = New SessionClose.MessageHeader
            oFromPartyId = New SessionClose.PartyId
            oFrom = New SessionClose.From
            oToPartyId = New SessionClose.PartyId
            oTo = New SessionClose.[To]
            oService = New SessionClose.Service
            oMessageData = New SessionClose.MessageData
            oSessionCloseService = New SessionClose.SessionCloseRQService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oSessionCloseService)

            oUsernameToken = New SessionClose.SecurityUsernameToken
            oSecurity = New SessionClose.Security
            oSource = New SessionClose.SessionCloseRQPOSSource
            oPos = New SessionClose.SessionCloseRQPOS
            oSessionCloseRQ = New SessionClose.SessionCloseRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity, _
                         oSource, _
                         oPos, _
                         oSessionCloseRQ, _
                         True, _
                         oSessionCloseService, _
                         intFirmaGDS)


            oSessionCloseRS = New SessionClose.SessionCloseRS
            oSessionCloseRS = oSessionCloseService.SessionCloseRQ(oSessionCloseRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(SessionClose.SessionCloseRQ), _
                                        GetType(SessionClose.SessionCloseRS), _
                                        oSessionCloseRQ, _
                                        oSessionCloseRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_SessionClose" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(ex.ToString)
        Finally

            FUNCTION_NAME = Nothing
            oSecurity = Nothing
            oPos = Nothing
            oSource = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFromPartyIdArr = Nothing
            oFrom = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oSessionCloseRQ = Nothing
            oSessionCloseService = Nothing
            oGeneraPayLoadXML = Nothing

        End Try

        Return oSessionCloseRS

    End Function
    Public Function _SabreCommand(ByVal strComando As String, _
                                  ByVal strNombre As String, _
                                  ByVal strCodigoSeguimiento As String, _
                                  ByVal intGDS As Integer, _
                                  ByVal intFirmaGDS As Integer, _
                                  ByVal intFirmaDB As Integer, _
                                  ByVal objSession As classSession) As SabreCommand.SabreCommandLLSRS

        Const ID_SABRE_WEB_SERVICE As String = "10"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        If strNombre IsNot Nothing Then FUNCTION_NAME = strNombre
        '
        Dim oSecurity As SabreCommand.Security = Nothing
        Dim oUsernameToken As SabreCommand.SecurityUsernameToken = Nothing
        Dim oMessageHeader As SabreCommand.MessageHeader = Nothing
        Dim oFromPartyId As SabreCommand.PartyId = Nothing
        Dim oFrom As SabreCommand.From = Nothing
        Dim oFromPartyIdArr(0) As SabreCommand.PartyId
        Dim oToPartyId As New SabreCommand.PartyId
        Dim oToPartyIdArr(0) As SabreCommand.PartyId
        Dim oTo As SabreCommand.[To] = Nothing
        Dim oMessageData As SabreCommand.MessageData = Nothing
        Dim oService As SabreCommand.Service = Nothing
        '
        Dim oSabreCommandRQ As SabreCommand.SabreCommandLLSRQ = Nothing
        Dim oSabreCommandLLSService As SabreCommand.SabreCommandLLSService = Nothing
        Dim oSabreCommandRS As SabreCommand.SabreCommandLLSRS = Nothing

        Dim oRequest As New SabreCommand.SabreCommandLLSRQRequest
        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try

            oMessageHeader = New SabreCommand.MessageHeader
            oFromPartyId = New SabreCommand.PartyId
            oFrom = New SabreCommand.From
            oToPartyId = New SabreCommand.PartyId
            oTo = New SabreCommand.[To]
            oService = New SabreCommand.Service
            oMessageData = New SabreCommand.MessageData
            oSabreCommandLLSService = New SabreCommand.SabreCommandLLSService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oSabreCommandLLSService)


            oUsernameToken = New SabreCommand.SecurityUsernameToken
            oSecurity = New SabreCommand.Security
            oSabreCommandRQ = New SabreCommand.SabreCommandLLSRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity, _
                         Nothing, _
                         Nothing, _
                         oSabreCommandRQ, _
                         True, _
                         oSabreCommandLLSService, _
                         intFirmaGDS)

            '======================================
            '
            oRequest.Output = GDS_NM_WebServicesSabre.SabreCommand.SabreCommandLLSRQRequestOutput.SDS
            oRequest.CDATA = True
            oRequest.HostCommand = strComando.ToString
            oSabreCommandRQ.Request = oRequest

            '
            oSabreCommandRS = oSabreCommandLLSService.SabreCommandLLSRQ(oSabreCommandRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(SabreCommand.SabreCommandLLSRQ), _
                                        GetType(SabreCommand.SabreCommandLLSRS), _
                                        oSabreCommandRQ, _
                                        oSabreCommandRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch Ex As Exception

            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_SabreCommand" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            strComando = Nothing
            strNombre = Nothing
            strCodigoSeguimiento = Nothing
            intGDS = Nothing
            intFirmaGDS = Nothing
            intFirmaDB = Nothing
            objSession = Nothing
            oSecurity = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oSabreCommandLLSService = Nothing
            oSabreCommandRQ = Nothing
        End Try

        Return oSabreCommandRS

    End Function
    Public Function _IgnoreTransaction(ByVal strCodigoSeguimiento As String, _
                                       ByVal intGDS As Integer, _
                                       ByVal intFirmaGDS As Integer, _
                                       ByVal intFirmaDB As Integer, _
                                       ByVal objSession As classSession) As IgnoreTransaction.IgnoreTransactionRS

        Const ID_SABRE_WEB_SERVICE As String = "37"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        '
        Dim oSecurity1 As IgnoreTransaction.Security1 = Nothing
        'Dim oPos As New OTA_AirPrice.OTA_AirPriceRQPOS
        'Dim oSource As New OTA_AirPrice.OTA_AirPriceRQPOSSource
        Dim oUsernameToken As IgnoreTransaction.SecurityUsernameToken = Nothing
        Dim oMessageHeader As IgnoreTransaction.MessageHeader = Nothing
        Dim oFromPartyId As IgnoreTransaction.PartyId = Nothing
        Dim oFrom As IgnoreTransaction.From = Nothing
        Dim oFromPartyIdArr(0) As IgnoreTransaction.PartyId
        Dim oToPartyId As IgnoreTransaction.PartyId = Nothing
        Dim oToPartyIdArr(0) As IgnoreTransaction.PartyId
        Dim oTo As New IgnoreTransaction.[To]
        Dim oMessageData As IgnoreTransaction.MessageData = Nothing
        Dim oService As IgnoreTransaction.Service = Nothing

        '
        Dim oIgnoreTransactionRQ As IgnoreTransaction.IgnoreTransactionRQ = Nothing
        Dim oIgnoreTransactionService As IgnoreTransaction.IgnoreTransactionService = Nothing
        Dim oIgnoreTransactionRS As IgnoreTransaction.IgnoreTransactionRS = Nothing



        '
        Dim oProfile As DesignatePrinter.DesignatePrinterRQProfile = Nothing


        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing
        Dim strProfileNumber As String = Nothing

        Try
            oMessageHeader = New IgnoreTransaction.MessageHeader
            oFromPartyId = New IgnoreTransaction.PartyId
            oFrom = New IgnoreTransaction.From
            oToPartyId = New IgnoreTransaction.PartyId
            oTo = New IgnoreTransaction.[To]
            oService = New IgnoreTransaction.Service
            oMessageData = New IgnoreTransaction.MessageData
            oIgnoreTransactionService = New IgnoreTransaction.IgnoreTransactionService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oIgnoreTransactionService)

            oUsernameToken = New IgnoreTransaction.SecurityUsernameToken
            oSecurity1 = New IgnoreTransaction.Security1
            'oSource = New OTA_AirPrice.SessionCloseRQPOSSource
            'oPos = New OTA_AirPrice.SessionCloseRQPOS
            oIgnoreTransactionRQ = New IgnoreTransaction.IgnoreTransactionRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oIgnoreTransactionRQ, _
                         True, _
                         oIgnoreTransactionService, _
                         intFirmaGDS)

            '===========================================================================

            oIgnoreTransactionRQ.ReturnHostCommand = True
            oIgnoreTransactionRQ.ReturnHostCommandSpecified = True

            oIgnoreTransactionRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oIgnoreTransactionRQ.TimeStampSpecified = True

            oIgnoreTransactionRS = New IgnoreTransaction.IgnoreTransactionRS
            oIgnoreTransactionRS = oIgnoreTransactionService.IgnoreTransactionRQ(oIgnoreTransactionRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(IgnoreTransaction.IgnoreTransactionRQ), _
                                        GetType(IgnoreTransaction.IgnoreTransactionRS), _
                                        oIgnoreTransactionRQ, _
                                        oIgnoreTransactionRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)


        Catch Ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_IgnoreTransaction" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            FUNCTION_NAME = Nothing
            '
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            '
            oIgnoreTransactionRQ = Nothing
            oIgnoreTransactionService = Nothing
            '
            oProfile = Nothing
            oGeneraPayLoadXML = Nothing
        End Try

        Return oIgnoreTransactionRS

    End Function
    Public Function _EndTransaction(ByVal strReceivedFrom As String, _
                                    ByVal strEnd As String, _
                                    ByVal strCodigoSeguimiento As String, _
                                    ByVal intGDS As Integer, _
                                    ByVal intFirmaGDS As Integer, _
                                    ByVal intFirmaDB As Integer, _
                                    ByVal objSession As classSession) As EndTransaction.EndTransactionRS

        Const ID_SABRE_WEB_SERVICE As String = "09"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        '
        Dim oSecurity1 As EndTransaction.Security1 = Nothing
        'Dim oPos As New OTA_AirPrice.OTA_AirPriceRQPOS
        'Dim oSource As New OTA_AirPrice.OTA_AirPriceRQPOSSource
        Dim oUsernameToken As EndTransaction.SecurityUsernameToken = Nothing
        Dim oMessageHeader As EndTransaction.MessageHeader = Nothing
        Dim oFromPartyId As EndTransaction.PartyId = Nothing
        Dim oFrom As EndTransaction.From = Nothing
        Dim oFromPartyIdArr(0) As EndTransaction.PartyId
        Dim oToPartyId As EndTransaction.PartyId = Nothing
        Dim oToPartyIdArr(0) As EndTransaction.PartyId
        Dim oTo As New EndTransaction.[To]
        Dim oMessageData As EndTransaction.MessageData = Nothing
        Dim oService As EndTransaction.Service = Nothing

        '
        Dim oEndTransactionRQ As EndTransaction.EndTransactionRQ = Nothing
        Dim oEndTransactionService As EndTransaction.EndTransactionService = Nothing
        Dim oEndTransactionRS As EndTransaction.EndTransactionRS = Nothing
        '
        Dim EndTransaction As EndTransaction.EndTransactionRQEndTransaction = Nothing
        Dim Source As EndTransaction.EndTransactionRQSource = Nothing

        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing


        Try
            oMessageHeader = New EndTransaction.MessageHeader
            oFromPartyId = New EndTransaction.PartyId
            oFrom = New EndTransaction.From
            oToPartyId = New EndTransaction.PartyId
            oTo = New EndTransaction.[To]
            oService = New EndTransaction.Service
            oMessageData = New EndTransaction.MessageData
            oEndTransactionService = New EndTransaction.EndTransactionService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oEndTransactionService)

            oUsernameToken = New EndTransaction.SecurityUsernameToken
            oSecurity1 = New EndTransaction.Security1
            'oSource = New OTA_AirPrice.SessionCloseRQPOSSource
            'oPos = New OTA_AirPrice.SessionCloseRQPOS
            oEndTransactionRQ = New EndTransaction.EndTransactionRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oEndTransactionRQ, _
                         True, _
                         oEndTransactionService, _
                         intFirmaGDS)

            '===========================================================================
            EndTransaction = New EndTransaction.EndTransactionRQEndTransaction
            If Not String.IsNullOrEmpty(strEnd) Then
                EndTransaction.Ind = True
            Else
                EndTransaction.Ind = False
            End If

            oEndTransactionRQ.EndTransaction = New EndTransaction.EndTransactionRQEndTransaction
            oEndTransactionRQ.EndTransaction = EndTransaction

            oEndTransactionRQ.ReturnHostCommand = True
            oEndTransactionRQ.ReturnHostCommandSpecified = True

            oEndTransactionRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oEndTransactionRQ.TimeStampSpecified = True

            If Not String.IsNullOrEmpty(strReceivedFrom) Then
                Source = New EndTransaction.EndTransactionRQSource
                Source.ReceivedFrom = strReceivedFrom.ToString
                oEndTransactionRQ.Source = New EndTransaction.EndTransactionRQSource
                oEndTransactionRQ.Source = Source
            End If

            oEndTransactionRS = New EndTransaction.EndTransactionRS
            oEndTransactionRS = oEndTransactionService.EndTransactionRQ(oEndTransactionRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(EndTransaction.EndTransactionRQ), _
                                        GetType(EndTransaction.EndTransactionRS), _
                                        oEndTransactionRQ, _
                                        oEndTransactionRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)


        Catch Ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_EndTransaction" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            FUNCTION_NAME = Nothing
            strCodigoSeguimiento = Nothing
            objSession = Nothing
            '
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            '
            oEndTransactionRQ = Nothing
            oEndTransactionService = Nothing
            '
            oGeneraPayLoadXML = Nothing
        End Try

        Return oEndTransactionRS

    End Function
    Public Function _ContextChange(ByVal strPseudo As String, _
                                   ByVal strCodigoSeguimiento As String, _
                                   ByVal intGDS As Integer, _
                                   ByVal intFirmaGDS As Integer, _
                                   ByVal intFirmaDB As Integer, _
                                   ByVal objSession As classSession) As GDS_NM_WebServicesSabre.ContextChange.ContextChangeRS

        Const ID_SABRE_WEB_SERVICE As String = "35"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity1 As ContextChange.Security1 = Nothing

        Dim oUsernameToken As ContextChange.SecurityUsernameToken = Nothing
        Dim oMessageHeader As ContextChange.MessageHeader = Nothing

        Dim oFromPartyId As ContextChange.PartyId = Nothing
        Dim oFrom As ContextChange.From = Nothing
        Dim oFromPartyIdArr(0) As ContextChange.PartyId
        Dim oToPartyId As ContextChange.PartyId = Nothing
        Dim oToPartyIdArr(0) As ContextChange.PartyId
        Dim oTo As ContextChange.[To] = Nothing
        Dim oMessageData As ContextChange.MessageData = Nothing
        Dim oService As ContextChange.Service = Nothing

        Dim oChangeAAA As ContextChange.ContextChangeRQChangeAAA = Nothing

        Dim oContextChangeRQ As ContextChange.ContextChangeRQ = Nothing
        Dim oContextChangeService As ContextChange.ContextChangeService = Nothing
        Dim oContextChangeRS As ContextChange.ContextChangeRS = Nothing

        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing
        Dim oFlagSeCargoRS As Boolean = False

        Try


            oMessageHeader = New ContextChange.MessageHeader
            oFromPartyId = New ContextChange.PartyId
            oFrom = New ContextChange.From
            oToPartyId = New ContextChange.PartyId
            oTo = New ContextChange.[To]
            oService = New ContextChange.Service
            oMessageData = New ContextChange.MessageData
            oContextChangeService = New ContextChange.ContextChangeService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oContextChangeService)


            oUsernameToken = New ContextChange.SecurityUsernameToken
            oSecurity1 = New ContextChange.Security1
            oContextChangeRQ = New ContextChange.ContextChangeRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oContextChangeRQ, _
                         True, _
                         oContextChangeService, _
                         intFirmaGDS)

            '======================================

            oChangeAAA = New ContextChange.ContextChangeRQChangeAAA
            oChangeAAA.PseudoCityCode = strPseudo
            oContextChangeRQ.ChangeAAA = oChangeAAA
            oContextChangeRQ.ReturnHostCommand = True
            oContextChangeRQ.ReturnHostCommandSpecified = True

            oContextChangeRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oContextChangeRQ.TimeStampSpecified = True

            oContextChangeRS = New ContextChange.ContextChangeRS
            oContextChangeRS = oContextChangeService.ContextChangeRQ(oContextChangeRQ)


            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(ContextChange.ContextChangeRQ), _
                                        GetType(ContextChange.ContextChangeRS), _
                                        oContextChangeRQ, _
                                        oContextChangeRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_ContextChange" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)

            Throw New Exception(ex.ToString)

        Finally

            strCodigoSeguimiento = Nothing
            intGDS = Nothing
            objSession = Nothing
            strPseudo = Nothing

            FUNCTION_NAME = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oChangeAAA = Nothing
            oContextChangeRQ = Nothing
            oContextChangeService = Nothing
            oGeneraPayLoadXML = Nothing

        End Try

        Return oContextChangeRS

    End Function
    Public Function _DailySalesReport(ByVal strFecha As String, _
                                      ByVal strPseudo As String, _
                                      ByVal strCodigoSeguimiento As String, _
                                      ByVal intGDS As Integer, _
                                      ByVal intFirmaGDS As Integer, _
                                      ByVal intFirmaDB As Integer, _
                                      ByVal objSession As classSession) As DailySalesReport.DailySalesReportRS


        Const ID_SABRE_WEB_SERVICE As String = "25"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity1 As DailySalesReport.Security1 = Nothing

        Dim oUsernameToken As DailySalesReport.SecurityUsernameToken = Nothing
        Dim oMessageHeader As DailySalesReport.MessageHeader = Nothing

        Dim oFromPartyId As DailySalesReport.PartyId = Nothing
        Dim oFrom As DailySalesReport.From = Nothing
        Dim oFromPartyIdArr(0) As DailySalesReport.PartyId
        Dim oToPartyId As DailySalesReport.PartyId = Nothing
        Dim oToPartyIdArr(0) As DailySalesReport.PartyId
        Dim oTo As DailySalesReport.[To] = Nothing
        Dim oMessageData As DailySalesReport.MessageData = Nothing
        Dim oService As DailySalesReport.Service = Nothing

        Dim oDailySalesReportRQ As DailySalesReport.DailySalesReportRQ = Nothing
        Dim oDailySalesReportService As DailySalesReport.DailySalesReportService = Nothing
        Dim oDailySalesReportRS As DailySalesReport.DailySalesReportRS = Nothing


        '========================================================================
        Dim oSalesReport As DailySalesReport.DailySalesReportRQSalesReport = Nothing

        '========================================================================

        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try


            oMessageHeader = New DailySalesReport.MessageHeader
            oFromPartyId = New DailySalesReport.PartyId
            oFrom = New DailySalesReport.From
            oToPartyId = New DailySalesReport.PartyId
            oTo = New DailySalesReport.[To]
            oService = New DailySalesReport.Service
            oMessageData = New DailySalesReport.MessageData
            oDailySalesReportService = New DailySalesReport.DailySalesReportService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID.ToString, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oDailySalesReportService)


            oUsernameToken = New DailySalesReport.SecurityUsernameToken
            oSecurity1 = New DailySalesReport.Security1
            oDailySalesReportRQ = New DailySalesReport.DailySalesReportRQ

            obj_Security(Nothing, _
                         objSession.Token.ToString, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oDailySalesReportRQ, _
                         True, _
                         oDailySalesReportService, _
                         intFirmaGDS)

            '======================================

            oSalesReport = New DailySalesReport.DailySalesReportRQSalesReport

            If String.IsNullOrEmpty(strFecha) Then strFecha = Now
            Dim fecha As Date = strFecha
            oSalesReport.StartDate = fecha.ToString("yyyy-MM-dd")
            oSalesReport.PseudoCityCode = strPseudo.ToString


            oDailySalesReportRQ.SalesReport = oSalesReport

            oDailySalesReportRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oDailySalesReportRQ.TimeStampSpecified = True

            oDailySalesReportRQ.ReturnHostCommand = True
            oDailySalesReportRQ.ReturnHostCommandSpecified = True

            '======================================

            oDailySalesReportRS = New DailySalesReport.DailySalesReportRS
            oDailySalesReportRS = oDailySalesReportService.DailySalesReportRQ(oDailySalesReportRQ)


            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(DailySalesReport.DailySalesReportRQ), _
                                        GetType(DailySalesReport.DailySalesReportRS), _
                                        oDailySalesReportRQ, _
                                        oDailySalesReportRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_DailySalesReport" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Token : " & objSession.Token.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "ConversationID : " & objSession.ConversationID.ToString & vbCrLf

            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(ex.ToString)

            'If Err.Description.IndexOf("Invalid or Expired binary security") > -1 Then
            '    Err.Raise(-9999, "ContextChangeRS", "Invalid or Expired binary security")
            'Else
            '    Throw New Exception(ex.ToString)
            'End If

        Finally

            FUNCTION_NAME = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oDailySalesReportRQ = Nothing
            oDailySalesReportService = Nothing
            oGeneraPayLoadXML = Nothing

        End Try

        Return oDailySalesReportRS

    End Function

    Public Function _DailyEMD_Report(ByVal strFecha As String, _
                                     ByVal strPseudo As String, _
                                     ByVal strCodigoSeguimiento As String, _
                                     ByVal intGDS As Integer, _
                                     ByVal intFirmaGDS As Integer, _
                                     ByVal intFirmaDB As Integer, _
                                     ByVal objSession As classSession) As DailyEMD_Report.DailyEMD_ReportRS


        Const ID_SABRE_WEB_SERVICE As String = "47"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity1 As DailyEMD_Report.Security1 = Nothing

        Dim oUsernameToken As DailyEMD_Report.SecurityUsernameToken = Nothing
        Dim oMessageHeader As DailyEMD_Report.MessageHeader = Nothing

        Dim oFromPartyId As DailyEMD_Report.PartyId = Nothing
        Dim oFrom As DailyEMD_Report.From = Nothing
        Dim oFromPartyIdArr(0) As DailyEMD_Report.PartyId
        Dim oToPartyId As DailyEMD_Report.PartyId = Nothing
        Dim oToPartyIdArr(0) As DailyEMD_Report.PartyId
        Dim oTo As DailyEMD_Report.[To] = Nothing
        Dim oMessageData As DailyEMD_Report.MessageData = Nothing
        Dim oService As DailyEMD_Report.Service = Nothing

        Dim oDailyEMD_ReportRQ As DailyEMD_Report.DailyEMD_ReportRQ = Nothing
        Dim oDailyEMD_ReportService As DailyEMD_Report.DailyEMD_ReportService = Nothing
        Dim oDailyEMD_ReportRS As DailyEMD_Report.DailyEMD_ReportRS = Nothing


        '========================================================================
        Dim oSalesReport As DailyEMD_Report.DailyEMD_ReportRQEMD_Report = Nothing

        '========================================================================

        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try


            oMessageHeader = New DailyEMD_Report.MessageHeader
            oFromPartyId = New DailyEMD_Report.PartyId
            oFrom = New DailyEMD_Report.From
            oToPartyId = New DailyEMD_Report.PartyId
            oTo = New DailyEMD_Report.[To]
            oService = New DailyEMD_Report.Service
            oMessageData = New DailyEMD_Report.MessageData
            oDailyEMD_ReportService = New DailyEMD_Report.DailyEMD_ReportService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID.ToString, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oDailyEMD_ReportService)


            oUsernameToken = New DailyEMD_Report.SecurityUsernameToken
            oSecurity1 = New DailyEMD_Report.Security1
            oDailyEMD_ReportRQ = New DailyEMD_Report.DailyEMD_ReportRQ

            obj_Security(Nothing, _
                         objSession.Token.ToString, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oDailyEMD_ReportRQ, _
                         True, _
                         oDailyEMD_ReportService, _
                         intFirmaGDS)

            '======================================

            oSalesReport = New DailyEMD_Report.DailyEMD_ReportRQEMD_Report

            If String.IsNullOrEmpty(strFecha) Then strFecha = Now
            Dim fecha As Date = strFecha
            oSalesReport.StartDate = fecha.ToString("MM-dd")

            oDailyEMD_ReportRQ.EMD_Report = oSalesReport

            oDailyEMD_ReportRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oDailyEMD_ReportRQ.TimeStampSpecified = True

            oDailyEMD_ReportRQ.ReturnHostCommand = True
            oDailyEMD_ReportRQ.ReturnHostCommandSpecified = True

            '======================================

            oDailyEMD_ReportRS = New DailyEMD_Report.DailyEMD_ReportRS
            oDailyEMD_ReportRS = oDailyEMD_ReportService.DailyEMD_ReportRQ(oDailyEMD_ReportRQ)


            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(DailyEMD_Report.DailyEMD_ReportRQ), _
                                        GetType(DailyEMD_Report.DailyEMD_ReportRS), _
                                        oDailyEMD_ReportRQ, _
                                        oDailyEMD_ReportRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_DailyEMD_Report" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Token : " & objSession.Token.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "ConversationID : " & objSession.ConversationID.ToString & vbCrLf

            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(ex.ToString)

        Finally

            FUNCTION_NAME = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oDailyEMD_ReportRQ = Nothing
            oDailyEMD_ReportService = Nothing
            oGeneraPayLoadXML = Nothing

        End Try

        Return oDailyEMD_ReportRS

    End Function
    Public Function _TravelItineraryReadEMD(ByVal strPNR As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intGDS As Integer, _
                                             ByVal intFirmaGDS As Integer, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal objSession As classSession) As TravelItineraryReadRQ360.TravelItineraryReadRS

        Const ID_SABRE_WEB_SERVICE As String = "51"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity1 As TravelItineraryReadRQ360.Security1 = Nothing
        Dim oUsernameToken As TravelItineraryReadRQ360.SecurityUsernameToken = Nothing
        Dim oMessageHeader As TravelItineraryReadRQ360.MessageHeader = Nothing
        Dim oFromPartyId As TravelItineraryReadRQ360.PartyId = Nothing
        Dim oFrom As TravelItineraryReadRQ360.From = Nothing
        Dim oFromPartyIdArr(0) As TravelItineraryReadRQ360.PartyId
        Dim oToPartyId As TravelItineraryReadRQ360.PartyId = Nothing
        Dim oToPartyIdArr(0) As TravelItineraryReadRQ360.PartyId
        Dim oTo As TravelItineraryReadRQ360.[To] = Nothing
        Dim oMessageData As TravelItineraryReadRQ360.MessageData = Nothing
        Dim oService As TravelItineraryReadRQ360.Service = Nothing

        Dim oTravelItineraryReadRQ As TravelItineraryReadRQ360.TravelItineraryReadRQ = Nothing
        Dim oTravelItineraryReadService As TravelItineraryReadRQ360.TravelItineraryReadService = Nothing
        Dim oTravelItineraryReadRS As TravelItineraryReadRQ360.TravelItineraryReadRS = Nothing


        Dim oUniqueID As TravelItineraryReadRQ360.TravelItineraryReadRQUniqueID = Nothing



        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try
            oMessageHeader = New TravelItineraryReadRQ360.MessageHeader
            oFromPartyId = New TravelItineraryReadRQ360.PartyId
            oFrom = New TravelItineraryReadRQ360.From
            oToPartyId = New TravelItineraryReadRQ360.PartyId
            oTo = New TravelItineraryReadRQ360.[To]
            oService = New TravelItineraryReadRQ360.Service
            oMessageData = New TravelItineraryReadRQ360.MessageData
            oTravelItineraryReadService = New TravelItineraryReadRQ360.TravelItineraryReadService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oTravelItineraryReadService)

            oUsernameToken = New TravelItineraryReadRQ360.SecurityUsernameToken
            oSecurity1 = New TravelItineraryReadRQ360.Security1
            'oSource = New OTA_AirPrice.SessionCloseRQPOSSource
            'oPos = New OTA_AirPrice.SessionCloseRQPOS
            oTravelItineraryReadRQ = New TravelItineraryReadRQ360.TravelItineraryReadRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oTravelItineraryReadRQ, _
                         True, _
                         oTravelItineraryReadService, _
                         intFirmaGDS)


            oTravelItineraryReadRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oTravelItineraryReadRQ.TimeStampSpecified = True


            oUniqueID = New TravelItineraryReadRQ360.TravelItineraryReadRQUniqueID
            oUniqueID.ID = strPNR.ToString
            oTravelItineraryReadRQ.UniqueID = New TravelItineraryReadRQ360.TravelItineraryReadRQUniqueID
            oTravelItineraryReadRQ.UniqueID = oUniqueID

            oTravelItineraryReadRS = New TravelItineraryReadRQ360.TravelItineraryReadRS
            oTravelItineraryReadRS = oTravelItineraryReadService.TravelItineraryReadRQ(oTravelItineraryReadRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(TravelItineraryReadRQ360.TravelItineraryReadRQ), _
                                        GetType(TravelItineraryReadRQ360.TravelItineraryReadRS), _
                                        oTravelItineraryReadRQ, _
                                        oTravelItineraryReadRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)


        Catch Ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_TravelItineraryRead" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oTravelItineraryReadRQ = Nothing
            oTravelItineraryReadService = Nothing
            oUniqueID = Nothing

        End Try

        Return oTravelItineraryReadRS

    End Function
    Public Function _TravelItineraryRead(ByVal strPNR As String, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intGDS As Integer, _
                                         ByVal intFirmaGDS As Integer, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal objSession As classSession) As TravelItineraryRead.TravelItineraryReadRS

        Const ID_SABRE_WEB_SERVICE As String = "27"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity1 As TravelItineraryRead.Security1 = Nothing
        Dim oUsernameToken As TravelItineraryRead.SecurityUsernameToken = Nothing
        Dim oMessageHeader As TravelItineraryRead.MessageHeader = Nothing
        Dim oFromPartyId As TravelItineraryRead.PartyId = Nothing
        Dim oFrom As TravelItineraryRead.From = Nothing
        Dim oFromPartyIdArr(0) As TravelItineraryRead.PartyId
        Dim oToPartyId As TravelItineraryRead.PartyId = Nothing
        Dim oToPartyIdArr(0) As TravelItineraryRead.PartyId
        Dim oTo As TravelItineraryRead.[To] = Nothing
        Dim oMessageData As TravelItineraryRead.MessageData = Nothing
        Dim oService As TravelItineraryRead.Service = Nothing

        Dim oTravelItineraryReadRQ As TravelItineraryRead.TravelItineraryReadRQ = Nothing
        Dim oTravelItineraryReadService As TravelItineraryRead.TravelItineraryReadService = Nothing
        Dim oTravelItineraryReadRS As TravelItineraryRead.TravelItineraryReadRS = Nothing

        Dim oMessagingDetails As TravelItineraryRead.TravelItineraryReadRQMessagingDetails = Nothing
        Dim oMessagingDetailsTransaction(0) As TravelItineraryRead.TravelItineraryReadRQMessagingDetailsTransaction
        Dim oCode As TravelItineraryRead.TravelItineraryReadRQMessagingDetailsTransactionCode = Nothing
        Dim oUniqueID As TravelItineraryRead.TravelItineraryReadRQUniqueID = Nothing

        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try
            oMessageHeader = New TravelItineraryRead.MessageHeader
            oFromPartyId = New TravelItineraryRead.PartyId
            oFrom = New TravelItineraryRead.From
            oToPartyId = New TravelItineraryRead.PartyId
            oTo = New TravelItineraryRead.[To]
            oService = New TravelItineraryRead.Service
            oMessageData = New TravelItineraryRead.MessageData
            oTravelItineraryReadService = New TravelItineraryRead.TravelItineraryReadService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oTravelItineraryReadService)

            oUsernameToken = New TravelItineraryRead.SecurityUsernameToken
            oSecurity1 = New TravelItineraryRead.Security1
            'oSource = New OTA_AirPrice.SessionCloseRQPOSSource
            'oPos = New OTA_AirPrice.SessionCloseRQPOS
            oTravelItineraryReadRQ = New TravelItineraryRead.TravelItineraryReadRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oTravelItineraryReadRQ, _
                         True, _
                         oTravelItineraryReadService, _
                         intFirmaGDS)


            oMessagingDetailsTransaction(0) = New TravelItineraryRead.TravelItineraryReadRQMessagingDetailsTransaction
            oCode = New TravelItineraryRead.TravelItineraryReadRQMessagingDetailsTransactionCode
            oCode = TravelItineraryRead.TravelItineraryReadRQMessagingDetailsTransactionCode.PNR
            oMessagingDetailsTransaction(0).Code = oCode

            oMessagingDetails = New TravelItineraryRead.TravelItineraryReadRQMessagingDetails
            oMessagingDetails.Transaction = oMessagingDetailsTransaction
            oTravelItineraryReadRQ.MessagingDetails = oMessagingDetails


            oUniqueID = New TravelItineraryRead.TravelItineraryReadRQUniqueID
            oUniqueID.ID = strPNR.ToString
            oTravelItineraryReadRQ.UniqueID = oUniqueID

            oTravelItineraryReadRQ.ReturnHostCommand = True
            oTravelItineraryReadRQ.ReturnHostCommandSpecified = True
            oTravelItineraryReadRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oTravelItineraryReadRQ.TimeStampSpecified = True

            oTravelItineraryReadRS = New TravelItineraryRead.TravelItineraryReadRS
            oTravelItineraryReadRS = oTravelItineraryReadService.TravelItineraryReadRQ(oTravelItineraryReadRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(TravelItineraryRead.TravelItineraryReadRQ), _
                                        GetType(TravelItineraryRead.TravelItineraryReadRS), _
                                        oTravelItineraryReadRQ, _
                                        oTravelItineraryReadRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)



        Catch Ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_TravelItineraryRead" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oTravelItineraryReadRQ = Nothing
            oTravelItineraryReadService = Nothing
            oMessagingDetails = Nothing
            oMessagingDetailsTransaction = Nothing
            oUniqueID = Nothing

        End Try

        Return oTravelItineraryReadRS

    End Function
    Public Function _TravelItineraryReadRQ(ByVal strPNR As String, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intGDS As Integer, _
                                           ByVal intFirmaGDS As Integer, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal objSession As classSession) As TravelItineraryReadRQ.TravelItineraryReadRS

        Const ID_SABRE_WEB_SERVICE As String = "43"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity1 As TravelItineraryReadRQ.Security1 = Nothing
        Dim oUsernameToken As TravelItineraryReadRQ.SecurityUsernameToken = Nothing
        Dim oMessageHeader As TravelItineraryReadRQ.MessageHeader = Nothing
        Dim oFromPartyId As TravelItineraryReadRQ.PartyId = Nothing
        Dim oFrom As TravelItineraryReadRQ.From = Nothing
        Dim oFromPartyIdArr(0) As TravelItineraryReadRQ.PartyId
        Dim oToPartyId As TravelItineraryReadRQ.PartyId = Nothing
        Dim oToPartyIdArr(0) As TravelItineraryReadRQ.PartyId
        Dim oTo As TravelItineraryReadRQ.[To] = Nothing
        Dim oMessageData As TravelItineraryReadRQ.MessageData = Nothing
        Dim oService As TravelItineraryReadRQ.Service = Nothing

        Dim oTravelItineraryReadRQ As TravelItineraryReadRQ.TravelItineraryReadRQ = Nothing
        Dim oTravelItineraryReadService As TravelItineraryReadRQ.TravelItineraryReadService = Nothing
        Dim oTravelItineraryReadRS As TravelItineraryReadRQ.TravelItineraryReadRS = Nothing

        Dim oMessagingDetails As TravelItineraryReadRQ.TravelItineraryReadRQMessagingDetails = Nothing
        Dim oUniqueID As TravelItineraryReadRQ.TravelItineraryReadRQUniqueID = Nothing

        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try
            oMessageHeader = New TravelItineraryReadRQ.MessageHeader
            oFromPartyId = New TravelItineraryReadRQ.PartyId
            oFrom = New TravelItineraryReadRQ.From
            oToPartyId = New TravelItineraryReadRQ.PartyId
            oTo = New TravelItineraryReadRQ.[To]
            oService = New TravelItineraryReadRQ.Service
            oMessageData = New TravelItineraryReadRQ.MessageData
            oTravelItineraryReadService = New TravelItineraryReadRQ.TravelItineraryReadService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oTravelItineraryReadService)

            oUsernameToken = New TravelItineraryReadRQ.SecurityUsernameToken
            oSecurity1 = New TravelItineraryReadRQ.Security1
            'oSource = New OTA_AirPrice.SessionCloseRQPOSSource
            'oPos = New OTA_AirPrice.SessionCloseRQPOS
            oTravelItineraryReadRQ = New TravelItineraryReadRQ.TravelItineraryReadRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oTravelItineraryReadRQ, _
                         True, _
                         oTravelItineraryReadService, _
                         intFirmaGDS)

            oMessagingDetails = New TravelItineraryReadRQ.TravelItineraryReadRQMessagingDetails
            Dim oSubjectAreas() As String
            ReDim oSubjectAreas(1)
            oSubjectAreas(0) = "FULL"
            'oSubjectAreas(1) = "POPULATE_IS_PAST"
            oMessagingDetails.SubjectAreas = oSubjectAreas


            oUniqueID = New TravelItineraryReadRQ.TravelItineraryReadRQUniqueID
            oUniqueID.ID = strPNR.ToString
            oTravelItineraryReadRQ.UniqueID = oUniqueID

            oTravelItineraryReadRQ.MessagingDetails = oMessagingDetails

            oTravelItineraryReadRQ.Version = "3.5.0"

            oTravelItineraryReadRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oTravelItineraryReadRQ.TimeStampSpecified = True

            oTravelItineraryReadRS = New TravelItineraryReadRQ.TravelItineraryReadRS
            oTravelItineraryReadRS = oTravelItineraryReadService.TravelItineraryReadRQ(oTravelItineraryReadRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(TravelItineraryReadRQ.TravelItineraryReadRQ), _
                                        GetType(TravelItineraryReadRQ.TravelItineraryReadRS), _
                                        oTravelItineraryReadRQ, _
                                        oTravelItineraryReadRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)



        Catch Ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_TravelItineraryRead" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.Message & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oTravelItineraryReadRQ = Nothing
            oTravelItineraryReadService = Nothing
            oUniqueID = Nothing

        End Try

        Return oTravelItineraryReadRS

    End Function
    Public Function _TravelItineraryReadRQ360(ByVal strPNR As String, _
                                           ByVal strTypeRemark As String, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intGDS As Integer, _
                                           ByVal intFirmaGDS As Integer, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal objSession As classSession) As TravelItineraryReadRQ360.TravelItineraryReadRS

        Const ID_SABRE_WEB_SERVICE As String = "52"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity1 As TravelItineraryReadRQ360.Security1 = Nothing
        Dim oUsernameToken As TravelItineraryReadRQ360.SecurityUsernameToken = Nothing
        Dim oMessageHeader As TravelItineraryReadRQ360.MessageHeader = Nothing
        Dim oFromPartyId As TravelItineraryReadRQ360.PartyId = Nothing
        Dim oFrom As TravelItineraryReadRQ360.From = Nothing
        Dim oFromPartyIdArr(0) As TravelItineraryReadRQ360.PartyId
        Dim oToPartyId As TravelItineraryReadRQ360.PartyId = Nothing
        Dim oToPartyIdArr(0) As TravelItineraryReadRQ360.PartyId
        Dim oTo As TravelItineraryReadRQ360.[To] = Nothing
        Dim oMessageData As TravelItineraryReadRQ360.MessageData = Nothing
        Dim oService As TravelItineraryReadRQ360.Service = Nothing

        Dim oTravelItineraryReadRQ As TravelItineraryReadRQ360.TravelItineraryReadRQ = Nothing
        Dim oTravelItineraryReadService As TravelItineraryReadRQ360.TravelItineraryReadService = Nothing
        Dim oTravelItineraryReadRS As TravelItineraryReadRQ360.TravelItineraryReadRS = Nothing

        Dim oMessagingDetails As TravelItineraryReadRQ360.TravelItineraryReadRQMessagingDetails = Nothing
        Dim oUniqueID As TravelItineraryReadRQ360.TravelItineraryReadRQUniqueID = Nothing

        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try
            oMessageHeader = New TravelItineraryReadRQ360.MessageHeader
            oFromPartyId = New TravelItineraryReadRQ360.PartyId
            oFrom = New TravelItineraryReadRQ360.From
            oToPartyId = New TravelItineraryReadRQ360.PartyId
            oTo = New TravelItineraryReadRQ360.[To]
            oService = New TravelItineraryReadRQ360.Service
            oMessageData = New TravelItineraryReadRQ360.MessageData
            oTravelItineraryReadService = New TravelItineraryReadRQ360.TravelItineraryReadService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oTravelItineraryReadService)

            oUsernameToken = New TravelItineraryReadRQ360.SecurityUsernameToken
            oSecurity1 = New TravelItineraryReadRQ360.Security1
            'oSource = New OTA_AirPrice.SessionCloseRQPOSSource
            'oPos = New OTA_AirPrice.SessionCloseRQPOS
            oTravelItineraryReadRQ = New TravelItineraryReadRQ360.TravelItineraryReadRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oTravelItineraryReadRQ, _
                         True, _
                         oTravelItineraryReadService, _
                         intFirmaGDS)

            oMessagingDetails = New TravelItineraryReadRQ360.TravelItineraryReadRQMessagingDetails
            Dim oSubjectAreas() As String
            ReDim oSubjectAreas(1)
            oSubjectAreas(0) = "FULL"
            'oSubjectAreas(1) = "POPULATE_IS_PAST"
            oMessagingDetails.SubjectAreas = oSubjectAreas


            oUniqueID = New TravelItineraryReadRQ360.TravelItineraryReadRQUniqueID
            oUniqueID.ID = strPNR.ToString
            oTravelItineraryReadRQ.UniqueID = oUniqueID

            oTravelItineraryReadRQ.MessagingDetails = oMessagingDetails

            oTravelItineraryReadRQ.Version = "3.6.0"

            oTravelItineraryReadRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oTravelItineraryReadRQ.TimeStampSpecified = True

            oTravelItineraryReadRS = New TravelItineraryReadRQ360.TravelItineraryReadRS
            oTravelItineraryReadRS = oTravelItineraryReadService.TravelItineraryReadRQ(oTravelItineraryReadRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(TravelItineraryReadRQ360.TravelItineraryReadRQ), _
                                        GetType(TravelItineraryReadRQ360.TravelItineraryReadRS), _
                                        oTravelItineraryReadRQ, _
                                        oTravelItineraryReadRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)



        Catch Ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_TravelItineraryRead" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.Message & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oTravelItineraryReadRQ = Nothing
            oTravelItineraryReadService = Nothing
            oUniqueID = Nothing

        End Try

        Return oTravelItineraryReadRS

    End Function
    Public Function _TravelItineraryAddInfo(ByVal strPNR As String, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intGDS As Integer, _
                                            ByVal intFirmaGDS As Integer, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal objSession As classSession) As TravelItineraryAddInfo.TravelItineraryAddInfoRS

        Const ID_SABRE_WEB_SERVICE As String = "06"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity1 As TravelItineraryAddInfo.Security1 = Nothing
        Dim oUsernameToken As TravelItineraryAddInfo.SecurityUsernameToken = Nothing
        Dim oMessageHeader As TravelItineraryAddInfo.MessageHeader = Nothing
        Dim oFromPartyId As TravelItineraryAddInfo.PartyId = Nothing
        Dim oFrom As TravelItineraryAddInfo.From = Nothing
        Dim oFromPartyIdArr(0) As TravelItineraryAddInfo.PartyId
        Dim oToPartyId As TravelItineraryAddInfo.PartyId = Nothing
        Dim oToPartyIdArr(0) As TravelItineraryAddInfo.PartyId
        Dim oTo As TravelItineraryAddInfo.[To] = Nothing
        Dim oMessageData As TravelItineraryAddInfo.MessageData = Nothing
        Dim oService As TravelItineraryAddInfo.Service = Nothing

        Dim oTravelItineraryAddInfoRQ As TravelItineraryAddInfo.TravelItineraryAddInfoRQ = Nothing
        Dim oTravelItineraryAddInfoService As TravelItineraryAddInfo.TravelItineraryAddInfoService = Nothing
        Dim oTravelItineraryAddInfoRS As TravelItineraryAddInfo.TravelItineraryAddInfoRS = Nothing

        Dim CustomerInfo As TravelItineraryAddInfo.TravelItineraryAddInfoRQCustomerInfo = Nothing



        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try
            oMessageHeader = New TravelItineraryAddInfo.MessageHeader
            oFromPartyId = New TravelItineraryAddInfo.PartyId
            oFrom = New TravelItineraryAddInfo.From
            oToPartyId = New TravelItineraryAddInfo.PartyId
            oTo = New TravelItineraryAddInfo.[To]
            oService = New TravelItineraryAddInfo.Service
            oMessageData = New TravelItineraryAddInfo.MessageData
            oTravelItineraryAddInfoService = New TravelItineraryAddInfo.TravelItineraryAddInfoService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oTravelItineraryAddInfoService)

            oUsernameToken = New TravelItineraryAddInfo.SecurityUsernameToken
            oSecurity1 = New TravelItineraryAddInfo.Security1
            'oSource = New OTA_AirPrice.SessionCloseRQPOSSource
            'oPos = New OTA_AirPrice.SessionCloseRQPOS
            oTravelItineraryAddInfoRQ = New TravelItineraryAddInfo.TravelItineraryAddInfoRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oTravelItineraryAddInfoRQ, _
                         True, _
                         oTravelItineraryAddInfoService, _
                         intFirmaGDS)



            CustomerInfo = New TravelItineraryAddInfo.TravelItineraryAddInfoRQCustomerInfo
            CustomerInfo.CustomerIdentifier = "23571"

            oTravelItineraryAddInfoRQ.CustomerInfo = New TravelItineraryAddInfo.TravelItineraryAddInfoRQCustomerInfo
            oTravelItineraryAddInfoRQ.CustomerInfo = CustomerInfo

            oTravelItineraryAddInfoRQ.ReturnHostCommand = True
            oTravelItineraryAddInfoRQ.ReturnHostCommandSpecified = True

            oTravelItineraryAddInfoRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oTravelItineraryAddInfoRQ.TimeStampSpecified = True

            oTravelItineraryAddInfoRS = oTravelItineraryAddInfoService.TravelItineraryAddInfoRQ(oTravelItineraryAddInfoRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(TravelItineraryAddInfo.TravelItineraryAddInfoRQ), _
                                        GetType(TravelItineraryAddInfo.TravelItineraryAddInfoRS), _
                                        oTravelItineraryAddInfoRQ, _
                                        oTravelItineraryAddInfoRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)



        Catch Ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_TravelItineraryAddInfo" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oTravelItineraryAddInfoRQ = Nothing
            oTravelItineraryAddInfoService = Nothing

        End Try

        Return oTravelItineraryAddInfoRS

    End Function
    Public Function _TravelItineraryModifyInfo(ByVal strDK As String, _
                                               ByVal objPersajero As List(Of classPasajeros), _
                                               ByVal strCodigoSeguimiento As String, _
                                               ByVal intGDS As Integer, _
                                               ByVal intFirmaGDS As Integer, _
                                               ByVal intFirmaDB As Integer, _
                                               ByVal objSession As classSession) As TravelItineraryModifyInfo.TravelItineraryModifyInfoRS

        Const ID_SABRE_WEB_SERVICE As String = "41"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity1 As TravelItineraryModifyInfo.Security1 = Nothing
        Dim oUsernameToken As TravelItineraryModifyInfo.SecurityUsernameToken = Nothing
        Dim oMessageHeader As TravelItineraryModifyInfo.MessageHeader = Nothing
        Dim oFromPartyId As TravelItineraryModifyInfo.PartyId = Nothing
        Dim oFrom As TravelItineraryModifyInfo.From = Nothing
        Dim oFromPartyIdArr(0) As TravelItineraryModifyInfo.PartyId
        Dim oToPartyId As TravelItineraryModifyInfo.PartyId = Nothing
        Dim oToPartyIdArr(0) As TravelItineraryModifyInfo.PartyId
        Dim oTo As TravelItineraryModifyInfo.[To] = Nothing
        Dim oMessageData As TravelItineraryModifyInfo.MessageData = Nothing
        Dim oService As TravelItineraryModifyInfo.Service = Nothing

        Dim oTravelItineraryModifyInfoRQ As TravelItineraryModifyInfo.TravelItineraryModifyInfoRQ = Nothing
        Dim oTravelItineraryModifyInfoService As TravelItineraryModifyInfo.TravelItineraryModifyInfoService = Nothing
        Dim oTravelItineraryModifyInfoRS As TravelItineraryModifyInfo.TravelItineraryModifyInfoRS = Nothing


        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try
            oMessageHeader = New TravelItineraryModifyInfo.MessageHeader
            oFromPartyId = New TravelItineraryModifyInfo.PartyId
            oFrom = New TravelItineraryModifyInfo.From
            oToPartyId = New TravelItineraryModifyInfo.PartyId
            oTo = New TravelItineraryModifyInfo.[To]
            oService = New TravelItineraryModifyInfo.Service
            oMessageData = New TravelItineraryModifyInfo.MessageData
            oTravelItineraryModifyInfoService = New TravelItineraryModifyInfo.TravelItineraryModifyInfoService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oTravelItineraryModifyInfoService)

            oUsernameToken = New TravelItineraryModifyInfo.SecurityUsernameToken
            oSecurity1 = New TravelItineraryModifyInfo.Security1
            'oSource = New OTA_AirPrice.SessionCloseRQPOSSource
            'oPos = New OTA_AirPrice.SessionCloseRQPOS
            oTravelItineraryModifyInfoRQ = New TravelItineraryModifyInfo.TravelItineraryModifyInfoRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oTravelItineraryModifyInfoRQ, _
                         True, _
                         oTravelItineraryModifyInfoService, _
                         intFirmaGDS)




            Dim CustomerInfo As TravelItineraryModifyInfo.TravelItineraryModifyInfoRQCustomerInfo = Nothing
            Dim CustomerIdentifier As TravelItineraryModifyInfo.TravelItineraryModifyInfoRQCustomerInfoCustomerIdentifier = Nothing

            Dim PersonName() As TravelItineraryModifyInfo.TravelItineraryModifyInfoRQCustomerInfoPersonName = Nothing
            Dim auxPersonName As TravelItineraryModifyInfo.TravelItineraryModifyInfoRQCustomerInfoPersonName = Nothing

            If Not String.IsNullOrEmpty(strDK) Then
                CustomerIdentifier = New TravelItineraryModifyInfo.TravelItineraryModifyInfoRQCustomerInfoCustomerIdentifier
                CustomerIdentifier.Identifier = strDK

                CustomerInfo = New TravelItineraryModifyInfo.TravelItineraryModifyInfoRQCustomerInfo
                CustomerInfo.CustomerIdentifier = CustomerIdentifier

                If oTravelItineraryModifyInfoRQ.CustomerInfo Is Nothing Then oTravelItineraryModifyInfoRQ.CustomerInfo = New TravelItineraryModifyInfo.TravelItineraryModifyInfoRQCustomerInfo
                oTravelItineraryModifyInfoRQ.CustomerInfo = CustomerInfo
            End If


            If objPersajero IsNot Nothing Then
                Dim flag As Boolean = False

                For i As Integer = 0 To objPersajero.Count - 1

                    flag = False
                    auxPersonName = New TravelItineraryModifyInfo.TravelItineraryModifyInfoRQCustomerInfoPersonName

                    If Not String.IsNullOrEmpty(objPersajero.Item(i).NumeroPasajero) Then
                        auxPersonName.NameNumber = objPersajero.Item(i).NumeroPasajero.ToString
                        flag = True
                    End If

                    If Not String.IsNullOrEmpty(objPersajero.Item(i).TipoPasajero) Then
                        auxPersonName.PassengerType = objPersajero.Item(i).TipoPasajero.ToString
                        flag = True
                    End If

                    If objPersajero.Item(i).Documento IsNot Nothing Then
                        If (Not String.IsNullOrEmpty(objPersajero.Item(i).Documento(0).Tipo)) And _
                           (Not String.IsNullOrEmpty(objPersajero.Item(i).Documento(0).Numero)) Then
                            auxPersonName.NameReference = objPersajero.Item(i).Documento(0).Tipo.ToString & objPersajero.Item(i).Documento(0).Numero.ToString
                            flag = True
                        End If
                    End If

                    If flag Then
                        If PersonName Is Nothing Then
                            ReDim PersonName(0)
                        Else
                            ReDim Preserve PersonName(PersonName.Length)
                        End If
                        PersonName(PersonName.Length - 1) = New TravelItineraryModifyInfo.TravelItineraryModifyInfoRQCustomerInfoPersonName
                        PersonName(PersonName.Length - 1) = auxPersonName
                    End If

                Next

                If PersonName IsNot Nothing Then

                    If CustomerInfo Is Nothing Then CustomerInfo = New TravelItineraryModifyInfo.TravelItineraryModifyInfoRQCustomerInfo
                    CustomerInfo.PersonName = PersonName

                    If oTravelItineraryModifyInfoRQ.CustomerInfo Is Nothing Then oTravelItineraryModifyInfoRQ.CustomerInfo = New TravelItineraryModifyInfo.TravelItineraryModifyInfoRQCustomerInfo
                    oTravelItineraryModifyInfoRQ.CustomerInfo = CustomerInfo
                End If

            End If

            oTravelItineraryModifyInfoRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oTravelItineraryModifyInfoRQ.TimeStampSpecified = True

            oTravelItineraryModifyInfoRQ.ReturnHostCommand = True

            oTravelItineraryModifyInfoRS = New TravelItineraryModifyInfo.TravelItineraryModifyInfoRS
            oTravelItineraryModifyInfoRS = oTravelItineraryModifyInfoService.TravelItineraryModifyInfoRQ(oTravelItineraryModifyInfoRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(TravelItineraryModifyInfo.TravelItineraryModifyInfoRQ), _
                                        GetType(TravelItineraryModifyInfo.TravelItineraryModifyInfoRS), _
                                        oTravelItineraryModifyInfoRQ, _
                                        oTravelItineraryModifyInfoRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)



        Catch Ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_TravelItineraryModifyInfo" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oTravelItineraryModifyInfoRQ = Nothing
            oTravelItineraryModifyInfoService = Nothing

        End Try

        Return oTravelItineraryModifyInfoRS

    End Function
    Public Function _OTA_AirPrice(ByVal listNumeroPasajeros As List(Of String), _
                                  ByVal listTipoPasajeros As List(Of String), _
                                  ByVal listNumeroSegmentos As List(Of String), _
                                  ByVal strTipoTarifa As String, _
                                  ByVal strCodigoSeguimiento As String, _
                                  ByVal intGDS As Integer, _
                                  ByVal intFirmaGDS As Integer, _
                                  ByVal intFirmaDB As Integer, _
                                  ByVal objSession As classSession, _
                                  ByVal strAccount As String, _
                                  ByVal strCorporateID As String, _
                                  ByVal strTouCode As String, _
                                  ByVal strNetRemit As String, _
                                  ByVal bolRetenerTarifa As Boolean) As OTA_AirPrice.OTA_AirPriceRS

        Const ID_SABRE_WEB_SERVICE As String = "08"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        '
        Dim oSecurity1 As OTA_AirPrice.Security1 = Nothing
        'Dim oPos As New OTA_AirPrice.OTA_AirPriceRQPOS
        'Dim oSource As New OTA_AirPrice.OTA_AirPriceRQPOSSource
        Dim oUsernameToken As OTA_AirPrice.SecurityUsernameToken = Nothing
        Dim oMessageHeader As OTA_AirPrice.MessageHeader = Nothing
        Dim oFromPartyId As OTA_AirPrice.PartyId = Nothing
        Dim oFrom As OTA_AirPrice.From = Nothing
        Dim oFromPartyIdArr(0) As OTA_AirPrice.PartyId
        Dim oToPartyId As OTA_AirPrice.PartyId = Nothing
        Dim oToPartyIdArr(0) As OTA_AirPrice.PartyId
        Dim oTo As New OTA_AirPrice.[To]
        Dim oMessageData As OTA_AirPrice.MessageData = Nothing
        Dim oService As OTA_AirPrice.Service = Nothing


        '
        Dim oAirPriceRQ As OTA_AirPrice.OTA_AirPriceRQ = Nothing
        Dim oAirPriceService As OTA_AirPrice.OTA_AirPriceService = Nothing
        Dim oAirPriceRS As OTA_AirPrice.OTA_AirPriceRS = Nothing

        '

        Dim oPriceRequestInformation As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformation = Nothing
        Dim oOptionalQualifiers As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiers = Nothing
        Dim oPricingQualifiers As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiers = Nothing
        Dim oFareOptions As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersFareOptions = Nothing

        Dim oNetRemit As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersNetRemit = Nothing
        Dim oNetRemitCAR As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersNetRemitCAR = Nothing
        Dim oNetRemitCash As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersNetRemitCash = Nothing
        Dim oNetRemitCredit As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersNetRemitCredit = Nothing

        Dim oFOP_Qualifiers As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_Qualifiers = Nothing
        Dim oBSP_Ticketing As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_Ticketing = Nothing
        Dim oMultipleFOP As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOP = Nothing
        Dim oFare As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFare = Nothing
        Dim oFOP_One As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_One = Nothing
        Dim oCC_Info As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_OneCC_Info = Nothing
        Dim oPaymentCard As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_OneCC_InfoPaymentCard = Nothing


        Dim oNameSelect() As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersNameSelect = Nothing
        Dim oSegmentSelect() As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersItineraryOptionsSegmentSelect = Nothing
        Dim oItineraryOptions As OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersItineraryOptions = Nothing


        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try
            oMessageHeader = New OTA_AirPrice.MessageHeader
            oFromPartyId = New OTA_AirPrice.PartyId
            oFrom = New OTA_AirPrice.From
            oToPartyId = New OTA_AirPrice.PartyId
            oTo = New OTA_AirPrice.[To]
            oService = New OTA_AirPrice.Service
            oMessageData = New OTA_AirPrice.MessageData
            oAirPriceService = New OTA_AirPrice.OTA_AirPriceService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oAirPriceService)

            oUsernameToken = New OTA_AirPrice.SecurityUsernameToken
            oSecurity1 = New OTA_AirPrice.Security1
            oAirPriceRQ = New OTA_AirPrice.OTA_AirPriceRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oAirPriceRQ, _
                         True, _
                         oAirPriceService, _
                         intFirmaGDS)



            oPriceRequestInformation = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformation
            oPriceRequestInformation.Retain = True


            If Not String.IsNullOrEmpty(strTipoTarifa) Then
                oFareOptions = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersFareOptions

                If strTipoTarifa.Equals(Constantes.IWS_PRIVATE) Then
                    oFareOptions.Private = True
                    oFareOptions.PrivateSpecified = True
                Else
                    oFareOptions.Public = True
                    oFareOptions.PublicSpecified = True
                End If

            End If

            '================= PASAJERO ===================
            If listNumeroPasajeros IsNot Nothing Then
                For i As Integer = 0 To listNumeroPasajeros.Count - 1
                    If oNameSelect Is Nothing Then
                        ReDim oNameSelect(0)
                    Else
                        ReDim Preserve oNameSelect(oNameSelect.Length)
                    End If

                    oNameSelect(oNameSelect.Length - 1) = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersNameSelect
                    oNameSelect(oNameSelect.Length - 1).NameNumber = listNumeroPasajeros.Item(i).ToString
                Next
            End If

            '================= SEGMENTO ===================
            oItineraryOptions = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersItineraryOptions

            If listNumeroSegmentos IsNot Nothing Then
                For i As Integer = 0 To listNumeroSegmentos.Count - 1
                    If oSegmentSelect Is Nothing Then
                        ReDim oSegmentSelect(0)
                    Else
                        ReDim Preserve oSegmentSelect(oSegmentSelect.Length)
                    End If

                    oSegmentSelect(oSegmentSelect.Length - 1) = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersItineraryOptionsSegmentSelect
                    oSegmentSelect(oSegmentSelect.Length - 1).Number = listNumeroSegmentos.Item(i).ToString
                Next
            End If

            '========= NET REMIT ==========

            If Not String.IsNullOrEmpty(strNetRemit) Then
                oNetRemit = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersNetRemit

                oNetRemit.Code = strNetRemit.Split(Constantes.Slash)(0)

                If strNetRemit.Split(Constantes.Slash).Length > 1 Then
                    oNetRemitCAR = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersNetRemitCAR
                    oNetRemitCAR.Code = strNetRemit.Split(Constantes.Slash)(1)

                    oNetRemit.CAR = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersNetRemitCAR
                    oNetRemit.CAR = oNetRemitCAR
                End If

                oNetRemitCash = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersNetRemitCash
                oNetRemitCash.Amount = "188.00"
                oNetRemitCash.CurrencyCode = "USD"

                oNetRemitCredit = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersNetRemitCredit
                oNetRemitCredit.Amount = "221.84"


                oNetRemit.Cash = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersNetRemitCash
                oNetRemit.Cash = oNetRemitCash
                oNetRemit.Credit = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersNetRemitCredit
                oNetRemit.Credit = oNetRemitCredit

            End If

            '========= FORMA DE PAGO ==========
            ''oFare = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFare
            ''oFare.Amount = "188.00"

            ''oCC_Info = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_OneCC_Info
            ''oCC_Info.Suppress = True
            ''oCC_Info.SuppressSpecified = True

            ''oPaymentCard = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_OneCC_InfoPaymentCard
            ''oPaymentCard.Code = "AX"
            ''oPaymentCard.Number = "376644556677889"
            ''oPaymentCard.ManualApprovalCode = "111111"
            ''oPaymentCard.ExpireDate = "2017-10"

            ''oCC_Info.PaymentCard = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_OneCC_InfoPaymentCard
            ''oCC_Info.PaymentCard = oPaymentCard


            ''oFOP_One = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_One
            ''oFOP_One.Type = "CA"
            ''oFOP_One.CC_Info = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_OneCC_Info
            ''oFOP_One.CC_Info = oCC_Info


            ''oMultipleFOP = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOP
            ''oMultipleFOP.Fare = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFare
            ''oMultipleFOP.Fare = oFare
            ''oMultipleFOP.FOP_One = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_One
            ''oMultipleFOP.FOP_One = oFOP_One


            ''oBSP_Ticketing = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_Ticketing
            ''oBSP_Ticketing.MultipleFOP = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOP
            ''oBSP_Ticketing.MultipleFOP = oMultipleFOP

            ''oFOP_Qualifiers = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_Qualifiers
            ''oFOP_Qualifiers.BSP_Ticketing = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_QualifiersBSP_Ticketing
            ''oFOP_Qualifiers.BSP_Ticketing = oBSP_Ticketing


            '========================================
            oItineraryOptions = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersItineraryOptions
            oItineraryOptions.SegmentSelect = oSegmentSelect


            oPricingQualifiers = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiers
            oPricingQualifiers.FareOptions = oFareOptions
            oPricingQualifiers.CurrencyCode = Constantes.IWS_AMERICAN_DOLLARS


            oPricingQualifiers.NameSelect = oNameSelect
            oPricingQualifiers.ItineraryOptions = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersItineraryOptions
            oPricingQualifiers.ItineraryOptions = oItineraryOptions

            If oNetRemit IsNot Nothing Then
                ReDim oPricingQualifiers.NetRemit(0)
                oPricingQualifiers.NetRemit(oPricingQualifiers.NetRemit.Length - 1) = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersPricingQualifiersNetRemit
                oPricingQualifiers.NetRemit(oPricingQualifiers.NetRemit.Length - 1) = oNetRemit
            End If

            oOptionalQualifiers = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiers
            oOptionalQualifiers.PricingQualifiers = oPricingQualifiers

            oOptionalQualifiers.FOP_Qualifiers = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiersFOP_Qualifiers
            If oFOP_Qualifiers IsNot Nothing Then
                oOptionalQualifiers.FOP_Qualifiers = oFOP_Qualifiers
            End If


            oPriceRequestInformation.OptionalQualifiers = New OTA_AirPrice.OTA_AirPriceRQPriceRequestInformationOptionalQualifiers
            oPriceRequestInformation.Retain = bolRetenerTarifa
            oPriceRequestInformation.RetainSpecified = bolRetenerTarifa

            oPriceRequestInformation.OptionalQualifiers = oOptionalQualifiers


            oAirPriceRQ.PriceRequestInformation = oPriceRequestInformation


            oAirPriceRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oAirPriceRQ.TimeStampSpecified = True

            oAirPriceRQ.ReturnHostCommand = True
            oAirPriceRQ.ReturnHostCommandSpecified = True


            oAirPriceRS = New OTA_AirPrice.OTA_AirPriceRS
            oAirPriceRS = oAirPriceService.OTA_AirPriceRQ(oAirPriceRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(OTA_AirPrice.OTA_AirPriceRQ), _
                                        GetType(OTA_AirPrice.OTA_AirPriceRS), _
                                        oAirPriceRQ, _
                                        oAirPriceRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)


        Catch Ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_OTA_AirPrice" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oPriceRequestInformation = Nothing
            oOptionalQualifiers = Nothing
            oPricingQualifiers = Nothing
            oFareOptions = Nothing
            '
            oAirPriceRQ = Nothing
            oAirPriceService = Nothing
            oGeneraPayLoadXML = Nothing
        End Try

        Return oAirPriceRS
    End Function
    ''Public Function _AirTicket(ByVal EmisionWeb As Object) As AirTicket.AirTicketRS

    ''    Const ID_SABRE_WEB_SERVICE As String = "12"
    ''    Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

    ''    Dim oSecurity1 As New AirTicket.Security1
    ''    'Dim oPos As New AirTicket.AirTicketRQPOS
    ''    'Dim oSource As New AirTicket.AirTicketRQPOSSource
    ''    Dim oUsernameToken As New AirTicket.SecurityUsernameToken
    ''    Dim oMessageHeader As New AirTicket.MessageHeader
    ''    Dim oFromPartyId As New AirTicket.PartyId
    ''    Dim oFrom As New AirTicket.From
    ''    Dim oFromPartyIdArr(0) As AirTicket.PartyId
    ''    Dim oToPartyId As New AirTicket.PartyId
    ''    Dim oToPartyIdArr(0) As AirTicket.PartyId
    ''    Dim oTo As New AirTicket.[To]
    ''    Dim oMessageData As New AirTicket.MessageData
    ''    Dim oService As New AirTicket.Service
    ''    '


    ''    Dim oOptionalQualifiers As AirTicket.AirTicketRQOptionalQualifiers = Nothing
    ''    Dim oMiscQualifiersTicket As AirTicket.AirTicketRQOptionalQualifiersMiscQualifiersTicket = Nothing

    ''    Dim oFOP_Qualifiers As AirTicket.AirTicketRQOptionalQualifiersFOP_Qualifiers = Nothing
    ''    Dim oBasicFOP As AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOP = Nothing
    ''    Dim oCC_Info As AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_Info = Nothing
    ''    Dim oPaymentCard As AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_InfoPaymentCard = Nothing


    ''    Dim oMultipleFOP As AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOP = Nothing
    ''    Dim oFOP_One As AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_One = Nothing
    ''    Dim oFOP_OneCC_Info As AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_OneCC_Info = Nothing
    ''    Dim oFOP_OneCC_InfoPaymentCard As AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_OneCC_InfoPaymentCard = Nothing
    ''    Dim oFOP_Two As AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_Two = Nothing

    ''    Dim oBSP_Ticketing As AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_Ticketing = Nothing
    ''    Dim FOPFare As AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFare = Nothing



    ''    Dim oAirTicketRQ As New AirTicket.AirTicketRQ
    ''    Dim oAirTicketService As New AirTicket.AirTicketService
    ''    Dim oAirTicketRS As AirTicket.AirTicketRS

    ''    Dim bolBasicFOP As Boolean = False


    ''    Try


    ''        oMessageHeader = New AirTicket.MessageHeader
    ''        oFromPartyId = New AirTicket.PartyId
    ''        oFrom = New AirTicket.From
    ''        oToPartyId = New AirTicket.PartyId
    ''        oTo = New AirTicket.[To]
    ''        oService = New AirTicket.Service
    ''        oMessageData = New AirTicket.MessageData
    ''        oAirTicketService = New AirTicket.AirTicketService

    ''        obj_Cabecera(ID_SABRE_WEB_SERVICE, _
    ''                     oMessageHeader, _
    ''                     oConversationID, _
    ''                     oFromPartyId, _
    ''                     oFromPartyIdArr, _
    ''                     oFrom, _
    ''                     oToPartyId, _
    ''                     oToPartyIdArr, _
    ''                     oTo, _
    ''                     Nothing, _
    ''                     oService, _
    ''                     oMessageData, _
    ''                     oAirTicketService)

    ''        oUsernameToken = New AirTicket.SecurityUsernameToken
    ''        oSecurity1 = New AirTicket.Security1
    ''        'oSource = New OTA_AirPrice.SessionCloseRQPOSSource
    ''        'oPos = New OTA_AirPrice.SessionCloseRQPOS
    ''        oAirTicketRQ = New AirTicket.AirTicketRQ

    ''        obj_Security(Nothing, _
    ''                     oToken, _
    ''                     oUsernameToken, _
    ''                     oSecurity1, _
    ''                     Nothing, _
    ''                     Nothing, _
    ''                     oAirTicketRQ, _
    ''                     True, _
    ''                     oAirTicketService)


    ''        'Tipo de Boleto
    ''        oMiscQualifiersTicket = New AirTicket.AirTicketRQOptionalQualifiersMiscQualifiersTicket
    ''        oMiscQualifiersTicket.Type = "ETR"

    ''        oOptionalQualifiers = New AirTicket.AirTicketRQOptionalQualifiers
    ''        oOptionalQualifiers.MiscQualifiers.Ticket = New AirTicket.AirTicketRQOptionalQualifiersMiscQualifiersTicket
    ''        oOptionalQualifiers.MiscQualifiers.Ticket = oMiscQualifiersTicket

    ''        Select Case EmisionWeb.FormaPago.Forma

    ''            Case "Cash" Or "Deposito/Transferencia"

    ''                oBasicFOP = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOP
    ''                oBasicFOP.Type = "CA"
    ''                bolBasicFOP = True

    ''            Case "Tarjeta de Credito"

    ''                oBasicFOP = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOP
    ''                oCC_Info = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_Info
    ''                oPaymentCard = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_InfoPaymentCard

    ''                oCC_Info.Suppress = True
    ''                oCC_Info.SuppressSpecified = True

    ''                oPaymentCard.Code = EmisionWeb.FormaPago.Tarjeta.Tipo
    ''                oPaymentCard.Number =  EmisionWeb.FormaPago.Tarjeta.Numero
    ''                oPaymentCard.ExpireDate = EmisionWeb.FormaPago.Tarjeta.FechaVencimiento '"2012-10"

    ''                If EmisionWeb.DK_Cliente = "23571" Then
    ''                    oPaymentCard.ManualApprovalCode = "111111"
    ''                End If

    ''                oCC_Info.PaymentCard = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_InfoPaymentCard
    ''                oCC_Info.PaymentCard = oPaymentCard

    ''                oBasicFOP.CC_Info = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_Info
    ''                oBasicFOP.CC_Info = oCC_Info

    ''                bolBasicFOP = True

    ''            Case "Card/Cash"

    ''                If EmisionWeb.EvaluacionTourCode.IndicardorTourCode <> "N" Then

    ''                    oFOP_One = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_One
    ''                    oFOP_OneCC_Info = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_OneCC_Info
    ''                    oFOP_OneCC_InfoPaymentCard = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_OneCC_InfoPaymentCard
    ''                    oFOP_Two = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_Two

    ''                    oMultipleFOP = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOP

    ''                    oFOP_OneCC_InfoPaymentCard.Code = EmisionWeb.FormaPago.CardCash.Tarjeta.Tipo
    ''                    oFOP_OneCC_InfoPaymentCard.Number = EmisionWeb.FormaPago.CardCash.Tarjeta.Numero
    ''                    oFOP_OneCC_InfoPaymentCard.ExpireDate = EmisionWeb.FormaPago.CardCash.Tarjeta.FechaVencimiento '"2012-10"

    ''                    If EmisionWeb.DK_Cliente = "23571" Then
    ''                        oFOP_OneCC_InfoPaymentCard.ManualApprovalCode = "111111"
    ''                    End If

    ''                    oFOP_OneCC_Info.PaymentCard = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_OneCC_InfoPaymentCard
    ''                    oFOP_OneCC_Info.PaymentCard = oFOP_OneCC_InfoPaymentCard

    ''                    oFOP_One.CC_Info = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_OneCC_Info
    ''                    oFOP_One.CC_Info = oFOP_OneCC_Info

    ''                    oFOP_Two.Type = "CA"

    ''                    oMultipleFOP.FOP_One = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_One
    ''                    oMultipleFOP.FOP_One = oFOP_One

    ''                    oMultipleFOP.FOP_Two = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFOP_Two
    ''                    oMultipleFOP.FOP_Two = oFOP_Two

    ''                    oMultipleFOP.Fare = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOPFare
    ''                    oMultipleFOP.Fare.Amount = EmisionWeb.FormaPago.CardCash.CashMonto '"16.40"


    ''                Else

    ''                    oBasicFOP = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOP
    ''                    oCC_Info = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_Info
    ''                    oPaymentCard = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_InfoPaymentCard

    ''                    oCC_Info.Suppress = True
    ''                    oCC_Info.SuppressSpecified = True

    ''                    oPaymentCard.Code = EmisionWeb.FormaPago.Tarjeta.Tipo
    ''                    oPaymentCard.Number = EmisionWeb.FormaPago.Tarjeta.Numero
    ''                    oPaymentCard.ExpireDate = EmisionWeb.FormaPago.Tarjeta.FechaVencimiento '"2012-10"

    ''                    If EmisionWeb.DK_Cliente = "23571" Then
    ''                        oPaymentCard.ManualApprovalCode = "111111"
    ''                    End If

    ''                    oCC_Info.PaymentCard = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_InfoPaymentCard
    ''                    oCC_Info.PaymentCard = oPaymentCard

    ''                    oBasicFOP.CC_Info = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_Info
    ''                    oBasicFOP.CC_Info = oCC_Info

    ''                    bolBasicFOP = True

    ''                End If

    ''        End Select

    ''        oFOP_Qualifiers = New AirTicket.AirTicketRQOptionalQualifiersFOP_Qualifiers

    ''        If bolBasicFOP Then
    ''            oFOP_Qualifiers.BasicFOP = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOP
    ''            oFOP_Qualifiers.BasicFOP = oBasicFOP
    ''        Else
    ''            oBSP_Ticketing = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_Ticketing
    ''            oBSP_Ticketing.MultipleFOP = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_TicketingMultipleFOP
    ''            oBSP_Ticketing.MultipleFOP = oMultipleFOP

    ''            oFOP_Qualifiers.BSP_Ticketing = New AirTicket.AirTicketRQOptionalQualifiersFOP_QualifiersBSP_Ticketing
    ''            oFOP_Qualifiers.BSP_Ticketing = oBSP_Ticketing
    ''        End If

    ''        oOptionalQualifiers.FOP_Qualifiers = New AirTicket.AirTicketRQOptionalQualifiersFOP_Qualifiers
    ''        oOptionalQualifiers.FOP_Qualifiers = oFOP_Qualifiers






    ''        oMiscQualifiers = New AirTicket.AirTicketRQOptionalQualifiersMiscQualifiers

    ''        'PASAJEROS Y SEGMENTOS A EMITIR

    ''        If CantPasajerosReserva > ArregloPasajeros.Length Then
    ''            For i = 0 To ArregloPasajeros.Length - 1
    ''                If oMiscQualifiers.NameSelect Is Nothing Then
    ''                    ReDim Preserve oMiscQualifiers.NameSelect(0)
    ''                Else
    ''                    ReDim Preserve oMiscQualifiers.NameSelect(oMiscQualifiers.NameSelect.Length)
    ''                End If
    ''                oMiscQualifiers.NameSelect(oMiscQualifiers.NameSelect.Length - 1) = New AirTicket.AirTicketRQOptionalQualifiersMiscQualifiersNameSelect
    ''                oMiscQualifiers.NameSelect(oMiscQualifiers.NameSelect.Length - 1).TravelerRefNumber = ArregloPasajeros(i)
    ''                oMiscQualifiers.NameSelect(oMiscQualifiers.NameSelect.Length - 1).EndTravelerRefNumber = ArregloPasajeros(i)
    ''                oMiscQualifiers.NameSelect(oMiscQualifiers.NameSelect.Length - 1).TravelerRefNumberSpecified = True
    ''                If i > 0 Then
    ''                    Dim Resultado As Integer
    ''                    Dim Numero1 As Integer
    ''                    Dim Numero2 As Integer
    ''                    Numero1 = CInt(oMiscQualifiers.NameSelect(i - 1).TravelerRefNumber.Substring(0, 1))
    ''                    Numero2 = CInt(oMiscQualifiers.NameSelect(i).TravelerRefNumber.Substring(0, 1))
    ''                    Resultado = Numero2 - Numero1
    ''                    If Resultado > 1 Then
    ''                        oMiscQualifiers.NameSelect(oMiscQualifiers.NameSelect.Length - 1).EndTravelerRefNumberSpecified = True
    ''                    End If
    ''                End If
    ''            Next
    ''        End If

    ''        If CantSegmentosReserva > ArregloSegmentos.Length Then
    ''            For i = 0 To ArregloSegmentos.Length - 1
    ''                If oMiscQualifiers.SegmentSelect Is Nothing Then
    ''                    ReDim Preserve oMiscQualifiers.SegmentSelect(0)
    ''                Else
    ''                    ReDim Preserve oMiscQualifiers.SegmentSelect(oMiscQualifiers.SegmentSelect.Length)
    ''                End If
    ''                oMiscQualifiers.SegmentSelect(oMiscQualifiers.SegmentSelect.Length - 1) = New AirTicket.AirTicketRQOptionalQualifiersMiscQualifiersSegmentSelect
    ''                oMiscQualifiers.SegmentSelect(oMiscQualifiers.SegmentSelect.Length - 1).ItinSeqNumber = ArregloSegmentos(i)
    ''                If ArregloSegmentos.Length > 1 Then
    ''                    oMiscQualifiers.SegmentSelect(oMiscQualifiers.SegmentSelect.Length - 1).EndItinSeqNumber = ArregloSegmentos(i)
    ''                End If
    ''            Next
    ''        End If

    ''        If EmisionWeb.EvaluacionTourCode.IndicardorTourCode <> "" Then
    ''            If EmisionWeb.EvaluacionTourCode.IndicardorTourCode = "T" Then
    ''                oTourCode = New AirTicket.AirTicketRQOptionalQualifiersMiscQualifiersTourCode
    ''                oSuppressIT = New AirTicket.AirTicketRQOptionalQualifiersMiscQualifiersTourCodeSuppressIT
    ''                oSuppressIT.IndSpecified = True
    ''                oSuppressIT.Ind = True

    ''                oTourCode.Text = EmisionWeb.EvaluacionTourCode.CodigoTourCode '"ZSSSXS"
    ''                oTourCode.SuppressIT = oSuppressIT

    ''                oMiscQualifiers.TourCode = oTourCode
    ''            End If

    ''            'NET REMIT
    ''            If EmisionWeb.EvaluacionTourCode.IndicardorTourCode = "N" Then

    ''                Dim CodNetRemit() As String = Split(EmisionWeb.EvaluacionTourCode.CodigoTourCode, "/")

    ''                oNetRemit = New AirTicket.AirTicketRQOptionalQualifiersPricingQualifiersNetRemit
    ''                ReDim oPricingQualifiers.NetRemit(0)
    ''                oPricingQualifiers.NetRemit(0) = New AirTicket.AirTicketRQOptionalQualifiersPricingQualifiersNetRemit

    ''                'ZVVVXV / PE11W1
    ''                'oNetRemit.Code = "ZVVVXV"
    ''                oPricingQualifiers.NetRemit(0).Code = CodNetRemit(0) '"ZVVVXV"

    ''                If CodNetRemit.Length > 1 Then
    ''                    oCAR = New AirTicket.AirTicketRQOptionalQualifiersPricingQualifiersNetRemitCAR
    ''                    oCAR.Code = CodNetRemit(1) '"PE11W1"
    ''                    oPricingQualifiers.NetRemit(0).CAR = oCAR
    ''                End If

    ''                oCash = New AirTicket.AirTicketRQOptionalQualifiersPricingQualifiersNetRemitCash
    ''                'oCash.Amount = CDec("20.00D") 'no valido
    ''                oCash.Amount = Neto
    ''                oCash.CurrencyCode = "USD"
    ''                oCash.AmountSpecified = True

    ''                oPricingQualifiers.NetRemit(0).Cash = oCash
    ''                'oNetRemit.CAR = oCAR
    ''                'oNetRemit.Cash = oCash

    ''                If EmisionWeb.FormaPago.Forma = "Tarjeta de Credito" Then
    ''                    oCredit = New AirTicket.AirTicketRQOptionalQualifiersPricingQualifiersNetRemitCredit
    ''                    'HAY QUE AGREGAR USD YA QUE NO TIENE ESTA PROPIEDAD EL WEB SERVICE
    ''                    oCredit.Amount = "USD" & EmisionWeb.FormaPago.Tarjeta.MontoxPax ' "USD2900.00"
    ''                    'oCredit.Amount = "USD" & EmisionWeb.FormaPago.Tarjeta.Monto ' "USD2900.00"
    ''                    oCredit.AmountSpecified = True
    ''                    oPricingQualifiers.NetRemit(0).Credit = oCredit
    ''                Else
    ''                    If EmisionWeb.FormaPago.Forma = "Card/Cash" Then
    ''                        oCredit = New AirTicket.AirTicketRQOptionalQualifiersPricingQualifiersNetRemitCredit
    ''                        'HAY QUE AGREGAR USD YA UQE NO TIENE ESTA PROPIEDAD EL WEB SERVICE
    ''                        oCredit.Amount = "USD" & EmisionWeb.FormaPago.CardCash.Tarjeta.Monto ' "USD2900.00"
    ''                        oCredit.AmountSpecified = True
    ''                        oPricingQualifiers.NetRemit(0).Credit = oCredit
    ''                    End If
    ''                End If
    ''                oOptionalQualifiers.PricingQualifiers = oPricingQualifiers
    ''            End If
    ''        End If


    ''        'INDICADOR DE TARIFA PUBLICADA
    ''        'oFare.Type = "PL"
    ''        'oPricingQualifiers.Fare = oFare
    ''        'oOptionalQualifiers.PricingQualifiers = oPricingQualifiers
    ''        'INDICADOR DE TARIFA PUBLICADA



    ''        'COMISION EN TKT
    ''        oCommission = New AirTicket.AirTicketRQOptionalQualifiersMiscQualifiersCommission
    ''        oCommission.PercentageSpecified = True
    ''        oCommission.Percentage = EmisionWeb.EvaluacionTourCode.ComisionTkt '"6"
    ''        oMiscQualifiers.Commission = oCommission

    ''        'LINEA AEREA VALIDADORA
    ''        oVendorPref = New AirTicket.AirTicketRQOptionalQualifiersMiscQualifiersVendorPref
    ''        oVendorPref.Code = EmisionWeb.LineaValidadora & "¥PL" '"AC"
    ''        oMiscQualifiers.VendorPref = oVendorPref

    ''        oOptionalQualifiers.MiscQualifiers = oMiscQualifiers

    ''        oEndTransaction = New AirTicket.AirTicketRQEndTransaction
    ''        oEndTransaction.Ind = True

    ''        oAirTicketRQ.EndTransaction = oEndTransaction
    ''        oAirTicketRQ.TicketingInfo = oTicketingInfo
    ''        oAirTicketRQ.OptionalQualifiers = oOptionalQualifiers

    ''        oAirTicketRS = oAirTicketService.AirTicketRQ(oAirTicketRQ)
    ''        Serialize(GetType(AirTicket.AirTicketRQ), GetType(AirTicket.AirTicketRS), oAirTicketRQ, oAirTicketRS, FUNCTION_NAME & "_EASY_" & sPNR)

    ''    Catch ex As Exception

    ''    Finally

    ''    End Try

    ''    Return objAirTicketRS

    ''End Function
    Public Function _VoidTicket(ByVal strLinea As String, _
                                ByVal strCodigoSeguimiento As String, _
                                ByVal intGDS As Integer, _
                                ByVal intFirmaGDS As Integer, _
                                ByVal intFirmaDB As Integer, _
                                ByVal objSession As classSession) As VoidTicket.VoidTicketRS


        Const ID_SABRE_WEB_SERVICE As String = "39"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity1 As VoidTicket.Security1 = Nothing

        Dim oUsernameToken As VoidTicket.SecurityUsernameToken = Nothing
        Dim oMessageHeader As VoidTicket.MessageHeader = Nothing

        Dim oFromPartyId As VoidTicket.PartyId = Nothing
        Dim oFrom As VoidTicket.From = Nothing
        Dim oFromPartyIdArr(0) As VoidTicket.PartyId
        Dim oToPartyId As VoidTicket.PartyId = Nothing
        Dim oToPartyIdArr(0) As VoidTicket.PartyId
        Dim oTo As VoidTicket.[To] = Nothing
        Dim oMessageData As VoidTicket.MessageData = Nothing
        Dim oService As VoidTicket.Service = Nothing

        Dim oVoidTicketRQ As VoidTicket.VoidTicketRQ = Nothing
        Dim oVoidTicketService As VoidTicket.VoidTicketService = Nothing
        Dim oVoidTicketRS As VoidTicket.VoidTicketRS = Nothing


        '========================================================================
        Dim oTicketing As VoidTicket.VoidTicketRQTicketing = Nothing
        '========================================================================

        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try


            oMessageHeader = New VoidTicket.MessageHeader
            oFromPartyId = New VoidTicket.PartyId
            oFrom = New VoidTicket.From
            oToPartyId = New VoidTicket.PartyId
            oTo = New VoidTicket.[To]
            oService = New VoidTicket.Service
            oMessageData = New VoidTicket.MessageData
            oVoidTicketService = New VoidTicket.VoidTicketService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oVoidTicketService)


            oUsernameToken = New VoidTicket.SecurityUsernameToken
            oSecurity1 = New VoidTicket.Security1
            oVoidTicketRQ = New VoidTicket.VoidTicketRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oVoidTicketRQ, _
                         True, _
                         oVoidTicketService, _
                         intFirmaGDS)

            '======================================
            oTicketing = New VoidTicket.VoidTicketRQTicketing

            oTicketing.RPH = strLinea
            oVoidTicketRQ.Ticketing = oTicketing

            oVoidTicketRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oVoidTicketRQ.TimeStampSpecified = True

            oVoidTicketRQ.ReturnHostCommand = True
            oVoidTicketRQ.ReturnHostCommandSpecified = True

            '======================================

            oVoidTicketRS = New VoidTicket.VoidTicketRS
            oVoidTicketRS = oVoidTicketService.VoidTicketRQ(oVoidTicketRQ)


            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(VoidTicket.VoidTicketRQ), _
                                        GetType(VoidTicket.VoidTicketRS), _
                                        oVoidTicketRQ, _
                                        oVoidTicketRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_VoidTicket" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(ex.ToString)

            'If Err.Description.IndexOf("Invalid or Expired binary security") > -1 Then
            '    Err.Raise(-9999, "_VoidTicket", "Invalid or Expired binary security")
            'Else
            '    Throw New Exception(ex.ToString)
            'End If

        Finally

            FUNCTION_NAME = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oVoidTicketRQ = Nothing
            oVoidTicketService = Nothing
            oGeneraPayLoadXML = Nothing

            strLinea = Nothing
            strCodigoSeguimiento = Nothing
            intGDS = Nothing
            intFirmaGDS = Nothing
            objSession = Nothing
        End Try

        Return oVoidTicketRS

    End Function

    ''Public Function _BargainFinderMaxSAPT(ByVal oToken As String, _
    ''                                      ByVal oConversationID As String, _
    ''                                      ByVal oPCC As String, _
    ''                                      Optional ByVal pExcluirAirlines() As String = Nothing, _
    ''                                      Optional ByVal RangoHoras As String = "", _
    ''                                      Optional ByVal pNacionalidad As String = Nothing, _
    ''                                      Optional ByVal pEnDolares As Boolean = True, _
    ''                                      Optional ByVal pPool As Boolean = False, _
    ''                                      Optional ByVal solo_Privadas As Boolean = False, _
    ''                                      Optional ByVal pStrIdUsuarioPool As Integer = 0, _
    ''                                      Optional ByVal oFlagPseudoOrigen As Boolean = False) As BargainFinderMaxSAPT.OTA_AirLowFareSearchRS

    ''    Const ID_SABRE_WEB_SERVICE As String = "36"
    ''    Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

    ''    Dim oSecurity As BargainFinderMaxSAPT.Security = Nothing

    ''    Dim oUsernameToken As BargainFinderMaxSAPT.SecurityUsernameToken = Nothing
    ''    Dim oMessageHeader As BargainFinderMaxSAPT.MessageHeader = Nothing

    ''    Dim oFromPartyId As BargainFinderMaxSAPT.PartyId = Nothing
    ''    Dim oFrom As BargainFinderMaxSAPT.From = Nothing
    ''    Dim oFromPartyIdArr(0) As BargainFinderMaxSAPT.PartyId
    ''    Dim oToPartyId As BargainFinderMaxSAPT.PartyId = Nothing
    ''    Dim oToPartyIdArr(0) As BargainFinderMaxSAPT.PartyId
    ''    Dim oTo As BargainFinderMaxSAPT.[To] = Nothing
    ''    Dim oMessageData As BargainFinderMaxSAPT.MessageData = Nothing
    ''    Dim oService As BargainFinderMaxSAPT.Service = Nothing

    ''    Dim oDescription(0) As BargainFinderMaxSAPT.Description
    ''    oDescription(0) = New BargainFinderMaxSAPT.Description
    ''    oDescription(0).lang = "en-us"
    ''    oDescription(0).Value = "Bargain Finder Max Service"

    ''    '==================================================

    ''    Dim oBargainFinderMaxRQPOS() As BargainFinderMaxSAPT.SourceType
    ''    Dim oOriginDestinationInformation() As BargainFinderMaxSAPT.OTA_AirLowFareSearchRQOriginDestinationInformation
    ''    Dim oOrigin As BargainFinderMaxSAPT.LocationType
    ''    Dim oDestination As BargainFinderMaxSAPT.LocationType
    ''    Dim oOriginDestinationInformationTPA_Extensions As BargainFinderMaxSAPT.OTA_AirLowFareSearchRQOriginDestinationInformationTPA_Extensions
    ''    Dim oSegmentType As New BargainFinderMaxSAPT.OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentType
    ''    Dim oSegmentTypeCode As New BargainFinderMaxSAPT.OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode
    ''    Dim oTravelPreferences As BargainFinderMaxSAPT.AirSearchPrefsType

    ''    Dim oCabinPref() As BargainFinderMaxSAPT.CabinPrefType
    ''    Dim oCabinType As BargainFinderMaxSAPT.CabinType
    ''    Dim oVendorPref() As BargainFinderMaxSAPT.CompanyNamePrefType
    ''    Dim oPreferLevelType As BargainFinderMaxSAPT.PreferLevelType
    ''    Dim oTravelPreferencesTPA_Extensions As BargainFinderMaxSAPT.AirSearchPrefsTypeTPA_Extensions
    ''    Dim oTripType As BargainFinderMaxSAPT.AirSearchPrefsTypeTPA_ExtensionsTripType
    ''    Dim oTravelerInfoSumary As BargainFinderMaxSAPT.TravelerInfoSummaryType
    ''    Dim oAirTravelerAvail() As BargainFinderMaxSAPT.TravelerInformationType


    ''    Dim oAirTraveler As BargainFinderMaxSAPT.AirTravelerType
    ''    Dim oPassengerTypeQuantity() As BargainFinderMaxSAPT.PassengerTypeQuantityType
    ''    Dim oPassengerTypeQuantity_PR() As BargainFinderMaxSAPT.PassengerTypeQuantityType
    ''    Dim oPassengerTypeQuantity_PR_2() As BargainFinderMaxSAPT.PassengerTypeQuantityType
    ''    Dim oPassengerTypeQuantity_PR_3() As BargainFinderMaxSAPT.PassengerTypeQuantityType
    ''    Dim oPassengerTypeQuantity_PR_4() As BargainFinderMaxSAPT.PassengerTypeQuantityType
    ''    Dim oTPA_Extensions As BargainFinderMaxSAPT.OTA_AirLowFareSearchRQTPA_Extensions
    ''    Dim oIntelliSellTransaction As BargainFinderMaxSAPT.TransactionType
    ''    Dim oRequestType As BargainFinderMaxSAPT.TransactionTypeRequestType

    ''    Dim FechaSal As BargainFinderMaxSAPT.TimeInstantType

    ''    Dim oUtil As New BargainFinderMaxSAPT.UniqueID_Type
    ''    Dim oCompany As New BargainFinderMaxSAPT.CompanyNameType

    ''    Dim oUtil1 As New BargainFinderMaxSAPT.UniqueID_Type
    ''    Dim oCompany1 As New BargainFinderMaxSAPT.CompanyNameType

    ''    Dim oPriceRequestInformation As New BargainFinderMaxSAPT.PriceRequestInformationType

    ''    Dim oBargainFinderMaxRQ As New BargainFinderMaxSAPT.OTA_AirLowFareSearchRQ
    ''    Dim oBargainFinderMaxService As New BargainFinderMaxSAPT.BargainFinderMaxService
    ''    Dim oBargainFinderMaxRS As New BargainFinderMaxSAPT.OTA_AirLowFareSearchRS

    ''    '==================================================

    ''    Dim oGeneraPayLoadXML As Servicios.GeneraPayLoadXML = Nothing
    ''    Dim oFlagSeCargoRS As Boolean = False

    ''    Dim IWS_BARGAINFINDERMAX_TRIPS As String = "50ITINS"

    ''    '-------------------
    ''    Dim CantPax As Integer = 0

    ''    Try

    ''        oMessageHeader = New BargainFinderMaxSAPT.MessageHeader
    ''        oFromPartyId = New BargainFinderMaxSAPT.PartyId
    ''        oFrom = New BargainFinderMaxSAPT.From
    ''        oToPartyId = New BargainFinderMaxSAPT.PartyId
    ''        oTo = New BargainFinderMaxSAPT.[To]
    ''        oService = New BargainFinderMaxSAPT.Service
    ''        oMessageData = New BargainFinderMaxSAPT.MessageData
    ''        oBargainFinderMaxService = New BargainFinderMaxSAPT.BargainFinderMaxService

    ''        obj_Cabecera(ID_SABRE_WEB_SERVICE, _
    ''                     oMessageHeader, _
    ''                     oConversationID, _
    ''                     oFromPartyId, _
    ''                     oFromPartyIdArr, _
    ''                     oFrom, _
    ''                     oToPartyId, _
    ''                     oToPartyIdArr, _
    ''                     oTo, _
    ''                     Nothing, _
    ''                     oService, _
    ''                     oMessageData, _
    ''                     oBargainFinderMaxService)


    ''        oUsernameToken = New BargainFinderMaxSAPT.SecurityUsernameToken
    ''        oSecurity = New BargainFinderMaxSAPT.Security
    ''        oBargainFinderMaxRQ = New BargainFinderMaxSAPT.OTA_AirLowFareSearchRQ

    ''        obj_Security(Nothing, _
    ''                     oToken, _
    ''                     oUsernameToken, _
    ''                     oSecurity, _
    ''                     Nothing, _
    ''                     Nothing, _
    ''                     oBargainFinderMaxRQ, _
    ''                     True, _
    ''                     oBargainFinderMaxService)

    ''        '======================================

    ''        ReDim oMessageHeader.Description(0)
    ''        oMessageHeader.Description(0) = New BargainFinderMaxSAPT.Description
    ''        oMessageHeader.Description = oDescription

    ''        ReDim oBargainFinderMaxRQPOS(0)
    ''        oBargainFinderMaxRQPOS(0) = New BargainFinderMaxSAPT.SourceType
    ''        oBargainFinderMaxRQPOS(0).PseudoCityCode = oPCC ' pStrPCCConsulta.Trim

    ''        'oBargainFinderMaxRQPOS(1) = New BargainFinderMax_SAPTRQ.SourceType
    ''        'oBargainFinderMaxRQPOS(1).PseudoCityCode = "QF05"

    ''        oCompany.Code = "TN"
    ''        oUtil.ID = "1"
    ''        oUtil.Type = "1"
    ''        oUtil.CompanyName = oCompany

    ''        'oCompany1.Code = "TN"
    ''        'oUtil1.ID = "2"
    ''        'oUtil1.Type = "1"
    ''        'oUtil1.CompanyName = oCompany1

    ''        oBargainFinderMaxRQPOS(0).RequestorID = oUtil
    ''        'oBargainFinderMaxRQPOS(1).RequestorID = oUtil1

    ''        'oBargainFinderMaxRQPOS(0).RequestorID

    ''        ReDim oPassengerTypeQuantity(0) '(oSearch.PassengerTypes.Length - 1)
    ''        ReDim oPassengerTypeQuantity_PR(0) 'oSearch.PassengerTypes.Length - 1)
    ''        ReDim oPassengerTypeQuantity_PR_2(0) 'oSearch.PassengerTypes.Length - 1)
    ''        ReDim oPassengerTypeQuantity_PR_3(0) 'oSearch.PassengerTypes.Length - 1)
    ''        ReDim oPassengerTypeQuantity_PR_4(0) 'oSearch.PassengerTypes.Length - 1)
    ''        '
    ''        For p As Integer = 0 To 0 'oSearch.PassengerTypes.Length - 1
    ''            oPassengerTypeQuantity(p) = New BargainFinderMaxSAPT.PassengerTypeQuantityType
    ''            oPassengerTypeQuantity_PR(p) = New BargainFinderMaxSAPT.PassengerTypeQuantityType
    ''            oPassengerTypeQuantity_PR_2(p) = New BargainFinderMaxSAPT.PassengerTypeQuantityType
    ''            oPassengerTypeQuantity_PR_3(p) = New BargainFinderMaxSAPT.PassengerTypeQuantityType
    ''            oPassengerTypeQuantity_PR_4(p) = New BargainFinderMaxSAPT.PassengerTypeQuantityType

    ''            Dim oPax As String = "ADT" 'oSearch.PassengerTypes(i).Passenger_Type.Id
    ''            Dim oPax_PR As String = ""
    ''            Dim oPax_PR_2 As String = ""
    ''            Dim oPax_PR_3 As String = "ITX"
    ''            Dim oPax_PR_4 As String = ""

    ''            Select Case oPax 'oSearch.PassengerTypes(i).Passenger_Type.Id
    ''                Case "ADT"
    ''                    oPax_PR = "PFA"
    ''                    oPax_PR_2 = "NEG"
    ''                    oPax_PR_3 = "ITX"
    ''                    oPax_PR_4 = "JCB"
    ''                Case "CNN"
    ''                    oPax_PR = "JNN"
    ''                    oPax_PR_2 = "CNE"
    ''                    oPax_PR_3 = "INN"
    ''                    oPax_PR_4 = "JNN"
    ''                Case "INF"
    ''                    oPax_PR = "JNF"
    ''                    oPax_PR_2 = "INE"
    ''                    oPax_PR_3 = "ITF"
    ''                    oPax_PR_4 = "JNF"
    ''            End Select

    ''            oPassengerTypeQuantity(p).Code = oPax
    ''            oPassengerTypeQuantity(p).Quantity = "1" 'oSearch.PassengerTypes(i).Quantity

    ''            oPassengerTypeQuantity_PR(p).Code = oPax_PR_2
    ''            oPassengerTypeQuantity_PR(p).Quantity = "1" 'oSearch.PassengerTypes(i).Quantity

    ''            oPassengerTypeQuantity_PR_2(p).Code = oPax_PR
    ''            oPassengerTypeQuantity_PR_2(p).Quantity = "1" 'oSearch.PassengerTypes(i).Quantity
    ''            'CantPax += Val(1) 'oSearch.PassengerTypes(i).Quantity)

    ''            oPassengerTypeQuantity_PR_3(p).Code = oPax_PR_3
    ''            oPassengerTypeQuantity_PR_3(p).Quantity = "1" 'oSearch.PassengerTypes(i).Quantity"

    ''            oPassengerTypeQuantity_PR_4(p).Code = oPax_PR_4
    ''            oPassengerTypeQuantity_PR_4(p).Quantity = "1" 'oSearch.PassengerTypes(i).Quantity

    ''            CantPax += Val(1) 'oSearch.PassengerTypes(i).Quantity)
    ''        Next
    ''        '
    ''        '/////
    ''        oTravelerInfoSumary = New BargainFinderMaxSAPT.TravelerInfoSummaryType
    ''        '/////
    ''        If solo_Privadas = False Then
    ''            ReDim Preserve oAirTravelerAvail(0)
    ''            oAirTravelerAvail(0) = New BargainFinderMaxSAPT.TravelerInformationType
    ''            oAirTravelerAvail(0).PassengerTypeQuantity = oPassengerTypeQuantity

    ''            ReDim Preserve oAirTravelerAvail(1)
    ''            oAirTravelerAvail(1) = New BargainFinderMaxSAPT.TravelerInformationType
    ''            oAirTravelerAvail(1).PassengerTypeQuantity = oPassengerTypeQuantity_PR

    ''            ReDim Preserve oAirTravelerAvail(2)
    ''            oAirTravelerAvail(2) = New BargainFinderMaxSAPT.TravelerInformationType
    ''            oAirTravelerAvail(2).PassengerTypeQuantity = oPassengerTypeQuantity_PR_2

    ''            ReDim Preserve oAirTravelerAvail(3)
    ''            oAirTravelerAvail(3) = New BargainFinderMaxSAPT.TravelerInformationType
    ''            oAirTravelerAvail(3).PassengerTypeQuantity = oPassengerTypeQuantity_PR_3
    ''        Else
    ''            ReDim Preserve oAirTravelerAvail(0)
    ''            oAirTravelerAvail(0) = New BargainFinderMaxSAPT.TravelerInformationType
    ''            oAirTravelerAvail(0).PassengerTypeQuantity = oPassengerTypeQuantity_PR

    ''            ReDim Preserve oAirTravelerAvail(1)
    ''            oAirTravelerAvail(1) = New BargainFinderMaxSAPT.TravelerInformationType
    ''            oAirTravelerAvail(1).PassengerTypeQuantity = oPassengerTypeQuantity_PR_2

    ''            ReDim Preserve oAirTravelerAvail(2)
    ''            oAirTravelerAvail(2) = New BargainFinderMaxSAPT.TravelerInformationType
    ''            oAirTravelerAvail(2).PassengerTypeQuantity = oPassengerTypeQuantity_PR_3

    ''            ReDim Preserve oAirTravelerAvail(3)
    ''            oAirTravelerAvail(3) = New BargainFinderMaxSAPT.TravelerInformationType
    ''            oAirTravelerAvail(3).PassengerTypeQuantity = oPassengerTypeQuantity_PR_4
    ''            'oPriceRequestInformation.NegotiatedFaresOnly = True
    ''            'oPriceRequestInformation.NegotiatedFaresOnlySpecified = True

    ''        End If

    ''        oTravelerInfoSumary.AirTravelerAvail = oAirTravelerAvail

    ''        oTravelerInfoSumary.PriceRequestInformation = oPriceRequestInformation


    ''        Dim aCant(0) As String
    ''        aCant(0) = CantPax
    ''        oTravelerInfoSumary.SeatsRequested = aCant
    ''        '/////
    ''        ReDim oOriginDestinationInformation(1) 'oSearch.FlightQuerysRequest.Length - 1)
    ''        ReDim oCabinPref(1) 'oOriginDestinationInformation.Length - 1)

    ''        ' richard ----------------------
    ''        Dim i As Integer = 0
    ''        Dim HoraPromedio As String

    ''        'For Each oConsulta In oSearch.FlightQuerys 'FlightQuerysRequest
    ''        oOriginDestinationInformationTPA_Extensions = New BargainFinderMaxSAPT.OTA_AirLowFareSearchRQOriginDestinationInformationTPA_Extensions
    ''        oSegmentType = New BargainFinderMaxSAPT.OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentType
    ''        oOriginDestinationInformation(i) = New BargainFinderMaxSAPT.OTA_AirLowFareSearchRQOriginDestinationInformation
    ''        With oOriginDestinationInformation(i)
    ''            '
    ''            .RPH = (i + 1).ToString.Trim
    ''            HoraPromedio = "01:00:00"
    ''            If "2013-10-25" <> "" Then 'oConsulta.Source.DepartureDate <> "" Then
    ''                oOrigin = New BargainFinderMaxSAPT.LocationType
    ''                oOrigin.LocationCode = "LIM" 'oConsulta.Source.City.Id
    ''                .OriginLocation = oOrigin
    ''            End If

    ''            If "2013-10-25" <> "" Then 'oConsulta.Source.DepartureDate <> "" Then
    ''                FechaSal = New BargainFinderMaxSAPT.TimeInstantType
    ''                FechaSal.Value = "2013-10-25T01:00:00" ' oConsulta.Source.DepartureDate & "T" & HoraPromedio
    ''                .ItemElementName = BargainFinderMaxSAPT.ItemChoiceType.DepartureDateTime
    ''                .Item = FechaSal
    ''                '.DepartureWindow = oConsulta.Source.DepartureDate & "T" & HoraPromedio
    ''            End If
    ''            'FechaSal = BargainFinderMaxRQ.ItemChoiceType.DepartureDateTime
    ''            'FechaSal = oConsulta.Source.DepartureDate & "T" & HoraPromedio
    ''            ' --------------------------------------------

    ''            '.OriginLocation = oOrigin
    ''            '
    ''            oDestination = New BargainFinderMaxSAPT.LocationType
    ''            oDestination.LocationCode = "MIA" 'oConsulta.Target.City.Id
    ''            '
    ''            .DestinationLocation = oDestination
    ''            '

    ''            If "2013-10-25" <> "" Then 'oConsulta.Source.DepartureDate <> "" Then
    ''                oSegmentType.Code = BargainFinderMaxSAPT.OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O
    ''            Else
    ''                oSegmentType.Code = BargainFinderMaxSAPT.OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK
    ''            End If

    ''            oSegmentType.CodeSpecified = True
    ''            '
    ''            oOriginDestinationInformationTPA_Extensions.SegmentType = oSegmentType
    ''            '
    ''            .TPA_Extensions = oOriginDestinationInformationTPA_Extensions

    ''            i += 1
    ''        End With
    ''        'Next

    ''        '¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿

    ''        oOriginDestinationInformationTPA_Extensions = New BargainFinderMaxSAPT.OTA_AirLowFareSearchRQOriginDestinationInformationTPA_Extensions
    ''        oSegmentType = New BargainFinderMaxSAPT.OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentType
    ''        oOriginDestinationInformation(i) = New BargainFinderMaxSAPT.OTA_AirLowFareSearchRQOriginDestinationInformation
    ''        With oOriginDestinationInformation(i)
    ''            '
    ''            .RPH = (i + 1).ToString.Trim
    ''            HoraPromedio = "01:00:00"
    ''            If "2013-10-30" <> "" Then ' oConsulta.Source.DepartureDate <> "" Then
    ''                oOrigin = New BargainFinderMaxSAPT.LocationType
    ''                oOrigin.LocationCode = "MIA" 'oConsulta.Source.City.Id
    ''                .OriginLocation = oOrigin
    ''            End If

    ''            If "2013-10-30" <> "" Then 'oConsulta.Source.DepartureDate <> "" Then
    ''                FechaSal = New BargainFinderMaxSAPT.TimeInstantType
    ''                FechaSal.Value = "2013-10-30T01:00:00" 'oConsulta.Source.DepartureDate & "T" & HoraPromedio
    ''                .ItemElementName = BargainFinderMaxSAPT.ItemChoiceType.DepartureDateTime
    ''                .Item = FechaSal
    ''                '.DepartureWindow = oConsulta.Source.DepartureDate & "T" & HoraPromedio
    ''            End If
    ''            'FechaSal = BargainFinderMaxRQ.ItemChoiceType.DepartureDateTime
    ''            'FechaSal = oConsulta.Source.DepartureDate & "T" & HoraPromedio
    ''            ' --------------------------------------------

    ''            '.OriginLocation = oOrigin
    ''            '
    ''            oDestination = New BargainFinderMaxSAPT.LocationType
    ''            oDestination.LocationCode = "LIM" 'oConsulta.Target.City.Id
    ''            '
    ''            .DestinationLocation = oDestination
    ''            '
    ''            If "2013-10-30" <> "" Then 'oConsulta.Source.DepartureDate <> "" Then
    ''                oSegmentType.Code = BargainFinderMaxSAPT.OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O
    ''            Else
    ''                oSegmentType.Code = BargainFinderMaxSAPT.OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.ARUNK
    ''            End If

    ''            oSegmentType.CodeSpecified = True
    ''            '
    ''            oOriginDestinationInformationTPA_Extensions.SegmentType = oSegmentType
    ''            '
    ''            .TPA_Extensions = oOriginDestinationInformationTPA_Extensions

    ''            i += 1
    ''        End With
    ''        '¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿


    ''        '
    ''        oTravelPreferences = New BargainFinderMaxSAPT.AirSearchPrefsType

    ''        oTravelPreferences.MaxStopsQuantity = "2" 'oSearch.Stops

    ''        ReDim oCabinPref(0)
    ''        oCabinPref(0) = New BargainFinderMaxSAPT.CabinPrefType
    ''        oCabinType = BargainFinderMaxSAPT.CabinType.Economy
    ''        'If oSearch.Service_Class = "Y" Then
    ''        '    oCabinType = BargainFinderMaxRQ.CabinType.Economy
    ''        'ElseIf oSearch.Service_Class = "C" Then
    ''        '    oCabinType = BargainFinderMaxRQ.CabinType.Business
    ''        'ElseIf oSearch.Service_Class = "F" Then
    ''        '    oCabinType = BargainFinderMaxRQ.CabinType.First
    ''        'End If

    ''        oCabinPref(0).Cabin = oCabinType
    ''        oPreferLevelType = BargainFinderMaxSAPT.PreferLevelType.Preferred
    ''        oCabinPref(0).PreferLevel = oPreferLevelType
    ''        oTravelPreferences.CabinPref = oCabinPref

    ''        'If Not oSearch.Airline Is Nothing Then
    ''        '    If oSearch.Airline.Id.trim <> "" Then
    ''        '        Dim CadenaAerolinea As String = oSearch.Airline.Id.ToString.Trim
    ''        '        Dim LargoCadena As Integer = CadenaAerolinea.Trim.Length
    ''        '        Dim Cant As Integer
    ''        '        Cant = CInt(LargoCadena) / 2
    ''        '        Dim x As Integer = 0

    ''        '        ReDim Preserve oVendorPref(Cant - 1)
    ''        '        For x = 0 To Cant - 1
    ''        '            oVendorPref(x) = New BargainFinderMaxSAPT.CompanyNamePrefType
    ''        '            oVendorPref(x).Code = CadenaAerolinea.Substring(0, 2)
    ''        '            CadenaAerolinea = CadenaAerolinea.Substring(2)
    ''        '        Next
    ''        '        oTravelPreferences.VendorPref = oVendorPref
    ''        '    End If
    ''        'End If

    ''        '
    ''        oTPA_Extensions = New BargainFinderMaxSAPT.OTA_AirLowFareSearchRQTPA_Extensions
    ''        oRequestType = New BargainFinderMaxSAPT.TransactionTypeRequestType
    ''        oIntelliSellTransaction = New BargainFinderMaxSAPT.TransactionType

    ''        oRequestType.Name = IWS_BARGAINFINDERMAX_TRIPS
    ''        oIntelliSellTransaction.RequestType = oRequestType

    ''        Dim CompressResponse As New BargainFinderMaxSAPT.TransactionTypeCompressResponse
    ''        CompressResponse.Value = True
    ''        oIntelliSellTransaction.CompressResponse = CompressResponse

    ''        oTPA_Extensions.IntelliSellTransaction = oIntelliSellTransaction

    ''        oBargainFinderMaxRQ.Version = "1.6.1"
    ''        oBargainFinderMaxRQ.AvailableFlightsOnly = True

    ''        '
    ''        With oBargainFinderMaxRQ
    ''            .POS = oBargainFinderMaxRQPOS
    ''            .TravelerInfoSummary = oTravelerInfoSumary
    ''            .TravelPreferences = oTravelPreferences
    ''            .TPA_Extensions = oTPA_Extensions
    ''            .OriginDestinationInformation = oOriginDestinationInformation
    ''        End With
    ''        '
    ''        'oBargainFinderMaxService.EnableDecompression = True
    ''        oBargainFinderMaxService.SoapVersion = Web.Services.Protocols.SoapProtocolVersion.Soap11

    ''        oBargainFinderMaxRS = oBargainFinderMaxService.BargainFinderMax_SAPTRQ(oBargainFinderMaxRQ)



    ''        oGeneraPayLoadXML = New Servicios.GeneraPayLoadXML
    ''        oGeneraPayLoadXML.Serialize(GetType(BargainFinderMaxSAPT.OTA_AirLowFareSearchRQ), GetType(BargainFinderMaxSAPT.OTA_AirLowFareSearchRS), oBargainFinderMaxRQ, oBargainFinderMaxRS, FUNCTION_NAME & "_EASY_" & sPNR)
    ''        ' ----------------------------------------------------

    ''    Catch ex As Exception
    ''        Err.Raise(Err.Number, "_ContextChange", Err.Description)

    ''    Finally

    ''        FUNCTION_NAME = Nothing
    ''        oSecurity = Nothing
    ''        oUsernameToken = Nothing
    ''        oMessageHeader = Nothing
    ''        oFromPartyId = Nothing
    ''        oFrom = Nothing
    ''        oFromPartyIdArr = Nothing
    ''        oToPartyId = Nothing
    ''        oToPartyIdArr = Nothing
    ''        oTo = Nothing
    ''        oMessageData = Nothing
    ''        oService = Nothing

    ''        oBargainFinderMaxRQ = Nothing
    ''        oBargainFinderMaxService = Nothing
    ''        oGeneraPayLoadXML = Nothing

    ''    End Try

    ''    Return oBargainFinderMaxRS

    ''End Function

    'Public Function _DesignatePrinter(ByVal objDesignatePrinterRQ As NuevoMundoUtility.SWS_DesignatePrinter.DesignatePrinterRQ) As DesignatePrinter.DesignatePrinterRS

    '    Const ID_SABRE_WEB_SERVICE As String = "22"
    '    Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
    '    '
    '    Dim oSecurity1 As DesignatePrinter.Security1 = Nothing
    '    'Dim oPos As New OTA_AirPrice.OTA_AirPriceRQPOS
    '    'Dim oSource As New OTA_AirPrice.OTA_AirPriceRQPOSSource
    '    Dim oUsernameToken As DesignatePrinter.SecurityUsernameToken = Nothing
    '    Dim oMessageHeader As DesignatePrinter.MessageHeader = Nothing
    '    Dim oFromPartyId As DesignatePrinter.PartyId = Nothing
    '    Dim oFrom As DesignatePrinter.From = Nothing
    '    Dim oFromPartyIdArr(0) As DesignatePrinter.PartyId
    '    Dim oToPartyId As DesignatePrinter.PartyId = Nothing
    '    Dim oToPartyIdArr(0) As DesignatePrinter.PartyId
    '    Dim oTo As New DesignatePrinter.[To]
    '    Dim oMessageData As DesignatePrinter.MessageData = Nothing
    '    Dim oService As DesignatePrinter.Service = Nothing

    '    '
    '    Dim oDesignatePrinterRQ As DesignatePrinter.DesignatePrinterRQ = Nothing
    '    Dim oDesignatePrinterService As DesignatePrinter.DesignatePrinterService = Nothing
    '    Dim oDesignatePrinterRS As DesignatePrinter.DesignatePrinterRS = Nothing

    '    '
    '    Dim oProfile As DesignatePrinter.DesignatePrinterRQProfile = Nothing


    '    Dim oGeneraPayLoadXML As GeneraPayLoadXML = Nothing
    '    Dim strProfileNumber As String = Nothing

    '    Try
    '        oMessageHeader = New DesignatePrinter.MessageHeader
    '        oFromPartyId = New DesignatePrinter.PartyId
    '        oFrom = New DesignatePrinter.From
    '        oToPartyId = New DesignatePrinter.PartyId
    '        oTo = New DesignatePrinter.[To]
    '        oService = New DesignatePrinter.Service
    '        oMessageData = New DesignatePrinter.MessageData
    '        oDesignatePrinterService = New DesignatePrinter.DesignatePrinterService

    '        obj_Cabecera(ID_SABRE_WEB_SERVICE, _
    '                     oMessageHeader, _
    '                     objDesignatePrinterRQ.ConversationID, _
    '                     oFromPartyId, _
    '                     oFromPartyIdArr, _
    '                     oFrom, _
    '                     oToPartyId, _
    '                     oToPartyIdArr, _
    '                     oTo, _
    '                     Nothing, _
    '                     oService, _
    '                     oMessageData, _
    '                     oDesignatePrinterService)

    '        oUsernameToken = New DesignatePrinter.SecurityUsernameToken
    '        oSecurity1 = New DesignatePrinter.Security1
    '        'oSource = New OTA_AirPrice.SessionCloseRQPOSSource
    '        'oPos = New OTA_AirPrice.SessionCloseRQPOS
    '        oDesignatePrinterRQ = New DesignatePrinter.DesignatePrinterRQ

    '        obj_Security(Nothing, _
    '                     objDesignatePrinterRQ.Token, _
    '                     oUsernameToken, _
    '                     oSecurity1, _
    '                     Nothing, _
    '                     Nothing, _
    '                     oDesignatePrinterRQ, _
    '                     True, _
    '                     oDesignatePrinterService)



    '        Select Case objDesignatePrinterRQ.PCC
    '            Case "QF05", "HW57"
    '                strProfileNumber = "10"
    '            Case "QP75"
    '                strProfileNumber = "8"
    '        End Select


    '        oProfile = New DesignatePrinter.DesignatePrinterRQProfile
    '        oProfile.Number = strProfileNumber


    '        oDesignatePrinterRQ.Profile = New DesignatePrinter.DesignatePrinterRQProfile
    '        oDesignatePrinterRQ.Profile = oProfile

    '        oDesignatePrinterRQ.TimeStamp = "0001-01-01T12:00:00Z"
    '        oDesignatePrinterRQ.TimeStampSpecified = True

    '        oDesignatePrinterRQ.ReturnHostCommand = True
    '        oDesignatePrinterRQ.ReturnHostCommandSpecified = True


    '        oDesignatePrinterRS = New DesignatePrinter.DesignatePrinterRS

    '        oDesignatePrinterRS = oDesignatePrinterService.DesignatePrinterRQ(oDesignatePrinterRQ)

    '        oGeneraPayLoadXML = New GDS_NM_WebServicesSabre.GeneraPayLoadXML
    '        oGeneraPayLoadXML.Serialize(GetType(DesignatePrinter.DesignatePrinterRQ), GetType(DesignatePrinter.DesignatePrinterRS), oDesignatePrinterRQ, oDesignatePrinterRS, FUNCTION_NAME & "_EASY_" & objDesignatePrinterRQ.PNR)


    '    Catch Ex As Exception
    '        Throw New Exception(Ex.ToString)
    '    Finally
    '        FUNCTION_NAME = Nothing
    '        '
    '        oSecurity1 = Nothing
    '        oUsernameToken = Nothing
    '        oMessageHeader = Nothing
    '        oFromPartyId = Nothing
    '        oFrom = Nothing
    '        oFromPartyIdArr = Nothing
    '        oToPartyId = Nothing
    '        oToPartyIdArr = Nothing
    '        oTo = Nothing
    '        oMessageData = Nothing
    '        oService = Nothing
    '        '
    '        oDesignatePrinterRQ = Nothing
    '        oDesignatePrinterService = Nothing
    '        '
    '        oProfile = Nothing
    '        oGeneraPayLoadXML = Nothing
    '    End Try

    '    Return oDesignatePrinterRS

    'End Function
    'Public Function _QueueAccess(ByVal objQueueAccessRQ As NuevoMundoUtility.SWS_QueueAccess.QueueAccessRQ) As QueueAccess.QueueAccessRS


    '    Const ID_SABRE_WEB_SERVICE As String = "38"
    '    Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

    '    Dim oSecurity1 As QueueAccess.Security1 = Nothing

    '    Dim oUsernameToken As QueueAccess.SecurityUsernameToken = Nothing
    '    Dim oMessageHeader As QueueAccess.MessageHeader = Nothing

    '    Dim oFromPartyId As QueueAccess.PartyId = Nothing
    '    Dim oFrom As QueueAccess.From = Nothing
    '    Dim oFromPartyIdArr(0) As QueueAccess.PartyId
    '    Dim oToPartyId As QueueAccess.PartyId = Nothing
    '    Dim oToPartyIdArr(0) As QueueAccess.PartyId
    '    Dim oTo As QueueAccess.[To] = Nothing
    '    Dim oMessageData As QueueAccess.MessageData = Nothing
    '    Dim oService As QueueAccess.Service = Nothing

    '    Dim oQueueAccessRQ As QueueAccess.QueueAccessRQ = Nothing
    '    Dim oQueueAccessService As QueueAccess.QueueAccessService = Nothing
    '    Dim oQueueAccessRS As QueueAccess.QueueAccessRS = Nothing


    '    '========================================================================
    '    Dim oQueueIdentifier As QueueAccess.QueueAccessRQQueueIdentifier = Nothing
    '    Dim oQueueIdentifierList As QueueAccess.QueueAccessRQQueueIdentifierList = Nothing

    '    Dim oNavigation As QueueAccess.QueueAccessRQNavigation = Nothing
    '    Dim oNavigationDirection As QueueAccess.QueueAccessRQNavigationDirection = Nothing

    '    '========================================================================

    '    Dim oGeneraPayLoadXML As GDS_NM_WebServicesSabre.GeneraPayLoadXML = Nothing

    '    Try


    '        oMessageHeader = New QueueAccess.MessageHeader
    '        oFromPartyId = New QueueAccess.PartyId
    '        oFrom = New QueueAccess.From
    '        oToPartyId = New QueueAccess.PartyId
    '        oTo = New QueueAccess.[To]
    '        oService = New QueueAccess.Service
    '        oMessageData = New QueueAccess.MessageData
    '        oQueueAccessService = New QueueAccess.QueueAccessService

    '        obj_Cabecera(ID_SABRE_WEB_SERVICE, _
    '                     oMessageHeader, _
    '                     objQueueAccessRQ.ConversationID, _
    '                     oFromPartyId, _
    '                     oFromPartyIdArr, _
    '                     oFrom, _
    '                     oToPartyId, _
    '                     oToPartyIdArr, _
    '                     oTo, _
    '                     Nothing, _
    '                     oService, _
    '                     oMessageData, _
    '                     oQueueAccessService)


    '        oUsernameToken = New QueueAccess.SecurityUsernameToken
    '        oSecurity1 = New QueueAccess.Security1
    '        oQueueAccessRQ = New QueueAccess.QueueAccessRQ

    '        obj_Security(Nothing, _
    '                     objQueueAccessRQ.Token, _
    '                     oUsernameToken, _
    '                     oSecurity1, _
    '                     Nothing, _
    '                     Nothing, _
    '                     oQueueAccessRQ, _
    '                     True, _
    '                     oQueueAccessService)

    '        '======================================

    '        Select Case UCase(objQueueAccessRQ.Accion)

    '            Case "INGRESO"

    '                oQueueIdentifier = New QueueAccess.QueueAccessRQQueueIdentifier
    '                oQueueIdentifier.PseudoCityCode = objQueueAccessRQ.PCC
    '                oQueueIdentifier.Number = objQueueAccessRQ.Number
    '                oQueueIdentifierList = New QueueAccess.QueueAccessRQQueueIdentifierList
    '                oQueueIdentifierList.Ind = False
    '                oQueueIdentifier.List = oQueueIdentifierList

    '                oQueueAccessRQ.QueueIdentifier = New QueueAccess.QueueAccessRQQueueIdentifier
    '                oQueueAccessRQ.QueueIdentifier = oQueueIdentifier



    '            Case "LISTADO"

    '                oQueueIdentifier = New QueueAccess.QueueAccessRQQueueIdentifier
    '                oQueueIdentifier.PseudoCityCode = objQueueAccessRQ.PCC
    '                oQueueIdentifier.Number = objQueueAccessRQ.Number
    '                oQueueIdentifierList = New QueueAccess.QueueAccessRQQueueIdentifierList
    '                oQueueIdentifierList.Ind = True
    '                oQueueIdentifier.List = oQueueIdentifierList

    '                oQueueAccessRQ.QueueIdentifier = New QueueAccess.QueueAccessRQQueueIdentifier
    '                oQueueAccessRQ.QueueIdentifier = oQueueIdentifier


    '            Case "ELIMINAR"

    '                oNavigation = New QueueAccess.QueueAccessRQNavigation
    '                oNavigation.Action = New QueueAccess.QueueAccessRQNavigationAction
    '                oNavigation.Action = QueueAccess.QueueAccessRQNavigationAction.QR
    '                oNavigation.ActionSpecified = True

    '                'oNavigationDirection = New QueueAccess.QueueAccessRQNavigationDirection
    '                'oNavigationDirection.Action = New QueueAccess.QueueAccessRQNavigationDirectionAction
    '                'oNavigationDirection.Action = QueueAccess.QueueAccessRQNavigationDirectionAction.E
    '                'oNavigationDirection.Plus = "1"

    '                'oNavigation.Direction = New QueueAccess.QueueAccessRQNavigationDirection
    '                'oNavigation.Direction = oNavigationDirection

    '                oQueueAccessRQ.Navigation = New QueueAccess.QueueAccessRQNavigation
    '                oQueueAccessRQ.Navigation = oNavigation


    '            Case "NAVEGACION"

    '                oNavigation = New QueueAccess.QueueAccessRQNavigation
    '                oNavigation.Action = New QueueAccess.QueueAccessRQNavigationAction
    '                oNavigation.Action = QueueAccess.QueueAccessRQNavigationAction.I
    '                oNavigation.ActionSpecified = True

    '                'oNavigationDirection = New QueueAccess.QueueAccessRQNavigationDirection
    '                'oNavigationDirection.Action = New QueueAccess.QueueAccessRQNavigationDirectionAction
    '                'oNavigationDirection.Action = QueueAccess.QueueAccessRQNavigationDirectionAction.E
    '                'oNavigationDirection.Plus = "1"

    '                'oNavigation.Direction = New QueueAccess.QueueAccessRQNavigationDirection
    '                'oNavigation.Direction = oNavigationDirection

    '                oQueueAccessRQ.Navigation = New QueueAccess.QueueAccessRQNavigation
    '                oQueueAccessRQ.Navigation = oNavigation

    '            Case "SALIR"

    '                oNavigation = New QueueAccess.QueueAccessRQNavigation
    '                oNavigation.Action = New QueueAccess.QueueAccessRQNavigationAction
    '                oNavigation.Action = QueueAccess.QueueAccessRQNavigationAction.QXI
    '                oNavigation.ActionSpecified = True

    '                'oNavigationDirection = New QueueAccess.QueueAccessRQNavigationDirection
    '                'oNavigationDirection.Action = New QueueAccess.QueueAccessRQNavigationDirectionAction
    '                'oNavigationDirection.Action = QueueAccess.QueueAccessRQNavigationDirectionAction.E
    '                'oNavigationDirection.Plus = "1"

    '                'oNavigation.Direction = New QueueAccess.QueueAccessRQNavigationDirection
    '                'oNavigation.Direction = oNavigationDirection

    '                oQueueAccessRQ.Navigation = New QueueAccess.QueueAccessRQNavigation
    '                oQueueAccessRQ.Navigation = oNavigation

    '        End Select


    '        oQueueAccessRQ.TimeStamp = IWS_TIMESTAMP
    '        oQueueAccessRQ.TimeStampSpecified = True

    '        oQueueAccessRQ.ReturnHostCommand = True
    '        oQueueAccessRQ.ReturnHostCommandSpecified = True

    '        '======================================

    '        oQueueAccessRS = New QueueAccess.QueueAccessRS
    '        oQueueAccessRS = oQueueAccessService.QueueAccessRQ(oQueueAccessRQ)


    '        oGeneraPayLoadXML = New GDS_NM_WebServicesSabre.GeneraPayLoadXML
    '        oGeneraPayLoadXML.Serialize(GetType(QueueAccess.QueueAccessRQ), _
    '                                    GetType(QueueAccess.QueueAccessRS), _
    '                                    oQueueAccessRQ, _
    '                                    oQueueAccessRS, _
    '                                    FUNCTION_NAME & "_EASY_" & Nothing)

    '    Catch ex As Exception
    '        If Err.Description.IndexOf("Invalid or Expired binary security") > -1 Then
    '            Err.Raise(-9999, "ContextChangeRS", "Invalid or Expired binary security")
    '        Else
    '            Throw New Exception(ex.ToString)
    '        End If

    '    Finally

    '        FUNCTION_NAME = Nothing
    '        oSecurity1 = Nothing
    '        oUsernameToken = Nothing
    '        oMessageHeader = Nothing
    '        oFromPartyId = Nothing
    '        oFrom = Nothing
    '        oFromPartyIdArr = Nothing
    '        oToPartyId = Nothing
    '        oToPartyIdArr = Nothing
    '        oTo = Nothing
    '        oMessageData = Nothing
    '        oService = Nothing
    '        oQueueAccessRQ = Nothing
    '        oQueueAccessService = Nothing
    '        oGeneraPayLoadXML = Nothing

    '    End Try

    '    Return oQueueAccessRS

    'End Function
    'Public Function _OTA_Cancel(ByVal objOTA_CancelRQ As NuevoMundoUtility.SWS_OTA_Cancel.OTA_CancelRQ) As OTA_Cancel.OTA_CancelRS


    '    Const ID_SABRE_WEB_SERVICE As String = "14"
    '    Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

    '    Dim oSecurity1 As OTA_Cancel.Security1 = Nothing

    '    Dim oUsernameToken As OTA_Cancel.SecurityUsernameToken = Nothing
    '    Dim oMessageHeader As OTA_Cancel.MessageHeader = Nothing

    '    Dim oFromPartyId As OTA_Cancel.PartyId = Nothing
    '    Dim oFrom As OTA_Cancel.From = Nothing
    '    Dim oFromPartyIdArr(0) As OTA_Cancel.PartyId
    '    Dim oToPartyId As OTA_Cancel.PartyId = Nothing
    '    Dim oToPartyIdArr(0) As OTA_Cancel.PartyId
    '    Dim oTo As OTA_Cancel.[To] = Nothing
    '    Dim oMessageData As OTA_Cancel.MessageData = Nothing
    '    Dim oService As OTA_Cancel.Service = Nothing

    '    Dim oOTA_CancelRQ As OTA_Cancel.OTA_CancelRQ = Nothing
    '    Dim oOTA_CancelService As OTA_Cancel.OTA_CancelService = Nothing
    '    Dim oOTA_CancelRS As OTA_Cancel.OTA_CancelRS = Nothing


    '    '========================================================================
    '    Dim oSegment(0) As OTA_Cancel.OTA_CancelRQSegment
    '    '========================================================================

    '    Dim oGeneraPayLoadXML As GDS_NM_WebServicesSabre.GeneraPayLoadXML = Nothing

    '    Try


    '        oMessageHeader = New OTA_Cancel.MessageHeader
    '        oFromPartyId = New OTA_Cancel.PartyId
    '        oFrom = New OTA_Cancel.From
    '        oToPartyId = New OTA_Cancel.PartyId
    '        oTo = New OTA_Cancel.[To]
    '        oService = New OTA_Cancel.Service
    '        oMessageData = New OTA_Cancel.MessageData
    '        oOTA_CancelService = New OTA_Cancel.OTA_CancelService

    '        obj_Cabecera(ID_SABRE_WEB_SERVICE, _
    '                     oMessageHeader, _
    '                     objOTA_CancelRQ.ConversationID, _
    '                     oFromPartyId, _
    '                     oFromPartyIdArr, _
    '                     oFrom, _
    '                     oToPartyId, _
    '                     oToPartyIdArr, _
    '                     oTo, _
    '                     Nothing, _
    '                     oService, _
    '                     oMessageData, _
    '                     oOTA_CancelService)


    '        oUsernameToken = New OTA_Cancel.SecurityUsernameToken
    '        oSecurity1 = New OTA_Cancel.Security1
    '        oOTA_CancelRQ = New OTA_Cancel.OTA_CancelRQ

    '        obj_Security(Nothing, _
    '                     objOTA_CancelRQ.Token, _
    '                     oUsernameToken, _
    '                     oSecurity1, _
    '                     Nothing, _
    '                     Nothing, _
    '                     oOTA_CancelRQ, _
    '                     True, _
    '                     oOTA_CancelService)

    '        '======================================
    '        oSegment(0) = New OTA_Cancel.OTA_CancelRQSegment
    '        oSegment(0).Type = New OTA_Cancel.OTA_CancelRQSegmentType
    '        oSegment(0).Type = OTA_Cancel.OTA_CancelRQSegmentType.air
    '        oSegment(0).TypeSpecified = True

    '        oOTA_CancelRQ.Segment = oSegment


    '        oOTA_CancelRQ.TimeStamp = IWS_TIMESTAMP
    '        oOTA_CancelRQ.TimeStampSpecified = True

    '        oOTA_CancelRQ.ReturnHostCommand = True
    '        oOTA_CancelRQ.ReturnHostCommandSpecified = True

    '        '======================================

    '        oOTA_CancelRS = New OTA_Cancel.OTA_CancelRS
    '        oOTA_CancelRS = oOTA_CancelService.OTA_CancelRQ(oOTA_CancelRQ)


    '        oGeneraPayLoadXML = New GDS_NM_WebServicesSabre.GeneraPayLoadXML
    '        oGeneraPayLoadXML.Serialize(GetType(OTA_Cancel.OTA_CancelRQ), _
    '                                    GetType(OTA_Cancel.OTA_CancelRS), _
    '                                    oOTA_CancelRQ, _
    '                                    oOTA_CancelRS, _
    '                                    FUNCTION_NAME & "_EASY_" & Nothing)

    '    Catch ex As Exception
    '        If Err.Description.IndexOf("Invalid or Expired binary security") > -1 Then
    '            Err.Raise(-9999, "ContextChangeRS", "Invalid or Expired binary security")
    '        Else
    '            Throw New Exception(ex.ToString)
    '        End If

    '    Finally

    '        FUNCTION_NAME = Nothing
    '        oSecurity1 = Nothing
    '        oUsernameToken = Nothing
    '        oMessageHeader = Nothing
    '        oFromPartyId = Nothing
    '        oFrom = Nothing
    '        oFromPartyIdArr = Nothing
    '        oToPartyId = Nothing
    '        oToPartyIdArr = Nothing
    '        oTo = Nothing
    '        oMessageData = Nothing
    '        oService = Nothing
    '        oOTA_CancelRQ = Nothing
    '        oOTA_CancelService = Nothing
    '        oGeneraPayLoadXML = Nothing

    '    End Try

    '    Return oOTA_CancelRS

    'End Function

    'Public Function _Mileage(ByVal objMileageRQ As NuevoMundoUtility.SWS_Mileage.MileageRQ) As Mileage.MileageRS


    '    Const ID_SABRE_WEB_SERVICE As String = "40"
    '    Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

    '    Dim oSecurity As Mileage.Security = Nothing
    '    Dim oPos As Mileage.MileageRQPOS = Nothing
    '    Dim oSource As Mileage.MileageRQPOSSource = Nothing

    '    Dim oUsernameToken As Mileage.SecurityUsernameToken = Nothing
    '    Dim oMessageHeader As Mileage.MessageHeader = Nothing

    '    Dim oFromPartyId As Mileage.PartyId = Nothing
    '    Dim oFrom As Mileage.From = Nothing
    '    Dim oFromPartyIdArr(0) As Mileage.PartyId
    '    Dim oToPartyId As Mileage.PartyId = Nothing
    '    Dim oToPartyIdArr(0) As Mileage.PartyId
    '    Dim oTo As Mileage.[To] = Nothing
    '    Dim oMessageData As Mileage.MessageData = Nothing
    '    Dim oService As Mileage.Service = Nothing

    '    Dim oMileageRQ As Mileage.MileageRQ = Nothing
    '    Dim oMileageService As Mileage.MileageService = Nothing
    '    Dim oMileageRS As Mileage.MileageRS = Nothing

    '    '========================================================================
    '    Dim oOriginLocation As Mileage.MileageRQOriginLocation = Nothing
    '    Dim oDestinationLocation() As Mileage.MileageRQDestinationLocation = Nothing
    '    '========================================================================

    '    Dim oGeneraPayLoadXML As GDS_NM_WebServicesSabre.GeneraPayLoadXML = Nothing

    '    Try


    '        oMessageHeader = New Mileage.MessageHeader
    '        oFromPartyId = New Mileage.PartyId
    '        oFrom = New Mileage.From
    '        oToPartyId = New Mileage.PartyId
    '        oTo = New Mileage.[To]
    '        oService = New Mileage.Service
    '        oMessageData = New Mileage.MessageData
    '        oMileageService = New Mileage.MileageService

    '        obj_Cabecera(ID_SABRE_WEB_SERVICE, _
    '                     oMessageHeader, _
    '                     objMileageRQ.ConversationID, _
    '                     oFromPartyId, _
    '                     oFromPartyIdArr, _
    '                     oFrom, _
    '                     oToPartyId, _
    '                     oToPartyIdArr, _
    '                     oTo, _
    '                     Nothing, _
    '                     oService, _
    '                     oMessageData, _
    '                     oMileageService)


    '        oUsernameToken = New Mileage.SecurityUsernameToken
    '        oSecurity = New Mileage.Security
    '        oMileageRQ = New Mileage.MileageRQ

    '        oSource = New Mileage.MileageRQPOSSource
    '        oPos = New Mileage.MileageRQPOS

    '        obj_Security(Nothing, _
    '                     objMileageRQ.Token, _
    '                     oUsernameToken, _
    '                     oSecurity, _
    '                     oSource, _
    '                     oPos, _
    '                     oMileageRQ, _
    '                     True, _
    '                     oMileageService)

    '        '======================================
    '        oOriginLocation = New Mileage.MileageRQOriginLocation
    '        oOriginLocation.LocationCode = objMileageRQ.CiudadOrigen

    '        ReDim oDestinationLocation(0)
    '        oDestinationLocation(0) = New Mileage.MileageRQDestinationLocation
    '        oDestinationLocation(0).LocationCode = objMileageRQ.CiudadDestino


    '        oMileageRQ.OriginLocation = oOriginLocation
    '        oMileageRQ.DestinationLocation = oDestinationLocation
    '        oMileageRQ.DepartureDateTime = Nothing
    '        oMileageRQ.TPA_Extensions = Nothing

    '        oMileageRQ.TimeStamp = IWS_TIMESTAMP

    '        '======================================

    '        oMileageRS = New Mileage.MileageRS
    '        oMileageRS = oMileageService.MileageRQ(oMileageRQ)


    '        oGeneraPayLoadXML = New GDS_NM_WebServicesSabre.GeneraPayLoadXML
    '        oGeneraPayLoadXML.Serialize(GetType(Mileage.MileageRQ), _
    '                                    GetType(Mileage.MileageRS), _
    '                                    oMileageRQ, _
    '                                    oMileageRS, _
    '                                    FUNCTION_NAME & "_EASY_" & Nothing)

    '    Catch ex As Exception
    '        If Err.Description.IndexOf("Invalid or Expired binary security") > -1 Then
    '            Err.Raise(-9999, "MileageRS", "Invalid or Expired binary security")
    '        Else
    '            Throw New Exception(ex.ToString)
    '        End If

    '    Finally

    '        FUNCTION_NAME = Nothing
    '        oSecurity = Nothing
    '        oUsernameToken = Nothing
    '        oMessageHeader = Nothing
    '        oFromPartyId = Nothing
    '        oFrom = Nothing
    '        oFromPartyIdArr = Nothing
    '        oToPartyId = Nothing
    '        oToPartyIdArr = Nothing
    '        oTo = Nothing
    '        oMessageData = Nothing
    '        oService = Nothing
    '        oMileageRQ = Nothing
    '        oMileageService = Nothing
    '        oGeneraPayLoadXML = Nothing
    '        oSource = Nothing
    '        oPos = Nothing

    '    End Try

    '    Return oMileageRS

    'End Function
    'Public Function _TicketCoupon(ByVal objTicketCouponRQ As NuevoMundoUtility.SWS_TicketCoupon.TicketCouponRQ) As TicketCoupon.eTicketCouponRS


    '    Const ID_SABRE_WEB_SERVICE As String = "23"
    '    Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

    '    Dim oSecurity1 As TicketCoupon.Security1 = Nothing
    '    'Dim oPos As Mileage.MileageRQPOS = Nothing
    '    'Dim oSource As Mileage.MileageRQPOSSource = Nothing

    '    Dim oUsernameToken As TicketCoupon.SecurityUsernameToken = Nothing
    '    Dim oMessageHeader As TicketCoupon.MessageHeader = Nothing

    '    Dim oFromPartyId As TicketCoupon.PartyId = Nothing
    '    Dim oFrom As TicketCoupon.From = Nothing
    '    Dim oFromPartyIdArr(0) As TicketCoupon.PartyId
    '    Dim oToPartyId As TicketCoupon.PartyId = Nothing
    '    Dim oToPartyIdArr(0) As TicketCoupon.PartyId
    '    Dim oTo As TicketCoupon.[To] = Nothing
    '    Dim oMessageData As TicketCoupon.MessageData = Nothing
    '    Dim oService As TicketCoupon.Service = Nothing

    '    Dim oTicketCouponRQ As TicketCoupon.eTicketCouponRQ = Nothing
    '    Dim oTicketCouponService As TicketCoupon.eTicketCouponService = Nothing
    '    Dim oTicketCouponRS As TicketCoupon.eTicketCouponRS = Nothing

    '    '========================================================================
    '    Dim oTicketing As TicketCoupon.eTicketCouponRQTicketing = Nothing

    '    '========================================================================

    '    Dim oGeneraPayLoadXML As GDS_NM_WebServicesSabre.GeneraPayLoadXML = Nothing

    '    Try


    '        oMessageHeader = New TicketCoupon.MessageHeader
    '        oFromPartyId = New TicketCoupon.PartyId
    '        oFrom = New TicketCoupon.From
    '        oToPartyId = New TicketCoupon.PartyId
    '        oTo = New TicketCoupon.[To]
    '        oService = New TicketCoupon.Service
    '        oMessageData = New TicketCoupon.MessageData
    '        oTicketCouponService = New TicketCoupon.eTicketCouponService

    '        obj_Cabecera(ID_SABRE_WEB_SERVICE, _
    '                     oMessageHeader, _
    '                     objTicketCouponRQ.ConversationID, _
    '                     oFromPartyId, _
    '                     oFromPartyIdArr, _
    '                     oFrom, _
    '                     oToPartyId, _
    '                     oToPartyIdArr, _
    '                     oTo, _
    '                     Nothing, _
    '                     oService, _
    '                     oMessageData, _
    '                     oTicketCouponService)


    '        oUsernameToken = New TicketCoupon.SecurityUsernameToken
    '        oSecurity1 = New TicketCoupon.Security1
    '        oTicketCouponRQ = New TicketCoupon.eTicketCouponRQ


    '        obj_Security(Nothing, _
    '                     objTicketCouponRQ.Token, _
    '                     oUsernameToken, _
    '                     oSecurity1, _
    '                     Nothing, _
    '                     Nothing, _
    '                     oTicketCouponRQ, _
    '                     True, _
    '                     oTicketCouponService)

    '        '======================================
    '        oTicketing = New TicketCoupon.eTicketCouponRQTicketing
    '        oTicketing.eTicketNumber = objTicketCouponRQ.Ticket

    '        oTicketCouponRQ.Ticketing = oTicketing
    '        oTicketCouponRQ.ReturnHostCommand = True
    '        oTicketCouponRQ.ReturnHostCommandSpecified = True
    '        oTicketCouponRQ.TimeStamp = IWS_TIMESTAMP

    '        '======================================

    '        oTicketCouponRS = New TicketCoupon.eTicketCouponRS
    '        oTicketCouponRS = oTicketCouponService.eTicketCouponRQ(oTicketCouponRQ)


    '        oGeneraPayLoadXML = New GDS_NM_WebServicesSabre.GeneraPayLoadXML
    '        oGeneraPayLoadXML.Serialize(GetType(TicketCoupon.eTicketCouponRQ), _
    '                                    GetType(TicketCoupon.eTicketCouponRS), _
    '                                    oTicketCouponRQ, _
    '                                    oTicketCouponRS, _
    '                                    FUNCTION_NAME & "_EASY_" & Nothing)

    '    Catch ex As Exception
    '        If Err.Description.IndexOf("Invalid or Expired binary security") > -1 Then
    '            Err.Raise(-9999, "MileageRS", "Invalid or Expired binary security")
    '        Else
    '            Throw New Exception(ex.ToString)
    '        End If

    '    Finally

    '        FUNCTION_NAME = Nothing
    '        oSecurity1 = Nothing
    '        oUsernameToken = Nothing
    '        oMessageHeader = Nothing
    '        oFromPartyId = Nothing
    '        oFrom = Nothing
    '        oFromPartyIdArr = Nothing
    '        oToPartyId = Nothing
    '        oToPartyIdArr = Nothing
    '        oTo = Nothing
    '        oMessageData = Nothing
    '        oService = Nothing
    '        oTicketCouponRQ = Nothing
    '        oTicketCouponService = Nothing
    '        oGeneraPayLoadXML = Nothing

    '    End Try

    '    Return oTicketCouponRS

    'End Function
    Public Function _AddRemark(ByVal listRemark As List(Of String), _
                               ByVal strTipo As String, _
                               ByVal strCodigoSeguimiento As String, _
                               ByVal intGDS As Integer, _
                               ByVal intFirmaGDS As Integer, _
                               ByVal intFirmaDB As Integer, _
                               ByVal objSession As classSession) As AddRemark.AddRemarkRS

        Const ID_SABRE_WEB_SERVICE As String = "15"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        '
        Dim oSecurity1 As AddRemark.Security1 = Nothing
        Dim oUsernameToken As AddRemark.SecurityUsernameToken = Nothing
        Dim oMessageHeader As AddRemark.MessageHeader = Nothing
        Dim oFromPartyId As AddRemark.PartyId = Nothing
        Dim oFrom As AddRemark.From = Nothing
        Dim oFromPartyIdArr(0) As AddRemark.PartyId
        Dim oToPartyId As New AddRemark.PartyId
        Dim oToPartyIdArr(0) As AddRemark.PartyId
        Dim oTo As AddRemark.[To] = Nothing
        Dim oMessageData As AddRemark.MessageData = Nothing
        Dim oService As AddRemark.Service = Nothing
        '
        Dim oAddRemarkRQ As AddRemark.AddRemarkRQ = Nothing
        Dim oAddRemarkService As AddRemark.AddRemarkService = Nothing
        Dim oAddRemarkRS As AddRemark.AddRemarkRS = Nothing

        Dim RemarkInfoRemark() As AddRemark.AddRemarkRQRemarkInfoRemark = Nothing
        Dim RemarkInfo As AddRemark.AddRemarkRQRemarkInfo = Nothing
        Dim RemarkInfoRemarkType As AddRemark.AddRemarkRQRemarkInfoRemarkType = Nothing



        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try

            oMessageHeader = New AddRemark.MessageHeader
            oFromPartyId = New AddRemark.PartyId
            oFrom = New AddRemark.From
            oToPartyId = New AddRemark.PartyId
            oTo = New AddRemark.[To]
            oService = New AddRemark.Service
            oMessageData = New AddRemark.MessageData
            oAddRemarkService = New AddRemark.AddRemarkService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oAddRemarkService)


            oUsernameToken = New AddRemark.SecurityUsernameToken
            oSecurity1 = New AddRemark.Security1
            oAddRemarkRQ = New AddRemark.AddRemarkRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oAddRemarkRQ, _
                         True, _
                         oAddRemarkService, _
                         intFirmaGDS)

            '======================================
            '

            If listRemark IsNot Nothing Then
                For i As Integer = 0 To listRemark.Count - 1

                    If RemarkInfoRemark Is Nothing Then
                        ReDim RemarkInfoRemark(0)
                    Else
                        ReDim Preserve RemarkInfoRemark(RemarkInfoRemark.Length)
                    End If

                    RemarkInfoRemark(RemarkInfoRemark.Length - 1) = New AddRemark.AddRemarkRQRemarkInfoRemark
                    RemarkInfoRemark(RemarkInfoRemark.Length - 1).Text = listRemark.Item(i).ToString
                    RemarkInfoRemark(RemarkInfoRemark.Length - 1).Type = New AddRemark.AddRemarkRQRemarkInfoRemarkType
                    Select Case strTipo
                        Case "AC"
                            RemarkInfoRemark(RemarkInfoRemark.Length - 1).Type = AddRemark.AddRemarkRQRemarkInfoRemarkType.AlphaCoded
                        Case "CA"
                            RemarkInfoRemark(RemarkInfoRemark.Length - 1).Type = AddRemark.AddRemarkRQRemarkInfoRemarkType.ClientAddress
                        Case "C"
                            RemarkInfoRemark(RemarkInfoRemark.Length - 1).Type = AddRemark.AddRemarkRQRemarkInfoRemarkType.Corporate
                        Case "DA"
                            RemarkInfoRemark(RemarkInfoRemark.Length - 1).Type = AddRemark.AddRemarkRQRemarkInfoRemarkType.DeliveryAddress
                        Case "G"
                            RemarkInfoRemark(RemarkInfoRemark.Length - 1).Type = AddRemark.AddRemarkRQRemarkInfoRemarkType.General
                        Case "GN"
                            RemarkInfoRemark(RemarkInfoRemark.Length - 1).Type = AddRemark.AddRemarkRQRemarkInfoRemarkType.GroupName
                        Case "HI"
                            RemarkInfoRemark(RemarkInfoRemark.Length - 1).Type = AddRemark.AddRemarkRQRemarkInfoRemarkType.Hidden
                        Case "H"
                            RemarkInfoRemark(RemarkInfoRemark.Length - 1).Type = AddRemark.AddRemarkRQRemarkInfoRemarkType.Historical
                        Case "I"
                            RemarkInfoRemark(RemarkInfoRemark.Length - 1).Type = AddRemark.AddRemarkRQRemarkInfoRemarkType.Invoice
                        Case "IT"
                            RemarkInfoRemark(RemarkInfoRemark.Length - 1).Type = AddRemark.AddRemarkRQRemarkInfoRemarkType.Itinerary
                    End Select

                Next

                RemarkInfo = New AddRemark.AddRemarkRQRemarkInfo
                RemarkInfo.Remark = RemarkInfoRemark

                oAddRemarkRQ.RemarkInfo = New AddRemark.AddRemarkRQRemarkInfo
                oAddRemarkRQ.RemarkInfo = RemarkInfo

            End If

            oAddRemarkRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oAddRemarkRQ.TimeStampSpecified = True

            oAddRemarkRQ.ReturnHostCommand = True
            oAddRemarkRQ.ReturnHostCommandSpecified = True

            '
            oAddRemarkRS = oAddRemarkService.AddRemarkRQ(oAddRemarkRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(AddRemark.AddRemarkRQ), _
                                        GetType(AddRemark.AddRemarkRS), _
                                        oAddRemarkRQ, _
                                        oAddRemarkRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch Ex As Exception

            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_SabreCommand" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            listRemark = Nothing
            strTipo = Nothing
            strCodigoSeguimiento = Nothing
            objSession = Nothing
            intGDS = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oAddRemarkService = Nothing
            oAddRemarkRQ = Nothing
        End Try

        Return oAddRemarkRS

    End Function
    Public Function _ModifyRemark(ByVal strTipo As String, _
                                  ByVal intLinea As String, _
                                  ByVal strRemark As String, _
                                  ByVal strCodigoSeguimiento As String, _
                                  ByVal intGDS As Integer, _
                                  ByVal intFirmaGDS As Integer, _
                                  ByVal intFirmaDB As Integer, _
                                  ByVal objSession As classSession) As ModifyRemark.ModifyRemarkRS

        Const ID_SABRE_WEB_SERVICE As String = "53"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        '
        Dim oSecurity1 As ModifyRemark.Security1 = Nothing
        Dim oUsernameToken As ModifyRemark.SecurityUsernameToken = Nothing
        Dim oMessageHeader As ModifyRemark.MessageHeader = Nothing
        Dim oFromPartyId As ModifyRemark.PartyId = Nothing
        Dim oFrom As ModifyRemark.From = Nothing
        Dim oFromPartyIdArr(0) As ModifyRemark.PartyId
        Dim oToPartyId As New ModifyRemark.PartyId
        Dim oToPartyIdArr(0) As ModifyRemark.PartyId
        Dim oTo As ModifyRemark.[To] = Nothing
        Dim oMessageData As ModifyRemark.MessageData = Nothing
        Dim oService As ModifyRemark.Service = Nothing
        '
        Dim oModifyRemarkRQ As ModifyRemark.ModifyRemarkRQ = Nothing
        Dim oModifyRemarkService As ModifyRemark.ModifyRemarkService = Nothing
        Dim oModifyRemarkRS As ModifyRemark.ModifyRemarkRS = Nothing
        ''''''''''''''''
        Dim RemarkInfo As ModifyRemark.ModifyRemarkRQRemarkInfo = Nothing
        Dim Remark As ModifyRemark.ModifyRemarkRQRemarkInfoRemark = Nothing
        Dim RemarkType As ModifyRemark.ModifyRemarkRQRemarkInfoRemarkType = Nothing


        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try

            oMessageHeader = New ModifyRemark.MessageHeader
            oFromPartyId = New ModifyRemark.PartyId
            oFrom = New ModifyRemark.From
            oToPartyId = New ModifyRemark.PartyId
            oTo = New ModifyRemark.[To]
            oService = New ModifyRemark.Service
            oMessageData = New ModifyRemark.MessageData
            oModifyRemarkService = New ModifyRemark.ModifyRemarkService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oModifyRemarkService)


            oUsernameToken = New ModifyRemark.SecurityUsernameToken
            oSecurity1 = New ModifyRemark.Security1
            oModifyRemarkRQ = New ModifyRemark.ModifyRemarkRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oModifyRemarkRQ, _
                         True, _
                         oModifyRemarkService, _
                         intFirmaGDS)

            '======================================
            '

            Remark = New ModifyRemark.ModifyRemarkRQRemarkInfoRemark
            'Remark.Code = "H"
            Remark.TypeSpecified = True
            Remark.Number = intLinea
            Remark.Text = strRemark

            RemarkType = New ModifyRemark.ModifyRemarkRQRemarkInfoRemarkType
            RemarkType = ModifyRemark.ModifyRemarkRQRemarkInfoRemarkType.Historical
            Remark.Type = RemarkType

            RemarkInfo = New ModifyRemark.ModifyRemarkRQRemarkInfo
            ReDim RemarkInfo.Remark(0)
            RemarkInfo.Remark(RemarkInfo.Remark.Length - 1) = New ModifyRemark.ModifyRemarkRQRemarkInfoRemark
            RemarkInfo.Remark(RemarkInfo.Remark.Length - 1) = Remark

            oModifyRemarkRQ.RemarkInfo = New ModifyRemark.ModifyRemarkRQRemarkInfo
            oModifyRemarkRQ.RemarkInfo = RemarkInfo

            oModifyRemarkRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oModifyRemarkRQ.TimeStampSpecified = True

            oModifyRemarkRQ.ReturnHostCommand = True
            oModifyRemarkRQ.ReturnHostCommandSpecified = True
            oModifyRemarkRQ.Version = "2.1.0"
            '
            oModifyRemarkRS = oModifyRemarkService.ModifyRemarkRQ(oModifyRemarkRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(ModifyRemark.ModifyRemarkRQ), _
                                        GetType(ModifyRemark.ModifyRemarkRS), _
                                        oModifyRemarkRQ, _
                                        oModifyRemarkRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch Ex As Exception

            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_SabreCommand" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            intLinea = Nothing
            strTipo = Nothing
            strRemark = Nothing
            strCodigoSeguimiento = Nothing
            objSession = Nothing
            intGDS = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oModifyRemarkService = Nothing
            oModifyRemarkRQ = Nothing
        End Try

        Return oModifyRemarkRS

    End Function
    Public Function _SpecialService(ByVal listPasajero As List(Of classPasajeros), _
                                    ByVal strTipo As String, _
                                    ByVal bolAmerican As Boolean, _
                                    ByVal strCodigoSeguimiento As String, _
                                    ByVal intGDS As Integer, _
                                    ByVal intFirmaGDS As Integer, _
                                    ByVal intFirmaDB As Integer, _
                                    ByVal objSession As classSession) As SpecialService.SpecialServiceRS

        Const ID_SABRE_WEB_SERVICE As String = "30"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        '
        Dim oSecurity1 As SpecialService.Security1 = Nothing
        Dim oUsernameToken As SpecialService.SecurityUsernameToken = Nothing
        Dim oMessageHeader As SpecialService.MessageHeader = Nothing
        Dim oFromPartyId As SpecialService.PartyId = Nothing
        Dim oFrom As SpecialService.From = Nothing
        Dim oFromPartyIdArr(0) As SpecialService.PartyId
        Dim oToPartyId As New SpecialService.PartyId
        Dim oToPartyIdArr(0) As SpecialService.PartyId
        Dim oTo As SpecialService.[To] = Nothing
        Dim oMessageData As SpecialService.MessageData = Nothing
        Dim oService As SpecialService.Service = Nothing
        '
        Dim oSpecialServiceRQ As SpecialService.SpecialServiceRQ = Nothing
        Dim oSpecialServiceService As SpecialService.SpecialServiceService = Nothing
        Dim oSpecialServiceRS As SpecialService.SpecialServiceRS = Nothing

        Dim SpecialServiceInfo As SpecialService.SpecialServiceRQSpecialServiceInfo = Nothing
        Dim Service() As SpecialService.SpecialServiceRQSpecialServiceInfoService = Nothing
        Dim ServicePersonName As SpecialService.SpecialServiceRQSpecialServiceInfoServicePersonName = Nothing

        Dim SecureFlight() As SpecialService.SpecialServiceRQSpecialServiceInfoSecureFlight = Nothing
        Dim SecureFlightPersonName As SpecialService.SpecialServiceRQSpecialServiceInfoSecureFlightPersonName = Nothing
        Dim SecureFlightVendorPrefs As SpecialService.SpecialServiceRQSpecialServiceInfoSecureFlightVendorPrefs = Nothing
        Dim SecureFlightVendorPrefsAirline As SpecialService.SpecialServiceRQSpecialServiceInfoSecureFlightVendorPrefsAirline = Nothing


        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Dim FNac As Date

        Try

            oMessageHeader = New SpecialService.MessageHeader
            oFromPartyId = New SpecialService.PartyId
            oFrom = New SpecialService.From
            oToPartyId = New SpecialService.PartyId
            oTo = New SpecialService.[To]
            oService = New SpecialService.Service
            oMessageData = New SpecialService.MessageData
            oSpecialServiceService = New SpecialService.SpecialServiceService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oSpecialServiceService)


            oUsernameToken = New SpecialService.SecurityUsernameToken
            oSecurity1 = New SpecialService.Security1
            oSpecialServiceRQ = New SpecialService.SpecialServiceRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oSpecialServiceRQ, _
                         True, _
                         oSpecialServiceService, _
                         intFirmaGDS)

            '======================================
            '

            If Not String.IsNullOrEmpty(strTipo) Then

                If listPasajero IsNot Nothing Then
                    For i As Integer = 0 To listPasajero.Count - 1
                        If listPasajero.Item(i).Marca Then

                            Select Case strTipo
                                Case Constantes.IWS_SSR_FOID

                                    If Service Is Nothing Then
                                        ReDim Service(0)
                                    Else
                                        ReDim Preserve Service(Service.Length)
                                    End If

                                    Service(Service.Length - 1) = New SpecialService.SpecialServiceRQSpecialServiceInfoService
                                    ServicePersonName = New SpecialService.SpecialServiceRQSpecialServiceInfoServicePersonName
                                    ServicePersonName.NameNumber = listPasajero.Item(i).NumeroPasajero
                                    Service(Service.Length - 1).PersonName = ServicePersonName
                                    Service(Service.Length - 1).SSR_Code = strTipo

                                    For x As Integer = 0 To listPasajero.Item(i).Documento.Count - 1

                                        If Not listPasajero.Item(i).Documento.Item(x).Tipo.Equals(Constantes.Id_RUC) Then
                                            Select Case listPasajero.Item(i).Documento.Item(x).Tipo
                                                Case Constantes.Id_DNI
                                                    Service(Service.Length - 1).Text = Constantes.Id_DNI_FOID & listPasajero.Item(i).Documento.Item(x).Numero.ToString
                                                Case Constantes.Id_CE
                                                    Service(Service.Length - 1).Text = Constantes.Id_CE_FOID & listPasajero.Item(i).Documento.Item(x).Numero.ToString
                                                Case Constantes.Id_PASS
                                                    Service(Service.Length - 1).Text = Constantes.Id_PASS_FOID & listPasajero.Item(i).Documento.Item(x).Numero.ToString
                                            End Select

                                        End If
                                    Next

                                Case Constantes.IWS_SSR_DOCS
                                    ' bolAmerican 
                                    ' YYYY-MM-DD
                                    If SecureFlight Is Nothing Then
                                        ReDim SecureFlight(0)
                                    Else
                                        ReDim Preserve SecureFlight(SecureFlight.Length)
                                    End If

                                    SecureFlight(SecureFlight.Length) = New SpecialService.SpecialServiceRQSpecialServiceInfoSecureFlight

                                    SecureFlightPersonName = New SpecialService.SpecialServiceRQSpecialServiceInfoSecureFlightPersonName

                                    FNac = CDate(listPasajero.Item(i).SSR.FechaNacimiento)
                                    SecureFlightPersonName.DateOfBirth = FNac.ToString(Constantes.IWS_DATE_FORMAT_FILE2)

                                    SecureFlightPersonName.Gender = New SpecialService.SpecialServiceRQSpecialServiceInfoSecureFlightPersonNameGender

                                    Select Case listPasajero.Item(i).SSR.Genero
                                        Case "F"
                                            If listPasajero.Item(i).Infante Then
                                                SecureFlightPersonName.Gender = SpecialService.SpecialServiceRQSpecialServiceInfoSecureFlightPersonNameGender.FI
                                                SecureFlightPersonName.NameNumber = listPasajero.Item(i).AdultoAsociado
                                            Else
                                                SecureFlightPersonName.Gender = SpecialService.SpecialServiceRQSpecialServiceInfoSecureFlightPersonNameGender.F
                                                SecureFlightPersonName.NameNumber = listPasajero.Item(i).NumeroPasajero
                                            End If
                                        Case "M"
                                            If listPasajero.Item(i).Infante Then
                                                SecureFlightPersonName.Gender = SpecialService.SpecialServiceRQSpecialServiceInfoSecureFlightPersonNameGender.MI
                                                SecureFlightPersonName.NameNumber = listPasajero.Item(i).AdultoAsociado
                                            Else
                                                SecureFlightPersonName.Gender = SpecialService.SpecialServiceRQSpecialServiceInfoSecureFlightPersonNameGender.M
                                                SecureFlightPersonName.NameNumber = listPasajero.Item(i).NumeroPasajero
                                            End If
                                    End Select
                                    SecureFlightPersonName.GenderSpecified = True

                                    SecureFlightPersonName.GivenName = listPasajero.Item(i).NombrePasajero
                                    SecureFlightPersonName.Surname = listPasajero.Item(i).ApellidoPaterno


                                    SecureFlight(SecureFlight.Length).PersonName = SecureFlightPersonName
                                    SecureFlight(SecureFlight.Length).SSR_Code = Constantes.IWS_SSR_DOCS


                                    SecureFlight(SecureFlight.Length).SegmentNumber = "A"

                                    SecureFlightVendorPrefs = New SpecialService.SpecialServiceRQSpecialServiceInfoSecureFlightVendorPrefs
                                    SecureFlightVendorPrefsAirline = New SpecialService.SpecialServiceRQSpecialServiceInfoSecureFlightVendorPrefsAirline
                                    SecureFlightVendorPrefsAirline.Hosted = bolAmerican
                                    SecureFlightVendorPrefsAirline.HostedSpecified = True
                                    SecureFlightVendorPrefs.Airline = SecureFlightVendorPrefsAirline

                                    SecureFlight(SecureFlight.Length).VendorPrefs = SecureFlightVendorPrefs

                            End Select

                        End If
                    Next

                    SpecialServiceInfo = New SpecialService.SpecialServiceRQSpecialServiceInfo
                    SpecialServiceInfo.Service = Service
                    SpecialServiceInfo.SecureFlight = SecureFlight

                End If

                oSpecialServiceRQ.SpecialServiceInfo = New SpecialService.SpecialServiceRQSpecialServiceInfo
                oSpecialServiceRQ.SpecialServiceInfo = SpecialServiceInfo

            End If


            oSpecialServiceRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oSpecialServiceRQ.TimeStampSpecified = True

            oSpecialServiceRQ.ReturnHostCommand = True
            oSpecialServiceRQ.ReturnHostCommandSpecified = True

            '
            oSpecialServiceRS = oSpecialServiceService.SpecialServiceRQ(oSpecialServiceRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(SpecialService.SpecialServiceRQ), _
                                        GetType(SpecialService.SpecialServiceRS), _
                                        oSpecialServiceRQ, _
                                        oSpecialServiceRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch Ex As Exception

            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_SabreCommand" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            listPasajero = Nothing
            strTipo = Nothing
            strCodigoSeguimiento = Nothing
            objSession = Nothing
            intFirmaGDS = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oSpecialServiceService = Nothing
            oSpecialServiceRQ = Nothing
            FNac = Nothing
        End Try

        Return oSpecialServiceRS

    End Function
    Public Function _DeleteSpecialService(ByVal lstDeleteSpecialService As List(Of classDeleteSpecialService), _
                                          ByVal strCodigoSeguimiento As String, _
                                          ByVal intGDS As Integer, _
                                          ByVal intFirmaGDS As Integer, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal objSession As classSession) As DeleteSpecialService.DeleteSpecialServiceRS

        Const ID_SABRE_WEB_SERVICE As String = "31"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        '
        Dim oSecurity1 As DeleteSpecialService.Security1 = Nothing
        Dim oUsernameToken As DeleteSpecialService.SecurityUsernameToken = Nothing
        Dim oMessageHeader As DeleteSpecialService.MessageHeader = Nothing
        Dim oFromPartyId As DeleteSpecialService.PartyId = Nothing
        Dim oFrom As DeleteSpecialService.From = Nothing
        Dim oFromPartyIdArr(0) As DeleteSpecialService.PartyId
        Dim oToPartyId As New DeleteSpecialService.PartyId
        Dim oToPartyIdArr(0) As DeleteSpecialService.PartyId
        Dim oTo As DeleteSpecialService.[To] = Nothing
        Dim oMessageData As DeleteSpecialService.MessageData = Nothing
        Dim oService As DeleteSpecialService.Service = Nothing
        '
        Dim oDeleteSpecialServiceRQ As DeleteSpecialService.DeleteSpecialServiceRQ = Nothing
        Dim oDeleteSpecialServiceService As DeleteSpecialService.DeleteSpecialServiceService = Nothing
        Dim oDeleteSpecialServiceRS As DeleteSpecialService.DeleteSpecialServiceRS = Nothing


        Dim Service() As DeleteSpecialService.DeleteSpecialServiceRQService = Nothing


        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Dim FNac As Date

        Try

            oMessageHeader = New DeleteSpecialService.MessageHeader
            oFromPartyId = New DeleteSpecialService.PartyId
            oFrom = New DeleteSpecialService.From
            oToPartyId = New DeleteSpecialService.PartyId
            oTo = New DeleteSpecialService.[To]
            oService = New DeleteSpecialService.Service
            oMessageData = New DeleteSpecialService.MessageData
            oDeleteSpecialServiceService = New DeleteSpecialService.DeleteSpecialServiceService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oDeleteSpecialServiceService)


            oUsernameToken = New DeleteSpecialService.SecurityUsernameToken
            oSecurity1 = New DeleteSpecialService.Security1
            oDeleteSpecialServiceRQ = New DeleteSpecialService.DeleteSpecialServiceRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oDeleteSpecialServiceRQ, _
                         True, _
                         oDeleteSpecialServiceService, _
                         intFirmaGDS)

            '======================================
            If lstDeleteSpecialService IsNot Nothing Then
                For i As Integer = 0 To lstDeleteSpecialService.Count - 1
                    If Service Is Nothing Then
                        ReDim Service(0)
                    Else
                        ReDim Preserve Service(Service.Length)
                    End If

                    Service(Service.Length - 1) = New DeleteSpecialService.DeleteSpecialServiceRQService
                    Service(Service.Length - 1).RPH = lstDeleteSpecialService.Item(i).Item
                Next

                oDeleteSpecialServiceRQ.Service = Service
            End If

            '======================================


            oDeleteSpecialServiceRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oDeleteSpecialServiceRQ.TimeStampSpecified = True

            oDeleteSpecialServiceRQ.ReturnHostCommand = True
            oDeleteSpecialServiceRQ.ReturnHostCommandSpecified = True

            '
            oDeleteSpecialServiceRS = oDeleteSpecialServiceService.DeleteSpecialServiceRQ(oDeleteSpecialServiceRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(DeleteSpecialService.DeleteSpecialServiceRQ), _
                                        GetType(DeleteSpecialService.DeleteSpecialServiceRS), _
                                        oDeleteSpecialServiceRQ, _
                                        oDeleteSpecialServiceRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch Ex As Exception

            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_SabreCommand" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            strCodigoSeguimiento = Nothing
            objSession = Nothing
            intGDS = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oDeleteSpecialServiceService = Nothing
            oDeleteSpecialServiceRQ = Nothing
            FNac = Nothing
        End Try

        Return oDeleteSpecialServiceRS

    End Function
    Public Function _AirTicket230(ByVal strDK As String, _
                                  ByVal strTipoEmision As String, _
                                  ByVal strNumeroEMD As String, _
                                  ByVal objFormaPago As classFormaPago, _
                                  ByVal strCodigoSeguimiento As String, _
                                  ByVal intGDS As Integer, _
                                  ByVal intFirmaGDS As Integer, _
                                  ByVal intFirmaDB As Integer, _
                                  ByVal objSession As classSession) As AirTicketLLS230.AirTicketRS

        Const ID_SABRE_WEB_SERVICE As String = "46"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        '
        Dim oSecurity1 As AirTicketLLS230.Security1 = Nothing
        Dim oUsernameToken As AirTicketLLS230.SecurityUsernameToken = Nothing
        Dim oMessageHeader As AirTicketLLS230.MessageHeader = Nothing
        Dim oFromPartyId As AirTicketLLS230.PartyId = Nothing
        Dim oFrom As AirTicketLLS230.From = Nothing
        Dim oFromPartyIdArr(0) As AirTicketLLS230.PartyId
        Dim oToPartyId As New AirTicketLLS230.PartyId
        Dim oToPartyIdArr(0) As AirTicketLLS230.PartyId
        Dim oTo As AirTicketLLS230.[To] = Nothing
        Dim oMessageData As AirTicketLLS230.MessageData = Nothing
        Dim oService As AirTicketLLS230.Service = Nothing
        '

        Dim oAirTicketRQ As AirTicketLLS230.AirTicketRQ = Nothing
        Dim oAirTicketService As AirTicketLLS230.AirTicketService = Nothing
        Dim oAirTicketRS As AirTicketLLS230.AirTicketRS = Nothing


        Dim OptionalQualifiers As AirTicketLLS230.AirTicketRQOptionalQualifiers = Nothing

        Dim FlightQualifiers As AirTicketLLS230.AirTicketRQOptionalQualifiersFlightQualifiers = Nothing
        Dim VendorPrefs As AirTicketLLS230.AirTicketRQOptionalQualifiersFlightQualifiersVendorPrefs = Nothing
        Dim Airline As AirTicketLLS230.AirTicketRQOptionalQualifiersFlightQualifiersVendorPrefsAirline = Nothing

        Dim MiscQualifiers As AirTicketLLS230.AirTicketRQOptionalQualifiersMiscQualifiers = Nothing
        Dim Ticket As AirTicketLLS230.AirTicketRQOptionalQualifiersMiscQualifiersTicket = Nothing
        Dim AirExtras As AirTicketLLS230.AirTicketRQOptionalQualifiersMiscQualifiersAirExtras() = Nothing

        Dim PricingQualifiers As AirTicketLLS230.AirTicketRQOptionalQualifiersPricingQualifiers = Nothing
        Dim NameSelect As AirTicketLLS230.AirTicketRQOptionalQualifiersPricingQualifiersNameSelect = Nothing

        Dim ItineraryOptions As AirTicketLLS230.AirTicketRQOptionalQualifiersPricingQualifiersItineraryOptions = Nothing
        Dim OptionsSegment As AirTicketLLS230.AirTicketRQOptionalQualifiersPricingQualifiersItineraryOptionsSegment = Nothing

        Dim FOP_Qualifiers As AirTicketLLS230.AirTicketRQOptionalQualifiersFOP_Qualifiers = Nothing
        Dim BasicFOP As AirTicketLLS230.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOP = Nothing
        Dim CC_Info As AirTicketLLS230.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_Info = Nothing
        Dim PaymentCard As AirTicketLLS230.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_InfoPaymentCard = Nothing



        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try

            oMessageHeader = New AirTicketLLS230.MessageHeader
            oFromPartyId = New AirTicketLLS230.PartyId
            oFrom = New AirTicketLLS230.From
            oToPartyId = New AirTicketLLS230.PartyId
            oTo = New AirTicketLLS230.[To]
            oService = New AirTicketLLS230.Service
            oMessageData = New AirTicketLLS230.MessageData
            oAirTicketService = New AirTicketLLS230.AirTicketService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oAirTicketService)


            oUsernameToken = New AirTicketLLS230.SecurityUsernameToken
            oSecurity1 = New AirTicketLLS230.Security1
            oAirTicketRQ = New AirTicketLLS230.AirTicketRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oAirTicketRQ, _
                         True, _
                         oAirTicketService, _
                         intFirmaGDS)

            oAirTicketRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oAirTicketRQ.TimeStampSpecified = True

            oAirTicketRQ.ReturnHostCommand = True
            oAirTicketRQ.ReturnHostCommandSpecified = True

            oAirTicketRQ.NumResponses = 1
            '

            Ticket = New AirTicketLLS230.AirTicketRQOptionalQualifiersMiscQualifiersTicket
            Ticket.Type = strTipoEmision
            MiscQualifiers = New AirTicketLLS230.AirTicketRQOptionalQualifiersMiscQualifiers
            MiscQualifiers.Ticket = Ticket

            If Not strTipoEmision.Equals("ETR") Then

                If Not String.IsNullOrEmpty(strNumeroEMD) Then

                    ReDim AirExtras(0)
                    AirExtras(AirExtras.Length - 1) = New AirTicketLLS230.AirTicketRQOptionalQualifiersMiscQualifiersAirExtras

                    AirExtras(AirExtras.Length - 1).Number = strNumeroEMD.Split("-")(0)
                    If strNumeroEMD.Split("-").Length > 1 Then
                        AirExtras(AirExtras.Length - 1).EndNumber = strNumeroEMD.Split("-")(1)
                    End If

                    MiscQualifiers.AirExtras = AirExtras

                End If

            End If


            '****************
            If objFormaPago IsNot Nothing Then

                BasicFOP = New AirTicketLLS230.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOP
                If objFormaPago.Tipo.Equals("CA") Then
                    BasicFOP.Type = "CA"
                Else

                    If objFormaPago.Tarjeta IsNot Nothing Then

                        CC_Info = New AirTicketLLS230.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_Info
                        PaymentCard = New AirTicketLLS230.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_InfoPaymentCard

                        PaymentCard.Code = objFormaPago.Tarjeta(0).CodigoTarjera
                        PaymentCard.Number = objFormaPago.Tarjeta(0).NumTarjeta
                        PaymentCard.ExpireDate = objFormaPago.Tarjeta(0).FechaVencimiento '2012-12
                        If Not String.IsNullOrEmpty(objFormaPago.Tarjeta(0).CodAutorizacion) Then
                            PaymentCard.ManualApprovalCode = objFormaPago.Tarjeta(0).CodAutorizacion
                            'Else
                            '    PaymentCard.ManualApprovalCode = "111111"
                        End If

                        CC_Info.PaymentCard = PaymentCard

                        BasicFOP.CC_Info = New AirTicketLLS230.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_Info
                        BasicFOP.CC_Info = CC_Info

                    End If

                End If


                FOP_Qualifiers = New AirTicketLLS230.AirTicketRQOptionalQualifiersFOP_Qualifiers
                FOP_Qualifiers.BasicFOP = New AirTicketLLS230.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOP
                FOP_Qualifiers.BasicFOP = BasicFOP

            End If





            OptionalQualifiers = New AirTicketLLS230.AirTicketRQOptionalQualifiers
            OptionalQualifiers.FOP_Qualifiers = New AirTicketLLS230.AirTicketRQOptionalQualifiersFOP_Qualifiers
            OptionalQualifiers.FOP_Qualifiers = FOP_Qualifiers
            OptionalQualifiers.FlightQualifiers = New AirTicketLLS230.AirTicketRQOptionalQualifiersFlightQualifiers
            OptionalQualifiers.FlightQualifiers = FlightQualifiers
            OptionalQualifiers.PricingQualifiers = New AirTicketLLS230.AirTicketRQOptionalQualifiersPricingQualifiers
            OptionalQualifiers.PricingQualifiers = PricingQualifiers
            OptionalQualifiers.MiscQualifiers = New AirTicketLLS230.AirTicketRQOptionalQualifiersMiscQualifiers
            OptionalQualifiers.MiscQualifiers = MiscQualifiers


            oAirTicketRQ.OptionalQualifiers = New AirTicketLLS230.AirTicketRQOptionalQualifiers
            oAirTicketRQ.OptionalQualifiers = OptionalQualifiers
            '
            oAirTicketRS = oAirTicketService.AirTicketRQ(oAirTicketRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(AirTicketLLS230.AirTicketRQ), _
                                        GetType(AirTicketLLS230.AirTicketRS), _
                                        oAirTicketRQ, _
                                        oAirTicketRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch Ex As Exception

            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_AirTicket230" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            strDK = Nothing
            strTipoEmision = Nothing
            strNumeroEMD = Nothing
            objFormaPago = Nothing
            strCodigoSeguimiento = Nothing
            intGDS = Nothing
            objSession = Nothing
            intFirmaGDS = Nothing

            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oAirTicketService = Nothing
            oAirTicketRQ = Nothing
        End Try

        Return oAirTicketRS

    End Function
    Public Function _AirTicketNEW(ByVal strDK As String, _
                               ByVal strTipoEmision As String, _
                               ByVal strNumeroEMD As String, _
                               ByVal strNumeroPQ As String, _
                               ByVal bolReissue As Boolean, _
                               ByVal objEasyOnLine As classEasyOnLine, _
                               ByVal strCodigoSeguimiento As String, _
                               ByVal intGDS As Integer, _
                               ByVal intFirmaGDS As Integer, _
                               ByVal intFirmaDB As Integer, _
                               ByVal objSession As classSession) As AirTicketLLS270.AirTicketRS

        Const ID_SABRE_WEB_SERVICE As String = "54"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        '
        Dim oSecurity1 As AirTicketLLS270.Security1 = Nothing
        Dim oUsernameToken As AirTicketLLS270.SecurityUsernameToken = Nothing
        Dim oMessageHeader As AirTicketLLS270.MessageHeader = Nothing
        Dim oFromPartyId As AirTicketLLS270.PartyId = Nothing
        Dim oFrom As AirTicketLLS270.From = Nothing
        Dim oFromPartyIdArr(0) As AirTicketLLS270.PartyId
        Dim oToPartyId As New AirTicketLLS270.PartyId
        Dim oToPartyIdArr(0) As AirTicketLLS270.PartyId
        Dim oTo As AirTicketLLS270.[To] = Nothing
        Dim oMessageData As AirTicketLLS270.MessageData = Nothing
        Dim oService As AirTicketLLS270.Service = Nothing
        '

        Dim oAirTicketRQ As AirTicketLLS270.AirTicketRQ = Nothing
        Dim oAirTicketService As AirTicketLLS270.AirTicketService = Nothing
        Dim oAirTicketRS As AirTicketLLS270.AirTicketRS = Nothing


        Dim OptionalQualifiers As AirTicketLLS270.AirTicketRQOptionalQualifiers = Nothing

        Dim FlightQualifiers As AirTicketLLS270.AirTicketRQOptionalQualifiersFlightQualifiers = Nothing
        Dim VendorPrefs As AirTicketLLS270.AirTicketRQOptionalQualifiersFlightQualifiersVendorPrefs = Nothing
        Dim Airline As AirTicketLLS270.AirTicketRQOptionalQualifiersFlightQualifiersVendorPrefsAirline = Nothing

        Dim MiscQualifiers As AirTicketLLS270.AirTicketRQOptionalQualifiersMiscQualifiers = Nothing
        Dim Commission As AirTicketLLS270.AirTicketRQOptionalQualifiersMiscQualifiersCommission = Nothing
        Dim Ticket As AirTicketLLS270.AirTicketRQOptionalQualifiersMiscQualifiersTicket = Nothing
        Dim AirExtras As AirTicketLLS270.AirTicketRQOptionalQualifiersMiscQualifiersAirExtras() = Nothing

        Dim PricingQualifiers As AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiers = Nothing
        Dim NameSelect As AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersNameSelect() = Nothing

        Dim ItineraryOptions As AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersItineraryOptions = Nothing
        Dim PriceQuote As AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersPriceQuote() = Nothing
        Dim PriceQuoteRecord As AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersPriceQuoteRecord() = Nothing
        Dim SegmentSelect As AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersItineraryOptionsSegmentSelect() = Nothing

        Dim FOP_Qualifiers As AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_Qualifiers = Nothing
        Dim BasicFOP As AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOP = Nothing
        Dim CC_Info As AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_Info = Nothing
        Dim PaymentCard As AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_InfoPaymentCard = Nothing


        Dim MultipleCC_FOP As AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOP = Nothing

        Dim FOPCC_One As AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_One = Nothing
        Dim OneCC_Info As AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_OneCC_Info = Nothing
        Dim OneCC_InfoPaymentCard As AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_OneCC_InfoPaymentCard = Nothing

        Dim FOPCC_Two As AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_Two = Nothing
        Dim TwoCC_Info As AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_TwoCC_Info = Nothing
        Dim TwoCC_InfoPaymentCard As AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_TwoCC_InfoPaymentCard = Nothing

        Dim Fare As AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPFare = Nothing



        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try

            oMessageHeader = New AirTicketLLS270.MessageHeader
            oFromPartyId = New AirTicketLLS270.PartyId
            oFrom = New AirTicketLLS270.From
            oToPartyId = New AirTicketLLS270.PartyId
            oTo = New AirTicketLLS270.[To]
            oService = New AirTicketLLS270.Service
            oMessageData = New AirTicketLLS270.MessageData
            oAirTicketService = New AirTicketLLS270.AirTicketService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oAirTicketService)


            oUsernameToken = New AirTicketLLS270.SecurityUsernameToken
            oSecurity1 = New AirTicketLLS270.Security1
            oAirTicketRQ = New AirTicketLLS270.AirTicketRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oAirTicketRQ, _
                         True, _
                         oAirTicketService, _
                         intFirmaGDS)

            oAirTicketRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oAirTicketRQ.TimeStampSpecified = True

            oAirTicketRQ.ReturnHostCommand = True
            oAirTicketRQ.ReturnHostCommandSpecified = True

            oAirTicketRQ.NumResponses = 1
            '

            Ticket = New AirTicketLLS270.AirTicketRQOptionalQualifiersMiscQualifiersTicket
            Ticket.Type = strTipoEmision
            MiscQualifiers = New AirTicketLLS270.AirTicketRQOptionalQualifiersMiscQualifiers
            MiscQualifiers.Ticket = Ticket

            Commission = New AirTicketLLS270.AirTicketRQOptionalQualifiersMiscQualifiersCommission
            Commission.Percent = 1
            Commission.PercentSpecified = True

            MiscQualifiers.Commission = New AirTicketLLS270.AirTicketRQOptionalQualifiersMiscQualifiersCommission
            MiscQualifiers.Commission = Commission

            If strTipoEmision.Equals("ETR") Then

                If Not String.IsNullOrEmpty(strNumeroPQ) Then
                    For x As Integer = 0 To strNumeroPQ.Split("/").Length - 1

                        If PriceQuoteRecord IsNot Nothing Then
                            ReDim PriceQuoteRecord(0)
                        Else
                            ReDim Preserve PriceQuoteRecord(PriceQuoteRecord.Length)
                        End If

                        If strNumeroPQ.Split("/").Length = 1 Then
                            PriceQuoteRecord(PriceQuoteRecord.Length - 1) = New AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersPriceQuoteRecord

                            If strNumeroPQ.Split("/")(x).Contains("-") Then
                                PriceQuoteRecord(PriceQuoteRecord.Length - 1).Number = strNumeroPQ.Split("/")(x).Split("-")(0)
                                PriceQuoteRecord(PriceQuoteRecord.Length - 1).EndNumber = strNumeroPQ.Split("/")(x).Split("-")(1)
                            Else
                                PriceQuoteRecord(PriceQuoteRecord.Length - 1).Number = strNumeroPQ.Split("/")(x)
                            End If

                            If bolReissue Then
                                PriceQuoteRecord(PriceQuoteRecord.Length - 1).Reissue = bolReissue
                                PriceQuoteRecord(PriceQuoteRecord.Length - 1).ReissueSpecified = bolReissue
                            End If

                        End If

                    Next


                    If PriceQuote Is Nothing Then
                        ReDim PriceQuote(0)
                        PriceQuote(PriceQuote.Length - 1) = New AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersPriceQuote
                        PriceQuote(PriceQuote.Length - 1).Record = PriceQuoteRecord
                    End If
                    PricingQualifiers.PriceQuote = PriceQuote


                Else

                    PricingQualifiers = New AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiers

                    For y As Integer = 0 To objEasyOnLine.Pasajeros.Count - 1
                        If NameSelect Is Nothing Then
                            ReDim Preserve NameSelect(0)
                        Else
                            ReDim Preserve NameSelect(NameSelect.Length)
                        End If

                        'NameSelect.NameNumber = 
                        ' Dim NameSelect As AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersNameSelect() = Nothing

                        NameSelect(NameSelect.Length - 1) = New AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersNameSelect
                        NameSelect(NameSelect.Length - 1).NameNumber = objEasyOnLine.Pasajeros(y).NumeroPasajero 'NameSelect
                        'If NameSelect.Length > 1 Then
                        '    NameSelect(NameSelect.Length - 1).NameNumber = objEasyOnLine.Pasajeros(y).NumeroPasajero
                        'End If
                    Next
                    PricingQualifiers.NameSelect = NameSelect

                    ItineraryOptions = New AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersItineraryOptions

                    For z As Integer = 0 To objEasyOnLine.Segmentos.Count - 1
                        If SegmentSelect Is Nothing Then
                            ReDim Preserve SegmentSelect(0)
                        Else
                            ReDim Preserve SegmentSelect(SegmentSelect.Length)
                        End If

                        SegmentSelect(SegmentSelect.Length - 1) = New AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersItineraryOptionsSegmentSelect
                        SegmentSelect(SegmentSelect.Length - 1).Number = objEasyOnLine.Segmentos(z).IdLinea
                        'If SegmentSelect.Length > 1 Then
                        '    SegmentSelect(SegmentSelect.Length - 1).Number = objEasyOnLine.Segmentos(z).IdLinea
                        'End If
                    Next

                    ItineraryOptions.SegmentSelect = SegmentSelect


                    PricingQualifiers.ItineraryOptions = New AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersItineraryOptions
                    PricingQualifiers.ItineraryOptions = ItineraryOptions

                    'NameSelect = New AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersNameSelect
                    'NameSelect.NameNumber = "1.1"
                    'NameSelect.EndNameNumber = "2.1"

                    'PricingQualifiers = New AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiers
                    'ReDim PricingQualifiers.NameSelect(0)

                    'PricingQualifiers.NameSelect(PricingQualifiers.NameSelect.Length - 1) = New AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersNameSelect
                    'PricingQualifiers.NameSelect(PricingQualifiers.NameSelect.Length - 1) = NameSelect

                    '***
                    'SegmentSelect = New AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersItineraryOptionsSegmentSelect
                    'SegmentSelect.Number = 1
                    'SegmentSelect.EndNumber = 2

                    'ItineraryOptions = New AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersItineraryOptions
                    'ReDim ItineraryOptions.SegmentSelect(0)
                    'ItineraryOptions.SegmentSelect(ItineraryOptions.SegmentSelect.Length - 1) = New AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersItineraryOptionsSegmentSelect
                    'ItineraryOptions.SegmentSelect(ItineraryOptions.SegmentSelect.Length - 1) = SegmentSelect

                    'PricingQualifiers.ItineraryOptions = New AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiersItineraryOptions
                    'PricingQualifiers.ItineraryOptions = ItineraryOptions

                    '**************
                    Airline = New AirTicketLLS270.AirTicketRQOptionalQualifiersFlightQualifiersVendorPrefsAirline
                    Airline.Code = objEasyOnLine.Reserva.Tarifa.LineaValidadora

                    VendorPrefs = New AirTicketLLS270.AirTicketRQOptionalQualifiersFlightQualifiersVendorPrefs
                    VendorPrefs.Airline = New AirTicketLLS270.AirTicketRQOptionalQualifiersFlightQualifiersVendorPrefsAirline
                    VendorPrefs.Airline = Airline

                    FlightQualifiers = New AirTicketLLS270.AirTicketRQOptionalQualifiersFlightQualifiers
                    FlightQualifiers.VendorPrefs = New AirTicketLLS270.AirTicketRQOptionalQualifiersFlightQualifiersVendorPrefs
                    FlightQualifiers.VendorPrefs = VendorPrefs

                End If

            Else

                If Not String.IsNullOrEmpty(strNumeroEMD) Then

                    ReDim AirExtras(0)
                    AirExtras(AirExtras.Length - 1) = New AirTicketLLS270.AirTicketRQOptionalQualifiersMiscQualifiersAirExtras

                    AirExtras(AirExtras.Length - 1).Number = strNumeroEMD.Split("-")(0)
                    If strNumeroEMD.Split("-").Length > 1 Then
                        AirExtras(AirExtras.Length - 1).EndNumber = strNumeroEMD.Split("-")(1)
                    End If

                    MiscQualifiers.AirExtras = AirExtras

                End If

            End If


            '****************
            If objEasyOnLine IsNot Nothing Then
                If objEasyOnLine.FormaPago IsNot Nothing Then

                    If objEasyOnLine.FormaPago IsNot Nothing Then

                        BasicFOP = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOP
                        If objEasyOnLine.FormaPago.Tipo.Equals("CA") Then
                            BasicFOP.Type = "CA"
                        Else

                            If objEasyOnLine.FormaPago.Tarjeta IsNot Nothing Then
                                If objEasyOnLine.FormaPago.Tarjeta.Count = 1 Then
                                    CC_Info = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_Info
                                    PaymentCard = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_InfoPaymentCard

                                    PaymentCard.Code = objEasyOnLine.FormaPago.Tarjeta(0).CodigoTarjera
                                    PaymentCard.Number = objEasyOnLine.FormaPago.Tarjeta(0).NumTarjeta
                                    PaymentCard.ExpireDate = objEasyOnLine.FormaPago.Tarjeta(0).FechaVencimiento '2012-12
                                    'If Not String.IsNullOrEmpty(objEasyOnLine.FormaPago.Tarjeta(0).CodAutorizacion) Then
                                    '    PaymentCard.ManualApprovalCode = objEasyOnLine.FormaPago.Tarjeta(0).CodAutorizacion
                                    '    'Else
                                    '    '    PaymentCard.ManualApprovalCode = "111111"
                                    'End If
                                    If String.IsNullOrEmpty(objEasyOnLine.FormaPago.Tarjeta(0).CodAutorizacion) Then
                                        OneCC_InfoPaymentCard.ManualApprovalCode = "111111"
                                    End If

                                    CC_Info.PaymentCard = PaymentCard

                                    BasicFOP.CC_Info = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOPCC_Info
                                    BasicFOP.CC_Info = CC_Info



                                Else

                                    BasicFOP = Nothing
                                    MultipleCC_FOP = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOP
                                    FOPCC_One = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_One

                                    OneCC_Info = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_OneCC_Info
                                    OneCC_InfoPaymentCard = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_OneCC_InfoPaymentCard
                                    OneCC_InfoPaymentCard.Code = objEasyOnLine.FormaPago.Tarjeta(0).CodigoTarjera
                                    OneCC_InfoPaymentCard.Number = objEasyOnLine.FormaPago.Tarjeta(0).NumTarjeta
                                    OneCC_InfoPaymentCard.ExpireDate = objEasyOnLine.FormaPago.Tarjeta(0).FechaVencimiento
                                    'If Not String.IsNullOrEmpty(objFormaPago.Tarjeta(0).CodAutorizacion) Then
                                    '    OneCC_InfoPaymentCard.ManualApprovalCode = objFormaPago.Tarjeta(0).CodAutorizacion
                                    'End If
                                    If String.IsNullOrEmpty(objEasyOnLine.FormaPago.Tarjeta(0).CodAutorizacion) Then
                                        OneCC_InfoPaymentCard.ManualApprovalCode = "111111"
                                    End If
                                    OneCC_Info.PaymentCard = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_OneCC_InfoPaymentCard
                                    OneCC_Info.PaymentCard = OneCC_InfoPaymentCard

                                    FOPCC_One.CC_Info = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_OneCC_Info
                                    FOPCC_One.CC_Info = OneCC_Info

                                    MultipleCC_FOP.CC_One = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_One
                                    MultipleCC_FOP.CC_One = FOPCC_One
                                    '---------------------------------------------------------------------------

                                    FOPCC_Two = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_Two

                                    TwoCC_Info = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_TwoCC_Info
                                    TwoCC_InfoPaymentCard = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_TwoCC_InfoPaymentCard
                                    TwoCC_InfoPaymentCard.Code = objEasyOnLine.FormaPago.Tarjeta(1).CodigoTarjera
                                    TwoCC_InfoPaymentCard.Number = objEasyOnLine.FormaPago.Tarjeta(1).NumTarjeta
                                    TwoCC_InfoPaymentCard.ExpireDate = objEasyOnLine.FormaPago.Tarjeta(1).FechaVencimiento
                                    'If Not String.IsNullOrEmpty(objFormaPago.Tarjeta(1).CodAutorizacion) Then
                                    '    TwoCC_InfoPaymentCard.ManualApprovalCode = objFormaPago.Tarjeta(1).CodAutorizacion
                                    'End If
                                    If String.IsNullOrEmpty(objEasyOnLine.FormaPago.Tarjeta(1).CodAutorizacion) Then
                                        TwoCC_InfoPaymentCard.ManualApprovalCode = "222222"
                                    End If
                                    TwoCC_Info.PaymentCard = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_TwoCC_InfoPaymentCard
                                    TwoCC_Info.PaymentCard = TwoCC_InfoPaymentCard

                                    FOPCC_Two.CC_Info = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_TwoCC_Info
                                    FOPCC_Two.CC_Info = TwoCC_Info

                                    MultipleCC_FOP.CC_Two = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPCC_Two
                                    MultipleCC_FOP.CC_Two = FOPCC_Two

                                End If

                            End If

                        End If


                        FOP_Qualifiers = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_Qualifiers
                        If BasicFOP IsNot Nothing Then
                            FOP_Qualifiers.BasicFOP = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersBasicFOP
                            FOP_Qualifiers.BasicFOP = BasicFOP
                        Else


                            Fare = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPFare
                            Fare.Amount = objEasyOnLine.Monto
                            MultipleCC_FOP.Fare = Fare

                            FOP_Qualifiers.MultipleCC_FOP = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOP
                            FOP_Qualifiers.MultipleCC_FOP = MultipleCC_FOP

                            ' FOP_Qualifiers.MultipleCC_FOP.Fare = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_QualifiersMultipleCC_FOPFare

                        End If

                    End If
                End If
            End If

            OptionalQualifiers = New AirTicketLLS270.AirTicketRQOptionalQualifiers
            OptionalQualifiers.FOP_Qualifiers = New AirTicketLLS270.AirTicketRQOptionalQualifiersFOP_Qualifiers
            OptionalQualifiers.FOP_Qualifiers = FOP_Qualifiers
            OptionalQualifiers.FlightQualifiers = New AirTicketLLS270.AirTicketRQOptionalQualifiersFlightQualifiers
            OptionalQualifiers.FlightQualifiers = FlightQualifiers
            OptionalQualifiers.PricingQualifiers = New AirTicketLLS270.AirTicketRQOptionalQualifiersPricingQualifiers
            OptionalQualifiers.PricingQualifiers = PricingQualifiers
            OptionalQualifiers.MiscQualifiers = New AirTicketLLS270.AirTicketRQOptionalQualifiersMiscQualifiers
            OptionalQualifiers.MiscQualifiers = MiscQualifiers


            oAirTicketRQ.OptionalQualifiers = New AirTicketLLS270.AirTicketRQOptionalQualifiers
            oAirTicketRQ.OptionalQualifiers = OptionalQualifiers
            '
            oAirTicketRS = oAirTicketService.AirTicketRQ(oAirTicketRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(AirTicketLLS270.AirTicketRQ), _
                                        GetType(AirTicketLLS270.AirTicketRS), _
                                        oAirTicketRQ, _
                                        oAirTicketRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch Ex As Exception

            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_AirTicketNEW" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            strDK = Nothing
            strTipoEmision = Nothing
            strNumeroEMD = Nothing
            objEasyOnLine = Nothing
            strCodigoSeguimiento = Nothing
            intGDS = Nothing
            objSession = Nothing
            intFirmaGDS = Nothing

            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oAirTicketService = Nothing
            oAirTicketRQ = Nothing
        End Try

        Return oAirTicketRS

    End Function
    Public Function _eTicketCoupon(ByVal strBOLETO As String, _
                                   ByVal strCodigoSeguimiento As String, _
                                   ByVal intGDS As Integer, _
                                   ByVal intFirmaGDS As Integer, _
                                   ByVal intFirmaDB As Integer, _
                                   ByVal objSession As classSession) As TicketCoupon.eTicketCouponRS

        Const ID_SABRE_WEB_SERVICE As String = "23"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        '
        Dim oSecurity1 As TicketCoupon.Security1 = Nothing
        Dim oUsernameToken As TicketCoupon.SecurityUsernameToken = Nothing
        Dim oMessageHeader As TicketCoupon.MessageHeader = Nothing
        Dim oFromPartyId As TicketCoupon.PartyId = Nothing
        Dim oFrom As TicketCoupon.From = Nothing
        Dim oFromPartyIdArr(0) As TicketCoupon.PartyId
        Dim oToPartyId As New TicketCoupon.PartyId
        Dim oToPartyIdArr(0) As TicketCoupon.PartyId
        Dim oTo As TicketCoupon.[To] = Nothing
        Dim oMessageData As TicketCoupon.MessageData = Nothing
        Dim oService As TicketCoupon.Service = Nothing
        '

        Dim oTicketCouponRQ As TicketCoupon.eTicketCouponRQ = Nothing
        Dim oTicketCouponService As TicketCoupon.eTicketCouponService = Nothing
        Dim oTicketCouponRS As TicketCoupon.eTicketCouponRS = Nothing

        Dim Ticketing As TicketCoupon.eTicketCouponRQTicketing = Nothing

        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try

            oMessageHeader = New TicketCoupon.MessageHeader
            oFromPartyId = New TicketCoupon.PartyId
            oFrom = New TicketCoupon.From
            oToPartyId = New TicketCoupon.PartyId
            oTo = New TicketCoupon.[To]
            oService = New TicketCoupon.Service
            oMessageData = New TicketCoupon.MessageData
            oTicketCouponService = New TicketCoupon.eTicketCouponService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oTicketCouponService)


            oUsernameToken = New TicketCoupon.SecurityUsernameToken
            oSecurity1 = New TicketCoupon.Security1
            oTicketCouponRQ = New TicketCoupon.eTicketCouponRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oTicketCouponRQ, _
                         True, _
                         oTicketCouponService, _
                         intFirmaGDS)

            Ticketing = New TicketCoupon.eTicketCouponRQTicketing
            Ticketing.eTicketNumber = strBOLETO
            oTicketCouponRQ.Ticketing = New TicketCoupon.eTicketCouponRQTicketing
            oTicketCouponRQ.Ticketing = Ticketing

            oTicketCouponRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oTicketCouponRQ.TimeStampSpecified = True

            oTicketCouponRQ.ReturnHostCommand = True
            oTicketCouponRQ.ReturnHostCommandSpecified = True

            '
            oTicketCouponRS = oTicketCouponService.eTicketCouponRQ(oTicketCouponRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(TicketCoupon.eTicketCouponRQ), _
                                        GetType(TicketCoupon.eTicketCouponRS), _
                                        oTicketCouponRQ, _
                                        oTicketCouponRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch Ex As Exception

            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_eTicketCouponRS" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            strCodigoSeguimiento = Nothing
            objSession = Nothing
            intGDS = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oTicketCouponService = Nothing
            oTicketCouponRQ = Nothing
        End Try

        Return oTicketCouponRS

    End Function
    Public Function _EMD_Display(ByVal strBOLETO As String, _
                                 ByVal strCodigoSeguimiento As String, _
                                 ByVal intGDS As Integer, _
                                 ByVal intFirmaGDS As Integer, _
                                 ByVal intFirmaDB As Integer, _
                                 ByVal objSession As classSession) As EMD_Display.EMD_DisplayRS

        Const ID_SABRE_WEB_SERVICE As String = "48"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        '
        Dim oSecurity1 As EMD_Display.Security1 = Nothing
        Dim oUsernameToken As EMD_Display.SecurityUsernameToken = Nothing
        Dim oMessageHeader As EMD_Display.MessageHeader = Nothing
        Dim oFromPartyId As EMD_Display.PartyId = Nothing
        Dim oFrom As EMD_Display.From = Nothing
        Dim oFromPartyIdArr(0) As EMD_Display.PartyId
        Dim oToPartyId As New EMD_Display.PartyId
        Dim oToPartyIdArr(0) As EMD_Display.PartyId
        Dim oTo As EMD_Display.[To] = Nothing
        Dim oMessageData As EMD_Display.MessageData = Nothing
        Dim oService As EMD_Display.Service = Nothing
        '

        Dim oEMD_DisplayRQ As EMD_Display.EMD_DisplayRQ = Nothing
        Dim oEMD_DisplayService As EMD_Display.EMD_DisplayService = Nothing
        Dim oEMD_DisplayRS As EMD_Display.EMD_DisplayRS = Nothing


        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try

            oMessageHeader = New EMD_Display.MessageHeader
            oFromPartyId = New EMD_Display.PartyId
            oFrom = New EMD_Display.From
            oToPartyId = New EMD_Display.PartyId
            oTo = New EMD_Display.[To]
            oService = New EMD_Display.Service
            oMessageData = New EMD_Display.MessageData
            oEMD_DisplayService = New EMD_Display.EMD_DisplayService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oEMD_DisplayService)


            oUsernameToken = New EMD_Display.SecurityUsernameToken
            oSecurity1 = New EMD_Display.Security1
            oEMD_DisplayRQ = New EMD_Display.EMD_DisplayRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oEMD_DisplayRQ, _
                         True, _
                         oEMD_DisplayService, _
                         intFirmaGDS)


            oEMD_DisplayRQ.EMD_Number = strBOLETO

            oEMD_DisplayRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oEMD_DisplayRQ.TimeStampSpecified = True
            oEMD_DisplayRQ.ReturnHostCommand = True
            oEMD_DisplayRQ.ReturnHostCommandSpecified = True
            oEMD_DisplayRQ.Version = "2.0.1"

            '
            oEMD_DisplayRS = oEMD_DisplayService.EMD_DisplayRQ(oEMD_DisplayRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(EMD_Display.EMD_DisplayRQ), _
                                        GetType(EMD_Display.EMD_DisplayRS), _
                                        oEMD_DisplayRQ, _
                                        oEMD_DisplayRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch Ex As Exception

            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_EMD_Display" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            strCodigoSeguimiento = Nothing
            objSession = Nothing
            intGDS = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oEMD_DisplayService = Nothing
            oEMD_DisplayRQ = Nothing
        End Try

        Return oEMD_DisplayRS

    End Function
    Public Function _OTA_AirRules(ByVal strCiudadOrigen As String, _
                                  ByVal strCiudadDestino As String, _
                                  ByVal strFechaSalida As String, _
                                  ByVal strFareBasis As String, _
                                  ByVal strTicketDesignator As String, _
                                  ByVal strAerolinea As String, _
                                  ByVal strAccount As String, _
                                  ByVal strCategory As String, _
                                  ByVal strRPH As String, _
                                  ByVal strCodigoSeguimiento As String, _
                                  ByVal intGDS As Integer, _
                                  ByVal intFirmaGDS As Integer, _
                                  ByVal intFirmaDB As Integer, _
                                  ByVal objSession As classSession) As OTA_AirRules.OTA_AirRulesRS

        Const ID_SABRE_WEB_SERVICE As String = "05"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        '
        Dim oSecurity1 As OTA_AirRules.Security1 = Nothing
        Dim oUsernameToken As OTA_AirRules.SecurityUsernameToken = Nothing
        Dim oMessageHeader As OTA_AirRules.MessageHeader = Nothing
        Dim oFromPartyId As OTA_AirRules.PartyId = Nothing
        Dim oFrom As OTA_AirRules.From = Nothing
        Dim oFromPartyIdArr(0) As OTA_AirRules.PartyId
        Dim oToPartyId As New OTA_AirRules.PartyId
        Dim oToPartyIdArr(0) As OTA_AirRules.PartyId
        Dim oTo As OTA_AirRules.[To] = Nothing
        Dim oMessageData As OTA_AirRules.MessageData = Nothing
        Dim oService As OTA_AirRules.Service = Nothing
        '

        Dim oAirRulesRQ As OTA_AirRules.OTA_AirRulesRQ = Nothing
        Dim oAirRulesService As OTA_AirRules.OTA_AirRulesService = Nothing
        Dim oAirRulesRS As OTA_AirRules.OTA_AirRulesRS = Nothing

        Dim oOptionalQualifiers As OTA_AirRules.OTA_AirRulesRQOptionalQualifiers = Nothing
        Dim oPricingQualifiers As OTA_AirRules.OTA_AirRulesRQOptionalQualifiersPricingQualifiers = Nothing
        Dim oAccount As OTA_AirRules.OTA_AirRulesRQOptionalQualifiersPricingQualifiersAccount = Nothing

        Dim oOriginDestinationInformation As OTA_AirRules.OTA_AirRulesRQOriginDestinationInformation = Nothing
        Dim oFlightSegment As OTA_AirRules.OTA_AirRulesRQOriginDestinationInformationFlightSegment = Nothing
        Dim oDestinationLocation As OTA_AirRules.OTA_AirRulesRQOriginDestinationInformationFlightSegmentDestinationLocation = Nothing
        Dim oMarketingCarrier As OTA_AirRules.OTA_AirRulesRQOriginDestinationInformationFlightSegmentMarketingCarrier = Nothing
        Dim oOriginLocation As OTA_AirRules.OTA_AirRulesRQOriginDestinationInformationFlightSegmentOriginLocation = Nothing

        Dim oRuleReqInfo As OTA_AirRules.OTA_AirRulesRQRuleReqInfo = Nothing
        Dim oFareBasis As OTA_AirRules.OTA_AirRulesRQRuleReqInfoFareBasis = Nothing



        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try

            oMessageHeader = New OTA_AirRules.MessageHeader
            oFromPartyId = New OTA_AirRules.PartyId
            oFrom = New OTA_AirRules.From
            oToPartyId = New OTA_AirRules.PartyId
            oTo = New OTA_AirRules.[To]
            oService = New OTA_AirRules.Service
            oMessageData = New OTA_AirRules.MessageData
            oAirRulesService = New OTA_AirRules.OTA_AirRulesService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oAirRulesService)


            oUsernameToken = New OTA_AirRules.SecurityUsernameToken
            oSecurity1 = New OTA_AirRules.Security1
            oAirRulesRQ = New OTA_AirRules.OTA_AirRulesRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oAirRulesRQ, _
                         True, _
                         oAirRulesService, _
                         intFirmaGDS)



            oAirRulesRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oAirRulesRQ.TimeStampSpecified = True

            oAirRulesRQ.ReturnHostCommand = True
            oAirRulesRQ.ReturnHostCommandSpecified = True


            oRuleReqInfo = New OTA_AirRules.OTA_AirRulesRQRuleReqInfo
            If Not String.IsNullOrEmpty(strRPH) Then
                oRuleReqInfo.RPH = strRPH
            Else

                If Not String.IsNullOrEmpty(strAccount) Then
                    oAccount = New OTA_AirRules.OTA_AirRulesRQOptionalQualifiersPricingQualifiersAccount
                    oAccount.Code = strAccount

                    oPricingQualifiers = New OTA_AirRules.OTA_AirRulesRQOptionalQualifiersPricingQualifiers
                    oPricingQualifiers.Account = New OTA_AirRules.OTA_AirRulesRQOptionalQualifiersPricingQualifiersAccount
                    oPricingQualifiers.Account = oAccount

                    oOptionalQualifiers = New OTA_AirRules.OTA_AirRulesRQOptionalQualifiers
                    oOptionalQualifiers.PricingQualifiers = New OTA_AirRules.OTA_AirRulesRQOptionalQualifiersPricingQualifiers
                    oOptionalQualifiers.PricingQualifiers = oPricingQualifiers

                    oAirRulesRQ.OptionalQualifiers = New OTA_AirRules.OTA_AirRulesRQOptionalQualifiers
                    oAirRulesRQ.OptionalQualifiers = oOptionalQualifiers
                End If
                '===========

                oOriginLocation = New OTA_AirRules.OTA_AirRulesRQOriginDestinationInformationFlightSegmentOriginLocation
                oOriginLocation.LocationCode = strCiudadOrigen

                oDestinationLocation = New OTA_AirRules.OTA_AirRulesRQOriginDestinationInformationFlightSegmentDestinationLocation
                oDestinationLocation.LocationCode = strCiudadDestino

                oMarketingCarrier = New OTA_AirRules.OTA_AirRulesRQOriginDestinationInformationFlightSegmentMarketingCarrier
                oMarketingCarrier.Code = strAerolinea

                oFlightSegment = New OTA_AirRules.OTA_AirRulesRQOriginDestinationInformationFlightSegment
                Dim FechaAux As String = Format(CDate(strFechaSalida), "MM-dd")
                oFlightSegment.DepartureDateTime = FechaAux

                oFlightSegment.DestinationLocation = New OTA_AirRules.OTA_AirRulesRQOriginDestinationInformationFlightSegmentDestinationLocation
                oFlightSegment.DestinationLocation = oDestinationLocation

                oFlightSegment.OriginLocation = New OTA_AirRules.OTA_AirRulesRQOriginDestinationInformationFlightSegmentOriginLocation
                oFlightSegment.OriginLocation = oOriginLocation

                oFlightSegment.MarketingCarrier = New OTA_AirRules.OTA_AirRulesRQOriginDestinationInformationFlightSegmentMarketingCarrier
                oFlightSegment.MarketingCarrier = oMarketingCarrier

                oOriginDestinationInformation = New OTA_AirRules.OTA_AirRulesRQOriginDestinationInformation
                oOriginDestinationInformation.FlightSegment = New OTA_AirRules.OTA_AirRulesRQOriginDestinationInformationFlightSegment
                oOriginDestinationInformation.FlightSegment = oFlightSegment

                oAirRulesRQ.OriginDestinationInformation = New OTA_AirRules.OTA_AirRulesRQOriginDestinationInformation
                oAirRulesRQ.OriginDestinationInformation = oOriginDestinationInformation

                '===========

                oFareBasis = New OTA_AirRules.OTA_AirRulesRQRuleReqInfoFareBasis
                oFareBasis.Code = strFareBasis
                If Not String.IsNullOrEmpty(strTicketDesignator) Then
                    oFareBasis.TicketDesignator = strTicketDesignator
                End If



                oRuleReqInfo.FareBasis = New OTA_AirRules.OTA_AirRulesRQRuleReqInfoFareBasis
                oRuleReqInfo.FareBasis = oFareBasis
            End If


            'Category
            If Not String.IsNullOrEmpty(strCategory) Then
                '6-7-8-12-16 
                oRuleReqInfo.Category = strCategory.Split(Constantes.Guion)
            End If


            oAirRulesRQ.RuleReqInfo = New OTA_AirRules.OTA_AirRulesRQRuleReqInfo
            oAirRulesRQ.RuleReqInfo = oRuleReqInfo

            '

            oAirRulesRS = oAirRulesService.OTA_AirRulesRQ(oAirRulesRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(OTA_AirRules.OTA_AirRulesRQ), _
                                        GetType(OTA_AirRules.OTA_AirRulesRS), _
                                        oAirRulesRQ, _
                                        oAirRulesRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch Ex As Exception

            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_OTA_AirRulesRS" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            strCiudadOrigen = Nothing
            strCiudadDestino = Nothing
            strFechaSalida = Nothing
            strFareBasis = Nothing
            strTicketDesignator = Nothing
            strAerolinea = Nothing
            strAccount = Nothing
            strCategory = Nothing
            strRPH = Nothing

            strCodigoSeguimiento = Nothing
            objSession = Nothing
            intGDS = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oAirRulesService = Nothing
            oAirRulesRQ = Nothing


            oOptionalQualifiers = Nothing
            oPricingQualifiers = Nothing
            oAccount = Nothing

            oOriginDestinationInformation = Nothing
            oFlightSegment = Nothing
            oDestinationLocation = Nothing
            oMarketingCarrier = Nothing
            oOriginLocation = Nothing

            oRuleReqInfo = Nothing
            oFareBasis = Nothing


        End Try

        Return oAirRulesRS

    End Function
    Public Function _ARUNK(ByVal strCodigoSeguimiento As String, _
                           ByVal intGDS As Integer, _
                           ByVal intFirmaGDS As Integer, _
                           ByVal intFirmaDB As Integer, _
                           ByVal objSession As classSession) As ARUNK.ARUNK_RS

        Const ID_SABRE_WEB_SERVICE As String = "32"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        '
        Dim oSecurity1 As ARUNK.Security1 = Nothing
        Dim oUsernameToken As ARUNK.SecurityUsernameToken = Nothing
        Dim oMessageHeader As ARUNK.MessageHeader = Nothing
        Dim oFromPartyId As ARUNK.PartyId = Nothing
        Dim oFrom As ARUNK.From = Nothing
        Dim oFromPartyIdArr(0) As ARUNK.PartyId
        Dim oToPartyId As New ARUNK.PartyId
        Dim oToPartyIdArr(0) As ARUNK.PartyId
        Dim oTo As ARUNK.[To] = Nothing
        Dim oMessageData As ARUNK.MessageData = Nothing
        Dim oService As ARUNK.Service = Nothing
        '

        Dim oARUNK_RQ As ARUNK.ARUNK_RQ = Nothing
        Dim oARUNKService As ARUNK.ARUNK_Service = Nothing
        Dim oARUNK_RS As ARUNK.ARUNK_RS = Nothing

        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try

            oMessageHeader = New ARUNK.MessageHeader
            oFromPartyId = New ARUNK.PartyId
            oFrom = New ARUNK.From
            oToPartyId = New ARUNK.PartyId
            oTo = New ARUNK.[To]
            oService = New ARUNK.Service
            oMessageData = New ARUNK.MessageData
            oARUNKService = New ARUNK.ARUNK_Service

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oARUNKService)


            oUsernameToken = New ARUNK.SecurityUsernameToken
            oSecurity1 = New ARUNK.Security1
            oARUNK_RQ = New ARUNK.ARUNK_RQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oARUNK_RQ, _
                         True, _
                         oARUNKService, _
                         intFirmaGDS)



            oARUNK_RQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oARUNK_RQ.TimeStampSpecified = True

            oARUNK_RQ.ReturnHostCommand = True
            oARUNK_RQ.ReturnHostCommandSpecified = True
            '

            oARUNK_RS = oARUNKService.ARUNK_RQ(oARUNK_RQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(ARUNK.ARUNK_RQ), _
                                        GetType(ARUNK.ARUNK_RS), _
                                        oARUNK_RQ, _
                                        oARUNK_RS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch Ex As Exception

            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_ARUNK" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally

            strCodigoSeguimiento = Nothing
            objSession = Nothing
            intGDS = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oARUNKService = Nothing
            oARUNK_RQ = Nothing

        End Try

        Return oARUNK_RS

    End Function
    Public Function _DesignatePrinter(ByVal strHardcopy As String, _
                                      ByVal strTicket As String, _
                                      ByVal strProfile As String, _
                                      ByVal strCodigoSeguimiento As String, _
                                      ByVal intGDS As Integer, _
                                      ByVal intFirmaGDS As Integer, _
                                      ByVal intFirmaDB As Integer, _
                                      ByVal objSession As classSession) As DesignatePrinter.DesignatePrinterRS

        Const ID_SABRE_WEB_SERVICE As String = "22"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        '
        Dim oSecurity1 As DesignatePrinter.Security1 = Nothing
        Dim oUsernameToken As DesignatePrinter.SecurityUsernameToken = Nothing
        Dim oMessageHeader As DesignatePrinter.MessageHeader = Nothing
        Dim oFromPartyId As DesignatePrinter.PartyId = Nothing
        Dim oFrom As DesignatePrinter.From = Nothing
        Dim oFromPartyIdArr(0) As DesignatePrinter.PartyId
        Dim oToPartyId As New DesignatePrinter.PartyId
        Dim oToPartyIdArr(0) As DesignatePrinter.PartyId
        Dim oTo As DesignatePrinter.[To] = Nothing
        Dim oMessageData As DesignatePrinter.MessageData = Nothing
        Dim oService As DesignatePrinter.Service = Nothing
        '

        Dim oDesignatePrinterRQ As DesignatePrinter.DesignatePrinterRQ = Nothing
        Dim oDesignatePrinterService As DesignatePrinter.DesignatePrinterService = Nothing
        Dim oDesignatePrinterRS As DesignatePrinter.DesignatePrinterRS = Nothing

        Dim Profile As DesignatePrinter.DesignatePrinterRQProfile = Nothing

        Dim Printers As DesignatePrinter.DesignatePrinterRQPrinters = Nothing
        Dim PrintersHardcopy As DesignatePrinter.DesignatePrinterRQPrintersHardcopy = Nothing
        Dim PrintersTicket As DesignatePrinter.DesignatePrinterRQPrintersTicket = Nothing


        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try

            oMessageHeader = New DesignatePrinter.MessageHeader
            oFromPartyId = New DesignatePrinter.PartyId
            oFrom = New DesignatePrinter.From
            oToPartyId = New DesignatePrinter.PartyId
            oTo = New DesignatePrinter.[To]
            oService = New DesignatePrinter.Service
            oMessageData = New DesignatePrinter.MessageData
            oDesignatePrinterService = New DesignatePrinter.DesignatePrinterService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oDesignatePrinterService)


            oUsernameToken = New DesignatePrinter.SecurityUsernameToken
            oSecurity1 = New DesignatePrinter.Security1
            oDesignatePrinterRQ = New DesignatePrinter.DesignatePrinterRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oDesignatePrinterRQ, _
                         True, _
                         oDesignatePrinterService, _
                         intFirmaGDS)

            If Not String.IsNullOrEmpty(strTicket) Then
                Printers = New DesignatePrinter.DesignatePrinterRQPrinters

                PrintersTicket = New DesignatePrinter.DesignatePrinterRQPrintersTicket
                PrintersTicket.CountryCode = "PE"
                PrintersTicket.LNIATA = strTicket

                Printers.Ticket = New DesignatePrinter.DesignatePrinterRQPrintersTicket
                Printers.Ticket = PrintersTicket

                oDesignatePrinterRQ.Printers = New DesignatePrinter.DesignatePrinterRQPrinters
                oDesignatePrinterRQ.Printers = Printers

            End If


            If Not String.IsNullOrEmpty(strHardcopy) Then

                Printers = New DesignatePrinter.DesignatePrinterRQPrinters
                PrintersHardcopy = New DesignatePrinter.DesignatePrinterRQPrintersHardcopy
                PrintersHardcopy.LNIATA = strHardcopy
                PrintersHardcopy.SpacingSpecified = False
                PrintersHardcopy.Undesignate = False
                PrintersHardcopy.UndesignateSpecified = False

                Printers.Hardcopy = New DesignatePrinter.DesignatePrinterRQPrintersHardcopy
                Printers.Hardcopy = PrintersHardcopy

                oDesignatePrinterRQ.Printers = New DesignatePrinter.DesignatePrinterRQPrinters
                oDesignatePrinterRQ.Printers = Printers
            End If

            If Not String.IsNullOrEmpty(strProfile) Then
                Profile = New DesignatePrinter.DesignatePrinterRQProfile
                Profile.Undesignate = False
                Profile.UndesignateSpecified = False
                Profile.Number = strProfile

                oDesignatePrinterRQ.Profile = New DesignatePrinter.DesignatePrinterRQProfile
                oDesignatePrinterRQ.Profile = Profile
            End If

            oDesignatePrinterRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oDesignatePrinterRQ.TimeStampSpecified = True

            oDesignatePrinterRQ.ReturnHostCommand = True
            oDesignatePrinterRQ.ReturnHostCommandSpecified = True
            '

            oDesignatePrinterRS = oDesignatePrinterService.DesignatePrinterRQ(oDesignatePrinterRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(DesignatePrinter.DesignatePrinterRQ), _
                                        GetType(DesignatePrinter.DesignatePrinterRS), _
                                        oDesignatePrinterRQ, _
                                        oDesignatePrinterRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch Ex As Exception

            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_DesignatePrinter" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            strHardcopy = Nothing
            strTicket = Nothing
            strProfile = Nothing
            strCodigoSeguimiento = Nothing
            objSession = Nothing
            intFirmaGDS = Nothing
            intGDS = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oDesignatePrinterService = Nothing
            oDesignatePrinterRQ = Nothing

        End Try

        Return oDesignatePrinterRS

    End Function
    Public Function _RulesFromPrice(ByVal strProfile As String, _
                                    ByVal strCodigoSeguimiento As String, _
                                    ByVal intGDS As Integer, _
                                    ByVal intFirmaGDS As Integer, _
                                    ByVal intFirmaDB As Integer, _
                                    ByVal objSession As classSession) As RulesFromPrice.RulesFromPriceRS

        Const ID_SABRE_WEB_SERVICE As String = "26"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        '
        Dim oSecurity1 As RulesFromPrice.Security1 = Nothing
        Dim oUsernameToken As RulesFromPrice.SecurityUsernameToken = Nothing
        Dim oMessageHeader As RulesFromPrice.MessageHeader = Nothing
        Dim oFromPartyId As RulesFromPrice.PartyId = Nothing
        Dim oFrom As RulesFromPrice.From = Nothing
        Dim oFromPartyIdArr(0) As RulesFromPrice.PartyId
        Dim oToPartyId As New RulesFromPrice.PartyId
        Dim oToPartyIdArr(0) As RulesFromPrice.PartyId
        Dim oTo As RulesFromPrice.[To] = Nothing
        Dim oMessageData As RulesFromPrice.MessageData = Nothing
        Dim oService As RulesFromPrice.Service = Nothing
        '

        Dim oRulesFromPriceRQ As RulesFromPrice.RulesFromPriceRQ = Nothing
        Dim oRulesFromPriceService As RulesFromPrice.RulesFromPriceService = Nothing
        Dim oRulesFromPriceRS As RulesFromPrice.RulesFromPriceRS = Nothing

        Dim RuleReqInfo As RulesFromPrice.RulesFromPriceRQRuleReqInfo = Nothing

        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try

            oMessageHeader = New RulesFromPrice.MessageHeader
            oFromPartyId = New RulesFromPrice.PartyId
            oFrom = New RulesFromPrice.From
            oToPartyId = New RulesFromPrice.PartyId
            oTo = New RulesFromPrice.[To]
            oService = New RulesFromPrice.Service
            oMessageData = New RulesFromPrice.MessageData
            oRulesFromPriceService = New RulesFromPrice.RulesFromPriceService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oRulesFromPriceService)


            oUsernameToken = New RulesFromPrice.SecurityUsernameToken
            oSecurity1 = New RulesFromPrice.Security1
            oRulesFromPriceRQ = New RulesFromPrice.RulesFromPriceRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oRulesFromPriceRQ, _
                         True, _
                         oRulesFromPriceService, _
                         intFirmaGDS)


            RuleReqInfo = New RulesFromPrice.RulesFromPriceRQRuleReqInfo
            RuleReqInfo.LevelTwo = True
            RuleReqInfo.LevelTwoSpecified = True

            oRulesFromPriceRQ.RuleReqInfo = New RulesFromPrice.RulesFromPriceRQRuleReqInfo
            oRulesFromPriceRQ.RuleReqInfo = RuleReqInfo

            oRulesFromPriceRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oRulesFromPriceRQ.TimeStampSpecified = True

            oRulesFromPriceRQ.ReturnHostCommand = True
            oRulesFromPriceRQ.ReturnHostCommandSpecified = True
            '

            oRulesFromPriceRS = oRulesFromPriceService.RulesFromPriceRQ(oRulesFromPriceRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(RulesFromPrice.RulesFromPriceRQ), _
                                        GetType(RulesFromPrice.RulesFromPriceRS), _
                                        oRulesFromPriceRQ, _
                                        oRulesFromPriceRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch Ex As Exception

            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_RulesFromPrice" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally

            strCodigoSeguimiento = Nothing
            objSession = Nothing
            intGDS = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oRulesFromPriceService = Nothing
            oRulesFromPriceRQ = Nothing

        End Try

        Return oRulesFromPriceRS

    End Function
    Public Function _PromotionalShopping(ByVal strPseudoCityCode As String, _
                                         ByVal intRequestStep As Integer, _
                                         ByVal strDepartureCity As String, _
                                         ByVal strArrivalCity As String, _
                                         ByVal strCarrier As String, _
                                         ByVal strRequestCode As String, _
                                         ByVal strRequestType As String, _
                                         ByVal strOutboundDateTime As String, _
                                         ByVal strInboundDateTime As String, _
                                         ByVal intDateRange As Integer, _
                                         ByVal strFareBasisCode As String, _
                                         ByVal strFareAmount As String, _
                                         ByVal strFareCurrency As String, _
                                         ByVal strPassengerType As String, _
                                         ByVal strPassengerCount As String, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intGDS As Integer, _
                                         ByVal intFirmaGDS As Integer, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal objSession As classSession) As PromotionalShopping.OTA_BestFareFinderRS

        Const ID_SABRE_WEB_SERVICE As String = "42"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        '
        Dim oSecurity As PromotionalShopping.Security = Nothing
        Dim oPos As PromotionalShopping.OTA_BestFareFinderRQPOS = Nothing
        Dim oUsernameToken As PromotionalShopping.SecurityUsernameToken = Nothing
        Dim oMessageHeader As PromotionalShopping.MessageHeader = Nothing
        Dim oFromPartyId As PromotionalShopping.PartyId = Nothing
        Dim oFrom As PromotionalShopping.From = Nothing
        Dim oFromPartyIdArr(0) As PromotionalShopping.PartyId
        Dim oToPartyId As New PromotionalShopping.PartyId
        Dim oToPartyIdArr(0) As PromotionalShopping.PartyId
        Dim oTo As PromotionalShopping.[To] = Nothing
        Dim oMessageData As PromotionalShopping.MessageData = Nothing
        Dim oService As PromotionalShopping.Service = Nothing
        '

        Dim oOTA_BestFareFinderRQ As PromotionalShopping.OTA_BestFareFinderRQ = Nothing
        Dim oPromotionalService As PromotionalShopping.PromotionalService = Nothing
        Dim oOTA_BestFareFinderRS As PromotionalShopping.OTA_BestFareFinderRS = Nothing
        '
        Dim BestFareFinderPreferences As PromotionalShopping.OTA_BestFareFinderRQBestFareFinderPreferences = Nothing
        Dim DateRange As PromotionalShopping.OTA_BestFareFinderRQBestFareFinderPreferencesDateRange = Nothing
        Dim Outbound() As PromotionalShopping.OTA_BestFareFinderRQBestFareFinderPreferencesOutbound = Nothing
        Dim Inbound() As PromotionalShopping.OTA_BestFareFinderRQBestFareFinderPreferencesOutboundInbound = Nothing
        Dim RequestedFare() As PromotionalShopping.OTA_BestFareFinderRQBestFareFinderPreferencesRequestedFare = Nothing
        Dim PassengerType() As PromotionalShopping.OTA_BestFareFinderRQBestFareFinderPreferencesPassengerType = Nothing

        Dim IntelliSellTransaction As PromotionalShopping.OTA_BestFareFinderRQIntelliSellTransaction = Nothing
        Dim ServiceTag As PromotionalShopping.OTA_BestFareFinderRQIntelliSellTransactionServiceTag = Nothing

        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing
        Dim FechaAux As String = Nothing
        Try

            oMessageHeader = New PromotionalShopping.MessageHeader
            oFromPartyId = New PromotionalShopping.PartyId
            oFrom = New PromotionalShopping.From
            oToPartyId = New PromotionalShopping.PartyId
            oTo = New PromotionalShopping.[To]
            oService = New PromotionalShopping.Service
            oMessageData = New PromotionalShopping.MessageData
            oPromotionalService = New PromotionalShopping.PromotionalService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oPromotionalService)


            oUsernameToken = New PromotionalShopping.SecurityUsernameToken
            oSecurity = New PromotionalShopping.Security
            oPos = New PromotionalShopping.OTA_BestFareFinderRQPOS
            oOTA_BestFareFinderRQ = New PromotionalShopping.OTA_BestFareFinderRQ

            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity, _
                         Nothing, _
                         oPos, _
                         oOTA_BestFareFinderRQ, _
                         True, _
                         oPromotionalService, _
                         intFirmaGDS)

            oPos.PseudoCityCode = UCase(Trim(strPseudoCityCode))
            oPos.CompanyCode = "TN"

            BestFareFinderPreferences = New PromotionalShopping.OTA_BestFareFinderRQBestFareFinderPreferences
            BestFareFinderPreferences.RequestType = UCase(Trim(strRequestType))
            If Not String.IsNullOrEmpty(Trim(strRequestCode)) Then
                If strRequestType.Equals("O") Then
                    If intRequestStep = 1 Or intRequestStep = 5 Then
                        BestFareFinderPreferences.RequestCode = UCase(Trim(strRequestCode))
                    End If
                ElseIf strRequestType.Equals("R") Then
                    If intRequestStep = 1 Or intRequestStep = 3 Then
                        BestFareFinderPreferences.RequestCode = UCase(Trim(strRequestCode))
                    End If
                End If
            End If

            BestFareFinderPreferences.RequestStep = intRequestStep
            BestFareFinderPreferences.DepartureCity = UCase(Trim(strDepartureCity))
            BestFareFinderPreferences.ArrivalCity = UCase(Trim(strArrivalCity))
            BestFareFinderPreferences.Carrier = UCase(Trim(strCarrier))


            Select Case intRequestStep

                Case 1
                    If intDateRange > 0 Then
                        DateRange = New PromotionalShopping.OTA_BestFareFinderRQBestFareFinderPreferencesDateRange
                        DateRange.DateRange = intDateRange

                        If Not String.IsNullOrEmpty(strOutboundDateTime) Then
                            If strOutboundDateTime.Split(Constantes.Arroba).Length = 1 Then
                                'strOutboundDateTime = "yyyy-MM-dd"
                                FechaAux = Format(CDate(strOutboundDateTime), Constantes.IWS_DATE_FORMAT_FILE2)
                                DateRange.OutboundDate = FechaAux
                                DateRange.OutboundDateSpecified = True
                            End If
                        End If

                        BestFareFinderPreferences.DateRange = DateRange

                    End If

                Case 2


                    If Not String.IsNullOrEmpty(strOutboundDateTime) Then
                        If Outbound Is Nothing Then
                            ReDim Outbound(0)
                            Outbound(Outbound.Length - 1) = New PromotionalShopping.OTA_BestFareFinderRQBestFareFinderPreferencesOutbound
                            'strOutboundDateTime = "yyyy-MM-dd"
                            FechaAux = Format(CDate(strOutboundDateTime), Constantes.IWS_DATE_FORMAT_FILE2)
                            Outbound(Outbound.Length - 1).Date = FechaAux

                            BestFareFinderPreferences.DateLists = Outbound
                        End If
                    End If

                Case 3

                    If intDateRange > 0 Then
                        DateRange = New PromotionalShopping.OTA_BestFareFinderRQBestFareFinderPreferencesDateRange
                        DateRange.DateRange = intDateRange
                        BestFareFinderPreferences.DateRange = DateRange
                    End If

                    If Not String.IsNullOrEmpty(strOutboundDateTime) Then
                        If Outbound Is Nothing Then
                            ReDim Outbound(0)
                            Outbound(Outbound.Length - 1) = New PromotionalShopping.OTA_BestFareFinderRQBestFareFinderPreferencesOutbound
                            'strOutboundDateTime = "yyyy-MM-dd"
                            FechaAux = Format(CDate(strOutboundDateTime), Constantes.IWS_DATE_FORMAT_FILE2)
                            Outbound(Outbound.Length - 1).Date = FechaAux
                        End If
                        BestFareFinderPreferences.DateLists = Outbound
                    End If

                Case 4

                    If Not String.IsNullOrEmpty(strOutboundDateTime) Then
                        If Outbound Is Nothing Then
                            ReDim Outbound(0)
                            Outbound(Outbound.Length - 1) = New PromotionalShopping.OTA_BestFareFinderRQBestFareFinderPreferencesOutbound
                            'strOutboundDateTime = "yyyy-MM-dd"
                            FechaAux = Format(CDate(strOutboundDateTime), Constantes.IWS_DATE_FORMAT_FILE2)
                            Outbound(Outbound.Length - 1).Date = FechaAux

                            If Not String.IsNullOrEmpty(strInboundDateTime) Then
                                'strInboundDateTime = "yyyy-MM-dd@yyyy-MM-dd"
                                For i As Integer = 0 To strInboundDateTime.Split(Constantes.Arroba).Length - 1

                                    If Inbound Is Nothing Then
                                        ReDim Inbound(0)
                                    Else
                                        ReDim Preserve Inbound(Inbound.Length)
                                    End If
                                    Inbound(Inbound.Length - 1) = New PromotionalShopping.OTA_BestFareFinderRQBestFareFinderPreferencesOutboundInbound
                                    'strInboundDateTime = "yyyy-MM-dd"
                                    FechaAux = Format(CDate(strInboundDateTime), Constantes.IWS_DATE_FORMAT_FILE2)
                                    Inbound(Inbound.Length - 1).Date = FechaAux
                                Next

                                Outbound(0).Inbound = Inbound
                            End If

                            BestFareFinderPreferences.DateLists = Outbound

                        End If
                    End If


                Case 5
                    'strOutboundDateTime "yyyy-MM-ddT00:00:00"
                    FechaAux = Format(CDate(strOutboundDateTime), Constantes.IWS_OUTIN_DATATIME_FORMAT)
                    BestFareFinderPreferences.OutboundDateTime = FechaAux
                    BestFareFinderPreferences.OutboundDateTimeSpecified = True
                Case 6
                    'strOutboundDateTime "yyyy-MM-ddT00:00:00"
                    FechaAux = Format(CDate(strOutboundDateTime), Constantes.IWS_OUTIN_DATATIME_FORMAT)
                    BestFareFinderPreferences.OutboundDateTime = FechaAux
                    BestFareFinderPreferences.OutboundDateTimeSpecified = True

                    'strInboundDateTime "yyyy-MM-ddT00:00:00"
                    FechaAux = Format(CDate(strInboundDateTime), Constantes.IWS_OUTIN_DATATIME_FORMAT)
                    BestFareFinderPreferences.InboundDateTime = FechaAux
                    BestFareFinderPreferences.InboundDateTimeSpecified = True
            End Select

            'Agregamos el farebasis
            If Not String.IsNullOrEmpty(strFareBasisCode) Then
                For i As Integer = 0 To strFareBasisCode.Split(Constantes.Arroba).Length - 1
                    If RequestedFare Is Nothing Then
                        ReDim RequestedFare(0)
                    Else
                        ReDim Preserve RequestedFare(RequestedFare.Length)
                    End If

                    RequestedFare(RequestedFare.Length - 1) = New PromotionalShopping.OTA_BestFareFinderRQBestFareFinderPreferencesRequestedFare
                    RequestedFare(RequestedFare.Length - 1).FareCurrency = UCase(Trim(strFareCurrency))
                    RequestedFare(RequestedFare.Length - 1).FareBasisCode = UCase(Trim(strFareBasisCode.Split(Constantes.Arroba)(i)))
                    RequestedFare(RequestedFare.Length - 1).FareAmount = strFareAmount.Split(Constantes.Slash)(i)
                Next
                BestFareFinderPreferences.RequestedFares = RequestedFare
            End If


            'Agregamos el tipo de pasajero
            If Not String.IsNullOrEmpty(strPassengerType) Then
                For i As Integer = 0 To strPassengerType.Split(Constantes.Slash).Length - 1
                    If PassengerType Is Nothing Then
                        ReDim PassengerType(0)
                    Else
                        ReDim Preserve PassengerType(PassengerType.Length)
                    End If

                    PassengerType(PassengerType.Length - 1) = New PromotionalShopping.OTA_BestFareFinderRQBestFareFinderPreferencesPassengerType
                    PassengerType(PassengerType.Length - 1).Count = strPassengerCount.Split(Constantes.Slash)(i)
                    PassengerType(PassengerType.Length - 1).Type = UCase(Trim(strPassengerType.Split(Constantes.Slash)(i)))
                Next
                BestFareFinderPreferences.PassengerTypes = PassengerType
            End If

            oOTA_BestFareFinderRQ.BestFareFinderPreferences = BestFareFinderPreferences

            IntelliSellTransaction = New PromotionalShopping.OTA_BestFareFinderRQIntelliSellTransaction
            ServiceTag = New PromotionalShopping.OTA_BestFareFinderRQIntelliSellTransactionServiceTag
            ServiceTag.Value = "BFFOTA"
            IntelliSellTransaction.ServiceTag = New PromotionalShopping.OTA_BestFareFinderRQIntelliSellTransactionServiceTag
            IntelliSellTransaction.ServiceTag = ServiceTag


            oOTA_BestFareFinderRQ.ResponseType = "BFF"
            oOTA_BestFareFinderRQ.Version = "1.0.2"
            oOTA_BestFareFinderRQ.ResponseVersion = "1.0.2"

            oOTA_BestFareFinderRQ.POS = New PromotionalShopping.OTA_BestFareFinderRQPOS
            oOTA_BestFareFinderRQ.POS = oPos

            oOTA_BestFareFinderRS = oPromotionalService.PromotionalShoppingRQ(oOTA_BestFareFinderRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(PromotionalShopping.OTA_BestFareFinderRQ), _
                                        GetType(PromotionalShopping.OTA_BestFareFinderRS), _
                                        oOTA_BestFareFinderRQ, _
                                        oOTA_BestFareFinderRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch Ex As Exception

            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_PromotionalShopping" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally
            strPseudoCityCode = Nothing
            intRequestStep = Nothing
            strDepartureCity = Nothing
            strArrivalCity = Nothing
            strCarrier = Nothing
            strRequestCode = Nothing
            strRequestType = Nothing
            strOutboundDateTime = Nothing
            strInboundDateTime = Nothing
            intDateRange = Nothing
            strFareBasisCode = Nothing
            strFareAmount = Nothing
            strFareCurrency = Nothing
            strPassengerType = Nothing
            strPassengerCount = Nothing

            strCodigoSeguimiento = Nothing
            objSession = Nothing
            intGDS = Nothing
            oSecurity = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oPromotionalService = Nothing
            oOTA_BestFareFinderRQ = Nothing

        End Try

        Return oOTA_BestFareFinderRS

    End Function
    Public Function _InvoiceItinerary(ByVal strCodigoSeguimiento As String, _
                                      ByVal intGDS As Integer, _
                                      ByVal intFirmaGDS As Integer, _
                                      ByVal intFirmaDB As Integer, _
                                      ByVal objSession As classSession) As InvoiceItinerary.InvoiceItineraryRS

        Const ID_SABRE_WEB_SERVICE As String = "45"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        '
        Dim oSecurity As InvoiceItinerary.Security = Nothing
        Dim oPos As InvoiceItinerary.InvoiceItineraryRQPOS = Nothing
        Dim oSource As InvoiceItinerary.InvoiceItineraryRQPOSSource = Nothing
        Dim oUsernameToken As InvoiceItinerary.SecurityUsernameToken = Nothing
        Dim oMessageHeader As InvoiceItinerary.MessageHeader = Nothing
        Dim oFromPartyId As InvoiceItinerary.PartyId = Nothing
        Dim oFrom As InvoiceItinerary.From = Nothing
        Dim oFromPartyIdArr(0) As InvoiceItinerary.PartyId
        Dim oToPartyId As New InvoiceItinerary.PartyId
        Dim oToPartyIdArr(0) As InvoiceItinerary.PartyId
        Dim oTo As InvoiceItinerary.[To] = Nothing
        Dim oMessageData As InvoiceItinerary.MessageData = Nothing
        Dim oService As InvoiceItinerary.Service = Nothing
        '

        Dim oInvoiceItineraryRQ As InvoiceItinerary.InvoiceItineraryRQ = Nothing
        Dim oInvoiceItineraryService As InvoiceItinerary.InvoiceItineraryService = Nothing
        Dim oInvoiceItineraryRS As InvoiceItinerary.InvoiceItineraryRS = Nothing
        '
        Dim InvoiceItineraryInfo As InvoiceItinerary.InvoiceItineraryRQInvoiceItineraryInfo = Nothing


        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try

            oMessageHeader = New InvoiceItinerary.MessageHeader
            oFromPartyId = New InvoiceItinerary.PartyId
            oFrom = New InvoiceItinerary.From
            oToPartyId = New InvoiceItinerary.PartyId
            oTo = New InvoiceItinerary.[To]
            oService = New InvoiceItinerary.Service
            oMessageData = New InvoiceItinerary.MessageData
            oInvoiceItineraryService = New InvoiceItinerary.InvoiceItineraryService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oInvoiceItineraryService)


            oUsernameToken = New InvoiceItinerary.SecurityUsernameToken
            oSecurity = New InvoiceItinerary.Security
            oPos = New InvoiceItinerary.InvoiceItineraryRQPOS
            oSource = New InvoiceItinerary.InvoiceItineraryRQPOSSource
            oInvoiceItineraryRQ = New InvoiceItinerary.InvoiceItineraryRQ

            obj_Security("QF05", _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity, _
                         oSource, _
                         oPos, _
                         oInvoiceItineraryRQ, _
                         True, _
                         oInvoiceItineraryService, _
                         intFirmaGDS)


            InvoiceItineraryInfo = New InvoiceItinerary.InvoiceItineraryRQInvoiceItineraryInfo
            InvoiceItineraryInfo.Print = "Itinerary"

            oInvoiceItineraryRQ.InvoiceItineraryInfo = New InvoiceItinerary.InvoiceItineraryRQInvoiceItineraryInfo
            oInvoiceItineraryRQ.InvoiceItineraryInfo = InvoiceItineraryInfo

            oInvoiceItineraryRQ.Version = "1.1.1"
            oInvoiceItineraryRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oInvoiceItineraryRQ.POS = oPos



            oInvoiceItineraryRS = oInvoiceItineraryService.InvoiceItineraryRQ(oInvoiceItineraryRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(InvoiceItinerary.InvoiceItineraryRQ), _
                                        GetType(InvoiceItinerary.InvoiceItineraryRS), _
                                        oInvoiceItineraryRQ, _
                                        oInvoiceItineraryRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch Ex As Exception

            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_InvoiceItinerary" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(Ex.ToString)
        Finally

            strCodigoSeguimiento = Nothing
            objSession = Nothing
            intGDS = Nothing
            oSecurity = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oInvoiceItineraryService = Nothing
            oInvoiceItineraryRQ = Nothing

        End Try

        Return oInvoiceItineraryRS

    End Function
    Public Function _Fare(ByVal strOrigen As String, _
                          ByVal strDestino As String, _
                          ByVal strFechaSal As String, _
                          ByVal strFechaRet As String, _
                          ByVal strPseudo As String, _
                          ByVal strOrientacion As String, _
                          ByVal strAerolinea As String, _
                          ByVal strTipoTarifa As String, _
                          ByVal strTipoPasajero As String, _
                          ByVal strCodigoSeguimiento As String, _
                          ByVal intGDS As Integer, _
                          ByVal intFirmaGDS As Integer, _
                          ByVal intFirmaDB As Integer, _
                          ByVal objSession As classSession) As Fare.FareRS

        Const ID_SABRE_WEB_SERVICE As String = "45"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity1 As Fare.Security1 = Nothing

        Dim oUsernameToken As Fare.SecurityUsernameToken = Nothing
        Dim oMessageHeader As Fare.MessageHeader = Nothing

        Dim oFromPartyId As Fare.PartyId = Nothing
        Dim oFrom As Fare.From = Nothing
        Dim oFromPartyIdArr(0) As Fare.PartyId
        Dim oToPartyId As Fare.PartyId = Nothing
        Dim oToPartyIdArr(0) As Fare.PartyId
        Dim oTo As Fare.[To] = Nothing
        Dim oMessageData As Fare.MessageData = Nothing
        Dim oService As Fare.Service = Nothing

        Dim oFareRQ As Fare.FareRQ = Nothing
        Dim oFareService As Fare.FareService = Nothing
        Dim oFareRS As Fare.FareRS = Nothing

        Dim oOriginDestination As Fare.FareRQOriginDestinationInformation = Nothing
        Dim oFlightSegment As Fare.FareRQOriginDestinationInformationFlightSegment = Nothing
        Dim oDestination As Fare.FareRQOriginDestinationInformationFlightSegmentDestinationLocation = Nothing
        Dim oOrigin As Fare.FareRQOriginDestinationInformationFlightSegmentOriginLocation = Nothing

        Dim oOptionalQualifiers As Fare.FareRQOptionalQualifiers = Nothing
        Dim oTimeQualifiers As Fare.FareRQOptionalQualifiersTimeQualifiers = Nothing
        Dim oTimeQualifiersTravelDateOptions As Fare.FareRQOptionalQualifiersTimeQualifiersTravelDateOptions = Nothing

        Dim oFlightQualifiers As Fare.FareRQOptionalQualifiersFlightQualifiers = Nothing
        Dim oVendorPrefs() As Fare.FareRQOptionalQualifiersFlightQualifiersAirline = Nothing
        Dim Airline As Fare.FareRQOptionalQualifiersFlightQualifiersAirline = Nothing

        Dim oPricingQualifiers As Fare.FareRQOptionalQualifiersPricingQualifiers = Nothing
        Dim oFareOptions As Fare.FareRQOptionalQualifiersPricingQualifiersFareOptions = Nothing
        Dim oFareType As Fare.FareRQOptionalQualifiersPricingQualifiersFareType = Nothing
        Dim oPassengerType() As Fare.FareRQOptionalQualifiersPricingQualifiersPassengerType = Nothing
        Dim oJourneyType As Fare.FareRQOptionalQualifiersPricingQualifiersJourneyType = Nothing

        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

        Try
            oMessageHeader = New Fare.MessageHeader
            oFromPartyId = New Fare.PartyId
            oFrom = New Fare.From
            oToPartyId = New Fare.PartyId
            oTo = New Fare.[To]
            oService = New Fare.Service
            oMessageData = New Fare.MessageData
            oFareService = New Fare.FareService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID.ToString, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oFareService)


            oUsernameToken = New Fare.SecurityUsernameToken
            oSecurity1 = New Fare.Security1
            oFareRQ = New Fare.FareRQ

            obj_Security(Nothing, _
                         objSession.Token.ToString, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oFareRQ, _
                         True, _
                         oFareService, _
                         intFirmaGDS)

            '======================================

            oOriginDestination = New Fare.FareRQOriginDestinationInformation
            oFlightSegment = New Fare.FareRQOriginDestinationInformationFlightSegment
            oDestination = New Fare.FareRQOriginDestinationInformationFlightSegmentDestinationLocation
            oOrigin = New Fare.FareRQOriginDestinationInformationFlightSegmentOriginLocation

            oOrigin.LocationCode = strOrigen
            oDestination.LocationCode = strDestino
            oFlightSegment.OriginLocation = oOrigin
            oFlightSegment.DestinationLocation = oDestination

            oOriginDestination.FlightSegment = oFlightSegment

            oFareRQ.OriginDestinationInformation = oOriginDestination
            oFareRQ.ReturnHostCommandSpecified = True
            oFareRQ.ReturnHostCommand = True

            '======================================
            If Not String.IsNullOrEmpty(strAerolinea) Then
                If strAerolinea.Split(Constantes.Slash).Length < 99 Then

                    For i As Integer = 0 To strAerolinea.Split(Constantes.Slash).Length - 1

                        Airline = New Fare.FareRQOptionalQualifiersFlightQualifiersAirline
                        Airline.Code = UCase(strAerolinea.Split(Constantes.Slash)(i))

                        If oVendorPrefs Is Nothing Then
                            ReDim oVendorPrefs(0)
                        Else
                            ReDim Preserve oVendorPrefs(oVendorPrefs.Length)
                        End If

                        oVendorPrefs(oVendorPrefs.Length - 1) = New Fare.FareRQOptionalQualifiersFlightQualifiersAirline
                        oVendorPrefs(oVendorPrefs.Length - 1) = Airline

                        If oFlightQualifiers Is Nothing Then oFlightQualifiers = New Fare.FareRQOptionalQualifiersFlightQualifiers
                        oFlightQualifiers.VendorPrefs = oVendorPrefs

                    Next
                End If
            End If

            '======================================

            If Not String.IsNullOrEmpty(strOrientacion) Then
                oJourneyType = New Fare.FareRQOptionalQualifiersPricingQualifiersJourneyType
                oJourneyType.Code = strOrientacion

                oPricingQualifiers = New Fare.FareRQOptionalQualifiersPricingQualifiers
                oPricingQualifiers.JourneyType = New Fare.FareRQOptionalQualifiersPricingQualifiersJourneyType
                oPricingQualifiers.JourneyType = oJourneyType

            End If

            If strFechaSal.Trim <> "" Then


                oOptionalQualifiers = New Fare.FareRQOptionalQualifiers
                If Not String.IsNullOrEmpty(strTipoTarifa) Then

                    oFareOptions = New Fare.FareRQOptionalQualifiersPricingQualifiersFareOptions

                    Select Case strTipoTarifa
                        Case "PL"
                            oFareOptions.Public = True
                            oFareOptions.PublicSpecified = True
                        Case "PV"
                            oFareOptions.Private = True
                            oFareOptions.PrivateSpecified = True
                        Case "NET"
                            oFareOptions.Net = True
                            oFareOptions.NetSpecified = True
                        Case "ADDON"
                            oFareOptions.AddOn = True
                            oFareOptions.AddOnSpecified = True
                    End Select

                    If Not String.IsNullOrEmpty(strTipoPasajero) Then
                        If strTipoPasajero.Contains("ALL") Then
                            oFareType = New Fare.FareRQOptionalQualifiersPricingQualifiersFareType
                            oFareType.InclusionCode = strTipoPasajero
                        End If
                    End If

                    If oPricingQualifiers Is Nothing Then oPricingQualifiers = New Fare.FareRQOptionalQualifiersPricingQualifiers
                    oPricingQualifiers.FareOptions = New Fare.FareRQOptionalQualifiersPricingQualifiersFareOptions
                    oPricingQualifiers.FareOptions = oFareOptions
                    If oFareType IsNot Nothing Then
                        oPricingQualifiers.FareType = New Fare.FareRQOptionalQualifiersPricingQualifiersFareType
                        oPricingQualifiers.FareType = oFareType
                    End If

                End If

                If Not String.IsNullOrEmpty(strTipoPasajero) Then


                    If Not strTipoPasajero.Contains("ALL") Then
                        For i As Integer = 0 To strTipoPasajero.Split(Constantes.Slash).Length - 1


                            If oPassengerType Is Nothing Then
                                ReDim oPassengerType(0)
                            Else
                                ReDim Preserve oPassengerType(oPassengerType.Length)
                            End If

                            oPassengerType(oPassengerType.Length - 1) = New Fare.FareRQOptionalQualifiersPricingQualifiersPassengerType
                            oPassengerType(oPassengerType.Length - 1).Code = strTipoPasajero.Split(Constantes.Slash)(i)

                        Next


                        If oPricingQualifiers Is Nothing Then oPricingQualifiers = New Fare.FareRQOptionalQualifiersPricingQualifiers
                        If oPassengerType IsNot Nothing Then
                            oPricingQualifiers.PassengerType = oPassengerType
                        End If

                    End If

                End If


                oTimeQualifiers = New Fare.FareRQOptionalQualifiersTimeQualifiers
                oTimeQualifiersTravelDateOptions = New Fare.FareRQOptionalQualifiersTimeQualifiersTravelDateOptions

                Dim FechaAux As String = Format(CDate(strFechaSal), "MM-dd")
                oTimeQualifiersTravelDateOptions.Start = FechaAux

                If strFechaRet.Trim <> "" Then
                    FechaAux = Format(CDate(strFechaRet), "MM-dd")
                    oTimeQualifiersTravelDateOptions.Return = FechaAux
                End If

                oTimeQualifiers.TravelDateOptions = oTimeQualifiersTravelDateOptions
                oOptionalQualifiers.TimeQualifiers = oTimeQualifiers

                If oFlightQualifiers IsNot Nothing Then
                    oOptionalQualifiers.FlightQualifiers = New Fare.FareRQOptionalQualifiersFlightQualifiers
                    oOptionalQualifiers.FlightQualifiers = oFlightQualifiers
                End If

                If oPricingQualifiers IsNot Nothing Then
                    oOptionalQualifiers.PricingQualifiers = New Fare.FareRQOptionalQualifiersPricingQualifiers
                    oOptionalQualifiers.PricingQualifiers = oPricingQualifiers
                End If

                oFareRQ.OptionalQualifiers = oOptionalQualifiers

            End If

            oFareRS = New Fare.FareRS
            oFareRS = oFareService.FareRQ(oFareRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(Fare.FareRQ), _
                                        GetType(Fare.FareRS), _
                                        oFareRQ, _
                                        oFareRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_Fare" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Token : " & objSession.Token.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "ConversationID : " & objSession.ConversationID.ToString & vbCrLf

            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(ex.ToString)

        Finally

            FUNCTION_NAME = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oFareRQ = Nothing
            oFareService = Nothing
            oGeneraPayLoadXML = Nothing

        End Try
        Return oFareRS
    End Function
    Public Function _OTA_AirBook(ByVal lstSegmentos As List(Of classSegmentos), _
                                 ByVal intCantidadPasajeros As Integer, _
                                 ByVal strCodigoSeguimiento As String, _
                                 ByVal intGDS As Integer, _
                                 ByVal intFirmaGDS As Integer, _
                                 ByVal intFirmaDB As Integer, _
                                 ByVal objSession As classSession) As OTA_AirBookLLS200.OTA_AirBookRS

        Const ID_SABRE_WEB_SERVICE As String = "07"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity1 As OTA_AirBookLLS200.Security1 = Nothing

        Dim oUsernameToken As OTA_AirBookLLS200.SecurityUsernameToken = Nothing
        Dim oMessageHeader As OTA_AirBookLLS200.MessageHeader = Nothing

        Dim oFromPartyId As OTA_AirBookLLS200.PartyId = Nothing
        Dim oFrom As OTA_AirBookLLS200.From = Nothing
        Dim oFromPartyIdArr(0) As OTA_AirBookLLS200.PartyId
        Dim oToPartyId As OTA_AirBookLLS200.PartyId = Nothing
        Dim oToPartyIdArr(0) As OTA_AirBookLLS200.PartyId
        Dim oTo As OTA_AirBookLLS200.[To] = Nothing
        Dim oMessageData As OTA_AirBookLLS200.MessageData = Nothing
        Dim oService As OTA_AirBookLLS200.Service = Nothing

        Dim oOTA_AirBookRQ As OTA_AirBookLLS200.OTA_AirBookRQ = Nothing
        Dim oOTA_AirBookService As OTA_AirBookLLS200.OTA_AirBookService = Nothing
        Dim oOTA_AirBookRS As OTA_AirBookLLS200.OTA_AirBookRS = Nothing
        '
        Dim oFlightSegment As OTA_AirBookLLS200.OTA_AirBookRQFlightSegment = Nothing
        Dim oDestinationLocation As OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentDestinationLocation = Nothing
        Dim oEquipment As OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentEquipment = Nothing
        Dim oMarketingAirline As OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentMarketingAirline = Nothing
        Dim oMarriageGrp As OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentMarriageGrp = Nothing
        Dim oOperatingAirline As OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentOperatingAirline = Nothing
        Dim oOriginLocation As OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentOriginLocation = Nothing

        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing
        '
        Dim cadenaFecha As String = Nothing
        Dim cadenaHora As String = Nothing

        Try
            oMessageHeader = New OTA_AirBookLLS200.MessageHeader
            oFromPartyId = New OTA_AirBookLLS200.PartyId
            oFrom = New OTA_AirBookLLS200.From
            oToPartyId = New OTA_AirBookLLS200.PartyId
            oTo = New OTA_AirBookLLS200.[To]
            oService = New OTA_AirBookLLS200.Service
            oMessageData = New OTA_AirBookLLS200.MessageData
            oOTA_AirBookService = New OTA_AirBookLLS200.OTA_AirBookService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID.ToString, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oOTA_AirBookService)


            oUsernameToken = New OTA_AirBookLLS200.SecurityUsernameToken
            oSecurity1 = New OTA_AirBookLLS200.Security1
            oOTA_AirBookRQ = New OTA_AirBookLLS200.OTA_AirBookRQ

            obj_Security(Nothing, _
                         objSession.Token.ToString, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oOTA_AirBookRQ, _
                         True, _
                         oOTA_AirBookService, _
                         intFirmaGDS)

            '======================================
            oOTA_AirBookRQ.ReturnHostCommandSpecified = True
            oOTA_AirBookRQ.ReturnHostCommand = True

            oOTA_AirBookRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oOTA_AirBookRQ.TimeStampSpecified = True
            oOTA_AirBookRQ.Version = "2.0.0"

            '--------------------------------

            If lstSegmentos IsNot Nothing Then
                For i As Integer = 0 To lstSegmentos.Count - 1

                    oFlightSegment = New OTA_AirBookLLS200.OTA_AirBookRQFlightSegment
                    oFlightSegment.Status = "NN"
                    If Not String.IsNullOrEmpty(lstSegmentos.Item(i).ClaseServicio) Then
                        oFlightSegment.ResBookDesigCode = lstSegmentos.Item(i).ClaseServicio
                    End If

                    oFlightSegment.NumberInParty = intCantidadPasajeros

                    If Not String.IsNullOrEmpty(lstSegmentos.Item(i).NumVuelo) Then
                        oFlightSegment.FlightNumber = lstSegmentos.Item(i).NumVuelo
                    End If

                    If Not String.IsNullOrEmpty(lstSegmentos.Item(i).FechaHoraSalida) Then
                        cadenaFecha = Format(CDate(lstSegmentos.Item(i).FechaHoraSalida.Split(" ")(0)), Constantes.IWS_DATE_FORMAT_FILE2)
                        cadenaHora = lstSegmentos.Item(i).FechaHoraSalida.Split(" ")(1)
                        oFlightSegment.DepartureDateTime = cadenaFecha & "T" & cadenaHora
                    End If

                    If Not String.IsNullOrEmpty(lstSegmentos.Item(i).FechaHoraLlegada) Then
                        cadenaFecha = Format(CDate(lstSegmentos.Item(i).FechaHoraLlegada.Split(" ")(0)), Constantes.IWS_DATE_FORMAT_FILE2)
                        cadenaHora = lstSegmentos.Item(i).FechaHoraLlegada.Split(" ")(1)
                        oFlightSegment.ArrivalDateTime = cadenaFecha & "T" & cadenaHora
                    End If

                    If Not String.IsNullOrEmpty(lstSegmentos.Item(i).Aerolinea) Then
                        oMarketingAirline = New OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentMarketingAirline
                        oMarketingAirline.Code = lstSegmentos.Item(i).Aerolinea
                        If Not String.IsNullOrEmpty(lstSegmentos.Item(i).NumVuelo) Then
                            oMarketingAirline.FlightNumber = lstSegmentos.Item(i).NumVuelo
                        End If
                        oFlightSegment.MarketingAirline = New OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentMarketingAirline
                        oFlightSegment.MarketingAirline = oMarketingAirline
                    End If

                    If Not String.IsNullOrEmpty(lstSegmentos.Item(i).Salida.Codigo) Then
                        oOriginLocation = New OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentOriginLocation
                        oOriginLocation.LocationCode = lstSegmentos.Item(i).Salida.Codigo
                        oFlightSegment.OriginLocation = New OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentOriginLocation
                        oFlightSegment.OriginLocation = oOriginLocation
                    End If

                    If Not String.IsNullOrEmpty(lstSegmentos.Item(i).Llegada.Codigo) Then
                        oDestinationLocation = New OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentDestinationLocation
                        oDestinationLocation.LocationCode = lstSegmentos.Item(i).Llegada.Codigo
                        oFlightSegment.DestinationLocation = New OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentDestinationLocation
                        oFlightSegment.DestinationLocation = oDestinationLocation
                    End If

                    If Not String.IsNullOrEmpty(lstSegmentos.Item(i).Equipo) Then
                        oEquipment = New OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentEquipment
                        oEquipment.AirEquipType = lstSegmentos.Item(i).Equipo
                        oFlightSegment.Equipment = New OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentEquipment
                        oFlightSegment.Equipment = oEquipment
                    End If

                    oMarriageGrp = New OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentMarriageGrp
                    If lstSegmentos.Item(i).Casado = 0 Then
                        oMarriageGrp.Ind = False
                    Else
                        oMarriageGrp.Ind = True
                    End If
                    oFlightSegment.MarriageGrp = New OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentMarriageGrp
                    oFlightSegment.MarriageGrp = oMarriageGrp

                    If Not String.IsNullOrEmpty(lstSegmentos.Item(i).OperadoPor) Then
                        oOperatingAirline = New OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentOperatingAirline
                        oOperatingAirline.Code = lstSegmentos.Item(i).OperadoPor
                        oFlightSegment.OperatingAirline = New OTA_AirBookLLS200.OTA_AirBookRQFlightSegmentOperatingAirline
                        oFlightSegment.OperatingAirline = oOperatingAirline
                    End If

                    If oOTA_AirBookRQ.OriginDestinationInformation Is Nothing Then
                        ReDim oOTA_AirBookRQ.OriginDestinationInformation(0)
                    Else
                        ReDim Preserve oOTA_AirBookRQ.OriginDestinationInformation(oOTA_AirBookRQ.OriginDestinationInformation().Length)
                    End If

                    oOTA_AirBookRQ.OriginDestinationInformation(oOTA_AirBookRQ.OriginDestinationInformation.Length - 1) = New OTA_AirBookLLS200.OTA_AirBookRQFlightSegment
                    oOTA_AirBookRQ.OriginDestinationInformation(oOTA_AirBookRQ.OriginDestinationInformation.Length - 1) = oFlightSegment

                Next
            End If

            '--------------------------------

            oOTA_AirBookRS = New OTA_AirBookLLS200.OTA_AirBookRS
            oOTA_AirBookRS = oOTA_AirBookService.OTA_AirBookRQ(oOTA_AirBookRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(OTA_AirBookLLS200.OTA_AirBookRQ), _
                                        GetType(OTA_AirBookLLS200.OTA_AirBookRS), _
                                        oOTA_AirBookRQ, _
                                        oOTA_AirBookRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_AirBook" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Token : " & objSession.Token.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "ConversationID : " & objSession.ConversationID.ToString & vbCrLf

            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(ex.ToString)
        Finally

            FUNCTION_NAME = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oOTA_AirBookRQ = Nothing
            oOTA_AirBookService = Nothing
            oGeneraPayLoadXML = Nothing

        End Try
        Return oOTA_AirBookRS
    End Function
    Public Function _AutomatedExchanges(ByVal strCodigoAerolinea As String, _
                                        ByVal strNumeroParajero As String, _
                                        ByVal strTicketOriginal As String, _
                                        ByVal lstSegmentos As List(Of String),
                                        ByVal strTourCode As String,
                                        ByVal strAccount As String,
                                        ByVal strCorporateId As String,
                                        ByVal strTipoTarifa As String,
                                        ByVal strPassengerType As String,
                                        ByVal strCodigoSeguimiento As String, _
                                        ByVal intGDS As Integer, _
                                        ByVal intFirmaGDS As Integer, _
                                        ByVal intFirmaDB As Integer, _
                                        ByVal objSession As classSession) As AutomatedExchanges.AutomatedExchangesRS

        Const ID_SABRE_WEB_SERVICE As String = "49"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity1 As AutomatedExchanges.Security1 = Nothing

        Dim oUsernameToken As AutomatedExchanges.SecurityUsernameToken = Nothing
        Dim oMessageHeader As AutomatedExchanges.MessageHeader = Nothing

        Dim oFromPartyId As AutomatedExchanges.PartyId = Nothing
        Dim oFrom As AutomatedExchanges.From = Nothing
        Dim oFromPartyIdArr(0) As AutomatedExchanges.PartyId
        Dim oToPartyId As AutomatedExchanges.PartyId = Nothing
        Dim oToPartyIdArr(0) As AutomatedExchanges.PartyId
        Dim oTo As AutomatedExchanges.[To] = Nothing
        Dim oMessageData As AutomatedExchanges.MessageData = Nothing
        Dim oService As AutomatedExchanges.Service = Nothing

        Dim oAutomatedExchangesRQ As AutomatedExchanges.AutomatedExchangesRQ = Nothing
        Dim oAutomatedExchangesRS As AutomatedExchanges.AutomatedExchangesRS = Nothing
        Dim oAutomatedExchangesService As AutomatedExchanges.AutomatedExchangesService = Nothing

        Dim ExchangeComparison As AutomatedExchanges.AutomatedExchangesRQExchangeComparison = Nothing
        Dim ExchangeSegment As AutomatedExchanges.AutomatedExchangesRQExchangeComparisonExchangeSegment() = Nothing

        Dim PriceRequestInformation As AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformation = Nothing
        Dim OptionalQualifiers As AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiers = Nothing
        Dim MiscQualifiers As AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersMiscQualifiers = Nothing
        Dim TourCode As AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersMiscQualifiersTourCode = Nothing

        Dim FlightQualifiers As AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersFlightQualifiers = Nothing
        Dim VendorPrefs As AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersFlightQualifiersVendorPrefs = Nothing
        Dim Airline As AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersFlightQualifiersVendorPrefsAirline = Nothing

        Dim PricingQualifiers As AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiers = Nothing
        Dim SegmentSelect As AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiersSegmentSelect() = Nothing
        Dim PassengerType As AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiersPassengerType = Nothing
        Dim FareOptions As AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiersFareOptions = Nothing
        Dim Corporate As AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiersCorporate = Nothing
        Dim Account As AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiersAccount = Nothing
        Dim NameSelect As AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiersNameSelect = Nothing
        '
        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing
        '


        Try
            oMessageHeader = New AutomatedExchanges.MessageHeader
            oFromPartyId = New AutomatedExchanges.PartyId
            oFrom = New AutomatedExchanges.From
            oToPartyId = New AutomatedExchanges.PartyId
            oTo = New AutomatedExchanges.[To]
            oService = New AutomatedExchanges.Service
            oMessageData = New AutomatedExchanges.MessageData
            oAutomatedExchangesService = New AutomatedExchanges.AutomatedExchangesService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID.ToString, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oAutomatedExchangesService)


            oUsernameToken = New AutomatedExchanges.SecurityUsernameToken
            oSecurity1 = New AutomatedExchanges.Security1
            oAutomatedExchangesRQ = New AutomatedExchanges.AutomatedExchangesRQ

            obj_Security(Nothing, _
                         objSession.Token.ToString, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oAutomatedExchangesRQ, _
                         True, _
                         oAutomatedExchangesService, _
                         intFirmaGDS)

            '======================================
            oAutomatedExchangesRQ.ReturnHostCommandSpecified = True
            oAutomatedExchangesRQ.ReturnHostCommand = True

            oAutomatedExchangesRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oAutomatedExchangesRQ.TimeStampSpecified = True
            oAutomatedExchangesRQ.Version = "2.3.0"

            '--------------------------------


            'If lstSegmentos IsNot Nothing Then
            '    For i As Integer = 0 To lstSegmentos.Count - 1

            '        If ExchangeSegment Is Nothing Then
            '            ReDim ExchangeSegment(0)
            '        Else
            '            ReDim Preserve ExchangeSegment(i)
            '        End If

            '        ExchangeSegment(ExchangeSegment.Length - 1) = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonExchangeSegment
            '        ExchangeSegment(ExchangeSegment.Length - 1).SegmentNumber = lstSegmentos.Item(0)

            '    Next
            'End If

            If Not String.IsNullOrEmpty(strTourCode) Then
                TourCode = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersMiscQualifiersTourCode
                TourCode.Text = strTourCode

                If MiscQualifiers Is Nothing Then MiscQualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersMiscQualifiers
                MiscQualifiers.TourCode = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersMiscQualifiersTourCode
                MiscQualifiers.TourCode = TourCode
            End If



            If Not String.IsNullOrEmpty(strCodigoAerolinea) Then
                Airline = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersFlightQualifiersVendorPrefsAirline
                Airline.Code = strCodigoAerolinea

                VendorPrefs = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersFlightQualifiersVendorPrefs
                VendorPrefs.Airline = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersFlightQualifiersVendorPrefsAirline
                VendorPrefs.Airline = Airline

                FlightQualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersFlightQualifiers
                FlightQualifiers.VendorPrefs = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersFlightQualifiersVendorPrefs
                FlightQualifiers.VendorPrefs = VendorPrefs
            End If


            If Not String.IsNullOrEmpty(strNumeroParajero) Then
                NameSelect = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiersNameSelect
                NameSelect.NameNumber = strNumeroParajero

                If PricingQualifiers Is Nothing Then PricingQualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiers
                PricingQualifiers.NameSelect = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiersNameSelect
                PricingQualifiers.NameSelect = NameSelect
            End If
            If Not String.IsNullOrEmpty(strAccount) Then
                Account = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiersAccount
                Account.Code = strAccount

                If PricingQualifiers Is Nothing Then PricingQualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiers
                PricingQualifiers.Account = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiersAccount
                PricingQualifiers.Account = Account
            End If
            If Not String.IsNullOrEmpty(strCorporateId) Then
                Corporate = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiersCorporate
                Corporate.ID = strCorporateId

                If PricingQualifiers Is Nothing Then PricingQualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiers
                PricingQualifiers.Corporate = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiersCorporate
                PricingQualifiers.Corporate = Corporate
            End If
            If Not String.IsNullOrEmpty(strTipoTarifa) Then
                FareOptions = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiersFareOptions
                If strTipoTarifa.Equals("PL") Then
                    FareOptions.Public = True
                    FareOptions.PublicSpecified = True
                Else
                    FareOptions.Private = True
                    FareOptions.PrivateSpecified = True
                End If

                If PricingQualifiers Is Nothing Then PricingQualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiers
                PricingQualifiers.FareOptions = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiersFareOptions
                PricingQualifiers.FareOptions = FareOptions
            End If
            If Not String.IsNullOrEmpty(strPassengerType) Then
                PassengerType = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiersPassengerType
                PassengerType.Code = strPassengerType

                If PricingQualifiers Is Nothing Then PricingQualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiers
                PricingQualifiers.PassengerType = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiersPassengerType
                PricingQualifiers.PassengerType = PassengerType
            End If
            If lstSegmentos IsNot Nothing Then
                For i As Integer = 0 To lstSegmentos.Count - 1

                    If SegmentSelect Is Nothing Then
                        ReDim SegmentSelect(0)
                    Else
                        ReDim Preserve SegmentSelect(i)
                    End If

                    SegmentSelect(SegmentSelect.Length - 1) = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiersSegmentSelect
                    SegmentSelect(SegmentSelect.Length - 1).Number = lstSegmentos.Item(0)
                Next

                If SegmentSelect IsNot Nothing Then
                    If PricingQualifiers Is Nothing Then PricingQualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiers
                    PricingQualifiers.ItineraryOptions = SegmentSelect
                End If
            End If


            OptionalQualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiers
            If FlightQualifiers IsNot Nothing Then
                OptionalQualifiers.FlightQualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersFlightQualifiers
                OptionalQualifiers.FlightQualifiers = FlightQualifiers
            End If
            If PricingQualifiers IsNot Nothing Then
                OptionalQualifiers.PricingQualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersPricingQualifiers
                OptionalQualifiers.PricingQualifiers = PricingQualifiers
            End If
            If MiscQualifiers IsNot Nothing Then
                OptionalQualifiers.MiscQualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiersMiscQualifiers
                OptionalQualifiers.MiscQualifiers = MiscQualifiers
            End If


            PriceRequestInformation = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformation
            PriceRequestInformation.OptionalQualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformationOptionalQualifiers
            PriceRequestInformation.OptionalQualifiers = OptionalQualifiers


            ExchangeComparison = New AutomatedExchanges.AutomatedExchangesRQExchangeComparison
            ExchangeComparison.OriginalTicketNumber = strTicketOriginal
            ExchangeComparison.PriceRequestInformation = New AutomatedExchanges.AutomatedExchangesRQExchangeComparisonPriceRequestInformation
            ExchangeComparison.PriceRequestInformation = PriceRequestInformation
            If ExchangeSegment IsNot Nothing Then
                ExchangeComparison.ExchangeSegment = ExchangeSegment
            End If

            oAutomatedExchangesRQ.ExchangeComparison = New AutomatedExchanges.AutomatedExchangesRQExchangeComparison
            oAutomatedExchangesRQ.ExchangeComparison = ExchangeComparison

            '--------------------------------

            oAutomatedExchangesRS = New AutomatedExchanges.AutomatedExchangesRS
            oAutomatedExchangesRS = oAutomatedExchangesService.AutomatedExchangesRQ(oAutomatedExchangesRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(AutomatedExchanges.AutomatedExchangesRQ), _
                                        GetType(AutomatedExchanges.AutomatedExchangesRS), _
                                        oAutomatedExchangesRQ, _
                                        oAutomatedExchangesRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_AutomatedExchanges" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Token : " & objSession.Token.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "ConversationID : " & objSession.ConversationID.ToString & vbCrLf

            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(ex.ToString)
        Finally

            FUNCTION_NAME = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oAutomatedExchangesRQ = Nothing
            oAutomatedExchangesService = Nothing
            oGeneraPayLoadXML = Nothing

        End Try

        Return oAutomatedExchangesRS

    End Function
    Public Function _ExchangeConfirmation(ByVal strPQ As String, _
                                          ByVal strBaggage As String, _
                                          ByVal strCommission As String, _
                                          ByVal FormaPago As classFormaPago, _
                                          ByVal strCodigoSeguimiento As String, _
                                          ByVal intGDS As Integer, _
                                          ByVal intFirmaGDS As Integer, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal objSession As classSession) As AutomatedExchanges.AutomatedExchangesRS

        Const ID_SABRE_WEB_SERVICE As String = "49"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity1 As AutomatedExchanges.Security1 = Nothing

        Dim oUsernameToken As AutomatedExchanges.SecurityUsernameToken = Nothing
        Dim oMessageHeader As AutomatedExchanges.MessageHeader = Nothing

        Dim oFromPartyId As AutomatedExchanges.PartyId = Nothing
        Dim oFrom As AutomatedExchanges.From = Nothing
        Dim oFromPartyIdArr(0) As AutomatedExchanges.PartyId
        Dim oToPartyId As AutomatedExchanges.PartyId = Nothing
        Dim oToPartyIdArr(0) As AutomatedExchanges.PartyId
        Dim oTo As AutomatedExchanges.[To] = Nothing
        Dim oMessageData As AutomatedExchanges.MessageData = Nothing
        Dim oService As AutomatedExchanges.Service = Nothing

        Dim oAutomatedExchangesRQ As AutomatedExchanges.AutomatedExchangesRQ = Nothing
        Dim oAutomatedExchangesRS As AutomatedExchanges.AutomatedExchangesRS = Nothing
        Dim oAutomatedExchangesService As AutomatedExchanges.AutomatedExchangesService = Nothing

        Dim ExchangeConfirmation As AutomatedExchanges.AutomatedExchangesRQExchangeConfirmation = Nothing
        Dim OptionalQualifiers As AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiers = Nothing
        Dim FOP_Qualifiers As AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersFOP_Qualifiers = Nothing
        Dim PaymentCard As AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersFOP_QualifiersBasicFOPCC_InfoPaymentCard = Nothing
        Dim BasicFOP As AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersFOP_QualifiersBasicFOP = Nothing
        Dim MiscQualifiers As AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersMiscQualifiers = Nothing
        Dim BaggageAllowance As AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersMiscQualifiersBaggageAllowance = Nothing
        Dim Commission As AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersMiscQualifiersCommission = Nothing

        '
        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing
        '


        Try
            oMessageHeader = New AutomatedExchanges.MessageHeader
            oFromPartyId = New AutomatedExchanges.PartyId
            oFrom = New AutomatedExchanges.From
            oToPartyId = New AutomatedExchanges.PartyId
            oTo = New AutomatedExchanges.[To]
            oService = New AutomatedExchanges.Service
            oMessageData = New AutomatedExchanges.MessageData
            oAutomatedExchangesService = New AutomatedExchanges.AutomatedExchangesService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID.ToString, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oAutomatedExchangesService)


            oUsernameToken = New AutomatedExchanges.SecurityUsernameToken
            oSecurity1 = New AutomatedExchanges.Security1
            oAutomatedExchangesRQ = New AutomatedExchanges.AutomatedExchangesRQ

            obj_Security(Nothing, _
                         objSession.Token.ToString, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oAutomatedExchangesRQ, _
                         True, _
                         oAutomatedExchangesService, _
                         intFirmaGDS)

            '======================================
            oAutomatedExchangesRQ.ReturnHostCommandSpecified = True
            oAutomatedExchangesRQ.ReturnHostCommand = True

            oAutomatedExchangesRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oAutomatedExchangesRQ.TimeStampSpecified = True
            oAutomatedExchangesRQ.Version = "2.3.0"

            '--------------------------------


            ExchangeConfirmation = New AutomatedExchanges.AutomatedExchangesRQExchangeConfirmation
            ExchangeConfirmation.PQR_Number = strPQ

            BasicFOP = New AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersFOP_QualifiersBasicFOP
            If FormaPago IsNot Nothing Then
                If FormaPago.Tipo = "CA" Then
                    BasicFOP.Type = AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersFOP_QualifiersBasicFOPType.CA
                    BasicFOP.TypeSpecified = True
                ElseIf FormaPago.Tipo = "CC" Then
                    If FormaPago.Tarjeta IsNot Nothing Then

                        BasicFOP.CC_Info = New AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersFOP_QualifiersBasicFOPCC_Info
                        PaymentCard = New AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersFOP_QualifiersBasicFOPCC_InfoPaymentCard
                        PaymentCard.Code = FormaPago.Tarjeta(0).CodigoTarjera
                        PaymentCard.Number = FormaPago.Tarjeta(0).NumTarjeta
                        Dim oDate As New Date
                        oDate = FormaPago.Tarjeta(0).FechaVencimiento
                        PaymentCard.ExpireDate = oDate.ToString("yyyy-MM")
                        PaymentCard.ManualApprovalCode = "123456"

                        BasicFOP.CC_Info.PaymentCard = PaymentCard
                    End If
                End If
            End If

            FOP_Qualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersFOP_Qualifiers
            FOP_Qualifiers.BasicFOP = New AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersFOP_QualifiersBasicFOP
            FOP_Qualifiers.BasicFOP = BasicFOP

            OptionalQualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiers
            OptionalQualifiers.FOP_Qualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersFOP_Qualifiers
            OptionalQualifiers.FOP_Qualifiers = FOP_Qualifiers
            '--
            BaggageAllowance = New AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersMiscQualifiersBaggageAllowance
            BaggageAllowance.Number = strBaggage


            Commission = New AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersMiscQualifiersCommission
            Commission.Percent = strCommission

            MiscQualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersMiscQualifiers

            MiscQualifiers.BaggageAllowance = New AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersMiscQualifiersBaggageAllowance
            MiscQualifiers.BaggageAllowance = BaggageAllowance

            MiscQualifiers.Commission = New AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersMiscQualifiersCommission
            MiscQualifiers.Commission = Commission

            OptionalQualifiers.MiscQualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiersMiscQualifiers
            OptionalQualifiers.MiscQualifiers = MiscQualifiers


            ExchangeConfirmation.OptionalQualifiers = New AutomatedExchanges.AutomatedExchangesRQExchangeConfirmationOptionalQualifiers
            ExchangeConfirmation.OptionalQualifiers = OptionalQualifiers

            oAutomatedExchangesRQ.ExchangeConfirmation = New AutomatedExchanges.AutomatedExchangesRQExchangeConfirmation
            oAutomatedExchangesRQ.ExchangeConfirmation = ExchangeConfirmation
            '--------------------------------

            oAutomatedExchangesRS = New AutomatedExchanges.AutomatedExchangesRS
            oAutomatedExchangesRS = oAutomatedExchangesService.AutomatedExchangesRQ(oAutomatedExchangesRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(AutomatedExchanges.AutomatedExchangesRQ), _
                                        GetType(AutomatedExchanges.AutomatedExchangesRS), _
                                        oAutomatedExchangesRQ, _
                                        oAutomatedExchangesRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_ExchangeConfirmation" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Token : " & objSession.Token.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "ConversationID : " & objSession.ConversationID.ToString & vbCrLf

            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(ex.ToString)
        Finally

            FUNCTION_NAME = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oAutomatedExchangesRQ = Nothing
            oAutomatedExchangesService = Nothing
            oGeneraPayLoadXML = Nothing

            strPQ = Nothing
            strBaggage = Nothing
            strCommission = Nothing
            FormaPago = Nothing
            strCodigoSeguimiento = Nothing
            intGDS = Nothing
            intFirmaGDS = Nothing
            intFirmaDB = Nothing
            objSession = Nothing

        End Try

        Return oAutomatedExchangesRS

    End Function
    Public Function _DisplayPriceQuote(ByVal strCodigoSeguimiento As String, _
                                       ByVal intGDS As Integer, _
                                       ByVal intFirmaGDS As Integer, _
                                       ByVal intFirmaDB As Integer, _
                                       ByVal objSession As classSession) As DisplayPriceQuote.DisplayPriceQuoteRS

        Const ID_SABRE_WEB_SERVICE As String = "53"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity1 As DisplayPriceQuote.Security1 = Nothing

        Dim oUsernameToken As DisplayPriceQuote.SecurityUsernameToken = Nothing
        Dim oMessageHeader As DisplayPriceQuote.MessageHeader = Nothing

        Dim oFromPartyId As DisplayPriceQuote.PartyId = Nothing
        Dim oFrom As DisplayPriceQuote.From = Nothing
        Dim oFromPartyIdArr(0) As DisplayPriceQuote.PartyId
        Dim oToPartyId As DisplayPriceQuote.PartyId = Nothing
        Dim oToPartyIdArr(0) As DisplayPriceQuote.PartyId
        Dim oTo As DisplayPriceQuote.[To] = Nothing
        Dim oMessageData As DisplayPriceQuote.MessageData = Nothing
        Dim oService As DisplayPriceQuote.Service = Nothing

        Dim oDisplayPriceQuoteRQ As DisplayPriceQuote.DisplayPriceQuoteRQ = Nothing
        Dim oDisplayPriceQuoteRS As DisplayPriceQuote.DisplayPriceQuoteRS = Nothing
        Dim oDisplayPriceQuoteService As DisplayPriceQuote.DisplayPriceQuoteService = Nothing
        '
        Dim AirItineraryPricingInfo As DisplayPriceQuote.DisplayPriceQuoteRQAirItineraryPricingInfo = Nothing
        Dim Summary As DisplayPriceQuote.DisplayPriceQuoteRQAirItineraryPricingInfoSummary = Nothing
        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing
        '


        Try
            oMessageHeader = New DisplayPriceQuote.MessageHeader
            oFromPartyId = New DisplayPriceQuote.PartyId
            oFrom = New DisplayPriceQuote.From
            oToPartyId = New DisplayPriceQuote.PartyId
            oTo = New DisplayPriceQuote.[To]
            oService = New DisplayPriceQuote.Service
            oMessageData = New DisplayPriceQuote.MessageData
            oDisplayPriceQuoteService = New DisplayPriceQuote.DisplayPriceQuoteService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID.ToString, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oDisplayPriceQuoteService)


            oUsernameToken = New DisplayPriceQuote.SecurityUsernameToken
            oSecurity1 = New DisplayPriceQuote.Security1
            oDisplayPriceQuoteRQ = New DisplayPriceQuote.DisplayPriceQuoteRQ

            obj_Security(Nothing, _
                         objSession.Token.ToString, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oDisplayPriceQuoteRQ, _
                         True, _
                         oDisplayPriceQuoteService, _
                         intFirmaGDS)

            '======================================
            oDisplayPriceQuoteRQ.ReturnHostCommandSpecified = True
            oDisplayPriceQuoteRQ.ReturnHostCommand = True

            oDisplayPriceQuoteRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oDisplayPriceQuoteRQ.TimeStampSpecified = True
            oDisplayPriceQuoteRQ.Version = "2.5.0"

            '--------------------------------

            AirItineraryPricingInfo = New DisplayPriceQuote.DisplayPriceQuoteRQAirItineraryPricingInfo


            'Summary = New DisplayPriceQuote.DisplayPriceQuoteRQAirItineraryPricingInfoSummary
            'Summary.Ind = True


            'AirItineraryPricingInfo.Summary = New DisplayPriceQuote.DisplayPriceQuoteRQAirItineraryPricingInfoSummary
            'AirItineraryPricingInfo.Summary = Summary
            AirItineraryPricingInfo.Reissue = True
            AirItineraryPricingInfo.ReissueSpecified = True

            oDisplayPriceQuoteRQ.AirItineraryPricingInfo = New DisplayPriceQuote.DisplayPriceQuoteRQAirItineraryPricingInfo
            oDisplayPriceQuoteRQ.AirItineraryPricingInfo = AirItineraryPricingInfo

            oDisplayPriceQuoteRS = New DisplayPriceQuote.DisplayPriceQuoteRS
            oDisplayPriceQuoteRS = oDisplayPriceQuoteService.DisplayPriceQuoteRQ(oDisplayPriceQuoteRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(DisplayPriceQuote.DisplayPriceQuoteRQ), _
                                        GetType(DisplayPriceQuote.DisplayPriceQuoteRS), _
                                        oDisplayPriceQuoteRQ, _
                                        oDisplayPriceQuoteRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_DisplayPriceQuote" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Token : " & objSession.Token.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "ConversationID : " & objSession.ConversationID.ToString & vbCrLf

            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(ex.ToString)
        Finally

            FUNCTION_NAME = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oDisplayPriceQuoteRQ = Nothing
            oDisplayPriceQuoteService = Nothing
            oGeneraPayLoadXML = Nothing

        End Try

        Return oDisplayPriceQuoteRS

    End Function
    Public Function _ExchangeShopping(ByVal strCodigoAerolinea As String, _
                                      ByVal strNumeroParajero As String, _
                                      ByVal strTicketOriginal As String, _
                                      ByVal lstSegmentos As List(Of String),
                                      ByVal strTourCode As String,
                                      ByVal strAccount As String,
                                      ByVal strCorporateId As String,
                                      ByVal strTipoTarifa As String,
                                      ByVal strPassengerType As String,
                                      ByVal strCodigoSeguimiento As String, _
                                      ByVal intGDS As Integer, _
                                      ByVal intFirmaGDS As Integer, _
                                      ByVal intFirmaDB As Integer, _
                                      ByVal objSession As classSession) As ExchangeShopping.ExchangeShoppingRS

        Const ID_SABRE_WEB_SERVICE As String = "50"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity1 As ExchangeShopping.Security1 = Nothing

        Dim oUsernameToken As ExchangeShopping.SecurityUsernameToken = Nothing
        Dim oMessageHeader As ExchangeShopping.MessageHeader = Nothing

        Dim oFromPartyId As ExchangeShopping.PartyId = Nothing
        Dim oFrom As ExchangeShopping.From = Nothing
        Dim oFromPartyIdArr(0) As ExchangeShopping.PartyId
        Dim oToPartyId As ExchangeShopping.PartyId = Nothing
        Dim oToPartyIdArr(0) As ExchangeShopping.PartyId
        Dim oTo As ExchangeShopping.[To] = Nothing
        Dim oMessageData As ExchangeShopping.MessageData = Nothing
        Dim oService As ExchangeShopping.Service = Nothing

        Dim oExchangeShoppingRQ As ExchangeShopping.ExchangeShoppingRQ = Nothing
        Dim oExchangeShoppingRS As ExchangeShopping.ExchangeShoppingRS = Nothing
        Dim oExchangeShoppingService As ExchangeShopping.ExchangeShoppingService = Nothing

        Dim ExchangeComparison As ExchangeShopping.ExchangeShoppingRQExchangeComparison = Nothing
        Dim ExchangeSegment As ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegment = Nothing
        Dim PriceRequestInformation As ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformation = Nothing
        Dim OptionalQualifiers As ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiers = Nothing
        Dim PricingQualifiers As ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiers = Nothing
        Dim Account As ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiersAccount = Nothing
        Dim Corporate As ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiersCorporate = Nothing
        Dim FareOptions As ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiersFareOptions = Nothing
        Dim SegmentSelect As ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiersSegmentSelect() = Nothing
        Dim NameSelect As ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiersNameSelect = Nothing
        Dim PassengerType As ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiersPassengerType = Nothing

        '
        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing
        '


        Try
            oMessageHeader = New ExchangeShopping.MessageHeader
            oFromPartyId = New ExchangeShopping.PartyId
            oFrom = New ExchangeShopping.From
            oToPartyId = New ExchangeShopping.PartyId
            oTo = New ExchangeShopping.[To]
            oService = New ExchangeShopping.Service
            oMessageData = New ExchangeShopping.MessageData
            oExchangeShoppingService = New ExchangeShopping.ExchangeShoppingService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID.ToString, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oExchangeShoppingService)


            oUsernameToken = New ExchangeShopping.SecurityUsernameToken
            oSecurity1 = New ExchangeShopping.Security1
            oExchangeShoppingRQ = New ExchangeShopping.ExchangeShoppingRQ

            obj_Security(Nothing, _
                         objSession.Token.ToString, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oExchangeShoppingRQ, _
                         True, _
                         oExchangeShoppingService, _
                         intFirmaGDS)

            '======================================
            oExchangeShoppingRQ.ReturnHostCommandSpecified = True
            oExchangeShoppingRQ.ReturnHostCommand = True

            oExchangeShoppingRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oExchangeShoppingRQ.TimeStampSpecified = True
            oExchangeShoppingRQ.Version = "2.0.1"

            '--------------------------------
            ExchangeComparison = New ExchangeShopping.ExchangeShoppingRQExchangeComparison
            ExchangeComparison.OriginalTicketNumber = strTicketOriginal

            PricingQualifiers = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiers
            PricingQualifiers.CurrencyCode = "USD"
            PricingQualifiers.NumOptions = "19"

            If Not String.IsNullOrEmpty(strAccount) Then
                Account = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiersAccount
                Account.Code = strAccount

                PricingQualifiers.Account = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiersAccount
                PricingQualifiers.Account = Account
            End If
            If Not String.IsNullOrEmpty(strCorporateId) Then
                Corporate = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiersCorporate
                Corporate.ID = strCorporateId

                PricingQualifiers.Corporate = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiersCorporate
                PricingQualifiers.Corporate = Corporate
            End If
            If Not String.IsNullOrEmpty(strTipoTarifa) Then
                FareOptions = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiersFareOptions
                If strTipoTarifa.Equals("PL") Then
                    FareOptions.Public = True
                    FareOptions.PublicSpecified = True
                Else
                    FareOptions.Private = True
                    FareOptions.PrivateSpecified = True
                End If

                PricingQualifiers.FareOptions = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiersFareOptions
                PricingQualifiers.FareOptions = FareOptions
            End If
            If lstSegmentos IsNot Nothing Then
                For i As Integer = 0 To lstSegmentos.Count - 1

                    If SegmentSelect Is Nothing Then
                        ReDim SegmentSelect(0)
                    Else
                        ReDim Preserve SegmentSelect(SegmentSelect.Length)
                    End If

                    SegmentSelect(SegmentSelect.Length - 1) = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiersSegmentSelect
                    SegmentSelect(SegmentSelect.Length - 1).Number = lstSegmentos.Item(i).ToString
                Next

                If SegmentSelect IsNot Nothing Then
                    PricingQualifiers.ItineraryOptions = SegmentSelect
                End If
            End If
            If Not String.IsNullOrEmpty(strNumeroParajero) Then
                NameSelect = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiersNameSelect
                NameSelect.NameNumber = strNumeroParajero

                PricingQualifiers.NameSelect = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiersNameSelect
                PricingQualifiers.NameSelect = NameSelect
            End If
            If Not String.IsNullOrEmpty(strPassengerType) Then
                PassengerType = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiersPassengerType
                PassengerType.Code = strPassengerType

                PricingQualifiers.PassengerType = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiersPassengerType
                PricingQualifiers.PassengerType = PassengerType
            End If




            If PricingQualifiers IsNot Nothing Then
                OptionalQualifiers = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiers
                OptionalQualifiers.PricingQualifiers = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiersPricingQualifiers
                OptionalQualifiers.PricingQualifiers = PricingQualifiers
            End If

            If OptionalQualifiers IsNot Nothing Then
                PriceRequestInformation = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformation
                PriceRequestInformation.OptionalQualifiers = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformationOptionalQualifiers
                PriceRequestInformation.OptionalQualifiers = OptionalQualifiers
            End If

            If PriceRequestInformation IsNot Nothing Then
                ExchangeSegment = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegment
                ExchangeSegment.PriceRequestInformation = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegmentPriceRequestInformation
                ExchangeSegment.PriceRequestInformation = PriceRequestInformation
            End If


            If ExchangeSegment IsNot Nothing Then
                ExchangeComparison.ExchangeSegment = New ExchangeShopping.ExchangeShoppingRQExchangeComparisonExchangeSegment
                ExchangeComparison.ExchangeSegment = ExchangeSegment
            End If


            oExchangeShoppingRQ.ExchangeComparison = New ExchangeShopping.ExchangeShoppingRQExchangeComparison
            oExchangeShoppingRQ.ExchangeComparison = ExchangeComparison

            '--------------------------------
            oExchangeShoppingRS = New ExchangeShopping.ExchangeShoppingRS
            oExchangeShoppingRS = oExchangeShoppingService.ExchangeShoppingRQ(oExchangeShoppingRQ)

            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(ExchangeShopping.ExchangeShoppingRQ), _
                                        GetType(ExchangeShopping.ExchangeShoppingRS), _
                                        oExchangeShoppingRQ, _
                                        oExchangeShoppingRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)

        Catch ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_AutomatedExchanges" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Token : " & objSession.Token.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "ConversationID : " & objSession.ConversationID.ToString & vbCrLf

            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(ex.ToString)
        Finally

            FUNCTION_NAME = Nothing
            oSecurity1 = Nothing
            oUsernameToken = Nothing
            oMessageHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            oMessageData = Nothing
            oService = Nothing
            oExchangeShoppingRQ = Nothing
            oExchangeShoppingService = Nothing
            oGeneraPayLoadXML = Nothing

        End Try

        Return oExchangeShoppingRS

    End Function
    'Public Function _OTA_AirLowFareSearch(ByVal strCodigoSeguimiento As String, _
    '                                       ByVal intGDS As Integer, _
    '                                       ByVal intFirmaGDS As Integer, _
    '                                       ByVal intFirmaDB As Integer, _
    '                                       ByVal objSession As classSession,
    '                                       ByVal strciudadOrigen As String, _
    '                                       ByVal strciudadDestino As String, _
    '                                       ByVal strFechaInicioViaje As String, _
    '                                       ByVal strFechaFinViaje As String, _
    '                                       ByVal strCod_Aerolinea As String, _
    '                                       ByVal strClase As String, _
    '                                       ByVal Orientacion As String, _
    '                                       ByVal oTipoPax As List(Of classTipoPaxVuelo),
    '                                       ByVal strTipoTarifa As String) As OTA_AirLowFareSearch230.OTA_AirLowFareSearchRS

    '    Const ID_SABRE_WEB_SERVICE As String = "55"
    '    Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
    '    '
    '    Dim oSecurity1 As OTA_AirLowFareSearch230.Security1 = Nothing
    '    Dim oUsernameToken As OTA_AirLowFareSearch230.SecurityUsernameToken = Nothing
    '    Dim oMessageHeader As OTA_AirLowFareSearch230.MessageHeader = Nothing
    '    Dim oFromPartyId As OTA_AirLowFareSearch230.PartyId = Nothing
    '    Dim oFrom As OTA_AirLowFareSearch230.From = Nothing
    '    Dim oFromPartyIdArr(0) As OTA_AirLowFareSearch230.PartyId
    '    Dim oToPartyId As New OTA_AirLowFareSearch230.PartyId
    '    Dim oToPartyIdArr(0) As OTA_AirLowFareSearch230.PartyId
    '    Dim oTo As OTA_AirLowFareSearch230.[To] = Nothing
    '    Dim oMessageData As OTA_AirLowFareSearch230.MessageData = Nothing
    '    Dim oService As OTA_AirLowFareSearch230.Service = Nothing
    '    '

    '    Dim oOTA_AirLowFareSearchRQ As OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQ = Nothing 'Request
    '    Dim oOTA_AirLowFareSearchService As OTA_AirLowFareSearch230.OTA_AirLowFareSearchService = Nothing
    '    Dim oOTA_AirLowFareSearchRS As OTA_AirLowFareSearch230.OTA_AirLowFareSearchRS = Nothing 'Response
    '    Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing

    '    'JMATTO PARAMETROS
    '    Dim oOriginDestinationInformation() As OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQOriginDestinationInformation = Nothing
    '    Dim oFlightSegment As OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQOriginDestinationInformationFlightSegment = Nothing
    '    Dim oPriceRequestInformation As New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQPriceRequestInformation
    '    Dim response_OTA_AirLowFareSearchRSPricedItinerary As OTA_AirLowFareSearch230.OTA_AirLowFareSearchRSPricedItinerary = Nothing
    '    Dim Segmentos As Integer = 0
    '    Try

    '        oMessageHeader = New OTA_AirLowFareSearch230.MessageHeader
    '        oFromPartyId = New OTA_AirLowFareSearch230.PartyId
    '        oFrom = New OTA_AirLowFareSearch230.From
    '        oToPartyId = New OTA_AirLowFareSearch230.PartyId
    '        oTo = New OTA_AirLowFareSearch230.[To]
    '        oService = New OTA_AirLowFareSearch230.Service
    '        oMessageData = New OTA_AirLowFareSearch230.MessageData
    '        oOTA_AirLowFareSearchService = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchService

    '        obj_Cabecera(ID_SABRE_WEB_SERVICE, _
    '                     oMessageHeader, _
    '                     objSession.ConversationID, _
    '                     oFromPartyId, _
    '                     oFromPartyIdArr, _
    '                     oFrom, _
    '                     oToPartyId, _
    '                     oToPartyIdArr, _
    '                     oTo, _
    '                     Nothing, _
    '                     oService, _
    '                     oMessageData, _
    '                     oOTA_AirLowFareSearchService)


    '        oUsernameToken = New OTA_AirLowFareSearch230.SecurityUsernameToken
    '        oSecurity1 = New OTA_AirLowFareSearch230.Security1
    '        oOTA_AirLowFareSearchRQ = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQ

    '        obj_Security(Nothing, _
    '                     objSession.Token, _
    '                     oUsernameToken, _
    '                     oSecurity1, _
    '                     Nothing, _
    '                     Nothing, _
    '                     oOTA_AirLowFareSearchRQ, _
    '                     True, _
    '                     oOTA_AirLowFareSearchService, _
    '                     intFirmaGDS)
    '        oOTA_AirLowFareSearchRQ.TimeStamp = Constantes.IWS_TIMESTAMP
    '        oOTA_AirLowFareSearchRQ.Version = "2.3.0"

    '        '------------------------------------------
    '        If Orientacion = "O" Then
    '            Segmentos = 1
    '        Else
    '            Segmentos = 2
    '        End If

    '        ReDim oOriginDestinationInformation(Segmentos - 1)
    '        For i As Integer = 0 To Segmentos - 1
    '            ReDim Preserve oOriginDestinationInformation(i)
    '            oOriginDestinationInformation(oOriginDestinationInformation.Length - 1) = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQOriginDestinationInformation
    '            With oOriginDestinationInformation(oOriginDestinationInformation.Length - 1)
    '                .RPH = CStr(i + 1) ' "1"                    
    '                oFlightSegment = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQOriginDestinationInformationFlightSegment
    '                oFlightSegment.OriginLocation = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQOriginDestinationInformationFlightSegmentOriginLocation
    '                oFlightSegment.OriginLocation.LocationCode = strciudadOrigen

    '                oFlightSegment.DestinationLocation = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQOriginDestinationInformationFlightSegmentDestinationLocation
    '                oFlightSegment.DestinationLocation.LocationCode = strciudadDestino
    '                oFlightSegment.DepartureDateTime = strFechaInicioViaje
    '                oFlightSegment.DepartureDateTimeSpecified = True
    '                oFlightSegment.ResBookDesigCode = strClase
    '                ReDim Preserve .FlightSegment(i)
    '                .FlightSegment(i) = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQOriginDestinationInformationFlightSegment
    '                .FlightSegment(i) = oFlightSegment

    '                'EN EL CASO QUE SEA ROUND TRIP
    '                strciudadOrigen = .FlightSegment(i).DestinationLocation.LocationCode
    '                strciudadDestino = .FlightSegment(i).OriginLocation.LocationCode
    '                strFechaInicioViaje = strFechaFinViaje
    '            End With
    '        Next

    '        oPriceRequestInformation.OptionalQualifiers = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQPriceRequestInformationOptionalQualifiers
    '        oPriceRequestInformation.OptionalQualifiers.FlightQualifiers = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQPriceRequestInformationOptionalQualifiersFlightQualifiers
    '        oPriceRequestInformation.OptionalQualifiers.FlightQualifiers.AvailableFlightsOnly = False 'Tarifas Disponible y no Disponibles
    '        oPriceRequestInformation.OptionalQualifiers.FlightQualifiers.AvailableFlightsOnlySpecified = False
    '        oPriceRequestInformation.OptionalQualifiers.FlightQualifiers.NumStops = "2" 'Número Paradas
    '        oPriceRequestInformation.OptionalQualifiers.FlightQualifiers.OnlineOnly = False
    '        oPriceRequestInformation.OptionalQualifiers.FlightQualifiers.OnlineOnlySpecified = False

    '        ReDim Preserve oPriceRequestInformation.OptionalQualifiers.FlightQualifiers.VendorPrefs(0)
    '        oPriceRequestInformation.OptionalQualifiers.FlightQualifiers.VendorPrefs(0) = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQPriceRequestInformationOptionalQualifiersFlightQualifiersVendorPrefs
    '        oPriceRequestInformation.OptionalQualifiers.FlightQualifiers.VendorPrefs(0).Exclude = False
    '        oPriceRequestInformation.OptionalQualifiers.FlightQualifiers.VendorPrefs(0).ExcludeSpecified = False

    '        ReDim Preserve oPriceRequestInformation.OptionalQualifiers.FlightQualifiers.VendorPrefs(0).Airline(0)
    '        oPriceRequestInformation.OptionalQualifiers.FlightQualifiers.VendorPrefs(0).Airline(0) = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQPriceRequestInformationOptionalQualifiersFlightQualifiersVendorPrefsAirline
    '        oPriceRequestInformation.OptionalQualifiers.FlightQualifiers.VendorPrefs(0).Airline(0).RPH = "1"
    '        oPriceRequestInformation.OptionalQualifiers.FlightQualifiers.VendorPrefs(0).Airline(0).Code = strCod_Aerolinea

    '        oPriceRequestInformation.OptionalQualifiers.MiscQualifiers = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQPriceRequestInformationOptionalQualifiersMiscQualifiers
    '        oPriceRequestInformation.OptionalQualifiers.MiscQualifiers.Priority = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQPriceRequestInformationOptionalQualifiersMiscQualifiersPriority
    '        oPriceRequestInformation.OptionalQualifiers.MiscQualifiers.Priority.Price = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQPriceRequestInformationOptionalQualifiersMiscQualifiersPriorityPrice
    '        oPriceRequestInformation.OptionalQualifiers.MiscQualifiers.Priority.Price.Priority = "1"
    '        oPriceRequestInformation.OptionalQualifiers.MiscQualifiers.Priority.DirectFlights = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQPriceRequestInformationOptionalQualifiersMiscQualifiersPriorityDirectFlights
    '        oPriceRequestInformation.OptionalQualifiers.MiscQualifiers.Priority.DirectFlights.Priority = "2"
    '        oPriceRequestInformation.OptionalQualifiers.MiscQualifiers.Priority.Time = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQPriceRequestInformationOptionalQualifiersMiscQualifiersPriorityTime
    '        oPriceRequestInformation.OptionalQualifiers.MiscQualifiers.Priority.Time.Priority = "3"
    '        oPriceRequestInformation.OptionalQualifiers.MiscQualifiers.Priority.Vendor = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQPriceRequestInformationOptionalQualifiersMiscQualifiersPriorityVendor
    '        oPriceRequestInformation.OptionalQualifiers.MiscQualifiers.Priority.Vendor.Priority = "4"

    '        oPriceRequestInformation.OptionalQualifiers.PricingQualifiers = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQPriceRequestInformationOptionalQualifiersPricingQualifiers
    '        oPriceRequestInformation.OptionalQualifiers.PricingQualifiers.CurrencyCode = "USD"
    '        'TIPO DE PASAJERO
    '        Dim contador As Integer = 0

    '        For Each aux As classTipoPaxVuelo In oTipoPax
    '            ReDim Preserve oPriceRequestInformation.OptionalQualifiers.PricingQualifiers.PassengerType(contador)
    '            oPriceRequestInformation.OptionalQualifiers.PricingQualifiers.PassengerType(contador) = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQPriceRequestInformationOptionalQualifiersPricingQualifiersPassengerType
    '            oPriceRequestInformation.OptionalQualifiers.PricingQualifiers.PassengerType(contador).Code = aux.IdTipoDePax
    '            oPriceRequestInformation.OptionalQualifiers.PricingQualifiers.PassengerType(contador).Quantity = "1"
    '            If strCod_Aerolinea.Equals("CM") And strTipoTarifa.Equals("PV") Then
    '                ReDim Preserve oPriceRequestInformation.OptionalQualifiers.PricingQualifiers.Account(contador)
    '                oPriceRequestInformation.OptionalQualifiers.PricingQualifiers.Account(contador) = "PEV814"
    '            End If
    '            contador += 1
    '        Next

    '        oPriceRequestInformation.OptionalQualifiers.PricingQualifiers.FareOptions = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQPriceRequestInformationOptionalQualifiersPricingQualifiersFareOptions
    '        If (strTipoTarifa.Equals("PV")) Then
    '            oPriceRequestInformation.OptionalQualifiers.PricingQualifiers.FareOptions.Private = True
    '            oPriceRequestInformation.OptionalQualifiers.PricingQualifiers.FareOptions.PrivateSpecified = True
    '        ElseIf (strTipoTarifa.Equals("PL")) Then
    '            oPriceRequestInformation.OptionalQualifiers.PricingQualifiers.FareOptions.Public = True
    '            oPriceRequestInformation.OptionalQualifiers.PricingQualifiers.FareOptions.PublicSpecified = True
    '        End If

    '        '------------------------------------------
    '        With oOTA_AirLowFareSearchRQ
    '            .OriginDestinationInformation = oOriginDestinationInformation
    '            .PriceRequestInformation = oPriceRequestInformation
    '        End With
    '        '------------------------------------------
    '        oOTA_AirLowFareSearchRS = oOTA_AirLowFareSearchService.OTA_AirLowFareSearchRQ(oOTA_AirLowFareSearchRQ)
    '        oGeneraPayLoadXML = New GeneraPayLoad
    '        oGeneraPayLoadXML.Serialize(GetType(OTA_AirLowFareSearch230.OTA_AirLowFareSearchRQ), _
    '                                    GetType(OTA_AirLowFareSearch230.OTA_AirLowFareSearchRS), _
    '                                    oOTA_AirLowFareSearchRQ, _
    '                                    oOTA_AirLowFareSearchRS, _
    '                                    FUNCTION_NAME, _
    '                                    strCodigoSeguimiento, _
    '                                    False, _
    '                                    False)
    '        If Not oOTA_AirLowFareSearchRS.PricedItineraries Is Nothing Then
    '            response_OTA_AirLowFareSearchRSPricedItinerary = (From c In oOTA_AirLowFareSearchRS.PricedItineraries
    '                                                                Order By Convert.ToDecimal(c.TotalAmount) Ascending
    '                                                                Select c).First
    '            oOTA_AirLowFareSearchRS = Nothing
    '            oOTA_AirLowFareSearchRS = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRS
    '            ReDim Preserve oOTA_AirLowFareSearchRS.PricedItineraries(0)
    '            oOTA_AirLowFareSearchRS.PricedItineraries(0) = New OTA_AirLowFareSearch230.OTA_AirLowFareSearchRSPricedItinerary
    '            oOTA_AirLowFareSearchRS.PricedItineraries(0) = response_OTA_AirLowFareSearchRSPricedItinerary
    '        End If

    '    Catch Ex As Exception

    '        strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
    '        strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
    '        strLog &= Constantes.TabEspacios & "Source : " & "_OTA_AirLowFareSearch" & vbCrLf
    '        strLog &= Constantes.TabEspacios & "Message : " & Ex.ToString & vbCrLf
    '        objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
    '        Throw New Exception(Ex.ToString)
    '    Finally
    '        strCodigoSeguimiento = Nothing
    '        intGDS = Nothing
    '        objSession = Nothing
    '        intFirmaGDS = Nothing

    '        oSecurity1 = Nothing
    '        oUsernameToken = Nothing
    '        oMessageHeader = Nothing
    '        oFromPartyId = Nothing
    '        oFrom = Nothing
    '        oFromPartyIdArr = Nothing
    '        oToPartyId = Nothing
    '        oToPartyIdArr = Nothing
    '        oTo = Nothing
    '        oMessageData = Nothing
    '        oService = Nothing
    '        oOTA_AirLowFareSearchService = Nothing
    '        oOTA_AirLowFareSearchRQ = Nothing
    '    End Try

    '    Return oOTA_AirLowFareSearchRS

    'End Function
    Public Function OTA_AirLowFareSearch_1_13_1(ByVal strCodigoSeguimiento As String, _
                                           ByVal objSession As classSession,
                                           ByVal strciudadOrigen As String, _
                                           ByVal strciudadDestino As String, _
                                           ByVal strFechaInicioViaje As String, _
                                           ByVal strFechaFinViaje As String, _
                                           ByVal strCod_Aerolinea As String, _
                                           ByVal strClase As String, _
                                           ByVal Orientacion As String, _
                                           ByVal oTipoPax() As String,
                                           ByVal strTipoTarifa As String) As OTA_AirLowFareSearch.OTA_AirLowFareSearchRS
        Const ID_SABRE_WEB_SERVICE As String = "03"
        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name
        Dim oGeneraPayLoadXML As GeneraPayLoad = Nothing
        Dim oSecurity As New OTA_AirLowFareSearch.Security
        Dim oPos As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPOS
        Dim oSource As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPOSSource
        Dim oUsernameToken As New OTA_AirLowFareSearch.SecurityUsernameToken
        Dim omsgHeader As New OTA_AirLowFareSearch.MessageHeader
        Dim oFromPartyId As New OTA_AirLowFareSearch.PartyId
        Dim oFrom As New OTA_AirLowFareSearch.From
        Dim oFromPartyIdArr(0) As OTA_AirLowFareSearch.PartyId
        Dim oToPartyId As New OTA_AirLowFareSearch.PartyId
        Dim oToPartyIdArr(0) As OTA_AirLowFareSearch.PartyId
        Dim oTo As New OTA_AirLowFareSearch.[To]
        Dim omsgData As New OTA_AirLowFareSearch.MessageData
        Dim oService As New OTA_AirLowFareSearch.Service
        '
        Dim oAirLowFareSearchRQ As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQ
        Dim oAirLowFareSearchRQService As New OTA_AirLowFareSearch.OTA_AirLowFareSearchService
        Dim oAirLowFareSearchRS As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRS
        '
        Dim oOriginDestinationInformation() As OTA_AirLowFareSearch.OTA_AirLowFareSearchRQOriginDestinationInformation = Nothing
        Dim oOriginLocation As OTA_AirLowFareSearch.OTA_AirLowFareSearchRQOriginDestinationInformationOriginLocation
        Dim oDestinationLocation As OTA_AirLowFareSearch.OTA_AirLowFareSearchRQOriginDestinationInformationDestinationLocation
        Dim oOriginDestinationInformationTPA_Extensions As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQOriginDestinationInformationTPA_Extensions
        Dim oTPA_ExtensionsSegmentType As OTA_AirLowFareSearch.OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentType

        Dim oTravelPreferences As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQTravelPreferences
        Dim oTravelPreferencesVendorPref() As OTA_AirLowFareSearch.OTA_AirLowFareSearchRQTravelPreferencesVendorPref = Nothing
        Dim oTravelPreferencesCabinPref() As OTA_AirLowFareSearch.OTA_AirLowFareSearchRQTravelPreferencesCabinPref = Nothing
        Dim oTravelPreferencesTPA_Extensions As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQTravelPreferencesTPA_Extensions
        Dim oTravelPreferencesTPA_ExtensionsNumTrips As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsNumTrips
        Dim oTravelPreferencesTPA_ExtensionsOnlineIndicator As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQTravelPreferencesTPA_ExtensionsOnlineIndicator


        Dim oTravelerInformation() As OTA_AirLowFareSearch.OTA_AirLowFareSearchRQTravelerInformation
        Dim oPassengerTypeQuantity() As OTA_AirLowFareSearch.OTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity = Nothing

        Dim oPriceRequestInformation As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPriceRequestInformation
        Dim oPriceRequestInformationTPA_Extensions As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPriceRequestInformationTPA_Extensions
        Dim oPrivateFare As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPrivateFare
        Dim oPublicFare As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPublicFare
        Dim oFareCalc As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsFareCalc
        Dim oFareCalcFareBasis As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsFareCalcFareBasis

        Dim oPriceRequestInformationTPA_ExtensionsPriority As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriority
        Dim oPriorityPrice As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityPrice
        Dim oPriorityDirectFlights As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityDirectFlights
        Dim oPriorityTime As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityTime
        Dim oPriorityVendor As New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityVendor

        Dim Segmentos As Integer = 0

        Dim oAuxAerolinea As String = Nothing


        Try
            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         omsgHeader, _
                         objSession.ConversationID, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         omsgData, _
                         oAirLowFareSearchRQService)
            obj_Security(Nothing, _
                         objSession.Token, _
                         oUsernameToken, _
                         oSecurity, _
                         Nothing, _
                         Nothing, _
                         oAirLowFareSearchRQ, _
                         True, _
                         oAirLowFareSearchRQService, _
                         "0")
            'obj_Security(oUsernameToken, oSecurity, oAirLowFareSearchRQ, oAirLowFareSearchRQService, sPCC, oSource, oPos, sToken, True, oSabreWebService.List(ID_SABRE_WEB_SERVICE).Version)
            '
            If Orientacion = "O" Then
                Segmentos = 1
            Else
                Segmentos = 2
            End If

            For i As Integer = 0 To Segmentos - 1
                ReDim Preserve oOriginDestinationInformation(i)
                oOriginDestinationInformation(oOriginDestinationInformation.Length - 1) = New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQOriginDestinationInformation
                With oOriginDestinationInformation(oOriginDestinationInformation.Length - 1)
                    .RPH = CStr(i + 1) ' "1"
                    oOriginLocation = New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQOriginDestinationInformationOriginLocation
                    oOriginLocation.LocationCode = strciudadOrigen ' "LIM" '& IWS_CROSS_OF_LORRAINE & "A"
                    .OriginLocation = oOriginLocation
                    .DepartureDateTime = strFechaInicioViaje '& "T13:00:00" '"2012-08-24T13:00:00"
                    oDestinationLocation = New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQOriginDestinationInformationDestinationLocation
                    oDestinationLocation.LocationCode = strciudadDestino '"BUE"
                    .DestinationLocation = oDestinationLocation
                    oTPA_ExtensionsSegmentType = New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentType
                    oTPA_ExtensionsSegmentType.Code = GDS_NM_WebServicesSabre.OTA_AirLowFareSearch.OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsSegmentTypeCode.O
                    oTPA_ExtensionsSegmentType.CodeSpecified = True
                    oOriginDestinationInformationTPA_Extensions.SegmentType = oTPA_ExtensionsSegmentType
                    oOriginDestinationInformationTPA_Extensions.WithoutAvail = New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQOriginDestinationInformationTPA_ExtensionsWithoutAvail
                    oOriginDestinationInformationTPA_Extensions.WithoutAvail.Ind = False
                    oOriginDestinationInformationTPA_Extensions.WithoutAvail.IndSpecified = False
                    .TPA_Extensions = oOriginDestinationInformationTPA_Extensions

                    strciudadOrigen = oDestinationLocation.LocationCode
                    strciudadDestino = oOriginLocation.LocationCode
                    strFechaInicioViaje = strFechaFinViaje
                End With

                ReDim Preserve oTravelPreferencesCabinPref(i)
                oTravelPreferencesCabinPref(oTravelPreferencesCabinPref.Length - 1) = New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQTravelPreferencesCabinPref
                oTravelPreferencesCabinPref(oTravelPreferencesCabinPref.Length - 1).Code = "Y"
                oTravelPreferencesCabinPref(oTravelPreferencesCabinPref.Length - 1).RPH = CStr(i + 1) '"1"

                ReDim Preserve oTravelPreferencesVendorPref(i)
                oTravelPreferencesVendorPref(oTravelPreferencesVendorPref.Length - 1) = New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQTravelPreferencesVendorPref
                'INGRESAMOS MAS AEROLINEAS
                oAuxAerolinea = strCod_Aerolinea 'IIf(strCod_Aerolinea = "TA" Or strCod_Aerolinea = "LR", "TALR", IIf(strCod_Aerolinea = "LP" Or strCod_Aerolinea = "LA", "LPLA", strCod_Aerolinea))
                oTravelPreferencesVendorPref(oTravelPreferencesVendorPref.Length - 1).Code = oAuxAerolinea
                oTravelPreferencesVendorPref(oTravelPreferencesVendorPref.Length - 1).RPH = CStr(i + 1) '"1"
            Next

            With oTravelPreferencesTPA_Extensions
                oTravelPreferencesTPA_ExtensionsNumTrips.Number = "19"
                .NumTrips = oTravelPreferencesTPA_ExtensionsNumTrips
                oTravelPreferencesTPA_ExtensionsOnlineIndicator.Ind = False 'true
                .OnlineIndicator = oTravelPreferencesTPA_ExtensionsOnlineIndicator

            End With

            With oTravelPreferences
                .CabinPref = oTravelPreferencesCabinPref
                .VendorPref = oTravelPreferencesVendorPref
                .TPA_Extensions = oTravelPreferencesTPA_Extensions
                .MaxStopsQuantity = "2"
            End With

            ReDim oTravelerInformation(0)
            oTravelerInformation(0) = New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQTravelerInformation
            'ReDim oTravelerInformation(oTipoPax.Length - 1)

            With oTravelerInformation(0)
                ReDim oPassengerTypeQuantity(oTipoPax.Count - 1)
                For i As Integer = 0 To oTipoPax.Length - 1
                    oPassengerTypeQuantity(i) = New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQTravelerInformationPassengerTypeQuantity
                    oPassengerTypeQuantity(i).Code = oTipoPax(i)
                    oPassengerTypeQuantity(i).Quantity = "1"
                Next
                .PassengerTypeQuantity = oPassengerTypeQuantity
            End With
            With oPriceRequestInformation
                .CurrencyCode = "USD"
                If strTipoTarifa.Equals("PV") Then
                    oPrivateFare.Ind = True
                    oPrivateFare.IndSpecified = True
                    oPriceRequestInformationTPA_Extensions.PrivateFare = oPrivateFare
                Else
                    oPublicFare.Ind = True
                    oPublicFare.IndSpecified = True
                    oPriceRequestInformationTPA_Extensions.PublicFare = oPublicFare
                End If
                oFareCalc.Ind = True
                oPriceRequestInformationTPA_Extensions.FareCalc = oFareCalc
                oFareCalcFareBasis.WithFareCalc = True
                oPriceRequestInformationTPA_Extensions.FareCalc.FareBasis = oFareCalcFareBasis
                oPriceRequestInformationTPA_Extensions.Priority = New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriority
                oPriceRequestInformationTPA_Extensions.Priority.Price = New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityPrice
                oPriceRequestInformationTPA_Extensions.Priority.Price.Priority = "1"
                oPriceRequestInformationTPA_Extensions.Priority.DirectFlights = New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityDirectFlights
                oPriceRequestInformationTPA_Extensions.Priority.DirectFlights.Priority = "2"
                oPriceRequestInformationTPA_Extensions.Priority.Time = New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityTime
                oPriceRequestInformationTPA_Extensions.Priority.Time.Priority = "3"
                oPriceRequestInformationTPA_Extensions.Priority.Vendor = New OTA_AirLowFareSearch.OTA_AirLowFareSearchRQPriceRequestInformationTPA_ExtensionsPriorityVendor
                oPriceRequestInformationTPA_Extensions.Priority.Vendor.Priority = "4"

                .TPA_Extensions = oPriceRequestInformationTPA_Extensions

            End With

            oAirLowFareSearchRQ.OriginDestinationInformation = oOriginDestinationInformation
            oAirLowFareSearchRQ.TravelPreferences = oTravelPreferences
            oAirLowFareSearchRQ.TravelerInformation = oTravelerInformation
            oAirLowFareSearchRQ.PriceRequestInformation = oPriceRequestInformation
            oAirLowFareSearchRQ.Version = "1.13.1"
            '***********************************
            oAirLowFareSearchRS = oAirLowFareSearchRQService.OTA_AirLowFareSearchRQ(oAirLowFareSearchRQ)

            Return oAirLowFareSearchRS

        Catch Ex As Exception
            oGeneraPayLoadXML = New GeneraPayLoad
            oGeneraPayLoadXML.Serialize(GetType(OTA_AirLowFareSearch.OTA_AirLowFareSearchRQ), _
                                        GetType(OTA_AirLowFareSearch.OTA_AirLowFareSearchRS), _
                                        oAirLowFareSearchRQ, _
                                        oAirLowFareSearchRS, _
                                        FUNCTION_NAME, _
                                        strCodigoSeguimiento, _
                                        False, _
                                        False)
            Err.Raise(Err.Number, Err.Source, Err.Description)
        Finally
            oSecurity = Nothing
            oPos = Nothing
            oSource = Nothing
            oUsernameToken = Nothing
            omsgHeader = Nothing
            oFromPartyId = Nothing
            oFrom = Nothing
            oFromPartyIdArr = Nothing
            oToPartyId = Nothing
            oToPartyIdArr = Nothing
            oTo = Nothing
            omsgData = Nothing
            oService = Nothing
            oAirLowFareSearchRQ = Nothing
            oAirLowFareSearchRQService = Nothing
        End Try
    End Function
    Public Function _QueuePlace204(ByVal strNombre As String, _
                                   ByVal strNumero As String, _
                                   ByVal strPrefatoryInstructionCode As String, _
                                   ByVal strPseudoCityCode As String, _
                                   ByVal strCodigoSeguimiento As String, _
                                   ByVal intFirmaGDS As Integer, _
                                   ByVal intFirmaDB As Integer, _
                                   ByVal objSession As classSession) As QueuePlaceLLS204.QueuePlaceRS

        Const ID_SABRE_WEB_SERVICE As String = "56"

        Dim FUNCTION_NAME As String = oSabreWebService.List(ID_SABRE_WEB_SERVICE).Name

        Dim oSecurity1 As QueuePlaceLLS204.Security1 = Nothing

        Dim oUsernameToken As QueuePlaceLLS204.SecurityUsernameToken = Nothing
        Dim oMessageHeader As QueuePlaceLLS204.MessageHeader = Nothing

        Dim oFromPartyId As QueuePlaceLLS204.PartyId = Nothing
        Dim oFrom As QueuePlaceLLS204.From = Nothing
        Dim oFromPartyIdArr(0) As QueuePlaceLLS204.PartyId
        Dim oToPartyId As QueuePlaceLLS204.PartyId = Nothing
        Dim oToPartyIdArr(0) As QueuePlaceLLS204.PartyId
        Dim oTo As QueuePlaceLLS204.[To] = Nothing
        Dim oMessageData As QueuePlaceLLS204.MessageData = Nothing
        Dim oService As QueuePlaceLLS204.Service = Nothing


        Dim oQueuePlaceService As QueuePlaceLLS204.QueuePlaceService = Nothing
        Dim oQueuePlaceRQ As QueuePlaceLLS204.QueuePlaceRQ = Nothing
        Dim oQueuePlaceRS As QueuePlaceLLS204.QueuePlaceRS = Nothing


        Dim QueueIdentifier As QueuePlaceLLS204.QueuePlaceRQQueueInfoQueueIdentifier() = Nothing
        Dim QueueInfo As QueuePlaceLLS204.QueuePlaceRQQueueInfo = Nothing


        Try

            oMessageHeader = New QueuePlaceLLS204.MessageHeader
            oFromPartyId = New QueuePlaceLLS204.PartyId
            oFrom = New QueuePlaceLLS204.From
            oToPartyId = New QueuePlaceLLS204.PartyId
            oTo = New QueuePlaceLLS204.[To]
            oService = New QueuePlaceLLS204.Service
            oMessageData = New QueuePlaceLLS204.MessageData
            oQueuePlaceService = New QueuePlaceLLS204.QueuePlaceService

            obj_Cabecera(ID_SABRE_WEB_SERVICE, _
                         oMessageHeader, _
                         objSession.ConversationID.ToString, _
                         oFromPartyId, _
                         oFromPartyIdArr, _
                         oFrom, _
                         oToPartyId, _
                         oToPartyIdArr, _
                         oTo, _
                         Nothing, _
                         oService, _
                         oMessageData, _
                         oQueuePlaceService)


            oUsernameToken = New QueuePlaceLLS204.SecurityUsernameToken
            oSecurity1 = New QueuePlaceLLS204.Security1
            oQueuePlaceRQ = New QueuePlaceLLS204.QueuePlaceRQ

            obj_Security(Nothing, _
                         objSession.Token.ToString, _
                         oUsernameToken, _
                         oSecurity1, _
                         Nothing, _
                         Nothing, _
                         oQueuePlaceRQ, _
                         True, _
                         oQueuePlaceService, _
                         intFirmaGDS)

            oQueuePlaceRQ.ReturnHostCommandSpecified = True
            oQueuePlaceRQ.ReturnHostCommand = True

            oQueuePlaceRQ.TimeStamp = Constantes.IWS_TIMESTAMP
            oQueuePlaceRQ.TimeStampSpecified = True
            oQueuePlaceRQ.Version = "2.0.4"


            ReDim QueueIdentifier(0)

            QueueIdentifier(QueueIdentifier.Length - 1) = New QueuePlaceLLS204.QueuePlaceRQQueueInfoQueueIdentifier
            QueueIdentifier(QueueIdentifier.Length - 1).Number = strNumero '"100"
            QueueIdentifier(QueueIdentifier.Length - 1).PrefatoryInstructionCode = strPrefatoryInstructionCode '"200"
            QueueIdentifier(QueueIdentifier.Length - 1).PseudoCityCode = strPseudoCityCode '"XX05"

            QueueInfo = New QueuePlaceLLS204.QueuePlaceRQQueueInfo
            QueueInfo.QueueIdentifier = QueueIdentifier

            oQueuePlaceRQ.QueueInfo = New QueuePlaceLLS204.QueuePlaceRQQueueInfo
            oQueuePlaceRQ.QueueInfo = QueueInfo

            oQueuePlaceRS = oQueuePlaceService.QueuePlaceRQ(oQueuePlaceRQ)


        Catch ex As Exception
            strLog = "IWebServices : " & FUNCTION_NAME & vbCrLf
            strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Source : " & "_QueuePlace204" & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Token : " & objSession.Token.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "ConversationID : " & objSession.ConversationID.ToString & vbCrLf

            objEscribeLog.WriteLog(strLog, FUNCTION_NAME, strCodigoSeguimiento)
            Throw New Exception(ex.ToString)
        End Try
        Return oQueuePlaceRS
    End Function
#End Region
#Region "Servicios"
    Private Sub obj_Cabecera(ByVal oSWS_Id As String, _
                               ByVal oMessageHeader As Object, _
                               ByVal oConversationId As String, _
                               ByVal oFromPartyId As Object, _
                               ByVal oFromPartyIdArr As Object, _
                               ByVal oFrom As Object, _
                               ByVal oToPartyId As Object, _
                               ByVal oToPartyIdArr As Object, _
                               ByVal oTo As Object, _
                               ByVal oPCC As String, _
                               ByVal oService As Object, _
                               ByVal oMessageData As Object, _
                               ByVal oSessionCreateService As Object)

        Dim oSWS_SOAP_Envelope As WS_Login_SOAPEnvelope.SOAP_Envelope = oSabreWebService.List(oSWS_Id)

        Try

            If Not String.IsNullOrEmpty(oConversationId) Then
                oMessageHeader.ConversationId = oConversationId
            End If

            oFromPartyId.Value = oSWS_Login.Conversation
            oFromPartyIdArr(0) = oFromPartyId

            oFrom.PartyId = oFromPartyIdArr

            oMessageHeader.From = oFrom

            oToPartyId.Value = Constantes.IWS_PARTY_ID
            oToPartyIdArr(0) = oToPartyId

            oTo.PartyId = oToPartyIdArr

            oMessageHeader.To = oTo

            If Not String.IsNullOrEmpty(oPCC) Then
                oMessageHeader.CPAId = oPCC
            End If


            If Not String.IsNullOrEmpty(oSWS_SOAP_Envelope.Action) Then
                oMessageHeader.Action = oSWS_SOAP_Envelope.Action
            End If

            If Not String.IsNullOrEmpty(oSWS_SOAP_Envelope.Service) Then
                oService.Value = oSWS_SOAP_Envelope.Service
            End If

            If Not String.IsNullOrEmpty(oSWS_SOAP_Envelope.Type) Then
                oService.type = oSWS_SOAP_Envelope.Type
            End If


            oMessageHeader.Service = oService

            oMessageData.MessageId = Constantes.IWS_LITERAL & Format(dHoy, Constantes.IWS_DATE_FORMAT_FILE) & "-" & Format(dHoy, Constantes.IWS_TIME_FORMAT_FILE3) & "-" & Format(dHoy, Constantes.IWS_TIME_FORMAT_FILE4) & Constantes.Arroba & oSWS_Login.Conversation '"mid:00010101-120000-0000@nuevomundoviajes.com"
            oMessageData.Timestamp = Format(dHoy, Constantes.IWS_DATE_FORMAT_FILE2) & "T" & Format(dHoy, Constantes.IWS_TIME_FORMAT_FILE_12) & "Z" '"0001-01-01T12:00:00Z"

            If Not String.IsNullOrEmpty(oSWS_SOAP_Envelope.Cid) Then
                oMessageData.RefToMessageId = oSWS_SOAP_Envelope.Cid
            End If

            oMessageHeader.MessageData = oMessageData
            If Not String.IsNullOrEmpty(oSWS_SOAP_Envelope.Version) Then
                oMessageHeader.version = oSWS_SOAP_Envelope.Version
            End If

            oSessionCreateService.MessageHeaderValue = oMessageHeader

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
    Private Sub obj_Security(ByVal oPCC As String, _
                             ByVal oToken As String, _
                             ByVal oUsernameToken As Object, _
                             ByVal oSecurity As Object, _
                             ByVal oSource As Object, _
                             ByVal oPos As Object, _
                             ByVal oObjetoRQ As Object, _
                             ByVal oCargaRQ As Boolean, _
                             ByVal oObjetService As Object, _
                             ByVal oOp_Firma As Integer)
        Try


            If oOp_Firma = "0" Then
                oUsernameToken.Username = oSWS_Login.User
                oUsernameToken.Password = oSWS_Login.Password
                oUsernameToken.Organization = oSWS_Login.Organization
                'ElseIf oOp_Firma = "1" Then
                '    oUsernameToken.Username = oSWS_LoginResert.User
                '    oUsernameToken.Password = oSWS_LoginResert.Password
                '    oUsernameToken.Organization = oSWS_LoginResert.Organization
            ElseIf oOp_Firma = "2" Then
                oUsernameToken.Username = oSWS_LoginAgcorp.User
                oUsernameToken.Password = oSWS_LoginAgcorp.Password
                oUsernameToken.Organization = oSWS_LoginAgcorp.Organization
            Else
                oUsernameToken.Username = oSWS_Login.User
                oUsernameToken.Password = oSWS_Login.Password
                oUsernameToken.Organization = oSWS_Login.Organization
            End If

            oUsernameToken.Domain = Constantes.IWS_DOMAIN

            oSecurity.UsernameToken = oUsernameToken

            If Not oSource Is Nothing Then
                If Not String.IsNullOrEmpty(oPCC) Then
                    oSource.PseudoCityCode = oPCC
                End If

                If Not oPos Is Nothing Then
                    oPos.Source = oSource
                End If
            End If

            If Not oPos Is Nothing Then
                oObjetoRQ.POS = oPos
            End If

            If oCargaRQ Then

                Try
                    oObjetoRQ.AltLangID = Constantes.IWS_LANGUAGE
                Catch ex As Exception
                End Try


                Try
                    oObjetoRQ.EchoToken = Constantes.IWS_STRING
                Catch ex As Exception
                End Try

                Try
                    oObjetoRQ.PrimaryLangID = Constantes.IWS_LANGUAGE
                Catch ex As Exception
                End Try

                Try
                    oObjetoRQ.Target = Constantes.IWS_TARGET
                Catch ex As Exception
                End Try

                Try
                    oObjetoRQ.SequenceNmbr = "1"
                Catch ex As Exception
                End Try

                Try
                    oObjetoRQ.TimeStamp = Format(dHoy, Constantes.IWS_DATE_FORMAT_FILE2) & "T" & Format(dHoy, Constantes.IWS_TIME_FORMAT_FILE_12)
                Catch ex As Exception
                End Try

                Try
                    If oObjetoRQ.Version Is Nothing Then
                        oObjetoRQ.Version = ""
                    End If
                Catch ex As Exception
                End Try

            End If


            If Not String.IsNullOrEmpty(oToken) Then
                oSecurity.BinarySecurityToken = oToken
            End If


            Try
                oObjetService.SecurityValue = oSecurity
            Catch ex As Exception
                oObjetService.Security = oSecurity
            End Try

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
#End Region
End Class
