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
Partial Public Class EquipmentList
         Inherits BaseEntity
         Implements IEquipmentEntity, IRecordCreateModifyInfo 
    Public Property EquipmentListId As System.Guid

	Public Overrides ReadOnly Property Id As System.Guid
		Get
			Return EquipmentListId
		End Get
	End Property
    Public Property Code As String
    Public Property Description As String
    Public Property Comments As String
    Public Property Effective As Date
    Public Property Expiration As Date
    Public Property CreatedDate As Date Implements IRecordCreateModifyInfo.CreatedDate
    Public Property CreatedBy As String Implements IRecordCreateModifyInfo.CreatedBy
    Public Property ModifiedDate As Nullable(Of Date) Implements IRecordCreateModifyInfo.ModifiedDate
    Public Property ModifiedBy As String Implements IRecordCreateModifyInfo.ModifiedBy

    Public Overridable Property EquipmentListDetails As ICollection(Of EquipmentListDetail) = New HashSet(Of EquipmentListDetail)

End Class
#End If
