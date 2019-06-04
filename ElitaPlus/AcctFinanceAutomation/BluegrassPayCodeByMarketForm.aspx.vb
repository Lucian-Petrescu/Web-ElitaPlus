
Imports System.Globalization

Partial Class BluegrassPayCodeByMarketForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "BluegrassPayCodeByMarketForm.aspx"
    Public Const PAGETITLE As String = "Blue Grass Pay Codes By Market"
    Public Const PAGETAB As String = "FINANCE_AUTOMATION"

    Public Const GRID_CTRL_NAME_PAYCODE_ROW_ID As String = "lblPayCodeRowID"
    Public Const GRID_CTRL_NAME_AcctPeriod_Year As String = "ddlGridAcctPeriodYear"
    Public Const GRID_CTRL_NAME_AcctPeriod_Month As String = "ddlGridAcctPeriodMonth"
    Public Const GRID_CTRL_NAME_MARKET_CODE As String = "txtMarketCode"
    Public Const GRID_CTRL_NAME_AP_CODE As String = "txtAPCode"
    Public Const GRID_CTRL_NAME_BCI_CODE As String = "txtBCICode"
    Public Const GRID_CTRL_NAME_KYCODE As String = "txtKYCode"


    Public Const GRID_CTRL_NAME_Button_Edit As String = "EditButton_WRITE"
    Public Const GRID_CTRL_NAME_Button_Delete As String = "DeleteButton_WRITE"

    Private Const GRID_COL_ROW_ID_IDX As Integer = 0
    Private Const GRID_COL_EFFECTIVE_IDX As Integer = 1
    Private Const GRID_COL_MARKETCODE_IDX As Integer = 2
    Private Const GRID_COL_APCODE_IDX As Integer = 3
    Private Const GRID_COL_BCICODE_IDX As Integer = 4
    Private Const GRID_COL_KYCODE_IDX As Integer = 5
    Private Const GRID_COL_BUTTON_IDX As Integer = 6

    Public Enum PageAction
        None = 0
        AddNew = 1
        EditExisting = 2
        Delete = 3
    End Enum

    Private savedAccountingMonth As String

#End Region

#Region "Page State"
    Class MyState
        ' Selected Item Information
        Public SearchDV As Collections.Generic.List(Of AfaInvoiceManaulData) = Nothing
        Public BluegrassDealerID As Guid
        Public searchPeriodYear As String
        Public searchPeriodMonth As String
        Public searchMarketCode As String
        Public myBO As AfaInvoiceManaulData
        Public gridAction As PageAction = PageAction.None
        Public PayCodeRecordID As Guid

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
        Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
        Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("SEARCH")
        Me.MasterPage.UsePageTabTitleInBreadCrum = False
        Me.MasterPage.BreadCrum = TranslationBase.TranslateLabelOrMessage(PAGETAB) + ElitaBase.Sperator + PAGETITLE
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.MasterPage.MessageController.Clear()
        Me.UpdateBreadCrum()
        SetBluegrassDearID()
        If Not Me.IsPostBack Then
            PopulateDropdowns()
            Me.TranslateGridHeader(Me.Grid)
            ControlMgr.SetVisibleControl(Me, moSearchResults, False)
        Else
            CheckIfComingFromDeleteConfirm()
        End If
        'Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub
#End Region

