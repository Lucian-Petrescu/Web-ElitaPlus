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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

            Try
                Me.ErrorController.Clear_Hide()
                If Not Page.IsPostBack Then
                    Me.MenuEnabled = False
                    PopulateDealerDropDown()
                    Me.AddCalendar(Me.ImageButtonEffDate, Me.TextboxEffDate)
                    Me.AddCalendar(Me.ImageButtonExpDate, Me.TextboxExpDate)
                End If
                'Me.chkExcludeCoveragesAndRates.Attributes.Add("OnCheckedChanged", "enable_disable_div(rCopyDelete.SelectedIndex, chkExcludeCoveragesAndRates.Checked)")
                Me.DisplayProgressBarOnClick(Me.btnSave_WRITE, Message.MSG_PERFORMING_REQUEST)

                'reqd ?
                'If Not Me.IsPostBack Then
                '    Me.AddLabelDecorations(Me.ToDealerBO)
                'End If
                If Me.LabelEffDate.Text.IndexOf(":") > 0 Then
                    Me.LabelEffDate.Text = Me.LabelEffDate.Text
                Else
                    Me.LabelEffDate.Text = Me.LabelEffDate.Text & ":"
                End If
                If Me.LabelExpDate.Text.IndexOf(":") > 0 Then
                    Me.LabelExpDate.Text = Me.LabelExpDate.Text
                Else
                    Me.LabelExpDate.Text = Me.LabelExpDate.Text & ":"
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorController)
            End Try
            Me.ShowMissingTranslations(ErrorController)

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

        Public Sub Index_Changed(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rCopyDelete.SelectedIndexChanged
            Try
                Dim mIndex As Integer = rCopyDelete.SelectedIndex

                Me.ToMultipleDrop.SelectedIndex = 0
                Me.FromMultipleDrop.SelectedIndex = 0
                Me.chkExcludeCoveragesAndRates.Checked = False

                If mIndex = DELETE_INDEX Then
                    'DELETE
                    ControlMgr.SetVisibleControl(Me, Me.ToMultipleDrop, False)
                    ControlMgr.SetVisibleControl(Me, Me.LabelEnterCovDateHeader, False)
                    ControlMgr.SetVisibleControl(Me, Me.LabelStar1, False)
                    ControlMgr.SetVisibleControl(Me, Me.LabelStar2, False)
                    ControlMgr.SetVisibleControl(Me, Me.LabelEffDate, False)
                    ControlMgr.SetVisibleControl(Me, Me.TextboxEffDate, False)
                    ControlMgr.SetVisibleControl(Me, Me.LabelExpDate, False)
                    ControlMgr.SetVisibleControl(Me, Me.TextboxExpDate, False)
                    ControlMgr.SetVisibleControl(Me, Me.ImageButtonEffDate, False)
                    ControlMgr.SetVisibleControl(Me, Me.ImageButtonExpDate, False)
                    Me.chkExcludeCoveragesAndRates.Text = TranslationBase.TranslateLabelOrMessage(INCLUDE_COVERAGES_AND_RATES)
                    chkExcludeCoveragesAndRates.Checked = True
                ElseIf mIndex = RENEW_INDEX Then
                    'RENEW
                    ControlMgr.SetVisibleControl(Me, Me.ToMultipleDrop, False)
                    ControlMgr.SetVisibleControl(Me, Me.LabelEnterCovDateHeader, True)
                    ControlMgr.SetVisibleControl(Me, Me.LabelStar1, True)
                    ControlMgr.SetVisibleControl(Me, Me.LabelStar2, False)
                    ControlMgr.SetVisibleControl(Me, Me.LabelEffDate, True)
                    ControlMgr.SetVisibleControl(Me, Me.TextboxEffDate, True)
                    ControlMgr.SetVisibleControl(Me, Me.LabelExpDate, False)
                    ControlMgr.SetVisibleControl(Me, Me.TextboxExpDate, False)
                    ControlMgr.SetVisibleControl(Me, Me.ImageButtonEffDate, True)
                    ControlMgr.SetVisibleControl(Me, Me.ImagebuttonExpDate, False)
                    Me.chkExcludeCoveragesAndRates.Visible = False
                Else
                    'COPY
                    ControlMgr.SetVisibleControl(Me, Me.ToMultipleDrop, True)
                    Me.chkExcludeCoveragesAndRates.Text = TranslationBase.TranslateLabelOrMessage(EXCLUDE_COVERAGES_AND_RATES)
                    chkExcludeCoveragesAndRates.Checked = False
                    ControlMgr.SetVisibleControl(Me, Me.LabelEnterCovDateHeader, True)
                    ControlMgr.SetVisibleControl(Me, Me.LabelStar1, True)
                    ControlMgr.SetVisibleControl(Me, Me.LabelStar2, True)
                    ControlMgr.SetVisibleControl(Me, Me.LabelEffDate, True)
                    ControlMgr.SetVisibleControl(Me, Me.TextboxEffDate, True)
                    ControlMgr.SetVisibleControl(Me, Me.LabelExpDate, True)
                    ControlMgr.SetVisibleControl(Me, Me.TextboxExpDate, True)
                    ControlMgr.SetVisibleControl(Me, Me.ImageButtonEffDate, True)
                    ControlMgr.SetVisibleControl(Me, Me.ImagebuttonExpDate, True)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorController)
            End Try
        End Sub

        Public Sub chkExcludeCoveragesAndRates_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkExcludeCoveragesAndRates.CheckedChanged
            'Me.chkExcludeCoveragesAndRates.Attributes.Add("OnCheckedChanged", "enable_disable_div(rCopyDelete.SelectedIndex, chkExcludeCoveragesAndRates.Checked)")

            Try
                Dim mchkIndex As Boolean = chkExcludeCoveragesAndRates.Checked
                If rCopyDelete.SelectedIndex = COPY_INDEX And Not mchkIndex Then
                    ControlMgr.SetVisibleControl(Me, Me.LabelEnterCovDateHeader, True)
                    ControlMgr.SetVisibleControl(Me, Me.LabelStar1, True)
                    ControlMgr.SetVisibleControl(Me, Me.LabelStar2, True)
                    ControlMgr.SetVisibleControl(Me, Me.LabelEffDate, True)
                    ControlMgr.SetVisibleControl(Me, Me.TextboxEffDate, True)
                    ControlMgr.SetVisibleControl(Me, Me.LabelExpDate, True)
                    ControlMgr.SetVisibleControl(Me, Me.TextboxExpDate, True)
                    ControlMgr.SetVisibleControl(Me, Me.ImageButtonEffDate, True)
                    ControlMgr.SetVisibleControl(Me, Me.ImagebuttonExpDate, True)
                Else
                    ControlMgr.SetVisibleControl(Me, Me.LabelEnterCovDateHeader, False)
                    ControlMgr.SetVisibleControl(Me, Me.LabelStar1, False)
                    ControlMgr.SetVisibleControl(Me, Me.LabelStar2, False)
                    ControlMgr.SetVisibleControl(Me, Me.LabelEffDate, False)
                    ControlMgr.SetVisibleControl(Me, Me.TextboxEffDate, False)
                    ControlMgr.SetVisibleControl(Me, Me.LabelExpDate, False)
                    ControlMgr.SetVisibleControl(Me, Me.TextboxExpDate, False)
                    ControlMgr.SetVisibleControl(Me, Me.ImageButtonEffDate, False)
                    ControlMgr.SetVisibleControl(Me, Me.ImagebuttonExpDate, False)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorController)
            End Try
        End Sub
