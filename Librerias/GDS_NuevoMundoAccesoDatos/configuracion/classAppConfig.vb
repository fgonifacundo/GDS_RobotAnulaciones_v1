Public Class classAppConfig
    Dim _classCadenaBD As New classCadenaBD
    Public Function Obtiene_CADENA_CONEXION_PTA_AMADEUS() As String
        Return _classCadenaBD.CadenaPTA_Amadeus
    End Function
    Public Function Obtiene_CADENA_CONEXION_PTA_SABRE() As String
        Return _classCadenaBD.CadenaPTA_Sabre
    End Function
    Public Function Obtiene_CADENA_CONEXION_PTA_EASYONLINE() As String
        Return _classCadenaBD.CadenaPTA_EasyOnLine
    End Function
    Public Function Obtiene_CADENA_CONEXION_WEB() As String
        Return _classCadenaBD.CadenaAppWebs
    End Function
    Public Function Obtiene_CADENA_CONEXION_WEB_DEMO() As String
        Return _classCadenaBD.CadenaDemoAppWebs
    End Function
    Public Function Obtiene_CADENA_CONEXION_PTA_DEMONUEVOMUNDO() As String
        Return _classCadenaBD.CadenaPTA_DemoNuevoMundo
    End Function
    Public Function Obtiene_CADENA_CONEXION_PTA_DESTINOS() As String
        Return _classCadenaBD.CadenaPTA_Destinos
    End Function
End Class
