Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Imports GDS_NuevoMundoPersistencia
Namespace ObjetoDominioNegocio


    Partial Public Class classBO
        Public Function ObtieneCodigoOperador(ByVal OperadoPor As String, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As String

            Dim objDAO As DAO = Nothing
            Dim objAerolineaAsociada As classAerolineaAsociada = Nothing


            Try

                If OperadoPor.Length > 2 Then

                    objDAO = New DAO
                    objAerolineaAsociada = objDAO.ObtenerAerolineaAsociada(OperadoPor, strCodigoSeguimiento, intFirmaDB, intEsquema)

                    If Not objAerolineaAsociada Is Nothing Then
                        OperadoPor = Trim(objAerolineaAsociada.ID.ToUpper.ToString)
                    End If

                    If Not String.IsNullOrEmpty(OperadoPor) Then
                        If OperadoPor = "T0" Then OperadoPor = "TA"
                    End If

                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objAerolineaAsociada = Nothing
                OperadoPor = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return OperadoPor

        End Function
        Public Function InsertaTablaXML(ByVal strTabla As String, _
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
    End Class
End Namespace
