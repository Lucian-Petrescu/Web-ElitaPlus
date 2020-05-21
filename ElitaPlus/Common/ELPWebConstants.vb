Public Class ELPWebConstants

#Region "ENUMERATIONS"


    Public Enum enumMenu_State
        Editing_Page_Mode
        View_Page_Mode
    End Enum


#End Region


#Region "PUBLIC Constants"


    Public Const UI_MULTIPLE_USERS_SINGLE_NETWORKID_ERR_MSG As String = "UI_MULTIPLE_USERS_SINGLE_NETWORKID"
    Public Const UI_INVALID_LOGIN_ERR_MSG As String = "UI_INVALID_LOGIN"
    Public Const UI_MULTIPLE_USERS_SINGLE_MACHINE_ERR_MSG As String = "UI_MULTIPLE_USERS_SINGLE_MACHINE"
    Public Const UI_INVALID_LOGIN_RIGHTS_ERR_MSG As String = "UI_INVALID_RIGHTS_LOGIN"
    Public Const OLDCOMPANYID As String = "OldCompanyId"
    Public Const NONAVMESSAGE As String = "NO_NAV_MESSAGE"
    Public Const MENUSTATE As String = "MenuState"
    Public Const HOME_PAGE_TAB_ID As String = "HOME_PAGE_TAB_ID"
    Public Const HOME_PAGE As String = "HomeForm.aspx"
    Public Const MAIN_PAGE_NAME As String = "MainPage.aspx"
    Public Const SELECTED_TAB As String = "SELECTED_TAB"
    Public Const AUTHORIZATION_DENIED As String = "AUTHORIZATION_DENIED"
    Public Const COUNTRY_DESCRIPTION_NOT_FOUND As String = "COUNTRY_DESCRIPTION_NOT_FOUND"
    Public Shared CFG_PATH_TO_HOME_PAGE As String = "~/Navigation/HomeForm.aspx"
    Public Shared APPLICATION_PATH As String = HttpContext.Current.Request.ApplicationPath
    Public Const CEHELPER As String = "CEHelper"
    Public Const SSHELPER As String = "SSHelper"
    Public Const ELITA_PLUS_AUTHENTICATION_COOKIE As String = "ELITA_PLUS_AUTHENTICATION_COOKIE"
    Public Const ELITA_PLUS_CONN_COOKIE As String = "ELITA_PLUS_CONN_COOKIE"
    Public Const ELITA_PLUS_ERROR_COOKIE As String = "ELITA_PLUS_ERROR_COOKIE"
    Public Const SETTING_SCHEMA_PATH As String = "../schemas/"
    Public Const SESSION_LOGIN_ERROR_MESSAGE = "SESSION_LOGIN_ERROR_MESSAGE"
    Public Const Session_Elita_Authz_Exception = "SESSION_ELITA_AUTHZ_EXCEPTION"

#End Region



