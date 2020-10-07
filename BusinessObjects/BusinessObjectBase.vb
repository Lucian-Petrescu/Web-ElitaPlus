'/*-----------------------------------------------------------------------------------------------------------------

'  AA      SSS  SSS  UU  UU RRRRR      AA    NN   NN  TTTTTTTT
'A    A   SS    SS   UU  UU RR   RR  A    A  NNN  NN     TT 
'AAAAAA   SSS   SSS  UU  UU RRRR     AAAAAA  NN N NN     TT
'AA  AA     SS    SS UU  UU RR RR    AA  AA  NN  NNN     TT
'AA  AA  SSSSS SSSSS  UUUU  RR   RR  AA  AA  NN  NNN     TT

'Copyright 2004, Assurant Group Inc..  All Rights Reserved.
'------------------------------------------------------------------------------
'This information is CONFIDENTIAL and for Assurant Group's exclusive use ONLY.
'Any reproduction or use without Assurant Group's explicit, written consent 
'is PROHIBITED.
'------------------------------------------------------------------------------

'Purpose:   This is the base class for a Business Object.  It provides the
'           following functionality all Business Objects use:
'           1. the Dataset and DataRow holding the data for the BO.
'           2. The IsNew, IsDirty, IsDeleted, IsValid methods.
'           3. The Audit Properties all BO's have.
'           4. The SetValue Method which should be used by all BO's to set their properties
'           5. The Save Method - this method MUST be called to set the Audit info. If the
'              If the BO overrides it, it should first call MyBase.Save to set Audit info.
'           6. The ValidationErrors property returns an Array of ValidationError objects
'              representing all the failed validations for the BO.
'           7. The Delete method.
'           8. Commonly used routines.
'           9. Note that there is no constructor.  Refer to any existing BusinessObject to
'              obtain the coding pattern that must be followed when deriving your BO's
'
'Author:  PAS3 Team
'
'Date:    03/22/2004

'MODIFICATION HISTORY:
'   Alejandro Riquenes (06/30/2004): Adaptation to ElitaPlus
'===========================================================================================
Imports Common = Assurant.ElitaPlus.Common
Imports System.Reflection
Imports System.Collections.Generic

Public MustInherit Class BusinessObjectBase
    Implements IBusinessObjectBase


#Region "Constructors"


    Protected Sub New()
        UniqueId = (Guid.NewGuid).ToString
    End Sub

    Protected Sub New(ByVal isDSCreator As Boolean)
        _isDSCreator = isDSCreator
        UniqueId = (Guid.NewGuid).ToString
    End Sub


#End Region


#Region "Constants"
    'temporary key value for DataColumns in Dataset that have PK Constraint
    Public Const TEMP_PK_VALUE As Int32 = 1

    Public Const SYSTEM_SEQUENCE_COL_NAME As String = DALBase.SYSTEM_SEQUENCE_COL_NAME

    Public Const SYSTEM_CHILDREN_COLLECTION_COL_NAME As String = "_SYSTEM_CHILDREN_COLLECTION"
#Region "Chile Constant Changes"
    Public Const NEW_MAX_LONG As Long = 999999999
    Public Const NEW_MAX_DOUBLE As Double = 999999999.99
    Public Const NEW_COVERAGE_MAX_LONG As Long = 999999999
    Public Const NEW_COVERAGE_MAX_DOUBLE As Double = 999999999.99999
#End Region


#End Region


#Region "Protected Attributes"

    'ARF Commented Out on 10/6/2004 'Protected Dataset As DataSet
    'ARF Commented Out on 10/6/2004 'Protected Row As DataRow
    'Protected _isDeleted As Boolean = False  'ARF commented out on 10/4/2004
    Protected _isDSCreator As Boolean = True

#End Region

#Region "Private Attributes"
    Private _ds As DataSet
    Private _row As DataRow
    Private _uniqueId As String
#End Region


