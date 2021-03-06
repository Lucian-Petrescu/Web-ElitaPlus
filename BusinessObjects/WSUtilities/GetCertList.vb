﻿Imports System.Text.RegularExpressions

Public Class GetCertList
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_BRANCH_CODE As String = "BranchCode"
    Public Const DATA_COL_NAME_CERTIFICATE_NUMBER As String = "CertificateNumber"
    Public Const DATA_COL_NAME_CUSTOMER_NAME As String = "CustomerName"
    Public Const DATA_COL_NAME_EMAIL As String = "Email"
    Public Const DATA_COL_NAME_SORT_BY As String = "SortBy"
    Public Const DATA_COL_NAME_SORT_ORDER As String = "SortOrder"
    Public Const DATA_COL_NAME_REQUEST_NUMBER As String = "RequestNumber"

    Public Const DATA_COL_NAME_FOR_CANCELLATION As String = "ForCancellation"

    Public Const DATA_COL_NAME_DEALER As String = "dealerCode"
    
    Private _recordsToRreturn As Integer = 100
    Private Const TABLE_NAME As String = "GetCertList"
    Private Const DATASET_NAME As String = "GetCertList"
    Private Const DATASET_TABLE_NAME As String = "Certificate"

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetCertListDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _dealerId As Guid = Guid.Empty

    Private Sub MapDataSet(ByVal ds As GetCertListDs)

        Dim schema As String = ds.GetXmlSchema '.Replace(SOURCE_COL_MAKE, DATA_COL_NAME_MANUFACTURER).Replace(SOURCE_COL_MILEAGE, DATA_COL_NAME_ODOMETER).Replace(SOURCE_COL_NEWUSED, DATA_COL_NAME_CONDITION)

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Me.Dataset = New DataSet
        Me.Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ByVal ds As GetCertListDs)
        Try
            Initialize()
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds)
            Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetCertList Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetCertListDs)
        Try
            If ds.GetCertList.Count = 0 Then Exit Sub
            With ds.GetCertList.Item(0)
                Me.DealerCode = .DealerCode
                If Not .IsBranchCodeNull Then Me.BranchCode = .BranchCode
                Me.SortBy = .SortBy
                Me.SortOrder = .SortOrder
                If Not .IsCertificateNumberNull Then Me.CertificateNumber = .CertificateNumber
                If Not .IsCustomerNameNull Then Me.CustomerName = .CustomerName
                If Not .IsEmailNull Then Me.Email = .Email
                If Not .IsForCancellationNull Then
                    Me.ForCancellation = .ForCancellation
                Else
                    Me.ForCancellation = "N" 'default
                End If

                If Not .IsRequestNumberNull Then
                    Me.RequestNumber = .RequestNumber
                Else
                    Me.RequestNumber = 0 'default
                End If


            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetCertList Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"





    <ValueMandatory("")> _
    Public Property DealerCode() As String
        Get
            If Row(Me.DATA_COL_NAME_DEALER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_DEALER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_DEALER, Value)
        End Set
    End Property

    Public Property CertificateNumber() As String
        Get
            If Row(Me.DATA_COL_NAME_CERTIFICATE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_CERTIFICATE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_CERTIFICATE_NUMBER, Value)
        End Set
    End Property

    Public Property CustomerName() As String
        Get
            If Row(Me.DATA_COL_NAME_CUSTOMER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_CUSTOMER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_CUSTOMER_NAME, Value)
        End Set
    End Property

    Public Property BranchCode() As String
        Get
            If Row(Me.DATA_COL_NAME_BRANCH_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_BRANCH_CODE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_BRANCH_CODE, Value)
        End Set
    End Property

    Public Property Email() As String
        Get
            If Row(Me.DATA_COL_NAME_EMAIL) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_EMAIL), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_EMAIL, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ForCancellation() As String
        Get
            If Row(Me.DATA_COL_NAME_FOR_CANCELLATION) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_FOR_CANCELLATION), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_FOR_CANCELLATION, Value)
        End Set
    End Property
    <ValueMandatory("")> _
    Public Property SortBy() As Integer
        Get
            If Row(Me.DATA_COL_NAME_SORT_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_SORT_BY), Integer)
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_SORT_BY, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property SortOrder() As Integer
        Get
            If Row(Me.DATA_COL_NAME_SORT_ORDER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_SORT_ORDER), Integer)
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_SORT_ORDER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property RequestNumber() As Integer
        Get
            If Row(Me.DATA_COL_NAME_REQUEST_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_REQUEST_NUMBER), Integer)
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_REQUEST_NUMBER, Value)
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Me.Validate()
            Dim cert As New Certificate
            Dim _CertListDataSet As DataSet = cert.GetCertificatesForCancellationList(Me.RequestNumber, Me.DealerId, Me.SortBy, SortOrder, Me.ForCancellation, _
                                                                             Me.BranchCode, Me.CertificateNumber, Me.CustomerName, Me.Email)
            _CertListDataSet.DataSetName = Me.DATASET_NAME
            _CertListDataSet.Tables(CertificateDAL.TABLE_NAME).TableName = Me.DATASET_TABLE_NAME
            'Return (XMLHelper.FromDatasetToXML_Std(_CertListDataSet))
            Return (XMLHelper.FromDatasetToXML(_CertListDataSet, Nothing, True, True, True, False, True))

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

#End Region

#Region "Extended Properties"

    Private ReadOnly Property DealerId() As Guid
        Get
            If Me._dealerId.Equals(Guid.Empty) Then

                Dim list As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                If list Is Nothing Then
                    Throw New BOValidationException("GetCertList Error: ", Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE)
                End If
                Me._dealerId = LookupListNew.GetIdFromCode(list, Me.DealerCode)
                If _dealerId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("GetCertList Error: ", Common.ErrorCodes.WS_DEALER_NOT_FOUND)
                End If
                list = Nothing
            End If

            Return Me._dealerId
        End Get
    End Property


#End Region

End Class
