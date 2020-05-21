Imports System.IO
Imports System.Threading
Imports Assurant.Elita.CommonConfiguration
Imports Assurant.ElitaPlus.Security
Imports Assurant.Elita.Web.Forms

Partial Class MainPage
    Inherits ElitaPlusPage 'System.Web.UI.Page



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

    'From homeform.aspx.vb
    Private Const NOTHING_SELECTED As String = "00000000-0000-0000-0000-000000000000"
    Private Const LANGUAGE_FLAG_FOLDER_PATH As String = "/Navigation/images/flags/"
    Private Const DEFAULT_FLAG_IMAGE As String = "us_Flag.png"

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Dim loginError As String = HttpContext.Current.Session(ELPWebConstants.Session_Elita_Authz_Exception)
            If Not String.IsNullOrEmpty(loginError) Then
                HttpContext.Current.Session(ErrorForm.MESSAGE_KEY_NAME) = loginError
                HttpContext.Current.Session(ELPWebConstants.Session_Elita_Authz_Exception) = Nothing

                ' Release the session
                context.Response.Redirect(ELPWebConstants.APPLICATION_PATH & "/Common/ErrorForm.aspx")
            End If

            'Register jquery call for the drop down language menu image display
            If Not Page.ClientScript.IsClientScriptBlockRegistered("ddlLangMenu") Then
                RegisterDDLScript()
            End If


            If Not Page.IsPostBack() Then
                Me.AppHomeUrl = Me.Request.Url.AbsolutePath

                'load the data to the drop down
                LoadLanguageInfo()

                'add the title to the drop down for images
                BindTitles()

                Me.lblCompany.Text = ElitaPlusIdentity.Current.ActiveUser.CompanyGroup.Description

                ' Add environment
                Me.lblEnv.Text += EnvironmentContext.Current.EnvironmentName
                'Added to display the separate paths in Test and Model
                If ((EnvironmentContext.Current.Environment <> Environments.Development) AndAlso (EnvironmentContext.Current.Environment <> Environments.Production)) Then
                    Me.lblEnv.Text += " - " + AppConfig.ConnType
                End If

                ' Added build version
                Dim sBuildNum As String
                Dim sBuildArr As String()
                sBuildNum = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
                'Display only Major.Minor.Build. Remove Revision
                sBuildArr = sBuildNum.Split(CChar("."))
                sBuildNum = String.Empty
                sBuildNum = sBuildArr(0).ToString() + "." + sBuildArr(1).ToString() + "." + sBuildArr(2).ToString()
                Me.lblBuild.Text += sBuildNum


            End If

            'Put user code to initialize the page here

            Dim AppPath As String = Request.ApplicationPath
            Dim ServerName As String = Request.ServerVariables("SERVER_NAME")

            serverURL.Value = ELPWebConstants.APPLICATION_PATH & "/"

            If Not Session("RedirectUrl") Is Nothing Then
                Me.Navigation_Content.Attributes.Add("src", Session("RedirectUrl").ToString())
                Session.Remove("RedirectUrl")
            Else
                Me.Navigation_Content.Attributes.Add("src", "HomeForm.aspx")
            End If


            Me.btnExit.Text = TranslationBase.TranslateLabelOrMessage("LOGOUT")

            ''user noification chages REQ-5279
            'If UserNotification.UserHasNotifications(Guid.Empty) Then
            '    Session("PageCalledFrom") = "MAINPAGE"
            '    Dim NotificationListURL As String = ELPWebConstants.APPLICATION_PATH & "/Security/NotificationListForm.aspx"
            '    'Me.Response.Redirect(NotificationListURL)
            '    Me.callPage(NotificationListURL, "MAINPAGE")
            'End If

        Catch ex As DataNotFoundException
            ELPWebConstants.ShowPopup(ex.Message, Me.Page)

        Catch ex As ApplicationException
            ELPWebConstants.ShowPopup(ex.Message, Me.Page)

        Catch ex As NullReferenceException
            ELPWebConstants.ShowPopup(ex.Message, Me.Page)

        End Try



    End Sub

    Protected Sub menuLangDll_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) _
    Handles menuLangDll.SelectedIndexChanged

        Dim LanguageIDGuid As Guid
        ' Dim HomePageLocation As String = ConfigurationMgr.ConfigValue(ELPWebConstants.HOME_PAGE)
        Dim HomePageLocation As String = ELPWebConstants.HOME_PAGE

        'save the language id and the changes to the LANGUAGE_ID
        If menuLangDll.SelectedItem.Value.ToString = NOTHING_SELECTED Then Exit Sub

        'reset the user's language id and save the change.
        LanguageIDGuid = New Guid(menuLangDll.SelectedItem.Value)
        ElitaPlusIdentity.Current.ActiveUser.LanguageId = LanguageIDGuid
        ElitaPlusIdentity.Current.ActiveUser.Save()
        'change the menu editing mode back to view mode.
        Session(ELPWebConstants.MENUSTATE) = ELPWebConstants.enumMenu_State.View_Page_Mode

        'basepage method to send reload script to the header
        Me.ReloadHeader()

    End Sub


    'Imported from HomeForm.aspx.vb
    Private Sub LoadLanguageInfo()

        Dim ElitaBasePage As ElitaPlusPage

        'Dim oLookUpList As DataView = LookupListNew.GetLanguageLookupList()
        'ElitaBasePage.BindListControlToDataView(menuLangDll, oLookUpList)

        Dim LanguageList As DataElements.ListItem() =
                CommonConfigManager.Current.ListManager.GetList(listCode:=ListCodes.LanguageList)

        Me.menuLangDll.Populate(LanguageList.ToArray(),
                                New PopulateOptions() With
                                {
                                    .AddBlankItem = True
                                })

        ElitaBasePage.SetSelectedItem(menuLangDll, ElitaPlusIdentity.Current.ActiveUser.LanguageId)

    End Sub

    Protected Sub BindTitles()
        If menuLangDll IsNot Nothing Then
            For Each li As ListItem In menuLangDll.Items
                Dim fileName As String
                Dim sFlagImage As String
                Dim languageCode As String = LookupListNew.GetCodeFromId(LookupListNew.LK_LANGUAGES, New Guid(li.Value))

                If Not languageCode Is Nothing Then
                    sFlagImage = LANGUAGE_FLAG_FOLDER_PATH & languageCode.Trim & "_Flag.png"
                    Dim temp As String = Server.MapPath(ELPWebConstants.APPLICATION_PATH & sFlagImage)
                    If File.Exists(temp) Then
                        fileName = sFlagImage
                    Else
                        fileName = LANGUAGE_FLAG_FOLDER_PATH & DEFAULT_FLAG_IMAGE
                    End If
                    li.Attributes("title") = ELPWebConstants.APPLICATION_PATH & fileName
                End If
            Next
        End If
    End Sub

    Protected Sub btnExit_Click(sender As Object, e As EventArgs) Handles btnExit.Click
        Dim AppPath As String = Request.ApplicationPath
        Dim ServerName As String = Request.ServerVariables("SERVER_NAME")
        Dim url As String = ELPWebConstants.APPLICATION_PATH & "/Common/"
        Response.Redirect(url & "LogOutForm.aspx")
    End Sub

    Public Sub RegisterDDLScript()

        Dim Script As New System.Text.StringBuilder()
        Script.Append("<script type=""text/javascript""> $(document).ready(function () { try { oHandler = $(""#menuLangDll"").msDropDown({ mainCSS: 'dd2' }).data(""dd""); $(""#ver"").html($.msDropDown.version); } catch (e) { alert(""Error: "" + e.message); }}); ToggleMenus(); </" & "script>")
        ' Register script
        Page.ClientScript.RegisterStartupScript(Me.GetType(), "ddlLangMenu", Script.ToString())

    End Sub

End Class
