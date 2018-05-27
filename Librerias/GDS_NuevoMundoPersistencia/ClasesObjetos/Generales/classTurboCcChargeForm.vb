Public Class classTurboCcChargeForm
    Private strPnr_Code As String = Nothing
    Private strCounter_Ta As String = Nothing
    Private strFecha_Alta As String = Nothing
    Private intPrint_Flag As Integer = 0
    Private strCounter_Email As String = Nothing
    Private strTicket_Number As String = Nothing
    Private strCuerpo_Documento As String = Nothing
    Private strCuerpo_Correo As String = Nothing
    
    Public Property Pnr_Code() As String
        Get
            Return strPnr_Code
        End Get
        Set(ByVal value As String)
            strPnr_Code = value
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

    Public Property Print_Flag() As Integer
        Get
            Return intPrint_Flag
        End Get
        Set(ByVal value As Integer)
            intPrint_Flag = value
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

    Public Property Ticket_Number() As String
        Get
            Return strTicket_Number
        End Get
        Set(ByVal value As String)
            strTicket_Number = value
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

    Public Property Cuerpo_Correo() As String
        Get
            Return strCuerpo_Correo
        End Get
        Set(ByVal value As String)
            strCuerpo_Correo = value
        End Set
    End Property

End Class
