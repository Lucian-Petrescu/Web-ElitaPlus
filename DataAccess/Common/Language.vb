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
Partial Public Class Language
         Inherits BaseEntity
         Implements ICommonEntity, IRecordCreateModifyInfo 
    Public Property LanguageId As System.Guid

	Public Overrides ReadOnly Property Id As System.Guid
		Get
			Return LanguageId
		End Get
	End Property
    Public Property Description As String
    Public Property Code As String
    Public Property CreatedDate As Date Implements IRecordCreateModifyInfo.CreatedDate
    Public Property ModifiedDate As Nullable(Of Date) Implements IRecordCreateModifyInfo.ModifiedDate
    Public Property CreatedBy As String Implements IRecordCreateModifyInfo.CreatedBy
    Public Property ModifiedBy As String Implements IRecordCreateModifyInfo.ModifiedBy
    Public Property CultureCode As String
    Public Property ActiveFlag As String
    Public Property Territory As String

End Class
#End If
