Imports System.Reflection

Public MustInherit Class BusinessObjectListBase
    Implements IEnumerable



#Region "Constants"

#End Region

#Region "Private Members"
    Private _table As DataTable
    Private _boType As Type
    Private _parent As BusinessObjectBase
#End Region



#Region "Constructors"
    Public Sub New(table As DataTable, boType As Type, parent As BusinessObjectBase)
        _table = table
        _parent = parent
        _boType = boType
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property Table As DataTable
        Get
            Return _table
        End Get
    End Property

    Public ReadOnly Property BOType As Type
        Get
            Return _boType
        End Get
    End Property

    Public ReadOnly Property Parent As BusinessObjectBase
        Get
            Return _parent
        End Get
    End Property

    Public Overridable ReadOnly Property IsDirty As Boolean
        Get
            Dim bo As BusinessObjectBase
            Dim deletions As DataTable = Table.GetChanges(DataRowState.Added Or DataRowState.Deleted)
            If Not deletions Is Nothing Then
                Dim row As DataRow
                For Each row In deletions.Rows
                    If row.RowState = DataRowState.Deleted Then
                        row.RejectChanges()
                        If Belong(GetChild(row)) Then
                            Return True
                        End If
                    End If
                Next
            End If
            For Each bo In Me
                If bo.IsDirty Then Return True
            Next
            Return False
        End Get
    End Property

#End Region

#Region "Public Methods"
    Public Overridable Function GetChild(childId As Guid) As BusinessObjectBase
        Dim bo As BusinessObjectBase
        Try
            bo = CType(_boType.GetConstructor(BindingFlags.Instance Or
                                        BindingFlags.NonPublic Or
                                        BindingFlags.Public, Nothing,
                                        New Type() {GetType(Guid), GetType(DataSet)}, Nothing).Invoke(New Object() {childId, _table.DataSet}), BusinessObjectBase)
        Catch ex As Exception
            If Not ex.InnerException Is Nothing AndAlso ex.InnerException.GetType Is GetType(DataNotFoundException) Then
                Throw ex.InnerException
            Else
                Throw
            End If
        End Try
        Return bo
    End Function

    Public Function GetChildThird(childId As Guid, ds As DataSet, SecondaryKeyName As String) As BusinessObjectBase
        Dim bo As BusinessObjectBase
        Try
            bo = CType(_boType.GetConstructor(New Type() {GetType(Guid), GetType(DataSet), GetType(String)}).Invoke(New Object() {childId, _table.DataSet, SecondaryKeyName}), BusinessObjectBase)
        Catch ex As Exception
            If ex.InnerException IsNot Nothing AndAlso ex.InnerException.GetType Is GetType(DataNotFoundException) Then
                Throw ex.InnerException
            Else
                Throw
            End If
        End Try
        Return bo
    End Function

    Public Overridable Function GetNewChild() As BusinessObjectBase
        Dim bo = CType(_boType.GetConstructor(BindingFlags.Instance Or
                                                              BindingFlags.NonPublic Or
                                                              BindingFlags.Public, Nothing,
                                                              New Type() {GetType(DataSet)}, Nothing).Invoke(New Object() {_table.DataSet}), BusinessObjectBase)
        Return bo
    End Function

    Public Overridable Function GetNewChild(parentId As Guid) As BusinessObjectBase
        Dim bo = CType(_boType.GetConstructor(BindingFlags.Instance Or
                                                              BindingFlags.NonPublic Or
                                                              BindingFlags.Public, Nothing,
                                                              New Type() {GetType(DataSet), GetType(Guid)}, Nothing).Invoke(New Object() {_table.DataSet, parentId}), BusinessObjectBase)
        Return bo
    End Function

    Public Overridable Function GetNewChild(parentId As Guid, secondaryTableName As String) As BusinessObjectBase
        Dim bo = CType(_boType.GetConstructor(BindingFlags.Instance Or
                                                              BindingFlags.NonPublic Or
                                                              BindingFlags.Public, Nothing,
                                                              New Type() {GetType(DataSet), GetType(Guid), GetType(String)}, Nothing).Invoke(New Object() {_table.DataSet, parentId, secondaryTableName}), BusinessObjectBase)
        Return bo
    End Function

    Public ReadOnly Property Count As Integer
        Get
            Dim countSum As Integer = 0
            Dim bo As BusinessObjectBase
            For Each bo In Me
                countSum += 1
            Next
            Return countSum
        End Get
    End Property

    Public Sub DeleteAll()
        Dim bo As BusinessObjectBase
        For Each bo In Me
            bo.Delete()
            bo.Save()
        Next
    End Sub

    Public Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Dim list As New ArrayList
        Dim row As DataRow
        For Each row In Table.Rows
            If Not (row.RowState = DataRowState.Deleted OrElse row.RowState = DataRowState.Detached) Then
                Dim bo As BusinessObjectBase = GetChild(row)
                If Belong(bo) Then
                    list.Add(bo)
                End If
            End If
        Next
        Return CType(list, IEnumerable).GetEnumerator
    End Function

    Public MustOverride Function Belong(bo As BusinessObjectBase) As Boolean


#End Region

#Region "Protected Methods"
    Friend Overridable Function GetChild(row As DataRow) As BusinessObjectBase
        Dim bo = CType(_boType.GetConstructor(BindingFlags.Instance Or
                                                              BindingFlags.NonPublic Or
                                                              BindingFlags.Public, Nothing,
                                                              New Type() {GetType(DataRow)}, Nothing).Invoke(New Object() {row}), BusinessObjectBase)
        Return bo
    End Function
#End Region


End Class
