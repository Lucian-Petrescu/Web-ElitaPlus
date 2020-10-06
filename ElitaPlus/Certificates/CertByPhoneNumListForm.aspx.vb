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
        Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
            ErrControllerMaster.Clear_Hide()
            Form.DefaultButton = btnSearch.UniqueID
            Try
                If Not IsPostBack Then
                    SetFormTitle(PAGETITLE)
                    SetFormTab(PAGETAB)
                    TranslateGridHeader(Grid)

                    GetStateProperties()
                    If Not IsReturningFromChild Then
                        ControlMgr.SetVisibleControl(Me, trPageSize, False)
                        If ElitaPlusIdentity.Current.ActiveUser.IsDealer Then
                            State.dealerId = ElitaPlusIdentity.Current.ActiveUser.ScDealerId
                            ControlMgr.SetEnableControl(Me, ddlDealer, False)
                        End If
                    End If

                    populateSearchControls()

                    If IsReturningFromChild Then
                        ' It is returning from detail
                        PopulateGrid()
                    End If
                    SetFocus(txtPhone)
                End If
                DisplayProgressBarOnClick(btnSearch, "Loading_Certificates")
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
            ShowMissingTranslations(ErrControllerMaster)
        End Sub

        Private Sub CertByPhoneNumListForm_LoadComplete(sender As Object, e As System.EventArgs) Handles Me.LoadComplete
            cboPageSize.Visible = Grid.Visible
            lblRecordCount.Visible = Grid.Visible

            If ErrControllerMaster.Visible Then
                If Grid.Visible And Grid.Rows.Count < 10 Then
                    Dim fillerHight As Integer = 200
                    fillerHight = fillerHight - Grid.Rows.Count * 20
                    spanFiller.Text = "<tr><td colspan=""2"" style=""height:" & fillerHight & "px"">&nbsp;</td></tr>"
                End If
            Else
                spanFiller.Text = ""
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
                    Case ElitaPlusPage.DetailPageCommand.Delete
                        AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                End Select
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
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
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
            Try
                txtCertNum.Text = String.Empty
                txtCustName.Text = String.Empty
                txtAddress.Text = String.Empty
                txtZip.Text = String.Empty
                txtPhone.Text = String.Empty
                If Not ElitaPlusIdentity.Current.ActiveUser.IsDealer Then ddlDealer.SelectedIndex = 0

                'Added for Req-610
                ddlPhoneType.SelectedIndex = 0

                'Update Page State
                With State
                    .certificateNumber = txtCertNum.Text
                    .customerName = txtCustName.Text
                    .address = txtAddress.Text
                    .zip = txtZip.Text
                    .PhoneNum = txtPhone.Text
                    .dealerId = Nothing
                    'Added for Req-610
                    .PhoneTypeId = Nothing
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
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

                If State.dealerId <> Guid.Empty Then SetSelectedItem(ddlDealer, State.dealerId)

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

                SetSelectedItem(ddlSortBy, State.selectedSortById)

                'Added for Req-610
                Dim phoneType As ListItem() = CommonConfigManager.Current.ListManager.GetList("PHNRTP", Thread.CurrentPrincipal.GetLanguageCode())
                ddlPhoneType.Populate(phoneType, New PopulateOptions() With
                                              {
                                                .AddBlankItem = False,
                                                .SortFunc = AddressOf PopulateOptions.GetCode
                                               })
                If State.PhoneTypeId <> Guid.Empty Then SetSelectedItem(ddlPhoneType, State.PhoneTypeId)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
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

        Private Sub GetStateProperties()
            Try
                txtCertNum.Text = State.certificateNumber
                txtCustName.Text = State.customerName
                txtAddress.Text = State.address
                txtZip.Text = State.zip
                txtPhone.Text = State.PhoneNum
                If State.dealerId <> Guid.Empty And ddlDealer.Items.Count > 0 Then SetSelectedItem(ddlDealer, State.dealerId)
                If State.selectedSortById <> Guid.Empty And ddlSortBy.Items.Count > 0 Then
                    SetSelectedItem(ddlSortBy, State.selectedSortById)
                End If

                'Added for Req-610
                If State.PhoneTypeId <> Guid.Empty And ddlPhoneType.Items.Count > 0 Then SetSelectedItem(ddlPhoneType, State.PhoneTypeId)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub SetStateProperties()
            Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

            Try
                If State Is Nothing Then
                    RestoreState(New MyState)
                End If
                State.certificateNumber = txtCertNum.Text.ToUpper
                State.customerName = txtCustName.Text.ToUpper
                State.address = txtAddress.Text.ToUpper
                State.zip = txtZip.Text.ToUpper
                State.PhoneNum = txtPhone.Text.ToUpper
                State.dealerId = GetSelectedItem(ddlDealer)
                State.dealerName = LookupListNew.GetCodeFromId("DEALERS", State.dealerId)
                State.selectedSortById = GetSelectedItem(ddlSortBy)

                'Added for Req-610
                State.PhoneTypeId = GetSelectedItem(ddlPhoneType)
                State.PhoneType = LookupListNew.GetCodeFromId("PHONE_NUMBER_TYPE", State.PhoneTypeId)
                SetSelectedItem(ddlPhoneType, State.PhoneTypeId)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub PopulateGrid()
            Try
                Dim oDataView As DataView
                Dim sortBy As String = "HOME_PHONE"
                If (State.searchDV Is Nothing) Then
                    If (Not (State.selectedSortById.Equals(Guid.Empty))) Then
                        sortBy = LookupListNew.GetCodeFromId(LookupListNew.LK_CERTIFICATE_SEARCH_FIELDS, State.selectedSortById)
                    End If
                    State.searchDV = Certificate.GetCertificatesListByPhoneNum(State.PhoneType, State.PhoneNum, State.certificateNumber,
                                                                            State.customerName, State.address,
                                                                            State.zip, State.dealerName, Authentication.CurrentUser.CompanyGroup.Id,
                                                                            Authentication.CurrentUser.NetworkId,
                                                                            sortBy)
                    If State.searchClick Then
                        ValidSearchResultCount(State.searchDV.Count, True)
                        State.searchClick = False
                    End If
                End If

                Grid.PageSize = State.PageSize
                If State.searchDV.Count = 0 Then
                    Dim dv As Certificate.PhoneNumberSearchDV = State.searchDV.AddNewRowToEmptyDV()
                    SetPageAndSelectedIndexFromGuid(dv, State.selectedCertificateId, Grid, State.PageIndex)
                    Grid.DataSource = dv
                Else
                    SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedCertificateId, Grid, State.PageIndex)
                    Grid.DataSource = State.searchDV
                End If

                State.PageIndex = Grid.PageIndex
                Grid.AllowSorting = False

                'Added for Req-610
                If State.PhoneType = "HM" Then
                    Grid.Columns(GRID_COL_HOME_PHONE_IDX).Visible = True
                    Grid.Columns(GRID_COL_WORK_PHONE_IDX).Visible = False
                ElseIf State.PhoneType = "WC" Then
                    Grid.Columns(GRID_COL_HOME_PHONE_IDX).Visible = False
                    Grid.Columns(GRID_COL_WORK_PHONE_IDX).Visible = True
                End If

                Grid.DataBind()

                HighLightGridViewSortColumn(Grid, sortBy)
                ControlMgr.SetVisibleControl(Me, Grid, True)
                ControlMgr.SetVisibleControl(Me, trPageSize, Grid.Visible)
                Session("recCount") = State.searchDV.Count

                If Grid.Visible Then
                    lblRecordCount.Text = State.searchDV.Count & " " & TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)
                End If
                If Not Grid.BottomPagerRow.Visible Then Grid.BottomPagerRow.Visible = True

                If State.searchDV.Count = 0 Then
                    For Each gvRow As GridViewRow In Grid.Rows
                        gvRow.Visible = False
                        gvRow.Controls.Clear()
                    Next
                End If

            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

