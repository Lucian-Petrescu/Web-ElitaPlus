Imports System.Collections.Generic
Imports System.Windows.Forms

Namespace Common

    Public Class UserControlApInvoiceLinesSearch
        Inherits UI.UserControl

#Region "Constant"
        Public Class PageConstant
            Public Const ServiceCenter As String = "ServiceCenter"
            Public Const ServiceCenterId As String = "ServiceCenterId"
            Public Const CountryId As String = "CountryId"
            Public Const ApInvoiceHeaderId As String = "ApInvoiceHeaderId"
            Public Const CompanyCode As String = "CompanyCode"
            Public Const CountryCode As String = "CountryCode"
            Public Const Dealer As String = "Dealer"
            Public Const TotalInvoiceLines As String = "TotalLines"
            Public Const PageIndex As String = "PageIndex"
            Public Const PageSize As String = "PageSize"
            Public Const SortExpression As String = "SortExpression"
            Public Const GridColCheckBoxIdx As Integer = 0
            
            Public Const GridColAuthSelectAllCtrl  As String = "checkBoxAll"
            Public Const GridColAuthSelectCtrl  As String = "checkBoxSelected"
            Public Const GridColPoLineSelectAllCtrl  As String = "checkBoxLinesAll"
            Public Const GridColPoLineSelectCtrl  As String = "checkBoxLinesSelected"
            Public Const GridColAuthorizationIdx As Integer = 5
            Public Const GridColLineType As Integer = 1
            Public Const GridColVendorItemCode As Integer = 2
            Public Const GridColDescription As Integer = 3
            Public Const GridColQty As Integer = 4
            Public Const GridColUnitPrice As Integer = 5
            Public Const GridColUomXcd As Integer = 6
            Public Const GridColPoNumber As Integer = 7
            public Const GridColAuthorizationDataField As string = "Claim_Authorization_id"
        End Class
        Private ReadOnly DefaultPageSize As Integer = 5
      
#End Region

#Region "Properties"

        Public Property HostMessageController As IMessageController
        Public Property ElitaHostPage As ElitaPlusPage
        Public Property ApInvoiceHeaderId As Guid
            Get
                Return DirectCast(ViewState(PageConstant.ApInvoiceHeaderId), Guid)
            End Get
            Set(value As Guid)
                ViewState(PageConstant.ApInvoiceHeaderId) = value
            End Set
        End Property
        Public Property CountryId As Guid
            Get
                Return DirectCast(ViewState(PageConstant.CountryId), Guid)
            End Get
            Set(value As Guid)
                ViewState(PageConstant.CountryId) = value
            End Set
        End Property
        Public Property CompanyId As Guid
            Get
                Return DirectCast(ViewState(PageConstant.CompanyCode), Guid)
            End Get
            Set(value As Guid)
                ViewState(PageConstant.CompanyCode) = value
            End Set
        End Property
        Public Property PageIndex As Integer
            Get
                Return DirectCast(ViewState(PageConstant.PageIndex), Integer)
            End Get
            Set(value As Integer)
                ViewState(PageConstant.PageIndex) = value
            End Set
        End Property
        Public Property PageSize As Integer
            Get
                If ViewState(PageConstant.PageSize) IsNot Nothing Then
                    Return DirectCast(ViewState(PageConstant.PageSize), Integer)
                Else
                    Return DefaultPageSize
                End If
            End Get
            Set(value As Integer)
                ViewState(PageConstant.PageSize) = value
            End Set
        End Property
        Public Property SortExpression As String
            Get
                Return DirectCast(ViewState(PageConstant.SortExpression), String)
            End Get
            Set(value As String)
                ViewState(PageConstant.SortExpression) = value
            End Set
        End Property
        Public Property CountryCode As String
            Get
                Return DirectCast(ViewState(PageConstant.CountryCode), String)
            End Get
            Set(value As String)
                ViewState(PageConstant.CountryCode) = value
            End Set
        End Property
        Public Property Dealer As String
            Get
                Return DirectCast(ViewState(PageConstant.Dealer), String)
            End Get
            Set(value As String)
                ViewState(PageConstant.Dealer) = value
            End Set
        End Property
        Public Property ServiceCenter As String
            Get
                Return DirectCast(ViewState(PageConstant.ServiceCenter), String)
            End Get
            Set(value As String)
                ViewState(PageConstant.ServiceCenter) = value
            End Set
        End Property
        Public Property ServiceCenterId As Guid
            Get
                Return DirectCast(ViewState(PageConstant.ServiceCenterId), Guid)
            End Get
            Set(value As Guid)
                ViewState(PageConstant.ServiceCenterId) = value
            End Set
        End Property
        Public Property TotalInvoiceLines As Integer
            Get
                Return DirectCast(ViewState(PageConstant.TotalInvoiceLines), Integer)
            End Get
            Set(value As Integer)
                ViewState(PageConstant.TotalInvoiceLines) = value
            End Set
        End Property
        Public Property TranslateGridHeaderFunc As Action(Of GridView)
        Public Property TranslationFunc As Func(Of String, String)
        Public Property NewCurrentPageIndexFunc As Func(Of GridView, Integer, Integer, Integer)

