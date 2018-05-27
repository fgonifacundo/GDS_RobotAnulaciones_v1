Public Class ClsItinerario_ODD
    Public id_ciudad_or As String
    Public id_ciudad_des As String
    Public strAirlines As String
    Public strpseudo_reg As String
    Public dtfecha_registro As String
    Public Sub New(id_ciudad_or As String, ByVal id_ciudad_des As String, ByVal strAirlines As String, ByVal strpseudo_reg As String, ByVal dtfecha_registro As String)
        Me.id_ciudad_or = id_ciudad_or
        Me.id_ciudad_des = id_ciudad_des
        Me.strAirlines = strAirlines
        Me.strpseudo_reg = strpseudo_reg
        Me.dtfecha_registro = dtfecha_registro
    End Sub
End Class
