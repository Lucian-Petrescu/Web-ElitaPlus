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
Partial Public Class DefaultClaimStatus
         Inherits BaseEntity
         Implements ICompanyGroupEntity, IRecordCreateModifyInfo 
    Public Property DefaultClaimStatusId As System.Guid

	Public Overrides ReadOnly Property Id As System.Guid
		Get
			Return DefaultClaimStatusId
		End Get
	End Property
    Public Property ClaimStatusByGroupId As System.Guid
    Public Property DefaultTypeId As System.Guid
    Public Property MethodOfRepairId As Nullable(Of System.Guid)
    Public Property CreatedBy As String Implements IRecordCreateModifyInfo.CreatedBy
    Public Property CreatedDate As Date Implements IRecordCreateModifyInfo.CreatedDate
    Public Property ModifiedBy As String Implements IRecordCreateModifyInfo.ModifiedBy
    Public Property ModifiedDate As Nullable(Of Date) Implements IRecordCreateModifyInfo.ModifiedDate
    Public Property CompanyGroupId As System.Guid

    Public Overridable Property ClaimStatusByGroup As ClaimStatusByGroup
    Public Overridable Property CompanyGroup As CompanyGroup

End Class
#End If