#Region "Helper functions"
    Private Sub SetBluegrassDearID()
        If State.BluegrassDealerID = Guid.Empty Then
            Dim dealerdv As Dealer.DealerSearchDV = Dealer.getList(String.Empty, "BLGS", Nothing, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            If Not dealerdv Is Nothing AndAlso dealerdv.Count = 1 Then
                State.BluegrassDealerID = GuidControl.ByteArrayToGuid(CType(dealerdv(0)(dealerdv.COL_DEALER_ID), Byte()))
            End If
        End If
    End Sub
    Private Sub PopulateDropdowns()

        Dim intYear As Integer = DateTime.Today.Year
        ddlAcctPeriodYear.Items.Add(New ListItem("", "0000"))
        For i As Integer = (intYear - 7) To intYear
            ddlAcctPeriodYear.Items.Add(New ListItem(i.ToString, i.ToString))
        Next

        Dim monthName As String
        ddlAcctPeriodMonth.Items.Add(New ListItem("", "00"))
        For month As Integer = 1 To 12
            monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month)
            ddlAcctPeriodMonth.Items.Add(New ListItem(monthName, month.ToString().PadLeft(2, CChar("0"))))
        Next

        ddlAcctPeriodYear.SelectedValue = "0000"
        ddlAcctPeriodMonth.SelectedValue = "00"
    End Sub

    Protected Sub CheckIfComingFromDeleteConfirm()
        Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
        If Not confResponse Is Nothing AndAlso State.gridAction = PageAction.Delete Then
            If confResponse = Me.MSG_VALUE_YES Then
                DeletePayCodeRecord()
                Grid.EditIndex = -1
                State.SearchDV = Nothing
                PopulateGrid()
            End If
            'Clean after consuming the action
            State.gridAction = PageAction.None
            SetControlState()
            Me.HiddenSaveChangesPromptResponse.Value = ""
        End If
    End Sub

    Private Sub PopulateGrid()
        Dim recCount As Integer
        Try

            If Me.State.SearchDV Is Nothing Then
                State.SearchDV = AfaInvoiceManaulData.GetListByTypeByPeriod(State.BluegrassDealerID, "MKT_PAYCODE", State.searchPeriodYear & State.searchPeriodMonth, "209912")
                If State.searchMarketCode <> String.Empty Then
                    Dim listMatched As New Collections.Generic.List(Of AfaInvoiceManaulData)
                    For Each objTem As AfaInvoiceManaulData In State.SearchDV
                        If objTem.DataText.ToUpper.Trim = State.searchMarketCode.ToUpper.Trim Then
                            listMatched.Add(objTem)
                        End If
                    Next
                    State.SearchDV = listMatched
                End If
            End If

            recCount = State.SearchDV.Count

            Me.Grid.DataSource = State.SearchDV
            Me.Grid.DataBind()

            If State.SearchDV.Count > 0 Then
                ControlMgr.SetVisibleControl(Me, moSearchResults, True)
            Else
                If State.gridAction = PageAction.None Then
                    Me.MasterPage.MessageController.AddInformation(Message.MSG_NO_RECORDS_FOUND, True)
                End If
                ControlMgr.SetVisibleControl(Me, moSearchResults, False)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Function ValidateChangesBeforeSave() As Boolean
        Dim blnResult As Boolean = True
        '1 validate a not duplicate if add new
        Dim objList As Collections.Generic.List(Of AfaInvoiceManaulData), objTemp As AfaInvoiceManaulData

        With State.myBO
            objList = AfaInvoiceManaulData.GetListByType(.DealerId, .InvoiceMonth, "MKT_PAYCODE")
            For Each objTemp In objList
                If objTemp.DataText = .DataText AndAlso objTemp.Id <> .Id Then 'found duplicate for the market code
                    blnResult = False
                    MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("Accounting_Period") + ": " + TranslationBase.TranslateLabelOrMessage("YOU_ENTER_A_DUPLICATED_MOTH"))
                End If
            Next
        End With

        Return blnResult
    End Function

    Private Function PopulateBOFromForm() As Boolean
        Dim blnSuccess As Boolean = True
        Dim strTemp As String, acctPeriod As String, strMarketCode As String, strAPCode As String, strBCICode As String, strKYCode As String

        Dim ind As Integer = Grid.EditIndex
        Dim ddl As DropDownList = CType(Grid.Rows(ind).FindControl(GRID_CTRL_NAME_AcctPeriod_Year), DropDownList)
        strTemp = ddl.SelectedValue
        If strTemp = String.Empty Then
            blnSuccess = False
            MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("Accounting_Period") + ": " + TranslationBase.TranslateLabelOrMessage("REQUIRED_FIELDS_ARE_MISSING"))
        Else
            acctPeriod = strTemp
        End If
        ddl = Nothing

        ddl = CType(Grid.Rows(ind).FindControl(GRID_CTRL_NAME_AcctPeriod_Month), DropDownList)
        strTemp = ddl.SelectedValue
        If strTemp = String.Empty Then
            blnSuccess = False
            MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("Accounting_Period") + ": " + TranslationBase.TranslateLabelOrMessage("REQUIRED_FIELDS_ARE_MISSING"))
        Else
            acctPeriod += strTemp
        End If
        ddl = Nothing

        Dim txt As TextBox = CType(Grid.Rows(ind).FindControl(GRID_CTRL_NAME_MARKET_CODE), TextBox)
        strMarketCode = txt.Text.Trim
        If strMarketCode = String.Empty Then
            blnSuccess = False
            MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("MARKET_CODE") + ": " + TranslationBase.TranslateLabelOrMessage("REQUIRED_FIELDS_ARE_MISSING"))
        End If
        txt = Nothing

        txt = CType(Grid.Rows(ind).FindControl(GRID_CTRL_NAME_AP_CODE), TextBox)
        strAPCode = txt.Text.Trim
        If strAPCode = String.Empty Then
            blnSuccess = False
            MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("ASSURANT_PAYABLE") + ": " + TranslationBase.TranslateLabelOrMessage("REQUIRED_FIELDS_ARE_MISSING"))
        End If
        txt = Nothing

        txt = CType(Grid.Rows(ind).FindControl(GRID_CTRL_NAME_BCI_CODE), TextBox)
        strBCICode = txt.Text.Trim
        If strBCICode = String.Empty Then
            blnSuccess = False
            MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("BCI_REVENUE") + ": " + TranslationBase.TranslateLabelOrMessage("REQUIRED_FIELDS_ARE_MISSING"))
        End If
        txt = Nothing

        txt = CType(Grid.Rows(ind).FindControl(GRID_CTRL_NAME_KYCODE), TextBox)
        strKYCode = txt.Text.Trim
        If strKYCode = String.Empty Then
            blnSuccess = False
            MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("KY_SURCHARGE") + ": " + TranslationBase.TranslateLabelOrMessage("REQUIRED_FIELDS_ARE_MISSING"))
        End If
        txt = Nothing

        If blnSuccess Then
            With State.myBO
                .DealerId = State.BluegrassDealerID
                .InvoiceMonth = acctPeriod
                .DataText = strMarketCode
                .AmountTypeCode = "MKT_PAYCODE"
                .DataText2 = strAPCode
                .DataText3 = strBCICode
                .DataText4 = strKYCode
            End With
            savedAccountingMonth = acctPeriod
        End If

        Return blnSuccess
    End Function

    Private Function SaveData() As Boolean
        Try
            If PopulateBOFromForm() = False Then
                MasterPage.MessageController.Show()
                Return False
            End If

            'valiate the records before saving
            If ValidateChangesBeforeSave() = False Then
                MasterPage.MessageController.Show()
                Return False
            End If

            'save the record
            State.myBO.SaveWithoutCheckDSCreator()
            Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)

            Return True
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Function

    Private Sub DeletePayCodeRecord()
        Try
            State.myBO.Delete()
            State.myBO.SaveWithoutCheckDSCreator()
            State.myBO = Nothing
            Me.MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Button events handlers"
    Protected Sub btnClear_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClear.Click
        Try
            'set dropdowns to default values
            ddlAcctPeriodYear.SelectedIndex = -1
            ddlAcctPeriodMonth.SelectedIndex = -1
            State.SearchDV = Nothing
            State.searchPeriodMonth = String.Empty
            State.searchPeriodYear = String.Empty
            State.searchMarketCode = String.Empty
            ControlMgr.SetVisibleControl(Me, moSearchResults, False) ' Hidden the search result grid
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
        Try

            Me.State.searchPeriodYear = ddlAcctPeriodYear.SelectedValue
            Me.State.searchPeriodMonth = ddlAcctPeriodMonth.SelectedValue
            Me.State.searchMarketCode = txtMarketCodeSearch.Text.Trim

            Me.State.SearchDV = Nothing
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnNew_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew_WRITE.Click
        Try
            State.gridAction = PageAction.AddNew
            AddNew()
            Me.SetControlState()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub AddNew()

        If State.SearchDV Is Nothing Then
            Grid.EditIndex = 0
        Else
            Grid.EditIndex = State.SearchDV.Count
        End If

        Me.State.myBO = New AfaInvoiceManaulData
        Me.State.myBO.DealerId = State.BluegrassDealerID
        Me.State.myBO.InvoiceMonth = DateTime.Today.Year.ToString & DateTime.Today.Month.ToString.PadLeft(2, CChar("0"))

        'State.myBO.AddEmptyRowToPONumSearchDV(Me.State.SearchDV, Me.State.myBO)
        If State.SearchDV Is Nothing Then
            State.SearchDV = New Collections.Generic.List(Of AfaInvoiceManaulData)
        End If
        Me.State.SearchDV.Add(State.myBO)

        PopulateGrid()
    End Sub

    Private Sub SetControlState()
        If (State.gridAction = PageAction.AddNew OrElse State.gridAction = PageAction.EditExisting) Then
            ControlMgr.SetVisibleControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnSearch, False)
            ControlMgr.SetEnableControl(Me, btnClear, False)
            Me.MenuEnabled = False
        Else
            ControlMgr.SetVisibleControl(Me, btnNew_WRITE, True)
            ControlMgr.SetEnableControl(Me, btnSearch, True)
            ControlMgr.SetEnableControl(Me, btnClear, True)
            Me.MenuEnabled = True
        End If
    End Sub


