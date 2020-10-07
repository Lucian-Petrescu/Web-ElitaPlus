Imports Assurant.ElitaPlus.BusinessObjectsNew
Imports System.Runtime.Serialization
Imports System.Collections.Generic

<DataContract(Name:="CoverageInfo", Namespace:="http://elita.assurant.com/DataContracts/Certificate")> _
Public Class CoverageInfo
    <DataMember(Name:="CoverageType", IsRequired:=False)> _
    Public Property CoverageType As String

    <DataMember(Name:="BeginDate", IsRequired:=False)> _
    Public Property BeginDate As Date

    <DataMember(Name:="EndDate", IsRequired:=False)> _
    Public Property EndDate As Date

    Public Sub New()

    End Sub

    Public Sub New(pCertItemCoverage As CertItemCoverage)
        BeginDate = pCertItemCoverage.BeginDate
        EndDate = pCertItemCoverage.EndDate
        CoverageType = pCertItemCoverage.CoverageTypeCode
    End Sub
End Class
