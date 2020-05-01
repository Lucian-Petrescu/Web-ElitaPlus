Public Class IIBBRegionTaxesDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_INVOICE_REGION_TAX"
    'Public Const TABLE_KEY_NAME As String = "INVOICE_TRANS_ID"
    Public Const TABLE_KEY_NAME As String = "INVOICE_REGION_TAX_ID"

    Public Const COL_NAME_INVOICE_REGION_TAX_ID As String = "INVOICE_REGION_TAX_ID"
    Public Const COL_NAME_INVOICE_TRANS_ID As String = "INVOICE_TRANS_ID"
    Public Const COL_NAME_REGION As String = "REGION"
    Public Const COL_NAME_REGION_ID As String = "REGION_ID"
    Public Const COL_NAME_TAX_AMOUNT As String = "TAX_AMOUNT"
    Public Const COL_NAME_TAX_TYPE As String = "TAX_TYPE_XCD"
    Public Const COL_NAME_REGION_DESCRIPTION As String = "REGION_DESCRIPTION"




    'stored procedure parameter names
    Public Const PAR_NAME_INVOICE_REGION_TAX_ID As String = "pi_invoice_region_tax_id"
    Public Const PAR_NAME_INVOICE_TRANS_ID As String = "pi_invoice_trans_id"
    Public Const PAR_NAME_TAX_TYPE As String = "pi_tax_type_xcd"
    Public Const PAR_NAME_REGION_ID As String = "pi_region_id"
    Public Const PAR_NAME_TAX_AMOUNT As String = "pi_tax_amount"
    Public Const PAR_NAME_REF_CURSOR As String = "po_invoice_region_tax_cur"
    Public Const PARAM_CRUD_STATUS As String = "po_crud_Status"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_INVOICE_REGION_TAX")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.PAR_NAME_INVOICE_TRANS_ID, id.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.PAR_NAME_REF_CURSOR, GetType(DataSet))}
        Try
            DBHelper.FetchSp(selectStmt, parameters, outParameters, familyDS, Me.TABLE_NAME)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadInvoiceRegionTax(ByVal id As Guid) As DataSet
        Try
            Dim selectStmt As String = Me.Config("/SQL/LOAD_INVOICE_REGION_TAX")

            Dim inparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.PAR_NAME_INVOICE_TRANS_ID, id.ToByteArray)}
            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.PAR_NAME_REF_CURSOR, GetType(DataSet))}

            Dim ds As New DataSet
            Dim tbl As String = Me.TABLE_NAME

            DBHelper.FetchSp(selectStmt, inparameters, outParameters, ds, tbl)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "Public Methods"
    Public Function SaveInvoiceRegionTax(ByVal row As DataRow) As String

        Dim sqlstatement As String
        Dim rowState As DataRowState = row.RowState
        Dim updatedby As String
        Try
            Select Case rowState
                Case DataRowState.Added
                    'Insert
                    sqlstatement = Me.Config("/SQL/INSERT_INVOICE_REGION_TAX")
                    updatedby = COL_NAME_CREATED_BY
                Case DataRowState.Deleted
                    'delete
                    sqlstatement = Me.Config("/SQL/DELETE_INVOICE_REGION_TAX")
                Case DataRowState.Modified
                    'update
                    sqlstatement = Me.Config("/SQL/UPDATE_INVOICE_REGION_TAX")
                    updatedby = COL_NAME_MODIFIED_BY
            End Select

            Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {
                        New DBHelper.DBHelperParameter(Me.PARAM_CRUD_STATUS, GetType(Integer))
                    }

            If rowState = DataRowState.Deleted Then
                Dim inParameter() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                       {
                           New DBHelper.DBHelperParameter(Me.PAR_NAME_INVOICE_REGION_TAX_ID.ToLower(), row(Me.COL_NAME_INVOICE_REGION_TAX_ID, DataRowVersion.Original))
                       }
                DBHelper.ExecuteSp(sqlstatement, inParameter, outputParameters)
                row.AcceptChanges()
            ElseIf rowState = DataRowState.Added Then
                Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                       {New DBHelper.DBHelperParameter(Me.PAR_NAME_INVOICE_REGION_TAX_ID, row(Me.COL_NAME_INVOICE_REGION_TAX_ID)),
                        New DBHelper.DBHelperParameter(Me.PAR_NAME_INVOICE_TRANS_ID, row(Me.COL_NAME_INVOICE_TRANS_ID)),
                        New DBHelper.DBHelperParameter(Me.PAR_NAME_REGION_ID, row(Me.COL_NAME_REGION_ID)),
                        New DBHelper.DBHelperParameter(Me.PAR_NAME_TAX_TYPE, row(Me.COL_NAME_TAX_TYPE)),
                        New DBHelper.DBHelperParameter(Me.PAR_NAME_TAX_AMOUNT, row(Me.COL_NAME_TAX_AMOUNT)),
                        New DBHelper.DBHelperParameter("pi_" & updatedby.ToLower(), row(updatedby))
                       }
                DBHelper.ExecuteSp(sqlstatement, inParameters, outputParameters)
                row.AcceptChanges()
            Else
                Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                      {New DBHelper.DBHelperParameter(Me.PAR_NAME_INVOICE_REGION_TAX_ID, row(Me.COL_NAME_INVOICE_REGION_TAX_ID)),
                       New DBHelper.DBHelperParameter(Me.PAR_NAME_TAX_AMOUNT, row(Me.COL_NAME_TAX_AMOUNT)),
                       New DBHelper.DBHelperParameter(Me.PAR_NAME_TAX_TYPE, row(Me.COL_NAME_TAX_TYPE)),
                       New DBHelper.DBHelperParameter("pi_" & updatedby.ToLower(), row(updatedby))
                      }
                DBHelper.ExecuteSp(sqlstatement, inParameters, outputParameters)
                row.AcceptChanges()
            End If


            If CType(outputParameters(0).Value, Integer) <> 1 Then
                Dim e As New ApplicationException("Return Value = " & outputParameters(0).Value)
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, e)
            Else
                Return outputParameters(0).Value.ToString
            End If

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region
End Class
