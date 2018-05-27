Imports GDS_NuevoMundoPersistencia
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Namespace ObjetoAccesoDatos
    Partial Public Class classDAO
        Public Function ObtenerLineaCredito(ByVal intCliente As Integer, _
                                            ByVal intIdEmpresa As Integer, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As classLineaCredito

            Dim ConnNM As New MyConnectionOracle
            'Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objLineaCredito As classLineaCredito = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spCREDITO_DISPONIBLE, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@id_cliente", intCliente, OracleDbType.Double, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@id_empresa", intIdEmpresa, OracleDbType.Double, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@credito_base", 0, OracleDbType.Double, 0, ParameterDirection.Output)
                ConnNM.AgregarParametro("@pendiente", 0, OracleDbType.Double, 0, ParameterDirection.Output)
                ConnNM.AgregarParametro("@sobregiro", 0, OracleDbType.Double, 0, ParameterDirection.Output)
                ConnNM.AgregarParametro("@extension_acumulada", 0, OracleDbType.Double, 0, ParameterDirection.Output)
                ConnNM.AgregarParametro("@extension_consumida", 0, OracleDbType.Double, 0, ParameterDirection.Output)
                ConnNM.AgregarParametro("@credito_disponible", 0, OracleDbType.Double, 0, ParameterDirection.Output)

                ConnNM._ExecuteReader()

                objLineaCredito = New classLineaCredito

                strLog = "Stored Procedure : " & Constantes.spCREDITO_DISPONIBLE & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Cliente: " & CStr(intCliente).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "ID Empresa : " & CStr(intIdEmpresa).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf

                objLineaCredito.CreditoBase = ConnNM.LeeParametros("@credito_base", Convert.ToString(0))
                objLineaCredito.CreditoBase = IIf(String.IsNullOrEmpty(objLineaCredito.CreditoBase), "0.00", objLineaCredito.CreditoBase.Replace(Constantes.Coma, Constantes.Punto))
                strLog &= Constantes.TabEspacios & "objLineaCredito.CreditoBase: " & objLineaCredito.CreditoBase & vbCrLf


                objLineaCredito.Pendiente = ConnNM.LeeParametros("@pendiente", Convert.ToString(0))
                objLineaCredito.Pendiente = IIf(String.IsNullOrEmpty(objLineaCredito.Pendiente), "0.00", objLineaCredito.Pendiente.Replace(Constantes.Coma, Constantes.Punto))
                strLog &= Constantes.TabEspacios & "objLineaCredito.Pendiente: " & objLineaCredito.Pendiente & vbCrLf


                objLineaCredito.SobreGiro = ConnNM.LeeParametros("@sobregiro", Convert.ToString(0))
                objLineaCredito.SobreGiro = IIf(String.IsNullOrEmpty(objLineaCredito.SobreGiro), "0.00", objLineaCredito.SobreGiro.Replace(Constantes.Coma, Constantes.Punto))
                strLog &= Constantes.TabEspacios & "objLineaCredito.SobreGiro: " & objLineaCredito.SobreGiro & vbCrLf


                objLineaCredito.ExtAcumulada = ConnNM.LeeParametros("@extension_acumulada", Convert.ToString(0))
                objLineaCredito.ExtAcumulada = IIf(String.IsNullOrEmpty(objLineaCredito.ExtAcumulada), "0.00", objLineaCredito.ExtAcumulada.Replace(Constantes.Coma, Constantes.Punto))
                strLog &= Constantes.TabEspacios & "objLineaCredito.ExtAcumulada: " & objLineaCredito.ExtAcumulada & vbCrLf


                objLineaCredito.ExtConsumida = ConnNM.LeeParametros("@extension_consumida", Convert.ToString(0))
                objLineaCredito.ExtConsumida = IIf(String.IsNullOrEmpty(objLineaCredito.ExtConsumida), "0.00", objLineaCredito.ExtConsumida.Replace(Constantes.Coma, Constantes.Punto))
                strLog &= Constantes.TabEspacios & "objLineaCredito.ExtConsumida: " & objLineaCredito.ExtConsumida & vbCrLf


                objLineaCredito.Disponible = ConnNM.LeeParametros("@credito_disponible", Convert.ToString(0))
                objLineaCredito.Disponible = IIf(String.IsNullOrEmpty(objLineaCredito.Disponible), "0.00", objLineaCredito.Disponible.Replace(Constantes.Coma, Constantes.Punto))
                strLog &= Constantes.TabEspacios & "objLineaCredito.Disponible: " & objLineaCredito.Disponible & vbCrLf

                objEscribeLog.WriteLog(strLog, "ObtenerLineaCredito1", strCodigoSeguimiento)


            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spCREDITO_DISPONIBLE & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Cliente: " & CStr(intCliente).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "ID Empresa : " & CStr(intIdEmpresa).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "OptieneLineaCredito" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "OptieneLineaCredito", strCodigoSeguimiento)

                Err.Raise(1, "OptieneLineaCredito", ex.ToString)

            Finally
                ConnNM.Disconnect()
                'objOracleDataReader = Nothing
                intCliente = Nothing
                intIdEmpresa = Nothing
                ConnNM = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objLineaCredito

        End Function
        Public Function ObtenerDocumentosVencidos(ByVal intCliente As Integer, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As classDocumentosVencidos

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objDocumentosVencidos As classDocumentosVencidos = Nothing
            Dim objDocumentosEmitidos As List(Of classDocumentosVencidos.classDocumentosEmitidos) = Nothing
            Dim auxDocumentosEmitidos As classDocumentosVencidos.classDocumentosEmitidos = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spDOCUMENTOSVENCIDOS, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("V_ID_CLIENTE", intCliente, OracleDbType.Double, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("V_CURRESULT_OUT", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)

                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read

                    auxDocumentosEmitidos = New classDocumentosVencidos.classDocumentosEmitidos
                    auxDocumentosEmitidos.Oficina = IIf(objOracleDataReader("Oficina") Is DBNull.Value, "", objOracleDataReader("Oficina"))
                    auxDocumentosEmitidos.NumDocumento = IIf(objOracleDataReader("Documento") Is DBNull.Value, "", objOracleDataReader("Documento"))
                    auxDocumentosEmitidos.Moneda = IIf(objOracleDataReader("Moneda") Is DBNull.Value, "", objOracleDataReader("Moneda"))
                    auxDocumentosEmitidos.Total = IIf(objOracleDataReader("Total") Is DBNull.Value, 0, objOracleDataReader("Total"))
                    auxDocumentosEmitidos.Pendiente = IIf(objOracleDataReader("Pendiente") Is DBNull.Value, 0, objOracleDataReader("Pendiente"))
                    auxDocumentosEmitidos.FecVence = IIf(objOracleDataReader("Vence") Is DBNull.Value, "", objOracleDataReader("Vence"))
                    auxDocumentosEmitidos.FecEmision = IIf(objOracleDataReader("Emitio") Is DBNull.Value, "", objOracleDataReader("Emitio"))
                    auxDocumentosEmitidos.Condicion = IIf(objOracleDataReader("condicion_de_pago") Is DBNull.Value, "", objOracleDataReader("condicion_de_pago"))

                    If objDocumentosEmitidos Is Nothing Then objDocumentosEmitidos = New List(Of classDocumentosVencidos.classDocumentosEmitidos)
                    objDocumentosEmitidos.Add(auxDocumentosEmitidos)

                End While

                objDocumentosVencidos = New classDocumentosVencidos
                objDocumentosVencidos.DocumentosEmitidos = New List(Of classDocumentosVencidos.classDocumentosEmitidos)
                objDocumentosVencidos.DocumentosEmitidos = objDocumentosEmitidos


            Catch ex As Exception
                objDocumentosVencidos.DocumentosEmitidos = Nothing
                objDocumentosVencidos.TotalEmision = Nothing
                objDocumentosVencidos.TotalVencidos = Nothing
                objDocumentosVencidos.Errores = New classErroresAlertas
                objDocumentosVencidos.Errores.Errores = New List(Of String)
                objDocumentosVencidos.Errores.Errores.Add(ex.ToString)

                strLog = "Stored Procedure : " & Constantes.spDOCUMENTOSVENCIDOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Cliente: " & CStr(intCliente).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDocumentosVencidos" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerDocumentosVencidos", strCodigoSeguimiento)

                Err.Raise(2, "ObtenerDocumentosVencidos", ex.ToString)
            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                auxDocumentosEmitidos = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objDocumentosVencidos

        End Function
        Public Function ObtenerPermisos(ByVal intCliente As Integer, _
                                        ByVal intUsuarioWeb As Integer, _
                                        ByVal strCodigoSeguimiento As String, _
                                        ByVal intFirmaDB As Integer, _
                                        ByVal intEsquema As Integer) As classPermisos

            'Emision Autonoma Sabre Con Restriccion = 1384
            'Emision Autonoma Sabre Sin Restriccion = 1382

            'UsuarioWeb Hugo Sánchez Prueba = 10796

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objPermisos As classPermisos = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spPERMISOS, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Codigo", intCliente, OracleDbType.Double, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objPermisos = New classPermisos

                    objPermisos.Tarjeta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMITE_TARJETA", 0)
                    objPermisos.EnRiesgo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "RIESGO", 1)
                    objPermisos.ProhOnLine = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PROH_ONLINE", 1)
                    objPermisos.EntregaFacturaCOM = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ENTREGA_FACTURACOM", 0)
                    objPermisos.FacturaCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FACTURA_CLIENTE", 0)
                    objPermisos.BoletoPaxIt = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMITE_BVIT", 0)
                    objPermisos.AutMorosidad = ConnNM.LeeColumnasDataReader(objOracleDataReader, "AUT_MOROSIDAD", 0)
                    objPermisos.PseudoAgy = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PSEUDO_AGY", Nothing)
                    objPermisos.MultiplePCC = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MULTIPLE_PCC", Nothing)

                    If intFirmaDB = Constantes.Usr_PTA_Amadeus Then

                    ElseIf intFirmaDB = Constantes.Usr_PTA_EasyOnline Then
                        objPermisos.EAConRestricciones = PermisoWEB(intUsuarioWeb, Constantes.EASabreConRestricciones, strCodigoSeguimiento, Constantes.Usr_WEB_General, "AppWebs")
                        objPermisos.EASinRestricciones = PermisoWEB(intUsuarioWeb, Constantes.EASabreSinRestricciones, strCodigoSeguimiento, Constantes.Usr_WEB_General, "AppWebs")

                        If (objPermisos.EAConRestricciones.Equals(1)) Or (objPermisos.EASinRestricciones.Equals(1)) Then
                            objPermisos.MuestraCiudad = 1
                        Else
                            objPermisos.MuestraCiudad = 0
                        End If

                    End If


                End While




            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spPERMISOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Cliente: " & Convert.ToString(intCliente) & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerPermisos" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerPermisos", strCodigoSeguimiento)

                Err.Raise(5, "ObtenerPermisos", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objPermisos

        End Function
        Public Function ObtenerGrupoInterno(ByVal intCliente As Integer, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As classGrupoInterno

            'intTipoUsuario = 0 Cliente
            'intTipoUsuario = 1 Personal

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objGrupoInterno As classGrupoInterno = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGRUPOINTERNO, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Codigo", intCliente, OracleDbType.Double, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objGrupoInterno = New classGrupoInterno
                    objGrupoInterno.IdGrupoInterno = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_GRUPO", 0)
                    objGrupoInterno.NombreGrupo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION_GRUPO", 0)
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spDATOSCLIENTE & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Cliente: " & Convert.ToString(intCliente) & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerGrupoInterno" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerGrupoInterno", strCodigoSeguimiento)

                Err.Raise(7, "ObtenerGrupoInterno", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objGrupoInterno

        End Function
        Public Function ObtenerDatosCliente(ByVal intCliente As Integer, _
                                            ByVal strDepartamento As String, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As classCliente


            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objCliente As classCliente = Nothing
            Dim objGrupoInterno As classGrupoInterno = Nothing

            Try

                ConnNM.Connect(intFirmaDB)

                If intEsquema = 6 Then
                    ConnNM.SP_Command(Constantes.spDATOSCLIENTE_DM, Constantes.StoredProcedure)
                Else
                    ConnNM.SP_Command(Constantes.spDATOSCLIENTE, Constantes.StoredProcedure)
                End If

                ConnNM.AgregarParametro("@p_Codigo", intCliente, OracleDbType.Double, 0, ParameterDirection.Input)
                If intEsquema <> 6 Then
                    If String.IsNullOrEmpty(strDepartamento) Then strDepartamento = "COU"
                    ConnNM.AgregarParametro("@p_Departamento", strDepartamento, OracleDbType.Varchar2, strDepartamento.Length, ParameterDirection.Input)
                End If

                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objCliente = New classCliente
                    objCliente.DK = intCliente
                    objCliente.RazonSocial = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOM_RAZON", Nothing)
                    objCliente.NombreComercial = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOM_COMERCIAL", Nothing)
                    objCliente.Tipo_Documento = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TIPO_DOCUMENTO", Nothing)
                    objCliente.Documento = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DOCUMENTO", Nothing)
                    objCliente.Direccion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DIRECCION", Nothing)
                    objCliente.EmailPTA = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CORREO_ELECTRONICO", Nothing)
                    objCliente.EnDesuso = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EN_DESUSO", 1)
                    objCliente.IdEmpresa = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_EMPRESA", -1)
                    objCliente.TipoDeCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_TIPO_DE_CLIENTE", -1)
                    objCliente.Condicion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CONDICION_DE_PAGO", -1)

                    If intEsquema <> 6 Then
                        If strDepartamento.Equals("CIA") Then
                            objGrupoInterno = New classGrupoInterno
                            objGrupoInterno.IdGrupoInterno = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_GRUPO", -1)
                            objGrupoInterno.NombreGrupo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION_GRUPO", -1)
                            objCliente.GrupoInterno = objGrupoInterno
                        End If
                    End If

                    objCliente.Logo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "LOGO", 0)
                    objGrupoInterno = Nothing
                End While

            Catch ex As Exception
                If intEsquema = 6 Then
                    strLog = "Stored Procedure : " & Constantes.spDATOSCLIENTE_DM & vbCrLf
                Else
                    strLog = "Stored Procedure : " & Constantes.spDATOSCLIENTE & vbCrLf
                End If

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Cliente: " & Convert.ToString(intCliente) & vbCrLf
                strLog &= Constantes.TabEspacios & "Dpto : " & Convert.ToString(strDepartamento) & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDatosCliente" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerDatosCliente", strCodigoSeguimiento)

                Err.Raise(8, "ObtenerDatosCliente", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                intCliente = Nothing
                strDepartamento = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objCliente

        End Function
        Public Function ObtenerEmailEmisionEasyOnLine(ByVal strDK As String, _
                                                      ByVal strPNR As String, _
                                                      ByVal strNumeroBoleto As String, _
                                                      ByVal strNumeroBoletoFULL As String, _
                                                      ByVal strDepartamento As String, _
                                                      ByVal strCodigoSeguimiento As String, _
                                                      ByVal intFirmaDB As Integer, _
                                                      ByVal intEsquema As Integer) As String


            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim strEmailSTOL As String = Nothing
            Dim strEmailTPR As String = Nothing
            Dim strRespuesta As String = Nothing

            Try

                ConnNM.Connect(intFirmaDB)

                'If iCnx = 6 Then
                '   ConnNM.SP_Command(Constantes.spDATOSCLIENTE_DM, Constantes.StoredProcedure)
                'Else
                ConnNM.SP_Command(Constantes.spEMAIL_EASYONLINE, Constantes.StoredProcedure)
                'End If

                ConnNM.AgregarParametro("@p_CodigoCliente", strDK, OracleDbType.Varchar2, strDK.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Pnr", strPNR, OracleDbType.Varchar2, strPNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_TicketNumber", strNumeroBoleto, OracleDbType.Varchar2, strNumeroBoleto.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_TicketNumberFull", strNumeroBoletoFULL, OracleDbType.Varchar2, strNumeroBoletoFULL.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Departamento", strDepartamento, OracleDbType.Varchar2, strDepartamento.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read

                    If strDepartamento.Equals("COU") Then
                        strEmailSTOL = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMAIL_AGENTE_STOL", Nothing)
                        strEmailTPR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMAIL_COUNTER_TPR", Nothing)

                        If Not String.IsNullOrEmpty(strEmailSTOL) Then
                            If Not String.IsNullOrEmpty(strEmailTPR) Then

                                If strEmailSTOL.Equals(strEmailTPR) Then
                                    strRespuesta = strEmailSTOL
                                Else
                                    strRespuesta = strEmailSTOL & ";" & strEmailTPR
                                End If

                            End If
                        Else
                            If Not String.IsNullOrEmpty(strEmailTPR) Then
                                strRespuesta = strEmailTPR
                            End If
                        End If

                    ElseIf strDepartamento.Equals("CIA") Then
                        strEmailTPR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMAIL_COUNTER_TPR", Nothing)
                        If Not String.IsNullOrEmpty(strEmailTPR) Then
                            strRespuesta = strEmailTPR
                        End If
                    End If

                    If Not String.IsNullOrEmpty(strRespuesta) Then
                        strRespuesta = strRespuesta.Replace(",", ";")
                    End If

                End While

            Catch ex As Exception
                If intFirmaDB = 6 Then
                    strLog = "Stored Procedure : " & Constantes.spDATOSCLIENTE_DM & vbCrLf
                Else
                    strLog = "Stored Procedure : " & Constantes.spDATOSCLIENTE & vbCrLf
                End If

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Cliente: " & strDK & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerEmailEmisionEasyOnLine" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerEmailEmisionEasyOnLine", strCodigoSeguimiento)

                Err.Raise(8, "ObtenerEmailEmisionEasyOnLine", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                ConnNM = Nothing

                strDK = Nothing
                strPNR = Nothing
                strNumeroBoleto = Nothing
                strNumeroBoletoFULL = Nothing
                strDepartamento = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                strEmailSTOL = Nothing
                strEmailTPR = Nothing
            End Try

            Return strRespuesta

        End Function
        Public Function ObtenerSubCodigo(ByVal intCliente As Integer, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As List(Of classSubCodigo)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objSubCodigo As List(Of classSubCodigo) = Nothing
            Dim auxSubCodigo As classSubCodigo = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spSUBCODIGO, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("p_Cliente", intCliente, OracleDbType.Int32, 0, ParameterDirection.Input)
                'ConnNM.AgregarParametro("p_NomBaseDatos", strBaseDatos, OracleDbType.Varchar2, 30, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxSubCodigo = New classSubCodigo

                    auxSubCodigo.ID = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_SUBCODIGO", -1)
                    auxSubCodigo.Nombre = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DESCRIPCION", Nothing)
                    auxSubCodigo.Marca = False
                    auxSubCodigo.RequiereEjecutiva = ConnNM.LeeColumnasDataReader(objOracleDataReader, "REQUIERE_EJECUTIVA", -1)
                    If objSubCodigo Is Nothing Then objSubCodigo = New List(Of classSubCodigo)
                    objSubCodigo.Add(auxSubCodigo)
                    auxSubCodigo = Nothing
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spSUBCODIGO & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Aplicacion: " & Convert.ToString(intCliente) & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerSubCodigo" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerSubCodigo", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerSubCodigo", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                auxSubCodigo = Nothing
            End Try

            Return objSubCodigo

        End Function

        Public Function ObtenerTipoCliente(ByVal intCliente As Integer, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As classCliente


            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objCliente As classCliente = Nothing
            Dim objGrupoInterno As classGrupoInterno = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_TIPOCLIENTE, Constantes.StoredProcedure)

                ConnNM.AgregarParametro("@p_Codigo", intCliente, OracleDbType.Double, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objCliente = New classCliente
                    objCliente.DK = intCliente
                    objCliente.RazonSocial = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOM_RAZON", Nothing)
                    objCliente.TipoDeCliente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_TIPO_DE_CLIENTE", -1)
                    objCliente.Condicion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CONDICION_DE_PAGO", -1)

                End While

            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spGDS_TIPOCLIENTE & vbCrLf

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Cliente: " & Convert.ToString(intCliente) & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerTipoCliente" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerTipoCliente", strCodigoSeguimiento)

                Err.Raise(8, "ObtenerTipoCliente", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objCliente

        End Function

#Region "BD_WEB"
        Public Function PermisoWEB(ByVal intUsuario As Integer, _
                                   ByVal intOpcion As Integer, _
                                   ByVal strCodigoSeguimiento As String, _
                                   ByVal intFirmaDB As Integer, _
                                   ByVal intEsquema As Integer) As String

            Dim ConnNM As New MyConnectionOracle
            Dim oOracleParameter As New OracleParameter
            Dim oRespuesta As String = Nothing

            Try
                'usuario = 10796
                'opcion = 1382

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command("begin :resultado := " & Constantes.spFN_PERM_TIENE_ACCESO_OPC_USU & "(:pNumIdOpc_in,:pNumIdUsu_in); end;", Constantes.EjecutaFuncion)

                oOracleParameter = ConnNM.AgregarParametro("rv", Nothing, OracleDbType.Varchar2, 3, ParameterDirection.ReturnValue, True)
                '----------
                ConnNM.AgregarParametro("pNumIdOpc_in", intUsuario, OracleDbType.Int64, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("pNumIdUsu_in", intUsuario, OracleDbType.Int64, 0, ParameterDirection.Input)



                ConnNM._ExecuteNonQuery()


                oRespuesta = oOracleParameter.Value.ToString

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spFN_PERM_TIENE_ACCESO_OPC_USU & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Usuario: " & Convert.ToString(intUsuario) & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "PermisoWEB" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "PermisoWEB", strCodigoSeguimiento)

                Err.Raise(13, "PermisoWEB", ex.ToString)

            Finally
                ConnNM.Disconnect()
                intUsuario = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return oRespuesta

        End Function
#End Region
    End Class
End Namespace