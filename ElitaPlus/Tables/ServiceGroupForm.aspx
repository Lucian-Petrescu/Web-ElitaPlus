<%@ Register TagPrefix="uc1" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ServiceGroupForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ServiceGroupForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>TemplateForm</title>
    <!-- ************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebForm.cst (9/20/2004)  ******************** -->
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet"/>
        <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet"/>
        <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js" > </script>
        <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js" > </script>
        <script type="text/javascript">
        $(function () {
            $("#tabs").tabs({
                activate: function() {
                    var selectedTab = $('#tabs').tabs('option', 'active');
                    $("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);
                },
                active: document.getElementById('<%= hdnSelectedTab.ClientID %>').value
            });
        });
        </script> 
</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
            border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
            cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td style="vertical-align:top;">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <p>
                                    &nbsp;
                                    <asp:Label ID="LabelTables" runat="server" Cssclass="TITLELABEL">Tables</asp:Label>:
                                    <asp:Label ID="Label40" runat="server"  Cssclass="TITLELABELTEXT">Service_Group</asp:Label></p>
                            </td>
                            <td align="right" height="20">
                                <strong>*</strong>
                                <asp:Label ID="Label9" runat="server" Font-Bold="false">INDICATES_REQUIRED_FIELDS</asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
            margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
            cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
            <!--d5d6e4-->
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align:center; vertical-align:top;">
                    <asp:Panel ID="WorkingPanel" runat="server" Width="100%" Height="98%">
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                            border-left: #999999 1px solid; border-bottom: #999999 1px solid; height: 95%"
                            cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea"
                            border="0">
                            <tr>
                                <td valign="middle" align="center" colspan="4">
                                    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="vertical-align:top;">
                                    <asp:Panel ID="EditPanel_WRITE" runat="server" Width="100%">
                                        <table id="Table1" style="width: 100%" cellspacing="1" cellpadding="0" width="710"
                                            border="0">
                                            <tr>
                                                <td valign="middle" align="right" width="15%">
                                                    <asp:Label ID="moCountryLabel" runat="server" Font-Bold="false" Width="90%">Country</asp:Label>:</td>
                                                <td valign="middle" align="left" width="30%">
                                                    &nbsp;
                                                    <asp:Label ID="moCountryLabel_NO_TRANSLATE" runat="server" Width="134px"></asp:Label>
                                                    <asp:DropDownList ID="moCountryDrop" runat="server" AutoPostBack="True">
                                                    </asp:DropDownList></td>
                                                <td align="right" width="15%">
                                                </td>
                                                <td align="left" width="30%">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle" align="right" width="15%">
                                                    <asp:Label ID="LabelShortDesc" runat="server" Font-Bold="false" Width="100%">Code</asp:Label></td>
                                                <td valign="middle" align="left" width="30%">
                                                    &nbsp;
                                                    <asp:TextBox ID="TextboxShortDesc" TabIndex="10" runat="server" Width="134px" CssClass="FLATTEXTBOX"></asp:TextBox></td>
                                                <td valign="middle" align="right" width="15%">
                                                    <asp:Label ID="LabelDescription" runat="server" Font-Bold="false" Width="100%">Description</asp:Label></td>
                                                <td valign="middle" align="left" width="30%">
                                                    &nbsp;
                                                    <asp:TextBox ID="TextboxDescription" TabIndex="15" runat="server" Width="189px" CssClass="FLATTEXTBOX"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td valign="middle" align="center" colspan="4">
                                                    <hr>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="middle" align="left" colspan="4">
                                                    <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
                                                    <div id="tabs" class="style-tabs-old style-tabs-oldBG">
                                                        <ul>
                                                        <li style="background:#d5d6e4"><a href="#tabSGDetails"><asp:Label ID="Label4" runat="server" CssClass="tabHeaderTextOld">Service_Group_Detail</asp:Label></a></li>                                                                
                                                        </ul>
                                                        <div id="tabSGDetails"  style="background:#d5d6e4">
                                                            <table id="tblDetail" style="width: 100%; height: 90%" cellspacing="2" cellpadding="2"
                                                                rules="cols" border="0">
                                                                <tr>
                                                                    <td style="vertical-align:top;" colspan="2" width="50%">
                                                                        <asp:DataGrid ID="DataGridDetail" Width="100%" runat="server" AutoGenerateColumns="False"
                                                                            BorderStyle="Solid" TabIndex="18" BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999"
                                                                            CellPadding="1" AllowPaging="True" AllowSorting="True" OnItemCreated="ItemCreated">
                                                                            <SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
                                                                            <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
                                                                            <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                                            <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                                            <HeaderStyle></HeaderStyle>
                                                                            <Columns>
                                                                                <asp:TemplateColumn HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="1%">
                                                                                    <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="btnDeleteRiskType_WRITE" Style="cursor: hand" runat="server"
                                                                                            CommandName="DeleteRiskType" ImageUrl="../Navigation/images/icons/trash.gif"></asp:ImageButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:BoundColumn Visible="False" />
                                                                                <asp:BoundColumn SortExpression="1" HeaderText="Risk_Type">
                                                                                    <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="50%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                </asp:BoundColumn>
                                                                                <asp:BoundColumn SortExpression="2" HeaderText="Manufacturer">
                                                                                    <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="50%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                </asp:BoundColumn>
                                                                                <asp:BoundColumn Visible="False" />
                                                                            </Columns>
                                                                            <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                                                PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                                                                        </asp:DataGrid>
                                                                    </td>
                                                                    <td style="vertical-align:top;" colspan="2" width="50%">
                                                                        <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0" designtimedragdrop="94">
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="LabelRiskType" runat="server">Risk_Type</asp:Label>:
                                                                                </td>
                                                                                <td>
                                                                                    <p>
                                                                                        <asp:DropDownList ID="DropDownRiskType" runat="server" Width="158px" AutoPostBack="True"
                                                                                            TabIndex="20">
                                                                                        </asp:DropDownList></p>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:Label ID="LabelManufacturer" runat="server">Manufacturer</asp:Label>:
                                                                                </td>
                                                                                <td>
                                                                                    <asp:RadioButtonList ID="RadioButtonListManufacturer" runat="server" RepeatDirection="Horizontal"
                                                                                        AutoPostBack="True" TabIndex="30" RepeatLayout="Flow">
                                                                                        <asp:ListItem Value="Selected" Selected="True">Selected</asp:ListItem>
                                                                                        <asp:ListItem Value="Any">Any</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="center" colspan="2">
                                                                                    <uc1:UserControlAvailableSelected ID="UserControlAvailableSelectedManufacturers"
                                                                                        runat="server"></uc1:UserControlAvailableSelected>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>

                                                    <%--<mytab:TabStrip ID="tsHoriz"  runat="server" TabDefaultStyle="border:solid 1px black;background:#f1f1f1;padding-top:2px;padding-bottom:2px;padding-left:7px;padding-right:7px;"
                                                        TabHoverStyle="background:#faecc2" TabSelectedStyle="border:solid 1px black;border-bottom:none;background:#d5d6e4;padding-left:7px;padding-right:7px;"
                                                        SepDefaultStyle="border-bottom:solid 1px #000000;" TargetID="mpHoriz">
                                                        <mytab:Tab Text="Service_Group_Detail" DefaultImageUrl=""></mytab:Tab>
                                                    </mytab:TabStrip>
                                                    <mytab:MultiPage ID="mpHoriz" Style="border-right: #000000 1px solid; padding-right: 5px;
                                                        border-top: #000000 1px solid; padding-left: 5px; background: #d5d6e4; padding-bottom: 5px;
                                                        border-left: #000000 1px solid; padding-top: 5px; border-bottom: #000000 1px solid"
                                                        runat="server" Height="350px" Width="100%">
                                                        <mytab:PAGEVIEW>
                                                            <!-- Tab begin -->
                                                            
                                                        </mytab:PAGEVIEW>
                                                    </mytab:MultiPage>--%>

                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" colspan="4">
                                    <hr style="width: 100%; height: 1px" size="1">
                                </td>
                            </tr>
                            <tr>
                                <td valign="bottom" nowrap align="left" height="20">
                                    <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                        cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
                                        Width="90px" CssClass="FLATBUTTON" Text="Back" Height="20px"></asp:Button>&nbsp;
                                    <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                        cursor: hand; background-repeat: no-repeat" TabIndex="190" runat="server" Font-Bold="false"
                                        Width="90px" CssClass="FLATBUTTON" Text="Save" Height="20px"></asp:Button>&nbsp;
                                    <asp:Button ID="btnUndo_Write" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                        cursor: hand; background-repeat: no-repeat" TabIndex="195" runat="server" Font-Bold="false"
                                        Width="90px" CssClass="FLATBUTTON" Text="Undo" Height="20px"></asp:Button>&nbsp;
                                    <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                        cursor: hand; background-repeat: no-repeat" TabIndex="200" runat="server" Font-Bold="false"
                                        Width="81px" CssClass="FLATBUTTON" Text="New" Height="20px"></asp:Button>&nbsp;&nbsp;
                                    <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                        cursor: hand; background-repeat: no-repeat" TabIndex="205" runat="server" Height="20px"
                                        Width="136px" CssClass="FLATBUTTON" Text="NEW_WITH_COPY" CausesValidation="False">
                                    </asp:Button>
                                    <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                        cursor: hand; background-repeat: no-repeat" TabIndex="210" runat="server" Font-Bold="false"
                                        Width="100px" CssClass="FLATBUTTON" Text="Delete" Height="20px"></asp:Button></td>
                            </tr>
                        </table>
                        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                               runat="server" designtimedragdrop="261"/>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>

    <script>
			//resizeScroller(document.getElementById("scroller"));
			
			function resizeScroller(item)
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
					newHeight = browseHeight - 275;
				}
					
				item.style.height = String(newHeight) + "px";
				
				return newHeight;
			}
    </script>

    <%--</TR>--%><%--</TABLE>--%>
    <div>
    </div>
    <%--</TR></TABLE></TR></TABLE></FORM>--%>
</body>
</html>