#End Region

#Region "Handle grid"

    Private Sub grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            Dim index As Integer

            Select Case e.CommandName.ToString()
                Case "EditAction"
                    index = CInt(e.CommandArgument)
                    Me.State.PayCodeRecordID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_ROW_ID_IDX).FindControl(Me.GRID_CTRL_NAME_PAYCODE_ROW_ID), Label).Text)

                    Grid.EditIndex = index
                    Grid.SelectedIndex = index
                    State.gridAction = PageAction.EditExisting
                    State.myBO = New AfaInvoiceManaulData(State.PayCodeRecordID)
                    PopulateGrid()
                    SetControlState()
                Case "DeleteRecord"
                    index = CInt(e.CommandArgument)
                    Me.State.PayCodeRecordID = New Guid(CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_ROW_ID_IDX).FindControl(Me.GRID_CTRL_NAME_PAYCODE_ROW_ID), Label).Text)
                    State.gridAction = PageAction.Delete
                    State.myBO = New AfaInvoiceManaulData(State.PayCodeRecordID)
                    Me.DisplayMessage(Message.DELETE_RECORD_PROMPT, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Case "CancelRecord"
                    Grid.EditIndex = -1
                    Grid.SelectedIndex = -1
                    If State.gridAction = PageAction.AddNew Then
                        State.SearchDV = Nothing
                    End If
                    PopulateGrid()
                    State.gridAction = PageAction.None
                    State.myBO = Nothing
                    SetControlState()
                Case "SaveRecord"
                    If SaveData() Then
                        State.searchPeriodMonth = State.myBO.InvoiceMonth.Substring(4)
                        State.searchPeriodYear = State.myBO.InvoiceMonth.Substring(0, 4)
                        State.searchMarketCode = State.myBO.DataText
                        State.SearchDV = Nothing
                        State.myBO = Nothing
                        Grid.EditIndex = -1
                        State.gridAction = PageAction.None
                        PopulateGrid()
                        SetControlState()
                    End If
            End Select
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim objRow As AfaInvoiceManaulData = CType(e.Row.DataItem, AfaInvoiceManaulData)
            Dim objDDL As DropDownList, strTemp As String

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                With e.Row
                    If .RowIndex = Grid.EditIndex Then
                        objDDL = CType(e.Row.FindControl(GRID_CTRL_NAME_AcctPeriod_Year), DropDownList)
                        If Not objDDL Is Nothing Then
                            Dim intYear As Integer = DateTime.Today.Year
                            For i As Integer = (intYear - 7) To intYear
                                objDDL.Items.Add(New ListItem(i.ToString, i.ToString))
                            Next
                            Me.SetSelectedItem(objDDL, objRow.InvoiceMonth.Substring(0, 4))
                        End If
                        objDDL = Nothing

                        objDDL = CType(e.Row.FindControl(GRID_CTRL_NAME_AcctPeriod_Month), DropDownList)
                        If Not objDDL Is Nothing Then
                            Dim monthName As String
                            For month As Integer = 1 To 12
                                monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month)
                                objDDL.Items.Add(New ListItem(monthName, month.ToString().PadLeft(2, CChar("0"))))
                            Next
                            Me.SetSelectedItem(objDDL, objRow.InvoiceMonth.Substring(4))
                        End If
                        objDDL = Nothing

                        Dim objtxt As TextBox
                        objtxt = CType(e.Row.FindControl(GRID_CTRL_NAME_MARKET_CODE), TextBox)
                        If Not objtxt Is Nothing Then
                            objtxt.Text = objRow.DataText
                        End If
                        objtxt = Nothing

                        objtxt = CType(e.Row.FindControl(GRID_CTRL_NAME_AP_CODE), TextBox)
                        If Not objtxt Is Nothing Then
                            objtxt.Text = objRow.DataText2
                        End If
                        objtxt = Nothing

                        objtxt = CType(e.Row.FindControl(GRID_CTRL_NAME_BCI_CODE), TextBox)
                        If Not objtxt Is Nothing Then
                            objtxt.Text = objRow.DataText3
                        End If
                        objtxt = Nothing

                        objtxt = CType(e.Row.FindControl(GRID_CTRL_NAME_KYCODE), TextBox)
                        If Not objtxt Is Nothing Then
                            objtxt.Text = objRow.DataText4
                        End If
                        objtxt = Nothing
                    End If
                End With
            End If
            BaseItemBound(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

End Class