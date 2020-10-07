'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebFormCodeBehind.cst (9/20/2004)  ********************

Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Class ServiceGroupForm
    Inherits ElitaPlusSearchPage



    Protected WithEvents ErrorCtrl As ErrorController



    Protected WithEvents UserControlAvailableSelectedManufacturers As Assurant.ElitaPlus.ElitaPlusWebApp.Generic.UserControlAvailableSelected

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const URL As String = "ServiceGroupForm.aspx"
    Public Const GRID_COL_DELETE_BUTTON As Integer = 0
    Public Const GRID_COL_RISK_TYPE_ID As Integer = 1
    Public Const GRID_COL_RISK_TYPE As Integer = 2
    Public Const GRID_COL_MANUFACTURER As Integer = 3
    Public Const GRID_COL_RISK_TYPE_MANUFACTURER_ID As Integer = 4
    Public Const RISK_TYPE_COL_NAME As String = "risk_type"
    Public Const MANUFACTURER_COL_NAME As String = "manufacturer"
    Public Const SERVICE_GROUP_RISK_COL_ID As String = "service_group_risk_type_id"
    Public Const SERVICE_GROUP_RISK_MAN_COL_ID As String = "service_group_risk_manu_id"
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As ServiceGroup
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As ServiceGroup, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"


    Class MyState
        Public MyBO As ServiceGroup
        Public ScreenSnapShotBO As ServiceGroup
        Public SelectedRiskTypeId As Guid = Guid.Empty
        Public SortExpressionDetailGrid As String = "RISK_TYPE_ENGLISH"
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

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                State.MyBO = New ServiceGroup(CType(CallingParameters, Guid))
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try

    End Sub

#End Region

#Region "Page Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        Try
            ErrorCtrl.Clear_Hide()
            If Not IsPostBack Then
                'Date Calendars
                MenuEnabled = False
                'Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                If State.MyBO Is Nothing Then
                    State.MyBO = New ServiceGroup
                    State.IsNew = True
                End If
                ' Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(), , , False)

                Dim countryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode())
                Dim FilteredRecord As ListItem() = (From x In countryLKl
                                                    Where (ElitaPlusIdentity.Current.ActiveUser.Countries).Contains(x.ListItemId)
                                                    Select x).ToArray()

                moCountryDrop.Populate(FilteredRecord, New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = False
                                                   })
                PopulateCountry()
                PopulateDropdowns()
                PopulateFormFromBOs()
                EnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
        ShowMissingTranslations(ErrorCtrl)
    End Sub
#End Region

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
        'Now disable depebding on the object state
        If State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If
        ControlMgr.SetEnableControl(Me, TextboxShortDesc, True)
        If Not State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, TextboxShortDesc, False)
        End If
    End Sub

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "ShortDesc", LabelShortDesc)
        BindBOPropertyToLabel(State.MyBO, "Description", LabelDescription)
        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Private Sub PopulateCountry()
        Dim oCountry As Country

        If State.IsNew Then
            ' New one
            If moCountryDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX Then
                moCountryDrop.SelectedIndex = 0
            End If
            PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, GetSelectedDescription(moCountryDrop))
            State.MyBO.CountryId = GetSelectedItem(moCountryDrop)
        Else
            oCountry = New Country(State.MyBO.CountryId)
            SetSelectedItem(moCountryDrop, State.MyBO.CountryId)
            PopulateControlFromBOProperty(moCountryLabel_NO_TRANSLATE, oCountry.Description)
        End If

        If ((moCountryDrop.Items.Count > 1) AndAlso State.IsNew) Then
            ' Multiple Countries
            ControlMgr.SetVisibleControl(Me, moCountryDrop, True)
            ControlMgr.SetVisibleControl(Me, moCountryLabel_NO_TRANSLATE, False)
        Else
            ControlMgr.SetVisibleControl(Me, moCountryDrop, False)
            ControlMgr.SetVisibleControl(Me, moCountryLabel_NO_TRANSLATE, True)
        End If
    End Sub

    Protected Sub PopulateDropdowns()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        'Me.BindListControlToDataView(Me.DropDownRiskType, LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
        Dim listcontext As ListContext = New ListContext()
        listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim riskTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
        DropDownRiskType.Populate(riskTypeLkl, New PopulateOptions() With
     {
       .AddBlankItem = True
        })
    End Sub

