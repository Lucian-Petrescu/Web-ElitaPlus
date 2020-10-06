Imports Microsoft.VisualBasic
Imports System.Globalization


Partial Public Class ClaimHistoryForm
    Inherits ElitaPlusSearchPage


#Region "Constants"
    Public Const PAGETITLE As String = "Claim_History"
    Public Const CLAIM_TAB As String = "Claim"
    Private Const STYLE_BLOCK As String = "STYLE_BLOCK"
    Public Const GRID_COL_EDIT_CTRL As String = "btnEdit"
    Public Const SELECT_ACTION_COMMAND As String = "SelectAction"

    Public Const CLAIM_NUMBER As Integer = 0
    Public Const GRID_COL_CREATED_DATE As Integer = 1
    Public Const GRID_COL_CREATED_BY As Integer = 2
    Public Const GRID_COL_STATUS_CODE_OLD As Integer = 3
    Public Const GRID_COL_STATUS_CODE_NEW As Integer = 4
    Public Const GRID_COL_AUTHORIZED_AMOUNT_OLD As Integer = 5
    Public Const GRID_COL_AUTHORIZED_AMOUNT_NEW As Integer = 6
    Public Const GRID_COL_CLAIM_CLOSED_DATE_OLD As Integer = 7
    Public Const GRID_COL_CLAIM_CLOSED_DATE_NEW As Integer = 8
    Public Const GRID_COL_REPAIR_DATE_OLD As Integer = 9
    Public Const GRID_COL_REPAIR_DATE_NEW As Integer = 10
    Public Const GRID_COL_MODIFIED_DATE As Integer = 11
    Public Const GRID_COL_MODIFIED_BY As Integer = 12
    Public Const GRID_COL_LIABILITY_LIMIT_OLD As Integer = 13
    Public Const GRID_COL_LIABILITY_LIMIT_NEW As Integer = 14
    Public Const GRID_COL_CERT_ITEM_COVERAGE_ID_OLD As Integer = 15
    Public Const GRID_COL_CERT_ITEM_COVERAGE_ID_NEW As Integer = 16
    Public Const GRID_COL_CLAIM_MODIFIED_DATE_NEW As Integer = 17
    Public Const GRID_COL_CLAIM_MODIFIED_DATE_OLD As Integer = 18
    Public Const GRID_COL_CLAIM_MODIFIED_BY_NEW As Integer = 19
    Public Const GRID_COL_CLAIM_MODIFIED_BY_OLD As Integer = 20
    Public Const GRID_COL_DEDUCTIBLE_NEW As Integer = 21
    Public Const GRID_COL_DEDUCTIBLE_OLD As Integer = 22
    Public Const GRID_COL_DESCRIPTION_OLD As Integer = 23
    Public Const GRID_COL_DESCRIPTION_NEW As Integer = 24
    Public Const GRID_COL_SERVICE_CENTER_NEW As Integer = 25
    Public Const GRID_COL_SERVICE_CENTER_OLD As Integer = 26
    Public Const GRID_COL_BATCH_NUMBER_NEW As Integer = 27
    Public Const GRID_COL_BATCH_NUMBER_OLD As Integer = 28
    Public Const GRID_COL_LAWSUIT_NEW As Integer = 29
    Public Const GRID_COL_LAWSUIT_OLD As Integer = 30

    Public COMMAND_ITEM_COMMAND As String = "ItemCommand"


#End Region


#Region "Parameters"

    Public Class Parameters
        Public moClaimId As Guid

        Public Sub New(oClaimId As Guid)
            moClaimId = oClaimId
        End Sub

    End Class

#End Region

#Region "Page State"
    Class MyState
        Public searchDV As DataView
        Public claimId As Guid = Guid.Empty
        Public DictItemId As Guid = Guid.Empty
        Public masterClaimNumber As String
        Public PageIndex As Integer = 0
        Public PageSize As Integer = DEFAULT_NEW_UI_TAB_PAGE_SIZE
        Public selectedPageSize As Integer = DEFAULT_NEW_UI_TAB_PAGE_SIZE
        Public IsEditMode As Boolean
        Public BeginningPageIndex As Integer = 0

    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub
    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
        Try
            If CallingParameters IsNot Nothing Then
                State.masterClaimNumber = CType(CType(CallingPar, ArrayList)(0), String)
                State.claimId = CType(CType(CallingPar, ArrayList)(1), Guid)

            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region
