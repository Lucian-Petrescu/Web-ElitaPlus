Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Common.Localization
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage
Imports Assurant.ElitaPlus.Common
Imports System.Data
Imports Microsoft.VisualBasic
Namespace Interfaces

    Partial Class ARPaymentReconWrkForm
        Inherits ElitaPlusSearchPage


#Region "Page State"

        Class MyState
            Public DealerfileProcessedId As Guid
            Public PageIndex As Integer = 0
            Public ActionInProgress As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Nothing_
            Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
            Public selectedPageIndex As Integer = DEFAULT_PAGE_INDEX
            Public RecordMode As String
            Public ParentFile As String = "N"
            Public RejRecNotUpdatable As String = "N"
            Public sortBy As String
            Public SrchRecordType As String
            Public SrchRejectCode As String
            Public SrchRejectReason As String
            Public SortExpression As String
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub SetStateProperties()
            State.DealerfileProcessedId = CType(CallingParameters, Guid)
        End Sub

        Private Sub SetQueryStringParam()
            Try
                If Request.QueryString("RECORDMODE") IsNot Nothing Then
                    State.RecordMode = Request.QueryString("RECORDMODE")
                End If
                If Request.QueryString("PARENTFILE") IsNot Nothing Then
                    State.ParentFile = Request.QueryString("PARENTFILE")
                End If
                If Request.QueryString("REJRECNOTUPDATABLE") IsNot Nothing Then
                    State.RejRecNotUpdatable = Request.QueryString("REJRECNOTUPDATABLE")
                End If
            Catch ex As Exception

            End Try
        End Sub
#End Region


#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (moDataGrid.EditIndex > NO_ITEM_SELECTED_INDEX)
            End Get
        End Property

        Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
            Get
                Return CType(MyBase.Page, ElitaPlusSearchPage)
            End Get
        End Property

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

#End Region
#Region "Constants"
        Public Const URL As String = "ARPaymentReconWrkForm.aspx"

        Private Const ID_COL As Integer = 1
        Private Const RECORD_TYPE_COL As Integer = 2
        Private Const REJECT_CODE_COL As Integer = 3
        Private Const REJECT_REASON_COL As Integer = 4
        Private Const CREDIT_CARD_NUMBER_COL As Integer = 5
        Private Const CERTIFICATE_COL As Integer = 6
        Private Const SUBSCRIBER_NUMBER_COL As Integer = 7
        Private Const PAYMENT_AMOUNT_COL As Integer = 8
        Private Const PAYMENT_DATE_COL As Integer = 9
        Private Const INVOICE_DATE_COL As Integer = 10
        Private Const INVOICE_PERIOD_START_DATE_COL As Integer = 11
        Private Const INVOICE_PERIOD_END_DATE_COL As Integer = 12
        Private Const INVOICE_NUMBER_COL As Integer = 13
        Private Const POST_PRE_PAID_COL As Integer = 14
        Private Const PAYMENT_METHOD_COL As Integer = 15
        Private Const PAYMENT_ENTITY_CODE_COL As Integer = 16
        Private Const PAYMENT_LOADED_COL As Integer = 17
        Private Const APPLICATION_MODE_COL As Integer = 18
        Private Const INSTALLMENT_NUM_COL As Integer = 19
        Private Const CURRENCY_CODE_COL As Integer = 20
        Private Const ENTIRE_RECORD_COL As Integer = 21
        Private Const REFERENCE_COL As Integer = 22
        Private Const SOURCE_COL As Integer = 23
#End Region

