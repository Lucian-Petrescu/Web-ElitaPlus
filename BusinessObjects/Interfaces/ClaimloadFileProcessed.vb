﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (4/2/2010)  ********************
Imports System.Collections.Generic

Public Class ClaimloadFileProcessed
    Inherits BusinessObjectBase
    Implements IFileLoadHeaderWork

#Region "Constants"
    Public Const COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID As String = "claimload_file_processed_id"
    Public Const COL_NAME_COMPANY_ID As String = "company_id"
    Public Const COL_NAME_FILENAME As String = "filename"
    Public Const COL_NAME_RECEIVED As String = "received"
    Public Const COL_NAME_COUNTED As String = "counted"
    Public Const COL_NAME_REJECTED As String = "rejected"
    Public Const COL_NAME_VALIDATED As String = "validated"
    Public Const COL_NAME_LOADED As String = "loaded"
    Public Const COL_NAME_ACCOUNT_IDENTIFIER As String = "account_identifier"
    Public Const COL_NAME_SC_COUNTRY_CODE As String = "sc_country_code"
    Public Const COL_NAME_FILE_TYPE As String = "file_type"
#End Region

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
            Dim dal As New ClaimloadFileProcessedDAL
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
            Dim dal As New ClaimloadFileProcessedDAL
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
    Private _children As List(Of IFileLoadReconWork)
    Private _syncRoot As New Object

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub
#End Region

