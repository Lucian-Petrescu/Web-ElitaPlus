
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
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Dim objPrincipal As ElitaPlusIdentity
            Me.MasterPage.MessageController.Clear()

            Me.Form.DefaultButton = btnSearch.UniqueID
            Try
                If Not Me.IsPostBack Then

                    ShowPanel(Me.State.ActivePanel)
                    Me.MasterPage.MessageController.Clear()

                    ' Populate the header and bredcrumb
                    Me.MasterPage.UsePageTabTitleInBreadCrum = False
                    ' Me.MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage("")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Certificates")
                    UpdateBreadCrum()

                    TranslateGridHeader(Grid)
                    Me.AddCalendar_New(Me.btnInforceDate, Me.txtInforceDate)
                    Me.AddCalendar_New(Me.btnInforceDate_SLN, Me.txtInforceDate_SLN)
                    Me.AddCalendar_New(Me.btnPhoneNumSrchInforceCert, Me.txtPhoneNumSrchInforceCert)
                    Me.AddCalendar_New(Me.btnLicTagSrchInforceCert, Me.txtLicTagSrchInforceCert)
                    Me.AddCalendar_New(Me.btnSrlNumSrchInforceCert, Me.txtSrlNumSrchInforceCert)

                    If Not Me.IsReturningFromChild Then
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

                    If Me.IsReturningFromChild Then
                        ' It is returning from detail
                        Me.PopulateGrid()
                    End If


                    'Set page size
                    cboPageSize.SelectedValue = Me.State.PageSize.ToString()

                    SetFocus(Me.txtCertNum)


                    objPrincipal = CType(System.Threading.Thread.CurrentPrincipal, ElitaPlusPrincipal).Identity
                    If objPrincipal.PrivacyUserType = AppConfig.DataProtectionPrivacyLevel.Privacy_DataProtection Then
                        Me.State.isDPO = True
                    ElseIf objPrincipal.DBPrivacyUserType = AppConfig.DB_PRIVACY_ADV Then
                        divHasRestrictedRecords.Visible = True
                    Else
                        divHasRestrictedRecords.Visible = False
                    End If

                End If
                Me.DisplayNewProgressBarOnClick(Me.btnSearch, "Loading_Certificates")
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub


        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("CERTIFICATE_SEARCH")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("CERTIFICATE_SEARCH")
                End If
            End If
        End Sub


        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As CertificateForm.ReturnType = CType(ReturnPar, CertificateForm.ReturnType)
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    Me.State.searchDV = Nothing
                End If
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                Me.State.selectedCertificateId = retObj.EditingBo.Id
                            End If
                            Me.State.IsGridVisible = True
                        End If
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Button event handlers"
        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
            Try
                Me.SetStateProperties()
                Me.State.PageIndex = 0
                Me.State.selectedCertificateId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.searchClick = True
                Me.State.searchDV = Nothing
                Me.PopulateGrid()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
            Try

                ' Clear all search options typed or selected by the user
                Me.ClearAllSearchOptions()

                ' Update the Bo state properties with the new value
                Me.ClearStateValues()

                Me.SetStateProperties()

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ClearStateValues()
            Try
                'clear State
                Me.State.certificateNumber = String.Empty
                Me.State.customerName = String.Empty
                Me.State.address = String.Empty
                Me.State.zip = String.Empty
                Me.State.PhoneNum = String.Empty
                Me.State.dealerId = Nothing
                Me.State.PhoneType = String.Empty   'Me.ddlPhoneType.SelectedValue 
                Me.State.PhoneNum = String.Empty
                Me.State.PhoneTypeId = Nothing
                Me.State.taxId = String.Empty
                Me.State.selectedCertificateStatusId = Guid.Empty.ToString
                Me.State.InforceDate = String.Empty
                Me.State.InvoiceNumber = String.Empty
                Me.State.serialNumber = String.Empty
                Me.State.VehicleLicenceTag = String.Empty
                Me.State.AcctNum = String.Empty
                Me.State.SortExpression = String.Empty
                Me.State.Service_line_number = String.Empty
                Me.State.dealerName = Nothing

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub ClearAllSearchOptions()

            ' Clear Phone search panel
            Me.txtCertNum.Text = String.Empty
            Me.txtCustName.Text = String.Empty
            Me.txtAddress.Text = String.Empty
            Me.txtZip.Text = String.Empty
            Me.txtPhone.Text = String.Empty
            If Not ElitaPlusIdentity.Current.ActiveUser.IsDealer Then
                Me.ddlDealer.SelectedIndex = 0
            End If

            'Added for Req-610
            Me.ddlPhoneType.SelectedIndex = 0
            Me.PopulateSortByDropDown(Me.ddlSortBy, True)

            ' Clear certificate search panel
            Me.moCertificateText.Text = String.Empty
            Me.moCustomerNameText.Text = String.Empty
            Me.moAddressText.Text = String.Empty
            Me.moZipText.Text = String.Empty
            Me.moTaxIdText.Text = String.Empty
            Me.moStatusDrop.SelectedIndex = -1
            Me.moAccountNum.Text = String.Empty
            If Not ElitaPlusIdentity.Current.ActiveUser.IsDealer Then Me.moDealerDrop.SelectedIndex = 0
            'Added for Req-801
            Me.moInvoiceNum.Text = String.Empty
            Me.PopulateSortByDropDown(Me.cboSortBy, False)

            'Clear Serial number panel search
            Me.moSerialNumberText.Text = String.Empty

            'Clear Vehicle License Tag Panel
            Me.moVehicleLicenceTagText.Text = String.Empty

            'Clear Service Line Panel
            Me.moServiceLineNumber.Text = String.Empty
            Me.txtInforceDate_SLN.Text = String.Empty
            Me.moStatusDrop_SLN.SelectedIndex = -1
            Me.PopulateSortByDropDown(Me.cboSortBy_SLN, False)
            If Not ElitaPlusIdentity.Current.ActiveUser.IsDealer Then Me.moDealerDrop_SLN.SelectedIndex = 0

        End Sub

        Protected Sub CertificateSearchDropDown_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs)
            Me.State.ActivePanel = CertificateSearchDropDown.SelectedValue
            Me.ClearAllSearchOptions()
            Me.ShowPanel(Me.State.ActivePanel)
            If CertificateSearchDropDown.SelectedIndex > 0 Then
                Me.Grid.DataSource = Nothing
                Me.Grid.DataBind()
            End If
        End Sub

        Protected Sub ShowPanel(selectedpanel As String)
            'First hide all
            For Each sPanel As String In Me.State.searchTypes
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
                Me.PopulateCertificateSearchByDropdown(CertificateSearchDropDown)

                'populate dealer list
                Me.PopulateDealerDropdown(Me.moDealerDrop, Me.State.dealerId)
                Me.PopulateDealerDropdown(Me.ddlDealer, Me.State.dealerId)
                Me.PopulateDealerDropdown(Me.moDealerDrop_SLN, Me.State.dealerId)

                Me.PopulateSortByDropDown(Me.cboSortBy, False)
                Me.PopulateSortByDropDown(Me.ddlSortBy, True)
                Me.PopulateSortByDropDown(Me.cboSortBy_SLN, False)

                'Added for Req-610
                PopulatePhoneTypeAndPhoneNumnberDropDown()
                If Me.State.PhoneTypeId <> Guid.Empty Then
                    Me.SetSelectedItem(ddlPhoneType, Me.State.PhoneTypeId)
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                Me.HandleErrors(ex, MasterPage.MessageController)
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

                Me.moStatusDrop.SelectedValue = Me.State.selectedCertificateStatusId
                Me.moStatusDrop_SLN.SelectedValue = Me.State.selectedCertificateStatusId

            Catch ex As Exception
                Me.HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateCertificateSearchByDropdown(ByVal DropDown As DropDownList)
            Try
                'Me.BindListTextToDataView(DropDown, LookupListNew.DropdownLookupList("CERTSRCHBY", ElitaPlusIdentity.Current.ActiveUser.LanguageId), "DESCRIPTION", "CODE", False)


                DropDown.Populate(CommonConfigManager.Current.ListManager.GetList("CERTSRCHBY", Thread.CurrentPrincipal.GetLanguageCode()), New PopulateOptions() With
                                                  {
                                                    .AddBlankItem = False,
                                                    .ValueFunc = AddressOf .GetCode
                                                  })



                If Not Me.State.ActivePanel Is Nothing Then
                    DropDown.SelectedValue = Me.State.ActivePanel
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateDealerDropdown(ByVal dealerDropDownList As DropDownList, ByVal setvalue As Guid)
            Try
                'Me.BindCodeNameToListControl(dealerDropDownList, dvListData, , , , True)
                Dim oDealerList = GetDealerListByCompanyForUser()
                Dim dealerTextFunc As Func(Of DataElements.ListItem, String) = Function(li As DataElements.ListItem)
                                                                                   Return li.Translation + " " + "(" + li.Code + ")"
                                                                               End Function
                dealerDropDownList.Populate(oDealerList, New PopulateOptions() With
                                                   {
                                                    .AddBlankItem = True,
                                                    .TextFunc = dealerTextFunc
                                                   })


                If setvalue <> Guid.Empty Then
                    Me.SetSelectedItem(dealerDropDownList, setvalue)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
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
                    If Not oDealerList Is Nothing Then
                        oDealerList.AddRange(oDealerListForCompany)
                    Else
                        oDealerList = oDealerListForCompany.Clone()
                    End If

                End If
            Next

            Return oDealerList.ToArray()

        End Function

        Sub PopulateSortByDropDown(ByVal sortByDropDownList As DropDownList, ByVal filterIdentificationNumber As Boolean)
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

                If (Me.State.selectedSortById.Equals(Guid.Empty)) Then
                    Me.SetSelectedItem(sortByDropDownList, defaultSelectedCodeId)
                    Me.State.selectedSortById = defaultSelectedCodeId
                Else
                    Me.SetSelectedItem(sortByDropDownList, Me.State.selectedSortById)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub GetStateProperties()
            Try

                ' Set state based on panel selected
                If Me.State.ActivePanel.Equals(Me.State.searchTypes.Item(0)) Then ' certificate panel

                    Me.moCertificateText.Text = Me.State.certificateNumber
                    If Me.State.selectedSortById <> Guid.Empty And cboSortBy.Items.Count > 0 Then Me.SetSelectedItem(cboSortBy, Me.State.selectedSortById)

                    'Me.State.dealerId = Me.GetSelectedItem(Me.moDealerDrop)
                    If Me.State.dealerId <> Guid.Empty And moDealerDrop.Items.Count > 0 Then Me.SetSelectedItem(moDealerDrop, State.dealerId)

                    Me.moCustomerNameText.Text = Me.State.customerName
                    Me.moZipText.Text = Me.State.zip
                    Me.moAddressText.Text = Me.State.address
                    Me.moTaxIdText.Text = Me.State.taxId
                    Me.moAccountNum.Text = State.AcctNum
                    Me.moStatusDrop.SelectedValue = Me.State.selectedCertificateStatusId
                    'Added for Req-801
                    Me.moInvoiceNum.Text = Me.State.InvoiceNumber
                    Me.txtInforceDate.Text = Me.State.InforceDate

                ElseIf Me.State.ActivePanel.Equals(Me.State.searchTypes.Item(1)) Then 'phone number

                    Me.State.certificateNumber = Me.txtCertNum.Text.ToUpper.Trim
                    If State.selectedSortById <> Guid.Empty And ddlSortBy.Items.Count > 0 Then
                        Me.SetSelectedItem(ddlSortBy, Me.State.selectedSortById)
                    End If

                    If Me.State.dealerId <> Guid.Empty And ddlDealer.Items.Count > 0 Then Me.SetSelectedItem(ddlDealer, Me.State.dealerId)

                    Me.txtCustName.Text = Me.State.customerName
                    Me.txtZip.Text = Me.State.zip
                    Me.txtAddress.Text = Me.State.address

                    Me.txtPhone.Text = Me.State.PhoneNum
                    'Added for Req-610
                    If Me.State.PhoneTypeId <> Guid.Empty And ddlPhoneType.Items.Count > 0 Then Me.SetSelectedItem(ddlPhoneType, Me.State.PhoneTypeId)

                    Me.txtPhoneNumSrchInforceCert.Text = Me.State.InforceDate
                    Me.ddlPhoneNumSrchByStatus.SelectedValue = Me.State.selectedCertificateStatusId.ToString

                ElseIf Me.State.ActivePanel.Equals(Me.State.searchTypes.Item(2)) Then ' serial number
                    ' Serial number panel is active
                    Me.moSerialNumberText.Text = Me.State.serialNumber
                    Me.txtSrlNumSrchInforceCert.Text = Me.State.InforceDate

                ElseIf Me.State.ActivePanel.Equals(Me.State.searchTypes.Item(3)) Then 'vl tag
                    ' vehicle license tag panel is active
                    Me.moVehicleLicenceTagText.Text = Me.State.VehicleLicenceTag
                    Me.txtLicTagSrchInforceCert.Text = Me.State.InforceDate
                ElseIf Me.State.ActivePanel.Equals(Me.State.searchTypes.Item(4)) Then 'Service Line Number
                    If Me.State.selectedSortById <> Guid.Empty And cboSortBy_SLN.Items.Count > 0 Then Me.SetSelectedItem(cboSortBy_SLN, Me.State.selectedSortById)
                    If Me.State.dealerId <> Guid.Empty And moDealerDrop_SLN.Items.Count > 0 Then Me.SetSelectedItem(moDealerDrop_SLN, Me.State.dealerId)
                    Me.moStatusDrop_SLN.SelectedValue = Me.State.selectedCertificateStatusId
                    Me.txtInforceDate_SLN.Text = Me.State.InforceDate
                    Me.moSerialNumberText.Text = Me.State.Service_line_number

                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SetStateProperties()
            Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

            Try
                If Me.State Is Nothing Then
                    Me.Trace(Me, "Restoring State")
                    Me.RestoreState(New MyState)
                End If

                Me.ClearStateValues()

                ' Set state based on panel selected
                ' Me.State.isDPO = current


                If Me.State.ActivePanel.Equals(Me.State.searchTypes.Item(0)) Then ' certificate panel
                    Me.State.certificateNumber = Me.moCertificateText.Text.ToUpper.Trim
                    Me.State.selectedSortById = Me.GetSelectedItem(Me.cboSortBy)
                    Me.State.dealerId = Me.GetSelectedItem(Me.moDealerDrop)
                    Me.State.selectedCertificateStatusId = Me.moStatusDrop.SelectedValue.Trim

                    Me.State.customerName = Me.moCustomerNameText.Text.ToUpper.Trim
                    Me.State.zip = Me.moZipText.Text.ToUpper.Trim
                    Me.State.address = Me.moAddressText.Text.ToUpper.Trim

                    Me.State.dealerName = LookupListNew.GetCodeFromId("DEALERS", Me.State.dealerId)

                    Me.State.PhoneTypeId = Me.GetSelectedItem(Me.ddlPhoneType)
                    Me.State.PhoneType = LookupListNew.GetCodeFromId("PHONE_NUMBER_TYPE", Me.State.PhoneTypeId)
                    Me.State.PhoneNum = Me.txtPhone.Text.ToUpper.Trim
                    Me.SetSelectedItem(ddlPhoneType, Me.State.PhoneTypeId)

                    Me.State.taxId = Me.moTaxIdText.Text.ToUpper.Trim
                    Me.State.AcctNum = moAccountNum.Text.Trim
                    'Added for Req-801
                    Me.State.InvoiceNumber = Me.moInvoiceNum.Text.ToUpper.Trim
                    'Added for Req 1310
                    Me.State.InforceDate = Me.txtInforceDate.Text.Trim
                    Me.State.isVSCSearch = False

                ElseIf Me.State.ActivePanel.Equals(Me.State.searchTypes.Item(1)) Then 'phone number

                    Me.State.certificateNumber = Me.txtCertNum.Text.ToUpper.Trim
                    Me.State.selectedSortById = Me.GetSelectedItem(Me.ddlSortBy)
                    Me.State.dealerId = Me.GetSelectedItem(ddlDealer)

                    Me.State.customerName = Me.txtCustName.Text.ToUpper.Trim
                    Me.State.zip = Me.txtZip.Text.ToUpper.Trim
                    Me.State.address = Me.txtAddress.Text.ToUpper.Trim

                    Me.State.dealerName = LookupListNew.GetCodeFromId("DEALERS", Me.State.dealerId)

                    Me.State.PhoneTypeId = Me.GetSelectedItem(Me.ddlPhoneType)
                    Me.State.PhoneType = LookupListNew.GetCodeFromId("PHONE_NUMBER_TYPE", Me.State.PhoneTypeId)
                    Me.State.PhoneNum = Me.txtPhone.Text.ToUpper.Trim
                    Me.SetSelectedItem(ddlPhoneType, Me.State.PhoneTypeId)

                    Me.State.taxId = Me.moTaxIdText.Text.ToUpper.Trim
                    Me.State.AcctNum = moAccountNum.Text.Trim

                    'Added for Req-801
                    Me.State.InvoiceNumber = Me.moInvoiceNum.Text.ToUpper.Trim

                    'Added for Req 1310
                    Me.State.InforceDate = Me.txtPhoneNumSrchInforceCert.Text.Trim
                    Me.State.selectedCertificateStatusId = Me.ddlPhoneNumSrchByStatus.SelectedValue.Trim
                    Me.State.isVSCSearch = False
                ElseIf Me.State.ActivePanel.Equals(Me.State.searchTypes.Item(2)) Then ' serial number
                    ' Serial number panel is active
                    Me.State.serialNumber = Me.moSerialNumberText.Text.ToUpper.Trim
                    Me.State.isVSCSearch = True

                    'Added for Req 1310
                    Me.State.InforceDate = Me.txtSrlNumSrchInforceCert.Text.Trim

                ElseIf Me.State.ActivePanel.Equals(Me.State.searchTypes.Item(3)) Then 'vl tag
                    ' vehicle license tag panel is active
                    Me.State.VehicleLicenceTag = Me.moVehicleLicenceTagText.Text.ToUpper.Trim
                    Me.State.isVSCSearch = True

                    'Added for Req 1310
                    Me.State.InforceDate = Me.txtLicTagSrchInforceCert.Text.Trim

                ElseIf Me.State.ActivePanel.Equals(Me.State.searchTypes.Item(4)) Then 'service line number
                    Me.State.dealerId = Me.GetSelectedItem(Me.moDealerDrop_SLN)
                    Me.State.dealerName = LookupListNew.GetCodeFromId("DEALERS", Me.State.dealerId)

                    Me.State.selectedSortById = Me.GetSelectedItem(Me.cboSortBy_SLN)
                    Me.State.selectedCertificateStatusId = Me.moStatusDrop_SLN.SelectedValue.Trim
                    Me.State.InforceDate = Me.txtInforceDate_SLN.Text.Trim
                    Me.State.Service_line_number = Me.moServiceLineNumber.Text.Trim
                    Me.State.isVSCSearch = False

                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGrid()
            Try
                Dim oDataView As DataView
                Dim sortBy As String = String.Empty
                If (Me.State.searchDV Is Nothing) Then
                    If (Not (Me.State.selectedSortById.Equals(Guid.Empty))) Then
                        sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CERTIFICATE_SEARCH_FIELDS, Me.State.selectedSortById)
                    End If

                    Me.State.searchDV = Certificate.GetCombinedCertificatesList(Me.State.PhoneType,
                                                                Me.State.PhoneNum,
                                                                Me.State.certificateNumber,
                                                                Me.State.customerName,
                                                                Me.State.address,
                                                                Me.State.zip,
                                                                Me.State.dealerName,
                                                                LookupListNew.GetCodeFromId(LookupListNew.LK_CERTIFICATE_STATUS, New Guid(Me.State.selectedCertificateStatusId)),
                                                                Me.State.taxId,
                                                                Me.State.InvoiceNumber,
                                                                Me.State.AcctNum,
                                                                Me.State.serialNumber,
                                                                Me.State.isVSCSearch,
                                                                Me.State.InforceDate,
                                                                Me.State.VehicleLicenceTag,
                                                                Me.State.Service_line_number,
                                                                Me.State.isDPO,
                                                                sortBy)

                    If Me.State.searchClick Then
                        Me.ValidSearchResultCountNew(Me.State.searchDV.Count, True)
                        Me.State.searchClick = False
                    End If

                End If

                Grid.PageSize = State.PageSize
                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedCertificateId, Me.Grid, Me.State.PageIndex)
                Me.Grid.DataSource = Me.State.searchDV

                Me.State.PageIndex = Me.Grid.PageIndex

                Grid.Columns(GRID_COL_SERVICE_LINE_NUMBER_IDX).Visible = False
                Grid.Columns(GRID_COL_HOME_PHONE_IDX).Visible = False
                Grid.Columns(GRID_COL_WORK_PHONE_IDX).Visible = False
                Grid.Columns(GRID_COL_SERIAL_NUMBER_IDX).Visible = False
                Grid.Columns(GRID_COL_IMEI_NUMBER_IDX).Visible = False
                Grid.Columns(GRID_COL_INVOICE_NUM_IDX).Visible = False
                Grid.Columns(GRID_COL_VEHICLE_LICENSE_TAG_IDX).Visible = False

                ' Set state based on panel selected
                If Me.State.ActivePanel.Equals(Me.State.searchTypes.Item(0)) Then
                    ' certificate panel
                    Grid.Columns(GRID_COL_INVOICE_NUM_IDX).Visible = True

                ElseIf Me.State.ActivePanel.Equals(Me.State.searchTypes.Item(1)) Then
                    'phone number
                    ''Added for Req-610
                    If State.PhoneType = "HM" Then
                        Grid.Columns(GRID_COL_HOME_PHONE_IDX).Visible = True
                    ElseIf State.PhoneType = "WC" Then
                        Grid.Columns(GRID_COL_WORK_PHONE_IDX).Visible = True
                    End If

                ElseIf Me.State.ActivePanel.Equals(Me.State.searchTypes.Item(2)) Then ' serial number
                    ' Serial number panel is active
                    Grid.Columns(GRID_COL_SERIAL_NUMBER_IDX).Visible = True
                    Grid.Columns(GRID_COL_IMEI_NUMBER_IDX).Visible = True

                ElseIf Me.State.ActivePanel.Equals(Me.State.searchTypes.Item(3)) Then 'vl tag
                    ' vehicle license tag panel is active
                    Grid.Columns(GRID_COL_VEHICLE_LICENSE_TAG_IDX).Visible = True

                ElseIf Me.State.ActivePanel.Equals(Me.State.searchTypes.Item(4)) Then 'Service_line_number
                    ' vehicle license tag panel is active
                    Grid.Columns(GRID_COL_SERVICE_LINE_NUMBER_IDX).Visible = True

                End If

                If (Not Me.State.SortExpression.Equals(String.Empty)) Then
                    Me.State.searchDV.Sort = Me.State.SortExpression
                Else
                    Me.State.SortExpression = sortBy
                End If

                HighLightSortColumn(Me.Grid, Me.State.SortExpression, Me.IsNewUI)

                Me.Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                Session("recCount") = Me.State.searchDV.Count

                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
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
                    Me.lblRecordCount.Text = ""
                End If
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

