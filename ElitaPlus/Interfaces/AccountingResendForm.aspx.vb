Public Partial Class AccountingResendForm
    Inherits ElitaPlusPage


#Region "Constants"


    Public Const URL As String = "Interfaces/AccountingResendForm.aspx"
    Public Const PAGETITLE As String = "ACCOUNTING_RESEND"
    Public Const PAGETAB As String = "INTERFACES"

    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const ONE_ITEM As Integer = 1
    Private Const RETURN_SUCCESS As String = "0"
    Private Const EDIT_COMMAND_NAME As String = "EditRecord"

    Private Const GRID_COL_ACCT_TRANSMISSION_ID_IDX As Integer = 0

    Protected WithEvents moUserCompanyMultipleDrop As Common.MultipleColumnDDLabelControl

#End Region

#Region "PAGE STATE"

    Class MyState
        Public pageIndex As Integer
        Public dvAcctTransFiles As AcctTransmission.AcctTransmissionSearchDV
        Public selectedRecordId As Guid
        Public companyId As Guid
    End Class

    Public Sub New()
        MyBase.New(New MyState)
    End Sub

    Protected Shadows ReadOnly Property State() As MyState
        Get
            Return CType(MyBase.State, MyState)
        End Get
    End Property
#End Region


#Region "Properties"

    Public ReadOnly Property UserCompanyMultipleDrop() As Common.MultipleColumnDDLabelControl
        Get
            If moUserCompanyMultipleDrop Is Nothing Then
                moUserCompanyMultipleDrop = CType(FindControl("moUserCompanyMultipleDrop"), Common.MultipleColumnDDLabelControl)
            End If
            Return moUserCompanyMultipleDrop
        End Get
    End Property

#End Region

#Region "Page Init"

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        'Put user code to initialize the page here
        ErrControllerMaster.Clear_Hide()

        ' Disable Report button's Submit Behavior
        btnReport.UseSubmitBehavior = False
        btnReport.OnClientClick = "_spFormOnSubmitCalled = false; _spSuppressFormOnSubmitWrapper=true;"

        Try

            DisplayProgressBarOnClick(btnExecute, ElitaPlusWebApp.Message.MSG_PERFORMING_REQUEST)
            AddConfirmation(btnDelete, Message.DELETE_RECORD_PROMPT)

            If Not IsPostBack Then
                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)

                PopulateAll()

            End If

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

#End Region


#Region "POPULATE"


    Private Sub PopulateAll()

        Try

            'Fill Companies
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, UserCompanyMultipleDrop.NO_CAPTION, True)
            If dv.Count.Equals(ONE_ITEM) Then
                UserCompanyMultipleDrop.SelectedIndex = ONE_ITEM
                UserCompanyMultipleDrop.ChangeEnabledControlProperty(False)
                Dim _company As New Company(UserCompanyMultipleDrop.SelectedGuid)
                PopulateGrid(_company.Id)
                State.companyId = _company.Id
            Else
                EnableDisableButtons()
            End If

        Catch ex As Exception
            ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
        End Try


    End Sub

    Protected Sub PopulateGrid(CompanyId As Guid)

        Try
            State.dvAcctTransFiles = AcctTransmission.GetFailures(CompanyId)
            Dim sTmpFileFilterCondition As String = "file_name not like '%.TMP'"
            State.dvAcctTransFiles.RowFilter = sTmpFileFilterCondition
            moDataGrid.SelectedIndex = -1
            moDataGrid.DataSource = State.dvAcctTransFiles
            moDataGrid.DataBind()

            EnableDisableButtons()

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
        

    End Sub


#End Region

