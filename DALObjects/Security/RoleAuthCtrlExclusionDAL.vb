Imports System.Text

Public Class RoleAuthCtrlExclusionDAL
    Inherits DALBase

#Region "Constants"
    Public Const TABLE_NAME As String = "ELP_ROLE_AUTH_CTRL_EXCLUSION"
    Public Const TABLE_KEY_NAME As String = "auth_ctrl_id"

    Public Const COL_NAME_ROLE_ID As String = "role_id"
    Public Const COL_NAME_FORM_ID As String = "form_id"
    Public Const COL_NAME_AUTH_CTRL_ID As String = "auth_ctrl_id"
    Public Const COL_NAME_CONTROL_NAME As String = "control_name"
    Public Const COL_NAME_PERMISSION_TYPE As String = "permission_type"

    Public Const COL_NAME_LANGUAGE_ID As String = "LANGUAGE_ID"
    Private Const ID_COLUMN_FORMID As Integer = 0
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
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() {New DBHelper.DBHelperParameter("auth_ctrl_id", id.ToByteArray)}
        Try
            DBHelper.Fetch(familyDS, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Public Sub Load(familyDS As DataSet, oFormId As Guid, oRoleId As Guid, sControlName As String)
        Dim selectStmt As String = Config("/SQL/LOAD_FORM_ROLE_CONTROL")
        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
            {New DBHelper.DBHelperParameter(COL_NAME_FORM_ID, oFormId.ToByteArray), _
             New DBHelper.DBHelperParameter(COL_NAME_ROLE_ID, oRoleId.ToByteArray), _
             New DBHelper.DBHelperParameter(COL_NAME_CONTROL_NAME, sControlName)}
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

    Public Function PopulateList(oLanguageID As Guid) As DataSet
        Dim selectStmt As String = Config("/SQL/GET_TABS_FORMS")
        Dim ds As New DataSet

        Dim parameters() As DBHelper.DBHelperParameter = New DBHelper.DBHelperParameter() _
                    {New DBHelper.DBHelperParameter(COL_NAME_LANGUAGE_ID, oLanguageID.ToByteArray) _
                    }
        Try
            DBHelper.Fetch(ds, selectStmt, TABLE_NAME, parameters)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function

    Private Sub SortTable(oFromTable As DataTable, oToDataView As DataView)
        Dim oToTable As DataTable
        Dim oHeaders As DataColumnCollection = oFromTable.Columns
        Dim oHeader As DataColumn
        Dim oFromRow, oToRow As DataRow
        Dim oRowArray() As DataRow
        Dim sSort As String
        Dim nIndex, nLength As Integer

        oToTable = New DataTable("Controls per Form")
        oToDataView.Table = oToTable
        ' Headers
        'For Each oHeader In oHeaders
        '    oToTable.Columns.Add(oHeader.ToString)
        'Next
        nLength = oHeaders.Count - 1
        For nIndex = 0 To nLength
            oHeader = oHeaders(nIndex)
            If oFromTable.Rows(0)(nIndex).GetType Is GetType(Byte()) Then
                oToTable.Columns.Add(oHeader.ToString, GetType(Byte()))
            Else
                oToTable.Columns.Add(oHeader.ToString)
            End If
        Next

        sSort = oToTable.Columns(ID_COLUMN_FORMID).ToString
        oRowArray = oFromTable.Select(Nothing, sSort, DataViewRowState.CurrentRows)
        ' Body
        'For Each oFromRow In oRowArray
        '    oToRow = oToTable.NewRow
        '    oToRow.ItemArray = oFromRow.ItemArray
        '    oToTable.Rows.Add(oToRow)
        'Next
        For Each oFromRow In oFromTable.Rows
            oToRow = oToTable.NewRow
            For nIndex = 0 To nLength
                If oFromRow(nIndex).GetType Is GetType(Byte()) Then
                    oToRow(nIndex) = CType(oFromRow(nIndex), Byte())
                Else
                    oToRow(nIndex) = oFromRow(nIndex).ToString()
                End If
            Next
            oToTable.Rows.Add(oToRow)
        Next

    End Sub


    Private Sub AppendRows(oFromDataView As DataView, oToTable As DataTable)
        Dim oFromTable As DataTable = oFromDataView.Table
        Dim oHeaders As DataColumnCollection = oFromTable.Columns
        Dim oHeader As DataColumn
        Dim oFromRow, oToRow As DataRow
        Dim nIndex, nLength As Integer

        nLength = oHeaders.Count - 1
        If oToTable.Columns.Count = 0 Then
            ' Headers()
            For nIndex = 0 To nLength
                oHeader = oHeaders(nIndex)
                If oFromTable.Rows(0)(nIndex).GetType Is GetType(Byte()) Then
                    oToTable.Columns.Add(oHeader.ToString, GetType(Byte()))
                Else
                    oToTable.Columns.Add(oHeader.ToString)
                End If
            Next
        End If
        ' Body
        For Each oFromRow In oFromTable.Rows
            oToRow = oToTable.NewRow
            For nIndex = 0 To nLength
                If oFromRow(nIndex).GetType Is GetType(Byte()) Then
                    oToRow(nIndex) = CType(oFromRow(nIndex), Byte())
                Else
                    oToRow(nIndex) = oFromRow(nIndex).ToString()
                End If
            Next
            oToTable.Rows.Add(oToRow)
        Next
    End Sub

    Private Function ObtainAControlPermission(sFormId As String, sControlName As String) As DataSet
        Dim selectStmt As StringBuilder = New StringBuilder(Config("/SQL/GET_CONTROL_PERMISSIONS"))
        Dim ds As New DataSet
        Dim oFormId As Guid

        selectStmt.Replace("#ControlName", sControlName)
        oFormId = New Guid(sFormId)
        selectStmt.Replace("#FormId", GuidControl.GuidToHexString(oFormId))
        Try
            ds = DBHelper.Fetch(selectStmt.ToString, TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

        Return ds
    End Function

    Public Function ObtainControlPermissions(sFormId As String, oControlNames As ArrayList) As DataView
        Dim sControlName As String
        Dim oDataView As DataView
        Dim oTable As DataTable
        Dim ds As DataSet

        oDataView = New DataView
        oTable = New DataTable
        For Each sControlName In oControlNames
            ds = ObtainAControlPermission(sFormId, sControlName)

            AppendRows(ds.Tables(TABLE_NAME).DefaultView, oTable)
        Next
        SortTable(oTable, oDataView)
        Return oDataView
    End Function

    Public Function GetControlPermissionAllRoles(sFormCode As String, sControlName As String) As DataSet
        Dim selectStmt As StringBuilder = New StringBuilder(Config("/SQL/GET_CONTROL_PERMISSIONS_All_Roles"))
        Dim ds As New DataSet
        Dim oFormId As Guid

        selectStmt.Replace("#ControlName", sControlName)
        selectStmt.Replace("#FormCode", sFormCode)
        Try
            ds = DBHelper.Fetch(selectStmt.ToString, TABLE_NAME)
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
        Return ds
    End Function
#End Region

#Region "Overloaded Methods"
    Public Overloads Sub Update(ds As DataSet, Optional ByVal Transaction As IDbTransaction = Nothing, Optional ByVal changesFilter As DataRowState = Nothing)
        If ds Is Nothing Then
            Return
        End If
        If Not ds.Tables(TABLE_NAME) Is Nothing Then
            MyBase.Update(ds.Tables(TABLE_NAME), Transaction, changesFilter)
        End If
    End Sub
#End Region

End Class
