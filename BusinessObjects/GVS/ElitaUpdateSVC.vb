Imports System.Text.RegularExpressions

Public Class ElitaUpdateSVC
    Inherits BusinessObjectBase

#Region "Constants"
    Private Const TABLE_NAME As String = "ElitaUpdateSVC"
    Private Const TABLE_NAME_TRANSACTION_HEADER As String = "TRANSACTION_HEADER"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"
    Public Const DATA_COL_NAME_TRANSACTION_ID As String = "TRANSACTION_ID"
    Public Const DATA_COL_NAME_FUNCTION_TYPE As String = "FUNCTION_TYPE"
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As ElitaUpdateSVCDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

        dsMyUpdateSVC = ds
    End Sub

#End Region

#Region "Private Members"

    Dim dsMyUpdateSVC As System.Data.DataSet

    Private Sub MapDataSet(ByVal ds As ElitaUpdateSVCDs)

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

    Private Sub Load(ByVal ds As ElitaUpdateSVCDs)
        Try
            Initialize()
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME_TRANSACTION_HEADER).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds)
            Me.Dataset.Tables(TABLE_NAME_TRANSACTION_HEADER).Rows.Add(newRow)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("ElitaUpdateSVC Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As ElitaUpdateSVCDs)
        Try
            If ds.TRANSACTION_HEADER.Count = 0 Or ds.TRANSACTION_DATA_RECORD.Count = 0 Then Exit Sub
            With ds.TRANSACTION_HEADER.Item(0)
                Me.TransactionId = .TRANSACTION_ID
                Me.FunctionType = .FUNCTION_TYPE
            End With

        Catch ex As Exception
            Throw New ElitaPlusException("ElitaUpdateSVC Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Dim logHeader As TransactionLogHeader = New TransactionLogHeader
            logHeader.TransactionStatusID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSTransactionStatusList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), DALObjects.TransactionLogHeaderDAL.TRANSACTION_STATUS_NEW)
            logHeader.CompanyGroupId = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
            logHeader.FunctionTypeID = LookupListNew.GetIdFromCode(LookupListNew.GetGVSFunctionTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId), Me.FunctionType)
            logHeader.TransactionXml = XMLHelper.FromDatasetToXML(dsMyUpdateSVC, Nothing, False, True, "ElitaUpdateSVCDs")
            logHeader.GVSoriginalTransNo = Me.TransactionId
            logHeader.Save()

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

    Public Property TransactionId() As String
        Get
            If Row(Me.DATA_COL_NAME_TRANSACTION_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_TRANSACTION_ID), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_TRANSACTION_ID, Value)
        End Set
    End Property

    Public Property FunctionType() As String
        Get
            If Row(Me.DATA_COL_NAME_FUNCTION_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_FUNCTION_TYPE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_FUNCTION_TYPE, Value)
        End Set
    End Property

#End Region

End Class
