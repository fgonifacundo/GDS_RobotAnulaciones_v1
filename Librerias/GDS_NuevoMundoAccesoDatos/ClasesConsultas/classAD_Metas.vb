Imports GDS_NuevoMundoPersistencia
Imports System.Data.SqlClient
Imports System.Data
Imports Oracle.DataAccess.Client
Imports System.Text
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports System.IO

Namespace ObjetoAccesoDatos
    Partial Public Class classDAO

        Public Function ObtenerDataTable(ByVal strSelect As String)
            Dim oDt As New DataTable
            Dim SqlConection As String = classCadenaBD.CadenaSqlPtaDestinos
            Try
                Using objCnx As New SqlConnection(SqlConection)
                    Using objComm As New SqlCommand(strSelect, objCnx)
                        objCnx.Open()
                        objComm.CommandTimeout = 5000
                        Dim reader As SqlDataReader = objComm.ExecuteReader()
                        If reader.HasRows Then
                            oDt.Load(reader)
                        End If
                    End Using
                    objCnx.Close()
                End Using
            Catch ex As Exception
                Throw ex
            End Try
            Return oDt
        End Function

        Public Sub EscribirLogException(ByVal ex As Exception, ByVal Source As String, ByVal strQuery As String, ByVal strNomCarpeta As String)
            Dim strLog As String = Nothing
            Dim objEscribeLog As New GDS_MuevoMundoLog.EscribeLog
            strLog &= Constantes.TabEspacios & "Source : " & Source & vbCrLf
            strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
            strLog &= Constantes.TabEspacios & "Query  :  " & strQuery & vbCrLf
            WriteLog(strLog, Source, strNomCarpeta)

        End Sub

        Public Function ObtenerReporteEjecutivosVentasDM(ByVal strFechaInicio As String, ByVal strFechaFin As String) As DataTable
            Dim strSelect = ObtenerQueryReporteEjecutivosVentasTripoint(strFechaInicio, strFechaFin)
            Dim oDt As New DataTable
            Try
                oDt = ObtenerDataTable(strSelect)
            Catch ex As Exception
                EscribirLogException(ex, "ObtenerReporteEjecutivosVentasDM", strSelect, "ReporteMetasDM")
            End Try
            Return oDt
        End Function

        Public Function ObtenerQueryReporteEjecutivosVentasTripoint(ByVal strFechaInicio As String, ByVal strFechaFin As String) As String

            Dim rptEjecutivoVentas As String = "SELECT Trek_File_InnerStatesHistory.[Date] As Fecha, Users.[Abreviation] AS Sigla, Users.[Name] AS Responsable, Trek_DetailTicket.[File] AS File1, " & _
            "Entity.[Name] AS Cliente, " & _
            "Trek_File.[Description] AS Descripcion, " & _
            "(CAST(YEAR(Trek_File.[Blocked_Date]) AS VARCHAR)+'/'+ RIGHT(CAST(100+MONTH(Trek_File.[Blocked_Date]) AS VARCHAR),2)+'/'+ RIGHT(CAST(100+DAY(Trek_File.[Blocked_Date]) AS VARCHAR),2)) AS FechaConferencia, " & _
            "SUM((ROUND(((Trek_DetailTicket.SellPrev+ ISNULL(CASE WHEN (Trek_DetailTicket.VatNotIncluded & 1)=1 THEN Trek_DetailTicket.SellPrevVat END,0))),2))) AS VentaPrevistaConIgv, " & _
            "0 AS VentaPrevistaSinIgv, " & _
            "SUM((ROUND((SellReal +ISNULL(CASE WHEN (Trek_DetailTicket.VatNotIncluded & 1)=1 THEN Trek_DetailTicket.SellRealVat END,0)),2))) AS VentaRealConIgv, " & _
            "SUM((ROUND((CASE WHEN  Trek_DetailTicket.Calc_FileType IN ('H','B') THEN Trek_DetailTicket.SellPrev + " & _
            "ISNULL(CASE WHEN (Trek_DetailTicket.VatNotIncluded & 1)=1 THEN Trek_DetailTicket.SellPrevVat END,0) ELSE 0 END),2))) AS ComisionPrevistaConIgv, " & _
            "SUM((ROUND((CASE WHEN  Trek_DetailTicket.Calc_FileType IN ('H','B') THEN Trek_DetailTicket.SellReal +" & _
            "ISNULL(CASE WHEN (Trek_DetailTicket.VatNotIncluded & 1)=1 THEN Trek_DetailTicket.SellRealVat END,0) ELSE 0 END ),2))) AS ComisionRealConIgv, " & _
            "SUM((ROUND(((Trek_DetailTicket.BuyPrev+ ISNULL(CASE WHEN (Trek_DetailTicket.VatNotIncluded & 1)=1 THEN Trek_DetailTicket.BuyPrevVat END,0))),2))) AS CostoPrevistoConIgv, " & _
            "0 AS CostoPrevistoSinIgv, " & _
            "0 AS CostoRealSinIgv, " & _
            "SUM((ROUND((Trek_DetailTicket.BuyReal+ ISNULL(CASE WHEN (Trek_DetailTicket.VatNotIncluded & 1)=1 THEN Trek_DetailTicket.BuyRealVat END,0)),2))) AS CostoRealConIgv," & _
            "SUM((ROUND((CASE WHEN Trek_DetailTicket.entity_typeOK='F' AND (Trek_DetailTicket.BuyState<>'C' OR ISNULL(Trek_DetailTicket.TR,0)<>0) THEN " & _
            "((ISNULL(Trek_DetailTicket.BuyPrev,0) + ISNULL(CASE WHEN (Trek_DetailTicket.VatNotIncluded & 1)=1 THEN Trek_DetailTicket.BuyPrevVat END,0)   )   +   " & _
            "(ISNULL(Trek_DetailTicket.SellPrev,0)  + ISNULL(CASE WHEN (Trek_DetailTicket.VatNotIncluded & 1)=1 THEN Trek_DetailTicket.SellPrevVat END,0) ) )  -  " & _
            "((ISNULL(Trek_DetailTicket.SellReal,0)  +ISNULL(CASE WHEN (Trek_DetailTicket.VatNotIncluded & 1)=1 THEN Trek_DetailTicket.SellRealVat END,0) )  +   " & _
            "(ISNULL(Trek_DetailTicket.BuyReal,0)  + ISNULL(CASE WHEN (Trek_DetailTicket.VatNotIncluded & 1)=1 THEN Trek_DetailTicket.BuyRealVat END,0)  )    )* " & _
            "ISNULL(CASE WHEN Trek_DetailTicket.Moeda<>Trek_DetailTicket.MoedaPrev THEN ISNULL(Trek_DetailTicket.Exchange,1)/(CASE WHEN ISNULL(Trek_DetailTicket.ExchangePrev,1)=0 " & _
            "THEN 1 ELSE ISNULL(Trek_DetailTicket.ExchangePrev,1) END) END,1)  ELSE 0 END),2))) AS CostoPorFacturarConIgv, " & _
            "SUM((ROUND((( ISNULL(Trek_DetailTicket.SellPrev,0) +ISNULL(Trek_DetailTicket.BuyPrev,0)  + ISNULL(CASE WHEN (Trek_DetailTicket.VatNotIncluded & 1)=1 " & _
            "THEN Trek_DetailTicket.SellPrevVat+Trek_DetailTicket.BuyPrevVat END,0) )*[dbo].[GetVatMarginal]('M') ),2))) AS LucroPrevistoConIgv, " & _
            "SUM((ROUND((( ISNULL(Trek_DetailTicket.SellReal,0) +ISNULL(CASE WHEN (Trek_DetailTicket.VatNotIncluded & 1)=1 " & _
            "THEN Trek_DetailTicket.SellRealVat+Trek_DetailTicket.BuyRealVat END,0)  +ISNULL(Trek_DetailTicket.BuyReal,0) ) ),2))) AS LucroRealConIgv, " & _
            "FileType.[Descr] AS FileTipo1, " & _
            "FileType2.[Descr] AS FileTipo2, " & _
            "0 AS VentaPorFacturarConIgv, " & _
            "22 as IdArea " & _
            " FROM [Trek_DetailTicket] Trek_DetailTicket WITH (NOLOCK)" & _
            " LEFT JOIN [Trek_File] Trek_File WITH (NOLOCK) ON Trek_File.[Cod] = Trek_DetailTicket.[File]" & _
            " LEFT JOIN [Trek_File_InnerStatesHistory] Trek_File_InnerStatesHistory WITH (NOLOCK) ON Trek_File_InnerStatesHistory.[File] = Trek_File.[Cod]" & _
            " LEFT JOIN [FileType2] FileType2 WITH (NOLOCK) ON FileType2.[Cod] = Trek_File.[File_Type2]" & _
            " LEFT JOIN [Entity] Entity WITH (NOLOCK) ON Entity.[Entity] = Trek_File.[Entity_Abreviation]" & _
            " LEFT JOIN [Users] Users WITH (NOLOCK) ON Users.[Abreviation] = Entity.[Contacto]" & _
            " LEFT JOIN [FileType] FileType WITH (NOLOCK) ON FileType.[Cod] = Trek_File.[File_Type]" & _
            " WHERE(Trek_File.[Deleted_Date] Is NULL)" & _
            " AND Trek_File.[Cod]>0 " & _
            " AND (ISNULL(Trek_File.[File_Type],0)<>28 " & _
            " AND ISNULL(Trek_File.[File_Type],0)<>31 " & _
            " AND ISNULL(Trek_File.[File_Type],0)<>34 " & _
            " AND ISNULL(Trek_File.[File_Type],0)<>43 " & _
            " AND ISNULL(Trek_File.[File_Type],0)<>35 " & _
            " AND ISNULL(Trek_File.[File_Type],0)<>38)" & _
            " AND (Users.[Abreviation] IS NOT NULL " & _
            " AND ISNULL(Users.[Abreviation],'')<>'CCARDENAS' " & _
            " AND ISNULL(Users.[Abreviation],'')<>'sa') " & _
            " AND (ISNULL(Trek_File.[File_Type2],0)<>131 AND ISNULL(Trek_File.[File_Type2],0)<>139) " & _
            " AND Trek_File_InnerStatesHistory.[StateField] =1447 " & _
            " AND Trek_File_InnerStatesHistory.[StateValue] IS NOT NULL " & _
            " AND convert(SMALLDATETIME,Trek_File.[Blocked_Date]) BETWEEN convert(SMALLDATETIME, '" & strFechaInicio & "' ,103) AND convert(SMALLDATETIME, '" & strFechaFin & "',103)" & _
            " AND Trek_File_InnerStatesHistory.[Date] = (select max(r.[Date]) from [Trek_File_InnerStatesHistory] r where r.[File] = Trek_DetailTicket.[File]) " & _
            " GROUP BY Trek_File_InnerStatesHistory.[Date], Users.[Abreviation], " & _
            " Users.[Name], " & _
            " Trek_DetailTicket.[File], " & _
            " Entity.[Name], " & _
            " Trek_File.[Description], " & _
            " (CAST(YEAR(Trek_File.[Blocked_Date]) AS VARCHAR)+'/'+ " & _
            " RIGHT(CAST(100+MONTH(Trek_File.[Blocked_Date]) AS VARCHAR),2)+'/' + " & _
            " RIGHT(CAST(100+DAY(Trek_File.[Blocked_Date]) AS VARCHAR),2)), " & _
            " FileType.[Descr], " & _
            " FileType2.[Descr] " & _
            " HAVING   (SUM((Trek_DetailTicket.SellReal +ISNULL(CASE WHEN (Trek_DetailTicket.VatNotIncluded & 1)=1 THEN Trek_DetailTicket.SellRealVat END,0) ) " & _
            "*ISNULL(Trek_DetailTicket.Exchange,1))>0 OR " & _
            " SUM((Trek_DetailTicket.SellReal +ISNULL(CASE WHEN (Trek_DetailTicket.VatNotIncluded & 1)=1 THEN Trek_DetailTicket.SellRealVat END,0) ) *" & _
            " ISNULL(Trek_DetailTicket.Exchange,1))<0)"

            '            "AND DATEPART(YEAR,Trek_File_InnerStatesHistory.[Date]) = " & strAnio & " AND DATEPART(MM,Trek_File_InnerStatesHistory.[Date]) = " & strMes & " " & _

            Return rptEjecutivoVentas

        End Function

        Public Function ObtenerReporteCounterEuropaDM(ByVal strFechaInicio As String, ByVal strFechaFin As String) As DataTable
            Dim strSelect = ObtenerQueryCounterEuropa(strFechaInicio, strFechaFin)
            Dim oDt As New DataTable
            Try
                oDt = ObtenerDataTable(strSelect)
            Catch ex As Exception
                EscribirLogException(ex, "ObtenerReporteCounterEuropaDM", strSelect, "ReporteMetasDM")
            End Try
            Return oDt
        End Function

        Public Function ObtenerQueryCounterEuropa(ByVal strFechaInicio As String, ByVal strFechaFin As String) As String
            Dim rptCounterEuropa As String = "SELECT T99R114D610.[Date] As Fecha, T99R114R174.[Abreviation] AS Sigla, " & _
            "T99R114R174.[Name] AS Responsable, " & _
            "T99.[File] AS File1, " & _
            "T99R114R90.[Name] AS Cliente, " & _
            "T99R114.[Description] AS Descripcion, " & _
            "(CAST(YEAR(T99R114.[Blocked_Date]) AS VARCHAR)+'/'+ RIGHT(CAST(100+MONTH(T99R114.[Blocked_Date]) AS VARCHAR),2)+'/'+ RIGHT(CAST(100+DAY(T99R114.[Blocked_Date]) AS VARCHAR),2)) AS FechaConferencia, " & _
            "SUM((ROUND(((T99.SellPrev+ ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellPrevVat END,0))),2))) AS VentaPrevistaConIgv, " & _
            "SUM((ROUND(((T99.SellPrev- ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=0 THEN T99.SellPrevVat END,0))),2))) AS VentaPrevistaSinIgv, " & _
            "SUM((ROUND((SellReal +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0)),2))) AS VentaRealConIgv, " & _
            "SUM((ROUND((CASE WHEN  T99.Calc_FileType IN ('H','B') THEN T99.SellPrev + " & _
            "ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellPrevVat END,0) ELSE 0 END),2))) AS ComisionPrevistaConIgv, " & _
            "SUM((ROUND((CASE WHEN  T99.Calc_FileType IN ('H','B') THEN T99.SellReal + " & _
            "ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) ELSE 0 END ),2))) AS ComisionRealConIgv, " & _
            "SUM((ROUND(((T99.BuyPrev+ ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyPrevVat END,0))),2))) AS CostoPrevistoConIgv, " & _
            "0 AS CostoPrevistoSinIgv, " & _
            "SUM((ROUND(((T99.BuyPrev- ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=0 THEN T99.BuyPrevVat END,0))),2))) AS CostoRealSinIgv, " & _
            "SUM((ROUND((T99.BuyReal+ ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyRealVat END,0)),2))) AS CostoRealConIgv, " & _
            "SUM((ROUND((CASE WHEN T99.entity_typeOK='F' AND (T99.BuyState<>'C' OR ISNULL(T99.TR,0)<>0) THEN " & _
            " ((ISNULL(T99.BuyPrev,0) + ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyPrevVat END,0))   +  " & _
            " (ISNULL(T99.SellPrev,0)  + ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellPrevVat END,0)))- " & _
            " ((ISNULL(T99.SellReal,0)  +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0))+ " & _
            " (ISNULL(T99.BuyReal,0)  + ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyRealVat END,0)))* " & _
            " ISNULL(CASE WHEN T99.Moeda<>T99.MoedaPrev THEN ISNULL(T99.Exchange,1)/(CASE WHEN ISNULL(T99.ExchangePrev,1)=0 THEN 1 " & _
            " ELSE ISNULL(T99.ExchangePrev,1) END) END,1)  ELSE 0 END),2))) AS CostoPorFacturarConIgv, " & _
            " SUM((ROUND((( ISNULL(T99.SellPrev,0) +" & _
            " ISNULL(T99.BuyPrev,0)  +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellPrevVat+T99.BuyPrevVat END,0))*[dbo].[GetVatMarginal]('M') ),2))) AS LucroPrevistoConIgv, " & _
            " SUM((ROUND((( ISNULL(T99.SellReal,0) +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat+T99.BuyRealVat END,0) " & _
            "  +ISNULL(T99.BuyReal,0) ) ),2))) AS LucroRealConIgv, " & _
            "  T99R114R92.[Descr] AS FileTipo1, " & _
            "  T99R114R113.[Descr] AS FileTipo2, " & _
            " 0 AS VentaPorFacturarConIgv, " & _
            " 3 as IdArea " & _
            "FROM [Trek_DetailTicket] T99 WITH (NOLOCK)" & _
            " LEFT JOIN [Trek_File] T99R114 WITH (NOLOCK) ON T99R114.[Cod] = T99.[File]" & _
            " LEFT JOIN [Trek_File_InnerStatesHistory] T99R114D610 WITH (NOLOCK) ON T99R114D610.[File] = T99R114.[Cod]" & _
            " LEFT JOIN [FileType2] T99R114R113 WITH (NOLOCK) ON T99R114R113.[Cod] = T99R114.[File_Type2]" & _
            " LEFT JOIN [Users] T99R114R174 WITH (NOLOCK) ON T99R114R174.[Abreviation] = T99R114.[Responsible]" & _
            " LEFT JOIN [Entity] T99R114R90 WITH (NOLOCK) ON T99R114R90.[Entity] = T99R114.[Entity_Abreviation]" & _
            " LEFT JOIN [FileType] T99R114R92 WITH (NOLOCK) ON T99R114R92.[Cod] = T99R114.[File_Type]" & _
            " WHERE T99R114.[Deleted_Date] Is NULL" & _
            " AND T99R114.[Cod]>0 " & _
            " AND T99R114R174.Groups Like '%DJ%' " & _
            " AND ISNULL(T99R114.[File_Type],0)<>43 " & _
            " AND T99R114D610.[StateField] =1447 " & _
            " AND T99R114D610.[StateValue] IS NOT NULL" & _
            " AND convert(SMALLDATETIME,T99R114.[Blocked_Date]) BETWEEN convert(SMALLDATETIME, '" & strFechaInicio & "' ,103) AND convert(SMALLDATETIME, '" & strFechaFin & "',103)" & _
            " AND T99R114D610.[Date] = (select max(r.[Date]) from [Trek_File_InnerStatesHistory] r where r.[File] = T99.[File]) " & _
            " GROUP BY T99R114D610.[Date], T99R114R174.[Abreviation], " & _
            " T99R114R174.[Name], " & _
            " T99.[File], " & _
            " T99R114R90.[Name], " & _
            " T99R114.[Description], " & _
            " (CAST(YEAR(T99R114.[Blocked_Date]) AS VARCHAR)+'/'+ " & _
            " RIGHT(CAST(100+MONTH(T99R114.[Blocked_Date]) AS VARCHAR),2)+'/'+ " & _
            " RIGHT(CAST(100+DAY(T99R114.[Blocked_Date]) AS VARCHAR),2)), " & _
            " T99R114R92.[Descr], " & _
            " T99R114R113.[Descr] " & _
            " HAVING (SUM((T99.SellReal +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) ) *ISNULL(T99.Exchange,1))>0 " & _
            " OR SUM((T99.SellReal +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) ) *ISNULL(T99.Exchange,1))<0)"


            ' "AND DATEPART(YEAR,T99R114D610.[Date]) = " & strAnio & " AND DATEPART(MM,T99R114D610.[Date]) = " & strMes & " " & _
            Return rptCounterEuropa
        End Function

        Public Function ObtenerReporteCounterGeneralDM(ByVal strFechaInicio As String, ByVal strFechaFin As String) As DataTable
            Dim strSelect = ObtenerQueryCounterGeneral(strFechaInicio, strFechaFin)
            Dim oDt As New DataTable
            Try
                oDt = ObtenerDataTable(strSelect)
            Catch ex As Exception
                EscribirLogException(ex, "ObtenerReporteCounterGeneralDM", strSelect, "ReporteMetasDM")
            End Try
            Return oDt
        End Function

        Public Function ObtenerQueryCounterGeneral(ByVal strFechaInicio As String, ByVal strFechaFin As String) As String
            Dim rptCounterGeneral As String = "SELECT T99R114D610.[Date] As Fecha, T99R114R174.[Abreviation] AS Sigla, T99R114R174.[Name] AS Responsable, T99.[File] AS File1, " & _
            "T99R114R90.[Name] AS Cliente, " & _
            "T99R114.[Description] AS Descripcion, " & _
            "(CAST(YEAR(T99R114.[Blocked_Date]) AS VARCHAR)+'/'+ RIGHT(CAST(100+MONTH(T99R114.[Blocked_Date]) AS VARCHAR),2)+'/'+ RIGHT(CAST(100+DAY(T99R114.[Blocked_Date]) AS VARCHAR),2)) AS FechaConferencia, " & _
            "SUM((ROUND(((T99.SellPrev+ ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellPrevVat END,0))),2))) AS VentaPrevistaConIgv, " & _
            "SUM((ROUND(((T99.SellPrev- ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=0 THEN T99.SellPrevVat END,0))),2))) AS VentaPrevistaSinIgv, " & _
            "SUM((ROUND((SellReal +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0)),2))) AS VentaRealConIgv, " & _
            "SUM((ROUND((CASE WHEN  T99.Calc_FileType IN ('H','B') THEN T99.SellPrev + " & _
            "ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellPrevVat END,0) ELSE 0 END),2))) AS ComisionPrevistaConIgv, " & _
            "SUM((ROUND((CASE WHEN  T99.Calc_FileType IN ('H','B') THEN T99.SellReal +" & _
            "ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) ELSE 0 END ),2))) AS ComisionRealConIgv, " & _
            "SUM((ROUND(((T99.BuyPrev+ ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyPrevVat END,0))),2))) AS CostoPrevistoConIgv, " & _
            "SUM((ROUND(((T99.BuyPrev- ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=0 THEN T99.BuyPrevVat END,0))),2))) AS CostoPrevistoSinIgv, " & _
            "0 AS CostoRealSinIgv, " & _
            "SUM((ROUND((T99.BuyReal+ ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyRealVat END,0)),2))) AS CostoRealConIgv, " & _
            "SUM((ROUND((CASE WHEN T99.entity_typeOK='F' AND (T99.BuyState<>'C' OR ISNULL(T99.TR,0)<>0) THEN " & _
            "((ISNULL(T99.BuyPrev,0) + ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyPrevVat END,0)) + " & _
            "(ISNULL(T99.SellPrev,0)  + ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellPrevVat END,0))) - ((ISNULL(T99.SellReal,0)  +" & _
            "ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) )  +   " & _
            "(ISNULL(T99.BuyReal,0)  + ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyRealVat END,0)))*" & _
            "ISNULL(CASE WHEN T99.Moeda<>T99.MoedaPrev THEN ISNULL(T99.Exchange,1)/(CASE WHEN ISNULL(T99.ExchangePrev,1)=0 " & _
            "THEN 1 ELSE ISNULL(T99.ExchangePrev,1) END) END,1)  ELSE 0 END),2))) AS CostoPorFacturarConIgv, " & _
            "SUM((ROUND((( ISNULL(T99.SellPrev,0) +ISNULL(T99.BuyPrev,0)  +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 " & _
            "THEN T99.SellPrevVat+T99.BuyPrevVat END,0) )*[dbo].[GetVatMarginal]('M') ),2))) AS LucroPrevistoConIgv, " & _
            "SUM((ROUND((( ISNULL(T99.SellReal,0) +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 " & _
            "THEN T99.SellRealVat+T99.BuyRealVat END,0)  +ISNULL(T99.BuyReal,0) ) ),2))) AS LucroRealConIgv, " & _
            "T99R114R92.[Descr] AS FileTipo1, " & _
            "T99R114R113.[Descr] AS FileTipo2, " & _
            "0 AS VentaPorFacturarConIgv, " & _
            "1 as IdArea " & _
            "FROM [Trek_DetailTicket] T99 WITH (NOLOCK)" & _
            " LEFT JOIN [Trek_File] T99R114 WITH (NOLOCK) ON T99R114.[Cod] = T99.[File]" & _
            " LEFT JOIN [Trek_File_InnerStatesHistory] T99R114D610 WITH (NOLOCK) ON T99R114D610.[File] = T99R114.[Cod]" & _
            " LEFT JOIN [FileType2] T99R114R113 WITH (NOLOCK) ON T99R114R113.[Cod] = T99R114.[File_Type2]" & _
            " LEFT JOIN [Users] T99R114R174 WITH (NOLOCK) ON T99R114R174.[Abreviation] = T99R114.[Responsible]" & _
            " LEFT JOIN [Entity] T99R114R90 WITH (NOLOCK) ON T99R114R90.[Entity] = T99R114.[Entity_Abreviation]" & _
            " LEFT JOIN [FileType] T99R114R92 WITH (NOLOCK) ON T99R114R92.[Cod] = T99R114.[File_Type]" & _
            " WHERE T99R114.[Deleted_Date] Is NULL And T99R114.[Cod] > 0" & _
            " AND ISNULL(T99R114R174.Groups,'') NOT Like '%DJ%' " & _
            " AND ISNULL(T99R114.[File_Type],0)<>43 " & _
            " AND T99R114D610.[StateField] =1447 " & _
            " AND T99R114D610.[StateValue] IS NOT NULL " & _
            " AND convert(SMALLDATETIME,T99R114.[Blocked_Date]) BETWEEN convert(SMALLDATETIME, '" & strFechaInicio & "' ,103) AND convert(SMALLDATETIME, '" & strFechaFin & "',103)" & _
            " AND T99R114D610.[Date] = (select max(r.[Date]) from [Trek_File_InnerStatesHistory] r where r.[File] = T99.[File]) " & _
            " GROUP BY T99R114D610.[Date], T99R114R174.[Abreviation], " & _
            " T99R114R174.[Name], " & _
            " T99.[File], " & _
            " T99R114R90.[Name], " & _
            " T99R114.[Description], " & _
            " (CAST(YEAR(T99R114.[Blocked_Date]) AS VARCHAR)+'/'+ " & _
            " RIGHT(CAST(100+MONTH(T99R114.[Blocked_Date]) AS VARCHAR),2)+'/'+ " & _
            " RIGHT(CAST(100+DAY(T99R114.[Blocked_Date]) AS VARCHAR),2)), " & _
            " T99R114R92.[Descr]," & _
            " T99R114R113.[Descr]" & _
            " HAVING (SUM((T99.SellReal +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) ) " & _
            " *ISNULL(T99.Exchange,1))>0 OR SUM((T99.SellReal +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) ) " & _
            " *ISNULL(T99.Exchange,1))<0)"

            '  "AND DATEPART(YEAR,T99R114D610.[Date]) = " & strAnio & " AND DATEPART(MM,T99R114D610.[Date]) = " & strMes & " " & _
            Return rptCounterGeneral
        End Function

        Public Function ObtenerReporteCrucerosDM(ByVal strFechaInicio As String, ByVal strFechaFin As String) As DataTable
            Dim strSelect = ObtenerQueryCruceros(strFechaInicio, strFechaFin)
            Dim oDt As New DataTable
            Try
                oDt = ObtenerDataTable(strSelect)
            Catch ex As Exception
                EscribirLogException(ex, "ObtenerReporteCrucerosDM", strSelect, "ReporteMetasDM")
            End Try
            Return oDt
        End Function

        Public Function ObtenerQueryCruceros(ByVal strFechaInicio As String, ByVal strFechaFin As String) As String
            Dim rptCruceros As String = "SELECT T99R114D610.[Date] As Fecha, T99R114R90R233.[Abreviation] AS Sigla, T99R114R90R233.[Name] AS Responsable, " & _
            "T99.[File] AS File1, T99R114R90.[Name] AS Cliente, T99R114.[Description] AS Descripcion, " & _
            "(CAST(YEAR(T99R114.[Blocked_Date]) AS VARCHAR)+'/'+ " & _
            "RIGHT(CAST(100+MONTH(T99R114.[Blocked_Date]) AS VARCHAR),2)+'/'+ " & _
            "RIGHT(CAST(100+DAY(T99R114.[Blocked_Date]) AS VARCHAR),2)) AS FechaConferencia, " & _
            "SUM((ROUND(((T99.SellPrev+ ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellPrevVat END,0))),2))) AS VentaPrevistaConIgv, " & _
            "SUM((ROUND(((T99.SellPrev- ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=0 THEN T99.SellPrevVat END,0))),2))) AS VentaPrevistaSinIgv, " & _
            "SUM((ROUND((SellReal +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0)),2))) AS VentaRealConIgv, " & _
            "SUM((ROUND((CASE WHEN  T99.Calc_FileType IN ('H','B') THEN T99.SellPrev +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 " & _
            "THEN T99.SellPrevVat END,0) ELSE 0 END),2))) AS ComisionPrevistaConIgv, " & _
            "SUM((ROUND((CASE WHEN  T99.Calc_FileType IN ('H','B') THEN T99.SellReal +" & _
            "ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) ELSE 0 END ),2))) AS ComisionRealConIgv, " & _
            "SUM((ROUND(((T99.BuyPrev+ ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyPrevVat END,0))),2))) AS CostoPrevistoConIgv, " & _
            "SUM((ROUND(((T99.BuyPrev- ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=0 THEN T99.BuyPrevVat END,0))),2))) AS CostoPrevistoSinIgv,  " & _
            "0 AS CostoRealSinIgv, " & _
            "SUM((ROUND((T99.BuyReal+ ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyRealVat END,0)),2))) AS CostoRealConIgv, " & _
            "SUM((ROUND((CASE WHEN T99.entity_typeOK='F' AND (T99.BuyState<>'C' OR ISNULL(T99.TR,0)<>0) THEN " & _
            "((ISNULL(T99.BuyPrev,0) + ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyPrevVat END,0))   + " & _
            "(ISNULL(T99.SellPrev,0)  + ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellPrevVat END,0)))  - " & _
            "((ISNULL(T99.SellReal,0)  +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) )  + " & _
            "(ISNULL(T99.BuyReal,0)  + ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyRealVat END,0)) )*" & _
            "ISNULL(CASE WHEN T99.Moeda<>T99.MoedaPrev THEN ISNULL(T99.Exchange,1)/(CASE WHEN ISNULL(T99.ExchangePrev,1)=0 " & _
            "THEN 1 ELSE ISNULL(T99.ExchangePrev,1) END) END,1)  ELSE 0 END),2))) AS CostoPorFacturarConIgv, " & _
            "SUM((ROUND((( ISNULL(T99.SellPrev,0) +" & _
            "ISNULL(T99.BuyPrev,0)  +" & _
            "ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellPrevVat+T99.BuyPrevVat END,0) )*[dbo].[GetVatMarginal]('M') ),2))) AS LucroPrevistoConIgv, " & _
            "SUM((ROUND((( ISNULL(T99.SellReal,0) +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat+T99.BuyRealVat END,0)  +" & _
            "ISNULL(T99.BuyReal,0) ) ),2))) AS LucroRealConIgv, " & _
            "T99R114R92.[Descr] AS FileTipo1, " & _
            "T99R114R113.[Descr] AS FileTipo2, " & _
            "SUM((ROUND((CASE WHEN T99.entity_typeOK='C' THEN  " & _
            "ISNULL(CASE WHEN T99.SellState='O' THEN T99.SellPrev+" & _
            "ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellprevVat END,0) END,0) -(ISNULL(T99.SellReal,0)+ISNULL(T99.SellRealDisc,0)+" &
            "ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0))*ISNULL(CASE WHEN T99.Moeda<>T99.MoedaPrev THEN " & _
            "ISNULL(T99.Exchange,1)/(CASE WHEN ISNULL(T99.ExchangePrev,1)=0 THEN 1 ELSE ISNULL(T99.ExchangePrev,1) END) END,1)  ELSE 0 END),2))) AS VentaPorFacturarConIgv, " & _
            "2 as IdArea " & _
            "FROM [Trek_DetailTicket] T99 WITH (NOLOCK) " & _
            "LEFT JOIN [Trek_File] T99R114 WITH (NOLOCK) ON T99R114.[Cod] = T99.[File] " & _
            "LEFT JOIN [Trek_File_InnerStatesHistory] T99R114D610 WITH (NOLOCK) ON T99R114D610.[File] = T99R114.[Cod] " & _
            "LEFT JOIN [FileType2] T99R114R113 WITH (NOLOCK) ON T99R114R113.[Cod] = T99R114.[File_Type2] " & _
            "LEFT JOIN [Entity] T99R114R90 WITH (NOLOCK) ON T99R114R90.[Entity] = T99R114.[Entity_Abreviation] " & _
            "LEFT JOIN [Users] T99R114R90R233 WITH (NOLOCK) ON T99R114R90R233.[Abreviation] = T99R114R90.[Contacto] " & _
            "LEFT JOIN [FileType] T99R114R92 WITH (NOLOCK) ON T99R114R92.[Cod] = T99R114.[File_Type] " & _
            " WHERE T99R114.[Deleted_Date] Is NULL" & _
            " AND T99R114.[Cod]>0 " & _
            " AND T99R114.[File_Type] =31 " & _
            " AND T99R114D610.[StateField] =1447 " & _
            " AND T99R114D610.[StateValue] IS NOT NULL " & _
            " AND convert(SMALLDATETIME,T99R114.[Blocked_Date]) BETWEEN convert(SMALLDATETIME, '" & strFechaInicio & "' ,103) AND convert(SMALLDATETIME, '" & strFechaFin & "',103)" & _
            " AND T99R114D610.[Date] = (select max(r.[Date]) from [Trek_File_InnerStatesHistory] r where r.[File] = T99.[File]) " & _
            " GROUP BY T99R114D610.[Date],T99R114R90R233.[Abreviation], " & _
            " T99R114R90R233.[Name], " & _
            " T99.[File], " & _
            " T99R114R90.[Name], " & _
            " T99R114.[Description], " & _
            " (CAST(YEAR(T99R114.[Blocked_Date]) AS VARCHAR)+'/'+ " & _
            " RIGHT(CAST(100+MONTH(T99R114.[Blocked_Date]) AS VARCHAR),2)+'/'+ " & _
            " RIGHT(CAST(100+DAY(T99R114.[Blocked_Date]) AS VARCHAR),2)), " & _
            " T99R114R92.[Descr]," & _
            " T99R114R113.[Descr]" & _
            " HAVING (SUM((T99.SellReal +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) ) *" & _
            " ISNULL(T99.Exchange,1))>0 OR SUM((T99.SellReal +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) ) *" & _
            " ISNULL(T99.Exchange,1))<0)"

            ' "AND DATEPART(YEAR,T99R114D610.[Date]) = " & strAnio & " AND DATEPART(MM,T99R114D610.[Date]) = " & strMes & " " & _
            Return rptCruceros
        End Function

        Public Function ObtenerReporteEjecutivoEuropaJuniorQuerolDM(ByVal strFechaInicio As String, ByVal strFechaFin As String) As DataTable
            Dim strSelect = ObtenerQueryEjecutivoEuropaJuniorQuerol(strFechaInicio, strFechaFin)
            Dim oDt As New DataTable
            Try
                oDt = ObtenerDataTable(strSelect)
            Catch ex As Exception
                EscribirLogException(ex, "ObtenerReporteEjecutivoEuropaJuniorQuerolDM", strSelect, "ReporteMetasDM")
            End Try
            Return oDt
        End Function

        Public Function ObtenerQueryEjecutivoEuropaJuniorQuerol(ByVal strFechaInicio As String, ByVal strFechaFin As String) As String
            Dim rptEjecutivoEuropaJuniorQuerol As String = "SELECT T99R114D610.[Date] As Fecha, T99R114R90R233.[Abreviation] AS Sigla, " & _
            "T99R114R90R233.[Name] AS Responsable, " & _
            "T99.[File] AS File1, " & _
            "T99R114R90.[Name] AS Cliente, " & _
            "T99R114.[Description] AS Descripcion, " & _
            "(CAST(YEAR(T99R114.[Blocked_Date]) AS VARCHAR)+'/'+ RIGHT(CAST(100+MONTH(T99R114.[Blocked_Date]) AS VARCHAR),2)+'/'+ " & _
            "RIGHT(CAST(100+DAY(T99R114.[Blocked_Date]) AS VARCHAR),2)) AS FechaConferencia, " & _
            "SUM((ROUND(((T99.SellPrev+ ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellPrevVat END,0))),2))) AS VentaPrevistaConIgv, " & _
            "SUM((ROUND(((T99.SellPrev- ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=0 THEN T99.SellPrevVat END,0))),2))) AS VentaPrevistaSinIgv, " & _
            "SUM((ROUND((SellReal +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0)),2))) AS VentaRealConIgv, " & _
            "SUM((ROUND((CASE WHEN  T99.Calc_FileType IN ('H','B') THEN T99.SellPrev +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 " & _
            "THEN T99.SellPrevVat END,0) ELSE 0 END),2))) AS ComisionPrevistaConIgv, " & _
            "SUM((ROUND((CASE WHEN  T99.Calc_FileType IN ('H','B') THEN T99.SellReal +" & _
            "ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) ELSE 0 END ),2))) AS ComisionRealConIgv, " & _
            "SUM((ROUND(((T99.BuyPrev+ ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyPrevVat END,0))),2))) AS CostoPrevistoConIgv, " & _
            "SUM((ROUND(((T99.BuyPrev- ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=0 THEN T99.BuyPrevVat END,0))),2))) AS CostoPrevistoSinIgv, " & _
            "0 AS CostoRealSinIgv, " & _
            "SUM((ROUND((T99.BuyReal+ ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyRealVat END,0)),2))) AS CostoRealConIgv, " & _
            "SUM((ROUND((CASE WHEN T99.entity_typeOK='F' AND (T99.BuyState<>'C' OR ISNULL(T99.TR,0)<>0) THEN " & _
            " (  (  ISNULL(T99.BuyPrev,0) + ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyPrevVat END,0)   )   +   " & _
            " (  ISNULL(T99.SellPrev,0)  + ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellPrevVat END,0) )    )  - " & _
            " (  ( ISNULL(T99.SellReal,0)  +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) )  +  " & _
            " (  ISNULL(T99.BuyReal,0)  + ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyRealVat END,0)  )    )*" & _
            " ISNULL(CASE WHEN T99.Moeda<>T99.MoedaPrev THEN ISNULL(T99.Exchange,1)/(CASE WHEN ISNULL(T99.ExchangePrev,1)=0 THEN 1 " & _
            " ELSE ISNULL(T99.ExchangePrev,1) END) END,1)  ELSE 0 END),2))) AS CostoPorFacturarConIgv, " & _
            " SUM((ROUND((( ISNULL(T99.SellPrev,0) +ISNULL(T99.BuyPrev,0)  + " & _
            " ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellPrevVat+T99.BuyPrevVat END,0) )*[dbo].[GetVatMarginal]('M') ),2))) AS LucroPrevistoConIgv, " & _
            " SUM((ROUND((( ISNULL(T99.SellReal,0) +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat+T99.BuyRealVat END,0)  +" & _
            " ISNULL(T99.BuyReal,0) ) ),2))) AS LucroRealConIgv, " & _
            " T99R114R92.[Descr] AS FileTipo1, " & _
            " T99R114R113.[Descr] AS FileTipo2, " & _
            " 0 AS VentaPorFacturarConIgv, " & _
            " 4 as IdArea " & _
            "FROM [Trek_DetailTicket] T99 WITH (NOLOCK)" & _
            " LEFT JOIN [Trek_File] T99R114 WITH (NOLOCK) ON T99R114.[Cod] = T99.[File]" & _
            " LEFT JOIN [Trek_File_InnerStatesHistory] T99R114D610 WITH (NOLOCK) ON T99R114D610.[File] = T99R114.[Cod]" & _
            " LEFT JOIN [FileType2] T99R114R113 WITH (NOLOCK) ON T99R114R113.[Cod] = T99R114.[File_Type2]" & _
            " LEFT JOIN [Entity] T99R114R90 WITH (NOLOCK) ON T99R114R90.[Entity] = T99R114.[Entity_Abreviation]" & _
            " LEFT JOIN [Users] T99R114R90R233 WITH (NOLOCK) ON T99R114R90R233.[Abreviation] = T99R114R90.[Contacto]" & _
            " LEFT JOIN [FileType] T99R114R92 WITH (NOLOCK) ON T99R114R92.[Cod] = T99R114.[File_Type]" & _
            " WHERE T99R114.[Deleted_Date] Is NULL" & _
            " AND T99R114.[Cod]>0 " & _
            " AND (ISNULL(T99R114.[File_Type],0)<>28 " & _
            " AND ISNULL(T99R114.[File_Type],0)<>31 " & _
            " AND ISNULL(T99R114.[File_Type],0)<>34 " & _
            " AND ISNULL(T99R114.[File_Type],0)<>43 " & _
            " AND ISNULL(T99R114.[File_Type],0)<>35 " & _
            " AND ISNULL(T99R114.[File_Type],0)<>38) " & _
            " AND T99R114.[File_Type2] =131 " & _
            " AND dbo.Entity_Properties_Text(T99R114R90.Entity,'',' * ') Like '%JUNIOR QUEROL: 1%' " & _
            " AND T99R114D610.[StateField] =1447 " & _
            " AND T99R114D610.[StateValue] IS NOT NULL " & _
            " AND convert(SMALLDATETIME,T99R114.[Blocked_Date]) BETWEEN convert(SMALLDATETIME, '" & strFechaInicio & "' ,103) AND convert(SMALLDATETIME, '" & strFechaFin & "',103)" & _
            " AND T99R114D610.[Date] = (select max(r.[Date]) from [Trek_File_InnerStatesHistory] r where r.[File] = T99.[File]) " & _
            " GROUP BY T99R114D610.[Date],T99R114R90R233.[Abreviation], " & _
            " T99R114R90R233.[Name], " & _
            " T99.[File], " & _
            " T99R114R90.[Name], " & _
            " T99R114.[Description], " & _
            " (CAST(YEAR(T99R114.[Blocked_Date]) AS VARCHAR)+'/'+ " & _
            " RIGHT(CAST(100+MONTH(T99R114.[Blocked_Date]) AS VARCHAR),2)+'/'+ " & _
            " RIGHT(CAST(100+DAY(T99R114.[Blocked_Date]) AS VARCHAR),2)), " & _
            " T99R114R92.[Descr]," & _
            " T99R114R113.[Descr]" & _
            " HAVING   (SUM((T99.SellReal +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) ) *" & _
            " ISNULL(T99.Exchange,1))>0 OR SUM((T99.SellReal +" & _
            " ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) ) *ISNULL(T99.Exchange,1))<0)"
            Return rptEjecutivoEuropaJuniorQuerol

            '"AND DATEPART(YEAR,T99R114D610.[Date]) = " & strAnio & " AND DATEPART(MM,T99R114D610.[Date]) = " & strMes & " " & _
        End Function

        Public Function ObtenerReporteEjecutivoRegionalDM(ByVal strFechaInicio As String, ByVal strFechaFin As String) As DataTable
            Dim strSelect = ObtenerQueryEjecutivoRegional(strFechaInicio, strFechaFin)
            Dim oDt As New DataTable
            Try
                oDt = ObtenerDataTable(strSelect)
            Catch ex As Exception
                EscribirLogException(ex, "ObtenerQueryEjecutivoRegional", strSelect, "ReporteMetasDM")
            End Try
            Return oDt
        End Function

        Public Function ObtenerQueryEjecutivoRegional(ByVal strFechaInicio As String, ByVal strFechaFin As String) As String
            Dim rptEjecutivoRegional As String = "SELECT T99R114D610.[Date] As Fecha, T99R114R90R233.[Abreviation] AS Sigla, " & _
            "T99R114R90R233.[Name] AS Responsable, " & _
            "T99.[File] AS File1, " & _
            "T99R114R90.[Name] AS Cliente, " & _
            "T99R114.[Description] AS Descripcion, " & _
            "(CAST(YEAR(T99R114.[Blocked_Date]) AS VARCHAR)+'/'+ RIGHT(CAST(100+MONTH(T99R114.[Blocked_Date]) AS VARCHAR),2)+'/'+ " & _
            "RIGHT(CAST(100+DAY(T99R114.[Blocked_Date]) AS VARCHAR),2)) AS FechaConferencia, " & _
            "SUM((ROUND(((T99.SellPrev+ ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellPrevVat END,0))),2))) AS VentaPrevistaConIgv, " & _
            "SUM((ROUND(((T99.SellPrev- ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=0 THEN T99.SellPrevVat END,0))),2))) AS VentaPrevistaSinIgv, " & _
            "SUM((ROUND((SellReal +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0)),2))) AS VentaRealConIgv, " & _
            "SUM((ROUND((CASE WHEN  T99.Calc_FileType IN ('H','B') THEN T99.SellPrev +" & _
            "ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellPrevVat END,0) ELSE 0 END),2))) AS ComisionPrevistaConIgv, " & _
            "SUM((ROUND((CASE WHEN  T99.Calc_FileType IN ('H','B') THEN T99.SellReal +" & _
            "ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) ELSE 0 END ),2))) AS ComisionRealConIgv, " & _
            "SUM((ROUND(((T99.BuyPrev+ ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyPrevVat END,0))),2))) AS CostoPrevistoConIgv, " & _
            "SUM((ROUND(((T99.BuyPrev- ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=0 THEN T99.BuyPrevVat END,0))),2))) AS CostoPrevistoSinIgv, " & _
            "0 AS CostoRealSinIgv, " & _
            "SUM((ROUND((T99.BuyReal+ ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyRealVat END,0)),2))) AS CostoRealConIgv, " & _
            "SUM((ROUND((CASE WHEN T99.entity_typeOK='F' AND (T99.BuyState<>'C' OR ISNULL(T99.TR,0)<>0) THEN " & _
            "((ISNULL(T99.BuyPrev,0) + ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyPrevVat END,0)   )   +   " & _
            "(ISNULL(T99.SellPrev,0)  + ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellPrevVat END,0) )    )  -  " & _
            "((ISNULL(T99.SellReal,0)  +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) )  +   " & _
            "(ISNULL(T99.BuyReal,0)  + ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.BuyRealVat END,0)  )    )*" & _
            "ISNULL(CASE WHEN T99.Moeda<>T99.MoedaPrev THEN ISNULL(T99.Exchange,1)/(CASE WHEN ISNULL(T99.ExchangePrev,1)=0 THEN 1 " & _
            "ELSE ISNULL(T99.ExchangePrev,1) END) END,1)  ELSE 0 END),2))) AS CostoPorFacturarConIgv, " & _
            "SUM((ROUND((( ISNULL(T99.SellPrev,0) +ISNULL(T99.BuyPrev,0)  +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN " & _
            "T99.SellPrevVat+T99.BuyPrevVat END,0) )*[dbo].[GetVatMarginal]('M') ),2))) AS LucroPrevistoConIgv, " & _
            "SUM((ROUND((( ISNULL(T99.SellReal,0) +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat+T99.BuyRealVat END,0)  +" & _
            "ISNULL(T99.BuyReal,0) ) ),2))) AS LucroRealConIgv, " & _
            "T99R114R92.[Descr] AS FileTipo1, " & _
            "T99R114R113.[Descr] AS FileTipo2, " & _
            "0 AS VentaPorFacturarConIgv, " & _
            "13 as IdArea " & _
            "FROM [Trek_DetailTicket] T99 WITH (NOLOCK) " & _
            "LEFT JOIN [Trek_File] T99R114 WITH (NOLOCK) ON T99R114.[Cod] = T99.[File] " & _
            "LEFT JOIN [Trek_File_InnerStatesHistory] T99R114D610 WITH (NOLOCK) ON T99R114D610.[File] = T99R114.[Cod] " & _
            "LEFT JOIN [FileType2] T99R114R113 WITH (NOLOCK) ON T99R114R113.[Cod] = T99R114.[File_Type2] " & _
            "LEFT JOIN [Entity] T99R114R90 WITH (NOLOCK) ON T99R114R90.[Entity] = T99R114.[Entity_Abreviation] " & _
            "LEFT JOIN [Users] T99R114R90R233 WITH (NOLOCK) ON T99R114R90R233.[Abreviation] = T99R114R90.[Contacto] " & _
            "LEFT JOIN [Geographic_Area] T99R114R90R282 WITH (NOLOCK) ON T99R114R90R282.[Cod] = T99R114R90.[GeoArea] " & _
            "LEFT JOIN [Geographic_Area] T99R114R90R282R706 WITH (NOLOCK) ON T99R114R90R282R706.[Cod_WBS] = LEFT(T99R114R90R282.Cod_WBS,12) " & _
            "LEFT JOIN [FileType] T99R114R92 WITH (NOLOCK) ON T99R114R92.[Cod] = T99R114.[File_Type] " & _
            "WHERE T99R114.[Deleted_Date] Is NULL And T99R114.[Cod] > 0 " & _
            " AND (ISNULL(T99R114R90R282R706.[Name],'')<>'Lima' " & _
            " AND ISNULL(T99R114R90R282R706.[Name],'')<>'Callao') " & _
            " AND ISNULL(T99R114.[File_Type],0)<>43 " & _
            " AND T99R114D610.[StateField] =1447 " & _
            " AND T99R114D610.[StateValue] IS NOT NULL " & _
            " AND convert(SMALLDATETIME,T99R114.[Blocked_Date]) BETWEEN convert(SMALLDATETIME, '" & strFechaInicio & "' ,103) AND convert(SMALLDATETIME, '" & strFechaFin & "',103)" & _
            " AND T99R114D610.[Date] = (select max(r.[Date]) from [Trek_File_InnerStatesHistory] r where r.[File] = T99.[File]) " & _
            " GROUP BY T99R114D610.[Date],T99R114R90R233.[Abreviation], " & _
            " T99R114R90R233.[Name], " & _
            " T99.[File], " & _
            " T99R114R90.[Name], " & _
            " T99R114.[Description], " & _
            " (CAST(YEAR(T99R114.[Blocked_Date]) AS VARCHAR)+'/'+ RIGHT(CAST(100+MONTH(T99R114.[Blocked_Date]) AS VARCHAR),2)+'/'+ " & _
            " RIGHT(CAST(100+DAY(T99R114.[Blocked_Date]) AS VARCHAR),2)), " & _
            " T99R114R92.[Descr]," & _
            " T99R114R113.[Descr]," & _
            " T99R114R90R282R706.[Name]" & _
            " HAVING (SUM((T99.SellReal +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) ) " & _
            " *ISNULL(T99.Exchange,1))>0 OR SUM((T99.SellReal +ISNULL(CASE WHEN (T99.VatNotIncluded & 1)=1 THEN T99.SellRealVat END,0) ) " & _
            " *ISNULL(T99.Exchange,1))<0)"

            '"AND DATEPART(YEAR,T99R114D610.[Date]) = " & strAnio & " AND DATEPART(MM,T99R114D610.[Date]) = " & strMes & " " & _
            Return rptEjecutivoRegional

            '"T99R114R90R282R706.[Name] AS Designation" & _
        End Function

        Public Function InsertarReporteGerencialMetasDM(ByVal strXML As String, _
                                                        ByVal strCodigoSeguimiento As String, _
                                                        ByVal intFirmaDB As Integer) As Boolean
            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean
            Dim strtabla As String = "WEB_DM_REPORTE_VENDEDOR"
            Try
                strXML = strXML.Replace("&", "Y")

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.GDS_MC_INS_VENTA, Constantes.StoredProcedure) 'CARGA LA TABLA MEDIANTE XML
                ConnNM.AgregarParametro("@p_Tabla", strtabla, OracleDbType.Varchar2, strtabla.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Xml", strXML.ToString(), OracleDbType.Clob, 0, ParameterDirection.Input)
                bolResultado = ConnNM._InsertExecuteNonQuery()


            Catch ex As Exception
                bolResultado = False
                strLog = "Stored Procedure : " & Constantes.GDS_MC_INS_VENTA & vbCrLf
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
                ConnNM = Nothing
            End Try
            Return bolResultado
        End Function

        Public Function ObtenerVendedorDestinosMundiales(ByVal strCodigoSeguimiento As String, _
                                                         ByVal intFirmaDB As Integer) As List(Of classDatosAgente)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objDatosAgente As List(Of classDatosAgente) = Nothing
            Dim auxDatosAgente As classDatosAgente = Nothing
            Try

                ConnNM.Connect(intFirmaDB)

                ConnNM.SP_Command(Constantes.spGDS_DATOS_VENDEDORMETAS_DM, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)

                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    'oDataRow = New DataRow
                    auxDatosAgente = New classDatosAgente
                    auxDatosAgente.IdArea = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_AREA", -1)
                    auxDatosAgente.IdVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_VENDEDOR", Nothing)
                    auxDatosAgente.NombreVendedor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "USUARIO", Nothing)
                    auxDatosAgente.NombreArea = ConnNM.LeeColumnasDataReader(objOracleDataReader, "AREA", Nothing)

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
                ConnNM = Nothing
                auxDatosAgente = Nothing
            End Try

            Return objDatosAgente
        End Function

        Public Function InsertarCorrelativoReporte(ByVal strFechaInicio As String, ByVal strFechaFin As String, ByVal intTipoCarga As Integer, ByVal strCodigoSeguimiento As String, ByVal intFirmaDB As Integer) As Integer
            Dim ConnNM As New MyConnectionOracle
            Dim intResultado As Integer = 0
            Try
                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spGDS_INS_UPDATE_REP, Constantes.StoredProcedure, True)

                If Not String.IsNullOrEmpty(strFechaInicio) Then
                    Dim FechaInicio As New Date
                    FechaInicio = CDate(strFechaInicio).ToString("dd-MM-yyyy")
                    ConnNM.AgregarParametro("datFecIniRep", FechaInicio, OracleDbType.Date, 0, ParameterDirection.Input)
                    FechaInicio = Nothing
                Else
                    ConnNM.AgregarParametro("datFecIniRep", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                If Not String.IsNullOrEmpty(strFechaFin) Then
                    Dim FechaFin As New Date
                    FechaFin = CDate(strFechaFin).ToString("dd-MM-yyyy")
                    ConnNM.AgregarParametro("datFecFinRep", FechaFin, OracleDbType.Date, 0, ParameterDirection.Input)
                    FechaFin = Nothing
                Else
                    ConnNM.AgregarParametro("datFecFinRep", Nothing, OracleDbType.NVarchar2, 0, ParameterDirection.Input)
                End If

                ConnNM.AgregarParametro("intIdRep", 16, OracleDbType.Int32, 0, ParameterDirection.Input)

                If Not String.IsNullOrEmpty(intTipoCarga) Then
                    ConnNM.AgregarParametro("pIntTipoCarga_in", intTipoCarga, OracleDbType.Int32, 0, ParameterDirection.Input)
                Else
                    ConnNM.AgregarParametro("pIntTipoCarga_in", Nothing, OracleDbType.Int32, 0, ParameterDirection.Input)
                End If
                ConnNM.AgregarParametro("numIdUpd_out", 0, OracleDbType.Int32, 0, ParameterDirection.Output)

                ConnNM._ExecuteReader()
                intResultado = ConnNM.LeeParametros("numIdUpd_out", 0)

            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spGDS_INS_UPDATE_REP & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ActualizaIdReporte" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "Actualizar Id Reporte", strCodigoSeguimiento)

                Err.Raise(14, "Actualizar Id Reporte", ex.ToString)
            End Try

            Return intResultado
        End Function

        Public Sub WriteLog(ByVal Mensaje As String, _
                    ByVal strNombreArchivo As String, _
                    ByVal strNombreCarpeta As String)

            Dim sFecha As String = Nothing
            Dim sHora As String = Nothing
            Dim strCarpera As String = Nothing
            Dim oRuta As String = "C:\inetpub\wwwroot\GNM_Integracion\GDS_MuevoMundoLog\archivosTXT\Metas\"
            Dim oStreamWriter As StreamWriter
            Dim Linea As String = Nothing

            Try

                sFecha = Format(Now, Constantes.IWS_DATE_FORMAT_FILE)
                sHora = Format(Now, Constantes.IWS_TIME_FORMAT_FILE)

                strCarpera = oRuta & sFecha & "\" & strNombreCarpeta.Split("#")(0)

                If System.IO.Directory.Exists(strCarpera) = False Then
                    System.IO.Directory.CreateDirectory(strCarpera)
                End If

                If strNombreCarpeta.Split("#").Length = 1 Then
                    oStreamWriter = New StreamWriter(strCarpera & "\" & sHora & "_" & strNombreCarpeta.Split("#")(0) & "_" & strNombreArchivo & ".txt")
                Else
                    oStreamWriter = New StreamWriter(strCarpera & "\" & sHora & "_" & strNombreCarpeta.Split("#")(0) & "_" & strNombreArchivo & "_" & strNombreCarpeta.Split("#")(1) & ".txt")
                End If

                Linea = "[" & sHora & "]  " & Mensaje
                oStreamWriter.WriteLine(Linea)
                oStreamWriter.Close()

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                Mensaje = Nothing
                strNombreArchivo = Nothing
                strNombreCarpeta = Nothing

                sFecha = Nothing
                sHora = Nothing
                strCarpera = Nothing
                oRuta = Nothing
                oStreamWriter = Nothing
                Linea = Nothing
            End Try

        End Sub



    End Class
End Namespace

