Namespace Tables.Accounting.AccountPayable
    Public Class PoAdjustmentDAL
        Inherits DALBase

#Region "Constants"

        public Const TOTAL_PARAM_PO_UPDATE As Integer = 4
        Public Const PO_LINES_TABLE_NAME As String  = "elp_ap_po_line"
        Public Const PO_LINE_ID_COL As String = "po_line_id"
        Public Const VENDOR_COL As String = "Vendor"
        Public Const PO_NUMBER_COL As String = "PO_Number"
        Public Const LINE_NUMBER_COL As String = "line_number"
        Public Const ITEM_CODE_COL As String = "Item_Code"
        Public Const DESCRIPTION_COL As String = "Description"
        Public Const QUANTITY_COL As String = "Quantity"
        Public Const UNIT_PRICE_COL As String = "Unit_Price"
        Public Const EXTENDED_PRICE_COL As String = "Extende_Price" 
        Public Const COMPANY_ID_COL As String = "company_id"
        Public Const MODIFIED_BY_COL As String ="modified_by"
        Public Const FIRST_POS As Integer = 0
        Public Const COL_NAME_RETURN As String = "po_update_status"

        Public Const PI_VENDOR As String = "pi_vendor"
        Public Const PI_AP_PO_NUMBER As String = "pi_ap_po_number"
        Public Const PI_AP_PO_LINE_ID As String ="pi_ap_po_line_id"
        Public Const PI_AP_PO_LINE_QTY As String = "pi_ap_po_line_qty"
        Public Const PI_MODIFIED_BY_COL As String ="pi_modified_by"
        Public Const PI_COMPANY_ID_COL As String = "pi_company_id"
        public Const PO_PO_LINES As String ="po_ap_lines"
       
        Public Const AP_PO_NUMBER = 0    
        Public Const AP_PO_LINE_ID = 1   
        Public Const AP_PO_COMPANY = 2         
        Public Const AP_PO_LINE_QTY = 3   
        Public Const MODIFIED_BY = 4     
    
#End Region

#Region "Constructors"

    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Public Methods"

    Public Function Load(ByVal ds As DataSet, ByVal vendorCode As String , ByVal poNumber As string,companyGroupId As Guid) As DataSet

        Dim selectStmt As String ="elita.elp_ap_po_update.search"
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(PI_VENDOR, vendorCode), _ 
                                                                     New DBHelper.DBHelperParameter ( PI_AP_PO_NUMBER,poNumber), _
                                                                     New DBHelper.DBHelperParameter (PI_COMPANY_ID_COL,companyGroupId.ToByteArray)}
        Dim outputParameter(0) As DBHelper.DBHelperParameter
        outputParameter(0) = New DBHelper.DBHelperParameter(PO_PO_LINES, GetType(DataSet))
        DBHelper.FetchSp(selectStmt,parameters, outputParameter,ds, PO_LINES_TABLE_NAME)

        Return ds

    End Function

    Public Function LoadSchema(ByVal ds As DataSet) As DataSet

        Return (Me.Load(ds, String.Empty,String.Empty,Guid.Empty))

    End Function

    Public Function UpdatePoLineQuantity(ByVal poNumber As string, ByVal poLineId As Guid, ByVal companyId As Guid, ByVal  poLineQuantity As Decimal,ByVal modifiedBy As string) As Integer
        Dim selectStmt As String = "elita.elp_ap_po_update.update_ap_po_line"
        Dim inputParameters(TOTAL_PARAM_PO_UPDATE) As DBHelper.DBHelperParameter
        Dim outputParameter(0) As DBHelper.DBHelperParameter

        inputParameters(AP_PO_NUMBER) = New DBHelper.DBHelperParameter(PI_AP_PO_NUMBER, poNumber)
        inputParameters(AP_PO_LINE_ID) = New DBHelper.DBHelperParameter(PI_AP_PO_LINE_ID, poLineId.ToByteArray())
        inputParameters(AP_PO_COMPANY) = New DBHelper.DBHelperParameter(PI_COMPANY_ID_COL, companyId.ToByteArray())
        inputParameters(AP_PO_LINE_QTY) = New DBHelper.DBHelperParameter(PI_AP_PO_LINE_QTY, poLineQuantity)
        inputParameters(MODIFIED_BY) = New DBHelper.DBHelperParameter(PI_MODIFIED_BY_COL, modifiedBy)
        outputParameter(0) = New DBHelper.DBHelperParameter(COL_NAME_RETURN, GetType(Integer))

        ' Call DBHelper Store Procedure 
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)

        If  outputParameter(0).Value <> 1 Then
            Dim e As New ApplicationException("Return Value = " & outputParameter(0).Value)
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, e)
        Else
            Return outputParameter(0).Value
        End If

    End Function

   #End Region

    End Class
End NameSpace