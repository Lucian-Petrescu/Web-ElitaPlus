Public Class ElitaWebServiceConstants

    ' Binding Parameters
    Public Const PARAM_COUNT_0 As Integer = 0
    Public Const PARAM_COUNT_1 As Integer = 1
    Public Const PARAM_COUNT_2 As Integer = 2
    ' ************************************************
    '         To KEEP BEGIN
    Public Const BINDING_XML As String = "X"
    Public Const BINDING_XML_FUNC As String = "XF"
    '         To KEEP END
    ' ************************************************
    '         To DELETE BEGIN
    'Public Const BINDING_XML As String = "N"
    '         To DELETE END
    ' ************************************************
    ' For VSC
    Public Const PROC_VSC_QUOTE As String = "GetQuote"
    Public Const PROC_VSC_ENROLL As String = "Enroll"
    Public Const PROC_VSC_GETMODELS As String = "GetModels"
    Public Const PROC_VSC_GETVERSIONS As String = "GetVersions"

    ' For Olita
    Public Const GET_CERT_USING_TRAN_NO As String = "GETCERTUSINGTRANNO"
    Public Const UPDAT_CONSUMER_INFO As String = "UPDATECONSUMERINFO"
    Public Const GET_REGIONS As String = "GETREGIONS"

    ' SETTING
    Public Const WEB_SERVICE_SETTING_TAG As String = "WEB_SERVICE"
    Public Const SETTING_TAG As String = "REQUEST"
    Public Const SETTING_TAG_ATTRIBUTE As String = "name"
    Public Const SETTING_TAG_SCHEMA As String = "SCHEMA"
    Public Const SETTING_TAG_BINDING As String = "BINDING"
    Public Const SETTING_TAG_BO_CLASS As String = "BO_CLASS"
    Public Const SETTING_TAG_SECURITY As String = "SECURITY"
    ' Public Const SETTING_CONFIG_PATH As String = "..\Services.xml"
    Public Const SETTING_CONFIG_NAME As String = "Services.xml"
    '  Public Shared WSE_APPLICATION_PATH As String = HttpContext.Current.Request.ApplicationPath
    '  Public Shared WSE_SETTING_CONFIG_PATH As String = WSE_APPLICATION_PATH & SETTING_CONFIG_NAME
    Public Const SETTING_SCHEMA_DIR As String = "schemas/"
    '  Public Shared WSE_SETTING_SCHEMA_DIR As String = WSE_APPLICATION_PATH & "/" & SETTING_SCHEMA_DIR
    ' Public Const SETTING_SCHEMA_PATH As String = "../schemas/"
    Public Const SETTING_BO_PATH As String = "Assurant.ElitaPlus.BusinessObjectsNew"

    ' GENERAL
    Public Const NAME_SCHEMA_EXTENSION As String = ".xsd"
    Public Const NAME_DATASET_SUFFIX As String = "Ds"

    'US 203684
    Public Const SPECIALIZED_SERVICE_CERTIFICATE_MANAGER = "SpecializedService_CertificateManager"
    Public Const SPECIALIZED_SERVICE_TIMB_CERTIFICATE_REPOSITORY = "SpecializedService_TIMB_CertificateRepository"

    'US 203685
    Public Const SPECIALIZED_SERVICE_CLAIM_MANAGER = "SpecializedService_ClaimManager"
    Public Const SPECIALIZED_SERVICE_TIMB_CLAIM_REPOSITORY = "SpecializedService_TIMB_ClaimRepository"
End Class