#Region "PropertyName"
        ' Property Name
        Private Const RECORD_TYPE_PROPERTY As String = "RecordType"
        Private Const REJECT_REASON_PROPERTY As String = "RejectReason"
        Private Const REJECT_CODE_PROPERTY As String = "Rejectcode"
        Private Const PAYMENT_ENTITY_CODE_PROPERTY As String = "PaymentEntityCode"
        Private Const CERTIFICATE_PROPERTY As String = "Certificate"
        Private Const PAYMENT_LOADED_PROPERTY As String = "PaymentLoaded"
        Private Const PAYMENT_AMOUNT_PROPERTY As String = "PaymentAmount"
        Private Const PAYMENT_DATE_PROPERTY As String = "PaymentDate"
        Private Const INVOICE_DATE_PROPERTY As String = "InvoiceDate"
        Private Const INVOICE_PERIOD_START_DATE_PROPERTY As String = "InvoicePeriodStartDate"
        Private Const INVOICE_PERIOD_END_DATE_PROPERTY As String = "InvoicePeriodEndDate"
        Private Const PAYMENT_INVOICE_NUMBER_PROPERTY As String = "PaymentInvoiceNumber"
        Private Const REJECT_MSG_PARAMS As String = "RejectMsgParams"
        Private Const REFERENCE_PROPERTY As String = "Reference"
        Private Const SOURCE_PROPERTY As String = "Source"
        Private Const INVOICE_NUMBER_PROPERTY As String = "InvoiceNumber"
        Private Const CREDIT_CARD_NUMBER_PROPERTY As String = "CreditCardNum"
        Private Const INSTALLMENT_NUMBER_PROPERTY As String = "InstallmentNumber"
        Private Const CURRENCY_CODE_PROPERTY As String = "Currency_Code"
        Private Const PAYMENT_METHOD_PROPERTY As String = "Payment_Method"
        Private Const PRODUCT_CODE_PROPERTY As String = "Product_Code"
        Private Const APPLICATION_MODE_PROPERTY As String = "ApplicationMode"
        Private Const SUBSCRIBER_NUMBER_PROPERTY As String = "SubscriberNumber"
        Private Const POST_PRE_PAID_PROPERTY As String = "PostPrePaid"
        Private Const ENTIRE_RECORD_PROPERTY As String = "EntireRecord"
        Private Const EMPTY As String = ""
        Private Const DEFAULT_PAGE_INDEX As Integer = 0

        'Actions
        Private Const ACTION_NONE As String = "ACTION_NONE"
        Private Const ACTION_SAVE As String = "ACTION_SAVE"
        Private Const ACTION_NO_EDIT As String = "ACTION_NO_EDIT"
        Private Const ACTION_EDIT As String = "ACTION_EDIT"
        Protected WithEvents Button1 As System.Web.UI.WebControls.Button
        Private Const ACTION_NEW As String = "ACTION_NEW"
#End Region

