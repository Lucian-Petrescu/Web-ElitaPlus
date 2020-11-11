Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.Web.Forms
Imports Assurant.Elita.CommonConfiguration.DataElements

Public Class ArInvoiceDetailsForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const Url As String = "~/Interfaces/ArInvoiceDetailsForm.aspx"
    Public Const PageTitle = "AR_INVOICE_DETAILS"
    Public Const PageTab As String = "INTERFACE"
    Public Const LineBatchSize As Integer = 100
    Public Const MaxLineNumberAllowed As Integer = 99999999
    Public Const DefaultGridPageSize As Integer = 15

#End Region

    #Region "Page State"
Class MyState
        Public HasDataChanged As Boolean =False
 
        Public PageIndex As Integer = 0
        Public MyBo As ArInvoiceHeader
        Public InvoiceLines As ArInvoiceHeader.ArInvoiceLinesDv = Nothing 
        Public SelectedPageSize As Integer = DefaultGridPageSize
        
        Public Sub New()
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

#Region "Page Event handler"
    Private Sub UpdateBreadCrumb()
        If (Not State Is Nothing) Then
            If (Not State Is Nothing) Then
                MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator &
                                          TranslationBase.TranslateLabelOrMessage(PageTitle)
                MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PageTitle)
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        MasterPage.MessageController.Clear_Hide()
        Try
            If Not IsPostBack Then
                UpdateBreadCrumb()

                SetFormTitle(PAGETITLE)
                SetFormTab(PAGETAB)
                
                PopulateDropDown()
                
                TranslateGridHeader(Grid)
                cboPageSize.SelectedValue = CType(State.selectedPageSize, String)
                SetGridItemStyleColor(Grid)     

                PopulateFormFromBo()
            End If
            
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)
    End Sub

    Private Sub ArInvoiceDetailsForm_PageCall(callFromUrl As String, callingPar As Object) Handles Me.PageCall
        Try
            Dim tempGuid As Guid = CallingPar
            State.MyBo = New ArInvoiceHeader(tempGuid)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


#End Region

