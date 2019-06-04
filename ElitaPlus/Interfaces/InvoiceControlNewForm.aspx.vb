Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms
Partial Public Class InvoiceControlNewForm
    Inherits ElitaPlusPage

#Region "Constants"
    Public Const URL As String = "InvoiceControlNewForm.aspx"
    Public Const PAGETITLE As String = "PREMIUM_INVOICE_NEW"
    Public Const PAGETAB As String = "INTERFACES"
    Public Const MSG_INVOICE_CREATED_OK As String = "MSG_INVOICE_CREATED_OK"
#End Region

#Region "Properties"

#End Region

#Region "Page Return Type"
    Public Class ReturnType
        Public LastOperation As DetailPageCommand
        Public HasDataChanged As Boolean
        Public Sub New(ByVal LastOp As DetailPageCommand, ByVal hasDataChanged As Boolean)
            Me.LastOperation = LastOp
            Me.HasDataChanged = hasDataChanged
        End Sub
    End Class
#End Region

#Region "Page State"
    Class MyState
        Public SelectedDealerID As Guid
        Public HasDataChanged As Boolean = False
        Public intStatusId As Guid
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

#Region "Page event"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.ErrControllerMaster.Clear_Hide()
        Try
            If Not Me.IsPostBack Then

                Me.SetFormTitle(PAGETITLE)
                Me.SetFormTab(PAGETAB)

                populateDropdown()
            End If
            Me.DisplayProgressBarOnClick(Me.btnCreateNew, "CREATE_INVOICE")
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
        Me.ShowMissingTranslations(Me.ErrControllerMaster)
    End Sub

    Private Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        If ErrControllerMaster.Visible Then
            Me.spanFiller.Text = "<tr><td colspan=""2"" style=""height:280px"">&nbsp;</td></tr>"
        Else
            Me.spanFiller.Text = "<tr><td colspan=""2"" style=""height:1px"">&nbsp;</td></tr>"
        End If
    End Sub

#End Region

#Region "Helper functions"
    Private Sub populateDropdown()
        Try
            'Dim dv As DataView = LookupListNew.GetDealerLookupList(Authentication.CurrentUser.Companies)
            'BindListControlToDataView(Me.ddlDealer, dv, "DESCRIPTION", "ID", True)
            'dv.Sort = "CODE"
            'BindListControlToDataView(Me.ddlDealerCode, dv, "CODE", "ID", True)

            Dim DealerList As New Collections.Generic.List(Of DataElements.ListItem)

            For Each CompanyId As Guid In ElitaPlusIdentity.Current.ActiveUser.Companies
                Dim Dealers As DataElements.ListItem() =
                    CommonConfigManager.Current.ListManager.GetList(listCode:="DealerListByCompany",
                                                        context:=New ListContext() With
                                                        {
                                                          .CompanyId = CompanyId
                                                        })

                If Dealers.Count > 0 Then
                    If Not DealerList Is Nothing Then
                        DealerList.AddRange(Dealers)
                    Else
                        DealerList = Dealers.Clone()
                    End If
                End If
            Next

            ddlDealer.Populate(DealerList.ToArray(),
                        New PopulateOptions() With
                        {
                            .AddBlankItem = True,
                            .TextFunc = Function(x)
                                            Return x.Translation '+ " (" + x.Code + ")"
                                        End Function,
                            .SortFunc = AddressOf .GetCode
                        })

            ddlDealerCode.Populate(DealerList.ToArray(),
                                   New PopulateOptions() With
                                        {
                                            .AddBlankItem = True,
                                            .ValueFunc = AddressOf .GetListItemId,
                                            .TextFunc = AddressOf .GetCode,
                                            .SortFunc = AddressOf .GetCode
                                        })

            Me.ddlDealer.Attributes.Add("onchange", "UpdateList('" & Me.ddlDealerCode.ClientID & "')")
            Me.ddlDealerCode.Attributes.Add("onchange", "UpdateList('" & Me.ddlDealer.ClientID & "')")
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Private Sub ShowInfoMsgBox(ByVal strMsg As String, Optional ByVal Translate As Boolean = True)
        Dim translatedMsg As String = strMsg
        If Translate Then translatedMsg = TranslationBase.TranslateLabelOrMessage(strMsg)
        Dim sJavaScript As String
        sJavaScript = "<SCRIPT>" & Environment.NewLine
        sJavaScript &= "setTimeout(""showMessage('" & translatedMsg & "', '" & "AlertWindow" & "', '" & Me.MSG_BTN_OK & "', '" & Me.MSG_TYPE_INFO & "', '" & "null" & "')"", 0);" & Environment.NewLine
        sJavaScript &= "</SCRIPT>" & Environment.NewLine
        Me.RegisterStartupScript("ShowConfirmation", sJavaScript)
    End Sub

#End Region

#Region "Button handler"
    Protected Sub btnBACK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBACK.Click
        Try
            Me.ReturnToCallingPage(Me.State.HasDataChanged)
        Catch ex As Threading.ThreadAbortException
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnCreateNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateNew.Click
        Try
            Dim DealerID As Guid = Me.GetSelectedItem(Me.ddlDealer)
            If DealerID = Guid.Empty Then
                Throw New GUIException("You must select a dealer file", Assurant.ElitaPlus.Common.ErrorCodes.GUI_DEALER_MUST_BE_SELECTED_ERR)
            Else
                AcctPremInvoice.CreateInvoice(DealerID, Authentication.CurrentUser.NetworkId)
                State.HasDataChanged = True
                ShowInfoMsgBox(MSG_INVOICE_CREATED_OK)
            End If
        Catch ex As Exception
            Me.HandleErrors(ex, Me.ErrControllerMaster)
        End Try
    End Sub

    Protected Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Me.ddlDealer.SelectedIndex = 0
        Me.ddlDealerCode.SelectedIndex = 0
    End Sub
#End Region


End Class