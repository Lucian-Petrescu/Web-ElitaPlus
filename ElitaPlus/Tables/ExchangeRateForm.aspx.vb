Imports Microsoft.VisualBasic
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Tables
    Partial Class ExchangeRateForm
        Inherits ElitaPlusPage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents ErrorCtrl As ErrorController
        Protected WithEvents multipleDealerDropControl As MultipleColumnDDLabelControl

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants"
        Public Shared URL As String = "ExchangeRateForm.aspx"
        Private Const DEFAULT_LAST_CERTIFICATE_NUMBER As String = "0"
        Private Const INSURANCE_COMPANY_TYPE As String = "1"
        Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"
#End Region

#Region "ENUMERATIONS"

        Public Enum enumPermissionType
            ENUM_NONE = 0
            ENUM_SINGLE = 1
            ENUM_MULTIPLE = 2
        End Enum

#End Region

#Region "Properties"
        Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
            Get
                If multipleDealerDropControl Is Nothing Then
                    multipleDealerDropControl = CType(FindControl("multipleDealerDropControl"), MultipleColumnDDLabelControl)
                End If
                Return multipleDealerDropControl
            End Get
        End Property

#End Region

#Region "Page State"

        Class MyState
            Public MyBO As CurrencyConversion
            Public ScreenSnapShotBO As CurrencyConversion
            Public IsNew As Boolean
            Public IsACopy As Boolean
            Public CmpnId As Guid
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public LastErrMsg As String
            Public HasDataChanged As Boolean
            Public DealerName As String
            Public ht As Hashtable
            Public boIsNew As Boolean = False
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    State.MyBO = New CurrencyConversion(CType(CallingParameters, Guid))
                    Dim oDealer As Dealer = New Dealer(State.MyBO.DealerId)
                    State.DealerName = oDealer.DealerName
                Else
                    State.IsNew = True
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As CurrencyConversion
            Public HasDataChanged As Boolean
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As CurrencyConversion, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page Events"
        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            ErrorCtrl.Clear_Hide()
            'hide the user control...since we are doing our ownlist.
            'ControlMgr.SetVisibleControl(Me, PostalCodeFormatLists, False)
            Try
                If Not IsPostBack Then
                    'Date Calendars
                    MenuEnabled = False
                    AddConfirmation(btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                    If State.MyBO Is Nothing Then
                        State.MyBO = New CurrencyConversion
                    End If
                    AddCalendar(btnEffectiveDate, txtEffectiveDate)
                    AddCalendar(btnExpirationDate, txtExpirationDate)
                    PopulateDropdowns()
                    If State.IsNew = True Then
                        CreateNew()
                    End If
                    PopulateFormFromBOs()
                    EnableDisableFields()
                End If
                CheckIfComingFromSaveConfirm()
                BindBoPropertiesToLabels()
                'CheckIfComingFromSaveConfirm()
                If Not IsPostBack Then
                    AddLabelDecorations(State.MyBO)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
        End Sub
#End Region

#Region "Controlling Logic"

        Protected Sub EnableDisableFields(Optional ByVal MyIsNew As Boolean = False)
            'Enabled by Default
            ControlMgr.SetEnableControl(Me, btnDelete_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnCopy_WRITE, True)
            'TheDealerControl
            'ControlMgr.SeteControl(Me, txtDealer, True)     
            TheDealerControl.ChangeEnabledControlProperty(False)
            'ControlMgr.SetEnableControl(Me, TheDealerControl., False)
            ControlMgr.SetEnableControl(Me, txtToCurrency, False)
            ControlMgr.SetVisibleControl(Me, lblExchangeTo, True)
            ControlMgr.SetVisibleControl(Me, txtToCurrency, True)
            ControlMgr.SetEnableControl(Me, txtEffectiveDate, False)
            ControlMgr.SetEnableControl(Me, txtExpirationDate, False)
            ControlMgr.SetVisibleControl(Me, btnEffectiveDate, False)
            ControlMgr.SetVisibleControl(Me, btnExpirationDate, False)

            If State.MyBO.IsNew AndAlso Not MyIsNew Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnNew_WRITE, False)
                ControlMgr.SetEnableControl(Me, btnCopy_WRITE, False)
                'ControlMgr.SetVisibleControl(Me, txtDealer, False)
                TheDealerControl.ChangeEnabledControlProperty(True)
                ControlMgr.SetVisibleControl(Me, lblExchangeTo, False)
                ControlMgr.SetVisibleControl(Me, txtToCurrency, False)
                ControlMgr.SetEnableControl(Me, txtEffectiveDate, True)
                ControlMgr.SetEnableControl(Me, txtExpirationDate, True)
                ControlMgr.SetVisibleControl(Me, btnEffectiveDate, True)
                ControlMgr.SetVisibleControl(Me, btnExpirationDate, True)
            End If

            Dim DV As CurrencyConversion.CurrencyRateDV = State.MyBO.FindMaxdate(State.MyBO.DealerId, State.MyBO.Currency1Id, State.MyBO.Currency2Id)
            Dim DVRow As DataRow = DV.Table.Rows(0)

            If DVRow(DV.COL_EFFECTIVE) Is DBNull.Value OrElse DVRow(DV.COL_EFFECTIVE) Is Nothing Then
            ElseIf CType(DVRow(DV.COL_EFFECTIVE), Date) <> State.MyBO.EffectiveDate.Value Then
                ControlMgr.SetEnableControl(Me, btnDelete_WRITE, False)
            End If
            'WRITE YOU OWN CODE HERE
        End Sub

        Protected Sub PopulateDropdowns()
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            TheDealerControl.SetControl(True,
                                        TheDealerControl.MODES.NEW_MODE,
                                        True,
                                        oDealerview,
                                        "* " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE),
                                        True, True)

            TheDealerControl.SelectedGuid = State.MyBO.DealerId

            'Me.BindSelectItem(Me.State.MyBO.DealerId.ToString, TheDealerControl)
            'Me.BindListControlToDataView(Me.cboFromCurrencyId, LookupListNew.GetCurrencyTypeLookupList(), , , True)
            cboFromCurrencyId.Populate(CommonConfigManager.Current.ListManager.GetList("CurrencyTypeList", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
            {
              .AddBlankItem = True
            })

            'Me.BindListControlToDataView(Me.cboToCurrencyID, LookupListNew.GetCurrencyTypeLookupList(), , , True)
            cboToCurrencyID.Populate(CommonConfigManager.Current.ListManager.GetList("CurrencyTypeList", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
           {
             .AddBlankItem = True
           })


        End Sub


        Protected Sub BindBoPropertiesToLabels()
            BindBOPropertyToLabel(State.MyBO, "DealerId", TheDealerControl.CaptionLabel)
            BindBOPropertyToLabel(State.MyBO, "Currency1Id", lblFromCurrencyId)
            BindBOPropertyToLabel(State.MyBO, "Currency2Id", lblToCurrencyId)
            BindBOPropertyToLabel(State.MyBO, "EffectiveDate", lblEffectiveDate)
            BindBOPropertyToLabel(State.MyBO, "ExpirationDate", lblExpirationDate)
            BindBOPropertyToLabel(State.MyBO, "Currency1Rate", lblExchangeFrom)
            BindBOPropertyToLabel(State.MyBO, "Currency2Rate", lblExchangeTo)
            ClearGridHeadersAndLabelsErrSign()

        End Sub


        Protected Sub PopulateFormFromBOs()
            With State.MyBO
                'Me.SetSelectedItem(Me.moDealerDrop, .DealerId)      
                TheDealerControl.SelectedIndex = -1
                TheDealerControl.NothingSelected = True
                TheDealerControl.SelectedGuid = .DealerId
                SetSelectedItem(cboFromCurrencyId, .Currency1Id)
                SetSelectedItem(cboToCurrencyID, .Currency2Id)
                If .Currency1Rate IsNot Nothing Then
                    PopulateControlFromBOProperty(txtFromCurrency, FormatNumber(.Currency1Rate.Value, 9, , TriState.True, TriState.True))
                Else
                    PopulateControlFromBOProperty(txtFromCurrency, .Currency1Rate)
                End If

                If .Currency2Rate IsNot Nothing Then
                    PopulateControlFromBOProperty(txtToCurrency, FormatNumber(.Currency2Rate.Value, 9, , TriState.True, TriState.True))
                Else
                    PopulateControlFromBOProperty(txtToCurrency, .Currency2Rate)
                End If

                PopulateControlFromBOProperty(txtEffectiveDate, .EffectiveDate)
                PopulateControlFromBOProperty(txtExpirationDate, .EffectiveDate)
                'Me.txtDealer.Text = Me.State.DealerName
            End With

        End Sub

        Protected Sub PopulateBOsFromForm()

            With State.MyBO
                'Me.PopulateBOProperty(Me.State.MyBO, "DealerId", Me.moDealerDrop)
                PopulateBOProperty(State.MyBO, "DealerId", TheDealerControl.SelectedGuid)
                PopulateBOProperty(State.MyBO, "EffectiveDate", txtEffectiveDate)
                PopulateBOProperty(State.MyBO, "ExpirationDate", txtExpirationDate)
                PopulateBOProperty(State.MyBO, "Currency1Rate", txtFromCurrency)
                'Me.PopulateBOProperty(Me.State.MyBO, "Currency2Rate", Me.txtToCurrency)
                PopulateBOProperty(State.MyBO, "Currency1Id", cboFromCurrencyId)
                PopulateBOProperty(State.MyBO, "Currency2Id", cboToCurrencyID)
            End With
            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Protected Sub CreateNew()
            State.ScreenSnapShotBO = Nothing 'Reset the backup copy

            State.MyBO = New CurrencyConversion
            ' PopulateDropdowns()

            PopulateFormFromBOs()
            EnableDisableFields()

        End Sub

        Protected Sub CreateNewWithCopy()

            'Me.State.IsACopy = True
            PopulateBOsFromForm()

            Dim newObj As New CurrencyConversion
            newObj.Copy(State.MyBO)

            State.MyBO = newObj
            'Me.State.MyBO.Dealer = Nothing
            'Me.State.MyBO.DealerName = Nothing

            PopulateFormFromBOs()
            EnableDisableFields()

            'create the backup copy
            State.ScreenSnapShotBO = New CurrencyConversion
            State.ScreenSnapShotBO.Clone(State.MyBO)
            'Me.State.IsACopy = False

        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = CONFIRM_MESSAGE_OK Then
                If State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    State.MyBO.Save()
                End If
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ReturnToCallingPage(New ReturnType(State.ActionInProgress, State.MyBO, State.HasDataChanged))
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ErrorCtrl.AddErrorAndShow(State.LastErrMsg)
                End Select
            ElseIf confResponse IsNot Nothing AndAlso confResponse = CONFIRM_MESSAGE_CANCEL Then
                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        ErrorCtrl.AddErrorAndShow(State.LastErrMsg)
                End Select
            End If
            'Clean after consuming the action
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            HiddenSaveChangesPromptResponse.Value = ""
        End Sub

        'Private Function ConvertRate(ByVal FromCurrency As Double) As DecimalType

        '    Return New DecimalType(Math.Round((1 / Me.State.MyBO.Currency1Rate.Value), 9))

        'End Function
#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click1(sender As Object, e As System.EventArgs) Handles btnBack.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty AndAlso Not State.boIsNew Then
                    AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.MyBO, State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
                AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                State.LastErrMsg = ErrorCtrl.Text
            End Try
        End Sub

        Private Sub btnCopy_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    CreateNewWithCopy()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                PopulateBOsFromForm()
                State.MyBO.SP_Delete()
                'Me.State.MyBO.DeleteAndSave()
                State.HasDataChanged = True
                ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, State.MyBO, State.HasDataChanged))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

        Private Sub btnNew_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                PopulateBOsFromForm()
                If (State.MyBO.IsDirty) Then
                    DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    CreateNew()
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnApply_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                PopulateBOsFromForm()
                If State.MyBO.IsDirty Then
                    State.MyBO.Save()
                    State.HasDataChanged = True
                    PopulateFormFromBOs()
                    If State.MyBO.IsNew Then
                        EnableDisableFields(True)
                        State.boIsNew = True
                    Else
                        EnableDisableFields()
                    End If

                    AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As ApplicationException
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not State.MyBO.IsNew Then
                    'Reload from the DB
                    State.MyBO = New CurrencyConversion(State.MyBO.Id)
                ElseIf State.ScreenSnapShotBO IsNot Nothing Then
                    'It was a new with copy
                    State.MyBO.Clone(State.ScreenSnapShotBO)
                Else
                    State.MyBO = New CurrencyConversion
                End If
                ' TheDealerControl.SelectedIndex = -1
                'TheDealerControl.NothingSelected = True
                PopulateFormFromBOs()
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

        Private Sub btnGetRate_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnGetRate_WRITE.Click

            'Dim FromSymble As String
            'Dim ToSymble As String
            'Try
            '    'Me.GetSelectedItem(moDealerDrop)
            '    Dim oCurrency As ELP.Currency = New ELP.Currency(Me.GetSelectedItem(cboFromCurrencyId))
            '    FromSymble = oCurrency.IsoCode
            '    oCurrency = New ELP.Currency(Me.GetSelectedItem(cboToCurrencyID))
            '    ToSymble = oCurrency.IsoCode
            '    Dim ws As New WS_CurrencyConvertor.CurrencyConvertor
            '    initializeAvailableCurrencies()
            '    Dim From As Double
            '    Dim FromCurr As Double
            '    Dim ToCurr As Double
            '    If FromSymble = "USD" Then
            '        FromCurr = 1
            '    Else
            '        FromCurr = ws.ConversionRate(getCurrency("USD"), getCurrency(FromSymble))
            '    End If

            '    If ToSymble = "USD" Then
            '        ToCurr = 1
            '    Else
            '        ToCurr = ws.ConversionRate(getCurrency("USD"), getCurrency(ToSymble))
            '    End If

            '    Me.PopulateControlFromBOProperty(Me.txtFromCurrency, FromCurr)
            '    From = FromCurr
            '    'result = From * (1 / FromCurr) * ToCurr
            '    'Me.PopulateControlFromBOProperty(Me.txtToCurrency, (From * (1 / FromCurr) * ToCurr))

            '    Me.PopulateControlFromBOProperty(Me.txtToCurrency, (Math.Round((ToCurr / FromCurr), 9)))

            'Catch ex As Exception
            '    Me.HandleErrors(ex, Me.ErrorCtrl)
            'End Try
            ''dlbto = ws.ConversionRate(getCurrency(ToSymble), getCurrency(FromSymble))
            ''Me.PopulateControlFromBOProperty(Me.txtToCurrency, dlbto)
        End Sub

        'Private Function getCurrency(ByVal symbol As String) As WS_CurrencyConvertor.Currency
        '    Dim i As Integer = 0
        '    If Me.State.ht.Contains(symbol) Then
        '        i = CInt(Me.State.ht(symbol))
        '    End If
        '    Return CType(i, WS_CurrencyConvertor.Currency)
        'End Function

        Private Sub btnCanvert_WRITE_Click(sender As System.Object, e As System.EventArgs)
            Dim result As Double
            result = (1 / CType(txtFromCurrency.Text, Double))
            txtToCurrency.Text = Math.Round(result, 9).ToString()

        End Sub


        'Sub initializeAvailableCurrencies()
        '    Dim al As New ArrayList(164)
        '    Me.State.ht = New Hashtable(164)

        '    Dim currency As Type = GetType(WS_CurrencyConvertor.Currency)

        '    Dim key As Integer = 0
        '    Dim s As FieldInfo
        '    For Each s In currency.GetFields()
        '        Dim str As String = s.Name
        '        If str.Length = 3 Then
        '            al.Add(str)
        '            Me.State.ht.Add(str, key)
        '            key += 1
        '        End If
        '    Next s
        'End Sub

        'Private Sub moDealerDrop_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moDealerDrop.SelectedIndexChanged
        '    Me.State.MyBO.DealerId = Me.GetSelectedItem(Me.moDealerDrop)
        '    Dim oDealer As Dealer = New Dealer(Me.GetSelectedItem(moDealerDrop))
        '    Me.State.DealerName = oDealer.DealerName
        'End Sub

        'Private Sub OnFromDrop_Changed(ByVal fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
        '   Handles multipleDealerDropControl.SelectedDropChanged

        '    Me.State.MyBO.DealerId = TheDealerControl.SelectedGuid
        '    Dim oDealer As Dealer = New Dealer(TheDealerControl.SelectedGuid)
        '    Me.State.DealerName = oDealer.DealerName
        'End Sub
    End Class
End Namespace
