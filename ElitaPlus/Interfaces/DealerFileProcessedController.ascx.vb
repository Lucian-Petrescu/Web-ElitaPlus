Imports Assurant.ElitaPlus.DALObjects
Imports Assurant.Common.Ftp
Imports Assurant.ElitaPlus.Common.MiscUtil
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Reports
Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports System.IO

Namespace Interfaces

    Partial Class DealerFileProcessedController
        Inherits System.Web.UI.UserControl


#Region "Constants"

        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_SELECT_IDX As Integer = 1
        Public Const GRID_COL_DEALERFILE_PROCESSED_ID_IDX As Integer = 2
        Public Const GRID_COL_FILENAME_IDX As Integer = 3
        Public Const GRID_COL_RECEIVED_IDX As Integer = 4
        Public Const GRID_COL_COUNTED_IDX As Integer = 5
        Public Const GRID_COL_BYPASSED_IDX As Integer = 6
        Public Const GRID_COL_REJECTED_IDX As Integer = 7
        Public Const GRID_COL_VALIDATED_IDX As Integer = 8
        Public Const GRID_COL_LOADED_IDX As Integer = 9
        Public Const GRID_COL_LAYOUT_IDX As Integer = 10


        Private Const SP_VALIDATE As Integer = 0
        Private Const SP_PROCESS As Integer = 1
        Private Const SP_DELETE As Integer = 2
        Private Const SP_DOWNLOAD As Integer = 3

        Private Const DEALER_FILE_REQUIRED As String = "DEALER_FILE_REQUIRED"
        Private Const DEALER_PAYMENT_FILE_SUBSTRING As String = "P"
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

#End Region

#Region "Page State"

#Region "MyState"

        Public Class MyState
            Public SelectedDealerFileProcessedId As Guid = Guid.Empty
            Public SelectedDealerFileLayout As String = ""
            Public SelectedDealerCode As String '= ""
            Public SelectedDealerGroupCode As String '= ""
            Public IsGridVisible As Boolean = False
            Public SelectedDealerId As Guid '= Guid.Empty
            Public SelectedDealerGroupId As Guid '= Guid.Empty
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
                            moDataGrid.CurrentPageIndex = Me.TheState.mnPageIndex
                            If Not Me.TheState.SelectedDealerFileProcessedId.Equals(Guid.Empty) Then
                                Me.TheState.IsGridVisible = True
                                PopulateDealerDropDown()
                                PopulateDealerGroupDropDown()
                                ' PopulateDealerInterface()
                                Me.PopulateGrid(ThePage.POPULATE_ACTION_SAVE)
                                ThePage.SetGridItemStyleColor(Me.moDataGrid)
                                EnableDisableButtons()
                                ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                                ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                            End If
                        Catch ex As Exception
                            ThePage.HandleErrors(ex, ErrorCtrl)
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
        Private ErrorCtrl As ErrorController
        Private DropDownIndexChangedController As Boolean = False
        Protected WithEvents moInterfaceProgressControl As InterfaceProgressControl
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

        Public ReadOnly Property DealerMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moDealerMultipleDrop Is Nothing Then
                    moDealerMultipleDrop = CType(FindControl("multipleDropControl"), MultipleColumnDDLabelControl)
                End If
                Return moDealerMultipleDrop
            End Get
        End Property

        Public ReadOnly Property DealerGroupMultipleDrop() As MultipleColumnDDLabelControl
            Get
                If moDealerGroupMultipleDrop Is Nothing Then
                    moDealerGroupMultipleDrop = CType(FindControl("multipleDealerGrpDropControl"), MultipleColumnDDLabelControl)
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
        'Public Property IsDealerSelected() As Guid
        '    Get
        '        Return Me.MyState.SelectedDealerId
        '    End Get
        '    Set(ByVal Value As Guid)
        '        Me.MyState.SelectedDealerId = Value
        '    End Set
        'End Property
#End Region

#Region "Handlers"


