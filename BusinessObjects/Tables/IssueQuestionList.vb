﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/27/2012)  ********************
Imports System.Guid

Public Class IssueQuestionList
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
            Dim dal As New IssueQuestionListDAL
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
            Dim dal As New IssueQuestionListDAL
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

#Region "Constants"
    Private ReadOnly EQUIPMENT_EXPIRATION_DEFAULT As New DateTime(2499, 12, 31, 23, 59, 59)
#End Region

#Region "Properties"

    'Key Property
    Public ReadOnly Property Id() As Guid
        Get
            If row(IssueQuestionListDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(IssueQuestionListDAL.COL_NAME_ISSUE_QUESTION_LIST_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property QuestionListId() As Guid
        Get
            CheckDeleted()
            If Row(IssueQuestionListDAL.COL_NAME_QUESTION_LIST_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(IssueQuestionListDAL.COL_NAME_QUESTION_LIST_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(IssueQuestionListDAL.COL_NAME_QUESTION_LIST_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property IssueQuestionId() As Guid
        Get
            CheckDeleted()
            If Row(IssueQuestionListDAL.COL_NAME_ISSUE_QUESTION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(IssueQuestionListDAL.COL_NAME_ISSUE_QUESTION_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(IssueQuestionListDAL.COL_NAME_ISSUE_QUESTION_ID, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property DisplayOrder() As LongType
        Get
            CheckDeleted()
            If row(IssueQuestionListDAL.COL_NAME_DISPLAY_ORDER) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(IssueQuestionListDAL.COL_NAME_DISPLAY_ORDER), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(IssueQuestionListDAL.COL_NAME_DISPLAY_ORDER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property Effective() As DateType
        Get
            CheckDeleted()
            If row(IssueQuestionListDAL.COL_NAME_EFFECTIVE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(IssueQuestionListDAL.COL_NAME_EFFECTIVE).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(IssueQuestionListDAL.COL_NAME_EFFECTIVE, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property Expiration() As DateType
        Get
            CheckDeleted()
            If row(IssueQuestionListDAL.COL_NAME_EXPIRATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(DateHelper.GetDateValue(Row(IssueQuestionListDAL.COL_NAME_EXPIRATION).ToString()))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(IssueQuestionListDAL.COL_NAME_EXPIRATION, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New IssueQuestionListDAL
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

    Public Sub AttachEquipments(ByVal selectedEquipmentGuidStrCollection As ArrayList)
        Dim cmpEquipmentIdStr As String
        For Each cmpEquipmentIdStr In selectedEquipmentGuidStrCollection
            Dim newBO As EquipmentListDetail = New EquipmentListDetail(Me.Dataset)
            If Not newBO Is Nothing Then
                newBO.EquipmentId = Me.ID
                newBO.EquipmentId = New Guid(cmpEquipmentIdStr)
                newBO.Save()
            End If
        Next

    End Sub

#End Region

#Region "DataView Retrieveing Methods"

#End Region

#Region "Public Methods"
    Public Shared Function IsChild(ByVal QuestionListId As Guid, ByVal QuestionId As Guid) As Byte()

        Try
            Dim dal As New IssueQuestionListDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Dim ds As DataSet = dal.IsChild(QuestionListId, QuestionId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If Not ds Is Nothing Then
                If ds.Tables(IssueQuestionListDAL.TABLE_NAME).Rows.Count > 0 Then
                    Return ds.Tables(IssueQuestionListDAL.TABLE_NAME).Rows(0)(IssueQuestionListDAL.COL_NAME_ISSUE_QUESTION_LIST_ID)
                Else
                    Return Guid.Empty.ToByteArray
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function IsDictChild(ByVal EquipListId As Guid, ByVal EquipId As Guid) As Byte()

        Try
            Dim dal As New IssueQuestionListDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Dim ds As DataSet = dal.IsChild(EquipListId, EquipId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            If Not ds Is Nothing Then
                If ds.Tables(IssueQuestionListDAL.TABLE_NAME).Rows.Count > 0 Then
                    Return ds.Tables(IssueQuestionListDAL.TABLE_NAME).Rows(0)(IssueQuestionListDAL.COL_NAME_ISSUE_QUESTION_LIST_ID)
                Else
                    Return Guid.Empty.ToByteArray
                End If
            End If
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Sub Copy(ByVal original As IssueQuestionList)
        If Not Me.IsNew Then
            Throw New BOInvalidOperationException("You cannot copy into an existing Best Replacement.")
        End If
        MyBase.CopyFrom(original)
    End Sub

    Public Shared Function GetQuestionInList(ByVal QuestionListId As Guid) As ArrayList

        Try
            Dim dal As New IssueQuestionListDAL
            Dim oCompanyGroupIds As ArrayList
            Dim oQuestionList As ArrayList
            Dim oDataTable As DataTable

            oCompanyGroupIds = New ArrayList
            oQuestionList = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            oDataTable = dal.GetQuestionInList(QuestionListId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0)
            For Each row As DataRow In oDataTable.Rows
                oQuestionList.Add(row(0))
            Next
            Return oQuestionList
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetQuestionList(ByVal QuestionListId As Guid) As ArrayList

        Try
            Dim dal As New IssueQuestionListDAL
            Dim oCompanyGroupIds As ArrayList
            Dim oQuestionList As ArrayList
            Dim oDataTable As DataTable

            oCompanyGroupIds = New ArrayList
            oQuestionList = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            oDataTable = dal.GetQuestionList(QuestionListId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0)
            For Each row As DataRow In oDataTable.Rows
                oQuestionList.Add(row(0))
            Next
            Return oQuestionList
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function SaveDealerList(ByVal DealerList As ArrayList, ByVal QuestionLIstCode As String) As Boolean

        Try
            Dim dal As New IssueQuestionListDAL
            Dim oCompanyGroupIds As ArrayList
            Dim oDealerList As ArrayList
            Dim oDataTable As DataTable

            oCompanyGroupIds = New ArrayList
            oDealerList = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            For Each DealerID In DealerList
                oDataTable = dal.SaveDealerList(DealerID, QuestionLIstCode, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0)
            Next
            Return True
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetDealerInList(ByVal QuestionListCode As String) As ArrayList

        Try
            Dim dal As New IssueQuestionListDAL
            Dim oCompanyGroupIds As ArrayList
            Dim oDealerList As ArrayList
            Dim oDataTable As DataTable
            oCompanyGroupIds = New ArrayList
            oDealerList = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            oDataTable = dal.GetDealerInList(QuestionListCode, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId).Tables(0)
            For Each row As DataRow In oDataTable.Rows
                oDealerList.Add(New Guid(CType(row(0), Byte())).ToString)
            Next
            Return oDealerList
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetQuestionExpiration(ByVal IssueQuestionListId As Guid) As DateTime

        Try
            Dim dal As New IssueQuestionListDAL
            Dim oCompanyGroupIds As ArrayList

            oCompanyGroupIds = New ArrayList
            oCompanyGroupIds.Add(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)

            Dim ds As DataSet = dal.GetQuestionExpiration(IssueQuestionListId, oCompanyGroupIds, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
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

    Public Shared Function GetCurrentDateTime() As DateTime
        Try
            Dim dal As New EquipmentListDetailDAL
            Return dal.GetCurrentDateTime()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region


End Class



