Imports System.Web.Services
Imports System.Web.Script.Services
Imports Microsoft.VisualBasic
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Public Class InvoiceDetailForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "


    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const URL As String = "~/Claims/InvoiceDetailForm.aspx"

    Public Const PAGETAB As String = "CLAIMS"
    Public Const PAGESUBTAB As String = "INVOICE"
    Public Const PAGETITLE As String = "INVOICE_DETAIL"

    Public Const SERVICE_CENTER_CHANGE_EVENT_NAME As String = "ServiceCenterChanged"
    Public Const SELECT_ALL_EVENT_NAME As String = "SelectAll"
    Public Const SELECT_SERVICE_CENTER_TO_POPULATE_CLAIM_AUTHORIZATIONS As String = "SELECT_SERVICE_CENTER_TO_POPULATE_CLAIM_AUTHORIZATIONS"
    Public Const SELECTED_SERVICE_CENTER_DO_NOT_HAVE_ANY_AUTHORIZATIONS As String = "SELECTED_SERVICE_CENTER_DO_NOT_HAVE_ANY_AUTHORIZATIONS"
    Public SelectAllCheckBoxId As String
#End Region

#Region "Ajax State"
    Private Shared ReadOnly Property AjaxState() As MyState
        Get
            Return CType(NavPage.ClientNavigator.PageState, MyState)
        End Get
    End Property
#End Region

#Region "Page State"
    Class MyState
        Public MyBO As Invoice

        Public IsAttributeEditing As Boolean = False
        Public IsItemEditing As Boolean = False

        Public IsAttributeAdding As Boolean = False
        Public IsItemAdding As Boolean = False

        Public SortExpressionItem As String = Invoice.InvoiceAuthorizationSearchDV.COL_NAME_CLAIM_NUMBER

        Public InvoiceReturnObject As InvoiceSearchForm.InvoiceReturnType
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean

        Public AvailableAuthorizationsCount As Integer
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property
#End Region

#Region "Page Events"
    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingParameters As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.InvoiceReturnObject = CType(CallingParameters, InvoiceSearchForm.InvoiceReturnType)
                If (Not Me.State.InvoiceReturnObject.InvoiceId.Equals(Guid.Empty)) Then
                    Me.State.MyBO = New Invoice(Me.State.InvoiceReturnObject.InvoiceId)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            Me.MasterPage.MessageController.Clear()
            moInfoMessageController.Clear()
            If (Not Me.IsPostBack) Then
                ' Date Calendars
                Me.AddCalendar_New(btnInvoiceDate, moInvoiceDate)
                Me.AddCalendar_New(btnDueDate, moDueDate)

                If (Me.State.MyBO Is Nothing) Then
                    Me.State.MyBO = New Invoice()
                End If

                ' Populate Bread Crum
                UpdateBreadCrum()

                ' Populate Drop Downs
                PopulateDropdowns()
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.AddCalendar_New(btnRepairDate, moRepairDate)
            Else
                If (Me.Request("__EVENTARGUMENT").Equals(SERVICE_CENTER_CHANGE_EVENT_NAME)) Then
                    UpdateServiceCenter()
                    For Each item As InvoiceItem In Me.State.MyBO.InvoiceItemChildren
                        item.Delete()
                    Next
                ElseIf (Me.Request("__EVENTARGUMENT").Equals(SELECT_ALL_EVENT_NAME)) Then
                    Dim oDv As Invoice.InvoiceAuthorizationSelectionView
                    oDv = Me.State.MyBO.GetInvoiceAuthorizationSelectionView()
                    Me.State.AvailableAuthorizationsCount = oDv.Count
                    If (Me.State.MyBO.ClaimAuthorizations.Count < Me.State.AvailableAuthorizationsCount) Then
                        For Each oDrv As DataRowView In oDv
                            Dim dr As DataRow
                            dr = oDrv.Row
                            If (DirectCast(dr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_IS_SELECTED), Boolean) = False) Then
                                Me.State.MyBO.AddAuthorization(New Guid(DirectCast(dr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_CLAIM_AUTHORIZATION_ID), Byte())))
                            End If
                        Next
                    Else
                        For Each oClaimAuthorization As ClaimAuthorization In
                            Me.State.MyBO.ClaimAuthorizations.Where(Function(item) item.ClaimAuthStatus = ClaimAuthorizationStatus.Authorized OrElse item.ClaimAuthStatus = ClaimAuthorizationStatus.Fulfilled)
                            AjaxState.MyBO.RemoveAuthorization(oClaimAuthorization.Id)
                        Next
                    End If
                End If

                'PopulateInvoiceItemGrid()
            End If
            If (Me.State.MyBO.ServiceCenterId = Guid.Empty) Then
                moInfoMessageController.AddInformation(SELECT_SERVICE_CENTER_TO_POPULATE_CLAIM_AUTHORIZATIONS)
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If
            'Me.DisplayNewProgressBarOnClick(Me.btnSearch, "Loading_Certificates")

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
#End Region

