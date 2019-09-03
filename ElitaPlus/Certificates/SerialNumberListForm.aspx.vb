Namespace Certificates

    Partial Class SerialNumberListForm
        Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        Protected WithEvents ErrorCtrl As ErrorController

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region


#Region "Constants"
        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_CERTIFICATE_ID_IDX As Integer = 1
        Public Const GRID_COL_SERIAL_NUMBER_IDX As Integer = 2
        Public Const GRID_COL_CERTIFICATE_IDX As Integer = 3
        Public Const GRID_COL_STATUS_CODE_IDX As Integer = 4
        Public Const GRID_COL_DEALER_IDX As Integer = 5
        Public Const GRID_COL_PRODUCT_CODE_IDX As Integer = 6

        Public Const GRID_TOTAL_COLUMNS As Integer = 7

        Public Const MAX_LIMIT As Integer = 1000

#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public PageIndex As Integer = 0
            Public PageSize As Integer = 10
            Public serialNumber As String
            Public searchDV As Certificate.SerialNumberSearchDV = Nothing
            Public searchClick As Boolean = False
            Public certificatesFoundMSG As String
            Public selectedCertificateId As Guid = Guid.Empty
            Public IsGridVisible As Boolean = False

        End Class

        Public Sub New()
            MyBase.New(New MyState)
        End Sub

        Protected Shadows ReadOnly Property State() As MyState
            Get
                Return CType(MyBase.State, MyState)
            End Get
        End Property

        Private Sub Page_PageReturn(ByVal ReturnFromUrl As String, ByVal ReturnPar As Object) Handles MyBase.PageReturn
            Try
                Me.MenuEnabled = True
                Me.IsReturningFromChild = True
                'Me.State.searchDV = Nothing
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
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Page_Events"

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Page.RegisterHiddenField("__EVENTTARGET", Me.btnSearch.ClientID)
            Me.ErrorCtrl.Clear_Hide()
            '    Me.Redirect("..\Reports\ReportCeHistoryForm.aspx")
            Try
                If Not Me.IsPostBack Then
                    Me.SetDefaultButton(Me.moSerialNumberText, btnSearch)
                    Me.trPageSize.Visible = False
                    Me.State.certificatesFoundMSG = TranslationBase.TranslateLabelOrMessage(Message.MSG_CERTIFICATES_FOUND)
                    Me.GetStateProperties()
                    If Me.State.IsGridVisible Then
                        If Not (Me.State.PageSize = 10) Then
                            cboPageSize.SelectedValue = CType(Me.State.PageSize, String)
                            Grid.PageSize = Me.State.PageSize
                        End If
                        Me.PopulateGrid()
                    End If
                    Me.SetGridItemStyleColor(Me.Grid)

                Else
                    Me.SetStateProperties()
                End If
                Me.DisplayProgressBarOnClick(Me.btnSearch, "Loading_Certificates")
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
            Me.ShowMissingTranslations(Me.ErrorCtrl)
        End Sub

        Private Sub SetStateProperties()
            Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

            Me.State.serialNumber = Me.moSerialNumberText.Text.ToUpper
        End Sub

        Private Sub GetStateProperties()
            Me.moSerialNumberText.Text = Me.State.serialNumber
        End Sub


#End Region

