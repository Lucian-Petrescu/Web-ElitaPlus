
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Namespace Certificates
    Partial Public Class CertificateListForm
        Inherits ElitaPlusSearchPage
#Region "Constants"
        ' Public Const URL As String = "Certificates/CertificateListForm.aspx"
        'Public Const PAGETITLE As String = "CERTIFICATE_SEARCH"
        Public Const PAGETAB As String = "CERTIFICATES"

        Public Const SELECT_ACTION_COMMAND As String = "SelectAction"

        Public Const GRID_COL_CERTIFICATE_NUMBER_CTRL As String = "btnEditCertificate"

        ' Public Const GRID_CTRL_CERTIFICATE_ID As String = "lblCertID"

        'Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_CERTIFICATE_ID_IDX As Integer = 0
        Public Const GRID_COL_SERVICE_LINE_NUMBER_IDX As Integer = 1
        Public Const GRID_COL_HOME_PHONE_IDX As Integer = 2
        Public Const GRID_COL_WORK_PHONE_IDX As Integer = 3
        Public Const GRID_COL_SERIAL_NUMBER_IDX As Integer = 4
        Public Const GRID_COL_IMEI_NUMBER_IDX As Integer = 5

        Public Const GRID_COL_CUSTOMER_NAME_IDX As Integer = 6
        Public Const GRID_COL_ADDRESS1_IDX As Integer = 7
        Public Const GRID_COL_POSTAL_CODE_IDX As Integer = 8
        Public Const GRID_COL_TAXID_IDX As Integer = 9
        '   Public Const GRID_COL_CERT_NUMBER_IDX As Integer = 7 ' no need anymore
        Public Const GRID_COL_DEALER_IDX As Integer = 10
        'Added for Req-801
        Public Const GRID_COL_INVOICE_NUM_IDX As Integer = 11
        Public Const GRID_COL_MEMBERSHIP_NUM_IDX As Integer = 12 ' account number
        Public Const GRID_COL_PRODUCT_CODE_IDX As Integer = 13

        Public Const GRID_COL_VEHICLE_LICENSE_TAG_IDX As Integer = 14
        Public Const GRID_COL_STATUS_CODE_IDX As Integer = 15
        Public Const GRID_COL_IS_RESTRICTED_IDX As Integer = 16
        Public Const GRID_ROW_RESTRICTED_COLOR As String = "#DEE3E7"
#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False
        Private Shared searchTypeItems() As String = {"CERTNUM",
                                                      "PHONENUM",
                                                      "SERIALNUM",
                                                      "LICTAG",
                                                      "SERVICELINE"
                                                     }

        Class MyState

            Public MyBO As Certificate
            Public selectedSortById As Guid = Guid.Empty
            Public searchDV As Certificate.CombinedCertificateSearchDV = Nothing
            Public searchClick As Boolean = False
            Public certificatesFoundMSG As String

            Public selectedCertificateStatusId As String = Guid.Empty.ToString
            Public selectedCertificateId As Guid = Guid.Empty
            Public IsGridVisible As Boolean = False
            Public SortExpression As String = String.Empty

            Public PageIndex As Integer = 0
            Public PageSize As Integer = 30

            Public searchTypes As ArrayList = New ArrayList(searchTypeItems)
            Public ActivePanel As String = CStr(searchTypes.Item(0))

            Public certificateNumber As String
            Public customerName As String
            Public address As String
            Public zip As String
            Public PhoneNum As String
            Public dealerId As Guid
            Public dealerName As String = String.Empty
            'Added for Req-610
            Public PhoneType As String
            Public PhoneTypeId As Guid = Guid.Empty
            Public VehicleLicenceTag As String
            Public isVSCSearch As Boolean = False
            Public taxId As String
            Public AcctNum As String = String.Empty
            'Added for Req-801
            Public InvoiceNumber As String = String.Empty
            Public serialNumber As String
            Public InforceDate As String = String.Empty
            Public Service_line_number As String = String.Empty
            Public isDPO As Boolean = False
            Sub New()
            End Sub
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

