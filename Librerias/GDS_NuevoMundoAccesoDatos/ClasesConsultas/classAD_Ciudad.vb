Imports GDS_NuevoMundoPersistencia
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Namespace ObjetoAccesoDatos
    Partial Public Class classDAO
        Public Function ObtenerDatosCiudad(ByVal strCodigo As String, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As classCiudad

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objclassCiudad As classCiudad = Nothing
            Dim strREGION As String = Nothing
            Dim strAPTO As String = Nothing
            Dim strTREN As String = Nothing
            Dim strNAC As String = Nothing

            Dim strNOMBRE_CIUDAD As String = Nothing
            Dim strNOMBRE_PAIS As String = Nothing

            Try


                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spCIUDAD, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Codigo", strCodigo.Trim(), OracleDbType.Varchar2, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)

                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    objclassCiudad = New classCiudad

                    objclassCiudad.Codigo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CODIGO", Nothing)
                    objclassCiudad.CodCiudad = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CIUDAD", Nothing)
                    objclassCiudad.CodPais = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAIS", Nothing)

                    strREGION = ConnNM.LeeColumnasDataReader(objOracleDataReader, "REGION", "x")
                    strAPTO = ConnNM.LeeColumnasDataReader(objOracleDataReader, "APTO", "x")
                    strTREN = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TREN", "x")
                    strNAC = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NAC", "x")
                    strNOMBRE_CIUDAD = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE_CIUDAD", "x")
                    strNOMBRE_PAIS = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE_PAIS", "x")

                    If strREGION.Equals("x") Then
                        Err.Raise(3, "", "No se encontro información de Región")
                    ElseIf strAPTO.Equals("x") Then
                        Err.Raise(4, "", "No se encontro información de Aeropuesto")
                    ElseIf strTREN.Equals("x") Then
                        Err.Raise(5, "", "No se encontro información de Estación de Tren")
                    ElseIf strNAC.Equals("x") Then
                        Err.Raise(6, "", "No se encontro información de Nacional/Internacional")
                    End If

                    objclassCiudad.CodRegion = strREGION
                    objclassCiudad.EsNacional = strNAC
                    objclassCiudad.NomCiudad = strNOMBRE_CIUDAD
                    objclassCiudad.NomPais = strNOMBRE_PAIS


                    If strAPTO.Equals(strTREN) Then
                        objclassCiudad.Tipo = strAPTO.ToString
                    ElseIf strAPTO.Equals(Convert.ToString(1)) Then
                        objclassCiudad.Tipo = Convert.ToString(1)
                    Else
                        objclassCiudad.Tipo = Convert.ToString(2)
                    End If

                End While

            Catch ex As Exception
                strLog = "Stored Procedure : " & Constantes.spCIUDAD & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Codigo : " & strCodigo.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerDatosCiudad" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerDatosCiudad", strCodigoSeguimiento)

                Err.Raise(3, "ObtenerDatosCiudad", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                strREGION = Nothing
                strAPTO = Nothing
                strTREN = Nothing
                strNAC = Nothing
                strNOMBRE_CIUDAD = Nothing
                strNOMBRE_PAIS = Nothing
            End Try

            Return objclassCiudad

        End Function
        Public Function ObtenerPais(ByVal strCodigoSeguimiento As String, _
                                    ByVal intFirmaDB As Integer, _
                                    ByVal intEsquema As Integer) As List(Of classPais)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objPais As List(Of classPais) = Nothing
            Dim auxPais As classPais = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spPAIS, Constantes.StoredProcedure)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxPais = New classPais
                    auxPais.Codigo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "ID_PAIS", Nothing)
                    auxPais.Valor = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE", Nothing)
                    If objPais Is Nothing Then objPais = New List(Of classPais)
                    objPais.Add(auxPais)
                End While

            Catch ex As Exception
                objPais = Nothing
                strLog = "Stored Procedure : " & Constantes.spPAIS & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerPais" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerPais", strCodigoSeguimiento)

                Err.Raise(13, "ObtenerPais", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                auxPais = Nothing
            End Try

            Return objPais

        End Function
        Public Function ObtenerListaDatosCiudad(ByVal strTextoBusqueda As String, _
                                                ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As List(Of classCiudad)

            Dim ConnNM As New MyConnectionOracle
            Dim objOracleDataReader As OracleDataReader = Nothing
            Dim objclassCiudad As classCiudad = Nothing
            Dim strREGION As String = Nothing
            Dim strAPTO As String = Nothing
            Dim strTREN As String = Nothing
            Dim strNAC As String = Nothing

            Dim strNOMBRE_CIUDAD As String = Nothing
            Dim strNOMBRE_PAIS As String = Nothing

            Dim objCiudades As List(Of classCiudad) = Nothing
            Dim auxCiudades As classCiudad = Nothing

            Try

                ConnNM.Connect(intFirmaDB)
                ConnNM.SP_Command(Constantes.spCIUDADES, Constantes.StoredProcedure)
                ConnNM.AgregarParametro("@p_Codigo", strTextoBusqueda.ToString, OracleDbType.Varchar2, 0, ParameterDirection.Input)
                '----------
                ConnNM.AgregarParametro("@p_Cursor", Nothing, OracleDbType.RefCursor, 0, ParameterDirection.Output)


                objOracleDataReader = ConnNM._ExecuteReader()

                While objOracleDataReader.Read
                    auxCiudades = New classCiudad

                    auxCiudades.Codigo = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CODIGO", Nothing)
                    auxCiudades.CodCiudad = ConnNM.LeeColumnasDataReader(objOracleDataReader, "CIUDAD", Nothing)
                    auxCiudades.CodPais = ConnNM.LeeColumnasDataReader(objOracleDataReader, "PAIS", Nothing)

                    strREGION = ConnNM.LeeColumnasDataReader(objOracleDataReader, "REGION", "x")
                    strAPTO = ConnNM.LeeColumnasDataReader(objOracleDataReader, "APTO", "x")
                    strTREN = ConnNM.LeeColumnasDataReader(objOracleDataReader, "TREN", "x")
                    strNAC = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NAC", "x")
                    strNOMBRE_CIUDAD = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE_CIUDAD", "x")
                    strNOMBRE_PAIS = ConnNM.LeeColumnasDataReader(objOracleDataReader, "NOMBRE_PAIS", "x")

                    If strREGION.Equals("x") Then
                        Err.Raise(3, "", "No se encontro información de Región")
                    ElseIf strAPTO.Equals("x") Then
                        Err.Raise(4, "", "No se encontro información de Aeropuesto")
                    ElseIf strTREN.Equals("x") Then
                        Err.Raise(5, "", "No se encontro información de Estación de Tren")
                    ElseIf strNAC.Equals("x") Then
                        Err.Raise(6, "", "No se encontro información de Nacional/Internacional")
                    End If

                    auxCiudades.CodRegion = strREGION
                    auxCiudades.EsNacional = strNAC
                    auxCiudades.NomCiudad = strNOMBRE_CIUDAD
                    auxCiudades.NomPais = strNOMBRE_PAIS


                    If strAPTO.Equals(strTREN) Then
                        auxCiudades.Tipo = strAPTO.ToString
                    ElseIf strAPTO.Equals(Convert.ToString(1)) Then
                        auxCiudades.Tipo = Convert.ToString(1)
                    Else
                        auxCiudades.Tipo = Convert.ToString(2)
                    End If

                    If objCiudades Is Nothing Then objCiudades = New List(Of classCiudad)
                    objCiudades.Add(auxCiudades)

                End While

            Catch ex As Exception

                strLog = "Stored Procedure : " & Constantes.spCIUDAD & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Codigo : " & strTextoBusqueda.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "ObtenerListaDatosCiudad" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "ObtenerListaDatosCiudad", strCodigoSeguimiento)

                Err.Raise(3, "ObtenerListaDatosCiudad", ex.ToString)

            Finally
                ConnNM.Disconnect()
                objOracleDataReader = Nothing
                strTextoBusqueda = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
                strREGION = Nothing
                strAPTO = Nothing
                strTREN = Nothing
                strNAC = Nothing
                strNOMBRE_CIUDAD = Nothing
                strNOMBRE_PAIS = Nothing
            End Try

            Return objCiudades

        End Function
    End Class
End Namespace