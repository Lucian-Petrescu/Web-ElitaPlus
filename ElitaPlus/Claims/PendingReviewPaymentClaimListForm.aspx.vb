Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements

Public Class PendingReviewPaymentClaimListForm
    Inherits ElitaPlusSearchPage
    'Implements IStateController

#Region "Variables"

    Private mbIsFirstPass As Boolean = True

#End Region

#Region "Class NonTransient Members"
    Protected isReturningFromDetail As Boolean = False
#End Region

#Region "Constants"
    Public Const GRID_COL_CHKBX_IDX As Integer = 0
    Public Const GRID_COL_SERVICE_CENTER_NAME_IDX As Integer = 1
    Public Const GRID_COL_COUNTRY_SERVICE_CENTER_IDX As Integer = 2
    Public Const GRID_COL_CLAIM_NUMBER_IDX As Integer = 3
    Public Const GRID_COL_SERIAL_NUMBER_IDX As Integer = 4
    Public Const GRID_COL_CLAIM_STATUS_IDX As Integer = 5
    Public Const GRID_COL_CLAIM_EXT_STATUS As Integer = 6
    Public Const GRID_COL_MAKE_IDX As Integer = 7
    Public Const GRID_COL_MODEL_IDX As Integer = 8
    Public Const GRID_COL_SKU_CLAIMED_IDX As Integer = 9
    Public Const GRID_COL_SKU_REPLACED_IDX As Integer = 10
    Public Const GRID_COL_COVERAGE_TYPE As Integer = 11
    Public Const GRID_COL_CLAIM_CREATED_DATE_IDX As Integer = 12
    Public Const GRID_COL_REPAIR_DATE As Integer = 13
    'Public Const GRID_COL_EDIT_IDX As Integer = 14
    Public Const GRID_COL_CLAIM_ID_IDX As Integer = 14

    Public Const MAX_LIMIT As Integer = 1000

    Public PendingReviewForPaymentId As Guid

    Public Const GRID_CLAIM_APPROVE_CHECKBOX As String = "chkbxtoappclaim"
    Public Const GRID_ALL_CLAIMS_CHECKBOX As String = "chkAllClaims"
    Public Const PAGETITLE As String = "R_AND_L_CLAIMS_SEARCH"
    Public Const PAGETAB As String = "Claims"
    Public Const CLAIM_EXTENDED_STATUS__PENDING_REVIEW_FOR_PAYMENT As String = "Pending Review For Payment"
    Public Const GRID_CLAIM_Number_CTRL As String = "btnclaimedit"


#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    Class MyState
        Public PageIndex As Integer = 0
        Public SortExpression As String = Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_SERVICE_CENTER_NAME
        Public selectedClaimId As Guid = Guid.Empty
        Public claimNumber As String = String.Empty
        Public serialNumber As String = String.Empty
        Public certificate As String
        Public servicecenterid As Guid
        ' Public serviceCenterName As String
        Public Countryid As Guid
        'Public make As String
        Public Manufacturerid As Guid = Guid.Empty
        Public Model As String
        Public claimstatusid As Guid = Guid.Empty
        Public claimstatus As String
        Public extclaimstatusid As Guid = Guid.Empty
        Public skuclaimed As String
        Public skureppart As String
        Public skureplaced As String
        Public coveragetypeid As Guid = Guid.Empty
        Public servicelevelid As Guid = Guid.Empty
        Public risktypeid As Guid = Guid.Empty
        Public replacementtypeid As Guid = Guid.Empty
        Public claimcreateddate As SearchCriteriaStructType(Of Date) = Nothing
        Public datesearchtype As String
        'Public enddate As String
        Public selectedSortById As Guid = Guid.Empty
        Public selectedPageSize As Int32 = 30
        Public IsGridVisible As Boolean = False
        Public searchDV As Claim.PendingReviewPaymentClaimSearchDV = Nothing
        Public SearchClicked As Boolean
        Public ActionInProgress As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Nothing_
        Public cmdProcessRecord As String = String.Empty

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


