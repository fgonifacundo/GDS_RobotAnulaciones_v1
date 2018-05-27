Imports System.Configuration

Namespace Constantes
    Public Class classConstantes

        Public Const EsquemaCondor As String = "AGCORP" '"AGCORP" "PTACONDOR"
        Public Const EsquemaNM As String = "NUEVOMUNDO"
        Public Const EsquemaDestinos As String = "DESTINOS_TRP"


        Public Shared Function getEsquema(ByVal intEsquema As Integer) As String
            Dim esquema As String = String.Empty
            Select Case intEsquema
                Case 5
                    esquema = EsquemaNM
                Case 6
                    esquema = EsquemaDestinos
                Case 7
                    esquema = EsquemaCondor
                Case Else
                    esquema = EsquemaNM
            End Select
            Return esquema
        End Function

#Region "Pseudo"
        Public Const PseudoInteragencias As String = "QF05"
        Public Const PseudoNuevoMundo As String = "QP75"
        Public Const PseudoDestinosMundiales As String = "QQ05"
        Public Const PseudoCaminoReal As String = "QP95"
#End Region
#Region "Controles"
        Public Const oControlPrincipal As String = "Easyonline1$"
#End Region
#Region "VariblesTablas"
        Public Const strAnchoTablaMensaje As String = "550px"
#End Region
#Region "VariablesSession"
        Public Const oListaCotizacion As String = "oListaCotizacion"
        Public Const objEasyOnLine As String = "objEasyOnLine"
        Public Const objDeposito As String = "objDeposito"

#End Region
#Region "CaracteresEspeciales"
        Public Const RetornoCarro As String = Chr(13)
        Public Const Espacio As String = Chr(32)
        Public Const FinLinea As String = Chr(10)
        Public Const IWS_CROSS_OF_LORRAINE As Char = ChrW(157) ' ¥
        Public Const Change As Char = ChrW(164) '¤
        Public Const IWS_Sumarotia As Char = ChrW(167)


        Public Const Separador As String = "%"
        Public Const Slash As String = "/"
        Public Const BackSlash As String = "\"
        Public Const PuntoComa As String = ";"
        Public Const DosPuntos As String = ":"
        Public Const Coma As String = ","
        Public Const Apostrofe As String = "'"
        Public Const ApostrofeComaApostrofe As String = "','"
        Public Const Punto As String = "."
        Public Const Asterisco As String = "*"
        Public Const Guion As String = "-"
        Public Const GuionBajo As String = "_"
        Public Const Arroba As String = "@"
        Public Const AsteriscoSlash As String = "*/"
        Public Const PasswordTemporalPerfilAgente As String = "1234ABC"
#End Region
#Region "Formatos"
        Public Const IWS_DATE_FORMAT_FILE As String = "yyyyMMdd"
        Public Const IWS_TIME_FORMAT_FILE As String = "HHmmssff"
        Public Const IWS_DATE_FORMAT_FILE2 As String = "yyyy-MM-dd"
        Public Const IWS_TIME_FORMAT_FILE3 As String = "hhmmss"
        Public Const IWS_TIME_FORMAT_FILE4 As String = "ss00"
        Public Const IWS_DATE_FORMAT_FILE5 As String = "dd/MM/yyyy"
        Public Const IWS_DATE_FORMAT_FILE6 As String = "dd-MM-yyyy"
        Public Const IWS_DATE_FORMAT_FILE7 As String = "MM/dd/yyyy"
        Public Const IWS_TIME_FORMAT_FILE_24 As String = "HH:mm:ss"
        Public Const IWS_TIME_FORMAT_FILE_12 As String = "hh:mm:ss"
        Public Const IWS_DATE_FORMAT_INSERT As String = "MM/dd/yyyy HH:mm:ss"
        Public Const IWS_DATE_FORMAT_INSERT_2 As String = "dd/MM/yyyy HH:mm:ss"
        Public Const IWS_TIMESTAMP As String = "0001-01-01T12:00:00Z"
        Public Const IWS_TIMESTAMP_AVISO As String = "0001-01-01T06:00:00Z"
        Public Const IWS_TIMESTAMP_VOID As String = "0001-01-01T07:00:00Z"
        Public Const IWS_OUTIN_DATATIME_FORMAT As String = "yyyy-MM-ddT00:00:00"

        Public Const IWS_DOMAIN As String = "Default"
        Public Const IWS_LANGUAGE As String = "en-us"
        Public Const IWS_STRING As String = "String"
        Public Const IWS_PARTY_ID As String = "webservices.sabre.com"
        Public Const IWS_LITERAL As String = "mid:"
        Public Const IWS_TARGET As Integer = 0
        Public Const TabEspacios As String = vbTab & "  "

        Public Const IWS_AMERICAN_DOLLARS As String = "USD"
        Public Const IWS_SSR_FOID As String = "FOID"
        Public Const IWS_SSR_DOCS As String = "DOCS"

        Public Const IWS_REQUEST As String = "RQ"
        Public Const IWS_RESPONSE As String = "RS"
        Public Const IWS_STATUS_SEGMENT As String = "HK"
        Public Const IWS_PRIVATE As String = "PV"
        Public Const IWS_PUBLIC As String = "PL"