#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents moDealerMultipleDrop As MultipleColumnDDLabelControl
        Protected WithEvents moDealerGroupMultipleDrop As MultipleColumnDDLabelControl
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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If IsReturningFromChild Then
                ValidateSelection()
                If (DealerMultipleDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Or DealerGroupMultipleDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED) Then
                    TheState.IsGridVisible = True
                    Me.PopulateGrid(ThePage.POPULATE_ACTION_SAVE)
                    ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                    Select Case TheState.moInterfaceTypeCode
                        Case DealerFileProcessedData.InterfaceTypeCode.CERT
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                        Case DealerFileProcessedData.InterfaceTypeCode.PAYM
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                        'ControlMgr.SetVisibleControl(ThePage, BtnProcessedExport, False)
                        Case DealerFileProcessedData.InterfaceTypeCode.TLMK
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnProcessedExport, False)

                        Case DealerFileProcessedData.InterfaceTypeCode.RINS
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnProcessedExport, False)
                        Case Else
                            Return
                    End Select
                    ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                End If
            End If
        End Sub

        ' This method should be called for every pageload
        Public Sub SetErrorController(ByVal oErrorCtrl As ErrorController)
            ErrorCtrl = oErrorCtrl
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
                If DealerMultipleDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Or DealerGroupMultipleDrop.SelectedIndex > ThePage.BLANK_ITEM_SELECTED Then
                    TheState.IsGridVisible = True
                    Me.PopulateGrid(ThePage.POPULATE_ACTION_NONE)
                    ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                    Select Case TheState.moInterfaceTypeCode
                        Case DealerFileProcessedData.InterfaceTypeCode.CERT
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                        Case DealerFileProcessedData.InterfaceTypeCode.PAYM
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                        'ControlMgr.SetVisibleControl(ThePage, BtnProcessedExport, False)
                        Case DealerFileProcessedData.InterfaceTypeCode.TLMK
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnErrorExport, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnRejectReport, False)
                        Case DealerFileProcessedData.InterfaceTypeCode.RINS
                            ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnProcessedExport, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnErrorExport, False)
                            ControlMgr.SetVisibleControl(ThePage, BtnRejectReport, False)
                        Case Else
                            Return
                    End Select
                    ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)
                End If
            Catch ex As Exception
                ThePage.HandleErrors(ex, Me.ErrorCtrl)
            Finally
                DropDownIndexChangedController = False
            End Try

        End Sub

        'Private Sub OnDealerFromDrop_Changed(ByVal fromMultipleDrop As MultipleColumnDDLabelControl) _
        '        Handles moDealerMultipleDrop.SelectedDropChanged
        '    Try
        '        PopulateDealerInterface()
        '    Catch ex As Exception
        '        ThePage.HandleErrors(ex, Me.ErrorCtrl)
        '    End Try
        'End Sub

        'Private Sub OnDealerGrpFromDrop_Changed(ByVal fromMultipleDrop As MultipleColumnDDLabelControl) _
        '      Handles moDealerGroupMultipleDrop.SelectedDropChanged
        '    Try
        '        PopulateDealerInterface()
        '    Catch ex As Exception
        '        ThePage.HandleErrors(ex, Me.ErrorCtrl)
        '    End Try
        'End Sub

        '''Private Sub moDealerDrop_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '''    DealerMultipleDrop.SelectedIndex = -1
        '''    DealerMultipleDrop.Items.FindByValue(moDealerDrop.SelectedValue).Selected = True
        '''    cboDealerCode_SelectedIndexChanged(Nothing, Nothing)
        '''End Sub

        '''Private Sub cboDealerCode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        '''    Me.moDealerDrop.SelectedIndex = -1
        '''    Me.moDealerDrop.Items.FindByValue(cboDealerCode.SelectedValue).Selected = True
        '''    PopulateDealerInterface()
        '''End Sub

#End Region

