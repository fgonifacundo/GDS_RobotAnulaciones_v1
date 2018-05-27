Public Class classAerolineaC

    Private strIdTransportador As String = Nothing

    Public Property IdTransportador() As String

        Get

            Return strIdTransportador

        End Get
        ''-----------------------------------
        Set(ByVal value As String)
            strIdTransportador = value
        End Set
        ''--------------------------------

    End Property

End Class
