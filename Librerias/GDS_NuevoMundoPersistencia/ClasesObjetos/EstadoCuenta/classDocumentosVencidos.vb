<Serializable()> _
Public Class classDocumentosVencidos
    Private strTotalEmision As String = Nothing
    Private strTotalVencidos As String = Nothing
    Private listDocumentosEmitidos As List(Of classDocumentosEmitidos) = Nothing
    Private objErrores As classErroresAlertas
    Public Property TotalEmision() As String
        Get
            Return strTotalEmision
        End Get
        Set(ByVal value As String)
            strTotalEmision = value
        End Set
    End Property
    Public Property TotalVencidos() As String
        Get
            Return strTotalVencidos
        End Get
        Set(ByVal value As String)
            strTotalVencidos = value
        End Set
    End Property
    Public Property DocumentosEmitidos() As List(Of classDocumentosEmitidos)
        Get
            Return listDocumentosEmitidos
        End Get
        Set(ByVal value As List(Of classDocumentosEmitidos))
            listDocumentosEmitidos = value
        End Set
    End Property
    Public Property Errores() As classErroresAlertas
        Get
            Return objErrores
        End Get
        Set(ByVal value As classErroresAlertas)
            objErrores = value
        End Set
    End Property
    <Serializable()> _
    Public Class classDocumentosEmitidos
        Private strOficina As String = Nothing
        Private strNumDocumento As String = Nothing
        Private strCondicion As String = Nothing
        Private strFecEmision As String = Nothing
        Private strMoneda As String = Nothing
        Private strTotal As String = Nothing
        Private strFecVence As String = Nothing
        Private strPendiente As String = Nothing
        Public Property Condicion() As String
            Get
                Return strCondicion
            End Get
            Set(ByVal value As String)
                strCondicion = value
            End Set
        End Property
        Public Property FecEmision() As String
            Get
                Return strFecEmision
            End Get
            Set(ByVal value As String)
                strFecEmision = value
            End Set
        End Property
        Public Property Moneda() As String
            Get
                Return strMoneda
            End Get
            Set(ByVal value As String)
                strMoneda = value
            End Set
        End Property
        Public Property NumDocumento() As String
            Get
                Return strNumDocumento
            End Get
            Set(ByVal value As String)
                strNumDocumento = value
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
        Public Property Pendiente() As String
            Get
                Return strPendiente
            End Get
            Set(ByVal value As String)
                strPendiente = value
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
        Public Property FecVence() As String
            Get
                Return strFecVence
            End Get
            Set(ByVal value As String)
                strFecVence = value
            End Set
        End Property
    End Class
End Class
