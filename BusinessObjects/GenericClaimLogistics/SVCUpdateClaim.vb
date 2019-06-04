Imports Assurant.ElitaPlus.BusinessObjects.Common
Imports System.Text
Imports Assurant.ElitaPlus.BusinessObjectData.Common

Public Class SVCUpdateClaim
    Inherits BusinessObjectBase

#Region "Member Variables"
    Private IsVisitDateNull As Boolean = True
    Private IsRepairDateNull As Boolean = True
    Private IsShippingAmountNull As Boolean = True
    Private IsLaborAmountNull As Boolean = True
    Private IsServiceChargeAmountNull As Boolean = True
    Private IsTripAmountNull As Boolean = True
    Private IsOtherAmountNull As Boolean = True
    Private IsOtherDescriptionNull As Boolean = True
    Private IsAuthorizationNumberNull As Boolean = True

    Private PartsDataHasChanged As Boolean = False

    Private _PartsAmount As Decimal = 0
    'Private IsStatusCodeNull As Boolean = True
    'Private dsItemCoverages As DataSet
    'Private dsCoverageInfo As DataSet
    Private _claimBo As Claim = Nothing
    Private _claimID As Guid = Guid.Empty
    Private _PartsInfoDV As DataView = Nothing
    
    Private ds1 As SVCUpdateClaimDs
#End Region

#Region "Constants"

    Public TABLE_NAME As String = "SVCUpdateClaim"
    Private Const TABLE_NAME_PARTS_LIST As String = "Parts_List"
    Private Const TABLE_NAME_CLAIM_EXTENDED_STATUS As String = "Claim_Extended_Status_List"

    'Public Const INSERT_FAILED As String = "ERR_INSERT_FAILED"
    'Public Const WEB_SERVICE_CALL_FAILED As String = "WEB_SERVICE_CALL_FAILED"
    'Private Const TABLE_RESULT As String = "RESULT"
    'Private Const VALUE_OK As String = "OK"
    'Private Const REASON_CODE_OPEN_IN_ERROR = "OERR"

    'Public TABLE_NAME_COVERAGE As String = "COVERAGES"
    ' Public TABLE_NAME_COVERAGE_INFO As String = "COVERAGES_INFO"


    'Private Const MSG_RECORD_NOT_SAVED As String = "MSG_RECORD_NOT_SAVED"
    'Private Const CERTIFICATE_NOT_FOUND As String = "ERR_CERTIFICATE_NOT_FOUND"
    'Private Const INVALID_SERVICE_CENTER_CODE As String = "INVALID_SERVICE_CENTER_ERR"
    'Private Const ERR_REASON_CLOSED_CODE_REQUIRED As String = "ERR_REASON_CLOSED_CODE_REQUIRED"
    'Private Const ERR_REASON_CLOSED_CODE_NOT_FOUND As String = "ERR_REASON_CLOSED_CODE_NOT_FOUND"
    'Private Const ERR_STATUS_CODE_AND_REASON_CLOSED_CODE_CONFLICT As String = "ERR_STATUS_CODE_AND_REASON_CLOSED_CODE_CONFLICT"
    'Private Const ERR_SERVICE_CENTER_CODE_NOT_UPDATABLE As String = "ERR_SERVICE_CENTER_CODE_NOT_UPDATABLE"
    'Private Const INVALID_REPAIR_CODE As String = "INVALID_REPAIR_CODE_ERR"
    'Private Const INVALID_CAUSE_OF_LOSS_CODE As String = "INVALID_CAUSE_OF_LOSS_CODE_ERR"
    'Private Const INVALID_STATUS_CODE As String = "INVALID_STATUS_CODE"
    'Private Const INVALID_AMOUNT As String = "INVALID_AMOUNT"

    'Private Const CERTIFICATE_COVERAGES_NOT_FOUND As String = "ERR_CERTIFICATE_COVERAGES_NOT_FOUND"
    'Private Const ERROR_ACCESSING_DATABASE As String = "ERR_ACCESSING_DATABASE"

    Private Const SOURCE_COL_NAME_CLAIM_ID As String = "Claim_ID"
    Private Const SOURCE_COL_VISIT_DATE As String = "VISIT_DATE"
    Private Const SOURCE_COL_REPAIR_DATE As String = "REPAIR_DATE"
    Private Const SOURCE_COL_SHIPPING_AMOUNT As String = "SHIPPING_AMOUNT"
    Private Const SOURCE_COL_LABOR_AMOUNT As String = "LABOR_AMOUNT"
    Private Const SOURCE_COL_SERVICE_CHARGE_AMOUNT As String = "SERVICE_CHARGE_AMOUNT"
    Private Const SOURCE_COL_TRIP_AMOUNT As String = "TRIP_AMOUNT"
    Private Const SOURCE_COL_OTHER_AMOUNT As String = "OTHER_AMOUNT"
    Private Const SOURCE_COL_OTHER_DESCRIPTION As String = "OTHER_DESCRIPTION"
    Private Const SOURCE_COL_AUTHORIZATION_NUMBER As String = "AUTHORIZATION_NUMBER"




    'Private Const SOURCE_COL_STATUS_CODE As String = "STATUS_CODE"
    'Private Const SOURCE_COL_SERVICE_CENTER_CODE As String = "SERVICE_CENTER_CODE"

    'Private Const SOURCE_COL_PROBLEM_DESCRIPTION As String = "PROBLEM_DESCRIPTION"
    'Private Const SOURCE_COL_SPECIAL_INSTRUCTION As String = "SPECIAL_INSTRUCTION"
    'Private Const SOURCE_COL_ASSURANT_PAY_AMOUNT As String = "ASSURANT_PAY_AMOUNT"
    'Private Const SOURCE_COL_LIABILITY_LIMIT As String = "LIABILITY_LIMIT"

    'Private Const SOURCE_COL_PICK_UP_DATE As String = "PICK_UP_DATE"
    'Private Const SOURCE_COL_CERT_ITEM_COVERAGE_ID As String = "CERT_ITEM_COVERAGE_ID"
    'Private Const SOURCE_COL_COVERAGE_CODE = "COVERAGE_CODE"
    'Private Const SOURCE_COL_DEDUCTIBLE = "DEDUCTIBLE"
    'Private Const SOURCE_COL_EXIST_IN_ELITA = "EXIST_IN_ELITA"
    'Private Const SOURCE_COL_CLAIM_ID = "CLAIM_ID"
    'Private Const SOURCE_COL_UNIT_NUMBER As String = "UNIT_NUMBER"
    'Private Const SOURCE_COL_CAUSE_OF_LOSS_CODE As String = "CAUSE_OF_LOSS_CODE"
    'Private Const SOURCE_COL_LOSS_DATE As String = "LOSS_DATE"
    'Private Const CLAIM_NUMBER_OFFSET As Integer = 50

    ''Added for Def-1782
    'Public Const SOURCE_COL_INVOICE_DATE As String = "INVOICE_DATE"
    'REQ-5615
    Public Const YES As String = "Y"
    Public Const COMP_ATTR_BLOCK_PAY_INVOICE As String = "BLOCK_PAY_INVOICE"

