Imports System.Security.Cryptography.X509Certificates
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Tables

    Public Class BonusStructureDetailForm
        Inherits ElitaPlusSearchPage



#Region "Page State"


#Region "MyState"

        Class MyState

            Public moBonusStructureId As Guid = Guid.Empty
            Public IsBonusStructureNew As Boolean = False
            Public IsNewWithCopy As Boolean = False

            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastOperation As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public DealerId As Guid = Guid.Empty
            Public ProductCodeId As Guid = Guid.Empty
            Public ServiceCenterId As Guid = Guid.Empty
            Public ExpirationDate As Date
            Public moBonusStructure As ClaimBonusSettings
            Public ScreenSnapShotBO As ClaimBonusSettings
            Public Sysdate As Date
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
            State.moBonusStructureId = CType(CallingParameters, Guid)
            If State.moBonusStructureId.Equals(Guid.Empty) Then
                State.IsBonusStructureNew = True
                BindBoPropertiesToLabels()
                AddLabelDecorations(BonusStructure)
                ClearAll()
                PopulateAll()
            Else
                State.IsBonusStructureNew = False
                BindBoPropertiesToLabels()
                AddLabelDecorations(BonusStructure)
                PopulateAll()
            End If
        End Sub
#End Region


#Region "Constants"

        Private Const BONUS_STRUCTURE_FORM001 As String = "BONUS_STRUCTURE_FORM001" ' Bonus Structure List Exception
        Private Const BONUS_STRUCTURE_FORM002 As String = "BONUS_STRUCTURE_FORM002" ' Bonus Structure Code Field Exception
        Private Const BONUS_STRUCTURE_FORM003 As String = "BONUS_STRUCTURE_FORM003" ' Bonus Structure Update Exception
        Private Const CONFIRM_MSG As String = "MGS_CONFIRM_PROMPT" '"Are you sure you want to delete the selected records?"
        Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK" '"The record has been successfully saved"
        Private Const MSG_UNIQUE_VIOLATION As String = "MSG_DUPLICATE_KEY_CONSTRAINT_VIOLATED" '"Unique value is in use"
        Private Const UNIQUE_VIOLATION As String = "unique constraint"
        Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
        Private Const EDIT_COMMAND As String = "EditRecord"
        Private Const CANCEL_COMMAND As String = "CancelRecord"
        Private Const SAVE_COMMAND As String = "SaveRecord"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const SORT_COMMAND As String = "Sort"
        Public Const LABEL_SELECT_BONUS_STRUCTURE As String = "BONUS_STRUCTURE"
        Private Const FIRST_POS As Integer = 0
        Private Const COL_NAME As String = "ID"
        Private Const LABEL_DEALER As String = "OR_SELECT_DEALER"
        Private Const LABEL_SERVICE_CENTER As String = "SELECT SERVICE CENTER"
        Private Const LABEL_PRODUCT_CODE As String = "PRODUCT_CODE"
        Private Const LABEL_BONUS_COMPUTE_METHOD As String = "BONUS_COMPUTE_METHOD"

        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Public Const URL As String = "BonusStructureDetailForm.aspx"

        Public Const DEALER_ID_PROPERTY As String = "DealerId"
        Public Const SERVICE_CENTER_PROPERTY As String = "ServiceCenterId"
        Public Const PRODUCT_CODE_PROPERTY As String = "ProductCodeId"
        Public Const BONUS_METHOD_COMPUTATION_ID As String = "BonusComputeMethodId"
        Public Const AVG_TAT As String = "ScAvgTat"

        Public Const PERCENTAGE_OR_AMOUNT As String = "Pecoramount"
        Public Const PRIORITY As String = "Priority"


        Public Const SC_REPLACEMENT_PERCENTAGE As String = "ScReplacementPct"
        Public Const BONUS_AMOUNT_PERIOD_MONTH As String = "BonusAmountPeriodMonth"
        Public Const EFFECTIVE_DATE As String = "Effective"
        Public Const EXPIRATION_DATE As String = "Expiration"

#End Region


#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public moBonusStructureId As Guid
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, oBonusStructureId As Guid, hasDataChanged As Boolean)
                LastOperation = LastOp
                moBonusStructureId = oBonusStructureId
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class

#End Region

