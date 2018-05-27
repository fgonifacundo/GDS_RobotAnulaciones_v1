Public Class classReporteConfirmaciones

    Private strFechaConf As String = Nothing
    Private strCodigoPnr As String = Nothing
    Private strPcc As String = Nothing
    Private strGds As String = Nothing
    Private strNumeroBoleto As String = Nothing
    Private strNombrePasajero As String = Nothing
    Private strAgenteCrea As String = Nothing
    Private strAgenteSolicita As String = Nothing
    Private strAgenteConfirma As String = Nothing
    Private strSucursal As String = Nothing
    Private strImporteOPT As String = Nothing
    Public Property FechaConf() As String
        Get
            Return strFechaConf
        End Get
        Set(ByVal value As String)
            strFechaConf = value
        End Set
    End Property
    Public Property CodigoPnr() As String
        Get
            Return strCodigoPnr
        End Get
        Set(ByVal value As String)
            strCodigoPnr = value
        End Set
    End Property
    Public Property Pcc() As String
        Get
            Return strPcc
        End Get
        Set(ByVal value As String)
            strPcc = value
        End Set
    End Property
    Public Property Gds() As String
        Get
            Return strGds
        End Get
        Set(ByVal value As String)
            strGds = value
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
    Public Property NombrePasajero() As String
        Get
            Return strNombrePasajero
        End Get
        Set(ByVal value As String)
            strNombrePasajero = value
        End Set
    End Property
    Public Property AgenteCrea() As String
        Get
            Return strAgenteCrea
        End Get
        Set(ByVal value As String)
            strAgenteCrea = value
        End Set
    End Property
    Public Property AgenteConfirma() As String
        Get
            Return strAgenteConfirma
        End Get
        Set(ByVal value As String)
            strAgenteConfirma = value
        End Set
    End Property
    Public Property AgenteSolicita() As String
        Get
            Return strAgenteSolicita
        End Get
        Set(ByVal value As String)
            strAgenteSolicita = value
        End Set
    End Property
    Public Property Sucursal() As String
        Get
            Return strSucursal
        End Get
        Set(ByVal value As String)
            strSucursal = value
        End Set
    End Property

    Public Property ImporteOPT() As String
        Get
            Return strImporteOPT
        End Get
        Set(ByVal value As String)
            strImporteOPT = value
        End Set
    End Property

End Class