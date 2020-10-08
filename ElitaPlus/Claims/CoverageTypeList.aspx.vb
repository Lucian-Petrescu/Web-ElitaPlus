Imports System.Diagnostics
Imports System.Threading
Imports Assurant.ElitaPlus.DALObjects
Imports Microsoft.VisualBasic
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements

Namespace Claims
    Partial Public Class CoverageTypeList
        Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants"
        Public Const GRID_COL_EDIT_IDX As Integer = 7
        Public Const GRID_COL_CERT_ITEM_COVERAGE_IDX As Integer = 0
        Public Const GRID_COL_RISK_TYPE_DESCRIPTION_IDX As Integer = 1
        Public Const GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX As Integer = 2
        Public Const GRID_COL_CAUSE_OF_LOSS_IDX As Integer = 3
        Public Const GRID_COL_AUTHORIZED_AMT_IDX As Integer = 4
        Public Const GRID_COL_BEGIN_DATE_IDX As Integer = 5
        Public Const GRID_COL_END_DATE_IDX As Integer = 6
        Public Const GRID_CAUSE_OF_LOSS_ID_DDL As String = "cboCauseOfLossId"
        Public Const GRID_AUTHORIZED_AMOUNT As String = "txtAuthAmt"
        Public Const GRID_Label_AUTHORIZATION_AMOUNT As String = "lblAUTHORIZATION_AMOUNT"
        Public Const GRID_Label_CAUSE_OF_LOSS As String = "lblCauseofLoss"
        Public Const MSG_COVERAGE_CHANGED_SUCCESSFULLY As String = "COVERAGE_CHANGED_SUCCESSFULLY"
        Public Const MSG_COVERAGE_NOT_IN_EFFECT As String = "COVERAGE_NOT_IN_EFFECT"
        Public Const URL As String = "~/Claims/CoverageTypeList.aspx"
        Private Const CAUSE_OF_LOSS_ID_PROPERTY As String = "CauseOfLossId"
        Private Const AUTHORIZED_AMOUNT_PROPERTY As String = "AuthorizedAmount"
        Private Const CLAIM_SPECIAL_SERVICE_ID_PROPERTY As String = "ClaimSpecialServiceId"
        Private Const WHO_PAYS_ID_PROPERTY As String = "WhoPaysId"
        Public ZERO_DECIMAL As Decimal = 0D

        Private Const Claims As String = "Claims"
        Private Const CHANGE_COVERAGE As String = "CHANGE_COVERAGE"
#End Region
#Region "Member Variables"

        Protected TempDataView As DataView = New DataView
        'Private Shared pageIndex As Integer
        'Private Shared pageCount As Integer
        'Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        'Protected WithEvents btnSave As System.Web.UI.WebControls.Button
        'Protected WithEvents btnUndo As System.Web.UI.WebControls.Button
        'Protected WithEvents LbPage As System.Web.UI.WebControls.Label
        'Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        'Protected WithEvents Button2 As System.Web.UI.WebControls.Button
        'Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
        'Protected WithEvents tsHoriz As Microsoft.Web.UI.WebControls.TabStrip
        'Protected WithEvents ddlCancellationReason As System.Web.UI.WebControls.DropDownList
        'Protected WithEvents mpHoriz As Microsoft.Web.UI.WebControls.MultiPage


