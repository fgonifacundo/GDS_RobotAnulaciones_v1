Imports GDS_NuevoMundoPersistencia
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Namespace ObjetoAccesoDatos
    Partial Public Class classDAO
        Public Function ObtenerDatosPromotor(ByVal intCliente As Integer, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As classPromotor

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objPromotor As classPromotor = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spPROMOTOR, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Codigo", intCliente, OracleDbType.Double, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objPromotor = New classPromotor

                    objPromotor.Codigo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PROMOTOR", Nothing)
                    objPromotor.NombrePromotor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)
                    objPromotor.EmailPromotor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CORREO", Nothing)
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spPROMOTOR & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Cliente: " & Convert.ToString(intCliente) & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDatosPromotor" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerDatosPromotor", strCodigoSeguimiento)

                Err.Raise(4, "ObtenerDatosPromotor", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objPromotor

        End Function
        Public Function ObtenerDatosAgente(ByVal strFirmaAgente As String, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As List(Of classDatosAgente)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objDatosAgente As List(Of classDatosAgente) = Nothing
            Dim auxDatosAgente As classDatosAgente = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                If intEsquema = 6 Then
                    ConnNM.SP_Command(Constantes.spFIRMAAGENTE_DM, Constantes.StoredProcedure)
                Else
                    ConnNM.SP_Command(Constantes.spFIRMAAGENTE, Constantes.StoredProcedure)
                End If

                ConnNM.AgregarParametro("@p_idfirma", strFirmaAgente, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)

                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxDatosAgente = New classDatosAgente
                    auxDatosAgente.FirmaAgente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FIRMA_AGENTE", Nothing)
                    auxDatosAgente.IdVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_VENDEDOR", Nothing)
                    auxDatosAgente.NombreAgente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE_AGENTE", Nothing)
                    auxDatosAgente.CorreoAgente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CORREO_AGENTE", Nothing)
                    auxDatosAgente.Oficina = ConnNM.LeeColumnasDataReader(objOracleDataReader, "OFICINA", Nothing)
                    auxDatosAgente.NombreJefe = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE_JEFE", Nothing)
                    auxDatosAgente.CorreoJefe = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CORREO_JEFE", Nothing)
                    auxDatosAgente.Departamento = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DEPARTAMENTO", Nothing)

                    If objDatosAgente Is Nothing Then objDatosAgente = New List(Of classDatosAgente)
                    objDatosAgente.Add(auxDatosAgente)
                End While

            Catch ex As Exception
                objDatosAgente = Nothing
                If intEsquema = 6 Then
                    strLog = "Stored Procedure : " & Constantes.spFIRMAAGENTE_DM & vbCrLf
                Else
                    strLog = "Stored Procedure : " & Constantes.spFIRMAAGENTE & vbCrLf
                End If

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDatosAgente" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerDatosAgente", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerDatosAgente", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                auxDatosAgente = Nothing
            End Try

            Return objDatosAgente

        End Function

        Public Function ObtenerDatosAgenteDINNERS(ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As List(Of classDatosAgente)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objDatosAgente As List(Of classDatosAgente) = Nothing
            Dim auxDatosAgente As classDatosAgente = Nothing

            Try

                ConnNM.Connect(intFirmaDB)

                ConnNM.SP_Command(Constantes.spGDS_FIRMA_AGENTE_DINNERS, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)

                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxDatosAgente = New classDatosAgente
                    auxDatosAgente.FirmaAgente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FIRMA_AGENTE", Nothing)
                    auxDatosAgente.IdVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_VENDEDOR", Nothing)
                    auxDatosAgente.NombreAgente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE_AGENTE", Nothing)
                    auxDatosAgente.CorreoAgente = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CORREO_AGENTE", Nothing)
                    auxDatosAgente.Oficina = ConnNM.LeeColumnasDataReader(objOracleDataReader, "OFICINA", Nothing)
                    auxDatosAgente.NombreJefe = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE_JEFE", Nothing)
                    auxDatosAgente.CorreoJefe = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CORREO_JEFE", Nothing)
                    auxDatosAgente.Departamento = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DEPARTAMENTO", Nothing)

                    If objDatosAgente Is Nothing Then objDatosAgente = New List(Of classDatosAgente)
                    objDatosAgente.Add(auxDatosAgente)
                End While

            Catch ex As Exception
                objDatosAgente = Nothing

                strLog = "Stored Procedure : " & Constantes.spGDS_FIRMA_AGENTE_DINNERS & vbCrLf

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDatosAgenteDINNERS" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerDatosAgenteDINNERS", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerDatosAgente", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                auxDatosAgente = Nothing
            End Try

            Return objDatosAgente

        End Function

        Public Function ObtenerAgenteEmite(ByVal strTicketNumber As String, _
                                           ByVal strCodigoPNR As String, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As String

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim AgenteEmite As String = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spAGENTE_EMITE, Constantes.StoredProcedure)

                ConnNM.AgregarParametro("@p_TICKET_NUMBER", strTicketNumber, OracleDbType.Varchar2, strTicketNumber.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_COD_RESERVA", strCodigoPNR, OracleDbType.Varchar2, strCodigoPNR.Length, ParameterDirection.Input)

                ConnNM.AgregarParametro("p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)

                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    AgenteEmite = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)

                End While


            Catch ex As Exception
                AgenteEmite = Nothing
                strLog = "Stored Procedure : " & Constantes.spAGENTE_EMITE & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerAgenteEmite" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerAgenteEmite", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerAgenteEmite", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return IIf(String.IsNullOrEmpty(AgenteEmite), String.Empty, AgenteEmite)

        End Function

        Public Function ObtenerVendedorPtaDestinos(ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As List(Of classDatosAgente)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objDatosAgente As List(Of classDatosAgente) = Nothing
            Dim auxDatosAgente As classDatosAgente = Nothing

            Try

                ConnNM.Connect(intFirmaDB)

                ConnNM.SP_Command(Constantes.spGDS_DATOS_VENDEDOR, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)

                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxDatosAgente = New classDatosAgente
                    auxDatosAgente.IdFirmaVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_FIRMA", Nothing)
                    auxDatosAgente.IdVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_VENDEDOR", Nothing)
                    auxDatosAgente.NombreVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)
                    auxDatosAgente.IdDepartamentoVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_DEPARTAMENTO", Nothing)

                    If objDatosAgente Is Nothing Then objDatosAgente = New List(Of classDatosAgente)
                    objDatosAgente.Add(auxDatosAgente)
                End While

            Catch ex As Exception
                objDatosAgente = Nothing

                strLog = "Stored Procedure : " & Constantes.spGDS_DATOS_VENDEDOR & vbCrLf

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerVendedorPtaDestinos" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerVendedorPtaDestinos", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerVendedorPtaDestinos", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                auxDatosAgente = Nothing
            End Try

            Return objDatosAgente

        End Function


        Public Function ObtenerVendedorNuevoMundo(ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As List(Of classDatosAgente)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objDatosAgente As List(Of classDatosAgente) = Nothing
            Dim auxDatosAgente As classDatosAgente = Nothing

            Try

                ConnNM.Connect(intFirmaDB)

                Dim commandText = Constantes.getEsquema(intEsquema) + Constantes.spGDS_DATOS_VENDEDOR_BD

                ConnNM.SP_Command(commandText, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)

                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxDatosAgente = New classDatosAgente
                    auxDatosAgente.IdFirmaVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_FIRMA", Nothing)
                    auxDatosAgente.IdVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_VENDEDOR", Nothing)
                    auxDatosAgente.NombreVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE_VENDEDOR", Nothing)
                    auxDatosAgente.CorreoVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CORREO_VENDEDOR", Nothing)
                    auxDatosAgente.CorreoJefe = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CORREO_JEFE", Nothing)
                    auxDatosAgente.IdDepartamentoVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_DEPARTAMENTO", Nothing)

                    If objDatosAgente Is Nothing Then objDatosAgente = New List(Of classDatosAgente)
                    objDatosAgente.Add(auxDatosAgente)
                End While

            Catch ex As Exception
                'objDatosAgente = Nothing

                strLog = "Stored Procedure : " & Constantes.spGDS_DATOS_VENDEDOR_BD & vbCrLf

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerVendedorNuevoMundo" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerVendedorNuevoMundo", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerVendedorNuevoMundo", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                'auxDatosAgente = Nothing
            End Try

            Return objDatosAgente

        End Function

    End Class
End Namespace