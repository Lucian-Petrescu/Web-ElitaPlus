'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/9/2013)  ********************

Public Class ReplacementPart
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
            Dim dal As New ReplacementPartDAL
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
            Dim dal As New ReplacementPartDAL
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
            If Row(ReplacementPartDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ReplacementPartDAL.COL_NAME_REPLACEMENT_PART_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ClaimId() As Guid
        Get
            CheckDeleted()
            If Row(ReplacementPartDAL.COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ReplacementPartDAL.COL_NAME_CLAIM_ID), Byte()))
            End If
        End Get
        Set(Value As Guid)
            CheckDeleted()
            SetValue(ReplacementPartDAL.COL_NAME_CLAIM_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=20)>
    Public Property SkuNumber() As String
        Get
            CheckDeleted()
            If Row(ReplacementPartDAL.COL_NAME_SKU_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ReplacementPartDAL.COL_NAME_SKU_NUMBER), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ReplacementPartDAL.COL_NAME_SKU_NUMBER, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(ReplacementPartDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ReplacementPartDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set(Value As String)
            CheckDeleted()
            SetValue(ReplacementPartDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New ReplacementPartDAL
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

#End Region

#Region "Lazy Initialize Fields"
    Private _claim As Claim = Nothing
#End Region

#Region "Lazy Initialize Properties"
    Public Property Claim As ClaimBase
        Get
            If (_claim Is Nothing) Then
                If Not ClaimId.Equals(Guid.Empty) Then
                    Me.Claim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(ClaimId, Dataset, False)
                End If
            End If
            Return _claim
        End Get
        Private Set(value As ClaimBase)
            If (_claim Is Nothing OrElse value Is Nothing OrElse Not _claim.Equals(value)) Then
                _claim = value
            End If
        End Set
    End Property
#End Region

End Class

Public Class ReplacementPartList
    Inherits BusinessObjectListEnumerableBase(Of ClaimBase, ReplacementPart)

    Public Sub New(parent As ClaimBase)
        MyBase.New(LoadTable(parent), parent)
    End Sub

    Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
        Return CType(bo, ReplacementPart).ClaimId.Equals(CType(Parent, ClaimBase).Id)
    End Function

    Private Shared Function LoadTable(parent As ClaimBase) As DataTable
        Try
            If Not parent.IsChildrenCollectionLoaded(GetType(ReplacementPartList)) Then
                Dim dal As New ReplacementPartDAL
                dal.LoadList(parent.Dataset, parent.Id)
                parent.AddChildrenCollection(GetType(ReplacementPartList))
            End If
            Return parent.Dataset.Tables(ReplacementPartDAL.TABLE_NAME)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Function

End Class

