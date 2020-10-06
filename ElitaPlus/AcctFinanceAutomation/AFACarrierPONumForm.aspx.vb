Imports Assurant.ElitaPlus.DALObjects
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms



Public Class AFACarrierPONumForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "AFACarrierPONumForm.aspx"
    Public Const PAGETITLE As String = "CARRIER_PO_NUMBER"
    Public Const PAGETAB As String = "FINANCE_AUTOMATION"

    Public Const GRID_CTRL_NAME_DEALER As String = "ddlGridDealer"
    Public Const GRID_CTRL_NAME_AcctPeriod_Year As String = "ddlGridAcctPeriodYear"
    Public Const GRID_CTRL_NAME_AcctPeriod_Month As String = "ddlGridAcctPeriodMonth"
    Public Const GRID_CTRL_NAME_PONumber As String = "txtPONumber"
    Public Const GRID_CTRL_NAME_Record_ID As String = "lblPONumberID"
    Public Const GRID_CTRL_NAME_Button_Edit As String = "EditButton_WRITE"
    Public Const GRID_CTRL_NAME_Button_Delete As String = "DeleteButton_WRITE"

    Private Const GRID_COL_PONUM_ID_IDX As Integer = 0
    Private Const GRID_COL_DEALER_IDX As Integer = 1
    Private Const GRID_COL_EFFECTIVE_IDX As Integer = 2
    Private Const GRID_COL_PONUM_IDX As Integer = 3
    Private Const GRID_COL_BUTTON_IDX As Integer = 4

    Public Enum PageAction
        None = 0
        AddNew = 1
        EditExisting = 2
        Delete = 3
    End Enum

    'Private listAmountsToSave As Collections.Generic.List(Of AfaInvoiceManaulData)
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
        Public myBO As AfaInvoiceManaulData
        Public gridAction As PageAction = PageAction.None
        Public PONumID As Guid

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
        Else
            CheckIfComingFromDeleteConfirm()
        End If
        'Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

#End Region

