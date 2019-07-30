Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.Common.MiscUtil
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.IO

Namespace Interfaces

    Partial Class DealerFileProcessedController_New
        Inherits System.Web.UI.UserControl


#Region "Constants"


        Public Const GRID_COL_DEALERFILE_PROCESSED_ID_IDX As Integer = 0
        Public Const GRID_COL_SELECT_IDX As Integer = 1
        Public Const GRID_COL_EDIT_IDX As Integer = 2
        Public Const GRID_COL_FILENAME_IDX As Integer = 3
        Public Const GRID_COL_RECEIVED_IDX As Integer = 4
        Public Const GRID_COL_COUNTED_IDX As Integer = 5
        Public Const GRID_COL_BYPASSED_IDX As Integer = 6
        Public Const GRID_COL_REJECTED_IDX As Integer = 7
        Public Const GRID_COL_REMAINING_REJECTED_IDX As Integer = 8
        Public Const GRID_COL_VALIDATED_IDX As Integer = 9
        Public Const GRID_COL_LOADED_IDX As Integer = 10
        Public Const GRID_COL_LAYOUT_IDX As Integer = 11
        Public Const GRID_COL_STATUS_IDX As Integer = 12
        Public Const GRID_COL_STATUS_DESC_IDX As Integer = 13

        ' For Paging
        Public Const GRID_COL_REC_COUNT_BYPASSED As Integer = 0
        Public Const GRID_COL_REC_COUNT_VALIDATED As Integer = 1
        Public Const GRID_COL_REC_COUNT_REJECTED As Integer = 2
        Public Const GRID_COL_REC_COUNT_REMAINING_REJECTED As Integer = 3
        Public Const GRID_COL_REC_COUNT_LOADED As Integer = 4


        Private Const SP_VALIDATE As Integer = 0
        Private Const SP_PROCESS As Integer = 1
        Private Const SP_DELETE As Integer = 2
        Private Const SP_DOWNLOAD As Integer = 3
        Private Const SP_GENRATE_RESPONSE As Integer = 4

        Private Const DEALER_FILE_REQUIRED As String = "DEALER_FILE_REQUIRED"
        Private Const DEALER_PAYMENT_FILE_SUBSTRING As String = "P"
        Private Const DEALER_REINSURANCE_FILE_SUBSTRING As String = "REINS"
        Private Const DEALER_INVOICE_FILE_SUBSTRING As String = "I"
        Private Const TELE_MRKT_FILE_SUBSTRING As String = "TMK"
        Private Const DEALERP_VARIABLE_NAME As String = "moDealerController_"
        Public Const SESSION_LOCALSTATE_KEY As String = "DEALERFILE_PROCESSEDCONTROLLER_SESSION_LOCALSTATE_KEY"
        Private Const ONE_ITEM As Integer = 1
        Private Const LABEL_SELECT_DEALER As String = "SELECT_DEALER"
        Private Const LABEL_SELECT_DEALER_GROUP As String = "SELECT_DEALER_GROUP"
        Private Const PORT As Integer = 21
        Private Const FIRST_POS As Integer = 0
        Private Const COL_NAME As String = "ID"
        Public Const VSCCode As String = "2"
        Public Const DealerType_VSC As String = "VSC"
        Public Const DealerType_ESC As String = "ESC"

        Public Const SHOW_COMMAND_REJECTED As String = "ShowRecordRej"
        Public Const SHOW_COMMAND_REMAINING_REJECTED As String = "ShowRecordRemRej"
        Public Const SHOW_COMMAND_VALIDATED As String = "ShowRecordVal"
        Public Const SHOW_COMMAND_LOADED As String = "ShowRecordLod"
        Public Const SHOW_COMMAND_BYPASSED As String = "ShowRecordByp"
        Public Const SHOW_COMMAND_STATUS As String = "ShowStatusDesc"

        Public Const QUERY_STRING_REJECTED As String = "?RECORDMODE=REJ"
        Public Const QUERY_STRING_REMAINING_REJECTED As String = "?RECORDMODE=REMREJ"
        Public Const QUERY_STRING_VALIDATED As String = "?RECORDMODE=VAL"
        Public Const QUERY_STRING_LOADED As String = "?RECORDMODE=LOD"
        Public Const QUERY_STRING_BYPASSED As String = "?RECORDMODE=BYP"

        Public Const GRID_LINK_BTN_BYPASSED As String = "BtnShowBypassed"
        Public Const GRID_LINK_BTN_REJECTED As String = "BtnShowRejected"
        Public Const GRID_LINK_BTN_REMAINING_REJECTED As String = "BtnShowRemainingRejected"
        Public Const GRID_LINK_BTN_VALIDATED As String = "BtnShowValidated"
        Public Const GRID_LINK_BTN_LOADED As String = "BtnShowLoaded"
        Public Const GRID_LINK_BTN_STATUS As String = "BtnShowStatus"

        Public Const FILE_STATUS_PENDING As String = "PENDING"
        Public Const FILE_STATUS_RUNNING As String = "RUNNING"
        Public Const FILE_STATUS_SUCCESS As String = "SUCCESS"
        Public Const FILE_STATUS_FAILURE As String = "FAILURE"

        Public Const Url_Payment_Recon_Wrk As String = "ARPaymentReconWrkForm.aspx"
        Public Const Count As String = "0"

        Public QUERY_STRING_PARENT_FILE As String = "&PARENTFILE="
        Public QUERY_STRING_PARENT_FILE1 As String = "?PARENTFILE="
        Public QUERY_STRING_REJREC_NOTUPDATABLE As String = "&REJRECNOTUPDATABLE=Y"

#End Region

#Region "Page State"

#Region "MyState"

        Public Class MyState
            Public SelectedDealerFileProcessedId As Guid = Guid.Empty
            Public SelectedFileName As String = String.Empty
            Public SelectedDealerFileLayout As String = ""
            Public SelectedDealerCode As String '= ""
            Public SelectedDealerGroupCode As String '= ""
            Public IsGridVisible As Boolean = False
            Public SelectedDealerId As Guid '= Guid.Empty
            Public SelectedDealerGroupId As Guid '= Guid.Empty
            Public IsParentFile As Boolean = False
            Public ErrorCtrl As ErrorController
            Public msUrlDetailPage As String
            Public msUrlPrintPage As String
            Public moInterfaceTypeCode As DealerFileProcessedData.InterfaceTypeCode
            Public mnPageIndex As Integer
            Public intStatusId As Guid
            Public errorStatus As InterfaceStatusWrk.IntError
            Public oDataView As DataView
            Public DealerType As String = ""
            Public dealerTypeVSC As String
            Public ErrControllerId As String
            Public Sub New(ByVal UrlDetailPage As String, ByVal UrlPrintPage As String,
            ByVal oInterfaceTypeCode As DealerFileProcessedData.InterfaceTypeCode)
                msUrlDetailPage = UrlDetailPage
                msUrlPrintPage = UrlPrintPage
                moInterfaceTypeCode = oInterfaceTypeCode
            End Sub
        End Class
#End Region

#Region "Page Return"
        Private IsReturningFromChild As Boolean = False

        Public Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object)
            Me.IsReturningFromChild = True
            Dim retObj As ReturnType = CType(ReturnPar, ReturnType)
            Select Case retObj.LastOperation
                Case ElitaPlusPage.DetailPageCommand.Back
                    If Not retObj Is Nothing Then
                        Try
                            Me.TheState.SelectedDealerFileProcessedId = retObj.SelectedDealerFileProcessedId
                            moDataGrid.PageIndex = Me.TheState.mnPageIndex
                            If Not Me.TheState.SelectedDealerFileProcessedId.Equals(Guid.Empty) Then
                                Me.TheState.IsGridVisible = True
                                PopulateDealerDropDown()
                                PopulateDealerGroupDropDown()
                                If Me.TheState.IsParentFile = True Then
                                    rdParentFile.Checked = True
                                    rdDealerFile.Checked = False
                                    multipleDropControl.SelectedIndex = -1
                                    multipleDropControl.ChangeEnabledControlProperty(False)
                                Else
                                    rdParentFile.Checked = False
                                    rdDealerFile.Checked = True
                                    multipleDropControl.ChangeEnabledControlProperty(True)
                                End If
                                ' PopulateDealerInterface()
                                Me.PopulateGrid(ThePage.POPULATE_ACTION_SAVE)
                                ThePage.SetGridItemStyleColor(Me.moDataGrid)
                                EnableDisableButtons()
                                ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                                If Me.TheState.IsParentFile = True Then
                                    ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, False, True)
                                Else
                                    ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                                End If
                            End If
                        Catch ex As Exception
                            ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
                        End Try
                    End If
            End Select
        End Sub

        Public Class ReturnType
            Public LastOperation As ElitaPlusPage.DetailPageCommand
            Public SelectedDealerFileProcessedId As Guid
            Public Sub New(ByVal LastOp As ElitaPlusPage.DetailPageCommand, ByVal selDealerFileProcessedId As Guid)
                Me.LastOperation = LastOp
                Me.SelectedDealerFileProcessedId = selDealerFileProcessedId
            End Sub
        End Class
#End Region

#End Region

#Region "Variables"
        Private moState As MyState
        Private MessageCtrl As MessageController
        Private DropDownIndexChangedController As Boolean = False
        ' Protected WithEvents moInterfaceProgressControl As InterfaceProgressControl
        Public Event CheckedChanged(ByVal aSrc As DealerFileProcessedController_New)
