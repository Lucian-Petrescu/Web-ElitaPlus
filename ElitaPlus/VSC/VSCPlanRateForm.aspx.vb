Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Public Class VSCPlanRateForm
    Inherits ElitaPlusSearchPage

#Region "Constants"

    Public Const URL As String = "VSC/VSCPlanRateForm.aspx"
    Public Const PAGETITLE As String = "VSC_PLAN_RATE"
    Public Const PAGETAB As String = "TABLES"

    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const GRID_COL_EDIT_IDX As Integer = 1
    Private Const GRID_COL_VERSIONRECORDID_IDX As Integer = 2
    Private Const GRID_COL_PLAN_IDX As Integer = 3
    Private Const GRID_COL_DEALERGROUP_IDX As Integer = 4
    Private Const GRID_COL_DEALER_IDX As Integer = 5
    Private Const GRID_COL_EFFECTIVEDATE_IDX As Integer = 7 'PBI 554831 changes
    Private Const GRID_COL_EXPIRATIONDATE_IDX As Integer = 8

    Private Const GRID_CONTROL_NAME_EXPIRATIONDATE As String = "txtExpirationDate"
    Private Const GRID_CONTROL_NAME_Calendar As String = "btnExpirationDate"
    'START PBI 554831 changes
    Private Const GRID_CONTROL_NAME_EFFECTIVEDATE As String = "txtEffectiveDate"
    Private Const GRID_CONTROL_NAME_EFFECTIVEDATE_Calendar As String = "btnEffectiveDate"
    'END
    Private Const GRID_CONTROL_NAME_VERSIONID As String = "RateVersionId"
    Private Const GRID_CONTROL_NAME_PLAN As String = "lblGridPlan"
    Private Const GRID_CONTROL_NAME_DEALERGROUP As String = "lblGridDEALERGROUP"
    Private Const GRID_CONTROL_NAME_DEALERCODE As String = "lblGridDEALERCODE"
    Private Const GRID_CONTROL_NAME_DEALERNAME As String = "lblGridDEALERNAME"

    Private Const GRID_NO_SELECTEDITEM_INX As Integer = -1

    Private Const COL_VERSION As String = "Version"
    Private Const COL_HIGHEST_VERSION As String = "HighestVersion"
    Private Const COL_PLAN As String = "PLAN"
    Private Const COL_DEALERNAME As String = "DealerName"
    Private Const COL_DEALERCODE As String = "DealerCode"
    Private Const COL_DEALERGROUP As String = "DealerGroup"
    Private Const COL_DEALERGROUPCODE As String = "DEALERGROUPCODE"

    Private Const MSG_CONFIRM_PROMPT As String = "MSG_CONFIRM_PROMPT"
    Private Const MSG_RECORD_SAVED_OK As String = "MSG_RECORD_SAVED_OK"
    Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"

#End Region

#Region "Properties"
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the search page.
    Class MyState
        Public MyBO As VSCRateVersion
        Public EditRowNum As Integer
        Public RateVersionID As Guid
        Public PageIndex As Integer = 0
        Public IsGridVisible As Boolean
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public searchDV As DataView = Nothing
        Public HasDataChanged As Boolean
        Public IsEditMode As Boolean = False
        Public bDealer As Boolean = False
        Public bDealerGroup As Boolean = False
        Public bCompanyGroup As Boolean = False
        Public searchByCode As String
        Public searchByName As String
        Public planCodeId As Guid
        Public planDescId As Guid
        Public effectiveDate As Date
        Public versionNumber As String
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

#Region "State-Management"
    Private Sub SetSession()
        With State
            .PageIndex = Grid.CurrentPageIndex
            .PageSize = Grid.PageSize
        End With
    End Sub

    Private Sub GetSession()
        With State
            Grid.PageSize = .PageSize
            cboPageSize.SelectedValue = CType(.PageSize, String)
        End With
    End Sub
#End Region

#Region "Page Return"

#End Region