#Region "Controlling logic"
    Private Sub PopulateDealerDropdown(Byval companyId As Guid)
        Dim oDealerList As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany", 
                                                                                        context:=New ListContext() With
                                                                                           {
                                                                                           .CompanyId = companyId
                                                                                           })
        

        Dim dealerTextFunc As Func(Of ListItem, String) = Function(li As ListItem)
            Return li.Code + " - " + li.Translation
        End Function

        ddlDealer.Populate(oDealerList, New PopulateOptions() With
                              {
                              .AddBlankItem = True,
                              .TextFunc = dealerTextFunc,
                              .SortFunc = dealerTextFunc
                              })

    End Sub

    Private Sub PopulateDropDown()

        Try
            Dim companyList As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="Company")

            Dim filteredCompanyList As ListItem() = (From x In companyList
                    Where Authentication.CurrentUser.Companies.Contains(x.ListItemId)
                    Select x).ToArray()

            Dim companyTextFunc As Func(Of ListItem, String) = Function(li As ListItem)
                Return li.Code + " - " + li.Translation
            End Function

            ddlCompany.Populate(filteredCompanyList, New PopulateOptions() With
                                   {
                                   .AddBlankItem = False,
                                   .TextFunc = companyTextFunc,
                                   .ValueFunc = AddressOf PopulateOptions.GetListItemId
                                   })

            ddlStatus.Populate(CommonConfigManager.Current.ListManager.GetList($"CMSTAUS", Authentication.CurrentUser.LanguageCode), New PopulateOptions() With
                                  {
                                  .AddBlankItem = True,
                                  .BlankItemValue = String.Empty,
                                  .TextFunc = AddressOf PopulateOptions.GetDescription,
                                  .ValueFunc = AddressOf PopulateOptions.GetExtendedCode,
                                  .SortFunc = AddressOf PopulateOptions.GetDescription
                                  })

            ddlPaymentMethod.Populate(CommonConfigManager.Current.ListManager.GetList($"PMTHD", Authentication.CurrentUser.LanguageCode), New PopulateOptions() With
                                  {
                                  .AddBlankItem = True,
                                  .BlankItemValue = String.Empty,
                                  .TextFunc = AddressOf PopulateOptions.GetDescription,
                                  .ValueFunc = AddressOf PopulateOptions.GetExtendedCode,
                                  .SortFunc = AddressOf PopulateOptions.GetDescription
                                  })

            'add hard-coded dropdown list
            ddlDocType.Items.Add(New WebControls.ListItem() With {.Value = String.Empty, .Text = String.Empty, .Selected = True})
            ddlDocType.Items.Add(New WebControls.ListItem() With {.Value = $"CM", .Text = TranslationBase.TranslateLabelOrMessage("CREDIT_MEMO")})
            ddlDocType.Items.Add(New WebControls.ListItem() With {.Value = $"INV", .Text = TranslationBase.TranslateLabelOrMessage("INVOICE")})

            ddlReference.Items.Add(New WebControls.ListItem() With {.Value = String.Empty, .Text = String.Empty, .Selected = True})
            ddlReference.Items.Add(New WebControls.ListItem() With {.Value = $"ELP_CERT", .Text = TranslationBase.TranslateLabelOrMessage("CERTIFICATE")})
            ddlReference.Items.Add(New WebControls.ListItem() With {.Value = $"ELP_CLAIM", .Text = TranslationBase.TranslateLabelOrMessage("CLAIM")})
            ddlReference.Items.Add(New WebControls.ListItem() With {.Value = $"ELP_CLAIM_AUTHORIZATION", .Text = TranslationBase.TranslateLabelOrMessage("CLAIM_AUTHORIZATION")})

        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try


    End Sub
    Private sub PopulateFormFromBo()
        If State Is Nothing OrElse State.MyBo Is Nothing Then 
            Exit Sub
        End If
        With State.MyBo
            'populate dealer dropdown first based on the company id
            PopulateDealerDropdown(.CompanyId)

            'populate dropdown lists
            PopulateControlFromBOProperty(ddlCompany, .CompanyId)
            PopulateControlFromBOProperty(ddlDealer, .DealerId)
            'PopulateControlFromBOProperty(ddlDocType, .DocType)
            'PopulateControlFromBOProperty(ddlReference, .Reference)
            'PopulateControlFromBOProperty(ddlPaymentMethod, .PaymentMethodXcd)
            'PopulateControlFromBOProperty(ddlStatus, .StatusXcd)

            ddlDocType.SelectedValue = .DocType
            ddlReference.SelectedValue = .Reference
            ddlStatus.SelectedValue = .StatusXcd
            ddlPaymentMethod.SelectedValue = .PaymentMethodXcd

            'populate texts
            PopulateControlFromBOProperty(txtSource, .Source)
            PopulateControlFromBOProperty(txtInvoiceNumber, .InvoiceNumber)
            PopulateControlFromBOProperty(txtAccountingPeriod, .AcctPeriod)
            PopulateControlFromBOProperty(txtComments, .Comments)
            PopulateControlFromBOProperty(txtCurrencyCode, .CurrencyCode)
            PopulateControlFromBOProperty(txtInvoiceAmount, .InvoiceAmount)
            PopulateControlFromBOProperty(txtInvoiceOpenAmt, .InvoiceOpenAmount)
            PopulateControlFromBOProperty(txtDocUniqueId, .DocUniqueIdentifier)
            PopulateControlFromBOProperty(txtPosted, .Posted)
            PopulateControlFromBOProperty(txtDistributed, .Distributed)
            PopulateControlFromBOProperty(txtExchangeRate, .ExchangeRate)
            PopulateControlFromBOProperty(txtInstallmentNum, .InstallmentNumber)
            PopulateControlFromBOProperty(txtNumOfPymtRejected, .NoOfTimesPymtRejected)
            PopulateControlFromBOProperty(txtInvoiceDate, .InvoiceDate)
            PopulateControlFromBOProperty(txtInvoiceDueDate, .InvioceDueDate)
            PopulateControlFromBOProperty(txtReferenceNum, .ReferenceId)
            PopulateControlFromBOProperty(txtCreatedBy, .CreatedById)
            PopulateControlFromBOProperty(txtCreatedDate, .CreatedDateTime)

            
            ucAddressShipTo.EnableControl(False)
            If .ShipToAddressId <> Guid.Empty Then
                ucAddressShipTo.Bind(new Address(.ShipToAddressId))
            End If
            
            ucAddressBillTo.EnableControl(False)
            if .BillToAddressId <> Guid.Empty Then
                ucAddressBillTo.Bind(New Address(.BillToAddressId))
            End If
            
            If .BankInfoId <> Guid.Empty Then
                ucBankInfo.Bind(New BankInfo(.BankInfoId))
                ucBankInfo.DisableAllFields() 'view only
            End If

            PopulateLinesGrid()
        End With
    End sub

    Private Sub PopulateLinesGrid()
        If State.InvoiceLines Is Nothing Then
            State.InvoiceLines = ArInvoiceHeader.GetInvoiceLinesByHeader(State.MyBo.Id)
        End If

        Grid.PageSize = State.selectedPageSize
        Grid.DataSource = State.InvoiceLines
        Grid.PageIndex = State.PageIndex

        HighLightSortColumn(Grid, String.Empty, IsNewUI)

        Grid.DataBind()

        ControlMgr.SetVisibleControl(Me, Grid, True)

        ControlMgr.SetVisibleControl(Me, trPageSize, True)

        Session("recCount") = State.InvoiceLines.Count

        If Grid.Visible Then
           lblRecordCount.Text = State.InvoiceLines.Count & $" " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND) 
        End If
    End Sub
#End Region

#Region "Button handler"
    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        Try            
            ReturnToCallingPage(new ArInvoiceListForm.ReturnType(State.HasDataChanged))
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub
#End Region

#Region "Grid Event handlers"
    Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
        Try
            Grid.PageIndex = e.NewPageIndex
            State.PageIndex = Grid.PageIndex
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Private Sub cboPageSize_SelectedIndexChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
        Try
            State.selectedPageSize = CType(cboPageSize.SelectedValue, Integer)
            State.PageIndex = NewCurrentPageIndex(Grid, State.InvoiceLines.Count, State.selectedPageSize)
            Grid.PageIndex = NewCurrentPageIndex(Grid, Session("recCount"),cboPageSize.SelectedValue)
            populateLinesGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub


    Public Sub RowCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
        Try
            BaseItemCreated(sender, e)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
        Try
            State.PageIndex = Grid.PageIndex
            populateLinesGrid()
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

#End Region
End Class