#End Region

#Region "Properties"

        'Public Property State() As MyState
        '    Get
        '        Return CType(ThePage.StateSession.Item(Me.UniqueID), MyState)
        '    End Get
        '    Set(ByVal Value As MyState)
        '        If ThePage.StateSession.ContainsKey(Me.UniqueID) = False Then
        '            ThePage.StateSession.Item(Me.UniqueID) = Value
        '            ClearAll()
        '        End If

        '    End Set
        'End Property

        Protected ReadOnly Property TheState() As MyState
            Get
                Try
                    If Me.moState Is Nothing Then
                        Me.moState = CType(Session(SESSION_LOCALSTATE_KEY), MyState)
                    End If
                    Return Me.moState
                Catch ex As Exception
                    'When we are in design mode there is no session object
                    Return Nothing
                End Try
            End Get
        End Property
        'Private ReadOnly Property ErrCtrl() As MessageController
        '    Get
        '        If Not Me.State.ErrControllerId Is Nothing Then
        '            Return CType(Me.Page.MasterPage.FindControl(Me.State.ErrControllerId), MessageController)
        '        End If
        '    End Get
        'End Property

        Private Shadows ReadOnly Property ThePage() As ElitaPlusSearchPage
            Get
                Return CType(MyBase.Page, ElitaPlusSearchPage)
            End Get
        End Property

        Public ReadOnly Property TheInterfaceProgress() As InterfaceProgressControl
            Get
                If moInterfaceProgressControl Is Nothing Then
                    moInterfaceProgressControl = CType(FindControl("moInterfaceProgressControl"), InterfaceProgressControl)
                End If
                Return moInterfaceProgressControl
            End Get
        End Property

        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl_New
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl_New)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property

        Public ReadOnly Property DealerGroupMultipleDrop() As MultipleColumnDDLabelControl_New
            Get
                If moDealerGroupMultipleDrop Is Nothing Then
                    moDealerGroupMultipleDrop = CType(FindControl("multipleDealerGrpDropControl"), MultipleColumnDDLabelControl_New)
                End If
                Return moDealerGroupMultipleDrop
            End Get
        End Property

        Public ReadOnly Property GetDealerTypeID() As Guid
            Get
                Dim dealertypeid As Guid
                Dim oDealertypeView As DataView = LookupListNew.GetDealerTypeId(Authentication.CurrentUser.CompanyGroup.Id)
                If oDealertypeView.Count > 0 Then
                    dealertypeid = GuidControl.ByteArrayToGuid(CType(oDealertypeView(FIRST_POS)(COL_NAME), Byte()))
                End If
                Return dealertypeid
            End Get
        End Property

#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl_New
        Protected WithEvents moDealerGroupMultipleDrop As MultipleColumnDDLabelControl_New
        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

#Region "Handlers-Init"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
            If IsReturningFromChild Then
                ValidateSelection()
                If (DealerMultipleDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Or DealerGroupMultipleDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Or rdParentFile.Checked) Then
                    TheState.IsGridVisible = True
                    Me.PopulateGrid(ThePage.POPULATE_ACTION_SAVE)
                    ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                    Select Case TheState.moInterfaceTypeCode
                        Case DealerFileProcessedData.InterfaceTypeCode.CERT
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, False)
                        Case DealerFileProcessedData.InterfaceTypeCode.PAYM
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, False)
                        Case DealerFileProcessedData.InterfaceTypeCode.TLMK
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, False)
                        Case DealerFileProcessedData.InterfaceTypeCode.RINS
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, False)
                            ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
                            ControlMgr.SetEnableControl(ThePage, BtnErrorExport, False)

                        Case DealerFileProcessedData.InterfaceTypeCode.INVC
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, False)
                            ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
                            ControlMgr.SetEnableControl(ThePage, BtnErrorExport, False)


                        Case Else
                            Return
                    End Select
                    If rdParentFile.Checked Then
                        ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, False, True)
                    Else
                        ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                    End If
                End If
            End If
        End Sub

        ' This method should be called for every pageload
        Public Sub SetErrorController(ByVal oErrorCtrl As MessageController)
            MessageCtrl = oErrorCtrl
        End Sub
        ' This is the initialization Method
        Public Sub InitController(ByVal UrlDetailPage As String, ByVal UrlPrintPage As String,
        ByVal oInterfaceTypeCode As DealerFileProcessedData.InterfaceTypeCode)

            '   State = New MyState(UrlDetailPage, UrlPrintPage, oInterfaceTypeCode)
            Me.moState = New MyState(UrlDetailPage, UrlPrintPage, oInterfaceTypeCode)
            Session(SESSION_LOCALSTATE_KEY) = Me.moState
            PopulateDealerDropDown()
            PopulateDealerGroupDropDown()
            'If State.IsGridVisible Then
            '    Me.PopulateGrid()
            'End If
            ThePage.SetGridItemStyleColor(Me.moDataGrid)
            '    LoadRequiredFieldControlData()
        End Sub

#End Region

#Region "Handlers-DropDown"

        Public Sub PopulateDealerInterface()
            Try
                ClearAll()
                ValidateSelection()
                If DealerMultipleDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Or DealerGroupMultipleDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Or rdParentFile.Checked Then
                    If (TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PAYM Or TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PYMT) AndAlso
                      Not DealerMultipleDrop.NothingSelected Then
                        Dim _dealer As Dealer = New Dealer(DealerMultipleDrop.SelectedGuid)
                        'If _dealer.AttributeValues.Value(Codes.ATTRIBUTE__DEFAULT_REINSURANCE_STATUS) Is Nothing Then
                        If Not _dealer.AttributeValues.Value(Codes.DLR_ATTRBT__NEW_INVOICE_PAYMENT) Is Nothing Then
                            If _dealer.AttributeValues.Value(Codes.DLR_ATTRBT__NEW_INVOICE_PAYMENT) = Codes.YESNO_Y Then
                                TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PYMT
                                Session(SESSION_LOCALSTATE_KEY) = Me.moState
                            End If
                        End If
                    End If

                    TheState.IsGridVisible = True
                    Me.PopulateGrid(ThePage.POPULATE_ACTION_NONE)
                    ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                    Select Case TheState.moInterfaceTypeCode
                        Case DealerFileProcessedData.InterfaceTypeCode.CERT
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, False)
                        Case DealerFileProcessedData.InterfaceTypeCode.PAYM
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, False)
                        Case DealerFileProcessedData.InterfaceTypeCode.TLMK
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnErrorExport, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnRejectReport, False)
                            ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, False)
                        Case DealerFileProcessedData.InterfaceTypeCode.RINS
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnErrorExport, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnRejectReport, False)
                        Case DealerFileProcessedData.InterfaceTypeCode.INVC
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnErrorExport, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnRejectReport, False)
                        Case DealerFileProcessedData.InterfaceTypeCode.PYMT
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnErrorExport, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnRejectReport, False)
                        Case Else
                            Return
                    End Select
                    If rdParentFile.Checked Then
                        ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, False, True)
                    Else
                        ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                    End If
                End If
            Catch ex As Exception
                ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
            Finally
                DropDownIndexChangedController = False
            End Try

        End Sub
#End Region

