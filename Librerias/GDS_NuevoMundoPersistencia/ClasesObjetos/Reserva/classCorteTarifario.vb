<Serializable()> _
Public Class classCorteTarifario
    Private objCiudadesCorte As List(Of classCorte) = Nothing
    Private objFareBasis As List(Of classFareBasis) = Nothing
    Public Property CiudadesCorte() As List(Of classCorte)
        Get
            Return objCiudadesCorte
        End Get
        Set(ByVal value As List(Of classCorte))
            objCiudadesCorte = value
        End Set
    End Property
    Public Property FareBasis() As List(Of classFareBasis)
        Get
            Return objFareBasis
        End Get
        Set(ByVal value As List(Of classFareBasis))
            objFareBasis = value
        End Set
    End Property
    <Serializable()> _
    Public Class classCorte
        Private strCiudadesCorte As String = Nothing
        Private objclassCiudad As classCiudad = Nothing
        Public Property CiudadesCorte() As String
            Get
                Return strCiudadesCorte
            End Get
            Set(ByVal value As String)
                strCiudadesCorte = value
            End Set
        End Property
        Public Property Ciudad() As classCiudad
            Get
                Return objclassCiudad
            End Get
            Set(ByVal value As classCiudad)
                objclassCiudad = value
            End Set
        End Property
    End Class
    <Serializable()> _
    Public Class classFareBasis
        Private strRPH As Integer = 0
        Private strTipoPax As String = Nothing
        Private strFareBasis As String = Nothing
        Public Property RPH() As Integer
            Get
                Return strRPH
            End Get
            Set(ByVal value As Integer)
                strRPH = value
            End Set
        End Property
        Public Property TipoPax() As String
            Get
                Return strTipoPax
            End Get
            Set(ByVal value As String)
                strTipoPax = value
            End Set
        End Property
        Public Property sFareBasis() As String
            Get
                Return strFareBasis
            End Get
            Set(ByVal value As String)
                strFareBasis = value
            End Set
        End Property
    End Class
End Class
