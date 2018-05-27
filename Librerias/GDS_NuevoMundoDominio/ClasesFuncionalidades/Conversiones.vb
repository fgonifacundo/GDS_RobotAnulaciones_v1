Public Class Conversiones
    Public Function FormatearNumero(ByVal Valor As String, ByVal CantidadDecimales As Integer, Optional ByVal SeparadorMiles As Boolean = False) As String
        Dim retorno As String = ""                                            ' Valor a devolver
        Try
            If IsNumeric(Valor) = True Then                                     ' Valor es numerico?
                Dim formato As String = "###,###,##0."                            ' Formato deseado
                If SeparadorMiles = False Then formato = formato.Replace(",", "") ' Reemplazar las (,) 
                For i As Integer = 0 To CantidadDecimales - 1                     ' Por la cantidad deseada
                    formato &= "0"                                                  ' Añadir los valores
                Next
                retorno = Format(Convert.ToDouble(Valor), formato).ToString()     ' Convertir a cadena el retorno
            Else
                Throw New Exception("Parametro no era numérico")
            End If
        Catch ex As Exception
            retorno = ""
            Throw New Exception(ex.ToString)
        End Try
        Return retorno
    End Function
End Class
