Imports System.Text.RegularExpressions

Public Class GetParts
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_risk_group_code As String = "risk_group_code"
    Private Const TABLE_NAME As String = "GetParts"
    Public Const DATA_COL_NAME_CLAIM_NUMBER As String = "Claim_Number"
    Public Const DATA_COL_NAME_COMPANY_CODE As String = "Company_Code"

#End Region

#Region "Constructors"

    Public Sub New(ds As GetPartsDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"

    Private _Risk_Group_id As Guid = Guid.Empty
    Private _ClaimId As Guid = Guid.Empty
    Private _company_id As Guid = Guid.Empty

    Private Sub MapDataSet(ds As GetPartsDs)

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

    Private Sub Load(ds As GetPartsDs)
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
            Throw New ElitaPlusException("WSUtilities GetParts Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ds As GetPartsDs)
        Try
            If ds.GetParts.Count = 0 Then Exit Sub
            With ds.GetParts.Item(0)

                'Claim poarameter takes priority over the Risk Group param
                If Not .IsClaim_IDNull Then
                    ClaimID = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(.Claim_ID))
                    LoadRiskGroup()
                ElseIf (Not .IsClaim_NumberNull AndAlso .IsCompany_CodeNull) OrElse (.IsClaim_NumberNull AndAlso Not .IsCompany_CodeNull) Then
                    Throw New BOValidationException("WSUtilities GetParts Error: ", Common.ErrorCodes.WS_XML_INVALID)
                ElseIf Not .IsClaim_NumberNull AndAlso Not .IsCompany_CodeNull Then
                    CompanyCode = .Company_Code
                    ClaimNumber = .Claim_Number
                    ValidateCompany()
                    Dim objClaim As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(ClaimNumber, CompanyId)
                    If objClaim Is Nothing Then
                        Throw New BOValidationException("WSUtilities GetParts Error: ", Common.ErrorCodes.INVALID_CLAIM_NOT_FOUND)
                    End If
                    ClaimID = objClaim.Id
                    LoadRiskGroup()
                Else
                    If Not .IsRisk_Group_CodeNull Then
                        RiskGroupCode = .Risk_Group_Code
                        getRiskGroupId(.Risk_Group_Code)
                    Else
                        _Risk_Group_id = Nothing
                    End If
                End If
            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("WSUtilities Invalid Parameters Error", Common.ErrorCodes.BO_INVALID_DATA, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

    Private Sub getRiskGroupId(Code As String)
        Dim dvRiskGroups As DataView = LookupListNew.GetRiskGroupsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        If dvRiskGroups IsNot Nothing AndAlso dvRiskGroups.Count > 0 Then
            _Risk_Group_id = LookupListNew.GetIdFromCode(dvRiskGroups, Code)
            If _Risk_Group_id.Equals(Guid.Empty) Then
                Throw New BOValidationException("WSUtilities GetParts Error: ", Common.ErrorCodes.ERR_INVALID_RISK_GROUP)
            End If
        End If
    End Sub
#End Region

#Region "Properties"

    Public Property RiskGroupCode As String
        Get
            If Row(DATA_COL_NAME_risk_group_code) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(DATA_COL_NAME_risk_group_code), String))
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_risk_group_code, Value)
        End Set
    End Property

    Public Property ClaimID As Guid
        Get
            Return _ClaimId
        End Get
        Set
            _ClaimId = Value
        End Set
    End Property

    Private ReadOnly Property RiskGroupId As Guid
        Get
            Return _Risk_Group_id
        End Get
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property ClaimNumber As String
        Get
            CheckDeleted()
            If Row(DATA_COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)> _
    Public Property CompanyCode As String
        Get
            CheckDeleted()
            If Row(DATA_COL_NAME_COMPANY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(DATA_COL_NAME_COMPANY_CODE), String)
            End If
        End Get
        Set
            CheckDeleted()
            SetValue(DATA_COL_NAME_COMPANY_CODE, Value)
        End Set
    End Property
    Public Property CompanyId As Guid
        Get
            Return _company_id
        End Get
        Set
            _company_id = Value
        End Set
    End Property
#End Region

#Region "Extended Properties"
    Private Sub ValidateCompany()
        Dim objCompaniesAL As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        Dim i As Integer
        For i = 0 To objCompaniesAL.Count - 1
            Dim objCompany As New Company(CType(objCompaniesAL.Item(i), Guid))
            If objCompany IsNot Nothing AndAlso objCompany.Code.Equals(CompanyCode.ToUpper) Then
                CompanyId = objCompany.Id
            End If
        Next
        If CompanyId.Equals(Guid.Empty) Then
            Throw New BOValidationException("GetClaimDetail Error: ", Common.ErrorCodes.WS_INVALID_COMPANY_CODE)
        End If
    End Sub

    Private Sub LoadRiskGroup()
        If ClaimID.Equals(Guid.Empty) Then
            Throw New BOValidationException("WSUtilities GetParts Error: ", Common.ErrorCodes.INVALID_CLAIM_NOT_FOUND)
        End If

        Dim objClaim As Claim = ClaimFacade.Instance.GetClaim(Of Claim)(ClaimID)

        If objClaim Is Nothing Then
            Throw New BOValidationException("WSUtilities GetParts Error: ", Common.ErrorCodes.INVALID_CLAIM_NOT_FOUND)
        End If

        Dim riskTypeBO As RiskType = New RiskType(objClaim.RiskTypeId)
        _Risk_Group_id = riskTypeBO.RiskGroupId
    End Sub
#End Region
#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String

        Try
            Dim ds As New DataSet("PARTS_LIST")
            Validate()
            
                Dim PartDiscriptionList As DataTable
                PartDiscriptionList = PartsDescription.getListForWS(RiskGroupId)
                If PartDiscriptionList Is Nothing OrElse PartDiscriptionList.Rows.Count <= 0 Then
                    Throw New BOValidationException("WSUtilities GetParts Error: ", Common.ErrorCodes.BO_ERROR_PART_INFO_NOT_FOUND)
                End If
                PartDiscriptionList.Columns.Remove("parts_description_id")
                PartDiscriptionList.Columns.Remove("risk_group_id")
                PartDiscriptionList.Columns.Remove("company_group_id")
                ds.Tables.Add(PartDiscriptionList.Copy)

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