#Region "Page Events"


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try



            If Not IsPostBack Then
                PopulateGrid()
                BackupLabelsTranslation()
                myPageInit()
            End If


            RetrieveTranslation()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
    Public Sub myPageInit()

        Grid.PageSize = CType(cboPageSize.SelectedValue, Integer)
        State.BeginningPageIndex = Grid.CurrentPageIndex

        If Not (State.selectedPageSize = DEFAULT_NEW_UI_TAB_PAGE_SIZE) Then
            cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
            Grid.PageSize = State.selectedPageSize
        End If

        PanelHistoryDetails.Height = 175
        TextBoxSearchClaimNumber.Text = State.masterClaimNumber.ToString

        PanelHistoryDetails.Attributes.Add("onResize", "resizePanel();")

        If (MasterPage.MessageController.Text.Trim = "") Then
            MasterPage.MessageController.Clear()
        End If

        SetFormTab(CLAIM_TAB)
        SetFormTitle(PAGETITLE)

        ShowMissingTranslations(MasterPage.MessageController)

        ControlMgr.SetEnableControl(Me, LabelStatusCode, False)
        ControlMgr.SetEnableControl(Me, LabelAuthorizedAmount, False)
        ControlMgr.SetEnableControl(Me, LabelClaimClosedDate, False)
        ControlMgr.SetEnableControl(Me, LabelRepairDate, False)
        ControlMgr.SetEnableControl(Me, LabelLiabilityLimit, False)
        ControlMgr.SetEnableControl(Me, LabelCertID, False)
        ControlMgr.SetEnableControl(Me, mLabelDeductible, False)
        ControlMgr.SetEnableControl(Me, mLabelServiceCenter, False)
        ControlMgr.SetEnableControl(Me, mLabelBatchNumber, False)
        ControlMgr.SetEnableControl(Me, mLabelClaimModifyDateNew, False)
        ControlMgr.SetEnableControl(Me, mLabelClaimModifyByNew, False)
        ControlMgr.SetEnableControl(Me, mLabelNew, False)
        ControlMgr.SetEnableControl(Me, mLabelOld, False)
        ControlMgr.SetEnableControl(Me, mLabelLawsuit, False)
        ControlMgr.SetEnableControl(Me, mLabelRecordCount, False)
        ControlMgr.SetVisibleControl(Me, PanelHistoryDetails, False)

    End Sub
#End Region
#Region "Initialization"
    Public Sub BackupLabelsTranslation()
        mLabelStatusCode.Text = LabelStatusCode.Text
        mLabelAuthorizedAmount.Text = LabelAuthorizedAmount.Text
        mLabelClaimClosedDate.Text = LabelClaimClosedDate.Text
        mLabelRepairDate.Text = LabelRepairDate.Text
        mLabelLiabilityLimit.Text = LabelLiabilityLimit.Text
        mLabelCertID.Text = LabelCertID.Text
        mLabelDeductible.Text = LabelDeductible.Text
        mLabelServiceCenter.Text = LabelServiceCenter.Text
        mLabelBatchNumber.Text = LabelBatchNumber.Text
        mLabelLawsuit.Text = LabelLawsuit.Text
        mLabelClaimModifyDateNew.Text = LabelClaimModifyDateNew.Text
        mLabelClaimModifyByNew.Text = LabelClaimModifyByNew.Text
        mLabelNew.Text = LabelNew.Text
        mLabelOld.Text = LabelOld.Text
        LabelCreatedDate.Text = LabelCreatedDate.Text + ":"
        LabelCreatedBy.Text = LabelCreatedBy.Text + ":"
        mLabelPageSize.Text = lblPageSize.Text
        LabelClaimNumber.Text = LabelClaimNumber.Text + ":"
        mLabelRecordCount.Text = lblRecordCount.Text
    End Sub
    Public Sub RetrieveTranslation()
        LabelStatusCode.Text = mLabelStatusCode.Text
        LabelAuthorizedAmount.Text = mLabelAuthorizedAmount.Text
        LabelClaimClosedDate.Text = mLabelClaimClosedDate.Text
        LabelRepairDate.Text = mLabelRepairDate.Text
        LabelLiabilityLimit.Text = mLabelLiabilityLimit.Text
        LabelCertID.Text = mLabelCertID.Text
        LabelDeductible.Text = mLabelDeductible.Text
        LabelServiceCenter.Text = mLabelServiceCenter.Text
        LabelBatchNumber.Text = mLabelBatchNumber.Text
        LabelLawsuit.Text = mLabelLawsuit.Text
        LabelClaimModifyDateNew.Text = mLabelClaimModifyDateNew.Text
        LabelClaimModifyByNew.Text = mLabelClaimModifyByNew.Text
        LabelNew.Text = mLabelNew.Text
        LabelOld.Text = mLabelOld.Text
        lblRecordCount.Text = mLabelRecordCount.Text
        lblPageSize.Text = mLabelPageSize.Text
    End Sub
