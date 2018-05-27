<Serializable()> _
Public Class classDatosAgente
    Private strFirmaAgente As String = Nothing
    Private strIdVendedor As String = Nothing
    Private strNombreAgente As String = Nothing
    Private strCorreoAgente As String = Nothing
    Private strOficina As String = Nothing
    Private strNombreJefe As String = Nothing
    Private strCorreoJefe As String = Nothing
    Private strDepartamento As String = Nothing

    Private strCorreoPromotor As String = Nothing
    Private strCliente As String = Nothing
    Private strCorreoCaja As String = Nothing
    Private strCorreoVendedor As String = Nothing
    Private intIdArea As Integer = -1
    Private strNombreArea As String = Nothing
    Private strNombreVendedor As String = Nothing
    Private strIdFirmaVendedor As String = Nothing
    Private strIdDepartamentoVendedor As String = Nothing

    Public Property FirmaAgente() As String
        Get
            Return strFirmaAgente
        End Get
        Set(ByVal value As String)
            strFirmaAgente = value
        End Set
    End Property
    Public Property IdVendedor() As String
        Get
            Return strIdVendedor
        End Get
        Set(ByVal value As String)
            strIdVendedor = value
        End Set
    End Property
    Public Property NombreAgente() As String
        Get
            Return strNombreAgente
        End Get
        Set(ByVal value As String)
            strNombreAgente = value
        End Set
    End Property
    Public Property CorreoAgente() As String
        Get
            Return strCorreoAgente
        End Get
        Set(ByVal value As String)
            strCorreoAgente = value
        End Set
    End Property
    Public Property Oficina() As String
        Get
            Return strOficina
        End Get
        Set(ByVal value As String)
            strOficina = value
        End Set
    End Property
    Public Property NombreJefe() As String
        Get
            Return strNombreJefe
        End Get
        Set(ByVal value As String)
            strNombreJefe = value
        End Set
    End Property
    Public Property CorreoJefe() As String
        Get
            Return strCorreoJefe
        End Get
        Set(ByVal value As String)
            strCorreoJefe = value
        End Set
    End Property
    Public Property Departamento() As String
        Get
            Return strDepartamento
        End Get
        Set(ByVal value As String)
            strDepartamento = value
        End Set
    End Property

    Public Property CorreoPromotor() As String
        Get
            Return strCorreoPromotor
        End Get
        Set(ByVal value As String)
            strCorreoPromotor = value
        End Set
    End Property

    Public Property Cliente() As String
        Get
            Return strCliente
        End Get
        Set(ByVal value As String)
            strCliente = value
        End Set
    End Property

    Public Property CorreoCaja() As String
        Get
            Return strCorreoCaja
        End Get
        Set(ByVal value As String)
            strCorreoCaja = value
        End Set
    End Property
    Public Property CorreoVendedor() As String
        Get
            Return strCorreoVendedor
        End Get
        Set(ByVal value As String)
            strCorreoVendedor = value
        End Set
    End Property

    Public Property NombreVendedor() As String
        Get
            Return strNombreVendedor
        End Get
        Set(ByVal value As String)
            strNombreVendedor = value
        End Set
    End Property

    Public Property IdFirmaVendedor() As String
        Get
            Return strIdFirmaVendedor
        End Get
        Set(ByVal value As String)
            strIdFirmaVendedor = value
        End Set
    End Property

    Public Property IdDepartamentoVendedor() As String
        Get
            Return strIdDepartamentoVendedor
        End Get
        Set(ByVal value As String)
            strIdDepartamentoVendedor = value
        End Set
    End Property

    Public Property IdArea() As Integer
        Get
            Return intIdArea
        End Get
        Set(value As Integer)
            intIdArea = value
        End Set
    End Property

    Public Property NombreArea() As String
        Get
            Return strNombreArea
        End Get
        Set(value As String)
            strNombreArea = value
        End Set
    End Property

End Class
