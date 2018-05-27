Imports GDS_NuevoMundoPersistencia
Imports GDS_NM_Mensajeria
Imports classDO = GDS_NuevoMundoAccesoDatos
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Imports BO = GDS_NuevoMundoDominio.ObjetoDominioNegocio.classBO
Imports LOG = GDS_MuevoMundoLog
Imports System.Linq
Imports System.IO
Imports System.Configuration


Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Private strConexion As String = "P"

        Dim esPrueba = ConfigurationSettings.AppSettings("ES_PRUEBA")
        Dim MailPrueba = ConfigurationSettings.AppSettings("MAIL_PRUEBA")
        Dim empresa = ConfigurationSettings.AppSettings("EMPRESA")


        Public Function EvaluarBoletosXCliente(ByVal lstBoletos As List(Of robotBoletoPendientePago.robotBoletoPendiente), ByVal Valor As String, ByVal Asunto As String, ByRef lstLog As List(Of String), ByVal strCodigoSeguimiento As String, ByVal intGDS As Integer, ByVal intFirmaGDS As Integer, ByVal intFirmaDB As Integer, ByVal intEsquema As Integer, ByVal objSession As classSession) As Boolean
            Dim lstBoletosXCliente As New List(Of robotBoletoPendientePago.robotBoletoPendiente)
            If lstBoletos IsNot Nothing Then
                Dim lstDK = (From x In (From b In lstBoletos
                                        Group b By b.Cliente.DK Into Cantidad = Count(b.NumeroBoleto)) Order By x.DK Select x).ToList
                For i As Integer = 0 To lstDK.count - 1
                    Dim boletoXCliente = lstBoletos.Where(Function(b) b.Cliente.DK = lstDK(i).DK).ToList
                    EnviarCorreoAvisoVoideo(boletoXCliente, "Agencia", Valor, Asunto, strCodigoSeguimiento)
                Next
            End If
        End Function

        Public Function EjecutarProcesoAvisoVoideo(ByVal lstBoletos As List(Of robotBoletoPendientePago.robotBoletoPendiente), ByVal Valor As String, ByVal Asunto As String, ByRef lstLog As List(Of String), ByVal strCodigoSeguimiento As String, ByVal intGDS As Integer, ByVal intFirmaGDS As Integer, ByVal intFirmaDB As Integer, ByVal intEsquema As Integer, ByVal objSession As classSession) As List(Of robotBoletoPendientePago.robotBoletoPendiente)
            Dim lstBoletosEmitidos As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim lstBoletosXVendedor As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim lstBoletosAvisoVoideo As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim lstBoletosVoideo As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim bolRespuesta As Boolean = False
            Dim lstBoletoVoideado As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim lstBoletosXAgencia As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim lstBoletosXPromotor As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim lstBoletosXCaja As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim arrValor As String() = {"AVI", "VOI", "AVI_A"}
            Dim oGeneraPayLoad As New LOG.GeneraPayLoad

            Try
                lstBoletosAvisoVoideo = New List(Of robotBoletoPendientePago.robotBoletoPendiente)
                lstBoletosVoideo = New List(Of robotBoletoPendientePago.robotBoletoPendiente)
                lstBoletoVoideado = New List(Of robotBoletoPendientePago.robotBoletoPendiente)
                lstBoletosEmitidos = lstBoletos

                'Agrupado por Vendedor
                Dim lstVendedor = (From b In lstBoletosEmitidos _
                                   Select New With {Key b.Vendedor.FirmaAgente} Distinct).ToList

                If lstVendedor IsNot Nothing Then
                    For k As Integer = 0 To lstVendedor.Count - 1
                        lstBoletosXVendedor = New List(Of robotBoletoPendientePago.robotBoletoPendiente)
                        lstBoletosXVendedor = (From b In lstBoletosEmitidos
                                               Where b.Vendedor.FirmaAgente = lstVendedor(k).FirmaAgente.ToString
                                               Select b).ToList()

                        If lstBoletosXVendedor IsNot Nothing Then
                            If lstBoletosXVendedor.Count > 0 Then
                                If Valor = "NBD" Or Valor = "NBD_A" Then
                                    bolRespuesta = EnviarCorreoReporteXFirma(lstBoletosXVendedor, "Counter", Valor, Asunto, strCodigoSeguimiento)
                                ElseIf Valor = "NFA" Or Valor = "NFA_AYER" Or Valor = "NFA_A" Or Valor = "NFA_AYER_A" Then
                                    bolRespuesta = EnviarCorreoReporteXFirma(lstBoletosXVendedor, "Counter", Valor, Asunto, strCodigoSeguimiento)
                                ElseIf Valor = "AVI" Or Valor = "AVI_A" Then
                                    bolRespuesta = EnviarCorreoAvisoVoideo(lstBoletosXVendedor, "Counter", Valor, Asunto, strCodigoSeguimiento)
                                    For x As Integer = 0 To lstBoletosXVendedor.Count - 1
                                        lstBoletosAvisoVoideo.Add(lstBoletosXVendedor.Item(x))
                                    Next
                                ElseIf Valor = "VOI" Then
                                    lstBoletosVoideo = Anular_BoletoEmitido1(lstBoletosXVendedor, "Counter", Asunto, lstLog, strCodigoSeguimiento, intFirmaDB, intGDS, intFirmaGDS, intEsquema, objSession)
                                    For g As Integer = 0 To lstBoletosVoideo.Count - 1
                                        lstBoletoVoideado.Add(lstBoletosVoideo.Item(g))
                                    Next
                                End If
                            End If
                        End If
                    Next
                End If

                '*****************************Relist de los boletos voideados******************************************'
                Dim oRpta As Boolean = False
                If lstBoletoVoideado IsNot Nothing Then
                    If lstBoletoVoideado.Count > 0 Then
                        oRpta = ReleaseBoletosVoideadosNoNM(lstBoletoVoideado, lstLog, strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, intEsquema, objSession)
                    End If
                End If
                '*********************************************************************************************************'

                'Agrupado por Promotor
                'Agrupando Boletos a enviar Aviso o Voideo por Agencia, Promotor y Caja
                If arrValor.Contains(Valor) Then '
                    If Valor = "VOI" Then
                        If lstBoletoVoideado IsNot Nothing Then
                            If lstBoletoVoideado.Count > 0 Then
                                lstBoletosEmitidos = lstBoletoVoideado
                            Else
                                lstLog.Add("No hay boletos a voidear......")
                                lstBoletosEmitidos = Nothing
                            End If
                        Else
                            lstLog.Add("No hay boletos a voidear......")
                            lstBoletosEmitidos = Nothing
                        End If
                    End If

                    If Valor = "AVI" Or Valor = "AVI_A" Then
                        If lstBoletosAvisoVoideo IsNot Nothing Then
                            If lstBoletosAvisoVoideo.Count > 0 Then
                                lstBoletosEmitidos = lstBoletosAvisoVoideo
                            Else
                                lstLog.Add("No hay boletos a voidear......")
                                lstBoletosEmitidos = Nothing
                            End If
                        Else
                            lstLog.Add("No hay boletos a voidear......")
                            lstBoletosEmitidos = Nothing
                        End If
                    End If


                    If lstBoletosEmitidos IsNot Nothing Then
                        If lstBoletosEmitidos.Count > 0 Then
                            'Agrupar por Agencia
                            Dim lstAgencia = (From x In lstBoletosEmitidos _
                                                    Select New With {Key x.Cliente.EmailAgencia} Distinct).ToList


                            'Agrupar por promotor
                            Dim lstPromotor = (From y In lstBoletosEmitidos _
                                                    Select New With {Key y.Promotor.EmailPromotor} Distinct).ToList

                            'Agrupar por Caja
                            Dim lstCaja = (From o In lstBoletosEmitidos _
                                           Select New With {Key o.Vendedor.CorreoCaja} Distinct).ToList

                            If lstAgencia IsNot Nothing Then
                                'Enviar Aviso Correo a Agencias
                                For k As Integer = 0 To lstAgencia.Count - 1
                                    If lstAgencia(k).EmailAgencia IsNot Nothing Then
                                        lstBoletosXAgencia = New List(Of robotBoletoPendientePago.robotBoletoPendiente)
                                        lstBoletosXAgencia = (From b In lstBoletosEmitidos
                                                             Where b.Cliente.EmailAgencia = lstAgencia(k).EmailAgencia
                                                             Select b).ToList()

                                        oGeneraPayLoad.Serializer(GetType(List(Of robotBoletoPendientePago.robotBoletoPendiente)), lstBoletosXAgencia, "ListaAgenciaAEnviar", "ROBOT_ANULACION", False, False)

                                        If lstBoletosXAgencia IsNot Nothing Then
                                            If lstBoletosXAgencia.Count > 0 Then
                                                If Valor = "AVI" Or Valor = "AVI_A" Then
                                                    If empresa = "NM" Then
                                                        bolRespuesta = EnviarCorreoAvisoVoideo(lstBoletosXAgencia, "Agencia", Valor, Asunto, strCodigoSeguimiento)
                                                    End If
                                                ElseIf Valor = "VOI" Then
                                                    EnviarCorreoAvisoVoideo(lstBoletosXAgencia, "Agencia", "VOI", Asunto, strCodigoSeguimiento)
                                                End If
                                                End If
                                            End If

                                        End If
                                Next
                            End If

                            If lstPromotor IsNot Nothing Then
                                'Enviar Aviso Correo a Promotores
                                For l As Integer = 0 To lstPromotor.Count - 1
                                    If lstPromotor(l).EmailPromotor IsNot Nothing Then
                                        lstBoletosXPromotor = New List(Of robotBoletoPendientePago.robotBoletoPendiente)
                                        lstBoletosXPromotor = (From b In lstBoletosEmitidos
                                                             Where b.Promotor.EmailPromotor = lstPromotor(l).EmailPromotor
                                                             Select b).ToList()

                                        If lstBoletosXPromotor IsNot Nothing Then
                                            If lstBoletosXPromotor.Count > 0 Then
                                                If Valor = "AVI" Or Valor = "AVI_A" Then
                                                    bolRespuesta = EnviarCorreoAvisoVoideo(lstBoletosXPromotor, "Promotor", Valor, Asunto, strCodigoSeguimiento)
                                                ElseIf Valor = "VOI" Then
                                                    EnviarCorreoAvisoVoideo(lstBoletosXPromotor, "Promotor", "VOI", Asunto, strCodigoSeguimiento)
                                                End If
                                            End If
                                        End If
                                    End If
                                Next
                            End If


                            'lstCaja
                            If lstCaja IsNot Nothing Then
                                For m As Integer = 0 To lstCaja.Count - 1
                                    If lstCaja(m).CorreoCaja IsNot Nothing Then
                                        lstBoletosXCaja = New List(Of robotBoletoPendientePago.robotBoletoPendiente)
                                        lstBoletosXCaja = (From b In lstBoletosEmitidos
                                                             Where b.Vendedor.CorreoCaja = lstCaja(m).CorreoCaja
                                                             Select b).ToList()

                                        If lstBoletosXCaja IsNot Nothing Then
                                            If lstBoletosXCaja.Count > 0 Then
                                                If Valor = "AVI" Or Valor = "AVI_A" Then
                                                    bolRespuesta = EnviarCorreoAvisoVoideo(lstBoletosXCaja, "Caja", Valor, Asunto, strCodigoSeguimiento)
                                                ElseIf Valor = "VOI" Then
                                                    EnviarCorreoAvisoVoideo(lstBoletosXCaja, "Caja", "VOI", Asunto, strCodigoSeguimiento)
                                                End If
                                            End If
                                        End If

                                    End If

                                Next
                            End If
                        End If
                    End If
                End If
            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                lstBoletosEmitidos = Nothing
                lstBoletosXVendedor = Nothing
                lstBoletosVoideo = Nothing
                bolRespuesta = False
                lstBoletosXAgencia = Nothing
                lstBoletosXPromotor = Nothing
                lstBoletosXCaja = Nothing
                arrValor = Nothing
                oGeneraPayLoad = Nothing
            End Try

            If Valor = "AVI" Or Valor = "AVI_A" Then
                Return lstBoletosAvisoVoideo
            ElseIf Valor = "VOI" Then
                Return lstBoletoVoideado
            Else
                Return New List(Of robotBoletoPendientePago.robotBoletoPendiente)
            End If
        End Function

        Public Function ReleaseBoletosVoideadosNoNM(ByVal lstBoletoVoideado As List(Of robotBoletoPendientePago.robotBoletoPendiente), ByRef lstLog As List(Of String), ByVal strCodigoSeguimiento As String, ByVal intGDS As Integer, ByVal intFirmaGDS As Integer, ByVal intFirmaDB As Integer, ByVal intEsquema As Integer, ByVal objSession As classSession) As Boolean
            Dim objBO As BO = Nothing
            Dim intCantidad As Integer = 0
            objBO = New BO
            Dim strPseudoAuxiliar = ConfigurationSettings.AppSettings("PSEUDO_RELEASE")

            Dim strRspta As String = Nothing
            Dim oRespuesta As Boolean = False
            Dim oWETRV As String = Nothing
            If lstBoletoVoideado IsNot Nothing Then
                If lstBoletoVoideado.Count > 0 Then
                    objBO.SabreCommand("IG", "", strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                    objBO.CambiarPseudo(strPseudoAuxiliar, strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                    For g As Integer = 0 To lstBoletoVoideado.Count - 1
                        If lstBoletoVoideado.Item(g).PseudoRelease IsNot Nothing Then
                            intCantidad = objBO.ObtenerExistePseudoNMundo(lstBoletoVoideado.Item(g).PseudoRelease, strCodigoSeguimiento, intFirmaDB, intEsquema)
                            If intCantidad = 0 Then
                                oWETRV = objBO.SabreCommand("*" & lstBoletoVoideado.Item(g).PNR, "", strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                If oWETRV.IndexOf("Simultaneous Changes to PNR has Occured") > -1 Then
                                    objBO.SabreCommand("IG", "", strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                    oWETRV = objBO.SabreCommand("*" & lstBoletoVoideado.Item(g).PNR, "", strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                ElseIf oWETRV.IndexOf("NO PNR PRESENT IN WORK AREA") > -1 Then
                                    objBO.SabreCommand("IG", "", strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                    oWETRV = objBO.SabreCommand("*" & lstBoletoVoideado.Item(g).PNR, "", strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                ElseIf oWETRV.IndexOf("UPDATE OR IGNORE") > -1 Then
                                    objBO.SabreCommand("IG", "", strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                    oWETRV = objBO.SabreCommand("*" & lstBoletoVoideado.Item(g).PNR, "", strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                End If
                                strRspta = objBO.SWS_Release(lstBoletoVoideado.Item(g).PNR, lstBoletoVoideado.Item(g).PseudoRelease, "ROBOT", strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                If strRspta.IndexOf("*") > -1 Then
                                    strRspta = objBO.SabreCommand("ET", "", strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                    lstLog.Add("El boleto Nro. " & lstBoletoVoideado.Item(g).NumeroBoleto & " , del PNR " & lstBoletoVoideado.Item(g).PNR & " , Pseudo " & lstBoletoVoideado.Item(g).Pseudo & " , Pseudo Release " & lstBoletoVoideado.Item(g).PseudoRelease & " , el resultado del release es : " & strRspta)
                                    oRespuesta = True
                                Else
                                    oRespuesta = False
                                    lstLog.Add("El boleto Nro. " & lstBoletoVoideado.Item(g).NumeroBoleto & " , del PNR " & lstBoletoVoideado.Item(g).PNR & " , Pseudo " & lstBoletoVoideado.Item(g).Pseudo & " , Pseudo Release " & lstBoletoVoideado.Item(g).PseudoRelease & " , el resultado del release es : " & strRspta)
                                End If
                            End If
                        End If
                    Next
                End If

            End If
            Return oRespuesta
        End Function

        Public Function EnviarCorreoAvisoVoideo(ByVal lstBoletos As List(Of robotBoletoPendientePago.robotBoletoPendiente), ByVal strDirigidoA As String, ByVal Valor As String, ByVal strAsunto As String, ByVal strCodigoSeguimiento As String) As Boolean
            Dim intDiaSem = Convert.ToInt32(DateTime.Now.DayOfWeek)
            Dim webContacto = ConfigurationSettings.AppSettings("WEB_CONTACTO")
            Dim horaMailAnulacion = IIf(intDiaSem = 6, ConfigurationSettings.AppSettings("MAIL_HORA_ANULACION_SABADO"), ConfigurationSettings.AppSettings("MAIL_HORA_ANULACION_LV"))
            Dim horaMailMaximaPago = IIf(intDiaSem = 6, ConfigurationSettings.AppSettings("MAIL_HORA_MAX_PAGO_SABADO"), ConfigurationSettings.AppSettings("MAIL_HORA_MAX_PAGO_LV"))

            Dim objEnviarEmail As EnviarEmail = Nothing
            Dim objCuerpoCorreo As cuerpoCorreo = Nothing
            Dim objCorreo As classCorreo = Nothing
            Dim strCadenaReporte As New System.Text.StringBuilder
            Dim objBO As BO = Nothing
            Dim bolRespuesta As Boolean = False

            '************************************Declaracion de Variables para Tabla de Boletos******************************************************************
            Dim strCorreoPromotor As String = Nothing
            Dim strCorreoJefePromotor As String = Nothing
            '***************************************************************************************************************************************************
            Try
                If lstBoletos.Item(0).Promotor.EmailPromotor Is Nothing Then
                    strCorreoPromotor = ConfigurationSettings.AppSettings("MAIL_PROMOTORES")
                Else
                    strCorreoPromotor = lstBoletos.Item(0).Promotor.EmailPromotor
                End If

                If lstBoletos IsNot Nothing Then
                    objBO = New BO
                    strCadenaReporte.Append("<br><table cellSpacing='0' cellPadding='3' border='0' width='100%' >" & vbCrLf)
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td>" & vbCrLf)
                    strCadenaReporte.Append("<table cellSpacing='0' cellPadding='3' border='0' width='100%' >" & vbCrLf)
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td class='textContenido'>" & vbCrLf)
                    strCadenaReporte.Append("Fecha / Hora : " & Date.Now & vbCrLf)
                    strCadenaReporte.Append("</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)
                    If Valor = "AVI" Or Valor = "AVIC" Or Valor = "AVI_A" Then
                        strCadenaReporte.Append("<tr>" & vbCrLf)
                        strCadenaReporte.Append("<td class='textContenidoROBOT'>" & vbCrLf)
                        If strDirigidoA = "Counter" Then
                            strCadenaReporte.Append("Estimado(a): " & lstBoletos.Item(0).Vendedor.NombreVendedor & ", " & "<br>" & vbCrLf)
                        ElseIf strDirigidoA = "Promotor" Then
                            strCadenaReporte.Append("Estimado(a): " & lstBoletos.Item(0).Promotor.NombrePromotor & ", " & "<br>" & vbCrLf)
                        ElseIf strDirigidoA = "Agencia" Then
                            strCadenaReporte.Append("Estimado Cliente : " & lstBoletos.Item(0).Cliente.NombreComercial & ", " & "<br>" & vbCrLf)
                        ElseIf strDirigidoA = "Caja" Then
                            strCadenaReporte.Append("Caja : " & lstBoletos.Item(0).Descripcion & ", " & "<br>" & vbCrLf)
                        End If
                        strCadenaReporte.Append("</td>" & vbCrLf)
                        strCadenaReporte.Append("</tr>" & vbCrLf)

                        If Valor = "AVI" Or Valor = "AVI_A" Then
                            strCadenaReporte.Append("<tr>" & vbCrLf)
                            strCadenaReporte.Append("<td class='textContenido'>" & vbCrLf)
                            If empresa = "NM" Then
                                strCadenaReporte.Append("Se hace de tu conocimiento que los siguientes boletos serán anulados a las ")
                                strCadenaReporte.Append(horaMailAnulacion)
                                strCadenaReporte.Append(", por FALTA DE PAGO.")
                            Else
                                strCadenaReporte.Append("Se hace de tu conocimiento que los siguientes boletos aún se encuentran pendientes de pago. ")
                            End If
                        Else
                            strCadenaReporte.Append("<tr>" & vbCrLf)
                            strCadenaReporte.Append("<td class='textContenido'>" & vbCrLf)
                            strCadenaReporte.Append("Se hace de su conocimiento que aún mantiene un saldo pendiente por boletos emitidos el día de hoy.")
                        End If

                        If Valor = "AVI" Or Valor = "AVI_A" Then
                            If strDirigidoA = "Agencia" Then
                                strCadenaReporte.Append("<br> " & vbCrLf)
                                strCadenaReporte.Append("Puedes hacer el pago y registrarlo antes de las ")
                                strCadenaReporte.Append(horaMailMaximaPago)
                                strCadenaReporte.Append("  en nuestro portal web: www.interagencias.com.pe, función:  <span style='color:blue'>REGISTRA TU PAGO</span>.")
                                strCadenaReporte.Append("<br> " & vbCrLf)
                                strCadenaReporte.Append("Si ya realizaste el registro antes de que llegue este mensaje, revisa tu <span style='color:blue'> Reporte Administrativo </span> en nuestro portal.")
                                strCadenaReporte.Append("<br> " & vbCrLf)
                                strCadenaReporte.Append("<br> " & vbCrLf)
                                strCadenaReporte.Append("<span style='font-weight: bold; color:orange'>En contacto Interagencias!</span>")
                            Else
                                If empresa <> "NM" Then
                                    strCadenaReporte.Append(" Por favor verificar si la agencia ya realizó el pago. " & vbCrLf)
                                End If
                            End If
                        Else
                            strCadenaReporte.Append("<br> " & vbCrLf)
                            strCadenaReporte.Append("No se anulará automáticamente ya que el saldo pendiente es menor a $20.00, por favor regularizar a la brevedad posible. ")
                            strCadenaReporte.Append("<br> " & vbCrLf)
                        End If
                        strCadenaReporte.Append("</td>" & vbCrLf)
                        strCadenaReporte.Append("</tr>" & vbCrLf)
                    ElseIf Valor = "VOI" Or Valor = "VOIC" Then
                        strCadenaReporte.Append("<tr>" & vbCrLf)
                        strCadenaReporte.Append("<td class='textContenido'>" & vbCrLf)
                        If strDirigidoA = "Agencia" Then
                            strCadenaReporte.Append("El Robot de Turbo SABRE anuló los siguientes boletos ya que el Cliente " & lstBoletos.Item(0).Cliente.NombreComercial & " tiene <br>" & vbCrLf)
                        Else
                            strCadenaReporte.Append("El Robot de Turbo SABRE anuló los siguientes boletos que tiene <br>" & vbCrLf)
                        End If
                        strCadenaReporte.Append("condición de pago CONTADO y el boleto NO FUE PAGADO A LA HORA INDICADA. <br>" & vbCrLf)
                        strCadenaReporte.Append("Por favor, tomar en cuenta esta acción.  <br>" & vbCrLf)
                        strCadenaReporte.Append("Si pagaste antes de la hora de anulacion indicada, por favor, contacta con <br>" & vbCrLf)
                        strCadenaReporte.Append("tu asesor comercial en el correo aquí copiado o búscalo en: <br>" & vbCrLf)
                        strCadenaReporte.Append(webContacto & "<br>" & vbCrLf)
                        strCadenaReporte.Append("</td>" & vbCrLf)
                        strCadenaReporte.Append("</tr>" & vbCrLf)
                    ElseIf Valor = "NOVOIDPTA" Then
                        strCadenaReporte.Append("<tr>" & vbCrLf)
                        strCadenaReporte.Append("<td class='textContenido'>" & vbCrLf)
                        strCadenaReporte.Append("El robot de Turbo SABRE NO PUDO ANULAR los boletos en PTA. <br>" & vbCrLf)
                        strCadenaReporte.Append("</td>" & vbCrLf)
                        strCadenaReporte.Append("</tr>" & vbCrLf)
                    ElseIf Valor = "NOVOIDSABRE" Or Valor = "NOVOIDREST" Then
                        strCadenaReporte.Append("<tr>" & vbCrLf)
                        strCadenaReporte.Append("<td class='textContenido'>" & vbCrLf)
                        strCadenaReporte.Append("El robot de turbo SABRE NO PUDO ANULAR los siguientes boletos. Verificar los mensajes de error devueltos por el GDS : <br>" & vbCrLf)
                        strCadenaReporte.Append("</td>" & vbCrLf)
                        strCadenaReporte.Append("</tr>" & vbCrLf)
                    End If
                    strCadenaReporte.Append("</table>" & vbCrLf)
                    strCadenaReporte.Append("</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)
                    strCadenaReporte.Append("</table>" & vbCrLf)

                    Dim lstBoletosXAgencia1 As New List(Of robotBoletoPendientePago.robotBoletoPendiente)
                    If strDirigidoA = "Promotor" Then
                        strCorreoJefePromotor = lstBoletos.Item(0).Promotor.CorreoJefePromotor
                        Dim lstAgencia = (From b In lstBoletos _
                                          Select New With {Key b.Cliente.NombreComercial, b.Cliente.DK} Distinct).ToList

                        For a As Integer = 0 To lstAgencia.Count - 1
                            strCadenaReporte.Append("<table cellSpacing='0' cellPadding='3' border='0' width='100%' >" & vbCrLf)
                            strCadenaReporte.Append("<tr>" & vbCrLf)
                            strCadenaReporte.Append("<td class='textContenidoNegrita'>" & vbCrLf)
                            strCadenaReporte.Append("AGENCIA : " & lstAgencia(a).NombreComercial & " DK : " & lstAgencia(a).DK & vbCrLf)
                            strCadenaReporte.Append("</td>" & vbCrLf)
                            strCadenaReporte.Append("</tr>" & vbCrLf)
                            strCadenaReporte.Append("</table>" & vbCrLf)
                            lstBoletosXAgencia1 = (From b In lstBoletos Where b.Cliente.NombreComercial = lstAgencia(a).NombreComercial Select b).ToList
                            strCadenaReporte = ConstruirHTMLCorreo(strCadenaReporte, lstBoletosXAgencia1, Valor, strDirigidoA)
                            strCadenaReporte.Append("<table cellSpacing='0' cellPadding='3' border='0' width='100%' >" & vbCrLf)
                            strCadenaReporte.Append("<tr>" & vbCrLf)
                            strCadenaReporte.Append("<td >" & vbCrLf)
                            strCadenaReporte.Append("" & vbCrLf)
                            strCadenaReporte.Append("</td>" & vbCrLf)
                            strCadenaReporte.Append("</tr>" & vbCrLf)
                            strCadenaReporte.Append("</table>" & vbCrLf)
                        Next
                    Else
                        strCadenaReporte = ConstruirHTMLCorreo(strCadenaReporte, lstBoletos, Valor, strDirigidoA)
                    End If

                    strCadenaReporte.Append("<table cellSpacing='0' cellPadding='3' border='0' width='100%' >" & vbCrLf)
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td class='textContenido'>" & vbCrLf)
                    strCadenaReporte.Append("****** Gracias por su atención ******")
                    strCadenaReporte.Append("</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)
                    strCadenaReporte.Append("</table>" & vbCrLf)

                    If Not String.IsNullOrEmpty(strCadenaReporte.ToString) Then
                        objCorreo = New classCorreo
                        If strDirigidoA = "Counter" Then
                            If Valor = "NOVOIDSABRE" Or Valor = "NOVOIDPTA" Then
                                objCorreo.ToCorreo = Constantes.emailCoordConsolidador & Constantes.PuntoComa & Constantes.emailCounterTurno & Constantes.PuntoComa & Constantes.emailSupervisoraCounterIA
                                objCorreo.BCCCorreo = Constantes.emailRobotAlertas
                            ElseIf Valor = "NOVOIDREST" Then
                                objCorreo.ToCorreo = lstBoletos.Item(0).Vendedor.CorreoVendedor & Constantes.PuntoComa & Constantes.emailCoordConsolidador & Constantes.PuntoComa & Constantes.emailCounterTurno & Constantes.PuntoComa & Constantes.emailSupervisoraCounterIA
                                objCorreo.BCCCorreo = Constantes.emailRobotAlertas
                            Else
                                objCorreo.ToCorreo = lstBoletos.Item(0).Vendedor.CorreoVendedor
                                objCorreo.CCCorreo = strCorreoPromotor
                                objCorreo.BCCCorreo = Constantes.emailRobotAlertas
                            End If
                        ElseIf strDirigidoA = "Promotor" Then
                            objCorreo.ToCorreo = strCorreoPromotor
                            If strCorreoJefePromotor IsNot Nothing Then
                                objCorreo.CCCorreo = strCorreoJefePromotor
                            End If
                            objCorreo.BCCCorreo = Constantes.emailRobotAlertas
                        ElseIf strDirigidoA = "Agencia" Then
                            objCorreo.ToCorreo = lstBoletos.Item(0).Cliente.EmailAgencia
                            ' objCorreo.CCCorreo = strCorreoPromotor & Constantes.PuntoComa & lstBoletos.Item(0).Vendedor.CorreoCaja & Constantes.PuntoComa & Constantes.emailCoordConsolidador
                            objCorreo.CCCorreo = strCorreoPromotor & Constantes.PuntoComa & Constantes.PuntoComa & Constantes.emailCoordConsolidador
                            objCorreo.BCCCorreo = Constantes.emailRobotAlertas
                        ElseIf strDirigidoA = "Caja" Then
                            objCorreo.ToCorreo = lstBoletos.Item(0).Vendedor.CorreoCaja & ";cajaconsolidador@nmviajes.com"
                            objCorreo.BCCCorreo = Constantes.emailRobotAlertas
                        End If
                        'objCorreo.NombreCorreo = "Robot Anulacion Sabre - " & strDirigidoA
                        objCorreo.NombreCorreo = "Robot Anulaciones Avisos  - " & strDirigidoA

                        If strDirigidoA = "Agencia" Then
                            objCorreo.SubjectCorreo = strAsunto & ", Cliente: " & lstBoletos.Item(0).Cliente.NombreComercial & " - DK: " & lstBoletos.Item(0).Cliente.DK
                        ElseIf strDirigidoA = "Counter" Then
                            If Valor = "NOVOIDSABRE" Or Valor = "NOVOIDPTA" Or Valor = "NOVOIDREST" Then
                                objCorreo.SubjectCorreo = "El Robot Turbo Anulaciones NO PUDO ANULAR boletos pendientes de pago."
                            Else
                                objCorreo.SubjectCorreo = strAsunto & ", Counter: " & lstBoletos.Item(0).Vendedor.NombreVendedor
                            End If
                        ElseIf strDirigidoA = "Promotor" Then
                            objCorreo.SubjectCorreo = strAsunto & ", Promotor: " & lstBoletos.Item(0).Promotor.NombrePromotor
                        ElseIf strDirigidoA = "Caja" Then
                            objCorreo.SubjectCorreo = strAsunto & ", Caja: " & lstBoletos.Item(0).Descripcion
                        Else
                            objCorreo.SubjectCorreo = strAsunto
                        End If

                        objCuerpoCorreo = New cuerpoCorreo
                        objCorreo.BodyCorreo = objCuerpoCorreo.GeneraCuerpoRobot(strCadenaReporte.ToString)

                        objEnviarEmail = New EnviarEmail

                        Dim intrspta As Integer = 0

                        If esPrueba = "1" Then
                            objCorreo.ToCorreo = MailPrueba
                            objCorreo.CCCorreo = ""
                            objCorreo.BCCCorreo = ""
                        End If
                       
                        intrspta = objEnviarEmail.Send(objCorreo, True, strCodigoSeguimiento, 7, Nothing)
                        If intrspta = 0 Then
                            objEscribeLog.WriteLog("Se volvio a enviar correo", "EnviarEmail_Send ", strCodigoSeguimiento)
                            Threading.Thread.Sleep(2000)
                            intrspta = objEnviarEmail.Send(objCorreo, True, strCodigoSeguimiento, 7, Nothing)
                        End If
                        bolRespuesta = True
                    End If
                End If
            Catch ex As Exception
                Throw ex
            Finally
                objEnviarEmail = Nothing
                objCuerpoCorreo = Nothing
                objCorreo = Nothing
                strCadenaReporte = Nothing
                objBO = Nothing
                strCorreoPromotor = Nothing
                strCorreoJefePromotor = Nothing
            End Try
            Return bolRespuesta
        End Function


        Public Function ConstruirHTMLCorreo_NO_DQB(ByVal strCadenaReporte As System.Text.StringBuilder, ByVal lstBoletos As List(Of robotBoletoPendientePago.robotBoletoPendiente), ByVal strValor As String, ByVal strDirigidoA As String) As System.Text.StringBuilder
            Dim strDetalleBoletos1 As New System.Text.StringBuilder
            Dim EstiloClase As String = String.Empty
            Dim intContadorPseudo As Integer = 0
            Dim intContadorSucursal As Integer = 0
            Dim intContadorFile As Integer = 0
            Dim intContadorComprobante As Integer = 0
            Dim strPseudoAdd As String = ""
            Dim strSucursalAdd As String = ""
            Dim strFileAdd As String = ""
            Dim strComprobanteAdd As String = ""

            Dim intNroBoletoSucursal As Integer = 0
            Dim intNroBoletoPseudo As Integer = 0
            Dim intNroBoletoFile As Integer = 0
            Dim intNroBoletoComprobante As Integer = 0

            Dim intPosPseudo As Integer = 0
            Dim intPosSucursal As Integer = 0
            Dim intPosFile As Integer = 0
            Dim intPosComprobante As Integer = 0

            strCadenaReporte.Append("<table cellSpacing='0' cellPadding='3' border='0' width='100%' style='background-color:White;border:1px #CC9966 solid;border-collapse:collapse;'>" & vbCrLf)
            strCadenaReporte.Append("<tr>" & vbCrLf)
            If strDirigidoA <> "Agencia" Then
                strCadenaReporte.Append("<td height='30' class='tarifario_header' align='center'>PSEUDO</td>" & vbCrLf)
            End If
            strCadenaReporte.Append("<td class='tarifario_header' align='center'>SUCURSAL</td>" & vbCrLf)
            strCadenaReporte.Append("<td class='tarifario_header' align='center'>FILE</td>" & vbCrLf)
            strCadenaReporte.Append("<td class='tarifario_header' align='center'>COMPROBANTE</td>" & vbCrLf)
            If strDirigidoA = "Counter" Or strDirigidoA = "Caja" Then
                strCadenaReporte.Append("<td height='30' class='tarifario_header' align='center'>DK</td>" & vbCrLf)
            End If
            strCadenaReporte.Append("<td class='tarifario_header' align='center'>PNR</td>" & vbCrLf)
            strCadenaReporte.Append("<td class='tarifario_header' align='center'>N&Uacute;MERO BOLETO</td>" & vbCrLf)
            strCadenaReporte.Append("<td class='tarifario_header' align='center'>NOMBRE PASAJERO</td>" & vbCrLf)
            strCadenaReporte.Append("<td class='tarifario_header' align='center'>PROMOTOR</td>" & vbCrLf)
            strCadenaReporte.Append("<td class='tarifario_header' align='center'>DEUDA PENDIENTE</td>" & vbCrLf)
            strCadenaReporte.Append("<td class='tarifario_header' align='center'>ESTADO</td>" & vbCrLf)

            If strValor = "NOVOIDSABRE" Or strValor = "NOVOIDPTA" Or strValor = "NOVOIDREST" Then
                strCadenaReporte.Append("<td class='tarifario_header' align='center'>MENSAJE ERROR</td>" & vbCrLf)
            End If
            strCadenaReporte.Append("</tr>" & vbCrLf)

            Dim lstBoletosXPseudo = (From x In (From b In lstBoletos
                                     Group b By b.Pseudo Into Cantidad = Count(b.NumeroBoleto)) Order By x.Pseudo Select x).ToList

            Dim lstBoletosXSucursal = (From x In (From b In lstBoletos
                                       Group b By b.Pseudo, b.Descripcion Into Cantidad = Count(b.NumeroBoleto)) Order By x.Pseudo, x.Descripcion Select x).ToList

            Dim lstBoletosXFile = (From x In (From b In lstBoletos
                                   Group b By b.Pseudo, b.Descripcion, b.File Into Cantidad = Count(b.NumeroBoleto)) Order By x.Pseudo, x.Descripcion, x.File Select x).ToList

            Dim lstBoletosXComprobante = (From x In (From b In lstBoletos
                                          Group b By b.Pseudo, b.Descripcion, b.File, b.Comprobante Into Cantidad = Count(b.NumeroBoleto)) Order By x.Pseudo, x.Descripcion, x.File, x.Comprobante Select x).ToList

            lstBoletos = (From m In lstBoletos
                         Order By m.Pseudo, m.Descripcion, m.File, m.Comprobante
                         Select m).ToList

            '       SUCURSAL	FILE	COMPROBANTE	    PNR	NÚMERO BOLETO	NOMBRE PASAJERO	PROMOTOR	DEUDA PENDIENTE	ESTADO
            'PSEUDO	SUCURSAL	FILE	COMPROBANTE	DK	PNR	NÚMERO BOLETO	NOMBRE PASAJERO	PROMOTOR	DEUDA PENDIENTE	ESTADO
            For j As Integer = 0 To lstBoletos.Count - 1
                strDetalleBoletos1 = New System.Text.StringBuilder
                EstiloClase = " class='tarifario_fila_a' align='center'"

                strDetalleBoletos1.Append("<tr>" & vbCrLf)
                If strDirigidoA <> "Agencia" Then
                    If intContadorPseudo < lstBoletosXPseudo.count Then
                        If j = 0 Then
                            If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXPseudo.Item(intContadorPseudo).Pseudo)) Then
                                strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXPseudo(intContadorPseudo).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).Pseudo & "</td>" & vbCrLf)
                                strPseudoAdd = lstBoletos.Item(j).Pseudo
                                intPosPseudo = j
                                intNroBoletoPseudo = lstBoletosXPseudo(intContadorPseudo).Cantidad
                            End If
                        Else
                            If j < intPosPseudo + intNroBoletoPseudo Then
                                'nada
                            Else
                                intContadorPseudo += 1
                                strPseudoAdd = ""
                                intNroBoletoPseudo = 0
                                If intContadorPseudo < lstBoletosXPseudo.count Then
                                    If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXPseudo.Item(intContadorPseudo).Pseudo)) Then
                                        strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXPseudo(intContadorPseudo).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).Pseudo & "</td>" & vbCrLf)
                                        strPseudoAdd = lstBoletos.Item(j).Pseudo
                                        intPosPseudo = j
                                        intNroBoletoPseudo = lstBoletosXPseudo(intContadorPseudo).Cantidad
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If


                If intContadorSucursal < lstBoletosXSucursal.Count Then
                    If j = 0 Then
                        If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXSucursal.Item(intContadorSucursal).Pseudo)) And (lstBoletos.Item(j).Descripcion.Equals(lstBoletosXSucursal.Item(intContadorSucursal).Descripcion)) Then
                            strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXSucursal(intContadorSucursal).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).Descripcion & "</td>" & vbCrLf)
                            strSucursalAdd = lstBoletos.Item(j).Pseudo & lstBoletos.Item(j).Descripcion
                            intNroBoletoSucursal = lstBoletosXSucursal(intContadorSucursal).Cantidad
                            intPosSucursal = j
                        End If
                    Else
                        If j < intPosSucursal + intNroBoletoSucursal Then

                        Else
                            intContadorSucursal += 1
                            strSucursalAdd = ""
                            intNroBoletoSucursal = 0
                            If intContadorSucursal < lstBoletosXSucursal.count Then
                                If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXSucursal.Item(intContadorSucursal).Pseudo)) And (lstBoletos.Item(j).Descripcion.Equals(lstBoletosXSucursal.Item(intContadorSucursal).Descripcion)) Then
                                    strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXSucursal(intContadorSucursal).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).Descripcion & "</td>" & vbCrLf)
                                    strSucursalAdd = lstBoletos.Item(j).Pseudo & lstBoletos.Item(j).Descripcion
                                    intNroBoletoSucursal = lstBoletosXSucursal(intContadorSucursal).Cantidad
                                    intPosSucursal = j
                                End If
                            End If
                        End If
                    End If
                End If

                If intContadorFile < lstBoletosXFile.Count Then
                    If j = 0 Then
                        If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXFile.Item(intContadorFile).Pseudo)) And (lstBoletos.Item(j).Descripcion.Equals(lstBoletosXFile.Item(intContadorFile).Descripcion)) And (lstBoletos.Item(j).File.Equals(lstBoletosXFile.Item(intContadorFile).File)) Then
                            strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXFile(intContadorFile).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).File & "</td>" & vbCrLf)
                            strFileAdd = lstBoletos.Item(j).Pseudo & lstBoletos.Item(j).Descripcion & lstBoletos.Item(j).File
                            intNroBoletoFile = lstBoletosXFile(intContadorFile).Cantidad
                            intPosFile = j
                        End If
                    Else
                        If j < intPosFile + intNroBoletoFile Then

                        Else
                            intContadorFile += 1
                            strFileAdd = ""
                            intNroBoletoFile = 0
                            If intContadorFile < lstBoletosXFile.count Then
                                If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXFile.Item(intContadorFile).Pseudo)) And (lstBoletos.Item(j).Descripcion.Equals(lstBoletosXFile.Item(intContadorFile).Descripcion)) And (lstBoletos.Item(j).File.Equals(lstBoletosXFile.Item(intContadorFile).File)) Then
                                    strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXFile(intContadorFile).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).File & "</td>" & vbCrLf)
                                    strFileAdd = lstBoletos.Item(j).Pseudo & lstBoletos.Item(j).Descripcion & lstBoletos.Item(j).File
                                    intNroBoletoFile = lstBoletosXFile(intContadorFile).Cantidad
                                    intPosFile = j
                                End If
                            End If
                        End If
                    End If

                End If

                If intContadorComprobante < lstBoletosXComprobante.Count Then
                    If j = 0 Then
                        If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXComprobante.Item(intContadorComprobante).Pseudo)) And (lstBoletos.Item(j).Descripcion.Equals(lstBoletosXComprobante.Item(intContadorComprobante).Descripcion)) And (lstBoletos.Item(j).File.Equals(lstBoletosXComprobante.Item(intContadorComprobante).File)) And (lstBoletos.Item(j).Comprobante.Equals(lstBoletosXComprobante.Item(intContadorComprobante).Comprobante)) Then
                            strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXComprobante(intContadorComprobante).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).Comprobante & "</td>" & vbCrLf)
                            strComprobanteAdd = lstBoletos.Item(j).Pseudo & lstBoletos.Item(j).Descripcion & lstBoletos.Item(j).File & lstBoletos.Item(j).Comprobante
                            intNroBoletoComprobante = lstBoletosXComprobante(intContadorComprobante).Cantidad
                            intPosComprobante = j
                        End If
                    Else
                        If j < intPosComprobante + intNroBoletoComprobante Then

                        Else
                            intContadorComprobante += 1
                            strFileAdd = ""
                            intNroBoletoComprobante = 0
                            If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXComprobante.Item(intContadorComprobante).Pseudo)) And (lstBoletos.Item(j).Descripcion.Equals(lstBoletosXComprobante.Item(intContadorComprobante).Descripcion)) And (lstBoletos.Item(j).File.Equals(lstBoletosXComprobante.Item(intContadorComprobante).File)) And (lstBoletos.Item(j).Comprobante.Equals(lstBoletosXComprobante.Item(intContadorComprobante).Comprobante)) Then
                                strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXComprobante(intContadorComprobante).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).Comprobante & "</td>" & vbCrLf)
                                strComprobanteAdd = lstBoletos.Item(j).Pseudo & lstBoletos.Item(j).Descripcion & lstBoletos.Item(j).File & lstBoletos.Item(j).Comprobante
                                intNroBoletoComprobante = lstBoletosXComprobante(intContadorComprobante).Cantidad
                                intPosComprobante = j
                            End If
                        End If
                    End If
                End If

                If strDirigidoA = "Counter" Or strDirigidoA = "Caja" Then
                    strDetalleBoletos1.Append("<td " & EstiloClase & ">" & lstBoletos.Item(j).Cliente.DK & "</td>" & vbCrLf)
                End If
                strDetalleBoletos1.Append("<td height='30' " & EstiloClase & ">" & lstBoletos.Item(j).PNR & "</td>" & vbCrLf)
                strDetalleBoletos1.Append("<td " & EstiloClase & ">" & lstBoletos.Item(j).PrefijoBoleto & lstBoletos.Item(j).NumeroBoleto & "</td>" & vbCrLf)
                strDetalleBoletos1.Append("<td " & EstiloClase & ">" & lstBoletos.Item(j).NombrePasajero & "</td>" & vbCrLf)
                strDetalleBoletos1.Append("<td " & EstiloClase & ">" & lstBoletos.Item(j).Promotor.NombrePromotor & "</td>" & vbCrLf)
                strDetalleBoletos1.Append("<td " & EstiloClase & ">" & (Val(lstBoletos.Item(j).MontoPendiente) - Val(lstBoletos.Item(j).MontoOtroDK)).ToString.Replace(",", ".") & "</td>" & vbCrLf)
                strDetalleBoletos1.Append("<td " & EstiloClase & ">" & lstBoletos.Item(j).Estado & "</td>" & vbCrLf)
                If strValor = "NOVOIDSABRE" Or strValor = "NOVOIDPTA" Or strValor = "NOVOIDREST" Then
                    strDetalleBoletos1.Append("<td " & EstiloClase & ">" & lstBoletos.Item(j).MensajeError & "</td>" & vbCrLf)
                End If
                strDetalleBoletos1.Append("</tr>" & vbCrLf)
                strCadenaReporte.Append(strDetalleBoletos1)
            Next
            strCadenaReporte.Append("</table>" & vbCrLf)

            Return strCadenaReporte
        End Function



        Public Function ConstruirHTMLCorreo(ByVal strCadenaReporte As System.Text.StringBuilder, ByVal lstBoletos As List(Of robotBoletoPendientePago.robotBoletoPendiente), ByVal strValor As String, ByVal strDirigidoA As String) As System.Text.StringBuilder
            Dim strDetalleBoletos1 As New System.Text.StringBuilder
            'Dim strDetallePasajeros As New System.Text.StringBuilder
            'Dim objBO As BO = Nothing
            Dim EstiloClase As String = String.Empty
            'Dim EstiloClase_c As String = " class='tarifario_fila_c' align='center'"

            Dim intContadorPseudo As Integer = 0
            Dim intContadorSucursal As Integer = 0
            Dim intContadorFile As Integer = 0
            Dim intContadorComprobante As Integer = 0
            Dim strPseudoAdd As String = ""
            Dim strSucursalAdd As String = ""
            Dim strFileAdd As String = ""
            Dim strComprobanteAdd As String = ""

            Dim intNroBoletoSucursal As Integer = 0
            Dim intNroBoletoPseudo As Integer = 0
            Dim intNroBoletoFile As Integer = 0
            Dim intNroBoletoComprobante As Integer = 0

            Dim intPosPseudo As Integer = 0
            Dim intPosSucursal As Integer = 0
            Dim intPosFile As Integer = 0
            Dim intPosComprobante As Integer = 0

            strCadenaReporte.Append("<table cellSpacing='0' cellPadding='3' border='0' width='100%' style='background-color:White;border:1px #CC9966 solid;border-collapse:collapse;'>" & vbCrLf)
            strCadenaReporte.Append("<tr>" & vbCrLf)
            If strValor = "AVI_A" Then
                strCadenaReporte.Append("<td height='30' class='tarifario_header' align='center'>OFICINA</td>" & vbCrLf)
            ElseIf strDirigidoA <> "Agencia" Then
                strCadenaReporte.Append("<td height='30' class='tarifario_header' align='center'>PSEUDO</td>" & vbCrLf)
            End If

            If strValor = "AVI" And strDirigidoA = "Agencia" Then
                strCadenaReporte.Append("<td height='30' class='tarifario_header' align='center'>PSEUDO</td>" & vbCrLf)
            End If

            strCadenaReporte.Append("<td class='tarifario_header' align='center'>SUCURSAL</td>" & vbCrLf)
            strCadenaReporte.Append("<td class='tarifario_header' align='center'>FILE</td>" & vbCrLf)
            strCadenaReporte.Append("<td class='tarifario_header' align='center'>COMPROBANTE</td>" & vbCrLf)
            'If strDirigidoA = "Counter" Or strDirigidoA = "Caja" Then
            If strDirigidoA = "Counter" Or strDirigidoA = "Caja" Then
                strCadenaReporte.Append("<td height='30' class='tarifario_header' align='center'>DK</td>" & vbCrLf)
            End If

            If strValor = "AVI" And strDirigidoA = "Agencia" Then
                strCadenaReporte.Append("<td height='30' class='tarifario_header' align='center'>DK</td>" & vbCrLf)
            End If

            strCadenaReporte.Append("<td class='tarifario_header' align='center'>PNR</td>" & vbCrLf)
            strCadenaReporte.Append("<td class='tarifario_header' align='center'>N&Uacute;MERO BOLETO</td>" & vbCrLf)
            strCadenaReporte.Append("<td class='tarifario_header' align='center'>NOMBRE PASAJERO</td>" & vbCrLf)
            strCadenaReporte.Append("<td class='tarifario_header' align='center'>PROMOTOR</td>" & vbCrLf)
            strCadenaReporte.Append("<td class='tarifario_header' align='center'>DEUDA PENDIENTE</td>" & vbCrLf)
            'strCadenaReporte.Append("<td class='tarifario_header' align='center'>CORREO PROMOTOR</td>" & vbCrLf)
            'strCadenaReporte.Append("<td class='tarifario_header' align='center'>CORREO CAJA</td>" & vbCrLf)
            'strCadenaReporte.Append("<td class='tarifario_header' align='center'>CORREO JEFE</td>" & vbCrLf)
            strCadenaReporte.Append("<td class='tarifario_header' align='center'>ESTADO</td>" & vbCrLf)

            If strValor = "NOVOIDSABRE" Or strValor = "NOVOIDPTA" Or strValor = "NOVOIDREST" Then
                strCadenaReporte.Append("<td class='tarifario_header' align='center'>MENSAJE ERROR</td>" & vbCrLf)
            End If
            strCadenaReporte.Append("</tr>" & vbCrLf)

            Dim lstBoletosXPseudo = (From x In (From b In lstBoletos
                                     Group b By b.Pseudo Into Cantidad = Count(b.NumeroBoleto)) Order By x.Pseudo Select x).ToList

            Dim lstBoletosXSucursal = (From x In (From b In lstBoletos
                                       Group b By b.Pseudo, b.Descripcion Into Cantidad = Count(b.NumeroBoleto)) Order By x.Pseudo, x.Descripcion Select x).ToList

            Dim lstBoletosXFile = (From x In (From b In lstBoletos
                                   Group b By b.Pseudo, b.Descripcion, b.File Into Cantidad = Count(b.NumeroBoleto)) Order By x.Pseudo, x.Descripcion, x.File Select x).ToList

            Dim lstBoletosXComprobante = (From x In (From b In lstBoletos
                                          Group b By b.Pseudo, b.Descripcion, b.File, b.Comprobante Into Cantidad = Count(b.NumeroBoleto)) Order By x.Pseudo, x.Descripcion, x.File, x.Comprobante Select x).ToList

            lstBoletos = (From m In lstBoletos
                         Order By m.Pseudo, m.Descripcion, m.File, m.Comprobante
                         Select m).ToList



            For j As Integer = 0 To lstBoletos.Count - 1
                strDetalleBoletos1 = New System.Text.StringBuilder
                EstiloClase = " class='tarifario_fila_a' align='center'"

                strDetalleBoletos1.Append("<tr>" & vbCrLf)
                'If strDirigidoA <> "Agencia" Then
                If strDirigidoA <> "Agencia" Then
                    If intContadorPseudo < lstBoletosXPseudo.count Then
                        If j = 0 Then
                            If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXPseudo.Item(intContadorPseudo).Pseudo)) Then
                                strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXPseudo(intContadorPseudo).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).Pseudo & "</td>" & vbCrLf)
                                strPseudoAdd = lstBoletos.Item(j).Pseudo
                                intPosPseudo = j
                                intNroBoletoPseudo = lstBoletosXPseudo(intContadorPseudo).Cantidad
                            End If
                        Else
                            If j < intPosPseudo + intNroBoletoPseudo Then
                                'nada
                            Else
                                intContadorPseudo += 1
                                strPseudoAdd = ""
                                intNroBoletoPseudo = 0
                                If intContadorPseudo < lstBoletosXPseudo.count Then
                                    If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXPseudo.Item(intContadorPseudo).Pseudo)) Then
                                        strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXPseudo(intContadorPseudo).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).Pseudo & "</td>" & vbCrLf)
                                        strPseudoAdd = lstBoletos.Item(j).Pseudo
                                        intPosPseudo = j
                                        intNroBoletoPseudo = lstBoletosXPseudo(intContadorPseudo).Cantidad
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If

                If strValor = "AVI" And strDirigidoA = "Agencia" Then
                    If intContadorPseudo < lstBoletosXPseudo.count Then
                        If j = 0 Then
                            If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXPseudo.Item(intContadorPseudo).Pseudo)) Then
                                strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXPseudo(intContadorPseudo).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).Pseudo & "</td>" & vbCrLf)
                                strPseudoAdd = lstBoletos.Item(j).Pseudo
                                intPosPseudo = j
                                intNroBoletoPseudo = lstBoletosXPseudo(intContadorPseudo).Cantidad
                            End If
                        Else
                            If j < intPosPseudo + intNroBoletoPseudo Then
                                'nada
                            Else
                                intContadorPseudo += 1
                                strPseudoAdd = ""
                                intNroBoletoPseudo = 0
                                If intContadorPseudo < lstBoletosXPseudo.count Then
                                    If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXPseudo.Item(intContadorPseudo).Pseudo)) Then
                                        strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXPseudo(intContadorPseudo).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).Pseudo & "</td>" & vbCrLf)
                                        strPseudoAdd = lstBoletos.Item(j).Pseudo
                                        intPosPseudo = j
                                        intNroBoletoPseudo = lstBoletosXPseudo(intContadorPseudo).Cantidad
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If


                If intContadorSucursal < lstBoletosXSucursal.Count Then
                    If j = 0 Then
                        If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXSucursal.Item(intContadorSucursal).Pseudo)) And (lstBoletos.Item(j).Descripcion.Equals(lstBoletosXSucursal.Item(intContadorSucursal).Descripcion)) Then
                            strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXSucursal(intContadorSucursal).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).Descripcion & "</td>" & vbCrLf)
                            strSucursalAdd = lstBoletos.Item(j).Pseudo & lstBoletos.Item(j).Descripcion
                            intNroBoletoSucursal = lstBoletosXSucursal(intContadorSucursal).Cantidad
                            intPosSucursal = j
                        End If
                    Else
                        If j < intPosSucursal + intNroBoletoSucursal Then

                        Else
                            intContadorSucursal += 1
                            strSucursalAdd = ""
                            intNroBoletoSucursal = 0
                            If intContadorSucursal < lstBoletosXSucursal.count Then
                                If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXSucursal.Item(intContadorSucursal).Pseudo)) And (lstBoletos.Item(j).Descripcion.Equals(lstBoletosXSucursal.Item(intContadorSucursal).Descripcion)) Then
                                    strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXSucursal(intContadorSucursal).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).Descripcion & "</td>" & vbCrLf)
                                    strSucursalAdd = lstBoletos.Item(j).Pseudo & lstBoletos.Item(j).Descripcion
                                    intNroBoletoSucursal = lstBoletosXSucursal(intContadorSucursal).Cantidad
                                    intPosSucursal = j
                                End If
                            End If
                        End If
                    End If
                End If

                If intContadorFile < lstBoletosXFile.Count Then
                    If j = 0 Then
                        If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXFile.Item(intContadorFile).Pseudo)) And (lstBoletos.Item(j).Descripcion.Equals(lstBoletosXFile.Item(intContadorFile).Descripcion)) And (lstBoletos.Item(j).File.Equals(lstBoletosXFile.Item(intContadorFile).File)) Then
                            strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXFile(intContadorFile).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).File & "</td>" & vbCrLf)
                            strFileAdd = lstBoletos.Item(j).Pseudo & lstBoletos.Item(j).Descripcion & lstBoletos.Item(j).File
                            intNroBoletoFile = lstBoletosXFile(intContadorFile).Cantidad
                            intPosFile = j
                        End If
                    Else
                        If j < intPosFile + intNroBoletoFile Then

                        Else
                            intContadorFile += 1
                            strFileAdd = ""
                            intNroBoletoFile = 0
                            If intContadorFile < lstBoletosXFile.count Then
                                If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXFile.Item(intContadorFile).Pseudo)) And (lstBoletos.Item(j).Descripcion.Equals(lstBoletosXFile.Item(intContadorFile).Descripcion)) And (lstBoletos.Item(j).File.Equals(lstBoletosXFile.Item(intContadorFile).File)) Then
                                    strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXFile(intContadorFile).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).File & "</td>" & vbCrLf)
                                    strFileAdd = lstBoletos.Item(j).Pseudo & lstBoletos.Item(j).Descripcion & lstBoletos.Item(j).File
                                    intNroBoletoFile = lstBoletosXFile(intContadorFile).Cantidad
                                    intPosFile = j
                                End If
                            End If
                        End If
                    End If

                End If

                If intContadorComprobante < lstBoletosXComprobante.Count Then
                    If j = 0 Then
                        If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXComprobante.Item(intContadorComprobante).Pseudo)) And (lstBoletos.Item(j).Descripcion.Equals(lstBoletosXComprobante.Item(intContadorComprobante).Descripcion)) And (lstBoletos.Item(j).File.Equals(lstBoletosXComprobante.Item(intContadorComprobante).File)) And (lstBoletos.Item(j).Comprobante.Equals(lstBoletosXComprobante.Item(intContadorComprobante).Comprobante)) Then
                            strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXComprobante(intContadorComprobante).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).Comprobante & "</td>" & vbCrLf)
                            strComprobanteAdd = lstBoletos.Item(j).Pseudo & lstBoletos.Item(j).Descripcion & lstBoletos.Item(j).File & lstBoletos.Item(j).Comprobante
                            intNroBoletoComprobante = lstBoletosXComprobante(intContadorComprobante).Cantidad
                            intPosComprobante = j
                        End If
                    Else
                        If j < intPosComprobante + intNroBoletoComprobante Then

                        Else
                            intContadorComprobante += 1
                            strFileAdd = ""
                            intNroBoletoComprobante = 0
                            If (lstBoletos.Item(j).Pseudo.Equals(lstBoletosXComprobante.Item(intContadorComprobante).Pseudo)) And (lstBoletos.Item(j).Descripcion.Equals(lstBoletosXComprobante.Item(intContadorComprobante).Descripcion)) And (lstBoletos.Item(j).File.Equals(lstBoletosXComprobante.Item(intContadorComprobante).File)) And (lstBoletos.Item(j).Comprobante.Equals(lstBoletosXComprobante.Item(intContadorComprobante).Comprobante)) Then
                                strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXComprobante(intContadorComprobante).Cantidad & "'" & EstiloClase & ">" & lstBoletos.Item(j).Comprobante & "</td>" & vbCrLf)
                                strComprobanteAdd = lstBoletos.Item(j).Pseudo & lstBoletos.Item(j).Descripcion & lstBoletos.Item(j).File & lstBoletos.Item(j).Comprobante
                                intNroBoletoComprobante = lstBoletosXComprobante(intContadorComprobante).Cantidad
                                intPosComprobante = j
                            End If
                        End If
                    End If

                End If

                If strDirigidoA = "Counter" Or strDirigidoA = "Caja" Then
                    strDetalleBoletos1.Append("<td " & EstiloClase & ">" & lstBoletos.Item(j).Cliente.DK & "</td>" & vbCrLf)
                End If
                If strValor = "AVI" And strDirigidoA = "Agencia" Then
                    strDetalleBoletos1.Append("<td " & EstiloClase & ">" & lstBoletos.Item(j).Cliente.DK & "</td>" & vbCrLf)
                End If

                'strDetalleBoletos1.Append("<td height='30'" & EstiloClase & ">" & lstBoletos.Item(j).Pseudo & "</td>" & vbCrLf)
                'strDetalleBoletos1.Append("<td" & EstiloClase & ">" & lstBoletos.Item(j).Descripcion & "</td>" & vbCrLf)
                'strDetalleBoletos1.Append("<td" & EstiloClase & ">" & lstBoletos.Item(j).File & "</td>" & vbCrLf)
                'strDetalleBoletos1.Append("<td" & EstiloClase & ">" & lstBoletos.Item(j).IdTipoComprobante & "-" & lstBoletos.Item(j).NumeroSerie1 & "-" & lstBoletos.Item(j).ID_FacturaCabeza & "</td>" & vbCrLf)
                strDetalleBoletos1.Append("<td height='30' " & EstiloClase & ">" & lstBoletos.Item(j).PNR & "</td>" & vbCrLf)
                strDetalleBoletos1.Append("<td " & EstiloClase & ">" & lstBoletos.Item(j).PrefijoBoleto & lstBoletos.Item(j).NumeroBoleto & "</td>" & vbCrLf)
                strDetalleBoletos1.Append("<td " & EstiloClase & ">" & lstBoletos.Item(j).NombrePasajero & "</td>" & vbCrLf)
                strDetalleBoletos1.Append("<td " & EstiloClase & ">" & lstBoletos.Item(j).Promotor.NombrePromotor & "</td>" & vbCrLf)
                strDetalleBoletos1.Append("<td " & EstiloClase & ">" & (Val(lstBoletos.Item(j).MontoPendiente) - Val(lstBoletos.Item(j).MontoOtroDK)).ToString.Replace(",", ".") & "</td>" & vbCrLf)
                strDetalleBoletos1.Append("<td " & EstiloClase & ">" & lstBoletos.Item(j).Estado & "</td>" & vbCrLf)

                If strValor = "NOVOIDSABRE" Or strValor = "NOVOIDPTA" Or strValor = "NOVOIDREST" Then
                    strDetalleBoletos1.Append("<td " & EstiloClase & ">" & lstBoletos.Item(j).MensajeError & "</td>" & vbCrLf)
                End If

                strDetalleBoletos1.Append("</tr>" & vbCrLf)
                strCadenaReporte.Append(strDetalleBoletos1)
                'End If
                'Next
            Next
            strCadenaReporte.Append("</table>" & vbCrLf)

            Return strCadenaReporte
        End Function

        Public Function EnviarCorreoReporteXFirma(ByVal lstBoletosXFirma As List(Of robotBoletoPendientePago.robotBoletoPendiente), ByVal strDirigidoA As String, ByVal Valor As String, ByVal Asunto As String, ByVal strCodigoSeguimiento As String) As Boolean
            Dim strCadenaReporte As New System.Text.StringBuilder
            Dim strDetalleBoletos1 As New System.Text.StringBuilder
            Dim objEnviarEmail As EnviarEmail = Nothing
            Dim objCuerpoCorreo As cuerpoCorreo = Nothing
            Dim objCorreo As classCorreo = Nothing
            'Dim Asunto As String = String.Empty
            Dim CorreoVendedor As String = String.Empty
            Dim bolRespuesta As Boolean = False
            Dim CorreoJefe As String = String.Empty
            Dim intContadorPseudo As Integer = 0
            Dim strPseudoAdd As String = ""
            Dim intPosPseudo As Integer = 0
            Dim intNroBoletoPseudo As Integer = 0
            Dim intContadorSucursal As Integer = 0
            Dim intContadorFile As Integer = 0
            Dim intPosSucursal As Integer = 0
            Dim intNroBoletoSucursal As Integer = 0
            Dim strSucursalAdd As String = ""
            Dim strFileAdd As String = ""
            Dim intNroBoletoFile As Integer = 0
            Dim intPosFile As Integer = 0
            Dim EstiloClase As String = String.Empty
            Dim EstiloClase_c As String = " class='tarifario_fila_c' align='center'"
            Try
                CorreoVendedor = lstBoletosXFirma.Item(0).Vendedor.CorreoVendedor
                CorreoJefe = lstBoletosXFirma.Item(0).Vendedor.CorreoJefe

                strCadenaReporte.Append("<br><table cellSpacing='0' cellPadding='3' border='0' width='100%' >" & vbCrLf)
                strCadenaReporte.Append("<tr>" & vbCrLf)
                strCadenaReporte.Append("<td class='textContenido'>" & vbCrLf)
                strCadenaReporte.Append("Fecha / Hora : " & Date.Now & vbCrLf)
                strCadenaReporte.Append("</td>" & vbCrLf)
                strCadenaReporte.Append("</tr>" & vbCrLf)
                If Valor = "NBD" Or Valor = "NBD_A" Then
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td class='textContenidoROBOT'>" & vbCrLf)

                    If strDirigidoA = "Counter" Then
                        strCadenaReporte.Append("Estimado(a): " & lstBoletosXFirma.Item(0).Vendedor.NombreVendedor & ", " & vbCrLf)
                    ElseIf strDirigidoA = "Promotor" Then
                        strCadenaReporte.Append("Estimado(a): " & lstBoletosXFirma.Item(0).Promotor.NombrePromotor & vbCrLf)
                    ElseIf strDirigidoA = "Agencia" Then
                        strCadenaReporte.Append("Estimado(a): " & lstBoletosXFirma.Item(0).Cliente.NombreComercial & vbCrLf)
                    ElseIf strDirigidoA = "Caja" Then
                        strCadenaReporte.Append("Atención: " & lstBoletosXFirma.Item(0).Descripcion & vbCrLf)
                    End If
                    strCadenaReporte.Append("</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td class='textContenido'>" & vbCrLf)
                    strCadenaReporte.Append("Se hace de tu conocimiento que los siguientes boletos emitidos no se encuentran en PTA - BACKOFFICE. " & vbCrLf)
                    strCadenaReporte.Append("</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)
                ElseIf Valor = "NFA" Or Valor = "NFA_AYER" Or Valor = "NFA_A" Or Valor = "NFA_AYER_A" Then
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td class='textContenidoROBOT'>" & vbCrLf)
                    strCadenaReporte.Append("Estimado(a): " & lstBoletosXFirma.Item(0).Vendedor.NombreVendedor & ", " & vbCrLf)
                    strCadenaReporte.Append("</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td class='textContenido'>" & vbCrLf)
                    If Valor = "NFA" Or Valor = "NFA_A" Then
                        strCadenaReporte.Append("Se hace de tu conocimiento que los siguientes boletos emitidos no están facturados. " & vbCrLf)
                    Else
                        strCadenaReporte.Append("Se hace de tu conocimiento que los siguientes boletos emitidos el día de ayer aún no están facturados. " & vbCrLf)
                    End If
                    strCadenaReporte.Append("</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)
                End If
                strCadenaReporte.Append("<tr>" & vbCrLf)
                strCadenaReporte.Append("<td class='textContenido'>" & vbCrLf)

                If CorreoVendedor = "helpdesk@nmviajes.com" And CorreoJefe = "helpdesk@nmviajes.com" Then
                    strCadenaReporte.Append("ID Firma : " & lstBoletosXFirma.Item(0).Vendedor.FirmaAgente & vbCrLf)
                    If CorreoVendedor = "helpdesk@nmviajes.com" Then
                        strCadenaReporte.Append("El Vendedor : " & lstBoletosXFirma.Item(0).Vendedor.NombreVendedor & " no tiene asignado un correo." & vbCrLf)
                        Asunto = "El Vendedor No registra correo " & CorreoVendedor
                    ElseIf CorreoJefe = "helpdesk@nmviajes.com" Then
                        strCadenaReporte.Append("El Jefe del vendedor : " & lstBoletosXFirma.Item(0).Vendedor.NombreVendedor & " no tiene asignado un correo." & vbCrLf)
                        Asunto = "El Vendedor No registra correo " & CorreoJefe
                    End If
                Else
                    strCadenaReporte.Append("<table cellSpacing='0' cellPadding='3' border='0' width='100%' style='background-color:White;border:1px #CC9966 solid;border-collapse:collapse;'>" & vbCrLf)
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    If Valor = "NFA_A" Or Valor = "NFA_AYER_A" Or Valor = "NBD_A" Then
                        strCadenaReporte.Append("<td class='tarifario_header' align='center'>OFICINA</td>" & vbCrLf)
                    Else
                        strCadenaReporte.Append("<td class='tarifario_header' align='center'>PSEUDO</td>" & vbCrLf)
                    End If

                    If Valor = "NFA" Or Valor = "NFA_AYER" Or Valor = "NFA_A" Or Valor = "NFA_AYER_A" Then
                        strCadenaReporte.Append("<td class='tarifario_header' align='center'>SUCURSAL</td>" & vbCrLf)
                        strCadenaReporte.Append("<td class='tarifario_header' align='center'>FILE</td>" & vbCrLf)
                    End If
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>PNR</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>N&Uacute;MERO BOLETO</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>NOMBRE PASAJERO</td>" & vbCrLf)
                    If Valor = "NFA" Or Valor = "NFA_AYER" Or Valor = "NFA_A" Or Valor = "NFA_AYER_A" Then
                        strCadenaReporte.Append("<td class='tarifario_header' align='center'>PROMOTOR</td>" & vbCrLf)
                    End If
                    'strCadenaReporte.Append("<td class='tarifario_header' align='center'>CORREO VENDEDOR</td>" & vbCrLf)
                    'strCadenaReporte.Append("<td class='tarifario_header' align='center'>JEFE VENDEDOR</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>ESTADO</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)

                    Dim lstBoletosXPseudo = (From x In (From b In lstBoletosXFirma
                             Group b By b.Pseudo Into Cantidad = Count(b.NumeroBoleto)) Order By x.Pseudo Select x).ToList

                    Dim lstBoletosXSucursal = (From x In (From b In lstBoletosXFirma
                                               Group b By b.Pseudo, b.Descripcion Into Cantidad = Count(b.NumeroBoleto)) Order By x.Pseudo, x.Descripcion Select x).ToList

                    Dim lstBoletosXFile = (From x In (From b In lstBoletosXFirma
                                           Group b By b.Pseudo, b.Descripcion, b.File Into Cantidad = Count(b.NumeroBoleto)) Order By x.Pseudo, x.Descripcion, x.File Select x).ToList

                    lstBoletosXFirma = (From m In lstBoletosXFirma
                                 Order By m.Pseudo, m.Descripcion, m.File
                                 Select m).ToList

                    For j As Integer = 0 To lstBoletosXFirma.Count - 1
                        strDetalleBoletos1 = New System.Text.StringBuilder
                        EstiloClase = " class='tarifario_fila_a' align='center'"
                        strDetalleBoletos1.Append("<tr>" & vbCrLf)

                        If intContadorPseudo < lstBoletosXPseudo.count Then
                            If j = 0 Then
                                If (lstBoletosXFirma.Item(j).Pseudo.Equals(lstBoletosXPseudo.Item(intContadorPseudo).Pseudo)) Then
                                    strDetalleBoletos1.Append("<td height='30' rowspan='" & lstBoletosXPseudo(intContadorPseudo).Cantidad & "'" & EstiloClase & ">" & lstBoletosXFirma.Item(j).Pseudo & "</td>" & vbCrLf)
                                    strPseudoAdd = lstBoletosXFirma.Item(j).Pseudo
                                    intPosPseudo = j
                                    intNroBoletoPseudo = lstBoletosXPseudo(intContadorPseudo).Cantidad
                                End If
                            Else
                                If j < intPosPseudo + intNroBoletoPseudo Then
                                    'nada
                                Else
                                    intContadorPseudo += 1
                                    strPseudoAdd = ""
                                    intNroBoletoPseudo = 0
                                    If intContadorPseudo < lstBoletosXPseudo.count Then
                                        If (lstBoletosXFirma.Item(j).Pseudo.Equals(lstBoletosXPseudo.Item(intContadorPseudo).Pseudo)) Then
                                            strDetalleBoletos1.Append("<td height='30' rowspan='" & lstBoletosXPseudo(intContadorPseudo).Cantidad & "'" & EstiloClase & ">" & lstBoletosXFirma.Item(j).Pseudo & "</td>" & vbCrLf)
                                            strPseudoAdd = lstBoletosXFirma.Item(j).Pseudo
                                            intPosPseudo = j
                                            intNroBoletoPseudo = lstBoletosXPseudo(intContadorPseudo).Cantidad
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        'strDetalleBoletos1.Append("<td height='30'" & EstiloClase & ">" & lstBoletosXFirma.Item(j).Pseudo & "</td>" & vbCrLf)
                        If Valor = "NFA" Or Valor = "NFA_AYER" Or Valor = "NFA_A" Or Valor = "NFA_AYER_A" Then
                            'strDetalleBoletos1.Append("<td" & EstiloClase & ">" & lstBoletosXFirma.Item(j).Descripcion & "</td>" & vbCrLf)
                            'strDetalleBoletos1.Append("<td" & EstiloClase & ">" & lstBoletosXFirma.Item(j).File & "</td>" & vbCrLf)
                            If intContadorSucursal < lstBoletosXSucursal.Count Then
                                If j = 0 Then
                                    If (lstBoletosXFirma.Item(j).Pseudo.Equals(lstBoletosXSucursal.Item(intContadorSucursal).Pseudo)) And (lstBoletosXFirma.Item(j).Descripcion.Equals(lstBoletosXSucursal.Item(intContadorSucursal).Descripcion)) Then
                                        strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXSucursal(intContadorSucursal).Cantidad & "'" & EstiloClase & ">" & lstBoletosXFirma.Item(j).Descripcion & "</td>" & vbCrLf)
                                        strSucursalAdd = lstBoletosXFirma.Item(j).Pseudo & lstBoletosXFirma.Item(j).Descripcion
                                        intNroBoletoSucursal = lstBoletosXSucursal(intContadorSucursal).Cantidad
                                        intPosSucursal = j
                                    End If
                                Else
                                    If j < intPosSucursal + intNroBoletoSucursal Then

                                    Else
                                        intContadorSucursal += 1
                                        strSucursalAdd = ""
                                        intNroBoletoSucursal = 0
                                        If intContadorSucursal < lstBoletosXSucursal.count Then
                                            If (lstBoletosXFirma.Item(j).Pseudo.Equals(lstBoletosXSucursal.Item(intContadorSucursal).Pseudo)) And (lstBoletosXFirma.Item(j).Descripcion.Equals(lstBoletosXSucursal.Item(intContadorSucursal).Descripcion)) Then
                                                strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXSucursal(intContadorSucursal).Cantidad & "'" & EstiloClase & ">" & lstBoletosXFirma.Item(j).Descripcion & "</td>" & vbCrLf)
                                                strSucursalAdd = lstBoletosXFirma.Item(j).Pseudo & lstBoletosXFirma.Item(j).Descripcion
                                                intNroBoletoSucursal = lstBoletosXSucursal(intContadorSucursal).Cantidad
                                                intPosSucursal = j
                                            End If
                                        End If
                                    End If
                                End If
                            End If

                            If intContadorFile < lstBoletosXFile.Count Then
                                If j = 0 Then
                                    If (lstBoletosXFirma.Item(j).Pseudo.Equals(lstBoletosXFile.Item(intContadorFile).Pseudo)) And (lstBoletosXFirma.Item(j).Descripcion.Equals(lstBoletosXFile.Item(intContadorFile).Descripcion)) And (lstBoletosXFirma.Item(j).File.Equals(lstBoletosXFile.Item(intContadorFile).File)) Then
                                        strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXFile(intContadorFile).Cantidad & "'" & EstiloClase & ">" & lstBoletosXFirma.Item(j).File & "</td>" & vbCrLf)
                                        strFileAdd = lstBoletosXFirma.Item(j).Pseudo & lstBoletosXFirma.Item(j).Descripcion & lstBoletosXFirma.Item(j).File
                                        intNroBoletoFile = lstBoletosXFile(intContadorFile).Cantidad
                                        intPosFile = j
                                    End If
                                Else
                                    If j < intPosFile + intNroBoletoFile Then

                                    Else
                                        intContadorFile += 1
                                        strFileAdd = ""
                                        intNroBoletoFile = 0
                                        If intContadorFile < lstBoletosXFile.count Then
                                            If (lstBoletosXFirma.Item(j).Pseudo.Equals(lstBoletosXFile.Item(intContadorFile).Pseudo)) And (lstBoletosXFirma.Item(j).Descripcion.Equals(lstBoletosXFile.Item(intContadorFile).Descripcion)) And (lstBoletosXFirma.Item(j).File.Equals(lstBoletosXFile.Item(intContadorFile).File)) Then
                                                strDetalleBoletos1.Append("<td rowspan='" & lstBoletosXFile(intContadorFile).Cantidad & "'" & EstiloClase & ">" & lstBoletosXFirma.Item(j).File & "</td>" & vbCrLf)
                                                strFileAdd = lstBoletosXFirma.Item(j).Pseudo & lstBoletosXFirma.Item(j).Descripcion & lstBoletosXFirma.Item(j).File
                                                intNroBoletoFile = lstBoletosXFile(intContadorFile).Cantidad
                                                intPosFile = j
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If
                        strDetalleBoletos1.Append("<td height='30' " & EstiloClase & ">" & lstBoletosXFirma.Item(j).PNR & "</td>" & vbCrLf)
                        strDetalleBoletos1.Append("<td" & EstiloClase & ">" & lstBoletosXFirma.Item(j).PrefijoBoleto & lstBoletosXFirma.Item(j).NumeroBoleto & "</td>" & vbCrLf)
                        strDetalleBoletos1.Append("<td" & EstiloClase & ">" & lstBoletosXFirma.Item(j).NombrePasajero & "</td>" & vbCrLf)
                        If Valor = "NFA" Or Valor = "NFA_AYER" Or Valor = "NFA_A" Or Valor = "NFA_AYER_A" Then
                            strDetalleBoletos1.Append("<td" & EstiloClase & ">" & lstBoletosXFirma.Item(j).Promotor.NombrePromotor & "</td>" & vbCrLf)
                        End If
                        strDetalleBoletos1.Append("<td" & EstiloClase & ">" & lstBoletosXFirma.Item(j).Estado & "</td>" & vbCrLf)
                        strDetalleBoletos1.Append("</tr>" & vbCrLf)
                        strCadenaReporte.Append(strDetalleBoletos1)
                    Next
                    strCadenaReporte.Append("</table>" & vbCrLf)
                    strCadenaReporte.Append("</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)
                    strCadenaReporte.Append("</table><br>" & vbCrLf)
                End If

                If Not String.IsNullOrEmpty(strCadenaReporte.ToString) Then
                    objCorreo = New classCorreo
                    objCorreo.ToCorreo = CorreoVendedor & Constantes.PuntoComa & CorreoJefe
                    If Valor = "NFA_AYER" Then
                        objCorreo.BCCCorreo = Constantes.emailDestinatariosNoFacturadosAyer + ";" + Constantes.emailRobotAlertas
                    Else
                        objCorreo.BCCCorreo = Constantes.emailRobotAlertas
                    End If


                    'objCorreo.NombreCorreo = "Robot Alertas Sabre - " & strDirigidoA
                    objCorreo.NombreCorreo = "Alertas GDS [" & lstBoletosXFirma(0).Pseudo & "] - " & strDirigidoA
                    objCorreo.SubjectCorreo = Asunto
                    objCuerpoCorreo = New cuerpoCorreo
                    objCorreo.BodyCorreo = objCuerpoCorreo.GeneraCuerpoRobot(strCadenaReporte.ToString)
                    If esPrueba = "1" Then
                        objCorreo.ToCorreo = MailPrueba
                        objCorreo.CCCorreo = ""
                        objCorreo.BCCCorreo = ""
                    End If

                    objEnviarEmail = New EnviarEmail
                    objCorreo.FromCorreo = lstBoletosXFirma(0).Pseudo & ".alertas@expertiatravel.com"
                    objEnviarEmail.Send(objCorreo, True, strCodigoSeguimiento, 8, Nothing)
                    bolRespuesta = True
                End If
                Return bolRespuesta
            Catch ex As Exception

            End Try
        End Function

        Public Function sendSabreCommand(ByVal objBO As BO,
                                         ByVal command As String,
                                         ByRef lstLog As List(Of String),
                                         ByVal codigoSeguimiento As String,
                                         ByVal intGDS As Integer,
                                         ByVal intFirmaGDS As Integer,
                                         ByVal intFirmaDB As Integer,
                                         ByVal objSession As classSession) As String
            If Not objBO Is Nothing Then
                lstLog.Add("Command : " + command)
                Return objBO.SabreCommand(command, "", codigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
            End If
        End Function

        Public Function Anular_BoletoEmitido1(ByVal lstBoleto As List(Of robotBoletoPendientePago.robotBoletoPendiente),
                                              ByVal strDirigidoA As String,
                                              ByVal Asunto As String,
                                              ByRef lstLog As List(Of String),
                                              ByVal strCodigoSeguimiento As String,
                                              ByVal intFirmaDB As Integer,
                                              ByVal intGDS As Integer,
                                              ByVal intFirmaGDS As Integer,
                                              ByVal intEsquema As Integer,
                                              ByVal objSession As classSession) As List(Of robotBoletoPendientePago.robotBoletoPendiente) 'As Boolean

            Dim firmaQuienAnula = ConfigurationSettings.AppSettings("VOIDEO_QUIEN_ANULA")
            Dim idMotivoAnulacion = ConfigurationSettings.AppSettings("VOIDEO_MOTIVO_ANULACION")

            Dim queueNumero = ConfigurationSettings.AppSettings("QUEUE_NUMERO")
            Dim queuePrefatoryInstruction = ConfigurationSettings.AppSettings("QUEUE_PREFATORY_INSTRUCTION_CODE")
            Dim queuePseudo = ConfigurationSettings.AppSettings("QUEUE_PSEUDO")

            'Validar Session
            Dim objBO As BO = Nothing
            Dim strRespuesta() As String = Nothing
            Dim strJSON As String = Nothing
            Dim objPNR As classPNR = Nothing
            '  Dim oWETR As String = Nothing
            '  Dim oWETRV As String = Nothing
            Dim responseCommand As String = Nothing
            Dim oRemark As String = Nothing
            Dim lstRemark As List(Of String) = Nothing
            Dim bolAnula As Boolean = False
            Dim bolEnviaCorreoAnulacion As Boolean = False
            Dim objTransaccion As classEndTransaction.classTransaction = Nothing
            '  Dim bolEstado As Boolean = True
            Dim strRptRemark As String() = Nothing
            Dim lstBoletosVoideado As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim lstBoletosNoVoideadoPTA As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim lstBoletosNoVoideado As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim lstBoletoDevueltos As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim strQueuePlace() As String = Nothing
            Try

                lstBoletosVoideado = New List(Of robotBoletoPendientePago.robotBoletoPendiente)
                lstBoletosNoVoideadoPTA = New List(Of robotBoletoPendientePago.robotBoletoPendiente)
                lstBoletosNoVoideado = New List(Of robotBoletoPendientePago.robotBoletoPendiente)
                lstBoletoDevueltos = New List(Of robotBoletoPendientePago.robotBoletoPendiente)

                For i As Integer = 0 To lstBoleto.Count - 1

                    objBO = New BO
                    objBO.SabreCommand("I", "", strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)

                    strRespuesta = objBO.CambiarPseudo(lstBoleto.Item(i).Pseudo, strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)

                    If strRespuesta IsNot Nothing Then
                        Dim SeEjecutoSCommand As Boolean = False
                        Try
                            lstLog.Add("====================================")
                            lstLog.Add("Recuperar PNR x ws : " + lstBoleto.Item(i).PNR)
                            objPNR = objBO.RecuperarPNRSinRestriccion(lstBoleto.Item(i).PNR, "", strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, intEsquema, objSession, True)
                        Catch ex As Exception
                            sendSabreCommand(objBO, "*" + lstBoleto.Item(i).PNR, lstLog, strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                            SeEjecutoSCommand = True
                        End Try

                        If objPNR IsNot Nothing Or SeEjecutoSCommand Then
                            If objPNR.MSGError Is Nothing Then
ANULACION:
                                responseCommand = sendSabreCommand(objBO, "WETR*T" & lstBoleto.Item(i).PrefijoBoleto & Trim(lstBoleto.Item(i).NumeroBoleto), lstLog, strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)

                                If responseCommand.IndexOf("UNABLE TO PROCESS CONTACT SABRE") > -1 Then
                                    GoTo error_anulacion
                                End If

                                responseCommand = sendSabreCommand(objBO, "WETRV", lstLog, strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                lstLog.Add("Response command : " + responseCommand)

                                If responseCommand.IndexOf("Simultaneous Changes to PNR has Occured") > -1 Or responseCommand.IndexOf("NO PNR PRESENT IN WORK AREA") > -1 Or responseCommand.IndexOf("UPDATE OR IGNORE") > -1 Then
                                    sendSabreCommand(objBO, "IG", lstLog, strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                    objPNR = objBO.RecuperarPNR(lstBoleto.Item(i).PNR, strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, intEsquema, objSession)
                                    GoTo ANULACION
                                ElseIf responseCommand.IndexOf("DISPLAY ETR FIRST") > -1 Then
                                    GoTo ANULACION
                                End If
                                If responseCommand.IndexOf("REENTER IF") > -1 Then
                                    responseCommand = sendSabreCommand(objBO, "WETRV", lstLog, strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                    lstLog.Add("Response command : " + responseCommand)
                                    If responseCommand.IndexOf("OK") > -1 Then
                                        oRemark = sendSabreCommand(objBO, "5H-**** BOLETO ANULADO ****", lstLog, strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                        lstRemark = New List(Of String)
                                        lstRemark.Add("BOLETO " & Trim(lstBoleto.Item(i).NumeroBoleto) & " FUE ANULADO POR ROBOT DE ANULACIONES")
                                        strRptRemark = objBO.AddRemark(lstRemark, "G", strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                        responseCommand = sendSabreCommand(objBO, "E", lstLog, strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                        lstLog.Add("Response command : " + responseCommand)
                                        If responseCommand.IndexOf("OK") > -1 Then
                                            lstLog.Add("Anular en PTA ..." + lstBoleto.Item(i).NumeroBoleto)
                                            bolAnula = AnularBoletoPTA1(lstBoleto.Item(i), firmaQuienAnula, idMotivoAnulacion, "1", 0, "", strCodigoSeguimiento, intFirmaDB, intEsquema)
                                            If bolAnula = True Then
                                                'desplegar el boleto
                                                lstLog.Add("Anulacion en PTA OK ...")
                                                Dim strPseudoRelease As String = ""
                                                Dim boolEjecutoSabreCommand As Boolean = False
                                                Try
                                                    objPNR = objBO.RecuperarPNRSinRestriccion(lstBoleto.Item(i).PNR, "", strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, intEsquema, objSession, True)
                                                Catch ex As Exception
                                                    objBO.SabreCommand("*" + lstBoleto.Item(i).PNR, "RecuperarReserva", strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                                    responseCommand = objBO.SabreCommand("*P6", "", strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                                    If responseCommand IsNot Nothing Then
                                                        strPseudoRelease = responseCommand.Split("\n")(1).Substring(0, 4)
                                                        boolEjecutoSabreCommand = True
                                                    End If
                                                End Try
                                                If objPNR IsNot Nothing Or boolEjecutoSabreCommand Then
                                                    'Se comenta la sgte linea porque Hugo usa el objPNR.PseudoRelease aunque no devuelve el mismo pseudo
                                                    'oWETRV = objBO.SabreCommand("*P6", "", strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                                    If objPNR IsNot Nothing Then
                                                        lstBoleto.Item(i).PseudoRelease = objPNR.PseudoRelease
                                                    Else
                                                        lstBoleto.Item(i).PseudoRelease = strPseudoRelease
                                                    End If
                                                    lstBoleto.Item(i).Estado = "VOID"
                                                    strQueuePlace = objBO.QueuePlace("1", queueNumero, queuePrefatoryInstruction, queuePseudo, strCodigoSeguimiento, intGDS, intFirmaGDS, intFirmaDB, objSession)
                                                End If
                                                lstBoletosVoideado.Add(lstBoleto.Item(i))
                                                lstLog.Add("Voideado : El boleto Nro. " & lstBoleto.Item(i).PrefijoBoleto & lstBoleto.Item(i).NumeroBoleto & " , PNR " & lstBoleto.Item(i).PNR & " , estado " & lstBoleto.Item(i).Estado & ", Pseudo " & lstBoleto.Item(i).Pseudo & ", pasajero " & lstBoleto.Item(i).NombrePasajero & ", Firma " & lstBoleto.Item(i).Vendedor.FirmaAgente)
                                            Else
                                                lstBoleto.Item(i).MensajeError = "Boleto no se voideó en PTA"
                                                lstBoletosNoVoideadoPTA.Add(lstBoleto.Item(i))
                                                lstLog.Add("No Voideado en PTA : El boleto Nro. " & lstBoleto.Item(i).PrefijoBoleto & lstBoleto.Item(i).NumeroBoleto & " , PNR " & lstBoleto.Item(i).PNR & " , estado " & lstBoleto.Item(i).Estado & ", Pseudo " & lstBoleto.Item(i).Pseudo & ", pasajero " & lstBoleto.Item(i).NombrePasajero & ", Firma " & lstBoleto.Item(i).Vendedor.FirmaAgente)
                                            End If
                                        Else
error_anulacion:
                                            lstBoleto.Item(i).MensajeError = responseCommand
                                            lstBoletosNoVoideado.Add(lstBoleto.Item(i))
                                            lstLog.Add("No Voideado : El boleto Nro. " & lstBoleto.Item(i).PrefijoBoleto & lstBoleto.Item(i).NumeroBoleto & " , PNR " & lstBoleto.Item(i).PNR & " , estado " & lstBoleto.Item(i).Estado & ", Pseudo " & lstBoleto.Item(i).Pseudo & ", pasajero " & lstBoleto.Item(i).NombrePasajero & ", Firma " & lstBoleto.Item(i).Vendedor.FirmaAgente & " por el sgte error: " & responseCommand)
                                        End If
                                    Else
                                        lstBoleto.Item(i).MensajeError = responseCommand
                                        lstBoletosNoVoideado.Add(lstBoleto.Item(i))
                                        lstLog.Add("No Voideado : El boleto Nro. " & lstBoleto.Item(i).PrefijoBoleto & lstBoleto.Item(i).NumeroBoleto & " , PNR " & lstBoleto.Item(i).PNR & " , estado " & lstBoleto.Item(i).Estado & ", Pseudo " & lstBoleto.Item(i).Pseudo & ", pasajero " & lstBoleto.Item(i).NombrePasajero & ", Firma " & lstBoleto.Item(i).Vendedor.FirmaAgente & " por el sgte error: " & responseCommand)
                                    End If
                                End If
                                Else
                                    For j As Integer = 0 To objPNR.MSGError.Count - 1
                                        If objPNR.MSGError(j).ToUpper.Contains("NO EXISTEN SEGMENTOS ACTIVOS EN EL PNR") Then
                                            GoTo ANULACION
                                        ElseIf objPNR.MSGError(j).ToUpper.Contains("RESTRICTED") Or objPNR.MSGError(j).Contains("SECURED") Then
                                            lstLog.Add("El boleto  " & lstBoleto.Item(i).PrefijoBoleto & lstBoleto.Item(i).NumeroBoleto & " , PNR " & lstBoleto.Item(i).PNR & " , estado " & lstBoleto.Item(i).Estado & ", Pseudo " & lstBoleto.Item(i).Pseudo & ", pasajero " & lstBoleto.Item(i).NombrePasajero & ", Firma " & lstBoleto.Item(i).Vendedor.FirmaAgente & " no puede ser voideado ya que fue devuelto")
                                            lstBoleto.Item(i).MensajeError = objPNR.MSGError(j).ToString '"PNR RESTRICTED"
                                            lstBoletoDevueltos.Add(lstBoleto.Item(i))
                                        Else
                                            lstLog.Add("El boleto  " & lstBoleto.Item(i).PrefijoBoleto & lstBoleto.Item(i).NumeroBoleto & " , PNR " & lstBoleto.Item(i).PNR & " , estado " & lstBoleto.Item(i).Estado & ", Pseudo " & lstBoleto.Item(i).Pseudo & ", pasajero " & lstBoleto.Item(i).NombrePasajero & ", Firma " & lstBoleto.Item(i).Vendedor.FirmaAgente & " tiene el sgte error " & objPNR.MSGError(j).ToString)
                                            lstBoleto.Item(i).MensajeError = objPNR.MSGError(j).ToString
                                            lstBoletoDevueltos.Add(lstBoleto.Item(i))
                                        End If
                                    Next
                                End If
                            End If
                        End If

                        'End If
                        'Else
                        '    lstBoletosVoideado.Add(lstBoleto.Item(i))
                        '    lstLog.Add("Voideado : El boleto Nro. " & lstBoleto.Item(i).PrefijoBoleto & lstBoleto.Item(i).NumeroBoleto & " , PNR " & lstBoleto.Item(i).PNR & " , estado " & lstBoleto.Item(i).Estado & ", Pseudo " & lstBoleto.Item(i).Pseudo & ", pasajero " & lstBoleto.Item(i).NombrePasajero & ", Firma " & lstBoleto.Item(i).Vendedor.FirmaAgente)

                        'End If
                Next

                Dim bolRspta As Boolean = False
                If lstBoletosVoideado IsNot Nothing Then
                    If lstBoletosVoideado.Count > 0 Then
                        bolRspta = EnviarCorreoAvisoVoideo(lstBoletosVoideado, strDirigidoA, "VOI", Asunto, strCodigoSeguimiento)
                    End If
                End If
                If lstBoletosNoVoideadoPTA IsNot Nothing Then
                    If lstBoletosNoVoideadoPTA.Count > 0 Then
                        EnviarCorreoAvisoVoideo(lstBoletosNoVoideadoPTA, strDirigidoA, "NOVOIDPTA", Asunto, strCodigoSeguimiento)
                        'EnviarCorreoAvisoErrorPromotor(lstBoletosNoVoideadoPTA, strCodigoSeguimiento)
                    End If
                End If
                If lstBoletosNoVoideado IsNot Nothing Then
                    If lstBoletosNoVoideado.Count > 0 Then
                        EnviarCorreoAvisoVoideo(lstBoletosNoVoideado, strDirigidoA, "NOVOIDSABRE", Asunto, strCodigoSeguimiento)
                        'EnviarCorreoAvisoErrorPromotor(lstBoletosNoVoideado, strCodigoSeguimiento)
                    End If
                End If
                If lstBoletoDevueltos IsNot Nothing Then
                    If lstBoletoDevueltos.Count > 0 Then
                        EnviarCorreoAvisoVoideo(lstBoletoDevueltos, strDirigidoA, "NOVOIDREST", Asunto, strCodigoSeguimiento)
                        'EnviarCorreoAvisoErrorPromotor(lstBoletoDevueltos, strCodigoSeguimiento)
                    End If
                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objBO = Nothing
                strRespuesta = Nothing
                strJSON = Nothing
                objPNR = Nothing
                oRemark = Nothing
                lstRemark = Nothing
                'bolEnviaCorreoAnulacion = Nothing
                objTransaccion = Nothing
                strRptRemark = Nothing

            End Try

            Return lstBoletosVoideado

        End Function

        Public Function ReporteBoletosEmitidos_X(ByVal strFecha As String, _
                                                 ByVal tipoGDS As Integer, _
                                                 ByVal proveedores As String, _
                                                 ByVal strCodigoSeguimiento As String, _
                                                 ByVal intFirmaDB As Integer, _
                                                 ByVal intEsquema As Integer) As List(Of robotBoletoPendientePago.robotBoletoPendiente)
            Dim objDAO As DAO = Nothing
            Dim objBoletoPendiente As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Try
                objDAO = New DAO
                objBoletoPendiente = objDAO.ObtenerBoletosEmitidos_X(strFecha, tipoGDS, proveedores, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally

                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objBoletoPendiente

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

            Dim objDAO As DAO = Nothing
            Dim bolRespuesta As Boolean = False

            Try
                objDAO = New DAO

                bolRespuesta = objDAO.AnularBoletoPTA1(objBoleto, _
                                                    strQuienAnula, _
                                                    strIdMotivoAnulacion, _
                                                    strFcVoidCliente, _
                                                    intConReposicion, _
                                                    strAutorizaVoideo, _
                                                    strCodigoSeguimiento, _
                                                    intFirmaDB, _
                                                    intEsquema)

            Catch ex As Exception
                bolRespuesta = False
                Throw New Exception(ex.ToString)

            Finally

                strQuienAnula = Nothing
                strIdMotivoAnulacion = Nothing
                strFcVoidCliente = Nothing
                intConReposicion = Nothing
                strAutorizaVoideo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return bolRespuesta

        End Function

        Public Function EnviarCorreoAvisoErrorPromotor(ByVal lstBoleto As List(Of robotBoletoPendientePago.robotBoletoPendiente), ByVal strCodigoSeguimiento As String) As Boolean
            Dim strCadenaReporte As System.Text.StringBuilder = Nothing
            Dim Asunto As String = Nothing
            Dim objEnviarEmail As EnviarEmail = Nothing
            Dim objCuerpoCorreo As cuerpoCorreo = Nothing
            Dim objCorreo As classCorreo = Nothing
            Dim strCorreoPromotor As String = Nothing
            Try
                If lstBoleto.Item(0).Promotor.EmailPromotor Is Nothing Then
                    strCorreoPromotor = Constantes.emailPromotor
                Else
                    strCorreoPromotor = lstBoleto.Item(0).Promotor.EmailPromotor
                End If
                For i As Integer = 0 To lstBoleto.Count - 1
                    strCadenaReporte = New System.Text.StringBuilder
                    Asunto = "NO PUDO ANULARSE el Boleto (" & lstBoleto.Item(i).NumeroBoleto & "), Cliente: " & lstBoleto.Item(i).Cliente.NombreComercial
                    strCadenaReporte.Append("<table cellSpacing='0' cellPadding='3' border='0' width='100%' >" & vbCrLf)
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td>" & vbCrLf)
                    strCadenaReporte.Append("<table cellSpacing='0' cellPadding='3' border='0' width='100%' >" & vbCrLf)
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td class='textContenido'>" & vbCrLf)
                    strCadenaReporte.Append("Fecha / Hora : " & Date.Now & vbCrLf)
                    strCadenaReporte.Append("</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td class='textContenido'>" & vbCrLf)
                    strCadenaReporte.Append("El robot de Turbo SABRE NO PUDO ANULAR el boleto No.  : " & lstBoleto.Item(i).NumeroBoleto & " del PNR: " & lstBoleto.Item(i).PNR & "<br>" & vbCrLf)
                    strCadenaReporte.Append("Mensaje GDS : " & lstBoleto.Item(i).MensajeError & "<br>" & vbCrLf)
                    strCadenaReporte.Append("por favor realizar la anulación a la brevedad posible para evitar futuros incovenientes. <br>" & vbCrLf)
                    strCadenaReporte.Append("</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)
                    strCadenaReporte.Append("</table>" & vbCrLf)
                    strCadenaReporte.Append("</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)
                    strCadenaReporte.Append("</table>" & vbCrLf)
                    If Not String.IsNullOrEmpty(strCadenaReporte.ToString) Then
                        objCorreo = New classCorreo
                        objCorreo.ToCorreo = Constantes.emailProgSabreWeb2
                        objCorreo.NombreCorreo = Asunto
                        objCorreo.SubjectCorreo = Asunto
                        objCuerpoCorreo = New cuerpoCorreo
                        objCorreo.BodyCorreo = objCuerpoCorreo.GeneraCuerpoRobot(strCadenaReporte.ToString)
                        objEnviarEmail = New EnviarEmail
                        objEnviarEmail.Send(objCorreo, True, strCodigoSeguimiento, 7, Nothing)
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCadenaReporte = Nothing
                Asunto = Nothing
                objEnviarEmail = Nothing
                objCuerpoCorreo = Nothing
                objCorreo = Nothing
            End Try

            Return True
        End Function

        Public Function ReporteBoletoEmitidoDQB(ByVal strFecha As String, _
                                                ByVal strCadenaPseudos As String, _
                                                ByVal strCodigoSeguimiento As String, _
                                                ByVal intGDS As Integer, _
                                                ByVal intFirmaGDS As Integer, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer, _
                                                ByVal objSession As classSession) As List(Of robotBoletoPendientePago.robotBoletoPendiente)

            If String.IsNullOrEmpty(strFecha) Then strFecha = Format(Now, Constantes.IWS_DATE_FORMAT_FILE2)
            Dim objReporteVentas As List(Of classReporteVentas) = Nothing
            Dim lstBoletosDQB As List(Of classReporteVentas.classBoleto) = Nothing
            Dim lstBoletoDQB As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim lstDatosVendedorPTA As List(Of classDatosAgente) = Nothing

            objReporteVentas = New List(Of classReporteVentas)
            lstBoletosDQB = New List(Of classReporteVentas.classBoleto)
            lstBoletoDQB = New List(Of robotBoletoPendientePago.robotBoletoPendiente)
            lstDatosVendedorPTA = New List(Of classDatosAgente)
            If String.IsNullOrEmpty(strCadenaPseudos) Then strCadenaPseudos = "QF05/S0X7/QP75/QQ05/HW57/QP35/94DH/S8S7"

            objReporteVentas = DQB(strFecha, _
                                   strCadenaPseudos, _
                                   strCodigoSeguimiento, _
                                   intGDS, _
                                   intFirmaGDS, _
                                   intFirmaDB, _
                                   objSession)

            lstDatosVendedorPTA = ObtenerVendedorPtaDestinos(strCodigoSeguimiento, intFirmaDB, intEsquema)

            If objReporteVentas IsNot Nothing Then
                Dim eBoleto As robotBoletoPendientePago.robotBoletoPendiente = Nothing
                Dim auxAgente As classDatosAgente = Nothing

                For i As Integer = 0 To objReporteVentas.Count - 1
                    If objReporteVentas(i).Pseudo = "QQ05" Then
                        lstBoletosDQB = objReporteVentas(i).Boletos
                        lstBoletosDQB = (From b In lstBoletosDQB
                                            Join p In lstDatosVendedorPTA On Right(b.Agente, 2) Equals p.FirmaAgente
                                            Select b).ToList
                    Else
                        lstBoletosDQB = objReporteVentas(i).Boletos
                    End If

                    For r As Integer = 0 To lstBoletosDQB.Count - 1
                        eBoleto = New robotBoletoPendientePago.robotBoletoPendiente
                        eBoleto.Pseudo = objReporteVentas(i).Pseudo
                        eBoleto.PrefijoBoleto = Left(lstBoletosDQB(r).NumBoleto, 3)
                        eBoleto.Hora = lstBoletosDQB(r).Hora
                        eBoleto.NombrePseudo = objReporteVentas(i).NombrePseudo
                        eBoleto.NumeroBoleto = Right(lstBoletosDQB(r).NumBoleto, 10)
                        eBoleto.PNR = lstBoletosDQB(r).PNR
                        eBoleto.NombrePasajero = lstBoletosDQB(r).NombrePasajero
                        auxAgente = New classDatosAgente()
                        auxAgente.FirmaAgente = lstBoletosDQB(r).Agente
                        eBoleto.Vendedor = auxAgente
                        eBoleto.Estado = lstBoletosDQB(r).Estado
                        lstBoletoDQB.Add(eBoleto)
                    Next
                Next
            End If
            Return lstBoletoDQB
        End Function

        Public Function ReporteBoletosPendientesPagoCajasRobot(ByVal srtFecha As String, _
                                                                ByVal strHora As String, _
                                                                ByVal strCodigoSeguimiento As String, _
                                                                ByVal intGDS As Integer, _
                                                                ByVal intFirmaGDS As Integer, _
                                                                ByVal intFirmaDB As Integer, _
                                                                ByVal intEsquema As Integer) As List(Of robotBoletoPendientePago.robotBoletoPendiente)

            Dim objDAO As DAO = Nothing
            '
            Dim objBoletoPendiente As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim objBoletoPagoOtroDk As List(Of robotBoletoPendientePago.robotBoletoPagoOtroDk) = Nothing
            Dim classConversiones As New Conversiones
            Dim strCadenaBoletosOtroDK As String = String.Empty
            Dim oFlagPagado As Boolean = False
            Dim MontoPendiente As Double = 0.0
            Dim MontoPendienteOtroDk As Double = 0.0
            Dim dblMontoPendienteNuevo As Double = 0.0

            'Dim strFecha As String = Format(Now, Constantes.IWS_DATE_FORMAT_FILE2)
            Try

                If String.IsNullOrEmpty(srtFecha) Then srtFecha = Format(Now, Constantes.IWS_DATE_FORMAT_FILE5)
                If String.IsNullOrEmpty(strHora) Then strHora = Format(Now, Constantes.IWS_TIME_FORMAT_FILE_24)
                objBoletoPendiente = New List(Of robotBoletoPendientePago.robotBoletoPendiente)

                objBoletoPagoOtroDk = New List(Of robotBoletoPendientePago.robotBoletoPagoOtroDk)

                objDAO = New DAO
                objBoletoPendiente = objDAO.ObtenerBoletosPendientesPagoSABRE(srtFecha, strHora, strCodigoSeguimiento, intFirmaDB, intEsquema)

                If objBoletoPendiente IsNot Nothing Then
                    objBoletoPagoOtroDk = objDAO.ObtenerBoletosPagaOtroDkSABRE(srtFecha, strHora, strCodigoSeguimiento, intFirmaDB, intEsquema)

                    If objBoletoPagoOtroDk IsNot Nothing Then

                        For i As Integer = 0 To objBoletoPagoOtroDk.Count - 1
                            If Not String.IsNullOrEmpty(objBoletoPagoOtroDk.Item(i).NumeroBoleto) Then
                                strCadenaBoletosOtroDK &= IIf(strCadenaBoletosOtroDK = "", "", "/") & objBoletoPagoOtroDk.Item(i).NumeroBoleto
                            End If
                        Next
                        '======================================
                        If Not String.IsNullOrEmpty(strCadenaBoletosOtroDK) Then
                            For i As Integer = 0 To objBoletoPendiente.Count - 1
                                If strCadenaBoletosOtroDK.Contains(objBoletoPendiente.Item(i).NumeroBoleto) Then
                                    For x As Integer = 0 To objBoletoPagoOtroDk.Count - 1
                                        If objBoletoPagoOtroDk.Item(x).NumeroBoleto.Equals(objBoletoPendiente.Item(i).NumeroBoleto) And _
                                           objBoletoPagoOtroDk.Item(x).IdCliente.Equals(objBoletoPendiente.Item(i).IdCliente) Then

                                            MontoPendiente = Convert.ToDouble(Trim(objBoletoPendiente.Item(i).MontoPendiente))
                                            MontoPendienteOtroDk = Convert.ToDouble(Trim(objBoletoPagoOtroDk.Item(x).MontoPagoOtroDk))
                                            dblMontoPendienteNuevo = MontoPendiente - MontoPendienteOtroDk

                                            If dblMontoPendienteNuevo < 20.0 Then
                                                objBoletoPendiente.Item(i).NoAnular = "NO VOIDEAR"
                                            End If

                                            objBoletoPendiente.Item(i).MontoPendiente = classConversiones.FormatearNumero(Convert.ToString(dblMontoPendienteNuevo), 2, False)
                                            objBoletoPendiente.Item(i).MontoOtroDK = classConversiones.FormatearNumero(Convert.ToString(MontoPendienteOtroDk), 2, False)

                                            oFlagPagado = True

                                            Exit For
                                        End If
                                    Next
                                End If
                            Next
                        End If
                    End If
                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                'srtFecha = Nothing
                strHora = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing

                objBoletoPagoOtroDk = Nothing
                classConversiones = Nothing
                strCadenaBoletosOtroDK = Nothing
                oFlagPagado = Nothing
                MontoPendiente = Nothing
                MontoPendienteOtroDk = Nothing
                dblMontoPendienteNuevo = Nothing
                '
            End Try

            Return objBoletoPendiente
        End Function

    End Class
End Namespace

