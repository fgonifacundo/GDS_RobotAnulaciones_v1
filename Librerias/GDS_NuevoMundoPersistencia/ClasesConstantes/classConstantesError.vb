Namespace Constantes
    Partial Public Class classConstantes
#Region "PNR"
        Public Const NoExisteSegmentoActivo As String = "No existen segmentos activos en el PNR"
        Public Const LocalizaroLineaAerea As String = "No se encontró localizador de la aerolínea"
        Public Const StatusDiferente As String = "Al menos un segmento no tiene status HK"
        Public Const PasajeroSinNombre As String = "No se encontro nombre para el pasajero"
        Public Const PasajeroSinApellido As String = "No se encontro apellido para el pasajero"
#End Region
#Region "SWS"
        Public Const ProblemasDQB As String = "Se presento error al momento de ejecutar el DQB"
        Public Const ProblemasModifyInfo As String = "Se presento error al momento de modificar el PNR"
        Public Const ProblemasOTA_AirPrice As String = "Se presento error al momento de cotizar el PNR"
        Public Const ProblemasVOID_Ticket As String = "Se presento error al momento de voidear el boleto"
        Public Const ProblemasRemark As String = "Se presento error al momento de ingresar Remark"
        Public Const ProblemasAirRules As String = "Se presento error al momento de recuperar las regulaciones"
        Public Const ProblemasET As String = "Se presento error al momento ejecutar en servicio ET"
#End Region
#Region "ErrorSabre"
        Public Const msgINFANT_DETAILS_REQUIRED_SSR As String = "INFANT DETAILS REQUIRED IN SSR" ' "INFANT DETAILS REQUIRED IN SSR - ENTER 3INFT/..."
#End Region
#Region "ErrorValidación"
        Public Const ErrorCampoFechaNacimientoVacio As String = "Ingrese la fecha de nacimiento."
        Public Const ErrorCampoGenero As String = "Seleccione el género."
        Public Const ErrorCampoTipoPasajero As String = "Seleccione el tipo de pasajero."
        Public Const ErrorCampoNumeroDocumentoPasajeroVacio As String = "Ingrese el número de documento."


        Public Const ErrorCampoFormaPago As String = "Seleccional la forma de pago."
        Public Const ErrorCampoTipoTarjeta As String = "Seleccional el tipo de tarjeta con la cual se realizará la emisión."
        Public Const ErrorCampoNumeroTarjeta As String = "El campo número de tarjeta contiene un caracter no numérico."
        Public Const ErrorCampoNumeroTarjetaLength As String = "El campo número de tarjeta no contiene la cantidad de caracteres requeridos."
        Public Const ErrorCampoNumeroTarjetaVacio As String = "Ingresar el número de la tarjeta."
        Public Const ErrorCampoCodigoVerificacion As String = "El campo código de verificación contiene un caracter no numérico."
        Public Const ErrorCampoCodigoVerificacionLength As String = "El campo código de verificación no contiene la cantidad de caracteres requeridos."
        Public Const ErrorCampoCodigoVerificacionVacio As String = "Ingresar el código de verificación de la tarjera."
        Public Const ErrorCampoNombreBancoVacio As String = "Ingresar el nombre del banco que expide la tarjeta."
        Public Const ErrorCampoNombrePais As String = "Seleccionar el nombre del país de la tarjeta."
        Public Const ErrorCampoFechaVencimientoCaracterNoPermitido As String = "El campofecha de vencimiento de la tarjeta tiene un caracter no permitido."
        Public Const ErrorCampoFechaVencimientoIncorrecto As String = "La fecha de vencimiento de la tarjeta no es correcta."
        Public Const ErrorCampoFechaVencimientoVacio As String = "Ingresar la fecha de vencimiento de la tarjeta."
        Public Const ErrorCampoNombreTitularTarjetaVacio As String = "Ingresar nombre del titular de la tarjeta de la tarjeta."
        Public Const ErrorCampoTipoDocumentoIdentidad As String = "Seleccionar el tipo de documento de identidad."
        Public Const ErrorCampoNumeroDocumentoIdentidad As String = "Ingresar el número de documento de identidad."
        Public Const ErrorCampoMontoCash As String = "Ingresar el monto a pagar al cash."
        Public Const ErrorCampoTarifaManualCash As String = "Ingresar el monto de la reserva."


        Public Const ErrorCampoTipoOperacionVacio As String = "Seleccione el tipo de operación realizada."
        Public Const ErrorCampoBancoVacio As String = "Seleccione el banco donde realizó el deposito."
        Public Const ErrorCampoSucursalVacio As String = "Ingrese el nombre de la sucursal del banco donde realizó el deposito."
        Public Const ErrorCampoOperacionVacio As String = "Ingrese el número de operación del depósito."
        Public Const ErrorCampoReferenciaVacio As String = "Ingrese la referencia del depósito."
        Public Const ErrorCampoImporteValido As String = "Ingrese un importe de deposito válido."
        Public Const ErrorCampoImporteVacio As String = "Ingrese el importe depositado."
        Public Const ErrorCampoFechaDepositoVacio As String = "Ingrese la fecha en la que realizó el depositado."
        Public Const ErrorCampoFechaDepositoValido As String = "Ingrese un fecha válida para el depositado."
        Public Const ErrorCampoFechaDepositoMayor As String = "La fecha de deposito deber ser menor o igual a la fecha actual."
        Public Const ErrorCampoHoraDepositoVacio As String = "Ingresar la hora del deposito."
        Public Const ErrorCampoHoraDepositoValido As String = "Ingresar una hora válida para el depósito."
        Public Const ErrorCampoHoraDepositoMayor As String = "La hora de deposito deber ser menor o igual a la hora actual."


#End Region

    End Class
End Namespace