Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Microsoft.VisualBasic
Imports System.Threading
Imports System.Globalization
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements

Namespace Tables
    Partial Class BillingCycleForm
        Inherits ElitaPlusSearchPage

#Region "Page State"

#Region "MyState"

        Class MyState

            Public moBillingCycleId As Guid = Guid.Empty
            Public IsBillingCycleNew As Boolean = False
            Public IsNewWithCopy As Boolean = False
            Public IsUndo As Boolean = False
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastOperation As DetailPageCommand = DetailPageCommand.Nothing_
            Public boChanged As Boolean = False
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public moBillingCycle As BillingCycle
            Public ScreenSnapShotBO As BillingCycle

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
            Me.State.moBillingCycleId = CType(Me.CallingParameters, Guid)
            If Me.State.moBillingCycleId.Equals(Guid.Empty) Then
                Me.State.IsBillingCycleNew = True
                BindBoPropertiesToLabels()
                Me.AddLabelDecorations(TheBillingCycle)
                ClearAll()
                PopulateAll()
            Else
                Me.State.IsBillingCycleNew = False
                BindBoPropertiesToLabels()
                Me.AddLabelDecorations(TheBillingCycle)
                PopulateAll()
            End If
        End Sub

#End Region

#Region "Constants"

        Private Const BILLING_CYCLE_FORM001 As String = "BILLING_CYCLE_FORM001" ' Billing Cycle List Exception
        Private Const BILLING_CYCLE_FORM002 As String = "BILLING_CYCLE_FORM002" ' Billing Cycle Code Field Exception
        Private Const BILLING_CYCLE_FORM003 As String = "BILLING_CYCLE_FORM003" ' Billing Cycle Update Exception
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
        Public Const LABEL_SELECT_BILLING_CYCLE As String = "BILLING_CYCLE"
        Private Const FIRST_POS As Integer = 0
        Private Const COL_NAME As String = "ID"
        Private Const LABEL_DEALER As String = "DEALER_NAME"
        Private Const YES As String = "Y"
        Private Const NO As String = "N"
        Public Const URL As String = "BillingCycleForm.aspx"

        Public Const BILLING_CYCLE_CODE_PROPERTY As String = "BillingCycleCode"
        Public Const DEALER_ID_PROPERTY As String = "DealerId"
        Public Const START_DAY_PROPERTY As String = "StartDay"
        Public Const END_DAY_PROPETRY As String = "EndDay"
        Public Const BILLING_RUNDATE_OFFSET_DAYS_PROPETRY As String = "BillingRunDateOffsetDays"
        Public Const DATE_OF_PAYMENT_OPTION_ID_PROPETRY As String = "DateOfPaymentOptionId"
        Public Const DATE_OF_PAYMENT_OFFSET_DAYS_PROPERTY As String = "DateOfPaymentOffsetDays"
        Public Const NUMBER_OF_DIGITS_ROUNDOFF_ID_PROPERTY As String = "NumberOfDigitsRoundOffId"

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public moBillingCycleId As Guid
            Public HasDataChanged As Boolean
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal oBillingCycleId As Guid, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.moBillingCycleId = oBillingCycleId
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class

#End Region