#Region "EVENT HANDLERS"

    Private Sub moCompanyDropDown_SelectedIndexChanged(moUserCompanyMultipleDrop As Common.MultipleColumnDDLabelControl) Handles moUserCompanyMultipleDrop.SelectedDropChanged
        Try
            If UserCompanyMultipleDrop.SelectedIndex = 0 Then
                ErrControllerMaster.AddErrorAndShow(ElitaPlus.Common.ErrorCodes.GUI_COMPANY_IS_REQUIRED)
                Exit Sub
            End If
            Dim _company As New Company(UserCompanyMultipleDrop.SelectedGuid)
            PopulateGrid(_company.Id)
            State.companyId = _company.Id

        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub moDataGrid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moDataGrid.ItemDataBound

        If e.Item.ItemType = ListItemType.AlternatingItem OrElse e.Item.ItemType = ListItemType.Item Then
            Dim drv As DataRowView = CType(e.Item.DataItem, DataRowView)
            e.Item.Cells(GRID_COL_ACCT_TRANSMISSION_ID_IDX).Text = GuidControl.ByteArrayToGuid(CType(drv(AcctTransmission.AcctTransmissionSearchDV.COL_ACCT_TRANSMISSION_ID), Byte())).ToString
        End If

    End Sub

    Private Sub moDataGrid_PageIndexChanged(source As System.Object, _
               e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles moDataGrid.PageIndexChanged
        Try
            moDataGrid.CurrentPageIndex = e.NewPageIndex
            State.pageIndex = moDataGrid.CurrentPageIndex
            moDataGrid.DataSource = State.dvAcctTransFiles
            moDataGrid.DataBind()
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try

    End Sub

    Private Sub moDataGrid_ItemCreated(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles moDataGrid.ItemCreated
        Dim _epSearchPage As New ElitaPlusSearchPage
        _epSearchPage.BaseItemCreated(sender, e)
    End Sub


    Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        Try
            If e.CommandName = EDIT_COMMAND_NAME Then
                moDataGrid.SelectedIndex = e.Item.ItemIndex
                State.selectedRecordId = New Guid(moDataGrid.SelectedItem.Cells(GRID_COL_ACCT_TRANSMISSION_ID_IDX).Text)
                EnableDisableButtons()
            End If
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As System.EventArgs) Handles btnBack.Click
        ReturnToTabHomePage()
    End Sub

    Private Sub btnExecute_Click(sender As Object, e As System.EventArgs) Handles btnExecute.Click

        If Not State.selectedRecordId.Equals(Guid.Empty) Then

            Try
                Dim _felitaEngine As New FelitaEngine(New Company(UserCompanyMultipleDrop.SelectedGuid).AcctCompanyId)
                Dim strReturn As String = ""

                strReturn = _felitaEngine.ResendFile(State.selectedRecordId)
                If strReturn = RETURN_SUCCESS Then
                    AddInfoMsg(ElitaPlusWebApp.Message.MSG_INTERFACES_HAS_COMPLETED)
                Else
                    ErrControllerMaster.AddErrorAndShow(strReturn)
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            Finally
                'Empty the state selection and repopulate the grid
                State.selectedRecordId = Guid.Empty
                PopulateGrid(New Company(UserCompanyMultipleDrop.SelectedGuid).Id)
            End Try
           
        End If

    End Sub

    Private Sub btnDelete_Click(sender As Object, e As System.EventArgs) Handles btnDelete.Click

        If Not State.selectedRecordId.Equals(Guid.Empty) Then

            Try

                Dim _accttrans As New AcctTransmission(State.selectedRecordId)

                'Grab associated files IF this is a vendor file.  Otherwise, only delete the current file
                If _accttrans.FileTypeFlag = BusinessObjectsNew.FelitaEngine.EventType.VEND Then
                    'Look for associated files (vendor records)
                    Dim dv As AcctTransmission.AcctTransmissionSearchDV
                    Dim _assocAcctTrans As AcctTransmission
                    dv = AcctTransmission.GetAssociatedFailures(_accttrans.FileName, _accttrans.BatchNumber)
                    For i As Integer = 0 To dv.Count - 1
                        _assocAcctTrans = New AcctTransmission(GuidControl.ByteArrayToGuid(dv.Item(i)(AcctTransmission.AcctTransmissionSearchDV.COL_ACCT_TRANSMISSION_ID)))
                        _assocAcctTrans.Delete()
                        _assocAcctTrans.Save()
                    Next
                End If

                _accttrans.Delete()
                _accttrans.Save()

                AddInfoMsg(ElitaPlusWebApp.Message.MSG_DELETE_WAS_COMPLETED_SUCCESSFULLY)

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            Finally
                'Empty the state selection and repopulate the grid
                State.selectedRecordId = Guid.Empty
                PopulateGrid(New Company(UserCompanyMultipleDrop.SelectedGuid).Id)
            End Try

        End If

    End Sub

    Private Sub btnReport_Click(sender As Object, e As System.EventArgs) Handles btnReport.Click

        Try
            If Not State.selectedRecordId.Equals(Guid.Empty) Then

                Dim _company As New Company(State.companyId)
                Dim _acctTrans As New AcctTransmission(State.selectedRecordId, True)

                Dim param As New Reports.SmartStreamInterfaceDetail.MyState
                param.CompanyId = _company.Id
                param.CompanyDescription = _company.Description
                param.FileName = _acctTrans.FileName
                param.TransmissionId = _acctTrans.Id
                param.BeginDate = String.Format("{0:dd-MMM-yyyy}", _acctTrans.CreatedDate)
                param.EndDate = String.Format("{0:dd-MMM-yyyy}", _acctTrans.CreatedDate)

                
                callPage(Reports.SmartStreamInterfaceDetail.URL, param)

               
            Else
                Throw New GUIException("You must select a dealer file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub


#End Region

#Region "Controlling Logic"
    Private Sub EnableDisableButtons()
        If Not State.selectedRecordId.Equals(Guid.Empty) Then
            EnableButtons()
        Else
            DisableButtons()
        End If

    End Sub

    Private Sub EnableButtons()

        ControlMgr.SetEnableControl(Me, btnDelete, True)
        ControlMgr.SetEnableControl(Me, btnExecute, True)
        ControlMgr.SetEnableControl(Me, btnReport, True)

    End Sub

    Private Sub DisableButtons()

        ControlMgr.SetEnableControl(Me, btnDelete, False)
        ControlMgr.SetEnableControl(Me, btnExecute, False)
        ControlMgr.SetEnableControl(Me, btnReport, False)

    End Sub



#End Region

   


End Class