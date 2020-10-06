Imports System.Text.RegularExpressions
Imports System.Xml

Public Class UpdatePickupException
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_CLAIM_NUMBER As String = "claim_number"
    Public Const DATA_COL_NAME_UPDATED_BY_NAME As String = "updated_by_name"
    Public Const DATA_COL_NAME_URESOLUTION_CLAIM_STATUS_CODE As String = "resolution_claim_status_code"
    Public Const DATA_COL_NAME_STATUS_COMMENTS As String = "status_comments"

    Private Const TABLE_NAME As String = "UpdatePickupException"
    Private Const SCHEMA_TABLE_NAME As String = "EXCEPTION"
    Private Const DATASET_NAME As String = "UpdatePickupExceptionDs"

    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"


#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As UpdatePickupExceptionDs)
        MyBase.New()
        'Me._dsExceptions = ds
        'MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _dealerId As Guid = Guid.Empty
    Private _serviceNetworkID As Guid = Guid.Empty
    Private _dsExceptions As DataSet

    Private Sub MapDataSet(ByVal ds As UpdatePickupExceptionDs)

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

    Private Sub Load(ByVal ds As UpdatePickupExceptionDs)
        Try
            _dsExceptions = New DataSet
            _dsExceptions.DataSetName = DATASET_NAME
            _dsExceptions.Tables.Add(ds.Tables(SCHEMA_TABLE_NAME).Copy)
            _dsExceptions.Tables(SCHEMA_TABLE_NAME).TableName = TABLE_NAME
            ds.Dispose()
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("UpdatePickupException Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    'Private Sub PopulateBOFromWebService(ByVal ds As UpdatePickupExceptionDs)
    '    Try
    '        If ds.UpdatePickupException.Count = 0 Then Exit Sub
    '        With ds.UpdatePickupException.Item(0)
    '            'no BO
    '        End With
    '    Catch ex As Exception
    '        Throw New ElitaPlusException("UpdatePickupException Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
    '    End Try
    'End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    Public Property ClaimNumber() As String
        Get
            If Row(DATA_COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_CLAIM_NUMBER), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property

    Public Property UpdatedByName() As String
        Get
            If Row(DATA_COL_NAME_UPDATED_BY_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_UPDATED_BY_NAME), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_UPDATED_BY_NAME, Value)
        End Set
    End Property

    Public Property ResolutionClaimStatusCode() As String
        Get
            If Row(DATA_COL_NAME_URESOLUTION_CLAIM_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_URESOLUTION_CLAIM_STATUS_CODE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_URESOLUTION_CLAIM_STATUS_CODE, Value)
        End Set
    End Property

    Public Property StatusComments() As String
        Get
            If Row(DATA_COL_NAME_STATUS_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_STATUS_COMMENTS), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_STATUS_COMMENTS, Value)
        End Set
    End Property


    Public ReadOnly Property ExceptionDataSet() As DataSet
        Get
            Return _dsExceptions
        End Get
    End Property
#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try

            Dim xmlDoc As XmlDataDocument

            'If Not Me.ExceptionDataSet Is Nothing AndAlso Me.ExceptionDataSet.Tables.Count > 0 AndAlso Me.ExceptionDataSet.Tables(0).Rows.Count > 0 Then
            '    xmlDoc = New XmlDataDocument(Me.ExceptionDataSet)
            'End If


            PickupListDetail.UpdatePickupExceptions(ExceptionDataSet)

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
