'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/15/2017)********************


Public Class OcTemplateGroupDealerDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_OC_TEMPLATE_GROUP_DEALER"
    Public Const TABLE_KEY_NAME As String = "oc_template_group_dealer_id"
	
    Public Const COL_NAME_OC_TEMPLATE_GROUP_DEALER_ID As String = "oc_template_group_dealer_id"
    Public Const COL_NAME_OC_TEMPLATE_GROUP_ID As String = "oc_template_group_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"

    Public Const COL_NAME_NUMBER_OF_TEMPLATE_GROUPS As String = "number_of_template_groups"
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
        Dim selectStmt As String = Config("/SQL/LOAD")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("oc_template_group_dealer_id", id.ToByteArray)}
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

    Public Function LoadList(templateGroupId As Guid) As DataSet
        Dim ds As New DataSet
        LoadList(ds, templateGroupId)
        Return ds
    End Function

    Public Sub LoadList(ds As DataSet, templateGroupId As Guid)
        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_TEMPLATE_GROUP_ID")
        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter(COL_NAME_OC_TEMPLATE_GROUP_ID, templateGroupId.ToByteArray)})
    End Sub

    Public Function GetAssociatedTemplateGroupCount(dealerId As Guid, templateGroupIdToExcludeFromCount As Guid)
        Try
            Dim selectStmt As String = Config("/SQL/GET_ASSOCIATED_TEMPLATE_GROUP_COUNT")
            Dim ds As New DataSet
            Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {
                New DBHelper.DBHelperParameter("dealerId", dealerId.ToByteArray),
                New DBHelper.DBHelperParameter("oc_template_group_id", templateGroupIdToExcludeFromCount.ToByteArray)
            }
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
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
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class