#End Region

#Region " Command button "

        Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                If Not Me.ValidSelection() Then
                    Throw New GUIException(Message.MSG_GUI_INVALID_SELECTION, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_SELECTION)
                End If

                If rCopyDelete.SelectedIndex = COPY_INDEX Then
                    If Not chkExcludeCoveragesAndRates.Checked Then
                        ' The dates must be entered 
                        If Me.TextboxEffDate.Text = "" Or Me.TextboxExpDate.Text = "" Then
                            Throw New GUIException(Message.MSG_GUI_INVALID_EMPTY_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_EMPTY_DATE)
                        End If
                        'check is the effective date is less than the expiration date
                        If (Date.Compare(CType(Me.TextboxExpDate.Text, Date), CType(Me.TextboxEffDate.Text, Date)) < 0) Then
                            Throw New GUIException(Message.MSG_GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_EFFECTIVE_HIGHER_EXPIRATION_DATE)
                        End If
                    End If
                    moFromDealerID = FromMultipleDrop.SelectedGuid
                    moToDealerID = ToMultipleDrop.SelectedGuid
                    If Me.EditingValidForCopy() Then
                        Me.ProcessCopy()
                    End If
                ElseIf rCopyDelete.SelectedIndex = RENEW_INDEX Then
                    moFromDealerID = FromMultipleDrop.SelectedGuid
                    Me.ProcessRenew()
                Else
                    moFromDealerID = FromMultipleDrop.SelectedGuid
                    If Me.EditingValidForDelete() Then
                        Me.ProcessDelete()
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorController)
            End Try
        End Sub

        Private Sub btnCancel_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel_WRITE.Click
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
                Me.HandleErrors(ex, Me.ErrorController)
            End Try
        End Sub

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Me.ReturnToTabHomePage()
        End Sub

