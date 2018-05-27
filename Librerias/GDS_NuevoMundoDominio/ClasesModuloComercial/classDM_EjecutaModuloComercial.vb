Imports Persistencia = GDS_NuevoMundoPersistencia
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        'PUBLICADAS
        Public Function ModuloComercial(ByVal intIdEmpresa As Integer, _
                                        ByVal strCodigoPNR As String, _
                                        ByVal intGrupo As Integer, _
                                        ByVal objConceptosTourCode As Persistencia.classConceptosEvaluacion, _
                                        ByVal intOpcionIata As Integer, _
                                        ByVal strCodigoSeguimiento As String, _
                                        ByVal intFirmaDB As Integer, _
                                        ByVal intEsquema As Integer) As List(Of Persistencia.classTourCodeResultado)

            ' intOpcionIata = 0 => IATACONSULTA

            Dim objConceptosEvaluar As List(Of Persistencia.classDato) = Nothing
            Dim objConceptosDevueltos(,) As String = Nothing
            Dim objResultadosTourCodeAutomatico As Persistencia.classTourCodeResultado = Nothing
            Dim objModuloComercial As List(Of Persistencia.classTourCodeResultado) = Nothing

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

                        objConceptosEvaluar = ObtenerConceptos(intIdEmpresa, _
                                                                  objConceptosTourCode.LINEAVALIDADORA, _
                                                                  objConceptosTourCode.IATAEMISION, _
                                                                  objConceptosTourCode.CIUDESTINO, _
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

                                    EliminarTourCodeEvaluacion(strCodigoPNR, _
                                                                  objConceptosTourCode.CLIENTE, _
                                                                  strCodigoSeguimiento, _
                                                                  intFirmaDB, _
                                                                  intEsquema)

                                    If InsertaConceptos("Tourcodes_EVALUACION", _
                                                           strCadenaXML.ToString, _
                                                           strCodigoSeguimiento, _
                                                           intFirmaDB, _
                                                           intEsquema) Then


                                        objResultadosTourCodeAutomatico = ObtieneResultadosTourCodeAutomatico(intIdEmpresa, _
                                                                                                                 strCodigoPNR, _
                                                                                                                 objConceptosTourCode.ConceptosTarifa(i).TIPOPAXESPECIAL, _
                                                                                                                 intGrupo, _
                                                                                                                 strCodigoSeguimiento, _
                                                                                                                 intFirmaDB, _
                                                                                                                 intEsquema)

                                        EliminarTourCodeEvaluacion(strCodigoPNR, _
                                                                      objConceptosTourCode.CLIENTE, _
                                                                      strCodigoSeguimiento, _
                                                                      intFirmaDB, _
                                                                      intEsquema)

                                        If objModuloComercial Is Nothing Then objModuloComercial = New List(Of Persistencia.classTourCodeResultado)
                                        objModuloComercial.Add(objResultadosTourCodeAutomatico)

                                    End If

                                Next
                            End If
                        End If

                    End If

                End If
            Catch ex As Exception
                EliminarTourCodeEvaluacion(strCodigoPNR, _
                                              objConceptosTourCode.CLIENTE, _
                                              strCodigoSeguimiento, _
                                              intFirmaDB, _
                                              intEsquema)

                Throw New Exception(ex.ToString)
            Finally

            End Try

            Return objModuloComercial

        End Function
    End Class
End Namespace