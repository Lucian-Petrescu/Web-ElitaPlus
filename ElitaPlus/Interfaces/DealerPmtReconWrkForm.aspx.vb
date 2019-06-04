Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Common.Localization
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage
Imports Assurant.ElitaPlus.Common
Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Globalization
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Threading
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Namespace Interfaces

    Partial Class DealerPmtReconWrkForm
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
            Me.State.DealerfileProcessedId = CType(Me.CallingParameters, Guid)
        End Sub

        Private Sub SetQueryStringParam()
            Try
                If Not Request.QueryString("RECORDMODE") Is Nothing Then
                    Me.State.RecordMode = Request.QueryString("RECORDMODE")
                End If
                If Not Request.QueryString("PARENTFILE") Is Nothing Then
                    Me.State.ParentFile = Request.QueryString("PARENTFILE")
                End If
                If Not Request.QueryString("REJRECNOTUPDATABLE") Is Nothing Then
                    Me.State.RejRecNotUpdatable = Request.QueryString("REJRECNOTUPDATABLE")
                End If
            Catch ex As Exception

            End Try
        End Sub
#End Region


#Region "Properties"

        Public ReadOnly Property IsEditing() As Boolean
            Get
                IsEditing = (Me.moDataGrid.EditIndex > Me.NO_ITEM_SELECTED_INDEX)
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
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

