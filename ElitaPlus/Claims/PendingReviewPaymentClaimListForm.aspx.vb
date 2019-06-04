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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.PendingReviewForPaymentId = LookupListNew.GetIdFromCode(LookupListNew.LK_EXTENDED_CLAIM_STATUSES, Codes.CLAIM_EXTENDED_STATUS__PENDING_REVIEW_FOR_PAYMENT)
        Page.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)
        Me.MasterPage.MessageController.Clear()
        Try
            ' Populate the header and bredcrumb
            Me.MasterPage.MessageController.Clear()
            Me.MasterPage.UsePageTabTitleInBreadCrum = False
            Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            Me.UpdateBreadCrum()
            Me.Form.DefaultButton = btnSearch.UniqueID
            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)

                PopulateDropdowns()

                Me.divbtns.Visible = False

                PopulateSearchFieldsFromState()
                Me.TranslateGridHeader(Grid)
                Me.TranslateGridControls(Grid)
                If Me.State.IsGridVisible Then
                    If Not (Me.State.selectedPageSize = Me.DEFAULT_NEW_UI_PAGE_SIZE) Then
                        cboPageSize.SelectedValue = CType(Me.State.selectedPageSize, String)
                        Me.Grid.PageSize = Me.State.selectedPageSize
                    End If
                    Me.PopulateGrid()
                End If
                Me.SetGridItemStyleColor(Me.Grid)

            End If
            Me.DisplayNewProgressBarOnClick(Me.btnSearch, "LOADING_R_AND_L_CLAIMS")
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)
    End Sub

    Private Sub UpdateBreadCrum()

        If (Not Me.State Is Nothing) Then
            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End If

    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Try
            If Me.CalledUrl = ClaimForm.URL Then
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As ClaimForm.ReturnType = CType(Me.ReturnedValues, ClaimForm.ReturnType)
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If
            End If

            If Me.CalledUrl = Reports.RepairLogisticsClaimsExportForm.URL Then
                Me.MenuEnabled = True
                Dim retObj As Reports.RepairLogisticsClaimsExportForm.ReturnType = CType(ReturnPar, Reports.RepairLogisticsClaimsExportForm.ReturnType)
                'Me.State.HasDataChanged = retObj.HasDataChanged
                If Not retObj Is Nothing AndAlso retObj.HasDataChanged Then
                    Me.State.searchDV = Nothing
                End If
                Me.State.IsGridVisible = True

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Controlling Logic"
    Private Sub Country_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlcountry.SelectedIndexChanged
        Try
            Dim CountryId As Guid = Me.GetSelectedItem(Me.ddlcountry)

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
                        If Not ServiceCenterList Is Nothing Then
                            ServiceCenterList.AddRange(ServiceCenters)
                        Else
                            ServiceCenterList = ServiceCenters.Clone()
                        End If
                    End If
                Next

                Me.ddlservicecenter.Populate(ServiceCenterList.ToArray(), New PopulateOptions() With
                    {
                        .AddBlankItem = True
                    })

            Else
                'Me.BindListControlToDataView(Me.ddlservicecenter, LookupListNew.GetServiceCenterLookupList(CountryId), , , True);ServiceCenterListByCountry
                Dim listcontext As ListContext = New ListContext()
                listcontext.CountryId = CountryId
                Dim serviceCenterLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ServiceCenterListByCountry", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
                Me.ddlservicecenter.Populate(serviceCenterLkl, New PopulateOptions() With
                        {
                        .AddBlankItem = True
                        })

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub PopulateDropdowns()
        Try
            Dim LangId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Dim listcontext As ListContext = New ListContext()
            listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id

            Dim extendedStatusByGroupLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtendedStatusByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.ddlclaimextstatus.Populate(extendedStatusByGroupLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })

            Dim countryList As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Country")

            Dim filteredCountryList As DataElements.ListItem() = (From x In countryList
                                                                  Where ElitaPlusIdentity.Current.ActiveUser.Countries.Contains(x.ListItemId)
                                                                  Select x).ToArray()

            Me.ddlcountry.Populate(filteredCountryList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True
                                                   })

            If ddlcountry.Items.Count = 2 Then
                ddlcountry.SelectedIndex = 1
                Me.State.Countryid = New Guid(ddlcountry.SelectedValue.ToString())
            End If

            Dim claimstatusLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CLSTAT", Thread.CurrentPrincipal.GetLanguageCode())
            Me.ddlclaimstatus.Populate(claimstatusLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })

            'Dim ocoveragebycompgroup As DataView
            'ocoveragebycompgroup = LookupListNew.GetCoverageTypeByCompanyGroupLookupList(LangId, compgrpid)


            ' Me.BindListControlToDataView(Me.ddlcoveragetype, ocoveragebycompgroup, , , True)

            Dim coverageTypeByCompanyGroupLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CoverageTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.ddlcoveragetype.Populate(coverageTypeByCompanyGroupLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })

            Dim servicelevelLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("SVC_LVL", Thread.CurrentPrincipal.GetLanguageCode())
            Me.ddlservicelevel.Populate(servicelevelLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })

            Dim replacementTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("DEVICE", Thread.CurrentPrincipal.GetLanguageCode())
            Me.ddlReplacementType.Populate(replacementTypeLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })

            Dim manufacturerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ManufacturerByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.ddlmake.Populate(manufacturerLkl, New PopulateOptions() With
                {
                .AddBlankItem = True
                })

            Dim RiskTypeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("RiskTypeByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)
            Me.ddlrisktype.Populate(RiskTypeLkl, New PopulateOptions() With
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
                    If Not ServiceCenterList Is Nothing Then
                        ServiceCenterList.AddRange(ServiceCenters)
                    Else
                        ServiceCenterList = ServiceCenters.Clone()
                    End If
                End If
            Next

            Me.ddlservicecenter.Populate(ServiceCenterList.ToArray(), New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Public Sub PopulateGrid()

        Try
            PopulateStateFromSearchFields()
            Dim haserrors As Boolean = False
            If (Not moClaimCreatedDate.IsEmpty) And Not moClaimCreatedDate.Validate() Then
                Exit Sub
            Else
                Me.State.claimcreateddate = DirectCast(moClaimCreatedDate.Value, SearchCriteriaStructType(Of Date))
            End If

            Me.State.claimstatus = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_STATUS, Me.State.claimstatusid)
            If Me.State.claimstatus Is Nothing Then
                Me.State.claimstatus = String.Empty
            End If

            If (Me.State.searchDV Is Nothing) Then
                Me.State.searchDV = Claim.GetPendingReviewPaymentClaimList(Me.State.claimNumber, Me.State.serialNumber, Me.State.certificate, Me.State.servicecenterid,
                                                                           Me.State.Countryid, Me.State.Manufacturerid, Me.State.Model, Me.State.skuclaimed, Me.State.skureplaced,
                                                                            Me.State.claimstatus, Me.State.extclaimstatusid, Me.State.coveragetypeid, Me.State.servicelevelid, Me.State.risktypeid,
                                                                            Me.State.skureppart, Me.State.replacementtypeid, Me.State.claimcreateddate)


            End If

            Me.Grid.AutoGenerateColumns = False


            Me.Grid.PageSize = Me.State.selectedPageSize

            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            Session("recCount") = Me.State.searchDV.Count

            If Me.State.searchDV.Count > 0 Then
                ControlMgr.SetVisibleControl(Me, mogridresults, True)
                Me.divbtns.Visible = True
                ControlMgr.SetVisibleControl(Me, btnexport, True)
                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                End If
                Me.Grid.DataSource = Me.State.searchDV
                Me.Grid.AutoGenerateColumns = False
                Me.Grid.Columns(Me.GRID_COL_SERVICE_CENTER_NAME_IDX).SortExpression = Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_SERVICE_CENTER_NAME
                Me.Grid.Columns(Me.GRID_COL_CLAIM_NUMBER_IDX).SortExpression = Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_NUMBER
                Me.Grid.Columns(Me.GRID_COL_COUNTRY_SERVICE_CENTER_IDX).SortExpression = Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_SERVICE_CENTER_COUNTRY

                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedClaimId, Me.Grid, Me.State.PageIndex)
                Me.State.PageIndex = Me.Grid.PageIndex
                HighLightSortColumn(Grid, Me.State.SortExpression, Me.IsNewUI)
                Me.State.searchDV.Sort = Me.State.SortExpression
                Me.Grid.DataBind()


            Else
                ControlMgr.SetVisibleControl(Me, mogridresults, False)
                Me.Grid.Visible = False
                Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)

            End If




        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub PopulateSearchFieldsFromState()

        Try
            Me.TextBoxSearchCertificate.Text = Me.State.certificate
            Me.TextBoxSearchClaimNumber.Text = Me.State.claimNumber
            Me.TextBoxSearchSerialNumber.Text = Me.State.serialNumber
            Me.txtSKUClaimed.Text = Me.State.skuclaimed
            Me.txtSKUReplaced.Text = Me.State.skureplaced
            Me.txtskureppart.Text = Me.State.skureppart
            Me.TextBoxmodel.Text = Me.State.Model
            Me.SetSelectedItem(ddlservicecenter, Me.State.servicecenterid)
            Me.SetSelectedItem(ddlservicelevel, Me.State.servicelevelid)
            Me.SetSelectedItem(ddlrisktype, Me.State.risktypeid)
            Me.SetSelectedItem(ddlReplacementType, Me.State.replacementtypeid)
            Me.SetSelectedItem(ddlmake, Me.State.Manufacturerid)
            Me.SetSelectedItem(ddlcountry, Me.State.Countryid)
            Me.SetSelectedItem(ddlclaimstatus, Me.State.claimstatusid)
            Me.SetSelectedItem(ddlclaimextstatus, Me.State.extclaimstatusid)
            Me.SetSelectedItem(ddlcoveragetype, Me.State.coveragetypeid)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub PopulateStateFromSearchFields()

        Try
            Me.State.claimNumber = Me.TextBoxSearchClaimNumber.Text
            Me.State.serialNumber = Me.TextBoxSearchSerialNumber.Text
            Me.State.certificate = Me.TextBoxSearchCertificate.Text
            Me.State.servicecenterid = GetSelectedItem(Me.ddlservicecenter)
            Me.State.Manufacturerid = GetSelectedItem(Me.ddlmake)
            Me.State.Model = Me.TextBoxmodel.Text
            Me.State.skuclaimed = Me.txtSKUClaimed.Text
            Me.State.skureplaced = Me.txtSKUReplaced.Text
            Me.State.skureppart = Me.txtskureppart.Text
            Me.State.Countryid = GetSelectedItem(Me.ddlcountry)
            Me.State.claimstatusid = GetSelectedItem(Me.ddlclaimstatus)
            Me.State.extclaimstatusid = GetSelectedItem(Me.ddlclaimextstatus)
            Me.State.coveragetypeid = GetSelectedItem(Me.ddlcoveragetype)
            Me.State.replacementtypeid = GetSelectedItem(Me.ddlReplacementType)
            Me.State.risktypeid = GetSelectedItem(Me.ddlrisktype)
            'Me.State.claimcreateddate = DirectCast(moClaimCreatedDate.Value, SearchCriteriaStructType(Of Date))
            Me.State.datesearchtype = moClaimCreatedDate.SearchType.ToString()
            Me.State.servicelevelid = GetSelectedItem(Me.ddlservicelevel)

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub ClearSearch()
        Me.TextBoxSearchClaimNumber.Text = String.Empty
        Me.TextBoxSearchSerialNumber.Text = String.Empty
        Me.TextBoxSearchCertificate.Text = String.Empty
        Me.moClaimCreatedDate.Clear()
        Me.txtSKUClaimed.Text = String.Empty
        Me.txtSKUReplaced.Text = String.Empty
        Me.txtskureppart.Text = String.Empty
        Me.TextBoxmodel.Text = String.Empty
        Me.ddlservicecenter.SelectedIndex = Me.BLANK_ITEM_SELECTED
        Me.ddlmake.SelectedIndex = Me.BLANK_ITEM_SELECTED
        Me.ddlclaimextstatus.SelectedIndex = Me.BLANK_ITEM_SELECTED
        Me.ddlclaimstatus.SelectedIndex = Me.BLANK_ITEM_SELECTED
        Me.ddlcountry.SelectedIndex = Me.BLANK_ITEM_SELECTED
        Me.ddlcoveragetype.SelectedIndex = Me.BLANK_ITEM_SELECTED
        Me.ddlservicelevel.SelectedIndex = Me.BLANK_ITEM_SELECTED
        Me.ddlrisktype.SelectedIndex = Me.BLANK_ITEM_SELECTED
        Me.ddlReplacementType.SelectedIndex = Me.BLANK_ITEM_SELECTED

    End Sub
    Private Sub ProcessCommand()
        'Process transaction
        Dim checkValues As String = String.Empty
        Dim i As Integer
        Dim checkValueArray() As String
        checkValueArray = checkRecords.Value.Split(":"c)

        For i = 0 To checkValueArray.Length - 1
            If (Not checkValueArray(i) Is Nothing And checkValueArray(i) <> "") Then
                checkValues = checkValueArray(i).ToString & ":" & checkValues
            End If
        Next
        checkRecords.Value = GetCheckedItemsValues()
        ProcessRecords()
        checkRecords.Value = ""
        Me.State.searchDV = Nothing
        PopulateGrid()
    End Sub

    Private Function GetCheckedItemsValues() As String
        Dim checkedValues As String = String.Empty
        For Each gvrow As GridViewRow In Me.Grid.Rows
            Dim CheckBox1 As CheckBox = DirectCast(gvrow.FindControl(GRID_CLAIM_APPROVE_CHECKBOX), CheckBox)

            If CheckBox1.Checked Then
                Dim claimId As Guid = New Guid(gvrow.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text)

                checkedValues += GuidControl.GuidToHexString(claimId) & ":"
            End If

        Next
        Return checkedValues
    End Function

    Protected Function ProcessRecords() As Boolean
        Try
            Dim outputParameters() As DALObjects.DBHelper.DBHelperParameter
            outputParameters = Claim.ApproveOrRejectClaims(Me.State.cmdProcessRecord, checkRecords.Value)

            If CType(outputParameters(0).Value, Integer) = 0 Then
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                Me.HiddenSaveChangesPromptResponse.Value = Me.MSG_BTN_OK
                'Me.DisplayMessageWithSubmit(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Me.MasterPage.MessageController.AddSuccess(Message.SAVE_RECORD_CONFIRMATION, True)
            ElseIf CType(outputParameters(0).Value, Integer) = 300 Then
                'Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR, Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
                Me.ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
            Else
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                Me.HiddenSaveChangesPromptResponse.Value = Me.MSG_BTN_OK
                'Me.DisplayMessageWithSubmit(CType(outputParameters(1).Value, String), "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO, False)
                Me.MasterPage.MessageController.AddErrorAndShow(Message.MSG_RECORD_NOT_SAVED, True)
            End If

            PopulateGrid()
            Return True
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
            Return False
        End Try
    End Function
