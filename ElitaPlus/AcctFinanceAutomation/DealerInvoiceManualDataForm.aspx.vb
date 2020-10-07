Imports System.Globalization
Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms


Partial Class DealerInvoiceManualDataForm
    Inherits ElitaPlusSearchPage
#Region "Constants"
    Public Const URL As String = "DealerInvoiceManualDataForm.aspx"
    Public Const PAGETITLE As String = "DEALER_INVOICE_Manual_Data"
    Public Const PAGETAB As String = "FINANCE_AUTOMATION"

    Public Const GRID_CTRL_NAME_DEALER As String = "ddlGridDealer"
    Public Const GRID_CTRL_NAME_AcctPeriod_Year As String = "ddlGridAcctPeriodYear"
    Public Const GRID_CTRL_NAME_AcctPeriod_Month As String = "ddlGridAcctPeriodMonth"
    Public Const GRID_CTRL_NAME_MDFRecon As String = "txtMDFRecon"
    Public Const GRID_CTRL_NAME_CessionLoss As String = "txtCessionLoss"
    Public Const GRID_CTRL_NAME_Button_Edit As String = "EditButton_WRITE"
    Public Const GRID_CTRL_NAME_Button_Delete As String = "DeleteButton_WRITE"
    Public Const GRID_CTRL_NAME_Button_RUN_INVOICE As String = "BtnRunInvoice_WRITE"

    Public Enum PageAction
        None = 0
        AddNew = 1
        EditExisting = 2
        Delete = 3
    End Enum

    Private listAmountsToSave As Collections.Generic.List(Of AfaInvoiceManaulData)
    Private savedAccountingMonth As String
#End Region

#Region "Page State"
    Class MyState
        ' Selected Item Information
        Public SearchDV As DataView = Nothing
        Public searchDealerID As Guid
        Public searchPeriodYear As String
        Public searchPeriodMonth As String
        Public myBO As AfaInvoiceManaulData
        Public gridAction As PageAction

        Sub New()
            'gridAction = PageAction.None 'init value
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
        Else
            CheckIfComingFromDeleteConfirm()
        End If
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub
#End Region

