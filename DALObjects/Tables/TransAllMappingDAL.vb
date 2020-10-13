'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (1/30/2009)********************


Public Class TransAllMappingDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_TRANSALL_MAPPING"
    Public Const TABLE_KEY_NAME As String = "transall_mapping_id"

    Public Const COL_NAME_TRANSALL_MAPPING_ID As String = "transall_mapping_id"
    Public Const COL_NAME_DEALER_ID As String = "dealer_id"
    Public Const COL_NAME_INBOUND_FILENAME As String = "inbound_filename"
    Public Const COL_NAME_OUTBOUND_FILENAME_REGEX As String = "outbound_filename_regex"
    Public Const COL_NAME_OUTPUT_PATH As String = "output_path"
    Public Const COL_NAME_TRANSALL_PACKAGE As String = "transall_package"
    Public Const COL_NAME_LOGFILE_EMAILS As String = "logfile_emails"
    Public Const COL_NAME_NUM_FILES As String = "num_files"
    Public Const COL_NAME_LAYOUT_CODE As String = "layout_code_id"
    Public Const COL_NAME_FTP_SITE_ID As String = "ftp_site_id"

    'For getlist method
    Public Const COL_NAME_DEALER_NAME As String = "dealer_name"

    Public Const PAR_FILE_NAME As String = "file_name"
    Public Const PAR_DEALER_ID As String = "dealer_id"
    Public Const PAR_COMPANY_ID As String = "company_id"
    Public Const PAR_OUTPUT_PATH As String = "output_path"

    Public Const PAR_HEADER As String = "P_HEADER"
    Public Const PAR_TYPE As String = "P_TYPE"
    Public Const PAR_CODE As String = "P_CODE"
    Public Const PAR_MACHINE_NAME As String = "P_MACHINE_NAME"
    Public Const PAR_APP_NAME As String = "P_APP_NAME"
    Public Const PAR_USER_NAME As String = "P_USER_NAME"
    Public Const PAR_EXTENDED_CONTENT As String = "P_EXTENDED_CONTENT"
    Public Const PAR_EXTENDED_CONTENT2 As String = "P_EXTENDED_CONTENT2"
    Public Const PAR_GENERATION_TIME As String = "P_GENERATION_TIME"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("transall_mapping_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Function LoadList(DealerId As Guid, CompanyIds As ArrayList) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST")

        Dim whereClauseConditions As String = ""

        If Not DealerId.ToString.Equals(Guid.Empty.ToString) Then
            whereClauseConditions &= String.Format(" AND tm.dealer_id = HEXTORAW('{0}')", GuidControl.GuidToHexString(DealerId))
        End If

        If CompanyIds IsNot Nothing Then
            whereClauseConditions &= MiscUtil.BuildListForSql(" AND d." & PAR_COMPANY_ID, CompanyIds, True)
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Try
            Return DBHelper.Fetch(selectStmt, TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadList(FileName As String, CompanyIds As ArrayList) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_FILE")
        Dim parameters As DBHelper.DBHelperParameter()
        Dim whereClauseConditions As String = ""

        parameters = New DBHelper.DBHelperParameter() { _
                       New DBHelper.DBHelperParameter(PAR_FILE_NAME, FileName)}

        If CompanyIds IsNot Nothing Then
            whereClauseConditions &= MiscUtil.BuildListForSql("elp_dealer." & PAR_COMPANY_ID, CompanyIds, True)
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim ds As New DataSet

        Try
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadListByDirectory(DirectoryName As String, CompanyIds As ArrayList) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_DIRECTORY")
        Dim parameters As DBHelper.DBHelperParameter()
        Dim whereClauseConditions As String = ""

        parameters = New DBHelper.DBHelperParameter() { _
                       New DBHelper.DBHelperParameter(PAR_OUTPUT_PATH, DirectoryName)}

        If CompanyIds IsNot Nothing Then
            whereClauseConditions &= MiscUtil.BuildListForSql("elp_dealer." & PAR_COMPANY_ID, CompanyIds, True)
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim ds As New DataSet

        Try
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
    Public Function LoadListByDirectoryAndFileName(DirectoryName As String, partialFileName As String, CompanyIds As ArrayList) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_DIRECTORY_FILENAME")
        Dim parameters As DBHelper.DBHelperParameter()
        Dim whereClauseConditions As String = ""
        partialFileName = partialFileName & "%"
        parameters = New DBHelper.DBHelperParameter() { _
                       New DBHelper.DBHelperParameter(PAR_OUTPUT_PATH, DirectoryName), _
                       New DBHelper.DBHelperParameter("inbound_filename", partialFileName.ToUpper().ToString())}

        If CompanyIds IsNot Nothing Then
            whereClauseConditions &= MiscUtil.BuildListForSql("elp_dealer." & PAR_COMPANY_ID, CompanyIds, True)
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim ds As New DataSet

        Try
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadListByOutputDirectoryAndFileName(DirectoryName As String, FileName As String, CompanyIds As ArrayList) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_OUTPUT_DIRECTORY_FILENAME")
        Dim parameters As DBHelper.DBHelperParameter()
        Dim whereClauseConditions As String = ""

        parameters = New DBHelper.DBHelperParameter() { _
                       New DBHelper.DBHelperParameter(PAR_OUTPUT_PATH, DirectoryName), _
                       New DBHelper.DBHelperParameter("inbound_filename", FileName.ToUpper().ToString())}

        If CompanyIds IsNot Nothing Then
            whereClauseConditions &= MiscUtil.BuildListForSql("elp_dealer." & PAR_COMPANY_ID, CompanyIds, True)
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim ds As New DataSet

        Try
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Function LoadListByOutputDirectory(DirectoryName As String, CompanyIds As ArrayList) As DataSet

        Dim selectStmt As String = Config("/SQL/LOAD_LIST_BY_OUTPUT_DIRECTORY")
        Dim parameters As DBHelper.DBHelperParameter()
        Dim whereClauseConditions As String = ""

        parameters = New DBHelper.DBHelperParameter() { _
                       New DBHelper.DBHelperParameter(PAR_OUTPUT_PATH, DirectoryName)}

        If CompanyIds IsNot Nothing Then
            whereClauseConditions &= MiscUtil.BuildListForSql("elp_dealer." & PAR_COMPANY_ID, CompanyIds, True)
        End If

        If Not whereClauseConditions = "" Then
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Else
            selectStmt = selectStmt.Replace(DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, "")
        End If

        Dim ds As New DataSet

        Try
            Return DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
#End Region



    Public Sub LogTransallErrors(region As String, filename As String, code As String, log_details As String, created_by As String)
        Dim sqlStmt As String = Config("/SQL/TRANSALL_LOG")
        Dim inputParameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() { _
                          New DBHelper.DBHelperParameter("p_region", region), _
                          New DBHelper.DBHelperParameter("p_file_name", filename), _
                          New DBHelper.DBHelperParameter("p_code", code), _
                          New DBHelper.DBHelperParameter("p_log_details", log_details), _
                          New DBHelper.DBHelperParameter("p_created_by", created_by)}

        Try
            DBHelper.ExecuteSp(sqlStmt, inputParameters, Nothing)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Sub
   

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



