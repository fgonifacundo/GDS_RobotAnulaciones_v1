Imports GDS_NuevoMundoPersistencia
Imports GDS_NM_Mensajeria
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports EscribeLog = GDS_MuevoMundoLog.EscribeLog
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function EnviarBoletoPorCorreo1(ByVal objCliente As classCliente, _
                                               ByVal objDatosAgente As classDatosAgente, _
                                               ByVal strRutaArchivo As String, _
                                               ByVal strNombreArchivoHTML As String, _
                                               ByVal strNombrePasajero As String, _
                                               ByVal strCodigoSeguimiento As String, _
                                               ByVal intGDS As Integer, _
                                               ByVal oOp_Firma As Integer) As String


            Dim EnviarEmail As EnviarEmail = Nothing
            Dim Correo As classCorreo = Nothing

            Dim objDAO As DAO = Nothing
            Dim strRespuesta As String = Nothing
            Dim lstArchivosAdjuntoas As List(Of String) = Nothing
            Dim strMensajeLog As String = Nothing

            Try

                Correo = New classCorreo
                Correo.FromCorreo = objDatosAgente.CorreoAgente
                If objCliente.DK = "4823" Then
                    Correo.ToCorreo = objDatosAgente.CorreoAgente & ";jzamudio@gruponuevomundo.com.pe"
                Else
                    Correo.ToCorreo = objDatosAgente.CorreoAgente
                End If

                Correo.BCCCorreo = "hsanchez@gruponuevomundo.com.pe"
                Correo.NombreCorreo = "NUEVO MUNDO VIAJES"

                If strRutaArchivo.Contains("\TKT\") Then
                    Correo.SubjectCorreo = "TKT pax " & strNombrePasajero.Replace("/", "_")
                Else
                    Correo.SubjectCorreo = "EMD pax " & strNombrePasajero.Replace("/", "_")
                End If



                lstArchivosAdjuntoas = New List(Of String)
                lstArchivosAdjuntoas.Add(strRutaArchivo & "\" & strNombreArchivoHTML & ".html" & "#" & strNombrePasajero & ".html")
                lstArchivosAdjuntoas.Add("C:\ServiciosNET\GNM_GenerardorOADP\ArchivosAdjuntosMail\Condiciones_de_Transporte_BSP.pdf")
                lstArchivosAdjuntoas.Add("C:\ServiciosNET\GNM_GenerardorOADP\ArchivosAdjuntosMail\tarjeta_de_migraciones.pdf")

                strMensajeLog = "el archivo " & strRutaArchivo & "\" & strNombreArchivoHTML & ".html" & " desde la cuenta : " & objDatosAgente.CorreoAgente & " al usuario " & objCliente.EmailPTA
                objEscribeLog.WriteLogGeneral("Se envió " & strMensajeLog, _
                                               Constantes.GNM_EnvioCorreoOADP, _
                                               strCodigoSeguimiento)

                EnviarEmail = New GDS_NM_Mensajeria.EnviarEmail
                EnviarEmail.SendAttachment(Correo, True, strCodigoSeguimiento, intGDS, lstArchivosAdjuntoas)
                strRespuesta = "Se envió " & strMensajeLog

                objEscribeLog.WriteLogGeneral("Se envió con exito", _
                                               Constantes.GNM_EnvioCorreoOADP, _
                                               strCodigoSeguimiento)

                'Dim auxFecha As Date = objDWLIST.FECHA
                'objDWLIST.FECHA = auxFecha.ToString(Constantes.IWS_DATE_FORMAT_FILE5)
                'objDAO.ActualizaMarcaGDS_DWLIST_OADP(objDWLIST, "ENVIA_CORREO", 0, strCodigoSeguimiento, intGDS)

            Catch ex As Exception
                strRespuesta = "No se envió " & strMensajeLog
                objEscribeLog.WriteLogGeneral("Se produjo error al enviar " & strMensajeLog & " Mensaje: " & ex.InnerException.ToString, _
                                               Constantes.GNM_EnvioCorreoOADP, _
                                               strCodigoSeguimiento)
                Err.Raise(-5, "WSSabre_EnviarCorreoAdjuntos", ex.InnerException.ToString)
            Finally
                EnviarEmail = Nothing
                Correo = Nothing

                objDAO = Nothing
                lstArchivosAdjuntoas = Nothing
                strMensajeLog = Nothing
                objCliente = Nothing
                objDatosAgente = Nothing
                strRutaArchivo = Nothing
                strNombreArchivoHTML = Nothing
                strNombrePasajero = Nothing
            End Try

            Return strRespuesta

        End Function
        Public Function EnviarBoletoPorCorreo(ByVal strFiltro As String, _
                                              ByVal strFecha As String, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As List(Of String)

            Dim EnviarEmail As EnviarEmail = Nothing
            Dim Correo As classCorreo = Nothing

            Dim objDAO As DAO = Nothing
            Dim strRespuesta As String = Nothing
            Dim lstArchivosAdjuntoas As List(Of String) = Nothing
            Dim strMensajeLog As String = Nothing
            Dim lstDWLIST As List(Of classDWLIST) = Nothing
            Dim lstAgente As List(Of classDatosAgente) = Nothing


            Dim objCliente As classCliente = Nothing
            Dim objDatosAgente As classDatosAgente = Nothing
            Dim strRutaArchivo As String = Nothing
            Dim strNombreArchivoHTML As String = Nothing
            Dim strNombrePasajero As String = Nothing

            Dim lstRespuesta As List(Of String) = Nothing

            Try

                objDAO = New DAO
                lstDWLIST = objDAO.ObtieneListadoDWLIST(strFiltro, strFecha, strCodigoSeguimiento, intFirmaDB, intEsquema)

                If lstDWLIST IsNot Nothing Then

                    For i As Integer = 0 To lstDWLIST.Count - 1

                        objCliente = New classCliente
                        objCliente = objDAO.ObtenerDatosCliente(lstDWLIST.Item(i).DK, Nothing, strCodigoSeguimiento, intFirmaDB, intEsquema)

                        objDatosAgente = New classDatosAgente
                        lstAgente = objDAO.ObtenerDatosAgente("'" & lstDWLIST.Item(i).FIRMA_AGENTE & "'", strCodigoSeguimiento, intFirmaDB, intEsquema)
                        objDatosAgente = lstAgente.Item(0)

                        strRutaArchivo = lstDWLIST.Item(i).RUTA_CARPETA
                        strNombreArchivoHTML = lstDWLIST.Item(i).NUM_TICKET

                        strNombrePasajero = lstDWLIST.Item(i).APELLIDO_PAX & "_" & lstDWLIST.Item(i).NOMBRE_PAX



                        Correo = New classCorreo
                        Correo.FromCorreo = objDatosAgente.CorreoAgente
                        If objCliente.DK = "4823" Then
                            Correo.ToCorreo = objDatosAgente.CorreoAgente & ";jzamudio@gruponuevomundo.com.pe"
                        Else
                            Correo.ToCorreo = objDatosAgente.CorreoAgente
                        End If

                        Correo.BCCCorreo = "hsanchez@gruponuevomundo.com.pe"
                        Correo.NombreCorreo = "NUEVO MUNDO VIAJES"

                        If strRutaArchivo.Contains("\TKT\") Then
                            Correo.SubjectCorreo = "TKT pax " & strNombrePasajero.Replace("/", "_")
                        Else
                            Correo.SubjectCorreo = "EMD pax " & strNombrePasajero.Replace("/", "_")
                        End If

                        lstArchivosAdjuntoas = New List(Of String)
                        lstArchivosAdjuntoas.Add(strRutaArchivo & "\" & strNombreArchivoHTML & ".html" & "#" & strNombrePasajero & ".html")
                        lstArchivosAdjuntoas.Add("C:\ServiciosNET\GNM_GenerardorOADP\ArchivosAdjuntosMail\Condiciones_de_Transporte_BSP.pdf")
                        lstArchivosAdjuntoas.Add("C:\ServiciosNET\GNM_GenerardorOADP\ArchivosAdjuntosMail\tarjeta_de_migraciones.pdf")

                        strMensajeLog = "el archivo " & strRutaArchivo & "\" & strNombreArchivoHTML & ".html" & " desde la cuenta : " & objDatosAgente.CorreoAgente & " al usuario " & objCliente.EmailPTA
                        objEscribeLog.WriteLogGeneral("Se envió " & strMensajeLog, _
                                                       Constantes.GNM_EnvioCorreoOADP, _
                                                       strCodigoSeguimiento)

                        EnviarEmail = New GDS_NM_Mensajeria.EnviarEmail
                        EnviarEmail.SendAttachment(Correo, True, strCodigoSeguimiento, intFirmaDB, lstArchivosAdjuntoas)
                        strRespuesta = "Se envió " & strMensajeLog

                        objEscribeLog.WriteLogGeneral("Se envió con exito", _
                                                       Constantes.GNM_EnvioCorreoOADP, _
                                                       strCodigoSeguimiento)

                        Dim auxFecha As Date = lstDWLIST.Item(i).FECHA
                        lstDWLIST.Item(i).FECHA = auxFecha.ToString(Constantes.IWS_DATE_FORMAT_FILE5)

                        objDAO.ActualizaMarcaGDS_DWLIST_OADP(lstDWLIST.Item(i), "ENVIA_CORREO", 0, strCodigoSeguimiento, intFirmaDB, intEsquema)

                        If lstRespuesta Is Nothing Then lstRespuesta = New List(Of String)
                        lstRespuesta.Add("N° Ttk: " & lstDWLIST.Item(i).NUM_TICKET & " Nombre Pax: " & lstDWLIST.Item(i).APELLIDO_PAX & "/" & lstDWLIST.Item(i).NOMBRE_PAX & " Doc: " & lstDWLIST.Item(i).DOCUMENT)

                    Next

                End If


            Catch ex As Exception
                strRespuesta = "No se envió " & strMensajeLog
                objEscribeLog.WriteLogGeneral("Se produjo error al enviar " & strMensajeLog & " Mensaje: " & ex.InnerException.ToString, _
                                               Constantes.GNM_EnvioCorreoOADP, _
                                               strCodigoSeguimiento)
                Err.Raise(-5, "WSSabre_EnviarCorreoAdjuntos", ex.InnerException.ToString)
            Finally
                EnviarEmail = Nothing
                Correo = Nothing

                objDAO = Nothing
                lstArchivosAdjuntoas = Nothing
                strMensajeLog = Nothing
                lstDWLIST = Nothing
                lstAgente = Nothing


                objCliente = Nothing
                objDatosAgente = Nothing
                strRutaArchivo = Nothing
                strNombreArchivoHTML = Nothing
                strNombrePasajero = Nothing
            End Try

            Return lstRespuesta

        End Function
    End Class
End Namespace
