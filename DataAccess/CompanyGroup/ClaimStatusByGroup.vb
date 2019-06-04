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
Partial Public Class ClaimStatusByGroup
         Inherits BaseEntity
         Implements ICompanyGroupEntity, IRecordCreateModifyInfo 
    Public Property ClaimStatusByGroupId As System.Guid

	Public Overrides ReadOnly Property Id As System.Guid
		Get
			Return ClaimStatusByGroupId
		End Get
	End Property
    Public Property CompanyGroupId As Nullable(Of System.Guid)
    Public Property DealerId As Nullable(Of System.Guid)
    Public Property ListItemId As System.Guid
    Public Property StatusOrder As Decimal
    Public Property CreatedBy As String Implements IRecordCreateModifyInfo.CreatedBy
    Public Property CreatedDate As Date Implements IRecordCreateModifyInfo.CreatedDate
    Public Property ModifiedBy As String Implements IRecordCreateModifyInfo.ModifiedBy
    Public Property ModifiedDate As Nullable(Of Date) Implements IRecordCreateModifyInfo.ModifiedDate
    Public Property OwnerId As Nullable(Of System.Guid)
    Public Property SkippingAllowedId As System.Guid
    Public Property ActiveId As System.Guid
    Public Property GroupNumber As Nullable(Of Short)

    Public Overridable Property CompanyGroup As CompanyGroup
    Public Overridable Property DefaultClaimStatuses As ICollection(Of DefaultClaimStatus) = New HashSet(Of DefaultClaimStatus)

End Class
#End If
