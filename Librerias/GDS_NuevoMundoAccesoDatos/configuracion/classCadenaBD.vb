Public Class classCadenaBD
    Public CadenaPTA_Amadeus As String = "Data Source=tn_pta;User ID=usr_emisiones;Password=s3rv3r"
    Public CadenaPTA_Sabre As String = "Data Source=tn_pta;User ID=usr_turbo;Password=usr_turbo"
    'Public CadenaPTA_EasyOnLine As String = "Data Source=tn_pta;User ID=usr_emisiones;Password=s3rv3r"
    Public CadenaPTA_EasyOnLine As String = "Data Source=(DESCRIPTION=" _
+ "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.75.102.15)(PORT=1521)))" _
+ "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ORCL)));" _
+ "User Id=usr_emisiones;Password=s3rv3r"
    Public CadenaAppWebs As String = "Data Source=tn_webs;User ID=appwebs;Password=6109338"
    Public CadenaDemoAppWebs As String = "Data Source=tn_webs;User ID=demoappwebs;Password=6109338"
    Public CadenaPTA_DemoNuevoMundo As String = "Data Source=tn_pta;User ID=demonuevomundo;Password=demonuevomundo"
    Public CadenaPTA_Destinos As String = "Data Source=tn_pta;User ID=ptadestinos;Password=ptadestinos"
    Public Const CadenaSqlPtaDestinos As String = "data source = 10.75.102.47; initial catalog = DestinosMundiales; user id = kcuba@nmviajes.com; password = 3xR8a5fej4JR; Connection Timeout=210000"
End Class
