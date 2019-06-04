Imports System.Text.RegularExpressions

Public Class GetRoutes
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_DEALER_CODE As String = "dealer_code"
    Private Const TABLE_NAME As String = "GetRoutes"
    Private Const COL_NAME_COUNTRY_ID As String = "country_id"
    Private Const COL_NAME_ROUTE_ID = "route_id"



#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetRoutesDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _dealerId As Guid = Guid.Empty
    Private _serviceNetworkID As Guid = Guid.Empty


    Private Sub MapDataSet(ByVal ds As GetRoutesDs)

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

    Private Sub Load(ByVal ds As GetRoutesDs)
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
            Throw New ElitaPlusException("GetRoutes Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetRoutesDs)
        Try
            If ds.GetRoutes.Count = 0 Then Exit Sub
            With ds.GetRoutes.Item(0)
                If Not .IsDEALER_CODENull Then Me.DealerCode = .DEALER_CODE
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetRoutes Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    Public Property DealerCode() As String
        Get
            If Row(Me.DATA_COL_NAME_DEALER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_DEALER_CODE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_DEALER_CODE, Value)
        End Set
    End Property


    Private ReadOnly Property ServiceNetworkID() As Guid
        Get
            If Me._dealerId.Equals(Guid.Empty) Then

                Dim list As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies)
                If list Is Nothing Then
                    Throw New BOValidationException("OlitagetCertInfo Error: ", Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE)
                End If
                Me._dealerId = LookupListNew.GetIdFromCode(list, Me.DealerCode)
                If _dealerId.Equals(Guid.Empty) Then
                    Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", Common.ErrorCodes.WS_DEALER_NOT_FOUND)
                End If
                list = Nothing
            End If

            If _serviceNetworkID.Equals(Guid.Empty) Then
                Dim objDealer As New Dealer(Me._dealerId)
                Me._serviceNetworkID = objDealer.ServiceNetworkId
                If _serviceNetworkID.Equals(Guid.Empty) Then
                    Throw New BOValidationException("OlitaUpdateConsumerInfo Error: ", Common.ErrorCodes.WS_SERVICE_NETWORK_NOT_FOUND)
                End If
            End If

            Return Me._serviceNetworkID

        End Get
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Me.Validate()

            Dim objRouteDS As DataSet = Route.LoadList(Me.ServiceNetworkID)


            If objRouteDS Is Nothing Then
                Throw New BOValidationException("GetRoutes Error: ", Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE)
            ElseIf objRouteDS.Tables.Count > 0 AndAlso objRouteDS.Tables(0).Rows.Count > 0 Then
                objRouteDS.Tables(0).Columns.Remove(Me.COL_NAME_ROUTE_ID)
                Return (XMLHelper.FromDatasetToXML(objRouteDS))
            ElseIf objRouteDS.Tables.Count > 0 AndAlso objRouteDS.Tables(0).Rows.Count = 0 Then
                Throw New BOValidationException("GetRoutes Error: ", Common.ErrorCodes.WS_ROUTE_NOT_FOUND)
            End If

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
