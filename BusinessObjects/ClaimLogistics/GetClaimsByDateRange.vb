
Imports System.Text.RegularExpressions

Public Class GetClaimsByDateRange
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_CLAIM_NUMBER As String = "CLAIM_NUMBER"
    Public Const DATA_COL_NAME_SERVICE_CENTER_CODE As String = "SERVICE_CENTER_CODE"
    Private Const TABLE_NAME As String = "GetClaimsByDateRange"
    Private Const DATASET_NAME As String = "GetClaimsByDateRange"
    Private Const INVALID_SERVICE_CENTER_CODE As String = "INVALID_SERVICE_CENTER_ERR"
    Private Const MAX_NUM_CLAIMS_EXCEEDED As String = "MAX_NUM_CLAIMS_EXCEEDED"
    Private Const MAX_NUM_CLAIMS As Integer = 100
#End Region

#Region "Constructors"

    Public Sub New(ds As GetClaimsByDateRangeDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _serviceCenterId As Guid = Guid.Empty

    Private Sub MapDataSet(ds As GetClaimsByDateRangeDs)

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

    Private Sub Load(ds As GetClaimsByDateRangeDs)
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
            Throw New ElitaPlusException("GetClaimsByDateRange Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ds As GetClaimsByDateRangeDs)
        Try
            If ds.GetClaimsByDateRange.Count = 0 Then Exit Sub
            With ds.GetClaimsByDateRange.Item(0)
                StartDate = ds.GetClaimsByDateRange.Item(0).START_DATE
                EndDate = ds.GetClaimsByDateRange.Item(0).END_DATE
                ServiceCenterCode = ds.GetClaimsByDateRange.Item(0).SERVICE_CENTER_CODE
            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetClaimsByDateRange Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    <ValueMandatory("")> _
    Public Property StartDate As DateTime
        Get
            If Row(PickupListHeaderDAL.COL_NAME_START_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PickupListHeaderDAL.COL_NAME_START_DATE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PickupListHeaderDAL.COL_NAME_START_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property EndDate As DateTime
        Get
            If Row(PickupListHeaderDAL.COL_NAME_END_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(PickupListHeaderDAL.COL_NAME_END_DATE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(PickupListHeaderDAL.COL_NAME_END_DATE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property ServiceCenterCode As String
        Get
            CheckDeleted()
            If Row(DATA_COL_NAME_SERVICE_CENTER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_SERVICE_CENTER_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_SERVICE_CENTER_CODE, Value)
        End Set
    End Property

    Public ReadOnly Property ServiceCenterID As Guid
        Get
            If _serviceCenterId.Equals(Guid.Empty) AndAlso ServiceCenterCode IsNot Nothing AndAlso ServiceCenterCode <> "" Then

                Dim dvServiceCenter As DataView = LookupListNew.GetServiceCenterLookupList(ElitaPlusIdentity.Current.ActiveUser.Countries)

                If dvServiceCenter IsNot Nothing AndAlso dvServiceCenter.Count > 0 Then
                    _serviceCenterId = LookupListNew.GetIdFromCode(dvServiceCenter, ServiceCenterCode)

                    If _serviceCenterId.Equals(Guid.Empty) Then
                        Throw New BOValidationException("GetClaimsByDateRange Error: ", INVALID_SERVICE_CENTER_CODE)
                    End If
                Else
                    Throw New BOValidationException("GetClaimsByDateRange Error: ", INVALID_SERVICE_CENTER_CODE)
                End If

            End If

            Return _serviceCenterId
        End Get
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Validate()

            Dim excludeTags As ArrayList = New ArrayList()

            Dim dsClaim As DataSet = PickupListHeader.GetClaimsByDateRange(StartDate, EndDate, ServiceCenterID)
            dsClaim.DataSetName = DATASET_NAME

            If dsClaim.Tables(0).Rows.Count > MAX_NUM_CLAIMS Then
                Throw New BOValidationException("GetClaimsByDateRange Error: ", MAX_NUM_CLAIMS_EXCEEDED)
            End If

            Dim xmlStr As String = ""

            For Each dr As DataRow In dsClaim.Tables(0).Rows
                Dim oClaim As Claim = ClaimFacade.Instance.CreateClaim(Of Claim)()
                oClaim = ClaimFacade.Instance.GetClaim(Of Claim)(New Guid(CType(dr(ClaimDAL.COL_NAME_CLAIM_ID), Byte())))
                Dim assurantPay As String = CType(oClaim.AssurantPays, String)
                dr("assurant_pay_amount") = assurantPay
            Next

            excludeTags.Add("/GetClaimsByDateRange/CLAIM/CLAIM_ID")

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

