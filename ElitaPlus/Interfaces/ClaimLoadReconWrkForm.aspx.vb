Imports Assurant.ElitaPlus.DALObjects
Partial Public Class ClaimLoadReconWrkForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "ClaimLoadReconWrkForm.aspx"
    Public Const PAGETITLE As String = "CLAIM_FILE"
    Public Const PAGETAB As String = "INTERFACES"

    Private Const COL_ReconWrk_ID As Integer = 0
    Private Const COL_Record_Type As Integer = 1
    Private Const COL_Reject_Code As Integer = 2
    Private Const COL_Reject_Reason As Integer = 3
    Private Const COL_Dealer_Code As Integer = 4
    Private Const COL_Cert_Num As Integer = 5
    Private Const COL_Claim_Type As Integer = 6
    Private Const COL_Auth_Num As Integer = 7
    Private Const COL_External_Date As Integer = 8
    Private Const COL_Coverage_Code As Integer = 9
    Private Const COL_Risk_Type_English As Integer = 10     'REQ-5585 
    Private Const COL_Loss_Date As Integer = 11
    Private Const COL_Loss_Cause As Integer = 12
    Private Const COL_Problem_Desc As Integer = 13
    Private Const COL_Comments As Integer = 14
    Private Const COL_Special_Instruction As Integer = 15
    Private Const COL_Repair_Date As Integer = 16
    Private Const COL_Visit_Date As Integer = 17
    Private Const COL_Pickup_Date As Integer = 18
    Private Const COL_Reason_Closed As Integer = 19
    Private Const COL_Manufacturer As Integer = 20
    Private Const COL_Model As Integer = 21
    Private Const COL_Serial_Num As Integer = 22
    Private Const COL_Item_Description As Integer = 23  'REQ-5585 
    Private Const COL_Service_Center As Integer = 24
    Private Const COL_Product_Code As Integer = 25
    Private Const COL_Defect_Reason As Integer = 26
    Private Const COL_Repair_Code As Integer = 27
    Private Const COL_Caller_Name As Integer = 28
    Private Const COL_Contact_Name As Integer = 29
    Private Const COL_Invoice_Num As Integer = 30
    Private Const COL_Amount As Integer = 31
    Private Const COL_Estimate_Amount As Integer = 32
    Private Const COL_Police_Rpt_Num As Integer = 33
    Private Const COL_Officer_Name As Integer = 34
    Private Const COL_Police_Station As Integer = 35
    Private Const COL_Document_Type As Integer = 36
    Private Const COL_RG_Num As Integer = 37
    Private Const COL_Document_Agency As Integer = 38
    Private Const COL_Doc_Issue_Date As Integer = 39
    Private Const COL_ID_Type As Integer = 40
    Private Const COL_Device_Reception_Date As Integer = 41
    Private Const COL_Replacement_Type As Integer = 42
    Private Const COL_Manufacturer_Replacement_Device As Integer = 43
    Private Const COL_Model_Replacement_Device As Integer = 44
    Private Const COL_SerialNumber_Replacement_Device As Integer = 45
    Private Const COL_SKU_Replacement_Device As Integer = 46
    Private Const COL_Deductible_Collected As Integer = 47
    Private Const COL_Labor_Amount As Integer = 48
    Private Const COL_Parts_Amount As Integer = 49
    Private Const COL_Service_Charge As Integer = 50
    Private Const COL_Shipping_Amount As Integer = 51
    'REQ-5585 Start
    Private Const COL_Trip_Amount As Integer = 52
    Private Const COL_Other_Amount As Integer = 53
    Private Const COL_Other_Explanation As Integer = 54
    'REQ-5585 End
    Private Const COL_Service_Level As Integer = 55
    Private Const COL_Dealers_Reference As Integer = 56
    Private Const COL_POS As Integer = 57
    Private Const COL_Problem_Found As Integer = 58
    Private Const COL_Final_Status As Integer = 59
    Private Const COL_Technical_Report As Integer = 60
    Private Const COL_Delivery_Date As Integer = 61
    Private Const COL_Batch_Number As Integer = 62
    Private Const COL_Comment_Type As Integer = 63
    Private Const COL_Invoice_Date As Integer = 64
    Private Const COL_Modified_Date As Integer = 65
    Private Const COL_Claim_Number As Integer = 66
    Private Const COL_Extended_Status_Code As Integer = 67
    Private Const COL_Extended_Status_Date As Integer = 68
    Private Const COL_Tracking_Number As Integer = 69
    Private const COL_Deductible_Included As Integer = 70

    Private Const PART_NUMBER_COL As Integer = 0
    Private Const PART_SKU_COL As Integer = 1
    Private Const PART_DESCRIPTION_COL As Integer = 2

    Private Const Ctl_Record_Type As String = "txtRecordType"
    Private Const Ctl_Dealer_Code As String = "txtDealerCode"
    Private Const Ctl_Certificate As String = "txtCertNum"
    Private Const Ctl_Claim_Type As String = "txtClaimType"
    Private Const Ctl_Authorization_Num As String = "txtAuthNum"
    Private Const Ctl_External_Date As String = "txtExternalDate"
    Private Const Ctl_Coverage_Code As String = "txtCoverageCode"
    Private Const Ctl_Loss_Date As String = "txtLossDate"
    Private Const Ctl_Loss_Cause As String = "txtLossCause"
    Private Const Ctl_Problem_Desc As String = "txtProbDesc"
    Private Const Ctl_Comments As String = "txtComments"
    Private Const Ctl_Special_Instruction As String = "txtSpecialInstruct"
    Private Const Ctl_Repair_Date As String = "txtRepairDate"
    Private Const Ctl_Visit_Date As String = "txtVisitDate"
    Private Const Ctl_Pickup_Date As String = "txtPickupDate"
    Private Const Ctl_Reason_Closed As String = "txtReasonClosed"
    Private Const Ctl_Manufacturer As String = "txtManufacturer"
    Private Const Ctl_Model As String = "txtModel"
    Private Const Ctl_Serial_Num As String = "txtSerialNum"
    Private Const Ctl_Service_Center_Code As String = "txtServiceCenter"
    Private Const Ctl_ProductCode As String = "txtProductCode"
    Private Const Ctl_DefectReason As String = "txtDefectReason"
    Private Const Ctl_Repair_Code As String = "txtRepairCode"
    Private Const Ctl_CallerName As String = "txtCallerName"
    Private Const Ctl_ContactName As String = "txtContactName"
    Private Const Ctl_Invoice_Num As String = "txtInvNum"
    Private Const Ctl_Amount As String = "txtAmount"
    Private Const Ctl_Estimate_Amount As String = "txtEstimateAmt"
    Private Const Ctl_Police_Report_Num As String = "txtPoliceRptNum"
    Private Const Ctl_Officer_Name As String = "txtOfficerName"
    Private Const Ctl_Police_Station_Code As String = "txtPoliceStationCode"
    Private Const Ctl_Document_Type As String = "txtDocumentType"
    Private Const Ctl_RG_Number As String = "txtRGNumber"
    Private Const Ctl_DocumentAgency As String = "txtDocAgency"
    Private Const Ctl_Document_Issue_Date As String = "txtDocIssueDate"
    Private Const Ctl_ID_Type As String = "txtIDType"
    Private Const Ctl_Device_Reception_Date As String = "txtDeviceReceptionDate"
    Private Const Ctl_Replacement_Type As String = "txtReplacementType"
    Private Const Ctl_Manufacturer_Replacement_Device As String = "txtManufacturerReplacementDevice"
    Private Const Ctl_Model_Replacement_Device As String = "txtModelReplacementDevice"
    Private Const Ctl_SerialNumber_Replacement_Device As String = "txtSerialNumberReplacementDevice"
    Private Const Ctl_SKU_Replacement_Device As String = "txtSKUReplacementDevice"
    Private Const Ctl_Deductible_Collected As String = "txtDeductibleCollected"
    Private Const Ctl_Labor_Amount As String = "txtLaborAmount"
    Private Const Ctl_Parts_Amount As String = "txtPartsAmount"
    Private Const Ctl_Service_Charge As String = "txtServiceCharge"
    Private Const Ctl_Shipping_Amount As String = "txtShippingAmount"
    Private Const Ctl_Service_Level As String = "txtServiceLevel"
    Private Const Ctl_Dealers_Reference As String = "txtDealersReference"
    Private Const Ctl_POS As String = "txtPOS"
    Private Const Ctl_Problem_Found As String = "txtProblemFound"
    Private Const Ctl_Final_Status As String = "txtFinalStatus"
    Private Const Ctl_Technical_Report As String = "txtTechnicalReport"
    Private Const Ctl_Delivery_Date As String = "txtDeliveryDate"
    Private Const Ctl_Batch_Number As String = "txtBatchNumber"
    Private Const Ctl_Comment_Type As String = "txtCommentType"
    Private Const Ctl_Invoice_Date As String = "txtInvoiceDate"
    'REQ-5585 Start
    Private Const Ctl_Risk_Type_English As String = "txtRiskTypeEnglish"
    Private Const Ctl_Item_Description As String = "txtItemDescription"
    Private Const Ctl_Trip_Amount As String = "txtTripAmount"
    Private Const Ctl_Other_Amount As String = "txtOtherAmount"
    Private Const Ctl_Other_Explanation As String = "txtOtherExplanation"
    'REQ-5585 End

    Private Const Ctl_Claim_Number As String = "txtClaimNumber"
    Private Const Ctl_Extended_Status_Code As String = "txtExtendedStatusCode"
    Private Const Ctl_Extended_Status_Date As String = "txtExtendedStatusDate"
    Private Const Ctl_Tracking_Number As String = "txtTrackingNumber"

    Private Const Ctl_Reject_Code As String = "txtRejectCode"
    Private Const Ctl_Reject_Reason As String = "txtRejectReason"

    Private Const Ctl_External_Date_Calendar As String = "imgExternalDate"
    Private Const Ctl_Loss_Date_Calendar As String = "imgLossDate"
    Private Const Ctl_Repair_Date_Calendar As String = "imgRepairDate"
    Private Const Ctl_Visit_Date_Calendar As String = "imgVisitDate"
    Private Const Ctl_Pickup_Date_Calendar As String = "imgPickupDate"
    Private Const Ctl_Document_Issue_Date_Calendar As String = "imgDocIssueDate"
    Private Const Ctl_Delivery_Date_Calendar As String = "imgDeliveryDate"
    Private Const Ctl_Device_Reception_Date_Calendar As String = "imgDeviceReceptionDate"
    Private Const Ctl_Invoice_Date_Calendar As String = "imgInvoiceDate"
    Private Const Ctl_Extended_Status_date_Calendar As String = "imgExtendedStatusDate"

    Private Const PART_NUMBER As String = "PartNumber"
    Private Const PART_SKU As String = "PartSKU"
    Private Const PART_DESCRIPTION As String = "PartDescription"

    Private Const Ctl_Deductile_Included As String = "txtDeductbleIncluded"

