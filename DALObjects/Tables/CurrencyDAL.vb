'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (10/21/2004)********************


Public Class CurrencyDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_CURRENCY"
    Public Const TABLE_KEY_NAME As String = "currency_id"

    Public Const COL_NAME_CURRENCY_ID As String = "currency_id"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_NOTATION As String = "notation"
    Public Const COL_NAME_ISO_CODE As String = "iso_code"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("currency_id", id.ToByteArray)}
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

    Public Function LoadList(strCode As String, strDec As String, strNotation As String, strISOCode As String) As DataSet
        Dim selectStmt As String = Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""

        If FormatSearchMask(strCode) Then
            whereClauseConditions &= " WHERE " & Environment.NewLine & "UPPER(" & COL_NAME_CODE & ") " & strCode.ToUpper
        End If


        If FormatSearchMask(strDec) Then
            If whereClauseConditions = "" Then
                whereClauseConditions &= " WHERE " & Environment.NewLine & "UPPER(" & COL_NAME_DESCRIPTION & ") " & strDec.ToUpper
            Else
                whereClauseConditions &= Environment.NewLine & "AND  UPPER(" & COL_NAME_DESCRIPTION & ") " & strDec.ToUpper
            End If
        End If

        If FormatSearchMask(strNotation) Then
            If whereClauseConditions = "" Then
                whereClauseConditions &= " WHERE " & Environment.NewLine & "UPPER(" & COL_NAME_NOTATION & ") " & strNotation.ToUpper
            Else
                whereClauseConditions &= Environment.NewLine & "AND  UPPER(" & COL_NAME_NOTATION & ") " & strNotation.ToUpper
            End If
        End If

        If FormatSearchMask(strISOCode) Then
            If whereClauseConditions = "" Then
                whereClauseConditions &= " WHERE " & Environment.NewLine & "UPPER(" & COL_NAME_ISO_CODE & ") " & strISOCode.ToUpper
            Else
                whereClauseConditions &= Environment.NewLine & "AND  UPPER(" & COL_NAME_ISO_CODE & ") " & strISOCode.ToUpper
            End If
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Return (DBHelper.Fetch(selectStmt, TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return DBHelper.Fetch(selectStmt, TABLE_NAME)
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
    End Sub
#End Region


End Class


