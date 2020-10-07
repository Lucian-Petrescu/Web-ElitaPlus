'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/2/2015)  ********************

Public Class DefaultClaimStatus
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
            Dim dal As New DefaultClaimStatusDAL
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
            Dim dal As New DefaultClaimStatusDAL
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
        CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(DefaultClaimStatusDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DefaultClaimStatusDAL.COL_NAME_DEFAULT_CLAIM_STATUS_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ClaimStatusByGroupId As Guid
        Get
            CheckDeleted()
            If row(DefaultClaimStatusDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DefaultClaimStatusDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DefaultClaimStatusDAL.COL_NAME_CLAIM_STATUS_BY_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DefaultTypeId As Guid
        Get
            CheckDeleted()
            If row(DefaultClaimStatusDAL.COL_NAME_DEFAULT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DefaultClaimStatusDAL.COL_NAME_DEFAULT_TYPE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DefaultClaimStatusDAL.COL_NAME_DEFAULT_TYPE_ID, Value)
        End Set
    End Property



    Public Property MethodOfRepairId As Guid
        Get
            CheckDeleted()
            If row(DefaultClaimStatusDAL.COL_NAME_METHOD_OF_REPAIR_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(DefaultClaimStatusDAL.COL_NAME_METHOD_OF_REPAIR_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DefaultClaimStatusDAL.COL_NAME_METHOD_OF_REPAIR_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CompanyGroupId As Guid
        Get
            CheckDeleted()
            If Row(DefaultClaimStatusDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DefaultClaimStatusDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DefaultClaimStatusDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New DefaultClaimStatusDAL
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
    Public Shared Function getList(CompanyGroupId As Guid) As DefaultClaimStatusSearchDV
        Try
            Dim dal As New DefaultClaimStatusDAL
            If CompanyGroupId.Equals(Guid.Empty) Then
                Return New DefaultClaimStatusSearchDV(dal.LoadList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
            Else
                Return New DefaultClaimStatusSearchDV(dal.LoadList(CompanyGroupId, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))
            End If

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

#Region "SearchDV"
    Public Class DefaultClaimStatusSearchDV
        Inherits DataView

        Public Const COL_DEFAULT_CLAIM_STATUS_ID As String = "default_claim_status_id"
        Public Const COL_DEFAULT_TYPE_ID As String = "default_type_id"
        Public Const COL_DEFAULT_TYPE As String = "default_type"
        Public Const COL_CLAIM_STATUS_BY_GROUP_ID As String = "claim_status_by_group_id"
        Public Const COL_CLAIM_STATUS_BY_GROUP As String = "claim_status_by_group"
        Public Const COL_METHOD_OF_REPAIR_ID As String = "method_of_repair_id"
        Public Const COL_METHOD_OF_REPAIR As String = "method_of_repair"

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

    Public Shared Sub AddNewRowToSearchDV(ByRef dv As DefaultClaimStatusSearchDV, NewBO As DefaultClaimStatus)
        Dim dt As DataTable, blnEmptyTbl As Boolean = False

        If NewBO.IsNew Then
            Dim row As DataRow
            If dv Is Nothing Then
                Dim guidTemp As New Guid
                blnEmptyTbl = True
                dt = New DataTable
                dt.Columns.Add(DefaultClaimStatusSearchDV.COL_DEFAULT_CLAIM_STATUS_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(DefaultClaimStatusSearchDV.COL_DEFAULT_TYPE_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(DefaultClaimStatusSearchDV.COL_DEFAULT_TYPE, GetType(String))
                dt.Columns.Add(DefaultClaimStatusSearchDV.COL_CLAIM_STATUS_BY_GROUP_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(DefaultClaimStatusSearchDV.COL_CLAIM_STATUS_BY_GROUP, GetType(String))
                dt.Columns.Add(DefaultClaimStatusSearchDV.COL_METHOD_OF_REPAIR_ID, guidTemp.ToByteArray.GetType)
                dt.Columns.Add(DefaultClaimStatusSearchDV.COL_METHOD_OF_REPAIR, GetType(String))

            Else
                dt = dv.Table
            End If
            row = dt.NewRow
            row(DefaultClaimStatusSearchDV.COL_DEFAULT_CLAIM_STATUS_ID) = NewBO.Id.ToByteArray
            row(DefaultClaimStatusSearchDV.COL_DEFAULT_TYPE_ID) = NewBO.DefaultTypeId.ToByteArray
            row(DefaultClaimStatusSearchDV.COL_CLAIM_STATUS_BY_GROUP_ID) = NewBO.ClaimStatusByGroupId.ToByteArray
            If Not NewBO.MethodOfRepairId.Equals(Guid.Empty) Then
                row(DefaultClaimStatusSearchDV.COL_METHOD_OF_REPAIR_ID) = NewBO.MethodOfRepairId.ToByteArray
            End If

            dt.Rows.Add(row)
            If blnEmptyTbl Then dv = New DefaultClaimStatusSearchDV(dt)
        End If
    End Sub

    Public Shared Function getEmptyList(dv As DataView) As System.Data.DataView
        Try

            Dim dsv As DataSet
            dsv = dv.Table().DataSet

            Dim row As DataRow = dsv.Tables(0).NewRow()
            row.Item(DefaultClaimStatusSearchDV.COL_DEFAULT_CLAIM_STATUS_ID) = Guid.NewGuid.ToByteArray

            row(DefaultClaimStatusSearchDV.COL_DEFAULT_TYPE_ID) = Guid.NewGuid.ToByteArray
            row.Item(DefaultClaimStatusSearchDV.COL_DEFAULT_TYPE) = String.Empty

            row(DefaultClaimStatusSearchDV.COL_CLAIM_STATUS_BY_GROUP_ID) = Guid.NewGuid.ToByteArray
            row(DefaultClaimStatusSearchDV.COL_CLAIM_STATUS_BY_GROUP) = String.Empty

            row(DefaultClaimStatusSearchDV.COL_METHOD_OF_REPAIR_ID) = Guid.NewGuid.ToByteArray
            row(DefaultClaimStatusSearchDV.COL_METHOD_OF_REPAIR) = String.Empty

            dsv.Tables(0).Rows.Add(row)
            Return New System.Data.DataView(dsv.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

End Class