#End Region

#Region " Datagrid Related "

    'The Binding LOgic is here
    Private Sub Grid_ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles Grid.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
        Dim chk As CheckBox = CType(e.Row.Cells(Me.GRID_COL_CHKBX_IDX).FindControl(Me.GRID_CLAIM_APPROVE_CHECKBOX), CheckBox)
        Dim btnedit As LinkButton
        Try
            If (e.Row.RowType = DataControlRowType.Header) Then

                Dim headerchk As CheckBox = CType(e.Row.Cells(Me.GRID_COL_CHKBX_IDX).FindControl(Me.GRID_ALL_CLAIMS_CHECKBOX), CheckBox)
                If Not headerchk Is Nothing Then
                    headerchk.Attributes("onClick") = "javascript:SelectAll('" & headerchk.ClientID & "')"
                End If
            End If
            If Not dvRow Is Nothing Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                    If (Not e.Row.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX).FindControl(Me.GRID_CLAIM_Number_CTRL) Is Nothing) Then
                        btnedit = CType(e.Row.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX).FindControl(Me.GRID_CLAIM_Number_CTRL), LinkButton)
                        btnedit.Text = dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_NUMBER).ToString
                        btnedit.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_ID), Byte()))

                    End If

                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_ID_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_ID))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_SERIAL_NUMBER_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_SERIAL_NUMBER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_SERVICE_CENTER_NAME_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_SERVICE_CENTER_NAME))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_CREATED_DATE_IDX),
                                                     If(dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_CREATED_DATE) Is DBNull.Value,
                                                        Nothing,
                                                        GetDateFormattedString(CType(dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_CREATED_DATE), Date))))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_COUNTRY_SERVICE_CENTER_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_SERVICE_CENTER_COUNTRY))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_MAKE_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_MAKE))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_MODEL_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_MODEL))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_SKU_CLAIMED_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_SKU_CLAIMED))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_SKU_REPLACED_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_SKU_REPLACED))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_COVERAGE_TYPE), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_COVERAGE_TYPE))
                    If (Not dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_REPAIR_DATE) Is DBNull.Value) Then
                        Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_REPAIR_DATE), GetDateFormattedString(CType(dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_REPAIR_DATE), Date)))
                    End If
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_STATUS_IDX), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_STATUS))

                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_COL_CLAIM_EXT_STATUS), dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_EXT_STATUS))

                    If ((Not dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_EXT_STATUS_ID) Is DBNull.Value) AndAlso
                        (New Guid(DirectCast(dvRow(Claim.PendingReviewPaymentClaimSearchDV.COL_NAME_CLAIM_EXT_STATUS_ID), Byte())).Equals(Me.PendingReviewForPaymentId))) Then
                        chk.Enabled = True
                        Me.divbtns.Visible = True
                        ControlMgr.SetVisibleControl(Me, btnapproveclaims, True)
                    End If

                End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            Grid.PageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Int32)
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub


    Public Sub RowCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        Try

            If e.CommandName = "Select" Then

                Me.State.selectedClaimId = New Guid(e.CommandArgument.ToString())
                If Me.State Is Nothing Then
                    Me.Trace(Me, "Restoring State")
                    Me.RestoreState(New MyState)
                End If

                Me.callPage(ClaimForm.URL, Me.State.selectedClaimId)

            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs)
        BaseItemCreated(sender, e)
    End Sub


    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try

            Me.State.PageIndex = Grid.PageIndex
            Me.State.selectedClaimId = Guid.Empty
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
    Private Sub Grid_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
        Try
            If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                If Me.State.SortExpression.EndsWith(" DESC") Then
                    Me.State.SortExpression = e.SortExpression
                Else
                    Me.State.SortExpression &= " DESC"
                End If
            Else
                Me.State.SortExpression = e.SortExpression
            End If
            Me.State.PageIndex = 0
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region " Button Clicks "

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            'Dim claimcreateddate As SearchCriteriaStructType(Of Date)

            Me.State.SearchClicked = True
            Me.State.PageIndex = 0
            ControlMgr.SetVisibleControl(Me, btnapproveclaims, False)
            ControlMgr.SetVisibleControl(Me, btnexport, False)
            Me.State.selectedClaimId = Guid.Empty
            Me.State.IsGridVisible = True
            Me.State.searchDV = Nothing
            Me.PopulateGrid()
            Me.State.SearchClicked = False
            If Not Me.State.searchDV Is Nothing Then
                Me.ValidSearchResultCountNew(Me.State.searchDV.Count, True)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Try
            Me.ClearSearch()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnapproveclaims_click(ByVal Sender As System.Object, ByVal e As System.EventArgs) Handles btnapproveclaims.Click

        Try
            ControlMgr.SetVisibleControl(Me, btnapproveclaims, False)
            Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
            Me.State.cmdProcessRecord = DALObjects.ClaimDAL.CMD_APPROVE
            Me.ProcessCommand()

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Protected Sub btnExportResults_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnexport.Click
        Try

            Me.callPage(Reports.RepairLogisticsClaimsExportForm.URL, State)

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
#End Region

End Class
