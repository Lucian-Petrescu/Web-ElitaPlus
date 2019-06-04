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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As PriceGroup, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New PriceGroup(CType(Me.CallingParameters, Guid))
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
                '  Me.MenuEnabled = False
                Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                Me.AddControlMsg(Me.btnDeleteChild_Write, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, True)
                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New PriceGroup
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
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            End If
            BindBoPropertiesToLabels()
            BindDetailBoPropertiesToLabels()
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

#Region "Controlling Logic"

    Protected Sub EnableDisableFields()
        ControlMgr.SetEnableControl(Me, btnAddNewChild_Write, True)
        ControlMgr.SetEnableControl(Me, btnAddChildWithCopy_Write, True)
        ControlMgr.SetEnableControl(Me, btnDeleteChild_Write, True)

        If Me.State.IsChildEditing Then
            ControlMgr.SetVisibleControl(Me, PanelAllEditDetail, True)
            EnableDisableParentControls(False)
            'Now disable depebding on the object state
            If (Me.State.MyChildBO.IsNew) Then
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
        If Me.State.MyBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
        End If

        'WRITE YOU OWN CODE HERE
    End Sub

    Sub EnableDisableParentControls(ByVal enableToggle As Boolean)
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
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ShortDesc", Me.LabelShortDesc)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Description", Me.LabelDescription)
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub

    Private Sub PopulateCountry()
        Dim oCountry As Country

        If Me.State.MyBO.IsNew Then
            ' New one
            If Me.moCountryDrop.SelectedIndex = Me.NO_ITEM_SELECTED_INDEX Then
                moCountryDrop.SelectedIndex = 0
            End If
            Me.PopulateControlFromBOProperty(moCountryText_NO_TRANSLATE, Me.GetSelectedDescription(moCountryDrop))
            Me.State.MyBO.CountryId = Me.GetSelectedItem(moCountryDrop)
        Else
            oCountry = New Country(Me.State.MyBO.CountryId)
            Me.SetSelectedItem(moCountryDrop, Me.State.MyBO.CountryId)
            Me.PopulateControlFromBOProperty(moCountryText_NO_TRANSLATE, oCountry.Description)
        End If

        If ((moCountryDrop.Items.Count > 1) AndAlso Me.State.MyBO.IsNew) Then
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
        With Me.State.MyBO
            PopulateDetailGrid()

            Me.PopulateControlFromBOProperty(Me.TextboxShortDesc_WRITE, .ShortDesc)
            Me.PopulateControlFromBOProperty(Me.TextboxDescription_WRITE, .Description)

        End With

    End Sub

    Sub PopulateDetailGrid()
        'This is a temporary Binding Logic. BEGIN        
        Dim dv As PriceGroup.PriceGroupDetailSelectionView = Me.State.MyBO.GetDetailSelectionView(Authentication.CurrentUser.CompanyGroup.Id)
        dv.Sort = Me.State.SortExpressionDetailGrid

        Me.DataGridDetail.Columns(Me.GRID_COL_RISK_TYPE).SortExpression = PriceGroup.PriceGroupDetailSelectionView.RISK_TYPE_COL_NAME
        Me.DataGridDetail.Columns(Me.GRID_COL_EFFECTIVE_DATE).SortExpression = PriceGroup.PriceGroupDetailSelectionView.EFFECTIVE_DATE_COL_NAME
        Me.DataGridDetail.Columns(Me.GRID_COL_PRICE_BAND_RANGE_FROM).SortExpression = PriceGroup.PriceGroupDetailSelectionView.PRICE_BAND_RANGE_FROM_COL_NAME
        Me.DataGridDetail.Columns(Me.GRID_COL_PRICE_BAND_RANGE_TO).SortExpression = PriceGroup.PriceGroupDetailSelectionView.PRICE_BAND_RANGE_TO_COL_NAME
        Me.SetGridItemStyleColor(Me.DataGridDetail)

        SetPageAndSelectedIndexFromGuid(dv, Me.State.selectedChildId, Me.DataGridDetail, Me.State.DetailPageIndex)
        Me.State.DetailPageIndex = Me.DataGridDetail.CurrentPageIndex

        Me.DataGridDetail.DataSource = dv
        Me.DataGridDetail.AutoGenerateColumns = False
        Me.DataGridDetail.DataBind()
        'This is a temporary Binding Logic. END
    End Sub


    Protected Sub PopulateBOsFormFrom()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "ShortDesc", Me.TextboxShortDesc_WRITE)
            Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.TextboxDescription_WRITE)
            Me.PopulateBOProperty(Me.State.MyBO, "CountryId", moCountryDrop)
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub


    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        Me.State.MyBO = New PriceGroup
        PopulateCountry()
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
    End Sub

    Protected Sub CreateNewWithCopy()
        Dim newObj As New PriceGroup
        newObj.Copy(Me.State.MyBO)

        Me.State.MyBO = newObj
        PopulateCountry()
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()

        'create the backup copy
        Me.State.ScreenSnapShotBO = New PriceGroup
        Me.State.ScreenSnapShotBO.Copy(Me.State.MyBO)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Accept Then
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
                Case ElitaPlusPage.DetailPageCommand.Accept
                    Me.PopulateChildBOFromDetail()
                    Me.State.MyChildBO.Save()
                    Me.State.MyChildBO.EndEdit()
                    Me.State.MyBO.Save()

                    Me.State.IsChildEditing = False
                    Me.EnableDisableFields()
                    Me.PopulateDetailGrid()
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
                Case ElitaPlusPage.DetailPageCommand.Accept
                    Me.State.MyChildBO.cancelEdit()
                    If Me.State.MyChildBO.IsSaveNew Then
                        Me.State.MyChildBO.Delete()
                        Me.State.MyChildBO.Save()
                    End If

                    Me.State.IsChildEditing = False
                    Me.EnableDisableFields()
                    Me.PopulateDetailGrid()
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub

    Sub BeginChildEdit()
        Me.State.IsChildEditing = True
        Me.AddCalendar(Me.ImageButtonEffectiveDate, Me.TextboxEffectiveDate)
        With Me.State
            If Not .selectedChildId.Equals(Guid.Empty) Then
                .MyChildBO = .MyBO.GetChild(.selectedChildId)
            Else
                .MyChildBO = .MyBO.GetNewChild
            End If
            .MyChildBO.BeginEdit()
        End With
        Me.EnableDisableFields()
        Me.PopulateDetailFromChildBO()
    End Sub

    Sub EndChildEdit(ByVal lastop As ElitaPlusPage.DetailPageCommand)
        Try
            With Me.State
                Select Case lastop
                    Case ElitaPlusPage.DetailPageCommand.OK
                        Me.PopulateChildBOFromDetail()
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
            Me.State.IsChildEditing = False
            Me.EnableDisableFields()
            Me.PopulateDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Sub PopulateDetailFromChildBO()
        ' Me.BindListControlToDataView(Me.DropdownlistRiskType, LookupListNew.GetRiskTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id))
        'populate tax type
        Dim listcontext As ListContext = New ListContext()
        listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim riskTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
        Me.DropdownlistRiskType.Populate(riskTypeLkl, New PopulateOptions() With
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

        With Me.State.MyChildBO
            Me.PopulateControlFromBOProperty(Me.TextboxCarryInPrice, .CarryInPrice)
            Me.PopulateControlFromBOProperty(Me.TextboxCleaningPrice, .CleaningPrice)
            Me.PopulateControlFromBOProperty(Me.TextboxEffectiveDate, .EffectiveDate)
            Me.PopulateControlFromBOProperty(Me.TextboxEstimatePrice, .EstimatePrice)
            Me.PopulateControlFromBOProperty(Me.TextboxHomePrice, .HomePrice)
            Me.PopulateControlFromBOProperty(Me.TextboxHourlyRate, .HourlyRate)
            Me.SetSelectedItem(Me.DropdownlistRiskType, .RiskTypeId)
            Me.PopulateControlFromBOProperty(Me.moReplacementText, .ReplacementPrice)
            Me.PopulateControlFromBOProperty(Me.TextboxPriceBandRangeFrom, .PriceBandRangeFrom)
            Me.PopulateControlFromBOProperty(Me.TextboxPriceBandRangeTo, .PriceBandRangeTo)
            Me.PopulateControlFromBOProperty(Me.TextboxSendInPrice, .SendInPrice)
            Me.PopulateControlFromBOProperty(Me.TextboxPickUpPrice, .PickUpPrice)
            Me.SetSelectedItem(Me.ddlReplTaxType, .ReplacementTaxType)
            Me.PopulateControlFromBOProperty(Me.TextboxDiscountedPrice, .DiscountedPrice)
        End With
    End Sub

    Sub PopulateChildBOFromDetail()
        With Me.State.MyChildBO
            Me.PopulateBOProperty(Me.State.MyChildBO, "CarryInPrice", Me.TextboxCarryInPrice)
            Me.PopulateBOProperty(Me.State.MyChildBO, "CleaningPrice", Me.TextboxCleaningPrice)
            Me.PopulateBOProperty(Me.State.MyChildBO, "EffectiveDate", Me.TextboxEffectiveDate)
            Me.PopulateBOProperty(Me.State.MyChildBO, "EstimatePrice", Me.TextboxEstimatePrice)
            Me.PopulateBOProperty(Me.State.MyChildBO, "HomePrice", Me.TextboxHomePrice)
            Me.PopulateBOProperty(Me.State.MyChildBO, "HourlyRate", Me.TextboxHourlyRate)
            Me.PopulateBOProperty(Me.State.MyChildBO, "RiskTypeId", Me.DropdownlistRiskType)
            Me.PopulateBOProperty(Me.State.MyChildBO, "ReplacementPrice", Me.moReplacementText)
            Me.PopulateBOProperty(Me.State.MyChildBO, "PriceBandRangeFrom", Me.TextboxPriceBandRangeFrom)
            Me.PopulateBOProperty(Me.State.MyChildBO, "PriceBandRangeTo", Me.TextboxPriceBandRangeTo)
            Me.PopulateBOProperty(Me.State.MyChildBO, "SendInPrice", Me.TextboxSendInPrice)
            Me.PopulateBOProperty(Me.State.MyChildBO, "PickUpPrice", Me.TextboxPickUpPrice)
            Me.PopulateBOProperty(Me.State.MyChildBO, "ReplacementTaxType", Me.ddlReplTaxType)
            Me.PopulateBOProperty(Me.State.MyChildBO, "DiscountedPrice", Me.TextboxDiscountedPrice)
        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub BindDetailBoPropertiesToLabels()
        With Me.State
            Me.BindBOPropertyToLabel(.MyChildBO, "CarryInPrice", Me.LabelCarryInPrice)
            Me.BindBOPropertyToLabel(.MyChildBO, "CleaningPrice", Me.LabelCleaningPrice)
            Me.BindBOPropertyToLabel(.MyChildBO, "EffectiveDate", Me.LabelEffectiveDate)
            Me.BindBOPropertyToLabel(.MyChildBO, "EstimatePrice", Me.LabelEstimatePrice)
            Me.BindBOPropertyToLabel(.MyChildBO, "HomePrice", Me.LabelHomePrice)
            Me.BindBOPropertyToLabel(.MyChildBO, "HourlyRate", Me.LabelHourlyRate)
            Me.BindBOPropertyToLabel(.MyChildBO, "RiskTypeId", Me.LabelRiskType)
            Me.BindBOPropertyToLabel(.MyChildBO, "ReplacementPrice", Me.moReplacementLabel)
            Me.BindBOPropertyToLabel(.MyChildBO, "PriceBandRangeFrom", Me.LabelPriceBandRangeFrom)
            Me.BindBOPropertyToLabel(.MyChildBO, "PriceBandRangeTo", Me.LabelPriceBandRangeTo)
            Me.BindBOPropertyToLabel(.MyChildBO, "SendInPrice", Me.LabelSendInPrice)
            Me.BindBOPropertyToLabel(.MyChildBO, "PickUpPrice", Me.LabelPickUpPrice)
            Me.BindBOPropertyToLabel(.MyChildBO, "DiscountedPrice", Me.LabelDiscountedPrice)
        End With
        Me.ClearGridHeadersAndLabelsErrSign()
    End Sub


#End Region

#Region "Handle-Drop"

    Private Sub moCountryDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles moCountryDrop.SelectedIndexChanged
        Try
            Me.State.MyBO.CountryId = Me.GetSelectedItem(moCountryDrop)
            PopulateCountry()
            PopulateDropdowns()
            Me.PopulateFormFromBOs()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region


#Region "Detail Grid Events"



    Public Sub DataGridDetail_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGridDetail.ItemDataBound
        Try
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                e.Item.Cells(Me.GRID_COL_PG_DETAIL_ID).Text = New Guid(CType(dvRow(PriceGroup.PriceGroupDetailSelectionView.DETAIL_ID_COL_NAME), Byte())).ToString
                e.Item.Cells(Me.GRID_COL_RISK_TYPE).Text = dvRow(PriceGroup.PriceGroupDetailSelectionView.RISK_TYPE_COL_NAME).ToString
                e.Item.Cells(Me.GRID_COL_EFFECTIVE_DATE).Text = Me.GetDateFormattedString(CType(dvRow(PriceGroup.PriceGroupDetailSelectionView.EFFECTIVE_DATE_COL_NAME), Date))
                e.Item.Cells(Me.GRID_COL_PRICE_BAND_RANGE_FROM).Text = GetAmountFormattedDoubleString(dvRow(PriceGroup.PriceGroupDetailSelectionView.PRICE_BAND_RANGE_FROM_COL_NAME).ToString)
                e.Item.Cells(Me.GRID_COL_PRICE_BAND_RANGE_TO).Text = GetAmountFormattedDoubleString(dvRow(PriceGroup.PriceGroupDetailSelectionView.PRICE_BAND_RANGE_TO_COL_NAME).ToString)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub


    Private Sub DataGridDetail_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles DataGridDetail.SortCommand
        Try
            If Me.State.SortExpressionDetailGrid.StartsWith(e.SortExpression) Then
                If Me.State.SortExpressionDetailGrid.StartsWith(e.SortExpression & " DESC") Then
                    Me.State.SortExpressionDetailGrid = e.SortExpression
                Else
                    Me.State.SortExpressionDetailGrid = e.SortExpression & " DESC"
                End If
            Else
                Me.State.SortExpressionDetailGrid = e.SortExpression
            End If
            If Me.State.SortExpressionDetailGrid.StartsWith(PriceGroup.PriceGroupDetailSelectionView.RISK_TYPE_COL_NAME) Then
                Me.State.SortExpressionDetailGrid &= ", " & PriceGroup.PriceGroupDetailSelectionView.EFFECTIVE_DATE_COL_NAME & ", " & PriceGroup.PriceGroupDetailSelectionView.PRICE_BAND_RANGE_FROM_COL_NAME
            End If
            If Me.State.SortExpressionDetailGrid.StartsWith(PriceGroup.PriceGroupDetailSelectionView.EFFECTIVE_DATE_COL_NAME) Then
                Me.State.SortExpressionDetailGrid &= ", " & PriceGroup.PriceGroupDetailSelectionView.PRICE_BAND_RANGE_FROM_COL_NAME
            End If
            Me.PopulateDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub DataGridDetail_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DataGridDetail.ItemCommand
        Try
            If e.CommandName = "ViewRecord" Then
                Me.State.IsChildEditing = True
                Me.State.selectedChildId = New Guid(e.Item.Cells(Me.GRID_COL_PG_DETAIL_ID).Text)
                Me.BeginChildEdit()
                Me.EnableDisableFields()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub DataGridDetail_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles DataGridDetail.PageIndexChanged
        Try
            Me.State.DetailPageIndex = e.NewPageIndex
            Me.State.selectedChildId = Guid.Empty
            Me.PopulateDetailGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub
#End Region

#Region "Button Clicks"

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.ErrorCtrl.Text
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.State.MyBO.Save()
                Me.State.HasDataChanged = True
                Me.PopulateCountry()
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
                Me.State.MyBO = New PriceGroup(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New PriceGroup
            End If
            PopulateCountry()
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub



    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Me.State.MyBO.Delete()
            Me.State.MyBO.Save()
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            'undo the delete
            Me.State.MyBO.RejectChanges()
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

#Region "Detail Clicks"

    Private Sub btnAddNewChildFromGrid_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNewChildFromGrid_WRITE.Click
        Try
            Me.State.selectedChildId = Guid.Empty
            Me.BeginChildEdit()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAddNewChild_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddNewChild_Write.Click
        Try
            Me.State.MyChildBO.cancelEdit()
            If Me.State.MyChildBO.IsSaveNew Then
                Me.State.MyChildBO.Delete()
            End If
            Me.State.MyChildBO = Me.State.MyBO.GetNewChild
            Me.State.MyChildBO.BeginEdit()
            Me.PopulateDetailFromChildBO()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnAddChildWithCopy_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddChildWithCopy_Write.Click
        Try
            With Me.State
                Me.State.MyChildBO.cancelEdit()
                If Me.State.MyChildBO.IsSaveNew Then
                    Me.State.MyChildBO.Delete()
                End If
                Me.State.MyChildBO = .MyBO.GetNewChild
                Me.State.MyChildBO.BeginEdit()
                Me.PopulateChildBOFromDetail()
            End With
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnBackChild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackChild.Click
        Try
            Me.PopulateChildBOFromDetail()
            If Me.State.MyChildBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            Else
                Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.Back)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.ErrorCtrl.Text
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

    Private Sub btnOkChild_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOkChild_Write.Click
        Try
            Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.OK)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

    Private Sub btnDeleteChild_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteChild_Write.Click
        Try
            Me.EndChildEdit(ElitaPlusPage.DetailPageCommand.Delete)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrorCtrl)
        End Try
    End Sub

#End Region


#End Region

#Region "Page Control Events"

#End Region


#Region "Error Handling"

#End Region




End Class


