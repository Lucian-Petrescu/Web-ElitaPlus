Partial Public Class navigation_menu
    Inherits System.Web.UI.UserControl


#Region "CONSTANTS"
    Private Const ICONFOLDER As String = "icons/"

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



    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Try
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
                'imgDisplayMode.Attributes.Add("style", "CURSOR: hand")
                LoadTabs()
            End If

            If ELPWebConstants.GetMenuState = ELPWebConstants.enumMenu_State.Editing_Page_Mode Then
                DisableLinks(True)
            End If

            If imgDisplayMode.ImageUrl = "images/icons/icon_min.png" Then
                Session("DisplayMode") = "visible"
            Else
                Session("DisplayMode") = "visible"
            End If

            If Session("DisplayMode") IsNot Nothing Then
                displayMode.Value = CType(Session("DisplayMode"), String)
            End If

        Catch ex As DataNotFoundException
            ELPWebConstants.ShowPopup(ex.Message, Page)

        Catch ex As ApplicationException
            ELPWebConstants.ShowPopup(ex.Message, Page)

        Catch ex As NullReferenceException
            ELPWebConstants.ShowPopup(ex.Message, Page)

        End Try

    End Sub

    '-------------------------------------
    'Name:Load_Tabs
    'Purpose:Load the tabs the first time. also load the home page tab id.
    ' then set the selected tab as the home page.
    'Input Values:moApplicationUser
    '-------------------------------------
    Private Sub LoadTabs()
        Try
            moDS = moTabMgr.Load()

            'set the home page as the selected page on first page load.
            If Session(ELPWebConstants.SELECTED_TAB) Is Nothing Then
                'this sets the selected tab to the home page id which has no navigation links.
                Application(ELPWebConstants.HOME_PAGE_TAB_ID) = Assurant.ElitaPlus.BusinessObjectsNew.Codes.TAB_HOME_PAGE
                Session(ELPWebConstants.SELECTED_TAB) = Application(ELPWebConstants.HOME_PAGE_TAB_ID)
            End If

            If moDS IsNot Nothing Then
                xmlSource.Data = moDS.GetXml
            End If

            If Not String.IsNullOrEmpty(xmlSource.Data) Then
                xmlSource.Transform = ElitaPlus.Common.XMLHelper.GetXMLElementsToAttributesTransformScript
                mnu.DataSource = xmlSource
                mnu.DataBind()
            End If

            If mnu.Items.Count <= 0 Then
                System.Threading.Thread.Sleep(3000)
                LoadTabs("retry")
            End If

        Catch ex As NullReferenceException
            ELPWebConstants.ShowPopup(ex.Message, Page)

        Catch ex As DataNotFoundException
            ELPWebConstants.ShowPopup(ex.Message, Page)

        Catch ex As ApplicationException
            ELPWebConstants.ShowPopup(ex.Message, Page)

        Catch ex As Exception
            ELPWebConstants.ShowPopup(ex.Message, Page)

        End Try
    End Sub

    Private Sub LoadTabs(retry As String)
        Try

            moDS = moTabMgr.Load()

            'set the home page as the selected page on first page load.
            If Session(ELPWebConstants.SELECTED_TAB) Is Nothing Then
                'this sets the selected tab to the home page id which has no navigation links.
                Application(ELPWebConstants.HOME_PAGE_TAB_ID) = Assurant.ElitaPlus.BusinessObjectsNew.Codes.TAB_HOME_PAGE
                Session(ELPWebConstants.SELECTED_TAB) = Application(ELPWebConstants.HOME_PAGE_TAB_ID)
            End If

            If moDS IsNot Nothing Then
                xmlSource.Data = moDS.GetXml
            End If

            If Not String.IsNullOrEmpty(xmlSource.Data) Then
                xmlSource.Transform = ElitaPlus.Common.XMLHelper.GetXMLElementsToAttributesTransformScript
                mnu.DataSource = xmlSource
                mnu.DataBind()
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

    Public Function GetTabIcon(CODE As String) As String
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

    Public Function GetTabTitle(CODE As String) As String
        '-------------------------------------
        'Name:GetTabTitle
        'Purpose:Return the text for the title of the tab for a specific imageid.
        'Input Values:nTabImageID- the id of a specific image with which there is 
        ' some associated text.
        '-------------------------------------

        Return moTabMgr.TabTitle(CODE, ElitaPlusIdentity.Current.ActiveUser.LanguageId).ToUpper
    End Function

    Public Function GetTabImage(nAlignment As Long, CODE As String) As String
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

    Private Sub DisableLinks(bEnabled As Boolean)
        'make all the navigation links either enabled or disabled.

        'dlstTabs.Enabled = bEnabled

        mnu.Enabled = bEnabled
    End Sub


#End Region


End Class