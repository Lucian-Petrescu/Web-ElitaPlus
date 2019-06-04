Partial Class Navigation_Header
    Inherits System.Web.UI.Page


    ' labels
    Protected WithEvents Label1 As System.Web.UI.WebControls.Label
    Protected WithEvents Label2 As System.Web.UI.WebControls.Label
    Protected WithEvents Label4 As System.Web.UI.WebControls.Label
    Protected WithEvents Label5 As System.Web.UI.WebControls.Label
    Protected WithEvents Label6 As System.Web.UI.WebControls.Label
    Protected WithEvents Label7 As System.Web.UI.WebControls.Label
    Protected WithEvents Label8 As System.Web.UI.WebControls.Label
    Protected WithEvents lblHome As System.Web.UI.WebControls.Label

    ' images
    Protected WithEvents imgHomeTabLeft As System.Web.UI.WebControls.Image
    Protected WithEvents imgHomeTabRight As System.Web.UI.WebControls.Image
    Protected WithEvents imgCertificates As System.Web.UI.WebControls.Image
    Protected WithEvents Image1 As System.Web.UI.WebControls.Image
    Protected WithEvents imgAdminTabLeft As System.Web.UI.WebControls.Image
    Protected WithEvents imgAdminTabTopSpacer As System.Web.UI.WebControls.Image
    Protected WithEvents imgAdminIcon As System.Web.UI.WebControls.Image
    Protected WithEvents imgAdminTabLeftSpacer As System.Web.UI.WebControls.Image
    Protected WithEvents imgAdminTabRightSpacer As System.Web.UI.WebControls.Image
    Protected WithEvents Image2 As System.Web.UI.WebControls.Image
    Protected WithEvents IMG1 As System.Web.UI.HtmlControls.HtmlImage


    ' buttons
    Protected WithEvents imgTabIcon As System.Web.UI.WebControls.ImageButton
    Protected WithEvents hlnkTabTitle As System.Web.UI.WebControls.Label


    ' table cells
    Protected WithEvents tdHomeIcon As System.Web.UI.HtmlControls.HtmlTableCell
    Protected WithEvents Elita_Small_Logo As System.Web.UI.WebControls.Image
    Protected WithEvents imgHideShowMenu As System.Web.UI.HtmlControls.HtmlImage

    ' datalist
    Protected WithEvents moCountryLabel As System.Web.UI.WebControls.Label




#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region



#Region "CONSTANTS"
    Private Const ICONFOLDER As String = "icons/"

    'Public Const CONST_LEFT As String = "left"
    'Public Const CONST_MIDDLE As String = "middle"
    'Public Const CONST_RIGHT As String = "right"

    ' Private CFG_HOME_PAGE As String = ConfigurationMgr.ConfigValue(ELPWebConstants.HOME_PAGE)
    ' Private CFG_NAVIGATION_SIDE As String = ConfigurationMgr.ConfigValue("NAVIGATION_SIDE")

    Private Const PATH_TO_IMAGES As String = "/Navigation/images/"

    Private Const LOGOUT As String = "LOGOUT"
    Private Const COLLAPSED_FRAME_SIZE As String = "'0,100%'"
    Private Const EXPANDED_FRAME_SIZE As String = "'219,66%'"

    Private Const ADMIN_SPLASH_PAGE As String = "Navigation_AdminSplash.aspx"
    Private Const CERTIFICATES_SPLASH_PAGE As String = "Navigation_CertificatesSplash.aspx"
    Private Const CLAIMS_SPLASH_PAGE As String = "Navigation_ClaimsSplash.aspx"
    Private Const INTERFACES_SPLASH_PAGE As String = "Navigation_InterfacesSplash.aspx"
    Private Const REPORTS_SPLASH_PAGE As String = "Navigation_ReportsSplash.aspx"
    Private Const TABLES_SPLASH_PAGE As String = "Navigation_TablesSplash.aspx"
    Private Const IHQ_SPLASH_PAGE As String = "Navigation_IHQSplash.aspx"

