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
            If State.moSpecialServiceId.Equals(Guid.Empty) Then
                State.IsSpecialServiceNew = True
                ClearAll()
                SetButtonsState(True)
            Else
                State.IsSpecialServiceNew = False
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
                    If State.IsSpecialServiceNew = True Then
                        ' For creating, inserting
                        moSpecialService = New SpecialService
                        State.moSpecialServiceId = moSpecialService.Id
                    Else
                        ' For updating, deleting
                        '  Dim oProductCodeId As Guid = Me.GetGuidFromString(Me.State.moProductCodeId)
                        moSpecialService = New SpecialService(State.moSpecialServiceId)
                        State.oDealer = New Dealer(moSpecialService.DealerId)
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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
            Dim retObj As ProductPriceRangeByRepairMethod.ReturnType = CType(ReturnPar, ProductPriceRangeByRepairMethod.ReturnType)
            State.moSpecialServiceId = retObj.EditingId
            SetStateProperties()
            'Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, _
            'Me.MSG_TYPE_CONFIRM, True)
            State.LastOperation = DetailPageCommand.Redirect_
        End Sub

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    State.moSpecialServiceId = CType(CType(CallingParameters, MyState).moSpecialServiceId, Guid)
                    State.CoverageTypeId = CType(CType(CallingParameters, MyState).CoverageTypeId, Guid)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                'moErrorController.Clear_Hide()
                MasterPage.MessageController.Clear()
                ClearLabelsErrSign()
                'Setting the bread crum navigation
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("TABLES")
                UpdateBreadCrum()


                If Not Page.IsPostBack Then
                    SetStateProperties()
                    AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO,
                                                                        MSG_TYPE_CONFIRM, True)
                End If

                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()
                If Not IsPostBack Then
                    'BindBoPropertiesToLabels()
                    AddLabelDecorations(TheSpecialService)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
                'moErrorController.Clear_Hide()
                '              ClearLabelsErrSign()
                State.LastOperation = DetailPageCommand.Nothing_
            Else
                ShowMissingTranslations(MasterPage.MessageController)
            End If
        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnApply_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            ApplyChanges()
        End Sub

        Private Sub GoBack()
            Dim retType As New SpecialServiceListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                              State.moSpecialServiceId, State.boChanged)
            ReturnToCallingPage(retType)
            'Me.callPage(ProductCodeForm.PRODUCTCODE_LIST, param)

        End Sub

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM,
                                                HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
                DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr

            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                PopulateAll()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNew()
            State.moSpecialServiceId = Guid.Empty
            State.IsSpecialServiceNew = True
            ClearAll()
            'EnableDisableFields()
            SetButtonsState(True)
            PopulateAll()
            TheDealerControl.ChangeEnabledControlProperty(True)
        End Sub

        Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNewCopy()
            State.moSpecialServiceId = Guid.Empty
            State.IsSpecialServiceNew = True
            ClearTexts()
            EnableDisableFields()
            PopulateUserControlAvailableSelectedProductCodes()
            SetButtonsState(True)
            TheDealerControl.ChangeEnabledControlProperty(True)
        End Sub

        Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                If DeleteSpecialService() = True Then
                    State.boChanged = True
                    'Dim param As New ProductCodeSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete, _
                    '                Me.State.moProductCodeId)
                    'param.BoChanged = True
                    'Me.callPage(ProductCodeForm.PRODUCTCODE_LIST, param)
                    Dim retType As New SpecialServiceListForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                              State.moSpecialServiceId, State.boChanged)
                    retType.BoChanged = True
                    ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-DropDown"

        Private Sub OnFromDrop_Changed(fromMultipleDrop As MultipleColumnDDLabelControl_New) _
                        Handles multipleDropControl.SelectedDropChanged
            Try
                PopulateUserControlAvailableSelectedProductCodes()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moCoverageTypeDrop_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboCoverageTypeDrop.SelectedIndexChanged
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
                If State.IsSpecialServiceNew = True Then
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
                MasterPage.ErrController.AddErrorAndShow(ex.Message, False)
            End Try
        End Sub

        Private Sub PopulateServiceClassType()
            Try
                Dim svcclassTypeDisabled As Boolean = False
                If cboAuthAmtFromDrop.SelectedItem IsNot Nothing Then
                    If (cboAuthAmtFromDrop.SelectedItem.Text = "Manual") Then
                        svcclassTypeDisabled = True
                    End If
                End If

                TheServiceClassTypeControl.SetControl(True, TheServiceClassTypeControl.MODES.NEW_MODE, True, , SERVICE_CLASS_TYPE_CAPTION, True, True, , , , , , , svcclassTypeDisabled)
                If State.IsSpecialServiceNew = True Then
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
                MasterPage.ErrController.AddErrorAndShow(ex.Message, False)
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
                    UserControlAvailableSelectedProductCodes.SetAvailableData(availableDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
                    UserControlAvailableSelectedProductCodes.SetSelectedData(selectedDv, LookupListNew.COL_CODE_AND_DESCRIPTION_NAME, LookupListNew.COL_ID_NAME)
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
                    If State.IsSpecialServiceNew = True Then

                        BindSelectItem(Nothing, cboCoverageTypeDrop)
                        BindSelectItem(Nothing, cboCauseOfLossDrop)
                        BindSelectItem(Nothing, cboOccurAllowedDrop)
                        BindSelectItem(Nothing, cboAuthAmtFromDrop)
                        BindSelectItem(Nothing, cboRepairCombinedDrop)

                        Dim noId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_LANG_INDEPENDENT_YES_NO, Codes.YESNO_N)
                        SetSelectedItem(cboAddItemsAllowedDrop, noId)
                        SetSelectedItem(cboAddItemsCertExpDrop, noId)
                        SetSelectedItem(cboAvailSvcCenterDrop, noId)
                        SplServiceCodeText.Text = String.Empty
                        SplServiceDescText.Text = String.Empty

                    Else

                        'Me.SetSelectedItem(Me.moCoverageTypeDrop, Me.State.CoverageTypeId)
                        BindSelectItem(.AddItemAllowed.ToString, cboAddItemsAllowedDrop)
                        oCovloss = New CoverageLoss(.CoverageLossId)
                        SetSelectedItem(cboCauseOfLossDrop, oCovloss.CauseOfLossId)
                        SetSelectedItem(cboCoverageTypeDrop, oCovloss.CoverageTypeId)
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
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateAll()
            If State.IsSpecialServiceNew = True Then
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
            With TheSpecialService
                .DealerId = TheDealerControl.SelectedGuid
                'Me.PopulateBOProperty(TheProductCode, "DealerId", Me.moDealerDrop)
                PopulateBOProperty(TheSpecialService, "AddItemAllowed", cboAddItemsAllowedDrop)
                If cboCauseOfLossDrop.SelectedIndex > BLANK_ITEM_SELECTED Then
                    ds = CoverageLoss.LoadSelectedCovLossFromCovandCauseOfLoss(GetSelectedItem(cboCauseOfLossDrop), GetSelectedItem(cboCoverageTypeDrop))
                    If ds.Tables(0).Rows.Count > 0 Then
                        CoverageLossId = GuidControl.ByteArrayToGuid(ds.Tables(0).Rows(0)(CoverageLoss.SearchDV.COL_NAME_COVERAGE_LOSS_ID))
                    End If
                End If
                PopulateBOProperty(TheSpecialService, "CoverageLossId", CoverageLossId)
                PopulateBOProperty(TheSpecialService, "AddItemAfterExpired", cboAddItemsCertExpDrop)
                PopulateBOProperty(TheSpecialService, "AllowedOccurrencesId", cboOccurAllowedDrop)
                PopulateBOProperty(TheSpecialService, "PriceGroupFieldId", cboAuthAmtFromDrop, )
                PopulateBOProperty(TheSpecialService, "AvailableForServCenterId", cboAvailSvcCenterDrop)
                PopulateBOProperty(TheSpecialService, "CombinedWithRepair", cboRepairCombinedDrop)
                PopulateBOProperty(TheSpecialService, "Code", SplServiceCodeText)
                PopulateBOProperty(TheSpecialService, "Description", SplServiceDescText)
                PopulateBOProperty(TheSpecialService, "ServiceClassId", ServiceClassServiceTypeControl.ServiceClassGuid)
                PopulateBOProperty(TheSpecialService, "ServiceTypeId", ServiceClassServiceTypeControl.ServiceTypeGuid)

            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub
