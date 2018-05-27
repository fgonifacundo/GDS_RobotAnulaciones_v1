Imports GDS_NuevoMundoPersistencia
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports System.Text

Namespace ObjetoAccesoDatos
    Partial Public Class classDAO
        'Public Function InsertaTarifasFQ(ByVal objFQ As List(Of classFQ.classFareRS), _
        '                                 ByVal strCodigoSeguimiento As String, _
        '                                 ByVal intFirmaDB As Integer, _
        '                                 ByVal intEsquema As Integer) As Boolean

        '    Dim ConnNM As New MyConnectionOracle
        '    Dim bolResultado As Boolean = False
        '    Dim bolFlag As Boolean = True
        '    Dim bolCommit As Boolean = False
        '    Try

        '        ConnNM.Connect(intFirmaDB)
        '        For x As Integer = 0 To objFQ.Count - 1
        '            ConnNM.SP_Command(Constantes.spINS_TARIFAS_FQ, Constantes.StoredProcedure)
        '            ConnNM.AgregarParametro("p_DK", objFQ.Item(x).DK, OracleDbType.Double, 0, ParameterDirection.Input)
        '            ConnNM.AgregarParametro("p_IDS", CStr(x), OracleDbType.Varchar2, CStr(x).Length, ParameterDirection.Input)
        '            ConnNM.AgregarParametro("p_NUMERO", CStr(objFQ.Item(x).ID), OracleDbType.Varchar2, CStr(objFQ.Item(x).ID).Length, ParameterDirection.Input)
        '            ConnNM.AgregarParametro("p_PNR", objFQ.Item(x).PNR, OracleDbType.Varchar2, objFQ.Item(x).PNR.Length, ParameterDirection.Input)

        '           If String.IsNullOrEmpty(objFQ.Item(x).AirLines) Then
        '                ConnNM.AgregarParametro("p_AIRLINES", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_AIRLINES", objFQ.Item(x).AirLines, OracleDbType.Varchar2, objFQ.Item(x).AirLines.Length, ParameterDirection.Input)
        '           End If

        '            If objFQ.Item(x).PassengerType Is Nothing Then
        '                ConnNM.AgregarParametro("p_PASSENGERTYPE", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_PASSENGERTYPE", objFQ.Item(x).PassengerType.Item(0).ID, OracleDbType.Varchar2, objFQ.Item(x).PassengerType.Item(0).ID.Length, ParameterDirection.Input)
        '            End If

        '            If String.IsNullOrEmpty(objFQ.Item(x).FareBasis) Then
        '                ConnNM.AgregarParametro("p_FAREBASIS", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_FAREBASIS", objFQ.Item(x).FareBasis, OracleDbType.Varchar2, objFQ.Item(x).FareBasis.Length, ParameterDirection.Input)
        '            End If

        '            If String.IsNullOrEmpty(objFQ.Item(x).BookingClass) Then
        '                ConnNM.AgregarParametro("p_BOOKINGCLASS", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_BOOKINGCLASS", objFQ.Item(x).BookingClass, OracleDbType.Varchar2, objFQ.Item(x).BookingClass.Length, ParameterDirection.Input)
        '            End If

        '            If String.IsNullOrEmpty(objFQ.Item(x).Cabin) Then
        '                ConnNM.AgregarParametro("p_CABINA", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_CABINA", objFQ.Item(x).Cabin, OracleDbType.Varchar2, objFQ.Item(x).Cabin.Length, ParameterDirection.Input)
        '            End If

        '            If String.IsNullOrEmpty(objFQ.Item(x).Currency) Then
        '                ConnNM.AgregarParametro("p_CURRENCY", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_CURRENCY", objFQ.Item(x).Currency, OracleDbType.Varchar2, objFQ.Item(x).Currency.Length, ParameterDirection.Input)
        '            End If

        '            If String.IsNullOrEmpty(objFQ.Item(x).BaseFare) Then
        '                ConnNM.AgregarParametro("p_BASEFARE", "", OracleDbType.Decimal, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_BASEFARE", objFQ.Item(x).BaseFare, OracleDbType.Decimal, objFQ.Item(x).BaseFare.Length, ParameterDirection.Input)
        '            End If

        '            If String.IsNullOrEmpty(objFQ.Item(x).FeeMinino) Then
        '                ConnNM.AgregarParametro("p_FEEMINIMO", "0.00", OracleDbType.Decimal, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_FEEMINIMO", objFQ.Item(x).FeeMinino, OracleDbType.Decimal, 0, ParameterDirection.Input)
        '            End If

        '            If String.IsNullOrEmpty(objFQ.Item(x).FeeMaximo) Then
        '                ConnNM.AgregarParametro("p_FEEMAXIMO", "0.00", OracleDbType.Decimal, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_FEEMAXIMO", objFQ.Item(x).FeeMaximo, OracleDbType.Decimal, 0, ParameterDirection.Input)
        '            End If

        '            If String.IsNullOrEmpty(objFQ.Item(x).ExpirationDate) Then
        '                ConnNM.AgregarParametro("p_EXPIRATIONDATE", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_EXPIRATIONDATE", objFQ.Item(x).ExpirationDate, OracleDbType.Varchar2, objFQ.Item(x).ExpirationDate.Length, ParameterDirection.Input)
        '            End If

        '            If String.IsNullOrEmpty(objFQ.Item(x).EffectiveDate) Then
        '                ConnNM.AgregarParametro("p_EFFECTIVEDATE", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_EFFECTIVEDATE", objFQ.Item(x).EffectiveDate, OracleDbType.Varchar2, objFQ.Item(x).EffectiveDate.Length, ParameterDirection.Input)
        '            End If

        '            If String.IsNullOrEmpty(objFQ.Item(x).TicketDate) Then
        '                ConnNM.AgregarParametro("p_TICKETDATE", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_TICKETDATE", objFQ.Item(x).TicketDate, OracleDbType.Varchar2, objFQ.Item(x).TicketDate.Length, ParameterDirection.Input)
        '            End If

        '            If String.IsNullOrEmpty(objFQ.Item(x).AdvancePurchase) Then
        '                ConnNM.AgregarParametro("p_ADVANCEPURCHASE", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_ADVANCEPURCHASE", objFQ.Item(x).AdvancePurchase, OracleDbType.Varchar2, objFQ.Item(x).AdvancePurchase.Length, ParameterDirection.Input)
        '            End If

        '            If String.IsNullOrEmpty(objFQ.Item(x).SeasonalApplication) Then
        '                ConnNM.AgregarParametro("p_SEASONALAPPLIC", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_SEASONALAPPLIC", objFQ.Item(x).SeasonalApplication, OracleDbType.Varchar2, objFQ.Item(x).SeasonalApplication.Length, ParameterDirection.Input)
        '            End If

        '            If String.IsNullOrEmpty(objFQ.Item(x).MinStay) Then
        '                ConnNM.AgregarParametro("p_MINSTAY", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_MINSTAY", objFQ.Item(x).MinStay, OracleDbType.Varchar2, objFQ.Item(x).MinStay.Length, ParameterDirection.Input)
        '            End If

        '            If String.IsNullOrEmpty(objFQ.Item(x).MaxStay) Then
        '                ConnNM.AgregarParametro("p_MAXSTAY", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_MAXSTAY", objFQ.Item(x).MaxStay, OracleDbType.Varchar2, objFQ.Item(x).MaxStay.Length, ParameterDirection.Input)
        '            End If

        '            If objFQ.Item(x).Pseudos Is Nothing Then
        '                ConnNM.AgregarParametro("p_PSEUDO", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
        '                ConnNM.AgregarParametro("p_IDPSEUDO", "", OracleDbType.Double, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_PSEUDO", objFQ.Item(x).Pseudos.Item(0).Pseudo, OracleDbType.Varchar2, objFQ.Item(x).Pseudos.Item(0).Pseudo.Length, ParameterDirection.Input)
        '                ConnNM.AgregarParametro("p_IDPSEUDO", objFQ.Item(x).Pseudos.Item(0).IDs, OracleDbType.Double, 0, ParameterDirection.Input)
        '            End If

        '            If objFQ.Item(x).RuleCategory Is Nothing Then
        '                ConnNM.AgregarParametro("p_REGLAS", "", OracleDbType.Varchar2, 0, ParameterDirection.Input)
        '            Else
        '                ConnNM.AgregarParametro("p_REGLAS", objFQ.Item(x).RuleCategory.Item(0), OracleDbType.Varchar2, objFQ.Item(x).RuleCategory.Item(0).Length, ParameterDirection.Input)
        '            End If

        '            bolResultado = ConnNM._InsertExecuteNonQuery(bolCommit, bolFlag)
        '            bolFlag = False
        '        Next

        '        ConnNM._Commit()

        '        '----------


        '    Catch ex As Exception

        '        bolResultado = Nothing
        '        strLog = "Stored Procedure : " & Constantes.spINS_TARIFAS_FQ & vbCrLf
        '        strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
        '        strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
        '        strLog &= Constantes.TabEspacios & "Source : " & "InsertaTarifasFQ" & vbCrLf
        '        strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
        '        objEscribeLog.WriteLog(strLog, "InsertaTarifasFQ", strCodigoSeguimiento)

        '        Err.Raise(13, "InsertaTarifasFQ", ex.ToString)


        '    Finally
        '        ConnNM.Disconnect()
        '        strCodigoSeguimiento = Nothing
        '        intFirmaDB = Nothing
        '        intEsquema = Nothing
        '        ConnNM = Nothing
        '    End Try

        '    Return bolResultado
        'End Function
        Public Function InsertaTarifasFQ(ByVal objFQ As List(Of classFQ.classFareRS), _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As Boolean
            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean
            Dim strtabla As String = "TARIFABULK_FQ"
            Dim strXML As StringBuilder = New StringBuilder
            Try
                elaboraXML_TarifasFQ(strXML, objFQ)
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_MC_INS_CONCEPTOS, Constantes.StoredProcedure) 'CARGA LA TABLA MEDIANTE XML
                ConnNM.AgregarParametro("@p_Tabla", strtabla, OracleDbType.Varchar2, strtabla.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Xml", strXML.ToString(), OracleDbType.Clob, 0, ParameterDirection.Input)
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
                strtabla = Nothing
                strXML = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return bolResultado
        End Function
        Public Sub elaboraXML_TarifasFQ(ByRef strXML As StringBuilder, ByVal objFQ As List(Of classFQ.classFareRS))
            strXML.Append("<?xml version='1.0'?>")
            strXML.Append("<ROWSET>")

            For x As Integer = 0 To objFQ.Count - 1
                strXML.Append("<ROW num='" & x + 1 & "'>")


                strXML.Append("<DK>" & objFQ.Item(x).DK & "</DK>")
                If Not String.IsNullOrEmpty(objFQ.Item(x).BaseFare) Then
                    objFQ.Item(x).BaseFare = objFQ.Item(x).BaseFare.Replace(".", ",")
                    strXML.Append("<BASEFARE>" & objFQ.Item(x).BaseFare & "</BASEFARE>")
                End If
                If Not objFQ.Item(x).Pseudos Is Nothing Then
                    strXML.Append("<IDPSEUDO>" & objFQ.Item(x).Pseudos.Item(0).IDs & "</IDPSEUDO>")
                    strXML.Append("<PSEUDO>" & objFQ.Item(x).Pseudos.Item(0).Pseudo & "</PSEUDO>")
                End If

                If Not String.IsNullOrEmpty(objFQ.Item(x).FeeMinino) Then
                    objFQ.Item(x).FeeMinino = objFQ.Item(x).FeeMinino.Replace(".", ",")
                    strXML.Append("<FEEMINIMO>" & objFQ.Item(x).FeeMinino & "</FEEMINIMO>")
                Else
                    strXML.Append("<FEEMINIMO>0</FEEMINIMO>")
                End If

                If Not String.IsNullOrEmpty(objFQ.Item(x).FeeMaximo) Then
                    objFQ.Item(x).FeeMaximo = objFQ.Item(x).FeeMaximo.Replace(".", ",")
                    strXML.Append("<FEEMAXIMO>" & objFQ.Item(x).FeeMaximo & "</FEEMAXIMO>")
                Else
                    strXML.Append("<FEEMAXIMO>0</FEEMAXIMO>")
                End If
                strXML.Append("<IDS>" & CStr(x) & "</IDS>")
                strXML.Append("<NUMERO>" & CStr(objFQ.Item(x).ID) & "</NUMERO>")
                strXML.Append("<PNR>" & objFQ.Item(x).PNR & "</PNR>")

                If Not String.IsNullOrEmpty(objFQ.Item(x).AirLines) Then
                    strXML.Append("<AIRLINES>" & objFQ.Item(x).AirLines & "</AIRLINES>")
                End If
                If Not objFQ.Item(x).PassengerType Is Nothing Then
                    strXML.Append("<PASSENGERTYPE>" & objFQ.Item(x).PassengerType.Item(0).ID & "</PASSENGERTYPE>")
                End If
                If Not String.IsNullOrEmpty(objFQ.Item(x).FareBasis) Then
                    strXML.Append("<FAREBASIS>" & objFQ.Item(x).FareBasis & "</FAREBASIS>")
                End If
                If Not String.IsNullOrEmpty(objFQ.Item(x).BookingClass) Then
                    strXML.Append("<BOOKINGCLASS>" & objFQ.Item(x).BookingClass & "</BOOKINGCLASS>")
                End If
                If Not String.IsNullOrEmpty(objFQ.Item(x).Cabin) Then
                    strXML.Append("<CABINA>" & objFQ.Item(x).Cabin & "</CABINA>")
                End If
                If Not String.IsNullOrEmpty(objFQ.Item(x).Currency) Then
                    strXML.Append("<CURRENCY>" & objFQ.Item(x).Currency & "</CURRENCY>")
                End If
                If Not String.IsNullOrEmpty(objFQ.Item(x).ExpirationDate) Then
                    strXML.Append("<EXPIRATIONDATE>" & objFQ.Item(x).ExpirationDate & "</EXPIRATIONDATE>")
                End If
                If Not String.IsNullOrEmpty(objFQ.Item(x).EffectiveDate) Then
                    strXML.Append("<EFFECTIVEDATE>" & objFQ.Item(x).EffectiveDate & "</EFFECTIVEDATE>")
                End If
                If Not String.IsNullOrEmpty(objFQ.Item(x).TicketDate) Then
                    strXML.Append("<TICKETDATE>" & objFQ.Item(x).TicketDate & "</TICKETDATE>")
                End If
                If Not String.IsNullOrEmpty(objFQ.Item(x).AdvancePurchase) Then
                    strXML.Append("<ADVANCEPURCHASE>" & objFQ.Item(x).AdvancePurchase & "</ADVANCEPURCHASE>")
                End If
                If Not String.IsNullOrEmpty(objFQ.Item(x).SeasonalApplication) Then
                    strXML.Append("<SEASONALAPPLIC>" & objFQ.Item(x).SeasonalApplication & "</SEASONALAPPLIC>")
                End If
                If Not String.IsNullOrEmpty(objFQ.Item(x).MinStay) Then
                    strXML.Append("<MINSTAY>" & objFQ.Item(x).MinStay & "</MINSTAY>")
                End If
                If Not String.IsNullOrEmpty(objFQ.Item(x).MaxStay) Then
                    strXML.Append("<MAXSTAY>" & objFQ.Item(x).MaxStay & "</MAXSTAY>")
                End If
                If Not objFQ.Item(x).RuleCategory Is Nothing Then
                    strXML.Append("<REGLAS>" & objFQ.Item(x).RuleCategory.Item(0) & "</REGLAS>")
                End If
                If Not objFQ.Item(x).tipoTarifa Is Nothing Then
                    strXML.Append("<TIPO_TARIFA>" & objFQ.Item(x).tipoTarifa & "</TIPO_TARIFA>")
                End If
                strXML.Append("</ROW>")
            Next

            strXML.Append("</ROWSET>")
        End Sub
        Public Function ObtenerTarifasFQ(ByVal strPNR As String, _
                                         ByVal strDK As String, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As List(Of classFQ.classFareRS)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing

            Dim objAuxFare As classFQ.classFareRS = Nothing
            Dim objAuxTipoPasajero As classDatosTipoPasajero = Nothing

            Dim objFareRS As List(Of classFQ.classFareRS) = Nothing

            Dim objPseudos As classFQ.classPseudoFare = Nothing
            Dim intContador As Integer = 0

            Dim strCadenaReglas As String = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spTARIFAS_FQ, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_CodigoPNR", strPNR, OracleDbType.Varchar2, strPNR.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cliente", strDK, OracleDbType.Varchar2, strDK.Length, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objAuxFare = New classFQ.classFareRS
                    objAuxFare.ID = intContador
                    'objAuxFare.Numero = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NUMERO", -1)

                    'DK
                    objAuxFare.DK = ConnNM.LeeColumnasDataReader(objOracleDataReader, "DK", -1)

                    'AIRLINES
                    objAuxFare.AirLines = ConnNM.LeeColumnasDataReader(objOracleDataReader, "AIRLINES", Nothing)

                    'PASSENGERTYPE / EQUIVALE / PERTENECE
                    objAuxTipoPasajero = New classDatosTipoPasajero
                    objAuxTipoPasajero.ID = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PASSENGERTYPE", Nothing)
                    objAuxTipoPasajero.Equivale = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EQUIVALE", Nothing)
                    objAuxTipoPasajero.Pertenece = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PERTENECE", Nothing)
                    If objAuxFare.PassengerType Is Nothing Then objAuxFare.PassengerType = New List(Of classDatosTipoPasajero)
                    objAuxFare.PassengerType.Add(objAuxTipoPasajero)

                    'FAREBASIS
                    objAuxFare.FareBasis = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FAREBASIS", Nothing)

                    'BOOKINGCLASS
                    objAuxFare.BookingClass = ConnNM.LeeColumnasDataReader(objOracleDataReader, "BOOKINGCLASS", Nothing)

                    'CABINA
                    objAuxFare.Cabin = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CABINA", Nothing)

                    'CURRENCY
                    objAuxFare.Currency = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CURRENCY", Nothing)

                    'BASEFARE
                    objAuxFare.BaseFare = ConnNM.LeeColumnasDataReader(objOracleDataReader, "BASEFARE", Nothing)
                    'objAuxFare.BaseFare = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ACCOUNTCODE", Nothing)
                    If String.IsNullOrEmpty(objAuxFare.BaseFare) Then
                        objAuxFare.BaseFare = "0.00"
                    Else
                        objAuxFare.BaseFare = FormatearNumero(objAuxFare.BaseFare, 2, False)
                        'objAuxFare.BaseFare = Format(CDbl(objAuxFare.BaseFare), "####.00")
                    End If

                    'FEE MINIMO
                    objAuxFare.FeeMinino = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FEEMINIMO", Nothing)
                    If String.IsNullOrEmpty(objAuxFare.FeeMinino) Then
                        objAuxFare.FeeMinino = "0.00"
                    Else
                        objAuxFare.FeeMinino = FormatearNumero(objAuxFare.FeeMinino, 2, True)
                        'objAuxFare.FeeMinino = Format(CDbl(objAuxFare.FeeMinino), "####.00")
                    End If

                    'FEE MAXIMO
                    objAuxFare.FeeMaximo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "FEEMAXIMO", Nothing)
                    If String.IsNullOrEmpty(objAuxFare.FeeMaximo) Then
                        objAuxFare.FeeMaximo = "0.00"
                    Else
                        objAuxFare.FeeMaximo = FormatearNumero(objAuxFare.FeeMaximo, 2, True)
                        'objAuxFare.FeeMaximo = Format(CDbl(objAuxFare.FeeMaximo), "####.00")
                    End If

                    'EXPIRATIONDATE
                    objAuxFare.ExpirationDate = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EXPIRATIONDATE", Nothing)

                    'EFFECTIVEDATE
                    objAuxFare.EffectiveDate = ConnNM.LeeColumnasDataReader(objOracleDataReader, "EFFECTIVEDATE", Nothing)

                    'TICKETDATE
                    objAuxFare.TicketDate = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TICKETDATE", Nothing)

                    'ADVANCEPURCHASE
                    objAuxFare.AdvancePurchase = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ADVANCEPURCHASE", Nothing)

                    'MINSTAY
                    objAuxFare.MinStay = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MINSTAY", Nothing)

                    'MAXSTAY
                    objAuxFare.MaxStay = ConnNM.LeeColumnasDataReader(objOracleDataReader, "MAXSTAY", Nothing)

                    'ACCOUNTCODE
                    'objAuxFare.MaxStay = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ACCOUNTCODE", Nothing)


                    'PSEUDO
                    objPseudos = New classFQ.classPseudoFare
                    objPseudos.Pseudo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PSEUDO", Nothing)
                    objPseudos.IDs = ConnNM.LeeColumnasDataReader(objOracleDataReader, "IDPSEUDO", -1)

                    objAuxFare.Pseudos = New List(Of classFQ.classPseudoFare)
                    objAuxFare.Pseudos.Add(objPseudos)

                    'SEASONALAPPLIC
                    objAuxFare.SeasonalApplication = ConnNM.LeeColumnasDataReader(objOracleDataReader, "SEASONALAPPLIC", Nothing)

                    'REGLAS
                    objAuxFare.RuleCategory = New List(Of String)
                    strCadenaReglas = ConnNM.LeeColumnasDataReader(objOracleDataReader, "REGLAS", Nothing)
                    If Not String.IsNullOrEmpty(strCadenaReglas) Then
                        For i As Integer = 0 To strCadenaReglas.Split(Constantes.Slash).Length - 1
                            objAuxFare.RuleCategory.Add(strCadenaReglas.Split(Constantes.Slash)(i))
                        Next
                    End If
                    'TIPO TARIFA 
                    objAuxFare.tipoTarifa = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TIPO_TARIFA", Nothing)

                    If objFareRS Is Nothing Then objFareRS = New List(Of classFQ.classFareRS)
                    objFareRS.Add(objAuxFare)

                    intContador += 1
                End While

            Catch ex As Exception
                objFareRS = Nothing
                strLog = "Stored Procedure : " & Constantes.spTARIFAS_FQ & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerTarifasFQ" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerTarifasFQ", strCodigoSeguimiento)

                Err.Raise(10, "ObtenerTarifasFQ", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strPNR = Nothing
                strDK = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try

            Return objFareRS

        End Function
        Private Function FormatearNumero(ByVal Valor As String, ByVal CantidadDecimales As Integer, Optional ByVal SeparadorMiles As Boolean = False) As String
            Dim retorno As String = ""                                            ' Valor a devolver
            Try
                If IsNumeric(Valor) = True Then                                     ' Valor es numerico?
                    Dim formato As String = "###,###,##0."                            ' Formato deseado
                    If SeparadorMiles = False Then formato = formato.Replace(",", "") ' Reemplazar las (,) 
                    For i As Integer = 0 To CantidadDecimales - 1                     ' Por la cantidad deseada
                        formato &= "0"                                                  ' Añadir los valores
                    Next
                    retorno = Format(Convert.ToDouble(Valor), formato).ToString()     ' Convertir a cadena el retorno
                Else
                    Throw New Exception("Parametro no era numérico")
                End If
            Catch ex As Exception
                retorno = ""
            End Try
            Return retorno
        End Function
        Public Function ListaTarifasPromocionales(ByVal strPseudo As String, _
                                                   ByVal strCodigoSeguimiento As String, _
                                                   ByVal intFirmaDB As Integer, _
                                                   ByVal intEsquema As Integer) As List(Of ClsTarifasMain_ODD)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim lista_TarifasPromo As New List(Of ClsTarifasMain_ODD)
            Try
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.sp_ODD_GETTARIFAS_PROMO, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@strPseudo", strPseudo, OracleDbType.Varchar2, strPseudo.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)
                objOracleDataReader = ConnNM._ExecuteReader()
                While objOracleDataReader.Read
                    Dim tarifa_Promo As New ClsTarifasMain_ODD(ConnNM.LeeColumnasDataReader(objOracleDataReader, "INTCORRELATIVO", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "STRPSEUDO", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "STRAIRLINES", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CIUDAD_OR", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CIUDAD_DES", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "STRTIPOTARIFA", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "STRFAREBASIS", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "BASEFARE_AMOUNT", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "TOTALFARE_AMOUNT", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "STRFECHASALIDA", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "STRFECHARETORNO", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "STRCLASE", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "STRFECHAVIGENCIA", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "STRFECHAEMISION", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "FECHA_REGISTRO", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "STRHOSTCOMMAND", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "STRTIPO_PASAJERO", Nothing))
                    lista_TarifasPromo.Add(tarifa_Promo)
                End While
            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.sp_ODD_GETTARIFAS_PROMO & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ListaTarifasPromocionales" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ListaTarifasPromocionales", strCodigoSeguimiento)
                Err.Raise(10, "ListaTarifasPromocionales", ex.ToString)
            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return lista_TarifasPromo
        End Function
        Public Function ODD_InsertarItin_Ind(ByVal id_ciudad_or As String, _
                                             ByVal id_ciudad_des As String, _
                                             ByVal strAirlines As String, _
                                             ByVal strpseudo_reg As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As Boolean
            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean
            Try
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.sp_INSERT_ITIN, Constantes.StoredProcedure) 'CARGA LA TABLA MEDIANTE XML
                ConnNM.AgregarParametro("@p_id_ciudad_or", id_ciudad_or, OracleDbType.Varchar2, id_ciudad_or.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_id_ciudad_des", id_ciudad_des, OracleDbType.Varchar2, id_ciudad_des.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_strAirlines", strAirlines, OracleDbType.Varchar2, strAirlines.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_strpseudo_reg", strpseudo_reg, OracleDbType.Varchar2, strpseudo_reg.Length, ParameterDirection.Input)
                '----------
                bolResultado = ConnNM._InsertExecuteNonQuery()
            Catch ex As Exception
                bolResultado = False
                strLog = "Stored Procedure : " & Constantes.spGDS_MC_INS_CONCEPTOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ODD_InsertarItin_Ind" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ODD_InsertarItin_Ind", strCodigoSeguimiento)


            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return bolResultado
        End Function
        Public Function ODD_ListarItinerario(ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As List(Of ClsItinerario_ODD)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim lista_Itinerario As New List(Of ClsItinerario_ODD)
            Try
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.sp_ODD_GETITINERARIO, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@pCurResult_out", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)
                objOracleDataReader = ConnNM._ExecuteReader()
                While objOracleDataReader.Read
                    Dim itinerario As New ClsItinerario_ODD(ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CIUDAD_OR", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_CIUDAD_DES", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "AIRLINES", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "STRPSEUDO_REG", Nothing),
                                                                               ConnNM.LeeColumnasDataReader(objOracleDataReader, "DTFECHA_REGISTRO", Nothing))
                    lista_Itinerario.Add(itinerario)
                End While
            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.sp_ODD_GETTARIFAS_PROMO & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ODD_ListarItinerario" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ODD_ListarItinerario", strCodigoSeguimiento)
                Err.Raise(10, "ODD_ListarItinerario", ex.ToString)
            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return lista_Itinerario
        End Function
        Public Function ODD_CargarExcel_ODD_OLD(ByVal strXML As String, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As Boolean
            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean
            Dim nameTable As String = "NUEVOMUNDO.GDS_ODD_ITINERARIO"
            Try
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.sp_ODD_DELETEITINERARIO_ALL, Constantes.StoredProcedure) 'CARGA LA TABLA MEDIANTE XML
                bolResultado = ConnNM._DeleteExecuteNonQuery()
                ConnNM.SP_Command(Constantes.GDS_INSERT_XMLtoTABLE, Constantes.StoredProcedure) 'CARGA LA TABLA MEDIANTE XML
                ConnNM.AgregarParametro("@p_Tabla", nameTable, OracleDbType.Varchar2, nameTable.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Xml", strXML, OracleDbType.Varchar2, strXML.Length, ParameterDirection.Input)
                bolResultado = ConnNM._InsertExecuteNonQuery()
            Catch ex As Exception
                bolResultado = False
                strLog = "Stored Procedure : " & Constantes.spGDS_MC_INS_CONCEPTOS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ODD_CargarExcel_ODD" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ODD_CargarExcel_ODD", strCodigoSeguimiento)
            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return bolResultado
        End Function

        Public Function ODD_CargarExcel_ODD(ByVal strXML As String, _
                                            ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As Boolean
            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean
            Dim nameTable As String = "NUEVOMUNDO.GDS_ODD_ITINERARIO"
            Try
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.GDS_INSERT_XMLtoTABLE, Constantes.StoredProcedure) 'CARGA LA TABLA MEDIANTE XML
                ConnNM.AgregarParametro("@p_Tabla", nameTable, OracleDbType.Varchar2, nameTable.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Xml", strXML, OracleDbType.Varchar2, strXML.Length, ParameterDirection.Input)
                bolResultado = ConnNM._InsertExecuteNonQuery()
            Catch ex As Exception
                bolResultado = False
                strLog = "Stored Procedure : " & Constantes.GDS_INSERT_XMLtoTABLE & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ODD_CargarExcel_ODD" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ODD_CargarExcel_ODD", strCodigoSeguimiento)
            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return bolResultado
        End Function

        Public Function ODD_DeleteItinerario_ALL(ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As Boolean
            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean
            Dim nameTable As String = "NUEVOMUNDO.GDS_ODD_ITINERARIO"
            Try
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.sp_ODD_DELETEITINERARIO_ALL, Constantes.StoredProcedure) 'ELIMINA TABLA
                bolResultado = ConnNM._DeleteExecuteNonQuery()
            Catch ex As Exception
                bolResultado = False
                strLog = "Stored Procedure : " & Constantes.sp_ODD_DELETEITINERARIO_ALL & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ODD_DELETEITINERARIO_ALL" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ODD_DELETEITINERARIO_ALL", strCodigoSeguimiento)
            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return bolResultado
        End Function

        Public Function ODD_ObtenerTipoPax(ByVal strCodigoSeguimiento As String, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As List(Of classTipoPaxVuelo)
            Dim lista_TipoPax As New List(Of classTipoPaxVuelo)
            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Try
                ConnNM.Connect(intFirmaDB)

                ConnNM.SP_Command(Constantes.SP_ODD_GETTIPO_DE_PAX, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)
                objOracleDataReader = ConnNM._ExecuteReader()
                While objOracleDataReader.Read
                    Dim tipoPax As New classTipoPaxVuelo
                    tipoPax.IdTipoDePax = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_TIPO_DE_PAX_VUELO", Nothing)
                    tipoPax.Aerolinea = ConnNM.LeeColumnasDataReader(objOracleDataReader, "AEROLINEA", Nothing)
                    lista_TipoPax.Add(tipoPax)
                End While
            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.SP_ODD_GETTIPO_DE_PAX & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ODD_ListarItinerario" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ODD_ObtenerTipoPax", strCodigoSeguimiento)
                Err.Raise(10, "ODD_ObtenerTipoPax", ex.ToString)
            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return lista_TipoPax
        End Function
    End Class
End Namespace
