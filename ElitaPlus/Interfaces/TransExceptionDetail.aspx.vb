
Option Strict On
Option Explicit On

Imports Microsoft.VisualBasic
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Interfaces

    Partial Class TransExceptionDetail
        Inherits ElitaPlusSearchPage

        Private Class PageStatus

            Public Sub New()
                statusPageIndex = 0
                statusPageCount = 0
                partPageIndex = 0
                partPageCount = 0
                followupPageIndex = 0
                followupPageCount = 0
            End Sub

        End Class

#Region "Member Variables"
        Public Shared statusPageIndex As Integer
        Public Shared statusPageCount As Integer
        Public Shared partPageIndex As Integer
        Public Shared partPageCount As Integer
        Public Shared followupPageIndex As Integer
        Public Shared followupPageCount As Integer
        Public rejectionMsgLbl As String
#End Region

#Region "Page State"
        Class MyState
            Public Shared statusPageIndex As Integer = 0
            Public Shared statusPageCount As Integer = 0
            Public Shared partPageIndex As Integer = 0
            Public Shared partPageCount As Integer = 0
            Public Shared followupPageIndex As Integer = 0
            Public Shared followupPageCount As Integer = 0
            Public bnoRow As Boolean = False
            Public transactionLogHeaderId As String
            Public ErrorCode As String = ""
            Public reprocess As String = "N"
            Public hide As String = String.Empty
            Public resend As String = String.Empty
            Public originator As String = String.Empty
            Public functionTypeCode As String = String.Empty
            Public dvTransData As TransactionLogHeader.TransactionDataDV = Nothing
            Public dvStatus As TransactionLogHeader.TransactionStatusDV = Nothing
            Public dvPart As TransactionLogHeader.TransactionPartDV = Nothing
            Public dvFollowup As TransactionLogHeader.TransactionFollowUpDV = Nothing
            Public ActionInProgress As DetailPageCommand = DetailPageCommand.Nothing_
            Public cmdResendOrHide As String = String.Empty
            Public rejectionMsg As String = String.Empty
            Public IsComunaEnabled As Boolean = False
            Public IsEditMode As Boolean = False
            Public OldComunaValue As String = String.Empty
            Public OldServiceTypeValue As String = String.Empty
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Public ReadOnly Property COMUNALIST() As DataView
            Get
                If _ComunaList Is Nothing Then
                    _ComunaList = ComunaStandardization.GetComunaList()
                End If
                Return _ComunaList
            End Get
        End Property

        'Public ReadOnly Property ServiceTypeList() As DataView
        '    Get
        '        If _ServiceTypeList Is Nothing Then
        '            _ServiceTypeList = LookupListNew.GetServiceTypeLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        '        End If
        '        Return _ServiceTypeList
        '    End Get
        'End Property

        Public ReadOnly Property ServiceTypeList() As DataElements.ListItem()
            Get
                If _ServiceTypeList Is Nothing Then
                    _ServiceTypeList = CommonConfigManager.Current.ListManager.GetList(listCode:="STYP", languageCode:=Thread.CurrentPrincipal.GetLanguageCode())
                End If
                Return _ServiceTypeList
            End Get
        End Property

#End Region

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Constants"
        Public Const URL As String = "TransExceptionDetail.aspx"

        Private Const PART_DESCRIPTION_COL As Integer = 1
        Private Const ID_COL As Integer = 2
        Private Const DESCRIPTION_COL As Integer = 3
        Private Const IN_STOCK_ID_COL As Integer = 4
        Private Const COST_COL As Integer = 5
        Private Const VIEW_COST_COL As Integer = 3

        Private Const DESCRIPTION_CONTROL_NAME As String = "DescriptionDropDownList"
        Private Const IN_STOCK_ID_CONTROL_NAME As String = "InStockDropDownList"
        Private Const COST_CONTROL_NAME As String = "CostTextBox"
        Private Const ID_CONTROL_NAME As String = "IdLabel"
        Private Const EDITBUTTON_WRITE_CONTROL_NAME As String = "EditButton_WRITE"
        Private Const DELETEBUTTON_WRITE_CONTROL_NAME As String = "DeleteButton_WRITE"

        Private Const EDIT_COMMAND As String = "SelectAction"
        Private Const DELETE_COMMAND As String = "DeleteRecord"
        Private Const NEW_COMMAND As String = "NewRecord"
        Private Const SORT_COMMAND As String = "Sort"

        Private Const ALREADY_RESEND_OR_HIDE As String = "Y"
        Private Const ORIGINATOR_GVS As String = "GVS"
        Private Const ORIGINATOR_ASSURANT As String = "Assurant"

        Private Const NO_ROW_SELECTED_INDEX As Integer = -1
        Private Const AUTH_DETAIL_TAB As Integer = 1
        Private Const PARTS_INFO_TAB As Integer = 0

        Private Const DV_ID_COL As Integer = 0
        Public Shared RETURN_URL As String = "/ElitaPlus/Interfaces/TransExceptionDetail.aspx"
        Private _ComunaList As DataView
        'Private _ServiceTypeList As DataView
        Private _ServiceTypeList As DataElements.ListItem()

        Private Const MSG_NO_CERT_FOUND As String = "NO CERTIFICATE FOUND"
        Private Const MSG_CERT_NOT_FOUND As String = "CERTIFICATE NOT FOUND"
        Private Const MSG_NO_AVAILABLE_CERTIFICATE As String = "NO AVAILABLE CERTIFICATE"
        Private Const MSG_INVALID_SERVICE_TYPE As String = "INVALID SERVICE TYPE"