#End Region
#Region "DOCUMENTO"
        Public Const Id_DNI As String = "D"
        Public Const Nombre_DNI As String = "DOCUMENTO NACIONAL DE IDENTIDAD"
        Public Const Id_DNI_FOID As String = "NI"

        Public Const Id_CE As String = "CE"
        Public Const Nombre_CE As String = "CARNET DE EXTRANJERIA"
        Public Const Id_CE_FOID As String = "NICE"

        Public Const Id_PASS As String = "P"
        Public Const Nombre_Pasaporte As String = "PASAPORTE"
        Public Const Id_PASS_FOID As String = "IDPP"

        Public Const Id_RUC As String = "RUC"
        Public Const Nombre_RUC As String = "REGISTRO UNICO DE CONTRIBUYENTE"

#End Region
#Region "Estructura"
        'Public Const PNR As String = "PNR%"
        'Public Const DK As String = "DK%"
        'Public Const TKT As String = "TKT%"
        ' Public Const RUTA As String = "RUTA%"
        Public Const StoredProcedure As String = "SP"
        Public Const EjecutaFuncion As String = "TX"
        Public Const SentenciaText As String = "TX"
        Public Const CommandTimeout As Integer = 200
#End Region
#Region "Usuarios"
        Public Const Usr_PTA_Amadeus As Integer = 0
        Public Const Usr_PTA_Sabre As Integer = 1
        Public Const Usr_PTA_EasyOnline As Integer = 2
        Public Const Usr_WEB_General As Integer = 3
        Public Const Usr_WEB_Demo As Integer = 4
        Public Const Usr_GENERADOROADP_NM As Integer = 5
        Public Const Usr_GENERADOROADP_DM As Integer = 6
        Public Const Usr_PTA_Demonuevomundo As Integer = 7
        Public Const Usr_PTA_Ptadestinos As Integer = 8
#End Region
#Region "Emision Autonoma"
        Public Const EASabreConRestricciones As Integer = 1384
        Public Const EASabreSinRestricciones As Integer = 1382
#End Region
#Region "Stored ProcedurePTA"
        Public Const spCREDITO_DISPONIBLE As String = "NUEVOMUNDO.UP_SEL_CREDITO_DISPONIBLE"
        Public Const spDOCUMENTOSVENCIDOS As String = "NUEVOMUNDO.PKG_GDS_CLIENTE.GDS_DOCUMENTOSVENCIDOS"
        Public Const spCIUDAD As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_CIUDAD"
        Public Const spCIUDADES As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_CIUDADES"
        Public Const spPROMOTOR As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_PROMOTOR"
        Public Const spPERMISOS As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_PERMISOS"
        Public Const spDATOS_SUCURSAL_PUNTO As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_DATOS_SUCURSAL_PUNTO"
        Public Const spDATOSCLIENTE As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_DATOS_CLIENTE"
        Public Const spEMAIL_EASYONLINE As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_EMAIL_EASYONLINE"
        Public Const spDATOSCLIENTE_DM As String = "PTADESTINOS.PKG_GDS_WEBSERVICEPTA.GDS_DATOS_CLIENTE"
        Public Const spGRUPOINTERNO As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_GRUPO_INTERNO"
        Public Const spGDSINTERFACE As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_INTERFACE"
        Public Const spSUBCODIGO As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_SUB_CODIGO"
        Public Const spDESCUENTOEXTRA As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_DSCTEXTRA"
        Public Const spDATOS_AEROLINEA As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_DATOS_AEROLINEA"
        Public Const spTIPO_PASAJEROS As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_TIPO_PASAJEROS"
        Public Const spDOCUMENTO_EMISION As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_DOCUMENTO_EMISION"
        Public Const spPAIS As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_PAIS"
        Public Const spFIRMAAGENTE As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_FIRMA_AGENTE"
        Public Const spFIRMAAGENTE_DM As String = "PTADESTINOS.PKG_GDS_WEBSERVICEPTA.GDS_FIRMA_AGENTE"
        Public Const spPSEUDOS As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_PSEUDOS"
        Public Const spGDSFEE_WAIVER As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_FEE_WAIVER"
        Public Const spGDS_DIFERENCIA_FECHAS As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_DIFERENCIA_FECHAS"
        Public Const spASIGNA_IMPRESORA As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_ASIGNA_IMPRESORA"
        Public Const spTARIFAS_FQ As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_TARIFAS_FQ"
        Public Const spINS_TARIFAS_FQ As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_INS_TARIFAS_FQ"
        Public Const spTIPO_TARJETA As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_TIPO_TARJETA"
        Public Const spACTUALIZA_IMPRESION As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_ACTUALIZA_IMPRESION"
        Public Const spEQUIPO_AVION As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_EQUIPO_AVION"
        Public Const spNO_PRINTER_CUENTAS As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_NO_PRINTER_CUENTAS"
        Public Const spCONSULTA_GDS_PSEUDO As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_CONSULTA_PSEUDO"

        Public Const spLISTA_CORREOS_ENVIAR As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_LISTAR_ENVIOS"
        Public Const spUP_LISTA_CORREOS As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_UP_LIST_ENVIOS"
        Public Const spAGENTE_EMITE As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_AGENTE_EMITE"
        '
        Public Const spDOCUMENTOTARJETA As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_DOCTARJETA"
        Public Const spBOLETOSPENDIENTES As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_BOLETOS_PENDIENTES"
        Public Const spBOLETOSPAGAOTRODK As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_BOLETOS_PAGA_OTRO_DK"
        Public Const spAEROLINEA_ASOCIADA As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_AEROLINEA_ASOCIADA"
        Public Const spEMDS_FACTURADOS As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_EMDS_FACTURADOS"
        Public Const spBUSCA_IATA As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_BUSCA_IATA"
        Public Const spPERFIL_IMPRESORA_PSEUDO As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_PERFIL_IMPRESORA_PSEUDO"
        Public Const spSTOCK_BOLETO As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_STOCK_BOLETO"

        Public Const spBOLETOSPENDIENTESROBOT As String = ".PKG_GDS_ROBOT_ANULACIONES.GDS_BOLETOS_PENDIENTES_ANULA"
        Public Const spBOLETOSPAGAOTRODKROBOT As String = ".PKG_GDS_ROBOT_ANULACIONES.GDS_BOLETOS_PAGA_OTRODK_ANULA"

