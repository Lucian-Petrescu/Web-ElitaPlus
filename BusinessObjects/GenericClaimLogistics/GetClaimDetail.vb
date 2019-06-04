Imports System.Text.RegularExpressions

Public Class GetClaimDetail
    Inherits BusinessObjectBase

#Region "Constants"
    Public Const DATA_COL_NAME_CLAIM_ID As String = "Claim_ID"
    Public Const DATA_COL_NAME_CLAIM_NUMBER As String = "Claim_Number"
    Public Const DATA_COL_NAME_COMPANY_CODE As String = "Company_Code"
    Public Const DATA_COL_NAME_INCLUDE_PART_DESCRIPTIONS As String = "Include_Part_Descriptions"
    Public Const DATA_COL_NAME_FOR_SVC_USE As String = "For_SVC_Use"
    Private Const TABLE_NAME As String = "GetClaimDetail"
    Private Const DATASET_NAME As String = "GetClaimDetail"
#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetClaimDetailDs)
        MyBase.New()

        MapDataSet(ds)
        ValidateInput(ds)
        Load(ds)

    End Sub

#End Region

#Region "Private Members"
    Private _claimId As Guid = Guid.Empty
    Private _company_id As Guid = Guid.Empty


    Private Sub MapDataSet(ByVal ds As GetClaimDetailDs)

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

    Private Sub ValidateInput(ByVal ds As GetClaimDetailDs)

        With ds.GetClaimDetail.Item(0)
            If .IsClaim_IDNull Then
                If .IsClaim_NumberNull AndAlso .IsCompany_CodeNull Then
                    Throw New BOValidationException("GetClaimDetail Error: ", Common.ErrorCodes.WS_CLAIM_ID_OR_CLAIM_NUMBER_IS_REQUIRED)
                End If
            End If

        End With

    End Sub

    Private Sub Load(ByVal ds As GetClaimDetailDs)
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
            Throw New ElitaPlusException("GetClaimDetail Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As GetClaimDetailDs)
        Try
            If ds.GetClaimDetail.Count = 0 Then Exit Sub
            With ds.GetClaimDetail.Item(0)
                If Not .IsClaim_IDNull Then Me.ClaimStringId = .Claim_ID
                Me.ForSvcUse = .For_SVC_Use
                If Not .IsInclude_Part_DescriptionsNull Then
                    Me.IncludePartDescription = .Include_Part_Descriptions
                Else
                    Me.IncludePartDescription = "N" 'Default
                End If

                If Not .IsClaim_NumberNull Then Me.ClaimNumber = .Claim_Number
                If Not .IsCompany_CodeNull Then
                    Me.CompanyCode = .Company_Code
                    ValidateCompany()
                End If

            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Properties"

    <ValidStringLength("", Max:=32)> _
    Public Property ClaimStringId() As String
        Get
            If Row(Me.DATA_COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_CLAIM_ID), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_CLAIM_ID, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=1)> _
    Public Property IncludePartDescription() As String
        Get
            If Row(Me.DATA_COL_NAME_INCLUDE_PART_DESCRIPTIONS) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_INCLUDE_PART_DESCRIPTIONS), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_INCLUDE_PART_DESCRIPTIONS, Value)
        End Set
    End Property

    <ValueMandatory(""), ValidStringLength("", Max:=1)> _
    Public Property ForSvcUse() As String
        Get
            If Row(Me.DATA_COL_NAME_FOR_SVC_USE) Is DBNull.Value Then
                Return Nothing
            Else
                Return (CType(Row(Me.DATA_COL_NAME_FOR_SVC_USE), String))
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_FOR_SVC_USE, Value)
        End Set
    End Property


    <ValidStringLength("", Max:=20)>
    Public Property ClaimNumber() As String
        Get
            CheckDeleted()
            If Row(Me.DATA_COL_NAME_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_CLAIM_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_CLAIM_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)> _
    Public Property CompanyCode() As String
        Get
            CheckDeleted()
            If Row(Me.DATA_COL_NAME_COMPANY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(Me.DATA_COL_NAME_COMPANY_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(Me.DATA_COL_NAME_COMPANY_CODE, Value)
        End Set
    End Property
    Public Property CompanyId() As Guid
        Get
            Return _company_id
        End Get
        Set(ByVal Value As Guid)
            _company_id = Value
        End Set
    End Property


#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Me.Validate()
            'Get Claim detail
            Dim dsClaim As DataSet = Claim.GetClaimDetailForWS(Me.ClaimStringId, Me.ClaimNumber, Me.CompanyId)
            dsClaim.DataSetName = "GET_CLAIM_DETAIL"
            If dsClaim Is Nothing Or dsClaim.Tables.Count <= 0 Or dsClaim.Tables(0).Rows.Count = 0 Then
                Throw New BOValidationException("GetClaimDetail Error: ", Common.ErrorCodes.INVALID_CLAIM_NOT_FOUND)
            Else
                'Get Sub_lists
                dsClaim.Tables(0).TableName = ClaimDAL.TABLE_NAME_WS
                Dim ClaimIdGuid = New Guid(CType(dsClaim.Tables(ClaimDAL.TABLE_NAME_WS).Rows(0)(ClaimDAL.COL_NAME_CLAIMID), Byte()))
                Dim oRiskTypeId As Guid = New Guid(CType(dsClaim.Tables(0).Rows(0)(DALObjects.ClaimDAL.COL_NAME_RISK_TYPE_ID), Byte()))
                dsClaim.Tables(ClaimDAL.TABLE_NAME_WS).Columns.Remove(ClaimDAL.COL_NAME_RISK_TYPE_ID)
                dsClaim.Tables(ClaimDAL.TABLE_NAME_WS).Columns.Remove(ClaimDAL.COL_NAME_CLAIMID)
                If Me.ForSvcUse.ToUpper.Equals("N") Then
                    'Get Claim comments list
                    Dim dsComments As DataSet = Comment.GetCommentsForClaim(ClaimIdGuid)
                    If Not dsComments Is Nothing AndAlso dsComments.Tables.Count > 0 AndAlso dsComments.Tables(CommentDAL.TABLE_NAME).Rows.Count <> 0 Then
                        Dim commentsTable As DataTable = dsComments.Tables(CommentDAL.TABLE_NAME).Copy
                        commentsTable.TableName = CommentDAL.TABLE_NAME_WS
                        commentsTable.Columns.Remove(CommentDAL.COL_NAME_CERT_ID)
                        commentsTable.Columns.Remove(CommentDAL.COL_NAME_CLAIM_ID)
                        commentsTable.Columns.Remove(CommentDAL.COL_NAME_CALLER_NAME)
                        commentsTable.Columns.Remove(CommentDAL.COL_NAME_COMMENT_TYPE_ID)
                        commentsTable.Columns.Remove(CommentDAL.COL_NAME_CREATED_BY)
                        commentsTable.Columns.Remove(CommentDAL.COL_NAME_MODIFIED_DATE)
                        commentsTable.Columns.Remove(CommentDAL.COL_NAME_MODIFIED_BY)
                        commentsTable.Columns.Remove(CommentDAL.COL_NAME_COMMENT_ID)
                        dsClaim.Tables.Add(commentsTable)
                    End If
                End If

                'Get Claim Extended Status List
                Dim dvClaimExtendedStatus As DataView = ClaimStatus.GetClaimStatusList(ClaimIdGuid)
                If Not dvClaimExtendedStatus Is Nothing AndAlso dvClaimExtendedStatus.Count > 0 Then
                    Dim ClaimExtendedStatusTable As DataTable = dvClaimExtendedStatus.Table.Copy
                    ClaimExtendedStatusTable.TableName = ClaimStatusDAL.TABLE_NAME_WS
                    ClaimExtendedStatusTable.Columns.Remove(ClaimStatusDAL.COL_NAME_CLAIM_ID)
                    ClaimExtendedStatusTable.Columns.Remove(ClaimStatusDAL.COL_NAME_CREATED_BY)
                    ClaimExtendedStatusTable.Columns.Remove(ClaimStatusDAL.COL_NAME_CREATED_DATE)
                    ClaimExtendedStatusTable.Columns.Remove(ClaimStatusDAL.COL_NAME_MODIFIED_DATE)
                    ClaimExtendedStatusTable.Columns.Remove(ClaimStatusDAL.COL_NAME_MODIFIED_BY)
                    ClaimExtendedStatusTable.Columns.Remove(ClaimStatusDAL.COL_NAME_CLAIM_STATUS_ID)
                    ClaimExtendedStatusTable.Columns.Remove(ClaimStatusDAL.COL_NAME_STATUS_ORDER)
                    dsClaim.Tables.Add(ClaimExtendedStatusTable)
                End If

                'Get Claim Parts Info List
                Dim dvClaimPartsInfo As DataView = PartsInfo.getSelectedList(ClaimIdGuid)
                If Not dvClaimPartsInfo Is Nothing AndAlso dvClaimPartsInfo.Count > 0 Then
                    Dim ClaimPartsInfoTable As DataTable = dvClaimPartsInfo.Table.Copy
                    ClaimPartsInfoTable.TableName = PartsInfoDAL.TABLE_NAME_WS
                    ClaimPartsInfoTable.Columns.Remove(PartsInfoDAL.COL_NAME_PARTS_INFO_ID)
                    ClaimPartsInfoTable.Columns.Remove(PartsInfoDAL.COL_NAME_CLAIM_ID)
                    ClaimPartsInfoTable.Columns.Remove(PartsInfoDAL.COL_NAME_PARTS_DESCRIPTION_ID)
                    ClaimPartsInfoTable.Columns.Remove(PartsInfoDAL.COL_NAME_IN_STOCK_ID)
                    dsClaim.Tables.Add(ClaimPartsInfoTable)
                End If

                'ClaimFacade.Instance.GetClaim(Of Claim)DetailForWS(Me.ClaimId, Me.ClaimNumber, Me.CompanyId)
                If Not Me.IncludePartDescription Is Nothing AndAlso Me.IncludePartDescription.ToUpper = "Y" Then
                    Dim riskTypeBO As RiskType = New RiskType(oRiskTypeId)
                    Dim partsDataTable As DataTable = PartsDescription.getListForWS(riskTypeBO.RiskGroupId).Copy
                    If Not partsDataTable Is Nothing AndAlso partsDataTable.Rows.Count > 0 Then
                        partsDataTable.TableName = PartsDescriptionDAL.TABLE_NAME_WS
                        partsDataTable.Columns.Remove(PartsDescriptionDAL.COL_NAME_PARTS_DESCRIPTION_ID)
                        partsDataTable.Columns.Remove(PartsDescriptionDAL.COL_NAME_RISK_GROUP_ID)
                        partsDataTable.Columns.Remove(PartsDescriptionDAL.COL_NAME_COMPANY_GROUP_ID)
                        dsClaim.Tables.Add(partsDataTable)
                    End If
                End If
                Return XMLHelper.FromDatasetToXML(dsClaim, Nothing, True, True, True, False, True)

            End If

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region

#Region "Extended Properties"
    Private Sub ValidateCompany()
        Dim objCompaniesAL As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        Dim i As Integer
        For i = 0 To objCompaniesAL.Count - 1
            Dim objCompany As New Company(CType(objCompaniesAL.Item(i), Guid))
            If Not objCompany Is Nothing AndAlso objCompany.Code.Equals(Me.CompanyCode.ToUpper) Then
                Me.CompanyId = objCompany.Id
            End If
        Next
        If Me.CompanyId.Equals(Guid.Empty) Then
            Throw New BOValidationException("GetClaimDetail Error: ", Common.ErrorCodes.WS_INVALID_COMPANY_CODE)
        End If


    End Sub
#End Region

End Class