#Region "Page Events"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ErrControllerMaster.Clear_Hide()
        Try
            If Not IsPostBack Then
                SetDefaultButton(rdoDealer, btnSearch)
                SetDefaultButton(rdoDealerGroup, btnSearch)
                SetDefaultButton(rdoCompanyGroup, btnSearch)
                SetDefaultButton(rdoHighestVersionOnly, btnSearch)
                SetDefaultButton(rdoAllVersion, btnSearch)
                SetDefaultButton(ddlPlanCode, btnSearch)
                SetDefaultButton(ddlPlanDesc, btnSearch)

                SetDefaultButton(txtDealerCode, btnSearch)
                SetDefaultButton(txtDealerName, btnSearch)
                SetDefaultButton(txtEffectiveDate, btnSearch)
                SetDefaultButton(txtVersionNumber, btnSearch)

                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                AddCalendar(btnEffectiveDate, txtEffectiveDate)
                SetFocus(txtDealerCode)

                populateSearchControls()

                If IsReturningFromChild Then
                    cboPageSize.SelectedValue = CType(State.PageSize, String)
                    Grid.PageSize = State.PageSize
                    PopulateGrid()
                End If
            End If
            AddJavascript()
            'txtEffectiveDate.Attributes.Add("readonly", "readonly")

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        ShowMissingTranslations(ErrControllerMaster)
    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        MenuEnabled = True
        IsReturningFromChild = True
        Dim retObj As vscPlanCoverageForm.ReturnType = CType(ReturnPar, vscPlanCoverageForm.ReturnType)
        State.HasDataChanged = retObj.HasDataChanged
        Select Case retObj.LastOperation
            Case ElitaPlusPage.DetailPageCommand.Back
                If retObj IsNot Nothing Then
                    State.IsGridVisible = True
                End If
        End Select
    End Sub

    Private Sub Page_LoadComplete(sender As System.Object, e As System.EventArgs) Handles MyBase.LoadComplete
        EnableDisableFields()
    End Sub
#End Region

