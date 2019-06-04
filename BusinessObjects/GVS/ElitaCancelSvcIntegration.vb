Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.OleDb

Public Class ElitaCancelSvcIntegration
    Inherits BusinessObjectBase

#Region "Constants"
    Private Const TABLE_NAME_TRANSACTION_DATA_RECORD As String = "TRANSACTION_DATA_RECORD"
    Public Const DATA_COL_NAME_TRANSACTION_ID As String = "TRANSACTION_ID"
    Public Const DATA_COL_NAME_ITEM_NUMBER As String = "ITEM_NUMBER"
    Public Const DATA_COL_NAME_RESULT As String = "RESULT"
    Public Const DATA_COL_NAME_ERROR As String = "ERROR"
    Public Const DATA_COL_NAME_CODE As String = "CODE"
    Public Const DATA_COL_NAME_MESSAGE As String = "MESSAGE"
    Public Const DATA_COL_NAME_ERROR_INFO As String = "ERROR_INFO"
    Private Const TABLE_NAME As String = "ElitaCancelSvcIntegration"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As ElitaCancelSvcIntegrationDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

        dsMyElitaCancelSvcIntegration = ds
    End Sub

#End Region

#Region "Private Members"

    Dim dsMyElitaCancelSvcIntegration As ElitaCancelSvcIntegrationDs
    Dim _transactionId As String
    Dim _functionTypeCode As String
    Dim _functionTypeId As Guid = Guid.Empty

    Private Sub MapDataSet(ByVal ds As ElitaCancelSvcIntegrationDs)

        Dim schema As String = ds.GetXmlSchema

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

    Private Sub Load(ByVal ds As ElitaCancelSvcIntegrationDs)
        Try
            Initialize()
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds)
            Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("ElitaCancelSvcIntegration Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As ElitaCancelSvcIntegrationDs)
        Try
            If ds.TRANSACTION_HEADER.Count = 0 Or ds.TRANSACTION_DATA_RECORD.Count = 0 Then Exit Sub
            With ds.TRANSACTION_HEADER.Item(0)
                Me.TransactionId = .TRANSACTION_ID
                Me.FunctionTypeCode = .FUNCTION_TYPE_CODE
            End With

            If Not (Me.FunctionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_CANCEL_SVC_INTEGRATION) Then
                Throw New BOValidationException("ElitaCancelSvcIntegration Error: ", Common.ErrorCodes.ERR_FUNCTION_TYPE_CODE)
            Else
                Me.FunctionTypeId = LookupListNew.GetIdFromCode(LookupListNew.GetGVSFunctionTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), Me.FunctionTypeCode)

                If Me.FunctionTypeId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("ElitaCancelSvcIntegration Error: ", Common.ErrorCodes.ERR_FUNCTION_TYPE_CODE)
                End If
            End If

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    Public Property TransactionId() As String
        Get
            Return _transactionId
        End Get
        Set(ByVal Value As String)
            _transactionId = Value
        End Set
    End Property

    Public Property FunctionTypeCode() As String
        Get
            Return _functionTypeCode
        End Get
        Set(ByVal Value As String)
            _functionTypeCode = Value
        End Set
    End Property

    Public Property FunctionTypeId() As Guid
        Get
            Return _functionTypeId
        End Get
        Set(ByVal Value As Guid)
            _functionTypeId = Value
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try

            Dim logHeader As TransactionLogHeader = New TransactionLogHeader
            logHeader.TransactionStatusID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSTransactionStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), DALObjects.TransactionLogHeaderDAL.TRANSACTION_STATUS_NEW)
            logHeader.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            logHeader.FunctionTypeID = Me.FunctionTypeId
            logHeader.TransactionXml = XMLHelper.FromDatasetToXML(dsMyElitaCancelSvcIntegration, Nothing, False).Replace("<ElitaCancelSvcIntegrationDs>", "").Replace("</ElitaCancelSvcIntegrationDs>", "")
            logHeader.GVSoriginalTransNo = Me.TransactionId

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
