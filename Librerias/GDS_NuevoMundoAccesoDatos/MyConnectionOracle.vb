Option Strict Off
Imports Oracle.DataAccess.Client
Imports Constantes = GDS_NuevoMundoPersistencia.Constantes.classConstantes
Public Class MyConnectionOracle
    Private objOracleConnection As New OracleConnection
    Private objOracleCommand As New OracleCommand
    Private objOracleTransaction As OracleTransaction
    Private objAppConfig As New classAppConfig
    Private objOracleDataAdapter As New OracleDataAdapter
    Public Function Connect(ByVal intOpcionCadena As Integer) As Boolean
        Dim bolRespuesta As Boolean = False
        Try

            If objOracleConnection.State = ConnectionState.Closed Then
                objOracleConnection.ConnectionString = Ruta(intOpcionCadena)
                objOracleConnection.Open()
                bolRespuesta = True
            Else
                bolRespuesta = True
            End If

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try

        Return bolRespuesta
    End Function
    Public Function Disconnect() As Boolean
        Dim bolRespuesta As Boolean = False
        Try
            If objOracleConnection.State = ConnectionState.Open Then
                objOracleCommand.Parameters.Clear()
                objOracleConnection.Close()
                objOracleConnection.Dispose()
                bolRespuesta = True
            Else
                bolRespuesta = True
            End If

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
        Return bolRespuesta
    End Function
    Private Function Ruta(ByVal intOpcion As Integer) As String
        Dim strRuta As String = Nothing
        Select Case intOpcion
            Case Constantes.Usr_PTA_Amadeus
                strRuta = objAppConfig.Obtiene_CADENA_CONEXION_PTA_AMADEUS()
            Case Constantes.Usr_PTA_Sabre
                strRuta = objAppConfig.Obtiene_CADENA_CONEXION_PTA_SABRE()
            Case Constantes.Usr_PTA_EasyOnline
                strRuta = objAppConfig.Obtiene_CADENA_CONEXION_PTA_EASYONLINE()
            Case Constantes.Usr_WEB_General
                strRuta = objAppConfig.Obtiene_CADENA_CONEXION_WEB()
            Case Constantes.Usr_WEB_Demo
                strRuta = objAppConfig.Obtiene_CADENA_CONEXION_WEB_DEMO()
            Case Constantes.Usr_PTA_Demonuevomundo
                strRuta = objAppConfig.Obtiene_CADENA_CONEXION_PTA_DEMONUEVOMUNDO()
            Case Constantes.Usr_PTA_Ptadestinos
                strRuta = objAppConfig.Obtiene_CADENA_CONEXION_PTA_DESTINOS()
        End Select
        Return strRuta
    End Function

    Public Sub SP_Command(ByVal strCommandText As String, _
                          ByVal strCommandType As String)
        Try
            _Command(strCommandText, strCommandType, False)
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
    Public Sub SP_Command(ByVal strCommandText As String, _
                          ByVal strCommandType As String, _
                          ByVal bolTransaction As Boolean)
        Try
            _Command(strCommandText, strCommandType, bolTransaction)

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
    Private Sub _Command(ByVal strCommandText As String, _
                         ByVal strCommandType As String, _
                         ByVal bolTransaction As Boolean)
        Try
            objOracleCommand.CommandText = strCommandText
            objOracleCommand.CommandTimeout = Constantes.CommandTimeout
            objOracleCommand.CommandType = CommandType.TableDirect
            objOracleCommand.Parameters.Clear()
            If strCommandType.Equals(Constantes.StoredProcedure) Then objOracleCommand.CommandType = CommandType.StoredProcedure
            If strCommandType.Equals(Constantes.SentenciaText) Then objOracleCommand.CommandType = CommandType.Text

            If Not bolTransaction Then
                objOracleCommand.Connection = objOracleConnection
            Else
                If objOracleTransaction Is Nothing Then
                    objOracleTransaction = objOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                End If
                objOracleCommand.Connection = objOracleTransaction.Connection
            End If

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
    Public Function _Commit() As Boolean
        Dim bolRespuesta As Boolean = False
        Try
            objOracleTransaction.Commit()
            bolRespuesta = True
        Catch ex As Exception
            If objOracleTransaction IsNot Nothing Then
                objOracleTransaction.Rollback()
            End If
            Throw New Exception(ex.ToString)
        End Try

        Return bolRespuesta
    End Function
    Public Sub Rollback()
        Try
            If objOracleTransaction IsNot Nothing Then
                objOracleTransaction.Rollback()
            End If
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
    Public Function _InsertExecuteNonQuery() As Boolean
        Dim bolRespuesta As Boolean = False
        Try
            objOracleTransaction = objOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            bolRespuesta = objOracleCommand.ExecuteNonQuery()
            objOracleTransaction.Commit()
            bolRespuesta = True
        Catch ex As Exception
            If objOracleTransaction IsNot Nothing Then
                objOracleTransaction.Rollback()
            End If
            Throw New Exception(ex.ToString)
        End Try

        Return bolRespuesta
    End Function
    Public Function _InsertExecuteNonQuery(ByVal bolCommit As Boolean, ByVal bolTransaction As Boolean) As Boolean
        Dim bolRespuesta As Boolean = False
        Try
            objOracleCommand.ExecuteNonQuery()

            If bolCommit Then
                objOracleTransaction.Commit()
                bolRespuesta = True
            Else
                If bolTransaction Then
                    objOracleTransaction = objOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted)
                End If
            End If

        Catch ex As Exception
            If objOracleTransaction IsNot Nothing Then
                objOracleTransaction.Rollback()
            End If
            Throw New Exception(ex.ToString)
        End Try

        Return bolRespuesta
    End Function
    Public Function _UpdateExecuteNonQuery() As Boolean
        Dim bolRespuesta As Boolean = False
        Try
            objOracleTransaction = objOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            bolRespuesta = objOracleCommand.ExecuteNonQuery()
            objOracleTransaction.Commit()
            bolRespuesta = True
        Catch ex As Exception
            If objOracleTransaction IsNot Nothing Then
                objOracleTransaction.Rollback()
            End If
            Throw New Exception(ex.ToString)
        End Try

        Return bolRespuesta
    End Function
    Public Function _DeleteExecuteNonQuery() As Boolean
        Dim bolRespuesta As Boolean = False
        Try
            objOracleTransaction = objOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            bolRespuesta = objOracleCommand.ExecuteNonQuery()
            objOracleTransaction.Commit()
        Catch ex As Exception
            If objOracleTransaction IsNot Nothing Then
                objOracleTransaction.Rollback()
            End If
            Throw New Exception(ex.ToString)
        End Try

        Return bolRespuesta
    End Function
    Public Function _ExecuteReader() As OracleDataReader
        Try
            Return objOracleCommand.ExecuteReader()
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function
    Public Sub _ExecuteNonQuery()
        Try
            objOracleCommand.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
    Public Function _ExecuteNonQuery_BeginTransaction() As Boolean
        Dim bolRespuesta As Boolean = False
        Try
            objOracleTransaction = objOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted)
            bolRespuesta = objOracleCommand.ExecuteNonQuery()
            objOracleTransaction.Commit()
            bolRespuesta = True
        Catch ex As Exception
            If objOracleTransaction IsNot Nothing Then
                objOracleTransaction.Rollback()
            End If
            Throw New Exception(ex.ToString)
        End Try

        Return bolRespuesta
    End Function
    Public Function AgregarParametro(ByVal Nombre As String, _
                                     ByVal Valor As Object, _
                                     ByVal Tipo As OracleDbType, _
                                     ByVal Size As Integer, _
                                     ByVal Direccion As ParameterDirection, _
                                     Optional ByVal Retorna As Boolean = False) As OracleParameter

        Try
            Using objParameter As New OracleParameter(Nombre, IIf(Valor Is Nothing, Nothing, Valor))
                With objParameter
                    .OracleDbType = Tipo
                    If Size > 0 Then .Size = Size
                    .Direction = Direccion
                End With

                If Retorna Then
                    Return objOracleCommand.Parameters.Add(objParameter)
                Else
                    objOracleCommand.Parameters.Add(objParameter)
                End If

                objParameter.Dispose()
            End Using
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function
    Public Sub LimpiarParametro()

        Try
            objOracleCommand.Parameters.Clear()
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
    Public Function LeeParametros(ByVal Nombre As String, _
                                  ByVal ValorDefecto As String) As String
        Dim strRespuesta As String = Nothing
        Try
            strRespuesta = IIf(objOracleCommand.Parameters(Nombre).Value Is DBNull.Value, IIf(ValorDefecto Is Nothing, Nothing, ValorDefecto), objOracleCommand.Parameters(Nombre).Value.ToString)
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
        Return strRespuesta
    End Function

    Public Function LeeColumnasDataReader(ByVal objOracleDataReader As OracleDataReader, _
                                          ByVal Nombre As String, _
                                          ByVal ValorDefecto As String) As String

        Dim strRespuesta As String = Nothing
        Try
            strRespuesta = IIf(objOracleDataReader(Nombre) Is DBNull.Value, IIf(ValorDefecto Is Nothing, Nothing, ValorDefecto), objOracleDataReader(Nombre).ToString.Trim)
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
        Return strRespuesta
    End Function

    Public Function _BeginTransactionDataTable(ByVal strNombreTabla As String) As DataTable
        Dim objDataTable As DataTable = Nothing
        Dim objOracleDataAdapter As OracleDataAdapter = Nothing

        Try
            objOracleTransaction = objOracleConnection.BeginTransaction(IsolationLevel.ReadCommitted)

            objDataTable = New DataTable(strNombreTabla)
            objOracleDataAdapter = New OracleDataAdapter(objOracleCommand)
            objOracleDataAdapter.Fill(objDataTable)
            objOracleTransaction.Commit()

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        Finally

        End Try

        Return objDataTable
    End Function

End Class

