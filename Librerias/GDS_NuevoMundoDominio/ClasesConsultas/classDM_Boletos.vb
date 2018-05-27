Imports GDS_NuevoMundoPersistencia
Imports GDS_NM_Mensajeria
Imports classDO = GDS_NuevoMundoAccesoDatos
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Imports BO = GDS_NuevoMundoDominio.ObjetoDominioNegocio.classBO
Imports System.Linq
Imports System.IO

Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Private strLogAviso As String = Nothing
#Region "Consultas"
        Public Function ObtenerBoletosPendientesPago(ByVal srtFecha As String, _
                                                     ByVal strHora As String, _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As List(Of robotBoletoPendientePago.robotBoletoPendiente)

            Dim objDAO As New DAO
            Dim objBoletoPendiente As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Try

                objBoletoPendiente = objDAO.ObtenerBoletosPendientesPago(srtFecha, strHora, strCodigoSeguimiento, intFirmaDB, intEsquema)


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                srtFecha = Nothing
                strHora = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objBoletoPendiente

        End Function
        Public Function ObtenerBoletosPagaOtroDk(ByVal srtFecha As String, _
                                                 ByVal strHora As String, _
                                                 ByVal strCodigoSeguimiento As String, _
                                                 ByVal intFirmaDB As Integer, _
                                                 ByVal intEsquema As Integer) As List(Of robotBoletoPendientePago.robotBoletoPagoOtroDk)

            Dim objDAO As New DAO
            Dim objBoletoPagoOtroDk As List(Of robotBoletoPendientePago.robotBoletoPagoOtroDk) = Nothing
            Try

                objBoletoPagoOtroDk = objDAO.ObtenerBoletosPagaOtroDk(srtFecha, strHora, strCodigoSeguimiento, intFirmaDB, intEsquema)


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                srtFecha = Nothing
                strHora = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objBoletoPagoOtroDk

        End Function
        Public Function ObtenerBoletosEmitidos_X_Agente(ByVal objReporteVentas As List(Of classReporteVentas), _
                                                        ByVal strFirmaAgente As String) As List(Of classReporteBoletos.classBoletosDuplicados)


            Dim CadenaPrincipal As String = ""
            Dim strBoletos As New System.Text.StringBuilder
            Dim auxDuplicados1 As classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre = Nothing
            Dim auxDuplicadosBoletos1 As classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos = Nothing
            Dim Boleto1 As String = Nothing
            Dim Nombre As String = Nothing

            Dim strBoletosAgente As New System.Text.StringBuilder
            Dim strPNRsAgente As New System.Text.StringBuilder
            Dim strPNRPasajerosAgente As New System.Text.StringBuilder
            Dim listBoletosAgente As List(Of classReporteBoletos.classBoletosDuplicados) = Nothing
            Dim auxBoletosAgente As classReporteBoletos.classBoletosDuplicados = Nothing

            Dim objReporteBoletos As classReporteBoletos.ReporteBoletosDelDias = Nothing

            Try

                If objReporteVentas IsNot Nothing Then

                    For y As Integer = 0 To objReporteVentas.Count - 1
                        For z As Integer = 0 To objReporteVentas.Item(y).Boletos.Count - 1

                            If objReporteVentas.Item(y).Boletos.Item(z).Estado <> "VOID" Then

                                'If objReporteVentas.Item(y).Boletos.Item(z).Estado <> "VOID" And _
                                '    objReporteVentas.Item(y).Boletos.Item(z).Estado <> "REMISION" Then
                                'If objReporteVentas.Item(y).Boletos.Item(z).PNR = "UYKFEO" Then

                                'If Trim(objReporteVentas.Item(y).Boletos.Item(z).NumBoleto.Length) = 13 Then


                                'CadenaPrincipal = objReporteVentas.Item(y).Boletos.Item(z).PNR & Constantes.Guion & _
                                '                  objReporteVentas.Item(y).Boletos.Item(z).NombrePasajero & Constantes.Guion & _
                                '                  Trim(objReporteVentas.Item(y).Boletos.Item(z).NumBoleto).Substring(0, 3) & Constantes.Guion & _
                                '                  objReporteVentas.Item(y).Boletos.Item(z).Domestico

                                auxDuplicados1 = New classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre
                                auxDuplicados1.NombrePax = objReporteVentas.Item(y).Boletos.Item(z).NombrePasajero
                                Nombre = objReporteVentas.Item(y).Boletos.Item(z).NombrePasajero

                                auxDuplicadosBoletos1 = New classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos
                                auxDuplicadosBoletos1.Pseudo = objReporteVentas.Item(y).Pseudo
                                auxDuplicadosBoletos1.Boleto = objReporteVentas.Item(y).Boletos.Item(z).NumBoleto
                                Boleto1 = objReporteVentas.Item(y).Boletos.Item(z).NumBoleto
                                auxDuplicadosBoletos1.Agente = objReporteVentas.Item(y).Boletos.Item(z).Agente
                                auxDuplicadosBoletos1.Hora = objReporteVentas.Item(y).Boletos.Item(z).Hora
                                auxDuplicadosBoletos1.EstadoTkt = objReporteVentas.Item(y).Boletos.Item(z).Estado

                                If Trim(objReporteVentas.Item(y).Boletos.Item(z).PNR.ToString).IndexOf("NO PNR") = -1 Then

                                    '========================================
                                    '*** VERIFICA LA FIRMA DEL EQUIPO GDS ***
                                    '========================================
                                    If strFirmaAgente.Contains(objReporteVentas.Item(y).Boletos.Item(z).Agente) Then

                                        If strPNRsAgente.ToString.IndexOf(objReporteVentas.Item(y).Boletos.Item(z).PNR) = -1 Then
                                            strPNRsAgente.Append(IIf(strPNRsAgente.ToString = "", "", "/") & objReporteVentas.Item(y).Boletos.Item(z).PNR)
                                            strPNRPasajerosAgente.Append(IIf(strPNRPasajerosAgente.ToString = "", "", Constantes.BackSlash) & objReporteVentas.Item(y).Boletos.Item(z).PNR & Constantes.Guion & objReporteVentas.Item(y).Boletos.Item(z).NombrePasajero)

                                            auxBoletosAgente = New classReporteBoletos.classBoletosDuplicados
                                            auxBoletosAgente.PNR = objReporteVentas.Item(y).Boletos.Item(z).PNR
                                            auxBoletosAgente.DuplicadosNombre = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre)

                                            Dim auxAgente As New classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre
                                            auxAgente = auxDuplicados1
                                            auxAgente.Boletos = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos)
                                            auxAgente.Boletos.Add(auxDuplicadosBoletos1)

                                            auxBoletosAgente.DuplicadosNombre.Add(auxAgente)

                                            If listBoletosAgente Is Nothing Then listBoletosAgente = New List(Of classReporteBoletos.classBoletosDuplicados)
                                            listBoletosAgente.Add(auxBoletosAgente)
                                            auxBoletosAgente = Nothing

                                            strBoletosAgente.Append(IIf(strBoletosAgente.ToString = "", "", "/") & Boleto1)

                                        ElseIf strPNRPasajerosAgente.ToString.IndexOf(objReporteVentas.Item(y).Boletos.Item(z).PNR & Constantes.Guion & objReporteVentas.Item(y).Boletos.Item(z).NombrePasajero) = -1 Then
                                            strPNRPasajerosAgente.Append(IIf(strPNRPasajerosAgente.ToString = "", "", Constantes.BackSlash) & objReporteVentas.Item(y).Boletos.Item(z).PNR & Constantes.Guion & objReporteVentas.Item(y).Boletos.Item(z).NombrePasajero)

                                            For u As Integer = 0 To listBoletosAgente.Count - 1
                                                If listBoletosAgente.Item(u).PNR = objReporteVentas.Item(y).Boletos.Item(z).PNR Then

                                                    Dim auxAgente As New classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre
                                                    auxAgente = auxDuplicados1
                                                    auxAgente.Boletos = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos)
                                                    auxAgente.Boletos.Add(auxDuplicadosBoletos1)

                                                    If listBoletosAgente.Item(u).DuplicadosNombre Is Nothing Then listBoletosAgente.Item(u).DuplicadosNombre = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre)
                                                    listBoletosAgente.Item(u).DuplicadosNombre.Add(auxAgente)
                                                    auxAgente = Nothing

                                                    strBoletosAgente.Append(IIf(strBoletosAgente.ToString = "", "", "/") & Boleto1)
                                                    Exit For
                                                End If
                                            Next

                                        Else

                                            If strBoletosAgente.ToString.IndexOf(Boleto1) = -1 Then
                                                For u As Integer = 0 To listBoletosAgente.Count - 1
                                                    If listBoletosAgente.Item(u).PNR = objReporteVentas.Item(y).Boletos.Item(z).PNR Then
                                                        For t As Integer = 0 To listBoletosAgente.Item(u).DuplicadosNombre.Count - 1
                                                            If listBoletosAgente.Item(u).DuplicadosNombre.Item(t).NombrePax = Nombre Then

                                                                If listBoletosAgente.Item(u).DuplicadosNombre.Item(t).Boletos Is Nothing Then listBoletosAgente.Item(u).DuplicadosNombre.Item(t).Boletos = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos)
                                                                listBoletosAgente.Item(u).DuplicadosNombre.Item(t).Boletos.Add(auxDuplicadosBoletos1)
                                                                strBoletosAgente.Append(IIf(strBoletos.ToString = "", "", "/") & Boleto1)
                                                                Exit For
                                                            End If
                                                        Next
                                                    End If
                                                Next
                                            End If


                                        End If
                                    End If

                                End If
                                'End If
                                'End If
                            End If
                        Next
                    Next

                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                CadenaPrincipal = Nothing
                strBoletos = Nothing
                auxDuplicados1 = Nothing
                auxDuplicadosBoletos1 = Nothing
                Boleto1 = Nothing
                Nombre = Nothing
                strBoletosAgente = Nothing
                strPNRsAgente = Nothing
                strPNRPasajerosAgente = Nothing
                auxBoletosAgente = Nothing

                objReporteVentas = Nothing
                strFirmaAgente = Nothing
            End Try

            Return listBoletosAgente

        End Function
        Public Function ObtenerBoletosDuplicados(ByVal strCadenaPseudos As String, _
                                                 ByVal strFecha As String, _
                                                 ByVal strCodigoSeguimiento As String, _
                                                 ByVal intGDS As Integer, _
                                                 ByVal intFirmaGDS As Integer, _
                                                 ByVal intFirmaDB As Integer, _
                                                 ByVal objSession As classSession) As classReporteBoletos.ReporteBoletosDelDias


            Dim strHora As String = Format(Now, Constantes.IWS_TIME_FORMAT_FILE_24)
            Dim objReporteVentas As List(Of classReporteVentas) = Nothing
            'Dim WSSabre As New BOWS.ClasesSabreWS.WSSabre

            Dim CadenaPrincipal As String = ""
            Dim CadenaComparacion As String = ""
            Dim strPNRs As New System.Text.StringBuilder
            Dim strBoletos As New System.Text.StringBuilder
            Dim strPNRPasajeros As New System.Text.StringBuilder
            Dim listBoletosDuplicados As List(Of classReporteBoletos.classBoletosDuplicados) = Nothing
            Dim auxBoletosDuplicados As classReporteBoletos.classBoletosDuplicados = Nothing
            Dim auxDuplicados1 As classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre = Nothing
            Dim auxDuplicadosAlternativo As classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre = Nothing
            Dim auxDuplicados2 As classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre = Nothing
            Dim auxDuplicadosBoletos1 As classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos = Nothing
            Dim auxDuplicadosBoletos2 As classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos = Nothing
            Dim Boleto1 As String = Nothing
            Dim Boleto2 As String = Nothing
            Dim Nombre As String = Nothing

            Dim FirmasAgentes As String = "AH1/ARC/A2E/ANR/A4V/AHW/ARU"


            'AH1 --> Hugo Sánchez
            'ARC --> Rosa Cardenas
            'A2E --> Juan Canepa
            'ANR --> Nellie Reid
            'A4V --> Javier Matto
            'AHW --> Karen Cuba
            'ARU --> Mario Huaman

            Dim strBoletosAgente As New System.Text.StringBuilder
            Dim strPNRsAgente As New System.Text.StringBuilder
            Dim strPNRPasajerosAgente As New System.Text.StringBuilder
            Dim listBoletosAgente As List(Of classReporteBoletos.classBoletosDuplicados) = Nothing
            Dim auxBoletosAgente As classReporteBoletos.classBoletosDuplicados = Nothing

            Dim objReporteBoletos As classReporteBoletos.ReporteBoletosDelDias = Nothing
            Dim cadenaFirmasAgentes As String = String.Empty
            Dim cadenaFirmasAgentesAuxiliar As String = String.Empty

            Try

                objReporteVentas = DQB(strFecha, _
                                       strCadenaPseudos, _
                                       strCodigoSeguimiento, _
                                       intGDS, _
                                       intFirmaGDS, _
                                       intFirmaDB, _
                                       objSession)

                If objReporteVentas IsNot Nothing Then

                    For y As Integer = 0 To objReporteVentas.Count - 1
                        For z As Integer = 0 To objReporteVentas.Item(y).Boletos.Count - 1

                            If Not cadenaFirmasAgentesAuxiliar.Contains(objReporteVentas.Item(y).Boletos.Item(z).Agente) Then
                                cadenaFirmasAgentesAuxiliar &= IIf(cadenaFirmasAgentesAuxiliar = "", "", Constantes.Slash) & objReporteVentas.Item(y).Boletos.Item(z).Agente
                            End If


                            If objReporteVentas.Item(y).Boletos.Item(z).Estado <> "VOID" And _
                                objReporteVentas.Item(y).Boletos.Item(z).Estado <> "REMISION" Then

                                'If objReporteVentas.Item(y).Boletos.Item(z).PNR = "UYKFEO" Then

                                If Trim(objReporteVentas.Item(y).Boletos.Item(z).NumBoleto.Length) = 13 Then


                                    CadenaPrincipal = objReporteVentas.Item(y).Boletos.Item(z).PNR & Constantes.Guion & _
                                                      objReporteVentas.Item(y).Boletos.Item(z).NombrePasajero & Constantes.Guion & _
                                                      Trim(objReporteVentas.Item(y).Boletos.Item(z).NumBoleto).Substring(0, 3) & Constantes.Guion & _
                                                      objReporteVentas.Item(y).Boletos.Item(z).Domestico

                                    auxDuplicados1 = New classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre
                                    auxDuplicados1.NombrePax = objReporteVentas.Item(y).Boletos.Item(z).NombrePasajero
                                    Nombre = objReporteVentas.Item(y).Boletos.Item(z).NombrePasajero

                                    auxDuplicadosBoletos1 = New classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos
                                    auxDuplicadosBoletos1.Pseudo = objReporteVentas.Item(y).Pseudo
                                    auxDuplicadosBoletos1.Boleto = objReporteVentas.Item(y).Boletos.Item(z).NumBoleto
                                    Boleto1 = objReporteVentas.Item(y).Boletos.Item(z).NumBoleto
                                    auxDuplicadosBoletos1.Agente = objReporteVentas.Item(y).Boletos.Item(z).Agente

                                    If Not cadenaFirmasAgentes.Contains(objReporteVentas.Item(y).Boletos.Item(z).Agente) Then
                                        cadenaFirmasAgentes &= IIf(cadenaFirmasAgentes = "", "", Constantes.Slash) & objReporteVentas.Item(y).Boletos.Item(z).Agente
                                    End If


                                    auxDuplicadosBoletos1.Hora = objReporteVentas.Item(y).Boletos.Item(z).Hora
                                    auxDuplicadosBoletos1.EstadoTkt = objReporteVentas.Item(y).Boletos.Item(z).Estado

                                    If Trim(objReporteVentas.Item(y).Boletos.Item(z).PNR.ToString).IndexOf("NO PNR") = -1 Then

                                        '========================================
                                        '*** VERIFICA LA FIRMA DEL EQUIPO GDS ***
                                        '========================================
                                        If FirmasAgentes.Contains(objReporteVentas.Item(y).Boletos.Item(z).Agente) Then

                                            If strPNRsAgente.ToString.IndexOf(objReporteVentas.Item(y).Boletos.Item(z).PNR) = -1 Then
                                                strPNRsAgente.Append(IIf(strPNRsAgente.ToString = "", "", "/") & objReporteVentas.Item(y).Boletos.Item(z).PNR)
                                                strPNRPasajerosAgente.Append(IIf(strPNRPasajerosAgente.ToString = "", "", Constantes.BackSlash) & objReporteVentas.Item(y).Boletos.Item(z).PNR & Constantes.Guion & objReporteVentas.Item(y).Boletos.Item(z).NombrePasajero)

                                                auxBoletosAgente = New classReporteBoletos.classBoletosDuplicados
                                                auxBoletosAgente.PNR = objReporteVentas.Item(y).Boletos.Item(z).PNR
                                                auxBoletosAgente.DuplicadosNombre = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre)

                                                Dim auxAgente As New classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre
                                                auxAgente = auxDuplicados1
                                                auxAgente.Boletos = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos)
                                                auxAgente.Boletos.Add(auxDuplicadosBoletos1)

                                                auxBoletosAgente.DuplicadosNombre.Add(auxAgente)

                                                If listBoletosAgente Is Nothing Then listBoletosAgente = New List(Of classReporteBoletos.classBoletosDuplicados)
                                                listBoletosAgente.Add(auxBoletosAgente)
                                                auxBoletosAgente = Nothing

                                                strBoletosAgente.Append(IIf(strBoletosAgente.ToString = "", "", "/") & Boleto1)

                                            ElseIf strPNRPasajerosAgente.ToString.IndexOf(objReporteVentas.Item(y).Boletos.Item(z).PNR & Constantes.Guion & objReporteVentas.Item(y).Boletos.Item(z).NombrePasajero) = -1 Then
                                                strPNRPasajerosAgente.Append(IIf(strPNRPasajerosAgente.ToString = "", "", Constantes.BackSlash) & objReporteVentas.Item(y).Boletos.Item(z).PNR & Constantes.Guion & objReporteVentas.Item(y).Boletos.Item(z).NombrePasajero)

                                                For u As Integer = 0 To listBoletosAgente.Count - 1
                                                    If listBoletosAgente.Item(u).PNR = objReporteVentas.Item(y).Boletos.Item(z).PNR Then

                                                        Dim auxAgente As New classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre
                                                        auxAgente = auxDuplicados1
                                                        auxAgente.Boletos = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos)
                                                        auxAgente.Boletos.Add(auxDuplicadosBoletos1)

                                                        If listBoletosAgente.Item(u).DuplicadosNombre Is Nothing Then listBoletosAgente.Item(u).DuplicadosNombre = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre)
                                                        listBoletosAgente.Item(u).DuplicadosNombre.Add(auxAgente)
                                                        auxAgente = Nothing

                                                        strBoletosAgente.Append(IIf(strBoletosAgente.ToString = "", "", "/") & Boleto1)
                                                        Exit For
                                                    End If
                                                Next

                                            Else

                                                If strBoletosAgente.ToString.IndexOf(Boleto1) = -1 Then
                                                    For u As Integer = 0 To listBoletosAgente.Count - 1
                                                        If listBoletosAgente.Item(u).PNR = objReporteVentas.Item(y).Boletos.Item(z).PNR Then
                                                            For t As Integer = 0 To listBoletosAgente.Item(u).DuplicadosNombre.Count - 1
                                                                If listBoletosAgente.Item(u).DuplicadosNombre.Item(t).NombrePax = Nombre Then

                                                                    If listBoletosAgente.Item(u).DuplicadosNombre.Item(t).Boletos Is Nothing Then listBoletosAgente.Item(u).DuplicadosNombre.Item(t).Boletos = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos)
                                                                    listBoletosAgente.Item(u).DuplicadosNombre.Item(t).Boletos.Add(auxDuplicadosBoletos1)
                                                                    strBoletosAgente.Append(IIf(strBoletos.ToString = "", "", "/") & Boleto1)
                                                                    Exit For
                                                                End If
                                                            Next
                                                        End If
                                                    Next
                                                End If


                                            End If
                                        End If


                                        '=======================================
                                        '*** VERIFICA LOS BOLETOS DUPLICADOS ***
                                        '=======================================

                                        For i As Integer = 0 To objReporteVentas.Count - 1
                                            For x As Integer = 0 To objReporteVentas.Item(i).Boletos.Count - 1
                                                If Not ((y = i) And (z = x)) Then

                                                    If objReporteVentas.Item(i).Boletos.Item(x).Estado <> "VOID" And _
                                                        objReporteVentas.Item(i).Boletos.Item(x).Estado <> "REMISION" Then

                                                        If objReporteVentas.Item(i).Boletos.Item(x).NumBoleto.Length = 13 Then



                                                            CadenaComparacion = objReporteVentas.Item(i).Boletos.Item(x).PNR & Constantes.Guion & _
                                                                                objReporteVentas.Item(i).Boletos.Item(x).NombrePasajero & Constantes.Guion & _
                                                                                Trim(objReporteVentas.Item(i).Boletos.Item(x).NumBoleto).Substring(0, 3) & Constantes.Guion & _
                                                                                objReporteVentas.Item(i).Boletos.Item(x).Domestico


                                                            If CadenaPrincipal = CadenaComparacion Then

                                                                auxDuplicados2 = New classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre
                                                                auxDuplicados2.NombrePax = objReporteVentas.Item(i).Boletos.Item(x).NombrePasajero

                                                                auxDuplicadosBoletos2 = New classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos

                                                                auxDuplicadosBoletos2.Pseudo = objReporteVentas.Item(i).Pseudo
                                                                auxDuplicadosBoletos2.Boleto = objReporteVentas.Item(i).Boletos.Item(x).NumBoleto
                                                                Boleto2 = objReporteVentas.Item(i).Boletos.Item(x).NumBoleto
                                                                auxDuplicadosBoletos2.Agente = objReporteVentas.Item(i).Boletos.Item(x).Agente
                                                                auxDuplicadosBoletos2.Hora = objReporteVentas.Item(i).Boletos.Item(x).Hora
                                                                auxDuplicadosBoletos2.EstadoTkt = objReporteVentas.Item(i).Boletos.Item(x).Estado


                                                                If strPNRs.ToString.IndexOf(objReporteVentas.Item(y).Boletos.Item(z).PNR) = -1 Then
                                                                    strPNRs.Append(IIf(strPNRs.ToString = "", "", "/") & objReporteVentas.Item(y).Boletos.Item(z).PNR)
                                                                    strPNRPasajeros.Append(IIf(strPNRPasajeros.ToString = "", "", Constantes.BackSlash) & objReporteVentas.Item(y).Boletos.Item(z).PNR & Constantes.Guion & objReporteVentas.Item(y).Boletos.Item(z).NombrePasajero)

                                                                    auxBoletosDuplicados = New classReporteBoletos.classBoletosDuplicados
                                                                    auxBoletosDuplicados.PNR = objReporteVentas.Item(y).Boletos.Item(z).PNR
                                                                    auxBoletosDuplicados.DuplicadosNombre = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre)

                                                                    Dim aux As New classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre
                                                                    aux = auxDuplicados1
                                                                    aux.Boletos = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos)
                                                                    aux.Boletos.Add(auxDuplicadosBoletos1)
                                                                    aux.Boletos.Add(auxDuplicadosBoletos2)

                                                                    '======================= COMPARAMOS SI HAY UN BOLETO BUPLICADO EMITIDI POR EL AGENTE =========
                                                                    If FirmasAgentes.Contains(objReporteVentas.Item(i).Boletos.Item(x).Agente) Then
                                                                        If strPNRPasajerosAgente.ToString.Contains(objReporteVentas.Item(i).Boletos.Item(x).PNR & Constantes.Guion & objReporteVentas.Item(i).Boletos.Item(x).NombrePasajero) Then
                                                                            For t As Integer = 0 To aux.Boletos.Count - 1
                                                                                If strBoletosAgente.ToString.IndexOf(aux.Boletos.Item(t).Boleto) = -1 Then
                                                                                    strBoletosAgente.Append(IIf(strBoletos.ToString = "", "", "/") & aux.Boletos.Item(t).Boleto)
                                                                                End If
                                                                            Next
                                                                        End If
                                                                    End If
                                                                    '=============================================================================================

                                                                    auxBoletosDuplicados.DuplicadosNombre.Add(aux)

                                                                    If listBoletosDuplicados Is Nothing Then listBoletosDuplicados = New List(Of classReporteBoletos.classBoletosDuplicados)
                                                                    listBoletosDuplicados.Add(auxBoletosDuplicados)
                                                                    auxBoletosDuplicados = Nothing

                                                                    strBoletos.Append(IIf(strBoletos.ToString = "", "", "/") & Boleto1)
                                                                    strBoletos.Append(IIf(strBoletos.ToString = "", "", "/") & Boleto2)

                                                                ElseIf strPNRPasajeros.ToString.IndexOf(objReporteVentas.Item(y).Boletos.Item(z).PNR & Constantes.Guion & objReporteVentas.Item(y).Boletos.Item(z).NombrePasajero) = -1 Then

                                                                    strPNRPasajeros.Append(IIf(strPNRPasajeros.ToString = "", "", Constantes.BackSlash) & objReporteVentas.Item(y).Boletos.Item(z).PNR & Constantes.Guion & objReporteVentas.Item(y).Boletos.Item(z).NombrePasajero)

                                                                    For u As Integer = 0 To listBoletosDuplicados.Count - 1
                                                                        If listBoletosDuplicados.Item(u).PNR = objReporteVentas.Item(y).Boletos.Item(z).PNR Then

                                                                            Dim aux As New classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre
                                                                            aux = auxDuplicados1
                                                                            aux.Boletos = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos)
                                                                            aux.Boletos.Add(auxDuplicadosBoletos1)
                                                                            aux.Boletos.Add(auxDuplicadosBoletos2)



                                                                            '======================= COMPARAMOS SI HAY UN BOLETO BUPLICADO EMITIDI POR EL AGENTE =========
                                                                            If FirmasAgentes.Contains(objReporteVentas.Item(i).Boletos.Item(x).Agente) Then
                                                                                If strPNRPasajerosAgente.ToString.Contains(objReporteVentas.Item(i).Boletos.Item(x).PNR & Constantes.Guion & objReporteVentas.Item(i).Boletos.Item(x).NombrePasajero) Then
                                                                                    For t As Integer = 0 To aux.Boletos.Count - 1
                                                                                        If strBoletosAgente.ToString.IndexOf(aux.Boletos.Item(t).Boleto) = -1 Then
                                                                                            strBoletosAgente.Append(IIf(strBoletos.ToString = "", "", "/") & aux.Boletos.Item(t).Boleto)
                                                                                        End If
                                                                                    Next
                                                                                End If
                                                                            End If
                                                                            '=============================================================================================


                                                                            If listBoletosDuplicados.Item(u).DuplicadosNombre Is Nothing Then listBoletosDuplicados.Item(u).DuplicadosNombre = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre)
                                                                            listBoletosDuplicados.Item(u).DuplicadosNombre.Add(aux)

                                                                            strBoletos.Append(IIf(strBoletos.ToString = "", "", "/") & Boleto1)
                                                                            strBoletos.Append(IIf(strBoletos.ToString = "", "", "/") & Boleto2)
                                                                            Exit For
                                                                        End If

                                                                    Next

                                                                Else

                                                                    If strBoletos.ToString.IndexOf(Boleto1) = -1 Then
                                                                        For u As Integer = 0 To listBoletosDuplicados.Count - 1
                                                                            If listBoletosDuplicados.Item(u).PNR = objReporteVentas.Item(y).Boletos.Item(z).PNR Then
                                                                                For t As Integer = 0 To listBoletosDuplicados.Item(u).DuplicadosNombre.Count - 1
                                                                                    If listBoletosDuplicados.Item(u).DuplicadosNombre.Item(t).NombrePax = Nombre Then

                                                                                        If listBoletosDuplicados.Item(u).DuplicadosNombre.Item(t).Boletos Is Nothing Then listBoletosDuplicados.Item(u).DuplicadosNombre.Item(t).Boletos = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos)
                                                                                        listBoletosDuplicados.Item(u).DuplicadosNombre.Item(t).Boletos.Add(auxDuplicadosBoletos1)
                                                                                        strBoletos.Append(IIf(strBoletos.ToString = "", "", "/") & Boleto1)
                                                                                        Exit For
                                                                                    End If
                                                                                Next
                                                                            End If
                                                                        Next
                                                                    End If


                                                                    If strBoletos.ToString.IndexOf(Boleto2) = -1 Then
                                                                        For u As Integer = 0 To listBoletosDuplicados.Count - 1
                                                                            If listBoletosDuplicados.Item(u).PNR = objReporteVentas.Item(i).Boletos.Item(x).PNR Then
                                                                                For t As Integer = 0 To listBoletosDuplicados.Item(u).DuplicadosNombre.Count - 1
                                                                                    If listBoletosDuplicados.Item(u).DuplicadosNombre.Item(t).NombrePax = Nombre Then

                                                                                        If listBoletosDuplicados.Item(u).DuplicadosNombre.Item(t).Boletos Is Nothing Then listBoletosDuplicados.Item(u).DuplicadosNombre.Item(t).Boletos = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos)
                                                                                        listBoletosDuplicados.Item(u).DuplicadosNombre.Item(t).Boletos.Add(auxDuplicadosBoletos2)
                                                                                        strBoletos.Append(IIf(strBoletos.ToString = "", "", "/") & Boleto2)
                                                                                        Exit For
                                                                                    End If
                                                                                Next
                                                                            End If
                                                                        Next
                                                                    End If

                                                                End If

                                                            End If
                                                        End If
                                                    End If

                                                End If
                                            Next
                                        Next

                                    End If
                                    ' End If
                                End If
                            End If
                        Next
                    Next



                    If listBoletosAgente IsNot Nothing Then
                        If objReporteBoletos Is Nothing Then objReporteBoletos = New classReporteBoletos.ReporteBoletosDelDias
                        objReporteBoletos.BoletosAgenteGDS = New List(Of classReporteBoletos.classBoletosDuplicados)
                        objReporteBoletos.BoletosAgenteGDS = listBoletosAgente
                    End If

                    If listBoletosDuplicados IsNot Nothing Then
                        If objReporteBoletos Is Nothing Then objReporteBoletos = New classReporteBoletos.ReporteBoletosDelDias
                        objReporteBoletos.BoletosDuplicados = New List(Of classReporteBoletos.classBoletosDuplicados)

                        objReporteBoletos.BoletosDuplicados = ObtieneSegmentos(listBoletosDuplicados, _
                                                                               strCodigoSeguimiento, _
                                                                               intGDS, _
                                                                               intFirmaGDS, _
                                                                               intFirmaDB, _
                                                                               objSession)
                    End If

                    '*************************************************************
                    '****  SE AGREYO YA QUE EXISTEN CASOS DE COUNTERS QUE NO  ****
                    '****  REALIZAN EMISIONES PERO IS REALIZAN REMISIONES     ****
                    '*************************************************************
                    'If cadenaFirmasAgentesAuxiliar.Length > cadenaFirmasAgentes.Length Then
                    cadenaFirmasAgentes = cadenaFirmasAgentesAuxiliar
                    'End If
                    '*************************************************************

                    If Not String.IsNullOrEmpty(cadenaFirmasAgentes) Then
                        If objReporteBoletos Is Nothing Then objReporteBoletos = New classReporteBoletos.ReporteBoletosDelDias
                        objReporteBoletos.FirmasAgentes = cadenaFirmasAgentes
                        objReporteBoletos.ReporteVentas = New List(Of classReporteVentas)
                        objReporteBoletos.ReporteVentas = objReporteVentas
                    End If

                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strFecha = Nothing
                strHora = Nothing
                objReporteVentas = Nothing
                CadenaPrincipal = Nothing
                strPNRs = Nothing
                strBoletos = Nothing
                strPNRPasajeros = Nothing
                auxBoletosDuplicados = Nothing
                auxDuplicados1 = Nothing
                auxDuplicados2 = Nothing
                auxDuplicadosBoletos1 = Nothing
                auxDuplicadosBoletos2 = Nothing
                Boleto1 = Nothing
                Boleto2 = Nothing
                Nombre = Nothing
            End Try

            Return objReporteBoletos

        End Function
        Private Function ObtieneSegmentos(ByVal listBoletosDuplicados As List(Of classReporteBoletos.classBoletosDuplicados), _
                                          ByVal strCodigoSeguimiento As String, _
                                          ByVal intGDS As Integer, _
                                          ByVal intFirmaGDS As Integer, _
                                          ByVal intFirmaDB As Integer, _
                                          ByVal objSession As classSession) As List(Of classReporteBoletos.classBoletosDuplicados)

            'Dim WSSabre As New BOWS.ClasesSabreWS.WSSabre

            Dim strPNR As String = Nothing
            Dim strBoleto As String = Nothing
            Dim objTCTicket As classTicketCoupon.TCTicket = Nothing
            Dim strOrigen As String = Nothing
            Dim strDestino As String = Nothing
            Dim strDepartureDateTime As String = Nothing
            Dim strFlightNumber As String = Nothing
            Dim strMarketingAirline As String = Nothing
            Dim strResBookDesigCode As String = Nothing
            Dim strSegmentos As String = Nothing
            Dim strAuxSegmentos As String = Nothing
            Try

                If listBoletosDuplicados IsNot Nothing Then


                    For i As Integer = 0 To listBoletosDuplicados.Count - 1

                        strPNR = Nothing
                        strPNR = listBoletosDuplicados.Item(i).PNR.ToString
                        For x As Integer = 0 To listBoletosDuplicados.Item(i).DuplicadosNombre.Count - 1

                            For y As Integer = 0 To listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Count - 1

                                strBoleto = Nothing
                                strSegmentos = Nothing
                                strBoleto = listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(y).Boleto.ToString

                                objTCTicket = New classTicketCoupon.TCTicket
                                objTCTicket = TicketCupon(strBoleto, _
                                                          strCodigoSeguimiento, _
                                                          intGDS, _
                                                          intFirmaGDS, _
                                                          intFirmaDB, _
                                                          objSession)


                                If objTCTicket IsNot Nothing Then
                                    If objTCTicket.TCCouponData IsNot Nothing Then
                                        If objTCTicket.TCCouponData.TCCoupon IsNot Nothing Then
                                            For v As Integer = 0 To objTCTicket.TCCouponData.TCCoupon.Count - 1

                                                If objTCTicket.TCCouponData.TCCoupon(v).TCFlightSegment IsNot Nothing Then

                                                    strOrigen = Nothing
                                                    strDestino = Nothing
                                                    strDepartureDateTime = Nothing
                                                    strFlightNumber = Nothing
                                                    strMarketingAirline = Nothing
                                                    strResBookDesigCode = Nothing
                                                    strAuxSegmentos = Nothing


                                                    strOrigen = objTCTicket.TCCouponData.TCCoupon(v).TCFlightSegment.OriginLocation
                                                    strDestino = objTCTicket.TCCouponData.TCCoupon(v).TCFlightSegment.DestinationLocation
                                                    strDepartureDateTime = objTCTicket.TCCouponData.TCCoupon(v).TCFlightSegment.DepartureDateTime
                                                    strFlightNumber = objTCTicket.TCCouponData.TCCoupon(v).TCFlightSegment.FlightNumber
                                                    strMarketingAirline = objTCTicket.TCCouponData.TCCoupon(v).TCFlightSegment.MarketingAirline.Code
                                                    strResBookDesigCode = objTCTicket.TCCouponData.TCCoupon(v).TCFlightSegment.ResBookDesigCode


                                                    strAuxSegmentos = strOrigen & Constantes.GuionBajo & _
                                                                      strDestino & Constantes.GuionBajo & _
                                                                      strDepartureDateTime & Constantes.GuionBajo & _
                                                                      strFlightNumber & Constantes.GuionBajo & _
                                                                      strMarketingAirline & Constantes.GuionBajo & _
                                                                      strResBookDesigCode

                                                    strSegmentos &= IIf(String.IsNullOrEmpty(strSegmentos), "", Constantes.Separador) & strAuxSegmentos

                                                End If
                                            Next
                                        End If
                                    End If
                                End If

                                listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(y).TodoSegmetos = strSegmentos

                            Next

                        Next
                    Next

                    listBoletosDuplicados = VerificaDuplicados(listBoletosDuplicados)

                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing

                strPNR = Nothing
                strBoleto = Nothing
                objTCTicket = Nothing
                strOrigen = Nothing
                strDestino = Nothing
                strDepartureDateTime = Nothing
                strFlightNumber = Nothing
                strMarketingAirline = Nothing
                strResBookDesigCode = Nothing
                strSegmentos = Nothing
                strAuxSegmentos = Nothing
            End Try

            Return listBoletosDuplicados
        End Function
        Private Function VerificaDuplicados(ByVal listBoletosDuplicados As List(Of classReporteBoletos.classBoletosDuplicados)) As List(Of classReporteBoletos.classBoletosDuplicados)


            Dim strPNR As String = Nothing
            Dim strBoleto1 As String = Nothing
            Dim strBoleto2 As String = Nothing
            Dim strNombrePax As String = Nothing

            Dim strSegmentos1 As String = Nothing
            Dim strSegmentos2 As String = Nothing

            Dim lstBoletos As List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos) = Nothing

            Dim lstNombre As List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre) = Nothing
            Dim auxNombre As classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre = Nothing

            Dim lstBoletosDuplicados As List(Of classReporteBoletos.classBoletosDuplicados) = Nothing
            Dim auxBoletosDuplicados As classReporteBoletos.classBoletosDuplicados = Nothing


            Try

                If listBoletosDuplicados IsNot Nothing Then


                    For i As Integer = 0 To listBoletosDuplicados.Count - 1

                        strPNR = Nothing
                        strPNR = listBoletosDuplicados.Item(i).PNR.ToString

                        lstNombre = Nothing

                        For x As Integer = 0 To listBoletosDuplicados.Item(i).DuplicadosNombre.Count - 1

                            strNombrePax = Nothing
                            strNombrePax = listBoletosDuplicados.Item(i).DuplicadosNombre(x).NombrePax

                            lstBoletos = Nothing

                            For y As Integer = 0 To listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Count - 1

                                If Not listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(y).Marca Then

                                    strBoleto1 = Nothing
                                    strSegmentos1 = Nothing
                                    strBoleto1 = listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(y).Boleto.ToString
                                    strSegmentos1 = listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(y).TodoSegmetos

                                    For n As Integer = 0 To listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Count - 1

                                        If Not listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(n).Marca Then

                                            strBoleto2 = Nothing
                                            strSegmentos2 = Nothing
                                            strBoleto2 = listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(n).Boleto.ToString

                                            If Not strBoleto1.Equals(strBoleto2) Then
                                                strSegmentos2 = listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(n).TodoSegmetos

                                                If strSegmentos1.Equals(strSegmentos2) Then
                                                    listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(y).Marca = True
                                                    listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(n).Marca = True

                                                    If lstBoletos Is Nothing Then lstBoletos = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos)
                                                    lstBoletos.Add(listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(y))
                                                    lstBoletos.Add(listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(n))
                                                End If

                                            End If

                                        End If

                                    Next

                                End If

                            Next

                            If lstBoletos IsNot Nothing Then

                                auxNombre = New classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre
                                auxNombre.NombrePax = strNombrePax
                                auxNombre.Boletos = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosBoletos)
                                auxNombre.Boletos = lstBoletos

                                '======= agregamos los boletos doplicados ========
                                If lstNombre Is Nothing Then lstNombre = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre)
                                lstNombre.Add(auxNombre)

                            End If


                        Next

                        If lstNombre IsNot Nothing Then
                            auxBoletosDuplicados = New classReporteBoletos.classBoletosDuplicados
                            auxBoletosDuplicados.PNR = strPNR
                            auxBoletosDuplicados.DuplicadosNombre = New List(Of classReporteBoletos.classBoletosDuplicados.classDuplicadosNombre)
                            auxBoletosDuplicados.DuplicadosNombre = lstNombre
                            '======= AGREGAMOS NOMBRE ========

                            If lstBoletosDuplicados Is Nothing Then lstBoletosDuplicados = New List(Of classReporteBoletos.classBoletosDuplicados)
                            lstBoletosDuplicados.Add(auxBoletosDuplicados)

                        End If

                    Next

                    listBoletosDuplicados = lstBoletosDuplicados

                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strPNR = Nothing
                strBoleto1 = Nothing
                strBoleto2 = Nothing
                strNombrePax = Nothing
                strSegmentos1 = Nothing
                strSegmentos2 = Nothing
                lstBoletos = Nothing
                lstNombre = Nothing
                auxNombre = Nothing
                lstBoletosDuplicados = Nothing
                auxBoletosDuplicados = Nothing
            End Try

            Return listBoletosDuplicados
        End Function

        Public Function ObtenerDatosFile(ByVal strPNR As String, _
                                         ByVal strBoleto As String, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As classFile


            Dim objDAO As DAO = Nothing
            Dim objFile As classFile = Nothing
            Try

                objDAO = New DAO
                objFile = objDAO.ObtenerDatosFile(strPNR, strBoleto, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strPNR = Nothing
                strBoleto = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objFile

        End Function


#End Region
#Region "Reportes"
        Public Function ReporteBoletosEmitidos(ByVal strCadenaPseudos As String, _
                                               ByVal strFechaAux As String, _
                                               ByVal intEnviaAgentesCounter As Integer, _
                                               ByVal intHora As Integer, _
                                               ByVal strCodigoSeguimiento As String, _
                                               ByVal intGDS As Integer, _
                                               ByVal intFirmaGDS As Integer, _
                                               ByVal intFirmaDB As Integer, _
                                               ByVal intEsquema As Integer, _
                                               ByVal objSession As classSession) As String()

            Dim strFecha As String = Format(Now, Constantes.IWS_DATE_FORMAT_FILE2)
            Dim strHora As String = Format(Now, Constantes.IWS_TIME_FORMAT_FILE_24)
            Dim objEnviarEmail As EnviarEmail = Nothing
            Dim objCuerpoCorreo As cuerpoCorreo = Nothing
            Dim objCorreo As classCorreo = Nothing

            Dim bolRespuesta As List(Of String) = Nothing
            Dim bolRespuesta2() As String = Nothing
            Dim objReporteBoletosDelDias As classReporteBoletos.ReporteBoletosDelDias = Nothing
            Dim listBoletos_x_Agente As List(Of classReporteBoletos.classBoletosDuplicados) = Nothing

            Dim strCadenaReporte As New System.Text.StringBuilder

            Dim objBO As BO = Nothing
            Dim objDatosAgente As List(Of classDatosAgente) = Nothing
            Dim strFirmas As String = String.Empty


            Dim strCorreoAgente As String = String.Empty

            Try

                If String.IsNullOrEmpty(strCadenaPseudos) Then strCadenaPseudos = "QF05/S0X7/QP75/HW57/QQ05/QP35/94DH"

                If Not String.IsNullOrEmpty(strFechaAux) Then
                    strFecha = Format(CDate(strFechaAux), Constantes.IWS_DATE_FORMAT_FILE2)
                End If


                objReporteBoletosDelDias = ObtenerBoletosDuplicados(strCadenaPseudos, _
                                                                    strFecha, _
                                                                    strCodigoSeguimiento, _
                                                                    intGDS, _
                                                                    intFirmaGDS, _
                                                                    intFirmaDB, _
                                                                    objSession)

                If objReporteBoletosDelDias IsNot Nothing Then

                    If intHora = 9 Then

                        '==================== BOLETOS AGENTES GDS ====================
                        If objReporteBoletosDelDias.BoletosAgenteGDS IsNot Nothing Then
                            If CrearCorreo(objReporteBoletosDelDias.BoletosAgenteGDS, _
                                           Nothing, _
                                           Nothing, _
                                           intEnviaAgentesCounter, _
                                           strCodigoSeguimiento, _
                                           intFirmaDB, _
                                           intEsquema, _
                                           "Reporte de boletos emitidos por agente el día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos, _
                                           Constantes.EmisionGDS) Then


                                If bolRespuesta Is Nothing Then bolRespuesta = New List(Of String)
                                bolRespuesta.Add("Se envió reporte de boletos emitidos por agentes de GDS el día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos)

                            Else
                                If bolRespuesta Is Nothing Then bolRespuesta = New List(Of String)
                                bolRespuesta.Add("No se envió reporte de boletos emitidos por agentes de GDS el día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos)

                            End If
                        Else
                            If bolRespuesta Is Nothing Then bolRespuesta = New List(Of String)
                            bolRespuesta.Add("No se encontraron boletos emitidos por agentes de GDS el día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos)

                        End If

                        '==================== BOLETOS DUPLICADOS ====================
                        If objReporteBoletosDelDias.BoletosDuplicados IsNot Nothing Then
                            If CrearCorreo(objReporteBoletosDelDias.BoletosDuplicados, _
                                           Nothing, _
                                           Nothing, _
                                           intEnviaAgentesCounter, _
                                           strCodigoSeguimiento, _
                                           intFirmaDB, _
                                           intEsquema, _
                                           "Reporte de boletos duplicados del día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos, _
                                           Constantes.Duplicados) Then

                                If bolRespuesta Is Nothing Then bolRespuesta = New List(Of String)
                                bolRespuesta.Add("Se envió reporte de boletos duplicados del día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos)

                            Else
                                If bolRespuesta Is Nothing Then bolRespuesta = New List(Of String)
                                bolRespuesta.Add("No de envió reporte de boletos duplicados el día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos)

                            End If
                        Else
                            If bolRespuesta Is Nothing Then bolRespuesta = New List(Of String)
                            bolRespuesta.Add("No se encontraron datos para el reporte de boletos duplicados del día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos)

                        End If


                        '==================== BOLETOS AGENTES ====================

                        If intEnviaAgentesCounter > 0 Then

                            If Not String.IsNullOrEmpty(objReporteBoletosDelDias.FirmasAgentes) Then

                                strFirmas = "'" & objReporteBoletosDelDias.FirmasAgentes.Substring(1).Replace("/A", "','") & "'"

                                objBO = New BO
                                objDatosAgente = objBO.ObtenerDatosAgenteConsolidador(strFirmas, _
                                                                                      strCodigoSeguimiento, _
                                                                                      intGDS, _
                                                                                      intFirmaGDS, _
                                                                                      intFirmaDB, _
                                                                                      intEsquema)

                                If objDatosAgente IsNot Nothing Then

                                    For i As Integer = 0 To objDatosAgente.Count - 1 ' objReporteBoletosDelDias.FirmasAgentes.Split(Constantes.Slash).Length - 1

                                        'If objDatosAgente.Item(i).FirmaAgente = "EW" Then
                                        'If objDatosAgente.Item(i).FirmaAgente = "LD" Then

                                        listBoletos_x_Agente = ObtenerBoletosEmitidos_X_Agente(objReporteBoletosDelDias.ReporteVentas, _
                                                               "A" & objDatosAgente.Item(i).FirmaAgente) 'objReporteBoletosDelDias.FirmasAgentes.Split(Constantes.Slash)(i))

                                        If listBoletos_x_Agente IsNot Nothing Then


                                            Threading.Thread.Sleep(1000)

                                            If CrearCorreo(listBoletos_x_Agente, _
                                                           objDatosAgente.Item(i).NombreAgente, _
                                                           objDatosAgente.Item(i).CorreoAgente, _
                                                           intEnviaAgentesCounter, _
                                                           strCodigoSeguimiento, _
                                                           intFirmaDB, _
                                                           intEsquema, _
                                                           "Reporte de emisiones realizadas por el agente " & objDatosAgente.Item(i).NombreAgente & " el día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos, _
                                                           Constantes.EmisionAgente) Then

                                                Dim intTotal As Integer = 0
                                                For x As Integer = 0 To listBoletos_x_Agente.Count - 1
                                                    For y As Integer = 0 To listBoletos_x_Agente.Item(x).DuplicadosNombre.Count - 1
                                                        intTotal += listBoletos_x_Agente.Item(x).DuplicadosNombre.Item(y).Boletos.Count
                                                    Next
                                                Next
                                                If bolRespuesta Is Nothing Then bolRespuesta = New List(Of String)
                                                bolRespuesta.Add("Se envió reporte de " & intTotal & " " & IIf(intTotal > 1, " emisiones realizadas ", " emisión realizada") & " por el agente " & objDatosAgente.Item(i).NombreAgente & " el día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos)

                                            Else
                                                If bolRespuesta Is Nothing Then bolRespuesta = New List(Of String)
                                                bolRespuesta.Add("No se envió el reporte de las emisiones realizadas por el agente " & objDatosAgente.Item(i).NombreAgente & " el día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos)
                                            End If

                                        Else
                                            If bolRespuesta Is Nothing Then bolRespuesta = New List(Of String)
                                            bolRespuesta.Add("No se encontraron datos para el envío del reporte de las emisiones realizadas por el agente " & objDatosAgente.Item(i).NombreAgente & " el día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos)
                                        End If
                                        'End If

                                    Next

                                End If

                            Else
                                If bolRespuesta Is Nothing Then bolRespuesta = New List(Of String)
                                bolRespuesta.Add("No se encontraron datos para el envío del reporte de las emisiones realizadas por agentes  el día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos)
                            End If

                        End If

                    Else

                        If objReporteBoletosDelDias.BoletosDuplicados IsNot Nothing Then
                            If CrearCorreo(objReporteBoletosDelDias.BoletosDuplicados, _
                                           Nothing, _
                                           Nothing, _
                                           intEnviaAgentesCounter, _
                                           strCodigoSeguimiento, _
                                           intFirmaDB, _
                                           intEsquema, _
                                           "Reporte de boletos duplicados del día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos, _
                                           Constantes.Duplicados) Then

                                If bolRespuesta Is Nothing Then bolRespuesta = New List(Of String)
                                bolRespuesta.Add("Se envió reporte de boletos duplicados del día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos)

                            Else
                                If bolRespuesta Is Nothing Then bolRespuesta = New List(Of String)
                                bolRespuesta.Add("No de envió reporte de boletos duplicados el día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos)

                            End If
                        Else
                            If bolRespuesta Is Nothing Then bolRespuesta = New List(Of String)
                            bolRespuesta.Add("No se encontraron datos para el reporte de boletos duplicados del día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos)

                        End If

                    End If



                Else
                    If bolRespuesta Is Nothing Then bolRespuesta = New List(Of String)
                    bolRespuesta.Add("No se encontraron boletos para generar reporte de las emisiones realizadas el día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos)
                End If



                strCadenaReporte.Append("<table cellSpacing='0' cellPadding='3' border='0'>" & vbCrLf)
                For i As Integer = 0 To bolRespuesta.Count - 1
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td class='textContenido'>" & bolRespuesta.Item(i).ToString & "</td>" & vbCrLf)

                    If bolRespuesta2 Is Nothing Then
                        ReDim bolRespuesta2(0)
                    Else
                        ReDim Preserve bolRespuesta2(bolRespuesta2.Length)
                    End If
                    bolRespuesta2(bolRespuesta2.Length - 1) = bolRespuesta.Item(i)
                    strCadenaReporte.Append("</tr>" & vbCrLf)
                Next
                strCadenaReporte.Append("</table>" & vbCrLf)


                objCorreo = New classCorreo
                objCorreo.ToCorreo = Constantes.emailGDS.ToString
                objCorreo.NombreCorreo = Constantes.NombreCorreoEasyReporte
                objCorreo.SubjectCorreo = "Confirmación del reporte de boletos emitidos el día " & strFecha & " hasta las " & strHora & " en los pseudos " & strCadenaPseudos
                objCuerpoCorreo = New cuerpoCorreo
                objCorreo.BodyCorreo = objCuerpoCorreo.GeneraCuerpo(strCadenaReporte.ToString)

                objEnviarEmail = New EnviarEmail
                objEnviarEmail.Send(objCorreo, True, strCodigoSeguimiento, 3, Nothing)


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCadenaPseudos = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                intHora = Nothing

                objSession = Nothing
                strFechaAux = Nothing
                strFecha = Nothing
                strHora = Nothing
                objEnviarEmail = Nothing
                objCuerpoCorreo = Nothing
                objCorreo = Nothing

                objReporteBoletosDelDias = Nothing
            End Try

            Return bolRespuesta2
        End Function
        Public Function CrearCorreo(ByVal listBoletosDuplicados As List(Of classReporteBoletos.classBoletosDuplicados), _
                                    ByVal strNombreAgente As String, _
                                    ByVal strCorreoAgente As String, _
                                    ByVal intEnviaAgentesCounter As Integer, _
                                    ByVal strCodigoSeguimiento As String, _
                                    ByVal intFirmaDB As Integer, _
                                    ByVal intEsquema As Integer, _
                                    ByVal Asunto As String, _
                                    ByVal strOrigen As String) As Boolean

            Dim strHora As String = Format(Now, Constantes.IWS_TIME_FORMAT_FILE_24)
            Dim objEnviarEmail As EnviarEmail = Nothing
            Dim objCuerpoCorreo As cuerpoCorreo = Nothing
            Dim objCorreo As classCorreo = Nothing

            Dim strCadenaReporte As New System.Text.StringBuilder
            Dim strPNRs As New System.Text.StringBuilder
            Dim strBoletos As New System.Text.StringBuilder
            Dim strPNRPasajeros As New System.Text.StringBuilder
            Dim strDetalleBoletos1 As New System.Text.StringBuilder
            Dim strDetalleBoletos2 As New System.Text.StringBuilder
            Dim strDetallePasajeros As New System.Text.StringBuilder

            Dim EstiloClase As String = String.Empty
            Dim EstiloClase_c As String = " class='tarifario_fila_c' align='center'"
            Dim bolRespuesta As Boolean = False

            Dim cantidadBoletos As Integer = 0
            Dim cantidadBoletos2 As Integer = 0
            Dim cantidadPasajeros As Integer = 0
            Dim strFirmasAgentes As String = Nothing

            Dim strFirmasAgenteConsulta As String = String.Empty
            Dim strNombreAgenteConsulta As String = String.Empty

            Try


                If listBoletosDuplicados IsNot Nothing Then


                    strCadenaReporte.Append("<table cellSpacing='0' cellPadding='3' border='0' width='100%' >" & vbCrLf)
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td>" & vbCrLf)

                    strCadenaReporte.Append("<table cellSpacing='0' cellPadding='3' border='0' width='100%' >" & vbCrLf)
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td class='textContenido'>" & vbCrLf)
                    strCadenaReporte.Append("Este reporte se genera de forma autom&aacute;tica en base al reporte de venta (DBQ*) e incluye solo las emisiones realizadas en Sabre. <br>" & vbCrLf)
                    strCadenaReporte.Append("Es responsabilidad de cada agente verificar y llevar el control de todas sus emisiones." & vbCrLf)
                    strCadenaReporte.Append("</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)
                    strCadenaReporte.Append("</table>" & vbCrLf)

                    strCadenaReporte.Append("<table cellSpacing='0' cellPadding='3' border='0' width='100%' style='background-color:White;border:1px #CC9966 solid;border-collapse:collapse;'>" & vbCrLf)

                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>N.</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>PNR</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>NOMBRE PASAJERO</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>PSEUDO</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>N&Uacute;MERO BOLETO</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>HORA</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>AGENTE</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>ESTADO</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)


                    strDetalleBoletos1 = New System.Text.StringBuilder
                    strDetalleBoletos2 = New System.Text.StringBuilder
                    strDetallePasajeros = New System.Text.StringBuilder
                    cantidadBoletos = 0
                    cantidadBoletos2 = 0
                    cantidadPasajeros = 0

                    For i As Integer = 0 To listBoletosDuplicados.Count - 1

                        cantidadPasajeros = 0
                        cantidadBoletos2 = 0
                        strDetallePasajeros = New System.Text.StringBuilder
                        For x As Integer = 0 To listBoletosDuplicados.Item(i).DuplicadosNombre.Count - 1

                            cantidadBoletos = 0
                            strDetalleBoletos1 = New System.Text.StringBuilder
                            strDetalleBoletos2 = New System.Text.StringBuilder
                            For z As Integer = 0 To listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Count - 1

                                If z Mod 2 = 0 Then
                                    EstiloClase = " class='tarifario_fila_a' align='center'"
                                Else
                                    EstiloClase = " class='tarifario_fila_a' align='center'"
                                End If


                                If listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Count = 1 Then
                                    strDetalleBoletos1.Append("<td height='30'" & EstiloClase & ">" & listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).Pseudo & "</td>" & vbCrLf)
                                    strDetalleBoletos1.Append("<td" & EstiloClase & ">" & listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).Boleto & "</td>" & vbCrLf)
                                    strDetalleBoletos1.Append("<td" & EstiloClase & ">" & listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).Hora & "</td>" & vbCrLf)

                                    strDetalleBoletos1.Append(ObtenerNombreAgente(listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).Agente, strNombreAgente, EstiloClase, strCodigoSeguimiento, intFirmaDB, intEsquema))

                                    strFirmasAgentes = RecopilaFirmas(strFirmasAgentes, listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).Agente)

                                    strDetalleBoletos1.Append("<td" & EstiloClase & ">" & listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).EstadoTkt & "</td>" & vbCrLf)
                                    strDetalleBoletos1.Append("</tr>" & vbCrLf)
                                Else
                                    If z = 0 Then
                                        strDetalleBoletos1.Append("<td height='30'" & EstiloClase & ">" & listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).Pseudo & "</td>" & vbCrLf)
                                        strDetalleBoletos1.Append("<td" & EstiloClase & ">" & listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).Boleto & "</td>" & vbCrLf)
                                        strDetalleBoletos1.Append("<td" & EstiloClase & ">" & listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).Hora & "</td>" & vbCrLf)

                                        strDetalleBoletos1.Append(ObtenerNombreAgente(listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).Agente, strNombreAgente, EstiloClase, strCodigoSeguimiento, intFirmaDB, intEsquema))

                                        strFirmasAgentes = RecopilaFirmas(strFirmasAgentes, listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).Agente)

                                        strDetalleBoletos1.Append("<td" & EstiloClase & ">" & listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).EstadoTkt & "</td>" & vbCrLf)
                                        strDetalleBoletos1.Append("</tr>" & vbCrLf)
                                    Else
                                        strDetalleBoletos2.Append("<tr>" & vbCrLf)
                                        strDetalleBoletos2.Append("<td height='30'" & EstiloClase & ">" & listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).Pseudo & "</td>" & vbCrLf)
                                        strDetalleBoletos2.Append("<td" & EstiloClase & ">" & listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).Boleto & "</td>" & vbCrLf)
                                        strDetalleBoletos2.Append("<td" & EstiloClase & ">" & listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).Hora & "</td>" & vbCrLf)


                                        strDetalleBoletos2.Append(ObtenerNombreAgente(listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).Agente, strNombreAgente, EstiloClase, strCodigoSeguimiento, intFirmaDB, intEsquema))

                                        strFirmasAgentes = RecopilaFirmas(strFirmasAgentes, listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).Agente)

                                        strDetalleBoletos2.Append("<td" & EstiloClase & ">" & listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Item(z).EstadoTkt & "</td>" & vbCrLf)
                                        strDetalleBoletos2.Append("</tr>" & vbCrLf)
                                    End If
                                End If

                                cantidadBoletos = listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).Boletos.Count
                            Next

                            If cantidadBoletos = 1 Then
                                strDetallePasajeros.Append("<td height='30'" & EstiloClase_c & ">" & listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).NombrePax & "</td>" & vbCrLf)
                            Else
                                strDetallePasajeros.Append("<td rowspan='" & cantidadBoletos & "'" & EstiloClase_c & ">" & listBoletosDuplicados.Item(i).DuplicadosNombre.Item(x).NombrePax & "</td>" & vbCrLf)
                            End If
                            strDetallePasajeros.Append(strDetalleBoletos1)

                            Dim PRUEBA1 As String = strDetalleBoletos1.ToString
                            If strDetalleBoletos2 IsNot Nothing Then
                                If Not String.IsNullOrEmpty(strDetalleBoletos2.ToString) Then
                                    strDetallePasajeros.Append(strDetalleBoletos2)
                                    PRUEBA1 = strDetalleBoletos2.ToString
                                End If
                            End If

                            strDetalleBoletos1 = Nothing
                            strDetalleBoletos2 = Nothing
                            cantidadPasajeros = listBoletosDuplicados.Item(i).DuplicadosNombre.Count
                            cantidadBoletos2 += cantidadBoletos
                        Next

                        strCadenaReporte.Append("<tr>" & vbCrLf)

                        If cantidadPasajeros = 1 Then
                            If cantidadBoletos2 = 1 Then
                                strCadenaReporte.Append("<td" & EstiloClase_c & ">" & i + 1 & "</td>" & vbCrLf)
                                strCadenaReporte.Append("<td" & EstiloClase_c & ">" & listBoletosDuplicados.Item(i).PNR & "</td>" & vbCrLf)
                            Else
                                strCadenaReporte.Append("<td rowspan='" & cantidadBoletos2 & "'" & EstiloClase_c & ">" & i + 1 & "</td>" & vbCrLf)
                                strCadenaReporte.Append("<td rowspan='" & cantidadBoletos2 & "'" & EstiloClase_c & ">" & listBoletosDuplicados.Item(i).PNR & "</td>" & vbCrLf)
                            End If
                        Else
                            strCadenaReporte.Append("<td rowspan='" & cantidadBoletos2 & "'" & EstiloClase_c & ">" & i + 1 & "</td>" & vbCrLf)
                            strCadenaReporte.Append("<td rowspan='" & cantidadBoletos2 & "'" & EstiloClase_c & ">" & listBoletosDuplicados.Item(i).PNR & "</td>" & vbCrLf)
                        End If

                        strCadenaReporte.Append(strDetallePasajeros)
                        Dim PRUEBA2 As String = strDetallePasajeros.ToString
                        strDetallePasajeros = Nothing

                    Next



                    strCadenaReporte.Append("</table>" & vbCrLf)

                    strCadenaReporte.Append("</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)
                    strCadenaReporte.Append("</table>" & vbCrLf)



                    'If Not String.IsNullOrEmpty(strCadenaReporte.ToString) Then

                    '    objCorreo = New classCorreo

                    '    If intEnviaAgentesCounter = 3 Then
                    '        objCorreo.ToCorreo = Constantes.emailProgSabreWeb
                    '    Else
                    '        If String.IsNullOrEmpty(strCorreoAgente) Then
                    '            If strOrigen.Equals(Constantes.Duplicados) Then
                    '                If strFirmasAgentes.Contains("A1") Then
                    '                    objCorreo.ToCorreo = ObtenerCorreos_X_Firma(strFirmasAgentes, strCodigoSeguimiento, intFirmaDB, intEsquema) & Constantes.PuntoComa & Constantes.emailEasySolutions & Constantes.PuntoComa & Constantes.emailCounterTurnoIA
                    '                Else
                    '                    objCorreo.ToCorreo = ObtenerCorreos_X_Firma(strFirmasAgentes, strCodigoSeguimiento, intFirmaDB, intEsquema) & Constantes.PuntoComa & Constantes.emailCounterTurnoIA
                    '                End If
                    '            Else
                    '                objCorreo.ToCorreo = ObtenerCorreos_X_Firma(strFirmasAgentes, strCodigoSeguimiento, intFirmaDB, intEsquema)
                    '            End If
                    '        Else
                    '            objCorreo.ToCorreo = strCorreoAgente
                    '        End If

                    '        objCorreo.BCCCorreo = Constantes.emailGDS.ToString
                    '    End If

                    '    objCorreo.NombreCorreo = Constantes.NombreCorreoEasyReporte
                    '    objCorreo.SubjectCorreo = Asunto
                    '    objCuerpoCorreo = New cuerpoCorreo
                    '    objCorreo.BodyCorreo = objCuerpoCorreo.GeneraCuerpo(strCadenaReporte.ToString)

                    '    objEnviarEmail = New EnviarEmail
                    '    objEnviarEmail.Send(objCorreo, True, strCodigoSeguimiento, 3, Nothing)

                    '    bolRespuesta = True

                    'End If

                End If


            Catch ex As Exception
                bolRespuesta = False
                Throw New Exception(ex.ToString)
            Finally
                listBoletosDuplicados = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing

                strHora = Nothing

                objEnviarEmail = Nothing
                objCuerpoCorreo = Nothing
                objCorreo = Nothing
                strCadenaReporte = Nothing
                strPNRs = Nothing
                strBoletos = Nothing
                strPNRPasajeros = Nothing
                listBoletosDuplicados = Nothing
                EstiloClase = Nothing
                EstiloClase_c = Nothing
                strDetalleBoletos1 = Nothing
                strDetallePasajeros = Nothing
                cantidadBoletos = Nothing
                cantidadBoletos2 = Nothing
                cantidadPasajeros = Nothing
            End Try

            Return bolRespuesta
        End Function
        Public Function ReporteBoletosNoPagados(ByVal srtFecha As String, _
                                                ByVal strHora As String, _
                                                ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As Boolean

            Dim objEnviarEmail As EnviarEmail = Nothing
            Dim objCuerpoCorreo As cuerpoCorreo = Nothing
            Dim objCorreo As classCorreo = Nothing
            Dim objBoletoPendiente As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing
            Dim strCadenaReporte As New System.Text.StringBuilder
            Dim bolRespuesta As Boolean = False

            Try

                objBoletoPendiente = ObtenerBoletosPendientesPago(srtFecha, _
                                                                  strHora, _
                                                                  strCodigoSeguimiento, _
                                                                  intFirmaDB, _
                                                                  intEsquema)


                If Not objBoletoPendiente Is Nothing Then

                    strCadenaReporte.Append("<table cellSpacing='0' cellPadding='3' border='0' width='90%'>" & vbCrLf)
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td>" & vbCrLf)

                    strCadenaReporte.Append("<table cellSpacing='0' cellPadding='3' border='0' width='100%' style='background-color:White;border:1px #CC9966 solid;border-collapse:collapse;'>" & vbCrLf)
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>N.</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>SUCURSAL</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>DK</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>NOMBRE</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>FILE</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>PNR</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>BOLETO</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>RUTA</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>M. VENTA</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>M. APLICADO</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>M. PENDIENTE</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>PROMOTOR</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>ESTADO</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)


                    For i As Integer = 0 To objBoletoPendiente.Count - 1
                        Dim EstiloClase As String = String.Empty

                        If i Mod 2 = 0 Then
                            EstiloClase = " class='tarifario_fila_a' align='center' "
                        Else
                            EstiloClase = " class='tarifario_fila_b' align='center' "
                        End If

                        strCadenaReporte.Append("<tr>" & vbCrLf)
                        strCadenaReporte.Append("<td height='30px' " & EstiloClase & ">" & Convert.ToString(i + 1) & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & objBoletoPendiente.Item(i).Descripcion & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & objBoletoPendiente.Item(i).IdCliente & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & objBoletoPendiente.Item(i).NombreCliente & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & objBoletoPendiente.Item(i).File & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & objBoletoPendiente.Item(i).PNR & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & objBoletoPendiente.Item(i).NumeroBoleto & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & objBoletoPendiente.Item(i).Ruta & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & objBoletoPendiente.Item(i).MontoVenta & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & objBoletoPendiente.Item(i).MontoAplicado & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & objBoletoPendiente.Item(i).MontoPendiente & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & objBoletoPendiente.Item(i).Promotor.NombrePromotor & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & objBoletoPendiente.Item(i).NoAnular & "</td>" & vbCrLf)
                        strCadenaReporte.Append("</tr>" & vbCrLf)
                    Next

                    strCadenaReporte.Append("</table>" & vbCrLf)


                    strCadenaReporte.Append("</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)
                    strCadenaReporte.Append("</table>" & vbCrLf)



                    If Not String.IsNullOrEmpty(strCadenaReporte.ToString) Then

                        objCorreo = New classCorreo
                        objCorreo.ToCorreo = "kcuba@nmviajes.com" 'Constantes.emailGDS.ToString
                        objCorreo.NombreCorreo = Constantes.NombreCorreoEasyReporte
                        objCorreo.SubjectCorreo = "Reporte de boletos no pagados hasta las " & strHora
                        objCuerpoCorreo = New cuerpoCorreo
                        objCorreo.BodyCorreo = objCuerpoCorreo.GeneraCuerpo(strCadenaReporte.ToString)

                        objEnviarEmail = New EnviarEmail
                        objEnviarEmail.Send(objCorreo, True, strCodigoSeguimiento, intFirmaDB, Nothing)

                        bolRespuesta = True
                    End If

                End If
            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objEnviarEmail = Nothing
                objCorreo = Nothing
                strCadenaReporte = Nothing
                objCuerpoCorreo = Nothing

                srtFecha = Nothing
                strHora = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return bolRespuesta
        End Function



        Public Function ReoporteBoletosPendientesPagoCajas(ByVal srtFecha As String, _
                                                           ByVal strHora As String, _
                                                           ByVal strCodigoSeguimiento As String, _
                                                           ByVal intFirmaDB As Integer, _
                                                           ByVal intEsquema As Integer) As Boolean

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
            '
            Dim objEnviarEmail As EnviarEmail = Nothing
            Dim objCuerpoCorreo As cuerpoCorreo = Nothing
            Dim objCorreo As classCorreo = Nothing
            '
            Dim strCadenaReporte As New System.Text.StringBuilder

            Dim intContador As Integer = 0
            Dim bolRespuesta As Boolean = False
            Try

                If String.IsNullOrEmpty(srtFecha) Then srtFecha = Format(Now, Constantes.IWS_DATE_FORMAT_FILE5)
                If String.IsNullOrEmpty(strHora) Then strHora = Format(Now, Constantes.IWS_TIME_FORMAT_FILE_24)

                objDAO = New DAO
                objBoletoPendiente = objDAO.ObtenerBoletosPendientesPago(srtFecha, strHora, strCodigoSeguimiento, intFirmaDB, intEsquema)

                If objBoletoPendiente IsNot Nothing Then
                    objBoletoPagoOtroDk = objDAO.ObtenerBoletosPagaOtroDk(srtFecha, strHora, strCodigoSeguimiento, intFirmaDB, intEsquema)

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

                    '*******************************************
                    '=== EMPIEZA LA CONSTRUCCION DE LA TABLA ===
                    '*******************************************

                    strCadenaReporte.Append("<table cellSpacing='0' cellPadding='3' border='0' width='100%' >" & vbCrLf)
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td>" & vbCrLf)

                    strCadenaReporte.Append("<p class='textContenido'>PARA GENERAR ESTE REPORTE SOLO SE HAN TOMADO EN CUENTA LOS BOLETOS EMITIDOS EN SABRE O AMADEUS Y FACTURADOS EN PTA.</p>" & vbCrLf)


                    strCadenaReporte.Append("<table cellSpacing='0' cellPadding='3' border='0' width='100%' style='background-color:White;border:1px #CC9966 solid;border-collapse:collapse;'>" & vbCrLf)
                    strCadenaReporte.Append("<tr>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>N.</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>SUCURSAL</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>DK</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>NOMBRE AGENCIA</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>FILE</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>PNR</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>BOLETO</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>GDS</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>RUTA</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>MONTO<br />VENTA</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>MONTO<br />APLICADO</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>MONTO<br />PENDIENTE</td>" & vbCrLf)
                    If oFlagPagado Then
                        strCadenaReporte.Append("<td class='tarifario_header' align='center'>MONTO<br />PAG OTRO DK</td>" & vbCrLf)
                    End If
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>PROMOTOR</td>" & vbCrLf)
                    strCadenaReporte.Append("<td class='tarifario_header' align='center'>ESTADO</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)



                    For i As Integer = 0 To objBoletoPendiente.Count - 1
                        Dim EstiloClase As String = String.Empty

                        If i Mod 2 = 0 Then
                            EstiloClase = " class='tarifario_fila_a' align='center' "
                        Else
                            EstiloClase = " class='tarifario_fila_b' align='center' "
                        End If

                        strCadenaReporte.Append("<tr>" & vbCrLf)
                        strCadenaReporte.Append("<td height='40px' " & EstiloClase & ">" & Convert.ToString(i + 1) & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & IIf(Not String.IsNullOrEmpty(objBoletoPendiente.Item(i).Descripcion), objBoletoPendiente.Item(i).Descripcion, "--") & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & IIf(Not String.IsNullOrEmpty(objBoletoPendiente.Item(i).IdCliente), objBoletoPendiente.Item(i).IdCliente, "--") & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & IIf(Not String.IsNullOrEmpty(objBoletoPendiente.Item(i).NombreCliente), objBoletoPendiente.Item(i).NombreCliente, "--") & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & IIf(Not String.IsNullOrEmpty(objBoletoPendiente.Item(i).File), objBoletoPendiente.Item(i).File, "--") & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & IIf(Not String.IsNullOrEmpty(objBoletoPendiente.Item(i).PNR), objBoletoPendiente.Item(i).PNR, "--") & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & IIf(Not String.IsNullOrEmpty(objBoletoPendiente.Item(i).NumeroBoleto), _
                                                                            IIf(Not String.IsNullOrEmpty(objBoletoPendiente.Item(i).PrefijoBoleto), objBoletoPendiente.Item(i).PrefijoBoleto & " ", "") & objBoletoPendiente.Item(i).NumeroBoleto, "--") & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & IIf(Not String.IsNullOrEmpty(objBoletoPendiente.Item(i).Ruta), objBoletoPendiente.Item(i).Gds, "--") & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & IIf(Not String.IsNullOrEmpty(objBoletoPendiente.Item(i).Ruta), objBoletoPendiente.Item(i).Ruta, "--") & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & IIf(Not String.IsNullOrEmpty(objBoletoPendiente.Item(i).MontoVenta), objBoletoPendiente.Item(i).MontoVenta, "--") & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & IIf(Not String.IsNullOrEmpty(objBoletoPendiente.Item(i).MontoAplicado), objBoletoPendiente.Item(i).MontoAplicado, "--") & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & IIf(Not String.IsNullOrEmpty(objBoletoPendiente.Item(i).MontoPendiente), objBoletoPendiente.Item(i).MontoPendiente, "--") & "</td>" & vbCrLf)

                        If oFlagPagado Then
                            strCadenaReporte.Append("<td" & EstiloClase & ">" & IIf(Not String.IsNullOrEmpty(objBoletoPendiente.Item(i).MontoOtroDK), objBoletoPendiente.Item(i).MontoOtroDK, "0.00") & "</td>" & vbCrLf)
                        End If


                        strCadenaReporte.Append("<td" & EstiloClase & ">" & IIf(Not String.IsNullOrEmpty(objBoletoPendiente.Item(i).Promotor.NombrePromotor), objBoletoPendiente.Item(i).Promotor.NombrePromotor, "--") & "</td>" & vbCrLf)
                        strCadenaReporte.Append("<td" & EstiloClase & ">" & IIf(Not String.IsNullOrEmpty(objBoletoPendiente.Item(i).NoAnular), objBoletoPendiente.Item(i).NoAnular, "--") & "</td>" & vbCrLf)
                        strCadenaReporte.Append("</tr>" & vbCrLf)
                    Next

                    strCadenaReporte.Append("</table>" & vbCrLf)


                    strCadenaReporte.Append("</td>" & vbCrLf)
                    strCadenaReporte.Append("</tr>" & vbCrLf)
                    strCadenaReporte.Append("</table>" & vbCrLf)



                    If Not String.IsNullOrEmpty(strCadenaReporte.ToString) Then

                        objCorreo = New classCorreo
                        objCorreo.ToCorreo = "kcuba@nmviajes.com" 'Constantes.emailGDS.ToString
                        objCorreo.NombreCorreo = Constantes.NombreCorreoEasyReporte
                        objCorreo.SubjectCorreo = "Reporte de boletos no pagados del día " & srtFecha & " hasta las " & strHora
                        objCuerpoCorreo = New cuerpoCorreo
                        objCorreo.BodyCorreo = objCuerpoCorreo.GeneraCuerpo(strCadenaReporte.ToString)

                        objEnviarEmail = New EnviarEmail
                        objEnviarEmail.Send(objCorreo, True, strCodigoSeguimiento, 3, Nothing)

                        bolRespuesta = True
                    End If

                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                srtFecha = Nothing
                strHora = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                '
                objBoletoPendiente = Nothing
                objBoletoPagoOtroDk = Nothing
                classConversiones = Nothing
                strCadenaBoletosOtroDK = Nothing
                oFlagPagado = Nothing
                MontoPendiente = Nothing
                MontoPendienteOtroDk = Nothing
                dblMontoPendienteNuevo = Nothing
                '
                objEnviarEmail = Nothing
                objCuerpoCorreo = Nothing
                objCorreo = Nothing
                '
                strCadenaReporte = Nothing

                intContador = Nothing
            End Try

            Return bolRespuesta

        End Function
        Private Function RecopilaFirmas(ByVal strCadenaFirmas As String, _
                                         ByVal strFirma As String) As String

            Try

                If Not String.IsNullOrEmpty(strFirma) Then
                    If Not String.IsNullOrEmpty(strCadenaFirmas) Then
                        If Not strCadenaFirmas.Contains(strFirma.Substring(1, 2)) Then
                            strCadenaFirmas &= IIf(String.IsNullOrEmpty(strCadenaFirmas), "", Constantes.Coma) & strFirma.Substring(1, 2)
                        End If
                    Else
                        strCadenaFirmas &= IIf(String.IsNullOrEmpty(strCadenaFirmas), "", Constantes.Coma) & strFirma.Substring(1, 2)
                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strFirma = Nothing
            End Try
            Return strCadenaFirmas
        End Function
        Private Function ObtenerCorreos_X_Firma(ByVal strFirmaAgente As String, _
                                                ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As String

            Dim objBO As New BO
            Dim objDatosAgente As List(Of classDatosAgente) = Nothing

            Dim strCadenaCorreosAgente As String = Nothing
            Dim strCadenaCorreosJefes As String = Nothing

            Dim strRespuesta As String = Nothing

            Try

                If Not String.IsNullOrEmpty(strFirmaAgente) Then
                    strFirmaAgente = "'" & strFirmaAgente.Replace(",", "','") & "'"
                    objDatosAgente = objBO.ObtenerDatosAgente(strFirmaAgente, _
                                                              strCodigoSeguimiento, _
                                                              intFirmaDB, _
                                                              intEsquema)

                    If objDatosAgente IsNot Nothing Then
                        For i As Integer = 0 To objDatosAgente.Count - 1

                            If Not String.IsNullOrEmpty(objDatosAgente.Item(i).CorreoAgente) Then
                                If Not String.IsNullOrEmpty(objDatosAgente.Item(i).Departamento) Then
                                    If Not objDatosAgente.Item(i).Departamento.Equals("EAU") Then
                                        If Not objDatosAgente.Item(i).Departamento.Equals("DEA") Then ' --diners
                                            If Not objDatosAgente.Item(i).Departamento.Equals("DEC") Then
                                                If Not objDatosAgente.Item(i).Departamento.Equals("DEE") Then
                                                    If Not objDatosAgente.Item(i).Departamento.Equals("DEV") Then

                                                        If String.IsNullOrEmpty(strCadenaCorreosAgente) Then
                                                            strCadenaCorreosAgente = objDatosAgente.Item(i).CorreoAgente.ToString
                                                        Else
                                                            If Not strCadenaCorreosAgente.Contains(objDatosAgente.Item(i).CorreoAgente.ToString) Then
                                                                strCadenaCorreosAgente &= IIf(String.IsNullOrEmpty(strCadenaCorreosAgente), "", Constantes.PuntoComa) & objDatosAgente.Item(i).CorreoAgente.ToString
                                                            End If
                                                        End If
                                                    End If
                                                End If
                                            End If
                                        End If '--
                                    End If
                                End If
                            End If


                            If Not String.IsNullOrEmpty(objDatosAgente.Item(i).CorreoJefe) Then
                                If String.IsNullOrEmpty(strCadenaCorreosJefes) Then
                                    strCadenaCorreosJefes = objDatosAgente.Item(i).CorreoJefe.ToString
                                Else
                                    If Not strCadenaCorreosJefes.Contains(objDatosAgente.Item(i).CorreoJefe.ToString) Then
                                        strCadenaCorreosJefes &= IIf(String.IsNullOrEmpty(strCadenaCorreosAgente), "", Constantes.PuntoComa) & objDatosAgente.Item(i).CorreoJefe.ToString
                                    End If
                                End If
                            End If

                        Next


                        strRespuesta &= IIf(String.IsNullOrEmpty(strRespuesta), "", Constantes.PuntoComa) & strCadenaCorreosAgente
                        strRespuesta &= IIf(String.IsNullOrEmpty(strRespuesta), "", Constantes.PuntoComa) & strCadenaCorreosJefes


                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strFirmaAgente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing

                objBO = Nothing
                objDatosAgente = Nothing
                strCadenaCorreosAgente = Nothing
                strCadenaCorreosJefes = Nothing
            End Try

            If String.IsNullOrEmpty(strRespuesta) Then
                strRespuesta = "coordconsolidador@gruponuevomundo.com.pe"
            End If

            Return strRespuesta

        End Function
        Private Function ObtenerNombreAgente(ByVal strFirmaAgente As String, _
                                             ByVal strNombreAgente As String, _
                                             ByVal EstiloClase As String, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As String

            Dim strDetalleBoletos2 As New System.Text.StringBuilder

            Dim strFirmasAgenteConsulta As String = String.Empty
            Dim strNombreAgenteConsulta As String = String.Empty

            Try

                If Not String.IsNullOrEmpty(strNombreAgente) Then
                    strDetalleBoletos2.Append("<td" & EstiloClase & ">(" & strFirmaAgente & ") " & strNombreAgente & "</td>" & vbCrLf)
                Else
                    strFirmasAgenteConsulta = strFirmaAgente.Substring(1)
                    strNombreAgenteConsulta = ObtenerNombre_X_Firma(strFirmasAgenteConsulta, strCodigoSeguimiento, intFirmaDB, intEsquema)

                    If Not String.IsNullOrEmpty(strNombreAgenteConsulta) Then
                        strDetalleBoletos2.Append("<td" & EstiloClase & ">(" & strFirmaAgente & ") " & strNombreAgenteConsulta & "</td>" & vbCrLf)
                    Else
                        strDetalleBoletos2.Append("<td" & EstiloClase & ">" & strFirmaAgente & "</td>" & vbCrLf)
                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strFirmaAgente = Nothing
                strNombreAgente = Nothing
                EstiloClase = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return strDetalleBoletos2.ToString
        End Function
        Private Function ObtenerNombre_X_Firma(ByVal strFirmaAgente As String, _
                                               ByVal strCodigoSeguimiento As String, _
                                               ByVal intFirmaDB As Integer, _
                                               ByVal intEsquema As Integer) As String

            Dim objBO As New BO
            Dim objDatosAgente As List(Of classDatosAgente) = Nothing

            Dim strCadenaCorreosAgente As String = Nothing
            Dim strCadenaCorreosJefes As String = Nothing

            Dim strRespuesta As String = Nothing

            Try

                If Not String.IsNullOrEmpty(strFirmaAgente) Then
                    strFirmaAgente = "'" & strFirmaAgente.Replace(",", "','") & "'"
                    objDatosAgente = objBO.ObtenerDatosAgenteGeneral(strFirmaAgente, _
                                                                     strCodigoSeguimiento, _
                                                                     intFirmaDB, _
                                                                     intEsquema)

                    If objDatosAgente IsNot Nothing Then
                        For i As Integer = 0 To objDatosAgente.Count - 1
                            strRespuesta = objDatosAgente.Item(i).NombreAgente
                        Next

                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strFirmaAgente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing

                objBO = Nothing
                objDatosAgente = Nothing
                strCadenaCorreosAgente = Nothing
                strCadenaCorreosJefes = Nothing
            End Try

            If String.IsNullOrEmpty(strRespuesta) Then
                strRespuesta = "NO REGISTRADO"
            End If

            Return strRespuesta

        End Function
        Public Function ObtenerEMDS_en_PTA(ByVal srtFecha As String, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As List(Of robotBoletoPendientePago.robotBoletoPendiente)

            Dim objDAO As New DAO
            Dim objBoletoPendiente As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing

            Try
                objBoletoPendiente = objDAO.ObtenerEMDS_en_PTA(srtFecha, _
                                                               strCodigoSeguimiento, _
                                                               intFirmaDB, _
                                                               intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                srtFecha = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return objBoletoPendiente

        End Function

#End Region
#Region "OADP"
        Public Function EnviarBoleroPorCorreo(ByVal strPara As String, _
                                              ByVal strCC As String, _
                                              ByVal strBCC As String, _
                                              ByVal strSubject As String, _
                                              ByVal strNombreCorreo As String, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal strTicketNumber As String, _
                                              ByVal intCuenta As Integer, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As Integer
            Dim objDAO As DAO = Nothing
            Dim objTurboPassengerReceipt As classTurboPassengerRecipt = Nothing
            Dim strCadena As System.Text.StringBuilder = Nothing

            Dim objCorreo As New classCorreo
            Dim objEnviarEmail As New EnviarEmail
            Dim intRespuesta As Integer = 0

            Try
                objDAO = New DAO
                objTurboPassengerReceipt = objDAO.ObtenerTurboPassengerReceipt(strTicketNumber,
                                                                               strCodigoSeguimiento,
                                                                               intFirmaDB,
                                                                               intEsquema)


                If objTurboPassengerReceipt IsNot Nothing Then
                    'Id_Header = 1/2  es boleto
                    'Id_Header = 3  es EMD
                    If Not String.IsNullOrEmpty(objTurboPassengerReceipt.Cuerpo_Documento) Then

                        strCadena = New System.Text.StringBuilder

                        If objTurboPassengerReceipt.Id_Header = 3 Then
                            strCadena.Append("<table width='700' border='0' align='left' cellpadding='0' cellspacing='0'>" & vbCrLf)
                            strCadena.Append("<tr>" & vbCrLf)
                            strCadena.Append("<td class='style1'><div align='center'><br>" & vbCrLf)
                            strCadena.Append("ELECTRONIC&nbsp;MISCELLANEOUS&nbsp;DOCUMENT<br>" & vbCrLf)
                            strCadena.Append("PASSENGER&nbsp;ITINERARY/RECEIPT" & vbCrLf)
                            strCadena.Append("</div>" & vbCrLf)
                            strCadena.Append("</td>" & vbCrLf)
                            strCadena.Append("</tr>" & vbCrLf)
                        Else
                            strCadena.Append("<table width='700' border='0' align='left' cellpadding='0' cellspacing='0'>" & vbCrLf)
                            strCadena.Append("<tr>" & vbCrLf)
                            strCadena.Append("<td class='style1'><div align='center'>" & vbCrLf)
                            strCadena.Append("<br>" & vbCrLf)
                            strCadena.Append("ELECTRONIC&nbsp;TICKET<br>" & vbCrLf)
                            strCadena.Append("PASSENGER&nbsp;ITINERARY/RECEIPT" & vbCrLf)
                            strCadena.Append("</div>" & vbCrLf)
                            strCadena.Append("</td>" & vbCrLf)
                            strCadena.Append("</tr>" & vbCrLf)
                        End If

                        strCadena.Append(objTurboPassengerReceipt.Cuerpo_Documento)

                        strCadena.Append("</td>" & vbCrLf)
                        strCadena.Append("</tr>" & vbCrLf)
                        strCadena.Append("</table>" & vbCrLf)




                        If Not String.IsNullOrEmpty(strNombreCorreo) And _
                           Not String.IsNullOrEmpty(strPara) And _
                           Not String.IsNullOrEmpty(strCadena.ToString) And _
                           Not String.IsNullOrEmpty(strSubject) Then

                            objCorreo.NombreCorreo = strNombreCorreo
                            objCorreo.ToCorreo = strPara
                            objCorreo.CCCorreo = strCC
                            objCorreo.BCCCorreo = strBCC
                            objCorreo.BodyCorreo = strCadena.ToString
                            objCorreo.SubjectCorreo = strSubject

                            objEnviarEmail.Send(objCorreo, _
                                                True, _
                                                strCodigoSeguimiento, _
                                                intCuenta)

                            intRespuesta = 1
                        Else
                            intRespuesta = 0
                            Err.Raise(-1111, "classDM_Email.EnviarCorreo", "Alguno de los valores OBLIGATORIOS llegaron vacios")
                        End If

                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                objTurboPassengerReceipt = Nothing
                strCadena = Nothing
                objCorreo = Nothing
                objEnviarEmail = Nothing
                strPara = Nothing
                strCC = Nothing
                strBCC = Nothing
                strSubject = Nothing
                strNombreCorreo = Nothing
                strCodigoSeguimiento = Nothing
                strTicketNumber = Nothing
                intCuenta = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return intRespuesta

        End Function
#End Region
        Public Function ObtenerNo_Imprime_Cuentas(ByVal srtFirma As String, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As Integer

            Dim objDAO As New DAO
            Dim inrRespuesta As Integer = 0

            Try
                inrRespuesta = objDAO.ObtenerNo_Imprime_Cuentas(srtFirma, _
                                                                strCodigoSeguimiento, _
                                                                intFirmaDB, _
                                                                intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                srtFirma = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return inrRespuesta

        End Function
        Public Function ObtenerBoletos_DINNER_en_PTA(ByVal srtFecha As String, _
                                                     ByVal strCodigoSeguimiento As String, _
                                                     ByVal intFirmaDB As Integer, _
                                                     ByVal intEsquema As Integer) As List(Of robotBoletoPendientePago.robotBoletoPendiente)

            Dim objDAO As New DAO
            Dim objBoletoPendiente As List(Of robotBoletoPendientePago.robotBoletoPendiente) = Nothing

            Try
                objBoletoPendiente = objDAO.ObtenerBoletos_DINNER_en_PTA(srtFecha, _
                                                                         strCodigoSeguimiento, _
                                                                         intFirmaDB, _
                                                                         intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                srtFecha = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return objBoletoPendiente
        End Function
        Public Function VerificaDobleInterfaceDestinos(ByVal srtCodigoReserva As String, _
                                                              ByVal srtNumeroBoleto As String, _
                                                              ByVal strCodigoSeguimiento As String, _
                                                              ByVal intFirmaDB As Integer, _
                                                              ByVal intEsquema As Integer) As String

            Dim objDAO As New DAO
            Dim strID_Cliente As String = Nothing

            Try
                strID_Cliente = objDAO.VerificaDobleInterfaceDestinos(srtCodigoReserva, _
                                                                      srtNumeroBoleto, _
                                                                      strCodigoSeguimiento, _
                                                                      intFirmaDB, _
                                                                      intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return strID_Cliente
        End Function
        Public Function GB_InsertBoleto(ByVal lista_Boletos As List(Of ClsBoleto_GB), _
                                                    ByVal strCodigoSeguimiento As String, _
                                                    ByVal intFirmaDB As Integer, _
                                                    ByVal intEsquema As Integer) As Boolean
            Dim objDAO As New DAO
            Dim rpta As Boolean
            Try
                rpta = objDAO.GB_InsertBoleto(lista_Boletos, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return rpta
        End Function
        Public Function GB_DeleteBoletos(ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As Boolean

            Dim objDAO As New DAO
            Dim rpta As Boolean
            Try
                rpta = objDAO.GB_DeleteBoletos(strCodigoSeguimiento, intFirmaDB, intEsquema)
            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try

            Return rpta
        End Function
        Public Function GB_ListarBoletosGenerados(ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As List(Of ClsBoleto_GB)
            Dim lista_Boletos As List(Of ClsBoleto_GB)
            Dim objDAO As New DAO
            Try
                lista_Boletos = objDAO.GB_ListarBoletosGenerados(strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDAO = Nothing
            End Try
            Return lista_Boletos
        End Function
    End Class
End Namespace