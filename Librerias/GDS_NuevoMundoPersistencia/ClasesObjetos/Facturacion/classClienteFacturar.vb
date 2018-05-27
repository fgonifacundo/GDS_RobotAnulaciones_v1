<Serializable()> _
Public Class classClienteFacturar
    Private objCliente As classPasajeros = Nothing
    Public Property Cliente() As classPasajeros
        Get
            Return objCliente
        End Get
        Set(ByVal value As classPasajeros)
            objCliente = value
        End Set
    End Property
End Class
