'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (12/21/2006)********************


Public Class EarningCodeDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_EARNING_CODE"
    Public Const TABLE_KEY_NAME As String = "earning_code_id"

    Public Const COL_NAME_EARNING_CODE_ID As String = "earning_code_id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_DESCRIPTION As String = "description"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("earning_code_id", id.ToByteArray)}
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

    Public Function LoadList(descriptionMask As String, codeMask As String, CompanyGroupId As Guid) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""
        Dim parameters() As DBHelper.DBHelperParameter
        Dim ds As New DataSet

        If ((Not (descriptionMask Is Nothing)) AndAlso (FormatSearchMask(descriptionMask))) Then
            whereClauseConditions &= " AND UPPER(" & COL_NAME_DESCRIPTION & ")" & descriptionMask.ToUpper
        End If

        If ((Not (codeMask Is Nothing)) AndAlso (FormatSearchMask(codeMask))) Then
            whereClauseConditions &= Environment.NewLine & " AND UPPER(" & COL_NAME_CODE & ")" & codeMask.ToUpper
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        parameters = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_NAME_COMPANY_GROUP_ID, CompanyGroupId.ToByteArray)}

        DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)

        Return ds

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



