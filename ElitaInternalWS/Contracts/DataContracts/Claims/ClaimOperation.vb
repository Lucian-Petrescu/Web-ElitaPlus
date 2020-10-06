Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.BusinessObjectsNew

<DataContract(Name:="ClaimOperation", Namespace:="http://elita.assurant.com/DataContracts/Claim")> _
Public Class ClaimOperation

    Public Overridable Sub Execute(oClaim As ClaimBase)
        Throw New NotSupportedException()
    End Sub

End Class

