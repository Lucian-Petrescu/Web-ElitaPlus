﻿Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.DALObjects
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Public Class DailyOutboundFileForm
    Inherits ElitaPlusSearchPage
#Region "Constants"
    Public Const GRID_COL_chk_select_IDX As Integer = 0
    Public Const GRID_COL_CERTIFICATE_NUMBER_IDX As Integer = 1
    Public Const GRID_COL_CREATED_DATE_IDX As Integer = 2
    Public Const GRID_COL_NEW_ENROLLMENT_IDX As Integer = 3
    Public Const GRID_COL_CANCELLATIONS_IDX As Integer = 4
    Public Const GRID_COL_BILLING_IDX As Integer = 5
    Public Const GRID_COL_FILE_DETAIL_ID_IDX As Integer = 6
    Public Const Y_YES As String = "Y"
    Public Const N_No As String = "N"
    Public Const YESNO As String = "YESNO"
    'Protected WithEvents moUserCompanyMultipleDrop As Common.MultipleColumnDDLabelControl
    Public Const LABEL_SELECT_DEALER As String = "SELECT_DEALER"
    Public Const LABEL_SELECT_COMPANY As String = "SELECT_COMPANY"
    Public Const NO_Record_Found As Integer = 0


#End Region

    Protected WithEvents moCompanyMultipleDrop As MultipleColumnDDLabelControl

    Public ReadOnly Property CompanyMultipleDrop() As MultipleColumnDDLabelControl
        Get
            If moCompanyMultipleDrop Is Nothing Then
                moCompanyMultipleDrop = CType(FindControl("drpCompany"), MultipleColumnDDLabelControl)
            End If
            Return moCompanyMultipleDrop
        End Get
    End Property
#Region "Page State"
    Private IsReturningFromChild As Boolean = False
    Class MyState
        Public PageIndex As Integer = 0
        Public PageSize As Integer = 30
        Public CompanyCode As String
        Public DealerCode As String
        Public CertNumber As String
        Public begindate As String
        Public enddate As String
        Public SelectionNewEnrollmnt As String
        Public selectiononcert As String = String.Empty
        Public selectionCancel As String
        Public selectionbilling As String
        Public Called_From As String = "GUI"
        Public Processed_date As String = ""
        Public IsGridVisible As Boolean = False
        Public HasDataChanged As Boolean = False
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SearchClicked As Boolean = False
        Public viewclicked As Boolean = False
        Public ClearSearch As Boolean = False
        Public dvYesNo As DataView
        Public searchDV As DailyObdFileDetailTemp.ObdFileDetTempSearchDV = Nothing
        Public ViewDV As DailyOutboundFileDetail.DailyOutboundFileDetailSearchDV = Nothing
        Public selectedFileDetailtempId As Guid = Guid.Empty
        Public SortExpression As String = String.Empty
        Public dealerId As Guid
        Public dealerName As String = String.Empty
        Public companyId As System.Guid
        Public CompanyName As String = String.Empty
        Public SelectAllClicked As Boolean = False
        Public IsRecordDeleted As Boolean = False



        Sub New()

        End Sub

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
#Region "Page event handlers"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            MasterPage.MessageController.Clear()
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("Interfaces")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Daily OutBound File")
            UpdateBreadCrum()

            If Not IsPostBack Then
                AddCalendar(Imagebtnbegindate, txtbegindate)
                AddCalendar(ImageBbtnEndDate, txtEnddate)
                chknewenrolment.Checked = True
                chkcancellations.Checked = True
                chkbilling.Checked = True
                'lblPageSize.Visible = False
                'cboPageSize.Visible = False
                'colonSepertor.Visible = False
                trPageSize.Visible = False
                btnSaveRecords.Visible = False
                btnDelectRecords.Visible = False
                PopulateSearchFieldsFromState()
                Session("Checked_Items") = Nothing
                HiddenIsCheckBoxEdited.Value = "0"
                checkboxParentHidden.Value = ""
                If Not IsReturningFromChild Then
                    ControlMgr.SetVisibleControl(Me, trPageSize, False)
                    If ElitaPlusIdentity.Current.ActiveUser.IsDealer Then
                        State.dealerId = ElitaPlusIdentity.Current.ActiveUser.ScDealerId
                    End If
                End If

                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = DEFAULT_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        Grid.PageSize = State.selectedPageSize
                    End If
                    'Me.PopulateGrid()
                End If
                SetGridItemStyleColor(Grid)

            End If

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)


    End Sub
    Private Sub UpdateBreadCrum()

        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage("Daily OutBound File")
        End If

    End Sub
