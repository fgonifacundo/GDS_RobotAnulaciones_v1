<Serializable()> _
Public Class classTurboPassengerRecipt
    Private strTicket_Number As String = Nothing
    Private strPnr_Code As String = Nothing
    Private strDk_Number As String = Nothing
    Private strRuc_Number As String = Nothing
    Private intPrint_Flag As Integer = 0
    Private strPcc As String = Nothing
    Private strCounter_Ta As String = Nothing
    Private strFecha_Alta As String = Nothing
    Private intPrinted_Flag As Integer = 0
    Private strCuerpo_Documento As String = Nothing
    Private strPasajero_Nombre As String = Nothing
    Private strPasajero_Apellido As String = Nothing
    Private intId_Header As Integer = Nothing
    Private strCounter_Email As String = Nothing
    Private intEmail_Flag As Integer = 0
    Private intMailed_Flag As Integer = 0
    Private strItinerario As String = Nothing
    Private intVenta_Personal As Integer = 0
    Private strFreq_Travel As String = Nothing
    Private strCod_Aerolinea As String = Nothing
    Private strRuta As String = Nothing
    Public Property Ticket_Number() As String
        Get
            Return strTicket_Number
        End Get
        Set(ByVal value As String)
            strTicket_Number = value
        End Set
    End Property
    Public Property Pnr_Code() As String
        Get
            Return strPnr_Code
        End Get
        Set(ByVal value As String)
            strPnr_Code = value
        End Set
    End Property
    Public Property Dk_Number() As String
        Get
            Return strDk_Number
        End Get
        Set(ByVal value As String)
            strDk_Number = value
        End Set
    End Property
    Public Property Ruc_Number() As String
        Get
            Return strRuc_Number
        End Get
        Set(ByVal value As String)
            strRuc_Number = value
        End Set
    End Property
    Public Property Print_Flag() As Integer
        Get
            Return intPrint_Flag
        End Get
        Set(ByVal value As Integer)
            intPrint_Flag = value
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
    Public Property Counter_Ta() As String
        Get
            Return strCounter_Ta
        End Get
        Set(ByVal value As String)
            strCounter_Ta = value
        End Set
    End Property
    Public Property Fecha_Alta() As String
        Get
            Return strFecha_Alta
        End Get
        Set(ByVal value As String)
            strFecha_Alta = value
        End Set
    End Property
    Public Property Printed_Flag() As Integer
        Get
            Return intPrinted_Flag
        End Get
        Set(ByVal value As Integer)
            intPrinted_Flag = value
        End Set
    End Property
    Public Property Cuerpo_Documento() As String
        Get
            Return strCuerpo_Documento
        End Get
        Set(ByVal value As String)
            strCuerpo_Documento = value
        End Set
    End Property
    Public Property Pasajero_Nombre() As String
        Get
            Return strPasajero_Nombre
        End Get
        Set(ByVal value As String)
            strPasajero_Nombre = value
        End Set
    End Property
    Public Property Pasajero_Apellido() As String
        Get
            Return strPasajero_Apellido
        End Get
        Set(ByVal value As String)
            strPasajero_Apellido = value
        End Set
    End Property
    Public Property Id_Header() As Integer
        Get
            Return intId_Header
        End Get
        Set(ByVal value As Integer)
            intId_Header = value
        End Set
    End Property
    Public Property Counter_Email() As String
        Get
            Return strCounter_Email
        End Get
        Set(ByVal value As String)
            strCounter_Email = value
        End Set
    End Property
    Public Property Email_Flag() As Integer
        Get
            Return intEmail_Flag
        End Get
        Set(ByVal value As Integer)
            intEmail_Flag = value
        End Set
    End Property
    Public Property Mailed_Flag() As Integer
        Get
            Return intMailed_Flag
        End Get
        Set(ByVal value As Integer)
            intMailed_Flag = value
        End Set
    End Property
    Public Property Itinerario() As String
        Get
            Return strItinerario
        End Get
        Set(ByVal value As String)
            strItinerario = value
        End Set
    End Property
    Public Property Venta_Personal() As Integer
        Get
            Return intVenta_Personal
        End Get
        Set(ByVal value As Integer)
            intVenta_Personal = value
        End Set
    End Property
    Public Property Freq_Travel() As String
        Get
            Return strFreq_Travel
        End Get
        Set(ByVal value As String)
            strFreq_Travel = value
        End Set
    End Property
    Public Property Cod_Aerolinea() As String
        Get
            Return strCod_Aerolinea
        End Get
        Set(ByVal value As String)
            strCod_Aerolinea = value
        End Set
    End Property
    Public Property Ruta() As String
        Get
            Return strRuta
        End Get
        Set(ByVal value As String)
            strRuta = value
        End Set
    End Property
End Class
