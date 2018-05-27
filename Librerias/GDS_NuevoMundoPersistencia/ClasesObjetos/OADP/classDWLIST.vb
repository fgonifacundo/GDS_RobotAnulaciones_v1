<Serializable()> _
Public Class classDWLIST
    Private strPSEUDO As String = Nothing
    Private strITEM As String = Nothing
    Private strTIPO As String = Nothing
    Private strPNR As String = Nothing
    Private strDK As String = Nothing
    Private strRUC_CLIENTE As String = Nothing
    Private strDOCUMENT As String = "00000000"
    Private strINDICADOR As String = "BLC"
    Private strFECHA As String = Nothing
    Private strNUM_TICKET As String = "0000000000000"
    Private strNOMBRE_PAX As String = Nothing
    Private strAPELLIDO_PAX As String = Nothing
    Private strFIRMA_AGENTE As String = Nothing
    Private strRUTA_CARPETA As String = Nothing
    Private objCUENTAS_EMAIL As classCorreo = Nothing
    Private intENVIA_CORREO As Integer = 0
    Private intIMPRIME As Integer = 0
    Private intACTUALIZA_TPR As Integer = 0
    Private strCuerpoHTML As String = Nothing
    Private strFREQ_TVL_ID As String = Nothing
    Public Property PSEUDO() As String
        Get
            Return strPSEUDO
        End Get
        Set(ByVal value As String)
            strPSEUDO = value
        End Set
    End Property
    Public Property ITEM() As String
        Get
            Return strITEM
        End Get
        Set(ByVal value As String)
            strITEM = value
        End Set
    End Property
    Public Property TIPO() As String
        Get
            Return strTIPO
        End Get
        Set(ByVal value As String)
            strTIPO = value
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
    Public Property DK() As String
        Get
            Return strDK
        End Get
        Set(ByVal value As String)
            strDK = value
        End Set
    End Property
    Public Property RUC_CLIENTE() As String
        Get
            Return strRUC_CLIENTE
        End Get
        Set(ByVal value As String)
            strRUC_CLIENTE = value
        End Set
    End Property
    Public Property DOCUMENT() As String
        Get
            Return strDOCUMENT
        End Get
        Set(ByVal value As String)
            strDOCUMENT = value
        End Set
    End Property
    Public Property INDICADOR() As String
        Get
            Return strINDICADOR
        End Get
        Set(ByVal value As String)
            strINDICADOR = value
        End Set
    End Property
    Public Property FECHA() As String
        Get
            Return strFECHA
        End Get
        Set(ByVal value As String)
            strFECHA = value
        End Set
    End Property
    Public Property NUM_TICKET() As String
        Get
            Return strNUM_TICKET
        End Get
        Set(ByVal value As String)
            strNUM_TICKET = value
        End Set
    End Property
    Public Property NOMBRE_PAX() As String
        Get
            Return strNOMBRE_PAX
        End Get
        Set(ByVal value As String)
            strNOMBRE_PAX = value
        End Set
    End Property
    Public Property APELLIDO_PAX() As String
        Get
            Return strAPELLIDO_PAX
        End Get
        Set(ByVal value As String)
            strAPELLIDO_PAX = value
        End Set
    End Property
    Public Property FIRMA_AGENTE() As String
        Get
            Return strFIRMA_AGENTE
        End Get
        Set(ByVal value As String)
            strFIRMA_AGENTE = value
        End Set
    End Property
    Public Property RUTA_CARPETA() As String
        Get
            Return strRUTA_CARPETA
        End Get
        Set(ByVal value As String)
            strRUTA_CARPETA = value
        End Set
    End Property
    Public Property CUENTAS_EMAIL() As classCorreo
        Get
            Return objCUENTAS_EMAIL
        End Get
        Set(ByVal value As classCorreo)
            objCUENTAS_EMAIL = value
        End Set
    End Property
    Public Property ENVIA_CORREO() As Integer
        Get
            Return intENVIA_CORREO
        End Get
        Set(ByVal value As Integer)
            intENVIA_CORREO = value
        End Set
    End Property
    Public Property IMPRIME() As Integer
        Get
            Return intIMPRIME
        End Get
        Set(ByVal value As Integer)
            intIMPRIME = value
        End Set
    End Property
    Public Property ACTUALIZA_TPR() As Integer
        Get
            Return intACTUALIZA_TPR
        End Get
        Set(ByVal value As Integer)
            intACTUALIZA_TPR = value
        End Set
    End Property
    Public Property CuerpoHTML() As String
        Get
            Return strCuerpoHTML
        End Get
        Set(ByVal value As String)
            strCuerpoHTML = value
        End Set
    End Property
    Public Property FREQ_TVL_ID() As String
        Get
            Return strFREQ_TVL_ID
        End Get
        Set(ByVal value As String)
            strFREQ_TVL_ID = value
        End Set
    End Property
End Class