#Region "Handlers-Buttons"

        Private Sub btnCopyDealerFile_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopyDealerFile_WRITE.Click
            Dim filename As String
            Try
                filename = uploadDealerFile()
                '    ExecuteAndWait(SP_DOWNLOAD, filename)
                ThePage.DisplayMessage(Message.MSG_THE_FILE_TRANSFER_HAS_COMPLETED, "", ThePage.MSG_BTN_OK, ThePage.MSG_TYPE_INFO)
            Catch ex As Exception
                ThePage.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub




        Private Sub BtnValidate_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnValidate_WRITE.Click
            ExecuteAndWait(SP_VALIDATE)
        End Sub

        Private Sub BtnLoadCertificate_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnLoadCertificate_WRITE.Click
            ExecuteAndWait(SP_PROCESS)
        End Sub

        Private Sub BtnDeleteDealerFile_WRITE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDeleteDealerFile_WRITE.Click
            ExecuteAndWait(SP_DELETE)
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

        Private Sub RejectReport(ByVal reportType As Integer)
            Try
                If Not TheState.SelectedDealerFileProcessedId.Equals(Guid.Empty) Then
                    Dim param As New PrintDealerLoadRejectForm.MyState
                    param.DealerfileProcessedId = TheState.SelectedDealerFileProcessedId
                    param.moInterfaceTypeCode = TheState.moInterfaceTypeCode
                    param.reportType = reportType
                    param.dealertype = TheState.DealerType
                    If Not TheState.SelectedDealerId = Guid.Empty Then
                        param.Selectioncode = TheState.SelectedDealerCode
                    ElseIf Not TheState.SelectedDealerGroupId = Guid.Empty Then
                        param.Selectioncode = TheState.SelectedDealerGroupCode
                    End If

                    ThePage.callPage(TheState.msUrlPrintPage, param)
                Else
                    Throw New GUIException("You must select a dealer file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ThePage.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub


#End Region

#Region "Handlers-Progress Buttons"

        Private Sub btnAfterProgressBar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAfterProgressBar.Click
            AfterProgressBar()
        End Sub

#End Region

#Region "Handlers-Grid"

        Private Sub moDataGrid_PageIndexChanged(ByVal source As System.Object, _
                ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged
            Try
                moDataGrid.CurrentPageIndex = e.NewPageIndex
                TheState.mnPageIndex = moDataGrid.CurrentPageIndex
                ClearSelectedDealerFile(ThePage.POPULATE_ACTION_NO_EDIT)
            Catch ex As Exception
                ThePage.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

        Public Sub ItemCreated(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            ThePage.BaseItemCreated(sender, e)
        End Sub

        Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                If e.CommandName = ThePage.EDIT_COMMAND_NAME Then
                    TheState.SelectedDealerFileProcessedId = New Guid(e.Item.Cells(Me.GRID_COL_DEALERFILE_PROCESSED_ID_IDX).Text)
                    TheState.mnPageIndex = moDataGrid.CurrentPageIndex
                    ThePage.callPage(TheState.msUrlDetailPage, TheState.SelectedDealerFileProcessedId)
                ElseIf e.CommandName = ThePage.SELECT_COMMAND_NAME Then
                    moDataGrid.SelectedIndex = e.Item.ItemIndex
                    TheState.SelectedDealerFileProcessedId = ThePage.GetGuidFromString( _
                                ThePage.GetSelectedGridText(moDataGrid, GRID_COL_DEALERFILE_PROCESSED_ID_IDX))
                    EnableDisableButtons()
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ThePage.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub moDataGrid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moDataGrid.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                With e.Item
                    ThePage.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_DEALERFILE_PROCESSED_ID_IDX), dvRow(DealerFileProcessed.COL_NAME_DEALERFILE_PROCESSED_ID))
                    ThePage.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_FILENAME_IDX), dvRow(DealerFileProcessed.COL_NAME_FILENAME))
                    ThePage.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_RECEIVED_IDX), dvRow(DealerFileProcessed.COL_NAME_RECEIVED))
                    ThePage.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_COUNTED_IDX), dvRow(DealerFileProcessed.COL_NAME_COUNTED))
                    ThePage.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_BYPASSED_IDX), dvRow(DealerFileProcessed.COL_NAME_BYPASSED))
                    ThePage.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_REJECTED_IDX), dvRow(DealerFileProcessed.COL_NAME_REJECTED))
                    ThePage.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_VALIDATED_IDX), dvRow(DealerFileProcessed.COL_NAME_VALIDATED))
                    ThePage.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_LOADED_IDX), dvRow(DealerFileProcessed.COL_NAME_LOADED))
                    ThePage.PopulateControlFromBOProperty(.Cells(Me.GRID_COL_LAYOUT_IDX), dvRow(DealerFileProcessed.COL_NAME_LAYOUT))
                End With
            End If
        End Sub

#End Region
#End Region

