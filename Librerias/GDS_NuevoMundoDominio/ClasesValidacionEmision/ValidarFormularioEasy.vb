Imports Persistencia = GDS_NuevoMundoPersistencia
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports BO = GDS_NuevoMundoDominio.ObjetoDominioNegocio.classBO
Namespace Validar
    Partial Public Class ValidarFormularioEasy
        Public Function EmisionAutomaticaIA(ByVal objEasyOnLine As Persistencia.classEasyOnLine, _
                                            ByVal intFirmaDB As Integer, _
                                            ByVal intEsquema As Integer) As Boolean

            Dim bloRespuesta As Boolean = True

            Try
                If objEasyOnLine IsNot Nothing Then
                    If objEasyOnLine.Reserva IsNot Nothing Then

                        bloRespuesta = VerificaCantidadPasajeros(objEasyOnLine)
                        If bloRespuesta = False Then Exit Try

                        bloRespuesta = VerificaInfanteConTaca(objEasyOnLine)
                        If bloRespuesta = False Then Exit Try

                        bloRespuesta = VerificaRangodeFecha(objEasyOnLine, intFirmaDB, intEsquema)
                        If bloRespuesta = False Then Exit Try

                        bloRespuesta = VerificaIGV(objEasyOnLine)
                        If bloRespuesta = False Then Exit Try

                        bloRespuesta = VerificaReglasTourCode(objEasyOnLine)
                        If bloRespuesta = False Then Exit Try

                        bloRespuesta = VerificaCreditoDisponible(objEasyOnLine)
                        If bloRespuesta = False Then Exit Try

                    End If
                End If
            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objEasyOnLine = Nothing
            End Try

            Return bloRespuesta

        End Function
        Private Function VerificaCantidadPasajeros(ByVal objEasyOnLine As Persistencia.classEasyOnLine) As Boolean

            Dim intLimitePasajeros As Integer = 8
            Dim intCantidadPasajeros As Integer = 0
            Dim bolRespuesta As Boolean = False

            Try

                With objEasyOnLine.Reserva
                    If .Pasajeros IsNot Nothing Then
                        For i As Integer = 0 To .Pasajeros.Count - 1
                            If .Pasajeros.Item(i).Marca Then
                                intCantidadPasajeros += 1
                                If intCantidadPasajeros > intLimitePasajeros Then
                                    bolRespuesta = False
                                    Exit For
                                Else
                                    bolRespuesta = True
                                End If
                            End If
                        Next
                    End If
                End With

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                intLimitePasajeros = Nothing
                intCantidadPasajeros = Nothing
                objEasyOnLine = Nothing
            End Try

            Return bolRespuesta
        End Function
        Private Function VerificaInfanteConTaca(ByVal objEasyOnLine As Persistencia.classEasyOnLine) As Boolean

            Dim bolRespuesta As Boolean = True
            Dim strCadenaAerolineaNoPermitida As String = "TA/T0/LR/AV"

            Try

                With objEasyOnLine.Reserva
                    If .Tarifa IsNot Nothing Then
                        'If .Tarifa.LineaValidadora IsNot Nothing Then
                        If Not String.IsNullOrEmpty(.Tarifa.LineaValidadora) Then
                            If strCadenaAerolineaNoPermitida.Contains(.Tarifa.LineaValidadora) Then
                                If .Pasajeros IsNot Nothing Then
                                    For i As Integer = 0 To .Pasajeros.Count - 1
                                        If .Pasajeros.Item(i).Marca Then
                                            If .Pasajeros.Item(i).Infante Then
                                                bolRespuesta = False
                                                Exit Try
                                            End If
                                        End If
                                    Next
                                End If
                            Else
                                bolRespuesta = True
                                Exit Try
                            End If
                        End If
                        'End If
                    End If
                End With

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objEasyOnLine = Nothing
                strCadenaAerolineaNoPermitida = Nothing
            End Try

            Return bolRespuesta
        End Function
        Private Function VerificaRangodeFecha(ByVal objEasyOnLine As Persistencia.classEasyOnLine, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As Boolean

            Dim objBO As New BO
            Dim dateFechaLlegada As String
            Dim intRespuesta As Integer = 0
            Dim bolRespuesta As Boolean = True

            Try
                With objEasyOnLine.Reserva
                    If .Segmentos IsNot Nothing Then
                        If Not String.IsNullOrEmpty(.Segmentos.Item(0).FechaHoraLlegada) Then

                            dateFechaLlegada = Format(CDate(.Segmentos.Item(0).FechaHoraLlegada.Replace("T", " ")), Constantes.IWS_DATE_FORMAT_FILE5)
                            intRespuesta = objBO.DiferenciaFecha(dateFechaLlegada, _
                                                                 objEasyOnLine.CodigoSegimiento, _
                                                                 intFirmaDB, _
                                                                 intEsquema)

                            If intRespuesta < 2 Then
                                bolRespuesta = False
                            End If
                        End If
                    End If
                End With

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objEasyOnLine = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objBO = Nothing
                dateFechaLlegada = Nothing
                intRespuesta = Nothing
            End Try

            Return bolRespuesta

        End Function
        Private Function VerificaIGV(ByVal objEasyOnLine As Persistencia.classEasyOnLine) As Boolean

            Dim bolRespuesta As Boolean = True

            Try
                With objEasyOnLine.Reserva
                    If .Tarifa IsNot Nothing Then
                        If .Tarifa.Tarifa_x_Pax IsNot Nothing Then
                            For i As Integer = 0 To .Tarifa.Tarifa_x_Pax.Count - 1
                                If .Tarifa.Tarifa_x_Pax.Item(i).IGV.Monto IsNot Nothing Then
                                    bolRespuesta = True
                                Else
                                    bolRespuesta = False
                                    Exit For
                                End If
                            Next
                        End If
                    End If

                End With

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objEasyOnLine = Nothing
            End Try

            Return bolRespuesta

        End Function
        Private Function VerificaReglasTourCode(ByVal objEasyOnLine As Persistencia.classEasyOnLine) As Boolean

            Dim bolRespuesta As Boolean = True

            Try


                If objEasyOnLine.TourCodeResultado IsNot Nothing Then
                    For i As Integer = 0 To objEasyOnLine.TourCodeResultado.Count - 1
                        If (objEasyOnLine.TourCodeResultado.Item(i).Tarifario = -1 Or objEasyOnLine.TourCodeResultado.Item(i).Tarifario = 0) _
                           And (objEasyOnLine.TourCodeResultado.Item(i).Regla = -1 Or objEasyOnLine.TourCodeResultado.Item(i).Regla = 0) Then
                            bolRespuesta = False
                        Else
                            If (objEasyOnLine.TourCodeResultado.Item(i).EmisionWeb = -1 Or objEasyOnLine.TourCodeResultado.Item(i).EmisionWeb = 0) Then
                                bolRespuesta = False
                            Else
                                bolRespuesta = True
                            End If
                        End If
                    Next
                Else
                    bolRespuesta = False
                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objEasyOnLine = Nothing
            End Try

            Return bolRespuesta

        End Function
        Private Function VerificaCreditoDisponible(ByVal objEasyOnLine As Persistencia.classEasyOnLine) As Boolean

            Dim bolRespuesta As Boolean = True
            Dim dblTotalEmisiones As Double = 0.0
            Dim dblTotalReserva As Double = 0.0
            Try


                If objEasyOnLine.Cliente IsNot Nothing Then


                    If objEasyOnLine.Reserva.Tarifa IsNot Nothing Then
                        If objEasyOnLine.Reserva.Tarifa.TotalReserva IsNot Nothing Then
                            dblTotalReserva = CDbl(objEasyOnLine.Reserva.Tarifa.TotalReserva.Monto)
                        End If
                    End If


                    If objEasyOnLine.Cliente.Condicion = "CON" Then

                        If objEasyOnLine.Cliente.LineaCredito IsNot Nothing Then

                            If Not String.IsNullOrEmpty(objEasyOnLine.Cliente.LineaCredito.Pendiente) Then
                                dblTotalEmisiones = CDbl(objEasyOnLine.Cliente.LineaCredito.Pendiente)
                            Else
                                bolRespuesta = False
                            End If

                            If (dblTotalEmisiones + dblTotalReserva) >= 3000 Then
                                bolRespuesta = False
                            Else
                                bolRespuesta = True
                            End If
                        Else
                            bolRespuesta = False
                        End If

                    Else

                        If objEasyOnLine.Cliente.LineaCredito IsNot Nothing Then
                            dblTotalEmisiones = CDbl(objEasyOnLine.Cliente.LineaCredito.Disponible)
                        Else
                            bolRespuesta = False
                        End If

                        If (dblTotalEmisiones - dblTotalReserva) >= 0 Then
                            bolRespuesta = True
                        Else
                            bolRespuesta = False
                        End If

                    End If

                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objEasyOnLine = Nothing
            End Try

            Return bolRespuesta

        End Function
    End Class
    'TipoTarjeta
    'Waiver
End Namespace
