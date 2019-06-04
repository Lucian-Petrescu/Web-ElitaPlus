Public Class CommPCodeEntityListDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_COMM_P_CODE_ENTITY_LIST"
    Public Const TABLE_KEY_NAME As String = "comm_p_code_entity_id"

    Public Const COL_NAME_COMM_P_CODE_ENTITY_ID As String = "comm_p_code_entity_id"
    Public Const COL_NAME_COMM_P_CODE_ID As String = "comm_p_code_id"
    Public Const COL_NAME_PAYEE_TYPE_ID As String = "payee_type_id"
    Public Const COL_NAME_PAYEE_TYPE As String = "payee_type"
    Public Const COL_NAME_COMM_SCHEDULE_ID As String = "comm_schedule_id"
    Public Const COL_NAME_COMM_SCHEDULE As String = "comm_schedule"

    Public Const COL_NAME_ENTITY_ID As String = "entity_id"
    Public Const COL_NAME_ENTITY As String = "entity"
    Public Const COL_NAME_IS_COMM_FIXED_ID As String = "is_comm_fixed_id"
    Public Const COL_NAME_IS_COMM_FIXED_CODE As String = "is_comm_fixed_code"
    Public Const COL_NAME_IS_COMM_FIXED As String = "is_comm_fixed"
    Public Const COL_NAME_COMMISSION_AMOUNT As String = "commission_amount"
    Public Const COL_NAME_IS_MARKUP_FIXED_ID As String = "is_markup_fixed_id"
    Public Const COL_NAME_IS_MARKUP_FIXED_CODE As String = "is_markup_fixed_code"
    Public Const COL_NAME_IS_MARKUP_FIXED As String = "is_markup_fixed"
    Public Const COL_NAME_MARKUP_AMOUNT As String = "markup_amount"
    'REQ-976
    Public Const COL_NAME_DAYS_TO_Clawback As String = "number_of_days_clawback"
    Public Const COL_NAME_BRANCH_ID As String = "branch_id"
    Public Const COL_NAME_BRANCH_NAME As String = "branch_name"
    'End REQ-976

#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.new()
    End Sub

#End Region

#Region "Load Methods"

    Public Sub LoadSchema(ByVal ds As DataSet)
        Load(ds)
    End Sub

   
    Public Sub Load(ByVal familyDS As DataSet)
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    
#End Region


End Class

