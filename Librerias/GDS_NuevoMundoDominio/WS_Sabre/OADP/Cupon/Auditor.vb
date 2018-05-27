Imports GDS_NuevoMundoPersistencia
'Imports IWebServices = GDS_NM_WebServicesSabre.IWebServices
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Imports objSabreWS = GDS_NM_WebServicesSabre
Imports EscribeLog = GDS_MuevoMundoLog.EscribeLog
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function GeneraCuponAuditor(ByVal TCTicket As classTicketCoupon.TCTicket, _
                                           ByVal strCodigoSeguimiento As String, _
                                           ByVal intGDS As Integer, _
                                           ByVal objSession As classSession, _
                                           ByVal oOp_Firma As Integer) As String

            Dim strCadenaCuponAuditor As System.Text.StringBuilder = Nothing

            Try


                If TCTicket IsNot Nothing Then
                    If TCTicket.ErroresAlertas Is Nothing Then
                        If TCTicket.TCCouponData IsNot Nothing Then
                            If TCTicket.TCCouponData.ItineraryRef IsNot Nothing Then

                            End If
                        End If

                        strCadenaCuponAuditor.Append("ELECTRONIC TICKET RECORD" & vbCrLf)
                        strCadenaCuponAuditor.Append(CompletarEspacios(22, "INV:", "D") & CompletarEspacios(31, "CUST:", "D") & vbCrLf)

                    End If
                End If

            Catch ex As Exception
                Throw New Exception(ex.InnerException.ToString)
            Finally

            End Try

        End Function
    End Class
End Namespace