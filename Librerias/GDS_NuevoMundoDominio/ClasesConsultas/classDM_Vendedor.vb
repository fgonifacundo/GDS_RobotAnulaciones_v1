Imports GDS_NuevoMundoPersistencia
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function ObtenerDatosPromotor(ByVal intCliente As Integer, _
                                             ByVal strCodigoSeguimiento As String, _
                                             ByVal intFirmaDB As Integer, _
                                             ByVal intEsquema As Integer) As classPromotor


            Dim objDAO As DAO = Nothing
            Dim objPromotor As classPromotor = Nothing
            Try

                objDAO = New DAO
                objPromotor = objDAO.ObtenerDatosPromotor(intCliente, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.Source.ToString & " " & ex.Message.ToString)
            Finally
                objDAO = Nothing
                intCliente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objPromotor

        End Function
        Public Function ObtenerDatosAgente(ByVal strFirmaAgente As String, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As List(Of classDatosAgente)

            Dim objDAO As DAO = Nothing
            Dim objDatosAgente As List(Of classDatosAgente) = Nothing
            Dim objRespuesta As List(Of classDatosAgente) = Nothing
            Try

                objDAO = New DAO
                objDatosAgente = objDAO.ObtenerDatosAgente(strFirmaAgente, _
                                                           strCodigoSeguimiento, _
                                                           intFirmaDB, _
                                                           intEsquema)

                If objDatosAgente IsNot Nothing Then

                    For i As Integer = 0 To objDatosAgente.Count - 1
                        If Not String.IsNullOrEmpty(objDatosAgente.Item(i).Departamento) Then
                            If Not objDatosAgente.Item(i).Departamento.Equals("EAU") Then
                                If Not objDatosAgente.Item(i).Departamento.Equals("DEA") Then ' -- diners
                                    If Not objDatosAgente.Item(i).Departamento.Equals("DEC") Then
                                        If Not objDatosAgente.Item(i).Departamento.Equals("DEE") Then
                                            If Not objDatosAgente.Item(i).Departamento.Equals("DEV") Then
                                                If objRespuesta Is Nothing Then objRespuesta = New List(Of classDatosAgente)
                                                objDatosAgente.Item(i).CorreoAgente = IIf(String.IsNullOrEmpty(objDatosAgente.Item(i).CorreoAgente), Constantes.emailSupervisoraCounterIA, objDatosAgente.Item(i).CorreoAgente & Constantes.PuntoComa & Constantes.emailSupervisoraCounterIA)
                                                objRespuesta.Add(objDatosAgente.Item(i))
                                            End If
                                        End If
                                    End If
                                End If '--
                            End If
                        End If
                    Next
                End If

            Catch ex As Exception
                objRespuesta = Nothing
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strFirmaAgente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDatosAgente = Nothing
            End Try

            Return objRespuesta
        End Function
        Public Function ObtenerDatosAgenteConsolidador(ByVal strFirmaAgente As String, _
                                                       ByVal strCodigoSeguimiento As String, _
                                                       ByVal intGDS As Integer, _
                                                       ByVal intFirmaGDS As Integer, _
                                                       ByVal intFirmaDB As Integer, _
                                                       ByVal intEsquema As Integer) As List(Of classDatosAgente)

            Dim objDAO As DAO = Nothing
            Dim objDatosAgente As List(Of classDatosAgente) = Nothing
            Dim objRespuesta As List(Of classDatosAgente) = Nothing
            Try

                objDAO = New DAO
                objDatosAgente = objDAO.ObtenerDatosAgente(strFirmaAgente, _
                                                           strCodigoSeguimiento, _
                                                           intFirmaGDS, _
                                                           intEsquema)

                If objDatosAgente IsNot Nothing Then
                    For i As Integer = 0 To objDatosAgente.Count - 1
                        If Not String.IsNullOrEmpty(objDatosAgente.Item(i).Departamento) Then
                            If Not objDatosAgente.Item(i).Departamento.Equals("EAU") Then
                                If Not objDatosAgente.Item(i).Departamento.Equals("DEA") Then ' --- diners
                                    If Not objDatosAgente.Item(i).Departamento.Equals("DEC") Then
                                        If Not objDatosAgente.Item(i).Departamento.Equals("DEE") Then
                                            If Not objDatosAgente.Item(i).Departamento.Equals("DEV") Then

                                                If objDatosAgente.Item(i).Departamento.Equals("CIA") Then
                                                    If objDatosAgente.Item(i).Oficina IsNot Nothing Then
                                                        If objDatosAgente.Item(i).Oficina.Contains("INTERAGENCIAS") Then
                                                            If objRespuesta Is Nothing Then objRespuesta = New List(Of classDatosAgente)
                                                            objDatosAgente.Item(i).CorreoAgente = IIf(String.IsNullOrEmpty(objDatosAgente.Item(i).CorreoAgente), Constantes.emailSupervisoraCounterIA, objDatosAgente.Item(i).CorreoAgente & Constantes.PuntoComa & Constantes.emailSupervisoraCounterIA)
                                                            objRespuesta.Add(objDatosAgente.Item(i))
                                                            'Else
                                                            '    If objRespuesta Is Nothing Then objRespuesta = New List(Of classDatosAgente)
                                                            '    objRespuesta.Add(objDatosAgente.Item(i))
                                                        End If
                                                    Else
                                                        If objDatosAgente.Item(i).NombreAgente IsNot Nothing Then
                                                            If objDatosAgente.Item(i).NombreAgente.Contains("MOTOR -WEB SABRE") Then
                                                                If objRespuesta Is Nothing Then objRespuesta = New List(Of classDatosAgente)
                                                                objDatosAgente.Item(i).CorreoAgente = IIf(String.IsNullOrEmpty(objDatosAgente.Item(i).CorreoAgente), Constantes.emailSupervisoraCounterIA, objDatosAgente.Item(i).CorreoAgente & Constantes.PuntoComa & Constantes.emailSupervisoraCounterIA)
                                                                objRespuesta.Add(objDatosAgente.Item(i))
                                                                'Else
                                                                '    If objRespuesta Is Nothing Then objRespuesta = New List(Of classDatosAgente)
                                                                '    objRespuesta.Add(objDatosAgente.Item(i))
                                                            End If
                                                        End If
                                                    End If

                                                ElseIf objDatosAgente.Item(i).Departamento.Equals("NMG") Then
                                                    If objRespuesta Is Nothing Then objRespuesta = New List(Of classDatosAgente)
                                                    objRespuesta.Add(objDatosAgente.Item(i))
                                                ElseIf objDatosAgente.Item(i).Departamento.Equals("SIS") Then
                                                    If objRespuesta Is Nothing Then objRespuesta = New List(Of classDatosAgente)
                                                    objRespuesta.Add(objDatosAgente.Item(i))
                                                Else
                                                    If objDatosAgente.Item(i).NombreAgente IsNot Nothing Then
                                                        If objDatosAgente.Item(i).NombreAgente.Contains("MOTOR -WEB SABRE") Then
                                                            If objRespuesta Is Nothing Then objRespuesta = New List(Of classDatosAgente)
                                                            objDatosAgente.Item(i).CorreoAgente = IIf(String.IsNullOrEmpty(objDatosAgente.Item(i).CorreoAgente), Constantes.emailSupervisoraCounterIA, objDatosAgente.Item(i).CorreoAgente & Constantes.PuntoComa & Constantes.emailSupervisoraCounterIA)
                                                            objRespuesta.Add(objDatosAgente.Item(i))
                                                            'Else
                                                            '    If objRespuesta Is Nothing Then objRespuesta = New List(Of classDatosAgente)
                                                            '    objRespuesta.Add(objDatosAgente.Item(i))
                                                        End If
                                                    End If

                                                End If

                                            End If
                                        End If
                                    End If
                                End If ' ---
                            End If
                        End If
                    Next

                End If



            Catch ex As Exception
                objRespuesta = Nothing
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strFirmaAgente = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDatosAgente = Nothing
            End Try

            Return objRespuesta
        End Function
        Public Function ObtenerDatosAgenteGeneral(ByVal strFirmaAgente As String, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As List(Of classDatosAgente)

            Dim objDAO As DAO = Nothing
            Dim objDatosAgente As List(Of classDatosAgente) = Nothing
            Dim objRespuesta As List(Of classDatosAgente) = Nothing
            Try

                objDAO = New DAO
                objDatosAgente = objDAO.ObtenerDatosAgente(strFirmaAgente, _
                                                           strCodigoSeguimiento, _
                                                           intFirmaDB, _
                                                           intEsquema)

                If objDatosAgente IsNot Nothing Then
                    For i As Integer = 0 To objDatosAgente.Count - 1
                        If objRespuesta Is Nothing Then objRespuesta = New List(Of classDatosAgente)
                        objDatosAgente.Item(i).CorreoAgente = IIf(String.IsNullOrEmpty(objDatosAgente.Item(i).CorreoAgente), Constantes.emailGDS2, objDatosAgente.Item(i).CorreoAgente)
                        objRespuesta.Add(objDatosAgente.Item(i))
                    Next
                End If

            Catch ex As Exception
                objRespuesta = Nothing
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strFirmaAgente = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDatosAgente = Nothing
            End Try

            Return objRespuesta
        End Function

        Public Function ObtenerDatosAgenteDINNERS(ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As List(Of classDatosAgente)

            Dim objDAO As DAO = Nothing
            Dim objDatosAgente As List(Of classDatosAgente) = Nothing
            Dim objRespuesta As List(Of classDatosAgente) = Nothing
            Try

                objDAO = New DAO
                objDatosAgente = objDAO.ObtenerDatosAgenteDINNERS(strCodigoSeguimiento, _
                                                                  intFirmaDB, _
                                                                  intEsquema)

                If objDatosAgente IsNot Nothing Then
                    For i As Integer = 0 To objDatosAgente.Count - 1
                        If objRespuesta Is Nothing Then objRespuesta = New List(Of classDatosAgente)
                        objDatosAgente.Item(i).CorreoAgente = IIf(String.IsNullOrEmpty(objDatosAgente.Item(i).CorreoAgente), Constantes.emailGDS2, objDatosAgente.Item(i).CorreoAgente)
                        objRespuesta.Add(objDatosAgente.Item(i))
                    Next
                End If

            Catch ex As Exception
                objRespuesta = Nothing
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                objDatosAgente = Nothing
            End Try

            Return objRespuesta
        End Function

        Public Function ObtenerAgenteEmite(ByVal strTicketNumber As String, _
                                           ByVal strCodigoPNR As String, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As String

            Dim objDAO As DAO = Nothing
            Dim AgenteEmite As String = Nothing
            Try

                objDAO = New DAO
                AgenteEmite = objDAO.ObtenerAgenteEmite(strTicketNumber, _
                                                        strCodigoPNR, _
                                                        strCodigoSeguimiento, _
                                                        intFirmaDB, _
                                                        intEsquema)


            Catch ex As Exception
                AgenteEmite = Nothing
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return AgenteEmite
        End Function

        Public Function ObtenerVendedorPtaDestinos(ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As List(Of classDatosAgente)

            Dim objDAO As DAO = Nothing
            Dim objDatosAgente As List(Of classDatosAgente) = Nothing
            Try

                objDAO = New DAO
                objDatosAgente = objDAO.ObtenerVendedorPtaDestinos(strCodigoSeguimiento, _
                                                                  intFirmaDB, _
                                                                  intEsquema)


            Catch ex As Exception
                objDatosAgente = Nothing
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objDatosAgente
        End Function

        Public Function ObtenerVendedorNuevoMundo(ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As List(Of classDatosAgente)

            Dim objDAO As DAO = Nothing
            Dim objDatosAgente As List(Of classDatosAgente) = Nothing
            Try

                objDAO = New DAO
                objDatosAgente = objDAO.ObtenerVendedorNuevoMundo(strCodigoSeguimiento, _
                                                                  intFirmaDB, _
                                                                  intEsquema)


            Catch ex As Exception
                objDatosAgente = Nothing
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objDatosAgente
        End Function

        Public Function ObtenerVendedoresPorEmpresa(ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As List(Of classDatosAgente)

            Dim objDAO As DAO = Nothing
            Dim objDatosAgente As List(Of classDatosAgente) = Nothing
            Try

                objDAO = New DAO
                objDatosAgente = objDAO.ObtenerVendedorNuevoMundo(strCodigoSeguimiento, _
                                                                  intFirmaDB, _
                                                                  intEsquema)


            Catch ex As Exception
                objDatosAgente = Nothing
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objDatosAgente
        End Function
        '
    End Class
End Namespace