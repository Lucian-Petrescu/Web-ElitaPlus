'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (8/15/2017)********************
Public Class OcTemplateRecipientDAL
    Inherits DALBase
#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_OC_TEMPLATE_RECIPIENT"
    Public Const TABLE_KEY_NAME As String = "oc_template_recipient_id"

    Public Const COL_NAME_OC_TEMPLATE_RECIPIENT_ID As String = "oc_template_recipient_id"
    Public Const COL_NAME_OC_TEMPLATE_ID As String = "oc_template_id"
    Public Const COL_NAME_RECIPIENT_SOURCE_FIELD_XCD As String = "recipient_source_field_xcd"
    Public Const COL_NAME_RECIPIENT_SOURCE_FIELD_DESCRIPTION As String = "recipient_source_field_desc"
    Public Const COL_NAME_RECIPIENT_ADDRESS As String = "recipient_address"
    Public Const COL_NAME_DESCRIPTION As String = "description"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("oc_template_recipient_id", id.ToByteArray)}
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

    Public Function LoadList(ByVal templateId As Guid, ByVal languageId As Guid) As DataSet
        Dim ds As New DataSet
        LoadList(ds, templateId, languageId)
        Return ds
    End Function

    Public Sub LoadList(ByVal ds As DataSet, ByVal templateId As Guid, ByVal languageId As Guid)
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST_BY_TEMPLATE_ID")

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {
            New DBHelper.DBHelperParameter("language_id", languageId.ToByteArray),
            New DBHelper.DBHelperParameter(COL_NAME_OC_TEMPLATE_ID, templateId.ToByteArray)
            }

        DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
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


