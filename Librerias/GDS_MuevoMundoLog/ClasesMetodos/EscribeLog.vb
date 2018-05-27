Imports System.IO
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports GDS_NuevoMundoPersistencia


Public Class EscribeLog
    Private objAppConfig As New GDS_NuevoMundoPersistencia.classAppConfig
    Public Sub WriteLog(ByVal Mensaje As String, _
                        ByVal strNombreArchivo As String, _
                        ByVal strNombreCarpeta As String)

        Dim sFecha As String = Nothing
        Dim sHora As String = Nothing
        Dim strCarpera As String = Nothing
        Dim oRuta As String = RutaLOG()
        Dim oStreamWriter As StreamWriter
        Dim Linea As String = Nothing

        Try

            sFecha = Format(Now, Constantes.IWS_DATE_FORMAT_FILE)
            sHora = Format(Now, Constantes.IWS_TIME_FORMAT_FILE)

            strCarpera = oRuta & sFecha & "\" & strNombreCarpeta.Split("#")(0)

            If System.IO.Directory.Exists(strCarpera) = False Then
                System.IO.Directory.CreateDirectory(strCarpera)
            End If

            If strNombreCarpeta.Split("#").Length = 1 Then
                oStreamWriter = New StreamWriter(strCarpera & "\" & sHora & "_" & strNombreCarpeta.Split("#")(0) & "_" & strNombreArchivo & ".xml")
            Else
                oStreamWriter = New StreamWriter(strCarpera & "\" & sHora & "_" & strNombreCarpeta.Split("#")(0) & "_" & strNombreArchivo & "_" & strNombreCarpeta.Split("#")(1) & ".xml")
            End If

            Linea = "[" & sHora & "]  " & Mensaje
            oStreamWriter.WriteLine(Linea)
            oStreamWriter.Close()

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        Finally
            Mensaje = Nothing
            strNombreArchivo = Nothing
            strNombreCarpeta = Nothing

            sFecha = Nothing
            sHora = Nothing
            strCarpera = Nothing
            oRuta = Nothing
            oStreamWriter = Nothing
            Linea = Nothing
        End Try

    End Sub
    Public Sub WriteLogImpresion(ByVal Mensaje As String, _
                                 ByVal strNombreCarpeta As String)

        Dim sFecha As String = Nothing
        Dim sHora As String = Nothing

        Dim oRuta As String = RutaLOG()


        Dim oSW As StreamWriter
        Dim Linea As String = Nothing

        Try

            strNombreCarpeta = strNombreCarpeta.Split("#")(0)

            sFecha = Format(Now, "yyyyMMdd")
            sHora = Format(Now, "HH:mm:ss")

            oRuta &= sFecha

            If System.IO.Directory.Exists(oRuta & "\" & strNombreCarpeta) = False Then
                System.IO.Directory.CreateDirectory(oRuta & "\" & strNombreCarpeta)
            End If

            oSW = New StreamWriter(oRuta & "\" & strNombreCarpeta & "\LogImpresionArchivos.txt", True)
            Linea = "[" & sHora & "]  " & Mensaje
            oSW.WriteLine(Linea)
            oSW.Close()

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        Finally
            sFecha = Nothing
            sHora = Nothing
            oRuta = Nothing
            Linea = Nothing
            oSW = Nothing
        End Try

    End Sub
    Public Sub WriteLogGeneral(ByVal Mensaje As String, _
                               ByVal strNombreArchivo As String, _
                               ByVal strCodigoSeguimiento As String)

        Dim sFecha As String = Nothing
        Dim sHora As String = Nothing

        Dim oRuta As String = RutaLOG()


        Dim oSW As StreamWriter
        Dim Linea As String = Nothing

        Try

            strCodigoSeguimiento = strCodigoSeguimiento.Split("#")(0)

            sFecha = Format(Now, "yyyyMMdd")
            sHora = Format(Now, "HH:mm:ss")

            oRuta &= sFecha & "\"

            If System.IO.Directory.Exists(oRuta & strCodigoSeguimiento) = False Then
                System.IO.Directory.CreateDirectory(oRuta & strCodigoSeguimiento)
            End If

            oSW = New StreamWriter(oRuta & strCodigoSeguimiento & "\" & strNombreArchivo, True)
            Linea = "[" & sHora & "]  " & Mensaje
            oSW.WriteLine(Linea)
            oSW.Close()

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        Finally
            sFecha = Nothing
            sHora = Nothing
            oRuta = Nothing
            Linea = Nothing
            oSW = Nothing
        End Try

    End Sub
    Public Function WriteArchivo(ByVal strCuerpoArchivo As String, _
                                 ByVal strRutaArchivo As String, _
                                 ByVal strNombreArchivo As String, _
                                 Optional ByVal bolIncluirHoraenNombre As Boolean = True) As Boolean

        Dim sFecha As String = Nothing
        Dim sHora As String = Nothing

        Dim oSW As StreamWriter
        Dim Linea As String = Nothing
        Dim strCadenaNombreArchivo As String = Nothing

        Dim bolGenerado As Boolean = False

        Try

            sFecha = Format(Now, "yyyyMMdd")
            sHora = Format(Now, "HH:mm:ss")

            If System.IO.Directory.Exists(strRutaArchivo) = False Then
                System.IO.Directory.CreateDirectory(strRutaArchivo)
            End If

            If bolIncluirHoraenNombre Then
                strCadenaNombreArchivo = strRutaArchivo & "\" & sHora.ToString.Replace(":", "") & "_" & strNombreArchivo & ".txt"
            Else
                strCadenaNombreArchivo = strRutaArchivo & "\" & strNombreArchivo & ".txt"
            End If


            If Not System.IO.File.Exists(strCadenaNombreArchivo) Then


                oSW = New StreamWriter(strCadenaNombreArchivo, True)

                Linea = strCuerpoArchivo
                oSW.WriteLine(Linea)
                oSW.Close()

                bolGenerado = True

            End If

        Catch ex As Exception
            bolGenerado = False
            Throw New Exception(ex.ToString & " " & strRutaArchivo)
        Finally
            sFecha = Nothing
            sHora = Nothing
            strRutaArchivo = Nothing
            strNombreArchivo = Nothing
            strCuerpoArchivo = Nothing
            Linea = Nothing
            oSW = Nothing
        End Try

        Return bolGenerado

    End Function
    Public Function WriteArchivoHTML(ByVal strCuerpoArchivo As String, _
                                     ByVal strRutaArchivo As String, _
                                     ByVal strNombreArchivo As String, _
                                     Optional ByVal bolIncluirHoraenNombre As Boolean = True) As Boolean

        Dim sFecha As String = Nothing
        Dim sHora As String = Nothing

        Dim oSW As StreamWriter
        Dim Linea As String = Nothing

        Dim strCadenaNombreArchivo As String = Nothing
        Dim bolGenerado As Boolean = False

        Try

            sFecha = Format(Now, "yyyyMMdd")
            sHora = Format(Now, "HH:mm:ss")

            If System.IO.Directory.Exists(strRutaArchivo) = False Then
                System.IO.Directory.CreateDirectory(strRutaArchivo)
            End If


            If bolIncluirHoraenNombre Then
                strCadenaNombreArchivo = strRutaArchivo & sHora.ToString.Replace(":", "") & "_" & strNombreArchivo & ".html"
            Else
                strCadenaNombreArchivo = strRutaArchivo & strNombreArchivo & ".html"
            End If

            If Not System.IO.File.Exists(strCadenaNombreArchivo) Then

                oSW = New StreamWriter(strCadenaNombreArchivo, True)

                Linea = strCuerpoArchivo
                oSW.WriteLine(Linea)
                oSW.Close()

                bolGenerado = True

            End If

        Catch ex As Exception
            bolGenerado = False
            Throw New Exception(ex.ToString)
        Finally
            sFecha = Nothing
            sHora = Nothing
            strRutaArchivo = Nothing
            strNombreArchivo = Nothing
            strCuerpoArchivo = Nothing
            Linea = Nothing
            oSW = Nothing
        End Try

        Return bolGenerado

    End Function
    Private Function RutaLOG() As String
        Return objAppConfig.Obtiene_RUTA_FILE_PAYLOAD_SABRE()
    End Function
    Private Function RutaLOGRobotAnulacion() As String
        Return objAppConfig.Obtiene_RUTA_FILE_ROBOTANULACION_SABRE()
    End Function

    Public Sub WriteLogRobotAnulacion(ByVal Mensaje As String, _
                                      ByVal strNombreArchivo As String, _
                                      ByVal strNombreCarpeta As String)

        Dim sFecha As String = Nothing
        Dim sHora As String = Nothing
        Dim strCarpera As String = Nothing
        Dim oRuta As String = RutaLOGRobotAnulacion()
        Dim oStreamWriter As StreamWriter
        Dim Linea As String = Nothing

        Try

            sFecha = Format(Now, Constantes.IWS_DATE_FORMAT_FILE)
            sHora = Format(Now, Constantes.IWS_TIME_FORMAT_FILE)

            strCarpera = oRuta & sFecha & "\" & strNombreCarpeta.Split("#")(0)

            If System.IO.Directory.Exists(strCarpera) = False Then
                System.IO.Directory.CreateDirectory(strCarpera)
            End If

            If strNombreCarpeta.Split("#").Length = 1 Then
                oStreamWriter = New StreamWriter(strCarpera & "\" & sHora & "_" & strNombreCarpeta.Split("#")(0) & "_" & strNombreArchivo & ".xml")
            Else
                oStreamWriter = New StreamWriter(strCarpera & "\" & sHora & "_" & strNombreCarpeta.Split("#")(0) & "_" & strNombreArchivo & "_" & strNombreCarpeta.Split("#")(1) & ".xml")
            End If

            Linea = "[" & sHora & "]  " & Mensaje
            oStreamWriter.WriteLine(Linea)
            oStreamWriter.Close()

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        Finally
            Mensaje = Nothing
            strNombreArchivo = Nothing
            strNombreCarpeta = Nothing

            sFecha = Nothing
            sHora = Nothing
            strCarpera = Nothing
            oRuta = Nothing
            oStreamWriter = Nothing
            Linea = Nothing
        End Try

    End Sub

    Public Function WriteLogRobotSABRE(ByVal Mensaje As List(Of String), _
                                  ByVal strNombreArchivo As String, _
                                  ByVal strNombreCarpeta As String) As String

        Dim sFecha As String = Nothing
        Dim sHora As String = Nothing
        Dim strCarpera As String = Nothing
        Dim oRuta As String = RutaLOGRobotAnulacion()
        Dim oStreamWriter As StreamWriter
        Dim Linea As String = Nothing
        Dim rutaRetorno As String = Nothing
        Try

            sFecha = Format(Now, Constantes.IWS_DATE_FORMAT_FILE)
            sHora = Format(Now, Constantes.IWS_TIME_FORMAT_FILE)

            strCarpera = oRuta & sFecha & "\" & strNombreCarpeta.Split("#")(0)

            If System.IO.Directory.Exists(strCarpera) = False Then
                System.IO.Directory.CreateDirectory(strCarpera)
            End If


            If strNombreCarpeta.Split("#").Length = 1 Then
                oStreamWriter = New StreamWriter(strCarpera & "\" & sHora & "_" & strNombreCarpeta.Split("#")(0) & "_" & strNombreArchivo & ".txt")
                rutaRetorno = strCarpera & "#" & sHora & "_" & strNombreCarpeta.Split("#")(0) & "_" & strNombreArchivo & ".txt"
            Else
                oStreamWriter = New StreamWriter(strCarpera & "\" & sHora & "_" & strNombreCarpeta.Split("#")(0) & "_" & strNombreArchivo & "_" & strNombreCarpeta.Split("#")(1) & ".txt")
                rutaRetorno = strCarpera & "#" & sHora & "_" & strNombreCarpeta.Split("#")(0) & "_" & strNombreArchivo & "_" & strNombreCarpeta.Split("#")(1) & ".txt"
            End If

            Dim contador As Integer = 0
            For Each line As String In Mensaje
                Linea = "linea " & contador & " de " & Mensaje.Count - 1 & "[" & sHora & "]  " & line
                oStreamWriter.WriteLine(Linea)
                contador += 1
            Next

            oStreamWriter.Close()

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        Finally
            Mensaje = Nothing
            strNombreArchivo = Nothing
            strNombreCarpeta = Nothing

            sFecha = Nothing
            sHora = Nothing
            strCarpera = Nothing
            oRuta = Nothing
            oStreamWriter = Nothing
            Linea = Nothing
        End Try
        Return rutaRetorno
    End Function


End Class
