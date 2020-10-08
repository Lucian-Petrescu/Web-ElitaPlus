'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/27/2012)  ********************

Public Class IssueQuestion
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
            Dim dal As New IssueQuestionDAL
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
            Dim dal As New IssueQuestionDAL
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
    End Sub
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id As Guid
        Get
            If row(IssueQuestionDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(IssueQuestionDAL.COL_NAME_ISSUE_QUESTION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property IssueId As Guid
        Get
            CheckDeleted()
            If Row(IssueQuestionDAL.COL_NAME_ISSUE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(IssueQuestionDAL.COL_NAME_ISSUE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(IssueQuestionDAL.COL_NAME_ISSUE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property SoftQuestionId As Guid
        Get
            CheckDeleted()
            If row(IssueQuestionDAL.COL_NAME_SOFT_QUESTION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(IssueQuestionDAL.COL_NAME_SOFT_QUESTION_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(IssueQuestionDAL.COL_NAME_SOFT_QUESTION_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DisplayOrder As LongType
        Get
            CheckDeleted()
            If Row(IssueQuestionDAL.COL_NAME_DISPLAY_ORDER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(IssueQuestionDAL.COL_NAME_DISPLAY_ORDER), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(IssueQuestionDAL.COL_NAME_DISPLAY_ORDER, Value)
        End Set
    End Property

   Public Property Effective As DateType
        Get
            CheckDeleted()
            If row(IssueQuestionDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(IssueQuestionDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(IssueQuestionDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property



    Public Property Expiration As DateType
        Get
            CheckDeleted()
            If row(IssueQuestionDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(IssueQuestionDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(IssueQuestionDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New IssueQuestionDAL
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

#Region "Public Methods"

    Public Function ExecuteQuestionsListFilter(Issue As Guid, QuestionList As String, SearchTags As String) As DataView
        Dim eqListDal As New IssueQuestionDAL
        Return eqListDal.ExecuteQuestionsFilter(Issue, QuestionList, SearchTags).Tables(0).DefaultView
    End Function

    Public Function AvailableQuestionsList(Issue As Guid, QuestionList As String, SearchTags As String, ActiveOn As String) As DataView
        Dim eqListDal As New IssueQuestionDAL
        Return eqListDal.AvailableQuestionListFilter(Issue, QuestionList, SearchTags, ActiveOn, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0).DefaultView
    End Function

    Public Function ExecuteDealerListFilter(Code As String) As DataView
        Dim eqListDal As New IssueQuestionDAL
        Return eqListDal.ExecuteDealerFilter(Code).Tables(0).DefaultView
    End Function

    Public Function GetSelectedDealerList(QuestionListCode As String) As DataView
        Dim eqListDal As New IssueQuestionListDAL
        Return eqListDal.GetSelectedDealerList(QuestionListCode).Tables(0).DefaultView
    End Function

#End Region

#Region "Public Methods"

    Public Sub Copy(original As IssueQuestion)
        If Not IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Issue Question.")
        End If
        CopyFrom(original)
    End Sub

    Public Shared Function IsChild(IssueId As Guid, SoftQuestionId As Guid) As Byte()

        Try
            Dim dal As New IssueQuestionDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Dim ds As DataSet = dal.IsChild(SoftQuestionId, IssueId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If ds IsNot Nothing Then
                If ds.Tables(IssueQuestionDAL.TABLE_NAME).Rows.Count > 0 Then
                    Return ds.Tables(IssueQuestionDAL.TABLE_NAME).Rows(0)(IssueQuestionDAL.COL_NAME_ISSUE_QUESTION_ID)
                Else
                    Return Guid.Empty.ToByteArray
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

End Class


