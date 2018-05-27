Namespace robotBoletoPendientePago
    <Serializable()> _
    Public Class robotBoletoPendiente
        Private intIdSucursal As Integer = 0
        Private strDescripcion As String = Nothing
        Private intIdCliente As Integer = 0
        Private strNombreCliente As String = Nothing
        Private objPromotor As classPromotor = Nothing
        Private strIdCondicionPago As String = Nothing
        Private strFile As String = Nothing
        Private strIdVendedor As String = Nothing
        Private objVendedor As classDatosAgente = Nothing
        Private strPNR As String = Nothing
        Private strPrefijoBoleto As String = Nothing
        Private strNumeroBoleto As String = Nothing
        Private strGds As String = Nothing
        Private strRuta As String = Nothing
        Private strNoAnular As String = Nothing
        Private strFechaAltaPTA As String = Nothing
        Private strMontoVenta As String = Nothing
        Private strMontoAplicado As String = Nothing
        Private strMontoPendiente As String = Nothing
        Private strMontoOtroDK As String = Nothing
        Private bolMarca As Boolean = False
        Private strEstado As String = Nothing
        Private strID_FacturaCabeza As String = Nothing
        Private strIdProveedor As String = Nothing
        Private strFechaEmision As String = Nothing
        Private objCliente As classCliente = Nothing
        Private intMarcaFacturado As Integer = 0
        Private intMarcaVoid As Integer = 0
        Private strNombrePasajero As String = Nothing
        Private strPseudo As String = Nothing
        Private strNombrePseudo As String = Nothing
        Private strHora As String = Nothing
        Private dblMontoPendiente As Double = 0
        Private dblMontoOtroDK As Double = 0
        Private strIdTipoComprobante As String = Nothing
        Private strNumeroSerie1 As String = Nothing
        Private strComprobante As String = Nothing
        Private strTotalPendiente As Double = 0.0
        Private strMensajeError As String = Nothing
        Private strPseudoRelease As String = Nothing
        Private strEstadoAct As String = Nothing
        Private boolExisteEnPTA As Boolean = True
        Private boolEmisionPseudoNM As Boolean = False

        Property ExistePTA() As Boolean
            Get
                Return boolExisteEnPTA
            End Get
            Set(ByVal value As Boolean)
                boolExisteEnPTA = value
            End Set
        End Property

        Property EmisionPseudoNM() As Boolean
            Get
                Return boolEmisionPseudoNM
            End Get
            Set(ByVal value As Boolean)
                boolEmisionPseudoNM = value
            End Set
        End Property

        Property Estado() As String
            Get
                Return strEstado
            End Get
            Set(ByVal value As String)
                strEstado = value
            End Set
        End Property

        Property EstadoAct() As String
            Get
                Return strEstadoAct
            End Get
            Set(ByVal value As String)
                strEstadoAct = value
            End Set
        End Property

        Property ID_FacturaCabeza() As String
            Get
                Return strID_FacturaCabeza
            End Get
            Set(ByVal value As String)
                strID_FacturaCabeza = value
            End Set
        End Property
        Property IdSucursal() As Integer
            Get
                Return intIdSucursal
            End Get
            Set(ByVal value As Integer)
                intIdSucursal = value
            End Set
        End Property
        Property Descripcion() As String
            Get
                Return strDescripcion
            End Get
            Set(ByVal value As String)
                strDescripcion = value
            End Set
        End Property
        Property IdCliente() As Integer
            Get
                Return intIdCliente
            End Get
            Set(ByVal value As Integer)
                intIdCliente = value
            End Set
        End Property
        Property NombreCliente() As String
            Get
                Return strNombreCliente
            End Get
            Set(ByVal value As String)
                strNombreCliente = value
            End Set
        End Property
        Property Promotor() As classPromotor
            Get
                Return objPromotor
            End Get
            Set(ByVal value As classPromotor)
                objPromotor = value
            End Set
        End Property
        Property IdCondicionPago() As String
            Get
                Return strIdCondicionPago
            End Get
            Set(ByVal value As String)
                strIdCondicionPago = value
            End Set
        End Property
        Property File() As String
            Get
                Return strFile
            End Get
            Set(ByVal value As String)
                strFile = value
            End Set
        End Property
        Property IdVendedor() As String
            Get
                Return strIdVendedor
            End Get
            Set(ByVal value As String)
                strIdVendedor = value
            End Set
        End Property
        Property Vendedor() As classDatosAgente
            Get
                Return objVendedor
            End Get
            Set(ByVal value As classDatosAgente)
                objVendedor = value
            End Set
        End Property

        Property PNR() As String
            Get
                Return strPNR
            End Get
            Set(ByVal value As String)
                strPNR = value
            End Set
        End Property
        Property PrefijoBoleto() As String
            Get
                Return strPrefijoBoleto
            End Get
            Set(ByVal value As String)
                strPrefijoBoleto = value
            End Set
        End Property
        Property NumeroBoleto() As String
            Get
                Return strNumeroBoleto
            End Get
            Set(ByVal value As String)
                strNumeroBoleto = value
            End Set
        End Property
        Property Gds() As String
            Get
                Return strGds
            End Get
            Set(ByVal value As String)
                strGds = value
            End Set
        End Property
        Property Ruta() As String
            Get
                Return strRuta
            End Get
            Set(ByVal value As String)
                strRuta = value
            End Set
        End Property
        Property NoAnular() As String
            Get
                Return strNoAnular
            End Get
            Set(ByVal value As String)
                strNoAnular = value
            End Set
        End Property
        Property FechaAltaPTA() As String
            Get
                Return strFechaAltaPTA
            End Get
            Set(ByVal value As String)
                strFechaAltaPTA = value
            End Set
        End Property
        Property MontoVenta() As String
            Get
                Return strMontoVenta
            End Get
            Set(ByVal value As String)
                strMontoVenta = value
            End Set
        End Property
        Property MontoAplicado() As String
            Get
                Return strMontoAplicado
            End Get
            Set(ByVal value As String)
                strMontoAplicado = value
            End Set
        End Property
        Property MontoPendiente() As String
            Get
                Return strMontoPendiente
            End Get
            Set(ByVal value As String)
                strMontoPendiente = value
            End Set
        End Property
        Property MontoOtroDK() As String
            Get
                Return strMontoOtroDK
            End Get
            Set(ByVal value As String)
                strMontoOtroDK = value
            End Set
        End Property
        Property Marca() As String
            Get
                Return bolMarca
            End Get
            Set(ByVal value As String)
                bolMarca = value
            End Set
        End Property

        Property IdProveedor() As String
            Get
                Return strIdProveedor
            End Get
            Set(ByVal value As String)
                strIdProveedor = value
            End Set
        End Property

        Property Cliente() As classCliente
            Get
                Return objCliente
            End Get
            Set(ByVal value As classCliente)
                objCliente = value
            End Set
        End Property

        Property FechaEmision() As String
            Get
                Return strFechaEmision
            End Get
            Set(ByVal value As String)
                strFechaEmision = value
            End Set
        End Property
        Property MarcaFacturado() As Integer
            Get
                Return intMarcaFacturado
            End Get
            Set(ByVal value As Integer)
                intMarcaFacturado = value
            End Set
        End Property

        Property MarcaVoid() As Integer
            Get
                Return intMarcaVoid
            End Get
            Set(ByVal value As Integer)
                intMarcaVoid = value
            End Set
        End Property

        Property NombrePasajero() As String
            Get
                Return strNombrePasajero
            End Get
            Set(ByVal value As String)
                strNombrePasajero = value
            End Set
        End Property

        Property Pseudo() As String
            Get
                Return strPseudo
            End Get
            Set(ByVal value As String)
                strPseudo = value
            End Set
        End Property
        Property NombrePseudo() As String
            Get
                Return strNombrePseudo
            End Get
            Set(ByVal value As String)
                strNombrePseudo = value
            End Set
        End Property

        Property Hora() As String
            Get
                Return strHora
            End Get
            Set(ByVal value As String)
                strHora = value
            End Set
        End Property

        Property MontoPendiente1() As Double
            Get
                Return dblMontoPendiente
            End Get
            Set(ByVal value As Double)
                dblMontoPendiente = value
            End Set
        End Property

        Property MontoOtroDK1() As Double
            Get
                Return dblMontoOtroDK
            End Get
            Set(ByVal value As Double)
                dblMontoOtroDK = value
            End Set
        End Property

        Property IdTipoComprobante() As String
            Get
                Return strIdTipoComprobante
            End Get
            Set(ByVal value As String)
                strIdTipoComprobante = value
            End Set
        End Property

        Property NumeroSerie1() As String
            Get
                Return strNumeroSerie1
            End Get
            Set(ByVal value As String)
                strNumeroSerie1 = value
            End Set
        End Property

        Property Comprobante() As String
            Get
                Return strComprobante
            End Get
            Set(ByVal value As String)
                strComprobante = value
            End Set
        End Property
        Property TotalPendiente() As Double
            Get
                Return strTotalPendiente
            End Get
            Set(ByVal value As Double)
                strTotalPendiente = value
            End Set
        End Property

        Property MensajeError() As String
            Get
                Return strMensajeError
            End Get
            Set(value As String)
                strMensajeError = value
            End Set
        End Property

        Property PseudoRelease() As String
            Get
                Return strPseudoRelease
            End Get
            Set(value As String)
                strPseudoRelease = value
            End Set
        End Property

    End Class
    'strComprobante


    <Serializable()> _
    Public Class robotBoletoPagoOtroDk
        Private intIdCliente As Integer = 0
        Private strNombreCliente As String = Nothing
        Private strIdCondicionPago As String = Nothing
        Private strFile As String = Nothing
        Private strPNR As String = Nothing
        Private strNumeroBoleto As String = Nothing
        Private strRuta As String = Nothing
        Private strFechaAltaPTA As String = Nothing
        Private strMontoPendiente As String = Nothing
        Private strMontoPagoOtroDk As String = Nothing
        Private intOtroDk As Integer = 0
        Private strNombreOtroDk As String = Nothing
        Private bolMarca As Boolean = False
        Property IdCliente() As Integer
            Get
                Return intIdCliente
            End Get
            Set(ByVal value As Integer)
                intIdCliente = value
            End Set
        End Property
        Property NombreCliente() As String
            Get
                Return strNombreCliente
            End Get
            Set(ByVal value As String)
                strNombreCliente = value
            End Set
        End Property
        Property IdCondicionPago() As String
            Get
                Return strIdCondicionPago
            End Get
            Set(ByVal value As String)
                strIdCondicionPago = value
            End Set
        End Property
        Property File() As String
            Get
                Return strFile
            End Get
            Set(ByVal value As String)
                strFile = value
            End Set
        End Property
        Property PNR() As String
            Get
                Return strPNR
            End Get
            Set(ByVal value As String)
                strPNR = value
            End Set
        End Property
        Property NumeroBoleto() As String
            Get
                Return strNumeroBoleto
            End Get
            Set(ByVal value As String)
                strNumeroBoleto = value
            End Set
        End Property
        Property Ruta() As String
            Get
                Return strRuta
            End Get
            Set(ByVal value As String)
                strRuta = value
            End Set
        End Property
        Property FechaAltaPTA() As String
            Get
                Return strFechaAltaPTA
            End Get
            Set(ByVal value As String)
                strFechaAltaPTA = value
            End Set
        End Property
        Property MontoPendiente() As String
            Get
                Return strMontoPendiente
            End Get
            Set(ByVal value As String)
                strMontoPendiente = value
            End Set
        End Property
        Property MontoPagoOtroDk() As String
            Get
                Return strMontoPagoOtroDk
            End Get
            Set(ByVal value As String)
                strMontoPagoOtroDk = value
            End Set
        End Property
        Property OtroDk() As Integer
            Get
                Return intOtroDk
            End Get
            Set(ByVal value As Integer)
                intOtroDk = value
            End Set
        End Property
        Property NombreOtroDk() As String
            Get
                Return strNombreOtroDk
            End Get
            Set(ByVal value As String)
                strNombreOtroDk = value
            End Set
        End Property

        Property Marca() As String
            Get
                Return bolMarca
            End Get
            Set(ByVal value As String)
                bolMarca = value
            End Set
        End Property

  
    End Class
End Namespace