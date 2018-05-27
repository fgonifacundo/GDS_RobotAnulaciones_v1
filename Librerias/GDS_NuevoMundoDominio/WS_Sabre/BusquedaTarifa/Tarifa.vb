Imports GDS_NuevoMundoPersistencia
Imports IWebServices = GDS_NM_WebServicesSabre.IWebServices
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports DAO = GDS_NuevoMundoAccesoDatos.ObjetoAccesoDatos.classDAO
Imports objSabreWS = GDS_NM_WebServicesSabre
Namespace ObjetoDominioNegocio
    Partial Public Class classBO
        Public Function InsertaTarifasFQ(ByVal objFQ As List(Of classFQ.classFareRS), _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As Boolean

            Dim strCadenaXML As System.Text.StringBuilder = Nothing
            'Dim objWSBusiness As WSBusiness = Nothing
            Dim bolRespuesta As Boolean = False

            'PNR()
            'AIRLINES()
            'PASSENGERTYPE()
            'FAREBASIS()
            'BOOKINGCLASS()
            'CURRENCY()
            'BASEFARE()
            'EXPIRATIONDATE()
            'EFFECTIVEDATE()
            'TICKETDATE()
            'ADVANCEPURCHASE()
            'MINSTAY()
            'MAXSTAY()
            'PSEUDO()
            'IDS()
            'NUMERO()
            'FEEMAXIMO()
            'FEEMINIMO()
            'DK()
            'ACCOUNTCODE()
            Try

                'strCadenaXML = New System.Text.StringBuilder

                'strCadenaXML.Append("<?xml version='1.0'?>")
                'strCadenaXML.Append("<ROWSET>")
                'For x As Integer = 0 To objFQ.Count - 1
                '    strCadenaXML.Append("<ROW num='" & x + 1 & "'>")
                '    strCadenaXML.Append("<DK>" & "23571" & "</DK>")
                '    strCadenaXML.Append("<IDS>" & x & "</IDS>")
                '    strCadenaXML.Append("<NUMERO>" & objFQ.Item(x).ID & "</NUMERO>")
                '    strCadenaXML.Append("<PNR>" & "PRUEBA" & "</PNR>")
                '    strCadenaXML.Append("<AIRLINES>" & objFQ.Item(x).AirLines & "</AIRLINES>")
                '    If objFQ.Item(x).PassengerType IsNot Nothing Then
                '        strCadenaXML.Append("<PASSENGERTYPE>" & objFQ.Item(x).PassengerType.Item(0).ID & "</PASSENGERTYPE>")
                '    End If
                '    strCadenaXML.Append("<FAREBASIS>" & objFQ.Item(x).FareBasis & "</FAREBASIS>")
                '    strCadenaXML.Append("<BOOKINGCLASS>" & objFQ.Item(x).BookingClass & "</BOOKINGCLASS>")
                '    strCadenaXML.Append("<CABINA>" & objFQ.Item(x).Cabin & "</CABINA>")
                '    strCadenaXML.Append("<CURRENCY>" & objFQ.Item(x).Currency & "</CURRENCY>")
                '    strCadenaXML.Append("<BASEFARE>" & objFQ.Item(x).BaseFare & "</BASEFARE>")
                '    strCadenaXML.Append("<EXPIRATIONDATE>" & objFQ.Item(x).ExpirationDate & "</EXPIRATIONDATE>")
                '    strCadenaXML.Append("<EFFECTIVEDATE>" & objFQ.Item(x).EffectiveDate & "</EFFECTIVEDATE>")
                '    strCadenaXML.Append("<TICKETDATE>" & objFQ.Item(x).TicketDate & "</TICKETDATE>")
                '    strCadenaXML.Append("<ADVANCEPURCHASE>" & objFQ.Item(x).AdvancePurchase & "</ADVANCEPURCHASE>")
                '    strCadenaXML.Append("<SEASONALAPPLIC>" & objFQ.Item(x).SeasonalApplication & "</SEASONALAPPLIC>")
                '    strCadenaXML.Append("<MINSTAY>" & objFQ.Item(x).MinStay & "</MINSTAY>")
                '    strCadenaXML.Append("<MAXSTAY>" & objFQ.Item(x).MaxStay & "</MAXSTAY>")

                '    If objFQ.Item(x).Pseudos IsNot Nothing Then
                '        strCadenaXML.Append("<PSEUDO>" & objFQ.Item(x).Pseudos.Item(0).Pseudo & "</PSEUDO>")
                '        strCadenaXML.Append("<IDPSEUDO>" & objFQ.Item(x).Pseudos.Item(0).IDs & "</IDPSEUDO>")
                '    Else
                '        strCadenaXML.Append("<PSEUDO> </PSEUDO>")
                '        strCadenaXML.Append("<IDPSEUDO> </IDPSEUDO>")
                '    End If

                '    If objFQ.Item(x).RuleCategory IsNot Nothing Then
                '        strCadenaXML.Append("<REGLAS>" & objFQ.Item(x).RuleCategory.Item(0) & "</REGLAS>")
                '    Else
                '        strCadenaXML.Append("<REGLAS> </REGLAS>")
                '    End If

                '    strCadenaXML.Append("</ROW>")
                'Next
                'strCadenaXML.Append("</ROWSET>")

                'objWSBusiness = New WSBusiness

                bolRespuesta = InsertaTarifasFQAlternativo(objFQ, strCodigoSeguimiento, intFirmaDB, intEsquema)

                'Try
                'bolRespuesta = objWSBusiness.InsertaTablaXML("TARIFABULK_FQ", strCadenaXML.ToString, strCodigoSeguimiento, iCnx)
                'Catch ex As Exception
                '    bolRespuesta = InsertaTarifasFQAlternativo(objFQ, strCodigoSeguimiento, iCnx)
                'End Try


            Catch ex As Exception
                bolRespuesta = False
                Throw New Exception(ex.ToString)
            Finally
                objFQ = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return bolRespuesta
        End Function
        Public Function InsertaTarifasFQAlternativo(ByVal objFQ As List(Of classFQ.classFareRS), _
                                                    ByVal strCodigoSeguimiento As String, _
                                                    ByVal intFirmaDB As Integer, _
                                                    ByVal intEsquema As Integer) As Boolean

            Dim objDAO As New DAO
            Dim bolRespuesta As Boolean = False
            Try

                bolRespuesta = objDAO.InsertaTarifasFQ(objFQ, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                objFQ = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return bolRespuesta

        End Function
        Public Function ObtenerTarifasFQ(ByVal strPNR As String, _
                                         ByVal strDK As String, _
                                         ByVal strCodigoSeguimiento As String, _
                                         ByVal intFirmaDB As Integer, _
                                         ByVal intEsquema As Integer) As List(Of classFQ.classFareRS)

            Dim objDAO As New DAO
            Dim objFareRS As List(Of classFQ.classFareRS) = Nothing
            Try

                objFareRS = objDAO.ObtenerTarifasFQ(strPNR, strDK, strCodigoSeguimiento, intFirmaDB, intEsquema)

            Catch ex As Exception
                Throw New Exception(ex.ToString)
            Finally
                objDAO = Nothing
                strPNR = Nothing
                strDK = Nothing
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
            End Try

            Return objFareRS

        End Function
    End Class
End Namespace