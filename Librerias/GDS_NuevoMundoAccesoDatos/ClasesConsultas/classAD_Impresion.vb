Imports GDS_NuevoMundoPersistencia
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Namespace ObjetoAccesoDatos
    Partial Public Class classDAO
        Public Function ActualizaImpresionTurboPassengerReceipt(ByVal strNombreArchivo As String, _
                                                                ByVal strNombreImpresora As String, _
                                                                ByVal strCodigoSeguimiento As String, _
                                                                ByVal intFirmaDB As Integer, _
                                                                ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean
            Dim strMensajeLog As String = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command("ALTER SESSION SET NLS_DATE_FORMAT = 'MM/dd/yyyy'", Constantes.SentenciaText)
                ConnNM._ExecuteNonQuery()


                ConnNM.SP_Command(Constantes.spACTUALIZA_IMPRESION, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_NombreArchivo", strNombreArchivo, OracleDbType.Varchar2, strNombreArchivo.Length, ParameterDirection.Input)
                '----------
                bolResultado = ConnNM._InsertExecuteNonQuery()
                strMensajeLog = "Se actualizó marca para el archivo " & strNombreArchivo
                objEscribeLog.WriteLogImpresion(strMensajeLog, "ActualizaImpresionTurboPassengerReceipt")


            Catch ex As Exception
                bolResultado = False
                strLog = "Stored Procedure : " & Constantes.spACTUALIZA_IMPRESION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ActualizaImpresionTurboPassengerReceipt" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf

                objEscribeLog.WriteLog(strLog, "ActualizaImpresionTurboPassengerReceipt", strCodigoSeguimiento)
                Err.Raise(13, "ActualizaImpresionTurboPassengerReceipt", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strNombreArchivo = Nothing
                strNombreImpresora = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado

        End Function
    End Class
End Namespace