#End Region

#Region "Control Status"

        Private Sub SetControlState(ByVal isVisible As Boolean)
            ControlMgr.SetVisibleControl(ElitaHostPage, linesGridHeader, isVisible)
            ControlMgr.SetVisibleControl(ElitaHostPage, btnSearchLines, isVisible)
            ControlMgr.SetVisibleControl(ElitaHostPage, btnAddSelectedPoLines, isVisible)
        End Sub
        Private sub SetControlForPoLineSearchResult()
            ControlMgr.SetVisibleControl(ElitaHostPage, linesGridHeader, True)
            ControlMgr.SetVisibleControl(ElitaHostPage, GridAuth, false)
            ControlMgr.SetVisibleControl(ElitaHostPage, GridPoLines, True)
            ControlMgr.SetVisibleControl(ElitaHostPage, authGridTitle, false)
            ControlMgr.SetVisibleControl(ElitaHostPage, AuthGridPageSize, false)
            ControlMgr.SetVisibleControl(ElitaHostPage, PoLineGridPageSize, true)
            ControlMgr.SetVisibleControl(ElitaHostPage, poLineGridTitle, true)
            ControlMgr.SetVisibleControl(ElitaHostPage, btnSearchLines, False)
          
        End sub


#End Region

#Region "Page Event"

        Sub InitializeComponent()
            Try

                SetControlState(False)
                TranslateGridHeaderFunc.Invoke(GridPoLines)
                TranslateGridHeaderFunc.Invoke(GridAuth)
                SetTranslations()
                txtServiceCenter.Text = ServiceCenter
                ControlMgr.SetVisibleControl(ElitaHostPage, PoLineGridPageSize, False)
                ControlMgr.SetVisibleControl(ElitaHostPage, divSearchError, false)
                ControlMgr.SetVisibleControl(ElitaHostPage, divSearchError, false)

            Catch ex As Exception
                HandleLocalException(ex)
            End Try
        End Sub
        sub CancelAndClearSearchResult()
            Try
                GridAuth.DataSource = nothing
                GridAuth.DataBind()
                UpdateRecordCount(0)
                GridPoLines.DataSource = nothing
                GridPoLines.DataBind()
                UpdatePoLinesRecordCount(0)
                ControlMgr.SetVisibleControl(ElitaHostPage, divSearchError, false)
                ControlMgr.SetVisibleControl(ElitaHostPage, divAddLinesStatus, false)
            Catch ex As Exception

            End Try
        End sub
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Dim parentControl = Parent
            While (Not TypeOf parentControl Is ElitaPlusPage And parentControl IsNot Nothing)
                parentControl = parentControl.Parent
            End While
            ElitaHostPage = DirectCast(parentControl, ElitaPlusPage)
            txtServiceCenter.Text = ServiceCenter
        
        End Sub
        Private Sub UserControlApInvoiceLinesSearch_PreRender(sender As Object, e As EventArgs) Handles Me.PreRender
           
            If GridAuth.Visible = True AndAlso Not GridAuth.datasource Is Nothing Then
              
                Dim chkboxAuth As WebControls.CheckBox
                chkboxAuth = CType(GridAuth.HeaderRow.FindControl(PageConstant.GridColAuthSelectAllCtrl), WebControls.CheckBox)
                If (Not chkboxAuth Is Nothing) Then
                    chkboxAuth.InputAttributes("class") = "checkboxHeader"
                End If

                For Each row As GridViewRow In GridAuth.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        chkboxAuth = CType(row.Cells(PageConstant.GridColCheckBoxIdx).FindControl(PageConstant.GridColAuthSelectCtrl),  WebControls.CheckBox)
                        If (Not chkboxAuth Is Nothing) Then
                            chkboxAuth.InputAttributes("class") = "checkboxLine"
                        End If
                    End If
                Next
          End If

            If GridPoLines.Visible = True AndAlso Not GridPoLines.datasource Is Nothing Then
              
                Dim chkboxPoLine As WebControls.CheckBox
                chkboxPoLine = CType(GridPoLines.HeaderRow.FindControl(PageConstant.GridColPoLineSelectAllCtrl), WebControls.CheckBox)
                If (Not chkboxPoLine Is Nothing) Then
                    chkboxPoLine.InputAttributes("class") = "checkboxHeader"
                End If

                For Each row As GridViewRow In GridPoLines.Rows
                    If row.RowType = DataControlRowType.DataRow Then
                        chkboxPoLine = CType(row.Cells(PageConstant.GridColCheckBoxIdx).FindControl(PageConstant.GridColPoLineSelectCtrl),  WebControls.CheckBox)
                        If (Not chkboxPoLine Is Nothing) Then
                            chkboxPoLine.InputAttributes("class") = "checkboxLine"
                        End If
                    End If
                Next
            End If

        End Sub