#End Region

#Region " Private Methods"
        Private Function ValidSelection() As Boolean
            If rCopyDelete.SelectedIndex = COPY_INDEX Then
                If FromMultipleDrop.SelectedGuid.Equals(Guid.Empty) Or Me.ToMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
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
                    Me.DisplayMessage(Message.MSG_FROM_DEALER_CONTAINS_NO_DEFINITIONS, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT)
                    blnEditingValidForCopy = False
                    Exit Try
                End If

                ' Verify that the to dealer has a valid contract
                If Not ToDealerBO.DealerHasValidContract Then
                    Me.DisplayMessage(Message.MSG_TO_DEALER_MUST_HAVE_A_VALID_CONTRACT, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT)
                    blnEditingValidForCopy = False
                    Exit Try
                End If

                'Req-1016 Begin
                ' Verify that the to dealer has the same RecurringPremiumId seeting on the contract
                If Not FromDealerBO.DealerHasSameRecurringPremiumSetting(ToDealerBO) Then
                    Me.DisplayMessage(Message.MSG_TO_DEALER_MUST_HAVE_SAME_RECURRING_PREMIUM_SETTING, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT)
                    blnEditingValidForCopy = False
                    Exit Try
                End If
                'Req-1016 End

                If Not Me.chkExcludeCoveragesAndRates.Checked Then
                    ' Verify that the entered date for new coverage are within the date of contract of to dealer
                    If Not ToDealerBO.EnteredDateWithinContract(Me.TextboxEffDate.Text, Me.TextboxExpDate.Text) Then
                        Me.DisplayMessage(Message.MSG_ENTERED_DATE_NOT_WITHIN_CONTRACT, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT)
                        blnEditingValidForCopy = False
                        Exit Try
                    End If
                End If

                ' Verify that the to dealer does not have definitions
                If ToDealerBO.GetDealerProductCodesCount > 0 Then
                    Me.DisplayMessage(Message.MSG_TO_DEALER_ALREADY_CONTAINS_DEFINITIONS, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT)
                    blnEditingValidForCopy = False
                    Exit Try
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorController)
            End Try

            Return blnEditingValidForCopy


        End Function

        Private Function EditingValidForDelete() As Boolean
            FromDealerBO = New Dealer(moFromDealerID)

            Dim blnEditingValidForDelete As Boolean = True
            Try
                If Me.chkExcludeCoveragesAndRates.Checked Then
                    ' Verify that the dealer selected has no certificates
                    If FromDealerBO.GetDealerCertificatesCount > 0 Then
                        Me.DisplayMessage(Message.MSG_DEALER_ALREADY_HAS_CERTIFICATES_DEFINITIONS_CANNOT_BE_DELETED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT)
                        blnEditingValidForDelete = False
                        Exit Try
                    End If
                Else
                    ' Verify that the dealer has no coverage definitions
                    If FromDealerBO.GetDealerCoveragesCount > 0 Then
                        Me.DisplayMessage(Message.MSG_DEALER_ALREADY_HAS_DEFINED_COVERAGES_DEFINITIONS_CANNOT_BE_DELETED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT)
                        blnEditingValidForDelete = False
                        Exit Try
                    End If
                End If

                ' Verify that the dealer has definitions to delete
                If FromDealerBO.GetDealerProductCodesCount <= 0 Then
                    Me.DisplayMessage(Message.MSG_FROM_DEALER_CONTAINS_NO_DEFINITIONS, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT)
                    blnEditingValidForDelete = False
                    Exit Try
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorController)
            End Try

            Return blnEditingValidForDelete

        End Function

        Private Sub ProcessRenew()
            Dim effDate As Date
            Dim oContract As Contract

            effDate = CType(Me.TextboxEffDate.Text, Date)
            'verify that contract in effect on effDate is the highest contract
            oContract = Contract.GetMaxExpirationContract(Me.moFromDealerID)
            If effDate < oContract.Effective.Value OrElse effDate > oContract.Expiration.Value Then
                oContract = Nothing
            End If

            If oContract Is Nothing Then
                Me.DisplayMessage(Message.MSG_NO_COVERAGE_AVAILABLE, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            Else
                If Dealer.RenewCoverage(Me.moFromDealerID, oContract.Id, effDate) = 0 Then
                    Me.DisplayMessage(Message.MSG_RENEW_WAS_COMPLETED_SUCCESSFULLY, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                ElseIf Dealer.RenewCoverage(Me.moFromDealerID, oContract.Id, effDate) = -1 Then
                    Me.DisplayMessage(Message.MSG_NO_COVERAGE_AVAILABLE, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Else
                    Me.DisplayMessage(Message.MSG_RENEW_COVERAGE_FAILED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT)
                End If
            End If

        End Sub

        Private Sub ProcessCopy()
            Dim intCopyLevel As Integer
            Dim effDate, expDate As Date
            If Me.chkExcludeCoveragesAndRates.Checked Then
                intCopyLevel = PRODUCTCODE_ITEM_TABLES
                effDate = Nothing
                expDate = Nothing
            Else
                intCopyLevel = PRODUCTCODE_ITEM_COVERAGE_RATE_DED_TABLES
                effDate = CType(Me.TextboxEffDate.Text, Date)
                expDate = CType(Me.TextboxExpDate.Text, Date)
            End If

            If Dealer.CopyDealerDefinitions(Me.moFromDealerID, Me.moToDealerID, intCopyLevel, effDate, expDate) = 0 Then
                Me.DisplayMessage(Message.MSG_COPY_WAS_COMPLETED_SUCCESSFULLY, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            Else
                Me.DisplayMessage(Message.MSG_COPY_FAILED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT)
            End If
        End Sub

        Private Sub ProcessDelete()
            Dim intDeleteLevel As Integer
            If Me.chkExcludeCoveragesAndRates.Checked Then
                intDeleteLevel = PRODUCTCODE_ITEM_COVERAGE_RATE_TABLES
            Else
                intDeleteLevel = PRODUCTCODE_ITEM_TABLES
            End If

            If Dealer.DeleteDealerDefinitions(Me.moFromDealerID, intDeleteLevel) = 0 Then
                Me.DisplayMessage(Message.MSG_DELETE_WAS_COMPLETED_SUCCESSFULLY, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            Else
                Me.DisplayMessage(Message.MSG_DELETE_FAILED, "", Me.MSG_BTN_OK, Me.MSG_TYPE_ALERT)
            End If

        End Sub

#End Region

    End Class

End Namespace

