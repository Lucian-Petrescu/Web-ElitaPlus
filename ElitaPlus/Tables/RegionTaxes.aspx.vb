Imports System.Text
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security

Partial Class RegionTaxes
    Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
    End Sub

    Protected WithEvents ErrorController1 As ErrorController
    Protected WithEvents moTitleLabel As System.Web.UI.WebControls.Label
    Protected WithEvents Label60 As System.Web.UI.WebControls.Label
    Protected WithEvents Label65 As System.Web.UI.WebControls.Label
    Protected WithEvents Label61 As System.Web.UI.WebControls.Label
    Protected WithEvents Label64 As System.Web.UI.WebControls.Label
    Protected WithEvents Label62 As System.Web.UI.WebControls.Label
    Protected WithEvents Label63 As System.Web.UI.WebControls.Label
    Protected WithEvents btnApply As System.Web.UI.WebControls.Button
    Protected WithEvents btnCancel As System.Web.UI.WebControls.Button
    Protected WithEvents Label30 As System.Web.UI.WebControls.Label

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    'User control to load region tax details
    Private mUserControlArray As System.Collections.Generic.List(Of RegionTaxUserControl) = New System.Collections.Generic.List(Of RegionTaxUserControl)

    Protected WithEvents RegionTaxUserControl1 As RegionTaxUserControl
    Protected WithEvents RegionTaxUserControl2 As RegionTaxUserControl
    Protected WithEvents RegionTaxUserControl3 As RegionTaxUserControl
    Protected WithEvents RegionTaxUserControl4 As RegionTaxUserControl
    Protected WithEvents RegionTaxUserControl5 As RegionTaxUserControl
    Protected WithEvents RegionTaxUserControl6 As RegionTaxUserControl

    Public Const PAGETITLE As String = "RegionTax"
    Public Const PAGETAB As String = "TABLES"

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants "
    Public Const URL As String = "RegionTaxes.aspx"
    Public Const DEALER_ID_PROPERTY As String = "DealerId"
    Public Const COMPANY_TYPE_ID_PROPERTY As String = "CompanyTypeXCD"
#End Region

#Region "Page State"

    Class MyState
        Public MyBO As RegionTax
        Public ScreenSnapShotBO As RegionTax
        Public IsNew As Boolean
        Public IsACopy As Boolean
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastErrMsg As String
        Public HasDataChanged As Boolean

        Public MyCountryBO As CountryTax
        Public RegionID As Guid
        Public TaxTypeDesc As String
        Public msProductTaxTypeDesc As String
        Public RegionDesc As String

        'DEF-1150
        Public DealerID As Guid
        Public DealerDesc As String
        Public Dealer As String

        Public CompanyTypeXCD As String
        Public CompanyType As String

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

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As CountryTax
        Public BoChanged As Boolean = False

        Public Sub New(LastOp As DetailPageCommand, curEditingBo As CountryTax, Optional ByVal boChanged As Boolean = False)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.BoChanged = boChanged
        End Sub
    End Class
#End Region

#Region "Page Parameters"
    Public Class Parameters
        Public CountryTaxBO As CountryTax
        Public TaxTypeDesc As String
        Public msProductTaxTypeDesc As String
        Public RegionTaxID As Guid
        Public RegionID As Guid
        Public RegionDesc As String
        Public DealerID As Guid
        Public DealerDesc As String
        Public Dealer As String
        Public CompanyTypeId As Guid
        Public CompanyType As String

        Public Sub New(countryTaxBO As CountryTax, regionTaxID As Guid, taxTypeDesc As String,
                       sProductTaxTypeDesc As String, regionID As Guid, regionDesc As String,
                       dealerID As Guid, dealerDesc As String, dealer As String, CompanyTypeId As Guid, CompanyType As String)
            Me.CountryTaxBO = countryTaxBO
            Me.RegionTaxID = regionTaxID
            Me.TaxTypeDesc = taxTypeDesc
            msProductTaxTypeDesc = sProductTaxTypeDesc
            Me.RegionID = regionID
            Me.RegionDesc = regionDesc
            Me.DealerID = dealerID
            Me.DealerDesc = dealerDesc
            Me.Dealer = dealer
            Me.CompanyTypeId = CompanyTypeId
            Me.CompanyType = CompanyType
        End Sub
    End Class
#End Region

