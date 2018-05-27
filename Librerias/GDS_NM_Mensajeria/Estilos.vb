Imports System.Configuration

Public Class Estilos
    Private Const color0 As String = "#000000;"
    Private Const color1 As String = "#FFFFFF;"
    Private Const color2 As String = "#ff5a00;"
    Private Const color3 As String = "#F2F2F2;"
    Private Const solid1 As String = "#e3dbca;"
    Private Const color4 As String = "#00007f;"
    Public Function tarifario_header() As String

        Dim strCadenaEstilo As New System.Text.StringBuilder
        Dim colorGeneric = ConfigurationSettings.AppSettings("BACKGROUND_HEADER_TABLE_MAIL")

        strCadenaEstilo.Append(".tarifario_header" & vbCrLf)
        strCadenaEstilo.Append("{" & vbCrLf)
        strCadenaEstilo.Append("background:" & colorGeneric & vbCrLf)
        strCadenaEstilo.Append("color:" & color1 & vbCrLf)
        strCadenaEstilo.Append("line-height:14px;" & vbCrLf)
        strCadenaEstilo.Append("font-size:11px;" & vbCrLf)
        strCadenaEstilo.Append("font-weight:bold;" & vbCrLf)
        strCadenaEstilo.Append("font-family:Tahoma, Verdana, Arial;" & vbCrLf)
        strCadenaEstilo.Append("}" & vbCrLf)

        Return strCadenaEstilo.ToString
    End Function
    Public Function tarifario_fila_a() As String
        Dim strCadenaEstilo As New System.Text.StringBuilder

        strCadenaEstilo.Append(".tarifario_fila_a" & vbCrLf)
        strCadenaEstilo.Append("{" & vbCrLf)
        strCadenaEstilo.Append("padding:3px;" & vbCrLf)
        strCadenaEstilo.Append("border-top:1px solid " & solid1 & vbCrLf)
        strCadenaEstilo.Append("border-left:1px solid " & solid1 & vbCrLf)
        strCadenaEstilo.Append("border-bottom:1px solid " & solid1 & vbCrLf)
        strCadenaEstilo.Append("border-right:1px solid " & solid1 & vbCrLf)
        strCadenaEstilo.Append("font-family:Tahoma, Verdana, Arial;" & vbCrLf)
        strCadenaEstilo.Append("font-size: 11px;" & vbCrLf)
        strCadenaEstilo.Append("color:" & color0 & vbCrLf)
        strCadenaEstilo.Append("background-color:" & color3 & vbCrLf)
        strCadenaEstilo.Append("}" & vbCrLf)
        Return strCadenaEstilo.ToString
    End Function
    Public Function tarifario_fila_b() As String
        Dim strCadenaEstilo As New System.Text.StringBuilder

        strCadenaEstilo.Append(".tarifario_fila_b" & vbCrLf)
        strCadenaEstilo.Append("{" & vbCrLf)
        strCadenaEstilo.Append("padding:3px;" & vbCrLf)
        strCadenaEstilo.Append("border-top:1px solid " & solid1 & vbCrLf)
        strCadenaEstilo.Append("border-left:1px solid " & solid1 & vbCrLf)
        strCadenaEstilo.Append("border-bottom:1px solid " & solid1 & vbCrLf)
        strCadenaEstilo.Append("border-right:1px solid " & solid1 & vbCrLf)
        strCadenaEstilo.Append("font-family:Tahoma, Verdana, Arial;" & vbCrLf)
        strCadenaEstilo.Append("font-size: 11px;" & vbCrLf)
        strCadenaEstilo.Append("color:" & color0 & vbCrLf)
        strCadenaEstilo.Append("background-color:" & color1 & vbCrLf)
        strCadenaEstilo.Append("}" & vbCrLf)

        Return strCadenaEstilo.ToString
    End Function
    Public Function tarifario_fila_c() As String
        Dim strCadenaEstilo As New System.Text.StringBuilder

        strCadenaEstilo.Append(".tarifario_fila_c" & vbCrLf)
        strCadenaEstilo.Append("{" & vbCrLf)
        strCadenaEstilo.Append("padding:3px;" & vbCrLf)
        strCadenaEstilo.Append("border-top:1px solid " & solid1 & vbCrLf)
        strCadenaEstilo.Append("border-left:1px solid " & solid1 & vbCrLf)
        strCadenaEstilo.Append("border-bottom:1px solid " & solid1 & vbCrLf)
        strCadenaEstilo.Append("border-right:1px solid " & solid1 & vbCrLf)
        strCadenaEstilo.Append("font-family:Tahoma, Verdana, Arial;" & vbCrLf)
        strCadenaEstilo.Append("font-size: 12px;" & vbCrLf)
        strCadenaEstilo.Append("color:" & color0 & vbCrLf)
        strCadenaEstilo.Append("background-color:" & color1 & vbCrLf)
        strCadenaEstilo.Append("}" & vbCrLf)

        Return strCadenaEstilo.ToString
    End Function
    Public Function textContenido() As String
        Dim strCadenaEstilo As New System.Text.StringBuilder

        strCadenaEstilo.Append(".textContenido" & vbCrLf)
        strCadenaEstilo.Append("{" & vbCrLf)
        strCadenaEstilo.Append("font-family:Tahoma, Verdana, Arial;" & vbCrLf)
        strCadenaEstilo.Append("font-size: 12px;" & vbCrLf)
        strCadenaEstilo.Append("}" & vbCrLf)

        Return strCadenaEstilo.ToString
    End Function
    Public Function textContenidoROBOT() As String
        Dim strCadenaEstilo As New System.Text.StringBuilder

        strCadenaEstilo.Append(".textContenidoROBOT" & vbCrLf)
        strCadenaEstilo.Append("{" & vbCrLf)
        strCadenaEstilo.Append("font-family:Tahoma, Verdana, Arial;" & vbCrLf)
        strCadenaEstilo.Append("font-size: 12px;" & vbCrLf)
        strCadenaEstilo.Append("color: #000099;" & vbCrLf)
        strCadenaEstilo.Append("font-weight: bold;" & vbCrLf)
        strCadenaEstilo.Append("}" & vbCrLf)

        Return strCadenaEstilo.ToString
    End Function
    Public Function textContenidoNegrita() As String
        Dim strCadenaEstilo As New System.Text.StringBuilder

        strCadenaEstilo.Append(".textContenidoNegrita" & vbCrLf)
        strCadenaEstilo.Append("{" & vbCrLf)
        strCadenaEstilo.Append("font-family:Tahoma, Verdana, Arial;" & vbCrLf)
        strCadenaEstilo.Append("font-size: 11px;" & vbCrLf)
        strCadenaEstilo.Append("color: #000000;" & vbCrLf)
        strCadenaEstilo.Append("font-weight: bold;" & vbCrLf)
        strCadenaEstilo.Append("}" & vbCrLf)

        Return strCadenaEstilo.ToString
    End Function

End Class
