''' <summary>
''' La clase classEasyOnLine se utiliza para guardar la información o datos que serán utilizados
''' durante todo el procesos de emisión o solicitud que se envia al counter
''' </summary>
''' <remarks>Usuario hsanchez / Febrero del 2014</remarks>
<Serializable()> _
Public Class classEasyOnLine
    ''' <summary>
    ''' El Objeto classCliente almacenará los datos del cliente que ingresa a la web
    ''' </summary>
    ''' <remarks>
    ''' En el caso de la web del Interagencias (extranet) se almacena los datos de la agencias la cual esta realizando la emisión.
    ''' 
    ''' En el caso de la Intranet se almacenan los datos de la empresa que realizará la emisón, los datos de su cliente serán almacenados en el  objeto classClienteFacturar.
    ''' </remarks>
    Private strPNR As String = Nothing
    Private intOpcionFirmaSabre As Integer = 0
    Private strCodigoSegimiento As String = Nothing
    Private objCliente As classCliente = Nothing
    Private objFormaPago As classFormaPago = Nothing
    Private objFacturacion As classFacturacion = Nothing
    Private objTipoTarifa As classDato = Nothing
    Private objGDS As classDato = Nothing
    Private strPseudoOrigen As String = Nothing
    Private strPseudoEmision As String = Nothing
    Private strIATA As String = Nothing
    Private objCotizacion As classCotizacion = Nothing
    Private objSolicitud As classSolicitud = Nothing
    Private objDocFacturacion As classDocFacturacion = Nothing
    Private objTipoFormaPago As List(Of classDato) = Nothing
    Private objTipoPasajero As List(Of classDatosTipoPasajero) = Nothing
    Private objTipoOperaciones As List(Of classDato) = Nothing
    Private objTipoTarifaSolicitud As List(Of classDato) = Nothing
    Private objTipoReservaSolicitud As List(Of classDato) = Nothing
    Private objCuentaBancaria As List(Of classCuentaBancaria) = Nothing
    Private objTipoDocumentoEmision As List(Of classTipoDocumentoEmision) = Nothing
    Private objTipoDocumentoTarjeta As List(Of classTipoDocumentoEmision) = Nothing
    Private objPais As List(Of classPais) = Nothing
    Private objErroresAlertas As classErroresAlertas = Nothing
    Private objReserva As classPNR = Nothing
    Private objSession As classSession = Nothing
    Private lstTourCodeResultado As List(Of classTourCodeResultado) = Nothing
    Private objFeeOpt As classFeeOpt.FeeOpt = Nothing
    Private objMonto As String = Nothing
    Private objPasajeros As List(Of classPasajeros) = Nothing
    Private objSegmentos As List(Of classSegmentos) = Nothing

    'Private objArregloPasajeros As Array = Nothing
    'Private objTarifa As classTarifa = Nothing

    Public Property PNR() As String
        Get
            Return strPNR
        End Get
        Set(ByVal value As String)
            strPNR = value
        End Set
    End Property
    Public Property OpcionFirmaSabre() As Integer
        Get
            Return intOpcionFirmaSabre
        End Get
        Set(ByVal value As Integer)
            intOpcionFirmaSabre = value
        End Set
    End Property
    Public Property CodigoSegimiento() As String
        Get
            Return strCodigoSegimiento
        End Get
        Set(ByVal value As String)
            strCodigoSegimiento = value
        End Set
    End Property
    Public Property Cliente() As classCliente
        Get
            Return objCliente
        End Get
        Set(ByVal value As classCliente)
            objCliente = value
        End Set
    End Property
    Public Property Cotizacion() As classCotizacion
        Get
            Return objCotizacion
        End Get
        Set(ByVal value As classCotizacion)
            objCotizacion = value
        End Set
    End Property
    Public Property DocFacturacion() As classDocFacturacion
        Get
            Return objDocFacturacion
        End Get
        Set(ByVal value As classDocFacturacion)
            objDocFacturacion = value
        End Set
    End Property
    Public Property FormaPago() As classFormaPago
        Get
            Return objFormaPago
        End Get
        Set(ByVal value As classFormaPago)
            objFormaPago = value
        End Set
    End Property
    Public Property Facturacion() As classFacturacion
        Get
            Return objFacturacion
        End Get
        Set(ByVal value As classFacturacion)
            objFacturacion = value
        End Set
    End Property
    Public Property GDS() As classDato
        Get
            Return objGDS
        End Get
        Set(ByVal value As classDato)
            objGDS = value
        End Set
    End Property
    Public Property Solicitud() As classSolicitud
        Get
            Return objSolicitud
        End Get
        Set(ByVal value As classSolicitud)
            objSolicitud = value
        End Set
    End Property
    Public Property TipoTarifa() As classDato
        Get
            Return objTipoTarifa
        End Get
        Set(ByVal value As classDato)
            objTipoTarifa = value
        End Set
    End Property
    Public Property IATA() As String
        Get
            Return strIATA
        End Get
        Set(ByVal value As String)
            strIATA = value
        End Set
    End Property
    Public Property PseudoEmision() As String
        Get
            Return strPseudoEmision
        End Get
        Set(ByVal value As String)
            strPseudoEmision = value
        End Set
    End Property
    Public Property PseudoOrigen() As String
        Get
            Return strPseudoOrigen
        End Get
        Set(ByVal value As String)
            strPseudoOrigen = value
        End Set
    End Property
    Public Property TipoFormaPago() As List(Of classDato)
        Get
            Return objTipoFormaPago
        End Get
        Set(ByVal value As List(Of classDato))
            objTipoFormaPago = value
        End Set
    End Property
    Public Property TipoOperaciones() As List(Of classDato)
        Get
            Return objTipoOperaciones
        End Get
        Set(ByVal value As List(Of classDato))
            objTipoOperaciones = value
        End Set
    End Property
    Public Property TipoTarifaSolicitud() As List(Of classDato)
        Get
            Return objTipoTarifaSolicitud
        End Get
        Set(ByVal value As List(Of classDato))
            objTipoTarifaSolicitud = value
        End Set
    End Property
    Public Property TipoReservaSolicitud() As List(Of classDato)
        Get
            Return objTipoReservaSolicitud
        End Get
        Set(ByVal value As List(Of classDato))
            objTipoReservaSolicitud = value
        End Set
    End Property
    Public Property CuentaBancaria() As List(Of classCuentaBancaria)
        Get
            Return objCuentaBancaria
        End Get
        Set(ByVal value As List(Of classCuentaBancaria))
            objCuentaBancaria = value
        End Set
    End Property
    Public Property TipoPasajero() As List(Of classDatosTipoPasajero)
        Get
            Return objTipoPasajero
        End Get
        Set(ByVal value As List(Of classDatosTipoPasajero))
            objTipoPasajero = value
        End Set
    End Property
    Public Property TipoDocumentoEmision() As List(Of classTipoDocumentoEmision)
        Get
            Return objTipoDocumentoEmision
        End Get
        Set(ByVal value As List(Of classTipoDocumentoEmision))
            objTipoDocumentoEmision = value
        End Set
    End Property
    Public Property TipoDocumentoTarjeta() As List(Of classTipoDocumentoEmision)
        Get
            Return objTipoDocumentoTarjeta
        End Get
        Set(ByVal value As List(Of classTipoDocumentoEmision))
            objTipoDocumentoTarjeta = value
        End Set
    End Property
    Public Property Pais() As List(Of classPais)
        Get
            Return objPais
        End Get
        Set(ByVal value As List(Of classPais))
            objPais = value
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
    Public Property Reserva() As classPNR
        Get
            Return objReserva
        End Get
        Set(ByVal value As classPNR)
            objReserva = value
        End Set
    End Property
    Public Property Session() As classSession
        Get
            Return objSession
        End Get
        Set(ByVal value As classSession)
            objSession = value
        End Set
    End Property
    Public Property TourCodeResultado() As List(Of classTourCodeResultado)
        Get
            Return lstTourCodeResultado
        End Get
        Set(ByVal value As List(Of classTourCodeResultado))
            lstTourCodeResultado = value
        End Set
    End Property
    Public Property FeeOpt() As classFeeOpt.FeeOpt
        Get
            Return objFeeOpt
        End Get
        Set(ByVal value As classFeeOpt.FeeOpt)
            objFeeOpt = value
        End Set
    End Property

    Public Property Monto() As String
        Get
            Return objMonto
        End Get
        Set(ByVal value As String)
            objMonto = value
        End Set
    End Property

    Public Property Pasajeros() As List(Of classPasajeros)
        Get
            Return objPasajeros
        End Get
        Set(value As List(Of classPasajeros))
            objPasajeros = value
        End Set
    End Property

    Public Property Segmentos() As List(Of classSegmentos)
        Get
            Return objSegmentos
        End Get
        Set(value As List(Of classSegmentos))
            objSegmentos = value
        End Set
    End Property

    'Public Property ArregloPasajeros() As Array
    '    Get
    '        Return objArregloPasajeros
    '    End Get
    '    Set(value As Array)
    '        objArregloPasajeros = value
    '    End Set
    'End Property


    'Public Property Tarifa() As classTarifa

    '    Get
    '        Return objTarifa
    '    End Get
    '    Set(value As classTarifa)
    '        objTarifa = value
    '    End Set
    'End Property

End Class
