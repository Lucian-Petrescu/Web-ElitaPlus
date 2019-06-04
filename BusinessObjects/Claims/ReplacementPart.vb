'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/9/2013)  ********************

Public Class ReplacementPart
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
            Dim dal As New ReplacementPartDAL
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
            Dim dal As New ReplacementPartDAL
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
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ReplacementPartDAL.COL_NAME_CLAIM_ID, Value)
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
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ReplacementPartDAL.COL_NAME_SKU_NUMBER, Value)
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
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ReplacementPartDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ReplacementPartDAL
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
                If Not Me.ClaimId.Equals(Guid.Empty) Then
                    Me.Claim = ClaimFacade.Instance.GetClaim(Of ClaimBase)(Me.ClaimId, Me.Dataset, False)
                End If
            End If
            Return _claim
        End Get
        Private Set(ByVal value As ClaimBase)
            If (_claim Is Nothing OrElse value Is Nothing OrElse Not _claim.Equals(value)) Then
                _claim = value
            End If
        End Set
    End Property
#End Region

End Class

Public Class ReplacementPartList
    Inherits BusinessObjectListEnumerableBase(Of ClaimBase, ReplacementPart)

    Public Sub New(ByVal parent As ClaimBase)
        MyBase.New(LoadTable(parent), parent)
    End Sub

    Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
        Return CType(bo, ReplacementPart).ClaimId.Equals(CType(Parent, ClaimBase).Id)
    End Function

    Private Shared Function LoadTable(ByVal parent As ClaimBase) As DataTable
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

