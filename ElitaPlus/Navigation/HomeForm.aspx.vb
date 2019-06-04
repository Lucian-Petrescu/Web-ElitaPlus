Imports System.IO
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Partial Class HomeForm
    Inherits ElitaPlusPage


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

    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const COUNTRIES_FOLDER_PATH As String = "images/countries/"
    'Private Const DEFAULT_FLAG_IMAGE As String = "new_USAFlag.gif"
    Private Const DEFAULT_FLAG_IMAGE As String = "new_UnitedStatesFlag.gif"
    ' Private Const DEFAULT_SPLASH_IMAGE As String = "new2_USASplash.gif"
    Private Const DEFAULT_SPLASH_IMAGE As String = "new2_UnitedStatesSplash.gif"
    Private Const COLLAPSED_FRAME_SIZE As String = "'0,100%'"

    ' Private CFG_HOME_PAGE As String = ConfigurationMgr.ConfigValue(ELPWebConstants.HOME_PAGE)
    ' Private CFG_MAIN_PAGE_NAME As String = ConfigurationMgr.ConfigValue(ELPWebConstants.MAIN_PAGE_NAME)


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        If Not IsPostBack() Then
            Me.AppHomeUrl = Me.Request.Url.AbsolutePath
            Me.LoadLanguageInfo()
            Me.SetFormImages()
            UpdateNavigation()

            Me.lblEnvValue.Text = EnvironmentContext.Current.EnvironmentName

            'Added to display the separate paths in Test and Model
            If ((EnvironmentContext.Current.Environment <> Environments.Development) AndAlso (EnvironmentContext.Current.Environment <> Environments.Production)) Then
                Me.lblEnvValue.Text += " - " + AppConfig.ConnType
            End If

            'Added build version
            Dim sBuildNum As String
            Dim sBuildArr As String()

            sBuildNum = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()

            'Display only Major.Minor.Build. Remove Revision
            sBuildArr = sBuildNum.Split(CChar("."))
            sBuildNum = String.Empty
            sBuildNum = sBuildArr(0).ToString() + "." + sBuildArr(1).ToString() + "." + sBuildArr(2).ToString()

            Me.lblBuildValue.Text = sBuildNum

            'If AppConfig.CurrentEnvironment.ToUpper.Equals("PROD") Then
            '    Me.lblEnvValue.Text = ""
            '    Me.lblEnvTitle.Text = ""
            'Else
            '    Me.lblEnvValue.Text = AppConfig.CurrentEnvironment
            '    Me.lblEnvTitle.Text = "Environment:"
            'End If

            'user noification chages REQ-5279
            If UserNotification.UserHasNotifications(Guid.Empty) Then
                Session("PageCalledFrom") = "MAINPAGE"
                Dim NotificationListURL As String = ELPWebConstants.APPLICATION_PATH & "/Security/NotificationListForm.aspx"
                'Me.Response.Redirect(NotificationListURL)
                Me.callPage(NotificationListURL, "MAINPAGE")
            End If
        End If

    End Sub

    Private Sub UpdateNavigation()
        'this sets the selected tab to the home page id which has no navigation links.
        Application(ELPWebConstants.HOME_PAGE_TAB_ID) = Assurant.ElitaPlus.BusinessObjectsNew.Codes.TAB_HOME_PAGE
        Session(ELPWebConstants.SELECTED_TAB) = Application(ELPWebConstants.HOME_PAGE_TAB_ID)
        'collapse the width of the side frame.
        'Dim sScript As String = "parent.document.all.item('SecondaryFrame').cols = " & COLLAPSED_FRAME_SIZE
        '  Dim sScript As String = "parent.document.frames['Navigation_Side'].frameElement.width = '0px';"
        ' ELPWebConstants.ExecuteJavascript("changewidth", sScript, Me.Page)
        txtNextPageID.Value = Assurant.ElitaPlus.BusinessObjectsNew.Codes.TAB_HOME_PAGE

        If Not Request.QueryString("nextaction") Is Nothing AndAlso Request.QueryString("nextaction").Equals("updateHeader") Then
            ELPWebConstants.ExecuteJavascript(" Reload_Header() ", Me.Page)
        End If
        '
    End Sub

    Public ReadOnly Property NoNavMessage() As String
        Get
            Return TranslationBase.TranslateLabelOrMessage(ELPWebConstants.NONAVMESSAGE, ElitaPlusIdentity.Current.ActiveUser.LanguageId)
        End Get

    End Property



