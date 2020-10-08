Imports System.Reflection
Imports System.Runtime.CompilerServices
Imports Assurant.ElitaPlus.BusinessObjectsNew.WrkQueue

Public Module LinqExtentions
    Public Enum SortDirection
        None = 0
        Ascending = 1
        Descending = 2
    End Enum

    <Extension> _
    Public Function OrderBy(Of TKey)(array As TKey(), propertyName As String, direction As SortDirection) As IOrderedEnumerable(Of TKey)
        If (direction = SortDirection.Descending) Then
            Return array.OrderByDescending(Function(item) GetPropertyValueByName(item, propertyName))
        Else
            Return array.OrderBy(Function(item) GetPropertyValueByName(item, propertyName))
        End If
    End Function

    Private Function GetPropertyValueByName(item As Object, propertyName As String) As String
        Dim propInfo As PropertyInfo
        Try
            propInfo = item.GetType().GetProperty(propertyName)
            If (propInfo.PropertyType.Equals(GetType(DateTime))) Then
                Return DirectCast(propInfo.GetValue(item, Nothing), DateTime).ToString("yyyyMMddHHmmss")
            ElseIf (propInfo.PropertyType.Equals(GetType(Nullable(Of DateTime)))) Then
                Dim dt As Nullable(Of DateTime)
                dt = DirectCast(propInfo.GetValue(item, Nothing), Nullable(Of DateTime))
                If (dt.HasValue) Then
                    Return dt.Value.ToString("yyyyMMddHHmmss")
                Else
                    Return String.Empty
                End If
            Else
                Return propInfo.GetValue(item, Nothing)
            End If

        Catch ex As Exception
            Return String.Empty
        End Try
    End Function
End Module
