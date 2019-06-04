'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/17/2006)  ********************

Public Class ServiceNetworkSvc
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
    'Public Sub New(ByVal familyDS As DataSet, ByVal Id As Guid, ByVal ServCentId As Guid)
    '    MyBase.New(False)
    '    Me.Dataset = familyDS
    '    LoadByUserIdCompanyId(Id, ServCentId)
    'End Sub

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
            Dim dal As New ServiceNetworkSvcDAL
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
            Dim dal As New ServiceNetworkSvcDAL
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


    Protected Sub LoadByServNetIdServId(ByVal oServNetId As Guid, ByVal oServCentId As Guid)
        Try
            Dim dal As New ServiceNetworkSvcDAL

            If Me._isDSCreator Then
                If Not Me.Row Is Nothing Then
                    Me.Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Me.Row)
                End If
            End If
            Me.Row = Nothing
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Me.Row = Me.FindRow(oServNetId, dal.COL_NAME_SERVICE_NETWORK_ID, oServCentId, dal.COL_NAME_SERVICE_CENTER_ID, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.LoadByUserIdCompanyID(Me.Dataset, oServNetId, oServCentId)
                Me.Row = Me.FindRow(oServNetId, dal.COL_NAME_SERVICE_NETWORK_ID, oServCentId, dal.COL_NAME_SERVICE_CENTER_ID, Me.Dataset.Tables(dal.TABLE_NAME))
            End If
            If Me.Row Is Nothing Then
                Throw New DataNotFoundException
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

#End Region

#Region "Variables"
    Private moServiceCenterIDs As ArrayList
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
            If Row(ServiceNetworkSvcDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceNetworkSvcDAL.COL_NAME_SERVICE_NETWORK_SVC_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ServiceNetworkId() As Guid
        Get
            CheckDeleted()
            If Row(ServiceNetworkSvcDAL.COL_NAME_SERVICE_NETWORK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceNetworkSvcDAL.COL_NAME_SERVICE_NETWORK_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ServiceNetworkSvcDAL.COL_NAME_SERVICE_NETWORK_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ServiceCenterId() As Guid
        Get
            CheckDeleted()
            If Row(ServiceNetworkSvcDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceNetworkSvcDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ServiceNetworkSvcDAL.COL_NAME_SERVICE_CENTER_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ServiceNetworkSvcDAL
                dal.Update(Me.Row)
                'Reload the Data from the DB
                If Me.Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Me.Id
                    Me.Dataset = New Dataset
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

#End Region
#Region "Extended Functionality"

    Public Shared Function GetNetworkServiceIDs(ByVal oServiceNetworkId As Guid) As DataView
        Dim dal As New ServiceNetworkSvcDAL
        Dim ds As DataSet

        ds = dal.LoadNetworkServicenterIDs(oServiceNetworkId)
        Return ds.Tables(dal.TABLE_NAME).DefaultView
    End Function

    Public Shared Function GetAllNetworkServiceIDs() As DataView
        Dim dal As New ServiceNetworkSvcDAL
        Dim ds As DataSet

        ds = dal.LoadAllNetworkServicenterIDs(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        Return ds.Tables(dal.TABLE_NAME).DefaultView
    End Function

    Public Function ServiceCentersIDs(ByVal isNetwork As Boolean, ByVal oMethodRepairId As Guid) As ArrayList
        Dim oSvcDv As DataView
        If moServiceCenterIDs Is Nothing Then
            If isNetwork Then
                oSvcDv = GetNetworkServiceIDs(Me.ServiceNetworkId)
            Else
                oSvcDv = GetAllNetworkServiceIDs()
            End If

            moServiceCenterIDs = New ArrayList

            If oSvcDv.Table.Rows.Count > 0 Then
                Dim index As Integer
                ' Create Array
                For index = 0 To oSvcDv.Table.Rows.Count - 1
                    If Not oSvcDv.Table.Rows(index)(ServiceNetworkSvcDAL.COL_NAME_SERVICE_CENTER_ID) Is System.DBNull.Value Then
                        moServiceCenterIDs.Add(New Guid(CType(oSvcDv.Table.Rows(index)(ServiceNetworkSvcDAL.COL_NAME_SERVICE_CENTER_ID), Byte())))
                    End If
                Next
            End If
        End If
        Return moServiceCenterIDs
    End Function
    '----------

#End Region
End Class



