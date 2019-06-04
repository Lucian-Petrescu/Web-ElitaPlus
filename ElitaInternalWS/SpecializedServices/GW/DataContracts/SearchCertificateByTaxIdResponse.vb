Imports System.Collections.Generic
Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataAccessInterface
Imports Assurant.ElitaPlus.DataEntities
Imports Assurant.ElitaPlus.Common

Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/SearchCertificateByTaxId", Name:="SearchCertByTaxIdResponse")>
    Public Class SearchCertificateByTaxIdResponse
        <DataMember(IsRequired:=True, Name:="TotalRecordFound")>
        Public Property TotalRecordFound As Integer

        <DataMember(IsRequired:=True, Name:="Language")>
        Public Property Language As String

        <DataMember(IsRequired:=False, Name:="ErrorMessage", EmitDefaultValue:=False)>
        Public Property ErrorMessage As String

        <DataMember(IsRequired:=False, Name:="CertificateList", EmitDefaultValue:=False)>
        Public Property Certificates As IEnumerable(Of SearchByTaxIdResultCertificateInfo)

        <DataMember(IsRequired:=False, Name:="RecordsCountByFilter", EmitDefaultValue:=False)>
        Public Property RecordsCountByFilter As IEnumerable(Of SearchFilterResult)


        Friend Sub PopulateCertificateList(ByVal pSearchResult As Collections.Generic.IEnumerable(Of DBSearchResultCertRecord),
                            ByRef pCertManager As ICertificateManager,
                            ByRef pCommonManager As ICommonManager,
                            ByRef pCompanyGroupManager As ICompanyGroupManager,
                            ByRef pdealerManager As IDealerManager,
                            ByRef paddressManager As IAddressManager,
                            ByRef pcountryManager As ICountryManager,
                            ByVal pLanguage As String)

            Me.Certificates = New List(Of SearchByTaxIdResultCertificateInfo)()

            For Each cert As DBSearchResultCertRecord In pSearchResult
                DirectCast(Me.Certificates, IList(Of SearchByTaxIdResultCertificateInfo)).Add(New SearchByTaxIdResultCertificateInfo(cert, pCertManager, pCompanyGroupManager, pCommonManager, pdealerManager, paddressManager, pcountryManager, pLanguage))
            Next

        End Sub
    End Class

    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/SearchCertificateByTaxId", Name:="CountsByFilter")>
    Public Class SearchFilterResult
        <DataMember(IsRequired:=True, Name:="Filter", Order:=1)>
        Public Property filter As SearchFilter

        <DataMember(IsRequired:=True, Name:="Count", Order:=2)>
        Public Property Count As Integer

        Public Sub New(ByVal pFilter As SearchFilter, ByVal pCount As Integer)
            Me.filter = pFilter
            Me.Count = pCount
        End Sub
    End Class

    Public Class DBSearchResultCertRecord
        Public Property CompanyGroupId As Guid
        Public Property CompanyCode As String
        Public Property CompanyDesc As String
        Public Property DealerCode As String
        Public Property DealerName As String
        Public Property DealerTypeId As Guid
        Public Property CertId As Guid
        'Public Property InsuranceActivationDate As Date
        Public Property StatusCode As String
        Public Property ContractPolicyNum As String
        Public Property BranchName As String
        Public Property MaxCoverageEndDate As Date
        Public Property ActiveCovCount As Integer
        Public Property ActiveClaimCnt As Integer

        Public Shared Function GetCertList(ByVal dsCertList As DataSet) As Collections.Generic.List(Of DBSearchResultCertRecord)
            Dim listCert As New Collections.Generic.List(Of DBSearchResultCertRecord)
            Dim rec As DBSearchResultCertRecord
            For Each dr As DataRow In dsCertList.Tables(0).Rows
                rec = New DBSearchResultCertRecord
                With rec
                    .CompanyGroupId = New Guid(CType(dr("CompanyGroupId"), Byte()))
                    .CompanyCode = CType(dr("companyCode"), String)
                    .CompanyDesc = CType(dr("CompanyDesc"), String)
                    .DealerCode = CType(dr("DealerCode"), String)
                    .DealerName = CType(dr("DealerName"), String)
                    .DealerTypeId = IIf(dr("dealer_type_id") Is DBNull.Value, Guid.Empty, New Guid(CType(dr("dealer_type_id"), Byte())))
                    .CertId = New Guid(CType(dr("cert_id"), Byte()))
                    '.InsuranceActivationDate = DateHelper.GetDateValue(dr("insurance_activation_date"))
                    .StatusCode = IIf(dr("status_code") Is DBNull.Value, String.Empty, CType(dr("status_code"), String))
                    .ContractPolicyNum = IIf(dr("contractPolicyNum") Is DBNull.Value, Nothing, CType(dr("contractPolicyNum"), String))
                    .MaxCoverageEndDate = DateHelper.GetDateValue(dr("MaxCoverageEndDate"))
                    .ActiveClaimCnt = CType(dr("ActiveClaimCnt"), Integer)
                    .ActiveCovCount = CType(dr("ActiveCovCount"), Integer)
                    If (Not dr("branch_name") Is DBNull.Value) Then
                        .BranchName = CType(dr("branch_name"), String)
                    End If
                End With
                listCert.Add(rec)
            Next
            Return listCert
        End Function
    End Class
End Namespace