#End Region

#Region "Constructors"

    Public Sub New(ByVal ds As SVCUpdateClaimDs)
        MyBase.New()

        ' dsCoverageInfo = New DataSet
        'Dim dt As DataTable = New DataTable(TABLE_NAME_COVERAGE_INFO)
        'dt.Columns.Add(SOURCE_COL_CERT_ITEM_COVERAGE_ID, GetType(Guid))
        'dt.Columns.Add(SOURCE_COL_ASSURANT_PAY_AMOUNT, GetType(Decimal))
        'dt.Columns.Add(SOURCE_COL_CLAIM_NUMBER, GetType(String))
        'dt.Columns.Add(SOURCE_COL_COVERAGE_CODE, GetType(String))
        'dt.Columns.Add(SOURCE_COL_DEDUCTIBLE, GetType(Decimal))
        'dsCoverageInfo.Tables.Add(dt)

        MapDataSet(ds)
        ValidateInput(ds)
        'Load(ds)
        ds1 = ds
    End Sub

#End Region

#Region "Properties"

    <ValueMandatory("")> _
    Public Property ClaimIDString() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_NAME_CLAIM_ID) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_NAME_CLAIM_ID), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_NAME_CLAIM_ID, Value)
        End Set
    End Property

    Public Property VisitDate() As DateType
        Get
            CheckDeleted()
            If Row(SOURCE_COL_VISIT_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_VISIT_DATE), DateTime)
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_VISIT_DATE, Value)
        End Set
    End Property

    Public Property RepairDate() As DateType
        Get
            CheckDeleted()
            If Row(SOURCE_COL_REPAIR_DATE) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_REPAIR_DATE), DateTime)
            End If
        End Get
        Set(ByVal Value As DateType)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_REPAIR_DATE, Value)
        End Set
    End Property

    Public Property ShippingAmount() As Decimal
        Get
            CheckDeleted()
            If Row(SOURCE_COL_SHIPPING_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_SHIPPING_AMOUNT), Decimal)
            End If
        End Get
        Set(ByVal Value As Decimal)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_SHIPPING_AMOUNT, Value)
        End Set
    End Property

    Public Property LaborAmount() As Decimal
        Get
            CheckDeleted()
            If Row(SOURCE_COL_LABOR_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_LABOR_AMOUNT), Decimal)
            End If
        End Get
        Set(ByVal Value As Decimal)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_LABOR_AMOUNT, Value)
        End Set
    End Property

    Public Property ServiceChargeAmount() As Decimal
        Get
            CheckDeleted()
            If Row(SOURCE_COL_SERVICE_CHARGE_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_SERVICE_CHARGE_AMOUNT), Decimal)
            End If
        End Get
        Set(ByVal Value As Decimal)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_SERVICE_CHARGE_AMOUNT, Value)
        End Set
    End Property

    Public Property TripAmount() As Decimal
        Get
            CheckDeleted()
            If Row(SOURCE_COL_TRIP_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_TRIP_AMOUNT), Decimal)
            End If
        End Get
        Set(ByVal Value As Decimal)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_TRIP_AMOUNT, Value)
        End Set
    End Property

    Public Property OtherAmount() As Decimal
        Get
            CheckDeleted()
            If Row(SOURCE_COL_OTHER_AMOUNT) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_OTHER_AMOUNT), Decimal)
            End If
        End Get
        Set(ByVal Value As Decimal)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_OTHER_AMOUNT, Value)
        End Set
    End Property

    Public Property OtherDescription() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_OTHER_DESCRIPTION) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_OTHER_DESCRIPTION), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_OTHER_DESCRIPTION, Value)
        End Set
    End Property

    Public Property AuthorizationNumber() As String
        Get
            CheckDeleted()
            If Row(SOURCE_COL_AUTHORIZATION_NUMBER) Is DBNull.Value Then
                Return Nothing
            Else
                Return CType(Row(SOURCE_COL_AUTHORIZATION_NUMBER), String)
            End If
        End Get
        Set(ByVal Value As String)
            CheckDeleted()
            Me.SetValue(SOURCE_COL_AUTHORIZATION_NUMBER, Value)
        End Set
    End Property

