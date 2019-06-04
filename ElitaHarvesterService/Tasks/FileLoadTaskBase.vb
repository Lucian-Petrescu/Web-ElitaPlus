Public MustInherit Class FileLoadTaskBase
    Inherits TaskBase
#Region "Constructors"
    Public Sub New(ByVal machineName As String, ByVal processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub
#End Region
End Class
