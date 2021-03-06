﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/27/2012)  ********************

Imports System.Collections.Generic

Public Class Issue
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
            Dim dal As New IssueDAL
            If Me.Dataset.Tables.IndexOf(dal.TABLE_NAME) < 0 Then
                dal.LoadSchema(Me.Dataset)
            End If
            Dim newRow As DataRow = Me.Dataset.Tables(dal.TABLE_NAME).NewRow
            Me.Dataset.Tables(dal.TABLE_NAME).Rows.Add(newRow)
            Me.Row = newRow
            SetValue(dal.TABLE_KEY_NAME, Guid.NewGuid)
            SetValue(dal.COL_NAME_EFFECTIVE, EquipmentListDetail.GetCurrentDateTime())
            SetValue(dal.COL_NAME_EXPIRATION, ISSUE_EXPIRATION_DEFAULT)
            Initialize()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try
    End Sub

    Protected Sub Load(ByVal id As Guid)
        Try
            Dim dal As New IssueDAL
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

#Region "Variables"

    Dim _ActiveOn As DateType
    Private Const TABLE_ISSUE_COMMENT As String = "elp_issue_comment"
    Private Const ICTYP As String = "ICTYP"
    Private Const _CODE As String = "CODE"
    Private Const _TEXT As String = "TEXT"
    Private Const ISSUE_COMMENT_ID As String = "ISSUE_COMMENT_ID"
    Private Const ISSUE_COMMENT_TYPE_ID As String = "ISSUE_COMMENT_TYPE_ID"

    'DEF-2855
    Public Const ISSUE_DESCRIPTION As String = "ISSUE_DESCRIPTION"
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
            If Row(IssueDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(IssueDAL.COL_NAME_ISSUE_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1020)>
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(IssueDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(IssueDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(IssueDAL.COL_NAME_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=2000)>
    Public Property Description() As String
        Get
            CheckDeleted()
            If Row(IssueDAL.COL_NAME_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Dim desc As String = LookupListNew.GetDescriptionFromId(ISSUE_DESCRIPTION, LookupListNew.GetIdFromCode(ISSUE_DESCRIPTION, Code), ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                If String.IsNullOrEmpty(desc) Then desc = CType(Row(IssueDAL.COL_NAME_DESCRIPTION), String)
                Return desc
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(IssueDAL.COL_NAME_DESCRIPTION, Value)
        End Set
    End Property

    <ValueMandatory("")>
    Public Property IssueTypeId() As Guid
        Get
            CheckDeleted()
            If Row(IssueDAL.COL_NAME_ISSUE_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(IssueDAL.COL_NAME_ISSUE_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(IssueDAL.COL_NAME_ISSUE_TYPE_ID, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property Effective() As DateType
        Get
            CheckDeleted()
            If Row(IssueDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(IssueDAL.COL_NAME_EFFECTIVE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(IssueDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property


    <ValueMandatory("")>
    Public Property Expiration() As DateType
        Get
            CheckDeleted()
            If Row(IssueDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(IssueDAL.COL_NAME_EXPIRATION).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(IssueDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property
    Public Property IssueProcessor() As String
        Get
            CheckDeleted()
            If Row(IssueDAL.COL_NAME_ISSUE_PROCESSOR) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(IssueDAL.COL_NAME_ISSUE_PROCESSOR), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(IssueDAL.COL_NAME_ISSUE_PROCESSOR, Value)
        End Set
    End Property

    Public Property DeniedReason() As String
        Get
            CheckDeleted()
            If Row(IssueDAL.COL_NAME_DENIED_REASON) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(IssueDAL.COL_NAME_DENIED_REASON), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(IssueDAL.COL_NAME_DENIED_REASON, Value)
        End Set
    End Property
    Public Property SPClaimType() As String
        Get
            CheckDeleted()
            If Row(IssueDAL.COL_NAME_SP_CLAIM_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(IssueDAL.COL_NAME_SP_CLAIM_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(IssueDAL.COL_NAME_SP_CLAIM_TYPE, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=500)>
    Public Property SPClaimValue() As String
        Get
            CheckDeleted()
            If Row(IssueDAL.COL_NAME_SP_CLAIM_VALUE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(IssueDAL.COL_NAME_SP_CLAIM_VALUE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(IssueDAL.COL_NAME_SP_CLAIM_VALUE, Value)
        End Set
    End Property
    '''TODO: Add RegEx Validations
    <ValidStringLength("", Max:=2000)>
    Public Property PreConditions() As String
        Get
            CheckDeleted()
            If Row(IssueDAL.COL_NAME_PRE_CONDITIONS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(IssueDAL.COL_NAME_PRE_CONDITIONS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(IssueDAL.COL_NAME_PRE_CONDITIONS, Value)
        End Set
    End Property

    Private m_preConditionsList As List(Of String)
    Public ReadOnly Property PreConditionList As List(Of String)
        Get
            If (m_preConditionsList Is Nothing) Then
                m_preConditionsList = New List(Of String)()

                If (Not String.IsNullOrWhiteSpace(Me.PreConditions)) Then
                    m_preConditionsList.AddRange(Me.PreConditions.Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries))
                End If
            End If

            Return m_preConditionsList
        End Get
    End Property


    Public Property ActiveOn() As DateType
        Get
            Return _ActiveOn
        End Get
        Set(ByVal Value As DateType)
            _ActiveOn = Value
        End Set
    End Property

    Public ReadOnly Property MyDataset() As DataSet
        Get
            Return Me.Dataset
        End Get
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New IssueDAL
                Me.UpdateTranslation()
                dal.UpdateFamily(Me.Dataset)
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

#Region "Translation"

    Sub UpdateTranslation()
        Dim dropdownBO As New DropdownItem
        Dim retVal As Integer

        'DEF-2855
        Dim DropdownId As Guid, listItemId As Guid

        DropdownId = QuestionList.GetDropdownId(ISSUE_DESCRIPTION)
        If Not DropdownId = Guid.Empty Then
            listItemId = (New IssueDAL).GetListItembyCode(Me.Code.ToUpper, DropdownId)
            If listItemId = Guid.Empty Then
                retVal = dropdownBO.AddDropdownItem(Me.Code.ToUpper, Codes.YESNO_Y, Codes.YESNO_Y, DropdownId, Me.Description, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
            Else
                retVal = dropdownBO.UpdateDropdownItem(listItemId, Me.Code.ToUpper,
                         Codes.YESNO_Y, Codes.YESNO_Y, Me.Description, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
            End If
        End If
        'DEF-2855

        DropdownId = QuestionList.GetDropdownId(ICTYP)
        If Not DropdownId = Guid.Empty Then
            If Me.MyDataset.Tables(TABLE_ISSUE_COMMENT).Rows.Count > 0 Then
                If Not Me.MyDataset.Tables(TABLE_ISSUE_COMMENT).GetChanges(DataRowState.Added) Is Nothing Then
                    For Each TECrow As DataRow In Me.MyDataset.Tables(TABLE_ISSUE_COMMENT).GetChanges(DataRowState.Added).Rows
                        retVal = dropdownBO.AddDropdownItem(TECrow(_CODE).ToString, Codes.YESNO_Y, Codes.YESNO_Y, DropdownId, TECrow(_TEXT).ToString, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                    Next
                End If
                If Not Me.MyDataset.Tables(TABLE_ISSUE_COMMENT).GetChanges(DataRowState.Modified) Is Nothing Then
                    For Each TECrow As DataRow In Me.MyDataset.Tables(TABLE_ISSUE_COMMENT).GetChanges(DataRowState.Modified).Rows
                        If Not GetDropdownCodeToUpdate(TECrow(ISSUE_COMMENT_ID)) = String.Empty Then
                            retVal = dropdownBO.UpdateDropdownItem(QuestionList.GetDropdownItemId(DropdownId,
                                     GetDropdownCodeToUpdate(TECrow(ISSUE_COMMENT_ID))), TECrow(_CODE).ToString,
                                     Codes.YESNO_Y, Codes.YESNO_Y, TECrow(_TEXT).ToString, ElitaPlusIdentity.Current.ActiveUser.NetworkId)
                        End If
                    Next
                End If
                If Not Me.MyDataset.Tables(TABLE_ISSUE_COMMENT).GetChanges(DataRowState.Deleted) Is Nothing Then
                    For Each TECrow As DataRow In Me.MyDataset.Tables(TABLE_ISSUE_COMMENT).GetChanges(DataRowState.Deleted).Rows
                        'If Not QuestionList.GetDropdownItemId(DropdownId, TECrow(_CODE).ToString) = Guid.Empty Then
                        '    retVal = dropdownBO.DeleteDropdownItem(QuestionList.GetDropdownItemId(DropdownId, TECrow(_CODE).ToString))
                        'End If
                    Next
                End If
            End If
        End If
    End Sub

#End Region


#Region "DataView Retrieveing Methods"

    Public Shared Function GetList(ByVal code As String,
                                    ByVal description As String, ByVal issueType As Guid, ByVal activeOn As String) As Issue.IssueSearchDV
        Try
            Dim dal As New IssueDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            If (description.Contains(DALBase.WILDCARD_CHAR) Or description.Contains(DALBase.ASTERISK)) Then
                description = description & DALBase.ASTERISK
            End If
            If (code.Contains(DALBase.WILDCARD_CHAR) Or code.Contains(DALBase.ASTERISK)) Then
                code = code & DALBase.ASTERISK
            End If

            Return New IssueSearchDV(dal.LoadSearchList(code, description, issueType, activeOn,
                oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetList(ByVal code As String,
                                    ByVal description As String, ByVal activeOn As String) As Issue.IssueSearchDV
        Try
            Dim dal As New IssueDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            If (description.Contains(DALBase.WILDCARD_CHAR) Or description.Contains(DALBase.ASTERISK)) Then
                description = description & DALBase.ASTERISK
            End If
            If (code.Contains(DALBase.WILDCARD_CHAR) Or code.Contains(DALBase.ASTERISK)) Then
                code = code & DALBase.ASTERISK
            End If

            Return New IssueSearchDV(dal.LoadList(code, description, activeOn,
                oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetQuestionExpiration(ByVal IssueId As Guid, ByVal IssueQuestionId As Guid) As DateTime

        Try
            Dim dal As New IssueDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Dim ds As DataSet = dal.GetQuestionExpiration(IssueId, IssueQuestionId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds.Tables(0).Rows(0)("EXPIRATION")
                Else
                    Return Nothing
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetRuleExpiration(ByVal IssueId As Guid, ByVal RuleId As Guid) As DateTime

        Try
            Dim dal As New IssueDAL
            Dim oCompanyGroupIds As ArrayList
            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Dim ds As DataSet = dal.GetRuleExpiration(IssueId, RuleId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds.Tables(0).Rows(0)("EXPIRATION")
                Else
                    Return Nothing
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetSoftQuestionID(ByVal IssueId As Guid, ByVal IssueQuestionId As Guid) As Byte()

        Try
            Dim dal As New IssueDAL
            Dim tempByte As Byte()
            Dim oCompanyGroupIds As ArrayList
            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Dim ds As DataSet = dal.GetSoftQuestionID(IssueId, IssueQuestionId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return ds.Tables(0).Rows(0)(0)
                Else
                    Return tempByte
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetRuleID(ByVal IssueId As Guid, ByVal RuleIssueId As Guid) As Guid

        Try
            Dim dal As New IssueDAL
            Dim tempByte As Byte()
            Dim oCompanyGroupIds As ArrayList
            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Dim ds As DataSet = dal.GetRuleID(IssueId, RuleIssueId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If Not ds Is Nothing Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Return New Guid(CType(ds.Tables(0).Rows(0)(0), Byte()))
                Else
                    Return Guid.Empty
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class IssueSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_ISSUE_ID As String = IssueDAL.COL_NAME_ISSUE_ID
        Public Const COL_NAME_DESCRIPTION As String = IssueDAL.COL_NAME_DESCRIPTION
        Public Const COL_NAME_ISSUE_TYPE As String = IssueDAL.COL_NAME_ISSUE_TYPE
        Public Const COL_NAME_CODE As String = IssueDAL.COL_NAME_CODE
        Public Const COL_NAME_EFFECTIVE As String = IssueDAL.COL_NAME_EFFECTIVE
        Public Const COL_NAME_EXPIRATION As String = IssueDAL.COL_NAME_EXPIRATION
#End Region

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property IssueId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_ISSUE_ID), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Code(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_CODE).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Description(ByVal row As DataRow) As String
            Get
                Return row(COL_NAME_DESCRIPTION).ToString
            End Get
        End Property

        Public Shared ReadOnly Property Effective(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_EFFECTIVE), Byte()))
            End Get
        End Property

        Public Shared ReadOnly Property Expiration(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_NAME_EXPIRATION), Byte()))
            End Get
        End Property

    End Class

    Public Shared Function GetIssuesListByDealer(ByVal dealerId As Guid) As DataView
        Try
            Dim dal As New IssueDAL
            Return dal.LoadIssuesByDealer(dealerId)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "Constants"
    Private ReadOnly ISSUE_EXPIRATION_DEFAULT As New DateTime(2499, 12, 31, 23, 59, 59)
    Friend Const EQUIPMENT_FORM004 As String = "EQUIPMENT_FORM004" ' Invalid List code since same effective
    Friend Const EQUIPMENT_FORM005 As String = "EQUIPMENT_FORM005" ' Equipment List Assigned To Dealer Cannt Be Deleted.
#End Region

#Region "Children Related"

    '' ---
    Public ReadOnly Property IssueNotesChildren() As IssueNotesChildrenList
        Get
            Return New IssueNotesChildrenList(Me)
        End Get
    End Property

    Public ReadOnly Property IssueQuestionsChildren() As IssueQuestionsChildrenList
        Get
            Return New IssueQuestionsChildrenList(Me)
        End Get
    End Property
    Public ReadOnly Property IssueQuestionsChildrenByIssueDealer(ByVal issueId, ByVal dealerId) As IssueQuestionsChildrenList
        Get
            Return New IssueQuestionsChildrenList(Me, issueId, dealerId)
        End Get
    End Property

    Public ReadOnly Property IssueRulesChildren() As IssueRulesChildrenList
        Get
            Return New IssueRulesChildrenList(Me)
        End Get
    End Property

    '' ---
    Public Function GetNotesChild(ByVal childId As Guid) As IssueComment
        Return CType(Me.IssueNotesChildren.GetChild(childId), IssueComment)
    End Function


    Public Function GetNotesSelectionView() As NotesSelectionView
        Dim t As DataTable = NotesSelectionView.CreateTable
        Dim detail As IssueComment
        For Each detail In Me.IssueNotesChildren
            Dim row As DataRow = t.NewRow
            row(NotesSelectionView.COL_NAME_ISSUE_COMMENT_ID) = detail.Id.ToByteArray
            row(NotesSelectionView.COL_NAME_NAME_ISSUE_COMMENT_TYPE_ID) = detail.IssueCommentTypeId.ToByteArray
            row(NotesSelectionView.COL_NAME_CODE) = detail.Code.ToString
            row(NotesSelectionView.COL_NAME_TEXT) = detail.Text.ToString
            t.Rows.Add(row)
        Next
        Return New NotesSelectionView(t)
    End Function

    Public Class NotesSelectionView
        Inherits DataView
        Public Const COL_NAME_ISSUE_COMMENT_ID As String = IssueCommentDAL.COL_NAME_ISSUE_COMMENT_ID
        Public Const COL_NAME_NAME_ISSUE_COMMENT_TYPE_ID As String = IssueCommentDAL.COL_NAME_ISSUE_COMMENT_TYPE_ID
        Public Const COL_NAME_CODE As String = IssueCommentDAL.COL_NAME_CODE
        Public Const COL_NAME_TEXT As String = IssueCommentDAL.COL_NAME_TEXT

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_ISSUE_COMMENT_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_NAME_ISSUE_COMMENT_TYPE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_CODE, GetType(String))
            t.Columns.Add(COL_NAME_TEXT, GetType(String))
            Return t
        End Function
    End Class

    Public Function GetQuestionsChild(ByVal childId As Guid) As IssueQuestion
        Return CType(Me.IssueQuestionsChildren.GetChild(childId), IssueQuestion)
    End Function

    Public Function GetRulesChild(ByVal childId As Guid) As RuleIssue
        Return CType(Me.IssueRulesChildren.GetChild(childId), RuleIssue)
    End Function

    '' ---
    Public Function GetNewNotesChild() As IssueComment
        Dim NewNotesList As IssueComment = CType(Me.IssueNotesChildren.GetNewChild, IssueComment)
        NewNotesList.IssueId = Me.Id
        Return NewNotesList
    End Function

    Public Function GetNewQuestionsChild() As IssueQuestion
        Dim NewQuestionsList As IssueQuestion = CType(Me.IssueQuestionsChildren.GetNewChild, IssueQuestion)
        NewQuestionsList.IssueId = Me.Id
        NewQuestionsList.Effective = Me.Effective
        NewQuestionsList.Expiration = Me.Expiration
        Return NewQuestionsList
    End Function

    Public Function GetNewRulesChild() As RuleIssue
        Dim NewRulesList As RuleIssue = CType(Me.IssueRulesChildren.GetNewChild, RuleIssue)
        NewRulesList.IssueId = Me.Id
        NewRulesList.Effective = Me.Effective.Value
        NewRulesList.Expiration = Me.Expiration.Value
        Return NewRulesList
    End Function

    '' -----------------

    Public ReadOnly Property CompanyWorkQueueIssueChildren() As CompanyWorkQueueIssue.CompanyWorkqueueIssueList
        Get
            Return New CompanyWorkQueueIssue.CompanyWorkqueueIssueList(Me)
        End Get
    End Property

    Public Function GetNewCompanyWorkQueueIssueChild() As CompanyWorkQueueIssue
        Dim NewNotesList As CompanyWorkQueueIssue = CType(Me.CompanyWorkQueueIssueChildren.GetNewChild, CompanyWorkQueueIssue)
        NewNotesList.IssueId = Me.Id
        Return NewNotesList
    End Function

    Public Function GetWorkQyueueSelectionView() As WorkQyueueSelectionView
        Dim t As DataTable = WorkQyueueSelectionView.CreateTable
        Dim detail As CompanyWorkQueueIssue

        Dim WQ_List As WrkQueue.WorkQueue() = WorkQueue.GetList("*", "", "CLM", Nothing, False)

        For Each detail In Me.CompanyWorkQueueIssueChildren
            Dim row As DataRow = t.NewRow
            row(WorkQyueueSelectionView.COL_NAME_COMPANY_WRKQUEUE_ID) = detail.Id.ToByteArray
            row(WorkQyueueSelectionView.COL_NAME_NAME_WORKQUEUE_ID) = detail.WorkqueueId.ToByteArray

            row(WorkQyueueSelectionView.COL_NAME_NAME_WORKQUEUE_DESC) = (From wq In WQ_List
                                                                         Where wq.Id = detail.WorkqueueId
                                                                         Select wq.Name).FirstOrDefault
            row(WorkQyueueSelectionView.COL_NAME_COMPANY_ID) = detail.CompanyId.ToByteArray
            row(WorkQyueueSelectionView.COL_NAME_ISSUE_ID) = detail.IssueId.ToByteArray
            t.Rows.Add(row)
        Next
        Return New WorkQyueueSelectionView(t)
    End Function

    Public Class WorkQyueueSelectionView
        Inherits DataView
        Public Const COL_NAME_COMPANY_WRKQUEUE_ID As String = CompanyWorkQueueIssueDAL.COL_NAME_COMPANY_WRKQUE_ISSUE_ID
        Public Const COL_NAME_NAME_WORKQUEUE_ID As String = CompanyWorkQueueIssueDAL.COL_NAME_WORKQUEUE_ID
        Public Const COL_NAME_NAME_WORKQUEUE_DESC As String = "DESCRIPTION"
        Public Const COL_NAME_COMPANY_ID As String = CompanyWorkQueueIssueDAL.COL_NAME_COMPANY_ID
        Public Const COL_NAME_ISSUE_ID As String = CompanyWorkQueueIssueDAL.COL_NAME_ISSUE_ID

        Public Sub New(ByVal Table As DataTable)
            MyBase.New(Table)
        End Sub

        Public Shared Function CreateTable() As DataTable
            Dim t As New DataTable
            t.Columns.Add(COL_NAME_COMPANY_WRKQUEUE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_NAME_WORKQUEUE_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_NAME_WORKQUEUE_DESC, GetType(String))
            t.Columns.Add(COL_NAME_COMPANY_ID, GetType(Byte()))
            t.Columns.Add(COL_NAME_ISSUE_ID, GetType(Byte()))
            Return t
        End Function
    End Class

    Public Shared Function GetAvailableWorkQueue() As WorkQyueueSelectionView

        Dim t As DataTable = WorkQyueueSelectionView.CreateTable
        Dim WQ_List As WrkQueue.WorkQueue()
        WQ_List = WorkQueue.GetList("*", "", "CLM", Nothing, False)
        For Each WQ As WrkQueue.WorkQueue In WQ_List
            Dim row As DataRow = t.NewRow
            'row(WorkQyueueSelectionView.COL_NAME_COMPANY_WRKQUEUE_ID) = 
            row(WorkQyueueSelectionView.COL_NAME_NAME_WORKQUEUE_ID) = WQ.Id.ToByteArray
            row(WorkQyueueSelectionView.COL_NAME_NAME_WORKQUEUE_DESC) = WQ.Name
            'row(WorkQyueueSelectionView.COL_NAME_COMPANY_ID) =
            'row(WorkQyueueSelectionView.COL_NAME_ISSUE_ID) = 
            t.Rows.Add(row)
        Next
        Return New WorkQyueueSelectionView(t)
    End Function

    Public Function SaveWorkQueueIssue(SelectedQueue As ArrayList) As Boolean
        Try
            'compare with what we have and what is there in the user control
            'user control will always have the final selection so remove from our list what we don't find
            For Each WQ_Issue As CompanyWorkQueueIssue In Me.CompanyWorkQueueIssueChildren
                Dim dFound As Boolean = False
                For Each Str As String In SelectedQueue
                    Dim WorkQueue_id As Guid = New Guid(Str)
                    If WQ_Issue.WorkqueueId = WorkQueue_id Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    WQ_Issue.BeginEdit()
                    WQ_Issue.Delete()
                    WQ_Issue.EndEdit()
                    WQ_Issue.Save()
                End If
            Next
            'next now add those items which are there in user control but we don't have it
            For Each Str As String In SelectedQueue
                Dim dFound As Boolean = False
                For Each WQ_Issue As CompanyWorkQueueIssue In Me.CompanyWorkQueueIssueChildren
                    Dim WorkQueue_id As Guid = New Guid(Str)
                    If WQ_Issue.WorkqueueId = WorkQueue_id Then
                        dFound = True : Exit For
                    End If
                Next
                If Not dFound Then
                    Dim CompWQ As CompanyWorkQueueIssue = Me.GetNewCompanyWorkQueueIssueChild()
                    CompWQ.BeginEdit()
                    CompWQ.WorkqueueId = New Guid(Str)
                    CompWQ.CompanyId = GetcompanyIdfromWorkQueue(CompWQ.WorkqueueId)
                    CompWQ.EndEdit()
                    CompWQ.Save()
                End If
            Next
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Function

    Private Function GetcompanyIdfromWorkQueue(WorkqueueId As Guid) As Guid
        'returns company id
        Dim WQ_List As WrkQueue.WorkQueue() = WorkQueue.GetList("*", "", "CLM", Nothing, False)
        Dim Comp_Code As String = (From wq In WQ_List
                                   Where wq.Id = WorkqueueId
                                   Select wq.CompanyCode).First

        If Not Comp_Code Is Nothing And Comp_Code <> "" Then
            Return LookupListNew.GetIdFromCode(LookupListNew.LK_COMPANY, Comp_Code)
        End If
    End Function

#End Region


#Region "Custom Validations"

    'Added manually to the code
    Public Overrides ReadOnly Property IsDirty() As Boolean
        Get
            Return MyBase.IsDirty OrElse Me.IsChildrenDirty
        End Get
    End Property

    Public Sub Copy(ByVal original As Issue)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Detail List")
        End If
        MyBase.CopyFrom(original)
        'copy the childrens        
        Dim ChildIssueComment As IssueComment
        For Each ChildIssueComment In original.IssueNotesChildren
            Dim newDetail As IssueComment = Me.IssueNotesChildren.GetNewChild
            newDetail.Copy(ChildIssueComment)
            newDetail.IssueId = Me.Id
            newDetail.Save()
        Next

        Dim ChildIssueQuestion As IssueQuestion
        For Each ChildIssueQuestion In original.IssueQuestionsChildren
            Dim newDetail As IssueQuestion = Me.IssueQuestionsChildren.GetNewChild
            newDetail.Copy(ChildIssueQuestion)
            newDetail.IssueId = Me.Id
            newDetail.Save()
        Next

        Dim ChildRuleIssue As RuleIssue
        For Each ChildRuleIssue In original.IssueRulesChildren
            Dim newDetail As RuleIssue = Me.IssueRulesChildren.GetNewChild
            newDetail.Copy(ChildRuleIssue)
            newDetail.IssueId = Me.Id
            newDetail.Save()
        Next

        Dim ChildCompanyWorkQueueIssue As CompanyWorkQueueIssue
        For Each ChildRuleIssue In original.CompanyWorkQueueIssueChildren
            Dim newDetail As CompanyWorkQueueIssue = Me.CompanyWorkQueueIssueChildren.GetNewChild
            newDetail.Copy(ChildRuleIssue)
            newDetail.IssueId = Me.Id
            newDetail.Save()
        Next


    End Sub

#End Region


#Region "Custom Validations"

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class CheckDuplicate
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, EQUIPMENT_FORM004)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Issue = CType(objectToValidate, Issue)
            If (obj.CheckDuplicateIssueListCode()) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)>
    Public NotInheritable Class CheckListCodeDatesOverlaped
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, EQUIPMENT_FORM005)
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As Issue = CType(objectToValidate, Issue)
            If (obj.CheckListCodeDatesForOverlap()) Then
                Return False
            Else
                Return True
            End If
        End Function
    End Class

    Protected Function CheckDuplicateIssueListCode() As Boolean
        Dim EquipDal As New IssueDAL
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        If Not Code Is String.Empty And Not Effective Is Nothing Then
            Dim dv As Issue.IssueSearchDV = New Issue.IssueSearchDV(EquipDal.LoadList(Code, String.Empty,
                   ActiveOn, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

            If Not Me.Code Is Nothing And Not Me.Effective Is Nothing Then
                For Each dr As DataRow In dv.Table.Rows
                    If ((dr(IssueDAL.COL_NAME_CODE).ToString.ToUpper = Me.Code.ToUpper) And
                        (dr(IssueDAL.COL_NAME_EFFECTIVE) = Date.Parse(Me.Effective).ToString("dd-MMM-yyyy")) And
                        Not DirectCast(dr(IssueDAL.COL_NAME_ISSUE_ID), Byte()).SequenceEqual(Me.Id.ToByteArray)) Then
                        Return True
                    End If
                Next
            End If
        End If
        Return False
    End Function

    Protected Function CheckListCodeDatesForOverlap() As Boolean
        Dim IssueDal As New IssueDAL
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        If Not Code Is String.Empty And Not Description Is String.Empty And Not Effective Is Nothing And Nothing And Not Expiration Is Nothing Then
            Dim dv As Issue.IssueSearchDV = New Issue.IssueSearchDV(IssueDal.LoadList(Code, String.Empty,
                   ActiveOn, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

            For Each dr As DataRow In dv.Table.Rows
                If ((Not dr(EquipmentDAL.COL_NAME_CODE) = Me.Code) And
                    (Not dr(EquipmentDAL.COL_NAME_EFFECTIVE) >= Equals(Me.Effective)) And
                    (Not dr(EquipmentDAL.COL_NAME_EXPIRATION) <= Equals(Me.Expiration))) Then
                    Return True
                End If
            Next
        End If
        Return False
    End Function

    Public Shared Function CheckDuplicateEquipmentListCode(ByVal vCode As String, ByVal vEffective As String, ByVal vId As Guid) As Boolean
        Dim IssueDal As New IssueDAL
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        If Not vCode Is String.Empty And Not vEffective Is Nothing Then
            Dim dv As Issue.IssueSearchDV = New Issue.IssueSearchDV(IssueDal.LoadList(vCode, String.Empty,
                   String.Empty, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0))

            If Not vCode Is Nothing And Not vEffective Is Nothing Then
                For Each dr As DataRow In dv.Table.Rows
                    If ((dr(IssueDAL.COL_NAME_CODE).ToString.ToUpper = vCode.ToUpper) And
                        (dr(IssueDAL.COL_NAME_EFFECTIVE) = vEffective) And
                        Not DirectCast(dr(IssueDAL.COL_NAME_ISSUE_ID), Byte()).SequenceEqual(vId.ToByteArray)) Then
                        Return True
                    End If
                Next
            End If
        End If
        Return False
    End Function

#End Region

#Region "Public Methods"

    Public Function IsDuplicaetNoteType() As Boolean
        Dim notesTypeList As System.Collections.Generic.List(Of Guid) = New System.Collections.Generic.List(Of Guid)()
        Dim temp As Byte()
        If Me.MyDataset.Tables(TABLE_ISSUE_COMMENT).Rows.Count > 0 Then
            For Each Row As DataRow In Me.MyDataset.Tables(TABLE_ISSUE_COMMENT).Rows
                temp = Row(ISSUE_COMMENT_TYPE_ID)
                If notesTypeList.Contains(New Guid(temp)) Then
                    Continue For
                Else
                    notesTypeList.Add(New Guid(temp))
                End If
            Next
        End If
        If notesTypeList.Count < Me.MyDataset.Tables(TABLE_ISSUE_COMMENT).Rows.Count Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Shared Function CheckListCodeForOverlap(ByVal code As String, ByVal effective As DateType, _
                                        ByVal expiration As DateType, ByVal listId As Guid) As Boolean

        Try
            Dim dal As New IssueDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            If Not code Is String.Empty Then
                Dim ds As DataSet = dal.CheckOverlap(code, effective, _
                    expiration, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId, listId)

                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                End If
            End If
            Return False
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

        Return False
    End Function


    Public Shared Function CheckListCodeDurationOverlap(ByVal code As String, ByVal effective As DateType, _
                                        ByVal expiration As DateType, ByVal listId As Guid) As Boolean

        Try
            Dim dal As New IssueDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            If Not code Is String.Empty Then
                Dim ds As DataSet = dal.CheckDurationOverlap(code, effective, _
                    expiration, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId, listId)

                If ds.Tables(0).Rows.Count > 0 Then
                    Return True
                End If
            End If
            Return False
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

        Return False
    End Function


    Public Shared Function ExpirePreviousList(ByVal code As String, ByVal effective As DateType, _
                                        ByVal expiration As DateType, ByVal listId As Guid) As Boolean

        Try
            Dim dal As New IssueDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Return dal.ExpireList(dal.CheckOverlapToExpire(code, effective, _
                expiration, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId, listId), effective.ToString)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

        Return False
    End Function


    Public Shared Function CheckIfIssueIsAssignedToQuestionNoteOrRule(ByVal vId As Guid) As Boolean
        Dim IssueDAL As New IssueDAL
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)
        If IssueDAL.IsAssignedQuestionNoteOrRule(vId).Tables(0).Rows.Count > 0 Then
            Return True
        End If
        Return False
    End Function

    Public Shared Function GetSelectedIssueList(ByVal IssueID As Guid) As DataView
        Dim eqListDal As New IssueDAL
        Return eqListDal.GetSelectedIssuesList(IssueID).Tables(0).DefaultView
    End Function

    Public Shared Function GetSelectedQuestionsList(ByVal IssueID As Guid) As DataView
        Dim issueDal As New IssueDAL
        Return issueDal.GetSelectedQuestionsList(IssueID).Tables(0).DefaultView
    End Function

    Public Shared Function GetSelectedRulesList(ByVal IssueID As Guid) As DataView
        Dim issueDal As New IssueDAL
        Return issueDal.GetSelectedRulesList(IssueID).Tables(0).DefaultView
    End Function

    Public Function ExecuteQuestionsListFilter(ByVal Issue As Guid, ByVal QuestionList As String, ByVal SearchTags As String, ByVal ActiveOn As String) As DataView
        Dim issueDal As New IssueQuestionDAL
        Return issueDal.AvailableQuestionListFilter(Issue, QuestionList, SearchTags, ActiveOn, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0).DefaultView
    End Function

    Public Function ExecuteRulesListFilter(ByVal Issue As Guid) As DataView
        Dim issueDal As New IssueDAL
        Return issueDal.ExecuteRulesFilter(Issue).Tables(0).DefaultView
    End Function

    Public Shared Function GetDropdownCodeToUpdate(ByVal IssueCommentId As Byte()) As String
        Dim IssueCommentDal As New IssueCommentDAL
        Dim oCompanyGroupIds As ArrayList
        oCompanyGroupIds = New ArrayList
        oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

        Return IssueCommentDal.GetDropdownCodeToUpdate(New Guid(IssueCommentId)).Tables(0).Rows(0)(0).ToString

    End Function

#End Region


End Class