#Region "FUNCTIONS AND SUBS"





    Public Shared Sub ShowTranslatedMessageAsPopup(ByVal sUI_Prog_Code As String,
                                                    ByVal nLanguageID As Guid,
                                                    ByVal oPage As System.Web.UI.Page,
                                                    ByVal oUnhandledException As Exception)

        'This function is used when the error is known 
        'but we want to show the original exception message.
        'mostly used for system debugging and logging.

        Try
            '  Dim sMessage As String = ApplicationMessages.GetApplicationMessage(sUI_Prog_Code, nLanguageID)
            Dim sMessage As String = TranslationBase.TranslateLabelOrMessage(sUI_Prog_Code, nLanguageID)
            ShowPopup(sMessage & " " & oUnhandledException.Message.Replace("'", String.Empty), oPage)


        Catch ex As Exception
            ShowPopup(ex.Message & "  " & oUnhandledException.Message.Replace("'", String.Empty), oPage)
        End Try

    End Sub


    Public Shared Sub ShowTranslatedMessageAsPopup(ByVal sUI_Prog_Code As String,
                                                   ByVal nLanguageID As Guid,
                                                   ByVal oPage As System.Web.UI.Page)
        'This function is used when the error is known but we 
        ' do NOT want to show the original exception message to the user.
        Try
            Dim sMessage As String = TranslationBase.TranslateLabelOrMessage(sUI_Prog_Code, nLanguageID)
            ShowPopup(sMessage, oPage)

        Catch ex As Exception
            ShowPopup(ex.Message, oPage)
        End Try

    End Sub



    '-------------------------------------
    'Name:ShowPopup
    'Purpose:'Use this version if you have more than one message to send to the user.
    'a unique name allows multiple messages to be sent. If you leave the name blank,
    ' then only the first message will be sent to the user.
    'Input Values:sMessageContent and the page object that is used to display the popup.
    'Uses:
    '-------------------------------------
    Public Shared Sub ShowPopup(ByVal messageContent As String, ByVal messageName As String, ByVal page As System.Web.UI.Page)

        messageContent = ("<script language=JavaScript>alert('" & messageContent & "')</script>")
        If Not page.IsStartupScriptRegistered(messageName) Then
            page.RegisterStartupScript(messageName, messageContent)
        End If

    End Sub



    Public Shared Function GetHashTable(ByVal ds As DataSet, ByVal DescriptionColumn As String, ByVal ValueColumn As String) As Hashtable

        Dim oRow As DataRow
        Dim oHash As New Hashtable
        Dim TempByteArray() As Byte

        For Each oRow In ds.Tables(0).Rows
            Dim oGUID As Guid

            With oHash

                TempByteArray = CType(oRow.Item(ValueColumn), Byte())

                oGUID = New Guid(TempByteArray)

                .Add(oRow.Item(DescriptionColumn), oGUID.ToString)

            End With
        Next

        Return oHash

    End Function

    Public Shared Function GetMenuState() As enumMenu_State

        'load or retrieve the menustate from the session.
        If Not HttpContext.Current.Session(MENUSTATE) Is Nothing Then
            'this executes on all runs except the first.
            Return CType(HttpContext.Current.Session(MENUSTATE), enumMenu_State)
        Else
            'this executes only the first page load so set the menu state to view mode.
            'also,set the session variable for the menu state.
            HttpContext.Current.Session(MENUSTATE) = ELPWebConstants.enumMenu_State.View_Page_Mode
            Return enumMenu_State.View_Page_Mode
        End If

    End Function




    '-------------------------------------
    'Name:ShowPopup
    'Purpose:'use this version if you only have a single message to be sent to the user.
    'Input Values:sMessageContent and the page object that is used to display the popup.
    'Uses:
    '-------------------------------------
    Public Shared Sub ShowPopup(ByVal messageContent As String, ByVal page As System.Web.UI.Page)

        Dim nRandomNumber As New Random
        Dim sTempName As String = nRandomNumber.Next.ToString
        Dim sScript As String

        'sScript = ("<script language=JavaScript>alert('" & messageContent & "')</script>")

        sScript = "<SCRIPT language=JavaScript>" & Environment.NewLine
        sScript &= "showMessage('" & messageContent & "', '" & "Alert" & "', '1', '2', 'null');" & Environment.NewLine
        sScript &= "</SCRIPT>" & Environment.NewLine

        page.RegisterStartupScript(sTempName, sScript)



    End Sub


    '-------------------------------------
    'Name:ExecuteJavascript
    'Purpose: Execute  javascript code at the end of loading the page.
    'Input Values:messageContent  and page object.
    'Uses:
    '-------------------------------------
    Public Shared Sub ExecuteJavascript(ByVal messageContent As String, ByVal page As System.Web.UI.Page)
        messageContent = ("<script language=JavaScript>" & messageContent & "</script>")
        page.RegisterStartupScript("somescript", messageContent)
    End Sub



    Public Shared Sub ExecuteJavascript(ByVal sScriptName As String, ByVal messageContent As String, ByVal page As System.Web.UI.Page)
        messageContent = ("<script language=JavaScript>" & messageContent & "</script>")
        page.RegisterStartupScript(sScriptName, messageContent)
    End Sub


#End Region


End Class
