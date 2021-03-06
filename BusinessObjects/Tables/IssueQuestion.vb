﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/27/2012)  ********************

Public Class IssueQuestion
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
            Dim dal As New IssueQuestionDAL
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
            Dim dal As New IssueQuestionDAL
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
            If row(IssueQuestionDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(IssueQuestionDAL.COL_NAME_ISSUE_QUESTION_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property IssueId() As Guid
        Get
            CheckDeleted()
            If Row(IssueQuestionDAL.COL_NAME_ISSUE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(IssueQuestionDAL.COL_NAME_ISSUE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(IssueQuestionDAL.COL_NAME_ISSUE_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property SoftQuestionId() As Guid
        Get
            CheckDeleted()
            If row(IssueQuestionDAL.COL_NAME_SOFT_QUESTION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(IssueQuestionDAL.COL_NAME_SOFT_QUESTION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(IssueQuestionDAL.COL_NAME_SOFT_QUESTION_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DisplayOrder() As LongType
        Get
            CheckDeleted()
            If Row(IssueQuestionDAL.COL_NAME_DISPLAY_ORDER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(IssueQuestionDAL.COL_NAME_DISPLAY_ORDER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(IssueQuestionDAL.COL_NAME_DISPLAY_ORDER, Value)
        End Set
    End Property

   Public Property Effective() As DateType
        Get
            CheckDeleted()
            If row(IssueQuestionDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(IssueQuestionDAL.COL_NAME_EFFECTIVE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(IssueQuestionDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property



    Public Property Expiration() As DateType
        Get
            CheckDeleted()
            If row(IssueQuestionDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(row(IssueQuestionDAL.COL_NAME_EXPIRATION), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(IssueQuestionDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property


#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New IssueQuestionDAL
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

#Region "Public Methods"

    Public Function ExecuteQuestionsListFilter(ByVal Issue As Guid, ByVal QuestionList As String, ByVal SearchTags As String) As DataView
        Dim eqListDal As New IssueQuestionDAL
        Return eqListDal.ExecuteQuestionsFilter(Issue, QuestionList, SearchTags).Tables(0).DefaultView
    End Function

    Public Function AvailableQuestionsList(ByVal Issue As Guid, ByVal QuestionList As String, ByVal SearchTags As String, ByVal ActiveOn As String) As DataView
        Dim eqListDal As New IssueQuestionDAL
        Return eqListDal.AvailableQuestionListFilter(Issue, QuestionList, SearchTags, ActiveOn, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0).DefaultView
    End Function

    Public Function ExecuteDealerListFilter(ByVal Code As String) As DataView
        Dim eqListDal As New IssueQuestionDAL
        Return eqListDal.ExecuteDealerFilter(Code).Tables(0).DefaultView
    End Function

    Public Function GetSelectedDealerList(ByVal QuestionListCode As String) As DataView
        Dim eqListDal As New IssueQuestionListDAL
        Return eqListDal.GetSelectedDealerList(QuestionListCode).Tables(0).DefaultView
    End Function

#End Region

#Region "Public Methods"

    Public Sub Copy(ByVal original As IssueQuestion)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Issue Question.")
        End If
        MyBase.CopyFrom(original)
    End Sub

    Public Shared Function IsChild(ByVal IssueId As Guid, ByVal SoftQuestionId As Guid) As Byte()

        Try
            Dim dal As New IssueQuestionDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Dim ds As DataSet = dal.IsChild(SoftQuestionId, IssueId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If Not ds Is Nothing Then
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


