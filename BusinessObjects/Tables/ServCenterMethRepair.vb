'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/26/2009)  ********************

Public Class ServCenterMethRepair
    Inherits BusinessObjectBase
    Public Const TableName As String = "ELP_SERV_CENTER_METH_REPAIR"
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
            Dim dal As New ServCenterMethRepairDAL
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
            Dim dal As New ServCenterMethRepairDAL
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
            If row(ServCenterMethRepairDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ServCenterMethRepairDAL.COL_NAME_SERV_CENTER_METH_REPAIR_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ServCenterMorId() As Guid
        Get
            CheckDeleted()
            If row(ServCenterMethRepairDAL.COL_NAME_SERV_CENTER_MOR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ServCenterMethRepairDAL.COL_NAME_SERV_CENTER_MOR_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ServCenterMethRepairDAL.COL_NAME_SERV_CENTER_MOR_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property ServiceCenterId() As Guid
        Get
            CheckDeleted()
            If row(ServCenterMethRepairDAL.COL_NAME_SERVICE_CENTER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ServCenterMethRepairDAL.COL_NAME_SERVICE_CENTER_ID), Byte()))
            End If
        End Get
        Set(ByVal value As Guid)
            CheckDeleted()
            Me.SetValue(ServCenterMethRepairDAL.COL_NAME_SERVICE_CENTER_ID, value)
        End Set
    End Property
    <ValidNumericRange("", Min:=0, Max:=999), ValueMandatory("")>
    Public Property ServiceWarrantyDays() As LongType
        Get
            CheckDeleted()
            If Row(ServCenterMethRepairDAL.ColNameServCenterServiceWarrantyDays) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ServCenterMethRepairDAL.ColNameServCenterServiceWarrantyDays), Long))
            End If
        End Get
        Set(ByVal value As LongType)
            CheckDeleted()
            SetValue(ServCenterMethRepairDAL.ColNameServCenterServiceWarrantyDays, value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ServCenterMethRepairDAL
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
    Public Sub SaveWithoutCheckDsCreator()
        Try
            MyBase.Save()
            If IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ServCenterMethRepairDAL
                dal.Update(Row)
                'Reload the Data from the DB
                If Row.RowState <> DataRowState.Detached Then
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
#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function GetMethodOfRepairList(ByVal serviceCenterId As Guid) As Generic.List(Of ServCenterMethRepair)
        Dim dal As New ServCenterMethRepairDAL
        Dim ds As DataSet = dal.GetSelectedListMor(serviceCenterId)
        Dim morList As New Generic.List(Of ServCenterMethRepair)
        For Each dr As DataRow In ds.Tables(0).Rows
            morList.Add(New ServCenterMethRepair(dr))
        Next
        Return morList
    End Function
#End Region

End Class


