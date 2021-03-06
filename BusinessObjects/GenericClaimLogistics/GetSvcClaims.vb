﻿Imports System.Xml
Imports System.IO
Imports System.Text

Public Class GetSvcClaims
    Inherits BusinessObjectBase

#Region "Constants"

    Private Const TABLE_NAME As String = "GetSvcClaims"
    Private Const TABLE_NAME_SVC As String = "SVC_CLAIM"
    Private Const DATASET_NAME As String = "GetSvcClaims"
    Private Const DATASET_TABLE_NAME As String = "Claim"
    Private Const COL_CLAIM_STATUS As String = "CLAIM_STATUS"
    Private Const COL_CLAIM_TYPE As String = "CLAIM_TYPE"
    Private Const COL_METHOD_OF_REPAIR As String = "METHOD_OF_REPAIR"
    Private Const COL_CLAIM_EXTENDED_STATUS_CODE As String = "CLAIM_EXTENDED_STATUS_CODE"
    Private Const COL_SORT_BY As String = "SORT_BY"
    Private Const COL_SORT_ORDER As String = "SORT_ORDER"
    Private Const COL_PAGE_NUMBER As String = "PAGE_NUMBER"
    Private Const COL_PAGE_SIZE As String = "PAGE_SIZE"
    Private Const COL_CLAIM_NUMBER As String = "CLAIM_NUMBER"
    Private Const COL_AUTHORIZATION_NUMBER As String = "AUTHORIZATION_NUMBER"
    Private Const COL_CERTIFICATE_NUMBER As String = "CERTIFICATE_NUMBER"
    Private Const COL_CUSTOMER_NAME As String = "CUSTOMER_NAME"
    Private Const COL_FROM_CLAIM_CREATED_DATE As String = "FROM_CLAIM_CREATED_DATE"
    Private Const COL_TO_CLAIM_CREATED_DATE As String = "TO_CLAIM_CREATED_DATE"
    Private Const COL_FROM_VISIT_DATE As String = "FROM_VISIT_DATE"
    Private Const COL_TO_VISIT_DATE As String = "TO_VISIT_DATE"
    Private Const COL_CLAIM_EXTENDED_STATUS_OWNER_CODE As String = "CLAIM_EXTENDED_STATUS_OWNER_CODE"
    Private Const COL_SERVICE_CENTER_CODE As String = "SERVICE_CENTER_CODE"
    Private Const COL_NUMBER_OF_ROW As String = "number_of_row"
    Private Const COL_TURN_AROUND_TIME_RANGE_CODE As String = "TURN_AROUND_TIME_RANGE_CODE"
    Private Const COL_BATCH_NUMBER As String = "BATCH_NUMBER"
    Private Const COL_SERIAL_NUMBER As String = "SERIAL_NUMBER"
    Private Const COL_WORK_PHONE As String = "WORK_PHONE"
    Private Const COL_COMPANY_CODE As String = "COMPANY_CODE"
    Private Const COL_HOME_PHONE As String = "HOME_PHONE"
    Private Const COL_LOSS_DATE As String = "LOSS_DATE"
    Private Const COL_CLAIM_PAID_AMOUNT As String = "CLAIM_PAID_AMOUNT"
    Private Const COL_BONUS_TOTAL As String = "BONUS_TOTAL"
    Private RouteId As Guid = Guid.Empty

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As GetSvcClaimsDs)
        MyBase.New()

        MapDataSet(ds)
        Load(ds)
    End Sub

#End Region

