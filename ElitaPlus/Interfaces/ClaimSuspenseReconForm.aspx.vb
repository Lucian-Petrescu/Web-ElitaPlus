Imports Assurant.ElitaPlus.DALObjects
Imports Microsoft.VisualBasic

Partial Class ClaimSuspenseReconForm
    Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Protected WithEvents moErrorController As ErrorController
    Protected WithEvents btnUndo_WRITE As System.Web.UI.WebControls.Button
    Protected WithEvents btnBack As System.Web.UI.WebControls.Button

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "Constants"
    Public Const URL As String = "ClaimSuspenseReconForm.aspx"

    Private Const EMPTY As String = ""
    Private Const DEFAULT_PAGE_INDEX As Integer = 0

    'Actions
    Private Const ACTION_NONE As String = "ACTION_NONE"
    Private Const ACTION_SAVE As String = "ACTION_SAVE"
    Private Const ACTION_NO_EDIT As String = "ACTION_NO_EDIT"
    Private Const ACTION_EDIT As String = "ACTION_EDIT"

    'Colors
    Private Const GRID_ALT_ITEM_COLOR As String = "#F1F1F1"
    Private Const GRID_ITEM_COLOR As String = "#DEE3E7"

    'Errors
    Private Const SEARCH_EXCEPTION As String = "CLAIMSUSPENSE_FORM001"

    'Local Variables
    Private CurrIndex As Integer = 0
    Private CurrMasterIndex As Integer = 0
    Private CurrCertificateNumber As String = ""
    Private CurrColor As String = "#DEE3E7"
    Private searchClick As Boolean = False

#End Region

#Region "Page State"

    Class MyState
        Public ActionInProgress As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Nothing_
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
        Public totalPages As Integer = 0
        Public sortBy As String
        Public dsClaimSuspense As DataSet
        Public arrCertificates As ArrayList
        Public currRecordIndex As Integer = 0
        Public retObj As ReturnType
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

#Region "Handlers"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Me.moErrorController.Clear_Hide()

        If Not Page.IsPostBack Then
            Me.ShowMissingTranslations(moErrorController)
            BaseSetButtonsState(False)

            'Disable the process button until after searching
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
            ControlMgr.SetEnableControl(Me, BtnRejectReport, False)

        Else
            CheckIfComingFromSaveConfirm()
        End If

        'Set default button to search
        Me.SetDefaultButton(Me.moCertificateText, Me.btnSearch)
        Me.SetDefaultButton(Me.moAuthorizationNumberText, Me.btnSearch)
        Me.SetDefaultButton(Me.moFileNameText, Me.btnSearch)



        'Set message confirmation boxes
        Me.AddConfirmationAndDisplayProgressBar(Me.btnSave_WRITE, TranslationBase.TranslateLabelOrMessage(ElitaPlusWebApp.Message.MSG_PROMPT_ARE_YOU_SURE), TranslationBase.TranslateLabelOrMessage(ElitaPlusWebApp.Message.MSG_PERFORMING_REQUEST), False)
        Me.DisplayProgressBarOnClick(Me.btnSearch, ElitaPlusWebApp.Message.MSG_PERFORMING_REQUEST)

        If Me.IsReturningFromChild Then
            Me.moAuthorizationNumberText.Text = Me.State.retObj.AuthenticationNumber
            Me.moCertificateText.Text = Me.State.retObj.CertificateNumber
            Me.moFileNameText.Text = Me.State.retObj.FileName
            searchClick = True
            PopulateDataList()
            Me.IsReturningFromChild = False
        End If

    End Sub

    'Undo button - Reload DS from database
    Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click

        Me.State.dsClaimSuspense = Nothing
        Me.State.selectedPageIndex = Me.DEFAULT_PAGE_INDEX
        PopulateDataList()

    End Sub

    'Build the datalist and page dataset on the search results
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Me.State.dsClaimSuspense = Nothing
        Me.State.arrCertificates = Nothing
        Me.State.selectedPageIndex = Me.DEFAULT_PAGE_INDEX

        searchClick = True
        PopulateDataList()

    End Sub

    'Clear Search Fields
    Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click

        ClearAll()

    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged

        'Save the values in the grid before changing page size
        SaveCurrentGridValues()

        Me.State.selectedPageSize = Integer.Parse(cboPageSize.SelectedValue)
        Me.State.selectedPageIndex = Me.DEFAULT_PAGE_INDEX
        PopulateDataList()

    End Sub

    Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click

        Dim boClaimSuspense As New BusinessObjectsNew.ClaimSuspense
        Dim ret As Integer

        Try

            If Me.State.dsClaimSuspense Is Nothing Then
                Me.moErrorController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, True)
                Exit Sub
            End If

            Me.SaveCurrentGridValues()
            ret = boClaimSuspense.Process(Me.State.dsClaimSuspense)

            If ret >= 0 Then
                Me.AddInfoMsg(ElitaPlusWebApp.Message.MSG_INTERFACES_HAS_COMPLETED)
                'Re-search the same criteria.  Determine if there are any remaining items in the batch.  If items remain, 
                'redisplay the grid, otherwise, clear it all out.

                Me.State.dsClaimSuspense = Nothing
                Me.State.arrCertificates = Nothing
                Me.State.selectedPageIndex = Me.DEFAULT_PAGE_INDEX

                Me.PopulateDataList()

                If Me.moDataGrid.Items.Count = 0 Then
                    ClearAll()
                End If
            Else
                Me.moErrorController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.UNEXPECTED_ERROR)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try

    End Sub

    Private Sub moDataGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged

        'Save the values in the grid before changing pages
        SaveCurrentGridValues()

        Me.State.selectedPageIndex = e.NewPageIndex
        Me.PopulateDataList()
    End Sub

    Private Sub BtnRejectReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRejectReport.Click
        Try


            Dim rptState As New Reports.PrintClaimLoadRejectForm.MyState

            rptState.SearchCertificate = Me.moCertificateText.Text.ToUpper
            rptState.SearchAuthorization = Me.moAuthorizationNumberText.Text.ToUpper
            rptState.SearchFilename = Me.moFileNameText.Text.ToUpper
            rptState.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.CLAIM_SUSPENSE

            Me.callPage(Reports.PrintClaimLoadRejectForm.URL, rptState)

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try
    End Sub

