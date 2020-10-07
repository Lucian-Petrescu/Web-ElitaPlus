Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Namespace Security
    Partial Public Class ServerForm
        Inherits ElitaPlusSearchPage

#Region "Constants"


        Public Const URL As String = "ServerForm.aspx"
        Public Const PAGETITLE As String = "SERVER"
        Public Const PAGETAB As String = "ADMIN"

        Public Const SERVER_DIRTY_COLUMNS_COUNT As Integer = 1

        Private Const GRID_SERVERS_TEST_BUTTON As String = "btnExecute"

#End Region

        '#Region "Page Return Type"
        '        Public Class ReturnType
        '            Public LastOperation As DetailPageCommand
        '            Public EditingBo As Assurant.ElitaPlus.BusinessObjectsNew.User
        '            Public HasDataChanged As Boolean
        '            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Assurant.ElitaPlus.BusinessObjectsNew.User, ByVal hasDataChanged As Boolean)
        '                Me.LastOperation = LastOp
        '                Me.EditingBo = curEditingBo
        '                Me.HasDataChanged = hasDataChanged
        '            End Sub
        '        End Class

        '#End Region
#Region "Page State"

        Class MyState
            Public MyBO As Servers
            Public ScreenSnapShotBO As Servers
            Public IsNew As Boolean
            Public IsACopy As Boolean
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    State.MyBO = New Servers(CType(CallingParameters, Guid))
                Else
                    State.IsNew = True
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Servers
            Public HasDataChanged As Boolean

            Public Sub New(LastOp As DetailPageCommand, curEditingBo As Servers, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Handlers"

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrControllerMaster.Clear_Hide()
            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    ' Me.MenuEnabled = False
                    AddConfirmation(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                    If State.MyBO Is Nothing Then
                        State.MyBO = New Servers
                    End If

                    If State.IsNew = True Then
                        CreateNew()
                    End If
                    PopulateFormFromBOs()
                    EnableDisableFields()
                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()
                If Not IsPostBack Then
                    AddLabelDecorations(State.MyBO)
                End If
                DisplayProgressBarOnClick(CType(btnExecuteCurrent, WebControl), "Running Test")

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

#End Region


#Region "Handlers-Buttons"

        Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click, btnBack2.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    Dim iDirtyCols As Integer
                    iDirtyCols = State.MyBO.DirtyColumns.Count
                    If iDirtyCols > SERVER_DIRTY_COLUMNS_COUNT Then
                        AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenDeletePromptResponse)
                        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                    Else
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    End If
                Else
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
                AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, HiddenDeletePromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = ErrControllerMaster.Text
            End Try
        End Sub
        Private Sub btnCopy_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewWithCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
        Private Sub btnDelete_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Dim addressDeleted As Boolean
            Try
                'Delete the Address
                State.MyBO.DeleteAndSave()
                State.HasDataChanged = True
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Private Sub btnNew_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                PopulateBOsFromForm()
                If (State.MyBO.IsDirty) Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDeletePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnApply_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    State.MyBO.Save()
                    State.HasDataChanged = True
                    PopulateFormFromBOs()
                    EnableDisableFields()
                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    'PopulateAll()
                Else
                    AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New Servers(State.MyBO.Id)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    CreateNew()
                End If
                PopulateFormFromBOs()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#End Region


#Region "Populate"


#End Region

#Region "Controlling Logic"

        Protected Sub EnableDisableFields()
            'Enabled by Default
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

            If State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            End If

        End Sub


        Protected Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(State.MyBO, "Description", DescriptionLabel)
            BindBOPropertyToLabel(State.MyBO, "HubRegion", HubRegionLabel)
            BindBOPropertyToLabel(State.MyBO, "Environment", EnvironmentLabel)
            BindBOPropertyToLabel(State.MyBO, "MachinePrefix", MachinePrefixLabel)
            BindBOPropertyToLabel(State.MyBO, "FtpHostname", FtpHostNameLabel)
            BindBOPropertyToLabel(State.MyBO, "FtpHostPath", FtpHostPathLabel)
            BindBOPropertyToLabel(State.MyBO, "FtpTriggerExtension", FtpTriggerExtensionLabel)
            BindBOPropertyToLabel(State.MyBO, "FtpSplitPath", FtpSplitPathLabel)
            BindBOPropertyToLabel(State.MyBO, "CrystalSdk", CrystalSdkLabel)
            BindBOPropertyToLabel(State.MyBO, "CrystalViewer", CrystalViewerLabel)
            BindBOPropertyToLabel(State.MyBO, "FelitaFtpHostname", FelitaFtpHostNameLabel)
            BindBOPropertyToLabel(State.MyBO, "LdapIp", LdapIpLabel)
            BindBOPropertyToLabel(State.MyBO, "SmartStreamHostName", SmartStreamHostNameLabel)
            BindBOPropertyToLabel(State.MyBO, "ServiceOrderImageHost", ServiceOrderImageHostNameLabel)
            BindBOPropertyToLabel(State.MyBO, "EuropeanPrivacy", EuropeanPrivacyLabel)
            BindBOPropertyToLabel(State.MyBO, "DatabaseName", DatabaseNameLabel)
            BindBOPropertyToLabel(State.MyBO, "BatchHostname", BatchHostnameLabel)
            BindBOPropertyToLabel(State.MyBO, "AcctBalanceHostname", AcctBalHostnameLabel)
            BindBOPropertyToLabel(State.MyBO, "SmartStreamAPUpload", SmartStreamAPUploadLabel)
            BindBOPropertyToLabel(State.MyBO, "SmartStreamGLStatus", SmartStreamGLStatusLabel)
            BindBOPropertyToLabel(State.MyBO, "SmartStreamGLUpload", SmartStreamGLUploadLabel)
            BindBOPropertyToLabel(State.MyBO, "DBUniqueName", UniqueDBLabel)
            BindBOPropertyToLabel(State.MyBO, "NoOfParallelProcesses", NoOfParallelProcessesLabel)
            BindBOPropertyToLabel(State.MyBO, "CommitFrequency", CommitFrequencyLabel)


            ClearGridHeadersAndLabelsErrSign()
        End Sub

        Protected Sub PopulateFormFromBOs()
            Dim yesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Dim yesNoId As Guid
            EuropeanPrivacyDrop.Populate(yesNoList, New PopulateOptions() With
                {
                    .AddBlankItem = False
                })

            'Dim langId As Guid = Authentication.LangId
            'Dim yesNoLkL As DataView = LookupListNew.GetYesNoLookupList(langId, False)
            'Me.BindListControlToDataView(Me.EuropeanPrivacyDrop, yesNoLkL, , , False)

            With State.MyBO
                PopulateControlFromBOProperty(DescriptionTextBox, .Description)
                PopulateControlFromBOProperty(HubRegionTextBox, .HubRegion)
                PopulateControlFromBOProperty(MachinePrefixTextBox, .MachinePrefix)
                PopulateControlFromBOProperty(EnvironmentextBox, .Environment)
                PopulateControlFromBOProperty(FtpHostNameTextBox, .FtpHostname)
                PopulateControlFromBOProperty(FelitaFtpHostNameTextBox, .FelitaFtpHostname)
                PopulateControlFromBOProperty(LdapIpTextBox, .LdapIp)
                PopulateControlFromBOProperty(FtpHostPathTextBox, .FtpHostPath)
                PopulateControlFromBOProperty(FtpTriggerExtensionTextBox, .FtpTriggerExtension)
                PopulateControlFromBOProperty(FtpSplitPathTextBox, .FtpSplitPath)
                PopulateControlFromBOProperty(SmartstreamHostnameTextBox, .SmartStreamHostName)
                PopulateControlFromBOProperty(ServiceorderImageHostnameTextBox, .ServiceOrderImageHost)
                PopulateControlFromBOProperty(BatchHostnameTextBox, .BatchHostname)
                PopulateControlFromBOProperty(AcctBalHostnameTextBox, .AcctBalanceHostname)

                If .PrivacyLevelXCD Is Nothing Then
                    .PrivacyLevelXCD = Codes.YESNO_N
                End If

                'yesNoId = LookupListNew.GetIdFromCode(yesNoLkL, .PrivacyLevelXCD)
                yesNoId = (From yn In yesNoList
                           Where yn.Code = .PrivacyLevelXCD
                           Select yn.ListItemId).FirstOrDefault()

                SetSelectedItem(EuropeanPrivacyDrop, yesNoId)
                PopulateControlFromBOProperty(DatabaNameTextBox, .DatabaseName)

                PopulateControlFromBOProperty(SmartStreamAPUploadTextBox, .SmartStreamAPUpload)
                PopulateControlFromBOProperty(SmartStreamGLStatusTextBox, .SmartStreamGLStatus)
                PopulateControlFromBOProperty(SmartStreamGLUploadTextBox, .SmartStreamGLUpload)

                PopulateControlFromBOProperty(UniqueDBTextBox, .DBUniqueName)
                PopulateControlFromBOProperty(NoOfParallelProcessesTextBox, .NoOfParallelProcesses)
                PopulateControlFromBOProperty(CommitFrequencyTextBox, .CommitFrequency)

            End With

            'Populate the diagnostics tab:
            Dim dvServers As BusinessObjectsNew.Servers.SearchDV
            dvServers = BusinessObjectsNew.Servers.GetServersList("", "")
            grdBatch.DataSource = dvServers
            grdBatch.DataBind()

        End Sub

        Protected Sub PopulateBOsFromForm()
            Dim langId As Guid = Authentication.LangId
            Dim yesNoLkL As DataView = LookupListNew.GetYesNoLookupList(langId, False)
            Dim yesNoId As Guid
            Dim yesNoCode As String

            With State.MyBO
                PopulateBOProperty(State.MyBO, "Description", DescriptionTextBox)
                PopulateBOProperty(State.MyBO, "HubRegion", HubRegionTextBox)
                PopulateBOProperty(State.MyBO, "MachinePrefix", MachinePrefixTextBox)
                PopulateBOProperty(State.MyBO, "Environment", EnvironmentextBox)
                PopulateBOProperty(State.MyBO, "FtpHostname", FtpHostNameTextBox)
                PopulateBOProperty(State.MyBO, "CrystalSdk", CrystalSdkTextBox)
                PopulateBOProperty(State.MyBO, "CrystalViewer", CrystalViewerTextBox)
                PopulateBOProperty(State.MyBO, "FelitaFtpHostname", FelitaFtpHostNameTextBox)
                PopulateBOProperty(State.MyBO, "LdapIp", LdapIpTextBox)
                PopulateBOProperty(State.MyBO, "FtpHostPath", FtpHostPathTextBox)
                PopulateBOProperty(State.MyBO, "FtpTriggerExtension", FtpTriggerExtensionTextBox)
                PopulateBOProperty(State.MyBO, "FtpSplitPath", FtpSplitPathTextBox)
                PopulateBOProperty(State.MyBO, "SmartStreamHostName", SmartstreamHostnameTextBox)
                PopulateBOProperty(State.MyBO, "ServiceOrderImageHost", ServiceorderImageHostnameTextBox)
                yesNoId = GetSelectedItem(EuropeanPrivacyDrop)
                yesNoCode = LookupListNew.GetCodeFromId(yesNoLkL, yesNoId)
                PopulateBOProperty(State.MyBO, "EuropeanPrivacy", yesNoCode)
                PopulateBOProperty(State.MyBO, "DatabaseName", DatabaNameTextBox)
                PopulateBOProperty(State.MyBO, "BatchHostname", BatchHostnameTextBox)
                PopulateBOProperty(State.MyBO, "AcctBalanceHostname", AcctBalHostnameTextBox)
                PopulateBOProperty(State.MyBO, "SmartStreamAPUpload", SmartStreamAPUploadTextBox)
                PopulateBOProperty(State.MyBO, "SmartStreamGLStatus", SmartStreamGLStatusTextBox)
                PopulateBOProperty(State.MyBO, "SmartStreamGLUpload", SmartStreamGLUploadTextBox)
                PopulateBOProperty(State.MyBO, "DBUniqueName", UniqueDBTextBox)
                PopulateBOProperty(State.MyBO, "NoOfParallelProcesses", NoOfParallelProcessesTextBox)
                PopulateBOProperty(State.MyBO, "CommitFrequency", CommitFrequencyTextBox)

            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub


        Protected Sub CreateNew()
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            State.MyBO = New Servers
            PopulateFormFromBOs()
            EnableDisableFields()
            'PopulateAll()
        End Sub

        Protected Sub CreateNewWithCopy()

            PopulateBOsFromForm()

            Dim newObj As New Servers
            newObj.Copy(State.MyBO)

            State.MyBO = newObj
            'Me.State.MyBO.BranchCode = Nothing
            'Me.State.MyBO.BranchName = Nothing

            PopulateFormFromBOs()
            EnableDisableFields()

            'create the backup copy
            State.ScreenSnapShotBO = New Servers
            State.ScreenSnapShotBO.Clone(State.MyBO)
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenDeletePromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = CONFIRM_MESSAGE_OK Then
                If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    State.MyBO.Save()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = CONFIRM_MESSAGE_CANCEL Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ErrControllerMaster.AddErrorAndShow(State.LastErrMsg)
                End Select
            End If
            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenDeletePromptResponse.Value = ""
        End Sub

#End Region

#Region "DIAGNOSTICS"

        Private Sub grdBatch_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdBatch.RowCommand

            Try
                If Not CheckBatchCredentials() Then Exit Sub
                statusResult_LABEL.Text = Servers.TestBatchService(batchUser_TEXT.Text, batchPass_TEXT.Text, batchGroup_TEXT.Text, e.CommandArgument.ToString)

            Catch ex As Exception
                ErrControllerMaster.AddErrorAndShow(ex.StackTrace, False)
                statusResult_LABEL.Text = "Error Calling the Batch Service.  Please check the server log in AIM"
            End Try

        End Sub

        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBatch.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If dvRow IsNot Nothing Then

                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then

                    If dvRow(Servers.SearchDV.COL_BATCH_HOSTNAME) Is DBNull.Value OrElse dvRow(Servers.SearchDV.COL_BATCH_HOSTNAME).ToString.Trim.Length = 0 Then
                        CType(e.Row.Cells(0).FindControl(GRID_SERVERS_TEST_BUTTON), ImageButton).ImageUrl = "../Navigation/images/icons/no_icon2.gif"
                        CType(e.Row.Cells(0).FindControl(GRID_SERVERS_TEST_BUTTON), ImageButton).Enabled = False
                    Else
                        DisplayProgressBarOnClick(CType(e.Row.Cells(0).FindControl(GRID_SERVERS_TEST_BUTTON), WebControl), "Running Test")
                        CType(e.Row.Cells(0).FindControl(GRID_SERVERS_TEST_BUTTON), ImageButton).CommandArgument = dvRow(Servers.SearchDV.COL_BATCH_HOSTNAME).ToString
                    End If
                End If
            End If
        End Sub

        Protected Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles grdBatch.RowDataBound

            Try
                BaseItemBound(source, e)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub btnExecuteCurrent_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles btnExecuteCurrent.Click

            Try
                If Not CheckBatchCredentials() Then Exit Sub
                statusResult_LABEL.Text = Servers.TestCurrentBatchService(batchUser_TEXT.Text, batchPass_TEXT.Text, batchGroup_TEXT.Text)

            Catch ex As Exception
                ErrControllerMaster.AddErrorAndShow(ex.StackTrace, False)
                statusResult_LABEL.Text = "Error Calling the Batch Service.  Please check the server log in AIM"
            End Try
        End Sub

        Private Function CheckBatchCredentials() As Boolean

            Dim ret As Boolean = True

            If batchUser_TEXT.Text.Trim.Length = 0 Then
                ret = False
                ErrControllerMaster.AddErrorAndShow("Username is required.", False)
            End If
            If batchGroup_TEXT.Text.Trim.Length = 0 Then
                ret = False
                ErrControllerMaster.AddErrorAndShow("LDAP Group is required.", False)
            End If

            Return ret
        End Function
#End Region

    End Class


End Namespace