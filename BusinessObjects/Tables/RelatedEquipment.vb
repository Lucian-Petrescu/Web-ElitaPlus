'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/13/2012)  ********************

Public Class RelatedEquipment
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(ByVal id As Guid)
        MyBase.New()
        Dataset = New DataSet
        Load(id)
    End Sub

    'New BO
    Public Sub New()
        MyBase.New()
        Dataset = New DataSet
        Load()
    End Sub

    'Exiting BO attaching to a BO family
    Public Sub New(ByVal id As Guid, ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(ByVal familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(ByVal row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New RelatedEquipmentDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New RelatedEquipmentDAL
            If _isDSCreator Then
                If Not Row Is Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.Load(Dataset, id)
                Row = FindRow(id, dal.TABLE_KEY_NAME, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
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
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(RelatedEquipmentDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RelatedEquipmentDAL.COL_NAME_RELATED_EQUIPMENT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property EquipmentId() As Guid
        Get
            CheckDeleted()
            If row(RelatedEquipmentDAL.COL_NAME_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RelatedEquipmentDAL.COL_NAME_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(RelatedEquipmentDAL.COL_NAME_EQUIPMENT_ID, Value)
        End Set
    End Property

    Public Property ChildEquipmentId() As Guid
        Get
            CheckDeleted()
            If row(RelatedEquipmentDAL.COL_NAME_CHILD_EQUIPMENT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RelatedEquipmentDAL.COL_NAME_CHILD_EQUIPMENT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(RelatedEquipmentDAL.COL_NAME_CHILD_EQUIPMENT_ID, Value)
        End Set
    End Property

    Public Property IsInOemBoxId() As Guid
        Get
            CheckDeleted()
            If row(RelatedEquipmentDAL.COL_NAME_IS_IN_OEM_BOX_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RelatedEquipmentDAL.COL_NAME_IS_IN_OEM_BOX_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(RelatedEquipmentDAL.COL_NAME_IS_IN_OEM_BOX_ID, Value)
        End Set
    End Property

    Public Property IsCoveredId() As Guid
        Get
            CheckDeleted()
            If row(RelatedEquipmentDAL.COL_NAME_IS_COVERED_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RelatedEquipmentDAL.COL_NAME_IS_COVERED_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(RelatedEquipmentDAL.COL_NAME_IS_COVERED_ID, Value)
        End Set
    End Property

    Public ReadOnly Property MakeID() As Guid
        Get
            CheckDeleted()
            If Row(EquipmentDAL.COL_NAME_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EquipmentDAL.COL_NAME_MANUFACTURER_ID), Byte()))
            End If
        End Get
    End Property

    Public ReadOnly Property EquipmentTypeID() As Guid
        Get
            CheckDeleted()
            If Row(EquipmentDAL.COL_NAME_EQUIPMENT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(EquipmentDAL.COL_NAME_EQUIPMENT_TYPE_ID), Byte()))
            End If
        End Get
    End Property

    Public ReadOnly Property Model() As String
        Get
            CheckDeleted()
            If Row(EquipmentDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return String.Empty
            Else
                Return Row(EquipmentDAL.COL_NAME_MODEL).ToString
            End If
        End Get
    End Property

    Public ReadOnly Property EquipmentDescription() As String
        Get
            CheckDeleted()
            If Row(EquipmentDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return String.Empty
            Else
                Return Row(EquipmentDAL.COL_NAME_DESCRIPTION).ToString
            End If
        End Get
    End Property

#End Region

#Region "Public Members"

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New RelatedEquipmentDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub Copy(ByVal original As RelatedEquipment)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Best Replacement.")
        End If
        MyBase.CopyFrom(original)
    End Sub

    Public Function GetRelatedEquipmentList(ByVal equipmentId As Guid) As DataView
        Try
            Dim RelatedEquipDAL As RelatedEquipmentDAL
            Return RelatedEquipDAL.GetRelatedEquipmentList(equipmentId).Tables(0).DefaultView

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

    Public Shared Function GetEquipmentType(ByVal equipment_Type As Guid) As DataView
        Try
            Dim dv As DataView = LookupListNew.GetEquipmentTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Dim STR As String = dv.RowFilter.ToString
            STR &= " AND " & EquipmentDAL.COL_NAME_CODE & " <> '" & LookupListNew.GetCodeFromId(dv, equipment_Type) & "'"
            dv.RowFilter = STR
            Return dv
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

#End Region

    Public Class RelatedEquipmentList
        Inherits BusinessObjectListBase

        Public Sub New(ByVal parent As Equipment)
            MyBase.New(LoadTable(parent), GetType(RelatedEquipment), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, RelatedEquipment).EquipmentId.Equals(CType(Parent, Equipment).Id)
        End Function

        Private Shared Function LoadTable(ByVal parent As Equipment) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(RelatedEquipmentList)) Then
                    Dim dal As New RelatedEquipmentDAL
                    dal.LoadList(parent.Dataset, parent.Id)
                    parent.AddChildrenCollection(GetType(RelatedEquipmentList))
                End If
                Return parent.Dataset.Tables(RelatedEquipmentDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class


End Class



