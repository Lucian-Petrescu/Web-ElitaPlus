'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (2/23/2010)  ********************

Public Class ClaimSystem
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
            Dim dal As New ClaimSystemDAL
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
            Dim dal As New ClaimSystemDAL
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
    Public ReadOnly Property Id As Guid
        Get
            If row(ClaimSystemDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimSystemDAL.COL_NAME_CLAIM_SYSTEM_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=40)> _
    Public Property Code As String
        Get
            CheckDeleted()
            If row(ClaimSystemDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimSystemDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSystemDAL.COL_NAME_CODE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=200)> _
    Public Property Description As String
        Get
            CheckDeleted()
            If row(ClaimSystemDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimSystemDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSystemDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property



    Public Property NewClaimId As Guid
        Get
            CheckDeleted()
            If row(ClaimSystemDAL.COL_NAME_NEW_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimSystemDAL.COL_NAME_NEW_CLAIM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSystemDAL.COL_NAME_NEW_CLAIM_ID, Value)
        End Set
    End Property



    Public Property PayClaimId As Guid
        Get
            CheckDeleted()
            If row(ClaimSystemDAL.COL_NAME_PAY_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimSystemDAL.COL_NAME_PAY_CLAIM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSystemDAL.COL_NAME_PAY_CLAIM_ID, Value)
        End Set
    End Property



    Public Property MaintainClaimId As Guid
        Get
            CheckDeleted()
            If row(ClaimSystemDAL.COL_NAME_MAINTAIN_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimSystemDAL.COL_NAME_MAINTAIN_CLAIM_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(ClaimSystemDAL.COL_NAME_MAINTAIN_CLAIM_ID, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimSystemDAL
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

#Region "ClaimSystemDV"
    Public Class ClaimSystemDV
        Inherits DataView

#Region "Constants"
        Public Const COL_CLAIM_SYSTEM_ID As String = ClaimSystemDAL.COL_NAME_CLAIM_SYSTEM_ID
        Public Const COL_DESCRIPTION As String = ClaimSystemDAL.COL_NAME_DESCRIPTION
        Public Const COL_CODE As String = ClaimSystemDAL.COL_NAME_CODE
        Public Const COL_MAINTAIN_CLAIM As String = ClaimSystemDAL.COL_NAME_MAINTAIN_CLAIM
        Public Const COL_NEW_CLAIM As String = ClaimSystemDAL.COL_NAME_NEW_CLAIM
        Public Const COL_PAY_CLAIM As String = ClaimSystemDAL.COL_NAME_PAY_CLAIM

#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(table As DataTable)
            MyBase.New(table)
        End Sub

    End Class
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function LoadList(descriptionMask As String) As DataView
        Try
            Dim dal As New ClaimSystemDAL
            Dim ds As DataSet

            ds = dal.LoadList(descriptionMask, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Return (ds.Tables(ClaimSystemDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function

    Public Shared Function GetNewDataViewRow(dv As DataView, id As Guid, bo As ClaimSystem) As DataView

        Dim dt As DataTable
        dt = dv.Table

        If bo.IsNew Then
            Dim row As DataRow = dt.NewRow

            row(ClaimSystemDAL.COL_NAME_DESCRIPTION) = bo.Description 'String.Empty
            row(ClaimSystemDAL.COL_NAME_CODE) = bo.Code 'String.Empty
            row(ClaimSystemDAL.COL_NAME_CLAIM_SYSTEM_ID) = bo.Id.ToByteArray
            row(ClaimSystemDAL.COL_NAME_NEW_CLAIM_ID) = bo.NewClaimId.ToByteArray
            row(ClaimSystemDAL.COL_NAME_PAY_CLAIM_ID) = bo.PayClaimId.ToByteArray
            row(ClaimSystemDAL.COL_NAME_MAINTAIN_CLAIM_ID) = bo.MaintainClaimId.ToByteArray
            dt.Rows.Add(row)
        End If

        Return (dv)

    End Function

#End Region

End Class