#Region "Progress Bar"

        'Public Sub InstallProgressBar()
        '    'ThePage.DisplayProgressBarOnClick(BtnValidate_WRITE, "Loading_File")
        '    'ThePage.InstallDisplayProgressBar()
        'End Sub


        'Private Sub ExecuteAndWait(ByVal oSP As Integer)
        '    Dim intStatus As InterfaceStatusWrk

        '    Try
        '        ' TheInterfaceProgress.EnableInterfaceProgress(DEALERP_VARIABLE_NAME)
        '        ExecuteSp(oSP)
        '        intStatus = New InterfaceStatusWrk(TheState.intStatusId)
        '        TheState.errorStatus = intStatus.WaitTilDone()

        '    Catch ex As Threading.ThreadAbortException
        '    Catch ex As Exception
        '        ThePage.HandleErrors(ex, Me.ErrorCtrl)
        '    Finally


        '        ClearSelectedDealerFile(ThePage.POPULATE_ACTION_SAVE)

        '    End Try
        'End Sub

        Public Sub InstallInterfaceProgressBar()
            'ThePage.DisplayProgressBarOnClick(BtnValidate_WRITE, "Interfaces")
            'ThePage.DisplayProgressBarOnClick(BtnLoadCertificate_WRITE, "Interfaces")
            'ThePage.DisplayProgressBarOnClick(BtnDeleteDealerFile_WRITE, "Interfaces")
            '   ThePage.DisplayProgressBarOnClick(btnCopyDealerFile_WRITE, "Interfaces")
            ThePage.InstallDisplayProgressBar()
        End Sub

        'Private Sub ExecuteAndWait(ByVal oSP As Integer, Optional ByVal filename As String = "")
        '    Dim intStatus As InterfaceStatusWrk
        '    Dim params As InterfaceBaseForm.Params

        '    Try
        '        If oSP = SP_DOWNLOAD Then
        '            ExecuteDownloadSp(filename)
        '        Else
        '            ExecuteSp(oSP)
        '        End If

        '        intStatus = New InterfaceStatusWrk(Me.TheState.intStatusId)
        '        Me.TheState.errorStatus = intStatus.WaitTilDone()
        '        If Me.TheState.errorStatus.status = InterfaceStatusWrk.IntStatus.INTERFACE_DB_FAILED Then
        '            ShowError(InterfaceStatusWrk.IntStatus.GetName(GetType(InterfaceStatusWrk.IntStatus), _
        '                            InterfaceStatusWrk.IntStatus.INTERFACE_DB_FAILED))
        '        Else
        '            params = SetParameters(Me.TheState.intStatusId, DEALERP_VARIABLE_NAME)
        '            Session(InterfaceBaseForm.SESSION_PARAMETERS_KEY) = params
        '            If TheState.errorStatus.status = InterfaceStatusWrk.IntStatus.PENDING Then
        '                TheInterfaceProgress.EnableInterfaceProgress(DEALERP_VARIABLE_NAME)
        '            Else
        '                AfterProgressBar()
        '            End If
        '        End If
        '    Catch ex As Threading.ThreadAbortException
        '    Catch ex As Exception
        '        ThePage.HandleErrors(ex, Me.ErrorCtrl)
        '        'Finally
        '        '    If TheState.errorStatus.status = InterfaceStatusWrk.IntStatus.PENDING Then
        '        '        TheInterfaceProgress.EnableInterfaceProgress(DEALERP_VARIABLE_NAME)
        '        '    Else
        '        '        AfterProgressBar()
        '        '    End If
        '    End Try
        'End Sub

        Private Sub ExecuteAndWait(ByVal oSP As Integer, Optional ByVal filename As String = "")
            Dim intStatus As InterfaceStatusWrk
            Dim params As InterfaceBaseForm.Params

            Try
                If oSP = SP_DOWNLOAD Then
                    ExecuteDownloadSp(filename)
                Else
                    ExecuteSp(oSP)
                End If

                'intStatus = New InterfaceStatusWrk(Me.TheState.intStatusId)
                'Me.TheState.errorStatus = intStatus.WaitTilDone()
                'If Me.TheState.errorStatus.status = InterfaceStatusWrk.IntStatus.INTERFACE_DB_FAILED Then
                '    ShowError(InterfaceStatusWrk.IntStatus.GetName(GetType(InterfaceStatusWrk.IntStatus), _
                '                    InterfaceStatusWrk.IntStatus.INTERFACE_DB_FAILED))
                '  Else
                '   params = SetParameters(Me.TheState.intStatusId, DEALERP_VARIABLE_NAME)
                '    Session(InterfaceBaseForm.SESSION_PARAMETERS_KEY) = params
                '   If TheState.errorStatus.status = InterfaceStatusWrk.IntStatus.PENDING Then
                ' TheInterfaceProgress.EnableInterfaceProgress(DEALERP_VARIABLE_NAME)
                '  Else
                '     AfterProgressBar()
                ' End If
                ' End If
                params = SetParameters(Me.TheState.intStatusId, DEALERP_VARIABLE_NAME)
                Session(InterfaceBaseForm.SESSION_PARAMETERS_KEY) = params
                TheInterfaceProgress.EnableInterfaceProgress(DEALERP_VARIABLE_NAME)
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                ThePage.HandleErrors(ex, Me.ErrorCtrl)
                'Finally
                '    If TheState.errorStatus.status = InterfaceStatusWrk.IntStatus.PENDING Then
                '        TheInterfaceProgress.EnableInterfaceProgress(DEALERP_VARIABLE_NAME)
                '    Else
                '        AfterProgressBar()
                '    End If
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
            Me.ErrorCtrl.AddError(msg)
            Me.ErrorCtrl.Show()
            AppConfig.Log(New Exception(msg))
        End Sub

#End Region