#End Region

#Region "Page Internal"
        Sub HandleLocalException(ex As Exception)
            Dim errorMessage As String = $"{ex.Message} {ex.StackTrace}"
            If HostMessageController IsNot Nothing Then
                HostMessageController.AddError(errorMessage, True)
            End If
        End Sub
        Private Sub SetTranslations()
            If TranslationFunc Is Nothing Then
                Throw New ArgumentException("The Translation lambda function not initialized")
            End If

            lblPageSize.Text = TranslationFunc("Page_Size")
            btnClearSearch.Text = TranslationFunc("Clear")
            btnSearch.Text = TranslationFunc("Search")
            btnSearchLines.Text = TranslationFunc("load_lines")

        End Sub

        Private Sub ClearResultList()
            txtAuthorizationNumber.Text = String.Empty
            txtClaimNumber.Text = String.Empty
            ControlMgr.SetVisibleControl(ElitaHostPage, btnSearchLines, False)
            ControlMgr.SetVisibleControl(ElitaHostPage, authGridTitle, true)
            ControlMgr.SetVisibleControl(ElitaHostPage, AuthGridPageSize, true)
            ControlMgr.SetVisibleControl(ElitaHostPage, poLineGridTitle, false)
            ControlMgr.SetVisibleControl(ElitaHostPage, PoLineGridPageSize, false)
            ControlMgr.SetVisibleControl(ElitaHostPage, divSearchError, false)
            ControlMgr.SetVisibleControl(ElitaHostPage, btnAddSelectedPoLines, false)
            GridAuth.DataSource = Nothing
            GridAuth.DataBind()
            GridPoLines.DataSource = Nothing
            GridPoLines.DataBind()
            UpdateRecordCount(0)
            UpdatePoLinesRecordCount(0)
        End Sub
        Private Sub UpdateRecordCount(records As Integer)
                lblRecordCount.Text = $"{records} {TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)}"
        End Sub
        Private Sub UpdatePoLinesRecordCount(records As Integer)
                lblPoLinesRecords.Text = $"{records} {TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)}"
        End Sub

        Private Function GetSelectedAuthorizationIds() As List(Of Guid)
            Dim chkbox As WebControls.CheckBox, claimIdStr As String
            Dim selectedAuth As List(Of Guid) = New List(Of Guid)

            For Each row As GridViewRow In GridAuth.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    chkbox = CType(row.Cells(PageConstant.GridColCheckBoxIdx).FindControl(PageConstant.GridColAuthSelectCtrl), WebControls.CheckBox)
                    If chkbox.Checked Then
                        claimIdStr = row.Cells(PageConstant.GridColAuthorizationIdx).Text.Trim
                        If String.IsNullOrEmpty(claimIdStr) = False Then
                            selectedAuth.Add(New Guid(claimIdStr))
                        End If
                    End If
                End If
            Next
            Return selectedAuth
        End Function
        Private Function PoLineSelected() As Boolean
            Dim chkbox As WebControls.CheckBox
            For Each row As GridViewRow In GridPoLines.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    chkbox = CType(row.Cells(PageConstant.GridColCheckBoxIdx).FindControl(PageConstant.GridColPoLineSelectCtrl), WebControls.CheckBox)
                    If chkbox.Checked Then
                      Return True
                    End If
                End If
            Next
            Return False
        End Function
        public Function AddSelectedPoLines() As Integer 
            Dim chkbox As WebControls.CheckBox
            Dim lineAdded as Integer = 0
            Dim lastLineNUmber as Integer = TotalInvoiceLines
            Dim apInvoiceLinesBo 
           Try
               For Each row As GridViewRow In GridPoLines.Rows
                   If row.RowType = DataControlRowType.DataRow Then
                       chkbox = CType(row.Cells(PageConstant.GridColCheckBoxIdx).FindControl(PageConstant.GridColPoLineSelectCtrl), WebControls.CheckBox)
                       If chkbox.Checked Then
                           apInvoiceLinesBo = new ApInvoiceLines

                           With   apInvoiceLinesBo
                               .ApInvoiceHeaderId = ApInvoiceHeaderId
                               .PaidQuantity = If(.PaidQuantity, 0)
                               .MatchedQuantity = If(.MatchedQuantity, 0)
                               .LineNumber = lastLineNUmber + lineAdded + 1
                               .LineType = row.Cells(PageConstant.GridColLineType ).Text.Trim
                               .VendorItemCode = row.Cells(PageConstant.GridColVendorItemCode).Text.Trim
                               .Description = row.Cells(PageConstant.GridColDescription).Text.Trim
                               .Quantity = 0
                               .UnitPrice = row.Cells(PageConstant.GridColUnitPrice).Text.Trim
                               .TotalPrice = 0
                               .UomXcd = row.Cells(PageConstant.GridColUomXcd).Text.Trim
                               .PoNumber = row.Cells(PageConstant.GridColPoNumber).Text.Trim
                               .Save()
                           end With
                           lineAdded += 1
                       End If
                   End If
               Next
               lblAddLinesStatus.Text = lineAdded.ToString() + " " + TranslationBase.TranslateLabelOrMessage(Message.MSG_PO_LINES_ADDED)
               ControlMgr.SetVisibleControl(ElitaHostPage, divAddLinesStatus, true) 
               TotalInvoiceLines +=lineAdded
               Return lineAdded
           Catch ex As Exception
               HandleLocalException(ex)
               TotalInvoiceLines +=lineAdded
               Return lineAdded
           End Try
        End Function

        private function ValidAuthorizationSearchCriteria() As Boolean
            If  String.IsNullOrEmpty(txtClaimNumber.Text.Trim()) AndAlso String.IsNullOrEmpty(txtAuthorizationNumber.Text.Trim()) Then
                lblSearchError.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_CLAIM_NUMBER_OR_AUTHORIZATION_NUMBER_REQUIRED)
                ControlMgr.SetVisibleControl(ElitaHostPage, divSearchError, true)
                return false
            End If
            Return true
        End function
