<Serializable()> _
Public Class classCliente
    Private intDK As Integer = 0
    Private strRazonSocial As String = Nothing
    Private strNombreComercial As String = Nothing
    Private strTipo_Documento As String = Nothing
    Private strDocumento As String = Nothing
    Private strOficina As String = Nothing
    Private strTelefono As String = Nothing
    Private strEmailPTA As String = Nothing
    Private intIdWeb As Integer = -1
    Private lstSubCodigo As List(Of classSubCodigo) = Nothing
    Private objUsuario As classUsuario = Nothing
    Private objSucursalPunto As classSucursalPunto = Nothing
    Private strDireccion As String = Nothing
    Private objPromotor As classPromotor = Nothing
    Private objPermisos As classPermisos = Nothing
    Private objClienteFacturar As classClienteFacturar = Nothing
    Private objLineaCredito As classLineaCredito = Nothing
    Private objDocumentosVencidos As classDocumentosVencidos = Nothing
    Private intIdEmpresa As Integer = -1
    Private intTipoDeCliente As Integer = -1
    Private objGDS_Interface As classGDS_Interface = Nothing
    Private strCondicion As String = Nothing
    Private objGrupoInterno As classGrupoInterno = Nothing
    Private intEnDesuso As Integer = -1
    Private intLogo As Integer = 0
    Private strEmailAgencia As String = Nothing

    Public Property DK() As Integer
        Get
            Return intDK
        End Get
        Set(ByVal value As Integer)
            intDK = value
        End Set
    End Property
    Public Property IdWeb() As Integer
        Get
            Return intIdWeb
        End Get
        Set(ByVal value As Integer)
            intIdWeb = value
        End Set
    End Property
    Public Property SubCodigo() As List(Of classSubCodigo)
        Get
            Return lstSubCodigo
        End Get
        Set(ByVal value As List(Of classSubCodigo))
            lstSubCodigo = value
        End Set
    End Property
    Public Property ClienteFacturar() As classClienteFacturar
        Get
            Return objClienteFacturar
        End Get
        Set(ByVal value As classClienteFacturar)
            objClienteFacturar = value
        End Set
    End Property
    Public Property DocumentoVencidos() As classDocumentosVencidos
        Get
            Return objDocumentosVencidos
        End Get
        Set(ByVal value As classDocumentosVencidos)
            objDocumentosVencidos = value
        End Set
    End Property
    Public Property GDS_Interface() As classGDS_Interface
        Get
            Return objGDS_Interface
        End Get
        Set(ByVal value As classGDS_Interface)
            objGDS_Interface = value
        End Set
    End Property
    Public Property LineaCredito() As classLineaCredito
        Get
            Return objLineaCredito
        End Get
        Set(ByVal value As classLineaCredito)
            objLineaCredito = value
        End Set
    End Property
    Public Property Permisos() As classPermisos
        Get
            Return objPermisos
        End Get
        Set(ByVal value As classPermisos)
            objPermisos = value
        End Set
    End Property
    Public Property Promotor() As classPromotor
        Get
            Return objPromotor
        End Get
        Set(ByVal value As classPromotor)
            objPromotor = value
        End Set
    End Property
    Public Property SucursalPunto() As classSucursalPunto
        Get
            Return objSucursalPunto
        End Get
        Set(ByVal value As classSucursalPunto)
            objSucursalPunto = value
        End Set
    End Property
    Public Property Usuario() As classUsuario
        Get
            Return objUsuario
        End Get
        Set(ByVal value As classUsuario)
            objUsuario = value
        End Set
    End Property
    Public Property Direccion() As String
        Get
            Return strDireccion
        End Get
        Set(ByVal value As String)
            strDireccion = value
        End Set
    End Property
    Public Property EmailPTA() As String
        Get
            Return strEmailPTA
        End Get
        Set(ByVal value As String)
            strEmailPTA = value
        End Set
    End Property
    Public Property IdEmpresa() As Integer
        Get
            Return intIdEmpresa
        End Get
        Set(ByVal value As Integer)
            intIdEmpresa = value
        End Set
    End Property
    Public Property TipoDeCliente() As Integer
        Get
            Return intTipoDeCliente
        End Get
        Set(ByVal value As Integer)
            intTipoDeCliente = value
        End Set
    End Property
    Public Property NombreComercial() As String
        Get
            Return strNombreComercial
        End Get
        Set(ByVal value As String)
            strNombreComercial = value
        End Set
    End Property
    Public Property Oficina() As String
        Get
            Return strOficina
        End Get
        Set(ByVal value As String)
            strOficina = value
        End Set
    End Property
    Public Property Tipo_Documento() As String
        Get
            Return strTipo_Documento
        End Get
        Set(ByVal value As String)
            strTipo_Documento = value
        End Set
    End Property
    Public Property Documento() As String
        Get
            Return strDocumento
        End Get
        Set(ByVal value As String)
            strDocumento = value
        End Set
    End Property
    Public Property RazonSocial() As String
        Get
            Return strRazonSocial
        End Get
        Set(ByVal value As String)
            strRazonSocial = value
        End Set
    End Property
    Public Property Telefono() As String
        Get
            Return strTelefono
        End Get
        Set(ByVal value As String)
            strTelefono = value
        End Set
    End Property
    Public Property Condicion() As String
        Get
            Return strCondicion
        End Get
        Set(ByVal value As String)
            strCondicion = value
        End Set
    End Property
    Public Property GrupoInterno() As classGrupoInterno
        Get
            Return objGrupoInterno
        End Get
        Set(ByVal value As classGrupoInterno)
            objGrupoInterno = value
        End Set
    End Property
    Public Property EnDesuso() As Integer
        Get
            Return intEnDesuso
        End Get
        Set(ByVal value As Integer)
            intEnDesuso = value
        End Set
    End Property

    Public Property Logo() As Integer
        Get
            Return intLogo
        End Get
        Set(ByVal value As Integer)
            intLogo = value
        End Set
    End Property

    Public Property EmailAgencia() As String
        Get
            Return strEmailAgencia
        End Get
        Set(ByVal value As String)
            strEmailAgencia = value
        End Set
    End Property
End Class
