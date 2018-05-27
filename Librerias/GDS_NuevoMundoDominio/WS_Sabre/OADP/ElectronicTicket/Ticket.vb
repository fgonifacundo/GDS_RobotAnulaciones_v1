Imports GDS_NuevoMundoPersistencia
'Imports IWebServices = GDS_NM_WebServicesSabre.IWebServices
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Imports objSabreWS = GDS_NM_WebServicesSabre
Imports EscribeLog = GDS_MuevoMundoLog.EscribeLog
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Dim DAO As New DAO
        'Dim strRutaGenerador As String = "\\vmspm04\c$\OADP\OADP_GENERADOS_X_ROBOT\"
        Dim strRutaGenerador As String = "C:\ServiciosNET\GNM_GeneradorOADP\OADP_GENERADOS_X_ROBOT\" '"C:\ServiciosNET\GNM_GenerardorOADP\ArchivosOADP\"

        Public Function DWLIST(ByVal strPseudo As String, _
                               ByVal strFecha As String, _
                               ByVal strCodigoSeguimiento As String, _
                               ByVal intGDS As Integer, _
                               ByVal intFirmaGDS As Integer, _
                               ByVal intFirmaDB As Integer, _
                               ByVal intEsquema As Integer, _
                               ByVal objSession As classSession) As List(Of String)




            Dim auxFecha As Date = strFecha
            Dim strRespuestaCambio As String() = Nothing
            Dim strRespuestaDWLIST As String = Nothing
            Dim strRespuestadp723 As String = Nothing
            Dim array As Array = Nothing
            Dim arrayBoletos As Array = Nothing

            Dim objDWLIST As classDWLIST = Nothing
            Dim longDK As String = Nothing

            Dim strNombreArchivo As String = Nothing

            Dim strCuerpoBoleto As String = Nothing
            Dim strBoletoGenerado As String = Nothing

            Dim bolTicket As Boolean = False

            Dim intInicioCadena As Integer = 0
            Dim intFinCadena As Integer = 0

            Dim lstDatosAgente As List(Of classDatosAgente) = Nothing
            Dim oblCliente As classCliente = Nothing


            Dim strNombrePasajero As String = Nothing
            Dim strNombreAerolina As String = Nothing

            Dim auxTipo As String = Nothing

            Dim bolSeCreoArchivoTXT As Boolean = False
            Dim bolSeCreoArchivoHTML As Boolean = False

            Dim auxDK As String = Nothing

            Dim lstRespuesta As List(Of String) = Nothing

            Try

                strRespuestaCambio = CambiarPseudo(strPseudo, _
                                                   strCodigoSeguimiento, _
                                                   intGDS, _
                                                   intFirmaGDS, _
                                                   intFirmaDB, _
                                                   objSession)

                strFecha = TraducirMes(auxFecha.ToString("ddMMM").ToUpper)
                strRespuestaDWLIST = SabreCommand("DWLIST/" & strFecha, "DWLIST", _
                                                   strCodigoSeguimiento, _
                                                   intGDS, _
                                                   intFirmaGDS, _
                                                   intFirmaDB, _
                                                   objSession)

                If Not String.IsNullOrEmpty(strRespuestaDWLIST) Then
                    array = strRespuestaDWLIST.Split(vbLf)

                    For i As Integer = 2 To array.Length - 1

                        objDWLIST = New classDWLIST

                        objDWLIST.PSEUDO = strPseudo
                        objDWLIST.FECHA = auxFecha.ToString(Constantes.IWS_DATE_FORMAT_FILE5)

                        auxTipo = Trim(UCase(array(i).ToString().Substring(0, 2)))
                        auxTipo = Trim(IIf(String.IsNullOrEmpty(auxTipo), "P", auxTipo))

                        If auxTipo <> "IT" Then
                            If auxTipo <> "V" Then
                                If auxTipo <> "P" Then
                                    objDWLIST.TIPO = IIf(Not auxTipo.Equals("P"), "P", auxTipo)
                                Else
                                    objDWLIST.TIPO = auxTipo
                                End If
                            Else
                                objDWLIST.TIPO = auxTipo
                            End If
                        Else
                            objDWLIST.TIPO = auxTipo
                        End If


                        objDWLIST.ITEM = Trim(array(i).ToString().Substring(5, 4))
                        objDWLIST.DOCUMENT = Trim(array(i).ToString().Substring(23, 7))

                        If objDWLIST.DOCUMENT.Contains("ITINER") Then
                            objDWLIST.TIPO = "IT"
                        End If

                        objDWLIST.PNR = Trim(array(i).ToString().Substring(34, 6))

                        auxDK = Trim(array(i).ToString().Substring(42, 12))
                        If String.IsNullOrEmpty(auxDK) Then
                            objDWLIST.TIPO = "IT"
                            longDK = "000"
                        Else
                            longDK = CLng(auxDK)
                        End If


                        objDWLIST.DK = longDK
                        objDWLIST.INDICADOR = Trim(array(i).ToString().Substring(60, 3))

                        If Not DAO.VerificaRegistroDWLIST(objDWLIST.PSEUDO, objDWLIST.DOCUMENT, objDWLIST.TIPO, objDWLIST.PNR, objDWLIST.DK, objDWLIST.INDICADOR, objDWLIST.FECHA, strCodigoSeguimiento, intFirmaDB, intEsquema) Then

                            objEscribeLog.WriteLogGeneral(array(i).ToString(), Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)

                            If Not objDWLIST.TIPO.Equals("V") Then
                                If Not objDWLIST.TIPO.Equals("IT") Then

                                    'If objDWLIST.PNR.Equals("YRACXH") Then  '*****************************

                                    'RANGE NUMBER IS NOT NUMERIC
                                    strRespuestadp723 = SabreCommand("DP7/2/3/" & objDWLIST.ITEM, "DP723_" & objDWLIST.ITEM, _
                                                                     strCodigoSeguimiento, _
                                                                     intGDS, _
                                                                     intFirmaGDS, _
                                                                     intFirmaDB, _
                                                                     objSession)

                                    If strRespuestadp723.Contains("FINISH OR IGNORE") Then
                                        IgnoreTransaction(strCodigoSeguimiento, _
                                                          intGDS, _
                                                          intFirmaGDS, _
                                                          intFirmaDB, _
                                                          objSession)
                                        strRespuestadp723 = SabreCommand("DP7/2/3/" & objDWLIST.ITEM, "DP723_" & objDWLIST.ITEM, _
                                                                         strCodigoSeguimiento, _
                                                                         intGDS, _
                                                                         intFirmaGDS, _
                                                                         intFirmaDB, _
                                                                         objSession)
                                    End If


                                    objEscribeLog.WriteLogGeneral("DP7/2/3/" & objDWLIST.ITEM, Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)

                                    strRespuestadp723 = strRespuestadp723.Replace("ELECTRONIC TICKET", "#ELECTRONIC TICKET")
                                    strRespuestadp723 = strRespuestadp723.Replace("ELECTRONIC MISCELLANEOUS DOCUMENT", "#ELECTRONIC MISCELLANEOUS DOCUMENT")
                                    arrayBoletos = strRespuestadp723.Split("#")

                                    For x As Integer = 0 To arrayBoletos.Length - 1

                                        If Not String.IsNullOrEmpty(Trim(arrayBoletos(x))) Then


                                            If arrayBoletos(x).ToString.Contains("ELECTRONIC TICKET") Then
                                                bolTicket = True
                                                strBoletoGenerado = CompletarEspacios(26, Nothing, Nothing) & arrayBoletos(x).ToString

                                                '*** NAME:
                                                strNombrePasajero = Trim(strBoletoGenerado.Substring(strBoletoGenerado.IndexOf("NAME:") + 5, 60).Replace(" ", ""))
                                                strNombrePasajero = Trim(strNombrePasajero.Split(vbLf)(0))

                                                '*** ETKT NBR:
                                                strNombreArchivo = Trim(strBoletoGenerado.Substring(strBoletoGenerado.IndexOf("ETKT NBR:") + 9, 15).Replace(" ", ""))

                                                objEscribeLog.WriteLogGeneral("ETKT NBR: " & strNombreArchivo & " NAME: " & strNombrePasajero, Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)

                                            ElseIf arrayBoletos(x).ToString.Contains("ELECTRONIC MISCELLANEOUS DOCUMENT") Then
                                                strBoletoGenerado = CompletarEspacios(18, Nothing, Nothing) & arrayBoletos(x).ToString
                                                bolTicket = False

                                                '*** NAME:
                                                strNombrePasajero = Trim(strBoletoGenerado.Substring(strBoletoGenerado.IndexOf("NAME:") + 5, 60).Replace(" ", ""))
                                                strNombrePasajero = Trim(strNombrePasajero.Split(vbLf)(0))


                                                '**** EMD NBR:
                                                strNombreArchivo = Trim(strBoletoGenerado.Substring(strBoletoGenerado.IndexOf("EMD NBR:") + 9, 15).Replace(" ", ""))

                                                objEscribeLog.WriteLogGeneral("EMD NBR: " & strNombreArchivo & " NAME: " & strNombrePasajero, Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)

                                            End If

                                            'Nombre: Apellido/nombre
                                            If Not String.IsNullOrEmpty(strNombrePasajero) Then
                                                objDWLIST.APELLIDO_PAX = strNombrePasajero.Split("/")(0)
                                                objDWLIST.NOMBRE_PAX = strNombrePasajero.Split("/")(1)
                                            End If
                                            'Boleto
                                            If Not String.IsNullOrEmpty(strNombreArchivo) Then
                                                objDWLIST.NUM_TICKET = strNombreArchivo
                                            End If


                                            '*** ISSUING AIRLINE:
                                            strNombreAerolina = strBoletoGenerado.Substring(strBoletoGenerado.IndexOf("ISSUING AIRLINE:") + 16, 49)
                                            strNombreAerolina = Trim(strNombreAerolina.Split(vbLf)(0))

                                            '*** FIRMA DE AGENTE
                                            lstDatosAgente = DatosAgente(strBoletoGenerado, _
                                                                         strCodigoSeguimiento, _
                                                                         intGDS, _
                                                                         intFirmaGDS, _
                                                                         intFirmaDB, _
                                                                         intEsquema)

                                            If lstDatosAgente IsNot Nothing Then
                                                objDWLIST.FIRMA_AGENTE = lstDatosAgente.Item(0).FirmaAgente
                                            End If


                                            '*** DK CLIENTE
                                            oblCliente = DatosCliente(strBoletoGenerado, _
                                                                      strCodigoSeguimiento, _
                                                                      intFirmaDB, _
                                                                      intEsquema)

                                            If oblCliente IsNot Nothing Then

                                                '*************************************************
                                                '*** CONSULTAMOS SI SE DEBE PROCESAR EL BOELTO
                                                '*************************************************
                                                If Not DAO.ObtieneBoletoDWLIST(objDWLIST.PSEUDO, objDWLIST.DOCUMENT, objDWLIST.TIPO, objDWLIST.PNR, objDWLIST.DK, objDWLIST.INDICADOR, objDWLIST.FECHA, objDWLIST.NUM_TICKET, strCodigoSeguimiento, intFirmaDB, intEsquema) Then

                                                    strCuerpoBoleto = strBoletoGenerado.Replace(vbLf, vbCrLf)


                                                    'If strPseudo.Equals("QP35") Then

                                                    objDWLIST.RUTA_CARPETA = strRutaGenerador & strPseudo
                                                    'If bolTicket Then
                                                    '    objDWLIST.RUTA_CARPETA = EstableceRuta(auxFecha, strPseudo, oblCliente.DK, objDWLIST.PNR, "TKT")
                                                    'Else
                                                    '    objDWLIST.RUTA_CARPETA = EstableceRuta(auxFecha, strPseudo, oblCliente.DK, objDWLIST.PNR, "EMD")
                                                    'End If

                                                    'End If

                                                    If strPseudo.Equals("QP35") Then
                                                        If bolTicket Then
                                                            CambiosTextoInglesCastellano(strCuerpoBoleto, objDWLIST.PNR, strNombreAerolina, strCodigoSeguimiento, intGDS, objSession)
                                                        End If
                                                    End If



                                                    bolSeCreoArchivoTXT = GeneraArchivoTexto(RTrim(strCuerpoBoleto), objDWLIST.RUTA_CARPETA, Trim(strNombreArchivo), objDWLIST.PNR, strCodigoSeguimiento, bolTicket)

                                                    If strPseudo.Equals("QP35") Then

                                                        'bolSeCreoArchivoHTML = GeneraArchivoHTML(oblCliente, lstDatosAgente.Item(0), strPseudo, RTrim(strCuerpoBoleto), objDWLIST.RUTA_CARPETA, Trim(strNombreArchivo), objDWLIST.PNR, strCodigoSeguimiento, oOp_Firma, bolTicket)

                                                        objDWLIST.ENVIA_CORREO = 1
                                                        objDWLIST.IMPRIME = 1
                                                        objDWLIST.ACTUALIZA_TPR = 1
                                                        bolSeCreoArchivoHTML = True

                                                        'EnviarBoletoPorCorreo1(oblCliente, lstDatosAgente.Item(0), objDWLIST.RUTA_CARPETA, strNombreArchivo, strNombrePasajero, strCodigoSeguimiento, intGDS, oOp_Firma)
                                                    Else
                                                        bolSeCreoArchivoHTML = True
                                                    End If

                                                    If bolSeCreoArchivoTXT And bolSeCreoArchivoHTML Then
                                                        If lstRespuesta Is Nothing Then lstRespuesta = New List(Of String)
                                                        lstRespuesta.Add("N° Tkt: " & objDWLIST.NUM_TICKET & " Nombre Pax: " & objDWLIST.APELLIDO_PAX & "/" & objDWLIST.NOMBRE_PAX & " Doc: " & objDWLIST.DOCUMENT)
                                                        'objDWLIST.RUTA_CARPETA = objDWLIST.RUTA_CARPETA.Replace("C:\", "\\sistemas29san\c$\")
                                                        If Not strPseudo.Equals("QP35") Then
                                                            objDWLIST.RUTA_CARPETA = Nothing
                                                        End If

                                                        DAO.InsertaGDS_DWLIST_OADP(objDWLIST, strCodigoSeguimiento, intFirmaDB, intEsquema)
                                                    End If

                                                End If
                                            Else
                                                strCuerpoBoleto = strBoletoGenerado.Replace(vbLf, vbCrLf)
                                                objDWLIST.RUTA_CARPETA = strRutaGenerador & strPseudo
                                                objDWLIST.DK = "00000"
                                                GeneraArchivoTexto(RTrim(strCuerpoBoleto), objDWLIST.RUTA_CARPETA, Trim(strNombreArchivo), objDWLIST.PNR, strCodigoSeguimiento, bolTicket)
                                                DAO.InsertaGDS_DWLIST_OADP(objDWLIST, strCodigoSeguimiento, intFirmaDB, intEsquema)
                                            End If

                                        End If

                                    Next

                                    'End If  '**************************************

                                End If

                            Else

                                objDWLIST.NUM_TICKET = array(i).ToString().Substring(10, 13)
                                objDWLIST.INDICADOR = "VOID"

                                If Not DAO.ObtieneBoletoDWLIST(objDWLIST.PSEUDO, objDWLIST.DOCUMENT, objDWLIST.TIPO, objDWLIST.PNR, objDWLIST.DK, objDWLIST.INDICADOR, objDWLIST.FECHA, objDWLIST.NUM_TICKET, strCodigoSeguimiento, intFirmaDB, intEsquema) Then
                                    DAO.InsertaGDS_DWLIST_OADP(objDWLIST, strCodigoSeguimiento, intFirmaDB, intEsquema)
                                End If

                            End If

                        Else
                            objEscribeLog.WriteLogGeneral("Ya se registro: " & array(i).ToString(), Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)
                        End If

                    Next

                End If

            Catch ex As Exception
                objEscribeLog.WriteLogGeneral(ex.ToString, Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)
                Throw New Exception(ex.ToString)
            Finally
                strPseudo = Nothing
                strFecha = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing

                auxFecha = Nothing
                strRespuestaCambio = Nothing
                strRespuestaDWLIST = Nothing
                strRespuestadp723 = Nothing
                array = Nothing
                arrayBoletos = Nothing
                strNombreArchivo = Nothing
                strCuerpoBoleto = Nothing
                strBoletoGenerado = Nothing
                bolTicket = Nothing
                intInicioCadena = Nothing
                intFinCadena = Nothing
                lstDatosAgente = Nothing
                oblCliente = Nothing
                strNombrePasajero = Nothing
                auxTipo = Nothing
            End Try

            Return lstRespuesta

        End Function
        Public Function DWLIST_GENERADOR(ByVal strPseudo As String, _
                                         ByVal strFecha As String, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intGDS As Integer, _
                                         ByVal intFirmaGDS As Integer, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer, _
                                         ByVal objSession As classSession) As List(Of String)

            Dim auxFecha As Date = strFecha
            Dim strRespuestaCambio As String() = Nothing
            Dim strRespuestaDWLIST As String = Nothing
            Dim strRespuestadp723 As String = Nothing
            Dim array As Array = Nothing
            Dim arrayBoletos As Array = Nothing

            Dim objDWLIST As classDWLIST = Nothing
            Dim longDK As String = Nothing

            Dim strNombreArchivo As String = Nothing

            Dim strCuerpoBoleto As String = Nothing
            Dim strBoletoGenerado As String = Nothing

            Dim bolTicket As Boolean = False

            Dim intInicioCadena As Integer = 0
            Dim intFinCadena As Integer = 0

            Dim lstDatosAgente As List(Of classDatosAgente) = Nothing
            Dim oblCliente As classCliente = Nothing


            Dim strNombrePasajero As String = Nothing
            Dim strNombreAerolina As String = Nothing

            Dim auxTipo As String = Nothing

            Dim bolSeCreoArchivoTXT As Boolean = False

            Dim auxDK As String = Nothing

            Dim lstRespuesta As List(Of String) = Nothing

            Try

                strRespuestaCambio = CambiarPseudo(strPseudo, _
                                                   strCodigoSeguimiento, _
                                                   intGDS, _
                                                   intFirmaGDS, _
                                                   intFirmaDB, _
                                                   objSession)

                strFecha = TraducirMes(auxFecha.ToString("ddMMM").ToUpper)
                strRespuestaDWLIST = SabreCommand("DWLIST/" & strFecha, "DWLIST", _
                                                  strCodigoSeguimiento, _
                                                  intGDS, _
                                                  intFirmaGDS, _
                                                  intFirmaDB, _
                                                  objSession)

                If Not String.IsNullOrEmpty(strRespuestaDWLIST) Then
                    array = strRespuestaDWLIST.Split(vbLf)

                    For i As Integer = 2 To array.Length - 1

                        objDWLIST = New classDWLIST

                        objDWLIST.PSEUDO = strPseudo
                        objDWLIST.FECHA = auxFecha.ToString(Constantes.IWS_DATE_FORMAT_FILE5)

                        auxTipo = Trim(UCase(array(i).ToString().Substring(0, 2)))
                        auxTipo = Trim(IIf(String.IsNullOrEmpty(auxTipo), "P", auxTipo))

                        If auxTipo <> "IT" Then
                            If auxTipo <> "V" Then
                                If auxTipo <> "P" Then
                                    objDWLIST.TIPO = IIf(Not auxTipo.Equals("P"), "P", auxTipo)
                                Else
                                    objDWLIST.TIPO = auxTipo
                                End If
                            Else
                                objDWLIST.TIPO = auxTipo
                            End If
                        Else
                            objDWLIST.TIPO = auxTipo
                        End If


                        objDWLIST.ITEM = Trim(array(i).ToString().Substring(5, 4))
                        objDWLIST.DOCUMENT = Trim(array(i).ToString().Substring(23, 7))

                        If objDWLIST.DOCUMENT.Contains("ITINER") Then
                            objDWLIST.TIPO = "IT"
                        End If

                        objDWLIST.PNR = Trim(array(i).ToString().Substring(34, 6))

                        auxDK = Trim(array(i).ToString().Substring(42, 12))
                        If String.IsNullOrEmpty(auxDK) Then
                            objDWLIST.TIPO = "IT"
                            longDK = "000"
                        Else
                            longDK = CLng(auxDK)
                        End If


                        objDWLIST.DK = longDK
                        objDWLIST.INDICADOR = Trim(array(i).ToString().Substring(60, 3))

                        If Not DAO.VerificaRegistroDWLIST(objDWLIST.PSEUDO, objDWLIST.DOCUMENT, objDWLIST.TIPO, objDWLIST.PNR, objDWLIST.DK, objDWLIST.INDICADOR, objDWLIST.FECHA, strCodigoSeguimiento, intFirmaDB, intEsquema) Then

                            objEscribeLog.WriteLogGeneral(array(i).ToString(), Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)

                            If Not objDWLIST.TIPO.Equals("V") Then
                                If Not objDWLIST.TIPO.Equals("IT") Then

                                    strRespuestadp723 = SabreCommand("DP7/2/3/" & objDWLIST.ITEM, "DP723_" & objDWLIST.ITEM, _
                                                                     strCodigoSeguimiento, _
                                                                     intGDS, _
                                                                     intFirmaGDS, _
                                                                     intFirmaDB, _
                                                                     objSession)

                                    If strRespuestadp723.Contains("FINISH OR IGNORE") Then
                                        IgnoreTransaction(strCodigoSeguimiento, _
                                                          intGDS, _
                                                          intFirmaGDS, _
                                                          intFirmaDB, _
                                                          objSession)

                                        strRespuestadp723 = SabreCommand("DP7/2/3/" & objDWLIST.ITEM, "DP723_" & objDWLIST.ITEM, _
                                                                         strCodigoSeguimiento, _
                                                                         intGDS, _
                                                                         intFirmaGDS, _
                                                                         intFirmaDB, _
                                                                         objSession)
                                    End If


                                    objEscribeLog.WriteLogGeneral("DP7/2/3/" & objDWLIST.ITEM, Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)

                                    strRespuestadp723 = strRespuestadp723.Replace("ELECTRONIC TICKET", "#ELECTRONIC TICKET")
                                    strRespuestadp723 = strRespuestadp723.Replace("ELECTRONIC MISCELLANEOUS DOCUMENT", "#ELECTRONIC MISCELLANEOUS DOCUMENT")
                                    arrayBoletos = strRespuestadp723.Split("#")

                                    For x As Integer = 0 To arrayBoletos.Length - 1

                                        If Not String.IsNullOrEmpty(Trim(arrayBoletos(x))) Then


                                            If arrayBoletos(x).ToString.Contains("ELECTRONIC TICKET") Then
                                                bolTicket = True
                                                strBoletoGenerado = CompletarEspacios(26, Nothing, Nothing) & arrayBoletos(x).ToString

                                                '*** NAME:
                                                strNombrePasajero = Trim(strBoletoGenerado.Substring(strBoletoGenerado.IndexOf("NAME:") + 5, 60).Replace(" ", ""))
                                                strNombrePasajero = Trim(strNombrePasajero.Split(vbLf)(0))

                                                '*** ETKT NBR:
                                                strNombreArchivo = Trim(strBoletoGenerado.Substring(strBoletoGenerado.IndexOf("ETKT NBR:") + 9, 15).Replace(" ", ""))

                                                objEscribeLog.WriteLogGeneral("ETKT NBR: " & strNombreArchivo & " NAME: " & strNombrePasajero, Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)

                                            ElseIf arrayBoletos(x).ToString.Contains("ELECTRONIC MISCELLANEOUS DOCUMENT") Then
                                                strBoletoGenerado = CompletarEspacios(18, Nothing, Nothing) & arrayBoletos(x).ToString
                                                bolTicket = False

                                                '*** NAME:
                                                strNombrePasajero = Trim(strBoletoGenerado.Substring(strBoletoGenerado.IndexOf("NAME:") + 5, 60).Replace(" ", ""))
                                                strNombrePasajero = Trim(strNombrePasajero.Split(vbLf)(0))


                                                '**** EMD NBR:
                                                strNombreArchivo = Trim(strBoletoGenerado.Substring(strBoletoGenerado.IndexOf("EMD NBR:") + 9, 15).Replace(" ", ""))

                                                objEscribeLog.WriteLogGeneral("EMD NBR: " & strNombreArchivo & " NAME: " & strNombrePasajero, Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)

                                            End If

                                            'Nombre: Apellido/nombre
                                            If Not String.IsNullOrEmpty(strNombrePasajero) Then
                                                objDWLIST.APELLIDO_PAX = strNombrePasajero.Split("/")(0)
                                                objDWLIST.NOMBRE_PAX = strNombrePasajero.Split("/")(1)
                                            End If
                                            'Boleto
                                            If Not String.IsNullOrEmpty(strNombreArchivo) Then
                                                objDWLIST.NUM_TICKET = strNombreArchivo
                                            End If


                                            '*** ISSUING AIRLINE:
                                            strNombreAerolina = strBoletoGenerado.Substring(strBoletoGenerado.IndexOf("ISSUING AIRLINE:") + 16, 49)
                                            strNombreAerolina = Trim(strNombreAerolina.Split(vbLf)(0))

                                            '*** FIRMA DE AGENTE
                                            lstDatosAgente = DatosAgente(strBoletoGenerado, _
                                                                         strCodigoSeguimiento, _
                                                                         intGDS, _
                                                                         intFirmaGDS, _
                                                                         intFirmaDB, _
                                                                         intEsquema)

                                            If lstDatosAgente IsNot Nothing Then
                                                objDWLIST.FIRMA_AGENTE = lstDatosAgente.Item(0).FirmaAgente
                                            End If


                                            '*** DK CLIENTE
                                            oblCliente = DatosCliente(strBoletoGenerado, strCodigoSeguimiento, intFirmaDB, intEsquema)

                                            If oblCliente IsNot Nothing Then

                                                '*************************************************
                                                '*** CONSULTAMOS SI SE DEBE PROCESAR EL BOELTO
                                                '*************************************************
                                                If Not DAO.ObtieneBoletoDWLIST(objDWLIST.PSEUDO, objDWLIST.DOCUMENT, objDWLIST.TIPO, objDWLIST.PNR, objDWLIST.DK, objDWLIST.INDICADOR, objDWLIST.FECHA, objDWLIST.NUM_TICKET, strCodigoSeguimiento, intFirmaDB, intEsquema) Then


                                                    If DAO.VerificaTURBO_PASSENGER_RECEIPT(objDWLIST.NUM_TICKET.Substring(3, 10), objDWLIST.PNR, objDWLIST.DK, strCodigoSeguimiento, intFirmaDB, intEsquema) > 0 Then

                                                        If DAO.SinDocumentoTURBO_PASSENGER_RECEIPT(objDWLIST.NUM_TICKET.Substring(3, 10), objDWLIST.PNR, objDWLIST.DK, strCodigoSeguimiento, intFirmaDB, intEsquema) > 0 Then

