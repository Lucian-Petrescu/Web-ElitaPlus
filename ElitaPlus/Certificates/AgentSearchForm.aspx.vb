Imports System.Collections.Generic
Imports System.IO
Imports System.Reflection
Imports System.Text
Imports System.Threading
Imports System.Xml
Imports System.Xml.Xsl
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Newtonsoft.Json
Imports OracleInternal.ServiceObjects
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports System.Globalization

Namespace Certificates
    Partial Public Class AgentSearchForm
        Inherits ElitaPlusSearchPage
        Implements IStateController
#Region "Page Constants and Properties"
#Region "Constant"
        Private Const BackupStateSessionKey As String = "SESSION_KEY_BACKUP_STATE_AGENT_SEARCH_FORM"
        Private Const PurposeCancelCertRequest As String = "CERTCANCEL"

        Private Const DefaultItem As Integer = 0
        Private Const OneSpace As String = " "

        Private Const SelectActionCommand As String = "SelectAction"
        Private Const SelectActionCommandCase As String = "SelectActionCase"
        Private Const SelectActionCommandCert As String = "SelectActionCert"
        Private Const SelectActionCancelCert As String = "SelectActionCancelReq"
        Private Const ActionCommandNewCaseCertificate As String = "NewCaseViewCertificate"
        Private Const ActionCommandNewCaseClaim As String = "NewCaseViewClaim"
        Private Const ActionCommandResumeCaseClaim As String = "ResumeCaseViewClaim"
        Private Const CodeSearchFieldCaseNumber As String = "CASE_NUMBER"
        Private Const CodeSearchFieldCertificateNumber As String = "CERTIFICATE_NUMBER"
        Private Const CodeSearchFieldClaimNumber As String = "CLAIM_NUMBER"
        Private Const CodeSearchFieldAccountNumber As String = "ACCOUNT_NUMBER"
        Private Const CodeSearchFieldCustomerFirstName As String = "CUSTOMER_FIRST_NAME"
        Private Const CodeSearchFieldCustomerLastName As String = "CUSTOMER_LAST_NAME"
        Private Const CodeSearchFieldEmail As String = "EMAIL"
        Private Const CodeSearchFieldGlobalCustomerNumber As String = "GLOBAL_CUSTOMER_NUMBER"
        Private Const CodeSearchFieldInvoiceNumber As String = "INVOICE_NUMBER"
        Private Const CodeSearchFieldPhoneNumber As String = "PHONE_NUMBER"
        Private Const CodeSearchFieldCertificateStatus As String = "CSTAT"
        Private Const CodeSearchFieldSerialImeiNumber As String = "SERIAL_OR_IMEI_NUMBER"
        Private Const CodeSearchFieldServiceLineNumber As String = "SERVICE_LINE_NUMBER"
        Private Const CodeSearchFieldTaxId As String = "TAX_ID"
        Private Const CodeSearchFieldZip As String = "ZIP"
        Private Const SearchTypeXCD As String = "SEARCH_TYPE-AGENT_SEARCH"
        Private Const CodeSearchFieldDob As String = "BIRTH_DATE_CAL"



#End Region

#Region "Properties"
        Private _isReturningFromChild As Boolean = False
#End Region
#End Region
#Region "Page State"

        Class MyState
            Public SearchDv As CaseBase.AgentSearchDv = Nothing

            Public SelectedId As Guid = Guid.Empty
            Public IsCallerAuthenticated As Boolean = False
            Public IsGridVisible As Boolean = False

            Public CompanyId As Guid = Guid.Empty
            Public DealerId As Guid = Guid.Empty
            Public CertificateStatus As String = String.Empty
            Public CaseNumber As String = String.Empty
            Public ClaimNumber As String = String.Empty
            Public CertificateNumber As String = String.Empty
            Public CustomerFirstName As String = String.Empty
            Public CustomerLastName As String = String.Empty
            Public PhoneNumber As String = String.Empty
            Public SerialNumber As String = String.Empty
            Public InvoiceNumber As String = String.Empty
            Public Email As String = String.Empty
            Public Zipcode As String = String.Empty
            Public TaxId As String = String.Empty
            Public ServiceLineNumber As String = String.Empty
            Public AccountNumber As String = String.Empty
            Public GlobalCustomerNumber As String = String.Empty
            Public ExclSecFieldsDt As DataTable = Nothing
            Public SearchResultsConfigListDt As DataTable = Nothing
            Public CallerAuthenticationNeeded As Boolean = False
            Public SearchCriteriaDt As DataTable
            Public PreviousCompanyId As Guid = Guid.Empty
            Public PreviousDealerId As Guid = Guid.Empty
            Public ShowAdditionalSearchFields As Boolean = False
            Public Dob As String = String.Empty
            Sub New()
            End Sub
        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        'Protected Shadows ReadOnly Property State() As MyState
        '    Get
        '        Return CType(MyBase.State, MyState)
        '    End Get
        'End Property

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Dim retState As MyState
                'Return CType(MyBase.State, MyState)
                If NavController Is Nothing Then
                    'Restart flow
                    StartNavControl()
                    NavController.State = CType(Session(BackupStateSessionKey), MyState)
                ElseIf NavController.State Is Nothing Then
                    NavController.State = New MyState
                ElseIf (Me.GetType.BaseType.FullName <> NavController.State.GetType.ReflectedType.FullName) Then
                    'Restart flow
                    StartNavControl()
                    NavController.State = CType(Session(BackupStateSessionKey), MyState)
                Else
                    If NavController.IsFlowEnded Then
                        'restart flow
                        Dim s As MyState = CType(NavController.State, MyState)
                        StartNavControl()
                        NavController.State = s
                    End If
                End If
                retState = CType(NavController.State, MyState)
                Session(BackupStateSessionKey) = retState
                Return retState
            End Get
        End Property

#End Region