#Region "Handlers-Buttons"

        Private Sub btnCopyDealerFile_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyDealerFile_WRITE.Click
            Try
                uploadDealerFile()
                ThePage.DisplayMessage(Message.MSG_THE_FILE_TRANSFER_HAS_COMPLETED, "", ThePage.MSG_BTN_OK, ThePage.MSG_TYPE_INFO)
            Catch ex As Exception
                ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnValidate_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnValidate_WRITE.Click
            'Def-24499: Calling common method to validated dealer file records.
            ValidateAndProcessDealerFile(SP_VALIDATE)
        End Sub

        Private Sub BtnLoadCertificate_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLoadCertificate_WRITE.Click
            'Def-24499: Calling common method to process dealer file records.
            ValidateAndProcessDealerFile(SP_PROCESS)
        End Sub

        Private Sub BtnGenerateResponse_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnGenerateResponse.Click
            Try
                ExecuteAndWait(SP_GENRATE_RESPONSE)
                ThePage.MasterPage.MessageController.AddInformation(Message.MSG_GENERATE_RESPONSE_PROCESS_STARTED, True)
                ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, False)
            Catch ex As Exception
                ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnDeleteDealerFile_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDeleteDealerFile_WRITE.Click
            'Def-24499: Calling common method to deleted dealer file records.
            ValidateAndProcessDealerFile(SP_DELETE)
        End Sub

        'Def-24499: Added common method to validated, process and deleted dealer file records.
        Private Sub ValidateAndProcessDealerFile(ByVal oSP As Integer)
            Try
                If Not TheState.SelectedDealerFileProcessedId.Equals(Guid.Empty) Then

                    Dim ointerface As New InterfaceStatusWrk()
                    Dim status As Boolean = ointerface.IsStatus_Running(TheState.SelectedDealerFileProcessedId)
                    Dim oDealerFile As DealerFileProcessed

                    If rdParentFile.Checked = False Then
                        oDealerFile = New DealerFileProcessed(TheState.SelectedDealerFileProcessedId)
                    Else
                        oDealerFile = New DealerFileProcessed(TheState.SelectedDealerFileProcessedId, True)
                    End If
                    Dim res As Integer = CInt(CLng(oDealerFile.Loaded()))

                    Select Case oSP
                        Case SP_VALIDATE
                            If (status = True) Then
                                ThePage.MasterPage.MessageController.AddInformation(Message.MSG_FILE_CANNOT_BE_VALIDATED_PLEASE_REFRESH_THE_SCREEN, True)
                            Else
                                ExecuteAndWait(SP_VALIDATE)
                                ThePage.MasterPage.MessageController.AddInformation(Message.MSG_VALIDATE_PROCESS_STARTED, True)
                                ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
                            End If
                        Case SP_PROCESS
                            If (status = True) Then
                                ThePage.MasterPage.MessageController.AddInformation(Message.MSG_FILE_CANNOT_BE_PROCESSED_PLEASE_REFRESH_THE_SCREEN, True)
                            Else
                                ExecuteAndWait(SP_PROCESS)
                                ThePage.MasterPage.MessageController.AddInformation(Message.MSG_LOAD_PROCESS_STARTED, True)
                                ControlMgr.SetEnableControl(ThePage, BtnLoadCertificate_WRITE, False)
                            End If
                        Case SP_DELETE
                            If (status = True OrElse res > 0) Then
                                ThePage.MasterPage.MessageController.AddInformation(Message.MSG_FILE_CANNOT__BE_DELETED_PLEASE_REFRESH_THE_SCREEN, True)
                            Else
                                ExecuteAndWait(SP_DELETE)
                            End If
                    End Select
                Else
                    Throw New GUIException("You must select a dealer file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub BtnRejectReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnRejectReport.Click
            RejectReport(PrintDealerLoadRejectForm.REJECT_REPORT)
        End Sub

        Private Sub BtnErrorExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnErrorExport.Click
            RejectReport(PrintDealerLoadRejectForm.ERROR_EXPORT)
        End Sub
        Private Sub BtnProcessedExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnProcessedExport.Click
            RejectReport(PrintDealerLoadRejectForm.PROCESSED_EXPORT)
        End Sub

        Private Sub btnNewItemCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExtendedContentPopupCancel.Click
            mdlPopup.Hide()
        End Sub

        Private Sub RejectReport(ByVal reportType As Integer)
            Try
                If Not TheState.SelectedDealerFileProcessedId.Equals(Guid.Empty) Then
                    Dim param As New PrintDealerLoadRejectForm.MyState
                    param.DealerfileProcessedId = TheState.SelectedDealerFileProcessedId
                    param.parentFileName = TheState.SelectedFileName
                    param.moInterfaceTypeCode = TheState.moInterfaceTypeCode
                    param.reportType = reportType
                    param.dealertype = TheState.DealerType
                    If Not TheState.SelectedDealerId = Guid.Empty Then
                        param.SelectionCode = TheState.SelectedDealerCode
                    ElseIf Not TheState.SelectedDealerGroupId = Guid.Empty Then
                        param.SelectionCode = TheState.SelectedDealerGroupCode
                    End If

                    If rdParentFile.Checked Then
                        param.isParentFile = "Y"
                        'param.parentFileName = TheState.
                    Else
                        param.isParentFile = "N"
                    End If
                    ThePage.callPage(TheState.msUrlPrintPage, param)
                Else
                    Throw New GUIException("You must select a dealer file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
            End Try
        End Sub


#End Region

#Region "Handlers-Progress Buttons"

        Private Sub btnAfterProgressBar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAfterProgressBar.Click
            AfterProgressBar()
        End Sub

#End Region

#Region "Handlers-Grid"
        Private Sub moDataGrid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles moDataGrid.PageIndexChanged
            Try
                ' moDataGrid.PageIndex = e.NewPageIndex
                TheState.mnPageIndex = moDataGrid.PageIndex
                ClearSelectedDealerFile(ThePage.POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
            End Try

        End Sub
        Private Sub moDataGrid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles moDataGrid.PageIndexChanging
            Try
                moDataGrid.PageIndex = e.NewPageIndex
                TheState.mnPageIndex = moDataGrid.PageIndex
            Catch ex As Exception
                ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
            End Try
        End Sub

        Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moDataGrid.RowCreated
            ThePage.BaseItemCreated(sender, e)
        End Sub

        Private Sub moDataGrid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles moDataGrid.RowCommand
            Try
                Dim index As Integer = 0
                Dim ds As DataSet

                If rdParentFile.Checked Then
                    QUERY_STRING_PARENT_FILE = QUERY_STRING_PARENT_FILE & Codes.YESNO_Y
                    QUERY_STRING_PARENT_FILE1 = QUERY_STRING_PARENT_FILE1 & Codes.YESNO_Y
                Else
                    QUERY_STRING_PARENT_FILE = QUERY_STRING_PARENT_FILE & Codes.YESNO_N
                    QUERY_STRING_PARENT_FILE1 = QUERY_STRING_PARENT_FILE1 & Codes.YESNO_N
                End If

                '  If (Not e.CommandArgument.ToString().Equals(String.Empty)) Then
                If e.CommandName = ThePage.EDIT_COMMAND_NAME Then
                    index = CInt(e.CommandArgument)
                    TheState.SelectedDealerFileProcessedId = New Guid(Me.moDataGrid.Rows(index).Cells(Me.GRID_COL_DEALERFILE_PROCESSED_ID_IDX).Text)
                    TheState.mnPageIndex = moDataGrid.PageIndex
                    GetTotalRecords(SHOW_COMMAND_REJECTED)
                    If Not (DealerMultipleDrop.SelectedGuid = Guid.Empty) Then
                        Dim objDealer As New Dealer(DealerMultipleDrop.SelectedGuid)

                        If (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objDealer.ReconRejRecTypeId) = Codes.YESNO_Y AndAlso
                           TheState.moInterfaceTypeCode <> DealerFileProcessedData.InterfaceTypeCode.PAYM) AndAlso
                           Me.TheState.DealerType <> DealerType_VSC Then
                            ThePage.callPage(TheState.msUrlDetailPage & QUERY_STRING_REMAINING_REJECTED & QUERY_STRING_PARENT_FILE, TheState.SelectedDealerFileProcessedId)

                        ElseIf (Me.moState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PYMT) Then
                            ThePage.callPage(Url_Payment_Recon_Wrk & QUERY_STRING_REJECTED & QUERY_STRING_PARENT_FILE, TheState.SelectedDealerFileProcessedId)

                        Else
                            ThePage.callPage(TheState.msUrlDetailPage & QUERY_STRING_PARENT_FILE1, TheState.SelectedDealerFileProcessedId)
                        End If
                    Else
                        ThePage.callPage(TheState.msUrlDetailPage & QUERY_STRING_PARENT_FILE1, TheState.SelectedDealerFileProcessedId)
                    End If
                ElseIf e.CommandName = ThePage.SELECT_COMMAND_NAME Then
                    index = CInt(e.CommandArgument)
                    moDataGrid.SelectedIndex = index
                    TheState.SelectedDealerFileProcessedId = ThePage.GetGuidFromString(
                                ThePage.GetSelectedGridText(moDataGrid, GRID_COL_DEALERFILE_PROCESSED_ID_IDX))
                    TheState.SelectedFileName = Me.moDataGrid.Rows(index).Cells(Me.GRID_COL_FILENAME_IDX).Text.ToString()
                    EnableDisableButtons()
                ElseIf e.CommandName = SHOW_COMMAND_REJECTED Then
                    index = CInt(e.CommandArgument)
                    TheState.SelectedDealerFileProcessedId = New Guid(Me.moDataGrid.Rows(index).Cells(Me.GRID_COL_DEALERFILE_PROCESSED_ID_IDX).Text)
                    TheState.mnPageIndex = moDataGrid.PageIndex

                    If Not (DealerMultipleDrop.SelectedGuid = Guid.Empty) Then
                        Dim objDealer As New Dealer(DealerMultipleDrop.SelectedGuid)
                        GetTotalRecords(SHOW_COMMAND_REJECTED)

                        If (Me.moState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PYMT) Then
                            ThePage.callPage(Url_Payment_Recon_Wrk & QUERY_STRING_REJECTED & QUERY_STRING_PARENT_FILE, TheState.SelectedDealerFileProcessedId)


                        ElseIf (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objDealer.PaymentRejectedRecordReconcileId) = Codes.YESNO_Y AndAlso
                           TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PAYM) Then
                            ThePage.callPage(TheState.msUrlDetailPage & QUERY_STRING_REJECTED & QUERY_STRING_PARENT_FILE, TheState.SelectedDealerFileProcessedId)
                        Else
                            ThePage.callPage(TheState.msUrlDetailPage & QUERY_STRING_REJECTED & QUERY_STRING_PARENT_FILE, TheState.SelectedDealerFileProcessedId)
                        End If
                    Else
                        ThePage.callPage(TheState.msUrlDetailPage & QUERY_STRING_REJECTED & QUERY_STRING_PARENT_FILE, TheState.SelectedDealerFileProcessedId)
                    End If
                ElseIf e.CommandName = SHOW_COMMAND_REMAINING_REJECTED Then
                    index = CInt(e.CommandArgument)
                    TheState.SelectedDealerFileProcessedId = New Guid(Me.moDataGrid.Rows(index).Cells(Me.GRID_COL_DEALERFILE_PROCESSED_ID_IDX).Text)
                    TheState.mnPageIndex = moDataGrid.PageIndex
                    GetTotalRecords(SHOW_COMMAND_REMAINING_REJECTED)
                    ThePage.callPage(TheState.msUrlDetailPage & QUERY_STRING_REMAINING_REJECTED & QUERY_STRING_PARENT_FILE, TheState.SelectedDealerFileProcessedId)
                ElseIf e.CommandName = SHOW_COMMAND_VALIDATED Then
                    index = CInt(e.CommandArgument)
                    TheState.SelectedDealerFileProcessedId = New Guid(Me.moDataGrid.Rows(index).Cells(Me.GRID_COL_DEALERFILE_PROCESSED_ID_IDX).Text)
                    TheState.mnPageIndex = moDataGrid.PageIndex
                    If Not (DealerMultipleDrop.SelectedGuid = Guid.Empty) Then
                        Dim objDealer As New Dealer(DealerMultipleDrop.SelectedGuid)
                        GetTotalRecords(SHOW_COMMAND_VALIDATED)
                        If (Me.moState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PYMT) Then
                            ThePage.callPage(Url_Payment_Recon_Wrk & QUERY_STRING_VALIDATED & QUERY_STRING_PARENT_FILE & QUERY_STRING_REJREC_NOTUPDATABLE, TheState.SelectedDealerFileProcessedId)


                        ElseIf (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objDealer.PaymentRejectedRecordReconcileId) = Codes.YESNO_Y AndAlso
                           TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PAYM) Then
                            ThePage.callPage(TheState.msUrlDetailPage & QUERY_STRING_VALIDATED & QUERY_STRING_PARENT_FILE & QUERY_STRING_REJREC_NOTUPDATABLE, TheState.SelectedDealerFileProcessedId)
                        Else
                            ThePage.callPage(TheState.msUrlDetailPage & QUERY_STRING_VALIDATED & QUERY_STRING_PARENT_FILE, TheState.SelectedDealerFileProcessedId)
                        End If
                    Else
                        ThePage.callPage(TheState.msUrlDetailPage & QUERY_STRING_VALIDATED & QUERY_STRING_PARENT_FILE, TheState.SelectedDealerFileProcessedId)
                    End If
                ElseIf e.CommandName = SHOW_COMMAND_LOADED Then
                    index = CInt(e.CommandArgument)
                    TheState.SelectedDealerFileProcessedId = New Guid(Me.moDataGrid.Rows(index).Cells(Me.GRID_COL_DEALERFILE_PROCESSED_ID_IDX).Text)
                    TheState.mnPageIndex = moDataGrid.PageIndex
                    If Not (DealerMultipleDrop.SelectedGuid = Guid.Empty) Then
                        Dim objDealer As New Dealer(DealerMultipleDrop.SelectedGuid)
                        GetTotalRecords(SHOW_COMMAND_LOADED)
                        If (Me.moState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PYMT) Then
                            ThePage.callPage(Url_Payment_Recon_Wrk & QUERY_STRING_LOADED & QUERY_STRING_PARENT_FILE & QUERY_STRING_REJREC_NOTUPDATABLE, TheState.SelectedDealerFileProcessedId)


                        ElseIf (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objDealer.PaymentRejectedRecordReconcileId) = Codes.YESNO_Y AndAlso
                           TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PAYM) Then
                            ThePage.callPage(TheState.msUrlDetailPage & QUERY_STRING_LOADED & QUERY_STRING_PARENT_FILE & QUERY_STRING_REJREC_NOTUPDATABLE, TheState.SelectedDealerFileProcessedId)
                        Else
                            ThePage.callPage(TheState.msUrlDetailPage & QUERY_STRING_LOADED & QUERY_STRING_PARENT_FILE & QUERY_STRING_REJREC_NOTUPDATABLE, TheState.SelectedDealerFileProcessedId)
                        End If
                    Else
                        ThePage.callPage(TheState.msUrlDetailPage & QUERY_STRING_LOADED & QUERY_STRING_PARENT_FILE & QUERY_STRING_REJREC_NOTUPDATABLE, TheState.SelectedDealerFileProcessedId)
                    End If
                ElseIf e.CommandName = SHOW_COMMAND_BYPASSED Then
                    index = CInt(e.CommandArgument)
                    TheState.SelectedDealerFileProcessedId = New Guid(Me.moDataGrid.Rows(index).Cells(Me.GRID_COL_DEALERFILE_PROCESSED_ID_IDX).Text)
                    GetTotalRecords(SHOW_COMMAND_BYPASSED)
                    TheState.mnPageIndex = moDataGrid.PageIndex
                    ThePage.callPage(TheState.msUrlDetailPage & QUERY_STRING_BYPASSED & QUERY_STRING_PARENT_FILE, TheState.SelectedDealerFileProcessedId)
                ElseIf e.CommandName = SHOW_COMMAND_STATUS Then
                    Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                    Dim RowInd As Integer = row.RowIndex
                    index = CInt(e.CommandArgument)
                    TheState.SelectedDealerFileProcessedId = New Guid(Me.moDataGrid.Rows(index).Cells(Me.GRID_COL_DEALERFILE_PROCESSED_ID_IDX).Text)
                    TheState.mnPageIndex = moDataGrid.PageIndex
                    txtExtendedContent.Text = HttpUtility.HtmlEncode(Me.moDataGrid.Rows(index).Cells(Me.GRID_COL_STATUS_DESC_IDX).Text)
                    mdlPopup.Show()
                End If


            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
            End Try
        End Sub
        Public Sub GetTotalRecords(ByVal param As String)
            Dim records As Double

            Dim oDealerFile As DealerFileProcessed
            oDealerFile = New DealerFileProcessed(TheState.SelectedDealerFileProcessedId)

            Select Case (param)
                Case SHOW_COMMAND_BYPASSED
                    records = CType(oDealerFile.Bypassed, Double)
                    Session("TotalRecords") = records
                Case SHOW_COMMAND_LOADED
                    records = CType(oDealerFile.Loaded, Double)
                    Session("TotalRecords") = records
                Case SHOW_COMMAND_VALIDATED
                    records = CType(oDealerFile.Validated, Double)
                    Session("TotalRecords") = records
                Case SHOW_COMMAND_REMAINING_REJECTED
                    records = CType(oDealerFile.RemainingRejected, Double)
                    Session("TotalRecords") = records
                Case SHOW_COMMAND_REJECTED
                    records = CType(oDealerFile.Rejected, Double)
                    Session("TotalRecords") = records
            End Select

        End Sub
        Private Sub moDataGrid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles moDataGrid.RowDataBound

            Dim itemType As ListItemType = CType(e.Row.RowType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

            '  If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
            If e.Row.RowType = DataControlRowType.DataRow OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                With e.Row
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_DEALERFILE_PROCESSED_ID_IDX), dvRow(DealerFileProcessed.COL_NAME_DEALERFILE_PROCESSED_ID))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_FILENAME_IDX), dvRow(DealerFileProcessed.COL_NAME_FILENAME))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_RECEIVED_IDX), Convert.ToInt32(dvRow(DealerFileProcessed.COL_NAME_RECEIVED)))
                    ThePage.PopulateControlFromBOProperty(.Cells(GRID_COL_COUNTED_IDX), Convert.ToInt32(dvRow(DealerFileProcessed.COL_NAME_COUNTED)))

                    Dim oLinkButton As LinkButton
                    oLinkButton = CType(.FindControl(GRID_LINK_BTN_BYPASSED), LinkButton)
                    ThePage.PopulateControlFromBOProperty(oLinkButton, Convert.ToInt32(dvRow(DealerFileProcessed.COL_NAME_BYPASSED)))
                    If ((oLinkButton.Text.Trim = "0") OrElse (Me.TheState.DealerType = DealerType_VSC)) Then
                        oLinkButton.Enabled = False
                    End If

                    oLinkButton = CType(.FindControl(GRID_LINK_BTN_REJECTED), LinkButton)
                    ThePage.PopulateControlFromBOProperty(oLinkButton, Convert.ToInt32(dvRow(DealerFileProcessed.COL_NAME_REJECTED)))
                    If ((oLinkButton.Text.Trim = "0") OrElse (Me.TheState.DealerType = DealerType_VSC)) Then
                        oLinkButton.Enabled = False
                    End If

                    oLinkButton = CType(.FindControl(GRID_LINK_BTN_REMAINING_REJECTED), LinkButton)
                    ThePage.PopulateControlFromBOProperty(oLinkButton, Convert.ToInt32(dvRow(DealerFileProcessed.COL_NAME_REMAINING_REJECTED)))
                    If ((oLinkButton.Text.Trim = "0") OrElse (Me.TheState.DealerType = DealerType_VSC)) Then
                        oLinkButton.Enabled = False
                    End If

                    oLinkButton = CType(.FindControl(GRID_LINK_BTN_VALIDATED), LinkButton)
                    ThePage.PopulateControlFromBOProperty(oLinkButton, Convert.ToInt32(dvRow(DealerFileProcessed.COL_NAME_VALIDATED)))
                    If ((oLinkButton.Text.Trim = "0") OrElse (Me.TheState.DealerType = DealerType_VSC)) Then
                        oLinkButton.Enabled = False
                    End If

                    oLinkButton = CType(.FindControl(GRID_LINK_BTN_LOADED), LinkButton)
                    ThePage.PopulateControlFromBOProperty(oLinkButton, Convert.ToInt32(dvRow(DealerFileProcessed.COL_NAME_LOADED)))
                    If ((oLinkButton.Text.Trim = "0") OrElse (Me.TheState.DealerType = DealerType_VSC)) Then
                        oLinkButton.Enabled = False
                    End If


                    ThePage.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_LAYOUT_IDX), dvRow(DealerFileProcessed.COL_NAME_LAYOUT))

                    oLinkButton = CType(.FindControl(GRID_LINK_BTN_STATUS), LinkButton)
                    ThePage.PopulateControlFromBOProperty(oLinkButton, dvRow(DealerFileProcessed.COL_NAME_STATUS))
                    If (oLinkButton.Text.Trim = "") Then
                        oLinkButton.Enabled = False
                    End If
                    ThePage.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_STATUS_DESC_IDX), dvRow(DealerFileProcessed.COL_NAME_STATUS_DESC))
                End With
            End If
        End Sub

