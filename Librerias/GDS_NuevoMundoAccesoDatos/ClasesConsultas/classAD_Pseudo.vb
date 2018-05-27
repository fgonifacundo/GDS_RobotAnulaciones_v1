Imports GDS_NuevoMundoPersistencia
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Namespace ObjetoAccesoDatos
    Partial Public Class classDAO
        Public Function ObtenerDatosPseudo(ByVal strCodigoSeguimiento As String, _
                                           ByVal intGDS As Integer, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As List(Of classDato)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objRespuesta As List(Of classDato) = Nothing
            Dim auxRespuesta As classDato = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spPSEUDOS, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_gds", CStr(intGDS).ToString, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxRespuesta = New classDato
                    auxRespuesta.sCodigo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PSEUDO", Nothing)
                    auxRespuesta.Valor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                    If objRespuesta Is Nothing Then objRespuesta = New List(Of classDato)
                    objRespuesta.Add(auxRespuesta)
                End While

            Catch ex As Exception
                objRespuesta = Nothing
                strLog = "Stored Procedure : " & Constantes.spPSEUDOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDatosPseudo" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerDatosPseudo", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerDatosPseudo", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                auxRespuesta = Nothing
            End Try

            Return objRespuesta

        End Function
        Public Function ObtenerExistePseudoNM(ByVal srtPseudo As String, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As Integer

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim intCantidad As Integer = 0

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command("SELECT COUNT(*) AS CANTIDAD FROM NUEVOMUNDO.PSEUDOS_GDS PGDS WHERE PGDS.ID_PSEUDO = '" & srtPseudo & "'", Constantes.SentenciaText)

                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    intCantidad = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CANTIDAD", 0)
                End While

            Catch ex As Exception
                strLog = "Query: " & "ObtenerExistePseudoNM" & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerExistePseudoNM" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerExistePseudoNM", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerExistePseudoNM", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing

                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return intCantidad

        End Function

        Public Function ObtenerExistePseudoNMundo(ByVal srtPseudo As String, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As Integer

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objRespuesta As List(Of classDato) = Nothing
            Dim auxRespuesta As classDato = Nothing
            Dim intCantidad As Integer = 0
            Try
                Dim strCommand = Constantes.getEsquema(intEsquema) + Constantes.spGDS_EXISTE_PSEUDO
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(strCommand, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_id_pseudo", srtPseudo, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)
                objOracleDataReader = ConnNM._ExecuteReader()
                While objOracleDataReader.Read
                    auxRespuesta = New classDato
                    auxRespuesta.sCodigo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PSEUDO", Nothing)
                    If objRespuesta Is Nothing Then objRespuesta = New List(Of classDato)
                    objRespuesta.Add(auxRespuesta)
                End While

                If objRespuesta IsNot Nothing Then
                    If objRespuesta.Count > 0 Then
                        intCantidad = objRespuesta.Count
                    End If
                End If
            Catch ex As Exception
                strLog = "Query: " & "ObtenerExistePseudoNMundo" & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerExistePseudoNMundo" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerExistePseudoNMundo", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerExistePseudoNMundo", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing

                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return intCantidad

        End Function

    End Class

End Namespace