#Region "Properties"
        Private ReadOnly Property BonusStructure As ClaimBonusSettings
            Get
                If State.moBonusStructure Is Nothing Then
                    If State.IsBonusStructureNew = True Then
                        ' For creating, inserting
                        State.moBonusStructure = New ClaimBonusSettings
                        State.moBonusStructureId = State.moBonusStructure.Id
                    Else
                        ' For updating, deleting
                        State.moBonusStructure = New ClaimBonusSettings(State.moBonusStructureId)
                    End If
                End If

                Return State.moBonusStructure
            End Get
        End Property



#End Region

#Region "Handlers"
#Region "Handlers-Init"

        Protected WithEvents moErrorController As ErrorController

        Protected WithEvents moPanel As System.Web.UI.WebControls.Panel
        Protected WithEvents EditPanel_WRITE As System.Web.UI.WebControls.Panel
        Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        Protected WithEvents moCoverageEditPanel As System.Web.UI.WebControls.Panel



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

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                MasterPage.MessageController.Clear_Hide()

                If Not Page.IsPostBack Then
                    MasterPage.MessageController.Clear()
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()
                    SetStateProperties()
                End If

                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()
                If Not IsPostBack Then
                    AddLabelDecorations(BonusStructure)
                End If

                ClientScript.RegisterStartupScript(Page.GetType, "startup", "bonusMethodChanged();", True)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
                MasterPage.MessageController.Clear_Hide()
                ClearLabelsErrSign()
                State.LastOperation = DetailPageCommand.Nothing_
            Else
                ShowMissingTranslations(MasterPage.MessageController)
            End If
        End Sub

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall

            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    State.moBonusStructure = New ClaimBonusSettings(CType(CallingParameters, Guid))
                Else
                    State.IsBonusStructureNew = True
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnApply_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            If State.IsBonusStructureNew = True Then
                ApplyChanges()

            ElseIf State.IsBonusStructureNew = False Then
                If IsDirtyBO() = True Then
                    ApplyChanges()
                Else
                    DisplayMessage(Message.NO_CHANGES_TO_RECORD, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End If
            End If
        End Sub

        Private Sub GoBack()
            Dim retType As New BonusStructureDetailForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.moBonusStructureId, State.boChanged)
            ReturnToCallingPage(retType)
        End Sub

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDirtyBO() = True Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


        Private Sub CreateNew()
            State.ScreenSnapShotBO = Nothing
            State.moBonusStructureId = Guid.Empty
            State.IsBonusStructureNew = True
            State.moBonusStructure = New ClaimBonusSettings
            ClearAll()
            SetButtonsState(True)
            PopulateAll()
            DealerDropControl.ChangeEnabledControlProperty(True)
            ServiceCenterDropControl.ChangeEnabledControlProperty(True)

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

            PopulateBOsFromForm()

            Dim newObj As New ClaimBonusSettings
            newObj.Copy(BonusStructure)

            State.moBonusStructure = newObj
            State.moBonusStructureId = Guid.Empty
            State.IsBonusStructureNew = True

            With BonusStructure
                .BonusComputeMethodId = Guid.Empty
                .ScAvgTat = Nothing
                .ScReplacementPct = Nothing
                .BonusAmountPeriodMonth = Nothing
                .Effective = Nothing
                .Expiration = Nothing
            End With

            SetButtonsState(True)
            ' DealerDropControl.ChangeEnabledControlProperty(True)

            'create the backup copy
            State.ScreenSnapShotBO = New ClaimBonusSettings
            State.ScreenSnapShotBO.Copy(BonusStructure)



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
                DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try

                DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenDelDeletePromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete



            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearTexts()

            moAVGTATText.Text = Nothing
            moPercentageOrAmountText.Text = Nothing
            moPriorityText.Text = Nothing
            moreplacementpercentageText.Text = Nothing
            mobonusamountperiodText.Text = Nothing
            moeffectiveText.Text = Nothing
            moexpirationText.Text = Nothing

        End Sub

        Private Sub ClearAll()
            ClearTexts()
            DealerDropControl.ClearMultipleDrop()
            ServiceCenterDropControl.ClearMultipleDrop()
            ClearList(moProductCode)
            ClearList(moBonusMethodComputationDD)
        End Sub

#End Region
#End Region

