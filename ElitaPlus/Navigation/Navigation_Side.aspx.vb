Partial Class Navigation_Side
    Inherits System.Web.UI.Page



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


    Private intRowCount As Int32
    Private moTabMgr As New TabAuthorizationMgr


#Region "CONSTANTS"

    'numbers for the position of columns in the grid.
    Private Const RELATIVE_URL As Int64 = 2
    Private Const LINK_BUTTON_CELL As Int64 = 1
    Private Const CODE_PAGE_NAME As Int64 = 3
    Private Const NAV_ALWAYS As Int64 = 0
    Private Const CURRENT_TABID As String = "nTabId"

    Private Const ELITA_MAIN_DIRECTORY As String = "/"

    'constant value indicating navigation is allowed.
    Private Const ALLOW_NAVIGATION As String = "Y"

    Private Const PATH_TO_IMAGES As String = "/Navigation/images/"
    Private Const ICONFOLDER As String = "icons/"



#End Region




    Public ReadOnly Property NoNavMessage() As String
        Get

            Return TranslationBase.TranslateLabelOrMessage(ELPWebConstants.NONAVMESSAGE, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        End Get

    End Property


    Private Sub Page_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        Dim sCODE As String = Request.QueryString(CURRENT_TABID)
        Session(ELPWebConstants.SELECTED_TAB) = sCODE

        If Not Page.IsPostBack Then

            btnMinimize.Attributes.Add("onclick", "toggleSideMenu();")
            If (sCODE <> Nothing) AndAlso (sCODE <> Assurant.ElitaPlus.BusinessObjectsNew.Codes.TAB_HOME_PAGE) Then
                LoadSecondaryNavigationLinks(sCODE)
            End If

            If Session(ELPWebConstants.MENUSTATE) IsNot Nothing Then
                If CType(Session(ELPWebConstants.MENUSTATE), ELPWebConstants.enumMenu_State) = ELPWebConstants.enumMenu_State.Editing_Page_Mode Then
                    DisableLinks(False)
                Else
                    DisableLinks(True)
                End If
            End If

            If Session("ScrollPos") IsNot Nothing Then
                scrollPos.Value = CType(Session("ScrollPos"), String)
            End If

        Else
            Session("ScrollPos") = scrollPos.Value
        End If
        If sCODE = Assurant.ElitaPlus.BusinessObjectsNew.Codes.TAB_HOME_PAGE Or sCODE Is Nothing Then
            isHomeTab.Value = "true"
        Else
            isHomeTab.Value = "false"
            If Session("SideMenuMinimized") IsNot Nothing Then
                isMinimized.Value = CType(Session("SideMenuMinimized"), String)
                If isMinimized.Value = "true" Then
                    btnMinimize.ImageUrl = "../Navigation/images/maximize.gif"
                    btnMinimize.ToolTip = "Maximize the menu bar to show icons and text"
                ElseIf isMinimized.Value = "false" AndAlso Not IsPostBack Then
                End If
            Else
                isMinimized.Value = "false"
                If Not IsPostBack Then
                    Session("SideMenuMinimized") = "false"
                    btnMinimize.ImageUrl = "../Navigation/images/minimize.gif"
                    btnMinimize.ToolTip = "Minimize the menu bar to show only icons"
                End If
            End If
        End If

    End Sub

    Private Sub MinimizeMenu(sender As System.Object, e As System.Web.UI.ImageClickEventArgs)

        If btnMinimize.ImageUrl = "../Navigation/images/minimize.gif" Then
            btnMinimize.ImageUrl = "../Navigation/images/maximize.gif"
            btnMinimize.ToolTip = "Maximize the menu bar to show icons and text"
            Session("SideMenuMinimized") = "true"
            isMinimized.Value = "true"
        Else
            btnMinimize.ImageUrl = "../Navigation/images/minimize.gif"
            btnMinimize.ToolTip = "Minimize the menu bar to show only icons"
            Session("SideMenuMinimized") = "false"
            isMinimized.Value = "false"
        End If

    End Sub

    Public Function GetTabIcon() As String
        '-------------------------------------
        'Name:GetTabIcon
        'Purpose:Retrieve the icon for a specific imageid.
        'Input Values:intTabImageID - the id of a specific image
        'Uses:
        '-------------------------------------
        'build the string for the location of the image folder.
        Dim sIconPath As String = Request.ApplicationPath & PATH_TO_IMAGES & ICONFOLDER
        Return TabIconPath(Request.QueryString(CURRENT_TABID), sIconPath)
    End Function

    Public Function TabIconPath(code As String, iconPath As String) As String
        Dim strIconPath As String

        Select Case code
            Case "ADMIN"
                strIconPath = iconPath & "admin_sm_icon2.gif"
            Case "CLAIMS"
                strIconPath = iconPath & "claims_sm_icon2.gif"
            Case "INTERFACES"
                strIconPath = iconPath & "interfaces_sm_icon2.gif"
            Case "REPORTS"
                strIconPath = iconPath & "reports_sm_icon2.gif"
            Case "TABLES"
                strIconPath = iconPath & "tables_sm_icon2.gif"
            Case "CERTIFICATES"
                strIconPath = iconPath & "certificates_sm_icon2.gif"
            Case "IHQ"
                strIconPath = iconPath & "ihq_sm_icon2.gif"
            Case "OTHER"
        End Select

        Return strIconPath
    End Function

    Function GetLinkText(sTranslation As String) As String
        Return sTranslation
    End Function

    Private Sub LoadSecondaryNavigationLinks(sCODE As String)
        Dim dvLinks As DataView = FormAuthorization.Load(sCODE)

        If dvLinks.Count > 0 Then
            grdNavigationLinks.DataSource = dvLinks
            grdNavigationLinks.DataBind()
        End If

    End Sub


    Public Sub DisplayContentArea(source As System.Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs)
        '-------------------------------------
        'Name:grdNavigationLinks_ItemCommand
        'Purpose Retrieve the value of the allow_nav column.
        'then, hide or show the secondary navigation links.
        'Input Values:e.Item.Cells(0).Text = Either a "Y" or "N"
        '-------------------------------------
        'there are 2 other hidden columns with the RELATIVE URL and the page name-CODE.
        Dim sAllow_Nav As String = e.Item.Cells(NAV_ALWAYS).Text
        Dim sURL As String
        Dim sScript As String

        If ELPWebConstants.GetMenuState = ELPWebConstants.enumMenu_State.Editing_Page_Mode Then
            'if the page is in edit mode display the message and disable navigation.
            EnableNavigation(False)
            Exit Sub
        End If

        'build url string for the content target. cell 2 is the relative URL/Folder and cell 3 is the CODE/pagename.
        sURL = Request.ApplicationPath & ELITA_MAIN_DIRECTORY & e.Item.Cells(RELATIVE_URL).Text & "/" & e.Item.Cells(CODE_PAGE_NAME).Text & ".aspx"

        If sAllow_Nav = ALLOW_NAVIGATION Then
            'navigation should be enabled.
            EnableNavigation(True)

        Else
            Session(ELPWebConstants.MENUSTATE) = ELPWebConstants.enumMenu_State.Editing_Page_Mode
            'shift into page editing mode.
            'navigation should NOT be enabled.

            'change the linkbutton style to show as only text not as a link.
            DisableLinks(False)
            DisableTabs()

        End If

        'set the target for the content pane.
        txtContentPage.Value = sURL

        'run the code to change the page inside the content frame.
        sScript = "parent.document.frames('Navigation_Content').document.body.style.cursor = 'wait'; "
        sScript &= "parent.document.frames('Navigation_Content').location.href = '" & sURL & "'"
        ELPWebConstants.ExecuteJavascript(sScript, Page)


    End Sub


    Sub DisableTabs()
        Dim sScript As String
        sScript = "parent.document.frames('Navigation_Header').location.href = 'Navigation_Header.aspx'"
        ELPWebConstants.ExecuteJavascript("RefreshTheHeader", sScript, Page)
    End Sub


    Sub DisableLinks(bEnabled As Boolean)
        'make all the navigation links either enabled or disabled.

        grdNavigationLinks.Enabled = bEnabled
    End Sub


    '-------------------------------------
    'Name:Enable_Navigation
    'Purpose:Set a flag to enable or disable navigation.
    '. Run a client side script to display a message to the user explaining the current status.
    'Input Values:blnEnableNow - flag to indicate whether to enable or disable navigation.
    '-------------------------------------

    Private Sub EnableNavigation(enableNow As Boolean)
        Dim sScript As String

        Try
            If enableNow = False Then
                ELPWebConstants.ShowPopup(NoNavMessage, Page)
            End If

            DisableLinks(enableNow)

            'Catch ex As MessageNotFoundException
            '    ELPWebConstants.ShowPopup(ex.Message, Me.Page)
        Catch ex As ApplicationException
            ELPWebConstants.ShowPopup(ex.Message, Page)
        Catch ex As Exception
            ELPWebConstants.ShowPopup(ex.Message, Page)
        End Try


    End Sub

    Sub ItemCreated(sender As Object, e As DataGridItemEventArgs)
        Dim elemType As ListItemType = e.Item.ItemType

        'If elemType = ListItemType.AlternatingItem Or elemType = ListItemType.Item Then
        '    e.Item.Attributes.Add("style", "text-decoration:none;BEHAVIOR: url(./scripts/PopUpButton.htc)")
        '    e.Item.Attributes.Add("popupbackgroundColorOff", "#f0f2f5")
        '    e.Item.Attributes.Add("popupbackgroundColorOn", "#f0f2f5")
        'End If

    End Sub


End Class
