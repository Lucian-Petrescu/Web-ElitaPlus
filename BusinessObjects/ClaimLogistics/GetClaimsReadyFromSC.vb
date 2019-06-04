
Imports System.Xml
Imports System.IO
Imports System.Text

Public Class GetClaimsReadyFromSC
    Inherits BusinessObjectBase

#Region "Constants"

    Private Const TABLE_NAME As String = "GetClaimsReadyFromSC"
    Private Const DATASET_NAME As String = "GetClaimsReadyFromSC"
    Private Const DATASET_TABLE_NAME As String = "Claim"
    Private Const COL_ROUTE_NUMBER As String = "ROUTE_NUMBER"
    Private RouteId As Guid = Guid.Empty

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetClaimsReadyFromSCDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)
    End Sub

#End Region

#Region "Private Members"

    Private Sub MapDataSet(ByVal ds As GetClaimsReadyFromSCDs)
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

    Private Sub Load(ByVal ds As GetClaimsReadyFromSCDs)
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
            Throw New ElitaPlusException("GetClaimsReadyFromSC Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetClaimsReadyFromSCDs)
        Try
            If ds.GetClaimsReadyFromSC.Count = 0 Then Exit Sub
            With ds.GetClaimsReadyFromSC.Item(0)
                Me.RouteCode = .ROUTE_NUMBER
            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetClaimsReadyFromSC Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Me.Validate()

            Dim dsRoute As DataSet = Route.GetRouteByCode(Me.RouteCode)
            If dsRoute Is Nothing Or dsRoute.Tables.Count <= 0 Or dsRoute.Tables(0).Rows.Count <> 1 Then
                Throw New BOValidationException("GetClaimsReadyFromSC Error: ", Common.ErrorCodes.WS_ROUTE_NOT_FOUND)
            Else
                RouteId = New Guid(CType(dsRoute.Tables(0).Rows(0)(RouteDAL.TABLE_KEY_NAME), Byte()))
            End If

            Dim headerBO As New PickupListHeader
            Dim dsHeader As DataSet = headerBO.GetClaimsReadyFromSC(Me.RouteId)

            dsHeader.DataSetName = Me.DATASET_NAME

            Dim excludeTags As ArrayList = New ArrayList()
            excludeTags.Add("/GetClaimsReadyFromSC/PICKLIST/ROUTE_ID")
            excludeTags.Add("/GetClaimsReadyFromSC/PICKLIST/STORE/ROUTE_ID")
            excludeTags.Add("/GetClaimsReadyFromSC/PICKLIST/STORE/STORE_SERVICE_CENTER_ID")
            excludeTags.Add("/GetClaimsReadyFromSC/PICKLIST/STORE/SERVICE_CENTER/SERVICE_CENTER_ID")
            excludeTags.Add("/GetClaimsReadyFromSC/PICKLIST/STORE/SERVICE_CENTER/STORE_SERVICE_CENTER_ID")
            excludeTags.Add("/GetClaimsReadyFromSC/PICKLIST/STORE/SERVICE_CENTER/CLAIM/SERVICE_CENTER_ID")
            excludeTags.Add("/GetClaimsReadyFromSC/PICKLIST/STORE/SERVICE_CENTER/CLAIM/STORE_SERVICE_CENTER_ID")

            Return XMLHelper.FromDatasetToXML(dsHeader, excludeTags, True)

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

#Region "Properties"

    <ValueMandatory(""), ValidStringLength("", Max:=50)> _
    Public Property RouteCode() As String
        Get
            CheckDeleted()
            If Row(COL_ROUTE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_ROUTE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_ROUTE_NUMBER, Value)
        End Set
    End Property

#End Region

End Class