#Region "Protected Methods"

    Friend Property Row As DataRow
        Get
            Return _row
        End Get
        Set
            _row = Value
            'If Not _row Is Nothing Then
            '    AddSystemColumns()
            'End If
        End Set
    End Property

    Friend Property Dataset As DataSet
        Get
            Return _ds
        End Get
        Set
            _ds = Value
        End Set
    End Property

    Protected Sub CheckIsOld()
        If Not IsNew Then
            Throw New BOInvalidOperationException(Common.ErrorCodes.BO_CANNOT_BE_UPDATED)
        End If
    End Sub

    Protected Sub CheckDeleted()
        If IsDeleted Then
            Throw New BOInvalidOperationException(Common.ErrorCodes.BO_IS_DELETED)
        End If
    End Sub

    Protected Sub WriteOnceCheck()
        If Not IsNew Then
            Throw New BOInvalidOperationException(Common.ErrorCodes.WRITE_ONLY_ONCE)
        End If
    End Sub

    'this overloaded version is used after a FIND operation on a DataTable
    Protected Overloads Sub CheckDataNotFound()
        If Row Is Nothing Then
            Throw New DataNotFoundException
        End If
    End Sub

    'this overloaded version is used after using the DAL to load a DataTable
    Protected Overloads Sub CheckDataNotFound(ByVal tableName As String)
        If Dataset.Tables(tableName).Rows.Count = 0 Then
            Throw New DataNotFoundException
        End If
    End Sub

    'this generic method should be called by all BO's to set their properties
    Protected Sub SetValue(ByVal columnName As String, ByVal newValue As Object)
        If Not newValue Is Nothing And Row(columnName) Is DBNull.Value Then
            'new value is something and old value is DBNULL
            If newValue.GetType Is GetType(BooleanType) Then
                '- BooleanType, special case - convert to string Y or N
                If CType(newValue, BooleanType).Value Then
                    Row(columnName) = "Y"
                Else
                    Row(columnName) = "N"
                End If
            ElseIf newValue.GetType Is GetType(Guid) Then
                'ElseIf newValue.GetType Is GetType(Guid) Then
                If Not newValue.Equals(Guid.Empty) Then
                    Row(columnName) = CType(newValue, Guid).ToByteArray
                End If
            ElseIf newValue.GetType Is GetType(Byte()) Then
                Row(columnName) = CType(newValue, Byte())
            ElseIf newValue.GetType Is GetType(DateType) Then
                Row(columnName) = CType(newValue.ToString, DateTime)
            ElseIf newValue.GetType Is GetType(Double) Then
                Row(columnName) = CType(newValue, Double)
            ElseIf newValue.GetType Is GetType(Decimal) Then
                Row(columnName) = CType(newValue, Decimal)
            Else
                '- DateType, DecimalType, etc... all our other custome types
                '- see if 'newValue Type' has a Value property (only our custom types do)
                Dim propInfo As System.Reflection.PropertyInfo = newValue.GetType.GetProperty("Value")
                If Not (propInfo Is Nothing) Then
                    '- call the Value property to extract the native .NET type (double, decimal, etc...)
                    newValue = propInfo.GetValue(newValue, Nothing)
                End If

                '- let the DataColumn convert the value to its internal data type
                Row(columnName) = newValue
            End If
        ElseIf Not newValue Is Nothing Then
            'new value is something and old value is also something
            '- convert current value to a string
            Dim currentValue As Object = Row(columnName)
            If newValue.GetType Is GetType(Guid) Then
                currentValue = New Guid(CType(currentValue, Byte()))
            ElseIf newValue.GetType Is GetType(Byte()) Then
                currentValue = CType(currentValue, Byte())
            Else
                currentValue = currentValue.ToString
                '- create an array of types containing one type, the String type
                Dim types() As Type = {GetType(String)}
                '- see if the 'newValue Type' has a 'Parse(String)' method taking a String parameter
                Dim miMethodInfo As System.Reflection.MethodInfo = newValue.GetType.GetMethod("Parse", types)
                If Not miMethodInfo Is Nothing Then
                    '- it does have a Parse method, newValue must be a number type.
                    '- extract the current value as a string
                    Dim args() As Object = {Row(columnName).ToString}
                    '- call the Parse method to convert the currentValue string to a number
                    currentValue = miMethodInfo.Invoke(newValue, args)
                End If
            End If
            '- only dirty the BO if new value is different from old value
            If Not newValue.Equals(currentValue) Then
                If newValue.GetType Is GetType(BooleanType) Then
                    '- BooleanType, special case - convert to string Y or N
                    If CType(newValue, BooleanType).Value Then
                        newValue = "Y"
                    Else
                        newValue = "N"
                    End If
                ElseIf newValue.GetType Is GetType(Byte()) Then
                    newValue = CType(newValue, Byte())
                Else
                    '- DateType, DecimalType, etc... all our other custome types
                    '- see if 'newValue Type' has a Value property (only our custom types do)
                    Dim propInfo As System.Reflection.PropertyInfo = newValue.GetType.GetProperty("Value")
                    If Not (propInfo Is Nothing) Then
                        '- call the Value property to extract the native .NET type (double, decimal, etc...)
                        newValue = propInfo.GetValue(newValue, Nothing)
                    End If
                End If
                '- at this point, newValue has a native .NET type
                If newValue.GetType Is GetType(Guid) Then
                    If newValue.Equals(Guid.Empty) Then
                        newValue = DBNull.Value
                    Else
                        newValue = CType(newValue, Guid).ToByteArray
                    End If
                End If
                Row(columnName) = newValue
            End If
        ElseIf newValue Is Nothing And Not Row(columnName) Is DBNull.Value Then
            Row(columnName) = DBNull.Value
        End If
    End Sub

    Protected Shared Sub SetCreatedAuditInfo(ByVal row As DataRow)
        row(DALBase.COL_NAME_CREATED_BY) = ElitaPlusIdentity.Current.ActiveUser.NetworkId
    End Sub

    Protected Shared Sub SetModifiedAuditInfo(ByVal row As DataRow)
        row(DALBase.COL_NAME_MODIFIED_BY) = ElitaPlusIdentity.Current.ActiveUser.NetworkId
    End Sub

    Protected Sub SetCreatedAuditInfo()
        Row(DALBase.COL_NAME_CREATED_BY) = ElitaPlusIdentity.Current.ActiveUser.NetworkId
        'note that we dont set any date audit columns because the Oracle triggers do it
        'Me.Row(Me.COL_ACTIVE_FLAG_YN) = "Y"
        'Me.Row(Me.COL_MAINT_BY_KEY) = Security.PASIdentity.Current.ResourceID
        'Me.Row(Me.COL_CREATED_BY_KEY) = Security.PASIdentity.Current.ResourceID
    End Sub

    Protected Sub SetModifiedAuditInfo()
        Row(DALBase.COL_NAME_MODIFIED_BY) = ElitaPlusIdentity.Current.ActiveUser.NetworkId
        'TODO Change COL_LAST_ADMIN_CHANGE_DT if the user is in the Admin Role
        'If Security.PASIdentity.Current.ResourceID <> Row(Me.COL_MAINT_BY_KEY).ToString Then
        '    Row(Me.COL_MAINT_BY_KEY) = Security.PASIdentity.Current.ResourceID
        'End If
    End Sub

    Protected Function BuildInClause(ByVal tableName As String, ByVal columnName As String) As String
        Dim inClause As New System.Text.StringBuilder("0")
        Dim row As DataRow

        For Each row In Dataset.Tables(tableName).Rows
            If Not row.RowState = DataRowState.Deleted Then
                If inClause.Length > 0 Then
                    inClause.Append(",")
                End If
                If row(columnName).GetType Is GetType(String) Then
                    inClause.Append("'")
                    inClause.Append(row(columnName).ToString)
                    inClause.Append("'")
                Else
                    inClause.Append(row(columnName).ToString)
                End If
            End If
        Next

        inClause.Append(")")
        Return "IN (" & inClause.ToString
    End Function

    'This method returns true if a column in a datatable is different from value fetched from database
    Protected Function CheckColumnChanged(ByVal colName As String, Optional ByVal targetVersion As DataRowVersion = DataRowVersion.Original) As Boolean
        Dim col As DataColumn

        col = Row.Table.Columns(colName)

        'if the bo is new, you cannot access the targetVersion value of a column,
        'so we will just check to see if the current version of column has a value
        If IsNew AndAlso Not IsBeingEdited Then
            If Row.IsNull(col, DataRowVersion.Current) Then
                Return False
            Else
                Return True
            End If
        End If

        'null checks
        If Row.IsNull(col, DataRowVersion.Current) And Row.IsNull(col, targetVersion) Then
            Return False
        End If
        If Row.IsNull(col, DataRowVersion.Current) And Not (Row.IsNull(col, targetVersion)) Then
            Return True
        End If
        If Not (Row.IsNull(col, DataRowVersion.Current)) And Row.IsNull(col, targetVersion) Then
            Return True
        End If

        'checks for each data type - brute force method - there is probably a better way

        If col.DataType Is GetType(String) Then
            Dim cur As String = CType(Row(col, DataRowVersion.Current), String)
            Dim old As String = CType(Row(col, targetVersion), String)
            If cur = old Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(DateTime) Then
            Dim cur As DateTime = CType(Row(col, DataRowVersion.Current), DateTime)
            Dim old As DateTime = CType(Row(col, targetVersion), DateTime)
            If cur = old Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(Decimal) Then
            Dim cur As Decimal = CType(Row(col, DataRowVersion.Current), Decimal)
            Dim old As Decimal = CType(Row(col, targetVersion), Decimal)
            If cur = old Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(Guid) Then
            Dim cur As Guid = CType(Row(col, DataRowVersion.Current), Guid)
            Dim old As Guid = CType(Row(col, targetVersion), Guid)
            If old.Equals(cur) Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(System.Byte()) Then
            Dim curB As Byte() = CType(Row(col, DataRowVersion.Current), Byte())
            Dim oldB As Byte() = CType(Row(col, targetVersion), Byte())
            If curB.Length <> 16 Then Throw New ApplicationException("BusinessObjectBase.CheckColumnChanged only supports Byte Arrays that represent GUIDs")
            If oldB.Length <> 16 Then Throw New ApplicationException("BusinessObjectBase.CheckColumnChanged only supports Byte Arrays that represent GUIDs")
            Dim cur As New Guid(curB)
            Dim old As New Guid(oldB)
            If old.Equals(cur) Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(Integer) Then
            Dim cur As Integer = CType(Row(col, DataRowVersion.Current), Integer)
            Dim old As Integer = CType(Row(col, targetVersion), Integer)
            If cur = old Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(System.Int16) Then
            Dim cur As Int16 = CType(Row(col, DataRowVersion.Current), Int16)
            Dim old As Int16 = CType(Row(col, targetVersion), Int16)
            If cur = old Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(Long) Then
            Dim cur As Long = CType(Row(col, DataRowVersion.Current), Long)
            Dim old As Long = CType(Row(col, targetVersion), Long)
            If cur = old Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(Single) Then
            Dim cur As Single = CType(Row(col, DataRowVersion.Current), Single)
            Dim old As Single = CType(Row(col, targetVersion), Single)
            If cur = old Then
                Return False
            Else
                Return True
            End If
        End If

        If col.DataType Is GetType(Double) Then
            Dim cur As Double = CType(Row(col, DataRowVersion.Current), Double)
            Dim old As Double = CType(Row(col, targetVersion), Double)
            If cur = old Then
                Return False
            Else
                Return True
            End If
        End If

        'if we get this far throw an exception
        Throw New ApplicationException("Please modify BusinessObject.CheckColumnChanged to handle the following Datatype: " & col.DataType.ToString)

    End Function


    'It adds the Constraint only if is not already there
    Public Shared Function FindRow(ByVal keyColValue As Object, ByVal keyColName As String, ByVal table As DataTable) As DataRow
        'Do a full scan. This will work fine only for a few records. TODO Revise this logic
        Dim row As DataRow
        For Each row In table.Rows
            If Not (row.RowState = DataRowState.Deleted Or row.RowState = DataRowState.Detached) Then
                Dim rowValue As Object = row(keyColName)
                If Not rowValue Is DBNull.Value Then
                    If keyColValue.GetType Is GetType(Guid) Then
                        rowValue = New Guid(CType(rowValue, Byte()))
                    End If
                    If keyColValue.Equals(rowValue) Then
                        Return row
                    End If
                End If
            End If
        Next
        Return Nothing

        'This Approach didn't work because of the infamous guid

        ''First add the search constraint if doesn't exist
        'If table.Constraints.IndexOf("Key") < 0 Then
        '    table.Constraints.Add("Key", table.Columns(keyColName), True)
        'End If
        ''Now find the row
        'Return table.Rows.Find(CType(keyColValue, Guid).ToString)
    End Function

    'It adds the Constraint only if is not already there
    Public Shared Function FindRow(ByVal keyColValue1 As Object, ByVal keyColName1 As String, _
                    ByVal keyColValue2 As Object, ByVal keyColName2 As String, ByVal table As DataTable) As DataRow
        'Do a full scan. This will work fine only for a few records. TODO Revise this logic
        Dim row As DataRow
        For Each row In table.Rows
            If Not (row.RowState = DataRowState.Deleted Or row.RowState = DataRowState.Detached) Then
                Dim rowValue1 As Object = row(keyColName1)
                Dim rowValue2 As Object = row(keyColName2)
                If ((Not rowValue1 Is DBNull.Value) AndAlso (Not rowValue2 Is DBNull.Value)) Then
                    If keyColValue1.GetType Is GetType(Guid) Then
                        rowValue1 = New Guid(CType(rowValue1, Byte()))
                    End If
                    If keyColValue2.GetType Is GetType(Guid) Then
                        rowValue2 = New Guid(CType(rowValue2, Byte()))
                    End If
                    If keyColValue1.Equals(rowValue1) AndAlso keyColValue2.Equals(rowValue2) Then
                        Return row
                    End If
                End If
            End If
        Next
        Return Nothing

        'This Approach didn't work because of the infamous guid

        ''First add the search constraint if doesn't exist
        'If table.Constraints.IndexOf("Key") < 0 Then
        '    table.Constraints.Add("Key", table.Columns(keyColName), True)
        'End If
        ''Now find the row
        'Return table.Rows.Find(CType(keyColValue, Guid).ToString)
    End Function

    Public Shared Function FindRow(ByVal NameValuePairList As Generic.List(Of KeyValuePair(Of String, Object)), ByVal table As DataTable)
        'Do a full scan. This will work fine only for a few records. TODO Revise this logic
        Dim row As DataRow
        Dim isRowMatch As Boolean
        For Each row In table.Rows
            If Not (row.RowState = DataRowState.Deleted Or row.RowState = DataRowState.Detached) Then
                isRowMatch = True
                For Each keyValuePair In NameValuePairList
                    Dim rowValue As Object = row(keyValuePair.Key)
                    If (rowValue Is DBNull.Value) Then
                        isRowMatch = False
                        Exit For
                    End If
                    If keyValuePair.Value.GetType Is GetType(Guid) Then
                        rowValue = New Guid(CType(rowValue, Byte()))
                    End If
                    If (Not keyValuePair.Value.Equals(rowValue)) Then
                        isRowMatch = False
                        Exit For
                    End If
                Next
                If (isRowMatch) Then
                    Return row
                End If
            End If
        Next
        Return Nothing
    End Function


    'It adds the Constraint only if is not already there
    Public Shared Function FindRow(ByVal keyColValue1 As Object, ByVal keyColName1 As String, _
                    ByVal keyColValue2 As Object, ByVal keyColName2 As String, _
                    ByVal keyColValue3 As Object, ByVal keyColName3 As String, ByVal table As DataTable) As DataRow
        'Do a full scan. This will work fine only for a few records. TODO Revise this logic
        Dim row As DataRow
        For Each row In table.Rows
            If Not (row.RowState = DataRowState.Deleted Or row.RowState = DataRowState.Detached) Then
                Dim rowValue1 As Object = row(keyColName1)
                Dim rowValue2 As Object = row(keyColName2)
                Dim rowValue3 As Object = row(keyColName3)
                If ((Not rowValue1 Is DBNull.Value) AndAlso (Not rowValue2 Is DBNull.Value) AndAlso _
                    (Not rowValue3 Is DBNull.Value)) Then
                    If keyColValue1.GetType Is GetType(Guid) Then
                        rowValue1 = New Guid(CType(rowValue1, Byte()))
                    End If
                    If keyColValue2.GetType Is GetType(Guid) Then
                        rowValue2 = New Guid(CType(rowValue2, Byte()))
                    End If
                    If keyColValue3.GetType Is GetType(Guid) Then
                        rowValue3 = New Guid(CType(rowValue3, Byte()))
                    End If
                    If keyColValue1.Equals(rowValue1) AndAlso keyColValue2.Equals(rowValue2) AndAlso _
                       keyColValue3.Equals(rowValue3) Then
                        Return row
                    End If
                End If
            End If
        Next
        Return Nothing

    End Function

    Public Overridable Function ProcessWSRequest() As String
        Return String.Empty

    End Function

