Public NotInheritable Class ServiceHelper

    Public Const WORKQUEUE_SERVICE_NAME As String = "WorkQueue"

    '' Resource Types
    Public Const RESTYP_WORKQUEUE As String = "WorkQueue"
    Public Const RESTYP_WORKQUEUESYSTEM As String = "WorkQueueSystem"

    '' Resource
    Public Const RES_WORKQUEUESYSTEM As String = "WorkQueueSystem"

    '' Permission Actions
    Public Const PA_WQS_VIEW_STATISTICS As String = "ViewStats"
    Public Const PA_WQS_CREATE_QUEUE As String = "CreateQueue"
    Public Const PA_WQS_MANAGE_DATA_TYPE As String = "ManageItemDataTypeList"
    Public Const PA_WQS_MANAGE_ITEM_STATUS As String = "ManageItemStatusList"

    Public Const PA_WQ_ADD_ITEM As String = "AddItem"
    Public Const PA_WQ_EDIT As String = "Edit"
    Public Const PA_WQ_VIEW As String = "View"
    Public Const PA_WQ_PROCESS As String = "ProcessItem"

    '' WorkQueue MetaData
    Public Const WQ_MD_COMPANY_CODE As String = "CompanyCode"
    Public Const WQ_MD_ACTION_CODE As String = "ActionCode"
    Public Const WQ_MD_ADMIN_ROLE As String = "AdminRole"
    Public Const WQ_MD_TRANSFORMATION_FILE As String = "TransformationFile"

    '' WorkQueueItem Data Types
    Public Const WQI_DT_CLAIM_ID As String = "ClaimId"
    Public Const WQI_DT_CLAIM_NUMBER As String = "ClaimNumber"
    Public Const WQI_DT_COMPANY_CODE As String = "CompanyCode"
    Public Const WQI_DT_CLAIM_ISSUE_ID As String = "ClaimIssueId"
    Public Const WQI_DT_IMAGE_ID As String = "ImageId"
    Public Const WQI_DT_SCAN_DATE As String = "ScanDate"


    '' WorkQueueItemStatusReason
    Public Const WQISR_DEFAULT_COMPLETED As String = "Completed - System Generated"
    Public Const WQISR_DEFAULT_REQUEUE As String = "Re-queue - System Generated"


    '' DO NOT DELETE.
    '' The purpose of Private Constructor used to control Creation of Instance of this Class.
    Private Sub New()
    End Sub

    Public Shared Function CreateWorkQueueServiceClient() As WrkQueue.WorkQueueServiceClient
        Dim wrkQueClient As WrkQueue.WorkQueueServiceClient
        Dim oWebPasswd As WebPasswd
        oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__WORK_QUEUE), True)
        wrkQueClient = New WrkQueue.WorkQueueServiceClient("CustomBinding_IWorkQueueService", oWebPasswd.Url)
        wrkQueClient.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        wrkQueClient.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return wrkQueClient
    End Function

    Public Shared Function CreateAuthorizationClient() As Auth.AuthorizationClient
        Dim authClient As Auth.AuthorizationClient
        Dim oWebPasswd As WebPasswd
        oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__AUTHORIZATION), True)
        authClient = New Auth.AuthorizationClient("CustomBinding_IAuthorization", oWebPasswd.Url)
        authClient.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        authClient.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return authClient
    End Function

    Public Shared Function CreateDocumentAdminClient() As DocAdmin.DocumentAdminClient
        Dim DocAdminClient As DocAdmin.DocumentAdminClient
        Dim oWebPasswd As WebPasswd
        oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__DOCUMENT_ADMIN), True)
        DocAdminClient = New DocAdmin.DocumentAdminClient("CustomBinding_IDocumentAdmin", oWebPasswd.Url)
        DocAdminClient.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        DocAdminClient.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return DocAdminClient
    End Function

    Public Shared Function CreateDocumentClient() As Doc.DocumentClient
        Dim DocClient As Doc.DocumentClient
        Dim oWebPasswd As WebPasswd
        oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__DOCUMENT_IMAGING), True)
        DocClient = New Doc.DocumentClient("CustomBinding_IDocument", oWebPasswd.Url)
        DocClient.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        DocClient.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return DocClient
    End Function

    Public Shared Function CreateDRPClient() As DRPSystem.MaxValueRecoveryClient
        Dim DRPSystemClient As DRPSystem.MaxValueRecoveryClient
        Dim oWebPasswd As WebPasswd
        oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE__DRP_SYSTEM), True)

        'If EnvironmentContext.Current.Environment = Environments.Production Then
        '    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3
        'End If


        DRPSystemClient = New DRPSystem.MaxValueRecoveryClient("BasicHttpBinding_MaxValueRecovery", oWebPasswd.Url)
        DRPSystemClient.ClientCredentials.UserName.UserName = oWebPasswd.UserId
        DRPSystemClient.ClientCredentials.UserName.Password = oWebPasswd.Password
        Return DRPSystemClient
    End Function

End Class
