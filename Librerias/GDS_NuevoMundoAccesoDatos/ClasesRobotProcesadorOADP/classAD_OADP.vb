Imports GDS_NuevoMundoPersistencia
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Namespace ObjetoAccesoDatos
    Partial Public Class classDAO
        Public Function InsertaGDS_DWLIST_OADP(ByVal objDWLIST As classDWLIST, _
                                               ByVal strCodigoSeguimiento As String, _
                                               ByVal intFirmaDB As Integer, _
                                               ByVal intEsquema As Integer) As Boolean


            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean

            Try

                ConnNM.Connect(intFirmaDB)

                ConnNM.SP_Command(Constantes.spINSERTA_DWLIST, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("p_Pseudo", objDWLIST.PSEUDO, OracleDbType.Varchar2, objDWLIST.PSEUDO.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Item", objDWLIST.ITEM, OracleDbType.Varchar2, objDWLIST.ITEM.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Tipo", objDWLIST.TIPO, OracleDbType.Varchar2, objDWLIST.TIPO.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Pnr", objDWLIST.PNR, OracleDbType.Varchar2, objDWLIST.PNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_CodigoCliente", objDWLIST.DK, OracleDbType.Varchar2, objDWLIST.DK.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Document", objDWLIST.DOCUMENT, OracleDbType.Varchar2, objDWLIST.DOCUMENT.Length, ParameterDirection.Input)

                If String.IsNullOrEmpty(objDWLIST.INDICADOR) Then
                    ConnNM.AgregarParametro("p_Indicador", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_Indicador", objDWLIST.INDICADOR, OracleDbType.Varchar2, objDWLIST.INDICADOR.Length, ParameterDirection.Input)
                End If

                ConnNM.AgregarParametro("p_Fecha", objDWLIST.FECHA, OracleDbType.Date, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_NumTicket", IIf(String.IsNullOrEmpty(objDWLIST.NUM_TICKET), "", objDWLIST.NUM_TICKET), OracleDbType.Varchar2, objDWLIST.NUM_TICKET.Length, ParameterDirection.Input)

                If String.IsNullOrEmpty(objDWLIST.NOMBRE_PAX) Then
                    ConnNM.AgregarParametro("p_NombrePax", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_NombrePax", objDWLIST.NOMBRE_PAX, OracleDbType.Varchar2, objDWLIST.NOMBRE_PAX.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objDWLIST.APELLIDO_PAX) Then
                    ConnNM.AgregarParametro("p_ApelidoPax", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_ApelidoPax", objDWLIST.APELLIDO_PAX, OracleDbType.Varchar2, objDWLIST.APELLIDO_PAX.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objDWLIST.FIRMA_AGENTE) Then
                    ConnNM.AgregarParametro("p_FirmaAgente", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_FirmaAgente", objDWLIST.FIRMA_AGENTE, OracleDbType.Varchar2, objDWLIST.FIRMA_AGENTE.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(objDWLIST.RUTA_CARPETA) Then
                    ConnNM.AgregarParametro("p_RutaCarpeta", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_RutaCarpeta", objDWLIST.RUTA_CARPETA, OracleDbType.Varchar2, objDWLIST.RUTA_CARPETA.Length, ParameterDirection.Input)
                End If



                ConnNM.AgregarParametro("p_EnviaCorre", objDWLIST.ENVIA_CORREO, OracleDbType.Int64, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Imprime", objDWLIST.IMPRIME, OracleDbType.Int64, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_ActualizaTPR", objDWLIST.ACTUALIZA_TPR, OracleDbType.Int64, 0, ParameterDirection.Input)

                '----------
                bolResultado = ConnNM._InsertExecuteNonQuery()


            Catch ex As Exception

                bolResultado = Nothing
                strLog = "Stored Procedure : " & Constantes.spINSERTA_DWLIST & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "InsertaGDS_DWLIST_OADP" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "InsertaGDS_DWLIST_OADP", strCodigoSeguimiento)

                If Not ex.ToString.ToUpper.Contains("VIOLADA") Then
                    Err.Raise(13, "InsertaGDS_DWLIST_OADP", ex.ToString)
                End If

            Finally
                ConnNM.Disconnect()
                objDWLIST = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function
        Public Function ObtieneListadoDWLIST(ByVal strFiltro As String, _
                                             ByVal strFecha As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As List(Of classDWLIST)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing

            Dim objDWLIST As List(Of classDWLIST) = Nothing
            Dim auxDWLIST As classDWLIST = Nothing


            Try

                ConnNM.Connect(intFirmaDB)

                ConnNM.SP_Command(Constantes.spLISTADO_DWLIST, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("p_Filtro", strFiltro, OracleDbType.Varchar2, strFiltro.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Fecha", strFecha, OracleDbType.Varchar2, strFecha.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxDWLIST = New classDWLIST
                    auxDWLIST.PSEUDO = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PSEUDO", Nothing)
                    auxDWLIST.ITEM = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ITEM", Nothing)
                    auxDWLIST.TIPO = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TIPO", Nothing)
                    auxDWLIST.PNR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PNR", Nothing)
                    auxDWLIST.DK = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DK", Nothing)
                    auxDWLIST.DOCUMENT = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DOCUMENT", Nothing)
                    auxDWLIST.INDICADOR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "INDICADOR", Nothing)
                    auxDWLIST.FECHA = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA", Nothing)
                    auxDWLIST.NUM_TICKET = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUM_TICKET", Nothing)
                    auxDWLIST.NOMBRE_PAX = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE_PAX", Nothing)
                    auxDWLIST.APELLIDO_PAX = ConnNM.LeeColumnasDataReader(objOracleDataReader, "APELLIDO_PAX", Nothing)
                    auxDWLIST.FIRMA_AGENTE = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FIRMA_AGENTE", Nothing)
                    auxDWLIST.RUTA_CARPETA = ConnNM.LeeColumnasDataReader(objOracleDataReader, "RUTA_CARPETA", Nothing)
                    auxDWLIST.ENVIA_CORREO = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ENVIA_CORREO", 0)
                    auxDWLIST.IMPRIME = ConnNM.LeeColumnasDataReader(objOracleDataReader, "IMPRIME", 0)
                    auxDWLIST.ACTUALIZA_TPR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ACTUALIZA_TPR", 0)

                    If objDWLIST Is Nothing Then objDWLIST = New List(Of classDWLIST)
                    objDWLIST.Add(auxDWLIST)
                    auxDWLIST = Nothing
                End While


            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spBOLETO_DWLIST & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtieneListadoDWLIST", strCodigoSeguimiento)

                Err.Raise(14, "ObtieneBoletoDWLIST", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strFiltro = Nothing
                strFecha = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing


            End Try

            Return objDWLIST

        End Function
        Public Function ObtieneBoletoDWLIST(ByVal strPseudo As String, _
                                            ByVal strDocument As String, _
                                            ByVal strTipo As String, _
                                            ByVal strPNR As String, _
                                            ByVal strCodigoCliente As String, _
                                            ByVal strIndicador As String, _
                                            ByVal strFecha As Date, _
                                            ByVal strNumBoleto As String, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim intRespuesta As Integer = -1

            Try

                ConnNM.Connect(intFirmaDB)

                ConnNM.SP_Command(Constantes.spBOLETO_DWLIST, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("p_Pseudo", strPseudo, OracleDbType.Varchar2, strPseudo.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Document", strDocument, OracleDbType.Varchar2, strDocument.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Tipo", strTipo, OracleDbType.Varchar2, strTipo.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Pnr", strPNR, OracleDbType.Varchar2, strPNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_CodigoCliente", strCodigoCliente, OracleDbType.Varchar2, strCodigoCliente.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Indicador", strIndicador, OracleDbType.Varchar2, strIndicador.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Fecha", strFecha, OracleDbType.Date, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_NumTicket", strNumBoleto, OracleDbType.Varchar2, strNumBoleto.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("p_Resultado", Nothing, OracleDbType.Int64, 0, ParameterDirection.Output)


                ConnNM._ExecuteReader()

                intRespuesta = CInt(ConnNM.LeeParametros("p_Resultado", -1))


            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spBOLETO_DWLIST & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtieneBoletoDWLIST", strCodigoSeguimiento)

                Err.Raise(14, "ObtieneBoletoDWLIST", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strPseudo = Nothing
                strDocument = Nothing
                strTipo = Nothing
                strPNR = Nothing
                strCodigoCliente = Nothing
                strIndicador = Nothing
                strFecha = Nothing
                strNumBoleto = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return intRespuesta

        End Function
        Public Function VerificaRegistroDWLIST(ByVal strPseudo As String, _
                                               ByVal strDocument As String, _
                                               ByVal strTipo As String, _
                                               ByVal strPNR As String, _
                                               ByVal strCodigoCliente As String, _
                                               ByVal strIndicador As String, _
                                               ByVal strFecha As Date, _
                                               ByVal strCodigoSeguimiento As String, _
                                               ByVal intFirmaDB As Integer, _
                                               ByVal intEsquema As Integer) As Boolean

            Dim ConnNM As New MyConnectionOracle
            Dim intRespuesta As Integer = -1

            Try

                ConnNM.Connect(intFirmaDB)

                ConnNM.SP_Command(Constantes.spVERIFICA_DWLIST, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("p_Pseudo", strPseudo, OracleDbType.Varchar2, strPseudo.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Document", strDocument, OracleDbType.Varchar2, strDocument.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Tipo", strTipo, OracleDbType.Varchar2, strTipo.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Pnr", strPNR, OracleDbType.Varchar2, strPNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_CodigoCliente", strCodigoCliente, OracleDbType.Varchar2, strCodigoCliente.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Indicador", strIndicador, OracleDbType.Varchar2, strIndicador.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Fecha", strFecha, OracleDbType.Date, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("p_Resultado", Nothing, OracleDbType.Int64, 0, ParameterDirection.Output)

                ConnNM._ExecuteReader()

                intRespuesta = CInt(ConnNM.LeeParametros("p_Resultado", -1))


            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spVERIFICA_DWLIST & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "VerificaRegistroDWLIST", strCodigoSeguimiento)

                Err.Raise(14, "VerificaRegistroDWLIST", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strPseudo = Nothing
                strDocument = Nothing
                strTipo = Nothing
                strPNR = Nothing
                strCodigoCliente = Nothing
                strIndicador = Nothing
                strFecha = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return intRespuesta

        End Function
        Public Function ActualizaMarcaGDS_DWLIST_OADP(ByVal objDWLIST As classDWLIST, _
                                                      ByVal strCampo As String, _
                                                      ByVal intValor As Integer, _
                                                      ByVal strCodigoSeguimiento As String, _
                                                      ByVal intFirmaDB As Integer, _
                                                      ByVal intEsquema As Integer) As Boolean


            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean

            Try

                ConnNM.Connect(intFirmaDB)

                ConnNM.SP_Command(Constantes.spUPDATE_DWLIST, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("p_Pseudo", objDWLIST.PSEUDO, OracleDbType.Varchar2, objDWLIST.PSEUDO.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Document", objDWLIST.DOCUMENT, OracleDbType.Varchar2, objDWLIST.DOCUMENT.Length, ParameterDirection.Input)

                If String.IsNullOrEmpty(objDWLIST.TIPO) Then
                    ConnNM.AgregarParametro("p_Tipo", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_Tipo", objDWLIST.TIPO, OracleDbType.Varchar2, objDWLIST.TIPO.Length, ParameterDirection.Input)
                End If

                ConnNM.AgregarParametro("p_Pnr", objDWLIST.PNR, OracleDbType.Varchar2, objDWLIST.PNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_CodigoCliente", objDWLIST.DK, OracleDbType.Varchar2, objDWLIST.DK.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Indicador", objDWLIST.INDICADOR, OracleDbType.Varchar2, objDWLIST.INDICADOR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Fecha", objDWLIST.FECHA, OracleDbType.Date, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_NumTicket", objDWLIST.NUM_TICKET, OracleDbType.Varchar2, objDWLIST.NUM_TICKET.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Campos", strCampo, OracleDbType.Varchar2, strCampo.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Valor", intValor, OracleDbType.Int64, 0, ParameterDirection.Input)

                '----------
                bolResultado = ConnNM._InsertExecuteNonQuery()


            Catch ex As Exception
                bolResultado = Nothing
                strLog = "Stored Procedure : " & Constantes.spUPDATE_DWLIST & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ActualizaMarcaGDS_DWLIST_OADP" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ActualizaMarcaGDS_DWLIST_OADP", strCodigoSeguimiento)

                Err.Raise(13, "ActualizaMarcaGDS_DWLIST_OADP", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objDWLIST = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function

        Public Function VerificaTURBO_PASSENGER_RECEIPT(ByVal strTicketNumber As String, _
                                                        ByVal strPnrCode As String, _
                                                        ByVal strDkNumber As String, _
                                                        ByVal strCodigoSeguimiento As String, _
                                                        ByVal intFirmaDB As Integer, _
                                                        ByVal intEsquema As Integer) As Integer

            Dim ConnNM As New MyConnectionOracle
            Dim intRespuesta As Integer = -1

            Try

                ConnNM.Connect(intFirmaDB)

                If intEsquema = 6 Then
                    ConnNM.SP_Command(Constantes.spVERIFICA_TPR_DM, Constantes.StoredProcedure)
                Else
                    ConnNM.SP_Command(Constantes.spVERIFICA_TPR, Constantes.StoredProcedure)
                End If

                ConnNM.AgregarParametro("p_TicketNumber", strTicketNumber, OracleDbType.Varchar2, strTicketNumber.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_PnrCode", strPnrCode, OracleDbType.Varchar2, strPnrCode.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_DkNumber", strDkNumber, OracleDbType.Int64, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("p_Respuesta", Nothing, OracleDbType.Int64, 0, ParameterDirection.Output)

                ConnNM._ExecuteReader()

                intRespuesta = CInt(ConnNM.LeeParametros("p_Respuesta", -1))


            Catch ex As Exception

                If intEsquema = 6 Then
                    strLog = "Stored Procedure : " & Constantes.spVERIFICA_TPR_DM & vbCrLf
                Else
                    strLog = "Stored Procedure : " & Constantes.spVERIFICA_TPR & vbCrLf
                End If

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "VerificaTURBO_PASSENGER_RECEIPT", strCodigoSeguimiento)

                Err.Raise(14, "VerificaTURBO_PASSENGER_RECEIPT", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strTicketNumber = Nothing
                strPnrCode = Nothing
                strDkNumber = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return intRespuesta

        End Function
        Public Function SinDocumentoTURBO_PASSENGER_RECEIPT(ByVal strTicketNumber As String, _
                                                            ByVal strPnrCode As String, _
                                                            ByVal strDkNumber As String, _
                                                            ByVal strCodigoSeguimiento As String, _
                                                            ByVal intFirmaDB As Integer, _
                                                            ByVal intEsquema As Integer) As Integer

            Dim ConnNM As New MyConnectionOracle
            Dim intRespuesta As Integer = -1

            Try

                ConnNM.Connect(intFirmaDB)

                If intEsquema = 6 Then
                    ConnNM.SP_Command(Constantes.spSINDOCUMENT_TPR_DM, Constantes.StoredProcedure)
                Else
                    ConnNM.SP_Command(Constantes.spSINDOCUMENT_TPR, Constantes.StoredProcedure)
                End If

                ConnNM.AgregarParametro("p_TicketNumber", strTicketNumber, OracleDbType.Varchar2, strTicketNumber.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_PnrCode", strPnrCode, OracleDbType.Varchar2, strPnrCode.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_DkNumber", strDkNumber, OracleDbType.Int64, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("p_Respuesta", Nothing, OracleDbType.Int64, 0, ParameterDirection.Output)

                ConnNM._ExecuteReader()

                intRespuesta = CInt(ConnNM.LeeParametros("p_Respuesta", -1))


            Catch ex As Exception

                If intEsquema = 6 Then
                    strLog = "Stored Procedure : " & Constantes.spSINDOCUMENT_TPR_DM & vbCrLf
                Else
                    strLog = "Stored Procedure : " & Constantes.spSINDOCUMENT_TPR & vbCrLf
                End If

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "SinDocumentoTURBO_PASSENGER_RECEIPT", strCodigoSeguimiento)

                Err.Raise(14, "SinDocumentoTURBO_PASSENGER_RECEIPT", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strTicketNumber = Nothing
                strPnrCode = Nothing
                strDkNumber = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return intRespuesta

        End Function
        Public Function VerificaTURBO_CC_CHARGE_FORM(ByVal strTicketNumber As String, _
                                                     ByVal strPnrCode As String, _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As Integer


            Dim ConnNM As New MyConnectionOracle
            Dim intRespuesta As Integer = -1

            Try

                ConnNM.Connect(intFirmaDB)

                If intEsquema = 6 Then
                    ConnNM.SP_Command(Constantes.spCONSULTA_CCCF_DM, Constantes.StoredProcedure)
                Else
                    ConnNM.SP_Command(Constantes.spCONSULTA_CCCF, Constantes.StoredProcedure)
                End If

                ConnNM.AgregarParametro("p_Pnr", strTicketNumber, OracleDbType.Varchar2, strTicketNumber.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Boleto", strPnrCode, OracleDbType.Varchar2, strPnrCode.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("p_Respuesta", Nothing, OracleDbType.Int64, 0, ParameterDirection.Output)

                ConnNM._ExecuteReader()

                intRespuesta = CInt(ConnNM.LeeParametros("p_Respuesta", -1))


            Catch ex As Exception

                If intEsquema = 6 Then
                    strLog = "Stored Procedure : " & Constantes.spVERIFICA_TPR_DM & vbCrLf
                Else
                    strLog = "Stored Procedure : " & Constantes.spVERIFICA_TPR & vbCrLf
                End If

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "VerificaTURBO_CC_CHARGE_FORM", strCodigoSeguimiento)

                Err.Raise(14, "VerificaTURBO_CC_CHARGE_FORM", ex.Message)

            Finally
                ConnNM.Disconnect()
                strTicketNumber = Nothing
                strPnrCode = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return intRespuesta

        End Function
        Public Function InsertaTURBO_PASSENGER_RECEIPT(ByVal strTicketNumber As String, _
                                                       ByVal strPnrCode As String, _
                                                       ByVal strDkNumber As String, _
                                                       ByVal strRucNumber As String, _
                                                       ByVal strPcc As String, _
                                                       ByVal strCounterTA As String, _
                                                       ByVal strCuerpoDocumento As String, _
                                                       ByVal strPasajeroNombre As String, _
                                                       ByVal strPasajeroApellido As String, _
                                                       ByVal intIdHeader As Integer, _
                                                       ByVal strCounterEmail As String, _
                                                       ByVal strFreqTravel As String, _
                                                       ByVal strRuta As String, _
                                                       ByVal strCodigoSeguimiento As String, _
                                                       ByVal intFirmaDB As Integer, _
                                                       ByVal intEsquema As Integer) As Boolean


            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean

            Try

                ConnNM.Connect(intFirmaDB)

                If intEsquema = 6 Then
                    ConnNM.SP_Command(Constantes.spINSERTA_TPR_DM, Constantes.StoredProcedure)
                Else
                    ConnNM.SP_Command(Constantes.spINSERTA_TPR, Constantes.StoredProcedure)
                End If

                ConnNM.AgregarParametro("p_TicketNumber", strTicketNumber, OracleDbType.Varchar2, strTicketNumber.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_PnrCode", strPnrCode, OracleDbType.Varchar2, strPnrCode.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_DkNumber", strDkNumber, OracleDbType.Varchar2, strDkNumber.Length, ParameterDirection.Input)
                If String.IsNullOrEmpty(strRucNumber) Then
                    ConnNM.AgregarParametro("p_RucNumber", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_RucNumber", strRucNumber, OracleDbType.Varchar2, strRucNumber.Length, ParameterDirection.Input)
                End If

                ConnNM.AgregarParametro("p_Pcc", strPcc, OracleDbType.Varchar2, strPcc.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_CounterTA", strCounterTA, OracleDbType.Varchar2, strCounterTA.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_CuerpoDocumento", strCuerpoDocumento, OracleDbType.Clob, 0, ParameterDirection.Input)
                If String.IsNullOrEmpty(strPasajeroNombre) Then
                    ConnNM.AgregarParametro("p_PasajeroNombre", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_PasajeroNombre", strPasajeroNombre, OracleDbType.Varchar2, strPasajeroNombre.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(strPasajeroApellido) Then
                    ConnNM.AgregarParametro("p_PasajeroApellido", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_PasajeroApellido", strPasajeroApellido, OracleDbType.Varchar2, strPasajeroApellido.Length, ParameterDirection.Input)
                End If

                ConnNM.AgregarParametro("p_IdHeader", intIdHeader, OracleDbType.Int16, 0, ParameterDirection.Input)

                If String.IsNullOrEmpty(strCounterEmail) Then
                    ConnNM.AgregarParametro("p_CounterEmail", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_CounterEmail", strCounterEmail, OracleDbType.Varchar2, strCounterEmail.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(strFreqTravel) Then
                    ConnNM.AgregarParametro("p_FreqTravel", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_FreqTravel", strFreqTravel, OracleDbType.Varchar2, strFreqTravel.Length, ParameterDirection.Input)
                End If

                If String.IsNullOrEmpty(strRuta) Then
                    ConnNM.AgregarParametro("p_Ruta", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_Ruta", strRuta, OracleDbType.Varchar2, strRuta.Length, ParameterDirection.Input)
                End If


                '----------
                bolResultado = ConnNM._InsertExecuteNonQuery()


            Catch ex As Exception

                bolResultado = Nothing

                If intEsquema = 6 Then
                    strLog = "Stored Procedure : " & Constantes.spINSERTA_TPR_DM & vbCrLf
                Else
                    strLog = "Stored Procedure : " & Constantes.spINSERTA_TPR & vbCrLf
                End If

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "InsertaTURBO_PASSENGER_RECEIPT" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.Message & vbCrLf
                objEscribeLog.WriteLog(strLog, "InsertaTURBO_PASSENGER_RECEIPT", strCodigoSeguimiento)

                If Not ex.ToString.ToUpper.Contains("VIOLADA") Then
                    Err.Raise(13, "InsertaTURBO_PASSENGER_RECEIPT", ex.ToString)
                End If

            Finally
                ConnNM.Disconnect()
                strTicketNumber = Nothing
                strPnrCode = Nothing
                strDkNumber = Nothing
                strRucNumber = Nothing
                strPcc = Nothing
                strCounterTA = Nothing
                strCuerpoDocumento = Nothing
                strPasajeroNombre = Nothing
                strPasajeroApellido = Nothing
                intIdHeader = Nothing
                strCounterEmail = Nothing
                strFreqTravel = Nothing
                strRuta = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function
        Public Function ActualizaTURBO_PASSENGER_RECEIPT(ByVal strTicketNumber As String, _
                                                         ByVal strPnrCode As String, _
                                                         ByVal strDkNumber As String, _
                                                         ByVal strCuerpoDocumento As String, _
                                                         ByVal strRuta As String, _
                                                         ByVal strCodigoSeguimiento As String, _
                                                         ByVal intFirmaDB As Integer, _
                                                         ByVal intEsquema As Integer) As Boolean


            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean

            Try

                ConnNM.Connect(intFirmaDB)

                If intEsquema = 6 Then
                    ConnNM.SP_Command(Constantes.spUPDATE_TRP_DM, Constantes.StoredProcedure)
                Else
                    ConnNM.SP_Command(Constantes.spUPDATE_TRP, Constantes.StoredProcedure)
                End If


                ConnNM.AgregarParametro("p_TicketNumber", strTicketNumber, OracleDbType.Varchar2, strTicketNumber.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_PnrCode", strPnrCode, OracleDbType.Varchar2, strPnrCode.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_DkNumber", strDkNumber, OracleDbType.Varchar2, strDkNumber.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_CuerpoDocumento", strCuerpoDocumento, OracleDbType.Clob, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Ruta", strRuta, OracleDbType.Varchar2, strRuta.Length, ParameterDirection.Input)
                '----------
                bolResultado = ConnNM._InsertExecuteNonQuery()


            Catch ex As Exception
                bolResultado = Nothing

                If intEsquema = 6 Then
                    strLog = "Stored Procedure : " & Constantes.spUPDATE_TRP_DM & vbCrLf
                Else
                    strLog = "Stored Procedure : " & Constantes.spUPDATE_TRP & vbCrLf
                End If

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ActualizaTURBO_PASSENGER_RECEIPT" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ActualizaTURBO_PASSENGER_RECEIPT", strCodigoSeguimiento)

                Err.Raise(13, "ActualizaTURBO_PASSENGER_RECEIPT", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strTicketNumber = Nothing
                strPnrCode = Nothing
                strDkNumber = Nothing
                strCodigoSeguimiento = Nothing
                strCuerpoDocumento = Nothing
                strRuta = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function
        Public Function TPR_BUSCAR(ByVal strPnr As String, _
                                   ByVal strDK As String, _
                                   ByVal strTicketNumber As String, _
                                   ByVal strOpcion As String, _
                                   ByVal strCodigoSeguimiento As String, _
                                   ByVal intFirmaDB As Integer, _
                                   ByVal intEsquema As Integer) As List(Of classTurboPassengerRecipt)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing

            Dim lstTurboPassengerRecipt As List(Of classTurboPassengerRecipt) = Nothing
            Dim auxTurboPassengerRecipt As classTurboPassengerRecipt = Nothing
            Dim strCadenaAuxiliar As String = Nothing
            Try


                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spTPR_BUSCAR, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Pnr", strPnr, OracleDbType.Varchar2, strPnr.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Dk", strDK, OracleDbType.Varchar2, strDK.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_NumeroTicket", strTicketNumber, OracleDbType.Varchar2, strTicketNumber.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Opcion", strOpcion, OracleDbType.Varchar2, strOpcion.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)

                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxTurboPassengerRecipt = New classTurboPassengerRecipt
                    strCadenaAuxiliar = Nothing

                    auxTurboPassengerRecipt.Ticket_Number = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TICKET_NUMBER", Nothing)
                    auxTurboPassengerRecipt.Pnr_Code = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PNR_CODE", Nothing)
                    auxTurboPassengerRecipt.Dk_Number = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DK_NUMBER", Nothing)
                    auxTurboPassengerRecipt.Ruc_Number = ConnNM.LeeColumnasDataReader(objOracleDataReader, "RUC_NUMBER", Nothing)
                    auxTurboPassengerRecipt.Print_Flag = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PRINT_FLAG", 0)
                    auxTurboPassengerRecipt.Pcc = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PCC", Nothing)
                    auxTurboPassengerRecipt.Counter_Ta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COUNTER_TA", Nothing)
                    auxTurboPassengerRecipt.Fecha_Alta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA_ALTA", Nothing)
                    auxTurboPassengerRecipt.Printed_Flag = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PRINTED_FLAG", 0)
                    auxTurboPassengerRecipt.Cuerpo_Documento = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CUERPO_DOCUMENTO", Nothing)
                    'If Not String.IsNullOrEmpty(auxTurboPassengerRecipt.Cuerpo_Documento) Then
                    '    strCadenaAuxiliar = auxTurboPassengerRecipt.Cuerpo_Documento
                    '    strCadenaAuxiliar = strCadenaAuxiliar.Replace("NAME:", "#NAME:")

                    '    If strCadenaAuxiliar.Split("#").Length > 1 Then
                    '        strCadenaAuxiliar = " " & strCadenaAuxiliar.Split("#")(1)
                    '    End If

                    '    strCadenaAuxiliar = strCadenaAuxiliar.Replace("&nbsp;", " ")
                    '    strCadenaAuxiliar = strCadenaAuxiliar.Replace("<br>", vbCrLf)
                    '    auxTurboPassengerRecipt.Cuerpo_Documento = strCadenaAuxiliar.Replace("</p>  </div>", vbCrLf)

                    'End If
                    auxTurboPassengerRecipt.Pasajero_Nombre = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PASAJERO_NOMBRE", Nothing)
                    auxTurboPassengerRecipt.Pasajero_Apellido = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PASAJERO_APELLIDO", Nothing)
                    auxTurboPassengerRecipt.Id_Header = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_HEADER", 0)
                    auxTurboPassengerRecipt.Counter_Email = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COUNTER_EMAIL", Nothing)
                    auxTurboPassengerRecipt.Email_Flag = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EMAIL_FLAG", 0)
                    auxTurboPassengerRecipt.Mailed_Flag = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MAILED_FLAG", 0)
                    auxTurboPassengerRecipt.Itinerario = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ITINERARIO", Nothing)
                    auxTurboPassengerRecipt.Venta_Personal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "VENTA_PERSONAL", 0)
                    auxTurboPassengerRecipt.Freq_Travel = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FREQ_TRAVEL", Nothing)
                    auxTurboPassengerRecipt.Cod_Aerolinea = ConnNM.LeeColumnasDataReader(objOracleDataReader, "COD_AEROLINEA", Nothing)
                    If Not String.IsNullOrEmpty(auxTurboPassengerRecipt.Cod_Aerolinea) Then
                        auxTurboPassengerRecipt.Cod_Aerolinea = auxTurboPassengerRecipt.Cod_Aerolinea.Replace("<br><table><tr><td>", "")
                        auxTurboPassengerRecipt.Cod_Aerolinea = auxTurboPassengerRecipt.Cod_Aerolinea.Replace("&nbsp;</td><td>&nbsp;", " ")
                        auxTurboPassengerRecipt.Cod_Aerolinea = auxTurboPassengerRecipt.Cod_Aerolinea.Replace("&nbsp;:</td><td>&nbsp;", ": ")
                        auxTurboPassengerRecipt.Cod_Aerolinea = auxTurboPassengerRecipt.Cod_Aerolinea.Replace("</td></tr><tr><td>", vbCrLf)
                        auxTurboPassengerRecipt.Cod_Aerolinea = auxTurboPassengerRecipt.Cod_Aerolinea.Replace(";</td><td>&nbsp;", " ")
                        auxTurboPassengerRecipt.Cod_Aerolinea = auxTurboPassengerRecipt.Cod_Aerolinea.Replace("</td></tr></table><br>", "")
                        auxTurboPassengerRecipt.Cod_Aerolinea = auxTurboPassengerRecipt.Cod_Aerolinea.Replace("|", "")
                        auxTurboPassengerRecipt.Cod_Aerolinea = auxTurboPassengerRecipt.Cod_Aerolinea.Replace("&nbsp;", "")
                    End If

                    auxTurboPassengerRecipt.Ruta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "RUTA", Nothing)

                    If lstTurboPassengerRecipt Is Nothing Then lstTurboPassengerRecipt = New List(Of classTurboPassengerRecipt)
                    lstTurboPassengerRecipt.Add(auxTurboPassengerRecipt)

                End While

            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spTPR_BUSCAR & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "TPR_LISTADO" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "TPR_LISTADO", strCodigoSeguimiento)

                Err.Raise(3, "TPR_LISTADO", ex.ToString)

            Finally

                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                strPnr = Nothing
                strDK = Nothing
                strTicketNumber = Nothing
                strOpcion = Nothing
                auxTurboPassengerRecipt = Nothing
            End Try

            Return lstTurboPassengerRecipt

        End Function
        Public Function TPR_ACTUALIZA_IMPRESION(ByVal strPnr As String, _
                                                ByVal strDK As String, _
                                                ByVal strTicketNumber As String, _
                                                ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As Boolean


            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean

            Try

                ConnNM.Connect(intFirmaDB)

                If intEsquema = 6 Then
                    ConnNM.SP_Command(Constantes.spTPR_ACTUALIZA_IMPRESION_DM, Constantes.StoredProcedure)
                Else
                    ConnNM.SP_Command(Constantes.spTPR_ACTUALIZA_IMPRESION, Constantes.StoredProcedure)
                End If


                ConnNM.AgregarParametro("p_Pnr", strPnr, OracleDbType.Varchar2, strPnr.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Dk", strDK, OracleDbType.Varchar2, strDK.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_NumeroTicket", strTicketNumber, OracleDbType.Varchar2, strTicketNumber.Length, ParameterDirection.Input)
                '----------
                bolResultado = ConnNM._InsertExecuteNonQuery()


            Catch ex As Exception
                bolResultado = Nothing

                If intEsquema = 6 Then
                    strLog = "Stored Procedure : " & Constantes.spTPR_ACTUALIZA_IMPRESION_DM & vbCrLf
                Else
                    strLog = "Stored Procedure : " & Constantes.spTPR_ACTUALIZA_IMPRESION & vbCrLf
                End If

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "TPR_ACTUALIZA_IMPRESION" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "TPR_ACTUALIZA_IMPRESION", strCodigoSeguimiento)

                Err.Raise(13, "TPR_ACTUALIZA_IMPRESION", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strTicketNumber = Nothing
                strDK = Nothing
                strCodigoSeguimiento = Nothing
                strTicketNumber = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function
        Public Function TPR_ACTUALIZA_CORREO(ByVal strPnr As String, _
                                             ByVal strDK As String, _
                                             ByVal strTicketNumber As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As Boolean


            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean

            Try

                ConnNM.Connect(intFirmaDB)

                If intEsquema = 6 Then
                    ConnNM.SP_Command(Constantes.spTPR_ACTUALIZA_CORREO_DM, Constantes.StoredProcedure)
                Else
                    ConnNM.SP_Command(Constantes.spTPR_ACTUALIZA_CORREO, Constantes.StoredProcedure)
                End If


                ConnNM.AgregarParametro("p_Pnr", strPnr, OracleDbType.Varchar2, strPnr.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_Dk", strDK, OracleDbType.Varchar2, strDK.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_NumeroTicket", strTicketNumber, OracleDbType.Varchar2, strTicketNumber.Length, ParameterDirection.Input)
                '----------
                bolResultado = ConnNM._InsertExecuteNonQuery()


            Catch ex As Exception
                bolResultado = Nothing

                If intEsquema = 6 Then
                    strLog = "Stored Procedure : " & Constantes.spTPR_ACTUALIZA_CORREO_DM & vbCrLf
                Else
                    strLog = "Stored Procedure : " & Constantes.spTPR_ACTUALIZA_CORREO & vbCrLf
                End If

                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "TPR_ACTUALIZA_CORREO" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "TPR_ACTUALIZA_CORREO", strCodigoSeguimiento)

                Err.Raise(13, "TPR_ACTUALIZA_CORREO", ex.ToString)

            Finally
                ConnNM.Disconnect()
                strTicketNumber = Nothing
                strDK = Nothing
                strCodigoSeguimiento = Nothing
                strTicketNumber = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return bolResultado
        End Function
    End Class
End Namespace