#Region "Page_Events"


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        PendingReviewForPaymentId = LookupListNew.GetIdFromCode(LookupListNew.LK_EXTENDED_CLAIM_STATUSES, Codes.CLAIM_EXTENDED_STATUS__PENDING_REVIEW_FOR_PAYMENT)
        Page.RegisterHiddenField("__EVENTTARGET", btnSearch.ClientID)
        MasterPage.MessageController.Clear()
        Try
            ' Populate the header and bredcrumb
            MasterPage.MessageController.Clear()
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            UpdateBreadCrum()
            Form.DefaultButton = btnSearch.UniqueID
            If Not IsPostBack Then
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)

                PopulateDropdowns()

                divbtns.Visible = False

                PopulateSearchFieldsFromState()
                TranslateGridHeader(Grid)
                TranslateGridControls(Grid)
                If State.IsGridVisible Then
                    If Not (State.selectedPageSize = DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                        Grid.PageSize = State.selectedPageSize
                    End If
                    PopulateGrid()
                End If
                SetGridItemStyleColor(Grid)

            End If
            DisplayNewProgressBarOnClick(btnSearch, "LOADING_R_AND_L_CLAIMS")
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub UpdateBreadCrum()

        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End If

    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        Try
            If CalledUrl = ClaimForm.URL Then
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As ClaimForm.ReturnType = CType(ReturnedValues, ClaimForm.ReturnType)
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If
            End If

            If CalledUrl = Reports.RepairLogisticsClaimsExportForm.URL Then
                MenuEnabled = True
                Dim retObj As Reports.RepairLogisticsClaimsExportForm.ReturnType = CType(ReturnPar, Reports.RepairLogisticsClaimsExportForm.ReturnType)
                'Me.State.HasDataChanged = retObj.HasDataChanged
                If retObj IsNot Nothing AndAlso retObj.HasDataChanged Then
                    State.searchDV = Nothing
                End If
                State.IsGridVisible = True

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Controlling Logic"
    Private Sub Country_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlcountry.SelectedIndexChanged
        Try
            Dim CountryId As Guid = GetSelectedItem(ddlcountry)

            If CountryId.Equals(Guid.Empty) Then
                ' Me.BindListControlToDataView(ddlservicecenter, LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries)) 'ServiceCenterListByCountry
                Dim ServiceCenterList As New Collections.Generic.List(Of DataElements.ListItem)
                For Each Country_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Countries
                    Dim ServiceCenters As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry",
                                                                    context:=New ListContext() With
                                                                    {
                                                                      .CountryId = Country_id
                                                                    })

                    If ServiceCenters.Count > 0 Then
                        If ServiceCenterList IsNot Nothing Then
                            ServiceCenterList.AddRange(ServiceCenters)
                        Else
                            ServiceCenterList = ServiceCenters.Clone()
                        End If
                    End If
                Next

                ddlservicecenter.Populate(ServiceCenterList.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })

            Else
                'Me.BindListControlToDataView(Me.ddlservicecenter, LookupListNew.GetServiceCenterLookupList(CountryId), , , True);ServiceCenterListByCountry
                Dim listcontext As ListContext = New ListContext()
                listcontext.CountryId = CountryId
                Dim serviceCenterLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ServiceCenterListByCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                ddlservicecenter.Populate(serviceCenterLkl, New PopulateOptions() With
                        {
                        .AddBlankItem = True
                        })

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub PopulateDropdowns()
        Try
            Dim LangId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

            Dim extendedStatusByGroupLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtendedStatusByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            ddlclaimextstatus.Populate(extendedStatusByGroupLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })

            Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")

            Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                                  Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                                  Select x).ToArray()

            ddlcountry.Populate(filteredCountryList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })

            If ddlcountry.Items.Count = 2 Then
                ddlcountry.SelectedIndex = 1
                State.Countryid = New Guid(ddlcountry.SelectedValue.ToString())
            End If

            Dim claimstatusLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CLSTAT", Thread.CurrentPrincipal.GetLanguageCode())
            ddlclaimstatus.Populate(claimstatusLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })

            'Dim ocoveragebycompgroup As DataView
            'ocoveragebycompgroup = LookupListNew.GetCoverageTypeByCompanyGroupLookupList(LangId, compgrpid)


            ' Me.BindListControlToDataView(Me.ddlcoveragetype, ocoveragebycompgroup, , , True)

            Dim coverageTypeByCompanyGroupLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CoverageTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            ddlcoveragetype.Populate(coverageTypeByCompanyGroupLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })

            Dim servicelevelLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("SVC_LVL", Thread.CurrentPrincipal.GetLanguageCode())
            ddlservicelevel.Populate(servicelevelLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })

            Dim replacementTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("DEVICE", Thread.CurrentPrincipal.GetLanguageCode())
            ddlReplacementType.Populate(replacementTypeLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })

            Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            ddlmake.Populate(manufacturerLkl, New PopulateOptions() With
                {
                .AddBlankItem = True
                })

            Dim RiskTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            ddlrisktype.Populate(RiskTypeLkl, New PopulateOptions() With
                {
                .AddBlankItem = True
                })

            Dim ServiceCenterList As New Collections.Generic.List(Of DataElements.ListItem)
            For Each Country_id As Guid In ElitaPlusIdentity.Current.ActiveUser.Countries
                Dim ServiceCenters As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="ServiceCenterListByCountry",
                                                                    context:=New ListContext() With
                                                                    {
                                                                      .CountryId = Country_id
                                                                    })

                If ServiceCenters.Count > 0 Then
                    If ServiceCenterList IsNot Nothing Then
                        ServiceCenterList.AddRange(ServiceCenters)
                    Else
                        ServiceCenterList = ServiceCenters.Clone()
                    End If
                End If
            Next

            ddlservicecenter.Populate(ServiceCenterList.ToArray(), New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PopulateGrid()

        Try
            PopulateStateFromSearchFields()
            Dim haserrors As Boolean = False
            If (Not moClaimCreatedDate.IsEmpty) And Not moClaimCreatedDate.Validate() Then
                Exit Sub
            Else
                State.claimcreateddate = DirectCast(moClaimCreatedDate.Value, SearchCriteriaStructType(Of Date))
            End If

            State.claimstatus = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_STATUS, State.claimstatusid)
            If State.claimstatus Is Nothing Then
                State.claimstatus = String.Empty
            End If

            If (State.searchDV Is Nothing) Then
                State.searchDV = Claim.GetPendingReviewPaymentClaimList(State.claimNumber, State.serialNumber, State.certificate, State.servicecenterid,
                                                                           State.Countryid, State.Manufacturerid, State.Model, State.skuclaimed, State.skureplaced,
                                                                            State.claimstatus, State.extclaimstatusid, State.coveragetypeid, State.servicelevelid, State.risktypeid,
                                                                            State.skureppart, State.replacementtypeid, State.claimcreateddate)


            End If

            Grid.AutoGenerateColumns = False


            Grid.PageSize = State.selectedPageSize

            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            Session("recCount") = State.searchDV.Count

            If State.searchDV.Count > 0 Then
                ControlMgr.SetVisibleControl(Me, mogridresults, True)
                divbtns.Visible = True
                ControlMgr.SetVisibleControl(Me, btnexport, True)
                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
                Grid.DataSource = State.searchDV
                Grid.AutoGenerateColumns = False
                Grid.Columns(GRID_COL_SERVICE_CENTER_NAME_IDX).SortExpression = Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_SERVICE_CENTER_NAME
                Grid.Columns(GRID_COL_CLAIM_NUMBER_IDX).SortExpression = Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_NUMBER
                Grid.Columns(GRID_COL_COUNTRY_SERVICE_CENTER_IDX).SortExpression = Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_SERVICE_CENTER_COUNTRY

                SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedClaimId, Grid, State.PageIndex)
                State.PageIndex = Grid.PageIndex
                HighLightSortColumn(Grid, State.SortExpression, IsNewUI)
                State.searchDV.Sort = State.SortExpression
                Grid.DataBind()


            Else
                ControlMgr.SetVisibleControl(Me, mogridresults, False)
                Grid.Visible = False
                lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)

            End If




        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub PopulateSearchFieldsFromState()

        Try
            TextBoxSearchCertificate.Text = State.certificate
            TextBoxSearchClaimNumber.Text = State.claimNumber
            TextBoxSearchSerialNumber.Text = State.serialNumber
            txtSKUClaimed.Text = State.skuclaimed
            txtSKUReplaced.Text = State.skureplaced
            txtskureppart.Text = State.skureppart
            TextBoxmodel.Text = State.Model
            SetSelectedItem(ddlservicecenter, State.servicecenterid)
            SetSelectedItem(ddlservicelevel, State.servicelevelid)
            SetSelectedItem(ddlrisktype, State.risktypeid)
            SetSelectedItem(ddlReplacementType, State.replacementtypeid)
            SetSelectedItem(ddlmake, State.Manufacturerid)
            SetSelectedItem(ddlcountry, State.Countryid)
            SetSelectedItem(ddlclaimstatus, State.claimstatusid)
            SetSelectedItem(ddlclaimextstatus, State.extclaimstatusid)
            SetSelectedItem(ddlcoveragetype, State.coveragetypeid)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub PopulateStateFromSearchFields()

        Try
            State.claimNumber = TextBoxSearchClaimNumber.Text
            State.serialNumber = TextBoxSearchSerialNumber.Text
            State.certificate = TextBoxSearchCertificate.Text
            State.servicecenterid = GetSelectedItem(ddlservicecenter)
            State.Manufacturerid = GetSelectedItem(ddlmake)
            State.Model = TextBoxmodel.Text
            State.skuclaimed = txtSKUClaimed.Text
            State.skureplaced = txtSKUReplaced.Text
            State.skureppart = txtskureppart.Text
            State.Countryid = GetSelectedItem(ddlcountry)
            State.claimstatusid = GetSelectedItem(ddlclaimstatus)
            State.extclaimstatusid = GetSelectedItem(ddlclaimextstatus)
            State.coveragetypeid = GetSelectedItem(ddlcoveragetype)
            State.replacementtypeid = GetSelectedItem(ddlReplacementType)
            State.risktypeid = GetSelectedItem(ddlrisktype)
            'Me.State.claimcreateddate = DirectCast(moClaimCreatedDate.Value, SearchCriteriaStructType(Of Date))
            State.datesearchtype = moClaimCreatedDate.SearchType.ToString()
            State.servicelevelid = GetSelectedItem(ddlservicelevel)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub ClearSearch()
        TextBoxSearchClaimNumber.Text = String.Empty
        TextBoxSearchSerialNumber.Text = String.Empty
        TextBoxSearchCertificate.Text = String.Empty
        moClaimCreatedDate.Clear()
        txtSKUClaimed.Text = String.Empty
        txtSKUReplaced.Text = String.Empty
        txtskureppart.Text = String.Empty
        TextBoxmodel.Text = String.Empty
        ddlservicecenter.SelectedIndex = BLANK_ITEM_SELECTED
        ddlmake.SelectedIndex = BLANK_ITEM_SELECTED
        ddlclaimextstatus.SelectedIndex = BLANK_ITEM_SELECTED
        ddlclaimstatus.SelectedIndex = BLANK_ITEM_SELECTED
        ddlcountry.SelectedIndex = BLANK_ITEM_SELECTED
        ddlcoveragetype.SelectedIndex = BLANK_ITEM_SELECTED
        ddlservicelevel.SelectedIndex = BLANK_ITEM_SELECTED
        ddlrisktype.SelectedIndex = BLANK_ITEM_SELECTED
        ddlReplacementType.SelectedIndex = BLANK_ITEM_SELECTED

    End Sub
    Private Sub ProcessCommand()
        'Process transaction
        Dim checkValues As String = String.Empty
        Dim i As Integer
        Dim checkValueArray() As String
        checkValueArray = checkRecords.Value.Split(":"c)

        For i = 0 To checkValueArray.Length - 1
            If (checkValueArray(i) IsNot Nothing And checkValueArray(i) <> "") Then
                checkValues = checkValueArray(i).ToString & ":" & checkValues
            End If
        Next
        checkRecords.Value = GetCheckedItemsValues()
        ProcessRecords()
        checkRecords.Value = ""
        State.searchDV = Nothing
        PopulateGrid()
    End Sub

    Private Function GetCheckedItemsValues() As String
        Dim checkedValues As String = String.Empty
        For Each gvrow As GridViewRow In Grid.Rows
            Dim CheckBox1 As CheckBox = DirectCast(gvrow.FindControl(GRID_CLAIM_APPROVE_CHECKBOX), CheckBox)

            If CheckBox1.Checked Then
                Dim claimId As Guid = New Guid(gvrow.Cells(GRID_COL_CLAIM_ID_IDX).Text)

                checkedValues += GuidControl.GuidToHexString(claimId) & ":"
            End If

        Next
        Return checkedValues
    End Function

    Protected Function ProcessRecords() As Boolean
        Try
            Dim outputParameters() As DALObjects.DBHelper.DBHelperParameter
            outputParameters = Claim.ApproveOrRejectClaims(State.cmdProcessRecord, checkRecords.Value)

            If CType(outputParameters(0).Value, Integer) = 0 Then
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                HiddenSaveChangesPromptResponse.Value = MSG_BTN_OK
                'Me.DisplayMessageWithSubmit(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
            ElseIf CType(outputParameters(0).Value, Integer) = 300 Then
                'Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR, Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
                ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
            Else
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                HiddenSaveChangesPromptResponse.Value = MSG_BTN_OK
                'Me.DisplayMessageWithSubmit(CType(outputParameters(1).Value, String), "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO, False)
                MasterPage.MessageController.AddErrorAndShow(Message.MSG_RECORD_NOT_SAVED, True)
            End If

            PopulateGrid()
            Return True
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
            Return False
        End Try
    End Function
