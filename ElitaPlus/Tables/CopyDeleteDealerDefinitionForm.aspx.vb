Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common


'Navigation_Header_aspx

Namespace Tables
    Partial Class CopyDeleteDealerDefinitionForm
        Inherits ElitaPlusPage

        Protected WithEvents Label2 As System.Web.UI.WebControls.Label
        Protected WithEvents Label9 As System.Web.UI.WebControls.Label
        Protected WithEvents txtItchange As System.Web.UI.WebControls.TextBox
        Protected WithEvents ErrorController As ErrorController
        Protected WithEvents moFromMultipleColumnDropControl As MultipleColumnDDLabelControl
        Protected WithEvents moToMultipleColumnDropControl As MultipleColumnDDLabelControl


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

#Region " Constants "

        Private Const APPLICATION_USER As String = "ApplicationUser"
        Private Const COPY As String = "COPY"
        Private Const DELETE As String = "DELETE"
        Private Const RENEW As String = "RENEW"
        Private Const INCLUDE_COVERAGES_AND_RATES As String = "INCLUDE_COVERAGES_AND_RATES"
        Private Const EXCLUDE_COVERAGES_AND_RATES As String = "EXCLUDE_COVERAGES_AND_RATES"
        Private Const RENEW_INDEX As Integer = 2
        Private Const DELETE_INDEX As Integer = 1
        Private Const COPY_INDEX As Integer = 0
        Private Const PRODUCTCODE_ITEM_COVERAGE_RATE_DED_TABLES As Integer = 5
        Private Const PRODUCTCODE_ITEM_COVERAGE_RATE_TABLES As Integer = 4
        Private Const PRODUCTCODE_ITEM_COVERAGE_TABLES As Integer = 3
        Private Const PRODUCTCODE_ITEM_TABLES As Integer = 2
        Private Const PRODUCTCODE_TABLE As Integer = 1
        Private Const LABEL_SELECT_FROM_DEALER As String = "FROM_DEALER"
        Private Const LABEL_SELECT_TO_DEALER As String = "TO_DEALER"
#End Region

#Region " Members "
        Private moFromDealerID As Guid = Guid.Empty
        Private moToDealerID As Guid = Guid.Empty
        Private FromDealerBO As Dealer
        Private ToDealerBO As Dealer

#End Region