#Region "PAGE EVENTS"

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End If
    End Sub

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        txtEffectiveDate.Attributes.Add("readonly", "readonly")
        txtExpirationDate.Attributes.Add("readonly", "readonly")

        Try
            MasterPage.MessageController.Clear()
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            UpdateBreadCrum()

            If Not IsPostBack Then
                'Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                AddCalendar(btnEffectiveDate, txtEffectiveDate)
                AddCalendar(btnExpirationDate, txtExpirationDate)

                If State.MyBO Is Nothing Then
                    State.MyBO = New RegionTax()
                End If

                txtTaxType.Text = State.TaxTypeDesc
                txtProductTaxType.Text = State.msProductTaxTypeDesc
                lblRegion.Text = State.RegionDesc

                PopulateFormFromBOs()
            End If
            BindBOPropertyToLabel(State.MyBO, DEALER_ID_PROPERTY, lblDealer)
            BindBOPropertyToLabel(State.MyBO, COMPANY_TYPE_ID_PROPERTY, lblCompanyType)
            CheckIfComingFromSaveConfirm()

            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If

            If Not State.DealerDesc.IsNullOrEmpty(State.DealerDesc) And Not State.Dealer.IsNullOrEmpty(State.Dealer) Then
                Label10.Visible = True
                Label11.Visible = True
                lblDealer.Visible = True
                Label12.Visible = True
                lblDealerDesc.Visible = True
                lblDealer.Text = State.Dealer
                lblDealerDesc.Text = State.DealerDesc
            Else
                Label10.Visible = False
                Label11.Visible = False
                lblDealer.Visible = False
                Label12.Visible = False
                lblDealerDesc.Visible = False
            End If

            If Not State.CompanyType.IsNullOrEmpty(State.CompanyType) Then
                Label66.Visible = True
                lblCompanyType.Visible = True
                lblCompanyType.Text = State.CompanyType

            Else
                Label66.Visible = False
                lblCompanyType.Visible = False
            End If



        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub Page_LoadComplete(sender As System.Object, e As System.EventArgs) Handles MyBase.LoadComplete
        EnableDisableFields()
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Dim objParam As Parameters
        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                objParam = CType(CallingParameters, Parameters)
                State.MyCountryBO = objParam.CountryTaxBO
                State.RegionID = objParam.RegionID
                State.TaxTypeDesc = objParam.TaxTypeDesc
                State.msProductTaxTypeDesc = objParam.msProductTaxTypeDesc
                State.RegionDesc = objParam.RegionDesc
                'REQ-1150
                State.DealerID = objParam.DealerID
                State.DealerDesc = objParam.DealerDesc
                State.Dealer = objParam.Dealer
                State.CompanyType = objParam.CompanyType

                ' getting Contry Type extnded code from  Country type GUID
                Dim listcontext As ListContext = New ListContext()
                Dim ExtendedcountryTypes As ListItem() = CommonConfigManager.Current.ListManager.GetList("COTYP", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)

                Dim filteredExtendedCountryType As ListItem() = (From x In ExtendedcountryTypes
                                                                 Where x.ListItemId = objParam.CompanyTypeId
                                                                 Select x).ToArray()
                If filteredExtendedCountryType.Any Then
                    State.CompanyTypeXCD = filteredExtendedCountryType(0).ExtendedCode
                End If

                If objParam.RegionTaxID = Nothing Or objParam.RegionTaxID = Guid.Empty Then
                    'new region tax, with regionid and taxtype id from countrytax form
                    State.MyBO = New RegionTax()
                    State.MyBO.RegionId = objParam.RegionID
                    State.MyBO.TaxTypeId = objParam.CountryTaxBO.TaxTypeId
                    State.MyBO.ProductTaxTypeId = objParam.CountryTaxBO.ProductTaxTypeId
                    Dim strTemp As String = objParam.RegionDesc & " " & objParam.TaxTypeDesc
                    State.MyBO.Description = strTemp.Substring(0, Math.Min(strTemp.Length, 30))
                    State.MyBO.DealerId = objParam.DealerID
                    State.MyBO.SetEffectiveExpirationDates()
                    State.MyBO.CompanyTypeXCD = State.CompanyTypeXCD
                Else
                    'existing region tax
                    State.MyBO = New RegionTax(objParam.RegionTaxID)
                End If



            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Control EVENTS"

    Private Sub btnDelete_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyCountryBO, State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            State.LastErrMsg = MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If Not State.MyBO.IsNew Then
                'Reload from the DB
                State.MyBO = New RegionTax(State.MyBO.Id)
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                State.MyBO = New RegionTax
            End If
            PopulateFormFromBOs()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnSave_WRITE.Click
        Try

            'ALR - DEF-560 - Commenting out as the clone is duplicating the child objects.  Not needed.
            'save a copy in case there is error on save
            'If Me.State.ScreenSnapShotBO Is Nothing Then Me.State.ScreenSnapShotBO = New RegionTax
            'Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)

            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                Try
                    State.MyBO.Save()
                Catch ex As Exception
                    'ALR - DEF-560 - Commenting out as the clone is duplicating the child objects.  Not needed.
                    ''restore the original BO
                    'Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                    'Me.State.ScreenSnapShotBO = Nothing
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

    Private Sub btnNew_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            PopulateBOsFromForm()
            If State.MyBO.IsDirty Then
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewWithCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"

    Private Sub CreateNew()
        State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        State.MyBO = New RegionTax
        State.MyBO.RegionId = State.RegionID
        State.MyBO.TaxTypeId = State.MyCountryBO.TaxTypeId
        State.MyBO.ProductTaxTypeId = State.MyCountryBO.ProductTaxTypeId
        State.MyBO.SetEffectiveExpirationDates()
        PopulateFormFromBOs()
    End Sub

    Private Sub CreateNewWithCopy()
        State.IsACopy = True
        PopulateBOsFromForm()

        Dim newObj As New RegionTax
        newObj.Copy(State.MyBO)

        State.MyBO = newObj
        State.MyBO.SetEffectiveExpirationDates()

        PopulateFormFromBOs()

        'create the backup copy
        State.ScreenSnapShotBO = New RegionTax
        State.ScreenSnapShotBO.Clone(State.MyBO)
        State.IsACopy = False
    End Sub

    Protected Sub PopulateFormFromBOs()

        Dim oRegionTax As RegionTax = State.MyBO

        With State.MyBO
            PopulateControlFromBOProperty(txtEffectiveDate, .EffectiveDate)
            PopulateControlFromBOProperty(txtExpirationDate, .ExpirationDate)
            'Me.txtEffectiveDate.Text = .EffectiveDate.Value.ToString(DATE_FORMAT, CultureInfo.CurrentCulture)
            'Me.txtExpirationDate.Text = .ExpirationDate.Value.ToString(DATE_FORMAT, CultureInfo.CurrentCulture)
        End With

        LoadTaxDetailUserControls(True)
    End Sub

    Protected Sub PopulateBOsFromForm()
        Dim objBO As RegionTax = State.MyBO
        PopulateBOProperty(objBO, "EffectiveDate", txtEffectiveDate)
        PopulateBOProperty(objBO, "ExpirationDate", txtExpirationDate)
        PopulateBOProperty(objBO, "DealerId", State.DealerID)
        PopulateBOProperty(objBO, COMPANY_TYPE_ID_PROPERTY, State.CompanyTypeXCD)

        LoadTaxDetailUserControls(False)

        Dim intTaxGroup As Integer
        Dim oUserControl As RegionTaxUserControl
        Dim errMsg() As String = New String() {}, hasErr As Boolean = False

        For intTaxGroup = 1 To CountryTax.NumberOfTaxGroups
            oUserControl = mUserControlArray(intTaxGroup - 1)
            If oUserControl.IsEnabledForEditing Then
                If oUserControl.VerifyInput(errMsg) Then
                    PopulateBOProperty(objBO.RegionTaxDetail(intTaxGroup), "Percent", oUserControl.Percent)
                    PopulateBOProperty(objBO.RegionTaxDetail(intTaxGroup), "NonTaxable", oUserControl.Nontaxable)
                    PopulateBOProperty(objBO.RegionTaxDetail(intTaxGroup), "MinimumTax", oUserControl.MinimumTax)
                    PopulateBOProperty(objBO.RegionTaxDetail(intTaxGroup), "GlAccountNumber", oUserControl.GLAccount)
                Else
                    hasErr = True
                End If
            End If
        Next

        If hasErr Then 'user control GUI error
            MasterPage.MessageController.AddErrorAndShow(errMsg)
            Throw New GUIException("", "")
        End If

        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Private Sub LoadTaxDetailUserControls(setCtlProperties As Boolean)

        mUserControlArray.Clear()
        mUserControlArray.Add(RegionTaxUserControl1)
        mUserControlArray.Add(RegionTaxUserControl2)
        mUserControlArray.Add(RegionTaxUserControl3)
        mUserControlArray.Add(RegionTaxUserControl4)
        mUserControlArray.Add(RegionTaxUserControl5)
        mUserControlArray.Add(RegionTaxUserControl6)

        If setCtlProperties Then SetTaxDetailCtlProperties()

    End Sub

    Private Sub SetTaxDetailCtlProperties()
        Dim i As Integer, objTG As CountryTax.TaxGroupData
        Dim PercentFlagID As Guid, objRTD As RegionTaxDetail

        Dim stateFlagID As Guid = LookupListNew.GetIdFromCode( _
                LookupListNew.DropdownLookupList(CountryTax.TAX_PERCENT_FLAG, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True) _
                , CountryTax.TAX_STATE_CODE)

        With State.MyCountryBO
            For i = 1 To CountryTax.NumberOfTaxGroups
                objTG = .TaxGroup(i)
                objRTD = State.MyBO.RegionTaxDetail(i)

                If objTG.PercentFlag.Equals(stateFlagID) Then 'use state/region tax
                    If objRTD Is Nothing Then
                        objRTD = New RegionTaxDetail()
                        State.MyBO.RegionTaxDetail(i) = objRTD
                    End If
                    'Get description from Country tax group
                    objRTD.Description = objTG.Description
                    mUserControlArray(i - 1).LoadText(objRTD, True)
                Else 'not region tax, display the existing entry but disable the editing
                    If objRTD IsNot Nothing Then mUserControlArray(i - 1).LoadText(objRTD, False)
                End If
            Next
        End With
    End Sub

    Private Sub ShowEditingIncompleteMessage()
        DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
        'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()

        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

        If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
            If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                State.MyBO.Save()
            End If
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyCountryBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyCountryBO, State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Delete
                    State.MyBO.DeleteAndSave()
                    State.HasDataChanged = True
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyCountryBO, State.HasDataChanged))
            End Select
        ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
            Select Case State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyCountryBO, State.HasDataChanged))
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
        HiddenSaveChangesPromptResponse.Value = ""
    End Sub

