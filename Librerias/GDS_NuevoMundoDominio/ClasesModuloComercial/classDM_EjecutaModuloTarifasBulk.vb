Imports Persistencia = GDS_NuevoMundoPersistencia
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes

Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        'NEGOCIADAS RICHARD
        Public Function ModuloTarifaBulk(ByVal intIdEmpresa As Integer, _
                                         ByVal strCodigoPNR As String, _
                                         ByVal strPseudoConsulta As String, _
                                         ByVal intGrupo As Integer, _
                                         ByVal objConceptosTourCode As Persistencia.classConceptosEvaluacion, _
                                         ByVal intOpcionIata As Integer, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As List(Of Persistencia.classFeeTarifaBulkResultado)

            ' intOpcionIata = 0 => IATACONSULTA

            Dim objConceptosEvaluar As List(Of Persistencia.classDato) = Nothing
            Dim objConceptosDevueltos(,) As String = Nothing
            Dim objResultadosTarifaBulk() As Persistencia.classFeeTarifaBulkResultado = Nothing
            Dim objModuloTarifaBulk As List(Of Persistencia.classFeeTarifaBulkResultado) = Nothing

            Dim strCodigoConcepto As String = Nothing
            Dim strNombreConcepto As String = Nothing
            Dim strValorConcepto As String = Nothing
            Dim strCadenaXML As System.Text.StringBuilder = Nothing

            Try

                If Not objConceptosTourCode Is Nothing Then



                    If ExisteReglas(intGrupo, _
                                       objConceptosTourCode.LINEAVALIDADORA, _
                                       strCodigoSeguimiento, _
                                       intFirmaDB, _
                                       intEsquema) = 1 Then

                        objConceptosEvaluar = ObtenerConceptosTarifasBulk(intIdEmpresa, _
                                                                          objConceptosTourCode.CIUDESTINO, _
                                                                          objConceptosTourCode.LINEAVALIDADORA, _
                                                                          strCodigoPNR, _
                                                                          intGrupo, _
                                                                          strCodigoSeguimiento, _
                                                                          intFirmaDB, _
                                                                          intEsquema)

                        If Not objConceptosEvaluar Is Nothing Then
                            If Not objConceptosTourCode.ConceptosTarifa Is Nothing Then
                                For i As Integer = 0 To objConceptosTourCode.ConceptosTarifa.Count - 1

                                    objConceptosDevueltos = DevuelveConcepto(objConceptosTourCode, _
                                                                                i, _
                                                                                intOpcionIata)

                                    strCadenaXML = New System.Text.StringBuilder

                                    strCadenaXML.Append("<?xml version='1.0'?>")
                                    strCadenaXML.Append("<ROWSET>")
                                    For x As Integer = 0 To objConceptosEvaluar.Count - 1
                                        strCodigoConcepto = objConceptosEvaluar.Item(x).Codigo
                                        strNombreConcepto = objConceptosEvaluar.Item(x).Valor
                                        strValorConcepto = objConceptosDevueltos(objConceptosEvaluar.Item(x).Codigo, 1)

                                        strCadenaXML.Append("<ROW num='" & x + 1 & "'>")
                                        strCadenaXML.Append("<ID_TRANSPORTADOR>" & objConceptosTourCode.LINEAVALIDADORA & "</ID_TRANSPORTADOR>")
                                        If intOpcionIata = 0 Then
                                            strCadenaXML.Append("<ID_IATA>" & objConceptosTourCode.IATACONSULTA & "</ID_IATA>")
                                        Else
                                            strCadenaXML.Append("<ID_IATA>" & objConceptosTourCode.IATAEMISION & "</ID_IATA>")
                                        End If
                                        strCadenaXML.Append("<CODIGO_RESERVA>" & strCodigoPNR & "</CODIGO_RESERVA>")
                                        strCadenaXML.Append("<CORRELATIVO_EVALUACION>" & x + 1 & "</CORRELATIVO_EVALUACION>")
                                        strCadenaXML.Append("<CODIGO_CONCEPTO>" & strCodigoConcepto & "</CODIGO_CONCEPTO>")
                                        strCadenaXML.Append("<VALOR>" & strValorConcepto & "</VALOR>")
                                        strCadenaXML.Append("<ID_CLIENTE>" & objConceptosTourCode.CLIENTE & "</ID_CLIENTE>")
                                        strCadenaXML.Append("<FECHA_ALTA>" & Now.ToString(Constantes.IWS_DATE_FORMAT_INSERT) & "</FECHA_ALTA>")
                                        strCadenaXML.Append("</ROW>")
                                    Next
                                    strCadenaXML.Append("</ROWSET>")

                                    BorrarTarifaBulkEvaluacion(strCodigoPNR, objConceptosTourCode.CLIENTE, strCodigoSeguimiento, intFirmaDB, intEsquema)


                                    If InsertaConceptos("TARIFABULK_EVALUACION", _
                                                           strCadenaXML.ToString, _
                                                           strCodigoSeguimiento, _
                                                           intFirmaDB, _
                                                           intEsquema) Then


                                        objResultadosTarifaBulk = ObtieneResultadosTarifaBulkFee(strCodigoPNR, _
                                                                                                 strPseudoConsulta, _
                                                                                                                 "", _
                                                                                                                 "", _
                                                                                                                 strCodigoSeguimiento, _
                                                                                                                 intFirmaDB, _
                                                                                                                 intEsquema)

                                        If Not objResultadosTarifaBulk Is Nothing Then
                                            If objModuloTarifaBulk Is Nothing Then objModuloTarifaBulk = New List(Of Persistencia.classFeeTarifaBulkResultado)
                                            For x As Integer = 0 To objResultadosTarifaBulk.Length - 1
                                                objModuloTarifaBulk.Add(objResultadosTarifaBulk(x))
                                            Next
                                        End If

                                        BorrarTarifaBulkEvaluacion(strCodigoPNR, objConceptosTourCode.CLIENTE, strCodigoSeguimiento, intFirmaDB, intEsquema)

                                    End If

                                Next
                            End If
                        End If

                    End If

                End If
            Catch ex As Exception
                BorrarTarifaBulkEvaluacion(strCodigoPNR, objConceptosTourCode.CLIENTE, strCodigoSeguimiento, intFirmaDB, intEsquema)

                Throw New Exception(ex.ToString)
            Finally

            End Try

            Return objModuloTarifaBulk

        End Function
        Public Function ModuloTarifaBulk_HS(ByVal strCodigoPNR As String, _
                                            ByVal strPseudoConsulta As String, _
                                            ByVal objConceptosTourCode As Persistencia.classConceptosEvaluacion, _
                                            ByVal intOpcionIata As Integer, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As Persistencia.classFeeTarifaBulkResultado()

            ' intOpcionIata = 0 => IATACONSULTA
            Dim lstTourCodesConceptos As List(Of Persistencia.classTourCodesConceptos) = Nothing
            Dim lstConceptosEvaluar As List(Of Persistencia.classPseudoBulkConceptos) = Nothing
            Dim objConceptosDevueltos As String(,) = Nothing
            Dim objConceptosEvaluacion As List(Of Persistencia.classTarifasBulkEvaluacion) = Nothing
            Dim objFeeTarifaBulkResultado As Persistencia.classFeeTarifaBulkResultado() = Nothing

            Try

                If Not objConceptosTourCode Is Nothing Then

                    lstConceptosEvaluar = ObtenerConceptosTarifasBulk_HS(objConceptosTourCode.LINEAVALIDADORA, _
                                                                         objConceptosTourCode.CIUDESTINO, _
                                                                         strCodigoPNR, _
                                                                         strCodigoSeguimiento, _
                                                                         intFirmaDB, _
                                                                         intEsquema)

                    If Not lstConceptosEvaluar Is Nothing Then
                        If Not objConceptosTourCode.ConceptosTarifa Is Nothing Then
                            For i As Integer = 0 To objConceptosTourCode.ConceptosTarifa.Count - 1


                                lstTourCodesConceptos = DevuelveConcepto(lstConceptosEvaluar, objConceptosTourCode, i, intOpcionIata, strCodigoSeguimiento, intFirmaDB, intEsquema)

                                'objConceptosEvaluacion = Conceptos(strCodigoPNR, lstConceptosEvaluar, objConceptosDevueltos)


                                objFeeTarifaBulkResultado = InsertarConceptosTarifasBulk(strCodigoPNR, _
                                                                                         strPseudoConsulta, _
                                                                                         objConceptosTourCode.LINEAVALIDADORA, _
                                                                                         objConceptosTourCode.CIUDESTINO, _
                                                                                         lstTourCodesConceptos, _
                                                                                         strCodigoSeguimiento, _
                                                                                         intFirmaDB, _
                                                                                         intEsquema)



                            Next
                        End If
                    End If

                End If

            Catch ex As Exception
                BorrarTarifaBulkEvaluacion(strCodigoPNR, objConceptosTourCode.CLIENTE, strCodigoSeguimiento, intFirmaDB, intEsquema)

                Throw New Exception(ex.ToString)
            Finally

            End Try

            Return objFeeTarifaBulkResultado

        End Function
        

    End Class
End Namespace