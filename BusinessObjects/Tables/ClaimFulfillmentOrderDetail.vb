
Public Class ClaimFulfillmentOrderDetail
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
            Dim dal As New ClaimFulfillmentOrderDetailDAL
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
            Dim dal As New ClaimFulfillmentOrderDetailDAL
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

    Public Sub Copy(ByVal original As ClaimFulfillmentOrderDetail)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Claim Fullment Order Detail")
        End If
        'Copy myself
        Me.CopyFrom(original)
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
            If Row(ClaimFulfillmentOrderDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_CF_ORDER_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)>
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_CF_ORDER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_CF_ORDER_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimFulfillmentOrderDetailDAL.COL_NAME_CF_ORDER_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=200)>
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_CF_ORDER_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_CF_ORDER_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimFulfillmentOrderDetailDAL.COL_NAME_CF_ORDER_DESCRIPTION, Value)
        End Set
    End Property

    Public Property CfOrderHeaderId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_CF_ORDER_HEADER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_CF_ORDER_HEADER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimFulfillmentOrderDetailDAL.COL_NAME_CF_ORDER_HEADER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property PriceListSourceXcd() As String
        Get
            CheckDeleted()
            If Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_PRICE_LIST_SOURCE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_PRICE_LIST_SOURCE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimFulfillmentOrderDetailDAL.COL_NAME_PRICE_LIST_SOURCE_XCD, Value)
        End Set
    End Property

    Public Property CountryId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_COUNTRY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_COUNTRY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimFulfillmentOrderDetailDAL.COL_NAME_COUNTRY_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)>
    Public Property PriceListCode() As String
        Get
            CheckDeleted()
            If Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_PRICE_LIST_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_PRICE_LIST_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimFulfillmentOrderDetailDAL.COL_NAME_PRICE_LIST_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property EquipmentTypeXcd() As String
        Get
            CheckDeleted()
            If Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_EQUIPMENT_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_EQUIPMENT_TYPE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimFulfillmentOrderDetailDAL.COL_NAME_EQUIPMENT_TYPE_XCD, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property ServiceClassXcd() As String
        Get
            CheckDeleted()
            If Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_SERVICE_CLASS_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_SERVICE_CLASS_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimFulfillmentOrderDetailDAL.COL_NAME_SERVICE_CLASS_XCD, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property ServiceTypeXcd() As String
        Get
            CheckDeleted()
            If Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_SERVICE_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_SERVICE_TYPE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimFulfillmentOrderDetailDAL.COL_NAME_SERVICE_TYPE_XCD, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property ServiceLevelXcd() As String
        Get
            CheckDeleted()
            If Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_SERVICE_LEVEL_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_SERVICE_LEVEL_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimFulfillmentOrderDetailDAL.COL_NAME_SERVICE_LEVEL_XCD, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=100)>
    Public Property StockItemTypeXcd() As String
        Get
            CheckDeleted()
            If Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_STOCK_ITEM_TYPE_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimFulfillmentOrderDetailDAL.COL_NAME_STOCK_ITEM_TYPE_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimFulfillmentOrderDetailDAL.COL_NAME_STOCK_ITEM_TYPE_XCD, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            Dim dal As New ClaimFulfillmentOrderDetailDAL
            dal.Update(Me.Row)
            'Reload the Data from the DB
            If Me.Row.RowState <> DataRowState.Detached Then
                Dim objId As Guid = Me.Id
                Me.Dataset = New DataSet
                Me.Row = Nothing
                Me.Load(objId)
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub DeleteAndSave()
        Me.BeginEdit()

        Try
            Me.Delete()
            Me.Save()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Me.cancelEdit()
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        Catch ex As RowNotInTableException
            ex = Nothing
        Catch ex As Exception
            Me.cancelEdit()
            Throw ex
        End Try
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Class CFOrderDetailSearchhDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_CODE As String = ClaimFulfillmentOrderDetailDAL.COL_NAME_CODE
        Public Const COL_NAME_DESCRIPTION As String = ClaimFulfillmentOrderDetailDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_PRICE_LIST_SOURCE As String = ClaimFulfillmentOrderDetailDAL.COL_NAME_PRICE_LIST_SOURCE
        Public Const COL_NAME_COUNTRY As String = ClaimFulfillmentOrderDetailDAL.COL_NAME_COUNTRY
        Public Const COL_NAME_PRICE_LIST_CODE As String = ClaimFulfillmentOrderDetailDAL.COL_NAME_PRICE_LIST_CODE
        Public Const COL_NAME_EQUIPMENT_TYPE As String = ClaimFulfillmentOrderDetailDAL.COL_NAME_EQUIPMENT_TYPE
        Public Const COL_NAME_CF_ORDER_DETAIL_ID As String = ClaimFulfillmentOrderDetailDAL.COL_NAME_CF_ORDER_DETAIL_ID

#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Shared Function GetList(ByVal Code As String, ByVal Description As String, ByVal PriceListSource As String) As CFOrderDetailSearchhDV
        Try
            Dim dal As New ClaimFulfillmentOrderDetailDAL
            Return New CFOrderDetailSearchhDV(dal.LoadList(Code, Description, PriceListSource, Authentication.LangId).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Function CFCodeExists(ByVal Code As String) As Boolean
        Dim dal As New ClaimFulfillmentOrderDetailDAL
        Dim lngItemNum As Long = 0
        Return (dal.CFCodeExists(Code))
    End Function

#End Region

End Class


