Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business

<DataContract(Name:="InvalidWildCardSearch", Namespace:="http://elita.assurant.com/Faults")>
Public Class InvalidWildCardSearch
    Public Sub New()

    End Sub

    Public Sub New(strSearchStr As String)
        SearchedWildCardString = strSearchStr
    End Sub

    <DataMember(Name:="SearchedWildCardString", IsRequired:=True)>
    Public Property SearchedWildCardString As String

End Class
