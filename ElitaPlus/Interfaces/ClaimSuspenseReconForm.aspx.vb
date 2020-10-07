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

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        moErrorController.Clear_Hide()

        If Not Page.IsPostBack Then
            ShowMissingTranslations(moErrorController)
            BaseSetButtonsState(False)

            'Disable the process button until after searching
            ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
            ControlMgr.SetEnableControl(Me, BtnRejectReport, False)

        Else
            CheckIfComingFromSaveConfirm()
        End If

        'Set default button to search
        SetDefaultButton(moCertificateText, btnSearch)
        SetDefaultButton(moAuthorizationNumberText, btnSearch)
        SetDefaultButton(moFileNameText, btnSearch)



        'Set message confirmation boxes
        AddConfirmationAndDisplayProgressBar(btnSave_WRITE, TranslationBase.TranslateLabelOrMessage(ElitaPlusWebApp.Message.MSG_PROMPT_ARE_YOU_SURE), TranslationBase.TranslateLabelOrMessage(ElitaPlusWebApp.Message.MSG_PERFORMING_REQUEST), False)
        DisplayProgressBarOnClick(btnSearch, ElitaPlusWebApp.Message.MSG_PERFORMING_REQUEST)

        If IsReturningFromChild Then
            moAuthorizationNumberText.Text = State.retObj.AuthenticationNumber
            moCertificateText.Text = State.retObj.CertificateNumber
            moFileNameText.Text = State.retObj.FileName
            searchClick = True
            PopulateDataList()
            IsReturningFromChild = False
        End If

    End Sub

    'Undo button - Reload DS from database
    Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click

        State.dsClaimSuspense = Nothing
        State.selectedPageIndex = DEFAULT_PAGE_INDEX
        PopulateDataList()

    End Sub

    'Build the datalist and page dataset on the search results
    Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click

        State.dsClaimSuspense = Nothing
        State.arrCertificates = Nothing
        State.selectedPageIndex = DEFAULT_PAGE_INDEX

        searchClick = True
        PopulateDataList()

    End Sub

    'Clear Search Fields
    Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click

        ClearAll()

    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged

        'Save the values in the grid before changing page size
        SaveCurrentGridValues()

        State.selectedPageSize = Integer.Parse(cboPageSize.SelectedValue)
        State.selectedPageIndex = DEFAULT_PAGE_INDEX
        PopulateDataList()

    End Sub

    Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click

        Dim boClaimSuspense As New BusinessObjectsNew.ClaimSuspense
        Dim ret As Integer

        Try

            If State.dsClaimSuspense Is Nothing Then
                moErrorController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_SEARCH_FIELD_NOT_SUPPLIED_ERR, True)
                Exit Sub
            End If

            SaveCurrentGridValues()
            ret = boClaimSuspense.Process(State.dsClaimSuspense)

            If ret >= 0 Then
                AddInfoMsg(ElitaPlusWebApp.Message.MSG_INTERFACES_HAS_COMPLETED)
                'Re-search the same criteria.  Determine if there are any remaining items in the batch.  If items remain, 
                'redisplay the grid, otherwise, clear it all out.

                State.dsClaimSuspense = Nothing
                State.arrCertificates = Nothing
                State.selectedPageIndex = DEFAULT_PAGE_INDEX

                PopulateDataList()

                If moDataGrid.Items.Count = 0 Then
                    ClearAll()
                End If
            Else
                moErrorController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.UNEXPECTED_ERROR)
            End If

        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try

    End Sub

    Private Sub moDataGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged

        'Save the values in the grid before changing pages
        SaveCurrentGridValues()

        State.selectedPageIndex = e.NewPageIndex
        PopulateDataList()
    End Sub

    Private Sub BtnRejectReport_Click(sender As System.Object, e As System.EventArgs) Handles BtnRejectReport.Click
        Try


            Dim rptState As New Reports.PrintClaimLoadRejectForm.MyState

            rptState.SearchCertificate = moCertificateText.Text.ToUpper
            rptState.SearchAuthorization = moAuthorizationNumberText.Text.ToUpper
            rptState.SearchFilename = moFileNameText.Text.ToUpper
            rptState.moInterfaceTypeCode = ClaimFileProcessedData.InterfaceTypeCode.CLAIM_SUSPENSE

            callPage(Reports.PrintClaimLoadRejectForm.URL, rptState)

        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try
    End Sub

#End Region

#Region "Page Return"
    Private IsReturningFromChild As Boolean = False

    Public Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        IsReturningFromChild = True
        Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
        Select Case retObj.LastOperation
            Case ElitaPlusPage.DetailPageCommand.Back
                If retObj IsNot Nothing Then
                    Try
                        State.retObj = retObj
                    Catch ex As Exception
                        HandleErrors(ex, moErrorController)
                    End Try
                End If
        End Select
    End Sub

    Public Class ReturnType
        Public LastOperation As ElitaPlusPage.DetailPageCommand
        Public AuthenticationNumber As String
        Public FileName As String
        Public CertificateNumber As String
        Public Sub New(LastOp As ElitaPlusPage.DetailPageCommand, AuthNumber As String, FName As String, CertNumber As String)
            LastOperation = LastOp
            AuthenticationNumber = AuthNumber
            FileName = FName
            CertificateNumber = CertNumber
        End Sub
    End Class
