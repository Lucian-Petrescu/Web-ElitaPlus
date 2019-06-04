Imports Assurant.elitaplus.Common
Imports System.Data
Imports microsoft.VisualBasic

Partial Public Class AccountingLoadForm
    Inherits ElitaPlusPage

#Region "Constants"

    Public Const URL As String = "Interfaces/AccountingLoadForm.aspx"
    Public Const PAGETITLE As String = "ACCOUNTING_LOAD"
    Public Const PAGETAB As String = "ADMIN"

    Private Const YESNO As String = "YESNO"
    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const YES_STRING As String = "Y"
    Private Const NO_STRING As String = "N"
    Private Const EXCLUDE_ROW As String = "USE_ROW"
    Private Const EXCLUDE_ROW_VALUE As String = "X"
    Private Const ONE_ITEM As Integer = 1
    Private Const RETURN_SUCCESS As String = "0"
    Private Const DEALER_STRING As String = "Dealer"
    Private Const SERVICE_CENTER_STRING As String = "Service Center"
    Private Const BRANCH_STRING As String = "Branch"
    Private Const DEALER_GROUP_STRING As String = "Dealer Group"
    Private Const COMMISSION_ENTITY_STRING As String = "Commission Entity"

    Private Const PMT_TERMS As String = "PAYMENT_TERMS"

    'Repeater Controls
    Private Const RPT_DDL As String = "ddlEventDetailYesNo"
    Private Const RPT_DDLSHEETS As String = "ddlWorksheet"
    Private Const RPT_LBL As String = "lblEventDetail"
    Private Const RPT_LBLID As String = "lblEventDetailID_NOTRANSLATE"
    Private Const RPT_LBLWORKSHEET As String = "lblWorksheet"


    'Field Mapping
    Private Const COL_FIRST_CELL As String = "F1"
    Private Const COL_TYPE_CELL As String = "F5"
    Private Const COL_BUSINESS_UNIT As String = "BUSINESS_UNIT"
    Private Const COL_CODE As String = "CODE"
    Private Const COL_BRANCH_CODE As String = "BRANCH_CODE"
    Private Const COL_TYPE As String = "TYPE"
    Private Const COL_ACCT_TYPE As String = "ACCOUNT_TYPE"
    Private Const COL_BANK_ACCOUNT As String = "ACCOUNT_NUMBER"

    'Event Type Data Mapping
    Private Const EVENT_UPR As String = "UPR"
    Private Const EVENT_IBNR As String = "IBNR"
    Private Const EVENT_PREM As String = "PREM"
    Private Const EVENT_REFUNDS As String = "REFUNDS"
    Private Const EVENT_CLAIM As String = "CLAIM"
    Private Const EVENT_CLAIMRES As String = "CLAIMRES"
    Private Const EVENT_INVOICING As String = "INV"
    Private Const EVENT_SHEET_UPR As String = "UPR"
    Private Const EVENT_SHEET_IBNR As String = "IBNR"
    Private Const EVENT_SHEET_PREM As String = "Premiums"
    Private Const EVENT_SHEET_REFUNDS As String = "Premium Disbursements"
    Private Const EVENT_SHEET_CLAIM As String = "Claim Payments"
    Private Const EVENT_SHEET_CLAIMRES As String = "Claims Reserve"
    Private Const EVENT_SHEET_INVOICING As String = "Invoicing"
    Private EventMap As Collections.Specialized.StringDictionary

    Private AcctEventDetailFielddv As DataView

    Protected WithEvents moUserCompanyMultipleDrop As Common.MultipleColumnDDLabelControl

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

#Region "PAGE STATE"

    Class MyState
        Public dvYesNo As DataView
        Public WorksheetTables As ArrayList
        Public FileLocation As String
        Public ConnectionString As String
        Public AccountingCompanyId As Guid
        Public SelectedCompany As Company
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

#Region "Page Init"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'Put user code to initialize the page here
        Me.ErrControllerMaster.Clear_Hide()

        Try
            Me.DisplayProgressBarOnClick(Me.btnExecute, ElitaPlusWebApp.Message.MSG_PERFORMING_REQUEST)

            If Not Me.IsPostBack Then
                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)

                PopulateAll()

            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Sub

#End Region