#Region "Controlling Logic"

        Public Sub PopulateGrid()
            Try

                'Authentication.CurrentUser.CompanyGroup.Id
                'Authentication.CurrentUser.NetworkId
                '    Me.GetSelectedItem(moDealerDrop)
                Me.State.searchDV = Certificate.GetSerialNumberList(Me.State.serialNumber,
                                                                    Authentication.CurrentUser.CompanyGroup.Id,
                                                                    Authentication.CurrentUser.NetworkId)
                If Me.State.searchClick Then
                    Me.ValidSearchResultCount(Me.State.searchDV.Count, True)
                    Me.State.searchClick = False

                End If

                Me.State.searchDV.Sort = Grid.DataMember
                Me.Grid.AutoGenerateColumns = False

                SetPageAndSelectedIndexFromGuid(Me.State.searchDV, Me.State.selectedCertificateId, Me.Grid, Me.State.PageIndex)
                Me.State.PageIndex = Me.Grid.CurrentPageIndex
                Me.Grid.DataSource = Me.State.searchDV
                Me.Grid.AllowSorting = False
                Me.Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, Me.State.IsGridVisible)
                Me.trPageSize.Visible = Me.Grid.Visible

                Session("recCount") = Me.State.searchDV.Count

                If Me.State.searchDV.Count > 0 Then

                    If Me.Grid.Visible Then
                        Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & Me.State.certificatesFoundMSG
                    End If
                Else
                    If Me.Grid.Visible Then
                        Me.lblRecordCount.Text = Me.State.searchDV.Count & " " & Me.State.certificatesFoundMSG
                    End If
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

        'This method will change the Page Index and the Selected Index
        Public Function FindDVSelectedRowIndex(ByVal dv As Certificate.CertificateSearchDV) As Integer
            If Me.State.selectedCertificateId.Equals(Guid.Empty) Then
                Return -1
            Else
                'Jump to the Right Page
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If New Guid(CType(dv(i)(Certificate.CertificateSearchDV.COL_CERTIFICATE_ID), Byte())).Equals(Me.State.selectedCertificateId) Then
                        Return i
                    End If
                Next
            End If
            Return -1
        End Function


#End Region

#Region " Datagrid Related "

        'The Binding LOgic is here
        Private Sub Grid_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            Try
                If itemType = ListItemType.Item Or itemType = ListItemType.AlternatingItem Or itemType = ListItemType.SelectedItem Then
                    e.Item.Cells(Me.GRID_COL_CERTIFICATE_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(Certificate.SerialNumberSearchDV.COL_CERTIFICATE_ID), Byte()))
                    e.Item.Cells(Me.GRID_COL_SERIAL_NUMBER_IDX).Text = dvRow(Certificate.SerialNumberSearchDV.COL_SERIAL_NUMBER).ToString
                    e.Item.Cells(Me.GRID_COL_CERTIFICATE_IDX).Text = dvRow(Certificate.SerialNumberSearchDV.COL_CERTIFICATE_NUMBER).ToString
                    e.Item.Cells(Me.GRID_COL_STATUS_CODE_IDX).Text = dvRow(Certificate.SerialNumberSearchDV.COL_STATUS_CODE).ToString
                    e.Item.Cells(Me.GRID_COL_DEALER_IDX).Text = dvRow(Certificate.SerialNumberSearchDV.COL_DEALER).ToString
                    e.Item.Cells(Me.GRID_COL_PRODUCT_CODE_IDX).Text = dvRow(Certificate.SerialNumberSearchDV.COL_PRODUCT_CODE).ToString
                End If
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Public Sub ItemCommand(ByVal source As System.Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    Me.State.selectedCertificateId = New Guid(e.Item.Cells(Me.GRID_COL_CERTIFICATE_ID_IDX).Text)
                    Me.callPage(CertificateForm.URL, Me.State.selectedCertificateId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try

        End Sub

        Public Sub ItemCreated(ByVal sender As System.Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
            Try
                Me.State.PageIndex = e.NewPageIndex
                Me.State.selectedCertificateId = Guid.Empty
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(ByVal source As Object, ByVal e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                Me.State.PageSize = Grid.PageSize
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub


#End Region

#Region " Buttons Clicks "

        Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
            Try
                Me.State.PageIndex = 0
                Me.State.selectedCertificateId = Guid.Empty
                Me.State.IsGridVisible = True
                Me.State.searchClick = True
                Me.State.searchDV = Nothing
                Me.PopulateGrid()
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

        Private Sub btnClearSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSearch.Click
            Try
                Me.moSerialNumberText.Text = String.Empty
                'Update Page State
                With Me.State
                    .serialNumber = Me.moSerialNumberText.Text
                End With
            Catch ex As Exception
                Me.HandleErrors(ex, Me.ErrorCtrl)
            End Try
        End Sub

#End Region



    End Class

End Namespace
