<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Navigation_Side.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Navigation_Side" SmartNavigation="False" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Navigation_Side</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../Styles.css" type="text/css" rel="STYLESHEET" />
    <script type="text/javascript" language="JavaScript" src="../Navigation/scripts/GlobalHeader.js"></script>
</head>
<body onresize="resizeForm(document.getElementById('scroller'));" bgcolor="#f0f2f5"
    leftmargin="0" topmargin="0" onload="changeScrollbarColor();">
    <form id="Form1" method="post" runat="server">
        <input type="hidden" id="scrollCount" name="scrollCount" runat="server"/>
        <input type="hidden" id="scrollPos" name="scrollPos" runat="server"/>
        <input id="isHomeTab" type="hidden" name="isHomeTab" runat="server"/>
        <input id="Hidden2" type="hidden" name="txtContentPage" runat="server"/>
        <input id="isMinimized" type="hidden" name="isMinimized" runat="server"/>
        <input id="txtContentPage" type="hidden" name="txtContentPage" runat="server"/>
    <table height="100%" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td valign="top" height="5">
                <img height="5" src="./images/trans_spacer.gif">
            </td>
        </tr>
        <tr>
            <td valign="top">
                <img src="./images/trans_spacer.gif" width="5">
            </td>
            <td valign="top" width="170">
                <hr>
            </td>
        </tr>
        <tr height="90%">
            <td valign="top" colspan="2"><div onscroll="saveScrollPosition();" id="scroller" style="overflow: auto; width: 190px;
                    height: 425px">
                    <asp:DataGrid ID="grdNavigationLinks" runat="server" GridLines="none" OnItemCreated="ItemCreated"
                        OnItemCommand="DisplayContentArea" ShowHeader="False" AutoGenerateColumns="false"
                        CssClass="SideNav">
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="NAV_ALWAYS_ALLOWED" HeaderText="NAV_ALWAYS_ALLOWED"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="nav_button">
                                <ItemTemplate><asp:LinkButton runat="server" ID="btnNav" ToolTip='<%# GetLinkText(Container.DataItem("TRANSLATION"))%>'
                                        Text='&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%# GetLinkText(Container.DataItem("TRANSLATION"))%>'
                                        CommandName="nav_now"></asp:LinkButton></ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn Visible="False" DataField="RELATIVE_URL" HeaderText="RELATIVE_URL"></asp:BoundColumn>
                            <asp:BoundColumn Visible="False" DataField="CODE" HeaderText="CODE"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div></td>
        </tr>
        <tr>
            <td valign="top">
                <img src="./images/trans_spacer.gif" width="5">
            </td>
            <td valign="top" width="170">
                <hr>
            </td>
        </tr>
        <tr>
            <td valign="top">
                <img src="./images/trans_spacer.gif" width="5">
            </td>
            <td>
                <asp:Image ID="btnMinimize" Style="cursor: hand" ForeColor="" ImageUrl="../Navigation/images/minimize.gif"
                    ToolTip="Minimize the menu bar to show only icons" runat="server" Width="18px"
                    Height="18px"></asp:Image>
            </td>
        </tr>
        <tr height="10%">
            <td valign="bottom" colspan="2">
                <table width="190" height="100%" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td valign="top" width="190">
                            <img width="190" src="./images/admin_menu_back9.jpg">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script>
	resizeForm(document.getElementById("scroller"));
	
	function resizeForm(item)
	{
		var browseWidth, browseHeight;
		
		if (document.layers)
		{
			browseWidth=window.outerWidth;
			browseHeight=window.outerHeight;
		}
		if (document.all)
		{
			browseWidth=document.body.clientWidth;
			browseHeight=document.body.clientHeight;
		}
		
		if (screen.width == "800" && screen.height == "600") 
		{
			newHeight = browseHeight - 220;
		}
		else
		{
			newHeight = browseHeight - 210;
		}
			
		if (newHeight > 0)
		{
			item.style.height = String(newHeight) + "px";
		}
	}
	
	if (document.all.item('isHomeTab').value != "true")
	{
		if (document.all.item("isMinimized") != null)
		{
			if (document.all.item("isMinimized").value == "true")
			{
				parent.document.frames['Navigation_Side'].frameElement.width= "37px";
			}
			else
			{
				parent.document.frames['Navigation_Side'].frameElement.width= "195px";
			}
		}
	}
	
	setScrollPosition();
		
	function toggleSideMenu()
	{
		if((document.getElementById("isMinimized").value == "false")||(document.getElementById("isMinimized").value == null))
		{
			document.getElementById("isMinimized").value = "true";
			document.getElementById("btnMinimize").src="<%=Request.ApplicationPath()%>/Navigation/images/maximize.gif";
			document.getElementById("btnMinimize").title="Maximize the menu bar to show icons and text";
			parent.document.frames['Navigation_Side'].frameElement.width= "37px";
		}
		else {
			document.getElementById("isMinimized").value = "false";
			document.getElementById("btnMinimize").src="<%=Request.ApplicationPath()%>/Navigation/images/minimize.gif";
			document.getElementById("btnMinimize").title="Minimize the menu bar to show only icons";
			parent.document.frames['Navigation_Side'].frameElement.width= "195px";
		}
	}

    </script>

    </form>
</body>
</html>