#Region "Populate"

    Private Sub PopulateAll()

        Try

            'Fill Companies
            Dim dv As DataView = LookupListNew.GetUserCompaniesLookupList()
            UserCompanyMultipleDrop.NothingSelected = True
            UserCompanyMultipleDrop.SetControl(True, UserCompanyMultipleDrop.MODES.NEW_MODE, True, dv, UserCompanyMultipleDrop.NO_CAPTION, True)
            If dv.Count.Equals(ONE_ITEM) Then
                UserCompanyMultipleDrop.SelectedIndex = Me.ONE_ITEM
                UserCompanyMultipleDrop.ChangeEnabledControlProperty(False)
            End If

            'Fill dropdowns based on Events and Accounting Companies
            Me.State.dvYesNo = LookupListNew.DropdownLookupList(YESNO, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False)

            Me.ddlVendorFiles.DataSource = Me.State.dvYesNo
            Me.ddlVendorFiles.DataValueField = BusinessObjectsNew.LookupListNew.COL_CODE_NAME
            Me.ddlVendorFiles.DataTextField = BusinessObjectsNew.LookupListNew.COL_DESCRIPTION_NAME
            Me.ddlVendorFiles.DataBind()

            Me.ddlAccountingEvents.DataSource = Me.State.dvYesNo
            Me.ddlAccountingEvents.DataValueField = BusinessObjectsNew.LookupListNew.COL_CODE_NAME
            Me.ddlAccountingEvents.DataTextField = BusinessObjectsNew.LookupListNew.COL_DESCRIPTION_NAME
            Me.ddlAccountingEvents.DataBind()

            PopulateEvents()

        Catch ex As Exception
            Me.ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
        End Try


    End Sub

    Private Sub PopulateEvents()

        Try
            Me.rptEvents.DataSource = BusinessObjectsNew.LookupListNew.DropdownLookupList(BusinessObjectsNew.LookupListNew.LK_ACCT_TRANS_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            Me.rptEvents.DataBind()

        Catch ex As Exception
            Me.ErrControllerMaster.AddErrorAndShow(ex.Message)
        End Try
    End Sub

    Private Sub rptEvents_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptEvents.ItemDataBound

        Dim lbl, lblID, lblWorksheet As Label
        Dim ddl, ddlSheets As DropDownList
        Dim ctl As Control
        Dim dr As DataRow

        Try

            If e.Item.ItemType = ListItemType.AlternatingItem Or e.Item.ItemType = ListItemType.Item Then

                dr = CType(e.Item.DataItem, System.Data.DataRowView).Row

                ctl = e.Item.FindControl(RPT_LBL)
                If Not ctl Is Nothing Then
                    lbl = CType(ctl, Label)
                    lbl.Text = dr(BusinessObjectsNew.LookupListNew.COL_DESCRIPTION_NAME).ToString + ":"
                End If

                ctl = e.Item.FindControl(RPT_LBLID)
                If Not ctl Is Nothing Then
                    lblID = CType(ctl, Label)
                    lblID.Text = GuidControl.ByteArrayToGuid(dr(BusinessObjectsNew.LookupListNew.COL_ID_NAME)).ToString
                End If

                ctl = e.Item.FindControl(RPT_DDL)
                If Not ctl Is Nothing Then
                    ddl = CType(ctl, DropDownList)
                    ddl.DataSource = Me.State.dvYesNo
                    ddl.DataValueField = BusinessObjectsNew.LookupListNew.COL_CODE_NAME
                    ddl.DataTextField = BusinessObjectsNew.LookupListNew.COL_DESCRIPTION_NAME
                    ddl.DataBind()
                End If

                ctl = e.Item.FindControl(RPT_LBLWORKSHEET)
                If Not ctl Is Nothing Then
                    lblWorksheet = CType(ctl, Label)
                    lblWorksheet.Text = TranslationBase.TranslateLabelOrMessage(lblWorksheet.Text)
                End If
              
                If Not Me.State.WorksheetTables Is Nothing AndAlso Me.State.WorksheetTables.Count > 0 Then

                    ctl = e.Item.FindControl(RPT_DDLSHEETS)
                    If Not ctl Is Nothing Then ddlSheets = CType(ctl, DropDownList)

                    PopulateWorksheets(ddlSheets)
                    ddl.Enabled = True
                End If

            End If

        Catch ex As Exception
            Me.ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.DB_ERROR)
        End Try
    End Sub

    Private Sub PopulateWorksheets(ByVal ctl As DropDownList)

        ctl.DataSource = Me.State.WorksheetTables
        ctl.DataBind()

        ctl.Items.Add(New ListItem("", Me.NOTHING_SELECTED))
        ctl.SelectedValue = Me.NOTHING_SELECTED
        ctl.Enabled = True

    End Sub

#End Region

#Region "EVENT HANDLERS"

    Private Sub btnValidate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnValidate.Click

        UploadFile()

    End Sub


    Private Sub btnExecute_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExecute.Click

        Try
            'Set AccountingCompanyId
            Dim _co As New Company(Me.moUserCompanyMultipleDrop.SelectedGuid)
            Me.State.SelectedCompany = _co
            Me.State.AccountingCompanyId = _co.AcctCompanyId

            'Validate selections
            If Not ValidateWorksheetSelections() Then Exit Sub

            'Load Sheets
            If LoadWorksheets() Then
                Me.AddInfoMsg(Message.MSG_INTERFACES_HAS_COMPLETED)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
      
    End Sub

    Private Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.ReturnToTabHomePage()
    End Sub

#End Region