#End Region

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            If HiddenIsCheckBoxEdited.Value = "1" Then
                RememberOldValues()
            End If

            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
            ' PopulateGrid()
            If State.SearchClicked = True Then
                ShowGrid(State.searchDV)
            Else
                ShowGrid(State.ViewDV)
            End If

            '''''
            'Dim HeaderChkBx As CheckBox = CType(Grid.HeaderRow.FindControl("HeaderChkBx"), CheckBox)
            Dim HeaderChkBx As CheckBox = CType(Grid.HeaderRow.FindControl("HeaderChkBx"), CheckBox)
            For Each gr As GridViewRow In Grid.Rows
                Dim chkrow As CheckBox = CType(gr.FindControl("SelectChkBx"), CheckBox)
                If State.SelectAllClicked = True Then
                    'If checkboxParentHidden.Value = "selectAllChecked" Then
                    chkrow.Checked = True
                    'HeaderChkBx.Checked = True
                Else
                    chkrow.Checked = False
                    'HeaderChkBx.Checked = False
                End If

            Next

            If State.SelectAllClicked = True Then
                'If checkboxParentHidden.Value = "selectAllChecked" Then
                HeaderChkBx.Checked = True
            Else
                HeaderChkBx.Checked = False
            End If

            If HiddenIsCheckBoxEdited.Value = "1" Then
                RePopulateValues()
            End If


        Catch ex As Exception

        End Try
    End Sub

    Protected Sub RememberOldValues()
        Try
            Dim selectedList As New ArrayList
            Dim index As String = String.Empty ' Guid = Guid.Empty
            If HiddenIsCheckBoxEdited.Value = "1" Then
                For Each gv As GridViewRow In Grid.Rows
                    'index = GuidControl.ByteArrayToGuid(Grid.DataKeys(gv.RowIndex).Value)
                    index = gv.Cells(1).Text.ToString() + gv.Cells(3).Text.ToString() + gv.Cells(4).Text.ToString() + gv.Cells(5).Text.ToString() + GuidControl.ByteArrayToGuid(Grid.DataKeys(gv.RowIndex).Values(7)).ToString()
                    Dim result As Boolean = CType(gv.FindControl("SelectChkBx"), CheckBox).Checked

                    If (Session("Checked_Items") IsNot Nothing) Then
                        selectedList = CType(Session("Checked_Items"), ArrayList)
                    End If
                    'For view
                    If State.viewclicked Then
                        If State.SelectAllClicked Then
                            If result = False Then
                                If Not selectedList.Contains(index) Then
                                    selectedList.Add(index)
                                End If
                            Else
                                If selectedList.Contains(index) Then
                                    selectedList.Remove(index)
                                End If
                            End If

                        Else
                            If result = True Then
                                If Not selectedList.Contains(index) Then
                                    selectedList.Add(index)
                                End If
                            Else
                                If selectedList.Contains(index) Then
                                    selectedList.Remove(index)
                                End If
                            End If
                        End If
                        If (selectedList IsNot Nothing) Then
                            Session("Checked_Items") = selectedList
                        End If
                    Else
                        If State.SelectAllClicked Then
                            If result = False Then
                                If Not selectedList.Contains(index) Then
                                    selectedList.Add(index)
                                End If
                            Else
                                If selectedList.Contains(index) Then
                                    selectedList.Remove(index)
                                End If
                            End If
                        Else
                            If result = True Then
                                If Not selectedList.Contains(index) Then
                                    selectedList.Add(index)
                                End If
                            Else
                                If selectedList.Contains(index) Then
                                    selectedList.Remove(index)
                                End If
                            End If
                        End If
                    End If

                Next
                If State.SearchClicked Then
                    If selectedList.Count > 0 Then
                        Dim strSelectedList() As String = CType(selectedList.ToArray(GetType(String)), String())
                        Dim strJoinedString As String

                        strJoinedString = String.Join(",", strSelectedList)
                        Session("Selcted_Items") = strJoinedString

                        If (selectedList IsNot Nothing) Then
                            Session("Checked_Items") = selectedList
                        End If
                    End If
                End If

                'If (Not selectedList Is Nothing) Then
                '    Session("Checked_Items") = selectedList
                'End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub RePopulateValues()
        Try
            'If checkboxParentHidden.Value = "" Then
            If HiddenIsCheckBoxEdited.Value = "1" Then
                Dim selectedList As New ArrayList
                selectedList = CType(Session("Checked_Items"), ArrayList)
                If (selectedList IsNot Nothing And selectedList.Count > 0) Then
                    For Each gv As GridViewRow In Grid.Rows
                        Dim index As String = String.Empty ' Guid = GuidControl.ByteArrayToGuid(Grid.DataKeys(gv.RowIndex).Value)
                        index = gv.Cells(1).Text.ToString() + gv.Cells(3).Text.ToString() + gv.Cells(4).Text.ToString() + gv.Cells(5).Text.ToString() + GuidControl.ByteArrayToGuid(Grid.DataKeys(gv.RowIndex).Values(7)).ToString()
                        Dim selectChkBx As CheckBox = CType(gv.FindControl("SelectChkBx"), CheckBox)
                        If State.viewclicked = True Then
                            If (selectedList.Contains(index)) Then
                                If State.SelectAllClicked Then
                                    selectChkBx.Checked = False
                                Else
                                    selectChkBx.Checked = True
                                End If
                            Else
                                If State.SelectAllClicked Then
                                    selectChkBx.Checked = True
                                    ' selectedList.Remove(index)
                                Else
                                    selectChkBx.Checked = False
                                End If
                            End If
                        Else
                            If (selectedList.Contains(index)) Then
                                If State.SelectAllClicked Then
                                    selectChkBx.Checked = False
                                Else
                                    selectChkBx.Checked = True
                                End If
                            End If
                        End If
                    Next
                End If
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Grid.PageIndex = State.PageIndex
            ' Me.PopulateGrid()
            If State.SearchClicked = True Then
                ShowGrid(State.searchDV)
            Else
                ShowGrid(State.ViewDV)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub Grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim chkSelected As CheckBox = CType(e.Row.FindControl("Selectchkbx"), CheckBox)
                chkSelected.Checked = False

                If State.viewclicked = True Then
                    chkSelected.Checked = True
                End If

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            'Me.PopulateGrid()

            If State.SearchClicked = True Then
                ShowGrid(State.searchDV)
            Else
                ShowGrid(State.ViewDV)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
    Protected Sub chkbxHeader_checkChanged(sender As Object, e As EventArgs)
        Try

            Dim chk As CheckBox = TryCast(sender, CheckBox)
            Session("Checked_Items") = Nothing
            Session("Selcted_Items") = Nothing
            If chk.Checked Then
                State.SelectAllClicked = True
            Else
                State.SelectAllClicked = False
            End If



        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub chkbxSelect_checkChanged(sender As Object, e As EventArgs) 'Handles Grid.SelectedIndexChanged
        Try
            Dim chk As CheckBox = CType(sender, CheckBox)
            Dim test As Boolean = chk.Checked
        Catch ex As Exception

        End Try
    End Sub
#Region "Controlling Logic"
    Public Sub ClearSearch()
        Try
            txtCertificatenumb.Text = String.Empty
            txtbegindate.Text = String.Empty
            txtEnddate.Text = String.Empty
            drpCompany.SelectedIndex = 0
            txtComDescription.Text = String.Empty
            If Not ElitaPlusIdentity.Current.ActiveUser.IsDealer Then
                drpDealer.SelectedIndex = 0
            End If
            txtDeaDescription.Text = String.Empty


        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Public Sub PopulateSearchFieldsFromState()

        Try
            txtbegindate.Text = State.begindate
            txtEnddate.Text = State.enddate
            txtCertificatenumb.Text = State.CertNumber
            drpCompany.SelectedValue = State.CompanyCode
            drpDealer.SelectedValue = State.DealerCode
            chkbilling.Checked = True
            chkcancellations.Checked = True
            chknewenrolment.Checked = True
            PopulateDealerDropdown(drpDealer)
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()

            PopulateCompanyDropdown(drpCompany)
            'Me.State.dealerId = Me.GetSelectedItem(Me.drpDealer)
            'Me.State.dealerName = LookupListNew.GetCodeFromId("DEALERS", Me.State.dealerId)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try


    End Sub
    Public Function PopulateStateFromSearchFields() As Boolean
        'Dim dblAmount As Double
        Try
            State.begindate = txtbegindate.Text
            State.enddate = txtEnddate.Text
            State.CertNumber = txtCertificatenumb.Text
            'Me.State.companyId = CompanyMultipleDrop.SelectedGuid
            'Me.State.CompanyCode = LookupListNew.GetCompanyCodeFromDescription(Me.State.companyId)
            State.CompanyCode = drpCompany.SelectedItem.Text
            'Me.State.DealerCode = Me.drpDealer.SelectedIndex.ToString()
            'If Me.State.dealerId <> Guid.Empty And drpDealer.Items.Count > 0 Then Me.SetSelectedItem(drpDealer, Me.State.dealerId)
            State.DealerCode = drpDealer.SelectedItem.Text

            If chknewenrolment.Checked = True Then
                State.SelectionNewEnrollmnt = Y_YES
            Else
                State.SelectionNewEnrollmnt = N_No
            End If

            If chkcancellations.Checked = True Then
                State.selectionCancel = Y_YES
            Else
                State.selectionCancel = N_No
            End If

            If chkbilling.Checked = True Then
                State.selectionbilling = Y_YES
            Else
                State.selectionbilling = N_No
            End If

            If txtCertificatenumb.Text = "" Then
                State.selectiononcert = N_No
            Else
                State.selectiononcert = Y_YES
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Function

    Public Sub PopulateGrid()

        Try
            PopulateStateFromSearchFields()
            Dim sortBy As String = String.Empty
            Dim processeddatestr As String = String.Empty
            Dim processeddate As DateTime = New DateTime

            If State.Processed_date.Length > 0 Then

                Try
                    processeddate = DateTime.Parse(State.Processed_date.ToString(),
                                                    System.Threading.Thread.CurrentThread.CurrentCulture,
                                                    System.Globalization.DateTimeStyles.NoCurrentDateDefault)
                Catch ex As Exception
                End Try

                processeddatestr = processeddate.ToString(DALObjects.DALBase.DOTNET_QUERY_DATEFORMAT)
            End If


            Dim Begindatestr As String = String.Empty
            Dim begindate As DateTime = New DateTime
            If State.begindate.Length > 0 Then

                Try
                    begindate = DateTime.Parse(State.begindate.ToString(),
                                                    System.Threading.Thread.CurrentThread.CurrentCulture,
                                                    System.Globalization.DateTimeStyles.NoCurrentDateDefault)
                Catch ex As Exception
                    SetLabelError(lblbegindate)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                End Try

                Begindatestr = begindate.ToString(DALObjects.DALBase.DOTNET_QUERY_DATEFORMAT)
            End If

            Dim Enddatestr As String = String.Empty
            Dim enddate As DateTime = New DateTime

            If State.enddate.Length > 0 Then

                Try
                    enddate = DateTime.Parse(State.enddate.ToString(),
                                                    System.Threading.Thread.CurrentThread.CurrentCulture,
                                                    System.Globalization.DateTimeStyles.NoCurrentDateDefault)
                Catch ex As Exception
                    SetLabelError(lblEnddate)
                    Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
                End Try

                Enddatestr = enddate.ToString(DALObjects.DALBase.DOTNET_QUERY_DATEFORMAT)
            End If

            DailyObdFileDetailTemp.getDetailRecordsList(State.CompanyCode,
                                                              State.DealerCode,
                                                              State.CertNumber,
                                                              State.SelectionNewEnrollmnt,
                                                               State.selectionCancel,
                                                               State.selectionbilling,
                                                               begindate,
                                                               enddate,
                                                               State.Called_From,
                                                               processeddate, State.selectiononcert)


            If (State.searchDV Is Nothing) Then
                'If Me.State.SearchClicked = True Then
                'Me.State.IsGridVisible = True
                'trPageSize.Visible = True
                State.searchDV = DailyObdFileDetailTemp.getList(State.begindate, State.enddate,
                                                                     State.CertNumber,
                                                                State.SelectionNewEnrollmnt,
                                                                State.selectionCancel,
                                                                State.selectionbilling)

            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateDealerDropdown(dealerDropDownList As DropDownList)
        Try
            'Dim dv As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code")
            'If ElitaPlusIdentity.Current.ActiveUser.IsDealer Then
            '    dv.RowFilter = "code = '" & LookupListNew.GetCodeFromId("DEALERS", ElitaPlusIdentity.Current.ActiveUser.ScDealerId) & "'"
            'End If
            'Me.BindListControlToDataView(dealerDropDownList, dv, , , True)

            Dim DealerList As New Collections.Generic.List(Of DataElements.ListItem)
            For Each CompanyId As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                Dim Dealers As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany",
                                                                    context:=New ListContext() With
                                                                    {
                                                                      .CompanyId = CompanyId
                                                                    })
                If Dealers.Count > 0 Then
                    If DealerList IsNot Nothing Then
                        DealerList.AddRange(Dealers)
                    Else
                        DealerList = Dealers.Clone()
                    End If
                End If
            Next

            If ElitaPlusIdentity.Current.ActiveUser.IsDealer Then
                Dim FilteredDealerList As DataElements.ListItem() = (From Dealer In DealerList
                                                                     Where ElitaPlusIdentity.Current.ActiveUser.ScDealerId = Dealer.ListItemId
                                                                     Select Dealer).ToArray()
                dealerDropDownList.Populate(FilteredDealerList.ToArray(),
                                            New PopulateOptions() With
                                            {
                                                .AddBlankItem = True,
                                                .TextFunc = AddressOf .GetCode,
                                                .ValueFunc = AddressOf .GetDescription
                                            })
            Else
                dealerDropDownList.Populate(DealerList.ToArray(),
                                            New PopulateOptions() With
                                            {
                                                .AddBlankItem = True,
                                                .TextFunc = AddressOf .GetCode,
                                                .ValueFunc = AddressOf .GetDescription
                                            })
            End If

            'dealerDropDownList.DataSource = dv
            'dealerDropDownList.DataTextField = "CODE"
            'dealerDropDownList.DataValueField = "DESCRIPTION"
            'dealerDropDownList.DataBind()
            'dealerDropDownList.Items.Insert(0, New ListItem("  ", " "))

            If State.dealerId <> Guid.Empty Then
                SetSelectedItem(dealerDropDownList, State.dealerId)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub PopulateCompanyDropdown(companyDropDownList As DropDownList)
        Try
            'Me.BindListControlToDataView(Me.drpCompany, LookupListNew.GetCompanyLookupList(), "CODE", "Description", True)
            Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
            companyDropDownList.DataSource = LookupListNew.GetCompanyLookupList(oCompanyId)
            companyDropDownList.DataTextField = "CODE"
            companyDropDownList.DataValueField = "DESCRIPTION"
            companyDropDownList.DataBind()
            companyDropDownList.Items.Insert(0, New ListItem("  ", " "))

            If State.companyId <> Guid.Empty Then
                SetSelectedItem(companyDropDownList, oCompanyId)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Button Event Handlers"
    Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        Try
            ClearSearch()
            'Me.State.IsGridVisible = False
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try
            HiddenIsCheckBoxEdited.Value = "0"
            Session("Checked_Items") = Nothing
            Session("Selcted_Items") = Nothing
            If (txtComDescription.Text = "" And txtDeaDescription.Text = "" And txtCertificatenumb.Text = "" And txtbegindate.Text = "" And txtEnddate.Text = "") Then

            ElseIf (txtComDescription.Text = "") Then
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_COMPANY_REQUIRED)

            ElseIf (txtDeaDescription.Text = "") Then
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEALER_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DEALER_REQUIRED)

            ElseIf (txtbegindate.Text = "" And txtEnddate.Text = "") Then
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_VALUE_IS_REQUIRED_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_VALUE_IS_REQUIRED_ERR)

            ElseIf (txtbegindate.Text = "") Then
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_DATE_REQUIRED_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_DATE_REQUIRED_ERR)
            ElseIf (txtEnddate.Text = "") Then
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_END_DATE_REQUIRED_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_END_DATE_REQUIRED_ERR)
                'DEF 3256
            ElseIf (DateTime.Parse(txtEnddate.Text) > Date.Now.AddDays(-1)) Then
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_END_DATE_MUST_NOT_BE_HIGHER_THAN_YESTERDAY_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_END_DATE_MUST_NOT_BE_HIGHER_THAN_YESTERDAY_ERR)

            ElseIf (DateTime.Parse(txtbegindate.Text) > DateTime.Parse(txtEnddate.Text)) Then
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR, Assurant.ElitaPlus.Common.ErrorCodes.GUI_BEGIN_END_DATE_ERR)
            End If
            State.SearchClicked = True
            State.viewclicked = False
            btnDelectRecords.Visible = False
            btnSaveRecords.Visible = False
            State.PageIndex = 0
            State.selectedFileDetailtempId = Guid.Empty
            State.IsGridVisible = False
            State.searchDV = Nothing
            checkboxParentHidden.Value = "selectAllNotChecked"
            State.SelectAllClicked = False
            PopulateGrid()
            ShowGrid(State.searchDV)
            btnDelectRecords.Visible = False
            btnSaveRecords.Visible = True
            ' End If
            'End If



        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Protected Sub btnView_Click(sender As Object, e As EventArgs) Handles btnView.Click
        Try
            Session("Checked_Items") = Nothing
            Session("Selcted_Items") = Nothing
            State.viewclicked = True
            State.SearchClicked = False
            btnSaveRecords.Visible = False
            btnDelectRecords.Visible = True
            State.ViewDV = Nothing
            State.IsGridVisible = False
            State.PageIndex = 0
            'Me.PopulateGrid()

            If State.viewclicked = True Then
                State.IsGridVisible = True
                State.ViewDV = DailyOutboundFileDetail.getviewList()
            End If

            btnDelectRecords.Visible = True
            btnSaveRecords.Visible = False
            If State.ViewDV.Table.Rows.Count > 0 Then
                checkboxParentHidden.Value = "selectAllChecked"
            Else
                checkboxParentHidden.Value = "selectAllNotChecked"
            End If
            ShowGrid(State.ViewDV)
            State.SelectAllClicked = True
            'checkboxParentHidden.Value = "selectAllChecked"
            HiddenIsCheckBoxEdited.Value = "0"

            If State.ViewDV.Table.Rows.Count > 0 Then
                '''check the header checkbox in the grid view
                Dim HeaderChkBx As New CheckBox
                HeaderChkBx = CType(Grid.HeaderRow.FindControl("HeaderChkBx"), CheckBox)
                If checkboxParentHidden.Value = "selectAllChecked" Then
                    HeaderChkBx.Checked = True
                Else
                    HeaderChkBx.Checked = False
                End If
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)

        End Try
    End Sub

    Private Sub ShowGrid(dv As DataView)
        Try
            Dim sortBy As String = String.Empty
            If dv.Count > 0 Then
                State.IsGridVisible = True
                Grid.AutoGenerateColumns = False
                SetPageAndSelectedIndexFromGuid(dv, State.selectedFileDetailtempId, Grid, State.PageIndex)

                Grid.DataSource = dv
                Grid.AllowSorting = True

                If (Not State.SortExpression.Equals(String.Empty)) Then
                    dv.Sort = State.SortExpression
                Else
                    State.SortExpression = sortBy
                End If
                HighLightSortColumn(Grid, State.SortExpression, IsNewUI)

                Grid.DataBind()
                Session("recCount") = dv.Count
                trPageSize.Visible = True

                If State.viewclicked = True Then
                    'Dim HeaderChkBx As CheckBox = CType(Grid.HeaderRow.FindControl("HeaderChkBx"), CheckBox)
                    'HeaderChkBx.Checked = True
                    If checkboxParentHidden.Value = "selectAllChecked" Then
                        State.SelectAllClicked = True
                    Else
                        State.SelectAllClicked = False
                    End If
                End If

                'Me.ValidSearchResultCountNew(dv.Count, True)
            Else
                State.IsGridVisible = False
                Grid.DataSource = Nothing
                Grid.DataBind()
                trPageSize.Visible = False
                btnSaveRecords.Visible = False
                btnDelectRecords.Visible = False

                'If Me.State.SelectAllClicked = False Then
                If checkboxParentHidden.Value = "selectAllNotChecked" Or checkboxParentHidden.Value = "" Then
                    If State.IsRecordDeleted = False Then
                        HandleGridMessages(NO_Record_Found, True)
                    End If
                    State.IsRecordDeleted = False
                End If
            End If

            lblRecordCount.Text = dv.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

        Catch ex As Exception

        End Try
    End Sub
