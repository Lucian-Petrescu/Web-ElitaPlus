Public Class SvcPriceListRecon
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
            Dim dal As New SvcPriceListReconDAL
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
            Dim dal As New SvcPriceListReconDAL
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
    Public Property PriceListId As Guid
        Get
            If Row(SvcPriceListReconDAL.COL_NAME_PRICE_LIST_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SvcPriceListReconDAL.COL_NAME_PRICE_LIST_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(SvcPriceListReconDAL.COL_NAME_PRICE_LIST_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ServiceCenterId() As Guid
        Get
            CheckDeleted()
            If Row(SvcPriceListReconDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SvcPriceListReconDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(SvcPriceListReconDAL.COL_NAME_SERVICE_CENTER_ID, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property StatusXcd() As String
        Get
            If Row(SvcPriceListReconDAL.COL_NAME_STATUS_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcPriceListReconDAL.COL_NAME_STATUS_XCD), String)
            End If
        End Get
        Set(value As String)
            CheckDeleted()
            Me.SetValue(SvcPriceListReconDAL.COL_NAME_STATUS_XCD, value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property RequestedBy() As String
        Get
            If Row(SvcPriceListReconDAL.COL_NAME_REQUESTED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcPriceListReconDAL.COL_NAME_REQUESTED_BY), String)
            End If
        End Get
        Set(value As String)
            CheckDeleted()
            Me.SetValue(SvcPriceListReconDAL.COL_NAME_REQUESTED_BY, value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property StatusBy() As String
        Get
            If Row(SvcPriceListReconDAL.COL_NAME_STATUS_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SvcPriceListReconDAL.COL_NAME_STATUS_BY), String)
            End If
        End Get
        Set(value As String)
            CheckDeleted()
            Me.SetValue(SvcPriceListReconDAL.COL_NAME_STATUS_BY, value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property RequestedDate() As DateType
        Get
            If Row(SvcPriceListReconDAL.COL_NAME_REQUESTED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(SvcPriceListReconDAL.COL_NAME_REQUESTED_DATE), Date))
            End If
        End Get
        Set(value As DateType)
            CheckDeleted()
            Me.SetValue(SvcPriceListReconDAL.COL_NAME_REQUESTED_DATE, value)
        End Set
    End Property

    Public Property StatusDate() As DateType
        Get
            If Row(SvcPriceListReconDAL.COL_NAME_STATUS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(SvcPriceListReconDAL.COL_NAME_STATUS_DATE), Date))
            End If
        End Get
        Set(value As DateType)
            CheckDeleted()
            Me.SetValue(SvcPriceListReconDAL.COL_NAME_STATUS_DATE, value)
        End Set
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso (Me.IsDirty OrElse Me.IsFamilyDirty) AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New SvcPriceListReconDAL
                'dal.Update(Me.Row)

                'DataTable dt = New DataTable(SvcPriceListReconDAL.TABLE_NAME))
                dal.UpdateFromSP(Me.Dataset.Tables(SvcPriceListReconDAL.TABLE_NAME))
                'MyBase.UpdateFromSP(ds.Tables(Me.TABLE_NAME), Transaction, changesFilter)
                'MyBase.UpdateFamily(Me.Dataset)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New DataSet
                    Me.Row = Nothing
                    'Me.Load(objId)
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub
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
        Public Const COL_STATUS_BY As String = "status_by"
        Public Const COL_STATUS_DATE As String = "status_date"
        Public Const COL_PRICE_LIST_CODE As String = "code"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub
    End Class

#End Region

    Public Shared Function LoadList(ByVal ServiceCenterCode As String, ByVal PriceListCode As String, ByVal CountryID As Guid) As DataView
        Try
            Dim dal As New SvcPriceListReconDAL
            Dim ds As DataSet

            ds = dal.LoadList(ServiceCenterCode, PriceListCode, CountryID)
            Return (ds.Tables(SvcPriceListReconDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function LoadListBySvc(ByVal ServiceCenterID As Guid) As DataView
        Try
            Dim dal As New SvcPriceListReconDAL
            Dim ds As DataSet

            ds = dal.LoadListBySvc(ServiceCenterID)
            Return (ds.Tables(SvcPriceListReconDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetList(ByVal DealerId As Guid, ByVal ProductData As DataSet) As DataView
        Try
            Dim dal As New SvcPriceListReconDAL
            Dim ds As New DataSet

            ds = dal.LoadList(DealerId, ProductData)
            Return New SvcPriceListReconSearchDV(ds.Tables(0))

        Catch ex As Exception

        End Try
    End Function
    'Public Shared Function Load(ByVal ServiceCenterId As Guid) As DataView
    '    Try
    '        Dim dal As New SvcPriceListReconDAL
    '        Dim ds As New DataSet

    '        'ds = dal.Load(ServiceCenterId)
    '        Return New SvcPriceListReconSearchDV(ds.Tables(0))

    '    Catch ex As Exception

    '    End Try
    'End Function

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid, ByVal bo As SvcPriceListRecon) As DataView

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(SvcPriceListReconDAL.COL_NAME_SVC_PRICE_LIST_RECON_ID) = bo.Id.ToByteArray
            row(SvcPriceListReconDAL.COL_NAME_PRICE_LIST_ID) = bo.PriceListId.ToByteArray
            row(SvcPriceListReconDAL.COL_NAME_SERVICE_CENTER_ID) = bo.ServiceCenterId.ToByteArray
            row(SvcPriceListReconDAL.COL_NAME_STATUS_XCD) = bo.StatusXcd

            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function

#End Region

End Class

