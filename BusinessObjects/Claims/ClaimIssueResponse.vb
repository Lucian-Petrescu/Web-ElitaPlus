'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (7/26/2012)  ********************

Public Class ClaimIssueResponse
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
            Dim dal As New ClaimIssueResponseDAL
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
            Dim dal As New ClaimIssueResponseDAL
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
            If row(ClaimIssueResponseDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimIssueResponseDAL.COL_NAME_CLAIM_ISSUE_RESPONSE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property ClaimIssueId() As Guid
        Get
            CheckDeleted()
            If row(ClaimIssueResponseDAL.COL_NAME_CLAIM_ISSUE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimIssueResponseDAL.COL_NAME_CLAIM_ISSUE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimIssueResponseDAL.COL_NAME_CLAIM_ISSUE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property AnswerId() As Guid
        Get
            CheckDeleted()
            If row(ClaimIssueResponseDAL.COL_NAME_ANSWER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimIssueResponseDAL.COL_NAME_ANSWER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimIssueResponseDAL.COL_NAME_ANSWER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property SupportsClaimId() As Guid
        Get
            CheckDeleted()
            If row(ClaimIssueResponseDAL.COL_NAME_SUPPORTS_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(ClaimIssueResponseDAL.COL_NAME_SUPPORTS_CLAIM_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimIssueResponseDAL.COL_NAME_SUPPORTS_CLAIM_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=1020)> _
    Public Property AnswerDescription() As String
        Get
            CheckDeleted()
            If row(ClaimIssueResponseDAL.COL_NAME_ANSWER_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimIssueResponseDAL.COL_NAME_ANSWER_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimIssueResponseDAL.COL_NAME_ANSWER_DESCRIPTION, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=1020)> _
    Public Property AnswerValue() As String
        Get
            CheckDeleted()
            If row(ClaimIssueResponseDAL.COL_NAME_ANSWER_VALUE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(ClaimIssueResponseDAL.COL_NAME_ANSWER_VALUE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimIssueResponseDAL.COL_NAME_ANSWER_VALUE, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimIssueResponseDAL
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

#Region "Claim Issue Response List Selection View"
    Public Class ClaimIssueResponseList
        Inherits BusinessObjectListBase

        Public Sub New(ByVal parent As ClaimIssue)
            MyBase.New(LoadTable(parent), GetType(ClaimIssueResponse), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, ClaimIssueResponse).ClaimIssueId.Equals(CType(Parent, ClaimIssue).ClaimIssueId)
        End Function

        Private Shared Function LoadTable(ByVal parent As ClaimIssue) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(ClaimIssueResponseList)) Then
                    Dim dal As New ClaimIssueResponseDAL
                    dal.LoadList(parent.Dataset, parent.ClaimIssueId)
                    parent.AddChildrenCollection(GetType(ClaimIssueResponseList))
                End If
                Return parent.Dataset.Tables(ClaimIssueResponseDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class
#End Region

End Class


