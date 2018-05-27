Imports GDS_NuevoMundoPersistencia
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Namespace ObjetoAccesoDatos
    Partial Public Class classDAO
#Region "BD_WEB"
        Public Function ObtieneTokenJava(ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As classSession

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objRespuesta As classSession = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spSP_OBTIENE_TOKEN_JAVA, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)

                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objRespuesta = New classSession
                    objRespuesta.Token = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TOKEN", Nothing)
                    objRespuesta.ConversationID = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CONVERSATIONID", Nothing)
                End While

            Catch ex As Exception
                objRespuesta = Nothing
                strLog = "Stored Procedure : " & Constantes.spSP_OBTIENE_TOKEN_JAVA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtieneTokenJava" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtieneTokenJava", strCodigoSeguimiento)

                Err.Raise(13, "ObtieneTokenJava", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objRespuesta

        End Function

        Public Function ObtenerControlMorosidad(ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As List(Of classControlMorosidad)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim lstControlMorosidad As List(Of classControlMorosidad) = Nothing
            Dim objControlMorosidad As classControlMorosidad = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spCONTROL_MOROSIDAD, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)

                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objControlMorosidad = New classControlMorosidad
                    objControlMorosidad.NumeroBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_BOLETO", Nothing)
                    objControlMorosidad.IdCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CLIENTE", 0)
                    objControlMorosidad.InicialesCounter = ConnNM.LeeColumnasDataReader(objOracleDataReader, "INICIALES_COUNTER", Nothing)
                    If lstControlMorosidad Is Nothing Then lstControlMorosidad = New List(Of classControlMorosidad)
                    lstControlMorosidad.Add(objControlMorosidad)
                End While

            Catch ex As Exception
                objControlMorosidad = Nothing
                strLog = "Stored Procedure : " & Constantes.spCONTROL_MOROSIDAD & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerControlMorosidad" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.Message & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerControlMorosidad", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerControlMorosidad", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return lstControlMorosidad

        End Function
        Public Function EliminarCuentaMorosa(ByVal strNumeroBoleto As String, _
                                             ByVal strIdCliente As String, _
                                             ByVal strInicialesCounter As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As String

            Dim ConnNM As New MyConnectionOracle
            Dim strMensaje As String = Nothing
            Dim bolMensaje As Boolean = False

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spELIMINAR_CUENTA_MOROSA, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("p_Num_Bol", strNumeroBoleto, OracleDbType.Varchar2, strNumeroBoleto.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_id_Cliente", strIdCliente, OracleDbType.Varchar2, strIdCliente.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_inicial_Counter", strInicialesCounter, OracleDbType.Varchar2, strInicialesCounter.Length, ParameterDirection.Input)

                bolMensaje = ConnNM._DeleteExecuteNonQuery()

                If bolMensaje Then
                    strMensaje = "1"
                Else
                    strMensaje = "0"
                End If

            Catch ex As Exception
                strMensaje = Nothing
                bolMensaje = False
                strLog = "Stored Procedure : " & Constantes.spELIMINAR_CUENTA_MOROSA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "EliminarCuentaMorosa" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "EliminarCuentaMorosa", strCodigoSeguimiento)

                Err.Raise(13, "EliminarCuentaMorosa", ex.ToString)

            Finally
                ConnNM.Disconnect()
                ' objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try


            Return strMensaje
        End Function

        Public Function ObtenerCorreoEjecutivoCobranza(ByVal strIdCliente As String, _
                                                       ByVal strCodigoSeguimiento As String, _
                                                       ByVal intFirmaDB As Integer, _
                                                       ByVal intEsquema As Integer) As classDato

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objDato As classDato = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGET_MAIL_EJE_COBRANZA, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("p_Id_Cliente", strIdCliente, OracleDbType.Varchar2, strIdCliente.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)

                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objDato = New classDato
                    objDato.sCodigo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ACOUNT_MANAGER", Nothing)
                    objDato.Valor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CORREO_EJECUTIVO_COBRANZA", Nothing)
                End While

            Catch ex As Exception
                objDato = Nothing
                strLog = "Stored Procedure : " & Constantes.spGET_MAIL_EJE_COBRANZA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerCorreoEjecutivoCobranza" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerCorreoEjecutivoCobranza", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerCorreoEjecutivoCobranza", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objDato

        End Function
#End Region
    End Class
End Namespace