#Region "Button-Management"

        Private Sub ExecuteSp(ByVal oSP As Integer)
            Dim oDealerFileProcessedData As New DealerFileProcessedData
            Dim oInterfaceStatusWrk As New InterfaceStatusWrk

            If Not TheState.SelectedDealerFileProcessedId.Equals(Guid.Empty) Then
                Dim oDealerFileProcessed As New DealerFileProcessed(TheState.SelectedDealerFileProcessedId)
                If oInterfaceStatusWrk.IsfileBeingProcessed(oDealerFileProcessed.Filename) Then
                    With oDealerFileProcessedData
                        .dealerfile_processed_id = TheState.SelectedDealerFileProcessedId
                        .DealerType = Me.TheState.DealerType
                        .filename = oDealerFileProcessed.Filename
                        .layout = oDealerFileProcessed.Layout
                        .fileTypeCode = TheState.moInterfaceTypeCode
                        .oSP = oSP
                    End With
                    Select Case oSP
                        Case SP_VALIDATE
                            DealerFileProcessed.ValidateFile(oDealerFileProcessedData)
                        Case SP_PROCESS
                            DealerFileProcessed.ProcessFileRecords(oDealerFileProcessedData)
                        Case SP_DELETE
                            DealerFileProcessed.DeleteFile(oDealerFileProcessedData)
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

        Private Function uploadDealerFile() As String
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

            Dim webServerPath As String = GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, ElitaPlusPrincipal.Current.ActiveUser.NetworkId)
            Dim webServerFile As String = webServerPath & "\" & System.IO.Path.GetFileName(dealerFileName)
            layoutFileName = webServerPath & "\" & _
                System.IO.Path.GetFileNameWithoutExtension(webServerFile) & AppConfig.UnixServer.FtpTriggerExtension
            CreateFolder(webServerPath)
            If Not TheState.SelectedDealerFileLayout Is Nothing Then
                File.WriteAllBytes(webServerFile, fileBytes)
                File.WriteAllBytes(layoutFileName, System.Text.Encoding.ASCII.GetBytes(TheState.SelectedDealerFileLayout))
            Else
                Throw New GUIException("Missing File Layout Code", Assurant.ElitaPlus.Common.ErrorCodes.GUI_MISSING_FILE_LAYOUT_CODE)
            End If

            Dim unixPath As String = AppConfig.UnixServer.FtpDirectory
            '' ''Dim objUnixFTP As New aFtp(AppConfig.UnixServer.HostName, unixPath, AppConfig.UnixServer.UserId, _
            '' ''                     AppConfig.UnixServer.Password, PORT)
            Dim objUnixFTP As New sFtp(AppConfig.UnixServer.HostName, unixPath, AppConfig.UnixServer.UserId, _
                                 AppConfig.UnixServer.Password)
            Try
                '' ''If (objUnixFTP.Login()) Then
                '' ''    objUnixFTP.UploadFile(webServerFile, False)
                '' ''    objUnixFTP.UploadFile(layoutFileName, False)
                '' ''End If
                objUnixFTP.UploadFile(webServerFile)
                objUnixFTP.UploadFile(layoutFileName)

                Return dealerFileName
            Catch ex As Exception
                ThePage.HandleErrors(ex, Me.ErrorCtrl)
            Finally
                '' ''objUnixFTP.CloseConnection()
            End Try

        End Function

        ' SHH, Scp
        'Private Function uploadDealerFile() As String
        '    Dim dealerFileName As String
        '    Dim layoutFileName As String
        '    Dim dealerSimpleName, layoutSimpleName, unixFile As String
        '    Dim fileLen As Integer = dealerFileInput.PostedFile.ContentLength
        '    DealerFileProcessed.ValidateFileName(fileLen)
        '    dealerFileName = dealerFileInput.PostedFile.FileName

        '    Dim fileBytes(fileLen - 1) As Byte
        '    Dim objStream As System.IO.Stream
        '    objStream = dealerFileInput.PostedFile.InputStream
        '    objStream.Read(fileBytes, 0, fileLen)

        '    Dim webServerPath As String = GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, ElitaPlusPrincipal.Current.Identity.Name)
        '    dealerSimpleName = System.IO.Path.GetFileName(dealerFileName)
        '    Dim webServerFile As String = webServerPath & "\" & dealerSimpleName
        '    layoutSimpleName = System.IO.Path.GetFileNameWithoutExtension(webServerFile) & _
        '                        AppConfig.UnixServer.FtpTriggerExtension
        '    layoutFileName = webServerPath & "\" & layoutSimpleName

        '    CreateFolder(webServerPath)
        '    File.WriteAllBytes(webServerFile, fileBytes)
        '    File.WriteAllBytes(layoutFileName, System.Text.Encoding.ASCII.GetBytes(TheState.SelectedDealerFileLayout))

        '    Dim oScp As New Scp(AppConfig.UnixServer.HostName, _
        '                        AppConfig.UnixServer.UserId, AppConfig.UnixServer.Password)

        '    Try
        '        oScp.Connect()
        '        unixFile = AppConfig.UnixServer.FtpDirectory & "/" & dealerSimpleName
        '        oScp.Put(webServerFile, unixFile)
        '        unixFile = AppConfig.UnixServer.FtpDirectory & "/" & layoutSimpleName
        '        oScp.Put(layoutFileName, unixFile)
        '        Return dealerFileName
        '    Catch ex As Exception
        '        ThePage.HandleErrors(ex, Me.ErrorCtrl)
        '    Finally
        '        oScp.Close()
        '    End Try

        'End Function

        Private Sub DisableButtons()
            'Dim InterfaceTypeCode As DealerFileProcessedData.InterfaceTypeCode = Me.State.moInterfaceTypeCode
            ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
            ControlMgr.SetEnableControl(ThePage, BtnLoadCertificate_WRITE, False)
            ControlMgr.SetEnableControl(ThePage, BtnDeleteDealerFile_WRITE, False)
            ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
            'If CBool(TheState.moInterfaceTypeCode.PAYM) Then
            '    ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
            '    ControlMgr.SetVisibleControl(ThePage, BtnProcessedExport, False)
            'End If
            Select Case TheState.moInterfaceTypeCode
                Case DealerFileProcessedData.InterfaceTypeCode.CERT
                    ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                Case DealerFileProcessedData.InterfaceTypeCode.PAYM
                    ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                Case DealerFileProcessedData.InterfaceTypeCode.TLMK
                    ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                    ControlMgr.SetVisibleControl(ThePage, BtnProcessedExport, False)
                Case DealerFileProcessedData.InterfaceTypeCode.RINS
                    ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, False)
                    ControlMgr.SetVisibleControl(ThePage, BtnProcessedExport, False)
                    ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
                    ControlMgr.SetEnableControl(ThePage, BtnErrorExport, False)

                Case Else
                    Return
            End Select

        End Sub

        Private Sub EnableDisableButtons()
            If Not TheState.SelectedDealerFileProcessedId.Equals(Guid.Empty) Then
                Dim oDealerFile As DealerFileProcessed = New DealerFileProcessed(TheState.SelectedDealerFileProcessedId)
                DisableButtons()
                With oDealerFile
                    If .Received.Value = .Counted.Value Then
                        ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, True)
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
                        ControlMgr.SetEnableControl(ThePage, BtnRejectReport, True)
                        ControlMgr.SetEnableControl(ThePage, BtnErrorExport, True)
                    ElseIf .Rejected.Value > 0 AndAlso TheState.moInterfaceTypeCode <> DealerFileProcessedData.InterfaceTypeCode.RINS Then
                        ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
                        ControlMgr.SetEnableControl(ThePage, BtnErrorExport, False)
                    Else
                        ControlMgr.SetEnableControl(ThePage, BtnRejectReport, False)
                        ControlMgr.SetEnableControl(ThePage, BtnErrorExport, False)
                    End If

                    If .Validated.Value > 0 Then ControlMgr.SetEnableControl(ThePage, BtnLoadCertificate_WRITE, True)
                    If .Received.Value = .Loaded.Value Then
                        ControlMgr.SetEnableControl(ThePage, BtnDeleteDealerFile_WRITE, True)
                        ControlMgr.SetEnableControl(ThePage, BtnLoadCertificate_WRITE, False)
                        ControlMgr.SetEnableControl(ThePage, BtnValidate_WRITE, False)
                    End If

                    If (.Loaded.Value = .Counted.Value) Or (.Loaded.Value = 0) Then
                        ControlMgr.SetEnableControl(ThePage, BtnDeleteDealerFile_WRITE, True)
                    End If

                    If Me.TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.CERT AndAlso .Loaded.Value > 0 Then
                        ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, True)
                    End If

                    If Me.TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.PAYM AndAlso .Loaded.Value > 0 Then
                        ControlMgr.SetEnableControl(ThePage, BtnProcessedExport, True)
                    End If

                    'ControlMgr.SetEnableControl(ThePage, BtnDeleteDealerFile_WRITE, True)
                    If TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.TLMK Then
                        ControlMgr.SetVisibleControl(ThePage, BtnErrorExport, False)
                        ControlMgr.SetVisibleControl(ThePage, BtnRejectReport, False)
                    End If

                    If TheState.moInterfaceTypeCode = DealerFileProcessedData.InterfaceTypeCode.RINS Then
                        ControlMgr.SetVisibleControl(ThePage, BtnErrorExport, False)
                        ControlMgr.SetVisibleControl(ThePage, BtnRejectReport, False)
                    End If

                End With
            Else
                Throw New GUIException("You must select a dealer file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)

            End If
        End Sub

        Public Sub EnableDisableEditControl()
            Dim i As Integer
            Dim edt As ImageButton

            '  Enable or Disable all the EDIT  buttons on the DataGrid
            For i = 0 To (Me.moDataGrid.Items.Count - 1)
                edt = CType(Me.moDataGrid.Items(i).Cells(ThePage.EDIT_COL).FindControl(ThePage.EDIT_CONTROL_NAME), ImageButton)
                If Not edt Is Nothing Then
                    edt.Enabled = (Me.moDataGrid.Items(i).Cells(Me.GRID_COL_REJECTED_IDX).Text.Trim() <> "0" _
                                   OrElse Me.moDataGrid.Items(i).Cells(Me.GRID_COL_BYPASSED_IDX).Text.Trim() <> "0")
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
            moDataGrid.CurrentPageIndex = ThePage.NO_PAGE_INDEX
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

        'Private Sub LoadRequiredFieldControlData()
        '    With moReqValidator
        '        .ControlToValidate = "dealerFileInput"
        '        .ErrorMessage = ThePage.TranslateLabelOrMessage(DEALER_FILE_REQUIRED)
        '        .Display = ValidatorDisplay.Dynamic
        '    End With
        'End Sub

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
            End If
        End Sub

#End Region

#Region "Populate"

        'Sub PopulateDealerDropDown()
        '    Try
        '        Dim oCompanyIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
        '        Select Case State.moInterfaceTypeCode
        '            Case DealerFileProcessedData.InterfaceTypeCode.CERT
        '                ThePage.BindListControlToLookupList(moDealerDrop, ElitaPlusLookupListFactory.DealerLookupList(ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID))
        '                Dim DealersLookupListSortedByCode As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
        '                DealersLookupListSortedByCode.Sort = "CODE"
        '                ThePage.BindListControlToDataView(cboDealerCode, DealersLookupListSortedByCode, "CODE", , True)

        '            Case DealerFileProcessedData.InterfaceTypeCode.PAYM
        '                Dim DealersLookupListSortedByCode As System.Data.DataView = LookupListNew.GetDealerMonthlyBillingLookupList(oCompanyId)
        '                ThePage.BindListControlToDataView(moDealerDrop, _
        '                                    LookupListNew.GetDealerMonthlyBillingLookupList(oCompanyId))
        '                ThePage.BindListControlToDataView(cboDealerCode, _
        '                                                   LookupListNew.GetDealerMonthlyBillingLookupList(oCompanyId), "CODE", , True)
        '            Case Else
        '                Return
        '        End Select
        '        ThePage.BindSelectItem(Me.State.SelectedDealerId.ToString, moDealerDrop)
        '        ThePage.BindSelectItem(Me.State.SelectedDealerId.ToString, cboDealerCode)
        '        '---------------------------------
        '        'Me.BindListControlToLookupList(Me.cboDealer, ElitaPlusLookupListFactory.DealerLookupList(GetApplicationUser.CompanyID))
        '        'Dim DealersLookupListSortedByCode As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.CompanyId)
        '        ' Dim DealersLookupListSortedByCode As System.Data.DataView = ElitaPlusLookupListFactory.DealerLookupList(oCompanyId)
        '        ''DealersLookupListSortedByCode.Sort = "CODE"
        '        'BindListControlToDataView(cboDealerCode, ElitaPlusLookupListFactory.DealerLookupList(oCompanyId), "CODE", , True)

        '        '---------------------------------

        '    Catch ex As Exception
        '        ThePage.HandleErrors(ex, ErrorCtrl)
        '    End Try
        'End Sub

        Sub PopulateDealerDropDown()
            Try
                ''Dim oCompanyIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
                ''Select Case TheState.moInterfaceTypeCode
                ''    Case DealerFileProcessedData.InterfaceTypeCode.CERT
                ''        ThePage.BindListControlToDataView(moDealerDrop, LookupListNew.GetDealerLookupList(oCompanyIds, False, "Code", "CODE"))
                ''        ThePage.BindListControlToDataView(cboDealerCode, LookupListNew.GetDealerLookupList(oCompanyIds, False, "Code", "CODE"), "CODE", , True)

                ''    Case DealerFileProcessedData.InterfaceTypeCode.PAYM
                ''        ThePage.BindListControlToDataView(moDealerDrop, LookupListNew.GetDealerMonthlyBillingLookupList(oCompanyIds, "CODE"))
                ''        ThePage.BindListControlToDataView(cboDealerCode, LookupListNew.GetDealerMonthlyBillingLookupList(oCompanyIds, "CODE"), "CODE", , True)
                ''    Case Else
                ''        Return
                ''End Select
                ''ThePage.BindSelectItem(TheState.SelectedDealerId.ToString, moDealerDrop)
                ''ThePage.BindSelectItem(TheState.SelectedDealerId.ToString, cboDealerCode)


                Dim oCompanyIds As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies
                Dim dv As DataView
                Select Case TheState.moInterfaceTypeCode
                    Case DealerFileProcessedData.InterfaceTypeCode.CERT
                        dv = LookupListNew.GetDealerLookupList(oCompanyIds, False, "Code", "CODE")
                    Case DealerFileProcessedData.InterfaceTypeCode.PAYM
                        dv = LookupListNew.GetDealerMonthlyBillingLookupList(oCompanyIds, "CODE")
                    Case DealerFileProcessedData.InterfaceTypeCode.TLMK
                        dv = LookupListNew.GetDealerLookupList(oCompanyIds, False, "Code", "CODE")
                    Case DealerFileProcessedData.InterfaceTypeCode.RINS
                        dv = LookupListNew.GetDealerLookupList(oCompanyIds, False, "Code", "CODE")
                    Case Else
                        Return
                End Select

                DealerMultipleDrop.NothingSelected = True
                DealerMultipleDrop.SetControl(True, DealerMultipleDrop.MODES.NEW_MODE, True, dv, "* " & TranslationBase.TranslateLabelOrMessage(LABEL_SELECT_DEALER), True)
                '''If dv.Count.Equals(ONE_ITEM) Then
                '''    DealerMultipleDrop.SelectedIndex = Me.ONE_ITEM
                '''End If

                ' If Not Me.TheState.SelectedDealerId = Guid.Empty Then
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
                ThePage.HandleErrors(ex, ErrorCtrl)
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
                    Case Else
                        Return
                End Select

                If (GetDealerTypeID.Equals(dealerTypeVSC)) Then
                    ControlMgr.SetVisibleControl(ThePage, DealerGroupMultipleDrop, True)
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

                Else
                    ControlMgr.SetVisibleControl(ThePage, DealerGroupMultipleDrop, False)
                End If

            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Function GetDataView() As DataView
            Dim oDealerFileData As DealerFileProcessedData = New DealerFileProcessedData
            Dim oDataView As DataView

            With oDealerFileData
                '  If Not DealerMultipleDrop.SelectedGuid.Equals(Guid.Empty) Then
                .dealerCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALERS, DealerMultipleDrop.SelectedGuid)
                .dealerId = DealerMultipleDrop.SelectedGuid
                .fileTypeCode = TheState.moInterfaceTypeCode
                '  oDataView = DealerFileProcessed.LoadList(ElitaPlusIdentity.Current.ActiveUser.Companies, oDealerFileData)
                '  Else
                .dealergrpCode = LookupListNew.GetCodeFromId(LookupListNew.LK_DEALER_GROUPS, DealerGroupMultipleDrop.SelectedGuid)
                .dealergroupId = DealerGroupMultipleDrop.SelectedGuid
                ' .fileTypeCode = TheState.moInterfaceTypeCode
                oDataView = DealerFileProcessed.LoadList(ElitaPlusIdentity.Current.ActiveUser.Companies, oDealerFileData)
                '   End If
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
            End If
            Dim sDirectory As String
            If Not TheState.SelectedDealerId.Equals(Guid.Empty) Then
                sDirectory = AppConfig.FileClientDirectory
                Dim tempDealerInfo As DealerFileProcessed.DealerInfo = DealerFileProcessed.GetDealerLayout( _
                                        TheState.SelectedDealerId, TheState.moInterfaceTypeCode)
                TheState.SelectedDealerFileLayout = tempDealerInfo.layout
                TheState.SelectedDealerCode = tempDealerInfo.dealerCode
                Dim dateStr As String = System.DateTime.Now.ToString("yyMMdd")
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
                        sFileName = sDirectory & TheState.SelectedDealerCode.Trim() & dateStr & ".TXT"
                End Select

            ElseIf Not TheState.SelectedDealerGroupId.Equals(Guid.Empty) Then
                Dim dv As DataView
                dv = Dealer.getFirstDealerByDealerGrp(TheState.SelectedDealerGroupId)

                If dv.Count > 0 Then
                    sDirectory = AppConfig.FileClientDirectory
                    Dim tempDealerInfo As DealerFileProcessed.DealerInfo = DealerFileProcessed.GetDealerLayout( _
                                             GuidControl.ByteArrayToGuid(CType(dv(FIRST_POS)(COL_NAME), Byte())), TheState.moInterfaceTypeCode)
                    TheState.SelectedDealerFileLayout = tempDealerInfo.layout
                    TheState.SelectedDealerGroupCode = TheState.SelectedDealerGroupCode
                    Dim dateStr As String = System.DateTime.Now.ToString("yyMMdd")
                    Select Case TheState.moInterfaceTypeCode
                        Case DealerFileProcessedData.InterfaceTypeCode.CERT
                            sFileName = sDirectory & TheState.SelectedDealerGroupCode.Trim() & dateStr & ".TXT"
                    End Select
                End If

            End If
            moExpectedFileLabel_NO_TRANSLATE.Text = sFileName
        End Sub


        'Private Sub PopulateGrid()
        '    Dim dv As DataView

        '    SetExpectedFile()
        '    dv = GetDataView()
        '    Me.moDataGrid.AutoGenerateColumns = False
        '    ThePage.SetPageAndSelectedIndexFromGuid(dv, Me.State.SelectedDealerFileProcessedId, Me.moDataGrid, moDataGrid.CurrentPageIndex)
        '    Me.moDataGrid.DataSource = dv
        '    Me.moDataGrid.DataBind()
        '    Me.moDataGrid.Visible = Me.State.IsGridVisible
        '    EnableDisableEditControl()
        'End Sub

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
                'Reset Selected item background and enable / disable controls
                ThePage.SetGridItemStyleColor(Me.moDataGrid)
                'EnableDisableButtons()
                ControlMgr.SetVisibleForControlFamily(ThePage, moButtonPanel, True, True)
                ControlMgr.SetVisibleForControlFamily(ThePage, moUpLoadPanel, True, True)

                'DEF-2447 : END
                SetExpectedFile()
                'If Me.TheState.oDataView Is Nothing Then
                Me.TheState.oDataView = GetDataView()
                'End If
                ThePage.BasePopulateGrid(moDataGrid, Me.TheState.oDataView, TheState.SelectedDealerFileProcessedId, oAction)
                ThePage.SetPageAndSelectedIndexFromGuid(Me.TheState.oDataView, TheState.SelectedDealerFileProcessedId, moDataGrid, TheState.mnPageIndex)
                EnableDisableEditControl()
            Catch ex As Exception
                ThePage.HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region


    End Class


End Namespace
