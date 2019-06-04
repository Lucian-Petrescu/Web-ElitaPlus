'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/13/2009)********************


Public Class LanguageDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_LANGUAGE"
    Public Const TABLE_KEY_NAME As String = "language_id"

    Public Const COL_NAME_LANGUAGE_ID As String = "language_id"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_CODE As String = "code"
    Public Const COL_NAME_CULTURE_CODE As String = "culture_code"
    Public Const COL_NAME_ACTIVE_FLAG As String = "active_flag"
    Public Const COL_NAME_TERRITORY As String = "territory"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("language_id", id.ToByteArray)}
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

    Public Function LoadList(ByVal strCode As String, ByVal strDec As String, ByVal strCulture As String, ByVal strTerritory As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        'Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""

        If Me.FormatSearchMask(strCode) Then
            whereClauseConditions &= " WHERE " & Environment.NewLine & "UPPER(" & Me.COL_NAME_CODE & ") " & strCode.ToUpper
        End If


        If Me.FormatSearchMask(strDec) Then
            If whereClauseConditions = "" Then
                whereClauseConditions &= " WHERE " & Environment.NewLine & "UPPER(" & Me.COL_NAME_DESCRIPTION & ") " & strDec.ToUpper
            Else
                whereClauseConditions &= Environment.NewLine & "AND  UPPER(" & Me.COL_NAME_DESCRIPTION & ") " & strDec.ToUpper
            End If
        End If

        If Me.FormatSearchMask(strCulture) Then
            If whereClauseConditions = "" Then
                whereClauseConditions &= " WHERE " & Environment.NewLine & "UPPER(" & Me.COL_NAME_CULTURE_CODE & ") " & strCulture.ToUpper
            Else
                whereClauseConditions &= Environment.NewLine & "AND  UPPER(" & Me.COL_NAME_CULTURE_CODE & ") " & strCulture.ToUpper
            End If
        End If

        If Me.FormatSearchMask(strTerritory) Then
            If whereClauseConditions = "" Then
                whereClauseConditions &= " WHERE " & Environment.NewLine & "UPPER(" & Me.COL_NAME_TERRITORY & ") " & strTerritory.ToUpper
            Else
                whereClauseConditions &= Environment.NewLine & "AND  UPPER(" & Me.COL_NAME_TERRITORY & ") " & strTerritory.ToUpper
            End If
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
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


