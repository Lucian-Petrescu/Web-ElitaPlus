Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common

Partial Public Class DirectBillingForm
    Inherits ElitaPlusSearchPage


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region
    Protected WithEvents ErrController As ErrorController

#Region "Constants"
    Private Const GRID_CONTROL_NAME_BILLING_IDX As String = "moBillingHeaderId"

    Public Const GRID_COL_EDIT_IDX As Integer = 0
    Public Const GRID_COL_BILLING_IDX As Integer = 1
    Public Const GRID_COL_DEALER_CODE_IDX As Integer = 2
    Public Const GRID_COL_DATE_FILE_SENT_IDX As Integer = 3
    Public Const GRID_COL_FILENAME_IDX As Integer = 4
    Public Const GRID_COL_REFERENCE_NUMBER_IDX As Integer = 5
    Public Const GRID_COL_TOTAL_BILLED_AMT_IDX As Integer = 5
    Public Const GRID_VSC_COL_TOTAL_BILLED_AMT_IDX As Integer = 6

    Public Const GRID_TOTAL_COLUMNS As Integer = 7
    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const LABEL_SELECT_DEALERCODE As String = "DEALER"
    Private Const LABEL_SELECT_COMPANY As String = "COMPANY"

    Private Const GRID_NO_SELECTEDITEM_INX As Integer = -1
    Private Const COL_DEALER_CODE As String = "Dealer"
    Private Const COL_DATE_FILE_SENT As String = "Date_File_Sent"
    Private Const COL_FILE_NAME As String = "FileName"
    Private Const COL_TOTAL_BILLED_AMT As String = "total_billed_amt"
    Private Const COL_REFERENCE_NUMBER As String = "reference_number"
    Private Const GRID_CTRL_EDIT_BUTTON_IMAGE_ID As String = "imgbtnEdit"
    Private Const COL_BILLING_HEADER_SOURCE As String = "source"
    Private Const BILLING_HEADER_REJECT_SOURCE As String = "BHR"

#End Region

#Region "Properties"

    Public ReadOnly Property TheDealerControl() As MultipleColumnDDLabelControl
        Get
            If multipleDropControl Is Nothing Then
                multipleDropControl = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
            End If
            Return multipleDropControl
        End Get
    End Property

    Public ReadOnly Property TheCompanyControl() As MultipleColumnDDLabelControl
        Get
            If companyMultipleDropControl Is Nothing Then
                companyMultipleDropControl = CType(FindControl("CompanyMultipleDropControl"), MultipleColumnDDLabelControl)
            End If
            Return companyMultipleDropControl
        End Get
    End Property

#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public BillingHeaderID As Guid = Guid.Empty
        Public PageIndex As Integer = 0
        Public IsGridVisible As Boolean = False
        Private mnPageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As DataView = Nothing
        Public errLabel As String
        Public SearchedComanyID As Guid
        Public SelectedComanyBillingFileByDealer As Boolean = False
        Public SearchedDealerID As Guid
        Public SearchedBeginDate As Date
        Public SearchedEndDate As Date
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        'Public BillingFileByDealer As Boolean = False
        Public BillingFileByDealerForAllUserCompanies As Boolean = False
        Public NoBillingFileByDealerForAllUserCompanies As Boolean = False
        Public MixedBillingFileByDealerForUserCompanies As Boolean = False
        Public companyDV As DataView
        Public alCompanies As ArrayList
        Public SortExpression As String = " dealer, date_file_sent desc"
        Public SortExpression_ByCompany As String = "date_file_sent desc"

        Public Property PageSize() As Integer
            Get
                Return mnPageSize
            End Get
            Set(Value As Integer)
                mnPageSize = Value
            End Set
        End Property
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

#Region "Page_Return"
    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        MenuEnabled = True
        IsReturningFromChild = True
    End Sub
#End Region