#End Region

#Region "BUTTONS PROPERTIES METHOD"

    Protected Sub EnableDisableFields()
        'Enabled by Default
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)

        'Disabled by default
        EnableDisableEffDate(False)
        EnableDisableExpDate(False)

        'Now we check which control should be enabled
        Dim theBO As RegionTax = State.MyBO
        Dim existingRecCnt As Integer, minEffDate As Date, maxExpDate As Date
        theBO.GetMinEffDateAndMaxExpDate(minEffDate, maxExpDate, existingRecCnt)

        'New
        If theBO.IsNew Then
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)

            If existingRecCnt = 0 Then 'the first record, allow modify the effective date
                EnableDisableEffDate(True)
            End If
            EnableDisableExpDate(True)
        Else 'Existing BO
            If theBO.ExpirationDate.Value <> maxExpDate Then
                'only allow delete the last record
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            Else
                'last record, allow modify expiration date
                EnableDisableExpDate(True)
                'allow modify effective date if the record is the only record with future effective date
                If theBO.EffectiveDate.Value = minEffDate AndAlso minEffDate > Date.Now Then
                    EnableDisableEffDate(True)
                End If
            End If
        End If

    End Sub

    Private Sub EnableDisableEffDate(enabled As Boolean)

        If enabled Then
            ControlMgr.SetVisibleControl(Me, btnEffectiveDate, True)
            txtEffectiveDate.Style.Add("background-color", "")
        Else
            ControlMgr.SetVisibleControl(Me, btnEffectiveDate, False)
            txtEffectiveDate.Style.Add("background-color", "Gray")
        End If

    End Sub

    Private Sub EnableDisableExpDate(enabled As Boolean)
        If enabled Then
            ControlMgr.SetVisibleControl(Me, btnExpirationDate, True)
            txtExpirationDate.Style.Add("background-color", "")
        Else
            ControlMgr.SetVisibleControl(Me, btnExpirationDate, False)
            txtExpirationDate.Style.Add("background-color", "Gray")
        End If
    End Sub

#End Region

End Class
