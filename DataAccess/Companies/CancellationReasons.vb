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
Partial Public Class CancellationReasons
         Inherits BaseEntity
         Implements ICompanyEntity, IRecordCreateModifyInfo 
    Public Property CancellationId As System.Guid

	Public Overrides ReadOnly Property Id As System.Guid
		Get
			Return CancellationId
		End Get
	End Property
    Public Property Description As String
    Public Property Code As String
    Public Property CompanyId As System.Guid
    Public Property REFUND_COMPUTE_METHOD_ID As System.Guid
    Public Property REFUND_DESTINATION_ID As System.Guid
    Public Property INPUT_AMT_REQ_ID As System.Guid
    Public Property CreatedDate As Date Implements IRecordCreateModifyInfo.CreatedDate
    Public Property ModifiedDate As Nullable(Of Date) Implements IRecordCreateModifyInfo.ModifiedDate
    Public Property CreatedBy As String Implements IRecordCreateModifyInfo.CreatedBy
    Public Property ModifiedBy As String Implements IRecordCreateModifyInfo.ModifiedBy
    Public Property ACTIVE_FLAG As String
    Public Property DISPLAY_CODE_ID As Nullable(Of System.Guid)

    Public Overridable Property Company As Company

End Class
#End If
