'Imports Assurant.ElitaPlus.BusinessObjectsNew.BillingDetail
Namespace Certificates
    Partial Public Class BillingHistoryListForm
        Inherits ElitaPlusSearchPage

#Region "Constants"
        Private Const EDIT_COMMAND As String = "SelectAction"
        Private Const PAID_COMMAND As String = "PaidAction"

        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"
        Private Const ACTION_SAVE As String = "ACTION_SAVE"
        Private Const ACTION_NO_EDIT As String = "ACTION_NO_EDIT"
        Private Const ACTION_EDIT As String = "ACTION_EDIT"

        Public Const URL As String = "~/Certificates/BillingHistoryListForm.aspx"
        Public Const PAGETITLE As String = "BILLING_HISTORY"
        Public Const PAGETAB As String = "CERTIFICATES"
        Public Const GRID_COL_BILLING_DETAIL_ID As Integer = 1
        Public Const GRID_COL_INSTALLMENT_NUMB As Integer = 2
        Public Const GRID_COL_BILL_AMT As Integer = 3
        Public Const GRID_COL_CREATED_DATE As Integer = 4
        Public Const GRID_COL_BILLING_STATUS As Integer = 5
        Public Const GRID_COL_REJECT_CODE As Integer = 6
        Public Const GRID_COL_REJECT_DATE As Integer = 7
        Public Const GRID_COL_PAID As Integer = 8

        Public Const GRID_COL_INSTALLMENT_DUE_DATE_VSC As Integer = 4
        Public Const GRID_COL_CREATED_DATE_VSC As Integer = 5
        Public Const GRID_COL_BILLING_STATUS_VSC As Integer = 6
        Public Const GRID_COL_REJECT_CODE_VSC As Integer = 7
        Public Const GRID_COL_RE_ATTEMPT_COUNT_VSC As Integer = 8
        Public Const GRID_COL_REJECT_DATE_VSC As Integer = 9
        Public Const GRID_COL_PAID_VSC As Integer = 10


        Public Const COL_ORDER_BY As String = "created_date1, installment_number"

        Public Const COL_BILLING_STATUS_ID As String = "BILLING_STATUS_ID"
        Public Const COL_REJECT_CODE_ID As String = "REJECT_CODE_ID"
        Public Const COL_REJECT_PAID_ID As String = "PAID_ID"

        Public Const BILLING_STATUS_ACTIVE As String = "A"
        Public Const BILLING_STATUS_REJECT As String = "R"
        Public Const BILLING_STATUS_ONHOLD As String = "H"

#End Region

#Region "Page State"
        ' This class keeps the current state for the page.
        Class MyState
            Public MyBO As BillingDetail
            Public oCertInstallment As CertInstallment
            Public oCert As Certificate
            Public oBilledTotalAmount As Decimal = 0
            Public oDv As DataView = Nothing
            Public searchDV As DataView = Nothing
            Public SelectBillHistId As Guid = Guid.Empty
            Public SortExpression As String = "INSTALLMENT_NUMB"
            'Public PageIndex As Integer = 0
            Public IsEditMode As Boolean
            Public IsAfterSave As Boolean
            Public IsGridVisible As Boolean
            'Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            'Public PageSize As Integer = DEFAULT_PAGE_SIZE
            Public oBillingCount As Int32
            Public DealerType As String
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property
#End Region

#Region "Properties"



        Public ReadOnly Property TheGridObject() As DataGrid
            Get
                If State.DealerType.Equals(Dealer.DEALER_TYPE_DESC) Then
                    Return moVSCGrid
                Else
                    Return Grid
                End If
            End Get
        End Property


        Public ReadOnly Property UserCertificateCtr() As UserControlCertificateInfo
            Get
                If moCertificateInfoController Is Nothing Then
                    moCertificateInfoController = CType(FindControl("moCertificateInfoController"), UserControlCertificateInfo)
                End If
                Return moCertificateInfoController
            End Get
        End Property

        Public ReadOnly Property GetCompanyCode() As String
            Get
                Dim companyBO As Company = New Company(State.oCert.CompanyId)

                Return companyBO.Code
            End Get

        End Property
#End Region