#Region "Properties"
        Private ReadOnly Property TheBillingCycle As BillingCycle
            Get
                If Me.State.moBillingCycle Is Nothing Then
                    If Me.State.IsBillingCycleNew = True Then
                        ' For creating, inserting
                        Me.State.moBillingCycle = New BillingCycle
                        Me.State.moBillingCycleId = Me.State.moBillingCycle.Id
                    Else
                        ' For updating, deleting
                        Me.State.moBillingCycle = New BillingCycle(Me.State.moBillingCycleId)
                    End If
                End If

                Return Me.State.moBillingCycle
            End Get
        End Property

        Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl_New
            Get
                If DealerDropControl Is Nothing Then
                    DealerDropControl = CType(FindControl("DealerDropControl"), MultipleColumnDDLabelControl_New)
                End If
                Return DealerDropControl
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Try
                Me.MasterPage.MessageController.Clear_Hide()
                ' ClearLabelsErrSign()
                If Not Page.IsPostBack Then
                    Me.MasterPage.MessageController.Clear()
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Tables")
                    UpdateBreadCrum()
                    Me.SetStateProperties()
                    Me.AddControlMsg(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO,
                                                                        MSG_TYPE_CONFIRM, True)

                    If Me.State.IsBillingCycleNew = True Then
                        CreateNew()
                    End If

                End If

                BindBoPropertiesToLabels()
                CheckIfComingFromConfirm()
                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(TheBillingCycle)
                End If
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
                    Me.State.moBillingCycle = New BillingCycle(CType(Me.CallingParameters, Guid))
                Else
                    Me.State.IsBillingCycleNew = True
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnApply_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
            ApplyChanges()
        End Sub

        Private Sub GoBack()
            Dim retType As New BillingCycleForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back,
                                                                Me.State.moBillingCycleId, Me.State.boChanged)
            Me.ReturnToCallingPage(retType)
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDirtyBO() = True Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM,
                                                Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    GoBack()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not Me.State.IsBillingCycleNew Then
                    'Reload from the DB
                    Me.State.moBillingCycle = New BillingCycle(Me.State.moBillingCycleId)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.moBillingCycle.Clone(Me.State.ScreenSnapShotBO)
                Else
                    Me.State.moBillingCycle = New BillingCycle
                End If
                PopulateAll()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing
            Me.State.moBillingCycleId = Guid.Empty
            Me.State.IsBillingCycleNew = True
            Me.State.moBillingCycle = New BillingCycle
            ClearAll()
            Me.SetButtonsState(True)
            Me.PopulateAll()
            TheDealerControl.ChangeEnabledControlProperty(True)


            If Me.State.IsBillingCycleNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
                'ControlMgr.SetEnableControl(Me, btnAcctSettings, False)
            End If

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

            ' Dim newObjDummy As New BillingCycle
            Dim newObj As New BillingCycle
            newObj.Copy(TheBillingCycle)

            Me.State.moBillingCycle = newObj
            Me.State.moBillingCycleId = Guid.Empty
            Me.State.IsBillingCycleNew = True

            With TheBillingCycle
                .BillingCycleCode = Nothing
                .StartDay = Nothing
                .EndDay = Nothing
                .BillingRunDateOffsetDays = Nothing
                .DateOfPaymentOptionId = Guid.Empty
                .DateOfPaymentOffsetDays = Nothing
            End With

            Me.SetButtonsState(True)
            TheDealerControl.ChangeEnabledControlProperty(True)

            'create the backup copy
            Me.State.ScreenSnapShotBO = New BillingCycle
            Me.State.ScreenSnapShotBO.Copy(TheBillingCycle)

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
                If DeleteBillingCycle() = True Then
                    Me.State.boChanged = True
                    Dim retType As New CoverageSearchForm.ReturnType(ElitaPlusPage.DetailPageCommand.Delete,
                                    Me.State.moBillingCycleId)
                    retType.BoChanged = True
                    Me.ReturnToCallingPage(retType)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Clear"

        Private Sub ClearTexts()
            moBillingCycleCodeText.Text = Nothing
            moStartDayText.Text = Nothing
            moEndDayText.Text = Nothing
            moBillingRunDateOffsetDaysText.Text = Nothing
            moDateOfPaymentOffsetDaysText.Text = Nothing
            moNextRunDateText.Text = Nothing
            moPrePaidDateOfPaymentText.Text = Nothing
            moPrePaidFromDateText.Text = Nothing
            moPrePaidToDateText.Text = Nothing
            moPostPaidDateOfPaymentText.Text = Nothing
            moPostPaidFromDateText.Text = Nothing
            moPostPaidToDateText.Text = Nothing
        End Sub

        Private Sub ClearAll()
            ClearTexts()
            TheDealerControl.ClearMultipleDrop()
            ClearList(moDateOfPaymentOptionDrop)
            ClearList(moNumberOfDigitsRoundoffDrop)
        End Sub