#End Region


#Region "General Properties"

    Public Property UniqueId As String Implements IBusinessObjectBase.UniqueId
        Get
            Return _uniqueId
        End Get
        Set
            _uniqueId = Value
        End Set
    End Property

    Public Overridable ReadOnly Property IsNew As Boolean
        Get
            'Me.CheckDeleted()
            Return (Row.RowState = DataRowState.Added)
        End Get
    End Property

    Public Shared ReadOnly Property IsNew(ByVal row As DataRow) As Boolean
        Get
            'Me.CheckDeleted()
            Return (row.RowState = DataRowState.Added)
        End Get
    End Property


    Public Overridable ReadOnly Property IsDirty As Boolean
        Get
            'Me.CheckDeleted()
            'Return (Row.RowState = DataRowState.Modified) Or (Row.RowState = DataRowState.Added) Or (Row.RowState = DataRowState.Deleted)

            If Row.HasVersion(DataRowVersion.Proposed) Then
                'The object is being edited so it will be different if it is different from the current version
                'Which contains the values before calling the begin edit
                Dim i As Integer = 0
                For i = 0 To Row.Table.Columns.Count - 1
                    If Not Row.Table.Columns(i).ColumnName.StartsWith("_SYSTEM") Then
                        If CheckColumnChanged(Row.Table.Columns(i).ColumnName, DataRowVersion.Proposed) Then
                            Return True
                        End If
                    End If
                Next
            Else
                Select Case Row.RowState
                    Case DataRowState.Unchanged
                        Return False
                    Case DataRowState.Added
                        'this may be changed in future to return true only if user has entered data in new row
                        Return True
                    Case DataRowState.Deleted
                        Return True
                    Case DataRowState.Modified
                        Return AnyColumnHasChanged
                    Case DataRowState.Detached
                        Throw New BOInvalidOperationException("Cannot call IsDirty on an object with a detached row")
                End Select
            End If
        End Get
    End Property

    Public ReadOnly Property DirtyColumns As Hashtable
        Get
            Dim ht As New Hashtable
            Dim col As DataColumn

            For Each col In Row.Table.Columns
                'Exclude Internal System added columns
                If Not col.ColumnName.ToUpper.StartsWith("_SYSTEM") Then
                    If CheckColumnChanged(col.ColumnName) Then
                        ht.Add(col.ColumnName.ToUpper, col)
                    End If
                End If
            Next

            Return ht
        End Get
    End Property

    Public Overridable ReadOnly Property IsChildrenDirty As Boolean
        Get
            Dim chidrenCollections As ArrayList = GetChildrenCollections
            Dim coll As BusinessObjectListBase
            For Each coll In chidrenCollections
                If Not coll Is Nothing AndAlso coll.IsDirty Then Return True
            Next
            Return False
        End Get
    End Property

    Public Overridable ReadOnly Property IsFamilyDirty As Boolean
        Get
            Return Dataset.HasChanges()
        End Get
    End Property

    Public Overridable ReadOnly Property IsDeleted As Boolean
        Get
            'Return Me._isDeleted
            Return Row.RowState = DataRowState.Deleted OrElse Row.RowState = DataRowState.Detached
        End Get
    End Property

    Public Shared ReadOnly Property IsDeleted(ByVal row As DataRow) As Boolean
        Get
            'Return Me._isDeleted
            Return row.RowState = DataRowState.Deleted OrElse row.RowState = DataRowState.Detached
        End Get
    End Property

    Public Overridable ReadOnly Property IsValid As Boolean
        Get
            Dim errors() As ValidationError = FindValidationErrors()
            If Not errors Is Nothing AndAlso errors.Length > 0 Then
                Return False
            Else
                Return True
            End If
        End Get
    End Property

    Public ReadOnly Property ValidationErrors As ValidationError()
        Get
            Return FindValidationErrors
        End Get
    End Property

    'returns true if just one col has a value different from what was fetched from database
    Public ReadOnly Property AnyColumnHasChanged As Boolean
        Get
            Dim col As DataColumn

            For Each col In Row.Table.Columns
                'Exclude Internal System added columns
                If Not col.ColumnName.ToUpper.StartsWith("_SYSTEM") Then
                    If CheckColumnChanged(col.ColumnName) Then
                        Return True
                    End If

                End If
            Next

            Return False
        End Get
    End Property

    Public ReadOnly Property IsSaveNew As Boolean
        Get
            Return CreatedById Is Nothing
        End Get
    End Property

    Public ReadOnly Property IsBeingEdited As Boolean
        Get
            Return Row.HasVersion(DataRowVersion.Proposed)
        End Get
    End Property

