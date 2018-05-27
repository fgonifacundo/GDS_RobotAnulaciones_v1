Imports Persistencia = GDS_NuevoMundoPersistencia
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Private objConceptos As New Persistencia.classConceptosEvaluacion
        Private strTipoPax As String = Nothing
        Public Function LlenaConceptos(ByVal intCodigoCliente As Integer, _
                                       ByVal intSubCodigo As Integer, _
                                       ByVal strPseudoConsulta As String, _
                                       ByVal strPseudoEmisor As String, _
                                       ByVal objSegmentos As List(Of Persistencia.classSegmentos), _
                                       ByVal objTarifa As Persistencia.classTarifa, _
                                       ByVal objTipoPasajero As List(Of Persistencia.classDatosTipoPasajero), _
                                       ByVal objCiudadDestino As Persistencia.classCiudad, _
                                       ByVal strStock As String, _
                                       ByVal strCodigoSeguimiento As String, _
                                       ByVal intFirmaDB As Integer, _
                                       ByVal intEsquema As Integer) As Persistencia.classConceptosEvaluacion

            Dim intSegmentoRetorno As Integer = 0
            Dim strCiudadConexAplica As String = String.Empty

            Dim objIata As Persistencia.classIata

            Try

                objConceptos.CLIENTE = intCodigoCliente
                objConceptos.SUBCODIGO = intSubCodigo
                objConceptos.PSEUDO = strPseudoConsulta

                '*** OBTENEMOS LA IATA SEGÚN EL PSEUDO DE CONSULTA ***
                objIata = ObtenerIATA(strPseudoConsulta, strCodigoSeguimiento, intFirmaDB, intEsquema)
                objConceptos.IATACONSULTA = objIata.Iata
                '*****************************************************

                '*** OBTENEMOS LA IATA SEGÚN EL PSEUDO DE EMISION ***
                objIata = ObtenerIATA(strPseudoEmisor, strCodigoSeguimiento, intFirmaDB, intEsquema)
                objConceptos.IATAEMISION = objIata.Iata
                objConceptos.SUCURSAL = objIata.Sucursal
                '*****************************************************


                intSegmentoRetorno = LlenaConceptosItinerario(objSegmentos, _
                                                              objTarifa.LineaValidadora, _
                                                              objCiudadDestino, _
                                                              strStock, _
                                                              strCodigoSeguimiento, _
                                                              intFirmaDB, _
                                                              intEsquema)

                LlenaConceptosTarifa(objTarifa, _
                                     objTipoPasajero, _
                                     intSegmentoRetorno)

                strCiudadConexAplica = IIf(String.IsNullOrEmpty(objConceptos.CIUDADCONEXDESTINO), "", objConceptos.CIUDADCONEXDESTINO)
                strCiudadConexAplica = strCiudadConexAplica & IIf(String.IsNullOrEmpty(strCiudadConexAplica), "", ",") & IIf(String.IsNullOrEmpty(objConceptos.CIUDADCONEXRETORNO), "", objConceptos.CIUDADCONEXRETORNO)

                objConceptos.CIUDADCONEXAPLICA = IIf(String.IsNullOrEmpty(strCiudadConexAplica), "", strCiudadConexAplica)

            Catch ex As Exception
                objConceptos = Nothing
                Throw New Exception(ex.ToString)
            Finally
                intCodigoCliente = Nothing
                intSubCodigo = Nothing
                strPseudoConsulta = Nothing
                objSegmentos = Nothing
                objTarifa = Nothing
                objCiudadDestino = Nothing
                strStock = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objConceptos

        End Function
        Private Function LlenaConceptosItinerario(ByVal objSegmentos As List(Of Persistencia.classSegmentos), _
                                                  ByVal strLineaValidadora As String, _
                                                  ByVal objCiudadDestino As Persistencia.classCiudad, _
                                                  ByVal strStock As String, _
                                                  ByVal strCodigoSeguimiento As String, _
                                                  ByVal intFirmaDB As Integer, _
                                                  ByVal intEsquema As Integer) As Integer

            Dim bolSegmentosSalida As Boolean = True
            Dim bolAuxSegmentosSalida As Boolean = True
            Dim strLineasEquivalentes As String = String.Empty
            Dim strLineasHomologas As String = String.Empty
            Dim bolBusca_Ciudad_En_Llegadas As Boolean = False

            Dim intSegmentoRetorno As Integer = 0
            Dim intContador As Integer = 0
            Try

                If objSegmentos IsNot Nothing Then

                    objConceptos.LINEAVALIDADORA = String.Empty

                    objConceptos.CIUDESTINO = String.Empty
                    objConceptos.CIUORIGEN = String.Empty
                    objConceptos.PAISORIGEN = String.Empty
                    objConceptos.FECSALIDA = String.Empty
                    objConceptos.CIUAUXSAL = String.Empty
                    objConceptos.CIUFINVIAJE = String.Empty
                    objConceptos.PAISFINVIAJE = String.Empty

                    objConceptos.NOVUELO = String.Empty
                    objConceptos.NVUELOSALIDA = String.Empty
                    objConceptos.NVUELORETORNO = String.Empty

                    objConceptos.CLASRESERV = String.Empty
                    objConceptos.CLASCABSAL = String.Empty
                    objConceptos.CLASCABRET = String.Empty

                    objConceptos.TIPORUTA = "C"
                    objConceptos.TIPOVUELO = "ON"
                    objConceptos.LINAEREAAUX = String.Empty
                    objConceptos.CODSHARELINAEREA = String.Empty
                    objConceptos.CODSHARE = "NO"

                    objConceptos.CIUDADCONEXDESTINO = String.Empty
                    objConceptos.CIUDADCONEXRETORNO = String.Empty

                    objConceptos.CIUREGRESO = String.Empty
                    objConceptos.CIUAUXRET = String.Empty
                    objConceptos.FECRETORNO = String.Empty
                    objConceptos.PAISRETORNO = String.Empty
                    objConceptos.REGRET = String.Empty

                    objConceptos.TIPOVIAJE = String.Empty
                    objConceptos.TIPOSTOCK = String.Empty

                    objConceptos.CIUDESTINO = objCiudadDestino.CodCiudad
                    objConceptos.PAISDESTINO = objCiudadDestino.CodPais
                    objConceptos.REGDES = objCiudadDestino.CodRegion

                    If Not String.IsNullOrEmpty(strStock) Then
                        objConceptos.TIPOSTOCK = strStock
                    Else
                        Err.Raise(-1111, "classDM_RecopilarDatos.LlenaConceptosItinerario", "No se encuentró valor en strStock")
                    End If



                    If String.IsNullOrEmpty(objConceptos.CIUDESTINO) Then
                        Err.Raise(-1111, "classDM_RecopilarDatos.LlenaConceptosItinerario", "No se encuentra la ciudad destino")
                    End If


                    bolBusca_Ciudad_En_Llegadas = UbicaCiudadDestino(objSegmentos, _
                                                                     objConceptos.CIUDESTINO)



                    '*** OBTIENE LÍNEA AÉREA HOMÓLOGA ***
                    strLineaValidadora = CambioAerolinea(strLineaValidadora, strCodigoSeguimiento, intFirmaDB, intEsquema)
                    objConceptos.LINEAVALIDADORA = strLineaValidadora
                    '************************************

                    '*** OBTIENE LÍNEA AÉREA HOMÓLOGA ***
                    strLineasHomologas = AerolineaHomologa(strLineaValidadora, strCodigoSeguimiento, intFirmaDB, intEsquema)

                    If Not String.IsNullOrEmpty(strLineasHomologas) Then
                        If Not strLineasHomologas.Contains(strLineaValidadora) Then
                            strLineasHomologas &= Constantes.Coma & strLineaValidadora
                        End If
                    Else
                        strLineasHomologas = strLineaValidadora
                    End If
                    '************************************

                    '*** OBTIENE LÍNEA AÉREA EQUIVALENTE ***
                    strLineasEquivalentes = AerolineaEquivalente(strLineaValidadora, strCodigoSeguimiento, intFirmaDB, intEsquema)

                    If Not String.IsNullOrEmpty(strLineasEquivalentes) Then
                        strLineaValidadora &= Constantes.Slash & strLineasEquivalentes
                    End If
                    '***************************************


                    For i As Integer = 0 To objSegmentos.Count - 1

                        If objSegmentos.Item(i).Marca Then



                            If Not bolAuxSegmentosSalida Then bolSegmentosSalida = bolAuxSegmentosSalida

                            If String.IsNullOrEmpty(objConceptos.CIUORIGEN) Then

                                If objSegmentos.Item(i).Salida IsNot Nothing Then
                                    'Ciudad origen
                                    objConceptos.CIUORIGEN = Trim(objSegmentos.Item(i).Salida.CodCiudad.ToString)
                                    'Pais origen 
                                    objConceptos.PAISORIGEN = Trim(objSegmentos.Item(i).Salida.CodPais.ToString)

                                Else
                                    Err.Raise(-1111, "classDM_RecopilarDatos.LlenaConceptosItinerario", "No existe datos en salidas en indice " & i)
                                End If

                                'Fecha salida
                                Dim date1 As Date = objSegmentos.Item(i).FechaHoraSalida
                                objConceptos.FECSALIDA = Trim(CStr(date1.ToString(Constantes.IWS_DATE_FORMAT_FILE6)))
                                'Ciudad auxiliar de salida
                                If strLineaValidadora.Contains(objSegmentos.Item(i).Aerolinea.ToString) Then
                                    objConceptos.CIUAUXSAL = objConceptos.CIUORIGEN
                                End If
                            End If

                            'Si bolBusca_Ciudad_En_Llegadas = True el corte de tarifa se debe buscar en Llegadas
                            'caso contrario se buscará en salida, con esta información obtendremos: Ciudad retorno, 
                            'País retorno, Región retorno, Ciudad Auxiliar de Retorno
                            If bolBusca_Ciudad_En_Llegadas Then
                                If objConceptos.CIUDESTINO = Trim(objSegmentos.Item(i).Llegada.CodCiudad.ToString) Then
                                    If bolSegmentosSalida Then
                                        'Salidas
                                        bolAuxSegmentosSalida = False
                                    End If
                                Else
                                    If bolSegmentosSalida Then
                                        'Salidas
                                        bolAuxSegmentosSalida = False
                                    Else
                                        '************************
                                        '***DATOS DE RETORNO ****
                                        '************************

                                        'Ciudad de Retorno
                                        If String.IsNullOrEmpty(objConceptos.CIUREGRESO) Then
                                            intSegmentoRetorno = intContador
                                            objConceptos.CIUREGRESO = Trim(objSegmentos.Item(i).Salida.CodCiudad.ToString)
                                            Dim date1 As Date = objSegmentos.Item(i).FechaHoraSalida
                                            'Fecha retorno
                                            objConceptos.FECRETORNO = Trim(CStr(date1.ToString(Constantes.IWS_DATE_FORMAT_FILE6)))
                                        End If
                                        'Ciudad auxiliar de retorno
                                        If String.IsNullOrEmpty(objConceptos.CIUAUXRET) Then
                                            If strLineaValidadora.Contains(objSegmentos.Item(i).Aerolinea) Then
                                                objConceptos.CIUAUXRET = Trim(objSegmentos.Item(i).Salida.CodCiudad.ToString)
                                            End If
                                        End If
                                        'País de retorno
                                        If String.IsNullOrEmpty(objConceptos.PAISRETORNO) Then
                                            objConceptos.PAISRETORNO = Trim(objSegmentos.Item(i).Salida.CodPais.ToString)
                                        End If
                                        'Región de retorno
                                        If String.IsNullOrEmpty(objConceptos.REGRET) Then
                                            objConceptos.REGRET = Trim(objSegmentos.Item(i).Salida.CodRegion.ToString)
                                        End If

                                    End If
                                End If
                            Else
                                If objConceptos.CIUDESTINO = Trim(objSegmentos.Item(i).Salida.CodCiudad.ToString) Then
                                    bolAuxSegmentosSalida = False
                                    bolSegmentosSalida = False

                                    '************************
                                    '***DATOS DE RETORNO ****
                                    '************************

                                    'Ciudad de Retorno
                                    If String.IsNullOrEmpty(objConceptos.CIUREGRESO) Then
                                        intSegmentoRetorno = intContador

                                        objConceptos.CIUREGRESO = Trim(objSegmentos.Item(i).Salida.CodCiudad.ToString)
                                        Dim date1 As Date = objSegmentos.Item(i).FechaHoraSalida
                                        'Fecha retorno
                                        objConceptos.FECRETORNO = Trim(CStr(date1.ToString(Constantes.IWS_DATE_FORMAT_FILE6)))
                                    End If
                                    'Ciudad auxiliar de retorno
                                    If String.IsNullOrEmpty(objConceptos.CIUAUXRET) Then
                                        If strLineaValidadora.Contains(objSegmentos.Item(i).Aerolinea) Then
                                            objConceptos.CIUAUXRET = Trim(objSegmentos.Item(i).Salida.CodCiudad.ToString)
                                        End If
                                    End If
                                    'País de retorno
                                    If String.IsNullOrEmpty(objConceptos.PAISRETORNO) Then
                                        objConceptos.PAISRETORNO = Trim(objSegmentos.Item(i).Salida.CodPais.ToString)
                                    End If
                                    'Región de retorno
                                    If String.IsNullOrEmpty(objConceptos.REGRET) Then
                                        objConceptos.REGRET = Trim(objSegmentos.Item(i).Salida.CodRegion.ToString)
                                    End If
                                End If
                            End If


                            If objSegmentos.Item(i).Llegada IsNot Nothing Then
                                'Ciudad fin de viaje
                                If Not String.IsNullOrEmpty(objSegmentos.Item(i).Llegada.CodCiudad) Then
                                    objConceptos.CIUFINVIAJE = Trim(objSegmentos.Item(i).Llegada.CodCiudad.ToString)
                                End If
                                'País fin de viaje
                                If Not String.IsNullOrEmpty(objSegmentos.Item(i).Llegada.CodPais) Then
                                    objConceptos.PAISFINVIAJE = Trim(objSegmentos.Item(i).Llegada.CodPais.ToString)
                                End If


                                'Ciudad de conexión destino y retorno 
                                'se solicito para el over de AA desde el 20/03/2013
                                If bolSegmentosSalida Then
                                    If Not objConceptos.CIUDADCONEXDESTINO.Contains(Trim(objSegmentos.Item(i).Llegada.CodCiudad)) Then
                                        If objConceptos.CIUDESTINO.Equals(Trim(objSegmentos.Item(i).Llegada.CodCiudad.ToString)) Then
                                            objConceptos.CIUDADCONEXDESTINO &= IIf(String.IsNullOrEmpty(objConceptos.CIUDADCONEXDESTINO), "", Constantes.Coma) & Trim(objSegmentos.Item(i).Llegada.CodCiudad.ToString)
                                        End If
                                    End If
                                Else
                                    If Not objConceptos.CIUDADCONEXRETORNO.Contains(Trim(objSegmentos.Item(i).Llegada.CodCiudad)) Then
                                        objConceptos.CIUDADCONEXRETORNO &= IIf(String.IsNullOrEmpty(objConceptos.CIUDADCONEXRETORNO), "", Constantes.Coma) & Trim(objSegmentos.Item(i).Llegada.CodCiudad.ToString)
                                    End If
                                End If

                            Else
                                Err.Raise(-1111, "classDM_RecopilarDatos.LlenaConceptosItinerario", "No existe datos en llegadas en indice " & i)
                            End If



                            'Tipo de Ruta 
                            If Not objConceptos.TIPORUTA.Equals("I") Then
                                If (objSegmentos.Item(i).Salida IsNot Nothing) And _
                                   (objSegmentos.Item(i).Llegada IsNot Nothing) Then
                                    If (Not String.IsNullOrEmpty(objSegmentos.Item(i).Salida.CodPais)) And _
                                       (Not String.IsNullOrEmpty(objSegmentos.Item(i).Llegada.CodPais)) Then

                                        If (Not objSegmentos.Item(i).Salida.CodPais.Equals("PE")) Or _
                                           (Not objSegmentos.Item(i).Llegada.CodPais.Equals("PE")) Then
                                            objConceptos.TIPORUTA = "I"
                                        End If
                                    End If
                                End If
                            End If


                            'Número de vuelos
                            If Not String.IsNullOrEmpty(objSegmentos.Item(i).NumVuelo) Then
                                'Todos los número de vuelos
                                If Not objConceptos.NOVUELO.Contains(Trim(objSegmentos.Item(i).NumVuelo)) Then
                                    objConceptos.NOVUELO &= IIf(String.IsNullOrEmpty(objConceptos.NOVUELO), "", Constantes.Coma) & Trim(objSegmentos.Item(i).NumVuelo.ToString)
                                End If

                                If bolSegmentosSalida Then
                                    'Todos los número de vuelos de salida
                                    If Not objConceptos.NVUELOSALIDA.Contains(Trim(objSegmentos.Item(i).NumVuelo)) Then
                                        objConceptos.NVUELOSALIDA &= IIf(String.IsNullOrEmpty(objConceptos.NVUELOSALIDA), "", Constantes.Coma) & Trim(objSegmentos.Item(i).NumVuelo.ToString)
                                    End If
                                Else
                                    'Todos los número de vuelos de retorno
                                    If Not objConceptos.NVUELORETORNO.Contains(Trim(objSegmentos.Item(i).NumVuelo)) Then
                                        objConceptos.NVUELORETORNO &= IIf(String.IsNullOrEmpty(objConceptos.NVUELORETORNO), "", Constantes.Coma) & Trim(objSegmentos.Item(i).NumVuelo.ToString)
                                    End If
                                End If
                            End If

                            'Clase de reserva
                            If Not String.IsNullOrEmpty(objSegmentos.Item(i).ClaseServicio) Then
                                'Todos las clases de reservas
                                If Not objConceptos.CLASRESERV.Contains(Trim(objSegmentos.Item(i).ClaseServicio)) Then
                                    objConceptos.CLASRESERV &= IIf(String.IsNullOrEmpty(objConceptos.CLASRESERV), "", Constantes.Coma) & Trim(objSegmentos.Item(i).ClaseServicio.ToString)
                                End If

                                If bolSegmentosSalida Then
                                    'Todos las clases de reserva de salida
                                    If Not objConceptos.CLASCABSAL.Contains(Trim(objSegmentos.Item(i).ClaseServicio)) Then
                                        objConceptos.CLASCABSAL &= IIf(String.IsNullOrEmpty(objConceptos.CLASCABSAL), "", Constantes.Coma) & Trim(objSegmentos.Item(i).ClaseServicio.ToString)
                                    End If
                                Else
                                    'Todos las clases de reserva de retorno
                                    If Not objConceptos.CLASCABRET.Contains(Trim(objSegmentos.Item(i).ClaseServicio)) Then
                                        objConceptos.CLASCABRET &= IIf(String.IsNullOrEmpty(objConceptos.CLASCABRET), "", Constantes.Coma) & Trim(objSegmentos.Item(i).ClaseServicio.ToString)
                                    End If
                                End If
                            End If



                            If Not strLineasHomologas.Contains(objSegmentos.Item(i).Aerolinea) Then

                                'Tipo de vuelo ON - OFF
                                If Not objConceptos.TIPOVUELO.Equals("OFF") Then
                                    objConceptos.TIPOVUELO = "OFF"
                                End If

                                'Línea aére auxiliar
                                If Not objConceptos.LINAEREAAUX.Contains(objSegmentos.Item(i).Aerolinea) Then
                                    objConceptos.LINAEREAAUX &= IIf(String.IsNullOrEmpty(objConceptos.LINAEREAAUX), "", Constantes.Coma) & Trim(objSegmentos.Item(i).Aerolinea.ToString)
                                End If

                            End If

                            'Cadena línea aérea codeshare
                            If Not String.IsNullOrEmpty(objSegmentos.Item(i).OperadoPor) Then
                                If Not strLineaValidadora.Contains(objSegmentos.Item(i).OperadoPor) Then
                                    If Not objConceptos.CODSHARELINAEREA.Contains(objSegmentos.Item(i).OperadoPor) Then
                                        objConceptos.CODSHARELINAEREA &= IIf(String.IsNullOrEmpty(objConceptos.CODSHARELINAEREA), "", Constantes.Coma) & objSegmentos.Item(i).OperadoPor.ToString
                                        If objConceptos.CODSHARE.Equals("NO") Then objConceptos.CODSHARE = "SI"
                                    End If
                                End If
                            End If

                            intContador += 1
                        End If

                    Next


                    'Tipo de viaje OW/RT
                    If objConceptos.CIUDESTINO.Equals(objConceptos.CIUFINVIAJE) Then
                        objConceptos.TIPOVIAJE = "OW"
                    Else
                        objConceptos.TIPOVIAJE = "RT"
                    End If

                    'Verificamos si el último valor en ciudad conexión de retorno es el mismo de ciudad fin de viaje
                    If Not String.IsNullOrEmpty(objConceptos.CIUDADCONEXRETORNO) Then
                        If objConceptos.CIUDADCONEXRETORNO.Length > 3 Then
                            If objConceptos.CIUDADCONEXRETORNO.Split(Constantes.Coma)(objConceptos.CIUDADCONEXRETORNO.Split(Constantes.Coma).Length - 1) = objConceptos.CIUFINVIAJE Then
                                objConceptos.CIUDADCONEXRETORNO = objConceptos.CIUDADCONEXRETORNO.Substring(0, objConceptos.CIUDADCONEXRETORNO.Length - 3)
                            End If
                        End If
                    End If

                    'Verificamos si existe CodeShare
                    If String.IsNullOrEmpty(objConceptos.CODSHARELINAEREA) Then
                        objConceptos.CODSHARELINAEREA = strLineaValidadora.Split(Constantes.Slash)(0)
                    End If

                    'Verificamos si existe Linea Aére Auxiliar
                    If String.IsNullOrEmpty(objConceptos.LINAEREAAUX) Then
                        objConceptos.LINAEREAAUX = strLineaValidadora.Split(Constantes.Slash)(0)
                    End If

                Else
                    Err.Raise(-1111, "classDM_RecopilarDatos.LlenaConceptosItinerario", "No se encuentró valor en objSegmentos")
                End If



            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                bolSegmentosSalida = Nothing
                bolAuxSegmentosSalida = Nothing
                strLineaValidadora = Nothing
                strLineasHomologas = Nothing
                objCiudadDestino = Nothing
                bolBusca_Ciudad_En_Llegadas = Nothing
                objSegmentos = Nothing
            End Try

            Return intSegmentoRetorno

        End Function
        Private Function LlenaConceptosTarifa(ByVal objTarifa As Persistencia.classTarifa, _
                                              ByVal objTipoPasajero As List(Of Persistencia.classDatosTipoPasajero), _
                                              ByVal intSegmentoRetorno As Integer) As Persistencia.classConceptosEvaluacion

            Dim strFareBasis As String = Nothing
            Dim strLetraFareBasis As String = Nothing

            Dim lstConceptos As List(Of Persistencia.classCETarifa) = Nothing
            Dim objAuxConceptos As Persistencia.classCETarifa = Nothing

            Try


                If objTarifa IsNot Nothing Then
                    If objTarifa.Tarifa_x_Pax IsNot Nothing Then

                        lstConceptos = New List(Of Persistencia.classCETarifa)

                        For i As Integer = 0 To objTarifa.Tarifa_x_Pax.Count - 1

                            objAuxConceptos = New Persistencia.classCETarifa

                            objAuxConceptos.FAREBASIS = String.Empty
                            objAuxConceptos.FBSINTKTDESIG = String.Empty
                            objAuxConceptos.CANTFARE = 0
                            objAuxConceptos.PrimeraLETFAREALL = String.Empty
                            objAuxConceptos.PrimerLETFARESAL = String.Empty
                            objAuxConceptos.PrimeraLETFARERET = String.Empty
                            objAuxConceptos.TIPOPAXESPECIAL = String.Empty
                            objAuxConceptos.TIPOPAX = String.Empty

                            '=========================================

                            objAuxConceptos.TIPOPAXESPECIAL = objTarifa.Tarifa_x_Pax.Item(i).TipoPax
                            strTipoPax = objTarifa.Tarifa_x_Pax.Item(i).TipoPax

                            Dim objTipoPax = objTipoPasajero.FindAll(AddressOf FindADT)
                            objAuxConceptos.TIPOPAX = objTipoPax.Item(0).Equivale


                            If objTarifa.Tarifa_x_Pax.Item(i).CorteTarifario IsNot Nothing Then

                                For x As Integer = 0 To objTarifa.Tarifa_x_Pax.Item(i).CorteTarifario.FareBasis.Count - 1

                                    If Not String.IsNullOrEmpty(objTarifa.Tarifa_x_Pax.Item(i).CorteTarifario.FareBasis.Item(i).sFareBasis) Then

                                        strFareBasis = objTarifa.Tarifa_x_Pax.Item(i).CorteTarifario.FareBasis.Item(x).sFareBasis

                                        strLetraFareBasis = objTarifa.Tarifa_x_Pax.Item(i).CorteTarifario.FareBasis.Item(x).sFareBasis.Substring(0, 1)

                                        'FareBasis
                                        If Not objAuxConceptos.FAREBASIS.Contains(strFareBasis) Then
                                            objAuxConceptos.FAREBASIS &= IIf(String.IsNullOrEmpty(objAuxConceptos.FAREBASIS), "", Constantes.Coma) & strFareBasis
                                            objAuxConceptos.FBSINTKTDESIG &= IIf(String.IsNullOrEmpty(objAuxConceptos.FBSINTKTDESIG), "", Constantes.Coma) & strFareBasis.Split(Constantes.Slash)(0)
                                        End If


                                        'FareBasis Sin Tkt Designator
                                        If Not objAuxConceptos.FBSINTKTDESIG.Contains(strFareBasis.Split(Constantes.Slash)(0)) Then
                                            objAuxConceptos.FBSINTKTDESIG &= IIf(String.IsNullOrEmpty(objAuxConceptos.FBSINTKTDESIG), "", Constantes.Coma) & strFareBasis.Split(Constantes.Slash)(0)
                                        End If

                                        'Primera letra de todos los fare basis
                                        If Not objAuxConceptos.PrimeraLETFAREALL.Contains(strLetraFareBasis) Then
                                            objAuxConceptos.PrimeraLETFAREALL &= IIf(String.IsNullOrEmpty(objAuxConceptos.PrimeraLETFAREALL), "", Constantes.Coma) & strLetraFareBasis
                                        End If

                                        If x < intSegmentoRetorno Then
                                            'Primera letra de los fare basis de salida
                                            If Not objAuxConceptos.PrimerLETFARESAL.Contains(strLetraFareBasis) Then
                                                objAuxConceptos.PrimerLETFARESAL &= IIf(String.IsNullOrEmpty(objAuxConceptos.PrimerLETFARESAL), "", Constantes.Coma) & strLetraFareBasis
                                            End If
                                        Else
                                            'Primera letra de los bare basis de retorno
                                            If Not objAuxConceptos.PrimeraLETFARERET.Contains(strLetraFareBasis) Then
                                                objAuxConceptos.PrimeraLETFARERET &= IIf(String.IsNullOrEmpty(objAuxConceptos.PrimeraLETFARERET), "", Constantes.Coma) & strLetraFareBasis
                                            End If
                                        End If


                                    Else
                                        Err.Raise(-1111, "classDM_RecopilarDatos.LlenaConceptosTarifa", "No se encuentró valor en objTarifa.Tarifa_x_Pax.Item(" & i & ").CorteTarifario.FareBasis.Item(" & x & ").sFareBasis")
                                    End If

                                Next

                                'Cantidad de FareBasis
                                If Not String.IsNullOrEmpty(objAuxConceptos.FAREBASIS) Then
                                    objAuxConceptos.CANTFARE = objAuxConceptos.FAREBASIS.Split(Constantes.Coma).Length
                                End If


                            Else
                                Err.Raise(-1111, "classDM_RecopilarDatos.LlenaConceptosTarifa", "No se encuentró valor en objTarifa.Tarifa_x_Pax.Item(" & i & ").CorteTarifario")
                            End If

                            If lstConceptos Is Nothing Then lstConceptos = New List(Of Persistencia.classCETarifa)
                            lstConceptos.Add(objAuxConceptos)

                        Next

                        If lstConceptos IsNot Nothing Then
                            If objConceptos.ConceptosTarifa Is Nothing Then objConceptos.ConceptosTarifa = New List(Of Persistencia.classCETarifa)
                            objConceptos.ConceptosTarifa = lstConceptos
                        Else
                            Err.Raise(-1111, "classDM_RecopilarDatos.LlenaConceptosTarifa", "No se encuentró valor en lstConceptos")
                        End If

                    Else
                        Err.Raise(-1111, "classDM_RecopilarDatos.LlenaConceptosTarifa", "No se encuentró valor en objTarifa.Tarifa_x_Pax")
                    End If
                Else
                    Err.Raise(-1111, "classDM_RecopilarDatos.LlenaConceptosTarifa", "No se encuentró valor en objTarifa")
                End If


            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strFareBasis = Nothing
                strLetraFareBasis = Nothing
                objTarifa = Nothing
                intSegmentoRetorno = Nothing
            End Try

            Return objConceptos
        End Function
        Private Function UbicaCiudadDestino(ByVal Segmentos As List(Of Persistencia.classSegmentos), _
                                            ByVal strCiudadDestino As String) As Boolean

            Dim bolRespuesta As Boolean = True

            Try
                If Segmentos IsNot Nothing Then
                    For i As Integer = 0 To Segmentos.Count - 1
                        If Segmentos.Item(i).Marca Then

                            'Verificamos si la ciudad destino se encuentra dentro de las salidas
                            If Segmentos.Item(i).Salida IsNot Nothing Then
                                If Segmentos.Item(i).Salida.CodCiudad.Equals(strCiudadDestino) Then
                                    bolRespuesta = False
                                    Exit For
                                End If
                            End If

                            'Verificamos si la ciudad destino se encuentra dentro de las llegadas
                            If Segmentos.Item(i).Llegada IsNot Nothing Then
                                If Segmentos.Item(i).Llegada.CodCiudad.Equals(strCiudadDestino) Then
                                    bolRespuesta = True
                                    Exit For
                                End If
                            End If

                        End If
                    Next
                End If

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                Segmentos = Nothing
                strCiudadDestino = Nothing
            End Try

            Return bolRespuesta

        End Function
        Private Function FindADT(ByVal temp As Persistencia.classDatosTipoPasajero) As Boolean
            Return temp.ID = strTipoPax

        End Function
    End Class
End Namespace