#End Region

#Region "Authorization Grid"
        Private Sub PopulateAuthorizationGrid()

            Dim authDv As DataView = GetAuthorizationGridDataView()
            GridAuth.DataSource = authDv
            UpdateRecordCount(authDv.Count)
            GridAuth.PageSize = PageSize
            GridAuth.DataBind()
            Session("authRecCount") = authDv.Count
            if Not authDv is Nothing AndAlso authDv.Count > 0 then
                ControlMgr.SetVisibleControl(ElitaHostPage, btnSearchLines, True)
            End If

           
        End Sub
        Private Function GetAuthorizationGridDataView() As DataView
            Dim apInvoiceLinesBo As New ApInvoiceLines
            Return apInvoiceLinesBo.GetAuthorization(ServiceCenterId, txtClaimNumber.Text, txtAuthorizationNumber.Text)
        End Function
        Private Sub GridAuth_PageIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridAuth.PageIndexChanged
            Try
                PageIndex = GridAuth.PageIndex
                PopulateAuthorizationGrid()
            Catch ex As Exception
                HandleLocalException(ex)
            End Try
        End Sub

        Private Sub Grid_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GridAuth.PageIndexChanging
            Try
                GridAuth.PageIndex = e.NewPageIndex
                PageIndex = GridAuth.PageIndex
            Catch ex As Exception
                HandleLocalException(ex)
            End Try
        End Sub

        Private Sub cboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboPageSize.SelectedIndexChanged
            Try
                PageSize = CType(cboPageSize.SelectedValue, Integer)
                PageIndex = NewCurrentPageIndexFunc.Invoke(GridAuth,  CType(Session("authRecCount"), Int32) , PageSize)
                GridAuth.PageIndex = PageIndex
                PopulateAuthorizationGrid()
           
            Catch ex As Exception
                HandleLocalException(ex)
            End Try
        End Sub

        Private Sub Grid_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles GridAuth.RowDataBound
            Dim dvRow As DataRowView = CType(e.Row.DataItem, DataRowView)
            Try
                If e.Row.RowType = DataControlRowType.DataRow Then
                    ElitaHostPage.PopulateControlFromBOProperty(e.Row.Cells(PageConstant.GridColAuthorizationIdx), GetGuidStringFromByteArray(dvRow(PageConstant.GridColAuthorizationDataField)))
                End If
            Catch ex As Exception
                HandleLocalException(ex)
            End Try
        End Sub

        Private Function GetGuidStringFromByteArray(ByVal value As Byte()) As String
            If value  is Nothing Then
                Return Guid.Empty.ToString()
            Else 
                Return New Guid(value).ToString
            End If
       
        End Function

