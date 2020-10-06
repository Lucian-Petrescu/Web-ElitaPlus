Public Class RiskTypeToleranceDAL
     Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_DLR_RK_TYP_TOLERANCE"
    Public Const TABLE_KEY_NAME As String = "DLR_RK_TYP_TOLERANCE_ID"
	
    Public Const COL_NAME_DLR_RK_TYP_TOLERANCE_ID As String = "DLR_RK_TYP_TOLERANCE_ID"
    Public Const COL_NAME_COMPANY_ID As String = "COMPANY_ID"
    Public Const COL_NAME_DEALER_ID As String = "DEALER_ID"
    Public Const COL_NAME_DEALER As String = "DEALER"
    Public Const COL_NAME_RISK_TYPE As String = "RISK_TYPE"
    Public Const COL_NAME_RISK_TYPE_ID As String = "RISK_TYPE_ID"
    Public Const COL_NAME_TOLERANCE_PCT As String = "TOLERANCE_PCT"
    

    'stored procedure parameter names
    Public Const PAR_NAME_DLR_RK_TYP_TOLERANCE_ID As String = "pi_dlr_rk_typ_tolerance_id"
    Public Const PAR_NAME_TOLERANCE_PCT As String = "pi_tolerance_pct"
    Public Const PAR_NAME_DEALER As String = "pi_dealer_id"
    Public Const PAR_NAME_RISK_TYPE_ID As String = "pi_risk_type_id"
    public Const PAR_NAME_REF_CURSOR As string ="po_risk_type_tolerance"
    Public Const PARAM_CRUD_STATUS As String = "po_crud_Status"

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_RISK_TYPE_TOLERANCE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(PAR_NAME_DEALER, id.ToByteArray)}
        Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter(){New DBHelper.DBHelperParameter(PAR_NAME_REF_CURSOR, GetType(DataSet))}
        Try
            DBHelper.FetchSp(selectStmt, parameters, outParameters, familyDS, TABLE_NAME)

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadRiskTypeTolerance(dealerId As Guid) As DataSet
        Try
            Dim selectStmt As String = Config("/SQL/LOAD_RISK_TYPE_TOLERANCE")
           
            Dim inparameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(PAR_NAME_DEALER, dealerId.ToByteArray)}
            Dim outParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter(){New DBHelper.DBHelperParameter(PAR_NAME_REF_CURSOR, GetType(DataSet))}

            Dim ds As New DataSet
            Dim tbl As String = TABLE_NAME

            DBHelper.FetchSp(selectStmt, inParameters, outParameters, ds, tbl)
            Return ds

        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    

#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "Public Methods"
    public Function SaveRiskTypeTolerance(row As DataRow) As String

        Dim sqlstatement As String
        Dim rowState As DataRowState = row.RowState
        Dim updatedby As string
        Try
         Select Case rowState
                    Case DataRowState.Added
                        'Insert
                        sqlstatement = Config("/SQL/INSERT")
                        updatedby= COL_NAME_CREATED_BY
                    Case DataRowState.Deleted
                        'delete
                        sqlstatement = Config("/SQL/DELETE")
                    Case DataRowState.Modified
                        'update
                        sqlstatement = Config("/SQL/UPDATE")
                        updatedby= COL_NAME_MODIFIED_BY
            End Select

            Dim outputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {
                        New DBHelper.DBHelperParameter(PARAM_CRUD_STATUS, GetType(Integer))
                    }
            
           If rowState = DataRowState.Deleted Then
               Dim inParameter() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                       {
                           New DBHelper.DBHelperParameter( PAR_NAME_DLR_RK_TYP_TOLERANCE_ID.ToLower(),   row(COL_NAME_DLR_RK_TYP_TOLERANCE_ID,DataRowVersion.Original))
                       }
               DBHelper.ExecuteSp(sqlstatement, inParameter, outputParameters)
               row.AcceptChanges()
           Else 
               Dim inParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                       {New DBHelper.DBHelperParameter( PAR_NAME_DLR_RK_TYP_TOLERANCE_ID, row(COL_NAME_DLR_RK_TYP_TOLERANCE_ID)),
                        New DBHelper.DBHelperParameter( PAR_NAME_DEALER, row(COL_NAME_DEALER_ID)),
                        New DBHelper.DBHelperParameter( PAR_NAME_RISK_TYPE_ID, row(COL_NAME_RISK_TYPE_ID)),
                        New DBHelper.DBHelperParameter( PAR_NAME_TOLERANCE_PCT, row(COL_NAME_TOLERANCE_PCT)),
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
