﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (6/6/2008)  ********************

Public Class RegistrationLetter
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
            Dim dal As New RegistrationLetterDAL
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
            Dim dal As New RegistrationLetterDAL
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
            If row(RegistrationLetterDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RegistrationLetterDAL.COL_NAME_REGISTRATION_LETTER_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If row(RegistrationLetterDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(RegistrationLetterDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(RegistrationLetterDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=3)> _
    Public Property LetterType() As String
        Get
            CheckDeleted()
            If Row(RegistrationLetterDAL.COL_NAME_LETTER_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RegistrationLetterDAL.COL_NAME_LETTER_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RegistrationLetterDAL.COL_NAME_LETTER_TYPE, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidNumericRange("", Min:=0)> _
    Public Property NumberOfDays() As LongType
        Get
            CheckDeleted()
            If Row(RegistrationLetterDAL.COL_NAME_NUMBER_OF_DAYS) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(RegistrationLetterDAL.COL_NAME_NUMBER_OF_DAYS), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(RegistrationLetterDAL.COL_NAME_NUMBER_OF_DAYS, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=100)> _
    Public Property EmailSubject() As String
        Get
            CheckDeleted()
            If Row(RegistrationLetterDAL.COL_NAME_EMAIL_SUBJECT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RegistrationLetterDAL.COL_NAME_EMAIL_SUBJECT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RegistrationLetterDAL.COL_NAME_EMAIL_SUBJECT, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=4000)> _
    Public Property EmailText() As String
        Get
            CheckDeleted()
            If Row(RegistrationLetterDAL.COL_NAME_EMAIL_TEXT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RegistrationLetterDAL.COL_NAME_EMAIL_TEXT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RegistrationLetterDAL.COL_NAME_EMAIL_TEXT, Value)
        End Set
    End Property

    Public ReadOnly Property OriginalNumberOfDays() As LongType
        Get
            Return New LongType(CType(Me.Row(RegistrationLetterDAL.COL_NAME_NUMBER_OF_DAYS, DataRowVersion.Original), Long))
        End Get
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=100)> _
    Public Property EmailFrom() As String
        Get
            CheckDeleted()
            If Row(RegistrationLetterDAL.COL_NAME_EMAIL_FROM) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RegistrationLetterDAL.COL_NAME_EMAIL_FROM), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RegistrationLetterDAL.COL_NAME_EMAIL_FROM, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=100)> _
    Public Property EmailTo() As String
        Get
            CheckDeleted()
            If Row(RegistrationLetterDAL.COL_NAME_EMAIL_TO) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RegistrationLetterDAL.COL_NAME_EMAIL_TO), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RegistrationLetterDAL.COL_NAME_EMAIL_TO, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=100)> _
    Public Property AttachmentFileName() As String
        Get
            CheckDeleted()
            If Row(RegistrationLetterDAL.COL_NAME_ATTACHMENT_FILE_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(RegistrationLetterDAL.COL_NAME_ATTACHMENT_FILE_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(RegistrationLetterDAL.COL_NAME_ATTACHMENT_FILE_NAME, Value)
        End Set
    End Property

    'Public Property AttachmentFileData() As Byte()
    '    Get
    '        CheckDeleted()
    '        If Row(RegistrationLetterDAL.COL_NAME_ATTACHMENT_FILE_DATA) Is DBNull.Value Then
    '            Return Nothing
    '        Else
    '            Return CType(Row(RegistrationLetterDAL.COL_NAME_ATTACHMENT_FILE_DATA), Byte())
    '        End If
    '    End Get
    '    Set(ByVal Value As Byte())
    '        CheckDeleted()
    '        Me.SetValue(RegistrationLetterDAL.COL_NAME_ATTACHMENT_FILE_DATA, Value)
    '    End Set
    'End Property
#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New RegistrationLetterDAL
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

    Public Overrides Sub Delete()
        Try
            Me.CheckDeleted()
            If Not Me.IsNew Then
                Dim maxDay As New MaxNumberOfDays(Me)
                If Not maxDay.isLast Then
                    ' Dim err As New ValidationError(Common.ErrorCodes.INVALID_EFFECTIVE_EXPIRATION_ERR, Me.GetType, GetType(ValidEffectiveAndExpirationDate), "Effective", Me.Effective)
                    Dim err As New ValidationError(Common.ErrorCodes.INVALID_REGISTRATION_DELETE_DAY_ERR, _
                            Me.GetType, Nothing, "NumberOfDays", Me.NumberOfDays)
                    Throw New BOValidationException(New ValidationError() {err}, Me.GetType.Name, Me.UniqueId)
                    ' Throw New BOValidationException()
                End If
            End If
            MyBase.Delete()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.WriteErr, ex)
        End Try
    End Sub

    Public Sub UpdateAttachment(ByVal RLId As Guid, ByVal AttachmentData As Byte())
        Dim dal As New RegistrationLetterDAL
        dal.UpdateAttachment(RLId, AttachmentData)
    End Sub

    Public Class MaxNumberOfDays
        Public maxDay As Integer
        Public isLast As Boolean = False

        Public Sub New(ByVal obj As RegistrationLetter)
            Try
                Dim dal As New RegistrationLetterDAL
                Dim maxDs As DataSet = dal.LoadMaxDay(obj.DealerId)
                If maxDs.Tables(0).Rows.Count > 0 AndAlso _
                    (Not maxDs.Tables(0).Rows(0)(dal.COL_NAME_NUMBER_OF_DAYS) Is DBNull.Value) Then
                    maxDay = CType(maxDs.Tables(0).Rows(0)(dal.COL_NAME_NUMBER_OF_DAYS), Integer)

                    isLast = Not obj.IsNew AndAlso (obj.OriginalNumberOfDays.Value = maxDay)
                End If
            Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
                Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
            End Try
        End Sub

    End Class

#End Region

#Region "DataView Retrieveing Methods"

    Public Shared Function getList(ByVal compIds As ArrayList, ByVal dealerId As Guid) As RegistrationLetterSearchDV
        Try
            Dim dal As New RegistrationLetterDAL
            Return New RegistrationLetterSearchDV(dal.LoadList(compIds, dealerId).Tables(0))

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function getEmptyList(ByVal dv As RegistrationLetterSearchDV) As DataView
        Try

            Dim dsv As DataSet
            dsv = dv.Table().DataSet

            Dim row As DataRow = dsv.Tables(0).NewRow()
            row.Item(RegistrationLetterDAL.COL_NAME_REGISTRATION_LETTER_ID) = System.Guid.NewGuid.ToByteArray

            dsv.Tables(0).Rows.Add(row)
            Return New System.Data.DataView(dsv.Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#Region "RegistrationLetterSearchDV"
    Public Class RegistrationLetterSearchDV
        Inherits DataView

#Region "Constants"
        Public Const COL_REGISTRATION_LETTER_ID As String = "registration_letter_id"
        Public Const COL_DEALER_CODE As String = "dealer_code"
        Public Const COL_DEALER_NAME As String = "dealer_name"
        Public Const COL_NUMBER_OF_DAYS As String = "number_of_days"
        Public Const COL_LETTER_TYPE As String = "LETTER_TYPE"
#End Region


        Public Sub New()
            MyBase.New()
        End Sub

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

    End Class

#End Region

#End Region

End Class



