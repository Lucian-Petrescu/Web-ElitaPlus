Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Tables
    Partial Class SpecialServiceForm
        Inherits ElitaPlusPage
#Region "Page State"

#Region "MyState"

        Class MyState
            Public MyBO As SpecialService
            Public moSpecialServiceId As Guid = Guid.Empty
            Public searchDV As CoverageLoss.SearchDV = Nothing
            Public CoverageTypeId As Guid = Guid.Empty
            Public IsSpecialServiceNew As Boolean = False
            Public oDealer As Dealer
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public LastOperation As DetailPageCommand = DetailPageCommand.Nothing_
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
            'Me.State.moProductCodeId = CType(Me.CallingParameters, Guid)
            If Me.State.moSpecialServiceId.Equals(Guid.Empty) Then
                Me.State.IsSpecialServiceNew = True
                ClearAll()
                SetButtonsState(True)
            Else
                Me.State.IsSpecialServiceNew = False
                SetButtonsState(False)
            End If
            PopulateAll()
        End Sub

#End Region

#Region "Constants"

        Private Const SPECIALSERVICE_FORM001 As String = "SPECIALSERVICE_FORM001" ' Maintain ProductCode Fetch Exception
        Private Const SPECIALSERVICE_FORM002 As String = "SPECIALSERVICE_FORM002" ' Maintain ProductCode Update Exception

        Private Const CONFIRM_MSG As String = "MGS_CONFIRM_PROMPT" '"Are you sure you want to delete the selected records?"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK" '"The record has been successfully saved"
        Private Const MSG_UNIQUE_VIOLATION As String = "MSG_DUPLICATE_KEY_CONSTRAINT_VIOLATED" '"Unique value is in use"
        Private Const UNIQUE_VIOLATION As String = "unique constraint"

        'Private Const PRODUCTCODE_LIST As String = "ProductCodeSearchForm.aspx"
        Public Const URL As String = "SpecialServiceForm.aspx"
        'Public Const URL1 As String = "ProductPriceRangeByRepairMethod.aspx"
        ' Property Name
        Public Const SPECIAL_SERVICE_PROPERTY As String = "SpecialService"
        Public Const DEALER_ID_PROPERTY As String = "DealerId"
        Public Const SPECIAL_SERVICE_CODE_PROPERTY As String = "Code"
        Public Const SPECIAL_SERVICE_DESC_PROPERTY As String = "Description"
        Public Const COVERAGE_TYPE_ID_PROPERTY As String = "CoverageTypeId"
        Public Const ADD_ITEMS_ALLOWED_ID_PROPERTY As String = "AddItemAllowed"
        Public Const CAUSE_OF_LOSS_ID_PROPERTY As String = "CauseOfLossId"
        Public Const ADD_ITEMS_AFTER_CERT_EXPIRED As String = "AddItemAfterExpired"
        Public Const OCCURENCES_ALLOWED_PROPERTY As String = "AllowedOccurrencesId"
        Public Const AUTHORIZED_AMOUNT_FROM_PROPERTY As String = "PriceGroupFieldId"
        Public Const AVAIL_SVC_CENTER_PROPERTY As String = "AvailableForServCenterId"
        Public Const COMBINED_REPAIR_PROPERTY As String = "CombinedWithRepair"
        Public Const SERVICE_CLASS_ID_PROPERTY As String = "ServiceClassId"
        Public Const SERVICE_TYPE_ID_PROPERTY As String = "ServiceTypeId"

        Private Const LABEL_SELECT_SPECIALSERVICE As String = "SPECIAL_SERVICE"
        Private Const FIRST_POS As Integer = 0
        Private Const COL_NAME As String = "ID"

        Private Const LABEL_DEALER As String = "DEALER_NAME"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Public Const PRICEGROUP_SPL_SVC_MANUAL As String = "M"
        Public Const PRICEGROUP_SPL_SVC_PRICE_LIST As String = "P"
        Public Const SERVICE_CLASS_TYPE_CAPTION As String = "Authorized_Amount_From"
#End Region

#Region "Variables"

#End Region

#Region "Attributes"

        Private moSpecialService As SpecialService

