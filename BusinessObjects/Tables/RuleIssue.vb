'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/30/2012)  ********************

Public Class RuleIssue
    Inherits BusinessObjectBase
    Implements IExpirable


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
            Dim dal As New RuleIssueDAL
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
            Dim dal As New RuleIssueDAL
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
    Public ReadOnly Property Id As Guid Implements IExpirable.ID
        Get
            If Row(RuleIssueDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RuleIssueDAL.COL_NAME_RULE_ISSUE_ID), Byte()))
            End If
        End Get
    End Property


    Public Property IssueId As Guid
        Get
            CheckDeleted()
            If Row(RuleIssueDAL.COL_NAME_ISSUE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RuleIssueDAL.COL_NAME_ISSUE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleIssueDAL.COL_NAME_ISSUE_ID, Value)
        End Set
    End Property

    Public Property RuleId As Guid
        Get
            CheckDeleted()
            If row(RuleIssueDAL.COL_NAME_RULE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RuleIssueDAL.COL_NAME_RULE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleIssueDAL.COL_NAME_RULE_ID, Value)
        End Set
    End Property


    Public Property Description As String
        Get
            CheckDeleted()
            If Row(RuleIssueDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RuleIssueDAL.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            'do nothing
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Effective As DateTimeType Implements IExpirable.Effective
        Get
            CheckDeleted()
            If Row(RuleIssueDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(RuleIssueDAL.COL_NAME_EFFECTIVE), DateTime))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleIssueDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Expiration As DateTimeType Implements IExpirable.Expiration
        Get
            CheckDeleted()
            If Row(RuleIssueDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(RuleIssueDAL.COL_NAME_EXPIRATION), DateTime))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleIssueDAL.COL_NAME_EXPIRATION, Value)
        End Set
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
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New RuleIssueDAL
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

#Region "Visitor"
    Public Function Accept(ByRef visitor As IVisitor) As Boolean Implements IExpirable.Accept
        Return visitor.Visit(Me)
    End Function
#End Region

#Region "Issue Rule View"
    Public Class IssueRuleDetailView
        Inherits BusinessObjectListBase

        Public Sub New(parent As Rule)
            MyBase.New(LoadTable(parent), GetType(RuleIssue), parent)
        End Sub

        Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
            Return CType(bo, RuleIssue).RuleId.Equals(CType(Parent, Rule).Id)
        End Function

        Private Shared Function LoadTable(parent As Rule) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(IssueRuleDetailView)) Then
                    Dim dal As New RuleIssueDAL
                    dal.LoadList(parent.Dataset, parent.Id, DateTime.Now)
                    parent.AddChildrenCollection(GetType(IssueRuleDetailView))
                End If
                Return parent.Dataset.Tables(RuleIssueDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class
#End Region

#Region "Public Methods"

    Public Sub Copy(original As RuleIssue)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Rule Issue.")
        End If
        MyBase.CopyFrom(original)
    End Sub

    Public Shared Function IsChild(IssueId As Guid, RuleIssueId As Guid) As Byte()

        Try
            Dim dal As New RuleIssueDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Dim ds As DataSet = dal.IsChild(RuleIssueId, IssueId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If Not ds Is Nothing Then
                If ds.Tables(RuleIssueDAL.TABLE_NAME).Rows.Count > 0 Then
                    Return ds.Tables(RuleIssueDAL.TABLE_NAME).Rows(0)(RuleIssueDAL.COL_NAME_RULE_ISSUE_ID)
                Else
                    Return Guid.Empty.ToByteArray
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetRulesInList(IssueId As Guid) As ArrayList

        Try
            Dim dal As New RuleIssueDAL
            Dim oCompanyGroupIds As ArrayList
            Dim oRuleList As ArrayList
            Dim oDataTable As DataTable

            oCompanyGroupIds = New ArrayList
            oRuleList = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            oDataTable = dal.GetRulesInList(IssueId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0)
            For Each row As DataRow In oDataTable.Rows
                oRuleList.Add(row(0))
            Next
            Return oRuleList
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "Dummy stuff for Iexpirable which is not used but required to declare"
    Private Property code As String Implements IExpirable.Code
        Get
            Return String.Empty
        End Get
        Set
            'do nothing
        End Set
    End Property

    Private Property parent_id As Guid Implements IExpirable.parent_id
        Get
            Return Guid.Empty
        End Get
        Set
            'do nothing
        End Set
    End Property

#End Region
End Class


