Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements

Public Class BenefitProductCodeForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "BenefitProductCodeForm.aspx"

    Private Const DEALER_TYPE_BENEFIT As String = "BENEFIT"
    Private Const LABEL_DEALER As String = "DEALER_NAME"
    Private Const ConfigurationSuperUserRole As String = "CONSU"
#End Region

#Region "MyState"
    Class MyState
        Public BenefitProductCodeId As Guid = Guid.Empty

        Public BenefitProductCode As BenefitProductCode

        Public DealerTypeID As Guid = Guid.Empty
        Public LastOperation As DetailPageCommand = DetailPageCommand.Nothing_
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public BusinessObjectChanged As Boolean

        Public Function Changed() As Boolean
            If BenefitProductCode IsNot Nothing Then
                Return BenefitProductCode.IsDirty
            Else
                Return False
            End If
        End Function
    End Class
#End Region

#Region "Page State"
    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property
    Private Sub SetStateProperties()

        State.DealerTypeID = GetDealerTypeID()
        'set the product code as new for the first time only
        If State.BenefitProductCode IsNot Nothing Then
            If Not State.BenefitProductCode.IsNew Then
                ClearAll()
            End If
            SetButtonsState(State.BenefitProductCode.IsNew)
            PopulateAll()
        Else
            SetButtonsState(True)
        End If
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
                        CreateNewCopy()
                    End If
                Case MSG_VALUE_NO
                    ' create a new BO
                    CreateNewCopy()
            End Select
        End If

    End Sub
    Protected Sub ComingFromNew()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

        If Not confResponse = String.Empty Then
            ' Return from the New Copy Button

            Select Case confResponse
                Case MSG_VALUE_YES
                    ' Save and create a new Copy BO
                    If ApplyChanges() = True Then
                        CreateNew()
                    End If
                Case MSG_VALUE_NO
                    ' create a new BO
                    CreateNew()
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
                    ComingFromNew()
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
#Region "Page Events"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            MasterPage.MessageController.Clear_Hide()
            ClearLabelsErrorSign()

            If Not Page.IsPostBack Then
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()
                SetStateProperties()
                AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
            End If

            BindBoPropertiesToLabels()
            CheckIfComingFromConfirm()
            IntializeCalendars()
            If Not IsPostBack Then
                AddLabelDecorations(State.BenefitProductCode)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
            MasterPage.MessageController.Clear_Hide()
            State.LastOperation = DetailPageCommand.Nothing_
        Else
            ShowMissingTranslations(MasterPage.MessageController)
        End If
    End Sub
    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles Me.PageReturn
        Dim retObj As ProductPriceRangeByRepairMethod.ReturnType = CType(ReturnPar, ProductPriceRangeByRepairMethod.ReturnType)

        State.BenefitProductCodeId = retObj.EditingId
        SetStateProperties()
        State.LastOperation = DetailPageCommand.Redirect_
        EnableDisableFields()
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                Dim guidId = CType(CallingParameters, Guid)
                If Not guidId.Equals(Guid.Empty) Then
                    LoadNewBO(guidId)
                Else
                    CreateNewBO()
                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
    Private Sub CreateNewBO()
        State.BenefitProductCode = New BenefitProductCode()
        State.BenefitProductCodeId = State.BenefitProductCode.Id
    End Sub
    Private Sub CreateNewBO(newObj As BenefitProductCode)
        State.BenefitProductCode = newObj
        State.BenefitProductCodeId = newObj.Id
    End Sub
    Private Sub LoadNewBO(guidId As Guid)
        State.BenefitProductCodeId = guidId
        State.BenefitProductCode = New BenefitProductCode(State.BenefitProductCodeId)
    End Sub
#End Region


#Region "Properties"
    Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl_New
        Get
            If DealerDropControl Is Nothing Then
                DealerDropControl = CType(FindControl("DealerDropControl"), MultipleColumnDDLabelControl_New)
            End If
            Return DealerDropControl
        End Get
    End Property
#End Region

