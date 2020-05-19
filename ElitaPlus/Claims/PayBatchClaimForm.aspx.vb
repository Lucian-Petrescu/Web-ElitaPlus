Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Microsoft.VisualBasic

Partial Class PayBatchClaimForm
    Inherits ElitaPlusSearchPage

#Region "Web Form Designer Generated Code"

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region "CONSTANTS"

    Public Const URL As String = "~/Claims/PayBatchClaimForm.aspx"

    Public Const GRID_CLAIM_COL_INVOICE_TRANS_NUMBER_IDX As Integer = 0
    Public Const GRID_CLAIM_COL_INVOICE_TRANS_DETAIL_IDX As Integer = 1
    Public Const GRID_CLAIM_COL_CLAIM_ID_IDX As Integer = 2
    Public Const GRID_CLAIM_COL_CLAIM_NUMBER_IDX As Integer = 3
    Public Const GRID_CLAIM_COL_AUTHORIZATION_NUMBER_IDX As Integer = 4
    Public Const GRID_CLAIM_COL_CUSTOMER_NAME_IDX As Integer = 5
    Public Const GRID_CLAIM_COL_CLAIM_MODIFIED_DATE_IDX As Integer = 6
    Public Const GRID_CLAIM_COL_RESERVE_AMOUNT_IDX As Integer = 7
    Public Const GRID_CLAIM_COL_TOTAL_BONUS_IDX As Integer = 8
    Public Const GRID_CLAIM_COL_SALVAGE_AMOUNT_IDX As Integer = 9
    Public Const GRID_CLAIM_COL_DEDUCTIBLE_IDX As Integer = 10
    Public Const GRID_CLAIM_COL_PAYMENT_AMOUNT_IDX As Integer = 11
    Public Const GRID_CLAIM_COL_REPAIR_DATE_IDX As Integer = 12
    Public Const GRID_CLAIM_COL_PICKUP_DATE_IDX As Integer = 13
    Public Const GRID_CLAIM_COL_CLOSE_CLAIM_IDX As Integer = 14
    Public Const GRID_CLAIM_COL_SPARE_PARTS_IDX As Integer = 15
    Public Const GRID_CLAIM_COL_LOSS_DATE_IDX As Integer = 16

    Public Const GRID_CLAIM_CTL_CLOSE_CLAIM As String = "chkCloseClaim"
    Public Const GRID_CLAIM_CTL_SPARE_PARTS As String = "chkSpareParts"
    Public Const GRID_CLAIM_CTL_PAYMENT_AMOUNT As String = "textAmountToBePaid"
    Public Const GRID_CLAIM_CTL_REPAIR_DATE As String = "textRepairDate"
    Public Const GRID_CLAIM_CTL_PICKUP_DATE As String = "textPickupDate"
    Public Const GRID_CLAIM_CTL_RESERVE_AMOUNT As String = "labelReserveAmount"
    Public Const GRID_CLAIM_CTL_SALVAGE_AMOUNT As String = "textSalvageAmount"
    Public Const GRID_CLAIM_CTL_TOTAL_BONUS As String = "textTotalBonus"

    Public Const PAGETITLE As String = "PAY_INVOICE"
    Public Const PAGETAB As String = "CLAIM"


    Public Const DEFAULT_PAGE_SIZE As Integer = 50

    Public Const YES_VALUE As String = "Y"
    Public Const NO_VALUE As String = "N"
    Public Const CLAIM_STATUS_UPDATE As String = "U"
    Public Const CSS_ERROR_CLASS As String = "errRow"

    Private BOOL_PAY_CLAIMS As Boolean = False
    Private BOOL_VALID_DATES As Boolean = True
    Private CURRENT_PAGE As Integer = 0
    Private CURRENT_PAGE_SIZE As Integer = 10
    Private BOOL_ERR_ADDED As Boolean = False

    Private arrPickUp, arrRepair As ArrayList

    Private Const Invoice_Tax_Type As String = "Invoice_Tax"

    Public Const COL_TAX1_COMPUTE_METHOD As String = "tax1_compute_method"
    Public Const COL_TAX2_COMPUTE_METHOD As String = "tax2_compute_method"
    Public Const COL_TAX3_COMPUTE_METHOD As String = "tax3_compute_method"
    Public Const COL_TAX4_COMPUTE_METHOD As String = "tax4_compute_method"
    Public Const COL_TAX5_COMPUTE_METHOD As String = "tax5_compute_method"
    Public Const COL_TAX6_COMPUTE_METHOD As String = "tax6_compute_method"
    Public COMPUTE_TYPE_MANUALLY As String = "I"
    Public Const INVOICE_STATUS_INPROGRESS As String = "IP"
    Public Const INVOICE_STATUS_PAID As String = "P"
    Public Const INVOICE_STATUS_REJECTED As String = "R"

    Public Const MAX_MANUALLY_ENTERED_TAXES As Integer = 2

    Private _claim As Claim = Nothing

#End Region

#Region "Page State"

    Class MyState
        Public MyBO As InvoiceTrans
        Public PageIndex As Integer = 0
        Public SortExpression As String = InvoiceTrans.InvoiceTransSearchDV.COL_BATCH_STATUS
        Public selectedInvoiceTransId As Guid = Guid.Empty
        Public serviceCenterId As Guid
        Public IsGridVisible As Boolean = False
        Public searchInvoiceTransDetailDV As InvoiceTrans.InvoiceTransDetailDV = Nothing
        Public selectedSortById As Guid = Guid.Empty
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public SearchClicked As Boolean
        Public InvoiceTaxType As String
        Public countryid As Guid
        Public InvoiceTaxTypeID As Guid
        Public bnoRow As Boolean = False
        Public isBatchInPreInvoiceAndPending As Boolean = False
        Public companyBO As Company


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

    Public Property isInvoiceTaxType() As String

        Get
            Return Me.State.InvoiceTaxType
        End Get
        Set(ByVal Value As String)
            Me.State.InvoiceTaxType = Value
        End Set

    End Property

    Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
        Try
            If Not Me.CallingParameters Is Nothing Then
                Me.State.MyBO = New InvoiceTrans(New Guid(CType(Me.CallingParameters, String)))

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public InvoiceTransId As Guid
        Public Sub New(ByVal CurrentInvoiceTransId As Guid)
            Me.InvoiceTransId = CurrentInvoiceTransId
        End Sub
    End Class
#End Region

#Region "Populate Dropdown"
    Private Sub LoadRegionList()
        Dim companyid As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID
        Dim countryID As Guid
        Me.State.companyBO = New Company(companyid)
        countryID = Me.State.companyBO.BusinessCountryId
        Me.State.countryid = countryID
        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext
        oListContext.CountryId = countryID
        Dim oRegionList As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="RegionsByCountry", context:=oListContext)
        DropDownState.Populate(oRegionList, New PopulateOptions() With
                                           {
                                           .AddBlankItem = True
                                           })
    End Sub
#End Region

