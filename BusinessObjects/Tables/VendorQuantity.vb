'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/27/2012)  ********************

Public Class VendorQuantity
    Inherits BusinessObjectBase

#Region "Constructors"

    'Exiting BO
    Public Sub New(id As Guid)
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
    Public Sub New(id As Guid, familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load(id)
    End Sub

    'New BO attaching to a BO family
    Public Sub New(familyDS As DataSet)
        MyBase.New(False)
        Dataset = familyDS
        Load()
    End Sub

    Public Sub New(row As DataRow)
        MyBase.New(False)
        Dataset = row.Table.DataSet
        Me.Row = row
    End Sub

    Protected Sub Load()
        Try
            Dim dal As New VendorQuantityDAL
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Dataset)
            End If
            Dim newRow As DataRow = Dataset.Tables(dal.TABLE_NAME).NewRow
            Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New VendorQuantityDAL
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
    Public Property Id As Guid
        Get
            If Row(VendorQuantityDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VendorQuantityDAL.COL_NAME_VENDOR_QUANTITY_ID), Byte()))
            End If
        End Get
        Set
            Row(VendorQuantityDAL.TABLE_KEY_NAME) = value.ToByteArray()
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ReferenceId As Guid
        Get
            CheckDeleted()
            If row(VendorQuantityDAL.COL_NAME_REFERENCE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(VendorQuantityDAL.COL_NAME_REFERENCE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VendorQuantityDAL.COL_NAME_REFERENCE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=30)> _
    Public Property TableName As String
        Get
            CheckDeleted()
            If Row(VendorQuantityDAL.COL_NAME_TABLE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VendorQuantityDAL.COL_NAME_TABLE_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VendorQuantityDAL.COL_NAME_TABLE_NAME, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=20)>
    Public Property Sku As String
        Get
            CheckDeleted()
            If Row(VendorQuantityDAL.COL_NAME_VENDOR_SKU) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VendorQuantityDAL.COL_NAME_VENDOR_SKU), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VendorQuantityDAL.COL_NAME_VENDOR_SKU, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property SkuDescription As String
        Get
            CheckDeleted()
            If Row(VendorQuantityDAL.COL_NAME_VENDOR_SKU_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VendorQuantityDAL.COL_NAME_VENDOR_SKU_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VendorQuantityDAL.COL_NAME_VENDOR_SKU_DESCRIPTION, Value)
        End Set
    End Property

    '<ValueMandatory("")> _
    'Public Property PriceListDetailId() As Guid
    '    Get
    '        CheckDeleted()
    '        If Row(VendorQuantityDAL.COL_NAME_PRICE_LIST_DETAIL_ID) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New Guid(CType(Row(VendorQuantityDAL.COL_NAME_PRICE_LIST_DETAIL_ID), Byte()))
    '        End If
    '    End Get
    '    Set(ByVal Value As Guid)
    '        CheckDeleted()
    '        Me.SetValue(VendorQuantityDAL.COL_NAME_PRICE_LIST_DETAIL_ID, Value)
    '    End Set
    'End Property

    <ValueMandatory("")> _
    Public Property Quantity As LongType
        Get
            CheckDeleted()
            If row(VendorQuantityDAL.COL_NAME_QUANTITY) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(VendorQuantityDAL.COL_NAME_QUANTITY), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VendorQuantityDAL.COL_NAME_QUANTITY, Value)
        End Set
    End Property

    Public Property EquipmentTypeId As Guid
        Get
            CheckDeleted()
            If Row(VendorQuantityDAL.COL_NAME_EQUIPMENT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VendorQuantityDAL.COL_NAME_EQUIPMENT_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VendorQuantityDAL.COL_NAME_EQUIPMENT_TYPE_ID, Value)
        End Set
    End Property

    Public Property ManufacturerId As Guid
        Get
            CheckDeleted()
            If Row(VendorQuantityDAL.COL_NAME_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VendorQuantityDAL.COL_NAME_MANUFACTURER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VendorQuantityDAL.COL_NAME_MANUFACTURER_ID, Value)
        End Set
    End Property

    Public Property JobModel As String
        Get
            CheckDeleted()
            If Row(VendorQuantityDAL.COL_NAME_JOB_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return New String(CType(Row(VendorQuantityDAL.COL_NAME_JOB_MODEL), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VendorQuantityDAL.COL_NAME_JOB_MODEL, Value)
        End Set
    End Property

    Public Property Price As DecimalType
        Get
            CheckDeleted()
            If Row(VendorQuantityDAL.COL_NAME_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(Row(VendorQuantityDAL.COL_NAME_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VendorQuantityDAL.COL_NAME_PRICE, Value)
        End Set
    End Property

    Public Property ConditionId As Guid
        Get
            CheckDeleted()
            If Row(VendorQuantityDAL.COL_NAME_CONDITION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VendorQuantityDAL.COL_NAME_CONDITION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VendorQuantityDAL.COL_NAME_CONDITION_ID, Value)
        End Set
    End Property

    Public Property Effective As DateType
        Get
            CheckDeleted()
            If Row(VendorQuantityDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(VendorQuantityDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VendorQuantityDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property

    Public Property Expiration As DateType
        Get
            CheckDeleted()
            If Row(VendorQuantityDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(VendorQuantityDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VendorQuantityDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property

    'Property to get/set the manufacturer name
    Public Property ManufacturerName As String
        Get
            CheckDeleted()
            If Row(VendorQuantityDAL.COL_NAME_MANUFACTURER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New String(CType(Row(VendorQuantityDAL.COL_NAME_MANUFACTURER_NAME), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VendorQuantityDAL.COL_NAME_MANUFACTURER_NAME, Value)
        End Set
    End Property

    'property to get/set the PriceListID
    Public Property PriceListDetailID As Guid
        Get
            CheckDeleted()
            If Row(VendorQuantityDAL.COL_NAME_PRICE_LIST_DETAIL_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(VendorQuantityDAL.COL_NAME_PRICE_LIST_DETAIL_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VendorQuantityDAL.COL_NAME_PRICE_LIST_DETAIL_ID, Value)
        End Set
    End Property

    'property to get/set the flag to check if the records are available in the vendor quantity table
    Public Property VendorQuantityRecordAvaliable As Boolean
        Get
            CheckDeleted()
            If Row(VendorQuantityDAL.COL_NAME_VENDOR_QUANTITY_AVALIABLE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(VendorQuantityDAL.COL_NAME_VENDOR_QUANTITY_AVALIABLE), Boolean)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(VendorQuantityDAL.COL_NAME_VENDOR_QUANTITY_AVALIABLE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"

    'Added manually to the code
    Public Overrides ReadOnly Property IsDirty As Boolean
        Get
            Return MyBase.IsDirty OrElse IsChildrenDirty OrElse AnyColumnHasChanged
        End Get
    End Property

    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New VendorQuantityDAL
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

    'Method to set the row State as added, so that it can be inserted in the Vendor Quantity table
    Public Sub SetRowStateAsAdded()
        'if RowState is not added or deleted, then set the rowstate as added
        If Row.RowState <> DataRowState.Deleted AndAlso Row.RowState <> DataRowState.Detached AndAlso Row.RowState <> DataRowState.Added Then
            Row.AcceptChanges()
            Row.SetAdded()
        End If
    End Sub

#End Region

#Region "DataView Retrieveing Methods"

#End Region

End Class


