Imports GDS_NuevoMundoPersistencia
Imports GDS_NM_Mensajeria
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function CambioAerolinea(ByVal strTransportador As String, _
                                        ByVal strCodigoSeguimiento As String, _
                                        ByVal intFirmaDB As Integer, _
                                        ByVal intEsquema As Integer) As String

            Dim objDAO As DAO = Nothing
            Dim strRespuesta As String = Nothing
            Try

                objDAO = New DAO
                strRespuesta = objDAO.CambioAerolinea(strTransportador, _
                                                      strCodigoSeguimiento, _
                                                      intFirmaDB, _
                                                      intEsquema)

            Catch ex As Exception
                strRespuesta = Nothing
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strTransportador = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return strRespuesta

        End Function
        Public Function AerolineaHomologa(ByVal strTransportador As String, _
                                          ByVal strCodigoSeguimiento As String, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal intEsquema As Integer) As String

            Dim objDAO As DAO = Nothing
            Dim strRespuesta As String = Nothing
            Try

                objDAO = New DAO
                strRespuesta = objDAO.AerolineaHomologa(strTransportador, _
                                                        strCodigoSeguimiento, _
                                                        intFirmaDB, _
                                                        intEsquema)

            Catch ex As Exception
                strRespuesta = Nothing
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strTransportador = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return strRespuesta

        End Function
        Public Function AerolineaEquivalente(ByVal strTransportador As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As String

            Dim objDAO As DAO = Nothing
            Dim strRespuesta As String = Nothing
            Try

                objDAO = New DAO
                strRespuesta = objDAO.AerolineaEquivalente(strTransportador, _
                                                              strCodigoSeguimiento, _
                                                              intFirmaDB, _
                                                              intEsquema)

            Catch ex As Exception
                strRespuesta = Nothing
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strTransportador = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return strRespuesta

        End Function
        Public Function ExisteReglas(ByVal Id_Grupo As Integer, _
                                     ByVal strTransportador As String, _
                                     ByVal strCodigoSeguimiento As String, _
                                     ByVal intFirmaDB As Integer, _
                                     ByVal intEsquema As Integer) As Integer

            Dim objDAO As DAO = Nothing
            Dim intRespuesta As Integer = -1
            Try

                objDAO = New DAO
                intRespuesta = objDAO.ExisteReglas(Id_Grupo, _
                                                     strTransportador, _
                                                     strCodigoSeguimiento, _
                                                     intFirmaDB, _
                                                     intEsquema)

            Catch ex As Exception
                intRespuesta = -1
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                Id_Grupo = Nothing
                strTransportador = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return intRespuesta

        End Function
        Public Function ObtenerIATA(ByVal strPseudo As String, _
                                    ByVal strCodigoSeguimiento As String, _
                                    ByVal intFirmaDB As Integer, _
                                    ByVal intEsquema As Integer) As classIata

            Dim objDAO As DAO = Nothing
            Dim strResultado As classIata = Nothing
            Try

                objDAO = New DAO
                strResultado = objDAO.ObtenerIATA(strPseudo, _
                                                    strCodigoSeguimiento, _
                                                    intFirmaDB, _
                                                    intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strPseudo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return strResultado

        End Function
        Public Function BorrarTarifaBulkEvaluacion(ByVal strCodigoPNR As String, _
                                                   ByVal strCodigoSeguimiento As String, _
                                                   ByVal intFirmaDB As Integer, _
                                                   ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolResultado As Boolean = False
            Try

                objDAO = New DAO
                bolResultado = objDAO.BorrarTarifaBulkEvaluacion(strCodigoPNR, _
                                                                 strCodigoSeguimiento, _
                                                                 intFirmaDB, _
                                                                 intEsquema)




            Catch ex As Exception
                bolResultado = Nothing
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return bolResultado

        End Function
        Public Function InsertaConceptos(ByVal strTabla As String, _
                                         ByVal strXML As String, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim bolResultado As Boolean = False
            Try

                objDAO = New DAO
                bolResultado = objDAO.InsertaTablaXML(strTabla, _
                                                     strXML, _
                                                     strCodigoSeguimiento, _
                                                     intFirmaDB, _
                                                     intEsquema)




            Catch ex As Exception
                bolResultado = Nothing
                Throw New Exception(ex.ToString)
            Finally
                strTabla = Nothing
                strXML = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return bolResultado

        End Function
        Public Function EliminarTourCodeEvaluacion(ByVal strCodigoPNR As String, _
                                                   ByVal intCliente As Integer, _
                                                   ByVal strCodigoSeguimiento As String, _
                                                   ByVal intFirmaDB As Integer, _
                                                   ByVal intEsquema As Integer) As Boolean

            Dim objDAO As DAO = Nothing
            Dim objResultado As Boolean = False
            Try

                objDAO = New DAO
                objResultado = objDAO.EliminarTourCodeEvaluacion(strCodigoPNR, _
                                                                   intCliente, _
                                                                   strCodigoSeguimiento, _
                                                                   intFirmaDB, _
                                                                   intEsquema)




            Catch ex As Exception
                objResultado = Nothing
                Throw New Exception(ex.ToString)
            Finally
                strCodigoPNR = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return objResultado

        End Function
        Public Function ObtenerConceptos(ByVal intIdEmpresa As Integer, _
                                         ByVal strTransportador As String, _
                                         ByVal strIataEmisora As String, _
                                         ByVal strCodCiudad As String, _
                                         ByVal strCodigoPNR As String, _
                                         ByVal intGrupo As Integer, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As List(Of classDato)


            Dim objDAO As DAO = Nothing
            Dim objResultado As List(Of classDato) = Nothing

            Try
                objDAO = New DAO
                If objDAO.ObtieneConceptosTourCodeAutomatico(intIdEmpresa, _
                                                               strTransportador, _
                                                               strIataEmisora, _
                                                               strCodCiudad, _
                                                               strCodigoPNR, _
                                                               intGrupo, _
                                                               strCodigoSeguimiento, _
                                                               intFirmaDB, _
                                                               intEsquema) Then



                    objResultado = objDAO.ObtenerConceptos(strTransportador, _
                                                             strCodigoPNR, _
                                                             strCodigoSeguimiento, _
                                                             intFirmaDB, _
                                                             intEsquema)

                End If


            Catch ex As Exception
                objResultado = Nothing
                Throw New Exception(ex.ToString)
            Finally
                strTransportador = Nothing
                strCodigoPNR = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return objResultado

        End Function
        Public Function ObtieneResultadosTourCodeAutomatico(ByVal intIdEmpresa As Integer, _
                                                            ByVal strCodigoPNR As String, _
                                                            ByVal strTipoPasajero As String, _
                                                            ByVal intGrupo As Integer, _
                                                            ByVal strCodigoSeguimiento As String, _
                                                            ByVal intFirmaDB As Integer, _
                                                            ByVal intEsquema As Integer) As classTourCodeResultado

            Dim objDAO As DAO = Nothing
            Dim objResultado As classTourCodeResultado = Nothing
            Try

                objDAO = New DAO
                objResultado = objDAO.ObtieneResultadosTourCodeAutomatico(intIdEmpresa, _
                                                                             strCodigoPNR, _
                                                                             strTipoPasajero, _
                                                                             intGrupo, _
                                                                             strCodigoSeguimiento, _
                                                                             intFirmaDB, _
                                                                             intEsquema)




            Catch ex As Exception
                objResultado = Nothing
                Throw New Exception(ex.ToString)
            Finally
                intIdEmpresa = Nothing
                strCodigoPNR = Nothing
                strTipoPasajero = Nothing
                intGrupo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing

                objDAO = Nothing
            End Try

            Return objResultado

        End Function
        Public Function DevuelveConcepto(ByVal lstConceptosEvaluar As List(Of classPseudoBulkConceptos), _
                                         ByVal objConceptosTourCode As classConceptosEvaluacion, _
                                         ByVal intIndexTarifaPax As Integer, _
                                         ByVal intOpcionIata As Integer, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As List(Of classTourCodesConceptos)

            Dim lstTourCodesConceptos As List(Of classTourCodesConceptos) = Nothing

            Try

                If Not lstConceptosEvaluar Is Nothing Then
                    If lstConceptosEvaluar.Count > 0 Then

                        lstTourCodesConceptos = ObtenerTourCodesConceptos(strCodigoSeguimiento, intFirmaDB, intEsquema)

                        For j As Integer = 0 To lstConceptosEvaluar.Count - 1
                            'JMATTO
                            Dim k As Integer = lstTourCodesConceptos.FindIndex(Function(invoice)
                                                                                   Return (invoice.CodigoConcepto = lstConceptosEvaluar.Item(j).CodigoConcepto)
                                                                               End Function)
                            'For k As Integer = 0 To lstTourCodesConceptos.Count - 1

                            'If lstConceptosEvaluar.Item(j).CodigoConcepto = lstTourCodesConceptos.Item(k).CodigoConcepto Then
                            If Not (k = -1) Then
                                    Select Case lstTourCodesConceptos.Item(k).CodigoConcepto

                                        Case 1 ' 1 = FAREBASIS
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).FAREBASIS), "", Constantes.Apostrofe & objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).FAREBASIS.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                                        Case 2 '2/CLASRESERV 
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.CLASRESERV), "", Constantes.Apostrofe & objConceptosTourCode.CLASRESERV.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                                        Case 3 '3/SUCURSAL 
                                            lstTourCodesConceptos.Item(k).Valor = IIf(objConceptosTourCode.SUCURSAL Is Nothing, "", objConceptosTourCode.SUCURSAL)

                                        Case 4 '4/TIPOSTOCK
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.TIPOSTOCK), "", Constantes.Apostrofe & objConceptosTourCode.TIPOSTOCK & Constantes.Apostrofe)

                                        Case 5 '5/1raLETFARE-SAL 
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).PrimerLETFARESAL), "", Constantes.Apostrofe & objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).PrimerLETFARESAL.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                                        Case 6 '6/CIUORIGEN
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUORIGEN), "", Constantes.Apostrofe & objConceptosTourCode.CIUORIGEN & Constantes.Apostrofe)

                                        Case 7 '7/CIUREGRESO
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUREGRESO), "", Constantes.Apostrofe & objConceptosTourCode.CIUREGRESO & Constantes.Apostrofe)

                                        Case 8 '8/CIUDESTINO
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUDESTINO), "", Constantes.Apostrofe & objConceptosTourCode.CIUDESTINO & Constantes.Apostrofe)

                                        Case 9 '9/PAISDESTINO
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.PAISDESTINO), "", Constantes.Apostrofe & objConceptosTourCode.PAISDESTINO & Constantes.Apostrofe)

                                        Case 10 '10/FECRETORNO DD-MM-AAAA
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.FECRETORNO), "", Constantes.Apostrofe & objConceptosTourCode.FECRETORNO & Constantes.Apostrofe)

                                        Case 11 '11/1raLETFARE-RET 
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.ConceptosTarifa(intIndexTarifaPax).PrimeraLETFARERET), "", Constantes.Apostrofe & objConceptosTourCode.ConceptosTarifa(intIndexTarifaPax).PrimeraLETFARERET.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                                        Case 12 '12/TIPOPAX (A,C,I)
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.ConceptosTarifa(intIndexTarifaPax).TIPOPAX), "", Constantes.Apostrofe & objConceptosTourCode.ConceptosTarifa(intIndexTarifaPax).TIPOPAX.Substring(0, 1) & Constantes.Apostrofe)

                                        Case 13 '13/PAISRETORNO
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.PAISRETORNO), "", Constantes.Apostrofe & objConceptosTourCode.PAISRETORNO & Constantes.Apostrofe)

                                        Case 14 '14/LIBRE 
                                            lstTourCodesConceptos.Item(k).Valor = IIf(objConceptosTourCode.LIBRE1 Is Nothing, "", objConceptosTourCode.LIBRE1)

                                        Case 15 '15/LIBRE 
                                            lstTourCodesConceptos.Item(k).Valor = IIf(objConceptosTourCode.LIBRE2 Is Nothing, "", objConceptosTourCode.LIBRE2)

                                        Case 16 '16/CODSHARE
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.CODSHARE), "", Constantes.Apostrofe & objConceptosTourCode.CODSHARE & Constantes.Apostrofe)

                                        Case 17 '17/TIPORUTA (I,C)
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.TIPORUTA), "", Constantes.Apostrofe & objConceptosTourCode.TIPORUTA & Constantes.Apostrofe)

                                        Case 18 '18/UNIREGULA
                                            lstTourCodesConceptos.Item(k).Valor = IIf(objConceptosTourCode.UNIREGULA Is Nothing, "", objConceptosTourCode.UNIREGULA)

                                        Case 19 '19/TIPOVUELO(ON-OFF)
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.TIPOVUELO), "", Constantes.Apostrofe & objConceptosTourCode.TIPOVUELO & Constantes.Apostrofe)

                                        Case 20 '20/FECSALIDA (DD-MM-AAAA)
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.FECSALIDA), "", Constantes.Apostrofe & objConceptosTourCode.FECSALIDA & Constantes.Apostrofe)

                                        Case 21 '21/PAISORIGEN"
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.PAISORIGEN), "", Constantes.Apostrofe & objConceptosTourCode.PAISORIGEN & Constantes.Apostrofe)

                                        Case 22 '22/LINAEREAAUX 
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.LINAEREAAUX), "", Constantes.Apostrofe & objConceptosTourCode.LINAEREAAUX.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                                        Case 23 '23/CODSHARE-LINAEREA 
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.CODSHARELINAEREA), "", Constantes.Apostrofe & objConceptosTourCode.CODSHARELINAEREA.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                                        Case 24 '24/CLASCABINA 
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.CLASCABINA), "", Constantes.Apostrofe & objConceptosTourCode.CLASCABINA.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                                        Case 25 '25/CANTFARE 
                                            lstTourCodesConceptos.Item(k).Valor = IIf(objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).CANTFARE Is Nothing, "", objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).CANTFARE)

                                        Case 26 '26/CLASCAB-SAL 
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.CLASCABSAL), "", Constantes.Apostrofe & objConceptosTourCode.CLASCABSAL.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                                        Case 27 '27/CLASCAB-RET 
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.CLASCABRET), "", Constantes.Apostrofe & objConceptosTourCode.CLASCABRET.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                                        Case 28 '28/1raLETFARE-ALL 
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).PrimeraLETFAREALL), "", Constantes.Apostrofe & objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).PrimeraLETFAREALL.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                                        Case 29 '29/PAIS-FINVIAJE
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.PAISFINVIAJE), "", Constantes.Apostrofe & objConceptosTourCode.PAISFINVIAJE & Constantes.Apostrofe)

                                        Case 30 '30/CIU-FINVIAJE
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUFINVIAJE), "", Constantes.Apostrofe & objConceptosTourCode.CIUFINVIAJE & Constantes.Apostrofe)

                                        Case 31 '31/REG-DES
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.REGDES), "", Constantes.Apostrofe & objConceptosTourCode.REGDES & Constantes.Apostrofe)

                                        Case 32 '32/REG-RET
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.REGRET), "", Constantes.Apostrofe & objConceptosTourCode.REGRET & Constantes.Apostrofe)

                                        Case 33 '33/CIUAUX-SAL
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUAUXSAL), "", Constantes.Apostrofe & objConceptosTourCode.CIUAUXSAL & Constantes.Apostrofe)

                                        Case 34 '34/CIUAUX-RET
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUAUXRET), "", Constantes.Apostrofe & objConceptosTourCode.CIUAUXRET & Constantes.Apostrofe)

                                        Case 35 '35/TIPOVIAJE
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.TIPOVIAJE), "", Constantes.Apostrofe & objConceptosTourCode.TIPOVIAJE & Constantes.Apostrofe)

                                        Case 36 '36/FORMA-PAGO
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.FORMAPAGO), "", Constantes.Apostrofe & objConceptosTourCode.FORMAPAGO & Constantes.Apostrofe)

                                        Case 37 '37/PAX-CLERO
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.PAXCLERO), "", Constantes.Apostrofe & objConceptosTourCode.PAXCLERO & Constantes.Apostrofe)

                                        Case 38 '38/TIPO-PAXESPECIAL
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).TIPOPAXESPECIAL), "", Constantes.Apostrofe & objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).TIPOPAXESPECIAL & Constantes.Apostrofe)

                                        Case 39 '39/ES-REEMISION
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.ESREEMISION), "", Constantes.Apostrofe & objConceptosTourCode.ESREEMISION & Constantes.Apostrofe)

                                        Case 40 '40/NO-VUELO
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.NOVUELO), "", Constantes.Apostrofe & objConceptosTourCode.NOVUELO.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                                        Case 41 '41/PSEUDO
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.PSEUDO), "", Constantes.Apostrofe & objConceptosTourCode.PSEUDO & Constantes.Apostrofe)

                                        Case 42 '42/IATA
                                            If intOpcionIata = 0 Then
                                                lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.IATACONSULTA), "", Constantes.Apostrofe & objConceptosTourCode.IATACONSULTA & Constantes.Apostrofe)
                                            Else
                                                lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.IATAEMISION), "", Constantes.Apostrofe & objConceptosTourCode.IATAEMISION & Constantes.Apostrofe)
                                            End If


                                        Case 43 '43/CLIENTE
                                            lstTourCodesConceptos.Item(k).Valor = IIf(objConceptosTourCode.CLIENTE Is Nothing, "", objConceptosTourCode.CLIENTE)

                                        Case 44 '-------
                                            lstTourCodesConceptos.Item(k).Valor = ""

                                        Case 45 '45/NoVUELO-SALIDA
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.NVUELOSALIDA), "", Constantes.Apostrofe & objConceptosTourCode.NVUELOSALIDA.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                                        Case 46 '46/NoVUELO-RETORNO
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.NVUELORETORNO), "", Constantes.Apostrofe & objConceptosTourCode.NVUELORETORNO.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                                        Case 47 '-----
                                            lstTourCodesConceptos.Item(k).Valor = ""

                                        Case 48 '-----
                                            lstTourCodesConceptos.Item(k).Valor = ""

                                        Case 49 '-----
                                            lstTourCodesConceptos.Item(k).Valor = ""

                                        Case 50 '50/FB_SIN_TKT_DESIG
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).FBSINTKTDESIG), "", Constantes.Apostrofe & objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).FBSINTKTDESIG.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                                        Case 51 '-----
                                            lstTourCodesConceptos.Item(k).Valor = ""

                                        Case 52 '52/CIUDAD_CONEX_SALIDA
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUDADCONEXDESTINO), "", Constantes.Apostrofe & objConceptosTourCode.CIUDADCONEXDESTINO.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                                        Case 53 '53/CIUDAD_CONEX_RETORNO
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUDADCONEXRETORNO), "", Constantes.Apostrofe & objConceptosTourCode.CIUDADCONEXRETORNO.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                                        Case 54 '54/CIUDAD_CONEX_APLICA
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUDADCONEXAPLICA), "", Constantes.Apostrofe & objConceptosTourCode.CIUDADCONEXAPLICA.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                                        Case 55 ' 55 = TARIFAS CORPORATIVAS
                                            lstTourCodesConceptos.Item(k).Valor = IIf(String.IsNullOrEmpty(objConceptosTourCode.TARIFASCORPORATIVAS), "", Constantes.Apostrofe & objConceptosTourCode.TARIFASCORPORATIVAS & Constantes.Apostrofe)

                                    End Select
                                End If
                            ' End If
                            ' Next
                        Next

                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                lstConceptosEvaluar = Nothing
                objConceptosTourCode = Nothing
                intIndexTarifaPax = Nothing
                intOpcionIata = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return lstTourCodesConceptos

        End Function
        Public Function DevuelveConcepto(ByVal objConceptosTourCode As classConceptosEvaluacion, _
                                         ByVal intIndexTarifaPax As Integer, _
                                         ByVal intOpcionIata As Integer) As String(,)

            Dim yConceptosTarifa(,) As String = Nothing
            Dim oTablaTemporal As New classConceptosEvaluacion

            Try


                yConceptosTarifa = oTablaTemporal.MatrizConceptosModuloTourCode()


                '1/FAREBASIS
                yConceptosTarifa(1, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).FAREBASIS), "", Constantes.Apostrofe & objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).FAREBASIS.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                '2/CLASRESERV 
                yConceptosTarifa(2, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.CLASRESERV), "", Constantes.Apostrofe & objConceptosTourCode.CLASRESERV.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                '3/SUCURSAL 
                yConceptosTarifa(3, 1) = IIf(objConceptosTourCode.SUCURSAL Is Nothing, "", objConceptosTourCode.SUCURSAL)

                '4/TIPOSTOCK
                yConceptosTarifa(4, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.TIPOSTOCK), "", Constantes.Apostrofe & objConceptosTourCode.TIPOSTOCK & Constantes.Apostrofe)

                '5/1raLETFARE-SAL 
                yConceptosTarifa(5, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).PrimerLETFARESAL), "", Constantes.Apostrofe & objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).PrimerLETFARESAL.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                '6/CIUORIGEN
                yConceptosTarifa(6, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUORIGEN), "", Constantes.Apostrofe & objConceptosTourCode.CIUORIGEN & Constantes.Apostrofe)

                '7/CIUREGRESO
                yConceptosTarifa(7, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUREGRESO), "", Constantes.Apostrofe & objConceptosTourCode.CIUREGRESO & Constantes.Apostrofe)

                '8/CIUDESTINO
                yConceptosTarifa(8, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUDESTINO), "", Constantes.Apostrofe & objConceptosTourCode.CIUDESTINO & Constantes.Apostrofe)

                '9/PAISDESTINO
                yConceptosTarifa(9, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.PAISDESTINO), "", Constantes.Apostrofe & objConceptosTourCode.PAISDESTINO & Constantes.Apostrofe)

                '10/FECRETORNO DD-MM-AAAA
                yConceptosTarifa(10, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.FECRETORNO), "", Constantes.Apostrofe & objConceptosTourCode.FECRETORNO & Constantes.Apostrofe)

                '11/1raLETFARE-RET 
                yConceptosTarifa(11, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.ConceptosTarifa(intIndexTarifaPax).PrimeraLETFARERET), "", Constantes.Apostrofe & objConceptosTourCode.ConceptosTarifa(intIndexTarifaPax).PrimeraLETFARERET.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                '12/TIPOPAX (A,C,I)
                yConceptosTarifa(12, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.ConceptosTarifa(intIndexTarifaPax).TIPOPAX), "", Constantes.Apostrofe & objConceptosTourCode.ConceptosTarifa(intIndexTarifaPax).TIPOPAX.Substring(0, 1) & Constantes.Apostrofe)

                '13/PAISRETORNO
                yConceptosTarifa(13, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.PAISRETORNO), "", Constantes.Apostrofe & objConceptosTourCode.PAISRETORNO & Constantes.Apostrofe)

                '14/LIBRE 
                yConceptosTarifa(14, 1) = IIf(objConceptosTourCode.LIBRE1 Is Nothing, "", objConceptosTourCode.LIBRE1)

                '15/LIBRE 
                yConceptosTarifa(15, 1) = IIf(objConceptosTourCode.LIBRE2 Is Nothing, "", objConceptosTourCode.LIBRE2)

                '16/CODSHARE
                yConceptosTarifa(16, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.CODSHARE), "", Constantes.Apostrofe & objConceptosTourCode.CODSHARE & Constantes.Apostrofe)

                '17/TIPORUTA (I,C)
                yConceptosTarifa(17, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.TIPORUTA), "", Constantes.Apostrofe & objConceptosTourCode.TIPORUTA & Constantes.Apostrofe)

                '18/UNIREGULA
                yConceptosTarifa(18, 1) = IIf(objConceptosTourCode.UNIREGULA Is Nothing, "", objConceptosTourCode.UNIREGULA)

                '19/TIPOVUELO(ON-OFF)
                yConceptosTarifa(19, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.TIPOVUELO), "", Constantes.Apostrofe & objConceptosTourCode.TIPOVUELO & Constantes.Apostrofe)

                '20/FECSALIDA (DD-MM-AAAA)
                yConceptosTarifa(20, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.FECSALIDA), "", Constantes.Apostrofe & objConceptosTourCode.FECSALIDA & Constantes.Apostrofe)

                '21/PAISORIGEN"
                yConceptosTarifa(21, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.PAISORIGEN), "", Constantes.Apostrofe & objConceptosTourCode.PAISORIGEN & Constantes.Apostrofe)

                '22/LINAEREAAUX 
                yConceptosTarifa(22, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.LINAEREAAUX), "", Constantes.Apostrofe & objConceptosTourCode.LINAEREAAUX.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                '23/CODSHARE-LINAEREA 
                yConceptosTarifa(23, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.CODSHARELINAEREA), "", Constantes.Apostrofe & objConceptosTourCode.CODSHARELINAEREA.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                '24/CLASCABINA 
                yConceptosTarifa(24, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.CLASCABINA), "", Constantes.Apostrofe & objConceptosTourCode.CLASCABINA.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                '25/CANTFARE 
                yConceptosTarifa(25, 1) = IIf(objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).CANTFARE Is Nothing, "", objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).CANTFARE)

                '26/CLASCAB-SAL 
                yConceptosTarifa(26, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.CLASCABSAL), "", Constantes.Apostrofe & objConceptosTourCode.CLASCABSAL.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                '27/CLASCAB-RET 
                yConceptosTarifa(27, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.CLASCABRET), "", Constantes.Apostrofe & objConceptosTourCode.CLASCABRET.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                '28/1raLETFARE-ALL 
                yConceptosTarifa(28, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).PrimeraLETFAREALL), "", Constantes.Apostrofe & objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).PrimeraLETFAREALL.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                '29/PAIS-FINVIAJE
                yConceptosTarifa(29, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.PAISFINVIAJE), "", Constantes.Apostrofe & objConceptosTourCode.PAISFINVIAJE & Constantes.Apostrofe)

                '30/CIU-FINVIAJE
                yConceptosTarifa(30, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUFINVIAJE), "", Constantes.Apostrofe & objConceptosTourCode.CIUFINVIAJE & Constantes.Apostrofe)

                '31/REG-DES
                yConceptosTarifa(31, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.REGDES), "", Constantes.Apostrofe & objConceptosTourCode.REGDES & Constantes.Apostrofe)

                '32/REG-RET
                yConceptosTarifa(32, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.REGRET), "", Constantes.Apostrofe & objConceptosTourCode.REGRET & Constantes.Apostrofe)

                '33/CIUAUX-SAL
                yConceptosTarifa(33, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUAUXSAL), "", Constantes.Apostrofe & objConceptosTourCode.CIUAUXSAL & Constantes.Apostrofe)

                '34/CIUAUX-RET
                yConceptosTarifa(34, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUAUXRET), "", Constantes.Apostrofe & objConceptosTourCode.CIUAUXRET & Constantes.Apostrofe)

                '35/TIPOVIAJE
                yConceptosTarifa(35, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.TIPOVIAJE), "", Constantes.Apostrofe & objConceptosTourCode.TIPOVIAJE & Constantes.Apostrofe)

                '36/FORMA-PAGO
                yConceptosTarifa(36, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.FORMAPAGO), "", Constantes.Apostrofe & objConceptosTourCode.FORMAPAGO & Constantes.Apostrofe)

                '37/PAX-CLERO
                yConceptosTarifa(37, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.PAXCLERO), "", Constantes.Apostrofe & objConceptosTourCode.PAXCLERO & Constantes.Apostrofe)

                '38/TIPO-PAXESPECIAL
                yConceptosTarifa(38, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).TIPOPAXESPECIAL), "", Constantes.Apostrofe & objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).TIPOPAXESPECIAL & Constantes.Apostrofe)

                '39/ES-REEMISION
                yConceptosTarifa(39, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.ESREEMISION), "", Constantes.Apostrofe & objConceptosTourCode.ESREEMISION & Constantes.Apostrofe)

                '40/NO-VUELO
                yConceptosTarifa(40, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.NOVUELO), "", Constantes.Apostrofe & objConceptosTourCode.NOVUELO.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                '41/PSEUDO
                yConceptosTarifa(41, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.PSEUDO), "", Constantes.Apostrofe & objConceptosTourCode.PSEUDO & Constantes.Apostrofe)

                '42/IATA
                If intOpcionIata = 0 Then
                    yConceptosTarifa(42, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.IATACONSULTA), "", Constantes.Apostrofe & objConceptosTourCode.IATACONSULTA & Constantes.Apostrofe)
                Else
                    yConceptosTarifa(42, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.IATAEMISION), "", Constantes.Apostrofe & objConceptosTourCode.IATAEMISION & Constantes.Apostrofe)
                End If


                '43/CLIENTE
                yConceptosTarifa(43, 1) = IIf(objConceptosTourCode.CLIENTE Is Nothing, "", objConceptosTourCode.CLIENTE)

                '45/NoVUELO-SALIDA
                yConceptosTarifa(45, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.NVUELOSALIDA), "", Constantes.Apostrofe & objConceptosTourCode.NVUELOSALIDA.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                '46/NoVUELO-RETORNO
                yConceptosTarifa(46, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.NVUELORETORNO), "", Constantes.Apostrofe & objConceptosTourCode.NVUELORETORNO.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                '50/FB_SIN_TKT_DESIG
                yConceptosTarifa(50, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).FBSINTKTDESIG), "", Constantes.Apostrofe & objConceptosTourCode.ConceptosTarifa.Item(intIndexTarifaPax).FBSINTKTDESIG.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                '52/CIUDAD_CONEX_SALIDA
                yConceptosTarifa(52, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUDADCONEXDESTINO), "", Constantes.Apostrofe & objConceptosTourCode.CIUDADCONEXDESTINO.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                '53/CIUDAD_CONEX_RETORNO
                yConceptosTarifa(53, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUDADCONEXRETORNO), "", Constantes.Apostrofe & objConceptosTourCode.CIUDADCONEXRETORNO.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                '54/CIUDAD_CONEX_APLICA
                yConceptosTarifa(54, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.CIUDADCONEXAPLICA), "", Constantes.Apostrofe & objConceptosTourCode.CIUDADCONEXAPLICA.Replace(Constantes.Coma, Constantes.ApostrofeComaApostrofe) & Constantes.Apostrofe)

                ' 55 = TARIFAS CORPORATIVAS
                yConceptosTarifa(55, 1) = IIf(String.IsNullOrEmpty(objConceptosTourCode.TARIFASCORPORATIVAS), "", Constantes.Apostrofe & objConceptosTourCode.TARIFASCORPORATIVAS & Constantes.Apostrofe)


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objConceptosTourCode = Nothing
            End Try

            Return yConceptosTarifa

        End Function
    End Class
End Namespace