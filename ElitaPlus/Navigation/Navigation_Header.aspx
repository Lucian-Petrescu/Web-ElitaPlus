<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Navigation_Header.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Navigation_Header" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Navigation_Header</title>
    <meta http-equiv="Content-Type" content="text/html; charset=windows-1252">
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/scripts/GlobalHeader.js"></script>

    <!--<script language="JavaScript" src="../Navigation/scripts/AsyncTimer.js"></script>-->

    <script language="javascript">
        function Logout() {
            document.all["txtTabID"].value = "LOGOUT"
            Form1.submit()

        }

        function Login() {
            document.all["txtTabID"].value = "LOGIN"
            Form1.submit()

        }
		
		
    </script>

</head>
<body oncontextmenu="return false;" style="background-repeat: repeat" leftmargin="0"
    topmargin="0" rightmargin="0">
    <form id="Form1" method="post" runat="server">
    <input id="txtTabID" type="hidden" name="txtTabID" runat="server">
    <input id="displayMode" type="hidden" name="displayMode" runat="server">
    <table class="HEADER_TABLE" cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr valign="top">
            <td>
                <table style="background-repeat: repeat" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td id="tdHeader" width="100%" background="images/header_rebrand_spacer.jpg" colspan="5"
                            height="59">
                            <table height="59" cellspacing="0" cellpadding="0" border="0" style="height: 59px"
                                width="100%">
                                <tr>
                                    <td valign="top" nowrap width="226" height="59">
                                        <img alt="The mark, Assurant, is a trademark of Assurant, Inc., and may not be copied, reproduced, transmitted, displayed, performed, distributed, sublicensed, altered, stored for subsequent use or otherwise used in whole or in part in any manner without Assurant's prior written consent."
                                            src="images/header_rebrand8.jpg" width="226" style="height: 59px">
                                    </td>
                                    <td align="right" colspan="1">
                                        <asp:Label ID="lblEnv" runat="server" ForeColor="white"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap width="35" background="images/tab_middle_back4.gif">
                            <img height="22" src="images/trans_spacer.gif" width="35">
                        </td>
                        <td>
                             <asp:Menu ID="mnu" runat="server" ItemWrap="false" Orientation="Horizontal">
                                <DataBindings> 
                                    <asp:MenuItemBinding  DataMember="TAB_AUTHORIZATION" TextField="translation" 
                                        ValueField="code" />
                                    <asp:MenuItemBinding  DataMember="FORM_AUTHORIZATION" TextField="translation" 
                                        ValueField="code" />
                                </DataBindings>
                            </asp:Menu>
                            <asp:XmlDataSource ID="xmlSource" runat="server" EnableCaching="false"  XPath="/*/*"></asp:XmlDataSource>
                            
                            <%--   <asp:DataList ID="dlstTabs" runat="server" RepeatDirection="Horizontal" CellPadding="0"
                                DataKeyField="CODE" CssClass="TopMenu" CellSpacing="0">
                                <SelectedItemStyle CssClass="TopMenuSelected"></SelectedItemStyle>
                                <FooterStyle CssClass="TopMenuFooter"></FooterStyle>
                                <ItemTemplate>
                                    <table id="Table1" style="background-repeat: repeat" onmouseout="javascript:document.body.style.cursor = 'default';"
                                        cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td background="images/tab_middle_back4.gif">
                                                <hr style="width: 1px; height: 10px" color="whitesmoke">
                                            </td>
                                            <td background="images/tab_middle_back4.gif">
                                                &nbsp;&nbsp;
                                            </td>
                                            <td id="Td2" background='<%# GetTabImage(enumAlignment.MIDDLE, Container.DataItem("CODE"))%>'>
                                                <asp:Image ID="imgHomeTabLeftSpacer" runat="server" ImageUrl="images/trans_spacer.gif"
                                                    Width="10" Height="1"></asp:Image>
                                            </td>
                                            <td id="Td1" onclick="setCell(this,'<%# Container.DataItem("CODE")%>')" onmouseover="changeCell(this);"
                                                valign="middle" nowrap width="100%" background='<%# GetTabImage(enumAlignment.MIDDLE, Container.DataItem("CODE"))%>'
                                                height="22">
                                                <asp:Label ID="hlnkTabTitle" runat="server" Text='<%# GetTabTitle(Container.DataItem("CODE"))%>'>
                                                </asp:Label>
                                            </td>
                                            <td id="Td3" background='<%# GetTabImage(enumAlignment.MIDDLE, Container.DataItem("CODE"))%>'>
                                                <asp:Image ID="Image1" runat="server" ImageUrl="images/trans_spacer.gif" Width="10"
                                                    Height="1"></asp:Image>
                                            </td>
                                            <td background="images/tab_middle_back4.gif">
                                                &nbsp;&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <HeaderStyle  ForeColor="White" BackColor="#006699"></HeaderStyle>
                            </asp:DataList>--%>
                        </td>
                        <td valign="bottom" nowrap width="100%" background="images/tab_middle_back4.gif">
                            <img height="22" src="images/trans_spacer.gif" width="100%">
                        </td>
                        <td valign="middle" nowrap align="right" width="21" background="images/tab_middle_back4.gif">
                            <asp:Image ID="imgDisplayMode" runat="server" ImageUrl="images/icons/max_icon.gif"
                                AlternateText="Maximize display view"></asp:Image>
                        </td>
                        <td background="images/tab_middle_back4.gif">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td width="100%" colspan="5">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td style="height: 6px; width: 212px; background: url(images/menu_bottom_left.gif) no-repeat;
                                        vertical-align: top; margin: 0; border: 0; padding: 0">
                                    </td>
                                    <td valign="top" width="100%" style="height: 6px; background: url(images/menu_bottom_spacer.gif) repeat-x;
                                        vertical-align: top; margin: 0; border: 0; padding: 0">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script>

        var _td1 = document.getElementById("Td1");
        var _td2 = document.getElementById("Td2");
        var _td3 = document.getElementById("Td3");

        setHeaderView();
        //fncSetTimeout();
        //	
        //function changeCell(el) 
        //{
        //if (document.getElementById("dlstTabs").disabled == "false" || document.getElementById("dlstTabs").disabled == "" )
        //   {
        //		document.body.style.cursor = 'hand';
        //		if (el.background!="/ElitaPlus/Navigation/images/tab_select_middle_back_b3.gif")
        //		{el.style.background="#666666";
        //		el.onmouseout=function() {if (el.background!="/ElitaPlus/Navigation/images/tab_select_middle_back_b3.gif") 
        //		el.style.background="#999999";}}
        //	}
        //}

        //function setCell(el,sel) 
        //{
        //   if (document.getElementById("dlstTabs").disabled == "false" || document.getElementById("dlstTabs").disabled == "" )
        //   {
        //		// Clear previous selected option
        //   		var _TDList=document.getElementsByTagName("TD");
        //		for (i = 0; i < _TDList.length; i++){
        //			if (_TDList.item(i).background == "/ElitaPlus/Navigation/images/tab_select_middle_back_b3.gif"){
        //				_TDList.item(i).background="/ElitaPlus/Navigation/images/tab_middle_back4.gif";
        //			}
        //		}
        //		
        //      _td1.style.background="#999999";
        //      _td2.style.background="#999999";
        //      _td3.style.background="#999999";
        //      
        //      el.background="/ElitaPlus/Navigation/images/tab_select_middle_back_b3.gif";
        //      el.style.background="#666666";
        //      _td1=el;

        //      if (sel == 'HOME PAGE') {
        //         parent.document.frames['Navigation_Side'].frameElement.width = '0px';
        //        // document.getElementById("tdBarImage").width = "212";
        //        // document.getElementById("tdBar").width = "212";
        //         window.parent.frames('Navigation_Content').location.href= 'HomeForm.aspx';
        //      }
        //      else {
        //         parent.document.frames['Navigation_Side'].frameElement.width = '195px';
        //        // document.getElementById("tdBar").width = "197";
        //        // document.getElementById("tdBarImage").width = "197";
        //         // if (sel != 'OTHER') {window.parent.frames('Navigation_Content').location.href= 'Navigation_'+sel+'Splash.aspx';}
        //         if (sel != 'OTHER') {window.parent.frames('Navigation_Content').location.href= '../NavSplash/Navigation_'+sel+'Splash.aspx';}
        //         parent.document.frames('Navigation_Side').location.href = 'Navigation_Side.aspx?nTabId='+sel;
        //      }
        //   }
        //}

        function setHeaderView() {
            if ((document.getElementById("displayMode").value == "visible") || (document.getElementById("displayMode").value == "")) {
                document.getElementById("tdHeader").style.display = "";
                document.getElementById("imgDisplayMode").src = "./images/icons/max_icon.gif"
                document.getElementById("imgDisplayMode").alt = "Maximize display view"
                parent.document.all.item("Navigation_Header").style.height = "90px";
            }
            else if (document.getElementById("displayMode").value == "hidden") {
                document.getElementById("tdHeader").style.display = "none";
                document.getElementById("imgDisplayMode").src = "./images/icons/min_icon.gif"
                document.getElementById("imgDisplayMode").alt = "Restore display view"
                parent.document.all.item("Navigation_Header").style.height = "31px";
            }
        }

        function toggleHeaderView() {
            if ((document.getElementById("displayMode").value == "visible") || (document.getElementById("displayMode").value == "")) {
                document.getElementById("tdHeader").style.display = "none";
                document.getElementById("displayMode").value = "hidden"
                document.getElementById("imgDisplayMode").src = "./images/icons/min_icon.gif"
                document.getElementById("imgDisplayMode").alt = "Restore display view"
                parent.document.all.item("Navigation_Header").style.height = "31px";
            }
            else if (document.getElementById("displayMode").value == "hidden") {
                document.getElementById("tdHeader").style.display = "";
                document.getElementById("displayMode").value = "visible"
                document.getElementById("imgDisplayMode").src = "./images/icons/max_icon.gif"
                document.getElementById("imgDisplayMode").alt = "Maximize display view"
                parent.document.all.item("Navigation_Header").style.height = "90px";
            }
        }
    </script>

    </form>
</body>
</html>
