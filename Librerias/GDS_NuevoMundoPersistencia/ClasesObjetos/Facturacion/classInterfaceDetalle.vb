<Serializable()> _
Public Class classInterfaceDetalle
    Private l_Referencia As String
    Private l_Secuencia As Integer
    Private l_TipoDescuento As String
    Private l_Descuento As String
    Private l_conGasto_Emision As String
    Private l_es_Waiver As String
    Private l_PNR As String
    Private l_en_PNR As String
    Private l_id_proveedor_GDS As String
    Private l_id_SecuenciaInfante As String
    Private l_IdPax As String
    Private l_es_IT As String
    Private l_QuienAutorizaDcto As String
    Private l_es_tourcode_automatico As String
    Private l_Factor_Meta As String
    Private l_Over As String
    Private l_Tarifa_No As String
    Private l_Comision_No As String
    Private l_NaceCancelado As String
    Private l_es_InfanteAdulto As String
    Private l_con_morosidad As String
    Private l_numero_de_boleto As String
    Private l_sin_facturar As String
    Private l_importe_waiver As String
    Private l_es_conexion As String
    Private l_error As String
    Private l_tarifa_auxiliar As String
    Private l_importe_fee As String
    Private l_tipo_de_waiver As String
    Private l_tarifa_adicional As String
    Private l_id_grupo_aereo As String
    Private l_id_cotizacion_pax As String
    Private l_tipo_comision_gds As String
    Private l_comision_gds As String

    Public Property Referencia() As String
        Get
            Return l_Referencia
        End Get
        Set(ByVal value As String)
            l_Referencia = value
        End Set
    End Property

    Public Property Secuencia() As Integer
        Get
            Return l_Secuencia
        End Get
        Set(ByVal value As Integer)
            l_Secuencia = value
        End Set
    End Property

    Public Property TipoDescuento() As String
        Get
            Return l_TipoDescuento
        End Get
        Set(ByVal value As String)
            l_TipoDescuento = value
        End Set
    End Property

    Public Property Descuento() As String
        Get
            Return l_Descuento
        End Get
        Set(ByVal value As String)
            l_Descuento = value
        End Set
    End Property

    Public Property conGasto_Emision() As String
        Get
            Return l_conGasto_Emision
        End Get
        Set(ByVal value As String)
            l_conGasto_Emision = value
        End Set
    End Property

    Public Property es_Waiver() As String
        Get
            Return l_es_Waiver
        End Get
        Set(ByVal value As String)
            l_es_Waiver = value
        End Set
    End Property

    Public Property PNR() As String
        Get
            Return l_PNR
        End Get
        Set(ByVal value As String)
            l_PNR = value
        End Set
    End Property

    Public Property en_PNR() As String
        Get
            Return l_en_PNR
        End Get
        Set(ByVal value As String)
            l_en_PNR = value
        End Set
    End Property

    Public Property id_proveedor_GDS() As String
        Get
            Return l_id_proveedor_GDS
        End Get
        Set(ByVal value As String)
            l_id_proveedor_GDS = value
        End Set
    End Property

    Public Property id_SecuenciaInfante() As String
        Get
            Return l_id_SecuenciaInfante
        End Get
        Set(ByVal value As String)
            l_id_SecuenciaInfante = value
        End Set
    End Property

    Public Property IdPax() As String
        Get
            Return l_IdPax
        End Get
        Set(ByVal value As String)
            l_IdPax = value
        End Set
    End Property

    Public Property es_IT() As String
        Get
            Return l_es_IT
        End Get
        Set(ByVal value As String)
            l_es_IT = value
        End Set
    End Property

    Public Property QuienAutorizaDcto() As String
        Get
            Return l_QuienAutorizaDcto
        End Get
        Set(ByVal value As String)
            l_QuienAutorizaDcto = value
        End Set
    End Property

    Public Property es_tourcode_automatico() As String
        Get
            Return l_es_tourcode_automatico
        End Get
        Set(ByVal value As String)
            l_es_tourcode_automatico = value
        End Set
    End Property

    Public Property Factor_Meta() As String
        Get
            Return l_Factor_Meta
        End Get
        Set(ByVal value As String)
            l_Factor_Meta = value
        End Set
    End Property

    Public Property Over() As String
        Get
            Return l_Over
        End Get
        Set(ByVal value As String)
            l_Over = value
        End Set
    End Property

    Public Property Tarifa_No() As String
        Get
            Return l_Tarifa_No
        End Get
        Set(ByVal value As String)
            l_Tarifa_No = value
        End Set
    End Property

    Public Property Comision_No() As String
        Get
            Return l_Comision_No
        End Get
        Set(ByVal value As String)
            l_Comision_No = value
        End Set
    End Property

    Public Property NaceCancelado() As String
        Get
            Return l_NaceCancelado
        End Get
        Set(ByVal value As String)
            l_NaceCancelado = value
        End Set
    End Property

    Public Property es_InfanteAdulto() As String
        Get
            Return l_es_InfanteAdulto
        End Get
        Set(ByVal value As String)
            l_es_InfanteAdulto = value
        End Set
    End Property

    Public Property con_morosidad() As String
        Get
            Return l_con_morosidad
        End Get
        Set(ByVal value As String)
            l_con_morosidad = value
        End Set
    End Property

    Public Property numero_de_boleto() As String
        Get
            Return l_numero_de_boleto
        End Get
        Set(ByVal value As String)
            l_numero_de_boleto = value
        End Set
    End Property

    Public Property sin_facturar() As String
        Get
            Return l_sin_facturar
        End Get
        Set(ByVal value As String)
            l_sin_facturar = value
        End Set
    End Property

    Public Property importe_waiver() As String
        Get
            Return l_importe_waiver
        End Get
        Set(ByVal value As String)
            l_importe_waiver = value
        End Set
    End Property

    Public Property es_conexion() As String
        Get
            Return l_es_conexion
        End Get
        Set(ByVal value As String)
            l_es_conexion = value
        End Set
    End Property

    Public Property tarifa_auxiliar() As String
        Get
            Return l_tarifa_auxiliar
        End Get
        Set(ByVal value As String)
            l_tarifa_auxiliar = value
        End Set
    End Property

    Public Property importe_fee() As String
        Get
            Return l_importe_fee
        End Get
        Set(ByVal value As String)
            l_importe_fee = value
        End Set
    End Property

    Public Property Error_Message() As String
        Get
            Return l_error
        End Get
        Set(ByVal value As String)
            l_error = value
        End Set
    End Property

    Public Property tipo_de_waiver() As String
        Get
            Return l_tipo_de_waiver
        End Get
        Set(ByVal value As String)
            l_tipo_de_waiver = value
        End Set
    End Property

    Public Property tarifa_adicional() As String
        Get
            Return l_tarifa_adicional
        End Get
        Set(ByVal value As String)
            l_tarifa_adicional = value
        End Set
    End Property
    Public Property id_grupo_aereo() As String
        Get
            Return l_id_grupo_aereo
        End Get
        Set(ByVal value As String)
            l_id_grupo_aereo = value
        End Set
    End Property
    Public Property id_cotizacion_pax() As String
        Get
            Return l_id_cotizacion_pax
        End Get
        Set(ByVal value As String)
            l_id_cotizacion_pax = value
        End Set
    End Property
    Public Property tipo_comision_gds() As String
        Get
            Return l_tipo_comision_gds
        End Get
        Set(ByVal Value As String)
            l_tipo_comision_gds = Value
        End Set
    End Property
    Public Property comision_gds() As String
        Get
            Return l_comision_gds
        End Get
        Set(ByVal Value As String)
            l_comision_gds = Value
        End Set
    End Property
End Class
