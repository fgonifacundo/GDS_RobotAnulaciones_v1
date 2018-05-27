Imports GDS_NuevoMundoPersistencia
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Imports System.Text
Namespace ObjetoAccesoDatos
    Partial Public Class classDAO
        Public Function InsertaXMLaTabla(ByVal strNombreTabla As String, _
                                            ByVal strXML As String, _
                                              ByVal strCodigoSeguimiento As String, _
                                              ByVal intFirmaDB As Integer, _
                                              ByVal intEsquema As Integer, _
                                              ByVal delete_table As Boolean, _
                                              ByVal strNameSP_delete As String, _
                                              Optional ByVal EsTablaPTADestinos As Boolean = False) As Boolean
            Dim ConnNM As New MyConnectionOracle
            Dim bolResultado As Boolean
            Dim Procedure As String
            Try
                ConnNM.Connect(intFirmaDB)
                If EsTablaPTADestinos Then
                    Procedure = Constantes.GDS_INSERT_XMLtoTABLE_PTADESTINOS
                Else
                    Procedure = Constantes.GDS_INSERT_XMLtoTABLE
                End If
                If delete_table Then 'ELIMINA DATOS DE LA TABLA SI EL VALOR ES TRUE
                    ConnNM.SP_Command(strNameSP_delete, Constantes.StoredProcedure)
                    bolResultado = ConnNM._DeleteExecuteNonQuery()
                End If
                strXML = strXML.Replace("&", "Y")
                ConnNM.SP_Command(Procedure, Constantes.StoredProcedure) 'CARGA LA TABLA MEDIANTE XML
                ConnNM.AgregarParametro("@p_Tabla", strNombreTabla, OracleDbType.Varchar2, strNombreTabla.Length, ParameterDirection.Input)
                ConnNM.AgregarParametro("@p_Xml", strXML, OracleDbType.Clob, 0, ParameterDirection.Input)
                bolResultado = ConnNM._InsertExecuteNonQuery()
            Catch ex As Exception
                bolResultado = False
                strLog = "Stored Procedure : " & Procedure & vbCrLf
                strLog &= Constantes.TabEspacios & "Código de Seguimiento: " & strCodigoSeguimiento.ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Conexion: " & CStr(intFirmaDB).ToString & vbCrLf
                strLog &= Constantes.TabEspacios & "Source : " & "InsertaXMLaTabla" & vbCrLf
                strLog &= Constantes.TabEspacios & "Message : " & ex.ToString & vbCrLf
                objEscribeLog.WriteLog(strLog, "InsertaXMLaTabla", strCodigoSeguimiento)
            Finally
                ConnNM.Disconnect()
                strCodigoSeguimiento = Nothing
                intFirmaDB = Nothing
                intEsquema = Nothing
                ConnNM = Nothing
            End Try
            Return bolResultado
        End Function
    End Class
End Namespace
