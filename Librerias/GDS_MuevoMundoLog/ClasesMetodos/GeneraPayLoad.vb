Imports System.IO
Imports System.Xml.Serialization
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
<Serializable()> _
Public Class GeneraPayLoad
    Private objAppConfig As New GDS_NuevoMundoPersistencia.classAppConfig
    <NonSerialized()> Private oSerializer As XmlSerializer
    Public Function Serialize(ByVal pTypeRQ As System.Type, _
                               ByVal pTypeRS As System.Type, _
                               ByVal pObjetoRQ As Object, _
                               ByVal pObjetoRS As Object, _
                               ByVal pNombreWebService As String, _
                               ByVal strNombreCarpeta As String, _
                               ByVal pReturnXML As Boolean, _
                               ByVal pXmlMail As Boolean) As String

        Serializer(pTypeRQ, pObjetoRQ, pNombreWebService & Constantes.IWS_REQUEST, strNombreCarpeta, pReturnXML, pXmlMail)
        Serialize = Serializer(pTypeRS, pObjetoRS, pNombreWebService & Constantes.IWS_RESPONSE, strNombreCarpeta, pReturnXML, pXmlMail)
        Return Serialize

    End Function
    Public Function Serializer(ByVal pType As System.Type, _
                                ByVal pObjeto As Object, _
                                ByVal pNombre As String, _
                                ByVal strNombreCarpeta As String, _
                                ByVal pReturnXML As Boolean, _
                                ByVal pXmlMail As Boolean) As String

        Dim sFecha As String, sHora As String
        Dim oStreamWriter As StreamWriter
        Dim oTextReader As TextReader
        Dim SERIALIZATION_PATH As String = String.Empty
        Dim strCarpera As String = Nothing
        Serializer = Nothing
        sFecha = Format(Now, Constantes.IWS_DATE_FORMAT_FILE)
        sHora = Format(Now, Constantes.IWS_TIME_FORMAT_FILE)
        oSerializer = New XmlSerializer(pType)
        SERIALIZATION_PATH = RutaLOGXML()

        strCarpera = SERIALIZATION_PATH & sFecha & "\" & strNombreCarpeta.Split("#")(0)

        If System.IO.Directory.Exists(strCarpera) = False Then
            System.IO.Directory.CreateDirectory(strCarpera)
        End If

        If strNombreCarpeta.Split("#").Length = 1 Then
            oStreamWriter = New StreamWriter(strCarpera & "\" & sHora & "_" & strNombreCarpeta.Split("#")(0) & "_" & pNombre & ".xml")
        Else
            oStreamWriter = New StreamWriter(strCarpera & "\" & sHora & "_" & strNombreCarpeta.Split("#")(0) & "_" & pNombre & "_" & strNombreCarpeta.Split("#")(1) & ".xml")
        End If


        oSerializer.Serialize(oStreamWriter, pObjeto)
        oStreamWriter.Close()

        If pReturnXML = False Then
            pReturnXML = pXmlMail
        End If

        If pReturnXML Then
            If strNombreCarpeta.Split("#").Length = 1 Then
                oTextReader = New StreamReader(SERIALIZATION_PATH & "\" & sFecha & "\" & sFecha & "_" & sHora & "_" & pNombre & ".xml")
            Else
                oTextReader = New StreamReader(SERIALIZATION_PATH & "\" & sFecha & "\" & sFecha & "_" & sHora & "_" & pNombre & "_" & strNombreCarpeta.Split("#")(1) & ".xml")
            End If

            Serializer = oTextReader.ReadToEnd
            oTextReader.Close()
            oTextReader = Nothing
        End If
        oStreamWriter = Nothing

    End Function
    Private Function RutaLOGXML() As String
        Return objAppConfig.Obtiene_RUTA_FILE_PAYLOAD_SABRE()
    End Function
End Class
