'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/7/2012)  ********************

Public Class RuleProcess
    Inherits BusinessObjectBase
    Implements IExpirable

#Region "Constructors"

    'Existing BO
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

    'Existing BO attaching to a BO family
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
            Dim dal As New RuleProcessDAL
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
            Dim dal As New RuleProcessDAL
            If _isDSCreator Then
                If Row IsNot Nothing Then
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
        Me.ProcessOrder = 0
        Effective = Date.Now
        Expiration = New Date(2499, 12, 31, 23, 59, 59)
    End Sub
#End Region


#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid Implements IExpirable.ID
        Get
            If Row(RuleProcessDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(RuleProcessDAL.COL_NAME_RULE_PROCESS_ID), Byte()))
            End If
        End Get
    End Property


    Public Property RuleId As Guid
        Get
            CheckDeleted()
            If row(RuleProcessDAL.COL_NAME_RULE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RuleProcessDAL.COL_NAME_RULE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleProcessDAL.COL_NAME_RULE_ID, Value)
        End Set
    End Property



    Public Property ProcessId As Guid
        Get
            CheckDeleted()
            If row(RuleProcessDAL.COL_NAME_PROCESS_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RuleProcessDAL.COL_NAME_PROCESS_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleProcessDAL.COL_NAME_PROCESS_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Effective As DateTimeType Implements IExpirable.Effective
        Get
            CheckDeleted()
            If Row(RuleProcessDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(RuleProcessDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleProcessDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Expiration As DateTimeType Implements IExpirable.Expiration
        Get
            CheckDeleted()
            If Row(RuleProcessDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateTimeType(CType(Row(RuleProcessDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(RuleProcessDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property

    Public Property ProcessOrder As LongType
        Get
            CheckDeleted()
            If Row(RuleProcessDAL.COL_NAME_PROCESS_ORDER) Is DBNull.Value Then
                Return 0
            Else                
                Return LongType.Parse(Row(RuleProcessDAL.COL_NAME_PROCESS_ORDER)) 'CType(Row(RuleProcessDAL.COL_NAME_PROCESS_ORDER), LongType)
            End If

        End Get
        Set
            CheckDeleted()
            SetValue(RuleProcessDAL.COL_NAME_PROCESS_ORDER, Value)
        End Set
    End Property

    Public Property Description As String
        Get
            CheckDeleted()
            If Row(DALBase.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DALBase.COL_NAME_DESCRIPTION), String)
            End If
        End Get
        Set
            'do nothing
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
                Dim dal As New RuleProcessDAL
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

#Region "Process Rule View"
    Public Class ProcessRuleDetailView
        Inherits BusinessObjectListBase

        Public Sub New(parent As Rule)
            MyBase.New(LoadTable(parent), GetType(RuleProcess), parent)
        End Sub

        Public Overrides Function Belong(bo As BusinessObjectBase) As Boolean
            Return CType(bo, RuleProcess).RuleId.Equals(CType(Parent, Rule).Id)
        End Function

        Private Shared Function LoadTable(parent As Rule) As DataTable
            Try
                If Not parent.IsChildrenCollectionLoaded(GetType(ProcessRuleDetailView)) Then
                    Dim dal As New RuleProcessDAL
                    'Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
                    dal.LoadList(parent.Dataset, parent.Id, DateTime.Now)
                    parent.AddChildrenCollection(GetType(ProcessRuleDetailView))
                End If
                Return parent.Dataset.Tables(RuleProcessDAL.TABLE_NAME)
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Function

    End Class
#End Region


#Region "Public Methods"

    Public Sub Copy(original As RuleProcess)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Rule Process.")
        End If
        CopyFrom(original)
    End Sub

    Public Shared Function IsChild(ProcessId As Guid, RuleProcessId As Guid) As Byte()

        Try
            Dim dal As New RuleProcessDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Dim ds As DataSet = dal.IsChild(RuleProcessId, ProcessId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If ds IsNot Nothing Then
                If ds.Tables(RuleProcessDAL.TABLE_NAME).Rows.Count > 0 Then
                    Return ds.Tables(RuleProcessDAL.TABLE_NAME).Rows(0)(RuleProcessDAL.COL_NAME_RULE_PROCESS_ID)
                Else
                    Return Guid.Empty.ToByteArray
                End If
            End If
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


