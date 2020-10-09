
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

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
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
            Public Sub New(LastOp As DetailPageCommand, curEditingBo As String, hasDataChanged As Boolean)
                LastOperation = LastOp
                EditingBo = curEditingBo
                Me.HasDataChanged = hasDataChanged
            End Sub
        End Class
#End Region

#Region "Private Methods"
        Private Sub Page_PageCall(CallFromUrl As String, CallingPar As Object) Handles MyBase.PageCall
            Try
                If CallingParameters IsNot Nothing Then
                    Dim params As ArrayList = CType(CallingParameters, ArrayList)
                    If params IsNot Nothing AndAlso params.Count > 1 Then
                        State.transactionLogHeaderId = GuidControl.GuidToHexString(CType(params(0), Guid))
                        State.reprocess = CType(params(1), String)
                        State.ErrorCode = CType(params(2), String)
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try

        End Sub

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            Try
                ErrController.Clear_Hide()
                GetRejectionMessage()

                If Not Page.IsPostBack Then
                    SetGridItemStyleColor(StatusGridView)
                    SetGridItemStyleColor(PartsGridView)
                    SetGridItemStyleColor(FollowUpGridView)
                    ShowMissingTranslations(ErrController)
                    State.statusPageIndex = 0
                    State.partPageIndex = 0
                    State.followupPageIndex = 0
                    State.IsComunaEnabled = ComunaEnabled
                    PopulateFormFromBO()
                    PopulateGrid()
                    SetButtonsState()
                    If State.IsComunaEnabled Then PopulateComunaFromBO()
                End If
                ClearErrLabels()
                CheckIfComingFromSaveConfirm()
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Protected Sub GetRejectionMessage()
            rejectionMsgLbl = TranslationBase.TranslateLabelOrMessage("REJECTION_MESSAGE", ElitaPlusIdentity.Current.ActiveUser.LanguageId) & ": "
            rejectionMsgLbl = rejectionMsgLbl & State.ErrorCode            
        End Sub

        Private Sub PopulateGrid()
            Try
                If State.dvStatus Is Nothing Then
                    State.dvStatus = TransactionLogHeader.GetStatusList(State.transactionLogHeaderId)
                End If

                If State.dvPart Is Nothing Then
                    State.dvPart = TransactionLogHeader.GetPartList(State.transactionLogHeaderId)
                End If

                If State.dvFollowup Is Nothing Then
                    State.dvFollowup = TransactionLogHeader.GetFollowUpList(State.transactionLogHeaderId)
                End If

                SetPageAndSelectedIndexFromGuid(State.dvStatus, Guid.Empty, StatusGridView, State.statusPageIndex)
                SetPageAndSelectedIndexFromGuid(State.dvPart, Guid.Empty, PartsGridView, State.partPageIndex)
                SetPageAndSelectedIndexFromGuid(State.dvFollowup, Guid.Empty, FollowUpGridView, State.followupPageIndex)

                TranslateGridHeader(StatusGridView)
                TranslateGridHeader(PartsGridView)
                TranslateGridHeader(FollowUpGridView)
                TranslateGridControls(StatusGridView)
                TranslateGridControls(PartsGridView)
                TranslateGridControls(FollowUpGridView)

                If State.dvStatus.Count = 0 Then
                    Dim dt As DataTable = State.dvStatus.Table.Clone()
                    Dim dvEmpty As New TransactionLogHeader.TransactionStatusDV(dt)
                    TransactionLogHeader.TransactionStatusDV.AddNewRowToEmptyDV(dvEmpty)
                    SetPageAndSelectedIndexFromGuid(dvEmpty, Guid.Empty, StatusGridView, State.statusPageIndex, False)
                    SortAndBindGrid(StatusGridView, dvEmpty, True, TransactionLogHeader.TransactionStatusDV.COL_EXTENDED_STATUS_DATE)
                Else
                    SetPageAndSelectedIndexFromGuid(State.dvStatus, Guid.Empty, StatusGridView, State.statusPageIndex, False)
                    SortAndBindGrid(StatusGridView, State.dvStatus, False, TransactionLogHeader.TransactionStatusDV.COL_EXTENDED_STATUS_DATE)
                End If

                If State.dvPart.Count = 0 Then
                    Dim dt As DataTable = State.dvPart.Table.Clone()
                    Dim dvEmpty As New TransactionLogHeader.TransactionPartDV(dt)
                    TransactionLogHeader.TransactionPartDV.AddNewRowToEmptyDV(dvEmpty)
                    SetPageAndSelectedIndexFromGuid(dvEmpty, Guid.Empty, PartsGridView, State.partPageIndex, False)
                    SortAndBindGrid(PartsGridView, dvEmpty, True, TransactionLogHeader.TransactionPartDV.COL_PART_CODE)
                Else
                    SetPageAndSelectedIndexFromGuid(State.dvPart, Guid.Empty, PartsGridView, State.partPageIndex, False)
                    SortAndBindGrid(PartsGridView, State.dvPart, False, TransactionLogHeader.TransactionPartDV.COL_PART_CODE)
                End If

                If State.dvFollowup.Count = 0 Then
                    Dim dt As DataTable = State.dvFollowup.Table.Clone()
                    Dim dvEmpty As New TransactionLogHeader.TransactionFollowUpDV(dt)
                    TransactionLogHeader.TransactionFollowUpDV.AddNewRowToEmptyDV(dvEmpty)
                    SetPageAndSelectedIndexFromGuid(dvEmpty, Guid.Empty, FollowUpGridView, State.followupPageIndex, False)
                    SortAndBindGrid(FollowUpGridView, dvEmpty, True, TransactionLogHeader.TransactionFollowUpDV.COL_COMMENT_CREATED_DATE)
                Else
                    SetPageAndSelectedIndexFromGuid(State.dvFollowup, Guid.Empty, FollowUpGridView, State.followupPageIndex, False)
                    SortAndBindGrid(FollowUpGridView, State.dvFollowup, False, TransactionLogHeader.TransactionFollowUpDV.COL_COMMENT_CREATED_DATE)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub SortAndBindGrid(gridView As GridView, dvBinding As DataView, Optional ByVal blnEmptyList As Boolean = False, Optional ByVal sortExpression As String = "1")
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
                If State.dvTransData Is Nothing Then State.dvTransData = TransactionLogHeader.GetTransactionData(State.transactionLogHeaderId)

                If State.dvTransData IsNot Nothing AndAlso State.dvTransData.Count = 1 Then
                    With State.dvTransData
                        Try
                            SetSelectedItemByText(moComunaDropdown, State.dvTransData(0).Item(.COL_POSTAL_CODE).ToString)
                        Catch ex As Exception
                            'The value is not in the comuna code list, get it from the Standerzation table
                            Dim dvComunaStanderzation As DataView = ComunaStandardization.GetComunaStanderization(State.dvTransData(0).Item(.COL_POSTAL_CODE).ToString)
                            Try
                                SetSelectedItem(moComunaDropdown, New Guid(CType(dvComunaStanderzation.Item(0)("COMUNA_CODE_ID"), Byte())))
                            Catch ex_New As Exception
                                'Throw New GUIException(Message.MSG_GUI_INVALID_VALUE, Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR_COMUNA_NOT_FOUND, ex)
                            End Try
                        End Try
                    End With
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub
        Private Sub PopulateFormFromBO()
            Try
                State.dvTransData = TransactionLogHeader.GetTransactionData(State.transactionLogHeaderId)

                If State.dvTransData IsNot Nothing AndAlso State.dvTransData.Count = 1 Then
                    With State.dvTransData
                        State.hide = If(State.dvTransData(0).Item(.COL_HIDE) Is DBNull.Value, "", CType(State.dvTransData(0).Item(.COL_HIDE), String))
                        State.resend = If(State.dvTransData(0).Item(.COL_RESEND) Is DBNull.Value, "", CType(State.dvTransData(0).Item(.COL_RESEND), String))
                        State.originator = CType(State.dvTransData(0).Item(.COL_ORIGINATOR), String)
                        State.functionTypeCode = CType(State.dvTransData(0).Item(.COL_FUNCTION_TYPE_CODE), String)

                        PopulateControlFromBOProperty(TextboxOriginator, State.originator)
                        ControlMgr.SetEnableControl(Me, TextboxOriginator, False)
                        PopulateControlFromBOProperty(TextboxFunction, State.dvTransData(0).Item(.COL_FUNCTION_TYPE))
                        ControlMgr.SetEnableControl(Me, TextboxFunction, False)

                        'REQ-391 Input GVS Chile claims without certificate
                        If State.dvTransData(0).Item(.COL_SERVICE_TYPE) IsNot Nothing OrElse State.dvTransData(0).Item(.COL_SERVICE_TYPE).Equals(String.Empty) Then
                            If State.ErrorCode IsNot Nothing AndAlso (State.ErrorCode.ToUpper.Equals(MSG_CERT_NOT_FOUND) _
                                   OrElse State.ErrorCode.ToUpper.Equals(MSG_NO_CERT_FOUND) _
                                   OrElse State.ErrorCode.ToUpper.Equals(MSG_NO_AVAILABLE_CERTIFICATE) _
                                   OrElse State.ErrorCode.ToUpper.Equals(MSG_INVALID_SERVICE_TYPE)) Then
                                cboServiceType.Visible = True
                                TextboxServiceType.Visible = False

                                'Me.BindListControlToDataView(Me.cboServiceType, ServiceTypeList, "Description", "id", True)
                                cboServiceType.Populate(ServiceTypeList.ToArray(), New PopulateOptions() With
                                    {
                                        .AddBlankItem = True
                                    })

                                'Dim serviceTypeId As Guid = LookupListNew.GetIdFromDescription(ServiceTypeList, Me.State.dvTransData(0).Item(.COL_SERVICE_TYPE).ToString)
                                Dim serviceTypeId As Guid = (From lst In ServiceTypeList
                                                             Where lst.Translation = State.dvTransData(0).Item(.COL_SERVICE_TYPE).ToString()
                                                             Select lst.ListItemId).FirstOrDefault()

                                PopulateControlFromBOProperty(cboServiceType, serviceTypeId)
                                ControlMgr.SetEnableControl(Me, cboServiceType, False)
                                State.OldServiceTypeValue = State.dvTransData(0).Item(.COL_SERVICE_TYPE).ToString
                            Else
                                PopulateControlFromBOProperty(TextboxServiceType, State.dvTransData(0).Item(.COL_SERVICE_TYPE))
                                ControlMgr.SetEnableControl(Me, TextboxServiceType, False)
                            End If
                        End If

                        PopulateControlFromBOProperty(TextboxCUSTOMER_NAME, State.dvTransData(0).Item(.COL_CUSTOMER_NAME))
                        ControlMgr.SetEnableControl(Me, TextboxCUSTOMER_NAME, False)
                        PopulateControlFromBOProperty(TextboxTAX_ID, State.dvTransData(0).Item(.COL_TAX_ID))
                        ControlMgr.SetEnableControl(Me, TextboxTAX_ID, False)
                        PopulateControlFromBOProperty(TextboxADDRESS1, State.dvTransData(0).Item(.COL_ADDRESS1))
                        ControlMgr.SetEnableControl(Me, TextboxADDRESS1, False)
                        PopulateControlFromBOProperty(TextboxADDRESS2, State.dvTransData(0).Item(.COL_ADDRESS2))
                        ControlMgr.SetEnableControl(Me, TextboxADDRESS2, False)
                        PopulateControlFromBOProperty(TextboxPOSTAL_CODE, State.dvTransData(0).Item(.COL_POSTAL_CODE))
                        ControlMgr.SetEnableControl(Me, TextboxPOSTAL_CODE, False)
                        ControlMgr.SetEnableControl(Me, moComunaDropdown, State.IsComunaEnabled AndAlso State.IsEditMode)
                        If State.IsComunaEnabled Then State.OldComunaValue = State.dvTransData(0).Item(.COL_POSTAL_CODE).ToString
                        PopulateControlFromBOProperty(TextboxSTATE, State.dvTransData(0).Item(.COL_STATE))
                        ControlMgr.SetEnableControl(Me, TextboxSTATE, False)
                        PopulateControlFromBOProperty(TextboxPHONE1, State.dvTransData(0).Item(.COL_PHONE1))
                        ControlMgr.SetEnableControl(Me, TextboxPHONE1, False)
                        PopulateControlFromBOProperty(TextboxPHONE2, State.dvTransData(0).Item(.COL_PHONE2))
                        ControlMgr.SetEnableControl(Me, TextboxPHONE2, False)
                        PopulateControlFromBOProperty(TextboxEMAIL, State.dvTransData(0).Item(.COL_EMAIL))
                        ControlMgr.SetEnableControl(Me, TextboxEMAIL, False)
                        PopulateControlFromBOProperty(TextboxRETAILER, State.dvTransData(0).Item(.COL_RETAILER))
                        ControlMgr.SetEnableControl(Me, TextboxRETAILER, False)
                        PopulateControlFromBOProperty(TextboxINVOICE_NUMBER, State.dvTransData(0).Item(.COL_INVOICE_NUMBER))
                        ControlMgr.SetEnableControl(Me, TextboxINVOICE_NUMBER, False)
                        PopulateControlFromBOProperty(TextboxPRODUCT_SALES_DATE, State.dvTransData(0).Item(.COL_PROD_SALES_DATE))
                        ControlMgr.SetEnableControl(Me, TextboxPRODUCT_SALES_DATE, False)
                        PopulateControlFromBOProperty(TextboxSALES_PRICE, State.dvTransData(0).Item(.COL_PROD_SALES_PRICE))
                        ControlMgr.SetEnableControl(Me, TextboxSALES_PRICE, False)
                        PopulateControlFromBOProperty(TextboxMODEL, State.dvTransData(0).Item(.COL_MODEL))
                        ControlMgr.SetEnableControl(Me, TextboxMODEL, False)
                        PopulateControlFromBOProperty(TextboxSERIAL_NUMBER, State.dvTransData(0).Item(.COL_SERIAL_NUMBER))
                        ControlMgr.SetEnableControl(Me, TextboxSERIAL_NUMBER, False)
                        PopulateControlFromBOProperty(TextboxCLAIM_NUMBER, State.dvTransData(0).Item(.COL_CLAIM_NUMBER))
                        ControlMgr.SetEnableControl(Me, TextboxCLAIM_NUMBER, False)
                        PopulateControlFromBOProperty(TextboxSERVICE_CENTER_CODE, State.dvTransData(0).Item(.COL_SERVICE_CENTER_CODE))
                        ControlMgr.SetEnableControl(Me, TextboxSERVICE_CENTER_CODE, False)
                        PopulateControlFromBOProperty(TextboxGVS_SERVICE_ORDER_NUMBER, State.dvTransData(0).Item(.COL_GVS_SERVICE_ORDER_NUM))
                        ControlMgr.SetEnableControl(Me, TextboxGVS_SERVICE_ORDER_NUMBER, False)
                        PopulateControlFromBOProperty(TextboxMETHOD_OF_REPAIR, State.dvTransData(0).Item(.COL_METHOD_OF_REPAIR))
                        ControlMgr.SetEnableControl(Me, TextboxMETHOD_OF_REPAIR, False)
                        PopulateControlFromBOProperty(TextboxDATE_CLAIM_OPENED, State.dvTransData(0).Item(.COL_CLAIM_CREATED_DATE))
                        ControlMgr.SetEnableControl(Me, TextboxDATE_CLAIM_OPENED, False)
                        PopulateControlFromBOProperty(TextboxPROBLEM_DESCRIPTION, State.dvTransData(0).Item(.COL_PROBLEM_DESCRIPTION))
                        ControlMgr.SetEnableControl(Me, TextboxPROBLEM_DESCRIPTION, False)
                        PopulateControlFromBOProperty(TextboxTECHNICAL_REPORT, State.dvTransData(0).Item(.COL_TECHNICAL_REPORT))
                        ControlMgr.SetEnableControl(Me, TextboxTECHNICAL_REPORT, False)
                        PopulateControlFromBOProperty(TextboxCLAIM_ACTIVITY, State.dvTransData(0).Item(.COL_CLAIMACTIVITY_CODE))
                        ControlMgr.SetEnableControl(Me, TextboxCLAIM_ACTIVITY, False)
                        PopulateControlFromBOProperty(TextboxREPAIR_DATE, State.dvTransData(0).Item(.COL_REPAIR_DATE))
                        ControlMgr.SetEnableControl(Me, TextboxREPAIR_DATE, False)
                        PopulateControlFromBOProperty(TextboxPICKUP_DATE, State.dvTransData(0).Item(.COL_PICKUP_DATE))
                        ControlMgr.SetEnableControl(Me, TextboxPICKUP_DATE, False)
                        PopulateControlFromBOProperty(TextboxSCHEDULED_VISIT_DATE, State.dvTransData(0).Item(.COL_SCHEDULE_VISIT_DATE))
                        ControlMgr.SetEnableControl(Me, TextboxSCHEDULED_VISIT_DATE, False)
                        PopulateControlFromBOProperty(TextboxEXPECTED_REPAIR_DATE, State.dvTransData(0).Item(.COL_EXPECTED_REPAIR_DATE))
                        ControlMgr.SetEnableControl(Me, TextboxEXPECTED_REPAIR_DATE, False)
                        PopulateControlFromBOProperty(TextboxVISIT_DATE, State.dvTransData(0).Item(.COL_VISIT_DATE))
                        ControlMgr.SetEnableControl(Me, TextboxVISIT_DATE, False)
                        PopulateControlFromBOProperty(TextboxDEFECT_REASON, State.dvTransData(0).Item(.COL_DEFECT_REASON_CODE))
                        ControlMgr.SetEnableControl(Me, TextboxDEFECT_REASON, False)
                        PopulateControlFromBOProperty(TextboxLIABILITY_LIMIT, State.dvTransData(0).Item(.COL_LIABILITY_LIMIT_AMOUNT))
                        ControlMgr.SetEnableControl(Me, TextboxLIABILITY_LIMIT, False)
                        PopulateControlFromBOProperty(TextboxLIABILITY_LIMIT_PERCENT, State.dvTransData(0).Item(.COL_LIABILITY_LIMIT_PERCENT))
                        ControlMgr.SetEnableControl(Me, TextboxLIABILITY_LIMIT_PERCENT, False)
                        PopulateControlFromBOProperty(TextboxDEDUCTIBLE_AMOUNT, State.dvTransData(0).Item(.COL_DEDUCTIBLE_AMOUNT))
                        ControlMgr.SetEnableControl(Me, TextboxDEDUCTIBLE_AMOUNT, False)
                        PopulateControlFromBOProperty(TextboxDEDUCTIBLE_PERCENT, State.dvTransData(0).Item(.COL_DEDUCTIBLE_PERCENT))
                        ControlMgr.SetEnableControl(Me, TextboxDEDUCTIBLE_PERCENT, False)
                        PopulateControlFromBOProperty(TextboxLABOR_AMT, State.dvTransData(0).Item(.COL_LABOR_AMOUNT))
                        ControlMgr.SetEnableControl(Me, TextboxLABOR_AMT, False)
                        PopulateControlFromBOProperty(TextboxSHIPPING, State.dvTransData(0).Item(.COL_shipping))
                        ControlMgr.SetEnableControl(Me, TextboxSHIPPING, False)
                    End With
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub SetButtonsState()
            ControlMgr.SetEnableControl(Me, btnBack, True)

            If (State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_UPDATE_CLAIM OrElse State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_CANCEL_SVC_INTEGRATION OrElse State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_TRANSACTION_UPDATE OrElse State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_INSERT_NEW_CLAIM) AndAlso _
                Not (State.hide = ALREADY_RESEND_OR_HIDE OrElse State.resend = ALREADY_RESEND_OR_HIDE) AndAlso _
                State.reprocess = "Y" Then
                btnResend_WRITE.Text = TranslationBase.TranslateLabelOrMessage("REPROCESS", ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                ControlMgr.SetEnableControl(Me, btnResend_WRITE, True)
            ElseIf (State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_GVS_NEW_CLAIM OrElse State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_GVS_UPDATE_CLAIM OrElse State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_GVS_UPDATE_SVC) AndAlso _
                Not (State.hide = ALREADY_RESEND_OR_HIDE OrElse State.resend = ALREADY_RESEND_OR_HIDE) Then
                ControlMgr.SetEnableControl(Me, btnResend_WRITE, True)
            Else
                ControlMgr.SetVisibleControl(Me, btnResend_WRITE, False)
            End If

            If Not (State.hide = ALREADY_RESEND_OR_HIDE OrElse State.resend = ALREADY_RESEND_OR_HIDE) Then
                ControlMgr.SetEnableControl(Me, btnHide_Write, True)
            Else
                ControlMgr.SetVisibleControl(Me, btnHide_Write, False)
            End If

            ControlMgr.SetVisibleControl(Me, btnUndo_Write, State.IsComunaEnabled)
            ControlMgr.SetVisibleControl(Me, btnEdit_WRITE, State.IsComunaEnabled)
            ControlMgr.SetVisibleControl(Me, btnAdd_WRITE, State.IsComunaEnabled)
            ControlMgr.SetVisibleControl(Me, moComunaDropdown, State.IsComunaEnabled)
            ControlMgr.SetVisibleControl(Me, LabelComuna, State.IsComunaEnabled)

        End Sub

#End Region

#Region "DataViewRelated "
        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles StatusGridView.Sorting
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
            Set(value As String)
                ViewState("SortDirection") = value
            End Set
        End Property

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles StatusGridView.PageIndexChanging

        End Sub

        Protected Sub ItemBound_StatusGridView(source As Object, e As GridViewRowEventArgs) Handles StatusGridView.RowDataBound
            Try
                BaseItemBound(source, e)
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Protected Sub ItemBound_PartsGridView(source As Object, e As GridViewRowEventArgs) Handles PartsGridView.RowDataBound
            Try
                BaseItemBound(source, e)
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles PartsGridView.PageIndexChanged
            Try
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles PartsGridView.PageIndexChanging
            Try
                PartsGridView.PageIndex = e.NewPageIndex
                State.partPageIndex = PartsGridView.PageIndex
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub ItemBound_FollowUpGridView(source As Object, e As GridViewRowEventArgs) Handles FollowUpGridView.RowDataBound
            Try
                BaseItemBound(source, e)
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Protected Sub ItemCreated_StatusGridView(sender As Object, e As GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Protected Sub ItemCreated_PartsGridView(sender As Object, e As GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Protected Sub ItemCreated_FollowUpGridView(sender As Object, e As GridViewRowEventArgs)
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

#End Region
#Region "Clear"

        Private Sub ClearErrLabels()
            ClearLabelErrSign(LabelServiceType)
        End Sub
#End Region
#Region "Button Click Handlers"

        Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
            Try
                If State.IsComunaEnabled AndAlso ComunaChanged Then
                    AddConfirmMsg(Message.SAVE_CHANGES_PROMPT, HiddenSaveChangesPromptResponse)
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
                Else
                    ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.transactionLogHeaderId, False))
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub btnResend_Click(sender As System.Object, e As System.EventArgs) Handles btnResend_WRITE.Click
            Try
                'Resend confirmation
                If (State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_UPDATE_CLAIM OrElse State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_CANCEL_SVC_INTEGRATION OrElse State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_TRANSACTION_UPDATE OrElse State.functionTypeCode = DALObjects.TransactionLogHeaderDAL.FUNCTION_TYPE_CODE_ELITA_INSERT_NEW_CLAIM) Then
                    DisplayMessage(Message.MSG_PROMPT_FOR_PROCESS_RECORDS, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                Else
                    DisplayMessage(Message.MSG_PROMPT_FOR_RESEND_TRANSACTION, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                End If

                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                State.cmdResendOrHide = DALObjects.TransactionLogHeaderDAL.CMD_RESEND
                If State.IsComunaEnabled Then
                    State.IsEditMode = False
                    EnableDisableFields()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub btnHide_Click(sender As System.Object, e As System.EventArgs) Handles btnHide_Write.Click
            Try
                'Hide confirmation
                DisplayMessage(Message.MSG_PROMPT_FOR_HIDE_TRANSACTION, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, HiddenSaveChangesPromptResponse)
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Accept
                State.cmdResendOrHide = DALObjects.TransactionLogHeaderDAL.CMD_HIDE
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub btnUndo_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnUndo_Write.Click
            'Get out of Edit mode
            Try
                UndoChanges()
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub btnEdit_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnEdit_WRITE.Click
            Try
                State.IsEditMode = True
                EnableDisableFields()
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Private Sub btnAdd_Write_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click
            Try
                'Call the Comuna Standardization screen (ComunaStandardizationForm.aspx)
                callPage(ComunaStandardizationForm.URL, Nothing)

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception

            End Try

        End Sub
#End Region


#Region "Controlling Logic"

        Protected Sub CheckIfComingFromSaveConfirm()
            Dim confResponse As String = HiddenSaveChangesPromptResponse.Value
            Try
                If confResponse IsNot Nothing AndAlso (confResponse = MSG_VALUE_YES OrElse confResponse = CONFIRM_MESSAGE_OK) Then
                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Accept
                            'Resend or Hide transaction
                            ResendOrHideTransaction()
                        Case ElitaPlusPage.DetailPageCommand.Back
                            ResendOrHideTransaction()
                    End Select
                ElseIf confResponse IsNot Nothing AndAlso (confResponse = MSG_VALUE_NO OrElse confResponse = CONFIRM_MESSAGE_CANCEL) Then
                    Select Case State.ActionInProgress
                        Case ElitaPlusPage.DetailPageCommand.Accept
                            ' Do nothing
                        Case ElitaPlusPage.DetailPageCommand.Back
                            ReturnToCallingPage(New ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.transactionLogHeaderId, False))
                    End Select

                End If

                'Clean after consuming the action
                State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Nothing_
                HiddenSaveChangesPromptResponse.Value = ""
            Catch ex As Exception
                HandleErrors(ex, ErrController)
            End Try
        End Sub

        Protected Function ResendOrHideTransaction() As Boolean
            Try
                Dim outputParameters() As DALObjects.DBHelper.DBHelperParameter
                Dim newServiceTypeValue As String = Nothing

                If cboServiceType.Visible Then
                    Dim serviceTypeGuid As Guid = GetSelectedItem(cboServiceType)
                    If serviceTypeGuid.Equals(Guid.Empty) Then
                        ElitaPlusPage.SetLabelError(LabelServiceType)
                        Throw New GUIException(Assurant.ElitaPlus.Common.ErrorCodes.INVALID_SERVICE_TYPE, Assurant.ElitaPlus.Common.ErrorCodes.INVALID_SERVICE_TYPE)
                    End If
                    'If ServiceTypeChanged() Then newServiceTypeValue = LookupListNew.GetCodeFromId(ServiceTypeList, serviceTypeGuid)
                    If ServiceTypeChanged() Then newServiceTypeValue = (From lst In ServiceTypeList
                                                                        Where lst.ListItemId = serviceTypeGuid
                                                                        Select lst.Code).FirstOrDefault()
                End If
                If State.IsComunaEnabled AndAlso ComunaChanged() Then
                    outputParameters = TransactionLogHeader.ResendOrHideTransaction(State.cmdResendOrHide, New Guid(GuidControl.HexToByteArray(State.transactionLogHeaderId)), moComunaDropdown.SelectedItem.Text, newServiceTypeValue)
                Else
                    outputParameters = TransactionLogHeader.ResendOrHideTransaction(State.cmdResendOrHide, New Guid(GuidControl.HexToByteArray(State.transactionLogHeaderId)), Nothing, newServiceTypeValue)
                End If


                If CType(outputParameters(0).Value, Integer) = 0 Then
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                    DisplayMessageWithSubmit(Message.SAVE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
                Else
                    State.ActionInProgress = ElitaPlusPage.DetailPageCommand.OK
                    DisplayMessageWithSubmit(CType(outputParameters(1).Value, String), "", MSG_BTN_OK, MSG_TYPE_INFO)
                End If

                PopulateFormFromBO()
                PopulateGrid()
                SetButtonsState()
                If State.IsComunaEnabled Then
                    State.IsEditMode = False
                    EnableDisableFields()
                End If

                Return True
            Catch ex As Exception
                HandleErrors(ex, ErrController)
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
                HandleErrors(ex, ErrController)
                Return False
            End Try
        End Function

        Protected Sub PopulateComunaDropDown()
            BindListControlToDataView(moComunaDropdown, COMUNALIST, "Description", "id", True)
        End Sub

        Protected Sub ShowHideComunaButtons(blnIsComunaEnabled As Boolean)
            ControlMgr.SetVisibleControl(Me, btnEdit_WRITE, blnIsComunaEnabled)
            ControlMgr.SetVisibleControl(Me, btnAdd_WRITE, blnIsComunaEnabled)
            ControlMgr.SetVisibleControl(Me, btnUndo_Write, blnIsComunaEnabled)
            ControlMgr.SetEnableControl(Me, btnEdit_WRITE, blnIsComunaEnabled)
            ControlMgr.SetEnableControl(Me, btnUndo_Write, False)
            ControlMgr.SetEnableControl(Me, btnAdd_WRITE, blnIsComunaEnabled)
        End Sub

        Protected Sub EnableDisableFields()

            If (State.IsEditMode) Then
                ControlMgr.SetVisibleForControlFamily(Me, btnEdit_WRITE, False, True)
                ControlMgr.SetVisibleForControlFamily(Me, btnAdd_WRITE, False, True)
                ControlMgr.SetEnableControl(Me, btnUndo_Write, True)
                ControlMgr.SetEnableControl(Me, cboServiceType, True)
                ControlMgr.SetEnableControl(Me, moComunaDropdown, State.IsComunaEnabled)
            Else
                ControlMgr.SetVisibleForControlFamily(Me, btnEdit_WRITE, True, True)
                ControlMgr.SetVisibleForControlFamily(Me, btnAdd_WRITE, True, True)
                ControlMgr.SetEnableControl(Me, btnUndo_Write, False)
                ControlMgr.SetEnableControl(Me, btnAdd_WRITE, True)
                ControlMgr.SetEnableControl(Me, cboServiceType, False)
                ControlMgr.SetEnableControl(Me, moComunaDropdown, False)
            End If


        End Sub

        Private Sub UndoChanges()
            State.IsEditMode = False
            PopulateFormFromBO()
            EnableDisableFields()
            'Me.MenuEnabled = True
            ErrController.Clear_Hide()            
        End Sub

        Protected Function ComunaChanged() As Boolean

            If Not moComunaDropdown.SelectedItem.Text.Equals(String.Empty) Then
                If (moComunaDropdown.SelectedItem.Text = State.OldComunaValue) Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If

        End Function
        Protected Function ServiceTypeChanged() As Boolean

            If Not cboServiceType.SelectedItem.Text.Equals(String.Empty) Then
                If (cboServiceType.SelectedItem.Text = State.OldServiceTypeValue) Then
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
