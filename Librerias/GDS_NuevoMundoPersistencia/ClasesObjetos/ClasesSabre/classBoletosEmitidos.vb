Namespace classBoletosEmitidos
    <Serializable()> _
    Public Class BoletosEmitidos
        Private objErrores As classErroresAlertas
        Private objBoletos As List(Of Boletos)
        Public Property Errores() As classErroresAlertas
            Get
                Return objErrores
            End Get
            Set(ByVal value As classErroresAlertas)
                objErrores = value
            End Set
        End Property
        Public Property Boletos() As List(Of Boletos)
            Get
                Return objBoletos
            End Get
            Set(ByVal value As List(Of Boletos))
                objBoletos = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class Boletos
        Private strNumeroPasajero As String = Nothing
        Private strCodAerolinea As String = Nothing
        Private strPrefijo As String = Nothing
        Private strNumeroBoleto As String = Nothing
        Private strEsConexion As Boolean = False
        Public Property NumeroPasajero() As String
            Get
                Return strNumeroPasajero
            End Get
            Set(ByVal value As String)
                strNumeroPasajero = value
            End Set
        End Property
        Public Property CodAerolinea() As String
            Get
                Return strCodAerolinea
            End Get
            Set(ByVal value As String)
                strCodAerolinea = value
            End Set
        End Property
        Public Property Prefijo() As String
            Get
                Return strPrefijo
            End Get
            Set(ByVal value As String)
                strPrefijo = value
            End Set
        End Property
        Public Property NumeroBoleto() As String
            Get
                Return strNumeroBoleto
            End Get
            Set(ByVal value As String)
                strNumeroBoleto = value
            End Set
        End Property
        Public Property EsConexion() As Boolean
            Get
                Return strEsConexion
            End Get
            Set(ByVal value As Boolean)
                strEsConexion = value
            End Set
        End Property
    End Class
End Namespace
