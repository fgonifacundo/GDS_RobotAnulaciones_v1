Imports GDS_NuevoMundoPersistencia
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes

Namespace ObjetoAccesoDatos
    Partial Public Class classDAO
        'Private strLog As String = Nothing
        'Private objEscribeLog As New GDS_MuevoMundoLog.EscribeLog

        Public Function InsertarCorreoRobotSabre(ByVal intMarca As Int16, _
                                                 ByVal objCorreo As classCorreo, _
                                                 ByVal strCodigoSeguimiento As String, _
                                                 ByVal intFirmaDB As Integer, _
                                                 ByVal intEsquema As Integer) As Boolean
            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean

            Try

                ConnNM.Connect(intFirmaDB)

                ConnNM.SP_Command(Constantes.spGDS_INSERTA_EMAILROBOT, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("p_De", objCorreo.FromCorreo, OracleDbType.Varchar2, objCorreo.FromCorreo.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Para", objCorreo.ToCorreo, OracleDbType.Varchar2, objCorreo.ToCorreo.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_CCopia", objCorreo.CCCorreo, OracleDbType.Varchar2, objCorreo.CCCorreo.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_CCopiaOculta", objCorreo.BCCCorreo, OracleDbType.Varchar2, objCorreo.BCCCorreo.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Asunto", objCorreo.SubjectCorreo, OracleDbType.Varchar2, objCorreo.SubjectCorreo.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Cuerpo", objCorreo.BodyCorreo, OracleDbType.Clob, objCorreo.BodyCorreo.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Marca", intMarca, OracleDbType.Int16, 0, ParameterDirection.Input)

                bolResultado = ConnNM._InsertExecuteNonQuery()


            Catch ex As Exception

                bolResultado = Nothing
                strLog = "Stored Procedure : " & Constantes.spGDS_INSERTA_EMAILROBOT & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "GDS_INSERTA_EMAILROBOT" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "GDS_INSERTA_EMAILROBOT", strCodigoSeguimiento)

                If Not ex.ToString.ToUpper.Contains("VIOLADA") Then
                    Err.Raise(13, "GDS_INSERTA_EMAILROBOT", ex.ToString)
                End If

            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function

        Public Function ObtenerBoletosPendientesPagoSABRE(ByVal srtFecha As String, _
                                                          ByVal strHora As String, _
                                                          ByVal strCodigoSeguimiento As String, _
                                                          ByVal intFirmaDB As Integer, _
                                                          ByVal intEsquema As Integer) As List(Of robotBoletoPendientePago.robotBoletoPendiente)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objrobotBoletoPendiente As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim auxrobotBoletoPendiente As robotBoletoPendientePago.robotBoletoPendiente = Nothing
            Dim auxPromotor As classPromotor = Nothing
            Try
                Dim commandText = Constantes.getEsquema(intEsquema) + Constantes.spBOLETOSPENDIENTESROBOT
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(commandText, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Fecha", srtFecha, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                'ConnNM.AgregarParametro("@p_Hora", strHora, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxrobotBoletoPendiente = New robotBoletoPendientePago.robotBoletoPendiente
                    auxrobotBoletoPendiente.IdSucursal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_SUCURSAL", 0)
                    auxrobotBoletoPendiente.Descripcion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                    auxrobotBoletoPendiente.IdCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CLIENTE", 0)
                    auxrobotBoletoPendiente.NombreCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)

                    auxPromotor = New classPromotor
                    auxPromotor.Codigo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PROMOTOR", Nothing)
                    auxPromotor.NombrePromotor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PROMO_NOMBRE", Nothing)
                    auxPromotor.EmailPromotor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PROMO_EMAIL", Nothing)
                    If auxrobotBoletoPendiente.Promotor Is Nothing Then auxrobotBoletoPendiente.Promotor = New classPromotor
                    auxrobotBoletoPendiente.Promotor = auxPromotor

                    auxrobotBoletoPendiente.IdCondicionPago = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CONDICION_DE_PAGO", "SIN DETERMIAR")
                    auxrobotBoletoPendiente.File = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_FILE", 0)
                    auxrobotBoletoPendiente.PrefijoBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PREFIJO", Nothing)
                    auxrobotBoletoPendiente.NumeroBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_DE_BOLETO", Nothing)
                    auxrobotBoletoPendiente.Gds = ConnNM.LeeColumnasDataReader(objOracleDataReader, "GDS", Nothing)
                    auxrobotBoletoPendiente.PNR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COD_RESERVA", Nothing)
                    auxrobotBoletoPendiente.Ruta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "RUTA", Nothing)
                    auxrobotBoletoPendiente.NoAnular = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NO_ANULAR", "SIN DETERMIAR")
                    auxrobotBoletoPendiente.FechaAltaPTA = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA_DE_ALTA", Nothing)
                    auxrobotBoletoPendiente.MontoVenta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "VENTA", Nothing)
                    auxrobotBoletoPendiente.MontoAplicado = ConnNM.LeeColumnasDataReader(objOracleDataReader, "APLICADO", Nothing)
                    auxrobotBoletoPendiente.MontoPendiente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PENDIENTE", Nothing)

                    If objrobotBoletoPendiente Is Nothing Then objrobotBoletoPendiente = New List(Of robotBoletoPendientePago.robotBoletoPendiente)
                    objrobotBoletoPendiente.Add(auxrobotBoletoPendiente)
                    auxrobotBoletoPendiente = Nothing
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spBOLETOSPENDIENTESROBOT & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Fecha: " & srtFecha.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Hora: " & strHora.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerBoletosPendientesPago" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerBoletosPendientesPago", strCodigoSeguimiento)

                Err.Raise(15, "ObtenerBoletosPendientesPago", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                srtFecha = Nothing
                strHora = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objrobotBoletoPendiente

        End Function
        Public Function ObtenerBoletosPagaOtroDkSABRE(ByVal srtFecha As String, _
                                                      ByVal strHora As String, _
                                                      ByVal strCodigoSeguimiento As String, _
                                                      ByVal intFirmaDB As Integer, _
                                                      ByVal intEsquema As Integer) As List(Of robotBoletoPendientePago.robotBoletoPagoOtroDk)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objrobotBoletoPagoOtroDk As List(Of robotBoletoPendientePago.robotBoletoPagoOtroDk) = Nothing
            Dim auxrobotBoletoPagoOtroDk As robotBoletoPendientePago.robotBoletoPagoOtroDk = Nothing

            Try
                Dim commandText = Constantes.getEsquema(intEsquema) + Constantes.spBOLETOSPAGAOTRODKROBOT
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(commandText, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Fecha", srtFecha, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)

                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxrobotBoletoPagoOtroDk = New robotBoletoPendientePago.robotBoletoPagoOtroDk
                    auxrobotBoletoPagoOtroDk.IdCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CLIENTE", 0)
                    auxrobotBoletoPagoOtroDk.NombreCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)
                    auxrobotBoletoPagoOtroDk.IdCondicionPago = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CONDICION_DE_PAGO", "SIN DETERMIAR")
                    auxrobotBoletoPagoOtroDk.File = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_FILE", 0)
                    auxrobotBoletoPagoOtroDk.PNR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COD_RESERVA", Nothing)
                    auxrobotBoletoPagoOtroDk.NumeroBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_DE_BOLETO", Nothing)
                    auxrobotBoletoPagoOtroDk.Ruta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "RUTA", Nothing)
                    auxrobotBoletoPagoOtroDk.FechaAltaPTA = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA_DE_ALTA", Nothing)
                    auxrobotBoletoPagoOtroDk.MontoPendiente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PENDIENTE", Nothing)
                    auxrobotBoletoPagoOtroDk.MontoPagoOtroDk = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAGO_OTRO_DK", Nothing)
                    auxrobotBoletoPagoOtroDk.OtroDk = ConnNM.LeeColumnasDataReader(objOracleDataReader, "OTRO_DK", 0)
                    auxrobotBoletoPagoOtroDk.NombreOtroDk = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE_CLIENTE", Nothing)

                    If objrobotBoletoPagoOtroDk Is Nothing Then objrobotBoletoPagoOtroDk = New List(Of robotBoletoPendientePago.robotBoletoPagoOtroDk)
                    objrobotBoletoPagoOtroDk.Add(auxrobotBoletoPagoOtroDk)
                    auxrobotBoletoPagoOtroDk = Nothing
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spBOLETOSPAGAOTRODKROBOT & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Fecha: " & srtFecha.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Hora: " & strHora.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerBoletosPagaOtroDk" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerBoletosPagaOtroDk", strCodigoSeguimiento)

                Err.Raise(15, "ObtenerBoletosPagaOtroDk", ex.Message)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                srtFecha = Nothing
                strHora = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objrobotBoletoPagoOtroDk

        End Function

    End Class
End Namespace

