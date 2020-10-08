Imports System.Diagnostics
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.BusinessObjectsNew.Doc
Imports Microsoft.VisualBasic
Imports System.Threading
Imports System.Globalization
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements
Namespace Claims

    Partial Class ClaimFollowUpListForm
        Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents ErrorCtrl As ErrorController

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As Object

        Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("CLAIM FOLLOW UP SEARCH")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CLAIM FOLLOW UP SEARCH")
                End If
            End If
        End Sub

#End Region

#Region "Constants"
        Public Const GRID_COL_EDIT_IDX As Integer = 1
        Public Const GRID_COL_CLAIM_NUMBER_IDX As Integer = 0
        Public Const GRID_COL_FOLLOWUP_DATE_IDX As Integer = 2
        Public Const GRID_COL_CLAIM_TAT_IDX As Integer = 3
        Public Const GRID_COL_SVC_TAT_IDX As Integer = 4
        Public Const GRID_COL_LAST_ACTIVITY_IDX As Integer = 5
        Public Const GRID_COL_STATUS_IDX As Integer = 6
        Public Const GRID_COL_CLAIM_EXT_STATUS_IDX As Integer = 7
        Public Const GRID_COL_STATUS_OWNER_IDX As Integer = 8
        Public Const GRID_COL_SERVICE_CENTER_NAME_IDX As Integer = 9
        Public Const GRID_COL_CLAIMS_ADJUSTER_IDX As Integer = 10
        Public Const GRID_COL_CUST_NAME_IDX As Integer = 11
        Public Const GRID_COL_DEALER_CODE_IDX As Integer = 12
        Public Const GRID_COL_DENIED_REASON_IDX As Integer = 13
        Public Const GRID_COL_COMMENT_TYPE_IDX As Integer = 14
        Public Const GRID_COL_NUM_OF_REMINDERS_SENT_IDX As Integer = 15
        Public Const GRID_COL_LAST_REMINDER_SENT_ON_IDX As Integer = 16
        Public Const GRID_COL_CLAIM_ID_IDX As Integer = 17
        Public Const GRID_TOTAL_COLUMNS As Integer = 18


        Public Const MAX_LIMIT As Integer = 1000

        Public Const CLAIM_STATUS_DESCRIPTION_ACTIVE As String = "Active"
        Public Const CLAIM_STATUS_DESCRIPTION_DENIED As String = "Denied"
        Public Const GRID_COL_CODE_CTRL As String = "btnEditCode"
        Public Const SELECT_ACTION_COMMAND As String = "SelectAction"