#Region "Page event handlers"
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            Dim objPrincipal As ElitaPlusIdentity
            MasterPage.MessageController.Clear()

            Form.DefaultButton = btnSearch.UniqueID
            Try
                If Not IsPostBack Then

                    ShowPanel(State.ActivePanel)
                    MasterPage.MessageController.Clear()

                    ' Populate the header and bredcrumb
                    MasterPage.UsePageTabTitleInBreadCrum = False
                    ' Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Certificates")
                    UpdateBreadCrum()

                    TranslateGridHeader(Grid)
                    AddCalendar_New(btnInforceDate, txtInforceDate)
                    AddCalendar_New(btnInforceDate_SLN, txtInforceDate_SLN)
                    AddCalendar_New(btnPhoneNumSrchInforceCert, txtPhoneNumSrchInforceCert)
                    AddCalendar_New(btnLicTagSrchInforceCert, txtLicTagSrchInforceCert)
                    AddCalendar_New(btnSrlNumSrchInforceCert, txtSrlNumSrchInforceCert)

                    If Not IsReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        If ElitaPlusIdentity.Current.ActiveUser.IsDealer Then
                            State.dealerId = ElitaPlusIdentity.Current.ActiveUser.ScDealerId
                            ControlMgr.SetEnableControl(Me, ddlDealer, False)
                            ControlMgr.SetEnableControl(Me, moDealerDrop, False)
                            ControlMgr.SetEnableControl(Me, moDealerDrop_SLN, False)
                        End If
                    End If

                    GetStateProperties()
                    PopulateSearchControls()

                    If IsReturningFromChild Then
                        ' It is returning from detail
                        PopulateGrid()
                    End If


                    'Set page size
                    cboPageSize.SelectedValue = State.PageSize.ToString()

                    SetFocus(txtCertNum)


                    objPrincipal = CType(System.Threading.Thread.CurrentPrincipal, ElitaPlusPrincipal).Identity
                    If objPrincipal.PrivacyUserType = AppConfig.DataProtectionPrivacyLevel.Privacy_DataProtection Then
                        State.isDPO = True
                    ElseIf objPrincipal.DBPrivacyUserType = AppConfig.DB_PRIVACY_ADV Then
                        divHasRestrictedRecords.Visible = True
                    Else
                        divHasRestrictedRecords.Visible = False
                    End If

                End If
                DisplayNewProgressBarOnClick(btnSearch, "Loading_Certificates")
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub


        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("CERTIFICATE_SEARCH")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CERTIFICATE_SEARCH")
                End If
            End If
        End Sub


        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As CertificateForm.ReturnType = CType(ReturnPar, CertificateForm.ReturnType)
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    State.searchDV = Nothing
                End If
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                State.selectedCertificateId = retObj.EditingBo.Id
                            End If
                            State.IsGridVisible = True
                        End If
                End Select
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Button event handlers"
        Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
            Try
                SetStateProperties()
                State.PageIndex = 0
                State.selectedCertificateId = Guid.Empty
                State.IsGridVisible = True
                State.searchClick = True
                State.searchDV = Nothing
                PopulateGrid()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
            Try

                ' Clear all search options typed or selected by the user
                ClearAllSearchOptions()

                ' Update the Bo state properties with the new value
                ClearStateValues()

                SetStateProperties()

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ClearStateValues()
            Try
                'clear State
                State.certificateNumber = String.Empty
                State.customerName = String.Empty
                State.address = String.Empty
                State.zip = String.Empty
                State.PhoneNum = String.Empty
                State.dealerId = Nothing
                State.PhoneType = String.Empty   'Me.ddlPhoneType.SelectedValue 
                State.PhoneNum = String.Empty
                State.PhoneTypeId = Nothing
                State.taxId = String.Empty
                State.selectedCertificateStatusId = Guid.Empty.ToString
                State.InforceDate = String.Empty
                State.InvoiceNumber = String.Empty
                State.serialNumber = String.Empty
                State.VehicleLicenceTag = String.Empty
                State.AcctNum = String.Empty
                State.SortExpression = String.Empty
                State.Service_line_number = String.Empty
                State.dealerName = Nothing

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub ClearAllSearchOptions()

            ' Clear Phone search panel
            txtCertNum.Text = String.Empty
            txtCustName.Text = String.Empty
            txtAddress.Text = String.Empty
            txtZip.Text = String.Empty
            txtPhone.Text = String.Empty
            If Not ElitaPlusIdentity.Current.ActiveUser.IsDealer Then
                ddlDealer.SelectedIndex = 0
            End If

            'Added for Req-610
            ddlPhoneType.SelectedIndex = 0
            PopulateSortByDropDown(ddlSortBy, True)

            ' Clear certificate search panel
            moCertificateText.Text = String.Empty
            moCustomerNameText.Text = String.Empty
            moAddressText.Text = String.Empty
            moZipText.Text = String.Empty
            moTaxIdText.Text = String.Empty
            moStatusDrop.SelectedIndex = -1
            moAccountNum.Text = String.Empty
            If Not ElitaPlusIdentity.Current.ActiveUser.IsDealer Then moDealerDrop.SelectedIndex = 0
            'Added for Req-801
            moInvoiceNum.Text = String.Empty
            PopulateSortByDropDown(cboSortBy, False)

            'Clear Serial number panel search
            moSerialNumberText.Text = String.Empty

            'Clear Vehicle License Tag Panel
            moVehicleLicenceTagText.Text = String.Empty

            'Clear Service Line Panel
            moServiceLineNumber.Text = String.Empty
            txtInforceDate_SLN.Text = String.Empty
            moStatusDrop_SLN.SelectedIndex = -1
            PopulateSortByDropDown(cboSortBy_SLN, False)
            If Not ElitaPlusIdentity.Current.ActiveUser.IsDealer Then moDealerDrop_SLN.SelectedIndex = 0

        End Sub

        Protected Sub CertificateSearchDropDown_SelectedIndexChanged(sender As Object, e As EventArgs)
            State.ActivePanel = CertificateSearchDropDown.SelectedValue
            ClearAllSearchOptions()
            ShowPanel(State.ActivePanel)
            If CertificateSearchDropDown.SelectedIndex > 0 Then
                Grid.DataSource = Nothing
                Grid.DataBind()
            End If
        End Sub

        Protected Sub ShowPanel(selectedpanel As String)
            'First hide all
            For Each sPanel As String In State.searchTypes
                searchTable.FindControl(sPanel).Visible = False
            Next
            ' Now show selected
            searchTable.FindControl(selectedpanel).Visible = True
        End Sub