#Region "Controlling Logic"


    Protected Sub PopulateFormFromBOs()
        Dim RegionDesc As String
        With Me.State.MyBO

            Me.PopulateControlFromBOProperty(Me.TextBoxInvoiceAmount, .SvcControlAmount)
            Me.PopulateControlFromBOProperty(Me.TextBoxInvoiceNumber, .SvcControlNumber)
            Me.PopulateControlFromBOProperty(Me.TextBoxServiceCenter, .ServiceCenterName)
            Me.PopulateControlFromBOProperty(Me.TextBoxInvoiceDate, .InvoiceDate)
            Me.PopulateControlFromBOProperty(Me.txtInvTyp, .InvoiceTypeName)

            Dim dectax As Decimal = 0


            If Me.State.InvoiceTaxType = Invoice_Tax_Type Then
                Me.PopulateControlFromBOProperty(Me.TextBoxBatchNumber, .BatchNumber)
                If Not .RegionId.Equals(Guid.Empty) Then
                    Me.SetSelectedItem(Me.DropDownState, .RegionId)
                Else
                    Dim sc As ServiceCenter = New ServiceCenter(Me.State.MyBO.ServiceCenterId)
                    Dim payeeAddress As Address = New Address(sc.AddressId)
                    If Not payeeAddress.RegionId.Equals(Guid.Empty) Then
                        Me.SetSelectedItem(Me.DropDownState, payeeAddress.RegionId)
                    End If
                End If

                Me.PopulateControlFromBOProperty(Me.TextBoxPerceptionIVA, .Tax1Amount)
                Me.PopulateControlFromBOProperty(Me.TextBoxPerceptionIIBB, .Tax2Amount)
                TextBoxPerceptionIVA.Text = CType((IIf(.Tax1Amount = 0, String.Empty, .Tax1Amount)), String)
                TextBoxPerceptionIIBB.Text = CType((IIf(.Tax2Amount = 0, String.Empty, .Tax2Amount)), String)

                TextBoxPerceptionIVA.Attributes("onchange") = "chkamt('" + TextBoxPerceptionIVA.ClientID + "'," + (Decimal.Parse(.SvcControlAmount.ToString) * 100).ToString + " );"
                TextBoxPerceptionIVA.Attributes("onfocus") = "setCur('" + TextBoxPerceptionIVA.ClientID + "');"

                TextBoxPerceptionIIBB.Attributes("onchange") = "chkamt('" + TextBoxPerceptionIIBB.ClientID + "'," + (Decimal.Parse(.SvcControlAmount.ToString) * 100).ToString + " );"
                TextBoxPerceptionIIBB.Attributes("onfocus") = "setCur('" + TextBoxPerceptionIIBB.ClientID + "');"

            End If
        End With

    End Sub

    Protected Sub PopulateBOsFromForm()
        With Me.State.MyBO
            Me.PopulateBOProperty(Me.State.MyBO, "SvcControlAmount", Me.TextBoxInvoiceAmount)
            Me.PopulateBOProperty(Me.State.MyBO, "SvcControlNumber", Me.TextBoxInvoiceNumber)
            Me.PopulateBOProperty(Me.State.MyBO, "ServiceCenterName", Me.TextBoxServiceCenter)
            Me.PopulateBOProperty(Me.State.MyBO, "InvoiceTypeName", Me.txtInvTyp)
            'following logic is only when its Perception Tax
            If Not Me.State.InvoiceTaxType Is Nothing Then

                Me.PopulateBOProperty(Me.State.MyBO, "Tax1Amount", Me.TextBoxPerceptionIVA)
                Me.PopulateBOProperty(Me.State.MyBO, "Tax2Amount", Me.TextBoxPerceptionIIBB)
                Me.PopulateBOProperty(Me.State.MyBO, "BatchNumber", Me.TextBoxBatchNumber)
                Me.PopulateBOProperty(Me.State.MyBO, "RegionId", Me.DropDownState)

            End If

            If Not Me.inpCurrentAmt.Value.Trim.ToString = String.Empty Then
                Me.TextBoxCurrentAmount.Text = inpCurrentAmt.Value
            End If
            If Not Me.inpDifference.Value.Trim.ToString = String.Empty Then
                Me.TextBoxDifference.Text = inpDifference.Value
            End If

        End With
        If Me.ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Protected Sub CheckGetBatchClosedClaims()
        Dim str As String
        If Me.TextBoxDifference.Text <> "0.00" Then
            str = InvoiceTrans.GetBatchClosedClaims(Me.State.MyBO.ServiceCenterId, Me.State.MyBO.BatchNumber, Me.State.MyBO.Id)
            If Not String.IsNullOrEmpty(str) Then
                Dim strErrorMess As String = TranslationBase.TranslateLabelOrMessage(Assurant.ElitaPlus.Common.ErrorCodes.GUI_BATCH_CLAIMS_ALREADY_CLOSED_ERR)
                Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.GUI_BATCH_CLAIMS_ALREADY_CLOSED_ERR, strErrorMess & " " & str)
            End If
        End If
    End Sub
    Protected Sub ChkInvoiceTaxTypeAndValidate()

        Dim ds As DataSet
        Dim dr As DataRow
        Dim dc As DataColumn
        Dim invoiceTrans As New InvoiceTrans
        Dim taxtypeID As Guid
        Dim boolErr As Boolean = False
        Dim taxcolumnscnt As Integer = 0

        ' taxtype code - 4 = Manual...
        taxtypeID = LookupListNew.GetIdFromCode(LookupListNew.LK_TAX_TYPES, "4")
        ds = invoiceTrans.CheckInvoiceTaxType(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID, taxtypeID, Guid.Empty)

        If ds.Tables(0).Rows.Count > 0 Then
            For Each dr In ds.Tables(0).Rows

                taxcolumnscnt = CInt(IIf(dr(COL_TAX1_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxcolumnscnt + 1, taxcolumnscnt + 0))
                taxcolumnscnt = CInt(IIf(dr(COL_TAX2_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxcolumnscnt + 1, taxcolumnscnt + 0))
                taxcolumnscnt = CInt(IIf(dr(COL_TAX3_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxcolumnscnt + 1, taxcolumnscnt + 0))
                taxcolumnscnt = CInt(IIf(dr(COL_TAX4_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxcolumnscnt + 1, taxcolumnscnt + 0))
                taxcolumnscnt = CInt(IIf(dr(COL_TAX5_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxcolumnscnt + 1, taxcolumnscnt + 0))
                taxcolumnscnt = CInt(IIf(dr(COL_TAX6_COMPUTE_METHOD).ToString = COMPUTE_TYPE_MANUALLY, taxcolumnscnt + 1, taxcolumnscnt + 0))

                If taxcolumnscnt <> MAX_MANUALLY_ENTERED_TAXES Then
                    boolErr = True
                    Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.ERR_TWO_TAXES_FOR_INVOICE_TAX_TYPE, True)
                    Me.State.InvoiceTaxType = Invoice_Tax_Type

                    ControlMgr.SetVisibleControl(Me, Me.btnPay_WRITE, False)
                    ControlMgr.SetVisibleControl(Me, Me.btnSave_WRITE, False)
                End If

                'Means Error Exists
                If boolErr Then
                    Me.MasterPage.MessageController.Show()
                    Exit Sub
                End If

                Me.State.InvoiceTaxType = Invoice_Tax_Type
                Me.State.InvoiceTaxTypeID = taxtypeID

            Next
        Else

        End If
    End Sub
    Protected Sub EnableOrDisableControls()

        If Me.State.InvoiceTaxType = Invoice_Tax_Type Then

            ControlMgr.SetVisibleControl(Me, Me.LabelPerceptionIVA, True)
            ControlMgr.SetVisibleControl(Me, Me.TextBoxPerceptionIVA, True)
            ControlMgr.SetEnableControl(Me, Me.TextBoxPerceptionIVA, True)
            ControlMgr.SetVisibleControl(Me, Me.LabelState, True)
            ControlMgr.SetVisibleControl(Me, Me.DropDownState, True)
            ControlMgr.SetEnableControl(Me, Me.DropDownState, True)
            ControlMgr.SetVisibleControl(Me, Me.LabelBatchNumber, True)
            ControlMgr.SetVisibleControl(Me, Me.TextBoxBatchNumber, True)
            ControlMgr.SetEnableControl(Me, Me.TextBoxBatchNumber, True)
            ControlMgr.SetVisibleControl(Me, Me.LabeLPerceptionIIBB, True)
            ControlMgr.SetVisibleControl(Me, Me.TextBoxPerceptionIIBB, True)
            ControlMgr.SetEnableControl(Me, Me.TextBoxPerceptionIIBB, True)

            ControlMgr.SetVisibleControl(Me, tdlblbatch, True)
            ControlMgr.SetVisibleControl(Me, tdlblstate, True)
            ControlMgr.SetVisibleControl(Me, tdlbltax1, True)
            ControlMgr.SetVisibleControl(Me, tdlbltax2, True)
            ControlMgr.SetVisibleControl(Me, tdtxtbatch, True)
            ControlMgr.SetVisibleControl(Me, tddpstate, True)
            ControlMgr.SetVisibleControl(Me, tdtxttax1, True)
            ControlMgr.SetVisibleControl(Me, tdtxttax2, True)
        Else

            ControlMgr.SetVisibleControl(Me, Me.LabelState, False)
            ControlMgr.SetVisibleControl(Me, Me.DropDownState, False)
            ControlMgr.SetEnableControl(Me, Me.DropDownState, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelBatchNumber, False)
            ControlMgr.SetVisibleControl(Me, Me.TextBoxBatchNumber, False)
            ControlMgr.SetEnableControl(Me, Me.TextBoxBatchNumber, False)
            ControlMgr.SetVisibleControl(Me, Me.LabelPerceptionIVA, False)
            ControlMgr.SetVisibleControl(Me, Me.TextBoxPerceptionIVA, False)
            ControlMgr.SetEnableControl(Me, Me.TextBoxPerceptionIVA, False)
            ControlMgr.SetVisibleControl(Me, Me.LabeLPerceptionIIBB, False)
            ControlMgr.SetVisibleControl(Me, Me.TextBoxPerceptionIIBB, False)
            ControlMgr.SetEnableControl(Me, Me.TextBoxPerceptionIIBB, False)

            ControlMgr.SetVisibleControl(Me, tdlblbatch, False)
            ControlMgr.SetVisibleControl(Me, tdlblstate, False)
            ControlMgr.SetVisibleControl(Me, tdlbltax1, False)
            ControlMgr.SetVisibleControl(Me, tdlbltax2, False)
            ControlMgr.SetVisibleControl(Me, tdtxtbatch, False)
            ControlMgr.SetVisibleControl(Me, tddpstate, False)
            ControlMgr.SetVisibleControl(Me, tdtxttax1, False)
            ControlMgr.SetVisibleControl(Me, tdtxttax2, False)

        End If

    End Sub


    Private Sub PopulateGrid()

        Dim dv As InvoiceTrans.InvoiceTransDetailDV
        Dim epsp As New ElitaPlusSearchPage

        If Me.State.searchInvoiceTransDetailDV Is Nothing Then
            dv = InvoiceTrans.GetInvoiceTransDetail(Me.State.MyBO.Id)
            Me.State.searchInvoiceTransDetailDV = dv
        Else
            dv = Me.State.searchInvoiceTransDetailDV
        End If

        If Not Me.BOOL_VALID_DATES Then
            GridClaims.PageIndex = Me.CURRENT_PAGE
            GridClaims.PageSize = Me.CURRENT_PAGE_SIZE
        End If

        'Me.GridClaims.DataSource = dv
        'Me.GridClaims.DataBind()

        If Me.BOOL_ERR_ADDED Then
            Me.MasterPage.MessageController.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.ANOTHER_USER_HAS_MODIFIED_THIS_CLAIM)
        End If

        Me.lblRecordCount.Text = dv.Count.ToString & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)

        If dv.Count > 0 Then

            If Me.GridClaims.Visible Then
                Me.lblRecordCount.Text = dv.Count.ToString & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                Me.State.bnoRow = False
            End If
            Me.GridClaims.DataSource = dv
            Me.GridClaims.AllowSorting = False
            Me.GridClaims.DataBind()
        Else
            If Me.GridClaims.Visible Then
                Me.State.bnoRow = True
                epsp.CreateHeaderForEmptyGrid(GridClaims, Me.State.SortExpression)
                Me.lblRecordCount.Text = dv.Count.ToString & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            End If
        End If

        If Not GridClaims.BottomPagerRow.Visible Then GridClaims.BottomPagerRow.Visible = True

        If dv.Count = 0 Then
            ControlMgr.SetVisibleControl(Me, Me.btnPay_WRITE, False)
            ControlMgr.SetVisibleControl(Me, Me.btnSave_WRITE, False)
        End If

    End Sub

    Public Sub PopulateCalculatedFields()

        Dim dt As DataTable
        Dim dr() As DataRow
        Dim total As Decimal = 0
        Dim dif As Decimal = 0
        Dim boolerr As Boolean = False

        dt = Me.State.searchInvoiceTransDetailDV.Table

        If dt.Rows.Count > 0 Then
            If Not dt.Rows(0)("TOTAL_REGION_PERCEPTION_IIBB") Is Nothing Then
                If Not IsDBNull(dt.Rows(0)("TOTAL_REGION_PERCEPTION_IIBB")) Then
                    Me.TextBoxPerceptionIIBB.Text = dt.Rows(0)("TOTAL_REGION_PERCEPTION_IIBB")
                End If
            End If
            total = CType(dt.Compute("sum(" + Me.State.searchInvoiceTransDetailDV.COL_Payment_Amount_Total + ")", ""), Decimal)
            'total = CType(dt.Compute("sum(" + Me.State.searchInvoiceTransDetailDV.COL_RESERVE_AMOUNT + ")-sum(" + Me.State.searchInvoiceTransDetailDV.COL_SALVAGE_AMOUNT + ")", ""), Decimal)
        End If

        If Me.State.InvoiceTaxType = Invoice_Tax_Type Then
            'total = CType(dt.Compute("sum(" + Me.State.searchInvoiceTransDetailDV.COL_PAYMENT_AMOUNT + ")", ""), Decimal)
            dif = CType(Me.State.MyBO.SvcControlAmount, Decimal) - CType(Me.State.MyBO.Tax1Amount, Decimal) _
                    - CType(Me.State.MyBO.Tax2Amount, Decimal) - total

            If Me.TextBoxPerceptionIVA.Text <> String.Empty Then
                Me.TextBoxPerceptionIVA.Text = IIf(CType(TextBoxPerceptionIVA.Text, Decimal) = 0, "0.00", CType(TextBoxPerceptionIVA.Text, Decimal).ToString("0.00")).ToString
            End If

            'If Me.TextBoxPerceptionIIBB.Text <> String.Empty Then
            '    Me.TextBoxPerceptionIIBB.Text = IIf(CType(TextBoxPerceptionIIBB.Text, Decimal) = 0, "0.00", CType(TextBoxPerceptionIIBB.Text, Decimal).ToString("0.00")).ToString
            'End If

        Else
            ' total = CType(dt.Compute("sum(" + Me.State.searchInvoiceTransDetailDV.COL_PAYMENT_AMOUNT + ")", ""), Decimal)
            dif = CType(Me.State.MyBO.SvcControlAmount, Decimal) - total

        End If

        Me.TextBoxCurrentAmount.Text = IIf(total = 0, "0.00", total.ToString("0.00")).ToString
        Me.TextBoxDifference.Text = IIf(dif = 0, "0.00", dif.ToString("0.00")).ToString

        ' Me.inpCurrentAmt.Value = IIf(total = 0, "0.00", total.ToString("0.00")).ToString
        ' Me.inpDifference.Value = IIf(dif = 0, "0.00", dif.ToString("0.00")).ToString

        If Math.Round(total, 2) <> 0 And Math.Round(dif, 2) = 0 Then
            'REQ-5565
            If (Me.State.isBatchInPreInvoiceAndPending) Then
                ' If PreInvoice is in Pending Status
                ControlMgr.SetEnableControl(Me, Me.btnPay_WRITE, False)
            Else
                ' If PreInvoice is in Approved Status
                ControlMgr.SetEnableControl(Me, Me.btnPay_WRITE, True)
            End If
        Else
            ControlMgr.SetEnableControl(Me, Me.btnPay_WRITE, False)
        End If

    End Sub

    Public Sub RegisterClientServerIds()

        Dim onloadScript As New System.Text.StringBuilder()
        onloadScript.Append("<script type='text/javascript'>")
        onloadScript.Append(Environment.NewLine)
        onloadScript.Append("var CurrentAmt = '" + TextBoxCurrentAmount.ClientID + "';")
        onloadScript.Append("var InvoiceAmt = '" + TextBoxInvoiceAmount.ClientID + "';")
        onloadScript.Append("var Tax1Amt= '" + TextBoxPerceptionIVA.ClientID + "';")
        onloadScript.Append("var Tax2Amt = '" + TextBoxPerceptionIIBB.ClientID + "';")
        onloadScript.Append("var Diffamt = '" + TextBoxDifference.ClientID + "';")
        onloadScript.Append("var btnpay = '" + btnPay_WRITE.ClientID + "';")
        onloadScript.Append("var inputCuurentAmt = '" + inpCurrentAmt.ClientID + "';")
        onloadScript.Append("var inputDifference = '" + inpDifference.ClientID + "';")


        onloadScript.Append(Environment.NewLine)
        onloadScript.Append("</script>")
        ' Register script with page
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "onLoadCall", onloadScript.ToString())

    End Sub

    Private Function SaveClaims() As Boolean

        Dim dgItem As GridViewRow
        Dim dsBCI As New DALObjects.BatchClaimInvoice
        Dim dsReturn As DataSet
        Dim dt As DataTable = dsBCI.INVOICE_TRANS_DETAIL
        Dim dr As DALObjects.BatchClaimInvoice.INVOICE_TRANS_DETAILRow
        Dim chkClose, chkSpareParts As CheckBox
        Dim txtPickup, txtRepair, txtPayment, txtSalvageAmt As TextBox
        Dim lossDate As Date
        Dim pmt As Decimal = 0
        Dim sal As Decimal = 0
        Dim bonusAmt As Decimal = 0
        Dim drCurrRow As DataRow
        Dim intRowNum As Integer
        Dim bool_RepairValid As Boolean = True, bool_RepairEntered As Boolean = True
        Dim bool_PickUpValid As Boolean = True
        Dim arraySalvage() As Decimal
        Dim dtVal As DateTime


        ' Figure out the current row number based on the page index
        If Me.CURRENT_PAGE = 0 Then
            intRowNum = 0
        Else
            intRowNum = Me.CURRENT_PAGE * Me.CURRENT_PAGE_SIZE
        End If

        Dim boolErr As Boolean = False

        If Me.State.InvoiceTaxType = Invoice_Tax_Type Then

            'Validate PerceptionIVA
            If Me.TextBoxPerceptionIVA.Text.Trim.Length > 0 Then

                If Not Microsoft.VisualBasic.IsNumeric(Me.TextBoxPerceptionIVA.Text) Then
                    Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR, True)
                    boolErr = True
                End If
            End If

            If Me.TextBoxPerceptionIIBB.Text.Trim.Length > 0 Then

                'Validate PerceptionIIBB
                If Not Microsoft.VisualBasic.IsNumeric(Me.TextBoxPerceptionIIBB.Text) Then
                    Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_AMOUNT_ENTERED_ERR, True)
                    boolErr = True
                End If

                If CType(Me.TextBoxPerceptionIIBB.Text, Decimal) > CType(0, Decimal) Then
                    'Region
                    If (DropDownState.SelectedIndex = -1 Or DropDownState.SelectedIndex = 0) Then
                        Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_REGION_CODE, True)
                        If Not Me.inpCurrentAmt.Value.Trim.ToString = String.Empty Then
                            Me.TextBoxCurrentAmount.Text = inpCurrentAmt.Value
                        End If
                        If Not Me.inpDifference.Value.Trim.ToString = String.Empty Then
                            Me.TextBoxDifference.Text = inpDifference.Value
                        End If
                        boolErr = True
                    End If
                End If

            End If

        End If

        'Means Error Exists
        If boolErr Then
            Me.MasterPage.MessageController.Show()
            Exit Function
        End If

        PopulateBOsFromForm()

        Array.Resize(arraySalvage, Me.State.searchInvoiceTransDetailDV.Count)

        'Loop through the visible items in the grid and update the claim record based on the values entered.
        'If checked, create a new row record in the datatable and then pass to add the items to the set
        For Each dgItem In GridClaims.Rows
            'Find the current row we are working on in the dataset that we have (in case we fail validation and need to rebind)
            If (intRowNum + dgItem.RowIndex) < Me.State.searchInvoiceTransDetailDV.Count Then
                drCurrRow = Me.State.searchInvoiceTransDetailDV.Table.Rows(dgItem.RowIndex + intRowNum)
            Else
                Exit For
            End If
            'set the loss date
            lossDate = DateHelper.GetDateValue(dgItem.Cells(Me.GRID_CLAIM_COL_LOSS_DATE_IDX).Text)

            dr = dsBCI.INVOICE_TRANS_DETAIL.NewINVOICE_TRANS_DETAILRow
            dr.ACTION = Me.CLAIM_STATUS_UPDATE
            dr.INVOICE_TRANS_DETAIL_ID = GuidControl.GuidToHexString(New Guid(dgItem.Cells(Me.GRID_CLAIM_COL_INVOICE_TRANS_DETAIL_IDX).Text))
            dr.CLAIM_ID = GuidControl.GuidToHexString(New Guid(dgItem.Cells(Me.GRID_CLAIM_COL_CLAIM_ID_IDX).Text))
            dr.CLAIM_MODIFIED_DATE = DateHelper.GetDateValue(dgItem.Cells(Me.GRID_CLAIM_COL_CLAIM_MODIFIED_DATE_IDX).Text)

            dr.INVOICE_TRANS_ID = GuidControl.GuidToHexString(Me.State.MyBO.Id)
            dr.USER_ID = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)

            bonusAmt = Decimal.Parse(dgItem.Cells(Me.GRID_CLAIM_COL_TOTAL_BONUS_IDX).Text)

            txtPayment = CType(dgItem.Cells(Me.GRID_CLAIM_COL_PAYMENT_AMOUNT_IDX).FindControl(Me.GRID_CLAIM_CTL_PAYMENT_AMOUNT), TextBox)
            If Not txtPayment Is Nothing Then
                If IsNumeric(txtPayment.Text) Then
                    pmt = CType(txtPayment.Text, Decimal)
                End If

            End If

            ' REQ-5578, take the bonus off the payment amount and save separately
            dr.TOTAL_BONUS = bonusAmt
            dr.PAYMENT_AMOUNT_TOTAL = pmt
            dr.PAYMENT_AMOUNT = pmt - bonusAmt
            drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_PAYMENT_AMOUNT) = dr.PAYMENT_AMOUNT
            drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_Payment_Amount_Total) = dr.PAYMENT_AMOUNT_TOTAL

            txtSalvageAmt = CType(dgItem.Cells(Me.GRID_CLAIM_COL_SALVAGE_AMOUNT_IDX).FindControl(Me.GRID_CLAIM_CTL_SALVAGE_AMOUNT), TextBox)
            If Not txtSalvageAmt Is Nothing Then
                If IsNumeric(txtSalvageAmt.Text) Then sal = CType(txtSalvageAmt.Text, Decimal)
            End If
            dr.SALVAGE_AMOUNT = sal
            drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_SALVAGE_AMOUNT) = dr.SALVAGE_AMOUNT
            'arraySalvage(dgItem.RowIndex + intRowNum) = sal


            txtRepair = CType(dgItem.Cells(Me.GRID_CLAIM_COL_REPAIR_DATE_IDX).FindControl(Me.GRID_CLAIM_CTL_REPAIR_DATE), TextBox)
            If Not txtRepair Is Nothing Then
                If txtRepair.Text.Trim = String.Empty Then
                    'BOOL_VALID_DATES = False
                    bool_RepairEntered = False
                    drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_REPAIR_DATE) = DBNull.Value
                Else
                    If DateHelper.IsDate(txtRepair.Text) Then
                        dr.REPAIR_DATE = DateHelper.GetDateValue(txtRepair.Text)
                        drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_REPAIR_DATE) = dr.REPAIR_DATE
                        If dr.REPAIR_DATE < lossDate Or dr.REPAIR_DATE > Today Then
                            If Not drCurrRow Is Nothing Then
                                Me.BOOL_VALID_DATES = False
                                bool_RepairValid = False
                                If arrRepair Is Nothing Then arrRepair = New ArrayList
                                arrRepair.Add(dgItem.RowIndex)
                            End If
                        End If
                    Else
                        Me.BOOL_VALID_DATES = False
                        Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR, True)
                        drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_REPAIR_DATE) = DBNull.Value
                    End If
                End If
            End If

            txtPickup = CType(dgItem.Cells(Me.GRID_CLAIM_COL_PICKUP_DATE_IDX).FindControl(Me.GRID_CLAIM_CTL_PICKUP_DATE), TextBox)
            If Not txtPickup Is Nothing Then
                If DateHelper.IsDate(txtPickup.Text) Then
                    dr.PICKUP_DATE = DateHelper.GetDateValue(txtPickup.Text)
                    drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_PICKUP_DATE) = dr.PICKUP_DATE
                    If (Not DateHelper.IsDate(txtRepair.Text)) OrElse (dr.PICKUP_DATE < dr.REPAIR_DATE Or dr.PICKUP_DATE > Today) Then
                        If Not drCurrRow Is Nothing Then
                            Me.BOOL_VALID_DATES = False
                            bool_PickUpValid = False
                            If arrPickUp Is Nothing Then arrPickUp = New ArrayList
                            arrPickUp.Add(dgItem.RowIndex)
                        End If
                    End If
                Else
                    If txtPickup.Text.Trim <> String.Empty Then
                        Me.BOOL_VALID_DATES = False
                        Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR, True)
                    End If
                    drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_PICKUP_DATE) = DBNull.Value
                End If
            End If

            If Decimal.Parse(dgItem.Cells(Me.GRID_CLAIM_COL_RESERVE_AMOUNT_IDX).Text) = pmt Then
                dr.CLOSE_CLAIM = Me.YES_VALUE
            Else
                chkClose = CType(dgItem.Cells(Me.GRID_CLAIM_COL_CLOSE_CLAIM_IDX).FindControl(Me.GRID_CLAIM_CTL_CLOSE_CLAIM), CheckBox)
                If Not chkClose Is Nothing Then
                    If chkClose.Checked Then dr.CLOSE_CLAIM = Me.YES_VALUE Else dr.CLOSE_CLAIM = Me.NO_VALUE
                Else
                    dr.CLOSE_CLAIM = Me.NO_VALUE
                End If
            End If
            drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_CLOSE_CLAIM) = dr.CLOSE_CLAIM

            chkSpareParts = CType(dgItem.Cells(Me.GRID_CLAIM_COL_SPARE_PARTS_IDX).FindControl(Me.GRID_CLAIM_CTL_SPARE_PARTS), CheckBox)
            If Not chkSpareParts Is Nothing Then
                If chkSpareParts.Checked Then dr.SPARE_PARTS = Me.YES_VALUE Else dr.SPARE_PARTS = Me.NO_VALUE
            Else
                dr.SPARE_PARTS = Me.NO_VALUE
            End If
            drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_SPARE_PARTS) = dr.SPARE_PARTS

            dt.Rows.Add(dr)

        Next

        Try
            If dt.Rows.Count > 0 Then
                Dim _invoiceTrans As New InvoiceTrans

                If (Not Me.BOOL_VALID_DATES) Then

                    'If Not bool_RepairEntered Then
                    '    Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_REPAIR_DATE_MISSING, True)
                    'End If
                    If Not bool_RepairValid Then
                        Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_REPAIR_DATE_ERR2, True)
                    End If
                    If Not bool_PickUpValid Then
                        Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR2, True)
                    End If
                    If Not Me.BOOL_VALID_DATES Then
                        Me.MasterPage.MessageController.Show()
                    End If

                    Me.PopulateGrid()
                    Me.PopulateCalculatedFields()
                    Return False
                Else
                    If Me.State.InvoiceTaxType = Invoice_Tax_Type Then
                        If (_invoiceTrans.SaveBatchTax(Me.State.MyBO.Id, Me.State.MyBO.Tax1Amount, Me.State.MyBO.Tax2Amount,
                                                   TextBoxBatchNumber.Text.ToString, Me.State.MyBO.RegionId)) Then
                        Else
                            Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_WRITE_ERROR, True)
                            Return False
                        End If
                    End If

                    'For Each drCurrRow In dt.Rows
                    '    Me.State.htSalvage.Add(drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_CLAIM_ID), arraySalvage(dt.Rows.IndexOf(drCurrRow)))
                    '    ' _claim = New Claim(New Guid(GuidControl.HexToByteArray(CType(drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_CLAIM_ID), String))))
                    '    '_claim.SalvageAmount = CType(drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_SALVAGE_AMOUNT), Decimal)
                    '    ' _claim.SalvageAmount = CType(arraySalvage(dt.Rows.IndexOf(drCurrRow)), Decimal)
                    '    ' _claim.Save()
                    'Next

                    _invoiceTrans.SaveBatch(dsBCI, Me.State.MyBO.Id)

                    If (Not Me.BOOL_PAY_CLAIMS) Then
                        Me.State.searchInvoiceTransDetailDV = Nothing
                        Me.PopulateGrid()
                        Me.PopulateCalculatedFields()
                        Return False

                    End If
                End If
            End If

            'make sure all repair dates are set before paying claims
            If BOOL_PAY_CLAIMS Then
                For Each drCurrRow In Me.State.searchInvoiceTransDetailDV.Table.Rows
                    If drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_REPAIR_DATE) Is DBNull.Value Then
                        Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_REPAIR_DATE_MISSING, True)
                        Return False
                    End If
                Next
            End If

        Catch ex As Exception
            Me.MasterPage.MessageController.AddErrorAndShow(ex.Message, False)
            Return False
        End Try

        Return True

    End Function



    Private Sub PayClaims()

        Dim _invoiceTrans As New InvoiceTrans

        Try
            BOOL_PAY_CLAIMS = True
            If SaveClaims() Then

                If _invoiceTrans.ProcessBatch(Me.State.MyBO.Id, Me.State.InvoiceTaxTypeID) Then
                    Me.callPage(PayBatchClaimListForm.URL, "P")
                Else
                    PopulateGrid()
                    PopulateCalculatedFields()
                End If
            End If

        Catch ex As Exception
            Me.MasterPage.MessageController.AddErrorAndShow(ex.Message, False)
        End Try

    End Sub



    Private Function IsRepairDateEmpty() As Boolean
        Dim drCurrRow As DataRow, blnResult As Boolean = False
        If Not State.searchInvoiceTransDetailDV Is Nothing Then
            For Each drCurrRow In Me.State.searchInvoiceTransDetailDV.Table.Rows
                If drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_REPAIR_DATE) Is DBNull.Value Then
                    blnResult = True
                    Exit For
                End If
            Next
        End If
        Return blnResult
    End Function
    'REQ-5565
    Private Sub CheckBatchNumberInPreInvoiceAndApproved()
        Dim objInvoiceTrans As New InvoiceTrans
        Me.State.isBatchInPreInvoiceAndPending = False ''Enable the pay claim button
        Dim ds As DataSet

        If (Me.State.companyBO.UsePreInvoiceProcessId = LookupListNew.GetIdFromCode(LookupListNew.LK_YESNO, "Y")) Then
            If (Me.TextBoxBatchNumber.Text <> String.Empty) Then
                ds = objInvoiceTrans.CheckForPreInvoice(Me.TextBoxBatchNumber.Text.Trim)
                If ds.Tables(0).Rows.Count > 0 Then
                    If (New Guid(CType(ds.Tables(0).Rows(0)(0), Byte())) = LookupListNew.GetIdFromCode("PREINVSTAT", "P")) Then
                        Me.State.isBatchInPreInvoiceAndPending = True  ''disable the pay claim button
                    End If

                End If
            End If
        End If
    End Sub
