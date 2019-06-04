Partial Public Class InvoiceReconWrkForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "InvoiceReconWrkForm.aspx"
    Public Const PAGETITLE As String = "VENDOR_INVOICE_FILE"
    Public Const PAGETAB As String = "INTERFACES"

#Region "VI Members"
    Private Const COL_ReconWrk_ID As Integer = 0
    Private Const COL_Record_Type As Integer = 1
    Private Const COL_Reject_Code As Integer = 2
    Private Const COL_Reject_Reason As Integer = 3
    Private Const COL_Company_Code As Integer = 4

    Private Const COL_Invoice_Number As Integer = 5
    Private Const COL_Invoice_Date As Integer = 6
    Private Const COL_Repair_Date As Integer = 7
    Private Const COL_Due_Date As Integer = 8
    Private Const COL_Attributes As Integer = 9
    Private Const COL_Service_Center As Integer = 10

    Private Const COL_Claim_Number As Integer = 11

    Private Const COL_Authorization_Number As Integer = 12

    Private Const COL_Line_Item_Number As Integer = 13
    Private Const COL_Vendor_SKU As Integer = 14
    Private Const COL_Vendor_SKU_DESCRIPTION As Integer = 15
    Private Const COL_SERVICE_LEVEL As Integer = 16


    Private Const COL_Amount As Integer = 17
    Private Const COL_Modified_Date As Integer = 18

    Private Const Ctl_Record_Type As String = "txtRecordType"
    Private Const Ctl_Reject_Code As String = "txtRejectCode"
    Private Const Ctl_Reject_Reason As String = "txtRejectReason"

    Private Const Ctl_Company_Code As String = "txtCompanyCode"

    Private Const Ctl_Invoice_Number As String = "txtInvoiceNumber"
    Private Const Ctl_Invoice_Date As String = "txtInvoiceDate"
    Private Const Ctl_Repair_Date As String = "txtRepairDate"
    Private Const Ctl_Due_Date As String = "txtDueDate"
    Private Const Ctl_Attributes As String = "txtAttributes"
    Private Const Ctl_Service_Center As String = "txtServiceCenter"

    Private Const Ctl_Claim_Number As String = "txtClaimNumber"

    Private Const Ctl_Authorization_Number As String = "txtAuthorizationNumber"

    Private Const Ctl_Line_Item_Number As String = "txtLineItemNumber"
    Private Const Ctl_SKU As String = "txtSKU"
    Private Const Ctl_SKU_Description As String = "txtSKUDescription"

    Private Const Ctl_Service_Level As String = "txtServiceLevel"

    Private Const Ctl_Amount As String = "txtAmount"
    'Private Const Ctl_Created_By As String = "txtCreated_By"
    'Private Const Ctl_Created_Date As String = "txtCreated_Date"
    'Private Const Ctl_Modified_By As String = "txtModified_By"
    'Private Const Ctl_Modified_Date As String = "txtModified_Date"


    Private Const Ctl_Invoice_Date_Calendar As String = "imgInvoiceDate"
    Private Const Ctl_Repair_Date_Calendar As String = "imgRepairDate"
    Private Const Ctl_Due_Date_Calendar As String = "imgDueDate"
#End Region
#End Region
#Region "Parameters"

    Public Class Parameters

        Private _moselectedFileName As String
        Public Property selectedFileName() As String
            Get
                Return _moselectedFileName
            End Get
            Set(ByVal value As String)
                _moselectedFileName = value
            End Set
        End Property
        Private _moselectedFileId As Guid
        Public Property selectedFileId() As Guid
            Get
                Return _moselectedFileId
            End Get
            Set(ByVal value As Guid)
                _moselectedFileId = value
            End Set
        End Property



    End Class




#End Region

#Region "Page State"

    Class MyState
        Public ClaimloadfileID As Guid
        Public ClaimloadfileName As String
        Public ActionInProgress As DetailPageCommand = ElitaPlusPage.DetailPageCommand.Nothing_
        Public PageSize As Integer = DEFAULT_PAGE_SIZE
        Public PageIndex As Integer = 0
        Public RecordCount As Integer
        'Public ClaimReconWrkId As Guid
        'Public PartsHashTable As Hashtable

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
        If Not CallingParameters Is Nothing Then
            Dim param As InvoiceReconWrkForm.Parameters = New InvoiceReconWrkForm.Parameters()
            param = CType(Me.CallingParameters, InvoiceReconWrkForm.Parameters)
            Me.State.ClaimloadfileID = param.selectedFileId
            Me.State.ClaimloadfileName = param.selectedFileName

        End If
    End Sub

#End Region

