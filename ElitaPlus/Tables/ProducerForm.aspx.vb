Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Partial Class ProducerForm
    Inherits ElitaPlusPage

#Region "Page State"

#Region "MyState"

    Class MyState

        Public moIsNewProducerLabel As Guid = Guid.Empty
        Public IsNewProducerNew As Boolean = False
        Public IsNewWithCopy As Boolean = False
        Public IsUndo As Boolean = False
        Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
        Public LastOperation As DetailPageCommand = DetailPageCommand.Nothing_
        Public boChanged As Boolean = False
        Public LastErrMsg As String
        Public HasDataChanged As Boolean
        Public MyBO As Producer
        Public ProducerId As Guid = Guid.Empty
        Public IsGridVisible As Boolean
        '  Public Ocompany As Company
        Public ScreenSnapShotBO As Producer
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
        State.moIsNewProducerLabel = CType(CallingParameters, Guid)
        If State.moIsNewProducerLabel.Equals(Guid.Empty) Then
            State.IsNewProducerNew = True
            ClearAll()
        Else
            State.IsNewProducerNew = False
        End If
    End Sub

#End Region

#Region "Constants"

    Public Shared URL As String = "ProducerForm.aspx"
    Public Const DEALERID_PROPERTY As String = "DealerId"
    Public Const COMPANYID_PROPERTY As String = "CompanyId"
    Public Const CODE_PROPERTY As String = "Code"
    Public Const DESCRIPTION_PROPERTY As String = "Description"
    Public Const PRODUCER_TYPE_PROPERTY As String = "ProducerTypeXcd"
    Public Const TAXIDNUMBER_PROPERTY As String = "TaxIdNumber"
    Public Const REGULATOR_REGISTRATION_ID_PROPERTY As String = "RegulatorRegistrationId"
    Public Const AddressInfoStartIndex As Int16 = 7
    Private Const LABEL_SELECT_COMPANY As String = "COMPANY"
#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As Producer
        Public HasDataChanged As Boolean
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As Producer, hasDataChanged As Boolean)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class

#End Region

#Region "Properties"
    Public ReadOnly Property TheCompanyControl() As MultipleColumnDDLabelControl_New
        Get
            If moMultipleColumnDrop Is Nothing Then
                moMultipleColumnDrop = CType(FindControl("moMultipleColumnDrop"), MultipleColumnDDLabelControl_New)
            End If
            Return moMultipleColumnDrop
        End Get
    End Property
    Private ReadOnly Property TheProducer As Producer

        Get
            If State.MyBO Is Nothing Then
                If State.IsNewProducerNew = True Then
                    ' For creating, inserting
                    State.MyBO = New Producer
                    State.moIsNewProducerLabel = State.MyBO.Id
                Else
                    ' For updating, deleting
                    State.MyBO = New Producer(State.moIsNewProducerLabel)
                End If
            End If

            Return State.MyBO
        End Get
    End Property

    Public ReadOnly Property AddressCtr() As UserControlAddress_New
        Get
            Return moAddressController
        End Get
    End Property

#End Region

#Region "Handlers"

