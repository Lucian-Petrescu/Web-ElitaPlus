'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/5/2017)  ********************

Public Class CertRegisteredItemHist
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
            Dim dal As New CertRegisteredItemHistDAL
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
            Dim dal As New CertRegisteredItemHistDAL            
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
            If row(CertRegisteredItemHistDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertRegisteredItemHistDAL.COL_NAME_ELP_CERT_REGISTERED_ITEM_HIST), Byte()))
            End If
        End Get
    End Property
	
    
    Public Property CertRegisteredItemId As Guid
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_CERT_REGISTERED_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertRegisteredItemHistDAL.COL_NAME_CERT_REGISTERED_ITEM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_CERT_REGISTERED_ITEM_ID, Value)
        End Set
    End Property
	
	
    
    Public Property CertItemId As Guid
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_CERT_ITEM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertRegisteredItemHistDAL.COL_NAME_CERT_ITEM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_CERT_ITEM_ID, Value)
        End Set
    End Property
	
	
    
    Public Property ManufacturerId As Guid
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_MANUFACTURER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertRegisteredItemHistDAL.COL_NAME_MANUFACTURER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_MANUFACTURER_ID, Value)
        End Set
    End Property
	
	
    
    Public Property ProdItemManufEquipId As Guid
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_PROD_ITEM_MANUF_EQUIP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertRegisteredItemHistDAL.COL_NAME_PROD_ITEM_MANUF_EQUIP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_PROD_ITEM_MANUF_EQUIP_ID, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=1020)> _
    Public Property Manufacturer As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_MANUFACTURER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemHistDAL.COL_NAME_MANUFACTURER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_MANUFACTURER, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=200)> _
    Public Property Model As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_MODEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemHistDAL.COL_NAME_MODEL), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_MODEL, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=120)> _
    Public Property SerialNumber As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemHistDAL.COL_NAME_SERIAL_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_SERIAL_NUMBER, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=400)> _
    Public Property ItemDescription As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_ITEM_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemHistDAL.COL_NAME_ITEM_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_ITEM_DESCRIPTION, Value)
        End Set
    End Property
	
	
    
    Public Property RetailPrice As DecimalType
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_RETAIL_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(CertRegisteredItemHistDAL.COL_NAME_RETAIL_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_RETAIL_PRICE, Value)
        End Set
    End Property
	
	
    
    Public Property PurchasedDate As DateType
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_PURCHASED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertRegisteredItemHistDAL.COL_NAME_PURCHASED_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_PURCHASED_DATE, Value)
        End Set
    End Property
	
	
    
    Public Property PurchasePrice As DecimalType
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_PURCHASE_PRICE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DecimalType(CType(row(CertRegisteredItemHistDAL.COL_NAME_PURCHASE_PRICE), Decimal))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_PURCHASE_PRICE, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=4)> _
    Public Property EnrollmentItem As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_ENROLLMENT_ITEM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemHistDAL.COL_NAME_ENROLLMENT_ITEM), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_ENROLLMENT_ITEM, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=120)> _
    Public Property ItemStatus As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_ITEM_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemHistDAL.COL_NAME_ITEM_STATUS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_ITEM_STATUS, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=120)> _
    Public Property ValidatedBy As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_VALIDATED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemHistDAL.COL_NAME_VALIDATED_BY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_VALIDATED_BY, Value)
        End Set
    End Property
	
	
    
    Public Property ValidatedDate As DateType
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_VALIDATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertRegisteredItemHistDAL.COL_NAME_VALIDATED_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_VALIDATED_DATE, Value)
        End Set
    End Property
	
	
    <ValueMandatory(""),ValidStringLength("", Max:=120)> _
    Public Property HistCreatedBy As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_HIST_CREATED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemHistDAL.COL_NAME_HIST_CREATED_BY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_HIST_CREATED_BY, Value)
        End Set
    End Property
	
	
    <ValueMandatory("")> _
    Public Property HistCreatedDate As DateType
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_HIST_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertRegisteredItemHistDAL.COL_NAME_HIST_CREATED_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_HIST_CREATED_DATE, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=120)> _
    Public Property HistModifiedBy As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_HIST_MODIFIED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemHistDAL.COL_NAME_HIST_MODIFIED_BY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_HIST_MODIFIED_BY, Value)
        End Set
    End Property
	
	
    
    Public Property HistModifiedDate As DateType
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_HIST_MODIFIED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(CertRegisteredItemHistDAL.COL_NAME_HIST_MODIFIED_DATE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_HIST_MODIFIED_DATE, Value)
        End Set
    End Property
	
	
    
    Public Property DeviceTypeId As Guid
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_DEVICE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(CertRegisteredItemHistDAL.COL_NAME_DEVICE_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_DEVICE_TYPE_ID, Value)
        End Set
    End Property
	
	
    <ValidStringLength("", Max:=1020)> _
    Public Property RegisteredItemName As String
        Get
            CheckDeleted()
            If row(CertRegisteredItemHistDAL.COL_NAME_REGISTERED_ITEM_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(CertRegisteredItemHistDAL.COL_NAME_REGISTERED_ITEM_NAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(CertRegisteredItemHistDAL.COL_NAME_REGISTERED_ITEM_NAME, Value)
        End Set
    End Property
	
	
   

#End Region

#Region "Public Members"
    Public Overrides Sub Save()         
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New CertRegisteredItemHistDAL
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
#Region "CertRegItemHistSearchDV"
    Public Class CertRegItemHistSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CERT_REGISTERED_ITEM_HIST_ID As String = "cert_registered_item_hist_id"
        Public Const COL_REGISTERED_ITEM_NAME As String = "registered_item_name"
        Public Const COL_DEVICE_TYPE As String = "device_type"
        Public Const COL_ITEM_DESCRIPTION As String = "item_description"
        Public Const COL_MAKE As String = "make"
        Public Const COL_MODEL As String = "model"
        Public Const COL_PURCHASE_DATE As String = "purchased_date"
        Public Const COL_PURCHASE_PRICE As String = "purchase_price"
        Public Const COL_SERIAL_NUMBER As String = "Serial_Number"
        Public Const COL_ITEM_REGISTRATION_STATUS As String = "item_status"
        Public Const COL_MODIFIED_DATE As String = "modified_date"
        'REQ-6002
        Public Const COL_ITEM_REGISTRATION_DATE As String = "registration_date"
        Public Const COL_ITEM_RETAIL_PRICE As String = "retail_price"
        Public Const COL_ITEM_INDIXID As String = "indixid"
        Public Const COL_EXPIRATION_DATE As String = "expiration_date"

#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region
    Public Shared Function getHistoryList(CertRegItemId As Guid) As CertRegItemHistSearchDV
        Try
            Dim dal As New CertRegisteredItemHistDAL
            Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
            Return New CertRegItemHistSearchDV(dal.LoadRegItemHistList(CertRegItemId, langId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class


