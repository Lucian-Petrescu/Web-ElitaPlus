'------------------------------------------------------------------------------
' <auto-generated>
'    This code was generated from a template.
'
'    Manual changes to this file may cause unexpected behavior in your application.
'    Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

#if DataEntities
Partial Public Class CompanyGroup
         Inherits BaseEntity
         Implements ICompanyGroupEntity, IRecordCreateModifyInfo 
    Public Property CompanyGroupId As System.Guid

	Public Overrides ReadOnly Property Id As System.Guid
		Get
			Return CompanyGroupId
		End Get
	End Property
    Public Property Description As String
    Public Property Code As String
    Public Property CreatedDate As Date Implements IRecordCreateModifyInfo.CreatedDate
    Public Property ModifiedDate As Nullable(Of Date) Implements IRecordCreateModifyInfo.ModifiedDate
    Public Property CreatedBy As String Implements IRecordCreateModifyInfo.CreatedBy
    Public Property ModifiedBy As String Implements IRecordCreateModifyInfo.ModifiedBy
    Public Property CLAIM_NUMBERING_BY_ID As System.Guid
    Public Property ACCT_BY_COMPANY As String
    Public Property INVOICE_NUMBERING_BY_ID As System.Guid
    Public Property FTP_SITE_ID As Nullable(Of System.Guid)
    Public Property INVOICE_GROUP_NUMBERING_BY_ID As System.Guid
    Public Property PAYMENT_GROUP_NUMBERING_BY_ID As System.Guid
    Public Property YEARS_TO_INACTIVE_USEDVEHICLES As Nullable(Of Short)
    Public Property INACTIVE_NEWVEHICLES_BASED_ON As Nullable(Of System.Guid)
    Public Property AUTHORIZATION_NUMBERING_BY_ID As System.Guid
    Public Property CLAIM_FAST_APPROVAL_ID As System.Guid

    Public Overridable Property RiskTypes As ICollection(Of RiskType) = New HashSet(Of RiskType)
    Public Overridable Property Manufacturers As ICollection(Of Manufacturer) = New HashSet(Of Manufacturer)
    Public Overridable Property PaymentTypes As ICollection(Of PaymentType) = New HashSet(Of PaymentType)
    Public Overridable Property CoverageLosses As ICollection(Of CoverageLoss) = New HashSet(Of CoverageLoss)
    Public Overridable Property ClaimStatusByGroups As ICollection(Of ClaimStatusByGroup) = New HashSet(Of ClaimStatusByGroup)
    Public Overridable Property DefaultClaimStatuses As ICollection(Of DefaultClaimStatus) = New HashSet(Of DefaultClaimStatus)

End Class
#End If