#Region "Page Events"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            ErrControllerMaster.Clear_Hide()
            Try
                If Not IsPostBack Then
                    MenuEnabled = False
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    moCertificateInfoController = UserCertificateCtr
                    moCertificateInfoController.InitController(State.oCert.Id, , GetCompanyCode)
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    'If Me.State.IsGridVisible Then
                    '    If Not (Me.State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                    '        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                    '        TheGridObject.PageSize = Me.State.selectedPageSize
                    '    End If
                    'End If
                    PopulateGrid()
                    SetButtonsState()
                End If
                CheckIfComingFromPayConfirm()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    'Get the id from the parent
                    State.searchDV = BillingDetail.getHistoryList(CType(CallingParameters, Guid))
                    If State.searchDV.Count > 0 Then
                        State.IsGridVisible = True
                    End If
                    State.oCert = New Certificate(CType(CallingParameters, Guid))
                    State.oCertInstallment = New CertInstallment(CType(CallingParameters, Guid), True)
                    'Get Dealer Type
                    Dim objDealer As Dealer = New Dealer(State.oCert.DealerId)
                    State.DealerType = objDealer.DealerTypeDesc

                    State.oDv = State.MyBO.getBillingTotals(State.oCert.Id)
                    'Me.State.oBilledTotalAmount = CType(Me.State.oDv.Table.Rows(0).Item(BillingDetail.BillingLaterRow.COL_NAME_BILLED_AMOUNT), Decimal)

                    State.oBillingCount = CType(State.oDv.Table.Rows(0).Item(BillingDetail.BillingTotals.COL_DETAIL_COUNT), Integer)
                    If State.oBillingCount > 0 Then
                        State.oBilledTotalAmount = CType(State.oDv.Table.Rows(0).Item(BillingDetail.BillingTotals.COL_BILLED_AMOUNT_TOTAL), Decimal)
                        'Dim oBillingCode As String = LookupListNew.GetCodeFromId(LookupListNew.GetBillingStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), Me.State.oCertInstallment.BillingStatusId)
                        'If oBillingCode = BILLING_STATUS_REJECT Then
                        '    Me.State.oDv = Me.State.MyBO.getBillingLaterRow(Me.State.oCert.Id)
                        '    Me.State.oBilledTotalAmount -= CType(Me.State.oDv.Table.Rows(0).Item(BillingDetail.BillingLaterRow.COL_NAME_BILLED_AMOUNT), Decimal)
                        'End If
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster, False)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        Protected Sub CheckIfComingFromPayConfirm()
            Dim confResponse As String = HiddenPayPromptResponse.Value
            HiddenPayPromptResponse.Value = ""
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_YES Then
                Dim oBillingDetail As BillingDetail
                oBillingDetail = New BillingDetail(State.SelectBillHistId)

                Dim selectedBilllingStatusId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_BILLING_STATUS, BILLING_STATUS_ACTIVE)
                Dim selectedRejectCodeId As Guid = Guid.Empty
                Dim InstNum As Integer = CType(oBillingDetail.InstallmentNumber, Integer)
                Dim retVal As Integer

                retVal = BillingDetail.CreateBillingHistForRejOrAct(selectedBilllingStatusId, State.oCert.Id, InstNum, selectedRejectCodeId, State.SelectBillHistId)
                If retVal = 0 Then
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    TheGridObject.SelectedIndex = NO_ITEM_SELECTED_INDEX
                    State.searchDV = Nothing
                    ReturnFromEditing()
                End If
            ElseIf confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_NO Then
                'Do Nothing
            End If
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = ACTION_NONE)
            If State.DealerType.Equals(Dealer.DEALER_TYPE_DESC) Then             'VSC
                ' hide the ESC grid
                moESCBillingInformation.Attributes("style") = "display: none"
                ' Show the VSC grid
                moVSCBillingInformation.Attributes("style") = ""
            Else                                                                    'ESC
                ' hide the VSC grid
                moVSCBillingInformation.Attributes("style") = "display: none"
                ' Show the ESC grid
                moESCBillingInformation.Attributes("style") = ""
            End If

            If State.searchDV Is Nothing Then
                State.searchDV = BillingDetail.getHistoryList(CType(CallingParameters, Guid))
                State.oDv = State.MyBO.getBillingTotals(State.oCert.Id)

                State.oBillingCount = CType(State.oDv.Table.Rows(0).Item(BillingDetail.BillingTotals.COL_DETAIL_COUNT), Integer)
                If State.oBillingCount > 0 Then
                    State.oBilledTotalAmount = CType(State.oDv.Table.Rows(0).Item(BillingDetail.BillingTotals.COL_BILLED_AMOUNT_TOTAL), Decimal)
                End If
            End If
            State.searchDV.Sort = COL_ORDER_BY
            SetSelectedIndexFromGuid()
            TheGridObject.AutoGenerateColumns = False
            SortAndBindGrid()

        End Sub

        Private Sub SetSelectedIndexFromGuid()
            Dim nSelectedRow As Integer

            nSelectedRow = FindSelectedRowIndexFromGuid(State.searchDV, State.SelectBillHistId)
            TheGridObject.SelectedIndex = NO_ITEM_SELECTED_INDEX
            TheGridObject.EditItemIndex = NO_ITEM_SELECTED_INDEX
            If (nSelectedRow > NO_ITEM_SELECTED_INDEX) Then
                TheGridObject.SelectedIndex = nSelectedRow
                If (State.IsEditMode) Then
                    TheGridObject.EditItemIndex = TheGridObject.SelectedIndex
                End If
            End If
            If (State.IsEditMode) Then
                TheGridObject.AllowSorting = False
            Else
                TheGridObject.AllowSorting = True
            End If
        End Sub

        Private Sub SortAndBindGrid()
            'Me.State.PageIndex = Me.TheGridObject.CurrentPageIndex
            TheGridObject.DataSource = State.searchDV
            'HighLightSortColumn(TheGridObject, Me.State.SortExpression)
            TheGridObject.DataBind()

            ControlMgr.SetVisibleControl(Me, TheGridObject, State.IsGridVisible)

            'ControlMgr.SetVisibleControl(Me, trPageSize, Me.TheGridObject.Visible)

            Session("recCount") = State.searchDV.Count

            If State.searchDV.Count > 0 Then

                If TheGridObject.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If TheGridObject.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If
        End Sub
        Private Sub SetButtonsState()

            If (State.IsEditMode) Then
                ControlMgr.SetVisibleControl(Me, btnSave_WRITE, True)
                ControlMgr.SetVisibleControl(Me, btnUndo_WRITE, True)
                MenuEnabled = False
                '    If (Me.cboPageSize.Visible) Then
                'ControlMgr.SetEnableControl(Me, cboPageSize, False)
                'End If
            Else
                ControlMgr.SetVisibleControl(Me, btnSave_WRITE, False)
                ControlMgr.SetVisibleControl(Me, btnUndo_WRITE, False)
                MenuEnabled = True
                ' If (Me.cboPageSize.Visible) Then
                '    ControlMgr.SetEnableControl(Me, cboPageSize, True)
                'End If
            End If
        End Sub

        Private Sub SetFocusOnEditableFieldInGrid(grid As DataGrid, cellPosition As Integer, itemIndex As Integer)
            'Set focus on the specified control on the EditItemIndex row for the grid
            Dim ddl As DropDownList = CType(grid.Items(itemIndex).Cells(cellPosition).FindControl("BillingStatusDropdown"), DropDownList)
            SetFocus(ddl)
        End Sub