#End Region
#End Region

#Region "Progress Bar"

        Public Sub InstallInterfaceProgressBar()
            ThePage.InstallDisplayProgressBar()
        End Sub

        Private Sub ExecuteAndWait(ByVal oSP As Integer, Optional ByVal filename As String = "")
            Dim intStatus As InterfaceStatusWrk
            Dim params As InterfaceBaseForm.Params

            Try
                If oSP = SP_DOWNLOAD Then
                    ExecuteDownloadSp(filename)
                Else
                    ExecuteSp(oSP)
                End If
                params = SetParameters(Me.TheState.intStatusId, Me.ClientID + "_")
                Session(InterfaceBaseForm.SESSION_PARAMETERS_KEY) = params
                If ((oSP = SP_VALIDATE) Or (oSP = SP_PROCESS) Or (oSP = SP_GENRATE_RESPONSE)) Then
                    'Disable TheInterfaceProgress if supported
                Else
                    TheInterfaceProgress.EnableInterfaceProgress(Me.ClientID + "_")
                End If

            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)

            End Try
        End Sub

        Function SetParameters(ByVal intStatusId As Guid, ByVal baseController As String) As InterfaceBaseForm.Params
            Dim params As New InterfaceBaseForm.Params

            With params
                .intStatusId = intStatusId
                .baseController = baseController
            End With
            Return params
        End Function

        Private Sub AfterProgressBar()
            ClearSelectedDealerFile(ThePage.POPULATE_ACTION_SAVE)
            ThePage.DisplayMessage(Message.MSG_INTERFACES_HAS_COMPLETED, "", ThePage.MSG_BTN_OK, ThePage.MSG_TYPE_INFO)
        End Sub


