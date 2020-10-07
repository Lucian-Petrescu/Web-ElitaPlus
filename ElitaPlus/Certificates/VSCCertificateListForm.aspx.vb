Namespace Certificates

    Partial Class VSCCertificateListForm
        Inherits ElitaPlusSearchPage

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub
        'Protected WithEvents ErrorCtrl As ErrorController

        'NOTE: The following placeholder declaration is required by the Web Form Designer.
        'Do not delete or move it.
        Private designerPlaceholderDeclaration As System.Object

        Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region


#Region "Constants"
        Public Const GRID_COL_EDIT_IDX As Integer = 0
        Public Const GRID_COL_CERTIFICATE_ID_IDX As Integer = 1
        Public Const GRID_COL_VEHICLE_LICENSE_TAG_IDX As Integer = 2
        Public Const GRID_COL_CERTIFICATE_IDX As Integer = 3
        Public Const GRID_COL_STATUS_CODE_IDX As Integer = 4
        Public Const GRID_COL_CUSTOMER_NAME_IDX As Integer = 5

        Public Const GRID_TOTAL_COLUMNS As Integer = 6

        Public Const MAX_LIMIT As Integer = 1000

#End Region

#Region "Page State"
        Private IsReturningFromChild As Boolean = False

        Class MyState
            Public PageIndex As Integer = 0
            Public PageSize As Integer = 10
            Public VehicleLicenceTag As String
            Public searchDV As Certificate.VehicleLicenseFlagSearchDV = Nothing
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

        Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
            Try
                MenuEnabled = True
                IsReturningFromChild = True
                'Me.State.searchDV = Nothing
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
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region

#Region "Page_Events"

        Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            Page.RegisterHiddenField("__EVENTTARGET", btnSearch.ClientID)
            ErrorCtrl.Clear_Hide()
            Try
                If Not IsPostBack Then
                    SetDefaultButton(moVehicleLicenceTagText, btnSearch)
                    trPageSize.Visible = False
                    State.certificatesFoundMSG = TranslationBase.TranslateLabelOrMessage(Message.MSG_CERTIFICATES_FOUND)
                    GetStateProperties()
                    If State.IsGridVisible Then
                        If Not (State.PageSize = 10) Then
                            cboPageSize.SelectedValue = CType(State.PageSize, String)
                            Grid.PageSize = State.PageSize
                        End If
                        PopulateGrid()
                    End If
                    SetGridItemStyleColor(Grid)

                Else
                    SetStateProperties()
                End If
                DisplayProgressBarOnClick(btnSearch, "Loading_Certificates")
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
            ShowMissingTranslations(ErrorCtrl)
        End Sub

        Private Sub SetStateProperties()
            Dim oCompanyId As Guid = ElitaPlusIdentity.Current.ActiveUser.FirstCompanyID

            State.VehicleLicenceTag = moVehicleLicenceTagText.Text.ToUpper
        End Sub

        Private Sub GetStateProperties()
            moVehicleLicenceTagText.Text = State.VehicleLicenceTag
        End Sub


#End Region

#Region "Controlling Logic"

        Public Sub PopulateGrid()
            Try
                '    Me.GetSelectedItem(moDealerDrop)
                State.searchDV = Certificate.GetVehicleLicenseFlagList(State.VehicleLicenceTag)
                If State.searchClick Then
                    ValidSearchResultCount(State.searchDV.Count, True)
                    State.searchClick = False

                End If

                State.searchDV.Sort = Grid.DataMember
                Grid.AutoGenerateColumns = False

                SetPageAndSelectedIndexFromGuid(State.searchDV, State.selectedCertificateId, Grid, State.PageIndex)
                State.PageIndex = Grid.CurrentPageIndex
                Grid.DataSource = State.searchDV
                Grid.AllowSorting = False
                Grid.DataBind()

                ControlMgr.SetVisibleControl(Me, Grid, State.IsGridVisible)
                trPageSize.Visible = Grid.Visible

                Session("recCount") = State.searchDV.Count

                If State.searchDV.Count > 0 Then

                    If Grid.Visible Then
                        lblRecordCount.Text = State.searchDV.Count & " " & State.certificatesFoundMSG
                    End If
                Else
                    If Grid.Visible Then
                        lblRecordCount.Text = State.searchDV.Count & " " & State.certificatesFoundMSG
                    End If
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

        'This method will change the Page Index and the Selected Index
        Public Function FindDVSelectedRowIndex(dv As Certificate.CertificateSearchDV) As Integer
            If State.selectedCertificateId.Equals(Guid.Empty) Then
                Return -1
            Else
                'Jump to the Right Page
                Dim i As Integer
                For i = 0 To dv.Count - 1
                    If New Guid(CType(dv(i)(Certificate.CertificateSearchDV.COL_CERTIFICATE_ID), Byte())).Equals(State.selectedCertificateId) Then
                        Return i
                    End If
                Next
            End If
            Return -1
        End Function