#End Region

#Region "Grid related"

        Private Sub Grid_PageIndexChanged(sender As Object, e As System.EventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = Grid.PageIndex
                State.selectedCertificateId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles Grid.PageIndexChanging
            Try
                Grid.PageIndex = e.NewPageIndex
                State.PageIndex = Grid.PageIndex
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub
        Private Sub Grid_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Grid.RowCommand
            Try
                If e.CommandName = "Select" Then
                    Dim lblCtrl As Label
                    Dim row As GridViewRow = CType(CType(e.CommandSource, Control).Parent.Parent, GridViewRow)
                    Dim RowInd As Integer = row.RowIndex
                    lblCtrl = CType(Grid.Rows(RowInd).Cells(GRID_COL_CERTIFICATE_ID_IDX).FindControl(GRID_CTRL_CERTIFICATE_ID), Label)
                    State.selectedCertificateId = New Guid(lblCtrl.Text)
                    callPage(CertificateForm.URL, State.selectedCertificateId)
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_RowCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowCreated
            Try
                BaseItemCreated(sender, e)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles Grid.RowDataBound
            Try
                BaseItemBound(sender, e)
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                State.PageSize = CType(cboPageSize.SelectedValue, Integer)
                State.PageIndex = NewCurrentPageIndex(Grid, State.searchDV.Count, State.PageSize)
                Grid.PageIndex = State.PageIndex
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrControllerMaster)
            End Try
        End Sub


#End Region


    End Class
End Namespace