#Region "Populate"

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("BONUS_STRUCTURE_DETAIL")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("BONUS_STRUCTURE_DETAIL")
                End If
            End If
        End Sub
        Private Sub PopulateDealer()

            Try
                DealerDropControl.AutoPostBackDD = True
                DealerDropControl.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER) & "&nbsp;&nbsp;&nbsp;"
                DealerDropControl.NothingSelected = True
                DealerDropControl.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))

                If State.IsBonusStructureNew = True Then

                    DealerDropControl.ChangeEnabledControlProperty(True)
                    DealerDropControl.SelectedGuid = Guid.Empty

                Else

                    DealerDropControl.SelectedGuid = BonusStructure.DealerId
                End If
            Catch ex As Exception
                MasterPage.MessageController.AddError(BONUS_STRUCTURE_FORM001)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub
        Private Sub PopulateServiceCenter()
            Dim oCountryList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries
            Try
                Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(oCountryList)
                ServiceCenterDropControl.SetControl(True, ServiceCenterDropControl.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SERVICE_CENTER), True, True)
                If State.IsBonusStructureNew = True Then
                    ServiceCenterDropControl.SelectedGuid = Guid.Empty
                    ServiceCenterDropControl.ChangeEnabledControlProperty(True)

                Else

                    ServiceCenterDropControl.ChangeEnabledControlProperty(True)
                    ServiceCenterDropControl.SelectedGuid = BonusStructure.ServiceCenterId
                End If
            Catch ex As Exception
                MasterPage.MessageController.AddError(BONUS_STRUCTURE_FORM001)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateProductCode()
            Dim oDealerId As Guid = DealerDropControl.SelectedGuid
            Try

                'Me.BindListControlToDataView(moProductCode, LookupListNew.GetProductCodeLookupList(oDealerId), "CODE", , True) 'only product code table
                Dim listcontext As ListContext = New ListContext()
                listcontext.DealerId = oDealerId
                Dim prodLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList(ListCodes.ProductCodeByDealer, Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                moProductCode.Populate(prodLkl, New PopulateOptions() With
                 {
                .AddBlankItem = True,
                .TextFunc = AddressOf .GetCode,
                .SortFunc = AddressOf .GetCode
                 })
                If State.IsBonusStructureNew = True Then
                    moProductCode.SelectedValue = Nothing
                    moProductCode.Enabled = True

                Else
                    BindSelectItem(BonusStructure.ProductCodeId.ToString, moProductCode)

                End If


            Catch ex As Exception
                moErrorController.AddError(TranslationBase.TranslateLabelOrMessage(BONUS_STRUCTURE_FORM002, ElitaPlusIdentity.Current.ActiveUser.LanguageId))
                moErrorController.AddError(ex.Message)
                moErrorController.Show()
            End Try
        End Sub
        Private Sub PopulateDropDowns()
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            ' BindListControlToDataView(moBonusMethodComputationDD, LookupListNew.GetBonusComputationMethodList(oLanguageId), , , True) 'CBCM
            Dim bonusMethodLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CBCM", Thread.CurrentPrincipal.GetLanguageCode())
            moBonusMethodComputationDD.Populate(bonusMethodLkl, New PopulateOptions() With
             {
             .AddBlankItem = True
             })
            Try
                If State.IsBonusStructureNew = True Then
                    BindSelectItem(Nothing, moBonusMethodComputationDD)
                Else
                    BindSelectItem(BonusStructure.BonusComputeMethodId.ToString, moBonusMethodComputationDD)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateTexts()
            Try
                Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

                With BonusStructure


                    PopulateControlFromBOProperty(moAVGTATText, .ScAvgTat)
                    PopulateControlFromBOProperty(moPercentageOrAmountText, .Pecoramount)
                    PopulateControlFromBOProperty(moPriorityText, .Priority)
                    PopulateControlFromBOProperty(moreplacementpercentageText, .ScReplacementPct)
                    PopulateControlFromBOProperty(mobonusamountperiodText, .BonusAmountPeriodMonth)


                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateAll()
            If State.IsBonusStructureNew = True Then
                PopulateDropDowns()
                PopulateDealer()
                PopulateServiceCenter()
                PopulateDateFields()
            Else
                ClearAll()
                PopulateDealer()
                PopulateServiceCenter()
                PopulateProductCode()
                PopulateDropDowns()
                PopulateTexts()
                PopulateDateFields()
            End If

        End Sub


        Protected Sub PopulateBOsFromForm()

            With BonusStructure
                .DealerId = DealerDropControl.SelectedGuid
                .ServiceCenterId = ServiceCenterDropControl.SelectedGuid
                .ProductCodeId = If(String.IsNullOrEmpty(moProductCode.SelectedValue), Guid.Empty, New Guid(moProductCode.SelectedValue))
                PopulateBOProperty(BonusStructure, BONUS_METHOD_COMPUTATION_ID, moBonusMethodComputationDD)
                PopulateBOProperty(BonusStructure, AVG_TAT, moAVGTATText)
                PopulateBOProperty(BonusStructure, PERCENTAGE_OR_AMOUNT, moPercentageOrAmountText)
                PopulateBOProperty(BonusStructure, PRIORITY, moPriorityText)
                PopulateBOProperty(BonusStructure, SC_REPLACEMENT_PERCENTAGE, moreplacementpercentageText)
                PopulateBOProperty(BonusStructure, BONUS_AMOUNT_PERIOD_MONTH, mobonusamountperiodText)
                PopulateBOProperty(BonusStructure, EFFECTIVE_DATE, moeffectiveText)
                PopulateBOProperty(BonusStructure, EXPIRATION_DATE, moexpirationText)

            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub

        Public Sub PopulateDateFields()
            Dim today As System.DateTime
            today = DateTime.Now


            PopulateControlFromBOProperty(moeffectiveText, BonusStructure.Effective)
            PopulateControlFromBOProperty(moexpirationText, BonusStructure.Expiration)
            AddCalendarwithTime_New(btneffective, moeffectiveText, , moeffectiveText.Text)
            AddCalendarwithTime_New(btnExpiration, moexpirationText, , moexpirationText.Text)


        End Sub

#End Region

#Region "Button State"

        Private Sub SetButtonsState(bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            DealerDropControl.ChangeEnabledControlProperty(bIsNew)
            ServiceCenterDropControl.ChangeEnabledControlProperty(bIsNew)
            moProductCode.Enabled = bIsNew

        End Sub

#End Region

#Region "Business Part"

        Private Function IsDirtyBO() As Boolean
            Dim bIsDirty As Boolean = True

            Try
                With BonusStructure
                    PopulateBOsFromForm()
                    bIsDirty = .IsDirty
                End With

            Catch ex As Exception
                'Me.MasterPage.MessageController.AddError(ex.Message, True)
                MasterPage.MessageController.Show()
            End Try

            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean
            Dim eff As Date = Date.Now
            Try
                customvalidate()

                If State.IsBonusStructureNew = False Then

                    If State.moBonusStructure.Effective.Value.Ticks <> CType(moeffectiveText.Text, Date).Ticks Then
                        eff = CType(moeffectiveText.Text, Date).AddSeconds(1)
                    End If

                    State.moBonusStructure.DeleteAndSave()

                    State.moBonusStructureId = Guid.Empty
                    State.IsBonusStructureNew = True
                    State.moBonusStructure = New ClaimBonusSettings

                    moeffectiveText.Text = eff.AddSeconds(1).ToString()
                End If

                PopulateBOsFromForm()

                If BonusStructure.IsDirty() Then
                    BonusStructure.Save()
                    State.boChanged = True
                    If State.IsBonusStructureNew = True Then
                        State.IsBonusStructureNew = False
                    End If
                    PopulateAll()
                    SetButtonsState(State.IsBonusStructureNew)
                    MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Function


        Private Function customvalidate() As Boolean

            If DealerDropControl.SelectedGuid.Equals(Guid.Empty) And ServiceCenterDropControl.SelectedGuid.Equals(Guid.Empty) Then
                Throw New GUIException(Message.MSG_ENTER_A_SERVICE_CENTER_OR_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_SERVICE_CENTER_OR_DEALER_MUST_BE_SELECTED_ERR)
            End If

            Dim bonusmethodId As Guid = GetSelectedItem(moBonusMethodComputationDD)
            Dim BonusMethodCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_BONUS_COMPUTATION_METHOD, bonusmethodId)
            If BonusMethodCode = Codes.BONUS_METHOD_FIXED_AMOUNT OrElse BonusMethodCode = Codes.BONUS_METHOD_LABOUR_AMOUNT OrElse BonusMethodCode = Codes.BONUS_METHOD_AUTHORIZED_AMOUNT Then

                If String.IsNullOrEmpty(moPercentageOrAmountText.Text) Then
                    Throw New GUIException(Message.MSG_ENTER_A_VALUE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ENTER_A_VALUE_SELECTED_BONUS_COMPUTATION_METHOD)
                End If
                If BonusMethodCode = Codes.BONUS_METHOD_LABOUR_AMOUNT OrElse BonusMethodCode = Codes.BONUS_METHOD_AUTHORIZED_AMOUNT Then
                    If CType(moPercentageOrAmountText.Text, Int32) > 100 Then
                        Throw New GUIException(Message.MSG_ENTER_A_VALID_VALUE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_ENTER_A_VALID_VALUE_FOR_BONUS_COMPUTATION_METHOD)
                    End If
                End If
            End If

            Dim ProductCodeId As Guid = If(String.IsNullOrEmpty(moProductCode.SelectedValue), Guid.Empty, New Guid(moProductCode.SelectedValue))

            If State.moBonusStructure.GetClaimBonusSettingCount(DealerDropControl.SelectedGuid, ServiceCenterDropControl.SelectedGuid, ProductCodeId, State.moBonusStructureId) > 0 Then
                Throw New GUIException(Message.MSG_VALUE_ALREADY_IN_USE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_DUPLICATE_SERVICE_CENTER_DEALER_AND_PRODUCT_CODE)
            End If

        End Function

        Private Function DeleteBonusstructure() As Boolean
            Dim bIsOk As Boolean = True

            Try
                With BonusStructure
                    .BeginEdit()
                    PopulateBOsFromForm()
                    .Delete()
                    .Save()
                End With
            Catch ex As Exception
                MasterPage.MessageController.AddError(BONUS_STRUCTURE_FORM002)
                MasterPage.MessageController.AddError(ex.Message, False)
                MasterPage.MessageController.Show()
                bIsOk = False
            End Try
            Return bIsOk
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
        Protected Sub ComingFromDelete()

            Dim confResponseDel As String = HiddenDelDeletePromptResponse.Value

            If confResponseDel IsNot Nothing AndAlso confResponseDel = MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    Try
                        State.moBonusStructure.DeleteAndSave()
                        State.HasDataChanged = True
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.moBonusStructureId, State.HasDataChanged))
                    Catch ex As Exception
                        HandleErrors(ex, MasterPage.MessageController)
                    End Try
                    'Clean after consuming the action
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    HiddenDelDeletePromptResponse.Value = ""
                End If
            ElseIf confResponseDel IsNot Nothing AndAlso confResponseDel = MSG_VALUE_NO Then
                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenDelDeletePromptResponse.Value = ""
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
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        ComingFromDelete()

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
            BindBOPropertyToLabel(BonusStructure, DEALER_ID_PROPERTY, DealerDropControl.CaptionLabel)
            BindBOPropertyToLabel(BonusStructure, SERVICE_CENTER_PROPERTY, ServiceCenterDropControl.CaptionLabel)
            BindBOPropertyToLabel(BonusStructure, PRODUCT_CODE_PROPERTY, moProductCodelabel)
            BindBOPropertyToLabel(BonusStructure, BONUS_METHOD_COMPUTATION_ID, moBonusMethodComputationLabel)
            BindBOPropertyToLabel(BonusStructure, AVG_TAT, moAVGTAT)
            BindBOPropertyToLabel(BonusStructure, PERCENTAGE_OR_AMOUNT, moPercentageOrAmountLabel)
            BindBOPropertyToLabel(BonusStructure, PRIORITY, moPriorityLabel)
            BindBOPropertyToLabel(BonusStructure, SC_REPLACEMENT_PERCENTAGE, moreplacementpercentage)
            BindBOPropertyToLabel(BonusStructure, BONUS_AMOUNT_PERIOD_MONTH, mobonusamountperiod)
            BindBOPropertyToLabel(BonusStructure, EFFECTIVE_DATE, moeffective)
            BindBOPropertyToLabel(BonusStructure, EXPIRATION_DATE, moexpiration)
            ClearLabelsErrSign()
        End Sub


        Private Sub ClearLabelsErrSign()

            ClearLabelErrSign(DealerDropControl.CaptionLabel)
            ClearLabelErrSign(ServiceCenterDropControl.CaptionLabel)
            ClearLabelErrSign(moProductCodelabel)
            ClearLabelErrSign(moBonusMethodComputationLabel)
            ClearLabelErrSign(moAVGTAT)
            ClearLabelErrSign(moPercentageOrAmountLabel)
            ClearLabelErrSign(moPriorityLabel)
            ClearLabelErrSign(moreplacementpercentage)
            ClearLabelErrSign(mobonusamountperiod)
            ClearLabelErrSign(moeffective)
            ClearLabelErrSign(moexpiration)

        End Sub

        Public Shared Sub SetLabelColor(lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub



        Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
         Handles DealerDropControl.SelectedDropChanged
            Try
                State.DealerId = DealerDropControl.SelectedGuid
                If DealerDropControl.SelectedIndex > 0 Then
                    PopulateProductCode()
                End If
            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try
        End Sub


#End Region



    End Class

End Namespace
