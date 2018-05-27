<Serializable()> _
Public Class classFacturaComision
    Private bolEntregaFactura As Boolean
    Private strTotal As String
    Private strAfectoOtroDK As String
    Private strAfectoDK As String


    Public Property EntregaFactura() As Boolean
        Get
            Return bolEntregaFactura
        End Get
        Set(ByVal value As Boolean)
            bolEntregaFactura = value
        End Set
    End Property

    Public Property Total() As String
        Get
            Return strTotal
        End Get
        Set(ByVal value As String)
            strTotal = value
        End Set
    End Property

    Public Property AfectoOtroDK() As String
        Get
            Return strAfectoOtroDK
        End Get
        Set(ByVal value As String)
            strAfectoOtroDK = value
        End Set
    End Property

    Public Property AfectoDK() As String
        Get
            Return strAfectoDK
        End Get
        Set(ByVal value As String)
            strAfectoDK = value
        End Set
    End Property

End Class
