Namespace classReporteBoletos
    <Serializable()> _
    Public Class ReporteBoletosDelDias
        Private strFirmasAgentes As String = Nothing
        Private objReporteVentas As List(Of classReporteVentas) = Nothing
        Private objBoletosDuplicados As List(Of classBoletosDuplicados)
        Private objBoletosAgenteGDS As List(Of classBoletosDuplicados)
        Public Property FirmasAgentes() As String
            Get
                Return strFirmasAgentes
            End Get
            Set(ByVal value As String)
                strFirmasAgentes = value
            End Set
        End Property
        Public Property ReporteVentas() As List(Of classReporteVentas)
            Get
                Return objReporteVentas
            End Get
            Set(ByVal value As List(Of classReporteVentas))
                objReporteVentas = value
            End Set
        End Property
        Public Property BoletosDuplicados() As List(Of classBoletosDuplicados)
            Get
                Return objBoletosDuplicados
            End Get
            Set(ByVal value As List(Of classBoletosDuplicados))
                objBoletosDuplicados = value
            End Set
        End Property
        Public Property BoletosAgenteGDS() As List(Of classBoletosDuplicados)
            Get
                Return objBoletosAgenteGDS
            End Get
            Set(ByVal value As List(Of classBoletosDuplicados))
                objBoletosAgenteGDS = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class classBoletosDuplicados
        Private strPNR As String = Nothing
        Private objDuplicadosNombre As List(Of classDuplicadosNombre) = Nothing
        Public Property PNR() As String
            Get
                Return strPNR
            End Get
            Set(ByVal value As String)
                strPNR = value
            End Set
        End Property
        Public Property DuplicadosNombre() As List(Of classDuplicadosNombre)
            Get
                Return objDuplicadosNombre
            End Get
            Set(ByVal value As List(Of classDuplicadosNombre))
                objDuplicadosNombre = value
            End Set
        End Property
        Public Class classDuplicadosNombre
            Private strNombrePax As String = Nothing
            Private objBoletos As List(Of classDuplicadosBoletos) = Nothing
            Public Property NombrePax() As String
                Get
                    Return strNombrePax
                End Get
                Set(ByVal value As String)
                    strNombrePax = value
                End Set
            End Property
            Public Property Boletos() As List(Of classDuplicadosBoletos)
                Get
                    Return objBoletos
                End Get
                Set(ByVal value As List(Of classDuplicadosBoletos))
                    objBoletos = value
                End Set
            End Property
        End Class
        Public Class classDuplicadosBoletos
            Private strPseudo As String = Nothing
            Private strBoleto As String = Nothing
            Private strAgente As String = Nothing
            Private strHora As String = Nothing
            Private strEstadoTkt As String = Nothing
            Private strTodoSegmetos As String = Nothing
            Private bolMarca As Boolean = False
            Public Property Pseudo() As String
                Get
                    Return strPseudo
                End Get
                Set(ByVal value As String)
                    strPseudo = value
                End Set
            End Property
            Public Property Boleto() As String
                Get
                    Return strBoleto
                End Get
                Set(ByVal value As String)
                    strBoleto = value
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
            Public Property Hora() As String
                Get
                    Return strHora
                End Get
                Set(ByVal value As String)
                    strHora = value
                End Set
            End Property
            Public Property EstadoTkt() As String
                Get
                    Return strEstadoTkt
                End Get
                Set(ByVal value As String)
                    strEstadoTkt = value
                End Set
            End Property
            Public Property TodoSegmetos() As String
                Get
                    Return strTodoSegmetos
                End Get
                Set(ByVal value As String)
                    strTodoSegmetos = value
                End Set
            End Property
            Public Property Marca() As Boolean
                Get
                    Return bolMarca
                End Get
                Set(ByVal value As Boolean)
                    bolMarca = value
                End Set
            End Property
        End Class
    End Class
End Namespace
