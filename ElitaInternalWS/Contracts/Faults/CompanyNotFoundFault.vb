﻿Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business

<DataContract(Name:="CompanyNotFoundFault", Namespace:="http://elita.assurant.com/Faults")>
Public Class CompanyNotFoundFault
    Public Sub New()

    End Sub

    Public Sub New(ByVal pCompanyNotfound As CompanyNotFoundException)
        Me.CompanyCode = pCompanyNotfound.CompanyCode
    End Sub

    <DataMember(Name:="CompanyCode", IsRequired:=True)>
    Public Property CompanyCode As String

End Class
