<Serializable()> _
Public Class classPermisos
    Private intMuestraCiudad As Integer = -1
    Private intTarjeta As Integer = -1
    Private intBoletoPaxIt As Integer = -1
    Private intProhOnLine As Integer = -1
    Private intEnRiesgo As Integer = -1
    Private strMultiplePCC As String = Nothing
    Private strPseudoAgy As String = Nothing
    Private intAutMotosidad As Integer = -1
    Private intFacturaCliente As Integer = -1
    Private intEntregaFacturaCom As Integer = -1
    Private intEAConRestriccion As Integer = -1
    Private intEASinRestriccion As Integer = -1
    Public Property AutMorosidad() As Integer
        Get
            Return intAutMotosidad
        End Get
        Set(ByVal value As Integer)
            intAutMotosidad = value
        End Set
    End Property
    Public Property ProhOnLine() As Integer
        Get
            Return intProhOnLine
        End Get
        Set(ByVal value As Integer)
            intProhOnLine = value
        End Set
    End Property
    Public Property EnRiesgo() As Integer
        Get
            Return intEnRiesgo
        End Get
        Set(ByVal value As Integer)
            intEnRiesgo = value
        End Set
    End Property
    Public Property EntregaFacturaCOM() As Integer
        Get
            Return intEntregaFacturaCom
        End Get
        Set(ByVal value As Integer)
            intEntregaFacturaCom = value
        End Set
    End Property
    Public Property BoletoPaxIt() As Integer
        Get
            Return intBoletoPaxIt
        End Get
        Set(ByVal value As Integer)
            intBoletoPaxIt = value
        End Set
    End Property
    Public Property MuestraCiudad() As Integer
        Get
            Return intMuestraCiudad
        End Get
        Set(ByVal value As Integer)
            intMuestraCiudad = value
        End Set
    End Property
    Public Property Tarjeta() As Integer
        Get
            Return intTarjeta
        End Get
        Set(ByVal value As Integer)
            intTarjeta = value
        End Set
    End Property
    Public Property MultiplePCC() As String
        Get
            Return strMultiplePCC
        End Get
        Set(ByVal value As String)
            strMultiplePCC = value
        End Set
    End Property
    Public Property PseudoAgy() As String
        Get
            Return strPseudoAgy
        End Get
        Set(ByVal value As String)
            strPseudoAgy = value
        End Set
    End Property
    Public Property FacturaCliente() As Integer
        Get
            Return FacturaCliente
        End Get
        Set(ByVal value As Integer)
            intFacturaCliente = value
        End Set
    End Property
    Public Property EAConRestricciones() As Integer
        Get
            Return intEAConRestriccion
        End Get
        Set(ByVal value As Integer)
            intEAConRestriccion = value
        End Set
    End Property
    Public Property EASinRestricciones() As Integer
        Get
            Return intEASinRestriccion
        End Get
        Set(ByVal value As Integer)
            intEASinRestriccion = value
        End Set
    End Property
End Class
