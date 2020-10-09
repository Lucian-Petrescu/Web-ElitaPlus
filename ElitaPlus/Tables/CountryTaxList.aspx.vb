Imports System.Text

Imports Assurant.ElitaPlus.BusinessObjectsNew

Partial Class CountryTaxList
    Inherits ElitaPlusSearchPage

#Region "Web controls and private members"
    Public Const PAGETITLE As String = "CountryTax"
    Public Const PAGETAB As String = "TABLES"
#End Region

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As System.Object, e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

#Region " Constants "
    Private Const COUNTRY_TAX_EDIT As String = "CountryTaxEdit.aspx"
    Private Const COUNTRY_TAX_BO As String = "COUNTRY_TAX_BO"
    Private Const DG_COUNTRY_TAX_GUID_ID As Integer = 5
    Private Const ACTION_SELECT As String = "SelectAction"
#End Region

#Region "Page State"
    Private IsReturningFromChild As Boolean = False

    ' This class keeps the current state for the page.
    Class MyState
        Public PageIndex As Integer = 0
        Public selectedPageSize As Integer = DEFAULT_PAGE_SIZE
        Public HasDataChanged As Boolean
        Public CountryTaxView As DataView
        Public selectedID As System.Guid = System.Guid.Empty

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

#Region "Page_Events"

    Private Sub UpdateBreadCrum()
        If (State IsNot Nothing) Then
            MasterPage.BreadCrum = MasterPage.PageTab & ElitaBase.Sperator & TranslationBase.TranslateLabelOrMessage(PAGETITLE)
        End If
    End Sub

    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        MasterPage.MessageController.Clear_Hide()
        Try
            MasterPage.MessageController.Clear()
            MasterPage.UsePageTabTitleInBreadCrum = False
            MasterPage.PageTab = TranslationBase.TranslateLabelOrMessage(PAGETAB)
            MasterPage.PageTitle = TranslationBase.TranslateLabelOrMessage(PAGETITLE)
            UpdateBreadCrum()

            If Not IsPostBack Then
                SetGridItemStyleColor(grdResults)
                TranslateGridHeader(grdResults)
                MenuEnabled = True
                LoadTaxes()
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
        ShowMissingTranslations(MasterPage.MessageController)

    End Sub

    Private Sub Page_PageReturn(ReturnFromUrl As String, ReturnPar As Object) Handles MyBase.PageReturn
        MenuEnabled = True
        IsReturningFromChild = True
        Dim retObj As CountryTaxEdit.ReturnType = CType(ReturnPar, CountryTaxEdit.ReturnType)
        State.HasDataChanged = retObj.HasDataChanged
        Select Case retObj.LastOperation
            Case ElitaPlusPage.DetailPageCommand.Delete
                'Me.AddInfoMsg(Message.DELETE_RECORD_CONFIRMATION)
                DisplayMessage(Message.DELETE_RECORD_CONFIRMATION, "", MSG_BTN_OK, MSG_TYPE_INFO)
        End Select
    End Sub

#End Region

    Private Sub LoadTaxes()
        Try

            Dim oCountriesDv As DataView = LookupListNew.GetUserCountriesLookupList()
            Dim oCompanyDv As DataView = LookupListNew.GetUserCompaniesLookupList()
            Dim oCountriesArr As New ArrayList
            Dim oCompanyArr As New ArrayList

            If oCountriesDv.Count > 0 Then
                Dim index As Integer
                ' Create Array
                For index = 0 To oCountriesDv.Count - 1
                    If oCountriesDv(index)("ID") IsNot System.DBNull.Value Then
                        oCountriesArr.Add(New Guid(CType(oCountriesDv(index)("ID"), Byte())))
                    End If
                Next
            End If

            If oCompanyDv.Count > 0 Then
                Dim index As Integer
                ' Create Array
                For index = 0 To oCompanyDv.Count - 1
                    If oCompanyDv(index)("ID") IsNot System.DBNull.Value Then
                        oCompanyArr.Add(New Guid(CType(oCompanyDv(index)("ID"), Byte())))
                    End If
                Next
            End If

            Dim dv As DataView = CountryTax.getList(oCountriesArr, oCompanyArr, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

            If (dv.Count = 0) Then
                dv = CountryTax.getEmptyList(dv)
                grdResults.DataSource = dv
                grdResults.DataBind()
                grdResults.Rows(0).Visible = False
            Else
                grdResults.DataSource = dv
                grdResults.DataBind()
            End If
            If Not grdResults.BottomPagerRow.Visible Then grdResults.BottomPagerRow.Visible = True
            'SetPageAndSelectedIndexFromGuid(dv, Me.State.selectedID, Me.grdResults, Me.State.PageIndex)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try
    End Sub

    Private Sub btnAdd_WRITE_Click(sender As System.Object, e As System.EventArgs) Handles btnAdd_WRITE.Click

        Try
            callPage(CountryTaxEdit.URL)
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

    Private Sub grdResults_ItemCreated(sender As Object, e As System.Web.UI.WebControls.GridViewRowEventArgs) Handles grdResults.RowCreated

        BaseItemCreated(sender, e)

    End Sub

    Private Sub grdResults_PageIndexChanged(source As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles grdResults.PageIndexChanging
        grdResults.PageIndex = e.NewPageIndex
        State.PageIndex = e.NewPageIndex
        LoadTaxes()
    End Sub

    Private Sub grdResults_ItemCommand(source As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles grdResults.RowCommand

        Dim sCurrentTaxID As String
        Try
            If e.CommandName = ACTION_SELECT Then

                Dim index As Integer = CInt(e.CommandArgument)
                sCurrentTaxID = CType(grdResults.Rows(index).Cells(DG_COUNTRY_TAX_GUID_ID).FindControl("lblCountryTaxId"), Label).Text
                State.selectedID = New Guid(sCurrentTaxID)
                callPage(CountryTaxEdit.URL, New Guid(sCurrentTaxID))
            End If
        Catch ex As Exception
            HandleErrors(ex, MasterPage.MessageController)
        End Try

    End Sub

End Class