#End Region

#Region "Gui-Validation"

        Private Sub SetButtonsState(bIsNew As Boolean)
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
                MasterPage.ErrController.AddErrorAndShow(ex.Message, False)
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean

            Try

                If UserControlAvailableSelectedProductCodes.SelectedListListBox.Items.Count = 0 Then
                    'display error
                    Throw New GUIException(Message.MSG_ATLEAST_ONE_PRODUCT_CODE_SHLD_BE_SELECTED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ATLEAST_ONE_PRODUCT_CODE_SHLD_BE_SELECTED)
                End If

                PopulateBOsFromForm()

                If TheSpecialService.IsDirty() Then
                    TheSpecialService.Save()

                    State.boChanged = True
                    If State.IsSpecialServiceNew = True Then
                        State.IsSpecialServiceNew = False
                    End If
                    PopulateAll()
                    'EnableDisableFields()
                    SetButtonsState(State.IsSpecialServiceNew)
                    'Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    MasterPage.MessageController.AddSuccess(ElitaPlus.ElitaPlusWebApp.Message.SAVE_RECORD_CONFIRMATION)
                Else
                    'Me.DisplayMessage(Message.MSG_RECORD_NOT_SAVED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                    MasterPage.MessageController.AddError(ElitaPlus.ElitaPlusWebApp.Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
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
                MasterPage.ErrController.AddErrorAndShow(ex.Message, False)
                bIsOk = False
            End Try
            Return bIsOk
        End Function

