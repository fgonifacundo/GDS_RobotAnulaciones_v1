Imports GDS_NuevoMundoPersistencia
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Namespace ObjetoAccesoDatos
    Partial Public Class classDAO
        Public Function ObtieneHorarioConsultaAutomatedExchange(ByVal strTipoCliente As String, _
                                                         ByVal intCodigoPermiso As Integer, _
                                                         ByVal strCodigoSeguimiento As String, _
                                                         ByVal intFirmaDB As Integer, _
                                                         ByVal intEsquema As Integer) As List(Of classDato)


            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim lstRespuesta As List(Of classDato) = Nothing

            Try

                ConnNM.Connect(intFirmaDB)

                If intEsquema = 3 Then
                    ConnNM.SP_Command(Constantes.spGDS_HORARIO_ING_SOL_AEXC, Constantes.StoredProcedure)
                Else
                    ConnNM.SP_Command(Constantes.spDemoGDS_HORARIO_ING_SOL_AEXC, Constantes.StoredProcedure)
                End If

                ConnNM.AgregarParametro("@p_TipoCliente", strTipoCliente, OracleDbType.Varchar2, IIf(String.IsNullOrEmpty(strTipoCliente), 0, strTipoCliente.Length), ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_CodPermiso", intCodigoPermiso, OracleDbType.Int64, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    Dim objRespuesta As classDato = Nothing

                    objRespuesta = New classDato
                    objRespuesta.sCodigo = "EMITE"
                    objRespuesta.Valor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMITE", Nothing)
                    If lstRespuesta Is Nothing Then lstRespuesta = New List(Of classDato)
                    lstRespuesta.Add(objRespuesta)
                    objRespuesta = Nothing

                    objRespuesta = New classDato
                    objRespuesta.sCodigo = "SOLICITUD"
                    objRespuesta.Valor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "SOLICITUD", Nothing)
                    If lstRespuesta Is Nothing Then lstRespuesta = New List(Of classDato)
                    lstRespuesta.Add(objRespuesta)

                    objRespuesta = Nothing
                End While

            Catch ex As Exception

                If intEsquema = 3 Then
                    strLog = "Stored Procedure : " & Constantes.spGDS_HORARIO_ING_SOL_AEXC & vbCrLf
                Else
                    strLog = "Stored Procedure : " & Constantes.spDemoGDS_HORARIO_ING_SOL_AEXC & vbCrLf
                End If

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "HorarioConsultaAutomatedExchange" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "HorarioConsultaAutomatedExchange", strCodigoSeguimiento)

                Err.Raise(8, "HorarioConsultaAutomatedExchange", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strTipoCliente = Nothing
                intCodigoPermiso = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return lstRespuesta

        End Function
    End Class
End Namespace