#End Region

#Region "Extended Properties"

    Private Property ClaimBO() As Claim
        Get
            If Me._claimBo Is Nothing Then
                If Not Me.ClaimIDString.Equals(String.Empty) AndAlso Me.ClaimIDString.Length = 32 Then
                    Me._claimID = GuidControl.ByteArrayToGuid(GuidControl.HexToByteArray(Me.ClaimIDString))
                    'PickupListHeader.GetClaimIDByCode(Me.ClaimNumber, Me.CertItemCoverageCode)

                    If Me._claimID.Equals(Guid.Empty) Then
                        Throw New BOValidationException("SVCUpdateClaim Error: ", Common.ErrorCodes.INVALID_CLAIM_NOT_FOUND)
                    End If
                    Me._claimBo = ClaimFacade.Instance.GetClaim(Of Claim)(Me._claimID)
                Else
                    Throw New BOValidationException("SVCUpdateClaim Error: ", Common.ErrorCodes.INVALID_CLAIM_NOT_FOUND)
                End If
            End If

            Return Me._claimBo
        End Get
        Set(ByVal value As Claim)
            Me._claimBo = value
        End Set
    End Property

#End Region

#Region "Member Methods"

    Private Sub ValidateInput(ByVal ds As SVCUpdateClaimDs)

        ' ''With ds.SVCUpdateClaim.Item(0)

        ' ''    If .Claim_ID Then
        ' ''        If .IsClaim_NumberNull AndAlso .IsCompany_CodeNull Then
        ' ''            Throw New BOValidationException("GetClaimDetail Error: ", Common.ErrorCodes.WS_CLAIM_ID_OR_CLAIM_NUMBER_IS_REQUIRED)
        ' ''        End If
        ' ''    End If

        ' ''End With

    End Sub
    Protected Sub CheckClaimPaymentInProgress(ByVal Id As Guid)
        Dim ds As New DataSet
        Dim blockInvoice As String
        Dim oCompany As New Company(Me.ClaimBO.Company.Id)
        Dim oDealer As New Dealer(Me.ClaimBO.Dealer.Id)
        
        'Check the flag at Company level
        If (oCompany.AttributeValues.Contains(COMP_ATTR_BLOCK_PAY_INVOICE)) Then
            blockInvoice = oCompany.AttributeValues.Value(COMP_ATTR_BLOCK_PAY_INVOICE)
        End If

        If (blockInvoice = YES) Then
            If Claim.CheckClaimPaymentInProgress(Id, Me.ClaimBO.Company.CompanyGroupId) Then
                Throw New BOValidationException("CLAIM_PROCESS_IN_PROGRESS_ERR", Assurant.ElitaPlus.Common.ErrorCodes.CLAIM_PROCESS_IN_PROGRESS_ERR)
            End If
        End If
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As SVCUpdateClaimDs, ByVal pos As Integer)
        Try
            If ds.SVCUpdateClaim.Count = 0 Then Exit Sub
            With ds.SVCUpdateClaim.Item(pos)
                Me.ClaimIDString = .Claim_ID ' GUID value

                CheckClaimPaymentInProgress(Me.ClaimBO.Id)

                If Not .IsVisit_DateNull Then
                    Me.VisitDate = .Visit_Date
                    IsVisitDateNull = False
                End If
                If Not .IsRepair_DateNull Then
                    Me.RepairDate = .Repair_Date
                    IsRepairDateNull = False
                End If

                If Not .IsShipping_AmountNull Then
                    Me.ShippingAmount = .Shipping_Amount
                    IsShippingAmountNull = False
                End If

                If Not .IsLabor_AmountNull Then
                    Me.LaborAmount = .Labor_Amount
                    IsLaborAmountNull = False
                End If

                If Not .IsService_Charge_AmountNull Then
                    Me.ServiceChargeAmount = .Service_Charge_Amount
                    IsServiceChargeAmountNull = False
                End If

                If Not .IsTrip_AmountNull Then
                    Me.TripAmount = .Trip_Amount
                    IsTripAmountNull = False
                End If

                If Not .IsOther_AmountNull Then
                    Me.OtherAmount = .Other_Amount
                    IsOtherAmountNull = False
                End If

                If Not .IsOther_DescriptionNull Then
                    Me.OtherDescription = .Other_Description
                    IsOtherDescriptionNull = False
                End If

                If Not .IsAuthorization_NumberNull Then
                    Me.AuthorizationNumber = .Authorization_Number
                    IsAuthorizationNumberNull = False
                End If

                ' Populate Parts_List
                Dim i As Integer
                Dim j As Integer
                Dim PartCode As String = String.Empty
                Dim PartAmount As Decimal = Nothing
                Dim InStock As String = String.Empty
                
                Me._PartsInfoDV = PartsInfo.getSelectedList(Me.ClaimBO.Id)
                'delet existing claim parts
                Me.DeleteExistingParts()

               For j = 0 To ds.Parts_List.Count - 1
                    With ds.Parts_List(j)
                        If ds.SVCUpdateClaim(pos).SVCUpdateClaim_Id.Equals(ds.Parts_List(j).SVCUpdateClaim_Id) Then
                            PartCode = .Part_Code
                            PartAmount = .Part_Amount
                            If Not .IsIn_StockNull Then InStock = .In_Stock

                            NewPartsList(PartCode, PartAmount, InStock, .IsIn_StockNull, Me.ClaimBO.Id)
                            PartCode = Nothing
                            PartAmount = Nothing
                            InStock = Nothing
                        End If
                    End With
                Next

                ' Populate Claim Extended Status                
                Dim ClaimExtendedStatusCode As String = String.Empty
                Dim ClaimExtendedStatusDate As Date = Nothing
                Dim ClaimExtendedStatusComment As String = String.Empty

                For j = 0 To ds.Claim_Extended_Status_List.Count - 1
                    With ds.Claim_Extended_Status_List(j)
                        If ds.SVCUpdateClaim(pos).SVCUpdateClaim_Id.Equals(ds.Claim_Extended_Status_List(j).SVCUpdateClaim_Id) Then
                            ClaimExtendedStatusCode = .Claim_Extended_Status_Code
                            ClaimExtendedStatusDate = .Claim_Extended_Status_Date
                            If Not .IsClaim_Extended_Status_CommentNull Then ClaimExtendedStatusComment = .Claim_Extended_Status_Comment

                            NewClaimExtendedStatus(ClaimExtendedStatusCode, ClaimExtendedStatusDate, ClaimExtendedStatusComment, .IsClaim_Extended_Status_CommentNull)
                        End If
                    End With
                    ClaimExtendedStatusCode = Nothing
                    ClaimExtendedStatusDate = Nothing
                    ClaimExtendedStatusComment = Nothing
                Next

            End With

        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub PopulateBOFromWebService(ByVal ds As SVCUpdateClaimDs)
        Try
            'Dim j As Integer
            If ds.SVCUpdateClaim.Count = 0 Then Exit Sub
            'For j = 0 To ds.SVCUpdateClaim.Count - 1
            With ds.SVCUpdateClaim.Item(0)
                Me.ClaimIDString = .Claim_ID ' GUID value

                If Not .IsVisit_DateNull Then
                    Me.VisitDate = .Visit_Date
                    IsVisitDateNull = False
                End If
                If Not .IsRepair_DateNull Then
                    Me.RepairDate = .Repair_Date
                    IsRepairDateNull = False
                End If

                If Not .IsShipping_AmountNull Then
                    Me.ShippingAmount = .Shipping_Amount
                    IsShippingAmountNull = False
                End If

                If Not .IsLabor_AmountNull Then
                    Me.LaborAmount = .Labor_Amount
                    IsLaborAmountNull = False
                End If

                If Not .IsService_Charge_AmountNull Then
                    Me.ServiceChargeAmount = .Service_Charge_Amount
                    IsServiceChargeAmountNull = False
                End If

                If Not .IsTrip_AmountNull Then
                    Me.TripAmount = .Trip_Amount
                    IsTripAmountNull = False
                End If

                If Not .IsOther_AmountNull Then
                    Me.OtherAmount = .Other_Amount
                    IsOtherAmountNull = False
                End If

                If Not .IsOther_DescriptionNull Then
                    Me.OtherDescription = .Other_Description
                    IsOtherDescriptionNull = False
                End If

                If Not .IsAuthorization_NumberNull Then
                    Me.AuthorizationNumber = .Authorization_Number
                    IsAuthorizationNumberNull = False
                End If

                ' Populate Parts_List
                Dim i As Integer
                Dim PartCode As String = String.Empty
                Dim PartAmount As Decimal = Nothing
                Dim InStock As String = String.Empty

                Me._PartsInfoDV = PartsInfo.getSelectedList(Me.ClaimBO.Id)
                'delet existing claim parts
                Me.DeleteExistingParts()

                For i = 0 To ds.Parts_List.Count - 1
                    With ds.Parts_List(i)
                        PartCode = .Part_Code
                        PartAmount = .Part_Amount
                        If Not .IsIn_StockNull Then InStock = .In_Stock

                        NewPartsList(PartCode, PartAmount, InStock, .IsIn_StockNull, Me.ClaimBO.Id)

                        PartCode = Nothing
                        PartAmount = Nothing
                        InStock = Nothing
                    End With
                Next

                ' Populate Claim Extended Status                
                Dim ClaimExtendedStatusCode As String = String.Empty
                Dim ClaimExtendedStatusDate As Date = Nothing
                Dim ClaimExtendedStatusComment As String = String.Empty

                For i = 0 To ds.Claim_Extended_Status_List.Count - 1
                    With ds.Claim_Extended_Status_List(i)
                        ClaimExtendedStatusCode = .Claim_Extended_Status_Code
                        ClaimExtendedStatusDate = .Claim_Extended_Status_Date
                        If Not .IsClaim_Extended_Status_CommentNull Then ClaimExtendedStatusComment = .Claim_Extended_Status_Comment

                        NewClaimExtendedStatus(ClaimExtendedStatusCode, ClaimExtendedStatusDate, ClaimExtendedStatusComment, .IsClaim_Extended_Status_CommentNull)
                    End With
                    ClaimExtendedStatusCode = Nothing
                    ClaimExtendedStatusDate = Nothing
                    ClaimExtendedStatusComment = Nothing
                Next
            End With


        Catch ex As BOValidationException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub NewPartsList(ByVal PartCode As String, ByVal PartAmount As Decimal, ByVal InStock As String, ByVal IsInStockNull As Boolean, ByVal ClaimId As Guid)
        Dim objPartDescriptionId As Guid = PartsDescription.GetPartDescriptionByCode(PartCode, ClaimId)
        If objPartDescriptionId.Equals(Guid.Empty) Then
            Throw New BOValidationException("UpdateClaimData Error: ", Common.ErrorCodes.INVALID_PART_CODE)
        End If


        Dim oPartInfo As PartsInfo = Me.ClaimBO.AddPartsInfo(Guid.Empty)
        oPartInfo.PartsDescriptionId = objPartDescriptionId
        oPartInfo.ClaimId = Me.ClaimBO.Id
        oPartInfo.Cost = PartAmount
        If Not IsInStockNull Then
            oPartInfo.InStockID = LookupListNew.GetIdFromCode(LookupListCache.LK_YESNO, InStock)
        End If
        Try
            oPartInfo.Validate()
            Me._PartsAmount += oPartInfo.Cost.Value
        Catch ex As BOValidationException
            Throw New BOValidationException(ex.Message, ex.Code)
        End Try

        PartsDataHasChanged = True

    End Sub

    Private Sub NewClaimExtendedStatus(ByVal Claim_Extended_Status_Code As String, ByVal Claim_Extended_Status_Date As DateTimeType, ByVal Claim_Extended_Status_Comment As String, ByVal IsClaim_Extended_Status_CommentNull As Boolean)
        Dim oClaimStatus As ClaimStatus = Nothing
        Dim ClaimStatusByGroupID As Guid
        ClaimStatusByGroupID = ClaimStatusByGroup.GetClaimStatusByGroupID(Claim_Extended_Status_Code)
        If ClaimStatusByGroupID.Equals(Guid.Empty) Then
            Throw New BOValidationException("UpdateClaimData Error: ", Common.ErrorCodes.INVALID_CLAIM_EXTENDED_STATUS_CODE)
        End If

        oClaimStatus = Me.ClaimBO.AddExtendedClaimStatus(Guid.Empty)
        oClaimStatus.ClaimId = Me.ClaimBO.Id
        oClaimStatus.ClaimStatusByGroupId = ClaimStatusByGroupID
        oClaimStatus.StatusDate = Claim_Extended_Status_Date
        If Not IsClaim_Extended_Status_CommentNull Then oClaimStatus.Comments = Claim_Extended_Status_Comment
        Try
            oClaimStatus.Validate()
        Catch ex As BOValidationException
            Throw New BOValidationException(ex.Message, ex.Code)
        End Try
        'Dim newRow As DataRow = Dataset.Tables(TABLE_NAME_CLAIM_EXTENDED_STATUS).NewRow
        'newRow(0) = Claim_Extended_Status_Code
        'newRow(1) = Claim_Extended_Status_Date
        'newRow(2) = Claim_Extended_Status_Comment
        'Dataset.Tables(TABLE_NAME_CLAIM_EXTENDED_STATUS).Rows.Add(newRow)

    End Sub

    Private Sub DeleteExistingParts()
        Dim i As Integer

        For i = 0 To _PartsInfoDV.Count - 1
            Dim objPartInfoId As Guid = New Guid(CType(_PartsInfoDV.Item(i)(PartsInfoDAL.COL_NAME_PARTS_INFO_ID), Byte()))
            Dim objPartInfo As PartsInfo = Me.ClaimBO.AddPartsInfo(objPartInfoId)
            objPartInfo.Delete()
        Next
    End Sub

    Public Function SetClaimBOProperties()
        If Not Me.IsRepairDateNull Then
            Me.ClaimBO.RepairDate = Me.RepairDate
        End If

        If Not Me.IsVisitDateNull Then
            Me.ClaimBO.VisitDate = Me.VisitDate
        End If

        If Not Me.IsRepairDateNull Then
            Me.ClaimBO.RepairDate = Me.RepairDate
        End If

        If Not Me.IsAuthorizationNumberNull Then
            Dim Cnt As Integer
            Me.ClaimBO.AuthorizationNumber = Me.AuthorizationNumber
            Cnt = Me.ClaimBO.IsClaimActive(Me.ClaimBO.Id)

            If Cnt = 0 Then
                Throw New BOValidationException("UpdateClaimData Error: ", Common.ErrorCodes.INVALID_CLAIM_NOT_ACTIVE)
            End If

            Cnt = Me.ClaimBO.IsClaimAuthNumberExists(Me.ClaimBO.Id, Me.AuthorizationNumber)

            If Cnt > 0 Then
                Throw New BOValidationException("UpdateClaimData Error: ", Common.ErrorCodes.INVALID_DUPLICATE_AUTHORIZATION_NUMBER)
            End If
        End If

        If Not Me.IsShippingAmountNull Or Not Me.IsLaborAmountNull Or Not Me.IsServiceChargeAmountNull Or Not Me.IsTripAmountNull Or Not Me.IsOtherAmountNull Then
            Dim objClaimAuthDetail As ClaimAuthDetail
            'This claim may/may not have a claimAuthDetail record.
            Try
                objClaimAuthDetail = Me.ClaimBO.AddClaimAuthDetail(Me.ClaimBO.Id, True)
            Catch ex As Exception
                objClaimAuthDetail = Me.ClaimBO.AddClaimAuthDetail(Guid.Empty)
                objClaimAuthDetail.ClaimId = Me.ClaimBO.Id
            End Try

            If Not Me.IsShippingAmountNull Then
                objClaimAuthDetail.ShippingAmount = Me.ShippingAmount
            End If
            If Not Me.IsLaborAmountNull Then
                objClaimAuthDetail.LaborAmount = Me.LaborAmount
            End If
            If Not Me.IsServiceChargeAmountNull Then
                objClaimAuthDetail.ServiceCharge = Me.ServiceChargeAmount
            End If
            If Not Me.IsTripAmountNull Then
                objClaimAuthDetail.TripAmount = Me.TripAmount
            End If
            If Not Me.IsOtherAmountNull Then
                objClaimAuthDetail.OtherAmount = Me.OtherAmount
            End If
            If Not Me.IsOtherDescriptionNull Then
                objClaimAuthDetail.OtherExplanation = Me.OtherDescription
            End If

            objClaimAuthDetail.PartAmount = Me._PartsAmount

            objClaimAuthDetail.Validate()
            Me.ClaimBO.AuthDetailDataHasChanged = True
        End If

        If Me.ClaimBO.AuthDetailDataHasChanged = False Then
            ClaimBO.AuthDetailDataHasChanged = Me.PartsDataHasChanged
        End If
    End Function

    Public Overrides Function ProcessWSRequest() As String
        Dim selfThrownException As Boolean = False
        Dim row As DataRow
        Dim claimFamilyBO As Claim = Nothing
        Dim processed_count As Integer = 0
        Dim error_count As Integer = 0
        Dim respXmlDs As DataSet = New DataSet("SVCUpdateClaimResponse")
        Dim respXmlTable As DataTable = New DataTable("UpdateResult")
        Dim respXmlErrorTable As DataTable = New DataTable("Error")
        respXmlTable.Columns.Add("ClaimId")
        respXmlTable.Columns.Add("Updated")
        respXmlErrorTable.Columns.Add("ClaimId")
        respXmlErrorTable.Columns.Add("Code")
        respXmlErrorTable.Columns.Add("Message")

        Try
            For i = 0 To ds1.SVCUpdateClaim.Count - 1
                Try
                    Load(ds1, i)
                    Me.Validate()
                    SetClaimBOProperties()
                    Me.ClaimBO.Save()
                    respXmlTable.Rows.Add(Me.ClaimIDString, True)
                    'processed_count += 1
                Catch ex As BOValidationException
                    If Not ds1.SVCUpdateClaim.Count > 1 Then
                        Throw New BOValidationException(ex.Message, ex.Code)
                    Else
                        respXmlTable.Rows.Add(Me.ClaimIDString, False)
                        respXmlErrorTable.Rows.Add(Me.ClaimIDString, ex.Code, ex.Message)
                    End If


                Catch ex As DALConcurrencyAccessException
                    If Not ds1.SVCUpdateClaim.Count > 1 Then
                        Throw ex
                    Else
                        respXmlTable.Rows.Add(Me.ClaimIDString, False)
                        respXmlErrorTable.Rows.Add(Me.ClaimIDString, ex.Code, ex.Message)
                    End If

                Catch ex As DataBaseUniqueKeyConstraintViolationException
                    If Not ds1.SVCUpdateClaim.Count > 1 Then
                        Throw ex
                    Else
                        respXmlTable.Rows.Add(Me.ClaimIDString, False)
                        respXmlErrorTable.Rows.Add(Me.ClaimIDString, ex.Code, ex.Message)
                    End If

                Catch ex As Exception
                    If Not ds1.SVCUpdateClaim.Count > 1 Then
                        Throw ex
                    Else
                        respXmlTable.Rows.Add(Me.ClaimIDString, False)
                        respXmlErrorTable.Rows.Add(Me.ClaimIDString, "Unexpected Error", ex.Message)
                    End If

                    'error_count += 1
                Finally
                    Me.ClaimBO = Nothing
                End Try

            Next

            If ds1.SVCUpdateClaim.Count > 1 Then
                respXmlDs.Tables.Add(respXmlTable)
                respXmlDs.Tables.Add(respXmlErrorTable)
                respXmlDs.Relations.Add(respXmlDs.Tables("UpdateResult").Columns("ClaimId"), _
                                        respXmlDs.Tables("Error").Columns("ClaimId"))
                Return XMLHelper.FromDatasetToXML(respXmlDs)
            Else
                Return XMLHelper.GetXML_OK_Response
            End If

        Catch ex As Exception
            Throw ex
        Finally
            Me.ClaimBO = Nothing
        End Try


    End Function

    Private Sub MapDataSet(ByVal ds As SVCUpdateClaimDs)

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

    Private Sub Load(ByVal ds As SVCUpdateClaimDs, ByVal pos As Integer)
        Try
            Initialize()
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds, pos)
            Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As BOValidationException
            Throw New BOValidationException(ex.Message, ex.Code)
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Claim Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub
    Private Sub Load(ByVal ds As SVCUpdateClaimDs)
        Try
            Initialize()
            Dim newRow As DataRow = Me.Dataset.Tables(TABLE_NAME).NewRow
            Me.Row = newRow
            PopulateBOFromWebService(ds)
            Me.Dataset.Tables(TABLE_NAME).Rows.Add(newRow)
        Catch ex As Assurant.ElitaPlus.DALObjects.DataBaseAccessException
            Throw ex
        Catch ex As BOValidationException
            Throw New BOValidationException(ex.Message, ex.Code)
        Catch ex As ElitaPlusException
            Throw ex
        Catch ex As Exception
            Throw New ElitaPlusException("Claim Loading Data", Common.ErrorCodes.UNEXPECTED_ERROR)
        End Try
    End Sub
    'Initialization code for new objects
    Private Sub Initialize()
    End Sub

    'Shadow the checkdeleted method so we don't validate like the DB objects
    Protected Shadows Sub CheckDeleted()
    End Sub

    Public Function IsValidFollowupDate(ByVal claimBO As Claim) As Boolean
        Dim obj As Claim = claimBO

        If ((obj.FollowupDate Is Nothing) OrElse _
            (obj.StatusCode = Codes.CLAIM_STATUS__CLOSED) OrElse _
            (obj.StatusCode = Codes.CLAIM_STATUS__PENDING) OrElse _
            ((obj.FollowupDate.Value >= obj.GetShortDate(Today)) AndAlso _
                (obj.FollowupDate.Value <= NonbusinessCalendar.GetNextBusinessDate(obj.MaxFollowUpDays.Value, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id)) AndAlso _
                (NonbusinessCalendar.GetSameBusinessDaysCount(obj.FollowupDate.Value, ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id) <= 0))) Then
            Return True
        End If

        If ((obj.GetShortDate(obj.FollowupDate.Value) = obj.OriginalFollowUpDate) AndAlso _
            (obj.ReasonClosedId.Equals(Guid.Empty))) Then
            obj.CalculateFollowUpDate()
            Return True
        End If

        If (obj.FollowupDate.Value < obj.GetShortDate(Today)) Then
            Return False
        End If

    End Function

#End Region




End Class

