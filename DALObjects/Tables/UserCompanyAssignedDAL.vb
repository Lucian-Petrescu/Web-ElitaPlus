'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/9/2010)********************


Public Class UserCompanyAssignedDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_USER_COMPANY_ASSIGNED"
    Public Const TABLE_KEY_NAME As String = "user_company_assigned_id"

    Public Const COL_NAME_USER_COMPANY_ASSIGNED_ID As String = "user_company_assigned_id"
    Public Const COL_NAME_USER_ID As String = "user_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"

    Public Const COL_NAME_AUTHORIZATION_LIMIT As String = "authorization_limit"
    Public Const COL_NAME_PAYMENT_LIMIT As String = "payment_limit"
    Public const COL_NAME_LIABILITY_OVERRIDE_LIMIT As String = "liability_override_limit"
    Public Const COL_NAME_IS_LOADED As String = "is_loaded"

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
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("user_company_assigned_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    End Function

    Public Sub LoadByUserIdCompanyID(ByVal familyDS As DataSet, ByVal userId As Guid, ByVal companyId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_BY_USERID_COMPANYID")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                    New DBHelper.DBHelperParameter(Me.COL_NAME_USER_ID, userId.ToByteArray), _
                    New DBHelper.DBHelperParameter(Me.COL_NAME_COMPANY_ID, companyId.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
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


End Class


