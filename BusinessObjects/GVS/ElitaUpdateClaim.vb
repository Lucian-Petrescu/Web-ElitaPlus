Imports System.Text.RegularExpressions
Imports System.Xml
Imports System.IO
Imports System.Text
Imports System.Data
Imports System.Data.OleDb

Public Class ElitaUpdateClaim
    Inherits BusinessObjectBase

#Region "Constants"
    Private Const TABLE_NAME_TRANSACTION_DATA_RECORD As String = "TRANSACTION_DATA_RECORD"
    Public Const DATA_COL_NAME_TRANSACTION_ID As String = "TRANSACTION_ID"
    Public Const DATA_COL_NAME_FUNCTION_TYPE As String = "FUNCTION_TYPE"
    Public Const DATA_COL_NAME_ITEM_NUMBER As String = "ITEM_NUMBER"
    Public Const DATA_COL_NAME_RESULT As String = "RESULT"
    Public Const DATA_COL_NAME_ERROR As String = "ERROR"
    Public Const DATA_COL_NAME_CODE As String = "CODE"
    Public Const DATA_COL_NAME_MESSAGE As String = "MESSAGE"
    Public Const DATA_COL_NAME_ERROR_INFO As String = "ERROR_INFO"
    Private Const TABLE_NAME As String = "ElitaUpdateClaim"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As ElitaUpdateClaimDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

        dsMyUpdateClaim = ds
    End Sub

#End Region

#Region "Private Members"

    Dim dsMyUpdateClaim As ElitaUpdateClaimDs
    Dim _transactionId As String
    Dim _functionTypeCode As String

    Private Sub MapDataSet(ByVal ds As ElitaUpdateClaimDs)

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

    Private Sub Load(ByVal ds As ElitaUpdateClaimDs)
        Try
            Initialize()
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds)
            Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("ElitaUpdateClaim Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As ElitaUpdateClaimDs)
        Try
            If ds.TRANSACTION_HEADER.Count = 0 Or ds.TRANSACTION_DATA_RECORD.Count = 0 Then Exit Sub
            With ds.TRANSACTION_HEADER.Item(0)
                Me.TransactionId = .TRANSACTION_ID
                Me.FunctionTypeCode = .FUNCTION_TYPE
            End With
        Catch ex As Exception
            Throw New ElitaPlusException("ElitaUpdateClaim Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
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

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Dim count As Integer = 0
            Dim transFamilyBO As TransactionLogHeader = Nothing

            ' Split the xml and insert into the transaction log header table as a single record
            While count < dsMyUpdateClaim.TRANSACTION_DATA_RECORD.Count
                Dim dsNewUpdateClaim As DataSet = New DataSet()
                dsNewUpdateClaim = dsMyUpdateClaim.Clone()

                dsNewUpdateClaim.Tables(dsMyUpdateClaim.ElitaUpdateClaim.TableName).ImportRow(dsMyUpdateClaim.ElitaUpdateClaim.Rows(0))
                dsNewUpdateClaim.Tables(dsMyUpdateClaim.TRANSACTION_HEADER.TableName).ImportRow(dsMyUpdateClaim.TRANSACTION_HEADER.Rows(0))
                dsNewUpdateClaim.Tables(dsMyUpdateClaim.TRANSACTION_DATA_RECORD.TableName).ImportRow(dsMyUpdateClaim.TRANSACTION_DATA_RECORD.Rows(count))

                If dsMyUpdateClaim.PARTS_LIST.Select("TRANSACTION_DATA_RECORD_ID =" & dsMyUpdateClaim.TRANSACTION_DATA_RECORD.Rows(count)("TRANSACTION_DATA_RECORD_ID")).Length Then
                    dsNewUpdateClaim.Tables(dsMyUpdateClaim.PARTS_LIST.TableName).ImportRow(dsMyUpdateClaim.PARTS_LIST.Rows(count))
                End If

                If dsMyUpdateClaim.FOLLOWUP.Select("TRANSACTION_DATA_RECORD_ID =" & dsMyUpdateClaim.TRANSACTION_DATA_RECORD.Rows(count)("TRANSACTION_DATA_RECORD_ID")).Length Then
                    dsNewUpdateClaim.Tables(dsMyUpdateClaim.FOLLOWUP.TableName).ImportRow(dsMyUpdateClaim.FOLLOWUP.Rows(count))
                End If

                If transFamilyBO Is Nothing Then
                    transFamilyBO = New TransactionLogHeader
                    transFamilyBO.TransactionStatusID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSTransactionStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), DALObjects.TransactionLogHeaderDAL.TRANSACTION_STATUS_NEW)
                    transFamilyBO.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                    transFamilyBO.FunctionTypeID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSFunctionTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_UPDATE_CLAIM)
                    transFamilyBO.TransactionXml = XMLHelper.FromDatasetToXML(dsNewUpdateClaim, Nothing, False).Replace("<ElitaUpdateClaimDs>", "").Replace("</ElitaUpdateClaimDs>", "")
                    transFamilyBO.GVSoriginalTransNo = Me.TransactionId
                Else
                    Dim logHeader As TransactionLogHeader = transFamilyBO.AddTransactionLogHeader(Guid.Empty)
                    logHeader.TransactionStatusID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSTransactionStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), DALObjects.TransactionLogHeaderDAL.TRANSACTION_STATUS_NEW)
                    logHeader.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                    logHeader.FunctionTypeID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSFunctionTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_UPDATE_CLAIM)
                    logHeader.TransactionXml = XMLHelper.FromDatasetToXML(dsNewUpdateClaim, Nothing, False).Replace("<ElitaUpdateClaimDs>", "").Replace("</ElitaUpdateClaimDs>", "")
                    logHeader.GVSoriginalTransNo = Me.TransactionId
                End If

                count = count + 1
            End While

            If count > 0 Then
                transFamilyBO.Save()
            End If

            ' Set the acknoledge OK response
            Return XMLHelper.GetXML_OK_Response

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
