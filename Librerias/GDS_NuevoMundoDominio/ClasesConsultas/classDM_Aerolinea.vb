Imports GDS_NuevoMundoPersistencia
Imports GDS_NM_Mensajeria
Imports classDO = GDS_NuevoMundoAccesoDatos
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Namespace ObjetoDominioNegocio
    Public Class classBO
        Public Function ObtenerDatosAerolinea(ByVal strCodigoAerolinea As String, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer) As classDatosAerolinea

            Dim objDAO As New DAO
            Dim objDatosAerolinea As classDatosAerolinea = Nothing
            Try

                objDatosAerolinea = objDAO.ObtenerDatosAerolinea(strCodigoAerolinea, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strCodigoAerolinea = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objDatosAerolinea

        End Function
        'Public Function ObtieneCodigoOperador(ByVal strCadenaAerolinea As String, _
        '                                         ByVal strCodigoSeguimiento As String, _
        '                                         ByVal iCnx As Integer) As String


        '    Dim strCodigoAerolinea As String = Nothing

        '    Try


        '        strCodigoAerolinea = objDAO.ObtieneCodigoOperador(strCadenaAerolinea, strCodigoSeguimiento, iCnx)

        '    Catch ex As Exception
        '        Throw New Exception(ex.Source.ToString & " " & ex.Message.ToString)
        '    Finally
        '        objBOWS = Nothing
        '        strCodigoSeguimiento = Nothing
        '        iCnx = Nothing
        '    End Try

        '    Return strCodigoAerolinea

        'End Function
    End Class
End Namespace