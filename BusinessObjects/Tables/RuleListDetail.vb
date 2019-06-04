'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/25/2012)  ********************

Public Class RuleListDetail
    Inherits BusinessObjectBase
    Implements IExpirable
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
            Dim dal As New RuleListDetailDAL
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
            Dim dal As New RuleListDetailDAL
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
    Public ReadOnly Property Id() As Guid Implements IExpirable.ID
        Get
            If Row(RuleListDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RuleListDetailDAL.COL_NAME_RULE_LIST_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property RuleListId() As Guid
        Get
            CheckDeleted()
            If Row(RuleListDetailDAL.COL_NAME_RULE_LIST_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RuleListDetailDAL.COL_NAME_RULE_LIST_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(RuleListDetailDAL.COL_NAME_RULE_LIST_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property RuleId() As Guid
        Get
            CheckDeleted()
            If row(RuleListDetailDAL.COL_NAME_RULE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RuleListDetailDAL.COL_NAME_RULE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(RuleListDetailDAL.COL_NAME_RULE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Effective() As DateTimeType Implements IExpirable.Effective
        Get
            CheckDeleted()
            If Row(RuleListDetailDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(RuleListDetailDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            Me.SetValue(RuleListDetailDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Expiration() As DateTimeType Implements IExpirable.Expiration
        Get
            CheckDeleted()
            If Row(RuleListDetailDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(RuleListDetailDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set(ByVal Value As DateTimeType)
            CheckDeleted()
            Me.SetValue(RuleListDetailDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property

    Public ReadOnly Property RuleDescription As String
        Get
            Return (New Rule(Me.RuleId)).Description
        End Get
    End Property

    Public Overrides ReadOnly Property IsNew As Boolean Implements IExpirable.IsNew
        Get
            Return MyBase.IsNew
        End Get
    End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New RuleListDetailDAL
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

    Public Sub Copy(ByVal original As RuleListDetail)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Best Replacement.")
        End If
        MyBase.CopyFrom(original)
    End Sub
#End Region

#Region "Visitor"
    Public Function Accept(ByRef visitor As IVisitor) As Boolean Implements IExpirable.Accept
        Return visitor.Visit(Me)
    End Function
#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Rule List Detail View"
    Public Class RuleListDetailView
        Inherits BusinessObjectListBase

        Public Sub New(ByVal parent As RuleList)
            MyBase.New(LoadTable(parent), GetType(RuleListDetail), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, RuleListDetail).RuleListId.Equals(CType(Parent, RuleList).Id)
        End Function

        Private Shared Function LoadTable(ByVal parent As RuleList) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(RuleListDetailView)) Then
                    Dim dal As New RuleListDetailDAL
                    dal.LoadList(parent.Dataset, parent.Id, DateTime.Now)
                    parent.AddChildrenCollection(GetType(RuleListDetailView))
                End If
                Return parent.Dataset.Tables(RuleListDetailDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class
#End Region

#Region "Dummy stuff for Iexpirable which is not used but required to declare"
    Private Property description As String
        Get
            Return String.Empty
        End Get
        Set(ByVal value As String)
            'do nothing
        End Set
    End Property

    Private Property code As String Implements IExpirable.Code
        Get
            Return String.Empty
        End Get
        Set(ByVal value As String)
            'do nothing
        End Set
    End Property

    Private Property parent_id As Guid Implements IExpirable.parent_id
        Get
            Return Guid.Empty
        End Get
        Set(ByVal value As Guid)
            'do nothing
        End Set
    End Property

#End Region

End Class


