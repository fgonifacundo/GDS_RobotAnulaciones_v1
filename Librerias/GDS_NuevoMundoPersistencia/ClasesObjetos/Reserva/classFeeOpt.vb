Namespace classFeeOpt
    <Serializable()> _
    Public Class FeeOpt
        Private lstFeeCallCenter As List(Of classFeeOpt.classCallCenter)
        Private strTotalFeeOpt As String = "0.00"
        Public Property FeeCallCenter() As List(Of classCallCenter)
            Get
                Return lstFeeCallCenter
            End Get
            Set(ByVal value As List(Of classCallCenter))
                lstFeeCallCenter = value
            End Set
        End Property
        Public Property TotalFeeOpt() As String
            Get
                Return strTotalFeeOpt
            End Get
            Set(ByVal value As String)
                strTotalFeeOpt = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class classCallCenter
        Private intIdTipoWaiver As Integer = -1
        Private strDescripcion As String = Nothing
        Private objMonto As classMonto
        Private intMarca As Integer = -1
        Property IdTipoWaiver() As Integer
            Get
                Return intIdTipoWaiver
            End Get
            Set(ByVal value As Integer)
                intIdTipoWaiver = value
            End Set
        End Property
        Property Descripcion() As String
            Get
                Return strDescripcion
            End Get
            Set(ByVal value As String)
                strDescripcion = value
            End Set
        End Property
        Property Monto() As classMonto
            Get
                Return objMonto
            End Get
            Set(ByVal value As classMonto)
                objMonto = value
            End Set
        End Property
        Property Marca() As Integer
            Get
                Return intMarca
            End Get
            Set(ByVal value As Integer)
                intMarca = value
            End Set
        End Property
    End Class
End Namespace