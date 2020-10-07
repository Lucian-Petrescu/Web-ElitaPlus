'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/4/2008)  ********************

Public Class WarrantyMaster
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
            Dim dal As New WarrantyMasterDAL
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

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New WarrantyMasterDAL
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
    Public ReadOnly Property Id As Guid
        Get
            If row(WarrantyMasterDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(WarrantyMasterDAL.COL_NAME_WARRANTY_MASTER_ID), Byte()))
            End If
        End Get
    End Property


    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If row(WarrantyMasterDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(WarrantyMasterDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WarrantyMasterDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property SkuNumber As String
        Get
            CheckDeleted()
            If Row(WarrantyMasterDAL.COL_NAME_SKU_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WarrantyMasterDAL.COL_NAME_SKU_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WarrantyMasterDAL.COL_NAME_SKU_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property SkuDescription As String
        Get
            CheckDeleted()
            If Row(WarrantyMasterDAL.COL_NAME_SKU_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WarrantyMasterDAL.COL_NAME_SKU_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WarrantyMasterDAL.COL_NAME_SKU_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4)> _
    Public Property ManufacturerId As String
        Get
            CheckDeleted()
            If Row(WarrantyMasterDAL.COL_NAME_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WarrantyMasterDAL.COL_NAME_MANUFACTURER_ID), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WarrantyMasterDAL.COL_NAME_MANUFACTURER_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=30)> _
    Public Property ManufacturerName As String
        Get
            CheckDeleted()
            If Row(WarrantyMasterDAL.COL_NAME_MANUFACTURER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WarrantyMasterDAL.COL_NAME_MANUFACTURER_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WarrantyMasterDAL.COL_NAME_MANUFACTURER_NAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property WarrantyType As String
        Get
            CheckDeleted()
            If Row(WarrantyMasterDAL.COL_NAME_WARRANTY_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WarrantyMasterDAL.COL_NAME_WARRANTY_TYPE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WarrantyMasterDAL.COL_NAME_WARRANTY_TYPE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property WarrantyDescription As String
        Get
            CheckDeleted()
            If Row(WarrantyMasterDAL.COL_NAME_WARRANTY_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WarrantyMasterDAL.COL_NAME_WARRANTY_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WarrantyMasterDAL.COL_NAME_WARRANTY_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=100)>
    Public Property ModelNumber As String
        Get
            CheckDeleted()
            If Row(WarrantyMasterDAL.COL_NAME_MODEL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WarrantyMasterDAL.COL_NAME_MODEL_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WarrantyMasterDAL.COL_NAME_MODEL_NUMBER, Value)
        End Set
    End Property



    Public Property WarrantyDurationParts As LongType
        Get
            CheckDeleted()
            If row(WarrantyMasterDAL.COL_NAME_WARRANTY_DURATION_PARTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(WarrantyMasterDAL.COL_NAME_WARRANTY_DURATION_PARTS), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WarrantyMasterDAL.COL_NAME_WARRANTY_DURATION_PARTS, Value)
        End Set
    End Property



    Public Property WarrantyDurationLabor As LongType
        Get
            CheckDeleted()
            If row(WarrantyMasterDAL.COL_NAME_WARRANTY_DURATION_LABOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(WarrantyMasterDAL.COL_NAME_WARRANTY_DURATION_LABOR), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WarrantyMasterDAL.COL_NAME_WARRANTY_DURATION_LABOR, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1)> _
    Public Property IsDeleted As String
        Get
            CheckDeleted()
            If Row(WarrantyMasterDAL.COL_NAME_IS_DELETED) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(WarrantyMasterDAL.COL_NAME_IS_DELETED), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WarrantyMasterDAL.COL_NAME_IS_DELETED, Value)
        End Set
    End Property

    Public Property RiskTypeId As Guid
        Get
            CheckDeleted()
            If row(WarrantyMasterDAL.COL_NAME_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(WarrantyMasterDAL.COL_NAME_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(WarrantyMasterDAL.COL_NAME_RISK_TYPE_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New WarrantyMasterDAL
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
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal compIds As ArrayList, ByVal dealerId As Guid, ByVal skuNumber As String, ByVal manufacturerName As String, ByVal modelNumber As String, ByVal warrantyType As String) As WarrantyMasterSearchDV
        Try
            Dim dal As New WarrantyMasterDAL
            Return New WarrantyMasterSearchDV(dal.LoadList(compIds, dealerId, skuNumber, manufacturerName, modelNumber, warrantyType).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#Region "WarrantyMasterSearchDV"
    Public Class WarrantyMasterSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_WARRANTY_MASTER_ID As String = "warranty_master_id"
        Public Const COL_DEALER_CODE As String = "dealer_code"
        Public Const COL_DEALER_NAME As String = "dealer_name"
        Public Const COL_SKU_NUMBER As String = "sku_number"
        Public Const COL_SKU_DESCRIPTION As String = "sku_description"
        Public Const COL_MANUFACTURER_ID As String = "manufacturer_id"
        Public Const COL_MANUFACTURER_NAME As String = "manufacturer_name"
        Public Const COL_WARRANTY_TYPE As String = "warranty_type"
        Public Const COL_WARRANTY_DESCRIPTION As String = "warranty_description"
        Public Const COL_MODEL_NUMBER As String = "model_number"
        Public Const COL_WARRANTY_DURATION_PARTS As String = "warranty_duration_parts"
        Public Const COL_WARRANTY_DURATION_LABOR As String = "warranty_duration_labor"
        Public Const COL_IS_DELETED As String = "is_deleted"
        Public Const COL_RISK_TYPE As String = "risk_type"

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region
#End Region

End Class