#End Region

#Region "Stored ProcedureTURBO"
        Public Const spTURBO_PUNTOFACTURACION As String = "NUEVOMUNDO.PKG_GDS_TURBO.GDS_TURBO_PUNTOFACTURACION"
        Public Const spTURBO_DATOSPAX As String = "NUEVOMUNDO.PKG_GDS_TURBO.GDS_TURBO_DATOSPAX"
        Public Const spTURBO_INS_DATOSPAX As String = "NUEVOMUNDO.PKG_GDS_TURBO.GDS_TURBO_INS_DATOSPAX"
        Public Const spTURBO_INS_INTERFACEGENERAL As String = "NUEVOMUNDO.PKG_GDS_TURBO.GDS_TURBO_INS_INT_GENERAL"
        Public Const spTURBO_INS_INTERFACEDETALLE As String = "NUEVOMUNDO.PKG_GDS_TURBO.GDS_TURBO_INS_INT_DETALLE"
        Public Const spTURBO_EMISION_SOLICITUD As String = "NUEVOMUNDO.PKG_GDS_TURBO.GDS_TURBO_EMITIO_SOLICITO"
        Public Const spGDS_TURBO_GSA As String = "NUEVOMUNDO.PKG_GDS_TURBO.GDS_TURBO_GSA"
        Public Const spFILE_BOLETO As String = "NUEVOMUNDO.PKG_GDS_TURBO.GDS_TURBO_FILE_BOLETO"
#End Region

