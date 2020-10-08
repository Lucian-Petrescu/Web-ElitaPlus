﻿Imports System.Text.RegularExpressions

Public Class GetMarkupAndCommission
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_CERTIFICATE_NUMBER As String = "CertificateNumber"
    Public Const DATA_COL_NAME_CUSTOMER_NAME As String = "CustomerName"
    Public Const DATA_COL_NAME_BEGIN_DATE As String = "BeginDate"
    Public Const DATA_COL_NAME_END_DATE As String = "EndDate"
    Public Const DATA_COL_NAME_REQUEST_NUMBER As String = "RequestNumber"

    Public Const DATA_COL_NAME_DEALER As String = "dealerCode"

    Private _recordsToRreturn As Integer = 100
    Private Const TABLE_NAME As String = "GetMarkupAndCommission"
    Private Const DATASET_NAME As String = "GetMarkupAndCommission"
    Private Const DATASET_TABLE_NAME As String = "Certificate"

#End Region

#Region "Constructors"

    Public Sub New(ds As GetMarkupAndCommissionDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _dealerId As Guid = Guid.Empty

    Private Sub MapDataSet(ds As GetMarkupAndCommissionDs)

        Dim schema As String = ds.GetXmlSchema '.Replace(SOURCE_COL_MAKE, DATA_COL_NAME_MANUFACTURER).Replace(SOURCE_COL_MILEAGE, DATA_COL_NAME_ODOMETER).Replace(SOURCE_COL_NEWUSED, DATA_COL_NAME_CONDITION)

        Dim t As Integer
        Dim i As Integer

        For t = 0 To ds.Tables.Count - 1
            For i = 0 To ds.Tables(t).Columns.Count - 1
                ds.Tables(t).Columns(i).ColumnName = ds.Tables(t).Columns(i).ColumnName.ToUpper
            Next
        Next

        Dataset = New DataSet
        Dataset.ReadXmlSchema(XMLHelper.GetXMLStream(schema))

    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    Private Sub Load(ds As GetMarkupAndCommissionDs)
        Try
            Initialize()
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            Dataset.Tables(TABLE_NAME).Rows.Add(newRow)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetMarkupAndCommission Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ds As GetMarkupAndCommissionDs)
        Try
            If ds.GetMarkupAndCommission.Count = 0 Then Exit Sub
            With ds.GetMarkupAndCommission.Item(0)

                If Not .IsDealerCodeNull Then DealerCode = .DealerCode
                If Not .IsCertificateNumberNull Then CertificateNumber = .CertificateNumber
                If Not .IsBeginDateNull Then
                    BeginDate = CType(.BeginDate, DateTime)
                Else
                    BeginDate = Nothing
                End If

                If Not .IsEndDateNull Then
                    EndDate = CType(.EndDate, DateTime)                    
                Else
                    EndDate = Nothing
                End If


                If Not .IsRequestNumberNull Then
                    RequestNumber = .RequestNumber
                Else
                    RequestNumber = 0 'default
                End If


            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetMarkupAndCommission Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    Public Property DealerCode As String
        Get
            If Row(DATA_COL_NAME_DEALER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_DEALER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_DEALER, Value)
        End Set
    End Property

    Public Property CertificateNumber As String
        Get
            If Row(DATA_COL_NAME_CERTIFICATE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CERTIFICATE_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CERTIFICATE_NUMBER, Value)
        End Set
    End Property

    Public Property EndDate As DateType
        Get
            CheckDeleted()
            If Row(DATA_COL_NAME_END_DATE) Is DBNull.Value Then
                Return Nothing
            Else                
                Return New DateType(CType(Row(DATA_COL_NAME_END_DATE), DateTime))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_END_DATE, Value)
        End Set
    End Property

    Public Property BeginDate As DateType
        Get
            CheckDeleted()
            If Row(DATA_COL_NAME_BEGIN_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DATA_COL_NAME_BEGIN_DATE), DateTime))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_BEGIN_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property RequestNumber As Integer
        Get
            If Row(DATA_COL_NAME_REQUEST_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_REQUEST_NUMBER), Integer)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_REQUEST_NUMBER, Value)
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Validate()
            Dim cert As New Certificate

            Dim _CertListDataSet As DataSet
            If Not BeginDate Is Nothing AndAlso Not EndDate Is Nothing Then
                _CertListDataSet = cert.GetMarkupAndCommissionList(RequestNumber, DealerId, BeginDate, EndDate, CertificateNumber)
            ElseIf Not BeginDate Is Nothing Then
                _CertListDataSet = cert.GetMarkupAndCommissionList(RequestNumber, DealerId, BeginDate, Nothing, CertificateNumber)
            ElseIf Not EndDate Is Nothing Then
                _CertListDataSet = cert.GetMarkupAndCommissionList(RequestNumber, DealerId, Nothing, EndDate, CertificateNumber)
            Else
                _CertListDataSet = cert.GetMarkupAndCommissionList(RequestNumber, DealerId, Nothing, Nothing, CertificateNumber)
            End If


            _CertListDataSet.DataSetName = DATASET_NAME
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

    Private ReadOnly Property DealerId As Guid
        Get
            If _dealerId.Equals(Guid.Empty) AndAlso Not DealerCode Is Nothing AndAlso Not DealerCode.Equals(String.Empty) Then

                Dim list As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                If list Is Nothing Then
                    Throw New BOValidationException("GetMarkupAndCommission Error: ", Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE)
                End If
                _dealerId = LookupListNew.GetIdFromCode(list, DealerCode)
                If _dealerId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("GetMarkupAndCommission Error: ", Common.ErrorCodes.WS_DEALER_NOT_FOUND)
                End If
                list = Nothing
            End If

            Return _dealerId
        End Get
    End Property


#End Region



End Class