#Region "Handlers"

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Protected WithEvents moErrorController As ErrorController

        Protected WithEvents CancelButton As System.Web.UI.WebControls.Button

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            MasterPage.MessageController.Clear_Hide()
            SetStateProperties()
            If Not Page.IsPostBack Then
                MasterPage.MessageController.Clear()
                MasterPage.UsePageTabTitleInBreadCrum = False
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Interfaces")
                UpdateBreadCrum()
                SetQueryStringParam()
                SortDirection = EMPTY
                SetGridItemStyleColor(moDataGrid)
                ShowMissingTranslations(MasterPage.MessageController)
                State.PageIndex = 0
                TranslateGridHeader(moDataGrid)
                TranslateGridControls(moDataGrid)
                BaseSetButtonsState(False)
                PopulateReadOnly()
                PopulateGrid()
                cboPageSize.SelectedValue = State.selectedPageSize.ToString()
            Else
                CheckIfComingFromSaveConfirm()
            End If
        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Dim retType As New DealerFileProcessedController_New.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.DealerfileProcessedId)
                    ReturnToCallingPage(retType)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                SavePage()
                DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                HiddenIsPageDirty.Value = EMPTY
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                PopulateGrid()
                HiddenIsPageDirty.Value = EMPTY
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Grid"

        Private Sub moDataGrid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moDataGrid.PageIndexChanging
            Try
                State.selectedPageIndex = e.NewPageIndex
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                Else
                    moDataGrid.PageIndex = e.NewPageIndex
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moDataGrid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                If IsDataGPageDirty() Then
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                Else
                    State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ItemCommand(source As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                Dim nIndex As Integer = e.Item.ItemIndex
                If (e.CommandName = SORT_COMMAND_NAME) Then
                    State.sortBy = CType(e.CommandArgument, String)
                    If IsDataGPageDirty() Then
                        State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridColSort
                        DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                    Else
                        PopulateGrid()
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moDataGrid.Sorting
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
        Private Sub ItemBound(source As Object, e As GridViewRowEventArgs) Handles moDataGrid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim oDateOfPayText As TextBox
            Dim oExtWarrSaleDateText As TextBox
            Dim oTextBox As TextBox

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                '   Display Only
                With e.Row
                    PopulateControlFromBOProperty(.Cells(ID_COL), dvRow(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_INTERFACE_ID))
                    Dim oDrop As DropDownList = CType(e.Row.FindControl("moRecordTypeDrop"), DropDownList)
                    oDrop.Attributes.Add("onchange", "setDirty()")
                    PopulateRecordTypeDrop(oDrop)
                    PopulateRecordTypeDrop(oDrop)
                    Dim oValue As String = CType(dvRow(ARPaymentReconWrkDAL.COL_NAME_RECORD_TYPE), String)
                    SetSelectedItemByText(oDrop, oValue)

                    oTextBox = CType(e.Row.Cells(REJECT_REASON_COL).FindControl("RejectReasonTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Check if message requires parameterized conversion in specific language. if required translate message accordingly : REQ-1264
                    Dim strMsg As String = GetSpecificRejectionReason(dvRow)
                    If (strMsg <> String.Empty) Then
                        dvRow(ARPaymentReconWrkDAL.COL_NAME_REJECT_REASON) = strMsg
                    End If
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_REJECT_REASON))

                    oTextBox = CType(e.Row.Cells(CREDIT_CARD_NUMBER_COL).FindControl("moCreditCardGrid"), TextBox)
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_CREDIT_CARD_NUMBER))

                    oTextBox = CType(e.Row.Cells(CREDIT_CARD_NUMBER_COL).FindControl("moRejectCodeGrid"), TextBox)
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_REJECT_CODE))

                    oTextBox = CType(e.Row.Cells(CERTIFICATE_COL).FindControl("moCertificateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_CERTIFICATE))
                    oTextBox = CType(e.Row.Cells(SUBSCRIBER_NUMBER_COL).FindControl("moSubscriberNumberGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_SUBSCRIBER_NUMBER))
                    oTextBox = CType(e.Row.Cells(PAYMENT_AMOUNT_COL).FindControl("moPaymentAmountTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_AMOUNT), "N5")
                    oDateOfPayText = CType(e.Row.Cells(PAYMENT_DATE_COL).FindControl("moPaymentDateGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oDateOfPayText, dvRow(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_DATE))
                    oExtWarrSaleDateText = CType(e.Row.Cells(PAYMENT_DATE_COL).FindControl("moInvoiceDateGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oExtWarrSaleDateText, dvRow(ARPaymentReconWrkDAL.COL_NAME_INVOICE_DATE))
                    oTextBox = CType(e.Row.Cells(INVOICE_DATE_COL).FindControl("moInvoicePeriodStartDateGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_INVOICE_PERIOD_START_DATE))
                    oTextBox = CType(e.Row.Cells(INVOICE_PERIOD_END_DATE_COL).FindControl("moInvoicePeriodEndDateGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_INVOICE_PERIOD_END_DATE))
                    oTextBox = CType(e.Row.Cells(INVOICE_NUMBER_COL).FindControl("moInvoiceNumberGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_INVOICE_NUMBER))
                    oTextBox = CType(e.Row.Cells(POST_PRE_PAID_COL).FindControl("moPostPrePaidGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_POST_PRE_PAID))
                    oTextBox = CType(e.Row.Cells(PAYMENT_METHOD_COL).FindControl("moPaymentMethodGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_METHOD))
                    oTextBox = CType(e.Row.Cells(PAYMENT_ENTITY_CODE_COL).FindControl("moPaymentEntityCodeGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_PAYMENT_ENTITY_CODE), "N5")
                    oTextBox = CType(e.Row.Cells(PAYMENT_LOADED_COL).FindControl("moPaymentLoadedGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_Payment_LOADED))
                    oTextBox = CType(e.Row.Cells(APPLICATION_MODE_COL).FindControl("moApplicationModeGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_APPLICATION_MODE), "N5")

                    oTextBox = CType(e.Row.Cells(INSTALLMENT_NUM_COL).FindControl("moInstallmentNumTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_INSTALLMENT_NUMBER))

                    oTextBox = CType(e.Row.Cells(CURRENCY_CODE_COL).FindControl("moFeeIncomeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_CURRENTCY_CODE))

                    oTextBox = CType(e.Row.Cells(SOURCE_COL).FindControl("moSourceGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_SOURCE))

                    oTextBox = CType(e.Row.Cells(REFERENCE_COL).FindControl("moReferenceGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    PopulateControlFromBOProperty(oTextBox, dvRow(ARPaymentReconWrkDAL.COL_NAME_REFERENCE))
                End With

                Dim oDateOfPayImage As ImageButton = CType(e.Row.Cells(PAYMENT_DATE_COL).FindControl("moDateOfPayImageGrid"), ImageButton)

                Dim oDatePaidForImage As ImageButton = CType(e.Row.Cells(INVOICE_DATE_COL).FindControl("moDatePaidForImageGrid"), ImageButton)

                If (oDateOfPayImage IsNot Nothing) Then
                    AddCalendar(oDateOfPayImage, oDateOfPayText)
                End If
                If (oDatePaidForImage IsNot Nothing) Then
                    AddCalendar(oDatePaidForImage, oExtWarrSaleDateText)
                End If
            End If
            BaseItemBound(source, e)
        End Sub

        Protected Function GetSpecificRejectionReason(dvRow As DataRowView) As String
            Dim dr As DataRow
            Dim strMsg As String

            If dvRow IsNot Nothing Then
                dr = dvRow.Row
            End If

            Return strMsg

        End Function

        Protected Sub ItemCreated(sender As Object, e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders(ARPaymentReconWrkInfo As ARPaymentReconWrk)
            BindBOPropertyToGridHeader(ARPaymentReconWrkInfo, RECORD_TYPE_PROPERTY, moDataGrid.Columns(RECORD_TYPE_COL))
            BindBOPropertyToGridHeader(ARPaymentReconWrkInfo, REJECT_REASON_PROPERTY, moDataGrid.Columns(REJECT_REASON_COL))
            BindBOPropertyToGridHeader(ARPaymentReconWrkInfo, PAYMENT_ENTITY_CODE_PROPERTY, moDataGrid.Columns(PAYMENT_ENTITY_CODE_COL))
            BindBOPropertyToGridHeader(ARPaymentReconWrkInfo, CERTIFICATE_PROPERTY, moDataGrid.Columns(CERTIFICATE_COL))
            BindBOPropertyToGridHeader(ARPaymentReconWrkInfo, PAYMENT_LOADED_PROPERTY, moDataGrid.Columns(PAYMENT_LOADED_COL))
            BindBOPropertyToGridHeader(ARPaymentReconWrkInfo, PAYMENT_AMOUNT_PROPERTY, moDataGrid.Columns(PAYMENT_AMOUNT_COL))
            BindBOPropertyToGridHeader(ARPaymentReconWrkInfo, PAYMENT_DATE_PROPERTY, moDataGrid.Columns(PAYMENT_DATE_COL))
            BindBOPropertyToGridHeader(ARPaymentReconWrkInfo, INVOICE_DATE_PROPERTY, moDataGrid.Columns(INVOICE_DATE_COL))
            BindBOPropertyToGridHeader(ARPaymentReconWrkInfo, INVOICE_PERIOD_START_DATE_PROPERTY, moDataGrid.Columns(INVOICE_PERIOD_START_DATE_COL))
            BindBOPropertyToGridHeader(ARPaymentReconWrkInfo, INVOICE_PERIOD_END_DATE_PROPERTY, moDataGrid.Columns(INVOICE_PERIOD_END_DATE_COL))
            BindBOPropertyToGridHeader(ARPaymentReconWrkInfo, REFERENCE_PROPERTY, moDataGrid.Columns(REFERENCE_COL))
            BindBOPropertyToGridHeader(ARPaymentReconWrkInfo, SOURCE_PROPERTY, moDataGrid.Columns(SOURCE_COL))
            BindBOPropertyToGridHeader(ARPaymentReconWrkInfo, CREDIT_CARD_NUMBER_PROPERTY, moDataGrid.Columns(CREDIT_CARD_NUMBER_COL))
            BindBOPropertyToGridHeader(ARPaymentReconWrkInfo, APPLICATION_MODE_PROPERTY, moDataGrid.Columns(APPLICATION_MODE_COL))
            BindBOPropertyToGridHeader(ARPaymentReconWrkInfo, INSTALLMENT_NUMBER_PROPERTY, moDataGrid.Columns(INSTALLMENT_NUM_COL))
            BindBOPropertyToGridHeader(ARPaymentReconWrkInfo, SUBSCRIBER_NUMBER_PROPERTY, moDataGrid.Columns(SUBSCRIBER_NUMBER_COL))
            ClearGridViewHeadersAndLabelsErrSign()
        End Sub

#End Region

#End Region

#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("AR_PAYMENT")
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("AR_PAYMENT")
        End Sub
        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSavePagePromptResponse.Value

            Try
                If Not confResponse.Equals(EMPTY) Then
                    If confResponse = MSG_VALUE_YES Then
                        SavePage()
                    End If
                    HiddenSavePagePromptResponse.Value = EMPTY
                    HiddenIsPageDirty.Value = EMPTY

                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retType As New DealerFileProcessedController_New.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.DealerfileProcessedId)
                            ReturnToCallingPage(retType)
                        Case ElitaPlusPage.DetailPageCommand.GridPageSize
                            moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                        Case Else
                            moDataGrid.PageIndex = State.selectedPageIndex
                    End Select
                    PopulateGrid()
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Function CreateBoFromGrid(index As Integer) As ARPaymentReconWrk
            Dim ARPaymentReconWrkId As Guid
            Dim ARPaymentReconWrkInfo As ARPaymentReconWrk
            Dim sModifiedDate As String

            moDataGrid.SelectedIndex = index
            ARPaymentReconWrkId = New Guid(moDataGrid.Rows(index).Cells(ID_COL).Text)
            ARPaymentReconWrkInfo = New ARPaymentReconWrk(ARPaymentReconWrkId, sModifiedDate)
            Return ARPaymentReconWrkInfo
        End Function

        Private Sub SavePage()
            Dim index As Integer = 0
            Dim ArpaymentReconWrkInfo As ARPaymentReconWrk
            Dim totItems As Integer = moDataGrid.Rows.Count

            If totItems > 0 Then
                ArpaymentReconWrkInfo = CreateBoFromGrid(0)
                BindBoPropertiesToGridHeaders(ArpaymentReconWrkInfo)
                PopulateBOFromForm(ArpaymentReconWrkInfo)
                ArpaymentReconWrkInfo.Save()
            End If

            totItems = totItems - 1
            For index = 1 To totItems
                ArpaymentReconWrkInfo = CreateBoFromGrid(index)
                BindBoPropertiesToGridHeaders(ArpaymentReconWrkInfo)
                PopulateBOFromForm(ArpaymentReconWrkInfo)
                ArpaymentReconWrkInfo.Save()
            Next
        End Sub

        Function IsDataGPageDirty() As Boolean
            Dim Result As String = HiddenIsPageDirty.Value

            Return Result.Equals("YES")
        End Function

#End Region

#Region "Button-Management"

        Public Overrides Sub BaseSetButtonsState(bIsEdit As Boolean)
            SetButtonsState(bIsEdit)
        End Sub

        Private Sub SetButtonsState(bIsEdit As Boolean)
            If (bIsEdit = True) Then
                'SaveButton_WRITE.Visible = True
                'CancelButton.Visible = True
                'btnBack.Visible = False
            Else
                'SaveButton_WRITE.Visible = False
                'CancelButton.Visible = False
                btnBack.Visible = True
                ControlMgr.SetVisibleControl(Me, btnBack, True)
            End If

        End Sub

#End Region

#Region "Populate"

        Private Sub PopulateReadOnly()
            Try
                Dim oDealerFile As DealerFileProcessed = New DealerFileProcessed(State.DealerfileProcessedId)
                With oDealerFile
                    If State.ParentFile = "N" Then
                        moDealerNameText.Text = .DealerNameLoad
                    Else
                        moDealerNameText.Text = EMPTY
                    End If

                    moFileNameText.Text = .Filename
                End With
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = ACTION_NONE)

            Dim dv As DataView
            Dim recCount As Integer = 0

            Try
                dv = GetDV()
                If Not SortDirection.Equals(EMPTY) Then
                    dv.Sort = SortDirection
                    HighLightSortColumn(moDataGrid, SortDirection)
                End If
                recCount = dv.Count
                Session("recCount") = recCount
                moDataGrid.PageSize = State.selectedPageSize
                moDataGrid.DataSource = dv.ToTable
                moDataGrid.DataBind()
                lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

                If State.RecordMode IsNot Nothing AndAlso State.RejRecNotUpdatable = "Y" Then
                    ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid, True)
                Else
                    ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Function GetDV() As DataView
            Dim dv As DataView

            dv = GetGridDataView()

            Return (dv)
        End Function

        Private Function GetGridDataView() As DataView

            With State
                Return (Assurant.ElitaPlus.BusinessObjectsNew.ARPaymentReconWrk.LoadList(.DealerfileProcessedId,
                                                                                         .RecordMode,
                                                                                         .srchRecordType,
                                                                                         .srchRejectCode,
                                                                                         .srchRejectReason,
                                                                                         .ParentFile,
                                                                                         .selectedPageIndex,
                                                                                         .selectedPageSize,
                                                                                         .SortExpression))


            End With

        End Function

        Private Sub PopulateBOItem(ARPaymentReconWrkInfo As ARPaymentReconWrk, oPropertyName As String, oCellPosition As Integer)
            PopulateBOProperty(ARPaymentReconWrkInfo, oPropertyName,
                                            CType(GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBODrop(ARPaymentReconWrkInfo As ARPaymentReconWrk, oPropertyName As String, oCellPosition As Integer)
            PopulateBOProperty(ARPaymentReconWrkInfo, oPropertyName,
                                CType(GetSelectedGridControl(moDataGrid, oCellPosition), DropDownList), False)
        End Sub

        Private Sub PopulateBOFromForm(ARPaymentReconWrkInfo As ARPaymentReconWrk)
            PopulateBODrop(ARPaymentReconWrkInfo, RECORD_TYPE_PROPERTY, RECORD_TYPE_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, REJECT_CODE_PROPERTY, REJECT_CODE_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, REJECT_REASON_PROPERTY, REJECT_REASON_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, CREDIT_CARD_NUMBER_PROPERTY, CREDIT_CARD_NUMBER_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, CERTIFICATE_PROPERTY, CERTIFICATE_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, PAYMENT_DATE_PROPERTY, PAYMENT_DATE_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, PAYMENT_AMOUNT_PROPERTY, PAYMENT_AMOUNT_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, PAYMENT_LOADED_PROPERTY, PAYMENT_LOADED_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, INVOICE_DATE_PROPERTY, INVOICE_DATE_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, INVOICE_PERIOD_START_DATE_PROPERTY, INVOICE_PERIOD_START_DATE_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, INVOICE_PERIOD_END_DATE_PROPERTY, INVOICE_PERIOD_END_DATE_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, REFERENCE_PROPERTY, REFERENCE_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, SUBSCRIBER_NUMBER_PROPERTY, SUBSCRIBER_NUMBER_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, SOURCE_PROPERTY, SOURCE_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, INVOICE_NUMBER_PROPERTY, INVOICE_NUMBER_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, INSTALLMENT_NUMBER_PROPERTY, INSTALLMENT_NUM_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, APPLICATION_MODE_PROPERTY, APPLICATION_MODE_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, POST_PRE_PAID_PROPERTY, POST_PRE_PAID_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, PAYMENT_METHOD_PROPERTY, PAYMENT_METHOD_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, PAYMENT_ENTITY_CODE_PROPERTY, PAYMENT_ENTITY_CODE_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, CURRENCY_CODE_PROPERTY, CURRENCY_CODE_COL)
            PopulateBOItem(ARPaymentReconWrkInfo, ENTIRE_RECORD_PROPERTY, ENTIRE_RECORD_COL)

            If ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub PopulateFormItem(oCellPosition As Integer, oPropertyValue As Object)
            PopulateControlFromBOProperty(GetSelectedGridControl(moDataGrid, oCellPosition), oPropertyValue)
        End Sub

        Sub PopulateRecordTypeDrop(recordTypeDrop As DropDownList, Optional ByVal AddNothingSelected As Boolean = False)
            Try
                Dim oLangId As Guid = Authentication.LangId
                Dim recordTypeList As DataView = LookupListNew.GetPymtRecordTypeLookupList(oLangId)
                BindListControlToDataView(recordTypeDrop, recordTypeList, , , AddNothingSelected)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

    End Class

End Namespace