#Region "Handlers-Init"
    Protected WithEvents moErrorController As ErrorController

    Protected WithEvents moPanel As System.Web.UI.WebControls.Panel
    Protected WithEvents EditPanel_WRITE As System.Web.UI.WebControls.Panel
    Protected WithEvents Button1 As System.Web.UI.WebControls.Button
    'Protected WithEvents moAddressController As UserControlAddress_New
    Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl

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

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Try
            MasterPage.MessageController.Clear_Hide()
            ClearLabelsErrSign()
            If Not Page.IsPostBack Then
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()
                SetStateProperties()
                AddControlMsg(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)

                If State.IsNewProducerNew = True Then
                    CreateNew()
                Else
                    If State.MyBO.AddressId.Equals(Guid.Empty) Then
                        AddressCtr.MyBO = New Address
                    End If
                End If
                PopulateDropDowns()
                PopulateAddressFields()
                PopulateFormFromBOs()
                moAddressController.ReAssignTabIndex(AddressInfoStartIndex)
                AddressCtr.EnableControls(False, True)
            End If

            BindBoPropertiesToLabels()
            CheckIfComingFromConfirm()
            If Not IsPostBack Then
                AddLabelDecorations(State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
        If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
            MasterPage.MessageController.Clear_Hide()
            'ClearLabelsErrSign()
            State.LastOperation = DetailPageCommand.Nothing_
        Else
            ShowMissingTranslations(MasterPage.MessageController)
        End If
    End Sub

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall

        Try
            If CallingParameters IsNot Nothing Then
                'Get the id from the parent
                State.MyBO = New Producer(CType(CallingParameters, Guid))
            Else
                State.IsNewProducerNew = True
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Handlers-Buttons"

    Private Sub btnApply_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click
        ApplyChanges()
        AddressCtr.EnableControls(False, True)
    End Sub

    Private Sub GoBack()
        Dim retType As New ProducerForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                                State.MyBO, State.boChanged)
        ReturnToCallingPage(retType)
    End Sub

    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        Try
            If State.MyBO.IsDirty() Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM,
                                                HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))

            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If Not State.IsNewProducerNew Then
                'Reload from the DB
                State.MyBO = New Producer(State.MyBO.Id)
                If State.MyBO.AddressId.Equals(Guid.Empty) Then
                    AddressCtr.ClearAll()
                End If
            ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                'It was a new with copy
                State.MyBO.Clone(State.ScreenSnapShotBO)
            Else
                CreateNew()
            End If
            PopulateDropDowns()
            PopulateFormFromBOs()
            SetButtonsState(State.IsNewProducerNew)
            AddressCtr.EnableControls(False, True)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CreateNew()
        State.ScreenSnapShotBO = Nothing
        State.IsNewProducerNew = True
        State.MyBO = New Producer
        State.MyBO.CompanyId = moMultipleColumnDrop.SelectedGuid
        AddressCtr.MyBO = New Address
        ClearAll()
        AddressCtr.ClearAll()
        SetButtonsState(True)
        AddressCtr.EnableControls(False, True)
        PopulateDropDowns()
        PopulateFormFromBOs()
        TheCompanyControl.ChangeEnabledControlProperty(True)
    End Sub

    Private Sub btnNew_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            If IsDirtyBO() = True Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub CreateNewCopy()

        PopulateBOsFromForm()

        Dim newObj As New Producer
        AddressCtr.MyBO = New Address
        newObj.Copy(State.MyBO)
        newObj.Address.CopyFrom(State.MyBO.Address)
        State.MyBO = newObj
        State.moIsNewProducerLabel = Guid.Empty
        State.IsNewProducerNew = True
        AddressCtr.EnableControls(False, True)
        With State.MyBO
            .Code = Nothing
            .Description = Nothing
        End With

        SetButtonsState(True)
        PopulateFormFromBOs()
        TheCompanyControl.ChangeEnabledControlProperty(True)
        'create the backup copy
        State.ScreenSnapShotBO = New Producer
        State.ScreenSnapShotBO.Copy(State.MyBO)

    End Sub

    Private Sub btnCopy_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            If IsDirtyBO() = True Then
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewCopy()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            State.MyBO.DeleteAndSave()
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
            State.HasDataChanged = True
            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Clear"

    Private Sub ClearTexts()
        txtCode.Text = Nothing
        txtDescription.Text = Nothing
        txtRegulatorRegistrationId.Text = Nothing
        txtTaxIdNumber.Text = Nothing
    End Sub

    Private Sub ClearAll()
        ClearTexts()
        ddlProducerType.ClearSelection()
        TheCompanyControl.ClearMultipleDrop()
    End Sub

#End Region

