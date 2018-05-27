Imports GDS_NuevoMundoPersistencia
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Namespace ObjetoAccesoDatos
    Partial Public Class classDAO
        Public Function DiferenciaFecha(ByVal strFechaComparar As String, _
                                        ByVal strCodigoSeguimiento As String, _
                                        ByVal intFirmaDB As Integer, _
                                        ByVal intEsquema As Integer) As Integer

            Dim ConnNM As New MyConnectionOracle
            Dim intRespuesta As Integer = -1

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_DIFERENCIA_FECHAS, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("p_Fecha", strFechaComparar, OracleDbType.Varchar2, strFechaComparar.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("p_Respuesta", Nothing, OracleDbType.Int64, 0, ParameterDirection.Output)


                ConnNM._ExecuteReader()

                intRespuesta = CInt(ConnNM.LeeParametros("p_Respuesta", -1))


            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_DIFERENCIA_FECHAS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "DiferenciaFecha" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "DiferenciaFecha", strCodigoSeguimiento)

                Err.Raise(14, "DiferenciaFecha", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return intRespuesta
        End Function
        Public Function ObtenerEjecutivo(ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As List(Of classEjecutivo)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objEjecutivo As classEjecutivo = Nothing
            Dim lstEjecutivo As List(Of classEjecutivo) = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_TURBO_GSA, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)

                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read

                    objEjecutivo = New classEjecutivo
                    objEjecutivo.ID_GSA = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_GSA", Nothing)
                    objEjecutivo.NOMBRE = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)
                    objEjecutivo.DIRECCION = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DIRECCION", Nothing)
                    objEjecutivo.LOCALIDAD = ConnNM.LeeColumnasDataReader(objOracleDataReader, "LOCALIDAD", Nothing)
                    objEjecutivo.TELEFONO = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TELEFONO", Nothing)
                    objEjecutivo.PORCENTAJE_COMISION = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PORCENTAJE_COMISION", 0)
                    objEjecutivo.ID_PROVEEDOR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PROVEEDOR", 0)
                    objEjecutivo.EN_DESUSO = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EN_DESUSO", 0)
                    objEjecutivo.ID_EMPRESA = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_EMPRESA", 0)
                    objEjecutivo.ID_CLIENTE = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CLIENTE", 0)
                    objEjecutivo.ID_TIPO_DOCUMENTO_IDENTIDAD = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_TIPO_DOCUMENTO_IDENTIDAD", Nothing)
                    objEjecutivo.NUM_DOCUMENTO_IDENTIDAD = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUM_DOCUMENTO_IDENTIDAD", Nothing)
                    objEjecutivo.EMAIL = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMAIL", Nothing)

                    If lstEjecutivo Is Nothing Then lstEjecutivo = New List(Of classEjecutivo)
                    lstEjecutivo.Add(objEjecutivo)

                End While

            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spGDS_TURBO_GSA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerEjecutivo" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerEjecutivo", strCodigoSeguimiento)

                Err.Raise(2, "ObtenerEjecutivo", ex.ToString)
            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                objEjecutivo = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return lstEjecutivo

        End Function

        Public Function ObtenerCorreosEnviar(ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As List(Of classEnvioCorreo)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objEnvioCorreo As List(Of classEnvioCorreo) = Nothing
            Dim objEnvioCorreoAux As classEnvioCorreo = Nothing


            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spLISTA_CORREOS_ENVIAR, Constantes.StoredProcedure)
                'ConnNM.AgregarParametro("p_Cliente", intCliente, OracleDbType.Int32, 0, ParameterDirection.Input)
                'ConnNM.AgregarParametro("p_NomBaseDatos", strBaseDatos, OracleDbType.Varchar2, 30, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()



                While objOracleDataReader.Read
                    objEnvioCorreoAux = New classEnvioCorreo

                    objEnvioCorreoAux.IdCorreo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CORREO", Nothing)
                    objEnvioCorreoAux.Remite = ConnNM.LeeColumnasDataReader(objOracleDataReader, "REMITE", Nothing)
                    objEnvioCorreoAux.Destino = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESTINO", Nothing)
                    objEnvioCorreoAux.Mensaje = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MENSAJE", Nothing)
                    objEnvioCorreoAux.Asunto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ASUNTO", Nothing)
                    objEnvioCorreoAux.FechaAlta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA_ALTA", Nothing)
                    objEnvioCorreoAux.ToSend = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TO_SEND", -1)
                    objEnvioCorreoAux.CopiaOculta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COPIA_OCULTA", Nothing)

                    If objEnvioCorreo Is Nothing Then objEnvioCorreo = New List(Of classEnvioCorreo)
                    objEnvioCorreo.Add(objEnvioCorreoAux)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spLISTA_CORREOS_ENVIAR & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerCorreosEnviar" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerCorreosEnviar", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerCorreosEnviar", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                objEnvioCorreoAux = Nothing
            End Try

            Return objEnvioCorreo

        End Function

        Public Function ActualizarCorreosEnviar(ByVal strIdCorreo As String, _
                                                ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As Boolean


            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean

            Try

                ConnNM.Connect(intFirmaDB)

                'If iCnx = 6 Then
                'ConnNM.SP_Command(Constantes.spUPDATE_TRP_DM, Constantes.StoredProcedure)
                'Else

                ConnNM.SP_Command(Constantes.spUP_LISTA_CORREOS, Constantes.StoredProcedure)

                'End If


                ConnNM.AgregarParametro("@p_ID_CORREO", strIdCorreo, OracleDbType.Varchar2, strIdCorreo.Length, ParameterDirection.Input)

                '----------
                bolResultado = ConnNM._InsertExecuteNonQuery()


            Catch ex As Exception
                bolResultado = Nothing

                'If iCnx = 6 Then
                'strLog = "Stored Procedure : " & Constantes.spUPDATE_TRP_DM & vbCrLf
                'Else
                strLog = "Stored Procedure : " & Constantes.spUP_LISTA_CORREOS & vbCrLf
                'End If

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ActualizarCorreosEnviar" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ActualizarCorreosEnviar", strCodigoSeguimiento)

                Err.Raise(13, "ActualizarCorreosEnviar", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strIdCorreo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function

        Public Function ActualizarBoletoPax(ByVal strNumeroBoleto As String, _
                                            ByVal strIdProveedor As String, _
                                            ByVal strIdSucursal As String, _
                                            ByVal strQuienAnula As String, _
                                            ByVal strIdMotivoAnulacion As String, _
                                            ByVal strFcVoidCliente As String, _
                                            ByVal strSinRefacturaXVoid As String, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As Boolean


            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean

            Try

                ConnNM.Connect(intFirmaDB)

                'If iCnx = 6 Then
                'ConnNM.SP_Command(Constantes.spUPDATE_TRP_DM, Constantes.StoredProcedure)
                'Else

                ConnNM.SP_Command(Constantes.spGDS_ACTUALIZA_BOLETO_PAX, Constantes.StoredProcedure)

                'End If


                ConnNM.AgregarParametro("@p_numero_de_boleto", strNumeroBoleto, OracleDbType.Varchar2, strNumeroBoleto.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_id_proveedor", strIdProveedor, OracleDbType.Varchar2, strIdProveedor.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_id_sucursal", strIdSucursal, OracleDbType.Varchar2, strIdSucursal.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_quien_anula", strQuienAnula, OracleDbType.Varchar2, strQuienAnula.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_id_motivo_anulacion", strIdMotivoAnulacion, OracleDbType.Varchar2, strIdMotivoAnulacion.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_fc_void_a_cliente", strFcVoidCliente, OracleDbType.Varchar2, strFcVoidCliente.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_sin_refactura_x_void", strSinRefacturaXVoid, OracleDbType.Varchar2, strSinRefacturaXVoid.Length, ParameterDirection.Input)

                '----------
                bolResultado = ConnNM._InsertExecuteNonQuery()


            Catch ex As Exception
                bolResultado = Nothing

                'If iCnx = 6 Then
                'strLog = "Stored Procedure : " & Constantes.spUPDATE_TRP_DM & vbCrLf
                'Else
                strLog = "Stored Procedure : " & Constantes.spGDS_ACTUALIZA_BOLETO_PAX & vbCrLf
                'End If

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ActualizarBoletoPax" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ActualizarBoletoPax", strCodigoSeguimiento)

                Err.Raise(13, "ActualizarBoletoPax", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strIdProveedor = Nothing
                strIdSucursal = Nothing
                strQuienAnula = Nothing
                strIdMotivoAnulacion = Nothing
                strFcVoidCliente = Nothing
                strSinRefacturaXVoid = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function

        Public Function InsertarTextoFile(ByVal intIdFile As Integer, _
                                          ByVal intIdSucursal As Integer, _
                                          ByVal strRenglonTexto As String, _
                                          ByVal strQuienEscribio As String, _
                                          ByVal intIdEmpresa As Integer, _
                                          ByVal strCodigoSeguimiento As String, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean = False

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_INSERTA_TEXTO_EN_FILE, Constantes.StoredProcedure)

                'If String.IsNullOrEmpty(objTurboDatosPax.IdTipoDocumento) Then
                'ConnNM.AgregarParametro("p_ID_TIPO_DOCUMENTO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                'Else
                ConnNM.AgregarParametro("@p_ID_FILE", intIdFile, OracleDbType.Int32, intIdFile.ToString.Length, ParameterDirection.Input)
                'End If

                'If String.IsNullOrEmpty(objTurboDatosPax.NumeroDocumento) Then
                'ConnNM.AgregarParametro("p_NUM_DOCUMENTO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                'Else
                ConnNM.AgregarParametro("@p_ID_SUCURSAL", intIdSucursal, OracleDbType.Int32, intIdSucursal.ToString.Length, ParameterDirection.Input)
                'End If


                'If String.IsNullOrEmpty(objTurboDatosPax.ApellidoMarterno) Then
                'ConnNM.AgregarParametro("p_APELLIDO_MATERNO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                'Else
                ConnNM.AgregarParametro("@p_RENGLON_DE_TEXTO", strRenglonTexto, OracleDbType.Varchar2, strRenglonTexto.Length, ParameterDirection.Input)
                'End If

                'If String.IsNullOrEmpty(objTurboDatosPax.NombrePasajero) Then
                'ConnNM.AgregarParametro("p_NOMBRES", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                'Else
                ConnNM.AgregarParametro("@p_QUIEN_ESCRIBIO", strQuienEscribio, OracleDbType.Varchar2, strQuienEscribio.Length, ParameterDirection.Input)
                'End If


                'If String.IsNullOrEmpty(objTurboDatosPax.TelefonoCasa) Then
                'ConnNM.AgregarParametro("p_TELEFONO_CASA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                'Else
                ConnNM.AgregarParametro("@p_ID_EMPRESA", intIdEmpresa, OracleDbType.Int32, intIdEmpresa.ToString.Length, ParameterDirection.Input)
                'End If

                'If String.IsNullOrEmpty(objTurboDatosPax.TelefonoCelular) Then
                'ConnNM.AgregarParametro("p_TELEFONO_CELULAR", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                'Else
                'ConnNM.AgregarParametro("p_TELEFONO_CELULAR", objTurboDatosPax.TelefonoCelular, OracleDbType.Varchar2, objTurboDatosPax.TelefonoCelular.Length, ParameterDirection.Input)
                'End If


                bolResultado = ConnNM._InsertExecuteNonQuery()
                '----------

            Catch ex As Exception

                bolResultado = False
                strLog = "Stored Procedure : " & Constantes.spGDS_INSERTA_TEXTO_EN_FILE & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "InsertarTextoFile" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "InsertarTextoFile", strCodigoSeguimiento)

                Err.Raise(13, "InsertarTextoFile", ex.ToString)


            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function

        Public Function ObtenerDatosFacturacion(ByVal strNumeroBoleto As String, _
                                                ByVal strIdProveedor As String, _
                                                ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As List(Of classBoletoPax)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objBoletoPax As List(Of classBoletoPax) = Nothing
            Dim objBoletoPaxAux As classBoletoPax = Nothing


            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_LISTAR_BOLETO_PAX, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_NUMERO_DE_BOLETO", strNumeroBoleto, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_ID_PROVEEDOR", strIdProveedor, OracleDbType.Varchar2, 0, ParameterDirection.Input)


                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()



                While objOracleDataReader.Read
                    objBoletoPaxAux = New classBoletoPax

                    objBoletoPaxAux.FechaEmision = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA_EMISION", Nothing)
                    objBoletoPaxAux.IdFacturaCabeza = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_FACTURA_CABEZA", Nothing)
                    objBoletoPaxAux.IdTipoComprobante = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_TIPO_DE_COMPROBANTE", Nothing)
                    objBoletoPaxAux.Emitido = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMITIDO", -1)
                    objBoletoPaxAux.IdFile = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_FILE", Nothing)
                    objBoletoPaxAux.MarcaVoid = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MARCA_VOID", -1)
                    objBoletoPaxAux.NumeroSerie = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_SERIE1", -1)
                    objBoletoPaxAux.IdSucursal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_SUCURSAL", -1)
                    objBoletoPaxAux.CodigoPNR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COD_RESERVA", Nothing)
                    objBoletoPaxAux.IdCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CLIENTE", Nothing)
                    objBoletoPaxAux.IdPaxReserva = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PAX_RESERVA", -1)
                    objBoletoPaxAux.IdIata = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_IATA", Nothing)
                    objBoletoPaxAux.BoletoTcAsociado = ConnNM.LeeColumnasDataReader(objOracleDataReader, "BOLETO_TC_ASOCIADO", Nothing)


                    If objBoletoPax Is Nothing Then objBoletoPax = New List(Of classBoletoPax)
                    objBoletoPax.Add(objBoletoPaxAux)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_LISTAR_BOLETO_PAX & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDatosFacturacion" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerDatosFacturacion", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerDatosFacturacion", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                objBoletoPaxAux = Nothing
            End Try

            Return objBoletoPax

        End Function

        Public Function ObtenerMotivosVoid(ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As List(Of classMotivoVoid)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objMotivoVoid As List(Of classMotivoVoid) = Nothing
            Dim objMotivoVoidAux As classMotivoVoid = Nothing


            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_LISTAR_MOTIVOS_VOID, Constantes.StoredProcedure)
                'ConnNM.AgregarParametro("p_Cliente", intCliente, OracleDbType.Int32, 0, ParameterDirection.Input)
                'ConnNM.AgregarParametro("p_NomBaseDatos", strBaseDatos, OracleDbType.Varchar2, 30, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()



                While objOracleDataReader.Read
                    objMotivoVoidAux = New classMotivoVoid

                    objMotivoVoidAux.IdMotivo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_MOTIVO", Nothing)
                    objMotivoVoidAux.Motivo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)


                    If objMotivoVoid Is Nothing Then objMotivoVoid = New List(Of classMotivoVoid)
                    objMotivoVoid.Add(objMotivoVoidAux)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_LISTAR_MOTIVOS_VOID & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerMotivosVoid" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerMotivosVoid", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerMotivosVoid", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                objMotivoVoidAux = Nothing
            End Try

            Return objMotivoVoid

        End Function

        Public Function ObtenerBoletosXConfirmacion(ByVal strCodigoPNR As String, _
                                                    ByVal strCodigoSeguimiento As String, _
                                                    ByVal intFirmaDB As Integer, _
                                                    ByVal intEsquema As Integer) As List(Of classBoletosXConfirmacion)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objBoletosXConfirmacion As List(Of classBoletosXConfirmacion) = Nothing
            Dim objBoletosXConfirmacionAux As classBoletosXConfirmacion = Nothing


            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_BOLETOS_X_CONFIRMACION, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_COD_RESERVA", strCodigoPNR, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                'ConnNM.AgregarParametro("p_NomBaseDatos", strBaseDatos, OracleDbType.Varchar2, 30, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()



                While objOracleDataReader.Read
                    objBoletosXConfirmacionAux = New classBoletosXConfirmacion

                    objBoletosXConfirmacionAux.IdEmitido = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_EMITIDO", -1)
                    objBoletosXConfirmacionAux.IdFirmaCreaPnr = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_FIRMA_CREA_PNR", Nothing)
                    objBoletosXConfirmacionAux.IdReferencia = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_REFERENCIA", Nothing)
                    objBoletosXConfirmacionAux.IdSecuencia = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_SECUENCIA", Nothing)


                    If objBoletosXConfirmacion Is Nothing Then objBoletosXConfirmacion = New List(Of classBoletosXConfirmacion)
                    objBoletosXConfirmacion.Add(objBoletosXConfirmacionAux)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_BOLETOS_X_CONFIRMACION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerBoletosXConfirmacion" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerBoletosXConfirmacion", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerBoletosXConfirmacion", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                objBoletosXConfirmacionAux = Nothing
            End Try

            Return objBoletosXConfirmacion

        End Function

        Public Function ActualizarBoletoPaxVoidEMD(ByVal strNumeroBoleto As String, _
                                                   ByVal strIdProveedor As String, _
                                                   ByVal strIdSucursal As String, _
                                                   ByVal strQuienAnula As String, _
                                                   ByVal strIdMotivoAnulacion As String, _
                                                   ByVal strFcVoidCliente As String, _
                                                   ByVal strCodigoSeguimiento As String, _
                                                   ByVal intFirmaDB As Integer, _
                                                   ByVal intEsquema As Integer) As Boolean


            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean

            Try

                ConnNM.Connect(intFirmaDB)

                'If iCnx = 6 Then
                'ConnNM.SP_Command(Constantes.spUPDATE_TRP_DM, Constantes.StoredProcedure)
                'Else

                ConnNM.SP_Command(Constantes.spGDS_ACTUALIZA_BOLETO_PAX_EMD, Constantes.StoredProcedure)

                'End If


                ConnNM.AgregarParametro("@p_numero_de_boleto", strNumeroBoleto, OracleDbType.Varchar2, strNumeroBoleto.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_id_proveedor", strIdProveedor, OracleDbType.Varchar2, strIdProveedor.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_id_sucursal", strIdSucursal, OracleDbType.Varchar2, strIdSucursal.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_quien_anula", strQuienAnula, OracleDbType.Varchar2, strQuienAnula.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_id_motivo_anulacion", strIdMotivoAnulacion, OracleDbType.Varchar2, strIdMotivoAnulacion.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_fc_void_a_cliente", strFcVoidCliente, OracleDbType.Varchar2, strFcVoidCliente.Length, ParameterDirection.Input)

                '----------
                bolResultado = ConnNM._InsertExecuteNonQuery()


            Catch ex As Exception
                bolResultado = Nothing

                'If iCnx = 6 Then
                'strLog = "Stored Procedure : " & Constantes.spUPDATE_TRP_DM & vbCrLf
                'Else
                strLog = "Stored Procedure : " & Constantes.spGDS_ACTUALIZA_BOLETO_PAX & vbCrLf
                'End If

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ActualizarBoletoPaxVoidEMD" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.Message & vbCrLf
                objEscribeLog.WriteLog(strLog, "ActualizarBoletoPaxVoidEMD", strCodigoSeguimiento)

                Err.Raise(13, "ActualizarBoletoPaxVoidEMD", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strIdProveedor = Nothing
                strIdSucursal = Nothing
                strQuienAnula = Nothing
                strIdMotivoAnulacion = Nothing
                strFcVoidCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function

        Public Function ObtenerTurboPassengerReceipt(ByVal strTicketNumber As String, _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As classTurboPassengerRecipt

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objTurboPassengerReceipt As classTurboPassengerRecipt = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_LISTAR_TURBO_PASSENGER, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_TICKET_NUMBER", strTicketNumber, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                'ConnNM.AgregarParametro("p_NomBaseDatos", strBaseDatos, OracleDbType.Varchar2, 30, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()



                While objOracleDataReader.Read

                    objTurboPassengerReceipt = New classTurboPassengerRecipt

                    objTurboPassengerReceipt.Ticket_Number = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TICKET_NUMBER", -1)
                    objTurboPassengerReceipt.Pnr_Code = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PNR_CODE", Nothing)
                    objTurboPassengerReceipt.Dk_Number = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DK_NUMBER", -1)
                    objTurboPassengerReceipt.Ruc_Number = ConnNM.LeeColumnasDataReader(objOracleDataReader, "RUC_NUMBER", Nothing)
                    objTurboPassengerReceipt.Print_Flag = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PRINT_FLAG", -1)
                    objTurboPassengerReceipt.Pcc = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PCC", Nothing)
                    objTurboPassengerReceipt.Counter_Ta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COUNTER_TA", Nothing)
                    objTurboPassengerReceipt.Fecha_Alta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA_ALTA", Nothing)
                    objTurboPassengerReceipt.Printed_Flag = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PRINTED_FLAG", -1)
                    objTurboPassengerReceipt.Cuerpo_Documento = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CUERPO_DOCUMENTO", Nothing)
                    objTurboPassengerReceipt.Pasajero_Nombre = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PASAJERO_NOMBRE", Nothing)
                    objTurboPassengerReceipt.Pasajero_Apellido = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PASAJERO_APELLIDO", Nothing)
                    objTurboPassengerReceipt.Id_Header = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_HEADER", -1)
                    objTurboPassengerReceipt.Counter_Email = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COUNTER_EMAIL", Nothing)
                    objTurboPassengerReceipt.Email_Flag = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMAIL_FLAG", Nothing)
                    objTurboPassengerReceipt.Mailed_Flag = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MAILED_FLAG", Nothing)
                    objTurboPassengerReceipt.Itinerario = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ITINERARIO", Nothing)
                    objTurboPassengerReceipt.Venta_Personal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "VENTA_PERSONAL", -1)
                    objTurboPassengerReceipt.Freq_Travel = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FREQ_TRAVEL", Nothing)
                    objTurboPassengerReceipt.Cod_Aerolinea = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COD_AEROLINEA", Nothing)
                    objTurboPassengerReceipt.Ruta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "RUTA", Nothing)


                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_LISTAR_TURBO_PASSENGER & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerTurboPassengerReceipt" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerTurboPassengerReceipt", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerTurboPassengerReceipt", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objTurboPassengerReceipt

        End Function

        Public Function ObtenerTurboCcChargeForm(ByVal strTicketNumber As String, _
                                                 ByVal strCodigoSeguimiento As String, _
                                                 ByVal intFirmaDB As Integer, _
                                                 ByVal intEsquema As Integer) As classTurboCcChargeForm

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objTurboCcChargeForm As classTurboCcChargeForm = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_LISTAR_TURBO_CC_CHARGE, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_TICKET_NUMBER", strTicketNumber, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                'ConnNM.AgregarParametro("p_NomBaseDatos", strBaseDatos, OracleDbType.Varchar2, 30, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()



                While objOracleDataReader.Read

                    objTurboCcChargeForm = New classTurboCcChargeForm

                    objTurboCcChargeForm.Pnr_Code = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PNR_CODE", Nothing)
                    objTurboCcChargeForm.Counter_Ta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COUNTER_TA", Nothing)
                    objTurboCcChargeForm.Fecha_Alta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA_ALTA", Nothing)
                    objTurboCcChargeForm.Print_Flag = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PRINT_FLAG", -1)
                    objTurboCcChargeForm.Counter_Email = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CORREO_ELECTRONICO", Nothing)
                    objTurboCcChargeForm.Ticket_Number = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TICKET_NUMBER", -1)
                    objTurboCcChargeForm.Cuerpo_Documento = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CUERPO_DOCUMENTO", Nothing)
                    objTurboCcChargeForm.Cuerpo_Correo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CUERPO_CORREO", Nothing)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_LISTAR_TURBO_CC_CHARGE & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerTurboCcChargeForm" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerTurboCcChargeForm", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerTurboCcChargeForm", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objTurboCcChargeForm

        End Function

        Public Function ObtenerDatosClienteEmpresa(ByVal strTipo_Documento As String, _
                                                   ByVal strDocumento As String, _
                                                   ByVal strCodigoSeguimiento As String, _
                                                   ByVal intFirmaDB As Integer, _
                                                   ByVal intEsquema As Integer) As classCliente

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objCliente As classCliente = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_DATOS_CLIENTE_EMPRESA, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_ID_TIPO_DOCUMENTO_IDENTIDAD", strTipo_Documento, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_DOCUMENTO", strDocumento, OracleDbType.Varchar2, 0, ParameterDirection.Input)


                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()



                While objOracleDataReader.Read

                    objCliente = New classCliente

                    objCliente.RazonSocial = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)
                    objCliente.Direccion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DIRECCION", Nothing)


                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_DATOS_CLIENTE_EMPRESA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDatosClienteEmpresa" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerDatosClienteEmpresa", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerDatosClienteEmpresa", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objCliente

        End Function

        Public Function ObtenerReporteConfirmaciones(ByVal strFechaInicio As String, _
                                                        ByVal strFechaFinal As String, _
                                                        ByVal strCodigoSeguimiento As String, _
                                                        ByVal intFirmaDB As Integer, _
                                                        ByVal intEsquema As Integer) As List(Of classReporteConfirmaciones)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objReporteConfirmaciones As List(Of classReporteConfirmaciones) = Nothing
            Dim objReporteConfirmacionesAux As classReporteConfirmaciones = Nothing


            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_REPORTE_CONFIRMACIONES, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_FECHA_CONF_INICIO", strFechaInicio, OracleDbType.Varchar2, strFechaInicio.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_FECHA_CONF_FINAL", strFechaFinal, OracleDbType.Varchar2, strFechaFinal.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()



                While objOracleDataReader.Read
                    objReporteConfirmacionesAux = New classReporteConfirmaciones

                    objReporteConfirmacionesAux.FechaConf = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA_CONF", Nothing)
                    objReporteConfirmacionesAux.CodigoPnr = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COD_RESERVA", Nothing)
                    objReporteConfirmacionesAux.Pcc = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PCC", Nothing)
                    objReporteConfirmacionesAux.Gds = ConnNM.LeeColumnasDataReader(objOracleDataReader, "GDS", Nothing)
                    objReporteConfirmacionesAux.NumeroBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_DE_BOLETO", Nothing)
                    objReporteConfirmacionesAux.NombrePasajero = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)
                    objReporteConfirmacionesAux.AgenteCrea = ConnNM.LeeColumnasDataReader(objOracleDataReader, "AGENTE_CREA", Nothing)
                    objReporteConfirmacionesAux.AgenteSolicita = ConnNM.LeeColumnasDataReader(objOracleDataReader, "AGENTE_SOLICITA", Nothing)
                    objReporteConfirmacionesAux.AgenteConfirma = ConnNM.LeeColumnasDataReader(objOracleDataReader, "AGENTE_CONFIRMA", Nothing)
                    objReporteConfirmacionesAux.Sucursal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "SUCURSAL", Nothing)
                    objReporteConfirmacionesAux.ImporteOPT = ConnNM.LeeColumnasDataReader(objOracleDataReader, "IMPORTE_OPT", Nothing)

                    If objReporteConfirmaciones Is Nothing Then objReporteConfirmaciones = New List(Of classReporteConfirmaciones)
                    objReporteConfirmaciones.Add(objReporteConfirmacionesAux)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_REPORTE_CONFIRMACIONES & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerReporteConfirmaciones" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, strCodigoSeguimiento, intFirmaDB)

                Err.Raise(10, "ObtenerReporteConfirmaciones", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                objReporteConfirmacionesAux = Nothing
            End Try

            Return objReporteConfirmaciones

        End Function
        Public Function ObtenerReporteConfirmacionesDM(ByVal strFechaInicio As String, _
                                                        ByVal strFechaFinal As String, _
                                                        ByVal strCodigoSeguimiento As String, _
                                                        ByVal intFirmaDB As Integer, _
                                                        ByVal intEsquema As Integer) As List(Of classReporteConfirmaciones)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objReporteConfirmaciones As List(Of classReporteConfirmaciones) = Nothing
            Dim objReporteConfirmacionesAux As classReporteConfirmaciones = Nothing


            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_REPORTE_CONFIRMACIONES_DM, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_FECHA_CONF_INICIO", strFechaInicio, OracleDbType.Varchar2, strFechaInicio.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_FECHA_CONF_FINAL", strFechaFinal, OracleDbType.Varchar2, strFechaFinal.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()



                While objOracleDataReader.Read
                    objReporteConfirmacionesAux = New classReporteConfirmaciones

                    objReporteConfirmacionesAux.FechaConf = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA_CONF", Nothing)
                    objReporteConfirmacionesAux.CodigoPnr = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COD_RESERVA", Nothing)
                    objReporteConfirmacionesAux.Gds = ConnNM.LeeColumnasDataReader(objOracleDataReader, "GDS", Nothing)
                    objReporteConfirmacionesAux.Pcc = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PCC", Nothing)
                    objReporteConfirmacionesAux.NumeroBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_DE_BOLETO", Nothing)
                    objReporteConfirmacionesAux.NombrePasajero = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)
                    objReporteConfirmacionesAux.AgenteCrea = ConnNM.LeeColumnasDataReader(objOracleDataReader, "AGENTE_CREA", Nothing)
                    objReporteConfirmacionesAux.AgenteSolicita = ConnNM.LeeColumnasDataReader(objOracleDataReader, "AGENTE_SOLICITA", Nothing)
                    objReporteConfirmacionesAux.AgenteConfirma = ConnNM.LeeColumnasDataReader(objOracleDataReader, "AGENTE_CONFIRMA", Nothing)
                    objReporteConfirmacionesAux.Sucursal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "SUCURSAL_EMITE", Nothing)
                    'objReporteConfirmacionesAux.ImporteOPT = ConnNM.LeeColumnasDataReader(objOracleDataReader, "IMPORTE_OPT", Nothing)

                    If objReporteConfirmaciones Is Nothing Then objReporteConfirmaciones = New List(Of classReporteConfirmaciones)
                    objReporteConfirmaciones.Add(objReporteConfirmacionesAux)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_REPORTE_CONFIRMACIONES_DM & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerReporteConfirmacionesDM" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, strCodigoSeguimiento, intFirmaDB)

                Err.Raise(10, "ObtenerReporteConfirmacionesDM", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                objReporteConfirmacionesAux = Nothing
            End Try

            Return objReporteConfirmaciones

        End Function


        Public Function InsertarEnvioMensajesEA(ByVal strDesde As String, _
                                                ByVal strPara As String, _
                                                ByVal strAsunto As String, _
                                                ByVal strMensaje As String, _
                                                ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean = False

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_INSERTA_ENVIO_MENSAJES_EA, Constantes.StoredProcedure)

                ConnNM.AgregarParametro("@p_DESDE", strDesde, OracleDbType.Varchar2, strDesde.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_PARA", strPara, OracleDbType.Varchar2, strPara.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_ASUNTO", strAsunto, OracleDbType.Varchar2, strAsunto.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_MENSAJE", strMensaje, OracleDbType.Varchar2, strMensaje.Length, ParameterDirection.Input)



                bolResultado = ConnNM._InsertExecuteNonQuery()
                '----------

            Catch ex As Exception

                bolResultado = False
                strLog = "Stored Procedure : " & Constantes.spGDS_INSERTA_ENVIO_MENSAJES_EA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "InsertarEnvioMensajesEA" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "InsertarEnvioMensajesEA", strCodigoSeguimiento)

                Err.Raise(13, "InsertarEnvioMensajesEA", ex.ToString)


            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function

        Public Function ObtenerDatosFacturaComision(ByVal strNumeroBoleto As String, _
                                                    ByVal strCodigoSeguimiento As String, _
                                                    ByVal intFirmaDB As Integer, _
                                                    ByVal intEsquema As Integer) As classFacturaComision

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objFacturaComision As classFacturaComision = Nothing


            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_PAGO_PENDIENTE, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Boleto", strNumeroBoleto, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)

                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objFacturaComision = New classFacturaComision

                    objFacturaComision.Total = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TOTAL", Nothing)
                    objFacturaComision.AfectoOtroDK = ConnNM.LeeColumnasDataReader(objOracleDataReader, "AFECTO_OTRODK", Nothing)
                    objFacturaComision.AfectoDK = ConnNM.LeeColumnasDataReader(objOracleDataReader, "AFECTO_DK", Nothing)
                End While

                'While objOracleDataReader.Read


                'End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_PAGO_PENDIENTE & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDatosFacturaComision" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLogRobotAnulacion(strLog, "ObtenerDatosFacturaComision", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerDatosFacturaComision", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objFacturaComision
        End Function

        Public Function ObtenerDatosAutorizacionNoVoid(ByVal intCliente As Integer, _
                                                       ByVal strCodReserva As String, _
                                                       ByVal strCodigoSeguimiento As String, _
                                                       ByVal intFirmaDB As Integer, _
                                                       ByVal intEsquema As Integer) As String
            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim strNoAnular As String = String.Empty

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_AUTORIZACION_VOID, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_IDCliente", intCliente, OracleDbType.Int32, intCliente.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_CodReserva", strCodReserva, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)

                objOracleDataReader = ConnNM._ExecuteReader()
                While objOracleDataReader.Read
                    strNoAnular = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NO_ANULAR", Nothing)
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_AUTORIZACION_VOID & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDatosAutorizacionNoVoid" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLogRobotAnulacion(strLog, "ObtenerDatosAutorizacionNoVoid", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerDatosAutorizacionNoVoid", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return strNoAnular
        End Function

        Public Function ObtenerDatosAgenciaPromotorVendedor(ByVal strNumeroBoleto As String, _
                                                            ByVal strCodigoSeguimiento As String, _
                                                            ByVal intFirmaDB As Integer, _
                                                            ByVal intEsquema As Integer) As classDatosAgente
            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objDatosAgente As classDatosAgente = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_CORREO_AGENTE_PROMOTOR, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Boleto", strNumeroBoleto, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)

                objOracleDataReader = ConnNM._ExecuteReader()
                While objOracleDataReader.Read
                    objDatosAgente = New classDatosAgente

                    objDatosAgente.CorreoPromotor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMAIL_PROM", Nothing)
                    objDatosAgente.CorreoAgente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMAIL_AGT", Nothing)
                    objDatosAgente.Cliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CLIENTE", Nothing)
                    objDatosAgente.CorreoCaja = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMAIL_CAJA", Nothing)
                    objDatosAgente.CorreoVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMAIL_VEN", Nothing)
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_CORREO_AGENTE_PROMOTOR & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDatosAgenciaPromotorVendedor" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLogRobotAnulacion(strLog, "ObtenerDatosAgenciaPromotorVendedor", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerDatosAgenciaPromotorVendedor", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objDatosAgente
        End Function
    End Class
End Namespace