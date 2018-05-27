<Serializable()> _
Public Class classSegmentos
    Private intSegmento As Integer = -1
    Private intIdLinea As Integer = -1
    Private intRPH As Integer = -1
    Private strAerolinea As String = Nothing
    Private objSalida As classCiudad = Nothing
    Private objLlegada As classCiudad = Nothing
    Private strNumVuelo As String = Nothing
    Private strClaseServicio As String = Nothing
    Private strStatus As String = Nothing
    Private strFechaHoraSalida As String = Nothing
    Private strUpDateFechaHoraSalida As String = Nothing
    Private strFechaHoraLlegada As String = Nothing
    Private strUpDateFechaHoraLlegada As String = Nothing
    Private bolMarca As Boolean = False
    Private strFareBasis As String = Nothing
    Private strMarcaSalidaRetorno As String = Nothing
    Private strCodigoAerolinea As String = Nothing
    Private strOperadoPor As String = Nothing
    Private strEquipo As String = Nothing
    Private strNombreEquipo As String = Nothing
    Private intCasado As Integer = 0
    Public Property Marca() As Boolean
        Get
            Return bolMarca
        End Get
        Set(ByVal value As Boolean)
            bolMarca = value
        End Set
    End Property
    Public Property MarcaSalidaRetorno() As String
        Get
            Return strMarcaSalidaRetorno
        End Get
        Set(ByVal value As String)
            strMarcaSalidaRetorno = value
        End Set
    End Property
    Public Property Segmento() As Integer
        Get
            Return intSegmento
        End Get
        Set(ByVal value As Integer)
            intSegmento = value
        End Set
    End Property
    Public Property RPH() As Integer
        Get
            Return intRPH
        End Get
        Set(ByVal value As Integer)
            intRPH = value
        End Set
    End Property
    Public Property Aerolinea() As String
        Get
            Return strAerolinea
        End Get
        Set(ByVal value As String)
            strAerolinea = value
        End Set
    End Property
    Public Property IdLinea() As Integer
        Get
            Return intIdLinea
        End Get
        Set(ByVal value As Integer)
            intIdLinea = value
        End Set
    End Property
    Public Property Salida() As classCiudad
        Get
            Return objSalida
        End Get
        Set(ByVal value As classCiudad)
            objSalida = value
        End Set
    End Property
    Public Property Llegada() As classCiudad
        Get
            Return objLlegada
        End Get
        Set(ByVal value As classCiudad)
            objLlegada = value
        End Set
    End Property
    Public Property ClaseServicio() As String
        Get
            Return strClaseServicio
        End Get
        Set(ByVal value As String)
            strClaseServicio = value
        End Set
    End Property
    Public Property FareBasis() As String
        Get
            Return strFareBasis
        End Get
        Set(ByVal value As String)
            strFareBasis = value
        End Set
    End Property
    Public Property FechaHoraLlegada() As String
        Get
            Return strFechaHoraLlegada
        End Get
        Set(ByVal value As String)
            strFechaHoraLlegada = value
        End Set
    End Property
    Public Property UpDateFechaHoraLlegada() As String
        Get
            Return strUpDateFechaHoraLlegada
        End Get
        Set(ByVal value As String)
            strUpDateFechaHoraLlegada = value
        End Set
    End Property
    Public Property FechaHoraSalida() As String
        Get
            Return strFechaHoraSalida
        End Get
        Set(ByVal value As String)
            strFechaHoraSalida = value
        End Set
    End Property
    Public Property UpDateFechaHoraSalida() As String
        Get
            Return strUpDateFechaHoraSalida
        End Get
        Set(ByVal value As String)
            strUpDateFechaHoraSalida = value
        End Set
    End Property
    Public Property NumVuelo() As String
        Get
            Return strNumVuelo
        End Get
        Set(ByVal value As String)
            strNumVuelo = value
        End Set
    End Property
    Public Property Status() As String
        Get
            Return strStatus
        End Get
        Set(ByVal value As String)
            strStatus = value
        End Set
    End Property
    Public Property CodigoAerolinea() As String
        Get
            Return strCodigoAerolinea
        End Get
        Set(ByVal value As String)
            strCodigoAerolinea = value
        End Set
    End Property
    Public Property OperadoPor() As String
        Get
            Return strOperadoPor
        End Get
        Set(ByVal value As String)
            strOperadoPor = value
        End Set
    End Property
    Public Property Equipo() As String
        Get
            Return strEquipo
        End Get
        Set(ByVal value As String)
            strEquipo = value
        End Set
    End Property
    Public Property NombreEquipo() As String
        Get
            Return strNombreEquipo
        End Get
        Set(ByVal value As String)
            strNombreEquipo = value
        End Set
    End Property
    Public Property Casado() As Integer
        Get
            Return intCasado
        End Get
        Set(ByVal value As Integer)
            intCasado = value
        End Set
    End Property
End Class
