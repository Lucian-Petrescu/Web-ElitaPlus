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
Partial Public Class SuspendedReason
         Inherits BaseEntity
         Implements IDealerEntity, IRecordCreateModifyInfo 
    Public Property SuspendedReasonId As System.Guid

	Public Overrides ReadOnly Property Id As System.Guid
		Get
			Return SuspendedReasonId
		End Get
	End Property
    Public Property DealerId As System.Guid
    Public Property Code As String
    Public Property Description As String
    Public Property ClaimAllowed As String
    Public Property CreatedBy As String Implements IRecordCreateModifyInfo.CreatedBy
    Public Property CreatedDate As Date Implements IRecordCreateModifyInfo.CreatedDate
    Public Property ModifiedBy As String Implements IRecordCreateModifyInfo.ModifiedBy
    Public Property ModifiedDate As Nullable(Of Date) Implements IRecordCreateModifyInfo.ModifiedDate

    Public Overridable Property Dealer As Dealer

End Class
#End If
