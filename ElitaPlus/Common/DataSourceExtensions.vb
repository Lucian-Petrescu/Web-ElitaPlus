Imports System.Runtime.CompilerServices
Imports System.Collections.Generic

Public Module DataSourceExtensions

    <Extension()> _
    Public Function GetSelectedRowIndex(ByVal pDataView As DataView, ByVal pCondition As Func(Of DataRowView, Boolean)) As Integer

        For i As Integer = 0 To pDataView.Count - 1
            If (pCondition(pDataView(i))) Then
                Return i
            End If
        Next

        Return -1

    End Function

    <Extension()> _
    Public Function GetSelectedRowIndex(Of TType)(ByVal pEnumerable As IEnumerable(Of TType), ByVal pCondition As Func(Of TType, Boolean)) As Integer

        Dim i As Integer = 0

        For Each o As TType In pEnumerable
            If (pCondition(o)) Then
                Return i
            End If
            i = i + 1
        Next

        Return -1

    End Function


    <Extension()> _
    Public Function GetSelectedRowIndex(ByVal pDataView As DataTable, ByVal pCondition As Func(Of DataRow, Boolean)) As Integer

        For i As Integer = 0 To pDataView.Rows.Count - 1
            If (pCondition(pDataView(i))) Then
                Return i
            End If
        Next

        Return -1

    End Function
End Module
