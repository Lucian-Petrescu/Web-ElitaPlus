Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports Microsoft.VisualBasic

Partial Class PriceGroupForm
    Inherits ElitaPlusSearchPage



    Protected WithEvents ErrorCtrl As ErrorController





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
    Public Const URL As String = "PriceGroupForm.aspx"

    Public Const GRID_COL_EDIT As Integer = 0
    Public Const GRID_COL_RISK_TYPE As Integer = 1
    Public Const GRID_COL_EFFECTIVE_DATE As Integer = 2
    Public Const GRID_COL_PRICE_BAND_RANGE_FROM As Integer = 3
    Public Const GRID_COL_PRICE_BAND_RANGE_TO As Integer = 4
    Public Const GRID_COL_PG_DETAIL_ID As Integer = 5
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As PriceGroup
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As PriceGroup, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"


    Class MyState
        Public MyBO As PriceGroup
        Public MyChildBO As PriceGroupDetail
        Public ScreenSnapShotBO As PriceGroup
        Public ScreenSnapShotChildBO As PriceGroupDetail

        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public SortExpressionDetailGrid As String = PriceGroup.PriceGroupDetailSelectionView.RISK_TYPE_COL_NAME & ", " & PriceGroup.PriceGroupDetailSelectionView.EFFECTIVE_DATE_COL_NAME & ", " & PriceGroup.PriceGroupDetailSelectionView.PRICE_BAND_RANGE_FROM_COL_NAME
        Public LastErrMsg As String
        Public IsChildEditing As Boolean = False
        Public selectedChildId As Guid = Guid.Empty
        Public DetailPageIndex As Integer = 0
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
                State.MyBO = New PriceGroup(CType(CallingParameters, Guid))
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
                '  Me.MenuEnabled = False
                AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                AddControlMsg(btnDeleteChild_Write, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
                If State.MyBO Is Nothing Then
                    State.MyBO = New PriceGroup
                End If
                ' Me.BindListControlToDataView(moCountryDrop, LookupListNew.GetUserCountriesLookupList(), , , False)
                Dim listcontext As ListContext = New ListContext()
                Dim countryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries

                Dim filteredList As ListItem() = (From x In countryLkl
                                                  Where list.Contains(x.ListItemId)
                                                  Select x).ToArray()
                moCountryDrop.Populate(filteredList, New PopulateOptions())

                PopulateCountry()
                PopulateDropdowns()
                PopulateFormFromBOs()
                EnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            BindDetailBoPropertiesToLabels()
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
        ControlMgr.SetEnableControl(Me, btnAddNewChild_Write, True)
        ControlMgr.SetEnableControl(Me, btnAddChildWithCopy_Write, True)
        ControlMgr.SetEnableControl(Me, btnDeleteChild_Write, True)

        If State.IsChildEditing Then
            ControlMgr.SetVisibleControl(Me, PanelAllEditDetail, True)
            EnableDisableParentControls(False)
            'Now disable depebding on the object state
            If (State.MyChildBO.IsNew) Then
                ControlMgr.SetEnableControl(Me, btnAddNewChild_Write, False)
                ControlMgr.SetEnableControl(Me, btnAddChildWithCopy_Write, False)
                ControlMgr.SetEnableControl(Me, btnDeleteChild_Write, False)
                'Now disable depebding on the object state
            End If
        Else
            ControlMgr.SetVisibleControl(Me, PanelAllEditDetail, False)
            EnableDisableParentControls(True)
        End If

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

        'WRITE YOU OWN CODE HERE
    End Sub

    Sub EnableDisableParentControls(enableToggle As Boolean)
        ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnSave_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnUndo_Write, enableToggle)

        ControlMgr.SetEnableControl(Me, TextboxShortDesc_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, TextboxDescription_WRITE, enableToggle)
        ControlMgr.SetVisibleControl(Me, DataGridDetail, enableToggle)
        ControlMgr.SetVisibleControl(Me, btnAddNewChildFromGrid_WRITE, enableToggle)
    End Sub


    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, "ShortDesc", LabelShortDesc)
        BindBOPropertyToLabel(State.MyBO, "Description", LabelDescription)
        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Private Sub PopulateCountry()
        Dim oCountry As Country

        If State.MyBO.IsNew Then
            ' New one
            If moCountryDrop.SelectedIndex = NO_ITEM_SELECTED_INDEX Then
                moCountryDrop.SelectedIndex = 0
            End If
            PopulateControlFromBOProperty(moCountryText_NO_TRANSLATE, GetSelectedDescription(moCountryDrop))
            State.MyBO.CountryId = GetSelectedItem(moCountryDrop)
        Else
            oCountry = New Country(State.MyBO.CountryId)
            SetSelectedItem(moCountryDrop, State.MyBO.CountryId)
            PopulateControlFromBOProperty(moCountryText_NO_TRANSLATE, oCountry.Description)
        End If

        If ((moCountryDrop.Items.Count > 1) AndAlso State.MyBO.IsNew) Then
            ' Multiple Countries
            ControlMgr.SetVisibleControl(Me, moCountryDrop, True)
            ControlMgr.SetVisibleControl(Me, moCountryText_NO_TRANSLATE, False)
        Else
            ControlMgr.SetVisibleControl(Me, moCountryDrop, False)
            ControlMgr.SetVisibleControl(Me, moCountryText_NO_TRANSLATE, True)
            moCountryText_NO_TRANSLATE.ReadOnly = True

        End If
    End Sub

    Protected Sub PopulateDropdowns()
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim compId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyId

        'TODO. Replace the YesNo Lookup List with the right Lookup List
    End Sub

    Protected Sub PopulateFormFromBOs()
        With State.MyBO
            PopulateDetailGrid()

            PopulateControlFromBOProperty(TextboxShortDesc_WRITE, .ShortDesc)
            PopulateControlFromBOProperty(TextboxDescription_WRITE, .Description)

        End With

    End Sub

    Sub PopulateDetailGrid()
        'This is a temporary Binding Logic. BEGIN        
        Dim dv As PriceGroup.PriceGroupDetailSelectionView = State.MyBO.GetDetailSelectionView(Authentication.CurrentUser.CompanyGroup.Id)
        dv.Sort = State.SortExpressionDetailGrid

        DataGridDetail.Columns(GRID_COL_RISK_TYPE).SortExpression = PriceGroup.PriceGroupDetailSelectionView.RISK_TYPE_COL_NAME
        DataGridDetail.Columns(GRID_COL_EFFECTIVE_DATE).SortExpression = PriceGroup.PriceGroupDetailSelectionView.EFFECTIVE_DATE_COL_NAME
        DataGridDetail.Columns(GRID_COL_PRICE_BAND_RANGE_FROM).SortExpression = PriceGroup.PriceGroupDetailSelectionView.PRICE_BAND_RANGE_FROM_COL_NAME
        DataGridDetail.Columns(GRID_COL_PRICE_BAND_RANGE_TO).SortExpression = PriceGroup.PriceGroupDetailSelectionView.PRICE_BAND_RANGE_TO_COL_NAME
        SetGridItemStyleColor(DataGridDetail)

        SetPageAndSelectedIndexFromGuid(dv, State.selectedChildId, DataGridDetail, State.DetailPageIndex)
        State.DetailPageIndex = DataGridDetail.CurrentPageIndex

        DataGridDetail.DataSource = dv
        DataGridDetail.AutoGenerateColumns = False
        DataGridDetail.DataBind()
        'This is a temporary Binding Logic. END
    End Sub


    Protected Sub PopulateBOsFormFrom()
        With State.MyBO
            PopulateBOProperty(State.MyBO, "ShortDesc", TextboxShortDesc_WRITE)
            PopulateBOProperty(State.MyBO, "Description", TextboxDescription_WRITE)
            PopulateBOProperty(State.MyBO, "CountryId", moCountryDrop)
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub


    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        State.MyBO = New PriceGroup
        PopulateCountry()
        PopulateFormFromBOs()
        EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Dim newObj As New PriceGroup
        newObj.Copy(State.MyBO)

        State.MyBO = newObj
        PopulateCountry()
        PopulateFormFromBOs()
        EnableDisableFields()

        'create the backup copy
        State.ScreenSnapShotBO = New PriceGroup
        State.ScreenSnapShotBO.Copy(State.MyBO)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
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
                Case ElitaPlusPage.DetailPageCommand.Accept
                    PopulateChildBOFromDetail()
                    State.MyChildBO.Save()
                    State.MyChildBO.EndEdit()
                    State.MyBO.Save()

                    State.IsChildEditing = False
                    EnableDisableFields()
                    PopulateDetailGrid()
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
                Case ElitaPlusPage.DetailPageCommand.Accept
                    State.MyChildBO.cancelEdit()
                    If State.MyChildBO.IsSaveNew Then
                        State.MyChildBO.Delete()
                        State.MyChildBO.Save()
                    End If

                    State.IsChildEditing = False
                    EnableDisableFields()
                    PopulateDetailGrid()
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Sub BeginChildEdit()
        State.IsChildEditing = True
        AddCalendar(ImageButtonEffectiveDate, TextboxEffectiveDate)
        With State
            If Not .selectedChildId.Equals(Guid.Empty) Then
                .MyChildBO = .MyBO.GetChild(.selectedChildId)
            Else
                .MyChildBO = .MyBO.GetNewChild
            End If
            .MyChildBO.BeginEdit()
        End With
        EnableDisableFields()
        PopulateDetailFromChildBO()
    End Sub

    Sub EndChildEdit(lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        PopulateChildBOFromDetail()
                        .MyChildBO.Save()
                        .MyChildBO.EndEdit()
                        .MyBO.Save()
                    Case ElitaPlusPage.DetailPageCommand.Cancel
                        .MyChildBO.cancelEdit()
                        If .MyChildBO.IsSaveNew Then
                            .MyChildBO.Delete()
                            .MyChildBO.Save()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Back
                        .MyChildBO.cancelEdit()
                        If .MyChildBO.IsSaveNew Then
                            .MyChildBO.Delete()
                            .MyChildBO.Save()
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        .MyChildBO.Delete()
                        .MyChildBO.Save()
                        .MyChildBO.EndEdit()
                        .selectedChildId = Guid.Empty
                End Select
            End With
            State.IsChildEditing = False
            EnableDisableFields()
            PopulateDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Sub PopulateDetailFromChildBO()
        ' Me.BindListControlToDataView(Me.DropdownlistRiskType, LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
        'populate tax type
        Dim listcontext As ListContext = New ListContext()
        listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim riskTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
        DropdownlistRiskType.Populate(riskTypeLkl, New PopulateOptions() With
             {
               .AddBlankItem = True
                })
        ' Dim oTaxTypeList As DataView = LookupListNew.DropdownLookupList("TTYP", ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        'oTaxTypeList.RowFilter &= " and CODE IN ('7', '8')"

        ' Me.BindListControlToDataView(ddlReplTaxType, oTaxTypeList)
        Dim replTaxTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("TTYP", Thread.CurrentPrincipal.GetLanguageCode())
        Dim FilteredRecord As ListItem() = (From x In replTaxTypeLkl
                                            Where x.Code = "7" Or x.Code = "8"
                                            Select x).ToArray()
        ddlReplTaxType.Populate(FilteredRecord, New PopulateOptions() With
                                  {
                                  .AddBlankItem = True
                                  })

        With State.MyChildBO
            PopulateControlFromBOProperty(TextboxCarryInPrice, .CarryInPrice)
            PopulateControlFromBOProperty(TextboxCleaningPrice, .CleaningPrice)
            PopulateControlFromBOProperty(TextboxEffectiveDate, .EffectiveDate)
            PopulateControlFromBOProperty(TextboxEstimatePrice, .EstimatePrice)
            PopulateControlFromBOProperty(TextboxHomePrice, .HomePrice)
            PopulateControlFromBOProperty(TextboxHourlyRate, .HourlyRate)
            SetSelectedItem(DropdownlistRiskType, .RiskTypeId)
            PopulateControlFromBOProperty(moReplacementText, .ReplacementPrice)
            PopulateControlFromBOProperty(TextboxPriceBandRangeFrom, .PriceBandRangeFrom)
            PopulateControlFromBOProperty(TextboxPriceBandRangeTo, .PriceBandRangeTo)
            PopulateControlFromBOProperty(TextboxSendInPrice, .SendInPrice)
            PopulateControlFromBOProperty(TextboxPickUpPrice, .PickUpPrice)
            SetSelectedItem(ddlReplTaxType, .ReplacementTaxType)
            PopulateControlFromBOProperty(TextboxDiscountedPrice, .DiscountedPrice)
        End With
    End Sub

    Sub PopulateChildBOFromDetail()
        With State.MyChildBO
            PopulateBOProperty(State.MyChildBO, "CarryInPrice", TextboxCarryInPrice)
            PopulateBOProperty(State.MyChildBO, "CleaningPrice", TextboxCleaningPrice)
            PopulateBOProperty(State.MyChildBO, "EffectiveDate", TextboxEffectiveDate)
            PopulateBOProperty(State.MyChildBO, "EstimatePrice", TextboxEstimatePrice)
            PopulateBOProperty(State.MyChildBO, "HomePrice", TextboxHomePrice)
            PopulateBOProperty(State.MyChildBO, "HourlyRate", TextboxHourlyRate)
            PopulateBOProperty(State.MyChildBO, "RiskTypeId", DropdownlistRiskType)
            PopulateBOProperty(State.MyChildBO, "ReplacementPrice", moReplacementText)
            PopulateBOProperty(State.MyChildBO, "PriceBandRangeFrom", TextboxPriceBandRangeFrom)
            PopulateBOProperty(State.MyChildBO, "PriceBandRangeTo", TextboxPriceBandRangeTo)
            PopulateBOProperty(State.MyChildBO, "SendInPrice", TextboxSendInPrice)
            PopulateBOProperty(State.MyChildBO, "PickUpPrice", TextboxPickUpPrice)
            PopulateBOProperty(State.MyChildBO, "ReplacementTaxType", ddlReplTaxType)
            PopulateBOProperty(State.MyChildBO, "DiscountedPrice", TextboxDiscountedPrice)
        End With
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub BindDetailBoPropertiesToLabels()
        With State
            BindBOPropertyToLabel(.MyChildBO, "CarryInPrice", LabelCarryInPrice)
            BindBOPropertyToLabel(.MyChildBO, "CleaningPrice", LabelCleaningPrice)
            BindBOPropertyToLabel(.MyChildBO, "EffectiveDate", LabelEffectiveDate)
            BindBOPropertyToLabel(.MyChildBO, "EstimatePrice", LabelEstimatePrice)
            BindBOPropertyToLabel(.MyChildBO, "HomePrice", LabelHomePrice)
            BindBOPropertyToLabel(.MyChildBO, "HourlyRate", LabelHourlyRate)
            BindBOPropertyToLabel(.MyChildBO, "RiskTypeId", LabelRiskType)
            BindBOPropertyToLabel(.MyChildBO, "ReplacementPrice", moReplacementLabel)
            BindBOPropertyToLabel(.MyChildBO, "PriceBandRangeFrom", LabelPriceBandRangeFrom)
            BindBOPropertyToLabel(.MyChildBO, "PriceBandRangeTo", LabelPriceBandRangeTo)
            BindBOPropertyToLabel(.MyChildBO, "SendInPrice", LabelSendInPrice)
            BindBOPropertyToLabel(.MyChildBO, "PickUpPrice", LabelPickUpPrice)
            BindBOPropertyToLabel(.MyChildBO, "DiscountedPrice", LabelDiscountedPrice)
        End With
        ClearGridHeadersAndLabelsErrSign()
    End Sub


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



    Public Sub DataGridDetail_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridDetail.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Item.Cells(GRID_COL_PG_DETAIL_ID).Text = New Guid(CType(dvRow(PriceGroup.PriceGroupDetailSelectionView.DETAIL_ID_COL_NAME), Byte())).ToString
                e.Item.Cells(GRID_COL_RISK_TYPE).Text = dvRow(PriceGroup.PriceGroupDetailSelectionView.RISK_TYPE_COL_NAME).ToString
                e.Item.Cells(GRID_COL_EFFECTIVE_DATE).Text = GetDateFormattedString(CType(dvRow(PriceGroup.PriceGroupDetailSelectionView.EFFECTIVE_DATE_COL_NAME), Date))
                e.Item.Cells(GRID_COL_PRICE_BAND_RANGE_FROM).Text = GetAmountFormattedDoubleString(dvRow(PriceGroup.PriceGroupDetailSelectionView.PRICE_BAND_RANGE_FROM_COL_NAME).ToString)
                e.Item.Cells(GRID_COL_PRICE_BAND_RANGE_TO).Text = GetAmountFormattedDoubleString(dvRow(PriceGroup.PriceGroupDetailSelectionView.PRICE_BAND_RANGE_TO_COL_NAME).ToString)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub


    Private Sub DataGridDetail_SortCommand(source As Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGridDetail.SortCommand
        Try
            If State.SortExpressionDetailGrid.StartsWith(e.SortExpression) Then
                If State.SortExpressionDetailGrid.StartsWith(e.SortExpression & " DESC") Then
                    State.SortExpressionDetailGrid = e.SortExpression
                Else
                    State.SortExpressionDetailGrid = e.SortExpression & " DESC"
                End If
            Else
                State.SortExpressionDetailGrid = e.SortExpression
            End If
            If State.SortExpressionDetailGrid.StartsWith(PriceGroup.PriceGroupDetailSelectionView.RISK_TYPE_COL_NAME) Then
                State.SortExpressionDetailGrid &= ", " & PriceGroup.PriceGroupDetailSelectionView.EFFECTIVE_DATE_COL_NAME & ", " & PriceGroup.PriceGroupDetailSelectionView.PRICE_BAND_RANGE_FROM_COL_NAME
            End If
            If State.SortExpressionDetailGrid.StartsWith(PriceGroup.PriceGroupDetailSelectionView.EFFECTIVE_DATE_COL_NAME) Then
                State.SortExpressionDetailGrid &= ", " & PriceGroup.PriceGroupDetailSelectionView.PRICE_BAND_RANGE_FROM_COL_NAME
            End If
            PopulateDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub DataGridDetail_ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGridDetail.ItemCommand
        Try
            If e.CommandName = "ViewRecord" Then
                State.IsChildEditing = True
                State.selectedChildId = New Guid(e.Item.Cells(GRID_COL_PG_DETAIL_ID).Text)
                BeginChildEdit()
                EnableDisableFields()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub DataGridDetail_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridDetail.PageIndexChanged
        Try
            State.DetailPageIndex = e.NewPageIndex
            State.selectedChildId = Guid.Empty
            PopulateDetailGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = ErrorCtrl.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            PopulateBOsFormFrom()
            If State.MyBO.IsDirty Then
                State.MyBO.Save()
                State.HasDataChanged = True
                PopulateCountry()
                PopulateFormFromBOs()
                EnableDisableFields()
                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Else
                DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New PriceGroup(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New PriceGroup
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
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#Region "Detail Clicks"

    Private Sub btnAddNewChildFromGrid_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnAddNewChildFromGrid_WRITE.Click
        Try
            State.selectedChildId = Guid.Empty
            BeginChildEdit()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAddNewChild_Click(sender As Object, e As System.EventArgs) Handles btnAddNewChild_Write.Click
        Try
            State.MyChildBO.cancelEdit()
            If State.MyChildBO.IsSaveNew Then
                State.MyChildBO.Delete()
            End If
            State.MyChildBO = State.MyBO.GetNewChild
            State.MyChildBO.BeginEdit()
            PopulateDetailFromChildBO()
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAddChildWithCopy_Click(sender As Object, e As System.EventArgs) Handles btnAddChildWithCopy_Write.Click
        Try
            With State
                State.MyChildBO.cancelEdit()
                If State.MyChildBO.IsSaveNew Then
                    State.MyChildBO.Delete()
                End If
                State.MyChildBO = .MyBO.GetNewChild
                State.MyChildBO.BeginEdit()
                PopulateChildBOFromDetail()
            End With
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnBackChild_Click(sender As System.Object, e As System.EventArgs) Handles btnBackChild.Click
        Try
            PopulateChildBOFromDetail()
            If State.MyChildBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            Else
                EndChildEdit(ElitaPlusPage.DetailPageCommand.Back)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO_CANCEL, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = ErrorCtrl.Text
        End Try
    End Sub

    'Private Sub btnCancelChild_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelChild.Click

    '    Try
    '        If Not Me.State.MyChildBO.IsNew Then
    '            'Reload from the DB
    '            Me.State.MyChildBO = Me.State.MyBO.GetChild(Me.State.selectedChildId)
    '            Me.PopulateDetailFromChildBO()
    '        ElseIf Not Me.State.ScreenSnapShotChildBO Is Nothing Then
    '            'It was a new with copy
    '            Me.State.MyChildBO.Clone(Me.State.ScreenSnapShotChildBO)
    '            Me.PopulateDetailFromChildBO()
    '        Else
    '            Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.Back)
    '        End If
    '        Me.EnableDisableFields()
    '    Catch ex As Exception
    '        Me.HandleErrors(ex, Me.ErrorCtrl)
    '    End Try

    'End Sub

    Private Sub btnOkChild_Click(sender As Object, e As System.EventArgs) Handles btnOkChild_Write.Click
        Try
            EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

    Private Sub btnDeleteChild_Write_Click(sender As Object, e As System.EventArgs) Handles btnDeleteChild_Write.Click
        Try
            EndChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
        Catch ex As Exception
            HandleErrors(ex, ErrorCtrl)
        End Try
    End Sub

#End Region


#End Region

#Region "Page Control Events"

#End Region


#Region "Error Handling"

#End Region




End Class