#End Region
#Region "Constants"
        Public Const URL As String = "DealerPmtReconWrkForm.aspx"

        Private Const ID_COL As Integer = 1
        Private Const RECORD_TYPE_COL As Integer = 2
        Private Const REJECT_REASON_COL As Integer = 3
        Private Const DEALER_COL As Integer = 4
        Private Const CERTIFICATE_COL As Integer = 5
        Private Const SERIAL_NUMBER_COL As Integer = 6
        Private Const PAYMENT_AMOUNT_COL As Integer = 7
        Private Const DATE_OF_PAY_COL As Integer = 8
        Private Const DATE_PAID_FOR_COL As Integer = 9
        Private Const CAMPAIGN_NUMBER_COL As Integer = 10
        Private Const NEW_PRODUCT_CODE_COL As Integer = 11
        Private Const PRODUCT_CODE_COL As Integer = 12
        Private Const MODIFIED_DATE_COL As Integer = 13
        Private Const MEMBERSHIP_NUMBER_COL As Integer = 14
        Private Const PAYMENT_INVOICE_NUMBER_COL As Integer = 15
        Private Const COLLECTED_AMOUNT_COL As Integer = 16
        Private Const SERVICE_LINE_NUMBER_COL As Integer = 17
        Private Const ADJUSTMENT_AMOUNT_COL As Integer = 18
        Private Const INSTALLMENT_NUM_COL As Integer = 19
        Private Const FEE_INCOME_COL As Integer = 20


        ' Property Name
        Private Const RECORD_TYPE_PROPERTY As String = "RecordType"
        Private Const REJECT_REASON_PROPERTY As String = "RejectReason"
        Private Const DEALER_PROPERTY As String = "Dealer"
        Private Const CERTIFICATE_PROPERTY As String = "Certificate"
        Private Const SERIAL_NUMBER_PROPERTY As String = "SerialNumber"
        Private Const PAYMENT_AMOUNT_PROPERTY As String = "PaymentAmount"
        Private Const DATE_OF_PAY_PROPERTY As String = "DateOfPayment"
        Private Const DATE_PAID_FOR_PROPERTY As String = "DatePaidFor"
        Private Const CAMPAIGN_NUMBER_PROPERTY As String = "CampaignNumber"
        Private Const MEMBERSHIP_NUMBER_PROPERTY As String = "MembershipNumber"
        Private Const PAYMENT_INVOICE_NUMBER_PROPERTY As String = "PaymentInvoiceNumber"
        Private Const COLLECTED_AMOUNT_PROPERTY As String = "CollectedAmount"
        Private Const SERVICE_LINE_NUMBER_PROPERTY As String = "ServiceLineNumber"
        Private Const NEW_PRODUCT_CODE_PROPERTY As String = "NewProductCode"
        Private Const PRODUCT_CODE_PROPERTY As String = "ProductCode"
        Private Const ADJUSTEMNT_AMOUNT_PROPERTY As String = "AdjustmentAmount"
        Private Const INSTALLMENT_NUMBER_PROPERTY As String = "InstallmentNum"
        Private Const FEE_INCOME_PROPERTY As String = "FeeIncome"

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

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Me.MasterPage.MessageController.Clear_Hide()
            Me.SetStateProperties()
            If Not Page.IsPostBack Then
                Me.MasterPage.MessageController.Clear()
                Me.MasterPage.UsePageTabTitleInBreadCrum = False
                Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Interfaces")
                UpdateBreadCrum()
                SetQueryStringParam()
                Me.SortDirection = EMPTY
                Me.SetGridItemStyleColor(moDataGrid)
                Me.ShowMissingTranslations(Me.MasterPage.MessageController)
                Me.State.PageIndex = 0
                Me.TranslateGridHeader(moDataGrid)
                Me.TranslateGridControls(moDataGrid)
                BaseSetButtonsState(False)
                PopulateReadOnly()
                PopulateGrid()
                cboPageSize.SelectedValue = Me.State.selectedPageSize.ToString()
            Else
                CheckIfComingFromSaveConfirm()
            End If
        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Dim retType As New DealerFileProcessedController_New.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.DealerfileProcessedId)
                    Me.ReturnToCallingPage(retType)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnSave_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click
            Try
                SavePage()
                Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Me.HiddenIsPageDirty.Value = EMPTY
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub btnUndo_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
            Try
                PopulateGrid()
                Me.HiddenIsPageDirty.Value = EMPTY
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Handlers-Grid"

        Private Sub moDataGrid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moDataGrid.PageIndexChanging
            Try
                Me.State.selectedPageIndex = e.NewPageIndex
                If IsDataGPageDirty() Then
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                Else
                    Me.moDataGrid.PageIndex = e.NewPageIndex
                    PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub moDataGrid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                If IsDataGPageDirty() Then
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                    DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                Else
                    'moDataGrid.PageIndex = NewPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                    Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                    Me.PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                Dim nIndex As Integer = e.Item.ItemIndex
                If (e.CommandName = SORT_COMMAND_NAME) Then
                    Me.State.sortBy = CType(e.CommandArgument, String)
                    If IsDataGPageDirty() Then
                        Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridColSort
                        DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                    Else
                        PopulateGrid()
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles moDataGrid.Sorting
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
        Private Sub ItemBound(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles moDataGrid.RowDataBound
            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Dim oDateOfPayText As TextBox
            Dim oExtWarrSaleDateText As TextBox
            Dim oTextBox As TextBox

            If (itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem) AndAlso e.Row.RowIndex <> -1 Then
                '   Display Only
                With e.Row
                    Me.PopulateControlFromBOProperty(.Cells(Me.ID_COL), dvRow(DealerPmtReconWrkDAL.COL_NAME_DEALER_PMT_RECON_WRK_ID))
                    'oTextBox = CType(e.Row.Cells(RECORD_TYPE_COL).FindControl("moRecordTypeTextGrid"), TextBox)
                    'oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerPmtReconWrkDAL.COL_NAME_RECORD_TYPE))
                    Dim oDrop As DropDownList = CType(e.Row.FindControl("moRecordTypeDrop"), DropDownList)
                    oDrop.Attributes.Add("onchange", "setDirty()")
                    PopulateRecordTypeDrop(oDrop)
                    PopulateRecordTypeDrop(oDrop)
                    Dim oValue As String = CType(dvRow(DealerPmtReconWrkDAL.COL_NAME_RECORD_TYPE), String)
                    Me.SetSelectedItemByText(oDrop, oValue)

                    oTextBox = CType(e.Row.Cells(REJECT_REASON_COL).FindControl("RejectReasonTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    'Check if message requires parameterized conversion in specific language. if required translate message accordingly : REQ-1264
                    Dim strMsg As String = GetSpecificRejectionReason(dvRow)
                    If (strMsg <> String.Empty) Then
                        dvRow(DealerPmtReconWrkDAL.COL_NAME_REJECT_REASON) = strMsg
                    End If
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerPmtReconWrkDAL.COL_NAME_REJECT_REASON))

                    oTextBox = CType(e.Row.Cells(DEALER_COL).FindControl("moDealerTextGrid"), TextBox)
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerPmtReconWrkDAL.COL_NAME_DEALER))

                    oTextBox = CType(e.Row.Cells(CERTIFICATE_COL).FindControl("moCertificateTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerPmtReconWrkDAL.COL_NAME_CERTIFICATE))
                    oTextBox = CType(e.Row.Cells(SERIAL_NUMBER_COL).FindControl("moSerialNumTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerPmtReconWrkDAL.COL_NAME_SERIAL_NUMBER))
                    oTextBox = CType(e.Row.Cells(PAYMENT_AMOUNT_COL).FindControl("moPaymentAmountTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerPmtReconWrkDAL.COL_NAME_PAYMENT_AMOUNT), "N5")
                    oDateOfPayText = CType(e.Row.Cells(DATE_OF_PAY_COL).FindControl("moDateOfPayTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oDateOfPayText, dvRow(DealerPmtReconWrkDAL.COL_NAME_DATE_OF_PAYMENT))
                    oExtWarrSaleDateText = CType(e.Row.Cells(DATE_PAID_FOR_COL).FindControl("moDatePaidForTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oExtWarrSaleDateText, dvRow(DealerPmtReconWrkDAL.COL_NAME_DATE_PAID_FOR))
                    oTextBox = CType(e.Row.Cells(CAMPAIGN_NUMBER_COL).FindControl("moCampaignNumTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerPmtReconWrkDAL.COL_NAME_CAMPAIGN_NUMBER))
                    oTextBox = CType(e.Row.Cells(NEW_PRODUCT_CODE_COL).FindControl("moNewProdCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerPmtReconWrkDAL.COL_NAME_NEW_PRODUCT_CODE))
                    oTextBox = CType(e.Row.Cells(PRODUCT_CODE_COL).FindControl("moProductCodeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerPmtReconWrkDAL.COL_NAME_PRODUCT_CODE))
                    oTextBox = CType(e.Row.Cells(MEMBERSHIP_NUMBER_COL).FindControl("moMembershipNumTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerPmtReconWrkDAL.COL_NAME_MEMBESHIP_NUMBER))
                    oTextBox = CType(e.Row.Cells(PAYMENT_INVOICE_NUMBER_COL).FindControl("moPaymentInvoiceNumTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerPmtReconWrkDAL.COL_NAME_PAYMENT_INVOICE_NUMBER))
                    oTextBox = CType(e.Row.Cells(COLLECTED_AMOUNT_COL).FindControl("moCollectedAmountTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerPmtReconWrkDAL.COL_NAME_COLLECTED_AMOUNT), "N5")
                    oTextBox = CType(e.Row.Cells(SERVICE_LINE_NUMBER_COL).FindControl("moServiceLineNumTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerPmtReconWrkDAL.COL_NAME_SERVICE_LINE_NUMBER))
                    oTextBox = CType(e.Row.Cells(ADJUSTMENT_AMOUNT_COL).FindControl("moAdjustmentAmountTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerPmtReconWrkDAL.COL_NAME_ADJUSTMENT_AMOUNT), "N5")

                    oTextBox = CType(e.Row.Cells(INSTALLMENT_NUM_COL).FindControl("moInstallmentNumTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerPmtReconWrkDAL.COL_NAME_INSTALLMENT_NUM))

                    oTextBox = CType(e.Row.Cells(FEE_INCOME_COL).FindControl("moFeeIncomeTextGrid"), TextBox)
                    oTextBox.Attributes.Add("onchange", "setDirty()")
                    Me.PopulateControlFromBOProperty(oTextBox, dvRow(DealerPmtReconWrkDAL.COL_NAME_FEE_INCOME))

                End With
                '      ElseIf itemType = ListItemType.EditItem Then
                ' Edit Only
                Dim oDateOfPayImage As ImageButton = CType(e.Row.Cells(DATE_OF_PAY_COL).FindControl("moDateOfPayImageGrid"), ImageButton)

                Dim oDatePaidForImage As ImageButton = CType(e.Row.Cells(DATE_PAID_FOR_COL).FindControl("moDatePaidForImageGrid"), ImageButton)

                If (Not oDateOfPayImage Is Nothing) Then
                    Me.AddCalendar(oDateOfPayImage, oDateOfPayText)
                End If
                If (Not oDatePaidForImage Is Nothing) Then
                    Me.AddCalendar(oDatePaidForImage, oExtWarrSaleDateText)
                End If
            End If
            BaseItemBound(source, e)
        End Sub

        Protected Function GetSpecificRejectionReason(ByVal dvRow As DataRowView) As String
            Dim dr As DataRow
            Dim strMsg As String
            Dim intParamCnt As Integer
            Dim strParamList As String

            If Not dvRow Is Nothing Then
                dr = dvRow.Row
                strMsg = dr(DealerPmtReconWrkDAL.COL_NAME_TRANSLATED_MSG).ToString.Trim
                If (strMsg <> String.Empty) Then
                    If Not dr(DealerPmtReconWrkDAL.COL_NAME_PARAMETER_COUNT) Is DBNull.Value Then
                        Integer.TryParse(dr(DealerPmtReconWrkDAL.COL_NAME_PARAMETER_COUNT).ToString(), intParamCnt)

                        If (intParamCnt > 0) Then
                            If Not dr(DealerPmtReconWrkDAL.COL_NAME_REJECT_MSG_PARAMS) Is DBNull.Value Then
                                strParamList = dr(DealerPmtReconWrkDAL.COL_NAME_REJECT_MSG_PARAMS).ToString.Trim
                            End If
                            strMsg = TranslationBase.TranslateParameterizedMsg(strMsg, intParamCnt, strParamList).Trim
                        End If
                    End If
                End If
            End If

            Return strMsg

        End Function


        Protected Sub ItemCreated(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Protected Sub BindBoPropertiesToGridHeaders(ByVal dealerReconWrkInfo As DealerPmtReconWrk)
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, RECORD_TYPE_PROPERTY, Me.moDataGrid.Columns(Me.RECORD_TYPE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, REJECT_REASON_PROPERTY, Me.moDataGrid.Columns(Me.REJECT_REASON_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, DEALER_PROPERTY, Me.moDataGrid.Columns(Me.DEALER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, CERTIFICATE_PROPERTY, Me.moDataGrid.Columns(Me.CERTIFICATE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, SERIAL_NUMBER_PROPERTY, Me.moDataGrid.Columns(Me.SERIAL_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, PAYMENT_AMOUNT_PROPERTY, Me.moDataGrid.Columns(Me.PAYMENT_AMOUNT_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, DATE_OF_PAY_PROPERTY, Me.moDataGrid.Columns(Me.DATE_OF_PAY_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, DATE_PAID_FOR_PROPERTY, Me.moDataGrid.Columns(Me.DATE_PAID_FOR_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, CAMPAIGN_NUMBER_PROPERTY, Me.moDataGrid.Columns(Me.CAMPAIGN_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, MEMBERSHIP_NUMBER_PROPERTY, Me.moDataGrid.Columns(Me.MEMBERSHIP_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, PAYMENT_INVOICE_NUMBER_PROPERTY, Me.moDataGrid.Columns(Me.PAYMENT_INVOICE_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, COLLECTED_AMOUNT_PROPERTY, Me.moDataGrid.Columns(Me.COLLECTED_AMOUNT_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, SERVICE_LINE_NUMBER_PROPERTY, Me.moDataGrid.Columns(Me.SERVICE_LINE_NUMBER_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, NEW_PRODUCT_CODE_PROPERTY, Me.moDataGrid.Columns(Me.NEW_PRODUCT_CODE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, PRODUCT_CODE_PROPERTY, Me.moDataGrid.Columns(Me.PRODUCT_CODE_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, ADJUSTEMNT_AMOUNT_PROPERTY, Me.moDataGrid.Columns(Me.ADJUSTMENT_AMOUNT_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, INSTALLMENT_NUMBER_PROPERTY, Me.moDataGrid.Columns(Me.INSTALLMENT_NUM_COL))
            Me.BindBOPropertyToGridHeader(dealerReconWrkInfo, FEE_INCOME_PROPERTY, Me.moDataGrid.Columns(Me.FEE_INCOME_COL))
            Me.ClearGridViewHeadersAndLabelsErrSign()
        End Sub

#End Region

#End Region

#Region "Controlling Logic"
        Private Sub UpdateBreadCrum()

            Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage("DEALER_PAYMENT")
            Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("DEALER_PAYMENT")

        End Sub
        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSavePagePromptResponse.Value

            Try
                If Not confResponse.Equals(EMPTY) Then
                    If confResponse = Me.MSG_VALUE_YES Then
                        SavePage()
                    End If
                    Me.HiddenSavePagePromptResponse.Value = EMPTY
                    Me.HiddenIsPageDirty.Value = EMPTY

                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Dim retType As New DealerFileProcessedController_New.ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.DealerfileProcessedId)
                            Me.ReturnToCallingPage(retType)
                        Case ElitaPlusPage.DetailPageCommand.GridPageSize
                            Me.moDataGrid.PageIndex = NewCurrentPageIndex(moDataGrid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                            Me.State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
                        Case Else
                            Me.moDataGrid.PageIndex = Me.State.selectedPageIndex
                    End Select
                    PopulateGrid()
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Function CreateBoFromGrid(ByVal index As Integer) As DealerPmtReconWrk
            Dim DealerReconWrkId As Guid
            Dim dealerReconWrkInfo As DealerPmtReconWrk
            Dim sModifiedDate As String

            moDataGrid.SelectedIndex = index
            DealerReconWrkId = New Guid(moDataGrid.Rows(index).Cells(Me.ID_COL).Text)
            sModifiedDate = Me.GetGridText(moDataGrid, index, Me.MODIFIED_DATE_COL)
            dealerReconWrkInfo = New DealerPmtReconWrk(DealerReconWrkId, sModifiedDate)
            Return dealerReconWrkInfo
        End Function

        Private Sub SavePage()
            Dim index As Integer = 0
            Dim dealerReconWrkInfo As DealerPmtReconWrk
            Dim totItems As Integer = Me.moDataGrid.Rows.Count

            If totItems > 0 Then
                dealerReconWrkInfo = CreateBoFromGrid(0)
                BindBoPropertiesToGridHeaders(dealerReconWrkInfo)
                PopulateBOFromForm(dealerReconWrkInfo)
                dealerReconWrkInfo.Save()
            End If

            totItems = totItems - 1
            For index = 1 To totItems
                dealerReconWrkInfo = CreateBoFromGrid(index)
                BindBoPropertiesToGridHeaders(dealerReconWrkInfo)
                PopulateBOFromForm(dealerReconWrkInfo)
                dealerReconWrkInfo.Save()
            Next
        End Sub

        Function IsDataGPageDirty() As Boolean
            Dim Result As String = Me.HiddenIsPageDirty.Value

            Return Result.Equals("YES")
        End Function

#End Region

#Region "Button-Management"

        Public Overrides Sub BaseSetButtonsState(ByVal bIsEdit As Boolean)
            SetButtonsState(bIsEdit)
        End Sub

        Private Sub SetButtonsState(ByVal bIsEdit As Boolean)
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
                Dim oDealerFile As DealerFileProcessed = New DealerFileProcessed(Me.State.DealerfileProcessedId)
                With oDealerFile
                    If Me.State.ParentFile = "N" Then
                        moDealerNameText.Text = .DealerNameLoad
                    Else
                        moDealerNameText.Text = EMPTY
                    End If

                    moFileNameText.Text = .Filename
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGrid(Optional ByVal oAction As String = ACTION_NONE)

            Dim dv As DataView
            Dim recCount As Integer = 0

            Try
                dv = GetDV()
                'dv.Sort = Me.State.sortBy
                If Not Me.SortDirection.Equals(EMPTY) Then
                    dv.Sort = Me.SortDirection
                    HighLightSortColumn(moDataGrid, Me.SortDirection)
                End If
                recCount = dv.Count
                Session("recCount") = recCount
                Me.moDataGrid.PageSize = Me.State.selectedPageSize
                Me.moDataGrid.DataSource = dv.ToTable
                moDataGrid.DataBind()
                Me.lblRecordCount.Text = recCount & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

                If Not Me.State.RecordMode Is Nothing AndAlso Me.State.RecordMode = "REJ" AndAlso Me.State.RejRecNotUpdatable = "Y" Then
                    ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid, True)
                Else
                    ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, moDataGrid)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Function GetDV() As DataView

            Dim dv As DataView

            dv = DealerPmtReconWrk.LoadList(Me.State.DealerfileProcessedId, Me.State.RecordMode, Me.State.ParentFile)
            dv.Sort = moDataGrid.DataMember()

            Return (dv)

        End Function

        Private Sub PopulateBOItem(ByVal dealerReconWrkInfo As DealerPmtReconWrk, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            Me.PopulateBOProperty(dealerReconWrkInfo, oPropertyName,
                                            CType(Me.GetSelectedGridControl(moDataGrid, oCellPosition), TextBox))
        End Sub

        Private Sub PopulateBODrop(ByVal dealerReconWrkInfo As DealerPmtReconWrk, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
            Me.PopulateBOProperty(dealerReconWrkInfo, oPropertyName,
                                CType(Me.GetSelectedGridControl(moDataGrid, oCellPosition), DropDownList), False)
        End Sub

        Private Sub PopulateBOFromForm(ByVal dealerReconWrkInfo As DealerPmtReconWrk)
            PopulateBODrop(dealerReconWrkInfo, RECORD_TYPE_PROPERTY, RECORD_TYPE_COL)
            PopulateBOItem(dealerReconWrkInfo, REJECT_REASON_PROPERTY, REJECT_REASON_COL)
            PopulateBOItem(dealerReconWrkInfo, CERTIFICATE_PROPERTY, CERTIFICATE_COL)
            PopulateBOItem(dealerReconWrkInfo, SERIAL_NUMBER_PROPERTY, SERIAL_NUMBER_COL)
            PopulateBOItem(dealerReconWrkInfo, PAYMENT_AMOUNT_PROPERTY, PAYMENT_AMOUNT_COL)
            PopulateBOItem(dealerReconWrkInfo, DATE_OF_PAY_PROPERTY, DATE_OF_PAY_COL)
            PopulateBOItem(dealerReconWrkInfo, DATE_PAID_FOR_PROPERTY, DATE_PAID_FOR_COL)
            PopulateBOItem(dealerReconWrkInfo, CAMPAIGN_NUMBER_PROPERTY, CAMPAIGN_NUMBER_COL)
            PopulateBOItem(dealerReconWrkInfo, MEMBERSHIP_NUMBER_PROPERTY, MEMBERSHIP_NUMBER_COL)
            PopulateBOItem(dealerReconWrkInfo, PAYMENT_INVOICE_NUMBER_PROPERTY, PAYMENT_INVOICE_NUMBER_COL)
            PopulateBOItem(dealerReconWrkInfo, COLLECTED_AMOUNT_PROPERTY, COLLECTED_AMOUNT_COL)
            PopulateBOItem(dealerReconWrkInfo, SERVICE_LINE_NUMBER_PROPERTY, SERVICE_LINE_NUMBER_COL)
            PopulateBOItem(dealerReconWrkInfo, NEW_PRODUCT_CODE_PROPERTY, NEW_PRODUCT_CODE_COL)
            PopulateBOItem(dealerReconWrkInfo, PRODUCT_CODE_PROPERTY, PRODUCT_CODE_COL)
            PopulateBOItem(dealerReconWrkInfo, ADJUSTEMNT_AMOUNT_PROPERTY, ADJUSTMENT_AMOUNT_COL)
            PopulateBOItem(dealerReconWrkInfo, INSTALLMENT_NUMBER_PROPERTY, INSTALLMENT_NUM_COL)
            PopulateBOItem(dealerReconWrkInfo, FEE_INCOME_PROPERTY, FEE_INCOME_COL)
            If Me.ErrCollection.Count > 0 Then
                Throw New PopulateBOErrorException
            End If
        End Sub

        Private Sub PopulateFormItem(ByVal oCellPosition As Integer, ByVal oPropertyValue As Object)
            Me.PopulateControlFromBOProperty(Me.GetSelectedGridControl(moDataGrid, oCellPosition), oPropertyValue)
        End Sub

        Sub PopulateRecordTypeDrop(ByVal recordTypeDrop As DropDownList, Optional ByVal AddNothingSelected As Boolean = False)
            Try
                Dim oLangId As Guid = Authentication.LangId
                ' Dim recordTypeList As DataView = LookupListNew.GetPaymentRecordTypeLookupList(oLangId)
                ' Me.BindListControlToDataView(recordTypeDrop, recordTypeList, , , AddNothingSelected) 'PYMTRECTYP
                recordTypeDrop.Populate(CommonConfigManager.Current.ListManager.GetList("PYMTRECTYP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                 {
                  .AddBlankItem = AddNothingSelected
                 })
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region



    End Class

End Namespace