#Region "Controlling Logic"

    Private Sub UploadFile()

        Try

            'Dim str As String = MiscUtil.ReplaceSpaceByUnderscore(Me.filinput.PostedFile.FileName)
            Dim str As String = Me.filinput.PostedFile.FileName
            If Str.Trim.Length = 0 Then
                Me.ErrControllerMaster.AddErrorAndShow(Message.MSG_INVALID_FILE_NAME)
                Exit Sub
            End If

            Dim webServerPath As String = MiscUtil.GetUniqueDirectory(AppConfig.UnixServer.InterfaceDirectory, ElitaPlusPrincipal.Current.Identity.Name)
            Dim webServerFile As String = webServerPath & "\" & System.IO.Path.GetFileName(str)

            MiscUtil.CreateFolder(webServerPath)
            Me.filinput.PostedFile.SaveAs(webServerFile)

            If ValidateFile(webServerFile) Then
                Me.btnExecute.Enabled = True
                Me.State.FileLocation = webServerFile
            End If

        Catch ex As Exception
            Me.ErrControllerMaster.AddErrorAndShow(ex.Message)
        End Try

    End Sub

    Private Function ValidateFile(ByVal FileName As String) As Boolean

        Dim olconn As New OleDb.OleDbConnection
        Dim olAd As New OleDb.OleDbDataAdapter
        Dim dt As New DataTable

        Me.State.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & FileName & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"""
        olconn.ConnectionString = Me.State.ConnectionString

        Try
            olconn.Open()
            Me.State.WorksheetTables = New ArrayList
            dt = olconn.GetSchema("TABLES")
            For Each dr As DataRow In dt.Rows
                If dr("TABLE_NAME").ToString.EndsWith("$") Then
                    Me.State.WorksheetTables.Add(dr("TABLE_NAME").ToString.Remove(dr("TABLE_NAME").ToString.LastIndexOf(CChar("$"))))
                End If
            Next

            PopulateEvents()

            If Me.State.WorksheetTables.Count > 0 Then

                Me.ddlAccountingEvents.Enabled = True
                Me.ddlVendorFiles.Enabled = True
                PopulateWorksheets(ddlWorksheetVendor)
                PopulateWorksheets(ddlWorksheetEvents)

                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        Finally
            If olconn.State <> ConnectionState.Closed Then olconn.Close()
            olconn.Dispose()
        End Try
    End Function

    Private Function ValidateWorksheetSelections() As Boolean

        Dim isValid As Boolean = True
        Dim arrErrors As New ArrayList

        Try

            If Me.ddlAccountingEvents.SelectedValue = YES_STRING Then
                If Me.ddlWorksheetEvents.SelectedValue = Me.NOTHING_SELECTED Then
                    isValid = False
                    arrErrors.Add(TranslationBase.TranslateLabelOrMessage(Message.MSG_WORKSHEET_REQUIRED) & ": " & Me.lblAccountingEvent.Text.Remove(Me.lblAccountingEvent.Text.LastIndexOf(":")))
                End If
            End If

            If Me.ddlVendorFiles.SelectedValue = YES_STRING Then
                If Me.ddlWorksheetVendor.SelectedValue = Me.NOTHING_SELECTED Then
                    isValid = False
                    arrErrors.Add(TranslationBase.TranslateLabelOrMessage(Message.MSG_WORKSHEET_REQUIRED) & ": " & Me.lblVendorFiles.Text.Remove(Me.lblVendorFiles.Text.LastIndexOf(":")))
                End If
            End If

            Dim rptItem As RepeaterItem
            Dim ddl, ddlSheets As DropDownList
            Dim lbl As Label
            Dim ctl As Control

            For Each rptItem In Me.rptEvents.Items

                ctl = rptItem.FindControl(Me.RPT_DDL)
                If Not ctl Is Nothing Then
                    ddl = CType(ctl, DropDownList)
                    If ddl.SelectedValue = YES_STRING Then
                        ctl = rptItem.FindControl(Me.RPT_DDLSHEETS)
                        If Not ctl Is Nothing Then
                            ddlSheets = CType(ctl, DropDownList)
                            If ddlSheets.SelectedValue = Me.NOTHING_SELECTED Then
                                isValid = False
                                ctl = rptItem.FindControl(Me.RPT_LBL)
                                If Not ctl Is Nothing Then
                                    lbl = CType(ctl, Label)
                                    arrErrors.Add(TranslationBase.TranslateLabelOrMessage(Message.MSG_WORKSHEET_REQUIRED) & ": " & lbl.Text.Remove(lbl.Text.LastIndexOf(":")))
                                End If
                            End If
                        End If
                    End If
                End If
            Next

            If isValid Then
                Return True
            Else
                Me.ErrControllerMaster.AddErrorAndShow(CType(arrErrors.ToArray(GetType(String)), String()), False)
                Return False
            End If

        Catch ex As Exception
            Me.ErrControllerMaster.AddErrorAndShow(ex.Message)
            Return False
        End Try
    End Function

    Private Function LoadWorksheets() As Boolean

        Dim oleConn As New OleDb.OleDbConnection

        Try
            oleConn.ConnectionString = Me.State.ConnectionString
            oleConn.Open()

            If Me.ddlVendorFiles.SelectedValue = YES_STRING Then
                LoadVendors(oleConn, Me.ddlWorksheetVendor.SelectedItem.Text + "$")
            End If

            If Me.ddlAccountingEvents.SelectedValue = YES_STRING Then
                LoadEvents(oleConn, Me.ddlWorksheetEvents.SelectedItem.Text + "$")
            End If

            Dim rptItem As RepeaterItem
            Dim ddl, ddlSheets As DropDownList
            Dim lblId As Label
            Dim ctl As Control

            For Each rptItem In Me.rptEvents.Items

                ctl = rptItem.FindControl(Me.RPT_DDL)
                If Not ctl Is Nothing Then
                    ddl = CType(ctl, DropDownList)
                    If ddl.SelectedValue = YES_STRING Then
                        ctl = rptItem.FindControl(Me.RPT_DDLSHEETS)
                        If Not ctl Is Nothing Then
                            ddlSheets = CType(ctl, DropDownList)
                            ctl = rptItem.FindControl(Me.RPT_LBLID)
                            If Not ctl Is Nothing Then
                                lblId = CType(ctl, Label)
                                LoadEventDetails(oleConn, ddlSheets.SelectedItem.Text + "$", New Guid(lblId.Text))
                            End If
                        End If
                    End If
                End If
            Next

            Return True
        Catch ex As Exception
            Throw ex
        Finally
            If oleConn.State <> ConnectionState.Closed Then oleConn.Close()
            oleConn.Dispose()
        End Try

    End Function

    Private Function LoadVendors(ByVal oleConn As OleDb.OleDbConnection, ByVal SheetName As String) As Boolean

        Dim strSQL As String = "Select * from [" & SheetName & "]"
        Dim olAd As New OleDb.OleDbDataAdapter
        Dim cmd As New OleDb.OleDbCommand
        Dim dt As New DataTable
        Dim _as As AcctSetting
        Dim _bi As BankInfo
        Dim ValidRow As Boolean
        Dim iRowCount As Integer = 0
        Dim vParent As Object
        Dim boolSaveParent As Boolean
        Dim colValue As String = ""

        Try
            cmd.CommandText = strSQL
            cmd.Connection = oleConn
            olAd.SelectCommand = cmd
            olAd.Fill(dt)

            iRowCount = ClearColumnsAndRows(dt)

            If dt.Columns.Contains(DALObjects.ServiceCenterDAL.COL_NAME_SERVICE_CENTER_ID) Then
                dt.Columns(DALObjects.ServiceCenterDAL.COL_NAME_SERVICE_CENTER_ID).ColumnName = Me.COL_CODE
            End If

            If dt.Columns.Contains(Me.COL_TYPE_CELL) Then
                dt.Columns(Me.COL_TYPE_CELL).ColumnName = Me.COL_BRANCH_CODE
            End If

            For Each dr As DataRow In dt.Rows

                'Reset bank info object
                _bi = Nothing

                If Not IsDBNull(dr(COL_CODE)) AndAlso dr(COL_CODE).ToString.Trim.Length > 0 Then

                    ValidRow = True

                    Select Case dr(COL_TYPE).ToString

                        Case Me.BRANCH_STRING
                            _as = New AcctSetting(Me.State.AccountingCompanyId, dr(COL_CODE).ToString, dr(Me.COL_BRANCH_CODE).ToString, dr(Me.COL_ACCT_TYPE).ToString)

                        Case Me.DEALER_GROUP_STRING
                            _as = New AcctSetting(Me.State.AccountingCompanyId, dr(COL_CODE).ToString, _as.VendorType.DealerGroup, dr(Me.COL_ACCT_TYPE).ToString)

                        Case Me.DEALER_STRING
                            _as = New AcctSetting(Me.State.AccountingCompanyId, dr(COL_CODE).ToString, _as.VendorType.Dealer, dr(Me.COL_ACCT_TYPE).ToString)

                        Case Me.SERVICE_CENTER_STRING
                            _as = New AcctSetting(Me.State.AccountingCompanyId, dr(COL_CODE).ToString, _as.VendorType.ServiceCenter, dr(Me.COL_ACCT_TYPE).ToString)

                        Case Me.COMMISSION_ENTITY_STRING
                            _as = New AcctSetting(Me.State.AccountingCompanyId, dr(COL_CODE).ToString, _as.VendorType.CommissionEntity, dr(Me.COL_ACCT_TYPE).ToString)

                        Case Else
                            ValidRow = False

                    End Select

                    If _as.IsNew Then

                        _as.AcctCompanyId = Me.State.AccountingCompanyId

                        Select Case dr(COL_TYPE).ToString

                            Case Me.DEALER_STRING, Me.BRANCH_STRING

                                Dim dealerdv As Dealer.DealerSearchDV
                                dealerdv = Dealer.getList(String.Empty, dr(Me.COL_CODE).ToString, Nothing, Me.State.SelectedCompany.CompanyGroupId)
                                If Not dealerdv Is Nothing AndAlso dealerdv.Count = 1 Then
                                    _as.DealerId = GuidControl.ByteArrayToGuid(CType(dealerdv(0)(dealerdv.COL_DEALER_ID), Byte()))

                                    If dr(COL_TYPE).ToString = Me.BRANCH_STRING Then
                                        Dim branchdv As Branch.BranchSearchDV
                                        branchdv = Branch.getList(String.Empty, dr(Me.COL_BRANCH_CODE).ToString, _as.DealerId)
                                        If Not branchdv Is Nothing AndAlso branchdv.Count = 1 Then
                                            _as.BranchId = GuidControl.ByteArrayToGuid(CType(branchdv(0)(branchdv.COL_BRANCH_ID), Byte()))
                                        Else
                                            ValidRow = False
                                        End If
                                    End If

                                Else
                                    ValidRow = False
                                End If

                            Case Me.SERVICE_CENTER_STRING

                                Dim svcCenterdv As ServiceCenter.ServiceCenterSearchDV
                                svcCenterdv = ServiceCenter.getList(dr(COL_CODE).ToString, String.Empty, String.Empty, String.Empty, String.Empty, Me.State.SelectedCompany.CountryId)
                                If Not svcCenterdv Is Nothing AndAlso svcCenterdv.Count = 1 Then
                                    _as.ServiceCenterId = GuidControl.ByteArrayToGuid(CType(svcCenterdv(0)(svcCenterdv.COL_SERVICE_CENTER_ID), Byte()))
                                Else
                                    ValidRow = False
                                End If

                            Case Me.DEALER_GROUP_STRING

                                Dim dGroupdv As New DealerGroup.DealerGroupSearchDV(DealerGroup.LoadList(String.Empty, dr(COL_CODE).ToString).Table)

                                If Not dGroupdv Is Nothing AndAlso dGroupdv.Count = 1 Then
                                    _as.DealerGroupId = GuidControl.ByteArrayToGuid(CType(dGroupdv(0)(dGroupdv.COL_DEALER_GROUP_ID), Byte()))
                                Else
                                    ValidRow = False
                                End If

                            Case Me.COMMISSION_ENTITY_STRING

                                Dim commEntdv As New CommissionEntity.CommissionEntitySearchDV(CommissionEntity.getList(dr(COL_CODE).ToString, Nothing, Me.State.SelectedCompany.CompanyGroupId).Table)

                                If Not commEntdv Is Nothing AndAlso commEntdv.Count = 1 Then
                                    _as.CommissionEntityId = GuidControl.ByteArrayToGuid(CType(commEntdv(0)(commEntdv.COL_COMMISSION_ENTITY_ID), Byte()))
                                Else
                                    ValidRow = False
                                End If

                            Case Else
                                ValidRow = False
                        End Select

                    End If

                    If ValidRow Then

                        'Bank info adding..
                        If dr.Table.Columns(COL_BANK_ACCOUNT) IsNot Nothing AndAlso dr(COL_BANK_ACCOUNT) IsNot Nothing AndAlso dr(COL_BANK_ACCOUNT).ToString.Trim.Length > 0 Then

                            Select Case dr(COL_TYPE).ToString
                                Case Me.DEALER_STRING
                                    vParent = New Dealer(_as.DealerId)
                                    If Not CType(vParent, Dealer).BankInfoId.Equals(Guid.Empty) Then
                                        _bi = New BankInfo(CType(vParent, Dealer).BankInfoId)
                                    Else
                                        _bi = New BankInfo
                                        _bi.CountryID = Me.State.SelectedCompany.CountryId
                                        _bi.SourceCountryID = Me.State.SelectedCompany.CountryId
                                    End If
                                Case Me.BRANCH_STRING
                                    vParent = New Branch(_as.BranchId)
                                    If Not CType(vParent, Branch).BankInfoId.Equals(Guid.Empty) Then
                                        _bi = New BankInfo(CType(vParent, Branch).BankInfoId)
                                    Else
                                        _bi = New BankInfo
                                        _bi.CountryID = Me.State.SelectedCompany.CountryId
                                        _bi.SourceCountryID = Me.State.SelectedCompany.CountryId
                                    End If
                                Case Me.SERVICE_CENTER_STRING
                                    vParent = New ServiceCenter(_as.ServiceCenterId)
                                    If Not CType(vParent, ServiceCenter).BankInfoId.Equals(Guid.Empty) Then
                                        _bi = New BankInfo(CType(vParent, ServiceCenter).BankInfoId)
                                    Else
                                        _bi = New BankInfo
                                        _bi.CountryID = Me.State.SelectedCompany.CountryId
                                        _bi.SourceCountryID = Me.State.SelectedCompany.CountryId
                                    End If
                                Case Me.DEALER_GROUP_STRING
                                    vParent = New DealerGroup(_as.DealerGroupId)
                                    If Not CType(vParent, DealerGroup).BankInfoId.Equals(Guid.Empty) Then
                                        _bi = New BankInfo(CType(vParent, DealerGroup).BankInfoId)
                                    Else
                                        _bi = New BankInfo
                                        _bi.CountryID = Me.State.SelectedCompany.CountryId
                                        _bi.SourceCountryID = Me.State.SelectedCompany.CountryId
                                    End If
                                Case Me.COMMISSION_ENTITY_STRING
                                    vParent = New CommissionEntity(_as.CommissionEntityId)
                                    If Not CType(vParent, CommissionEntity).BankInfoId.Equals(Guid.Empty) Then
                                        _bi = New BankInfo(CType(vParent, CommissionEntity).BankInfoId)
                                    Else
                                        _bi = New BankInfo
                                        _bi.CountryID = Me.State.SelectedCompany.CountryId
                                        _bi.SourceCountryID = Me.State.SelectedCompany.CountryId
                                    End If
                            End Select

                        End If

                        For Each dc As DataColumn In dt.Columns
                            If dc.ColumnName = "XXXX" Then Exit For
                            If Not dc.ColumnName = DALObjects.ServiceCenterDAL.COL_NAME_SERVICE_CENTER_ID Then


                                If IsNumeric(dr(dc.ColumnName)) AndAlso dr(dc.ColumnName).ToString.Contains("+") Then
                                    colValue = CStr(CType(dr(dc.ColumnName), Double))
                                Else
                                    colValue = dr(dc.ColumnName).ToString
                                End If

                                If Not _as.SetPropertyByColumnName(dc.ColumnName, colValue) Then
                                    Select Case dc.ColumnName
                                        Case PMT_TERMS
                                            _as.SetPropertyByColumnName(DALObjects.AcctSettingDAL.COL_NAME_PAYMENT_TERMS_ID, LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_PAYMENT_TERMS, ElitaPlusIdentity.Current.ActiveUser.LanguageId, False), colValue))
                                        Case Else
                                            'Check if these are bankinfo columns
                                            If _bi IsNot Nothing Then
                                                _bi.SetPropertyByColumnName(dc.ColumnName, colValue)
                                            End If
                                    End Select
                                End If
                            End If
                        Next

                        Try
                            _as.Save()
                            If _bi IsNot Nothing Then
                                'if the BI record is new, then set flag to save parent record before exit
                                If _bi.IsNew Then
                                    boolSaveParent = True
                                End If
                                'Set the domestic transfer information
                                If (_bi.SwiftCode IsNot Nothing AndAlso _bi.SwiftCode.Length > 0) Or _
                                   (_bi.IbanNumber IsNot Nothing AndAlso _bi.IbanNumber.Length > 0) Then
                                    _bi.DomesticTransfer = False
                                Else
                                    _bi.DomesticTransfer = True
                                End If
                                _bi.Save()
                                If boolSaveParent Then
                                    Select Case dr(COL_TYPE).ToString
                                        Case Me.DEALER_STRING
                                            CType(vParent, Dealer).BankInfoId = _bi.Id
                                            CType(vParent, Dealer).Save()
                                        Case Me.DEALER_GROUP_STRING
                                            CType(vParent, DealerGroup).BankInfoId = _bi.Id
                                            CType(vParent, DealerGroup).Save()
                                        Case Me.BRANCH_STRING
                                            CType(vParent, Branch).BankInfoId = _bi.Id
                                            CType(vParent, Branch).Save()
                                        Case Me.COMMISSION_ENTITY_STRING
                                            CType(vParent, CommissionEntity).BankInfoId = _bi.Id
                                            CType(vParent, CommissionEntity).Save()
                                        Case Me.SERVICE_CENTER_STRING
                                            CType(vParent, ServiceCenter).BankInfoId = _bi.Id
                                            CType(vParent, ServiceCenter).Save()
                                    End Select
                                End If

                            End If
                        Catch ex As BOValidationException

                            Dim valList(ex.ValidationErrorList.Length - 1) As ValidationError
                            For i As Integer = 0 To ex.ValidationErrorList.Length - 1
                                valList(i) = New ValidationError(SheetName.Replace("$", "") & ", Line " & iRowCount.ToString & ", " & ex.ValidationErrorList(i).PropertyName & ": |" & ex.ValidationErrorList(i).Message, ex.ValidationErrorList(i).BusinessObjectType, ex.ValidationErrorList(i).ValidationAttributeType, ex.ValidationErrorList(i).PropertyName, ex.ValidationErrorList(i).OffendingValue)
                            Next
                            Me.HandleErrors(New BOValidationException(valList, ex.BusinessObjectName), Me.ErrControllerMaster)
                        End Try

                    End If

                Else
                    Exit For
                End If

                iRowCount += 1
            Next


        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Function

    Private Function LoadEvents(ByVal oleConn As OleDb.OleDbConnection, ByVal SheetName As String) As Boolean

        Dim strSQL As String = "Select * from [" & SheetName & "]"
        Dim olAd As New OleDb.OleDbDataAdapter
        Dim cmd As New OleDb.OleDbCommand
        Dim dt As New DataTable
        Dim _ae As AcctEvent
        Dim aedv As AcctEvent.AcctEventSearchDV
        Dim strEventType As String
        Dim eventTypeId, businessUnitId As Guid
        Dim iRowCount As Integer = 0

        Try
            cmd.CommandText = strSQL
            cmd.Connection = oleConn
            olAd.SelectCommand = cmd
            olAd.Fill(dt)

            iRowCount = ClearColumnsAndRows(dt)

            For Each dr As DataRow In dt.Rows

                If Not IsDBNull(dr(COL_FIRST_CELL)) AndAlso dr(COL_FIRST_CELL).ToString.Trim.Length > 0 Then

                    eventTypeId = LookupListNew.GetIdFromCode(LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_TRANS_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId), GetEvent(dr(COL_FIRST_CELL).ToString))

                    'Check if the event exists for the accounting company/business unit combination.  If it exists, load the existing record for update.  If 
                    '   it does not exist, create a new record.
                    aedv = AcctEvent.getList(eventTypeId, Me.State.AccountingCompanyId)
                    If Not aedv Is Nothing AndAlso aedv.Count = 1 Then
                        _ae = New AcctEvent(GuidControl.ByteArrayToGuid(CType(aedv(0)(aedv.COL_EVENT_ID), Byte())))
                    Else
                        _ae = New AcctEvent
                        'Set AccountingEventId
                        _ae.AcctEventTypeId = eventTypeId

                        'Set Accounting Company
                        _ae.AcctCompanyId = Me.State.AccountingCompanyId

                    End If

                    For Each dc As DataColumn In dt.Columns
                        _ae.SetPropertyByColumnName(dc.ColumnName, dr(dc.ColumnName))
                    Next

                    Try
                        _ae.Save()
                    Catch ex As BOValidationException
                        Dim valList(ex.ValidationErrorList.Length - 1) As ValidationError
                        For i As Integer = 0 To ex.ValidationErrorList.Length - 1
                            valList(i) = New ValidationError(SheetName.Replace("$", "") & ", Line " & iRowCount.ToString & ", " & ex.ValidationErrorList(i).PropertyName & ": |" & ex.ValidationErrorList(i).Message, ex.ValidationErrorList(i).BusinessObjectType, ex.ValidationErrorList(i).ValidationAttributeType, ex.ValidationErrorList(i).PropertyName, ex.ValidationErrorList(i).OffendingValue)
                        Next
                        Me.HandleErrors(New BOValidationException(valList, ex.BusinessObjectName), Me.ErrControllerMaster)
                    End Try

                Else
                    Exit For
                End If

                iRowCount += 1
            Next

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Function

    Private Function LoadEventDetails(ByVal oleConn As OleDb.OleDbConnection, ByVal SheetName As String, ByVal EventTypeId As Guid) As Boolean

        Dim strSQL As String = "Select * from [" & SheetName & "]"
        Dim olAd As New OleDb.OleDbDataAdapter
        Dim cmd As New OleDb.OleDbCommand
        Dim dt As New DataTable
        Dim _aed As AcctEventDetail
        Dim _ae As AcctEvent
        Dim aeDetaildv As AcctEventDetail.AcctEventDetailSearchDV
        Dim aedv As AcctEvent.AcctEventSearchDV
        Dim eventId, businessUnitId As Guid
        Dim currFilter As String
        Dim iRowCount As Integer = 0
        Dim AcctTCodeSourcedv, AcctDescSourcedv As DataView
        Dim curFieldValue As String = ""

        Try
            cmd.CommandText = strSQL
            cmd.Connection = oleConn
            olAd.SelectCommand = cmd
            olAd.Fill(dt)

            iRowCount = ClearColumnsAndRows(dt)

            If AcctEventDetailFielddv Is Nothing Then
                AcctEventDetailFielddv = LookupListNew.DropdownLookupList(LookupListNew.LK_ACCT_FIELD_TYPE, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            End If
            currFilter = AcctEventDetailFielddv.RowFilter

            AcctTCodeSourcedv = LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_ANALYSIS_SOURCE, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
            AcctDescSourcedv = LookupListNew.DropdownLookupList(LookupListNew.LK_ACCOUNTING_DESC_SOURCE, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            For Each dr As DataRow In dt.Rows

                If Not IsDBNull(dr(COL_BUSINESS_UNIT)) AndAlso dr(COL_BUSINESS_UNIT).ToString.Trim.Length > 0 Then

                    businessUnitId = GetBusinessUnit(dr(COL_BUSINESS_UNIT).ToString.Trim)

                    'Check if the event exists for the accounting company/business unit combination.  If it exists, load the existing record for update.  If 
                    '   it does not exist, throw an error as this is our parent key
                    aedv = AcctEvent.getList(EventTypeId, Me.State.AccountingCompanyId)
                    If Not aedv Is Nothing AndAlso aedv.Count = 1 Then
                        _ae = New AcctEvent(GuidControl.ByteArrayToGuid(CType(aedv(0)(aedv.COL_EVENT_ID), Byte())))
                    Else
                        Me.ErrControllerMaster.AddErrorAndShow(Message.GUI_MSG_NO_EVENT_EXISTS)
                        Return False
                    End If

                    _aed = New AcctEventDetail
                    _aed.AcctEventId = _ae.Id
                    _aed.AcctBusinessUnitId = businessUnitId

                    For Each dc As DataColumn In dt.Columns
                        If dc.ColumnName.ToLower = DALObjects.AcctEventDetailDAL.COL_NAME_FIELD_TYPE_ID.ToLower Then
                            AcctEventDetailFielddv.RowFilter = currFilter + IIf(currFilter.Trim.Length > 0, " and ", "").ToString + _
                              LookupListNew.COL_CODE_NAME + " = '" + dr(dc.ColumnName).ToString.Replace(" ", "_").ToUpper + "'"
                            If AcctEventDetailFielddv.Count > 0 Then
                                _aed.SetPropertyByColumnName(dc.ColumnName, AcctEventDetailFielddv(0)(LookupListNew.COL_ID_NAME))
                            End If
                            AcctEventDetailFielddv.RowFilter = currFilter
                        ElseIf dc.ColumnName.ToLower.Equals("description_src") Then
                            curFieldValue = dr(dc.ColumnName).ToString
                            If curFieldValue.Contains(":") Then curFieldValue = curFieldValue.Substring(curFieldValue.LastIndexOf(":") + 1)

                            _aed.SetPropertyByColumnName(dc.ColumnName + "_id", LookupListNew.GetIdFromCode(AcctDescSourcedv, curFieldValue))

                        ElseIf dc.ColumnName.ToLower.StartsWith("analysis_src") Then
                            curFieldValue = dr(dc.ColumnName).ToString
                            If curFieldValue.Contains(":") Then curFieldValue = curFieldValue.Substring(curFieldValue.LastIndexOf(":") + 1)

                            _aed.SetPropertyByColumnName(dc.ColumnName + "_id", LookupListNew.GetIdFromCode(AcctTCodeSourcedv, curFieldValue))

                        Else
                            If dr(dc.ColumnName).ToString.EndsWith("%") Then dr(dc.ColumnName) = dr(dc.ColumnName).ToString.Remove(dr(dc.ColumnName).ToString.LastIndexOf("%"))
                            _aed.SetPropertyByColumnName(dc.ColumnName, dr(dc.ColumnName))

                        End If
                    Next

                    Try
                        _aed.Save()
                    Catch ex As BOValidationException
                        Dim valList(ex.ValidationErrorList.Length - 1) As ValidationError
                        For i As Integer = 0 To ex.ValidationErrorList.Length - 1
                            valList(i) = New ValidationError(SheetName.Replace("$", "") & ", Line " & iRowCount.ToString & ", " & ex.ValidationErrorList(i).PropertyName & ": |" & ex.ValidationErrorList(i).Message, ex.ValidationErrorList(i).BusinessObjectType, ex.ValidationErrorList(i).ValidationAttributeType, ex.ValidationErrorList(i).PropertyName, ex.ValidationErrorList(i).OffendingValue)
                        Next
                        Me.HandleErrors(New BOValidationException(valList, ex.BusinessObjectName), Me.ErrControllerMaster)
                    End Try
                Else
                    Exit For
                End If

                iRowCount += 1
            Next
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try

    End Function

    Private Function ClearColumnsAndRows(ByRef dt As DataTable) As Integer

        Dim dr As DataRow
        Dim dc As DataColumn
        Dim i, cnt As Integer

        If dt.Columns.IndexOf(Me.EXCLUDE_ROW) > 0 Then
            For i = dt.Rows.Count - 1 To 0 Step -1
                dr = dt.Rows(i)
                If dr(Me.EXCLUDE_ROW).ToString = Me.EXCLUDE_ROW_VALUE Then
                    dt.Rows.Remove(dr)
                    cnt += 1
                End If
            Next
        End If

        Return (cnt + 2)

    End Function

    Private Function GetEvent(ByVal EventName As String) As String

        If Me.EventMap Is Nothing Then
            Me.EventMap = New Collections.Specialized.StringDictionary
            Me.EventMap.Add(Me.EVENT_SHEET_UPR, Me.EVENT_UPR)
            Me.EventMap.Add(Me.EVENT_SHEET_REFUNDS, Me.EVENT_REFUNDS)
            Me.EventMap.Add(Me.EVENT_SHEET_PREM, Me.EVENT_PREM)
            Me.EventMap.Add(Me.EVENT_SHEET_IBNR, Me.EVENT_IBNR)
            Me.EventMap.Add(Me.EVENT_SHEET_CLAIMRES, Me.EVENT_CLAIMRES)
            Me.EventMap.Add(Me.EVENT_SHEET_CLAIM, Me.EVENT_CLAIM)
            Me.EventMap.Add(Me.EVENT_SHEET_INVOICING, Me.EVENT_INVOICING)
        End If

        Return Me.EventMap(EventName)

    End Function

    Private Function GetBusinessUnit(ByVal BusinessUnitName As String) As Guid

        Dim dv As AcctBusinessUnit.AcctBusinessUnitSearchDV

        dv = AcctBusinessUnit.getList(Me.State.AccountingCompanyId, BusinessUnitName)

        If Not dv Is Nothing AndAlso dv.Count = 1 Then
            Return GuidControl.ByteArrayToGuid(CType(dv(0)(dv.COL_ACCT_BUSINESS_UNIT_ID), Byte()))
        Else
            Dim _bu As New AcctBusinessUnit
            _bu.AcctCompanyId = Me.State.AccountingCompanyId
            _bu.BusinessUnit = BusinessUnitName
            _bu.Code = BusinessUnitName
            _bu.SuppressVendors = NO_STRING
            _bu.Save()
            Return _bu.Id
        End If

    End Function

#End Region
End Class