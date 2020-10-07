Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms



Partial Class DealerInvoiceDatesOverwriteForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "DealerInvoiceDatesOverwriteForm.aspx"
    Public Const PAGETITLE As String = "DEALER_INVOICE_DATES"
    Public Const PAGETAB As String = "FINANCE_AUTOMATION"

    Public Const GRID_CTRL_NAME_MANUAL_INV_DATE As String = "txtManualInvDate"
    Public Const GRID_CTRL_NAME_MANUAL_INV_DATE_CALENDAR As String = "imgInvoiceDate"
    Public Const GRID_CTRL_NAME_MANUAL_DUE_DATE As String = "txtManualDueDate"
    Public Const GRID_CTRL_NAME_MANUAL_DUE_DATE_CALENDAR As String = "imgDueDate"
    Public Const GRID_CTRL_NAME_Button_Dates_Overwrite As String = "BtnOverwriteDates_WRITE"
    Public Const GRID_CTRL_NAME_Button_Invoice_Update As String = "BtnUpdateInvoice_WRITE"

    Private Const GRID_COL_DEALER_IDX As Integer = 0
    Private Const GRID_COL_ACCT_PERIOD_IDX As Integer = 1
    Private Const GRID_COL_INVOICE_DATE_IDX As Integer = 2
    Private Const GRID_COL_DUE_DATE_IDX As Integer = 3
    Private Const GRID_COL_INVOICE_DATE_MANUAL_IDX As Integer = 4
    Private Const GRID_COL_DUE_DATE_MANUAL_IDX As Integer = 5
    Private Const GRID_COL_BUTTON_IDX As Integer = 6

    Private Const DATE_NOT_AVAILABLE As String = "Not Available"
    Public Enum PageAction
        None = 0
        Edit = 1
    End Enum

    Private dvDealerList As DataView '= LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, True, "Code")
    Private savedAccountingMonth As String

    Private ReadOnly Property DealerList() As DataView
        Get
            If dvDealerList Is Nothing Then
                dvDealerList = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, True, "Code")
            End If
            Return dvDealerList
        End Get
    End Property
#End Region

#Region "Page State"
    Class MyState
        ' Selected Item Information
        Public SearchDV As DataView = Nothing
        Public searchDealerID As Guid
        Public searchPeriodYear As String
        Public searchPeriodMonth As String
        Public myBOManualInvoiceDate As AfaInvoiceManaulData
        Public myBOManualDueDate As AfaInvoiceManaulData
        Public gridAction As PageAction = PageAction.None        

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

#Region "Page Events"
    Private Sub UpdateBreadCrum()
        MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        MasterPage.UsePageTabTitleInBreadCrum = False
        MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + TranslationBase.TranslateLabelOrMessage(PAGETITLE)
    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        MasterPage.MessageController.Clear()
        UpdateBreadCrum()
        If Not IsPostBack Then
            PopulateDropdowns()
            TranslateGridHeader(Grid)            
        End If
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

#End Region