#Region "Helper functions"
    Protected Sub CheckIfComingFromDeleteConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso State.gridAction = PageAction.Delete Then
            If confResponse = MSG_VALUE_YES Then
                DeleteAmounts()
                Grid.EditIndex = -1
                State.SearchDV = Nothing
                PopulateGrid()
            End If
            'Clean after consuming the action
            State.gridAction = PageAction.None
            SetControlState()
            HiddenSaveChangesPromptResponse.Value = ""
        End If        
    End Sub

    Private Sub PopulateDropdowns()
        'Me.BindCodeNameToListControl(ddlDealer, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, True, "Code"), , , , False)
        Dim oDealerList = GetDealerListByCompanyForUser()
        Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                           Return li.ExtendedCode + " - " + li.Translation + " " + "(" + li.Code + ")"
                                                                       End Function
        ddlDealer.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .TextFunc = dealerTextFunc
                                           })
        Dim intYear As Integer = DateTime.Today.Year
        'ddlAcctPeriodYear.Items.Add(New ListItem("", "0000"))
        For i As Integer = (intYear - 7) To intYear
            ddlAcctPeriodYear.Items.Add(New System.Web.UI.WebControls.ListItem(i.ToString, i.ToString))
        Next
        ddlAcctPeriodYear.SelectedValue = intYear.ToString

        Dim monthName As String
        ddlAcctPeriodMonth.Items.Add(New System.Web.UI.WebControls.ListItem("", ""))
        For month As Integer = 1 To 12
            monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month)
            ddlAcctPeriodMonth.Items.Add(New System.Web.UI.WebControls.ListItem(monthName, month.ToString().PadLeft(2, CChar("0"))))
        Next

        'Dim strMonth As String = "0" & DateTime.Today.Month.ToString
        'strMonth = strMonth.Substring(strMonth.Length - 2)
        ddlAcctPeriodMonth.SelectedValue = ""
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
                State.SearchDV = AfaInvoiceManaulData.getListByDealer(State.searchDealerID, State.searchPeriodYear, State.searchPeriodMonth)
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


    Private Function ValidateChangesBeforeSave() As Boolean
        Dim blnResult As Boolean = True
        '1 validate a not duplicate if add new
        If State.gridAction = PageAction.AddNew Then
            Dim dv As DataView = AfaInvoiceManaulData.getListByDealer(listAmountsToSave(0).DealerId, listAmountsToSave(0).InvoiceMonth.Substring(0, 4), listAmountsToSave(0).InvoiceMonth.Substring(4, 2))
            If dv.Count > 0 Then
                blnResult = False
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("Accounting_Period") + ": " + TranslationBase.TranslateLabelOrMessage("YOU_ENTER_A_DUPLICATED_MOTH"))
            End If
        End If
        Return blnResult
    End Function

    Private Function PopulateBOFromForm() As Boolean
        Dim blnSuccess As Boolean = True
        Dim strTemp As String, acctPeriod As String, dblMDFAmunt As Decimal, dblCessionLossAmt As Decimal

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

        Dim txt As TextBox = CType(Grid.Rows(ind).FindControl(GRID_CTRL_NAME_MDFRecon), TextBox)
        strTemp = txt.Text.Trim
        If strTemp = String.Empty Then
            blnSuccess = False
            MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("MDF_RECON") + ": " + TranslationBase.TranslateLabelOrMessage("REQUIRED_FIELDS_ARE_MISSING"))
        Else
            If Not Decimal.TryParse(strTemp, dblMDFAmunt) Then
                'numric error
                blnSuccess = False
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("MDF_RECON") + ": " + TranslationBase.TranslateLabelOrMessage("INVALID_AMOUNT_ENTERED"))
            End If
        End If
        txt = Nothing

        txt = CType(Grid.Rows(ind).FindControl(GRID_CTRL_NAME_CessionLoss), TextBox)
        strTemp = txt.Text.Trim
        If strTemp = String.Empty Then
            blnSuccess = False
            MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("Cession_Loss") + ": " + TranslationBase.TranslateLabelOrMessage("REQUIRED_FIELDS_ARE_MISSING"))
        Else
            If Not Decimal.TryParse(strTemp, dblCessionLossAmt) Then
                'numric error
                blnSuccess = False
                MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("Cession_Loss") + ": " + TranslationBase.TranslateLabelOrMessage("INVALID_AMOUNT_ENTERED"))
            End If
        End If
        txt = Nothing

        If blnSuccess Then
            If State.gridAction = PageAction.AddNew Then 'add new
                listAmountsToSave = New Collections.Generic.List(Of AfaInvoiceManaulData)
                Dim obj As AfaInvoiceManaulData
                'save the MDF Amount
                obj = New AfaInvoiceManaulData
                obj.DealerId = State.myBO.DealerId
                obj.InvoiceMonth = acctPeriod
                obj.AmountTypeCode = "MDF"
                obj.DataAmount = dblMDFAmunt
                listAmountsToSave.Add(obj)
                obj = Nothing

                'save the cession loss Amount
                obj = New AfaInvoiceManaulData
                obj.DealerId = State.myBO.DealerId
                obj.InvoiceMonth = acctPeriod
                obj.AmountTypeCode = "CESSLOSS"
                obj.DataAmount = dblCessionLossAmt
                listAmountsToSave.Add(obj)
            ElseIf State.gridAction = PageAction.EditExisting Then
                listAmountsToSave = AfaInvoiceManaulData.getDealerMonthlyRecords(State.myBO.DealerId, acctPeriod)
                For Each obj As AfaInvoiceManaulData In listAmountsToSave
                    If obj.AmountTypeCode = "MDF" Then
                        obj.DataAmount = dblMDFAmunt
                    ElseIf obj.AmountTypeCode = "CESSLOSS" Then
                        obj.DataAmount = dblCessionLossAmt
                    End If
                Next
            End If
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

            'save the amounts
            For Each obj As AfaInvoiceManaulData In listAmountsToSave
                obj.SaveWithoutCheckDSCreator()
            Next

            MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)

            State.searchPeriodYear = savedAccountingMonth.Substring(0, 4)
            State.searchPeriodMonth = savedAccountingMonth.Substring(4, 2)

            Return True
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Function

    Private Sub DeleteAmounts()
        Try
            listAmountsToSave = AfaInvoiceManaulData.getDealerMonthlyRecords(State.myBO.DealerId, State.myBO.InvoiceMonth)
            For Each obj As AfaInvoiceManaulData In listAmountsToSave
                obj.Delete()
                obj.SaveWithoutCheckDSCreator()
            Next
            MasterPage.MessageController.AddSuccess(Message.DELETE_RECORD_CONFIRMATION)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Button events handlers"
    Protected Sub btnClear_Click(sender As Object, e As EventArgs) Handles btnClear.Click
        Try
            'set dropdowns to default values
            ddlDealer.SelectedIndex = -1
            ddlAcctPeriodYear.SelectedValue = DateTime.Today.Year.ToString
            ddlAcctPeriodMonth.SelectedValue = ""

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

    Private Sub btnNew_Click(sender As Object, e As System.EventArgs) Handles btnNew.Click
        Try           
            State.gridAction = PageAction.AddNew
            AddNew()
            SetControlState()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub AddNew()

        If State.SearchDV Is Nothing Then
            Grid.EditIndex = 0
        Else
            Grid.EditIndex = State.SearchDV.Count
        End If

        If State.myBO Is Nothing OrElse State.myBO.IsNew = False Then
            State.myBO = New AfaInvoiceManaulData
            State.myBO.DealerId = New Guid(ddlDealer.SelectedValue)
            State.myBO.InvoiceMonth = DateTime.Today.Year.ToString & DateTime.Today.Month.ToString.PadLeft(2, CChar("0"))

            State.myBO.AddEmptyRowToSearchDV(State.SearchDV, State.myBO)
        End If
        PopulateGrid()
        'Set focus on the Code TextBox for the EditItemIndex row
        'Me.SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.productID, Me.Grid, _
        'ControlMgr.DisableEditDeleteGridIfNotEditAuth(Me, Grid)
        'SetGridControls(Me.Grid, False)
    End Sub

    Private Sub SetControlState()
        If (State.gridAction = PageAction.AddNew OrElse State.gridAction = PageAction.EditExisting) Then
            ControlMgr.SetVisibleControl(Me, btnNew, False)
            ControlMgr.SetEnableControl(Me, btnSearch, False)
            ControlMgr.SetEnableControl(Me, btnClear, False)
            MenuEnabled = False            
        Else
            ControlMgr.SetVisibleControl(Me, btnNew, True)
            ControlMgr.SetEnableControl(Me, btnSearch, True)
            ControlMgr.SetEnableControl(Me, btnClear, True)
            MenuEnabled = True           
        End If
    End Sub
