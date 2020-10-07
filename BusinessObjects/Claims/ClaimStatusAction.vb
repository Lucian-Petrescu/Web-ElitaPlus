'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/23/2012)  ********************

Public Class ClaimStatusAction
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
            Dim dal As New ClaimStatusActionDAL
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
            Dim dal As New ClaimStatusActionDAL
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

#Region "Constants"
    Public Const URL As String = "ClaimStatusActionForm.aspx"
    Public Const COL_NAME_CLAIM_STATUS_ACTION_ID As String = ClaimStatusActionDAL.COL_NAME_CLAIM_STATUS_ACTION_ID
    Public Const COL_NAME_COMPANY_GROUP_ID As String = ClaimStatusActionDAL.COL_NAME_COMPANY_GROUP_ID
    Public Const COL_NAME_ACTION_ID As String = ClaimStatusActionDAL.COL_NAME_ACTION_ID
    Public Const COL_NAME_CURRENT_STATUS_ID As String = ClaimStatusActionDAL.COL_NAME_CURRENT_STATUS_ID
    Public Const COL_NAME_NEXT_STATUS_ID As String = ClaimStatusActionDAL.COL_NAME_NEXT_STATUS_ID
    Public Const COL_NAME_ACTION As String = ClaimStatusActionDAL.COL_NAME_ACTION
    Public Const COL_NAME_CURRENT_STATUS As String = ClaimStatusActionDAL.COL_NAME_CURRENT_STATUS
    Public Const COL_NAME_NEXT_STATUS As String = ClaimStatusActionDAL.COL_NAME_NEXT_STATUS
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(ClaimStatusActionDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimStatusActionDAL.COL_NAME_CLAIM_STATUS_ACTION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyGroupId As Guid
        Get
            CheckDeleted()
            If row(ClaimStatusActionDAL.COL_NAME_COMPANY_GROUP_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimStatusActionDAL.COL_NAME_COMPANY_GROUP_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusActionDAL.COL_NAME_COMPANY_GROUP_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property ActionId As Guid
        Get
            CheckDeleted()
            If row(ClaimStatusActionDAL.COL_NAME_ACTION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimStatusActionDAL.COL_NAME_ACTION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusActionDAL.COL_NAME_ACTION_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CurrentStatusId As Guid
        Get
            CheckDeleted()
            If row(ClaimStatusActionDAL.COL_NAME_CURRENT_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimStatusActionDAL.COL_NAME_CURRENT_STATUS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusActionDAL.COL_NAME_CURRENT_STATUS_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property NextStatusId As Guid
        Get
            CheckDeleted()
            If row(ClaimStatusActionDAL.COL_NAME_NEXT_STATUS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimStatusActionDAL.COL_NAME_NEXT_STATUS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimStatusActionDAL.COL_NAME_NEXT_STATUS_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimStatusActionDAL
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
    Public Shared Function LoadList() As DataView
        Try
            Dim dal As New ClaimStatusActionDAL
            Dim ds As DataSet
            ds = dal.LoadList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Return (ds.Tables(ClaimStatusActionDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetNewDataViewRow(dv As DataView, id As Guid) As DataView

        Dim dt As DataTable
        dt = dv.Table

        Dim row As DataRow = dt.NewRow
        row(ClaimStatusActionDAL.COL_NAME_CLAIM_STATUS_ACTION_ID) = id.ToByteArray
        row(ClaimStatusActionDAL.COL_NAME_ACTION_ID) = Guid.Empty.ToByteArray
        row(ClaimStatusActionDAL.COL_NAME_CURRENT_STATUS_ID) = Guid.Empty.ToByteArray
        row(ClaimStatusActionDAL.COL_NAME_NEXT_STATUS_ID) = Guid.Empty.ToByteArray

        dt.Rows.Add(row)

        Return (dv)

    End Function

    Public Shared Function getEmptyList(dv As DataView) As System.Data.DataView
        Try

            Dim dsv As DataSet
            dsv = dv.Table().DataSet

            Dim row As DataRow = dsv.Tables(0).NewRow()
            row.Item(ClaimStatusActionDAL.COL_NAME_CLAIM_STATUS_ACTION_ID) = System.Guid.NewGuid.ToByteArray

            row(ClaimStatusActionDAL.COL_NAME_ACTION_ID) = Guid.Empty.ToByteArray
            row(ClaimStatusActionDAL.COL_NAME_CURRENT_STATUS_ID) = Guid.Empty.ToByteArray
            row(ClaimStatusActionDAL.COL_NAME_NEXT_STATUS_ID) = Guid.Empty.ToByteArray


            dsv.Tables(0).Rows.Add(row)
            Return New System.Data.DataView(dsv.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

End Class


