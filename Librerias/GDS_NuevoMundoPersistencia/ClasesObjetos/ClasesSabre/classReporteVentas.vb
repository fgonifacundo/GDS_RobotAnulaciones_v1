<Serializable()> _
Public Class classReporteVentas
    Private strPseudo As String = Nothing
    Private strNombrePseudo As String = Nothing
    Private strFecha As String = Nothing
    Private strMSGError As String = Nothing
    Private objBoletos As List(Of classBoleto) = Nothing
    Public Property Pseudo() As String
        Get
            Return strPseudo
        End Get
        Set(ByVal value As String)
            strPseudo = value
        End Set
    End Property
    Public Property Fecha() As String
        Get
            Return strFecha
        End Get
        Set(ByVal value As String)
            strFecha = value
        End Set
    End Property
    Public Property NombrePseudo() As String
        Get
            Return strNombrePseudo
        End Get
        Set(ByVal value As String)
            strNombrePseudo = value
        End Set
    End Property
    Public Property MSGError() As String
        Get
            Return strMSGError
        End Get
        Set(ByVal value As String)
            strMSGError = value
        End Set
    End Property
    Public Property Boletos() As List(Of classBoleto)
        Get
            Return objBoletos
        End Get
        Set(ByVal value As List(Of classBoleto))
            objBoletos = value
        End Set
    End Property
    Public Class classBoleto
        Private strPNR As String = Nothing
        Private strNumBoleto As String = Nothing
        Private strNombrePasajero As String = Nothing
        Private strAgente As String = Nothing
        Private strEstado As String = Nothing
        Private intID As Integer = 0
        Private intConjuncion As Integer = 0
        Private strHora As String = Nothing
        Private strDomestico As String = Nothing
        Private strComision As String = Nothing
        Private strFormaPago As String = Nothing
        Private bolFacturado As Boolean = False
        Private bolEnviaAvisoAgencia As Boolean = False
        Public Property PNR() As String
            Get
                Return strPNR
            End Get
            Set(ByVal value As String)
                strPNR = value
            End Set
        End Property
        Public Property Agente() As String
            Get
                Return strAgente
            End Get
            Set(ByVal value As String)
                strAgente = value
            End Set
        End Property
        Public Property Estado() As String
            Get
                Return strEstado
            End Get
            Set(ByVal value As String)
                strEstado = value
            End Set
        End Property
        Public Property NombrePasajero() As String
            Get
                Return strNombrePasajero
            End Get
            Set(ByVal value As String)
                strNombrePasajero = value
            End Set
        End Property
        Public Property NumBoleto() As String
            Get
                Return strNumBoleto
            End Get
            Set(ByVal value As String)
                strNumBoleto = value
            End Set
        End Property
        Public Property ID() As Integer
            Get
                Return intID
            End Get
            Set(ByVal value As Integer)
                intID = value
            End Set
        End Property
        Public Property Conjuncion() As Integer
            Get
                Return intConjuncion
            End Get
            Set(ByVal value As Integer)
                intConjuncion = value
            End Set
        End Property
        Public Property Hora() As String
            Get
                Return strHora
            End Get
            Set(ByVal value As String)
                strHora = value
            End Set
        End Property
        Public Property Domestico() As String
            Get
                Return strDomestico
            End Get
            Set(ByVal value As String)
                strDomestico = value
            End Set
        End Property
        Public Property Comision() As String
            Get
                Return strComision
            End Get
            Set(ByVal value As String)
                strComision = value
            End Set
        End Property
        Public Property FormaPago() As String
            Get
                Return strFormaPago
            End Get
            Set(ByVal value As String)
                strFormaPago = value
            End Set
        End Property

        Public Property Facturado() As Boolean
            Get
                Return bolFacturado
            End Get
            Set(ByVal value As Boolean)
                bolFacturado = value
            End Set
        End Property

        Public Property EnviaAvisoAgencia() As Boolean
            Get
                Return bolEnviaAvisoAgencia
            End Get
            Set(ByVal value As Boolean)
                bolEnviaAvisoAgencia = value
            End Set
        End Property
    End Class
End Class
