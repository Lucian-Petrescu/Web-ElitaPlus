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
            If Not Me.BenefitProductCode Is Nothing Then
                Return Me.BenefitProductCode.IsDirty
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

        Me.State.DealerTypeID = GetDealerTypeID()
        'set the product code as new for the first time only
        If Not Me.State.BenefitProductCode Is Nothing Then
            If Not Me.State.BenefitProductCode.IsNew Then
                ClearAll()
            End If
            SetButtonsState(Me.State.BenefitProductCode.IsNew)
            PopulateAll()
        Else
            SetButtonsState(True)
        End If
    End Sub
#End Region

#Region "State-Management"

    Protected Sub ComingFromBack()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

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
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

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
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value

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
            Select Case Me.State.ActionInProgress
                    ' Period
                Case ElitaPlusPage.DetailPageCommand.Back
                    ComingFromBack()
                Case ElitaPlusPage.DetailPageCommand.New_
                    ComingFromNew()
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
#Region "Page Events"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.MasterPage.MessageController.Clear_Hide()
            ClearLabelsErrorSign()

            If Not Page.IsPostBack Then
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()
                Me.SetStateProperties()
                Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)
            End If

            BindBoPropertiesToLabels()
            CheckIfComingFromConfirm()
            IntializeCalendars()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.BenefitProductCode)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
            Me.MasterPage.MessageController.Clear_Hide()
            Me.State.LastOperation = DetailPageCommand.Nothing_
        Else
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End If
    End Sub
    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles Me.PageReturn
        Dim retObj As ProductPriceRangeByRepairMethod.ReturnType = CType(ReturnPar, ProductPriceRangeByRepairMethod.ReturnType)

        Me.State.BenefitProductCodeId = retObj.EditingId
        Me.SetStateProperties()
        Me.State.LastOperation = DetailPageCommand.Redirect_
        EnableDisableFields()
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Dim guidId = CType(Me.CallingParameters, Guid)
                If Not guidId.Equals(Guid.Empty) Then
                    LoadNewBO(guidId)
                Else
                    CreateNewBO()
                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub
    Private Sub CreateNewBO()
        Me.State.BenefitProductCode = New BenefitProductCode()
        Me.State.BenefitProductCodeId = Me.State.BenefitProductCode.Id
    End Sub
    Private Sub CreateNewBO(newObj As BenefitProductCode)
        Me.State.BenefitProductCode = newObj
        Me.State.BenefitProductCodeId = newObj.Id
    End Sub
    Private Sub LoadNewBO(guidId As Guid)
        Me.State.BenefitProductCodeId = guidId
        Me.State.BenefitProductCode = New BenefitProductCode(Me.State.BenefitProductCodeId)
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
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("Benefit_Product_Code")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Benefit_Product_Code")
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

        If Not Me.State.BenefitProductCode Is Nothing Then

            Me.BindBOPropertyToLabel(Me.State.BenefitProductCode, BenefitProductCode.PropertiesName.DealerId, Me.DealerDropControl.CaptionLabel)
            Me.BindBOPropertyToLabel(Me.State.BenefitProductCode, BenefitProductCode.PropertiesName.VendorId, Me.moVendorLabel)
            Me.BindBOPropertyToLabel(Me.State.BenefitProductCode, BenefitProductCode.PropertiesName.BenefitProductCode, Me.moBenefitProductCodeLabel)
            Me.BindBOPropertyToLabel(Me.State.BenefitProductCode, BenefitProductCode.PropertiesName.Description, Me.moDescriptionLabel)
            Me.BindBOPropertyToLabel(Me.State.BenefitProductCode, BenefitProductCode.PropertiesName.CurrencyIsoCode, Me.moCurrencyLabel)
            Me.BindBOPropertyToLabel(Me.State.BenefitProductCode, BenefitProductCode.PropertiesName.UnitOfMeasureXcd, Me.moUnitOfMeasureLabel)
            Me.BindBOPropertyToLabel(Me.State.BenefitProductCode, BenefitProductCode.PropertiesName.NetPrice, Me.moNetPriceLabel)
            Me.BindBOPropertyToLabel(Me.State.BenefitProductCode, BenefitProductCode.PropertiesName.TaxTypeXCD, Me.moTaxTypeXcdLabel)
            Me.BindBOPropertyToLabel(Me.State.BenefitProductCode, BenefitProductCode.PropertiesName.DurationInMonth, Me.moDurationInMonthLabel)
            Me.BindBOPropertyToLabel(Me.State.BenefitProductCode, BenefitProductCode.PropertiesName.EffectiveDate, Me.moEffectiveDateLabel)
            Me.BindBOPropertyToLabel(Me.State.BenefitProductCode, BenefitProductCode.PropertiesName.ExpirationDate, Me.moExpirationDateLabel)
            Me.BindBOPropertyToLabel(Me.State.BenefitProductCode, BenefitProductCode.PropertiesName.VendorBillablePartNum, Me.moBillablePartNumberLabel)
            Me.BindBOPropertyToLabel(Me.State.BenefitProductCode, BenefitProductCode.PropertiesName.DaysToExpireAfterEndDay, Me.moDaysToExpireAfterEndDateLabel)

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
            If Me.State.BenefitProductCode.IsNew = True Then

            Else

            End If

            PopulateDealer()
            PopulateDrops()
            PopulateFromBO()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateDealer()

        Try

            Dim dv As DataView = LookupListNew.GetDealerLookupListByDealerType(ElitaPlusIdentity.Current.ActiveUser.Companies, DEALER_TYPE_BENEFIT)
            TheDealerControl.SetControl(True, MultipleColumnDDLabelControl_New.MODES.NEW_MODE, True, dv, "*" + TranslationBase.TranslateLabelOrMessage(LABEL_DEALER), True, True)
            If Me.State.BenefitProductCode.IsNew = True Then
                If Me.State.BenefitProductCode.DealerId.Equals(Guid.Empty) Then
                    TheDealerControl.SelectedGuid = Guid.Empty
                Else
                    TheDealerControl.SelectedGuid = Me.State.BenefitProductCode.DealerId
                End If
                TheDealerControl.ChangeEnabledControlProperty(True)
            Else
                TheDealerControl.ChangeEnabledControlProperty(False)
                TheDealerControl.SelectedGuid = Me.State.BenefitProductCode.DealerId
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                If Not ServiceCenterList Is Nothing Then
                    ServiceCenterList.AddRange(ServiceCenters)
                Else
                    ServiceCenterList = ServiceCenters.Clone()
                End If
            End If
        Next

        Me.moVendorDrop.Populate(ServiceCenterList.ToArray(), New PopulateOptions() With
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
        Me.AddCalendar(Me.moEffectiveDateImageButton, Me.moEffectiveDateText)
        Me.AddCalendar(Me.moExpirationDateImageButton, Me.moExpirationDateText)
    End Sub
    Private Sub PopulateFromBO()

        If Not Me.State.BenefitProductCode Is Nothing Then
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


            With Me.State.BenefitProductCode
                Me.PopulateControlFromBOProperty(moBenefitProductCodeText, .BenefitProductCode)
                Me.PopulateControlFromBOProperty(moDescriptionText, .Description)
                Me.PopulateControlFromBOProperty(moNetPriceText, .NetPrice)
                Me.PopulateControlFromBOProperty(moDurationInMonthText, .DurationInMonth)
                Me.PopulateControlFromBOProperty(moBillablePartNumberText, .VendorBillablePartNum)
                Me.PopulateControlFromBOProperty(moDaysToExpireAfterEndDateText, .DaysToExpireAfterEndDay)

                Me.PopulateControlFromBOProperty(moEffectiveDateText, .EffectiveDate)
                Me.PopulateControlFromBOProperty(moExpirationDateText, .ExpirationDate)

                If Not .VendorId.Equals(Guid.Empty) Then
                    Me.PopulateControlFromBOProperty(moVendorDrop, .VendorId)
                End If
                If Not String.IsNullOrEmpty(.CurrencyIsoCode) Then
                    Me.SetSelectedItem(moCurrencyDrop, .CurrencyIsoCode)
                End If
                If Not String.IsNullOrEmpty(.TaxTypeXCD) Then
                    Me.SetSelectedItem(moTaxTypeXcdDrop, .TaxTypeXCD)
                End If
                If Not String.IsNullOrEmpty(.UnitOfMeasureXcd) Then
                    Me.SetSelectedItem(moUnitOfMeasureXcdDrop, .UnitOfMeasureXcd)
                End If
            End With

        End If

    End Sub

    Private Sub PopulateBenefitsBOFromForm()

        If Not Me.State.BenefitProductCode Is Nothing Then
            If Me.State.BenefitProductCode.IsNew Then
                Me.State.BenefitProductCode.DealerId = TheDealerControl.SelectedGuid
                PopulateBOProperty(Me.State.BenefitProductCode, "BenefitProductCode", moBenefitProductCodeText)
            End If

            PopulateBOProperty(Me.State.BenefitProductCode, "VendorId", moVendorDrop)
            PopulateBOProperty(Me.State.BenefitProductCode, "Description", moDescriptionText)
            PopulateBOProperty(Me.State.BenefitProductCode, "CurrencyIsoCode", moCurrencyDrop, isGuidValue:=False, isStringValue:=True)
            PopulateBOProperty(Me.State.BenefitProductCode, "TaxTypeXCD", moTaxTypeXcdDrop, isGuidValue:=False, isStringValue:=True)
            PopulateBOProperty(Me.State.BenefitProductCode, "UnitOfMeasureXcd", moUnitOfMeasureXcdDrop, isGuidValue:=False, isStringValue:=True)

            PopulateBOProperty(Me.State.BenefitProductCode, "NetPrice", moNetPriceText)
            PopulateBOProperty(Me.State.BenefitProductCode, "DurationInMonth", moDurationInMonthText)
            PopulateBOProperty(Me.State.BenefitProductCode, "VendorBillablePartNum", moBillablePartNumberText)
            PopulateBOProperty(Me.State.BenefitProductCode, "DaysToExpireAfterEndDay", moDaysToExpireAfterEndDateText)
            PopulateBOProperty(Me.State.BenefitProductCode, "EffectiveDate", moEffectiveDateText)
            PopulateBOProperty(Me.State.BenefitProductCode, "ExpirationDate", moExpirationDateText)

            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End If

    End Sub
#End Region
#Region "Gui-Validation"
    Private Sub SetButtonsState()
        If Not Me.State.BenefitProductCode Is Nothing Then
            SetButtonsState(Me.State.BenefitProductCode.IsNew)
        Else
            SetButtonsState(True)
        End If
    End Sub
    Private Sub SetButtonsState(ByVal bIsNew As Boolean)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
        TheDealerControl.ChangeEnabledControlProperty(bIsNew)
        ControlMgr.SetEnableControl(Me, moBenefitProductCodeText, bIsNew)
        If Not bIsNew Then
            If Not Me.State.BenefitProductCode Is Nothing Then
                If Me.State.BenefitProductCode.ExpirationDate.Value <= DateTime.Today Then
                    ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                    ControlMgr.SetEnableControl(Me, btnApply_WRITE, False)
                    ControlMgr.SetEnableControl(Me, btnUndo_WRITE, False)
                End If
            End If
        End If
    End Sub
    Private Sub ClearLabelsErrorSign()
        Me.ClearLabelErrSign(moDescriptionLabel)
        Me.ClearLabelErrSign(moVendorLabel)
        Me.ClearLabelErrSign(moCurrencyLabel)
        Me.ClearLabelErrSign(moTaxTypeXcdLabel)
        Me.ClearLabelErrSign(moUnitOfMeasureLabel)
        Me.ClearLabelErrSign(moNetPriceLabel)
        Me.ClearLabelErrSign(moDurationInMonthLabel)
        Me.ClearLabelErrSign(moBillablePartNumberLabel)
        Me.ClearLabelErrSign(moDaysToExpireAfterEndDateLabel)
        Me.ClearLabelErrSign(moEffectiveDateLabel)
        Me.ClearLabelErrSign(moExpirationDateLabel)
    End Sub
#End Region

#Region "Business Part"
    Private Function IsDirtyBO() As Boolean
        Dim bIsDirty As Boolean = False

        Try

            If Not Me.State.BenefitProductCode Is Nothing Then
                bIsDirty = Me.State.BenefitProductCode.IsDirty
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
    Private Sub btnApply_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
        Try
            ApplyChanges()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If Me.State.BenefitProductCode Is Nothing Then
                Return
            End If

            If Not Me.State.BenefitProductCode.IsNew Then
                'Reload from the DB
                LoadNewBO(Me.State.BenefitProductCodeId)
            ElseIf Not Me.State.BenefitProductCode Is Nothing Then
                'It was a new with copy
                Me.State.BenefitProductCode.Clone(Me.State.BenefitProductCode)
            Else
                CreateNewBO()
            End If
            PopulateAll()
            SetButtonsState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            PopulateBenefitsBOFromForm()

            If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                GoBack()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else

                CreateNewBO()
                SetButtonsState()
                PopulateAll()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            If IsEditAllowed() AndAlso IsDirtyBO() = True Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                If IsEditAllowed() = False Then 'enable the save and delete button disabled when open the page
                    Me.btnApply_WRITE.Enabled = True
                    Me.btnDelete_WRITE.Enabled = True
                End If
                CreateNewCopy()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            If DoDelete() = True Then
                Dim retType As New BenefitProductCodeSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.BenefitProductCodeId, True)
                retType.BoChanged = True
                Me.ReturnToCallingPage(retType)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Handlers-Buttons-Methods"
    Private Sub GoBack()

        Dim retType As New BenefitProductCodeSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.BenefitProductCodeId, Me.State.BusinessObjectChanged)
        Me.ReturnToCallingPage(retType)

    End Sub

    Private Sub CreateNew()
        CreateNewBO()
        SetButtonsState()
        PopulateAll()
    End Sub
    Private Sub CreateNewCopy()
        PopulateBenefitsBOFromForm()

        Dim newObj As New BenefitProductCode
        newObj.CopyFrom(Me.State.BenefitProductCode)

        CreateNewBO(newObj)
        SetButtonsState()
        PopulateAll()

    End Sub
    Private Function ApplyChanges() As Boolean

        Try
            PopulateBenefitsBOFromForm()

            If IsDirtyBO() Then
                Me.State.BenefitProductCode.SaveWithLog()
                Me.SetButtonsState(Me.State.BenefitProductCode.IsNew)
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Return True
            Else
                Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

        Return False

    End Function
    Private Function DoDelete() As Boolean
        Dim bIsOk As Boolean = True

        Try
            If Not Me.State.BenefitProductCode Is Nothing Then
                With Me.State.BenefitProductCode
                    .BeginEdit()
                    .Delete()
                    .Save()
                End With
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            bIsOk = False
        End Try
        Return bIsOk
    End Function


#End Region
End Class