Imports System.Text
Imports System.Globalization

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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants "
    Public Const URL As String = "RegionTaxes.aspx"
    Public Const DEALER_ID_PROPERTY As String = "DealerId"
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

        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As CountryTax, Optional ByVal boChanged As Boolean = False)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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

        Public Sub New(ByVal countryTaxBO As CountryTax, ByVal regionTaxID As Guid, ByVal taxTypeDesc As String, _
                       ByVal sProductTaxTypeDesc As String, ByVal regionID As Guid, ByVal regionDesc As String, _
                       ByVal dealerID As Guid, ByVal dealerDesc As String, ByVal dealer As String)
            Me.CountryTaxBO = countryTaxBO
            Me.RegionTaxID = regionTaxID
            Me.TaxTypeDesc = taxTypeDesc
            Me.msProductTaxTypeDesc = sProductTaxTypeDesc
            Me.RegionID = regionID
            Me.RegionDesc = regionDesc
            Me.DealerID = dealerID
            Me.DealerDesc = dealerDesc
            Me.Dealer = dealer
        End Sub
    End Class
#End Region

#Region "PAGE EVENTS"

    Private Sub UpdateBreadCrum()
        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(Me.PAGETITLE)
        End If
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        txtEffectiveDate.Attributes.Add("readonly", "readonly")
        txtExpirationDate.Attributes.Add("readonly", "readonly")

        Try
            Me.MasterPage.MessageController.Clear()
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(Me.PAGETAB)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(Me.PAGETITLE)
            Me.UpdateBreadCrum()

            If Not IsPostBack Then
                'Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                Me.AddCalendar(Me.btnEffectiveDate, Me.txtEffectiveDate)
                Me.AddCalendar(Me.btnExpirationDate, Me.txtExpirationDate)

                If Me.State.MyBO Is Nothing Then
                    Me.State.MyBO = New RegionTax()
                End If

                Me.txtTaxType.Text = Me.State.TaxTypeDesc
                Me.txtProductTaxType.Text = Me.State.msProductTaxTypeDesc
                Me.lblRegion.Text = Me.State.RegionDesc

                Me.PopulateFormFromBOs()
            End If
            Me.BindBOPropertyToLabel(Me.State.MyBO, DEALER_ID_PROPERTY, Me.lblDealer)
            CheckIfComingFromSaveConfirm()

            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If

            If Not Me.State.DealerDesc.IsNullOrEmpty(Me.State.DealerDesc) And Not Me.State.Dealer.IsNullOrEmpty(Me.State.Dealer) Then
                Me.Label10.Visible = True
                Me.Label11.Visible = True
                Me.lblDealer.Visible = True
                Me.Label12.Visible = True
                Me.lblDealerDesc.Visible = True
                Me.lblDealer.Text = Me.State.Dealer
                Me.lblDealerDesc.Text = Me.State.DealerDesc
            Else
                Me.Label10.Visible = False
                Me.Label11.Visible = False
                Me.lblDealer.Visible = False
                Me.Label12.Visible = False
                Me.lblDealerDesc.Visible = False
            End If


        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub Page_LoadComplete(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.LoadComplete
        EnableDisableFields()
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Dim objParam As Parameters
        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                objParam = CType(Me.CallingParameters, Parameters)
                Me.State.MyCountryBO = objParam.CountryTaxBO
                Me.State.RegionID = objParam.RegionID
                Me.State.TaxTypeDesc = objParam.TaxTypeDesc
                Me.State.msProductTaxTypeDesc = objParam.msProductTaxTypeDesc
                Me.State.RegionDesc = objParam.RegionDesc
                'REQ-1150
                Me.State.DealerID = objParam.DealerID
                Me.State.DealerDesc = objParam.DealerDesc
                Me.State.Dealer = objParam.Dealer

                If objParam.RegionTaxID = Nothing Or objParam.RegionTaxID = Guid.Empty Then
                    'new region tax, with regionid and taxtype id from countrytax form
                    Me.State.MyBO = New RegionTax()
                    Me.State.MyBO.RegionId = objParam.RegionID
                    Me.State.MyBO.TaxTypeId = objParam.CountryTaxBO.TaxTypeId
                    Me.State.MyBO.ProductTaxTypeId = objParam.CountryTaxBO.ProductTaxTypeId
                    Dim strTemp As String = objParam.RegionDesc & " " & objParam.TaxTypeDesc
                    Me.State.MyBO.Description = strTemp.Substring(0, Math.Min(strTemp.Length, 30))
                    Me.State.MyBO.DealerId = objParam.DealerID
                    Me.State.MyBO.SetEffectiveExpirationDates()
                Else
                    'existing region tax
                    Me.State.MyBO = New RegionTax(objParam.RegionTaxID)
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Control EVENTS"

    Private Sub btnDelete_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyCountryBO, Me.State.HasDataChanged))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            'Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
            Me.DisplayMessage(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
            Me.State.LastErrMsg = Me.MasterPage.MessageController.Text
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If Not Me.State.MyBO.IsNew Then
                'Reload from the DB
                Me.State.MyBO = New RegionTax(Me.State.MyBO.Id)
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                Me.State.MyBO = New RegionTax
            End If
            Me.PopulateFormFromBOs()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
        Try

            'ALR - DEF-560 - Commenting out as the clone is duplicating the child objects.  Not needed.
            'save a copy in case there is error on save
            'If Me.State.ScreenSnapShotBO Is Nothing Then Me.State.ScreenSnapShotBO = New RegionTax
            'Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)

            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                Try
                    Me.State.MyBO.Save()
                Catch ex As Exception
                    'ALR - DEF-560 - Commenting out as the clone is duplicating the child objects.  Not needed.
                    ''restore the original BO
                    'Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                    'Me.State.ScreenSnapShotBO = Nothing
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

    Private Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                Me.CreateNew()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnCopy_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            Me.PopulateBOsFromForm()
            If Me.State.MyBO.IsDirty Then
                'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                Me.CreateNewWithCopy()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Controlling Logic"

    Private Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

        Me.State.MyBO = New RegionTax
        State.MyBO.RegionId = State.RegionID
        State.MyBO.TaxTypeId = State.MyCountryBO.TaxTypeId
        State.MyBO.ProductTaxTypeId = State.MyCountryBO.ProductTaxTypeId
        Me.State.MyBO.SetEffectiveExpirationDates()
        Me.PopulateFormFromBOs()
    End Sub

    Private Sub CreateNewWithCopy()
        Me.State.IsACopy = True
        Me.PopulateBOsFromForm()

        Dim newObj As New RegionTax
        newObj.Copy(Me.State.MyBO)

        Me.State.MyBO = newObj
        Me.State.MyBO.SetEffectiveExpirationDates()

        Me.PopulateFormFromBOs()

        'create the backup copy
        Me.State.ScreenSnapShotBO = New RegionTax
        Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
        Me.State.IsACopy = False
    End Sub

    Protected Sub PopulateFormFromBOs()

        Dim oRegionTax As RegionTax = Me.State.MyBO

        With Me.State.MyBO
            Me.PopulateControlFromBOProperty(Me.txtEffectiveDate, .EffectiveDate)
            Me.PopulateControlFromBOProperty(Me.txtExpirationDate, .ExpirationDate)
            'Me.txtEffectiveDate.Text = .EffectiveDate.Value.ToString(DATE_FORMAT, CultureInfo.CurrentCulture)
            'Me.txtExpirationDate.Text = .ExpirationDate.Value.ToString(DATE_FORMAT, CultureInfo.CurrentCulture)
        End With

        LoadTaxDetailUserControls(True)
    End Sub

    Protected Sub PopulateBOsFromForm()
        Dim objBO As RegionTax = Me.State.MyBO
        Me.PopulateBOProperty(objBO, "EffectiveDate", Me.txtEffectiveDate)
        Me.PopulateBOProperty(objBO, "ExpirationDate", Me.txtExpirationDate)
        Me.PopulateBOProperty(objBO, "DealerId", Me.State.DealerID)

        LoadTaxDetailUserControls(False)

        Dim intTaxGroup As Integer
        Dim oUserControl As RegionTaxUserControl
        Dim errMsg() As String = New String() {}, hasErr As Boolean = False

        For intTaxGroup = 1 To CountryTax.NumberOfTaxGroups
            oUserControl = mUserControlArray(intTaxGroup - 1)
            If oUserControl.IsEnabledForEditing Then
                If oUserControl.VerifyInput(errMsg) Then
                    Me.PopulateBOProperty(objBO.RegionTaxDetail(intTaxGroup), "Percent", oUserControl.Percent)
                    Me.PopulateBOProperty(objBO.RegionTaxDetail(intTaxGroup), "NonTaxable", oUserControl.Nontaxable)
                    Me.PopulateBOProperty(objBO.RegionTaxDetail(intTaxGroup), "MinimumTax", oUserControl.MinimumTax)
                    Me.PopulateBOProperty(objBO.RegionTaxDetail(intTaxGroup), "GlAccountNumber", oUserControl.GLAccount)
                Else
                    hasErr = True
                End If
            End If
        Next

        If hasErr Then 'user control GUI error
            Me.MasterPage.MessageController.AddErrorAndShow(errMsg)
            Throw New GUIException("", "")
        End If

        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Private Sub LoadTaxDetailUserControls(ByVal setCtlProperties As Boolean)

        mUserControlArray.Clear()
        mUserControlArray.Add(Me.RegionTaxUserControl1)
        mUserControlArray.Add(Me.RegionTaxUserControl2)
        mUserControlArray.Add(Me.RegionTaxUserControl3)
        mUserControlArray.Add(Me.RegionTaxUserControl4)
        mUserControlArray.Add(Me.RegionTaxUserControl5)
        mUserControlArray.Add(Me.RegionTaxUserControl6)

        If setCtlProperties Then SetTaxDetailCtlProperties()

    End Sub

    Private Sub SetTaxDetailCtlProperties()
        Dim i As Integer, objTG As CountryTax.TaxGroupData
        Dim PercentFlagID As Guid, objRTD As RegionTaxDetail

        Dim stateFlagID As Guid = LookupListNew.GetIdFromCode( _
                LookupListNew.DropdownLookupList(CountryTax.TAX_PERCENT_FLAG, ElitaPlusIdentity.Current.ActiveUser.LanguageId, True) _
                , CountryTax.TAX_STATE_CODE)

        With Me.State.MyCountryBO
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
                    If Not objRTD Is Nothing Then mUserControlArray(i - 1).LoadText(objRTD, False)
                End If
            Next
        End With
    End Sub

    Private Sub ShowEditingIncompleteMessage()
        Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
        'Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
    End Sub

    Protected Sub CheckIfComingFromSaveConfirm()

        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

        If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_YES Then
            If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                Me.State.MyBO.Save()
            End If
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyCountryBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.New_
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNew()
                Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                    'Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                    Me.CreateNewWithCopy()
                Case ElitaPlusPage.DetailPageCommand.BackOnErr
                    Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyCountryBO, Me.State.HasDataChanged))
                Case ElitaPlusPage.DetailPageCommand.Delete
                    Me.State.MyBO.DeleteAndSave()
                    Me.State.HasDataChanged = True
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyCountryBO, Me.State.HasDataChanged))
            End Select
        ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
            Select Case Me.State.ActionInProgress
                Case ElitaPlusPage.DetailPageCommand.Back
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyCountryBO, Me.State.HasDataChanged))
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
        Me.HiddenSaveChangesPromptResponse.Value = ""
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
        Dim theBO As RegionTax = Me.State.MyBO
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
                    Me.EnableDisableEffDate(True)
                End If
            End If
        End If

    End Sub

    Private Sub EnableDisableEffDate(ByVal enabled As Boolean)

        If enabled Then
            ControlMgr.SetVisibleControl(Me, btnEffectiveDate, True)
            Me.txtEffectiveDate.Style.Add("background-color", "")
        Else
            ControlMgr.SetVisibleControl(Me, btnEffectiveDate, False)
            Me.txtEffectiveDate.Style.Add("background-color", "Gray")
        End If

    End Sub

    Private Sub EnableDisableExpDate(ByVal enabled As Boolean)
        If enabled Then
            ControlMgr.SetVisibleControl(Me, btnExpirationDate, True)
            Me.txtExpirationDate.Style.Add("background-color", "")
        Else
            ControlMgr.SetVisibleControl(Me, btnExpirationDate, False)
            Me.txtExpirationDate.Style.Add("background-color", "Gray")
        End If
    End Sub

#End Region

End Class
