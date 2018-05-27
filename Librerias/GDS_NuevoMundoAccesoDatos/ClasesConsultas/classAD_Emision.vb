Imports GDS_NuevoMundoPersistencia
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Namespace ObjetoAccesoDatos
    Partial Public Class classDAO
        Public Function ObtenerWebDatosSucursalPunto(ByVal intSucursal As Integer, _
                                                     ByVal intPunto As Integer, _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As classSucursalPunto

            'usuario = 10796

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objSucursalPunto As classSucursalPunto = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spDATOS_SUCURSAL_PUNTO, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Sucursal", intSucursal, OracleDbType.Int64, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Punto", intPunto, OracleDbType.Int64, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objSucursalPunto = New classSucursalPunto

                    objSucursalPunto.Nombre = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)
                    objSucursalPunto.EmailCaja = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMAIL", Nothing)
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spDATOS_SUCURSAL_PUNTO & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Sucursal: " & Convert.ToString(intSucursal) & vbCrLf
                strLog &= Constantes.TabEspacios & "Punto: " & Convert.ToString(intPunto) & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerWebDatosSucursalPunto" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerWebDatosSucursalPunto", strCodigoSeguimiento)

                Err.Raise(6, "ObtenerWebDatosSucursalPunto", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                intSucursal = Nothing
                intPunto = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objSucursalPunto

        End Function
        Public Function ObtenerGDSInterface(ByVal intAplicacion As Integer, _
                                            ByVal intOrigen As Integer, _
                                            ByVal intCliente As Integer, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As classGDS_Interface

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objGDS_Interface As classGDS_Interface = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDSINTERFACE, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Aplicacion", intAplicacion, OracleDbType.Int64, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Origen", intOrigen, OracleDbType.Int64, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Codigo", intCliente, OracleDbType.Int64, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objGDS_Interface = New classGDS_Interface
                    objGDS_Interface.IdPTA = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_INTERFACE", -1)
                    objGDS_Interface.NombreBaseDatos = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NO_BASEDATOS", Nothing)
                    objGDS_Interface.Pseudo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PSEUDO", Nothing)
                    objGDS_Interface.DkPaxDirecto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DK_PAXDIRECTO", -1)
                    objGDS_Interface.ConCotizador = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CONCOTIZADOR", -1)
                    objGDS_Interface.Sucursal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "SUCURSAL", 0)
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spGDSINTERFACE & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Aplicacion: " & Convert.ToString(intAplicacion) & vbCrLf
                strLog &= Constantes.TabEspacios & "Origen : " & Convert.ToString(intOrigen) & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerGDSInterface" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerGDSInterface", strCodigoSeguimiento)

                Err.Raise(9, "ObtenerGDSInterface", ex.Message)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                intAplicacion = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objGDS_Interface

        End Function
        Public Function ObtenerDocTarjeta(ByVal strCodigoSeguimiento As String, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal intEsquema As Integer) As List(Of classDocumento)


            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objDocTarjeta As List(Of classDocumento) = Nothing
            Dim auxDocTarjeta As classDocumento = Nothing


            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spDOCUMENTOTARJETA, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxDocTarjeta = New classDocumento
                    auxDocTarjeta.Tipo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TIPO", Nothing)
                    auxDocTarjeta.Num_Nombre = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)
                    If objDocTarjeta Is Nothing Then objDocTarjeta = New List(Of classDocumento)
                    objDocTarjeta.Add(auxDocTarjeta)
                    auxDocTarjeta = Nothing
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spDOCUMENTOTARJETA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDocTarjeta" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerDocTarjeta", strCodigoSeguimiento)

                Err.Raise(12, "ObtenerDocTarjeta", ex.Message)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objDocTarjeta

        End Function
        Public Function ObtenerDsctoExta(ByVal strPNR As String, _
                                          ByVal intCliente As Integer, _
                                          ByVal strAerolinea As String, _
                                          ByVal strCodigoSeguimiento As String, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal intEsquema As Integer) As classDsctExtra


            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objDsctExtra As classDsctExtra = Nothing


            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spDESCUENTOEXTRA, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Pnr ", strPNR, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cliente", intCliente, OracleDbType.Int64, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Aerolinea", strAerolinea, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objDsctExtra = New classDsctExtra
                    objDsctExtra.Descuento = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCUENTO", Nothing)
                    objDsctExtra.CodigoAut = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CODIGO_AUT", -1)
                    objDsctExtra.NombreAut = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE_AUT", Nothing)
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spDESCUENTOEXTRA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Pnr: " & strPNR.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Cliente: " & Convert.ToString(intCliente) & vbCrLf
                strLog &= Constantes.TabEspacios & "Aerolínea: " & Convert.ToString(strAerolinea) & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDsctoExta" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerDsctoExta", strCodigoSeguimiento)

                Err.Raise(11, "ObtenerDsctoExta", ex.Message)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strPNR = Nothing
                intCliente = Nothing
                strAerolinea = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objDsctExtra

        End Function
        Public Function ObtenerFeeOPT(ByVal intCliente As Integer, _
                                      ByVal strPNR As String, _
                                      ByVal strCodigoSeguimiento As String, _
                                      ByVal intFirmaDB As Integer, _
                                      ByVal intEsquema As Integer) As classFeeOpt.FeeOpt


            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim auxFeeOpt As classFeeOpt.classCallCenter = Nothing
            Dim lstCallCenter As List(Of classFeeOpt.classCallCenter) = Nothing
            Dim objRespuesta As classFeeOpt.FeeOpt = Nothing
            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDSFEE_WAIVER, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Pnr ", strPNR, OracleDbType.Varchar2, strPNR.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read

                    auxFeeOpt = New classFeeOpt.classCallCenter
                    auxFeeOpt.IdTipoWaiver = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_TIPO_WAIVER", Nothing)
                    auxFeeOpt.Descripcion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", -1)
                    auxFeeOpt.Monto = New classMonto
                    auxFeeOpt.Monto.Moneda = "USD"
                    auxFeeOpt.Monto.Monto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MONTO", 0.0)
                    auxFeeOpt.Marca = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MARCA", -1)

                    If lstCallCenter Is Nothing Then lstCallCenter = New List(Of classFeeOpt.classCallCenter)
                    lstCallCenter.Add(auxFeeOpt)

                End While

                If lstCallCenter IsNot Nothing Then
                    objRespuesta = New classFeeOpt.FeeOpt
                    objRespuesta.FeeCallCenter = lstCallCenter
                End If

            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spGDSFEE_WAIVER & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Pnr: " & strPNR.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Cliente: " & Convert.ToString(intCliente) & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerFeeOPT" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerFeeOPT", strCodigoSeguimiento)

                Err.Raise(11, "ObtenerFeeOPT", ex.Message)
            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strPNR = Nothing
                intCliente = Nothing
                auxFeeOpt = Nothing
                lstCallCenter = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objRespuesta

        End Function
        Public Function ObtenerTipoPasajero(ByVal strTipoTarifa As String, _
                                            ByVal strAerolineas As String, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As List(Of classDatosTipoPasajero)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objDatosTipoPasajero As List(Of classDatosTipoPasajero) = Nothing
            Dim auxDatosTipoPasajero As classDatosTipoPasajero = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spTIPO_PASAJEROS, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_TipoTarifa", strTipoTarifa, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Aerolinea", strAerolineas, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxDatosTipoPasajero = New classDatosTipoPasajero
                    auxDatosTipoPasajero.ID = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID", Nothing)
                    auxDatosTipoPasajero.Descripcion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                    auxDatosTipoPasajero.Pertenece = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PERTENECE", Nothing)
                    auxDatosTipoPasajero.Equivale = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EQUIVALE", Nothing)
                    If objDatosTipoPasajero Is Nothing Then objDatosTipoPasajero = New List(Of classDatosTipoPasajero)
                    objDatosTipoPasajero.Add(auxDatosTipoPasajero)
                End While

            Catch ex As Exception
                objDatosTipoPasajero = Nothing
                strLog = "Stored Procedure : " & Constantes.spTIPO_PASAJEROS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerTipoPasajero" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerTipoPasajero", strCodigoSeguimiento)

                Err.Raise(11, "ObtenerTipoPasajero", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strTipoTarifa = Nothing
                strAerolineas = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objDatosTipoPasajero

        End Function
        Public Function ObtenerDocumentoEmision(ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As List(Of classTipoDocumentoEmision)

            'Dim ConnNM As New MyConnectionOracle
            'Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objTipoDocumentoEmision As List(Of classTipoDocumentoEmision) = Nothing
            Dim auxTipoDocumentoEmision As classTipoDocumentoEmision = Nothing

            Try
                ' 1 Emisión
                ' 2 Pago con Tarjeta
                ' 3 Busqueda
                If objTipoDocumentoEmision Is Nothing Then objTipoDocumentoEmision = New List(Of classTipoDocumentoEmision)

                auxTipoDocumentoEmision = New classTipoDocumentoEmision
                auxTipoDocumentoEmision.ID = "DK"
                auxTipoDocumentoEmision.Descripcion = "DK"
                auxTipoDocumentoEmision.Longitud = 6
                auxTipoDocumentoEmision.ParaEmision = -1
                auxTipoDocumentoEmision.ParaTarjeta = -1
                auxTipoDocumentoEmision.ParaBusqueda = -1
                objTipoDocumentoEmision.Add(auxTipoDocumentoEmision)


                auxTipoDocumentoEmision = New classTipoDocumentoEmision
                auxTipoDocumentoEmision.ID = "D"
                auxTipoDocumentoEmision.Descripcion = "DNI"
                auxTipoDocumentoEmision.Longitud = 8
                auxTipoDocumentoEmision.ParaEmision = 1
                auxTipoDocumentoEmision.ParaTarjeta = 1
                auxTipoDocumentoEmision.ParaBusqueda = -1
                objTipoDocumentoEmision.Add(auxTipoDocumentoEmision)

                auxTipoDocumentoEmision = New classTipoDocumentoEmision
                auxTipoDocumentoEmision.ID = "CE"
                auxTipoDocumentoEmision.Descripcion = "C. Extranjería"
                auxTipoDocumentoEmision.Longitud = -1
                auxTipoDocumentoEmision.ParaEmision = 1
                auxTipoDocumentoEmision.ParaTarjeta = 1
                auxTipoDocumentoEmision.ParaBusqueda = -1
                objTipoDocumentoEmision.Add(auxTipoDocumentoEmision)


                auxTipoDocumentoEmision = New classTipoDocumentoEmision
                auxTipoDocumentoEmision.ID = "PASS"
                auxTipoDocumentoEmision.Descripcion = "Pasaporte"
                auxTipoDocumentoEmision.Longitud = -1
                auxTipoDocumentoEmision.ParaEmision = 1
                auxTipoDocumentoEmision.ParaTarjeta = 1
                auxTipoDocumentoEmision.ParaBusqueda = -1
                objTipoDocumentoEmision.Add(auxTipoDocumentoEmision)


                auxTipoDocumentoEmision = New classTipoDocumentoEmision
                auxTipoDocumentoEmision.ID = "RUC"
                auxTipoDocumentoEmision.Descripcion = "RUC"
                auxTipoDocumentoEmision.Longitud = 11
                auxTipoDocumentoEmision.ParaEmision = 1
                auxTipoDocumentoEmision.ParaTarjeta = 1
                auxTipoDocumentoEmision.ParaBusqueda = 1
                objTipoDocumentoEmision.Add(auxTipoDocumentoEmision)


                auxTipoDocumentoEmision = New classTipoDocumentoEmision
                auxTipoDocumentoEmision.ID = "RZ"
                auxTipoDocumentoEmision.Descripcion = "Razón Social"
                auxTipoDocumentoEmision.Longitud = -1
                auxTipoDocumentoEmision.ParaEmision = -1
                auxTipoDocumentoEmision.ParaTarjeta = -1
                auxTipoDocumentoEmision.ParaBusqueda = 1
                objTipoDocumentoEmision.Add(auxTipoDocumentoEmision)

                auxTipoDocumentoEmision = New classTipoDocumentoEmision
                auxTipoDocumentoEmision.ID = "NC"
                auxTipoDocumentoEmision.Descripcion = "NombreComercial"
                auxTipoDocumentoEmision.Longitud = -1
                auxTipoDocumentoEmision.ParaEmision = -1
                auxTipoDocumentoEmision.ParaTarjeta = -1
                auxTipoDocumentoEmision.ParaBusqueda = 1
                objTipoDocumentoEmision.Add(auxTipoDocumentoEmision)

                'ConnNM.Connect(iCnx)
                'ConnNM.SP_Command(Constantes.spDOCUMENTO_EMISION, Constantes.StoredProcedure)
                ''----------
                'ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                'objOracleDataReader = ConnNM._ExecuteReader()

                'While objOracleDataReader.Read
                '    auxTipoDocumentoEmision = New classTipoDocumentoEmision
                '    auxTipoDocumentoEmision.ID = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID", Nothing)
                '    auxTipoDocumentoEmision.Descripcion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                '    auxTipoDocumentoEmision.Longitud = ConnNM.LeeColumnasDataReader(objOracleDataReader, "LONGITUD", -1)
                '    auxTipoDocumentoEmision.ParaDatosTarjeta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "SOLO_CLIENTES", -1)
                '    auxTipoDocumentoEmision.ParaEmision = ConnNM.LeeColumnasDataReader(objOracleDataReader, "GDS_EMISION", -1)
                '    If objTipoDocumentoEmision Is Nothing Then objTipoDocumentoEmision = New List(Of classTipoDocumentoEmision)
                '    objTipoDocumentoEmision.Add(auxTipoDocumentoEmision)
                'End While

            Catch ex As Exception
                objTipoDocumentoEmision = Nothing
                strLog = "Stored Procedure : " & Constantes.spDOCUMENTO_EMISION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDocumentoEmision" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerDocumentoEmision", strCodigoSeguimiento)

                Err.Raise(12, "ObtenerDocumentoEmision", ex.ToString)

            Finally
                'ConnNM.Disconnect()
                'objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                'ConnNM = Nothing
                auxTipoDocumentoEmision = Nothing
            End Try

            Return objTipoDocumentoEmision

        End Function
        Public Function ObtenerFormaPago(ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As List(Of classDato)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objFormaPago As List(Of classDato) = Nothing
            Dim auxFormaPago As classDato = Nothing

            Try

                objFormaPago = New List(Of classDato)

                auxFormaPago = New classDato
                auxFormaPago.Codigo = "00"
                auxFormaPago.Valor = "Seleccione"
                objFormaPago.Add(auxFormaPago)

                auxFormaPago = New classDato
                auxFormaPago.Codigo = "01"
                auxFormaPago.Valor = "Cash"
                objFormaPago.Add(auxFormaPago)


                auxFormaPago = New classDato
                auxFormaPago.Codigo = "02"
                auxFormaPago.Valor = "Deposito/Transferencia"
                objFormaPago.Add(auxFormaPago)

                auxFormaPago = New classDato
                auxFormaPago.Codigo = "03"
                auxFormaPago.Valor = "Card/Cash"
                objFormaPago.Add(auxFormaPago)

                auxFormaPago = New classDato
                auxFormaPago.Codigo = "04"
                auxFormaPago.Valor = "Deposito/Tarjeta"
                objFormaPago.Add(auxFormaPago)

                auxFormaPago = New classDato
                auxFormaPago.Codigo = "05"
                auxFormaPago.Valor = "Tarjeta"
                objFormaPago.Add(auxFormaPago)


                'ConnNM.Connect(iCnx)
                'ConnNM.SP_Command(Constantes.spDOCUMENTO_EMISION, Constantes.StoredProcedure)
                ''----------
                'ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                'objOracleDataReader = ConnNM._ExecuteReader()

                'While objOracleDataReader.Read
                '    auxTipoDocumentoEmision = New classTipoDocumentoEmision
                '    auxTipoDocumentoEmision.ID = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID", Nothing)
                '    auxTipoDocumentoEmision.Descripcion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                '    auxTipoDocumentoEmision.Longitud = ConnNM.LeeColumnasDataReader(objOracleDataReader, "LONGITUD", -1)
                '    auxTipoDocumentoEmision.ParaDatosTarjeta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "SOLO_CLIENTES", -1)
                '    auxTipoDocumentoEmision.ParaEmision = ConnNM.LeeColumnasDataReader(objOracleDataReader, "GDS_EMISION", -1)
                '    If objTipoDocumentoEmision Is Nothing Then objTipoDocumentoEmision = New List(Of classTipoDocumentoEmision)
                '    objTipoDocumentoEmision.Add(auxTipoDocumentoEmision)
                'End While

            Catch ex As Exception
                objFormaPago = Nothing
                'strLog = "Stored Procedure : " & Constantes.spDOCUMENTO_EMISION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerFormaPago" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerFormaPago", strCodigoSeguimiento)

                Err.Raise(12, "ObtenerFormaPago", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                auxFormaPago = Nothing
            End Try

            Return objFormaPago

        End Function
        Public Function ObtenerOperaciones(ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As List(Of classDato)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objOperaciones As List(Of classDato) = Nothing
            Dim auxOperaciones As classDato = Nothing

            Try

                objOperaciones = New List(Of classDato)

                auxOperaciones = New classDato
                auxOperaciones.Codigo = "00"
                auxOperaciones.Valor = "Seleccione"
                objOperaciones.Add(auxOperaciones)

                auxOperaciones = New classDato
                auxOperaciones.Codigo = "01"
                auxOperaciones.Valor = "Deposito en Efectivo"
                objOperaciones.Add(auxOperaciones)

                auxOperaciones = New classDato
                auxOperaciones.Codigo = "02"
                auxOperaciones.Valor = "Deposito en Cheque"
                objOperaciones.Add(auxOperaciones)

                auxOperaciones = New classDato
                auxOperaciones.Codigo = "03"
                auxOperaciones.Valor = "Transferencia"
                objOperaciones.Add(auxOperaciones)


                'ConnNM.Connect(iCnx)
                'ConnNM.SP_Command(Constantes.spDOCUMENTO_EMISION, Constantes.StoredProcedure)
                ''----------
                'ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                'objOracleDataReader = ConnNM._ExecuteReader()

                'While objOracleDataReader.Read
                '    auxTipoDocumentoEmision = New classTipoDocumentoEmision
                '    auxTipoDocumentoEmision.ID = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID", Nothing)
                '    auxTipoDocumentoEmision.Descripcion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                '    auxTipoDocumentoEmision.Longitud = ConnNM.LeeColumnasDataReader(objOracleDataReader, "LONGITUD", -1)
                '    auxTipoDocumentoEmision.ParaDatosTarjeta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "SOLO_CLIENTES", -1)
                '    auxTipoDocumentoEmision.ParaEmision = ConnNM.LeeColumnasDataReader(objOracleDataReader, "GDS_EMISION", -1)
                '    If objTipoDocumentoEmision Is Nothing Then objTipoDocumentoEmision = New List(Of classTipoDocumentoEmision)
                '    objTipoDocumentoEmision.Add(auxTipoDocumentoEmision)
                'End While

            Catch ex As Exception
                objOperaciones = Nothing
                'strLog = "Stored Procedure : " & Constantes.spDOCUMENTO_EMISION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerOperaciones" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerOperaciones", strCodigoSeguimiento)

                Err.Raise(12, "ObtenerOperaciones", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                auxOperaciones = Nothing
            End Try

            Return objOperaciones

        End Function
        Public Function ObtenerCuentaBancaria(ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As List(Of classCuentaBancaria)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objCuentaBancaria As List(Of classCuentaBancaria) = Nothing
            Dim auxCuentaBancaria As classCuentaBancaria = Nothing

            Try

                objCuentaBancaria = New List(Of classCuentaBancaria)

                auxCuentaBancaria = New classCuentaBancaria
                auxCuentaBancaria.IDCuenta = "00"
                auxCuentaBancaria.NombreBanco = "Seleccione"
                auxCuentaBancaria.NumeroCuenta = String.Empty
                objCuentaBancaria.Add(auxCuentaBancaria)

                auxCuentaBancaria = New classCuentaBancaria
                auxCuentaBancaria.IDCuenta = "01"
                auxCuentaBancaria.NombreBanco = "Banco de Crédito"
                auxCuentaBancaria.NumeroCuenta = "194-1126794-1-66"
                objCuentaBancaria.Add(auxCuentaBancaria)

                auxCuentaBancaria = New classCuentaBancaria
                auxCuentaBancaria.IDCuenta = "02"
                auxCuentaBancaria.NombreBanco = "Banco Continental"
                auxCuentaBancaria.NumeroCuenta = "0011-0126-07-0100005951"
                objCuentaBancaria.Add(auxCuentaBancaria)

                auxCuentaBancaria = New classCuentaBancaria
                auxCuentaBancaria.IDCuenta = "03"
                auxCuentaBancaria.NombreBanco = "Banco Interbank"
                auxCuentaBancaria.NumeroCuenta = "100-0000096602"
                objCuentaBancaria.Add(auxCuentaBancaria)

                auxCuentaBancaria = New classCuentaBancaria
                auxCuentaBancaria.IDCuenta = "04"
                auxCuentaBancaria.NombreBanco = "Banco Scotiabank"
                auxCuentaBancaria.NumeroCuenta = "000-1696427"
                objCuentaBancaria.Add(auxCuentaBancaria)


                'ConnNM.Connect(iCnx)
                'ConnNM.SP_Command(Constantes.spDOCUMENTO_EMISION, Constantes.StoredProcedure)
                ''----------
                'ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                'objOracleDataReader = ConnNM._ExecuteReader()

                'While objOracleDataReader.Read
                '    auxTipoDocumentoEmision = New classTipoDocumentoEmision
                '    auxTipoDocumentoEmision.ID = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID", Nothing)
                '    auxTipoDocumentoEmision.Descripcion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                '    auxTipoDocumentoEmision.Longitud = ConnNM.LeeColumnasDataReader(objOracleDataReader, "LONGITUD", -1)
                '    auxTipoDocumentoEmision.ParaDatosTarjeta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "SOLO_CLIENTES", -1)
                '    auxTipoDocumentoEmision.ParaEmision = ConnNM.LeeColumnasDataReader(objOracleDataReader, "GDS_EMISION", -1)
                '    If objTipoDocumentoEmision Is Nothing Then objTipoDocumentoEmision = New List(Of classTipoDocumentoEmision)
                '    objTipoDocumentoEmision.Add(auxTipoDocumentoEmision)
                'End While

            Catch ex As Exception
                objCuentaBancaria = Nothing
                'strLog = "Stored Procedure : " & Constantes.spDOCUMENTO_EMISION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerCuentaBancaria" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.Message & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerCuentaBancaria", strCodigoSeguimiento)

                Err.Raise(12, "ObtenerCuentaBancaria", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                auxCuentaBancaria = Nothing
            End Try

            Return objCuentaBancaria

        End Function
        Public Function ObtenerTipoTarifa(ByVal strCodigoSeguimiento As String, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal intEsquema As Integer) As List(Of classDato)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objTipoTarifa As List(Of classDato) = Nothing
            Dim auxTipoTarifa As classDato = Nothing

            Try

                auxTipoTarifa = New classDato
                auxTipoTarifa.sCodigo = "00"
                auxTipoTarifa.Valor = "Seleccione"
                objTipoTarifa = New List(Of classDato)
                objTipoTarifa.Add(auxTipoTarifa)

                auxTipoTarifa = New classDato
                auxTipoTarifa.sCodigo = "PL"
                auxTipoTarifa.Valor = "Publicada"
                objTipoTarifa.Add(auxTipoTarifa)


                auxTipoTarifa = New classDato
                auxTipoTarifa.sCodigo = "PV"
                auxTipoTarifa.Valor = "Negociada"
                objTipoTarifa.Add(auxTipoTarifa)




                'ConnNM.Connect(iCnx)
                'ConnNM.SP_Command(Constantes.spDOCUMENTO_EMISION, Constantes.StoredProcedure)
                ''----------
                'ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                'objOracleDataReader = ConnNM._ExecuteReader()

                'While objOracleDataReader.Read
                '    auxTipoDocumentoEmision = New classTipoDocumentoEmision
                '    auxTipoDocumentoEmision.ID = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID", Nothing)
                '    auxTipoDocumentoEmision.Descripcion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                '    auxTipoDocumentoEmision.Longitud = ConnNM.LeeColumnasDataReader(objOracleDataReader, "LONGITUD", -1)
                '    auxTipoDocumentoEmision.ParaDatosTarjeta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "SOLO_CLIENTES", -1)
                '    auxTipoDocumentoEmision.ParaEmision = ConnNM.LeeColumnasDataReader(objOracleDataReader, "GDS_EMISION", -1)
                '    If objTipoDocumentoEmision Is Nothing Then objTipoDocumentoEmision = New List(Of classTipoDocumentoEmision)
                '    objTipoDocumentoEmision.Add(auxTipoDocumentoEmision)
                'End While

            Catch ex As Exception
                objTipoTarifa = Nothing
                'strLog = "Stored Procedure : " & Constantes.spDOCUMENTO_EMISION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerTipoTarifa" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerTipoTarifa", strCodigoSeguimiento)

                Err.Raise(12, "ObtenerTipoTarifa", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                auxTipoTarifa = Nothing
            End Try

            Return objTipoTarifa

        End Function
        Public Function ObtenerTipoReserva(ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As List(Of classDato)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objTipoReserva As List(Of classDato) = Nothing
            Dim auxTipoReserva As classDato = Nothing

            Try

                objTipoReserva = New List(Of classDato)

                auxTipoReserva = New classDato
                auxTipoReserva.Codigo = "00"
                auxTipoReserva.Valor = "Seleccione"
                objTipoReserva.Add(auxTipoReserva)

                auxTipoReserva = New classDato
                auxTipoReserva.Codigo = "01"
                auxTipoReserva.Valor = "Amadeus"
                objTipoReserva.Add(auxTipoReserva)

                auxTipoReserva = New classDato
                auxTipoReserva.Codigo = "02"
                auxTipoReserva.Valor = "Sabre"
                objTipoReserva.Add(auxTipoReserva)

                auxTipoReserva = New classDato
                auxTipoReserva.Codigo = "03"
                auxTipoReserva.Valor = "Kiu"
                objTipoReserva.Add(auxTipoReserva)

                auxTipoReserva = New classDato
                auxTipoReserva.Codigo = "04"
                auxTipoReserva.Valor = "Resiber"
                objTipoReserva.Add(auxTipoReserva)


                auxTipoReserva = New classDato
                auxTipoReserva.Codigo = "05"
                auxTipoReserva.Valor = "WorldSpan"
                objTipoReserva.Add(auxTipoReserva)

                auxTipoReserva = New classDato
                auxTipoReserva.Codigo = "06"
                auxTipoReserva.Valor = "Aerolínea"
                objTipoReserva.Add(auxTipoReserva)

                'ConnNM.Connect(iCnx)
                'ConnNM.SP_Command(Constantes.spDOCUMENTO_EMISION, Constantes.StoredProcedure)
                ''----------
                'ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                'objOracleDataReader = ConnNM._ExecuteReader()

                'While objOracleDataReader.Read
                '    auxTipoDocumentoEmision = New classTipoDocumentoEmision
                '    auxTipoDocumentoEmision.ID = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID", Nothing)
                '    auxTipoDocumentoEmision.Descripcion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                '    auxTipoDocumentoEmision.Longitud = ConnNM.LeeColumnasDataReader(objOracleDataReader, "LONGITUD", -1)
                '    auxTipoDocumentoEmision.ParaDatosTarjeta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "SOLO_CLIENTES", -1)
                '    auxTipoDocumentoEmision.ParaEmision = ConnNM.LeeColumnasDataReader(objOracleDataReader, "GDS_EMISION", -1)
                '    If objTipoDocumentoEmision Is Nothing Then objTipoDocumentoEmision = New List(Of classTipoDocumentoEmision)
                '    objTipoDocumentoEmision.Add(auxTipoDocumentoEmision)
                'End While

            Catch ex As Exception
                objTipoReserva = Nothing
                'strLog = "Stored Procedure : " & Constantes.spDOCUMENTO_EMISION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerTipoReserva" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerTipoReserva", strCodigoSeguimiento)

                Err.Raise(12, "ObtenerTipoReserva", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                auxTipoReserva = Nothing
            End Try

            Return objTipoReserva

        End Function
        Public Function ObtenerProFile(ByVal strPseudoEmisor As String, _
                                       ByVal strCodigoSeguimiento As String, _
                                       ByVal intFirmaDB As Integer, _
                                       ByVal intEsquema As Integer) As String


            Dim ConnNM As New MyConnectionOracle
            Dim strProFile As String = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spASIGNA_IMPRESORA, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("p_Pseudo", strPseudoEmisor, OracleDbType.Varchar2, strPseudoEmisor.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("p_Profile", Nothing, OracleDbType.Varchar2, 10, ParameterDirection.Output)

                ConnNM._ExecuteReader()

                strProFile = ConnNM.LeeParametros("p_Profile", Nothing)

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spASIGNA_IMPRESORA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerProFile" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerProFile", strCodigoSeguimiento)

                Err.Raise(14, "ObtenerProFile", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strPseudoEmisor = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return strProFile

        End Function
        Public Function ObtenerTipoTarjeta(ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As List(Of classDocumento)


            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim lstDocumento As List(Of classDocumento) = Nothing
            Dim auxDocumento As classDocumento = Nothing
            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spTIPO_TARJETA, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read

                    auxDocumento = New classDocumento
                    auxDocumento.Tipo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ABREVIATURA", Nothing)
                    auxDocumento.Num_Nombre = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)


                    If lstDocumento Is Nothing Then lstDocumento = New List(Of classDocumento)
                    lstDocumento.Add(auxDocumento)

                End While

            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spTIPO_TARJETA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerTipoTarjeta" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerTipoTarjeta", strCodigoSeguimiento)

                Err.Raise(11, "ObtenerTipoTarjeta", ex.ToString)
            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return lstDocumento

        End Function
        Public Function ObtenerTURBO_PuntoFacturacion(ByVal strPCC_AAA As String, _
                                                      ByVal strPCC_Firma As String, _
                                                      ByVal strCodigoSeguimiento As String, _
                                                      ByVal intFirmaDB As Integer, _
                                                      ByVal intEsquema As Integer) As List(Of classSucursalPunto)


            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim SucursalPunto As classSucursalPunto = Nothing
            Dim lstSucursalPunto As List(Of classSucursalPunto) = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spTURBO_PUNTOFACTURACION, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("@p_PCC_AAA", strPCC_AAA, OracleDbType.Varchar2, strPCC_AAA.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_PCC_FIRMA", strPCC_Firma, OracleDbType.Varchar2, strPCC_Firma.Length, ParameterDirection.Input)

                ConnNM.AgregarParametro("@p_Resultado", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read

                    'ID_PSEUDO_CITY
                    'ID_SUCURSAL
                    'ID_PUNTO
                    'NOMBRE_PUNTO
                    SucursalPunto = New classSucursalPunto
                    SucursalPunto.Sucursal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_SUCURSAL", -1)
                    SucursalPunto.Punto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PUNTO", -1)
                    SucursalPunto.Nombre = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE_PUNTO", Nothing)
                    SucursalPunto.PseudoCity = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PSEUDO_CITY", Nothing)

                    If lstSucursalPunto Is Nothing Then lstSucursalPunto = New List(Of classSucursalPunto)
                    lstSucursalPunto.Add(SucursalPunto)
                    SucursalPunto = Nothing

                End While

            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spTURBO_PUNTOFACTURACION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerTURBO_PuntoFacturacion" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerTURBO_PuntoFacturacion", strCodigoSeguimiento)

                Err.Raise(11, "ObtenerTURBO_PuntoFacturacion", ex.ToString)
            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return lstSucursalPunto

        End Function
        Public Function ObtenerTURBO_DatosPax(ByVal strNUMERO_DOCUMENTO As String, _
                                              ByVal strTIPO_DOCUMENTO As String, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As classTurboDatosPax


            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objPasajeros As classTurboDatosPax = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spTURBO_DATOSPAX, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("@p_NUMERO_DOCUMENTO", strNUMERO_DOCUMENTO, OracleDbType.Varchar2, strNUMERO_DOCUMENTO.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_TIPO_DOCUMENTO", strTIPO_DOCUMENTO, OracleDbType.Varchar2, strTIPO_DOCUMENTO.Length, ParameterDirection.Input)

                ConnNM.AgregarParametro("@p_Resultado", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objPasajeros = New classTurboDatosPax

                    objPasajeros.NombrePasajero = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRES", -1)
                    objPasajeros.ApellidoPaterno = ConnNM.LeeColumnasDataReader(objOracleDataReader, "APELLIDO_PATERNO", -1)
                    objPasajeros.ApellidoMarterno = ConnNM.LeeColumnasDataReader(objOracleDataReader, "APELLIDO_MATERNO", Nothing)
                    objPasajeros.Direccion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DIRECCION", Nothing)
                    objPasajeros.TelefonoCasa = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TELEFONO_CASA", Nothing)
                    objPasajeros.TelefonoCelular = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TELEFONO_CELULAR", Nothing)

                End While

            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spTURBO_PUNTOFACTURACION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerTURBO_DatosPax" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerTURBO_DatosPax", strCodigoSeguimiento)

                Err.Raise(11, "ObtenerTURBO_DatosPax", ex.ToString)
            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objPasajeros

        End Function
        Public Function ObtenerTURBO_EmisionSolicitud(ByVal strFiltro As String, _
                                                      ByVal strCodigoSeguimiento As String, _
                                                      ByVal intFirmaDB As Integer, _
                                                      ByVal intEsquema As Integer) As List(Of classDato)


            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim classDato As classDato = Nothing
            Dim lstDato As List(Of classDato) = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spTURBO_EMISION_SOLICITUD, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("@p_TipoConsulta", strFiltro, OracleDbType.Varchar2, strFiltro.Length, ParameterDirection.Input)

                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    classDato = New classDato

                    classDato.Codigo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_COMO_SE_EMITIO", -1)
                    classDato.Valor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)

                    If lstDato Is Nothing Then lstDato = New List(Of classDato)
                    lstDato.Add(classDato)

                End While

            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spTURBO_EMISION_SOLICITUD & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerTURBO_EmisionSolicitud" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerTURBO_EmisionSolicitud", strCodigoSeguimiento)

                Err.Raise(11, "ObtenerTURBO_EmisionSolicitud", ex.ToString)
            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                classDato = Nothing
            End Try

            Return lstDato

        End Function
        Public Function InsertaTURBO_DatosPax(ByVal objTurboDatosPax As classTurboDatosPax, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean = False

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spTURBO_INS_DATOSPAX, Constantes.StoredProcedure)

                If String.IsNullOrEmpty(objTurboDatosPax.IdTipoDocumento) Then
                    ConnNM.AgregarParametro("p_ID_TIPO_DOCUMENTO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_TIPO_DOCUMENTO", objTurboDatosPax.IdTipoDocumento, OracleDbType.Varchar2, objTurboDatosPax.IdTipoDocumento.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objTurboDatosPax.NumeroDocumento) Then
                    ConnNM.AgregarParametro("p_NUM_DOCUMENTO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_NUM_DOCUMENTO", objTurboDatosPax.NumeroDocumento, OracleDbType.Varchar2, objTurboDatosPax.NumeroDocumento.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objTurboDatosPax.ApellidoPaterno) Then
                    ConnNM.AgregarParametro("p_APELLIDO_PATERNO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_APELLIDO_PATERNO", objTurboDatosPax.ApellidoPaterno, OracleDbType.Varchar2, objTurboDatosPax.ApellidoPaterno.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objTurboDatosPax.ApellidoMarterno) Then
                    ConnNM.AgregarParametro("p_APELLIDO_MATERNO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_APELLIDO_MATERNO", objTurboDatosPax.ApellidoMarterno, OracleDbType.Varchar2, objTurboDatosPax.ApellidoMarterno.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objTurboDatosPax.NombrePasajero) Then
                    ConnNM.AgregarParametro("p_NOMBRES", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_NOMBRES", objTurboDatosPax.NombrePasajero, OracleDbType.Varchar2, objTurboDatosPax.NombrePasajero.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objTurboDatosPax.Direccion) Then
                    ConnNM.AgregarParametro("p_DIRECCION", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_DIRECCION", objTurboDatosPax.Direccion, OracleDbType.Varchar2, objTurboDatosPax.Direccion.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objTurboDatosPax.TelefonoCasa) Then
                    ConnNM.AgregarParametro("p_TELEFONO_CASA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_TELEFONO_CASA", objTurboDatosPax.TelefonoCasa, OracleDbType.Varchar2, objTurboDatosPax.TelefonoCasa.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objTurboDatosPax.TelefonoCelular) Then
                    ConnNM.AgregarParametro("p_TELEFONO_CELULAR", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_TELEFONO_CELULAR", objTurboDatosPax.TelefonoCelular, OracleDbType.Varchar2, objTurboDatosPax.TelefonoCelular.Length, ParameterDirection.Input)
                End If


                bolResultado = ConnNM._InsertExecuteNonQuery()
                '----------

            Catch ex As Exception

                bolResultado = False
                strLog = "Stored Procedure : " & Constantes.spTURBO_INS_DATOSPAX & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "InsertaTURBO_DatosPax" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "InsertaTURBO_DatosPax", strCodigoSeguimiento)

                Err.Raise(13, "InsertaTURBO_DatosPax", ex.ToString)


            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function

        Public Function Inserta_Interface_General(ByVal objIG As classInterfaceGeneral, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean = False

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spTURBO_INS_INTERFACEGENERAL, Constantes.StoredProcedure)

                '1
                If String.IsNullOrEmpty(objIG.Referencia) Then
                    ConnNM.AgregarParametro("p_ID_REFERENCIA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_REFERENCIA", objIG.Referencia, OracleDbType.Varchar2, objIG.Referencia.Length, ParameterDirection.Input)
                End If
                '2
                If String.IsNullOrEmpty(objIG.Secuencia) Then
                    ConnNM.AgregarParametro("p_ID_SECUENCIA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_SECUENCIA", objIG.Secuencia, OracleDbType.Varchar2, objIG.Secuencia.ToString.Length, ParameterDirection.Input)
                End If
                '3
                If String.IsNullOrEmpty(objIG.OfficeId) Then
                    ConnNM.AgregarParametro("p_ID_PSEUDO_CITY", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_PSEUDO_CITY", objIG.OfficeId, OracleDbType.Varchar2, objIG.OfficeId.Length, ParameterDirection.Input)
                End If
                '4
                If String.IsNullOrEmpty(objIG.Dk) Then
                    ConnNM.AgregarParametro("p_ID_CLIENTE", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_CLIENTE", objIG.Dk, OracleDbType.Varchar2, objIG.Dk.ToString.Length, ParameterDirection.Input)
                End If
                '5
                If String.IsNullOrEmpty(objIG.SubCodigo) Then
                    ConnNM.AgregarParametro("p_ID_SUBCODIGO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_SUBCODIGO", objIG.SubCodigo, OracleDbType.Varchar2, objIG.SubCodigo.Length, ParameterDirection.Input)
                End If
                '6
                If String.IsNullOrEmpty(objIG.IdGSA) Then
                    ConnNM.AgregarParametro("p_ID_EJECUTIVA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_EJECUTIVA", objIG.IdGSA, OracleDbType.Varchar2, objIG.IdGSA.Length, ParameterDirection.Input)
                End If
                '7
                If String.IsNullOrEmpty(objIG.Moneda) Then
                    ConnNM.AgregarParametro("p_ID_MONEDA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_MONEDA", objIG.Moneda, OracleDbType.Varchar2, objIG.Moneda.Length, ParameterDirection.Input)
                End If
                '8
                If String.IsNullOrEmpty(objIG.PNR) Then
                    ConnNM.AgregarParametro("p_COD_RESERVA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_COD_RESERVA", objIG.PNR, OracleDbType.Varchar2, objIG.PNR.Length, ParameterDirection.Input)
                End If
                '9
                If String.IsNullOrEmpty(objIG.IdGDS) Then
                    ConnNM.AgregarParametro("p_ID_GDS", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_GDS", objIG.IdGDS, OracleDbType.Varchar2, objIG.IdGDS.ToString.Length, ParameterDirection.Input)
                End If
                '10
                If String.IsNullOrEmpty(objIG.TipoDocumentoFC) Then
                    ConnNM.AgregarParametro("p_TIPO_DOC_IDENTIDAD_A_FC", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_TIPO_DOC_IDENTIDAD_A_FC", objIG.TipoDocumentoFC, OracleDbType.Varchar2, objIG.TipoDocumentoFC.Length, ParameterDirection.Input)
                End If
                '11
                If String.IsNullOrEmpty(objIG.NumeroDocumentoFC) Then
                    ConnNM.AgregarParametro("p_NUMERO_DOCUMENTO_A_FC", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_NUMERO_DOCUMENTO_A_FC", objIG.NumeroDocumentoFC, OracleDbType.Varchar2, objIG.NumeroDocumentoFC.Length, ParameterDirection.Input)
                End If
                '12
                If String.IsNullOrEmpty(objIG.NombreFacturar) Then
                    ConnNM.AgregarParametro("p_NOMBRE_A_FC", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_NOMBRE_A_FC", objIG.NombreFacturar, OracleDbType.Varchar2, objIG.NombreFacturar.Length, ParameterDirection.Input)
                End If
                '13
                If String.IsNullOrEmpty(objIG.CiudadDestino) Then
                    ConnNM.AgregarParametro("p_ID_CIUDAD_DESTINO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_CIUDAD_DESTINO", objIG.CiudadDestino, OracleDbType.Varchar2, objIG.CiudadDestino.Length, ParameterDirection.Input)
                End If
                '14
                If String.IsNullOrEmpty(objIG.EmailCliente) Then
                    ConnNM.AgregarParametro("p_EMAIL_CLIENTE", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_EMAIL_CLIENTE", objIG.EmailCliente, OracleDbType.Varchar2, objIG.EmailCliente.Length, ParameterDirection.Input)
                End If
                '15
                If String.IsNullOrEmpty(objIG.IdSucursal) Then
                    ConnNM.AgregarParametro("p_ID_SUCURSAL", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_SUCURSAL", objIG.IdSucursal, OracleDbType.Varchar2, objIG.IdSucursal.ToString.Length, ParameterDirection.Input)
                End If
                '16
                If String.IsNullOrEmpty(objIG.IdTipoComprobante) Then
                    ConnNM.AgregarParametro("p_ID_TIPO_DE_COMPROBANTE", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_TIPO_DE_COMPROBANTE", objIG.IdTipoComprobante, OracleDbType.Varchar2, objIG.IdTipoComprobante.Length, ParameterDirection.Input)
                End If
                '17
                If String.IsNullOrEmpty(objIG.Trae_FComision) Then
                    ConnNM.AgregarParametro("p_TRAE_FCOMISION", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_TRAE_FCOMISION", objIG.Trae_FComision, OracleDbType.Varchar2, objIG.Trae_FComision.ToString.Length, ParameterDirection.Input)
                End If
                '18
                If String.IsNullOrEmpty(objIG.Facturar_en_Pseudo) Then
                    ConnNM.AgregarParametro("p_FACTURAR_EN_PSEUDO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_FACTURAR_EN_PSEUDO", objIG.Facturar_en_Pseudo, OracleDbType.Varchar2, objIG.Facturar_en_Pseudo.Length, ParameterDirection.Input)
                End If
                '19
                If String.IsNullOrEmpty(objIG.Id_Punto_Facturar) Then
                    ConnNM.AgregarParametro("p_ID_PUNTO_FACTURAR", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_PUNTO_FACTURAR", objIG.Id_Punto_Facturar, OracleDbType.Varchar2, objIG.Id_Punto_Facturar.Length, ParameterDirection.Input)
                End If
                '20
                If String.IsNullOrEmpty(objIG.Id_Sucursal_Facturar) Then
                    ConnNM.AgregarParametro("p_ID_SUCURSAL_FACTURAR", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_SUCURSAL_FACTURAR", objIG.Id_Sucursal_Facturar, OracleDbType.Varchar2, objIG.Id_Sucursal_Facturar.Length, ParameterDirection.Input)
                End If
                '21
                If String.IsNullOrEmpty(objIG.No_ImprimeComprobante) Then
                    ConnNM.AgregarParametro("p_NO_IMPRIME_COMPROBANTE", "1", OracleDbType.Varchar2, 1, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_NO_IMPRIME_COMPROBANTE", objIG.No_ImprimeComprobante, OracleDbType.Varchar2, objIG.No_ImprimeComprobante.ToString.Length, ParameterDirection.Input)
                End If
                '22
                If String.IsNullOrEmpty(objIG.Como_Emitio) Then
                    ConnNM.AgregarParametro("p_COMO_EMITIO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_COMO_EMITIO", objIG.Como_Emitio, OracleDbType.Varchar2, objIG.Como_Emitio.ToString.Length, ParameterDirection.Input)
                End If
                '23
                If String.IsNullOrEmpty(objIG.Como_Solicito) Then
                    ConnNM.AgregarParametro("p_COMO_SOLICITO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_COMO_SOLICITO", objIG.Como_Solicito, OracleDbType.Varchar2, objIG.Como_Solicito.ToString.Length, ParameterDirection.Input)
                End If
                '24
                If String.IsNullOrEmpty(objIG.es_EmisionAutonoma) Then
                    ConnNM.AgregarParametro("p_EMISION_AUTONOMA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_EMISION_AUTONOMA", objIG.es_EmisionAutonoma, OracleDbType.Varchar2, objIG.es_EmisionAutonoma.ToString.Length, ParameterDirection.Input)
                End If
                '25
                If String.IsNullOrEmpty(objIG.Apellido_Paterno) Then
                    ConnNM.AgregarParametro("p_APELLIDO_PATERNO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_APELLIDO_PATERNO", objIG.No_ImprimeComprobante, OracleDbType.Varchar2, objIG.Apellido_Paterno.Length, ParameterDirection.Input)
                End If
                '26
                If String.IsNullOrEmpty(objIG.Apellido_Materno) Then
                    ConnNM.AgregarParametro("p_APELLIDO_MATERNO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_APELLIDO_MATERNO", objIG.Apellido_Materno, OracleDbType.Varchar2, objIG.Apellido_Materno.Length, ParameterDirection.Input)
                End If
                '27
                If String.IsNullOrEmpty(objIG.Nombres) Then
                    ConnNM.AgregarParametro("p_NOMBRES", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_NOMBRES", objIG.Nombres, OracleDbType.Varchar2, objIG.Nombres.Length, ParameterDirection.Input)
                End If
                '28
                If String.IsNullOrEmpty(objIG.DireccionFacturar) Then
                    ConnNM.AgregarParametro("p_DIRECCION_A_FC", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_DIRECCION_A_FC", objIG.DireccionFacturar, OracleDbType.Varchar2, objIG.DireccionFacturar.Length, ParameterDirection.Input)
                End If
                '29
                If String.IsNullOrEmpty(objIG.Quien_Solicita) Then
                    ConnNM.AgregarParametro("p_QUIEN_SOLICITA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_QUIEN_SOLICITA", objIG.Quien_Solicita, OracleDbType.Varchar2, objIG.Quien_Solicita.Length, ParameterDirection.Input)
                End If
                '30
                If String.IsNullOrEmpty(objIG.id_file) Then
                    ConnNM.AgregarParametro("p_ID_FILE", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_FILE", objIG.id_file, OracleDbType.Varchar2, objIG.id_file.Length, ParameterDirection.Input)
                End If
                '31
                If String.IsNullOrEmpty(objIG.Texto_FC_1) Then
                    ConnNM.AgregarParametro("p_TEXTO_FC_1", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_TEXTO_FC_1", objIG.Texto_FC_1, OracleDbType.Varchar2, objIG.Texto_FC_1.Length, ParameterDirection.Input)
                End If
                '32
                If String.IsNullOrEmpty(objIG.Texto_FC_2) Then
                    ConnNM.AgregarParametro("p_TEXTO_FC_2", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_TEXTO_FC_2", objIG.Texto_FC_2, OracleDbType.Varchar2, objIG.Texto_FC_2.Length, ParameterDirection.Input)
                End If
                '33
                If String.IsNullOrEmpty(objIG.Texto_FC_3) Then
                    ConnNM.AgregarParametro("p_TEXTO_FC_3", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_TEXTO_FC_3", objIG.Texto_FC_3, OracleDbType.Varchar2, objIG.Texto_FC_3.Length, ParameterDirection.Input)
                End If
                '34
                If String.IsNullOrEmpty(objIG.Texto_FC_4) Then
                    ConnNM.AgregarParametro("p_TEXTO_FC_4", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_TEXTO_FC_4", objIG.Texto_FC_4, OracleDbType.Varchar2, objIG.Texto_FC_4.Length, ParameterDirection.Input)
                End If
                '35
                If String.IsNullOrEmpty(objIG.id_empresa) Then
                    ConnNM.AgregarParametro("p_ID_EMPRESA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_EMPRESA", objIG.id_empresa, OracleDbType.Varchar2, objIG.id_empresa.Length, ParameterDirection.Input)
                End If
                '36
                If String.IsNullOrEmpty(objIG.id_forma_de_pago) Then
                    ConnNM.AgregarParametro("p_ID_FORMA_DE_PAGO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_FORMA_DE_PAGO", objIG.id_forma_de_pago, OracleDbType.Varchar2, objIG.id_forma_de_pago.Length, ParameterDirection.Input)
                End If
                '37
                If String.IsNullOrEmpty(objIG.con_morosidad) Then
                    ConnNM.AgregarParametro("p_CON_MOROSIDAD", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_CON_MOROSIDAD", objIG.con_morosidad, OracleDbType.Varchar2, objIG.con_morosidad.Length, ParameterDirection.Input)
                End If
                '38
                If String.IsNullOrEmpty(objIG.id_iata_boleto) Then
                    ConnNM.AgregarParametro("p_ID_IATA_BOLETO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_IATA_BOLETO", objIG.id_iata_boleto, OracleDbType.Varchar2, objIG.id_iata_boleto.Length, ParameterDirection.Input)
                End If
                '39
                If String.IsNullOrEmpty(objIG.nombre_titular_tarjeta) Then
                    ConnNM.AgregarParametro("p_NOMBRE_TITULAR_TARJETA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_NOMBRE_TITULAR_TARJETA", objIG.nombre_titular_tarjeta, OracleDbType.Varchar2, objIG.nombre_titular_tarjeta.Length, ParameterDirection.Input)
                End If
                '40
                If String.IsNullOrEmpty(objIG.tipo_doc_titular_tarjeta) Then
                    ConnNM.AgregarParametro("p_TIPO_DOC_TITULAR_TARJETA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_TIPO_DOC_TITULAR_TARJETA", objIG.tipo_doc_titular_tarjeta, OracleDbType.Varchar2, objIG.tipo_doc_titular_tarjeta.Length, ParameterDirection.Input)
                End If
                '41
                If String.IsNullOrEmpty(objIG.nro_doc_titular_tarjeta) Then
                    ConnNM.AgregarParametro("p_NRO_DOC_TITULAR_TARJETA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_NRO_DOC_TITULAR_TARJETA", objIG.nro_doc_titular_tarjeta, OracleDbType.Varchar2, objIG.nro_doc_titular_tarjeta.Length, ParameterDirection.Input)
                End If
                '42
                If String.IsNullOrEmpty(objIG.id_pais_emision_tarjeta) Then
                    ConnNM.AgregarParametro("p_ID_PAIS_EMISION_TARJETA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_PAIS_EMISION_TARJETA", objIG.id_pais_emision_tarjeta, OracleDbType.Varchar2, objIG.id_pais_emision_tarjeta.Length, ParameterDirection.Input)
                End If
                '43
                If String.IsNullOrEmpty(objIG.nombre_banco_tarjeta) Then
                    ConnNM.AgregarParametro("p_NOMBRE_BANCO_TARJETA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_NOMBRE_BANCO_TARJETA", objIG.nombre_banco_tarjeta, OracleDbType.Varchar2, objIG.nombre_banco_tarjeta.Length, ParameterDirection.Input)
                End If
                '44
                If String.IsNullOrEmpty(objIG.id_tipo_de_waiver) Then
                    ConnNM.AgregarParametro("p_ID_TIPO_DE_WAIVER", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_TIPO_DE_WAIVER", objIG.id_tipo_de_waiver, OracleDbType.Varchar2, objIG.id_tipo_de_waiver.Length, ParameterDirection.Input)
                End If
                '45
                If String.IsNullOrEmpty(objIG.Factor_meta) Then
                    ConnNM.AgregarParametro("p_FACTOR_META", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_FACTOR_META", objIG.Factor_meta, OracleDbType.Varchar2, objIG.Factor_meta.Length, ParameterDirection.Input)
                End If
                '46
                If String.IsNullOrEmpty(objIG.quien_autoriza) Then
                    ConnNM.AgregarParametro("p_QUIEN_AUTORIZA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_QUIEN_AUTORIZA", objIG.quien_autoriza, OracleDbType.Varchar2, objIG.quien_autoriza.Length, ParameterDirection.Input)
                End If
                '47
                If String.IsNullOrEmpty(objIG.agente_confirma) Then
                    ConnNM.AgregarParametro("p_AGENTE_CONFIRMA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_AGENTE_CONFIRMA", objIG.agente_confirma, OracleDbType.Varchar2, objIG.agente_confirma.Length, ParameterDirection.Input)
                End If
                '48
                If String.IsNullOrEmpty(objIG.id_factura_cabeza) Then
                    ConnNM.AgregarParametro("p_ID_FACTURA_CABEZA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_FACTURA_CABEZA", objIG.id_factura_cabeza, OracleDbType.Varchar2, objIG.id_factura_cabeza.Length, ParameterDirection.Input)
                End If
                '49
                If String.IsNullOrEmpty(objIG.numero_serie) Then
                    ConnNM.AgregarParametro("p_NUMERO_SERIE", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_NUMERO_SERIE", objIG.numero_serie, OracleDbType.Varchar2, objIG.numero_serie.Length, ParameterDirection.Input)
                End If
                '50
                If String.IsNullOrEmpty(objIG.codigo_vendedorweb) Then
                    ConnNM.AgregarParametro("p_CODIGO_VENDEDORWEB", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_CODIGO_VENDEDORWEB", objIG.codigo_vendedorweb, OracleDbType.Varchar2, objIG.codigo_vendedorweb.Length, ParameterDirection.Input)
                End If
                '51
                If String.IsNullOrEmpty(objIG.id_cotizacion) Then
                    ConnNM.AgregarParametro("p_ID_COTIZACION", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_COTIZACION", objIG.id_cotizacion, OracleDbType.Varchar2, objIG.id_cotizacion.Length, ParameterDirection.Input)
                End If
                '52
                If String.IsNullOrEmpty(objIG.cot_autorizada) Then
                    ConnNM.AgregarParametro("p_COT_AUTORIZADA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_COT_AUTORIZADA", objIG.cot_autorizada, OracleDbType.Varchar2, objIG.cot_autorizada.Length, ParameterDirection.Input)
                End If

                bolResultado = ConnNM._InsertExecuteNonQuery()
                '----------

            Catch ex As Exception

                bolResultado = False
                strLog = "Stored Procedure : " & Constantes.spTURBO_INS_INTERFACEGENERAL & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "Inserta_Interface_General" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "Inserta_Interface_General", strCodigoSeguimiento)

                Err.Raise(13, "Inserta_Interface_General", ex.ToString)


            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function

        Public Function Inserta_Interface_Detalle(ByVal objID As classInterfaceDetalle, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean = False

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spTURBO_INS_INTERFACEDETALLE, Constantes.StoredProcedure)
                '1
                If String.IsNullOrEmpty(objID.Referencia) Then
                    ConnNM.AgregarParametro("p_ID_REFERENCIA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_REFERENCIA", objID.Referencia, OracleDbType.Varchar2, objID.Referencia.Length, ParameterDirection.Input)
                End If
                '2
                If String.IsNullOrEmpty(objID.Secuencia) Then
                    ConnNM.AgregarParametro("p_ID_SECUENCIA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_SECUENCIA", objID.Secuencia, OracleDbType.Varchar2, objID.Secuencia.ToString.Length, ParameterDirection.Input)
                End If
                '3
                If String.IsNullOrEmpty(objID.TipoDescuento) Then
                    ConnNM.AgregarParametro("p_TIPO_DESCUENTO", "P", OracleDbType.Varchar2, 1, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_TIPO_DESCUENTO", objID.TipoDescuento, OracleDbType.Varchar2, objID.TipoDescuento.Length, ParameterDirection.Input)
                End If
                '4
                If String.IsNullOrEmpty(objID.Descuento) Then
                    ConnNM.AgregarParametro("p_DESCUENTO", "0", OracleDbType.Varchar2, 1, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_DESCUENTO", objID.Descuento, OracleDbType.Varchar2, objID.Descuento.ToString.Length, ParameterDirection.Input)
                End If
                '5
                If String.IsNullOrEmpty(objID.conGasto_Emision) Then
                    ConnNM.AgregarParametro("p_CON_GASTO_EMISION", "1", OracleDbType.Varchar2, 1, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_CON_GASTO_EMISION", objID.conGasto_Emision, OracleDbType.Varchar2, objID.conGasto_Emision.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.es_Waiver) Then
                    ConnNM.AgregarParametro("p_ES_WAIVER", "0", OracleDbType.Varchar2, 1, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ES_WAIVER", objID.es_Waiver, OracleDbType.Varchar2, objID.es_Waiver.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.en_PNR) Then
                    ConnNM.AgregarParametro("p_EN_PNR", "1", OracleDbType.Varchar2, 1, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_EN_PNR", objID.en_PNR, OracleDbType.Varchar2, objID.en_PNR.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.id_proveedor_GDS) Then
                    ConnNM.AgregarParametro("p_ID_PROVEEDOR_GDS", "6000", OracleDbType.Varchar2, 4, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_PROVEEDOR_GDS", objID.id_proveedor_GDS, OracleDbType.Varchar2, objID.id_proveedor_GDS.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.id_SecuenciaInfante) Then
                    ConnNM.AgregarParametro("p_ID_SECUENCIA_INFANTE", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_SECUENCIA_INFANTE", objID.id_SecuenciaInfante, OracleDbType.Varchar2, objID.id_SecuenciaInfante.ToString.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.IdPax) Then
                    ConnNM.AgregarParametro("p_ID_PAX", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_PAX", objID.IdPax, OracleDbType.Varchar2, objID.IdPax.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.es_IT) Then
                    ConnNM.AgregarParametro("p_ES_IT", "0", OracleDbType.Varchar2, 1, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ES_IT", objID.es_IT, OracleDbType.Varchar2, objID.es_IT.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.QuienAutorizaDcto) Then
                    ConnNM.AgregarParametro("p_QUIEN_AUTORIZA_DSCTO_AD", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_QUIEN_AUTORIZA_DSCTO_AD", objID.QuienAutorizaDcto, OracleDbType.Varchar2, objID.QuienAutorizaDcto.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.es_tourcode_automatico) Then
                    ConnNM.AgregarParametro("p_ES_TOURCODE_AUTOMATICO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ES_TOURCODE_AUTOMATICO", objID.es_tourcode_automatico, OracleDbType.Varchar2, objID.es_tourcode_automatico.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.Factor_Meta) Then
                    ConnNM.AgregarParametro("p_FACTOR_META", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_FACTOR_META", objID.Factor_Meta, OracleDbType.Varchar2, objID.Factor_Meta.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.Over) Then
                    ConnNM.AgregarParametro("p_PORC_OVER_LINEA_AEREA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_PORC_OVER_LINEA_AEREA", objID.Over, OracleDbType.Varchar2, objID.Over.ToString.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.Tarifa_No) Then
                    ConnNM.AgregarParametro("p_NUMERO_TARIFARIO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_NUMERO_TARIFARIO", objID.Tarifa_No, OracleDbType.Varchar2, objID.Tarifa_No.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.Comision_No) Then
                    ConnNM.AgregarParametro("p_NUMERO_REGULACION", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_NUMERO_REGULACION", objID.Comision_No, OracleDbType.Varchar2, objID.Comision_No.ToString.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.NaceCancelado) Then
                    ConnNM.AgregarParametro("p_OVER_NACE_CANCELADO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_OVER_NACE_CANCELADO", objID.NaceCancelado, OracleDbType.Varchar2, objID.NaceCancelado.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.es_InfanteAdulto) Then
                    ConnNM.AgregarParametro("p_ES_INFANTE_ADULTO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ES_INFANTE_ADULTO", objID.es_InfanteAdulto, OracleDbType.Varchar2, objID.es_InfanteAdulto.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.numero_de_boleto) Then
                    ConnNM.AgregarParametro("p_NUMERO_DE_BOLETO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_NUMERO_DE_BOLETO", objID.numero_de_boleto, OracleDbType.Varchar2, objID.numero_de_boleto.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.sin_facturar) Then
                    ConnNM.AgregarParametro("p_SIN_FACTURAR", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_SIN_FACTURAR", objID.sin_facturar, OracleDbType.Varchar2, objID.sin_facturar.ToString.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.importe_waiver) Then
                    ConnNM.AgregarParametro("p_IMPORTE_WAIVER", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IMPORTE_WAIVER", objID.importe_waiver, OracleDbType.Varchar2, objID.importe_waiver.ToString.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.es_conexion) Then
                    ConnNM.AgregarParametro("p_ES_CONEXION", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ES_CONEXION", objID.es_conexion, OracleDbType.Varchar2, objID.es_conexion.ToString.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.tarifa_auxiliar) Then
                    ConnNM.AgregarParametro("p_TARIFA_AUXILIAR", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_TARIFA_AUXILIAR", objID.tarifa_auxiliar, OracleDbType.Varchar2, objID.tarifa_auxiliar.ToString.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.importe_fee) Then
                    ConnNM.AgregarParametro("p_IMPORTE_FEE", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IMPORTE_FEE", objID.importe_fee, OracleDbType.Varchar2, objID.importe_fee.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.tipo_de_waiver) Then
                    ConnNM.AgregarParametro("p_TIPO_DE_WAIVER", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_TIPO_DE_WAIVER", objID.tipo_de_waiver, OracleDbType.Varchar2, objID.tipo_de_waiver.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.tarifa_adicional) Then
                    ConnNM.AgregarParametro("p_TARIFA_ADICIONAL", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_TARIFA_ADICIONAL", objID.tarifa_adicional, OracleDbType.Varchar2, objID.tarifa_adicional.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.id_grupo_aereo) Then
                    ConnNM.AgregarParametro("p_ID_GRUPO_AEREO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_GRUPO_AEREO", objID.id_grupo_aereo, OracleDbType.Varchar2, objID.id_grupo_aereo.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objID.id_cotizacion_pax) Then
                    ConnNM.AgregarParametro("p_ID_COTIZACION_PAX", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ID_COTIZACION_PAX", objID.id_cotizacion_pax, OracleDbType.Varchar2, objID.id_cotizacion_pax.Length, ParameterDirection.Input)
                End If

                bolResultado = ConnNM._InsertExecuteNonQuery()
                '----------

            Catch ex As Exception

                bolResultado = False
                strLog = "Stored Procedure : " & Constantes.spTURBO_INS_INTERFACEDETALLE & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "Inserta_Interface_Detalle" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "Inserta_Interface_Detalle", strCodigoSeguimiento)

                Err.Raise(13, "Inserta_Interface_Detalle", ex.ToString)


            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function

        Public Function ObtenerSecuenciaReferencia(ByVal strCodigoSeguimiento As String, _
                                                   ByVal intFirmaDB As Integer, _
                                                   ByVal intEsquema As Integer) As String

            Dim valor As String = ""
            Dim oConn As MyConnectionOracle
            Dim dv As DataView
            'Año y mes actual (de la BD o del sistema)
            Dim intAñoAct As Integer = 0
            Dim intMesAct As Integer = 0

            'Año y mes de la referencia
            Dim intAñoRef As Integer = 0
            Dim intMesRef As Integer = 0
            'Año y Mes final
            Dim intAñoFinal As Integer = 0
            Dim intMesFinal As Integer = 0
            'Referencia / Secuencia
            Dim strReferencia As String = ""
            Dim intSecuencia As Integer

            Dim strReferenciaFinal As String
            Dim intSecuenciaFinal As Integer

            Dim cmd As OracleCommand
            Dim cmdSQ As OracleCommand
            Dim datareader As OracleDataReader

            Dim strSelect As String, strSelectRef As String

            'Recoger esquema
            Dim strEsquema As String = "ALTER SESSION SET CURRENT_SCHEMA=NUEVOMUNDO"

            Try
                'Obtener la fecha actual de la base ó del sistema
                oConn = New MyConnectionOracle
                oConn.Connect(intFirmaDB)
                oConn.SP_Command(strEsquema, Constantes.SentenciaText)
                oConn._ExecuteNonQuery()
                'oConn = Nothing
                'cmdSQ = New OracleCommand(strEsquema, oConn)
                'cmdSQ.CommandTimeout = 200
                'cmdSQ.ExecuteNonQuery()
                'cmdSQ = Nothing

                strSelect = "SELECT to_char(sysdate,'mm') Mes, to_char(sysdate,'yyyy') Anio FROM DUAL"

                'oConn = New MyConnectionOracle
                'oConn.Connect(iCnx)
                oConn.SP_Command(strSelect, Constantes.SentenciaText)
                datareader = oConn._ExecuteReader

                'cmd = New OracleCommand(strSelect, oConn.conn)
                'cmd.CommandTimeout = 200
                'datareader = cmd.ExecuteReader(CommandBehavior.SingleResult)

                'Validación de los datos obtenidos desde la base...
                While datareader.Read
                    'Recuperar mes / año de la base desde la BD
                    intMesAct = datareader("Mes")
                    intAñoAct = datareader("Anio")
                End While

                If intMesAct = 0 Then
                    intMesAct = Now.Month
                End If

                If intAñoAct = 0 Then
                    intAñoAct = Now.Year
                End If


                'Obtener la referencia actual de la BD
                strSelectRef = "SELECT id_referencia, ultimo_numero_usado FROM INTERFACE_NUMERACION"
                'oConn.Disconnect()
                'oConn = Nothing
                'oConn = New MyConnectionOracle
                'oConn.Connect(iCnx)
                oConn.SP_Command(strSelectRef, Constantes.SentenciaText)
                datareader = oConn._ExecuteReader

                'cmd = Nothing
                'cmd = New OracleCommand(strSelectRef, oConn.conn)
                'cmd.CommandTimeout = 200
                'datareader = cmd.ExecuteReader(CommandBehavior.SingleResult)

                While datareader.Read
                    'Obtener la referencia / secuencia en caso existan registros...
                    strReferencia = datareader("id_referencia")
                    intSecuencia = datareader("ultimo_numero_usado")
                End While

                'Validación de los datos
                If strReferencia.Trim <> "" Then
                    ''Obtener la referencia / secuencia en caso existan registros...
                    'strReferencia = Convert.ToString(dv.Item(0).Item(0))
                    'intSecuencia = Convert.ToInt64(dv.Item(0).Item(1))
                    If IsNumeric(strReferencia) = True And IsNumeric(intSecuencia) = True Then
                        'Año / Mes de la referencia
                        intAñoRef = Convert.ToInt64(strReferencia.Substring(0, 4))
                        intMesRef = Convert.ToInt64(strReferencia.Substring(4))
                        'Evaluar los datos obtenidos
                        If intAñoRef = intAñoAct Then
                            'Coinciden los años (REF / ACTUAL)...??
                            If intMesRef = intMesAct Then
                                'Coinciden los meses (REF / ACTUAL)...??
                                intAñoFinal = intAñoRef : intMesFinal = intMesRef
                                'Aumentar la secuencia
                                intSecuencia += 1
                            Else
                                'Cambio el mes (REF / ACTUAL)...???
                                intAñoFinal = intAñoRef : intMesFinal = intMesRef + 1
                                intSecuencia = 1
                            End If
                        Else
                            'Cambió de año (REF / ACTUAL)...? - Aumentar el año, reiniciar el mes, reiniciar la secuencia
                            intAñoFinal = intAñoRef + 1 : intMesFinal = 1 : intSecuencia = 1
                        End If

                        strReferenciaFinal = intAñoFinal.ToString()                     ' Agregar el año - 2007
                        If intMesFinal < 10 Then                                        ' Mes menor que 10?
                            strReferenciaFinal &= "0" & intMesFinal.ToString()            ' 2007 + 09
                        Else                                                            ' Caso contrario?
                            strReferenciaFinal &= intMesFinal.ToString()                  ' 2007 + 11
                        End If
                        intSecuenciaFinal = intSecuencia                                ' Asignar secuencia final
                        valor = strReferenciaFinal & "/" & intSecuenciaFinal.ToString() ' Asignar valor final

                        'Nueva logica ************************************************************************************************************************
                        Dim booActualizoReferencia As Boolean = False
                        Dim booSecuenciaInvalida As Boolean = False
                        'Dim objIgral As New PtaDO.Business.Interface_General
                        Dim intUltimoNo As Integer = 0
                        Do
                            Try
                                'Resetear los indicadores
                                booActualizoReferencia = False : intUltimoNo = 0
                                Dim strActualiza As String = "UPDATE INTERFACE_NUMERACION SET id_referencia = '" & strReferenciaFinal & "', ultimo_numero_usado = " & intSecuenciaFinal.ToString() & ""
                                'Actualizar la numeracion / Obtener el ultimo numero usado
                                oConn.SP_Command(strActualiza, Constantes.SentenciaText)
                                oConn._UpdateExecuteNonQuery()
                                booActualizoReferencia = oConn._UpdateExecuteNonQuery()
                                'booActualizoReferencia = oConn.ExecuteUpdate("UPDATE INTERFACE_NUMERACION SET id_referencia = '" & strReferenciaFinal & "', ultimo_numero_usado = " & intSecuenciaFinal.ToString() & "")
                                Dim sSQL As String = "SELECT id_secuencia FROM INTERFACE_GENERAL" & _
                                                        " WHERE rownum=1 and id_referencia = '" & strReferenciaFinal & "'" & _
                                                        " ORDER BY id_secuencia DESC"

                                oConn.SP_Command(sSQL, Constantes.SentenciaText)
                                datareader = oConn._ExecuteReader

                                While datareader.Read
                                    intUltimoNo = datareader("id_secuencia")
                                End While

                                'intUltimoNo = Convert.ToInt64(objIgral.SeleccionarUltimaSecuencia(strReferenciaFinal))
                            Catch ex As Exception
                                'WriteToLog("ObtenerSecuenciaReferencia() " & ex.Message)
                            Finally
                                'Verificar que el numero actualizado sea el ultimo en Interface_General / Aumentar la secuencia en (1)
                                If Not intSecuenciaFinal > intUltimoNo Then
                                    intSecuenciaFinal += 1
                                    booSecuenciaInvalida = True
                                Else
                                    valor = strReferenciaFinal & "/" & intSecuenciaFinal.ToString() ' Asignar valor final
                                    booSecuenciaInvalida = False
                                End If
                                'Destruir conexion
                                'oConn = Nothing
                            End Try
                        Loop While (booActualizoReferencia = True) And (booSecuenciaInvalida = True)
                        '*************************************************************************************************************************************
                    Else
                        Throw New Exception("Datos facturación de PTA no son válidos " & strReferencia & "/" & intSecuencia.ToString())
                    End If
                Else
                    Throw New Exception("No se obtuvo Ref. Facturación desde PTA")
                End If

            Catch ex As Exception
                strLog = "Método : ObtenerSecuenciaReferencia" & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerSecuenciaReferencia" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerSecuenciaReferencia", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerSecuenciaReferencia", ex.ToString)
            Finally
                intFirmaDB = Nothing
                intEsquema = Nothing
                oConn.Disconnect()
                oConn = Nothing
            End Try
            Return valor
        End Function

        Public Function ObtenerSucursalXPseudo(ByVal Pseudo As String, _
                                               ByVal strCodigoSeguimiento As String, _
                                               ByVal intFirmaDB As Integer, _
                                               ByVal intEsquema As Integer) As String

            Dim valor As String = ""
            Dim oConn As MyConnectionOracle
            Dim dv As DataView
            'Año y mes actual (de la BD o del sistema)

            Dim strRpta As String

            Dim cmd As OracleCommand
            Dim cmdSQ As OracleCommand
            Dim datareader As OracleDataReader

            Dim strSelect As String, strSelectRef As String

            'Recoger esquema
            Dim strEsquema As String = "ALTER SESSION SET CURRENT_SCHEMA=NUEVOMUNDO"

            Try
                'Obtener la fecha actual de la base ó del sistema
                oConn = New MyConnectionOracle
                oConn.Connect(intFirmaDB)
                oConn.SP_Command(strEsquema, Constantes.SentenciaText)
                oConn._ExecuteNonQuery()
                'oConn = Nothing

                strSelect = "select * from nuevomundo.punto_de_emision pe where pe.id_pseudo_city like '" & Pseudo & "' AND PE.ES_DEPENDIENTE = 0"

                oConn.SP_Command(strSelect, Constantes.SentenciaText)
                datareader = oConn._ExecuteReader

                'Validación de los datos obtenidos desde la base...
                While datareader.Read
                    strRpta = datareader("id_sucursal")
                End While


            Catch ex As Exception
                strRpta = ""
                strLog = "Método : ObtenerSucursalXPseudo" & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerSucursalXPseudo" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerSucursalXPseudo", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerSucursalXPseudo", ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                oConn.Disconnect()
                oConn = Nothing
            End Try
            Return strRpta
        End Function
        Public Function ObtenerPseudo(ByVal strID_TRANSPORTADOR As String, _
                                      ByVal strPseudoOriginal As String, _
                                      ByVal strCodigoSeguimiento As String, _
                                      ByVal intFirmaDB As Integer, _
                                      ByVal intEsquema As Integer) As String

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim strRespuesta As String = strPseudoOriginal
            Try
                ConnNM.Connect(intFirmaDB)
                If intEsquema = 6 Then
                    ConnNM.SP_Command(Constantes.spCONSULTA_GDS_PSEUDO, Constantes.StoredProcedure)
                Else
                    ConnNM.SP_Command(Constantes.spCONSULTA_GDS_PSEUDO, Constantes.StoredProcedure)
                End If

                ConnNM.AgregarParametro("@p_ID_TRANSPORTADOR", strID_TRANSPORTADOR, OracleDbType.NVarchar2, strID_TRANSPORTADOR.Length, ParameterDirection.Input)

                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    strRespuesta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PSEUDO", Nothing)
                End While


            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spCONSULTA_GDS_PSEUDO & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerPseudo" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerPseudo", strCodigoSeguimiento)
                Err.Raise(14, "ObtenerPseudo", ex.ToString)
            Finally
                ConnNM.Disconnect()
                strID_TRANSPORTADOR = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return strRespuesta
        End Function

        Public Function ObtenerIataPorPseudo(ByVal strPseudo As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As String
            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim strRespuesta As String = Nothing
            Try
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spBUSCA_IATA, Constantes.StoredProcedure)

                ConnNM.AgregarParametro("@p_Pseudo", strPseudo, OracleDbType.NVarchar2, strPseudo.Length, ParameterDirection.Input)

                ConnNM.AgregarParametro("@p_Iata", Nothing, OracleDbType.NVarchar2, 20, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()
                strRespuesta = ConnNM.LeeParametros("@p_Iata", Nothing)

                'While objOracleDataReader.Read
                '    strRespuesta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PSEUDO", Nothing)
                'End While


            Catch ex As Exception
                strLog = "Método : ObtenerIataPorPseudo" & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerIataPorPseudo" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerIataPorPseudo", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerIataPorPseudo", ex.ToString)
            Finally
                ConnNM.Disconnect()
                strPseudo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return strRespuesta
        End Function


        Public Function ObtenerPerfilImpresoraPseudo(ByVal strPseudo As String, _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As String
            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim strRespuesta As String = Nothing
            Try
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spPERFIL_IMPRESORA_PSEUDO, Constantes.StoredProcedure)

                ConnNM.AgregarParametro("@p_Pseudo", strPseudo, OracleDbType.NVarchar2, strPseudo.Length, ParameterDirection.Input)

                ConnNM.AgregarParametro("@p_Perfil", Nothing, OracleDbType.NVarchar2, 5, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()
                strRespuesta = ConnNM.LeeParametros("@p_Perfil", Nothing)

                'While objOracleDataReader.Read
                '    strRespuesta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PSEUDO", Nothing)
                'End While


            Catch ex As Exception
                strLog = "Método : ObtenerPerfilImpresoraPseudo" & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerPerfilImpresoraPseudo" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerPerfilImpresoraPseudo", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerPerfilImpresoraPseudo", ex.ToString)
            Finally
                ConnNM.Disconnect()
                strPseudo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return strRespuesta
        End Function

        Public Function ObtenerStockBoleto(ByVal strPseudoConsulta As String, _
                                           ByVal strPseudoEmision As String, _
                                           ByVal intIdWeb As Integer, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As String
            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim strRespuesta As String = Nothing
            Try
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spSTOCK_BOLETO, Constantes.StoredProcedure)

                ConnNM.AgregarParametro("@p_Pseudo_Consulta", strPseudoConsulta, OracleDbType.NVarchar2, strPseudoConsulta.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Pseudo_Emision", strPseudoEmision, OracleDbType.NVarchar2, strPseudoEmision.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Id_Web", intIdWeb, OracleDbType.Double, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Id_Stock", Nothing, OracleDbType.NVarchar2, 8, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()
                strRespuesta = ConnNM.LeeParametros("@p_Id_Stock", Nothing)

                'While objOracleDataReader.Read
                '    strRespuesta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PSEUDO", Nothing)
                'End While


            Catch ex As Exception
                strLog = "Método : ObtenerStockBoleto" & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerStockBoleto" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerStockBoleto", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerStockBoleto", ex.ToString)
            Finally
                ConnNM.Disconnect()
                strPseudoConsulta = Nothing
                strPseudoEmision = Nothing
                intIdWeb = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return strRespuesta
        End Function

#Region "BD_WEB"
        Public Function ObtenerWebSucursalPunto(ByVal intUsuarioWeb As Integer, _
                                                ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As classSucursalPunto

            'Punto = 14
            'Sucursal = 2

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objSucursalPunto As classSucursalPunto = Nothing
            Dim auxSucursalPunto As classSucursalPunto = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spSP_OBTIENE_PTO_EMISION_USU, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("pNumIdUsuWeb_in", intUsuarioWeb, OracleDbType.Int64, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("pCurResult_out", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objSucursalPunto = New classSucursalPunto

                    objSucursalPunto.Punto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PUNTO_EMISION_PTA", Nothing)
                    objSucursalPunto.Sucursal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_SUCURSAL_EMISION_PTA", Nothing)

                    If String.IsNullOrEmpty(objSucursalPunto.Punto) Or String.IsNullOrEmpty(objSucursalPunto.Sucursal) Then
                        objSucursalPunto = Nothing
                        auxSucursalPunto = Nothing
                        Exit While
                    Else
                        auxSucursalPunto = ObtenerWebDatosSucursalPunto(objSucursalPunto.Sucursal, objSucursalPunto.Punto, strCodigoSeguimiento, intFirmaDB, intEsquema)

                        If auxSucursalPunto IsNot Nothing Then
                            objSucursalPunto.EmailCaja = auxSucursalPunto.EmailCaja
                            objSucursalPunto.Nombre = auxSucursalPunto.Nombre
                        End If
                    End If

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spSP_OBTIENE_PTO_EMISION_USU & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Usuario: " & Convert.ToString(intUsuarioWeb) & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerWebSucursalPunto" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerWebSucursalPunto", strCodigoSeguimiento)

                Err.Raise(14, "ObtenerWebSucursalPunto", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                intUsuarioWeb = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            If objSucursalPunto Is Nothing Then
                objSucursalPunto = New classSucursalPunto
                objSucursalPunto.Punto = Convert.ToString(14)
                objSucursalPunto.Sucursal = Convert.ToString(2)
                objSucursalPunto.Nombre = "INTERAGENCIAS - OF. JORGE CHAVEZ"
                objSucursalPunto.EmailCaja = "cajapardo@gruponuevomundo.com.pe"
            End If

            Return objSucursalPunto

        End Function
        Public Function ObtenerHorariosWeb(ByVal strCondicionCliente As String, _
                                           ByVal intTipoConsulta As Integer, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As String

            'Punto = 14
            'Sucursal = 2

            Dim ConnNM As New MyConnectionOracle
            Dim intRespuesta As Integer = -1
            Dim strMensajeRespuesta As String = Nothing

            Dim strRespuesta As String = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spSP_OBTIENE_HORARIO_WEB, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("p_Condicion", strCondicionCliente, OracleDbType.Varchar2, 4, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_TipoConsulta", intTipoConsulta, OracleDbType.Int64, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("p_Resultado", Nothing, OracleDbType.Int64, 1, ParameterDirection.Output)
                ConnNM.AgregarParametro("P_Mensaje", Nothing, OracleDbType.Varchar2, 100, ParameterDirection.Output)

                ConnNM._ExecuteReader()

                intRespuesta = CInt(ConnNM.LeeParametros("p_Resultado", -1))
                strMensajeRespuesta = ConnNM.LeeParametros("P_Mensaje", Nothing)

                strRespuesta = CStr(intRespuesta) & Constantes.Slash & strMensajeRespuesta


            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spSP_OBTIENE_HORARIO_WEB & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerHorariosWeb" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerHorariosWeb", strCodigoSeguimiento)

                Err.Raise(14, "ObtenerHorariosWeb", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strCondicionCliente = Nothing
                intTipoConsulta = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                intRespuesta = Nothing
                strMensajeRespuesta = Nothing
            End Try

            Return strRespuesta

        End Function
#End Region
    End Class
End Namespace