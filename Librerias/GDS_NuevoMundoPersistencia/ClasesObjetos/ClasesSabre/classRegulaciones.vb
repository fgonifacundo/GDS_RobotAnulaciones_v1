Namespace classRegulaciones
    <Serializable()> _
    Public Class classRegulacionTarifa
        Private objReglas As List(Of classReglas)
        Private strHostCommand As String = Nothing
        Private strDuplicateFareInfo As String = Nothing
        Private strIATA_Number As String = Nothing
        Private strTimeStamp As String = Nothing
        Private objErroresAlertas As classErroresAlertas
        Public Property Reglas() As List(Of classReglas)
            Get
                Return objReglas
            End Get
            Set(ByVal value As List(Of classReglas))
                objReglas = value
            End Set
        End Property
        Public Property HostCommand() As String
            Get
                Return strHostCommand
            End Get
            Set(ByVal value As String)
                strHostCommand = value
            End Set
        End Property
        Public Property DuplicateFareInfo() As String
            Get
                Return strDuplicateFareInfo
            End Get
            Set(ByVal value As String)
                strDuplicateFareInfo = value
            End Set
        End Property
        Public Property IATA_Number() As String
            Get
                Return strIATA_Number
            End Get
            Set(ByVal value As String)
                strIATA_Number = value
            End Set
        End Property
        Public Property TimeStamp() As String
            Get
                Return strTimeStamp
            End Get
            Set(ByVal value As String)
                strTimeStamp = value
            End Set
        End Property
        Public Property ErroresAlertas() As classErroresAlertas
            Get
                Return objErroresAlertas
            End Get
            Set(ByVal value As classErroresAlertas)
                objErroresAlertas = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class classReglas
        Private strNumRegulacion As String = Nothing
        Private strTitulo As String = Nothing
        Private strText() As String = Nothing
        Public Property NumRegulacion() As String
            Get
                Return strNumRegulacion
            End Get
            Set(ByVal value As String)
                strNumRegulacion = value
            End Set
        End Property
        Public Property Titulo() As String
            Get
                Return strTitulo
            End Get
            Set(ByVal value As String)
                strTitulo = value
            End Set
        End Property
        Public Property Text() As String()
            Get
                Return strText
            End Get
            Set(ByVal value As String())
                strText = value
            End Set
        End Property
    End Class
End Namespace