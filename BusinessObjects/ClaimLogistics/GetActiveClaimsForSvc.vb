Imports System.Text.RegularExpressions

Public Class GetActiveClaimsForSvc
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_SERVICE_CENTER_CODE As String = "service_center_code"
    Public Const DATA_COL_NAME_SORT_ORDER As String = "sort_order"
    Public Const DATA_COL_NAME_CURRENT_CLAIM_STATUS_CODE As String = "current_claim_status_code"
    Public Const DATA_COL_NAME_EXCLUDE_REPAIRED_CLAIMS As String = "exclude_repaired_claims"
    Private Const TABLE_NAME As String = "GetActiveClaimsForSvc"

#End Region

#Region "Constructors"

    Public Sub New(ds As GetActiveClaimsForSvcDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _serviceCenterID As Guid = Guid.Empty
    Private _ExtendedClaimStatusListItemID As Guid = Guid.Empty


    Private Sub MapDataSet(ds As GetActiveClaimsForSvcDs)

        Dim schema As String = ds.GetXmlSchema '.Replace(SOURCE_COL_MAKE, DATA_COL_NAME_MANUFACTURER).Replace(SOURCE_COL_MILEAGE, DATA_COL_NAME_ODOMETER).Replace(SOURCE_COL_NEWUSED, DATA_COL_NAME_CONDITION)

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

    Private Sub Load(ds As GetActiveClaimsForSvcDs)
        Try
            Initialize()
            Dim newRow As DataRow = Dataset.Tables(TABLE_NAME).NewRow
            Row = newRow
            PopulateBOFromWebService(ds)
            Dataset.Tables(TABLE_NAME).Rows.Add(newRow)
            _serviceCenterID = Guid.Empty
            _ExtendedClaimStatusListItemID = Guid.Empty
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As BOValidationException
            Throw ex
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetActiveClaimsForSvc Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ds As GetActiveClaimsForSvcDs)
        Try
            If ds.GetActiveClaimsForSvc.Count = 0 Then Exit Sub
            With ds.GetActiveClaimsForSvc.Item(0)
                ServiceCenterCode = .SERVICE_CENTER_CODE.ToUpper
                SortOrder = .SORT_ORDER
                If Not .IsCURRENT_CLAIM_STATUS_CODENull Then CurrentClaimStatusCode = .CURRENT_CLAIM_STATUS_CODE
                If Not .IsEXCLUDE_REPAIRED_CLAIMSNull Then
                    ExcludeRepairedClaims = .EXCLUDE_REPAIRED_CLAIMS
                Else
                    ExcludeRepairedClaims = "N"
                End If                
                'DATA_COL_NAME_EXCLUDE_REPAIRED_CLAIMS
            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetActiveClaimsForSvc Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    <ValueMandatory("")> _
    Public Property ServiceCenterCode As String
        Get
            If Row(DATA_COL_NAME_SERVICE_CENTER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_SERVICE_CENTER_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_SERVICE_CENTER_CODE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property SortOrder As String
        Get
            If Row(DATA_COL_NAME_SORT_ORDER) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_SORT_ORDER), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_SORT_ORDER, Value)
        End Set
    End Property

    Public Property CurrentClaimStatusCode As String
        Get
            If Row(DATA_COL_NAME_CURRENT_CLAIM_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_CURRENT_CLAIM_STATUS_CODE), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CURRENT_CLAIM_STATUS_CODE, Value)
        End Set
    End Property

    Public Property ExcludeRepairedClaims As String
        Get
            If Row(DATA_COL_NAME_EXCLUDE_REPAIRED_CLAIMS) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_EXCLUDE_REPAIRED_CLAIMS), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_EXCLUDE_REPAIRED_CLAIMS, Value)
        End Set
    End Property

    Private ReadOnly Property ServiceCenterID As Guid
        Get

            If _serviceCenterID.Equals(Guid.Empty) Then
                Dim objServiceCenter As New ServiceCenter(ServiceCenterCode)
                _serviceCenterID = objServiceCenter.Id
                If _serviceCenterID.Equals(Guid.Empty) Then
                    Throw New BOValidationException("GetActiveClaimsForSvc Error: ", Common.ErrorCodes.INVALID_SERVICE_CENTER_CODE)
                End If
            End If

            Return _serviceCenterID

        End Get
    End Property

    Private ReadOnly Property ExtendedClaimStatusListItemID As Guid
        Get
            If Not CurrentClaimStatusCode Is Nothing AndAlso Not CurrentClaimStatusCode.Equals(String.Empty) Then
                If _ExtendedClaimStatusListItemID.Equals(Guid.Empty) Then
                    _ExtendedClaimStatusListItemID = LookupListNew.GetIdFromCode(LookupListNew.LK_EXTENDED_CLAIM_STATUSES, CurrentClaimStatusCode)
                    If _ExtendedClaimStatusListItemID.Equals(Guid.Empty) Then
                        Throw New BOValidationException("GetActiveClaimsForSvc Error: ", Common.ErrorCodes.INVALID_CLAIM_EXTENDED_STATUS_CODE)
                    End If
                End If
            End If


            Return _ExtendedClaimStatusListItemID
        End Get
    End Property

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Validate()

            Dim objClaimsDS As DataSet = Claim.GetActiveClaimsForSvc(ServiceCenterID, SortOrder, ExtendedClaimStatusListItemID, ExcludeRepairedClaims)

            If objClaimsDS Is Nothing Then
                Throw New BOValidationException("GetActiveClaimsForSvc Error: ", Common.ErrorCodes.WS_ERROR_ACCESSING_DATABASE)
            ElseIf objClaimsDS.Tables.Count > 0 AndAlso objClaimsDS.Tables(0).Rows.Count >= 0 Then
                'Return (XMLHelper.FromDatasetToXML_Coded(objClaimsDS))
                Return XMLHelper.FromDatasetToXML(objClaimsDS, Nothing, True, True, True, False, True)
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
