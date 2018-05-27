<Serializable()> _
Public Class classSolicitudReemision
    Property intCodigoReemision As Integer
    Property intCodigoWeb As Integer
    Property intCodigoLenguaje As Integer
    Property intCodigoUsuarioWeb As Integer
    Property strCodigoPNR As String
    Property strTipoReserva As String
    Property strNombrePromotor As String
    Property intEstado As Integer
    Property intOficinaDestinos As Integer
    Property strFechaReemision As String
    Property strTipoTarifa As String
    Property intDepartamentoDestino As Integer
    Property intSistemaOrigen As Integer
    Property intSubCodigo As Integer
    Property strObservacion As String
    Property objGenerales As classGenerales
    Property objCalificadores As classCalificadores
    Property objTarifa As classReeTarifa
    Property objPago As classPago

    <Serializable()> _
    Public Class classGenerales
        Property strItinerarioOriginal As String
        Property strItinerarioReemision As String
        Property strBoletoOriginal As String
    End Class

    <Serializable()> _
    Public Class classCalificadores
        Property strTipoTarifa As String
        Property strTipoPasajero As String
        Property strNumeroPasajero As String
        Property strAerolinea As String
        Property strNumeroBoleto As String
        Property strSegmentos As String
        Property strTourCode As String
        Property strAccountCode As String
        Property strCorporateId As String
    End Class

    <Serializable()> _
    Public Class classReeTarifa
        Property intTipoConsulta As Integer
        Property strTarifaBaseOriginal As String
        Property strImpuestoOriginal As String
        Property strTotalOriginal As String
        Property strTarifaBaseNuevo As String
        Property strImpuestoNuevo As String
        Property strTotalNuevo As String
        Property strTarifaBaseReemision As String
        Property strImpuestoReemision As String
        Property strTotalReemision As String
        Property strTarifaTotal As String
        Property strTotalPenalidad As String
        Property strTotalFee As String
        Property strTotalAPagar As String
        Property strDiferenciaTarifa As String

        Property strMensajeMostrado As String
        Property strTipoPasajero As String
        Property strUltimaFechaEmision As String
        Property strLineaValidadora As String
        Property strInformacionTarifa As String
        Property strItinerarioTarifa As String
    End Class

    <Serializable()> _
    Public Class classPago
        Property intCodigoPago As String
        Property strTipoPago As String
        Property strTipoTarjeta As String
        Property strNumeroTarjeta As String
        Property strFechaVencimientoTarjeta As String
        Property strTitularTarjeta As String
        Property intPaisTarjeta As Integer
        Property strNombrePaisTarjeta As String
        Property strBancoTarjeta As String
        Property strTipoDocumentoTarjeta As String
        Property strNumeroDocumentoTarjeta As String
        Property strCodigoSeguridadTarjeta As String
        Property strDepositoHasta As String
        Property strEmailCaja As String
        Property objDeposito As List(Of classDeposito)
    End Class

    <Serializable()> _
    Public Class classDeposito
        Property strTipoOperacion As String
        Property strNombreBanco As String
        Property strSucursalBanco As String
        Property strNumeroOperacion As String
        Property strReferenciaOperacion As String
        Property strMontoOperacion As String
        Property strFechaOperacion As String
        Property strHoraOperacion As String
        Property strRutaImagen As String
        Property strCuenta As String
    End Class
End Class
