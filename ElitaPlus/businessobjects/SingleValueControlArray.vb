Public Class SingleValueControlArray
    Inherits CollectionBase

    Public Sub Add(ByVal oControlInfo As SingleValueControl)

        list.Add(oControlInfo)

    End Sub

    Public ReadOnly Property Item(ByVal Index As Int16) As SingleValueControl
        Get
            Return CType(list.Item(Index), SingleValueControl)
        End Get

    End Property

  
End Class