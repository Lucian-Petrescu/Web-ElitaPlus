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

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Me.State.MyBO = New Servers(CType(Me.CallingParameters, Guid))
                Else
                    Me.State.IsNew = True
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As Servers
            Public HasDataChanged As Boolean

            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Servers, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Handlers"

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrControllerMaster.Clear_Hide()
            Try
                If Not Me.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    ' Me.MenuEnabled = False
                    Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New Servers
                    End If

                    If Me.State.IsNew = True Then
                        CreateNew()
                    End If
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()
                End If
                BindBoPropertiesToLabels()
                CheckIfComingFromSaveConfirm()
                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(Me.State.MyBO)
                End If
                Me.DisplayProgressBarOnClick(CType(Me.btnExecuteCurrent, WebControl), "Running Test")

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub

#End Region


#Region "Handlers-Buttons"

        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click, btnBack2.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Dim iDirtyCols As Integer
                    iDirtyCols = Me.State.MyBO.DirtyColumns.Count
                    If iDirtyCols > SERVER_DIRTY_COLUMNS_COUNT Then
                        Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenDeletePromptResponse)
                        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                    Else
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    End If
                Else
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
                Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenDeletePromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.ErrControllerMaster.Text
            End Try
        End Sub
        Private Sub btnCopy_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    Me.CreateNewWithCopy()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
        Private Sub btnDelete_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Dim addressDeleted As Boolean
            Try
                'Delete the Address
                Me.State.MyBO.DeleteAndSave()
                Me.State.HasDataChanged = True
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try

        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If (Me.State.MyBO.IsDirty) Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenDeletePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    Me.CreateNew()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnApply_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = True
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()
                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    'PopulateAll()
                Else
                    Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not Me.State.MyBO.IsNew Then
                    'Reload from the DB
                    Me.State.MyBO = New Servers(Me.State.MyBO.Id)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    CreateNew()
                End If
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
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

            If Me.State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
            End If

        End Sub


        Protected Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.DescriptionLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "HubRegion", Me.HubRegionLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Environment", Me.EnvironmentLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "MachinePrefix", Me.MachinePrefixLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "FtpHostname", Me.FtpHostNameLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "FtpHostPath", Me.FtpHostPathLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "FtpTriggerExtension", Me.FtpTriggerExtensionLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "FtpSplitPath", Me.FtpSplitPathLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CrystalSdk", Me.CrystalSdkLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CrystalViewer", Me.CrystalViewerLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "FelitaFtpHostname", Me.FelitaFtpHostNameLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "LdapIp", Me.LdapIpLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "SmartStreamHostName", Me.SmartStreamHostNameLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ServiceOrderImageHost", Me.ServiceOrderImageHostNameLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "EuropeanPrivacy", Me.EuropeanPrivacyLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DatabaseName", Me.DatabaseNameLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "BatchHostname", Me.BatchHostnameLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "AcctBalanceHostname", Me.AcctBalHostnameLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "SmartStreamAPUpload", Me.SmartStreamAPUploadLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "SmartStreamGLStatus", Me.SmartStreamGLStatusLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "SmartStreamGLUpload", Me.SmartStreamGLUploadLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DBUniqueName", Me.UniqueDBLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "NoOfParallelProcesses", Me.NoOfParallelProcessesLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "CommitFrequency", Me.CommitFrequencyLabel)


            Me.ClearGridHeadersAndLabelsErrSign()
        End Sub

        Protected Sub PopulateFormFromBOs()
            Dim yesNoList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="YESNO", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Dim yesNoId As Guid
            Me.EuropeanPrivacyDrop.Populate(yesNoList, New PopulateOptions() With
                {
                    .AddBlankItem = False
                })

            'Dim langId As Guid = Authentication.LangId
            'Dim yesNoLkL As DataView = LookupListNew.GetYesNoLookupList(langId, False)
            'Me.BindListControlToDataView(Me.EuropeanPrivacyDrop, yesNoLkL, , , False)

            With Me.State.MyBO
                Me.PopulateControlFromBOProperty(Me.DescriptionTextBox, .Description)
                Me.PopulateControlFromBOProperty(Me.HubRegionTextBox, .HubRegion)
                Me.PopulateControlFromBOProperty(Me.MachinePrefixTextBox, .MachinePrefix)
                Me.PopulateControlFromBOProperty(Me.EnvironmentextBox, .Environment)
                Me.PopulateControlFromBOProperty(Me.FtpHostNameTextBox, .FtpHostname)
                Me.PopulateControlFromBOProperty(Me.FelitaFtpHostNameTextBox, .FelitaFtpHostname)
                Me.PopulateControlFromBOProperty(Me.LdapIpTextBox, .LdapIp)
                Me.PopulateControlFromBOProperty(Me.FtpHostPathTextBox, .FtpHostPath)
                Me.PopulateControlFromBOProperty(Me.FtpTriggerExtensionTextBox, .FtpTriggerExtension)
                Me.PopulateControlFromBOProperty(Me.FtpSplitPathTextBox, .FtpSplitPath)
                Me.PopulateControlFromBOProperty(Me.SmartstreamHostnameTextBox, .SmartStreamHostName)
                Me.PopulateControlFromBOProperty(Me.ServiceorderImageHostnameTextBox, .ServiceOrderImageHost)
                Me.PopulateControlFromBOProperty(Me.BatchHostnameTextBox, .BatchHostname)
                Me.PopulateControlFromBOProperty(Me.AcctBalHostnameTextBox, .AcctBalanceHostname)

                If .PrivacyLevelXCD Is Nothing Then
                    .PrivacyLevelXCD = Codes.YESNO_N
                End If

                'yesNoId = LookupListNew.GetIdFromCode(yesNoLkL, .PrivacyLevelXCD)
                yesNoId = (From yn In yesNoList
                           Where yn.Code = .PrivacyLevelXCD
                           Select yn.ListItemId).FirstOrDefault()

                Me.SetSelectedItem(Me.EuropeanPrivacyDrop, yesNoId)
                Me.PopulateControlFromBOProperty(Me.DatabaNameTextBox, .DatabaseName)

                Me.PopulateControlFromBOProperty(Me.SmartStreamAPUploadTextBox, .SmartStreamAPUpload)
                Me.PopulateControlFromBOProperty(Me.SmartStreamGLStatusTextBox, .SmartStreamGLStatus)
                Me.PopulateControlFromBOProperty(Me.SmartStreamGLUploadTextBox, .SmartStreamGLUpload)

                Me.PopulateControlFromBOProperty(Me.UniqueDBTextBox, .DBUniqueName)
                Me.PopulateControlFromBOProperty(Me.NoOfParallelProcessesTextBox, .NoOfParallelProcesses)
                Me.PopulateControlFromBOProperty(Me.CommitFrequencyTextBox, .CommitFrequency)

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

            With Me.State.MyBO
                Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.DescriptionTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "HubRegion", Me.HubRegionTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "MachinePrefix", Me.MachinePrefixTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "Environment", Me.EnvironmentextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "FtpHostname", Me.FtpHostNameTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "CrystalSdk", Me.CrystalSdkTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "CrystalViewer", Me.CrystalViewerTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "FelitaFtpHostname", Me.FelitaFtpHostNameTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "LdapIp", Me.LdapIpTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "FtpHostPath", Me.FtpHostPathTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "FtpTriggerExtension", Me.FtpTriggerExtensionTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "FtpSplitPath", Me.FtpSplitPathTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "SmartStreamHostName", Me.SmartstreamHostnameTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "ServiceOrderImageHost", Me.ServiceorderImageHostnameTextBox)
                yesNoId = Me.GetSelectedItem(Me.EuropeanPrivacyDrop)
                yesNoCode = LookupListNew.GetCodeFromId(yesNoLkL, yesNoId)
                Me.PopulateBOProperty(Me.State.MyBO, "EuropeanPrivacy", yesNoCode)
                Me.PopulateBOProperty(Me.State.MyBO, "DatabaseName", Me.DatabaNameTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "BatchHostname", Me.BatchHostnameTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "AcctBalanceHostname", Me.AcctBalHostnameTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "SmartStreamAPUpload", Me.SmartStreamAPUploadTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "SmartStreamGLStatus", Me.SmartStreamGLStatusTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "SmartStreamGLUpload", Me.SmartStreamGLUploadTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "DBUniqueName", Me.UniqueDBTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "NoOfParallelProcesses", Me.NoOfParallelProcessesTextBox)
                Me.PopulateBOProperty(Me.State.MyBO, "CommitFrequency", Me.CommitFrequencyTextBox)

            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub


        Protected Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy
            Me.State.MyBO = New Servers
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
            'PopulateAll()
        End Sub

        Protected Sub CreateNewWithCopy()

            Me.PopulateBOsFromForm()

            Dim newObj As New Servers
            newObj.Copy(Me.State.MyBO)

            Me.State.MyBO = newObj
            'Me.State.MyBO.BranchCode = Nothing
            'Me.State.MyBO.BranchName = Nothing

            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()

            'create the backup copy
            Me.State.ScreenSnapShotBO = New Servers
            Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenDeletePromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_OK Then
                If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    Me.State.MyBO.Save()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_CANCEL Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ErrControllerMaster.AddErrorAndShow(Me.State.LastErrMsg)
                End Select
            End If
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenDeletePromptResponse.Value = ""
        End Sub

