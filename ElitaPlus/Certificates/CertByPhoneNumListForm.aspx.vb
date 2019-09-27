Imports Assurant.ElitaPlus.ElitaPlusWebApp.Common
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.Elita.CommonConfiguration.DataElements
Imports Assurant.Elita.Web.Forms
Imports System.Threading
Namespace Certificates
    Partial Public Class CertByPhoneNumListForm
        Inherits ElitaPlusSearchPage
#Region "Constants"
        Public Const URL As String = "Certificates/CertByPhoneNumListForm.aspx"
        Public Const PAGETITLE As String = "PHONE_NUMBER_SEARCH"
        Public Const PAGETAB As String = "CERTIFICATES"

        Public Const GRID_COL_CERTIFICATE_ID_IDX As Integer = 1
        Public Const GRID_CTRL_CERTIFICATE_ID As String = "lblCertID"

        Public Const GRID_COL_HOME_PHONE_IDX As Integer = 2
        Public Const GRID_COL_WORK_PHONE_IDX As Integer = 3
        Public Const GRID_COL_STATUS_CODE_IDX As Integer = 4
        Public Const GRID_COL_CUSTOMER_NAME_IDX As Integer = 5
        Public Const GRID_COL_ADDRESS1_IDX As Integer = 6
        Public Const GRID_COL_POSTAL_CODE_IDX As Integer = 7
        Public Const GRID_COL_CERT_NUMBER_IDX As Integer = 8
        Public Const GRID_COL_DEALER_IDX As Integer = 9
        Public Const GRID_COL_PRODUCT_CODE_IDX As Integer = 10
#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public PageIndex As Integer = 0
            Public PageSize As Integer = 10
            Public certificateNumber As String
            Public customerName As String
            Public address As String
            Public zip As String
            Public PhoneNum As String
            Public dealerId As Guid
            Public dealerName As String = String.Empty
            Public selectedSortById As Guid = Guid.Empty
            Public searchDV As Certificate.PhoneNumberSearchDV = Nothing
            Public searchClick As Boolean = False
            Public certificatesFoundMSG As String
            'Added for Req-610
            Public PhoneType As String
            Public PhoneTypeId As Guid = Guid.Empty

            Public selectedCertificateId As Guid = Guid.Empty
            Public IsGridVisible As Boolean = False

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
            Me.ErrControllerMaster.Clear_Hide()
            Me.Form.DefaultButton = btnSearch.UniqueID
            Try
                If Not Me.IsPostBack Then
                    Me.SetFormTitle(PAGETITLE)
                    Me.SetFormTab(PAGETAB)
                    TranslateGridHeader(Grid)

                    GetStateProperties()
                    If Not Me.IsReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        If ElitaPlusIdentity.Current.ActiveUser.IsDealer Then
                            State.dealerId = ElitaPlusIdentity.Current.ActiveUser.ScDealerId
                            ControlMgr.SetEnableControl(Me, ddlDealer, False)
                        End If
                    End If

                    populateSearchControls()

                    If Me.IsReturningFromChild Then
                        ' It is returning from detail
                        Me.PopulateGrid()
                    End If
                    SetFocus(Me.txtPhone)
                End If
                Me.DisplayProgressBarOnClick(Me.btnSearch, "Loading_Certificates")
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
            Me.ShowMissingTranslations(Me.ErrControllerMaster)
        End Sub

        Private Sub CertByPhoneNumListForm_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
            cboPageSize.Visible = Grid.Visible
            lblRecordCount.Visible = Grid.Visible

            If ErrControllerMaster.Visible Then
                If Grid.Visible And Grid.Rows.Count < 10 Then
                    Dim fillerHight As Integer = 200
                    fillerHight = fillerHight - Grid.Rows.Count * 20
                    Me.spanFiller.Text = "<tr><td colspan=""2"" style=""height:" & fillerHight & "px"">&nbsp;</td></tr>"
                End If
            Else
                Me.spanFiller.Text = ""
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
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
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
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnClearSearch_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClearSearch.Click
            Try
                Me.txtCertNum.Text = String.Empty
                Me.txtCustName.Text = String.Empty
                Me.txtAddress.Text = String.Empty
                Me.txtZip.Text = String.Empty
                Me.txtPhone.Text = String.Empty
                If Not ElitaPlusIdentity.Current.ActiveUser.IsDealer Then Me.ddlDealer.SelectedIndex = 0

                'Added for Req-610
                Me.ddlPhoneType.SelectedIndex = 0

                'Update Page State
                With Me.State
                    .certificateNumber = Me.txtCertNum.Text
                    .customerName = Me.txtCustName.Text
                    .address = Me.txtAddress.Text
                    .zip = Me.txtZip.Text
                    .PhoneNum = Me.txtPhone.Text
                    .dealerId = Nothing
                    'Added for Req-610
                    .PhoneTypeId = Nothing
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub
#End Region

