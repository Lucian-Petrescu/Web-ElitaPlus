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
            Me.State.moBonusStructureId = CType(Me.CallingParameters, Guid)
            If Me.State.moBonusStructureId.Equals(Guid.Empty) Then
                Me.State.IsBonusStructureNew = True
                BindBoPropertiesToLabels()
                Me.AddLabelDecorations(BonusStructure)
                ClearAll()
                PopulateAll()
            Else
                Me.State.IsBonusStructureNew = False
                BindBoPropertiesToLabels()
                Me.AddLabelDecorations(BonusStructure)
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
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal oBonusStructureId As Guid, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.moBonusStructureId = oBonusStructureId
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class

#End Region

#Region "Properties"
        Private ReadOnly Property BonusStructure As ClaimBonusSettings
            Get
                If Me.State.moBonusStructure Is Nothing Then
                    If Me.State.IsBonusStructureNew = True Then
                        ' For creating, inserting
                        Me.State.moBonusStructure = New ClaimBonusSettings
                        Me.State.moBonusStructureId = Me.State.moBonusStructure.Id
                    Else
                        ' For updating, deleting
                        Me.State.moBonusStructure = New ClaimBonusSettings(Me.State.moBonusStructureId)
                    End If
                End If

                Return Me.State.moBonusStructure
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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.MasterPage.MessageController.Clear_Hide()

                If Not Page.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()
                    Me.SetStateProperties()
                End If

                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()
                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(BonusStructure)
                End If

                ClientScript.RegisterStartupScript(Me.Page.GetType, "startup", "bonusMethodChanged();", True)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            If Me.State.LastOperation = DetailPageCommand.Redirect_ Then
                Me.MasterPage.MessageController.Clear_Hide()
                ClearLabelsErrSign()
                Me.State.LastOperation = DetailPageCommand.Nothing_
            Else
                Me.ShowMissingTranslations(Me.MasterPage.MessageController)
            End If
        End Sub

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall

            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Me.State.moBonusStructure = New ClaimBonusSettings(CType(Me.CallingParameters, Guid))
                Else
                    Me.State.IsBonusStructureNew = True
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnApply_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
            If Me.State.IsBonusStructureNew = True Then
                ApplyChanges()

            ElseIf Me.State.IsBonusStructureNew = False Then
                If IsDirtyBO() = True Then
                    ApplyChanges()
                Else
                    Me.DisplayMessage(Message.NO_CHANGES_TO_RECORD, "", MSG_BTN_OK, MSG_TYPE_INFO)
                End If
            End If
        End Sub

        Private Sub GoBack()
            Dim retType As New BonusStructureDetailForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.moBonusStructureId, Me.State.boChanged)
            Me.ReturnToCallingPage(retType)
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


        Private Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing
            Me.State.moBonusStructureId = Guid.Empty
            Me.State.IsBonusStructureNew = True
            Me.State.moBonusStructure = New ClaimBonusSettings
            ClearAll()
            Me.SetButtonsState(True)
            Me.PopulateAll()
            DealerDropControl.ChangeEnabledControlProperty(True)
            ServiceCenterDropControl.ChangeEnabledControlProperty(True)

        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNewCopy()

            Me.PopulateBOsFromForm()

            Dim newObj As New ClaimBonusSettings
            newObj.Copy(BonusStructure)

            Me.State.moBonusStructure = newObj
            Me.State.moBonusStructureId = Guid.Empty
            Me.State.IsBonusStructureNew = True

            With BonusStructure
                .BonusComputeMethodId = Guid.Empty
                .ScAvgTat = Nothing
                .ScReplacementPct = Nothing
                .BonusAmountPeriodMonth = Nothing
                .Effective = Nothing
                .Expiration = Nothing
            End With

            Me.SetButtonsState(True)
            ' DealerDropControl.ChangeEnabledControlProperty(True)

            'create the backup copy
            Me.State.ScreenSnapShotBO = New ClaimBonusSettings
            Me.State.ScreenSnapShotBO.Copy(BonusStructure)



        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewCopy()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
                Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try

                Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenDelDeletePromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete



            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("BONUS_STRUCTURE_DETAIL")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("BONUS_STRUCTURE_DETAIL")
                End If
            End If
        End Sub
        Private Sub PopulateDealer()

            Try
                DealerDropControl.AutoPostBackDD = True
                DealerDropControl.Caption = TranslationBase.TranslateLabelOrMessage(LABEL_DEALER) & "&nbsp;&nbsp;&nbsp;"
                DealerDropControl.NothingSelected = True
                DealerDropControl.BindData(LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies))

                If Me.State.IsBonusStructureNew = True Then

                    DealerDropControl.ChangeEnabledControlProperty(True)
                    DealerDropControl.SelectedGuid = Guid.Empty

                Else

                    DealerDropControl.SelectedGuid = BonusStructure.DealerId
                End If
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(BONUS_STRUCTURE_FORM001)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
        End Sub
        Private Sub PopulateServiceCenter()
            Dim oCountryList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Countries
            Try
                Dim dv As DataView = LookupListNew.GetServiceCenterLookupList(oCountryList)
                ServiceCenterDropControl.SetControl(True, ServiceCenterDropControl.MODES.NEW_MODE, True, dv, TranslationBase.TranslateLabelOrMessage(LABEL_SERVICE_CENTER), True, True)
                If Me.State.IsBonusStructureNew = True Then
                    ServiceCenterDropControl.SelectedGuid = Guid.Empty
                    ServiceCenterDropControl.ChangeEnabledControlProperty(True)

                Else

                    ServiceCenterDropControl.ChangeEnabledControlProperty(True)
                    ServiceCenterDropControl.SelectedGuid = BonusStructure.ServiceCenterId
                End If
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(BONUS_STRUCTURE_FORM001)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
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
                If Me.State.IsBonusStructureNew = True Then
                    moProductCode.SelectedValue = Nothing
                    moProductCode.Enabled = True

                Else
                    BindSelectItem(Me.BonusStructure.ProductCodeId.ToString, moProductCode)

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
                If Me.State.IsBonusStructureNew = True Then
                    BindSelectItem(Nothing, moBonusMethodComputationDD)
                Else
                    BindSelectItem(Me.BonusStructure.BonusComputeMethodId.ToString, moBonusMethodComputationDD)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateTexts()
            Try
                Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId

                With BonusStructure


                    Me.PopulateControlFromBOProperty(Me.moAVGTATText, .ScAvgTat)
                    Me.PopulateControlFromBOProperty(Me.moPercentageOrAmountText, .Pecoramount)
                    Me.PopulateControlFromBOProperty(Me.moPriorityText, .Priority)
                    Me.PopulateControlFromBOProperty(Me.moreplacementpercentageText, .ScReplacementPct)
                    Me.PopulateControlFromBOProperty(Me.mobonusamountperiodText, .BonusAmountPeriodMonth)


                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateAll()
            If Me.State.IsBonusStructureNew = True Then
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

            With Me.BonusStructure
                .DealerId = DealerDropControl.SelectedGuid
                .ServiceCenterId = ServiceCenterDropControl.SelectedGuid
                .ProductCodeId = If(String.IsNullOrEmpty(Me.moProductCode.SelectedValue), Guid.Empty, New Guid(Me.moProductCode.SelectedValue))
                Me.PopulateBOProperty(BonusStructure, BONUS_METHOD_COMPUTATION_ID, Me.moBonusMethodComputationDD)
                Me.PopulateBOProperty(BonusStructure, AVG_TAT, Me.moAVGTATText)
                Me.PopulateBOProperty(BonusStructure, PERCENTAGE_OR_AMOUNT, Me.moPercentageOrAmountText)
                Me.PopulateBOProperty(BonusStructure, PRIORITY, Me.moPriorityText)
                Me.PopulateBOProperty(BonusStructure, SC_REPLACEMENT_PERCENTAGE, Me.moreplacementpercentageText)
                Me.PopulateBOProperty(BonusStructure, BONUS_AMOUNT_PERIOD_MONTH, Me.mobonusamountperiodText)
                Me.PopulateBOProperty(BonusStructure, EFFECTIVE_DATE, Me.moeffectiveText)
                Me.PopulateBOProperty(BonusStructure, EXPIRATION_DATE, Me.moexpirationText)

            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub

        Public Sub PopulateDateFields()
            Dim today As System.DateTime
            today = DateTime.Now


            Me.PopulateControlFromBOProperty(Me.moeffectiveText, Me.BonusStructure.Effective)
            Me.PopulateControlFromBOProperty(Me.moexpirationText, Me.BonusStructure.Expiration)
            Me.AddCalendarwithTime_New(btneffective, moeffectiveText, , moeffectiveText.Text)
            Me.AddCalendarwithTime_New(btnExpiration, moexpirationText, , moexpirationText.Text)


        End Sub