#Region "Helper functions"
    Protected Sub CheckIfComingFromDeleteConfirm()
        Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
        If confResponse IsNot Nothing AndAlso State.gridAction = PageAction.Delete Then
            If confResponse = MSG_VALUE_YES Then
                DeletePONumber()
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
        '  Me.BindCodeNameToListControl(ddlDealer, DealerList, , , , False)
        Dim oDealerList = GetDealerListByCompanyForUser()
        Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                           Return li.ExtendedCode + " - " + li.Translation + " " + "(" + li.Code + ")"
                                                                       End Function
        ddlDealer.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .TextFunc = dealerTextFunc
                                           })

        Dim intYear As Integer = DateTime.Today.Year
        ddlAcctPeriodYear.Items.Add(New System.Web.UI.WebControls.ListItem("", "0000"))
        For i As Integer = (intYear - 7) To intYear + 1
            ddlAcctPeriodYear.Items.Add(New System.Web.UI.WebControls.ListItem(i.ToString, i.ToString))
        Next
        'ddlAcctPeriodYear.SelectedValue = intYear.ToString

        Dim monthName As String
        ddlAcctPeriodMonth.Items.Add(New System.Web.UI.WebControls.ListItem("", "00"))
        For month As Integer = 1 To 12
            monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month)
            ddlAcctPeriodMonth.Items.Add(New System.Web.UI.WebControls.ListItem(monthName, month.ToString().PadLeft(2, CChar("0"))))
        Next

        ddlAcctPeriodYear.SelectedValue = "0000"
        ddlAcctPeriodMonth.SelectedValue = "00"
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
                State.SearchDV = AfaInvoiceManaulData.getPONumberListByDealer(State.searchDealerID, State.searchPeriodYear & State.searchPeriodMonth)
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
        Dim objList As Collections.Generic.List(Of AfaInvoiceManaulData), objTemp As AfaInvoiceManaulData

        With State.myBO
            objList = AfaInvoiceManaulData.GetListByType(.DealerId, .InvoiceMonth, "PONUM")
            For Each objTemp In objList
                If objTemp.Id <> .Id Then 'found duplicate
                    blnResult = False
                    MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("Accounting_Period") + ": " + TranslationBase.TranslateLabelOrMessage("YOU_ENTER_A_DUPLICATED_MOTH"))
                End If
            Next
        End With

        Return blnResult
    End Function

    Private Function PopulateBOFromForm() As Boolean
        Dim blnSuccess As Boolean = True
        Dim strTemp As String, acctPeriod As String, strPONum As String, guidDealerID As Guid

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

        ddl = CType(Grid.Rows(ind).FindControl(GRID_CTRL_NAME_DEALER), DropDownList)
        strTemp = ddl.SelectedValue
        If strTemp = Guid.Empty.ToString OrElse strTemp = String.Empty Then
            blnSuccess = False
            MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("DEALER") + ": " + TranslationBase.TranslateLabelOrMessage("REQUIRED_FIELDS_ARE_MISSING"))
        Else
            guidDealerID = New Guid(strTemp)
        End If
        ddl = Nothing

        Dim txt As TextBox = CType(Grid.Rows(ind).FindControl(GRID_CTRL_NAME_PONumber), TextBox)
        strPONum = txt.Text.Trim
        If strPONum = String.Empty Then
            blnSuccess = False
            MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage("PO_NUMBER") + ": " + TranslationBase.TranslateLabelOrMessage("REQUIRED_FIELDS_ARE_MISSING"))
        End If
        txt = Nothing

        If blnSuccess Then
            With State.myBO
                .DealerId = guidDealerID
                .InvoiceMonth = acctPeriod
                .DataText = strPONum
                .AmountTypeCode = "PONUM"
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
            State.myBO.Save()                        
            MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION)

            Return True
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Function

    Private Sub DeletePONumber()
        Try
            State.myBO.Delete()
            State.myBO.Save()
            State.myBO = Nothing
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
            ddlAcctPeriodYear.SelectedIndex = -1
            ddlAcctPeriodMonth.SelectedIndex = -1
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

    Private Sub btnNew_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnNew_WRITE.Click
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

        State.myBO = New AfaInvoiceManaulData
        State.myBO.DealerId = New Guid(ddlDealer.SelectedValue)
        State.myBO.InvoiceMonth = DateTime.Today.Year.ToString & DateTime.Today.Month.ToString.PadLeft(2, CChar("0"))
        State.myBO.AddEmptyRowToPONumSearchDV(State.SearchDV, State.myBO)

        PopulateGrid()        
    End Sub

    Private Sub SetControlState()
        If (State.gridAction = PageAction.AddNew OrElse State.gridAction = PageAction.EditExisting) Then
            ControlMgr.SetVisibleControl(Me, btnNew_WRITE, False)
            ControlMgr.SetEnableControl(Me, btnSearch, False)
            ControlMgr.SetEnableControl(Me, btnClear, False)
            MenuEnabled = False
        Else
            ControlMgr.SetVisibleControl(Me, btnNew_WRITE, True)
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
                Case "EditAction"
                    index = CInt(e.CommandArgument)
                    State.PONumID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_PONUM_ID_IDX).FindControl(GRID_CTRL_NAME_Record_ID), Label).Text)

                    Grid.EditIndex = index
                    Grid.SelectedIndex = index
                    State.gridAction = PageAction.EditExisting
                    State.myBO = New AfaInvoiceManaulData(State.PONumID)
                    PopulateGrid()
                    SetControlState()
                Case "DeleteRecord"
                    index = CInt(e.CommandArgument)
                    State.PONumID = New Guid(CType(Grid.Rows(index).Cells(GRID_COL_PONUM_ID_IDX).FindControl(GRID_CTRL_NAME_Record_ID), Label).Text)
                    State.gridAction = PageAction.Delete
                    State.myBO = New AfaInvoiceManaulData(State.PONumID)
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
                        State.searchDealerID = State.myBO.DealerId
                        State.searchPeriodMonth = State.myBO.InvoiceMonth.Substring(4)
                        State.searchPeriodYear = State.myBO.InvoiceMonth.Substring(0, 4)
                        State.SearchDV = Nothing
                        State.myBO = Nothing
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
            Dim objDDL As DropDownList, strTemp As String

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                With e.Row
                    If .RowIndex = Grid.EditIndex Then
                        'OrElse (State.gridAction = PageAction.EditExisting AndAlso State.myBO.InvoiceMonth = dvRow("invoice_month").ToString) Then
                        objDDL = CType(e.Row.FindControl(GRID_CTRL_NAME_DEALER), DropDownList)
                        If objDDL IsNot Nothing Then
                            ' Me.BindCodeNameToListControl(objDDL, DealerList, , , , True)
                            Dim oDealerList = GetDealerListByCompanyForUser()
                            Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                               Return li.ExtendedCode + " - " + li.Translation + " " + "(" + li.Code + ")"
                                                                                           End Function
                            objDDL.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .TextFunc = dealerTextFunc,
                                            .AddBlankItem = True
                                           })
                            If Not State.myBO.IsNew Then
                                SetSelectedItem(objDDL, State.myBO.DealerId)
                            End If
                            ' disable the dealer if edit existing
                            If State.gridAction = PageAction.AddNew Then
                                objDDL.Enabled = True
                            Else
                                objDDL.Enabled = False
                            End If
                        End If
                        objDDL = Nothing

                        objDDL = CType(e.Row.FindControl(GRID_CTRL_NAME_AcctPeriod_Year), DropDownList)
                        If objDDL IsNot Nothing Then
                            Dim intYear As Integer = DateTime.Today.Year
                            For i As Integer = (intYear - 7) To intYear + 1
                                objDDL.Items.Add(New System.Web.UI.WebControls.ListItem(i.ToString, i.ToString))
                            Next
                            SetSelectedItem(objDDL, State.myBO.InvoiceMonth.Substring(0, 4))                            
                        End If
                        objDDL = Nothing

                        objDDL = CType(e.Row.FindControl(GRID_CTRL_NAME_AcctPeriod_Month), DropDownList)
                        If objDDL IsNot Nothing Then
                            Dim monthName As String
                            For month As Integer = 1 To 12
                                monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month)
                                objDDL.Items.Add(New System.Web.UI.WebControls.ListItem(monthName, month.ToString().PadLeft(2, CChar("0"))))
                            Next
                            SetSelectedItem(objDDL, State.myBO.InvoiceMonth.Substring(4))
                        End If
                        objDDL = Nothing

                        Dim objtxt As TextBox
                        objtxt = CType(e.Row.FindControl(GRID_CTRL_NAME_PONumber), TextBox)
                        If objtxt IsNot Nothing Then
                            objtxt.Text = dvRow("PONumber").ToString
                        End If
                        objtxt = Nothing                    
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