#End Region


#Region "Audit Properties"

    Public ReadOnly Property ModifiedDate As DateType
        Get
            If Row(DALBase.COL_NAME_MODIFIED_DATE) Is DBNull.Value Then Return Nothing
            Return New DateType(CType(Row(DALBase.COL_NAME_MODIFIED_DATE), Date))
        End Get
    End Property

    '<ValueMandatory("")> _
    Public ReadOnly Property ModifiedById As String
        Get
            If Row(DALBase.COL_NAME_MODIFIED_BY) Is DBNull.Value Then Return Nothing
            Return CType(Row(DALBase.COL_NAME_MODIFIED_BY), String)
        End Get
    End Property


    Public ReadOnly Property CreatedDate As DateType
        Get
            If Row(DALBase.COL_NAME_CREATED_DATE) Is DBNull.Value Then Return Nothing
            Return New DateType(CType(Row(DALBase.COL_NAME_CREATED_DATE), Date))
        End Get
    End Property

    Public ReadOnly Property CreatedDateTime As DateTimeType
        Get
            If Row(DALBase.COL_NAME_CREATED_DATE) Is DBNull.Value Then Return Nothing
            Return CType(Row(DALBase.COL_NAME_CREATED_DATE), DateTime)
        End Get
    End Property


    '<ValueMandatory("")> _
    Public ReadOnly Property CreatedById As String
        Get
            If Row(DALBase.COL_NAME_CREATED_BY) Is DBNull.Value Then Return Nothing
            Return CType(Row(DALBase.COL_NAME_CREATED_BY), String)
        End Get
    End Property