#End Region

#Region "Datagrid Related "

        'The Binding LOgic is here
        Private Sub Grid_ItemDataBound(sender As Object, e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles Grid.ItemDataBound
            Dim itemType As ListItemType = CType(e.Item.ItemType, ListItemType)
            Dim dvRow As DataRowView = CType(e.Item.DataItem, DataRowView)

            Try
                If itemType = ListItemType.Item OrElse itemType = ListItemType.AlternatingItem OrElse itemType = ListItemType.SelectedItem Then
                    e.Item.Cells(GRID_COL_CERTIFICATE_ID_IDX).Text = GetGuidStringFromByteArray(CType(dvRow(Certificate.VehicleLicenseFlagSearchDV.COL_CERTIFICATE_ID), Byte()))
                    e.Item.Cells(GRID_COL_VEHICLE_LICENSE_TAG_IDX).Text = dvRow(Certificate.VehicleLicenseFlagSearchDV.COL_VEHICLE_LICENSE_TAG).ToString
                    e.Item.Cells(GRID_COL_CERTIFICATE_IDX).Text = dvRow(Certificate.VehicleLicenseFlagSearchDV.COL_CERTIFICATE_NUMBER).ToString
                    e.Item.Cells(GRID_COL_STATUS_CODE_IDX).Text = dvRow(Certificate.VehicleLicenseFlagSearchDV.COL_STATUS_CODE).ToString
                    e.Item.Cells(GRID_COL_CUSTOMER_NAME_IDX).Text = dvRow(Certificate.VehicleLicenseFlagSearchDV.COL_CUSTOMER_NAME).ToString
                End If
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Public Sub ItemCommand(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
            Try
                If e.CommandName = "SelectAction" Then
                    State.selectedCertificateId = New Guid(e.Item.Cells(GRID_COL_CERTIFICATE_ID_IDX).Text)
                    callPage(CertificateForm.URL, State.selectedCertificateId)
                End If
            Catch ex As Threading.ThreadAbortException
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try

        End Sub

        Public Sub ItemCreated(sender As System.Object, e As System.Web.UI.WebControls.DataGridItemEventArgs)
            BaseItemCreated(sender, e)
        End Sub

        Private Sub Grid_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles Grid.PageIndexChanged
            Try
                State.PageIndex = e.NewPageIndex
                State.selectedCertificateId = Guid.Empty
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub Grid_PageSizeChanged(source As Object, e As System.EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                Grid.CurrentPageIndex = NewCurrentPageIndex(Grid, CType(Session("recCount"), Int32), CType(cboPageSize.SelectedValue, Int32))
                State.PageSize = Grid.PageSize
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub


#End Region

#Region " Buttons Clicks "

        Private Sub btnSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnSearch.Click
            Try
                State.PageIndex = 0
                State.selectedCertificateId = Guid.Empty
                State.IsGridVisible = True
                State.searchClick = True
                State.searchDV = Nothing
                PopulateGrid()
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

        Private Sub btnClearSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSearch.Click
            Try
                moVehicleLicenceTagText.Text = String.Empty
                'Update Page State
                With State
                    .VehicleLicenceTag = moVehicleLicenceTagText.Text
                End With
            Catch ex As Exception
                HandleErrors(ex, ErrorCtrl)
            End Try
        End Sub

#End Region



    End Class

End Namespace
