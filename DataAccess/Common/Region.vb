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
Partial Public Class Region
         Inherits BaseEntity
         Implements ICountryEntity, IRecordCreateModifyInfo 
    Public Property RegionId As System.Guid

	Public Overrides ReadOnly Property Id As System.Guid
		Get
			Return RegionId
		End Get
	End Property
    Public Property Description As String
    Public Property CountryId As System.Guid
    Public Property CreatedDate As Date Implements IRecordCreateModifyInfo.CreatedDate
    Public Property ModifiedDate As Nullable(Of Date) Implements IRecordCreateModifyInfo.ModifiedDate
    Public Property ShortDesc As String
    Public Property CreatedBy As String Implements IRecordCreateModifyInfo.CreatedBy
    Public Property ModifiedBy As String Implements IRecordCreateModifyInfo.ModifiedBy
    Public Property ACCOUNTING_CODE As String
    Public Property INVOICETAX_GL_ACCT As String

    Public Overridable Property Country As Country

End Class
#End If