#End Region

#Region "Page Return"
    Private IsReturningFromChild As Boolean = False

    Public Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Me.IsReturningFromChild = True
        Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
        Select Case retObj.LastOperation
            Case ElitaPlusPage.DetailPageCommand.Back
                If Not retObj Is Nothing Then
                    Try
                        Me.State.retObj = retObj
                    Catch ex As Exception
                        Me.HandleErrors(ex, Me.moErrorController)
                    End Try
                End If
        End Select
    End Sub

    Public Class ReturnType
        Public LastOperation As ElitaPlusPage.DetailPageCommand
        Public AuthenticationNumber As String
        Public FileName As String
        Public CertificateNumber As String
        Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal AuthNumber As String, ByVal FName As String, ByVal CertNumber As String)
            Me.LastOperation = LastOp
            AuthenticationNumber = AuthNumber
            FileName = FName
            CertificateNumber = CertNumber
        End Sub
    End Class
#End Region

#Region "Controlling Logic"

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSavePagePromptResponse.Value

        Try
            'If Not confResponse.Equals(EMPTY) Then
            '    If confResponse = Me.MSG_VALUE_YES Then
            '        '   SavePage()
            '    End If
            '    Me.HiddenSavePagePromptResponse.Value = EMPTY
            '    Me.HiddenIsPageDirty.Value = EMPTY

            '    Select Case Me.State.ActionInProgress
            '        Case ElitaPlusPage.DetailPageCommand.Back
            '            '              Dim retType As New ClaimFileProcessedController.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.ClaimfileProcessedId)
            '            Me.ReturnToCallingPage(retType)
            '        Case ElitaPlusPage.DetailPageCommand.GridPageSize
            '            Me.moDataGrid.CurrentPageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
            '            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            '        Case Else
            '            Me.moDataGrid.CurrentPageIndex = Me.State.selectedPageIndex
            '    End Select
            '    PopulateDataList()
            'End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try
    End Sub

    Private Sub BuildCertificateList()

        Me.State.arrCertificates = New ArrayList

        For Each dr As DataRow In Me.State.dsClaimSuspense.Tables(0).Rows
            If (Not Microsoft.VisualBasic.IsDBNull(dr(ClaimSuspenseDAL.COL_NAME_CERTIFICATE))) AndAlso (Not Me.State.arrCertificates.Contains(dr(ClaimSuspenseDAL.COL_NAME_CERTIFICATE))) Then
                Me.State.arrCertificates.Add(dr(ClaimSuspenseDAL.COL_NAME_CERTIFICATE))
            End If
        Next

        Me.State.totalPages = CType(Math.Ceiling(Me.State.arrCertificates.Count / Me.State.selectedPageSize), Integer)

        'ALR 05/25/2007 : Modified  to determine if a filename has been entered.  If so, we don't limit the results
        If Me.moFileNameText.Text.Trim.Length = 0 Then
            Me.lblRecordCount.Text = IIf((Me.State.arrCertificates.Count > ClaimSuspenseDAL.MAX_NUMBER_OF_ROWS - 1), (ClaimSuspenseDAL.MAX_NUMBER_OF_ROWS - 1).ToString, Me.State.arrCertificates.Count.ToString).ToString + " " + TranslationBase.TranslateLabelOrMessage(ElitaPlus.ElitaPlusWebApp.Message.MSG_CERTIFICATES_FOUND)
        Else
            Me.lblRecordCount.Text = Me.State.arrCertificates.Count.ToString + " " + TranslationBase.TranslateLabelOrMessage(ElitaPlus.ElitaPlusWebApp.Message.MSG_CERTIFICATES_FOUND)
        End If

    End Sub

    Protected Sub PopulateDataList()

        Try

            If Me.moFileNameText.Text.Trim.Length = 0 AndAlso _
                    Me.moCertificateText.Text.Trim.Length = 0 AndAlso _
                    Me.moAuthorizationNumberText.Text.Trim.Length = 0 Then
                Dim errors() As Assurant.Common.Validation.ValidationError = {New Assurant.Common.Validation.ValidationError(SEARCH_EXCEPTION, GetType(ClaimSuspense), Nothing, "Search", Nothing)}
                Throw New BOValidationException(errors, GetType(ClaimSuspense).ToString, Me.UniqueID)
            End If

            If Me.State.dsClaimSuspense Is Nothing Then
                Me.State.dsClaimSuspense = BusinessObjectsNew.ClaimSuspense.LoadList(Me.moCertificateText.Text.Trim, Me.moAuthorizationNumberText.Text.Trim, Me.moFileNameText.Text.Trim)
                Me.BuildCertificateList()
            End If

            'Set these fields to mark and fill the key field in the dataset to be able to propogate changes later
            Me.moDataGrid.DataKeyField = ClaimSuspense.COL_NAME_KEY_FIELD

            Me.moDataGrid.PageSize = Me.State.selectedPageSize
            Me.moDataGrid.CurrentPageIndex = Me.State.selectedPageIndex
            Me.moDataGrid.DataSource = Me.State.dsClaimSuspense
            Me.moDataGrid.DataBind()

            ControlMgr.SetVisibleControl(Me, Me.moDataGrid, True)
            ControlMgr.SetVisibleControl(Me, Me.trPageSize, True)

            If moDataGrid.Items.Count > 0 Then
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)
                ControlMgr.SetEnableControl(Me, BtnRejectReport, True)
            Else
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
                ControlMgr.SetEnableControl(Me, BtnRejectReport, False)
            End If

            'ALR 05/25/2007 : Modified to determine if a filename has been entered.  If so, we don't limit the results
            If searchClick AndAlso Me.moFileNameText.Text.Trim.Length = 0 Then
                ValidSearchResultCount(Me.State.dsClaimSuspense.Tables(0).Rows.Count, ClaimSuspenseDAL.MAX_NUMBER_OF_ROWS - 1, True)
            End If
            searchClick = False

        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try


    End Sub

    Private Sub SaveCurrentGridValues()

        Dim dr As DataRow
        Dim dgItem As DataGridItem

        For Each dgItem In moDataGrid.Items

            Try
                dr = Me.State.dsClaimSuspense.Tables(0).Rows.Find(moDataGrid.DataKeys.Item(dgItem.ItemIndex).ToString)

                dr(ClaimSuspenseDAL.COL_NAME_PROCESS_ORDER) = CType(dgItem.FindControl("moSequenceTextGrid"), TextBox).Text
                dr(ClaimSuspenseDAL.COL_NAME_DO_NOT_PROCESS) = CType(dgItem.FindControl("moDoNotProcessTextGrid"), TextBox).Text
                dr(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_NUMBER) = CType(dgItem.FindControl("moAuthorizationNumberTextGrid"), TextBox).Text
                dr(ClaimSuspenseDAL.COL_NAME_REJECT_REASON) = CType(dgItem.FindControl("moRejectReasonTextGrid"), TextBox).Text
                dr(ClaimSuspenseDAL.COL_NAME_DEALER_CODE) = CType(dgItem.FindControl("moDealerCodeTextGrid"), TextBox).Text
                dr(ClaimSuspenseDAL.COL_NAME_SERVICE_CENTER_CODE) = CType(dgItem.FindControl("moServiceCenterCodeTextGrid"), TextBox).Text
                dr(ClaimSuspenseDAL.COL_NAME_SERIAL_NUMBER) = CType(dgItem.FindControl("moSerialNumberTextGrid"), TextBox).Text
                dr(ClaimSuspenseDAL.COL_NAME_PRODUCT_CODE) = CType(dgItem.FindControl("moProductCodeTextGrid"), TextBox).Text
                dr(ClaimSuspenseDAL.COL_NAME_ADDITIONAL_PRODUCT_CODE) = CType(dgItem.FindControl("moAdditionalProductCodeTextGrid"), TextBox).Text
                dr(ClaimSuspenseDAL.COL_NAME_MANUFACTURER) = CType(dgItem.FindControl("moManufacturerTextGrid"), TextBox).Text
                dr(ClaimSuspenseDAL.COL_NAME_MODEL) = CType(dgItem.FindControl("moModelTextGrid"), TextBox).Text

                dr(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_CODE) = CType(dgItem.FindControl("moAuthorizationCodeTextGrid"), TextBox).Text
                dr(ClaimSuspenseDAL.COL_NAME_STATUS_CODE) = CType(dgItem.FindControl("moStatusCodeTextGrid"), TextBox).Text

                'ALR Ticket # 1054112 - No need to set prob description since we are dropping from table.
                'dr(ClaimSuspenseDAL.COL_NAME_PROBLEM_DESCRIPTION) = CType(dgItem.FindControl("moProblemDescriptionTextGrid"), TextBox).Text

                'Check Numeric Fields before settings
                If IsNumeric(CType(dgItem.FindControl("moAmountTextGrid"), TextBox).Text) Then dr(ClaimSuspenseDAL.COL_NAME_AMOUNT) = CType(CType(dgItem.FindControl("moAmountTextGrid"), TextBox).Text, Decimal)

                'Check Date columns before settings
                If (DateHelper.IsDate(CType(dgItem.FindControl("moAuthorizationCreationDateTextGrid"), TextBox).Text)) Then dr(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_CREATION_DATE) = DateHelper.GetDateValue(CType(dgItem.FindControl("moAuthorizationCreationDateTextGrid"), TextBox).Text)
                If (DateHelper.IsDate(CType(dgItem.FindControl("moDateClaimClosedTextGrid"), TextBox).Text)) Then dr(ClaimSuspenseDAL.COL_NAME_DATE_CLAIM_CLOSED) = DateHelper.GetDateValue(CType(dgItem.FindControl("moDateClaimClosedTextGrid"), TextBox).Text)
                If (DateHelper.IsDate(CType(dgItem.FindControl("moCertificateSalesDateTextGrid"), TextBox).Text)) Then dr(ClaimSuspenseDAL.COL_NAME_CERTIFICATE_SALES_DATE) = DateHelper.GetDateValue(CType(dgItem.FindControl("moCertificateSalesDateTextGrid"), TextBox).Text)

                dr.AcceptChanges()

            Catch
            End Try

        Next
    End Sub

    Private Sub ClearAll()

        Me.moAuthorizationNumberText.Text = Me.EMPTY
        Me.moCertificateText.Text = Me.EMPTY
        Me.moFileNameText.Text = Me.EMPTY
        Me.State.dsClaimSuspense = Nothing
        ControlMgr.SetVisibleControl(Me, Me.moDataGrid, False)
        ControlMgr.SetVisibleControl(Me, Me.trPageSize, False)

    End Sub