#Region "PAGE ROUTINES"


    ' *************************************************************************** '
    '   Sub PopulateFormWithLanguage: Populate the form with corresponding splash
    '                                 screen, imagery, labels and variables for 
    '                                 language selected.
    ' *************************************************************************** '
    Private Sub SetFormImages()

        ' Dim sFlagImage As String = COUNTRIES_FOLDER_PATH & "new_" & moApplicationUser.CompaniesCountryFlagImage.Replace(" ", "")
        Dim sFlagImage As String = COUNTRIES_FOLDER_PATH & "new_" & ElitaPlusIdentity.Current.ActiveUser.CompaniesCountryFlagImage.Replace(" ", "")
        '  Dim sSplashImage As String = COUNTRIES_FOLDER_PATH & "new2_" & moApplicationUser.CompaniesCountrySplashImage.Replace(" ", "")
        Dim sSplashImage As String = COUNTRIES_FOLDER_PATH & "new2_" & ElitaPlusIdentity.Current.ActiveUser.CompaniesCountrySplashImage.Replace(" ", "")
        If File.Exists(Server.MapPath(sSplashImage)) Then
            imgHomeSplash.ImageUrl = sSplashImage
        Else
            imgHomeSplash.ImageUrl = COUNTRIES_FOLDER_PATH & DEFAULT_SPLASH_IMAGE
        End If

        If File.Exists(Server.MapPath(sFlagImage)) Then
            imgCountryFlag.ImageUrl = sFlagImage
        Else
            imgCountryFlag.ImageUrl = COUNTRIES_FOLDER_PATH & DEFAULT_FLAG_IMAGE
        End If

    End Sub


    Sub LoadListItems(ByVal oDataSource As Object)

        With cboLanguage

            .DataSource = oDataSource
            .DataTextField = "Description"
            .DataValueField = "ID"
            .DataBind()

        End With


    End Sub


    Private Sub LoadLanguageInfo()

        Dim oLookUpList As DataView = LookupListNew.GetLanguageLookupList()
        'Me.BindListControlToDataView(cboLanguage, oLookUpList)


        Dim LanguageList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.LanguageList)

        Me.cboLanguage.Populate(LanguageList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

        Me.SetSelectedItem(cboLanguage, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

    End Sub


    Function SetSelectedLanguageID(ByVal languageID As Guid) As Integer
        'find the item that matches the value of the languageid.

        Dim oItem As ListItem
        For Each oItem In cboLanguage.Items
            If oItem.Value = languageID.ToString Then
                oItem.Selected = True
            End If
        Next


    End Function



    Private Sub cboLanguage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboLanguage.SelectedIndexChanged
        Dim LanguageIDGuid As Guid
        ' Dim HomePageLocation As String = ConfigurationMgr.ConfigValue(ELPWebConstants.HOME_PAGE)
        Dim HomePageLocation As String = ELPWebConstants.HOME_PAGE

        'save the language id and the changes to the LANGUAGE_ID
        If cboLanguage.SelectedItem.Value.ToString = NOTHING_SELECTED Then Exit Sub

        'reset the user's language id and save the change.
        LanguageIDGuid = New Guid(cboLanguage.SelectedItem.Value)
        ElitaPlusIdentity.Current.ActiveUser.LanguageId = LanguageIDGuid
        ElitaPlusIdentity.Current.ActiveUser.Save()
        'change the menu editing mode back to view mode.
        Session(ELPWebConstants.MENUSTATE) = ELPWebConstants.enumMenu_State.View_Page_Mode

        'basepage method to send reload script to the header
        Me.ReloadHeader()

        'Response.Redirect(HomePageLocation & "?nextaction=updateHeader")
        'SetFormImages()

    End Sub

    Private Sub Logout()
        Dim AppPath As String = Request.ApplicationPath
        Dim ServerName As String = Request.ServerVariables("SERVER_NAME")
        'Dim url As String = "http://" & ServerName & AppPath & "/Common/"
        Dim url As String = ELPWebConstants.APPLICATION_PATH & "/Common/"
        Response.Redirect(url & "LogOutForm.aspx")
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        'close the main page
        Logout()
        'Dim sScript As String
        'sScript = " window.parent.close() "
        'ELPWebConstants.ExecuteJavascript(sScript, Me.Page)
    End Sub


#End Region


#Region "NAVIGATION FUNCTION CODE"


    Private Sub btnForward_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        NavigateForward()
    End Sub

    Public Sub NavigateForward()


        If ELPWebConstants.GetMenuState = ELPWebConstants.enumMenu_State.View_Page_Mode Then

            'add the current page to the history then redirect to the next page.
            'the current page is added by retrieving the full request path of the page.

            Dim sNextPage As String = "../test/testpage2.aspx"
            Response.Redirect(sNextPage)
        Else
            ELPWebConstants.ShowPopup(NoNavMessage, Me.Page)
        End If
    End Sub


    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        NavigateToPrevious()
    End Sub


    Private Sub NavigateToPrevious()

        If ELPWebConstants.GetMenuState = ELPWebConstants.enumMenu_State.View_Page_Mode Then

            Dim sPreviousPage As String = NavigationHistory.LastPage()
            Response.Redirect(sPreviousPage)

        Else
            ELPWebConstants.ShowPopup(NoNavMessage, Me.Page)
        End If

    End Sub


#End Region


    Private Sub btnExit_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'close the main page
        Dim sScript As String
        sScript = " window.parent.close() "
        ELPWebConstants.ExecuteJavascript(sScript, Me.Page)
    End Sub

End Class
