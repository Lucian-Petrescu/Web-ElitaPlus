Imports System.Text
Imports System.Threading
Imports System.Linq
Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.Runtime.CompilerServices

Public Class ConfigureWorkQueueWebService
    Inherits ElitaPlusSearchPage

    Private Const PAGETAB As String = "ADMIN"
    Private Const PAGETITLE As String = "CONFIGURE_WORKQUEUE_WEB_SERVICE"
    Private Shared IsRunning As Boolean = False
    Private Shared outputStringBuilder As StringBuilder = New StringBuilder()

    Private Sub UpdateBreadCrum()
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        Me.MasterPage.UsePageTabTitleInBreadCrum = True
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.MasterPage.MessageController.Clear()
            UpdateBreadCrum()
            If (Not Me.IsPostBack) Then
                RunDiagnosticAsync(False)
            End If

            UpdateUI()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub UpdateUI()
        Output.InnerHtml = "<ul>" & outputStringBuilder.ToString() & "</ul>"

        btnFix.Enabled = Not IsRunning
        btnGrantCWQI.Enabled = Not IsRunning
        btnCreateUsers.Enabled = Not IsRunning
        btnDiagnostic.Enabled = Not IsRunning
        btnRefresh.Enabled = IsRunning

        If IsRunning Then
            Me.RegisterStartupScript("autoPostBack", "<script type=text/javascript>setInterval(function(){" + Page.GetPostBackEventReference(btnRefresh) + "},10000);</script>")
        End If
    End Sub

    Private Sub RunDiagnosticAsync(ByVal fix As Boolean)
        Dim createUsersThread As Thread
        createUsersThread = New Thread(New ParameterizedThreadStart(AddressOf RunDiagnostic))
        createUsersThread.Start(fix)

        Thread.CurrentThread.Sleep(100)

        UpdateUI()
    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Do nothing
    End Sub

    Protected Sub btnCreateUsers_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim createUsersThread As Thread
        createUsersThread = New Thread(New ThreadStart(AddressOf CreateUsers))
        createUsersThread.Start()

        Thread.CurrentThread.Sleep(100)

        UpdateUI()
    End Sub

    Protected Sub btnGrantCWQI_Click(ByVal sender As Object, ByVal e As EventArgs)
        Dim createUsersThread As Thread
        createUsersThread = New Thread(New ThreadStart(AddressOf GrantCWQI))
        createUsersThread.Start()

        Thread.CurrentThread.Sleep(100)

        UpdateUI()
    End Sub

    Protected Sub btnFix_Click(ByVal sender As Object, ByVal e As EventArgs)
        RunDiagnosticAsync(True)
    End Sub

    Protected Sub btnDiagnostic_Click(ByVal sender As Object, ByVal e As EventArgs)
        ShowAssemblyInformation()
    End Sub

    Private Sub ShowAssemblyInformation()
        Try
            IsRunning = True
            outputStringBuilder = New StringBuilder()
            outputStringBuilder.AddInformation("Starting Loaded Assembly List")

            Dim assemblies As System.Reflection.Assembly() = AppDomain.CurrentDomain.GetAssemblies()
            For iCount As Integer = Microsoft.VisualBasic.LBound(assemblies) To Microsoft.VisualBasic.UBound(assemblies)
                outputStringBuilder.AddInformation("Full Name : " + assemblies(iCount).FullName)
                outputStringBuilder.AddInformation("GlobalAssemblyCache : " + assemblies(iCount).GlobalAssemblyCache.ToString())
                Try
                    outputStringBuilder.AddInformation("Location : " + assemblies(iCount).Location)
                Catch ex As Exception
                End Try
                outputStringBuilder.AddInformation("")
            Next

            outputStringBuilder.AddInformation("Loaded Assembly List Finished")

            UpdateUI()
        Catch ex As Exception
            outputStringBuilder.AddError("An error occured in execution", ex)
        Finally
            IsRunning = False
        End Try
    End Sub

    Private Sub RunDiagnostic(ByVal objfix As Object)
        Try
            Dim fix As Boolean = DirectCast(objfix, Boolean)
            Dim sw As New System.Diagnostics.Stopwatch
            Dim oUser As Auth.User
            IsRunning = True
            outputStringBuilder = New StringBuilder()
            outputStringBuilder.AddInformation("Starting Diagnostics" + If(fix, " with Fix Option", String.Empty))

            sw.Start()

            Dim hasError As Boolean = False

            ' Check All Web Services in Non-Invasive
            ' Step 1 - Check Authorization Service
            Try
                ServiceHelper.CreateAuthorizationClient().GetGroups(ServiceHelper.WORKQUEUE_SERVICE_NAME)
                outputStringBuilder.AddSuccess("Authorization Service Call Successful!")
            Catch ex As Exception
                outputStringBuilder.AddError("Authorization Service Call Failed!", ex)
                hasError = True
            End Try

            ' Step 1A - Check if Current User Exists
            ' Check if Current User Exists
            Dim users As Auth.User()
            users = ServiceHelper.CreateAuthorizationClient().FindUsers(New Auth.User() With {.UserId = ElitaPlusIdentity.Current.ActiveUser.NetworkId})
            If (users Is Nothing OrElse users.Length = 0) Then
                outputStringBuilder.AddError(String.Format("Remote User {0} does not exists! Please run Create User.", ElitaPlusIdentity.Current.ActiveUser.NetworkId))
                hasError = True
            Else
                outputStringBuilder.AddSuccess(String.Format("Remote User {0} exists!", ElitaPlusIdentity.Current.ActiveUser.NetworkId))
                oUser = users(0)
            End If

            ' Step 2 - Check Work Queue Service
            Try
                ServiceHelper.CreateWorkQueueServiceClient().GetWorkQueues(ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                outputStringBuilder.AddSuccess("Work Queue Service Call Successful!")
            Catch ex As Exception
                outputStringBuilder.AddError("Work Queue Service Call Failed!", ex)
                hasError = True
            End Try

            ' Step 3 - Check Document Service
            Try
                ServiceHelper.CreateDocumentClient().Find(New Doc.FindRequest() With {.Name = "ABC"})
                outputStringBuilder.AddSuccess("Document Service Call Successful!")
            Catch ex As Exception
                outputStringBuilder.AddError("Document Service Call Failed!", ex)
                hasError = True
            End Try

            If (hasError) Then
                outputStringBuilder.AddError("Terminating Diagnostics")
                Exit Sub
            End If

            ' Find Role
            Dim mwqRole As Auth.Group = CreateRole("MWQ", "Manage Work Queue", fix, outputStringBuilder)
            Dim cwqiRole As Auth.Group = CreateRole("CWQI", "Create Work Queue Item", fix, outputStringBuilder)

            ' Grant Permission to MWQ Role
            Dim permissions As Guid()
            permissions = (From per As Auth.Permission In _
                ServiceHelper.CreateAuthorizationClient().FindPermissions(ServiceHelper.WORKQUEUE_SERVICE_NAME, New Auth.Permission() With {.ResourceType = ServiceHelper.RESTYP_WORKQUEUESYSTEM, .Resource = ServiceHelper.RES_WORKQUEUESYSTEM}) _
                Select per.Id).ToArray()
            ServiceHelper.CreateAuthorizationClient().AddPermissionsToGroup(permissions, mwqRole.Id)
            outputStringBuilder.AddSuccess("Granted Work Queue System Permissions to Manage Work Queue Role!")

            If (fix) Then
                ' Grant Role MWQ to Current User
                ServiceHelper.CreateAuthorizationClient().AddUsersToGroup(New Guid() {oUser.Id}, mwqRole.Id)
            End If

            ' Create Work Queue Types
            Dim wqtFIFO As WrkQueue.WorkQueueType
            wqtFIFO = (From wqt As WrkQueue.WorkQueueType In ServiceHelper.CreateWorkQueueServiceClient().GetWorkQueueTypes(ElitaPlusIdentity.Current.ActiveUser.NetworkId) Where wqt.Name = "FIFO" Select wqt).FirstOrDefault()
            If (wqtFIFO Is Nothing) Then
                If (fix) Then
                    wqtFIFO = New WrkQueue.WorkQueueType
                    With wqtFIFO
                        .CreatedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                        .Name = "FIFO"
                    End With
                    ServiceHelper.CreateWorkQueueServiceClient().CreateWorkQueueType(wqtFIFO, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                    outputStringBuilder.AddInformation("Work Queue Type FIFO created!")
                Else
                    outputStringBuilder.AddError("Work Queue Type FIFO not present!")
                End If
            Else
                outputStringBuilder.AddSuccess("Work Queue Type FIFO present!")
            End If

            ' Create Work Queue Data Types
            CreateWorkQueueDataTypes(ServiceHelper.WQI_DT_CLAIM_ID, fix, outputStringBuilder)
            CreateWorkQueueDataTypes(ServiceHelper.WQI_DT_CLAIM_NUMBER, fix, outputStringBuilder)
            CreateWorkQueueDataTypes(ServiceHelper.WQI_DT_COMPANY_CODE, fix, outputStringBuilder)
            CreateWorkQueueDataTypes(ServiceHelper.WQI_DT_CLAIM_ISSUE_ID, fix, outputStringBuilder)
            CreateWorkQueueDataTypes(ServiceHelper.WQI_DT_IMAGE_ID, fix, outputStringBuilder)
            CreateWorkQueueDataTypes(ServiceHelper.WQI_DT_SCAN_DATE, fix, outputStringBuilder)

            ' Create Document Type - TIFF
            Dim documentFormat As DocAdmin.DocumentFormat
            documentFormat = (From docFormat As DocAdmin.DocumentFormat In ServiceHelper.CreateDocumentAdminClient().GetDocumentFormats() Where docFormat.Name = "TIFF" Select docFormat).FirstOrDefault()
            If (documentFormat Is Nothing) Then
                If (fix) Then
                    documentFormat = New DocAdmin.DocumentFormat
                    documentFormat.Name = "TIFF"
                    documentFormat.CreatedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                    documentFormat.Description = "Tagged Image File Format"
                    documentFormat.Extension = "TIFF"
                    ServiceHelper.CreateDocumentAdminClient().CreateDocumentFormat(documentFormat)
                    outputStringBuilder.AddInformation("Document Format TIFF created!")
                Else
                    outputStringBuilder.AddError("Document Format TIFF not present!")
                End If
            Else
                outputStringBuilder.AddSuccess(String.Format("Document Format TIFF present! [ ID = {0} ]", documentFormat.Id.ToString()))
            End If

            If (fix) Then
                ' Revoke Role MWQ to Current User
                ServiceHelper.CreateAuthorizationClient().RemoveUsersFromGroup(New Guid() {oUser.Id}, mwqRole.Id)
            End If

            outputStringBuilder.AddSuccess("Completed Diagnostics")
        Catch ex As Exception
            outputStringBuilder.AddError("An error occured in execution", ex)
        Finally
            IsRunning = False
        End Try
    End Sub

    Private Sub CreateUsers()
        Try
            Dim sw As New System.Diagnostics.Stopwatch
            IsRunning = True
            outputStringBuilder = New StringBuilder()
            outputStringBuilder.AddInformation("Starting Creating Users")
            sw.Start()

            Dim searchDV As Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV
            searchDV = Assurant.ElitaPlus.BusinessObjectsNew.User.GetUserNewList("*", "*", "*", "*")
            Dim authClient As Auth.AuthorizationClient = ServiceHelper.CreateAuthorizationClient()
            Dim remoteRoleProvider As IRemoteRoleProvider = BaseRoleProvider.CreateRoleProvider(Codes.ROLE_PROVIDER__WORKQUEUE)
            Dim numberOfUsers As Integer = searchDV.Count
            Dim i As Integer = 0
            For Each drv As DataRowView In searchDV
                Dim networkId As String
                networkId = Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.NetworkID(drv.Row)
                Dim isActive As Boolean = False
                If (Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.IsActive(drv.Row) = "Y") Then
                    isActive = True
                Else
                    isActive = False
                End If
                Dim usr As Auth.User = authClient.FindUsers(New Auth.User() With {.UserId = networkId}).FirstOrDefault()
                Try
                    If (usr Is Nothing) Then
                        remoteRoleProvider.CreateUser(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.UserId(drv.Row), _
                            networkId, Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.UserName(drv.Row), isActive)
                    Else
                        remoteRoleProvider.UpdateUser(Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.UserId(drv.Row), _
                            networkId, networkId, Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.UserName(drv.Row), isActive)
                    End If
                Catch ex As BOValidationException
                    outputStringBuilder.AddError(String.Format("Failed to create user {0} because {1}", networkId, ex.GetMessage()))
                End Try
                i = i + 1
                If (i Mod 50 = 0) Then
                    outputStringBuilder.AddInformation(String.Format("Created {0} of {1} Users", i, numberOfUsers))
                End If
            Next
            outputStringBuilder.AddInformation(String.Format("Created {0} of {1} Users", numberOfUsers, numberOfUsers))
            sw.Stop()

            outputStringBuilder.AddInformation(String.Format("The process took {0} Seconds", sw.ElapsedMilliseconds / 1000))
            outputStringBuilder.AddSuccess("Creating Users Completed")

        Catch ex As Exception
            outputStringBuilder.AddError("An error occured in execution")
        Finally
            IsRunning = False
        End Try
    End Sub

    Private Sub GrantCWQI()
        Try
            Dim sw As New System.Diagnostics.Stopwatch
            IsRunning = True
            outputStringBuilder = New StringBuilder()
            outputStringBuilder.AddInformation("Starting to Grant Create Work Queue Item Role to All Active Users")
            sw.Start()

            Dim cwqiRole As Auth.Group = CreateRole("CWQI", "Create Work Queue Item", False, outputStringBuilder)

            If (cwqiRole Is Nothing) Then
                outputStringBuilder.AddError("CWQI Role not found!")
                outputStringBuilder.AddError("Terminating to Grant Create Work Queue Item Role to All Active Users")
                Exit Sub
            End If

            ' Grant Roles
            Dim searchDV As Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV

            Dim userIds(-1) As Guid
            searchDV = Assurant.ElitaPlus.BusinessObjectsNew.User.GetUserNewList("*", "*", "*", "*")
            Dim authClient As Auth.AuthorizationClient = ServiceHelper.CreateAuthorizationClient()
            Dim numberOfUsers As Integer = searchDV.Count
            Dim i As Integer = 0
            For Each drv As DataRowView In searchDV
                If (Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.IsActive(drv.Row) = "Y") Then
                    Dim networkId As String
                    networkId = Assurant.ElitaPlus.BusinessObjectsNew.User.UserSearchDV.NetworkID(drv.Row)
                    Dim usr As Auth.User = authClient.FindUsers(New Auth.User() With {.UserId = networkId}).FirstOrDefault()
                    If (usr Is Nothing) Then
                        outputStringBuilder.AddError(String.Format("Remote User with Network ID {0} not found!", networkId))
                    Else
                        ReDim Preserve userIds(userIds.Length)
                        userIds(userIds.Length - 1) = usr.Id
                        If (userIds.Length > 50) Then
                            authClient.AddUsersToGroup(userIds, cwqiRole.Id)
                            ReDim userIds(-1)
                        End If
                    End If
                End If
                i = i + 1
                If (i Mod 50 = 0) Then
                    outputStringBuilder.AddInformation(String.Format("Granted Role CWQI to {0} of {1} Users", i, numberOfUsers))
                End If
            Next
            If (userIds.Length > 0) Then
                authClient.AddUsersToGroup(userIds, cwqiRole.Id)
            End If
            outputStringBuilder.AddInformation(String.Format("Granted Role CWQI to {0} of {1} Users", numberOfUsers, numberOfUsers))
            sw.Stop()

            outputStringBuilder.AddInformation(String.Format("The process took {0} Seconds", sw.ElapsedMilliseconds / 1000))
            outputStringBuilder.AddSuccess("Completed to Grant Create Work Queue Item Role to All Active Users")
        Catch ex As Exception
            outputStringBuilder.AddError("An error occured in execution")
        Finally
            IsRunning = False
        End Try
    End Sub

    Private Function CreateRole(ByVal roleName As String, ByVal roleDescription As String, ByVal fix As Boolean, ByVal sb As StringBuilder) As Auth.Group
        Dim returnVal As Auth.Group
        returnVal = (From grp As Auth.Group In ServiceHelper.CreateAuthorizationClient().GetGroups(ServiceHelper.WORKQUEUE_SERVICE_NAME) Where grp.Name = roleName Select grp).FirstOrDefault()
        If (returnVal Is Nothing) Then
            If (fix) Then
                Dim cwqiLocalRole As New Role
                cwqiLocalRole.Code = roleName
                cwqiLocalRole.Description = roleDescription
                cwqiLocalRole.IhqOnly = "N"
                cwqiLocalRole.RoleProviderId = LookupListNew.GetIdFromCode(Codes.ROLE_PROVIDER, Codes.SERVICE_TYPE__WORK_QUEUE)
                cwqiLocalRole.Save()
                returnVal = (From grp As Auth.Group In ServiceHelper.CreateAuthorizationClient().GetGroups(ServiceHelper.WORKQUEUE_SERVICE_NAME) Where grp.Name = roleName Select grp).FirstOrDefault()
                sb.AddInformation(String.Format("Role {0} created!", roleName))
            Else
                sb.AddError(String.Format("Role {0} not present", roleName))
            End If
        Else
            sb.AddSuccess(String.Format("Role {0} present!", roleName))
        End If
        Return returnVal
    End Function

    Private Sub CreateWorkQueueDataTypes(ByVal dataTypeName As String, ByVal fix As Boolean, ByVal sb As StringBuilder)
        ' Create Work Queue Data Types
        Dim wqidt As WrkQueue.WorkQueueItemDataType
        wqidt = (From dt As WrkQueue.WorkQueueItemDataType In ServiceHelper.CreateWorkQueueServiceClient().GetWorkQueueItemDataTypes(ElitaPlusIdentity.Current.ActiveUser.NetworkId) Where dt.Name = dataTypeName Select dt).FirstOrDefault()
        If (wqidt Is Nothing) Then
            If (fix) Then
                wqidt = New WrkQueue.WorkQueueItemDataType
                With wqidt
                    .CreatedBy = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                    .Name = dataTypeName
                End With
                ServiceHelper.CreateWorkQueueServiceClient().CreateWorkQueueItemDataType(wqidt, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                sb.AddInformation(String.Format("Work Queue Item Data Type {0} created!", dataTypeName))
            Else
                sb.AddError(String.Format("Work Queue Item Data Type {0} not present", dataTypeName))
            End If
        Else
            sb.AddSuccess(String.Format("Work Queue Item Data Type {0} present! [ ID = {1} ]", dataTypeName, wqidt.Id.ToString()))
        End If
    End Sub

End Class

Public Module BOValidationExceptionExtention
    <Extension()> _
    Public Function GetMessage(ByVal ex As BOValidationException) As String
        Dim sb As StringBuilder = New StringBuilder()
        For Each valErr As ValidationError In ex.ValidationErrorList()
            sb.Append(valErr.Message).Append("<br />")
        Next
        Return sb.ToString()
    End Function
End Module

Public Module StringBuilderExtention
    <Extension()> _
    Public Sub AddSuccess(ByVal sb As StringBuilder, ByVal message As String)
        sb.Append(String.Format("<li class=""success"">{0}</li>", message))
    End Sub

    <Extension()> _
    Public Sub AddError(ByVal sb As StringBuilder, ByVal message As String)
        sb.Append(String.Format("<li class=""error"">{0}</li>", message))
    End Sub

    <Extension()> _
    Public Sub AddError(ByVal sb As StringBuilder, ByVal message As String, ByVal ex As Exception)
        sb.Append(String.Format("<li class=""error"">{0}<br><p class=""exception"">{1}</p></li>", message, ex.FormattedString()))
    End Sub

    <Extension()> _
    Public Sub AddInformation(ByVal sb As StringBuilder, ByVal message As String)
        sb.Append(String.Format("<li class=""info"">{0}</li>", message))
    End Sub

    <Extension()> _
    Public Function FormattedString(ByVal ex As Exception) As String
        Return String.Format("{0}<br />{1}<br />{2}", ex.Message, ex.Source, ex.StackTrace)
    End Function
End Module