#Region "Page events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ErrControllerMaster.Clear_Hide()
        'Me.ErrController2.Clear_Hide()
        Try
            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)
                TranslateGridHeader(Grid)
                SetStateProperties()
                populateReadyOnly()
                PopulateGrid()
            Else
                CheckIfComingFromSaveConfirm()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub
#End Region

#Region "Helper Function"
    Protected Sub CheckIfComingFromSaveConfirm()
        Dim confResponse As String = Me.HiddenSavePagePromptResponse.Value
        Try
            If Not confResponse.Equals(String.Empty) Then
                If confResponse = Me.MSG_VALUE_YES Then
                    'SavePage()
                End If
                Me.HiddenSavePagePromptResponse.Value = String.Empty
                Me.HiddenIsPageDirty.Value = String.Empty

                Select Case Me.State.ActionInProgress
                    Case ElitaPlusPage.DetailPageCommand.Back
                        Dim retType As New ClaimLoadForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.ClaimloadfileID)
                        Me.ReturnToCallingPage(retType)
                    Case ElitaPlusPage.DetailPageCommand.GridPageSize
                        Grid.PageIndex = NewCurrentPageIndex(Grid, State.RecordCount, State.PageSize)
                        State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                    Case Else
                        Grid.PageIndex = State.PageIndex
                End Select
                PopulateGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Public Sub PopulateGrid()
        Dim dv As DataView
        dv = InvoiceReconWrk.LoadList(State.ClaimloadfileID)
        State.RecordCount = dv.Count

        SetPageAndSelectedIndexFromGuid(dv, Nothing, Me.Grid, Me.State.PageIndex, False)
        Me.Grid.DataSource = dv
        Me.Grid.DataBind()
        Me.lblRecordCount.Text = dv.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
        If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True
    End Sub

    Public Sub populateReadyOnly()
        moFileNameText.Text = State.ClaimloadfileName
    End Sub

    Function IsDataGPageDirty() As Boolean
        Dim Result As String = Me.HiddenIsPageDirty.Value.ToUpper
        Return Result.Equals("YES")
    End Function

    Private Sub SavePage()
        Dim index As Integer = 0
        Dim objRecon As InvoiceReconWrk
        Dim totItems As Integer = Me.Grid.Rows.Count

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

    Private Function CreateBoFromGrid(ByVal index As Integer) As InvoiceReconWrk
        Dim ReconWrkId As Guid
        Dim objReconWrk As InvoiceReconWrk
        Dim sModifiedDate As String

        'moDataGrid.SelectedIndex = index
        'ReconWrkId = New Guid(Grid.Rows(index).Cells(Me.COL_ReconWrk_ID).Text)
        ReconWrkId = New Guid(GetGridText(Grid, index, COL_ReconWrk_ID))
        sModifiedDate = GetGridText(Grid, index, COL_Modified_Date)
        objReconWrk = New InvoiceReconWrk(ReconWrkId, sModifiedDate)
        Return objReconWrk
    End Function

    Private Sub PopulateBOFromForm(ByVal objReconWrk As InvoiceReconWrk)

        PopulateBOItem(objReconWrk, "RecordType", COL_Record_Type)
        PopulateBOItem(objReconWrk, "RejectCode", COL_Reject_Code)
        PopulateBOItem(objReconWrk, "RejectReason", COL_Reject_Reason)

        PopulateBOItem(objReconWrk, "CompanyCode", COL_Company_Code)

        PopulateBOItem(objReconWrk, "InvoiceNumber", COL_Invoice_Number)
        PopulateBOItem(objReconWrk, "InvoiceDate", COL_Invoice_Date)
        PopulateBOItem(objReconWrk, "Attributes", COL_Attributes)

        PopulateBOItem(objReconWrk, "ServiceCenterCode", COL_Service_Center)

        PopulateBOItem(objReconWrk, "ClaimNumber", COL_Claim_Number)

        PopulateBOItem(objReconWrk, "AuthorizationNumber", COL_Authorization_Number)

        PopulateBOItem(objReconWrk, "LineItemNumber", COL_Line_Item_Number)
        PopulateBOItem(objReconWrk, "VendorSku", COL_Vendor_SKU)
        PopulateBOItem(objReconWrk, "VendorSkuDescription", COL_Vendor_SKU_DESCRIPTION)


        PopulateBOItem(objReconWrk, "Amount", COL_Amount)

        PopulateBOItem(objReconWrk, "RepairDate", COL_Repair_Date)
        PopulateBOItem(objReconWrk, "DueDate", COL_Due_Date)
        PopulateBOItem(objReconWrk, "ServiceLevel", COL_SERVICE_LEVEL)
        'PopulateBOItem(objReconWrk, "CreatedBy", COL_Created_By)
        'PopulateBOItem(objReconWrk, "CreatedDate", COL_Created_Date)
        'PopulateBOItem(objReconWrk, "ModifiedBy", COL_Modified_By)
        'PopulateBOItem(objReconWrk, "ModifiedDate", COL_Modified_Date)


        'If Me.ErrCollection.Count > 0 Then
        '    Throw New PopulateBOErrorException
        'End If
    End Sub

    Private Sub PopulateBOItem(ByVal objReconWrk As InvoiceReconWrk, ByVal oPropertyName As String, ByVal oCellPosition As Integer)
        PopulateBOProperty(objReconWrk, oPropertyName, CType(Me.GetSelectedGridControl(Grid, oCellPosition), TextBox))
    End Sub

    Protected Sub BindBoPropertiesToGridHeaders(ByVal objRecon As InvoiceReconWrk)
        Me.BindBOPropertyToGridHeader(objRecon, "RecordType", Grid.Columns(COL_Record_Type))
        Me.BindBOPropertyToGridHeader(objRecon, "RejectCode", Grid.Columns(COL_Reject_Code))
        Me.BindBOPropertyToGridHeader(objRecon, "RejectReason", Grid.Columns(COL_Reject_Reason))

        Me.BindBOPropertyToGridHeader(objRecon, "CompanyCode", Grid.Columns(COL_Company_Code))

        Me.BindBOPropertyToGridHeader(objRecon, "InvoiceNumber", Grid.Columns(COL_Invoice_Number))
        Me.BindBOPropertyToGridHeader(objRecon, "InvoiceDate", Grid.Columns(COL_Invoice_Date))
        Me.BindBOPropertyToGridHeader(objRecon, "RepairDate", Grid.Columns(COL_Repair_Date))
        Me.BindBOPropertyToGridHeader(objRecon, "DueDate", Grid.Columns(COL_Due_Date))
        Me.BindBOPropertyToGridHeader(objRecon, "Attributes", Grid.Columns(COL_Attributes))
        Me.BindBOPropertyToGridHeader(objRecon, "ServiceCenterCode", Grid.Columns(COL_Service_Center))

        Me.BindBOPropertyToGridHeader(objRecon, "ClaimNumber", Grid.Columns(COL_Claim_Number))

        Me.BindBOPropertyToGridHeader(objRecon, "AuthorizationNumber", Grid.Columns(COL_Authorization_Number))

        Me.BindBOPropertyToGridHeader(objRecon, "LineItemNumber", Grid.Columns(COL_Line_Item_Number))
        Me.BindBOPropertyToGridHeader(objRecon, "VendorSKU", Grid.Columns(COL_Vendor_SKU))
        Me.BindBOPropertyToGridHeader(objRecon, "VendorSKUDescription", Grid.Columns(COL_Vendor_SKU_DESCRIPTION))


        Me.BindBOPropertyToGridHeader(objRecon, "ServiceLevel", Grid.Columns(COL_SERVICE_LEVEL))
        Me.BindBOPropertyToGridHeader(objRecon, "Amount", Grid.Columns(COL_Amount))

        Me.ClearGridViewHeadersAndLabelsErrSign()
    End Sub