#Region "Private Members"
    Dim _methodOfRepairIds As ArrayList
    Dim _ClaimExtendedStatusIds As ArrayList
    Dim _ClaimExtendedStatusOwnerIds As ArrayList
    Dim _claimTypeIds As ArrayList
    Private Sub MapDataSet(ByVal ds As GetSvcClaimsDs)
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

    Private Sub Load(ByVal ds As GetSvcClaimsDs)
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
            Throw New ElitaPlusException("GetSvcClaims Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub

    Private Sub BuildMethodOfRepairArrayList()
        If Me._methodOfRepairIds Is Nothing Then Me._methodOfRepairIds = New ArrayList

        ' Split string based on solid pipe
        Dim oList As String() = Me.MethodOfRepair.Split(New Char() {"|"c})

        ' Use For Each loop over oList to get the method of repair id
        Dim word As String
        For Each word In oList
            Dim oMOR As Guid = LookupListNew.GetIdFromCode(LookupListCache.LK_METHODS_OF_REPAIR, word)
            If oMOR.Equals(Guid.Empty) Then
                Throw New ElitaPlusException("GetSvcClaims Error: ", Common.ErrorCodes.INVALID_METHOD_OF_REPAIR)
            Else
                Me._methodOfRepairIds.Add(oMOR)
            End If
        Next

    End Sub
    Private Sub BuildClaimExtendedStatusIDsList()
        If Me._ClaimExtendedStatusIds Is Nothing Then Me._ClaimExtendedStatusIds = New ArrayList
        ' Split string based on solid pipe
        Dim oList As String() = Me.ClaimExtendedStatusCode.Split(New Char() {"|"c})

        ' Use For Each loop over oList to get the claim extended status id
        Dim word As String
        Dim dv As DataView = LookupListNew.GetExtendedStatusLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        For Each word In oList
            Dim oCES As Guid = LookupListNew.GetIdFromCode(dv, word)
            If oCES.Equals(Guid.Empty) Then
                Throw New ElitaPlusException("GetSvcClaims Error: ", Common.ErrorCodes.INVALID_CLAIM_EXTENDED_STATUS_CODE)
            Else
                Me._ClaimExtendedStatusIds.Add(oCES)
            End If
        Next
    End Sub

    Private Sub BuildClaimTypeList()

        If Me._claimTypeIds Is Nothing Then Me._claimTypeIds = New ArrayList

        ' Split string based on solid pipe
        Dim oList As String() = Me.ClaimType.Split(New Char() {"|"c})

        ' Use For Each loop over oList to get the claim typer id
        Dim word As String
        For Each word In oList
            Dim oClaimTypeID As Guid = LookupListNew.GetIdFromCode(LookupListCache.LK_CLAIM_TYPES, word)
            If oClaimTypeID.Equals(Guid.Empty) Then
                Throw New ElitaPlusException("GetSvcClaims Error: ", Common.ErrorCodes.INVALID_CLAIM_TYPE_CODE)
            Else
                Me._claimTypeIds.Add(oClaimTypeID)
            End If
        Next

    End Sub
    Private Sub BuildClaimExtendedStatusOwnerCodeList()
        If Me._ClaimExtendedStatusOwnerIds Is Nothing Then Me._ClaimExtendedStatusOwnerIds = New ArrayList

        ' We want to split this input string
        Dim s As String = Me.MethodOfRepair

        ' Split string based on solid pipe
        Dim oList As String() = Me.ClaimExtendedStatusOwnerCode.Split(New Char() {"|"c})

        ' Use For Each loop over oList to get the claim extended status id
        Dim word As String
        For Each word In oList
            Dim oCES As Guid = LookupListNew.GetIdFromCode(LookupListCache.LK_EXTENDED_CLAIM_STATUS_OWNER, word)
            If oCES.Equals(Guid.Empty) Then
                Throw New ElitaPlusException("GetSvcClaims Error: ", Common.ErrorCodes.INVALID_CLAIM_EXTENDED_STATUS_OWNER_CODE)
            Else
                Me._ClaimExtendedStatusOwnerIds.Add(oCES)
            End If
        Next

    End Sub
    Private Sub PopulateBOFromWebService(ByVal ds As GetSvcClaimsDs)
        Try
            If ds.GetSvcClaims.Count = 0 Then Exit Sub
            With ds.GetSvcClaims.Item(0)
                If .IsService_Center_CodeNull And .IsCertificate_NumberNull And .IsClaim_NumberNull Then
                    Throw New BOValidationException("GetSvcClaims Error: ", Common.ErrorCodes.WS_SERVICE_CENTER_IS_REQUIRED)
                ElseIf Not .IsService_Center_CodeNull And .IsClaim_StatusNull Then
                    Throw New BOValidationException("GetSvcClaims Error: ", Common.ErrorCodes.WS_CLAIM_STATUS_IS_REQUIRED)
                ElseIf Not .IsService_Center_CodeNull And Not .IsClaim_StatusNull And .IsClaim_TypeNull Then
                    Throw New BOValidationException("GetSvcClaims Error: ", Common.ErrorCodes.WS_CLAIM_TYPE_IS_REQUIRED)
                ElseIf Not .IsService_Center_CodeNull And Not .IsClaim_StatusNull And Not .IsClaim_TypeNull And .IsMethod_Of_RepairNull Then
                    Throw New BOValidationException("GetSvcClaims Error: ", Common.ErrorCodes.WS_METHOD_OF_REPAIR_IS_REQUIRED)
                ElseIf .IsService_Center_CodeNull And Not .IsClaim_NumberNull And .IsCompany_CodeNull Then
                    Throw New BOValidationException("GetSvcClaims Error: ", Common.ErrorCodes.WS_COMPANY_CODE_IS_REQUIRED)
                ElseIf .IsService_Center_CodeNull And Not .IsCertificate_NumberNull And .IsCompany_CodeNull Then
                    Throw New BOValidationException("GetSvcClaims Error: ", Common.ErrorCodes.WS_COMPANY_CODE_IS_REQUIRED)
                End If
                If Not .IsService_Center_CodeNull Then Me.ServiceCenterCode = .Service_Center_Code
                If Not .IsClaim_StatusNull Then Me.ClaimStatus = .Claim_Status 'A|C|D|P
                If Not .IsClaim_TypeNull Then
                    Me.ClaimType = .Claim_Type     'RT|SVC|RPR
                    Me.BuildClaimTypeList()
                End If
                If Not .IsMethod_Of_RepairNull Then
                    Me.MethodOfRepair = .Method_Of_Repair
                    BuildMethodOfRepairArrayList()
                End If
                Me.SortBy = .Sort_By
                Me.SortOrder = .Sort_Order
                Me.PageSize = .Page_Size
                Me.PageNumber = .Page_Number
                If Not .IsClaim_NumberNull Then Me.ClaimNumber = .Claim_Number
                If Not .IsAuthorization_NumberNull Then Me.AuthorizationNumber = .Authorization_Number
                If Not .IsCertificate_NumberNull Then Me.CertificateNumber = .Certificate_Number
                If Not .IsCustomer_NameNull Then Me.CustomerName = .Customer_Name
                If Not .IsFrom_Claim_Created_DateNull Then Me.FromClaimCreatedDate = .From_Claim_Created_Date
                If Not .IsTo_Claim_Created_DateNull Then Me.ToClaimCreatedDate = .To_Claim_Created_Date
                If Not .IsFrom_Visit_DateNull Then Me.FromVisitDate = .From_Visit_Date
                If Not .IsTo_Visit_DateNull Then Me.ToVisitDate = .To_Visit_Date
                If Not .IsClaim_Extended_Status_CodeNull Then
                    Me.ClaimExtendedStatusCode = .Claim_Extended_Status_Code
                    BuildClaimExtendedStatusIDsList()
                End If
                If Not .IsClaim_Extended_Status_Owner_CodeNull Then
                    Me.ClaimExtendedStatusOwnerCode = .Claim_Extended_Status_Owner_Code
                    BuildClaimExtendedStatusOwnerCodeList()
                End If
                If Not .IsTurn_Around_Time_Range_CodeNull Then Me.TurnAroundTimeRangeCode = .Turn_Around_Time_Range_Code
                If Not .IsBatch_NumberNull Then Me.BatchNumber = .Batch_Number
                If Not .IsSerial_NumberNull Then Me.SerialNumber = .Serial_Number
                If Not .IsWork_PhoneNull Then Me.WorkPhone = .Work_Phone
                If Not .IsCompany_CodeNull Then Me.CompanyCode = .Company_Code
                If Not .IsHome_PhoneNull Then Me.HomePhone = .Home_Phone
                If Not .IsLoss_DateNull Then Me.LossDate = .Loss_Date
                If Not .IsClaim_Paid_AmountNull Then Me.ClaimPaidAmount = .Claim_Paid_Amount
                If Not .IsBonus_TotalNull Then Me.BonusTotal = .Bonus_Total

            End With
        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("GetSvcClaims Invalid Parameters Error", DirectCast(ex, Assurant.ElitaPlus.Common.ElitaPlusException).Code, ex)
        End Try
    End Sub

    Protected Shadows Sub CheckDeleted()
    End Sub

#End Region

#Region "Public Members"

    Public Overrides Function ProcessWSRequest() As String
        Try
            Me.Validate()
            Dim oServiceCenterClaimsSearchData As New ClaimDAL.ServiceCenterClaimsSearchData
            If Not Me.ServiceCenterCode = Nothing Then
                Dim oServiceCenter As New ServiceCenter(Me.ServiceCenterCode)
                If oServiceCenter Is Nothing Then
                    Throw New BOValidationException("GetSvcClaims Error: ", Common.ErrorCodes.INVALID_SERVICE_CENTER_CODE)
                End If
                oServiceCenterClaimsSearchData.ServiceCenterId = oServiceCenter.Id
            End If

            If Not Me.CompanyCode = Nothing AndAlso GetCompanyId(Me.CompanyCode) = Nothing Then
                Throw New BOValidationException("GetPreInvoice Error: Invalid Company Code ", Assurant.ElitaPlus.Common.ErrorCodes.WS_INVALID_COMPANY_CODE)
            End If

            If Not Me.ClaimStatus = Nothing Then
                oServiceCenterClaimsSearchData.ClaimStatus = Me.ClaimStatus.ToUpper
            End If
            If Not Me.ClaimType = Nothing Then
                oServiceCenterClaimsSearchData.ClaimTypeIds = Me._claimTypeIds
            End If
            If Not Me.MethodOfRepair = Nothing Then
                oServiceCenterClaimsSearchData.MethodOfRepairIds = Me._methodOfRepairIds
            End If
            oServiceCenterClaimsSearchData.SortBy = Me.SortBy
            oServiceCenterClaimsSearchData.SortOrder = Me.SortOrder
            oServiceCenterClaimsSearchData.PageSize = Me.PageSize
            oServiceCenterClaimsSearchData.PageNumber = Me.PageNumber
            oServiceCenterClaimsSearchData.ClaimNumber = Me.ClaimNumber
            oServiceCenterClaimsSearchData.AuthorizationNumber = Me.AuthorizationNumber
            oServiceCenterClaimsSearchData.CertificateNumber = Me.CertificateNumber
            oServiceCenterClaimsSearchData.CustomerName = Me.CustomerName
            oServiceCenterClaimsSearchData.FromClaimCreatedDate = Me.FromClaimCreatedDate
            oServiceCenterClaimsSearchData.ToClaimCreatedDate = Me.ToClaimCreatedDate
            oServiceCenterClaimsSearchData.FromVisitDate = Me.FromVisitDate
            oServiceCenterClaimsSearchData.ToVisitDate = Me.ToVisitDate
            oServiceCenterClaimsSearchData.ClaimExtendedStatusIds = Me._ClaimExtendedStatusIds
            oServiceCenterClaimsSearchData.ClaimExtendedStatusOwnerCodeIds = Me._ClaimExtendedStatusOwnerIds
            oServiceCenterClaimsSearchData.TurnAroundTimeRangeCode = Me.TurnAroundTimeRangeCode
            oServiceCenterClaimsSearchData.BatchNumber = Me.BatchNumber
            oServiceCenterClaimsSearchData.SerialNumber = Me.SerialNumber
            oServiceCenterClaimsSearchData.WorkPhone = Me.WorkPhone
            oServiceCenterClaimsSearchData.CompanyCode = Me.CompanyCode
            oServiceCenterClaimsSearchData.HomePhone = Me.HomePhone
            oServiceCenterClaimsSearchData.LossDate = Me.LossDate
            oServiceCenterClaimsSearchData.ClaimPaidAmount = Me.ClaimPaidAmount
            oServiceCenterClaimsSearchData.BonusTotal = Me.BonusTotal
            Dim dsClaims As DataSet
            If Not Me.ServiceCenterCode = Nothing Then
                dsClaims = Claim.GetActiveClaimsForSvcGeneric(oServiceCenterClaimsSearchData, oServiceCenterClaimsSearchData.PageNumber = 1)
                dsClaims.DataSetName = "GetSvcClaims"

                If Not dsClaims Is Nothing AndAlso dsClaims.Tables.Count > 0 AndAlso dsClaims.Tables(0).Rows.Count > 0 Then
                    If oServiceCenterClaimsSearchData.PageNumber = 1 Then
                        If CType(dsClaims.Tables(0).Rows(0).Item("COUNT"), Integer) = 0 Then
                            dsClaims.Tables.Remove(ClaimDAL.TABLE_NAME_SVC_COUNT)
                        End If
                    End If
                    dsClaims.Tables(Me.TABLE_NAME_SVC).Columns.Remove("COUNT")
                End If
            Else

                dsClaims = Claim.GetActiveClaimsByClaimNumberorCertificate(oServiceCenterClaimsSearchData, oServiceCenterClaimsSearchData.PageNumber = 1)
                dsClaims.DataSetName = "GetSvcClaims"

                If Not dsClaims Is Nothing AndAlso dsClaims.Tables.Count > 0 AndAlso dsClaims.Tables(0).Rows.Count > 0 Then
                    If oServiceCenterClaimsSearchData.PageNumber = 1 Then
                        If CType(dsClaims.Tables(0).Rows(0).Item("COUNT"), Integer) = 0 Then
                            dsClaims.Tables.Remove(ClaimDAL.TABLE_NAME_SVC_COUNT)
                        End If
                    End If
                    dsClaims.Tables(Me.TABLE_NAME_SVC).Columns.Remove("COUNT")
                End If

            End If
            Return XMLHelper.FromDatasetToXML(dsClaims, Nothing, True, True, True, False, True)
            ' Return XMLHelper.FromDatasetToXML(dsClaims, Nothing, True, True, Nothing, True, )

        Catch ex As BOValidationException
            Throw ex
        Catch ex As StoredProcedureGeneratedException
            Throw ex
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As Exception
            Throw ex
            'Throw New DataBaseAccessException(DataBaseAccessException.DatabaseAccessErrorType.ReadErr, ex)
        End Try

    End Function

    Public Shared Function GetCompanyId(ByVal Companycode As String) As Guid
        Dim oUser As New User(ElitaPlusIdentity.Current.ActiveUser.NetworkId)
        Dim userAssignedCompaniesDv As DataView = oUser.GetSelectedAssignedCompanies(ElitaPlusIdentity.Current.ActiveUser.Id)
        Dim companyId As System.Guid = Guid.Empty

        For i = 0 To userAssignedCompaniesDv.Count - 1
            Dim oCompanyId As New Guid(CType(userAssignedCompaniesDv.Table.Rows(i)("COMPANY_ID"), Byte()))
            If Not oCompanyId = Nothing AndAlso userAssignedCompaniesDv.Table.Rows(i)("CODE").Equals(Companycode.ToUpper) Then
                companyId = oCompanyId
                Exit For
            End If
        Next

        If companyId.Equals(Guid.Empty) Then
            Return Nothing
        Else
            Return companyId
        End If
    End Function

#End Region

#Region "Properties"

    '<ValueMandatory(""), ValidStringLength("", Max:=50)> _
    <ValidStringLength("", Max:=50)> _
    Public Property ServiceCenterCode() As String
        Get
            CheckDeleted()
            If Row(COL_SERVICE_CENTER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_SERVICE_CENTER_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_SERVICE_CENTER_CODE, Value)
        End Set
    End Property

    '<ValueMandatory("")> _
    Public Property ClaimStatus() As String
        Get
            CheckDeleted()
            If Row(COL_CLAIM_STATUS) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_CLAIM_STATUS), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_CLAIM_STATUS, Value)
        End Set
    End Property
    '<ValueMandatory("")> _
    Public Property ClaimType() As String
        Get
            CheckDeleted()
            If Row(COL_CLAIM_TYPE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_CLAIM_TYPE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_CLAIM_TYPE, Value)
        End Set
    End Property
    '<ValueMandatory("")> _
    Public Property MethodOfRepair() As String
        Get
            CheckDeleted()
            If Row(COL_METHOD_OF_REPAIR) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_METHOD_OF_REPAIR), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_METHOD_OF_REPAIR, Value)
        End Set
    End Property
    <ValueMandatory("")> _
    Public Property SortBy() As Integer
        Get
            CheckDeleted()
            If Row(COL_SORT_BY) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_SORT_BY), String)
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            Me.SetValue(COL_SORT_BY, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property SortOrder() As Integer
        Get
            CheckDeleted()
            If Row(COL_SORT_ORDER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_SORT_ORDER), String)
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            Me.SetValue(COL_SORT_ORDER, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property PageSize() As Integer
        Get
            CheckDeleted()
            If Row(COL_PAGE_SIZE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_PAGE_SIZE), String)
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            Me.SetValue(COL_PAGE_SIZE, Value)
        End Set
    End Property

    <ValueMandatory("")> _
    Public Property PageNumber() As Integer
        Get
            CheckDeleted()
            If Row(COL_PAGE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_PAGE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As Integer)
            CheckDeleted()
            Me.SetValue(COL_PAGE_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)>
    Public Property ClaimNumber() As String
        Get
            CheckDeleted()
            If Row(COL_CLAIM_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_CLAIM_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_CLAIM_NUMBER, Value)
        End Set
    End Property
    <ValidStringLength("", Max:=10)> _
    Public Property AuthorizationNumber() As String
        Get
            CheckDeleted()
            If Row(COL_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=20)> _
    Public Property CertificateNumber() As String
        Get
            CheckDeleted()
            If Row(COL_CERTIFICATE_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_CERTIFICATE_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_CERTIFICATE_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=50)> _
    Public Property CustomerName() As String
        Get
            CheckDeleted()
            If Row(COL_CUSTOMER_NAME) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_CUSTOMER_NAME), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_CUSTOMER_NAME, Value)
        End Set
    End Property


    Public Property FromClaimCreatedDate() As DateType
        Get
            CheckDeleted()
            If Row(COL_FROM_CLAIM_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_FROM_CLAIM_CREATED_DATE), DateTime)
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(COL_FROM_CLAIM_CREATED_DATE, Value)
        End Set
    End Property

    Public Property ToClaimCreatedDate() As DateType
        Get
            CheckDeleted()
            If Row(COL_TO_CLAIM_CREATED_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_TO_CLAIM_CREATED_DATE), DateTime)
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(COL_TO_CLAIM_CREATED_DATE, Value)
        End Set
    End Property

    Public Property FromVisitDate() As DateType
        Get
            CheckDeleted()
            If Row(COL_FROM_VISIT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_FROM_VISIT_DATE), DateTime)
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(COL_FROM_VISIT_DATE, Value)
        End Set
    End Property

    Public Property ToVisitDate() As DateType
        Get
            CheckDeleted()
            If Row(COL_TO_VISIT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_TO_VISIT_DATE), DateTime)
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(COL_TO_VISIT_DATE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=250)> _
    Public Property ClaimExtendedStatusOwnerCode() As String
        Get
            CheckDeleted()
            If Row(COL_CLAIM_EXTENDED_STATUS_OWNER_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_CLAIM_EXTENDED_STATUS_OWNER_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_CLAIM_EXTENDED_STATUS_OWNER_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=250)> _
    Public Property ClaimExtendedStatusCode() As String
        Get
            CheckDeleted()
            If Row(COL_CLAIM_EXTENDED_STATUS_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_CLAIM_EXTENDED_STATUS_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_CLAIM_EXTENDED_STATUS_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)> _
    Public Property TurnAroundTimeRangeCode() As String
        Get
            CheckDeleted()
            If Row(COL_TURN_AROUND_TIME_RANGE_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_TURN_AROUND_TIME_RANGE_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_TURN_AROUND_TIME_RANGE_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=10)> _
    Public Property BatchNumber() As String
        Get
            CheckDeleted()
            If Row(COL_BATCH_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_BATCH_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_BATCH_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=30)> _
    Public Property SerialNumber() As String
        Get
            CheckDeleted()
            If Row(COL_SERIAL_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_SERIAL_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_SERIAL_NUMBER, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)> _
    Public Property WorkPhone() As String
        Get
            CheckDeleted()
            If Row(COL_WORK_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_WORK_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_WORK_PHONE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=5)> _
    Public Property CompanyCode() As String
        Get
            CheckDeleted()
            If Row(COL_COMPANY_CODE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_COMPANY_CODE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_COMPANY_CODE, Value)
        End Set
    End Property

    <ValidStringLength("", Max:=15)> _
    Public Property HomePhone() As String
        Get
            CheckDeleted()
            If Row(COL_HOME_PHONE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_HOME_PHONE), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_HOME_PHONE, Value)
        End Set
    End Property

    Public Property LossDate() As DateType
        Get
            CheckDeleted()
            If Row(COL_LOSS_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_LOSS_DATE), DateTime)
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(COL_LOSS_DATE, Value)
        End Set
    End Property

    Public Property ClaimPaidAmount() As String
        Get
            CheckDeleted()
            If Row(COL_CLAIM_PAID_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_CLAIM_PAID_AMOUNT), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_CLAIM_PAID_AMOUNT, Value)
        End Set
    End Property

    Public Property BonusTotal() As String
        Get
            CheckDeleted()
            If Row(COL_BONUS_TOTAL) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(COL_BONUS_TOTAL), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(COL_BONUS_TOTAL, Value)
        End Set
    End Property
#End Region

End Class