#Region "Navigation Handling"
        Public Sub Process(ByVal callingPage As Page, ByVal navCtrl As INavigationController) Implements IStateController.Process
            Try
                If Not IsPostBack AndAlso navCtrl.CurrentFlow.Name = FlowName AndAlso
                   Not navCtrl.PrevNavState Is Nothing Then
                    _isReturningFromChild = True
                    If navCtrl.IsFlowEnded Then
                        State.SearchDv = Nothing 'This will force a reload
                        'restart the flow
                        Dim savedState As MyState = CType(navCtrl.State, MyState)
                        StartNavControl()
                        NavController.State = savedState
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "State Controller"

        Private Const FlowName As String = "AUTHORIZE_AGENT_PENDING_CLAIM"
        Private Sub StartNavControl()
            Dim nav As New ElitaPlusNavigation
            Me.NavController = New NavControllerBase(nav.Flow(FlowName))
            Me.NavController.State = New MyState
        End Sub
#End Region
#Region "Page Return Type"

        Public Class ReturnType
            Public LastOperation As DetailPageCommand
            Public EditingBo As CaseBase
            Public BoChanged As Boolean = False
            Public IsCallerAuthenticated As Boolean = False

            Public Sub New(ByVal lastOp As DetailPageCommand, ByVal curEditingBo As CaseBase, Optional ByVal boChanged As Boolean = False, Optional ByVal IsCallerAuthenticated As Boolean = False)
                LastOperation = lastOp
                Me.EditingBo = curEditingBo
                Me.BoChanged = boChanged
                Me.IsCallerAuthenticated = IsCallerAuthenticated
            End Sub
        End Class

#End Region

