Public Class ClsTarifasMain_ODD
    Public intCorrelativo As String
    Public strPseudo As String
    Public strAirlines As String
    Public id_ciudad_or As String
    Public id_ciudad_des As String
    Public strTipoTarifa As String
    Public strFechaSalida As String
    Public strFechaRetorno As String
    Public strClase As String
    Public FareBasis As String
    Public BaseFare_Amount As String
    Public TotalFare_Amount As String
    Public strFechaVigencia As String
    Public strFechaEmision As String
    Public fecha_registro As String
    Public strHostCommand As String
    Public strTipoPasajero As String
    Public Sub New(intCorrelativo As String, strPseudo As String, strAirlines As String, id_ciudad_or As String, id_ciudad_des As String, strTipoTarifa As String, _
        FareBasis As String, BaseFare_Amount As String, TotalFare_Amount As String, strFechaSalida As String, strFechaRetorno As String, strClase As String, _
        strFechaVigencia As String, strFechaEmision As String, fecha_registro As String, strHostCommand As String, strTipoPasajero As String)
        Me.intCorrelativo = intCorrelativo
        Me.strPseudo = strPseudo
        Me.strAirlines = strAirlines
        Me.id_ciudad_or = id_ciudad_or
        Me.id_ciudad_des = id_ciudad_des
        Me.strTipoTarifa = strTipoTarifa
        Me.strFechaSalida = strFechaSalida
        Me.strFechaRetorno = strFechaRetorno
        Me.strClase = strClase
        Me.FareBasis = FareBasis
        Me.BaseFare_Amount = BaseFare_Amount
        Me.TotalFare_Amount = TotalFare_Amount
        Me.strFechaVigencia = strFechaVigencia
        Me.strFechaEmision = strFechaEmision
        Me.fecha_registro = fecha_registro
        Me.strHostCommand = strHostCommand
        Me.strTipoPasajero = strTipoPasajero
    End Sub
End Class