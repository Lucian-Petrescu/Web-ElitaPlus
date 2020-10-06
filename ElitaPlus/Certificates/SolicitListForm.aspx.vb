Imports System.Linq
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Imports System.Net.Http
Imports System.Net.Http.Headers
Imports Newtonsoft.Json
Imports System.Xml.Serialization
Imports System.Xml.Xsl
Imports System.IO
Imports System.Collections.Generic


Namespace Certificates
    Partial Public Class SolicitListForm
        Inherits ElitaPlusSearchPage
#Region "Constants"
        ' Public Const URL As String = "Certificates/CertificateListForm.aspx"
        'Public Const PAGETITLE As String = "CERTIFICATE_SEARCH"

        Public Const SEARCH_EXCEPTION As String = "NOTE_SEARCH_CRITERIA_REQUIRED" 'Solicit List Search Exception
        Public Const NO_DEALER_SELECTED = "--"
        Public Const NO_RECORDS_FOUND = "MSG_NO_RECORDS_FOUND"
        Public Const SALES_ORDER_NUMBER = "salesOrderNumber"
        Public Const CUSTOMER_ID = "customerId"
        Public Const APPLY_DATE = "effectiveDate"
        Public Const SIM_PHONE_NUMBER = "phoneNumber"

#End Region
#Region "Page State"
        Private IsReturningFromChild As Boolean = False
        Private Shared searchTypeItems() As String = {"SOLICITNUM"}

        Class MyState

            Public MyBO As Solicit

            Public searchClick As Boolean = False
            Public solicitFoundMSG As String
            Public selectedSolicitId As Guid = Guid.Empty

            Public searchTypes As ArrayList = New ArrayList(searchTypeItems)
            Public ActivePanel As String = CStr(searchTypes.Item(0))

            Public initialSalesOrderNumber As String
            Public customerId As String
            Public dealerId As Guid
            Public dealerName As String = String.Empty
            Public applyDate As String = String.Empty
            Public simPhoneNumber As String = String.Empty
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
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Solicitations")

                    UpdateBreadCrum()

                    'Check for company group code should be ASJP and Company code AJP
                    If Not GetFormattedCompanyCode() = "ASJP-AJP" Then
                        Dim errors() As ValidationError = {New ValidationError(NO_RECORDS_FOUND, GetType(Solicit), Nothing, "Search", Nothing)}
                        Throw New BOValidationException(errors, GetType(Solicit).FullName)
                    End If
                    'TranslateGridHeader(Grid)
                    AddCalendar_New(btnApplyDate, txtApplyDate)

                    GetStateProperties()
                    PopulateSearchControls()

                    If IsReturningFromChild Then
                        ' It is returning from detail
                        PopulateGrid()
                    End If

                    SetFocus(txtInitialSalesOrder)
                    objPrincipal = CType(System.Threading.Thread.CurrentPrincipal, ElitaPlusPrincipal).Identity
                    If objPrincipal.PrivacyUserType = AppConfig.DataProtectionPrivacyLevel.Privacy_DataProtection Then
                        State.isDPO = True
                    End If

                    If ddlDealer.Items.Count = 0 Then
                        ddlDealer.Enabled = False
                        btnSearch.Enabled = False
                        btnClearSearch.Enabled = False
                        Dim errors() As ValidationError = {New ValidationError("No dealers found for Solicitation", GetType(Solicit), Nothing, "Search", Nothing)}
                        Throw New BOValidationException(errors, GetType(Solicit).FullName)
                    Else
                        ddlDealer.Enabled = True
                        btnSearch.Enabled = True
                        btnClearSearch.Enabled = True
                    End If
                End If

                DisplayNewProgressBarOnClick(btnSearch, "LOADING_SOLICITATIONS")
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
            ShowMissingTranslations(MasterPage.MessageController)
        End Sub
        Protected Sub Page_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete

        End Sub

        Private Sub UpdateBreadCrum()
            If (State IsNot Nothing) Then
                If (State IsNot Nothing) Then
                    MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("SOLICITATION_SEARCH")
                    MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("SOLICITATION_SEARCH")
                End If
            End If
        End Sub


        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                Dim retObj As CertificateForm.ReturnType = CType(ReturnPar, CertificateForm.ReturnType)
                If retObj IsNot Nothing AndAlso retObj.BoChanged Then
                    'Me.State.searchDV = Nothing
                End If
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If retObj IsNot Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                ' Me.State.selectedSolicitId = retObj.EditingBo.Id
                            End If

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
                If txtInitialSalesOrder.Text = String.Empty And txtCustomerId.Text = String.Empty And txtApplyDate.Text = String.Empty And
                    txtSimPhoneNumber.Text = String.Empty And ddlDealer.SelectedIndex = &H0 Then

                    MasterPage.MessageController.AddErrorAndShow(SEARCH_EXCEPTION, True)
                    Return
                End If
                If ddlDealer.SelectedIndex = &H0 Or ddlDealer.SelectedValue Is Nothing Then

                    MasterPage.MessageController.AddErrorAndShow("SELECT_DEALER", True)
                    Return
                End If
                Dim selectedDealer As Assurant.Elita.CommonConfiguration.DataElements.ListItem
                If Authentication.CurrentUser.IsDealerGroup Then
                    Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
                    selectedDealer = oDealerList.Where(Function(x) x.ListItemId.ToString() = ddlDealer.SelectedValue).FirstOrDefault()
                Else
                    Dim oDealerList() As Assurant.Elita.CommonConfiguration.DataElements.ListItem = GetDealerListByCompanyForUser()
                    selectedDealer = oDealerList.Where(Function(x) x.ListItemId.ToString() = ddlDealer.SelectedValue).FirstOrDefault()
                End If

                If selectedDealer.Code <> "RSIM" Then
                    MasterPage.MessageController.AddInformation(NO_RECORDS_FOUND, True)
                    Return
                End If

                If ddlDealer.SelectedIndex > 0 Then
                    If (txtInitialSalesOrder.Text = String.Empty AndAlso txtCustomerId.Text = String.Empty AndAlso txtApplyDate.Text = String.Empty AndAlso
                    txtSimPhoneNumber.Text = String.Empty) Then
                        Dim errors() As ValidationError = {New ValidationError("Enter At Least One Search Criterion along with dealer", GetType(Solicit), Nothing, "Search", Nothing)}
                        Throw New BOValidationException(errors, GetType(Solicit).FullName)
                    End If
                End If
                SetStateProperties()
                State.selectedSolicitId = Guid.Empty

                State.searchClick = True
                'Me.State.searchDV = Nothing
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
                State.initialSalesOrderNumber = String.Empty
                State.customerId = String.Empty
                State.simPhoneNumber = String.Empty
                State.dealerId = Nothing
                State.applyDate = String.Empty
                State.dealerName = Nothing

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub ClearAllSearchOptions()
            ' Clear Solicit search panel
            txtInitialSalesOrder.Text = String.Empty
            txtCustomerId.Text = String.Empty
            txtSimPhoneNumber.Text = String.Empty
            txtApplyDate.Text = String.Empty
            If Not ElitaPlusIdentity.Current.ActiveUser.IsDealer Then ddlDealer.SelectedIndex = 0


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

            Try
                'Populate Solicit Search Dropdowns

                PopulateDealerDropdown(ddlDealer, State.dealerId)

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateDealerDropdown(dealerDropDownList As DropDownList, setvalue As Guid)
            Try
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

        Private Function GetFormattedCompanyCode() As String
            Dim UserCompanyGroup As CompanyGroup = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup
            Dim company As Company = ElitaPlusIdentity.Current.ActiveUser.Company
            Return UserCompanyGroup.Code & "-" & company.Code
        End Function

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

        Private Sub GetStateProperties()
            Try

                ' Set state based on panel selected
                If State.ActivePanel.Equals(State.searchTypes.Item(0)) Then ' solicit panel

                    txtInitialSalesOrder.Text = State.initialSalesOrderNumber

                    State.dealerId = GetSelectedItem(ddlDealer)
                    If State.dealerId <> Guid.Empty And ddlDealer.Items.Count > 0 Then SetSelectedItem(ddlDealer, State.dealerId)

                    txtCustomerId.Text = State.customerId

                    txtApplyDate.Text = State.applyDate
                    txtSimPhoneNumber.Text = State.simPhoneNumber
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SetStateProperties()
            Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.ScDealerId

            Try
                If State Is Nothing Then
                    RestoreState(New MyState)
                End If

                ClearStateValues()
                If State.ActivePanel.Equals(State.searchTypes.Item(0)) Then 'solicit panel
                    State.initialSalesOrderNumber = txtInitialSalesOrder.Text.ToUpper.Trim
                    State.dealerId = GetSelectedItem(ddlDealer)
                    State.dealerName = LookupListNew.GetCodeFromId("DEALERS", State.dealerId)
                    State.customerId = txtCustomerId.Text.ToUpper.Trim
                    State.simPhoneNumber = txtSimPhoneNumber.Text.ToUpper.Trim
                End If

            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGrid()
            Try


                Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE_SOLICIT_API_URL), True)

                If String.IsNullOrEmpty(oWebPasswd.Url) Then
                    Throw New ArgumentNullException($"Web Password entry not found or Solicitation  Api Url not configured for Service Type {Codes.SERVICE_TYPE_SOLICIT_API_URL}")
                ElseIf String.IsNullOrEmpty(oWebPasswd.UserId) Or String.IsNullOrEmpty(oWebPasswd.Password) Then
                    Throw New ArgumentNullException($"Web Password username or password not configured for Service Type {Codes.SERVICE_TYPE_SOLICIT_API_URL}")
                End If
                ' System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls Or System.Net.SecurityProtocolType.Tls11 Or System.Net.SecurityProtocolType.Tls12
                Dim client As HttpClient = New HttpClient()
                client.DefaultRequestHeaders.Accept.Clear()
                client.DefaultRequestHeaders.Add(oWebPasswd.UserId, oWebPasswd.Password)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                'pbi-543167 needs to revisit as currently organization code is hardcoded which evantaally gets generated using companyID+ dealer value(ddlDealer.SelectedValue)
                Dim searchSolicit As Solicit.SolicitSearch = New Solicit.SolicitSearch()
                'Form Organization code dynamically
                'searchSolicit.OwnerOrganizationCode = policyInfo.WorkSpace.Workgroup.CompanyGroupCode & "-" + policyInfo.WorkSpace.Company.Code & "-" + dealerCode
                searchSolicit.OwnerOrganizationCode = "ASJP-AJP-RSIM" 'value hard coded for now ddlDealer.SelectedValue value should be assigned later
                Dim locatorProperties As New Dictionary(Of String, String)
                If Not String.IsNullOrEmpty(txtInitialSalesOrder.Text.Trim) Then
                    locatorProperties.Add(SALES_ORDER_NUMBER, txtInitialSalesOrder.Text.Trim)
                End If
                If Not String.IsNullOrEmpty(txtCustomerId.Text.Trim) Then
                    locatorProperties.Add(CUSTOMER_ID, txtCustomerId.Text.Trim)
                End If
                If Not String.IsNullOrEmpty(txtApplyDate.Text.Trim) Then
                    Dim applyDateProperFormat = DateTime.Parse(txtApplyDate.Text.Trim).ToString("yyyy-MM-dd")
                    locatorProperties.Add(APPLY_DATE, applyDateProperFormat)
                End If

                If Not String.IsNullOrEmpty(txtSimPhoneNumber.Text.Trim) Then
                    locatorProperties.Add(SIM_PHONE_NUMBER, txtSimPhoneNumber.Text.Trim)
                End If

                searchSolicit.LocatorProperties = locatorProperties

                Dim requestData = JsonConvert.SerializeObject(searchSolicit, Formatting.Indented)
                Dim requestBody = New StringContent(requestData)
                Dim url As New Uri(oWebPasswd.Url)
                Dim response As HttpResponseMessage = client.PostAsync(url, requestBody).GetAwaiter().GetResult()
                Dim json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()

                If Not response.IsSuccessStatusCode Then
                    'Me.MasterPage.MessageController.AddInformation("There is an error in displaying the SOLICIT Data", True)
                    Throw New Exception($"There is an error in displaying the SOLICITATION Data. {response.ReasonPhrase}")
                Else
                    Try
                        Dim jsonSolicitsArray As Newtonsoft.Json.Linq.JArray = New Newtonsoft.Json.Linq.JArray(json)
                        If jsonSolicitsArray.Count = 0 Then
                            MasterPage.MessageController.AddInformation(NO_RECORDS_FOUND, True)
                            'Throw New Exception($"No Records Found for SOLICIT Data. {response.ReasonPhrase}"
                        End If
                    Catch ex As Exception
                        Throw New Exception($"No Records Found for SOLICITATION Data,refine your search criteria to try again. {response.ReasonPhrase}")
                    End Try
                End If

                Try
                    Dim solicitDetailsList As List(Of Solicit.SolicitDetails) = JsonConvert.DeserializeObject(Of List(Of Solicit.SolicitDetails))(json)
                    Dim solicitLableTranslation As Solicit.SolicitLabelTranslation = GetAllLabelTranslation()
                    Dim solicitDetails As List(Of Solicit.SolicitDetails) =
                    (From solicit In solicitDetailsList
                     Select New Solicit.SolicitDetails With {
                        .campaignCode = solicit.campaignCode,
                        .conversionDate = solicit.conversionDate,
                    .creationSourceName = solicit.creationSourceName,
                        .creationSourceType = solicit.creationSourceType,
                        .customer = solicit.customer,
                        .effectiveDate = solicit.effectiveDate,
                        .expirationDate = solicit.expirationDate,
                        .id = solicit.id,
                        .origin = solicit.origin,
                        .owner = solicit.owner,
                        .status = solicit.status,
                        .labelTranslation = solicitLableTranslation
                        }).ToList()

                    Dim solicitDetailsListXml As String = ConvertJsonToXML(solicitDetails)
                    ShowGridData(solicitDetailsListXml)

                Catch ex As Exception
                    MasterPage.MessageController.AddInformation(NO_RECORDS_FOUND, True)

                End Try
                Session("recCount") = ""
            Catch ex As Exception
                Dim GetExceptionType As String = ex.GetBaseException.GetType().Name
                MasterPage.MessageController.AddInformation(NO_RECORDS_FOUND, True)

            End Try
        End Sub

        Private Function GetAllLabelTranslation() As Solicit.SolicitLabelTranslation
            Dim solicitLabel As Solicit.SolicitLabelTranslation = New Solicit.SolicitLabelTranslation()
            solicitLabel.INITIAL_SALES_ORDER = TranslationBase.TranslateLabelOrMessage("INITIAL_SALES_ORDER")
            solicitLabel.CUSTOMER_ID = TranslationBase.TranslateLabelOrMessage("CUSTOMER_ID")
            solicitLabel.APPLY_DATE_SOLICITATION_DATE = TranslationBase.TranslateLabelOrMessage("APPLY_DATE")
            solicitLabel.OPEN_DATE_FROM_LEAD_FILE = TranslationBase.TranslateLabelOrMessage("OPEN_DATE_FROM_LEAD_FILE")
            solicitLabel.LEAD_RECORD_STATUS = TranslationBase.TranslateLabelOrMessage("LEAD_RECORD_STATUS")
            solicitLabel.SOURCE = TranslationBase.TranslateLabelOrMessage("SOURCE")
            solicitLabel.EXPIRATION_DATE = TranslationBase.TranslateLabelOrMessage("EXPIRATION_DATE")
            solicitLabel.CONVERSION_DATE = TranslationBase.TranslateLabelOrMessage("SOLICIT_CONVERSION_DATE")
            solicitLabel.CUSTOMER_LAST_NAME = TranslationBase.TranslateLabelOrMessage("CUSTOMER_LAST_NAME")
            solicitLabel.CUSTOMER_FIRST_NAME = TranslationBase.TranslateLabelOrMessage("CUSTOMER_FIRST_NAME")
            solicitLabel.LAST_NAME_KANA = TranslationBase.TranslateLabelOrMessage("LAST_NAME_KANA")
            solicitLabel.FIRST_NAME_KANA = TranslationBase.TranslateLabelOrMessage("FIRST_NAME_KANA")
            solicitLabel.CUSTOMER_BIRTH_DATE = TranslationBase.TranslateLabelOrMessage("CUSTOMER_BIRTH_DATE")
            solicitLabel.ADDRESS = TranslationBase.TranslateLabelOrMessage("ADDRESS")
            solicitLabel.POSTAL_CODE = TranslationBase.TranslateLabelOrMessage("POSTAL_CODE")
            solicitLabel.E_MAIL = TranslationBase.TranslateLabelOrMessage("E_MAIL")
            solicitLabel.TEL_MOB_PHONE_NUMBER = TranslationBase.TranslateLabelOrMessage("TEL_MOB_PHONE_NUMBER")
            solicitLabel.SIM_HOME_PHONE_NUMBER = TranslationBase.TranslateLabelOrMessage("SIM_HOME_PHONE_NUMBER")
            solicitLabel.SALES_CHANNEL = TranslationBase.TranslateLabelOrMessage("SALES_CHANNEL")
            solicitLabel.SHOP_ID = TranslationBase.TranslateLabelOrMessage("SHOP_ID")
            solicitLabel.SHOP_NAME = TranslationBase.TranslateLabelOrMessage("SHOP_NAME")
            solicitLabel.SHOP_ZIP_CODE = TranslationBase.TranslateLabelOrMessage("SHOP_ZIP_CODE")
            solicitLabel.SHOP_ADDRESS = TranslationBase.TranslateLabelOrMessage("SHOP_ADDRESS")
            solicitLabel.SHOP_TELE_PHONE_NUMBER = TranslationBase.TranslateLabelOrMessage("SHOP_TELE_PHONE_NUMBER")
            solicitLabel.DATE_SEPARATOR = "年-月-日"


            Return solicitLabel
        End Function

        Public Function ConvertJsonToXML(solicitDetailsList As List(Of Solicit.SolicitDetails)) As String
            Dim solicitStringWriter As StringWriter = New StringWriter()
            Dim solicitSerializer As New XmlSerializer(solicitDetailsList.GetType())
            solicitSerializer.Serialize(solicitStringWriter, solicitDetailsList)
            Return solicitStringWriter.ToString()
        End Function
        Public Function ConvertJsonToXML(solicitDetailsLabelList As Dictionary(Of String, String)) As String
            Dim solicitStringWriter As StringWriter = New StringWriter()
            Dim solicitSerializer As New XmlSerializer(solicitDetailsLabelList.GetType())
            solicitSerializer.Serialize(solicitStringWriter, solicitDetailsLabelList)
            Return solicitStringWriter.ToString()
        End Function
        Private Sub ShowGridData(solicitListXml As String)
            Try
                Dim xdoc As New System.Xml.XmlDocument
                xdoc.LoadXml(solicitListXml)
                xmlSource.TransformSource = HttpContext.Current.Server.MapPath("~/Certificates/SolicitDetailsForm.xslt")
                xmlSource.TransformArgumentList = New XsltArgumentList
                'xmlSource.TransformArgumentList.AddParam("recordCount", String.Empty, 30)
                xmlSource.Document = xdoc
            Catch ex As Exception
                HandleErrors(ex, MasterPage.MessageController)
            End Try
        End Sub
#End Region


    End Class


End Namespace