<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MainPage.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.MainPage" %>

<%@ Register Src="navigation_menu.ascx" TagName="navigation_menu" TagPrefix="uc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title>**** Assurant Solutions - Elita System ****</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link rel="stylesheet" href="../Styles.css" type="text/css" />
    <link href="styles/dd.css" rel="stylesheet" type="text/css" />
    <script language="JavaScript" src="../Navigation/scripts/GlobalHeader.js"></script>
    <script language="javascript">

        function closeParent() {
            //debugger; 
            try {
                if (opener && !opener.closed) {
                    if (document.getElementById("closeParentCheck")) {
                        if (document.getElementById("closeParentCheck").value == "1") {
                            var op = window.opener;
                            op.opener = self;
                            op.close();
                        }
                    }
                    else {
                        var op = window.opener;
                        op.opener = self;
                        op.close();
                    }
                }
            }
            catch (exception) {
            }
        }

        closeParent();

        function LogoutNow() {
            var MyDoc = window.frames["Navigation_Header"]
            MyDoc.Logout()
        }

        function Reload_Header() {

            //	window.parent.document.frames["Navigation_Header"].location.href = "Navigation_Header.aspx"
            //	window.parent.document.frames["Navigation_Side"].location.href = "Navigation_Side.aspx?nTabId=" +  document.all["txtNextPageID"].value

        }

        function RedirectParent() {
            debugger;
            alert(this.contentWindow.location);
        }

        if (!String.prototype.startsWith) {
            String.prototype.startsWith = function (searchString, position) {
                position = position || 0;
                return this.indexOf(searchString, position) === position;
            };
        }

        function bodyOnLoad() {
            changeScrollbarColor();

            var iframe = document.getElementById("Navigation_Content");

            iframe.addEventListener("load", function (a) {
                debugger;
                try {
                    var iframeContentDocument = iframe.contentDocument;

                    var urlTokens = document.location.pathname.split("//");
                    if (urlTokens.length > 1)
                        var documentVFolderName = urlTokens[1];
                    
                    urlTokens = iframeContentDocument.location.pathname.split("//");
                    if (urlTokens.length > 1)
                        var iframeVFolderName = urlTokens[1];

                    // Potential XSS but within same domain
                    if (!((iframeContentDocument.location.hostname === document.location.hostname) &&
                        (iframeContentDocument.location.port === document.location.port) &&
                        (iframeContentDocument.location.protocol === document.location.protocol) &&
                        (iframeVFolderName === documentVFolderName))) {
                        document.location.href = iframe.src;
                    }
                } catch (e) {
                    debugger;
                    if (e.description.startsWith("Access is denied")) {
                        document.location.href = iframe.src;
                    } else {
                        throw(e);
                    }
                }
            });
        }

    </script>
</head>
<body oncontextmenu="return false" leftmargin="0" topmargin="0" onload="bodyOnLoad();"
    ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <input id="txtNextPageID" type="hidden" name="txtNextPageID" runat="server" />
        <asp:ScriptManager ID="ScriptManagerMain" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Navigation/scripts/jquery-1.6.1.min.js" ScriptMode="Release" />
                <asp:ScriptReference Path="~/Navigation/scripts/jquery.dd.js" ScriptMode="Release" />
            </Scripts>
        </asp:ScriptManager>
        <input type="hidden" id="serverURL" runat="server" />
        <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td valign="top">
                    <div id="Navigation_Header" class="header">
                        <div class="logo">
                            <asp:Image ID="Image1" ImageUrl="~/Navigation/Images/Assurant_horz_logo_RGB.png" AlternateText="The mark, Assurant, is a trademark of Assurant, Inc., and may not be copied, reproduced, transmitted, displayed, performed, distributed, sublicensed, altered, stored for subsequent use or otherwise used in whole or in part in any manner without Assurant's prior written consent."
                                Width="176" Height="60" runat="server" />
                        </div>
                        <div class="globalNav">
                            <div class="globalNav1">
                                <ul>
                                    <li>|</li>
                                    <li>
                                        <asp:Label ID="lblCompany" runat="server"></asp:Label></li>
                                    <li>|</li>
                                    <li>
                                        <asp:Label ID="lblEnv" runat="server"></asp:Label></li>
                                    <li>
                                        <asp:Label ID="lblBuild" runat="server">Build:&nbsp;</asp:Label></li>
                                    <li>|</li>
                                    <li>
                                        <asp:LinkButton Text="Logout" ID="btnExit" runat="server" Font-Underline="true"></asp:LinkButton></li>
                                </ul>
                            </div>
                            <div class="globalNavSelLang">
                                <asp:DropDownList ID="menuLangDll" Visible="true" runat="server" AutoPostBack="True"
                                    TabIndex="1" OnSelectedIndexChanged="menuLangDll_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <uc1:navigation_menu ID="navigation_menu1" runat="server" />
                </td>
            </tr>
            <tr>
                <td valign="top" height="100%">
                    <table height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td valign="top" width="100%" height="100%">
                                <iframe style="width: 100%; border-top: 0px grey solid; border-left: 1px grey solid;"
                                    id="Navigation_Content" name="Navigation_Content" src="" frameborder="0" width="100%"
                                    scrolling="yes" height="100%" runat="server"
                                    application="yes"></iframe>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
