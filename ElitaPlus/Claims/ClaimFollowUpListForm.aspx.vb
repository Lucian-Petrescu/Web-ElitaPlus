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
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents ErrorCtrl As ErrorController

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("CLAIM FOLLOW UP SEARCH")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CLAIM FOLLOW UP SEARCH")
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

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Me.MenuEnabled = True
            Me.IsReturningFromChild = True
            Dim retObj As ClaimForm.ReturnType = CType(ReturnPar, ClaimForm.ReturnType)
            Try
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.selectedClaimId = retObj.EditingBo.Id
                            End If
                            Me.State.IsGridVisible = True
                            If retObj.BoChanged Then
                                Me.State.searchDV = Nothing
                            End If
                        End If
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Page_Events"
        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Page.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)
            Me.MasterPage.MessageController.Clear_Hide()

            Try
                If Not Me.IsPostBack Then
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Claim Follow Up")
                    UpdateBreadCrum()

                    setDefaultSearchButton()

                    ControlMgr.SetVisibleControl(Me, trPageSize, False)

                    Me.PopulateDealerDropdown(Me.moDealerDrop)
                    '  Me.PopulateServiceCenterDropdown(cboSearchServiceCenter)
                    Me.PopulateSortByDropDown()
                    Me.PopulateClaimStatusDropDown()
                    PopulateDropDowns()

                    Me.GetStateProperties()
                    If Me.State.IsGridVisible Then
                        If Not (Me.State.PageSize = 10) Then
                            cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                            Grid.PageSize = Me.State.PageSize
                        End If
                        Me.PopulateGrid()
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)
                    Me.AddCalendar(Me.BtnFollowUpDate, Me.TextBoxFollowUpDate)
                    SetFocus(Me.TextBoxSearchClaimNumber)
                Else
                    Me.ClearLabelErrSign(LabelFollowUpDate)
                End If
                Me.DisplayNewProgressBarOnClick(Me.btnSearch, "Loading_Claims")
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub

        Sub PopulateDropDowns()

            Try
                'Claim Extended Status Owner

                Dim oExtendedStatusOwnerLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("EXTOWN", Thread.CurrentPrincipal.GetLanguageCode())
                moOwnerDD.Populate(oExtendedStatusOwnerLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })

                If Not (Me.State.selectedClaimExtendedStatusOwnerId.Equals(Guid.Empty)) Then
                    Me.SetSelectedItem(Me.moOwnerDD, Me.State.selectedClaimExtendedStatusOwnerId)
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

                    If Not (Me.State.selectedClaimExtendedStatusId.Equals(Guid.Empty)) Then
                        Me.SetSelectedItem(Me.moClaimExtendedStatusDD, Me.State.selectedClaimExtendedStatusId)
                    End If
                Else
                    ControlMgr.SetVisibleControl(Me, Me.moClaimExtendedStatusDD, False)
                    ControlMgr.SetVisibleControl(Me, Me.LabelSearchClaimExtendedStatus, False)
                    ControlMgr.SetVisibleControl(Me, Me.moOwnerDD, False)
                    ControlMgr.SetVisibleControl(Me, Me.lblOwner, False)
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
                    If Not (Me.State.selectedClaimTATId.Equals(Guid.Empty)) Then
                        Me.SetSelectedItem(Me.moClaimTATRangeDD, Me.State.selectedClaimTATId)
                    End If
                Else
                    ControlMgr.SetVisibleControl(Me, Me.moClaimTATRangeDD, False)
                    ControlMgr.SetVisibleControl(Me, Me.LabelSearchClaimTATRange, False)
                End If

                'No Activity Time Range list

                Dim activityTimeRangeLkl As ListItem() = CommonConfigManager.Current.ListManager.GetList("LkScTatByGroupList", Thread.CurrentPrincipal.GetLanguageCode(), listcontext)

                If Not activityTimeRangeLkl.Count = 0 Then

                    Me.moLastActivityDD.Populate(activityTimeRangeLkl, New PopulateOptions() With
                    {
                    .AddBlankItem = True
                    })
                    If Not (Me.State.selectedNoActivityTATId.Equals(Guid.Empty)) Then
                        Me.SetSelectedItem(Me.moLastActivityDD, Me.State.selectedNoActivityTATId)
                    End If
                Else
                    ControlMgr.SetVisibleControl(Me, Me.moLastActivityDD, False)
                    ControlMgr.SetVisibleControl(Me, Me.LabelSearchLastActivity, False)
                End If

                Dim oNonOperatedClaims As ListItem() = CommonConfigManager.Current.ListManager.GetList("YESNO", Thread.CurrentPrincipal.GetLanguageCode())
                cboNonOperatedClaims.Populate(oNonOperatedClaims, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                If Not (Me.State.selectedNonOperatedClaimsId.Equals(Guid.Empty)) Then
                    Me.SetSelectedItem(Me.cboNonOperatedClaims, Me.State.selectedNonOperatedClaimsId)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SetStateProperties()
            Try
                If String.IsNullOrEmpty(Me.TextBoxFollowUpDate.Text) Then
                    Me.State.followUpDate = Nothing
                Else
                    Me.State.followUpDate = DateHelper.GetDateValue(Me.TextBoxFollowUpDate.Text)
                End If

            Catch ex As Exception
                Me.SetLabelError(LabelFollowUpDate)
                Throw New GUIException(Message.MSG_BEGIN_END_DATE, Message.MSG_INVALID_DATE)
            End Try
            'Me.State.servCenterZip = Me.TextBoxSearchServiceCenterZIP.Text.ToUpper
            Me.State.claimNumber = Me.TextBoxSearchClaimNumber.Text.ToUpper
            Me.State.claimAdjuster = Me.TextBoxSearchClaimAdjuster.Text.ToUpper
            Me.State.customerName = Me.TextBoxSearchCustomerName.Text.ToUpper
            Me.State.dealerId = Me.GetSelectedItem(moDealerDrop)
            Me.State.dealerName = Me.GetSelectedDescription(moDealerDrop)
            Me.State.dealer = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, Me.State.dealerId)
            '  Me.State.serviceCenterId = Me.GetSelectedItem(cboSearchServiceCenter)
            '  Me.State.serviceCenterName = Me.GetSelectedDescription(cboSearchServiceCenter)
            Me.State.serviceCenterName = moServiceCenterText.Text.ToUpper
            Me.State.selectedSortById = Me.GetSelectedItem(Me.cboSortBy)

            Me.State.selectedClaimExtendedStatusId = Me.GetSelectedItem(Me.moClaimExtendedStatusDD)
            Me.State.selectedClaimExtendedStatusOwnerId = Me.GetSelectedItem(Me.moOwnerDD)
            Me.State.selectedClaimTATId = Me.GetSelectedItem(Me.moClaimTATRangeDD)
            Me.State.selectedNoActivityTATId = Me.GetSelectedItem(Me.moLastActivityDD)
            Me.State.selectedNonOperatedClaimsId = Me.GetSelectedItem(Me.cboNonOperatedClaims)
            If cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_ACTIVE Then
                Me.State.claimStatus = Codes.CLAIM_STATUS__ACTIVE
            ElseIf cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_DENIED Then
                Me.State.claimStatus = Codes.CLAIM_STATUS__DENIED
            End If

        End Sub

        Private Sub GetStateProperties()
            Try
                If Me.State.followUpDate Is Nothing Then
                    Me.TextBoxFollowUpDate.Text = String.Empty
                Else
                    Me.TextBoxFollowUpDate.Text = CType(Me.State.followUpDate.Value, String)
                End If

                'Me.TextBoxSearchServiceCenterZIP.Text = Me.State.servCenterZip
                Me.TextBoxSearchClaimNumber.Text = Me.State.claimNumber
                Me.TextBoxSearchClaimAdjuster.Text = Me.State.claimAdjuster
                Me.TextBoxSearchCustomerName.Text = Me.State.customerName
                Me.SetSelectedItem(Me.moDealerDrop, Me.State.dealerId)

                If Not Me.State.selectedClaimExtendedStatusId.Equals(Guid.Empty) Then
                    Me.SetSelectedItem(Me.moClaimExtendedStatusDD, Me.State.selectedClaimExtendedStatusId)
                End If

                If Not Me.State.selectedClaimExtendedStatusOwnerId.Equals(Guid.Empty) Then
                    Me.SetSelectedItem(Me.moOwnerDD, Me.State.selectedClaimExtendedStatusOwnerId)
                End If

                If Not Me.State.selectedClaimTATId.Equals(Guid.Empty) Then
                    Me.SetSelectedItem(Me.moClaimTATRangeDD, Me.State.selectedClaimTATId)
                End If

                If Not Me.State.selectedNoActivityTATId.Equals(Guid.Empty) Then
                    Me.SetSelectedItem(Me.moClaimTATRangeDD, Me.State.selectedNoActivityTATId)
                End If

                '   Me.SetSelectedItem(Me.cboSearchServiceCenter, Me.State.serviceCenterId)
                moServiceCenterText.Text = Me.State.serviceCenterName
                Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub setDefaultSearchButton()

            Try
                Me.SetDefaultButton(Me.TextBoxFollowUpDate, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchClaimAdjuster, btnSearch)
                'Me.SetDefaultButton(Me.TextBoxSearchServiceCenterZIP, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchClaimNumber, btnSearch)
                Me.SetDefaultButton(Me.TextBoxSearchCustomerName, btnSearch)
                Me.SetDefaultButton(Me.moServiceCenterText, btnSearch)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateDealerDropdown(ByVal dealerDropDownList As DropDownList)
            ' Dim oCompanyId As Guid = Me.GetApplicationUser.CompanyID
            Try

                Me.BindListControlToDataView(dealerDropDownList,
                LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code"), , , True)
                '   Me.SetSelectedItem(dealerDropDownList, Me.State.dealerId)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Sub PopulateClaimStatusDropDown()
            Try
                cboClaimStatus.Items.Add(New System.Web.UI.WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_ACTIVE))
                cboClaimStatus.Items.Add(New System.Web.UI.WebControls.ListItem(CLAIM_STATUS_DESCRIPTION_DENIED))

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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

                If (Me.State.selectedSortById.Equals(Guid.Empty)) Then
                    Me.SetSelectedItem(Me.cboSortBy, defaultSelectedCodeId)
                    Me.State.selectedSortById = defaultSelectedCodeId
                Else
                    Me.SetSelectedItem(Me.cboSortBy, Me.State.selectedSortById)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Controlling Logic"

        Public Sub PopulateGrid()
            Dim foundLabel As String
            '   Dim oCompanyId As Guid = Me.GetApplicationUser.CompanyID
            Dim errors() As ValidationError = {New ValidationError(ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, GetType(Claim), Nothing, "Search", Nothing)}

            Try
                Me.GetSelectedItem(moDealerDrop)
                '   Me.GetSelectedItem(cboSearchServiceCenter)

                If (Me.State.searchDV Is Nothing) Then

                    Dim searchFollowUpDate As String = Nothing

                    If Not (Me.State.followUpDate Is Nothing) Then
                        Dim tempFollowDate As DateTime = New DateTime
                        tempFollowDate = DateHelper.GetDateValue(Me.State.followUpDate.ToString())
                        searchFollowUpDate = tempFollowDate.ToString("MMddyyyy")
                    Else
                        searchFollowUpDate = String.Empty
                    End If

                    Dim nonOperatedClaims As String = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.State.selectedNonOperatedClaimsId)

                    Dim sortBy As String

                    If (Not (Me.State.selectedSortById.Equals(Guid.Empty))) Then
                        sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CLAIM_FOLLOWUP_SEARCH_FIELDS, Me.State.selectedSortById)

                        Me.State.searchDV = Claim.GetClaimFollowUpList(searchFollowUpDate,
                                                                           Me.State.serviceCenterName,
                                                                           Me.State.claimNumber,
                                                                           Me.State.claimAdjuster,
                                                                           Me.State.customerName,
                                                                           Me.State.claimStatus,
                                                                           Me.State.dealerId,
                                                                           Me.State.selectedClaimTATId,
                                                                           Me.State.selectedClaimExtendedStatusId,
                                                                           Me.State.selectedNoActivityTATId,
                                                                           Me.State.selectedClaimExtendedStatusOwnerId,
                                                                           ElitaPlusIdentity.Current.ActiveUser.LanguageId,
                                                                           nonOperatedClaims,
                                                                           sortBy)
                    Else
                        Me.State.searchDV = Claim.GetClaimFollowUpList(searchFollowUpDate,
                                                                          Me.State.serviceCenterName,
                                                                          Me.State.claimNumber,
                                                                          Me.State.claimAdjuster,
                                                                          Me.State.customerName,
                                                                          Me.State.claimStatus,
                                                                          Me.State.dealerId,
                                                                          Me.State.selectedClaimTATId,
                                                                          Me.State.selectedClaimExtendedStatusId,
                                                                          Me.State.selectedNoActivityTATId,
                                                                          Me.State.selectedClaimExtendedStatusOwnerId,
                                                                          ElitaPlusIdentity.Current.ActiveUser.LanguageId,
                                                                          nonOperatedClaims)
                    End If

                    Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                End If

                'Me.State.searchDV.Sort = Grid.DataMember
                Me.State.searchDV.Sort = Me.State.SortExpression
                Me.Grid.AutoGenerateColumns = False
                Me.Grid.PageSize = Me.State.PageSize

                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedClaimId, Me.Grid, Me.State.PageIndex)
                Me.State.PageIndex = Me.Grid.CurrentPageIndex
                Me.Grid.DataSource = Me.State.searchDV
                'Me.Grid.AllowSorting = False
                Me.Grid.Columns(Me.GRID_COL_FOLLOWUP_DATE_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_FOLLOWUP_DATE
                Me.Grid.Columns(Me.GRID_COL_CLAIM_TAT_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_CLAIM_TAT
                Me.Grid.Columns(Me.GRID_COL_SVC_TAT_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_SVC_TAT
                Me.Grid.Columns(Me.GRID_COL_LAST_ACTIVITY_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_NO_ACTIVITY
                Me.Grid.Columns(Me.GRID_COL_CLAIM_NUMBER_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_CLAIM_NUMBER
                Me.Grid.Columns(Me.GRID_COL_CUST_NAME_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_CUST_NAME
                Me.Grid.Columns(Me.GRID_COL_SERVICE_CENTER_NAME_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_SERVICE_CENTER_NAME
                Me.Grid.Columns(Me.GRID_COL_CLAIMS_ADJUSTER_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_CLAIMS_AJDUSTER
                Me.Grid.Columns(Me.GRID_COL_CLAIM_EXT_STATUS_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_EXTENDED_STATUS
                Me.Grid.Columns(Me.GRID_COL_STATUS_OWNER_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_OWNER
                Me.Grid.Columns(Me.GRID_COL_STATUS_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_STATUS_CODE
                Me.Grid.Columns(Me.GRID_COL_DEALER_CODE_IDX).SortExpression = Claim.ClaimFollowUpSearchDV.COL_DEALER_CODE

                Me.Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)

                Session("recCount") = Me.State.searchDV.Count

                foundLabel = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIMS_FOUND)
                If Me.State.searchDV.Count > 0 Then
                    If Me.Grid.Visible Then
                        Me.lblRecordCount.Text = foundLabel
                    End If
                Else
                    If Me.Grid.Visible Then
                        Me.lblRecordCount.Text = foundLabel
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region " Datagrid Related "

        'The Binding Logic is here
        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
            Dim btnEditButtonCode As LinkButton
            Try
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                    If (Not e.Item.Cells(GRID_COL_CLAIM_NUMBER_IDX).FindControl(GRID_COL_CODE_CTRL) Is Nothing) Then
                        btnEditButtonCode = CType(e.Item.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX).FindControl(GRID_COL_CODE_CTRL), LinkButton)
                        btnEditButtonCode.Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_CLAIM_NUMBER).ToString
                        btnEditButtonCode.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimFollowUpSearchDV.COL_CLAIM_ID), Byte()))
                        btnEditButtonCode.CommandName = SELECT_ACTION_COMMAND
                    End If

                    e.Item.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(Claim.ClaimFollowUpSearchDV.COL_CLAIM_ID), Byte()))
                    e.Item.Cells(Me.GRID_COL_FOLLOWUP_DATE_IDX).Text = Me.GetDateFormattedString(DateHelper.GetDateValue(dvRow(Claim.ClaimFollowUpSearchDV.COL_FOLLOWUP_DATE).ToString()))
                    e.Item.Cells(Me.GRID_COL_LAST_ACTIVITY_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_NO_ACTIVITY).ToString
                    e.Item.Cells(Me.GRID_COL_CLAIM_NUMBER_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_CLAIM_NUMBER).ToString
                    e.Item.Cells(Me.GRID_COL_STATUS_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_STATUS_CODE).ToString
                    e.Item.Cells(Me.GRID_COL_CLAIM_EXT_STATUS_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_EXTENDED_STATUS).ToString
                    e.Item.Cells(Me.GRID_COL_STATUS_OWNER_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_OWNER).ToString
                    e.Item.Cells(Me.GRID_COL_SERVICE_CENTER_NAME_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_SERVICE_CENTER_NAME).ToString
                    e.Item.Cells(Me.GRID_COL_CLAIMS_ADJUSTER_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_CLAIMS_AJDUSTER).ToString
                    e.Item.Cells(Me.GRID_COL_CUST_NAME_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_CUST_NAME).ToString
                    e.Item.Cells(Me.GRID_COL_DEALER_CODE_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_DEALER_CODE).ToString


                    If Not dvRow(Claim.ClaimFollowUpSearchDV.COL_CLAIM_TAT) Is System.DBNull.Value Then
                        Dim claim_TAT As Integer = CType(dvRow(Claim.ClaimFollowUpSearchDV.COL_CLAIM_TAT), Integer)
                        e.Item.Cells(Me.GRID_COL_CLAIM_TAT_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_CLAIM_TAT).ToString

                        Dim claim_TAT_colorCode As String = FindTATColor(claim_TAT)
                        If Not claim_TAT_colorCode Is Nothing AndAlso Not claim_TAT_colorCode.Equals(String.Empty) Then e.Item.Cells(Me.GRID_COL_CLAIM_TAT_IDX).BackColor = System.Drawing.ColorTranslator.FromHtml(claim_TAT_colorCode)
                    Else
                        e.Item.Cells(Me.GRID_COL_CLAIM_TAT_IDX).Text = "N/A"
                    End If

                    If Not dvRow(Claim.ClaimFollowUpSearchDV.COL_NO_ACTIVITY) Is System.DBNull.Value Then
                        Dim no_activity As Integer = CType(dvRow(Claim.ClaimFollowUpSearchDV.COL_NO_ACTIVITY), Integer)
                        Dim no_activity_colorCode As String = FindTATColor(no_activity)
                        If Not no_activity_colorCode Is Nothing AndAlso Not no_activity_colorCode.Equals(String.Empty) Then e.Item.Cells(Me.GRID_COL_LAST_ACTIVITY_IDX).BackColor = System.Drawing.ColorTranslator.FromHtml(no_activity_colorCode)
                    Else
                        e.Item.Cells(Me.GRID_COL_LAST_ACTIVITY_IDX).Text = "N/A"
                    End If

                    If Not dvRow(Claim.ClaimFollowUpSearchDV.COL_SVC_TAT) Is System.DBNull.Value Then
                        Dim SVC_TAT As Integer = CType(dvRow(Claim.ClaimFollowUpSearchDV.COL_SVC_TAT), Integer)
                        e.Item.Cells(Me.GRID_COL_SVC_TAT_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_SVC_TAT).ToString
                        Dim SVC_TAT_colorCode As String = FindTATColor(SVC_TAT)
                        If Not SVC_TAT_colorCode Is Nothing AndAlso Not SVC_TAT_colorCode.Equals(String.Empty) Then e.Item.Cells(Me.GRID_COL_SVC_TAT_IDX).BackColor = System.Drawing.ColorTranslator.FromHtml(SVC_TAT_colorCode)
                    Else
                        e.Item.Cells(Me.GRID_COL_SVC_TAT_IDX).Text = "N/A"
                    End If

                    If cboClaimStatus.SelectedItem.Text = CLAIM_STATUS_DESCRIPTION_DENIED Then
                        Grid.Columns(Me.GRID_COL_DENIED_REASON_IDX).Visible = True
                        Grid.Columns(Me.GRID_COL_COMMENT_TYPE_IDX).Visible = True
                        e.Item.Cells(Me.GRID_COL_DENIED_REASON_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_DENIED_REASON).ToString
                        e.Item.Cells(Me.GRID_COL_COMMENT_TYPE_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_COMMENT_TYPE).ToString
                    Else
                        Grid.Columns(Me.GRID_COL_DENIED_REASON_IDX).Visible = False
                        Grid.Columns(Me.GRID_COL_COMMENT_TYPE_IDX).Visible = False
                    End If
                    e.Item.Cells(Me.GRID_COL_NUM_OF_REMINDERS_SENT_IDX).Text = dvRow(Claim.ClaimFollowUpSearchDV.COL_NUM_OF_REMINDERS).ToString
                    If Not dvRow(Claim.ClaimFollowUpSearchDV.COL_LAST_REMINDER_SEND_DATE) Is System.DBNull.Value Then
                        e.Item.Cells(Me.GRID_COL_LAST_REMINDER_SENT_ON_IDX).Text = Me.GetDateFormattedString(DateHelper.GetDateValue(dvRow(Claim.ClaimFollowUpSearchDV.COL_LAST_REMINDER_SEND_DATE).ToString()))
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Function FindTATColor(ByVal TAT As Integer) As String
            If Me.State.TATDV Is Nothing Then
                Me.State.TATDV = TurnAroundTimeRange.LoadListWithColor()
            End If
            If Not Me.State.TATDV Is Nothing AndAlso Me.State.TATDV.Count > 0 Then

                Dim i As Integer
                For i = 0 To Me.State.TATDV.Count - 1
                    If CType(Me.State.TATDV.Item(i)("min_days"), Integer) <= TAT AndAlso CType(Me.State.TATDV.Item(i)("max_days"), Integer) >= TAT Then
                        Try
                            Dim c As Color = System.Drawing.ColorTranslator.FromHtml(Me.State.TATDV.Item(i)("color_code").ToString)
                        Catch ex As Exception
                            Throw New GUIException(Message.MSG_INVALID_COLOR_CODE, Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVALID_COLOR_CODE)
                        End Try
                        Return (Me.State.TATDV.Item(i)("color_code").ToString)
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

        Private Sub Grid_SortCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridSortCommandEventArgs) Handles Grid.SortCommand
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
        Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    Me.State.selectedClaimId = New Guid(e.Item.Cells(Me.GRID_COL_CLAIM_ID_IDX).Text)
                    Me.callPage(ClaimForm.URL, Me.State.selectedClaimId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.State.selectedClaimId = Guid.Empty
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.PageSize = Grid.PageSize
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region " Buttons Clicks "

        Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                SetStateProperties()
                Me.State.PageIndex = 0
                Me.State.selectedClaimId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.searchDV = Nothing
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
            Try
                Me.TextBoxFollowUpDate.Text = String.Empty
                'Me.TextBoxSearchServiceCenterZIP.Text = String.Empty
                Me.TextBoxSearchClaimNumber.Text = String.Empty
                Me.TextBoxSearchClaimAdjuster.Text = String.Empty
                Me.TextBoxSearchCustomerName.Text = String.Empty
                If Me.moDealerDrop.SelectedIndex > 0 Then
                    Me.moDealerDrop.SelectedIndex = 0
                End If
                '  Me.cboSearchServiceCenter.SelectedIndex = 0
                Me.moServiceCenterText.Text = String.Empty
                If Me.moClaimExtendedStatusDD.SelectedIndex > 0 Then
                    Me.moClaimExtendedStatusDD.SelectedIndex = 0
                End If

                If Me.moClaimTATRangeDD.SelectedIndex > 0 Then
                    Me.moClaimTATRangeDD.SelectedIndex = 0
                End If

                If Me.moLastActivityDD.SelectedIndex > 0 Then
                    Me.moLastActivityDD.SelectedIndex = 0
                End If

                If Me.moOwnerDD.SelectedIndex > 0 Then
                    Me.moOwnerDD.SelectedIndex = 0
                End If

                If Me.cboNonOperatedClaims.SelectedIndex > 0 Then
                    Me.cboNonOperatedClaims.SelectedIndex = 0
                    ControlMgr.SetEnableControl(Me, cboClaimStatus, True)
                    ControlMgr.SetEnableControl(Me, moClaimExtendedStatusDD, True)
                End If
                'Update Page State
                With Me.State
                    .followUpDate = Nothing
                    '.servCenterZip = Me.TextBoxSearchServiceCenterZIP.Text
                    .claimNumber = Me.TextBoxSearchClaimNumber.Text
                    .claimAdjuster = Me.TextBoxSearchClaimAdjuster.Text
                    .customerName = Me.TextBoxSearchCustomerName.Text
                    .dealerId = Nothing
                    ' .serviceCenterId = Nothing
                    .serviceCenterName = moServiceCenterText.Text
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub cboNonOperatedClaims_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboNonOperatedClaims.SelectedIndexChanged
            Dim nonOperatedClaims As String = LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, Me.GetSelectedItem(Me.cboNonOperatedClaims))
            If nonOperatedClaims = Codes.YESNO_Y Then
                ControlMgr.SetEnableControl(Me, cboClaimStatus, False)
                ControlMgr.SetEnableControl(Me, moClaimExtendedStatusDD, False)
                Me.SetSelectedItem(cboClaimStatus, CLAIM_STATUS_DESCRIPTION_ACTIVE)
                Me.moClaimExtendedStatusDD.SelectedIndex = 0
            Else
                ControlMgr.SetEnableControl(Me, cboClaimStatus, True)
                ControlMgr.SetEnableControl(Me, moClaimExtendedStatusDD, True)
                Me.moClaimExtendedStatusDD.SelectedIndex = 0
            End If
        End Sub
#End Region

    End Class

End Namespace