#Region "Stored RobotOADP"
        Public Const spLISTADO_DWLIST As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_LISTADO_DWLIST"
        Public Const spBOLETO_DWLIST As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_BOLETO_DWLIST"
        Public Const spINSERTA_DWLIST As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_INSERTA_DWLIST"
        Public Const spUPDATE_DWLIST As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_UPDATE_DWLIST"
        Public Const spVERIFICA_DWLIST As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_VERIFICA_DWLIST"

        Public Const spVERIFICA_TPR As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_VERIFICA_TPR"
        Public Const spVERIFICA_TPR_DM As String = "PTADESTINOS.PKG_GDS_WEBSERVICEPTA.GDS_VERIFICA_TPR"
        Public Const spSINDOCUMENT_TPR As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_SINDOCUMENTO_TPR"
        Public Const spSINDOCUMENT_TPR_DM As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_SINDOCUMENTO_TPR"
        Public Const spINSERTA_TPR As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_INSERTA_TPR"
        Public Const spINSERTA_TPR_DM As String = "PTADESTINOS.PKG_GDS_WEBSERVICEPTA.GDS_INSERTA_TPR"
        Public Const spUPDATE_TRP As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_UPDATE_TRP"
        Public Const spUPDATE_TRP_DM As String = "PTADESTINOS.PKG_GDS_WEBSERVICEPTA.GDS_UPDATE_TRP"
        Public Const spCONSULTA_CCCF As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_CONSULTA_CCCF"
        Public Const spCONSULTA_CCCF_DM As String = "PTADESTINOS.PKG_GDS_WEBSERVICEPTA.GDS_CONSULTA_CCCF"

        Public Const spTPR_BUSCAR As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_TPR_BUSCAR"
        Public Const spTPR_ACTUALIZA_IMPRESION As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_TPR_ACTUALIZA_IMPRESION"
        Public Const spTPR_ACTUALIZA_IMPRESION_DM As String = "PTADESTINOS.PKG_GDS_WEBSERVICEPTA.GDS_TPR_ACTUALIZA_IMPRESION"
        Public Const spTPR_ACTUALIZA_CORREO As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_TPR_ACTUALIZA_CORREO"
        Public Const spTPR_ACTUALIZA_CORREO_DM As String = "PTADESTINOS.PKG_GDS_WEBSERVICEPTA.GDS_TPR_ACTUALIZA_CORREO"
        Public Const spCONTROL_MOROSIDAD As String = "NUEVOMUNDO.PKG_GDS_SABRE_JAVA.GDS_CONTROL_MOROSIDAD"
        Public Const spELIMINAR_CUENTA_MOROSA As String = "NUEVOMUNDO.PKG_GDS_SABRE_JAVA.GDS_ELIMINAR_CUENTA_MOROSA"
        Public Const spGET_MAIL_EJE_COBRANZA As String = "NUEVOMUNDO.PKG_GDS_SABRE_JAVA.GDS_GET_MAIL_EJE_COBRANZA"

        Public Const spGDS_VERIFICA_DOBLE_INTERFACE_DESTINOS As String = "PTADESTINOS.PKG_GDS_WEBSERVICEPTA.GDS_VERIFICA_DOBLE_INT_DM"

#End Region