#Region "Methods"
    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("Benefit_Product_Code")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Benefit_Product_Code")
            End If
        End If
    End Sub
    Public Function GetDealerTypeID() As Guid
        Const FIRST_POS As Integer = 0
        Const COL_NAME As String = "ID"
        Dim dealertypeid As Guid
        Dim oDealertypeView As DataView = LookupListNew.GetDealerTypeId(Authentication.CurrentUser.CompanyGroup.Id)
        If oDealertypeView.Count > 0 Then
            dealertypeid = GuidControl.ByteArrayToGuid(CType(oDealertypeView(FIRST_POS)(COL_NAME), Byte()))
        End If
        Return dealertypeid
    End Function
#End Region

#Region "Controlling Logic"
    Private Sub BindBoPropertiesToLabels()

        If State.BenefitProductCode IsNot Nothing Then

            BindBOPropertyToLabel(State.BenefitProductCode, BenefitProductCode.PropertiesName.DealerId, DealerDropControl.CaptionLabel)
            BindBOPropertyToLabel(State.BenefitProductCode, BenefitProductCode.PropertiesName.VendorId, moVendorLabel)
            BindBOPropertyToLabel(State.BenefitProductCode, BenefitProductCode.PropertiesName.BenefitProductCode, moBenefitProductCodeLabel)
            BindBOPropertyToLabel(State.BenefitProductCode, BenefitProductCode.PropertiesName.Description, moDescriptionLabel)
            BindBOPropertyToLabel(State.BenefitProductCode, BenefitProductCode.PropertiesName.CurrencyIsoCode, moCurrencyLabel)
            BindBOPropertyToLabel(State.BenefitProductCode, BenefitProductCode.PropertiesName.UnitOfMeasureXcd, moUnitOfMeasureLabel)
            BindBOPropertyToLabel(State.BenefitProductCode, BenefitProductCode.PropertiesName.NetPrice, moNetPriceLabel)
            BindBOPropertyToLabel(State.BenefitProductCode, BenefitProductCode.PropertiesName.TaxTypeXCD, moTaxTypeXcdLabel)
            BindBOPropertyToLabel(State.BenefitProductCode, BenefitProductCode.PropertiesName.DurationInMonth, moDurationInMonthLabel)
            BindBOPropertyToLabel(State.BenefitProductCode, BenefitProductCode.PropertiesName.EffectiveDate, moEffectiveDateLabel)
            BindBOPropertyToLabel(State.BenefitProductCode, BenefitProductCode.PropertiesName.ExpirationDate, moExpirationDateLabel)
            BindBOPropertyToLabel(State.BenefitProductCode, BenefitProductCode.PropertiesName.VendorBillablePartNum, moBillablePartNumberLabel)
            BindBOPropertyToLabel(State.BenefitProductCode, BenefitProductCode.PropertiesName.DaysToExpireAfterEndDay, moDaysToExpireAfterEndDateLabel)

        End If
    End Sub
    Protected Sub EnableDisableFields()

    End Sub
#End Region

#Region "Clear"
    Private Sub ClearAll()
        ClearList(moVendorDrop)
        ClearList(moCurrencyDrop)
        ClearList(moTaxTypeXcdDrop)
        ClearList(moUnitOfMeasureXcdDrop)
    End Sub
#End Region

