Imports GDS_NuevoMundoPersistencia
Imports GDS_NM_Mensajeria
Imports classDO = GDS_NuevoMundoAccesoDatos
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports BO = GDS_NuevoMundoDominio.ObjetoDominioNegocio.classBO
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Imports System.Text
Imports System.Globalization
Imports System.Text.RegularExpressions

Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function ObtenerEInsertarReporteGerencialDM(ByVal strFechaInicio As String, ByVal strFechaFin As String, ByVal intTipoCarga As Integer, ByVal strCodigoSeguimiento As String, ByVal intFirmaDB As Integer) As Boolean
            Dim oDtReporteGerencial As New DataTable
            Dim strXML As StringBuilder
            Dim bolRespuesta As Boolean
            Try
                Dim objDAO As New DAO
                Dim lstDatosVendedor As New List(Of classDatosAgente)
                oDtReporteGerencial = ObtenerReporteEjecutivosVentasDM(strFechaInicio, strFechaFin)

                For Each oDataRow As DataRow In ObtenerReporteCounterEuropaDM(strFechaInicio, strFechaFin).Rows
                    oDtReporteGerencial.ImportRow(oDataRow)
                Next

                For Each oDataRow2 As DataRow In ObtenerReporteCounterGeneralDM(strFechaInicio, strFechaFin).Rows
                    oDtReporteGerencial.ImportRow(oDataRow2)
                Next

                For Each oDataRow3 As DataRow In ObtenerReporteCrucerosDM(strFechaInicio, strFechaFin).Rows
                    oDtReporteGerencial.ImportRow(oDataRow3)
                Next

                For Each oDataRow4 As DataRow In ObtenerReporteEjecutivoEuropaJuniorQuerolDM(strFechaInicio, strFechaFin).Rows
                    oDtReporteGerencial.ImportRow(oDataRow4)
                Next

                For Each oDataRow5 As DataRow In ObtenerReporteEjecutivoRegionalDM(strFechaInicio, strFechaFin).Rows
                    oDtReporteGerencial.ImportRow(oDataRow5)
                Next

                lstDatosVendedor = ObtenerVendedorDestinosMundiales(strCodigoSeguimiento, intFirmaDB)
                Dim intIdUPD As Integer = objDAO.InsertarCorrelativoReporte(strFechaInicio, strFechaFin, intTipoCarga, strCodigoSeguimiento, intFirmaDB)
                strXML = New StringBuilder
                Dim intContador As Integer = 1
                strXML.Append("<?xml version='1.0'?>")
                strXML.Append("<ROWSET>")
                For i As Integer = 0 To oDtReporteGerencial.Rows.Count - 1
                    Dim strCodigoVendedor As String = String.Empty
                    For Each oVendedor As classDatosAgente In lstDatosVendedor
                        If oVendedor.NombreVendedor.Trim().ToString().Equals(oDtReporteGerencial.Rows(i)("Responsable").ToString().ToUpper.Replace("Ñ", "N")) Then
                            strCodigoVendedor = oVendedor.IdVendedor
                            Exit For
                        End If
                    Next
                    If Not String.IsNullOrEmpty(strCodigoVendedor) Then
                        strXML.Append("<ROW NUM='" + intContador.ToString() + "'>")
                        strXML.Append("<UPD_ID>" + intIdUPD.ToString() + "</UPD_ID>" & Environment.NewLine)
                        Dim FechaReporte As String = ""
                        Dim FechaConferencia As String = ""
                        If String.IsNullOrEmpty(oDtReporteGerencial.Rows(i)("FECHA").ToString()) Then
                            FechaReporte = "01/01/1900 00:00:00"
                        Else
                            FechaReporte = String.Format("{0:dd/MM/yyyy HH:mm:ss}", Convert.ToDateTime(oDtReporteGerencial.Rows(i)("FECHA").ToString())) 'Convert.ToDateTime(oDtReporteGerencial.Rows(i)("FECHA")).ToString().Substring(0, 10)
                            'FechaReporte = String.Format("{0:dd/MM/yyyy}", FechaReporte)
                            'IIf(String.IsNullOrEmpty(oDtReporteGerencial.Rows(i)("FECHA").ToString()), "01/01/1900", String.Format("{0:dd/MM/yyyy}", DateTime.Parse(oDtReporteGerencial.Rows(i)("FECHA").ToString(), CultureInfo.InvariantCulture)))
                        End If
                        strXML.Append("<FECHA_REPORTE>" + FechaReporte + "</FECHA_REPORTE>" & Environment.NewLine)
                        strXML.Append("<SIGLA>" + QuitarCaracteresEspeciales(oDtReporteGerencial.Rows(i)("SIGLA")) + "</SIGLA>" & Environment.NewLine)
                        strXML.Append("<ID_VENDEDOR>" + strCodigoVendedor + "</ID_VENDEDOR>" & Environment.NewLine) 'ODTREPORTEGERENCIAL.ROWS(I)("RESPONSABLE")
                        strXML.Append("<ID_AREA>" + oDtReporteGerencial.Rows(i)("IDAREA").ToString() + "</ID_AREA>" & Environment.NewLine)
                        strXML.Append("<ID_FILE>" + oDtReporteGerencial.Rows(i)("FILE1").ToString() + "</ID_FILE>" & Environment.NewLine)
                        strXML.Append("<ID_MONEDA>" + "USD" + "</ID_MONEDA>" & Environment.NewLine)
                        strXML.Append("<CLIENTE>" + QuitarCaracteresEspeciales(oDtReporteGerencial.Rows(i)("CLIENTE")) + "</CLIENTE>" & Environment.NewLine)
                        strXML.Append("<DESCRIPCION>" + QuitarCaracteresEspeciales(oDtReporteGerencial.Rows(i)("DESCRIPCION")) + "</DESCRIPCION>" & Environment.NewLine)
                        'Dim FechaConferencia As String = String.Format("{0:dd/MM/yyyy}", DateTime.Parse(oDtReporteGerencial.Rows(i)("FECHACONFERENCIA").ToString(), CultureInfo.InvariantCulture))
                        If String.IsNullOrEmpty(oDtReporteGerencial.Rows(i)("FECHACONFERENCIA").ToString()) Then
                            FechaConferencia = "01/01/1900"
                        Else
                            FechaConferencia = String.Format("{0:dd/MM/yyyy}", Convert.ToDateTime(oDtReporteGerencial.Rows(i)("FECHACONFERENCIA").ToString()))

                            'FechaConferencia = String.Format("{0:dd/MM/yyyy HH:mm:ss}", Convert.ToDateTime(oDtReporteGerencial.Rows(i)("FECHACONFERENCIA").ToString()))

                            'FechaReporte = String.Format("{0:dd/MM/yyyy}", oDtReporteGerencial.Rows(i)("FECHACONFERENCIA"))
                            'IIf(String.IsNullOrEmpty(oDtReporteGerencial.Rows(i)("FECHA").ToString()), "01/01/1900", String.Format("{0:dd/MM/yyyy}", DateTime.Parse(oDtReporteGerencial.Rows(i)("FECHA").ToString(), CultureInfo.InvariantCulture)))
                        End If

                        strXML.Append("<FECHA_CONFERENCIA>" + FechaConferencia + "</FECHA_CONFERENCIA>" & Environment.NewLine)

                        strXML.Append("<VENTA_PREVISTA_CON_IGV>" + Decimal.Round(oDtReporteGerencial.Rows(i)("VENTAPREVISTACONIGV"), 2).ToString().Replace(".", ",") + "</VENTA_PREVISTA_CON_IGV>" & Environment.NewLine)
                        strXML.Append("<VENTA_PREVISTA_SIN_IGV>" + Decimal.Round(oDtReporteGerencial.Rows(i)("VENTAPREVISTASINIGV"), 2).ToString().Replace(".", ",") + "</VENTA_PREVISTA_SIN_IGV>" & Environment.NewLine)
                        strXML.Append("<VENTA_REAL_CON_IGV>" + Decimal.Round(oDtReporteGerencial.Rows(i)("VENTAREALCONIGV"), 2).ToString().Replace(".", ",") + "</VENTA_REAL_CON_IGV>" & Environment.NewLine)
                        strXML.Append("<COMISION_PREVISTA_CON_IGV>" + Decimal.Round(oDtReporteGerencial.Rows(i)("COMISIONPREVISTACONIGV"), 2).ToString().Replace(".", ",") + "</COMISION_PREVISTA_CON_IGV>" & Environment.NewLine)
                        strXML.Append("<COMISION_REAL_CON_IGV>" + Decimal.Round(oDtReporteGerencial.Rows(i)("COMISIONREALCONIGV"), 2).ToString().Replace(".", ",") + "</COMISION_REAL_CON_IGV>" & Environment.NewLine)
                        strXML.Append("<COSTO_PREVISTO_CON_IGV>" + Decimal.Round(oDtReporteGerencial.Rows(i)("COSTOPREVISTOCONIGV"), 2).ToString().Replace(".", ",") + "</COSTO_PREVISTO_CON_IGV>" & Environment.NewLine)
                        strXML.Append("<COSTO_PREVISTO_SIN_IGV>" + Decimal.Round(oDtReporteGerencial.Rows(i)("COSTOPREVISTOSINIGV"), 2).ToString().Replace(".", ",") + "</COSTO_PREVISTO_SIN_IGV>" & Environment.NewLine)
                        strXML.Append("<COSTO_REAL_SIN_IGV>" + Decimal.Round(oDtReporteGerencial.Rows(i)("COSTOREALSINIGV"), 2).ToString().Replace(".", ",") + "</COSTO_REAL_SIN_IGV>" & Environment.NewLine)
                        strXML.Append("<COSTO_REAL_CON_IGV>" + Decimal.Round(oDtReporteGerencial.Rows(i)("COSTOREALCONIGV"), 2).ToString().Replace(".", ",") + "</COSTO_REAL_CON_IGV>" & Environment.NewLine)
                        strXML.Append("<COSTO_POR_FACTURAR_CON_IGV>" + Decimal.Round(oDtReporteGerencial.Rows(i)("COSTOPORFACTURARCONIGV"), 2).ToString().Replace(".", ",") + "</COSTO_POR_FACTURAR_CON_IGV>" & Environment.NewLine)
                        strXML.Append("<LUCRO_PREVISTO_CON_IGV>" + Decimal.Round(oDtReporteGerencial.Rows(i)("LUCROPREVISTOCONIGV"), 2).ToString().Replace(".", ",") + "</LUCRO_PREVISTO_CON_IGV>" & Environment.NewLine)
                        strXML.Append("<LUCRO_REAL_CON_IGV>" + Decimal.Round(oDtReporteGerencial.Rows(i)("LUCROREALCONIGV"), 2).ToString().Replace(".", ",") + "</LUCRO_REAL_CON_IGV>" & Environment.NewLine)
                        strXML.Append("<FILE_TIPO_1>" + QuitarCaracteresEspeciales(oDtReporteGerencial.Rows(i)("FILETIPO1")) + "</FILE_TIPO_1>" & Environment.NewLine)
                        strXML.Append("<FILE_TIPO_2>" + QuitarCaracteresEspeciales(oDtReporteGerencial.Rows(i)("FILETIPO2")) + "</FILE_TIPO_2>" & Environment.NewLine)
                        strXML.Append("<VENTA_POR_FACTURAR_CON_IGV>" + oDtReporteGerencial.Rows(i)("VENTAPORFACTURARCONIGV").ToString() + "</VENTA_POR_FACTURAR_CON_IGV>" & Environment.NewLine)


                        'strXML.Append("<ROW num='" + intContador.ToString() + "'>")
                        'strXML.Append("<upd_id>" + intIdUPD.ToString() + "</upd_id>")
                        'strXML.Append("<fecha_reporte>" + oDtReporteGerencial.Rows(i)("Fecha") + "</fecha_reporte>")
                        'strXML.Append("<sigla>" + oDtReporteGerencial.Rows(i)("Sigla") + "</sigla>")
                        'strXML.Append("<id_vendedor>" + strCodigoVendedor + "</id_vendedor>") 'oDtReporteGerencial.Rows(i)("Responsable")
                        'strXML.Append("<id_area>" + oDtReporteGerencial.Rows(i)("IdArea").ToString() + "</id_area>")
                        'strXML.Append("<id_file>" + oDtReporteGerencial.Rows(i)("File1").ToString() + "</id_file>")
                        'strXML.Append("<id_moneda>" + "USD" + "</id_moneda>")
                        'strXML.Append("<cliente>" + oDtReporteGerencial.Rows(i)("Cliente") + "</cliente>")
                        'strXML.Append("<descripcion>" + oDtReporteGerencial.Rows(i)("Descripcion") + "</descripcion>")
                        'strXML.Append("<fecha_conferencia>" + IIf(String.IsNullOrEmpty(oDtReporteGerencial.Rows(i)("FechaConferencia").ToString()), "01/01/1900", oDtReporteGerencial.Rows(i)("FechaConferencia").ToString()) + "</fecha_conferencia>")
                        'strXML.Append("<venta_prevista_con_igv>" + oDtReporteGerencial.Rows(i)("VentaPrevistaConIgv").ToString().Replace(".", ",") + "</venta_prevista_con_igv>")
                        'strXML.Append("<venta_prevista_sin_igv>" + oDtReporteGerencial.Rows(i)("VentaPrevistaSinIgv").ToString().Replace(".", ",") + "</venta_prevista_sin_igv>")
                        'strXML.Append("<venta_real_con_igv>" + oDtReporteGerencial.Rows(i)("VentaRealConIgv").ToString().Replace(".", ",") + "</venta_real_con_igv>")
                        'strXML.Append("<comision_prevista_con_igv>" + oDtReporteGerencial.Rows(i)("ComisionPrevistaConIgv").ToString().Replace(".", ",") + "</comision_prevista_con_igv>")
                        'strXML.Append("<comision_real_con_igv>" + oDtReporteGerencial.Rows(i)("ComisionRealConIgv").ToString().Replace(".", ",") + "</comision_real_con_igv>")
                        'strXML.Append("<costo_previsto_con_igv>" + oDtReporteGerencial.Rows(i)("CostoPrevistoConIgv").ToString().Replace(".", ",") + "</costo_previsto_con_igv>")
                        'strXML.Append("<costo_previsto_sin_igv>" + oDtReporteGerencial.Rows(i)("CostoPrevistoSinIgv").ToString().Replace(".", ",") + "</costo_previsto_sin_igv>")
                        'strXML.Append("<costo_real_sin_igv>" + oDtReporteGerencial.Rows(i)("CostoRealSinIgv").ToString().Replace(".", ",") + "</costo_real_sin_igv>")
                        'strXML.Append("<costo_real_con_igv>" + oDtReporteGerencial.Rows(i)("CostoRealConIgv").ToString().Replace(".", ",") + "</costo_real_con_igv>")
                        'strXML.Append("<costo_por_facturar_con_igv>" + oDtReporteGerencial.Rows(i)("CostoPorFacturarConIgv").ToString()..Replace(".",",") + "</costo_por_facturar_con_igv>")
                        'strXML.Append("<lucro_previsto_con_igv>" + oDtReporteGerencial.Rows(i)("LucroPrevistoConIgv").ToString().Replace(".", ",") + "</lucro_previsto_con_igv>")
                        'strXML.Append("<lucro_real_con_igv>" + oDtReporteGerencial.Rows(i)("LucroRealConIgv").ToString().Replace(".", ",") + "</lucro_real_con_igv>")
                        'strXML.Append("<file_tipo_1>" + oDtReporteGerencial.Rows(i)("FileTipo1") + "</file_tipo_1>")
                        'strXML.Append("<file_tipo_2>" + oDtReporteGerencial.Rows(i)("FileTipo2") + "</file_tipo_2>")
                        'strXML.Append("<venta_por_facturar_con_igv>" + oDtReporteGerencial.Rows(i)("VentaPorFacturarConIgv").ToString() + "</venta_por_facturar_con_igv>")

                        strXML.Append("</ROW>" & Environment.NewLine)
                        intContador += 1
                    End If
                Next
                strXML.Append("</ROWSET>" & Environment.NewLine)

                'strXML.Append("<?xml version='1.0'?>")
                'strXML.Append("<ROWSET>")
                'strXML.Append("<ROW num='" + intContador.ToString() + "'>")
                'strXML.Append("<UPD_ID>" + "2" + "</UPD_ID>")
                'strXML.Append("<FECHA_REPORTE>03/01/2017 18:03:04</FECHA_REPORTE>")
                'strXML.Append("<SIGLA>" + "A" + "</SIGLA>")
                'strXML.Append("<ID_VENDEDOR>" + "15" + "</ID_VENDEDOR>") 'ODTREPORTEGERENCIAL.ROWS(I)("RESPONSABLE")
                'strXML.Append("<ID_AREA>" + "22" + "</ID_AREA>")
                'strXML.Append("<ID_FILE>" + "12543" + "</ID_FILE>")
                'strXML.Append("<ID_MONEDA>" + "USD" + "</ID_MONEDA>")
                'strXML.Append("<CLIENTE>" + "EMPRESA AO SAC" + "</CLIENTE>")
                'strXML.Append("<DESCRIPCION>" + "DESARROLLO DE XML" + "</DESCRIPCION>")
                'strXML.Append("<FECHA_CONFERENCIA>03/01/2017 18:03:04</FECHA_CONFERENCIA>")
                'strXML.Append("<VENTA_PREVISTA_CON_IGV>" + "686,00" + "</VENTA_PREVISTA_CON_IGV>")
                'strXML.Append("<VENTA_PREVISTA_SIN_IGV>" + "0,00" + "</VENTA_PREVISTA_SIN_IGV>")
                'strXML.Append("<VENTA_REAL_CON_IGV>" + "686,00" + "</VENTA_REAL_CON_IGV>")
                'strXML.Append("<COMISION_PREVISTA_CON_IGV>" + "-73,00" + "</COMISION_PREVISTA_CON_IGV>")
                'strXML.Append("<COMISION_REAL_CON_IGV>" + "-76,00" + "</COMISION_REAL_CON_IGV>")
                'strXML.Append("<COSTO_PREVISTO_CON_IGV>" + "541,90" + "</COSTO_PREVISTO_CON_IGV>")
                'strXML.Append("<COSTO_PREVISTO_SIN_IGV>" + "0,00" + "</COSTO_PREVISTO_SIN_IGV>")
                'strXML.Append("<COSTO_REAL_SIN_IGV>" + "0,00" + "</COSTO_REAL_SIN_IGV>")
                'strXML.Append("<COSTO_REAL_CON_IGV>" + "541,89" + "</COSTO_REAL_CON_IGV>")
                'strXML.Append("<COSTO_POR_FACTURAR_CON_IGV>" + "0" + "</COSTO_POR_FACTURAR_CON_IGV>")
                'strXML.Append("<LUCRO_PREVISTO_CON_IGV>" + "145,50" + "</LUCRO_PREVISTO_CON_IGV>")
                'strXML.Append("<LUCRO_REAL_CON_IGV>" + "145,50" + "</LUCRO_REAL_CON_IGV>")
                'strXML.Append("<FILE_TIPO_1>" + "ENLATADOS" + "</FILE_TIPO_1>")
                'strXML.Append("<FILE_TIPO_2>" + "CARIBE" + "</FILE_TIPO_2>")
                'strXML.Append("<VENTA_POR_FACTURAR_CON_IGV>" + "0" + "</VENTA_POR_FACTURAR_CON_IGV>")
                'strXML.Append("</ROW>")

                'strXML.Append("<ROW NUM='2'>")
                'strXML.Append("<UPD_ID>2</UPD_ID>")
                'strXML.Append("<FECHA_REPORTE>17/12/2016 00:00:00</FECHA_REPORTE>")
                'strXML.Append("<SIGLA>JLARA</SIGLA>")
                'strXML.Append("<ID_VENDEDOR>26</ID_VENDEDOR>")
                'strXML.Append("<ID_AREA>22</ID_AREA>")
                'strXML.Append("<ID_FILE>12163</ID_FILE>")
                'strXML.Append("<ID_MONEDA>USD</ID_MONEDA>")
                'strXML.Append("<CLIENTE>HIT TRAVEL SERVICE S.A.C.</CLIENTE>")
                'strXML.Append("<DESCRIPCION>MARIA MILAGROS/OLIVERA CASTILLO  X 2 - CCB 43982 - FILE ASOCIADO CON  12854</DESCRIPCION>")
                'strXML.Append("<FECHA_CONFERENCIA>01/01/1900 00:00:00</FECHA_CONFERENCIA>")
                'strXML.Append("<VENTA_PREVISTA_CON_IGV>171,38</VENTA_PREVISTA_CON_IGV>")
                'strXML.Append("<VENTA_PREVISTA_SIN_IGV>0</VENTA_PREVISTA_SIN_IGV>")
                'strXML.Append("<VENTA_REAL_CON_IGV>149,58</VENTA_REAL_CON_IGV>")
                'strXML.Append("<COMISION_PREVISTA_CON_IGV>-16,62</COMISION_PREVISTA_CON_IGV>")
                'strXML.Append("<COMISION_REAL_CON_IGV>-16,62</COMISION_REAL_CON_IGV>")
                'strXML.Append("<COSTO_PREVISTO_CON_IGV>-156,80</COSTO_PREVISTO_CON_IGV>")
                'strXML.Append("<COSTO_PREVISTO_SIN_IGV>0</COSTO_PREVISTO_SIN_IGV>")
                'strXML.Append("<COSTO_REAL_SIN_IGV>0</COSTO_REAL_SIN_IGV>")
                'strXML.Append("<COSTO_REAL_CON_IGV>-146,80</COSTO_REAL_CON_IGV>")
                'strXML.Append("<COSTO_POR_FACTURAR_CON_IGV>-10,00</COSTO_POR_FACTURAR_CON_IGV>")
                'strXML.Append("<LUCRO_PREVISTO_CON_IGV>14,58</LUCRO_PREVISTO_CON_IGV>")
                'strXML.Append("<LUCRO_REAL_CON_IGV>2,78</LUCRO_REAL_CON_IGV>")
                'strXML.Append("<FILE_TIPO_1>Bloqueo</FILE_TIPO_1>")
                'strXML.Append("<FILE_TIPO_2>Nacionales Sur</FILE_TIPO_2>")
                'strXML.Append("<VENTA_POR_FACTURAR_CON_IGV>0</VENTA_POR_FACTURAR_CON_IGV>")
                'strXML.Append("</ROW>")
                'strXML.Append("</ROWSET>")
                objDAO.InsertaXMLaTabla("PTADESTINOS.WEB_DM_REPORTE_VENDEDOR", strXML.ToString(), "kcuba", 1, 0, False, Nothing, True)
                'objDAO.InsertaXMLaTabla("PTADESTINOS.WEB_DM_REPORTE_VENDEDOR", strXML.ToString(), "kcuba", 1, 0, False, "")
                'bolRespuesta = objDAO.InsertarReporteGerencialMetasDM(strXML.ToString(), strCodigoSeguimiento, intFirmaDB)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            End Try
            Return bolRespuesta
        End Function

        Public Sub InsertarXMLTabla(ByVal strXML As String)
            Dim objDAO As DAO = Nothing
            Try
                objDAO = New DAO
                objDAO.InsertaXMLaTabla("PTADESTINOS.WEB_DM_REPORTE_VENDEDOR", strXML.ToString(), "kcuba", 1, 0, False, Nothing, True)
            Catch ex As Exception

            End Try
        End Sub

        Public Function QuitarCaracteresEspeciales(ByVal strTexto As String)
            'Dim textoNormalizado As String = strTexto.Normalize(NormalizationForm.FormD)

            ''coincide todo lo que no sean letras y números ascii o espacio y lo reemplazamos por una cadena vacía.
            'Dim reg As Regex = New Regex("[^a-zA-Z0-9 ]")
            'Dim textoSinAcentos As String = reg.Replace(textoNormalizado, "")

            'Return strTexto

            Dim tempBytes As Byte()
            tempBytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(strTexto)
            Return System.Text.Encoding.UTF8.GetString(tempBytes)
        End Function
        Public Function ObtenerReporteEjecutivosVentasDM(ByVal strFechaInicio As String, ByVal strFechaFin As String) As DataTable
            Dim objDAO As DAO = Nothing
            Dim oDt As New DataTable
            Try
                objDAO = New DAO
                oDt = objDAO.ObtenerReporteEjecutivosVentasDM(strFechaInicio, strFechaFin)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            End Try
            Return oDt
        End Function

        Public Function ObtenerReporteCounterEuropaDM(ByVal strFechaInicio As String, ByVal strFechaFin As String) As DataTable
            Dim objDAO As DAO = Nothing
            Dim oDt As New DataTable
            Try
                objDAO = New DAO
                oDt = objDAO.ObtenerReporteCounterEuropaDM(strFechaInicio, strFechaFin)

            Catch ex As Exception
                Throw ex
            End Try
            Return oDt
        End Function

        Public Function ObtenerReporteCounterGeneralDM(ByVal strFechaInicio As String, ByVal strFechaFin As String) As DataTable
            Dim objDAO As DAO = Nothing
            Dim oDt As New DataTable
            Try
                objDAO = New DAO
                oDt = objDAO.ObtenerReporteCounterGeneralDM(strFechaInicio, strFechaFin)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            End Try
            Return oDt
        End Function

        Public Function ObtenerReporteCrucerosDM(ByVal strFechaInicio As String, ByVal strFechaFin As String) As DataTable
            Dim objDAO As DAO = Nothing
            Dim oDt As New DataTable
            Try
                objDAO = New DAO
                oDt = objDAO.ObtenerReporteCrucerosDM(strFechaInicio, strFechaFin)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            End Try
            Return oDt
        End Function

        Public Function ObtenerReporteEjecutivoEuropaJuniorQuerolDM(ByVal strFechaInicio As String, ByVal strFechaFin As String) As DataTable
            Dim objDAO As DAO = Nothing
            Dim oDt As New DataTable
            Try
                objDAO = New DAO
                oDt = objDAO.ObtenerReporteEjecutivoEuropaJuniorQuerolDM(strFechaInicio, strFechaFin)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            End Try
            Return oDt
        End Function

        Public Function ObtenerReporteEjecutivoRegionalDM(ByVal strFechaInicio As String, ByVal strFechaFin As String) As DataTable
            Dim objDAO As DAO = Nothing
            Dim oDt As New DataTable
            Try
                objDAO = New DAO
                oDt = objDAO.ObtenerReporteEjecutivoRegionalDM(strFechaInicio, strFechaFin)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            End Try
            Return oDt
        End Function

        Public Function ObtenerVendedorDestinosMundiales(ByVal strCodigoSeguimiento As String, _
                                                         ByVal intFirmaDB As Integer)
            Dim objDAO As DAO = Nothing
            Dim lstDatosAgente As New List(Of classDatosAgente)
            Try
                objDAO = New DAO
                lstDatosAgente = objDAO.ObtenerVendedorDestinosMundiales(strCodigoSeguimiento, intFirmaDB)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            End Try
            Return lstDatosAgente

        End Function
    End Class
End Namespace
