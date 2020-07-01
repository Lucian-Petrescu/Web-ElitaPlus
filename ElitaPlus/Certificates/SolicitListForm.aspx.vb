
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
Imports System.Text



Namespace Certificates
    Partial Public Class SolicitListForm
        Inherits ElitaPlusSearchPage
#Region "Constants"
        ' Public Const URL As String = "Certificates/CertificateListForm.aspx"
        'Public Const PAGETITLE As String = "CERTIFICATE_SEARCH"
        Public Const PAGETAB As String = "SOLICITES"
        Public Const SEARCH_EXCEPTION As String = "Enter Correct Search Criterion(s)" 'Solicit List Search Exception
        Public Const NO_DEALER_SELECTED = "--"
        Public Const NO_RECORDS_FOUND = "NO RECORDS FOUND."
        Public Const SALES_ORDER_NUMBER = "salesOrderNumber"
        Public Const CUSTOMER_ID = "customerId"
        Public Const DATE_OF_BIRTH = "dateOfBirth"
        Public Const SIM_PHONE_NUMBER = "simPhoneNumber"
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
            Public dateOfBirth As String = String.Empty
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
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("Solicites")
                    UpdateBreadCrum()

                    'TranslateGridHeader(Grid)
                    Me.AddCalendar_New(Me.btnDateOfBirth, Me.txtDateOfBirth)

                    GetStateProperties()
                    PopulateSearchControls()

                    If Me.IsReturningFromChild Then
                        ' It is returning from detail
                        Me.PopulateGrid()
                    End If

                    SetFocus(Me.txtInitialSalesOrder)
                    objPrincipal = CType(System.Threading.Thread.CurrentPrincipal, ElitaPlusPrincipal).Identity
                    If objPrincipal.PrivacyUserType = AppConfig.DataProtectionPrivacyLevel.Privacy_DataProtection Then
                        Me.State.isDPO = True
                    End If

                End If
                Me.DisplayNewProgressBarOnClick(Me.btnSearch, "Loading Solicit")
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
            Me.ShowMissingTranslations(Me.MasterPage.MessageController)
        End Sub


        Private Sub UpdateBreadCrum()
            If (Not Me.State Is Nothing) Then
                If (Not Me.State Is Nothing) Then
                    Me.MasterPage.BreadCrum = Me.MasterPage.PageTab & ElitaBase.Sperator &
                        TranslationBase.TranslateLabelOrMessage("SOLICIT SEARCH")
                    Me.MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage("SOLICIT SEARCH")
                End If
            End If
        End Sub


        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                Dim retObj As CertificateForm.ReturnType = CType(ReturnPar, CertificateForm.ReturnType)
                If Not retObj Is Nothing AndAlso retObj.BoChanged Then
                    'Me.State.searchDV = Nothing
                End If
                Select Case retObj.LastOperation
                    Case ElitaPlusPage.DetailPageCommand.Back
                        If Not retObj Is Nothing Then
                            If Not retObj.EditingBo.IsNew Then
                                ' Me.State.selectedSolicitId = retObj.EditingBo.Id
                            End If

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
                If txtInitialSalesOrder.Text = String.Empty And txtCustomerId.Text = String.Empty And txtDateOfBirth.Text = String.Empty And
                    txtSimPhoneNumber.Text = String.Empty And ddlDealer.SelectedIndex = &H0 Then
                    Dim errors() As ValidationError = {New ValidationError(SEARCH_EXCEPTION, GetType(Solicit), Nothing, "Search", Nothing)}
                    Throw New BOValidationException(errors, GetType(Solicit).FullName)
                End If
                If ddlDealer.SelectedIndex = &H0 Or ddlDealer.SelectedValue Is Nothing Then
                    Dim errors() As ValidationError = {New ValidationError("DEALER_NAME_CANT_BE_EMPTY", GetType(Solicit), Nothing, "Search", Nothing)}
                    Throw New BOValidationException(errors, GetType(Solicit).FullName)
                End If
                If ddlDealer.SelectedIndex > 0 Then
                    If (txtInitialSalesOrder.Text = String.Empty AndAlso txtCustomerId.Text = String.Empty AndAlso txtDateOfBirth.Text = String.Empty AndAlso
                    txtSimPhoneNumber.Text = String.Empty) Then
                        Dim errors() As ValidationError = {New ValidationError("Enter At Least One Search Criterion along with dealer", GetType(Solicit), Nothing, "Search", Nothing)}
                        Throw New BOValidationException(errors, GetType(Solicit).FullName)
                    End If
                End If
                Me.SetStateProperties()
                Me.State.selectedSolicitId = Guid.Empty

                Me.State.searchClick = True
                'Me.State.searchDV = Nothing
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
                Me.State.initialSalesOrderNumber = String.Empty
                Me.State.customerId = String.Empty
                Me.State.simPhoneNumber = String.Empty
                Me.State.dealerId = Nothing
                Me.State.dateOfBirth = String.Empty
                Me.State.dealerName = Nothing

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try

        End Sub

        Protected Sub ClearAllSearchOptions()
            ' Clear Solicit search panel
            Me.txtInitialSalesOrder.Text = String.Empty
            Me.txtCustomerId.Text = String.Empty
            Me.txtSimPhoneNumber.Text = String.Empty
            Me.txtDateOfBirth.Text = String.Empty
            If Not ElitaPlusIdentity.Current.ActiveUser.IsDealer Then Me.ddlDealer.SelectedIndex = 0


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

            Try
                'Populate Solicit Search Dropdowns

                Me.PopulateDealerDropdown(Me.ddlDealer, Me.State.dealerId)

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateDealerDropdown(ByVal dealerDropDownList As DropDownList, ByVal setvalue As Guid)
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

        Private Sub GetStateProperties()
            Try

                ' Set state based on panel selected
                If Me.State.ActivePanel.Equals(Me.State.searchTypes.Item(0)) Then ' solicit panel

                    Me.txtInitialSalesOrder.Text = Me.State.initialSalesOrderNumber

                    Me.State.dealerId = Me.GetSelectedItem(Me.ddlDealer)
                    If Me.State.dealerId <> Guid.Empty And ddlDealer.Items.Count > 0 Then Me.SetSelectedItem(ddlDealer, State.dealerId)

                    Me.txtCustomerId.Text = Me.State.customerId

                    Me.txtDateOfBirth.Text = Me.State.dateOfBirth
                    Me.txtSimPhoneNumber.Text = Me.State.simPhoneNumber
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub SetStateProperties()
            Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.ScDealerId

            Try
                If Me.State Is Nothing Then
                    Me.RestoreState(New MyState)
                End If

                Me.ClearStateValues()
                If Me.State.ActivePanel.Equals(Me.State.searchTypes.Item(0)) Then 'solicit panel
                    Me.State.initialSalesOrderNumber = Me.txtInitialSalesOrder.Text.ToUpper.Trim
                    Me.State.dealerId = Me.GetSelectedItem(Me.ddlDealer)
                    Me.State.dealerName = LookupListNew.GetCodeFromId("DEALERS", Me.State.dealerId)
                    Me.State.customerId = Me.txtCustomerId.Text.ToUpper.Trim
                    Me.State.simPhoneNumber = Me.txtSimPhoneNumber.Text.ToUpper.Trim
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub

        Private Sub PopulateGrid()
            Try

                Dim oWebPasswd As WebPasswd = New WebPasswd(Guid.Empty, LookupListNew.GetIdFromCode(Codes.SERVICE_TYPE, Codes.SERVICE_TYPE_SOLICIT_API_URL), False)

                If String.IsNullOrEmpty(oWebPasswd.Url) Then
                    Throw New ArgumentNullException($"Web Password entry not found or Solicit  Api Url not configured for Service Type {Codes.SERVICE_TYPE_SOLICIT_API_URL}")
                ElseIf String.IsNullOrEmpty(oWebPasswd.UserId) Or String.IsNullOrEmpty(oWebPasswd.Password) Then
                    Throw New ArgumentNullException($"Web Password username or password not configured for Service Type {Codes.SERVICE_TYPE_SOLICIT_API_URL}")
                End If
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls Or System.Net.SecurityProtocolType.Tls11 Or System.Net.SecurityProtocolType.Tls12
                Dim client As HttpClient = New HttpClient()
                client.DefaultRequestHeaders.Accept.Clear()
                client.DefaultRequestHeaders.Add(oWebPasswd.UserId, oWebPasswd.Password)
                client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
                'pbi-543167 needs to revisit as currently organization code is hardcoded which evantaally gets generated using companyID+ dealer value(ddlDealer.SelectedValue)
                Dim searchSolicit As Solicit.SolicitSearch = New Solicit.SolicitSearch()
                searchSolicit.ownerOrganizationCode = "ASJP-AJP-RSIM" 'value hard coded for now ddlDealer.SelectedValue value should be assigned later
                Dim locatorProperties = New Dictionary(Of String, String) From {
                        {SALES_ORDER_NUMBER, txtInitialSalesOrder.Text.Trim},
                        {CUSTOMER_ID, txtCustomerId.Text.Trim},
                        {DATE_OF_BIRTH, txtDateOfBirth.Text.Trim},
                        {SIM_PHONE_NUMBER, txtSimPhoneNumber.Text.Trim}
                }
                searchSolicit.LocatorProperties = locatorProperties

                Dim requestData = JsonConvert.SerializeObject(searchSolicit, Formatting.Indented)
                Dim requestBody = New StringContent(requestData)
                Dim url As New Uri(oWebPasswd.Url)
                Dim response As HttpResponseMessage = client.PostAsync(url, requestBody).GetAwaiter().GetResult()
                Dim json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                If Not response.IsSuccessStatusCode Then
                    Throw New Exception($"There is an error in displaying the SOLICIT Data. {response.ReasonPhrase}")
                End If

                ''Dim response As HttpResponseMessage = client.GetAsync("/solicitsearch").GetAwaiter().GetResult()

                'If Not response.IsSuccessStatusCode Then
                '    Throw New Exception($"There is an error in displaying the SOLICIT Data. {response.ReasonPhrase}")
                'End If
                'Dim json As String = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
                Try
                    Dim solicitDetailsList As List(Of Solicit.SolicitDetails) = JsonConvert.DeserializeObject(Of List(Of Solicit.SolicitDetails))(json)
                    Dim solicitDetailsListXml As String = ConvertJsonToXML(solicitDetailsList)
                    ShowGridData(solicitDetailsListXml)
                Catch ex As Exception
                    Me.HandleErrors(ex, Me.MasterPage.MessageController)
                End Try
                Session("recCount") = ""
            Catch ex As Exception
                Dim GetExceptionType As String = ex.GetBaseException.GetType().Name

                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
        Public Function ConvertJsonToXML(ByVal solicitDetailsList As List(Of Solicit.SolicitDetails)) As String
            Dim solicitStringWriter As StringWriter = New StringWriter()
            Dim solicitSerializer As New XmlSerializer(solicitDetailsList.GetType())
            solicitSerializer.Serialize(solicitStringWriter, solicitDetailsList)
            Return solicitStringWriter.ToString()
        End Function

        Private Sub ShowGridData(ByVal solicitListXml As String)
            Try
                Dim xdoc As New System.Xml.XmlDocument
                xdoc.LoadXml(solicitListXml)
                xmlSource.TransformSource = HttpContext.Current.Server.MapPath("~/Certificates/SolicitDetailsForm.xslt")
                xmlSource.TransformArgumentList = New XsltArgumentList
                xmlSource.TransformArgumentList.AddParam("recordCount", String.Empty, 30)
                xmlSource.Document = xdoc
            Catch ex As Exception
                Me.HandleErrors(ex, Me.MasterPage.MessageController)
            End Try
        End Sub
#End Region


    End Class


End Namespace