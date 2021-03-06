﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (10/11/2010)  ********************

Public Class CertItemHistory
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
            Dim dal As New CertItemHistoryDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            setvalue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New CertItemHistoryDAL
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
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(CertItemHistoryDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertItemHistoryDAL.COL_NAME_CERT_ITEM_HIST_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CertItemId() As Guid
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_CERT_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertItemHistoryDAL.COL_NAME_CERT_ITEM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_CERT_ITEM_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CertId() As Guid
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertItemHistoryDAL.COL_NAME_CERT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_CERT_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ItemNumber() As LongType
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_ITEM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(CertItemHistoryDAL.COL_NAME_ITEM_NUMBER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_ITEM_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RiskTypeId() As Guid
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_RISK_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertItemHistoryDAL.COL_NAME_RISK_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_RISK_TYPE_ID, Value)
        End Set
    End Property



    Public Property ManufacturerId() As Guid
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertItemHistoryDAL.COL_NAME_MANUFACTURER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_MANUFACTURER_ID, Value)
        End Set
    End Property



    Public Property MaxReplacementCost() As DecimalType
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_MAX_REPLACEMENT_COST) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(CertItemHistoryDAL.COL_NAME_MAX_REPLACEMENT_COST), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_MAX_REPLACEMENT_COST, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property SerialNumber() As String
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertItemHistoryDAL.COL_NAME_SERIAL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_SERIAL_NUMBER, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property Model() As String
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertItemHistoryDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_MODEL, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=120)> _
    Public Property ItemCreatedBy() As String
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_ITEM_CREATED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertItemHistoryDAL.COL_NAME_ITEM_CREATED_BY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_ITEM_CREATED_BY, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ItemCreatedDate() As DateType
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_ITEM_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertItemHistoryDAL.COL_NAME_ITEM_CREATED_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_ITEM_CREATED_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=120)> _
    Public Property ItemModifiedBy() As String
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_ITEM_MODIFIED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertItemHistoryDAL.COL_NAME_ITEM_MODIFIED_BY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_ITEM_MODIFIED_BY, Value)
        End Set
    End Property



    Public Property ItemModifiedDate() As DateType
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_ITEM_MODIFIED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertItemHistoryDAL.COL_NAME_ITEM_MODIFIED_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_ITEM_MODIFIED_DATE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertItemHistoryDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=800)> _
    Public Property ItemDescription() As String
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_ITEM_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertItemHistoryDAL.COL_NAME_ITEM_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_ITEM_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=10)> _
    Public Property ItemCode() As String
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_ITEM_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertItemHistoryDAL.COL_NAME_ITEM_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_ITEM_CODE, Value)
        End Set
    End Property



    Public Property ItemRetailPrice() As DecimalType
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_ITEM_RETAIL_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(CertItemHistoryDAL.COL_NAME_ITEM_RETAIL_PRICE), Decimal))
            End If
        End Get
        Set(ByVal Value As DecimalType)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_ITEM_RETAIL_PRICE, Value)
        End Set
    End Property



    Public Property ItemReplaceReturnDate() As DateType
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_ITEM_REPLACE_RETURN_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertItemHistoryDAL.COL_NAME_ITEM_REPLACE_RETURN_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_ITEM_REPLACE_RETURN_DATE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=40)> _
    Public Property ExternalProductCode() As String
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_EXTERNAL_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertItemHistoryDAL.COL_NAME_EXTERNAL_PRODUCT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_EXTERNAL_PRODUCT_CODE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)> _
    Public Property ProductCode() As String
        Get
            CheckDeleted()
            If row(CertItemHistoryDAL.COL_NAME_PRODUCT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertItemHistoryDAL.COL_NAME_PRODUCT_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(CertItemHistoryDAL.COL_NAME_PRODUCT_CODE, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CertItemHistoryDAL
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
#End Region

#Region "DataView Retrieveing Methods"
#Region "CertItemHistSearchDV"
    Public Class CertItemHistSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CERT_ITEM_HIST_ID As String = "cert_item_hist_id"
        Public Const COL_SERIAL_NUMBER As String = "serial_number"
        Public Const COL_MANUFACTURER As String = "manufacturer"
        Public Const COL_MODEL As String = "MODEL"
        Public Const COL_IMEI_NUMBER As String = "IMEI_number"
        Public Const COL_SKU_NUMBER As String = "sku_number"
        Public Const COL_RISK_TYPE As String = "risk_type"
        Public Const COL_ITEM_MODIFIED_DATE As String = "item_modified_date"

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

    Public Shared Function getHistoryList(ByVal certId As Guid, ByVal CertItemId As Guid) As CertItemHistSearchDV
        Try
            Dim dal As New CertItemHistoryDAL
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Return New CertItemHistSearchDV(dal.LoadItemHistList(certId, CertItemId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class


