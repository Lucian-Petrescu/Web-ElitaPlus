'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (8/17/2006)  ********************

Public Class ServiceNetworkSvc
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
    'Public Sub New(ByVal familyDS As DataSet, ByVal Id As Guid, ByVal ServCentId As Guid)
    '    MyBase.New(False)
    '    Me.Dataset = familyDS
    '    LoadByUserIdCompanyId(Id, ServCentId)
    'End Sub

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
            Dim dal As New ServiceNetworkSvcDAL
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
            Dim dal As New ServiceNetworkSvcDAL
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


    Protected Sub LoadByServNetIdServId(oServNetId As Guid, oServCentId As Guid)
        Try
            Dim dal As New ServiceNetworkSvcDAL

            If _isDSCreator Then
                If Row IsNot Nothing Then
                    Dataset.Tables(dal.TABLE_NAME).Rows.Remove(Row)
                End If
            End If
            Row = Nothing
            If Dataset.Tables.IndexOf(dal.TABLE_NAME) >= 0 Then
                Row = FindRow(oServNetId, dal.COL_NAME_SERVICE_NETWORK_ID, oServCentId, dal.COL_NAME_SERVICE_CENTER_ID, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then 'it is not in the dataset, so will bring it from the db
                dal.LoadByUserIdCompanyID(Dataset, oServNetId, oServCentId)
                Row = FindRow(oServNetId, dal.COL_NAME_SERVICE_NETWORK_ID, oServCentId, dal.COL_NAME_SERVICE_CENTER_ID, Dataset.Tables(dal.TABLE_NAME))
            End If
            If Row Is Nothing Then
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
    Public ReadOnly Property Id As Guid
        Get
            If Row(ServiceNetworkSvcDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceNetworkSvcDAL.COL_NAME_SERVICE_NETWORK_SVC_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ServiceNetworkId As Guid
        Get
            CheckDeleted()
            If Row(ServiceNetworkSvcDAL.COL_NAME_SERVICE_NETWORK_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceNetworkSvcDAL.COL_NAME_SERVICE_NETWORK_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceNetworkSvcDAL.COL_NAME_SERVICE_NETWORK_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ServiceCenterId As Guid
        Get
            CheckDeleted()
            If Row(ServiceNetworkSvcDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ServiceNetworkSvcDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ServiceNetworkSvcDAL.COL_NAME_SERVICE_CENTER_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ServiceNetworkSvcDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
                    Dim objId As Guid = Id
                    Dataset = New Dataset
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

#End Region
#Region "Extended Functionality"

    Public Shared Function GetNetworkServiceIDs(oServiceNetworkId As Guid) As DataView
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

    Public Function ServiceCentersIDs(isNetwork As Boolean, oMethodRepairId As Guid) As ArrayList
        Dim oSvcDv As DataView
        If moServiceCenterIDs Is Nothing Then
            If isNetwork Then
                oSvcDv = GetNetworkServiceIDs(ServiceNetworkId)
            Else
                oSvcDv = GetAllNetworkServiceIDs()
            End If

            moServiceCenterIDs = New ArrayList

            If oSvcDv.Table.Rows.Count > 0 Then
                Dim index As Integer
                ' Create Array
                For index = 0 To oSvcDv.Table.Rows.Count - 1
                    If oSvcDv.Table.Rows(index)(ServiceNetworkSvcDAL.COL_NAME_SERVICE_CENTER_ID) IsNot DBNull.Value Then
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