#Region "Properties"
        Public ReadOnly Property FromMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moFromMultipleColumnDropControl Is Nothing Then
                    moFromMultipleColumnDropControl = CType(FindControl("moFromMultipleColumnDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moFromMultipleColumnDropControl
            End Get
        End Property

        Public ReadOnly Property ToMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moToMultipleColumnDropControl Is Nothing Then
                    moToMultipleColumnDropControl = CType(FindControl("moToMultipleColumnDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moToMultipleColumnDropControl
            End Get
        End Property

#End Region

#Region "Private Subs"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

            Try
                ErrorController.Clear_Hide()
                If Not Page.IsPostBack Then
                    MenuEnabled = False
                    PopulateDealerDropDown()
                    AddCalendar(ImageButtonEffDate, TextboxEffDate)
                    AddCalendar(ImageButtonExpDate, TextboxExpDate)
                End If
                'Me.chkExcludeCoveragesAndRates.Attributes.Add("OnCheckedChanged", "enable_disable_div(rCopyDelete.SelectedIndex, chkExcludeCoveragesAndRates.Checked)")
                DisplayProgressBarOnClick(btnSave_WRITE, Message.MSG_PERFORMING_REQUEST)

                'reqd ?
                'If Not Me.IsPostBack Then
                '    Me.AddLabelDecorations(Me.ToDealerBO)
                'End If
                If LabelEffDate.Text.IndexOf(":") > 0 Then
                    LabelEffDate.Text = LabelEffDate.Text
                Else
                    LabelEffDate.Text = LabelEffDate.Text & ":"
                End If
                If LabelExpDate.Text.IndexOf(":") > 0 Then
                    LabelExpDate.Text = LabelExpDate.Text
                Else
                    LabelExpDate.Text = LabelExpDate.Text & ":"
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrorController)
            End Try
            ShowMissingTranslations(ErrorController)

        End Sub

        Private Sub PopulateDealerDropDown()
            '''Dim DealersLookupListSortedByDec As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            '''Me.BindListControlToDataView(Me.cboDealerDecFrom, DealersLookupListSortedByDec, , , True)
            '''Me.BindListControlToDataView(Me.cboDealerDecTo, DealersLookupListSortedByDec, , , True)

            '''Dim DealersLookupListSortedByCode As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            '''DealersLookupListSortedByCode.Sort = "CODE"
            '''Me.BindListControlToDataView(Me.cboDealerCodeFrom, DealersLookupListSortedByCode, "CODE", , True)
            '''Me.BindListControlToDataView(Me.cboDealerCodeTo, DealersLookupListSortedByCode, "CODE", , True)
            Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
            ToMultipleDrop.NothingSelected = True
            'ToMultipleDrop.SetControl(False, ToMultipleDrop.MODES.NEW_MODE, True, dv, Me.TranslateLabelOrMessage(LABEL_SELECT_TO_DEALER), True, False)
            ToMultipleDrop.SetControl(False, _
                                              ToMultipleDrop.MODES.NEW_MODE, _
                                              True, _
                                              dv, _
                                              "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_TO_DEALER), _
                                              True, _
                                              False, _
                                              "", _
                                              "moToMultipleColumnDropControl_moMultipleColumnDrop", _
                                              "moToMultipleColumnDropControl_moMultipleColumnDropDesc", "moToMultipleColumnDropControl_lb_DropDown")


            FromMultipleDrop.NothingSelected = True
            'FromMultipleDrop.SetControl(False, FromMultipleDrop.MODES.NEW_MODE, True, dv, Me.TranslateLabelOrMessage(LABEL_SELECT_FROM_DEALER), True, False, , )
            FromMultipleDrop.SetControl(False, _
                                              FromMultipleDrop.MODES.NEW_MODE, _
                                              True, _
                                              dv, _
                                              "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_FROM_DEALER), _
                                              True, _
                                              False, _
                                              "", _
                                              "moFromMultipleColumnDropControl_moMultipleColumnDrop", _
                                              "moFromMultipleColumnDropControl_moMultipleColumnDropDesc", "moFromMultipleColumnDropControl_lb_DropDown")


        End Sub

        Public Sub Index_Changed(sender As System.Object, e As System.EventArgs) Handles rCopyDelete.SelectedIndexChanged
            Try
                Dim mIndex As Integer = rCopyDelete.SelectedIndex

                ToMultipleDrop.SelectedIndex = 0
                FromMultipleDrop.SelectedIndex = 0
                chkExcludeCoveragesAndRates.Checked = False

                If mIndex = DELETE_INDEX Then
                    'DELETE
                    ControlMgr.SetVisibleControl(Me, ToMultipleDrop, False)
                    ControlMgr.SetVisibleControl(Me, LabelEnterCovDateHeader, False)
                    ControlMgr.SetVisibleControl(Me, LabelStar1, False)
                    ControlMgr.SetVisibleControl(Me, LabelStar2, False)
                    ControlMgr.SetVisibleControl(Me, LabelEffDate, False)
                    ControlMgr.SetVisibleControl(Me, TextboxEffDate, False)
                    ControlMgr.SetVisibleControl(Me, LabelExpDate, False)
                    ControlMgr.SetVisibleControl(Me, TextboxExpDate, False)
                    ControlMgr.SetVisibleControl(Me, ImageButtonEffDate, False)
                    ControlMgr.SetVisibleControl(Me, ImageButtonExpDate, False)
                    chkExcludeCoveragesAndRates.Text = TranslationBase.TranslateLabelOrMessage(INCLUDE_COVERAGES_AND_RATES)
                    chkExcludeCoveragesAndRates.Checked = True
                ElseIf mIndex = RENEW_INDEX Then
                    'RENEW
                    ControlMgr.SetVisibleControl(Me, ToMultipleDrop, False)
                    ControlMgr.SetVisibleControl(Me, LabelEnterCovDateHeader, True)
                    ControlMgr.SetVisibleControl(Me, LabelStar1, True)
                    ControlMgr.SetVisibleControl(Me, LabelStar2, False)
                    ControlMgr.SetVisibleControl(Me, LabelEffDate, True)
                    ControlMgr.SetVisibleControl(Me, TextboxEffDate, True)
                    ControlMgr.SetVisibleControl(Me, LabelExpDate, False)
                    ControlMgr.SetVisibleControl(Me, TextboxExpDate, False)
                    ControlMgr.SetVisibleControl(Me, ImageButtonEffDate, True)
                    ControlMgr.SetVisibleControl(Me, ImagebuttonExpDate, False)
                    chkExcludeCoveragesAndRates.Visible = False
                Else
                    'COPY
                    ControlMgr.SetVisibleControl(Me, ToMultipleDrop, True)
                    chkExcludeCoveragesAndRates.Text = TranslationBase.TranslateLabelOrMessage(EXCLUDE_COVERAGES_AND_RATES)
                    chkExcludeCoveragesAndRates.Checked = False
                    ControlMgr.SetVisibleControl(Me, LabelEnterCovDateHeader, True)
                    ControlMgr.SetVisibleControl(Me, LabelStar1, True)
                    ControlMgr.SetVisibleControl(Me, LabelStar2, True)
                    ControlMgr.SetVisibleControl(Me, LabelEffDate, True)
                    ControlMgr.SetVisibleControl(Me, TextboxEffDate, True)
                    ControlMgr.SetVisibleControl(Me, LabelExpDate, True)
                    ControlMgr.SetVisibleControl(Me, TextboxExpDate, True)
                    ControlMgr.SetVisibleControl(Me, ImageButtonEffDate, True)
                    ControlMgr.SetVisibleControl(Me, ImagebuttonExpDate, True)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorController)
            End Try
        End Sub

        Public Sub chkExcludeCoveragesAndRates_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkExcludeCoveragesAndRates.CheckedChanged
            'Me.chkExcludeCoveragesAndRates.Attributes.Add("OnCheckedChanged", "enable_disable_div(rCopyDelete.SelectedIndex, chkExcludeCoveragesAndRates.Checked)")

            Try
                Dim mchkIndex As Boolean = chkExcludeCoveragesAndRates.Checked
                If rCopyDelete.SelectedIndex = COPY_INDEX AndAlso Not mchkIndex Then
                    ControlMgr.SetVisibleControl(Me, LabelEnterCovDateHeader, True)
                    ControlMgr.SetVisibleControl(Me, LabelStar1, True)
                    ControlMgr.SetVisibleControl(Me, LabelStar2, True)
                    ControlMgr.SetVisibleControl(Me, LabelEffDate, True)
                    ControlMgr.SetVisibleControl(Me, TextboxEffDate, True)
                    ControlMgr.SetVisibleControl(Me, LabelExpDate, True)
                    ControlMgr.SetVisibleControl(Me, TextboxExpDate, True)
                    ControlMgr.SetVisibleControl(Me, ImageButtonEffDate, True)
                    ControlMgr.SetVisibleControl(Me, ImagebuttonExpDate, True)
                Else
                    ControlMgr.SetVisibleControl(Me, LabelEnterCovDateHeader, False)
                    ControlMgr.SetVisibleControl(Me, LabelStar1, False)
                    ControlMgr.SetVisibleControl(Me, LabelStar2, False)
                    ControlMgr.SetVisibleControl(Me, LabelEffDate, False)
                    ControlMgr.SetVisibleControl(Me, TextboxEffDate, False)
                    ControlMgr.SetVisibleControl(Me, LabelExpDate, False)
                    ControlMgr.SetVisibleControl(Me, TextboxExpDate, False)
                    ControlMgr.SetVisibleControl(Me, ImageButtonEffDate, False)
                    ControlMgr.SetVisibleControl(Me, ImagebuttonExpDate, False)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorController)
            End Try
        End Sub
#End Region

#Region " Command button "

        Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                If Not ValidSelection() Then
                    Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
                End If

                If rCopyDelete.SelectedIndex = COPY_INDEX Then
                    If Not chkExcludeCoveragesAndRates.Checked Then
                        ' The dates must be entered 
                        If TextboxEffDate.Text = "" OrElse TextboxExpDate.Text = "" Then
                            Throw New GUIException(Message.MSG_GUI_INVALID_EMPTY_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_EMPTY_DATE)
                        End If
                        'check is the effective date is less than the expiration date
                        If (Date.Compare(CType(TextboxExpDate.Text, Date), CType(TextboxEffDate.Text, Date)) < 0) Then
                            Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE)
                        End If
                    End If
                    moFromDealerID = FromMultipleDrop.SelectedGuid
                    moToDealerID = ToMultipleDrop.SelectedGuid
                    If EditingValidForCopy() Then
                        ProcessCopy()
                    End If
                ElseIf rCopyDelete.SelectedIndex = RENEW_INDEX Then
                    moFromDealerID = FromMultipleDrop.SelectedGuid
                    ProcessRenew()
                Else
                    moFromDealerID = FromMultipleDrop.SelectedGuid
                    If EditingValidForDelete() Then
                        ProcessDelete()
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorController)
            End Try
        End Sub

        Private Sub btnCancel_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel_WRITE.Click
            Try
                If rCopyDelete.SelectedIndex = DELETE_INDEX Then
                    chkExcludeCoveragesAndRates.Checked = True
                    FromMultipleDrop.SelectedIndex = -1
                Else
                    chkExcludeCoveragesAndRates.Checked = False
                    FromMultipleDrop.SelectedIndex = -1
                    ToMultipleDrop.SelectedIndex = -1
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorController)
            End Try
        End Sub

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            ReturnToTabHomePage()
        End Sub

#End Region

#Region " Private Methods"
        Private Function ValidSelection() As Boolean
            If rCopyDelete.SelectedIndex = COPY_INDEX Then
                If FromMultipleDrop.SelectedGuid.Equals(Guid.Empty) OrElse ToMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                    Return False
                ElseIf FromMultipleDrop.SelectedIndex = ToMultipleDrop.SelectedIndex Then
                    Return False
                Else
                    Return True
                End If
            Else
                If FromMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                    Return False
                Else
                    Return True
                End If
            End If


        End Function

        Private Function EditingValidForCopy() As Boolean
            FromDealerBO = New Dealer(moFromDealerID)
            ToDealerBO = New Dealer(moToDealerID)

            Dim blnEditingValidForCopy As Boolean = True
            Try
                ' Verify that the from dealer has definitions to copy
                If FromDealerBO.GetDealerProductCodesCount <= 0 Then
                    DisplayMessage(Message.MSG_FROM_DEALER_CONTAINS_NO_DEFINITIONS, "", MSG_BTN_OK, MSG_TYPE_ALERT)
                    blnEditingValidForCopy = False
                    Exit Try
                End If

                ' Verify that the to dealer has a valid contract
                If Not ToDealerBO.DealerHasValidContract Then
                    DisplayMessage(Message.MSG_TO_DEALER_MUST_HAVE_A_VALID_CONTRACT, "", MSG_BTN_OK, MSG_TYPE_ALERT)
                    blnEditingValidForCopy = False
                    Exit Try
                End If

                'Req-1016 Begin
                ' Verify that the to dealer has the same RecurringPremiumId seeting on the contract
                If Not FromDealerBO.DealerHasSameRecurringPremiumSetting(ToDealerBO) Then
                    DisplayMessage(Message.MSG_TO_DEALER_MUST_HAVE_SAME_RECURRING_PREMIUM_SETTING, "", MSG_BTN_OK, MSG_TYPE_ALERT)
                    blnEditingValidForCopy = False
                    Exit Try
                End If
                'Req-1016 End

                If Not chkExcludeCoveragesAndRates.Checked Then
                    ' Verify that the entered date for new coverage are within the date of contract of to dealer
                    If Not ToDealerBO.EnteredDateWithinContract(TextboxEffDate.Text, TextboxExpDate.Text) Then
                        DisplayMessage(Message.MSG_ENTERED_DATE_NOT_WITHIN_CONTRACT, "", MSG_BTN_OK, MSG_TYPE_ALERT)
                        blnEditingValidForCopy = False
                        Exit Try
                    End If
                End If

                ' Verify that the to dealer does not have definitions
                If ToDealerBO.GetDealerProductCodesCount > 0 Then
                    DisplayMessage(Message.MSG_TO_DEALER_ALREADY_CONTAINS_DEFINITIONS, "", MSG_BTN_OK, MSG_TYPE_ALERT)
                    blnEditingValidForCopy = False
                    Exit Try
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrorController)
            End Try

            Return blnEditingValidForCopy


        End Function

        Private Function EditingValidForDelete() As Boolean
            FromDealerBO = New Dealer(moFromDealerID)

            Dim blnEditingValidForDelete As Boolean = True
            Try
                If chkExcludeCoveragesAndRates.Checked Then
                    ' Verify that the dealer selected has no certificates
                    If FromDealerBO.GetDealerCertificatesCount > 0 Then
                        DisplayMessage(Message.MSG_DEALER_ALREADY_HAS_CERTIFICATES_DEFINITIONS_CANNOT_BE_DELETED, "", MSG_BTN_OK, MSG_TYPE_ALERT)
                        blnEditingValidForDelete = False
                        Exit Try
                    End If
                Else
                    ' Verify that the dealer has no coverage definitions
                    If FromDealerBO.GetDealerCoveragesCount > 0 Then
                        DisplayMessage(Message.MSG_DEALER_ALREADY_HAS_DEFINED_COVERAGES_DEFINITIONS_CANNOT_BE_DELETED, "", MSG_BTN_OK, MSG_TYPE_ALERT)
                        blnEditingValidForDelete = False
                        Exit Try
                    End If
                End If

                ' Verify that the dealer has definitions to delete
                If FromDealerBO.GetDealerProductCodesCount <= 0 Then
                    DisplayMessage(Message.MSG_FROM_DEALER_CONTAINS_NO_DEFINITIONS, "", MSG_BTN_OK, MSG_TYPE_ALERT)
                    blnEditingValidForDelete = False
                    Exit Try
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrorController)
            End Try

            Return blnEditingValidForDelete

        End Function

        Private Sub ProcessRenew()
            Dim effDate As Date
            Dim oContract As Contract

            effDate = CType(TextboxEffDate.Text, Date)
            'verify that contract in effect on effDate is the highest contract
            oContract = Contract.GetMaxExpirationContract(moFromDealerID)
            If effDate < oContract.Effective.Value OrElse effDate > oContract.Expiration.Value Then
                oContract = Nothing
            End If

            If oContract Is Nothing Then
                DisplayMessage(Message.MSG_NO_COVERAGE_AVAILABLE, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Else
                If Dealer.RenewCoverage(moFromDealerID, oContract.Id, effDate) = 0 Then
                    DisplayMessage(Message.MSG_RENEW_WAS_COMPLETED_SUCCESSFULLY, "", MSG_BTN_OK, MSG_TYPE_INFO)
                ElseIf Dealer.RenewCoverage(moFromDealerID, oContract.Id, effDate) = -1 Then
                    DisplayMessage(Message.MSG_NO_COVERAGE_AVAILABLE, "", MSG_BTN_OK, MSG_TYPE_INFO)
                Else
                    DisplayMessage(Message.MSG_RENEW_COVERAGE_FAILED, "", MSG_BTN_OK, MSG_TYPE_ALERT)
                End If
            End If

        End Sub

        Private Sub ProcessCopy()
            Dim intCopyLevel As Integer
            Dim effDate, expDate As Date
            If chkExcludeCoveragesAndRates.Checked Then
                intCopyLevel = PRODUCTCODE_ITEM_TABLES
                effDate = Nothing
                expDate = Nothing
            Else
                intCopyLevel = PRODUCTCODE_ITEM_COVERAGE_RATE_DED_TABLES
                effDate = CType(TextboxEffDate.Text, Date)
                expDate = CType(TextboxExpDate.Text, Date)
            End If

            If Dealer.CopyDealerDefinitions(moFromDealerID, moToDealerID, intCopyLevel, effDate, expDate) = 0 Then
                DisplayMessage(Message.MSG_COPY_WAS_COMPLETED_SUCCESSFULLY, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Else
                DisplayMessage(Message.MSG_COPY_FAILED, "", MSG_BTN_OK, MSG_TYPE_ALERT)
            End If
        End Sub

        Private Sub ProcessDelete()
            Dim intDeleteLevel As Integer
            If chkExcludeCoveragesAndRates.Checked Then
                intDeleteLevel = PRODUCTCODE_ITEM_COVERAGE_RATE_TABLES
            Else
                intDeleteLevel = PRODUCTCODE_ITEM_TABLES
            End If

            If Dealer.DeleteDealerDefinitions(moFromDealerID, intDeleteLevel) = 0 Then
                DisplayMessage(Message.MSG_DELETE_WAS_COMPLETED_SUCCESSFULLY, "", MSG_BTN_OK, MSG_TYPE_INFO)
            Else
                DisplayMessage(Message.MSG_DELETE_FAILED, "", MSG_BTN_OK, MSG_TYPE_ALERT)
            End If

        End Sub

#End Region

    End Class

End Namespace

