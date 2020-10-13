'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/15/2017)********************
Public Class OcTemplateParamsDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_OC_TEMPLATE_PARAMS"
    Public Const TABLE_KEY_NAME As String = "oc_template_params_id"

    Public Const COL_NAME_OC_TEMPLATE_PARAMS_ID As String = "oc_template_params_id"
    Public Const COL_NAME_OC_TEMPLATE_ID As String = "oc_template_id"
    Public Const COL_NAME_PARAM_NAME As String = "param_name"
    Public Const COL_NAME_PARAM_VALUE_SOURCE_XCD As String = "param_value_source_xcd"
    Public Const COL_NAME_PARAM_VALUE_SOURCE_DESCRIPTION As String = "param_value_source_description"
    Public Const COL_NAME_PARAM_VALUE As String = "param_value"
    Public Const COL_NAME_PARAM_TYPE As String = "param_type"
    Public Const COL_NAME_PARAM_DATA_TYPE_XCD As String = "param_data_type_xcd"
    Public Const COL_NAME_PARAM_DATA_TYPE_DESCRIPTION As String = "param_data_type_description"
    Public Const COL_NAME_DATE_FORMAT_STRING As String = "date_format_string"
    Public Const COL_NAME_ALLOW_EMPTY_VALUE_XCD As String = "allow_empty_value_xcd"
    Public Const COL_NAME_ALLOW_EMPTY_VALUE_DESCRIPTION As String = "allow_empty_value_description"
#End Region

#Region "Constructors"
    Public Sub New()
        MyBase.New()
    End Sub

#End Region

#Region "Load Methods"
    Public Sub LoadSchema(ds As DataSet)
        Load(ds, Guid.Empty)
    End Sub

    Public Sub Load(familyDS As DataSet, id As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("oc_template_params_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList() As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function

    Public Function LoadList(templateId As Guid, languageId As Guid) As DataSet
        Dim ds As New DataSet
        LoadList(ds, templateId, languageId)
        Return ds
    End Function

    Public Sub LoadList(ds As DataSet, templateId As Guid, languageId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_TEMPLATE_ID")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {
            New DBHelper.DBHelperParameter("language_id", languageId.ToByteArray),
            New DBHelper.DBHelperParameter("language_id", languageId.ToByteArray),
            New DBHelper.DBHelperParameter("language_id", languageId.ToByteArray),
            New DBHelper.DBHelperParameter(COL_NAME_OC_TEMPLATE_ID, templateId.ToByteArray)
            }

        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
    End Sub
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region
End Class