#End Region

#Region "Button State"

        Private Sub SetButtonsState(ByVal bIsNew As Boolean)
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
                Me.MasterPage.MessageController.Show()
            End Try

            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean
            Dim eff As Date = Date.Now
            Try
                Me.customvalidate()

                If Me.State.IsBonusStructureNew = False Then

                    If Me.State.moBonusStructure.Effective.Value.Ticks <> CType(moeffectiveText.Text, Date).Ticks Then
                        eff = CType(moeffectiveText.Text, Date).AddSeconds(1)
                    End If

                    Me.State.moBonusStructure.DeleteAndSave()

                    Me.State.moBonusStructureId = Guid.Empty
                    Me.State.IsBonusStructureNew = True
                    Me.State.moBonusStructure = New ClaimBonusSettings

                    moeffectiveText.Text = eff.AddSeconds(1).ToString()
                End If

                Me.PopulateBOsFromForm()

                If BonusStructure.IsDirty() Then
                    Me.BonusStructure.Save()
                    Me.State.boChanged = True
                    If Me.State.IsBonusStructureNew = True Then
                        Me.State.IsBonusStructureNew = False
                    End If
                    PopulateAll()
                    Me.SetButtonsState(Me.State.IsBonusStructureNew)
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Function


        Private Function customvalidate() As Boolean

            If Me.DealerDropControl.SelectedGuid.Equals(Guid.Empty) And Me.ServiceCenterDropControl.SelectedGuid.Equals(Guid.Empty) Then
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

            Dim ProductCodeId As Guid = If(String.IsNullOrEmpty(Me.moProductCode.SelectedValue), Guid.Empty, New Guid(Me.moProductCode.SelectedValue))

            If Me.State.moBonusStructure.GetClaimBonusSettingCount(DealerDropControl.SelectedGuid, ServiceCenterDropControl.SelectedGuid, ProductCodeId, Me.State.moBonusStructureId) > 0 Then
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
                Me.MasterPage.MessageController.AddError(BONUS_STRUCTURE_FORM002)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
                bIsOk = False
            End Try
            Return bIsOk
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
        Protected Sub ComingFromDelete()

            Dim confResponseDel As String = Me.HiddenDelDeletePromptResponse.Value

            If Not confResponseDel Is Nothing AndAlso confResponseDel = Me.MSG_VALUE_YES Then
                If Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Delete Then
                    Try
                        Me.State.moBonusStructure.DeleteAndSave()
                        Me.State.HasDataChanged = True
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.moBonusStructureId, Me.State.HasDataChanged))
                    Catch ex As Exception
                        Me.HandleErrors(ex, Me.MasterPage.MessageController)
                    End Try
                    'Clean after consuming the action
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                    Me.HiddenDelDeletePromptResponse.Value = ""
                End If
            ElseIf Not confResponseDel Is Nothing AndAlso confResponseDel = Me.MSG_VALUE_NO Then
                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenDelDeletePromptResponse.Value = ""
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
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        ComingFromDelete()

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
            Me.BindBOPropertyToLabel(BonusStructure, DEALER_ID_PROPERTY, DealerDropControl.CaptionLabel)
            Me.BindBOPropertyToLabel(BonusStructure, SERVICE_CENTER_PROPERTY, ServiceCenterDropControl.CaptionLabel)
            Me.BindBOPropertyToLabel(BonusStructure, PRODUCT_CODE_PROPERTY, moProductCodelabel)
            Me.BindBOPropertyToLabel(BonusStructure, BONUS_METHOD_COMPUTATION_ID, moBonusMethodComputationLabel)
            Me.BindBOPropertyToLabel(BonusStructure, AVG_TAT, moAVGTAT)
            Me.BindBOPropertyToLabel(BonusStructure, PERCENTAGE_OR_AMOUNT, moPercentageOrAmountLabel)
            Me.BindBOPropertyToLabel(BonusStructure, PRIORITY, moPriorityLabel)
            Me.BindBOPropertyToLabel(BonusStructure, SC_REPLACEMENT_PERCENTAGE, moreplacementpercentage)
            Me.BindBOPropertyToLabel(BonusStructure, BONUS_AMOUNT_PERIOD_MONTH, mobonusamountperiod)
            Me.BindBOPropertyToLabel(BonusStructure, EFFECTIVE_DATE, moeffective)
            Me.BindBOPropertyToLabel(BonusStructure, EXPIRATION_DATE, moexpiration)
            ClearLabelsErrSign()
        End Sub


        Private Sub ClearLabelsErrSign()

            Me.ClearLabelErrSign(DealerDropControl.CaptionLabel)
            Me.ClearLabelErrSign(ServiceCenterDropControl.CaptionLabel)
            Me.ClearLabelErrSign(moProductCodelabel)
            Me.ClearLabelErrSign(moBonusMethodComputationLabel)
            Me.ClearLabelErrSign(moAVGTAT)
            Me.ClearLabelErrSign(moPercentageOrAmountLabel)
            Me.ClearLabelErrSign(moPriorityLabel)
            Me.ClearLabelErrSign(moreplacementpercentage)
            Me.ClearLabelErrSign(mobonusamountperiod)
            Me.ClearLabelErrSign(moeffective)
            Me.ClearLabelErrSign(moexpiration)

        End Sub

        Public Shared Sub SetLabelColor(ByVal lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub



        Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
         Handles DealerDropControl.SelectedDropChanged
            Try
                Me.State.DealerId = DealerDropControl.SelectedGuid
                If DealerDropControl.SelectedIndex > 0 Then
                    PopulateProductCode()
                End If
            Catch ex As Exception
                HandleErrors(ex, Me.moErrorController)
            End Try
        End Sub


#End Region



    End Class

End Namespace