#End Region

#Region " Datagrid Related "

    Private Sub GridClaims_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaims.RowDataBound
        Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
        Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

        Dim chkClose, chkSpareParts As CheckBox
        Dim txtAmount, txtRepairDt, txtPickupDt, txtSalvage, txtTotalBonus As TextBox
        Dim imgbtnPickUpDt, imgbtnRepairDt As ImageButton
        Dim decReserve As Decimal
        Dim decAmount As Decimal
        Dim decSalvage As Decimal
        Dim decDeductible As Decimal
        Dim excludeDeductible As String
        Dim dscBonusTotal As Decimal

        Try
            If Not dvRow Is Nothing And Not Me.State.bnoRow And Me.State.searchInvoiceTransDetailDV.Count > 0 Then
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then

                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_CLAIM_ID_IDX), dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_CLAIM_ID))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_CLAIM_NUMBER_IDX), dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_CLAIM_NUMBER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_CLAIM_MODIFIED_DATE_IDX), dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_CLAIM_MODIFIED_DATE).ToString)
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_AUTHORIZATION_NUMBER_IDX), dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_NAME_AUTHORIZATION_NUMBER))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_CUSTOMER_NAME_IDX), dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_CONTACT_NAME))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_INVOICE_TRANS_NUMBER_IDX), dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_INVOICE_TRANS_ID))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_INVOICE_TRANS_DETAIL_IDX), dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_INVOICE_TRANS_DETAIL_ID))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_RESERVE_AMOUNT_IDX), dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_RESERVE_AMOUNT))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_TOTAL_BONUS_IDX), dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_Total_BONUS))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_LOSS_DATE_IDX), dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_LOSS_DATE))
                    Me.PopulateControlFromBOProperty(e.Row.Cells(Me.GRID_CLAIM_COL_DEDUCTIBLE_IDX), dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_DEDUCTIBLE))

                    excludeDeductible = dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_EXCLUDE_DEDUCTIBLE).ToString

                    decReserve = Decimal.Parse(dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_RESERVE_AMOUNT).ToString)
                    decAmount = Decimal.Parse(dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_PAYMENT_AMOUNT).ToString)
                    decSalvage = Decimal.Parse(dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_SALVAGE_AMOUNT).ToString)
                    decDeductible = Decimal.Parse(dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_DEDUCTIBLE).ToString)
                    dscBonusTotal = Decimal.Parse(dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_Total_BONUS).ToString)



                    txtSalvage = CType(e.Row.Cells(Me.GRID_CLAIM_COL_SALVAGE_AMOUNT_IDX).FindControl(Me.GRID_CLAIM_CTL_SALVAGE_AMOUNT), TextBox)
                    txtAmount = CType(e.Row.Cells(Me.GRID_CLAIM_COL_PAYMENT_AMOUNT_IDX).FindControl(Me.GRID_CLAIM_CTL_PAYMENT_AMOUNT), TextBox)

                    'txtTotalBonus = CType(e.Row.Cells(Me.GRID_CLAIM_COL_TOTAL_BONUS_IDX).FindControl(Me.GRID_CLAIM_CTL_TOTAL_BONUS), TextBox)
                    'If Not txtTotalBonus Is Nothing Then
                    '    txtTotalBonus.Text = CType(IIf(dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_Total_BONUS) Is DBNull.Value, New Decimal(0D), dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_Total_BONUS)), Double).ToString("0.00")
                    'End If

                    If Not txtSalvage Is Nothing Then
                        If excludeDeductible = "Y" Then
                            txtSalvage.Attributes("onchange") = "calcAmountToBePaid('" + txtSalvage.ClientID + "','" + txtAmount.ClientID + "'," + ((decReserve + dscBonusTotal - decDeductible) * 100).ToString + "," + (decSalvage * 100).ToString + ");" 'chkamt('" + txtAmount.ClientID + "'," + (decReserve * 100).ToString + ");"
                        Else
                            txtSalvage.Attributes("onchange") = "calcAmountToBePaid('" + txtSalvage.ClientID + "','" + txtAmount.ClientID + "'," + ((decReserve + dscBonusTotal) * 100).ToString + "," + (decSalvage * 100).ToString + ");chkamt('" + txtAmount.ClientID + "'," + ((decReserve + dscBonusTotal) * 100).ToString + ");"
                        End If
                        txtSalvage.Attributes("onfocus") = "setCur('" + txtAmount.ClientID + "');"
                        txtSalvage.Text = CType(IIf(dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_SALVAGE_AMOUNT) Is DBNull.Value, New Decimal(0D), dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_SALVAGE_AMOUNT)), Double).ToString("0.00")

                    End If


                    If Not txtAmount Is Nothing Then
                        If decReserve = 0 Then
                            txtAmount.Text = "0.00"
                            txtAmount.Enabled = False
                        Else
                            'txtAmount.Attributes("onchange") = "chkamt('" + txtAmount.ClientID + "'," + (decReserve * 100).ToString + ",'" + Me.State.InvoiceTaxType + "');"
                            'txtAmount.Attributes("onfocus") = "setCur('" + txtAmount.ClientID + "');"
                            txtAmount.Attributes("onchange") = "chkamt('" + txtAmount.ClientID + "'," + ((decReserve + dscBonusTotal) * 100).ToString + ");"
                            txtAmount.Attributes("onfocus") = "setCur('" + txtAmount.ClientID + "');"
                            If Not IsPostBack And Not decAmount >= 0 Then
                                If excludeDeductible = "Y" Then
                                    txtAmount.Text = CType((decReserve + dscBonusTotal - decSalvage - decDeductible), Double).ToString("0.00")
                                Else
                                    txtAmount.Text = CType((decReserve + dscBonusTotal - decSalvage), Double).ToString("0.00")
                                End If
                            Else
                                'If excludeDeductible = "Y" Then
                                'txtAmount.Text = CType((decReserve - decDeductible), Double).ToString("0.00")
                                'Else
                                txtAmount.Text = CType(decAmount + dscBonusTotal, Double).ToString("0.00")
                                'End If
                            End If
                        End If
                    End If




                End If

                txtRepairDt = CType(e.Row.Cells(Me.GRID_CLAIM_COL_REPAIR_DATE_IDX).FindControl(Me.GRID_CLAIM_CTL_REPAIR_DATE), TextBox)
                If Not txtRepairDt Is Nothing Then
                    If DateHelper.IsDate(dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_REPAIR_DATE)) Then txtRepairDt.Text = Me.GetDateFormattedString(CType(dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_REPAIR_DATE), Date))
                    If Not arrRepair Is Nothing Then
                        If arrRepair.IndexOf(e.Row.RowIndex) >= 0 Then
                            txtRepairDt.CssClass = Me.CSS_ERROR_CLASS
                        End If
                    End If

                    imgbtnRepairDt = CType(e.Row.Cells(Me.GRID_CLAIM_COL_REPAIR_DATE_IDX).FindControl("ImageButtonRepairDate"), ImageButton)
                    If Not imgbtnRepairDt Is Nothing Then
                        Me.AddCalendar(imgbtnRepairDt, txtRepairDt)
                    End If
                End If

                txtPickupDt = CType(e.Row.Cells(Me.GRID_CLAIM_COL_PICKUP_DATE_IDX).FindControl(Me.GRID_CLAIM_CTL_PICKUP_DATE), TextBox)
                If Not txtPickupDt Is Nothing Then
                    If DateHelper.IsDate(dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_PICKUP_DATE)) Then txtPickupDt.Text = Me.GetDateFormattedString(CType(dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_PICKUP_DATE), Date))
                    If Not arrPickUp Is Nothing Then
                        If arrPickUp.IndexOf(e.Row.RowIndex) >= 0 Then
                            txtPickupDt.CssClass = Me.CSS_ERROR_CLASS
                        End If
                    End If

                    imgbtnPickUpDt = CType(e.Row.Cells(Me.GRID_CLAIM_COL_PICKUP_DATE_IDX).FindControl("ImageButtonPickupDate"), ImageButton)
                    If Not imgbtnPickUpDt Is Nothing Then
                        Me.AddCalendar(imgbtnPickUpDt, txtPickupDt)
                    End If
                End If

                chkClose = CType(e.Row.Cells(Me.GRID_CLAIM_COL_CLOSE_CLAIM_IDX).FindControl(Me.GRID_CLAIM_CTL_CLOSE_CLAIM), CheckBox)
                If Not chkClose Is Nothing Then
                    If (dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_CLOSE_CLAIM).ToString = String.Empty) _
                        OrElse (dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_CLOSE_CLAIM).ToString = Me.YES_VALUE) _
                        OrElse decReserve = 0 Then
                        chkClose.Checked = True
                        If decReserve = 0 Then chkClose.Enabled = False
                    Else
                        chkClose.Checked = False
                    End If
                End If

                chkSpareParts = CType(e.Row.Cells(Me.GRID_CLAIM_COL_SPARE_PARTS_IDX).FindControl(Me.GRID_CLAIM_CTL_SPARE_PARTS), CheckBox)
                If Not chkSpareParts Is Nothing Then
                    If (dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_SPARE_PARTS).ToString = String.Empty) _
                        OrElse (dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_SPARE_PARTS).ToString = Me.NO_VALUE) Then
                        chkSpareParts.Checked = False
                    Else
                        chkSpareParts.Checked = True
                    End If

                    If dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_CLAIM_NUMBER).ToString.EndsWith("R") Then
                        chkSpareParts.Enabled = False
                    End If

                End If

                If dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_STATUS).ToString.Equals("F") Then
                    e.Row.CssClass = Me.CSS_ERROR_CLASS
                    If txtAmount.Text <> "0.00" Then txtAmount.CssClass = Me.CSS_ERROR_CLASS
                    Me.BOOL_ERR_ADDED = True
                End If

                'REQ-5578, Amount to be paid not allowed to be changed if claim is part of pre-invoice, to avoid partial payment
                If dvRow(InvoiceTrans.InvoiceTransDetailDV.COL_ISPreInvoiceClaim).ToString.Equals("Y") Then
                    txtAmount.Enabled = False
                End If
            End If


        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try

    End Sub

    Private Sub GridClaims_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridClaims.PageIndexChanged

        'Me.CURRENT_PAGE = GridClaims.PageIndex
        Me.CURRENT_PAGE_SIZE = GridClaims.PageSize
        SaveClaims()
        Me.CURRENT_PAGE = GridClaims.PageIndex
    End Sub

    Private Sub GridClaims_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridClaims.PageIndexChanging
        Try
            Me.CURRENT_PAGE = GridClaims.PageIndex
            GridClaims.PageIndex = e.NewPageIndex
            State.PageIndex = GridClaims.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
    End Sub

    Private Sub GridClaims_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles GridClaims.RowCreated
        Dim epsp As New ElitaPlusSearchPage
        epsp.BaseItemCreated(sender, e)
    End Sub