#Region "Page_Events"
    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ErrController.Clear_Hide()

        Try
            If Not IsPostBack Then
                PopulateBillingByDealerFlag()
                AddCalendar(btnBeginDate, txtBeginDate)
                AddCalendar(btnEndDate, txtEndDate)
                ControlMgr.SetVisibleControl(Me, trPageSize, False)
                If IsReturningFromChild AndAlso State.IsGridVisible Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    If State.BillingFileByDealerForAllUserCompanies OrElse State.SelectedComanyBillingFileByDealer Then
                        moBillingGrid.PageSize = State.PageSize
                    Else
                        moVSCBillingGrid.PageSize = State.PageSize
                    End If
                    If Not State.SearchedComanyID.Equals(Guid.Empty) Then
                        TheCompanyControl.SelectedGuid = State.SearchedComanyID
                    End If
                    If Not State.SearchedDealerID.Equals(Guid.Empty) Then
                        TheDealerControl.SelectedGuid = State.SearchedDealerID
                        TheDealerControl.ChangeEnabledControlProperty(True)
                    End If
                    PopulateGrid()
                End If
            Else
                If State.errLabel <> "" Then
                    ClearLabelErrSign(CType(FindControl(State.errLabel), Label))
                    State.errLabel = ""
                End If
            End If

            If IsReturningFromChild = True Then
                IsReturningFromChild = False
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
        ShowMissingTranslations(ErrController)
    End Sub

#End Region

#Region "Button Handlers"
    Private Sub mobtnSearch_Click(sender As Object, e As System.EventArgs) Handles mobtnSearch.Click
        Try
            State.PageIndex = 0
            State.IsGridVisible = True
            State.BillingHeaderID = Guid.Empty
            State.searchDV = Nothing
            If State.BillingFileByDealerForAllUserCompanies OrElse State.MixedBillingFileByDealerForUserCompanies Then
                State.SearchedComanyID = TheCompanyControl.SelectedGuid
                If State.alCompanies.Count > 1 AndAlso State.SearchedComanyID.Equals(Guid.Empty) Then
                    ElitaPlusPage.SetLabelError(TheCompanyControl.CaptionLabel)
                    Throw New GUIException(Message.MSG_INVALID_DEALER, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_REQUIRED)
                End If
            End If

            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try

    End Sub

    Private Sub mobtnClear_Click(sender As Object, e As System.EventArgs) Handles mobtnClear.Click
        TheDealerControl.SelectedIndex = 0
        txtBeginDate.Text = ""
        txtEndDate.Text = ""
    End Sub
#End Region

#Region "Grid Handler"
    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim lblControl As Label
        Try
            Dim blnBillingFileByDealer As Boolean
            If e.CommandName = "SelectAction" Then
                If State.BillingFileByDealerForAllUserCompanies OrElse State.SelectedComanyBillingFileByDealer Then
                    lblControl = CType(moBillingGrid.Items(e.Item.ItemIndex).Cells(GRID_COL_BILLING_IDX).FindControl(GRID_CONTROL_NAME_BILLING_IDX), Label)
                    blnBillingFileByDealer = True
                Else
                    lblControl = CType(moVSCBillingGrid.Items(e.Item.ItemIndex).Cells(GRID_COL_BILLING_IDX).FindControl(GRID_CONTROL_NAME_BILLING_IDX), Label)
                    blnBillingFileByDealer = False
                End If

                State.BillingHeaderID = New Guid(lblControl.Text)
                callPage(DirectBillingDetailForm.URL, New DirectBillingDetailForm.Parameters(State.BillingHeaderID, blnBillingFileByDealer, State.SearchedDealerID))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub moBillingGrid_SortCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moBillingGrid.SortCommand
        Try
            If State.SortExpression.StartsWith(e.SortExpression) Then
                If State.SortExpression.EndsWith(" DESC") Then
                    State.SortExpression = e.SortExpression
                Else
                    State.SortExpression &= " DESC"
                End If
            Else
                State.SortExpression = e.SortExpression
            End If
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub moVSCBillingGrid_SortCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles moVSCBillingGrid.SortCommand
        Try
            If State.SortExpression.StartsWith(e.SortExpression) Then
                If State.SortExpression.EndsWith(" DESC") Then
                    State.SortExpression = e.SortExpression
                Else
                    State.SortExpression &= " DESC"
                End If
            Else
                State.SortExpression = e.SortExpression
            End If
            State.PageIndex = 0
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub moBillingGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moBillingGrid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.BillingHeaderID = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Private Sub moVSCBillingGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moVSCBillingGrid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.BillingHeaderID = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            If State.BillingFileByDealerForAllUserCompanies OrElse State.SelectedComanyBillingFileByDealer Then
                State.PageIndex = NewCurrentPageIndex(moBillingGrid, State.searchDV.Count, State.PageSize)
                moBillingGrid.CurrentPageIndex = State.PageIndex
            Else
                State.PageIndex = NewCurrentPageIndex(moVSCBillingGrid, State.searchDV.Count, State.PageSize)
                moVSCBillingGrid.CurrentPageIndex = State.PageIndex
            End If

            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub
    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moVSCBillingGrid.ItemDataBound
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
        Try
            If dvRow IsNot Nothing AndAlso State.searchDV.Count > 0 Then
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                    If dvRow(BillingHeader.BillingSearchDV.COL_SOURCE).ToString = BILLING_HEADER_REJECT_SOURCE Then
                        CType(e.Item.Cells(GRID_COL_EDIT_IDX).FindControl(GRID_CTRL_EDIT_BUTTON_IMAGE_ID), ImageButton).Visible = False
                    End If

                End If
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