#End Region


#Region "Page State"

    Class MyState
        Public ClaimloadfileName As String
        Public ActionInProgress As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Nothing_
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public PageIndex As Integer = 0
        Public RecordCount As Integer
        Public ClaimReconWrkId As Guid
        Public PartsHashTable As Hashtable

    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property

    Private Sub SetStateProperties()
        If CallingParameters IsNot Nothing Then
            State.ClaimloadfileName = CType(CallingParameters, String)
        End If
    End Sub

#End Region

#Region "Page events"
    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        ErrControllerMaster.Clear_Hide()
        ErrController2.Clear_Hide()
        Try
            If Not IsPostBack Then
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                TranslateGridHeader(Grid)
                SetStateProperties()
                populateReadyOnly()
                PopulateGrid()
            Else
                CheckIfComingFromSaveConfirm()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        ShowMissingTranslations(ErrControllerMaster)
    End Sub
#End Region

#Region "Helper Function"
    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = HiddenSavePagePromptResponse.Value
        Try
            If Not confResponse.Equals(String.Empty) Then
                If confResponse = MSG_VALUE_YES Then
                    'SavePage()
                End If
                HiddenSavePagePromptResponse.Value = String.Empty
                HiddenIsPageDirty.Value = String.Empty

                Select Case State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Dim retType As New ClaimLoadForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.ClaimloadfileName)
                        ReturnToCallingPage(retType)
                    Case ElitaPlusPage.DetailPageCommand.GridPageSize
                        Grid.PageIndex = NewCurrentPageIndex(Grid, State.RecordCount, State.PageSize)
                        State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                    Case Else
                        Grid.PageIndex = State.PageIndex
                End Select
                PopulateGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Public Sub PopulateGrid()
        Dim dv As DataView
        dv = ClaimloadReconWrk.LoadList(State.ClaimloadfileName)
        If dv IsNot Nothing AndAlso dv.Count > 0 Then
            State.RecordCount = dv.Count
            SetPageAndSelectedIndexFromGuid(dv, Nothing, Grid, State.PageIndex, False)
            Grid.DataSource = dv
            Grid.DataBind()
            lblRecordCount.Text = dv.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
            If Grid.BottomPagerRow IsNot Nothing Then
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
            End If
        Else
            State.RecordCount = 0
            lblRecordCount.Text = "0 " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        End If
    End Sub

    Public Sub populateReadyOnly()
        moFileNameText.Text = State.ClaimloadfileName
    End Sub

    Function IsDataGPageDirty() As Boolean
        Dim Result As String = HiddenIsPageDirty.Value.ToUpper
        Return Result.Equals("YES")
    End Function

    Private Sub SavePage()
        Dim index As Integer = 0
        Dim objRecon As ClaimloadReconWrk
        Dim totItems As Integer = Grid.Rows.Count

        If totItems > 0 Then
            objRecon = CreateBoFromGrid(0)
            BindBoPropertiesToGridHeaders(objRecon)
            Grid.SelectedIndex = 0
            PopulateBOFromForm(objRecon)
            If objRecon.IsDirty Then objRecon.Save()
        End If

        totItems = totItems - 1
        For index = 1 To totItems
            objRecon = CreateBoFromGrid(index)
            Grid.SelectedIndex = index
            PopulateBOFromForm(objRecon)
            If objRecon.IsDirty Then objRecon.Save()
        Next
    End Sub

    Private Function CreateBoFromGrid(index As Integer) As ClaimloadReconWrk
        Dim ReconWrkId As Guid
        Dim objReconWrk As ClaimloadReconWrk
        Dim sModifiedDate As String

        'moDataGrid.SelectedIndex = index
        'ReconWrkId = New Guid(Grid.Rows(index).Cells(Me.COL_ReconWrk_ID).Text)
        ReconWrkId = New Guid(GetGridText(Grid, index, COL_ReconWrk_ID))
        sModifiedDate = GetGridText(Grid, index, COL_Modified_Date)
        objReconWrk = New ClaimloadReconWrk(ReconWrkId, sModifiedDate)
        Return objReconWrk
    End Function

    Private Sub PopulateBOFromForm(objReconWrk As ClaimloadReconWrk)
        PopulateBOItem(objReconWrk, "RecordType", COL_Record_Type)
        PopulateBOItem(objReconWrk, "DealerCode", COL_Dealer_Code)
        PopulateBOItem(objReconWrk, "Certificate", COL_Cert_Num)
        PopulateBOItem(objReconWrk, "ClaimType", COL_Claim_Type)
        PopulateBOItem(objReconWrk, "AuthorizationNum", COL_Auth_Num)
        PopulateBOItem(objReconWrk, "ExternalCreatedDate", COL_External_Date)
        PopulateBOItem(objReconWrk, "CoverageCode", COL_Coverage_Code)
        PopulateBOItem(objReconWrk, "DateOfLoss", COL_Loss_Date)
        PopulateBOItem(objReconWrk, "CauseOfLoss", COL_Loss_Cause)
        PopulateBOItem(objReconWrk, "ProblemDescription", COL_Problem_Desc)
        PopulateBOItem(objReconWrk, "Comments", COL_Comments)
        PopulateBOItem(objReconWrk, "SpecialInstructions", COL_Special_Instruction)
        PopulateBOItem(objReconWrk, "RepairDate", COL_Repair_Date)
        PopulateBOItem(objReconWrk, "VisitDate", COL_Visit_Date)
        PopulateBOItem(objReconWrk, "InvoiceDate", COL_Invoice_Date)
        PopulateBOItem(objReconWrk, "PickupDate", COL_Pickup_Date)
        PopulateBOItem(objReconWrk, "ReasonClosed", COL_Reason_Closed)
        PopulateBOItem(objReconWrk, "Manufacturer", COL_Manufacturer)
        PopulateBOItem(objReconWrk, "Model", COL_Model)
        PopulateBOItem(objReconWrk, "SerialNumber", COL_Serial_Num)
        PopulateBOItem(objReconWrk, "ServiceCenterCode", COL_Service_Center)
        PopulateBOItem(objReconWrk, "ProductCode", COL_Product_Code)
        PopulateBOItem(objReconWrk, "DefectReason", COL_Defect_Reason)
        PopulateBOItem(objReconWrk, "RepairCode", COL_Repair_Code)
        PopulateBOItem(objReconWrk, "CallerName", COL_Caller_Name)
        PopulateBOItem(objReconWrk, "ContactName", COL_Contact_Name)
        PopulateBOItem(objReconWrk, "InvoiceNumber", COL_Invoice_Num)
        PopulateBOItem(objReconWrk, "Amount", COL_Amount)
        PopulateBOItem(objReconWrk, "EstimateAmount", COL_Estimate_Amount)
        PopulateBOItem(objReconWrk, "PoliceReportNum", COL_Police_Rpt_Num)
        PopulateBOItem(objReconWrk, "OfficerName", COL_Officer_Name)
        PopulateBOItem(objReconWrk, "PoliceStationCode", COL_Police_Station)
        PopulateBOItem(objReconWrk, "DocumentType", COL_Document_Type)
        PopulateBOItem(objReconWrk, "RgNumber", COL_RG_Num)
        PopulateBOItem(objReconWrk, "DocumentAgency", COL_Document_Agency)
        PopulateBOItem(objReconWrk, "DocumentIssueDate", COL_Doc_Issue_Date)
        PopulateBOItem(objReconWrk, "IdType", COL_ID_Type)
        PopulateBOItem(objReconWrk, "DeviceReceptionDate", COL_Device_Reception_Date)
        PopulateBOItem(objReconWrk, "ReplacementType", COL_Replacement_Type)
        PopulateBOItem(objReconWrk, "ReplacementManufacturer", COL_Manufacturer_Replacement_Device)
        PopulateBOItem(objReconWrk, "ReplacementModel", COL_Model_Replacement_Device)
        PopulateBOItem(objReconWrk, "ReplacementSerialNumber", COL_SerialNumber_Replacement_Device)
        PopulateBOItem(objReconWrk, "ReplacementSku", COL_SKU_Replacement_Device)
        PopulateBOItem(objReconWrk, "DeductibleCollected", COL_Deductible_Collected)
        PopulateBOItem(objReconWrk, "LaborAmount", COL_Labor_Amount)
        PopulateBOItem(objReconWrk, "PartsAmount", COL_Parts_Amount)
        PopulateBOItem(objReconWrk, "ServiceAmount", COL_Service_Charge)
        PopulateBOItem(objReconWrk, "ShippingAmount", COL_Shipping_Amount)
        PopulateBOItem(objReconWrk, "ServiceLevel", COL_Service_Level)
        PopulateBOItem(objReconWrk, "DealerReference", COL_Dealers_Reference)
        PopulateBOItem(objReconWrk, "Pos", COL_POS)
        PopulateBOItem(objReconWrk, "ProblemFound", COL_Problem_Found)
        PopulateBOItem(objReconWrk, "FinalStatus", COL_Final_Status)
        PopulateBOItem(objReconWrk, "TechnicalReport", COL_Technical_Report)
        PopulateBOItem(objReconWrk, "DeliveryDate", COL_Delivery_Date)
        PopulateBOItem(objReconWrk, "BatchNumber", COL_Batch_Number)
        PopulateBOItem(objReconWrk, "CommentType", COL_Comment_Type)
        'REQ-5585 Start
        PopulateBOItem(objReconWrk, "RiskTypeEnglish", COL_Risk_Type_English)
        PopulateBOItem(objReconWrk, "ItemDescription", COL_Item_Description)
        PopulateBOItem(objReconWrk, "TripAmount", COL_Trip_Amount)
        PopulateBOItem(objReconWrk, "OtherAmount", COL_Other_Amount)
        PopulateBOItem(objReconWrk, "OtherExplanation", COL_Other_Explanation)
        'REQ-5585 End
        PopulateBOItem(objReconWrk, "ClaimNumber", COL_Claim_Number)
        PopulateBOItem(objReconWrk, "ExtendedStatusCode", COL_Extended_Status_Code)
        PopulateBOItem(objReconWrk, "ExtendedStatusDate", COL_Extended_Status_Date)
        PopulateBOItem(objReconWrk, "TrackingNumber", COL_Tracking_Number)
        PopulateBOItem(objReconWrk, "DeductibleIncluded", COL_Deductible_Included)

        'If Me.ErrCollection.Count > 0 Then
        '    Throw New PopulateBOErrorException
        'End If
    End Sub

    Private Sub PopulateBOItem(objReconWrk As ClaimloadReconWrk, oPropertyName As String, oCellPosition As Integer)
        PopulateBOProperty(objReconWrk, oPropertyName, CType(GetSelectedGridControl(Grid, oCellPosition), TextBox))
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders(objRecon As ClaimloadReconWrk)
        BindBOPropertyToGridHeader(objRecon, "RecordType", Grid.Columns(COL_Record_Type))
        BindBOPropertyToGridHeader(objRecon, "DealerCode", Grid.Columns(COL_Dealer_Code))
        BindBOPropertyToGridHeader(objRecon, "Certificate", Grid.Columns(COL_Cert_Num))
        BindBOPropertyToGridHeader(objRecon, "ClaimType", Grid.Columns(COL_Claim_Type))
        BindBOPropertyToGridHeader(objRecon, "AuthorizationNum", Grid.Columns(COL_Auth_Num))
        BindBOPropertyToGridHeader(objRecon, "ExternalCreatedDate", Grid.Columns(COL_External_Date))
        BindBOPropertyToGridHeader(objRecon, "CoverageCode", Grid.Columns(COL_Coverage_Code))
        BindBOPropertyToGridHeader(objRecon, "DateOfLoss", Grid.Columns(COL_Loss_Date))
        BindBOPropertyToGridHeader(objRecon, "CauseOfLoss", Grid.Columns(COL_Loss_Cause))
        BindBOPropertyToGridHeader(objRecon, "ProblemDescription", Grid.Columns(COL_Problem_Desc))
        BindBOPropertyToGridHeader(objRecon, "Comments", Grid.Columns(COL_Comments))
        BindBOPropertyToGridHeader(objRecon, "SpecialInstructions", Grid.Columns(COL_Special_Instruction))
        BindBOPropertyToGridHeader(objRecon, "RepairDate", Grid.Columns(COL_Repair_Date))
        BindBOPropertyToGridHeader(objRecon, "VisitDate", Grid.Columns(COL_Visit_Date))
        BindBOPropertyToGridHeader(objRecon, "InvoiceDate", Grid.Columns(COL_Invoice_Date))
        BindBOPropertyToGridHeader(objRecon, "PickupDate", Grid.Columns(COL_Pickup_Date))
        BindBOPropertyToGridHeader(objRecon, "ReasonClosed", Grid.Columns(COL_Reason_Closed))
        BindBOPropertyToGridHeader(objRecon, "Manufacturer", Grid.Columns(COL_Manufacturer))
        BindBOPropertyToGridHeader(objRecon, "Model", Grid.Columns(COL_Model))
        BindBOPropertyToGridHeader(objRecon, "SerialNumber", Grid.Columns(COL_Serial_Num))
        BindBOPropertyToGridHeader(objRecon, "ServiceCenterCode", Grid.Columns(COL_Service_Center))
        BindBOPropertyToGridHeader(objRecon, "ProductCode", Grid.Columns(COL_Product_Code))
        BindBOPropertyToGridHeader(objRecon, "DefectReason", Grid.Columns(COL_Defect_Reason))
        BindBOPropertyToGridHeader(objRecon, "RepairCode", Grid.Columns(COL_Repair_Code))
        BindBOPropertyToGridHeader(objRecon, "CallerName", Grid.Columns(COL_Caller_Name))
        BindBOPropertyToGridHeader(objRecon, "ContactName", Grid.Columns(COL_Contact_Name))
        BindBOPropertyToGridHeader(objRecon, "InvoiceNumber", Grid.Columns(COL_Invoice_Num))
        BindBOPropertyToGridHeader(objRecon, "Amount", Grid.Columns(COL_Amount))
        BindBOPropertyToGridHeader(objRecon, "EstimateAmount", Grid.Columns(COL_Estimate_Amount))
        BindBOPropertyToGridHeader(objRecon, "PoliceReportNum", Grid.Columns(COL_Police_Rpt_Num))
        BindBOPropertyToGridHeader(objRecon, "OfficerName", Grid.Columns(COL_Officer_Name))
        BindBOPropertyToGridHeader(objRecon, "PoliceStationCode", Grid.Columns(COL_Police_Station))
        BindBOPropertyToGridHeader(objRecon, "DocumentType", Grid.Columns(COL_Document_Type))
        BindBOPropertyToGridHeader(objRecon, "RgNumber", Grid.Columns(COL_RG_Num))
        BindBOPropertyToGridHeader(objRecon, "DocumentAgency", Grid.Columns(COL_Document_Agency))
        BindBOPropertyToGridHeader(objRecon, "DocumentIssueDate", Grid.Columns(COL_Doc_Issue_Date))
        BindBOPropertyToGridHeader(objRecon, "IdType", Grid.Columns(COL_ID_Type))
        BindBOPropertyToGridHeader(objRecon, "DeviceReceptionDate", Grid.Columns(COL_Device_Reception_Date))
        BindBOPropertyToGridHeader(objRecon, "ReplacementType", Grid.Columns(COL_Replacement_Type))
        BindBOPropertyToGridHeader(objRecon, "ReplacementManufacturer", Grid.Columns(COL_Manufacturer_Replacement_Device))
        BindBOPropertyToGridHeader(objRecon, "ReplacementModel", Grid.Columns(COL_Model_Replacement_Device))
        BindBOPropertyToGridHeader(objRecon, "ReplacementSerialNumber", Grid.Columns(COL_SerialNumber_Replacement_Device))
        BindBOPropertyToGridHeader(objRecon, "ReplacementSku", Grid.Columns(COL_SKU_Replacement_Device))
        BindBOPropertyToGridHeader(objRecon, "DeductibleCollected", Grid.Columns(COL_Deductible_Collected))
        BindBOPropertyToGridHeader(objRecon, "LaborAmount", Grid.Columns(COL_Labor_Amount))
        BindBOPropertyToGridHeader(objRecon, "PartsAmount", Grid.Columns(COL_Parts_Amount))
        BindBOPropertyToGridHeader(objRecon, "ServiceAmount", Grid.Columns(COL_Service_Charge))
        BindBOPropertyToGridHeader(objRecon, "ShippingAmount", Grid.Columns(COL_Shipping_Amount))
        BindBOPropertyToGridHeader(objRecon, "ServiceLevel", Grid.Columns(COL_Service_Level))
        BindBOPropertyToGridHeader(objRecon, "DealerReference", Grid.Columns(COL_Dealers_Reference))
        BindBOPropertyToGridHeader(objRecon, "Pos", Grid.Columns(COL_POS))
        BindBOPropertyToGridHeader(objRecon, "ProblemFound", Grid.Columns(COL_Problem_Found))
        BindBOPropertyToGridHeader(objRecon, "FinalStatus", Grid.Columns(COL_Final_Status))
        BindBOPropertyToGridHeader(objRecon, "TechnicalReport", Grid.Columns(COL_Technical_Report))
        BindBOPropertyToGridHeader(objRecon, "DeliveryDate", Grid.Columns(COL_Delivery_Date))
        BindBOPropertyToGridHeader(objRecon, "BatchNumber", Grid.Columns(COL_Batch_Number))
        BindBOPropertyToGridHeader(objRecon, "CommentType", Grid.Columns(COL_Comment_Type))
        BindBOPropertyToGridHeader(objRecon, "RiskTypeEnglish", Grid.Columns(COL_Risk_Type_English))
        BindBOPropertyToGridHeader(objRecon, "ItemDescription", Grid.Columns(COL_Item_Description))
        BindBOPropertyToGridHeader(objRecon, "TripAmount", Grid.Columns(COL_Trip_Amount))
        BindBOPropertyToGridHeader(objRecon, "OtherAmount", Grid.Columns(COL_Other_Amount))
        BindBOPropertyToGridHeader(objRecon, "OtherExplanation", Grid.Columns(COL_Other_Explanation))
        BindBOPropertyToGridHeader(objRecon, "ClaimNumber", Grid.Columns(COL_Claim_Number))
        BindBOPropertyToGridHeader(objRecon, "ExtendedStatusCode", Grid.Columns(COL_Extended_Status_Code))
        BindBOPropertyToGridHeader(objRecon, "ExtendedStatusDate", Grid.Columns(COL_Extended_Status_Date))
        BindBOPropertyToGridHeader(objRecon, "TrackingNumber", Grid.Columns(COL_Tracking_Number))
        BindBOPropertyToGridHeader(objRecon, "DeductibleIncluded", Grid.Columns(COL_Deductible_Included))
        ClearGridViewHeadersAndLabelsErrSign()
    End Sub

    Protected Sub BindBoPropertiesToPartsGridHeaders(claimReconWrkPartsInfo As ClaimReconWrkParts)
        BindBOPropertyToGridHeader(claimReconWrkPartsInfo, "PartNumber", gvPop.Columns(PART_NUMBER_COL))
        BindBOPropertyToGridHeader(claimReconWrkPartsInfo, "PartSKU", gvPop.Columns(PART_SKU_COL))
        BindBOPropertyToGridHeader(claimReconWrkPartsInfo, "PartDescription", gvPop.Columns(PART_DESCRIPTION_COL))
        ClearGridViewHeadersAndLabelsErrSign()
    End Sub