#End Region

#Region "Populate"

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("BILLING_CYCLE_DETAIL")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("BILLING_CYCLE_DETAIL")
                End If
            End If
        End Sub

        Private Sub PopulateDealer()
            Dim oCompanyList As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
            Try
                Dim dv As DataView = LookupListNew.GetDealerLookupList(oCompanyList, True)
                TheDealerControl.SetControl(True, TheDealerControl.MODES.NEW_MODE, True, dv, "*" + TranslationBase.TranslateLabelOrMessage(LABEL_DEALER), True, True)
                If Me.State.IsBillingCycleNew = True Then
                    TheDealerControl.SelectedGuid = Guid.Empty
                    TheDealerControl.ChangeEnabledControlProperty(True)
                Else
                    TheDealerControl.ChangeEnabledControlProperty(False)
                    TheDealerControl.SelectedGuid = TheBillingCycle.DealerId
                End If
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(BILLING_CYCLE_FORM001)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateDropDowns()
            Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Try
                BindListControlToDataView(moDateOfPaymentOptionDrop, LookupListNew.GetDateOfPaymentOPtionsList(oLanguageId), , , True)
                Dim dateOfPaymentOptionLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("DATE_OF_PAYMENT_OPTION", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
                Me.moDateOfPaymentOptionDrop.Populate(dateOfPaymentOptionLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })
                ' BindListControlToDataView(moNumberOfDigitsRoundoffDrop, LookupListNew.GetNumberOfDigitsRoundOffList(oLanguageId), , , True)
                Dim numberOfDigitsRoundoffLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("NUMBER_OF_DIGITS_ROUNDOFF", ElitaPlusIdentity.Current.ActiveUser.LanguageCode)
                Me.moNumberOfDigitsRoundoffDrop.Populate(numberOfDigitsRoundoffLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })

            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(BILLING_CYCLE_FORM001)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
        End Sub

        Private Sub PopulateTexts()
            Try
                Dim oLanguageId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                Dim TwoDecimalsId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetNumberOfDigitsRoundOffList(oLanguageId), "TWO_DECIMAL")
                With TheBillingCycle
                    If Me.State.IsBillingCycleNew = True Then
                        BindSelectItem(Nothing, moDateOfPaymentOptionDrop)
                        SetSelectedItem(Me.moDateOfPaymentOptionDrop, TwoDecimalsId)
                        moBillingCycleCodeText.Text = Nothing
                        moStartDayText.Text = Nothing
                        moEndDayText.Text = Nothing
                        moBillingRunDateOffsetDaysText.Text = Nothing
                        moDateOfPaymentOffsetDaysText.Text = Nothing
                        moPrePaidDateOfPaymentText.Text = Nothing
                        moPostPaidDateOfPaymentText.Text = Nothing
                        moPrePaidFromDateText.Text = Nothing
                        moPrePaidToDateText.Text = Nothing
                        moPostPaidFromDateText.Text = Nothing
                        moPostPaidToDateText.Text = Nothing
                    Else
                        BindSelectItem(.DateOfPaymentOptionId.ToString, moDateOfPaymentOptionDrop)
                        BindSelectItem(.NumberOfDigitsRoundOffId.ToString, moNumberOfDigitsRoundoffDrop)
                        Me.PopulateControlFromBOProperty(Me.moBillingCycleCodeText, .BillingCycleCode)
                        Me.PopulateControlFromBOProperty(Me.moStartDayText, .StartDay)
                        Me.PopulateControlFromBOProperty(Me.moEndDayText, .EndDay)
                        Me.PopulateControlFromBOProperty(Me.moBillingRunDateOffsetDaysText, .BillingRunDateOffsetDays)
                        Me.PopulateControlFromBOProperty(Me.moDateOfPaymentOffsetDaysText, .DateOfPaymentOffsetDays)
                    End If

                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateAll()
            If Me.State.IsBillingCycleNew = True Then
                PopulateDropDowns()
                PopulateDealer()
            Else
                ClearAll()
                PopulateDealer()
                PopulateDropDowns()
                PopulateTexts()
                PopulateDateFields()
            End If
        End Sub

        Protected Sub PopulateBOsFromForm()

            With Me.TheBillingCycle
                .DealerId = TheDealerControl.SelectedGuid
                Me.PopulateBOProperty(TheBillingCycle, BILLING_CYCLE_CODE_PROPERTY, Me.moBillingCycleCodeText)
                Me.PopulateBOProperty(TheBillingCycle, START_DAY_PROPERTY, Me.moStartDayText)
                Me.PopulateBOProperty(TheBillingCycle, END_DAY_PROPETRY, Me.moEndDayText)
                Me.PopulateBOProperty(TheBillingCycle, BILLING_RUNDATE_OFFSET_DAYS_PROPETRY, Me.moBillingRunDateOffsetDaysText)
                Me.PopulateBOProperty(TheBillingCycle, DATE_OF_PAYMENT_OPTION_ID_PROPETRY, Me.moDateOfPaymentOptionDrop)
                Me.PopulateBOProperty(TheBillingCycle, DATE_OF_PAYMENT_OFFSET_DAYS_PROPERTY, Me.moDateOfPaymentOffsetDaysText)
                Me.PopulateBOProperty(TheBillingCycle, NUMBER_OF_DIGITS_ROUNDOFF_ID_PROPERTY, Me.moNumberOfDigitsRoundoffDrop)

            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If

        End Sub

        Public Sub PopulateDateFields()
            Dim today As System.DateTime
            today = DateTime.Now
            Dim StartDay As Integer = If(Me.moStartDayText.Text = String.Empty Or Me.moStartDayText.Text Is Nothing, 0, CInt(moStartDayText.Text))
            Dim EndDay As Integer = If(Me.moEndDayText.Text = String.Empty Or Me.moEndDayText.Text Is Nothing, 0, If(CInt(Me.moEndDayText.Text) = 31, 0, CInt(Me.moEndDayText.Text)))
            Dim Boffsetdays As Integer
            If Me.moBillingRunDateOffsetDaysText.Text = String.Empty Then
                Boffsetdays = 0
            Else
                Boffsetdays = CInt(Me.moBillingRunDateOffsetDaysText.Text)
            End If
            Dim Doffsetdays As Integer
            If Me.moDateOfPaymentOffsetDaysText.Text = String.Empty Then
                Doffsetdays = 0
            Else
                Doffsetdays = CInt(Me.moDateOfPaymentOffsetDaysText.Text)
            End If

            If today < DateSerial(Year(today), Month(today), StartDay).AddDays(Boffsetdays) Then
                Me.moNextRunDateText.Text = DateSerial(Year(today), Month(today), StartDay).AddDays(Boffsetdays)
                Me.moPrePaidFromDateText.Text = DateSerial(Year(today), Month(today), StartDay)
                Me.moPostPaidFromDateText.Text = DateSerial(Year(today), Month(today) - 1, StartDay)
                Me.moPrePaidToDateText.Text = DateSerial(Year(today), Month(today) + 1, EndDay)
                Me.moPostPaidToDateText.Text = DateSerial(Year(today), Month(today), EndDay)
            Else
                Me.moNextRunDateText.Text = DateSerial(Year(today), Month(today) + 1, StartDay).AddDays(Boffsetdays)
                Me.moPrePaidFromDateText.Text = DateSerial(Year(today), Month(today) + 1, StartDay)
                Me.moPostPaidFromDateText.Text = DateSerial(Year(today), Month(today), StartDay)
                Me.moPrePaidToDateText.Text = DateSerial(Year(today), Month(today) + 2, EndDay)
                Me.moPostPaidToDateText.Text = DateSerial(Year(today), Month(today) + 1, EndDay)
            End If

            Dim DateOfPaymentOptionCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_DATE_OF_PAYMENT_OPTION, TheBillingCycle.DateOfPaymentOptionId)

            If Not DateOfPaymentOptionCode Is Nothing AndAlso DateOfPaymentOptionCode = "FROM_DATE" Then
                Me.moPrePaidDateOfPaymentText.Text = DateTime.Parse(Me.moPrePaidFromDateText.Text).AddDays(Doffsetdays)
                Me.moPostPaidDateOfPaymentText.Text = DateTime.Parse(Me.moPostPaidFromDateText.Text).AddDays(Doffsetdays)
            ElseIf Not DateOfPaymentOptionCode Is Nothing AndAlso DateOfPaymentOptionCode = "TO_DATE" Then
                Me.moPrePaidDateOfPaymentText.Text = DateTime.Parse(Me.moPostPaidFromDateText.Text).AddDays(Doffsetdays)
                Me.moPostPaidDateOfPaymentText.Text = DateTime.Parse(Me.moPostPaidToDateText.Text).AddDays(Doffsetdays)
            Else
                Me.moPrePaidDateOfPaymentText.Text = String.Empty
                Me.moPostPaidDateOfPaymentText.Text = String.Empty
            End If

        End Sub