#Region "Helper functions"
    'Private Sub populatePlanDDL()
    '    Dim dv As DataView = LookupListNew.GetVSCPlanLookupList(Authentication.CurrentUser.CompanyGroup.Id)

    '    dv.Sort = "CODE"
    '    BindListControlToDataView(Me.ddlPlanCode, dv, "CODE", "ID", True)
    '    dv.Sort = "DESCRIPTION"
    '    BindListControlToDataView(Me.ddlPlanDesc, dv, "DESCRIPTION", "ID", True)
    '    Me.ddlPlanDesc.Attributes.Add("onchange", "UpdateList('" & Me.ddlPlanCode.ClientID & "')")
    '    Me.ddlPlanCode.Attributes.Add("onchange", "UpdateList('" & Me.ddlPlanDesc.ClientID & "')")
    'End Sub

    Private Sub populateSearchControls()
        Try
            'populatePlanDDL()

            'Dim dv As DataView = LookupListNew.GetVSCPlanLookupList(Authentication.CurrentUser.CompanyGroup.Id)
            'dv.Sort = "CODE"
            'BindListControlToDataView(Me.ddlPlanCode, dv, "CODE", "ID", True)
            'dv.Sort = "DESCRIPTION"
            'BindListControlToDataView(Me.ddlPlanDesc, dv, "DESCRIPTION", "ID", True)

            Dim Plans As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.VscPlanByCompanyGroup,
                                                                context:=New ListContext() With
                                                                {
                                                                  .CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                                                                })

            ddlPlanCode.Populate(Plans.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True,
                                        .TextFunc = AddressOf .GetCode,
                                        .SortFunc = AddressOf .GetCode
                                    })

            ddlPlanDesc.Populate(Plans.ToArray(),
                                    New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })

            ddlPlanDesc.Attributes.Add("onchange", "UpdateList('" & ddlPlanCode.ClientID & "')")
            ddlPlanCode.Attributes.Add("onchange", "UpdateList('" & ddlPlanDesc.ClientID & "')")

            If IsReturningFromChild Then
                With State
                    rdoDealer.Checked = .bDealer
                    rdoDealerGroup.Checked = .bDealerGroup
                    rdoCompanyGroup.Checked = .bCompanyGroup
                    txtDealerName.Text = .searchByName
                    txtDealerCode.Text = .searchByCode
                    ElitaPlusPage.BindSelectItem(.planCodeId.ToString, ddlPlanCode)
                    ElitaPlusPage.BindSelectItem(.planDescId.ToString, ddlPlanDesc)

                    If Not .effectiveDate = #12:00:00 AM# Then
                        PopulateControlFromBOProperty(txtEffectiveDate, .effectiveDate)
                    End If

                    PopulateControlFromBOProperty(txtVersionNumber, .versionNumber)
                End With
            End If
        Catch ex As Exception
            ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
        End Try
    End Sub

    Private Sub AddJavascript()
        Dim strJS As System.Text.StringBuilder = New System.Text.StringBuilder()
        strJS.Append("<script type=""text/javascript"">")
        strJS.Append(Environment.NewLine)
        strJS.Append(" function DisableDealer(blnEnabled) {")
        'strJS.Append(Environment.NewLine)
        'strJS.Append("alert('blnEnabled:' + blnEnabled);")
        strJS.Append(Environment.NewLine)
        strJS.Append("var objDC = document.getElementById('")
        strJS.Append(txtDealerCode.ClientID)
        strJS.Append("'); objDC.disabled = (!blnEnabled);")
        strJS.Append(Environment.NewLine)
        strJS.Append("var objDN = document.getElementById('")
        strJS.Append(txtDealerName.ClientID)
        strJS.Append("'); objDN.disabled = (!blnEnabled);")
        strJS.Append(Environment.NewLine)
        strJS.Append("if (!blnEnabled){objDC.value = ''; objDN.value = '';}")
        strJS.Append("}")
        strJS.Append(Environment.NewLine)
        strJS.Append("</script>")
        strJS.Append(Environment.NewLine)

        Page.RegisterClientScriptBlock("SearchDealerOrGroup", strJS.ToString())
    End Sub

    Private Sub PopulateGrid()

        If State.searchDV Is Nothing Then SearchVSCRateVersion()

        Grid.AutoGenerateColumns = False

        SetPageAndSelectedIndexFromGuid(State.searchDV, State.RateVersionID, Grid, State.PageIndex, (Grid.EditItemIndex > GRID_NO_SELECTEDITEM_INX))

        State.PageIndex = Grid.CurrentPageIndex
        Grid.DataSource = State.searchDV
        Grid.DataBind()
        ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
        ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

        'Session("recCount") = Me.State.searchDV.Count
        If Grid.Visible Then
            lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If

    End Sub

    Private Sub SearchVSCRateVersion()

        Dim VSCPlanID As Guid, DealerGroupID As Guid, strEffectiveDate As String, dtEffective As Date
        Dim strCode As String = String.Empty, strName As String = String.Empty, HighestVersionOnly As Boolean = True
        Dim strVersionNumber As String, iVersionNumber As Integer
        Dim SearchBy As Assurant.ElitaPlus.DALObjects.VSCRateVersionDAL.SearchByType
        Dim objRV As VSCRateVersion = New VSCRateVersion

        VSCPlanID = GetSelectedItem(ddlPlanCode) 'Me.VSCPlanMultipleDrop.SelectedGuid
        strEffectiveDate = txtEffectiveDate.Text.Trim()
        If strEffectiveDate <> "" Then
            If Not Date.TryParse(strEffectiveDate, dtEffective) Then
                SetLabelError(lblEffectiveDate)
                Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
            End If
        End If

        strVersionNumber = txtVersionNumber.Text.Trim()
        If strVersionNumber <> "" Then
            If Not Integer.TryParse(strVersionNumber, iVersionNumber) Then
                SetLabelError(lblVersionNumber)
                Throw New GUIException(Message.MSG_INVALID_NUMBER, Message.MSG_INVALID_NUMBER)
            End If
        End If

        If rdoAllVersion.Checked Then HighestVersionOnly = False

        If rdoDealer.Checked Then
            SearchBy = DALObjects.VSCRateVersionDAL.SearchByType.Dealer
        ElseIf rdoDealerGroup.Checked Then
            SearchBy = DALObjects.VSCRateVersionDAL.SearchByType.DealerGroup
        Else
            SearchBy = DALObjects.VSCRateVersionDAL.SearchByType.CompanyGroup
        End If

        If rdoDealer.Checked OrElse rdoDealerGroup.Checked Then
            strCode = txtDealerCode.Text.Trim()
            strName = txtDealerName.Text.Trim()
        End If
        State.searchDV = objRV.getList(SearchBy, Authentication.CurrentUser.CompanyGroup.Id, VSCPlanID, strCode, strName, dtEffective, HighestVersionOnly, iVersionNumber)

        With State
            .bDealer = rdoDealer.Checked
            .bDealerGroup = rdoDealerGroup.Checked
            .bCompanyGroup = rdoCompanyGroup.Checked
            .searchByName = txtDealerName.Text
            .searchByCode = txtDealerCode.Text
            .planCodeId = ElitaPlusPage.GetSelectedItem(ddlPlanCode)
            .planDescId = ElitaPlusPage.GetSelectedItem(ddlPlanDesc)
            If txtEffectiveDate.Text IsNot Nothing AndAlso txtEffectiveDate.Text <> "" Then
                .effectiveDate = CType(txtEffectiveDate.Text, Date)
            End If
            .versionNumber = strVersionNumber

        End With

    End Sub

    Private Sub EnableDisableFields()
        If State.IsEditMode Then
            btnSave.Visible = True
            btnCancel.Visible = True
            btnClearSearch.Enabled = False
            btnSearch.Enabled = False
        Else
            btnSave.Visible = False
            btnCancel.Visible = False
            btnClearSearch.Enabled = True
            btnSearch.Enabled = True
        End If

        If rdoCompanyGroup.Checked Then
            txtDealerCode.Enabled = False
            txtDealerName.Enabled = False
        Else
            txtDealerCode.Enabled = True
            txtDealerName.Enabled = True
        End If
    End Sub

    Private Sub ReturnFromEditing()
        'update the Search results with the new date
        Dim dr As DataRow = State.searchDV.Table.Select("ROWNUM=" & State.EditRowNum)(0)
        dr("EXPIRATION_DATE") = State.MyBO.ExpirationDate.Value
        dr("EFFECTIVE_DATE") = State.MyBO.EffectiveDate.Value
        dr.AcceptChanges()
    End Sub
