'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/14/2007)********************


Public Class VSCClassCodeDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_VSC_CLASS_CODE"
    Public Const TABLE_KEY_NAME As String = "class_code_id"

    Public Const COL_NAME_CLASS_CODE_ID As String = "class_code_id"
    Public Const COL_NAME_CODE As String = "class_code"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_ACTIVE As String = "active"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("class_code_id", id.ToByteArray)}
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

    Public Function LoadList(ByVal codeidMask As Guid, _
                                    ByVal ActiveMask As Guid, _
                                    ByVal company_group_Id As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim parameters() As DBHelper.DBHelperParameter
        Dim whereClauseConditions As String = ""
        Dim bIsLikeClause As Boolean = False

        If Not codeidMask.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND  UPPER(" & Me.COL_NAME_CLASS_CODE_ID & ") ='" & Me.GuidToSQLString(codeidMask) & "'"
        End If


        If Not ActiveMask.Equals(Guid.Empty) Then
            whereClauseConditions &= Environment.NewLine & "AND UPPER(" & Me.COL_NAME_ACTIVE & ") ='" & Me.GuidToSQLString(ActiveMask) & "'"
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If


        parameters = New DBHelper.DBHelperParameter() _
                                              {New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, company_group_Id.ToByteArray)}



        Dim ds As New DataSet
        Try
            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
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


End Class