#Region "Populate"
    Private Sub PopulateAll()
        Try
            If State.BenefitProductCode.IsNew = True Then

            Else

            End If

            PopulateDealer()
            PopulateDrops()
            PopulateFromBO()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateDealer()

        Try

            Dim dv As DataView = LookupListNew.GetDealerLookupListByDealerType(ElitaPlusIdentity.Current.ActiveUser.Companies, DEALER_TYPE_BENEFIT)
            TheDealerControl.SetControl(True, MultipleColumnDDLabelControl_New.MODES.NEW_MODE, True, dv, "*" + TranslationBase.TranslateLabelOrMessage(LABEL_DEALER), True, True)
            If State.BenefitProductCode.IsNew = True Then
                If State.BenefitProductCode.DealerId.Equals(Guid.Empty) Then
                    TheDealerControl.SelectedGuid = Guid.Empty
                Else
                    TheDealerControl.SelectedGuid = State.BenefitProductCode.DealerId
                End If
                TheDealerControl.ChangeEnabledControlProperty(True)
            Else
                TheDealerControl.ChangeEnabledControlProperty(False)
                TheDealerControl.SelectedGuid = State.BenefitProductCode.DealerId
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateDrops()
        'BindListControlToDataView(moVendorDrop, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries),,, False)
        'BindCodeToListControl(moCurrencyDrop, LookupListNew.GetCurrenciesForCompanyandDealersInCompanyLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId), , "iso_code", True)
        'Me.moCurrencyDrop.Populate("LK_UNIT_OF_MEASURE", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
        'Me.moTaxTypeXcdDrop.Populate("LK_BENEFIT_TAX_TYPES", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)
        'Me.moUnitOfMeasureXcdDrop.Populate("LK_UNIT_OF_MEASURE", ListValueType.Description, ListValueType.ExtendedCode, PopulateBehavior.AddBlankListItem, String.Empty, ListValueType.Description)

        'BindListControlToDataView(moVendorDrop, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries))
        Dim ServiceCenterList As New Collections.Generic.List(Of DataElements.ListItem)

        For Each Country_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Countries
            Dim ServiceCenters As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry",
                                                                    context:=New ListContext() With
                                                                    {
                                                                      .CountryId = Country_id
                                                                    })

            If ServiceCenters.Count > 0 Then
                If ServiceCenterList IsNot Nothing Then
                    ServiceCenterList.AddRange(ServiceCenters)
                Else
                    ServiceCenterList = ServiceCenters.Clone()
                End If
            End If
        Next

        moVendorDrop.Populate(ServiceCenterList.ToArray(), New PopulateOptions() With
            {
                .AddBlankItem = True
            })

        'BindCodeToListControl(moCurrencyDrop, LookupListNew.GetCurrenciesForCompanyandDealersInCompanyLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId), , "iso_code", True)
        Dim listcontext1 As ListContext = New ListContext()
        listcontext1.CompanyId = ElitaPlusIdentity.Current.ActiveUser.CompanyId
        Dim currLKl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.GetCurrencyByCompany, ElitaPlusIdentity.Current.ActiveUser.LanguageCode, listcontext1)
        moCurrencyDrop.Populate(currLKl, New PopulateOptions() With
             {
               .AddBlankItem = True,
               .TextFunc = AddressOf .GetDescription,
               .BlankItemValue = String.Empty,
               .ValueFunc = AddressOf .GetCode
             })

        ' BindCodeToListControl(moTaxTypeXcdDrop, LookupListNew.GetBenefitTaxTypes(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True) 'BENEFIT_TAX_TYPES
        Dim taxTypeXcdLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("BENEFIT_TAX_TYPES", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        moTaxTypeXcdDrop.Populate(taxTypeXcdLkl, New PopulateOptions() With
                 {
              .AddBlankItem = True,
               .ValueFunc = AddressOf .GetExtendedCode,
               .BlankItemValue = String.Empty,
               .SortFunc = AddressOf .GetDescription
                })
        'BindCodeToListControl(moUnitOfMeasureXcdDrop, LookupListNew.GetUnitOfMeasureList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , True) 'UNIT_OF_MEASURE
        Dim unitOfMeasreXcdLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("UNIT_OF_MEASURE", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
        moUnitOfMeasureXcdDrop.Populate(unitOfMeasreXcdLkl, New PopulateOptions() With
                 {
              .AddBlankItem = True,
               .ValueFunc = AddressOf .GetExtendedCode,
               .BlankItemValue = String.Empty,
               .SortFunc = AddressOf .GetDescription
                })
    End Sub

    Private Sub IntializeCalendars()
        AddCalendar(moEffectiveDateImageButton, moEffectiveDateText)
        AddCalendar(moExpirationDateImageButton, moExpirationDateText)
    End Sub
    Private Sub PopulateFromBO()

        If State.BenefitProductCode IsNot Nothing Then
            'If Not Me.State.BenefitProductCode.EffectiveDate Is Nothing Then
            '    Me.moEffectiveDateText.Text = Me.State.BenefitProductCode.EffectiveDate.Value.ToString(DATE_FORMAT, System.Threading.Thread.CurrentThread.CurrentCulture)
            'Else
            '    Me.moEffectiveDateText.Text = System.DateTime.Today.ToString(DATE_FORMAT, System.Threading.Thread.CurrentThread.CurrentCulture)
            'End If


            'If Not Me.State.BenefitProductCode.ExpirationDate Is Nothing Then
            '    Me.moExpirationDateText.Text = Me.State.BenefitProductCode.ExpirationDate.Value.ToString(DATE_FORMAT, System.Threading.Thread.CurrentThread.CurrentCulture)
            'Else
            '    Me.moExpirationDateText.Text = System.DateTime.Today.ToString(DATE_FORMAT, System.Threading.Thread.CurrentThread.CurrentCulture)
            'End If


            With State.BenefitProductCode
                PopulateControlFromBOProperty(moBenefitProductCodeText, .BenefitProductCode)
                PopulateControlFromBOProperty(moDescriptionText, .Description)
                PopulateControlFromBOProperty(moNetPriceText, .NetPrice)
                PopulateControlFromBOProperty(moDurationInMonthText, .DurationInMonth)
                PopulateControlFromBOProperty(moBillablePartNumberText, .VendorBillablePartNum)
                PopulateControlFromBOProperty(moDaysToExpireAfterEndDateText, .DaysToExpireAfterEndDay)

                PopulateControlFromBOProperty(moEffectiveDateText, .EffectiveDate)
                PopulateControlFromBOProperty(moExpirationDateText, .ExpirationDate)

                If Not .VendorId.Equals(Guid.Empty) Then
                    PopulateControlFromBOProperty(moVendorDrop, .VendorId)
                End If
                If Not String.IsNullOrEmpty(.CurrencyIsoCode) Then
                    SetSelectedItem(moCurrencyDrop, .CurrencyIsoCode)
                End If
                If Not String.IsNullOrEmpty(.TaxTypeXCD) Then
                    SetSelectedItem(moTaxTypeXcdDrop, .TaxTypeXCD)
                End If
                If Not String.IsNullOrEmpty(.UnitOfMeasureXcd) Then
                    SetSelectedItem(moUnitOfMeasureXcdDrop, .UnitOfMeasureXcd)
                End If
            End With

        End If

    End Sub

    Private Sub PopulateBenefitsBOFromForm()

        If State.BenefitProductCode IsNot Nothing Then
            If State.BenefitProductCode.IsNew Then
                State.BenefitProductCode.DealerId = TheDealerControl.SelectedGuid
                PopulateBOProperty(State.BenefitProductCode, "BenefitProductCode", moBenefitProductCodeText)
            End If

            PopulateBOProperty(State.BenefitProductCode, "VendorId", moVendorDrop)
            PopulateBOProperty(State.BenefitProductCode, "Description", moDescriptionText)
            PopulateBOProperty(State.BenefitProductCode, "CurrencyIsoCode", moCurrencyDrop, isGuidValue:=False, isStringValue:=True)
            PopulateBOProperty(State.BenefitProductCode, "TaxTypeXCD", moTaxTypeXcdDrop, isGuidValue:=False, isStringValue:=True)
            PopulateBOProperty(State.BenefitProductCode, "UnitOfMeasureXcd", moUnitOfMeasureXcdDrop, isGuidValue:=False, isStringValue:=True)

            PopulateBOProperty(State.BenefitProductCode, "NetPrice", moNetPriceText)
            PopulateBOProperty(State.BenefitProductCode, "DurationInMonth", moDurationInMonthText)
            PopulateBOProperty(State.BenefitProductCode, "VendorBillablePartNum", moBillablePartNumberText)
            PopulateBOProperty(State.BenefitProductCode, "DaysToExpireAfterEndDay", moDaysToExpireAfterEndDateText)
            PopulateBOProperty(State.BenefitProductCode, "EffectiveDate", moEffectiveDateText)
            PopulateBOProperty(State.BenefitProductCode, "ExpirationDate", moExpirationDateText)

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End If

    End Sub
#End Region
#Region "Gui-Validation"
    Private Sub SetButtonsState()
        If State.BenefitProductCode IsNot Nothing Then
            SetButtonsState(State.BenefitProductCode.IsNew)
        Else
            SetButtonsState(True)
        End If
    End Sub
    Private Sub SetButtonsState(bIsNew As Boolean)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
        TheDealerControl.ChangeEnabledControlProperty(bIsNew)
        ControlMgr.SetEnableControl(Me, moBenefitProductCodeText, bIsNew)
        If Not bIsNew Then
            If State.BenefitProductCode IsNot Nothing Then
                If State.BenefitProductCode.ExpirationDate.Value <= DateTime.Today Then
                    ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                    ControlMgr.SetEnableControl(Me, btnApply_WRITE, False)
                    ControlMgr.SetEnableControl(Me, btnUndo_WRITE, False)
                End If
            End If
        End If
    End Sub
    Private Sub ClearLabelsErrorSign()
        ClearLabelErrSign(moDescriptionLabel)
        ClearLabelErrSign(moVendorLabel)
        ClearLabelErrSign(moCurrencyLabel)
        ClearLabelErrSign(moTaxTypeXcdLabel)
        ClearLabelErrSign(moUnitOfMeasureLabel)
        ClearLabelErrSign(moNetPriceLabel)
        ClearLabelErrSign(moDurationInMonthLabel)
        ClearLabelErrSign(moBillablePartNumberLabel)
        ClearLabelErrSign(moDaysToExpireAfterEndDateLabel)
        ClearLabelErrSign(moEffectiveDateLabel)
        ClearLabelErrSign(moExpirationDateLabel)
    End Sub
#End Region

#Region "Business Part"
    Private Function IsDirtyBO() As Boolean
        Dim bIsDirty As Boolean = False

        Try

            If State.BenefitProductCode IsNot Nothing Then
                bIsDirty = State.BenefitProductCode.IsDirty
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        Return bIsDirty
    End Function

    Private Function IsEditAllowed() As Boolean

        If ElitaPlusPrincipal.Current.IsInRole(ConfigurationSuperUserRole) = False Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Handlers-Buttons"
    Private Sub btnApply_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click
        Try
            ApplyChanges()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If State.BenefitProductCode Is Nothing Then
                Return
            End If

            If Not State.BenefitProductCode.IsNew Then
                'Reload from the DB
                LoadNewBO(State.BenefitProductCodeId)
            ElseIf State.BenefitProductCode IsNot Nothing Then
                'It was a new with copy
                State.BenefitProductCode.Clone(State.BenefitProductCode)
            Else
                CreateNewBO()
            End If
            PopulateAll()
            SetButtonsState()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBenefitsBOFromForm()

            If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                GoBack()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else

                CreateNewBO()
                SetButtonsState()
                PopulateAll()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                If IsEditAllowed() = False Then 'enable the save and delete button disabled when open the page
                    btnApply_WRITE.Enabled = True
                    btnDelete_WRITE.Enabled = True
                End If
                CreateNewCopy()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            If DoDelete() = True Then
                Dim retType As New BenefitProductCodeSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.BenefitProductCodeId, True)
                retType.BoChanged = True
                ReturnToCallingPage(retType)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Handlers-Buttons-Methods"
    Private Sub GoBack()

        Dim retType As New BenefitProductCodeSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.BenefitProductCodeId, State.BusinessObjectChanged)
        ReturnToCallingPage(retType)

    End Sub

    Private Sub CreateNew()
        CreateNewBO()
        SetButtonsState()
        PopulateAll()
    End Sub
    Private Sub CreateNewCopy()
        PopulateBenefitsBOFromForm()

        Dim newObj As New BenefitProductCode
        newObj.CopyFrom(State.BenefitProductCode)

        CreateNewBO(newObj)
        SetButtonsState()
        PopulateAll()

    End Sub
    Private Function ApplyChanges() As Boolean

        Try
            PopulateBenefitsBOFromForm()

            If IsDirtyBO() Then
                State.BenefitProductCode.SaveWithLog()
                SetButtonsState(State.BenefitProductCode.IsNew)
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Return True
            Else
                MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

        Return False

    End Function
    Private Function DoDelete() As Boolean
        Dim bIsOk As Boolean = True

        Try
            If State.BenefitProductCode IsNot Nothing Then
                With State.BenefitProductCode
                    .BeginEdit()
                    .Delete()
                    .Save()
                End With
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            bIsOk = False
        End Try
        Return bIsOk
    End Function


#End Region
End Class