#Region "Populate"

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("Producer")
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Producer")
            End If
        End If
    End Sub
    Private Sub PopulateDropDowns()
        PopulateProducerTypes()
        PopulateCompanyDropDown()
    End Sub
    Private Sub PopulateCompanyDropDown()
        Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
        TheCompanyControl.SetControl(True, TheCompanyControl.MODES.NEW_MODE, True, dv, "*" + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True, True)
        If State.IsNewProducerNew = True Then
            TheCompanyControl.SelectedGuid = Guid.Empty
            TheCompanyControl.ChangeEnabledControlProperty(True)
        Else
            TheCompanyControl.ChangeEnabledControlProperty(False)
            TheCompanyControl.SelectedGuid = State.MyBO.CompanyId
        End If

    End Sub
    Private Sub PopulateProducerTypes()
        Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Try
            Dim producerType As ListItem() = CommonConfigManager.Current.ListManager.GetList("PRODUCER_TYP", Thread.CurrentPrincipal.GetLanguageCode())
            Dim populateOptions As PopulateOptions = New PopulateOptions() With
            {
                .AddBlankItem = False,
                .ValueFunc = AddressOf .GetExtendedCode
            }
            ddlProducerType.Populate(producerType, populateOptions)

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateFormFromBOs()
        Try
            With State.MyBO
                PopulateControlFromBOProperty(txtCode, .Code)
                PopulateControlFromBOProperty(txtDescription, .Description)
                BindSelectItem(.ProducerTypeXcd, ddlProducerType)
                PopulateControlFromBOProperty(txtRegulatorRegistrationId, .RegulatorRegistrationId)
                PopulateControlFromBOProperty(txtTaxIdNumber, .TaxIdNumber)
                If Not .AddressId.Equals(Guid.Empty) Then
                    AddressCtr.Bind(State.MyBO.Address)
                End If
            End With
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub PopulateBOsFromForm()

        State.MyBO.CompanyId = TheCompanyControl.SelectedGuid
        PopulateBOProperty(State.MyBO, "Code", txtCode)
        PopulateBOProperty(State.MyBO, "Description", txtDescription)
        PopulateBOProperty(State.MyBO, "RegulatorRegistrationId", txtRegulatorRegistrationId)
        PopulateBOProperty(State.MyBO, "ProducerTypeXcd", ddlProducerType, False, True)
        PopulateBOProperty(State.MyBO, "TaxIdNumber", txtTaxIdNumber)

        AddressCtr.PopulateBOFromControl(True)
        If AddressCtr.MyBO IsNot Nothing Then
            If ((AddressCtr.MyBO.IsDeleted = False) AndAlso
                (AddressCtr.MyBO.IsEmpty = False)) AndAlso Not State.MyBO.AddressId = AddressCtr.MyBO.Id Then
                State.MyBO.AddressId = AddressCtr.MyBO.Id
            End If
        End If


        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

    End Sub
    Private Sub OnFromDrop_Changed(fromMultipleDrop As MultipleColumnDDLabelControl_New) _
            Handles moMultipleColumnDrop.SelectedDropChanged
        Try
            State.MyBO.CompanyId = moMultipleColumnDrop.SelectedGuid
            'PopulateDropDowns()
            PopulateAddressFields()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub PopulateAddressFields()
        Dim populateOptions As PopulateOptions = New PopulateOptions() With
            {
                .AddBlankItem = True
            }

        ' If Me.State.IsNewProducerNew Then
        'Set country to the country of selected company
        If Not State.MyBO.CompanyId.Equals(Guid.Empty) Then
            Dim oListContext As New ListContext
            oListContext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
            ' Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="UserCountries", context:=oListContext)
            CType(AddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList).Populate(countryList, populateOptions)

            Dim ListContext1 As New ListContext
            ListContext1.CompanyId = State.MyBO.CompanyId
            Dim countryListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CountryByCompany", context:=ListContext1)

            If countryListForCompany.Count > 0 Then
                State.MyBO.Address.CountryId = countryListForCompany.FirstOrDefault().ListItemId
                SetSelectedItem(CType(AddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList), State.MyBO.Address.CountryId)
            End If

            If Not State.MyBO.Address.CountryId.Equals(Guid.Empty) Then
                CType(AddressCtr.FindControl("moCountryText"), TextBox).Text = (From lst In countryList
                                                                                   Where lst.ListItemId = State.MyBO.Address.CountryId
                                                                                   Select lst.Translation).FirstOrDefault()
            End If

            Dim listcontext2 As ListContext = New ListContext()
            listcontext2.CountryId = State.MyBO.Address.CountryId
            CType(AddressCtr.FindControl("moRegionDrop_WRITE"), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("RegionsByCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext2), populateOptions)
        End If

        'End If
    End Sub
#End Region

#Region "Gui-Validation"

    Private Sub SetButtonsState(bIsNew As Boolean)
        ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
        ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
        ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
        ControlMgr.SetEnableControl(Me, lblCode, bIsNew)
        TheCompanyControl.ChangeEnabledControlProperty(bIsNew)
    End Sub


#End Region

#Region "Business Part"

    Private Function IsDirtyBO() As Boolean
        Dim bIsDirty As Boolean = True

        Try

            PopulateBOsFromForm()
            bIsDirty = State.MyBO.IsDirty

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        Return bIsDirty
    End Function

    Private Function ApplyChanges() As Boolean

        Try
            PopulateBOsFromForm()

            If State.MyBO.IsDirty() Then
                State.MyBO.Save()
                State.boChanged = True
                If State.IsNewProducerNew = True Then
                    State.IsNewProducerNew = False
                End If
                PopulateDropDowns()
                PopulateFormFromBOs()
                SetButtonsState(State.IsNewProducerNew)
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Function

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
    Protected Sub ComingFromNew()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value

        If Not confResponse = String.Empty Then
            ' Return from the New Copy Button

            Select Case confResponse
                Case MSG_VALUE_YES
                    ' Save and create a new Copy BO
                    If ApplyChanges() = True Then
                        State.boChanged = True
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

#Region "Handlers-Labels"

    Private Sub BindBoPropertiesToLabels()
        BindBOPropertyToLabel(State.MyBO, COMPANYID_PROPERTY, TheCompanyControl.CaptionLabel)
        BindBOPropertyToLabel(State.MyBO, CODE_PROPERTY, lblCode)
        BindBOPropertyToLabel(State.MyBO, DESCRIPTION_PROPERTY, lblDescription)
        BindBOPropertyToLabel(State.MyBO, PRODUCER_TYPE_PROPERTY, lblProducerType)
        BindBOPropertyToLabel(State.MyBO, REGULATOR_REGISTRATION_ID_PROPERTY, lblRegulatorRegistrationId)
        BindBOPropertyToLabel(State.MyBO, TAXIDNUMBER_PROPERTY, lblTaxIdNumber)
    End Sub

    Private Sub ClearLabelsErrSign()
        ClearLabelErrSign(lblCode)
        ClearLabelErrSign(lblDescription)
        ClearLabelErrSign(lblProducerType)
        ClearLabelErrSign(lblRegulatorRegistrationId)
        ClearLabelErrSign(lblTaxIdNumber)
        ClearLabelErrSign(TheCompanyControl.CaptionLabel)
    End Sub

    Public Shared Sub SetLabelColor(lbl As Label)
        lbl.ForeColor = Color.Black
    End Sub



#End Region

#End Region

End Class