#End Region

#Region "DIAGNOSTICS"

        Private Sub grdBatch_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdBatch.RowCommand

            Try
                If Not CheckBatchCredentials() Then Exit Sub
                Me.statusResult_LABEL.Text = Servers.TestBatchService(Me.batchUser_TEXT.Text, Me.batchPass_TEXT.Text, Me.batchGroup_TEXT.Text, e.CommandArgument.ToString)

            Catch ex As Exception
                ErrControllerMaster.AddErrorAndShow(ex.StackTrace, False)
                Me.statusResult_LABEL.Text = "Error Calling the Batch Service.  Please check the server log in AIM"
            End Try

        End Sub

        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdBatch.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If Not dvRow Is Nothing Then

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                    If dvRow(Servers.SearchDV.COL_BATCH_HOSTNAME) Is DBNull.Value OrElse dvRow(Servers.SearchDV.COL_BATCH_HOSTNAME).ToString.Trim.Length = 0 Then
                        CType(e.Row.Cells(0).FindControl(Me.GRID_SERVERS_TEST_BUTTON), ImageButton).ImageUrl = "../Navigation/images/icons/no_icon2.gif"
                        CType(e.Row.Cells(0).FindControl(Me.GRID_SERVERS_TEST_BUTTON), ImageButton).Enabled = False
                    Else
                        Me.DisplayProgressBarOnClick(CType(e.Row.Cells(0).FindControl(Me.GRID_SERVERS_TEST_BUTTON), WebControl), "Running Test")
                        CType(e.Row.Cells(0).FindControl(Me.GRID_SERVERS_TEST_BUTTON), ImageButton).CommandArgument = dvRow(Servers.SearchDV.COL_BATCH_HOSTNAME).ToString
                    End If
                End If
            End If
        End Sub

        Protected Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles grdBatch.RowDataBound

            Try
                BaseItemBound(source, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub btnExecuteCurrent_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnExecuteCurrent.Click

            Try
                If Not CheckBatchCredentials() Then Exit Sub
                Me.statusResult_LABEL.Text = Servers.TestCurrentBatchService(Me.batchUser_TEXT.Text, Me.batchPass_TEXT.Text, Me.batchGroup_TEXT.Text)

            Catch ex As Exception
                ErrControllerMaster.AddErrorAndShow(ex.StackTrace, False)
                Me.statusResult_LABEL.Text = "Error Calling the Batch Service.  Please check the server log in AIM"
            End Try
        End Sub

        Private Function CheckBatchCredentials() As Boolean

            Dim ret As Boolean = True

            If batchUser_TEXT.Text.Trim.Length = 0 Then
                ret = False
                Me.ErrControllerMaster.AddErrorAndShow("Username is required.", False)
            End If
            If batchGroup_TEXT.Text.Trim.Length = 0 Then
                ret = False
                Me.ErrControllerMaster.AddErrorAndShow("LDAP Group is required.", False)
            End If

            Return ret
        End Function
#End Region

    End Class


End Namespace