Public Class cuerpoCorreo
    Public Function GeneraCuerpo(ByVal strCuperoMensaje As String) As String
        Dim CorreoEmisionWeb As New System.Text.StringBuilder
        Try
            CorreoEmisionWeb.Append("<HTML>" & vbCrLf)
            CorreoEmisionWeb.Append("<HEAD>" & vbCrLf)
            CorreoEmisionWeb.Append(GeneraHEAD())
            CorreoEmisionWeb.Append("</HEAD>" & vbCrLf)
            CorreoEmisionWeb.Append("<BODY>" & vbCrLf)
            CorreoEmisionWeb.Append(strCuperoMensaje)
            CorreoEmisionWeb.Append("</BODY>" & vbCrLf)
            CorreoEmisionWeb.Append("</HTML>" & vbCrLf)

            Return CorreoEmisionWeb.ToString

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function
    Private Function GeneraHEAD() As String

        Dim Head As New System.Text.StringBuilder
        Dim Estilos As New Estilos
        Try

            Head.Append("<META http-equiv=Content-Type content='text/html; charset=iso-8859-1'>" & vbCrLf)
            Head.Append("<STYLE type=text/css>" & vbCrLf)
            Head.Append("BODY {BACKGROUND-COLOR: #e9e9e9}" & vbCrLf)
            Head.Append(Estilos.tarifario_header & vbCrLf)
            Head.Append(Estilos.tarifario_fila_a & vbCrLf)
            Head.Append(Estilos.tarifario_fila_b & vbCrLf)
            Head.Append(Estilos.tarifario_fila_c & vbCrLf)
            Head.Append(Estilos.textContenido & vbCrLf)
            Head.Append(Estilos.textContenidoROBOT & vbCrLf)
            Head.Append("</STYLE>" & vbCrLf)
            Head.Append("<META content='MSHTML 6.00.2900.5897' name=GENERATOR>" & vbCrLf)

            Return Head.ToString
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function
    Public Function GeneraCuerpoRobot(ByVal strCuperoMensaje As String) As String
        Dim CorreoEmisionWeb As New System.Text.StringBuilder
        Try
            CorreoEmisionWeb.Append("<HTML>" & vbCrLf)
            CorreoEmisionWeb.Append("<HEAD>" & vbCrLf)
            CorreoEmisionWeb.Append(GeneraHEADRobot())
            CorreoEmisionWeb.Append("</HEAD>" & vbCrLf)
            CorreoEmisionWeb.Append("<BODY>" & vbCrLf)
            CorreoEmisionWeb.Append(strCuperoMensaje)
            CorreoEmisionWeb.Append("</BODY>" & vbCrLf)
            CorreoEmisionWeb.Append("</HTML>" & vbCrLf)

            Return CorreoEmisionWeb.ToString

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function
    Private Function GeneraHEADRobot() As String

        Dim Head As New System.Text.StringBuilder
        Dim Estilos As New Estilos
        Try

            Head.Append("<META http-equiv=Content-Type content='text/html; charset=iso-8859-1'>" & vbCrLf)
            Head.Append("<STYLE type=text/css>" & vbCrLf)
            Head.Append("BODY {BACKGROUND-COLOR: #ffffff}" & vbCrLf)
            Head.Append(Estilos.tarifario_header & vbCrLf)
            Head.Append(Estilos.tarifario_fila_a & vbCrLf)
            Head.Append(Estilos.tarifario_fila_b & vbCrLf)
            Head.Append(Estilos.tarifario_fila_c & vbCrLf)
            Head.Append(Estilos.textContenido & vbCrLf)
            Head.Append(Estilos.textContenidoROBOT & vbCrLf)
            Head.Append(Estilos.textContenidoNegrita & vbCrLf)
            Head.Append("</STYLE>" & vbCrLf)
            Head.Append("<META content='MSHTML 6.00.2900.5897' name=GENERATOR>" & vbCrLf)

            Return Head.ToString
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function
    '#ffffcc
End Class
