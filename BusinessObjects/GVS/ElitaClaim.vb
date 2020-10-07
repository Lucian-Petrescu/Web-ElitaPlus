Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.OleDb
Imports System.Xml.Linq

Public Class ElitaClaim
    Inherits BusinessObjectBase

#Region "Constants"
    Private Const TABLE_NAME_TRANSACTION_DATA_RECORD As String = "TRANSACTION_DATA_RECORD"
    Public Const DATA_COL_NAME_TRANSACTION_ID As String = "TRANSACTION_ID"
    Public Const DATA_COL_NAME_FUNCTION_TYPE_CODE As String = "FUNCTION_TYPE_CODE"
    Public Const DATA_COL_NAME_ITEM_NUMBER As String = "ITEM_NUMBER"
    Public Const DATA_COL_NAME_RESULT As String = "RESULT"
    Public Const DATA_COL_NAME_ERROR As String = "ERROR"
    Public Const DATA_COL_NAME_CODE As String = "CODE"
    Public Const DATA_COL_NAME_MESSAGE As String = "MESSAGE"
    Public Const DATA_COL_NAME_ERROR_INFO As String = "ERROR_INFO"
    Private Const TABLE_NAME As String = "ElitaClaim"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"
    Private Const XML_VERSION_AND_ENCODING As String = "<?xml version='1.0' encoding='utf-8' ?>"
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As ElitaClaimDs, ByVal xml As String)
        MyBase.New()

        'MapDataSet(ds)
        'Load(ds)

        'dsMyElitaClaim = ds
        myXml = xml
    End Sub

#End Region

#Region "Private Members"

    Dim dsMyElitaClaim As ElitaClaimDs
    Dim _transactionId As String
    Dim _functionTypeCode As String
    Dim _functionTypeId As Guid = Guid.Empty
    Dim myXml As String

    Private Sub MapDataSet(ByVal ds As ElitaClaimDs)

        Dim schema As String = ds.GetXmlSchema

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

    Private Sub Load(ByVal ds As ElitaClaimDs)
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
            Throw New ElitaPlusException("ElitaClaim Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As ElitaClaimDs)
        Try
            If ds.TRANSACTION_HEADER.Count = 0 Or ds.TRANSACTION_DATA_RECORD.Count = 0 Then Exit Sub
            With ds.TRANSACTION_HEADER.Item(0)
                TransactionId = .TRANSACTION_ID
                FunctionTypeCode = .FUNCTION_TYPE_CODE
            End With

            If Not (FunctionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_INSERT_NEW_CLAIM Or _
                    FunctionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_UPDATE_CLAIM Or _
                    FunctionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_GVS_NEW_CLAIM Or _
                    FunctionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_GVS_UPDATE_CLAIM) Then
                Throw New BOValidationException("ElitaCancelSvcIntegration Error: ", Common.ErrorCodes.ERR_FUNCTION_TYPE_CODE)
            Else
                FunctionTypeId = LookupListNew.GetIdFromCode(LookupListNew.GetGVSFunctionTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), FunctionTypeCode)

                If FunctionTypeId.Equals(Guid.Empty) Then
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

    Public Property TransactionId As String
        Get
            Return _transactionId
        End Get
        Set
            _transactionId = Value
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
            ' *** another way to split the xml into single TRANSACTION_DATA_RECORD
            'Dim _xdoc As XDocument = XDocument.Parse(myXml)
            'Dim _tempDoc As New XDocument
            'Dim x, z As XElement
            'Dim dump, dump1 As String
            '_tempDoc = XDocument.Parse(_xdoc.ToString)
            '_tempDoc.Element("ElitaClaimDs").Elements("ElitaClaim").Elements("TRANSACTION_DATA_RECORD").Remove()
            'z = _tempDoc.Element("ElitaClaimDs").Elements("ElitaClaim").Single
            'For Each y As XElement In _xdoc.Element("ElitaClaimDs").Elements("ElitaClaim").Elements("TRANSACTION_DATA_RECORD")
            '    z.Add(y)
            '    dump = _tempDoc.ToString
            '    ' add the busniess logic here

            '    _tempDoc.Element("ElitaClaimDs").Elements("ElitaClaim").Elements("TRANSACTION_DATA_RECORD").Remove()
            'Next
            Dim count As Integer = 0
            Dim transFamilyBO As TransactionLogHeader = Nothing
            Dim xmlRec As String
            Dim doc = XElement.Parse(myXml)
            Dim tranHeader As XElement =  doc...<TRANSACTION_HEADER>(0)

            For Each header As XElement In tranHeader.Elements
                If header.Name = "TRANSACTION_ID" then
                    TransactionId = header.Value
                Else IF header.Name = "FUNCTION_TYPE_CODE" then
                    FunctionTypeCode = header.Value
                End If
            Next

            If TransactionLogHeader.IsTransactionExist(TransactionId) Then
                Throw New BOValidationException("ElitaCancelSvcIntegration Error: ", Common.ErrorCodes.ERR_TRANSACTION_ALREADY_EXIST)
            End If

            If FunctionTypeCode Is Nothing Or _
                Not (FunctionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_INSERT_NEW_CLAIM Or _
                    FunctionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_UPDATE_CLAIM Or _
                    FunctionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_GVS_NEW_CLAIM Or _
                    FunctionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_GVS_UPDATE_CLAIM) Then
                Throw New BOValidationException("ElitaCancelSvcIntegration Error: ", Common.ErrorCodes.ERR_FUNCTION_TYPE_CODE)
            Else
                FunctionTypeId = LookupListNew.GetIdFromCode(LookupListNew.GetGVSFunctionTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), FunctionTypeCode)

                If FunctionTypeId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("ElitaCancelSvcIntegration Error: ", Common.ErrorCodes.ERR_FUNCTION_TYPE_CODE)
                End If
            End If


            Dim tranDataRec As XElement = <TO_BE_REPLACE>
                                  <%= From dataRec In doc...<ElitaClaim> _
                                      Select dataRec.Nodes  %>
                              </TO_BE_REPLACE>

            For Each rec As XElement In tranDataRec.Elements
                if rec.Name <> "TRANSACTION_HEADER" then
                    xmlRec = XML_VERSION_AND_ENCODING & "<ElitaClaim>" & tranHeader.ToString & rec.ToString & "</ElitaClaim>"
                    
                    If transFamilyBO Is Nothing Then
                        transFamilyBO = New TransactionLogHeader
                        transFamilyBO.TransactionStatusID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSTransactionStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), DALObjects.TransactionLogHeaderDAL.TRANSACTION_STATUS_NEW)
                        transFamilyBO.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                        transFamilyBO.FunctionTypeID = FunctionTypeId
                        transFamilyBO.TransactionXml = xmlRec
                        transFamilyBO.GVSoriginalTransNo = TransactionId
                    Else
                        Dim logHeader As TransactionLogHeader = transFamilyBO.AddTransactionLogHeader(Guid.Empty)
                        logHeader.TransactionStatusID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSTransactionStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), DALObjects.TransactionLogHeaderDAL.TRANSACTION_STATUS_NEW)
                        logHeader.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                        logHeader.FunctionTypeID = FunctionTypeId
                        logHeader.TransactionXml = xmlRec
                        logHeader.GVSoriginalTransNo = TransactionId
                    End If
                    
                    count = count + 1
                    
                End If
            Next
            If count > 0 Then
                transFamilyBO.Save()
            End If

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
