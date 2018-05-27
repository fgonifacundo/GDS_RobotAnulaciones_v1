Imports GDS_NuevoMundoPersistencia
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports objSabreWS = GDS_NM_WebServicesSabre
Imports EscribeLog = GDS_MuevoMundoLog.EscribeLog
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Private Function CompletarCeros(ByVal strValor As String) As String

            Dim strRespuesta As String = String.Empty

            Try

                If Not String.IsNullOrEmpty(strValor) Then
                    If strValor.Length < 6 Then
                        strRespuesta = strValor
                        For i As Integer = 0 To (5 - strValor.Length)
                            strRespuesta = "0" & strRespuesta
                        Next
                    Else
                        strRespuesta = strValor
                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strValor = Nothing
            End Try

            Return strRespuesta

        End Function
        Private Function CompletarEspacios(ByVal intCantidad As Integer, _
                                           ByVal strValor As String, _
                                           ByVal strDI As String) As String

            Dim strRespuesta As String = String.Empty

            Try

                If Not String.IsNullOrEmpty(strValor) Then
                    If strValor.Length = intCantidad Then
                        strRespuesta = strValor
                    ElseIf strValor.Length < intCantidad Then
                        strRespuesta = strValor
                        For i As Integer = 1 To (intCantidad - strValor.Length)
                            If strDI.Equals("D") Then
                                strRespuesta = strRespuesta & Constantes.Espacio
                            Else
                                strRespuesta = Constantes.Espacio & strRespuesta
                            End If
                        Next
                    Else
                        strRespuesta = strValor.Substring(0, intCantidad)
                    End If
                Else
                    For i As Integer = 1 To intCantidad
                        strRespuesta &= Constantes.Espacio
                    Next
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strValor = Nothing
            End Try

            Return strRespuesta

        End Function
        Private Function TraducirMes(ByVal strFecha As String) As String
            Try

                If strFecha.Contains("ENE") Then
                    strFecha = strFecha.Replace("ENE", "JAN")
                ElseIf strFecha.Contains("ABR") Then
                    strFecha = strFecha.Replace("ABR", "APR")
                ElseIf strFecha.Contains("AGO") Then
                    strFecha = strFecha.Replace("AGO", "AUG")
                ElseIf strFecha.Contains("DIC") Then
                    strFecha = strFecha.Replace("DIC", "DEC")
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally

            End Try

            Return strFecha

        End Function
    End Class
End Namespace
