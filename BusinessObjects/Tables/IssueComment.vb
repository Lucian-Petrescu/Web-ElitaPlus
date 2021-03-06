﻿
'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (5/30/2012)  ********************

Public Class IssueComment
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
            Dim dal As New IssueCommentDAL
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
            Dim dal As New IssueCommentDAL
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

    Protected Function CheckDuplicateNoteType() As Boolean
        Dim returnValue As Boolean = True
        Dim issueCommentTable As DataTable
        Dim issueCommentObj As IssueComment
        Dim issueCommentId As Guid
        If Me.Dataset.Tables.Contains(IssueCommentDAL.TABLE_NAME) Then
            issueCommentTable = Me.Dataset.Tables(IssueCommentDAL.TABLE_NAME)
            For Each issueCommentRow As DataRow In issueCommentTable.Rows
                issueCommentId = New Guid(CType(issueCommentRow(IssueCommentDAL.COL_NAME_ISSUE_COMMENT_ID), Byte()))
                issueCommentObj = New IssueComment(issueCommentId, Me.Dataset)
                If (Not issueCommentObj.Id.Equals(Me.Id) AndAlso issueCommentObj.IssueCommentTypeId.Equals(Me.IssueCommentTypeId)) Then
                    returnValue = False
                    Exit For
                End If
            Next
        End If
        Return returnValue
    End Function
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If Row(IssueCommentDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(IssueCommentDAL.COL_NAME_ISSUE_COMMENT_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property IssueId() As Guid
        Get
            CheckDeleted()
            If Row(IssueCommentDAL.COL_NAME_ISSUE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(IssueCommentDAL.COL_NAME_ISSUE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(IssueCommentDAL.COL_NAME_ISSUE_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), DuplicateNoteTypeValidator("")> _
    Public Property IssueCommentTypeId() As Guid
        Get
            CheckDeleted()
            If Row(IssueCommentDAL.COL_NAME_ISSUE_COMMENT_TYPE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(IssueCommentDAL.COL_NAME_ISSUE_COMMENT_TYPE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(IssueCommentDAL.COL_NAME_ISSUE_COMMENT_TYPE_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1020)> _
    Public Property Code() As String
        Get
            CheckDeleted()
            If Row(IssueCommentDAL.COL_NAME_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(IssueCommentDAL.COL_NAME_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(IssueCommentDAL.COL_NAME_CODE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=4000)> _
    Public Property Text() As String
        Get
            CheckDeleted()
            If Row(IssueCommentDAL.COL_NAME_TEXT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(IssueCommentDAL.COL_NAME_TEXT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(IssueCommentDAL.COL_NAME_TEXT, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DisplayOnWeb() As Guid
        Get
            CheckDeleted()
            If Row(IssueCommentDAL.COL_NAME_DISPLAY_ON_WEB) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(IssueCommentDAL.COL_NAME_DISPLAY_ON_WEB), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(IssueCommentDAL.COL_NAME_DISPLAY_ON_WEB, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New IssueCommentDAL
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

    Public Sub Copy(ByVal original As IssueComment)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Issue Comment.")
        End If
        MyBase.CopyFrom(original)
    End Sub

    Public Shared Function IsChild(ByVal IssueId As Guid, ByVal IssueCommentId As Guid) As Byte()

        Try
            Dim dal As New IssueCommentDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Dim ds As DataSet = dal.IsChild(IssueCommentId, IssueId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If Not ds Is Nothing Then
                If ds.Tables(IssueCommentDAL.TABLE_NAME).Rows.Count > 0 Then
                    Return ds.Tables(IssueCommentDAL.TABLE_NAME).Rows(0)(IssueCommentDAL.COL_NAME_ISSUE_COMMENT_ID)
                Else
                    Return Guid.Empty.ToByteArray
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

#Region "DataView Retrieveing Methods"

    Public Function GetNotesSelectionView() As NotesSelectionView
        Dim t As DataTable = NotesSelectionView.CreateTable
        Dim detail As IssueComment
        Dim issue As Issue = New Issue()

        For Each detail In issue.IssueNotesChildren
            Dim row As DataRow = t.NewRow
            row(NotesSelectionView.COL_NAME_ISSUE_COMMENT_ID) = detail.IssueCommentTypeId
            row(NotesSelectionView.COL_NAME_NAME_ISSUE_COMMENT_TYPE_ID) = detail.IssueCommentTypeId
            row(NotesSelectionView.COL_NAME_CODE) = detail.Code
            row(NotesSelectionView.COL_NAME_TEXT) = detail.Text
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
            t.Columns.Add(COL_NAME_NAME_ISSUE_COMMENT_TYPE_ID, GetType(String))
            t.Columns.Add(COL_NAME_CODE, GetType(String))
            t.Columns.Add(COL_NAME_TEXT, GetType(String))
            Return t
        End Function
    End Class

    Public Shared Function GetNewDataViewRow(ByVal dv As DataView, ByVal id As Guid, ByVal bo As IssueComment) As DataView
        Dim dt As DataTable
        dt = dv.Table
        Dim row As DataRow = dt.NewRow
        row(IssueCommentDAL.COL_NAME_ISSUE_COMMENT_ID) = Guid.Empty.ToByteArray
        row(IssueCommentDAL.COL_NAME_ISSUE_COMMENT_TYPE_ID) = Guid.Empty.ToByteArray
        row(IssueCommentDAL.COL_NAME_CODE) = String.Empty
        row(IssueCommentDAL.COL_NAME_TEXT) = String.Empty
        Return (dv)
    End Function

    Public Shared Function GetList(ByVal IssueId As Guid) As IssueComment.IssueCommentGridDV

        Try
            Dim dal As New IssueCommentDAL
            Return New IssueCommentGridDV(dal.LoadList(IssueId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class IssueCommentGridDV
        Inherits DataView

#Region "Constants"
        Public Const COL_NAME_ISSUE_COMMENT_ID As String = IssueCommentDAL.COL_NAME_ISSUE_COMMENT_ID
        Public Const COL_NAME_ISSUE_ID As String = IssueCommentDAL.COL_NAME_ISSUE_ID
        Public Const COL_NAME_ISSUE_COMMENT_TYPE_ID As String = IssueCommentDAL.COL_NAME_ISSUE_COMMENT_TYPE_ID
        Public Const COL_NAME_CODE As String = IssueCommentDAL.COL_NAME_CODE
        Public Const COL_NAME_TEXT As String = IssueCommentDAL.COL_NAME_TEXT
        Public Const COL_NAME_DISPLAY_ON_WEB As String = IssueCommentDAL.COL_NAME_DISPLAY_ON_WEB
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        'Public Shared ReadOnly Property IssueComment(ByVal row) As Guid
        '    Get
        '        Return New Guid(CType(row(COL_NAME_ISSUE_COMMENT_ID), Byte()))
        '    End Get
        'End Property
    End Class
#End Region

#Region "Custom Validators"
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field)> _
    Public NotInheritable Class DuplicateNoteTypeValidator
        Inherits ValidBaseAttribute

        Public Sub New(ByVal fieldDisplayName As String)
            MyBase.New(fieldDisplayName, "ONLY_ONE_NOTE_TYPE_ALLOWED")
        End Sub

        Public Overrides Function IsValid(ByVal objectToCheck As Object, ByVal objectToValidate As Object) As Boolean
            Dim obj As IssueComment = CType(objectToValidate, IssueComment)
            Return obj.CheckDuplicateNoteType()
        End Function
    End Class

#End Region
End Class




