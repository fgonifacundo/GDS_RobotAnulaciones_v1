Public Class classPseudosTarifasNegociadas

    Private lstPseudos As List(Of String) = Nothing
    Private lstAerolineas As List(Of String) = Nothing

    Public Property Pseudos() As List(Of String)
        Get
            Return lstPseudos
        End Get
        Set(ByVal value As List(Of String))
            lstPseudos = value
        End Set
    End Property

    Public Property Aerolineas() As List(Of String)
        Get
            Return lstAerolineas
        End Get
        Set(ByVal value As List(Of String))
            lstAerolineas = value
        End Set
    End Property

End Class
