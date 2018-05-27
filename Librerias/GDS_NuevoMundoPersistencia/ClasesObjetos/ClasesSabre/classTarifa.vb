<Serializable()> _
Public Class classTarifa
    Private strLNIATA As String = Nothing
    Private strHostCommand As String = Nothing
    Private strTimeStamp As String = Nothing
    Private strLineaValidadora As String = Nothing
    Private strLastTicketing As String = Nothing
    Private objTotalReserva As classMonto = Nothing
    Private objTarifa_x_Pax As List(Of classTarifa_x_Tipo) = Nothing
    Private objFeeNM As classMonto = Nothing
    Private objFeeEmpNM As classMonto = Nothing
    Private strComando As String = Nothing
    Private objErroresAlertas As classErroresAlertas = Nothing
    Public Property LNIATA() As String
        Get
            Return strLNIATA
        End Get
        Set(ByVal value As String)
            strLNIATA = value
        End Set
    End Property
    Public Property HostCommand() As String
        Get
            Return strHostCommand
        End Get
        Set(ByVal value As String)
            strHostCommand = value
        End Set
    End Property
    Public Property TimeStamp() As String
        Get
            Return strTimeStamp
        End Get
        Set(ByVal value As String)
            strTimeStamp = value
        End Set
    End Property
    Public Property LineaValidadora() As String
        Get
            Return strLineaValidadora
        End Get
        Set(ByVal value As String)
            strLineaValidadora = value
        End Set
    End Property
    Public Property LastTicketing() As String
        Get
            Return strLastTicketing
        End Get
        Set(ByVal value As String)
            strLastTicketing = value
        End Set
    End Property
    Public Property FeeEmpNM() As classMonto
        Get
            Return objFeeEmpNM
        End Get
        Set(ByVal value As classMonto)
            objFeeEmpNM = value
        End Set
    End Property
    Public Property FeeNM() As classMonto
        Get
            Return objFeeNM
        End Get
        Set(ByVal value As classMonto)
            objFeeNM = value
        End Set
    End Property
    Public Property Tarifa_x_Pax() As List(Of GDS_NuevoMundoPersistencia.classTarifa_x_Tipo)
        Get
            Return objTarifa_x_Pax
        End Get
        Set(ByVal value As List(Of GDS_NuevoMundoPersistencia.classTarifa_x_Tipo))
            objTarifa_x_Pax = value
        End Set
    End Property
    Public Property TotalReserva() As classMonto
        Get
            Return objTotalReserva
        End Get
        Set(ByVal value As classMonto)
            objTotalReserva = value
        End Set
    End Property
    Public Property Comando() As String
        Get
            Return strComando
        End Get
        Set(ByVal value As String)
            strComando = value
        End Set
    End Property
    Public Property ErroresAlertas() As classErroresAlertas
        Get
            Return objErroresAlertas
        End Get
        Set(ByVal value As classErroresAlertas)
            objErroresAlertas = value
        End Set
    End Property
End Class