#End Region

#Region "Error-Management"

        Private Sub ShowError(ByVal msg As String)
            MessageCtrl.AddError(msg)
            MessageCtrl.Show()
            AppConfig.Log(New Exception(msg))
        End Sub

#End Region

#Region "Button-Management"

        Private Sub ExecuteSp(ByVal oSP As Integer)
            Dim oDealerFileProcessedData As New DealerFileProcessedData
            Dim oInterfaceStatusWrk As New InterfaceStatusWrk

            If Not TheState.SelectedDealerFileProcessedId.Equals(Guid.Empty) Then
                Dim oDealerFileProcessed As New DealerFileProcessed(TheState.SelectedDealerFileProcessedId)
                If oInterfaceStatusWrk.IsfileBeingProcessed(oDealerFileProcessed.Filename, TheState.IsParentFile) Then
                    With oDealerFileProcessedData
                        .dealerfile_processed_id = TheState.SelectedDealerFileProcessedId
                        .DealerType = Me.TheState.DealerType
                        .filename = oDealerFileProcessed.Filename
                        .layout = oDealerFileProcessed.Layout
                        .fileTypeCode = TheState.moInterfaceTypeCode
                        If TheState.IsParentFile = True Then
                            .parentFile = Codes.YESNO_Y
                        Else
                            .parentFile = Codes.YESNO_N
                        End If
                        .oSP = oSP
                    End With
                    Select Case oSP
                        Case SP_VALIDATE
                            DealerFileProcessed.ValidateFile(oDealerFileProcessedData)
                        Case SP_PROCESS
                            DealerFileProcessed.ProcessFileRecords(oDealerFileProcessedData)
                        Case SP_DELETE
                            DealerFileProcessed.DeleteFile(oDealerFileProcessedData)
                        Case SP_GENRATE_RESPONSE
                            DealerFileProcessed.GenerateResponseFile(oDealerFileProcessedData)
                    End Select
                Else
                    Throw New GUIException("File is Been Process", Assurant.ElitaPlus.Common.ErrorCodes.ERR_INTERFACE_FILE_IN_PROCESS)
                End If
            Else
                Throw New GUIException("You must select a dealer file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If
            TheState.intStatusId = oDealerFileProcessedData.interfaceStatus_id

        End Sub

        Private Sub ExecuteDownloadSp(ByVal filename As String)
            Dim oDealerFileProcessedData As New DealerFileProcessedData

            With oDealerFileProcessedData
                .filename = filename
                .fileTypeCode = TheState.moInterfaceTypeCode
            End With
            DealerFileProcessed.DownloadFile(oDealerFileProcessedData)
            TheState.intStatusId = oDealerFileProcessedData.interfaceStatus_id
        End Sub

        Private Sub uploadDealerFile()
            Dim dealerFileName As String
            Dim layoutFileName As String
            Dim fileLen As Integer = dealerFileInput.PostedFile.ContentLength
            DealerFileProcessed.ValidateFileName(fileLen)
            'dealerFileName = MiscUtil.ReplaceSpaceByUnderscore(dealerFileInput.PostedFile.FileName)
            dealerFileName = dealerFileInput.PostedFile.FileName
            Dim fileBytes(fileLen - 1) As Byte
            Dim objStream As System.IO.Stream
            objStream = dealerFileInput.PostedFile.InputStream
            objStream.Read(fileBytes, 0, fileLen)

            Dim webServerPath As String = GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, ElitaPlusPrincipal.Current.Identity.Name)
            Dim webServerFile As String = webServerPath & "\" & System.IO.Path.GetFileName(dealerFileName)
            layoutFileName = webServerPath & "\" &
                System.IO.Path.GetFileNameWithoutExtension(webServerFile) & AppConfig.UnixServer.FtpTriggerExtension
            CreateFolder(webServerPath)
            If Not TheState.SelectedDealerFileLayout Is Nothing Then
                File.WriteAllBytes(webServerFile, fileBytes)
                File.WriteAllBytes(layoutFileName, System.Text.Encoding.ASCII.GetBytes(TheState.SelectedDealerFileLayout))
            Else
                Throw New GUIException("Missing File Layout Code", Assurant.ElitaPlus.Common.ErrorCodes.GUI_MISSING_FILE_LAYOUT_CODE)
            End If

            ' Taking the FTP Location value from elp_web_passwd for both FTP non PCI or PCI zone
            ' No more populating it from elp_servers table

            Dim unixHostName As String
            Dim unixPath As String
            Dim unixUserName As String
            Dim unixPassword As String
            Dim unixPort As Integer = 22

            Dim serviceTypeCode As String = Codes.SERVICE_TYPE__FTP_LOCATION_NON_PCI_SECURE_ZONE

            Select Case TheState.moInterfaceTypeCode
                Case DealerFileProcessedData.InterfaceTypeCode.CERT
                    If Not TheState.SelectedDealerId = Guid.Empty Then
                        Dim oDealer As New Dealer(TheState.SelectedDealerId)
                        Dim attvalue As AttributeValue = oDealer.AttributeValues.Where(Function(i) i.Attribute.UiProgCode = Codes.DLR_ATTR_ENROLLMENT_FILE_PCI_PARAMETER).FirstOrDefault

                        If Not attvalue Is Nothing Then
                            serviceTypeCode = Codes.SERVICE_TYPE__FTP_LOCATION_PCI_SECURE_ZONE
                            If Not TheState.SelectedDealerFileLayout Is Nothing AndAlso Not String.IsNullOrWhiteSpace(attvalue.Value) Then
                                Dim trcFileContent As String
                                trcFileContent = TheState.SelectedDealerFileLayout & " " & attvalue.Value
                                File.WriteAllBytes(layoutFileName, System.Text.Encoding.UTF8.GetBytes(trcFileContent))
                            End If
                        End If
                    End If
            End Select

            Dim oWebPasswd As WebPasswd
            Try
                oWebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, serviceTypeCode), False)
            Catch ex As Exception
                Throw New GUIException("Missing FTP File Location record in web passwd", Assurant.ElitaPlus.Common.ErrorCodes.GUI_FTP_FILE_LOCATION_WEB_PASSWORD_MISSING)
            End Try

            If Not oWebPasswd Is Nothing Then
                Dim ftpUri As New Uri(oWebPasswd.Url)
                If ftpUri.Scheme = Uri.UriSchemeFtp Then
                    unixHostName = ftpUri.Host
                    unixPath = ftpUri.AbsolutePath
                    unixUserName = oWebPasswd.UserId
                    unixPassword = oWebPasswd.Password
                    unixPort = ftpUri.Port
                Else
                    Throw New GUIException("Invalid FTP File Location record in web passwd", Assurant.ElitaPlus.Common.ErrorCodes.GUI_FTP_FILE_LOCATION_WEB_PASSWORD_INVALID)
                End If
            Else
                Throw New GUIException("Missing FTP File Location record in web passwd", Assurant.ElitaPlus.Common.ErrorCodes.GUI_FTP_FILE_LOCATION_WEB_PASSWORD_MISSING)
            End If

            '' ''Dim objUnixFTP As New aFtp(AppConfig.UnixServer.HostName, unixPath, AppConfig.UnixServer.UserId, _
            '' ''                     AppConfig.UnixServer.Password, PORT)

            Try
                Dim objUnixFTP As New sFtp(unixHostName, unixPath, unixUserName, unixPassword, unixPort)

                objUnixFTP.UploadFile(webServerFile)
                objUnixFTP.UploadFile(layoutFileName)

            Catch ex As Exception
                Throw New GUIException("FTP File transfer failed", Assurant.ElitaPlus.Common.ErrorCodes.GUI_FTP_FILE_PROCESS_FAILED)
            End Try

        End Sub

        Private Sub DisableButtons()
            'Dim InterfaceTypeCode As DealerFileProcessedData.InterfaceTypeCode = Me.State.moInterfaceTypeCode
            ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
            ControlMgr.SetEnableControl(ThePage, BtnLoadCertificate_WRITE, False)
            ControlMgr.SetEnableControl(ThePage, BtnDeleteDealerFile_WRITE, False)
            ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
            ControlMgr.SetEnableControl(ThePage, BtnErrorExport, False)

            Select Case TheState.moInterfaceTypeCode
                Case DealerFileProcessedData.InterfaceTypeCode.CERT
                    ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                    ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, False)
                Case DealerFileProcessedData.InterfaceTypeCode.PAYM
                    ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                    ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, False)
                Case DealerFileProcessedData.InterfaceTypeCode.TLMK
                    ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                    ControlMgr.SetVisibleControl(ThePage, BtnProcessedExport, False)
                    ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, False)
                Case DealerFileProcessedData.InterfaceTypeCode.RINS
                    ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                    ControlMgr.SetVisibleControl(ThePage, BtnProcessedExport, False)
                    ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, False)
                'ControlMgr.SetVisibleControl(ThePage, BtnErrorExport, False)
                'ControlMgr.SetVisibleControl(ThePage, BtnRejectReport, False)

                Case Else
                    Return
            End Select

        End Sub

        Private Sub EnableDisableButtons()
            If Not TheState.SelectedDealerFileProcessedId.Equals(Guid.Empty) Then
                Dim oDealerFile As DealerFileProcessed
                If rdParentFile.Checked = False Then
                    oDealerFile = New DealerFileProcessed(TheState.SelectedDealerFileProcessedId)
                Else
                    oDealerFile = New DealerFileProcessed(TheState.SelectedDealerFileProcessedId, True)
                End If
                DisableButtons()
                With oDealerFile
                    If .Received.Value = .Counted.Value Then
                        If .IsChildFile Then
                            ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
                        Else
                            ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, True)
                        End If
                    Else 'REQ 859
                        ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
                    End If
                    'REQ 859 Even if Received = Validated, the Validate Option should remain Enabled
                    'It will only get Disabled when Counted/Received = Loaded
                    'If .Received.Value = .Validated.Value Then ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
                    If .Loaded.Value = .Counted.Value Then
                        ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
                    End If

                    If .Rejected.Value > 0 Then
                        'If rdParentFile.Checked = False Then
                        If .IsChildFile Then
                            ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
                            ControlMgr.SetEnableControl(ThePage, BtnErrorExport, False)
                        ElseIf Me.TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PYMT Or
                               Me.TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.INVC Then
                            ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, True)
                            ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
                            ControlMgr.SetEnableControl(ThePage, BtnErrorExport, False)
                        Else
                            ControlMgr.SetEnableControl(ThePage, BtnRejectReport, True)
                            ControlMgr.SetEnableControl(ThePage, BtnErrorExport, True)
                        End If

                        'End If
                    ElseIf .Rejected.Value > 0 AndAlso Me.TheState.moInterfaceTypeCode <> DealerFileProcessedData.InterfaceTypeCode.RINS Then
                        ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
                        ControlMgr.SetEnableControl(ThePage, BtnErrorExport, False)
                    ElseIf .Rejected.Value <= 0 Then
                        ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
                        ControlMgr.SetEnableControl(ThePage, BtnErrorExport, False)
                    End If
                    If rdParentFile.Checked = True Then
                        If .Rejected.Value > 0 Then
                            If .IsChildFile Then
                                ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
                            Else

                                ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, True)
                            End If
                        Else
                            ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
                        End If
                    End If
                    If .Validated.Value > 0 Then
                        If .IsChildFile Then
                            ControlMgr.SetEnableControl(ThePage, BtnLoadCertificate_WRITE, False)
                        Else
                            ControlMgr.SetEnableControl(ThePage, BtnLoadCertificate_WRITE, True)
                        End If

                    End If
                    If .Received.Value = .Loaded.Value Then
                        ControlMgr.SetEnableControl(ThePage, BtnLoadCertificate_WRITE, False)
                        ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
                    End If

                    If (.Loaded.Value = 0) Then
                        If .IsChildFile Then
                            ControlMgr.SetEnableControl(ThePage, BtnDeleteDealerFile_WRITE, False)
                        Else
                            ControlMgr.SetEnableControl(ThePage, BtnDeleteDealerFile_WRITE, True)
                        End If
                    End If

                    'If rdParentFile.Checked = False Then
                    If Me.TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.CERT AndAlso .Loaded.Value > 0 Then
                        If .IsChildFile Then
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                        Else
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, True)
                        End If

                        If TheState.SelectedDealerId <> Guid.Empty Then

                            Dim Dealer As New Dealer(TheState.SelectedDealerId)

                            If Dealer.AttributeValues.Contains(Codes.DLR_ATTRBT__GEN_RESPONSE_FILE) Then
                                If Dealer.AttributeValues.Value(Codes.DLR_ATTRBT__GEN_RESPONSE_FILE) = Codes.YESNO_Y Then
                                    ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, True)
                                End If
                            End If
                        End If
                    End If

                    If Me.TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PAYM AndAlso .Loaded.Value > 0 Then
                        If .IsChildFile Then
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                        Else
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, True)
                        End If
                    End If
                    'End If

                    'ControlMgr.SetEnableControl(ThePage, BtnDeleteDealerFile_WRITE, True)
                    If TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.TLMK Then
                        ControlMgr.SetVisibleControl(ThePage, BtnErrorExport, False)
                        ControlMgr.SetVisibleControl(ThePage, BtnRejectReport, False)
                    End If
                    If TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.RINS Then
                        ControlMgr.SetVisibleControl(ThePage, BtnErrorExport, False)
                        ControlMgr.SetVisibleControl(ThePage, BtnRejectReport, False)
                        ControlMgr.SetVisibleControl(ThePage, BtnProcessedExport, False)

                    End If


                End With


                'Disable Validate, Delete and Process button based on the File Status --> PENDING/RUNNING
                If (ThePage.GetSelectedGridText(moDataGrid, GRID_COL_STATUS_IDX).Trim.ToUpper = FILE_STATUS_PENDING) _
                        Or (ThePage.GetSelectedGridText(moDataGrid, GRID_COL_STATUS_IDX).Trim.ToUpper = FILE_STATUS_RUNNING) Then
                    ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
                    ControlMgr.SetEnableControl(ThePage, BtnLoadCertificate_WRITE, False)
                    ControlMgr.SetEnableControl(ThePage, BtnDeleteDealerFile_WRITE, False)
                    ControlMgr.SetEnableControl(ThePage, BtnGenerateResponse, False)
                End If
            Else
                Throw New GUIException("You must select a dealer file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)

            End If
        End Sub

        Public Sub EnableDisableEditControl()
            Dim i As Integer
            Dim edt As ImageButton
            Dim oLinkButtonRejected As LinkButton
            Dim oLinkButtonRemainingRejected As LinkButton
            Dim oLinkButtonBypassed As LinkButton

            '  Enable or Disable all the EDIT  buttons on the DataGrid
            For i = 0 To (Me.moDataGrid.Rows.Count - 1)
                edt = CType(Me.moDataGrid.Rows(i).Cells(ThePage.EDIT_COL).FindControl(ThePage.EDIT_CONTROL_NAME), ImageButton)
                If Not edt Is Nothing Then
                    oLinkButtonRejected = CType(Me.moDataGrid.Rows(i).Cells(Me.GRID_COL_REJECTED_IDX).FindControl(GRID_LINK_BTN_REJECTED), LinkButton)
                    oLinkButtonRemainingRejected = CType(Me.moDataGrid.Rows(i).Cells(Me.GRID_COL_REMAINING_REJECTED_IDX).FindControl(GRID_LINK_BTN_REMAINING_REJECTED), LinkButton)
                    oLinkButtonBypassed = CType(Me.moDataGrid.Rows(i).Cells(Me.GRID_COL_BYPASSED_IDX).FindControl(GRID_LINK_BTN_BYPASSED), LinkButton)

                    edt.Enabled = ((oLinkButtonRejected.Text.Trim() <> "0" And oLinkButtonRejected.Text.Trim() <> "") _
                                   OrElse (oLinkButtonRemainingRejected.Text.Trim() <> "0" And oLinkButtonRemainingRejected.Text.Trim() <> "") _
                                    OrElse (oLinkButtonBypassed.Text.Trim() <> "0" And oLinkButtonBypassed.Text.Trim() <> ""))
                End If
            Next

        End Sub

        Private Sub ClearSelectedDealerFile(ByVal oAction As String)
            moDataGrid.SelectedIndex = ThePage.NO_ITEM_SELECTED_INDEX
            DisableButtons()
            TheState.SelectedDealerFileProcessedId = Guid.Empty
            Me.PopulateGrid(oAction)
        End Sub


#End Region

#Region "Clear"

        Private Sub ClearAll()
            moDataGrid.PageIndex = ThePage.NO_PAGE_INDEX
            Me.TheState.oDataView = Nothing
            moDataGrid.DataSource = Nothing
            moDataGrid.DataBind()
            TheState.SelectedDealerFileProcessedId = Guid.Empty
            TheState.SelectedDealerCode = ""
            TheState.SelectedDealerFileLayout = ""
            moExpectedFileLabel_NO_TRANSLATE.Text = String.Empty
            TheState.SelectedDealerGroupCode = ""
            TheState.SelectedDealerGroupId = Guid.Empty
            TheState.SelectedDealerId = Guid.Empty
            TheState.IsParentFile = False
            DisableButtons()
            ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, False, True)
            ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, False, True)
        End Sub
        Public Sub ClearDealerSelection()
            DealerMultipleDrop.NothingSelected = True
            DealerMultipleDrop.SelectedIndex = -1
        End Sub
        Public Sub ClearDealerGroupSelection()
            DealerGroupMultipleDrop.NothingSelected = True
            DealerGroupMultipleDrop.SelectedIndex = -1
        End Sub

