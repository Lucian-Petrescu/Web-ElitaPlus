﻿'************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BusinessObject.cst (1/31/2013)  ********************

Public Class DailyOutboundFileDetail
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
            Dim dal As New DailyOutboundFileDetailDAL
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
            Dim dal As New DailyOutboundFileDetailDAL
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
            If Row(DailyOutboundFileDetailDAL.TABLE_KEY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DailyOutboundFileDetailDAL.COL_NAME_FILE_DETAIL_ID), Byte()))
            End If
        End Get
    End Property

    <ValueMandatory("")> _
    Public Property CompanyId() As Guid
        Get
            CheckDeleted()
            If Row(DailyOutboundFileDetailDAL.COL_NAME_COMPANY_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DailyOutboundFileDetailDAL.COL_NAME_COMPANY_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_COMPANY_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property DealerId() As Guid
        Get
            CheckDeleted()
            If Row(DailyOutboundFileDetailDAL.COL_NAME_DEALER_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DailyOutboundFileDetailDAL.COL_NAME_DEALER_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_DEALER_ID, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CertId() As Guid
        Get
            CheckDeleted()
            If Row(DailyOutboundFileDetailDAL.COL_NAME_CERT_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return New Guid(CType(Row(DailyOutboundFileDetailDAL.COL_NAME_CERT_ID), Byte()))
            End If
        End Get
        Set(ByVal Value As Guid)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_CERT_ID, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=80)> _
    Public Property CertNumber() As String
        Get
            CheckDeleted()
            If Row(DailyOutboundFileDetailDAL.COL_NAME_CERT_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DailyOutboundFileDetailDAL.COL_NAME_CERT_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_CERT_NUMBER, Value)
        End Set
    End Property


    <ValueMandatory("")> _
    Public Property CertCreatedDate() As DateType
        Get
            CheckDeleted()
            If Row(DailyOutboundFileDetailDAL.COL_NAME_CERT_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DailyOutboundFileDetailDAL.COL_NAME_CERT_CREATED_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_CERT_CREATED_DATE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=80)> _
    Public Property RecordType() As String
        Get
            CheckDeleted()
            If Row(DailyOutboundFileDetailDAL.COL_NAME_RECORD_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DailyOutboundFileDetailDAL.COL_NAME_RECORD_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_RECORD_TYPE, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property RecCancel() As String
        Get
            CheckDeleted()
            If Row(DailyOutboundFileDetailDAL.COL_NAME_REC_CANCEL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DailyOutboundFileDetailDAL.COL_NAME_REC_CANCEL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_REC_CANCEL, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property RecNewBusiness() As String
        Get
            CheckDeleted()
            If Row(DailyOutboundFileDetailDAL.COL_NAME_REC_NEW_BUSINESS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DailyOutboundFileDetailDAL.COL_NAME_REC_NEW_BUSINESS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_REC_NEW_BUSINESS, Value)
        End Set
    End Property


    <ValueMandatory(""), ValidStringLength("", Max:=4)> _
    Public Property RecBilling() As String
        Get
            CheckDeleted()
            If Row(DailyOutboundFileDetailDAL.COL_NAME_REC_BILLING) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DailyOutboundFileDetailDAL.COL_NAME_REC_BILLING), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(DailyOutboundFileDetailDAL.COL_NAME_REC_BILLING, Value)
        End Set
    End Property




#End Region

#Region "Public Members"
    Public Overrides Sub Save()
        Try
            MyBase.Save()
            If Me._isDSCreator AndAlso Me.IsDirty AndAlso Me.Row.RowState <> DataRowState.Detached Then
                Dim dal As New DailyOutboundFileDetailDAL
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
    Public Shared Function getviewList() As DailyOutboundFileDetailSearchDV
        Try
            Dim dal As New DailyOutboundFileDetailDAL

            Return New DailyOutboundFileDetailSearchDV(dal.LoadList().Tables(0))
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function
    Public Class DailyOutboundFileDetailSearchDV
        Inherits DataView
#Region "Constants"

        Public Const COL_File_Detail_ID As String = "File_Detail_id"
        Public Const COL_CERT_NUMBER As String = "Cert_Number"
       
        Public Const COL_SELECTION_ON_CREATED_DATE As String = "Created_date"

        Public Const COL_SELECTION_ON_NEW_BUSINESS As String = "Selection_On_New_Enrollment"
        Public Const COL_SELECTION_ON_Cancel As String = "Selection_On_Cancel"
        Public Const COL_SELECTION_ON_BILLING As String = "Selection_On_Billing"

#End Region

        Private _dataTable As DataTable

        Public Sub New(ByVal table As DataTable)
            MyBase.New(table)
        End Sub

        Public Shared ReadOnly Property FileHeaderTempId(ByVal row) As Guid
            Get
                Return New Guid(CType(row(COL_File_Detail_ID), Byte()))
            End Get
        End Property


    End Class

#End Region
#Region "Insert Detail Records"
    Public Shared Function InsertDetailRecord(ByVal Company_id As Guid, ByVal Dealer_id As Guid, ByVal cert_id As Guid, ByVal certcreatedDate As Date, ByVal CertNumber As String, _
                                           ByVal selectonNewEnrollment As String, ByVal selectoncancel As String, ByVal selectonbilling As String, _
                                            ByVal recordType As String, ByVal createdDate As Date, ByVal createdBy As String, ByVal billing_detail_id As Guid, Optional ByVal selectioncertificate As String = "")
        Try
            Dim dal As New DailyOutboundFileDetailDAL
            dal.insertdetailrecords(Company_id, Dealer_id, cert_id, CertNumber, certcreatedDate, selectonNewEnrollment, selectoncancel, selectonbilling, recordType, createdDate, createdBy, billing_detail_id) ' fromdate, todate, callfrom, processeddate, selectioncertificate)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

#End Region

    Public Shared Sub getDetailviewList(ByVal CompanyCode As String, ByVal Dealercode As String, ByVal CertNumber As String, _
                                           ByVal selectonNewEnrollment As String, ByVal selectoncancel As String, ByVal selectonbilling As String, _
                                            ByVal fromdate As Date, ByVal todate As Date, ByVal callfrom As String, _
                                             Optional ByVal processeddate As Date = Nothing, _
                                            Optional ByVal selectioncertificate As String = "")
        Try
            Dim dal As New DailyOutboundFileDetailDAL

            dal.getrecordsviewlist(CompanyCode, Dealercode, CertNumber, selectonNewEnrollment, selectoncancel, selectonbilling, fromdate, todate, callfrom, processeddate, selectioncertificate)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Sub
    Public Shared Function DeleteDetailRecord(ByVal file_detail_Id As Guid)
        Try
            Dim dal As New DailyOutboundFileDetailDAL
            dal.deletedetailrecord(file_detail_Id)

        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw New DataBaseAccessException(ex.ErrorType, ex)
        End Try
    End Function

    Public Class DailyOutboundFileDetailView
        Inherits DataView
    End Class
End Class