#End Region

#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub Grid_ItemBound(source As Object, e As GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim chk As CheckBox = CType(e.Row.Cells(GRID_COL_CHKBX_IDX).FindControl(GRID_CLAIM_APPROVE_CHECKBOX), CheckBox)
        Dim btnedit As LinkButton
        Try
            If (e.Row.RowType = DataControlRowType.Header) Then

                Dim headerchk As CheckBox = CType(e.Row.Cells(GRID_COL_CHKBX_IDX).FindControl(GRID_ALL_CLAIMS_CHECKBOX), CheckBox)
                If headerchk IsNot Nothing Then
                    headerchk.Attributes("onClick") = "javascript:SelectAll('" & headerchk.ClientID & "')"
                End If
            End If
            If dvRow IsNot Nothing Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                    If (e.Row.Cells(GRID_COL_CLAIM_NUMBER_IDX).FindControl(GRID_CLAIM_Number_CTRL) IsNot Nothing) Then
                        btnedit = CType(e.Row.Cells(GRID_COL_CLAIM_NUMBER_IDX).FindControl(GRID_CLAIM_Number_CTRL), LinkButton)
                        btnedit.Text = dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_NUMBER).ToString
                        btnedit.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_ID), Byte()))

                    End If

                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_ID_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_ID))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_SERIAL_NUMBER_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_SERIAL_NUMBER))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_SERVICE_CENTER_NAME_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_SERVICE_CENTER_NAME))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_CREATED_DATE_IDX),
                                                     If(dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_CREATED_DATE) Is DBNull.Value,
                                                        Nothing,
                                                        GetDateFormattedString(CType(dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_CREATED_DATE), Date))))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_COUNTRY_SERVICE_CENTER_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_SERVICE_CENTER_COUNTRY))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_MAKE_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_MAKE))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_MODEL_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_MODEL))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_SKU_CLAIMED_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_SKU_CLAIMED))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_SKU_REPLACED_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_SKU_REPLACED))
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_COVERAGE_TYPE), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_COVERAGE_TYPE))
                    If (dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_REPAIR_DATE) IsNot DBNull.Value) Then
                        PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_REPAIR_DATE), GetDateFormattedString(CType(dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_REPAIR_DATE), Date)))
                    End If
                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_STATUS_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_STATUS))

                    PopulateControlFromBOProperty(e.Row.Cells(GRID_COL_CLAIM_EXT_STATUS), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_EXT_STATUS))

                    If ((dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_EXT_STATUS_ID) IsNot DBNull.Value) AndAlso
                        (New Guid(DirectCast(dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_EXT_STATUS_ID), Byte())).Equals(PendingReviewForPaymentId))) Then
                        chk.Enabled = True
                        divbtns.Visible = True
                        ControlMgr.SetVisibleControl(Me, btnapproveclaims, True)
                    End If

                End If
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Int32)
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Public Sub RowCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try

            If e.CommandName = "Select" Then

                State.selectedClaimId = New Guid(e.CommandArgument.ToString())
                If State Is Nothing Then
                    Trace(Me, "Restoring State")
                    RestoreState(New MyState)
                End If

                callPage(ClaimForm.URL, State.selectedClaimId)

            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub


    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub Grid_PageIndexChanged(source As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try

            State.PageIndex = Grid.PageIndex
            State.selectedClaimId = Guid.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub Grid_SortCommand(source As System.Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
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
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
        Try
            'Dim claimcreateddate As SearchCriteriaStructType(Of Date)

            State.SearchClicked = True
            State.PageIndex = 0
            ControlMgr.SetVisibleControl(Me, btnapproveclaims, False)
            ControlMgr.SetVisibleControl(Me, btnexport, False)
            State.selectedClaimId = Guid.Empty
            State.IsGridVisible = True
            State.searchDV = Nothing
            PopulateGrid()
            State.SearchClicked = False
            If State.searchDV IsNot Nothing Then
                ValidSearchResultCountNew(State.searchDV.Count, True)
            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
        Try
            ClearSearch()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnapproveclaims_click(Sender As System.Object, e As System.EventArgs) Handles btnapproveclaims.Click

        Try
            ControlMgr.SetVisibleControl(Me, btnapproveclaims, False)
            State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            State.cmdProcessRecord = DALObjects.ClaimDAL.CMD_APPROVE
            ProcessCommand()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnExportResults_Click(sender As Object, e As EventArgs) Handles btnexport.Click
        Try

            callPage(Reports.RepairLogisticsClaimsExportForm.URL, State)

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
#End Region

End Class