#Region "Methods"
    Private Sub PopulateDropdowns()
        Dim languageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim companyGroupId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        'BindListControlToDataView(moServiceClass, LookupListNew.GetServiceClassList(languageId))
        moServiceClass.Populate(CommonConfigManager.Current.ListManager.GetList("SVCCLASS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                {
                .AddBlankItem = True
            })
        ' BindListControlToDataView(moServiceType, LookupListNew.GetNewServiceTypeLookupList(languageId))
        moServiceType.Populate(CommonConfigManager.Current.ListManager.GetList("SVCTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                {
                .AddBlankItem = True
            })
        ' BindListControlToDataView(moRiskType, LookupListNew.GetRiskTypeLookupList(companyGroupId))
        Dim oListContext As New ListContext
        oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim riskList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RiskTypeByCompanyGroup", context:=oListContext)
        moRiskType.Populate(riskList, New PopulateOptions() With
                                           {
                                           .AddBlankItem = True
                                           })


        ' BindListControlToDataView(moEquipmentClass, LookupListNew.GetEquipmentClassLookupList(languageId))
        moEquipmentClass.Populate(CommonConfigManager.Current.ListManager.GetList("EQPCLS", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                {
                .AddBlankItem = True
            })
        ' BindListControlToDataView(moEquipment, LookupListNew.GetEquipmentLookupList(companyGroupId))
        oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
        Dim equipmentList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="EquipmentByCompanyGroup", context:=oListContext)
        moEquipment.Populate(equipmentList, New PopulateOptions() With
                                           {
                                           .AddBlankItem = True
                                           })
        ' BindListControlToDataView(moCondition, LookupListNew.GetConditionLookupList(languageId))
        moCondition.Populate(CommonConfigManager.Current.ListManager.GetList("TEQP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                {
                .AddBlankItem = True
            })
    End Sub

    Private Sub UpdateBreadCrum()
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGESUBTAB) & ElitaBase.Sperator
        Me.MasterPage.BreadCrum = Me.MasterPage.BreadCrum & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
    End Sub

    Private Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, "InvoiceNumber", Me.moInvoiceNumberLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "InvoiceDate", Me.moInvoiceDateLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "ServiceCenterId", Me.moServiceCenterLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DueDate", Me.moDueDateLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "InvoiceStatusId", Me.moInvoiceStatusLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "InvoiceAmount", Me.moInvoiceAmountLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "Source", Me.moSourceLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "DifferenceAmount", Me.moDifferenceAmountLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "IsComplete", Me.moIsCompleteLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PerceptionIIBB", Me.moPerceptionIibbLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PerceptionIIBBRegion", Me.moPerceptionIibbProvinceLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "PerceptionIVA", Me.moPerceptionIvaLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, "BatchNumber", Me.moBatchNumberLabel)
        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Private Sub PopulateFormFromBOs()
        With Me.State.MyBO
            PopulateInvoiceItemGrid()
            Me.PopulateControlFromBOProperty(Me.moInvoiceNumber, .InvoiceNumber)
            Me.PopulateControlFromBOProperty(Me.moInvoiceDate, .InvoiceDate)
            Me.inputServiceCenterId.Value = .ServiceCenterId.ToString()
            If (.ServiceCenter Is Nothing) Then
                Me.PopulateControlFromBOProperty(Me.moServiceCenter, String.Empty)
            Else
                Me.PopulateControlFromBOProperty(Me.moServiceCenter, .ServiceCenter.Description)
            End If
            Me.PopulateControlFromBOProperty(Me.moDueDate, .DueDate)
            Me.PopulateControlFromBOProperty(Me.moInvoiceStatus, LookupListNew.GetDescriptionFromId(LookupListNew.LK_INVOICE_STATUS, .InvoiceStatusId))
            Select Case .InvoiceStatusCode
                Case Codes.INVOICE_STATUS__BALANCED
                    Me.moInvoiceStatus.ForeColor = Color.FromArgb(51, 151, 0)
                Case Codes.INVOICE_STATUS__OVER, Codes.INVOICE_STATUS__UNDER
                    Me.moInvoiceStatus.ForeColor = Color.FromArgb(255, 0, 0)
                Case Else
                    Me.moInvoiceStatus.ForeColor = Color.FromArgb(51, 51, 51)
            End Select
            Me.PopulateControlFromBOProperty(Me.moInvoiceAmount, .InvoiceAmount)
            Me.PopulateControlFromBOProperty(Me.moDifferenceAmount, .DifferenceAmount)
            Me.PopulateControlFromBOProperty(Me.moSource, .Source)
            Me.PopulateControlFromBOProperty(Me.moIsComplete, LookupListNew.GetDescriptionFromId(LookupListNew.LK_YESNO, .IsCompleteId))
            Me.PopulateControlFromBOProperty(Me.moBatchNumber, .BatchNumber)
            Me.PopulateControlFromBOProperty(Me.moPerceptionIva, .PerceptionIVA)
            Me.PopulateControlFromBOProperty(Me.moPerceptionIibb, .PerceptionIIBB)
            PopulateRegionDropDown()
            Me.PopulateControlFromBOProperty(Me.moPerceptionIibbProvince, .PerceptionIIBBRegion)
        End With
    End Sub

    Private Sub PopulateBOsFormFrom()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "InvoiceNumber", Me.moInvoiceNumber)
            Me.PopulateBOProperty(Me.State.MyBO, "InvoiceDate", Me.moInvoiceDate)
            UpdateServiceCenter()
            Me.PopulateBOProperty(Me.State.MyBO, "DueDate", Me.moDueDate)
            Me.PopulateBOProperty(Me.State.MyBO, "InvoiceAmount", Me.moInvoiceAmount)
            Me.PopulateBOProperty(Me.State.MyBO, "PerceptionIIBB", Me.moPerceptionIibb)
            Me.PopulateBOProperty(Me.State.MyBO, "PerceptionIVA", Me.moPerceptionIva)
            Me.PopulateBOProperty(Me.State.MyBO, "PerceptionIIBBRegion", Me.moPerceptionIibbProvince)
            Me.PopulateBOProperty(Me.State.MyBO, "BatchNumber", Me.moBatchNumber)
        End With
    End Sub

    Private Sub UpdateServiceCenter()
        Dim serviceCenterId As Guid
        If (Me.inputServiceCenterId.Value.Trim() = String.Empty) Then
            serviceCenterId = Guid.Empty
        Else
            serviceCenterId = New Guid(Me.inputServiceCenterId.Value)
        End If
        If (Not serviceCenterId.Equals(Me.State.MyBO.ServiceCenterId)) Then
            Me.PopulateBOProperty(Me.State.MyBO, "ServiceCenterId", serviceCenterId)
            ' Re-Populate Region Drop Down
            PopulateRegionDropDown()
            EnableDisableFields()
        End If
    End Sub

    Private Sub PopulateRegionDropDown()
        Dim oRegionList As DataView
        If (Me.State.MyBO.ServiceCenter Is Nothing) Then
            oRegionList = LookupListNew.GetRegionLookupList(Guid.Empty)
        Else
            oRegionList = LookupListNew.GetRegionLookupList(Me.State.MyBO.ServiceCenter.CountryId)
        End If
        ElitaPlusPage.BindListControlToDataView(moPerceptionIibbProvince, oRegionList, , , True)
        moPerceptionIibbProvince.ClearSelection()
    End Sub

    Sub PopulateInvoiceItemGrid()
        Dim dv As Invoice.InvoiceAuthorizationSelectionView = Me.State.MyBO.GetInvoiceAuthorizationSelectionView()
        Me.State.AvailableAuthorizationsCount = dv.Count
        If (dv.Count = 0 AndAlso Me.State.MyBO.ServiceCenterId <> Guid.Empty) Then
            moInfoMessageController.AddInformation(SELECTED_SERVICE_CENTER_DO_NOT_HAVE_ANY_AUTHORIZATIONS)
        End If
        dv.Sort = Me.State.SortExpressionItem
        Me.moInvoiceRepeater.DataSource = dv
        Me.moInvoiceRepeater.DataBind()
    End Sub

    Private Sub EnableDisableFields()
        If Me.State.IsItemEditing OrElse Me.State.IsAttributeEditing Then
            EnableDisableParentControls(False)
        Else
            EnableDisableParentControls(True)
        End If
    End Sub

    Private Sub EnableDisableParentControls(ByVal enableToggle As Boolean)
        ControlMgr.SetEnableControl(Me, btnBack, enableToggle)
        ControlMgr.SetEnableControl(Me, btnUndoBalance_WRITE, Me.State.MyBO.UndoBalance.CanExecute)
        ControlMgr.SetEnableControl(Me, btnBalance_WRITE, Me.State.MyBO.Balance.CanExecute)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Me.State.MyBO.Delete.CanExecute)
        ControlMgr.SetEnableControl(Me, btnSave_WRITE, enableToggle)
        ControlMgr.SetEnableControl(Me, btnUndo_Write, enableToggle)

        Me.moInvoiceNumber.ReadOnly = Not (Me.State.MyBO.IsNew)
        Me.moInvoiceDate.ReadOnly = Not (Me.State.MyBO.IsNew)
        ControlMgr.SetVisibleControl(Me, btnInvoiceDate, Me.State.MyBO.IsNew)
        Me.moServiceCenter.ReadOnly = Not (Me.State.MyBO.IsNew)
        Me.moDueDate.ReadOnly = Not (Me.State.MyBO.IsNew)
        ControlMgr.SetVisibleControl(Me, btnDueDate, Me.State.MyBO.IsNew)
        Me.moInvoiceAmount.ReadOnly = Not (Me.State.MyBO.IsNew)

        If (Me.State.MyBO.IsPerceptionTaxDefined) Then
            trInvoiceTaxes1.Visible = True
            trInvoiceTaxes2.Visible = True
            ControlMgr.SetVisibleControl(Me, moPerceptionIvaLabel, True)
            ControlMgr.SetVisibleControl(Me, moPerceptionIva, True)
            ControlMgr.SetVisibleControl(Me, moPerceptionIibbLabel, True)
            ControlMgr.SetVisibleControl(Me, moPerceptionIibb, True)
            ControlMgr.SetVisibleControl(Me, moPerceptionIibbProvinceLabel, True)
            ControlMgr.SetVisibleControl(Me, moPerceptionIibbProvince, True)
            ControlMgr.SetVisibleControl(Me, moBatchNumber, True)
            ControlMgr.SetVisibleControl(Me, moBatchNumberLabel, True)

            If (Me.State.MyBO.IsAnyClaimAuthorizationPaid) Then
                moPerceptionIva.ReadOnly = True
                moPerceptionIibb.ReadOnly = True
                moBatchNumber.ReadOnly = True
                ControlMgr.SetEnableControl(Me, moPerceptionIibbProvince, False)
            Else
                moPerceptionIva.ReadOnly = False
                moPerceptionIibb.ReadOnly = False
                moBatchNumber.ReadOnly = False
                If (Me.State.MyBO.InvoiceItemChildren.Count > 0) Then
                    moBatchNumber.Attributes.Add("readOnly", "readOnly")
                End If
                ControlMgr.SetEnableControl(Me, moPerceptionIibbProvince, True)
            End If
        Else
            trInvoiceTaxes1.Visible = False
            trInvoiceTaxes2.Visible = False
            ControlMgr.SetVisibleControl(Me, moPerceptionIvaLabel, False)
            ControlMgr.SetVisibleControl(Me, moPerceptionIva, False)
            ControlMgr.SetVisibleControl(Me, moPerceptionIibbLabel, False)
            ControlMgr.SetVisibleControl(Me, moPerceptionIibb, False)
            ControlMgr.SetVisibleControl(Me, moPerceptionIibbLabel, False)
            ControlMgr.SetVisibleControl(Me, moPerceptionIibb, False)
            ControlMgr.SetVisibleControl(Me, moBatchNumber, False)
            ControlMgr.SetVisibleControl(Me, moBatchNumberLabel, False)
        End If

        If (enableToggle) Then
            'Enabled by Default
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)

            'Now disable depebding on the object state
            If Me.State.MyBO.IsNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnBalance_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnUndoBalance_WRITE, False)
            End If
            If (Me.State.MyBO.ReadOnly) Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnUndo_Write, False)
            End If
        End If
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Delete _
                AndAlso Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.Expire Then
                Me.State.MyBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New PageReturnType(Of InvoiceSearchForm.InvoiceReturnType)(ElitaPlusPage.DetailPageCommand.Back, Me.State.InvoiceReturnObject, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    Me.State.MyBO = New Invoice()
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New PageReturnType(Of InvoiceSearchForm.InvoiceReturnType)(ElitaPlusPage.DetailPageCommand.Back, Me.State.InvoiceReturnObject, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.State.MyBO.Delete.Execute()
                    Me.ReturnToCallingPage(New PageReturnType(Of InvoiceSearchForm.InvoiceReturnType)(ElitaPlusPage.DetailPageCommand.Back, Me.State.InvoiceReturnObject, Me.State.HasDataChanged))
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New PageReturnType(Of InvoiceSearchForm.InvoiceReturnType)(ElitaPlusPage.DetailPageCommand.Back, Me.State.InvoiceReturnObject, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.State.MyBO = New Invoice()
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
            End Select
        End If

        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = ""
    End Sub
#End Region

#Region "Events"
    Private Sub btnDelete_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAddRepairDate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRepairDate.Click
        Dim strRepairDt As String = moRepairDate.Text.Trim()
        Dim dtRepair As Date
        Dim blnValid As Boolean = True
        Try
            If (strRepairDt = String.Empty) Then
                Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_REPAIR_DATE_MISSING, True)
            End If
            If DateHelper.IsDate(strRepairDt) Then
                dtRepair = DateHelper.GetDateValue(strRepairDt)
                If dtRepair > Today Then
                    Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_REPAIR_DATE_ERR2, True)
                Else
                    For Each oClaimAuthorization As ClaimAuthorization In Me.State.MyBO.ClaimAuthorizations
                        If (oClaimAuthorization.RepairDate Is Nothing) Then
                            If (dtRepair < oClaimAuthorization.Claim.LossDate.Value) Then
                                Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_REPAIR_DATE_ERR2, True)
                                blnValid = False
                                Exit For
                            End If
                            If (Not oClaimAuthorization.PickUpDate Is Nothing) Then
                                If (dtRepair > oClaimAuthorization.PickUpDate.Value) Then
                                    Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR2, True)
                                    blnValid = False
                                    Exit For
                                End If
                            End If
                        End If
                    Next
                    If (blnValid) Then
                        For Each oClaimAuthorization As ClaimAuthorization In Me.State.MyBO.ClaimAuthorizations
                            If (oClaimAuthorization.RepairDate Is Nothing) Then
                                oClaimAuthorization.RepairDate = dtRepair
                            End If
                        Next
                        PopulateInvoiceItemGrid()
                    End If
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub moInvoiceRepeater_ItemCommand(ByVal sender As Object, ByVal e As RepeaterCommandEventArgs) Handles moInvoiceRepeater.ItemCommand
        Select Case e.CommandName
            Case SORT_COMMAND_NAME
                If Me.State.SortExpressionItem.StartsWith(e.CommandArgument.ToString()) Then
                    If Me.State.SortExpressionItem.StartsWith(e.CommandArgument.ToString() & " DESC") Then
                        Me.State.SortExpressionItem = e.CommandArgument.ToString()
                    Else
                        Me.State.SortExpressionItem = e.CommandArgument.ToString() & " DESC"
                    End If
                Else
                    Me.State.SortExpressionItem = e.CommandArgument.ToString()
                End If
                PopulateInvoiceItemGrid()
        End Select
    End Sub

    Protected Sub moInvoiceRepeater_ItemDataBound(ByVal sender As Object, ByVal e As RepeaterItemEventArgs) Handles moInvoiceRepeater.ItemDataBound
        Select Case e.Item.ItemType
            Case ListItemType.Item, ListItemType.AlternatingItem
                Dim authorizationDr As DataRow = DirectCast(e.Item.DataItem, System.Data.DataRowView).Row
                Dim moExpandCollapse As Label = DirectCast(e.Item.FindControl("moExpandCollapse"), Label)
                Dim moRepairDate As TextBox = DirectCast(e.Item.FindControl("moRepairDate"), TextBox)
                Dim btnRepairDate As ImageButton = DirectCast(e.Item.FindControl("btnRepairDate"), ImageButton)
                Dim moPickupDate As TextBox = DirectCast(e.Item.FindControl("moPickupDate"), TextBox)
                Dim btnPickupDate As ImageButton = DirectCast(e.Item.FindControl("btnPickupDate"), ImageButton)
                Dim moAddInvoiceLineItems As LinkButton = DirectCast(e.Item.FindControl("moAddInvoiceLineItems"), LinkButton)
                Dim oInvoiceAuthorizationAmount As Label = DirectCast(e.Item.FindControl("moInvoiceAuthorizationAmount"), Label)

                With moExpandCollapse
                    ' {0} - index, {1} claimAuthorizationId, {2} - invoiceAuthorizationAmountId
                    .Attributes.Add("onclick", String.Format("ShowHideInvoiceAuthorizationDetails(this, {0}, '{1}', '{2}');", e.Item.ItemIndex,
                                                             New Guid(CType(authorizationDr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_CLAIM_AUTHORIZATION_ID), Byte())).ToString(),
                                                             oInvoiceAuthorizationAmount.ClientID))
                    If (DirectCast(authorizationDr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_IS_SELECTED), Boolean)) Then
                        .Text = "+"
                    Else
                        moRepairDate.Attributes.Add("readOnly", "readOnly")
                        btnRepairDate.Attributes.Add("style", "display:none")
                        moPickupDate.Attributes.Add("readOnly", "readOnly")
                        btnPickupDate.Attributes.Add("style", "display:none")
                        moAddInvoiceLineItems.Attributes.Add("style", "display:none")
                    End If
                End With
                Dim authorizationId As Guid = New Guid(CType(authorizationDr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_CLAIM_AUTHORIZATION_ID), Byte()))
                Dim oClaimAuthorization As ClaimAuthorization
                With DirectCast(e.Item.FindControl("moSelect"), CheckBox)
                    ' {0} - index, {1} - claimAuthorizationId, {2} - expandId, {3} - repairDateId, {4} - repairDateImageId, {5} - pickupDateId, {6} - pickupDateImageId, {7} - moAddInvoiceLineItemsId
                    .Attributes.Add("onclick", String.Format("IncludeExcludeAuthorization(this, {0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}');",
                        e.Item.ItemIndex, authorizationId.ToString(), moExpandCollapse.ClientID, moRepairDate.ClientID, btnRepairDate.ClientID, moPickupDate.ClientID,
                        btnPickupDate.ClientID, moAddInvoiceLineItems.ClientID))
                    .Checked = DirectCast(authorizationDr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_IS_SELECTED), Boolean)
                    If (.Checked) Then
                        oClaimAuthorization = Me.State.MyBO.ClaimAuthorizations.Where(Function(item) item.Id = authorizationId).FirstOrDefault()
                        If (oClaimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.Reconsiled OrElse oClaimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.Paid OrElse
                            oClaimAuthorization.ClaimAuthStatus = ClaimAuthorizationStatus.ToBePaid) Then
                            .Enabled = False
                            btnRepairDate.Visible = False
                            btnPickupDate.Visible = False
                            moAddInvoiceLineItems.Visible = False
                            moRepairDate.ReadOnly = True
                            moPickupDate.ReadOnly = True
                        Else
                            .Enabled = True
                            btnRepairDate.Visible = True
                            btnPickupDate.Visible = True
                            moAddInvoiceLineItems.Visible = True
                            moRepairDate.ReadOnly = False
                            moPickupDate.ReadOnly = False
                        End If
                    End If
                End With
                With moAddInvoiceLineItems
                    .Attributes.Add("onclick", "javascript:return showAddInvoiceItemSearch('" & authorizationId.ToString() & "', '" & DirectCast(e.Item.FindControl("moExpandCollapse"), Label).ClientID & "', '" & e.Item.ItemIndex & "', '" & oInvoiceAuthorizationAmount.ClientID & "');")
                End With
                DirectCast(e.Item.FindControl("moClaimNumber"), Label).Text = authorizationDr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_CLAIM_NUMBER).ToString()
                DirectCast(e.Item.FindControl("moAuthorizationNumber"), Label).Text = authorizationDr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_AUTHORIZATION_NUMBER).ToString()
                DirectCast(e.Item.FindControl("moBatchNumber"), Label).Text = authorizationDr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_BATCH_NUMBER).ToString()
                DirectCast(e.Item.FindControl("moVerificationNumber"), Label).Text = authorizationDr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_SVC_REFERENCE_NUMBER).ToString()
                DirectCast(e.Item.FindControl("moCustomerName"), Label).Text = authorizationDr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_CUSTOMER_NAME).ToString()
                DirectCast(e.Item.FindControl("moReserveAmount"), Label).Text = GetAmountFormattedString(CType(authorizationDr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_RESERVE_AMOUNT), Decimal), Nothing)
                DirectCast(e.Item.FindControl("moDeductible"), Label).Text = GetAmountFormattedString(CType(authorizationDr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_DEDUCTIBLE), Decimal), Nothing)
                oInvoiceAuthorizationAmount.Text = GetAmountFormattedString(CType(authorizationDr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_INVOICE_AUTH_AMOUNT), Decimal), Nothing)
                If (Not authorizationDr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_REPAIR_DATE) Is DBNull.Value) Then
                    moRepairDate.Text = GetDateFormattedString(CType(authorizationDr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_REPAIR_DATE), Date))
                End If
                If (Not authorizationDr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_PICK_UP_DATE) Is DBNull.Value) Then
                    moPickupDate.Text = GetDateFormattedString(CType(authorizationDr(Invoice.InvoiceAuthorizationSelectionView.COL_NAME_PICK_UP_DATE), Date))
                End If
                Me.AddCalendar_New(btnRepairDate, moRepairDate)
                Me.AddCalendar_New(btnPickupDate, moPickupDate)
                Dim authOnChange As String = String.Format("UpdateAuthorization('{0}', '{1}', '{2}')", authorizationId.ToString(), moRepairDate.ClientID, moPickupDate.ClientID)
                moRepairDate.Attributes.Add("onchange", authOnChange)
                moPickupDate.Attributes.Add("onchange", authOnChange)
            Case ListItemType.Header
                Dim chkSelectAll As CheckBox
                chkSelectAll = DirectCast(e.Item.FindControl("moSelectAll"), CheckBox)
                If (Not chkSelectAll Is Nothing) Then
                    SelectAllCheckBoxId = chkSelectAll.ClientID
                    chkSelectAll.Checked = (Me.State.MyBO.ClaimAuthorizations.Count >= Me.State.AvailableAuthorizationsCount)
                End If
                HighLightSortColumn(DirectCast(e.Item.FindControl("moClaimNumberSort"), LinkButton), Me.State.SortExpressionItem, Invoice.InvoiceAuthorizationSelectionView.COL_NAME_CLAIM_NUMBER)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moAuthorizationNumberSort"), LinkButton), Me.State.SortExpressionItem, Invoice.InvoiceAuthorizationSelectionView.COL_NAME_AUTHORIZATION_NUMBER)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moBatchNumberSort"), LinkButton), Me.State.SortExpressionItem, Invoice.InvoiceAuthorizationSelectionView.COL_NAME_BATCH_NUMBER)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moVerificationNumberSort"), LinkButton), Me.State.SortExpressionItem, Invoice.InvoiceAuthorizationSelectionView.COL_NAME_SVC_REFERENCE_NUMBER)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moCustomerNameSort"), LinkButton), Me.State.SortExpressionItem, Invoice.InvoiceAuthorizationSelectionView.COL_NAME_CUSTOMER_NAME)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moReserveAmountSort"), LinkButton), Me.State.SortExpressionItem, Invoice.InvoiceAuthorizationSelectionView.COL_NAME_RESERVE_AMOUNT)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moDeductibleSort"), LinkButton), Me.State.SortExpressionItem, Invoice.InvoiceAuthorizationSelectionView.COL_NAME_DEDUCTIBLE)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moInvoiceAuthorizationAmountSort"), LinkButton), Me.State.SortExpressionItem, Invoice.InvoiceAuthorizationSelectionView.COL_NAME_INVOICE_AUTH_AMOUNT)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moRepairDateSort"), LinkButton), Me.State.SortExpressionItem, Invoice.InvoiceAuthorizationSelectionView.COL_NAME_REPAIR_DATE)
                HighLightSortColumn(DirectCast(e.Item.FindControl("moPickupDateSort"), LinkButton), Me.State.SortExpressionItem, Invoice.InvoiceAuthorizationSelectionView.COL_NAME_PICK_UP_DATE)
        End Select
    End Sub

    Protected Sub btnSave_WRITE_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsFamilyDirty Then
                Me.State.MyBO.Save()
                Me.State.HasDataChanged = True
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnBalance_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBalance_WRITE.Click
        Try
            If (Me.State.MyBO.Balance.CanExecute) Then
                Me.State.MyBO.Balance.Execute()
                EnableDisableFields()
                PopulateFormFromBOs()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndoBalance_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndoBalance_WRITE.Click
        Try
            If (Me.State.MyBO.UndoBalance.CanExecute) Then
                Me.State.MyBO.UndoBalance.Execute()
                EnableDisableFields()
                PopulateFormFromBOs()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub moSelectAllChecked(ByVal sender As Object, ByVal e As System.EventArgs)
        Try

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New Invoice(Me.State.MyBO.Id)
            Else
                Me.State.MyBO = New Invoice()
            End If
            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFormFrom()
            Me.State.InvoiceReturnObject.InvoiceId = Me.State.MyBO.Id
            If Me.State.MyBO.IsFamilyDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New PageReturnType(Of InvoiceSearchForm.InvoiceReturnType)(ElitaPlusPage.DetailPageCommand.Back, Me.State.InvoiceReturnObject, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFormFrom()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO_CANCEL, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.State.MyBO = New Invoice()
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moBatchNumber_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moBatchNumber.TextChanged
        If (Me.State.MyBO.InvoiceItemChildren.Count = 0) Then
            PopulateBOProperty(Me.State.MyBO, "BatchNumber", moBatchNumber)
        End If
        PopulateInvoiceItemGrid()
    End Sub

#End Region

#Region "Web Service Methods"
    <WebMethod(), ScriptMethod()>
    Public Shared Function PopulateServiceCenterDrop(ByVal prefixText As String, ByVal count As Integer) As String()
        Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries())
        Return AjaxController.BindAutoComplete(prefixText, dv)
    End Function

    <WebMethod(), ScriptMethod()>
    Public Shared Function GetInvoiceAuthorizationDetails(ByVal claimAuthorizationId As String, ByVal senderId As String, ByVal index As String, ByVal invoiceAuthorizationAmountId As String) As String
        Try
            Dim invoiceAuthorizationItems As Invoice.InvoiceAuthorizationItemSelectionView
            Dim ds As New DataSet
            ds.DataSetName = "InvoiceAuthorizationItemDs"

            Dim dt As New DataTable("InvoiceAuthorization")
            dt.Columns.Add("StatusCode", GetType(String))
            ds.Tables.Add(dt)
            dt.Rows.Add(New String() {AjaxState.MyBO.ClaimAuthorizations.Where(Function(item) item.Id = New Guid(claimAuthorizationId)).First().ClaimAuthorizationStatusCode})

            Dim dtHeaders As New DataTable("Headers")
            dtHeaders.Columns.Add("UI_PROG_CODE", GetType(String))
            dtHeaders.Columns.Add("TRANSLATION", GetType(String))
            ds.Tables.Add(dtHeaders)
            dtHeaders.Rows.Add(New String() {"SKU", TranslationBase.TranslateLabelOrMessage("SKU")})
            dtHeaders.Rows.Add(New String() {"DESCRIPTION", TranslationBase.TranslateLabelOrMessage("DESCRIPTION")})
            dtHeaders.Rows.Add(New String() {"SERVICE_CLASS", TranslationBase.TranslateLabelOrMessage("SERVICE_CLASS")})
            dtHeaders.Rows.Add(New String() {"SERVICE_TYPE", TranslationBase.TranslateLabelOrMessage("SERVICE_TYPE")})
            dtHeaders.Rows.Add(New String() {"AUTHORIZED_AMOUNT", TranslationBase.TranslateLabelOrMessage("AUTHORIZED_AMOUNT")})
            dtHeaders.Rows.Add(New String() {"AMOUNT", TranslationBase.TranslateLabelOrMessage("AMOUNT")})
            dtHeaders.Rows.Add(New String() {"RECONCILED_AMOUNT", TranslationBase.TranslateLabelOrMessage("RECONCILED_AMOUNT")})
            dtHeaders.Rows.Add(New String() {"SENDER_ID", senderId})
            dtHeaders.Rows.Add(New String() {"INDEX", index})
            dtHeaders.Rows.Add(New String() {"INVOICE_AUTH_AMOUNT_ID", invoiceAuthorizationAmountId})

            invoiceAuthorizationItems = AjaxState.MyBO.GetInvoiceAuthorizationItemSelectionView(New Guid(claimAuthorizationId))
            invoiceAuthorizationItems.Table.TableName = "InvoiceAuthorizationItem"
            ds.Tables.Add(invoiceAuthorizationItems.Table)

            Return ds.GetXml()
        Catch ex As Exception
            Return "Error!"
        End Try

    End Function

    <WebMethod(), ScriptMethod()>
    Public Shared Function IncludeAuthorization(ByVal claimAuthorizationId As String) As InvoiceAjaxResponse
        Dim returnValue As InvoiceAjaxResponse
        With AjaxState.MyBO
            .AddAuthorization(New Guid(claimAuthorizationId))
            returnValue.DifferenceAmount = .DifferenceAmount.Value
            returnValue.BatchNumberReadOnly = (.InvoiceItemChildren.Count > 0)
            returnValue.SelectAllChecked = (.ClaimAuthorizations.Count >= AjaxState.AvailableAuthorizationsCount)
        End With
        Return returnValue
    End Function

    <WebMethod(), ScriptMethod()>
    Public Shared Function ExcludeAuthorization(ByVal claimAuthorizationId As String) As InvoiceAjaxResponse
        Dim returnValue As InvoiceAjaxResponse
        With AjaxState.MyBO.ClaimAuthorizations.Where(Function(item) item.Id = New Guid(claimAuthorizationId)).First()
            AjaxState.MyBO.RemoveAuthorization(New Guid(claimAuthorizationId))

            returnValue.AuthorizationAmount = .AuthorizedAmount.Value
            If (.PickUpDate Is Nothing) Then
                returnValue.PickupDate = String.Empty
            Else
                returnValue.PickupDate = .PickUpDate.Value.ToString()
            End If
            If (.RepairDate Is Nothing) Then
                returnValue.RepairDate = String.Empty
            Else
                returnValue.RepairDate = .RepairDate.Value.ToString()
            End If
        End With
        With AjaxState.MyBO
            returnValue.DifferenceAmount = .DifferenceAmount.Value
            returnValue.BatchNumberReadOnly = (.InvoiceItemChildren.Count > 0)
            returnValue.SelectAllChecked = (.ClaimAuthorizations.Count >= AjaxState.AvailableAuthorizationsCount)
        End With
        Return returnValue
    End Function

    <WebMethod(), ScriptMethod()>
    Public Shared Function GetAddInvoiceItemSearchResults(ByVal claimAuthorizationId As String, ByVal serviceClassId As String, ByVal serviceTypeId As String, ByVal riskTypeId As String,
        ByVal equipmentClassId As String, ByVal equipmentId As String, ByVal conditionId As String, ByVal sku As String, ByVal skuDescription As String) As PriceListSearchResponse
        Dim claimAuthorization As ClaimAuthorization
        Dim oPriceListSearchResponse As PriceListSearchResponse
        Dim ds As DataSet
        claimAuthorization = (From ca As ClaimAuthorization In AjaxState.MyBO.ClaimAuthorizations() Where ca.Id = New Guid(claimAuthorizationId)).FirstOrDefault()
        ds = claimAuthorization.GetPriceListDetails(New Guid(serviceClassId), New Guid(serviceTypeId), New Guid(riskTypeId), New Guid(equipmentClassId), New Guid(equipmentId), New Guid(conditionId), sku, skuDescription)
        ds.DataSetName = "PriceListDs"

        Dim dtHeaders As New DataTable("Headers")
        dtHeaders.Columns.Add("UI_PROG_CODE", GetType(String))
        dtHeaders.Columns.Add("TRANSLATION", GetType(String))
        ds.Tables.Add(dtHeaders)
        dtHeaders.Rows.Add(New String() {"VENDOR_SKU", TranslationBase.TranslateLabelOrMessage("VENDOR_SKU")})
        dtHeaders.Rows.Add(New String() {"VENDOR_SKU_DESCRIPTION", TranslationBase.TranslateLabelOrMessage("VENDOR_SKU_DESCRIPTION")})
        dtHeaders.Rows.Add(New String() {"SERVICE_CLASS", TranslationBase.TranslateLabelOrMessage("SERVICE_CLASS")})
        dtHeaders.Rows.Add(New String() {"SERVICE_TYPE", TranslationBase.TranslateLabelOrMessage("SERVICE_TYPE")})
        dtHeaders.Rows.Add(New String() {"RISK_TYPE", TranslationBase.TranslateLabelOrMessage("RISK_TYPE")})
        dtHeaders.Rows.Add(New String() {"EQUIPMENT_CLASS", TranslationBase.TranslateLabelOrMessage("EQUIPMENT_CLASS")})
        dtHeaders.Rows.Add(New String() {"EQUIPMENT", TranslationBase.TranslateLabelOrMessage("EQUIPMENT")})
        dtHeaders.Rows.Add(New String() {"CONDITION", TranslationBase.TranslateLabelOrMessage("CONDITION")})
        dtHeaders.Rows.Add(New String() {"PRICE", TranslationBase.TranslateLabelOrMessage("PRICE")})
        dtHeaders.Rows.Add(New String() {"MSG_RECORDS_FOUND", TranslationBase.TranslateLabelOrMessage("MSG_RECORDS_FOUND")})

        If (ds.Tables.Contains(DALObjects.PriceListDetailDAL.TABLE_NAME)) Then
            If (ds.Tables(DALObjects.PriceListDetailDAL.TABLE_NAME).Rows.Count = 0) Then
                oPriceListSearchResponse.message = TranslationBase.TranslateLabelOrMessage(Message.MSG_NO_RECORDS_FOUND)
            ElseIf (ds.Tables(DALObjects.PriceListDetailDAL.TABLE_NAME).Rows.Count = 100) Then
                oPriceListSearchResponse.message = TranslationBase.TranslateLabelOrMessage(Message.MSG_MAX_LIMIT_EXCEEDED_REFINE_SEARCH_CRITERIA)
            End If
            ds.Tables(DALObjects.PriceListDetailDAL.TABLE_NAME).TableName = "PriceListDetail"
        End If

        oPriceListSearchResponse.xml = ds.GetXml()

        Return oPriceListSearchResponse
    End Function

    <WebMethod(), ScriptMethod()>
    Public Shared Function AddInvoiceLineItem(ByVal claimAuthorizationId As String, ByVal serviceClassId As String, ByVal serviceTypeId As String, ByVal sku As String, ByVal skuDescription As String, ByVal price As String) As InvoiceAjaxResponse
        Dim returnValue As InvoiceAjaxResponse
        With AjaxState.MyBO.GetNewInvoiceItemChild()
            .ClaimAuthorizationId = New Guid(claimAuthorizationId)
            .ServiceClassId = New Guid(GuidControl.HexToByteArray(serviceClassId))
            .ServiceTypeId = New Guid(GuidControl.HexToByteArray(serviceTypeId))
            .VendorSku = sku
            .LineItemNumber = AjaxState.MyBO.InvoiceItemChildren.Count()
            .VendorSkuDescription = skuDescription
            .Amount = Decimal.Parse(price)
            .Save()
        End With
        returnValue.DifferenceAmount = AjaxState.MyBO.DifferenceAmount.Value
        returnValue.ClaimAuthorizationId = New Guid(claimAuthorizationId).ToString()
        returnValue.AuthorizationAmount = (From item As InvoiceItem In AjaxState.MyBO.InvoiceItemChildren Where item.ClaimAuthorizationId = New Guid(claimAuthorizationId) Select item.Amount.Value).Sum()
        Return returnValue
    End Function

    <WebMethod(), ScriptMethod()>
    Public Shared Function RemoveInvoiceLineItem(ByVal invoiceItemId As String) As InvoiceAjaxResponse
        Dim returnValue As InvoiceAjaxResponse
        Dim oInvoiceItem As InvoiceItem
        Dim oClaimAuthorizationId As Guid
        oInvoiceItem = AjaxState.MyBO.GetInvoiceItemChild(New Guid(GuidControl.HexToByteArray(invoiceItemId)))
        oClaimAuthorizationId = oInvoiceItem.ClaimAuthorizationId
        returnValue.ClaimAuthorizationId = oClaimAuthorizationId.ToString()
        oInvoiceItem.Delete()
        returnValue.DifferenceAmount = AjaxState.MyBO.DifferenceAmount.Value
        returnValue.AuthorizationAmount = (From item As InvoiceItem In AjaxState.MyBO.InvoiceItemChildren Where item.ClaimAuthorizationId = oClaimAuthorizationId Select item.Amount.Value).Sum()
        Return returnValue
    End Function

    <WebMethod(), ScriptMethod()>
    Public Shared Function UpdateInvoice(ByVal invoiceAmount As String, ByVal perceptionIvaAmount As String, ByVal perceptionIibbAmount As String) As InvoiceAjaxResponse
        Dim returnValue As InvoiceAjaxResponse
        With AjaxState.MyBO
            .InvoiceAmount = Decimal.Parse(invoiceAmount)

            If (.IsPerceptionTaxDefined) Then
                .PerceptionIVA = Decimal.Parse(perceptionIvaAmount)
                .PerceptionIIBB = Decimal.Parse(perceptionIibbAmount)
            End If
            returnValue.DifferenceAmount = .DifferenceAmount.Value
        End With
        Return returnValue
    End Function

    <WebMethod(), ScriptMethod()>
    Public Shared Sub UpdateAuthorization(ByVal claimAuthorizationId As String, ByVal repairDate As String, ByVal pickupDate As String)
        With AjaxState.MyBO.ClaimAuthorizations.Where(Function(item) item.Id = New Guid(claimAuthorizationId)).First()
            If (repairDate <> String.Empty) Then .RepairDate = DateTime.Parse(repairDate) Else .RepairDate = Nothing
            If (pickupDate <> String.Empty) Then .PickUpDate = DateTime.Parse(pickupDate) Else .PickUpDate = Nothing
            .Save()
        End With
    End Sub

    <WebMethod(), ScriptMethod()>
    Public Shared Function UpdateInvoiceItem(ByVal invoiceItemId As String, ByVal lineItemAmount As String) As InvoiceAjaxResponse
        Dim returnValue As InvoiceAjaxResponse
        With AjaxState.MyBO.InvoiceItemChildren.Where(Function(item) item.Id = New Guid(GuidControl.HexToByteArray(invoiceItemId))).First()
            .Amount = Decimal.Parse(lineItemAmount)
            .Save()
            returnValue.AuthorizationAmount = (From item As InvoiceItem In AjaxState.MyBO.InvoiceItemChildren Where item.ClaimAuthorizationId = .ClaimAuthorizationId Select item.Amount.Value).Sum()
        End With
        returnValue.DifferenceAmount = AjaxState.MyBO.DifferenceAmount.Value
        Return returnValue
    End Function

    Public Structure PriceListSearchResponse
        Public xml As String
        Public message As String
    End Structure

    Public Structure InvoiceAjaxResponse
        Public DifferenceAmount As Decimal
        Public AuthorizationAmount As Decimal
        Public ClaimAuthorizationId As String
        Public PickupDate As String
        Public RepairDate As String
        Public BatchNumberReadOnly As Boolean
        Public SelectAllChecked As Boolean
    End Structure

    Private Sub mosearch_Click(sender As Object, e As EventArgs) Handles mosearch.Click
        Dim serviceCenterId As Guid
        If (Me.inputServiceCenterId.Value.Trim() = String.Empty) Then
            serviceCenterId = Guid.Empty
        Else
            serviceCenterId = New Guid(Me.inputServiceCenterId.Value)
        End If
        If (Not serviceCenterId.Equals(Me.State.MyBO.ServiceCenterId)) Then
            Me.PopulateBOProperty(Me.State.MyBO, "ServiceCenterId", serviceCenterId)
        End If

        PopulateInvoiceItemGrid()
    End Sub
#End Region

End Class