#Region "Properties"
    'Key Property
    Public ReadOnly Property Id() As Guid Implements IFileLoadHeaderWork.Id
        Get
            If Row(ClaimloadFileProcessedDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimloadFileProcessedDAL.COL_NAME_CLAIMLOAD_FILE_PROCESSED_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(ClaimloadFileProcessedDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(ClaimloadFileProcessedDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(ClaimloadFileProcessedDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=100)> _
    Public Property Filename() As String Implements IFileLoadHeaderWork.FileName
        Get
            CheckDeleted()
            If Row(ClaimloadFileProcessedDAL.COL_NAME_FILENAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadFileProcessedDAL.COL_NAME_FILENAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimloadFileProcessedDAL.COL_NAME_FILENAME, Value)
        End Set
    End Property

    Public Property Received() As LongType Implements IFileLoadHeaderWork.Received
        Get
            CheckDeleted()
            If Row(ClaimloadFileProcessedDAL.COL_NAME_RECEIVED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimloadFileProcessedDAL.COL_NAME_RECEIVED), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ClaimloadFileProcessedDAL.COL_NAME_RECEIVED, Value)
        End Set
    End Property

    Public Property Counted() As LongType Implements IFileLoadHeaderWork.Counted
        Get
            CheckDeleted()
            If Row(ClaimloadFileProcessedDAL.COL_NAME_COUNTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimloadFileProcessedDAL.COL_NAME_COUNTED), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ClaimloadFileProcessedDAL.COL_NAME_COUNTED, Value)
        End Set
    End Property

    Public Property Rejected() As LongType Implements IFileLoadHeaderWork.Rejected
        Get
            CheckDeleted()
            If Row(ClaimloadFileProcessedDAL.COL_NAME_REJECTED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimloadFileProcessedDAL.COL_NAME_REJECTED), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ClaimloadFileProcessedDAL.COL_NAME_REJECTED, Value)
        End Set
    End Property

    Public Property Validated() As LongType Implements IFileLoadHeaderWork.Validated
        Get
            CheckDeleted()
            If Row(ClaimloadFileProcessedDAL.COL_NAME_VALIDATED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimloadFileProcessedDAL.COL_NAME_VALIDATED), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ClaimloadFileProcessedDAL.COL_NAME_VALIDATED, Value)
        End Set
    End Property

    Public Property Bypassed() As LongType Implements IFileLoadHeaderWork.Bypassed
        Get
            Return 0
        End Get
        Set(ByVal value As LongType)
            ' Do Nothing
        End Set
    End Property

    Public Property Loaded() As LongType Implements IFileLoadHeaderWork.Loaded
        Get
            CheckDeleted()
            If Row(ClaimloadFileProcessedDAL.COL_NAME_LOADED) Is DBNull.Value Then
                Return Nothing
            Else
                Return New LongType(CType(Row(ClaimloadFileProcessedDAL.COL_NAME_LOADED), Long))
            End If
        End Get
        Set(ByVal Value As LongType)
            CheckDeleted()
            Me.SetValue(ClaimloadFileProcessedDAL.COL_NAME_LOADED, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=40)> _
    Public Property AccountIdentifier() As String
        Get
            CheckDeleted()
            If Row(ClaimloadFileProcessedDAL.COL_NAME_ACCOUNT_IDENTIFIER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadFileProcessedDAL.COL_NAME_ACCOUNT_IDENTIFIER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimloadFileProcessedDAL.COL_NAME_ACCOUNT_IDENTIFIER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=2)> _
    Public Property ServiceCenterCountryCode() As String
        Get
            CheckDeleted()
            If Row(ClaimloadFileProcessedDAL.COL_NAME_SC_COUNTRY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadFileProcessedDAL.COL_NAME_SC_COUNTRY_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimloadFileProcessedDAL.COL_NAME_SC_COUNTRY_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=3)> _
    Public Property FileType() As String
        Get
            CheckDeleted()
            If Row(ClaimloadFileProcessedDAL.COL_NAME_FILE_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(ClaimloadFileProcessedDAL.COL_NAME_FILE_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(ClaimloadFileProcessedDAL.COL_NAME_FILE_TYPE, Value)
        End Set
    End Property

    Public ReadOnly Property FamilyDataSet As DataSet Implements IFileLoadHeaderWork.FamilyDataSet
        Get
            Return Me.Dataset
        End Get
    End Property
#End Region

#Region "Public Members"
    Public Overloads Sub Save(ByVal oClaim As MultiAuthClaim)
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim tr As IDbTransaction = DBHelper.GetNewTransaction()
                If (Not oClaim Is Nothing) Then
                    oClaim.Save(tr)
                End If
                Dim dal As New ClaimloadFileProcessedDAL
                dal.UpdateFamily(Me.Dataset, tr)
                DBHelper.Commit(tr)
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

    Public Overrides Sub Save() Implements IFileLoadHeaderWork.Save
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New ClaimloadFileProcessedDAL
                dal.UpdateFamily(Me.Dataset)
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

    Public Shared Sub ValidateFileName(ByVal fileLength As Integer)
        If fileLength = 0 Then
            Dim errors() As ValidationError = {New ValidationError("DEALERLOADFORM_FORM001", GetType(ClaimFileProcessed), Nothing, Nothing, Nothing)}
            Throw New BOValidationException(errors, GetType(ClaimFileProcessed).FullName)
        End If
    End Sub

    Public ReadOnly Property Children As IEnumerable(Of IFileLoadReconWork) Implements IFileLoadHeaderWork.Children
        Get
            If (_children Is Nothing) Then
                SyncLock (_syncRoot)
                    If (_children Is Nothing) Then
                        _children = New List(Of IFileLoadReconWork)
                        If (Me.FileType = Codes.CLAIM_LOAD_FILE_TYPE__VENDOR_INVOICE) Then
                            Dim invoiceReconWork As InvoiceReconWrk
                            Dim invoiceReconWorkId As Guid
                            ClaimloadFileProcessed.LoadList(Me.Dataset, Me.Id, Me.FileType)
                            For Each row As DataRow In (Me.Dataset.Tables(InvoiceReconWrkDAL.TABLE_NAME).Rows)
                                invoiceReconWorkId = New Guid(CType(row(InvoiceReconWrkDAL.COL_NAME_INVOICE_RECON_WRK_ID), Byte()))
                                invoiceReconWork = New InvoiceReconWrk(invoiceReconWorkId, Me.Dataset)
                                _children.Add(invoiceReconWork)
                            Next
                        Else
                            Dim claimLoadReconWork As ClaimloadReconWrk
                            Dim claimLoadReconWorkId As Guid
                            ClaimloadFileProcessed.LoadList(Me.Dataset, Me.Id, Me.FileType)
                            For Each row As DataRow In (Me.Dataset.Tables(ClaimloadReconWrkDAL.TABLE_NAME).Rows)
                                claimLoadReconWorkId = New Guid(CType(row(ClaimloadReconWrkDAL.COL_NAME_CLAIMLOAD_RECON_WRK_ID), Byte()))
                                claimLoadReconWork = New ClaimloadReconWrk(claimLoadReconWorkId, Me.Dataset)
                                _children.Add(claimLoadReconWork)
                            Next
                        End If
                    End If
                End SyncLock
            End If
            Return _children
        End Get
    End Property
#End Region

#Region "DataView Retrieveing Methods"
    Public Shared Function LoadList(ByVal ds As DataSet, ByVal claimLoadFileProcessedId As Guid, ByVal FileType As String) As DataSet
        Try
            If (ds Is Nothing) Then ds = New DataSet
            If (FileType = Codes.CLAIM_LOAD_FILE_TYPE__VENDOR_INVOICE) Then
                Dim dal As New InvoiceReconWrkDAL

                dal.LoadByClaimLoadFileProcessedId(ds, claimLoadFileProcessedId)
            Else
                Dim dal As New ClaimloadReconWrkDAL

                dal.LoadByClaimLoadFileProcessedId(ds, claimLoadFileProcessedId)
            End If

            Return ds

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function LoadList(ByVal userId As Guid, ByVal countryCode As String, ByVal fileType As String, ByVal fileName As String) As DataView
        Try
            Dim dal As New ClaimloadFileProcessedDAL
            Dim ds As DataSet

            ds = dal.LoadList(userId, countryCode, fileType, fileName)
            Return (ds.Tables(ClaimloadFileProcessedDAL.TABLE_NAME).DefaultView)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try

    End Function
#End Region

#Region "StoreProcedures Control"
    Public Shared Function ValidateFile(ByVal strFileName As String) As Guid
        Try
            Dim oData As New ClaimloadFileProcessedDAL.ClaimLoadFileProcessedData
            oData.filename = strFileName
            oData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_VALIDATE)

            Dim dal As New ClaimloadFileProcessedDAL
            dal.ValidateFile(oData)
            Return oData.interfaceStatus_id
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function ProcessFile(ByVal strFileName As String) As Guid
        Try
            Dim oData As New ClaimloadFileProcessedDAL.ClaimLoadFileProcessedData
            oData.filename = strFileName
            oData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_VALIDATE)

            Dim dal As New ClaimloadFileProcessedDAL
            dal.ProcessFile(oData)
            Return oData.interfaceStatus_id
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Shared Function DeleteFile(ByVal strFileName As String) As Guid
        Try
            Dim oData As New ClaimloadFileProcessedDAL.ClaimLoadFileProcessedData
            oData.filename = strFileName
            oData.interfaceStatus_id = InterfaceStatusWrk.CreateInterfaceStatus(InterfaceStatusWrkDAL.DESC_VALIDATE)

            Dim dal As New ClaimloadFileProcessedDAL
            dal.DeleteFile(oData)
            Return oData.interfaceStatus_id
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
#End Region
End Class