#End Region



    Private Sub btnSaveRecords_Click(sender As Object, e As System.EventArgs) Handles btnSaveRecords.Click
        Try
            Dim company_id As Guid
            Dim dealer_id As Guid
            Dim cert_id As Guid
            Dim billing_detail_id As Guid
            Dim certNo As String = String.Empty
            Dim createdDate As Date
            Dim certcreatedDate As Date
            Dim recordType As String
            Dim createdBy As String
            Dim enrollments As String
            Dim cancellations As String
            Dim billing As String
            Dim DailyObdFileDetailTempId As Guid
            Dim dailyOutboundFileDetailDAL As New DailyOutboundFileDetailDAL
            'Save
            Dim selectedList As String
            If (Session("Selcted_Items") IsNot Nothing) Then
                selectedList = CType(Session("Selcted_Items"), String)
            End If
            Dim savedRecCount As Integer = 0
            If selectedList IsNot Nothing Then

                'If Grid.Rows.Count > 0 Then
                'If Me.State.SelectAllClicked = False Then
                'If checkboxParentHidden.Value = "selectAllChecked" Then
                Dim tempRow() As DataRow
                For i As Integer = 0 To State.searchDV.Table.Rows.Count - 1 'selectedList.Count - 1
                    'tempRow = Me.State.ViewDV.Table.Select("Cert_Number='" & selectedList(i).ToString() & "'")
                    'tempRow = Me.State.ViewDV.Table.Rows.Find(selectedList(i))
                    If State.SelectAllClicked = True Then
                        If Not selectedList.Contains(State.searchDV.Table.Rows(i)("Cert_number").ToString() + State.searchDV.Table.Rows(i)("REC_NEW_BUSINESS").ToString() + State.searchDV.Table.Rows(i)("REC_CANCEL").ToString() + State.searchDV.Table.Rows(i)("REC_BILLING").ToString() + GuidControl.ByteArrayToGuid(State.searchDV.Table.Rows(i)("Billing_Detail_Id")).ToString()) Then

                            certNo = State.searchDV.Table.Rows(i)("cert_number").ToString()
                            'createdDate = gr.Cells(2).Text.ToString() 'DateTime.Parse()
                            certcreatedDate = DateTime.Parse(State.searchDV.Table.Rows(i)("cert_created_date").ToString(),
                                                        System.Threading.Thread.CurrentThread.CurrentCulture,
                                                        System.Globalization.DateTimeStyles.NoCurrentDateDefault)
                            enrollments = State.searchDV.Table.Rows(i)("rec_new_business").ToString()
                            cancellations = State.searchDV.Table.Rows(i)("rec_cancel").ToString()
                            billing = State.searchDV.Table.Rows(i)("rec_billing").ToString()

                            company_id = GuidControl.ByteArrayToGuid(State.searchDV.Table.Rows(i)("company_id"))
                            dealer_id = GuidControl.ByteArrayToGuid(State.searchDV.Table.Rows(i)("dealer_id"))
                            cert_id = GuidControl.ByteArrayToGuid(State.searchDV.Table.Rows(i)("cert_id"))

                            createdDate = DateTime.Parse(State.searchDV.Table.Rows(i)("created_date").ToString(),
                                                       System.Threading.Thread.CurrentThread.CurrentCulture,
                                                       System.Globalization.DateTimeStyles.NoCurrentDateDefault)
                            recordType = State.searchDV.Table.Rows(i)("record_type").ToString()
                            createdBy = State.searchDV.Table.Rows(i)("created_by").ToString()
                            billing_detail_id = GuidControl.ByteArrayToGuid(State.searchDV.Table.Rows(i)("billing_detail_id"))

                            DailyOutboundFileDetail.InsertDetailRecord(company_id, dealer_id, cert_id, certcreatedDate, certNo, enrollments, cancellations, billing, recordType, createdDate, createdBy, billing_detail_id)
                            'dailyOutboundFileDetailDAL.insertdetailrecords(company_id, dealer_id, certNo, createdDate, enrollments, cancellations, billing)
                            DailyObdFileDetailTemp.DeleteTempRecord(GuidControl.ByteArrayToGuid(State.searchDV.Table.Rows(i)("File_Detail_Temp_Id")))
                            savedRecCount += 1
                        End If
                    Else
                        If selectedList.Contains(State.searchDV.Table.Rows(i)("Cert_number").ToString() + State.searchDV.Table.Rows(i)("REC_NEW_BUSINESS").ToString() + State.searchDV.Table.Rows(i)("REC_CANCEL").ToString() + State.searchDV.Table.Rows(i)("REC_BILLING").ToString() + GuidControl.ByteArrayToGuid(State.searchDV.Table.Rows(i)("Billing_Detail_Id")).ToString()) Then

                            certNo = State.searchDV.Table.Rows(i)("cert_number").ToString()
                            'createdDate = gr.Cells(2).Text.ToString() 'DateTime.Parse()
                            certcreatedDate = DateTime.Parse(State.searchDV.Table.Rows(i)("cert_created_date").ToString(),
                                                        System.Threading.Thread.CurrentThread.CurrentCulture,
                                                        System.Globalization.DateTimeStyles.NoCurrentDateDefault)
                            enrollments = State.searchDV.Table.Rows(i)("rec_new_business").ToString()
                            cancellations = State.searchDV.Table.Rows(i)("rec_cancel").ToString()
                            billing = State.searchDV.Table.Rows(i)("rec_billing").ToString()

                            company_id = GuidControl.ByteArrayToGuid(State.searchDV.Table.Rows(i)("company_id"))
                            dealer_id = GuidControl.ByteArrayToGuid(State.searchDV.Table.Rows(i)("dealer_id"))
                            cert_id = GuidControl.ByteArrayToGuid(State.searchDV.Table.Rows(i)("cert_id"))

                            createdDate = DateTime.Parse(State.searchDV.Table.Rows(i)("created_date").ToString(),
                                                       System.Threading.Thread.CurrentThread.CurrentCulture,
                                                       System.Globalization.DateTimeStyles.NoCurrentDateDefault)
                            recordType = State.searchDV.Table.Rows(i)("record_type").ToString()
                            createdBy = State.searchDV.Table.Rows(i)("created_by").ToString()
                            billing_detail_id = GuidControl.ByteArrayToGuid(State.searchDV.Table.Rows(i)("billing_detail_id"))

                            DailyOutboundFileDetail.InsertDetailRecord(company_id, dealer_id, cert_id, certcreatedDate, certNo, enrollments, cancellations, billing, recordType, createdDate, createdBy, billing_detail_id)
                            'dailyOutboundFileDetailDAL.insertdetailrecords(company_id, dealer_id, certNo, createdDate, enrollments, cancellations, billing)
                            DailyObdFileDetailTemp.DeleteTempRecord(GuidControl.ByteArrayToGuid(State.searchDV.Table.Rows(i)("File_Detail_Temp_Id")))
                            savedRecCount += 1
                        End If
                    End If
                Next

            Else
                ''''save select all records
                If State.SelectAllClicked = True And selectedList Is Nothing Then
                    For Each dr As DataRow In State.searchDV.Table.Rows
                        company_id = GuidControl.ByteArrayToGuid(dr("company_id"))
                        dealer_id = GuidControl.ByteArrayToGuid(dr("dealer_id"))
                        cert_id = GuidControl.ByteArrayToGuid(dr("cert_id"))
                        certcreatedDate = DateTime.Parse(dr("Cert_created_Date").ToString(),
                                                        System.Threading.Thread.CurrentThread.CurrentCulture,
                                                        System.Globalization.DateTimeStyles.NoCurrentDateDefault)
                        certNo = dr("cert_number").ToString()
                        enrollments = dr("REC_NEW_BUSINESS").ToString()
                        cancellations = dr("REC_CANCEL").ToString()
                        billing = dr("REC_BILLING").ToString()
                        recordType = dr("Record_type").ToString()
                        createdDate = DateTime.Parse(dr("created_Date").ToString(),
                                                        System.Threading.Thread.CurrentThread.CurrentCulture,
                                                        System.Globalization.DateTimeStyles.NoCurrentDateDefault)
                        createdBy = dr("created_by").ToString()
                        billing_detail_id = GuidControl.ByteArrayToGuid(dr("Billing_Detail_Id"))

                        DailyOutboundFileDetail.InsertDetailRecord(company_id, dealer_id, cert_id, certcreatedDate, certNo, enrollments, cancellations, billing, recordType, createdDate, createdBy, billing_detail_id)


                        DailyObdFileDetailTemp.DeleteTempRecord(GuidControl.ByteArrayToGuid(dr("File_Detail_Temp_Id")))
                        btnSaveRecords.Visible = False
                        savedRecCount += 1
                    Next


                End If
            End If
            State.searchDV = Nothing
            PopulateGrid()
            ShowGrid(State.searchDV)
            State.SelectAllClicked = False
            'End If
            If savedRecCount > 0 Then
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
            Else
                MasterPage.MessageController.AddError(Message.MSG_ATLEAST_ONE_RECORD_SHLD_BE_CHECKED, True)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Protected Sub btnDelectRecords_Click(sender As Object, e As EventArgs) Handles btnDelectRecords.Click
        Try
            Dim delRecCount As Integer = 0
            Dim uchk As CheckBox
            Dim selectedList As New ArrayList
            If (Session("Checked_Items") IsNot Nothing) Then
                selectedList = CType(Session("Checked_Items"), ArrayList)
            End If

            'If selectedList.Count = 0 Then
            If selectedList Is Nothing Then
                If checkboxParentHidden.Value = "selectAllNotChecked" Then
                    For Each dr As DataRow In State.ViewDV.Table.Rows
                        DailyOutboundFileDetail.DeleteDetailRecord(GuidControl.ByteArrayToGuid(dr("file_detail_Id")))
                        State.IsRecordDeleted = True
                        delRecCount += 1
                    Next
                End If
            Else
                Dim fileDetailId As Guid = Guid.Empty
                Dim tempRow() As DataRow

                For i As Integer = 0 To State.ViewDV.Table.Rows.Count - 1 'selectedList.Count - 1
                    If State.SelectAllClicked = True Then
                        If selectedList.Contains(State.ViewDV.Table.Rows(i)("Cert_number").ToString() + State.ViewDV.Table.Rows(i)("REC_NEW_BUSINESS").ToString() + State.ViewDV.Table.Rows(i)("REC_CANCEL").ToString() + State.ViewDV.Table.Rows(i)("REC_BILLING").ToString() + GuidControl.ByteArrayToGuid(State.ViewDV.Table.Rows(i)("Billing_Detail_Id")).ToString()) Then
                            fileDetailId = GuidControl.ByteArrayToGuid(State.ViewDV.Table.Rows(i)("file_detail_id"))
                            DailyOutboundFileDetail.DeleteDetailRecord(fileDetailId)
                            State.IsRecordDeleted = True
                            delRecCount += 1
                        End If
                    Else
                        If Not selectedList.Contains(State.ViewDV.Table.Rows(i)("Cert_number").ToString() + State.ViewDV.Table.Rows(i)("REC_NEW_BUSINESS").ToString() + State.ViewDV.Table.Rows(i)("REC_CANCEL").ToString() + State.ViewDV.Table.Rows(i)("REC_BILLING").ToString() + GuidControl.ByteArrayToGuid(State.ViewDV.Table.Rows(i)("Billing_Detail_Id")).ToString()) Then
                            fileDetailId = GuidControl.ByteArrayToGuid(State.ViewDV.Table.Rows(i)("file_detail_id"))
                            DailyOutboundFileDetail.DeleteDetailRecord(fileDetailId)
                            State.IsRecordDeleted = True
                            delRecCount += 1
                        End If
                    End If
                Next
                'End If
            End If
            If State.IsRecordDeleted Then
                State.SelectAllClicked = True ''to supress No Records Found message
                checkboxParentHidden.Value = "selectAllChecked"
                btnView_Click(sender, e)
                If delRecCount > 0 Then
                    MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION, True)
                Else
                    MasterPage.MessageController.AddError(Message.MSG_ATLEAST_ONE_RECORD_SHLD_BE_UNCHECK, True)
                End If
            Else
                MasterPage.MessageController.AddError(Message.MSG_ATLEAST_ONE_RECORD_SHLD_BE_UNCHECK, True)
            End If
            'Me.State.SelectAllClicked = False


        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
End Class