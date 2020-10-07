Public Class SVCPLRecon

    '************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/12/2007)  ********************

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
            Dim dal As New SVCPLReconDAL
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
            Dim dal As New SVCPLReconDAL
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
            If Row(SVCPLReconDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SVCPLReconDAL.TABLE_KEY_NAME), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")>
    Public Property ServiceCenterId() As Guid
        Get
            CheckDeleted()
            If Row(SVCPLReconDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SVCPLReconDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(SVCPLReconDAL.COL_NAME_SERVICE_CENTER_ID, Value)
        End Set
    End Property

    Public Property PriceListId() As Guid
        Get
            CheckDeleted()
            If Row(SVCPLReconDAL.COL_NAME_PRICE_LIST_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(SVCPLReconDAL.COL_NAME_PRICE_LIST_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            SetValue(SVCPLReconDAL.COL_NAME_PRICE_LIST_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property Status_xcd() As String
        Get
            CheckDeleted()
            If Row(SVCPLReconDAL.COL_NAME_STATUS_XCD) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SVCPLReconDAL.COL_NAME_STATUS_XCD), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SVCPLReconDAL.COL_NAME_STATUS_XCD, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property RequestedBy() As String
        Get
            CheckDeleted()
            If Row(SVCPLReconDAL.COL_NAME_REQUESTED_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SVCPLReconDAL.COL_NAME_REQUESTED_BY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SVCPLReconDAL.COL_NAME_REQUESTED_BY, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property ReqestedDate() As DateType
        Get
            CheckDeleted()
            If Row(SVCPLReconDAL.COL_NAME_REQUESTED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(SVCPLReconDAL.COL_NAME_REQUESTED_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(SVCPLReconDAL.COL_NAME_REQUESTED_DATE, Value)
        End Set

    End Property
    <ValueMandatory("")>
    Public Property StatusDate() As DateType
        Get
            CheckDeleted()
            If Row(SVCPLReconDAL.COL_NAME_STATUS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(SVCPLReconDAL.COL_NAME_STATUS_DATE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(SVCPLReconDAL.COL_NAME_STATUS_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property StatusChangedBy() As String
        Get
            CheckDeleted()
            If Row(SVCPLReconDAL.COL_NAME_STATUS_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SVCPLReconDAL.COL_NAME_STATUS_BY), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(SVCPLReconDAL.COL_NAME_STATUS_BY, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New SVCPLReconDAL
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


    Public Function Add(ByVal svc_price_list_recon_id As Guid, ByVal servicenterId As Guid, ByVal price_list_id As Guid, ByVal status_xcd As String, ByVal Requested_By As String) As Integer
        Dim dal As New SVCPLReconDAL
        Return dal.Add(svc_price_list_recon_id, servicenterId, price_list_id, status_xcd, Requested_By)
    End Function

    'Public Function Update(ByVal svc_price_list_recon_id As Guid, ByVal price_list_id As Guid, ByVal status_xcd As String, ByVal Requested_By As String) As Integer
    '    Dim dal As New SVCPLReconDAL
    '    Return dal.Update(svc_price_list_recon_id, price_list_id, status_xcd, Requested_By)
    'End Function

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function SVCLoadList(ByVal servicecenterid As Guid) As System.Data.DataView
        Try
            Dim dal As New SVCPLReconDAL
            Dim ds As New DataSet
            dal.LoadListByServiceCenter(ds, servicecenterid)
            Return New System.Data.DataView(ds.Tables(SVCPLReconDAL.TABLE_NAME))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function LoadLatestStatusList(ByVal servicecenterid As Guid) As DataSet
        Try
            Dim dal As New SVCPLReconDAL
            Dim ds As New DataSet
            ds = dal.GetLatestStatus(servicecenterid)
            Return ds
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function


#End Region



End Class

