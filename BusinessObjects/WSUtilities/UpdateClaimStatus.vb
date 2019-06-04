Public Class UpdateClaimStatus
    Inherits BusinessObjectBase

#Region "Member Variables"


#End Region

#Region "Constants"

    Public TABLE_NAME As String = "ClaimStatus"
    Private Const TABLE_RESULT As String = "RESULT"
    Private Const VALUE_OK As String = "OK"

    Private Const SOURCE_COL_COMMENTS As String = "COMMENTS"
    Private Const SOURCE_COL_STATUS_CODE As String = "STATUS_CODE"
    Private Const SOURCE_COL_CLAIM_NUMBER As String = "CLAIM_NUMBER"
    Private Const SOURCE_COL_DEALER_CODE As String = "DEALER_CODE"
    Private Const SOURCE_COL_CERTIFICATE_NUMBER As String = "CERTIFICATE_NUMBER"

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As UpdateClaimStatusDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)
    End Sub

#End Region

#Region "Member Methods"

    Private Sub PopulateBOFromWebService(ByVal ds As UpdateClaimStatusDs)
        Try

            With ds.UpdateClaimStatus.Item(0)
                Me.CertificateNumber = .CERTIFICATE_NUMBER
                Me.DealerCode = .DEALER_CODE
                Me.Comments = .COMMENTS
                Me.ClaimNumber = .CLAIM_NUMBER
                Me.StatusCode = .STATUS_CODE
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub MapDataSet(ByVal ds As UpdateClaimStatusDs)

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

    Private Sub Load(ByVal ds As UpdateClaimStatusDs)
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
            Throw New ElitaPlusException("Claim Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    'Shadow the checkdeleted method so we don't validate like the DB objects
    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region


#Region "Properties"

    Public Property CertificateNumber() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CERTIFICATE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CERTIFICATE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_CERTIFICATE_NUMBER, Value)
        End Set
    End Property

    Public Property DealerCode() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_DEALER_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_DEALER_CODE, Value)
        End Set
    End Property

    Public Property ClaimNumber() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_CLAIM_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_CLAIM_NUMBER, Value)
        End Set
    End Property

    Public Property StatusCode() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_STATUS_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_STATUS_CODE, Value)
        End Set
    End Property

    Public Property Comments() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_COMMENTS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_COMMENTS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_COMMENTS, Value)
        End Set
    End Property

#End Region

#Region "Public Members"
    Public Overrides Function ProcessWSRequest() As String
        Dim row As DataRow

        Try
            Me.Validate()

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region
End Class