#End Region

#Region "DataBinding Related"

    Protected Sub DataGridItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)

        'Fill the grid with the claim information
        If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then
            Dim ctl As Control
            Dim lbl As Label
            Dim txt As TextBox
            Dim imgbtn As ImageButton
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If CurrCertificateNumber <> dvRow(ClaimSuspenseDAL.COL_NAME_CERTIFICATE).ToString Then
                CurrCertificateNumber = dvRow(ClaimSuspenseDAL.COL_NAME_CERTIFICATE).ToString
                CurrIndex = 0

                'Set the item back color on the grid
                If CurrColor = Me.GRID_ALT_ITEM_COLOR Then
                    CurrColor = Me.GRID_ITEM_COLOR
                Else
                    CurrColor = Me.GRID_ALT_ITEM_COLOR
                End If

            End If

            CurrIndex += 1
            Try

                e.Item.BackColor = System.Drawing.ColorTranslator.FromHtml(CurrColor)

                'If currIndex = 1 then show certificate, otherwise, do not
                If CurrIndex <> 1 Then
                    ControlMgr.SetVisibleControl(Me, e.Item.FindControl("lblCertificateNumber"), False)
                End If

                Me.PopulateControlFromBOProperty(e.Item.FindControl("lblCertificateNumber"), dvRow(ClaimSuspenseDAL.COL_NAME_CERTIFICATE))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moIdLabel"), dvRow(ClaimSuspenseDAL.COL_NAME_CLAIM_RECON_WRK_ID))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moSequenceTextGrid"), CurrIndex)
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moDoNotProcessTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_DO_NOT_PROCESS))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moFileNameLabelGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_FILENAME))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moClaimActionLabelGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_CLAIM_ACTION))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moAuthorizationNumberTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_NUMBER))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moAuthorizationCreationDateTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_CREATION_DATE))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moRejectReasonTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_REJECT_REASON))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moDealerCodeTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_DEALER_CODE))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moCertificateSalesDateTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_CERTIFICATE_SALES_DATE))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moDateClaimClosedTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_DATE_CLAIM_CLOSED))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moServiceCenterCodeTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_SERVICE_CENTER_CODE))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moSerialNumberTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_SERIAL_NUMBER))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moAmountTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_AMOUNT))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moProductCodeTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_PRODUCT_CODE))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moAdditionalProductCodeTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_ADDITIONAL_PRODUCT_CODE))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moManufacturerTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_MANUFACTURER))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moModelTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_MODEL))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moAuthorizationCodeTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_CODE))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moStatusCodeTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_STATUS_CODE))
                Me.PopulateControlFromBOProperty(e.Item.FindControl("moProblemDescriptionTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_PROBLEM_DESCRIPTION))

                'Add Calendars
                Me.AddCalendar(CType(e.Item.FindControl("ImgCertificateSalesDateTextGrid"), ImageButton), CType(e.Item.FindControl("moCertificateSalesDateTextGrid"), TextBox))
                Me.AddCalendar(CType(e.Item.FindControl("ImgAuthorizationCreationDateTextGrid"), ImageButton), CType(e.Item.FindControl("moAuthorizationCreationDateTextGrid"), TextBox))
                Me.AddCalendar(CType(e.Item.FindControl("ImgDateClaimClosedTextGrid"), ImageButton), CType(e.Item.FindControl("moDateClaimClosedTextGrid"), TextBox))

            Catch ex As Exception
                Me.HandleErrors(ex, Me.moErrorController)
            End Try

        End If

    End Sub

    Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            Me.HandleErrors(ex, Me.moErrorController)
        End Try
    End Sub

#End Region



End Class
