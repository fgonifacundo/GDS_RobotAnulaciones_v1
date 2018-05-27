<Serializable()> _
Public Class classConceptosEvaluacion
    Private oCLASRESERV As String = String.Empty
    Private oSUCURSAL As String = String.Empty
    Private oTIPOSTOCK As String = String.Empty
    Private oCIUORIGEN As String = String.Empty
    Private oCIUREGRESO As String = String.Empty
    Private oCIUDESTINO As String = String.Empty
    Private oPAISDESTINO As String = String.Empty
    Private oFECRETORNO As String = String.Empty
    Private oPAISRETORNO As String = String.Empty
    Private oLIBRE1 As String = String.Empty
    Private oLIBRE2 As String = String.Empty
    Private oCODSHARE As String = String.Empty
    Private oTIPORUTA As String = String.Empty
    Private oUNIREGULA As String = String.Empty
    Private oTIPOVUELO As String = String.Empty
    Private oFECSALIDA As String = String.Empty
    Private oPAISORIGEN As String = String.Empty
    Private oLINAEREAAUX As String = String.Empty
    Private oCODSHARELINAEREA As String = String.Empty
    Private oCLASCABINA As String = String.Empty
    Private oCLASCABSAL As String = String.Empty
    Private oCLASCABRET As String = String.Empty
    Private oPAISFINVIAJE As String = String.Empty
    Private oCIUFINVIAJE As String = String.Empty
    Private oREGDES As String = String.Empty
    Private oREGRET As String = String.Empty
    Private oCIUAUXSAL As String = String.Empty
    Private oCIUAUXRET As String = String.Empty
    Private oTIPOVIAJE As String = String.Empty
    Private oFORMAPAGO As String = String.Empty
    Private oPAXCLERO As String = String.Empty
    Private oESREEMISION As String = String.Empty
    Private oNOVUELO As String = String.Empty
    Private oPSEUDO As String = String.Empty
    Private oIATACONSULTA As String = String.Empty
    Private oIATAEMISION As String = String.Empty
    Private oCLIENTE As String = String.Empty
    Private oINCLUIRYQ As String = String.Empty
    Private oNVUELOSALIDA As String = String.Empty
    Private oNVUELORETORNO As String = String.Empty
    Private oSUBCODIGO As String = String.Empty
    Private oACCOUNTCODE As String = String.Empty
    Private oTKTENCONJUNCION As String = String.Empty
    Private oCIUDADCONEXDESTINO As String = String.Empty
    Private oCIUDADCONEXRETORNO As String = String.Empty
    Private oCIUDADCONEXAPLICA As String = String.Empty
    Private lstConceptosTarifa As List(Of classCETarifa) = Nothing
    Private oLINEAVALIDADORA As String = String.Empty
    Private oTARIFASCORPORATIVAS As String = String.Empty
    Public Property CLASRESERV() As String
        Get
            Return oCLASRESERV
        End Get
        Set(ByVal value As String)
            oCLASRESERV = value
        End Set
    End Property
    Public Property SUCURSAL() As String
        Get
            Return oSUCURSAL
        End Get
        Set(ByVal value As String)
            oSUCURSAL = value
        End Set
    End Property
    Public Property TIPOSTOCK() As String
        Get
            Return oTIPOSTOCK
        End Get
        Set(ByVal value As String)
            oTIPOSTOCK = value
        End Set
    End Property
    Public Property CIUORIGEN() As String
        Get
            Return oCIUORIGEN
        End Get
        Set(ByVal value As String)
            oCIUORIGEN = value
        End Set
    End Property
    Public Property CIUREGRESO() As String
        Get
            Return oCIUREGRESO
        End Get
        Set(ByVal value As String)
            oCIUREGRESO = value
        End Set
    End Property
    Public Property CIUDESTINO() As String
        Get
            Return oCIUDESTINO
        End Get
        Set(ByVal value As String)
            oCIUDESTINO = value
        End Set
    End Property
    Public Property PAISDESTINO() As String
        Get
            Return oPAISDESTINO
        End Get
        Set(ByVal value As String)
            oPAISDESTINO = value
        End Set
    End Property
    Public Property FECRETORNO() As String
        Get
            Return oFECRETORNO
        End Get
        Set(ByVal value As String)
            oFECRETORNO = value
        End Set
    End Property
    Public Property PAISRETORNO() As String
        Get
            Return oPAISRETORNO
        End Get
        Set(ByVal value As String)
            oPAISRETORNO = value
        End Set
    End Property
    Public Property LIBRE1() As String
        Get
            Return oLIBRE1
        End Get
        Set(ByVal value As String)
            oLIBRE1 = value
        End Set
    End Property
    Public Property LIBRE2() As String
        Get
            Return oLIBRE2
        End Get
        Set(ByVal value As String)
            oLIBRE2 = value
        End Set
    End Property
    Public Property CODSHARE() As String
        Get
            Return oCODSHARE
        End Get
        Set(ByVal value As String)
            oCODSHARE = value
        End Set
    End Property
    Public Property TIPORUTA() As String
        Get
            Return oTIPORUTA
        End Get
        Set(ByVal value As String)
            oTIPORUTA = value
        End Set
    End Property
    Public Property UNIREGULA() As String
        Get
            Return oUNIREGULA
        End Get
        Set(ByVal value As String)
            oUNIREGULA = value
        End Set
    End Property
    Public Property TIPOVUELO() As String
        Get
            Return oTIPOVUELO
        End Get
        Set(ByVal value As String)
            oTIPOVUELO = value
        End Set
    End Property
    Public Property FECSALIDA() As String
        Get
            Return oFECSALIDA
        End Get
        Set(ByVal value As String)
            oFECSALIDA = value
        End Set
    End Property
    Public Property PAISORIGEN() As String
        Get
            Return oPAISORIGEN
        End Get
        Set(ByVal value As String)
            oPAISORIGEN = value
        End Set
    End Property
    Public Property LINAEREAAUX() As String
        Get
            Return oLINAEREAAUX
        End Get
        Set(ByVal value As String)
            oLINAEREAAUX = value
        End Set
    End Property
    Public Property CODSHARELINAEREA() As String
        Get
            Return oCODSHARELINAEREA
        End Get
        Set(ByVal value As String)
            oCODSHARELINAEREA = value
        End Set
    End Property
    Public Property CLASCABINA() As String
        Get
            Return oCLASCABINA
        End Get
        Set(ByVal value As String)
            oCLASCABINA = value
        End Set
    End Property
    Public Property CLASCABSAL() As String
        Get
            Return oCLASCABSAL
        End Get
        Set(ByVal value As String)
            oCLASCABSAL = value
        End Set
    End Property
    Public Property CLASCABRET() As String
        Get
            Return oCLASCABRET
        End Get
        Set(ByVal value As String)
            oCLASCABRET = value
        End Set
    End Property
    Public Property PAISFINVIAJE() As String
        Get
            Return oPAISFINVIAJE
        End Get
        Set(ByVal value As String)
            oPAISFINVIAJE = value
        End Set
    End Property
    Public Property CIUFINVIAJE() As String
        Get
            Return oCIUFINVIAJE
        End Get
        Set(ByVal value As String)
            oCIUFINVIAJE = value
        End Set
    End Property
    Public Property REGDES() As String
        Get
            Return oREGDES
        End Get
        Set(ByVal value As String)
            oREGDES = value
        End Set
    End Property
    Public Property REGRET() As String
        Get
            Return oREGRET
        End Get
        Set(ByVal value As String)
            oREGRET = value
        End Set
    End Property
    Public Property CIUAUXSAL() As String
        Get
            Return oCIUAUXSAL
        End Get
        Set(ByVal value As String)
            oCIUAUXSAL = value
        End Set
    End Property
    Public Property CIUAUXRET() As String
        Get
            Return oCIUAUXRET
        End Get
        Set(ByVal value As String)
            oCIUAUXRET = value
        End Set
    End Property
    Public Property TIPOVIAJE() As String
        Get
            Return oTIPOVIAJE
        End Get
        Set(ByVal value As String)
            oTIPOVIAJE = value
        End Set
    End Property
    Public Property FORMAPAGO() As String
        Get
            Return oFORMAPAGO
        End Get
        Set(ByVal value As String)
            oFORMAPAGO = value
        End Set
    End Property
    Public Property PAXCLERO() As String
        Get
            Return oPAXCLERO
        End Get
        Set(ByVal value As String)
            oPAXCLERO = value
        End Set
    End Property
    Public Property ESREEMISION() As String
        Get
            Return oESREEMISION
        End Get
        Set(ByVal value As String)
            oESREEMISION = value
        End Set
    End Property
    Public Property NOVUELO() As String
        Get
            Return oNOVUELO
        End Get
        Set(ByVal value As String)
            oNOVUELO = value
        End Set
    End Property
    Public Property PSEUDO() As String
        Get
            Return oPSEUDO
        End Get
        Set(ByVal value As String)
            oPSEUDO = value
        End Set
    End Property
    Public Property IATACONSULTA() As String
        Get
            Return oIATACONSULTA
        End Get
        Set(ByVal value As String)
            oIATACONSULTA = value
        End Set
    End Property
    Public Property IATAEMISION() As String
        Get
            Return oIATAEMISION
        End Get
        Set(ByVal value As String)
            oIATAEMISION = value
        End Set
    End Property
    Public Property CLIENTE() As String
        Get
            Return oCLIENTE
        End Get
        Set(ByVal value As String)
            oCLIENTE = value
        End Set
    End Property
    Public Property INCLUIRYQ() As String
        Get
            Return oINCLUIRYQ
        End Get
        Set(ByVal value As String)
            oINCLUIRYQ = value
        End Set
    End Property
    Public Property NVUELOSALIDA() As String
        Get
            Return oNVUELOSALIDA
        End Get
        Set(ByVal value As String)
            oNVUELOSALIDA = value
        End Set
    End Property
    Public Property NVUELORETORNO() As String
        Get
            Return oNVUELORETORNO
        End Get
        Set(ByVal value As String)
            oNVUELORETORNO = value
        End Set
    End Property
    Public Property SUBCODIGO() As String
        Get
            Return oSUBCODIGO
        End Get
        Set(ByVal value As String)
            oSUBCODIGO = value
        End Set
    End Property
    Public Property ACCOUNTCODE() As String
        Get
            Return oACCOUNTCODE
        End Get
        Set(ByVal value As String)
            oACCOUNTCODE = value
        End Set
    End Property
    Public Property TKTENCONJUNCION() As String
        Get
            Return oTKTENCONJUNCION
        End Get
        Set(ByVal value As String)
            oTKTENCONJUNCION = value
        End Set
    End Property
    Public Property CIUDADCONEXDESTINO() As String
        Get
            Return oCIUDADCONEXDESTINO
        End Get
        Set(ByVal value As String)
            oCIUDADCONEXDESTINO = value
        End Set
    End Property
    Public Property CIUDADCONEXRETORNO() As String
        Get
            Return oCIUDADCONEXRETORNO
        End Get
        Set(ByVal value As String)
            oCIUDADCONEXRETORNO = value
        End Set
    End Property
    Public Property CIUDADCONEXAPLICA() As String
        Get
            Return oCIUDADCONEXAPLICA
        End Get
        Set(ByVal value As String)
            oCIUDADCONEXAPLICA = value
        End Set
    End Property
    Public Property ConceptosTarifa() As List(Of classCETarifa)
        Get
            Return lstConceptosTarifa
        End Get
        Set(ByVal value As List(Of classCETarifa))
            lstConceptosTarifa = value
        End Set
    End Property
    Public Property LINEAVALIDADORA() As String
        Get
            Return oLINEAVALIDADORA
        End Get
        Set(ByVal value As String)
            oLINEAVALIDADORA = value
        End Set
    End Property
    Public Property TARIFASCORPORATIVAS() As String
        Get
            Return oTARIFASCORPORATIVAS
        End Get
        Set(ByVal value As String)
            oTARIFASCORPORATIVAS = value
        End Set
    End Property