#Region "Stored ProcedureModuloComercial"
        Public Const spGDS_RMC_CAMBIAR_AEROLINEA As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_RMC_CAMBIAR_AEROLINEA"
        Public Const spGDS_RMC_HOMOLOGA As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_RMC_HOMOLOGA"
        Public Const spGDS_RMC_EQUIVALENTE As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_RMC_EQUIVALENTE"
        Public Const spGDS_MC_EXISTE_REGLA As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_MC_EXISTE_REGLA"
        Public Const spGDS_MC_BUSCA_IATA As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_MC_BUSCA_IATA"
        Public Const spGDS_MC_OBTIENE_CONCEPTOS As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_MC_OBTIENE_CONCEPTOS"
        Public Const spGDS_MC_INS_CONCEPTOS As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_MC_INS_CONCEPTOS"
        Public Const spGDS_MC_DEL_TOURCODE_EVALU As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_MC_DEL_TOURCODE_EVALU"
        Public Const spGDS_EMC_UP_CONCEPTOS_TOURCODES As String = "NUEVOMUNDO.UP_CONCEPTOS_TOURCODES"
        Public Const spGDS_EMC_UP_TOURCODE_EVALUACION As String = "NUEVOMUNDO.UP_TOURCODE_EVALUACION"
        Public Const spGDS_EMC_CONCEPTOS_PSEUDOSBULK As String = "NUEVOMUNDO.GDS_CONCEPTOS_PSEUDOSBULK"
        Public Const spGDS_EMC_UP_CONCEPTOS_TARIFABULK As String = "NUEVOMUNDO.UP_CONCEPTOS_TARIFABULK"
        Public Const spGDS_EMC_FORMATOS_TARIFASBULK As String = "NUEVOMUNDO.GDS_FORMATOS_TARIFASBULK"
        Public Const spGDS_EMC_UP_TARIFABULK_EVALUACION As String = "NUEVOMUNDO.UP_TARIFABULK_EVALUACION"
        Public Const spGDS_MC_OBTIENE_CONCEPTOS_TARIFASBULK As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_MC_OBTIENE_CON_TARIFABULK"
        'Public Const spGDS_MC_DEL_TARIFASBULK_EVALU As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_MC_DEL_TARIFABULK_EVALU"


        Public Const spGDS_PSEUDOBULK_EVALUACION As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_PSEUDOBULK_EVALUACION"
        Public Const spGDS_LISTAR_AEROLINEAS_TRANSPORTADOR As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_LISTAR_AEROLINEAS"
        Public Const spGDS_LISTAR_AEROLINEAS As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_LISTAR_AEROLINEAS"
        Public Const spGDS_DEL_TARIFABULK_FQ As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_DEL_TARIFABULK_FQ"
        Public Const spGDS_DEL_TARIFABULK_COMBINACION As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_DEL_TARIFABULK_COMBINACION"
        Public Const spGDS_PSEUDOSBULK_CONCEPTOS As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_PSEUDOSBULK_CONCEPTOS"
        Public Const spGDS_POSIBLES_AEROLINEAS_CC As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_POSIBLES_AEROLINEAS_CC"
        Public Const spGDS_LISTA_DT_TARIFABULK_FQ As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_LISTA_DT_TARIFABULK_FQ"
        Public Const spGDS_AEROLINEA_TARIFA As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_AEROLINEA_TARIFA"
        Public Const spGDS_INSERTAR_TARIFABULK_FQ As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_INSERTAR_TARIFABULK_FQ"
        Public Const spGDS_LISTAR_TIPO_DE_PAX_VUELO As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_LISTAR_TIPO_DE_PAX_VUELO"
        Public Const spGDS_TIPO_DE_PAX_VUELO_ALL As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_TIPO_DE_PAX_VUELO_ALL"
        Public Const spGDS_LISTAR_TOURCODES_CONCEPTOS As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_LISTAR_TOURCODES_CONCEPTOS"
        Public Const spGDS_DEL_PSEUDOSBULK_CONCEPTOS As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_DEL_PSEUDOSBULK_CONCEPTOS"
        Public Const spGDS_DEL_PSEUDOSBULK_EVALUACION As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_DEL_PSEUDOSBULK_EVALUACION"
        Public Const spGDS_INS_TARIFABULK_EVALUACION As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_INS_TARIFABULK_EVALUACION"
        Public Const spGDS_TARIFASBULK_CONCEPTOS As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_TARIFASBULK_CONCEPTOS"
        Public Const spGDS_DEL_TARIFABULK_EVALUACION As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_DEL_TARIFABULK_EVALUACION"
        Public Const spGDS_DEL_TARIFABULK_CONCEPTOS As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_DEL_TARIFABULK_CONCEPTOS"
        Public Const spGDS_TARIFASBULK_GETCORRELATIVO As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_TARIFASBULK_GETCORRELATIVO"
        Public Const sp_ODD_GETTARIFAS_PROMO As String = "NUEVOMUNDO.PKG_GDS_TARIFAS.SP_ODD_GETTARIFAS_PROMO"
        Public Const sp_INSERT_ITIN As String = "NUEVOMUNDO.PKG_GDS_TARIFAS.SP_ODD_INSITINERARIO"
        Public Const sp_ODD_GETITINERARIO As String = "NUEVOMUNDO.PKG_GDS_TARIFAS.SP_ODD_GETITINERARIO"
        Public Const GDS_INSERT_XMLtoTABLE As String = "NUEVOMUNDO.PKG_GDS_TARIFAS.GDS_INSERT_XMLtoTABLE"
        Public Const GDS_INSERT_XMLtoTABLE_PTADESTINOS As String = "PTADESTINOS.PKG_WEB_REP_METAS_DM.GDS_INSERT_XMLtoTABLE"

        Public Const sp_ODD_DELETEITINERARIO_ALL As String = "NUEVOMUNDO.PKG_GDS_TARIFAS.SP_ODD_DELETEITINERARIO_ALL"
        Public Const SP_ODD_GETTIPO_DE_PAX As String = "NUEVOMUNDO.PKG_GDS_TARIFAS.SP_ODD_GETTIPO_DE_PAX"

        Public Const spGDS_REPORTE_CONFIRMACIONES As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_REPORTE_CONFIRMACIONES"
        Public Const spGDS_REPORTE_CONFIRMACIONES_DM As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_REPORTE_CONFIRMACIONES_DM"
        Public Const spGDS_BOLETOS_FACTURADOS_DINNERS As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_BOLETOS_FACTURADOS_DINNERS"
        Public Const spGDS_FIRMA_AGENTE_DINNERS As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_FIRMA_AGENTE_DINNERS"
        Public Const spGDS_LISTAR_TURBO_CC_CHARGE As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_LISTAR_TURBO_CC_CHARGE"

        Public Const spGDS_INSERTA_ENVIO_MENSAJES_EA As String = "NUEVOMUNDO.PKG_GDS_PRUEBA.GDS_INSERTA_ENVIO_MENSAJES_EA"




        Public Const spGDS_PseudosBulk_Evaluacion_T As String = "NUEVOMUNDO.GDS_PseudosBulk_Evaluacion_T"
        Public Const spUP_conceptos_tarifaBulk As String = "NUEVOMUNDO.UP_conceptos_tarifaBulk"
        Public Const spUP_tarifabulk_evaluacion As String = "NUEVOMUNDO.UP_tarifabulk_evaluacion"


        Public Const spINSERTA_PSEUDOSBULK_EVALUACION As String = "NUEVOMUNDO.PKG_GDS_TARIFASBULK.INSERTA_PSEUDOSBULK_EVALUACION"




        Public Const spGDS_ACTUALIZA_BOLETO_PAX As String = "NUEVOMUNDO.PKG_GDS_SABRE_JAVA.GDS_ACTUALIZA_BOLETO_PAX"
        Public Const spGDS_INSERTA_TEXTO_EN_FILE As String = "NUEVOMUNDO.PKG_GDS_SABRE_JAVA.GDS_INSERTA_TEXTO_EN_FILE"
        Public Const spGDS_LISTAR_BOLETO_PAX As String = "NUEVOMUNDO.PKG_GDS_SABRE_JAVA.GDS_LISTAR_BOLETO_PAX"
        Public Const spGDS_LISTAR_MOTIVOS_VOID As String = "NUEVOMUNDO.PKG_GDS_SABRE_JAVA.GDS_LISTAR_MOTIVOS_VOID"
        Public Const spGDS_BOLETOS_X_CONFIRMACION As String = "NUEVOMUNDO.PKG_GDS_SABRE_JAVA.GDS_BOLETOS_X_CONFIRMACION"
        Public Const spGDS_ACTUALIZA_BOLETO_PAX_EMD As String = "NUEVOMUNDO.PKG_GDS_SABRE_JAVA.GDS_ACTUALIZA_BOLETO_PAX_EMD"
        Public Const spGDS_LISTAR_TURBO_PASSENGER As String = "NUEVOMUNDO.PKG_GDS_SABRE_JAVA.GDS_LISTAR_TURBO_PASSENGER"
        Public Const spGDS_DATOS_CLIENTE_EMPRESA As String = "NUEVOMUNDO.PKG_GDS_SABRE_JAVA.GDS_DATOS_CLIENTE_EMPRESA"

        Public Const spGDS_OBTENER_BOLETO_FACTURADO As String = "NUEVOMUNDO.PKG_GDS_PRUEBA_BOLETO.GDS_BOLETO_FACTURADO"
        Public Const spGDS_BOLETO_FACTURADO_PTADESTINOS As String = "NUEVOMUNDO.PKG_GDS_PRUEBA_BOLETO.GDS_BOLETO_FACTURADO_PTADEST"
        Public Const spGDS_TIPOCLIENTE As String = "NUEVOMUNDO.PKG_GDS_PRUEBA_BOLETO.GDS_DATOS_CLIENTE"
        Public Const spGDS_PAGO_PENDIENTE As String = "NUEVOMUNDO.PKG_GDS_PRUEBA_BOLETO.GDS_PAGO_PENDIENTE"
        Public Const spGDS_AUTORIZACION_VOID As String = "NUEVOMUNDO.PKG_GDS_PRUEBA_BOLETO.GDS_AUTORIZACION_NOVOID"
        Public Const spGDS_CORREO_AGENTE_PROMOTOR As String = "NUEVOMUNDO.PKG_GDS_PRUEBA_BOLETO.GDS_CORREO_AGENTE_PROMOTOR"

        Public Const spGDS_GDS_ANULAR_BOLETO_PAX As String = ".PKG_GDS_ROBOT_ANULACIONES.GDS_ANULAR_BOLETO_PAX"
        Public Const spBOLETOSEMITIDOS As String = ".PKG_GDS_ROBOT_ANULACIONES.GDS_BOLETO_EMITIDO"
        Public Const spGDS_DATOS_VENDEDOR_BD As String = ".PKG_GDS_ROBOT_ANULACIONES.GDS_DATOS_VENDEDOR"
        Public Const spGDS_EXISTE_PSEUDO As String = ".PKG_GDS_ROBOT_ANULACIONES.GDS_EXISTE_PSEUDO"

        Public Const spGDS_DATOS_VENDEDOR As String = "PTADESTINOS.PKG_GDS_PRUEBA_BOLETO.GDS_DATOS_VENDEDOR"



        Public Const spGDS_INSERTA_EMAILROBOT As String = "DEMONUEVOMUNDO.PKG_GDS_ROBOT_SABRE.GDS_INSERTA_EMAILROBOT"
        Public Const spGDS_DATOS_VENDEDORMETAS_DM As String = "PTADESTINOS.PKG_WEB_REP_METAS_DM.GDS_GET_VENDEDOR_DM"

