Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.DataEntities

Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/GetCertificate", Name:="CompanyInfo")>
    Public Class CompanyInfo
        <DataMember(Name:="CompanyNumber")>
        Public Property CompanyCode As String

        <DataMember(Name:="CompanyName")>
        Public Property CompanyName As String

        Public Sub New()

        End Sub

        Public Sub New(pCompany As Company)
            CompanyCode = pCompany.Code
            CompanyName = pCompany.Description
        End Sub
    End Class
End Namespace