#End Region

#Region "Helper functions"
        Private Sub PopulateSearchControls()
            'Dim dv_dealerList As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code")

            Try
                PopulateCertificateStatusDropDown()

                'Populate Certificate Search Dropdowns
                PopulateCertificateSearchByDropdown(CertificateSearchDropDown)

                'populate dealer list
                PopulateDealerDropdown(moDealerDrop, State.dealerId)
                PopulateDealerDropdown(ddlDealer, State.dealerId)
                PopulateDealerDropdown(moDealerDrop_SLN, State.dealerId)

                PopulateSortByDropDown(cboSortBy, False)
                PopulateSortByDropDown(ddlSortBy, True)
                PopulateSortByDropDown(cboSortBy_SLN, False)

                'Added for Req-610
                PopulatePhoneTypeAndPhoneNumnberDropDown()
                If State.PhoneTypeId <> Guid.Empty Then
                    SetSelectedItem(ddlPhoneType, State.PhoneTypeId)
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulatePhoneTypeAndPhoneNumnberDropDown()
            Try

                Dim phoneType As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("PHNRTP", Thread.CurrentPrincipal.GetLanguageCode())
                ddlPhoneType.Populate(phoneType, New PopulateOptions())

                Dim phoneNumSrchBy As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("CSTAT", Thread.CurrentPrincipal.GetLanguageCode())

                ddlPhoneNumSrchByStatus.Populate(phoneNumSrchBy, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateCertificateStatusDropDown()
            Try

                Dim certStatus As DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList("CSTAT", Thread.CurrentPrincipal.GetLanguageCode())
                moStatusDrop.Populate(certStatus, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                moStatusDrop_SLN.Populate(certStatus, New PopulateOptions() With
                                                      {
                                                        .AddBlankItem = True
                                                       })

                moStatusDrop.SelectedValue = State.selectedCertificateStatusId
                moStatusDrop_SLN.SelectedValue = State.selectedCertificateStatusId

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateCertificateSearchByDropdown(DropDown As DropDownList)
            Try
                'Me.BindListTextToDataView(DropDown, LookupListNew.DropdownLookupList("CERTSRCHBY", ElitaPlusIdentity.Current.ActiveUser.LanguageId), "DESCRIPTION", "CODE", False)


                DropDown.Populate(CommonConfigManager.Current.ListManager.GetList("CERTSRCHBY", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = False,
                                                    .ValueFunc = AddressOf .GetCode
                                                  })



                If State.ActivePanel IsNot Nothing Then
                    DropDown.SelectedValue = State.ActivePanel
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateDealerDropdown(dealerDropDownList As DropDownList, setvalue As Guid)
            Try
                'Me.BindCodeNameToListControl(dealerDropDownList, dvListData, , , , True)
                Dim oDealerList
                If Authentication.CurrentUser.IsDealerGroup Then
                    oDealerList = CaseBase.GetDealerListByCompanyForExternalUser()
                Else
                    oDealerList = GetDealerListByCompanyForUser()
                End If
                Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                   Return li.Translation + " " + "(" + li.Code + ")"
                                                                               End Function
                dealerDropDownList.Populate(oDealerList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True,
                                                    .TextFunc = dealerTextFunc
                                                   })


                If setvalue <> Guid.Empty Then
                    SetSelectedItem(dealerDropDownList, setvalue)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Function GetDealerListByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
            Dim Index As Integer
            Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

            Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

            Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)

            For Index = 0 To UserCompanies.Count - 1
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CompanyId = UserCompanies(Index)
                Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
                If oDealerListForCompany.Count > 0 Then
                    If oDealerList IsNot Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = oDealerListForCompany.Clone()
                    End If

                End If
            Next

            Return oDealerList.ToArray()

        End Function

        Sub PopulateSortByDropDown(sortByDropDownList As DropDownList, filterIdentificationNumber As Boolean)
            Try
                'Dim dv As DataView = LookupListNew.GetCertificateSearchFieldsLookupList(ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                If filterIdentificationNumber Then
                    'dv.RowFilter &= " and code <> 'IDENTIFICATION_NUMBER'"
                    'CSDRP
                    Dim sortTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                     If (li.Code <> "IDENTIFICATION_NUMBER") Then

                                                                                         Return li.Translation
                                                                                     Else
                                                                                         Return Nothing
                                                                                        End If
                                                                                    End Function
                    sortByDropDownList.Populate(CommonConfigManager.Current.ListManager.GetList("CSDRP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                 {
                                                    .AddBlankItem = False,
                                                    .TextFunc = sortTextFunc
                                                 })

                End If

                'Me.BindListControlToDataView(sortByDropDownList, dv, , , False)
                sortByDropDownList.Populate(CommonConfigManager.Current.ListManager.GetList("CSDRP", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                 {
                                                    .AddBlankItem = False
                                                 })


                'Set the default Sort by
                Dim defaultSelectedCodeId As Guid = LookupListNew.GetIdFromCode(LookupListNew.LK_CERTIFICATE_SEARCH_FIELDS, "CUSTOMER_NAME")

                If (State.selectedSortById.Equals(Guid.Empty)) Then
                    SetSelectedItem(sortByDropDownList, defaultSelectedCodeId)
                    State.selectedSortById = defaultSelectedCodeId
                Else
                    SetSelectedItem(sortByDropDownList, State.selectedSortById)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GetStateProperties()
            Try

                ' Set state based on panel selected
                If State.ActivePanel.Equals(State.searchTypes.Item(0)) Then ' certificate panel

                    moCertificateText.Text = State.certificateNumber
                    If State.selectedSortById <> Guid.Empty And cboSortBy.Items.Count > 0 Then SetSelectedItem(cboSortBy, State.selectedSortById)

                    'Me.State.dealerId = Me.GetSelectedItem(Me.moDealerDrop)
                    If State.dealerId <> Guid.Empty And moDealerDrop.Items.Count > 0 Then SetSelectedItem(moDealerDrop, State.dealerId)

                    moCustomerNameText.Text = State.customerName
                    moZipText.Text = State.zip
                    moAddressText.Text = State.address
                    moTaxIdText.Text = State.taxId
                    moAccountNum.Text = State.AcctNum
                    moStatusDrop.SelectedValue = State.selectedCertificateStatusId
                    'Added for Req-801
                    moInvoiceNum.Text = State.InvoiceNumber
                    txtInforceDate.Text = State.InforceDate

                ElseIf State.ActivePanel.Equals(State.searchTypes.Item(1)) Then 'phone number

                    State.certificateNumber = txtCertNum.Text.ToUpper.Trim
                    If State.selectedSortById <> Guid.Empty And ddlSortBy.Items.Count > 0 Then
                        SetSelectedItem(ddlSortBy, State.selectedSortById)
                    End If

                    If State.dealerId <> Guid.Empty And ddlDealer.Items.Count > 0 Then SetSelectedItem(ddlDealer, State.dealerId)

                    txtCustName.Text = State.customerName
                    txtZip.Text = State.zip
                    txtAddress.Text = State.address

                    txtPhone.Text = State.PhoneNum
                    'Added for Req-610
                    If State.PhoneTypeId <> Guid.Empty And ddlPhoneType.Items.Count > 0 Then SetSelectedItem(ddlPhoneType, State.PhoneTypeId)

                    txtPhoneNumSrchInforceCert.Text = State.InforceDate
                    ddlPhoneNumSrchByStatus.SelectedValue = State.selectedCertificateStatusId.ToString

                ElseIf State.ActivePanel.Equals(State.searchTypes.Item(2)) Then ' serial number
                    ' Serial number panel is active
                    moSerialNumberText.Text = State.serialNumber
                    txtSrlNumSrchInforceCert.Text = State.InforceDate

                ElseIf State.ActivePanel.Equals(State.searchTypes.Item(3)) Then 'vl tag
                    ' vehicle license tag panel is active
                    moVehicleLicenceTagText.Text = State.VehicleLicenceTag
                    txtLicTagSrchInforceCert.Text = State.InforceDate
                ElseIf State.ActivePanel.Equals(State.searchTypes.Item(4)) Then 'Service Line Number
                    If State.selectedSortById <> Guid.Empty And cboSortBy_SLN.Items.Count > 0 Then SetSelectedItem(cboSortBy_SLN, State.selectedSortById)
                    If State.dealerId <> Guid.Empty And moDealerDrop_SLN.Items.Count > 0 Then SetSelectedItem(moDealerDrop_SLN, State.dealerId)
                    moStatusDrop_SLN.SelectedValue = State.selectedCertificateStatusId
                    txtInforceDate_SLN.Text = State.InforceDate
                    moSerialNumberText.Text = State.Service_line_number

                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SetStateProperties()
            Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

            Try
                If State Is Nothing Then
                    Trace(Me, "Restoring State")
                    RestoreState(New MyState)
                End If

                ClearStateValues()

                ' Set state based on panel selected
                ' Me.State.isDPO = current


                If State.ActivePanel.Equals(State.searchTypes.Item(0)) Then ' certificate panel
                    State.certificateNumber = moCertificateText.Text.ToUpper.Trim
                    State.selectedSortById = GetSelectedItem(cboSortBy)
                    State.dealerId = GetSelectedItem(moDealerDrop)
                    State.selectedCertificateStatusId = moStatusDrop.SelectedValue.Trim

                    State.customerName = moCustomerNameText.Text.ToUpper.Trim
                    State.zip = moZipText.Text.ToUpper.Trim
                    State.address = moAddressText.Text.ToUpper.Trim

                    State.dealerName = LookupListNew.GetCodeFromId("DEALERS", State.dealerId)

                    State.PhoneTypeId = GetSelectedItem(ddlPhoneType)
                    State.PhoneType = LookupListNew.GetCodeFromId("PHONE_NUMBER_TYPE", State.PhoneTypeId)
                    State.PhoneNum = txtPhone.Text.ToUpper.Trim
                    SetSelectedItem(ddlPhoneType, State.PhoneTypeId)

                    State.taxId = moTaxIdText.Text.ToUpper.Trim
                    State.AcctNum = moAccountNum.Text.Trim
                    'Added for Req-801
                    State.InvoiceNumber = moInvoiceNum.Text.ToUpper.Trim
                    'Added for Req 1310
                    State.InforceDate = txtInforceDate.Text.Trim
                    State.isVSCSearch = False

                ElseIf State.ActivePanel.Equals(State.searchTypes.Item(1)) Then 'phone number

                    State.certificateNumber = txtCertNum.Text.ToUpper.Trim
                    State.selectedSortById = GetSelectedItem(ddlSortBy)
                    State.dealerId = GetSelectedItem(ddlDealer)

                    State.customerName = txtCustName.Text.ToUpper.Trim
                    State.zip = txtZip.Text.ToUpper.Trim
                    State.address = txtAddress.Text.ToUpper.Trim

                    State.dealerName = LookupListNew.GetCodeFromId("DEALERS", State.dealerId)

                    State.PhoneTypeId = GetSelectedItem(ddlPhoneType)
                    State.PhoneType = LookupListNew.GetCodeFromId("PHONE_NUMBER_TYPE", State.PhoneTypeId)
                    State.PhoneNum = txtPhone.Text.ToUpper.Trim
                    SetSelectedItem(ddlPhoneType, State.PhoneTypeId)

                    State.taxId = moTaxIdText.Text.ToUpper.Trim
                    State.AcctNum = moAccountNum.Text.Trim

                    'Added for Req-801
                    State.InvoiceNumber = moInvoiceNum.Text.ToUpper.Trim

                    'Added for Req 1310
                    State.InforceDate = txtPhoneNumSrchInforceCert.Text.Trim
                    State.selectedCertificateStatusId = ddlPhoneNumSrchByStatus.SelectedValue.Trim
                    State.isVSCSearch = False
                ElseIf State.ActivePanel.Equals(State.searchTypes.Item(2)) Then ' serial number
                    ' Serial number panel is active
                    State.serialNumber = moSerialNumberText.Text.ToUpper.Trim
                    State.isVSCSearch = True

                    'Added for Req 1310
                    State.InforceDate = txtSrlNumSrchInforceCert.Text.Trim

                ElseIf State.ActivePanel.Equals(State.searchTypes.Item(3)) Then 'vl tag
                    ' vehicle license tag panel is active
                    State.VehicleLicenceTag = moVehicleLicenceTagText.Text.ToUpper.Trim
                    State.isVSCSearch = True

                    'Added for Req 1310
                    State.InforceDate = txtLicTagSrchInforceCert.Text.Trim

                ElseIf State.ActivePanel.Equals(State.searchTypes.Item(4)) Then 'service line number
                    State.dealerId = GetSelectedItem(moDealerDrop_SLN)
                    State.dealerName = LookupListNew.GetCodeFromId("DEALERS", State.dealerId)

                    State.selectedSortById = GetSelectedItem(cboSortBy_SLN)
                    State.selectedCertificateStatusId = moStatusDrop_SLN.SelectedValue.Trim
                    State.InforceDate = txtInforceDate_SLN.Text.Trim
                    State.Service_line_number = moServiceLineNumber.Text.Trim
                    State.isVSCSearch = False

                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGrid()
            Try
                Dim oDataView As DataView
                Dim sortBy As String = String.Empty
                If (State.searchDV Is Nothing) Then
                    If (Not (State.selectedSortById.Equals(Guid.Empty))) Then
                        sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CERTIFICATE_SEARCH_FIELDS, State.selectedSortById)
                    End If

                    State.searchDV = Certificate.GetCombinedCertificatesList(State.PhoneType,
                                                                State.PhoneNum,
                                                                State.certificateNumber,
                                                                State.customerName,
                                                                State.address,
                                                                State.zip,
                                                                State.dealerName,
                                                                LookupListNew.GetCodeFromId(LookupListNew.LK_CERTIFICATE_STATUS, New Guid(State.selectedCertificateStatusId)),
                                                                State.taxId,
                                                                State.InvoiceNumber,
                                                                State.AcctNum,
                                                                State.serialNumber,
                                                                State.isVSCSearch,
                                                                State.InforceDate,
                                                                State.VehicleLicenceTag,
                                                                State.Service_line_number,
                                                                State.isDPO,
                                                                sortBy)

                    If State.searchClick Then
                        ValidSearchResultCountNew(State.searchDV.Count, True)
                        State.searchClick = False
                    End If

                End If

                Grid.PageSize = State.PageSize
                SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedCertificateId, Grid, State.PageIndex)
                Grid.DataSource = State.searchDV

                State.PageIndex = Grid.PageIndex

                Grid.Columns(GRID_COL_SERVICE_LINE_NUMBER_IDX).Visible = False
                Grid.Columns(GRID_COL_HOME_PHONE_IDX).Visible = False
                Grid.Columns(GRID_COL_WORK_PHONE_IDX).Visible = False
                Grid.Columns(GRID_COL_SERIAL_NUMBER_IDX).Visible = False
                Grid.Columns(GRID_COL_IMEI_NUMBER_IDX).Visible = False
                Grid.Columns(GRID_COL_INVOICE_NUM_IDX).Visible = False
                Grid.Columns(GRID_COL_VEHICLE_LICENSE_TAG_IDX).Visible = False

                ' Set state based on panel selected
                If State.ActivePanel.Equals(State.searchTypes.Item(0)) Then
                    ' certificate panel
                    Grid.Columns(GRID_COL_INVOICE_NUM_IDX).Visible = True

                ElseIf State.ActivePanel.Equals(State.searchTypes.Item(1)) Then
                    'phone number
                    ''Added for Req-610
                    If State.PhoneType = "HM" Then
                        Grid.Columns(GRID_COL_HOME_PHONE_IDX).Visible = True
                    ElseIf State.PhoneType = "WC" Then
                        Grid.Columns(GRID_COL_WORK_PHONE_IDX).Visible = True
                    End If

                ElseIf State.ActivePanel.Equals(State.searchTypes.Item(2)) Then ' serial number
                    ' Serial number panel is active
                    Grid.Columns(GRID_COL_SERIAL_NUMBER_IDX).Visible = True
                    Grid.Columns(GRID_COL_IMEI_NUMBER_IDX).Visible = True

                ElseIf State.ActivePanel.Equals(State.searchTypes.Item(3)) Then 'vl tag
                    ' vehicle license tag panel is active
                    Grid.Columns(GRID_COL_VEHICLE_LICENSE_TAG_IDX).Visible = True

                ElseIf State.ActivePanel.Equals(State.searchTypes.Item(4)) Then 'Service_line_number
                    ' vehicle license tag panel is active
                    Grid.Columns(GRID_COL_SERVICE_LINE_NUMBER_IDX).Visible = True

                End If

                If (Not State.SortExpression.Equals(String.Empty)) Then
                    State.searchDV.Sort = State.SortExpression
                Else
                    State.SortExpression = sortBy
                End If

                HighLightSortColumn(Grid, State.SortExpression, IsNewUI)

                Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                Session("recCount") = State.searchDV.Count

                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If

                If State.searchDV.Count = 0 Then
                    For Each gvRow As GridViewRow In Grid.Rows
                        gvRow.Visible = False
                        gvRow.Controls.Clear()
                    Next
                    lblPageSize.Visible = False
                    cboPageSize.Visible = False
                    colonSepertor.Visible = False
                Else
                    lblPageSize.Visible = True
                    cboPageSize.Visible = True
                    colonSepertor.Visible = True
                End If

            Catch ex As Exception
                Dim GetExceptionType As String = ex.GetBaseException.GetType().Name
                If ((Not GetExceptionType.Equals(String.Empty)) And GetExceptionType.Equals("BOValidationException")) Then
                    ControlMgr.SetVisibleControl(Me, Grid, False)
                    lblPageSize.Visible = False
                    cboPageSize.Visible = False
                    colonSepertor.Visible = False
                    lblRecordCount.Text = ""
                End If
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Grid related"

        Private Sub Grid_SortCommand(source As Object, e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
            Try
                If State.SortExpression.StartsWith(e.SortExpression) Then
                    If State.SortExpression.EndsWith(" DESC") Then
                        State.SortExpression = e.SortExpression
                    Else
                        State.SortExpression &= " DESC"
                    End If
                Else
                    State.SortExpression = e.SortExpression
                End If
                State.PageIndex = 0
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = Grid.PageIndex
                State.selectedCertificateId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If (e.Row.RowType = DataControlRowType.DataRow) _
                    OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                    If (e.Row.Cells(GRID_COL_CERTIFICATE_ID_IDX).FindControl(GRID_COL_CERTIFICATE_NUMBER_CTRL) IsNot Nothing) Then
                        btnEditItem = CType(e.Row.Cells(GRID_COL_CERTIFICATE_ID_IDX).FindControl(GRID_COL_CERTIFICATE_NUMBER_CTRL), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Certificate.CombinedCertificateSearchDV.COL_CERTIFICATE_ID), Byte()))
                        btnEditItem.CommandName = SELECT_ACTION_COMMAND
                        btnEditItem.Text = dvRow(Certificate.CombinedCertificateSearchDV.COL_CERTIFICATE_NUMBER).ToString
                    End If

                    ' Convert short status codes to full description with css
                    e.Row.Cells(GRID_COL_STATUS_CODE_IDX).Text = LookupListNew.GetDescriptionFromCode("CSTAT", dvRow(Certificate.CombinedCertificateSearchDV.COL_STATUS_CODE).ToString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    If (dvRow(Certificate.CombinedCertificateSearchDV.COL_STATUS_CODE).ToString = Codes.CERTIFICATE_STATUS__ACTIVE) Then
                        e.Row.Cells(GRID_COL_STATUS_CODE_IDX).CssClass = "StatActive"
                    Else
                        e.Row.Cells(GRID_COL_STATUS_CODE_IDX).CssClass = "StatInactive"
                    End If

                    'This code for certificate restriction.
                    If (dvRow(Certificate.CombinedCertificateSearchDV.COL_IS_RESTRICTED) IsNot DBNull.Value AndAlso dvRow(Certificate.CombinedCertificateSearchDV.COL_IS_RESTRICTED).ToString = Codes.ENTITY_IS__RESTRICTED) Then
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(GRID_ROW_RESTRICTED_COLOR)
                    End If

                    'Fix DEF-2408
                    e.Row.Cells(GRID_COL_DEALER_IDX).Text = LookupListNew.GetDescriptionFromCode("DEALERS", dvRow(Certificate.CombinedCertificateSearchDV.COL_DEALER).ToString)
                End If
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If e.CommandName = SELECT_ACTION_COMMAND Then
                    If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                        State.selectedCertificateId = New Guid(e.CommandArgument.ToString())
                        callPage(CertificateForm.URL, State.selectedCertificateId)
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                If (TypeOf ex Is System.Reflection.TargetInvocationException) AndAlso
               (TypeOf ex.InnerException Is Threading.ThreadAbortException) Then Return
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub


#End Region

    End Class
End Namespace