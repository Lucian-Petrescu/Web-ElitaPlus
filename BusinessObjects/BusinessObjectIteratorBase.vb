Public Class BusinessObjectIteratorBase
    Implements System.Collections.IEnumerable
#Region "Private Members"
    Private _table As DataTable
    Private _boType As Type
#End Region



#Region "Constructors"
    Public Sub New(ByVal table As DataTable, ByVal boType As Type)
        _table = table
        _boType = boType
    End Sub
#End Region

#Region "Properties"
    Public ReadOnly Property Table() As DataTable
        Get
            Return _table
        End Get
    End Property

    Public ReadOnly Property BOType() As Type
        Get
            Return _boType
        End Get
    End Property

    Public ReadOnly Property Count() As Integer
        Get
            Dim countSum As Integer = 0
            Dim bo As BusinessObjectBase
            For Each bo In Me
                countSum += 1
            Next
            Return countSum
        End Get
    End Property

#End Region

#Region "Public Methods"
    Public Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
        Dim list As New ArrayList
        Dim row As DataRow
        For Each row In Table.Rows
            If Not (row.RowState = DataRowState.Deleted Or row.RowState = DataRowState.Detached) Then
                Dim bo As BusinessObjectBase = GetChild(row)
                list.Add(bo)
            End If
        Next
        Return CType(list, IEnumerable).GetEnumerator
    End Function
#End Region

#Region "Protected Methods"
    Friend Function GetChild(ByVal row As DataRow) As BusinessObjectBase
        Dim bo As BusinessObjectBase = _boType.GetConstructor(New Type() {GetType(DataRow)}).Invoke(New Object() {row})
        Return bo
    End Function
#End Region

End Class
