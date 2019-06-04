Imports System.Collections.Generic
Imports System.Reflection

Public Class BestReplacement
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Me.Dataset = New DataSet
        Me.Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Me.Dataset = familyDS
        Me.Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Me.Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New BestReplacementDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New BestReplacementDAL
            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Me.Dataset, id)
                Me.Row = Me.FindRow(id, dal.TABLE_KEY_NAME, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub
#End Region

#Region "Private Members"
    'Initialization code for new objects
    Private Sub Initialize()
        Me.Priority = New LongType(0)
    End Sub
#End Region

#Region "Constants"
    Private Const BEST_REPLACEMENT_FORM001 As String = "BEST_REPLACEMENT_FORM001" ' Equipment and Replacement Equipment combination is Duplicate.
    Private Const BEST_REPLACEMENT_FORM002 As String = "BEST_REPLACEMENT_FORM002" ' Priority is Duplicate.
    Private Const BEST_REPLACEMENT_FORM003 As String = "BEST_REPLACEMENT_FORM003" ' Priority should be between 0 and 999
    Private Const MIN_PRIORITY As Double = 1
    Private Const MAX_PRIORITY As Double = 999
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(BestReplacementDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BestReplacementDAL.TABLE_KEY_NAME), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property MigrationPathId() As Guid
        Get
            CheckDeleted()
            If Row(BestReplacementDAL.COL_NAME_MIGRATION_PATH_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BestReplacementDAL.COL_NAME_MIGRATION_PATH_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(BestReplacementDAL.COL_NAME_MIGRATION_PATH_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property EquipmentId() As Guid
        Get
            CheckDeleted()
            If Row(BestReplacementDAL.COL_NAME_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BestReplacementDAL.COL_NAME_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(BestReplacementDAL.COL_NAME_EQUIPMENT_ID, Value)
            ' Set Equipment Model
            Dim dv As DataView = LookupListNew.GetEquipmentLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Dim model As String = LookupListNew.GetCodeFromId(dv, Value)
            Me.SetValue(BestReplacementDAL.COL_NAME_EQUIPMENT_MODEL, model)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property EquipmentManufacturerId() As Guid
        Get
            CheckDeleted()
            If Row(BestReplacementDAL.COL_NAME_EQUIPMENT_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BestReplacementDAL.COL_NAME_EQUIPMENT_MANUFACTURER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(BestReplacementDAL.COL_NAME_EQUIPMENT_MANUFACTURER_ID, Value)
            'Set Manufacturer Description
            Dim dv As DataView = LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Dim manufacturerDescription As String = LookupListNew.GetDescriptionFromId(dv, Value)
            Me.SetValue(BestReplacementDAL.COL_NAME_EQUIPMENT_MANUFACTURER, manufacturerDescription)
        End Set
    End Property

    <ValueMandatory("")> _
    Public ReadOnly Property EquipmentManufacturer() As String
        Get
            CheckDeleted()
            If Row(BestReplacementDAL.COL_NAME_EQUIPMENT_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BestReplacementDAL.COL_NAME_EQUIPMENT_MANUFACTURER), String)
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public ReadOnly Property EquipmentModel() As String
        Get
            CheckDeleted()
            If Row(BestReplacementDAL.COL_NAME_EQUIPMENT_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BestReplacementDAL.COL_NAME_EQUIPMENT_MODEL), String)
            End If
        End Get
    End Property

    <ValueMandatory(""), CheckDuplicateEquipmentReplacementEquipmentCombination("")> _
    Public Property ReplacementEquipmentId() As Guid
        Get
            CheckDeleted()
            If Row(BestReplacementDAL.COL_NAME_REPLACEMENT_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BestReplacementDAL.COL_NAME_REPLACEMENT_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(BestReplacementDAL.COL_NAME_REPLACEMENT_EQUIPMENT_ID, Value)
            ' Set Replacement Equipment Model
            Dim dv As DataView = LookupListNew.GetEquipmentLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Dim replacementModel As String = LookupListNew.GetCodeFromId(dv, Value)
            Me.SetValue(BestReplacementDAL.COL_NAME_REPLACEMENT_EQUIPMENT_MODEL, replacementModel)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ReplacementEquipmentManufacturerId() As Guid
        Get
            CheckDeleted()
            If Row(BestReplacementDAL.COL_NAME_REPLACEMENT_EQUIPMENT_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(BestReplacementDAL.COL_NAME_REPLACEMENT_EQUIPMENT_MANUFACTURER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(BestReplacementDAL.COL_NAME_REPLACEMENT_EQUIPMENT_MANUFACTURER_ID, Value)
            'Set Replacement Manufacturer Description
            Dim dv As DataView = LookupListNew.GetManufacturerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
            Dim replacementManufacturerDescription As String = LookupListNew.GetDescriptionFromId(dv, Value)
            Me.SetValue(BestReplacementDAL.COL_NAME_REPLACEMENT_EQUIPMENT_MANUFACTURER, replacementManufacturerDescription)
        End Set
    End Property

    <ValueMandatory("")> _
    Public ReadOnly Property ReplacementEquipmentManufacturer() As String
        Get
            CheckDeleted()
            If Row(BestReplacementDAL.COL_NAME_REPLACEMENT_EQUIPMENT_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BestReplacementDAL.COL_NAME_REPLACEMENT_EQUIPMENT_MANUFACTURER), String)
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public ReadOnly Property ReplacementEquipmentModel() As String
        Get
            CheckDeleted()
            If Row(BestReplacementDAL.COL_NAME_REPLACEMENT_EQUIPMENT_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(BestReplacementDAL.COL_NAME_REPLACEMENT_EQUIPMENT_MODEL), String)
            End If
        End Get
    End Property

    <ValueMandatory(""), CheckDuplicatePriorities(""), ValidNumericRange("LowPrice", MIN:=MIN_PRIORITY, Max:=MAX_PRIORITY, Message:=BEST_REPLACEMENT_FORM003)> _
    Public Property Priority() As LongType
        Get
            CheckDeleted()
            If Row(BestReplacementDAL.COL_NAME_PRIORITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(BestReplacementDAL.COL_NAME_PRIORITY), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(BestReplacementDAL.COL_NAME_PRIORITY, Value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New BestReplacementDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub Copy(ByVal original As BestReplacement)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Best Replacement.")
        End If
        MyBase.CopyFrom(original)
    End Sub

#End Region

#Region "Custom Validation"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class CheckDuplicatePriorities
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, BEST_REPLACEMENT_FORM002)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BestReplacement = CType(objectToValidate, BestReplacement)
            If (obj.CheckForDuplicatePriorities) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class


    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class CheckDuplicateEquipmentReplacementEquipmentCombination
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, BEST_REPLACEMENT_FORM001)
        End Sub

        Public Overrides Function IsValid(ByVal valueToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As BestReplacement = CType(objectToValidate, BestReplacement)
            If (obj.CheckForDuplicateEquipmentReplacementEquipmentCombination) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

    Protected Function CheckForDuplicatePriorities() As Boolean
        Dim row As DataRow
        For Each row In Me.Dataset.Tables(BestReplacementDAL.TABLE_NAME).Rows
            If row.RowState <> DataRowState.Deleted And row.RowState <> DataRowState.Detached Then
                Dim bo As New BestReplacement(row)
                If Not Me.Priority Is Nothing AndAlso Not bo.Priority Is Nothing Then 'DEF-2109
                    ' Check if combination of Equipment ID and Priority is Unique
                    If (not bo.Id.Equals(Me.Id)) AndAlso Me.Priority.Value = bo.Priority.Value andalso bo.EquipmentId = me.EquipmentId Then
                        Return True
                    End If
                End If
            End If
        Next
        Return False
    End Function

    Protected Function CheckForDuplicateEquipmentReplacementEquipmentCombination() As Boolean
        Dim row As DataRow
        For Each row In Me.Dataset.Tables(BestReplacementDAL.TABLE_NAME).Rows
            If row.RowState <> DataRowState.Deleted And row.RowState <> DataRowState.Detached Then
                Dim bo As New BestReplacement(row)
                ' Check if combination of Equipment ID and Replacement Equipment is Unique
                If Not bo.Id.Equals(Me.Id) AndAlso Me.EquipmentId.Equals(bo.EquipmentId) AndAlso Me.ReplacementEquipmentId.Equals(bo.ReplacementEquipmentId) Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function

#End Region

#Region "Best Replacement Options"

    Public Shared Function GetReplacementEquipments(ByVal migrationPathId As Guid, ByVal equipmentId As Guid, ByVal numberOfEquipments As Integer) As List(Of BestReplacementOptions)
        Try
            Dim dal As New BestReplacementDAL
            Dim RetValues As List(Of BestReplacementOptions)
            Return FromDataTableToList(dal.GetReplacementEquipments(migrationPathId, equipmentId, numberOfEquipments).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class BestReplacementOptions
        Public Manufacturer_id As Guid
        Public Equipment_id As Guid
        Public Make As String
        Public Model As String
        'Public Property Device_SKU As String
        Public Priority As Integer
    End Class

    Public Shared Function FromDataTableToList(dt As DataTable) As List(Of BestReplacementOptions)
        Dim genericList As New List(Of BestReplacementOptions)
        Dim type_BestRepOpt As Type = GetType(BestReplacementOptions)
        Dim Propinfo() As PropertyInfo = type_BestRepOpt.GetProperties

        For Each dtrow As DataRow In dt.Rows
            Dim obj As Object = Activator.CreateInstance(type_BestRepOpt)
            Propinfo = type_BestRepOpt.GetProperties
            For Each prop As PropertyInfo In Propinfo
                Dim columnValue As Object = dtrow(prop.Name.ToLower)
                If Not columnValue Is Nothing Then
                    Select Case prop.PropertyType.Name
                        Case "Guid"
                            Dim GuidValue1 As New Guid(CType(columnValue, Byte()))
                            prop.SetValue(obj, GuidValue1, Nothing)
                        Case Else
                            prop.SetValue(obj, columnValue, Nothing)
                    End Select

                End If
            Next
            genericList.Add(CType(obj, BestReplacementOptions))
        Next
        Return genericList
    End Function

#End Region

    ' Code commented as part of DEF-2109
    'Private Shared Function Guid() As Object
    '    Throw New NotImplementedException
    'End Function

End Class
