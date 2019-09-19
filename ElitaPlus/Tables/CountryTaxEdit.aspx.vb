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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As CountryTax, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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
        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(Me.PAGETITLE)
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.SetFormTab(Me.PAGETAB)
        Me.SetFormTitle(Me.PAGETITLE)

        Me.MasterPage.MessageController.Clear_Hide()

        'Set the readonly attributes for date textboxes
        txtEffectiveDate.Attributes.Add("readonly", "readonly")
        txtExpirationDate.Attributes.Add("readonly", "readonly")

        Try
            Me.MasterPage.MessageController.Clear()
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(Me.PAGETAB)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(Me.PAGETITLE)
            Me.UpdateBreadCrum()

            If Not Me.IsPostBack Then
                'Date Calendars
                Me.AddCalendar(Me.btnEffectiveDate, Me.txtEffectiveDate)
                Me.AddCalendar(Me.btnExpirationDate, Me.txtExpirationDate)
                'Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)

                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New CountryTax()
                End If

                PopulateDropdowns()
                'PopulateDealerDropdown(Me.cboDealer)
                Me.PopulateFormFromBOs()
            End If
            BindBoPropertiesToLabels()
            CheckIfComingFromSaveConfirm()

            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

        Me.ShowMissingTranslations(Me.MasterPage.MessageController)

    End Sub

    Private Sub Page_LoadComplete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.LoadComplete
        'reset control status if there is not error, otherwise keep the original
        EnableDisableFields()
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New CountryTax(CType(Me.CallingParameters, Guid))
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Me.IsReturningFromChild = True

        Try
            If Me.CalledUrl = RegionTaxes.URL Then
                Dim retObj As RegionTaxes.ReturnType = CType(ReturnPar, RegionTaxes.ReturnType)

                If Not retObj Is Nothing Then
                    Me.State.MyBO = retObj.EditingBo
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"

    Protected Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, COUNTRYID_PROPERTY, Me.lblCountry)
        Me.BindBOPropertyToLabel(Me.State.MyBO, TAXTYPEID_PROPERTY, Me.lblTaxType)
        Me.BindBOPropertyToLabel(Me.State.MyBO, EFFECTIVEDATE_PROPERTY, Me.lblEffectiveDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, EXPIRATIONDATE_PROPERTY, Me.lblExpirationDate)
        Me.BindBOPropertyToLabel(Me.State.MyBO, COMPANYTYPEID_PROPERTY, Me.lblCompanyType)
        Me.BindBOPropertyToLabel(Me.State.MyBO, PRODUCT_TAX_TYPE_ID_PROPERTY, Me.lblProductTaxType)
        'Me.BindBOPropertyToLabel(Me.State.MyBO, DEALER_ID_PROPERTY, Me.lblDealer)

        Me.ClearGridHeadersAndLabelsErrSign()
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
        Dim theBO As CountryTax = Me.State.MyBO
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
                    Me.EnableDisableEffectiveDate(True)
                End If
            End If
        End If

    End Sub

    Protected Sub EnableDisableExpirationDate(ByVal enabled As Boolean)
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

    Protected Sub EnableDisableEffectiveDate(ByVal enabled As Boolean)
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
    Protected Sub EnableDisableTaxType(ByVal enabled As Boolean)
        If enabled Then
            dlstTaxType_WRITE.Visible = True
            dlstTaxType_WRITETextBox.Visible = False
        Else
            dlstTaxType_WRITE.Visible = False
            dlstTaxType_WRITETextBox.Visible = True
            dlstTaxType_WRITETextBox.Text = dlstTaxType_WRITE.SelectedItem.ToString()
        End If
    End Sub

    Protected Sub EnableDisableCompanyType(ByVal enabled As Boolean)
        If enabled Then
            ControlMgr.SetEnableControl(Me, Me.cboCompanyType, True)
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

    Protected Sub EnableDisableProductTaxType(ByVal enabled As Boolean)
        If enabled Then
            ControlMgr.SetEnableControl(Me, Me.cboProductTaxType, True)
            cboProductTaxType.Style.Add("background-color", "")
        Else
            ControlMgr.SetEnableControl(Me, cboProductTaxType, False)
            cboProductTaxType.Style.Add("background-color", "LightGray")
        End If
    End Sub

    Private Sub SetTaxByProductType(ByVal oCountry As Country, ByVal oTaxTypeId As Guid)
        Dim oYesId, oTaxTypePosId, oTaxTypeCommId As Guid

        Me.State.TaxTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_TAX_TYPES, oTaxTypeId)

        oYesId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(
                    Codes.YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId), Codes.YESNO_Y)
        oTaxTypePosId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(
                Codes.TAX_TYPE, Authentication.LangId), Codes.TAX_TYPE__POS)
        oTaxTypeCommId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(
                Codes.TAX_TYPE, Authentication.LangId), Codes.TAX_TYPE__COMMISSIONS)
        If (oCountry.TaxByProductTypeId.Equals(oYesId) AndAlso
                (oTaxTypeId.Equals(oTaxTypePosId) OrElse oTaxTypeId.Equals(oTaxTypeCommId))) Then
            ' Yes => Visible
            ControlMgr.SetVisibleControl(Me, Me.lblProductTaxType, True)
            ControlMgr.SetVisibleControl(Me, Me.cboProductTaxType, True)
        Else    ' No => Invisible
            Me.SetSelectedItem(Me.cboProductTaxType,
                LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(
                Codes.PRODUCT_TAX_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), Codes.PRODUCT_TAX_TYPE__ALL))
            ControlMgr.SetVisibleControl(Me, Me.lblProductTaxType, False)
            ControlMgr.SetVisibleControl(Me, Me.cboProductTaxType, False)
        End If
    End Sub


    Protected Sub EnableDisableCountry(ByVal enabled As Boolean)
        If enabled Then
            ControlMgr.SetEnableControl(Me, Me.cboCountry, True)
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

    Protected Sub EnableDisableDealer(ByVal enabled As Boolean)
        If Not Me.moDealerMultipleDrop.VisibleDD Then
            Me.moDealerMultipleDrop.Visible = True
        End If
        If enabled Then
            Me.moDealerMultipleDrop.Mode = 1
            Me.moDealerMultipleDrop.CodeDropDown.Style.Add("background-color", "")
            Me.moDealerMultipleDrop.DescDropDown.Style.Add("background-color", "")
        Else
            If Me.moDealerMultipleDrop.CodeDropDown.SelectedIndex <> Me.BLANK_ITEM_SELECTED Then
                Me.moDealerMultipleDrop.CodeDropDown.Style.Add("background-color", "LightGray")
                Me.moDealerMultipleDrop.DescDropDown.Style.Add("background-color", "LightGray")
                moDealerMultipleDrop.CodeTextBox.Text = moDealerMultipleDrop.CodeDropDown.SelectedItem.ToString()
                moDealerMultipleDrop.DescriptionTextBox.Text = moDealerMultipleDrop.DescDropDown.SelectedItem.ToString()
                Me.moDealerMultipleDrop.Mode = 2

            Else
                Me.moDealerMultipleDrop.Visible = False
            End If

        End If
    End Sub

    Protected Sub PopulateDropdowns()
        'Dim oTaxTypeList As DataView = LookupListNew.DropdownLookupList(TAX_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True)
        Me.dlstTaxType_WRITE.Items.Clear()
        Me.cboProductTaxType.Items.Clear()
        'Me.BindListControlToDataView(dlstTaxType_WRITE, oTaxTypeList)--TTYP
        dlstTaxType_WRITE.Populate(CommonConfigManager.Current.ListManager.GetList("TTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
              .AddBlankItem = True
                })
        'Me.BindListControlToDataView(Me.cboCompanyType, LookupListNew.DropdownLookupList(LookupListNew.LK_COTYP, Authentication.LangId, False), , , False)
        Me.cboCompanyType.Populate(CommonConfigManager.Current.ListManager.GetList("COTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
        '  Me.BindListControlToDataView(Me.cboProductTaxType, LookupListNew.DropdownLookupList(
        '.LK_PRODUCT_TAX_TYPE, Authentication.LangId, False), , , False) 'PTT
        Me.cboProductTaxType.Populate(CommonConfigManager.Current.ListManager.GetList("PTT", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions())
        ' Me.BindListControlToDataView(Me.cboCountry, LookupListNew.GetUserCountriesLookupList()) 'UserCountries
        Dim listcontext As ListContext = New ListContext()
        Dim countryLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.Country, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
        Dim list As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries

        Dim filteredList As ListItem() = (From x In countryLkl
                                          Where list.Contains(x.ListItemId)
                                          Select x).ToArray()
        Me.cboCountry.Populate(filteredList, New PopulateOptions() With {
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
        Dim oCountryTax As CountryTax = Me.State.MyBO
        '  Dim oYesId, oTaxTypePosId, oTaxTypeCommId As Guid
        With Me.State.MyBO
            Me.PopulateControlFromBOProperty(Me.txtEffectiveDate, .EffectiveDate)
            Me.PopulateControlFromBOProperty(Me.txtExpirationDate, .ExpirationDate)
            Me.SetSelectedItem(Me.dlstTaxType_WRITE, .TaxTypeId)
            Me.SetSelectedItem(Me.cboCompanyType, .CompanyTypeId)
            Me.SetSelectedItem(Me.cboCountry, .CountryId)
            'REQ-1150
            'Me.SetSelectedItem(Me.cboDealer, .DealerId)

            If Not Me.State.MyBO.TaxTypeId.Equals(System.Guid.Empty) Then
                Me.moDealerMultipleDrop.SelectedGuid = .DealerId
            Else
                Me.moDealerMultipleDrop.SelectedIndex = Me.BLANK_ITEM_SELECTED
            End If
            ' ProductTaxType
            If Me.State.MyBO.ProductTaxTypeId.Equals(System.Guid.Empty) Then
                Me.SetSelectedItem(Me.cboProductTaxType,
                    LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(
                    Codes.PRODUCT_TAX_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), Codes.PRODUCT_TAX_TYPE__ALL))
            Else
                Me.SetSelectedItem(Me.cboProductTaxType, .ProductTaxTypeId)
            End If

            Me.State.TaxTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_TAX_TYPES, .TaxTypeId)

            If claimTaxTypes.Contains(Me.State.TaxTypeCode) Then
                ControlMgr.SetVisibleControl(Me, Me.tdWithholdingCheck, True)
                If Not .ApplyWithholdingFlag.Equals(String.Empty) AndAlso .ApplyWithholdingFlag.Equals("Y") Then
                    Me.CheckBoxApplyWithholding.Checked = True
                Else
                    Me.CheckBoxApplyWithholding.Checked = False
                End If
            Else
                ControlMgr.SetVisibleControl(Me, Me.tdWithholdingCheck, False)
            End If

            oCountry = New Country(.CountryId)
            SetTaxByProductType(oCountry, .TaxTypeId)

            LoadTaxGroupUserControls(True)
        End With

    End Sub

    Private Sub LoadTaxGroupUserControls(Optional ByVal setCtlProperties As Boolean = False)

        mUserControlArray.Clear()
        mUserControlArray.Add(Me.CountryTaxUserControl1)
        mUserControlArray.Add(Me.CountryTaxUserControl2)
        mUserControlArray.Add(Me.CountryTaxUserControl3)
        mUserControlArray.Add(Me.CountryTaxUserControl4)
        mUserControlArray.Add(Me.CountryTaxUserControl5)
        mUserControlArray.Add(Me.CountryTaxUserControl6)

        If setCtlProperties Then SetUserControlsProperties()

    End Sub

    Private Sub SetUserControlsProperties()

        Dim intTaxGroup As Integer
        Dim oUserControl As CountryTaxUserControl
        Dim oUseRegion As Boolean = False
        Dim oTG As CountryTax.TaxGroupData

        With Me.State.MyBO
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
                If claimTaxTypes.Contains(Me.State.TaxTypeCode) Then
                    'remove item " P; Compute on Product Price as it Is Not applicable to claim taxes
                    strItemToRemove = "P"
                End If

                oUserControl.LoadProperties(strItemToRemove)

                If (Not oUseRegion) AndAlso oUserControl.UseRegion Then
                    oUseRegion = True
                End If
            Next

            If oUseRegion Then
                Me.State.showRegionTax = True
                EnableDisableRegionTaxes(True)
            End If
        End With

    End Sub

    Private Sub EnableDisableRegionTaxes(ByVal enabled As Boolean)
        If enabled Then

            'show the controls
            ControlMgr.SetVisibleControl(Me, lstRegion, True)
            ControlMgr.SetVisibleControl(Me, lstRegionTax, True)
            ControlMgr.SetVisibleControl(Me, lblRegionList, True)
            ControlMgr.SetVisibleControl(Me, lblRegionTax, True)
            ControlMgr.SetVisibleControl(Me, btnNewRegionTax_WRITE, True)

            'load the list data
            '  Dim oRegionList As DataView = LookupListNew.GetRegionLookupList(ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id)
            Me.lstRegion.Items.Clear()
            'Me.BindListControlToDataView(Me.lstRegion, oRegionList, , , False) 'RegionsByCountry
            Dim listcontext As ListContext = New ListContext()
            listcontext.CountryId = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID).Id
            Me.lstRegion.Populate(CommonConfigManager.Current.ListManager.GetList("RegionsByCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext), New PopulateOptions())
            Me.lstRegion.SelectedIndex = 0

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

        Dim objBO As CountryTax = Me.State.MyBO
        'If Me.dlstTaxType_WRITE.SelectedValue.Trim() = "00000000-0000-0000-0000-000000000000" Then
        'GUI_FIELDS_EMPTY_ERR
        '  Me.ErrControllerMaster.AddErrorAndShow(GUI_EMPTY_FORM_ERROR)
        '  Throw New GUIException("", "")
        ' Else
        Me.PopulateBOProperty(objBO, "TaxTypeId", Me.dlstTaxType_WRITE)
        ' End If
        Me.PopulateBOProperty(objBO, "EffectiveDate", Me.txtEffectiveDate)
        Me.PopulateBOProperty(objBO, "ExpirationDate", Me.txtExpirationDate)
        Me.PopulateBOProperty(objBO, "CompanyTypeId", Me.cboCompanyType)
        Me.PopulateBOProperty(objBO, PRODUCT_TAX_TYPE_ID_PROPERTY, Me.cboProductTaxType)
        Me.PopulateBOProperty(objBO, "CountryId", Me.cboCountry)
        Me.PopulateBOProperty(objBO, "DealerId", Me.moDealerMultipleDrop.SelectedGuid)
        If claimTaxTypes.Contains(State.TaxTypeCode) AndAlso Me.CheckBoxApplyWithholding.Checked Then
            Me.PopulateBOProperty(objBO, "ApplyWithholdingFlag", "Y")
        Else
            Me.PopulateBOProperty(objBO, "ApplyWithholdingFlag", "N")
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
                    Me.PopulateBOProperty(objBO, String.Format(BOPROP_TAXGROUP_DESC, intTaxGroup), .Description)
                    Me.PopulateBOProperty(objBO, String.Format(BOPROP_TAXGROUP_COMPMETHOD, intTaxGroup), .ComputeMethodID)
                    Me.PopulateBOProperty(objBO, String.Format(BOPROP_TAXGROUP_PERCTFLAG, intTaxGroup), .PercentFlagID)
                    Me.PopulateBOProperty(objBO, String.Format(BOPROP_TAXGROUP_PERCT, intTaxGroup), .Percent.ToString())
                End With
            End If
        Next

        If intErrNum > 0 Then 'User control GUI error
            Array.Resize(errMsg, intErrNum)
            Me.MasterPage.MessageController.AddErrorAndShow(errMsg)
            Throw New GUIException("", "")
        End If

        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        Me.State.MyBO = New CountryTax
        Me.PopulateFormFromBOs()
        Me.EnableDisableFields()
        TestForRegions(False)
    End Sub

    Protected Sub CreateNewWithCopy()

        Me.State.IsACopy = True
        Me.PopulateBOsFromForm()

        Dim newObj As New CountryTax
        newObj.Copy(Me.State.MyBO)

        Me.State.MyBO = newObj
        Me.State.MyBO.SetEffectiveExpirationDates()

        Me.PopulateFormFromBOs()

        'create the backup copy
        Me.State.ScreenSnapShotBO = New CountryTax
        Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
        Me.State.IsACopy = False

        TestForRegions(False)
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
                    'Me.ReturnToTabHomePage()
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.State.MyBO.DeleteAndSave()
                    Me.State.HasDataChanged = True
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    'Me.ReturnToTabHomePage()
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.MasterPage.MessageController.AddErrorAndShow(Me.State.LastErrMsg)
                Case ElitaPlusPage.DetailPageCommand.Delete
            End Select
        End If
        'Clean after consuming the action
        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
        Me.HiddenSaveChangesPromptResponse.Value = String.Empty
    End Sub

    Private Sub LoadRegionTaxList()

        lstRegionTax.Items.Clear()

        If lstRegion.SelectedIndex = -1 Then
            Return
        End If

        Dim oRegionTaxList As DataView = RegionTax.getList(Me.GetSelectedItem(Me.lstRegion), Me.State.MyBO.TaxTypeId,
                                           Me.State.MyBO.ProductTaxTypeId, Me.State.MyBO.DealerId)
        If oRegionTaxList.Count > 0 Then
            For i As Integer = 0 To oRegionTaxList.Count - 1
                lstRegionTax.Items.Add(New WebControls.ListItem() With {.Text = oRegionTaxList(i)("ListFields"), .Value = New Guid(CType(oRegionTaxList(i)("region_tax_id"), Byte())).ToString()})
            Next
        End If

    End Sub
#End Region

#Region "Button Clicks"

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
        Catch ex As Threading.ThreadAbortException
        Catch ex As BOValidationException
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try
            'save a copy in case there is error on save
            If Me.State.ScreenSnapShotBO Is Nothing Then Me.State.ScreenSnapShotBO = New CountryTax
            Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Try
                    Me.State.MyBO.Save()
                Catch ex As Exception
                    'restore the original BO
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                    Me.State.ScreenSnapShotBO = Nothing
                    Throw ex
                End Try
                Me.State.HasDataChanged = True
                Me.PopulateFormFromBOs()
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.MasterPage.MessageController.AddWarning(Message.MSG_RECORD_NOT_SAVED)
                'Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_Write_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New CountryTax(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New CountryTax
            End If
            Me.PopulateFormFromBOs()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub dlstTaxType_WRITE_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlstTaxType_WRITE.SelectedIndexChanged
        Dim oExistingCountryTax As CountryTax = Me.State.MyBO
        Dim oCountryId, oTaxTypeId As Guid
        Dim oCountry As Country
        Try
            If oExistingCountryTax.IsNew Then
                oExistingCountryTax.TaxTypeId = Me.GetSelectedItem(dlstTaxType_WRITE)
                oExistingCountryTax.CountryId = Me.GetSelectedItem(cboCountry)
                '  oExistingCountryTax.ProductTaxTypeId = Me.GetSelectedItem(cboProductTaxType)
                If (Not oExistingCountryTax.TaxTypeId.Equals(Guid.Empty)) AndAlso _
                (Not oExistingCountryTax.CountryId.Equals(Guid.Empty)) AndAlso _
                 (Not oExistingCountryTax.ProductTaxTypeId.Equals(Guid.Empty)) Then
                    oExistingCountryTax.SetEffectiveExpirationDates()
                    'Sridhar Me.txtEffectiveDate.Text = oExistingCountryTax.EffectiveDate.Value.ToString(DATE_FORMAT, CultureInfo.CurrentCulture)
                    'Me.txtExpirationDate.Text = oExistingCountryTax.ExpirationDate.Value.ToString(DATE_FORMAT, CultureInfo.CurrentCulture)
                    Me.txtEffectiveDate.Text = GetDateFormattedString(oExistingCountryTax.EffectiveDate.Value)
                    Me.txtExpirationDate.Text = GetDateFormattedString(oExistingCountryTax.ExpirationDate.Value)
                End If
            End If
            oTaxTypeId = GetSelectedItem(dlstTaxType_WRITE)
            oCountryId = Me.GetSelectedItem(Me.cboCountry)
            oCountry = New Country(oCountryId)
            SetTaxByProductType(oCountry, oTaxTypeId)

            Me.State.TaxTypeCode = LookupListNew.GetCodeFromId(LookupListNew.LK_TAX_TYPES, oTaxTypeId)
            If claimTaxTypes.Contains(Me.State.TaxTypeCode) Then
                'remove item " P; Compute on Product Price as it is not applicable to claim taxes                
                LoadTaxGroupUserControls()
                Dim oUserControl As CountryTaxUserControl
                Dim intTaxGroup As Integer
                For intTaxGroup = 1 To CountryTax.NumberOfTaxGroups
                    oUserControl = CType(mUserControlArray(intTaxGroup - 1), CountryTaxUserControl)
                    oUserControl.LoadProperties("P")
                Next
                ControlMgr.SetVisibleControl(Me, Me.tdWithholdingCheck, True)
            Else
                LoadTaxGroupUserControls()
                Dim oUserControl As CountryTaxUserControl
                Dim intTaxGroup As Integer
                For intTaxGroup = 1 To CountryTax.NumberOfTaxGroups
                    oUserControl = CType(mUserControlArray(intTaxGroup - 1), CountryTaxUserControl)
                    oUserControl.LoadProperties()
                Next
                ControlMgr.SetVisibleControl(Me, Me.tdWithholdingCheck, False)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub cboProductTaxType_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboProductTaxType.SelectedIndexChanged
        Dim oExistingCountryTax As CountryTax = Me.State.MyBO
        Try
            If oExistingCountryTax.IsNew Then
                oExistingCountryTax.CompanyTypeId = Me.GetSelectedItem(Me.cboCompanyType)
                oExistingCountryTax.CountryId = Me.GetSelectedItem(cboCountry)
                oExistingCountryTax.ProductTaxTypeId = Me.GetSelectedItem(cboProductTaxType)
                If (Not oExistingCountryTax.TaxTypeId.Equals(Guid.Empty)) AndAlso _
                (Not oExistingCountryTax.CountryId.Equals(Guid.Empty)) AndAlso _
                 (Not oExistingCountryTax.ProductTaxTypeId.Equals(Guid.Empty)) Then
                    oExistingCountryTax.SetEffectiveExpirationDates()
                    'Sridhar Me.txtEffectiveDate.Text = oExistingCountryTax.EffectiveDate.Value.ToString(DATE_FORMAT, CultureInfo.CurrentCulture)
                    'Me.txtExpirationDate.Text = oExistingCountryTax.ExpirationDate.Value.ToString(DATE_FORMAT, CultureInfo.CurrentCulture)
                    Me.txtEffectiveDate.Text = GetDateFormattedString(oExistingCountryTax.EffectiveDate.Value)
                    Me.txtExpirationDate.Text = GetDateFormattedString(oExistingCountryTax.ExpirationDate.Value)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboCountry_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCountry.SelectedIndexChanged
        Dim oExistingCountryTax As CountryTax = Me.State.MyBO
        Try
            If oExistingCountryTax.IsNew Then
                oExistingCountryTax.TaxTypeId = Me.GetSelectedItem(dlstTaxType_WRITE)
                oExistingCountryTax.CountryId = Me.GetSelectedItem(cboCountry)
                If (Not oExistingCountryTax.TaxTypeId.Equals(Guid.Empty)) AndAlso _
                    (Not oExistingCountryTax.CountryId.Equals(Guid.Empty)) AndAlso _
                   (Not oExistingCountryTax.ProductTaxTypeId.Equals(Guid.Empty)) Then
                    oExistingCountryTax.SetEffectiveExpirationDates()
                    'Sridhar Me.txtEffectiveDate.Text = oExistingCountryTax.EffectiveDate.Value.ToString(DATE_FORMAT, CultureInfo.CurrentCulture)
                    'Me.txtExpirationDate.Text = oExistingCountryTax.ExpirationDate.Value.ToString(DATE_FORMAT, CultureInfo.CurrentCulture)
                    Me.txtEffectiveDate.Text = GetDateFormattedString(oExistingCountryTax.EffectiveDate.Value)
                    Me.txtExpirationDate.Text = GetDateFormattedString(oExistingCountryTax.ExpirationDate.Value)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub cboCompanyType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCompanyType.SelectedIndexChanged
        Dim oExistingCountryTax As CountryTax = Me.State.MyBO
        Try
            If oExistingCountryTax.IsNew Then
                oExistingCountryTax.CompanyTypeId = Me.GetSelectedItem(Me.cboCompanyType)
                oExistingCountryTax.CountryId = Me.GetSelectedItem(cboCountry)
                If (Not oExistingCountryTax.TaxTypeId.Equals(Guid.Empty)) AndAlso _
                (Not oExistingCountryTax.CountryId.Equals(Guid.Empty)) AndAlso _
                 (Not oExistingCountryTax.ProductTaxTypeId.Equals(Guid.Empty)) Then
                    oExistingCountryTax.SetEffectiveExpirationDates()
                    'Sridhar Me.txtEffectiveDate.Text = oExistingCountryTax.EffectiveDate.Value.ToString(DATE_FORMAT, CultureInfo.CurrentCulture)
                    'Me.txtExpirationDate.Text = oExistingCountryTax.ExpirationDate.Value.ToString(DATE_FORMAT, CultureInfo.CurrentCulture)
                    Me.txtEffectiveDate.Text = GetDateFormattedString(oExistingCountryTax.EffectiveDate.Value)
                    Me.txtExpirationDate.Text = GetDateFormattedString(oExistingCountryTax.ExpirationDate.Value)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub moDealerMultipleDrop_SelectedDropChanged(ByVal aSrc As MultipleColumnDDLabelControl_New) Handles moDealerMultipleDrop.SelectedDropChanged
        Dim oExistingCountryTax As CountryTax = Me.State.MyBO
        Try
            If oExistingCountryTax.IsNew Then
                oExistingCountryTax.CompanyTypeId = Me.GetSelectedItem(Me.cboCompanyType)
                oExistingCountryTax.CountryId = Me.GetSelectedItem(cboCountry)
                oExistingCountryTax.DealerId = Me.GetSelectedItem(Me.moDealerMultipleDrop.CodeDropDown)
                If (Not oExistingCountryTax.TaxTypeId.Equals(Guid.Empty)) AndAlso _
                (Not oExistingCountryTax.CountryId.Equals(Guid.Empty)) AndAlso _
                 (Not oExistingCountryTax.ProductTaxTypeId.Equals(Guid.Empty)) Then
                    oExistingCountryTax.SetEffectiveExpirationDates()
                    'Sridhar Me.txtEffectiveDate.Text = oExistingCountryTax.EffectiveDate.Value.ToString(DATE_FORMAT, CultureInfo.CurrentCulture)
                    'Me.txtExpirationDate.Text = oExistingCountryTax.ExpirationDate.Value.ToString(DATE_FORMAT, CultureInfo.CurrentCulture)
                    Me.txtEffectiveDate.Text = GetDateFormattedString(oExistingCountryTax.EffectiveDate.Value)
                    Me.txtExpirationDate.Text = GetDateFormattedString(oExistingCountryTax.ExpirationDate.Value)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub lstRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstRegion.SelectedIndexChanged
        Try
            LoadRegionTaxList()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNewRegionTax_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewRegionTax_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Try
                    'Call the Region tax Detail screen (RegionTaxes.aspx)
                    Me.callPage(RegionTaxes.URL, New RegionTaxes.Parameters(Me.State.MyBO, Nothing,
                        Me.dlstTaxType_WRITE.SelectedItem.Text, Me.cboProductTaxType.SelectedItem.Text,
                        GetSelectedItem(Me.lstRegion), Me.lstRegion.SelectedItem.Text,
                         GetSelectedItem(Me.moDealerMultipleDrop.CodeDropDown), Me.moDealerMultipleDrop.DescDropDown.SelectedItem.Text, Me.moDealerMultipleDrop.CodeDropDown.SelectedItem.Text,
                         GetSelectedItem(Me.cboCompanyType), Me.cboCompanyType.SelectedItem.Text))
                Catch ex As Threading.ThreadAbortException
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub lstRegionTax_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstRegionTax.SelectedIndexChanged
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Try
                    'Call the Region tax Detail screen (RegionTaxes.aspx)
                    Me.callPage(RegionTaxes.URL, New RegionTaxes.Parameters(Me.State.MyBO,
                        Me.GetSelectedItem(Me.lstRegionTax), Me.dlstTaxType_WRITE.SelectedItem.Text,
                        Me.cboProductTaxType.SelectedItem.Text, GetSelectedItem(Me.lstRegion), Me.lstRegion.SelectedItem.Text,
                        GetSelectedItem(Me.moDealerMultipleDrop.CodeDropDown), Me.moDealerMultipleDrop.DescDropDown.SelectedItem.Text, Me.moDealerMultipleDrop.CodeDropDown.SelectedItem.Text,
                        GetSelectedItem(Me.cboCompanyType), Me.cboCompanyType.SelectedItem.Text))
                Catch ex As Threading.ThreadAbortException
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Public methods"
    Public Sub TestForRegions(ByVal useRegion As Boolean)

        Dim intTaxGroup As Integer
        Dim oUserControl As CountryTaxUserControl
        Dim oUseRegion As Boolean

        If useRegion Then 'region tax selected
            If Not Me.State.showRegionTax Then
                Me.State.showRegionTax = True
                Me.EnableDisableRegionTaxes(True)
            End If
        Else 'region tax is de-selected
            'if region tax is enabled now,
            'check to see whether there are other tax groups still use region tax
            If Me.State.showRegionTax Then
                Me.LoadTaxGroupUserControls()
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
                    Me.State.showRegionTax = False
                    Me.EnableDisableRegionTaxes(False)
                End If
            End If
        End If

    End Sub
#End Region


End Class
