Public Class ClsBoleto_GB
    Public strPseudo As String
    Public strPnr As String
    Public strNroBoleto As String
    Public strNombreArchivo As String
    Public intflagArchivo As Integer
    Public intflag_Email As Integer
    Sub New()
    End Sub
    Public Sub New(strPseudo As String, strPnr As String, strNroBoleto As String, strNombreArchivo As String, intflagArchivo As Integer, intflag_Email As Integer)
        Me.strPseudo = strPseudo
        Me.strPnr = strPnr
        Me.strNroBoleto = strNroBoleto
        Me.strNombreArchivo = strNombreArchivo
        Me.intflagArchivo = intflagArchivo
        Me.intflag_Email = intflag_Email
    End Sub
End Class