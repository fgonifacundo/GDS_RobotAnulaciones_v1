Imports system.Net.Mail
Imports GDS_NuevoMundoPersistencia
Public Class EasyMail
    Private oMail As New System.Net.Mail.MailMessage
    Private oSmtp As New System.Net.Mail.SmtpClient
    Private objCorreo As classCorreo
    Private mvarMailFrom As String
    Private mvarMailSubject As String
    Private mvarMailCc As String
    Private mvarMailBcc As String
    Private mvarMailServer As String
    Private mvarMailBody As String
    Private mvarMailHTML As String
    Private mailSent As Boolean = False

    Public WriteOnly Property MailFrom() As MailAddress
        Set(ByVal value As MailAddress)
            oMail.From = value
        End Set
    End Property
    Public WriteOnly Property MailTo() As MailAddress
        Set(ByVal value As MailAddress)
            oMail.To.Add(value)
        End Set
    End Property
    Public WriteOnly Property MailSubject() As String
        Set(ByVal value As String)
            oMail.Subject = value
        End Set
    End Property
    Public WriteOnly Property MailCc() As MailAddress
        Set(ByVal value As MailAddress)
            oMail.CC.Add(value)
        End Set
    End Property
    Public WriteOnly Property MailBcc() As MailAddress
        Set(ByVal value As MailAddress)
            oMail.Bcc.Add(value)
        End Set
    End Property
    Public WriteOnly Property MailServer() As String
        Set(ByVal value As String)
            oSmtp.Host = value
        End Set
    End Property
    Public WriteOnly Property MailBody() As String
        Set(ByVal value As String)
            oMail.Body = value
        End Set
    End Property
    Public WriteOnly Property MailHTML() As Boolean
        Set(ByVal value As Boolean)
            oMail.IsBodyHtml = value
        End Set
    End Property

    Public Sub AddAttachment_Stream(ByVal pObjStream As IO.Stream, _
                                    ByVal pStrNomArchivo As String)
        Dim objAttachment As New Attachment(pObjStream, pStrNomArchivo)
        oMail.Attachments.Add(objAttachment)
    End Sub

    Public Sub AddAttachment(ByVal pStrRuta As String, _
                             ByVal pStrNomArchivo As String)
        Dim objAttachment As New Attachment(pStrRuta)
        If Not String.IsNullOrEmpty(pStrNomArchivo) Then
            pStrNomArchivo = pStrNomArchivo.Replace("/", "_")
            objAttachment.Name = pStrNomArchivo
        End If
        oMail.Attachments.Add(objAttachment)
    End Sub

#Region "Otros métodos"
    Public WriteOnly Property AddMailFrom(ByVal pStrDisplay As String) As String
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(Trim(value)) Then
                oMail.From = New MailAddress(Trim(value), Trim(pStrDisplay))
            End If
        End Set
    End Property

    Public WriteOnly Property AddMailsCC() As String
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(Trim(value)) Then
                For intX As Integer = 0 To value.Split(";").Length - 1
                    If Not String.IsNullOrEmpty(Trim(value.Split(";")(intX))) Then
                        oMail.CC.Add(value.Split(";")(intX))
                    End If
                Next
            End If
        End Set
    End Property

    Public WriteOnly Property AddMailsBCC() As String
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(Trim(value)) Then
                For intX As Integer = 0 To value.Split(";").Length - 1
                    If Not String.IsNullOrEmpty(Trim(value.Split(";")(intX))) Then
                        oMail.Bcc.Add(value.Split(";")(intX))
                    End If
                Next
            End If
        End Set
    End Property

    Public WriteOnly Property AddMailsTo() As String
        Set(ByVal value As String)
            If Not String.IsNullOrEmpty(Trim(value)) Then
                For intX As Integer = 0 To value.Split(";").Length - 1
                    If Not String.IsNullOrEmpty(Trim(value.Split(";")(intX))) Then
                        oMail.To.Add(value.Split(";")(intX))
                    End If
                Next
            End If
        End Set
    End Property
#End Region


#Region "SEND WEB"
    Public Sub SendMail()
        oSmtp.Credentials = New System.Net.NetworkCredential("webmaster@gruponuevomundo.com.pe", "w3bm@st3r")
        oSmtp.Send(oMail)
    End Sub

    Public Sub SendMail(ByVal Token As String, _
    Optional ByVal correo As classCorreo = Nothing)
        Try
            AddHandler oSmtp.SendCompleted, AddressOf SendCompletedCallback
            objCorreo = correo
            oSmtp.SendAsync(oMail, Token)
        Catch ex As Exception
            Err.Raise(-9999, Err.Source.ToString, Err.Description.ToString)
        End Try
    End Sub
#End Region

#Region "SEND WEB"
    Public Sub SendMailTurbo()
        oSmtp.Credentials = New System.Net.NetworkCredential("TurboSabre@gruponuevomundo.com.pe", "S5br3q1k")
        oSmtp.Send(oMail)
    End Sub
    Public Sub SendMailTurbo(ByVal Token As String, _
    Optional ByVal correo As classCorreo = Nothing)
        Try
            AddHandler oSmtp.SendCompleted, AddressOf SendCompletedCallback
            objCorreo = correo
            oSmtp.SendAsync(oMail, Token)
        Catch ex As Exception
            Err.Raise(-9999, Err.Source.ToString, Err.Description.ToString)
        End Try
    End Sub
#End Region
    Public Sub SendMail_Validation(ByVal usuario As String, ByVal contrasenia As String)
        oSmtp.Credentials = New System.Net.NetworkCredential(usuario, contrasenia)
        oSmtp.Send(oMail)
    End Sub

    Public Sub SendCompletedCallback(ByVal sender As System.Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)
        Dim token As String = CStr(e.UserState.ToString)

        If e.Error IsNot Nothing Then
            Serializer(token, e)
        End If

        objCorreo = Nothing
        oSmtp = Nothing
        oMail = Nothing
        mailSent = True

    End Sub

    Private Sub Serializer(ByVal Token As String, ByVal e As Object)

        Dim oStreamWriter As System.IO.StreamWriter
        Dim oSerializer As System.Xml.Serialization.XmlSerializer
        'Dim file_date, file_name, file_err_data As String
        Dim fp As System.IO.StreamWriter

        Try
            'With Date.Now
            '    file_date = .Day & "-" & .Month & "-" & .Year & "-" & .Hour & "-" & .Minute & "-" & .Second & "-" & .Millisecond
            'End With
            'file_name = Token & "_" & objCorreo.IdWeb & "_" & objCorreo.IdLang & "_" & file_date
            'file_err_data = "Err_" & file_name

            'oSerializer = New System.Xml.Serialization.XmlSerializer(GetType(classCorreo))
            'oStreamWriter = New System.IO.StreamWriter(NuevoMundoUtility.ConstantesUtility.ERR_FOLDER_PATH & file_name & ".xml")
            'oSerializer.Serialize(oStreamWriter, objCorreo)

            'fp = System.IO.File.CreateText(NuevoMundoUtility.ConstantesUtility.ERR_FOLDER_PATH & file_err_data & ".txt")
            'fp.WriteLine(e.Error.InnerException)
            'fp.Close()

            'oStreamWriter.Close()
        Catch ex As Exception

        Finally
            oSerializer = Nothing
            oStreamWriter = Nothing
            fp = Nothing
        End Try
    End Sub

    Public Sub Dispose()
        oMail.Dispose()
    End Sub
End Class