#End Region


#Region "Handle grid"
    Private Sub grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
        Try
            Select Case e.CommandName.ToString()
                Case "EditAction"
                    Dim index As Integer = CInt(e.CommandArgument)
                    Grid.EditIndex = index
                    Grid.SelectedIndex = index
                    State.gridAction = PageAction.EditExisting
                    State.myBO = New AfaInvoiceManaulData
                    State.myBO.DealerId = State.searchDealerID
                    State.myBO.InvoiceMonth = CType(Grid.Rows(index).Cells(1).FindControl("lblAcctPeriod"), Label).Text                    
                    PopulateGrid()
                    SetControlState()
                Case "DeleteRecord"
                    State.gridAction = PageAction.Delete
                    State.myBO = New AfaInvoiceManaulData
                    State.myBO.DealerId = State.searchDealerID
                    State.myBO.InvoiceMonth = e.CommandArgument.ToString
                    'DeleteAmounts()
                    'Grid.EditIndex = -1
                    'State.SearchDV = Nothing
                    'PopulateGrid()
                    'State.gridAction = PageAction.None
                    'SetControlState()
                    DisplayMessage(Message.DELETE_RECORD_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
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
                        Grid.EditIndex = -1
                        State.SearchDV = Nothing
                        State.gridAction = PageAction.None
                        PopulateGrid()
                        State.myBO = Nothing
                        SetControlState()
                    End If
                Case "RunInvoice"
                    Dim Result As Boolean
                    Try
                        Result = AfaInvoiceManaulData.StartInvoiceProcess(State.searchDealerID, e.CommandArgument.ToString)

                        If Result Then
                            MasterPage.MessageController.AddSuccess("PRCESS_RUN_SUCCESSFULLY", True)
                        Else
                            MasterPage.MessageController.AddErrorAndShow("PRCESS_RUN_FAILED", True)
                        End If

                    Catch ex As Exception
                        HandleErrors(ex, MasterPage.MessageController)
                    End Try
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
            Dim objDDL As DropDownList, strTemp As String

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                With e.Row
                    If .RowIndex = Grid.EditIndex Then
                        'OrElse (State.gridAction = PageAction.EditExisting AndAlso State.myBO.InvoiceMonth = dvRow("invoice_month").ToString) Then
                        objDDL = CType(e.Row.FindControl(GRID_CTRL_NAME_DEALER), DropDownList)
                        If objDDL IsNot Nothing Then
                            ' Me.BindCodeNameToListControl(objDDL, LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, True, "Code"), , , , False)
                            Dim oDealerList = GetDealerListByCompanyForUser()
                            Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                               Return li.ExtendedCode + " " + "(" + li.Code + ")"
                                                                                           End Function
                            objDDL.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .TextFunc = dealerTextFunc
                                           })
                            SetSelectedItem(objDDL, New Guid(CType(dvRow("dealer_id"), Byte())))
                            objDDL.Enabled = False
                        End If
                        objDDL = Nothing

                        objDDL = CType(e.Row.FindControl(GRID_CTRL_NAME_AcctPeriod_Year), DropDownList)
                        If objDDL IsNot Nothing Then
                            Dim intYear As Integer = DateTime.Today.Year
                            For i As Integer = (intYear - 7) To intYear
                                objDDL.Items.Add(New System.Web.UI.WebControls.ListItem(i.ToString, i.ToString))
                            Next
                            SetSelectedItem(objDDL, dvRow("invoice_month").ToString.Substring(0, 4))
                            If State.gridAction = PageAction.EditExisting Then
                                objDDL.Enabled = False
                            End If
                        End If
                        objDDL = Nothing

                        objDDL = CType(e.Row.FindControl(GRID_CTRL_NAME_AcctPeriod_Month), DropDownList)
                        If objDDL IsNot Nothing Then
                            Dim monthName As String
                            For month As Integer = 1 To 12
                                monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month)
                                objDDL.Items.Add(New System.Web.UI.WebControls.ListItem(monthName, month.ToString().PadLeft(2, CChar("0"))))
                            Next
                            SetSelectedItem(objDDL, dvRow("invoice_month").ToString.Substring(4))
                            If State.gridAction = PageAction.EditExisting Then
                                objDDL.Enabled = False
                            End If
                        End If
                        objDDL = Nothing

                        Dim objtxt As TextBox
                        objtxt = CType(e.Row.FindControl(GRID_CTRL_NAME_MDFRecon), TextBox)
                        If objtxt IsNot Nothing Then
                            objtxt.Text = dvRow("MDFReconAmount").ToString
                        End If
                        objtxt = Nothing

                        objtxt = CType(e.Row.FindControl(GRID_CTRL_NAME_CessionLoss), TextBox)
                        If objtxt IsNot Nothing Then
                            objtxt.Text = dvRow("CessionLossAmount").ToString
                        End If
                        objtxt = Nothing
                    Else
                        'disable edit and delete button if accounting file already posted
                        Dim objbtn As ImageButton
                        objbtn = CType(e.Row.FindControl(GRID_CTRL_NAME_Button_Edit), ImageButton)
                        If objbtn IsNot Nothing Then
                            If dvRow("SSGLFilePosted").ToString = "N" Then
                                objbtn.Visible = True
                                objbtn.Enabled = True
                            Else
                                objbtn.Visible = False
                                objbtn.Enabled = False
                            End If
                        End If
                        objbtn = Nothing

                        Dim currentPeriod As Integer, accountingPeriod As Integer
                        strTemp = DateTime.Today.AddMonths(-1).Year.ToString & DateTime.Today.AddMonths(-1).Month.ToString.PadLeft(2, CChar("0"))
                        If Integer.TryParse(strTemp, currentPeriod) _
                                    AndAlso Integer.TryParse(dvRow("invoice_month").ToString(), accountingPeriod) Then

                            objbtn = CType(e.Row.FindControl(GRID_CTRL_NAME_Button_Delete), ImageButton)
                            If objbtn IsNot Nothing Then
                                objbtn.Visible = False
                                objbtn.Enabled = False
                                If dvRow("SSGLFilePosted").ToString = "N" Then
                                    'only allow deletion for future date
                                    If accountingPeriod > currentPeriod Then
                                        objbtn.Visible = True
                                        objbtn.Enabled = True
                                    End If
                                End If
                            End If
                            objbtn = Nothing

                            Dim objBtnInv As Button
                            objBtnInv = CType(e.Row.FindControl(GRID_CTRL_NAME_Button_RUN_INVOICE), Button)
                            If objBtnInv IsNot Nothing Then
                                If State.gridAction = PageAction.AddNew OrElse State.gridAction = PageAction.EditExisting Then
                                    objBtnInv.Visible = False
                                Else
                                    objBtnInv.Visible = True
                                    objBtnInv.Enabled = False
                                    If accountingPeriod = currentPeriod AndAlso dvRow("SSGLFilePosted").ToString = "N" Then
                                        'only allow run invoice for current month if the accounting file has not been posted yet
                                        'objBtnInv.Visible = True
                                        objBtnInv.Enabled = True
                                        objBtnInv.Text = TranslationBase.TranslateLabelOrMessage(objBtnInv.Text)
                                    End If
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