#Region "Helper functions"
    Private Sub PopulateDropdowns()
        'Me.BindCodeNameToListControl(ddlDealer, DealerList, , , , False)
        Dim oDealerList = GetDealerListByCompanyForUser()
        Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                           Return li.ExtendedCode + " - " + li.Translation + " " + "(" + li.Code + ")"
                                                                       End Function
        ddlDealer.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .TextFunc = dealerTextFunc
                                           })
        Dim intYear As Integer = DateTime.Today.Year
        For i As Integer = (intYear - 7) To intYear + 1
            ddlAcctPeriodYear.Items.Add(New System.Web.UI.WebControls.ListItem(i.ToString, i.ToString))
        Next

        Dim monthName As String
        For month As Integer = 1 To 12
            monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month)
            ddlAcctPeriodMonth.Items.Add(New System.Web.UI.WebControls.ListItem(monthName, month.ToString().PadLeft(2, CChar("0"))))
        Next
        Dim intMonth As Integer = (DateTime.Today.Month - 1) 'default to previous month
        If intMonth = 0 Then
            intMonth = 12
            intYear = intYear - 1
        End If
        ddlAcctPeriodYear.SelectedValue = intYear.ToString
        ddlAcctPeriodMonth.SelectedValue = intMonth.ToString.PadLeft(2, CChar("0"))
    End Sub
    Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
        Dim Index As Integer
        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

        Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

        Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

        For Index = 0 To UserCompanies.Count - 1
            'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
            oListContext.CompanyId = UserCompanies(Index)
            Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
            If oDealerListForCompany.Count > 0 Then
                If oDealerList IsNot Nothing Then
                    oDealerList.AddRange(oDealerListForCompany)
                Else
                    oDealerList = oDealerListForCompany.Clone()
                End If

            End If
        Next

        Return oDealerList.ToArray()

    End Function

    Private Sub PopulateGrid()
        Dim recCount As Integer
        Try

            If State.SearchDV Is Nothing Then
                State.SearchDV = AfaInvoiceManaulData.GetDealerInvoiceDates(State.searchDealerID, State.searchPeriodYear & State.searchPeriodMonth)
            End If

            recCount = State.SearchDV.Count

            Grid.DataSource = State.SearchDV
            Grid.DataBind()

            If State.SearchDV.Count > 0 Then
                ControlMgr.SetVisibleControl(Me, moSearchResults, True)
            Else
                If State.gridAction = PageAction.None Then
                    MasterPage.MessageController.AddInformation(Message.MSG_NO_RECORDS_FOUND, True)
                End If
                ControlMgr.SetVisibleControl(Me, moSearchResults, False)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Function PopulateBOFromForm() As Boolean
        Dim blnSuccess As Boolean = True, dtTemp As Date

        Dim ind As Integer = Grid.EditIndex
        Dim objtxt As TextBox = CType(Grid.Rows(ind).FindControl(GRID_CTRL_NAME_MANUAL_INV_DATE), TextBox)
        If objtxt.Text.Trim <> String.Empty Then
            If Date.TryParse(objtxt.Text.Trim, dtTemp) Then
                State.myBOManualInvoiceDate.DataDate = dtTemp
            Else
                blnSuccess = False
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("MANUAL_INVOICE_DATE") + ": " + TranslationBase.TranslateLabelOrMessage("MSG_INVALID_DATE"))
            End If
        End If
        
        'PopulateBOProperty(State.myBOManualInvoiceDate, "DataDate", objtxt)

        objtxt = CType(Grid.Rows(ind).FindControl(GRID_CTRL_NAME_MANUAL_DUE_DATE), TextBox)
        If objtxt.Text.Trim <> String.Empty Then
            If Date.TryParse(objtxt.Text.Trim, dtTemp) Then
                State.myBOManualDueDate.DataDate = dtTemp
            Else
                blnSuccess = False
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("MANUAL_DUE_DATE") + ": " + TranslationBase.TranslateLabelOrMessage("MSG_INVALID_DATE"))
            End If
        End If
        
        'PopulateBOProperty(State.myBOManualDueDate, "DataDate", objtxt)

        Return blnSuccess
    End Function

    Private Function SaveData() As Boolean
        Dim blnSaved As Boolean = False
        Try
            If PopulateBOFromForm() = False Then
                MasterPage.MessageController.Show()
                Return False
            End If

            With State.myBOManualInvoiceDate
                If .IsDirty AndAlso (.DataDate IsNot Nothing) Then 'value populated, save the change
                    .SaveWithoutCheckDSCreator()
                    blnSaved = True
                ElseIf (Not .IsNew) AndAlso .DataDate Is Nothing Then 'existing record erased, delete the records
                    .Delete()
                    .SaveWithoutCheckDSCreator()
                    blnSaved = True
                End If
            End With

            With State.myBOManualDueDate
                If .IsDirty AndAlso (.DataDate IsNot Nothing) Then 'value populated, save the change
                    .SaveWithoutCheckDSCreator()
                    blnSaved = True
                ElseIf (Not .IsNew) AndAlso .DataDate Is Nothing Then 'existing record erased, delete the records
                    .Delete()
                    .SaveWithoutCheckDSCreator()
                    blnSaved = True
                End If
            End With

            If blnSaved = True Then
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)
            Else
                MasterPage.MessageController.AddSuccess(Message.MSG_RECORD_NOT_SAVED)
            End If


            Return True
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Function
#End Region