#End Region

#Region "Controlling Logic"

    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSavePagePromptResponse.Value

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
            HandleErrors(ex, moErrorController)
        End Try
    End Sub

    Private Sub BuildCertificateList()

        State.arrCertificates = New ArrayList

        For Each dr As DataRow In State.dsClaimSuspense.Tables(0).Rows
            If (Not Microsoft.VisualBasic.IsDBNull(dr(ClaimSuspenseDAL.COL_NAME_CERTIFICATE))) AndAlso (Not State.arrCertificates.Contains(dr(ClaimSuspenseDAL.COL_NAME_CERTIFICATE))) Then
                State.arrCertificates.Add(dr(ClaimSuspenseDAL.COL_NAME_CERTIFICATE))
            End If
        Next

        State.totalPages = CType(Math.Ceiling(State.arrCertificates.Count / State.selectedPageSize), Integer)

        'ALR 05/25/2007 : Modified  to determine if a filename has been entered.  If so, we don't limit the results
        If moFileNameText.Text.Trim.Length = 0 Then
            lblRecordCount.Text = IIf((State.arrCertificates.Count > ClaimSuspenseDAL.MAX_NUMBER_OF_ROWS - 1), (ClaimSuspenseDAL.MAX_NUMBER_OF_ROWS - 1).ToString, State.arrCertificates.Count.ToString).ToString + " " + TranslationBase.TranslateLabelOrMessage(ElitaPlus.ElitaPlusWebApp.Message.MSG_CERTIFICATES_FOUND)
        Else
            lblRecordCount.Text = State.arrCertificates.Count.ToString + " " + TranslationBase.TranslateLabelOrMessage(ElitaPlus.ElitaPlusWebApp.Message.MSG_CERTIFICATES_FOUND)
        End If

    End Sub

    Protected Sub PopulateDataList()

        Try

            If moFileNameText.Text.Trim.Length = 0 AndAlso _
                    moCertificateText.Text.Trim.Length = 0 AndAlso _
                    moAuthorizationNumberText.Text.Trim.Length = 0 Then
                Dim errors() As Assurant.Common.Validation.ValidationError = {New Assurant.Common.Validation.ValidationError(SEARCH_EXCEPTION, GetType(ClaimSuspense), Nothing, "Search", Nothing)}
                Throw New BOValidationException(errors, GetType(ClaimSuspense).ToString, UniqueID)
            End If

            If State.dsClaimSuspense Is Nothing Then
                State.dsClaimSuspense = BusinessObjectsNew.ClaimSuspense.LoadList(moCertificateText.Text.Trim, moAuthorizationNumberText.Text.Trim, moFileNameText.Text.Trim)
                BuildCertificateList()
            End If

            'Set these fields to mark and fill the key field in the dataset to be able to propogate changes later
            moDataGrid.DataKeyField = ClaimSuspense.COL_NAME_KEY_FIELD

            moDataGrid.PageSize = State.selectedPageSize
            moDataGrid.CurrentPageIndex = State.selectedPageIndex
            moDataGrid.DataSource = State.dsClaimSuspense
            moDataGrid.DataBind()

            ControlMgr.SetVisibleControl(Me, moDataGrid, True)
            ControlMgr.SetVisibleControl(Me, trPageSize, True)

            If moDataGrid.Items.Count > 0 Then
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, True)
                ControlMgr.SetEnableControl(Me, BtnRejectReport, True)
            Else
                ControlMgr.SetEnableControl(Me, btnSave_WRITE, False)
                ControlMgr.SetEnableControl(Me, BtnRejectReport, False)
            End If

            'ALR 05/25/2007 : Modified to determine if a filename has been entered.  If so, we don't limit the results
            If searchClick AndAlso moFileNameText.Text.Trim.Length = 0 Then
                ValidSearchResultCount(State.dsClaimSuspense.Tables(0).Rows.Count, ClaimSuspenseDAL.MAX_NUMBER_OF_ROWS - 1, True)
            End If
            searchClick = False

        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try


    End Sub

    Private Sub SaveCurrentGridValues()

        Dim dr As DataRow
        Dim dgItem As DataGridItem

        For Each dgItem In moDataGrid.Items

            Try
                dr = State.dsClaimSuspense.Tables(0).Rows.Find(moDataGrid.DataKeys.Item(dgItem.ItemIndex).ToString)

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

        moAuthorizationNumberText.Text = EMPTY
        moCertificateText.Text = EMPTY
        moFileNameText.Text = EMPTY
        State.dsClaimSuspense = Nothing
        ControlMgr.SetVisibleControl(Me, moDataGrid, False)
        ControlMgr.SetVisibleControl(Me, trPageSize, False)

    End Sub

