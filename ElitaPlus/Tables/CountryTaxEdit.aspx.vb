Imports System.Text
Imports System.Globalization
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Partial Class CountryTaxEdit
    Inherits ElitaPlusPage

#Region "Web controls and private members"

    Protected WithEvents CountryTaxUserControl1 As CountryTaxUserControl
    Protected WithEvents CountryTaxUserControl2 As CountryTaxUserControl
    Protected WithEvents CountryTaxUserControl3 As CountryTaxUserControl
    Protected WithEvents CountryTaxUserControl4 As CountryTaxUserControl
    Protected WithEvents CountryTaxUserControl5 As CountryTaxUserControl
    Protected WithEvents CountryTaxUserControl6 As CountryTaxUserControl

    Protected WithEvents valSummary As System.Web.UI.WebControls.ValidationSummary
    Protected WithEvents lblDBErrorMessage As System.Web.UI.WebControls.Label
    Protected WithEvents Label57 As System.Web.UI.WebControls.Label
    Protected WithEvents Label59 As System.Web.UI.WebControls.Label

    Protected WithEvents Table5 As System.Web.UI.WebControls.Table
    Protected WithEvents cboAssignedTo As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Dropdownlist1 As System.Web.UI.WebControls.DropDownList
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Textbox3 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Textbox4 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Label10 As System.Web.UI.WebControls.Label
    Protected WithEvents Label11 As System.Web.UI.WebControls.Label
    Protected WithEvents Textbox5 As System.Web.UI.WebControls.TextBox
    Protected WithEvents Textbox1 As System.Web.UI.WebControls.TextBox
    Protected WithEvents lblAddress As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents TablesLabel As System.Web.UI.WebControls.Label
    Protected WithEvents EditCountryTaxLabel As System.Web.UI.WebControls.Label
    Protected WithEvents Label9 As System.Web.UI.WebControls.Label
    Protected WithEvents ViewPanel_WRITEold As System.Web.UI.WebControls.Panel

    Private mEnglishLanguageID As Guid
    Private mUserControlArray As ArrayList = New ArrayList

    Public Const PAGETITLE As String = "CountryTax"
    Public Const PAGETAB As String = "TABLES"
    Public Const REGION_TAX_ID As String = "REGION_TAX_ID"
    Public Const LISTFIELDS As String = "ListFields"