#Region "Button events handlers"
    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            'set dropdowns to default values
            ddlDealer.SelectedIndex = -1
            Dim intYear As Integer = DateTime.Today.Year
            Dim intMonth As Integer = (DateTime.Today.Month - 1) 'default to previous month
            If intMonth = 0 Then
                intMonth = 12
                intYear = intYear - 1
            End If
            ddlAcctPeriodYear.SelectedValue = intYear.ToString
            ddlAcctPeriodMonth.SelectedValue = intMonth.ToString.PadLeft(2, CChar("0"))

            'clear state values
            State.SearchDV = Nothing
            State.searchDealerID = Guid.Empty
            State.searchPeriodMonth = String.Empty
            State.searchPeriodYear = String.Empty
            ControlMgr.SetVisibleControl(Me, moSearchResults, False) ' Hidden the search result grid
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Try

            State.searchDealerID = New Guid(ddlDealer.SelectedValue)
            State.searchPeriodYear = ddlAcctPeriodYear.SelectedValue
            State.searchPeriodMonth = ddlAcctPeriodMonth.SelectedValue

            State.SearchDV = Nothing
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    
    Private Sub SetControlState()
        If State.gridAction = PageAction.Edit Then
            ControlMgr.SetEnableControl(Me, btnSearch, False)
            ControlMgr.SetEnableControl(Me, btnClear, False)
            MenuEnabled = False
        Else
            ControlMgr.SetEnableControl(Me, btnSearch, True)
            ControlMgr.SetEnableControl(Me, btnClear, True)
            MenuEnabled = True
        End If
    End Sub

#End Region