#End Region

#Region "Properties"

        Private ReadOnly Property TheSpecialService() As SpecialService
            Get
                If moSpecialService Is Nothing Then
                    If Me.State.IsSpecialServiceNew = True Then
                        ' For creating, inserting
                        moSpecialService = New SpecialService
                        Me.State.moSpecialServiceId = moSpecialService.Id
                    Else
                        ' For updating, deleting
                        '  Dim oProductCodeId As Guid = Me.GetGuidFromString(Me.State.moProductCodeId)
                        moSpecialService = New SpecialService(Me.State.moSpecialServiceId)
                        Me.State.oDealer = New Dealer(moSpecialService.DealerId)
                    End If
                End If

                Return moSpecialService
            End Get
        End Property

        Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl_New
            Get
                If multipleDropControl Is Nothing Then
                    multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl_New)
                End If
                Return multipleDropControl
            End Get
        End Property

        Public ReadOnly Property TheServiceClassTypeControl() As Interfaces.ServiceClassServiceTypeControl
            Get
                If ServiceClassServiceTypeControl Is Nothing Then
                    ServiceClassServiceTypeControl = CType(FindControl("ServiceClassServiceTypeControl"), Interfaces.ServiceClassServiceTypeControl)
                End If
                Return ServiceClassServiceTypeControl
            End Get
        End Property

#End Region

#Region "Handlers"

#Region "Handlers-Init"

        '        Protected WithEvents moErrorController As ErrorController

        Protected WithEvents moProductCodeMultipleDrop As MultipleColumnDDLabelControl_New
        'Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl
        Protected WithEvents moProductCodeDrop As System.Web.UI.WebControls.DropDownList

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub


        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn
            Dim retObj As ProductPriceRangeByRepairMethod.ReturnType = CType(ReturnPar, ProductPriceRangeByRepairMethod.ReturnType)
            Me.State.moSpecialServiceId = retObj.EditingId
            Me.SetStateProperties()
            'Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, _
            'Me.MSG_TYPE_CONFIRM, True)
            Me.State.LastOperation = DetailPageCommand.Redirect_
        End Sub

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Me.State.moSpecialServiceId = CType(CType(Me.CallingParameters, MyState).moSpecialServiceId, Guid)
                    Me.State.CoverageTypeId = CType(CType(Me.CallingParameters, MyState).CoverageTypeId, Guid)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                'moErrorController.Clear_Hide()
                Me.MasterPage.MessageController.Clear()
                ClearLabelsErrSign()
                'Setting the bread crum navigation
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("TABLES")
                Me.UpdateBreadCrum()


                If Not Page.IsPostBack Then
                    Me.SetStateProperties()
                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO,
                                                                        Me.MSG_TYPE_CONFIRM, True)
                End If

                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()
                If Not Me.IsPostBack Then
                    'BindBoPropertiesToLabels()
                    Me.AddLabelDecorations(TheSpecialService)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
                'moErrorController.Clear_Hide()
                '              ClearLabelsErrSign()
                Me.State.LastOperation = DetailPageCommand.Nothing_
            Else
                Me.ShowMissingTranslations(Me.MasterPage.MessageController)
            End If
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnApply_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
            ApplyChanges()
        End Sub

        Private Sub GoBack()
            Dim retType As New SpecialServiceListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                              Me.State.moSpecialServiceId, Me.State.boChanged)
            Me.ReturnToCallingPage(retType)
            'Me.callPage(ProductCodeForm.PRODUCTCODE_LIST, param)

        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM,
                                                Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr

            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                PopulateAll()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNew()
            Me.State.moSpecialServiceId = Guid.Empty
            Me.State.IsSpecialServiceNew = True
            ClearAll()
            'EnableDisableFields()
            Me.SetButtonsState(True)
            Me.PopulateAll()
            TheDealerControl.ChangeEnabledControlProperty(True)
        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNewCopy()
            Me.State.moSpecialServiceId = Guid.Empty
            Me.State.IsSpecialServiceNew = True
            ClearTexts()
            EnableDisableFields()
            PopulateUserControlAvailableSelectedProductCodes()
            Me.SetButtonsState(True)
            TheDealerControl.ChangeEnabledControlProperty(True)
        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteSpecialService() = True Then
                    Me.State.boChanged = True
                    'Dim param As New ProductCodeSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, _
                    '                Me.State.moProductCodeId)
                    'param.BoChanged = True
                    'Me.callPage(ProductCodeForm.PRODUCTCODE_LIST, param)
                    Dim retType As New SpecialServiceListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                              Me.State.moSpecialServiceId, Me.State.boChanged)
                    retType.BoChanged = True
                    Me.ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As MultipleColumnDDLabelControl_New) _
                        Handles multipleDropControl.SelectedDropChanged
            Try
                PopulateUserControlAvailableSelectedProductCodes()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moCoverageTypeDrop_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCoverageTypeDrop.SelectedIndexChanged
            Dim oCovloss As CoverageLoss
            Dim ds As DataSet
            If cboCoverageTypeDrop.SelectedIndex > BLANK_ITEM_SELECTED Then
                ControlMgr.SetEnableControl(Me, cboCauseOfLossDrop, True)

                'BindListControlToDataView(cboCauseOfLossDrop, New DataView(CoverageLoss.LoadCauseOfLossByCov(GetSelectedItem(cboCoverageTypeDrop)).Tables(0)), , , True) '
                Dim listcontext As ListContext = New ListContext()
                listcontext.CoverageTypeId = GetSelectedItem(cboCoverageTypeDrop)
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                listcontext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                'listcontext.DealerId and prod_code  =should be optional
                Dim causeofloss As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.CauseOfLossByCoverageTypeAndSplSvcLookupList, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                cboCauseOfLossDrop.Populate(causeofloss, New PopulateOptions() With
                {
               .AddBlankItem = True
               })
            Else
                cboCauseOfLossDrop.SelectedIndex = BLANK_ITEM_SELECTED
                ControlMgr.SetEnableControl(Me, cboCauseOfLossDrop, False)
            End If
        End Sub

