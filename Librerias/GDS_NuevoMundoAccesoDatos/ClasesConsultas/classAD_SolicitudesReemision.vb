Imports GDS_NuevoMundoPersistencia
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Namespace ObjetoAccesoDatos
    Partial Public Class classDAO
        Public Function InsertaSolicitudReemision(ByVal objSolicitudReemision As classSolicitudReemision, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As Integer
            Dim ConnNM As New MyConnectionOracle
            Dim intCodigoReemision As Integer = 0
            Dim intPago As Integer = 0

            Try

                If objSolicitudReemision IsNot Nothing Then

                    ConnNM.Connect(intFirmaDB)

                    ConnNM.SP_Command(Constantes.spSP_INS_REE_SOLICITUD, Constantes.StoredProcedure, True)

                    ConnNM.AgregarParametro("p_IN_WEBS_CID", objSolicitudReemision.intCodigoWeb, OracleDbType.Int32, 0, ParameterDirection.Input)
                    ConnNM.AgregarParametro("p_IN_LANG_CID", objSolicitudReemision.intCodigoLenguaje, OracleDbType.Int32, 0, ParameterDirection.Input)
                    ConnNM.AgregarParametro("p_IN_USUWEB_ID", objSolicitudReemision.intCodigoUsuarioWeb, OracleDbType.Int32, 0, ParameterDirection.Input)

                    If Not String.IsNullOrEmpty(objSolicitudReemision.strCodigoPNR) Then
                        ConnNM.AgregarParametro("p_IN_REE_PNR_COD", objSolicitudReemision.strCodigoPNR, OracleDbType.Varchar2, objSolicitudReemision.strCodigoPNR.ToString.Length, ParameterDirection.Input)
                    Else
                        ConnNM.AgregarParametro("p_IN_REE_PNR_COD", Nothing, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                    End If

                    If Not String.IsNullOrEmpty(objSolicitudReemision.strTipoReserva) Then
                        ConnNM.AgregarParametro("p_IN_REE_TIPO_RESV", objSolicitudReemision.strTipoReserva, OracleDbType.Varchar2, objSolicitudReemision.strTipoReserva.ToString.Length, ParameterDirection.Input)
                    Else
                        ConnNM.AgregarParametro("p_IN_REE_TIPO_RESV", Nothing, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                    End If

                    If Not String.IsNullOrEmpty(objSolicitudReemision.strNombrePromotor) Then
                        ConnNM.AgregarParametro("p_IN_REE_PROMOTOR", objSolicitudReemision.strNombrePromotor, OracleDbType.Varchar2, objSolicitudReemision.strNombrePromotor.ToString.Length, ParameterDirection.Input)
                    Else
                        ConnNM.AgregarParametro("p_IN_REE_PROMOTOR", Nothing, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                    End If

                    ConnNM.AgregarParametro("p_IN_REE_ESTADO", objSolicitudReemision.intEstado, OracleDbType.Int32, 0, ParameterDirection.Input)
                    ConnNM.AgregarParametro("p_IN_REE_OFI_DESTINO", objSolicitudReemision.intOficinaDestinos, OracleDbType.Int32, 0, ParameterDirection.Input)

                    'If Not String.IsNullOrEmpty(objSolicitudReemision.strFechaReemision) Then
                    '    ConnNM.AgregarParametro("p_IN_REE_FECHA", objSolicitudReemision.strFechaReemision, OracleDbType.Date, objSolicitudReemision.strFechaReemision.ToString.Length, ParameterDirection.Input)
                    'Else
                    '    ConnNM.AgregarParametro("p_IN_REE_FECHA", Nothing, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                    'End If

                    If Not String.IsNullOrEmpty(objSolicitudReemision.strTipoTarifa) Then
                        ConnNM.AgregarParametro("p_IN_REE_TIPO_TARIFA", objSolicitudReemision.strTipoTarifa, OracleDbType.Varchar2, objSolicitudReemision.strTipoTarifa.ToString.Length, ParameterDirection.Input)
                    Else
                        ConnNM.AgregarParametro("p_IN_REE_TIPO_TARIFA", Nothing, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                    End If

                    ConnNM.AgregarParametro("p_IN_REE_DEP_DESTINO", objSolicitudReemision.intDepartamentoDestino, OracleDbType.Int32, 0, ParameterDirection.Input)
                    ConnNM.AgregarParametro("p_IN_REE_SIST_ORIGEN", objSolicitudReemision.intSistemaOrigen, OracleDbType.Int32, 0, ParameterDirection.Input)
                    ConnNM.AgregarParametro("p_IN_REE_ID_SUBCODIGO", objSolicitudReemision.intSubCodigo, OracleDbType.Int32, 0, ParameterDirection.Input)

                    If Not String.IsNullOrEmpty(objSolicitudReemision.strObservacion) Then
                        ConnNM.AgregarParametro("p_IN_REE_OBSERVACION", objSolicitudReemision.strObservacion, OracleDbType.NVarchar2, objSolicitudReemision.strObservacion.ToString.Length, ParameterDirection.Input)
                    Else
                        ConnNM.AgregarParametro("p_IN_REE_OBSERVACION", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                    End If

                    ConnNM.AgregarParametro("p_OUT_NumRegistro", Nothing, OracleDbType.Int32, 0, ParameterDirection.Output)


                    ConnNM._ExecuteReader()
                    intCodigoReemision = ConnNM.LeeParametros("p_OUT_NumRegistro", 0)

                    objSolicitudReemision.intCodigoReemision = intCodigoReemision

                    If objSolicitudReemision.objGenerales IsNot Nothing Then
                        InsertaGenerales(objSolicitudReemision, strCodigoSeguimiento, intFirmaDB, intEsquema, ConnNM)
                    End If

                    If objSolicitudReemision.objCalificadores IsNot Nothing Then
                        InsertaCalificadores(objSolicitudReemision, strCodigoSeguimiento, intFirmaDB, intEsquema, ConnNM)
                    End If

                    If objSolicitudReemision.objTarifa IsNot Nothing Then
                        InsertaTarifa(objSolicitudReemision, strCodigoSeguimiento, intFirmaDB, intEsquema, ConnNM)
                    End If

                    If objSolicitudReemision.objPago IsNot Nothing Then
                        intPago = InsertaPago(objSolicitudReemision, strCodigoSeguimiento, intFirmaDB, intEsquema, ConnNM)

                        If objSolicitudReemision.objPago.objDeposito IsNot Nothing Then
                            objSolicitudReemision.objPago.intCodigoPago = intPago
                            InsertaDepositos(objSolicitudReemision, strCodigoSeguimiento, intFirmaDB, intEsquema, ConnNM)
                        End If

                    End If

                    ConnNM._Commit()

                End If

            Catch ex As Exception

                ConnNM.Rollback()

                strLog = "Stored Procedure : " & Constantes.spSP_SEL_REE_DEPOSITOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "InsertaSolicitudReemision" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "InsertaSolicitudReemision", strCodigoSeguimiento)

                Err.Raise(14, "InsertaSolicitudReemision", ex.ToString)
            Finally
                ConnNM.Disconnect()
                objSolicitudReemision = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return intCodigoReemision
        End Function
        Private Sub InsertaGenerales(ByVal objSolicitudReemision As classSolicitudReemision, _
                                     ByVal strCodigoSeguimiento As String, _
                                     ByVal intFirmaDB As Integer, _
                                     ByVal intEsquema As Integer, _
                                    ByVal ConnNM As MyConnectionOracle)
            Try

                ConnNM.SP_Command(Constantes.spSP_INS_REE_GENERALES, Constantes.StoredProcedure, True)

                ConnNM.AgregarParametro("p_IN_REE_CODIGO", objSolicitudReemision.intCodigoReemision, OracleDbType.Int32, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_IN_PNR", objSolicitudReemision.strCodigoPNR, OracleDbType.NVarchar2, objSolicitudReemision.strCodigoPNR.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_IN_ITINERARIO_ORIGINAL", objSolicitudReemision.objGenerales.strItinerarioOriginal, OracleDbType.NVarchar2, objSolicitudReemision.objGenerales.strItinerarioOriginal.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_IN_ITINERARIO_REEMITIR", objSolicitudReemision.objGenerales.strItinerarioReemision, OracleDbType.NVarchar2, objSolicitudReemision.objGenerales.strItinerarioReemision.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_IN_BOLETO_ORIGINAL", objSolicitudReemision.objGenerales.strBoletoOriginal, OracleDbType.NVarchar2, objSolicitudReemision.objGenerales.strBoletoOriginal.ToString.Length, ParameterDirection.Input)

                ConnNM._InsertExecuteNonQuery(False, False)

            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spSP_INS_REE_GENERALES & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "InsertaGenerales" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "InsertaGenerales", strCodigoSeguimiento)

                Err.Raise(14, "InsertaGenerales", ex.ToString)
            Finally
                objSolicitudReemision = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try
        End Sub
        Private Sub InsertaCalificadores(ByVal objSolicitudReemision As classSolicitudReemision, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer, _
                                         ByVal ConnNM As MyConnectionOracle)
            Try

                ConnNM.SP_Command(Constantes.spSP_INS_REE_CALIFICADORES, Constantes.StoredProcedure, True)

                ConnNM.AgregarParametro("p_IN_REE_CODIGO", objSolicitudReemision.intCodigoReemision, OracleDbType.Int32, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_IN_TIPO_TARIFA", objSolicitudReemision.objCalificadores.strTipoTarifa, OracleDbType.NVarchar2, objSolicitudReemision.objCalificadores.strTipoTarifa.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_IN_TIPO_PASAJERO", objSolicitudReemision.objCalificadores.strTipoPasajero, OracleDbType.NVarchar2, objSolicitudReemision.objCalificadores.strTipoPasajero.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_IN_NUMERO_PASAJERO", objSolicitudReemision.objCalificadores.strNumeroPasajero, OracleDbType.NVarchar2, objSolicitudReemision.objCalificadores.strNumeroPasajero.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_IN_AEROLINEA", objSolicitudReemision.objCalificadores.strAerolinea, OracleDbType.NVarchar2, objSolicitudReemision.objCalificadores.strAerolinea.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_IN_NUMERO_TICKET", objSolicitudReemision.objCalificadores.strNumeroBoleto, OracleDbType.NVarchar2, objSolicitudReemision.objCalificadores.strNumeroBoleto.ToString.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_IN_SEGMENTOS", objSolicitudReemision.objCalificadores.strSegmentos, OracleDbType.NVarchar2, objSolicitudReemision.objCalificadores.strSegmentos.ToString.Length, ParameterDirection.Input)
                If Not String.IsNullOrEmpty(objSolicitudReemision.objCalificadores.strTourCode) Then
                    ConnNM.AgregarParametro("p_IN_TOUR_CODE", objSolicitudReemision.objCalificadores.strTourCode, OracleDbType.NVarchar2, objSolicitudReemision.objCalificadores.strTourCode.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_TOUR_CODE", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If
                If Not String.IsNullOrEmpty(objSolicitudReemision.objCalificadores.strAccountCode) Then
                    ConnNM.AgregarParametro("p_IN_ACCOUNT_CODE", objSolicitudReemision.objCalificadores.strAccountCode, OracleDbType.NVarchar2, objSolicitudReemision.objCalificadores.strAccountCode.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_ACCOUNT_CODE", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objCalificadores.strCorporateId) Then
                    ConnNM.AgregarParametro("p_IN_CORPORATE_ID", objSolicitudReemision.objCalificadores.strCorporateId, OracleDbType.NVarchar2, objSolicitudReemision.objCalificadores.strCorporateId.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_CORPORATE_ID", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                ConnNM._InsertExecuteNonQuery(False, False)

            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spSP_INS_REE_CALIFICADORES & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "InsertaCalificadores" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "InsertaCalificadores", strCodigoSeguimiento)

                Err.Raise(14, "InsertaCalificadores", ex.ToString)
            Finally
                objSolicitudReemision = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try
        End Sub
        Private Sub InsertaTarifa(ByVal objSolicitudReemision As classSolicitudReemision, _
                                  ByVal strCodigoSeguimiento As String, _
                                  ByVal intFirmaDB As Integer, _
                                  ByVal intEsquema As Integer, _
                                  ByVal ConnNM As MyConnectionOracle)
            Try

                ConnNM.SP_Command(Constantes.spSP_INS_REE_TARIFA, Constantes.StoredProcedure, True)

                ConnNM.AgregarParametro("p_IN_REE_CODIGO", objSolicitudReemision.intCodigoReemision, OracleDbType.Int32, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_IN_TIPO_CONSULTA", objSolicitudReemision.objTarifa.intTipoConsulta, OracleDbType.Int32, 0, ParameterDirection.Input)
                If Not String.IsNullOrEmpty(objSolicitudReemision.objTarifa.strTarifaBaseOriginal) Then
                    ConnNM.AgregarParametro("p_IN_TARIFA_BASE_ORIGINAL", objSolicitudReemision.objTarifa.strTarifaBaseOriginal, OracleDbType.NVarchar2, objSolicitudReemision.objTarifa.strTarifaBaseOriginal.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_TARIFA_BASE_ORIGINAL", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objTarifa.strImpuestoOriginal) Then
                    ConnNM.AgregarParametro("p_IN_IMPUESTO_ORIGINAL", objSolicitudReemision.objTarifa.strImpuestoOriginal, OracleDbType.NVarchar2, objSolicitudReemision.objTarifa.strImpuestoOriginal.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_IMPUESTO_ORIGINAL", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objTarifa.strTotalOriginal) Then
                    ConnNM.AgregarParametro("p_IN_TOTAL_ORIGINAL", objSolicitudReemision.objTarifa.strTotalOriginal, OracleDbType.NVarchar2, objSolicitudReemision.objTarifa.strTotalOriginal.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_TOTAL_ORIGINAL", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objTarifa.strTarifaBaseNuevo) Then
                    ConnNM.AgregarParametro("p_IN_TARIFA_BASE_NUEVO", objSolicitudReemision.objTarifa.strTarifaBaseNuevo, OracleDbType.NVarchar2, objSolicitudReemision.objTarifa.strTarifaBaseNuevo.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_TARIFA_BASE_NUEVO", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objTarifa.strImpuestoNuevo) Then
                    ConnNM.AgregarParametro("p_IN_IMPUESTO_NUEVO", objSolicitudReemision.objTarifa.strImpuestoNuevo, OracleDbType.NVarchar2, objSolicitudReemision.objTarifa.strImpuestoNuevo.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_IMPUESTO_NUEVO", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objTarifa.strTotalNuevo) Then
                    ConnNM.AgregarParametro("p_IN_TOTAL_NUEVO", objSolicitudReemision.objTarifa.strTotalNuevo, OracleDbType.NVarchar2, objSolicitudReemision.objTarifa.strTotalNuevo.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_TOTAL_NUEVO", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objTarifa.strTarifaBaseReemision) Then
                    ConnNM.AgregarParametro("p_IN_TARIFA_BASE_REEMISION", objSolicitudReemision.objTarifa.strTarifaBaseReemision, OracleDbType.NVarchar2, objSolicitudReemision.objTarifa.strTarifaBaseReemision.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_TARIFA_BASE_REEMISION", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objTarifa.strImpuestoReemision) Then
                    ConnNM.AgregarParametro("p_IN_IMPUESTO_REEMISION", objSolicitudReemision.objTarifa.strImpuestoReemision, OracleDbType.NVarchar2, objSolicitudReemision.objTarifa.strImpuestoReemision.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_IMPUESTO_REEMISION", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objTarifa.strTotalReemision) Then
                    ConnNM.AgregarParametro("p_IN_TOTAL_REEMISION", objSolicitudReemision.objTarifa.strTotalReemision, OracleDbType.NVarchar2, objSolicitudReemision.objTarifa.strTotalReemision.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_TOTAL_REEMISION", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objTarifa.strTarifaTotal) Then
                    ConnNM.AgregarParametro("p_IN_TARIFA_TOTAL", objSolicitudReemision.objTarifa.strTarifaTotal, OracleDbType.NVarchar2, objSolicitudReemision.objTarifa.strTarifaTotal.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_TARIFA_TOTAL", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objTarifa.strTotalPenalidad) Then
                    ConnNM.AgregarParametro("p_IN_TOTAL_PENALIDAD", objSolicitudReemision.objTarifa.strTotalPenalidad, OracleDbType.NVarchar2, objSolicitudReemision.objTarifa.strTotalPenalidad.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_TOTAL_PENALIDAD", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objTarifa.strTotalFee) Then
                    ConnNM.AgregarParametro("p_IN_TOTAL_FEE", objSolicitudReemision.objTarifa.strTotalFee, OracleDbType.NVarchar2, objSolicitudReemision.objTarifa.strTotalFee.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_TOTAL_FEE", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objTarifa.strTotalAPagar) Then
                    ConnNM.AgregarParametro("p_IN_TOTAL_A_PAGAR", objSolicitudReemision.objTarifa.strTotalAPagar, OracleDbType.NVarchar2, objSolicitudReemision.objTarifa.strTotalAPagar.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_TOTAL_A_PAGAR", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objTarifa.strDiferenciaTarifa) Then
                    ConnNM.AgregarParametro("p_IN_DIFERENCIA_TARIFA", objSolicitudReemision.objTarifa.strDiferenciaTarifa, OracleDbType.NVarchar2, objSolicitudReemision.objTarifa.strDiferenciaTarifa.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_DIFERENCIA_TARIFA", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If



                ConnNM._InsertExecuteNonQuery(False, False)

            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spSP_INS_REE_TARIFA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "InsertaTarifa" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "InsertaTarifa", strCodigoSeguimiento)

                Err.Raise(14, "InsertaTarifa", ex.ToString)
            Finally
                objSolicitudReemision = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try
        End Sub
        Private Function InsertaPago(ByVal objSolicitudReemision As classSolicitudReemision, _
                                     ByVal strCodigoSeguimiento As String, _
                                     ByVal intFirmaDB As Integer, _
                                     ByVal intEsquema As Integer, _
                                     ByVal ConnNM As MyConnectionOracle) As Integer

            Dim intResultado As Integer = 0

            Try

                ConnNM.SP_Command(Constantes.spSP_INS_REE_PAGO, Constantes.StoredProcedure, True)

                ConnNM.AgregarParametro("p_IN_REE_CODIGO", objSolicitudReemision.intCodigoReemision, OracleDbType.Int32, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_IN_PAG_TIPO", objSolicitudReemision.objPago.strTipoPago, OracleDbType.NVarchar2, objSolicitudReemision.objPago.strTipoPago.ToString.Length, ParameterDirection.Input)

                If Not String.IsNullOrEmpty(objSolicitudReemision.objPago.strTipoTarjeta) Then
                    ConnNM.AgregarParametro("p_IN_PAG_TIPO_TARJETA", objSolicitudReemision.objPago.strTipoTarjeta, OracleDbType.NVarchar2, objSolicitudReemision.objPago.strTipoTarjeta.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_PAG_TIPO_TARJETA", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objPago.strNumeroTarjeta) Then
                    ConnNM.AgregarParametro("p_IN_PAG_NRO_TARJETA", objSolicitudReemision.objPago.strNumeroTarjeta, OracleDbType.NVarchar2, objSolicitudReemision.objPago.strNumeroTarjeta.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_PAG_NRO_TARJETA", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objPago.strFechaVencimientoTarjeta) Then
                    Dim Fecha As New Date
                    Fecha = CDate(objSolicitudReemision.objPago.strFechaVencimientoTarjeta).ToString("dd-MM-yyyy")
                    ConnNM.AgregarParametro("p_IN_PAG_FEC_VENC_TARJ", Fecha, OracleDbType.Date, 0, ParameterDirection.Input)
                    Fecha = Nothing
                Else
                    ConnNM.AgregarParametro("p_IN_PAG_FEC_VENC_TARJ", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objPago.strTitularTarjeta) Then
                    ConnNM.AgregarParametro("p_IN_PAG_TITULAR_TARJ", objSolicitudReemision.objPago.strTitularTarjeta, OracleDbType.NVarchar2, objSolicitudReemision.objPago.strTitularTarjeta.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_PAG_TITULAR_TARJ", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objPago.intPaisTarjeta) Then
                    ConnNM.AgregarParametro("p_IN_PAG_PAIS_TARJ_TITU", objSolicitudReemision.objPago.intPaisTarjeta, OracleDbType.Int32, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_PAG_PAIS_TARJ_TITU", Nothing, OracleDbType.Int32, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objPago.strBancoTarjeta) Then
                    ConnNM.AgregarParametro("p_IN_PAG_BANCO_TARJ_TITU", objSolicitudReemision.objPago.strBancoTarjeta, OracleDbType.NVarchar2, objSolicitudReemision.objPago.strBancoTarjeta.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_PAG_BANCO_TARJ_TITU", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objPago.strTipoDocumentoTarjeta) Then
                    ConnNM.AgregarParametro("p_IN_PAG_TIP_DOC_TITU", objSolicitudReemision.objPago.strTipoDocumentoTarjeta, OracleDbType.NVarchar2, objSolicitudReemision.objPago.strTipoDocumentoTarjeta.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_PAG_TIP_DOC_TITU", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objPago.strNumeroDocumentoTarjeta) Then
                    ConnNM.AgregarParametro("p_IN_PAG_NUM_DOC_TITU", objSolicitudReemision.objPago.strNumeroDocumentoTarjeta, OracleDbType.NVarchar2, objSolicitudReemision.objPago.strNumeroDocumentoTarjeta.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_PAG_NUM_DOC_TITU", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objPago.strCodigoSeguridadTarjeta) Then
                    ConnNM.AgregarParametro("p_IN_PAG_COD_SEG_TARJ", objSolicitudReemision.objPago.strCodigoSeguridadTarjeta, OracleDbType.NVarchar2, objSolicitudReemision.objPago.strCodigoSeguridadTarjeta.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_PAG_COD_SEG_TARJ", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objPago.strDepositoHasta) Then
                    ConnNM.AgregarParametro("p_IN_PAG_DEP_HASTA", objSolicitudReemision.objPago.strDepositoHasta, OracleDbType.NVarchar2, objSolicitudReemision.objPago.strDepositoHasta.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_PAG_DEP_HASTA", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(objSolicitudReemision.objPago.strEmailCaja) Then
                    ConnNM.AgregarParametro("p_IN_PAG_EMAIL_CAJA", objSolicitudReemision.objPago.strEmailCaja, OracleDbType.NVarchar2, objSolicitudReemision.objPago.strEmailCaja.ToString.Length, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("p_IN_PAG_EMAIL_CAJA", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If


                ConnNM.AgregarParametro("p_OUT_NumRegistro", Nothing, OracleDbType.Int32, 0, ParameterDirection.Output)


                ConnNM._ExecuteReader()
                intResultado = ConnNM.LeeParametros("p_OUT_NumRegistro", 0)

            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spSP_INS_REE_PAGO & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "InsertaPago" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "InsertaPago", strCodigoSeguimiento)

                Err.Raise(14, "InsertaPago", ex.ToString)
            Finally
                objSolicitudReemision = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return intResultado

        End Function
        Private Sub InsertaDepositos(ByVal objSolicitudReemision As classSolicitudReemision, _
                                     ByVal strCodigoSeguimiento As String, _
                                     ByVal intFirmaDB As Integer, _
                                     ByVal intEsquema As Integer, _
                                     ByVal ConnNM As MyConnectionOracle)
            Try

                For i As Integer = 0 To objSolicitudReemision.objPago.objDeposito.Count - 1
                    If objSolicitudReemision.objPago.objDeposito.Item(i) IsNot Nothing Then
                        ConnNM.SP_Command(Constantes.spSP_INS_REE_DEPOSITOS, Constantes.StoredProcedure, True)

                        ConnNM.AgregarParametro("p_IN_PAGO_CODIGO", objSolicitudReemision.objPago.intCodigoPago, OracleDbType.Int32, 0, ParameterDirection.Input)
                        ConnNM.AgregarParametro("p_IN_REE_CODIGO", objSolicitudReemision.intCodigoReemision, OracleDbType.Int32, 0, ParameterDirection.Input)
                        ConnNM.AgregarParametro("p_IN_TIPO_OPERACION", objSolicitudReemision.objPago.objDeposito.Item(i).strTipoOperacion, OracleDbType.NVarchar2, objSolicitudReemision.objPago.objDeposito.Item(i).strTipoOperacion.ToString.Length, ParameterDirection.Input)
                        ConnNM.AgregarParametro("p_IN_NOMBRE_BANCO", objSolicitudReemision.objPago.objDeposito.Item(i).strNombreBanco, OracleDbType.NVarchar2, objSolicitudReemision.objPago.objDeposito.Item(i).strNombreBanco.ToString.Length, ParameterDirection.Input)
                        ConnNM.AgregarParametro("p_IN_SUCURSAL_BANCO", objSolicitudReemision.objPago.objDeposito.Item(i).strSucursalBanco, OracleDbType.NVarchar2, objSolicitudReemision.objPago.objDeposito.Item(i).strSucursalBanco.ToString.Length, ParameterDirection.Input)
                        ConnNM.AgregarParametro("p_IN_NUMERO_OPERACION", objSolicitudReemision.objPago.objDeposito.Item(i).strNumeroOperacion, OracleDbType.NVarchar2, objSolicitudReemision.objPago.objDeposito.Item(i).strNumeroOperacion.ToString.Length, ParameterDirection.Input)
                        ConnNM.AgregarParametro("p_IN_REFERENCIA_OPERACION", objSolicitudReemision.objPago.objDeposito.Item(i).strReferenciaOperacion, OracleDbType.NVarchar2, objSolicitudReemision.objPago.objDeposito.Item(i).strReferenciaOperacion.ToString.Length, ParameterDirection.Input)
                        ConnNM.AgregarParametro("p_IN_MONTO_OPERACION", objSolicitudReemision.objPago.objDeposito.Item(i).strMontoOperacion, OracleDbType.NVarchar2, objSolicitudReemision.objPago.objDeposito.Item(i).strMontoOperacion.ToString.Length, ParameterDirection.Input)
                        ConnNM.AgregarParametro("p_IN_FECHA_OPERACION", objSolicitudReemision.objPago.objDeposito.Item(i).strFechaOperacion, OracleDbType.NVarchar2, objSolicitudReemision.objPago.objDeposito.Item(i).strFechaOperacion.ToString.Length, ParameterDirection.Input)
                        ConnNM.AgregarParametro("p_IN_HORA_OPERACION", objSolicitudReemision.objPago.objDeposito.Item(i).strHoraOperacion, OracleDbType.NVarchar2, objSolicitudReemision.objPago.objDeposito.Item(i).strHoraOperacion.ToString.Length, ParameterDirection.Input)

                        If Not String.IsNullOrEmpty(objSolicitudReemision.objPago.objDeposito.Item(i).strRutaImagen) Then
                            ConnNM.AgregarParametro("p_IN_RUTA_IMAGEN", objSolicitudReemision.objPago.objDeposito.Item(i).strRutaImagen, OracleDbType.NVarchar2, objSolicitudReemision.objPago.objDeposito.Item(i).strRutaImagen.ToString.Length, ParameterDirection.Input)
                        Else
                            ConnNM.AgregarParametro("p_IN_RUTA_IMAGEN", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                        End If

                        ConnNM.AgregarParametro("p_IN_CUENTA", objSolicitudReemision.objPago.objDeposito.Item(i).strCuenta, OracleDbType.NVarchar2, objSolicitudReemision.objPago.objDeposito.Item(i).strCuenta.ToString.Length, ParameterDirection.Input)

                        ConnNM._InsertExecuteNonQuery(False, False)

                    End If

                Next

            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spSP_INS_REE_DEPOSITOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "InsertaDepositos" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "InsertaDepositos", strCodigoSeguimiento)

                Err.Raise(14, "InsertaDepositos", ex.ToString)
            Finally
                objSolicitudReemision = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try
        End Sub
        Public Function ObtieneSolicitudReemision(ByVal intCodigoReemision As Integer, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As classSolicitudReemision

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim intPago As Integer = 0

            Dim auxSolicitudReemision As classSolicitudReemision = Nothing
            Dim auxGenerales As classSolicitudReemision.classGenerales = Nothing
            Dim auxCalificadores As classSolicitudReemision.classCalificadores = Nothing
            Dim auxTarifa As classSolicitudReemision.classReeTarifa = Nothing
            Dim auxPago As classSolicitudReemision.classPago = Nothing
            Dim auxDeposito As List(Of classSolicitudReemision.classDeposito) = Nothing

            Try

                ConnNM.Connect(intFirmaDB)

                ConnNM.SP_Command(Constantes.spSP_SEL_REEMISION, Constantes.StoredProcedure, True)

                ConnNM.AgregarParametro("p_IN_REE_CODIGO", intCodigoReemision, OracleDbType.Int32, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_OUT_RESULTADO", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxSolicitudReemision = New classSolicitudReemision

                    auxSolicitudReemision.intCodigoReemision = intCodigoReemision
                    auxSolicitudReemision.strCodigoPNR = ConnNM.LeeColumnasDataReader(objOracleDataReader, "REE_PNR_COD", Nothing)
                    auxSolicitudReemision.strTipoReserva = ConnNM.LeeColumnasDataReader(objOracleDataReader, "REE_TIPO_RESV", Nothing)
                    auxSolicitudReemision.strNombrePromotor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "REE_PROMOTOR", Nothing)
                    auxSolicitudReemision.strFechaReemision = ConnNM.LeeColumnasDataReader(objOracleDataReader, "REE_FECHA", Nothing)
                    auxSolicitudReemision.strTipoTarifa = ConnNM.LeeColumnasDataReader(objOracleDataReader, "REE_TIPO_TARIFA", Nothing)
                    auxSolicitudReemision.intSubCodigo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "REE_ID_SUBCODIGO", 0)
                    auxSolicitudReemision.strObservacion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "REE_OBSERVACION", Nothing)

                    auxGenerales = ObtieneSolicitudGenerales(intCodigoReemision, strCodigoSeguimiento, intFirmaDB, intEsquema, ConnNM)
                    If auxGenerales IsNot Nothing Then
                        auxSolicitudReemision.objGenerales = New classSolicitudReemision.classGenerales
                        auxSolicitudReemision.objGenerales = auxGenerales
                    End If

                    auxCalificadores = ObtieneSolicitudCalificadores(intCodigoReemision, strCodigoSeguimiento, intFirmaDB, intEsquema, ConnNM)
                    If auxCalificadores IsNot Nothing Then
                        auxSolicitudReemision.objCalificadores = New classSolicitudReemision.classCalificadores
                        auxSolicitudReemision.objCalificadores = auxCalificadores
                    End If

                    auxTarifa = ObtieneSolicitudTarifa(intCodigoReemision, strCodigoSeguimiento, intFirmaDB, intEsquema, ConnNM)
                    If auxTarifa IsNot Nothing Then
                        auxSolicitudReemision.objTarifa = New classSolicitudReemision.classReeTarifa
                        auxSolicitudReemision.objTarifa = auxTarifa
                    End If

                    auxPago = ObtieneSolicitudPago(intCodigoReemision, strCodigoSeguimiento, intFirmaDB, intEsquema, ConnNM)
                    If auxPago IsNot Nothing Then
                        auxSolicitudReemision.objPago = New classSolicitudReemision.classPago
                        auxSolicitudReemision.objPago = auxPago
                    End If

                    If auxPago IsNot Nothing Then
                        If (Not auxPago.strTipoPago.ToUpper.Equals("CASH")) Or (Not auxPago.strTipoPago.ToUpper.Equals("TARJETA")) Then
                            If String.IsNullOrEmpty(auxPago.strDepositoHasta) Then
                                auxDeposito = ObtieneSolicitudDeposito(auxPago.intCodigoPago, auxSolicitudReemision.intCodigoReemision, strCodigoSeguimiento, intFirmaDB, intEsquema, ConnNM)
                                If auxDeposito IsNot Nothing Then
                                    auxSolicitudReemision.objPago.objDeposito = New List(Of classSolicitudReemision.classDeposito)
                                    auxSolicitudReemision.objPago.objDeposito = auxDeposito
                                End If

                            End If
                        End If
                    End If



                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spSP_SEL_REEMISION & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtieneSolicitudReemision" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtieneSolicitudReemision", strCodigoSeguimiento)

                Err.Raise(14, "ObtieneSolicitudReemision", ex.ToString)
            Finally
                ConnNM.Disconnect()
                ConnNM = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return auxSolicitudReemision
        End Function
        Private Function ObtieneSolicitudGenerales(ByVal intCodigoReemision As Integer, _
                                                   ByVal strCodigoSeguimiento As String, _
                                                   ByVal intFirmaDB As Integer, _
                                                   ByVal intEsquema As Integer, _
                                                   ByVal ConnNM As MyConnectionOracle) As classSolicitudReemision.classGenerales

            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim intPago As Integer = 0

            Dim auxGenerales As classSolicitudReemision.classGenerales = Nothing

            Try


                ConnNM.SP_Command(Constantes.spSP_SEL_REE_GENERALES, Constantes.StoredProcedure, True)

                ConnNM.AgregarParametro("p_IN_REE_CODIGO", intCodigoReemision, OracleDbType.Int32, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_OUT_RESULTADO", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxGenerales = New classSolicitudReemision.classGenerales

                    auxGenerales.strItinerarioOriginal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ITINERARIO_ORIGINAL", Nothing)
                    auxGenerales.strItinerarioReemision = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ITINERARIO_REEMITIR", Nothing)
                    auxGenerales.strBoletoOriginal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "BOLETO_ORIGINAL", Nothing)
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spSP_SEL_REE_GENERALES & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtieneSolicitudGenerales" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtieneSolicitudGenerales", strCodigoSeguimiento)

                Err.Raise(14, "ObtieneSolicitudGenerales", ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return auxGenerales
        End Function
        Private Function ObtieneSolicitudCalificadores(ByVal intCodigoReemision As Integer, _
                                                       ByVal strCodigoSeguimiento As String, _
                                                       ByVal intFirmaDB As Integer, _
                                                       ByVal intEsquema As Integer, _
                                                       ByVal ConnNM As MyConnectionOracle) As classSolicitudReemision.classCalificadores

            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim intPago As Integer = 0

            Dim auxCalificadores As classSolicitudReemision.classCalificadores = Nothing

            Try


                ConnNM.SP_Command(Constantes.spSP_SEL_REE_CALIFICADORES, Constantes.StoredProcedure, True)

                ConnNM.AgregarParametro("p_IN_REE_CODIGO", intCodigoReemision, OracleDbType.Int32, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_OUT_RESULTADO", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxCalificadores = New classSolicitudReemision.classCalificadores

                    auxCalificadores.strTipoTarifa = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TIPO_TARIFA", Nothing)
                    auxCalificadores.strTipoPasajero = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TIPO_PASAJERO", Nothing)
                    auxCalificadores.strNumeroPasajero = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_PASAJERO", Nothing)
                    auxCalificadores.strAerolinea = ConnNM.LeeColumnasDataReader(objOracleDataReader, "AEROLINEA", Nothing)
                    auxCalificadores.strNumeroBoleto = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_TICKET", Nothing)
                    auxCalificadores.strSegmentos = ConnNM.LeeColumnasDataReader(objOracleDataReader, "SEGMENTOS", Nothing)
                    auxCalificadores.strTourCode = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TOUR_CODE", Nothing)
                    auxCalificadores.strAccountCode = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ACCOUNT_CODE", Nothing)
                    auxCalificadores.strCorporateId = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CORPORATE_ID", Nothing)
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spSP_SEL_REE_CALIFICADORES & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtieneSolicitudCalificadores" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtieneSolicitudCalificadores", strCodigoSeguimiento)

                Err.Raise(14, "ObtieneSolicitudCalificadores", ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return auxCalificadores
        End Function
        Private Function ObtieneSolicitudTarifa(ByVal intCodigoReemision As Integer, _
                                                ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer, _
                                                ByVal ConnNM As MyConnectionOracle) As classSolicitudReemision.classReeTarifa


            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim intPago As Integer = 0

            Dim auxTarifa As classSolicitudReemision.classReeTarifa = Nothing

            Try


                ConnNM.SP_Command(Constantes.spSP_SEL_REE_TARIFA, Constantes.StoredProcedure, True)

                ConnNM.AgregarParametro("p_IN_REE_CODIGO", intCodigoReemision, OracleDbType.Int32, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_OUT_RESULTADO", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxTarifa = New classSolicitudReemision.classReeTarifa

                    auxTarifa.intTipoConsulta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TIPO_CONSULTA", 0)
                    auxTarifa.strTarifaBaseOriginal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TARIFA_BASE_ORIGINAL", Nothing)
                    auxTarifa.strImpuestoOriginal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "IMPUESTO_ORIGINAL", Nothing)
                    auxTarifa.strTotalOriginal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TOTAL_ORIGINAL", Nothing)
                    auxTarifa.strTarifaBaseNuevo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TARIFA_BASE_NUEVO", Nothing)
                    auxTarifa.strImpuestoNuevo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "IMPUESTO_NUEVO", Nothing)
                    auxTarifa.strTotalNuevo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TOTAL_NUEVO", Nothing)
                    auxTarifa.strTarifaBaseReemision = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TARIFA_BASE_REEMISION", Nothing)
                    auxTarifa.strImpuestoReemision = ConnNM.LeeColumnasDataReader(objOracleDataReader, "IMPUESTO_REEMISION", Nothing)
                    auxTarifa.strTotalReemision = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TOTAL_REEMISION", Nothing)
                    auxTarifa.strTarifaTotal = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TARIFA_TOTAL", Nothing)
                    auxTarifa.strTotalPenalidad = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TOTAL_PENALIDAD", Nothing)
                    auxTarifa.strTotalFee = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TOTAL_FEE", Nothing)
                    auxTarifa.strTotalAPagar = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TOTAL_A_PAGAR", Nothing)
                    auxTarifa.strDiferenciaTarifa = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DIFERENCIA_TARIFA", Nothing)
                    auxTarifa.strMensajeMostrado = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MENSAJE_MOSTRADO", Nothing)
                    auxTarifa.strTipoPasajero = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TIPO_PASAJERO", Nothing)
                    auxTarifa.strUltimaFechaEmision = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ULTIMA_FECHA_EMISION", Nothing)
                    auxTarifa.strLineaValidadora = ConnNM.LeeColumnasDataReader(objOracleDataReader, "LINEA_VALIDADORA", Nothing)
                    auxTarifa.strInformacionTarifa = ConnNM.LeeColumnasDataReader(objOracleDataReader, "INFORMACION_TARIFA", Nothing)
                    auxTarifa.strItinerarioTarifa = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ITINERARIO_TARIFA", Nothing)
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spSP_SEL_REE_TARIFA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtieneSolicitudTarifa" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtieneSolicitudTarifa", strCodigoSeguimiento)

                Err.Raise(14, "ObtieneSolicitudTarifa", ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return auxTarifa
        End Function
        Private Function ObtieneSolicitudPago(ByVal intCodigoReemision As Integer, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer, _
                                              ByVal ConnNM As MyConnectionOracle) As classSolicitudReemision.classPago


            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim intPago As Integer = 0

            Dim auxPago As classSolicitudReemision.classPago = Nothing

            Try


                ConnNM.SP_Command(Constantes.spSP_SEL_REE_PAGO, Constantes.StoredProcedure, True)

                ConnNM.AgregarParametro("p_IN_REE_CODIGO", intCodigoReemision, OracleDbType.Int32, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_OUT_RESULTADO", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxPago = New classSolicitudReemision.classPago

                    auxPago.intCodigoPago = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAG_CODIGO", 0)
                    auxPago.strTipoPago = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAG_TIPO", Nothing)
                    auxPago.strTipoTarjeta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAG_TIPO_TARJETA", Nothing)
                    auxPago.strNumeroTarjeta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAG_NRO_TARJETA", Nothing)
                    auxPago.strFechaVencimientoTarjeta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAG_FEC_VENC_TARJ", Nothing)
                    auxPago.strTitularTarjeta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAG_TITULAR_TARJ", Nothing)
                    auxPago.intPaisTarjeta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAG_PAIS_TARJ_TITU", 0)

                    auxPago.strNombrePaisTarjeta = ObtienePais(auxPago.intPaisTarjeta, strCodigoSeguimiento, intFirmaDB, intEsquema, ConnNM)

                    auxPago.strBancoTarjeta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAG_BANCO_TARJ_TITU", Nothing)
                    auxPago.strTipoDocumentoTarjeta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAG_TIP_DOC_TITU", Nothing)
                    auxPago.strNumeroDocumentoTarjeta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAG_NUM_DOC_TITU", Nothing)
                    auxPago.strCodigoSeguridadTarjeta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAG_COD_SEG_TARJ", Nothing)
                    auxPago.strDepositoHasta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAG_DEP_HASTA", Nothing)
                    auxPago.strEmailCaja = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAG_EMAIL_CAJA", Nothing)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spSP_SEL_REE_PAGO & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtieneSolicitudPago" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtieneSolicitudPago", strCodigoSeguimiento)

                Err.Raise(14, "ObtieneSolicitudPago", ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return auxPago
        End Function
        Private Function ObtienePais(ByVal intCodigoPais As Integer, _
                                     ByVal strCodigoSeguimiento As String, _
                                     ByVal intFirmaDB As Integer, _
                                     ByVal intEsquema As Integer, _
                                     ByVal ConnNM As MyConnectionOracle) As String

            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim intPago As Integer = 0

            Dim strPais As String = Nothing

            Try


                ConnNM.SP_Command(Constantes.spSP_PAIS_OBTIENE_X_ID, Constantes.StoredProcedure, True)

                ConnNM.AgregarParametro("pNumIdPais_in", intCodigoPais, OracleDbType.Int32, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("pCurResult_out", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    strPais = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAIS_NOM", Nothing)
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spSP_PAIS_OBTIENE_X_ID & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtienePais" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtienePais", strCodigoSeguimiento)

                Err.Raise(14, "ObtienePais", ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return strPais
        End Function
        Private Function ObtieneSolicitudDeposito(ByVal intCodigoPago As Integer, _
                                                  ByVal intCodigoReemision As Integer, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer, _
                                                  ByVal ConnNM As MyConnectionOracle) As List(Of classSolicitudReemision.classDeposito)

            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim intPago As Integer = 0
            Dim lstDeposito As List(Of classSolicitudReemision.classDeposito) = Nothing
            Dim auxDeposito As classSolicitudReemision.classDeposito = Nothing

            Try


                ConnNM.SP_Command(Constantes.spSP_SEL_REE_DEPOSITOS, Constantes.StoredProcedure, True)

                ConnNM.AgregarParametro("p_IN_PAG_CODIGO", intCodigoPago, OracleDbType.Int32, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_IN_REE_CODIGO", intCodigoReemision, OracleDbType.Int32, 0, ParameterDirection.Input)
                ConnNM.AgregarParametro("p_OUT_RESULTADO", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxDeposito = New classSolicitudReemision.classDeposito

                    auxDeposito.strTipoOperacion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TIPO_OPERACION", Nothing)
                    auxDeposito.strNombreBanco = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE_BANCO", Nothing)
                    auxDeposito.strSucursalBanco = ConnNM.LeeColumnasDataReader(objOracleDataReader, "SUCURSAL_BANCO", Nothing)
                    auxDeposito.strNumeroOperacion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO_OPERACION", Nothing)
                    auxDeposito.strReferenciaOperacion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "REFERENCIA_OPERACION", Nothing)
                    auxDeposito.strMontoOperacion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MONTO_OPERACION", 0)
                    auxDeposito.strFechaOperacion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA_OPERACION", Nothing)
                    auxDeposito.strHoraOperacion = ConnNM.LeeColumnasDataReader(objOracleDataReader, "HORA_OPERACION", Nothing)
                    auxDeposito.strRutaImagen = ConnNM.LeeColumnasDataReader(objOracleDataReader, "RUTA_IMAGEN", Nothing)
                    If lstDeposito Is Nothing Then lstDeposito = New List(Of classSolicitudReemision.classDeposito)
                    lstDeposito.Add(auxDeposito)
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spSP_SEL_REE_DEPOSITOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtieneSolicitudDeposito" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtieneSolicitudDeposito", strCodigoSeguimiento)

                Err.Raise(14, "ObtieneSolicitudDeposito", ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return lstDeposito
        End Function
        Public Function ObtieneMensajeIGVAutomatedExchange(ByVal srtAerolinea As String, _
                                                          ByVal strCodigoSeguimiento As String, _
                                                          ByVal intFirmaDB As Integer, _
                                                          ByVal intEsquema As Integer) As Integer

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim intRespuesta As Integer = 0
            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spAEROLINEA_ASOCIADA, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Aerolinea", srtAerolinea.ToString, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    intRespuesta = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MENSAJE_IGV_EXCHANGES", 0)
                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spAEROLINEA_ASOCIADA & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Código Aerolinea : " & srtAerolinea.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "classAD_SolicitudesReemision.ObtieneMensajeIGVAutomatedExchange" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtieneMensajeIGVAutomatedExchange", strCodigoSeguimiento)

                Err.Raise(3, "classAD_SolicitudesReemision.ObtieneMensajeIGVAutomatedExchange", ex.Message)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                srtAerolinea = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return intRespuesta

        End Function
    End Class
End Namespace