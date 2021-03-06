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
Partial Public Class EntityIssue
         Inherits BaseEntity
         Implements IClaimEntity, IRecordCreateModifyInfo 
    Public Property EntityIssueId As System.Guid

	Public Overrides ReadOnly Property Id As System.Guid
		Get
			Return EntityIssueId
		End Get
	End Property
    Public Property Entity As String
    Public Property EntityId As System.Guid
    Public Property IssueId As System.Guid
    Public Property CreatedBy As String Implements IRecordCreateModifyInfo.CreatedBy
    Public Property CreatedDate As Date Implements IRecordCreateModifyInfo.CreatedDate
    Public Property ModifiedBy As String Implements IRecordCreateModifyInfo.ModifiedBy
    Public Property ModifiedDate As Nullable(Of Date) Implements IRecordCreateModifyInfo.ModifiedDate
    Public Property WORKQUEUE_ITEM_CREATED_ID As Nullable(Of System.Guid)

    Public Overridable Property Claim As Claim
    Public Overridable Property ClaimIssueResponses As ICollection(Of ClaimIssueResponse) = New HashSet(Of ClaimIssueResponse)
    Public Overridable Property ClaimIssueStatuses As ICollection(Of ClaimIssueStatus) = New HashSet(Of ClaimIssueStatus)

	Public Function ShalowClone() As EntityIssue
		Dim returnValue As EntityIssue
		returnValue = DirectCast(Me.MemberwiseClone(), EntityIssue)
		returnValue.EntityIssueId = Guid.NewGuid()
		Return returnValue
	End Function

End Class
#End If
