Imports GDS_NuevoMundoPersistencia
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports System.Text

Namespace ObjetoAccesoDatos
    Partial Public Class classDAO
        Public Function CambioAerolinea(ByVal strTransportador As String, _
                                        ByVal strCodigoSeguimiento As String, _
                                        ByVal intFirmaDB As Integer, _
                                        ByVal intEsquema As Integer) As String

            Dim ConnNM As New MyConnectionOracle
            Dim strRespuesta As String = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_RMC_CAMBIAR_AEROLINEA, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("p_Transportador", strTransportador, OracleDbType.Varchar2, strTransportador.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("p_Resultado", Nothing, OracleDbType.Varchar2, 20, ParameterDirection.Output)


                ConnNM._ExecuteReader()

                strRespuesta = ConnNM.LeeParametros("p_Resultado", Nothing)


            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_RMC_CAMBIAR_AEROLINEA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "CambioAerolinea" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "CambioAerolinea", strCodigoSeguimiento)

                Err.Raise(14, "CambioAerolinea", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strTransportador = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return strRespuesta

        End Function
        Public Function AerolineaHomologa(ByVal strTransportador As String, _
                                          ByVal strCodigoSeguimiento As String, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal intEsquema As Integer) As String

            Dim ConnNM As New MyConnectionOracle
            Dim strRespuesta As String = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_RMC_HOMOLOGA, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("p_Transportador", strTransportador, OracleDbType.Varchar2, strTransportador.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("p_Resultado", Nothing, OracleDbType.Varchar2, 20, ParameterDirection.Output)


                ConnNM._ExecuteReader()

                strRespuesta = ConnNM.LeeParametros("p_Resultado", Nothing)


            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_RMC_HOMOLOGA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "AerolineaHomologa" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "AerolineaHomologa", strCodigoSeguimiento)

                Err.Raise(14, "AerolineaHomologa", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strTransportador = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return strRespuesta

        End Function
        Public Function AerolineaEquivalente(ByVal strTransportador As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As String

            Dim ConnNM As New MyConnectionOracle
            Dim strRespuesta As String = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_RMC_EQUIVALENTE, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("p_Transportador", strTransportador, OracleDbType.Varchar2, strTransportador.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("p_Resultado", Nothing, OracleDbType.Varchar2, 20, ParameterDirection.Output)


                ConnNM._ExecuteReader()

                strRespuesta = ConnNM.LeeParametros("p_Resultado", Nothing)


            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_RMC_EQUIVALENTE & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "AerolineaEquivalente" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "AerolineaEquivalente", strCodigoSeguimiento)

                Err.Raise(14, "AerolineaEquivalente", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strTransportador = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return strRespuesta

        End Function
        Public Function ExisteReglas(ByVal Id_Grupo As Integer, _
                                     ByVal strTransportador As String, _
                                     ByVal strCodigoSeguimiento As String, _
                                     ByVal intFirmaDB As Integer, _
                                     ByVal intEsquema As Integer) As Integer

            'Id_Grupo = 3 Publicadas
            '           4 Privadas

            Dim ConnNM As New MyConnectionOracle
            Dim intRespuesta As Integer = -1

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_MC_EXISTE_REGLA, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("p_IdGrupo", Id_Grupo, OracleDbType.Int64, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Transportador", strTransportador, OracleDbType.Varchar2, 2, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("p_Resultado", Nothing, OracleDbType.Int64, 0, ParameterDirection.Output)


                ConnNM._ExecuteReader()

                intRespuesta = CInt(ConnNM.LeeParametros("p_Resultado", -1))


            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_MC_EXISTE_REGLA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ExisteReglas" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ExisteReglas", strCodigoSeguimiento)

                Err.Raise(14, "ExisteReglas", ex.ToString)

            Finally
                ConnNM.Disconnect()
                Id_Grupo = Nothing
                strTransportador = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return intRespuesta

        End Function
        Public Function ObtenerIATA(ByVal strPseudo As String, _
                                    ByVal strCodigoSeguimiento As String, _
                                    ByVal intFirmaDB As Integer, _
                                    ByVal intEsquema As Integer) As classIata

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objIata As classIata = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_MC_BUSCA_IATA, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("p_Pseudo", strPseudo, OracleDbType.Varchar2, 4, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objIata = New classIata
                    objIata.Iata = ConnNM.LeeColumnasDataReader(objOracleDataReader, "IATA", Nothing)
                    objIata.Sucursal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "SUCURSAL", Nothing)
                End While


            Catch ex As Exception
                objIata = Nothing
                strLog = "Stored Procedure : " & Constantes.spGDS_MC_BUSCA_IATA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerIATA" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerIATA", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerIATA", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strPseudo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing

                objOracleDataReader = Nothing
            End Try

            Return objIata

        End Function
        Public Function ObtenerConceptos(ByVal strTransportador As String, _
                                         ByVal strCodigoPNR As String, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As List(Of classDato)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objResultado As List(Of classDato) = Nothing
            Dim auxResultado As classDato = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_MC_OBTIENE_CONCEPTOS, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Transportador", strTransportador, OracleDbType.Varchar2, 2, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_CodigoPNR", strCodigoPNR, OracleDbType.Varchar2, 6, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxResultado = New classDato
                    auxResultado.Codigo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CODIGO", Nothing)
                    auxResultado.Valor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                    If objResultado Is Nothing Then objResultado = New List(Of classDato)
                    objResultado.Add(auxResultado)
                End While

            Catch ex As Exception
                objResultado = Nothing
                strLog = "Stored Procedure : " & Constantes.spGDS_MC_OBTIENE_CONCEPTOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerConceptos" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerConceptos", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerConceptos", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                auxResultado = Nothing
            End Try

            Return objResultado

        End Function
        Public Function ObtenerConceptosTarifasBulk(ByVal strTransportador As String, _
                                                    ByVal strCodigoPNR As String, _
                                                    ByVal strCodigoSeguimiento As String, _
                                                    ByVal intFirmaDB As Integer, _
                                                    ByVal intEsquema As Integer) As List(Of classDato)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objResultado As List(Of classDato) = Nothing
            Dim auxResultado As classDato = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_MC_OBTIENE_CONCEPTOS, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Transportador", strTransportador, OracleDbType.Varchar2, 2, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_CodigoPNR", strCodigoPNR, OracleDbType.Varchar2, 6, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxResultado = New classDato
                    auxResultado.Codigo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CODIGO", Nothing)
                    auxResultado.Valor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                    If objResultado Is Nothing Then objResultado = New List(Of classDato)
                    objResultado.Add(auxResultado)
                End While

            Catch ex As Exception
                objResultado = Nothing
                strLog = "Stored Procedure : " & Constantes.spGDS_MC_OBTIENE_CONCEPTOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerConceptosTarifasBulk" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerConceptosTarifasBulk", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerConceptosTarifasBulk", ex.Message)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                auxResultado = Nothing
            End Try

            Return objResultado

        End Function
        Public Function InsertaTablaXML(ByVal strTabla As String, _
                                        ByVal strXML As String, _
                                        ByVal strCodigoSeguimiento As String, _
                                        ByVal intFirmaDB As Integer, _
                                        ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean

            Try

                ConnNM.Connect(intFirmaDB)
                'If iOP = 0 Then
                '    ConnNM.SP_Command("ALTER SESSION SET NLS_DATE_FORMAT = 'MM/dd/yyyy'", Constantes.SentenciaText)
                'Else
                '    ConnNM.SP_Command("ALTER SESSION SET NLS_DATE_FORMAT = 'dd/MM/yyyy'", Constantes.SentenciaText)
                'End If

                'ConnNM._ExecuteNonQuery()


                ConnNM.SP_Command(Constantes.spGDS_MC_INS_CONCEPTOS, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Tabla", strTabla, OracleDbType.Varchar2, strTabla.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Xml", strXML, OracleDbType.Clob, 0, ParameterDirection.Input)
                '----------
                bolResultado = ConnNM._InsertExecuteNonQuery()


            Catch ex As Exception
                bolResultado = Nothing
                strLog = "Stored Procedure : " & Constantes.spGDS_MC_INS_CONCEPTOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "InsertaTablaXML" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "InsertaTablaXML", strCodigoSeguimiento)

                Err.Raise(13, "InsertaTablaXML", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strTabla = Nothing
                strXML = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado

        End Function
        Public Function EliminarTourCodeEvaluacion(ByVal strCodigoPNR As String, _
                                                   ByVal intCliente As Integer, _
                                                   ByVal strCodigoSeguimiento As String, _
                                                   ByVal intFirmaDB As Integer, _
                                                   ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean = False

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_MC_DEL_TOURCODE_EVALU, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_CodigoPNR", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cliente", intCliente, OracleDbType.Int64, 0, ParameterDirection.Input)
                '----------
                bolResultado = ConnNM._DeleteExecuteNonQuery()


            Catch ex As Exception
                bolResultado = Nothing
                strLog = "Stored Procedure : " & Constantes.spGDS_MC_DEL_TOURCODE_EVALU & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de     Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "EliminarTourCodeEvaluacion" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "EliminarTourCodeEvaluacion", strCodigoSeguimiento)

                Err.Raise(13, "EliminarTourCodeEvaluacion", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strCodigoPNR = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado

        End Function
        Public Function ObtieneConceptosTourCodeAutomatico(ByVal intIdEmpresa As Integer, _
                                                           ByVal strTransportador As String, _
                                                           ByVal strIataEmisora As String, _
                                                           ByVal strCodCiudad As String, _
                                                           ByVal strCodigoPNR As String, _
                                                           ByVal intGrupo As Integer, _
                                                           ByVal strCodigoSeguimiento As String, _
                                                           ByVal intFirmaDB As Integer, _
                                                           ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim bolResultado As Boolean = False

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_EMC_UP_CONCEPTOS_TOURCODES, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@empresa", intIdEmpresa, OracleDbType.Double, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@transportador", strTransportador, OracleDbType.Varchar2, strTransportador.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@iata", strIataEmisora, OracleDbType.Varchar2, strIataEmisora.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@ciudad_destino", strCodCiudad, OracleDbType.Varchar2, strCodCiudad.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@codigo_pnr", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@_id_grupo", intGrupo, OracleDbType.Int64, 0, ParameterDirection.Input)
                '----------
                ConnNM._ExecuteReader()
                bolResultado = True

            Catch ex As Exception
                bolResultado = Nothing
                strLog = "Stored Procedure : " & Constantes.spGDS_EMC_UP_CONCEPTOS_TOURCODES & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de     Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtieneConceptosTourCodeAutomatico" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtieneConceptosTourCodeAutomatico", strCodigoSeguimiento)

                Err.Raise(13, "ObtieneConceptosTourCodeAutomatico", ex.ToString)

            Finally
                ConnNM.Disconnect()
                intIdEmpresa = Nothing
                strTransportador = Nothing
                strIataEmisora = Nothing
                strCodCiudad = Nothing
                strCodigoPNR = Nothing
                intGrupo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado

        End Function
        Public Function ObtieneResultadosTourCodeAutomatico(ByVal intIdEmpresa As Integer, _
                                                            ByVal strCodigoPNR As String, _
                                                            ByVal strTipoPasajero As String, _
                                                            ByVal intGrupo As Integer, _
                                                            ByVal strCodigoSeguimiento As String, _
                                                            ByVal intFirmaDB As Integer, _
                                                            ByVal intEsquema As Integer) As classTourCodeResultado

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objTourCodeResultado As classTourCodeResultado = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_EMC_UP_TOURCODE_EVALUACION, Constantes.StoredProcedure)
                'Parametros de entrada
                ConnNM.AgregarParametro("@arg_empresa", intIdEmpresa, OracleDbType.Double, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@codigo_pnr", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@codigo_grupo", intGrupo, OracleDbType.Double, 0, ParameterDirection.Input)
                'Parametros de salida
                ConnNM.AgregarParametro("@numero_tarifario_out", 0, OracleDbType.Double, 0, ParameterDirection.Output) '01
                ConnNM.AgregarParametro("@numero_comision_out", 0, OracleDbType.Double, 0, ParameterDirection.Output) '02
                ConnNM.AgregarParametro("@tipo_codigo_out", "", OracleDbType.Varchar2, 1, ParameterDirection.Output) '03
                ConnNM.AgregarParametro("@tourcode_out", "", OracleDbType.Varchar2, 20, ParameterDirection.Output) '04
                ConnNM.AgregarParametro("@porc_comision_out", 0, OracleDbType.Double, 0, ParameterDirection.Output) '05 
                ConnNM.AgregarParametro("@porc_agencia_out", 0, OracleDbType.Double, 0, ParameterDirection.Output) '06
                ConnNM.AgregarParametro("@porc_factor_meta_out", 0, OracleDbType.Double, 0, ParameterDirection.Output) '07
                ConnNM.AgregarParametro("@porc_over_out", 0, OracleDbType.Double, 0, ParameterDirection.Output) '08
                ConnNM.AgregarParametro("@over_nace_cancelado_out", 0, OracleDbType.Double, 0, ParameterDirection.Output) '09
                ConnNM.AgregarParametro("@es_emision_web_out", 0, OracleDbType.Double, 0, ParameterDirection.Output) '10
                ConnNM.AgregarParametro("@account_code_out", "", OracleDbType.Varchar2, 15, ParameterDirection.Output) '11
                ConnNM.AgregarParametro("@adicionar_over_out", 0, OracleDbType.Double, 0, ParameterDirection.Output) '12
                '----------
                ConnNM._ExecuteReader()

                'If syObCommand.Parameters("@numero_tarifario_out").Value Is DBNull.Value Then
                '    oNoTarifario = 0
                'Else
                '    oNoTarifario = syObCommand.Parameters("@numero_tarifario_out").Value.ToString
                'End If

                'While objOracleDataReader.Read

                objTourCodeResultado = New classTourCodeResultado
                objTourCodeResultado.Tarifario = ConnNM.LeeParametros("@numero_tarifario_out", -1)
                objTourCodeResultado.TipoPasajero = strTipoPasajero
                objTourCodeResultado.Regla = ConnNM.LeeParametros("@numero_comision_out", -1)
                objTourCodeResultado.TipoNet_Tour = ConnNM.LeeParametros("@tipo_codigo_out", Nothing)
                If String.IsNullOrEmpty(Trim(objTourCodeResultado.TipoNet_Tour)) Then
                    objTourCodeResultado.TipoNet_Tour = Nothing
                End If
                objTourCodeResultado.TourCode = ConnNM.LeeParametros("@tourcode_out", Nothing)
                If String.IsNullOrEmpty(Trim(objTourCodeResultado.TourCode)) Then
                    objTourCodeResultado.TourCode = Nothing
                End If
                objTourCodeResultado.ComNM = ConnNM.LeeParametros("@porc_comision_out", -1)
                objTourCodeResultado.ComAgencia = ConnNM.LeeParametros("@porc_agencia_out", -1)
                objTourCodeResultado.FactorMeta = ConnNM.LeeParametros("@porc_factor_meta_out", -1)
                objTourCodeResultado.Over = ConnNM.LeeParametros("@porc_over_out", -1)
                objTourCodeResultado.OverNacCancelado = ConnNM.LeeParametros("@over_nace_cancelado_out", -1)
                objTourCodeResultado.EmisionWeb = ConnNM.LeeParametros("@es_emision_web_out", -1)

                ' End While

            Catch ex As Exception
                objTourCodeResultado = Nothing
                strLog = "Stored Procedure : " & Constantes.spGDS_EMC_UP_CONCEPTOS_TOURCODES & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Tipo Pasajero: " & strTipoPasajero.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtieneResultadosTourCodeAutomatico" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtieneResultadosTourCodeAutomatico", strCodigoSeguimiento)

                Err.Raise(13, "ObtieneResultadosTourCodeAutomatico", ex.ToString)

            Finally
                ConnNM.Disconnect()
                intIdEmpresa = Nothing
                strCodigoPNR = Nothing
                strTipoPasajero = Nothing
                intGrupo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objTourCodeResultado

        End Function
        'Richard
        Public Function ObtieneConceptos_TarifaBulk(ByVal strTransportador As String, _
                                                    ByVal strCiudad_Destino As String, _
                                                    ByVal strCodigoPNR As String, _
                                                    ByVal strCodigoSeguimiento As String, _
                                                    ByVal intFirmaDB As Integer, _
                                                    ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim bolResultado As Boolean = False

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_EMC_UP_CONCEPTOS_TARIFABULK, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("v_transportador", strTransportador, OracleDbType.Varchar2, 2, ParameterDirection.Input)
                ConnNM.AgregarParametro("v_ciudad_destino", strCiudad_Destino, OracleDbType.Varchar2, 3, ParameterDirection.Input)
                ConnNM.AgregarParametro("v_codigo_pnr", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)

                '----------
                ConnNM._ExecuteReader()
                bolResultado = True

            Catch ex As Exception
                bolResultado = Nothing
                strLog = "Stored Procedure : " & Constantes.spGDS_EMC_UP_CONCEPTOS_TOURCODES & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de     Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtieneConceptos_TarifaBulk" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtieneConceptos_TarifaBulk", strCodigoSeguimiento)

                Err.Raise(13, "ObtieneConceptos_TarifaBulk", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strTransportador = Nothing
                strCiudad_Destino = Nothing
                strCodigoPNR = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado

        End Function
        Public Function ObtieneTarifaBulkFee(ByVal strCodigoPNR As String, _
                                             ByVal strPseudoConsulta As String, _
                                             ByVal strAerolinea As String, _
                                             ByVal strCiudadDestino As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As classFeeTarifaBulkResultado()

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objFeeTarifaBulkResultado() As classFeeTarifaBulkResultado = Nothing

            Try
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_EMC_UP_TARIFABULK_EVALUACION, Constantes.StoredProcedure)
                'Parametros de entrada
                ConnNM.AgregarParametro("v_transportador", strAerolinea, OracleDbType.Varchar2, 2, ParameterDirection.Input)
                ConnNM.AgregarParametro("v_ciudad_destino", strCiudadDestino, OracleDbType.Varchar2, 3, ParameterDirection.Input)
                ConnNM.AgregarParametro("v_codigo_pnr", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)
                'Parametros de salida
                ConnNM.AgregarParametro("v_regla_tarifabulk_out", 0, OracleDbType.Double, 0, ParameterDirection.Output) '01
                ConnNM.AgregarParametro("v_cadena_importes_out", 0, OracleDbType.Varchar2, 6000, ParameterDirection.Output) '02

                '--------------------------------------------------------------------------------------------------------------------------

                ConnNM._ExecuteReader()
                Dim Tarifario As Integer = -1
                Dim Resultados As String = Nothing

                Tarifario = ConnNM.LeeParametros("v_regla_tarifabulk_out", -1)
                Resultados = ConnNM.LeeParametros("v_cadena_importes_out", Nothing)

                If Not String.IsNullOrEmpty(Trim(Resultados)) Then
                    If Not Resultados.Equals("null") Then
                        Dim aResultados() As String = Resultados.Split("/")
                        If aResultados.Length > 0 Then
                            For i As Integer = 0 To aResultados.Length - 1
                                If Not String.IsNullOrEmpty(aResultados(i)) Then

                                    If strPseudoConsulta.Equals(aResultados(i).Split(";")(0)) And _
                                         CStr(aResultados(i).Split(";")(4)) = "1" Then 'JMATTO ADD

                                        If objFeeTarifaBulkResultado Is Nothing Then
                                            ReDim objFeeTarifaBulkResultado(0)
                                        Else
                                            ReDim Preserve objFeeTarifaBulkResultado(objFeeTarifaBulkResultado.Length)
                                        End If

                                        objFeeTarifaBulkResultado(objFeeTarifaBulkResultado.Length - 1) = New classFeeTarifaBulkResultado
                                        objFeeTarifaBulkResultado(objFeeTarifaBulkResultado.Length - 1).PseudoOficina = aResultados(i).Split(";")(0)
                                        objFeeTarifaBulkResultado(objFeeTarifaBulkResultado.Length - 1).Es_porcentaje = aResultados(i).Split(";")(1)
                                        objFeeTarifaBulkResultado(objFeeTarifaBulkResultado.Length - 1).Fee_Minimo = aResultados(i).Split(";")(2)
                                        If Not String.IsNullOrEmpty(objFeeTarifaBulkResultado(objFeeTarifaBulkResultado.Length - 1).Fee_Minimo) Then
                                            If objFeeTarifaBulkResultado(objFeeTarifaBulkResultado.Length - 1).Fee_Minimo = ".00" Then
                                                objFeeTarifaBulkResultado(objFeeTarifaBulkResultado.Length - 1).Fee_Minimo = "0.00"
                                            End If
                                        End If
                                        objFeeTarifaBulkResultado(objFeeTarifaBulkResultado.Length - 1).Fee_Maximo = aResultados(i).Split(";")(3)
                                        If Not String.IsNullOrEmpty(objFeeTarifaBulkResultado(objFeeTarifaBulkResultado.Length - 1).Fee_Maximo) Then
                                            If objFeeTarifaBulkResultado(objFeeTarifaBulkResultado.Length - 1).Fee_Maximo = ".00" Then
                                                objFeeTarifaBulkResultado(objFeeTarifaBulkResultado.Length - 1).Fee_Maximo = "0.00"
                                            End If
                                        End If

                                        objFeeTarifaBulkResultado(objFeeTarifaBulkResultado.Length - 1).Venta_X_Web = aResultados(i).Split(";")(4)
                                        objFeeTarifaBulkResultado(objFeeTarifaBulkResultado.Length - 1).Muestra_Web_Agencia = aResultados(i).Split(";")(5)
                                        objFeeTarifaBulkResultado(objFeeTarifaBulkResultado.Length - 1).No_permite_RUC = aResultados(i).Split(";")(6)
                                        'objFeeTarifaBulkResultado(i).Emite_con_TarjetaCredito = aResultados(i).Split(";")(7)

                                    End If
                                End If
                            Next
                        End If
                    End If
                End If

            Catch ex As Exception
                objFeeTarifaBulkResultado = Nothing
                strLog = "Stored Procedure : " & Constantes.spGDS_EMC_UP_TARIFABULK_EVALUACION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtieneTarifaBulkFee" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtieneTarifaBulkFee", strCodigoSeguimiento)

                Err.Raise(13, "ObtieneTarifaBulkFee", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strAerolinea = Nothing
                strCodigoPNR = Nothing
                strCiudadDestino = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objFeeTarifaBulkResultado

        End Function
        Public Function ObtenerSecuenciaPseudobulkEvaluacion(ByVal strCodigoPNR As String, _
                                                             ByVal strIdCliente As String, _
                                                             ByVal strCodigoSeguimiento As String, _
                                                             ByVal intFirmaDB As Integer, _
                                                             ByVal intEsquema As Integer) As List(Of classSecuenciaPseudobulk)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objSecuenciaPseudobulk As List(Of classSecuenciaPseudobulk) = Nothing
            Dim objSecuenciaPseudobulkAux As classSecuenciaPseudobulk = Nothing


            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_PSEUDOBULK_EVALUACION, Constantes.StoredProcedure)
                'ConnNM.AgregarParametro("p_Cliente", intCliente, OracleDbType.Int32, 0, ParameterDirection.Input)
                'ConnNM.AgregarParametro("p_NomBaseDatos", strBaseDatos, OracleDbType.Varchar2, 30, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_CODIGO_RESERVA", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_ID_CLIENTE", strIdCliente, OracleDbType.Varchar2, strIdCliente.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@RESULTADO", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()



                While objOracleDataReader.Read
                    objSecuenciaPseudobulkAux = New classSecuenciaPseudobulk

                    objSecuenciaPseudobulkAux.Secuencia = ConnNM.LeeColumnasDataReader(objOracleDataReader, "SECUENCIA", Nothing)


                    If objSecuenciaPseudobulk Is Nothing Then objSecuenciaPseudobulk = New List(Of classSecuenciaPseudobulk)
                    objSecuenciaPseudobulk.Add(objSecuenciaPseudobulkAux)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_PSEUDOBULK_EVALUACION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerSecuenciaPseudobulkEvaluacion" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerSecuenciaPseudobulkEvaluacion", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerSecuenciaPseudobulkEvaluacion", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                objSecuenciaPseudobulkAux = Nothing
            End Try

            Return objSecuenciaPseudobulk

        End Function

        Public Function ObtenerAerolineas(ByVal strCodigoSeguimiento As String, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal intEsquema As Integer) As List(Of classAerolineaC)


            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objAerolineaC As List(Of classAerolineaC) = Nothing
            Dim objAerolineaCAux As classAerolineaC = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_LISTAR_AEROLINEAS_TRANSPORTADOR, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()


                While objOracleDataReader.Read
                    objAerolineaCAux = New classAerolineaC

                    objAerolineaCAux.IdTransportador = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_TRANSPORTADOR", Nothing)


                    If objAerolineaC Is Nothing Then objAerolineaC = New List(Of classAerolineaC)
                    objAerolineaC.Add(objAerolineaCAux)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_LISTAR_AEROLINEAS_TRANSPORTADOR & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerAerolineas" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerAerolineas", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerAerolineas", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                objAerolineaCAux = Nothing
            End Try

            Return objAerolineaC

        End Function

        Public Function ListarAerolineas(ByVal strCodigoSeguimiento As String, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal intEsquema As Integer) As List(Of ClsAerolinea)


            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim listAerolinea As List(Of ClsAerolinea) = Nothing
            Dim aerolinea As ClsAerolinea = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_LISTAR_AEROLINEAS, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)
                objOracleDataReader = ConnNM._ExecuteReader()


                While objOracleDataReader.Read
                    aerolinea = New ClsAerolinea

                    aerolinea.idtransportador = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_TRANSPORTADOR", Nothing)
                    aerolinea.nombre = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)
                    aerolinea.idprefijo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PREFIJO", Nothing)
                    aerolinea.idiatatransportador = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_IATA_TRANSPORTADOR", Nothing)


                    If listAerolinea Is Nothing Then listAerolinea = New List(Of ClsAerolinea)
                    listAerolinea.Add(aerolinea)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_LISTAR_AEROLINEAS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ListarAerolineas" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ListarAerolineas", strCodigoSeguimiento)

                Err.Raise(10, "ListarAerolineas", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                aerolinea = Nothing
            End Try

            Return listAerolinea

        End Function
        Public Function EliminarTarifasBulkFq(ByVal strCodigoPNR As String, _
                                              ByVal intCliente As Integer, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean = False

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_DEL_TARIFABULK_FQ, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_CodigoPNR", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cliente", intCliente, OracleDbType.Int64, 0, ParameterDirection.Input)
                '----------
                bolResultado = ConnNM._DeleteExecuteNonQuery()


            Catch ex As Exception
                bolResultado = Nothing
                strLog = "Stored Procedure : " & Constantes.spGDS_DEL_TARIFABULK_FQ & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de     Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "EliminarTarifasBulkFq" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "EliminarTarifasBulkFq", strCodigoSeguimiento)

                Err.Raise(13, "EliminarTarifasBulkFq", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strCodigoPNR = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado

        End Function
        Public Function EliminarTarifasBulkCombinaciones(ByVal strCodigoPNR As String, _
                                                         ByVal intCliente As Integer, _
                                                         ByVal strCodigoSeguimiento As String, _
                                                         ByVal intFirmaDB As Integer, _
                                                         ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean = False

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_DEL_TARIFABULK_COMBINACION, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_CodigoPNR", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cliente", intCliente, OracleDbType.Int64, 0, ParameterDirection.Input)
                '----------
                bolResultado = ConnNM._DeleteExecuteNonQuery()


            Catch ex As Exception
                bolResultado = Nothing
                strLog = "Stored Procedure : " & Constantes.spGDS_DEL_TARIFABULK_COMBINACION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de     Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "EliminarTarifasBulkCombinaciones" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "EliminarTarifasBulkCombinaciones", strCodigoSeguimiento)

                Err.Raise(13, "EliminarTarifasBulkCombinaciones", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strCodigoPNR = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado

        End Function
 
        Public Function ObtenerPosiblesAerolineasCC(ByVal strCodigoSeguimiento As String, _
                                                    ByVal intFirmaDB As Integer, _
                                                    ByVal intEsquema As Integer) As List(Of classAerolineaC)


            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objAerolineaC As List(Of classAerolineaC) = Nothing
            Dim objAerolineaCAux As classAerolineaC = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_POSIBLES_AEROLINEAS_CC, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()


                While objOracleDataReader.Read
                    objAerolineaCAux = New classAerolineaC

                    objAerolineaCAux.IdTransportador = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_TRANSPORTADOR", Nothing)


                    If objAerolineaC Is Nothing Then objAerolineaC = New List(Of classAerolineaC)
                    objAerolineaC.Add(objAerolineaCAux)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_LISTAR_AEROLINEAS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerPosiblesAerolineasCC" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerPosiblesAerolineasCC", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerPosiblesAerolineasCC", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                objAerolineaCAux = Nothing
            End Try

            Return objAerolineaC

        End Function
        Public Function ObtenerDtTarifabulkFq(ByVal strCodigoPNR As String, _
                                              ByVal strCliente As String, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As List(Of classTarifabulkFq)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objTarifabulkFq As List(Of classTarifabulkFq) = Nothing
            Dim objTarifabulkFqAux As classTarifabulkFq = Nothing


            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_LISTA_DT_TARIFABULK_FQ, Constantes.StoredProcedure)
                'ConnNM.AgregarParametro("p_Cliente", intCliente, OracleDbType.Int32, 0, ParameterDirection.Input)
                'ConnNM.AgregarParametro("p_NomBaseDatos", strBaseDatos, OracleDbType.Varchar2, 30, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_CodigoPNR", strCodigoPNR, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cliente", strCliente, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()



                While objOracleDataReader.Read
                    objTarifabulkFqAux = New classTarifabulkFq

                    objTarifabulkFqAux.CodigoPNR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PNR", Nothing)
                    objTarifabulkFqAux.Airlines = ConnNM.LeeColumnasDataReader(objOracleDataReader, "AIRLINES", Nothing)
                    objTarifabulkFqAux.PassengerType = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PASSENGERTYPE", Nothing)
                    objTarifabulkFqAux.FareBasis = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FAREBASIS", Nothing)
                    objTarifabulkFqAux.BookingClass = ConnNM.LeeColumnasDataReader(objOracleDataReader, "BOOKINGCLASS", Nothing)
                    objTarifabulkFqAux.Currency = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CURRENCY", Nothing)
                    objTarifabulkFqAux.BaseFare = ConnNM.LeeColumnasDataReader(objOracleDataReader, "BASEFARE", -1)
                    objTarifabulkFqAux.ExpirationDate = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EXPIRATIONDATE", Nothing)
                    objTarifabulkFqAux.EffectiveDate = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EFFECTIVEDATE", Nothing)
                    objTarifabulkFqAux.TicketDate = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TICKETDATE", Nothing)
                    objTarifabulkFqAux.AdvancePurchase = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ADVANCEPURCHASE", Nothing)
                    objTarifabulkFqAux.MinStay = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MINSTAY", Nothing)
                    objTarifabulkFqAux.MaxStay = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MAXSTAY", Nothing)
                    objTarifabulkFqAux.Pseudo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PSEUDO", Nothing)
                    objTarifabulkFqAux.Ids = ConnNM.LeeColumnasDataReader(objOracleDataReader, "IDS", Nothing)
                    objTarifabulkFqAux.Numero = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO", Nothing)
                    objTarifabulkFqAux.FeeMaximo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FEEMAXIMO", -1)
                    objTarifabulkFqAux.FeeMinimo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FEEMINIMO", -1)
                    objTarifabulkFqAux.Dk = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DK", -1)
                    objTarifabulkFqAux.AccountCode = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ACCOUNTCODE", Nothing)
                    objTarifabulkFqAux.Cabina = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CABINA", Nothing)
                    objTarifabulkFqAux.PaxEquivalente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAXEQUIVALENTE", Nothing)
                    objTarifabulkFqAux.Reglas = ConnNM.LeeColumnasDataReader(objOracleDataReader, "REGLAS", Nothing)
                    objTarifabulkFqAux.SeasonAlApplic = ConnNM.LeeColumnasDataReader(objOracleDataReader, "SEASONALAPPLIC", Nothing)
                    objTarifabulkFqAux.IdPseudo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "IDPSEUDO", Nothing)

                    If objTarifabulkFq Is Nothing Then objTarifabulkFq = New List(Of classTarifabulkFq)
                    objTarifabulkFq.Add(objTarifabulkFqAux)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_LISTA_DT_TARIFABULK_FQ & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDtTarifabulkFq" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerDtTarifabulkFq", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerDtTarifabulkFq", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                objTarifabulkFqAux = Nothing
            End Try

            Return objTarifabulkFq

        End Function
        Public Function ObtenerAerolineaTarifa(ByVal strCodigoPNR As String, _
                                               ByVal strCliente As String, _
                                               ByVal strCodigoSeguimiento As String, _
                                               ByVal intFirmaDB As Integer, _
                                               ByVal intEsquema As Integer) As List(Of String)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objResultado As List(Of String) = Nothing
            Dim objAux As String = Nothing


            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_AEROLINEA_TARIFA, Constantes.StoredProcedure)
                'ConnNM.AgregarParametro("p_Cliente", intCliente, OracleDbType.Int32, 0, ParameterDirection.Input)
                'ConnNM.AgregarParametro("p_NomBaseDatos", strBaseDatos, OracleDbType.Varchar2, 30, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_CodigoPNR", strCodigoPNR, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cliente", strCliente, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()



                While objOracleDataReader.Read



                    objAux = ConnNM.LeeColumnasDataReader(objOracleDataReader, "AIRLINES", Nothing)


                    If objResultado Is Nothing Then objResultado = New List(Of String)
                    objResultado.Add(objAux)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_AEROLINEA_TARIFA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerAerolineaTarifa" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerAerolineaTarifa", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerAerolineaTarifa", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                objAux = Nothing
            End Try

            Return objResultado

        End Function
        Public Function InsertarTARIFABULK_FQ(ByVal strDk As String, _
                                              ByVal strCodigoPNR As String, _
                                              ByVal strAirlines As String, _
                                              ByVal strPassengerType As String, _
                                              ByVal strFareBasis As String, _
                                              ByVal strBookingClass As String, _
                                              ByVal strCurrency As String, _
                                              ByVal douBaseFare As Double, _
                                              ByVal strExpirationDate As String, _
                                              ByVal strEffectiveDate As String, _
                                              ByVal strTicketDate As String, _
                                              ByVal strAdvancePurchase As String, _
                                              ByVal strMinStay As String, _
                                              ByVal strMaxStay As String, _
                                              ByVal strPseudo As String, _
                                              ByVal strIds As String, _
                                              ByVal strNumero As String, _
                                              ByVal douFeeMinimo As Double, _
                                              ByVal douFeeMaximo As Double, _
                                              ByVal strAccountCode As String, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean = False

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_INSERTAR_TARIFABULK_FQ, Constantes.StoredProcedure)

                'If String.IsNullOrEmpty(objTurboDatosPax.IdTipoDocumento) Then
                'ConnNM.AgregarParametro("p_ID_TIPO_DOCUMENTO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                'Else
                ConnNM.AgregarParametro("@p_DK", strDk, OracleDbType.Varchar2, strDk.ToString.Length, ParameterDirection.Input)
                'End If

                'If String.IsNullOrEmpty(objTurboDatosPax.NumeroDocumento) Then
                'ConnNM.AgregarParametro("p_NUM_DOCUMENTO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                'Else
                ConnNM.AgregarParametro("@p_PNR", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.ToString.Length, ParameterDirection.Input)
                'End If
                ConnNM.AgregarParametro("@p_AIRLINES", strAirlines, OracleDbType.Varchar2, strAirlines.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_PASSENGERTYPE", strPassengerType, OracleDbType.Varchar2, strPassengerType.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_FAREBASIS", strFareBasis, OracleDbType.Varchar2, strFareBasis.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_BOOKINGCLASS", strBookingClass, OracleDbType.Varchar2, strBookingClass.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_CURRENCY", strCurrency, OracleDbType.Varchar2, strCurrency.ToString.Length, ParameterDirection.Input)

                'IIf(oTARIFABULK_FQ.BASEFARE <= 0, "NULL", oTARIFABULK_FQ.BASEFARE)
                If (douBaseFare <= 0) Then
                    ConnNM.AgregarParametro("p_BASEFARE", Nothing, OracleDbType.Double, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_BASEFARE", douBaseFare, OracleDbType.Double, douBaseFare.ToString.Length, ParameterDirection.Input)
                End If

                ConnNM.AgregarParametro("p_EXPIRATIONDATE", strExpirationDate, OracleDbType.Varchar2, strExpirationDate.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_EFFECTIVEDATE", strEffectiveDate, OracleDbType.Varchar2, strEffectiveDate.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_TICKETDATE", strTicketDate, OracleDbType.Varchar2, strTicketDate.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_ADVANCEPURCHASE", strAdvancePurchase, OracleDbType.Varchar2, strAdvancePurchase.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_MINSTAY", strMinStay, OracleDbType.Varchar2, strMinStay.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_MAXSTAY", strMaxStay, OracleDbType.Varchar2, strMaxStay.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_PSEUDO", strPseudo, OracleDbType.Varchar2, strPseudo.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_IDS", strIds, OracleDbType.Varchar2, strIds.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_NUMERO", strNumero, OracleDbType.Varchar2, strNumero.ToString.Length, ParameterDirection.Input)


                'IIf(oTARIFABULK_FQ.FEEMINIMO < 0, "NULL", oTARIFABULK_FQ.FEEMINIMO)
                If (douFeeMinimo < 0) Then
                    ConnNM.AgregarParametro("p_FEEMINIMO", Nothing, OracleDbType.Double, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_FEEMINIMO", douFeeMinimo, OracleDbType.Double, 0, ParameterDirection.Input)
                End If


                'IIf(oTARIFABULK_FQ.FEEMAXIMO < 0, "NULL", oTARIFABULK_FQ.FEEMAXIMO)
                If (douFeeMaximo < 0) Then
                    ConnNM.AgregarParametro("p_FEEMAXIMO", Nothing, OracleDbType.Double, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_FEEMAXIMO", douFeeMaximo, OracleDbType.Double, 0, ParameterDirection.Input)
                End If

                ConnNM.AgregarParametro("p_ACCOUNTCODE", strAccountCode, OracleDbType.Varchar2, strAccountCode.ToString.Length, ParameterDirection.Input)

                bolResultado = ConnNM._InsertExecuteNonQuery()
                '----------

            Catch ex As Exception

                bolResultado = False
                strLog = "Stored Procedure : " & Constantes.spGDS_INSERTAR_TARIFABULK_FQ & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "InsertarTARIFABULK_FQ" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "InsertarTARIFABULK_FQ", strCodigoSeguimiento)

                Err.Raise(13, "InsertarTARIFABULK_FQ", ex.ToString)


            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function
        Public Function ObtenerTipoPaxEspecial(ByVal strIdTipoPaxVuelo As String, _
                                               ByVal strCodigoSeguimiento As String, _
                                               ByVal intFirmaDB As Integer, _
                                               ByVal intEsquema As Integer) As classTipoPaxVuelo

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objTipoPaxVuelo As classTipoPaxVuelo = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_LISTAR_TIPO_DE_PAX_VUELO, Constantes.StoredProcedure)
                'ConnNM.AgregarParametro("p_Cliente", intCliente, OracleDbType.Int32, 0, ParameterDirection.Input)
                'ConnNM.AgregarParametro("p_NomBaseDatos", strBaseDatos, OracleDbType.Varchar2, 30, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_ID_TIPO_DE_PAX_VUELO", strIdTipoPaxVuelo, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()


                While objOracleDataReader.Read
                    objTipoPaxVuelo = New classTipoPaxVuelo

                    objTipoPaxVuelo.IdTipoDePax = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_TIPO_DE_PAX_VUELO", Nothing)
                    objTipoPaxVuelo.TipoPax = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                    objTipoPaxVuelo.EsDesuso = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ES_DESUSO", -1)
                    objTipoPaxVuelo.Equivale = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EQUIVALE", Nothing)
                    objTipoPaxVuelo.Pertenece = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PERTENECE", Nothing)
                    objTipoPaxVuelo.IdOrden = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_ORDEN", -1)
                    objTipoPaxVuelo.Publicadas = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PUBLICADAS", Nothing)
                    objTipoPaxVuelo.Privadas = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PRIVADAS", Nothing)
                    objTipoPaxVuelo.Aerolinea = ConnNM.LeeColumnasDataReader(objOracleDataReader, "AEROLINEA", Nothing)
                    objTipoPaxVuelo.Countable = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COUNTABLE", -1)
                    objTipoPaxVuelo.PerteneceAmadeus = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PERTENECE_AMADEUS", Nothing)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_LISTAR_TIPO_DE_PAX_VUELO & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerTipoPaxEspecial" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerTipoPaxEspecial", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerTipoPaxEspecial", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objTipoPaxVuelo

        End Function
        Public Function ComparaTipoPaxEspecial(ByVal strIdTipoPaxVuelo As String, _
                                               ByVal strCodigoSeguimiento As String, _
                                               ByVal intFirmaDB As Integer, _
                                               ByVal intEsquema As Integer) As Boolean

            Dim oRespuesta As Boolean = False
            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim oDataTable As New DataTable

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_LISTAR_TIPO_DE_PAX_VUELO, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_ID_TIPO_DE_PAX_VUELO", strIdTipoPaxVuelo, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                If ConnNM.Connect(intFirmaDB) Then
                    If oDataTable.Rows.Count > 0 Then
                        oDataTable.Clear()
                    End If

                    oDataTable = New DataTable
                    oDataTable.TableName = "ComparaTipoDePax"
                    oDataTable.Load(objOracleDataReader)

                    If oDataTable.Rows.Count > 0 Then

                        For i As Integer = 0 To oDataTable.Rows.Count - 1
                            If UCase(strIdTipoPaxVuelo) = oDataTable.Rows(0).Item(0) Then
                                oRespuesta = True
                                Exit For
                            End If
                        Next

                    Else
                        oRespuesta = False
                    End If
                Else
                    oRespuesta = False
                End If


            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_LISTAR_TIPO_DE_PAX_VUELO & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ComparaTipoPaxEspecial" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ComparaTipoPaxEspecial", strCodigoSeguimiento)

                Err.Raise(10, "ComparaTipoPaxEspecial", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return oRespuesta

        End Function
#Region "TarifasBulkHugo"
        Public Function ObtenerConceptosTarifasBulk_HS(ByVal strTransportador As String, _
                                                       ByVal strCiudadDestino As String, _
                                                       ByVal strCodigoPnr As String, _
                                                       ByVal strCodigoSeguimiento As String, _
                                                       ByVal intFirmaDB As Integer, _
                                                       ByVal intEsquema As Integer) As List(Of classPseudoBulkConceptos)


            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objPseudobulkConceptos As List(Of classPseudoBulkConceptos) = Nothing
            Dim objPseudobulkConceptosAux As classPseudoBulkConceptos = Nothing



            Try


                BorrarTarifaBulkConceptos(strCodigoPnr, strTransportador, strCodigoSeguimiento, intFirmaDB, intEsquema)

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spUP_conceptos_tarifaBulk, Constantes.StoredProcedure)

                ConnNM.AgregarParametro("@v_transportador", strTransportador, OracleDbType.Varchar2, strTransportador.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@v_ciudad_destino", strCiudadDestino, OracleDbType.Varchar2, strCiudadDestino.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@v_codigo_pnr", strCodigoPnr, OracleDbType.Varchar2, strCodigoPnr.Length, ParameterDirection.Input)


                '----------
                If ConnNM._InsertExecuteNonQuery() Then


                    ConnNM.SP_Command(Constantes.spGDS_TARIFASBULK_CONCEPTOS, Constantes.StoredProcedure)

                    ConnNM.AgregarParametro("@p_CODIGO_RESERVA", strCodigoPnr, OracleDbType.Varchar2, strCodigoPnr.Length, ParameterDirection.Input)
                    ConnNM.AgregarParametro("@p_ID_TRANSPORTADOR", strTransportador, OracleDbType.Varchar2, strTransportador.Length, ParameterDirection.Input)
                    ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                    objOracleDataReader = ConnNM._ExecuteReader()


                    While objOracleDataReader.Read
                        objPseudobulkConceptosAux = New classPseudoBulkConceptos
                        objPseudobulkConceptosAux.CodigoPNR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CODIGO_RESERVA", Nothing)
                        objPseudobulkConceptosAux.CodigoConcepto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CODIGO_CONCEPTO", -1)
                        objPseudobulkConceptosAux.IdTransportador = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_TRANSPORTADOR", Nothing)

                        If objPseudobulkConceptos Is Nothing Then objPseudobulkConceptos = New List(Of classPseudoBulkConceptos)
                        objPseudobulkConceptos.Add(objPseudobulkConceptosAux)
                    End While

                    BorrarTarifaBulkConceptos(strCodigoPnr, strTransportador, strCodigoSeguimiento, intFirmaDB, intEsquema)

                End If

            Catch ex As Exception
                objPseudobulkConceptos = Nothing

                BorrarTarifaBulkConceptos(strCodigoPnr, strTransportador, strCodigoSeguimiento, intFirmaDB, intEsquema)

                strLog = "Stored Procedure : " & Constantes.spUP_conceptos_tarifaBulk & vbCrLf
                strLog = "Stored Procedure : " & Constantes.spGDS_TARIFASBULK_CONCEPTOS & vbCrLf

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerConceptosTarifasBulk_HS" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerConceptosTarifasBulk_HS", strCodigoSeguimiento)

                If ex.ToString.Contains("") Then

                End If

                Err.Raise(13, "ObtenerConceptosTarifasBulk_HS", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strTransportador = Nothing
                strCiudadDestino = Nothing
                strCodigoPnr = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objPseudobulkConceptos
        End Function
        Public Function InsertarConceptosTarifasBulk(ByVal strCodigoPNR As String, _
                                                     ByVal strPseudoConsulta As String, _
                                                     ByVal strAerolinea As String, _
                                                     ByVal strCiudadDestino As String, _
                                                     ByVal lstTourCodesConceptos As List(Of classTourCodesConceptos), _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As classFeeTarifaBulkResultado()

            'ciudad destino con comilla simple
            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean = False
            Dim bolCommit As Boolean = False
            Dim objFeeTarifaBulkResultado As classFeeTarifaBulkResultado() = Nothing
            Dim bolFlag As Boolean = True
            Dim strtabla As String = "TARIFABULK_EVALUACION"
            Dim strXML As StringBuilder = New StringBuilder

            Try

                ' CodigoTipoDato: 1 = String
                ' CodigoTipoDato: 2 = Integer
                ' CodigoTipoDato: 3 = Date

                bolResultado = BorrarTarifaBulkEvaluacion(strCodigoPNR, strCodigoSeguimiento, intFirmaDB, intEsquema)

                If bolResultado Then

                    ConnNM.Connect(intFirmaDB)
                    elaboraXML_TarifasBulk(strXML, lstTourCodesConceptos, strCodigoPNR, intFirmaDB, strCodigoSeguimiento)

                    ConnNM.SP_Command(Constantes.spGDS_MC_INS_CONCEPTOS, Constantes.StoredProcedure) 'CARGA LA TABLA MEDIANTE XML
                    ConnNM.AgregarParametro("@p_Tabla", strtabla, OracleDbType.Varchar2, strtabla.Length, ParameterDirection.Input)
                    ConnNM.AgregarParametro("@p_Xml", strXML.ToString(), OracleDbType.Clob, 0, ParameterDirection.Input)
                    '----------
                    bolResultado = ConnNM._InsertExecuteNonQuery()

                    objFeeTarifaBulkResultado = ObtieneTarifaBulkFee(strCodigoPNR, _
                                                                     strPseudoConsulta, _
                                                                     strAerolinea, _
                                                                     strCiudadDestino, _
                                                                     strCodigoSeguimiento, _
                                                                     intFirmaDB, _
                                                                     intEsquema)

                    bolResultado = BorrarTarifaBulkEvaluacion(strCodigoPNR, strCodigoSeguimiento, intFirmaDB, intEsquema)

                End If

            Catch ex As Exception

                bolResultado = False
                strLog = "Stored Procedure : " & Constantes.spGDS_DEL_TARIFABULK_EVALUACION & vbCrLf
                strLog &= "Stored Procedure : " & Constantes.spGDS_INS_TARIFABULK_EVALUACION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "InsertarConceptosTarifasBulk" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "InsertarConceptosTarifasBulk", strCodigoSeguimiento)

                Err.Raise(13, "InsertarConceptosTarifasBulk", ex.ToString)
            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objFeeTarifaBulkResultado
        End Function
        Public Sub elaboraXML_TarifasBulk(ByRef strXML As StringBuilder, ByVal lstTourCodesConceptos As List(Of classTourCodesConceptos), ByVal strCodigoPNR As String, ByVal intFirmaDB As Integer, ByVal strCodigoSeguimiento As String)
            Dim strValor As String = String.Empty
            Dim ConnNM As New MyConnectionOracle
            Dim intCorrelativo As Integer = -1
            Dim intContador As Integer = 1

            Try
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_TARIFASBULK_GETCORRELATIVO, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@CODIGO_RESERVA", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_correlativo", Nothing, OracleDbType.Int64, 0, ParameterDirection.Output)

                ConnNM._ExecuteReader()
                intCorrelativo = ConnNM.LeeParametros("p_correlativo", Nothing)

                strXML.Append("<?xml version='1.0'?>")
                strXML.Append("<ROWSET>")

                If intCorrelativo > 0 Then
                    For i As Integer = 0 To lstTourCodesConceptos.Count - 1
                        If Not String.IsNullOrEmpty(Trim(lstTourCodesConceptos.Item(i).Valor)) Then
                            strValor = String.Empty
                            strXML.Append("<ROW num='" & intContador.ToString() & "'>")

                            strXML.Append("<CODIGO_RESERVA>" & strCodigoPNR & "</CODIGO_RESERVA>")
                            strXML.Append("<CORRELATIVO_EVALUACION>" & intCorrelativo & "</CORRELATIVO_EVALUACION>")
                            strXML.Append("<CODIGO_CONCEPTO>" & lstTourCodesConceptos.Item(i).CodigoConcepto & "</CODIGO_CONCEPTO>")
                            strXML.Append("<VALOR>" & lstTourCodesConceptos.Item(i).Valor & "</VALOR>")
                            strXML.Append("</ROW>")
                            intCorrelativo += 1
                            intContador += 1
                        End If
                    Next

                    strXML.Append("</ROWSET>")
                Else
                    Throw New Exception("Error al  ejecutar el SP GDS_TARIFASBULK_GETCORRELATIVO")
                End If

            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spGDS_DEL_TARIFABULK_EVALUACION & vbCrLf
                strLog &= "Stored Procedure : " & Constantes.spGDS_INS_TARIFABULK_EVALUACION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "InsertarConceptosTarifasBulk" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "InsertarConceptosTarifasBulk", strCodigoSeguimiento)

                Err.Raise(13, "InsertarConceptosTarifasBulk", ex.ToString)
            Finally
                ConnNM.Disconnect()
            End Try
        End Sub
        'Public Function InsertarConceptosTarifasBulk(ByVal strCodigoPNR As String, _
        '                                             ByVal strPseudoConsulta As String, _
        '                                             ByVal strAerolinea As String, _
        '                                             ByVal strCiudadDestino As String, _
        '                                             ByVal lstTourCodesConceptos As List(Of classTourCodesConceptos), _
        '                                             ByVal strCodigoSeguimiento As String, _
        '                                             ByVal intFirmaDB As Integer, _
        '                                             ByVal intEsquema As Integer) As classFeeTarifaBulkResultado()

        '    'ciudad destino con comilla simple
        '    Dim ConnNM As New MyConnectionOracle
        '    Dim bolResultado As Boolean = False
        '    Dim bolCommit As Boolean = False
        '    Dim objFeeTarifaBulkResultado As classFeeTarifaBulkResultado() = Nothing
        '    Dim strValor As String = String.Empty
        '    Dim bolFlag As Boolean = True
        '    Try

        '        ' CodigoTipoDato: 1 = String
        '        ' CodigoTipoDato: 2 = Integer
        '        ' CodigoTipoDato: 3 = Date

        '        bolResultado = BorrarTarifaBulkEvaluacion(strCodigoPNR, strCodigoSeguimiento, intFirmaDB, intEsquema)

        '        If bolResultado Then

        '            ConnNM.Connect(intFirmaDB)

        '            For i As Integer = 0 To lstTourCodesConceptos.Count - 1

        '                If Not String.IsNullOrEmpty(Trim(lstTourCodesConceptos.Item(i).Valor)) Then
        '                    ConnNM.SP_Command(Constantes.spGDS_INS_TARIFABULK_EVALUACION, Constantes.StoredProcedure)
        '                    ConnNM.AgregarParametro("@p_CODIGO_RESERVA", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)
        '                    ConnNM.AgregarParametro("@p_CODIGO_CONCEPTO", lstTourCodesConceptos.Item(i).CodigoConcepto, OracleDbType.Int32, 0, ParameterDirection.Input)

        '                    strValor = String.Empty

        '                    If lstTourCodesConceptos.Item(i).CodigoTipoDato = 2 Then
        '                        ConnNM.AgregarParametro("@p_VALOR", lstTourCodesConceptos.Item(i).Valor, OracleDbType.Varchar2, lstTourCodesConceptos.Item(i).Valor.Length, ParameterDirection.Input)
        '                    Else
        '                        strValor = lstTourCodesConceptos.Item(i).Valor
        '                        ConnNM.AgregarParametro("@p_VALOR", strValor, OracleDbType.Varchar2, strValor.Length, ParameterDirection.Input)
        '                    End If

        '                    ConnNM._InsertExecuteNonQuery(bolCommit, bolFlag)

        '                    bolFlag = False
        '                End If
        '            Next

        '            bolResultado = ConnNM._Commit

        '            objFeeTarifaBulkResultado = ObtieneTarifaBulkFee(strCodigoPNR, _
        '                                                             strPseudoConsulta, _
        '                                                             strAerolinea, _
        '                                                             strCiudadDestino, _
        '                                                             strCodigoSeguimiento, _
        '                                                             intFirmaDB, _
        '                                                             intEsquema)

        '            bolResultado = BorrarTarifaBulkEvaluacion(strCodigoPNR, strCodigoSeguimiento, intFirmaDB, intEsquema)

        '        End If

        '    Catch ex As Exception

        '        bolResultado = False
        '        strLog = "Stored Procedure : " & Constantes.spGDS_DEL_TARIFABULK_EVALUACION & vbCrLf
        '        strLog &= "Stored Procedure : " & Constantes.spGDS_INS_TARIFABULK_EVALUACION & vbCrLf
        '        strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
        '        strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
        '        strLog &= Constantes.TabEspacios & "Source : " & "InsertarConceptosTarifasBulk" & vbCrLf
        '        strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
        '        objEscribeLog.WriteLog(strLog, "InsertarConceptosTarifasBulk", strCodigoSeguimiento)

        '        Err.Raise(13, "InsertarConceptosTarifasBulk", ex.ToString)
        '    Finally
        '        ConnNM.Disconnect()
        '        strCodigoSeguimiento = Nothing
        '        intFirmaDB = Nothing
        '        intEsquema = Nothing
        '        ConnNM = Nothing
        '    End Try

        '    Return objFeeTarifaBulkResultado
        'End Function
        Public Function BorrarTarifaBulkEvaluacion(ByVal strCodigoPNR As String, _
                                                   ByVal strCodigoSeguimiento As String, _
                                                   ByVal intFirmaDB As Integer, _
                                                   ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean = False

            Try

                ConnNM.Connect(intFirmaDB)

                ConnNM.SP_Command(Constantes.spGDS_DEL_TARIFABULK_EVALUACION, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_CODIGO_RESERVA", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)
                bolResultado = ConnNM._ExecuteNonQuery_BeginTransaction()

            Catch ex As Exception

                bolResultado = False
                strLog = "Stored Procedure : " & Constantes.spGDS_DEL_TARIFABULK_EVALUACION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "BorrarTarifaBulkEvaluacion" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "BorrarTarifaBulkEvaluacion", strCodigoSeguimiento)

                Err.Raise(13, "BorrarTarifaBulkEvaluacion", ex.ToString)
            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function
        Public Function BorrarTarifaBulkConceptos(ByVal strCodigoPNR As String, _
                                                  ByVal strID_Transportador As String, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean = False

            Try

                ConnNM.Connect(intFirmaDB)

                ConnNM.SP_Command(Constantes.spGDS_DEL_TARIFABULK_CONCEPTOS, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_CODIGO_RESERVA", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_ID_TRANSPORTADOR", strID_Transportador, OracleDbType.Varchar2, strID_Transportador.Length, ParameterDirection.Input)
                bolResultado = ConnNM._ExecuteNonQuery_BeginTransaction()

            Catch ex As Exception

                bolResultado = False
                strLog = "Stored Procedure : " & Constantes.spGDS_DEL_TARIFABULK_CONCEPTOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "BorrarTarifaBulkConceptos" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "BorrarTarifaBulkConceptos", strCodigoSeguimiento)

                Err.Raise(13, "BorrarTarifaBulkConceptos", ex.ToString)
            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function
#End Region
#Region "Pseudos"
        Public Function ObtenerTourCodesConceptos(ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As List(Of classTourCodesConceptos)


            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objTourCodesConceptos As List(Of classTourCodesConceptos) = Nothing
            Dim objTourCodesConceptosAux As classTourCodesConceptos = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_LISTAR_TOURCODES_CONCEPTOS, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()


                While objOracleDataReader.Read
                    objTourCodesConceptosAux = New classTourCodesConceptos

                    objTourCodesConceptosAux.CodigoConcepto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CODIGO_CONCEPTO", -1)
                    objTourCodesConceptosAux.DescripcionConcepto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION_CONCEPTO", Nothing)
                    objTourCodesConceptosAux.CodigoTipoDato = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CODIGO_TIPO_DATO_CONCEPTO", -1)


                    If objTourCodesConceptos Is Nothing Then objTourCodesConceptos = New List(Of classTourCodesConceptos)
                    objTourCodesConceptos.Add(objTourCodesConceptosAux)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_LISTAR_TOURCODES_CONCEPTOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerTourCodesConceptos" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerTourCodesConceptos", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerTourCodesConceptos", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objTourCodesConceptos

        End Function
        Public Function ObtenerPseudosbulkConceptos(ByVal strCodigoPNR As String, _
                                                    ByVal strCodigoSeguimiento As String, _
                                                    ByVal intFirmaDB As Integer, _
                                                    ByVal intEsquema As Integer) As List(Of classPseudoBulkConceptos)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objPseudobulkConceptos As List(Of classPseudoBulkConceptos) = Nothing
            Dim objPseudobulkConceptosAux As classPseudoBulkConceptos = Nothing


            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_PSEUDOSBULK_CONCEPTOS, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("@p_CodigoPNR", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@RESULTADO", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()



                While objOracleDataReader.Read
                    objPseudobulkConceptosAux = New classPseudoBulkConceptos

                    objPseudobulkConceptosAux.CodigoPNR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CODIGO_RESERVA", Nothing)
                    objPseudobulkConceptosAux.CodigoConcepto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CODIGO_CONCEPTO", -1)
                    objPseudobulkConceptosAux.IdTransportador = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_TRANSPORTADOR", Nothing)


                    If objPseudobulkConceptos Is Nothing Then objPseudobulkConceptos = New List(Of classPseudoBulkConceptos)
                    objPseudobulkConceptos.Add(objPseudobulkConceptosAux)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_PSEUDOSBULK_CONCEPTOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerPseudosbulkConceptos" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerPseudosbulkConceptos", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerPseudosbulkConceptos", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                objPseudobulkConceptosAux = Nothing
            End Try

            Return objPseudobulkConceptos

        End Function
        Public Sub ObtenerConceptosPseudosBulk(ByVal strTransportador As String, _
                                                    ByVal strCiudadDestino As String, _
                                                    ByVal strCodigoPNR As String, _
                                                    ByVal strCodigoSeguimiento As String, _
                                                    ByVal intFirmaDB As Integer, _
                                                    ByVal intEsquema As Integer)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim auxResultado As classDato = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_EMC_CONCEPTOS_PSEUDOSBULK, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("v_transportador", strTransportador, OracleDbType.Varchar2, strTransportador.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("v_ciudad_destino", strCiudadDestino, OracleDbType.Varchar2, strCiudadDestino.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("v_codigo_pnr", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)
                '----------

                ConnNM._ExecuteNonQuery_BeginTransaction()


            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spGDS_EMC_CONCEPTOS_PSEUDOSBULK & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerConceptosPseudosBulk" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerConceptosPseudosBulk", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerConceptosPseudosBulk", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                auxResultado = Nothing
            End Try
        End Sub
        Public Function InsertarPseudosBulkEvaluacion(ByVal strCodigoPNR As String, _
                                                      ByVal strIdCliente As String, _
                                                      ByVal lstTourCodesConceptos As List(Of classTourCodesConceptos), _
                                                      ByVal strCodigoSeguimiento As String, _
                                                      ByVal intFirmaDB As Integer, _
                                                      ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean = False
            Dim secuencia As New List(Of classSecuenciaPseudobulk)
            Dim listaTourCodes As New List(Of classTourCodesConceptos)
            listaTourCodes = lstTourCodesConceptos

            secuencia = ObtenerSecuenciaPseudobulkEvaluacion(strCodigoPNR, _
                                                             strIdCliente, _
                                                             strCodigoSeguimiento, _
                                                             intFirmaDB, _
                                                             intEsquema)

            Try

                ConnNM.Connect(intFirmaDB)

                For i As Integer = 0 To lstTourCodesConceptos.Count - 1

                    If Not String.IsNullOrEmpty(lstTourCodesConceptos.Item(i).Valor) Then


                        Dim valor As String = IIf(lstTourCodesConceptos.Item(i).Valor Is Nothing, "", lstTourCodesConceptos.Item(i).Valor)
                        Dim sec As Integer = Integer.Parse(secuencia.Item(0).Secuencia) + i

                        ConnNM.SP_Command(Constantes.spINSERTA_PSEUDOSBULK_EVALUACION, Constantes.StoredProcedure)

                        ConnNM.AgregarParametro("@oCODIGO_RESERVA", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)
                        ConnNM.AgregarParametro("@oCORRELATIVO_EVALUACION", sec, OracleDbType.Int32, sec.ToString.Length, ParameterDirection.Input)
                        ConnNM.AgregarParametro("@oCODIGO_CONCEPTO", listaTourCodes.Item(i).CodigoConcepto, OracleDbType.Int32, listaTourCodes.Item(i).CodigoConcepto.ToString.Length, ParameterDirection.Input)
                        ConnNM.AgregarParametro("@oVALOR", valor, OracleDbType.Varchar2, valor.Length, ParameterDirection.Input)
                        ConnNM.AgregarParametro("@oID_CLIENTE", strIdCliente, OracleDbType.Varchar2, strIdCliente.Length, ParameterDirection.Input)

                        bolResultado = ConnNM._InsertExecuteNonQuery()
                        '----------
                    End If

                Next

            Catch ex As Exception

                bolResultado = False
                strLog = "Stored Procedure : " & Constantes.spINSERTA_PSEUDOSBULK_EVALUACION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "InsertarPseudosBulkEvaluacion" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "InsertarPseudosBulkEvaluacion", strCodigoSeguimiento)

                Err.Raise(13, "InsertarPseudosBulkEvaluacion", ex.ToString)


            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing

                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function
        Public Function SP_NEW_PseudosBulkEvaluacion(ByVal strAerolinea As String, _
                                                     ByVal strCiudadDestino As String, _
                                                     ByVal strCodigoPNR As String, _
                                                     ByVal strIdCliente As Integer, _
                                                     ByVal strPseudo As String, _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As List(Of classPseudosBulkEvaluacion)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objPseudosBulkEvaluacion As List(Of classPseudosBulkEvaluacion) = Nothing
            Dim objPseudosBulkEvaluacionAux As classPseudosBulkEvaluacion = Nothing
            Dim objDataTable As DataTable = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_PseudosBulk_Evaluacion_T, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@v_transportador", strAerolinea, OracleDbType.Varchar2, strAerolinea.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@v_ciudad_destino", strCiudadDestino, OracleDbType.Varchar2, strCiudadDestino.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@v_codigo_pnr", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@v_pseudo", strPseudo, OracleDbType.Varchar2, strPseudo.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objDataTable = ConnNM._BeginTransactionDataTable("SP_NEW_PseudosBulkEvaluacion")

                If objDataTable IsNot Nothing Then
                    For i As Integer = 0 To objDataTable.Rows.Count - 1

                        objPseudosBulkEvaluacionAux = New classPseudosBulkEvaluacion

                        objPseudosBulkEvaluacionAux.Regla = CStr(objDataTable.Rows(i).Item(0))
                        objPseudosBulkEvaluacionAux.PseudosVenta = CStr(objDataTable.Rows(i).Item(1))
                        objPseudosBulkEvaluacionAux.PseudosEmision = CStr(objDataTable.Rows(i).Item(2))

                        If objPseudosBulkEvaluacion Is Nothing Then objPseudosBulkEvaluacion = New List(Of classPseudosBulkEvaluacion)
                        objPseudosBulkEvaluacion.Add(objPseudosBulkEvaluacionAux)

                    Next
                End If


                'While objOracleDataReader.Read
                '    objPseudosBulkEvaluacionAux = New classPseudosBulkEvaluacion

                '    objPseudosBulkEvaluacionAux.Regla = ConnNM.LeeColumnasDataReader(objOracleDataReader, "REGLA", Nothing)
                '    objPseudosBulkEvaluacionAux.PseudosVenta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PSEUDOS_VENTA", Nothing)
                '    objPseudosBulkEvaluacionAux.PseudosEmision = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PSEUDOS_EMISION", Nothing)

                '    If objPseudosBulkEvaluacion Is Nothing Then objPseudosBulkEvaluacion = New List(Of classPseudosBulkEvaluacion)
                '    objPseudosBulkEvaluacion.Add(objPseudosBulkEvaluacionAux)

                'End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_PseudosBulk_Evaluacion_T & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "SP_NEW_PseudosBulkEvaluacion" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "SP_NEW_PseudosBulkEvaluacion", strCodigoSeguimiento)
                EliminarPseudosBulkConceptos(strCodigoPNR, strAerolinea, strCodigoSeguimiento, intFirmaDB, intEsquema)
                EliminarPseudosBulkEvaluacion(strCodigoPNR, strIdCliente, strCodigoSeguimiento, intFirmaDB, intEsquema)

                Err.Raise(10, "SP_NEW_PseudosBulkEvaluacion", ex.ToString)

            Finally
                EliminarPseudosBulkConceptos(strCodigoPNR, strAerolinea, strCodigoSeguimiento, intFirmaDB, intEsquema)
                EliminarPseudosBulkEvaluacion(strCodigoPNR, strIdCliente, strCodigoSeguimiento, intFirmaDB, intEsquema)
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing

            End Try

            Return objPseudosBulkEvaluacion

        End Function
        Public Function EliminarPseudosBulkConceptos(ByVal strCodigoPNR As String, _
                                                     ByVal strAerolinea As String, _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean = False

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_DEL_PSEUDOSBULK_CONCEPTOS, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_CODIGO_RESERVA", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_ID_TRANSPORTADOR", strAerolinea, OracleDbType.Varchar2, strAerolinea.Length, ParameterDirection.Input)
                '----------
                bolResultado = ConnNM._DeleteExecuteNonQuery()


            Catch ex As Exception
                bolResultado = Nothing
                strLog = "Stored Procedure : " & Constantes.spGDS_DEL_PSEUDOSBULK_CONCEPTOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "EliminarPseudosBulkConceptos" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "EliminarPseudosBulkConceptos", strCodigoSeguimiento)

                Err.Raise(13, "EliminarPseudosBulkConceptos", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strCodigoPNR = Nothing
                strAerolinea = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado

        End Function
        Public Function EliminarPseudosBulkEvaluacion(ByVal strCodigoPNR As String, _
                                                      ByVal strIdCliente As String, _
                                                      ByVal strCodigoSeguimiento As String, _
                                                      ByVal intFirmaDB As Integer, _
                                                      ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean = False

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_DEL_PSEUDOSBULK_EVALUACION, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_CODIGO_RESERVA", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_ID_CLIENTE", strIdCliente, OracleDbType.Varchar2, strIdCliente.Length, ParameterDirection.Input)
                '----------
                bolResultado = ConnNM._DeleteExecuteNonQuery()


            Catch ex As Exception
                bolResultado = Nothing
                strLog = "Stored Procedure : " & Constantes.spGDS_DEL_PSEUDOSBULK_EVALUACION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "EliminarPseudosBulkEvaluacion" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "EliminarPseudosBulkEvaluacion", strCodigoSeguimiento)

                Err.Raise(13, "EliminarPseudosBulkEvaluacion", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strCodigoPNR = Nothing
                strIdCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return bolResultado
        End Function
#End Region
    End Class
End Namespace