#End Region

#Region "DataBinding Related"

    Protected Sub DataGridItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)

        'Fill the grid with the claim information
        If e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item Then
            Dim ctl As Control
            Dim lbl As Label
            Dim txt As TextBox
            Dim imgbtn As ImageButton
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If CurrCertificateNumber <> dvRow(ClaimSuspenseDAL.COL_NAME_CERTIFICATE).ToString Then
                CurrCertificateNumber = dvRow(ClaimSuspenseDAL.COL_NAME_CERTIFICATE).ToString
                CurrIndex = 0

                'Set the item back color on the grid
                If CurrColor = GRID_ALT_ITEM_COLOR Then
                    CurrColor = GRID_ITEM_COLOR
                Else
                    CurrColor = GRID_ALT_ITEM_COLOR
                End If

            End If

            CurrIndex += 1
            Try

                e.Item.BackColor = System.Drawing.ColorTranslator.FromHtml(CurrColor)

                'If currIndex = 1 then show certificate, otherwise, do not
                If CurrIndex <> 1 Then
                    ControlMgr.SetVisibleControl(Me, e.Item.FindControl("lblCertificateNumber"), False)
                End If

                PopulateControlFromBOProperty(e.Item.FindControl("lblCertificateNumber"), dvRow(ClaimSuspenseDAL.COL_NAME_CERTIFICATE))
                PopulateControlFromBOProperty(e.Item.FindControl("moIdLabel"), dvRow(ClaimSuspenseDAL.COL_NAME_CLAIM_RECON_WRK_ID))
                PopulateControlFromBOProperty(e.Item.FindControl("moSequenceTextGrid"), CurrIndex)
                PopulateControlFromBOProperty(e.Item.FindControl("moDoNotProcessTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_DO_NOT_PROCESS))
                PopulateControlFromBOProperty(e.Item.FindControl("moFileNameLabelGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_FILENAME))
                PopulateControlFromBOProperty(e.Item.FindControl("moClaimActionLabelGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_CLAIM_ACTION))
                PopulateControlFromBOProperty(e.Item.FindControl("moAuthorizationNumberTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_NUMBER))
                PopulateControlFromBOProperty(e.Item.FindControl("moAuthorizationCreationDateTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_CREATION_DATE))
                PopulateControlFromBOProperty(e.Item.FindControl("moRejectReasonTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_REJECT_REASON))
                PopulateControlFromBOProperty(e.Item.FindControl("moDealerCodeTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_DEALER_CODE))
                PopulateControlFromBOProperty(e.Item.FindControl("moCertificateSalesDateTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_CERTIFICATE_SALES_DATE))
                PopulateControlFromBOProperty(e.Item.FindControl("moDateClaimClosedTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_DATE_CLAIM_CLOSED))
                PopulateControlFromBOProperty(e.Item.FindControl("moServiceCenterCodeTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_SERVICE_CENTER_CODE))
                PopulateControlFromBOProperty(e.Item.FindControl("moSerialNumberTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_SERIAL_NUMBER))
                PopulateControlFromBOProperty(e.Item.FindControl("moAmountTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_AMOUNT))
                PopulateControlFromBOProperty(e.Item.FindControl("moProductCodeTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_PRODUCT_CODE))
                PopulateControlFromBOProperty(e.Item.FindControl("moAdditionalProductCodeTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_ADDITIONAL_PRODUCT_CODE))
                PopulateControlFromBOProperty(e.Item.FindControl("moManufacturerTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_MANUFACTURER))
                PopulateControlFromBOProperty(e.Item.FindControl("moModelTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_MODEL))
                PopulateControlFromBOProperty(e.Item.FindControl("moAuthorizationCodeTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_AUTHORIZATION_CODE))
                PopulateControlFromBOProperty(e.Item.FindControl("moStatusCodeTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_STATUS_CODE))
                PopulateControlFromBOProperty(e.Item.FindControl("moProblemDescriptionTextGrid"), dvRow(ClaimSuspenseDAL.COL_NAME_PROBLEM_DESCRIPTION))

                'Add Calendars
                AddCalendar(CType(e.Item.FindControl("ImgCertificateSalesDateTextGrid"), ImageButton), CType(e.Item.FindControl("moCertificateSalesDateTextGrid"), TextBox))
                AddCalendar(CType(e.Item.FindControl("ImgAuthorizationCreationDateTextGrid"), ImageButton), CType(e.Item.FindControl("moAuthorizationCreationDateTextGrid"), TextBox))
                AddCalendar(CType(e.Item.FindControl("ImgDateClaimClosedTextGrid"), ImageButton), CType(e.Item.FindControl("moDateClaimClosedTextGrid"), TextBox))

            Catch ex As Exception
                HandleErrors(ex, moErrorController)
            End Try

        End If

    End Sub

    Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, moErrorController)
        End Try
    End Sub

#End Region



End Class
