Imports System.Runtime.Serialization
Imports Assurant.ElitaPlus.Business
Imports Assurant.ElitaPlus.DataEntities

Namespace SpecializedServices.GW
    <DataContract(Namespace:="http://elita.assurant.com/SpecializedServices/GwPilService/GetCertificate", Name:="EndorsementInfo")>
    Public Class CertificateEndorsementInfo
        <DataMember(IsRequired:=True, Name:="EndorsementNumber")>
        Public Property EndorsementNumber As Integer

        <DataMember(IsRequired:=True, Name:="CreatedBy")>
        Public Property CreatedBy As String

        <DataMember(IsRequired:=True, Name:="CreatedDate")>
        Public Property CreatedDate As Date

        <DataMember(IsRequired:=False, Name:="EffectiveDate")>
        Public Property EffectiveDate As Date

        <DataMember(IsRequired:=False, Name:="ExpirationDate")>
        Public Property ExpirationDate As Date
        Public Sub New()

        End Sub

        Public Sub New(ByVal pCertficateEndorsement As CertificateEndorsement,
                       ByVal pCommonManager As CommonManager,
                       ByVal pLangauge As String)
            With Me
                .EndorsementNumber = pCertficateEndorsement.EndorseNumber
                .CreatedBy = pCertficateEndorsement.CreatedBy
                .CreatedDate = pCertficateEndorsement.CreatedDate
                If Not pCertficateEndorsement.EffectiveDate Is Nothing Then
                    .EffectiveDate = pCertficateEndorsement.EffectiveDate
                End If
                If Not pCertficateEndorsement.ExpirationDate Is Nothing Then
                    .ExpirationDate = pCertficateEndorsement.ExpirationDate
                End If
            End With
        End Sub
    End Class
End Namespace