#End Region

#Region "Gui-Validation"

        Private Sub SetButtonsState(ByVal bIsNew As Boolean)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, Not bIsNew)
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, Not bIsNew)
            TheDealerControl.ChangeEnabledControlProperty(bIsNew)
            ControlMgr.SetEnableControl(Me, moBillingCycleCodeText, bIsNew)

        End Sub

#End Region

#Region "Business Part"

        Private Function IsDirtyBO() As Boolean
            Dim bIsDirty As Boolean = True

            Try
                With TheBillingCycle
                    PopulateBOsFromForm()
                    bIsDirty = .IsDirty
                End With
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(BILLING_CYCLE_FORM001)
                Me.MasterPage.MessageController.AddError(ex.Message, False)
                Me.MasterPage.MessageController.Show()
            End Try
            Return bIsDirty
        End Function

        Private Function ApplyChanges() As Boolean

            Try

                Me.PopulateBOsFromForm()

                Dim errors() As ValidationError = {New ValidationError("Billing Cycle Code or Start Day or End Day is required", GetType(BillingCycle), Nothing, "DealerID", Nothing)}
                If ((Me.State.moBillingCycle.BillingCycleCode = Nothing) Or (Me.State.moBillingCycle.StartDay = Nothing) Or (Me.State.moBillingCycle.EndDay = Nothing)) Then
                    Throw New BOValidationException(errors, GetType(BillingCycle).FullName)
                End If

                If TheBillingCycle.IsDirty() Then
                    Me.TheBillingCycle.Save()
                    Me.State.boChanged = True
                    If Me.State.IsBillingCycleNew = True Then
                        Me.State.IsBillingCycleNew = False
                    End If
                    PopulateAll()
                    Me.SetButtonsState(Me.State.IsBillingCycleNew)
                    Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Function

        Private Function DeleteBillingCycle() As Boolean
            Dim bIsOk As Boolean = True

            Try
                With TheBillingCycle
                    .BeginEdit()
                    PopulateBOsFromForm()
                    .Delete()
                    .Save()
                End With
            Catch ex As Exception
                Me.MasterPage.MessageController.AddError(BILLING_CYCLE_FORM002)
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
            Me.BindBOPropertyToLabel(TheBillingCycle, DEALER_ID_PROPERTY, TheDealerControl.CaptionLabel)
            Me.BindBOPropertyToLabel(TheBillingCycle, BILLING_CYCLE_CODE_PROPERTY, moBillingCycleCodeLabel)
            Me.BindBOPropertyToLabel(TheBillingCycle, START_DAY_PROPERTY, moStartDayLabel)
            Me.BindBOPropertyToLabel(TheBillingCycle, END_DAY_PROPETRY, moEndDayLabel)
            Me.BindBOPropertyToLabel(TheBillingCycle, BILLING_RUNDATE_OFFSET_DAYS_PROPETRY, moBillingRunDateOffsetDaysLabel)
            Me.BindBOPropertyToLabel(TheBillingCycle, DATE_OF_PAYMENT_OPTION_ID_PROPETRY, moDateOfPaymentOptionLabel)
            Me.BindBOPropertyToLabel(TheBillingCycle, DATE_OF_PAYMENT_OFFSET_DAYS_PROPERTY, moDateOfPaymentOffsetDaysLabel)
            Me.BindBOPropertyToLabel(TheBillingCycle, NUMBER_OF_DIGITS_ROUNDOFF_ID_PROPERTY, moNumberOfDigitsRoundoffLabel)
        End Sub

        Private Sub ClearLabelsErrSign()
            Me.ClearLabelErrSign(moBillingCycleCodeLabel)
            Me.ClearLabelErrSign(TheDealerControl.CaptionLabel)
            Me.ClearLabelErrSign(moStartDayLabel)
            Me.ClearLabelErrSign(moEndDayLabel)
            Me.ClearLabelErrSign(moBillingRunDateOffsetDaysLabel)
            Me.ClearLabelErrSign(moDateOfPaymentOptionLabel)
            Me.ClearLabelErrSign(moDateOfPaymentOffsetDaysLabel)
            Me.ClearLabelErrSign(moPrePaidFromDateLabel)
            Me.ClearLabelErrSign(moPrePaidToDateLabel)
            Me.ClearLabelErrSign(moPostPaidFromDateLabel)
            Me.ClearLabelErrSign(moPostPaidToDateLabel)
            Me.ClearLabelErrSign(moPrePaidDateOfPaymentLabel)
            Me.ClearLabelErrSign(moPostPaidDateOfPaymentLabel)
        End Sub

        Public Shared Sub SetLabelColor(ByVal lbl As Label)
            lbl.ForeColor = Color.Black
        End Sub

        Private Sub moDateOfPaymentOptionDrop_SelectedIndexChanged(sender As Object, e As EventArgs) Handles moDateOfPaymentOptionDrop.SelectedIndexChanged,
                                                                                                             moDateOfPaymentOffsetDaysText.TextChanged,
                                                                                                             moStartDayText.TextChanged,
                                                                                                             moEndDayText.TextChanged,
                                                                                                             moBillingRunDateOffsetDaysText.TextChanged
            ClearLabelsErrSign()
            Try
                If Not moStartDayText.Text = String.Empty AndAlso Not IsNumeric(moStartDayText.Text) Then
                    SetLabelError(moStartDayLabel)
                    Me.MasterPage.MessageController.AddError(Message.MSG_INVALID_NUMBER, True)
                ElseIf Not moEndDayText.Text = String.Empty AndAlso Not IsNumeric(moEndDayText.Text) Then
                    SetLabelError(moEndDayLabel)
                    Me.MasterPage.MessageController.AddError(Message.MSG_INVALID_NUMBER, True)
                ElseIf Not moBillingRunDateOffsetDaysText.Text = String.Empty AndAlso Not IsNumeric(moBillingRunDateOffsetDaysText.Text) Then
                    SetLabelError(moBillingRunDateOffsetDaysLabel)
                    Me.MasterPage.MessageController.AddError(Message.MSG_INVALID_NUMBER, True)
                ElseIf Not moDateOfPaymentOffsetDaysText.Text = String.Empty AndAlso Not IsNumeric(moDateOfPaymentOffsetDaysText.Text) Then
                    SetLabelError(moDateOfPaymentOffsetDaysLabel)
                    Me.MasterPage.MessageController.AddError(Message.MSG_INVALID_NUMBER, True)
                Else
                    Me.PopulateBOProperty(TheBillingCycle, DATE_OF_PAYMENT_OPTION_ID_PROPETRY, Me.moDateOfPaymentOptionDrop)
                    PopulateDateFields()
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#End Region
    End Class
End Namespace
