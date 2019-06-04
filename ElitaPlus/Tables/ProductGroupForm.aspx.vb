Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Partial Class ProductGroupForm
    Inherits ElitaPlusSearchPage

    Protected WithEvents LabelRiskType As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents moCountryColonLabel_NO_TRANSLATE As System.Web.UI.WebControls.Label
    Protected WithEvents SearchCodeLabel As System.Web.UI.WebControls.Label
    Protected WithEvents TextBoxSearchCode As System.Web.UI.WebControls.TextBox
    Protected WithEvents LabelSearchDescription As System.Web.UI.WebControls.Label
    Protected WithEvents TextBoxSearchDescription As System.Web.UI.WebControls.TextBox
    Protected WithEvents btnClearSearch As System.Web.UI.WebControls.Button
    Protected WithEvents btnSearch As System.Web.UI.WebControls.Button
    Protected WithEvents lblPageSize As System.Web.UI.WebControls.Label
    Protected WithEvents cboPageSize As System.Web.UI.WebControls.DropDownList
    Protected WithEvents lblRecordCount As System.Web.UI.WebControls.Label
    Protected WithEvents Grid As System.Web.UI.WebControls.DataGrid
    Protected WithEvents btnAdd_WRITE As System.Web.UI.WebControls.Button
    Protected WithEvents trPageSize As System.Web.UI.HtmlControls.HtmlTableRow


    Protected WithEvents moProductCodeMultipleDrop As MultipleColumnDDLabelControl


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const URL As String = "ProductGroupForm.aspx"
    Private Const LABEL_DEALER As String = "DEALER_NAME"
    Private Const AVAILABLE_PRODUCTCODES As String = "AVAILABLE_PRODUCTCODES"
    Private Const SELECTED_PRODUCTCODES As String = "SELECTED_PRODUCTCODES"

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ProductGroup
        Public HasDataChanged As Boolean
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As ProductGroup, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"


    Class MyState
        Public MyBO As ProductGroup
        Public ScreenSnapShotBO As ProductGroup
        Public SelectedSCId As Guid = Guid.Empty
        Public SortExpressionDetailGrid As String = ServiceGroup.RiskTypeManufacturerDataView.RISK_TYPE_COL_NAME
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public IsNew As Boolean = False
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New ProductGroup(CType(Me.CallingParameters, Guid))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            Me.ErrorCtrl.Clear_Hide()
            If Not Me.IsPostBack Then
                'Date Calendars
                Me.MenuEnabled = False
                Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)

                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New ProductGroup
                    Me.State.IsNew = True
                End If
                PopulateDealer()
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
        Me.ShowMissingTranslations(Me.ErrorCtrl)
    End Sub
#End Region
#Region "Handlers-DropDown"
    Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
         Handles multipleDealerDropControl.SelectedDropChanged
        Try
            'ClearForDealer()
            'TheCoverage.DealerId = TheDealerControl.SelectedGuid
            'PopulateDealer()
            If TheDealerControl.SelectedIndex > Me.NO_ITEM_SELECTED_INDEX Then
                PopulateDetailControls()
            Else
                ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedProductCodes, False)
            End If
        Catch ex As Exception
            HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region
#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        If UserControlAvailableSelectedProductCodes.AvailableList.Count = 0 Then
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        Else
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
        End If
        'Now disable depebding on the object state
        If Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.LabelDescription)
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Private Sub PopulateDealer()
        Dim oCompanyList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        Try
            Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyList, True)
            TheDealerControl.SetControl(True, TheDealerControl.MODES.NEW_MODE, True, dv, "*" + TranslationBase.TranslateLabelOrMessage(LABEL_DEALER), True, True)
            If Me.State.IsNew = True Then
                TheDealerControl.SelectedGuid = Guid.Empty
                TheDealerControl.ChangeEnabledControlProperty(True)
            Else
                TheDealerControl.ChangeEnabledControlProperty(False)
                TheDealerControl.SelectedGuid = Me.State.MyBO.DealerId
            End If
        Catch ex As Exception
            Me.ErrorCtrl.AddError(ex.Message, False)
            Me.ErrorCtrl.Show()
        End Try
    End Sub