End Class
Partial Public Class classCETarifa
    Private oFAREBASIS As String = String.Empty
    Private oCANTFARE As String = String.Empty
    Private oPrimeraLETFAREALL As String = String.Empty
    Private oPrimerLETFARESAL As String = String.Empty
    Private oPrimeraLETFARERET As String = String.Empty
    Private oTIPOPAXESPECIAL As String = String.Empty
    Private oTIPOPAX As String = String.Empty
    Private oFBSINTKTDESIG As String = String.Empty
    Public Property CANTFARE() As String
        Get
            Return oCANTFARE
        End Get
        Set(ByVal value As String)
            oCANTFARE = value
        End Set
    End Property
    Public Property FAREBASIS() As String
        Get
            Return oFAREBASIS
        End Get
        Set(ByVal value As String)
            oFAREBASIS = value
        End Set
    End Property
    Public Property PrimeraLETFAREALL() As String
        Get
            Return oPrimeraLETFAREALL
        End Get
        Set(ByVal value As String)
            oPrimeraLETFAREALL = value
        End Set
    End Property
    Public Property PrimerLETFARESAL() As String
        Get
            Return oPrimerLETFARESAL
        End Get
        Set(ByVal value As String)
            oPrimerLETFARESAL = value
        End Set
    End Property
    Public Property PrimeraLETFARERET() As String
        Get
            Return oPrimeraLETFARERET
        End Get
        Set(ByVal value As String)
            oPrimeraLETFARERET = value
        End Set
    End Property
    Public Property TIPOPAXESPECIAL() As String
        Get
            Return oTIPOPAXESPECIAL
        End Get
        Set(ByVal value As String)
            oTIPOPAXESPECIAL = value
        End Set
    End Property
    Public Property TIPOPAX() As String
        Get
            Return oTIPOPAX
        End Get
        Set(ByVal value As String)
            oTIPOPAX = value
        End Set
    End Property
    Public Property FBSINTKTDESIG() As String
        Get
            Return oFBSINTKTDESIG
        End Get
        Set(ByVal value As String)
            oFBSINTKTDESIG = value
        End Set
    End Property