#End Region



#Region "ENUMERATIONS"

    Enum enumAlignment
        LEFT
        RIGHT
        MIDDLE
    End Enum

#End Region



#Region "PRIVATE OBJECT VARIABLES"

    Private moDS As New DataSet()
    Private mCode As String
    Private moTabMgr As New TabAuthorizationMgr

#End Region



#Region "PAGE PROPERTIES"



    Public ReadOnly Property NoNavMessage() As String
        Get
            Return TranslationBase.TranslateLabelOrMessage(ELPWebConstants.NONAVMESSAGE, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        End Get

    End Property

#End Region



#Region "PAGE ROUTINES"


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        '-------------------------------------
        'Name:Page_Load
        'Purpose:on the first time run, load the data and set the selected to home.
        ' otherwise set the selected tab from the grid and the language id from the user's session.
        'Input Values:
        '-------------------------------------
        'load the language and network id values into the objApplcation user object

        If txtTabID.Value = LOGOUT Then
            Session.Abandon()
            Exit Sub
        End If

        If Not Page.IsPostBack() Then

            imgDisplayMode.Attributes.Add("onclick", "toggleHeaderView();")
            imgDisplayMode.Attributes.Add("style", "height:21px;width:21px;CURSOR: hand")
            Me.LoadTabs()
        End If

        If ELPWebConstants.GetMenuState = ELPWebConstants.enumMenu_State.Editing_Page_Mode Then
            DisableLinks(False)
        End If

        If imgDisplayMode.ImageUrl = "images/icons/min_icon.gif" Then
            Session("DisplayMode") = "hidden"
        Else
            Session("DisplayMode") = "visible"
        End If

        If Not Session("DisplayMode") Is Nothing Then
            Me.displayMode.Value = CType(Session("DisplayMode"), String)
        End If

        'If Not Session(ELPWebConstants.SELECTED_TAB) Is Nothing AndAlso Not (CType(Session(ELPWebConstants.SELECTED_TAB), String).Equals(CType(Application(ELPWebConstants.HOME_PAGE_TAB_ID), String)) AndAlso Not Page.IsPostBack()) Then
        If EnvironmentContext.Current.Environment = Environments.Production Then
            Me.lblEnv.Text = ""
        Else
            Me.lblEnv.Text = "Env: " & EnvironmentContext.Current.EnvironmentName
            Me.lblEnv.Text &= "&nbsp;&nbsp;&nbsp;"
            If ((EnvironmentContext.Current.Environment = Environments.Development) OrElse
                 (EnvironmentContext.Current.Environment = Environments.Test)) Then
                Me.lblEnv.Text &= ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Description
                Me.lblEnv.Text &= "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"
            End If
        End If

    End Sub

    '-------------------------------------
    'Name:Load_Tabs
    'Purpose:Load the tabs the first time. also load the home page tab id.
    ' then set the selected tab as the home page.
    'Input Values:moApplicationUser
    '-------------------------------------
    Private Sub LoadTabs()
        Dim sScript As String
        Try
            moDS = moTabMgr.Load()

            'set the home page as the selected page on first page load.
            If Session(ELPWebConstants.SELECTED_TAB) Is Nothing Then

                'this sets the selected tab to the home page id which has no navigation links.
                Application(ELPWebConstants.HOME_PAGE_TAB_ID) = Assurant.ElitaPlus.BusinessObjectsNew.Codes.TAB_HOME_PAGE
                Session(ELPWebConstants.SELECTED_TAB) = Application(ELPWebConstants.HOME_PAGE_TAB_ID)

                'collapse the width of the side frame.
                sScript = "parent.document.frames['Navigation_Side'].frameElement.width = '0px';"

            End If

            If Not moDS Is Nothing Then
                xmlSource.Data = moDS.GetXml
            End If

            If Not String.IsNullOrEmpty(xmlSource.Data) Then
                xmlSource.Transform = ElitaPlus.Common.XMLHelper.GetXMLElementsToAttributesTransformScript
                Me.mnu.DataSource = xmlSource
                Me.mnu.DataBind()
            End If

            If Me.mnu.Items.Count <= 0 Then
                System.Threading.Thread.Sleep(3000)
                LoadTabs("retry")
            End If

        Catch ex As NullReferenceException
            ELPWebConstants.ShowPopup(ex.Message, Me.Page)

        Catch ex As DataNotFoundException
            ELPWebConstants.ShowPopup(ex.Message, Me.Page)

        Catch ex As ApplicationException
            ELPWebConstants.ShowPopup(ex.Message, Me.Page)

        Catch ex As Exception
            ELPWebConstants.ShowPopup(ex.Message, Me.Page)

        End Try

    End Sub

    Private Sub LoadTabs(retry As String)
        Dim sScript As String

        Try
            moDS = moTabMgr.Load()

            'set the home page as the selected page on first page load.
            If Session(ELPWebConstants.SELECTED_TAB) Is Nothing Then

                'this sets the selected tab to the home page id which has no navigation links.
                Application(ELPWebConstants.HOME_PAGE_TAB_ID) = Assurant.ElitaPlus.BusinessObjectsNew.Codes.TAB_HOME_PAGE
                Session(ELPWebConstants.SELECTED_TAB) = Application(ELPWebConstants.HOME_PAGE_TAB_ID)

                'collapse the width of the side frame.
                sScript = "parent.document.frames['Navigation_Side'].frameElement.width = '0px';"

            End If

            If Not moDS Is Nothing Then
                xmlSource.Data = moDS.GetXml
            End If

            If Not String.IsNullOrEmpty(xmlSource.Data) Then
                xmlSource.Transform = ElitaPlus.Common.XMLHelper.GetXMLElementsToAttributesTransformScript
                Me.mnu.DataSource = xmlSource
                Me.mnu.DataBind()
            End If

        Catch ex As NullReferenceException
            Throw New NullReferenceException(ex.Message)

        Catch ex As DataNotFoundException
            Throw New DataNotFoundException(ex.Message)

        Catch ex As ApplicationException
            Throw New ApplicationException(ex.Message)

        Catch ex As Exception
            Throw New Exception(ex.Message)

        End Try

    End Sub

    Public Function GetTabIcon(ByVal CODE As String) As String
        '-------------------------------------
        'Name:GetTabIcon
        'Purpose:Retrieve the icon for a specific imageid.
        'Input Values:intTabImageID - the id of a specific image
        'Uses:
        '-------------------------------------
        'build the string for the location of the image folder.
        Dim sIconPath As String = Request.ApplicationPath & PATH_TO_IMAGES & ICONFOLDER
        Return moTabMgr.TabIconPath(CODE, sIconPath)
    End Function

    Public Function GetTabTitle(ByVal CODE As String) As String
        '-------------------------------------
        'Name:GetTabTitle
        'Purpose:Return the text for the title of the tab for a specific imageid.
        'Input Values:nTabImageID- the id of a specific image with which there is 
        ' some associated text.
        '-------------------------------------

        Return moTabMgr.TabTitle(CODE, ElitaPlusIdentity.Current.ActiveUser.LanguageId).ToUpper
    End Function

    Public Function GetTabImage(ByVal nAlignment As Long, ByVal CODE As String) As String
        '-------------------------------------
        'Name:GetTabImage
        'Purpose:build the left center and right outlines for the rounded tabs.
        'Input Values:strAlignment - there are 3 sections for each image , one for
        'the left,center and right sections of the rounded tab.
        'Uses:
        '-------------------------------------

        Dim sTabImage As String = moTabMgr.TabImage(CODE, nAlignment)
        Return Request.ApplicationPath & PATH_TO_IMAGES & sTabImage
    End Function

    Private Sub DisableLinks(ByVal bEnabled As Boolean)
        'make all the navigation links either enabled or disabled.

        'dlstTabs.Enabled = bEnabled

        mnu.Enabled = bEnabled
    End Sub


#End Region


End Class