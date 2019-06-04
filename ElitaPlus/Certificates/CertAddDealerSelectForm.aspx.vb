Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading

Partial Public Class CertAddDealerSelectForm
    Inherits ElitaPlusSearchPage

#Region "Constants"
    Public Const URL As String = "~/Certificates/CertAddDealerSelectForm.aspx"
    Public Const PAGETITLE As String = "SELECT DEALER" '"ADD_CERTIFICATE"
    Public Const PAGETAB As String = "ADD_CERTIFICATE" '"CERTIFICATES"
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False
    ' This class keeps the current state for the search page.
    Class MyState
        Public DealerID As Guid
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

#Region "Page Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ErrControllerMaster.Clear_Hide()
        Try
            If Not Me.IsPostBack Then

                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)

                populateControls()
                'Else
                'If State.errLabel <> "" Then
                '   Me.ClearLabelErrSign(CType(Me.FindControl(State.errLabel), Label))
                'State.errLabel = ""
                'End If
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub

    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If ErrControllerMaster.Visible Then
            Me.spanFiller.Text = "<tr><td colspan=""2"" style=""height:300px"">&nbsp;</td></tr>"
        Else
            Me.spanFiller.Text = "<tr><td colspan=""2"" style=""height:1px"">&nbsp;</td></tr>"
        End If
    End Sub

    Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
        Me.IsReturningFromChild = True
        If ReturnPar Is Nothing Then
            State.DealerID = Guid.Empty
        Else
            State.DealerID = CType(ReturnPar, Guid)
        End If
    End Sub

#End Region

#Region "Helper functions"
    Sub populateControls()
        Try
            Dim listcontextForDlrList As ListContext = New ListContext()
            listcontextForDlrList.UserId = ElitaPlusIdentity.Current.ActiveUser.Id

            Dim dealerList = GetDealerListForManualEnrollmentByCompanyForUser()
            ' Dim dealerList As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListWithManualEnrollByUserCompany", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontextForDlrList)

            ddlDealerCode.Populate(dealerList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True,
                                                   .ValueFunc = AddressOf .GetListItemId,
                                                   .TextFunc = AddressOf .GetCode,
                                                   .SortFunc = AddressOf .GetCode
                                                  })

            ddlDealer.Populate(dealerList, New PopulateOptions() With
                                                 {
                                                   .AddBlankItem = True,
                                                   .ValueFunc = AddressOf .GetListItemId,
                                                   .TextFunc = Function(x)
                                                                   If ElitaPlusIdentity.Current.ActiveUser.Companies.Count > 1 Then
                                                                       Return x.ExtendedCode + " - " + x.Translation
                                                                   Else
                                                                       Return x.Translation
                                                                   End If

                                                               End Function,
                                                   .SortFunc = AddressOf .GetCode
                                                  })

            'Dim dv As DataView = CertAddController.GetDealerCertAddEnabled(Authentication.CurrentUser.Companies)
            'BindListControlToDataView(Me.ddlDealer, dv, "DESCRIPTION", "ID", True)
            'dv.Sort = "CODE"
            'BindListControlToDataView(Me.ddlDealerCode, dv, "CODE", "ID", True)

            Me.ddlDealer.Attributes.Add("onchange", "UpdateList('" & Me.ddlDealerCode.ClientID & "')")
            Me.ddlDealerCode.Attributes.Add("onchange", "UpdateList('" & Me.ddlDealer.ClientID & "')")

            If Me.State.DealerID <> Guid.Empty Then
                Me.PopulateControlFromBOProperty(ddlDealerCode, State.DealerID)
                Me.PopulateControlFromBOProperty(ddlDealer, State.DealerID)
            End If

        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub


    Private Function GetDealerListForManualEnrollmentByCompanyForUser() As Assurant.Elita.CommonConfiguration.DataElements.ListItem()
        Dim Index As Integer
        Dim oListContext As New Assurant.Elita.CommonConfiguration.ListContext

        Dim UserCompanies As ArrayList = ElitaPlusIdentity.Current.ActiveUser.Companies

        Dim oDealerList As New Collections.Generic.List(Of Assurant.Elita.CommonConfiguration.DataElements.ListItem)
        'Dim companyList As ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:="CompanyListWithManualEnrollByUserCompany", languageCode:=Thread.CurrentPrincipal.GetLanguageCode(), context:=listcontextForCmpList)

        For Index = 0 To UserCompanies.Count - 1
            'UserCompanyList &= ",'" & GuidControl.GuidToHexString(UserCompanyies(Index))
            oListContext.CompanyId = UserCompanies(Index)
            Dim oDealerListForCompany As Assurant.Elita.CommonConfiguration.DataElements.ListItem() = CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.DealerListWithManualEnrollByCompany, context:=oListContext)
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

#End Region

#Region "button click events"
    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Me.State.DealerID = Me.GetSelectedItem(Me.ddlDealer)
            If State.DealerID = Guid.Empty Then
                Me.ErrControllerMaster.AddErrorAndShow(Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            Else
                'Me.callPage(CertAddDetails.URL, Me.State.DealerID)
                Me.callPage(CertAddDetailsForm.URL, New CertAddDetailsForm.Parameters(Me.State.DealerID, Me.ddlDealer.SelectedItem.Text))
            End If
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
        Me.ddlDealer.SelectedIndex = 0
        Me.ddlDealerCode.SelectedIndex = 0
        Me.State.DealerID = Guid.Empty
    End Sub
#End Region


End Class