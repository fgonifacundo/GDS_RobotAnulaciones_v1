Imports GDS_NuevoMundoPersistencia
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Namespace ObjetoAccesoDatos
    Public Class classDAO
        Private strLog As String = Nothing
        Private objEscribeLog As New GDS_MuevoMundoLog.EscribeLog
        Public Function ObtenerAerolineaAsociada(ByVal strNombreAerolinea As String, _
                                                 ByVal strCodigoSeguimiento As String, _
                                                 ByVal intFirmaDB As Integer, _
                                                 ByVal intEsquema As Integer) As classAerolineaAsociada

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objRespuesta As classAerolineaAsociada = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spAEROLINEA_ASOCIADA, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Nombre", strNombreAerolinea.ToString, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objRespuesta = New classAerolineaAsociada

                    objRespuesta.ID = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_TRANSPORTADOR", Nothing)
                    objRespuesta.Descripcion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spAEROLINEA_ASOCIADA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Nombre Aerolinea : " & strNombreAerolinea.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "classAerolinea.ObtenerAerolineaAsociada" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerAerolineaAsociada", strCodigoSeguimiento)

                Err.Raise(3, "classAerolinea.ObtenerAerolineaAsociada", ex.Message)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strNombreAerolinea = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objRespuesta

        End Function
        Public Function ObtenerDatosAerolinea(ByVal strCodigoAerolinea As String, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As classDatosAerolinea

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objDatosAerolinea As classDatosAerolinea = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spDATOS_AEROLINEA, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Aerolinea", strCodigoAerolinea, OracleDbType.Varchar2, strCodigoAerolinea.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objDatosAerolinea = New classDatosAerolinea
                    objDatosAerolinea.Nombre = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)
                    objDatosAerolinea.Prefijo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PREFIJO", -1)
                    objDatosAerolinea.IATA = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_IATA_TRANSPORTADOR", -1)
                End While

            Catch ex As Exception
                objDatosAerolinea = Nothing
                strLog = "Stored Procedure : " & Constantes.spDATOS_AEROLINEA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDatosAerolinea" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerDatosAerolinea", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerDatosAerolinea", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoAerolinea = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objDatosAerolinea

        End Function
        Public Function ObtenerNombreEquipo(ByVal strCodigoEquipo As String, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As String

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim strRespuesta As String = Nothing
            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spEQUIPO_AVION, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@P_CODIGO", strCodigoEquipo, OracleDbType.Varchar2, strCodigoEquipo.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    strRespuesta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                End While

            Catch ex As Exception
                strRespuesta = Nothing
                strLog = "Stored Procedure : " & Constantes.spEQUIPO_AVION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerNombreEquipo" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerNombreEquipo", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerNombreEquipo", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoEquipo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return strRespuesta

        End Function
    End Class
End Namespace