#End Region

#Region "Grid related"

    Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            Me.State.PageIndex = Grid.PageIndex
            Me.PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub
    Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub


    Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
        Try
            BaseItemBound(sender, e)
            If e.Row.RowType = DataControlRowType.DataRow Then
                Dim ctlTxt As TextBox, ctlCanlendar As ImageButton
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)

                ctlTxt = CType(e.Row.Cells(COL_Record_Type).FindControl(Ctl_Record_Type), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_RECORD_TYPE).ToString

                CType(e.Row.Cells(COL_Reject_Code).FindControl(Ctl_Reject_Code), TextBox).Text = dvRow(InvoiceReconWrk.COL_NAME_REJECT_CODE).ToString()
                CType(e.Row.Cells(COL_Reject_Reason).FindControl(Ctl_Reject_Reason), TextBox).Text = dvRow(InvoiceReconWrk.COL_NAME_REJECT_REASON).ToString()

                ctlTxt = CType(e.Row.Cells(COL_Record_Type).FindControl(Ctl_Record_Type), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_RECORD_TYPE).ToString
                ctlTxt = CType(e.Row.Cells(COL_Reject_Code).FindControl(Ctl_Reject_Code), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_REJECT_CODE).ToString
                ctlTxt = CType(e.Row.Cells(COL_Reject_Reason).FindControl(Ctl_Reject_Reason), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_REJECT_REASON).ToString

                ctlTxt = CType(e.Row.Cells(COL_Company_Code).FindControl(Ctl_Company_Code), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_COMPANY_CODE).ToString

                ctlTxt = CType(e.Row.Cells(COL_Invoice_Number).FindControl(Ctl_Invoice_Number), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_INVOICE_NUMBER).ToString

                ctlTxt = CType(e.Row.Cells(COL_Invoice_Date).FindControl(Ctl_Invoice_Date), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_INVOICE_DATE).ToString
                ctlCanlendar = CType(e.Row.Cells(COL_Invoice_Date).FindControl(Ctl_Invoice_Date_Calendar), ImageButton)
                If (Not ctlCanlendar Is Nothing) Then
                    Me.AddCalendar(ctlCanlendar, ctlTxt)
                End If

                ctlTxt = CType(e.Row.Cells(COL_Repair_Date).FindControl(Ctl_Repair_Date), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_REPAIR_DATE).ToString
                ctlCanlendar = CType(e.Row.Cells(COL_Repair_Date).FindControl(Ctl_Repair_Date_Calendar), ImageButton)
                If (Not ctlCanlendar Is Nothing) Then
                    Me.AddCalendar(ctlCanlendar, ctlTxt)
                End If

                ctlTxt = CType(e.Row.Cells(COL_Due_Date).FindControl(Ctl_Due_Date), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_DUE_DATE).ToString
                ctlCanlendar = CType(e.Row.Cells(COL_Due_Date).FindControl(Ctl_Due_Date_Calendar), ImageButton)
                If (Not ctlCanlendar Is Nothing) Then
                    Me.AddCalendar(ctlCanlendar, ctlTxt)
                End If

                ctlTxt = CType(e.Row.Cells(COL_Attributes).FindControl(Ctl_Attributes), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_ATTRIBUTES).ToString

                ctlTxt = CType(e.Row.Cells(COL_Service_Center).FindControl(Ctl_Service_Center), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_SERVICE_CENTER_CODE).ToString

                ctlTxt = CType(e.Row.Cells(COL_Claim_Number).FindControl(Ctl_Claim_Number), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_CLAIM_NUMBER).ToString

                ctlTxt = CType(e.Row.Cells(COL_Authorization_Number).FindControl(Ctl_Authorization_Number), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_AUTHORIZATION_NUMBER).ToString

                ctlTxt = CType(e.Row.Cells(COL_Line_Item_Number).FindControl(Ctl_Line_Item_Number), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_LINE_ITEM_NUMBER).ToString

                ctlTxt = CType(e.Row.Cells(COL_Vendor_SKU).FindControl(Ctl_SKU), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_vendor_SKU).ToString

                ctlTxt = CType(e.Row.Cells(COL_Vendor_SKU_DESCRIPTION).FindControl(Ctl_SKU_Description), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_vendor_SKU_DESCRIPTION).ToString

                ctlTxt = CType(e.Row.Cells(COL_SERVICE_LEVEL).FindControl(Ctl_Service_Level), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_SERVICE_LEVEL).ToString
                ctlTxt = CType(e.Row.Cells(COL_Amount).FindControl(Ctl_Amount), TextBox)
                ctlTxt.Attributes.Add("onchange", "setDirty()")
                ctlTxt.Text = dvRow(InvoiceReconWrk.COL_NAME_AMOUNT).ToString

            End If
        Catch ex As Exception
            HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.PageSize = CType(cboPageSize.SelectedValue, Integer)
            If IsDataGPageDirty() Then
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.GridPageSize
                DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
            Else
                Me.State.PageIndex = NewCurrentPageIndex(Grid, State.RecordCount, State.PageSize)
                Me.PopulateGrid()
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub



#End Region


#Region "button event handlers"
    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            If IsDataGPageDirty() Then
                DisplayMessage(Message.MSG_PAGE_SAVE_PROMPT, "", MSG_BTN_YES_NO, MSG_TYPE_CONFIRM, Me.HiddenSavePagePromptResponse)
                Me.State.ActionInProgress = ElitaPlusPage.DetailPageCommand.Back
            Else
                Dim retType As New ClaimLoadForm.ReturnType(ElitaPlusPage.DetailPageCommand.Back, State.ClaimloadfileID)
                Me.ReturnToCallingPage(retType)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnUndo_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUndo_WRITE.Click
        Try
            PopulateGrid()
            Me.HiddenIsPageDirty.Value = String.Empty
        Catch ex As Exception
            Me.HandleErrors(ex, ErrControllerMaster)
        End Try
    End Sub

    Private Sub SaveButton_WRITE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SaveButton_WRITE.Click
        Try
            SavePage()
            Me.DisplayMessage(Message.SAVE_RECORD_CONFIRMATION, "", Me.MSG_BTN_OK, Me.MSG_TYPE_INFO)
            Me.HiddenIsPageDirty.Value = String.Empty
            PopulateGrid()
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub


#End Region






End Class