#End Region

#Region "Grid related"
    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Dim index As Integer
        Dim lblControl As Label, dr As DataRow
        Try
            'Find the datarow associated with the selected item
            If e.CommandName = "SelectAction" OrElse e.CommandName = "EditAction" Then
                index = e.Item.ItemIndex
                lblControl = CType(Grid.Items(index).Cells(GRID_COL_VERSIONRECORDID_IDX).FindControl(GRID_CONTROL_NAME_VERSIONID), Label)
                If Not (Integer.TryParse(lblControl.Text, State.EditRowNum)) Then State.EditRowNum = 0
                dr = State.searchDV.Table.Select("ROWNUM=" & lblControl.Text)(0)
                State.RateVersionID = New Guid(CType(dr("VSC_RATE_VERSION_ID"), Byte()))
            End If

            If e.CommandName = "SelectAction" Then
                Dim strPlan As String, strDealer As String, strDealerGroup As String
                strPlan = dr(COL_PLAN).ToString
                If dr(COL_DEALERNAME).ToString <> "" Then
                    strDealer = String.Format("{0} - {1}", dr(COL_DEALERCODE).ToString, dr(COL_DEALERNAME).ToString)
                Else
                    strDealer = ""
                End If
                If dr(COL_DEALERGROUP).ToString <> "" Then
                    strDealerGroup = String.Format("{0}- {1}", dr(COL_DEALERGROUPCODE).ToString, dr(COL_DEALERGROUP).ToString)
                Else
                    strDealerGroup = ""
                End If
                SetSession()
                Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
                callPage(vscPlanCoverageForm.URL, New vscPlanCoverageForm.Parameters(State.RateVersionID, strPlan, strDealer, strDealerGroup))
            ElseIf e.CommandName = "EditAction" Then
                State.IsEditMode = True
                Grid.EditItemIndex = index

                State.MyBO = New VSCRateVersion(State.RateVersionID)
                PopulateGrid()

                'Disable all icon buttons on the Grid
                SetGridControls(Grid, False)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        BaseItemCreated(sender, e)
    End Sub

    Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim elemType As ListItemType = e.Item.ItemType
        If elemType = ListItemType.Item OrElse elemType = ListItemType.AlternatingItem Then
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim ctr As Web.UI.Control
            'Only allow editing of expiration date for the highest version
            If dvRow(COL_HIGHEST_VERSION).ToString <> dvRow(COL_VERSION).ToString Then
                For Each ctr In e.Item.Cells(GRID_COL_EDIT_IDX).Controls
                    If ctr.GetType.Name = "ImageButton" Then
                        ctr.Visible = False
                        Exit For
                    End If
                Next
            End If
        ElseIf elemType = ListItemType.EditItem Then
            'START PBI 554831 Changes
            Dim ctrEffDtTxt As TextBox = CType(e.Item.FindControl(GRID_CONTROL_NAME_EFFECTIVEDATE), TextBox)
            Dim ctrEffDtBtn As ImageButton = CType(e.Item.FindControl(GRID_CONTROL_NAME_EFFECTIVEDATE_Calendar), ImageButton)
            If Not (ctrEffDtTxt Is Nothing OrElse ctrEffDtBtn Is Nothing) Then
                AddCalendar(ctrEffDtBtn, ctrEffDtTxt)
            End If
            'END
            Dim ctrTxt As TextBox = CType(e.Item.FindControl(GRID_CONTROL_NAME_EXPIRATIONDATE), TextBox)
            Dim ctrBtn As ImageButton = CType(e.Item.FindControl(GRID_CONTROL_NAME_Calendar), ImageButton)
            If Not (ctrTxt Is Nothing OrElse ctrBtn Is Nothing) Then
                AddCalendar(ctrBtn, ctrTxt)
                'ctrTxt.Attributes.Add("readonly", "readonly")
            End If
        End If
    End Sub

    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = e.NewPageIndex
            State.RateVersionID = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub cboPageSize_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
            Grid.CurrentPageIndex = State.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region