#End Region
#Region "Stored ProcedureWEB"
        Public Const spFN_PERM_TIENE_ACCESO_OPC_USU As String = "APPWEBS.PKG_PERMISOS.FN_PERM_TIENE_ACCESO_OPC_USU"
        Public Const spSP_OBTIENE_PTO_EMISION_USU As String = "APPWEBS.PKG_TURBO_EMISION.SP_OBTIENE_PTO_EMISION_USU"
        Public Const spSP_OBTIENE_HORARIO_WEB As String = "APPWEBS.PKG_GDS_WEB.SP_OBTIENE_HORARIO_WEB"
        Public Const spSP_OBTIENE_TOKEN_JAVA As String = "APPWEBS.PKG_GDS_POOL_SESIONES.GDS_TA_JAVA"

        '*** SOLICITUDES
        Public Const spSP_INS_REE_SOLICITUD As String = "APPWEBS.PKG_GDS_SOLICITUDES.INS_REE_SOLICITUD"
        Public Const spSP_INS_REE_GENERALES As String = "APPWEBS.PKG_GDS_SOLICITUDES.INS_REE_GENERALES"
        Public Const spSP_INS_REE_CALIFICADORES As String = "APPWEBS.PKG_GDS_SOLICITUDES.INS_REE_CALIFICADORES"
        Public Const spSP_INS_REE_TARIFA As String = "APPWEBS.PKG_GDS_SOLICITUDES.INS_REE_TARIFA"
        Public Const spSP_INS_REE_PAGO As String = "APPWEBS.PKG_GDS_SOLICITUDES.INS_REE_PAGO"
        Public Const spSP_INS_REE_DEPOSITOS As String = "APPWEBS.PKG_GDS_SOLICITUDES.INS_REE_DEPOSITOS"

        Public Const spSP_SEL_REEMISION As String = "APPWEBS.PKG_GDS_SOLICITUDES.SEL_REEMISION"
        Public Const spSP_SEL_REE_GENERALES As String = "APPWEBS.PKG_GDS_SOLICITUDES.SEL_REE_GENERALES"
        Public Const spSP_SEL_REE_CALIFICADORES As String = "APPWEBS.PKG_GDS_SOLICITUDES.SEL_REE_CALIFICADORES"
        Public Const spSP_SEL_REE_TARIFA As String = "APPWEBS.PKG_GDS_SOLICITUDES.SEL_REE_TARIFA"
        Public Const spSP_SEL_REE_PAGO As String = "APPWEBS.PKG_GDS_SOLICITUDES.SEL_REE_PAGO"
        Public Const spSP_SEL_REE_DEPOSITOS As String = "APPWEBS.PKG_GDS_SOLICITUDES.SEL_REE_DEPOSITOS"
        Public Const spSP_PAIS_OBTIENE_X_ID As String = "APPWEBS.PKG_PAIS.SP_PAIS_OBTIENE_X_ID"


        Public Const spGDS_HORARIO_ING_SOL_AEXC As String = "APPWEBS.PKG_GDS_HORARIOS.GDS_HORARIO_ING_SOL_AEXC"
        Public Const spDemoGDS_HORARIO_ING_SOL_AEXC As String = "DEMOAPPWEBS.PKG_GDS_HORARIOS.GDS_HORARIO_ING_SOL_AEXC"

        Public Const spGDS_INS_UPDATE_REP As String = "PTADESTINOS.PKG_WEB_REP_METAS_DM.GDS_INS_UPDATE_REP"
