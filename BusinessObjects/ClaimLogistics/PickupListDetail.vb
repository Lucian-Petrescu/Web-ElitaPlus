'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/29/2008)  ********************

Public Class PickupListDetail
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
            Dim dal As New PickupListDetailDAL
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
            Dim dal As New PickupListDetailDAL
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
        Catch ex As Exception
            Throw ex
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
            If row(PickupListDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PickupListDetailDAL.COL_NAME_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property HeaderId As Guid
        Get
            CheckDeleted()
            If row(PickupListDetailDAL.COL_NAME_HEADER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PickupListDetailDAL.COL_NAME_HEADER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PickupListDetailDAL.COL_NAME_HEADER_ID, Value)
        End Set
    End Property


    '<ValueMandatory("")> _
    'Public Property ClaimStatusId() As Guid
    '    Get
    '        CheckDeleted()
    '        If row(PickupListDetailDAL.COL_NAME_CLAIM_STATUS_ID) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New Guid(CType(row(PickupListDetailDAL.COL_NAME_CLAIM_STATUS_ID), Byte()))
    '        End If
    '    End Get
    '    Set(ByVal Value As Guid)
    '        CheckDeleted()
    '        Me.SetValue(PickupListDetailDAL.COL_NAME_CLAIM_STATUS_ID, Value)
    '    End Set
    'End Property


    <ValueMandatory("")> _
    Public Property ClaimId As Guid
        Get
            CheckDeleted()
            If row(PickupListDetailDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(PickupListDetailDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PickupListDetailDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=16)> _
    Public Property IsException As String
        Get
            CheckDeleted()
            If row(PickupListDetailDAL.COL_NAME_IS_EXCEPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(PickupListDetailDAL.COL_NAME_IS_EXCEPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PickupListDetailDAL.COL_NAME_IS_EXCEPTION, Value)
        End Set
    End Property



    'Public Property ResolutionClaimStatusId() As Guid
    '    Get
    '        CheckDeleted()
    '        If row(PickupListDetailDAL.COL_NAME_RESOLUTION_CLAIM_STATUS_ID) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return New Guid(CType(row(PickupListDetailDAL.COL_NAME_RESOLUTION_CLAIM_STATUS_ID), Byte()))
    '        End If
    '    End Get
    '    Set(ByVal Value As Guid)
    '        CheckDeleted()
    '        Me.SetValue(PickupListDetailDAL.COL_NAME_RESOLUTION_CLAIM_STATUS_ID, Value)
    '    End Set
    'End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New PickupListDetailDAL
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

    Public Shared Function UpdatePickupExceptions(xmlExceptions As DataSet) As Boolean
        Try
            Dim dal As New PickupListDetailDAL
            Dim userId As Guid = ElitaPlusIdentity.Current.ActiveUser.Id

            dal.UpdatePickupExceptions(xmlExceptions, userId)

            Return True

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function
    Public Shared Function getPickListByClaimId(claimID As Guid) As PickListDetailDV

        Dim dal As New PickupListDetailDAL

        Return New PickListDetailDV(dal.LoadByClaimId(claimID).Tables(0))

    End Function
#End Region

#Region "DataView Retrieveing Methods"

    Public Class PickListDetailDV
        Inherits DataView

#Region "Constants"
        Public Const COL_DETAIL_ID As String = "detail_id"
        Public Const COL_CLAIM_ID As String = "claim_id"
#End Region

        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub
        Public Shared ReadOnly Property DetailId(row) As Guid
            Get
                Return New Guid(CType(row(COL_DETAIL_ID), Byte()))
            End Get
        End Property
        Public Shared ReadOnly Property ClaimId(row) As Guid
            Get
                Return New Guid(CType(row(COL_CLAIM_ID), Byte()))
            End Get
        End Property


    End Class
#End Region

End Class


