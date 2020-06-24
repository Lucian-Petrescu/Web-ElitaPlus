Public Class UserControlApInvoiceLinesSearch
    Inherits System.Web.UI.UserControl

#Region "Constant"
    public class PageConstant
        Public Const ServiceCenter As String = "ServiceCenter"
        Public Const CountryId As String = "CountryId"
        Public Const ApInvoiceHeaderId As String = "ApInvoiceHeaderId"
        Public Const CompanyCode As String = "CompanyCode"
        Public Const CountryCode As String = "CountryCode"
        Public Const Dealer As String = "Dealer"
    End Class
        
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
                Return DirectCast(ViewState(PageConstant.CompanyCode), Guid )
            End Get
            Set(value As Guid)
                ViewState(PageConstant.CompanyCode) = value
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
    Public Property TranslateGridHeaderFunc As Action(Of System.Web.UI.WebControls.GridView)
    Public Property TranslationFunc As Func(Of String, String)

#End Region

    #Region "Control Status"

    Private sub SetControlState(ByVal isVisible  As Boolean )
        ControlMgr.SetVisibleControl(ElitaHostPage, linesGridHeader, isVisible) 
        ControlMgr.SetVisibleControl(ElitaHostPage, btnSearchLines, isVisible )
        
    End sub

   
    #End Region

    #Region "Page Event"
        
    Sub InitializeComponent()
        Try
          
            SetControlState(false)
            TranslateGridHeaderFunc.Invoke(GridPoLines)
            TranslateGridHeaderFunc.Invoke(GridAuth)
            SetTranslations()

        Catch ex As Exception
            HandleLocalException(ex)
        End Try
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim parentControl = Parent
        While (Not TypeOf parentControl Is ElitaPlusPage And parentControl IsNot Nothing)
            parentControl = parentControl.Parent
        End While
        ElitaHostPage = DirectCast(parentControl, ElitaPlusPage)
    End Sub
    #End Region

    # Region "Page Internal"
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
        btnSearchLines.Text = TranslationFunc("Search")
        
    End Sub

    Private Sub ClearResultList()
        txtAuthorizationNumber.Text =String.Empty 
        txtClaimNumber.Text = string.Empty 
        txtServiceCenter.Text = String.Empty 
        ControlMgr.SetVisibleControl(ElitaHostPage, btnSearchLines, false )
        GridAuth.DataSource = Nothing
        GridAuth.DataBind()
        GridPoLines.DataSource = Nothing
        GridPoLines.DataBind()
        UpdateRecordCount(0)
    End Sub
    Private sub UpdateRecordCount(records As Integer)
        If Me.GridAuth.Visible Then
            Me.lblRecordCount.Text = $"{records} {TranslationBase.TranslateLabelOrMessage(Message.MSG_RECORDS_FOUND)}"
        End If
    End sub
    # End Region

    #Region "Authorization Grid"
    Private sub PopulateAuthorizationGrid()

    End sub

#End Region

    #Region "Button Event"

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        ControlMgr.SetVisibleControl(ElitaHostPage, btnSearchLines, true )
        ControlMgr.SetVisibleControl(ElitaHostPage, GridAuth, true )

    End Sub

    Protected Sub btnSearchLines_Click(sender As Object, e As EventArgs) Handles btnSearchLines.Click
        ControlMgr.SetVisibleControl(ElitaHostPage, linesGridHeader, true) 
        ControlMgr.SetVisibleControl(ElitaHostPage, GridPoLines, true )
    End Sub

    Protected Sub btnClearSearch_Click(sender As Object, e As EventArgs) Handles btnClearSearch.Click
        ClearResultList
    End Sub

#End Region
End Class