Namespace Constantes
    Partial Public Class classConstantes
#Region "PNR"
        Public Const NoExisteSegmentoActivo As String = "No existen segmentos activos en el PNR"
        Public Const LocalizaroLineaAerea As String = "No se encontr� localizador de la aerol�nea"
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
#Region "ErrorValidaci�n"
        Public Const ErrorCampoFechaNacimientoVacio As String = "Ingrese la fecha de nacimiento."
        Public Const ErrorCampoGenero As String = "Seleccione el g�nero."
        Public Const ErrorCampoTipoPasajero As String = "Seleccione el tipo de pasajero."
        Public Const ErrorCampoNumeroDocumentoPasajeroVacio As String = "Ingrese el n�mero de documento."


        Public Const ErrorCampoFormaPago As String = "Seleccional la forma de pago."
        Public Const ErrorCampoTipoTarjeta As String = "Seleccional el tipo de tarjeta con la cual se realizar� la emisi�n."
        Public Const ErrorCampoNumeroTarjeta As String = "El campo n�mero de tarjeta contiene un caracter no num�rico."
        Public Const ErrorCampoNumeroTarjetaLength As String = "El campo n�mero de tarjeta no contiene la cantidad de caracteres requeridos."
        Public Const ErrorCampoNumeroTarjetaVacio As String = "Ingresar el n�mero de la tarjeta."
        Public Const ErrorCampoCodigoVerificacion As String = "El campo c�digo de verificaci�n contiene un caracter no num�rico."
        Public Const ErrorCampoCodigoVerificacionLength As String = "El campo c�digo de verificaci�n no contiene la cantidad de caracteres requeridos."
        Public Const ErrorCampoCodigoVerificacionVacio As String = "Ingresar el c�digo de verificaci�n de la tarjera."
        Public Const ErrorCampoNombreBancoVacio As String = "Ingresar el nombre del banco que expide la tarjeta."
        Public Const ErrorCampoNombrePais As String = "Seleccionar el nombre del pa�s de la tarjeta."
        Public Const ErrorCampoFechaVencimientoCaracterNoPermitido As String = "El campofecha de vencimiento de la tarjeta tiene un caracter no permitido."
        Public Const ErrorCampoFechaVencimientoIncorrecto As String = "La fecha de vencimiento de la tarjeta no es correcta."
        Public Const ErrorCampoFechaVencimientoVacio As String = "Ingresar la fecha de vencimiento de la tarjeta."
        Public Const ErrorCampoNombreTitularTarjetaVacio As String = "Ingresar nombre del titular de la tarjeta de la tarjeta."
        Public Const ErrorCampoTipoDocumentoIdentidad As String = "Seleccionar el tipo de documento de identidad."
        Public Const ErrorCampoNumeroDocumentoIdentidad As String = "Ingresar el n�mero de documento de identidad."
        Public Const ErrorCampoMontoCash As String = "Ingresar el monto a pagar al cash."
        Public Const ErrorCampoTarifaManualCash As String = "Ingresar el monto de la reserva."


        Public Const ErrorCampoTipoOperacionVacio As String = "Seleccione el tipo de operaci�n realizada."
        Public Const ErrorCampoBancoVacio As String = "Seleccione el banco donde realiz� el deposito."
        Public Const ErrorCampoSucursalVacio As String = "Ingrese el nombre de la sucursal del banco donde realiz� el deposito."
        Public Const ErrorCampoOperacionVacio As String = "Ingrese el n�mero de operaci�n del dep�sito."
        Public Const ErrorCampoReferenciaVacio As String = "Ingrese la referencia del dep�sito."
        Public Const ErrorCampoImporteValido As String = "Ingrese un importe de deposito v�lido."
        Public Const ErrorCampoImporteVacio As String = "Ingrese el importe depositado."
        Public Const ErrorCampoFechaDepositoVacio As String = "Ingrese la fecha en la que realiz� el depositado."
        Public Const ErrorCampoFechaDepositoValido As String = "Ingrese un fecha v�lida para el depositado."
        Public Const ErrorCampoFechaDepositoMayor As String = "La fecha de deposito deber ser menor o igual a la fecha actual."
        Public Const ErrorCampoHoraDepositoVacio As String = "Ingresar la hora del deposito."
        Public Const ErrorCampoHoraDepositoValido As String = "Ingresar una hora v�lida para el dep�sito."
        Public Const ErrorCampoHoraDepositoMayor As String = "La hora de deposito deber ser menor o igual a la hora actual."


#End Region

    End Class
End Namespace