#Region "Detail Grid"

    Sub PopulateUserControlAvailableSelectedProductCodes()
        Me.UserControlAvailableSelectedProductCodes.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedProductCodes, False)
        If Not Me.State.MyBO.Id.Equals(Guid.Empty) Then
            Dim availableDv As DataView
            Dim selectedDv As DataView
            If Not TheDealerControl.SelectedGuid.Equals(Guid.Empty) Then
                Dim availDS As DataSet = Me.State.MyBO.GetAvailableProductCodes(TheDealerControl.SelectedGuid)
                If availDS.Tables.Count > 0 Then
                    availableDv = New DataView(availDS.Tables(0))
                End If
            End If

            Dim selectedDS As DataSet = Me.State.MyBO.GetSelectedProductCodes(TheDealerControl.SelectedGuid)
            If Not Me.State.IsNew Then
                If selectedDS.Tables.Count > 0 Then
                    selectedDv = New DataView(selectedDS.Tables(0))
                    selectedDv.RowFilter = "PRODUCT_GROUP_NAME = " + "'" + Me.TextboxDescription.Text.Trim + "'"
                End If
            Else
                selectedDv = Nothing
            End If

            Me.UserControlAvailableSelectedProductCodes.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            Me.UserControlAvailableSelectedProductCodes.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedProductCodes, True)
            'UserControlAvailableSelectedProductCodes.AvailableDesc = TranslationBase.TranslateLabelOrMessage(AVAILABLE_PRODUCTCODES)
            'UserControlAvailableSelectedProductCodes.SelectedDesc = TranslationBase.TranslateLabelOrMessage(SELECTED_PRODUCTCODES)
        End If
    End Sub

    Sub PopulateDetailControls()
        PopulateUserControlAvailableSelectedProductCodes()
    End Sub
#End Region

    Protected Sub PopulateFormFromBOs()
        With Me.State.MyBO
            Me.PopulateControlFromBOProperty(Me.TextboxDescription, .Description)
            PopulateDealer()
            PopulateUserControlAvailableSelectedProductCodes()
        End With
    End Sub

    Protected Sub PopulateBOsFormFrom()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.TextboxDescription)
            .DealerId = TheDealerControl.SelectedGuid
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        Me.State.MyBO = New ProductGroup
        Me.State.IsNew = True
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Dim newObj As New ProductGroup
        newObj.Copy(Me.State.MyBO)

        Me.State.MyBO = newObj
        Me.State.MyBO.Description = Nothing
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
        Me.UserControlAvailableSelectedProductCodes.SetSelectedData(Nothing, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)

        'create the backup copy
        Me.State.ScreenSnapShotBO = New ProductGroup
        Me.State.ScreenSnapShotBO.Copy(Me.State.MyBO)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                Me.State.MyBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub


#End Region


#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.ErrorCtrl.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.State.MyBO.Save()
                Me.State.IsNew = False
                Me.State.HasDataChanged = True
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            Else
                Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New ProductGroup(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New ProductGroup
            End If
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub



    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Dim ProductGroupIDs As New ArrayList
        Dim proGroupIdStr As String
        For Each proGroupIdStr In Me.UserControlAvailableSelectedProductCodes.SelectedList
            ProductGroupIDs.Add(proGroupIdStr)
        Next
        Me.State.MyBO.DetachProductCodes(ProductGroupIDs)

        Dim addressDeleted As Boolean
        Try
            Me.State.MyBO.DeleteAndSave()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub



    Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


#End Region

#Region "Page Control Events"

#End Region

#Region "Handle-Drop"


#End Region

#Region "Attach - Detach Event Handlers"

    Private Sub UserControlAvailableSelectedProductCodes_Attach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedProductCodes.Attach
        Try
            If attachedList.Count > 0 Then
                Me.State.MyBO.AttachProductcodes(attachedList)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedProductCodes_Detach(ByVal aSrc As Generic.UserControlAvailableSelected, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedProductCodes.Detach
        Try
            If detachedList.Count > 0 Then
                Me.State.MyBO.DetachProductCodes(detachedList)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

    Private Sub mpHoriz_SelectedIndexChange(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub
End Class