End Class

Partial Class classConceptosEvaluacion
    Public Function MatrizConceptosModuloTourCode() As String(,)
        Dim aConceptoTarifas(,) As String = {{"Tarifas", "Tarifas"}, _
                                             {"1/FAREBASIS", ""}, {"2/CLASRESERV", ""}, _
                                             {"3/SUCURSAL", ""}, {"4/TIPOSTOCK", ""}, _
                                             {"5/1raLETFARE-SAL", ""}, {"6/CIUORIGEN", ""}, _
                                             {"7/CIUREGRESO", ""}, {"8/CIUDESTINO", ""}, _
                                             {"9/PAISDESTINO", ""}, {"10/FECRETORNO", ""}, _
                                             {"11/1raLETFARE-RET", ""}, {"12/TIPOPAX", ""}, _
                                             {"13/PAISRETORNO", ""}, {"14/LIBRE", ""}, _
                                             {"15/LIBRE", ""}, {"16/CODSHARE", ""}, _
                                             {"17/TIPORUTA", ""}, {"18/UNIREGULA", ""}, _
                                             {"19/TIPOVUELO(ON-OFF)", ""}, {"20/FECSALIDA", ""}, _
                                             {"21/PAISORIGEN", ""}, {"22/LINAEREAAUX", ""}, _
                                             {"23/CODSHARE-LINAEREA", ""}, {"24/CLASCABINA", ""}, _
                                             {"25/CANTFARE", ""}, {"26/CLASCAB-SAL", ""}, _
                                             {"27/CLASCAB-RET", ""}, {"28/1raLETFARE-ALL", ""}, _
                                             {"29/PAIS-FINVIAJE", ""}, {"30/CIU-FINVIAJE", ""}, _
                                             {"31/REG-DES", ""}, {"32/REG-RET", ""}, _
                                             {"33/CIUAUX-SAL", ""}, {"34/CIUAUX-RET", ""}, _
                                             {"35/TIPOVIAJE", ""}, {"36/FORMA-PAGO", ""}, _
                                             {"37/PAX-CLERO", ""}, {"38/TIPO-PAXESPECIAL", ""}, _
                                             {"39/ES-REEMISION", ""}, {"40/NO-VUELO", ""}, _
                                             {"41/PSEUDO", ""}, {"42/IATA", ""}, _
                                             {"43/CLIENTE", ""}, {"44/INCLUIRYQ", ""}, _
                                             {"45/No_VUELO-SALIDA", ""}, {"46/No_VUELO-RETORNO", ""}, _
                                             {"47/CON-RUC", ""}, {"48/SUBCODIGO", ""}, _
                                             {"49/ACCOUNT-CODE", ""}, {"50/FB_SIN_TKT-DESIG", ""}, _
                                             {"51/TKT_EN_CONJUNCION", ""}, {"52/CIUDAD_CONEX_DESTINO", ""}, _
                                             {"53/CIUDAD_CONEX_RETORNO", ""}, {"54/CIUDAD_CONEX_APLICA", ""}, _
                                             {"55/TARIFAS CORPORATIVAS", ""}}

        Return aConceptoTarifas

    End Function
End Class