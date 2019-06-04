'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE DALObject.cst (3/22/2006)********************


Public Class InterfaceStatusWrkDAL
    Inherits DALBase


#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_INTERFACE_STATUS_WRK"
    Public Const TABLE_KEY_NAME As String = "interface_status_id"

    Public Const COL_NAME_INTERFACE_STATUS_ID As String = "interface_status_id"
    Public Const COL_NAME_DESCRIPTION As String = "description"
    Public Const COL_NAME_STATUS As String = "status"
    Public Const COL_NAME_ACTIVE_FILENAME As String = "active_filename"
    Public Const COL_NAME_SESSIONPADDRID As String = "sessionpaddrid'"
    Public Const COL_NAME_ERROR_MESSAGE As String = "error_message"

    'REQ-1056
    Public Const COL_NAME_CREATED_DATE As String = "created_date"
    'END REQ-1056

    Public Const DESC_VALIDATE As String = "Validate"
    Public Const DESC_PROCESS As String = "Process"
    Public Const DESC_ENRRES As String = "Response"
    Public Const DESC_DELETE As String = "Delete"
    Public Const DESC_SPLIT As String = "Split"
    Public Const DESC_DOWNLOAD As String = "Download"
    Public Const DESC_VSC_PROCESS As String = "Vsc Process"

    Public Const STATUS_PENDING As String = "Pending"
    Public Const STATUS_RUNNING As String = "Running"
    Public Const STATUS_FAILURE As String = "Failure"
    Public Const STATUS_SUCCESS As String = "Success"

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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("interface_status_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, Me.TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
    Public Function Load_IsStatus_Running(ByVal id As Guid) As DataSet
        Dim selectstmt As String = Me.Config("/SQL/ISSTATUS_RUNNING")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("file_id", id.ToByteArray)}
        Try
            Dim Ds As DataSet = New DataSet()
            DBHelper.Fetch(Ds, selectstmt, Me.TABLE_NAME, parameters)
            Return Ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function
 
    Public Function LoadByActiveFileName(ByVal activefilename As String, Optional ByVal parentFile As Boolean = False) As DataSet
        Dim selectStmt As String
        Dim parameters() As DBHelper.DBHelperParameter
        If parentFile = False Then
            selectStmt = Me.Config("/SQL/GET_ACTIVE_PROCESS")
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("activefilename", activefilename)}
        Else
            selectStmt = Me.Config("/SQL/GET_ACTIVE_PROCESS_PARENTFILE")
            parameters = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("activefilename", activefilename), _
                                                           New DBHelper.DBHelperParameter("activefilename", activefilename)}
        End If

        Try
            Dim ds As New DataSet

            DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters)
            Return ds
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    'REQ-1056
    Public Function LoadList(ByVal activefilename As String) As DataSet
        Dim selectStmt As String = Me.Config("/SQL/LOAD_LIST")
        Dim whereClauseConditions As String = ""

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("activefilename", activefilename)}

        If (activefilename = "*") Then
            whereClauseConditions &= "where active_filename is not null and status='Running'"
        Else
            whereClauseConditions &= "where active_filename='" & activefilename & "'and status='Running'"
        End If
        selectStmt = selectStmt.Replace(Me.DYNAMIC_WHERE_CLAUSE_PLACE_HOLDER, whereClauseConditions)
        Try
            Dim ds As New DataSet
            Return (DBHelper.Fetch(ds, selectStmt, Me.TABLE_NAME, parameters))
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function
    'END REQ-1056


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