#End Region

#Region "Children Related"


    'collType must inherit from BusinessObjectListBase
    Public Sub AddChildrenCollection(ByVal collType As Type)
        Dim columnName As String = SYSTEM_CHILDREN_COLLECTION_COL_NAME & collType.Name
        If Row.Table.Columns.IndexOf(columnName) < 0 Then
            Row.Table.Columns.Add(columnName, GetType(String))
        End If
        Dim assemblyName As String = collType.Assembly.FullName
        Dim typeName As String = collType.FullName
        Row(columnName) = assemblyName & "|" & typeName
    End Sub

    Public Function GetChildrenCollections() As ArrayList
        Dim result As New ArrayList
        Dim column As DataColumn
        For Each column In Row.Table.Columns
            If column.ColumnName.ToUpper.StartsWith(SYSTEM_CHILDREN_COLLECTION_COL_NAME) Then
                result.Add(GetChildrenCollection(column.ColumnName))
            End If
        Next
        Return result
    End Function

    Public Function GetChildrenCollection(ByVal collType As Type) As BusinessObjectListBase
        Return GetChildrenCollection(SYSTEM_CHILDREN_COLLECTION_COL_NAME & collType.Name)
    End Function

    Public Function IsChildrenCollectionLoaded(ByVal collType As Type) As Boolean
        Dim columnName As String = SYSTEM_CHILDREN_COLLECTION_COL_NAME & collType.Name
        If Row.Table.Columns.IndexOf(columnName) >= 0 AndAlso Not Row(columnName) Is DBNull.Value Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Function GetChildrenCollection(ByVal columnName As String) As BusinessObjectListBase
        If Row.Table.Columns.IndexOf(columnName) < 0 OrElse Row(columnName) Is DBNull.Value Then
            Return Nothing
        End If
        Dim fieldParsedContents() As String = System.Text.RegularExpressions.Regex.Split(Row(columnName), "\|")
        Dim assemblyName As String = fieldParsedContents(0)
        Dim typeName As String = fieldParsedContents(1)
        Dim collAssembly As [Assembly] = System.Reflection.Assembly.Load(assemblyName)
        Dim collType As Type = collAssembly.GetType(typeName)
        Dim list As BusinessObjectListBase = collType.GetConstructor(New Type() {[GetType]}).Invoke(New Object() {Me})
        Return list
        'Dim objHandle As System.Runtime.Remoting.ObjectHandle = System.Activator.CreateInstance(assemblyName, typeName, New Object() {Me})
        'Return CType(objHandle.Unwrap, BusinessObjectListBase)
    End Function




