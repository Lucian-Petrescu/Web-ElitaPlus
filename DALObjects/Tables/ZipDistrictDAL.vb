'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/22/2004)********************


Public Class ZipDistrictDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ZIP_DISTRICT"
    Public Const TABLE_KEY_NAME As String = "zip_district_id"

    Public Const COL_NAME_ZIP_DISTRICT_ID As String = "zip_district_id"
    'Public Const COL_NAME_COMPANY_ID As String = "company_id"
    'Public Const COL_NAME_COMPANY_ID1 As String = "companyid"
    Public Const COL_NAME_COUNTRY_ID As String = "country_id"
    Public Const COL_NAME_COUNTRY_ID1 As String = "countryid"
    Public Const COL_NAME_COUNTRY_DESC As String = "country_description"
    Public Const COL_NAME_SHORT_DESC As String = "short_desc"
    Public Const COL_NAME_DESCRIPTION As String = "description"

    Public Const TOTAL_PARAM_BATCH_INSERT = 5
    Public Const TOTAL_PARAM_BATCH_DELETE = 0
    Public Const ZIP_DISTRICT_ID = 0
    '  Public Const COMPANY_ID = 1
    Public Const COUNTRY_ID = 1
    Public Const SHORT_DESC = 2
    Public Const DESCRIPTION = 3
    Public Const ZIP_CODE_LOW_VALUE = 4
    Public Const ZIP_CODE_HIGH_VALUE = 5

    Public Const COL_NAME_ZIP_CODE_LOW_VALUE As String = "zip_code_Low_Value"
    Public Const COL_NAME_ZIP_CODE_HIGH_VALUE As String = "zip_code_High_Value"
    Public Const COL_NAME_RETURN As String = "return_code"
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("zip_district_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(oCountryIds As ArrayList, searchCode As String, searchDesc As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""

        whereClauseConditions &= Environment.NewLine & " AND " & MiscUtil.BuildListForSql("c." & COL_NAME_COUNTRY_ID, oCountryIds, False)
        If FormatSearchMask(searchCode) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(" & COL_NAME_SHORT_DESC & ") " & searchCode.ToUpper
        End If
        If FormatSearchMask(searchDesc) Then
            whereClauseConditions &= " AND " & Environment.NewLine & "UPPER(z." & COL_NAME_DESCRIPTION & ") " & searchDesc.ToUpper
        End If
        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If
                              
        Try
            Dim ds As New DataSet
            ds = DBHelper.Fetch(selectStmt, TABLE_NAME)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function


#End Region

#Region "Overloaded Methods"
    'This method was added manually to accommodate BO families Save
    Public Overloads Sub UpdateFamily(familyDataset As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing)
        Dim detailDAL As New ZipDistrictDetailDAL
        Dim tr As IDbTransaction = Transaction
        If tr Is Nothing Then
            tr = DBHelper.GetNewTransaction
        End If
        Try
            'First Pass updates Deletions
            detailDAL.Update(familyDataset, tr, DataRowState.Deleted)
            MyBase.Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Deleted)

            'Second Pass updates additions and changes
            Update(familyDataset.Tables(TABLE_NAME), tr, DataRowState.Added Or DataRowState.Modified)
            detailDAL.Update(familyDataset, tr, DataRowState.Added Or DataRowState.Modified)

            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.Commit(tr)
            End If
        Catch ex As Exception
            If Transaction Is Nothing Then
                'We are the creator of the transaction we shoul commit it  and close the connection
                DBHelper.RollBack(tr)
            End If
            Throw ex
        End Try
    End Sub


    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds.Tables(TABLE_NAME) IsNot Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

#Region "Extended Functionality: Batch Insert in DB"

    Public Function ZDAndDetail_Batch_Insert(zipdistrictid As Guid, oCountryID As Guid, strShort_Desc As String, StrDescription As String, intZipCodeLowValue As Integer, intZipCodeHighValue As Integer) As Integer
        Dim selectStmt As String = Config("/SQL/BATCH_INSERT")
        Dim inputParameters(TOTAL_PARAM_BATCH_INSERT) As DBHelper.DBHelperParameter
        Dim outputParameter(0) As DBHelper.DBHelperParameter
        inputParameters(ZIP_DISTRICT_ID) = New DBHelper.DBHelperParameter(COL_NAME_ZIP_DISTRICT_ID, zipdistrictid)
        '  inputParameters(COMPANY_ID) = New DBHelper.DBHelperParameter(COL_NAME_COMPANY_ID1, companyID)
        inputParameters(COUNTRY_ID) = New DBHelper.DBHelperParameter(COL_NAME_COUNTRY_ID1, oCountryID)
        inputParameters(SHORT_DESC) = New DBHelper.DBHelperParameter(COL_NAME_SHORT_DESC, strShort_Desc)
        inputParameters(DESCRIPTION) = New DBHelper.DBHelperParameter(COL_NAME_DESCRIPTION, StrDescription)
        inputParameters(ZIP_CODE_LOW_VALUE) = New DBHelper.DBHelperParameter(COL_NAME_ZIP_CODE_LOW_VALUE, intZipCodeLowValue, GetType(Integer))
        inputParameters(ZIP_CODE_HIGH_VALUE) = New DBHelper.DBHelperParameter(COL_NAME_ZIP_CODE_HIGH_VALUE, intZipCodeHighValue, GetType(Integer))
        outputParameter(0) = New DBHelper.DBHelperParameter(COL_NAME_RETURN, GetType(Integer))

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)

        Return CType(outputParameter(0).Value, Integer)

    End Function
    Public Function ZDAndDetail_Batch_Delete(zipdistrictid As Guid) As Integer
        Dim selectStmt As String = Config("/SQL/BATCH_DELETE")
        Dim inputParameters(TOTAL_PARAM_BATCH_DELETE) As DBHelper.DBHelperParameter
        Dim outputParameter(0) As DBHelper.DBHelperParameter
        inputParameters(ZIP_DISTRICT_ID) = New DBHelper.DBHelperParameter(COL_NAME_ZIP_DISTRICT_ID, zipdistrictid)
        outputParameter(0) = New DBHelper.DBHelperParameter(COL_NAME_RETURN, GetType(Integer))

        ' Call DBHelper Store Procedure
        DBHelper.ExecuteSp(selectStmt, inputParameters, outputParameter)

        Return CType(outputParameter(0).Value, Integer)

    End Function

#End Region
End Class



