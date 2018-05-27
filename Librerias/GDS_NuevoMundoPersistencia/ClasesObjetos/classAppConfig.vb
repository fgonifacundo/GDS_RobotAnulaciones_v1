Imports System.Configuration
Public Class classAppConfig
#Region "Log"
    Public Function Obtiene_RUTA_FILE_CONFIGURACION() As String
        Return ConfigurationManager.AppSettings("RUTA_FILE_CONFIGURACION")
    End Function
    Public Function Obtiene_RUTA_FILE_LOG_AMADEUS() As String
        Return ConfigurationManager.AppSettings("RUTA_FILE_LOG_AMADEUS")
    End Function
    Public Function Obtiene_RUTA_FILE_LOG_SABRE() As String
        Return ConfigurationManager.AppSettings("RUTA_FILE_LOG_SABRE")
    End Function
    Public Function Obtiene_RUTA_FILE_LOG_EASYONLINE() As String
        Return ConfigurationManager.AppSettings("RUTA_FILE_LOG_EASYONLINE")
    End Function
    Public Function Obtiene_RUTA_FILE_LOG_GNM_GENERADOROADP() As String
        Return ConfigurationManager.AppSettings("RUTA_FILE_LOG_GNM_GENERADOROADP")
    End Function


#End Region
#Region "Payload"
    Public Function Obtiene_RUTA_FILE_PAYLOAD_SABRE() As String
        Return ConfigurationManager.AppSettings("RUTA_FILE_LOG_SABRE")
    End Function
    Public Function Obtiene_RUTA_FILE_ROBOTANULACION_SABRE() As String
        Return ConfigurationManager.AppSettings("RUTA_FILE_LOG_ROBOTANULA")
    End Function
#End Region
End Class