#Region "Detail Grid"
    Sub PopulateDetailGrid()
        'This is a temporary Binding Logic. BEGIN        
        Dim dv As DataSet
        Dim countofrecords As Double
        countofrecords = State.MyBO.countofrecords(State.MyBO.Id)

        dv = GetDV()

        DataGridDetail.DataSource = dv
        DataGridDetail.AllowCustomPaging = True
        DataGridDetail.VirtualItemCount = countofrecords
        DataGridDetail.AutoGenerateColumns = False
        DataGridDetail.DataBind()
        'This is a temporary Binding Logic. END
    End Sub

    Private Function GetDV() As DataSet
        Dim dv As DataSet
        dv = State.MyBO.LoadGrid(State.MyBO.Id, DataGridDetail.CurrentPageIndex, State.SortExpressionDetailGrid)
        Return (dv)
    End Function

    Sub PopulateUserControlAvailableSelectedManufacturers()
        UserControlAvailableSelectedManufacturers.BackColor = "#d5d6e4"
        ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedManufacturers, False)
        If Not State.SelectedRiskTypeId.Equals(Guid.Empty) Then
            If RadioButtonListManufacturer.SelectedItem.Value <> "Any" Then
                Dim availableDv As DataView = State.MyBO.GetAvailableManufacturers(State.SelectedRiskTypeId)
                Dim selectedDv As DataView = State.MyBO.GetSelectedManufacturers(State.SelectedRiskTypeId)
                UserControlAvailableSelectedManufacturers.SetAvailableData(availableDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                UserControlAvailableSelectedManufacturers.SetSelectedData(selectedDv, LookupListNew.COL_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedManufacturers, True)
            End If
        End If
    End Sub

    Sub PopulateDetailControls()
        PopulateDetailGrid()
        PopulateUserControlAvailableSelectedManufacturers()
    End Sub
#End Region

    Protected Sub PopulateFormFromBOs()
        With State.MyBO

            PopulateControlFromBOProperty(TextboxShortDesc, .ShortDesc)
            PopulateControlFromBOProperty(TextboxDescription, .Description)

        End With

        PopulateDetailControls()

    End Sub

    Protected Sub PopulateBOsFormFrom()
        With State.MyBO
            PopulateBOProperty(State.MyBO, "ShortDesc", TextboxShortDesc)
            PopulateBOProperty(State.MyBO, "Description", TextboxDescription)
            PopulateBOProperty(State.MyBO, "CountryId", moCountryDrop)
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub


    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        State.MyBO = New ServiceGroup
        State.IsNew = True
        PopulateCountry()
        PopulateFormFromBOs()
        EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Dim newObj As New ServiceGroup
        newObj.Copy(State.MyBO)

        State.MyBO = newObj
        PopulateCountry()
        PopulateFormFromBOs()
        EnableDisableFields()

        'create the backup copy
        State.ScreenSnapShotBO = New ServiceGroup
        State.ScreenSnapShotBO.Copy(State.MyBO)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                State.MyBO.Save()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ErrorCtrl.AddErrorAndShow(State.LastErrMsg)
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub


#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = ErrorCtrl.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then

                Dim result As Integer
                Dim Strarr As String
                Dim risktypeid As Guid = Guid.Parse(DropDownRiskType.SelectedValue.ToString())

                For result = 0 To UserControlAvailableSelectedManufacturers.SelectedList.Count() - 1
                    Strarr = Strarr + Assurant.ElitaPlus.Common.GuidControl.GuidToHexString(Guid.Parse(UserControlAvailableSelectedManufacturers.SelectedList.Item(result))).ToString() + ";"
                Next

                State.MyBO.sgrtmanusave(State.MyBO.Id, risktypeid, Strarr)
                State.IsNew = False
                State.HasDataChanged = True
                PopulateCountry()
                PopulateFormFromBOs()
                EnableDisableFields()
                'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Else
                DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
                'Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New ServiceGroup(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New ServiceGroup
            End If
            PopulateCountry()
            PopulateFormFromBOs()
            EnableDisableFields()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            State.MyBO.Delete()
            State.MyBO.Save()
            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            State.MyBO.RejectChanges()
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Page Control Events"

#End Region

#Region "Handle-Drop"

    Private Sub moCountryDrop_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles moCountryDrop.SelectedIndexChanged
        Try
            State.MyBO.CountryId = GetSelectedItem(moCountryDrop)
            PopulateCountry()
            PopulateDropdowns()
            PopulateFormFromBOs()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region

#Region "Detail Grid Events"

    Private Sub DataGridDetail_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridDetail.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                e.Item.Cells(GRID_COL_RISK_TYPE).Text = dvRow(RISK_TYPE_COL_NAME).ToString
                e.Item.Cells(GRID_COL_MANUFACTURER).Text = dvRow(MANUFACTURER_COL_NAME).ToString
                e.Item.Cells(GRID_COL_RISK_TYPE_ID).Text = dvRow(SERVICE_GROUP_RISK_COL_ID).ToString
                e.Item.Cells(GRID_COL_RISK_TYPE_MANUFACTURER_ID).Text = dvRow(SERVICE_GROUP_RISK_MAN_COL_ID).ToString

                Dim del As ImageButton
                del = CType(e.Item.Cells(GRID_COL_DELETE_BUTTON).FindControl("btnDeleteRiskType_WRITE"), ImageButton)
                If del IsNot Nothing Then
                    AddConfirmation(del, ElitaPlus.ElitaPlusWebApp.Message.DELETE_RECORD_PROMPT)
                End If

            End If

            'if new record or new with copy, hide the delete column
            If State.MyBO.IsNew Then DataGridDetail.Columns(GRID_COL_DELETE_BUTTON).Visible = False
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub DataGridDetail_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridDetail.PageIndexChanged
        Try
            DataGridDetail.CurrentPageIndex = e.NewPageIndex
            PopulateDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub DataGridDetail_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGridDetail.SortCommand
        Try
            If State.SortExpressionDetailGrid.StartsWith(e.SortExpression) Then
                If State.SortExpressionDetailGrid.EndsWith(" DESC") Then
                    State.SortExpressionDetailGrid = e.SortExpression
                Else
                    State.SortExpressionDetailGrid = e.SortExpression & " DESC"
                End If
            Else
                State.SortExpressionDetailGrid = e.SortExpression
            End If
            PopulateDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub DataGridDetail_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGridDetail.ItemCommand

        Try
            If e.CommandName = "DeleteRiskType" Then

                PopulateBOsFormFrom()

                If State.MyBO.IsDirty Then
                    State.MyBO.Save()
                    State.IsNew = False
                    State.HasDataChanged = True
                End If

                If Not e.Item.Cells(GRID_COL_MANUFACTURER).Text.Trim.Equals("*") Then
                    Dim _serviceGroupRiskTypeMan As New SgRtManufacturer(New Guid(e.Item.Cells(GRID_COL_RISK_TYPE_MANUFACTURER_ID).Text))
                    _serviceGroupRiskTypeMan.Delete()
                    _serviceGroupRiskTypeMan.Save()
                Else
                    Dim _serviceGroupRiskType As New ServiceGroupRiskType(New Guid(e.Item.Cells(GRID_COL_RISK_TYPE_ID).Text))
                    _serviceGroupRiskType.Delete()
                    _serviceGroupRiskType.Save()
                End If

                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New ServiceGroup(State.MyBO.Id)
                End If

                PopulateDetailControls()
            End If

        Catch ex As Exception
            ErrorCtrl.AddErrorAndShow(ex.Message, False)
        End Try
    End Sub

#End Region


#Region "Attach - Detach Event Handlers"
    Private Sub RadioButtonListManufacturer_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles RadioButtonListManufacturer.SelectedIndexChanged
        Try
            If Not State.SelectedRiskTypeId.Equals(Guid.Empty) Then
                If RadioButtonListManufacturer.SelectedItem.Value <> "Any" Then
                    If State.MyBO.IsRiskTypeAssociatedForAnyManufacturer(State.SelectedRiskTypeId) Then
                        State.MyBO.DetachRiskType(State.SelectedRiskTypeId)
                        PopulateDetailControls()
                    End If
                Else
                    State.MyBO.AttachRiskTypeForAnyManufacturer(State.SelectedRiskTypeId)
                    PopulateDetailControls()
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


    Private Sub DropDownRiskType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownRiskType.SelectedIndexChanged
        Try
            State.SelectedRiskTypeId = GetSelectedItem(DropDownRiskType)
            RadioButtonListManufacturer.SelectedIndex = 0 'Selected
            If Not State.SelectedRiskTypeId.Equals(Guid.Empty) Then
                If State.MyBO.IsRiskTypeAssociatedForAnyManufacturer(State.SelectedRiskTypeId) Then
                    RadioButtonListManufacturer.SelectedIndex = 1 'Any
                End If
            End If
            PopulateUserControlAvailableSelectedManufacturers()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedManufacturers_Attach(aSrc As Generic.UserControlAvailableSelected, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedManufacturers.Attach
        Try
            If attachedList.Count > 0 Then
                State.MyBO.AttachManufacturers(State.SelectedRiskTypeId, attachedList)
                PopulateDetailGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub UserControlAvailableSelectedManufacturers_Detach(aSrc As Generic.UserControlAvailableSelected, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedManufacturers.Detach
        Try
            If detachedList.Count > 0 Then
                State.MyBO.DetachManufacturers(State.SelectedRiskTypeId, detachedList)
                PopulateDetailGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region



End Class


