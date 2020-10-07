Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Namespace Tables

    Partial Public Class ClaimStatusLetterForm
        Inherits ElitaPlusPage

#Region "Page State"

#Region "MyState"

        Class MyState
            Public moClaimStatusLetterId As Guid = Guid.Empty
            Public IsClaimStatusLetterNew As Boolean = False

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public bnoRow As Boolean = False

        End Class
#End Region

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub SetStateProperties()
            State.moClaimStatusLetterId = CType(CallingParameters, Guid)
            If State.moClaimStatusLetterId.Equals(Guid.Empty) Then
                State.IsClaimStatusLetterNew = True
                ClearAll()
                SetButtonsState(True)
                ClaimStatus = True
            Else
                State.IsClaimStatusLetterNew = False
                SetButtonsState(False)
            End If
            PopulateAll()
        End Sub

#End Region

#Region "Constants"

        Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
        Public Shared URL As String = "~/Tables/ClaimStatusLetterForm.aspx"
        Public Const PAGETITLE As String = "CLAIM_STATUS_LETTER_DETAILS"
        Public Const PAGETAB As String = "TABLES"
        Public Const DEALER_ID_PROPERTY As String = "DealerId"
        Public Const LETTER_TYPE_PROPERTY As String = "LetterType"
        Public Const NUMBER_OF_DAYS_PROPERTY As String = "NumberOfDays"
        Public Const EMAIL_SUBJECT_PROPERTY As String = "EmailSubject"
        Public Const EMAIL_TEXT_PROPERTY As String = "EmailText"
        Public Const CLAIM_STATUS_BY_GROUP_ID_PROPERTY As String = "ClaimStatusByGroupId"
        Public Const CLAIM_GROUP_OWNER_ID_PROPERTY As String = "GroupOwnerId"
        Public Const NOTIFICATION_TYPE_ID_PROPERTY As String = "NotificationTypeId"
        Public Const NOTIFICATION_TYPE_ID As String = "NotificationTypeId"
        Public Const USE_SERVICE_CENTER_EMAIL_PROPERTY As String = "UseServiceCenterEmail"
        Public Const IS_ACTIVE_PROPERTY As String = "IsActive"
        Public Const EMAIL_TO_PROPERTY As String = "EmailTo"
        Public Const EMAIL_FROM_PROPERTY As String = "EmailFrom"
        Public Const GRID_COL_LIST_ITEM_ID_IDX As Integer = 0
        Public Const GRID_COL_DESCRIPTION_IDX As Integer = 1
        Public Const GRID_COL_CODE_IDX As Integer = 2
        Public Const GRID_COL_COPY_TO_CLIPBOARD_IDX As Integer = 3
        Public Const GRID_COL_CODE_PROPERTY As String = "CODE"
        Public Const GRID_COL_DESCRIPTION_PROPERTY As String = "DESCRIPTION"
        Public Const CLAIM_STATUS_LETTER_VARIABLES As String = "CSLV"
        Private Const EXTOWN As String = "EXTOWN"

#End Region

#Region "Variables"
        Public emailText As String = ""
        Public textEditorCtlStr As String = ""

#End Region

#Region "Attributes"

        Private moClaimStatusLetter As ClaimStatusLetter

#End Region

