<Serializable()> _
Public Class classFacturacion
    Private objFacturaComision As classFacturaComision
    Public Property FacturaComision() As classFacturaComision
        Get
            Return objFacturaComision
        End Get
        Set(ByVal value As classFacturaComision)
            objFacturaComision = value
        End Set
    End Property

End Class