#Region "Button event handlers"
    Protected Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        ddlPlanCode.SelectedIndex = 0
        ddlPlanDesc.SelectedIndex = 0
        txtDealerName.Text = ""
        txtDealerCode.Text = ""
        txtEffectiveDate.Text = ""
        rdoDealer.Checked = True
        rdoDealerGroup.Checked = False
        rdoHighestVersionOnly.Checked = True
        rdoAllVersion.Checked = False
        txtVersionNumber.Text = ""
        SetFocus(txtDealerCode)
    End Sub

    Protected Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            State.PageIndex = 0
            State.RateVersionID = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            State.HasDataChanged = False
            PopulateGrid()
        Catch ex As Exception
            State.IsGridVisible = False
            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            ControlMgr.SetVisibleControl(Me, trPageSize, State.IsGridVisible)
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        State.IsEditMode = False
        Grid.EditItemIndex = GRID_NO_SELECTEDITEM_INX

        State.MyBO = Nothing
        PopulateGrid()
    End Sub

    Protected Sub btnSave_Click(sender As System.Object, e As System.EventArgs) Handles btnSave.Click
        Try
            Dim index As Integer = Grid.EditItemIndex
            Dim txtControl As TextBox = CType(Grid.Items(index).Cells(GRID_COL_EXPIRATIONDATE_IDX).FindControl(GRID_CONTROL_NAME_EXPIRATIONDATE), TextBox)
            Dim dtExpirationDate As Date
            If Not Date.TryParse(txtControl.Text, dtExpirationDate) Then
                Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
            Else
                PopulateBOProperty(State.MyBO, "ExpirationDate", txtControl)
            End If

            'START PBI 554831 changes
            Dim txtEffDtControl As TextBox = CType(Grid.Items(index).Cells(GRID_COL_EFFECTIVEDATE_IDX).FindControl(GRID_CONTROL_NAME_EFFECTIVEDATE), TextBox)
            Dim dtEffectiveDate As Date

            If Not Date.TryParse(txtEffDtControl.Text, dtEffectiveDate) Then
                Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
            ElseIf dtEffectiveDate > DateTime.Today Then
                Throw New GUIException(Message.MSG_INVALID_DATE, Message.MSG_INVALID_DATE)
            Else
                If (State.MyBO.EffectiveDate <> dtEffectiveDate) Then
                    Dim msgCode As String = VSCRateVersion.validateEffectiveDate(State.RateVersionID, dtEffectiveDate)
                    If Not String.IsNullOrWhiteSpace(msgCode) Then
                        Throw New GUIException(msgCode, msgCode)
                    End If
                End If
                PopulateBOProperty(State.MyBO, "EffectiveDate", txtEffDtControl)
            End If
            'END

            If (State.MyBO.IsDirty) Then
                State.MyBO.Save()
                AddInfoMsg(MSG_RECORD_SAVED_OK)
                ReturnFromEditing()
            Else
                AddInfoMsg(MSG_RECORD_NOT_SAVED)
            End If

            State.IsEditMode = False
            Grid.EditItemIndex = GRID_NO_SELECTEDITEM_INX

            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region

End Class