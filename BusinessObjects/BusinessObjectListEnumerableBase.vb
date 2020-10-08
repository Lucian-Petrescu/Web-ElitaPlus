Imports System.Collections.Generic

Public MustInherit Class BusinessObjectListEnumerableBase(Of TParent As BusinessObjectBase, TChild As BusinessObjectBase)
    Inherits BusinessObjectListBase
    Implements IEnumerable(Of TChild)

    Public Sub New(table As DataTable, parent As TParent)
        MyBase.New(table, GetType(TChild), parent)
    End Sub

    Public Function GetEnumerator() As IEnumerator(Of TChild) Implements IEnumerable(Of TChild).GetEnumerator
        Dim list As New List(Of TChild)
        Dim row As DataRow
        For Each row In Table.Rows
            If Not (row.RowState = DataRowState.Deleted Or row.RowState = DataRowState.Detached) Then
                Dim bo As BusinessObjectBase = GetChild(row)
                If Belong(bo) Then
                    list.Add(DirectCast(bo, TChild))
                End If
            End If
        Next
        Return CType(list, IEnumerable(Of TChild)).GetEnumerator
    End Function
End Class


