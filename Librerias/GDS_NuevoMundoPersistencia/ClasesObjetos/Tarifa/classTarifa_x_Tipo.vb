<Serializable()> _
Public Class classTarifa_x_Tipo
    Private strTipoPax As String = Nothing
    Private strCantidadPax As Integer = 0
    Private objTotalPax As classMonto = Nothing
    Private strIGV As classMonto = Nothing
    Private strTotalImpuestos As String = Nothing
    Private lstDetalleImpuestos As List(Of classMonto) = Nothing
    Private objTarifaNeta As classMonto = Nothing
    Private intComision As Integer = Nothing
    Private strFareCalculation As String = Nothing
    Private objQueue As List(Of classMonto) = Nothing
    Private objFare As List(Of classMonto) = Nothing
    Private objCorteTarifario As classCorteTarifario = Nothing
    Public Property Queue() As List(Of classMonto)
        Get
            Return objQueue
        End Get
        Set(ByVal value As List(Of classMonto))
            objQueue = value
        End Set
    End Property
    Public Property Fare() As List(Of classMonto)
        Get
            Return objFare
        End Get
        Set(ByVal value As List(Of classMonto))
            objFare = value
        End Set
    End Property
    Public Property DetalleImpuestos() As List(Of classMonto)
        Get
            Return lstDetalleImpuestos
        End Get
        Set(ByVal value As List(Of classMonto))
            lstDetalleImpuestos = value
        End Set
    End Property
    Public Property IGV() As classMonto
        Get
            Return strIGV
        End Get
        Set(ByVal value As classMonto)
            strIGV = value
        End Set
    End Property
    Public Property TotalImpuestos() As String
        Get
            Return strTotalImpuestos
        End Get
        Set(ByVal value As String)
            strTotalImpuestos = value
        End Set
    End Property
    Public Property TarifaNeta() As classMonto
        Get
            Return objTarifaNeta
        End Get
        Set(ByVal value As classMonto)
            objTarifaNeta = value
        End Set
    End Property
    Public Property Comision() As Integer
        Get
            Return intComision
        End Get
        Set(ByVal value As Integer)
            intComision = value
        End Set
    End Property
    Public Property TotalPax() As classMonto
        Get
            Return objTotalPax
        End Get
        Set(ByVal value As classMonto)
            objTotalPax = value
        End Set
    End Property
    Public Property CantidadPax() As Integer
        Get
            Return strCantidadPax
        End Get
        Set(ByVal value As Integer)
            strCantidadPax = value
        End Set
    End Property
    Public Property FareCalculation() As String
        Get
            Return strFareCalculation
        End Get
        Set(ByVal value As String)
            strFareCalculation = value
        End Set
    End Property
    Public Property TipoPax() As String
        Get
            Return strTipoPax
        End Get
        Set(ByVal value As String)
            strTipoPax = value
        End Set
    End Property
    Public Property CorteTarifario() As classCorteTarifario
        Get
            Return objCorteTarifario
        End Get
        Set(ByVal value As classCorteTarifario)
            objCorteTarifario = value
        End Set
    End Property
End Class