#End Region

#Region "Grid related"

        Private Sub Grid_SortCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.GridViewSortEventArgs) Handles Grid.Sorting
            Try
                If Me.State.SortExpression.StartsWith(e.SortExpression) Then
                    If Me.State.SortExpression.EndsWith(" DESC") Then
                        Me.State.SortExpression = e.SortExpression
                    Else
                        Me.State.SortExpression &= " DESC"
                    End If
                Else
                    Me.State.SortExpression = e.SortExpression
                End If
                Me.State.PageIndex = 0
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                Me.State.PageIndex = Grid.PageIndex
                Me.State.selectedCertificateId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowCreated(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
                Dim btnEditItem As LinkButton
                If (e.Row.RowType = DataControlRowType.DataRow) _
                    OrElse (e.Row.RowType = DataControlRowType.Separator) Then
                    If (Not e.Row.Cells(Me.GRID_COL_CERTIFICATE_ID_IDX).FindControl(GRID_COL_CERTIFICATE_NUMBER_CTRL) Is Nothing) Then
                        btnEditItem = CType(e.Row.Cells(Me.GRID_COL_CERTIFICATE_ID_IDX).FindControl(GRID_COL_CERTIFICATE_NUMBER_CTRL), LinkButton)
                        btnEditItem.CommandArgument = GetGuidStringFromByteArray(CType(dvRow(Certificate.CombinedCertificateSearchDV.COL_CERTIFICATE_ID), Byte()))
                        btnEditItem.CommandName = SELECT_ACTION_COMMAND
                        btnEditItem.Text = dvRow(Certificate.CombinedCertificateSearchDV.COL_CERTIFICATE_NUMBER).ToString
                    End If

                    ' Convert short status codes to full description with css
                    e.Row.Cells(Me.GRID_COL_STATUS_CODE_IDX).Text = LookupListNew.GetDescriptionFromCode("CSTAT", dvRow(Certificate.CombinedCertificateSearchDV.COL_STATUS_CODE).ToString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    If (dvRow(Certificate.CombinedCertificateSearchDV.COL_STATUS_CODE).ToString = Codes.CERTIFICATE_STATUS__ACTIVE) Then
                        e.Row.Cells(Me.GRID_COL_STATUS_CODE_IDX).CssClass = "StatActive"
                    Else
                        e.Row.Cells(Me.GRID_COL_STATUS_CODE_IDX).CssClass = "StatInactive"
                    End If

                    'This code for certificate restriction.
                    If (dvRow(Certificate.CombinedCertificateSearchDV.COL_IS_RESTRICTED) IsNot DBNull.Value AndAlso dvRow(Certificate.CombinedCertificateSearchDV.COL_IS_RESTRICTED).ToString = Codes.ENTITY_IS__RESTRICTED) Then
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(GRID_ROW_RESTRICTED_COLOR)
                    End If

                    'Fix DEF-2408
                    e.Row.Cells(Me.GRID_COL_DEALER_IDX).Text = LookupListNew.GetDescriptionFromCode("DEALERS", dvRow(Certificate.CombinedCertificateSearchDV.COL_DEALER).ToString)
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If e.CommandName = SELECT_ACTION_COMMAND Then
                    If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                        Me.State.selectedCertificateId = New Guid(e.CommandArgument.ToString())
                        Me.callPage(CertificateForm.URL, Me.State.selectedCertificateId)
                    End If
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                If (TypeOf ex Is System.Reflection.TargetInvocationException) AndAlso
               (TypeOf ex.InnerException Is Threading.ThreadAbortException) Then Return
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub


#End Region

    End Class
End Namespace