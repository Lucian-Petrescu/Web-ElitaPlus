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
        Me.State.moIsNewProducerLabel = CType(Me.CallingParameters, Guid)
        If Me.State.moIsNewProducerLabel.Equals(Guid.Empty) Then
            Me.State.IsNewProducerNew = True
            ClearAll()
        Else
            Me.State.IsNewProducerNew = False
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
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As Producer, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.EditingBo = curEditingBo
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
            If Me.State.MyBO Is Nothing Then
                If Me.State.IsNewProducerNew = True Then
                    ' For creating, inserting
                    Me.State.MyBO = New Producer
                    Me.State.moIsNewProducerLabel = Me.State.MyBO.Id
                Else
                    ' For updating, deleting
                    Me.State.MyBO = New Producer(Me.State.moIsNewProducerLabel)
                End If
            End If

            Return Me.State.MyBO
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

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        Try
            Me.MasterPage.MessageController.Clear_Hide()
            ClearLabelsErrSign()
            If Not Page.IsPostBack Then
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                UpdateBreadCrum()
                Me.SetStateProperties()
                Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, True)

                If Me.State.IsNewProducerNew = True Then
                    CreateNew()
                Else
                    If Me.State.MyBO.AddressId.Equals(Guid.Empty) Then
                        Me.AddressCtr.MyBO = New Address
                    End If
                End If
                PopulateDropDowns()
                PopulateAddressFields()
                PopulateFormFromBOs()
                Me.moAddressController.ReAssignTabIndex(AddressInfoStartIndex)
                AddressCtr.EnableControls(False, True)
            End If

            BindBoPropertiesToLabels()
            CheckIfComingFromConfirm()
            If Not Me.IsPostBack Then
                Me.AddLabelDecorations(Me.State.MyBO)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)

        End Try
        If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
            Me.MasterPage.MessageController.Clear_Hide()
            'ClearLabelsErrSign()
            Me.State.LastOperation = DetailPageCommand.Nothing_
        Else
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End If
    End Sub

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall

        Try
            If Not Me.CallingParameters Is Nothing Then
                'Get the id from the parent
                Me.State.MyBO = New Producer(CType(Me.CallingParameters, Guid))
            Else
                Me.State.IsNewProducerNew = True
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Handlers-Buttons"

    Private Sub btnApply_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
        ApplyChanges()
        AddressCtr.EnableControls(False, True)
    End Sub

    Private Sub GoBack()
        Dim retType As New ProducerForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                                Me.State.MyBO, Me.State.boChanged)
        Me.ReturnToCallingPage(retType)
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            If Me.State.MyBO.IsDirty() Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM,
                                                Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))

            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            If Not Me.State.IsNewProducerNew Then
                'Reload from the DB
                Me.State.MyBO = New Producer(Me.State.MyBO.Id)
                If Me.State.MyBO.AddressId.Equals(Guid.Empty) Then
                    AddressCtr.ClearAll()
                End If
            ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                'It was a new with copy
                Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
            Else
                CreateNew()
            End If
            PopulateDropDowns()
            PopulateFormFromBOs()
            Me.SetButtonsState(Me.State.IsNewProducerNew)
            AddressCtr.EnableControls(False, True)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub CreateNew()
        Me.State.ScreenSnapShotBO = Nothing
        Me.State.IsNewProducerNew = True
        Me.State.MyBO = New Producer
        Me.State.MyBO.CompanyId = moMultipleColumnDrop.SelectedGuid
        Me.AddressCtr.MyBO = New Address
        ClearAll()
        AddressCtr.ClearAll()
        Me.SetButtonsState(True)
        AddressCtr.EnableControls(False, True)
        Me.PopulateDropDowns()
        PopulateFormFromBOs()
        TheCompanyControl.ChangeEnabledControlProperty(True)
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            If IsDirtyBO() = True Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
            Else
                CreateNew()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub CreateNewCopy()

        Me.PopulateBOsFromForm()

        Dim newObj As New Producer
        Me.AddressCtr.MyBO = New Address
        newObj.Copy(Me.State.MyBO)
        newObj.Address.CopyFrom(Me.State.MyBO.Address)
        Me.State.MyBO = newObj
        Me.State.moIsNewProducerLabel = Guid.Empty
        Me.State.IsNewProducerNew = True
        AddressCtr.EnableControls(False, True)
        With Me.State.MyBO
            .Code = Nothing
            .Description = Nothing
        End With

        Me.SetButtonsState(True)
        PopulateFormFromBOs()
        TheCompanyControl.ChangeEnabledControlProperty(True)
        'create the backup copy
        Me.State.ScreenSnapShotBO = New Producer
        Me.State.ScreenSnapShotBO.Copy(Me.State.MyBO)

    End Sub

    Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
        Try
            If IsDirtyBO() = True Then
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            Else
                CreateNewCopy()
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
        End Try
    End Sub

    Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
        Try
            Me.State.MyBO.DeleteAndSave()
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete
            Me.State.HasDataChanged = True
            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
        If (Not Me.State Is Nothing) Then
            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("Producer")
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Producer")
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
        If Me.State.IsNewProducerNew = True Then
            TheCompanyControl.SelectedGuid = Guid.Empty
            TheCompanyControl.ChangeEnabledControlProperty(True)
        Else
            TheCompanyControl.ChangeEnabledControlProperty(False)
            TheCompanyControl.SelectedGuid = Me.State.MyBO.CompanyId
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
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateFormFromBOs()
        Try
            With Me.State.MyBO
                Me.PopulateControlFromBOProperty(Me.txtCode, .Code)
                Me.PopulateControlFromBOProperty(Me.txtDescription, .Description)
                BindSelectItem(.ProducerTypeXcd, ddlProducerType)
                Me.PopulateControlFromBOProperty(Me.txtRegulatorRegistrationId, .RegulatorRegistrationId)
                Me.PopulateControlFromBOProperty(Me.txtTaxIdNumber, .TaxIdNumber)
                If Not .AddressId.Equals(Guid.Empty) Then
                    AddressCtr.Bind(Me.State.MyBO.Address)
                End If
            End With
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub PopulateBOsFromForm()

        Me.State.MyBO.CompanyId = TheCompanyControl.SelectedGuid
        Me.PopulateBOProperty(Me.State.MyBO, "Code", Me.txtCode)
        Me.PopulateBOProperty(Me.State.MyBO, "Description", Me.txtDescription)
        Me.PopulateBOProperty(Me.State.MyBO, "RegulatorRegistrationId", Me.txtRegulatorRegistrationId)
        Me.PopulateBOProperty(Me.State.MyBO, "ProducerTypeXcd", Me.ddlProducerType, False, True)
        Me.PopulateBOProperty(Me.State.MyBO, "TaxIdNumber", Me.txtTaxIdNumber)

        Me.AddressCtr.PopulateBOFromControl(True)
        If Not AddressCtr.MyBO Is Nothing Then
            If ((AddressCtr.MyBO.IsDeleted = False) AndAlso
                (AddressCtr.MyBO.IsEmpty = False)) AndAlso Not Me.State.MyBO.AddressId = Me.AddressCtr.MyBO.Id Then
                Me.State.MyBO.AddressId = Me.AddressCtr.MyBO.Id
            End If
        End If


        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If

    End Sub
    Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As MultipleColumnDDLabelControl_New) _
            Handles moMultipleColumnDrop.SelectedDropChanged
        Try
            Me.State.MyBO.CompanyId = moMultipleColumnDrop.SelectedGuid
            'PopulateDropDowns()
            PopulateAddressFields()

        Catch ex As Exception
            HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub PopulateAddressFields()
        Dim populateOptions As PopulateOptions = New PopulateOptions() With
            {
                .AddBlankItem = True
            }

        ' If Me.State.IsNewProducerNew Then
        'Set country to the country of selected company
        If Not Me.State.MyBO.CompanyId.Equals(Guid.Empty) Then
            Dim oListContext As New ListContext
            oListContext.UserId = ElitaPlusIdentity.Current.ActiveUser.Id
            ' Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
            Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="UserCountries", context:=oListContext)
            CType(Me.AddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList).Populate(countryList, populateOptions)

            Dim ListContext1 As New ListContext
            ListContext1.CompanyId = Me.State.MyBO.CompanyId
            Dim countryListForCompany As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CountryByCompany", context:=ListContext1)

            If countryListForCompany.Count > 0 Then
                Me.State.MyBO.Address.CountryId = countryListForCompany.FirstOrDefault().ListItemId
                SetSelectedItem(CType(Me.AddressCtr.FindControl("moCountryDrop_WRITE"), DropDownList), Me.State.MyBO.Address.CountryId)
            End If

            If Not Me.State.MyBO.Address.CountryId.Equals(Guid.Empty) Then
                CType(Me.AddressCtr.FindControl("moCountryText"), TextBox).Text = (From lst In countryList
                                                                                   Where lst.ListItemId = Me.State.MyBO.Address.CountryId
                                                                                   Select lst.Translation).FirstOrDefault()
            End If

            Dim listcontext2 As ListContext = New ListContext()
            listcontext2.CountryId = Me.State.MyBO.Address.CountryId
            CType(Me.AddressCtr.FindControl("moRegionDrop_WRITE"), DropDownList).Populate(CommonConfigManager.Current.ListManager.GetList("RegionsByCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext2), populateOptions)
        End If

        'End If
    End Sub
#End Region

#Region "Gui-Validation"

    Private Sub SetButtonsState(ByVal bIsNew As Boolean)
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
            bIsDirty = Me.State.MyBO.IsDirty

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Return bIsDirty
    End Function

    Private Function ApplyChanges() As Boolean

        Try
            Me.PopulateBOsFromForm()

            If Me.State.MyBO.IsDirty() Then
                Me.State.MyBO.Save()
                Me.State.boChanged = True
                If Me.State.IsNewProducerNew = True Then
                    Me.State.IsNewProducerNew = False
                End If
                PopulateDropDowns()
                PopulateFormFromBOs()
                Me.SetButtonsState(Me.State.IsNewProducerNew)
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Function

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
                        Me.State.boChanged = True
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
                        Me.State.boChanged = True
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
                        Me.State.boChanged = True
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

#Region "Handlers-Labels"

    Private Sub BindBoPropertiesToLabels()
        Me.BindBOPropertyToLabel(Me.State.MyBO, COMPANYID_PROPERTY, TheCompanyControl.CaptionLabel)
        Me.BindBOPropertyToLabel(Me.State.MyBO, CODE_PROPERTY, Me.lblCode)
        Me.BindBOPropertyToLabel(Me.State.MyBO, DESCRIPTION_PROPERTY, Me.lblDescription)
        Me.BindBOPropertyToLabel(Me.State.MyBO, PRODUCER_TYPE_PROPERTY, Me.lblProducerType)
        Me.BindBOPropertyToLabel(Me.State.MyBO, REGULATOR_REGISTRATION_ID_PROPERTY, Me.lblRegulatorRegistrationId)
        Me.BindBOPropertyToLabel(Me.State.MyBO, TAXIDNUMBER_PROPERTY, Me.lblTaxIdNumber)
    End Sub

    Private Sub ClearLabelsErrSign()
        Me.ClearLabelErrSign(lblCode)
        Me.ClearLabelErrSign(lblDescription)
        Me.ClearLabelErrSign(lblProducerType)
        Me.ClearLabelErrSign(lblRegulatorRegistrationId)
        Me.ClearLabelErrSign(lblTaxIdNumber)
        Me.ClearLabelErrSign(TheCompanyControl.CaptionLabel)
    End Sub

    Public Shared Sub SetLabelColor(ByVal lbl As Label)
        lbl.ForeColor = Color.Black
    End Sub



#End Region

#End Region

End Class