#End Region

#End Region

#Region "Clear"

        Private Sub ClearTexts()
            SplServiceCodeText.Text = String.Empty
            SplServiceDescText.Text = String.Empty
        End Sub

        Private Sub ClearAll()
            ClearTexts()
            TheDealerControl.ClearMultipleDrop()
            ClearList(cboCoverageTypeDrop)
            ClearList(cboAddItemsAllowedDrop)
            ClearList(cboCauseOfLossDrop)
            ClearList(cboAddItemsCertExpDrop)
            ClearList(cboOccurAllowedDrop)
            ClearList(cboAuthAmtFromDrop)
            ClearList(cboAvailSvcCenterDrop)
            ClearList(cboRepairCombinedDrop)
        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateDealer()
            Dim oCompanyList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Try
                Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyList, True)
                TheDealerControl.SetControl(True, TheDealerControl.MODES.NEW_MODE, True, dv, "*" + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_SPECIALSERVICE), True, True)
                If Me.State.IsSpecialServiceNew = True Then
                    TheDealerControl.SelectedGuid = Guid.Empty
                    TheDealerControl.ChangeEnabledControlProperty(True)
                Else
                    TheDealerControl.ChangeEnabledControlProperty(False)
                    TheDealerControl.SelectedGuid = TheSpecialService.DealerId
                End If
            Catch ex As Exception
                'moErrorController.AddError(SPECIALSERVICE_FORM001)
                'moErrorController.AddError(ex.Message, False)
                'moErrorController.Show()
                Me.MasterPage.ErrController.AddErrorAndShow(ex.Message, False)
            End Try
        End Sub

        Private Sub PopulateServiceClassType()
            Try
                Dim svcclassTypeDisabled As Boolean = False
                If Not cboAuthAmtFromDrop.SelectedItem Is Nothing Then
                    If (cboAuthAmtFromDrop.SelectedItem.Text = "Manual") Then
                        svcclassTypeDisabled = True
                    End If
                End If

                TheServiceClassTypeControl.SetControl(True, TheServiceClassTypeControl.MODES.NEW_MODE, True, , Me.SERVICE_CLASS_TYPE_CAPTION, True, True, , , , , , , svcclassTypeDisabled)
                If Me.State.IsSpecialServiceNew = True Then
                    TheServiceClassTypeControl.SpecialServiceGuid = Guid.Empty
                    'TheServiceClassTypeControl.ChangeEnabledControlProperty(True)
                Else
                    'TheServiceClassTypeControl.ChangeEnabledControlProperty(False)
                    TheServiceClassTypeControl.SpecialServiceGuid = TheSpecialService.Id
                    TheServiceClassTypeControl.ServiceClassGuid = TheSpecialService.ServiceClassId
                    TheServiceClassTypeControl.ServiceTypeGuid = TheSpecialService.ServiceTypeId
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub PopulateDropDowns()
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim oVSCPlanview As DataView
            Dim oCoverageType As CoverageType

            Try
                ' Me.BindListControlToDataView(cboCoverageTypeDrop, LookupListNew.GetCoverageTypeByCompanyGroupLookupList(oLanguageId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, False), , , True) 'CoverageTypeByCompanyGroup
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

                Dim covTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CoverageTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                cboCoverageTypeDrop.Populate(covTypeLkl, New PopulateOptions() With
               {
                .AddBlankItem = True
               })
                ' BindListControlToDataView(cboAddItemsAllowedDrop, LookupListNew.GetYesNoLookupList(oLanguageId), , , True)
                cboAddItemsAllowedDrop.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
               {
                .AddBlankItem = True
               })
                ' If Not Me.State.IsSpecialServiceNew Then
                ' BindListControlToDataView(cboCauseOfLossDrop, LookupListNew.GetCauseOfLossLookupList(oLanguageId), , , True) 'CAUSE
                cboCauseOfLossDrop.Populate(CommonConfigManager.Current.ListManager.GetList("CAUSE", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
               {
                .AddBlankItem = True
               })
                'End If

                ' BindListControlToDataView(cboAddItemsCertExpDrop, LookupListNew.GetYesNoLookupList(oLanguageId), , , True)
                cboAddItemsCertExpDrop.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
               {
                .AddBlankItem = True
               })
                ' BindListControlToDataView(cboOccurAllowedDrop, LookupListNew.GetOccurancesAllowedLookupList(oLanguageId), , , True) 'OCCRALWD
                cboOccurAllowedDrop.Populate(CommonConfigManager.Current.ListManager.GetList("OCCRALWD", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
               {
                .AddBlankItem = True
               })
                'BindListControlToDataView(cboAuthAmtFromDrop, LookupListNew.GetPriceGroupDPLookupList(oLanguageId), , , True)
                'BindListControlToDataView(cboAuthAmtFromDrop, SpecialService.getPriceGroupsList(oLanguageId).Tables(0).DefaultView) 'PRCGROUP
                cboAuthAmtFromDrop.Populate(CommonConfigManager.Current.ListManager.GetList("PRCGROUP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
               {
                .AddBlankItem = True
               })

                'BindListControlToDataView(cboAvailSvcCenterDrop, LookupListNew.GetYesNoLookupList(oLanguageId), , , True)
                cboAvailSvcCenterDrop.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
               {
                .AddBlankItem = True
               })
                'BindListControlToDataView(cboRepairCombinedDrop, LookupListNew.GetYesNoLookupList(oLanguageId), , , True)
                cboRepairCombinedDrop.Populate(CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
              {
               .AddBlankItem = True
              })
            Catch ex As Exception
                'moErrorController.AddError(SPECIALSERVICE_FORM001)
                'moErrorController.AddError(ex.Message, False)
                'moErrorController.Show()
                Me.MasterPage.ErrController.AddErrorAndShow(ex.Message, False)
            End Try
        End Sub

        Private Sub Populatecauseoflossdropdown()
            ' BindListControlToDataView(cboCauseOfLossDrop, New DataView(CoverageLoss.LoadCauseOfLossByCov(GetSelectedItem(cboCoverageTypeDrop)).Tables(0)), , , True) '
            Dim listcontext As ListContext = New ListContext()
            listcontext.CoverageTypeId = GetSelectedItem(cboCoverageTypeDrop)
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            listcontext.LanguageId = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            'listcontext.DealerId =should be optional
            Dim causeofloss As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.CauseOfLossByCoverageTypeAndSplSvcLookupList, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            cboCauseOfLossDrop.Populate(causeofloss, New PopulateOptions() With
           {
           .AddBlankItem = True
           })
        End Sub

        Sub PopulateUserControlAvailableSelectedProductCodes()
            'Me.UserControlAvailableSelectedProductCodes.BackColor = "#d5d6e4"
            ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedProductCodes, False)
            With TheSpecialService
                If Not .Id.Equals(Guid.Empty) Then
                    Dim availableDv As DataView = .GetAvailableProductCodes(TheDealerControl.SelectedGuid)
                    Dim selectedDv As DataView = .GetSelectedProductCodes(TheDealerControl.SelectedGuid)
                    Me.UserControlAvailableSelectedProductCodes.SetAvailableData(availableDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                    Me.UserControlAvailableSelectedProductCodes.SetSelectedData(selectedDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                    ControlMgr.SetVisibleControl(Me, UserControlAvailableSelectedProductCodes, True)
                End If
            End With

        End Sub

        Private Sub PopulateTexts()
            '  ClearAll()
            Dim oCovloss As CoverageLoss
            Dim CauseOfLossId As Guid
            Try
                With TheSpecialService
                    If Me.State.IsSpecialServiceNew = True Then

                        BindSelectItem(Nothing, cboCoverageTypeDrop)
                        BindSelectItem(Nothing, cboCauseOfLossDrop)
                        BindSelectItem(Nothing, cboOccurAllowedDrop)
                        BindSelectItem(Nothing, cboAuthAmtFromDrop)
                        BindSelectItem(Nothing, cboRepairCombinedDrop)

                        Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
                        Me.SetSelectedItem(Me.cboAddItemsAllowedDrop, noId)
                        Me.SetSelectedItem(Me.cboAddItemsCertExpDrop, noId)
                        Me.SetSelectedItem(Me.cboAvailSvcCenterDrop, noId)
                        SplServiceCodeText.Text = String.Empty
                        SplServiceDescText.Text = String.Empty

                    Else

                        'Me.SetSelectedItem(Me.moCoverageTypeDrop, Me.State.CoverageTypeId)
                        BindSelectItem(.AddItemAllowed.ToString, cboAddItemsAllowedDrop)
                        oCovloss = New CoverageLoss(.CoverageLossId)
                        Me.SetSelectedItem(Me.cboCauseOfLossDrop, oCovloss.CauseOfLossId)
                        Me.SetSelectedItem(Me.cboCoverageTypeDrop, oCovloss.CoverageTypeId)
                        'BindSelectItem(.CoverageLossId.ToString, moCauseOfLossDrop)
                        BindSelectItem(.AddItemAfterExpired.ToString, cboAddItemsCertExpDrop)
                        BindSelectItem(.AllowedOccurrencesId.ToString, cboOccurAllowedDrop)
                        BindSelectItem(.PriceGroupFieldId.ToString, cboAuthAmtFromDrop)
                        BindSelectItem(.AvailableForServCenterId.ToString, cboAvailSvcCenterDrop)
                        BindSelectItem(.CombinedWithRepair.ToString, cboRepairCombinedDrop)
                        SplServiceCodeText.Text = .Code
                        SplServiceDescText.Text = .Description

                    End If

                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateAll()
            If Me.State.IsSpecialServiceNew = True Then
                PopulateDropDowns()
                PopulateDealer()
                'PopulateServiceClassType()
                PopulateUserControlAvailableSelectedProductCodes()
                PopulateTexts()
                PopulateServiceClassType()
                EnableDisableFields()
            Else
                ClearAll()
                PopulateDealer()
                PopulateDropDowns()
                PopulateUserControlAvailableSelectedProductCodes()
                PopulateTexts()
                PopulateServiceClassType()
                EnableDisableFields()
            End If
        End Sub

        Protected Sub PopulateBOsFromForm()

            Dim oCovloss As CoverageLoss
            Dim ds As DataSet
            Dim CoverageLossId As Guid
            Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
            With Me.TheSpecialService
                .DealerId = TheDealerControl.SelectedGuid
                'Me.PopulateBOProperty(TheProductCode, "DealerId", Me.moDealerDrop)
                Me.PopulateBOProperty(TheSpecialService, "AddItemAllowed", Me.cboAddItemsAllowedDrop)
                If cboCauseOfLossDrop.SelectedIndex > BLANK_ITEM_SELECTED Then
                    ds = CoverageLoss.LoadSelectedCovLossFromCovandCauseOfLoss(GetSelectedItem(cboCauseOfLossDrop), GetSelectedItem(cboCoverageTypeDrop))
                    If ds.Tables(0).Rows.Count > 0 Then
                        CoverageLossId = GuidControl.ByteArrayToGuid(ds.Tables(0).Rows(0)(CoverageLoss.SearchDV.COL_NAME_COVERAGE_LOSS_ID))
                    End If
                End If
                Me.PopulateBOProperty(TheSpecialService, "CoverageLossId", CoverageLossId)
                Me.PopulateBOProperty(TheSpecialService, "AddItemAfterExpired", Me.cboAddItemsCertExpDrop)
                Me.PopulateBOProperty(TheSpecialService, "AllowedOccurrencesId", Me.cboOccurAllowedDrop)
                Me.PopulateBOProperty(TheSpecialService, "PriceGroupFieldId", Me.cboAuthAmtFromDrop, )
                Me.PopulateBOProperty(TheSpecialService, "AvailableForServCenterId", Me.cboAvailSvcCenterDrop)
                Me.PopulateBOProperty(TheSpecialService, "CombinedWithRepair", Me.cboRepairCombinedDrop)
                Me.PopulateBOProperty(TheSpecialService, "Code", Me.SplServiceCodeText)
                Me.PopulateBOProperty(TheSpecialService, "Description", Me.SplServiceDescText)
                Me.PopulateBOProperty(TheSpecialService, "ServiceClassId", ServiceClassServiceTypeControl.ServiceClassGuid)
                Me.PopulateBOProperty(TheSpecialService, "ServiceTypeId", ServiceClassServiceTypeControl.ServiceTypeGuid)

            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub
#End Region

#Region "Gui-Validation"

        Private Sub SetButtonsState(ByVal bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            TheDealerControl.ChangeEnabledControlProperty(bIsNew)
            'ControlMgr.SetEnableControl(Me, moDealerDrop, bIsNew)
            ControlMgr.SetEnableControl(Me, SplServiceCodeText, bIsNew)
            ControlMgr.SetEnableControl(Me, SplServiceDescText, bIsNew)
        End Sub
#End Region

#Region "Business Part"

        Private Function IsDirtyBO() As Boolean
            Dim bIsDirty As Boolean = True
            Try
                With TheSpecialService
                    PopulateBOsFromForm()
                    bIsDirty = .IsDirty
                End With
            Catch ex As Exception
                'moErrorController.AddError(SPECIALSERVICE_FORM001)
                'moErrorController.AddError(ex.Message, False)
                'moErrorController.Show()
                Me.MasterPage.ErrController.AddErrorAndShow(ex.Message, False)
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean

            Try

                If UserControlAvailableSelectedProductCodes.SelectedListListBox.Items.Count = 0 Then
                    'display error
                    Throw New GUIException(Message.MSG_ATLEAST_ONE_PRODUCT_CODE_SHLD_BE_SELECTED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ATLEAST_ONE_PRODUCT_CODE_SHLD_BE_SELECTED)
                End If

                Me.PopulateBOsFromForm()

                If TheSpecialService.IsDirty() Then
                    Me.TheSpecialService.Save()

                    Me.State.boChanged = True
                    If Me.State.IsSpecialServiceNew = True Then
                        Me.State.IsSpecialServiceNew = False
                    End If
                    PopulateAll()
                    'EnableDisableFields()
                    Me.SetButtonsState(Me.State.IsSpecialServiceNew)
                    'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                Else
                    'Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    Me.MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Function

        Private Function DeleteSpecialService() As Boolean
            Dim bIsOk As Boolean = True

            Try
                With TheSpecialService
                    .BeginEdit()
                    PopulateBOsFromForm()
                    PopulateUserControlAvailableSelectedProductCodes()
                    .Delete()
                    .Save()
                End With
            Catch ex As Exception
                'moErrorController.AddError(SPECIALSERVICE_FORM002)
                'moErrorController.AddError(ex.Message, False)
                'moErrorController.Show()
                Me.MasterPage.ErrController.AddErrorAndShow(ex.Message, False)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

#End Region

#Region "Handlers-Labels"

        Private Sub BindBoPropertiesToLabels()

            Me.BindBOPropertyToLabel(TheSpecialService, DEALER_ID_PROPERTY, TheDealerControl.CaptionLabel)

            Me.BindBOPropertyToLabel(TheSpecialService, SPECIAL_SERVICE_CODE_PROPERTY, Me.SplServiceCodeLabel)

            Me.BindBOPropertyToLabel(TheSpecialService, SPECIAL_SERVICE_DESC_PROPERTY, Me.SplServiceDescLabel)

            Me.BindBOPropertyToLabel(TheSpecialService, COVERAGE_TYPE_ID_PROPERTY, Me.CoverageTypeLabel)

            Me.BindBOPropertyToLabel(TheSpecialService, ADD_ITEMS_ALLOWED_ID_PROPERTY, Me.AddItemsAllowedLabel)
            Me.BindBOPropertyToLabel(TheSpecialService, CAUSE_OF_LOSS_ID_PROPERTY, Me.CauseOfLossLabel)
            Me.BindBOPropertyToLabel(TheSpecialService, ADD_ITEMS_AFTER_CERT_EXPIRED, Me.AddItemsCertExpLabel)
            Me.BindBOPropertyToLabel(TheSpecialService, OCCURENCES_ALLOWED_PROPERTY, Me.OccurAllowedLabel)
            Me.BindBOPropertyToLabel(TheSpecialService, AUTHORIZED_AMOUNT_FROM_PROPERTY, Me.AuthAmtFromLabel)
            Me.BindBOPropertyToLabel(TheSpecialService, AVAIL_SVC_CENTER_PROPERTY, Me.AvailSvcCenterLabel)
            Me.BindBOPropertyToLabel(TheSpecialService, COMBINED_REPAIR_PROPERTY, Me.RepairCombinedLabel)


            Dim specialServiceLabel As Label = CType(Me.multipleDropControl.FindControl("lb_DropDown"), Label)
            Me.BindBOPropertyToLabel(TheSpecialService, SPECIAL_SERVICE_PROPERTY, specialServiceLabel)

            Dim serviceClassLabel As Label = CType(Me.ServiceClassServiceTypeControl.FindControl("ServiceClassLabel"), Label)
            Dim serviceTypeLabel As Label = CType(Me.ServiceClassServiceTypeControl.FindControl("ServiceTypeLabel"), Label)

            Me.BindBOPropertyToLabel(TheSpecialService, SERVICE_CLASS_ID_PROPERTY, serviceClassLabel)
            Me.BindBOPropertyToLabel(TheSpecialService, SERVICE_TYPE_ID_PROPERTY, serviceTypeLabel)

        End Sub

        Private Sub ClearLabelsErrSign()

            Me.ClearLabelErrSign(TheDealerControl.CaptionLabel)
            Me.ClearLabelErrSign(Me.SplServiceCodeLabel)
            Me.ClearLabelErrSign(Me.SplServiceDescLabel)
            Me.ClearLabelErrSign(Me.CoverageTypeLabel)
            Me.ClearLabelErrSign(Me.AddItemsAllowedLabel)
            Me.ClearLabelErrSign(Me.CauseOfLossLabel)
            Me.ClearLabelErrSign(Me.AddItemsCertExpLabel)
            Me.ClearLabelErrSign(Me.OccurAllowedLabel)
            Me.ClearLabelErrSign(Me.AuthAmtFromLabel)
            Me.ClearLabelErrSign(Me.AvailSvcCenterLabel)
            Me.ClearLabelErrSign(Me.RepairCombinedLabel)

            Dim serviceClassLabel As Label = CType(Me.ServiceClassServiceTypeControl.FindControl("ServiceClassLabel"), Label)
            Dim serviceTypeLabel As Label = CType(Me.ServiceClassServiceTypeControl.FindControl("ServiceTypeLabel"), Label)

            Me.ClearLabelErrSign(serviceClassLabel)
            Me.ClearLabelErrSign(serviceTypeLabel)
        End Sub
#End Region


#Region "State-Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        ' Save and go back to Search Page
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
                ' Return from the New Copy Button

                Select Case confResponse
                    Case Me.MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            Me.State.boChanged = True
                            CreateNewCopy()
                        End If
                    Case Me.MSG_VALUE_NO
                        ' create a new BO
                        CreateNewCopy()
                End Select
            End If

        End Sub


        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case Me.State.ActionInProgress
                    ' Period
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNewCopy()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()
                End Select

                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()
            'If (Not Me.State Is Nothing) Then
            'If (Not Me.State.searchDV Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                          TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_SPECIALSERVICE) & " " & "Item"
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_SPECIALSERVICE) & " " & "Item"
            'End If
            'End If
        End Sub

        Protected Sub EnableDisableFields()

            If Not Me.State.IsSpecialServiceNew = True Then
                TheDealerControl.ChangeEnabledControlProperty(False)
                ControlMgr.SetEnableControl(Me, cboCoverageTypeDrop, False)
                ControlMgr.SetEnableControl(Me, cboCauseOfLossDrop, False)
                ControlMgr.SetEnableControl(Me, SplServiceCodeText, False)
                ControlMgr.SetEnableControl(Me, SplServiceDescText, False)
            Else
                TheDealerControl.SelectedIndex = BLANK_ITEM_SELECTED
                cboCoverageTypeDrop.SelectedIndex = BLANK_ITEM_SELECTED
                ControlMgr.SetEnableControl(Me, cboCoverageTypeDrop, True)
                If cboCauseOfLossDrop.Items.Count > 0 Then
                    cboCauseOfLossDrop.SelectedIndex = BLANK_ITEM_SELECTED
                    ControlMgr.SetEnableControl(Me, cboCauseOfLossDrop, False)
                End If
            End If
        End Sub

#End Region

#Region "AUTHORIZED MANUFACTURER: Attach - Detach Event Handlers"


        Private Sub UserControlAvailableSelectedProductCodes_Attach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedProductCodes.Attach
            Try
                If attachedList.Count > 0 Then
                    TheSpecialService.AttachProductCodes(attachedList)
                    'Me.PopulateDetailMfgGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UserControlAvailableSelectedProductCodes_Detach(ByVal aSrc As Generic.UserControlAvailableSelected_New, ByVal detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedProductCodes.Detach
            Try
                If detachedList.Count > 0 Then
                    TheSpecialService.DetachProductCodes(detachedList)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

        Private Sub cboAuthAmtFromDrop_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboAuthAmtFromDrop.SelectedIndexChanged
            Try
                Dim priceGroupFieldId As Guid = ElitaPlusPage.GetSelectedItem(cboAuthAmtFromDrop)
                If LookupListNew.GetCodeFromId(LookupListNew.GetPriceGroupDPLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), priceGroupFieldId) = Codes.PRICEGROUP_SPL_SVC_MANUAL Then
                    TheServiceClassTypeControl.SetControl(True, TheServiceClassTypeControl.MODES.EDIT_MODE, True, , Me.SERVICE_CLASS_TYPE_CAPTION, True, True, , , , , , , True)
                Else
                    TheServiceClassTypeControl.SetControl(True, TheServiceClassTypeControl.MODES.EDIT_MODE, True, , Me.SERVICE_CLASS_TYPE_CAPTION, True, True, , , , , , , False)
                    TheServiceClassTypeControl.SpecialServiceGuid = TheSpecialService.Id
                    TheServiceClassTypeControl.ServiceClassGuid = TheSpecialService.ServiceClassId
                    TheServiceClassTypeControl.ServiceTypeGuid = TheSpecialService.ServiceTypeId
                End If
            Catch ex As Exception

            End Try
        End Sub


    End Class

End Namespace
