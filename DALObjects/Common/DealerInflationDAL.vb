Public Class DealerInflationDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_DEALER_INFLATION"
    Public Const TABLE_KEY_NAME As String = "DEALER_INFLATION_ID"
	
    Public Const COL_NAME_DEALER_INFLATION_ID As String = "DEALER_INFLATION_ID"
    Public Const COL_NAME_COMPANY_ID As String = "COMPANY_ID"
    Public Const COL_NAME_DEALER_ID As String = "DEALER_ID"
    Public Const COL_NAME_INFLATION_MONTH As String = "INFLATION_MONTH"
    Public Const COL_NAME_INFLATION_YEAR As String = "INFLATION_YEAR"
    Public Const COL_NAME_INFLATION_PCT As String = "INFLATION_PCT"
    

    'stored procedure parameter names
    Public Const PAR_NAME_DEALER_INFLATION_ID As String = "pi_dealer_inflation_id"
    Public Const PAR_NAME_INFLATION_PCT As String = "pi_inflation_pct"
    Public Const PAR_NAME_DEALER As String = "pi_dealer_id"
    Public Const PAR_NAME_INFLATION_MONTH As String = "pi_inflation_month"
    Public Const PAR_NAME_INFLATION_YEAR As String = "pi_inflation_year"
    Public Const PAR_NAME_RETURN_CODE As String = "po_return_code"
    public Const PAR_NAME_REF_CURSOR As string ="po_dealer_inflation"
    Public Const PARAM_CRUD_STATUS As String = "po_crud_Status"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(ByVal familyDS As DataSet, ByVal id As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_DEALER_INFLATION")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.PAR_NAME_DEALER, id.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter(){New DBHelper.DBHelperParameter(Me.PAR_NAME_REF_CURSOR, GetType(DataSet))}
        Try
            DBHelper.FetchSp(selectStmt, parameters, outParameters, familyDS, Me.TABLE_NAME)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadDealerInflation(ByVal dealerId As Guid) As DataSet
        Try
            Dim selectStmt As String = Me.Config("/SQL/LOAD_DEALER_INFLATION")
           
            Dim inparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(Me.PAR_NAME_DEALER, dealerId.ToByteArray)}
            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter(){New DBHelper.DBHelperParameter(Me.PAR_NAME_REF_CURSOR, GetType(DataSet))}

            Dim ds As New DataSet
            Dim tbl As String = Me.TABLE_NAME

            DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, tbl)
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
    public Function SaveDealerInflation(ByVal row As DataRow) As String

        Dim sqlstatement As String
        Dim rowState As DataRowState = row.RowState
        Dim updatedby As string
        Try
         Select Case rowState
                    Case DataRowState.Added
                        'Insert
                        sqlstatement = Me.Config("/SQL/INSERT")
                        updatedby= COL_NAME_CREATED_BY
                    Case DataRowState.Deleted
                        'delete
                        sqlstatement = Me.Config("/SQL/DELETE")
                    Case DataRowState.Modified
                        'update
                        sqlstatement = Me.Config("/SQL/UPDATE")
                        updatedby= COL_NAME_MODIFIED_BY
            End Select

            Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {
                        New DBHelper.DBHelperParameter(Me.PARAM_CRUD_STATUS, GetType(Integer))
                    }
            
           If rowState = DataRowState.Deleted Then
               Dim inParameter() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                       {
                           New DBHelper.DBHelperParameter( Me.PAR_NAME_DEALER_INFLATION_ID.ToLower(),   row(Me.COL_NAME_DEALER_INFLATION_ID,DataRowVersion.Original))
                       }
               DBHelper.ExecuteSp(sqlstatement, inParameter, outputParameters)
               row.AcceptChanges()
           Else 
               Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                       {New DBHelper.DBHelperParameter( Me.PAR_NAME_DEALER_INFLATION_ID, row(Me.COL_NAME_DEALER_INFLATION_ID)),
                        New DBHelper.DBHelperParameter( Me.PAR_NAME_DEALER, row(Me.COL_NAME_DEALER_ID)),
                        New DBHelper.DBHelperParameter( Me.PAR_NAME_INFLATION_MONTH, row(Me.COL_NAME_INFLATION_MONTH)),
                        New DBHelper.DBHelperParameter( Me.PAR_NAME_INFLATION_YEAR, row(Me.COL_NAME_INFLATION_YEAR)),
                        New DBHelper.DBHelperParameter( Me.PAR_NAME_INFLATION_PCT, row(Me.COL_NAME_INFLATION_PCT)),
                        New DBHelper.DBHelperParameter("pi_"& updateDby.ToLower(), row(updatedby))
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
