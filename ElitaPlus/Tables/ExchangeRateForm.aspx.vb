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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    'Get the id from the parent
                    Me.State.MyBO = New CurrencyConversion(CType(Me.CallingParameters, Guid))
                    Dim oDealer As Dealer = New Dealer(Me.State.MyBO.DealerId)
                    Me.State.DealerName = oDealer.DealerName
                Else
                    Me.State.IsNew = True
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As CurrencyConversion
            Public HasDataChanged As Boolean
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As CurrencyConversion, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page Events"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.ErrorCtrl.Clear_Hide()
            'hide the user control...since we are doing our ownlist.
            'ControlMgr.SetVisibleControl(Me, PostalCodeFormatLists, False)
            Try
                If Not Me.IsPostBack Then
                    'Date Calendars
                    Me.MenuEnabled = False
                    Me.AddConfirmation(Me.btnDelete_WRITE, Message.DELETE_RECORD_PROMPT)
                    If Me.State.MyBO Is Nothing Then
                        Me.State.MyBO = New CurrencyConversion
                    End If
                    Me.AddCalendar(Me.btnEffectiveDate, Me.txtEffectiveDate)
                    Me.AddCalendar(Me.btnExpirationDate, Me.txtExpirationDate)
                    PopulateDropdowns()
                    If Me.State.IsNew = True Then
                        CreateNew()
                    End If
                    Me.PopulateFormFromBOs()
                    Me.EnableDisableFields()
                End If
                CheckIfComingFromSaveConfirm()
                BindBoPropertiesToLabels()
                'CheckIfComingFromSaveConfirm()
                If Not Me.IsPostBack Then
                    Me.AddLabelDecorations(Me.State.MyBO)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
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
            ControlMgr.SetEnableControl(Me, Me.txtToCurrency, False)
            ControlMgr.SetVisibleControl(Me, lblExchangeTo, True)
            ControlMgr.SetVisibleControl(Me, Me.txtToCurrency, True)
            ControlMgr.SetEnableControl(Me, txtEffectiveDate, False)
            ControlMgr.SetEnableControl(Me, txtExpirationDate, False)
            ControlMgr.SetVisibleControl(Me, btnEffectiveDate, False)
            ControlMgr.SetVisibleControl(Me, btnExpirationDate, False)

            If Me.State.MyBO.IsNew AndAlso Not MyIsNew Then
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

            Dim DV As CurrencyConversion.CurrencyRateDV = Me.State.MyBO.FindMaxdate(Me.State.MyBO.DealerId, Me.State.MyBO.Currency1Id, Me.State.MyBO.Currency2Id)
            Dim DVRow As DataRow = DV.Table.Rows(0)

            If DVRow(DV.COL_EFFECTIVE) Is DBNull.Value Or DVRow(DV.COL_EFFECTIVE) Is Nothing Then
            ElseIf CType(DVRow(DV.COL_EFFECTIVE), Date) <> Me.State.MyBO.EffectiveDate.Value Then
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

            TheDealerControl.SelectedGuid = Me.State.MyBO.DealerId

            'Me.BindSelectItem(Me.State.MyBO.DealerId.ToString, TheDealerControl)
            'Me.BindListControlToDataView(Me.cboFromCurrencyId, LookupListNew.GetCurrencyTypeLookupList(), , , True)
            Me.cboFromCurrencyId.Populate(CommonConfigManager.Current.ListManager.GetList("CurrencyTypeList", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
            {
              .AddBlankItem = True
            })

            'Me.BindListControlToDataView(Me.cboToCurrencyID, LookupListNew.GetCurrencyTypeLookupList(), , , True)
            Me.cboToCurrencyID.Populate(CommonConfigManager.Current.ListManager.GetList("CurrencyTypeList", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
           {
             .AddBlankItem = True
           })


        End Sub


        Protected Sub BindBoPropertiesToLabels()
            Me.BindBOPropertyToLabel(Me.State.MyBO, "DealerId", Me.TheDealerControl.CaptionLabel)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Currency1Id", Me.lblFromCurrencyId)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Currency2Id", Me.lblToCurrencyId)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "EffectiveDate", Me.lblEffectiveDate)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "ExpirationDate", Me.lblExpirationDate)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Currency1Rate", Me.lblExchangeFrom)
            Me.BindBOPropertyToLabel(Me.State.MyBO, "Currency2Rate", Me.lblExchangeTo)
            Me.ClearGridHeadersAndLabelsErrSign()

        End Sub


        Protected Sub PopulateFormFromBOs()
            With Me.State.MyBO
                'Me.SetSelectedItem(Me.moDealerDrop, .DealerId)      
                TheDealerControl.SelectedIndex = -1
                TheDealerControl.NothingSelected = True
                TheDealerControl.SelectedGuid = .DealerId
                Me.SetSelectedItem(Me.cboFromCurrencyId, .Currency1Id)
                Me.SetSelectedItem(Me.cboToCurrencyID, .Currency2Id)
                If Not .Currency1Rate Is Nothing Then
                    Me.PopulateControlFromBOProperty(Me.txtFromCurrency, FormatNumber(.Currency1Rate.Value, 9, , TriState.True, TriState.True))
                Else
                    Me.PopulateControlFromBOProperty(Me.txtFromCurrency, .Currency1Rate)
                End If

                If Not .Currency2Rate Is Nothing Then
                    Me.PopulateControlFromBOProperty(Me.txtToCurrency, FormatNumber(.Currency2Rate.Value, 9, , TriState.True, TriState.True))
                Else
                    Me.PopulateControlFromBOProperty(Me.txtToCurrency, .Currency2Rate)
                End If

                Me.PopulateControlFromBOProperty(Me.txtEffectiveDate, .EffectiveDate)
                Me.PopulateControlFromBOProperty(Me.txtExpirationDate, .EffectiveDate)
                'Me.txtDealer.Text = Me.State.DealerName
            End With

        End Sub

        Protected Sub PopulateBOsFromForm()

            With Me.State.MyBO
                'Me.PopulateBOProperty(Me.State.MyBO, "DealerId", Me.moDealerDrop)
                Me.PopulateBOProperty(Me.State.MyBO, "DealerId", Me.TheDealerControl.SelectedGuid)
                Me.PopulateBOProperty(Me.State.MyBO, "EffectiveDate", Me.txtEffectiveDate)
                Me.PopulateBOProperty(Me.State.MyBO, "ExpirationDate", Me.txtExpirationDate)
                Me.PopulateBOProperty(Me.State.MyBO, "Currency1Rate", Me.txtFromCurrency)
                'Me.PopulateBOProperty(Me.State.MyBO, "Currency2Rate", Me.txtToCurrency)
                Me.PopulateBOProperty(Me.State.MyBO, "Currency1Id", Me.cboFromCurrencyId)
                Me.PopulateBOProperty(Me.State.MyBO, "Currency2Id", Me.cboToCurrencyID)
            End With
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Protected Sub CreateNew()
            Me.State.ScreenSnapShotBO = Nothing 'Reset the backup copy

            Me.State.MyBO = New CurrencyConversion
            ' PopulateDropdowns()

            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()

        End Sub

        Protected Sub CreateNewWithCopy()

            'Me.State.IsACopy = True
            Me.PopulateBOsFromForm()

            Dim newObj As New CurrencyConversion
            newObj.Copy(Me.State.MyBO)

            Me.State.MyBO = newObj
            'Me.State.MyBO.Dealer = Nothing
            'Me.State.MyBO.DealerName = Nothing

            Me.PopulateFormFromBOs()
            Me.EnableDisableFields()

            'create the backup copy
            Me.State.ScreenSnapShotBO = New CurrencyConversion
            Me.State.ScreenSnapShotBO.Clone(Me.State.MyBO)
            'Me.State.IsACopy = False

        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_OK Then
                If Me.State.ActionInProgress <> ElitaPlusPage.DetailPageCommand.BackOnErr Then
                    Me.State.MyBO.Save()
                End If
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                        Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ReturnToCallingPage(New ReturnType(Me.State.ActionInProgress, Me.State.MyBO, Me.State.HasDataChanged))
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_NO Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
                End Select
            ElseIf Not confResponse Is Nothing AndAlso confResponse = Me.CONFIRM_MESSAGE_CANCEL Then
                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                    Case ElitaPlusPage.DetailPageCommand.New_
                        Me.CreateNew()
                    Case ElitaPlusPage.DetailPageCommand.NewAndCopy
                        Me.CreateNewWithCopy()
                    Case ElitaPlusPage.DetailPageCommand.BackOnErr
                        Me.ErrorCtrl.AddErrorAndShow(Me.State.LastErrMsg)
                End Select
            End If
            'Clean after consuming the action
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End Sub

        'Private Function ConvertRate(ByVal FromCurrency As Double) As DecimalType

        '    Return New DecimalType(Math.Round((1 / Me.State.MyBO.Currency1Rate.Value), 9))

        'End Function