#End Region

#Region "Public Methods"
    ' this method will make the BO dirty so the audit fields will be updated.
    Public Overridable Sub Touch()
        Row.Item(DALBase.COL_NAME_MODIFIED_DATE) = System.DateTime.Now
    End Sub

    Public Overridable Sub Delete()
        CheckDeleted()

        'First Delete the Children
        DeleteChildren()

        Row.Delete()
        'Me._isDeleted = True 'ARF commented out on 10/4/04
    End Sub

    Public Overridable Sub DeleteChildren()
        Dim chidrenCollections As ArrayList = GetChildrenCollections
        Dim coll As BusinessObjectListBase
        For Each coll In chidrenCollections
            If Not coll Is Nothing Then
                coll.DeleteAll()
            End If
        Next
    End Sub

    'For a BO that must persist to the database, the developer should override this Save
    'but they should call MyBase.Save (this method) first, then add their persist code.
    Public Overridable Sub Save()
        'Me.CheckDeleted()
        If Row.RowState = DataRowState.Detached Then
            Return 'Nothing to do. This is the case when something is added and delted before calling save
        End If
        If Not IsDeleted Then
            Validate()
        End If
        If IsNew Then
            SetCreatedAuditInfo()
        ElseIf Not IsDeleted AndAlso IsDirty Then
            SetModifiedAuditInfo()
        End If
    End Sub

    Public Shared Sub UpdateFamily(ByVal ds As DataSet)
        Dim dt As DataTable

        For Each dt In ds.Tables
            Dim rowIdx As Integer
            For rowIdx = 0 To dt.Rows.Count - 1
                Dim rowState As DataRowState = dt.Rows(rowIdx).RowState

                If rowState = DataRowState.Added Then   ' New Row
                    dt.Rows(rowIdx)(DALBase.COL_NAME_CREATED_BY) = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                Else
                    Dim isDeletedLocal As Boolean = (rowState = DataRowState.Deleted OrElse rowState = DataRowState.Detached)
                    Dim isDirtyLocal As Boolean = False

                    Select Case rowState
                        Case DataRowState.Unchanged
                            isDirtyLocal = False
                        Case DataRowState.Added
                            'this may be changed in future to return true only if user has entered data in new row
                            isDirtyLocal = True
                        Case DataRowState.Deleted
                            isDirtyLocal = True
                        Case DataRowState.Modified
                            isDirtyLocal = True
                        Case DataRowState.Detached
                            isDirtyLocal = False
                    End Select

                    If Not isDeletedLocal AndAlso isDirtyLocal Then
                        dt.Rows(rowIdx)(DALBase.COL_NAME_MODIFIED_BY) = ElitaPlusIdentity.Current.ActiveUser.NetworkId
                    End If
                End If
            Next
        Next

    End Sub

    Public Shared Sub Save(ByVal row As DataRow)
        If row.RowState = DataRowState.Detached Then
            Return 'Nothing to do. This is the case when something is added and delted before calling save
        End If
        If IsNew(row) Then
            SetCreatedAuditInfo(row)
        ElseIf Not IsDeleted(row) Then 'AndAlso Me.IsDirty Then
            SetModifiedAuditInfo(row)
        End If
    End Sub

    Public Overridable Sub Validate()
        Dim errors() As ValidationError = FindValidationErrors()

        If Not errors Is Nothing AndAlso errors.Length > 0 Then
            Throw New BOValidationException(errors, [GetType].FullName, UniqueId)
        End If
    End Sub

    Public Function FindValidationErrors() As ValidationError()
        Dim validator As New Validator
        validator.IsValid(Me)
        Return validator.Messages
    End Function

    Public Sub VerifyConcurrency(ByVal sModifiedDate As String)
        Dim dtModifiedDate As DateType

        If (Not sModifiedDate = String.Empty AndAlso (Not ModifiedDate Is Nothing)) Then
            dtModifiedDate = New DateType(CType(sModifiedDate, Date))
            If ModifiedDate.Value <> dtModifiedDate.Value Then
                ' Modified Dates are different
                Throw New DALConcurrencyAccessException
            End If
        Else
            If (Not ModifiedDate Is Nothing) Then
                ' First Instance does not have modified date but Second Instance has a modified date
                Throw New DALConcurrencyAccessException
            End If
        End If
    End Sub

    'Copy all the Public properties with the same name in both objects that are in the class of the original. 
    'Properties named as "ID" are not copied either
    Public Sub CopyFrom(ByVal original As BusinessObjectBase, Optional ByVal includeParentProperties As Boolean = False)
        Dim fromType As Type = original.GetType
        Dim toType As Type = [GetType]
        Dim fromProp As PropertyInfo

        For Each fromProp In fromType.GetProperties(BindingFlags.Public Or BindingFlags.Instance)
            If fromProp.Name.ToUpper <> "ID" AndAlso Not fromProp.GetSetMethod Is Nothing Then
                Dim toProp As PropertyInfo
                If (includeParentProperties) Then
                    toProp = toType.GetProperty(fromProp.Name, BindingFlags.Public Or BindingFlags.Instance)
                Else
                    toProp = toType.GetProperty(fromProp.Name, BindingFlags.Public Or BindingFlags.Instance Or BindingFlags.DeclaredOnly)
                End If

                If Not toProp Is Nothing Then
                    toProp.SetValue(Me, fromProp.GetValue(original, Nothing), Nothing)
                End If
            End If
        Next

    End Sub


    'Copy all the Public properties with the same name in both objects that are in the class of the original.     
    Public Sub Clone(ByVal original As BusinessObjectBase)
        Dim fromType As Type = original.GetType
        Dim toType As Type = [GetType]
        Dim fromProp As PropertyInfo

        For Each fromProp In fromType.GetProperties(BindingFlags.Public Or BindingFlags.Instance)
            If Not fromProp.GetSetMethod Is Nothing Then
                Dim toProp As PropertyInfo = toType.GetProperty(fromProp.Name, BindingFlags.Public Or BindingFlags.Instance)
                If Not toProp Is Nothing Then
                    toProp.SetValue(Me, fromProp.GetValue(original, Nothing), Nothing)
                End If
            End If
        Next

    End Sub

    'this restores the business object to all the values it had when it was loaded from the database
    Public Sub RejectChanges()
        Select Case Row.RowState
            Case DataRowState.Unchanged
                'if there are no changes just get out
                Exit Sub
            Case DataRowState.Detached
                'a detached row means it was deleted in database by dataadapter, you cannot undo
                Throw New BOInvalidOperationException("Cannot call RejectChanges on a BusinessObject that has already been deleted in database")
            Case DataRowState.Added
                'an added row cannot be undone because we currently dont have a callable method to reinitialize the bo fields
                Throw New BOInvalidOperationException("Cannot call RejectChanges on a new Businessobject - to accomplish this just throw away the current BO and instantiate a new one")
            Case DataRowState.Modified, DataRowState.Deleted
                'this will reset the columns from the 'original' version fetched from db, also it will set RowState to UnChanged
                Row.RejectChanges()
                'Me._isDeleted = False 'ARF Commented out on 10/4/2004
        End Select
    End Sub

    Public Sub BeginEdit()
        Row.BeginEdit()
    End Sub

    Public Sub EndEdit()
        Row.EndEdit()
    End Sub

    Public Sub cancelEdit()
        Row.CancelEdit()
    End Sub

    Public Function GetShortDate(ByVal longDate As Date) As Date

        If (longDate = Nothing) Then
            Return Nothing
        End If

        Dim shortDate As Date
        With longDate
            shortDate = New Date(.Year, .Month, .Day)
        End With
        Return (shortDate)
    End Function

   
    Protected Shared Function SplitDatasetForXML(ByVal DatasetInput As DataSet, ByVal GroupedColumn As String, Optional ByVal MaxRows As Integer = 100) As DataSet()

        Dim ds As DataSet
        Dim arrList As New ArrayList
        Dim i, j, iCurrRow As Integer

        If DatasetInput.Tables(0).Rows.Count > MaxRows Then

            For i = 0 To System.Math.Ceiling(DatasetInput.Tables(0).Rows.Count / MaxRows)

                'create a new dataset as a clone of the original
                ds = New DataSet(DatasetInput.DataSetName)
                ds.Tables.Add(DatasetInput.Tables(0).Clone)
                j = 1

                'If the current iteration is less than the MAXROWS, and we still have items in the orig. ds, loop
                While j <= MaxRows And iCurrRow < DatasetInput.Tables(0).Rows.Count
                    ds.Tables(0).ImportRow(DatasetInput.Tables(0).Rows(iCurrRow))
                    iCurrRow += 1
                    j += 1
                End While

                'If the current row is equal to the count of the table, we are done
                If iCurrRow = DatasetInput.Tables(0).Rows.Count Then
                    'Add the current dataset to the arraylist
                    arrList.Add(ds)
                    Exit For
                End If

                'Check to see if there is a grouped column necessary.  If so, check the last added record
                '  and the next record in the original ds to see if it need to come also.
                If (Not GroupedColumn Is Nothing) AndAlso (Not GroupedColumn = String.Empty) Then
                    While DatasetInput.Tables(0).Rows.Count > iCurrRow AndAlso ds.Tables(0).Rows(ds.Tables(0).Rows.Count - 1)(GroupedColumn) = DatasetInput.Tables(0).Rows(iCurrRow)(GroupedColumn)
                        ds.Tables(0).ImportRow(DatasetInput.Tables(0).Rows(iCurrRow))
                        iCurrRow += 1
                    End While
                End If

                'Add the current dataset to the arraylist
                arrList.Add(ds)
            Next

        Else
            'The dataset is under the limit so return the original
            arrList.Add(DatasetInput)
        End If

        'Convert the arraylist to an array of Datasets

        Return arrList.ToArray(GetType(DataSet))

    End Function


    'Public Function created to allow for setting of the properties by specifying column name properties rather than BO Properties
    '  Used in data import routines
    Public Function SetPropertyByColumnName(ByVal ColumnName As String, ByVal Value As Object) As Boolean

        'If column doesn't exist, return false.  
        If Row.Table.Columns(ColumnName) Is Nothing Then Return False

        Try
            SetValue(ColumnName, Value)
            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function BuildWSResponseStatus(ByVal DisplayMessage As String, ByVal ErrorMessage As String, ByVal ErrorType As String) As DataTable

        Dim ResponseStatusTable As New DataTable("ResponseStatus")

        ResponseStatusTable.Columns.Add("DisplayMessage", GetType(String))
        ResponseStatusTable.Columns.Add("ErrorMessage", GetType(String))
        ResponseStatusTable.Columns.Add("ErrorType", GetType(String))

        Dim newRow As DataRow = ResponseStatusTable.NewRow()

        newRow("DisplayMessage") = DisplayMessage
        newRow("ErrorMessage") = ErrorMessage
        newRow("ErrorType") = ErrorType
        ResponseStatusTable.Rows.Add(newRow)

        Return ResponseStatusTable
    End Function

#End Region


#Region "Private Methods"

#End Region

#Region "Share Methods"
    Public Shared Function GetHexStringFromGuid(ByVal id As Guid) As String
        Return DALBase.GuidToSQLString(id)
    End Function

    Public Shared Function IsNothing(ByVal Value As Object) As Boolean
        Return DALBase.IsNothing(Value)
    End Function


    '"Time Zone Related"
    Public Shared Function LoadConvertedTime_From_DB_ServerTimeZone(ByVal dateToConvert As DateTime, ByVal toTimeZone As String) As DateTime
        Dim dalObj As New DALBase
        Dim dv As DataView = dalObj.LoadConvertedTime_From_DB_ServerTimeZone(dateToConvert, toTimeZone)

        Dim convertedTime As DateTime = CType(dv.Item(0).Row(0), DateTime)
        Return (convertedTime)

    End Function

#End Region

#Region "Iterator"
    Public Function GetBusinessObjectIterator(ByVal boType As Type) As BusinessObjectIteratorBase
        Return New BusinessObjectIteratorBase(Row.Table, boType)
    End Function
#End Region


End Class