#End Region

#Region " Button clicks "

    Private Sub btnPay_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPay_WRITE.Click

        Me.CURRENT_PAGE = GridClaims.PageIndex
        Me.CURRENT_PAGE_SIZE = GridClaims.PageSize

        PayClaims()
    End Sub

    Private Sub btnReject_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReject_WRITE.Click

        ControlMgr.SetVisibleControl(Me, Me.txtareaRejectReason, True)
        ControlMgr.SetVisibleControl(Me, Me.btnRejectSave, True)
        ControlMgr.SetVisibleControl(Me, Me.btnRejectCancel, True)
        ControlMgr.SetVisibleControl(Me, Me.lblRejectReason, True)
        ControlMgr.SetVisibleControl(Me, Me.btnCancel, False)
        ControlMgr.SetVisibleControl(Me, Me.btnPay_WRITE, False)
        ControlMgr.SetVisibleControl(Me, Me.btnSave_WRITE, False)
        ControlMgr.SetVisibleControl(Me, Me.btnReject_WRITE, False)
    End Sub

    Private Sub btnRejectSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRejectSave.Click

        Dim _invoiceTrans As New InvoiceTrans

        ControlMgr.SetVisibleControl(Me, Me.txtareaRejectReason, False)
        ControlMgr.SetVisibleControl(Me, Me.btnRejectSave, False)
        ControlMgr.SetVisibleControl(Me, Me.btnRejectCancel, False)
        ControlMgr.SetVisibleControl(Me, Me.lblRejectReason, False)
        ControlMgr.SetVisibleControl(Me, Me.btnCancel, True)
        ControlMgr.SetVisibleControl(Me, Me.btnPay_WRITE, True)
        ControlMgr.SetVisibleControl(Me, Me.btnSave_WRITE, True)
        ControlMgr.SetVisibleControl(Me, Me.btnReject_WRITE, True)

        If _invoiceTrans.UpdateRejectReason(Me.State.MyBO.Id, Me.txtareaRejectReason.Text.ToString()) Then
            Me.callPage(PayBatchClaimListForm.URL, "R")
        Else
            Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_INVOICE_COMMENTS_UPDATE_ERR, True)
        End If

    End Sub
    Private Sub btnRejectCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRejectCancel.Click

        ControlMgr.SetVisibleControl(Me, Me.txtareaRejectReason, False)
        ControlMgr.SetVisibleControl(Me, Me.btnRejectSave, False)
        ControlMgr.SetVisibleControl(Me, Me.btnRejectCancel, False)
        ControlMgr.SetVisibleControl(Me, Me.lblRejectReason, False)
        ControlMgr.SetVisibleControl(Me, Me.btnCancel, True)
        ControlMgr.SetVisibleControl(Me, Me.btnPay_WRITE, True)
        ControlMgr.SetVisibleControl(Me, Me.btnSave_WRITE, True)
        ControlMgr.SetVisibleControl(Me, Me.btnReject_WRITE, True)

    End Sub
    Private Sub btnSave_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave_WRITE.Click

        Me.CURRENT_PAGE = GridClaims.PageIndex
        Me.CURRENT_PAGE_SIZE = GridClaims.PageSize

        SaveClaims()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.ReturnToCallingPage(New ReturnType(Me.State.MyBO.Id))
    End Sub

    Private Sub btnAddRepairDate_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddRepairDate_WRITE.Click

        Dim strRepairDt As String = TextBoxRepairDate.Text.Trim
        Dim dtRepair As Date, drCurrRow As DataRow, blnValid As Boolean = True
        Dim dtLoss As Date, dtPickUp As Date
        If strRepairDt = String.Empty Then
            Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_REPAIR_DATE_MISSING, True)
        Else
            If DateHelper.IsDate(strRepairDt) Then
                dtRepair = DateHelper.GetDateValue(strRepairDt)
                If dtRepair > Today Then
                    Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_REPAIR_DATE_ERR2, True)
                    blnValid = False
                Else
                    If Not State.searchInvoiceTransDetailDV Is Nothing Then
                        For Each drCurrRow In Me.State.searchInvoiceTransDetailDV.Table.Rows
                            If drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_REPAIR_DATE) Is DBNull.Value Then
                                dtLoss = DateHelper.GetDateValue(drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_LOSS_DATE).ToString)
                                If dtRepair < dtLoss Then
                                    Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_REPAIR_DATE_ERR2, True)
                                    blnValid = False
                                    Exit For
                                End If
                                If Not drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_PICKUP_DATE) Is DBNull.Value Then
                                    dtPickUp = DateHelper.GetDateValue(drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_PICKUP_DATE).ToString)
                                    If dtRepair > dtPickUp Then
                                        Me.MasterPage.MessageController.AddError(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_PICK_UP_DATE_ERR2, True)
                                        blnValid = False
                                        Exit For
                                    End If
                                End If
                            End If
                        Next
                    End If
                End If

                If blnValid Then
                    'save changes if there is any
                    SaveClaims()

                    Dim dsBCI As New DALObjects.BatchClaimInvoice
                    Dim dt As DataTable = dsBCI.INVOICE_TRANS_DETAIL
                    Dim dr As DALObjects.BatchClaimInvoice.INVOICE_TRANS_DETAILRow

                    For Each drCurrRow In Me.State.searchInvoiceTransDetailDV.Table.Rows
                        If drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_REPAIR_DATE) Is DBNull.Value Then
                            drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_REPAIR_DATE) = dtRepair

                            dr = dsBCI.INVOICE_TRANS_DETAIL.NewINVOICE_TRANS_DETAILRow
                            dr.ACTION = Me.CLAIM_STATUS_UPDATE
                            dr.INVOICE_TRANS_DETAIL_ID = GuidControl.GuidToHexString(New Guid(CType(drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_INVOICE_TRANS_DETAIL_ID), Byte())))
                            dr.CLAIM_ID = GuidControl.GuidToHexString(New Guid(CType(drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_CLAIM_ID), Byte())))
                            dr.CLAIM_MODIFIED_DATE = DateHelper.GetDateValue(drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_CLAIM_MODIFIED_DATE).ToString)
                            dr.INVOICE_TRANS_ID = GuidControl.GuidToHexString(Me.State.MyBO.Id)
                            dr.USER_ID = GuidControl.GuidToHexString(ElitaPlusIdentity.Current.ActiveUser.Id)
                            dr.PAYMENT_AMOUNT = Double.Parse(drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_PAYMENT_AMOUNT).ToString)
                            dr.REPAIR_DATE = dtRepair
                            If Not drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_PICKUP_DATE) Is DBNull.Value Then dr.PICKUP_DATE = DateHelper.GetDateValue(drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_PICKUP_DATE).ToString)
                            dr.SPARE_PARTS = drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_SPARE_PARTS).ToString
                            dr.CLOSE_CLAIM = drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_CLOSE_CLAIM).ToString
                            dr.SALVAGE_AMOUNT = Double.Parse(drCurrRow(InvoiceTrans.InvoiceTransDetailDV.COL_SALVAGE_AMOUNT).ToString)
                            dt.Rows.Add(dr)

                        End If
                    Next
                    If dt.Rows.Count > 0 Then
                        Dim _invoiceTrans As New InvoiceTrans
                        _invoiceTrans.SaveBatch(dsBCI, Me.State.MyBO.Id)
                    End If
                    State.searchInvoiceTransDetailDV = Nothing
                    PopulateGrid()
                    TextBoxRepairDate.Text = String.Empty
                Else
                    Me.MasterPage.MessageController.Show()
                End If
            Else
                Me.MasterPage.MessageController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_DATE_ERR, True)
            End If
        End If
    End Sub
