Public Class OcMessageParamsDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_OC_MESSAGE_PARAMS"
    Public Const TABLE_KEY_NAME As String = "oc_message_params_id"

    Public Const COL_NAME_OC_MESSAGE_PARAMS_ID As String = "oc_message_params_id"
    Public Const COL_NAME_OC_MESSAGE_ID As String = "oc_message_id"
    Public Const COL_NAME_PARAM_NAME As String = "param_name"
    Public Const COL_NAME_PARAM_VALUE As String = "param_value"
    Public Const COL_NAME_CREATED_DATE As String = "created_date"
    Public Const COL_NAME_CREATED_BY As String = "created_by"

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
        Dim selectStmt As String = Me.Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
        {
        New DBHelper.DBHelperParameter("oc_message_params_id", id.ToByteArray)
        }

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

    Public Function LoadList(ByVal messageId As Guid) As DataSet
        Dim ds As New DataSet
        LoadList(ds, messageId)
        Return ds
    End Function

    Public Sub LoadList(ByVal ds As DataSet, ByVal messageId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_BY_MESSAGE_ID")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {
            New DBHelper.DBHelperParameter(COL_NAME_OC_MESSAGE_ID, messageId.ToByteArray)
            }

        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
    End Sub
#End Region

End Class
