﻿Imports System.Text.RegularExpressions

Public Class ElitaTransactionUpdate
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_TRANSACTION_ID As String = "transaction_log_header_id"
    Public Const DATA_COL_NAME_FUNCTION_TYPE As String = "function_type_code"
    Public Const DATA_COL_NAME_ITEM_NUMBER As String = "ITEM_NUMBER"
    Public Const DATA_COL_NAME_RESULT As String = "RESULT"
    Public Const DATA_COL_NAME_ERROR As String = "ERROR"
    Public Const DATA_COL_NAME_CODE As String = "CODE"
    Public Const DATA_COL_NAME_MESSAGE As String = "MESSAGE"
    Public Const DATA_COL_NAME_ERROR_INFO As String = "ERROR_INFO"
    Private Const TABLE_NAME As String = "ElitaTransactionUpdate"

    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As ElitaTransactionUpdateDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

        dsMyTransactionUpdate = ds
    End Sub

#End Region

#Region "Private Members"

    Dim dsMyTransactionUpdate As System.Data.DataSet
    Dim _originalTransLogHdrID As Guid
    Dim _GVSoriginalTransNo As String
    Dim _functionTypeCode As String
    Dim _functionTypeId As Guid = Guid.Empty

    Private Sub MapDataSet(ByVal ds As ElitaTransactionUpdateDs)

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

    Private Sub Load(ByVal ds As ElitaTransactionUpdateDs)
        Try
            Initialize()
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            Dataset.Tables(TABLE_NAME).Rows.Add(newRow)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("ElitaTransactionUpdate Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As ElitaTransactionUpdateDs)
        Dim OriginalTransLogHdrIDExist As Boolean = True

        Try
            If ds.TRANSACTION_HEADER.Count = 0 Or ds.TRANSACTION_DATA_RECORD.Count = 0 Then Exit Sub
            With ds.TRANSACTION_HEADER.Item(0)
                OriginalTransLogHdrID = New Guid(GuidControl.HexToByteArray(.ELITA_ORGINAL_TRANS_ID))
                GVSoriginalTransNo = .TRANSACTION_ID
                FunctionTypeCode = .FUNCTION_TYPE
            End With

            If Not (FunctionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_TRANSACTION_UPDATE) Then
                Throw New BOValidationException("ElitaTransactionUpdate Error: ", Common.ErrorCodes.ERR_FUNCTION_TYPE_CODE)
            Else
                FunctionTypeId = LookupListNew.GetIdFromCode(LookupListNew.GetGVSFunctionTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), FunctionTypeCode)

                If FunctionTypeId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("ElitaTransactionUpdate Error: ", Common.ErrorCodes.ERR_FUNCTION_TYPE_CODE)
                End If
            End If

            OriginalTransLogHdrIDExist = False
            Dim logHeader As TransactionLogHeader = New TransactionLogHeader(OriginalTransLogHdrID)
            OriginalTransLogHdrIDExist = True

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            If OriginalTransLogHdrIDExist = False Then
                Throw New ElitaPlusException("ElitaTransactionUpdate Invalid Parameters Error", Common.ErrorCodes.ERR_ELITA_ORGINAL_TRANS_ID, ex)
            Else
                Throw New ElitaPlusException("ElitaTransactionUpdate Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
            End If
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    Public Property OriginalTransLogHdrID As Guid
        Get
            Return _originalTransLogHdrID
        End Get
        Set
            _originalTransLogHdrID = Value
        End Set
    End Property

    Public Property GVSoriginalTransNo As String
        Get
            Return _GVSoriginalTransNo
        End Get
        Set
            _GVSoriginalTransNo = Value
        End Set
    End Property

    Public Property FunctionTypeCode As String
        Get
            Return _functionTypeCode
        End Get
        Set
            _functionTypeCode = Value
        End Set
    End Property

    Public Property FunctionTypeId As Guid
        Get
            Return _functionTypeId
        End Get
        Set
            _functionTypeId = Value
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Dim logHeader As TransactionLogHeader = New TransactionLogHeader
            logHeader.OriginalTransLogHdrID = OriginalTransLogHdrID
            logHeader.GVSoriginalTransNo = GVSoriginalTransNo
            logHeader.TransactionStatusID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSTransactionStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), DALObjects.TransactionLogHeaderDAL.TRANSACTION_STATUS_NEW)
            logHeader.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            logHeader.FunctionTypeID = FunctionTypeId
            logHeader.TransactionXml = XMLHelper.FromDatasetToXML(dsMyTransactionUpdate, Nothing, True).Replace("<ElitaTransactionUpdateDs>", "").Replace("</ElitaTransactionUpdateDs>", "")
            logHeader.Save()

            ' Set the acknoledge OK response
            Return XMLHelper.GetXML_OK_Response

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

#End Region

End Class
