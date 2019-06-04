Imports System.Runtime.CompilerServices

Public Module DALExtension

    <Extension()> _
    Public Function ToLoadExclusionClause(ByVal familtyDs As DataSet, ByVal predicatePrefix As String, ByVal predicateColumnName As String, ByVal dsTableName As String, ByVal dsColumnName As String, _
                                          ByVal dsCompareValue As Guid, ByRef parameters() As DBHelper.DBHelperParameter) As String

        If (dsCompareValue.Equals(Guid.Empty)) Then Return Nothing

        If (familtyDs Is Nothing) Then Return Nothing

        If (dsTableName Is Nothing OrElse dsTableName.Trim.Length = 0) Then
            Throw New ArgumentNullException("dsTableName")
        End If

        If (Not familtyDs.Tables.Contains(dsTableName)) Then
            Return Nothing
        End If

        Return familtyDs.Tables(dsTableName).ToLoadExclusionClause(predicatePrefix, predicateColumnName, dsColumnName, dsCompareValue, parameters)
    End Function

    <Extension()> _
    Public Function ToLoadExclusionClause(ByVal dataTable As DataTable, ByVal predicatePrefix As String, ByVal predicateColumnName As String, ByVal dsColumnName As String, _
                                          ByVal dsCompareValue As Guid, ByRef parameters() As DBHelper.DBHelperParameter) As String

        If (dsCompareValue.Equals(Guid.Empty)) Then Return Nothing

        If (dataTable Is Nothing) Then Return Nothing

        If (predicateColumnName Is Nothing OrElse predicateColumnName.Trim.Length = 0) Then
            Throw New ArgumentNullException("predicateColumnName")
        End If

        If (dsColumnName Is Nothing OrElse dsColumnName.Trim.Length = 0) Then
            Throw New ArgumentNullException("dsColumnName")
        End If

        If (Not dataTable.Columns.Contains(dsColumnName)) Then
            Throw New InvalidOperationException(String.Format("Column Name {0} does not exists in Data Set Table {1}", dsColumnName, dataTable.TableName))
        End If

        If (Not dataTable.Columns.Contains(predicateColumnName)) Then
            Throw New InvalidOperationException(String.Format("Column Name {0} does not exists in Data Set Table {1}", predicateColumnName, dataTable.TableName))
        End If

        If (Not dataTable.Columns.Contains(dsColumnName)) Then
            Throw New InvalidOperationException(String.Format("Column Name {0} does not exists in Data Set Table {1}", dsColumnName, dataTable.TableName))
        End If

        If (predicatePrefix Is Nothing) Then
            predicatePrefix = String.Empty
        Else
            If (predicatePrefix.Trim.Length > 0) Then
                predicatePrefix = String.Concat(predicatePrefix.Trim(), ".")
            Else
                predicatePrefix = String.Empty
            End If
        End If

        Dim sqlString As New System.Text.StringBuilder
        Dim iCnt As Integer = 0
        For Each dr As DataRow In dataTable.Rows
            If (New Guid(DirectCast(dr(dsColumnName), Byte())).Equals(dsCompareValue)) Then
                Dim parameterName As String = String.Format("{0}{1}", predicateColumnName, iCnt.ToString())
                If (iCnt <> 0) Then sqlString.Append(", ")
                sqlString.AppendFormat("hextoraw(:{0})", parameterName)
                ReDim Preserve parameters(parameters.Length)
                parameters(parameters.Length - 1) = New DBHelper.DBHelperParameter(parameterName, New Guid(DirectCast(dr(predicateColumnName), Byte())))
                iCnt = iCnt + 1
            End If
        Next

        If (iCnt = 0) Then Return Nothing
        Return String.Format(" {0} {1}{2} not in ({3})", "and", predicatePrefix, predicateColumnName, sqlString.ToString())
    End Function



    <Extension()> _
    Public Function ToSqlString(Of TType)(ByVal value As SearchCriteriaType(Of TType), ByVal columnName As String, ByRef parameters() As DBHelper.DBHelperParameter) As String
        Return value.ToSqlString(String.Empty, columnName, parameters)
    End Function

    <Extension()> _
    Public Function ToSqlString(Of TType)(ByVal value As SearchCriteriaType(Of TType), ByVal tableName As String, ByVal columnName As String, ByRef parameters() As DBHelper.DBHelperParameter) _
        As String
        Dim strSqlString As String

        ' Return Blank String if value is nothing or value is Empty
        If (value Is Nothing) Then Return String.Empty
        If (value.IsEmpty()) Then Return String.Empty
        ' Throw an error is value is not valid
        If (Not value.IsValid()) Then Throw New InvalidOperationException()

        ' If Table Name is nothing then make it string.Empty otherwise suffix with period (.)
        If (tableName Is Nothing) Then
            tableName = String.Empty
        Else
            If (tableName.Trim().Length > 0) Then
                tableName = String.Concat(tableName.Trim(), ".")
            Else
                tableName = String.Empty
            End If
        End If

        Dim dataCleansingFunctionPrefix As String = String.Empty
        Dim dataCleansingFunctionPostfix As String = String.Empty
        Select Case value.DataType
            Case SearchDataType.Date
                dataCleansingFunctionPrefix = "trunc("
                dataCleansingFunctionPostfix = ")"
        End Select

        ' Build SQL String
        Select Case value.SearchType
            Case SearchTypeEnum.Equals
                strSqlString = String.Format("{0} {3}{2}{1}{4} = :{1}", " and", columnName, tableName, dataCleansingFunctionPrefix, dataCleansingFunctionPostfix)
            Case SearchTypeEnum.Like
                strSqlString = String.Format("{0} {3}{2}{1}{4} like :{1}", " and", columnName, tableName, dataCleansingFunctionPrefix, dataCleansingFunctionPostfix)
            Case SearchTypeEnum.GreaterThan
                strSqlString = String.Format("{0} {3}{2}{1}{4} > :{1}", " and", columnName, tableName, dataCleansingFunctionPrefix, dataCleansingFunctionPostfix)
            Case SearchTypeEnum.LessThan
                strSqlString = String.Format("{0} {3}{2}{1}{4} < :{1}", " and", columnName, tableName, dataCleansingFunctionPrefix, dataCleansingFunctionPostfix)
            Case SearchTypeEnum.Between
                strSqlString = String.Format("{0} {3}{2}{1}{4} between :{1}1 and :{1}2", " and", columnName, tableName, dataCleansingFunctionPrefix, dataCleansingFunctionPostfix)
            Case Else
                Throw New NotSupportedException()
        End Select

        ' Add Parameters to Parameter Collection
        If (value.SearchType = SearchTypeEnum.Between) Then
            ' Add columnName1 and columnName2 parameters
            ReDim Preserve parameters(parameters.Length + 1)
            parameters(parameters.Length - 2) = New DBHelper.DBHelperParameter(String.Format("{0}1", columnName), value.FromValue)
            parameters(parameters.Length - 1) = New DBHelper.DBHelperParameter(String.Format("{0}2", columnName), value.ToValue)
        Else
            ' Add columnName parameter
            ReDim Preserve parameters(parameters.Length)
            parameters(parameters.Length - 1) = New DBHelper.DBHelperParameter(columnName, _
                IIf((value.SearchType = SearchTypeEnum.Like), value.FromValue.ToString().Replace(DALBase.ASTERISK, DALBase.WILDCARD_CHAR), value.FromValue))
        End If

        Return strSqlString
    End Function
End Module
