Imports GDS_NuevoMundoPersistencia
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports EscribeLog = GDS_MuevoMundoLog.EscribeLog
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Private Function GeneraArchivoHTML(ByVal objCliente As classCliente, _
                                           ByVal objDatosAgente As classDatosAgente, _
                                           ByVal strPseudo As String, _
                                           ByVal strCuerpoBoleto As String, _
                                           ByVal strRuta As String, _
                                           ByVal strNombreArchivo As String, _
                                           ByVal strPNR As String, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal oOp_Firma As Integer, _
                                           ByVal bolTicket As Boolean) As Boolean

            Dim intFinCabecera As Integer = 0
            Dim strRespuesta As String = Nothing
            Dim strCadenaHTML As System.Text.StringBuilder = Nothing
            Dim bolQuitarCabecera As Boolean = False

            Dim bolGenerado As Boolean = False

            Try
                strCadenaHTML = New System.Text.StringBuilder

                strCadenaHTML.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'>" & vbCrLf)
                strCadenaHTML.Append("<html xmlns='http://www.w3.org/1999/xhtml'>" & vbCrLf)
                strCadenaHTML.Append(Head())
                strCadenaHTML.Append("<body>" & vbCrLf)

                '*** CABECERA CON LOGO ***
                strCadenaHTML.Append(CabeceraLogo(strPseudo, objCliente, objDatosAgente, bolTicket, bolQuitarCabecera))

                If bolQuitarCabecera Then
                    intFinCabecera = strCuerpoBoleto.IndexOf("NAME:") - 1
                    strCuerpoBoleto = strCuerpoBoleto.Substring(intFinCabecera, (strCuerpoBoleto.Length - intFinCabecera))
                    strCadenaHTML.Append("<tr>" & vbCrLf)
                    strCadenaHTML.Append("<td colspan='3'><div align='left'>" & vbCrLf)
                    strCadenaHTML.Append("<br/>" & vbCrLf)
                    strCadenaHTML.Append("<br/>" & vbCrLf)
                    strCadenaHTML.Append("<br/>" & vbCrLf)
                    strCadenaHTML.Append("<br/>" & vbCrLf)
                Else
                    '*** CUERPO DEL BOLETO ***
                    strCadenaHTML.Append("<table class='stlBoleto' border='0' align='left' cellpadding='0' cellspacing='0' width='700'>" & vbCrLf)
                    strCadenaHTML.Append("<tr>" & vbCrLf)
                    strCadenaHTML.Append("<td><div align='left'>" & vbCrLf)
                    strCadenaHTML.Append("<br/>" & vbCrLf)
                    strCadenaHTML.Append("<br/>" & vbCrLf)
                    strCadenaHTML.Append("<br/>" & vbCrLf)
                    strCadenaHTML.Append("<br/>" & vbCrLf)
                End If


                '*** CUERPO DEL BOLETO ***
                strCuerpoBoleto = strCuerpoBoleto.Replace(" ", "&nbsp;")
                strCuerpoBoleto = strCuerpoBoleto.Replace(vbCrLf, "<br />")

                strCadenaHTML.Append(strCuerpoBoleto)

                'If bolQuitarCabecera Then
                strCadenaHTML.Append("</div>" & vbCrLf)
                strCadenaHTML.Append("</td>" & vbCrLf)
                'Else
                '    strCadenaHTML.Append("</td>" & vbCrLf)
                'End If

                strCadenaHTML.Append("</tr>" & vbCrLf)
                strCadenaHTML.Append("</table>" & vbCrLf)
                '*************************

                strCadenaHTML.Append("</body>" & vbCrLf)
                strCadenaHTML.Append("</html>" & vbCrLf)

                strRespuesta = strCadenaHTML.ToString

                objEscribeLog = New EscribeLog
                If Not String.IsNullOrEmpty(strRespuesta) Then

                    bolGenerado = objEscribeLog.WriteArchivoHTML(Trim(strRespuesta.ToString), strRuta, Trim(strNombreArchivo), False)

                    If bolTicket Then
                        objEscribeLog.WriteLogGeneral("Se genero el archivo HTML para el TKT: " & strNombreArchivo & " del PNR:" & strPNR, Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)
                    Else
                        objEscribeLog.WriteLogGeneral("Se genero el archivo HTML para el EMD: " & strNombreArchivo & " del PNR:" & strPNR, Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)
                    End If
                Else
                    objEscribeLog.WriteLogGeneral("No genero el archivo HTML para el EMD: " & strNombreArchivo & " del PNR:" & strPNR, Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)
                End If

            Catch ex As Exception
                strRespuesta = Nothing
                Throw New Exception(ex.ToString)
            Finally
                strCuerpoBoleto = Nothing
                strRuta = Nothing
                strNombreArchivo = Nothing
                strPNR = Nothing
                strCodigoSeguimiento = Nothing
                oOp_Firma = Nothing
                bolTicket = Nothing
            End Try

            Return bolGenerado

        End Function
        Private Function Head() As String
            Dim strRespuesta As String = Nothing
            Dim strCadenaHTML As System.Text.StringBuilder = Nothing
            Try

                strCadenaHTML = New System.Text.StringBuilder

                strCadenaHTML.Append("<head>" & vbCrLf)
                strCadenaHTML.Append("<meta http-equiv='Content-Type' content='text/html; charset=iso-8859-1' />" & vbCrLf)
                strCadenaHTML.Append("<title>Boleto</title>" & vbCrLf)
                strCadenaHTML.Append(Style())
                strCadenaHTML.Append("</head>" & vbCrLf)

                strRespuesta = strCadenaHTML.ToString

            Catch ex As Exception
                strRespuesta = Nothing
                Throw New Exception(ex.ToString)
            Finally

            End Try

            Return strRespuesta
        End Function
        Private Function Style() As String
            Dim strRespuesta As String = Nothing
            Dim strCadenaHTML As System.Text.StringBuilder = Nothing
            Try
                strCadenaHTML = New System.Text.StringBuilder

                strCadenaHTML.Append("<style type='text/css'>" & vbCrLf)
                strCadenaHTML.Append("<!--" & vbCrLf)
                strCadenaHTML.Append(".stlBoleto {" & vbCrLf)
                strCadenaHTML.Append("font-family: 'Courier New';" & vbCrLf)
                strCadenaHTML.Append("font-size: 14px;" & vbCrLf)
                strCadenaHTML.Append("}" & vbCrLf)
                strCadenaHTML.Append("-->" & vbCrLf)
                strCadenaHTML.Append("</style>" & vbCrLf)

                strRespuesta = strCadenaHTML.ToString()

            Catch ex As Exception
                strRespuesta = Nothing
                Throw New Exception(ex.ToString)
            End Try
            Return strRespuesta
        End Function
        Private Function CabeceraLogo(ByVal strPseudo As String, _
                                      ByVal objCliente As classCliente, _
                                      ByVal objDatosAgente As classDatosAgente, _
                                      ByVal bolTicket As Boolean, _
                                      ByRef bolQuitarCabecera As Boolean) As String

            Dim strRespuesta As String = Nothing
            Dim strCadenaHTML As System.Text.StringBuilder = Nothing

            '<table border='0' align='left' cellpadding='0' cellspacing='0' width='600'>
            Try

                If Not String.IsNullOrEmpty(strPseudo) Then

                    Select Case strPseudo.ToUpper

                        Case "QF05", "S0X7" ' INTERAGENCIAS

                            strCadenaHTML = New System.Text.StringBuilder
                            strCadenaHTML.Append("<table class='stlBoleto' border='0' align='left' cellpadding='0' cellspacing='0' width='700'>" & vbCrLf)
                            strCadenaHTML.Append("<tr>" & vbCrLf)

                            If objCliente.Logo > 0 Then
                                strCadenaHTML.Append("<td width='200' height='100'><div align='rigth'><img src='http://www.gruponuevomundo.com.pe/intranet/resources/mailing/easy/" & objCliente.DK & ".JPG' /></div></td>" & vbCrLf)
                            Else
                                strCadenaHTML.Append("<td width='200' height='100'>&nbsp;</td>" & vbCrLf)
                            End If


                            If bolTicket Then
                                strCadenaHTML.Append("<td><div align='center'>ELECTRONIC TICKET<br />PASSENGER ITINERARY/RECEIPT</div></td>" & vbCrLf)
                            Else
                                strCadenaHTML.Append("<td><div align='center'>ELECTRONIC MISCELLANEOUS DOCUMENT<br />PASSENGER ITINERARY/RECEIPT</div></td>" & vbCrLf)
                            End If

                            strCadenaHTML.Append("<td width='118' height='100'><div align='rigth'><img src='http://www.gruponuevomundo.com.pe/intranet/resources/mailing/easy/easy-delivery.jpg' /></div></td>" & vbCrLf)
                            strCadenaHTML.Append("</tr>" & vbCrLf)
                            'strCadenaHTML.Append("</table>" & vbCrLf)

                            bolQuitarCabecera = True

                        Case "HW57", "QP75"

                            'http://www.gruponuevomundo.com.pe/intranet/resources/mailing/easy/339.jpg

                        Case "QP35" ' CUENTAS COMERCIALES

                            strCadenaHTML = New System.Text.StringBuilder
                            strCadenaHTML.Append("<table class='stlBoleto' border='0' align='left' cellpadding='0' cellspacing='0' width='600'>" & vbCrLf)
                            strCadenaHTML.Append("<tr>" & vbCrLf)
                            strCadenaHTML.Append("<td width='200' height='100'><div align='rigth'><img src='http://www.gruponuevomundo.com.pe/intranet/resources/mailing/easy/339.jpg' /></div></td>" & vbCrLf)

                            If bolTicket Then
                                strCadenaHTML.Append("<td><div align='center'>ELECTRONIC TICKET<br />PASSENGER ITINERARY/RECEIPT</div></td>" & vbCrLf)
                            Else
                                strCadenaHTML.Append("<td><div align='center'>ELECTRONIC MISCELLANEOUS DOCUMENT<br />PASSENGER ITINERARY/RECEIPT</div></td>" & vbCrLf)
                            End If

                            strCadenaHTML.Append("<td width='118' height='100'>&nbsp;</td>" & vbCrLf)
                            strCadenaHTML.Append("</tr>" & vbCrLf)
                            'strCadenaHTML.Append("</table>" & vbCrLf)

                            bolQuitarCabecera = True

                        Case "QP95" ' CAMINO REAL

                            bolQuitarCabecera = False

                        Case "QQ05" ' DESTINOS MUNDIALES

                            bolQuitarCabecera = False

                    End Select

                    If strCadenaHTML IsNot Nothing Then
                        strRespuesta = strCadenaHTML.ToString
                    End If

                End If

            Catch ex As Exception
                strRespuesta = Nothing
                Throw New Exception(ex.ToString)
            Finally
                strCadenaHTML = Nothing
                strPseudo = Nothing
                objCliente = Nothing
                bolTicket = Nothing
            End Try

            Return strRespuesta

        End Function
        Private Function PieCastellano() As String
            Dim strCadenaHTML As System.Text.StringBuilder = Nothing
            Try
                strCadenaHTML = New System.Text.StringBuilder
                strCadenaHTML.Append("                                 AVISO IMPORTANTE" & vbCrLf)
                strCadenaHTML.Append("- INFORMESE ANTES DE SU COMPRA Y DEL INICIO DEL VIAJE" & vbCrLf)
                strCadenaHTML.Append(" EL VALOR DEL BOLETO SE FIJA EN DOLARES AMERICANOS (USD) Y SOLO PODRA SER ANULADO" & vbCrLf)
                strCadenaHTML.Append(" EL MISMO DIA DE SU EMISION. SI USTED COMPRA DOS BOLETOS SEPARADOS CON DIFERENTES" & vbCrLf)
                strCadenaHTML.Append(" CONDICIONES ESTOS NO ESTAN RELACIONADOS ENTRE SI, Y SON DOS CONTRATOS" & vbCrLf)
                strCadenaHTML.Append(" INDEPENDIENTES UNO DEL OTRO." & vbCrLf)

                strCadenaHTML.Append("REEMBOLSOS Y CAMBIOS SUJETOS A PENALIDAD SOLO PROCEDEN CUANDO LAS CONDICIONES" & vbCrLf)
                strCadenaHTML.Append(" DE LA TARIFA LO PERMITEN, ESTAN SUJETOS A PENALIDADES Y/O GASTOS ADMINISTRATIVOS." & vbCrLf)

                strCadenaHTML.Append("SOLICITE A SU AGENTE DE VIAJES CON AUTORIZACION A EMITIR BOLETOS ELECTRONICOS" & vbCrLf)
                strCadenaHTML.Append(" INFORMACION SOBRE VUELOS EN CONEXION, AEROLINEAS QUE PARTICIPEN EN SU RUTA," & vbCrLf)
                strCadenaHTML.Append(" FRANQUICIA/PESO MAXIMO DE EQUIPAJE EN CADA VUELO, COSTO POR EXCESO DE" & vbCrLf)
                strCadenaHTML.Append(" PESO/PIEZAS DE EQUIPAJE, TEMPORADA Y CONDICIONES DE LA TARIFA." & vbCrLf)

                strCadenaHTML.Append("SOLICITE EL TEXTO DE LAS CONDICIONES GENERALES DEL CONTRARO DE TRANSPORTE REGIDAS" & vbCrLf)
                strCadenaHTML.Append(" POR LOS CONVENIOS DE VARSOVIA Y MONTREAL QUE INCLUYEN CLAUSULAS EN CASOS DE" & vbCrLf)
                strCadenaHTML.Append(" PERDIDAS DE EQUIPAJE. PARA BOLETOS DE RUTAS NACIONALES (DENTRO DEL PERU) LAS" & vbCrLf)
                strCadenaHTML.Append(" CONDICIONES ESTAN ESTABLECIDAS POR LA LEY 27261 Y SU REGLAMENTEO." & vbCrLf)
                strCadenaHTML.Append(" (LEY DE AERONAUTICA CIVIL)" & vbCrLf)

                strCadenaHTML.Append("EL BOLETO QUE UD HA ADQUIRIDO ES :" & vbCrLf)
                strCadenaHTML.Append(" * NO REEMBOLSABLE: DE NO SER UTILIZADO, PIERDE SU VALOR SIN RESPONSABILIDAD" & vbCrLf)
                strCadenaHTML.Append("   PARA LA AGENCIA NI PARA LA LINEA AÉREA." & vbCrLf)
                strCadenaHTML.Append(" * NO TRANSFERIBLE: NO PUEDE SER UTILIZADO POR PERSONA DISTINTA AL TITULAR" & vbCrLf)
                strCadenaHTML.Append(" * NO MODIFICABLE: PARA CUALQUIER CAMBIO DE FECHA, HORA, RUTA, ESTARÁ SUJETO" & vbCrLf)
                strCadenaHTML.Append("   A ALGUNA PENALIDAD O, EN SU DEFECTO, A LA IMPOSIBILIDAD DE MODIFICACIÓN" & vbCrLf)
                strCadenaHTML.Append("   ALGUNA DEPENDIENDO DE LAS CONDICIONES DE LA TARIFA PUBLICADA." & vbCrLf)
                strCadenaHTML.Append(" * NO ENDOSABLE: ESTE BOLETO SOLAMENTE PODRÁ SER HONRANDO POR EL PROVEEDOR ORIGINAL." & vbCrLf)
                strCadenaHTML.Append(" ******************************************************************************" & vbCrLf)
                strCadenaHTML.Append(" POR FAVOR, PRESENTARSE EN EL AEROPUERTO 3 HORAS ANTES DE LA SALIDA DE SU VUELO." & vbCrLf)
                strCadenaHTML.Append(" ******************************************************************************" & vbCrLf)

            Catch ex As Exception

            End Try
        End Function
    End Class
End Namespace