#End Region
#Region "Email"
        Public Const emailEasyOnline As String = "easyonline@gruponuevomundo.com.pe"
        Public Const emailEasyReporte As String = "easyreport@gruponuevomundo.com.pe"
        Public Const emailTurboSabre As String = "TurboSabre@gruponuevomundo.com.pe"
        Public Const emailRobotOADP As String = "robot_oadp@gruponuevomundo.com.pe"

        Public Const emailReporteroNM As String = "reportes@nmviajes.com"
        Public Const emailGDSRobotSABRE As String = "gds@nmviajes.com"
        Public Const emailGDS As String = "gds@nmviajes.com" '"gdsweb@gruponuevomundo.com.pe"
        Public Const emailGDS2 As String = "gds@nmviajes.com" '"gds@gruponuevomundo.com.pe"
        Public Const emailHelpDesk As String = "helpdesk@gruponuevomundo.com.pe"


        Public Const emailProgAmadeus As String = "rrosales@nmviajes.com"
        Public Const emailProgSabreWeb As String = "hsanchez@nmviajes.com"
        Public Const emailProgSabreWeb2 As String = "fgoni@nmviajes.com"
        Public Const emailProgSabreRed As String = "jcanepa@nmviajes.com"
        Public Const emailSupervisoraGDS As String = "rcardenas@nmviajes.com"
        Public Const emailJefaGDS As String = "nreid@nmviajes.com"

        'usados en Robot de anulaciones
        Public Shared emailCounterTurno As String = ConfigurationSettings.AppSettings("MAIL_COUNTER_TURNO")
        Public Shared emailRobotAlertas As String = ConfigurationSettings.AppSettings("MAIL_ROBOT_DE_ALERTAS")
        Public Shared emailCoordConsolidador As String = ConfigurationSettings.AppSettings("MAIL_COORD_CONSOLIDADOR")
        Public Shared emailProcesoSabre As String = ConfigurationSettings.AppSettings("MAIL_PROCESO_SABRE")
        Public Shared emailRemitenteRobot As String = ConfigurationSettings.AppSettings("MAIL_REMITENTE_CORREO")

        Public Shared emailSupervisoraCounterIA As String = ConfigurationSettings.AppSettings("MAIL_SUPERVISOR_COUNTER_IA")
        Public Shared emailPromotor As String = ConfigurationSettings.AppSettings("MAIL_SUPERVISOR_COUNTER_IA")
        Public Shared emailDestinatariosNoFacturadosAyer = ConfigurationSettings.AppSettings("MAIL_DESTINATARIOS_NO_FACTURADOS_AYER")

        Public Const emailCounterTurnoIA As String = "counterturno@nmviajes.com" '"counterturno@gruponuevomundo.com.pe"

        Public Const emailEasySolutions As String = "ccanales@gruponuevomundo.com.pe"
        
        Public Const IPCorreos As String = "10.75.102.2"
        Public Const TokenCorreos As String = " EasyTicketing"

        Public Const NombreCorreoEasyReporte As String = " EASY REPORTE"
        Public Const NombreCorreoPrivadas As String = " EASY ONLINE PRIVADAS"
        Public Const NombreCorreoPublicadas As String = " EASY ONLINE PUBLICADAS"
