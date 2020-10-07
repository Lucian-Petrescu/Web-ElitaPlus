Imports System.Runtime.Serialization

<Serializable>
Public Class InvalidServiceLevelException
    Inherits Exception

    Public ReadOnly ServiceLevel As String

    Public Sub New(pServiceLevel As String)
        ServiceLevel = pServiceLevel
    End Sub
    Public Sub New(pServiceLevel As String, pMessage As String)
        MyBase.New(pMessage)
        ServiceLevel = pServiceLevel
    End Sub
    Protected Sub New(info As SerializationInfo, context As StreamingContext)
        MyBase.New(info, context)
    End Sub

End Class

