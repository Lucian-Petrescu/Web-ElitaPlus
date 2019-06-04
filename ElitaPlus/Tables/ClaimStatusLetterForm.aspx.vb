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
            Me.State.moClaimStatusLetterId = CType(Me.CallingParameters, Guid)
            If Me.State.moClaimStatusLetterId.Equals(Guid.Empty) Then
                Me.State.IsClaimStatusLetterNew = True
                ClearAll()
                SetButtonsState(True)
                ClaimStatus = True
            Else
                Me.State.IsClaimStatusLetterNew = False
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
                    If Me.State.IsClaimStatusLetterNew = True Then
                        ' For creating, inserting
                        moClaimStatusLetter = New ClaimStatusLetter
                        Me.State.moClaimStatusLetterId = moClaimStatusLetter.Id
                    Else
                        ' For updating, deleting
                        moClaimStatusLetter = New ClaimStatusLetter(Me.State.moClaimStatusLetterId)
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
            Set(ByVal Value As Boolean)
                If Value = True Then
                    ControlMgr.SetVisibleControl(Me, Me.ExtendedClaimStatusDropdownList, True)
                    ControlMgr.SetVisibleControl(Me, Me.moClaimStatusLabel, True)
                    ControlMgr.SetVisibleControl(Me, Me.cboGroupId, False)
                    ControlMgr.SetVisibleControl(Me, Me.moGroupIdLabel, False)
                    ControlMgr.SetVisibleControl(Me, Me.cboNotificationTypeId, False)
                    ControlMgr.SetVisibleControl(Me, Me.moNotificationTypeLabel, False)
                    ControlMgr.SetVisibleControl(Me, Me.Mandatory1, False)
                Else
                    ControlMgr.SetVisibleControl(Me, Me.ExtendedClaimStatusDropdownList, False)
                    ControlMgr.SetVisibleControl(Me, Me.moClaimStatusLabel, False)
                    ControlMgr.SetVisibleControl(Me, Me.cboGroupId, True)
                    ControlMgr.SetVisibleControl(Me, Me.moGroupIdLabel, True)
                    ControlMgr.SetVisibleControl(Me, Me.cboNotificationTypeId, True)
                    ControlMgr.SetVisibleControl(Me, Me.moNotificationTypeLabel, True)
                    ControlMgr.SetVisibleControl(Me, Me.Mandatory1, True)
                End If

                rbUseClaimStatus.Checked = Value
                rbUseGroup.Checked = (Not Value)
            End Set
        End Property


#End Region

#Region "Handlers"



