<Serializable()> _
Public Class classEjecutivo
    Private strID_GSA As String = Nothing
    Private strNOMBRE As String = Nothing
    Private strDIRECCION As String = Nothing
    Private strLOCALIDAD As String = Nothing
    Private strTELEFONO As String = Nothing
    Private intPORCENTAJE_COMISION As Integer = 0
    Private intID_PROVEEDOR As Integer = 0
    Private intEN_DESUSO As Integer = 0
    Private intID_EMPRESA As Integer = 0
    Private intID_CLIENTE As Integer = 0
    Private strID_TIPO_DOCUMENTO_IDENTIDAD As String = Nothing
    Private strNUM_DOCUMENTO_IDENTIDAD As String = Nothing
    Private strEMAIL As String = Nothing
    Public Property ID_GSA() As String
        Get
            Return strID_GSA
        End Get
        Set(ByVal value As String)
            strID_GSA = value
        End Set
    End Property
    Public Property NOMBRE() As String
        Get
            Return strNOMBRE
        End Get
        Set(ByVal value As String)
            strNOMBRE = value
        End Set
    End Property
    Public Property DIRECCION() As String
        Get
            Return strDIRECCION
        End Get
        Set(ByVal value As String)
            strDIRECCION = value
        End Set
    End Property
    Public Property LOCALIDAD() As String
        Get
            Return strLOCALIDAD
        End Get
        Set(ByVal value As String)
            strLOCALIDAD = value
        End Set
    End Property
    Public Property TELEFONO() As String
        Get
            Return strTELEFONO
        End Get
        Set(ByVal value As String)
            strTELEFONO = value
        End Set
    End Property
    Public Property PORCENTAJE_COMISION() As Integer
        Get
            Return intPORCENTAJE_COMISION
        End Get
        Set(ByVal value As Integer)
            intPORCENTAJE_COMISION = value
        End Set
    End Property
    Public Property ID_PROVEEDOR() As Integer
        Get
            Return intID_PROVEEDOR
        End Get
        Set(ByVal value As Integer)
            intID_PROVEEDOR = value
        End Set
    End Property
    Public Property EN_DESUSO() As Integer
        Get
            Return intEN_DESUSO
        End Get
        Set(ByVal value As Integer)
            intEN_DESUSO = value
        End Set
    End Property
    Public Property ID_EMPRESA() As Integer
        Get
            Return intID_EMPRESA
        End Get
        Set(ByVal value As Integer)
            intID_EMPRESA = value
        End Set
    End Property
    Public Property ID_CLIENTE() As Integer
        Get
            Return intID_CLIENTE
        End Get
        Set(ByVal value As Integer)
            intID_CLIENTE = value
        End Set
    End Property
    Public Property ID_TIPO_DOCUMENTO_IDENTIDAD() As String
        Get
            Return strID_TIPO_DOCUMENTO_IDENTIDAD
        End Get
        Set(ByVal value As String)
            strID_TIPO_DOCUMENTO_IDENTIDAD = value
        End Set
    End Property
    Public Property NUM_DOCUMENTO_IDENTIDAD() As String
        Get
            Return strNUM_DOCUMENTO_IDENTIDAD
        End Get
        Set(ByVal value As String)
            strNUM_DOCUMENTO_IDENTIDAD = value
        End Set
    End Property
    Public Property EMAIL() As String
        Get
            Return strEMAIL
        End Get
        Set(ByVal value As String)
            strEMAIL = value
        End Set
    End Property
End Class
