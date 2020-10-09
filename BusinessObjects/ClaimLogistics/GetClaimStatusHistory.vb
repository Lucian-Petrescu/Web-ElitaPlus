Imports System.Text.RegularExpressions

Public Class GetClaimStatusHistory
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_CLAIM_NUMBER As String = "CLAIM_NUMBER"
    Private Const TABLE_NAME As String = "GetClaimStatusHistory"
    Private Const DATASET_NAME As String = "GetClaimStatusHistory"

    Private _claimId As Guid = Guid.Empty
#End Region

#Region "Constructors"

    Public Sub New(ds As GetClaimStatusHistoryDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"

    Private Sub MapDataSet(ds As GetClaimStatusHistoryDs)

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

    Private Sub Load(ds As GetClaimStatusHistoryDs)
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
            Throw New ElitaPlusException("GetClaimStatusHistory Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ds As GetClaimStatusHistoryDs)
        Try
            If ds.GetClaimStatusHistory.Count = 0 Then Exit Sub
            With ds.GetClaimStatusHistory.Item(0)
                ClaimNumber = ds.GetClaimStatusHistory.Item(0).CLAIM_NUMBER
            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetClaimStatusHistory Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property ClaimNumber As String
        Get
            If Row(DATA_COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_CLAIM_NUMBER), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property

    Private ReadOnly Property ClaimID As Guid
        Get
            If _claimId.Equals(Guid.Empty) Then
                _claimId = Claim.GetClaimID(ElitaPlusIdentity.Current.ActiveUser.Companies, ClaimNumber)

                If _claimId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("GetClaimStatusHistory Error: ", Common.ErrorCodes.INVALID_CLAIM_NOT_FOUND)
                End If

            End If

            Return _claimId
        End Get
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Validate()

            Dim dsClaim As DataSet = PickupListHeader.GetClaimStatusHistory(ClaimID)

            dsClaim.DataSetName = DATASET_NAME
            Dim excludeTags As ArrayList = New ArrayList()
            excludeTags.Add("/GetClaimStatusHistory/CLAIM_STATUS_HISTORY/CLAIM_ID")

            Return XMLHelper.FromDatasetToXML(dsClaim, excludeTags, True)

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