#End Region

#Region "Datagrid Related"

        Public Sub EnableDisableRejectCodeDD(sender As Object, e As System.EventArgs)
            Dim oBSCode As String
            Dim ddl As DropDownList
            oBSCode = CType(sender, DropDownList).SelectedItem.Value
            ddl = CType(TheGridObject.Items(TheGridObject.EditItemIndex).Cells(GRID_COL_REJECT_CODE).FindControl("RejectCodesDropdown"), DropDownList)
            If oBSCode = BILLING_STATUS_REJECT Then
                ControlMgr.SetEnableControl(Me, ddl, True)
            Else
                SetSelectedItem(ddl, "")
                ControlMgr.SetEnableControl(Me, ddl, False)
            End If
        End Sub
        Public Sub EnableDisableRejectCodeDD_VSC(sender As Object, e As System.EventArgs)
            Dim oBSCode As String
            Dim ddl As DropDownList
            oBSCode = CType(sender, DropDownList).SelectedItem.Value
            ddl = CType(TheGridObject.Items(TheGridObject.EditItemIndex).Cells(GRID_COL_REJECT_CODE_VSC).FindControl("RejectCodesDropdown"), DropDownList)
            If oBSCode = BILLING_STATUS_REJECT Then
                ControlMgr.SetEnableControl(Me, ddl, True)
            Else
                SetSelectedItem(ddl, "")
                ControlMgr.SetEnableControl(Me, ddl, False)
            End If
        End Sub

        Private Sub Grid_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemCreated
            Dim elemType As ListItemType = e.Item.ItemType

            If elemType = ListItemType.EditItem Then
                AddHandler CType(e.Item.FindControl("BillingStatusDropdown"), DropDownList).SelectedIndexChanged, AddressOf EnableDisableRejectCodeDD
            End If

            If elemType = ListItemType.AlternatingItem Or elemType = ListItemType.Item Then
                Dim objButton As Button
                objButton = CType(e.Item.Cells(GRID_COL_PAID).FindControl("btnPaid"), Button)
                objButton.Style.Add("background-color", "#dee3e7")
                objButton.Style.Add("cursor", "hand")
                objButton.CssClass = "FLATBUTTON"
            End If

        End Sub

        Private Sub moVSCGrid_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moVSCGrid.ItemCreated
            Dim elemType As ListItemType = e.Item.ItemType

            If elemType = ListItemType.EditItem Then
                AddHandler CType(e.Item.FindControl("BillingStatusDropdown"), DropDownList).SelectedIndexChanged, AddressOf EnableDisableRejectCodeDD_VSC
            End If

            If elemType = ListItemType.AlternatingItem Or elemType = ListItemType.Item Then
                Dim objButton As Button
                objButton = CType(e.Item.Cells(GRID_COL_PAID_VSC).FindControl("btnPaid"), Button)
                objButton.Style.Add("background-color", "#dee3e7")
                objButton.Style.Add("cursor", "hand")
                objButton.CssClass = "FLATBUTTON"
            End If

        End Sub

        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Try
                Dim oBSCode As String
                Dim blnIncludeFirstPayment As Boolean
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                Dim oMaxActiveInstNoForCert As String = BillingDetail.GetMaxActiveInstNoForCert(CType(CallingParameters, Guid)).ToString
                Dim oGetAllRejInstNoForCert As DataView = BillingDetail.GetAllRejInstNoForCert(CType(CallingParameters, Guid))
                Dim oGetLatestRejInstNoForCert As String = BillingDetail.GetLatestRejInstNoForCert(CType(CallingParameters, Guid)).ToString
                'Dim ar As ArrayList
                'For Each dt As DataTable In oGetAllRejInstNoForCert.Table(0)
                '    ar.Add(dt)
                'Next
                Dim oContractId As Guid = Contract.GetContractID(CType(CallingParameters, Guid))
                Dim oContract As New Contract(oContractId)
                'DEF-2822 Start
                Dim displayButton As Boolean = True
                If e.Item.DataItem IsNot Nothing Then
                    Dim dataChk As DataRow()

                    Dim allRejectTable As DataTable = New DataTable()
                    allRejectTable = oGetAllRejInstNoForCert.ToTable()
                    If allRejectTable IsNot Nothing And allRejectTable.Rows.Count > 0 Then
                        dataChk = allRejectTable.Select("installment_number=" & dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString())
                        If (dataChk IsNot Nothing And dataChk.Length > 0) Then  ' Data available 
                            displayButton = True
                        Else
                            displayButton = False
                        End If
                    Else
                        displayButton = False
                    End If

                End If
                'DEF-2822 End
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Item.Cells(GRID_COL_BILLING_DETAIL_ID).Text = New Guid(CType(dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_BILLING_DETAIL_ID), Byte())).ToString
                    e.Item.Cells(GRID_COL_INSTALLMENT_NUMB).Text = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString
                    e.Item.Cells(GRID_COL_CREATED_DATE).Text = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_CREATED_DATE).ToString
                    e.Item.Cells(GRID_COL_BILLING_STATUS).Text = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_BILLING_STATUS).ToString
                    e.Item.Cells(GRID_COL_REJECT_CODE).Text = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECT_CODE).ToString
                    Dim img As ImageButton = CType(e.Item.Cells(0).FindControl("btnEdit"), ImageButton)
                    oBSCode = LookupListNew.GetCodeFromDescription(LookupListNew.GetBillingStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_BILLING_STATUS).ToString)
                    If dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString = "1" Then

                        'Req - 1016 - Start
                        Dim emptyGuid As Guid = Guid.Empty
                        Dim singlePremiumId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_PERIOD_RENEW, Codes.PERIOD_RENEW__SINGLE_PREMIUM)
                        'If (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oContract.MonthlyBillingId) = Codes.YESNO_Y) And (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oContract.IncludeFirstPmt) = Codes.YESNO_Y) Then
                        If (((Not oContract.RecurringPremiumId.Equals(emptyGuid)) And (Not oContract.RecurringPremiumId.Equals(singlePremiumId))) And (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oContract.IncludeFirstPmt) = Codes.YESNO_Y)) Then
                            'Req - 1016 - end
                            img.Visible = False
                        Else
                            If oBSCode = BILLING_STATUS_ACTIVE And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECTED_ID).ToString = "") And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString = oMaxActiveInstNoForCert) Then
                                img.Visible = True
                            Else
                                img.Visible = False
                            End If
                        End If
                    Else
                        If LookupListNew.GetCodeFromId(LookupListNew.LK_ALLOW_CC_REJECTIONS, oContract.AllowMultipleRejectionsId) = Codes.ALLOW_CC_REJECTIONS_NO Then
                            If oBSCode = BILLING_STATUS_ACTIVE And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECTED_ID).ToString = "") And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString = oMaxActiveInstNoForCert) Then
                                img.Visible = True
                            Else
                                img.Visible = False
                            End If
                        Else
                            If oBSCode = BILLING_STATUS_ACTIVE And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECTED_ID).ToString = "") Then
                                img.Visible = True
                            Else
                                img.Visible = False
                            End If
                        End If

                    End If

                    PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_BILL_AMT), dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_BILLED_AMOUNT))
                    If dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECT_DATE) IsNot DBNull.Value Then
                        PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_REJECT_DATE), dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECT_DATE))
                    End If

                    If LookupListNew.GetCodeFromId(LookupListNew.LK_ALLOW_CC_REJECTIONS, oContract.AllowMultipleRejectionsId) = Codes.ALLOW_CC_REJECTIONS_NO Then
                        If oBSCode = BILLING_STATUS_REJECT And dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_PAID).ToString = "" And displayButton And Integer.Parse(dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString) < 0 Then
                            Dim btnpd As Button = CType(e.Item.Cells(GRID_COL_PAID).FindControl("btnPaid"), Button)
                            ControlMgr.SetVisibleControl(Me, btnpd, True)
                        Else
                            e.Item.Cells(GRID_COL_PAID).Text = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_PAID).ToString
                        End If
                    Else
                        Dim btnpd As Button = CType(e.Item.Cells(GRID_COL_PAID).FindControl("btnPaid"), Button)
                        Dim lblpd As Label = CType(e.Item.Cells(GRID_COL_PAID).FindControl("lblPaid"), Label)

                        lblpd.Text = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_PAID).ToString
                        ControlMgr.SetVisibleControl(Me, lblpd, True)
                        ControlMgr.SetVisibleControl(Me, btnpd, False)
                        Dim i As Integer
                        For i = 0 To oGetAllRejInstNoForCert.Count - 1

                            If oGetAllRejInstNoForCert(i)(0).ToString = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString AndAlso oGetAllRejInstNoForCert(i)(1).ToString = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_CREATED_DATE1).ToString And displayButton Then
                                ControlMgr.SetVisibleControl(Me, btnpd, True)
                                ControlMgr.SetVisibleControl(Me, lblpd, False)
                            End If
                        Next
                    End If

                ElseIf (itemType = ListItemType.EditItem) Then
                    oBSCode = LookupListNew.GetCodeFromDescription(LookupListNew.GetBillingStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_BILLING_STATUS).ToString)

                    e.Item.Cells(GRID_COL_BILLING_DETAIL_ID).Text = New Guid(CType(dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_BILLING_DETAIL_ID), Byte())).ToString
                    CType(e.Item.Cells(GRID_COL_INSTALLMENT_NUMB).FindControl("txtInstallmentNumb"), TextBox).Text = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString
                    PopulateControlFromBOProperty(CType(e.Item.Cells(GRID_COL_BILL_AMT).FindControl("txtBilledAmount"), TextBox), dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_BILLED_AMOUNT))
                    PopulateControlFromBOProperty(CType(e.Item.Cells(GRID_COL_REJECT_DATE).FindControl("txtRecjectDate"), TextBox), dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECT_DATE))
                    CType(e.Item.Cells(GRID_COL_CREATED_DATE).FindControl("txtDateAdded"), TextBox).Text = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_CREATED_DATE).ToString

                    Dim ddl As DropDownList = CType(e.Item.Cells(GRID_COL_BILLING_STATUS).FindControl("BillingStatusDropdown"), DropDownList)
                    If ddl IsNot Nothing Then
                        Dim dvList As DataView = LookupListNew.GetBillingStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                        Dim i As Integer
                        ddl.Items.Clear()
                        ddl.Items.Add(New ListItem("", ""))
                        If dvList IsNot Nothing Then
                            For i = 0 To dvList.Count - 1
                                If dvList(i)("CODE").ToString <> BILLING_STATUS_ONHOLD Then
                                    ddl.Items.Add(New ListItem(dvList(i)("DESCRIPTION").ToString, dvList(i)("CODE").ToString))
                                End If
                            Next
                        End If
                        ddl.SelectedValue = oBSCode
                    End If

                    ddl = CType(e.Item.Cells(GRID_COL_REJECT_CODE).FindControl("RejectCodesDropdown"), DropDownList)
                    If ddl IsNot Nothing Then
                        Dim dvList As DataView = LookupListNew.GetRejectCodesList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                        Dim i As Integer
                        ddl.Items.Clear()
                        ddl.Items.Add(New ListItem("", ""))
                        If dvList IsNot Nothing Then
                            For i = 0 To dvList.Count - 1
                                ddl.Items.Add(New ListItem(dvList(i)("DESCRIPTION").ToString, dvList(i)("CODE").ToString))
                            Next
                        End If
                        ControlMgr.SetEnableControl(Me, ddl, False)
                    End If

                    Dim img As ImageButton = CType(e.Item.Cells(0).FindControl("btnEdit"), ImageButton)
                    If dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString = "1" Then

                        'Req - 1016 - Start
                        Dim emptyGuid As Guid = Guid.Empty
                        Dim singlePremiumId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_PERIOD_RENEW, Codes.PERIOD_RENEW__SINGLE_PREMIUM)
                        'If (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oContract.MonthlyBillingId) = Codes.YESNO_Y) And (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oContract.IncludeFirstPmt) = Codes.YESNO_Y) Then
                        If (((Not oContract.RecurringPremiumId.Equals(emptyGuid)) And (Not oContract.RecurringPremiumId.Equals(singlePremiumId))) And (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, oContract.IncludeFirstPmt) = Codes.YESNO_Y)) Then
                            'Req - 1016 - end
                            img.Visible = False
                        Else
                            If oBSCode = BILLING_STATUS_ACTIVE And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECTED_ID).ToString = "") And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString = oMaxActiveInstNoForCert) Then
                                img.Visible = True
                            Else
                                img.Visible = False
                            End If
                        End If
                    Else
                        If LookupListNew.GetCodeFromId(LookupListNew.LK_ALLOW_CC_REJECTIONS, oContract.AllowMultipleRejectionsId) = Codes.ALLOW_CC_REJECTIONS_NO Then
                            If oBSCode = BILLING_STATUS_ACTIVE And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECTED_ID).ToString = "") And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString = oMaxActiveInstNoForCert) Then
                                img.Visible = True
                            Else
                                img.Visible = False
                            End If
                        Else
                            If oBSCode = BILLING_STATUS_ACTIVE And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECTED_ID).ToString = "") Then
                                img.Visible = True
                            Else
                                img.Visible = False
                            End If
                        End If

                    End If

                    Dim btnpd As Button = CType(e.Item.Cells(GRID_COL_PAID).FindControl("btnPaid"), Button)
                    ControlMgr.SetVisibleControl(Me, btnpd, False)
                End If

                    If itemType = ListItemType.Footer Then
                        e.Item.Cells(2).Text = BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage("TOTAL_BILLED_AMOUNT")
                        PopulateControlFromBOProperty(e.Item.Cells(3), State.oBilledTotalAmount)
                    End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub moVSCGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moVSCGrid.ItemDataBound
            Try
                Dim oBSCode As String
                Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                Dim oMaxActiveInstNoForCert As String = BillingDetail.GetMaxActiveInstNoForCert(CType(CallingParameters, Guid)).ToString
                Dim oGetAllRejInstNoForCert As DataView = BillingDetail.GetAllRejInstNoForCert(CType(CallingParameters, Guid))
                Dim oGetLatestRejInstNoForCert As String = BillingDetail.GetLatestRejInstNoForCert(CType(CallingParameters, Guid)).ToString

                Dim oContractId As Guid = Contract.GetContractID(CType(CallingParameters, Guid))
                Dim oContract As New Contract(oContractId)

                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Item.Cells(GRID_COL_BILLING_DETAIL_ID).Text = New Guid(CType(dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_BILLING_DETAIL_ID), Byte())).ToString
                    e.Item.Cells(GRID_COL_INSTALLMENT_NUMB).Text = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString
                    e.Item.Cells(GRID_COL_CREATED_DATE_VSC).Text = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_CREATED_DATE).ToString
                    e.Item.Cells(GRID_COL_BILLING_STATUS_VSC).Text = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_BILLING_STATUS).ToString
                    e.Item.Cells(GRID_COL_REJECT_CODE_VSC).Text = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECT_CODE).ToString
                    Dim img As ImageButton = CType(e.Item.Cells(0).FindControl("btnEdit"), ImageButton)
                    'oBSCode = LookupListNew.GetCodeFromDescription(LookupListNew.GetBillingStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_BILLING_STATUS).ToString)
                    'If oBSCode = BILLING_STATUS_ACTIVE And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECTED_ID).ToString = "") And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString = oMaxActiveInstNoForCert) Then
                    'img.Visible = True
                    'Else
                    '    img.Visible = False
                    'End If
                    If LookupListNew.GetCodeFromId(LookupListNew.LK_ALLOW_CC_REJECTIONS, oContract.AllowMultipleRejectionsId) = Codes.ALLOW_CC_REJECTIONS_NO Then
                        If oBSCode = BILLING_STATUS_ACTIVE And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECTED_ID).ToString = "") And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString = oMaxActiveInstNoForCert) Then
                            img.Visible = True
                        Else
                            img.Visible = False
                        End If
                    Else
                        If oBSCode = BILLING_STATUS_ACTIVE And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECTED_ID).ToString = "") Then
                            img.Visible = True
                        Else
                            img.Visible = False
                        End If
                    End If

                    PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_BILL_AMT), dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_BILLED_AMOUNT))
                    PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_INSTALLMENT_DUE_DATE_VSC), dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_DUE_DATE))
                    If dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_RE_ATTEMPT_COUNT) IsNot DBNull.Value Then
                        PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_RE_ATTEMPT_COUNT_VSC), CType(dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_RE_ATTEMPT_COUNT), Integer))
                    End If
                    If dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECT_DATE) IsNot DBNull.Value Then
                        PopulateControlFromBOProperty(e.Item.Cells(GRID_COL_REJECT_DATE_VSC), dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECT_DATE))
                    End If

                    If LookupListNew.GetCodeFromId(LookupListNew.LK_ALLOW_CC_REJECTIONS, oContract.AllowMultipleRejectionsId) = Codes.ALLOW_CC_REJECTIONS_NO Then
                        If oBSCode = BILLING_STATUS_REJECT And dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_PAID).ToString = "" And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString = oGetLatestRejInstNoForCert) Then
                            Dim btnpd As Button = CType(e.Item.Cells(GRID_COL_PAID_VSC).FindControl("btnPaid"), Button)
                            ControlMgr.SetVisibleControl(Me, btnpd, True)
                        Else
                            e.Item.Cells(GRID_COL_PAID_VSC).Text = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_PAID).ToString
                        End If
                    Else
                        Dim btnpd As Button = CType(e.Item.Cells(GRID_COL_PAID_VSC).FindControl("btnPaid"), Button)
                        Dim lblpd As Label = CType(e.Item.Cells(GRID_COL_PAID_VSC).FindControl("lblPaid"), Label)

                        lblpd.Text = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_PAID).ToString
                        ControlMgr.SetVisibleControl(Me, lblpd, True)
                        ControlMgr.SetVisibleControl(Me, btnpd, False)
                        Dim i As Integer
                        For i = 0 To oGetAllRejInstNoForCert.Count - 1

                            If oGetAllRejInstNoForCert(i)(0).ToString = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString AndAlso oGetAllRejInstNoForCert(i)(1).ToString = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_CREATED_DATE1).ToString Then
                                ControlMgr.SetVisibleControl(Me, btnpd, True)
                                ControlMgr.SetVisibleControl(Me, lblpd, False)
                            End If
                        Next
                    End If

                ElseIf (itemType = ListItemType.EditItem) Then
                    oBSCode = LookupListNew.GetCodeFromDescription(LookupListNew.GetBillingStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_BILLING_STATUS).ToString)

                    e.Item.Cells(GRID_COL_BILLING_DETAIL_ID).Text = New Guid(CType(dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_BILLING_DETAIL_ID), Byte())).ToString
                    CType(e.Item.Cells(GRID_COL_INSTALLMENT_NUMB).FindControl("txtInstallmentNumb"), TextBox).Text = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString
                    PopulateControlFromBOProperty(CType(e.Item.Cells(GRID_COL_BILL_AMT).FindControl("txtBilledAmount"), TextBox), dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_BILLED_AMOUNT))
                    PopulateControlFromBOProperty(CType(e.Item.Cells(GRID_COL_INSTALLMENT_DUE_DATE_VSC).FindControl("txtInstallmentDueDate"), TextBox), dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_DUE_DATE))
                    PopulateControlFromBOProperty(CType(e.Item.Cells(GRID_COL_RE_ATTEMPT_COUNT_VSC).FindControl("txtInstallmentDueDate"), TextBox), dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_RE_ATTEMPT_COUNT))
                    PopulateControlFromBOProperty(CType(e.Item.Cells(GRID_COL_REJECT_DATE_VSC).FindControl("txtRecjectDate"), TextBox), dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECT_DATE))
                    CType(e.Item.Cells(GRID_COL_CREATED_DATE_VSC).FindControl("txtDateAdded"), TextBox).Text = dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_CREATED_DATE).ToString

                    Dim ddl As DropDownList = CType(e.Item.Cells(GRID_COL_BILLING_STATUS_VSC).FindControl("BillingStatusDropdown"), DropDownList)
                    If ddl IsNot Nothing Then
                        Dim dvList As DataView = LookupListNew.GetBillingStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                        Dim i As Integer
                        ddl.Items.Clear()
                        ddl.Items.Add(New ListItem("", ""))
                        If dvList IsNot Nothing Then
                            For i = 0 To dvList.Count - 1
                                If dvList(i)("CODE").ToString <> BILLING_STATUS_ONHOLD Then
                                    ddl.Items.Add(New ListItem(dvList(i)("DESCRIPTION").ToString, dvList(i)("CODE").ToString))
                                End If
                            Next
                        End If
                        ddl.SelectedValue = oBSCode
                    End If

                    ddl = CType(e.Item.Cells(GRID_COL_REJECT_CODE_VSC).FindControl("RejectCodesDropdown"), DropDownList)
                    If ddl IsNot Nothing Then
                        Dim dvList As DataView = LookupListNew.GetRejectCodesList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                        Dim i As Integer
                        ddl.Items.Clear()
                        ddl.Items.Add(New ListItem("", ""))
                        If dvList IsNot Nothing Then
                            For i = 0 To dvList.Count - 1
                                ddl.Items.Add(New ListItem(dvList(i)("DESCRIPTION").ToString, dvList(i)("CODE").ToString))
                            Next
                        End If
                        ControlMgr.SetEnableControl(Me, ddl, False)
                    End If

                    Dim img As ImageButton = CType(e.Item.Cells(0).FindControl("btnEdit"), ImageButton)

                    If LookupListNew.GetCodeFromId(LookupListNew.LK_ALLOW_CC_REJECTIONS, oContract.AllowMultipleRejectionsId) = Codes.ALLOW_CC_REJECTIONS_NO Then
                        If oBSCode = BILLING_STATUS_ACTIVE And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECTED_ID).ToString = "") And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_INSTALLMENT_NUMBER).ToString = oMaxActiveInstNoForCert) Then
                            img.Visible = True
                        Else
                            img.Visible = False
                        End If
                    Else
                        If oBSCode = BILLING_STATUS_ACTIVE And (dvRow(BillingDetail.BillingHistorySearchDV.COL_NAME_REJECTED_ID).ToString = "") Then
                            img.Visible = True
                        Else
                            img.Visible = False
                        End If
                    End If

                    Dim btnpd As Button = CType(e.Item.Cells(GRID_COL_PAID_VSC).FindControl("btnPaid"), Button)
                    ControlMgr.SetVisibleControl(Me, btnpd, False)
                End If

                    If itemType = ListItemType.Footer Then
                        e.Item.Cells(2).Text = BusinessObjectsNew.TranslationBase.TranslateLabelOrMessage("TOTAL_BILLED_AMOUNT")
                        PopulateControlFromBOProperty(e.Item.Cells(3), State.oBilledTotalAmount)
                    End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
            Try
                'Me.State.PageIndex = e.NewPageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
        Private Sub moVSCGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moVSCGrid.PageIndexChanged
            Try
                'Me.State.PageIndex = e.NewPageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)

            Try
                Dim index As Integer = e.Item.ItemIndex

                If (e.CommandName = EDIT_COMMAND) Then
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True
                    State.SelectBillHistId = New Guid(TheGridObject.Items(e.Item.ItemIndex).Cells(GRID_COL_BILLING_DETAIL_ID).Text)
                    PopulateGrid(ACTION_EDIT)
                    'Me.State.PageIndex = TheGridObject.CurrentPageIndex

                    'Set focus on the billing sttaus dropdwon for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(TheGridObject, GRID_COL_BILLING_STATUS, index)
                    SetButtonsState()
                ElseIf (e.CommandName = PAID_COMMAND) Then

                    State.SelectBillHistId = New Guid(TheGridObject.Items(e.Item.ItemIndex).Cells(GRID_COL_BILLING_DETAIL_ID).Text)
                    DisplayMessage(Message.MSG_PROMPT_FOR_PAY, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenPayPromptResponse)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub
        Protected Sub ItemCommand_VSC(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)

            Try
                Dim index As Integer = e.Item.ItemIndex

                If (e.CommandName = EDIT_COMMAND) Then
                    'Do the Edit here

                    'Set the IsEditMode flag to TRUE
                    State.IsEditMode = True
                    State.SelectBillHistId = New Guid(TheGridObject.Items(e.Item.ItemIndex).Cells(GRID_COL_BILLING_DETAIL_ID).Text)
                    PopulateGrid(ACTION_EDIT)
                    'Me.State.PageIndex = TheGridObject.CurrentPageIndex

                    'Set focus on the billing sttaus dropdwon for the EditItemIndex row
                    SetFocusOnEditableFieldInGrid(TheGridObject, GRID_COL_BILLING_STATUS_VSC, index)
                    SetButtonsState()
                ElseIf (e.CommandName = PAID_COMMAND) Then

                    State.SelectBillHistId = New Guid(TheGridObject.Items(e.Item.ItemIndex).Cells(GRID_COL_BILLING_DETAIL_ID).Text)
                    DisplayMessage(Message.MSG_PROMPT_FOR_PAY, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenPayPromptResponse)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try

        End Sub

        Protected Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub cboPageSize_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                'TheGridObject.CurrentPageIndex = NewCurrentPageIndex(TheGridObject, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region


