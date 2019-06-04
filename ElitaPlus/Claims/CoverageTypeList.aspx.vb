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
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
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
                If Not ViewState("SortDirection") Is Nothing Then
                    Return ViewState("SortDirection").ToString
                Else
                    Return String.Empty
                End If

            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property
#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public ClaimBO As ClaimBase
            Public HasDataChanged As Boolean

            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal ClaimBO As ClaimBase, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.ClaimBO = ClaimBO
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Page_Events"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Me.MasterPage.MessageController.Clear_Hide()
            Try
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(Claims)
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(CHANGE_COVERAGE)
                Me.UpdateBreadCrum()

                Dim ClaimBO As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.claimId)
                Me.State.authorizedAmount = CType(ClaimBO.AuthorizedAmount, String)

                moClaimNumberText.Text = ClaimBO.ClaimNumber
                EnableDisableControls(moClaimNumberText, False)
                moDealerText.Text = ClaimBO.DealerName
                EnableDisableControls(moCertificateText, False)
                moCertificateText.Text = ClaimBO.CertificateNumber
                EnableDisableControls(moDealerText, False)
                Me.EnableDisableButtons()
                CheckIfComingFromSaveConfirm()
                If Not Me.IsPostBack Then
                    TranslateGridHeader(Grid)
                    Me.PopulateGrid()
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)

        End Sub

        Private Sub UpdateBreadCrum()

            If (Not Me.State Is Nothing) Then
                Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                    TranslationBase.TranslateLabelOrMessage(CHANGE_COVERAGE)
            End If

        End Sub

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            If Not confResponse Is Nothing AndAlso confResponse = Me.MSG_VALUE_OK Then
                Me.HiddenSaveChangesPromptResponse.Value = ""
                Me.Back(ElitaPlusPage.DetailPageCommand.Back)
                'Me.callPage(ClaimForm.URL, Me.State.claimId)
            End If
        End Sub


        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    Me.State.lossDate = CType(CType(CallingPar, ArrayList)(0), Date)
                    Me.State.claimId = CType(CType(CallingPar, ArrayList)(1), Guid)
                    Me.State.certId = CType(CType(CallingPar, ArrayList)(2), Guid)
                    Me.State.certItemCoverageId = CType(CType(CallingPar, ArrayList)(3), Guid)
                    Me.State.StatusCode = CType(CType(CallingPar, ArrayList)(4), String)
                    Me.State.InvoiceProcessDate = CType(CType(CallingPar, ArrayList)(5), Date)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

#End Region

