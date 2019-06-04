Imports System.Runtime.Serialization

<DataContract(Name:="ClaimOperationNotSupported", Namespace:="http://elita.assurant.com/Faults")> _
Public Class ClaimOperationNotSupportedFault

    <DataMember(Name:="OperationName", IsRequired:=True)> _
    Public Property OperationName As String

    <DataMember(Name:="Message", IsRequired:=True)> _
    Public Property Message As String

End Class