#Region "Button Handlers"
        Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                ReturnToCallingPage()
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                Dim billingStatusGridColumn As Integer
                Dim recjectCodeGridColumn As Integer
                If State.DealerType.Equals(Dealer.DEALER_TYPE_DESC) Then
                    'VSC
                    billingStatusGridColumn = GRID_COL_BILLING_STATUS_VSC
                    recjectCodeGridColumn = GRID_COL_REJECT_CODE_VSC
                Else
                    'ESC
                    billingStatusGridColumn = GRID_COL_BILLING_STATUS
                    recjectCodeGridColumn = GRID_COL_REJECT_CODE
                End If

                Dim ddl As DropDownList = CType(TheGridObject.Items(TheGridObject.EditItemIndex).Cells(billingStatusGridColumn).FindControl("BillingStatusDropdown"), DropDownList)
                Dim selectedBilllingCode As String = GetSelectedValue(ddl)
                Dim selectedRejectCode As String
                ddl = CType(TheGridObject.Items(TheGridObject.EditItemIndex).Cells(recjectCodeGridColumn).FindControl("RejectCodesDropdown"), DropDownList)
                selectedRejectCode = GetSelectedValue(ddl)

                If selectedBilllingCode <> BILLING_STATUS_REJECT Then
                    ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SELECT_CORRECT_BILLING_STATUS_ERR)
                    Exit Sub
                Else
                    If selectedRejectCode = "" Then
                        ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_SELECT_REJECT_CODE_ERR)
                        Exit Sub
                    End If
                End If
                Dim selectedBilllingStatusId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_BILLING_STATUS, selectedBilllingCode)
                Dim selectedRejectCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_REJECT_CODES, selectedRejectCode)
                Dim retVal As Integer
                Dim txtInstNo As TextBox = CType(TheGridObject.Items(TheGridObject.EditItemIndex).Cells(GRID_COL_INSTALLMENT_NUMB).FindControl("txtInstallmentNumb"), TextBox)

                retVal = BillingDetail.CreateBillingHistForRejOrAct(selectedBilllingStatusId, State.oCert.Id, CType(txtInstNo.Text, Integer), selectedRejectCodeId, State.SelectBillHistId)
                If retVal = 0 Then
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                    TheGridObject.SelectedIndex = NO_ITEM_SELECTED_INDEX
                    State.searchDV = Nothing
                    ReturnFromEditing()
                End If
            Catch ex As ApplicationException
                HandleErrors(ex, ErrControllerMaster)

            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            TheGridObject.SelectedIndex = NO_ITEM_SELECTED_INDEX
            State.searchDV = Nothing
            ReturnFromEditing()
        End Sub

        Private Sub ReturnFromEditing()

            TheGridObject.EditItemIndex = NO_ITEM_SELECTED_INDEX

            If (TheGridObject.PageCount = 0) Then
                ControlMgr.SetVisibleControl(Me, TheGridObject, False)
            Else
                ControlMgr.SetVisibleControl(Me, TheGridObject, True)
            End If

            State.IsEditMode = False
            PopulateGrid()
            'Me.State.PageIndex = TheGridObject.CurrentPageIndex
            SetButtonsState()
        End Sub

#End Region


    End Class
End Namespace