#End Region

#Region "Invoice Region Taxes"

    Private Sub IIBBTaxes_RequestIIBBTaxes(ByVal sender As Object, ByRef e As UserControlInvoiceRegionTaxes.RequestDataEventArgs) Handles IIBBTaxes.RequestIIBBTaxesData
        Dim iibbregion As New InvoiceRegionTax
        iibbregion.InvoiceRegionTaxId = Me.State.MyBO.Id
        e.Data = iibbregion.GetInvoiceRegionTax()
    End Sub

#End Region

#Region " Page Events "
    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged

        Me.CURRENT_PAGE = GridClaims.PageIndex
        Me.CURRENT_PAGE_SIZE = GridClaims.PageSize

        GridClaims.PageIndex = 0
        GridClaims.PageSize = Integer.Parse(cboPageSize.SelectedValue)
        If Not SaveClaims() Then
            If Not cboPageSize.Items.FindByValue(Me.CURRENT_PAGE_SIZE.ToString) Is Nothing Then cboPageSize.SelectedValue = Me.CURRENT_PAGE_SIZE.ToString
        End If

    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.InvoiceTabs.Visible = True
        Me.MasterPage.MessageController.Clear()

        Try
            RegisterClientServerIds()
            Me.AddCalendar(ImageButtonRepirDate, TextBoxRepairDate)
            Dim msg As String = TranslationBase.TranslateLabelOrMessage("PROCESS_RECORDS")
            Me.AddConfirmationAndDisplayNewProgressBar(Me.btnPay_WRITE, msg + "?", msg, False)

            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                Me.TranslateGridHeader(Me.GridClaims)
                Me.TranslateGridControls(Me.GridClaims)
                Me.State.searchInvoiceTransDetailDV = Nothing
                ChkInvoiceTaxTypeAndValidate()
                EnableOrDisableControls()
                LoadRegionList()
                PopulateFormFromBOs()
                PopulateGrid()
                CheckBatchNumberInPreInvoiceAndApproved()
                PopulateCalculatedFields()
                CheckGetBatchClosedClaims()
                Me.IIBBTaxes.InvoicetransId = Me.State.MyBO.Id
                Me.IIBBTaxes.Populate()
                Dim statusId As String = LookupListNew.GetCodeFromId(LookupListNew.LK_INVSTAT, Me.State.MyBO.InvoiceStatusId)
                'If statusId = INVOICE_STATUS_PAID Or statusId = INVOICE_STATUS_REJECTED Then
                '    ControlMgr.SetEnableControl(Me, Me.btnSave_WRITE, False)
                '    ControlMgr.SetEnableControl(Me, Me.btnNEXT_WRITE, False)
                'End If
                Me.IIBBTaxes.InvoiceStatus = statusId
                Me.IIBBTaxes.SetControlState()
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.MasterPage.MessageController)
        End Try
        Me.ShowMissingTranslations(Me.MasterPage.MessageController)

    End Sub

    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Try
            If IsRepairDateEmpty() Then
                TextBoxRepairDate.Enabled = True
                ImageButtonRepirDate.Enabled = True
                btnAddRepairDate_WRITE.Enabled = True
            Else
                TextBoxRepairDate.Enabled = False
                ImageButtonRepirDate.Enabled = False
                btnAddRepairDate_WRITE.Enabled = False
            End If
            Dim dv As InvoiceTrans.InvoiceTransDetailDV
            dv = InvoiceTrans.GetInvoiceTransDetail(Me.State.MyBO.Id)
            Me.State.searchInvoiceTransDetailDV = dv
            Me.PopulateCalculatedFields()
        Catch ex As Exception
        End Try
    End Sub

#End Region

End Class