#End Region
#Region "Origen Reporte"
        Public Const EmisionGDS As String = "EmisionGDS"
        Public Const Duplicados As String = "Duplicados"
        Public Const EmisionAgente As String = "EmisionAgente"
#End Region
#Region "CotizacionEmision"
        Public Const Emitir As String = "Emitir"
        Public Const NoEmitir As String = "NoEmitir"
#End Region
#Region "FirmasSabre"
        Public Const FirmaMotorEmisionWeb As String = "MotorEmisionWeb"
#End Region
#Region "NombreArchivosLog"
        Public Const GNM_GenerardorOADP As String = "GNM_GenerardorOADP.txt"
        Public Const GNM_EnvioCorreoOADP As String = "GNM_EnvioCorreoOADP.txt"
        Public Const GNM_ImprimeOADP As String = "GNM_ImprimeOADP.txt"
        Public Const GNM_ReporteEMDs As String = "GNM_ReporteEMDs.txt"
#End Region
#Region "CodigoSeguimiento"
        Public Const CodSegReporteEMDs As String = "GNM_ReporteEMDs"
#End Region
#Region "CodigoSeguimiento"
        Public Const GNM_RutaLogROBOT As String = "C:\ROBOT_ANULACION\GNM_ROBOTANULACIONES.txt"
#End Region
#Region "Listas"
        'Public Shared Sub RemoveDuplicates(Of T)(list As IList(Of T))
        '    If list Is Nothing Then
        '        Return
        '    End If
        '    Dim i As Integer = 1
        '    While i < list.Count
        '        Dim j As Integer = 0
        '        Dim remove As Boolean = False
        '        While j < i AndAlso Not remove
        '            If list(i).Equals(list(j)) Then
        '                remove = True
        '            End If
        '            j += 1
        '        End While
        '        If remove Then
        '            list.RemoveAt(i)
        '        Else
        '            i += 1
        '        End If
        '    End While
        'End Sub
#End Region
#Region "ROBOT_GENERADOR_TICKET"
        Public Const SP_INSERT_BOLETO As String = "NUEVOMUNDO.PKG_GDS_FACTURACION.SP_GDS_GBINSERT_BOLETO"
        Public Const SP_GETBOLETO_GENERADOR As String = "NUEVOMUNDO.PKG_GDS_FACTURACION.SP_GDS_GBGETBOLETO_GENERADOR"
        Public Const SP_INSERT_BOLETO_CT As String = EsquemaCondor & ". PKG_GDS_FACTURACION.SP_GDS_GBINSERT_BOLETO"
        Public Const SP_GETBOLETO_GENERADOR_CT As String = EsquemaCondor & ". PKG_GDS_FACTURACION.SP_GDS_GBGETBOLETO_GENERADOR"
        Public Const SP_GDS_DELETEBOLETOS_CT As String = EsquemaCondor & ". PKG_GDS_FACTURACION.SP_GDS_DELETEBOLETOS"
        Public Const SP_GDS_DELETEBOLETOS As String = "NUEVOMUNDO.PKG_GDS_FACTURACION.SP_GDS_DELETEBOLETOS"
#End Region
#Region "Stored ProcedureMetasDestinosMundiales"
        Public Const GDS_MC_INS_VENTA As String = "PTADESTINOS.PKG_WEB_REP_METAS_DM.GDS_MC_INS_VENTA"
#End Region

#Region "ROBOT_REGENERADOR"
        Public Const SP_GDS_CONSULTA_CCCF As String = "NUEVOMUNDO.PKG_GDS_WEBSERVICEPTA.GDS_CONSULTA_CCCF"
        Public Const SP_GDS_CONSULTA_CCCF_CT As String = EsquemaCondor & ". PKG_GDS_WEBSERVICEPTA.GDS_CONSULTA_CCCF"
#End Region

    End Class
End Namespace