GeneraArchivo:

                                                            strCuerpoBoleto = strBoletoGenerado.Replace(vbLf, vbCrLf)


                                                            objDWLIST.RUTA_CARPETA = strRutaGenerador & strPseudo

                                                            'If bolTicket Then
                                                            '    objDWLIST.RUTA_CARPETA = EstableceRuta(auxFecha, strPseudo, oblCliente.DK, objDWLIST.PNR, "TKT")
                                                            'Else
                                                            '    objDWLIST.RUTA_CARPETA = EstableceRuta(auxFecha, strPseudo, oblCliente.DK, objDWLIST.PNR, "EMD")
                                                            'End If


                                                            bolSeCreoArchivoTXT = GeneraArchivoTexto(RTrim(strCuerpoBoleto), objDWLIST.RUTA_CARPETA, Trim(strNombreArchivo), objDWLIST.PNR, strCodigoSeguimiento, bolTicket)

                                                            If bolSeCreoArchivoTXT Then
                                                                DAO.InsertaGDS_DWLIST_OADP(objDWLIST, strCodigoSeguimiento, intFirmaDB, intEsquema)

                                                                If lstRespuesta Is Nothing Then lstRespuesta = New List(Of String)
                                                                lstRespuesta.Add("N° Tkt: " & objDWLIST.NUM_TICKET & " Nombre Pax: " & objDWLIST.APELLIDO_PAX & "/" & objDWLIST.NOMBRE_PAX & " Doc: " & objDWLIST.DOCUMENT)

                                                            End If

                                                        Else
                                                            DAO.InsertaGDS_DWLIST_OADP(objDWLIST, _
                                                                                       strCodigoSeguimiento, _
                                                                                       intFirmaDB, _
                                                                                       intEsquema)
                                                        End If

                                                    Else

                                                        GoTo GeneraArchivo

                                                    End If

                                                End If
                                            Else
                                                strCuerpoBoleto = strBoletoGenerado.Replace(vbLf, vbCrLf)
                                                objDWLIST.RUTA_CARPETA = strRutaGenerador & strPseudo
                                                objDWLIST.DK = "00000"
                                                GeneraArchivoTexto(RTrim(strCuerpoBoleto), objDWLIST.RUTA_CARPETA, Trim(strNombreArchivo), objDWLIST.PNR, strCodigoSeguimiento, bolTicket)
                                                DAO.InsertaGDS_DWLIST_OADP(objDWLIST, strCodigoSeguimiento, intFirmaDB, intEsquema)
                                            End If

                                        End If

                                    Next

                                End If

                            Else

                                objDWLIST.NUM_TICKET = array(i).ToString().Substring(10, 13)
                                objDWLIST.INDICADOR = "VOID"

                                If Not DAO.ObtieneBoletoDWLIST(objDWLIST.PSEUDO, objDWLIST.DOCUMENT, objDWLIST.TIPO, objDWLIST.PNR, objDWLIST.DK, objDWLIST.INDICADOR, objDWLIST.FECHA, objDWLIST.NUM_TICKET, strCodigoSeguimiento, intFirmaDB, intEsquema) Then
                                    DAO.InsertaGDS_DWLIST_OADP(objDWLIST, strCodigoSeguimiento, intFirmaDB, intEsquema)
                                End If

                            End If

                        Else
                            objEscribeLog.WriteLogGeneral("Ya se registro: " & array(i).ToString(), Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)
                        End If

                    Next

                End If

            Catch ex As Exception
                objEscribeLog.WriteLogGeneral(ex.ToString, Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)
                Throw New Exception(ex.ToString)
            Finally
                strPseudo = Nothing
                strFecha = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objSession = Nothing
                auxFecha = Nothing
                strRespuestaCambio = Nothing
                strRespuestaDWLIST = Nothing
                strRespuestadp723 = Nothing
                array = Nothing
                arrayBoletos = Nothing
                strNombreArchivo = Nothing
                strCuerpoBoleto = Nothing
                strBoletoGenerado = Nothing
                bolTicket = Nothing
                intInicioCadena = Nothing
                intFinCadena = Nothing
                lstDatosAgente = Nothing
                oblCliente = Nothing
                strNombrePasajero = Nothing
                auxTipo = Nothing
            End Try

            Return lstRespuesta

        End Function
        Public Function SWS_DWList_PorPseudo(ByVal strPseudo As String, _
                                             ByVal strFecha As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intGDS As Integer, _
                                             ByVal intFirmaGDS As Integer, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal objSession As classSession) As String

            Dim auxFecha As Date = strFecha
            Dim strRespuestaDWLIST As String = Nothing
            Dim strRespuestaCambio As String() = Nothing

            Dim strCuerpoBoleto As String = Nothing
            Dim strBoletoGenerado As String = Nothing


            Try

                strRespuestaCambio = CambiarPseudo(strPseudo, _
                                                   strCodigoSeguimiento, _
                                                   intGDS, _
                                                   intFirmaGDS, _
                                                   intFirmaDB, _
                                                   objSession)

                strFecha = TraducirMes(auxFecha.ToString("ddMMM").ToUpper)
                strRespuestaDWLIST = SabreCommand("DWLIST/" & strFecha, "DWLIST", _
                                                  strCodigoSeguimiento, _
                                                  intGDS, _
                                                  intFirmaGDS, _
                                                  intFirmaDB, _
                                                  objSession)

            Catch ex As Exception
                objEscribeLog.WriteLogGeneral(ex.ToString, Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)
                Throw New Exception(ex.ToString)
            Finally
                strPseudo = Nothing
                strFecha = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing
                auxFecha = Nothing
                strRespuestaCambio = Nothing
                strCuerpoBoleto = Nothing
                strBoletoGenerado = Nothing
            End Try

            Return strRespuestaDWLIST

        End Function
        Private Function EstableceRuta(ByVal strFecha As Date, _
                                       ByVal strPseudo As String, _
                                       ByVal strDKCliente As String, _
                                       ByVal strPNR As String, _
                                       ByVal op As String) As String

            Dim strRura As String = Nothing
            Dim Dia As String = strFecha.ToString("dd")
            Dim Mes As String = strFecha.ToString("MM")
            Dim Anio As String = strFecha.ToString("yyyy")

            Try

                strRura = strRutaGenerador & Anio & "\" & Mes & "\" & Dia & "\" & strPseudo & "\" & strDKCliente & "\" & strPNR & "\" & op & "\"

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            End Try

            Return strRura

        End Function
        Private Function DatosAgente(ByVal strBoletoGenerado As String, _
                                     ByVal strCodigoSeguimiento As String, _
                                     ByVal intGDS As Integer, _
                                     ByVal intFirmaGDS As Integer, _
                                     ByVal intFirmaDB As Integer, _
                                     ByVal intEsquema As Integer) As List(Of classDatosAgente)

            Dim intInicioCadena As Integer = 0
            Dim intFinCadena As Integer = 0
            Dim FirmaAgente As String = Nothing
            Dim objDAO As DAO = Nothing

            Dim lstDatosAgente As List(Of classDatosAgente) = Nothing

            Try
                '*** FIRMA DE AGENTE
                intInicioCadena = strBoletoGenerado.IndexOf("ISSUING AGENT:") + 14
                intFinCadena = strBoletoGenerado.IndexOf("DATE OF ISSUE:")

                FirmaAgente = Trim(strBoletoGenerado.Substring(intInicioCadena, intFinCadena - intInicioCadena).Split("/")(1))
                If Not String.IsNullOrEmpty(FirmaAgente) Then
                    FirmaAgente = FirmaAgente.Substring(5, 2)

                    objDAO = New DAO
                    lstDatosAgente = objDAO.ObtenerDatosAgente("'" & FirmaAgente & "'", _
                                                                 strCodigoSeguimiento, _
                                                                 intFirmaDB, _
                                                                 intEsquema)

                    If lstDatosAgente Is Nothing Then
                        objEscribeLog.WriteLogGeneral("No se encontró datos para el agente: " & FirmaAgente, Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)
                    End If


                Else
                    'ENVIAR CORREO
                    FirmaAgente = String.Empty
                End If

            Catch ex As Exception
                Err.Raise("1", "WSSabre_DatosAgente", ex.ToString)
            Finally
                intInicioCadena = Nothing
                intFinCadena = Nothing
                FirmaAgente = Nothing

                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return lstDatosAgente

        End Function
        Private Function DatosCliente(ByVal strBoletoGenerado As String, _
                                      ByVal strCodigoSeguimiento As String, _
                                      ByVal intFirmaDB As Integer, _
                                      ByVal intEsquema As Integer) As classCliente

            Dim intInicioCadena As Integer = 0
            Dim intFinCadena As Integer = 0
            Dim auxDKCliente As String = Nothing
            Dim objDAO As DAO = Nothing
            Dim lngDK As Long = Nothing

            Dim objCliente As classCliente = Nothing

            Try

                '*** DK CLIENTE
                intInicioCadena = strBoletoGenerado.IndexOf("CUSTOMER NBR:") + 13
                intFinCadena = intInicioCadena + 8
                auxDKCliente = Trim(strBoletoGenerado.Substring(intInicioCadena, intFinCadena - intInicioCadena))

                If Not String.IsNullOrEmpty(auxDKCliente) Then
                    lngDK = CLng(Trim(auxDKCliente))

                    objDAO = New DAO
                    objCliente = objDAO.ObtenerDatosCliente(lngDK, Nothing, _
                                                            strCodigoSeguimiento, _
                                                            intFirmaDB, _
                                                            intEsquema)

                    If objCliente Is Nothing Then
                        objEscribeLog.WriteLogGeneral("No se encontró datos para el DK: " & lngDK, Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)
                    End If
                Else
                    'ENVIAR CORREO
                End If


            Catch ex As Exception
                Err.Raise("1", "WSSabre_DatosCliente", ex.ToString)
            Finally
                intInicioCadena = Nothing
                intFinCadena = Nothing
                auxDKCliente = Nothing
                objDAO = Nothing
                lngDK = Nothing
                strBoletoGenerado = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objCliente

        End Function
        Private Function GeneraArchivoTexto(ByVal strCuerpoBoleto As String, _
                                           ByVal strRuta As String, _
                                           ByVal strNombreArchivo As String, _
                                           ByVal strPNR As String, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal bolTicket As Boolean) As Boolean

            Dim bolGenerado As Boolean = False
            Try


                If Not String.IsNullOrEmpty(strCuerpoBoleto) Then


                    bolGenerado = objEscribeLog.WriteArchivo(RTrim(strCuerpoBoleto), strRuta, Trim(strNombreArchivo), False)

                    If bolTicket Then
                        objEscribeLog.WriteLogGeneral("Se genero el archivo TKT: " & strNombreArchivo & " del PNR:" & strPNR, Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)
                    Else
                        objEscribeLog.WriteLogGeneral("Se genero el archivo EMD: " & strNombreArchivo & " del PNR:" & strPNR, Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)
                    End If
                Else
                    objEscribeLog.WriteLogGeneral("No genero el archivo TXT: " & strNombreArchivo & " del PNR:" & strPNR, Constantes.GNM_GenerardorOADP, strCodigoSeguimiento)
                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCuerpoBoleto = Nothing
                strRuta = Nothing
                strNombreArchivo = Nothing
                strPNR = Nothing
                strCodigoSeguimiento = Nothing

                bolTicket = Nothing
            End Try

            Return bolGenerado

        End Function
        Private Sub CambiosTextoInglesCastellano(ByRef strCuerpoBoleto As String, _
                                                 ByVal strPNR As String, _
                                                 ByVal strNombreAerolina As String, _
                                                 ByVal strCodigoSeguimiento As String, _
                                                 ByVal intGDS As Integer, _
                                                 ByVal objSession As classSession)

            Dim strCadenaNuevoTexto = New System.Text.StringBuilder
            Dim objPNR As classPNR = Nothing
            Dim strCodigoAerolinea As String = Nothing

            Try
                'objPNR = RecuperarPNR(strPNR, strCodigoSeguimiento, intGDS, objSession, oOp_Firma)

                'If objPNR IsNot Nothing Then
                '    If objPNR.MSGError Is Nothing Then
                '        If objPNR.Segmentos IsNot Nothing Then
                '            For i As Integer = 0 To objPNR.Segmentos.Count - 1
                '                strCodigoAerolinea = objPNR.Segmentos.Item(i).CodigoAerolinea
                '            Next
                '        End If
                '    End If
                'End If


                strCuerpoBoleto = strCuerpoBoleto.Replace("                                NOTICE", "#                               NOTICE")

                strCadenaNuevoTexto = New System.Text.StringBuilder
                If Not String.IsNullOrEmpty(strCodigoAerolinea) Then
                    strCadenaNuevoTexto.Append("CODIGO DE AEROLINEA PARA " & strNombreAerolina & ": " & strCodigoAerolinea & vbCrLf)
                    strCadenaNuevoTexto.Append(vbCrLf)
                    strCadenaNuevoTexto.Append(vbCrLf)
                End If

                strCadenaNuevoTexto.Append("                                 AVISO IMPORTANTE" & vbCrLf)
                strCadenaNuevoTexto.Append("- INFORMESE ANTES DE SU COMPRA Y DEL INICIO DEL VIAJE" & vbCrLf)
                strCadenaNuevoTexto.Append(" EL VALOR DEL BOLETO SE FIJA EN DOLARES AMERICANOS (USD) Y SOLO PODRA SER ANULADO" & vbCrLf)
                strCadenaNuevoTexto.Append(" EL MISMO DIA DE SU EMISION. SI USTED COMPRA DOS BOLETOS SEPARADOS CON DIFERENTES" & vbCrLf)
                strCadenaNuevoTexto.Append(" CONDICIONES ESTOS NO ESTAN RELACIONADOS ENTRE SI, Y SON DOS CONTRATOS" & vbCrLf)
                strCadenaNuevoTexto.Append(" INDEPENDIENTES UNO DEL OTRO." & vbCrLf)
                strCadenaNuevoTexto.Append(vbCrLf)
                strCadenaNuevoTexto.Append("REEMBOLSOS Y CAMBIOS SUJETOS A PENALIDAD SOLO PROCEDEN CUANDO LAS CONDICIONES" & vbCrLf)
                strCadenaNuevoTexto.Append(" DE LA TARIFA LO PERMITEN, ESTAN SUJETOS A PENALIDADES Y/O GASTOS ADMINISTRATIVOS." & vbCrLf)
                strCadenaNuevoTexto.Append(vbCrLf)
                strCadenaNuevoTexto.Append("SOLICITE A SU AGENTE DE VIAJES CON AUTORIZACION A EMITIR BOLETOS ELECTRONICOS" & vbCrLf)
                strCadenaNuevoTexto.Append(" INFORMACION SOBRE VUELOS EN CONEXION, AEROLINEAS QUE PARTICIPEN EN SU RUTA," & vbCrLf)
                strCadenaNuevoTexto.Append(" FRANQUICIA/PESO MAXIMO DE EQUIPAJE EN CADA VUELO, COSTO POR EXCESO DE" & vbCrLf)
                strCadenaNuevoTexto.Append(" PESO/PIEZAS DE EQUIPAJE, TEMPORADA Y CONDICIONES DE LA TARIFA." & vbCrLf)
                strCadenaNuevoTexto.Append(vbCrLf)
                strCadenaNuevoTexto.Append("SOLICITE EL TEXTO DE LAS CONDICIONES GENERALES DEL CONTRARO DE TRANSPORTE REGIDAS" & vbCrLf)
                strCadenaNuevoTexto.Append(" POR LOS CONVENIOS DE VARSOVIA Y MONTREAL QUE INCLUYEN CLAUSULAS EN CASOS DE" & vbCrLf)
                strCadenaNuevoTexto.Append(" PERDIDAS DE EQUIPAJE. PARA BOLETOS DE RUTAS NACIONALES (DENTRO DEL PERU) LAS" & vbCrLf)
                strCadenaNuevoTexto.Append(" CONDICIONES ESTAN ESTABLECIDAS POR LA LEY 27261 Y SU REGLAMENTEO." & vbCrLf)
                strCadenaNuevoTexto.Append(" (LEY DE AERONAUTICA CIVIL)" & vbCrLf)
                strCadenaNuevoTexto.Append(vbCrLf)
                strCadenaNuevoTexto.Append("EL BOLETO QUE UD HA ADQUIRIDO ES :" & vbCrLf)
                strCadenaNuevoTexto.Append(" * NO REEMBOLSABLE: DE NO SER UTILIZADO, PIERDE SU VALOR SIN RESPONSABILIDAD" & vbCrLf)
                strCadenaNuevoTexto.Append("   PARA LA AGENCIA NI PARA LA LINEA AÉREA." & vbCrLf)
                strCadenaNuevoTexto.Append(" * NO TRANSFERIBLE: NO PUEDE SER UTILIZADO POR PERSONA DISTINTA AL TITULAR" & vbCrLf)
                strCadenaNuevoTexto.Append(" * NO MODIFICABLE: PARA CUALQUIER CAMBIO DE FECHA, HORA, RUTA, ESTARÁ SUJETO" & vbCrLf)
                strCadenaNuevoTexto.Append("   A ALGUNA PENALIDAD O, EN SU DEFECTO, A LA IMPOSIBILIDAD DE MODIFICACIÓN" & vbCrLf)
                strCadenaNuevoTexto.Append("   ALGUNA DEPENDIENDO DE LAS CONDICIONES DE LA TARIFA PUBLICADA." & vbCrLf)
                strCadenaNuevoTexto.Append(" * NO ENDOSABLE: ESTE BOLETO SOLAMENTE PODRÁ SER HONRANDO POR EL PROVEEDOR ORIGINAL." & vbCrLf)
                strCadenaNuevoTexto.Append(" ******************************************************************************" & vbCrLf)
                strCadenaNuevoTexto.Append(" POR FAVOR, PRESENTARSE EN EL AEROPUERTO 3 HORAS ANTES DE LA SALIDA DE SU VUELO." & vbCrLf)
                strCadenaNuevoTexto.Append(" ******************************************************************************" & vbCrLf)


                strCuerpoBoleto = strCuerpoBoleto.Split("#")(0) & strCadenaNuevoTexto.ToString()

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCadenaNuevoTexto = Nothing
            End Try

        End Sub


        Public Function GeneraBoleto(ByVal TCTicket As classTicketCoupon.TCTicket, _
                                     ByVal objTravelItineraryReadRS As classPNR, _
                                     ByVal strCodigoSeguimiento As String, _
                                     ByVal oGDS As Integer, _
                                     ByVal objSession As classSession, _
                                     ByVal oOp_Firma As Integer) As String


            Dim ElectronicTicket As classElectronicTicket = Nothing
            Dim auxTaxes As classTaxes = Nothing
            Dim auxMonto As classMonto = Nothing
            Dim fecha As Date = Nothing
            Try

                If TCTicket IsNot Nothing Then

                    '*** MARCA DE REMISION ***
                    If Not String.IsNullOrEmpty(TCTicket.TCCouponData.ExchangeInd) Then
                        If TCTicket.TCCouponData.ExchangeInd.ToUpper.Equals("X") Then
                            ElectronicTicket.Exchange = True
                        End If
                    End If

                    '*** PREFIJO ***
                    If Not String.IsNullOrEmpty(TCTicket.TicketNumber) Then
                        ElectronicTicket.Prefijo = TCTicket.TicketNumber.Substring(0, 3)
                        ElectronicTicket.NumeroBoleto = TCTicket.TicketNumber.Substring(3, 10)
                    End If
                    '*** NUEMERO TICKET ***


                    '*** AGENTE EMISOR ***
                    If (Not String.IsNullOrEmpty(TCTicket.PseudoCityCode)) And (Not String.IsNullOrEmpty(TCTicket.IssuingAgent)) Then
                        ElectronicTicket.AgenteEmisor = TCTicket.PseudoCityCode & TCTicket.IssuingAgent
                    End If

                    '*** DATOS IATA ***
                    If Not String.IsNullOrEmpty(TCTicket.LNIATA) Then
                        ElectronicTicket.IATA = DatosIata(TCTicket.LNIATA, _
                                                          ElectronicTicket.Prefijo, _
                                                          ElectronicTicket.NumeroBoleto, _
                                                          strCodigoSeguimiento, _
                                                          oGDS)
                    End If


                    '*** AGENTE CREADOR ***
                    If objTravelItineraryReadRS IsNot Nothing Then
                        If (Not String.IsNullOrEmpty(objTravelItineraryReadRS.PseudoAAA)) And (Not String.IsNullOrEmpty(objTravelItineraryReadRS.AgenteCreador)) Then
                            ElectronicTicket.AgenteCreador = objTravelItineraryReadRS.PseudoAAA & objTravelItineraryReadRS.AgenteCreador
                        End If
                    End If

                    If TCTicket.TCCouponData IsNot Nothing Then

                        '*** FECHA DE EMISION ***
                        If Not String.IsNullOrEmpty(TCTicket.TCCouponData.IssueDate) Then
                            fecha = New Date
                            fecha = TCTicket.TCCouponData.IssueDate.ToString
                            ElectronicTicket.FechaEmision = TraducirMes(fecha.ToString("ddMMMyy").ToUpper)
                            fecha = Nothing
                        End If

                        '*** DK ****
                        If TCTicket.TCCouponData.ItineraryRef IsNot Nothing Then
                            If Not String.IsNullOrEmpty(TCTicket.TCCouponData.ItineraryRef.CustomerIdentifier) Then
                                ElectronicTicket.DK = TCTicket.TCCouponData.ItineraryRef.CustomerIdentifier
                            End If
                        End If

                        '*** TOUR CODE
                        If Not String.IsNullOrEmpty(TCTicket.TCCouponData.ProductID) Then
                            ElectronicTicket.TourCode = TCTicket.TCCouponData.ProductID
                        End If

                        '*** ENDOSOS ***
                        If TCTicket.TCCouponData.AirItineraryPricing IsNot Nothing Then
                            If TCTicket.TCCouponData.AirItineraryPricing.Endorsements IsNot Nothing Then

                                For i As Integer = 0 To TCTicket.TCCouponData.AirItineraryPricing.Endorsements.Count - 1
                                    If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.Endorsements.Item(0)) Then
                                        ElectronicTicket.Endosos &= IIf(String.IsNullOrEmpty(ElectronicTicket.Endosos), "", vbCr) & TCTicket.TCCouponData.AirItineraryPricing.Endorsements.Item(0).ToString
                                    End If
                                Next

                            End If
                        End If

                        '*** FARE CACULATION ***
                        If TCTicket.TCCouponData.AirItineraryPricing IsNot Nothing Then
                            If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.FareCalculation) Then
                                ElectronicTicket.CalculoTarifa = TCTicket.TCCouponData.AirItineraryPricing.FareCalculation
                            End If

                            If TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare IsNot Nothing Then

                                '*** FARE ***
                                'If TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCBaseFare IsNot Nothing Then
                                '    TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCBaseFare.Amount
                                '    TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCBaseFare.CurrencyCode

                                '    If TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes IsNot Nothing Then
                                '        TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.Total
                                '    End If


                                'End If



                                '*** IMPUESTOS PAID ***
                                If TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes IsNot Nothing Then
                                    If TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax IsNot Nothing Then

                                        For i As Integer = 0 To TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Count - 1
                                            If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).CodePaid) Then
                                                If TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).CodePaid.Equals("PD") Then
                                                    auxTaxes = New classTaxes

                                                    auxTaxes.Paid = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).CodePaid

                                                    If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).TaxCode) Then
                                                        auxTaxes.Codigo = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).TaxCode
                                                    End If

                                                    If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).Amount) Then
                                                        auxTaxes.Monto = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTaxes.TCTax.Item(i).Amount
                                                    End If

                                                    If ElectronicTicket.TaxesPaid Is Nothing Then ElectronicTicket.TaxesPaid = New List(Of classTaxes)
                                                    ElectronicTicket.TaxesPaid.Add(auxTaxes)
                                                    auxTaxes = Nothing

                                                End If
                                            End If
                                        Next

                                    End If
                                End If

                                'Total
                                If TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTotalFare IsNot Nothing Then
                                    auxMonto = New classMonto
                                    If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTotalFare.CurrencyCode) Then
                                        auxMonto.Moneda = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTotalFare.CurrencyCode
                                    End If

                                    If Not String.IsNullOrEmpty(TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTotalFare.Amount) Then
                                        auxMonto.Monto = TCTicket.TCCouponData.AirItineraryPricing.TCItinTotalFare.TCTotalFare.Amount
                                        If auxMonto.Monto.Contains("A") Then
                                            auxMonto.Monto = auxMonto.Monto.Replace("A", "")
                                        End If
                                    End If

                                    ElectronicTicket.Total = New classMonto
                                    ElectronicTicket.Total = auxMonto

                                End If

                            End If

                        End If


                        '*** TICKET ORIGINAL ****
                        If TCTicket.TCCouponData.ExchangeData IsNot Nothing Then
                            If Not String.IsNullOrEmpty(TCTicket.TCCouponData.ExchangeData.OriginalTicketNumber) Then
                                ElectronicTicket.TicketOriginal = TCTicket.TCCouponData.ExchangeData.OriginalTicketNumber
                            End If
                        End If


                        If TCTicket.TCCouponData.TCCustomer IsNot Nothing Then
                            If TCTicket.TCCouponData.TCCustomer.TCPersonName IsNot Nothing Then

                                '*** FORMA DE PAGO ***
                                If TCTicket.TCCouponData.TCCustomer.TCPayment IsNot Nothing Then
                                    For i As Integer = 0 To TCTicket.TCCouponData.TCCustomer.TCPayment.Count - 1
                                        ElectronicTicket.FormaPago = FormaPago(TCTicket.TCCouponData.TCCustomer.TCPayment.Item(i).Type)
                                    Next

                                End If

                                '*** NOMBRE DEL PASAJERO ***
                                If Not String.IsNullOrEmpty(TCTicket.TCCouponData.TCCustomer.TCPersonName.Surname) Then
                                    ElectronicTicket.NombrePasajero = TCTicket.TCCouponData.TCCustomer.TCPersonName.Surname
                                End If

                                If Not String.IsNullOrEmpty(TCTicket.TCCouponData.TCCustomer.TCPersonName.GivenName) Then
                                    ElectronicTicket.NombrePasajero &= "/" & TCTicket.TCCouponData.TCCustomer.TCPersonName.GivenName
                                End If

                                '*** DUCUMENTO DE IDENTIDAD ***
                                If Not String.IsNullOrEmpty(TCTicket.TCCouponData.TCCustomer.TCPersonName.NameReference) Then
                                    ElectronicTicket.DocumentoPasajero = TCTicket.TCCouponData.TCCustomer.TCPersonName.NameReference
                                End If

                            End If
                        End If
                    End If
                End If

            Catch ex As Exception

            End Try
            Return ""
        End Function
        Public Function DatosIata(ByVal strIATA As String, _
                                  ByVal strPrefijo As String, _
                                  ByVal strNumeroBoleto As String, _
                                  ByVal strCodigoSeguimiento As String, _
                                  ByVal intGDS As Integer) As classIata

            Dim objIata As classIata = Nothing

            Try
                If Not String.IsNullOrEmpty(strIATA) Then

                    objIata.Iata = strIATA

                    If objIata.Iata.Equals("91500286") Then
                        objIata.NombreIata = "NM"
                        objIata.CiudadIata = "LIMA"
                        objIata.PaisIata = "PE"
                    ElseIf objIata.Iata.Equals("91500312") Then
                        objIata.NombreIata = "INTERAGENCIAS"
                        objIata.CiudadIata = "LIMA"
                        objIata.PaisIata = "PE"
                    Else
                        objEscribeLog.WriteLog("Iata " & objIata.Iata & " no registrada " & strPrefijo & strNumeroBoleto, strCodigoSeguimiento, intGDS)
                    End If

                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally

            End Try

            Return objIata

        End Function
        Public Function FormaPago(ByVal strType As String) As String

            Dim strRespuesta As String = Nothing

            Try

                If strType.Equals("CA") Then
                    strRespuesta = "CA"
                ElseIf strType.Equals("VI") Then
                    strRespuesta = "VISA"
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            End Try

            Return strRespuesta

        End Function
    End Class
End Namespace