#End Region

#Region "Po Line Grid"
        Private Sub PopulatePoLinesGrid()
            Dim poLinesDv As DataView = GetPoLinesGridDataView()
            If Not poLinesDv Is Nothing Then
                UpdatePoLinesRecordCount(poLinesDv.Count)
                GridPoLines.DataSource = poLinesDv
                GridPoLines.PageSize = PageSize
                GridPoLines.DataBind()
                Session("PoLineRecCount") = poLinesDv.Count
                if poLinesDv.Count > 0 then
                    ControlMgr.SetVisibleControl(ElitaHostPage, btnAddSelectedPoLines, True)
                End If
            End If
            
        End Sub
        Private Function GetPoLinesGridDataView() As DataView
            Dim apInvoiceLinesBo As New ApInvoiceLines
            Dim selectedAuthIds As List(Of Guid) = GetSelectedAuthorizationIds()
            If selectedAuthIds.Count = 0 Then
                lblSearchError.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_AUTHORIZATION_NUMBER_REQUIRED)
                ControlMgr.SetVisibleControl(ElitaHostPage, divSearchError, true)
                Return nothing

            End If
            Return apInvoiceLinesBo.GetPoLines(selectedAuthIds)
        End Function
        Private Sub GridPoLines_PageIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles GridPoLines.PageIndexChanged
            Try
                PageIndex = GridPoLines.PageIndex
                PopulatePoLinesGrid()
            Catch ex As Exception
                HandleLocalException(ex)
            End Try
        End Sub
        Private Sub GridPoLines_PageIndexChanging(ByVal sender As Object, ByVal e As GridViewPageEventArgs) Handles GridPoLines.PageIndexChanging
            Try
                GridPoLines.PageIndex = e.NewPageIndex
                PageIndex = GridPoLines.PageIndex
            Catch ex As Exception
                HandleLocalException(ex)
            End Try
        End Sub
        Private Sub PoLineCboPageSize_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles PoLineCboPageSize.SelectedIndexChanged
            Try
                PageSize = CType(PoLineCboPageSize.SelectedValue, Integer)
                PageIndex = NewCurrentPageIndexFunc.Invoke(GridPoLines,  CType(Session("PoLineRecCount"), Int32), PageSize)
                GridPoLines.PageIndex = PageIndex
                PopulatePoLinesGrid()
            Catch ex As Exception
                HandleLocalException(ex)
            End Try
        End Sub
#End Region

#Region "Button Event"

        Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
           
            ControlMgr.SetVisibleControl(ElitaHostPage, divSearchError, false)
            ControlMgr.SetVisibleControl(ElitaHostPage, divAddLinesStatus, false)
            If ValidAuthorizationSearchCriteria Then
                PopulateAuthorizationGrid()
                ControlMgr.SetVisibleControl(ElitaHostPage, GridAuth, True)
            End If
        End Sub

        Protected Sub btnSearchLines_Click(sender As Object, e As EventArgs) Handles btnSearchLines.Click
            Try
                PopulatePoLinesGrid()
                SetControlForPoLineSearchResult
            Catch ex As Exception
                HandleLocalException(ex)
            End Try
        End Sub
        Protected Sub btnAddSelectedPoLines_Click(sender As Object, e As EventArgs) Handles btnAddSelectedPoLines.Click
           Try
               ControlMgr.SetVisibleControl(ElitaHostPage, divAddLinesStatus, false) 
               If PoLineSelected Then
                   AddSelectedPoLines()
                   ClearResultList()
               Else 
                   lblSearchError.Text = TranslationBase.TranslateLabelOrMessage(Message.MSG_SELECT_PO_LINES)
                   ControlMgr.SetVisibleControl(ElitaHostPage, divSearchError, true)
                   Return
               End If
              
           Catch ex As Exception
               HandleLocalException(ex)
           End Try
        End Sub
        Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
            ClearResultList
        End Sub


        

#End Region
    End Class
End NameSpace