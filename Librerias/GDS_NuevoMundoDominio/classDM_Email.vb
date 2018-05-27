Imports GDS_NM_Mensajeria
Imports GDS_NuevoMundoPersistencia
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function EnviarCorreo(ByVal strPara As String, _
                                     ByVal strCC As String, _
                                     ByVal strBCC As String, _
                                     ByVal strBody As String, _
                                     ByVal strSubject As String, _
                                     ByVal bolHTML As Boolean, _
                                     ByVal strNombreCorreo As String, _
                                     ByVal strCodigoSeguimiento As String, _
                                     ByVal intConexion As Integer) As Boolean

            Dim objCorreo As New classCorreo
            Dim objEnviarEmail As New EnviarEmail
            Dim bolRespuesta As Boolean = False

            Try

                If Not String.IsNullOrEmpty(strNombreCorreo) And _
                   Not String.IsNullOrEmpty(strPara) And _
                   Not String.IsNullOrEmpty(strBody) And _
                   Not String.IsNullOrEmpty(strSubject) Then

                    objCorreo.NombreCorreo = strNombreCorreo
                    objCorreo.ToCorreo = strPara
                    objCorreo.CCCorreo = strCC
                    objCorreo.BCCCorreo = strBCC
                    objCorreo.BodyCorreo = strBody
                    objCorreo.SubjectCorreo = strSubject

                    objEnviarEmail.Send(objCorreo, _
                                        bolHTML, _
                                        strCodigoSeguimiento, _
                                        intConexion)

                    bolRespuesta = True
                Else
                    bolRespuesta = False
                    Err.Raise(-1111, "classDM_Email.EnviarCorreo", "Alguno de los valores OBLIGATORIOS llegaron vacios")
                End If


            Catch ex As Exception
                bolRespuesta = False
                Throw New Exception(ex.ToString)
            Finally
                strPara = Nothing
                strCC = Nothing
                strBCC = Nothing
                strBody = Nothing
                strSubject = Nothing
                bolHTML = Nothing
                strNombreCorreo = Nothing
                strCodigoSeguimiento = Nothing
                intConexion = Nothing

                objCorreo = Nothing
                objEnviarEmail = Nothing
            End Try

            Return bolRespuesta

        End Function
        Public Function SendAttachment(ByVal objCorreo As classCorreo, _
                                       ByVal lstAdjuntos As List(Of String), _
                                       ByVal bolHTML As Boolean, _
                                       ByVal strCodigoSeguimiento As String, _
                                       ByVal intConexion As Integer) As Boolean

            Dim objEnviarEmail As New EnviarEmail
            Dim bolRespuesta As Boolean = False

            Try

                If Not String.IsNullOrEmpty(objCorreo.FromCorreo) And _
                   Not String.IsNullOrEmpty(objCorreo.ToCorreo) And _
                   Not String.IsNullOrEmpty(objCorreo.SubjectCorreo) And _
                   Not String.IsNullOrEmpty(objCorreo.BodyCorreo) Then


                    objEnviarEmail.SendAttachment(objCorreo, _
                                                  bolHTML, _
                                                  strCodigoSeguimiento, _
                                                  intConexion, _
                                                  lstAdjuntos)

                    bolRespuesta = True
                Else
                    bolRespuesta = False
                    Err.Raise(-1111, "classDM_Email.EnviarCorreo", "Alguno de los valores OBLIGATORIOS llegaron vacios")
                End If


            Catch ex As Exception
                bolRespuesta = False
                Throw New Exception(ex.ToString)
            Finally
                objCorreo = Nothing
                lstAdjuntos = Nothing
                bolHTML = Nothing
                strCodigoSeguimiento = Nothing
                intConexion = Nothing
                objCorreo = Nothing
                objEnviarEmail = Nothing
            End Try

            Return bolRespuesta

        End Function
    End Class
End Namespace