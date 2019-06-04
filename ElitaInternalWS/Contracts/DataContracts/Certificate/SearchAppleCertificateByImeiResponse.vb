
Imports System.Runtime.Serialization
Namespace Certificates
    <DataContract(Name:="SearchAppleCertificateByImeiResponse", Namespace:="http://elita.assurant.com/DataContracts/Certificate")> _
    Public Class SearchAppleCertificateByImeiResponse
   
        <DataMember()> _
        public Property CertificateNumber as String

        <DataMember()> _
        public Property DealerCode as string

        <DataMember()> _
        public Property Statuscode As string

        <DataMember()> _
        public Property CustomerName as string

        <DataMember()> _
        public Property IdentificationNumber as string

        <DataMember()> _
        public Property WorkPhone as string

        <DataMember()> _
        public Property WarrantySalesDate as DateTime

        <DataMember()> _
        public Property ProductCode as string

        <DataMember()> _
        public Property SerialNumber as string

        <DataMember()> _
        public Property Imei as string

        <DataMember()> _
        public Property ItemDescription as string

        <DataMember()> _
        public Property Manufacturer as string

        <DataMember()> _
        public Property Model as string

        <DataMember()> _
        public Property SkuNumber as string

        <DataMember()> _
        public Property ServiceLineNumber as string

        <DataMember()> _
        public Property ProductDescription as string
    End Class
End Namespace
