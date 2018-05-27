Imports GDS_NuevoMundoPersistencia
'Imports IWebServices = GDS_NM_WebServicesSabre.IWebServices
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Imports objSabreWS = GDS_NM_WebServicesSabre
Imports EscribeLog = GDS_MuevoMundoLog.EscribeLog
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function SWS_Release(ByVal strPnr As String, _
                                    ByVal strPseudo As String, _
                                    ByVal strFirmaUsuario As String, _
                                    ByVal strCodigoSeguimiento As String, _
                                    ByVal intGDS As Integer, _
                                    ByVal intFirmaGDS As Integer, _
                                    ByVal intFirmaDB As Integer, _
                                    ByVal objSession As classSession) As String

            Dim strRespuestaSC As String = Nothing
            Dim objEndTransaction As classEndTransaction.classTransaction = Nothing
            Dim strRespuesta As String = "False"
            Try


                strRespuestaSC = SabreCommand("6" & Constantes.Change & "TA/" & strPseudo & "-" & strFirmaUsuario, _
                                              "Release", _
                                              strCodigoSeguimiento, _
                                              intGDS, _
                                              intFirmaGDS, _
                                              intFirmaDB, _
                                              objSession)



            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                strPnr = Nothing
                strPseudo = Nothing
                strFirmaUsuario = Nothing
                strCodigoSeguimiento = Nothing
                intGDS = Nothing
                intFirmaGDS = Nothing
                intFirmaDB = Nothing
                objSession = Nothing

            End Try

            Return strRespuestaSC

        End Function
    End Class
End Namespace