#Region "Helper functions"
        Private Sub populateSearchControls()
            Try
                'populate dealer list
                'Dim dvDealer As DataView = LookupListNew.GetDealerLookupList(ElitaPlusIdentity.Current.ActiveUser.Companies, False, "Code")
                'Me.BindListControlToDataView(ddlDealer, dvDealer, , , True)
                Dim oDealerList
                If Authentication.CurrentUser.IsDealerGroup Then
                    oDealerList = CaseBase.GetDealerListByCompanyForExternalUser()
                Else
                    oDealerList = GetDealerListByCompanyForUser()
                End If

                ddlDealer.Populate(oDealerList, New PopulateOptions() With
                                           {
                                            .AddBlankItem = True
                                           })

                If Me.State.dealerId <> Guid.Empty Then Me.SetSelectedItem(ddlDealer, Me.State.dealerId)

                'populate sort by list
                'Dim strRowFilter As String = "''IDENTIFICATION_NUMBER''"
                Dim sortByList As ListItem() = CommonConfigManager.Current.ListManager.GetList("CSDRP", Thread.CurrentPrincipal.GetLanguageCode())
                Dim filteredList As ListItem() = (From x In sortByList
                                                  Where x.Code <> "IDENTIFICATION_NUMBER"
                                                  Select x).ToArray()

                ddlSortBy.Populate(filteredList, New PopulateOptions() With
                                              {
                                                .AddBlankItem = False,
                                                .SortFunc = AddressOf PopulateOptions.GetDescription
                                               })

                If State.selectedSortById = Guid.Empty Then
                    State.selectedSortById = LookupListNew.GetIdFromCode(LookupListNew.LK_CERTIFICATE_SEARCH_FIELDS, "CUSTOMER_NAME")
                End If

                Me.SetSelectedItem(Me.ddlSortBy, Me.State.selectedSortById)

                'Added for Req-610
                Dim phoneType As ListItem() = CommonConfigManager.Current.ListManager.GetList("PHNRTP", Thread.CurrentPrincipal.GetLanguageCode())
                ddlPhoneType.Populate(phoneType, New PopulateOptions() With
                                              {
                                                .AddBlankItem = False,
                                                .SortFunc = AddressOf PopulateOptions.GetCode
                                               })
                If Me.State.PhoneTypeId <> Guid.Empty Then Me.SetSelectedItem(ddlPhoneType, Me.State.PhoneTypeId)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
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
                Me.txtCertNum.Text = Me.State.certificateNumber
                Me.txtCustName.Text = Me.State.customerName
                Me.txtAddress.Text = Me.State.address
                Me.txtZip.Text = Me.State.zip
                Me.txtPhone.Text = Me.State.PhoneNum
                If Me.State.dealerId <> Guid.Empty And ddlDealer.Items.Count > 0 Then Me.SetSelectedItem(ddlDealer, Me.State.dealerId)
                If State.selectedSortById <> Guid.Empty And ddlSortBy.Items.Count > 0 Then
                    Me.SetSelectedItem(ddlSortBy, Me.State.selectedSortById)
                End If

                'Added for Req-610
                If Me.State.PhoneTypeId <> Guid.Empty And ddlPhoneType.Items.Count > 0 Then Me.SetSelectedItem(ddlPhoneType, Me.State.PhoneTypeId)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub SetStateProperties()
            Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

            Try
                If Me.State Is Nothing Then
                    Me.RestoreState(New MyState)
                End If
                Me.State.certificateNumber = Me.txtCertNum.Text.ToUpper
                Me.State.customerName = Me.txtCustName.Text.ToUpper
                Me.State.address = Me.txtAddress.Text.ToUpper
                Me.State.zip = Me.txtZip.Text.ToUpper
                Me.State.PhoneNum = Me.txtPhone.Text.ToUpper
                Me.State.dealerId = Me.GetSelectedItem(ddlDealer)
                Me.State.dealerName = LookupListNew.GetCodeFromId("DEALERS", Me.State.dealerId)
                Me.State.selectedSortById = Me.GetSelectedItem(Me.ddlSortBy)

                'Added for Req-610
                Me.State.PhoneTypeId = Me.GetSelectedItem(Me.ddlPhoneType)
                Me.State.PhoneType = LookupListNew.GetCodeFromId("PHONE_NUMBER_TYPE", Me.State.PhoneTypeId)
                Me.SetSelectedItem(ddlPhoneType, Me.State.PhoneTypeId)
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateGrid()
            Try
                Dim oDataView As DataView
                Dim sortBy As String = "HOME_PHONE"
                If (Me.State.searchDV Is Nothing) Then
                    If (Not (Me.State.selectedSortById.Equals(Guid.Empty))) Then
                        sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CERTIFICATE_SEARCH_FIELDS, Me.State.selectedSortById)
                    End If
                    Me.State.searchDV = Certificate.GetCertificatesListByPhoneNum(State.PhoneType, State.PhoneNum, Me.State.certificateNumber,
                                                                            Me.State.customerName, Me.State.address,
                                                                            Me.State.zip, Me.State.dealerName, Authentication.CurrentUser.CompanyGroup.Id,
                                                                            Authentication.CurrentUser.NetworkId,
                                                                            sortBy)
                    If Me.State.searchClick Then
                        Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                        Me.State.searchClick = False
                    End If
                End If

                Grid.PageSize = State.PageSize
                If State.searchDV.Count = 0 Then
                    Dim dv As Certificate.PhoneNumberSearchDV = State.searchDV.AddNewRowToEmptyDV()
                    SetPageAndSelectedIndexFromGuid(dv, Me.State.selectedCertificateId, Me.Grid, Me.State.PageIndex)
                    Me.Grid.DataSource = dv
                Else
                    SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedCertificateId, Me.Grid, Me.State.PageIndex)
                    Me.Grid.DataSource = Me.State.searchDV
                End If

                Me.State.PageIndex = Me.Grid.PageIndex
                Me.Grid.AllowSorting = False

                'Added for Req-610
                If State.PhoneType = "HM" Then
                    Grid.Columns(GRID_COL_HOME_PHONE_IDX).Visible = True
                    Grid.Columns(GRID_COL_WORK_PHONE_IDX).Visible = False
                ElseIf State.PhoneType = "WC" Then
                    Grid.Columns(GRID_COL_HOME_PHONE_IDX).Visible = False
                    Grid.Columns(GRID_COL_WORK_PHONE_IDX).Visible = True
                End If

                Me.Grid.DataBind()

                HighLightGridViewSortColumn(Grid, sortBy)
                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Me.Grid.Visible)
                Session("recCount") = Me.State.searchDV.Count

                If Me.Grid.Visible Then
                    Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                If State.searchDV.Count = 0 Then
                    For Each gvRow As GridViewRow In Grid.Rows
                        gvRow.Visible = False
                        gvRow.Controls.Clear()
                    Next
                End If

            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Grid related"

        Private Sub Grid_PageIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                Me.State.PageIndex = Grid.PageIndex
                Me.State.selectedCertificateId = Guid.Empty
                PopulateGrid()
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
        Private Sub Grid_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If e.CommandName = "Select" Then
                    Dim lblCtrl As Label
                    Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                    Dim RowInd As Integer = row.RowIndex
                    lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_CERTIFICATE_ID_IDX).FindControl(Me.GRID_CTRL_CERTIFICATE_ID), Label)
                    Me.State.selectedCertificateId = New Guid(lblCtrl.Text)
                    Me.callPage(CertificateForm.URL, Me.State.selectedCertificateId)
                End If
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
            Catch ex As Exception
                HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                Me.State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Me.Grid.PageIndex = Me.State.PageIndex
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrControllerMaster)
            End Try
        End Sub


#End Region


    End Class
End Namespace