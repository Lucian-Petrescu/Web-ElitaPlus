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
Partial Public Class Label
         Inherits BaseEntity
         Implements ICommonEntity, IRecordCreateModifyInfo 
    Public Property LabelId As System.Guid

	Public Overrides ReadOnly Property Id As System.Guid
		Get
			Return LabelId
		End Get
	End Property
    Public Property UiProgCode As String
    Public Property InUse As String
    Public Property DictionaryItemId As System.Guid
    Public Property CreatedDate As Date Implements IRecordCreateModifyInfo.CreatedDate
    Public Property ModifiedDate As Nullable(Of Date) Implements IRecordCreateModifyInfo.ModifiedDate
    Public Property CreatedBy As String Implements IRecordCreateModifyInfo.CreatedBy
    Public Property ModifiedBy As String Implements IRecordCreateModifyInfo.ModifiedBy

    Public Overridable Property DictionaryItem As DictionaryItem

End Class
#End If