#End Region

#Region "Populate"
    Private Sub PopulateDealer(objCompanyIds As ArrayList)
        Try
            Dim oDealerview As DataView = LookupListNew.GetDealerLookupList(objCompanyIds)
            TheDealerControl.SetControl(False,
                                        TheDealerControl.MODES.NEW_MODE,
                                        True,
                                        oDealerview,
                                        " &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALERCODE),
                                        True, True,
                                        ,
                                        "multipleDropControl_moMultipleColumnDrop",
                                        "multipleDropControl_moMultipleColumnDropDesc",
                                        "multipleDropControl_lb_DropDown",
                                        False,
                                        0)

        Catch ex As Exception
            ErrController.AddError(ex.Message, False)
            ErrController.Show()
        End Try
    End Sub

    Private Sub PopulateCompanyDropDown()

        TheCompanyControl.NothingSelected = True

        If State.companyDV Is Nothing Then
            State.companyDV = LookupListNew.GetUserCompaniesLookupList()
        End If

        TheCompanyControl.SetControl(True,
                            TheCompanyControl.MODES.NEW_MODE,
                            True,
                            State.companyDV,
                            " " + TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY),
                            True, True,
                            ,
                            "multipleDropControl_moMultipleColumnDrop",
                            "multipleDropControl_moMultipleColumnDropDesc",
                            "multipleDropControl_lb_DropDown",
                             False,
                            0)

        'If ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 AndAlso Me.State.MyBO.IsNew Then
        '    TheCompanyControl.SetControl(True,
        '                                 TheCompanyControl.MODES.NEW_MODE,
        '                                 False,
        '                                 Me.State.companyDV,
        '                                 "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY),
        '                                 True,
        '                                 False)
        'Else
        '    CompanyMultipleDrop.SetControl(True, CompanyMultipleDrop.MODES.EDIT_MODE, False, Me.State.companyDV, "", True, False)
        'End If

    End Sub

    Private Sub PopulateBillingByDealerFlag()
        Try
            Dim index As Integer
            Dim indexByDelaer As Integer = 0
            Dim indexByCompany As Integer = 0
            State.alCompanies = ElitaPlusIdentity.Current.ActiveUser.Companies
            For index = 0 To ElitaPlusIdentity.Current.ActiveUser.Companies.Count - 1
                Dim objCompany As Company = New Company(CType(State.alCompanies(index), Guid))
                If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objCompany.BillingByDealerId) = Codes.YESNO_N Then
                    'Me.State.BillingFileByDealer = False
                    indexByCompany = indexByCompany + 1
                Else
                    indexByDelaer = indexByDelaer + 1
                End If
            Next

            If indexByCompany = State.alCompanies.Count Then
                State.NoBillingFileByDealerForAllUserCompanies = True
            ElseIf indexByDelaer = State.alCompanies.Count Then
                State.BillingFileByDealerForAllUserCompanies = True
            Else
                State.MixedBillingFileByDealerForUserCompanies = True
            End If


            If State.companyDV Is Nothing Then
                State.companyDV = LookupListNew.GetUserCompaniesLookupList()
            End If

            If State.NoBillingFileByDealerForAllUserCompanies Then 'Billing Header file by company
                moCompanyInformation.Attributes("style") = "display: none"
                TheDealerControl.ChangeEnabledControlProperty(False)
                PopulateDealer(State.alCompanies)
            Else 'Billing Header file by dealer or mix
                If State.alCompanies.Count > 1 Then
                    TheCompanyControl.SetControl(True, TheCompanyControl.MODES.NEW_MODE, True, State.companyDV, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_COMPANY), True, False)
                    TheDealerControl.ChangeEnabledControlProperty(False)
                    PopulateDealer(State.alCompanies)
                    TheCompanyControl.Visible = True
                    TheCompanyControl.ChangeEnabledControlProperty(True)
                ElseIf State.alCompanies.Count = 1 Then
                    moCompanyInformation.Attributes("style") = "display: none"
                    PopulateDealer(State.alCompanies)
                End If

            End If


        Catch ex As Exception
            ErrController.AddError(ex.Message, False)
            ErrController.Show()
        End Try
    End Sub


    Private Sub PopulateGrid(Optional ByVal refreshData As Boolean = False)

        If State.searchDV Is Nothing Then
            GetBillingData()
        End If


        If State.BillingFileByDealerForAllUserCompanies OrElse State.SelectedComanyBillingFileByDealer Then  'Company dropdown is hidden, no company selection
            ' hide the VSC grid
            moVSCBillingInformation.Attributes("style") = "display: none"
            ' Show the ESC grid
            moESCBillingInformation.Attributes("style") = ""
            moBillingGrid.AutoGenerateColumns = False
            moBillingGrid.AllowSorting = True
            State.searchDV.Sort = State.SortExpression
            moBillingGrid.Columns(GRID_COL_DEALER_CODE_IDX).SortExpression = COL_DEALER_CODE
            moBillingGrid.Columns(GRID_COL_DATE_FILE_SENT_IDX).SortExpression = COL_DATE_FILE_SENT
            moBillingGrid.Columns(GRID_COL_FILENAME_IDX).SortExpression = COL_FILE_NAME
            moBillingGrid.Columns(GRID_COL_TOTAL_BILLED_AMT_IDX).SortExpression = COL_TOTAL_BILLED_AMT

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.BillingHeaderID, moBillingGrid, State.PageIndex, (moBillingGrid.EditItemIndex > GRID_NO_SELECTEDITEM_INX))

            State.PageIndex = moBillingGrid.CurrentPageIndex
            moBillingGrid.DataSource = State.searchDV
            HighLightSortColumn(moBillingGrid, State.SortExpression)
            moBillingGrid.DataBind()
            ControlMgr.SetVisibleControl(Me, moBillingGrid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, moBillingGrid.Visible)

            If moBillingGrid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If

        Else
            ' hide the ESC grid
            moESCBillingInformation.Attributes("style") = "display: none"
            ' Show the VSC grid
            moVSCBillingInformation.Attributes("style") = ""

            moVSCBillingGrid.AutoGenerateColumns = False
            moVSCBillingGrid.AllowSorting = True
            State.searchDV.Sort = State.SortExpression_ByCompany
            'Me.moVSCBillingGrid.Columns(Me.GRID_COL_DEALER_CODE_IDX).SortExpression = COL_DEALER_CODE
            moVSCBillingGrid.Columns(GRID_COL_DATE_FILE_SENT_IDX).SortExpression = COL_DATE_FILE_SENT
            moVSCBillingGrid.Columns(GRID_COL_FILENAME_IDX).SortExpression = COL_FILE_NAME
            moVSCBillingGrid.Columns(GRID_COL_REFERENCE_NUMBER_IDX).SortExpression = COL_REFERENCE_NUMBER
            moVSCBillingGrid.Columns(GRID_VSC_COL_TOTAL_BILLED_AMT_IDX).SortExpression = COL_TOTAL_BILLED_AMT

            SetPageAndSelectedIndexFromGuid(State.searchDV, State.BillingHeaderID, moVSCBillingGrid, State.PageIndex, (moVSCBillingGrid.EditItemIndex > GRID_NO_SELECTEDITEM_INX))

            State.PageIndex = moVSCBillingGrid.CurrentPageIndex
            moVSCBillingGrid.DataSource = State.searchDV
            HighLightSortColumn(moVSCBillingGrid, State.SortExpression)
            moVSCBillingGrid.DataBind()
            ControlMgr.SetVisibleControl(Me, moVSCBillingGrid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, moVSCBillingGrid.Visible)

            If moVSCBillingGrid.Visible Then
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If


        End If


    End Sub
    Private Sub OnFromDrop_Changed(fromMultipleDrop As Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl) _
    Handles companyMultipleDropControl.SelectedDropChanged
        Try
            State.SearchedComanyID = TheCompanyControl.SelectedGuid
            Dim index As Integer
            Dim alCompanies As New ArrayList
            alCompanies.Add(State.SearchedComanyID)
            PopulateDealer(alCompanies)

            Dim objCompany As Company = New Company(State.SearchedComanyID)
            If LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objCompany.BillingByDealerId) = Codes.YESNO_N Then
                State.SelectedComanyBillingFileByDealer = False
                TheDealerControl.ChangeEnabledControlProperty(False)
            Else
                State.SelectedComanyBillingFileByDealer = True
                TheDealerControl.ChangeEnabledControlProperty(True)
            End If

        Catch ex As Exception
            HandleErrors(ex, ErrController)
        End Try
    End Sub

    Public Function formatDateTime(objDate As Object) As String
        Dim strRet As String = objDate.ToString
        Try
            Dim dtVal As DateTime = CType(objDate, DateTime)
            strRet = dtVal.ToString("dd-MMM-yyyy") & " " & dtVal.ToLongTimeString()
        Catch ex As Exception
        End Try
        Return strRet
    End Function

    Private Sub GetBillingData()

        Dim DealerID As Guid, strInvNum As String, dtStart As Date, dtEnd As Date
        Dim strBeginDtTemp As String = String.Empty
        Dim strEndDtTemp As String = String.Empty

        'clear stored search criteria
        State.SearchedComanyID = Guid.Empty
        State.SearchedDealerID = Guid.Empty
        State.SearchedBeginDate = Date.MinValue
        State.SearchedEndDate = Date.MinValue

        If State.BillingFileByDealerForAllUserCompanies OrElse State.MixedBillingFileByDealerForUserCompanies Then
            State.SearchedComanyID = TheCompanyControl.SelectedGuid
        End If

        DealerID = TheDealerControl.SelectedGuid

        strBeginDtTemp = txtBeginDate.Text.Trim()
        If strBeginDtTemp <> "" Then
            If Not Date.TryParse(strBeginDtTemp, dtStart) Then
                SetLabelError(moBeginDateLabel)
                State.errLabel = moBeginDateLabel.UniqueID
                Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
            End If
        Else
            dtStart = Date.MinValue
        End If

        strEndDtTemp = txtEndDate.Text.Trim()
        If strEndDtTemp <> "" Then
            If Not Date.TryParse(strEndDtTemp, dtEnd) Then
                SetLabelError(moEndDateLabel)
                State.errLabel = moEndDateLabel.UniqueID
                Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
            End If
        Else
            dtEnd = Date.MinValue
        End If
        If strBeginDtTemp = "" AndAlso strEndDtTemp <> "" Then
            SetLabelError(moBeginDateLabel)
            State.errLabel = moBeginDateLabel.UniqueID
            Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
        End If

        If strBeginDtTemp <> "" AndAlso strEndDtTemp = "" Then
            SetLabelError(moEndDateLabel)
            State.errLabel = moEndDateLabel.UniqueID
            Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
        End If

        If dtEnd < dtStart Then
            SetLabelError(moEndDateLabel)
            SetLabelError(moBeginDateLabel)
            Throw New GUIException(Message.MSG_BEGIN_END_DATE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
        End If

        State.SearchedDealerID = DealerID
        State.SearchedBeginDate = dtStart
        State.SearchedEndDate = dtEnd

        If State.BillingFileByDealerForAllUserCompanies Then
            State.searchDV = BillingHeader.getList(State.alCompanies, DealerID, dtStart, dtEnd)
        ElseIf State.NoBillingFileByDealerForAllUserCompanies Then
            State.searchDV = BillingHeader.getListByCompany(State.alCompanies, dtStart, dtEnd)
        Else ' Mix
            Dim alCompanies As New ArrayList
            alCompanies.Add(State.SearchedComanyID)
            If State.SelectedComanyBillingFileByDealer Then
                State.searchDV = BillingHeader.getList(alCompanies, DealerID, dtStart, dtEnd)
            Else
                State.searchDV = BillingHeader.getListByCompany(alCompanies, dtStart, dtEnd)
            End If
        End If


    End Sub
#End Region

End Class