#Region "Page Event"
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

            MasterPage.MessageController.Clear()
            Form.DefaultButton = btnSearch.UniqueID
            Try
                If Not IsPostBack Then
                    UpdateBreadCrum()
                    PopulateSearchDropDownControls()
                    PopulateUserPermission()
                    If Authentication.CurrentUser.IsDealer Then
                        State.DealerId = Authentication.CurrentUser.ScDealerId
                        ControlMgr.SetEnableControl(Me, ddlCompany, False)
                        ControlMgr.SetEnableControl(Me, ddlDealer, False)
                    End If

                    GetStateFieldsValueIntoControl()

                    If _isReturningFromChild Then
                        ' It is returning from detail

                        PopulateSearchResultInRepeater()
                    End If

                    SetFocus(ddlCompany)
                    GetDynamicSearchCriteria()

                Else
                    DisplayDynamicSearchCriteria()
                End If
                ShowHideFields()
                DisplayNewProgressBarOnClick(btnSearch, "Loading_Agent")
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub

        Private Sub Page_PageReturn(ByVal returnFromUrl As String, ByVal returnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                _isReturningFromChild = True
                'Reset the Autentication mode to False.
                Me.State.IsCallerAuthenticated = False
                If Not returnPar Is Nothing AndAlso returnPar.GetType() Is GetType(CaseRecordingForm.ReturnType) Then
                    Dim returnParInstance As CaseRecordingForm.ReturnType = CType(returnPar, CaseRecordingForm.ReturnType)
                    ' Me.State.IsCallerAuthenticated = returnParInstance.IsCallerAuthenticated
                End If

                If Not returnPar Is Nothing AndAlso returnPar.GetType() Is GetType(ClaimForm.ReturnType) Then
                    Dim returnParInstance As ClaimForm.ReturnType = CType(returnPar, ClaimForm.ReturnType)
                    'Me.State.IsCallerAuthenticated = returnParInstance.IsCallerAuthenticated
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region
#Region "Other Functions"
        Private Sub UpdateBreadCrum()
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                                   TranslationBase.TranslateLabelOrMessage("AGENT_SEARCH")
        End Sub
        Protected Sub ClearStateFieldsValue()
            'clear State
            State.CompanyId = Nothing
            State.DealerId = Nothing
            State.CertificateStatus = String.Empty
            State.CaseNumber = String.Empty
            State.CertificateNumber = String.Empty
            State.ClaimNumber = String.Empty
            State.CustomerFirstName = String.Empty
            State.CustomerLastName = String.Empty
            State.PhoneNumber = String.Empty
            State.InvoiceNumber = String.Empty
            State.SerialNumber = String.Empty
            State.Email = String.Empty
            State.TaxId = String.Empty
            State.ServiceLineNumber = String.Empty
            State.AccountNumber = String.Empty
            State.GlobalCustomerNumber = String.Empty
        End Sub
        Private Sub SetStateFieldsValue()

            If State Is Nothing Then
                Trace(Me, "Restoring State")
                RestoreState(New MyState)
            End If

            ClearStateFieldsValue()

            State.DealerId = GetSelectedItem(ddlDealer)
            State.CompanyId = GetSelectedItem(ddlCompany)


            ' Dynamic controls - text box
            State.CertificateNumber = GetSearchTextBoxValue(CodeSearchFieldCertificateNumber)
            State.ClaimNumber = GetSearchTextBoxValue(CodeSearchFieldClaimNumber)
            State.CaseNumber = GetSearchTextBoxValue(CodeSearchFieldCaseNumber)
            State.CustomerFirstName = GetSearchTextBoxValue(CodeSearchFieldCustomerFirstName)
            State.CustomerLastName = GetSearchTextBoxValue(CodeSearchFieldCustomerLastName)
            State.Zipcode = GetSearchTextBoxValue(CodeSearchFieldZip)
            State.PhoneNumber = GetSearchTextBoxValue(CodeSearchFieldPhoneNumber)
            State.InvoiceNumber = GetSearchTextBoxValue(CodeSearchFieldInvoiceNumber)
            State.Email = GetSearchTextBoxValue(CodeSearchFieldEmail)
            State.SerialNumber = GetSearchTextBoxValue(CodeSearchFieldSerialImeiNumber)
            State.TaxId = GetSearchTextBoxValue(CodeSearchFieldTaxId)
            State.ServiceLineNumber = GetSearchTextBoxValue(CodeSearchFieldServiceLineNumber)
            State.AccountNumber = GetSearchTextBoxValue(CodeSearchFieldAccountNumber)
            State.GlobalCustomerNumber = GetSearchTextBoxValue(CodeSearchFieldGlobalCustomerNumber)

            ' Dynamic controls - drop down
            State.CertificateStatus = GetSearchDropDownValue(CodeSearchFieldCertificateStatus)
            State.ShowAdditionalSearchFields = checkboxAdditionalSearchCriteria.Checked

        End Sub

        Protected Sub SetSearchSettingToDefault(Optional ByVal setCompanyDealerValue As Boolean = False)

            SetCompanyDealerDropdown(setCompanyDealerValue)

            'Clear the text box
            ClearSearchTextBox(CodeSearchFieldCertificateNumber)
            ClearSearchTextBox(CodeSearchFieldClaimNumber)
            ClearSearchTextBox(CodeSearchFieldCaseNumber)
            ClearSearchTextBox(CodeSearchFieldCustomerFirstName)
            ClearSearchTextBox(CodeSearchFieldCustomerLastName)
            ClearSearchTextBox(CodeSearchFieldZip)
            ClearSearchTextBox(CodeSearchFieldPhoneNumber)
            ClearSearchTextBox(CodeSearchFieldInvoiceNumber)
            ClearSearchTextBox(CodeSearchFieldEmail)
            ClearSearchTextBox(CodeSearchFieldSerialImeiNumber)
            ClearSearchTextBox(CodeSearchFieldTaxId)
            ClearSearchTextBox(CodeSearchFieldServiceLineNumber)
            ClearSearchTextBox(CodeSearchFieldAccountNumber)
            ClearSearchTextBox(CodeSearchFieldGlobalCustomerNumber)
            ClearSearchTextBox(CodeSearchFieldDob)

            ' Reset drop down
            ResetSearchDropDown(CodeSearchFieldCertificateStatus)

            ResetSearchResult()

            checkboxAdditionalSearchCriteria.Checked = False
        End Sub
        Protected Sub SetCompanyDealerDropdown(ByVal setCompanyDealerValue As Boolean)
            If Authentication.CurrentUser.IsDealer OrElse setCompanyDealerValue = True Then
                If Not State.CompanyId.Equals(Guid.Empty) And ddlCompany.Items.Count > 0 Then SetSelectedItem(ddlCompany, State.CompanyId)
                If Not State.DealerId.Equals(Guid.Empty) And ddlDealer.Items.Count > 0 Then SetSelectedItem(ddlDealer, State.DealerId)
            Else
                ddlCompany.SelectedIndex = DefaultItem
                ddlDealer.SelectedIndex = DefaultItem
            End If
        End Sub
        Private Sub GetStateFieldsValueIntoControl()
            If State.CompanyId <> Guid.Empty And ddlCompany.Items.Count > 0 Then SetSelectedItem(ddlCompany, State.CompanyId)
            If State.DealerId <> Guid.Empty And ddlDealer.Items.Count > 0 Then SetSelectedItem(ddlDealer, State.DealerId)

            ' Dynamic controls - Text Box
            SetSearchTextBox(CodeSearchFieldCertificateNumber, State.CertificateNumber)
            SetSearchTextBox(CodeSearchFieldClaimNumber, State.ClaimNumber)
            SetSearchTextBox(CodeSearchFieldCaseNumber, State.CaseNumber)
            SetSearchTextBox(CodeSearchFieldCustomerFirstName, State.CustomerFirstName)
            SetSearchTextBox(CodeSearchFieldCustomerLastName, State.CustomerLastName)
            SetSearchTextBox(CodeSearchFieldZip, State.Zipcode)
            SetSearchTextBox(CodeSearchFieldPhoneNumber, State.PhoneNumber)
            SetSearchTextBox(CodeSearchFieldInvoiceNumber, State.InvoiceNumber)
            SetSearchTextBox(CodeSearchFieldEmail, State.Email)
            SetSearchTextBox(CodeSearchFieldSerialImeiNumber, State.SerialNumber)
            SetSearchTextBox(CodeSearchFieldTaxId, State.TaxId)
            SetSearchTextBox(CodeSearchFieldServiceLineNumber, State.ServiceLineNumber)
            SetSearchTextBox(CodeSearchFieldAccountNumber, State.AccountNumber)
            SetSearchTextBox(CodeSearchFieldGlobalCustomerNumber, State.GlobalCustomerNumber)
            SetSearchTextBox(CodeSearchFieldDob, State.Dob)

            ' Dynamic controls - Drop down
            SetSearchDropDown(CodeSearchFieldCertificateStatus, State.CertificateStatus)

            checkboxAdditionalSearchCriteria.Checked = State.ShowAdditionalSearchFields
        End Sub

        Private Sub ShowHideFields()
            If Not Me.State.CompanyId.IsEmpty Then
                Dim countryCode As String = String.Empty
                countryCode = BusinessObjectsNew.Claim.GetCountryCodeOverwrite(Me.State.CompanyId)
                'temp solution for now but its need to change
                If (countryCode = "JP") Then
                    ControlMgr.SetVisibleControl(Me, checkboxAdditionalSearchCriteria, False)
                End If
            End If
        End Sub
        Public Function GetCountryCode() As String
            Dim countryCode As String = String.Empty
            If Not Me.State.CompanyId.IsEmpty Then
                countryCode = BusinessObjectsNew.Claim.GetCountryCodeOverwrite(Me.State.CompanyId)
            Else
                Return countryCode
            End If
        End Function
        Private Sub GetDynamicSearchCriteria()
            If Not (State.CompanyId.Equals(State.PreviousCompanyId) And State.DealerId.Equals(State.PreviousDealerId)) Then
                'Get all Search Criteria for the company and dealer
                Dim dv As DataView = SearchConfigAssignment.GetDynamicSearchCriteriaFields(State.CompanyId, State.DealerId, Authentication.CurrentUser.LanguageCode, "AGENT_SEARCH")
                State.SearchCriteriaDt = dv.Table
                PopulateSearchConfigList()
                State.PreviousCompanyId = State.CompanyId
                State.PreviousDealerId = State.DealerId
            End If
            DisplayDynamicSearchCriteria()
        End Sub
        Private Sub DisplayDynamicSearchCriteria()
            If State.SearchCriteriaDt IsNot Nothing AndAlso State.SearchCriteriaDt.Rows.Count > 0 Then
                Dim foundRows() As DataRow
                PanelHolderDynamicSearchCriteria.Controls.Clear()
                ' Presuming the DataTable has a column named Date.
                Dim expression As String = "search_type = 1"

                If State.ShowAdditionalSearchFields Then
                    expression = "search_type = 1 or search_type = 2"
                End If
                foundRows = State.SearchCriteriaDt.Select(expression)

                If foundRows.Length > 0 Then
                    GenerateSearchCriteriaFields(foundRows)
                End If
                ActivateCalendarClick()
            End If
        End Sub

#End Region

#Region "Event handlers - Button, Dropdown, CheckBox"
        Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSearch.Click
            Try
                SetStateFieldsValue()
                State.IsGridVisible = True
                State.SearchDv = Nothing
                PopulateSearchResultInRepeater()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
            Try
                ' Clear all search options typed or selected by the user
                SetSearchSettingToDefault()

                SetStateFieldsValue()

                GetDynamicSearchCriteria()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub btnClassicView_Click(sender As Object, e As EventArgs) Handles btnClassicView.Click
            Redirect(CaseSearchForm.Url)
        End Sub
        Protected Sub ddlDealer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlDealer.SelectedIndexChanged
            Try
                State.DealerId = GetSelectedItem(ddlDealer)
                SetSearchSettingToDefault(True)
                SetStateFieldsValue()
                PopulateExclSecFields()
                GetDynamicSearchCriteria()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub ddlCompany_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompany.SelectedIndexChanged
            Try
                State.CompanyId = GetSelectedItem(ddlCompany)
                SetSearchSettingToDefault(True)
                GetDynamicSearchCriteria()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
        Protected Sub checkboxAdditionalSearchCriteria_CheckedChanged(sender As Object, e As EventArgs) Handles checkboxAdditionalSearchCriteria.CheckedChanged
            Try
                State.ShowAdditionalSearchFields = checkboxAdditionalSearchCriteria.Checked
                SetStateFieldsValue()
                DisplayDynamicSearchCriteria()
                GetStateFieldsValueIntoControl()
                ResetSearchResult()
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region

#Region "Functions - Populate DropDowns"
        Private Sub PopulateSearchDropDownControls()
            'populate company list
            PopulateCompanyDropdown()

            'populate dealer list
            PopulateDealerDropdown()
        End Sub
        Private Sub PopulateDealerDropdown()
            Dim oDealerList
            If Authentication.CurrentUser.IsDealerGroup Then
                oDealerList = CaseBase.GetDealerListByCompanyForExternalUser()
            Else
                oDealerList = GetDealerListByCompanyForUser()
            End If

            Dim dealerTextFunc As Func(Of ListItem, String) = Function(li As ListItem)
                                                                  Return li.Translation + OneSpace + "(" + li.Code + ")"
                                                              End Function
            ddlDealer.Populate(oDealerList, New PopulateOptions() With
                                  {
                                  .AddBlankItem = True,
                                  .TextFunc = dealerTextFunc
                                  })


            If State.DealerId <> Guid.Empty Then
                SetSelectedItem(ddlDealer, State.DealerId)
            End If
        End Sub
        Private Sub PopulateCompanyDropdown()
            Dim companyList As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Company")

            Dim filteredCompanyList As ListItem() = (From x In companyList
                                                     Where Authentication.CurrentUser.Companies.Contains(x.ListItemId)
                                                     Select x).ToArray()

            Dim companyTextFunc As Func(Of ListItem, String) = Function(li As ListItem)
                                                                   Return li.Translation + OneSpace + "(" + li.Code + ")"
                                                               End Function

            ddlCompany.Populate(filteredCompanyList, New PopulateOptions() With
                                   {
                                   .AddBlankItem = False,
                                   .TextFunc = companyTextFunc
                                   })

            If State IsNot Nothing AndAlso State.CompanyId <> Guid.Empty Then
                SetSelectedItem(ddlCompany, State.CompanyId)
            Else
                ddlCompany.SelectedIndex = DefaultItem
                State.CompanyId = GetSelectedItem(ddlCompany)
            End If
        End Sub
        Private Sub PopulateUserPermission()
            Dim oUser As User = New User()
            State.CallerAuthenticationNeeded = oUser.NeedPERMtoViewPrivacyData()
        End Sub

        Private Sub PopulateExclSecFields()
            Try

                If Not (State.CompanyId.Equals(State.PreviousCompanyId) And State.DealerId.Equals(State.PreviousDealerId)) Then
                    Dim exclSecFieldsDt As DataTable
                    Dim objList As List(Of CaseBase.ExclSecFields)

                    ' If State.ExclSecFieldsDt Is Nothing Then
                    objList = CaseBase.LoadExclSecFieldsConfig(Guid.Empty, State.DealerId)
                    If objList.Count > 0 Then
                        State.ExclSecFieldsDt = ConvertToDataTable(Of CaseBase.ExclSecFields)(objList)
                    End If
                    'End If
                End If


            Catch ex As ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateSearchConfigList()
            If Not (State.CompanyId.Equals(State.PreviousCompanyId) And State.DealerId.Equals(State.PreviousDealerId)) Then
                Dim dsResults As DataSet
                dsResults = CaseBase.GetAgentSearchConfigList(State.CompanyId, State.DealerId, SearchTypeXCD)
                If dsResults.Tables(0).Rows.Count > 0 Then
                    State.SearchResultsConfigListDt = dsResults.Tables(0)
                End If
            End If
        End Sub

#End Region
#Region "Helper functions"

        Private Function CreateDropdownField(ByVal dropdownListName As String) As DropDownList
            Dim ddl As New DropDownList
            ddl.ID = dropdownListName
            ddl.AutoPostBack = False
            ddl.SkinID = "SmallDropDown"
            Dim lookuplist As ListItem() = CommonConfigManager.Current.ListManager.GetList(dropdownListName, Authentication.CurrentUser.LanguageCode)
            ddl.Populate(lookuplist, New PopulateOptions() With
                            {
                            .AddBlankItem = True,
                            .BlankItemValue = "",
                            .ValueFunc = AddressOf PopulateOptions.GetCode
                            })
            Return ddl
        End Function
        Private Function CreateTextBoxField(ByVal textBoxName As String) As TextBox
            Dim txt As New TextBox
            txt.ID = textBoxName
            txt.SkinID = "MediumTextBox"
            txt.AutoPostBack = False
            Return txt
        End Function
        Private Function CreateCalendarControlField(ByVal calControl As String) As htmlControlsObj
            Dim txt As New TextBox
            txt.ID = calControl
            txt.SkinID = "MediumTextBox"
            txt.AutoPostBack = False

            Dim btnimg As New ImageButton
            btnimg.ID = calControl + "BTN"
            btnimg.ImageUrl = "~/App_Themes/Default/Images/calendar.png"

            Dim objcontrol As New htmlControlsObj
            objcontrol.imgbtn = btnimg
            objcontrol.txtboxcontrl = txt
            Return objcontrol
        End Function

        'Private Function CreateImageButtonField(ByVal imageButtonName As String) As ImageButton

        '    Dim btnimg As New ImageButton
        '    btnimg.ID = "imageButtonName"
        '    btnimg.ImageUrl = "~/App_Themes/Default/Images/calendar.png"
        '    Dim txt5 As TextBox = TryCast(PanelHolderDynamicSearchCriteria.FindControl("TAX_ID"), TextBox)
        '    'Me.AddCalendar_New(btnimg, txt5)
        '    Return btnimg
        'End Function
        Private Sub GenerateSearchCriteriaFields(ByVal foundRows() As DataRow)
            If foundRows.Length > 0 Then
                ' Dim placeHolderDynamic As PlaceHolder = Page.FindControl(placeholdername)
                Dim tr As HtmlGenericControl
                Dim isTrAdded As Boolean = True
                For i As Integer = 1 To foundRows.Length
                    Dim td As New HtmlGenericControl("td")
                    Dim fieldName As String = foundRows(i - 1)("field_name").ToString()
                    Dim fieldCode As String = foundRows(i - 1)("field_code").ToString()

                    ' add label
                    Dim lbl As New Label
                    lbl.ID = "lbl" + fieldCode
                    lbl.Text = fieldName
                    td.Controls.Add(lbl)

                    ' add break
                    'Dim br As New HtmlGenericControl("br")
                    'td.Controls.Add(Page.ParseControl("br"))

                    td.Controls.Add(New LiteralControl("</br>"))

                    ' add Text box
                    Dim fieldType As String = foundRows(i - 1)("field_type").ToString()
                    Select Case fieldType.Trim().ToLower()
                        Case "textbox"
                            td.Controls.Add(CreateTextBoxField(fieldCode))
                        Case "dropdownlist"
                            td.Controls.Add(CreateDropdownField(fieldCode))
                        Case "calendar"
                            Dim obj As htmlControlsObj
                            obj = CreateCalendarControlField(fieldCode)
                            td.Controls.Add(obj.txtboxcontrl)
                            tr.Controls.Add(td)
                            td.Controls.Add(New LiteralControl("&nbsp"))
                            td.Controls.Add(obj.imgbtn)

                        Case Else
                            Throw New NotSupportedException(fieldType.Trim().ToUpper())
                    End Select

                    If i Mod 3 = 1 Then
                        tr = New HtmlGenericControl("tr")
                        tr.Controls.Add(td)
                        isTrAdded = False
                    Else
                        tr.Controls.Add(td)
                        PanelHolderDynamicSearchCriteria.Controls.Add(tr)
                        isTrAdded = True
                    End If
                Next

                If isTrAdded = False Then
                    PanelHolderDynamicSearchCriteria.Controls.Add(tr)
                End If
            End If
        End Sub
        Private Function GetDealerListByCompanyForUser() As ListItem()
            Dim index As Integer
            Dim oListContext As New ListContext

            Dim userCompanies As ArrayList = Authentication.CurrentUser.Companies

            Dim oDealerList As New Collections.Generic.List(Of ListItem)

            For index = 0 To userCompanies.Count - 1
                'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
                oListContext.CompanyId = userCompanies(index)
                Dim oDealerListForCompany As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", context:=oListContext)
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
        Private Sub ActivateCalendarClick()
            Dim txtCtl As TextBox = TryCast(PanelHolderDynamicSearchCriteria.FindControl(CodeSearchFieldDob), TextBox)
            Dim btnImg As ImageButton = TryCast(PanelHolderDynamicSearchCriteria.FindControl(CodeSearchFieldDob + "BTN"), ImageButton)
            If (Not txtCtl Is Nothing And Not btnImg Is Nothing) Then
                Me.AddCalendar_New(btnImg, txtCtl)
            End If
        End Sub
        Private Function GetSearchTextBoxValue(ByVal textboxName As String) As String
            Dim txt As TextBox = TryCast(PanelHolderDynamicSearchCriteria.FindControl(textboxName), TextBox)
            If txt IsNot Nothing Then
                Return txt.Text.ToUpper.Trim
            Else
                Return String.Empty
            End If
        End Function
        Private Sub SetSearchTextBox(ByVal textboxName As String, ByVal textboxValue As String)
            Dim txt As TextBox = TryCast(PanelHolderDynamicSearchCriteria.FindControl(textboxName), TextBox)
            If txt IsNot Nothing Then
                txt.Text = textboxValue
            End If
        End Sub
        Private Sub ClearSearchTextBox(ByVal textboxName As String)
            Dim txt As TextBox = TryCast(PanelHolderDynamicSearchCriteria.FindControl(textboxName), TextBox)
            If txt IsNot Nothing Then
                txt.Text = String.Empty
            End If
        End Sub

        Private Function GetSearchDropDownValue(ByVal dropdownName As String) As String
            Dim ddl As DropDownList = TryCast(PanelHolderDynamicSearchCriteria.FindControl(dropdownName), DropDownList)
            If ddl IsNot Nothing Then
                Return GetSelectedValue(ddl)
            Else
                Return String.Empty
            End If
        End Function
        Private Sub SetSearchDropDown(ByVal dropdownName As String, ByVal selectedValue As String)
            Dim ddl As DropDownList = TryCast(PanelHolderDynamicSearchCriteria.FindControl(dropdownName), DropDownList)
            If ddl IsNot Nothing AndAlso ddl.Items.Count > 0 Then
                SetSelectedItem(ddl, selectedValue)
            End If
        End Sub
        Private Sub ResetSearchDropDown(ByVal dropdownName As String)
            Dim ddl As DropDownList = TryCast(PanelHolderDynamicSearchCriteria.FindControl(dropdownName), DropDownList)
            If ddl IsNot Nothing AndAlso ddl.Items.Count > 0 Then
                ddl.SelectedIndex = DefaultItem
            End If
        End Sub

#End Region

#Region " Search Result - Function"
        Private Sub ResetSearchResult()
            repeaterSearchResult.DataSource = Nothing
            repeaterSearchResult.DataBind()
            State.SearchDv = Nothing
            Session("recCount") = 0
            lblRecordCount.Text = ""
            ControlMgr.SetVisibleControl(Me, repeaterSearchResult, False)
            ControlMgr.SetVisibleControl(Me, trPageSize, repeaterSearchResult.Visible)
        End Sub
        Private Sub PopulateSearchResultInRepeater()
            Try
                If (State.SearchDv Is Nothing) Then
                    State.SearchDv = CaseBase.GetAgentList(State.CompanyId,
                                                           State.DealerId,
                                                           State.CustomerFirstName,
                                                           State.CustomerLastName,
                                                           State.CaseNumber,
                                                           State.ClaimNumber,
                                                           State.CertificateNumber,
                                                           State.SerialNumber,
                                                           State.InvoiceNumber,
                                                           State.PhoneNumber,
                                                           State.Zipcode,
                                                           State.CertificateStatus,
                                                           State.Email,
                                                           State.TaxId,
                                                           State.ServiceLineNumber,
                                                           State.AccountNumber,
                                                           State.GlobalCustomerNumber,
                                                           State.Dob,
                                                           Authentication.CurrentUser.LanguageId)

                    ValidSearchResultCountNew(State.SearchDv.Count, True)


                    If Not State.SearchDv Is Nothing Then

                        State.SearchDv.Table.Columns.Add("LOSS_DATE_FORMAT", GetType(String))
                        State.SearchDv.Table.Columns.Add("REPORTED_DATE_FORMAT", GetType(String))
                        State.SearchDv.Table.Columns.Add("CASE_OPEN_DATE_FORMAT", GetType(String))
                        State.SearchDv.Table.Columns.Add("WARRANTY_SALES_DATE_FORMAT", GetType(String))
                        State.SearchDv.Table.Columns.Add("PRODUCT_SALES_DATE_FORMAT", GetType(String))

                        For Each dr As DataRow In State.SearchDv.Table.Rows
                            If Not dr("LOSS_DATE") Is DBNull.Value Then
                                dr("LOSS_DATE_FORMAT") = GetDateFormattedStringNullable(CType(dr("LOSS_DATE"), DateTime))
                            End If
                            If Not dr("REPORTED_DATE") Is DBNull.Value Then
                                dr("REPORTED_DATE_FORMAT") = GetDateFormattedStringNullable(CType(dr("REPORTED_DATE"), DateTime))
                            End If
                            If Not dr("CASE_OPEN_DATE") Is DBNull.Value Then
                                dr("CASE_OPEN_DATE_FORMAT") = GetDateFormattedStringNullable(CType(dr("CASE_OPEN_DATE"), DateTime))
                            End If
                            If Not dr("WARRANTY_SALES_DATE") Is DBNull.Value Then
                                dr("WARRANTY_SALES_DATE_FORMAT") = GetDateFormattedStringNullable(CType(dr("WARRANTY_SALES_DATE"), DateTime))
                            End If
                            If Not dr("PRODUCT_SALES_DATE") Is DBNull.Value Then
                                dr("PRODUCT_SALES_DATE_FORMAT") = GetDateFormattedStringNullable(CType(dr("PRODUCT_SALES_DATE"), DateTime))
                            End If
                        Next

                        If Not State.ExclSecFieldsDt Is Nothing AndAlso State.CallerAuthenticationNeeded Then
                            For Each col As DataColumn In State.SearchDv.Table.Columns
                                For Each drSecrow As DataRow In State.ExclSecFieldsDt.Rows
                                    If col.ColumnName.ToString().ToUpper().Contains(drSecrow("Column_Name").ToString().ToUpper()) _
                                       AndAlso Not State.IsCallerAuthenticated Then
                                        'AndAlso ((drSecrow("Table_Name").ToString().ToUpper() = "ELP_CERT") OrElse drSecrow("Table_Name").ToString().ToUpper() ="ELP_ADDRESS" OrElse drSecrow("Table_Name").ToString().ToUpper() = "ELP_CALLER") then
                                        State.SearchDv.Table.Columns(col.ColumnName).Expression = String.Empty
                                    End If
                                Next
                            Next
                        End If
                    End If

                End If

                repeaterSearchResult.DataSource = State.SearchDv
                repeaterSearchResult.DataBind()

                ControlMgr.SetVisibleControl(Me, repeaterSearchResult, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, repeaterSearchResult.Visible)
                Session("recCount") = State.SearchDv.Count

                If repeaterSearchResult.Visible Then
                    lblRecordCount.Text = State.SearchDv.Count & OneSpace & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If

            Catch ex As Exception
                Dim exceptionType As String = ex.GetBaseException.GetType().Name
                If ((Not exceptionType.Equals(String.Empty)) And exceptionType.Equals("BOValidationException")) Then
                    ControlMgr.SetVisibleControl(Me, repeaterSearchResult, False)
                    lblRecordCount.Text = ""
                End If
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Protected Sub repeaterSearchResult_OnItemDataBound(sender As Object, e As RepeaterItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim isRestricted As String = String.Empty
                Dim lblStatus As WebControls.Label = DirectCast(e.Item.FindControl("lblStatus"), WebControls.Label)
                Dim xmlSource As Xml = DirectCast(e.Item.FindControl("Xmlsource"), Xml)

                Dim statusCode As Object = DataBinder.Eval(e.Item.DataItem, "Status_Code")
                Dim itemtype As Object = DataBinder.Eval(e.Item.DataItem, "item_type")
                Dim btnCancelReqAgent As LinkButton = DirectCast(e.Item.FindControl("btnCancelReqAgent"), LinkButton)
                Dim cId As Object = DataBinder.Eval(e.Item.DataItem, "entity_id")
                Dim certId As Object = DataBinder.Eval(e.Item.DataItem, "cert_id")
                Dim btnEditCase As LinkButton = DirectCast(e.Item.FindControl("btnEditCase"), LinkButton)
                Dim btnEditCert As LinkButton = DirectCast(e.Item.FindControl("btnEditCert"), LinkButton)

                Dim btnNewCase As LinkButton = DirectCast(e.Item.FindControl("LinkButtonNewCase"), LinkButton)
                btnNewCase.Text = TranslationBase.TranslateLabelOrMessage("DCM_NEW_CASE")

                Dim btnEditAgent As LinkButton = DirectCast(e.Item.FindControl("btnEditAgent"), LinkButton)
                btnEditAgent.CommandArgument = GetGuidStringFromByteArray(CType(cId, Byte()))
                btnEditAgent.CommandName = SelectActionCommand

                Dim row As DataRowView = CType(e.Item.DataItem, DataRowView)
                Dim ds As DataSet = New DataSet("AgentSearchResults")
                ds.Tables.Add(row.Row.Table.Clone())
                ds.Tables(0).TableName = "Results"
                ds.Tables("Results").ImportRow(CType(row.Row, DataRow))
                Dim xmlStr As String = ds.GetXml()
                Dim xdoc As New System.Xml.XmlDocument()
                Dim trans As XslTransform = New XslTransform()
                Dim srConfigRow As DataRow()

                If DataBinder.Eval(e.Item.DataItem, "is_restricted") IsNot DBNull.Value Then
                    isRestricted = DataBinder.Eval(e.Item.DataItem, "is_restricted")
                End If

                If (itemtype.ToString = "Cert") Then

                    xdoc.LoadXml(ds.GetXml())
                    srConfigRow = State.SearchResultsConfigListDt.Select("item_type_xcd = 'ITEM_TYPE-CERT'")
                    trans.Load(XmlReader.Create(New StringReader(srConfigRow(0)("search_config_data").ToString())))
                    xmlSource.Transform = trans
                    xmlSource.Document = xdoc

                    Dim certAcceptedRequest As Integer = 0
                    lblStatus.Text = LookupListNew.GetDescriptionFromCode("CSTAT", statusCode.ToString, Authentication.CurrentUser.LanguageId)
                    btnEditAgent.Text = TranslationBase.TranslateLabelOrMessage("DCM_NEW_CLAIM")

                    If isRestricted = Codes.ENTITY_IS__RESTRICTED Then
                        btnEditAgent.Visible = False
                    Else
                        btnEditAgent.Visible = True
                    End If
                    'Display the Cancel Request button only if the Cancel Policy Question Set Code attrbute is configured in the Product Code
                    Dim certificateBo As Certificate = Nothing
                    certificateBo = New Certificate(New Guid(CType(certId, Byte())))
                    Dim cancelQuestionSetCode As String
                    cancelQuestionSetCode = CaseBase.GetQuestionSetCode(Guid.Empty, Guid.Empty, Guid.Empty, Guid.Empty, certificateBo.Product.Id, Guid.Empty,
                                                                        Guid.Empty, Guid.Empty, Guid.Empty, PurposeCancelCertRequest)

                    If (Not certificateBo Is Nothing AndAlso certificateBo.StatusCode <> Codes.CERTIFICATE_STATUS__CANCELLED _
                        AndAlso Not String.IsNullOrEmpty(cancelQuestionSetCode)) Then
                        ' check for any existing Accepted Cancel Requests                        
                        Dim dsRequests As DataSet = CertCancelRequest.GetCertCancelRequestData(certificateBo.Id)
                        If (Not dsRequests Is Nothing AndAlso dsRequests.Tables.Count > 0 AndAlso dsRequests.Tables(0).Rows.Count > 0) Then
                            If (dsRequests.Tables(0).Select("status_description = 'Accepted'").Count > 0) Then
                                certAcceptedRequest += 1
                            End If
                        End If
                        'Display the Cancel link when the Certificate is active andalso no "Accepted" cancellation request 
                        If (certAcceptedRequest = 0) Then
                            'andalso not String.IsNullOrWhiteSpace(strQuestionSetCode)) Then

                            btnCancelReqAgent.Text = TranslationBase.TranslateLabelOrMessage("DCM_CERT_CANCEL")
                            btnCancelReqAgent.CommandArgument = GetGuidStringFromByteArray(CType(cId, Byte()))
                            btnCancelReqAgent.CommandName = SelectActionCancelCert

                        End If
                    End If

                    btnEditCert.Visible = Not State.CallerAuthenticationNeeded
                    btnEditCert.Text = TranslationBase.TranslateLabelOrMessage("VIEW")
                    btnEditCert.CommandArgument = GetGuidStringFromByteArray(CType(cId, Byte()))
                    btnEditCert.CommandName = SelectActionCommandCert

                    btnNewCase.Visible = True
                    btnNewCase.CommandArgument = GetGuidStringFromByteArray(CType(cId, Byte()))
                    btnNewCase.CommandName = ActionCommandNewCaseCertificate

                ElseIf (itemtype.ToString = "Claim") Then

                    xdoc.LoadXml(ds.GetXml())
                    srConfigRow = State.SearchResultsConfigListDt.Select("item_type_xcd = 'ITEM_TYPE-CLAIM'")
                    trans.Load(XmlReader.Create(New StringReader(srConfigRow(0)("search_config_data").ToString())))
                    xmlSource.Transform = trans
                    xmlSource.Document = xdoc

                    lblStatus.Text = LookupListNew.GetDescriptionFromCode("CLSTAT", statusCode.ToString, Authentication.CurrentUser.LanguageId)
                    btnEditAgent.Visible = Not State.CallerAuthenticationNeeded
                    btnEditAgent.Text = TranslationBase.TranslateLabelOrMessage("DCM_VIEW_CLAIM")
                    btnNewCase.Visible = True
                    btnNewCase.CommandArgument = GetGuidStringFromByteArray(CType(cId, Byte()))
                    btnNewCase.CommandName = ActionCommandNewCaseClaim

                ElseIf (itemtype.ToString = "Case") Then

                    xdoc.LoadXml(ds.GetXml())
                    srConfigRow = State.SearchResultsConfigListDt.Select("item_type_xcd = 'ITEM_TYPE-CASE'")
                    trans.Load(XmlReader.Create(New StringReader(srConfigRow(0)("search_config_data").ToString())))
                    xmlSource.Transform = trans
                    xmlSource.Document = xdoc

                    'lblStatus.Text = LookupListNew.GetDescriptionFromCode("CASSTAT", Status_Code.ToString, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
                    lblStatus.Text = statusCode.ToString
                    btnEditAgent.Text = TranslationBase.TranslateLabelOrMessage("DCM_RESUME_CLAIM")
                    If DataBinder.Eval(e.Item.DataItem, "Case_Purpose_XCD").ToString.Contains("CASEPUR-INQUIRY") Then
                        btnEditAgent.CommandName = ActionCommandResumeCaseClaim
                    End If

                    btnEditCase.Visible = Not State.CallerAuthenticationNeeded
                    btnEditCase.Text = TranslationBase.TranslateLabelOrMessage("DCM_VIEW_CLAIM")
                    btnEditCase.CommandArgument = GetGuidStringFromByteArray(CType(cId, Byte()))
                    btnEditCase.CommandName = SelectActionCommandCase
                End If

                If (statusCode.ToString = Codes.CERTIFICATE_STATUS__ACTIVE) Then
                    lblStatus.CssClass = "StatActive"
                Else
                    lblStatus.CssClass = "StatInactive"
                End If



            End If
        End Sub

        Protected Sub repeaterSearchResult_OnItemCommand(sender As Object, e As RepeaterCommandEventArgs)
            Try
                If Not e.CommandArgument.ToString().Equals(String.Empty) Then
                    Dim selectedId As Guid = New Guid(e.CommandArgument.ToString())
                    If e.CommandName = SelectActionCommand Or e.CommandName = SelectActionCancelCert Then

                        Dim itemtype As HiddenField = DirectCast(e.Item.FindControl("hfItemType"), HiddenField)
                        If Not itemtype.Value.ToString().Equals(String.Empty) Then
                            If (itemtype.Value.ToString = "Cert") Then
                                callPage(ClaimRecordingForm.Url, New ClaimRecordingForm.Parameters(selectedId, Nothing, Nothing,
                                                                                                   If(e.CommandName = SelectActionCommand, Codes.CASE_PURPOSE__REPORT_CLAIM, Codes.CASE_PURPOSE__CANCELLATION_REQUEST), Me.State.IsCallerAuthenticated))
                            ElseIf (itemtype.Value.ToString = "Claim") Then
                                Dim claimId As Guid
                                claimId = selectedId
                                Dim claimBo As ClaimBase = ClaimFacade.Instance.GetClaim(Of ClaimBase)(claimId)
                                If claimBo.Status = BasicClaimStatus.Pending Then
                                    If (claimBo.ClaimAuthorizationType = ClaimAuthorizationType.Multiple) Then
                                        NavController = Nothing
                                        callPage(ClaimWizardForm.URL, New ClaimWizardForm.Parameters(ClaimWizardForm.ClaimWizardSteps.Step3, Nothing, claimId, Nothing))
                                    Else
                                        If Not State.ExclSecFieldsDt Is Nothing And State.ExclSecFieldsDt.Rows.Count > 0 Then
                                            NavController.FlowSession(FlowSessionKeys.SESSION_CLAIM) = claimBo
                                            NavController.Navigate(Me, FlowEvents.EVENT_CLAIM_SELECTED)
                                        Else
                                            MasterPage.MessageController.AddError(TranslationBase.TranslateLabelOrMessage(ElitaPlus.Common.ErrorCodes.GUI_CASE_MANAGEMENT_WEBAPPGATEWAY_SERVICE_ERR), False)
                                        End If

                                    End If
                                Else
                                    callPage(ClaimForm.URL, New ClaimForm.Parameters(selectedId, State.IsCallerAuthenticated))
                                End If
                            ElseIf (itemtype.Value.ToString = "Case") Then
                                callPage(ClaimRecordingForm.Url, New ClaimRecordingForm.Parameters(Nothing, Nothing, selectedId,, Me.State.IsCallerAuthenticated))
                            End If
                        End If

                    ElseIf e.CommandName = SelectActionCommandCase Then
                        callPage(CaseDetailsForm.Url, New CaseDetailsForm.Parameters(selectedId, State.IsCallerAuthenticated))
                    ElseIf e.CommandName = ActionCommandNewCaseCertificate Then
                        callPage(CaseRecordingForm.Url, New CaseRecordingForm.Parameters(selectedId, CaseRecordingForm.CasePurpose.CertificateInquiry, Me.State.IsCallerAuthenticated))

                    ElseIf e.CommandName = ActionCommandNewCaseClaim Then
                        callPage(CaseRecordingForm.Url, New CaseRecordingForm.Parameters(selectedId, CaseRecordingForm.CasePurpose.ClaimInquiry, Me.State.IsCallerAuthenticated))

                    ElseIf e.CommandName = ActionCommandResumeCaseClaim Then
                        callPage(CaseRecordingForm.Url, New CaseRecordingForm.Parameters(selectedId, CaseRecordingForm.CasePurpose.CaseInquiry, Me.State.IsCallerAuthenticated))

                    ElseIf e.CommandName = SelectActionCommandCert Then
                        callPage(CertificateForm.URL, New CertificateForm.Parameters(selectedId, Me.State.IsCallerAuthenticated))

                    End If
                End If

            Catch ex As ThreadAbortException
            Catch ex As Exception
                If (TypeOf ex Is TargetInvocationException) AndAlso
                   (TypeOf ex.InnerException Is ThreadAbortException) Then Return
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

#End Region
    End Class
    Public Class htmlControlsObj
        Public txtboxcontrl As TextBox
        Public imgbtn As ImageButton
    End Class
End Namespace