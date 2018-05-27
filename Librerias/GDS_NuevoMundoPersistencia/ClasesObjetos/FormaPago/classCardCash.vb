<Serializable()> _
Public Class classCardCash
    Public objCash As classMonto = Nothing
    Public objCard As classTarjeta_ = Nothing
    Public Property Card() As classTarjeta_
        Get
            Return objCard
        End Get
        Set(ByVal value As classTarjeta_)
            objCard = value
        End Set
    End Property
    Public Property Cash() As classMonto
        Get
            Return objCash
        End Get
        Set(ByVal value As classMonto)
            objCash = value
        End Set
    End Property
End Class