#End Region

#Region "Gui-Validation"

        Public Sub ValidateSelection()
            If DealerMultipleDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Then
                DealerGroupMultipleDrop.NothingSelected = True
                DealerGroupMultipleDrop.SelectedIndex = -1
                Me.TheState.SelectedDealerGroupId = Guid.Empty
                Me.TheState.SelectedDealerGroupCode = String.Empty
            ElseIf DealerGroupMultipleDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Then
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SelectedIndex = -1
                Me.TheState.SelectedDealerId = Guid.Empty
                Me.TheState.SelectedDealerCode = String.Empty
            ElseIf rdParentFile.Checked Then
                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SelectedIndex = -1
                TheState.IsParentFile = True
                Me.TheState.SelectedDealerId = Guid.Empty
                Me.TheState.SelectedDealerCode = String.Empty
                DealerGroupMultipleDrop.NothingSelected = True
                DealerGroupMultipleDrop.SelectedIndex = -1
                Me.TheState.SelectedDealerGroupId = Guid.Empty
                Me.TheState.SelectedDealerGroupCode = String.Empty
            End If
        End Sub

#End Region

#Region "Populate"
        Sub PopulateDealerDropDown()
            Try
                Dim oCompanyIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
                Dim dv As DataView

                Dim Dealerobj As New Dealer
                Select Case TheState.moInterfaceTypeCode
                    Case DealerFileProcessedData.InterfaceTypeCode.CERT
                        dv = LookupListNew.GetDealerLookupList(oCompanyIds, False, "Code", "CODE")
                    Case DealerFileProcessedData.InterfaceTypeCode.PAYM
                        dv = LookupListNew.GetDealerMonthlyBillingLookupList(oCompanyIds, "CODE")
                    Case DealerFileProcessedData.InterfaceTypeCode.TLMK
                        dv = LookupListNew.GetDealerLookupList(oCompanyIds, False, "Code", "CODE")
                    Case DealerFileProcessedData.InterfaceTypeCode.RINS
                        dv = LookupListNew.GetDealerLookupList(oCompanyIds, False, "Code", "CODE")
                    Case DealerFileProcessedData.InterfaceTypeCode.INVC
                        dv = LookupListNew.GetDealerLookupListByAttribute(oCompanyIds, Codes.DEALER_TABLE, Codes.DLR_ATTRBT__NEW_INVOICE_PAYMENT)
                    Case DealerFileProcessedData.InterfaceTypeCode.PYMT
                        dv = LookupListNew.GetDealerLookupListByAttribute(oCompanyIds, Codes.DEALER_TABLE, Codes.DLR_ATTRBT__NEW_INVOICE_PAYMENT)


                    Case Else
                        Return
                End Select

                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(True, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True)
                DealerMultipleDrop.SelectedGuid = TheState.SelectedDealerId

                If Not dv Is Nothing AndAlso dv.Table.Rows.Count > 0 Then
                    Dim Dealer_id As Guid = New Guid(CType(dv.Table.Rows(0)("ID"), Byte()))
                    Dim objDealer As New Dealer(Dealer_id)


                    If Not objDealer Is Nothing AndAlso objDealer.DealerTypeDesc = DealerType_VSC Then
                        TheState.msUrlDetailPage = DealerVSCReconWrkForm.URL
                        Me.TheState.DealerType = objDealer.DealerTypeDesc
                    End If

                End If
                '  End If

            Catch ex As Exception
                ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
            End Try
        End Sub

        Sub PopulateDealerGroupDropDown()
            Try
                Dim dealerTypeVSC As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_DEALER_TYPE, VSCCode)
                Dim oCompanygrpId As Guid = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Id
                Dim dv As DataView
                Select Case TheState.moInterfaceTypeCode
                    Case DealerFileProcessedData.InterfaceTypeCode.CERT
                        dv = LookupListNew.GetDealerGroupLookupList(oCompanygrpId)
                    Case DealerFileProcessedData.InterfaceTypeCode.INVC
                        dv = LookupListNew.GetDealerGroupLookupList(oCompanygrpId)
                    Case Else
                        Return
                End Select

                If (GetDealerTypeID.Equals(dealerTypeVSC)) Then
                    ControlMgr.SetVisibleControl(ThePage, DealerGroupMultipleDrop, True)
                    ControlMgr.SetVisibleControl(ThePage, rdDealerFile, False)
                    ControlMgr.SetVisibleControl(ThePage, rdParentFile, False)
                    ControlMgr.SetVisibleControl(ThePage, lblParentFile, False)

                    DealerGroupMultipleDrop.NothingSelected = True
                    DealerGroupMultipleDrop.SetControl(True, DealerGroupMultipleDrop.MODES.NEW_MODE, True, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER_GROUP), True)

                    ' If Not Me.TheState.SelectedDealerGroupId = Guid.Empty Then
                    DealerGroupMultipleDrop.SelectedGuid = TheState.SelectedDealerGroupId

                    If Not dv Is Nothing AndAlso dv.Table.Rows.Count > 0 Then
                        Dim Dealer_group_id As Guid = New Guid(CType(dv.Table.Rows(0)("ID"), Byte()))
                        Dim objDealerGrp As New DealerGroup(Dealer_group_id)

                        TheState.msUrlDetailPage = DealerVSCReconWrkForm.URL
                        Me.TheState.DealerType = DealerType_VSC

                    End If
                    'End If

                ElseIf (TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.INVC) Then
                    ControlMgr.SetVisibleControl(ThePage, DealerGroupMultipleDrop, False)
                    ControlMgr.SetVisibleControl(ThePage, rdDealerFile, False)
                    ControlMgr.SetVisibleControl(ThePage, rdParentFile, False)
                    ControlMgr.SetVisibleControl(ThePage, lblParentFile, False)

                ElseIf (TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PYMT) Then
                    ControlMgr.SetVisibleControl(ThePage, DealerGroupMultipleDrop, False)
                    ControlMgr.SetVisibleControl(ThePage, rdDealerFile, False)
                    ControlMgr.SetVisibleControl(ThePage, rdParentFile, False)
                    ControlMgr.SetVisibleControl(ThePage, lblParentFile, False)
                Else
                    ControlMgr.SetVisibleControl(ThePage, DealerGroupMultipleDrop, False)
                    ControlMgr.SetVisibleControl(ThePage, rdDealerFile, True)
                    ControlMgr.SetVisibleControl(ThePage, rdParentFile, True)
                    ControlMgr.SetVisibleControl(ThePage, lblParentFile, True)
                End If

            Catch ex As Exception
                ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
            End Try
        End Sub

        Private Function GetDataView() As DataView
            Dim oDealerFileData As DealerFileProcessedData = New DealerFileProcessedData
            Dim oDataView As DataView

            With oDealerFileData
                .dealerCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, DealerMultipleDrop.SelectedGuid)
                .dealerId = DealerMultipleDrop.SelectedGuid
                .fileTypeCode = TheState.moInterfaceTypeCode
                .dealergrpCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_GROUPS, DealerGroupMultipleDrop.SelectedGuid)
                .dealergroupId = DealerGroupMultipleDrop.SelectedGuid
                If rdParentFile.Checked Then
                    .parentFile = Codes.YESNO_Y
                Else
                    .parentFile = Codes.YESNO_N
                End If

                'If TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PAYM Then
                '    'Dim dealer As Dealer = New Dealer(.dealerId)
                '    'dealer.AttributeValues.Co
                '    oDataView = LookupListNew.GetDealerByAttribute(oDealerFileData.dealerCode, TABLE_NAME)

                '    If (oDataView.Table(0)).Item(0).ToString() <> Count Then
                '        TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PYMT
                '    Else
                '        TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PAYM
                '    End If
                'End If

                oDataView = DealerFileProcessed.LoadList(ElitaPlusIdentity.Current.ActiveUser.Companies, oDealerFileData)


            End With

            Return oDataView
        End Function

        Private Sub SetExpectedFile()
            Dim sFileName As String = String.Empty

            If DealerMultipleDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Then
                TheState.SelectedDealerCode = DealerMultipleDrop.SelectedCode
                Dim oDealerId As Guid = DealerMultipleDrop.SelectedGuid
                TheState.SelectedDealerId = oDealerId
            ElseIf DealerGroupMultipleDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Then
                Dim oDealerGroupId As Guid = DealerGroupMultipleDrop.SelectedGuid
                TheState.SelectedDealerGroupCode = DealerGroupMultipleDrop.SelectedCode
                TheState.SelectedDealerGroupId = oDealerGroupId
            ElseIf rdParentFile.Checked Then
                TheState.IsParentFile = True
            End If
            Dim sDirectory As String
            If Not TheState.SelectedDealerId.Equals(Guid.Empty) Then
                sDirectory = AppConfig.FileClientDirectory
                Dim dateStr As String = System.DateTime.Now.ToString("yyMMdd")
                Dim tempDealerInfo As DealerFileProcessed.DealerInfo = DealerFileProcessed.GetDealerLayout(
                                        TheState.SelectedDealerId, TheState.moInterfaceTypeCode)

                TheState.SelectedDealerFileLayout = tempDealerInfo.layout
                TheState.SelectedDealerCode = tempDealerInfo.dealerCode
                Select Case TheState.moInterfaceTypeCode
                    Case DealerFileProcessedData.InterfaceTypeCode.CERT
                        sFileName = sDirectory & TheState.SelectedDealerCode.Trim() & dateStr & ".TXT"
                    Case DealerFileProcessedData.InterfaceTypeCode.PAYM
                        sFileName = sDirectory & TheState.SelectedDealerCode.Trim() & DEALER_PAYMENT_FILE_SUBSTRING _
                                    & dateStr & ".TXT"
                    Case DealerFileProcessedData.InterfaceTypeCode.TLMK
                        sFileName = sDirectory & TheState.SelectedDealerCode.Trim() & TELE_MRKT_FILE_SUBSTRING _
                                    & dateStr & ".TXT"
                    Case DealerFileProcessedData.InterfaceTypeCode.RINS
                        sFileName = sDirectory & TheState.SelectedDealerCode.Trim() & DEALER_REINSURANCE_FILE_SUBSTRING _
                                     & dateStr & ".TXT"
                        TheState.SelectedDealerFileLayout = "NA"
                    Case DealerFileProcessedData.InterfaceTypeCode.INVC
                        sFileName = sDirectory & TheState.SelectedDealerCode.Trim() & DEALER_INVOICE_FILE_SUBSTRING _
                                     & dateStr & ".TXT"
                        TheState.SelectedDealerFileLayout = "NA"
                    Case DealerFileProcessedData.InterfaceTypeCode.PYMT
                        sFileName = sDirectory & TheState.SelectedDealerCode.Trim() & DEALER_PAYMENT_FILE_SUBSTRING _
                                    & dateStr & ".TXT"
                        TheState.SelectedDealerFileLayout = "NA"
                End Select
                moExpectedFileLabel_NO_TRANSLATE.Text = sFileName


            ElseIf Not TheState.SelectedDealerGroupId.Equals(Guid.Empty) Then
                Dim dv As DataView
                dv = Dealer.getFirstDealerByDealerGrp(TheState.SelectedDealerGroupId)

                If dv.Count > 0 Then
                    sDirectory = AppConfig.FileClientDirectory
                    Dim dateStr As String = System.DateTime.Now.ToString("yyMMdd")
                    Select Case TheState.moInterfaceTypeCode
                        Case DealerFileProcessedData.InterfaceTypeCode.CERT
                            sFileName = sDirectory & TheState.SelectedDealerGroupCode.Trim() & dateStr & ".TXT"
                    End Select
                    moExpectedFileLabel_NO_TRANSLATE.Text = sFileName

                    Dim tempDealerInfo As DealerFileProcessed.DealerInfo = DealerFileProcessed.GetDealerLayout(
                                         GuidControl.ByteArrayToGuid(CType(dv(FIRST_POS)(COL_NAME), Byte())), TheState.moInterfaceTypeCode)
                    TheState.SelectedDealerFileLayout = tempDealerInfo.layout
                    TheState.SelectedDealerGroupCode = TheState.SelectedDealerGroupCode


                End If
            ElseIf TheState.IsParentFile = True Then
                sDirectory = AppConfig.FileClientDirectory
                Dim dateStr As String = System.DateTime.Now.ToString("yyMMdd")
                Select Case TheState.moInterfaceTypeCode
                    Case DealerFileProcessedData.InterfaceTypeCode.CERT
                        sFileName = sDirectory & "XXXX" & dateStr & ".TXT"
                    Case DealerFileProcessedData.InterfaceTypeCode.PAYM
                        sFileName = sDirectory & "XXXX" & DEALER_PAYMENT_FILE_SUBSTRING _
                                    & dateStr & ".TXT"
                End Select
                moExpectedFileLabel_NO_TRANSLATE.Text = sFileName
            End If

        End Sub

        Private Sub PopulateGrid(ByVal oAction As String)
            'Dim oDataView As DataView

            Try
                'DEF-2447 : START
                'Dealer Code is not available. Check if State has these details. In that case fill from state 
                If ((DealerMultipleDrop.SelectedGuid = Guid.Empty) AndAlso (Not Me.moState Is Nothing) _
                        AndAlso (Me.moState.SelectedDealerId <> Guid.Empty)) Then
                    'Set Selected Value
                    PopulateDealerDropDown()
                ElseIf ((DealerGroupMultipleDrop.SelectedGuid = Guid.Empty) _
                    AndAlso (Not Me.moState Is Nothing) AndAlso (Me.moState.SelectedDealerGroupId <> Guid.Empty)) Then
                    PopulateDealerGroupDropDown()
                End If

                'DEF_26687 Start
                If (TheState.moInterfaceTypeCode <> DealerFileProcessedData.InterfaceTypeCode.PAYM) AndAlso (TheState.moInterfaceTypeCode <> DealerFileProcessedData.InterfaceTypeCode.RINS) Then
                    If DealerMultipleDrop.SelectedGuid <> Guid.Empty Then
                        Dim EscVscDealer As New Dealer(DealerMultipleDrop.SelectedGuid)
                        If Not EscVscDealer Is Nothing AndAlso EscVscDealer.DealerTypeDesc = DealerType_VSC Then
                            TheState.msUrlDetailPage = DealerVSCReconWrkForm.URL
                            Me.TheState.DealerType = EscVscDealer.DealerTypeDesc
                        ElseIf Not EscVscDealer Is Nothing AndAlso EscVscDealer.DealerTypeDesc = DealerType_ESC Then
                            TheState.msUrlDetailPage = DealerReconWrkForm.URL
                            Me.TheState.DealerType = EscVscDealer.DealerTypeDesc
                        End If
                    End If
                End If

                'DEF_26687 End
                'Reset Selected item background and enable / disable controls
                ThePage.SetGridItemStyleColor(Me.moDataGrid)
                'EnableDisableButtons()
                ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                If rdParentFile.Checked Then
                    ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, False, True)
                Else
                    ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                End If

                'DEF-2447 : END
                SetExpectedFile()
                'If Me.TheState.oDataView Is Nothing Then
                Me.TheState.oDataView = GetDataView()
                'End If

                If Not (DealerMultipleDrop.SelectedGuid = Guid.Empty) Then
                    Dim objDealer As New Dealer(DealerMultipleDrop.SelectedGuid)

                    If ((LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objDealer.ReconRejRecTypeId) = Codes.YESNO_Y AndAlso
                       TheState.moInterfaceTypeCode <> DealerFileProcessedData.InterfaceTypeCode.PAYM) Or
                       (LookupListNew.GetCodeFromId(LookupListNew.LK_YESNO, objDealer.PaymentRejectedRecordReconcileId) = Codes.YESNO_Y AndAlso
                       TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PAYM)) AndAlso
                       Me.TheState.DealerType <> DealerType_VSC Then
                        Me.moDataGrid.Columns(GRID_COL_REMAINING_REJECTED_IDX).Visible = True
                    Else
                        Me.moDataGrid.Columns(GRID_COL_REMAINING_REJECTED_IDX).Visible = False
                    End If
                End If
                If rdParentFile.Checked Then
                    Me.moDataGrid.Columns(GRID_COL_REMAINING_REJECTED_IDX).Visible = False
                End If
                ThePage.BasePopulateGrid(moDataGrid, Me.TheState.oDataView, TheState.SelectedDealerFileProcessedId, oAction)
                ThePage.SetPageAndSelectedIndexFromGuid(Me.TheState.oDataView, TheState.SelectedDealerFileProcessedId, moDataGrid, TheState.mnPageIndex)
                EnableDisableEditControl()
            Catch ex As Exception
                ThePage.HandleErrors(ex, ThePage.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "radiobuttons handlers"

#End Region

        Private Sub rdParentFile_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rdParentFile.CheckedChanged
            rdDealerFile.Checked = False
            multipleDropControl.SelectedIndex = -1
            multipleDropControl.ChangeEnabledControlProperty(False)
            TheState.IsParentFile = True
            RaiseEvent CheckedChanged(Me)
        End Sub

        Private Sub rdDealerFile_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rdDealerFile.CheckedChanged
            rdParentFile.Checked = False
            TheState.IsParentFile = False
            multipleDropControl.ChangeEnabledControlProperty(True)
            ClearAll()
        End Sub
    End Class


End Namespace