#Region "Properties"

        Private ReadOnly Property TheClaimStatusLetter() As ClaimStatusLetter
            Get
                If moClaimStatusLetter Is Nothing Then
                    If State.IsClaimStatusLetterNew = True Then
                        ' For creating, inserting
                        moClaimStatusLetter = New ClaimStatusLetter
                        State.moClaimStatusLetterId = moClaimStatusLetter.Id
                    Else
                        ' For updating, deleting
                        moClaimStatusLetter = New ClaimStatusLetter(State.moClaimStatusLetterId)
                    End If
                End If

                Return moClaimStatusLetter
            End Get
        End Property

        Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
            Get
                If multipleDealerDropControl Is Nothing Then
                    multipleDealerDropControl = CType(FindControl("multipleDealerDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDealerDropControl
            End Get
        End Property


        Public Property ClaimStatus() As Boolean
            Get
                Return rbUseClaimStatus.Checked
            End Get
            Set(Value As Boolean)
                If Value = True Then
                    ControlMgr.SetVisibleControl(Me, ExtendedClaimStatusDropdownList, True)
                    ControlMgr.SetVisibleControl(Me, moClaimStatusLabel, True)
                    ControlMgr.SetVisibleControl(Me, cboGroupId, False)
                    ControlMgr.SetVisibleControl(Me, moGroupIdLabel, False)
                    ControlMgr.SetVisibleControl(Me, cboNotificationTypeId, False)
                    ControlMgr.SetVisibleControl(Me, moNotificationTypeLabel, False)
                    ControlMgr.SetVisibleControl(Me, Mandatory1, False)
                Else
                    ControlMgr.SetVisibleControl(Me, ExtendedClaimStatusDropdownList, False)
                    ControlMgr.SetVisibleControl(Me, moClaimStatusLabel, False)
                    ControlMgr.SetVisibleControl(Me, cboGroupId, True)
                    ControlMgr.SetVisibleControl(Me, moGroupIdLabel, True)
                    ControlMgr.SetVisibleControl(Me, cboNotificationTypeId, True)
                    ControlMgr.SetVisibleControl(Me, moNotificationTypeLabel, True)
                    ControlMgr.SetVisibleControl(Me, Mandatory1, True)
                End If

                rbUseClaimStatus.Checked = Value
                rbUseGroup.Checked = (Not Value)
            End Set
        End Property


#End Region

#Region "Handlers"



#Region "Handlers-Init"

        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Try
                ' moRecipientText.Attributes.Add("onchange", "ValidateEmail('" + moRecipientText.ClientID + "');")
                ErrControllerMaster.Clear_Hide()
                ClearLabelsErrSign()
                If Not Page.IsPostBack Then
                    SetStateProperties()
                    AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, _
                                                                        MSG_TYPE_CONFIRM, True)
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    TranslateGridHeader(Grid)
                    TranslateGridControls(Grid)
                End If

                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()

                If Not IsPostBack Then
                    AddLabelDecorations(TheClaimStatusLetter)
                End If

                RenderTextEditor()
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Public Function RenderTextEditor() As String
            textEditorCtlStr = "<script type=""text/javascript"">"
            textEditorCtlStr = textEditorCtlStr & "var sBasePath = ""../Common/Editor/fckeditor/""; "
            textEditorCtlStr = textEditorCtlStr & "var oFCKeditor = new FCKeditor( 'EmailText' ) ; "
            textEditorCtlStr = textEditorCtlStr & "oFCKeditor.BasePath	= sBasePath ; "
            textEditorCtlStr = textEditorCtlStr & "oFCKeditor.Height	= 300 ; "
            textEditorCtlStr = textEditorCtlStr & "oFCKeditor.Value	= '" & emailText.Replace(Environment.NewLine, "").Replace("'", "\'") & "'; "
            textEditorCtlStr = textEditorCtlStr & "oFCKeditor.Create() ; "
            textEditorCtlStr = textEditorCtlStr & "</script>"

            Return textEditorCtlStr
        End Function

#End Region

#Region "Handlers-Buttons"

        Protected Sub btnApply_WRITE_Click(sender As Object, e As EventArgs) Handles btnApply_WRITE.Click
            ApplyChanges()
        End Sub

        Private Sub GoBack()
            Dim retType As New ClaimStatusLetterSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, _
                                                                                State.moClaimStatusLetterId, State.boChanged)
            ReturnToCallingPage(retType)
        End Sub

        Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, _
                                                HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnUndo_WRITE_Click(sender As Object, e As EventArgs) Handles btnUndo_WRITE.Click
            Try
                PopulateAll()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub CreateNew()
            State.moClaimStatusLetterId = Guid.Empty
            State.IsClaimStatusLetterNew = True
            ClearAll()
            SetButtonsState(True)
            PopulateAll()
            TheDealerControl.ChangeEnabledControlProperty(True)

        End Sub

        Protected Sub btnNew_WRITE_Click(sender As Object, e As EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub CreateNewCopy()
            State.moClaimStatusLetterId = Guid.Empty
            State.IsClaimStatusLetterNew = True

            SetButtonsState(True)
            TheDealerControl.ChangeEnabledControlProperty(True)

        End Sub

        Protected Sub btnCopy_WRITE_Click(sender As Object, e As EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnDelete_WRITE_Click(sender As Object, e As EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteClaimStatusLetter() = True Then
                    State.boChanged = True
                    Dim retType As New ClaimStatusLetterSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, _
                                    State.moClaimStatusLetterId)
                    retType.BoChanged = True
                    ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#End Region


#Region "Clear"

        Private Sub ClearTexts()
            moLetterTypeText.Text = Nothing
            moNumberOfDaysText.Text = Nothing
            moEmailSubjectText.Text = Nothing
            moSenderText.Text = Nothing
            moRecipientText.Text = Nothing
            ServiceCenterRecipientDropdown.SelectedIndex = 0
            IsActiveDropdownlList.SelectedIndex = 0
            emailText = ""
            ExtendedClaimStatusDropdownList.SelectedIndex = 0
            cboGroupId.SelectedIndex = 0
            cboNotificationTypeId.SelectedIndex = 0
        End Sub

        Private Sub ClearAll()
            rbUseClaimStatus.Checked = True
            ClearTexts()
            TheDealerControl.ClearMultipleDrop()
        End Sub

#End Region

#Region "Populate"

        Protected Sub populateDealer()
            Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            TheDealerControl.SetControl(True, _
                                        TheDealerControl.MODES.NEW_MODE, _
                                        True, _
                                        oDealerview, _
                                        "* " + TranslationBase.TranslateLabelOrMessage("Dealer"), _
                                        True, True, _
                                        , _
                                        "multipleDropControl_moMultipleColumnDrop", _
                                        "multipleDropControl_moMultipleColumnDropDesc", _
                                        "multipleDropControl_lb_DropDown", _
                                        False, _
                                        0)
            If State.IsClaimStatusLetterNew = True Then
                TheDealerControl.SelectedGuid = Guid.Empty
                TheDealerControl.ChangeEnabledControlProperty(True)
            Else
                TheDealerControl.ChangeEnabledControlProperty(False)
                TheDealerControl.SelectedGuid = TheClaimStatusLetter.DealerId
            End If
        End Sub

        Private Sub PopulateExtendedClaimStatus()
            Try
                Dim dvClaimStatus As DataView = New DataView(ClaimStatusByGroup.LoadListByCompanyGroup(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id).Tables(0))
                ' Me.BindListControlToDataView(Me.ExtendedClaimStatusDropdownList, dvClaimStatus, "description", "claim_status_by_group_id", True) 'ClaimStatusByCompanyGroup
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                listcontext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                Dim StatLKl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ClaimStatusByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                ExtendedClaimStatusDropdownList.Populate(StatLKl, New PopulateOptions() With
                 {
                .AddBlankItem = True
                      })
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateTexts()

            Try
                With TheClaimStatusLetter

                    If State.IsClaimStatusLetterNew = True Then
                        ClaimStatus = True
                    Else
                        ClaimStatus = .UseClaimStatus = "Y"
                        moLetterTypeText.Text = .LetterType
                        moNumberOfDaysText.Text = .NumberOfDays.ToString
                        moEmailSubjectText.Text = .EmailSubject
                        moSenderText.Text = .EmailFrom
                        moRecipientText.Text = .EmailTo
                        emailText = .EmailText
                    End If

                    If ClaimStatus Then
                        SetSelectedItem(ExtendedClaimStatusDropdownList, .ClaimStatusByGroupId)
                        SetSelectedItem(cboGroupId, Guid.Empty)
                        SetSelectedItem(cboNotificationTypeId, Guid.Empty)
                    Else
                        SetSelectedItem(cboGroupId, .GroupOwnerId)
                        SetSelectedItem(cboNotificationTypeId, .NotificationTypeId)
                        SetSelectedItem(ExtendedClaimStatusDropdownList, Guid.Empty)
                    End If
                    SetSelectedItem(IsActiveDropdownlList, .IsActive)
                End With

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateAll()
            Dim yesNoLkL As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
            Dim OWNERdv As ListItem() = CommonConfigManager.Current.ListManager.GetList(EXTOWN, Thread.CurrentPrincipal.GetLanguageCode())
            Dim NotificatioTypes As ListItem() = CommonConfigManager.Current.ListManager.GetList("NOTIFICATION_TYPES", Thread.CurrentPrincipal.GetLanguageCode())

            If State.IsClaimStatusLetterNew = True Then

                ServiceCenterRecipientDropdown.Populate(yesNoLkL, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })

                IsActiveDropdownlList.Populate(yesNoLkL, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })

                cboGroupId.Populate(OWNERdv, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })


                cboNotificationTypeId.Populate(NotificatioTypes, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
                populateDealer()
                PopulateExtendedClaimStatus()
            Else
                ClearAll()
                ServiceCenterRecipientDropdown.Populate(yesNoLkL, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })

                IsActiveDropdownlList.Populate(yesNoLkL, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = False
                                                       })

                cboGroupId.Populate(OWNERdv, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })


                cboNotificationTypeId.Populate(NotificatioTypes, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
                populateDealer()
                PopulateExtendedClaimStatus()
                PopulateTexts()
            End If
        End Sub

        Protected Sub PopulateBOsFromForm()
            With TheClaimStatusLetter
                emailText = Request.Params("EmailText")

                If Not (.EmailText IsNot Nothing AndAlso emailText IsNot Nothing AndAlso emailText.Replace(Environment.NewLine, "") = .EmailText.Replace(Environment.NewLine, "")) Then
                    .EmailText = emailText
                End If

                If ClaimStatus Then
                    .UseClaimStatus = "Y"
                Else
                    .UseClaimStatus = "N"
                End If

                PopulateBOProperty(TheClaimStatusLetter, CLAIM_STATUS_BY_GROUP_ID_PROPERTY, ExtendedClaimStatusDropdownList)
                PopulateBOProperty(TheClaimStatusLetter, CLAIM_GROUP_OWNER_ID_PROPERTY, cboGroupId)
                PopulateBOProperty(TheClaimStatusLetter, NOTIFICATION_TYPE_ID, cboNotificationTypeId)
                PopulateBOProperty(TheClaimStatusLetter, DEALER_ID_PROPERTY, TheDealerControl.SelectedGuid)
                PopulateBOProperty(TheClaimStatusLetter, LETTER_TYPE_PROPERTY, moLetterTypeText)
                PopulateBOProperty(TheClaimStatusLetter, NUMBER_OF_DAYS_PROPERTY, moNumberOfDaysText)
                PopulateBOProperty(TheClaimStatusLetter, EMAIL_SUBJECT_PROPERTY, moEmailSubjectText)

                PopulateBOProperty(TheClaimStatusLetter, USE_SERVICE_CENTER_EMAIL_PROPERTY, ServiceCenterRecipientDropdown)
                PopulateBOProperty(TheClaimStatusLetter, IS_ACTIVE_PROPERTY, IsActiveDropdownlList)
                PopulateBOProperty(TheClaimStatusLetter, EMAIL_TO_PROPERTY, moRecipientText)
                PopulateBOProperty(TheClaimStatusLetter, EMAIL_FROM_PROPERTY, moSenderText)
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub

        Protected Sub PopulateGrid()
            Dim dvDropdown As DataView = LookupListNew.DropdownLookupList(CLAIM_STATUS_LETTER_VARIABLES, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Grid.AutoGenerateColumns = False
            Grid.DataSource = dvDropdown
            Grid.DataBind()
            If dvDropdown.Count > 0 Then
                State.bnoRow = True
            Else
                State.bnoRow = False
            End If
        End Sub

        Public Shared Sub SetLabelColor(lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub

        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If dvRow IsNot Nothing Then 'And Not Me.State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Row.Cells(GRID_COL_DESCRIPTION_IDX).Text = dvRow(GRID_COL_DESCRIPTION_PROPERTY).ToString
                    e.Row.Cells(GRID_COL_CODE_IDX).Text = "{" & dvRow(GRID_COL_CODE_PROPERTY).ToString & "}"
                    e.Row.Cells(GRID_COL_COPY_TO_CLIPBOARD_IDX).Attributes.Add("onclick", "return CopyToClipboard('{" & dvRow(GRID_COL_CODE_PROPERTY).ToString & "}');")
                End If
            End If
        End Sub
#End Region

#Region "Gui-Validation"

        Private Sub SetButtonsState(bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            TheDealerControl.ChangeEnabledControlProperty(bIsNew)
        End Sub
#End Region

#Region "Business Part"

        Private Function IsDirtyBO() As Boolean
            Dim bIsDirty As Boolean = True
            Try
                With TheClaimStatusLetter
                    PopulateBOsFromForm()
                    bIsDirty = .IsDirty
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean
            Dim isOK As Boolean = True
            Try

                PopulateBOsFromForm()
                If TheClaimStatusLetter.IsDirty() Then
                    TheClaimStatusLetter.Save()
                    State.boChanged = True
                    If State.IsClaimStatusLetterNew = True Then
                        State.IsClaimStatusLetterNew = False
                    End If
                    PopulateAll()
                    SetButtonsState(State.IsClaimStatusLetterNew)
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                Else
                    DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
                isOK = False
            End Try
            Return isOK
        End Function

        Private Function DeleteClaimStatusLetter() As Boolean
            Dim bIsOk As Boolean = True

            Try
                With TheClaimStatusLetter
                    PopulateBOsFromForm()
                    .Delete()
                    .Save()
                End With
            Catch ex As Exception
                TheClaimStatusLetter.RejectChanges()
                HandleErrors(ex, ErrControllerMaster)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

#End Region

#Region "Handlers-Labels"

        Private Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(TheClaimStatusLetter, DEALER_ID_PROPERTY, TheDealerControl.CaptionLabel)
            BindBOPropertyToLabel(TheClaimStatusLetter, LETTER_TYPE_PROPERTY, moLetterTypeLabel)
            BindBOPropertyToLabel(TheClaimStatusLetter, NUMBER_OF_DAYS_PROPERTY, moNumberOfDaysLabel)
            BindBOPropertyToLabel(TheClaimStatusLetter, EMAIL_SUBJECT_PROPERTY, moEmailSubjectLabel)
            BindBOPropertyToLabel(TheClaimStatusLetter, EMAIL_TEXT_PROPERTY, moEmailTextLabel)
            BindBOPropertyToLabel(TheClaimStatusLetter, EMAIL_FROM_PROPERTY, moSenderLabel)
            BindBOPropertyToLabel(TheClaimStatusLetter, EMAIL_TO_PROPERTY, moRecipientLabel)
            BindBOPropertyToLabel(TheClaimStatusLetter, USE_SERVICE_CENTER_EMAIL_PROPERTY, moServiceCenterRecipientLabel)
            BindBOPropertyToLabel(TheClaimStatusLetter, IS_ACTIVE_PROPERTY, moActiveLabel)
            BindBOPropertyToLabel(TheClaimStatusLetter, NOTIFICATION_TYPE_ID_PROPERTY, moNotificationTypeLabel)
            BindBOPropertyToLabel(TheClaimStatusLetter, CLAIM_STATUS_BY_GROUP_ID_PROPERTY, moClaimStatusLabel)
            BindBOPropertyToLabel(TheClaimStatusLetter, CLAIM_GROUP_OWNER_ID_PROPERTY, moGroupIdLabel)

        End Sub

        Private Sub ClearLabelsErrSign()
            ClearLabelErrSign(TheDealerControl.CaptionLabel)
            ClearLabelErrSign(moLetterTypeLabel)
            ClearLabelErrSign(moNumberOfDaysLabel)
            ClearLabelErrSign(moEmailSubjectLabel)
            ClearLabelErrSign(moEmailTextLabel)
            ClearLabelErrSign(moClaimStatusLabel)
            ClearLabelErrSign(moSenderLabel)
            ClearLabelErrSign(moRecipientLabel)
            ClearLabelErrSign(moActiveLabel)
            ClearLabelErrSign(moNotificationTypeLabel)
            ClearLabelErrSign(moGroupIdLabel)

        End Sub
#End Region

#Region "State-Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            GoBack()
                        End If

                    Case MSG_VALUE_NO
                        GoBack()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNewCopy()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                Select Case confResponse
                    Case MSG_VALUE_YES
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            CreateNewCopy()
                        End If
                    Case MSG_VALUE_NO
                        CreateNewCopy()
                End Select
            End If

        End Sub

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNewCopy()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()
                End Select

                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region
#Region "Handlers-rb"
        Private Sub rbUseClaimStatus_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbUseClaimStatus.CheckedChanged

            ClaimStatus = rbUseClaimStatus.Checked
            SetSelectedItem(ExtendedClaimStatusDropdownList, TheClaimStatusLetter.ClaimStatusByGroupId)
            SetSelectedItem(cboGroupId, NOTHING_SELECTED)
            SetSelectedItem(cboNotificationTypeId, NOTHING_SELECTED)

        End Sub

        Private Sub rbUseGroup_CheckedChanged(sender As Object, e As System.EventArgs) Handles rbUseGroup.CheckedChanged

            ClaimStatus = rbUseClaimStatus.Checked
            SetSelectedItem(ExtendedClaimStatusDropdownList, NOTHING_SELECTED)
            SetSelectedItem(cboGroupId, TheClaimStatusLetter.GroupOwnerId)
            SetSelectedItem(cboNotificationTypeId, TheClaimStatusLetter.NotificationTypeId)
        End Sub
#End Region
    End Class

End Namespace