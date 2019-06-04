Imports System.Text.RegularExpressions

Partial Class MessageBoxForm
    Inherits System.Web.UI.Page


#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'Translate()
        Dim test As String = ""
        Dim argArr As String() = Regex.Split(Request.QueryString("arg"), "~")

        If argArr(3) = "0" Then
            imgMsgIcon.Src = "../Navigation/images/infoIcon.gif"
        ElseIf argArr(3) = "1" Then
            imgMsgIcon.Src = "../Navigation/images/questionIcon.gif"
        ElseIf argArr(3) = "2" Then
            imgMsgIcon.Src = "../Navigation/images/warningIcon.gif"
        End If
        Dim translatedMsg As String

        If Not Session(ElitaPlusPage.SESSION_TRANSLATION) Is Nothing Then
            translatedMsg = CType(Session(ElitaPlusPage.SESSION_TRANSLATION), String)
            Session.Remove(ElitaPlusPage.SESSION_TRANSLATION)
            tdBody.InnerHtml = translatedMsg
        Else
            tdBody.InnerHtml = argArr(0)
        End If

        'tdBody.InnerHtml = argArr(0)
        'tdBody.InnerHtml = translatedMsg

        If argArr(2) = "1" Then
            tdButtons.InnerHtml = "<input id='button1' type=button name=OK value=OK onClick='window.returnValue = 1;window.close();' style='Height:22px;Width:100px;Color:#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/yes_icon2.gif); CURSOR: hand' Class='FLATBUTTON'>"
        ElseIf argArr(2) = "2" Then
            tdButtons.InnerHtml = "<input id='button1' type=button name=Cancel value=Cancel onClick='window.returnValue =0;window.close();' style='Height:22px;Width:100px;Color:#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand' Class='FLATBUTTON'>"
        ElseIf argArr(2) = "3" Then
            tdButtons.InnerHtml = "<input id='button1' type=button name=OK value=OK onClick='window.returnValue = 1;window.close();' style='Height:22px;Width:100px;Color:#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/yes_icon2.gif); CURSOR: hand' Class='FLATBUTTON'>&nbsp;&nbsp;<input id='button1' type=button name=Cancel value=Cancel onClick='window.returnValue =0;window.close();' style='Height:22px;Width:100px;Color:#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand' Class='FLATBUTTON'>"
        ElseIf argArr(2) = "4" Then
            tdButtons.InnerHtml = "<input id='button1' type=button name=YES value=Yes style='Height:22px;Width:100px;Color:#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/yes_icon2.gif); CURSOR: hand' Class='FLATBUTTON' onClick='window.returnValue = 2;window.close();'>&nbsp;&nbsp;<input id='button2' type=button name=NO value=No style='font-bold:true;Height:22px;Width:100px;Color:#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/no_icon2.gif); CURSOR: hand' Class='FLATBUTTON' onClick='window.returnValue = 3;window.close();'>"
        ElseIf argArr(2) = "5" Then
            tdButtons.InnerHtml = "<input id='button1' type=button name=YES value=Yes style='Height:22px;Width:100px;Color:#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/yes_icon2.gif); CURSOR: hand' Class='FLATBUTTON' onClick='window.returnValue = 2;window.close();'>&nbsp;&nbsp;<input id='button2' type=button name=NO value=No style='font-bold:true;Height:22px;Width:100px;Color:#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/no_icon2.gif); CURSOR: hand' Class='FLATBUTTON' onClick='window.returnValue = 3;window.close();'>&nbsp;&nbsp;<input id='button3' type=button name=Cancel value=Cancel onClick='window.returnValue = 0;window.close();' style='font-weight:bold;Height:22px;Width:100px;Color:#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand' Class='FLATBUTTON'>"
        ElseIf argArr(2) = "6" Then
            tdButtons.InnerHtml = "<input id='button1' type=button name=DEALER value=Dealer style='Height:22px;Width:100px;Color:#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; CURSOR: hand' Class='FLATBUTTON' onClick='window.returnValue = 1;window.close();'>&nbsp;&nbsp;<input id='button2' type=button name=SERVICE_CENTER value='Service Center' style='font-bold:true;Height:22px;Width:100px;Color:#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; CURSOR: hand' Class='FLATBUTTON' onClick='window.returnValue = 2;window.close();'>&nbsp;&nbsp;"
        ElseIf argArr(2) = "7" Then
            tdButtons.InnerHtml = "<input id='button1' type=button name=YES value=Yes style='Height:22px;Width:100px;Color:#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/yes_icon2.gif); CURSOR: hand' Class='FLATBUTTON' onClick='window.returnValue = 2;window.close();'>"
        End If

        Translate()
    End Sub

    Public Function Translate() As String
        Dim langId As Guid = ElitaPlusIdentity.Current.ActiveUser.LanguageId
        Dim TransProcObj As New TranslationProcess
        TransProcObj.TranslateThePage(Me, langId)
    End Function



End Class
