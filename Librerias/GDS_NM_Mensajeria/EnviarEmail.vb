Imports GDS_NuevoMundoPersistencia
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Public Class EnviarEmail
    Private strLog As String = Nothing
    Private objEscribeLog As New GDS_MuevoMundoLog.EscribeLog
    Private Const TabEspacios As String = vbTab & "  "
    Public Function Send(ByVal objCorreo As classCorreo, _
                    ByVal bolFormatoHTML As Boolean, _
                    ByVal strCodigoSeguimiento As String, _
                    ByVal intFirmaDB As Integer, _
                    Optional ByVal objAdjunto As IO.Stream = Nothing) As Integer

        Dim objMail As New EasyMail
        Dim intRspta As Integer = 0

        Try

            objMail.MailServer = Constantes.IPCorreos.ToString

            Select Case intFirmaDB
                Case 1
                    objMail.MailFrom = New System.Net.Mail.MailAddress(Constantes.emailTurboSabre.ToString, _
                                                                       objCorreo.NombreCorreo, _
                                                                       System.Text.Encoding.UTF8)

                Case 2

                    objMail.MailFrom = New System.Net.Mail.MailAddress(Constantes.emailEasyOnline.ToString, _
                                                   objCorreo.NombreCorreo, _
                                                   System.Text.Encoding.UTF8)
                Case 3

                    objMail.MailFrom = New System.Net.Mail.MailAddress(Constantes.emailEasyReporte.ToString, _
                                                                       objCorreo.NombreCorreo, _
                                                                       System.Text.Encoding.UTF8)

                Case 5
                    'RobotOADP
                    objMail.MailFrom = New System.Net.Mail.MailAddress(Constantes.emailRobotOADP.ToString, _
                                                                       objCorreo.NombreCorreo, _
                                                                       System.Text.Encoding.UTF8)

                Case 6
                    'RobotOADP
                    objMail.MailFrom = New System.Net.Mail.MailAddress(Constantes.emailRobotOADP.ToString, _
                                                                       objCorreo.NombreCorreo, _
                                                                       System.Text.Encoding.UTF8)
                Case 7
                    'ProcesoSabre
                    objMail.MailFrom = New System.Net.Mail.MailAddress(Constantes.emailRemitenteRobot.ToString, _
                                                                       objCorreo.NombreCorreo, _
                                                                       System.Text.Encoding.UTF8)
                Case 8
                    'ProcesoSabre
                    objMail.MailFrom = New System.Net.Mail.MailAddress(objCorreo.FromCorreo, _
                                                                       objCorreo.NombreCorreo, _
                                                                       System.Text.Encoding.UTF8)
            End Select

            If objCorreo.ToCorreo.Split(Constantes.PuntoComa).Length = 1 Then
                objMail.MailTo = New System.Net.Mail.MailAddress(objCorreo.ToCorreo)
            Else
                objMail.AddMailsTo = objCorreo.ToCorreo
            End If

            If Not String.IsNullOrEmpty(objCorreo.CCCorreo) Then
                objMail.AddMailsCC = objCorreo.CCCorreo
            End If

            If Not String.IsNullOrEmpty(objCorreo.BCCCorreo) Then
                objMail.AddMailsBCC = objCorreo.BCCCorreo
            End If

            objMail.MailSubject = objCorreo.SubjectCorreo
            objMail.MailHTML = bolFormatoHTML
            objMail.MailBody = objCorreo.BodyCorreo

            If objAdjunto IsNot Nothing Then
                objMail.AddAttachment_Stream(objAdjunto, "archivo.rar")
                objMail.SendMail()
                intRspta = 1
            Else
                objMail.SendMail(Constantes.TokenCorreos, objCorreo)
                intRspta = 1

                'strLog = "Envio de Correo : " & objCorreo.SubjectCorreo.ToString & vbCrLf
                'strLog &= TabEspacios & "Nombre Correo: " & objCorreo.NombreCorreo.ToString & vbCrLf
                'strLog &= TabEspacios & "Destinatario : " & objCorreo.ToCorreo.ToString & vbCrLf
                'objEscribeLog.WriteLogGeneral(objCorreo.BodyCorreo, Nothing, strCodigoSeguimiento, iCnx)
            End If

        Catch ex As Exception
            intRspta = 0
            strLog = "Envio de Correo : " & objCorreo.SubjectCorreo.ToString & vbCrLf
            strLog &= TabEspacios & "Nombre Correo: " & objCorreo.NombreCorreo.ToString & vbCrLf
            strLog &= TabEspacios & "Destinatario : " & objCorreo.ToCorreo.ToString & vbCrLf
            strLog &= TabEspacios & "Error : " & ex.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, "EnviarEmail_Send", strCodigoSeguimiento)
        Finally
            objCorreo = Nothing
            objMail = Nothing
        End Try

        Return intRspta
    End Function
    Public Sub SendAttachment(ByVal objCorreo As classCorreo, _
                              ByVal bolFormatoHTML As Boolean, _
                              ByVal strCodigoSeguimiento As String, _
                              ByVal iCnx As Integer, _
                              ByVal lstArchivosAdjuntos As List(Of String))

        Dim objMail As New EasyMail

        Try

            objMail.MailServer = Constantes.IPCorreos.ToString

            Select Case iCnx
                Case 1
                    objMail.MailFrom = New System.Net.Mail.MailAddress(Constantes.emailTurboSabre.ToString, _
                                                                       objCorreo.NombreCorreo, _
                                                                       System.Text.Encoding.UTF8)

                Case 2

                    objMail.MailFrom = New System.Net.Mail.MailAddress(Constantes.emailEasyOnline.ToString, _
                                                   objCorreo.NombreCorreo, _
                                                   System.Text.Encoding.UTF8)
                Case 3

                    objMail.MailFrom = New System.Net.Mail.MailAddress(Constantes.emailEasyReporte.ToString, _
                                                                       objCorreo.NombreCorreo, _
                                                                       System.Text.Encoding.UTF8)

                Case Else

                    objMail.MailFrom = New System.Net.Mail.MailAddress(objCorreo.FromCorreo, _
                                                                       objCorreo.NombreCorreo, _
                                                                       System.Text.Encoding.UTF8)

            End Select

            If objCorreo.ToCorreo.Split(Constantes.PuntoComa).Length = 1 Then
                objMail.MailTo = New System.Net.Mail.MailAddress(objCorreo.ToCorreo)
            Else
                objMail.AddMailsTo = objCorreo.ToCorreo
            End If

            If Not String.IsNullOrEmpty(objCorreo.CCCorreo) Then
                objMail.AddMailsCC = objCorreo.CCCorreo
            End If

            If Not String.IsNullOrEmpty(objCorreo.BCCCorreo) Then
                objMail.AddMailsBCC = objCorreo.BCCCorreo
            End If

            objMail.MailSubject = objCorreo.SubjectCorreo
            objMail.MailHTML = bolFormatoHTML
            objMail.MailBody = objCorreo.BodyCorreo

            If lstArchivosAdjuntos IsNot Nothing Then

                For i As Integer = 0 To lstArchivosAdjuntos.Count - 1
                    If lstArchivosAdjuntos.Item(i).ToString.Split("#").Length = 1 Then
                        objMail.AddAttachment(lstArchivosAdjuntos.Item(i).ToString.Split("#")(0), Nothing)
                    Else
                        objMail.AddAttachment(lstArchivosAdjuntos.Item(i).ToString.Split("#")(0), lstArchivosAdjuntos.Item(i).ToString.Split("#")(1))
                    End If

                    Dim intTamano As String = lstArchivosAdjuntos.Item(i).ToString.Split("\").Length - 1

                    objEscribeLog.WriteLogGeneral("Se adjunto el archivo: " & lstArchivosAdjuntos.Item(i).ToString.Split("\")(intTamano), _
                                                   Constantes.GNM_GenerardorOADP, _
                                                   strCodigoSeguimiento)

                    intTamano = Nothing
                Next

                objMail.SendMail()
            Else
                objMail.SendMail(Constantes.TokenCorreos, objCorreo)

                strLog = "Envio de Correo : " & objCorreo.SubjectCorreo.ToString & vbCrLf
                strLog &= TabEspacios & "Nombre Correo: " & objCorreo.NombreCorreo.ToString & vbCrLf
                strLog &= TabEspacios & "Destinatario : " & objCorreo.ToCorreo.ToString & vbCrLf
                objEscribeLog.WriteLog(objCorreo.BodyCorreo, strCodigoSeguimiento, iCnx)

            End If




        Catch ex As Exception
            strLog = "Envio de Correo : " & objCorreo.SubjectCorreo.ToString & vbCrLf
            strLog &= TabEspacios & "Nombre Correo: " & objCorreo.NombreCorreo.ToString & vbCrLf
            strLog &= TabEspacios & "Destinatario : " & objCorreo.ToCorreo.ToString & vbCrLf
            objEscribeLog.WriteLog(strLog, strCodigoSeguimiento, iCnx)
        Finally
            objCorreo = Nothing
            objMail.Dispose()
            objMail = Nothing
        End Try
    End Sub
End Class
