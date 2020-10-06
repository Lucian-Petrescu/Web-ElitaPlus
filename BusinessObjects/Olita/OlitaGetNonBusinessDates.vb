Imports System.Text.RegularExpressions
Imports Assurant.ElitaPlus.BusinessObjectsNew.OlitaGetNonBusinessDatesDs

Public Class OlitaGetNonBusinessDates
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_COMP_GROUP_CODE As String = "COMPANY_GROUP_CODE"
    Public Const DATA_COL_NAME_START_DATE As String = "START_DATE"
    Public Const DATA_COL_NAME_END_DATE As String = "END_DATE"
    Private Const TABLE_NAME As String = "OlitaGetNonBusinessDates"
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As OlitaGetNonBusinessDatesDs)
        MyBase.New()
        MapDataSet(ds)
        Load(ds)
    End Sub

#End Region

#Region "Private Members"
    Private Sub MapDataSet(ByVal ds As OlitaGetNonBusinessDatesDs)

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

    Private Sub Load(ByVal ds As OlitaGetNonBusinessDatesDs)
        Try
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
            Throw New ElitaPlusException("Olita OlitaGetNonBusinessDates Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As OlitaGetNonBusinessDatesDs)
        Try
            If ds.OlitaGetNonBusinessDates.Count = 0 Then Exit Sub
            With ds.OlitaGetNonBusinessDates.Item(0)
                CompanyGroupCode = .COMPANY_GROUP_CODE
                StartDate = .START_DATE
                EndDate = .END_DATE
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Olita OlitaGetNonBusinessDates Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

#End Region

#Region "Properties"

    <ValueMandatory("")> _
    Public Property CompanyGroupCode() As String
        Get
            If Row(DATA_COL_NAME_COMP_GROUP_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_COMP_GROUP_CODE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            SetValue(DATA_COL_NAME_COMP_GROUP_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property StartDate() As DateType
        Get
            CheckDeleted()
            If Row(DATA_COL_NAME_START_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DATA_COL_NAME_START_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(DATA_COL_NAME_START_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property EndDate() As DateType
        Get
            CheckDeleted()
            If Row(DATA_COL_NAME_END_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return New DateType(CType(Row(DATA_COL_NAME_END_DATE), Date))
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            SetValue(DATA_COL_NAME_END_DATE, Value)
        End Set
    End Property

    Protected Shadows Sub CheckDeleted()
    End Sub
#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Validate()

            Dim ds As DataSet = NonbusinessCalendar.GetNonBusinessDates(CompanyGroupCode, StartDate, EndDate)
            ds.Tables(0).TableName = TABLE_NAME
            Return XMLHelper.FromDatasetToXML(ds, Nothing, True, True, True, False, True)
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
End Class
