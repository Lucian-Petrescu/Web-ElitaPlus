Imports System.Reflection
Imports System.Text

<AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
Public NotInheritable Class OverlapValidatorAttribute
    Inherits ValidBaseAttribute

    Private _effectiveDateColumnName As String
    Private _expirationDateColumnName As String
    Private _keyColumns() As String
    Private _dataTablePropertyName As String
    Private _dataRowPropertyName As String

    Public Sub New(fieldDisplayName As String, Optional ByVal message As String = Common.ErrorCodes.OVERLAPPING_SCHEDULE_ERR)
        MyBase.New(fieldDisplayName, message)

    End Sub

    Public Property ExpirationDateColumnName As String
        Get
            Return _expirationDateColumnName
        End Get
        Set
            _expirationDateColumnName = value
        End Set
    End Property

    Public Property EffectiveDateColumnName As String
        Get
            Return _effectiveDateColumnName
        End Get
        Set
            _effectiveDateColumnName = value
        End Set
    End Property

    Public Property KeyColumns As String()
        Get
            Return _keyColumns
        End Get
        Set
            _keyColumns = value
        End Set
    End Property

    Public Property DataTablePropertyName As String
        Get
            Return _dataTablePropertyName
        End Get
        Set
            _dataTablePropertyName = value
        End Set
    End Property

    Public Property DataRowPropertyName As String
        Get
            Return _dataRowPropertyName
        End Get
        Set
            _dataRowPropertyName = value
        End Set
    End Property


    Public Overrides Function IsValid(objectToCheck As Object, context As Object) As Boolean
        Dim oDataTable As DataTable
        Dim oLhsRow As DataRow
        Dim expression As StringBuilder
        Dim oRows As DataRow()
        Dim oGuidKeyValues(-1) As Guid
        Dim oGuidKeys(-1) As String
        Dim oRhsRow As DataRow
        Dim oPropertyInfo As PropertyInfo
        Dim lhsExpirationDate As Date
        Dim lhsEffectiveDate As Date
        Dim rhsExpirationDate As Date
        Dim rhsEffectiveDate As Date

        oPropertyInfo = context.GetType().GetProperty(_dataTablePropertyName)
        If (Not (oPropertyInfo.PropertyType Is GetType(DataTable))) Then
            Throw New InvalidOperationException()
        End If
        oDataTable = CType(oPropertyInfo.GetValue(context, Nothing), DataTable)

        oPropertyInfo = context.GetType().GetProperty(_dataRowPropertyName)
        If (Not (oPropertyInfo.PropertyType Is GetType(DataRow))) Then
            Throw New InvalidOperationException()
        End If
        oLhsRow = CType(oPropertyInfo.GetValue(context, Nothing), DataRow)

        If (KeyColumns IsNot Nothing AndAlso KeyColumns.Length > 0) Then
            expression = New StringBuilder()
            For Each key As String In KeyColumns
                If (expression.Length > 0) Then
                    expression.Append(" AND ")
                End If
                If (oLhsRow.Table.Columns(key).DataType Is GetType(Byte())) Then
                    ReDim Preserve oGuidKeys(oGuidKeys.Length)
                    oGuidKeys(oGuidKeys.Length - 1) = key
                    ReDim Preserve oGuidKeyValues(oGuidKeyValues.Length)
                    If (oLhsRow(key) Is DBNull.Value) Then
                        oGuidKeyValues(oGuidKeyValues.Length - 1) = Guid.Empty
                    Else
                        oGuidKeyValues(oGuidKeyValues.Length - 1) = New Guid(CType(oLhsRow(key), Byte()))
                    End If
                Else
                    expression.AppendFormat("{0} = '{1}'", key, oLhsRow(key).ToString())
                End If
            Next
            oRows = oDataTable.Select(expression.ToString())
        Else
            oRows = oDataTable.Select()
        End If

        Dim cnt As Integer
        If (oLhsRow(EffectiveDateColumnName) Is DBNull.Value) Then
            lhsEffectiveDate = DateTime.MinValue
        Else
            lhsEffectiveDate = CType(oLhsRow(EffectiveDateColumnName), Date)
        End If
        If (oLhsRow(ExpirationDateColumnName) Is DBNull.Value) Then
            lhsExpirationDate = DateTime.MinValue
        Else
            lhsExpirationDate = CType(oLhsRow(ExpirationDateColumnName), Date)
        End If

        For cnt = 0 To (oRows.Count - 1)
            oRhsRow = oRows(cnt)
            If (Not oLhsRow.Equals(oRhsRow)) Then
                If (oRhsRow(EffectiveDateColumnName) Is Nothing) Then
                    rhsEffectiveDate = DateTime.MinValue
                Else
                    rhsEffectiveDate = CType(oRhsRow(EffectiveDateColumnName), Date)
                End If
                If (oRhsRow(ExpirationDateColumnName) Is Nothing) Then
                    rhsExpirationDate = DateTime.MinValue
                Else
                    rhsExpirationDate = CType(oRhsRow(ExpirationDateColumnName), Date)
                End If
                If (Not (lhsExpirationDate < rhsEffectiveDate Or lhsEffectiveDate > rhsExpirationDate)) Then
                    '' Check Guids
                    If (oGuidKeys.Length > 0) Then
                        Dim isMatch As Boolean = True
                        For i As Integer = 0 To (oGuidKeys.Length - 1)
                            Dim guidValue As Guid
                            If (oRhsRow(oGuidKeys(i)) Is DBNull.Value) Then
                                guidValue = Guid.Empty
                            Else
                                guidValue = New Guid(CType(oRhsRow(oGuidKeys(i)), Byte()))
                            End If
                            If (Not guidValue.Equals(oGuidKeyValues(i))) Then
                                isMatch = False
                                Exit For
                            End If
                        Next
                        If (isMatch) Then Return False
                    Else
                        Return False
                    End If
                End If
            End If
        Next
        Return True
    End Function
End Class