#End Region

#Region "Handlers-Labels"

        Private Sub BindBoPropertiesToLabels()

            BindBOPropertyToLabel(TheSpecialService, DEALER_ID_PROPERTY, TheDealerControl.CaptionLabel)

            BindBOPropertyToLabel(TheSpecialService, SPECIAL_SERVICE_CODE_PROPERTY, SplServiceCodeLabel)

            BindBOPropertyToLabel(TheSpecialService, SPECIAL_SERVICE_DESC_PROPERTY, SplServiceDescLabel)

            BindBOPropertyToLabel(TheSpecialService, COVERAGE_TYPE_ID_PROPERTY, CoverageTypeLabel)

            BindBOPropertyToLabel(TheSpecialService, ADD_ITEMS_ALLOWED_ID_PROPERTY, AddItemsAllowedLabel)
            BindBOPropertyToLabel(TheSpecialService, CAUSE_OF_LOSS_ID_PROPERTY, CauseOfLossLabel)
            BindBOPropertyToLabel(TheSpecialService, ADD_ITEMS_AFTER_CERT_EXPIRED, AddItemsCertExpLabel)
            BindBOPropertyToLabel(TheSpecialService, OCCURENCES_ALLOWED_PROPERTY, OccurAllowedLabel)
            BindBOPropertyToLabel(TheSpecialService, AUTHORIZED_AMOUNT_FROM_PROPERTY, AuthAmtFromLabel)
            BindBOPropertyToLabel(TheSpecialService, AVAIL_SVC_CENTER_PROPERTY, AvailSvcCenterLabel)
            BindBOPropertyToLabel(TheSpecialService, COMBINED_REPAIR_PROPERTY, RepairCombinedLabel)


            Dim specialServiceLabel As Label = CType(multipleDropControl.FindControl("lb_DropDown"), Label)
            BindBOPropertyToLabel(TheSpecialService, SPECIAL_SERVICE_PROPERTY, specialServiceLabel)

            Dim serviceClassLabel As Label = CType(ServiceClassServiceTypeControl.FindControl("ServiceClassLabel"), Label)
            Dim serviceTypeLabel As Label = CType(ServiceClassServiceTypeControl.FindControl("ServiceTypeLabel"), Label)

            BindBOPropertyToLabel(TheSpecialService, SERVICE_CLASS_ID_PROPERTY, serviceClassLabel)
            BindBOPropertyToLabel(TheSpecialService, SERVICE_TYPE_ID_PROPERTY, serviceTypeLabel)

        End Sub

        Private Sub ClearLabelsErrSign()

            ClearLabelErrSign(TheDealerControl.CaptionLabel)
            ClearLabelErrSign(SplServiceCodeLabel)
            ClearLabelErrSign(SplServiceDescLabel)
            ClearLabelErrSign(CoverageTypeLabel)
            ClearLabelErrSign(AddItemsAllowedLabel)
            ClearLabelErrSign(CauseOfLossLabel)
            ClearLabelErrSign(AddItemsCertExpLabel)
            ClearLabelErrSign(OccurAllowedLabel)
            ClearLabelErrSign(AuthAmtFromLabel)
            ClearLabelErrSign(AvailSvcCenterLabel)
            ClearLabelErrSign(RepairCombinedLabel)

            Dim serviceClassLabel As Label = CType(ServiceClassServiceTypeControl.FindControl("ServiceClassLabel"), Label)
            Dim serviceTypeLabel As Label = CType(ServiceClassServiceTypeControl.FindControl("ServiceTypeLabel"), Label)

            ClearLabelErrSign(serviceClassLabel)
            ClearLabelErrSign(serviceTypeLabel)
        End Sub