#End Region

#Region "Grid"

    Public Sub PopulateGrid()
        Dim dv As DataView
        Try

            Dim myDALBusObject As New Assurant.ElitaPlus.DALObjects.ClaimHistoryDAL
            Dim defaultSelectedCodeId As New Guid

            defaultSelectedCodeId = State.claimId
            dv = myDALBusObject.GetClaimHistoryDetails(defaultSelectedCodeId, Authentication.LangId)

            Grid.PageSize = Convert.ToInt32(cboPageSize.SelectedItem.ToString)
            'SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.claimId, Me.Grid, Me.State.PageIndex)
            Grid.DataSource = State.searchDV

            State.PageIndex = Grid.CurrentPageIndex

            If (dv.Count > 0) Then
                Grid.DataSource = dv
                Grid.DataBind()
            End If

            ControlMgr.SetVisibleControl(Me, Grid, True)

            lblRecordCount.Text = dv.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub CommentsGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
        Dim btnEditItem As LinkButton
        Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)
        If (e.Item.Cells(GRID_COL_CREATED_DATE).FindControl(GRID_COL_EDIT_CTRL) IsNot Nothing) Then
            btnEditItem = CType(e.Item.Cells(GRID_COL_CREATED_DATE).FindControl(GRID_COL_EDIT_CTRL), LinkButton)
            btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow("CLAIM_HISTORY_ID"), Byte()))
            btnEditItem.CommandName = SELECT_ACTION_COMMAND
            Dim date1 As Date
            date1 = CType(dvRow("CREATED_DATE"), Date)
            btnEditItem.Text = GetLongDate12FormattedString(date1)
        End If

        If (e.Item.ItemType <> ListItemType.Header And e.Item.ItemType <> ListItemType.Footer) Then
            Dim d As Decimal = CType(dvRow("AUTHORIZED_AMOUNT_NEW"), Decimal)

            e.Item.Cells(GRID_COL_AUTHORIZED_AMOUNT_NEW).Text = GetAmountFormattedString(d)

            d = CType(dvRow("AUTHORIZED_AMOUNT_OLD"), Decimal)
            e.Item.Cells(GRID_COL_AUTHORIZED_AMOUNT_OLD).Text = GetAmountFormattedString(d)
        End If

    End Sub
    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = "SelectAction" Then

                Dim date1 As Date, d As Decimal
                Dim btnEditItem As LinkButton

                If (e.Item.Cells(GRID_COL_CREATED_DATE).FindControl(GRID_COL_EDIT_CTRL) IsNot Nothing) Then
                    btnEditItem = CType(e.Item.Cells(GRID_COL_CREATED_DATE).FindControl(GRID_COL_EDIT_CTRL), LinkButton)
                    TextboxModifiedDate.Text = btnEditItem.Text
                End If

                TextboxModifiedBy.Text = e.Item.Cells(GRID_COL_CREATED_BY).Text.ToString.Replace("&nbsp;", " ").ToString.Trim()

                TextboxClaimStatusCodeOld.Text = e.Item.Cells(GRID_COL_STATUS_CODE_OLD).Text.ToString.Replace("&nbsp;", " ")
                TextboxClaimStatusCodeNew.Text = e.Item.Cells(GRID_COL_STATUS_CODE_NEW).Text.ToString.Replace("&nbsp;", " ")

                TextboxAuthorizedAmountOld.Text = e.Item.Cells(GRID_COL_AUTHORIZED_AMOUNT_OLD).Text.ToString.Replace("&nbsp;", " ").Trim
                If TextboxAuthorizedAmountOld.Text = String.Empty Then
                    d = 0D
                Else
                    d = CType(TextboxAuthorizedAmountOld.Text, Decimal)
                End If
                TextboxAuthorizedAmountOld.Text = GetAmountFormattedString(d)

                TextboxAuthorizedAmountNew.Text = e.Item.Cells(GRID_COL_AUTHORIZED_AMOUNT_NEW).Text.ToString.Replace("&nbsp;", " ")

                d = CType(TextboxAuthorizedAmountNew.Text, Decimal)
                TextboxAuthorizedAmountNew.Text = GetAmountFormattedString(d)

                If IsDate(e.Item.Cells(GRID_COL_CLAIM_CLOSED_DATE_OLD).Text.ToString.Replace("&nbsp;", " ").ToString.Trim()) Then
                    date1 = CDate(e.Item.Cells(GRID_COL_CLAIM_CLOSED_DATE_OLD).Text.ToString.Replace("&nbsp;", " "))
                    TextboxClaimClosedDateOld.Text = GetDateFormattedStringNullable(date1)
                End If

                If IsDate(e.Item.Cells(GRID_COL_CLAIM_CLOSED_DATE_NEW).Text.ToString.Replace("&nbsp;", " ").ToString.Trim()) Then
                    date1 = CDate(e.Item.Cells(GRID_COL_CLAIM_CLOSED_DATE_NEW).Text.ToString.Replace("&nbsp;", " "))
                    TextboxClaimClosedDateNew.Text = GetDateFormattedStringNullable(date1)
                End If

                If IsDate(e.Item.Cells(GRID_COL_REPAIR_DATE_OLD).Text.ToString.Replace("&nbsp;", " ").ToString.Trim()) Then
                    date1 = CDate(e.Item.Cells(GRID_COL_REPAIR_DATE_OLD).Text.ToString.Replace("&nbsp;", " "))
                    TextboxRepairDateOld.Text = GetDateFormattedStringNullable(date1)
                End If

                If IsDate(e.Item.Cells(GRID_COL_REPAIR_DATE_NEW).Text.ToString.Replace("&nbsp;", " ").ToString.Trim()) Then
                    date1 = CDate(e.Item.Cells(GRID_COL_REPAIR_DATE_NEW).Text.ToString.Replace("&nbsp;", " "))
                    TextboxRepairDateNew.Text = GetDateFormattedStringNullable(date1)
                End If

                TextboxLiabilityLimitOld.Text = e.Item.Cells(GRID_COL_LIABILITY_LIMIT_OLD).Text.ToString.Replace("&nbsp;", " ").Trim
                If TextboxLiabilityLimitOld.Text = String.Empty Then
                    d = 0D
                Else
                    d = CType(TextboxLiabilityLimitOld.Text, Decimal)
                End If
                TextboxLiabilityLimitOld.Text = GetAmountFormattedString(d)

                TextboxLiabilityLimitNew.Text = e.Item.Cells(GRID_COL_LIABILITY_LIMIT_NEW).Text.ToString.Replace("&nbsp;", " ").Trim
                If TextboxLiabilityLimitNew.Text = String.Empty Then
                    d = 0D
                Else
                    d = CType(TextboxLiabilityLimitNew.Text, Decimal)
                End If
                TextboxLiabilityLimitNew.Text = GetAmountFormattedString(d)


                If IsDate(e.Item.Cells(GRID_COL_CLAIM_MODIFIED_DATE_NEW).Text.ToString.Replace("&nbsp;", " ").ToString.Trim()) Then
                    date1 = CDate(e.Item.Cells(GRID_COL_CLAIM_MODIFIED_DATE_NEW).Text.ToString.Replace("&nbsp;", " "))
                    TextboxClaimModifyDateNew.Text = GetDateFormattedStringNullable(date1)
                End If

                TextboxClaimModifyByNew.Text = e.Item.Cells(GRID_COL_CLAIM_MODIFIED_BY_NEW).Text.ToString.Replace("&nbsp;", " ")
                TextboxClaimModifyByOld.Text = e.Item.Cells(GRID_COL_CLAIM_MODIFIED_BY_OLD).Text.ToString.Replace("&nbsp;", " ")

                If IsDate(e.Item.Cells(GRID_COL_CLAIM_MODIFIED_DATE_OLD).Text.ToString.Replace("&nbsp;", " ").ToString.Trim()) Then
                    date1 = CDate(e.Item.Cells(GRID_COL_CLAIM_MODIFIED_DATE_OLD).Text.ToString.Replace("&nbsp;", " "))
                    TextboxClaimModifyDateOld.Text = GetDateFormattedStringNullable(date1)
                End If

                TextboxDeductibleNew.Text = e.Item.Cells(GRID_COL_DEDUCTIBLE_NEW).Text.ToString.Replace("&nbsp;", " ").Trim
                If TextboxDeductibleNew.Text = String.Empty Then
                    d = 0D
                Else
                    d = CType(TextboxDeductibleNew.Text, Decimal)
                End If
                TextboxDeductibleNew.Text = GetAmountFormattedString(d)

                TextboxDeductibleOld.Text = e.Item.Cells(GRID_COL_DEDUCTIBLE_OLD).Text.ToString.Replace("&nbsp;", " ").Trim
                If TextboxDeductibleOld.Text = String.Empty Then
                    d = 0D
                Else
                    d = CType(TextboxDeductibleOld.Text, Decimal)
                End If
                TextboxDeductibleOld.Text = GetAmountFormattedString(d)

                TextboxCertItemCoverageIDOld.Text = e.Item.Cells(GRID_COL_DESCRIPTION_OLD).Text.ToString.Replace("&nbsp;", " ")
                TextboxCertItemCoverageIDNew.Text = e.Item.Cells(GRID_COL_DESCRIPTION_NEW).Text.ToString.Replace("&nbsp;", " ")

                TextboxServiceCenterNew.Text = e.Item.Cells(GRID_COL_SERVICE_CENTER_NEW).Text.ToString.Replace("&nbsp;", " ")
                TextboxServiceCenterOld.Text = e.Item.Cells(GRID_COL_SERVICE_CENTER_OLD).Text.ToString.Replace("&nbsp;", " ")

                TextboxBatchNumberNew.Text = e.Item.Cells(GRID_COL_BATCH_NUMBER_NEW).Text.ToString.Replace("&nbsp;", " ")
                TextboxBatchNumberOld.Text = e.Item.Cells(GRID_COL_BATCH_NUMBER_OLD).Text.ToString.Replace("&nbsp;", " ")

                TextboxLawsuitNew.Text = e.Item.Cells(GRID_COL_LAWSUIT_NEW).Text.ToString.Replace("&nbsp;", " ")
                TextboxLawsuitOld.Text = e.Item.Cells(GRID_COL_LAWSUIT_OLD).Text.ToString.Replace("&nbsp;", " ")
                '-------------------------------------------------------------------------------------------------------------
                LabelStatusCode.Text = LabelStatusCode.Text + ":"
                LabelAuthorizedAmount.Text = LabelAuthorizedAmount.Text + ":"
                LabelClaimClosedDate.Text = LabelClaimClosedDate.Text + ":"
                LabelRepairDate.Text = LabelRepairDate.Text + ":"
                LabelLiabilityLimit.Text = LabelLiabilityLimit.Text + ":"
                LabelCertID.Text = LabelCertID.Text + ":"
                LabelDeductible.Text = LabelDeductible.Text + ":"
                LabelServiceCenter.Text = LabelServiceCenter.Text + ":"
                LabelBatchNumber.Text = LabelBatchNumber.Text + ":"
                LabelLawsuit.Text = LabelLawsuit.Text + ":"
                LabelClaimModifyDateNew.Text = LabelClaimModifyDateNew.Text + ":"
                LabelClaimModifyByNew.Text = LabelClaimModifyByNew.Text + ":"

                FlagPanel.Text = "y"
                ControlMgr.SetVisibleControl(Me, LabelCreatedDate, True)
                ControlMgr.SetVisibleControl(Me, LabelCreatedBy, True)
                ControlMgr.SetVisibleControl(Me, TextboxModifiedDate, True)
                ControlMgr.SetVisibleControl(Me, TextboxModifiedBy, True)
                ControlMgr.SetVisibleControl(Me, PanelHistoryDetails, True)

                ControlMgr.SetVisibleControl(Me, Grid, False)
                ControlMgr.SetVisibleControl(Me, lblPageSize, False)
                ControlMgr.SetVisibleControl(Me, cboPageSize, False)
                ControlMgr.SetVisibleControl(Me, lblRecordCount, False)
                ControlMgr.SetVisibleControl(Me, pnlPageSize, False)


                Dim sJavaScript As String = "<script type ='text/jscript' language='JavaScript'>resizePanel();</script>"
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "MyScript", sJavaScript, False)

            End If

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub
    Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try


            Grid.PageSize = Convert.ToInt32(cboPageSize.SelectedItem.ToString)
            Grid.CurrentPageIndex = State.BeginningPageIndex
            PopulateGrid()

            ControlMgr.SetVisibleControl(Me, PanelHistoryDetails, False)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
        Try
            ControlMgr.SetVisibleControl(Me, PanelHistoryDetails, False)

            Grid.CurrentPageIndex = e.NewPageIndex
            PopulateGrid()

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region

