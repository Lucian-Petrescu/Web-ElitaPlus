Imports System.Collections.Generic

Namespace Common

    Public Class UserControlApInvoiceLinesSearch
        Inherits UserControl

#Region "Constant"
        Public Class PageConstant
            Public Const ServiceCenter As String = "ServiceCenter"
            Public Const ServiceCenterId As String = "ServiceCenterId"
            Public Const CountryId As String = "CountryId"
            Public Const ApInvoiceHeaderId As String = "ApInvoiceHeaderId"
            Public Const CompanyCode As String = "CompanyCode"
            Public Const CountryCode As String = "CountryCode"
            Public Const Dealer As String = "Dealer"
            Public Const PageIndex As String = "PageIndex"
            Public Const PageSize As String = "PageSize"
            Public Const SortExpression As String = "SortExpression"
            Public Const GridColCheckBoxIdx As Integer = 0
            Public Const GridColClaimSelectCtrl  As String = "checkBoxSelected"
            Public Const GridColClaimIdx As Integer = 5
            Public Const GridColAuthorizationIdx As Integer = 5
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
        Public Property TranslateGridHeaderFunc As Action(Of GridView)
        Public Property TranslationFunc As Func(Of String, String)
        Public Property NewCurrentPageIndexFunc As Func(Of GridView, Integer, Integer, Integer)

#End Region

#Region "Control Status"

        Private Sub SetControlState(ByVal isVisible As Boolean)
            ControlMgr.SetVisibleControl(ElitaHostPage, linesGridHeader, isVisible)
            ControlMgr.SetVisibleControl(ElitaHostPage, btnSearchLines, isVisible)

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

            Catch ex As Exception
                HandleLocalException(ex)
            End Try
        End Sub
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            Dim parentControl = Parent
            While (Not TypeOf parentControl Is ElitaPlusPage And parentControl IsNot Nothing)
                parentControl = parentControl.Parent
            End While
            ElitaHostPage = DirectCast(parentControl, ElitaPlusPage)
            txtServiceCenter.Text = ServiceCenter
        
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
            GridAuth.DataSource = Nothing
            GridAuth.DataBind()
            GridPoLines.DataSource = Nothing
            GridPoLines.DataBind()
            UpdateRecordCount(0)
        End Sub
        Private Sub UpdateRecordCount(records As Integer)
            If GridAuth.Visible Then
                lblRecordCount.Text = $"{records} {TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)}"
          
            End If
        End Sub
        Private Sub UpdatePoLinesRecordCount(records As Integer)
            If GridAuth.Visible Then
                lblPoLinesRecords.Text = $"{records} {TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)}"
            End If
        End Sub

        Private Function GetSelectedAuthorizationIds() As List(Of Guid)
            Dim chkbox As CheckBox, claimIdStr As String
            Dim selectedAuth As List(Of Guid) = New List(Of Guid)

            For Each row As GridViewRow In GridAuth.Rows
                If row.RowType = DataControlRowType.DataRow Then
                    chkbox = CType(row.Cells(PageConstant.GridColCheckBoxIdx).FindControl(PageConstant.GridColClaimSelectCtrl), CheckBox)
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
#End Region

#Region "Authorization Grid"
        Private Sub PopulateAuthorizationGrid()

            Dim authDv As DataView = GetAuthorizationGridDataView()
            GridAuth.DataSource = authDv
            UpdateRecordCount(authDv.Count)
            GridAuth.PageSize = PageSize
            GridAuth.DataBind()
            Session("authRecCount") = authDv.Count
        End Sub
        Private Function GetAuthorizationGridDataView() As DataView
            Dim apInvoiceLinesBo As New ApInvoiceLines
            Return apInvoiceLinesBo.GetAuthorization(ServiceCenterId, txtClaimNumber.Text, txtAuthorizationNumber.Text)
        End Function

        Private Function GetPoLinesGridDataView() As DataView
            Dim apInvoiceLinesBo As New ApInvoiceLines
            Dim selectedAuthIds As List(Of Guid) = GetSelectedAuthorizationIds()
            Return apInvoiceLinesBo.GetPoLines(selectedAuthIds)
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
            UpdatePoLinesRecordCount(poLinesDv.Count)
            GridPoLines.DataSource = poLinesDv
            GridPoLines.PageSize = PageSize
            GridPoLines.DataBind()
            Session("PoLineRecCount") = poLinesDv.Count
        End Sub
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
            PopulateAuthorizationGrid()
            ControlMgr.SetVisibleControl(ElitaHostPage, btnSearchLines, True)
            ControlMgr.SetVisibleControl(ElitaHostPage, GridAuth, True)
        
        End Sub

        Protected Sub btnSearchLines_Click(sender As Object, e As EventArgs) Handles btnSearchLines.Click
            Try
                PopulatePoLinesGrid()
                SetControlForPoLineSearchResult
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