#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public PageIndex As Integer = 0
            Public PageSize As Integer = 10
            Public selectedSortById As Guid = Guid.Empty
            Public followUpDate As DateType = Nothing
            'Public servCenterZip As String = String.Empty
            Public claimNumber As String = String.Empty
            Public claimAdjuster As String = String.Empty
            Public claimStatus As String = String.Empty
            Public customerName As String = String.Empty
            Public dealerId As Guid
            Public dealerName As String = String.Empty
            Public dealer As String = String.Empty
            '  Public serviceCenterId As Guid
            Public serviceCenterName As String = String.Empty
            Public searchDV As Claim.ClaimFollowUpSearchDV = Nothing

            Public SortColumns(GRID_TOTAL_COLUMNS - 1) As String
            Public IsSortDesc(GRID_TOTAL_COLUMNS - 1) As Boolean
            Public selectedClaimId As Guid = Guid.Empty
            Public selectedClaimExtendedStatusId As Guid = Guid.Empty
            Public selectedClaimExtendedStatusOwnerId As Guid = Guid.Empty
            Public selectedClaimTATId As Guid = Guid.Empty
            Public selectedNoActivityTATId As Guid = Guid.Empty
            Public selectedNonOperatedClaimsId As Guid = Guid.Empty
            Public SortExpression As String = String.Empty

            Public IsGridVisible As Boolean = False
            Public TATDV As DataView = Nothing

            Sub New()
                'SortColumns(GRID_COL_FOLLOWUP_DATE_IDX) = Claim.ClaimFollowUpSearchDV.COL_FOLLOWUP_DATE
                'SortColumns(GRID_COL_SERVICE_CENTER_ZIP_IDX) = Claim.ClaimFollowUpSearchDV.COL_SERVICE_CENTER_ZIP
                'SortColumns(GRID_COL_SERVICE_CENTER_NAME_IDX) = Claim.ClaimFollowUpSearchDV.COL_SERVICE_CENTER_NAME
                'SortColumns(GRID_COL_CLAIM_NUMBER_IDX) = Claim.ClaimFollowUpSearchDV.COL_CLAIM_NUMBER
                'SortColumns(GRID_COL_CLAIMS_ADJUSTER_IDX) = Claim.ClaimFollowUpSearchDV.COL_CLAIMS_AJDUSTER
                'SortColumns(GRID_COL_CUST_NAME_IDX) = Claim.ClaimFollowUpSearchDV.COL_CUST_NAME
                'SortColumns(GRID_COL_DEALER_CODE_IDX) = Claim.ClaimFollowUpSearchDV.COL_DEALER_CODE

                'IsSortDesc(GRID_COL_FOLLOWUP_DATE_IDX) = False
                'IsSortDesc(GRID_COL_SERVICE_CENTER_ZIP_IDX) = False
                'IsSortDesc(GRID_COL_SERVICE_CENTER_NAME_IDX) = False
                'IsSortDesc(GRID_COL_CLAIM_NUMBER_IDX) = False
                'IsSortDesc(GRID_COL_CLAIMS_ADJUSTER_IDX) = False
                'IsSortDesc(GRID_COL_CUST_NAME_IDX) = False
                'IsSortDesc(GRID_COL_DEALER_CODE_IDX) = False
            End Sub

            Public ReadOnly Property CurrentSortExpresion() As String
                Get
                    'Dim s As String
                    'Dim i As Integer
                    'Dim sortExp As String = ""
                    'For i = 0 To Me.SortColumns.Length - 1
                    '    If Not Me.SortColumns(i) Is Nothing Then
                    '        sortExp &= Me.SortColumns(i)
                    '        If Me.IsSortDesc(i) Then sortExp &= " DESC"
                    '        sortExp &= ","
                    '    End If
                    'Next
                    'Return sortExp.Substring(0, sortExp.Length - 1) 'to remove the last comma
                End Get
            End Property

            'Public Sub ToggleSort(ByVal gridColIndex As Integer)
            '    'IsSortDesc(gridColIndex) = Not IsSortDesc(gridColIndex)
            'End Sub
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            MenuEnabled = True
            IsReturningFromChild = True
            Dim retObj As ClaimForm.ReturnType = CType(ReturnPar, ClaimForm.ReturnType)
            Try
                Select Case retObj.LastOperation
                    Case DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.selectedClaimId = retObj.EditingBo.Id
                            End If
                            State.IsGridVisible = True
                            If retObj.BoChanged Then
                                State.searchDV = Nothing
                            End If
                        End If
                    Case DetailPageCommand.Delete
                        AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Page_Events"
        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Page.RegisterHiddenField("__EVENTTARGET", btnSearch.ClientID)
            MasterPage.MessageController.Clear_Hide()

            Try
                If Not IsPostBack Then
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Claim Follow Up")
                    UpdateBreadCrum()

                    setDefaultSearchButton()

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)

                    PopulateDealerDropdown(moDealerDrop)
                    '  Me.PopulateServiceCenterDropdown(cboSearchServiceCenter)
                    PopulateSortByDropDown()
                    PopulateClaimStatusDropDown()
                    PopulateDropDowns()

                    GetStateProperties()
                    If State.IsGridVisible Then
                        If Not (State.PageSize = 10) Then
                            cboPageSize.SelectedValue = CType(State.PageSize, String)
                            Grid.PageSize = State.PageSize
                        End If
                        PopulateGrid()
                    End If
                    SetGridItemStyleColor(Grid)
                    AddCalendar(BtnFollowUpDate, TextBoxFollowUpDate)
                    SetFocus(TextBoxSearchClaimNumber)
                Else
                    ClearLabelErrSign(LabelFollowUpDate)
                End If
                DisplayNewProgressBarOnClick(btnSearch, "Loading_Claims")
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Sub PopulateDropDowns()

            Try
                'Claim Extended Status Owner

                Dim oExtendedStatusOwnerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("EXTOWN", Thread.CurrentPrincipal.GetLanguageCode())
                moOwnerDD.Populate(oExtendedStatusOwnerLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })

                If Not (State.selectedClaimExtendedStatusOwnerId.Equals(Guid.Empty)) Then
                    SetSelectedItem(moOwnerDD, State.selectedClaimExtendedStatusOwnerId)
                End If
                Dim listcontext As ListContext = New ListContext()
                listcontext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Dim claimExtendedStatusLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("ExtendedStatusByCompanyGroup", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)

                'Claim Extended Status

                If Not claimExtendedStatusLkl.Count = 0 Then

                    moClaimExtendedStatusDD.Populate(claimExtendedStatusLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })

                    If Not (State.selectedClaimExtendedStatusId.Equals(Guid.Empty)) Then
                        SetSelectedItem(moClaimExtendedStatusDD, State.selectedClaimExtendedStatusId)
                    End If
                Else
                    ControlMgr.SetVisibleControl(Me, moClaimExtendedStatusDD, False)
                    ControlMgr.SetVisibleControl(Me, LabelSearchClaimExtendedStatus, False)
                    ControlMgr.SetVisibleControl(Me, moOwnerDD, False)
                    ControlMgr.SetVisibleControl(Me, lblOwner, False)
                End If

                'Me.moClaimExtendedStatusDD.Attributes.Add("onchange", "ToggleSelection1('" & moClaimExtendedStatusDD.ClientID & "','" & moOwnerDD.ClientID & "')")
                'Me.moOwnerDD.Attributes.Add("onchange", "ToggleSelection2('" & moOwnerDD.ClientID & "','" & moClaimExtendedStatusDD.ClientID & "')")

                'Claim_Turn_Around_Time list

                Dim claimTurnAroundTimeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("LkScTatByGroupList", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)



                If Not claimTurnAroundTimeLkl.Count = 0 Then

                    moClaimTATRangeDD.Populate(claimTurnAroundTimeLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })
                    If Not (State.selectedClaimTATId.Equals(Guid.Empty)) Then
                        SetSelectedItem(moClaimTATRangeDD, State.selectedClaimTATId)
                    End If
                Else
                    ControlMgr.SetVisibleControl(Me, moClaimTATRangeDD, False)
                    ControlMgr.SetVisibleControl(Me, LabelSearchClaimTATRange, False)
                End If

                'No Activity Time Range list

                Dim activityTimeRangeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("LkScTatByGroupList", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)

                If Not activityTimeRangeLkl.Count = 0 Then

                    moLastActivityDD.Populate(activityTimeRangeLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })
                    If Not (State.selectedNoActivityTATId.Equals(Guid.Empty)) Then
                        SetSelectedItem(moLastActivityDD, State.selectedNoActivityTATId)
                    End If
                Else
                    ControlMgr.SetVisibleControl(Me, moLastActivityDD, False)
                    ControlMgr.SetVisibleControl(Me, LabelSearchLastActivity, False)
                End If

                Dim oNonOperatedClaims As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                cboNonOperatedClaims.Populate(oNonOperatedClaims, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                If Not (State.selectedNonOperatedClaimsId.Equals(Guid.Empty)) Then
                    SetSelectedItem(cboNonOperatedClaims, State.selectedNonOperatedClaimsId)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SetStateProperties()
            Try
                If String.IsNullOrEmpty(TextBoxFollowUpDate.Text) Then
                    State.followUpDate = Nothing
                Else
                    State.followUpDate = DateHelper.GetDateValue(TextBoxFollowUpDate.Text)
                End If

            Catch ex As Exception
                SetLabelError(LabelFollowUpDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
            End Try
            'Me.State.servCenterZip = Me.TextBoxSearchServiceCenterZIP.Text.ToUpper
            State.claimNumber = TextBoxSearchClaimNumber.Text.ToUpper
            State.claimAdjuster = TextBoxSearchClaimAdjuster.Text.ToUpper
            State.customerName = TextBoxSearchCustomerName.Text.ToUpper
            State.dealerId = GetSelectedItem(moDealerDrop)
            State.dealerName = GetSelectedDescription(moDealerDrop)
            State.dealer = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, State.dealerId)
            '  Me.State.serviceCenterId = Me.GetSelectedItem(cboSearchServiceCenter)
            '  Me.State.serviceCenterName = Me.GetSelectedDescription(cboSearchServiceCenter)
            State.serviceCenterName = moServiceCenterText.Text.ToUpper
            State.selectedSortById = GetSelectedItem(cboSortBy)

            State.selectedClaimExtendedStatusId = GetSelectedItem(moClaimExtendedStatusDD)
            State.selectedClaimExtendedStatusOwnerId = GetSelectedItem(moOwnerDD)
            State.selectedClaimTATId = GetSelectedItem(moClaimTATRangeDD)
            State.selectedNoActivityTATId = GetSelectedItem(moLastActivityDD)
            State.selectedNonOperatedClaimsId = GetSelectedItem(cboNonOperatedClaims)
            If cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_ACTIVE Then
                State.claimStatus = Codes.CLAIM_STATUS__ACTIVE
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_DENIED Then
                State.claimStatus = Codes.CLAIM_STATUS__DENIED
            End If

        End Sub

        Private Sub GetStateProperties()
            Try
                If State.followUpDate Is Nothing Then
                    TextBoxFollowUpDate.Text = String.Empty
                Else
                    TextBoxFollowUpDate.Text = CType(State.followUpDate.Value, String)
                End If

                'Me.TextBoxSearchServiceCenterZIP.Text = Me.State.servCenterZip
                TextBoxSearchClaimNumber.Text = State.claimNumber
                TextBoxSearchClaimAdjuster.Text = State.claimAdjuster
                TextBoxSearchCustomerName.Text = State.customerName
                SetSelectedItem(moDealerDrop, State.dealerId)

                If Not State.selectedClaimExtendedStatusId.Equals(Guid.Empty) Then
                    SetSelectedItem(moClaimExtendedStatusDD, State.selectedClaimExtendedStatusId)
                End If

                If Not State.selectedClaimExtendedStatusOwnerId.Equals(Guid.Empty) Then
                    SetSelectedItem(moOwnerDD, State.selectedClaimExtendedStatusOwnerId)
                End If

                If Not State.selectedClaimTATId.Equals(Guid.Empty) Then
                    SetSelectedItem(moClaimTATRangeDD, State.selectedClaimTATId)
                End If

                If Not State.selectedNoActivityTATId.Equals(Guid.Empty) Then
                    SetSelectedItem(moClaimTATRangeDD, State.selectedNoActivityTATId)
                End If

                '   Me.SetSelectedItem(Me.cboSearchServiceCenter, Me.State.serviceCenterId)
                moServiceCenterText.Text = State.serviceCenterName
                SetSelectedItem(cboSortBy, State.selectedSortById)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub setDefaultSearchButton()

            Try
                SetDefaultButton(TextBoxFollowUpDate, btnSearch)
                SetDefaultButton(TextBoxSearchClaimAdjuster, btnSearch)
                'Me.SetDefaultButton(Me.TextBoxSearchServiceCenterZIP, btnSearch)
                SetDefaultButton(TextBoxSearchClaimNumber, btnSearch)
                SetDefaultButton(TextBoxSearchCustomerName, btnSearch)
                SetDefaultButton(moServiceCenterText, btnSearch)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateDealerDropdown(dealerDropDownList As DropDownList)
            ' Dim oCompanyId As Guid = Me.GetApplicationUser.CompanyID
            Try

                BindListControlToDataView(dealerDropDownList,
                LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code"), , , True)
                '   Me.SetSelectedItem(dealerDropDownList, Me.State.dealerId)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Sub PopulateClaimStatusDropDown()
            Try
                cboClaimStatus.Items.Add(New WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_ACTIVE))
                cboClaimStatus.Items.Add(New WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_DENIED))

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Sub PopulateSortByDropDown()
            Try
                ' Me.BindListControlToDataView(Me.cboSortBy, LookupListNew.GetClaimFollowUpSearchFieldsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), , , False)

                Dim claimFollowUpSearchFieldsLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("CFUDR", Thread.CurrentPrincipal.GetLanguageCode())
                cboSortBy.Populate(claimFollowUpSearchFieldsLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = False
                    })

                Dim defaultSelectedCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CLAIM_FOLLOWUP_SEARCH_FIELDS, "CFDAT")

                If (State.selectedSortById.Equals(Guid.Empty)) Then
                    SetSelectedItem(cboSortBy, defaultSelectedCodeId)
                    State.selectedSortById = defaultSelectedCodeId
                Else
                    SetSelectedItem(cboSortBy, State.selectedSortById)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        Public Sub PopulateGrid()
            Dim foundLabel As String
            '   Dim oCompanyId As Guid = Me.GetApplicationUser.CompanyID
            Dim errors() As ValidationError = {New ValidationError(Assurant.ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Claim), Nothing, "Search", Nothing)}

            Try
                GetSelectedItem(moDealerDrop)
                '   Me.GetSelectedItem(cboSearchServiceCenter)

                If (State.searchDV Is Nothing) Then

                    Dim searchFollowUpDate As String = Nothing

                    If Not (State.followUpDate Is Nothing) Then
                        Dim tempFollowDate As DateTime = New DateTime
                        tempFollowDate = DateHelper.GetDateValue(State.followUpDate.ToString())
                        searchFollowUpDate = tempFollowDate.ToString("MMddyyyy")
                    Else
                        searchFollowUpDate = String.Empty
                    End If

                    Dim nonOperatedClaims As String = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, State.selectedNonOperatedClaimsId)

                    Dim sortBy As String

                    If (Not (State.selectedSortById.Equals(Guid.Empty))) Then
                        sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_FOLLOWUP_SEARCH_FIELDS, State.selectedSortById)

                        State.searchDV = Claim.GetClaimFollowUpList(searchFollowUpDate,
                                                                           State.serviceCenterName,
                                                                           State.claimNumber,
                                                                           State.claimAdjuster,
                                                                           State.customerName,
                                                                           State.claimStatus,
                                                                           State.dealerId,
                                                                           State.selectedClaimTATId,
                                                                           State.selectedClaimExtendedStatusId,
                                                                           State.selectedNoActivityTATId,
                                                                           State.selectedClaimExtendedStatusOwnerId,
                                                                           ElitaPlusIdentity.Current.ActiveUser.LanguageId,
                                                                           nonOperatedClaims,
                                                                           sortBy)
                    Else
                        State.searchDV = Claim.GetClaimFollowUpList(searchFollowUpDate,
                                                                          State.serviceCenterName,
                                                                          State.claimNumber,
                                                                          State.claimAdjuster,
                                                                          State.customerName,
                                                                          State.claimStatus,
                                                                          State.dealerId,
                                                                          State.selectedClaimTATId,
                                                                          State.selectedClaimExtendedStatusId,
                                                                          State.selectedNoActivityTATId,
                                                                          State.selectedClaimExtendedStatusOwnerId,
                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId,
                                                                          nonOperatedClaims)
                    End If

                    ValidSearchResultCount(State.searchDV.Count, True)
                End If

                'Me.State.searchDV.Sort = Grid.DataMember
                State.searchDV.Sort = State.SortExpression
                Grid.AutoGenerateColumns = False
                Grid.PageSize = State.PageSize

                SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedClaimId, Grid, State.PageIndex)
                State.PageIndex = Grid.CurrentPageIndex
                Grid.DataSource = State.searchDV
                'Me.Grid.AllowSorting = False
                Grid.Columns(GRID_COL_FOLLOWUP_DATE_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_FOLLOWUP_DATE
                Grid.Columns(GRID_COL_CLAIM_TAT_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_CLAIM_TAT
                Grid.Columns(GRID_COL_SVC_TAT_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_SVC_TAT
                Grid.Columns(GRID_COL_LAST_ACTIVITY_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_NO_ACTIVITY
                Grid.Columns(GRID_COL_CLAIM_NUMBER_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_CLAIM_NUMBER
                Grid.Columns(GRID_COL_CUST_NAME_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_CUST_NAME
                Grid.Columns(GRID_COL_SERVICE_CENTER_NAME_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_SERVICE_CENTER_NAME
                Grid.Columns(GRID_COL_CLAIMS_ADJUSTER_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_CLAIMS_AJDUSTER
                Grid.Columns(GRID_COL_CLAIM_EXT_STATUS_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_EXTENDED_STATUS
                Grid.Columns(GRID_COL_STATUS_OWNER_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_OWNER
                Grid.Columns(GRID_COL_STATUS_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_STATUS_CODE
                Grid.Columns(GRID_COL_DEALER_CODE_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_DEALER_CODE

                Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)

                Session("recCount") = State.searchDV.Count

                foundLabel = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                If State.searchDV.Count > 0 Then
                    If Grid.Visible Then
                        lblRecordCount.Text = foundLabel
                    End If
                Else
                    If Grid.Visible Then
                        lblRecordCount.Text = foundLabel
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region " Datagrid Related "

        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles Grid.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim btnEditButtonCode As LinkButton
            Try
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then

                    If (e.Item.Cells(GRID_COL_CLAIM_NUMBER_IDX).FindControl(GRID_COL_CODE_CTRL) IsNot Nothing) Then
                        btnEditButtonCode = CType(e.Item.Cells(GRID_COL_CLAIM_NUMBER_IDX).FindControl(GRID_COL_CODE_CTRL), LinkButton)
                        btnEditButtonCode.Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_CLAIM_NUMBER).ToString
                        btnEditButtonCode.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimFollowUpSearchDV.COL_CLAIM_ID), Byte()))
                        btnEditButtonCode.CommandName = SELECT_ACTION_COMMAND
                    End If

                    e.Item.Cells(GRID_COL_CLAIM_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimFollowUpSearchDV.COL_CLAIM_ID), Byte()))
                    e.Item.Cells(GRID_COL_FOLLOWUP_DATE_IDX).Text = GetDateFormattedString(DateHelper.GetDateValue(dvRow(Claim.ClaimFollowUpSearchDV.COL_FOLLOWUP_DATE).ToString()))
                    e.Item.Cells(GRID_COL_LAST_ACTIVITY_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_NO_ACTIVITY).ToString
                    e.Item.Cells(GRID_COL_CLAIM_NUMBER_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_CLAIM_NUMBER).ToString
                    e.Item.Cells(GRID_COL_STATUS_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_STATUS_CODE).ToString
                    e.Item.Cells(GRID_COL_CLAIM_EXT_STATUS_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_EXTENDED_STATUS).ToString
                    e.Item.Cells(GRID_COL_STATUS_OWNER_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_OWNER).ToString
                    e.Item.Cells(GRID_COL_SERVICE_CENTER_NAME_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_SERVICE_CENTER_NAME).ToString
                    e.Item.Cells(GRID_COL_CLAIMS_ADJUSTER_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_CLAIMS_AJDUSTER).ToString
                    e.Item.Cells(GRID_COL_CUST_NAME_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_CUST_NAME).ToString
                    e.Item.Cells(GRID_COL_DEALER_CODE_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_DEALER_CODE).ToString


                    If dvRow(Claim.ClaimFollowUpSearchDV.COL_CLAIM_TAT) IsNot DBNull.Value Then
                        Dim claim_TAT As Integer = CType(dvRow(Claim.ClaimFollowUpSearchDV.COL_CLAIM_TAT), Integer)
                        e.Item.Cells(GRID_COL_CLAIM_TAT_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_CLAIM_TAT).ToString

                        Dim claim_TAT_colorCode As String = FindTATColor(claim_TAT)
                        If claim_TAT_colorCode IsNot Nothing AndAlso Not claim_TAT_colorCode.Equals(String.Empty) Then e.Item.Cells(GRID_COL_CLAIM_TAT_IDX).BackColor = ColorTranslator.FromHtml(claim_TAT_colorCode)
                    Else
                        e.Item.Cells(GRID_COL_CLAIM_TAT_IDX).Text = "N/A"
                    End If

                    If dvRow(Claim.ClaimFollowUpSearchDV.COL_NO_ACTIVITY) IsNot DBNull.Value Then
                        Dim no_activity As Integer = CType(dvRow(Claim.ClaimFollowUpSearchDV.COL_NO_ACTIVITY), Integer)
                        Dim no_activity_colorCode As String = FindTATColor(no_activity)
                        If no_activity_colorCode IsNot Nothing AndAlso Not no_activity_colorCode.Equals(String.Empty) Then e.Item.Cells(GRID_COL_LAST_ACTIVITY_IDX).BackColor = ColorTranslator.FromHtml(no_activity_colorCode)
                    Else
                        e.Item.Cells(GRID_COL_LAST_ACTIVITY_IDX).Text = "N/A"
                    End If

                    If dvRow(Claim.ClaimFollowUpSearchDV.COL_SVC_TAT) IsNot DBNull.Value Then
                        Dim SVC_TAT As Integer = CType(dvRow(Claim.ClaimFollowUpSearchDV.COL_SVC_TAT), Integer)
                        e.Item.Cells(GRID_COL_SVC_TAT_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_SVC_TAT).ToString
                        Dim SVC_TAT_colorCode As String = FindTATColor(SVC_TAT)
                        If SVC_TAT_colorCode IsNot Nothing AndAlso Not SVC_TAT_colorCode.Equals(String.Empty) Then e.Item.Cells(GRID_COL_SVC_TAT_IDX).BackColor = ColorTranslator.FromHtml(SVC_TAT_colorCode)
                    Else
                        e.Item.Cells(GRID_COL_SVC_TAT_IDX).Text = "N/A"
                    End If

                    If cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_DENIED Then
                        Grid.Columns(GRID_COL_DENIED_REASON_IDX).Visible = True
                        Grid.Columns(GRID_COL_COMMENT_TYPE_IDX).Visible = True
                        e.Item.Cells(GRID_COL_DENIED_REASON_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_DENIED_REASON).ToString
                        e.Item.Cells(GRID_COL_COMMENT_TYPE_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_COMMENT_TYPE).ToString
                    Else
                        Grid.Columns(GRID_COL_DENIED_REASON_IDX).Visible = False
                        Grid.Columns(GRID_COL_COMMENT_TYPE_IDX).Visible = False
                    End If
                    e.Item.Cells(GRID_COL_NUM_OF_REMINDERS_SENT_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_NUM_OF_REMINDERS).ToString
                    If dvRow(Claim.ClaimFollowUpSearchDV.COL_LAST_REMINDER_SEND_DATE) IsNot DBNull.Value Then
                        e.Item.Cells(GRID_COL_LAST_REMINDER_SENT_ON_IDX).Text = GetDateFormattedString(DateHelper.GetDateValue(dvRow(Claim.ClaimFollowUpSearchDV.COL_LAST_REMINDER_SEND_DATE).ToString()))
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Function FindTATColor(TAT As Integer) As String
            If State.TATDV Is Nothing Then
                State.TATDV = TurnAroundTimeRange.LoadListWithColor()
            End If
            If State.TATDV IsNot Nothing AndAlso State.TATDV.Count > 0 Then

                Dim i As Integer
                For i = 0 To State.TATDV.Count - 1
                    If CType(State.TATDV.Item(i)("min_days"), Integer) <= TAT AndAlso CType(State.TATDV.Item(i)("max_days"), Integer) >= TAT Then
                        Try
                            Dim c As Color = ColorTranslator.FromHtml(State.TATDV.Item(i)("color_code").ToString)
                        Catch ex As Exception
                            Throw New GUIException(Message.MSG_INVALID_COLOR_CODE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_COLOR_CODE)
                        End Try
                        Return (State.TATDV.Item(i)("color_code").ToString)
                    End If
                Next
            Else
                Return Nothing
            End If
        End Function

        'Public Function FindTATDesc(ByVal TAT As Integer) As String
        '    If Me.State.TATDV Is Nothing Then
        '        Me.State.TATDV = TurnAroundTimeRange.LoadListWithColor()
        '    End If
        '    If Not Me.State.TATDV Is Nothing AndAlso Me.State.TATDV.Count > 0 Then

        '        Dim i As Integer
        '        For i = 0 To Me.State.TATDV.Count - 1
        '            If CType(Me.State.TATDV.Item(i)("min_days"), Integer) <= TAT AndAlso CType(Me.State.TATDV.Item(i)("max_days"), Integer) >= TAT Then
        '                Return (Me.State.TATDV.Item(i)("description").ToString)
        '            End If
        '        Next
        '    Else
        '        Return Nothing
        '    End If
        'End Function

        Private Sub Grid_SortCommand(source As Object, e As DataGridSortCommandEventArgs) Handles Grid.SortCommand
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
        Public Sub ItemCommand(source As Object, e As DataGridCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    State.selectedClaimId = New Guid(e.Item.Cells(GRID_COL_CLAIM_ID_IDX).Text)
                    callPage(ClaimForm.URL, State.selectedClaimId)
                End If
            Catch ex As ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Public Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = e.NewPageIndex
                State.selectedClaimId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.PageSize = Grid.PageSize
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region " Buttons Clicks "

        Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            Try
                SetStateProperties()
                State.PageIndex = 0
                State.selectedClaimId = Guid.Empty
                State.IsGridVisible = True
                State.searchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
            Try
                TextBoxFollowUpDate.Text = String.Empty
                'Me.TextBoxSearchServiceCenterZIP.Text = String.Empty
                TextBoxSearchClaimNumber.Text = String.Empty
                TextBoxSearchClaimAdjuster.Text = String.Empty
                TextBoxSearchCustomerName.Text = String.Empty
                If moDealerDrop.SelectedIndex > 0 Then
                    moDealerDrop.SelectedIndex = 0
                End If
                '  Me.cboSearchServiceCenter.SelectedIndex = 0
                moServiceCenterText.Text = String.Empty
                If moClaimExtendedStatusDD.SelectedIndex > 0 Then
                    moClaimExtendedStatusDD.SelectedIndex = 0
                End If

                If moClaimTATRangeDD.SelectedIndex > 0 Then
                    moClaimTATRangeDD.SelectedIndex = 0
                End If

                If moLastActivityDD.SelectedIndex > 0 Then
                    moLastActivityDD.SelectedIndex = 0
                End If

                If moOwnerDD.SelectedIndex > 0 Then
                    moOwnerDD.SelectedIndex = 0
                End If

                If cboNonOperatedClaims.SelectedIndex > 0 Then
                    cboNonOperatedClaims.SelectedIndex = 0
                    ControlMgr.SetEnableControl(Me, cboClaimStatus, True)
                    ControlMgr.SetEnableControl(Me, moClaimExtendedStatusDD, True)
                End If
                'Update Page State
                With State
                    .followUpDate = Nothing
                    '.servCenterZip = Me.TextBoxSearchServiceCenterZIP.Text
                    .claimNumber = TextBoxSearchClaimNumber.Text
                    .claimAdjuster = TextBoxSearchClaimAdjuster.Text
                    .customerName = TextBoxSearchCustomerName.Text
                    .dealerId = Nothing
                    ' .serviceCenterId = Nothing
                    .serviceCenterName = moServiceCenterText.Text
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub cboNonOperatedClaims_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboNonOperatedClaims.SelectedIndexChanged
            Dim nonOperatedClaims As String = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, GetSelectedItem(cboNonOperatedClaims))
            If nonOperatedClaims = Codes.YESNO_Y Then
                ControlMgr.SetEnableControl(Me, cboClaimStatus, False)
                ControlMgr.SetEnableControl(Me, moClaimExtendedStatusDD, False)
                SetSelectedItem(cboClaimStatus, CLAIM_STATUS_DESCRIPTION_ACTIVE)
                moClaimExtendedStatusDD.SelectedIndex = 0
            Else
                ControlMgr.SetEnableControl(Me, cboClaimStatus, True)
                ControlMgr.SetEnableControl(Me, moClaimExtendedStatusDD, True)
                moClaimExtendedStatusDD.SelectedIndex = 0
            End If
        End Sub
#End Region

    End Class

End Namespace