#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public PageIndex As Integer = 0
            Public claimId As Guid = Guid.Empty
            Public certId As Guid = Guid.Empty
            Public certItemCoverageId As Guid = Guid.Empty
            Public causeoflossId As Guid = Guid.Empty
            Public PageSize As Integer = DEFAULT_NEW_UI_PAGE_SIZE
            Public claimSpecialServiceId As Guid = Guid.Empty
            'LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), "N") 'Guid.Empty          
            Public lossDate As Date
            Public StatusCode As String
            Public InvoiceProcessDate As Date
            Public selectedCertItemCoverageId As Guid = Guid.Empty
            Public searchDV As CertItemCoverage.CertItemCoverageSearchDV = Nothing
            Public IsGridVisible As Boolean = True
            Public SearchClicked As Boolean
            Public authorizedAmount As String
            Public idx As Integer = 0
            Public selectControl As DropDownList
            Public selectTextBoxControl As TextBox
            Public isEditMode As Boolean
            Public isSpecialServiceCase As Boolean ' to store whether the claim is single/multi/special service


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
        Public Property SortDirection() As String
            Get
                If ViewState("SortDirection") IsNot Nothing Then
                    Return ViewState("SortDirection").ToString
                Else
                    Return String.Empty
                End If

            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public ClaimBO As ClaimBase
            Public HasDataChanged As Boolean

            Public Sub New(LastOp As DetailPageCommand, ClaimBO As ClaimBase, hasDataChanged As Boolean)
                LastOperation = LastOp
                Me.ClaimBO = ClaimBO
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page_Events"

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            MasterPage.MessageController.Clear_Hide()
            Try
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(Claims)
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(CHANGE_COVERAGE)
                UpdateBreadCrum()

                Dim ClaimBO As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.claimId)
                State.authorizedAmount = CType(ClaimBO.AuthorizedAmount, String)

                moClaimNumberText.Text = ClaimBO.ClaimNumber
                EnableDisableControls(moClaimNumberText, False)
                moDealerText.Text = ClaimBO.DealerName
                EnableDisableControls(moCertificateText, False)
                moCertificateText.Text = ClaimBO.CertificateNumber
                EnableDisableControls(moDealerText, False)
                EnableDisableButtons()
                CheckIfComingFromSaveConfirm()
                If Not IsPostBack Then
                    TranslateGridHeader(Grid)
                    PopulateGrid()
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)

        End Sub

        Private Sub UpdateBreadCrum()

            If (State IsNot Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage(CHANGE_COVERAGE)
            End If

        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            If confResponse IsNot Nothing AndAlso confResponse = MSG_VALUE_OK Then
                HiddenSaveChangesPromptResponse.Value = ""
                Back(DetailPageCommand.Back)
                'Me.callPage(ClaimForm.URL, Me.State.claimId)
            End If
        End Sub


        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    State.lossDate = CType(CType(CallingPar, ArrayList)(0), Date)
                    State.claimId = CType(CType(CallingPar, ArrayList)(1), Guid)
                    State.certId = CType(CType(CallingPar, ArrayList)(2), Guid)
                    State.certItemCoverageId = CType(CType(CallingPar, ArrayList)(3), Guid)
                    State.StatusCode = CType(CType(CallingPar, ArrayList)(4), String)
                    State.InvoiceProcessDate = CType(CType(CallingPar, ArrayList)(5), Date)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Controlling Logic"
        Protected Sub cboCauseOfLossId_SelectedIndexChanged(sender As Object, e As EventArgs)
            Try
                Dim ddlCauseOfLoss As DropDownList = CType(sender, DropDownList)
                'Dim cell As TableCell = CType(ddCauseOfLoss.Parent, TableCell)
                Dim index As Integer = CType(ddlCauseOfLoss.NamingContainer, GridViewRow).RowIndex
                ' Dim item As DataGridItem = CType(cell.Parent, DataGridItem)
                Dim content As String = Grid.Rows(index).Cells(0).Text 'item.Cells(0).Text
                State.causeoflossId = GetSelectedItem(ddlCauseOfLoss)
                Dim txtType As TextBox = CType(Grid.Rows(index).Cells(GRID_COL_AUTHORIZED_AMT_IDX).FindControl(GRID_AUTHORIZED_AMOUNT), TextBox)

                PopulateAuthorizedAmountFromPGPrices()

                EnableDisableButtons()

                Grid.DataSource = State.searchDV
                Grid.DataBind()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Sub PopulateAuthorizedAmountFromPGPrices()
            Dim retVal As Boolean
            Dim ClaimBO As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.claimId)
            ClaimBO.CertItemCoverageId = State.selectedCertItemCoverageId
            ClaimBO.CauseOfLossId = State.causeoflossId

            Select Case ClaimBO.ClaimAuthorizationType
                Case ClaimAuthorizationType.Single
                    State.claimSpecialServiceId = CType(ClaimBO, Claim).GetSpecialServiceValue()
                    CType(ClaimBO, Claim).ClaimSpecialServiceId = State.claimSpecialServiceId
                    retVal = CType(ClaimBO, Claim).CalculateAuthorizedAmountFromPGPrices()
                Case ClaimAuthorizationType.Multiple
                    retVal = False
                Case ClaimAuthorizationType.None
                    Throw New NotImplementedException
            End Select

            State.authorizedAmount = ClaimBO.AuthorizedAmount.ToString
            State.isSpecialServiceCase = retVal

        End Sub


        Sub PopulateCauseOfLossDropDown(CovTypeId As Guid, ddl As DropDownList)
            Try
                Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                'Me.BindListControlToDataView(ddl, LookupListNew.GetCauseOfLossByCoverageTypeLookupList(Authentication.LangId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, CovTypeId, , True))

                Dim certificateBO As New Certificate(State.certId)

               ' Me.BindListControlToDataView(ddl, LookupListNew.GetCauseOfLossByCoverageTypeAndSplSvcLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, CovTypeId, certificateBO.DealerId, Authentication.LangId, certificateBO.ProductCode, , False))
                Dim oListContext As ListContext = New ListContext()
                oListContext.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                oListContext.CoverageTypeId = CovTypeId
                oListContext.LanguageId = Authentication.LangId
                oListContext.DealerId = certificateBO.DealerId
                oListContext.ProductCode = certificateBO.ProductCode
                Dim coverageList As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CauseOfLossByCoverageTypeAndSplSvcLookupList", context:=oListContext)
                ddl.Populate(coverageList, New PopulateOptions() With
                                                   {
                                                   .AddBlankItem = True
                                                   })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub PopulateGrid()

            Try
                Dim blnNewSearch As Boolean = False
                If (State.searchDV Is Nothing) Then
                    State.searchDV = CertItemCoverage.GetClaimCoverageType(State.certId, State.certItemCoverageId, State.lossDate, State.StatusCode, State.InvoiceProcessDate)
                    blnNewSearch = True
                End If
                ' ''''
                Grid.AutoGenerateColumns = False
                State.searchDV.Sort = SortDirection
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedCertItemCoverageId, Grid, State.PageIndex)
                ' Me.State.PageIndex = Me.Grid.PageIndex
                'Me.Grid.DataSource = Me.State.searchDV
                'Me.Grid.AllowSorting = True
                'Me.Grid.PagerStyle.HorizontalAlign = HorizontalAlign.Center

                'Me.Grid.DataBind()
                Grid.AutoGenerateColumns = False
                ' Me.Grid.Columns(Me.GRID_COL_RISK_TYPE_DESCRIPTION_IDX).SortExpression = CertItemCoverage.CertItemCoverageSearchDV.COL_ISK_TYPE
                ' Me.Grid.Columns(Me.GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).SortExpression = CertItemCoverage.CertItemCoverageSearchDV.COL_COVERAGE_TYPE
                SortAndBindGrid(blnNewSearch)

                ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

            TranslateGridControls(Grid)

            If (State.searchDV.Count = 0) Then
                State.searchDV = Nothing

                Grid.Rows(0).Visible = False

                State.IsGridVisible = False
                If blnShowErr Then
                    MasterPage.MessageController.AddInformation(Message.MSG_NO_RECORDS_FOUND, True)
                End If
            Else
                Grid.Enabled = True
                Grid.PageSize = State.PageSize
                Grid.DataSource = State.searchDV
                State.IsGridVisible = True
                HighLightSortColumn(Grid, SortDirection)
                Grid.DataBind()
            End If


            ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
            'ControlMgr.SetVisibleControl(Me, moSearchResults, Me.State.IsGridVisible)

            Session("recCount") = State.searchDV.Count

            If State.searchDV.Count > 0 Then
                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If

        End Sub
        Private Sub EnableDisableButtons()
            If Not State.selectedCertItemCoverageId.Equals(Guid.Empty) Then
                ControlMgr.SetEnableControl(Me, btnChangeCoverage, True)
            Else
                ControlMgr.SetEnableControl(Me, btnChangeCoverage, False)
            End If
        End Sub

#End Region


#Region " Datagrid Related "

        Private Sub Grid_ItemDataBound(sender As Object, e As GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim lbl As Label
                Dim txt As TextBox
                Dim ddl As DropDownList

                'If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Or itemType = ListItemType.EditItem Then
                If e.Row.RowType = DataControlRowType.DataRow Then
                    '''Edit only on the row that was clicked by the user.
                    If State.isEditMode AndAlso Grid.EditIndex = e.Row.RowIndex Then
                        If (e.Row.Cells(GRID_COL_AUTHORIZED_AMT_IDX).FindControl(GRID_AUTHORIZED_AMOUNT) IsNot Nothing) Then
                            txt = CType(e.Row.Cells(GRID_COL_AUTHORIZED_AMT_IDX).FindControl(GRID_AUTHORIZED_AMOUNT), TextBox)
                            '''''if claim is of special service then allow the user to edit the authorization amount
                            If (State.isSpecialServiceCase) Then
                                txt.Enabled = True
                            Else
                                txt.Enabled = False
                            End If
                            txt.Text = State.authorizedAmount
                        End If
                        If (e.Row.Cells(GRID_COL_CAUSE_OF_LOSS_IDX).FindControl(GRID_CAUSE_OF_LOSS_ID_DDL) IsNot Nothing) Then
                            ddl = CType(e.Row.Cells(GRID_COL_CAUSE_OF_LOSS_IDX).FindControl(GRID_CAUSE_OF_LOSS_ID_DDL), DropDownList)
                            Dim dv As DataView = LookupListNew.GetServiceClassList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            Dim mytext As String = GetGuidStringFromByteArray(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_COVERAGE_TYPE_ID), Byte()))
                            PopulateCauseOfLossDropDown(New Guid(mytext), ddl)
                            If Not State.causeoflossId.Equals(Guid.Empty) Then SetSelectedItem(ddl, State.causeoflossId)
                            ddl.Enabled = True

                        End If
                        e.Row.Cells(GRID_COL_RISK_TYPE_DESCRIPTION_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_RISK_TYPE).ToString()
                        e.Row.Cells(GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_COVERAGE_TYPE).ToString
                        e.Row.Cells(GRID_COL_BEGIN_DATE_IDX).Text = GetDateFormattedString(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_BEGIN_DATE), Date))
                        e.Row.Cells(GRID_COL_END_DATE_IDX).Text = GetDateFormattedString(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_END_DATE), Date))

                    Else
                        If (e.Row.Cells(GRID_COL_AUTHORIZED_AMT_IDX).FindControl(GRID_Label_AUTHORIZATION_AMOUNT) IsNot Nothing) Then
                            lbl = CType(e.Row.Cells(GRID_COL_AUTHORIZED_AMT_IDX).FindControl(GRID_Label_AUTHORIZATION_AMOUNT), Label)
                            lbl.Text = State.authorizedAmount
                        End If
                        If (e.Row.Cells(GRID_COL_CAUSE_OF_LOSS_IDX).FindControl(GRID_Label_CAUSE_OF_LOSS) IsNot Nothing) Then
                            lbl = CType(e.Row.Cells(GRID_COL_CAUSE_OF_LOSS_IDX).FindControl(GRID_Label_CAUSE_OF_LOSS), Label)
                        End If
                        e.Row.Cells(GRID_COL_RISK_TYPE_DESCRIPTION_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_RISK_TYPE).ToString()
                        e.Row.Cells(GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_COVERAGE_TYPE).ToString
                        e.Row.Cells(GRID_COL_BEGIN_DATE_IDX).Text = GetDateFormattedString(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_BEGIN_DATE), Date))
                        e.Row.Cells(GRID_COL_END_DATE_IDX).Text = GetDateFormattedString(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_END_DATE), Date))
                    End If
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Public Sub ItemCommand(source As Object, e As GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                Dim nIndex As Integer

                If e.CommandName = "SelectRecord" Then
                    nIndex = CInt(e.CommandArgument)
                    State.isEditMode = True
                    Grid.EditIndex = nIndex
                    State.idx = nIndex
                    State.selectedCertItemCoverageId = New Guid(CType(Grid.Rows(nIndex).Cells(GRID_COL_CERT_ITEM_COVERAGE_IDX).FindControl("moCertItemCoverageId"), Label).Text)

                    EnableDisableButtons()

                    Grid.DataSource = State.searchDV
                    Grid.DataBind()
                ElseIf e.CommandName = "Sort" Then
                    Grid.DataMember = e.CommandArgument.ToString
                    PopulateGrid()

                End If
            Catch ex As ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Grid_PageIndexChanged(source As Object, e As GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                State.PageIndex = e.NewPageIndex
                Grid.PageIndex = State.PageIndex
                State.selectedCertItemCoverageId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Button Clicks"

        Private Sub btnChangeCoverage_Click(sender As Object, e As EventArgs) Handles btnChangeCoverage.Click
            Dim AuthAmtText As String
            Dim AssurantPaysAmt As New DecimalType(0)
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), Codes.YESNO_Y)
            Dim AssurantPaysId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetWhoPaysLookupList(Authentication.LangId), Codes.ASSURANT_PAYS)
            Dim oClaimDal As ClaimDAL
            Try
                If (Not State.claimId.Equals(Guid.Empty)) AndAlso (Not State.selectedCertItemCoverageId.Equals(Guid.Empty)) Then
                    'Me.State.searchDV = CertItemCoverage.GetClaimCoverageType(Me.State.certId, Me.State.certItemCoverageId, Me.State.lossDate, Me.State.StatusCode, Me.State.InvoiceProcessDate)

                    If (State.searchDV.Count > 0) Then
                        Dim ClaimBO As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.claimId)
                        BindBOPropertyToGridHeader(ClaimBO, CAUSE_OF_LOSS_ID_PROPERTY, Grid.Columns(GRID_COL_CAUSE_OF_LOSS_IDX))
                        'REQ-863

                        If ClaimBO.ClaimAuthorizationType = ClaimAuthorizationType.Multiple Then
                            PopulateBOProperty(ClaimBO, CAUSE_OF_LOSS_ID_PROPERTY, GetSelectedItem(CType(Grid.Rows(State.idx).Cells(GRID_COL_CAUSE_OF_LOSS_IDX).FindControl(GRID_CAUSE_OF_LOSS_ID_DDL), DropDownList)))
                            CType(ClaimBO, MultiAuthClaim).ChangeCoverageType(State.selectedCertItemCoverageId, ClaimBO.CauseOfLossId)
                            'End REQ-863
                        Else
                            ClaimBO.CertItemCoverageId = State.selectedCertItemCoverageId
                            ClaimBO.CalculateFollowUpDate()

                            'claimBO.CauseOfLossId = Me.GetSelectedItem(CType(Grid.Items(Me.State.idx).Cells(Me.GRID_COL_CAUSE_OF_LOSS_IDX).FindControl("cboCauseOfLossId"), DropDownList))
                            PopulateBOProperty(ClaimBO, CAUSE_OF_LOSS_ID_PROPERTY, GetSelectedItem(CType(Grid.Rows(State.idx).Cells(GRID_COL_CAUSE_OF_LOSS_IDX).FindControl(GRID_CAUSE_OF_LOSS_ID_DDL), DropDownList)))

                            If ClaimBO.CauseOfLossId.Equals(Guid.Empty) Then
                                Throw New GUIException(Message.MSG_CAUSE_OF_LOSS_IS_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CAUSE_OF_LOSS_IS_REQUIRED)
                            End If

                            AuthAmtText = CType(Grid.Rows(State.idx).Cells(GRID_COL_AUTHORIZED_AMT_IDX).FindControl(GRID_AUTHORIZED_AMOUNT), TextBox).Text

                            If IsNumeric(AuthAmtText) AndAlso CType(AuthAmtText, Decimal) < 0 Then
                                Throw New GUIException(Message.MSG_CAUSE_OF_LOSS_IS_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)
                            Else
                                PopulateBOProperty(ClaimBO, AUTHORIZED_AMOUNT_PROPERTY, CType(Grid.Rows(State.idx).Cells(GRID_COL_AUTHORIZED_AMT_IDX).FindControl(GRID_AUTHORIZED_AMOUNT), TextBox).Text)
                            End If

                            'Req - 1001 Change of Coverage Type and Recompute Deductible
                            ClaimBO.PrepopulateDeductible()

                            ' Check if Deductible Calculation Method is List and SKU Price is resolved
                            'If it is not resolved then set the Claim to Pending Status
                            Dim oDeductible As CertItemCoverage.DeductibleType
                            oDeductible = CertItemCoverage.GetDeductible(ClaimBO.CertItemCoverageId, ClaimBO.MethodOfRepairId)
                            Dim bListPriceFound As Boolean = True
                            If (oDeductible.DeductibleBasedOn = Codes.DEDUCTIBLE_BASED_ON__PERCENT_OF_LIST_PRICE) Then
                                Dim lstPrice As DecimalType
                                Dim moCertItemCvg As New CertItemCoverage(ClaimBO.CertItemCoverageId)
                                Dim moCertItem As New CertItem(moCertItemCvg.CertItemId)
                                Dim moCert As New Certificate(moCertItem.CertId)
                                Dim strSKU As String = moCertItem.SkuNumber
                                Dim c As Comment

                                lstPrice = ListPrice.GetListPrice(moCert.DealerId, strSKU, CDate(ClaimBO.LossDate).ToString("yyyyMMdd"))
                                If (lstPrice Is Nothing) Then
                                    bListPriceFound = False
                                    c = ClaimBO.AddNewComment()
                                    c.CommentTypeId = LookupListNew.GetIdFromCode(LookupListNew.LK_COMMENT_TYPES, Codes.COMMENT_TYPE__CLAIM_SET_TO_PENDING)
                                    c.Comments = TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEDUCTIBLE_CAN_NOT_BE_DETERMINED_ERR)
                                    ' If the SKU Price is not resolved, set the Claim to Pending status.
                                    ClaimBO.StatusCode = Codes.CLAIM_STATUS__PENDING
                                End If
                            End If 'end SKU Price

                            PopulateBOProperty(ClaimBO, CLAIM_SPECIAL_SERVICE_ID_PROPERTY, State.claimSpecialServiceId)
                            PopulateBOProperty(ClaimBO, CLAIM_SPECIAL_SERVICE_ID_PROPERTY, State.claimSpecialServiceId)
                            If State.claimSpecialServiceId = yesId Then
                                PopulateBOProperty(ClaimBO, WHO_PAYS_ID_PROPERTY, AssurantPaysId)
                                ClaimBO.Deductible = New DecimalType(ZERO_DECIMAL)
                                Dim myContractId As Guid = Contract.GetContractID(ClaimBO.CertificateId)
                                If ClaimBO.MethodOfRepairCode <> Codes.METHOD_OF_REPAIR__RECOVERY Then
                                    Dim al As ArrayList = ClaimBO.CalculateLiabilityLimit(ClaimBO.CertificateId, myContractId, ClaimBO.CertItemCoverageId, ClaimBO.LossDate)
                                    ClaimBO.LiabilityLimit = CType(al(0), Decimal)
                                End If
                            End If


                            ClaimBO.Save()
                        End If
                        MasterPage.MessageController.AddSuccess(MSG_COVERAGE_CHANGED_SUCCESSFULLY, True)

                    Else
                        'Req - 1001 Display the Error Message 'New Coverage Type is not in effect on the date of loss of the claim'
                        MasterPage.MessageController.AddError(MSG_COVERAGE_NOT_IN_EFFECT, True)
                    End If

                End If
                State.isEditMode = False
                'Send the user back to the previous page to see the udpated results
                Back(DetailPageCommand.Back)

            Catch ex As ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
            Try
                State.isEditMode = False
                Back(DetailPageCommand.Back)
            Catch ex As ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub Back(cmd As DetailPageCommand)
            Dim ClaimBO As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(State.claimId)
            Dim retObj As ReturnType = New ReturnType(cmd, ClaimBO, False)
            ReturnToCallingPage(retObj)
        End Sub

        Private Sub Grid_Sorting(sender As Object, e As GridViewSortEventArgs) Handles Grid.Sorting
            Try
                Dim spaceIndex As Integer = SortDirection.LastIndexOf(" ")


                If spaceIndex > 0 AndAlso SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If SortDirection.EndsWith(" ASC") Then
                        SortDirection = e.SortExpression + " DESC"
                    Else
                        SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    SortDirection = e.SortExpression + " ASC"
                End If

                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region
    End Class

End Namespace