#End Region

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
    Public Const URL As String = "CountryTaxEdit.aspx"
    Private Const TAX_TYPE As String = "TTYP"

    Private Const BOPROP_TAXGROUP_DESC As String = "Tax{0}Description"
    Private Const BOPROP_TAXGROUP_COMPMETHOD As String = "Tax{0}ComputeMethodId"
    Private Const BOPROP_TAXGROUP_PERCTFLAG As String = "Tax{0}PercentFlagId"
    Private Const BOPROP_TAXGROUP_PERCT As String = "Tax{0}Percent"
    Private Const TAX_ON_GROSS_COMPUTE_METHOD As String = "Compute On Gross"

    Public Const COUNTRYID_PROPERTY As String = "CountryId"
    Public Const TAXTYPEID_PROPERTY As String = "TaxTypeId"
    Public Const EFFECTIVEDATE_PROPERTY As String = "EffectiveDate"
    Public Const EXPIRATIONDATE_PROPERTY As String = "ExpirationDate"
    Public Const COMPANYTYPEID_PROPERTY As String = "CompanyTypeId"
    Public Const PRODUCT_TAX_TYPE_ID_PROPERTY As String = "ProductTaxTypeId"
    Public Const DEALER_ID_PROPERTY As String = "DealerId"
    Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"

    Private Const GUI_EMPTY_FORM_ERROR As String = "FIELDS ARE EMPTY"

    Public Const REPAIR__CLAIM_DIAGNOSTICS = "RPR_CDIAG"
    Public Const REPAIR__CLAIM_DISPOSITION = "RPR_CDISP"
    Public Const REPAIR__CLAIM_LABOR = "RPR_CLABR"
    Public Const REPAIR__CLAIM_OTHER = "RPR_COTHR"
    Public Const REPAIR__CLAIM_PARTS = "RPR_CPART"
    Public Const REPAIR__CLAIM_SERVICE = "RPR_CSRVC"
    Public Const REPAIR__CLAIM_SHIPPING = "RPR_CSHIP"
    Public Const REPAIR__CLAIM_TRIP = "RPR_CTRIP"
    Public Const REPAIR__CLAIM_DEFAULT = "RPR_CDFLT"

    Public Const REPLACEMENT__CLAIM_DIAGNOSTICS = "RPL_CDIAG"
    Public Const REPLACEMENT__CLAIM_DISPOSITION = "RPL_CDISP"
    Public Const REPLACEMENT__CLAIM_LABOR = "RPL_CLABR"
    Public Const REPLACEMENT__CLAIM_OTHER = "RPL_COTHR"
    Public Const REPLACEMENT__CLAIM_PARTS = "RPL_CPART"
    Public Const REPLACEMENT__CLAIM_SERVICE = "RPL_CSRVC"
    Public Const REPLACEMENT__CLAIM_SHIPPING = "RPL_CSHIP"
    Public Const REPLACEMENT__CLAIM_TRIP = "RPL_CTRIP"
    Public Const REPLACEMENT__CLAIM_DEFAULT = "RPL_CDFLT"

    Public claimTaxTypes() As String = {REPAIR__CLAIM_DEFAULT, REPAIR__CLAIM_DIAGNOSTICS, REPAIR__CLAIM_DISPOSITION, REPAIR__CLAIM_LABOR, REPAIR__CLAIM_OTHER, REPAIR__CLAIM_PARTS,
                                 REPAIR__CLAIM_SERVICE, REPAIR__CLAIM_SHIPPING, REPAIR__CLAIM_TRIP, REPAIR__CLAIM_DEFAULT, REPLACEMENT__CLAIM_DIAGNOSTICS, REPLACEMENT__CLAIM_DISPOSITION,
                                 REPLACEMENT__CLAIM_LABOR, REPLACEMENT__CLAIM_OTHER, REPLACEMENT__CLAIM_PARTS, REPLACEMENT__CLAIM_SERVICE, REPLACEMENT__CLAIM_SHIPPING,
                                 REPLACEMENT__CLAIM_TRIP, REPLACEMENT__CLAIM_DEFAULT}

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As CountryTax
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As CountryTax, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public MyBO As CountryTax
        Public ScreenSnapShotBO As CountryTax
        Public IsNew As Boolean
        Public IsACopy As Boolean
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public showRegionTax As Boolean = False
        Public TaxTypeCode As String

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

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End If
    End Sub

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        SetFormTab(PAGETAB)
        SetFormTitle(PAGETITLE)

        MasterPage.MessageController.Clear_Hide()

        'Set the readonly attributes for date textboxes
        txtEffectiveDate.Attributes.Add("readonly", "readonly")
        txtExpirationDate.Attributes.Add("readonly", "readonly")

        Try
            MasterPage.MessageController.Clear()
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            UpdateBreadCrum()

            If Not IsPostBack Then
                'Date Calendars
                AddCalendar(btnEffectiveDate, txtEffectiveDate)
                AddCalendar(btnExpirationDate, txtExpirationDate)
                'Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)

                If State.MyBO Is Nothing Then
                    State.MyBO = New CountryTax()
                End If

                PopulateDropdowns()
                'PopulateDealerDropdown(Me.cboDealer)
                PopulateFormFromBOs()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()

            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

        ShowMissingTranslations(MasterPage.MessageController)

    End Sub

    Private Sub Page_LoadComplete(sender As System.Object, e As System.EventArgs) Handles MyBase.LoadComplete
        'reset control status if there is not error, otherwise keep the original
        EnableDisableFields()
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                State.MyBO = New CountryTax(CType(CallingParameters, Guid))
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        IsReturningFromChild = True

        Try
            If CalledUrl = RegionTaxes.URL Then
                Dim retObj As RegionTaxes.ReturnType = CType(ReturnPar, RegionTaxes.ReturnType)

                If retObj IsNot Nothing Then
                    State.MyBO = retObj.EditingBo
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"

    Protected Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, COUNTRYID_PROPERTY, lblCountry)
        BindBOPropertyToLabel(State.MyBO, TAXTYPEID_PROPERTY, lblTaxType)
        BindBOPropertyToLabel(State.MyBO, EFFECTIVEDATE_PROPERTY, lblEffectiveDate)
        BindBOPropertyToLabel(State.MyBO, EXPIRATIONDATE_PROPERTY, lblExpirationDate)
        BindBOPropertyToLabel(State.MyBO, COMPANYTYPEID_PROPERTY, lblCompanyType)
        BindBOPropertyToLabel(State.MyBO, PRODUCT_TAX_TYPE_ID_PROPERTY, lblProductTaxType)
        'Me.BindBOPropertyToLabel(Me.State.MyBO, DEALER_ID_PROPERTY, Me.lblDealer)

        ClearGridHeadersAndLabelsErrSign()
    End Sub

    Protected Sub EnableDisableFields()
        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

        'Disabled by default
        ControlMgr.SetEnableControl(Me, dlstTaxType_WRITE, False)
        dlstTaxType_WRITE.Style.Add("background-color", "LightGray")
        EnableDisableTaxType(False)
        EnableDisableProductTaxType(False)
        EnableDisableCompanyType(False)
        EnableDisableCountry(False)
        EnableDisableEffectiveDate(False)
        EnableDisableExpirationDate(False)
        'REQ-1150
        EnableDisableDealer(False)

        'Now we check which control should be enabled
        Dim theBO As CountryTax = State.MyBO
        Dim existingRecCnt As Integer, minEffDate As Date, maxExpDate As Date
        theBO.GetMinEffDateAndMaxExpDate(minEffDate, maxExpDate, existingRecCnt)

        'New
        If theBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)

            'new BO, enable tax type
            ControlMgr.SetEnableControl(Me, dlstTaxType_WRITE, True)
            dlstTaxType_WRITE.Style.Add("background-color", "")
            EnableDisableTaxType(True)
            EnableDisableProductTaxType(True)
            If Not theBO.TaxTypeId.Equals(Guid.Empty) Then
                'Enable other fields for existing Tax type
                If existingRecCnt = 0 Then 'the first record, allow modify the effective date
                    EnableDisableEffectiveDate(True)
                End If
                EnableDisableExpirationDate(True)
                EnableDisableCompanyType(True)
                '   EnableDisableProductTaxType(True)
                EnableDisableCountry(True)
                'REQ-1150
                EnableDisableDealer(True)
            End If
        Else 'Existing BO
            If theBO.ExpirationDate.Value <> maxExpDate Then
                'only allow delete the last record
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                'only allow expiration date modification the last record
                EnableDisableExpirationDate(False)
            Else
                EnableDisableExpirationDate(True)
                'allow modify effective date if the record is the only record with future effective date
                If theBO.EffectiveDate.Value = minEffDate AndAlso minEffDate > Date.Now Then
                    EnableDisableEffectiveDate(True)
                End If
            End If
        End If

    End Sub

    Protected Sub EnableDisableExpirationDate(enabled As Boolean)
        If enabled Then
            ControlMgr.SetVisibleControl(Me, btnExpirationDate, True)
            ControlMgr.SetEnableControl(Me, btnExpirationDate, True)
            txtExpirationDate.Style.Add("color", "black")
        Else
            ControlMgr.SetVisibleControl(Me, btnExpirationDate, False)
            ControlMgr.SetEnableControl(Me, btnExpirationDate, False)
            txtExpirationDate.Style.Add("color", "Gray")
        End If
    End Sub

    Protected Sub EnableDisableEffectiveDate(enabled As Boolean)
        If enabled Then
            ControlMgr.SetVisibleControl(Me, btnEffectiveDate, True)
            ControlMgr.SetEnableControl(Me, btnEffectiveDate, True)
            txtEffectiveDate.Style.Add("color", "black")
        Else
            ControlMgr.SetVisibleControl(Me, btnEffectiveDate, False)
            ControlMgr.SetEnableControl(Me, btnEffectiveDate, False)
            txtEffectiveDate.Style.Add("color", "Gray")
        End If
    End Sub

    'REQ 1150
    Protected Sub EnableDisableTaxType(enabled As Boolean)
        If enabled Then
            dlstTaxType_WRITE.Visible = True
            dlstTaxType_WRITETextBox.Visible = False
        Else
            dlstTaxType_WRITE.Visible = False
            dlstTaxType_WRITETextBox.Visible = True
            dlstTaxType_WRITETextBox.Text = dlstTaxType_WRITE.SelectedItem.ToString()
        End If
    End Sub

    Protected Sub EnableDisableCompanyType(enabled As Boolean)
        If enabled Then
            ControlMgr.SetEnableControl(Me, cboCompanyType, True)
            cboCompanyType.Style.Add("background-color", "")

            'REQ 1150
            cboCompanyType.Visible = True
            cboCompanyTypeTextBox.Visible = False
        Else
            ControlMgr.SetEnableControl(Me, cboCompanyType, False)
            cboCompanyType.Style.Add("background-color", "LightGray")

            'REQ 1150
            cboCompanyType.Visible = False
            cboCompanyTypeTextBox.Visible = True
            cboCompanyTypeTextBox.Text = cboCompanyType.SelectedItem.ToString()
        End If
    End Sub

    Protected Sub EnableDisableProductTaxType(enabled As Boolean)
        If enabled Then
            ControlMgr.SetEnableControl(Me, cboProductTaxType, True)
            cboProductTaxType.Style.Add("background-color", "")
        Else
            ControlMgr.SetEnableControl(Me, cboProductTaxType, False)
            cboProductTaxType.Style.Add("background-color", "LightGray")
        End If
    End Sub

    Private Sub SetTaxByProductType(oCountry As Country, oTaxTypeId As Guid)
        Dim oYesId, oTaxTypePosId, oTaxTypeCommId As Guid

        State.TaxTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_TAX_TYPES, oTaxTypeId)

        oYesId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(
                    Codes.YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId), Codes.YESNO_Y)
        oTaxTypePosId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(
                Codes.TAX_TYPE, Authentication.LangId), Codes.TAX_TYPE__POS)
        oTaxTypeCommId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(
                Codes.TAX_TYPE, Authentication.LangId), Codes.TAX_TYPE__COMMISSIONS)
        If (oCountry.TaxByProductTypeId.Equals(oYesId) AndAlso
                (oTaxTypeId.Equals(oTaxTypePosId) OrElse oTaxTypeId.Equals(oTaxTypeCommId))) Then
            ' Yes => Visible
            ControlMgr.SetVisibleControl(Me, lblProductTaxType, True)
            ControlMgr.SetVisibleControl(Me, cboProductTaxType, True)
        Else    ' No => Invisible
            SetSelectedItem(cboProductTaxType,
                LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(
                Codes.PRODUCT_TAX_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), Codes.PRODUCT_TAX_TYPE__ALL))
            ControlMgr.SetVisibleControl(Me, lblProductTaxType, False)
            ControlMgr.SetVisibleControl(Me, cboProductTaxType, False)
        End If
    End Sub


    Protected Sub EnableDisableCountry(enabled As Boolean)
        If enabled Then
            ControlMgr.SetEnableControl(Me, cboCountry, True)
            cboCountry.Style.Add("background-color", "")

            'REQ 1150
            cboCountry.Visible = True
            cboCountryTextBox.Visible = False
        Else
            ControlMgr.SetEnableControl(Me, cboCountry, False)
            cboCountry.Style.Add("background-color", "LightGray")

            'REQ 1150
            cboCountry.Visible = False
            cboCountryTextBox.Visible = True
            cboCountryTextBox.Text = cboCountry.SelectedItem.ToString()
        End If
    End Sub

    Protected Sub EnableDisableDealer(enabled As Boolean)
        If Not moDealerMultipleDrop.VisibleDD Then
            moDealerMultipleDrop.Visible = True
        End If
        If enabled Then
            moDealerMultipleDrop.Mode = 1
            moDealerMultipleDrop.CodeDropDown.Style.Add("background-color", "")
            moDealerMultipleDrop.DescDropDown.Style.Add("background-color", "")
        Else
            If moDealerMultipleDrop.CodeDropDown.SelectedIndex <> BLANK_ITEM_SELECTED Then
                moDealerMultipleDrop.CodeDropDown.Style.Add("background-color", "LightGray")
                moDealerMultipleDrop.DescDropDown.Style.Add("background-color", "LightGray")
                moDealerMultipleDrop.CodeTextBox.Text = moDealerMultipleDrop.CodeDropDown.SelectedItem.ToString()
                moDealerMultipleDrop.DescriptionTextBox.Text = moDealerMultipleDrop.DescDropDown.SelectedItem.ToString()
                moDealerMultipleDrop.Mode = 2

            Else
                moDealerMultipleDrop.Visible = False
            End If

        End If
    End Sub

    Protected Sub PopulateDropdowns()
        'Dim oTaxTypeList As DataView = LookupListNew.DropdownLookupList(TAX_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        dlstTaxType_WRITE.Items.Clear()
        cboProductTaxType.Items.Clear()
        'Me.BindListControlToDataView(dlstTaxType_WRITE, oTaxTypeList)--TTYP
        dlstTaxType_WRITE.Populate(CommonConfigManager.Current.ListManager.GetList("TTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
              .AddBlankItem = True
                })
        'Me.BindListControlToDataView(Me.cboCompanyType, LookupListNew.DropdownLookupList(LookupListNew.LK_COTYP, Authentication.LangId, False), , , False)
        cboCompanyType.Populate(CommonConfigManager.Current.ListManager.GetList("COTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
        '  Me.BindListControlToDataView(Me.cboProductTaxType, LookupListNew.DropdownLookupList(
        '.LK_PRODUCT_TAX_TYPE, Authentication.LangId, False), , , False) 'PTT
        cboProductTaxType.Populate(CommonConfigManager.Current.ListManager.GetList("PTT", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
        ' Me.BindListControlToDataView(Me.cboCountry, LookupListNew.GetUserCountriesLookupList()) 'UserCountries
        Dim listcontext As ListContext = New ListContext()
        Dim countryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
        Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries

        Dim filteredList As ListItem() = (From x In countryLkl
                                          Where list.Contains(x.ListItemId)
                                          Select x).ToArray()
        cboCountry.Populate(filteredList, New PopulateOptions() With {
        .AddBlankItem = True
        })
        'Me.BindListControlToDataView(Me.cboDealer,
        '                 LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code"), "CODE", , True)

        moDealerMultipleDrop.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE)
        moDealerMultipleDrop.NothingSelected = True

        moDealerMultipleDrop.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))
        moDealerMultipleDrop.AutoPostBackDD = True
    End Sub

    Protected Sub PopulateFormFromBOs()
        Dim oCountry As Country
        Dim oCountryTax As CountryTax = State.MyBO
        '  Dim oYesId, oTaxTypePosId, oTaxTypeCommId As Guid
        With State.MyBO
            PopulateControlFromBOProperty(txtEffectiveDate, .EffectiveDate)
            PopulateControlFromBOProperty(txtExpirationDate, .ExpirationDate)
            SetSelectedItem(dlstTaxType_WRITE, .TaxTypeId)
            SetSelectedItem(cboCompanyType, .CompanyTypeId)
            SetSelectedItem(cboCountry, .CountryId)
            'REQ-1150
            'Me.SetSelectedItem(Me.cboDealer, .DealerId)

            If Not State.MyBO.TaxTypeId.Equals(System.Guid.Empty) Then
                moDealerMultipleDrop.SelectedGuid = .DealerId
            Else
                moDealerMultipleDrop.SelectedIndex = BLANK_ITEM_SELECTED
            End If
            ' ProductTaxType
            If State.MyBO.ProductTaxTypeId.Equals(System.Guid.Empty) Then
                SetSelectedItem(cboProductTaxType,
                    LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(
                    Codes.PRODUCT_TAX_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), Codes.PRODUCT_TAX_TYPE__ALL))
            Else
                SetSelectedItem(cboProductTaxType, .ProductTaxTypeId)
            End If

            State.TaxTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_TAX_TYPES, .TaxTypeId)

            If claimTaxTypes.Contains(State.TaxTypeCode) Then
                ControlMgr.SetVisibleControl(Me, tdWithholdingCheck, True)
                If Not .ApplyWithholdingFlag.Equals(String.Empty) AndAlso .ApplyWithholdingFlag.Equals("Y") Then
                    CheckBoxApplyWithholding.Checked = True
                Else
                    CheckBoxApplyWithholding.Checked = False
                End If
            Else
                ControlMgr.SetVisibleControl(Me, tdWithholdingCheck, False)
            End If

            oCountry = New Country(.CountryId)
            SetTaxByProductType(oCountry, .TaxTypeId)

            LoadTaxGroupUserControls(True)
        End With

    End Sub

    Private Sub LoadTaxGroupUserControls(Optional ByVal setCtlProperties As Boolean = False)

        mUserControlArray.Clear()
        mUserControlArray.Add(CountryTaxUserControl1)
        mUserControlArray.Add(CountryTaxUserControl2)
        mUserControlArray.Add(CountryTaxUserControl3)
        mUserControlArray.Add(CountryTaxUserControl4)
        mUserControlArray.Add(CountryTaxUserControl5)
        mUserControlArray.Add(CountryTaxUserControl6)

        If setCtlProperties Then SetUserControlsProperties()

    End Sub

    Private Sub SetUserControlsProperties()

        Dim intTaxGroup As Integer
        Dim oUserControl As CountryTaxUserControl
        Dim oUseRegion As Boolean = False
        Dim oTG As CountryTax.TaxGroupData

        With State.MyBO
            For intTaxGroup = 1 To CountryTax.NumberOfTaxGroups
                oUserControl = CType(mUserControlArray(intTaxGroup - 1), CountryTaxUserControl)
                oTG = .TaxGroup(intTaxGroup)

                oUserControl.Description = oTG.Description

                If oTG.CompMethod.Equals(Guid.Empty) Then
                    oUserControl.ComputeMethodID = Guid.Empty
                    oUserControl.PercentFlagID = Guid.Empty
                Else
                    oUserControl.ComputeMethodID = oTG.CompMethod
                    oUserControl.PercentFlagID = oTG.PercentFlag
                End If

                oUserControl.Percent = oTG.Percent

                Dim strItemToRemove As String = ""
                If claimTaxTypes.Contains(State.TaxTypeCode) Then
                    'remove item " P; Compute on Product Price as it Is Not applicable to claim taxes
                    strItemToRemove = "P"
                End If

                oUserControl.LoadProperties(strItemToRemove)

                If (Not oUseRegion) AndAlso oUserControl.UseRegion Then
                    oUseRegion = True
                End If
            Next

            If oUseRegion Then
                State.showRegionTax = True
                EnableDisableRegionTaxes(True)
            End If
        End With

    End Sub

    Private Sub EnableDisableRegionTaxes(enabled As Boolean)
        If enabled Then

            'show the controls
            ControlMgr.SetVisibleControl(Me, lstRegion, True)
            ControlMgr.SetVisibleControl(Me, lstRegionTax, True)
            ControlMgr.SetVisibleControl(Me, lblRegionList, True)
            ControlMgr.SetVisibleControl(Me, lblRegionTax, True)
            ControlMgr.SetVisibleControl(Me, btnNewRegionTax_WRITE, True)

            'load the list data
            '  Dim oRegionList As DataView = LookupListNew.GetRegionLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
            lstRegion.Items.Clear()
            'Me.BindListControlToDataView(Me.lstRegion, oRegionList, , , False) 'RegionsByCountry
            Dim listcontext As ListContext = New ListContext()
            listcontext.CountryId = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id
            lstRegion.Populate(CommonConfigManager.Current.ListManager.GetList("RegionsByCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext), New PopulateOptions())
            lstRegion.SelectedIndex = 0

            'Dim oRegionTaxList As DataView = LookupListNew.GetRegionTaxLookupList(Me.GetSelectedItem(Me.lstRegion), Me.State.MyBO.TaxTypeId)
            'Me.lstRegionTax.Items.Clear()
            'oRegionTaxList.Sort = Nothing   ' Preserve sort order from database by Effective Date, which is not in the select field list
            'Me.BindListControlToDataView(Me.lstRegionTax, oRegionTaxList, "CODE", "ID", False)
            LoadRegionTaxList()
        Else 'hide the region tax section
            ControlMgr.SetVisibleControl(Me, lstRegion, False)
            ControlMgr.SetVisibleControl(Me, lstRegionTax, False)
            ControlMgr.SetVisibleControl(Me, lblRegionList, False)
            ControlMgr.SetVisibleControl(Me, lblRegionTax, False)
            ControlMgr.SetVisibleControl(Me, btnNewRegionTax_WRITE, False)
        End If

    End Sub

    Protected Sub PopulateBOsFromForm()

        Dim objBO As CountryTax = State.MyBO
        'If Me.dlstTaxType_WRITE.SelectedValue.Trim() = "00000000-0000-0000-0000-000000000000" Then
        'GUI_FIELDS_EMPTY_ERR
        '  Me.ErrControllerMaster.AddErrorAndShow(GUI_EMPTY_FORM_ERROR)
        '  Throw New GUIException("", "")
        ' Else
        PopulateBOProperty(objBO, "TaxTypeId", dlstTaxType_WRITE)
        ' End If
        PopulateBOProperty(objBO, "EffectiveDate", txtEffectiveDate)
        PopulateBOProperty(objBO, "ExpirationDate", txtExpirationDate)
        PopulateBOProperty(objBO, "CompanyTypeId", cboCompanyType)
        PopulateBOProperty(objBO, PRODUCT_TAX_TYPE_ID_PROPERTY, cboProductTaxType)
        PopulateBOProperty(objBO, "CountryId", cboCountry)
        PopulateBOProperty(objBO, "DealerId", moDealerMultipleDrop.SelectedGuid)

        If claimTaxTypes.Contains(State.TaxTypeCode) Then
            If tdWithholdingCheck.Visible Then
                If CheckBoxApplyWithholding.Checked Then
                    PopulateBOProperty(objBO, "ApplyWithholdingFlag", "Y")
                Else
                    PopulateBOProperty(objBO, "ApplyWithholdingFlag", "N")
                End If
            End If
        End If

        LoadTaxGroupUserControls(False)

        Dim intTaxGroup As Integer
        Dim oUserControl As CountryTaxUserControl
        Dim oTG As CountryTax.TaxGroupData
        Dim validNum As Decimal, errMsg(CountryTax.NumberOfTaxGroups) As String
        Dim intErrNum As Integer = 0, blnSaveErr As Boolean = False

        For intTaxGroup = 1 To CountryTax.NumberOfTaxGroups
            oUserControl = CType(mUserControlArray(intTaxGroup - 1), CountryTaxUserControl)
            Try
                oUserControl.SaveProperties()
            Catch ex As GUIException
                errMsg(intErrNum) = ex.Message
                intErrNum += 1
                blnSaveErr = True
            End Try

            If intTaxGroup = 1 And oUserControl.ComputeMethodDescription = TAX_ON_GROSS_COMPUTE_METHOD Then
                errMsg(intErrNum) = Message.MSG_TAX_METHOD_COMPUTE_ON_GROSS_NOT_ALLOWED_ON_1ST_BRACKET
                intErrNum += 1
                blnSaveErr = True
            End If

            If Not blnSaveErr Then
                With oUserControl
                    PopulateBOProperty(objBO, String.Format(BOPROP_TAXGROUP_DESC, intTaxGroup), .Description)
                    PopulateBOProperty(objBO, String.Format(BOPROP_TAXGROUP_COMPMETHOD, intTaxGroup), .ComputeMethodID)
                    PopulateBOProperty(objBO, String.Format(BOPROP_TAXGROUP_PERCTFLAG, intTaxGroup), .PercentFlagID)
                    PopulateBOProperty(objBO, String.Format(BOPROP_TAXGROUP_PERCT, intTaxGroup), .Percent.ToString())
                End With
            End If
        Next

        If intErrNum > 0 Then 'User control GUI error
            Array.Resize(errMsg, intErrNum)
            MasterPage.MessageController.AddErrorAndShow(errMsg)
            Throw New GUIException("", "")
        End If

        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        State.MyBO = New CountryTax
        PopulateFormFromBOs()
        EnableDisableFields()
        TestForRegions(False)
    End Sub

    Protected Sub CreateNewWithCopy()

        State.IsACopy = True
        PopulateBOsFromForm()

        Dim newObj As New CountryTax
        newObj.Copy(State.MyBO)

        State.MyBO = newObj
        State.MyBO.SetEffectiveExpirationDates()

        PopulateFormFromBOs()

        'create the backup copy
        State.ScreenSnapShotBO = New CountryTax
        State.ScreenSnapShotBO.Clone(State.MyBO)
        State.IsACopy = False

        TestForRegions(False)
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
                    'Me.ReturnToTabHomePage()
                Case ElitaPlusPage.DetailPageCommand.New_
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Delete
                    State.MyBO.DeleteAndSave()
                    State.HasDataChanged = True
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    'Me.ReturnToTabHomePage()
                Case ElitaPlusPage.DetailPageCommand.New_
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    MasterPage.MessageController.AddErrorAndShow(State.LastErrMsg)
                Case ElitaPlusPage.DetailPageCommand.Delete
            End Select
        End If
        'Clean after consuming the action
        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        HiddenSaveChangesPromptResponse.Value = String.Empty
    End Sub

    Private Sub LoadRegionTaxList()

        lstRegionTax.Items.Clear()

        If lstRegion.SelectedIndex = -1 Then
            Return
        End If

        Dim oRegionTaxList As DataView = RegionTax.getList(GetSelectedItem(lstRegion), State.MyBO.TaxTypeId,
                                           State.MyBO.ProductTaxTypeId, State.MyBO.DealerId)
        If oRegionTaxList.Count > 0 Then
            For i As Integer = 0 To oRegionTaxList.Count - 1
                lstRegionTax.Items.Add(New WebControls.ListItem() With {.Text = oRegionTaxList(i)("ListFields"), .Value = New Guid(CType(oRegionTaxList(i)("region_tax_id"), Byte())).ToString()})
            Next
        End If

    End Sub
#End Region

#Region "Button Clicks"

    Private Sub btnCancel_Click(sender As Object, e As System.EventArgs) Handles btnCancel.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
        Catch ex As Threading.ThreadAbortException
        Catch ex As BOValidationException
            HandleErrors(ex, MasterPage.MessageController)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            'save a copy in case there is error on save
            If State.ScreenSnapShotBO Is Nothing Then State.ScreenSnapShotBO = New CountryTax
            State.ScreenSnapShotBO.Clone(State.MyBO)
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                Try
                    State.MyBO.Save()
                Catch ex As Exception
                    'restore the original BO
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                    State.ScreenSnapShotBO = Nothing
                    Throw ex
                End Try
                State.HasDataChanged = True
                PopulateFormFromBOs()
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
            Else
                MasterPage.MessageController.AddWarning(Message.MSG_RECORD_NOT_SAVED)
                'Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(sender As Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New CountryTax(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New CountryTax
            End If
            PopulateFormFromBOs()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub dlstTaxType_WRITE_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles dlstTaxType_WRITE.SelectedIndexChanged
        Dim oExistingCountryTax As CountryTax = State.MyBO
        Dim oCountryId, oTaxTypeId As Guid
        Dim oCountry As Country
        Try
            If oExistingCountryTax.IsNew Then
                oExistingCountryTax.TaxTypeId = GetSelectedItem(dlstTaxType_WRITE)
                oExistingCountryTax.CountryId = GetSelectedItem(cboCountry)
                '  oExistingCountryTax.ProductTaxTypeId = Me.GetSelectedItem(cboProductTaxType)
                If (Not oExistingCountryTax.TaxTypeId.Equals(Guid.Empty)) AndAlso _
                (Not oExistingCountryTax.CountryId.Equals(Guid.Empty)) AndAlso _
                 (Not oExistingCountryTax.ProductTaxTypeId.Equals(Guid.Empty)) Then
                    oExistingCountryTax.SetEffectiveExpirationDates()
                    txtEffectiveDate.Text = GetDateFormattedStringNullable(oExistingCountryTax.EffectiveDate.Value)
                    txtExpirationDate.Text = GetDateFormattedStringNullable(oExistingCountryTax.ExpirationDate.Value)
                End If
            End If
            oTaxTypeId = GetSelectedItem(dlstTaxType_WRITE)
            oCountryId = GetSelectedItem(cboCountry)
            oCountry = New Country(oCountryId)
            SetTaxByProductType(oCountry, oTaxTypeId)

            State.TaxTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_TAX_TYPES, oTaxTypeId)
            If claimTaxTypes.Contains(State.TaxTypeCode) Then
                'remove item " P; Compute on Product Price as it is not applicable to claim taxes                
                LoadTaxGroupUserControls()
                Dim oUserControl As CountryTaxUserControl
                Dim intTaxGroup As Integer
                For intTaxGroup = 1 To CountryTax.NumberOfTaxGroups
                    oUserControl = CType(mUserControlArray(intTaxGroup - 1), CountryTaxUserControl)
                    oUserControl.LoadProperties("P")
                Next
                ControlMgr.SetVisibleControl(Me, tdWithholdingCheck, True)
            Else
                LoadTaxGroupUserControls()
                Dim oUserControl As CountryTaxUserControl
                Dim intTaxGroup As Integer
                For intTaxGroup = 1 To CountryTax.NumberOfTaxGroups
                    oUserControl = CType(mUserControlArray(intTaxGroup - 1), CountryTaxUserControl)
                    oUserControl.LoadProperties()
                Next
                ControlMgr.SetVisibleControl(Me, tdWithholdingCheck, False)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub cboProductTaxType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProductTaxType.SelectedIndexChanged
        Dim oExistingCountryTax As CountryTax = State.MyBO
        Try
            If oExistingCountryTax.IsNew Then
                oExistingCountryTax.CompanyTypeId = GetSelectedItem(cboCompanyType)
                oExistingCountryTax.CountryId = GetSelectedItem(cboCountry)
                oExistingCountryTax.ProductTaxTypeId = GetSelectedItem(cboProductTaxType)
                If (Not oExistingCountryTax.TaxTypeId.Equals(Guid.Empty)) AndAlso _
                (Not oExistingCountryTax.CountryId.Equals(Guid.Empty)) AndAlso _
                 (Not oExistingCountryTax.ProductTaxTypeId.Equals(Guid.Empty)) Then
                    oExistingCountryTax.SetEffectiveExpirationDates()
                    txtEffectiveDate.Text = GetDateFormattedStringNullable(oExistingCountryTax.EffectiveDate.Value)
                    txtExpirationDate.Text = GetDateFormattedStringNullable(oExistingCountryTax.ExpirationDate.Value)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboCountry_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboCountry.SelectedIndexChanged
        Dim oExistingCountryTax As CountryTax = State.MyBO
        Try
            If oExistingCountryTax.IsNew Then
                oExistingCountryTax.TaxTypeId = GetSelectedItem(dlstTaxType_WRITE)
                oExistingCountryTax.CountryId = GetSelectedItem(cboCountry)
                If (Not oExistingCountryTax.TaxTypeId.Equals(Guid.Empty)) AndAlso _
                    (Not oExistingCountryTax.CountryId.Equals(Guid.Empty)) AndAlso _
                   (Not oExistingCountryTax.ProductTaxTypeId.Equals(Guid.Empty)) Then
                    oExistingCountryTax.SetEffectiveExpirationDates()
                    txtEffectiveDate.Text = GetDateFormattedStringNullable(oExistingCountryTax.EffectiveDate.Value)
                    txtExpirationDate.Text = GetDateFormattedStringNullable(oExistingCountryTax.ExpirationDate.Value)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboCompanyType_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboCompanyType.SelectedIndexChanged
        Dim oExistingCountryTax As CountryTax = State.MyBO
        Try
            If oExistingCountryTax.IsNew Then
                oExistingCountryTax.CompanyTypeId = GetSelectedItem(cboCompanyType)
                oExistingCountryTax.CountryId = GetSelectedItem(cboCountry)
                If (Not oExistingCountryTax.TaxTypeId.Equals(Guid.Empty)) AndAlso _
                (Not oExistingCountryTax.CountryId.Equals(Guid.Empty)) AndAlso _
                 (Not oExistingCountryTax.ProductTaxTypeId.Equals(Guid.Empty)) Then
                    oExistingCountryTax.SetEffectiveExpirationDates()
                    txtEffectiveDate.Text = GetDateFormattedStringNullable(oExistingCountryTax.EffectiveDate.Value)
                    txtExpirationDate.Text = GetDateFormattedStringNullable(oExistingCountryTax.ExpirationDate.Value)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moDealerMultipleDrop_SelectedDropChanged(aSrc As MultipleColumnDDLabelControl_New) Handles moDealerMultipleDrop.SelectedDropChanged
        Dim oExistingCountryTax As CountryTax = State.MyBO
        Try
            If oExistingCountryTax.IsNew Then
                oExistingCountryTax.CompanyTypeId = GetSelectedItem(cboCompanyType)
                oExistingCountryTax.CountryId = GetSelectedItem(cboCountry)
                oExistingCountryTax.DealerId = GetSelectedItem(moDealerMultipleDrop.CodeDropDown)
                If (Not oExistingCountryTax.TaxTypeId.Equals(Guid.Empty)) AndAlso _
                (Not oExistingCountryTax.CountryId.Equals(Guid.Empty)) AndAlso _
                 (Not oExistingCountryTax.ProductTaxTypeId.Equals(Guid.Empty)) Then
                    oExistingCountryTax.SetEffectiveExpirationDates()
                    txtEffectiveDate.Text = GetDateFormattedStringNullable(oExistingCountryTax.EffectiveDate.Value)
                    txtExpirationDate.Text = GetDateFormattedStringNullable(oExistingCountryTax.ExpirationDate.Value)
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub lstRegion_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles lstRegion.SelectedIndexChanged
        Try
            LoadRegionTaxList()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNewRegionTax_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNewRegionTax_WRITE.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Try
                    'Call the Region tax Detail screen (RegionTaxes.aspx)
                    callPage(RegionTaxes.URL, New RegionTaxes.Parameters(State.MyBO, Nothing,
                        dlstTaxType_WRITE.SelectedItem.Text, cboProductTaxType.SelectedItem.Text,
                        GetSelectedItem(lstRegion), lstRegion.SelectedItem.Text,
                         GetSelectedItem(moDealerMultipleDrop.CodeDropDown), moDealerMultipleDrop.DescDropDown.SelectedItem.Text, moDealerMultipleDrop.CodeDropDown.SelectedItem.Text,
                         GetSelectedItem(cboCompanyType), cboCompanyType.SelectedItem.Text))
                Catch ex As Threading.ThreadAbortException
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub lstRegionTax_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles lstRegionTax.SelectedIndexChanged
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Try
                    'Call the Region tax Detail screen (RegionTaxes.aspx)
                    callPage(RegionTaxes.URL, New RegionTaxes.Parameters(State.MyBO,
                        GetSelectedItem(lstRegionTax), dlstTaxType_WRITE.SelectedItem.Text,
                        cboProductTaxType.SelectedItem.Text, GetSelectedItem(lstRegion), lstRegion.SelectedItem.Text,
                        GetSelectedItem(moDealerMultipleDrop.CodeDropDown), moDealerMultipleDrop.DescDropDown.SelectedItem.Text, moDealerMultipleDrop.CodeDropDown.SelectedItem.Text,
                        GetSelectedItem(cboCompanyType), cboCompanyType.SelectedItem.Text))
                Catch ex As Threading.ThreadAbortException
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Public methods"
    Public Sub TestForRegions(useRegion As Boolean)

        Dim intTaxGroup As Integer
        Dim oUserControl As CountryTaxUserControl
        Dim oUseRegion As Boolean

        If useRegion Then 'region tax selected
            If Not State.showRegionTax Then
                State.showRegionTax = True
                EnableDisableRegionTaxes(True)
            End If
        Else 'region tax is de-selected
            'if region tax is enabled now,
            'check to see whether there are other tax groups still use region tax
            If State.showRegionTax Then
                LoadTaxGroupUserControls()
                oUseRegion = False
                For intTaxGroup = 0 To CountryTax.NumberOfTaxGroups - 1
                    oUserControl = CType(mUserControlArray(intTaxGroup), CountryTaxUserControl)
                    If oUserControl.isRegion Then
                        'find one, done
                        oUseRegion = True
                        Exit For
                    End If
                Next

                'no tax group uses region tax, hide the section
                If Not oUseRegion Then
                    State.showRegionTax = False
                    EnableDisableRegionTaxes(False)
                End If
            End If
        End If

    End Sub
#End Region


End Class