#End Region

#Region "Button Clicks"

        Private Sub btnBack_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty AndAlso Not Me.State.boIsNew Then
                    Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.MyBO, Me.State.HasDataChanged))
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
                Me.AddConfirmMsg(Message.MSG_PROMPT_FOR_LEAVING_WHEN_ERROR, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.BackOnErr
                Me.State.LastErrMsg = Me.ErrorCtrl.Text
            End Try
        End Sub

        Private Sub btnCopy_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCopy_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.NewAndCopy
                Else
                    Me.CreateNewWithCopy()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnDelete_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                Me.State.MyBO.SP_Delete()
                'Me.State.MyBO.DeleteAndSave()
                Me.State.HasDataChanged = True
                Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Delete, Me.State.MyBO, Me.State.HasDataChanged))
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

        Private Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If (Me.State.MyBO.IsDirty) Then
                    Me.DisplayMessage(Message.SAVE_CHANGES_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.New_
                Else
                    Me.CreateNew()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnApply_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApply_WRITE.Click
            Try
                Me.PopulateBOsFromForm()
                If Me.State.MyBO.IsDirty Then
                    Me.State.MyBO.Save()
                    Me.State.HasDataChanged = True
                    Me.PopulateFormFromBOs()
                    If Me.State.MyBO.IsNew Then
                        Me.EnableDisableFields(True)
                        Me.State.boIsNew = True
                    Else
                        Me.EnableDisableFields()
                    End If

                    Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
                Else
                    Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
                End If
            Catch ex As ApplicationException
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                If Not Me.State.MyBO.IsNew Then
                    'Reload from the DB
                    Me.State.MyBO = New CurrencyConversion(Me.State.MyBO.Id)
                ElseIf Not Me.State.ScreenSnapShotBO Is Nothing Then
                    'It was a new with copy
                    Me.State.MyBO.Clone(Me.State.ScreenSnapShotBO)
                Else
                    Me.State.MyBO = New CurrencyConversion
                End If
                ' TheDealerControl.SelectedIndex = -1
                'TheDealerControl.NothingSelected = True
                Me.PopulateFormFromBOs()
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

        Private Sub btnGetRate_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGetRate_WRITE.Click

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

        Private Sub btnCanvert_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Dim result As Double
            result = (1 / CType(Me.txtFromCurrency.Text, Double))
            Me.txtToCurrency.Text = Math.Round(result, 9).ToString()

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
