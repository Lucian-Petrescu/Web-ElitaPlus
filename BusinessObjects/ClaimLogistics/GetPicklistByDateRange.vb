Imports System.Text.RegularExpressions

Public Class GetPicklistByDateRange
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_CLAIM_NUMBER As String = "CLAIM_NUMBER"
    Public Const DATA_COL_NAME_INCLUDE_STATUS_HISTORY As String = "INCLUDE_STATUS_HISTORY"
    Private Const TABLE_NAME As String = "GetPicklistByDateRange"
    Private Const DATASET_NAME As String = "GetPicklistByDateRange"
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetPicklistByDateRangeDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _claimId As Guid = Guid.Empty

    Private Sub MapDataSet(ByVal ds As GetPicklistByDateRangeDs)

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

    Private Sub Load(ByVal ds As GetPicklistByDateRangeDs)
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
            Throw New ElitaPlusException("GetPicklistByDateRange Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetPicklistByDateRangeDs)
        Try
            If ds.GetPicklistByDateRange.Count = 0 Then Exit Sub
            With ds.GetPicklistByDateRange.Item(0)
                StartDate = ds.GetPicklistByDateRange.Item(0).START_DATE
                EndDate = ds.GetPicklistByDateRange.Item(0).END_DATE
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetPicklistByDateRange Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    <ValueMandatory("")> _
    Public Property StartDate As DateTime
        Get
            If Row(DALObjects.PickupListHeaderDAL.COL_NAME_START_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DALObjects.PickupListHeaderDAL.COL_NAME_START_DATE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DALObjects.PickupListHeaderDAL.COL_NAME_START_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property EndDate As DateTime
        Get
            If Row(DALObjects.PickupListHeaderDAL.COL_NAME_END_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DALObjects.PickupListHeaderDAL.COL_NAME_END_DATE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DALObjects.PickupListHeaderDAL.COL_NAME_END_DATE, Value)
        End Set
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Validate()
            Dim dsPicklist As DataSet = PickupListHeader.GetPicklistByDateRange(StartDate, EndDate)

            dsPicklist.DataSetName = DATASET_NAME

            Return XMLHelper.FromDatasetToXML(dsPicklist, Nothing, True)

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

