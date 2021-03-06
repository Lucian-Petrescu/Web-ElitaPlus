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
Partial Public Class EquipmentListDetail
         Inherits BaseEntity
         Implements IEquipmentEntity, IRecordCreateModifyInfo 
    Public Property EquipmentListDetailId As System.Guid

	Public Overrides ReadOnly Property Id As System.Guid
		Get
			Return EquipmentListDetailId
		End Get
	End Property
    Public Property EquipmentId As System.Guid
    Public Property EquipmentListId As Nullable(Of System.Guid)
    Public Property Effective As Nullable(Of Date)
    Public Property Expiration As Nullable(Of Date)
    Public Property CreatedDate As Date Implements IRecordCreateModifyInfo.CreatedDate
    Public Property CreatedBy As String Implements IRecordCreateModifyInfo.CreatedBy
    Public Property ModifiedDate As Nullable(Of Date) Implements IRecordCreateModifyInfo.ModifiedDate
    Public Property ModifiedBy As String Implements IRecordCreateModifyInfo.ModifiedBy

    Public Overridable Property Equipment As Equipment
    Public Overridable Property EquipmentList As EquipmentList

End Class
#End If