#End Region

#Region "Page Return Type"
        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As String
            Public HasDataChanged As Boolean
            Public Sub New(ByVal LastOp As DetailPageCommand, ByVal curEditingBo As String, ByVal hasDataChanged As Boolean)
                Me.LastOperation = LastOp
                Me.EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Private Methods"
        Private Sub Page_PageCall(ByVal CallFromUrl As String, ByVal CallingPar As Object) Handles MyBase.PageCall
            Try
                If Not Me.CallingParameters Is Nothing Then
                    Dim params As ArrayList = CType(Me.CallingParameters, ArrayList)
                    If Not params Is Nothing AndAlso params.Count > 1 Then
                        Me.State.transactionLogHeaderId = GuidControl.GuidToHexString(CType(params(0), Guid))
                        Me.State.reprocess = CType(params(1), String)
                        Me.State.ErrorCode = CType(params(2), String)
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try

        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            Try
                ErrController.Clear_Hide()
                GetRejectionMessage()

                If Not Page.IsPostBack Then
                    Me.SetGridItemStyleColor(Me.StatusGridView)
                    Me.SetGridItemStyleColor(Me.PartsGridView)
                    Me.SetGridItemStyleColor(Me.FollowUpGridView)
                    Me.ShowMissingTranslations(ErrController)
                    Me.State.statusPageIndex = 0
                    Me.State.partPageIndex = 0
                    Me.State.followupPageIndex = 0
                    Me.State.IsComunaEnabled = ComunaEnabled
                    PopulateFormFromBO()
                    PopulateGrid()
                    SetButtonsState()
                    If Me.State.IsComunaEnabled Then PopulateComunaFromBO()
                End If
                ClearErrLabels()
                CheckIfComingFromSaveConfirm()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Protected Sub GetRejectionMessage()
            rejectionMsgLbl = TranslationBase.TranslateLabelOrMessage("REJECTION_MESSAGE", ElitaPlusIdentity.Current.ActiveUser.LanguageId) & ": "
            rejectionMsgLbl = rejectionMsgLbl & Me.State.ErrorCode            
        End Sub

        Private Sub PopulateGrid()
            Try
                If Me.State.dvStatus Is Nothing Then
                    Me.State.dvStatus = TransactionLogHeader.GetStatusList(Me.State.transactionLogHeaderId)
                End If

                If Me.State.dvPart Is Nothing Then
                    Me.State.dvPart = TransactionLogHeader.GetPartList(Me.State.transactionLogHeaderId)
                End If

                If Me.State.dvFollowup Is Nothing Then
                    Me.State.dvFollowup = TransactionLogHeader.GetFollowUpList(Me.State.transactionLogHeaderId)
                End If

                Me.SetPageAndSelectedIndexFromGuid(Me.State.dvStatus, Guid.Empty, Me.StatusGridView, Me.State.statusPageIndex)
                Me.SetPageAndSelectedIndexFromGuid(Me.State.dvPart, Guid.Empty, Me.PartsGridView, Me.State.partPageIndex)
                Me.SetPageAndSelectedIndexFromGuid(Me.State.dvFollowup, Guid.Empty, Me.FollowUpGridView, Me.State.followupPageIndex)

                Me.TranslateGridHeader(Me.StatusGridView)
                Me.TranslateGridHeader(Me.PartsGridView)
                Me.TranslateGridHeader(Me.FollowUpGridView)
                Me.TranslateGridControls(Me.StatusGridView)
                Me.TranslateGridControls(Me.PartsGridView)
                Me.TranslateGridControls(Me.FollowUpGridView)

                If Me.State.dvStatus.Count = 0 Then
                    Dim dt As DataTable = Me.State.dvStatus.Table.Clone()
                    Dim dvEmpty As New TransactionLogHeader.TransactionStatusDV(dt)
                    TransactionLogHeader.TransactionStatusDV.AddNewRowToEmptyDV(dvEmpty)
                    SetPageAndSelectedIndexFromGuid(dvEmpty, Guid.Empty, Me.StatusGridView, Me.State.statusPageIndex, False)
                    SortAndBindGrid(Me.StatusGridView, dvEmpty, True, TransactionLogHeader.TransactionStatusDV.COL_EXTENDED_STATUS_DATE)
                Else
                    SetPageAndSelectedIndexFromGuid(Me.State.dvStatus, Guid.Empty, Me.StatusGridView, Me.State.statusPageIndex, False)
                    SortAndBindGrid(Me.StatusGridView, Me.State.dvStatus, False, TransactionLogHeader.TransactionStatusDV.COL_EXTENDED_STATUS_DATE)
                End If

                If Me.State.dvPart.Count = 0 Then
                    Dim dt As DataTable = Me.State.dvPart.Table.Clone()
                    Dim dvEmpty As New TransactionLogHeader.TransactionPartDV(dt)
                    TransactionLogHeader.TransactionPartDV.AddNewRowToEmptyDV(dvEmpty)
                    SetPageAndSelectedIndexFromGuid(dvEmpty, Guid.Empty, Me.PartsGridView, Me.State.partPageIndex, False)
                    SortAndBindGrid(Me.PartsGridView, dvEmpty, True, TransactionLogHeader.TransactionPartDV.COL_PART_CODE)
                Else
                    SetPageAndSelectedIndexFromGuid(Me.State.dvPart, Guid.Empty, Me.PartsGridView, Me.State.partPageIndex, False)
                    SortAndBindGrid(Me.PartsGridView, Me.State.dvPart, False, TransactionLogHeader.TransactionPartDV.COL_PART_CODE)
                End If

                If Me.State.dvFollowup.Count = 0 Then
                    Dim dt As DataTable = Me.State.dvFollowup.Table.Clone()
                    Dim dvEmpty As New TransactionLogHeader.TransactionFollowUpDV(dt)
                    TransactionLogHeader.TransactionFollowUpDV.AddNewRowToEmptyDV(dvEmpty)
                    SetPageAndSelectedIndexFromGuid(dvEmpty, Guid.Empty, Me.FollowUpGridView, Me.State.followupPageIndex, False)
                    SortAndBindGrid(Me.FollowUpGridView, dvEmpty, True, TransactionLogHeader.TransactionFollowUpDV.COL_COMMENT_CREATED_DATE)
                Else
                    SetPageAndSelectedIndexFromGuid(Me.State.dvFollowup, Guid.Empty, Me.FollowUpGridView, Me.State.followupPageIndex, False)
                    SortAndBindGrid(Me.FollowUpGridView, Me.State.dvFollowup, False, TransactionLogHeader.TransactionFollowUpDV.COL_COMMENT_CREATED_DATE)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub SortAndBindGrid(ByVal gridView As GridView, ByVal dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False, Optional ByVal sortExpression As String = "1")
            gridView.DataSource = dvBinding
            HighLightSortColumn(gridView, sortExpression)
            gridView.DataBind()
            If Not gridView.BottomPagerRow.Visible Then gridView.BottomPagerRow.Visible = True

            If blnEmptyList Then
                For Each gvRow As GridViewRow In gridView.Rows
                    gvRow.Controls.Clear()
                Next
            End If
        End Sub

        Private Sub PopulateComunaFromBO()
            Try
                If Me.State.dvTransData Is Nothing Then Me.State.dvTransData = TransactionLogHeader.GetTransactionData(Me.State.transactionLogHeaderId)

                If Not Me.State.dvTransData Is Nothing AndAlso Me.State.dvTransData.Count = 1 Then
                    With Me.State.dvTransData
                        Try
                            Me.SetSelectedItemByText(Me.moComunaDropdown, Me.State.dvTransData(0).Item(.COL_POSTAL_CODE).ToString)
                        Catch ex As Exception
                            'The value is not in the comuna code list, get it from the Standerzation table
                            Dim dvComunaStanderzation As DataView = ComunaStandardization.GetComunaStanderization(Me.State.dvTransData(0).Item(.COL_POSTAL_CODE).ToString)
                            Try
                                Me.SetSelectedItem(Me.moComunaDropdown, New Guid(CType(dvComunaStanderzation.Item(0)("COMUNA_CODE_ID"), Byte())))
                            Catch ex_New As Exception
                                'Throw New GUIException(Message.MSG_GUI_INVALID_VALUE, Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR_COMUNA_NOT_FOUND, ex)
                            End Try
                        End Try
                    End With
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub
        Private Sub PopulateFormFromBO()
            Try
                Me.State.dvTransData = TransactionLogHeader.GetTransactionData(Me.State.transactionLogHeaderId)

                If Not Me.State.dvTransData Is Nothing AndAlso Me.State.dvTransData.Count = 1 Then
                    With Me.State.dvTransData
                        Me.State.hide = If(Me.State.dvTransData(0).Item(.COL_HIDE) Is DBNull.Value, "", CType(Me.State.dvTransData(0).Item(.COL_HIDE), String))
                        Me.State.resend = If(Me.State.dvTransData(0).Item(.COL_RESEND) Is DBNull.Value, "", CType(Me.State.dvTransData(0).Item(.COL_RESEND), String))
                        Me.State.originator = CType(Me.State.dvTransData(0).Item(.COL_ORIGINATOR), String)
                        Me.State.functionTypeCode = CType(Me.State.dvTransData(0).Item(.COL_FUNCTION_TYPE_CODE), String)

                        Me.PopulateControlFromBOProperty(Me.TextboxOriginator, Me.State.originator)
                        ControlMgr.SetEnableControl(Me, Me.TextboxOriginator, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxFunction, Me.State.dvTransData(0).Item(.COL_FUNCTION_TYPE))
                        ControlMgr.SetEnableControl(Me, Me.TextboxFunction, False)

                        'REQ-391 Input GVS Chile claims without certificate
                        If Not Me.State.dvTransData(0).Item(.COL_SERVICE_TYPE) Is Nothing OrElse Me.State.dvTransData(0).Item(.COL_SERVICE_TYPE).Equals(String.Empty) Then
                            If Not Me.State.ErrorCode Is Nothing AndAlso (Me.State.ErrorCode.ToUpper.Equals(MSG_CERT_NOT_FOUND) _
                                   Or Me.State.ErrorCode.ToUpper.Equals(MSG_NO_CERT_FOUND) _
                                   Or Me.State.ErrorCode.ToUpper.Equals(MSG_NO_AVAILABLE_CERTIFICATE) _
                                   Or Me.State.ErrorCode.ToUpper.Equals(MSG_INVALID_SERVICE_TYPE)) Then
                                Me.cboServiceType.Visible = True
                                Me.TextboxServiceType.Visible = False

                                'Me.BindListControlToDataView(Me.cboServiceType, ServiceTypeList, "Description", "id", True)
                                Me.cboServiceType.Populate(ServiceTypeList.ToArray(), New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })

                                'Dim serviceTypeId As Guid = LookupListNew.GetIdFromDescription(ServiceTypeList, Me.State.dvTransData(0).Item(.COL_SERVICE_TYPE).ToString)
                                Dim serviceTypeId As Guid = (From lst In ServiceTypeList
                                                             Where lst.Translation = Me.State.dvTransData(0).Item(.COL_SERVICE_TYPE).ToString()
                                                             Select lst.ListItemId).FirstOrDefault()

                                Me.PopulateControlFromBOProperty(Me.cboServiceType, serviceTypeId)
                                ControlMgr.SetEnableControl(Me, Me.cboServiceType, False)
                                Me.State.OldServiceTypeValue = Me.State.dvTransData(0).Item(.COL_SERVICE_TYPE).ToString
                            Else
                                Me.PopulateControlFromBOProperty(Me.TextboxServiceType, Me.State.dvTransData(0).Item(.COL_SERVICE_TYPE))
                                ControlMgr.SetEnableControl(Me, Me.TextboxServiceType, False)
                            End If
                        End If

                        Me.PopulateControlFromBOProperty(Me.TextboxCUSTOMER_NAME, Me.State.dvTransData(0).Item(.COL_CUSTOMER_NAME))
                        ControlMgr.SetEnableControl(Me, Me.TextboxCUSTOMER_NAME, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxTAX_ID, Me.State.dvTransData(0).Item(.COL_TAX_ID))
                        ControlMgr.SetEnableControl(Me, Me.TextboxTAX_ID, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxADDRESS1, Me.State.dvTransData(0).Item(.COL_ADDRESS1))
                        ControlMgr.SetEnableControl(Me, Me.TextboxADDRESS1, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxADDRESS2, Me.State.dvTransData(0).Item(.COL_ADDRESS2))
                        ControlMgr.SetEnableControl(Me, Me.TextboxADDRESS2, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxPOSTAL_CODE, Me.State.dvTransData(0).Item(.COL_POSTAL_CODE))
                        ControlMgr.SetEnableControl(Me, Me.TextboxPOSTAL_CODE, False)
                        ControlMgr.SetEnableControl(Me, Me.moComunaDropdown, Me.State.IsComunaEnabled And Me.State.IsEditMode)
                        If Me.State.IsComunaEnabled Then Me.State.OldComunaValue = Me.State.dvTransData(0).Item(.COL_POSTAL_CODE).ToString
                        Me.PopulateControlFromBOProperty(Me.TextboxSTATE, Me.State.dvTransData(0).Item(.COL_STATE))
                        ControlMgr.SetEnableControl(Me, Me.TextboxSTATE, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxPHONE1, Me.State.dvTransData(0).Item(.COL_PHONE1))
                        ControlMgr.SetEnableControl(Me, Me.TextboxPHONE1, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxPHONE2, Me.State.dvTransData(0).Item(.COL_PHONE2))
                        ControlMgr.SetEnableControl(Me, Me.TextboxPHONE2, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxEMAIL, Me.State.dvTransData(0).Item(.COL_EMAIL))
                        ControlMgr.SetEnableControl(Me, Me.TextboxEMAIL, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxRETAILER, Me.State.dvTransData(0).Item(.COL_RETAILER))
                        ControlMgr.SetEnableControl(Me, Me.TextboxRETAILER, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxINVOICE_NUMBER, Me.State.dvTransData(0).Item(.COL_INVOICE_NUMBER))
                        ControlMgr.SetEnableControl(Me, Me.TextboxINVOICE_NUMBER, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxPRODUCT_SALES_DATE, Me.State.dvTransData(0).Item(.COL_PROD_SALES_DATE))
                        ControlMgr.SetEnableControl(Me, Me.TextboxPRODUCT_SALES_DATE, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxSALES_PRICE, Me.State.dvTransData(0).Item(.COL_PROD_SALES_PRICE))
                        ControlMgr.SetEnableControl(Me, Me.TextboxSALES_PRICE, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxMODEL, Me.State.dvTransData(0).Item(.COL_MODEL))
                        ControlMgr.SetEnableControl(Me, Me.TextboxMODEL, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxSERIAL_NUMBER, Me.State.dvTransData(0).Item(.COL_SERIAL_NUMBER))
                        ControlMgr.SetEnableControl(Me, Me.TextboxSERIAL_NUMBER, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxCLAIM_NUMBER, Me.State.dvTransData(0).Item(.COL_CLAIM_NUMBER))
                        ControlMgr.SetEnableControl(Me, Me.TextboxCLAIM_NUMBER, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxSERVICE_CENTER_CODE, Me.State.dvTransData(0).Item(.COL_SERVICE_CENTER_CODE))
                        ControlMgr.SetEnableControl(Me, Me.TextboxSERVICE_CENTER_CODE, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxGVS_SERVICE_ORDER_NUMBER, Me.State.dvTransData(0).Item(.COL_GVS_SERVICE_ORDER_NUM))
                        ControlMgr.SetEnableControl(Me, Me.TextboxGVS_SERVICE_ORDER_NUMBER, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxMETHOD_OF_REPAIR, Me.State.dvTransData(0).Item(.COL_METHOD_OF_REPAIR))
                        ControlMgr.SetEnableControl(Me, Me.TextboxMETHOD_OF_REPAIR, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxDATE_CLAIM_OPENED, Me.State.dvTransData(0).Item(.COL_CLAIM_CREATED_DATE))
                        ControlMgr.SetEnableControl(Me, Me.TextboxDATE_CLAIM_OPENED, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxPROBLEM_DESCRIPTION, Me.State.dvTransData(0).Item(.COL_PROBLEM_DESCRIPTION))
                        ControlMgr.SetEnableControl(Me, Me.TextboxPROBLEM_DESCRIPTION, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxTECHNICAL_REPORT, Me.State.dvTransData(0).Item(.COL_TECHNICAL_REPORT))
                        ControlMgr.SetEnableControl(Me, Me.TextboxTECHNICAL_REPORT, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxCLAIM_ACTIVITY, Me.State.dvTransData(0).Item(.COL_CLAIMACTIVITY_CODE))
                        ControlMgr.SetEnableControl(Me, Me.TextboxCLAIM_ACTIVITY, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxREPAIR_DATE, Me.State.dvTransData(0).Item(.COL_REPAIR_DATE))
                        ControlMgr.SetEnableControl(Me, Me.TextboxREPAIR_DATE, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxPICKUP_DATE, Me.State.dvTransData(0).Item(.COL_PICKUP_DATE))
                        ControlMgr.SetEnableControl(Me, Me.TextboxPICKUP_DATE, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxSCHEDULED_VISIT_DATE, Me.State.dvTransData(0).Item(.COL_SCHEDULE_VISIT_DATE))
                        ControlMgr.SetEnableControl(Me, Me.TextboxSCHEDULED_VISIT_DATE, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxEXPECTED_REPAIR_DATE, Me.State.dvTransData(0).Item(.COL_EXPECTED_REPAIR_DATE))
                        ControlMgr.SetEnableControl(Me, Me.TextboxEXPECTED_REPAIR_DATE, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxVISIT_DATE, Me.State.dvTransData(0).Item(.COL_VISIT_DATE))
                        ControlMgr.SetEnableControl(Me, Me.TextboxVISIT_DATE, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxDEFECT_REASON, Me.State.dvTransData(0).Item(.COL_DEFECT_REASON_CODE))
                        ControlMgr.SetEnableControl(Me, Me.TextboxDEFECT_REASON, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxLIABILITY_LIMIT, Me.State.dvTransData(0).Item(.COL_LIABILITY_LIMIT_AMOUNT))
                        ControlMgr.SetEnableControl(Me, Me.TextboxLIABILITY_LIMIT, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxLIABILITY_LIMIT_PERCENT, Me.State.dvTransData(0).Item(.COL_LIABILITY_LIMIT_PERCENT))
                        ControlMgr.SetEnableControl(Me, Me.TextboxLIABILITY_LIMIT_PERCENT, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxDEDUCTIBLE_AMOUNT, Me.State.dvTransData(0).Item(.COL_DEDUCTIBLE_AMOUNT))
                        ControlMgr.SetEnableControl(Me, Me.TextboxDEDUCTIBLE_AMOUNT, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxDEDUCTIBLE_PERCENT, Me.State.dvTransData(0).Item(.COL_DEDUCTIBLE_PERCENT))
                        ControlMgr.SetEnableControl(Me, Me.TextboxDEDUCTIBLE_PERCENT, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxLABOR_AMT, Me.State.dvTransData(0).Item(.COL_LABOR_AMOUNT))
                        ControlMgr.SetEnableControl(Me, Me.TextboxLABOR_AMT, False)
                        Me.PopulateControlFromBOProperty(Me.TextboxSHIPPING, Me.State.dvTransData(0).Item(.COL_shipping))
                        ControlMgr.SetEnableControl(Me, Me.TextboxSHIPPING, False)
                    End With
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub SetButtonsState()
            ControlMgr.SetEnableControl(Me, Me.btnBack, True)

            If (Me.State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_UPDATE_CLAIM Or _
                Me.State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_CANCEL_SVC_INTEGRATION Or _
                Me.State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_TRANSACTION_UPDATE Or _
                Me.State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_INSERT_NEW_CLAIM) AndAlso _
                Not (Me.State.hide = ALREADY_RESEND_OR_HIDE Or Me.State.resend = ALREADY_RESEND_OR_HIDE) AndAlso _
                Me.State.reprocess = "Y" Then
                btnResend_WRITE.Text = TranslationBase.TranslateLabelOrMessage("REPROCESS", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                ControlMgr.SetEnableControl(Me, Me.btnResend_WRITE, True)
            ElseIf (Me.State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_GVS_NEW_CLAIM Or _
                Me.State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_GVS_UPDATE_CLAIM Or _
                Me.State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_GVS_UPDATE_SVC) AndAlso _
                Not (Me.State.hide = ALREADY_RESEND_OR_HIDE Or Me.State.resend = ALREADY_RESEND_OR_HIDE) Then
                ControlMgr.SetEnableControl(Me, Me.btnResend_WRITE, True)
            Else
                ControlMgr.SetVisibleControl(Me, Me.btnResend_WRITE, False)
            End If

            If Not (Me.State.hide = ALREADY_RESEND_OR_HIDE Or Me.State.resend = ALREADY_RESEND_OR_HIDE) Then
                ControlMgr.SetEnableControl(Me, Me.btnHide_Write, True)
            Else
                ControlMgr.SetVisibleControl(Me, Me.btnHide_Write, False)
            End If

            ControlMgr.SetVisibleControl(Me, Me.btnUndo_Write, Me.State.IsComunaEnabled)
            ControlMgr.SetVisibleControl(Me, Me.btnEdit_WRITE, Me.State.IsComunaEnabled)
            ControlMgr.SetVisibleControl(Me, Me.btnAdd_WRITE, Me.State.IsComunaEnabled)
            ControlMgr.SetVisibleControl(Me, Me.moComunaDropdown, Me.State.IsComunaEnabled)
            ControlMgr.SetVisibleControl(Me, Me.LabelComuna, Me.State.IsComunaEnabled)

        End Sub

#End Region

#Region "DataViewRelated "
        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles StatusGridView.Sorting
            'Try
            '    Dim spaceIndex As Integer = Me.SortDirection.LastIndexOf(" ")

            '    If spaceIndex > 0 AndAlso Me.SortDirection.Substring(0, spaceIndex).Equals(e.SortExpression) Then
            '        If Me.SortDirection.EndsWith(" ASC") Then
            '            Me.SortDirection = e.SortExpression + " DESC"
            '        Else
            '            Me.SortDirection = e.SortExpression + " ASC"
            '        End If
            '    Else
            '        Me.SortDirection = e.SortExpression + " ASC"
            '    End If
            '    'Me.State.SortExpression = Me.SortDirection
            '    Me.State.PageIndex = 0
            '    Me.PopulateGrid()
            'Catch ex As Exception
            '    Me.HandleErrors(ex, Me.ErrController)
            'End Try
        End Sub

        Public Property SortDirection() As String
            Get
                Return ViewState("SortDirection").ToString
            End Get
            Set(ByVal value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles StatusGridView.PageIndexChanging

        End Sub

        Protected Sub ItemBound_StatusGridView(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles StatusGridView.RowDataBound
            Try
                BaseItemBound(source, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Protected Sub ItemBound_PartsGridView(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles PartsGridView.RowDataBound
            Try
                BaseItemBound(source, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PartsGridView.PageIndexChanged
            Try
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles PartsGridView.PageIndexChanging
            Try
                PartsGridView.PageIndex = e.NewPageIndex
                State.partPageIndex = PartsGridView.PageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub ItemBound_FollowUpGridView(ByVal source As Object, ByVal e As GridViewRowEventArgs) Handles FollowUpGridView.RowDataBound
            Try
                BaseItemBound(source, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Protected Sub ItemCreated_StatusGridView(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Protected Sub ItemCreated_PartsGridView(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Protected Sub ItemCreated_FollowUpGridView(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

#End Region
#Region "Clear"

        Private Sub ClearErrLabels()
            Me.ClearLabelErrSign(LabelServiceType)
        End Sub
#End Region
#Region "Button Click Handlers"

        Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
            Try
                If Me.State.IsComunaEnabled AndAlso Me.ComunaChanged Then
                    Me.AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, Me.HiddenSaveChangesPromptResponse)
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.transactionLogHeaderId, False))
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub btnResend_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResend_WRITE.Click
            Try
                'Resend confirmation
                If (Me.State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_UPDATE_CLAIM Or _
                    Me.State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_CANCEL_SVC_INTEGRATION Or _
                    Me.State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_TRANSACTION_UPDATE Or _
                    Me.State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_INSERT_NEW_CLAIM) Then
                    Me.DisplayMessage(Message.MSG_PROMPT_FOR_PROCESS_RECORDS, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Else
                    Me.DisplayMessage(Message.MSG_PROMPT_FOR_RESEND_TRANSACTION, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                End If

                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                Me.State.cmdResendOrHide = DALObjects.TransactionLogHeaderDAL.CMD_RESEND
                If Me.State.IsComunaEnabled Then
                    Me.State.IsEditMode = False
                    Me.EnableDisableFields()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub btnHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnHide_Write.Click
            Try
                'Hide confirmation
                Me.DisplayMessage(Message.MSG_PROMPT_FOR_HIDE_TRANSACTION, "", Me.MSG_BTN_YES_NO, Me.MSG_TYPE_CONFIRM, Me.HiddenSaveChangesPromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                Me.State.cmdResendOrHide = DALObjects.TransactionLogHeaderDAL.CMD_HIDE
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub btnUndo_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUndo_Write.Click
            'Get out of Edit mode
            Try
                UndoChanges()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub btnEdit_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit_WRITE.Click
            Try
                Me.State.IsEditMode = True
                Me.EnableDisableFields()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Private Sub btnAdd_Write_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_WRITE.Click
            Try
                'Call the Comuna Standardization screen (ComunaStandardizationForm.aspx)
                Me.callPage(ComunaStandardizationForm.URL, Nothing)

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception

            End Try

        End Sub
#End Region


#Region "Controlling Logic"

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = Me.HiddenSaveChangesPromptResponse.Value
            Try
                If Not confResponse Is Nothing AndAlso (confResponse = Me.MSG_VALUE_YES Or confResponse = Me.CONFIRM_MESSAGE_OK) Then
                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Accept
                            'Resend or Hide transaction
                            ResendOrHideTransaction()
                        Case ElitaPlusPage.DetailPageCommand.Back
                            ResendOrHideTransaction()
                    End Select
                ElseIf Not confResponse Is Nothing AndAlso (confResponse = Me.MSG_VALUE_NO Or confResponse = Me.CONFIRM_MESSAGE_CANCEL) Then
                    Select Case Me.State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Accept
                            ' Do nothing
                        Case ElitaPlusPage.DetailPageCommand.Back
                            Me.ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, Me.State.transactionLogHeaderId, False))
                    End Select

                End If

                'Clean after consuming the action
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                Me.HiddenSaveChangesPromptResponse.Value = ""
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
            End Try
        End Sub

        Protected Function ResendOrHideTransaction() As Boolean
            Try
                Dim outputParameters() As DALObjects.DBHelper.DBHelperParameter
                Dim newServiceTypeValue As String = Nothing

                If Me.cboServiceType.Visible Then
                    Dim serviceTypeGuid As Guid = Me.GetSelectedItem(Me.cboServiceType)
                    If serviceTypeGuid.Equals(Guid.Empty) Then
                        ElitaPlusPage.SetLabelError(LabelServiceType)
                        Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_SERVICE_TYPE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_SERVICE_TYPE)
                    End If
                    'If ServiceTypeChanged() Then newServiceTypeValue = LookupListNew.GetCodeFromId(ServiceTypeList, serviceTypeGuid)
                    If ServiceTypeChanged() Then newServiceTypeValue = (From lst In ServiceTypeList
                                                                        Where lst.ListItemId = serviceTypeGuid
                                                                        Select lst.Code).FirstOrDefault()
                End If
                If Me.State.IsComunaEnabled AndAlso ComunaChanged() Then
                    outputParameters = TransactionLogHeader.ResendOrHideTransaction(Me.State.cmdResendOrHide, New Guid(GuidControl.HexToByteArray(Me.State.transactionLogHeaderId)), Me.moComunaDropdown.SelectedItem.Text, newServiceTypeValue)
                Else
                    outputParameters = TransactionLogHeader.ResendOrHideTransaction(Me.State.cmdResendOrHide, New Guid(GuidControl.HexToByteArray(Me.State.transactionLogHeaderId)), Nothing, newServiceTypeValue)
                End If


                If CType(outputParameters(0).Value, Integer) = 0 Then
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                    Me.DisplayMessageWithSubmit(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                Else
                    Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                    Me.DisplayMessageWithSubmit(CType(outputParameters(1).Value, String), "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
                End If

                PopulateFormFromBO()
                PopulateGrid()
                SetButtonsState()
                If Me.State.IsComunaEnabled Then
                    Me.State.IsEditMode = False
                    Me.EnableDisableFields()
                End If

                Return True
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
                Return False
            End Try
        End Function

        Protected Function ComunaEnabled() As Boolean
            '1-Obtain the user's country.
            '2-load the postal code format(s) for this country
            '3-Loop thr until you find one with the COMUNA_ENABLED = 'Y'; else it is a NO
            Try
                Dim blnIsComunaEnabled As Boolean
                Dim oCountry As Country = ElitaPlusIdentity.Current.ActiveUser.Country(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
                Dim oCountryPostalFormat As DataView = oCountry.GetCountryPostalFormat(oCountry.Id)
                For Each Row As DataRow In oCountryPostalFormat.Table.Rows
                    If Row(PostalCodeFormat.COL_NAME_COMUNA_ENABLED) Is DBNull.Value Then
                        blnIsComunaEnabled = False
                    Else
                        blnIsComunaEnabled = CType(Row(PostalCodeFormat.COL_NAME_COMUNA_ENABLED), String) = "Y"
                    End If
                    If blnIsComunaEnabled Then
                        PopulateComunaDropDown()
                        ShowHideComunaButtons(blnIsComunaEnabled)
                        Exit For
                    End If

                Next
                Return blnIsComunaEnabled
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrController)
                Return False
            End Try
        End Function

        Protected Sub PopulateComunaDropDown()
            Me.BindListControlToDataView(Me.moComunaDropdown, COMUNALIST, "Description", "id", True)
        End Sub

        Protected Sub ShowHideComunaButtons(ByVal blnIsComunaEnabled As Boolean)
            ControlMgr.SetVisibleControl(Me, Me.btnEdit_WRITE, blnIsComunaEnabled)
            ControlMgr.SetVisibleControl(Me, Me.btnAdd_WRITE, blnIsComunaEnabled)
            ControlMgr.SetVisibleControl(Me, Me.btnUndo_Write, blnIsComunaEnabled)
            ControlMgr.SetEnableControl(Me, Me.btnEdit_WRITE, blnIsComunaEnabled)
            ControlMgr.SetEnableControl(Me, Me.btnUndo_Write, False)
            ControlMgr.SetEnableControl(Me, Me.btnAdd_WRITE, blnIsComunaEnabled)
        End Sub

        Protected Sub EnableDisableFields()

            If (Me.State.IsEditMode) Then
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnEdit_WRITE, False, True)
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnAdd_WRITE, False, True)
                ControlMgr.SetEnableControl(Me, Me.btnUndo_Write, True)
                ControlMgr.SetEnableControl(Me, Me.cboServiceType, True)
                ControlMgr.SetEnableControl(Me, Me.moComunaDropdown, Me.State.IsComunaEnabled)
            Else
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnEdit_WRITE, True, True)
                ControlMgr.SetVisibleForControlFamily(Me, Me.btnAdd_WRITE, True, True)
                ControlMgr.SetEnableControl(Me, Me.btnUndo_Write, False)
                ControlMgr.SetEnableControl(Me, Me.btnAdd_WRITE, True)
                ControlMgr.SetEnableControl(Me, Me.cboServiceType, False)
                ControlMgr.SetEnableControl(Me, Me.moComunaDropdown, False)
            End If


        End Sub

        Private Sub UndoChanges()
            Me.State.IsEditMode = False
            Me.PopulateFormFromBO()
            Me.EnableDisableFields()
            'Me.MenuEnabled = True
            Me.ErrController.Clear_Hide()            
        End Sub

        Protected Function ComunaChanged() As Boolean

            If Not Me.moComunaDropdown.SelectedItem.Text.Equals(String.Empty) Then
                If (Me.moComunaDropdown.SelectedItem.Text = Me.State.OldComunaValue) Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If

        End Function
        Protected Function ServiceTypeChanged() As Boolean

            If Not Me.cboServiceType.SelectedItem.Text.Equals(String.Empty) Then
                If (Me.cboServiceType.SelectedItem.Text = Me.State.OldServiceTypeValue) Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If

        End Function

        'Protected Sub UpdateComuna()
        '    Try
        '        If Me.State.IsEditMode AndAlso Me.ComunaChanged Then
        '            TransactionLogHeader.UpdateComuna(New Guid(GuidControl.HexToByteArray(Me.State.transactionLogHeaderId)), Me.moComunaDropdown.SelectedItem.Text)
        '            Me.State.IsEditMode = False
        '            Me.EnableDisableFields()
        '            PopulateFormFromBO()
        '            PopulateComunaFromBO()
        '            Me.AddInfoMsg(Message.SAVE_RECORD_CONFIRMATION)
        '        Else
        '            Me.AddInfoMsg(Message.MSG_RECORD_NOT_SAVED)
        '        End If
        '    Catch ex As Exception
        '        Me.HandleErrors(ex, Me.ErrController)
        '    End Try
        'End Sub
#End Region


    End Class


End Namespace