#Region "Button Clicks"
    Protected Sub ImgCloseButton_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ImgCloseButton.Click
        ControlMgr.SetVisibleControl(Me, Grid, True)
        ControlMgr.SetVisibleControl(Me, lblPageSize, True)
        ControlMgr.SetVisibleControl(Me, cboPageSize, True)
        ControlMgr.SetVisibleControl(Me, lblRecordCount, True)
        ControlMgr.SetVisibleControl(Me, pnlPageSize, True)
        ControlMgr.SetVisibleControl(Me, PanelHistoryDetails, False)
        ControlMgr.SetVisibleControl(Me, LabelCreatedDate, False)
        ControlMgr.SetVisibleControl(Me, LabelCreatedBy, False)
        ControlMgr.SetVisibleControl(Me, TextboxModifiedDate, False)
        ControlMgr.SetVisibleControl(Me, TextboxModifiedBy, False)
        FlagPanel.Text = "n"


    End Sub
    Protected Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try
            If (FlagPanel.Text = "y") Then
                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, lblPageSize, True)
                ControlMgr.SetVisibleControl(Me, cboPageSize, True)
                ControlMgr.SetVisibleControl(Me, lblRecordCount, True)
                ControlMgr.SetVisibleControl(Me, pnlPageSize, True)
                ControlMgr.SetVisibleControl(Me, PanelHistoryDetails, False)
                ControlMgr.SetVisibleControl(Me, LabelCreatedDate, False)
                ControlMgr.SetVisibleControl(Me, LabelCreatedBy, False)
                ControlMgr.SetVisibleControl(Me, TextboxModifiedDate, False)
                ControlMgr.SetVisibleControl(Me, TextboxModifiedBy, False)

                FlagPanel.Text = "n"
            Else
                Dim retType As New ClaimForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back)
                ReturnToCallingPage(retType)
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
    Protected Sub Back(cmd As ElitaPlusPage.DetailPageCommand)
        NavController = Nothing
        ReturnToCallingPage(True)
    End Sub
#End Region
#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public EditingBo As Claim
        Public BoChanged As Boolean = False
        Public Sub New(LastOp As DetailPageCommand, curEditingBo As Claim, Optional ByVal boChanged As Boolean = False)
            LastOperation = LastOp
            EditingBo = curEditingBo
            Me.BoChanged = boChanged
        End Sub
        Public Sub New(LastOp As DetailPageCommand)
            LastOperation = LastOp
        End Sub
    End Class
#End Region


End Class

