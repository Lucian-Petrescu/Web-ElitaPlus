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
Partial Public Class Equipment
         Inherits BaseEntity
         Implements IEquipmentEntity, IRecordCreateModifyInfo 
    Public Property EquipmentId As System.Guid

	Public Overrides ReadOnly Property Id As System.Guid
		Get
			Return EquipmentId
		End Get
	End Property
    Public Property Description As String
    Public Property Model As String
    Public Property MasterEquipmentId As Nullable(Of System.Guid)
    Public Property REPAIRABLE_ID As System.Guid
    Public Property ManufacturerId As System.Guid
    Public Property EquipmentClassId As System.Guid
    Public Property EquipmentTypeId As System.Guid
    Public Property CreatedDate As Date Implements IRecordCreateModifyInfo.CreatedDate
    Public Property ModifiedDate As Nullable(Of Date) Implements IRecordCreateModifyInfo.ModifiedDate
    Public Property CreatedBy As String Implements IRecordCreateModifyInfo.CreatedBy
    Public Property ModifiedBy As String Implements IRecordCreateModifyInfo.ModifiedBy
    Public Property Effective As Nullable(Of Date)
    Public Property Expiration As Nullable(Of Date)
    Public Property IsMasterEquipment As System.Guid

    Public Overridable Property Equipments As ICollection(Of Equipment) = New HashSet(Of Equipment)
    Public Overridable Property EquipmentListDetails As ICollection(Of EquipmentListDetail) = New HashSet(Of EquipmentListDetail)
    
End Class
#End If