#Region "Controlling Logic"
        Protected Sub cboCauseOfLossId_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
            Try
                Dim ddlCauseOfLoss As DropDownList = CType(sender, DropDownList)
                'Dim cell As TableCell = CType(ddCauseOfLoss.Parent, TableCell)
                Dim index As Integer = CType(ddlCauseOfLoss.NamingContainer, GridViewRow).RowIndex
                ' Dim item As DataGridItem = CType(cell.Parent, DataGridItem)
                Dim content As String = Grid.Rows(index).Cells(0).Text 'item.Cells(0).Text
                Me.State.causeoflossId = GetSelectedItem(ddlCauseOfLoss)
                Dim txtType As TextBox = CType(Me.Grid.Rows(index).Cells(Me.GRID_COL_AUTHORIZED_AMT_IDX).FindControl(GRID_AUTHORIZED_AMOUNT), TextBox)

                PopulateAuthorizedAmountFromPGPrices()

                EnableDisableButtons()

                Grid.DataSource = Me.State.searchDV
                Grid.DataBind()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Sub PopulateAuthorizedAmountFromPGPrices()
            Dim retVal As Boolean
            Dim ClaimBO As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.claimId)
            ClaimBO.CertItemCoverageId = Me.State.selectedCertItemCoverageId
            ClaimBO.CauseOfLossId = Me.State.causeoflossId

            Select Case ClaimBO.ClaimAuthorizationType
                Case ClaimAuthorizationType.Single
                    Me.State.claimSpecialServiceId = CType(ClaimBO, Claim).GetSpecialServiceValue()
                    CType(ClaimBO, Claim).ClaimSpecialServiceId = Me.State.claimSpecialServiceId
                    retVal = CType(ClaimBO, Claim).CalculateAuthorizedAmountFromPGPrices()
                Case ClaimAuthorizationType.Multiple
                    retVal = False
                Case ClaimAuthorizationType.None
                    Throw New NotImplementedException
            End Select

            Me.State.authorizedAmount = ClaimBO.AuthorizedAmount.ToString
            Me.State.isSpecialServiceCase = retVal

        End Sub


        Sub PopulateCauseOfLossDropDown(ByVal CovTypeId As Guid, ByVal ddl As DropDownList)
            Try
                Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                'Me.BindListControlToDataView(ddl, LookupListNew.GetCauseOfLossByCoverageTypeLookupList(Authentication.LangId, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, CovTypeId, , True))

                Dim certificateBO As New Certificate(Me.State.certId)

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
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub PopulateGrid()

            Try
                Dim blnNewSearch As Boolean = False
                If (Me.State.searchDV Is Nothing) Then
                    Me.State.searchDV = CertItemCoverage.GetClaimCoverageType(Me.State.certId, Me.State.certItemCoverageId, Me.State.lossDate, Me.State.StatusCode, Me.State.InvoiceProcessDate)
                    blnNewSearch = True
                End If
                ' ''''
                Me.Grid.AutoGenerateColumns = False
                Me.State.searchDV.Sort = Me.SortDirection
                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedCertItemCoverageId, Me.Grid, Me.State.PageIndex)
                ' Me.State.PageIndex = Me.Grid.PageIndex
                'Me.Grid.DataSource = Me.State.searchDV
                'Me.Grid.AllowSorting = True
                'Me.Grid.PagerStyle.HorizontalAlign = HorizontalAlign.Center

                'Me.Grid.DataBind()
                Me.Grid.AutoGenerateColumns = False
                ' Me.Grid.Columns(Me.GRID_COL_RISK_TYPE_DESCRIPTION_IDX).SortExpression = CertItemCoverage.CertItemCoverageSearchDV.COL_ISK_TYPE
                ' Me.Grid.Columns(Me.GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).SortExpression = CertItemCoverage.CertItemCoverageSearchDV.COL_COVERAGE_TYPE
                SortAndBindGrid(blnNewSearch)

                ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub SortAndBindGrid(Optional ByVal blnShowErr As Boolean = True)

            Me.TranslateGridControls(Grid)

            If (Me.State.searchDV.Count = 0) Then
                Me.State.searchDV = Nothing

                Me.Grid.Rows(0).Visible = False

                Me.State.IsGridVisible = False
                If blnShowErr Then
                    Me.MasterPage.MessageController.AddInformation(ElitaPlus.ElitaPlusWebApp.Message.MSG_NO_RECORDS_FOUND, True)
                End If
            Else
                Me.Grid.Enabled = True
                Me.Grid.PageSize = Me.State.PageSize
                Me.Grid.DataSource = Me.State.searchDV
                Me.State.IsGridVisible = True
                HighLightSortColumn(Grid, Me.SortDirection)
                Me.Grid.DataBind()
            End If


            ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
            'ControlMgr.SetVisibleControl(Me, moSearchResults, Me.State.IsGridVisible)

            Session("recCount") = Me.State.searchDV.Count

            If Me.State.searchDV.Count > 0 Then
                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            Else
                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
            End If

        End Sub
        Private Sub EnableDisableButtons()
            If Not Me.State.selectedCertItemCoverageId.Equals(Guid.Empty) Then
                ControlMgr.SetEnableControl(Me, btnChangeCoverage, True)
            Else
                ControlMgr.SetEnableControl(Me, btnChangeCoverage, False)
            End If
        End Sub

#End Region


#Region " Datagrid Related "

        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim lbl As Label
                Dim txt As TextBox
                Dim ddl As DropDownList

                'If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Or itemType = ListItemType.EditItem Then
                If e.Row.RowType = DataControlRowType.DataRow Then
                    '''Edit only on the row that was clicked by the user.
                    If Me.State.isEditMode And Grid.EditIndex = e.Row.RowIndex Then
                        If (Not e.Row.Cells(Me.GRID_COL_AUTHORIZED_AMT_IDX).FindControl(GRID_AUTHORIZED_AMOUNT) Is Nothing) Then
                            txt = CType(e.Row.Cells(Me.GRID_COL_AUTHORIZED_AMT_IDX).FindControl(GRID_AUTHORIZED_AMOUNT), TextBox)
                            '''''if claim is of special service then allow the user to edit the authorization amount
                            If (Me.State.isSpecialServiceCase) Then
                                txt.Enabled = True
                            Else
                                txt.Enabled = False
                            End If
                            txt.Text = Me.State.authorizedAmount
                        End If
                        If (Not e.Row.Cells(Me.GRID_COL_CAUSE_OF_LOSS_IDX).FindControl(GRID_CAUSE_OF_LOSS_ID_DDL) Is Nothing) Then
                            ddl = CType(e.Row.Cells(Me.GRID_COL_CAUSE_OF_LOSS_IDX).FindControl(GRID_CAUSE_OF_LOSS_ID_DDL), DropDownList)
                            Dim dv As DataView = LookupListNew.GetServiceClassList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                            Dim mytext As String = GetGuidStringFromByteArray(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_COVERAGE_TYPE_ID), Byte()))
                            PopulateCauseOfLossDropDown(New Guid(mytext), ddl)
                            If Not Me.State.causeoflossId.Equals(Guid.Empty) Then Me.SetSelectedItem(ddl, Me.State.causeoflossId)
                            ddl.Enabled = True

                        End If
                        e.Row.Cells(Me.GRID_COL_RISK_TYPE_DESCRIPTION_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_RISK_TYPE).ToString()
                        e.Row.Cells(Me.GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_COVERAGE_TYPE).ToString
                        e.Row.Cells(Me.GRID_COL_BEGIN_DATE_IDX).Text = Me.GetDateFormattedString(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_BEGIN_DATE), Date))
                        e.Row.Cells(Me.GRID_COL_END_DATE_IDX).Text = Me.GetDateFormattedString(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_END_DATE), Date))

                    Else
                        If (Not e.Row.Cells(Me.GRID_COL_AUTHORIZED_AMT_IDX).FindControl(GRID_Label_AUTHORIZATION_AMOUNT) Is Nothing) Then
                            lbl = CType(e.Row.Cells(Me.GRID_COL_AUTHORIZED_AMT_IDX).FindControl(GRID_Label_AUTHORIZATION_AMOUNT), Label)
                            lbl.Text = Me.State.authorizedAmount
                        End If
                        If (Not e.Row.Cells(Me.GRID_COL_CAUSE_OF_LOSS_IDX).FindControl(GRID_Label_CAUSE_OF_LOSS) Is Nothing) Then
                            lbl = CType(e.Row.Cells(Me.GRID_COL_CAUSE_OF_LOSS_IDX).FindControl(GRID_Label_CAUSE_OF_LOSS), Label)
                        End If
                        e.Row.Cells(Me.GRID_COL_RISK_TYPE_DESCRIPTION_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_RISK_TYPE).ToString()
                        e.Row.Cells(Me.GRID_COL_COVERAGE_TYPE_DESCRIPTION_IDX).Text = dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_COVERAGE_TYPE).ToString
                        e.Row.Cells(Me.GRID_COL_BEGIN_DATE_IDX).Text = Me.GetDateFormattedString(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_BEGIN_DATE), Date))
                        e.Row.Cells(Me.GRID_COL_END_DATE_IDX).Text = Me.GetDateFormattedString(CType(dvRow(CertItemCoverage.CertItemCoverageSearchDV.COL_END_DATE), Date))
                    End If
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                Dim nIndex As Integer

                If e.CommandName = "SelectRecord" Then
                    nIndex = CInt(e.CommandArgument)
                    Me.State.isEditMode = True
                    Grid.EditIndex = nIndex
                    Me.State.idx = nIndex
                    Me.State.selectedCertItemCoverageId = New Guid(CType(Grid.Rows(nIndex).Cells(Me.GRID_COL_CERT_ITEM_COVERAGE_IDX).FindControl("moCertItemCoverageId"), Label).Text)

                    EnableDisableButtons()

                    Grid.DataSource = Me.State.searchDV
                    Grid.DataBind()
                ElseIf e.CommandName = "Sort" Then
                    Grid.DataMember = e.CommandArgument.ToString
                    Me.PopulateGrid()

                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Me.State.PageIndex = e.NewPageIndex
                Grid.PageIndex = Me.State.PageIndex
                Me.State.selectedCertItemCoverageId = Guid.Empty
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Button Clicks"

        Private Sub btnChangeCoverage_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnChangeCoverage.Click
            Dim AuthAmtText As String
            Dim AssurantPaysAmt As New DecimalType(0)
            Dim yesId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetYesNoLookupList(Authentication.LangId), Codes.YESNO_Y)
            Dim AssurantPaysId As Guid = LookupListNew.GetIdFromCode(LookupListNew.GetWhoPaysLookupList(Authentication.LangId), Codes.ASSURANT_PAYS)
            Dim oClaimDal As ClaimDAL
            Try
                If (Not Me.State.claimId.Equals(Guid.Empty)) AndAlso (Not Me.State.selectedCertItemCoverageId.Equals(Guid.Empty)) Then
                    'Me.State.searchDV = CertItemCoverage.GetClaimCoverageType(Me.State.certId, Me.State.certItemCoverageId, Me.State.lossDate, Me.State.StatusCode, Me.State.InvoiceProcessDate)

                    If (Me.State.searchDV.Count > 0) Then
                        Dim ClaimBO As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.claimId)
                        Me.BindBOPropertyToGridHeader(ClaimBO, Me.CAUSE_OF_LOSS_ID_PROPERTY, Me.Grid.Columns(Me.GRID_COL_CAUSE_OF_LOSS_IDX))
                        'REQ-863

                        If ClaimBO.ClaimAuthorizationType = ClaimAuthorizationType.Multiple Then
                            PopulateBOProperty(ClaimBO, Me.CAUSE_OF_LOSS_ID_PROPERTY, Me.GetSelectedItem(CType(Grid.Rows(Me.State.idx).Cells(Me.GRID_COL_CAUSE_OF_LOSS_IDX).FindControl(GRID_CAUSE_OF_LOSS_ID_DDL), DropDownList)))
                            CType(ClaimBO, MultiAuthClaim).ChangeCoverageType(Me.State.selectedCertItemCoverageId, ClaimBO.CauseOfLossId)
                            'End REQ-863
                        Else
                            ClaimBO.CertItemCoverageId = Me.State.selectedCertItemCoverageId
                            ClaimBO.CalculateFollowUpDate()

                            'claimBO.CauseOfLossId = Me.GetSelectedItem(CType(Grid.Items(Me.State.idx).Cells(Me.GRID_COL_CAUSE_OF_LOSS_IDX).FindControl("cboCauseOfLossId"), DropDownList))
                            PopulateBOProperty(ClaimBO, Me.CAUSE_OF_LOSS_ID_PROPERTY, Me.GetSelectedItem(CType(Grid.Rows(Me.State.idx).Cells(Me.GRID_COL_CAUSE_OF_LOSS_IDX).FindControl(GRID_CAUSE_OF_LOSS_ID_DDL), DropDownList)))

                            If ClaimBO.CauseOfLossId.Equals(Guid.Empty) Then
                                Throw New GUIException(Message.MSG_CAUSE_OF_LOSS_IS_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.GUI_CAUSE_OF_LOSS_IS_REQUIRED)
                            End If

                            AuthAmtText = CType(Grid.Rows(Me.State.idx).Cells(Me.GRID_COL_AUTHORIZED_AMT_IDX).FindControl(GRID_AUTHORIZED_AMOUNT), TextBox).Text

                            If IsNumeric(AuthAmtText) AndAlso CType(AuthAmtText, Decimal) < 0 Then
                                Throw New GUIException(Message.MSG_CAUSE_OF_LOSS_IS_REQUIRED, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR)
                            Else
                                PopulateBOProperty(ClaimBO, Me.AUTHORIZED_AMOUNT_PROPERTY, CType(Grid.Rows(Me.State.idx).Cells(Me.GRID_COL_AUTHORIZED_AMT_IDX).FindControl(GRID_AUTHORIZED_AMOUNT), TextBox).Text)
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

                            PopulateBOProperty(ClaimBO, Me.CLAIM_SPECIAL_SERVICE_ID_PROPERTY, Me.State.claimSpecialServiceId)
                            PopulateBOProperty(ClaimBO, CLAIM_SPECIAL_SERVICE_ID_PROPERTY, Me.State.claimSpecialServiceId)
                            If Me.State.claimSpecialServiceId = yesId Then
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
                        Me.MasterPage.MessageController.AddSuccess(Me.MSG_COVERAGE_CHANGED_SUCCESSFULLY, True)

                    Else
                        'Req - 1001 Display the Error Message 'New Coverage Type is not in effect on the date of loss of the claim'
                        Me.MasterPage.MessageController.AddError(Me.MSG_COVERAGE_NOT_IN_EFFECT, True)
                    End If

                End If
                Me.State.isEditMode = False
                'Send the user back to the previous page to see the udpated results
                Me.Back(ElitaPlusPage.DetailPageCommand.Back)

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                Me.State.isEditMode = False
                Me.Back(ElitaPlusPage.DetailPageCommand.Back)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub Back(ByVal cmd As ElitaPlusPage.DetailPageCommand)
            Dim ClaimBO As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.State.claimId)
            Dim retObj As ReturnType = New ReturnType(cmd, ClaimBO, False)
            Me.ReturnToCallingPage(retObj)
        End Sub

        Private Sub Grid_Sorting(sender As Object, e As GridViewSortEventArgs) Handles Grid.Sorting
            Try
                Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")


                If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
                    If Me.SortDirection.EndsWith(" ASC") Then
                        Me.SortDirection = e.SortExpression + " DESC"
                    Else
                        Me.SortDirection = e.SortExpression + " ASC"
                    End If
                Else
                    Me.SortDirection = e.SortExpression + " ASC"
                End If

                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region
    End Class

End Namespace

