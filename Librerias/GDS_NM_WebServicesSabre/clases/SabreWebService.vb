Option Explicit On
Option Strict On
Imports System.Xml
<Serializable()> _
Public Class SabreWebService
    '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    ' Clase                     : SabreWebService.vb                                      '
    ' Descripción               : Clase que lee el XML donde se guarda la lista de Sabre  '
    '                             Web Services que utiliza la aplicación                  '
    ' Creado por                : Bruno Doumenz Haman                                     '
    ' Aplicación                : ConexionAir 1.0                                         '
    ' Fecha de Creación         : 13/04/2007                                              '
    ' Fecha de actualizació0p nmn    : 15/06/2007                                         '
    ' '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
    'direccion local
    Private objAppConfig As New GDS_NuevoMundoPersistencia.classAppConfig
    Dim XML_CONFIGFILEPATH As String = objAppConfig.Obtiene_RUTA_FILE_CONFIGURACION

    Private Const XML_WEBSERVICES As String = "SabreWebService.xml"
    Private Const XML_ID_WEBSERVICE As String = "Id"
    Private oLista As Collection

    Public Sub New()
        Dim oXMLWebServices As New XmlDocument
        Dim oWebServices As XmlNode = Nothing
        Dim oWebService As XmlNode = Nothing
        Dim oSWS_SoapEnvelope As WS_Login_SOAPEnvelope.SOAP_Envelope
        Try
            oXMLWebServices.Load(XML_CONFIGFILEPATH & "\" & XML_WEBSERVICES)
            oWebServices = oXMLWebServices.ChildNodes(1)
            oLista = New Collection
            For Each oWebServices In oWebServices.ChildNodes
                For Each oWebService In oWebServices.ChildNodes
                    If oWebService.NodeType = XmlNodeType.Element Then
                        oSWS_SoapEnvelope = New WS_Login_SOAPEnvelope.SOAP_Envelope(oWebService.Attributes(XML_ID_WEBSERVICE).Value)
                        With oSWS_SoapEnvelope
                            .Name = oWebService.ChildNodes(0).InnerText
                            .Service = oWebService.ChildNodes(1).InnerText
                            .Action = oWebService.ChildNodes(2).InnerText
                            .Cid = oWebService.ChildNodes(3).InnerText
                            .Version = oWebService.ChildNodes(4).InnerText
                        End With
                        oLista.Add(oSWS_SoapEnvelope, oSWS_SoapEnvelope.Id)
                    End If
                Next
            Next
        Catch ex As Exception
            oXMLWebServices = Nothing
        Finally
            oXMLWebServices = Nothing
            oWebServices = Nothing
            oWebService = Nothing
            oSWS_SoapEnvelope = Nothing
        End Try
    End Sub
    Public ReadOnly Property List() As Collection
        Get
            Return oLista
        End Get
    End Property
End Class
