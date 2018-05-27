Imports GDS_NuevoMundoPersistencia
Imports GDS_NM_Mensajeria
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Imports BOWS = GDS_NuevoMundoDominioWS_Sabre
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function ObtenerPais(ByVal strCodigoSeguimiento As String, _
                                    ByVal intFirmaDB As Integer, _
                                    ByVal intEsquema As Integer) As List(Of classPais)


            Dim objDAO As DAO = Nothing
            Dim objPais As List(Of classPais) = Nothing
            Try

                objDAO = New DAO
                objPais = objDAO.ObtenerPais(strCodigoSeguimiento, _
                                             intFirmaDB, _
                                             intEsquema)




            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objPais

        End Function
        Public Function ObtenerDatosCiudad(ByVal strCodigo As String, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intFirmaDB As Integer, _
                                           ByVal intEsquema As Integer) As classCiudad


            Dim objDAO As DAO = Nothing
            Dim objCiudad As classCiudad = Nothing
            Try

                objDAO = New DAO
                objCiudad = objDAO.ObtenerDatosCiudad(strCodigo, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                objCiudad = New classCiudad
                objCiudad.CodCiudad = strCodigo
                objCiudad.Codigo = ex.ToString
                'Throw New Exception(ex.Source.ToString & " " & ex.Message.ToString)
            Finally
                objDAO = Nothing
                strCodigo = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objCiudad

        End Function
        Public Function ObtenerListaDatosCiudad(ByVal strTextoBusqueda As String, _
                                                ByVal strCodigoSeguimiento As String, _
                                                ByVal intFirmaDB As Integer, _
                                                ByVal intEsquema As Integer) As List(Of classCiudad)


            Dim objDAO As DAO = Nothing
            Dim objCiudad As List(Of classCiudad)
            Try

                objDAO = New DAO
                objCiudad = objDAO.ObtenerListaDatosCiudad(strTextoBusqueda, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strTextoBusqueda = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objCiudad

        End Function
    End Class
End Namespace