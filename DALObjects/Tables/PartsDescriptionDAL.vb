'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/18/2005)********************


Public Class PartsDescriptionDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_PARTS_DESCRIPTION"
    Public Const TABLE_NAME_WS As String = "PARTS_DESCRIPTION"
    Public Const TABLE_KEY_NAME As String = "parts_description_id"

    Public Const COL_NAME_PARTS_DESCRIPTION_ID As String = "parts_description_id"
    Public Const COL_NAME_COMPANY_GROUP_ID As String = "company_group_id"
    Public Const COL_NAME_RISK_GROUP_ID As String = "risk_group_id"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_DESCRIPTION_ENGLISH As String = "description_english"
    Public Const COL_NAME_RISK_GROUP As String = "risk_group"
    Public Const COL_NAME_CODE As String = "code"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("parts_description_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    'Public Function LoadList() As DataSet
    '    Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
    '    Return DBHelper.Fetch(selectStmt, Me.TABLE_NAME)
    'End Function

    Public Function LoadList(ByVal compGrpId As Guid, ByVal langID As Guid, ByVal riskGroupID As Guid, ByVal description As String) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")

        selectStmt &= Environment.NewLine & " AND parts.company_group_id = '" & Me.GuidToSQLString(compGrpId) & "'"
        selectStmt &= Environment.NewLine & " AND trans.language_id = '" & Me.GuidToSQLString(langID) & "'"

        If Not riskGroupID.Equals(Guid.Empty) Then
            selectStmt &= Environment.NewLine & " AND parts.risk_group_id = '" & Me.GuidToSQLString(riskGroupID) & "'"
        End If

        If ((Not (description Is Nothing)) AndAlso (Me.FormatSearchMask(description))) Then
            selectStmt &= Environment.NewLine & " AND UPPER(parts.description)" & description.ToUpper
        End If

        selectStmt &= Environment.NewLine & " ORDER BY Risk_Group"
        Try
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadAssignedList(ByVal compGrpId As Guid, ByVal langID As Guid) As DataSet

        Dim selectStmt As String = Me.Config("/SQL/LOAD_ASSIGNED_LIST")

        selectStmt &= Environment.NewLine & " AND parts.company_group_id = '" & Me.GuidToSQLString(compGrpId) & "'"
        selectStmt &= Environment.NewLine & " AND trans.language_id = '" & Me.GuidToSQLString(langID) & "'"

        selectStmt &= Environment.NewLine & " ORDER BY Risk_Group"
        Try
            Return (DBHelper.Fetch(selectStmt, Me.TABLE_NAME))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function GetNextCode(ByVal compGrpId As Guid) As String
        Dim selectStmt As String = Me.Config("/SQL/MAX_CODE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("company_group_id", compGrpId.ToByteArray)}
        Dim retVal As String

        Try
            Dim ds As DataSet = New DataSet

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

            If ds.Tables(Me.TABLE_NAME).Rows.Count = 1 AndAlso Not ds.Tables(Me.TABLE_NAME).Rows(0)(0) Is DBNull.Value Then
                retVal = CType(CType(ds.Tables(Me.TABLE_NAME).Rows(0)(0), Integer) + 1, String)
            Else
                retVal = Nothing
            End If

            Return retVal
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function IsValidCode(ByVal compGrpId As Guid, ByVal code As String) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/CODE_VALIDATION")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("company_group_id", compGrpId.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("code", code)}
        Dim retVal As Boolean

        Try
            Dim ds As DataSet = New DataSet

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

            If ds.Tables(Me.TABLE_NAME).Rows.Count = 1 AndAlso ds.Tables(Me.TABLE_NAME).Rows(0)(0) = 1 Then
                retVal = True
            Else
                retVal = False
            End If

            Return retVal
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetPartDescriptionByCode(ByVal compGrpId As Guid, ByVal code As String) As Guid
        Dim selectStmt As String = Me.Config("/SQL/GET_PARTS_DESCRIPTION_BY_CODE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("company_group_id", compGrpId.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("code", code)}
        Dim retVal As Guid

        Try
            Dim ds As DataSet = New DataSet

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

            If ds.Tables(Me.TABLE_NAME).Rows.Count = 1 Then
                retVal = New Guid(CType(ds.Tables(Me.TABLE_NAME).Rows(0)(0), Byte()))
            Else
                retVal = Guid.Empty
            End If

            Return retVal
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function GetPartDescriptionByCode(ByVal compGrpId As Guid, ByVal code As String, ByVal ClaimId As Guid) As Guid
        Dim selectStmt As String = Me.Config("/SQL/GET_PARTS_DESCRIPTION_BY_CODE_AND_RISK_GROUP")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("company_group_id", compGrpId.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("code", code), _
                                                                                           New DBHelper.DBHelperParameter("company_group_id", compGrpId.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("Claim_id", ClaimId.ToByteArray)}
        Dim retVal As Guid

        Try
            Dim ds As DataSet = New DataSet

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

            If ds.Tables(Me.TABLE_NAME).Rows.Count = 1 Then
                retVal = New Guid(CType(ds.Tables(Me.TABLE_NAME).Rows(0)(0), Byte()))
            Else
                retVal = Guid.Empty
            End If

            Return retVal
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function IsPartCodeUnique(ByVal riskgrpId As Guid, ByVal compGrpId As Guid, ByVal code As String) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/IS_PART_CODE_UNIQUE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("risk_group_id", riskgrpId.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("company_group_id", compGrpId.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("code", code)}

        Try
            Dim ds As DataSet = New DataSet

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

            If ds.Tables(Me.TABLE_NAME).Rows.Count >= 1 Then
                Return False
            Else
                Return True
            End If


        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Function IsEnglishDescriptionUnique(ByVal riskgrpId As Guid, ByVal compGrpId As Guid, ByVal description_english As String) As Boolean
        Dim selectStmt As String = Me.Config("/SQL/IS_ENGLISH_DESCRIPTION_UNIQUE")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("risk_group_id", riskgrpId.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("company_group_id", compGrpId.ToByteArray), _
                                                                                           New DBHelper.DBHelperParameter("description_english", Description_English)}

        Try
            Dim ds As DataSet = New DataSet

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)

            If ds.Tables(Me.TABLE_NAME).Rows.Count >= 1 Then
                Return False
            Else
                Return True
            End If


        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function



#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ByVal ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If Not ds.Tables(Me.TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region


End Class



