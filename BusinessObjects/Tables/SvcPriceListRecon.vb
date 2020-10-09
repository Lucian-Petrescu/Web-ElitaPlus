Public Class SvcPriceListRecon

    '************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/12/2007)  ********************

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
            Dim dal As New SvcPriceListReconDAL
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

    Protected Sub Load(id As Guid)
        Try
            Dim dal As New SvcPriceListReconDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
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
            If Row(SvcPriceListReconDAL.COL_NAME_SVC_PRICE_LIST_RECON_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SvcPriceListReconDAL.COL_NAME_SVC_PRICE_LIST_RECON_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property ServiceCenterId As Guid
        Get
            CheckDeleted()
            If Row(SvcPriceListReconDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SvcPriceListReconDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SvcPriceListReconDAL.COL_NAME_SERVICE_CENTER_ID, Value)
        End Set
    End Property

    Public Property PriceListId As Guid
        Get
            CheckDeleted()
            If Row(SvcPriceListReconDAL.COL_NAME_PRICE_LIST_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SvcPriceListReconDAL.COL_NAME_PRICE_LIST_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SvcPriceListReconDAL.COL_NAME_PRICE_LIST_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property Status_xcd As String
        Get
            CheckDeleted()
            If Row(SvcPriceListReconDAL.COL_NAME_STATUS_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcPriceListReconDAL.COL_NAME_STATUS_XCD), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SvcPriceListReconDAL.COL_NAME_STATUS_XCD, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property RequestedBy As String
        Get
            CheckDeleted()
            If Row(SvcPriceListReconDAL.COL_NAME_REQUESTED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcPriceListReconDAL.COL_NAME_REQUESTED_BY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SvcPriceListReconDAL.COL_NAME_REQUESTED_BY, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ReqestedDate As DateType
        Get
            CheckDeleted()
            If Row(SvcPriceListReconDAL.COL_NAME_REQUESTED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(SvcPriceListReconDAL.COL_NAME_REQUESTED_DATE).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SvcPriceListReconDAL.COL_NAME_REQUESTED_DATE, Value)
        End Set

    End Property
    <ValueMandatory("")>
    Public Property StatusDate As DateType
        Get
            CheckDeleted()
            If Row(SvcPriceListReconDAL.COL_NAME_STATUS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(SvcPriceListReconDAL.COL_NAME_STATUS_DATE).ToString()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SvcPriceListReconDAL.COL_NAME_STATUS_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property StatusChangedBy As String
        Get
            CheckDeleted()
            If Row(SvcPriceListReconDAL.COL_NAME_STATUS_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcPriceListReconDAL.COL_NAME_STATUS_BY), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(SvcPriceListReconDAL.COL_NAME_STATUS_BY, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso (IsDirty OrElse IsFamilyDirty) AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New SvcPriceListReconDAL
                'dal.Update(Me.Row)

                dal.UpdateFromSP(Dataset.Tables(SvcPriceListReconDAL.TABLE_NAME))
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

    Public Sub SaveWithoutCheckDsCreator()
        Try
            MyBase.Save()
            If IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New SvcPriceListReconDAL
                'dal.Update(Me.Row)

                dal.UpdateFromSP(Dataset.Tables(SvcPriceListReconDAL.TABLE_NAME))
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Deleted Then
                    Dim objId As Guid = Id
                    Dataset = New DataSet
                    Row = Nothing
                    Load(objId)
                End If
            End If
        Catch ex As DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub


    Public Function Add(svc_price_list_recon_id As Guid, servicenterId As Guid, price_list_id As Guid, status_xcd As String, Requested_By As String) As Integer
        Dim dal As New SvcPriceListReconDAL
        Return dal.Add(svc_price_list_recon_id, servicenterId, price_list_id, status_xcd, Requested_By)
    End Function


#End Region

#Region "DataView Retrieveing Methods"

#Region "Service Center Price List Search DataView"

    Public Class SvcPriceListReconSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_SVC_PRICE_LIST_RECON_ID As String = "svc_price_list_recon_id"
        Public Const COL_SERVICE_CENTER_ID As String = "service_center_id"
        Public Const COL_PRICE_LIST_ID As String = "price_list_id"
        Public Const COL_STATUS_XCD As String = "status_xcd"
        Public Const COL_REQUESTED_BY As String = "requested_by"
        Public Const COL_REQUESTED_DATE As String = "requested_date"
        Public Const COL_STATUS_BY As String = "status_by"
        Public Const COL_STATUS_DATE As String = "status_date"
        Public Const COL_PRICE_LIST_CODE As String = "code"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property Id(row) As Guid
            Get
                Return New Guid(CType(row(COL_SVC_PRICE_LIST_RECON_ID), Byte()))
            End Get
        End Property
        Public Shared ReadOnly Property ServiceCenterId(row) As Guid
            Get
                Return New Guid(CType(row(COL_SERVICE_CENTER_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property PriceListId(row) As Guid
            Get
                Return New Guid(CType(row(COL_PRICE_LIST_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Status_Xcd(row As DataRow) As String
            Get
                Return row(COL_STATUS_XCD).ToString
            End Get
        End Property

        Public Shared ReadOnly Property RequestedBy(row As DataRow) As String
            Get
                Return row(COL_REQUESTED_BY).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Statusy(row As DataRow) As String
            Get
                Return row(COL_STATUS_BY).ToString
            End Get
        End Property
        Public Shared ReadOnly Property RequestedDate(row As DataRow) As DateType
            Get
                Return New DateType(DateHelper.GetDateValue(row(COL_REQUESTED_DATE).ToString()))
            End Get
        End Property

        Public Shared ReadOnly Property StatusDate(row As DataRow) As DateType
            Get
                Return New DateType(DateHelper.GetDateValue(row(COL_STATUS_DATE).ToString()))
            End Get
        End Property

    End Class

#End Region

    Public Shared Function LoadList(ServiceCenterCode As String, PriceListCode As String, CountryID As Guid) As DataView
        Try
            Dim dal As New SvcPriceListReconDAL
            Dim ds As DataSet

            ds = dal.LoadList(ServiceCenterCode, PriceListCode, CountryID)
            Return (ds.Tables(SvcPriceListReconDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function LoadListBySvc(servicecenterid As Guid) As DataView
        Try
            Dim dal As New SvcPriceListReconDAL
            Dim ds As New DataSet
            ds = dal.LoadListBySvc(servicecenterid)
            Return (ds.Tables(SvcPriceListReconDAL.TABLE_NAME).DefaultView)
            'Return New SvcPriceListReconSearchDV(dal.LoadListBySvc(servicecenterid).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function LoadLatestStatusList(servicecenterid As Guid) As DataSet
        Try
            Dim dal As New SvcPriceListReconDAL
            Dim ds As New DataSet
            ds = dal.GetLatestStatus(servicecenterid)
            Return ds
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


    Public Shared Function GetList(DealerId As Guid, ProductData As DataSet) As DataView
        Try
            Dim dal As New SvcPriceListReconDAL
            Dim ds As New DataSet

            ds = dal.LoadList(DealerId, ProductData)
            Return New SvcPriceListReconSearchDV(ds.Tables(0))

        Catch ex As Exception

        End Try
    End Function

    Public Shared Function GetNewDataViewRow(dv As DataView, id As Guid, bo As SvcPriceListRecon) As DataView

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(SvcPriceListReconDAL.COL_NAME_SVC_PRICE_LIST_RECON_ID) = bo.Id.ToByteArray
            row(SvcPriceListReconDAL.COL_NAME_PRICE_LIST_ID) = bo.PriceListId.ToByteArray
            row(SvcPriceListReconDAL.COL_NAME_SERVICE_CENTER_ID) = bo.ServiceCenterId.ToByteArray
            row(SvcPriceListReconDAL.COL_NAME_STATUS_XCD) = bo.Status_xcd

            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function
#End Region



End Class