#Region "Handlers-Init"

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Try
                ' moRecipientText.Attributes.Add("onchange", "ValidateEmail('" + moRecipientText.ClientID + "');")
                Me.ErrControllerMaster.Clear_Hide()
                ClearLabelsErrSign()
                If Not Page.IsPostBack Then
                    Me.SetStateProperties()
                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, _
                                                                        Me.MSG_TYPE_CONFIRM, True)
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    Me.TranslateGridHeader(Grid)
                    Me.TranslateGridControls(Grid)
                End If

                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()

                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(Me.TheClaimStatusLetter)
                End If

                Me.RenderTextEditor()
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
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

        Protected Sub btnApply_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnApply_WRITE.Click
            ApplyChanges()
        End Sub

        Private Sub GoBack()
            Dim retType As New ClaimStatusLetterSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, _
                                                                                Me.State.moClaimStatusLetterId, Me.State.boChanged)
            Me.ReturnToCallingPage(retType)
        End Sub

        Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, _
                                                Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnUndo_WRITE.Click
            Try
                PopulateAll()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub CreateNew()
            Me.State.moClaimStatusLetterId = Guid.Empty
            Me.State.IsClaimStatusLetterNew = True
            ClearAll()
            Me.SetButtonsState(True)
            Me.PopulateAll()
            TheDealerControl.ChangeEnabledControlProperty(True)

        End Sub

        Protected Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub CreateNewCopy()
            Me.State.moClaimStatusLetterId = Guid.Empty
            Me.State.IsClaimStatusLetterNew = True

            Me.SetButtonsState(True)
            TheDealerControl.ChangeEnabledControlProperty(True)

        End Sub

        Protected Sub btnCopy_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnDelete_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteClaimStatusLetter() = True Then
                    Me.State.boChanged = True
                    Dim retType As New ClaimStatusLetterSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, _
                                    Me.State.moClaimStatusLetterId)
                    retType.BoChanged = True
                    Me.ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
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
            Me.emailText = ""
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
            If Me.State.IsClaimStatusLetterNew = True Then
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
                Me.ExtendedClaimStatusDropdownList.Populate(StatLKl, New PopulateOptions() With
                 {
                .AddBlankItem = True
                      })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateTexts()

            Try
                With TheClaimStatusLetter

                    If Me.State.IsClaimStatusLetterNew = True Then
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
                        Me.SetSelectedItem(Me.ExtendedClaimStatusDropdownList, .ClaimStatusByGroupId)
                        Me.SetSelectedItem(Me.cboGroupId, Guid.Empty)
                        Me.SetSelectedItem(Me.cboNotificationTypeId, Guid.Empty)
                    Else
                        Me.SetSelectedItem(Me.cboGroupId, .GroupOwnerId)
                        Me.SetSelectedItem(Me.cboNotificationTypeId, .NotificationTypeId)
                        Me.SetSelectedItem(Me.ExtendedClaimStatusDropdownList, Guid.Empty)
                    End If
                    Me.SetSelectedItem(Me.IsActiveDropdownlList, .IsActive)
                End With

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateAll()
            Dim yesNoLkL As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
            Dim OWNERdv As ListItem() = CommonConfigManager.Current.ListManager.GetList(EXTOWN, Thread.CurrentPrincipal.GetLanguageCode())
            Dim NotificatioTypes As ListItem() = CommonConfigManager.Current.ListManager.GetList("NOTIFICATION_TYPES", Thread.CurrentPrincipal.GetLanguageCode())

            If Me.State.IsClaimStatusLetterNew = True Then

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
            With Me.TheClaimStatusLetter
                emailText = Request.Params("EmailText")

                If Not (Not .EmailText Is Nothing AndAlso Not emailText Is Nothing AndAlso emailText.Replace(Environment.NewLine, "") = .EmailText.Replace(Environment.NewLine, "")) Then
                    .EmailText = emailText
                End If

                If ClaimStatus Then
                    .UseClaimStatus = "Y"
                Else
                    .UseClaimStatus = "N"
                End If

                Me.PopulateBOProperty(TheClaimStatusLetter, CLAIM_STATUS_BY_GROUP_ID_PROPERTY, Me.ExtendedClaimStatusDropdownList)
                Me.PopulateBOProperty(TheClaimStatusLetter, CLAIM_GROUP_OWNER_ID_PROPERTY, Me.cboGroupId)
                Me.PopulateBOProperty(TheClaimStatusLetter, NOTIFICATION_TYPE_ID, Me.cboNotificationTypeId)
                Me.PopulateBOProperty(TheClaimStatusLetter, DEALER_ID_PROPERTY, Me.TheDealerControl.SelectedGuid)
                Me.PopulateBOProperty(TheClaimStatusLetter, LETTER_TYPE_PROPERTY, Me.moLetterTypeText)
                Me.PopulateBOProperty(TheClaimStatusLetter, NUMBER_OF_DAYS_PROPERTY, Me.moNumberOfDaysText)
                Me.PopulateBOProperty(TheClaimStatusLetter, EMAIL_SUBJECT_PROPERTY, Me.moEmailSubjectText)

                Me.PopulateBOProperty(TheClaimStatusLetter, USE_SERVICE_CENTER_EMAIL_PROPERTY, Me.ServiceCenterRecipientDropdown)
                Me.PopulateBOProperty(TheClaimStatusLetter, IS_ACTIVE_PROPERTY, Me.IsActiveDropdownlList)
                Me.PopulateBOProperty(TheClaimStatusLetter, EMAIL_TO_PROPERTY, Me.moRecipientText)
                Me.PopulateBOProperty(TheClaimStatusLetter, EMAIL_FROM_PROPERTY, Me.moSenderText)
            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub

        Protected Sub PopulateGrid()
            Dim dvDropdown As DataView = LookupListNew.DropdownLookupList(CLAIM_STATUS_LETTER_VARIABLES, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Me.Grid.AutoGenerateColumns = False
            Me.Grid.DataSource = dvDropdown
            Me.Grid.DataBind()
            If dvDropdown.Count > 0 Then
                Me.State.bnoRow = True
            Else
                Me.State.bnoRow = False
            End If
        End Sub

        Public Shared Sub SetLabelColor(ByVal lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub

        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            If Not dvRow Is Nothing Then 'And Not Me.State.bnoRow Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Row.Cells(Me.GRID_COL_DESCRIPTION_IDX).Text = dvRow(GRID_COL_DESCRIPTION_PROPERTY).ToString
                    e.Row.Cells(Me.GRID_COL_CODE_IDX).Text = "{" & dvRow(GRID_COL_CODE_PROPERTY).ToString & "}"
                    e.Row.Cells(Me.GRID_COL_COPY_TO_CLIPBOARD_IDX).Attributes.Add("onclick", "return CopyToClipboard('{" & dvRow(GRID_COL_CODE_PROPERTY).ToString & "}');")
                End If
            End If
        End Sub
#End Region

#Region "Gui-Validation"

        Private Sub SetButtonsState(ByVal bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, Me.btnDelete_WRITE, Not bIsNew)
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
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean
            Dim isOK As Boolean = True
            Try

                Me.PopulateBOsFromForm()
                If TheClaimStatusLetter.IsDirty() Then
                    Me.TheClaimStatusLetter.Save()
                    Me.State.boChanged = True
                    If Me.State.IsClaimStatusLetterNew = True Then
                        Me.State.IsClaimStatusLetterNew = False
                    End If
                    PopulateAll()
                    Me.SetButtonsState(Me.State.IsClaimStatusLetterNew)
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Else
                    Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
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
                Me.HandleErrors(ex, Me.ErrControllerMaster)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

#End Region

#Region "Handlers-Labels"

        Private Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(TheClaimStatusLetter, DEALER_ID_PROPERTY, Me.TheDealerControl.CaptionLabel)
            Me.BindBOPropertyToLabel(TheClaimStatusLetter, LETTER_TYPE_PROPERTY, Me.moLetterTypeLabel)
            Me.BindBOPropertyToLabel(TheClaimStatusLetter, NUMBER_OF_DAYS_PROPERTY, Me.moNumberOfDaysLabel)
            Me.BindBOPropertyToLabel(TheClaimStatusLetter, EMAIL_SUBJECT_PROPERTY, Me.moEmailSubjectLabel)
            Me.BindBOPropertyToLabel(TheClaimStatusLetter, EMAIL_TEXT_PROPERTY, Me.moEmailTextLabel)
            Me.BindBOPropertyToLabel(TheClaimStatusLetter, EMAIL_FROM_PROPERTY, Me.moSenderLabel)
            Me.BindBOPropertyToLabel(TheClaimStatusLetter, EMAIL_TO_PROPERTY, Me.moRecipientLabel)
            Me.BindBOPropertyToLabel(TheClaimStatusLetter, USE_SERVICE_CENTER_EMAIL_PROPERTY, Me.moServiceCenterRecipientLabel)
            Me.BindBOPropertyToLabel(TheClaimStatusLetter, IS_ACTIVE_PROPERTY, Me.moActiveLabel)
            Me.BindBOPropertyToLabel(TheClaimStatusLetter, NOTIFICATION_TYPE_ID_PROPERTY, Me.moNotificationTypeLabel)
            Me.BindBOPropertyToLabel(TheClaimStatusLetter, CLAIM_STATUS_BY_GROUP_ID_PROPERTY, Me.moClaimStatusLabel)
            Me.BindBOPropertyToLabel(TheClaimStatusLetter, CLAIM_GROUP_OWNER_ID_PROPERTY, Me.moGroupIdLabel)

        End Sub

        Private Sub ClearLabelsErrSign()
            Me.ClearLabelErrSign(Me.TheDealerControl.CaptionLabel)
            Me.ClearLabelErrSign(Me.moLetterTypeLabel)
            Me.ClearLabelErrSign(Me.moNumberOfDaysLabel)
            Me.ClearLabelErrSign(Me.moEmailSubjectLabel)
            Me.ClearLabelErrSign(Me.moEmailTextLabel)
            Me.ClearLabelErrSign(Me.moClaimStatusLabel)
            Me.ClearLabelErrSign(Me.moSenderLabel)
            Me.ClearLabelErrSign(Me.moRecipientLabel)
            Me.ClearLabelErrSign(Me.moActiveLabel)
            Me.ClearLabelErrSign(Me.moNotificationTypeLabel)
            Me.ClearLabelErrSign(Me.moGroupIdLabel)

        End Sub
#End Region

#Region "State-Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        If ApplyChanges() = True Then
                            Me.State.boChanged = True
                            GoBack()
                        End If

                    Case Me.MSG_VALUE_NO
                        GoBack()
                End Select
            End If

        End Sub

        Protected Sub ComingFromNewCopy()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        If ApplyChanges() = True Then
                            Me.State.boChanged = True
                            CreateNewCopy()
                        End If
                    Case Me.MSG_VALUE_NO
                        CreateNewCopy()
                End Select
            End If

        End Sub

        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNewCopy()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()
                End Select

                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region
#Region "Handlers-rb"
        Private Sub rbUseClaimStatus_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbUseClaimStatus.CheckedChanged

            ClaimStatus = rbUseClaimStatus.Checked
            Me.SetSelectedItem(Me.ExtendedClaimStatusDropdownList, TheClaimStatusLetter.ClaimStatusByGroupId)
            Me.SetSelectedItem(Me.cboGroupId, NOTHING_SELECTED)
            Me.SetSelectedItem(Me.cboNotificationTypeId, NOTHING_SELECTED)

        End Sub

        Private Sub rbUseGroup_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rbUseGroup.CheckedChanged

            ClaimStatus = rbUseClaimStatus.Checked
            Me.SetSelectedItem(Me.ExtendedClaimStatusDropdownList, NOTHING_SELECTED)
            Me.SetSelectedItem(Me.cboGroupId, TheClaimStatusLetter.GroupOwnerId)
            Me.SetSelectedItem(Me.cboNotificationTypeId, TheClaimStatusLetter.NotificationTypeId)
        End Sub
#End Region
    End Class

End Namespace