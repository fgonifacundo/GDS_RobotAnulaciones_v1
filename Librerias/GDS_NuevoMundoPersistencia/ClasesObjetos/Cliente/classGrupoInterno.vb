<Serializable()> _
Public Class classGrupoInterno
    Private strIdGrupoInterno As String = Nothing
    Private srtNombreGrupo As String = Nothing
    Public Property IdGrupoInterno() As String
        Get
            Return strIdGrupoInterno
        End Get
        Set(ByVal value As String)
            strIdGrupoInterno = value
        End Set
    End Property
    Public Property NombreGrupo() As String
        Get
            Return srtNombreGrupo
        End Get
        Set(ByVal value As String)
            srtNombreGrupo = value
        End Set
    End Property
End Class
