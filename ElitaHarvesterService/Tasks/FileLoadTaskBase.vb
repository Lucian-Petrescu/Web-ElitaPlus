Public MustInherit Class FileLoadTaskBase
    Inherits TaskBase
#Region "Constructors"
    Public Sub New(machineName As String, processThreadName As String)
        MyBase.New(machineName, processThreadName)
    End Sub
#End Region
End Class