#Region "Handle grid"

    Private Sub grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            Dim index As Integer

            Select Case e.CommandName.ToString()
                Case "OverwriteDates"
                    index = CInt(e.CommandArgument)

                    Grid.EditIndex = index
                    Grid.SelectedIndex = index
                    State.gridAction = PageAction.Edit

                    Dim strTemp As String, objlist As Collections.Generic.List(Of AfaInvoiceManaulData)

                    State.myBOManualInvoiceDate = Nothing
                    If State.SearchDV(0)("ManualInvoiceDate").ToString <> DATE_NOT_AVAILABLE Then
                        objlist = AfaInvoiceManaulData.GetListByType(State.searchDealerID, State.searchPeriodYear & State.searchPeriodMonth, "DATE_INVOICE")
                        If objlist.Count > 0 Then
                            State.myBOManualInvoiceDate = objlist.Item(0)
                        End If
                    End If

                    If State.myBOManualInvoiceDate Is Nothing Then 'new record
                        State.myBOManualInvoiceDate = New AfaInvoiceManaulData()
                        State.myBOManualInvoiceDate.DealerId = State.searchDealerID
                        State.myBOManualInvoiceDate.InvoiceMonth = State.searchPeriodYear & State.searchPeriodMonth
                        State.myBOManualInvoiceDate.AmountTypeCode = "DATE_INVOICE"
                    End If
                    objlist = Nothing

                    State.myBOManualDueDate = Nothing
                    If State.SearchDV(0)("ManualDueDate").ToString <> DATE_NOT_AVAILABLE Then
                        objlist = AfaInvoiceManaulData.GetListByType(State.searchDealerID, State.searchPeriodYear & State.searchPeriodMonth, "DATE_INV_DUE")
                        If objlist.Count > 0 Then
                            State.myBOManualDueDate = objlist.Item(0)
                        End If
                    End If

                    If State.myBOManualDueDate Is Nothing Then 'new record
                        State.myBOManualDueDate = New AfaInvoiceManaulData()
                        State.myBOManualDueDate.DealerId = State.searchDealerID
                        State.myBOManualDueDate.InvoiceMonth = State.searchPeriodYear & State.searchPeriodMonth
                        State.myBOManualDueDate.AmountTypeCode = "DATE_INV_DUE"
                    End If

                    PopulateGrid()
                    SetControlState()
                Case "UpdateInvoice"
                    Dim strResult As String
                    Try
                        AfaInvoiceManaulData.UpdateInvoicewithManualDates(State.searchDealerID, State.searchPeriodYear & State.searchPeriodMonth, strResult)

                        If strResult.StartsWith("Error") Then
                            MasterPage.MessageController.AddErrorAndShow(strResult, True)
                        Else
                            MasterPage.MessageController.AddSuccess(strResult, True)
                        End If

                    Catch ex As Exception
                        HandleErrors(ex, MasterPage.MessageController)
                    End Try
                    State.SearchDV = Nothing
                    PopulateGrid()
                Case "CancelRecord"
                    Grid.EditIndex = -1
                    Grid.SelectedIndex = -1                    
                    PopulateGrid()
                    State.gridAction = PageAction.None
                    State.myBOManualInvoiceDate = Nothing
                    State.myBOManualDueDate = Nothing
                    SetControlState()
                Case "SaveRecord"
                    If SaveData() Then
                        State.SearchDV = Nothing
                        State.myBOManualInvoiceDate = Nothing
                        State.myBOManualDueDate = Nothing
                        Grid.EditIndex = -1
                        State.gridAction = PageAction.None
                        PopulateGrid()
                        SetControlState()
                    End If
            End Select
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim strTemp As String, dtTemp As Date, objBtn As Button, guidTemp As Guid

            If (itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                With e.Row
                    If .RowIndex = Grid.EditIndex Then
                        Dim objtxt As TextBox, objImg As ImageButton
                        objtxt = CType(e.Row.FindControl(GRID_CTRL_NAME_MANUAL_INV_DATE), TextBox)
                        If objtxt IsNot Nothing Then
                            strTemp = dvRow("ManualInvoiceDate").ToString
                            If strTemp = DATE_NOT_AVAILABLE Then
                                strTemp = String.Empty
                            Else
                                If Date.TryParseExact(strTemp, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dtTemp) Then
                                    strTemp = GetDateFormattedString(dtTemp)
                                Else
                                    strTemp = String.Empty
                                End If
                            End If
                            objtxt.Text = strTemp
                        End If
                        objImg = CType(e.Row.FindControl(GRID_CTRL_NAME_MANUAL_INV_DATE_CALENDAR), ImageButton)
                        objImg.Visible = True
                        AddCalendar_New(objImg, objtxt)
                        objImg = Nothing
                        objtxt = Nothing
                        strTemp = String.Empty

                        objtxt = CType(e.Row.FindControl(GRID_CTRL_NAME_MANUAL_DUE_DATE), TextBox)
                        If objtxt IsNot Nothing Then
                            strTemp = dvRow("ManualDueDate").ToString
                            If strTemp = DATE_NOT_AVAILABLE Then
                                strTemp = strTemp.Empty
                            Else
                                If Date.TryParseExact(strTemp, "MM/dd/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, dtTemp) Then
                                    strTemp = GetDateFormattedString(dtTemp)
                                Else
                                    strTemp = String.Empty
                                End If
                            End If
                            objtxt.Text = strTemp
                        End If
                        objImg = CType(e.Row.FindControl(GRID_CTRL_NAME_MANUAL_DUE_DATE_CALENDAR), ImageButton)
                        objImg.Visible = True
                        AddCalendar_New(objImg, objtxt)
                        objImg = Nothing
                        objtxt = Nothing
                    Else

                        objBtn = CType(e.Row.FindControl(GRID_CTRL_NAME_Button_Dates_Overwrite), Button)
                        If objBtn IsNot Nothing Then
                            objBtn.Text = TranslationBase.TranslateLabelOrMessage(objBtn.Text)
                        End If
                        objBtn = CType(e.Row.FindControl(GRID_CTRL_NAME_Button_Invoice_Update), Button)
                        If objBtn IsNot Nothing Then
                            objBtn.Visible = False
                            If Not (dvRow("AFA_INVOICE_DATA_ID") Is DBNull.Value) Then 'allow update only when manual dates and inovice are available, and also there are differences 
                                strTemp = dvRow("ManualInvoiceDate").ToString.Trim
                                If strTemp <> DATE_NOT_AVAILABLE AndAlso strTemp <> dvRow("InvoiceDate").ToString Then
                                    objBtn.Visible = True

                                End If
                                strTemp = dvRow("ManualDueDate").ToString.Trim
                                If objBtn.Visible = False AndAlso strTemp <> DATE_NOT_AVAILABLE AndAlso strTemp <> dvRow("DueDate").ToString Then
                                    objBtn.Visible = True
                                End If
                                If objBtn.Visible = True Then
                                    objBtn.Text = TranslationBase.TranslateLabelOrMessage(objBtn.Text)
                                End If
                            End If
                        End If
                        
                        
                    End If
                End With
            End If
            BaseItemBound(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
End Class