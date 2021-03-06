﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/30/2009)  ********************

Public Class TransallMapping
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
            Dim dal As New TransallMappingDAL
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
            Dim dal As New TransallMappingDAL
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
            If row(TransallMappingDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TransallMappingDAL.COL_NAME_TRANSALL_MAPPING_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(TransAllMappingDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(TransAllMappingDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(TransAllMappingDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=400)> _
    Public Property InboundFilename() As String
        Get
            CheckDeleted()
            If row(TransallMappingDAL.COL_NAME_INBOUND_FILENAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransallMappingDAL.COL_NAME_INBOUND_FILENAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransallMappingDAL.COL_NAME_INBOUND_FILENAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=400)> _
    Public Property OutboundFilenameRegex() As String
        Get
            CheckDeleted()
            If row(TransallMappingDAL.COL_NAME_OUTBOUND_FILENAME_REGEX) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransallMappingDAL.COL_NAME_OUTBOUND_FILENAME_REGEX), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransallMappingDAL.COL_NAME_OUTBOUND_FILENAME_REGEX, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=400)> _
    Public Property OutputPath() As String
        Get
            CheckDeleted()
            If Row(TransAllMappingDAL.COL_NAME_OUTPUT_PATH) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransAllMappingDAL.COL_NAME_OUTPUT_PATH), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransAllMappingDAL.COL_NAME_OUTPUT_PATH, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=400)> _
    Public Property TransallPackage() As String
        Get
            CheckDeleted()
            If row(TransallMappingDAL.COL_NAME_TRANSALL_PACKAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransallMappingDAL.COL_NAME_TRANSALL_PACKAGE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransallMappingDAL.COL_NAME_TRANSALL_PACKAGE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4000)> _
    Public Property LogfileEmails() As String
        Get
            CheckDeleted()
            If row(TransallMappingDAL.COL_NAME_LOGFILE_EMAILS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransallMappingDAL.COL_NAME_LOGFILE_EMAILS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(TransallMappingDAL.COL_NAME_LOGFILE_EMAILS, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property NumFiles() As LongType
        Get
            CheckDeleted()
            If row(TransallMappingDAL.COL_NAME_NUM_FILES) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(TransallMappingDAL.COL_NAME_NUM_FILES), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(TransallMappingDAL.COL_NAME_NUM_FILES, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property LayoutCodeId() As Guid
        Get
            CheckDeleted()
            If Row(TransAllMappingDAL.COL_NAME_LAYOUT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(TransAllMappingDAL.COL_NAME_LAYOUT_CODE), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(TransAllMappingDAL.COL_NAME_LAYOUT_CODE, Value)
        End Set
    End Property

    Public Property FtpSiteId() As Guid
        Get
            CheckDeleted()
            If Row(TransAllMappingDAL.COL_NAME_FTP_SITE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(TransAllMappingDAL.COL_NAME_FTP_SITE_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(TransAllMappingDAL.COL_NAME_FTP_SITE_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New TransallMappingDAL
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

#Region "DataView Retrieveing Methods"

    Public Shared Function GetList(ByVal DealerId As Guid, Optional ByVal CompanyIds As ArrayList = Nothing) As DataView

        Try
            Dim dal As New DALObjects.TransAllMappingDAL
            Return New System.Data.DataView(dal.LoadList(DealerId, CompanyIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetList(ByVal FileName As String, Optional ByVal CompanyIds As ArrayList = Nothing) As DataView

        Try
            Dim dal As New DALObjects.TransAllMappingDAL
            Return New System.Data.DataView(dal.LoadList(FileName, CompanyIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetListByDirectory(ByVal DirectoryName As String, Optional ByVal CompanyIds As ArrayList = Nothing) As DataView

        Try
            Dim dal As New DALObjects.TransAllMappingDAL
            Return New System.Data.DataView(dal.LoadListByDirectory(DirectoryName, CompanyIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function GetListByDirectoryAndFile(ByVal DirectoryName As String, ByVal partialFileName As String, Optional ByVal CompanyIds As ArrayList = Nothing) As DataView

        Try
            Dim dal As New DALObjects.TransAllMappingDAL
            Return New System.Data.DataView(dal.LoadListByDirectoryAndFileName(DirectoryName, partialFileName, CompanyIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetListByOutputDirectoryAndFile(ByVal DirectoryName As String, ByVal partialFileName As String, Optional ByVal CompanyIds As ArrayList = Nothing) As DataView

        Try
            Dim dal As New DALObjects.TransAllMappingDAL
            Return New System.Data.DataView(dal.LoadListByOutputDirectoryAndFileName(DirectoryName, partialFileName, CompanyIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetListByOutputDirectory(ByVal DirectoryName As String, Optional ByVal CompanyIds As ArrayList = Nothing) As DataView

        Try
            Dim dal As New DALObjects.TransAllMappingDAL
            Return New System.Data.DataView(dal.LoadListByOutputDirectory(DirectoryName, CompanyIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

  

#Region "Children Related"
    Public Sub DetachTransMapOut(ByVal transMapOutIdGuidStrCollection As ArrayList)
        Dim transMapOutIdStr As Guid
        For Each transMapOutIdStr In transMapOutIdGuidStrCollection
            Dim tranMapOutBO As TransallMappingOut = New TransallMappingOut(transMapOutIdStr)
            If Not tranMapOutBO Is Nothing Then
                tranMapOutBO.Delete()
                tranMapOutBO.Save()
            End If
        Next
    End Sub
#End Region

    Public Shared Sub LogTransallMessage(ByVal region As String, ByVal filename As String, ByVal code As String, ByVal log_details As String, ByVal created_by As String)
        Try
            Dim dal As New TransAllMappingDAL
            dal.LogTransallErrors(region, filename, code, log_details, created_by)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

End Class