#End Region


#Region "State-Management"

        Protected Sub ComingFromBack()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

            If Not confResponse = String.Empty Then
                ' Return from the Back Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and go back to Search Page
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
                ' Return from the New Copy Button

                Select Case confResponse
                    Case MSG_VALUE_YES
                        ' Save and create a new Copy BO
                        If ApplyChanges() = True Then
                            State.boChanged = True
                            CreateNewCopy()
                        End If
                    Case MSG_VALUE_NO
                        ' create a new BO
                        CreateNewCopy()
                End Select
            End If

        End Sub


        Protected Sub CheckIfComingFromConfirm()
            Try
                Select Case State.ActionInProgress
                    ' Period
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ComingFromBack()
                    Case ElitaPlusPage.DetailPageCommand.New_
                        ComingFromNewCopy()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        ComingFromNewCopy()
                End Select

                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = String.Empty
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()
            'If (Not Me.State Is Nothing) Then
            'If (Not Me.State.searchDV Is Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                          TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_SPECIALSERVICE) & " " & "Item"
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_SPECIALSERVICE) & " " & "Item"
            'End If
            'End If
        End Sub

        Protected Sub EnableDisableFields()

            If Not State.IsSpecialServiceNew = True Then
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


        Private Sub UserControlAvailableSelectedProductCodes_Attach(aSrc As Generic.UserControlAvailableSelected_New, attachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedProductCodes.Attach
            Try
                If attachedList.Count > 0 Then
                    TheSpecialService.AttachProductCodes(attachedList)
                    'Me.PopulateDetailMfgGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub UserControlAvailableSelectedProductCodes_Detach(aSrc As Generic.UserControlAvailableSelected_New, detachedList As System.Collections.ArrayList) Handles UserControlAvailableSelectedProductCodes.Detach
            Try
                If detachedList.Count > 0 Then
                    TheSpecialService.DetachProductCodes(detachedList)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

        Private Sub cboAuthAmtFromDrop_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboAuthAmtFromDrop.SelectedIndexChanged
            Try
                Dim priceGroupFieldId As Guid = ElitaPlusPage.GetSelectedItem(cboAuthAmtFromDrop)
                If LookupListNew.GetCodeFromId(LookupListNew.GetPriceGroupDPLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), priceGroupFieldId) = Codes.PRICEGROUP_SPL_SVC_MANUAL Then
                    TheServiceClassTypeControl.SetControl(True, TheServiceClassTypeControl.MODES.EDIT_MODE, True, , SERVICE_CLASS_TYPE_CAPTION, True, True, , , , , , , True)
                Else
                    TheServiceClassTypeControl.SetControl(True, TheServiceClassTypeControl.MODES.EDIT_MODE, True, , SERVICE_CLASS_TYPE_CAPTION, True, True, , , , , , , False)
                    TheServiceClassTypeControl.SpecialServiceGuid = TheSpecialService.Id
                    TheServiceClassTypeControl.ServiceClassGuid = TheSpecialService.ServiceClassId
                    TheServiceClassTypeControl.ServiceTypeGuid = TheSpecialService.ServiceTypeId
                End If
            Catch ex As Exception

            End Try
        End Sub


    End Class

End Namespace
