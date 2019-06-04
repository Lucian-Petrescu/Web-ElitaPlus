'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (9/25/2012)  ********************

Public Class CompanyWorkQueueIssue
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
            Dim dal As New CompanyWorkQueueIssueDAL
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
            Dim dal As New companyworkqueueissuedAL
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
            If Row(CompanyWorkQueueIssueDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyWorkQueueIssueDAL.COL_NAME_COMPANY_WRKQUE_ISSUE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property WorkqueueId() As Guid
        Get
            CheckDeleted()
            If Row(CompanyWorkQueueIssueDAL.COL_NAME_WORKQUEUE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyWorkQueueIssueDAL.COL_NAME_WORKQUEUE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CompanyWorkQueueIssueDAL.COL_NAME_WORKQUEUE_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidateUniqueCompanyIssue("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(CompanyWorkQueueIssueDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyWorkQueueIssueDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CompanyWorkQueueIssueDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property IssueId() As Guid
        Get
            CheckDeleted()
            If Row(CompanyWorkQueueIssueDAL.COL_NAME_ISSUE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(CompanyWorkQueueIssueDAL.COL_NAME_ISSUE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(CompanyWorkQueueIssueDAL.COL_NAME_ISSUE_ID, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New CompanyWorkQueueIssueDAL
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

    Public Sub Copy(ByVal original As RuleIssue)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Rule Issue.")
        End If
        MyBase.CopyFrom(original)
    End Sub
#End Region

#Region "DataView Retrieveing Methods"
    Public Class CompanyWorkqueueIssueList
        Inherits BusinessObjectListBase

        Public Sub New(ByVal parent As Issue)
            MyBase.New(LoadTable(parent), GetType(CompanyWorkQueueIssue), parent)
        End Sub

        Public Overrides Function Belong(ByVal bo As BusinessObjectBase) As Boolean
            Return CType(bo, CompanyWorkQueueIssue).IssueId.Equals(CType(Parent, Issue).Id)
        End Function

        Private Shared Function LoadTable(ByVal parent As Issue) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(CompanyWorkqueueIssueList)) Then
                    Dim dal As New CompanyWorkQueueIssueDAL
                    dal.LoadlistByIssue(parent.Dataset, parent.Id)
                    parent.AddChildrenCollection(GetType(CompanyWorkqueueIssueList))
                End If
                Return parent.Dataset.Tables(CompanyWorkQueueIssueDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function
    End Class
#End Region

#Region "Custom Validator"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class ValidateUniqueCompanyIssue
        Inherits ValidBaseAttribute
        Implements IValidatorAttribute
        Private _fieldDisplayName As String
        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, Messages.INVALID_COMPANY_WORKQUEUE_ISSUE)
            _fieldDisplayName = fieldDisplayName
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal context As Object) As Boolean
            Dim obj As CompanyWorkQueueIssue = CType(context, CompanyWorkQueueIssue)
            For Each dtrow As DataRow In obj.Dataset.Tables(CompanyWorkQueueIssueDAL.TABLE_NAME).Rows
                If Not dtrow.RowState = DataRowState.Deleted AndAlso Not dtrow.RowState = DataRowState.Detached Then
                    Dim CQI_id As Guid = New Guid(CType(dtrow(CompanyWorkQueueIssueDAL.COL_NAME_COMPANY_WRKQUE_ISSUE_ID), Byte()))
                    If Not obj.Id = CQI_id Then
                        Dim comp_id As Guid = New Guid(CType(dtrow(CompanyWorkQueueIssueDAL.COL_NAME_COMPANY_ID), Byte()))
                        If comp_id = obj.CompanyId Then
                            Return False
                        End If
                    End If
                End If
            Next
            Return True
        End Function
    End Class
#End Region
End Class


