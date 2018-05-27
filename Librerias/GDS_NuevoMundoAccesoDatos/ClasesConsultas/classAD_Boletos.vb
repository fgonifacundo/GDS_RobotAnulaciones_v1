Imports GDS_NuevoMundoPersistencia
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Namespace ObjetoAccesoDatos
    Partial Public Class classDAO
        Public Function ObtenerBoletosPendientesPago(ByVal srtFecha As String, _
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

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spBOLETOSPENDIENTES, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Fecha", srtFecha, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Hora", strHora, OracleDbType.Varchar2, 0, ParameterDirection.Input)
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

                strLog = "Stored Procedure : " & Constantes.spBOLETOSPENDIENTES & vbCrLf
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
        Public Function ObtenerBoletosPagaOtroDk(ByVal srtFecha As String, _
                                                 ByVal strHora As String, _
                                                 ByVal strCodigoSeguimiento As String, _
                                                 ByVal intFirmaDB As Integer, _
                                                 ByVal intEsquema As Integer) As List(Of robotBoletoPendientePago.robotBoletoPagoOtroDk)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objrobotBoletoPagoOtroDk As List(Of robotBoletoPendientePago.robotBoletoPagoOtroDk) = Nothing
            Dim auxrobotBoletoPagoOtroDk As robotBoletoPendientePago.robotBoletoPagoOtroDk = Nothing


            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spBOLETOSPAGAOTRODK, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Fecha", srtFecha, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Hora", strHora, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                '----------
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

                strLog = "Stored Procedure : " & Constantes.spBOLETOSPAGAOTRODK & vbCrLf
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
        Public Function ObtenerEMDS_en_PTA(ByVal srtFecha As String, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As List(Of robotBoletoPendientePago.robotBoletoPendiente)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objrobotBoletoPendiente As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim auxrobotBoletoPendiente As robotBoletoPendientePago.robotBoletoPendiente = Nothing
            Dim auxDatosAgente As classDatosAgente = Nothing
            Dim auxPromotor As classPromotor = Nothing


            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spEMDS_FACTURADOS, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Fecha", srtFecha, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxrobotBoletoPendiente = New robotBoletoPendientePago.robotBoletoPendiente
                    auxrobotBoletoPendiente.IdSucursal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_SUCURSAL", 0)
                    auxrobotBoletoPendiente.Descripcion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                    auxrobotBoletoPendiente.IdCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CLIENTE", 0)
                    auxrobotBoletoPendiente.NombreCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)

                    auxrobotBoletoPendiente.IdCondicionPago = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CONDICION_DE_PAGO", "SIN DETERMIAR")
                    auxrobotBoletoPendiente.File = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_FILE", 0)

                    auxrobotBoletoPendiente.IdVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_VENDEDOR", Nothing)

                    auxDatosAgente = New classDatosAgente
                    auxDatosAgente.NombreAgente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE_VENDEDOR", Nothing)
                    auxDatosAgente.CorreoAgente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CORREO_ELECTRONICO", Nothing)
                    auxDatosAgente.CorreoJefe = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CORREO_JEFE", Nothing)

                    auxrobotBoletoPendiente.Vendedor = New classDatosAgente
                    auxrobotBoletoPendiente.Vendedor = auxDatosAgente

                    auxrobotBoletoPendiente.PNR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COD_RESERVA", Nothing)
                    auxrobotBoletoPendiente.PrefijoBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PREFIJO", Nothing)
                    auxrobotBoletoPendiente.NumeroBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_DE_BOLETO", Nothing)
                    auxrobotBoletoPendiente.Gds = ConnNM.LeeColumnasDataReader(objOracleDataReader, "GDS", Nothing)

                    auxrobotBoletoPendiente.Ruta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "RUTA", Nothing)
                    auxrobotBoletoPendiente.Estado = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ESTADO", Nothing)
                    auxrobotBoletoPendiente.ID_FacturaCabeza = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_FACTURA_CABEZA", Nothing)

                    auxrobotBoletoPendiente.FechaAltaPTA = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA_DE_ALTA", Nothing)

                    If objrobotBoletoPendiente Is Nothing Then objrobotBoletoPendiente = New List(Of robotBoletoPendientePago.robotBoletoPendiente)
                    objrobotBoletoPendiente.Add(auxrobotBoletoPendiente)
                    auxrobotBoletoPendiente = Nothing
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spEMDS_FACTURADOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Fecha: " & srtFecha.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerEMDS_en_PTA" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerEMDS_en_PTA", strCodigoSeguimiento)

                Err.Raise(15, "ObtenerEMDS_en_PTA", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                srtFecha = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objrobotBoletoPendiente

        End Function
        Public Function ObtenerNo_Imprime_Cuentas(ByVal srtFirma As String, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As Integer

            Dim ConnNM As New MyConnectionOracle
            Dim intRespuesta As Integer = 0
            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spNO_PRINTER_CUENTAS, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@id_cliente", srtFirma, OracleDbType.Varchar2, srtFirma.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Respuesta", 0, OracleDbType.Double, 0, ParameterDirection.Output)

                ConnNM._ExecuteReader()

                intRespuesta = ConnNM.LeeParametros("@p_Respuesta", Convert.ToString(0))

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spCREDITO_DISPONIBLE & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerNo_Imprime_Cuentas" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerNo_Imprime_Cuentas", strCodigoSeguimiento)

                Err.Raise(1, "ObtenerNo_Imprime_Cuentas", ex.ToString)

            Finally
                ConnNM.Disconnect()
                ConnNM = Nothing
                srtFirma = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return intRespuesta

        End Function

        Public Function ObtenerDatosFile(ByVal srtPNR As String, _
                                         ByVal strBoleto As String, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As classFile


            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objFile As classFile = Nothing
            Dim objGrupoInterno As classGrupoInterno = Nothing


            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spFILE_BOLETO, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Pnr", srtPNR, OracleDbType.Varchar2, srtPNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_NumBoleto", strBoleto, OracleDbType.Varchar2, strBoleto.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objFile = New classFile

                    objFile.Sucursal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "SUCURSAL", 0)
                    objFile.DK = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DK", 0)
                    objFile.NumeroFile = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_FILE", 0)
                    objFile.PNR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PNR", Nothing)
                    objFile.Prefijo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PREFIJO", 0)
                    objFile.Boleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "BOLETO", 0)
                    objFile.Estado = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ESTADO", Nothing)
                    objFile.Stock = ConnNM.LeeColumnasDataReader(objOracleDataReader, "STOCK", Nothing)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spBOLETOSPENDIENTES & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "PNR: " & srtPNR.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Boleto: " & strBoleto.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDatosFile" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerDatosFile", strCodigoSeguimiento)

                Err.Raise(15, "ObtenerDatosFile", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                srtPNR = Nothing
                strBoleto = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objFile

        End Function

        Public Function ObtenerBoletos_DINNER_en_PTA(ByVal srtFecha As String, _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As List(Of robotBoletoPendientePago.robotBoletoPendiente)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objrobotBoletoPendiente As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim auxrobotBoletoPendiente As robotBoletoPendientePago.robotBoletoPendiente = Nothing
            Dim auxDatosAgente As classDatosAgente = Nothing
            Dim auxPromotor As classPromotor = Nothing


            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_BOLETOS_FACTURADOS_DINNERS, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Fecha", srtFecha, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxrobotBoletoPendiente = New robotBoletoPendientePago.robotBoletoPendiente
                    auxrobotBoletoPendiente.IdSucursal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_SUCURSAL", 0)
                    auxrobotBoletoPendiente.Descripcion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                    auxrobotBoletoPendiente.IdCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CLIENTE", 0)
                    auxrobotBoletoPendiente.NombreCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)

                    auxrobotBoletoPendiente.IdCondicionPago = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CONDICION_DE_PAGO", "SIN DETERMIAR")
                    auxrobotBoletoPendiente.File = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_FILE", 0)

                    auxrobotBoletoPendiente.IdVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_VENDEDOR", Nothing)

                    auxDatosAgente = New classDatosAgente
                    auxDatosAgente.NombreAgente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE_VENDEDOR", Nothing)
                    auxDatosAgente.CorreoAgente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CORREO_ELECTRONICO", Nothing)
                    auxDatosAgente.CorreoJefe = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CORREO_JEFE", Nothing)

                    auxrobotBoletoPendiente.Vendedor = New classDatosAgente
                    auxrobotBoletoPendiente.Vendedor = auxDatosAgente

                    auxrobotBoletoPendiente.PNR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COD_RESERVA", Nothing)
                    auxrobotBoletoPendiente.PrefijoBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PREFIJO", Nothing)
                    auxrobotBoletoPendiente.NumeroBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_DE_BOLETO", Nothing)
                    auxrobotBoletoPendiente.Gds = ConnNM.LeeColumnasDataReader(objOracleDataReader, "GDS", Nothing)

                    auxrobotBoletoPendiente.Ruta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "RUTA", Nothing)
                    auxrobotBoletoPendiente.Estado = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ESTADO", Nothing)
                    auxrobotBoletoPendiente.ID_FacturaCabeza = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_FACTURA_CABEZA", Nothing)

                    auxrobotBoletoPendiente.FechaAltaPTA = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA_DE_ALTA", Nothing)

                    If objrobotBoletoPendiente Is Nothing Then objrobotBoletoPendiente = New List(Of robotBoletoPendientePago.robotBoletoPendiente)
                    objrobotBoletoPendiente.Add(auxrobotBoletoPendiente)
                    auxrobotBoletoPendiente = Nothing
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDS_BOLETOS_FACTURADOS_DINNERS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Fecha: " & srtFecha.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerBoletos_DINNER_en_PTA" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerBoletos_DINNER_en_PTA", strCodigoSeguimiento)

                Err.Raise(15, "ObtenerBoletos_DINNER_en_PTA", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                srtFecha = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objrobotBoletoPendiente

        End Function

        Public Function VerificaDobleInterfaceDestinos(ByVal srtCodigoReserva As String, _
                                                             ByVal srtNumeroBoleto As String, _
                                                             ByVal strCodigoSeguimiento As String, _
                                                             ByVal intFirmaDB As Integer, _
                                                             ByVal intEsquema As Integer) As String

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim strID_Cliente As String = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_VERIFICA_DOBLE_INTERFACE_DESTINOS, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_PNR", srtCodigoReserva, OracleDbType.Varchar2, srtCodigoReserva.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Boleto", srtNumeroBoleto, OracleDbType.Varchar2, srtNumeroBoleto.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    strID_Cliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CLIENTE", 0)
                End While

            Catch ex As Exception

                strID_Cliente = Nothing

                strLog = "Stored Procedure : " & Constantes.spGDS_VERIFICA_DOBLE_INTERFACE_DESTINOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "VerificaDobleInterfaceDestinos" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "VerificaDobleInterfaceDestinos", strCodigoSeguimiento)

                Err.Raise(15, "VerificaDobleInterfaceDestinos", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return strID_Cliente

        End Function

        Public Function ObtenerBoletoFacturadoPtaDestinos(ByVal srtNumeroBoleto As String, _
                                                           ByVal strCodigoSeguimiento As String, _
                                                           ByVal intFirmaDB As Integer, _
                                                           ByVal intEsquema As Integer) As classBoletoEmitido

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            'Dim strID_Cliente As String = Nothing
            Dim objBoletoEmitido As classBoletoEmitido = Nothing
            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_BOLETO_FACTURADO_PTADESTINOS, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Boleto", srtNumeroBoleto, OracleDbType.Varchar2, srtNumeroBoleto.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objBoletoEmitido = New classBoletoEmitido
                    objBoletoEmitido.NumeroBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_DE_BOLETO", 0)
                    objBoletoEmitido.IDCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CLIENTE", 0)
                    objBoletoEmitido.IDProveedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PROVEEDOR", 0)
                    objBoletoEmitido.IDSucursal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_SUCURSAL", 0)
                    objBoletoEmitido.IDVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_VENDEDOR", 0)
                End While

            Catch ex As Exception
                objBoletoEmitido = Nothing

                strLog = "Stored Procedure : " & Constantes.spGDS_BOLETO_FACTURADO_PTADESTINOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerBoletoFacturado" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLogRobotAnulacion(strLog, "ObtenerBoletoFacturado", strCodigoSeguimiento)

                Err.Raise(15, "ObtenerBoletoFacturado", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objBoletoEmitido
        End Function

        Public Function ObtenerBoletoFacturado(ByVal srtNumeroBoleto As String, _
                                               ByVal strCodigoSeguimiento As String, _
                                               ByVal intFirmaDB As Integer, _
                                               ByVal intEsquema As Integer) As classBoletoEmitido

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            'Dim strID_Cliente As String = Nothing
            Dim objBoletoEmitido As classBoletoEmitido = Nothing
            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_OBTENER_BOLETO_FACTURADO, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Boleto", srtNumeroBoleto, OracleDbType.Varchar2, srtNumeroBoleto.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objBoletoEmitido = New classBoletoEmitido
                    objBoletoEmitido.NumeroBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_DE_BOLETO", 0)
                    objBoletoEmitido.IDCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CLIENTE", 0)
                    objBoletoEmitido.IDProveedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PROVEEDOR", 0)
                    objBoletoEmitido.IDSucursal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_SUCURSAL", 0)
                    objBoletoEmitido.IDVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_VENDEDOR", 0)
                    objBoletoEmitido.CodigoPNR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COD_RESERVA", 0)
                End While

            Catch ex As Exception
                objBoletoEmitido = Nothing

                strLog = "Stored Procedure : " & Constantes.spGDS_OBTENER_BOLETO_FACTURADO & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerBoletoFacturado" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLogRobotAnulacion(strLog, "ObtenerBoletoFacturado", strCodigoSeguimiento)
                Err.Raise(15, "ObtenerBoletoFacturado", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objBoletoEmitido
        End Function

        Public Function AnularBoletoPTA1(ByVal objBoleto As robotBoletoPendientePago.robotBoletoPendiente, _
                                        ByVal strQuienAnula As String, _
                                        ByVal strIdMotivoAnulacion As String, _
                                        ByVal strFcVoidCliente As String, _
                                        ByVal intConReposicion As Integer, _
                                        ByVal strAutorizaVoideo As String, _
                                        ByVal strCodigoSeguimiento As String, _
                                        ByVal intFirmaDB As Integer, _
                                        ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean

            Try

                ConnNM.Connect(intFirmaDB)

                Dim strCommand = Constantes.getEsquema(intEsquema) + Constantes.spGDS_GDS_ANULAR_BOLETO_PAX

                ConnNM.SP_Command(strCommand, Constantes.StoredProcedure)

                ConnNM.AgregarParametro("@p_numero_de_boleto", objBoleto.NumeroBoleto, OracleDbType.Varchar2, objBoleto.NumeroBoleto.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_id_proveedor", objBoleto.IdProveedor, OracleDbType.Varchar2, objBoleto.IdProveedor.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_id_sucursal", objBoleto.IdSucursal, OracleDbType.Varchar2, objBoleto.IdSucursal.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_quien_anula", strQuienAnula, OracleDbType.Varchar2, strQuienAnula.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_id_motivo_anulacion", strIdMotivoAnulacion, OracleDbType.Varchar2, strIdMotivoAnulacion.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_fc_void_a_cliente", strFcVoidCliente, OracleDbType.Varchar2, strFcVoidCliente.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_ConReposicion", intConReposicion, OracleDbType.Int32, intConReposicion.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_AutorizaVoideo", strAutorizaVoideo, OracleDbType.Varchar2, strAutorizaVoideo.Length, ParameterDirection.Input)

                '----------
                bolResultado = ConnNM._UpdateExecuteNonQuery()


            Catch ex As Exception
                ConnNM.Rollback()
                bolResultado = Nothing
                strLog = "Stored Procedure : " & Constantes.spGDS_GDS_ANULAR_BOLETO_PAX & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ActualizarBoletoPax" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ActualizarBoletoPax", strCodigoSeguimiento)
                Err.Raise(13, "ActualizarBoletoPax", ex.ToString)
            Finally
                ConnNM.Disconnect()
                strQuienAnula = Nothing
                strIdMotivoAnulacion = Nothing
                strFcVoidCliente = Nothing
                intConReposicion = Nothing
                strAutorizaVoideo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado

        End Function

        Public Function AnularBoletoPTA(ByVal srtNumeroBoleto As String, _
                                        ByVal strIdProveedor As String, _
                                        ByVal strIdSucursal As String, _
                                        ByVal strQuienAnula As String, _
                                        ByVal strIdMotivoAnulacion As String, _
                                        ByVal strFcVoidCliente As String, _
                                        ByVal intConReposicion As Integer, _
                                        ByVal strAutorizaVoideo As String, _
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

                ConnNM.SP_Command(Constantes.spGDS_GDS_ANULAR_BOLETO_PAX, Constantes.StoredProcedure)

                'End If


                ConnNM.AgregarParametro("@p_numero_de_boleto", srtNumeroBoleto, OracleDbType.Varchar2, srtNumeroBoleto.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_id_proveedor", strIdProveedor, OracleDbType.Varchar2, strIdProveedor.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_id_sucursal", strIdSucursal, OracleDbType.Varchar2, strIdSucursal.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_quien_anula", strQuienAnula, OracleDbType.Varchar2, strQuienAnula.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_id_motivo_anulacion", strIdMotivoAnulacion, OracleDbType.Varchar2, strIdMotivoAnulacion.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_fc_void_a_cliente", strFcVoidCliente, OracleDbType.Varchar2, strFcVoidCliente.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_ConReposicion", intConReposicion, OracleDbType.Int32, intConReposicion.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_AutorizaVoideo", strAutorizaVoideo, OracleDbType.Varchar2, strAutorizaVoideo.Length, ParameterDirection.Input)

                '----------
                bolResultado = ConnNM._UpdateExecuteNonQuery()


            Catch ex As Exception
                bolResultado = Nothing

                'If iCnx = 6 Then
                'strLog = "Stored Procedure : " & Constantes.spUPDATE_TRP_DM & vbCrLf
                'Else
                strLog = "Stored Procedure : " & Constantes.spGDS_GDS_ANULAR_BOLETO_PAX & vbCrLf
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
                intConReposicion = Nothing
                strAutorizaVoideo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado

        End Function

        Public Function ObtenerBoletosEmitidos(ByVal strCodigoSeguimiento As String, _
                                               ByVal intFirmaDB As Integer, _
                                               ByVal intEsquema As Integer) As List(Of robotBoletoPendientePago.robotBoletoPendiente)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objrobotBoletoPendiente As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim auxrobotBoletoPendiente As robotBoletoPendientePago.robotBoletoPendiente = Nothing
            Dim auxPromotor As classPromotor = Nothing
            Dim auxAgente As classDatosAgente = Nothing
            Dim auxCliente As classCliente = Nothing
            Try

                Dim commandText = Constantes.getEsquema(intEsquema) + Constantes.spBOLETOSEMITIDOS

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(commandText, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)
                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxrobotBoletoPendiente = New robotBoletoPendientePago.robotBoletoPendiente
                    auxrobotBoletoPendiente.IdSucursal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_SUCURSAL", 0)
                    auxrobotBoletoPendiente.Descripcion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                    auxrobotBoletoPendiente.IdCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CLIENTE", 0)
                    auxCliente = New classCliente
                    auxCliente.DK = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CLIENTE", 0)
                    auxCliente.TipoDeCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_TIPO_DE_CLIENTE", 0)
                    auxCliente.NombreComercial = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)
                    auxCliente.EmailAgencia = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMAIL_AGENCIA", Nothing)
                    auxCliente.Condicion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CONDICION_PAGO_AGENCIA", Nothing)
                    If auxrobotBoletoPendiente.Cliente Is Nothing Then auxrobotBoletoPendiente.Cliente = New classCliente
                    auxrobotBoletoPendiente.Cliente = auxCliente

                    auxPromotor = New classPromotor
                    auxPromotor.Codigo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PROMOTOR", Nothing)
                    auxPromotor.CodigoVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_VEND_PROMO", Nothing)
                    auxPromotor.NombrePromotor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PROMO_NOMBRE", Nothing)
                    auxPromotor.EmailPromotor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PROMO_EMAIL", Nothing)
                    auxPromotor.Anulada = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ANULADA", Nothing)
                    If auxrobotBoletoPendiente.Promotor Is Nothing Then auxrobotBoletoPendiente.Promotor = New classPromotor
                    auxrobotBoletoPendiente.Promotor = auxPromotor

                    auxAgente = New classDatosAgente
                    auxAgente.CorreoCaja = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMAIL_CAJA", Nothing)
                    If auxrobotBoletoPendiente.Vendedor Is Nothing Then auxrobotBoletoPendiente.Vendedor = New classDatosAgente
                    auxrobotBoletoPendiente.Vendedor = auxAgente

                    auxrobotBoletoPendiente.File = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_FILE", 0)
                    auxrobotBoletoPendiente.NumeroSerie1 = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_SERIE1", Nothing)
                    auxrobotBoletoPendiente.IdTipoComprobante = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_TIPO_DE_COMPROBANTE", Nothing)
                    auxrobotBoletoPendiente.PNR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COD_RESERVA", Nothing)
                    auxrobotBoletoPendiente.IdProveedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PROVEEDOR", Nothing)
                    auxrobotBoletoPendiente.FechaAltaPTA = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA_DE_ALTA", Nothing)
                    auxrobotBoletoPendiente.FechaEmision = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA_EMISION", Nothing)
                    auxrobotBoletoPendiente.PrefijoBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PREFIJO", Nothing)
                    auxrobotBoletoPendiente.NumeroBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_DE_BOLETO", Nothing)
                    auxrobotBoletoPendiente.Gds = ConnNM.LeeColumnasDataReader(objOracleDataReader, "GDS", Nothing)
                    auxrobotBoletoPendiente.Ruta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "RUTA", Nothing)
                    auxrobotBoletoPendiente.MarcaVoid = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MARCA_VOID", Nothing)
                    auxrobotBoletoPendiente.NoAnular = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NO_ANULAR", "SIN DETERMIAR")
                    auxrobotBoletoPendiente.MontoVenta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "VENTA", Nothing)
                    auxrobotBoletoPendiente.MontoAplicado = ConnNM.LeeColumnasDataReader(objOracleDataReader, "APLICADO", Nothing)
                    auxrobotBoletoPendiente.MontoPendiente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PENDIENTE", Nothing)
                    auxrobotBoletoPendiente.MontoOtroDK = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAGA_OTRO_DK", Nothing)
                    auxrobotBoletoPendiente.MarcaFacturado = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MARCA_FACTURADO", Nothing)
                    auxrobotBoletoPendiente.ID_FacturaCabeza = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_FACTURA_CABEZA", Nothing)
                    auxrobotBoletoPendiente.TotalPendiente = Val(ConnNM.LeeColumnasDataReader(objOracleDataReader, "PENDIENTE", Nothing)) - Val(ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAGA_OTRO_DK", Nothing))
                    If objrobotBoletoPendiente Is Nothing Then objrobotBoletoPendiente = New List(Of robotBoletoPendientePago.robotBoletoPendiente)
                    objrobotBoletoPendiente.Add(auxrobotBoletoPendiente)
                    auxrobotBoletoPendiente = Nothing
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spBOLETOSPENDIENTES & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerBoletosPendientesPagoAnula" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerBoletosPendientesPagoAnula", strCodigoSeguimiento)

                Err.Raise(15, "ObtenerBoletosPendientesPagoAnula", ex.ToString)
            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return objrobotBoletoPendiente
        End Function

        Public Function ObtenerBoletosEmitidos_X(ByVal strFecha As String, _
                                                 ByVal tipoGDS As Integer, _
                                                 ByVal proveedores As String, _
                                                 ByVal strCodigoSeguimiento As String, _
                                                 ByVal intFirmaDB As Integer, _
                                                 ByVal intEsquema As Integer) As List(Of robotBoletoPendientePago.robotBoletoPendiente)
            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objrobotBoletoPendiente As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim auxrobotBoletoPendiente As robotBoletoPendientePago.robotBoletoPendiente = Nothing
            Dim auxPromotor As classPromotor = Nothing
            Dim auxAgente As classDatosAgente = Nothing
            Dim auxCliente As classCliente = Nothing
            Try
                Dim commandText = Constantes.getEsquema(intEsquema) + Constantes.spBOLETOSEMITIDOS
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(commandText, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_fecha", strFecha, OracleDbType.Varchar2, strFecha.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_GDS", tipoGDS, OracleDbType.Int16, 1, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Proveedores", proveedores, OracleDbType.Varchar2, proveedores.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)
                objOracleDataReader = ConnNM._ExecuteReader()
                While objOracleDataReader.Read
                    auxrobotBoletoPendiente = New robotBoletoPendientePago.robotBoletoPendiente
                    auxrobotBoletoPendiente.IdSucursal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_SUCURSAL", 0)
                    auxrobotBoletoPendiente.Descripcion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                    auxrobotBoletoPendiente.IdCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CLIENTE", 0)
                    'auxrobotBoletoPendiente.IdVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_VENDEDOR", Nothing)
                    auxCliente = New classCliente
                    auxCliente.DK = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CLIENTE", 0)
                    auxCliente.TipoDeCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_TIPO_DE_CLIENTE", 0)
                    auxCliente.NombreComercial = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)
                    auxCliente.EmailAgencia = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMAIL_AGENCIA", Nothing)
                    auxCliente.Condicion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CONDICION_PAGO_AGENCIA", Nothing)
                    If auxrobotBoletoPendiente.Cliente Is Nothing Then auxrobotBoletoPendiente.Cliente = New classCliente
                    auxrobotBoletoPendiente.Cliente = auxCliente

                    auxPromotor = New classPromotor
                    auxPromotor.Codigo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PROMOTOR", Nothing)
                    auxPromotor.CodigoVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_VEND_PROMO", Nothing)
                    auxPromotor.NombrePromotor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PROMO_NOMBRE", Nothing)
                    auxPromotor.EmailPromotor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PROMO_EMAIL", Nothing)
                    auxPromotor.Anulada = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ANULADA", Nothing)
                    If auxrobotBoletoPendiente.Promotor Is Nothing Then auxrobotBoletoPendiente.Promotor = New classPromotor
                    auxrobotBoletoPendiente.Promotor = auxPromotor

                    auxAgente = New classDatosAgente
                    auxAgente.CorreoCaja = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMAIL_CAJA", Nothing)
                    If auxrobotBoletoPendiente.Vendedor Is Nothing Then auxrobotBoletoPendiente.Vendedor = New classDatosAgente
                    auxrobotBoletoPendiente.Vendedor = auxAgente

                    auxrobotBoletoPendiente.File = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_FILE", 0)
                    auxrobotBoletoPendiente.NumeroSerie1 = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_SERIE1", Nothing)
                    auxrobotBoletoPendiente.IdTipoComprobante = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_TIPO_DE_COMPROBANTE", Nothing)
                    auxrobotBoletoPendiente.PNR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COD_RESERVA", Nothing)
                    auxrobotBoletoPendiente.IdProveedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PROVEEDOR", Nothing)
                    auxrobotBoletoPendiente.FechaAltaPTA = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA_DE_ALTA", Nothing)
                    auxrobotBoletoPendiente.FechaEmision = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA_EMISION", Nothing)
                    auxrobotBoletoPendiente.PrefijoBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PREFIJO", Nothing)
                    auxrobotBoletoPendiente.NumeroBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_DE_BOLETO", Nothing)
                    auxrobotBoletoPendiente.Gds = ConnNM.LeeColumnasDataReader(objOracleDataReader, "GDS", Nothing)
                    auxrobotBoletoPendiente.Ruta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "RUTA", Nothing)
                    auxrobotBoletoPendiente.MarcaVoid = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MARCA_VOID", Nothing)
                    auxrobotBoletoPendiente.NoAnular = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NO_ANULAR", "SIN DETERMIAR")
                    auxrobotBoletoPendiente.MontoVenta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "VENTA", Nothing)
                    auxrobotBoletoPendiente.MontoAplicado = ConnNM.LeeColumnasDataReader(objOracleDataReader, "APLICADO", Nothing)
                    auxrobotBoletoPendiente.MontoPendiente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PENDIENTE", Nothing)
                    auxrobotBoletoPendiente.MontoOtroDK = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAGA_OTRO_DK", Nothing)
                    auxrobotBoletoPendiente.MarcaFacturado = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MARCA_FACTURADO", Nothing)
                    auxrobotBoletoPendiente.ID_FacturaCabeza = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_FACTURA_CABEZA", Nothing)
                    auxrobotBoletoPendiente.TotalPendiente = Val(ConnNM.LeeColumnasDataReader(objOracleDataReader, "PENDIENTE", Nothing)) - Val(ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAGA_OTRO_DK", Nothing))
                    If objrobotBoletoPendiente Is Nothing Then objrobotBoletoPendiente = New List(Of robotBoletoPendientePago.robotBoletoPendiente)
                    objrobotBoletoPendiente.Add(auxrobotBoletoPendiente)
                    auxrobotBoletoPendiente = Nothing
                End While
            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spBOLETOSPENDIENTES & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerBoletosPendientesPagoAnula" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerBoletosPendientesPagoAnula", strCodigoSeguimiento)

                Err.Raise(15, "ObtenerBoletosPendientesPagoAnula", ex.ToString)
            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return objrobotBoletoPendiente
        End Function


        Public Function GB_InsertBoleto(ByVal lista_Boletos As List(Of ClsBoleto_GB), _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As Boolean
            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim rpta As Boolean
            Dim strProcedure As String = IIf(intEsquema = 7, Constantes.SP_INSERT_BOLETO_CT, Constantes.SP_INSERT_BOLETO)
            Try
                ConnNM.Connect(intFirmaDB)
                For Each aux As ClsBoleto_GB In lista_Boletos
                    ConnNM.SP_Command(strProcedure, Constantes.StoredProcedure)
                    ConnNM.AgregarParametro("@V_PSEUDO", aux.strPseudo, OracleDbType.Varchar2, aux.strPseudo.Length, ParameterDirection.Input)
                    ConnNM.AgregarParametro("@V_PNR", aux.strPnr, OracleDbType.Varchar2, aux.strPnr.Length, ParameterDirection.Input)
                    ConnNM.AgregarParametro("@V_NUMBOLETO", aux.strNroBoleto, OracleDbType.Varchar2, aux.strNroBoleto.Length, ParameterDirection.Input)
                    ConnNM.AgregarParametro("@V_NOMBRE_ARCHIVO", aux.strNombreArchivo, OracleDbType.Varchar2, aux.strNombreArchivo.Length, ParameterDirection.Input)
                    ConnNM.AgregarParametro("@V_FLAG_ARCHIVO", aux.intflagArchivo, OracleDbType.Varchar2, 1, ParameterDirection.Input)
                    ConnNM.AgregarParametro("@V_FLAG_EMAIL", aux.intflag_Email, OracleDbType.Varchar2, 1, ParameterDirection.Input)
                    ConnNM._ExecuteNonQuery()
                Next
                rpta = True
            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.SP_INSERT_BOLETO & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "GeneradorBoleto_InsertBoleto" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "GeneradorBoleto_InsertBoleto", strCodigoSeguimiento)
                Err.Raise(15, "GeneradorBoleto_InsertBoleto", ex.ToString)
                rpta = False
            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return rpta
        End Function
        Public Function GB_ListarBoletosGenerados(ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As List(Of ClsBoleto_GB)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim lista_Boletos As New List(Of ClsBoleto_GB)
            Dim obj As ClsBoleto_GB
            Dim strProcedure As String = IIf(intEsquema = 7, Constantes.SP_GETBOLETO_GENERADOR_CT, Constantes.SP_GETBOLETO_GENERADOR)
            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(strProcedure, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)
                objOracleDataReader = ConnNM._ExecuteReader()
                While objOracleDataReader.Read
                    obj = New ClsBoleto_GB
                    obj.strNroBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMBOLETO", 0)
                    obj.intflagArchivo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FLAG_ARCHIVO", 0)
                    obj.intflag_Email = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FLAG_EMAIL", 0)
                    lista_Boletos.Add(obj)
                End While
            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.SP_GETBOLETO_GENERADOR & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "GB_ListarBoletosGenerados" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "GB_ListarBoletosGenerados", strCodigoSeguimiento)
                Err.Raise(15, "GB_ListarBoletosGenerados", ex.ToString)
            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return lista_Boletos

        End Function
        Public Function GB_DeleteBoletos(ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As Boolean
            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim rpta As Boolean
            Dim strProcedure As String = IIf(intEsquema = 7, Constantes.SP_GDS_DELETEBOLETOS_CT, Constantes.SP_GDS_DELETEBOLETOS)
            Try
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(strProcedure, Constantes.StoredProcedure)
                ConnNM._ExecuteNonQuery()
                rpta = True
            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.SP_INSERT_BOLETO & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "GeneradorBoleto_InsertBoleto" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "GeneradorBoleto_InsertBoleto", strCodigoSeguimiento)
                Err.Raise(15, "GeneradorBoleto_InsertBoleto", ex.ToString)
                rpta = False
            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return rpta
        End Function

        Public Function ConsultarExisteCCCF(ByVal strPnr As String,
                                        ByVal strBoleto As String,
                                        ByVal strCodigoSeguimiento As String,
                                        ByVal intFirmaDB As Integer, _
                                        ByVal intEsquema As Integer) As Integer

            Dim intRespuesta As Integer = 0

            Dim strProcedure As String = IIf(intEsquema = 7, Constantes.SP_GDS_CONSULTA_CCCF_CT, Constantes.SP_GDS_CONSULTA_CCCF)
            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Try
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(strProcedure, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Pnr", strPnr, OracleDbType.Varchar2, strPnr.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Boleto", strBoleto, OracleDbType.Varchar2, strBoleto.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Respuesta", Nothing, OracleDbType.Int16, 2, ParameterDirection.Output)
                ConnNM._ExecuteReader()
                intRespuesta = ConnNM.LeeParametros("p_Respuesta", 0)
            Catch ex As Exception
                intRespuesta = 0
                strLog = "Stored Procedure : " & Constantes.SP_GETBOLETO_GENERADOR & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ConsultarExisteCCCF" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ConsultarExisteCCCF", strCodigoSeguimiento)
                Err.Raise(15, "ConsultarExisteCCCF", ex.ToString)
            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return intRespuesta
        End Function

    End Class
End Namespace