#End Region

#Region "Grid related"

    Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub
    Private Sub Grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub


    Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            BaseItemBound(sender, e)
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim ctlTxt As TextBox, ctlCanlendar As ImageButton
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                ctlTxt = CType(e.Row.Cells(COL_Record_Type).FindControl(Ctl_Record_Type), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_RECORD_TYPE).ToString

                CType(e.Row.Cells(COL_Reject_Code).FindControl(Ctl_Reject_Code), TextBox).Text = dvRow(ClaimloadReconWrk.COL_NAME_REJECT_CODE).ToString()
                CType(e.Row.Cells(COL_Reject_Reason).FindControl(Ctl_Reject_Reason), TextBox).Text = dvRow(ClaimloadReconWrk.COL_NAME_REJECT_REASON).ToString()

                ctlTxt = CType(e.Row.Cells(COL_Dealer_Code).FindControl(Ctl_Dealer_Code), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_DEALER_CODE).ToString

                ctlTxt = CType(e.Row.Cells(COL_Cert_Num).FindControl(Ctl_Certificate), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_CERTIFICATE).ToString

                ctlTxt = CType(e.Row.Cells(COL_Claim_Type).FindControl(Ctl_Claim_Type), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_CLAIM_TYPE).ToString

                ctlTxt = CType(e.Row.Cells(COL_Auth_Num).FindControl(Ctl_Authorization_Num), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_AUTHORIZATION_NUM).ToString

                ctlTxt = CType(e.Row.Cells(COL_External_Date).FindControl(Ctl_External_Date), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlCanlendar = CType(e.Row.Cells(COL_External_Date).FindControl(Ctl_External_Date_Calendar), ImageButton)
                If (ctlCanlendar IsNot Nothing) Then
                    AddCalendar(ctlCanlendar, ctlTxt)
                End If
                PopulateControlFromBOProperty(ctlTxt, dvRow(ClaimloadReconWrk.COL_NAME_EXTERNAL_CREATED_DATE))

                ctlTxt = CType(e.Row.Cells(COL_Coverage_Code).FindControl(Ctl_Coverage_Code), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_COVERAGE_CODE).ToString

                ctlTxt = CType(e.Row.Cells(COL_Loss_Date).FindControl(Ctl_Loss_Date), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlCanlendar = CType(e.Row.Cells(COL_Loss_Date).FindControl(Ctl_Loss_Date_Calendar), ImageButton)
                If (ctlCanlendar IsNot Nothing) Then
                    AddCalendar(ctlCanlendar, ctlTxt)
                End If
                PopulateControlFromBOProperty(ctlTxt, dvRow(ClaimloadReconWrk.COL_NAME_DATE_OF_LOSS))

                ctlTxt = CType(e.Row.Cells(COL_Loss_Cause).FindControl(Ctl_Loss_Cause), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_CAUSE_OF_LOSS).ToString

                ctlTxt = CType(e.Row.Cells(COL_Problem_Desc).FindControl(Ctl_Problem_Desc), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_PROBLEM_DESCRIPTION).ToString

                ctlTxt = CType(e.Row.Cells(COL_Comments).FindControl(Ctl_Comments), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_COMMENTS).ToString

                ctlTxt = CType(e.Row.Cells(COL_Special_Instruction).FindControl(Ctl_Special_Instruction), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_SPECIAL_INSTRUCTIONS).ToString

                ctlTxt = CType(e.Row.Cells(COL_Repair_Date).FindControl(Ctl_Repair_Date), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlCanlendar = CType(e.Row.Cells(COL_Repair_Date).FindControl(Ctl_Repair_Date_Calendar), ImageButton)
                If (ctlCanlendar IsNot Nothing) Then
                    AddCalendar(ctlCanlendar, ctlTxt)
                End If
                PopulateControlFromBOProperty(ctlTxt, dvRow(ClaimloadReconWrk.COL_NAME_REPAIR_DATE))

                ctlTxt = CType(e.Row.Cells(COL_Visit_Date).FindControl(Ctl_Visit_Date), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlCanlendar = CType(e.Row.Cells(COL_Visit_Date).FindControl(Ctl_Visit_Date_Calendar), ImageButton)
                If (ctlCanlendar IsNot Nothing) Then
                    AddCalendar(ctlCanlendar, ctlTxt)
                End If
                PopulateControlFromBOProperty(ctlTxt, dvRow(ClaimloadReconWrk.COL_NAME_VISIT_DATE))

                ctlTxt = CType(e.Row.Cells(COL_Invoice_Date).FindControl(Ctl_Invoice_Date), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlCanlendar = CType(e.Row.Cells(COL_Invoice_Date).FindControl(Ctl_Invoice_Date_Calendar), ImageButton)
                If (ctlCanlendar IsNot Nothing) Then
                    AddCalendar(ctlCanlendar, ctlTxt)
                End If
                PopulateControlFromBOProperty(ctlTxt, dvRow(ClaimloadReconWrk.COL_NAME_INVOICE_DATE))

                ctlTxt = CType(e.Row.Cells(COL_Pickup_Date).FindControl(Ctl_Pickup_Date), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlCanlendar = CType(e.Row.Cells(COL_Pickup_Date).FindControl(Ctl_Pickup_Date_Calendar), ImageButton)
                If (ctlCanlendar IsNot Nothing) Then
                    AddCalendar(ctlCanlendar, ctlTxt)
                End If
                PopulateControlFromBOProperty(ctlTxt, dvRow(ClaimloadReconWrk.COL_NAME_PICKUP_DATE))

                ctlTxt = CType(e.Row.Cells(COL_Reason_Closed).FindControl(Ctl_Reason_Closed), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_REASON_CLOSED).ToString

                ctlTxt = CType(e.Row.Cells(COL_Manufacturer).FindControl(Ctl_Manufacturer), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_MANUFACTURER).ToString

                ctlTxt = CType(e.Row.Cells(COL_Model).FindControl(Ctl_Model), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_MODEL).ToString

                ctlTxt = CType(e.Row.Cells(COL_Serial_Num).FindControl(Ctl_Serial_Num), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_SERIAL_NUMBER).ToString

                ctlTxt = CType(e.Row.Cells(COL_Service_Center).FindControl(Ctl_Service_Center_Code), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_SERVICE_CENTER_CODE).ToString

                ctlTxt = CType(e.Row.Cells(COL_Product_Code).FindControl(Ctl_ProductCode), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_PRODUCT_CODE).ToString

                ctlTxt = CType(e.Row.Cells(COL_Defect_Reason).FindControl(Ctl_DefectReason), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_DEFECT_REASON).ToString

                ctlTxt = CType(e.Row.Cells(COL_Repair_Code).FindControl(Ctl_Repair_Code), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_REPAIR_CODE).ToString

                ctlTxt = CType(e.Row.Cells(COL_Caller_Name).FindControl(Ctl_CallerName), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_CALLER_NAME).ToString

                ctlTxt = CType(e.Row.Cells(COL_Contact_Name).FindControl(Ctl_ContactName), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_CONTACT_NAME).ToString

                ctlTxt = CType(e.Row.Cells(COL_Invoice_Num).FindControl(Ctl_Invoice_Num), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_INVOICE_NUMBER).ToString

                ctlTxt = CType(e.Row.Cells(COL_Amount).FindControl(Ctl_Amount), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                PopulateControlFromBOProperty(ctlTxt, dvRow(ClaimloadReconWrk.COL_NAME_AMOUNT))

                ctlTxt = CType(e.Row.Cells(COL_Estimate_Amount).FindControl(Ctl_Estimate_Amount), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                PopulateControlFromBOProperty(ctlTxt, dvRow(ClaimloadReconWrk.COL_NAME_ESTIMATE_AMOUNT))

                ctlTxt = CType(e.Row.Cells(COL_Police_Station).FindControl(Ctl_Police_Station_Code), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_POLICE_STATION_CODE).ToString

                ctlTxt = CType(e.Row.Cells(COL_Officer_Name).FindControl(Ctl_Officer_Name), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_OFFICER_NAME).ToString

                ctlTxt = CType(e.Row.Cells(COL_Police_Rpt_Num).FindControl(Ctl_Police_Report_Num), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_POLICE_REPORT_NUM).ToString

                ctlTxt = CType(e.Row.Cells(COL_Document_Type).FindControl(Ctl_Document_Type), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_DOCUMENT_TYPE).ToString

                ctlTxt = CType(e.Row.Cells(COL_RG_Num).FindControl(Ctl_RG_Number), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_RG_NUMBER).ToString

                ctlTxt = CType(e.Row.Cells(COL_Document_Agency).FindControl(Ctl_DocumentAgency), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_DOCUMENT_AGENCY).ToString

                ctlTxt = CType(e.Row.Cells(COL_Doc_Issue_Date).FindControl(Ctl_Document_Issue_Date), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlCanlendar = CType(e.Row.Cells(COL_Doc_Issue_Date).FindControl(Ctl_Document_Issue_Date_Calendar), ImageButton)
                If (ctlCanlendar IsNot Nothing) Then
                    AddCalendar(ctlCanlendar, ctlTxt)
                End If
                PopulateControlFromBOProperty(ctlTxt, dvRow(ClaimloadReconWrk.COL_NAME_DOCUMENT_ISSUE_DATE))

                ctlTxt = CType(e.Row.Cells(COL_ID_Type).FindControl(Ctl_ID_Type), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_ID_TYPE).ToString

                ctlTxt = CType(e.Row.Cells(COL_Device_Reception_Date).FindControl(Ctl_Device_Reception_Date), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlCanlendar = CType(e.Row.Cells(COL_Device_Reception_Date).FindControl(Ctl_Device_Reception_Date_Calendar), ImageButton)
                If (ctlCanlendar IsNot Nothing) Then
                    AddCalendar(ctlCanlendar, ctlTxt)
                End If
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_DEVICE_RECEPTION_DATE).ToString

                ctlTxt = CType(e.Row.Cells(COL_Replacement_Type).FindControl(Ctl_Replacement_Type), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_REPLACEMENT_TYPE).ToString

                ctlTxt = CType(e.Row.Cells(COL_Manufacturer_Replacement_Device).FindControl(Ctl_Manufacturer_Replacement_Device), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_REPLACEMENT_MANUFACTURER).ToString

                ctlTxt = CType(e.Row.Cells(COL_Model_Replacement_Device).FindControl(Ctl_Model_Replacement_Device), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_REPLACEMENT_MODEL).ToString

                ctlTxt = CType(e.Row.Cells(COL_SerialNumber_Replacement_Device).FindControl(Ctl_SerialNumber_Replacement_Device), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_REPLACEMENT_SERIAL_NUMBER).ToString

                ctlTxt = CType(e.Row.Cells(COL_SKU_Replacement_Device).FindControl(Ctl_SKU_Replacement_Device), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_REPLACEMENT_SKU).ToString

                ctlTxt = CType(e.Row.Cells(COL_Deductible_Collected).FindControl(Ctl_Deductible_Collected), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_DEDUCTIBLE_COLLECTED).ToString

                ctlTxt = CType(e.Row.Cells(COL_Labor_Amount).FindControl(Ctl_Labor_Amount), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                PopulateControlFromBOProperty(ctlTxt, dvRow(ClaimloadReconWrk.COL_NAME_LABOR_AMOUNT))

                ctlTxt = CType(e.Row.Cells(COL_Parts_Amount).FindControl(Ctl_Parts_Amount), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                PopulateControlFromBOProperty(ctlTxt, dvRow(ClaimloadReconWrk.COL_NAME_PARTS_AMOUNT))

                ctlTxt = CType(e.Row.Cells(COL_Service_Charge).FindControl(Ctl_Service_Charge), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                PopulateControlFromBOProperty(ctlTxt, dvRow(ClaimloadReconWrk.COL_NAME_SERVICE_AMOUNT))

                ctlTxt = CType(e.Row.Cells(COL_Shipping_Amount).FindControl(Ctl_Shipping_Amount), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                PopulateControlFromBOProperty(ctlTxt, dvRow(ClaimloadReconWrk.COL_NAME_SHIPPING_AMOUNT))

                ctlTxt = CType(e.Row.Cells(COL_Service_Level).FindControl(Ctl_Service_Level), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_SERVICE_LEVEL).ToString

                ctlTxt = CType(e.Row.Cells(COL_Dealers_Reference).FindControl(Ctl_Dealers_Reference), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_DEALER_REFERENCE).ToString

                ctlTxt = CType(e.Row.Cells(COL_POS).FindControl(Ctl_POS), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_POS).ToString

                ctlTxt = CType(e.Row.Cells(COL_Problem_Found).FindControl(Ctl_Problem_Found), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_PROBLEM_FOUND).ToString

                ctlTxt = CType(e.Row.Cells(COL_Final_Status).FindControl(Ctl_Final_Status), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_FINAL_STATUS).ToString

                ctlTxt = CType(e.Row.Cells(COL_Technical_Report).FindControl(Ctl_Technical_Report), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_TECHNICAL_REPORT).ToString

                ctlTxt = CType(e.Row.Cells(COL_Delivery_Date).FindControl(Ctl_Delivery_Date), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlCanlendar = CType(e.Row.Cells(COL_Delivery_Date).FindControl(Ctl_Delivery_Date_Calendar), ImageButton)
                If (ctlCanlendar IsNot Nothing) Then
                    AddCalendar(ctlCanlendar, ctlTxt)
                End If
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_DELIVERY_DATE).ToString

                ctlTxt = CType(e.Row.Cells(COL_Batch_Number).FindControl(Ctl_Batch_Number), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_BATCH_NUMBER).ToString

                ctlTxt = CType(e.Row.Cells(COL_Comment_Type).FindControl(Ctl_Comment_Type), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_COMMENT_TYPE).ToString
                'REQ-5585 Start
                ctlTxt = CType(e.Row.Cells(COL_Risk_Type_English).FindControl(Ctl_Risk_Type_English), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_RISK_TYPE_ENGLISH).ToString

                ctlTxt = CType(e.Row.Cells(COL_Item_Description).FindControl(Ctl_Item_Description), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_ITEM_DESCRIPTION).ToString

                ctlTxt = CType(e.Row.Cells(COL_Trip_Amount).FindControl(Ctl_Trip_Amount), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                PopulateControlFromBOProperty(ctlTxt, dvRow(ClaimloadReconWrk.COL_NAME_TRIP_AMOUNT))

                ctlTxt = CType(e.Row.Cells(COL_Other_Amount).FindControl(Ctl_Other_Amount), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                PopulateControlFromBOProperty(ctlTxt, dvRow(ClaimloadReconWrk.COL_NAME_OTHER_AMOUNT))

                ctlTxt = CType(e.Row.Cells(COL_Other_Explanation).FindControl(Ctl_Other_Explanation), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_OTHER_EXPLANATION).ToString
                'REQ-5585 End

                ctlTxt = CType(e.Row.Cells(COL_Claim_Number).FindControl(Ctl_Claim_Number), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_CLAIM_NUMBER).ToString

                ctlTxt = CType(e.Row.Cells(COL_Extended_Status_Code).FindControl(Ctl_Extended_Status_Code), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_EXTENDED_STATUS_CODE).ToString

                ctlTxt = CType(e.Row.Cells(COL_Extended_Status_Date).FindControl(Ctl_Extended_Status_Date), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlCanlendar = CType(e.Row.Cells(COL_Delivery_Date).FindControl(Ctl_Extended_Status_date_Calendar), ImageButton)
                If (ctlCanlendar IsNot Nothing) Then
                    AddCalendar(ctlCanlendar, ctlTxt)
                End If
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_EXTENDED_STATUS_DATE).ToString

                ctlTxt = CType(e.Row.Cells(COL_Tracking_Number).FindControl(Ctl_Tracking_Number), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_TRACKING_NUMBER).ToString

                ctlTxt = CType(e.Row.Cells(COL_Deductible_Included).FindControl(Ctl_Deductile_Included), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(ClaimloadReconWrk.COL_NAME_DEDUCTILE_INCLUDED).ToString


            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            If IsDataGPageDirty() Then
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
            Else
                State.PageIndex = NewCurrentPageIndex(Grid, State.RecordCount, State.PageSize)
                PopulateGrid()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub


    Protected Sub BtnViewParts_Click(sender As Object, e As System.EventArgs)
        Dim claimReconWorkID As String = CType(Grid.Rows(Grid.SelectedIndex).FindControl("lblReconWrkID"), Label).Text
        Dim claimReconWorkIDGUID As Guid = GetGuidFromString(claimReconWorkID)
        State.ClaimReconWrkId = claimReconWorkIDGUID
        PopulatePartsGrid(claimReconWorkIDGUID)

        'update the contents in the detail panel
        updPnlParts.Update()
        'show the modal popup
        mdlPopup.Show()
    End Sub
#End Region

#Region "Populate"
    Private Sub PopulatePartsGrid(guid As Guid)

        Dim dv As DataView

        Try
            If State.PartsHashTable Is Nothing Then
                dv = GetPartsDV()
            Else
                If State.PartsHashTable.Contains(guid) Then
                    dv = CType(State.PartsHashTable.Item(guid), DataSet).Tables(ClaimReconWrkPartsDAL.TABLE_NAME).DefaultView
                Else
                    dv = GetPartsDV()
                End If
            End If

            gvPop.DataSource = dv
            gvPop.DataBind()
            'ControlMgr.DisableAllGridControlsIfNotEditAuth(Me, gvPop)

        Catch ex As Exception
            HandleErrors(ex, ErrController2)
        End Try

    End Sub

    Private Function GetPartsDV() As DataView
        Dim dv As DataView

        dv = GetPartsDataSet().Tables(ClaimReconWrkPartsDAL.TABLE_NAME).DefaultView
        Return dv
    End Function

    Private Function GetPartsDataSet() As DataSet
        With State
            Return (Assurant.ElitaPlus.BusinessObjectsNew.ClaimReconWrkParts.LoadList(.ClaimReconWrkId))
        End With
    End Function

    Private Sub PopulateBOFromPartsForm(claimReconWrkPartsInfo As ClaimReconWrkParts)
        PopulatePartsBOItem(claimReconWrkPartsInfo, PART_NUMBER, PART_NUMBER_COL)
        PopulatePartsBOItem(claimReconWrkPartsInfo, PART_SKU, PART_SKU_COL)
        PopulatePartsBOItem(claimReconWrkPartsInfo, PART_DESCRIPTION, PART_DESCRIPTION_COL)
        If ErrCollection.Count > 0 Then
            Throw New PopulateBOErrorException
        End If
    End Sub

    Private Sub PopulatePartsBOItem(claimReconWrkPartsInfo As ClaimReconWrkParts, oPropertyName As String, oCellPosition As Integer)
        PopulateBundlesBOProperty(claimReconWrkPartsInfo, oPropertyName, _
                                        CType(GetSelectedGridControl(gvPop, oCellPosition), TextBox))
    End Sub
#End Region

#Region "button event handlers"
    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        Try
            If IsDataGPageDirty() Then
                DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSavePagePromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Dim retType As New ClaimLoadForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.ClaimloadfileName)
                ReturnToCallingPage(retType)
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(sender As Object, e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            PopulateGrid()
            HiddenIsPageDirty.Value = String.Empty
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub SaveButton_WRITE_Click(sender As Object, e As System.EventArgs) Handles SaveButton_WRITE.Click
        Try
            SavePage()
            Select Case SaveParts()
                Case 1, 2
                    DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
            End Select
            HiddenIsPageDirty.Value = String.Empty
            PopulateGrid()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub


    Protected Sub btnApply_Click(sender As System.Object, e As System.EventArgs) Handles btnApply.Click
        Dim hashTable As New Hashtable
        ApplyParts()
    End Sub
#End Region

#Region "Controlling Logic"

    Private Sub ApplyParts()
        Try
            Dim claimReconWrkPartsInfo As ClaimReconWrkParts

            Dim ds As DataSet = GetPartsDataSet()

            For i As Integer = 0 To ds.Tables(ClaimReconWrkPartsDAL.TABLE_NAME).Rows.Count - 1
                With ds.Tables(ClaimReconWrkPartsDAL.TABLE_NAME).Rows
                    gvPop.SelectedIndex = i
                    Dim oClaimReconWrkParts As New ClaimReconWrkParts(.Item(i), ds)
                    BindBoPropertiesToPartsGridHeaders(oClaimReconWrkParts)
                    PopulateBOFromPartsForm(oClaimReconWrkParts)
                    oClaimReconWrkParts.Validate()

                End With
            Next

            If State.PartsHashTable Is Nothing Then
                State.PartsHashTable = New Hashtable
            End If
            If State.PartsHashTable.Contains(State.ClaimReconWrkId) Then
                State.PartsHashTable.Item(State.ClaimReconWrkId) = ds
            Else
                State.PartsHashTable.Add(State.ClaimReconWrkId, ds)
            End If

            updPnlParts.Update()
            'hide the modal popup
            mdlPopup.Hide()
        Catch ex As Exception
            HandleErrors(ex, ErrController2)
            mdlPopup.Show()

        End Try
    End Sub

    Private Function SaveParts() As Integer
        Dim dv As DataView = ClaimloadReconWrk.LoadList(State.ClaimloadfileName)
        Dim ds As DataSet

        For i As Integer = 0 To Grid.Rows.Count - 1
            Dim claimReconWrkIdLabel As String = CType(Grid.Rows(i).FindControl("lblReconWrkID"), Label).Text.Trim
            Dim claimReconWrkIdLabelID As Guid = GetGuidFromString(claimReconWrkIdLabel)
            If State.PartsHashTable IsNot Nothing Then
                If State.PartsHashTable.ContainsKey(claimReconWrkIdLabelID) Then
                    ds = CType(State.PartsHashTable.Item(claimReconWrkIdLabelID), DataSet)
                    If ds.HasChanges Then
                        For Each row As DataRow In ds.Tables(ClaimReconWrkPartsDAL.TABLE_NAME).Rows
                            If row.RowState = DataRowState.Unchanged Then
                                row.Delete()
                            End If
                        Next
                        ds.AcceptChanges()
                        Dim claimReconWrkPartsInfo As New ClaimReconWrkParts()
                        Dim ret As Integer = claimReconWrkPartsInfo.SaveParts(ds)
                        If ret = 0 Then
                            ds = GetPartsDataSet()
                            State.PartsHashTable.Item(claimReconWrkIdLabelID) = ds
                            Return 2
                        Else
                            'Me.ErrController.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_WRITE_ERROR)
                        End If
                    End If
                End If
            End If
        Next
        Return 1

    End Function
#End Region




 
End Class