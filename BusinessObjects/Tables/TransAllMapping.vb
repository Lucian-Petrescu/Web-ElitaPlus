﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/30/2009)  ********************

Public Class TransallMapping
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
            Dim dal As New TransallMappingDAL
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
            Dim dal As New TransallMappingDAL
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
    Public ReadOnly Property Id As Guid
        Get
            If row(TransallMappingDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(row(TransallMappingDAL.COL_NAME_TRANSALL_MAPPING_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property DealerId As Guid
        Get
            CheckDeleted()
            If Row(TransAllMappingDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(TransAllMappingDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransAllMappingDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=400)> _
    Public Property InboundFilename As String
        Get
            CheckDeleted()
            If row(TransallMappingDAL.COL_NAME_INBOUND_FILENAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransallMappingDAL.COL_NAME_INBOUND_FILENAME), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransallMappingDAL.COL_NAME_INBOUND_FILENAME, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=400)> _
    Public Property OutboundFilenameRegex As String
        Get
            CheckDeleted()
            If row(TransallMappingDAL.COL_NAME_OUTBOUND_FILENAME_REGEX) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransallMappingDAL.COL_NAME_OUTBOUND_FILENAME_REGEX), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransallMappingDAL.COL_NAME_OUTBOUND_FILENAME_REGEX, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=400)> _
    Public Property OutputPath As String
        Get
            CheckDeleted()
            If Row(TransAllMappingDAL.COL_NAME_OUTPUT_PATH) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(TransAllMappingDAL.COL_NAME_OUTPUT_PATH), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransAllMappingDAL.COL_NAME_OUTPUT_PATH, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=400)> _
    Public Property TransallPackage As String
        Get
            CheckDeleted()
            If row(TransallMappingDAL.COL_NAME_TRANSALL_PACKAGE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransallMappingDAL.COL_NAME_TRANSALL_PACKAGE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransallMappingDAL.COL_NAME_TRANSALL_PACKAGE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=4000)> _
    Public Property LogfileEmails As String
        Get
            CheckDeleted()
            If row(TransallMappingDAL.COL_NAME_LOGFILE_EMAILS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(row(TransallMappingDAL.COL_NAME_LOGFILE_EMAILS), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransallMappingDAL.COL_NAME_LOGFILE_EMAILS, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property NumFiles As LongType
        Get
            CheckDeleted()
            If row(TransallMappingDAL.COL_NAME_NUM_FILES) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(row(TransallMappingDAL.COL_NAME_NUM_FILES), Long))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransallMappingDAL.COL_NAME_NUM_FILES, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property LayoutCodeId As Guid
        Get
            CheckDeleted()
            If Row(TransAllMappingDAL.COL_NAME_LAYOUT_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(TransAllMappingDAL.COL_NAME_LAYOUT_CODE), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransAllMappingDAL.COL_NAME_LAYOUT_CODE, Value)
        End Set
    End Property

    Public Property FtpSiteId As Guid
        Get
            CheckDeleted()
            If Row(TransAllMappingDAL.COL_NAME_FTP_SITE_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(TransAllMappingDAL.COL_NAME_FTP_SITE_ID), Byte()))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(TransAllMappingDAL.COL_NAME_FTP_SITE_ID, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If _isDSCreator AndAlso IsDirty AndAlso Row.RowState <> DataRowState.Detached Then
                Dim dal As New TransallMappingDAL
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

    Public Shared Function GetList(DealerId As Guid, Optional ByVal CompanyIds As ArrayList = Nothing) As DataView

        Try
            Dim dal As New DALObjects.TransAllMappingDAL
            Return New System.Data.DataView(dal.LoadList(DealerId, CompanyIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetList(FileName As String, Optional ByVal CompanyIds As ArrayList = Nothing) As DataView

        Try
            Dim dal As New DALObjects.TransAllMappingDAL
            Return New System.Data.DataView(dal.LoadList(FileName, CompanyIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetListByDirectory(DirectoryName As String, Optional ByVal CompanyIds As ArrayList = Nothing) As DataView

        Try
            Dim dal As New DALObjects.TransAllMappingDAL
            Return New System.Data.DataView(dal.LoadListByDirectory(DirectoryName, CompanyIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Shared Function GetListByDirectoryAndFile(DirectoryName As String, partialFileName As String, Optional ByVal CompanyIds As ArrayList = Nothing) As DataView

        Try
            Dim dal As New DALObjects.TransAllMappingDAL
            Return New System.Data.DataView(dal.LoadListByDirectoryAndFileName(DirectoryName, partialFileName, CompanyIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetListByOutputDirectoryAndFile(DirectoryName As String, partialFileName As String, Optional ByVal CompanyIds As ArrayList = Nothing) As DataView

        Try
            Dim dal As New DALObjects.TransAllMappingDAL
            Return New System.Data.DataView(dal.LoadListByOutputDirectoryAndFileName(DirectoryName, partialFileName, CompanyIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function GetListByOutputDirectory(DirectoryName As String, Optional ByVal CompanyIds As ArrayList = Nothing) As DataView

        Try
            Dim dal As New DALObjects.TransAllMappingDAL
            Return New System.Data.DataView(dal.LoadListByOutputDirectory(DirectoryName, CompanyIds).Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region

  

#Region "Children Related"
    Public Sub DetachTransMapOut(transMapOutIdGuidStrCollection As ArrayList)
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

    Public Shared Sub LogTransallMessage(region As String, filename As String, code As String, log_details As String, created_by As String)
        Try
            Dim dal As New TransAllMappingDAL
            dal.LogTransallErrors(region, filename, code, log_details, created_by)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

End Class


