<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BestReplacementForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.BestReplacementForm" %>

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
    <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet"/>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js" > </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js" > </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/Tabs.js" > </script> 
</head>
<body onresize="resizeScroller(document.getElementById('scroller'));" leftmargin="0"
    topmargin="0" onload="changeScrollbarColor();" border="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <!--Start Header-->
    <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
        border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
        cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td valign="top">
                <table width="100%" border="0">
                    <tr>
                        <td height="20">
                            <p>
                                &nbsp;
                                <asp:Label ID="LabelTables" runat="server" CssClass="TITLELABEL">Tables</asp:Label>:
                                <asp:Label ID="Label40" runat="server" CssClass="TITLELABELTEXT">BEST_REPLACEMENT</asp:Label></p>
                        </td>
                        <td align="right" height="20">
                            <strong>*</strong>
                            <asp:Label ID="Label9" runat="server" Font-Bold="false">INDICATES_REQUIRED_FIELDS</asp:Label>
                        </td>
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
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <asp:Panel ID="WorkingPanel" runat="server" Height="98%" Width="100%">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; border-bottom: #999999 1px solid; height: 95%"
                        cellspacing="0" cellpadding="6" rules="cols" width="95%" align="center" bgcolor="#fef9ea"
                        border="0">
                        <tr>
                            <td valign="middle" align="center" colspan="4">
                                <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Panel ID="EditPanel" runat="server" Width="100%">
                                    <table id="Table1" style="width: 100%" cellspacing="1" cellpadding="0" width="710"
                                        border="0">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="PanelMasterFields_Write" runat="server" Width="100%">
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tr>
                                                            <td valign="middle" align="right" width="15%">
                                                                <asp:Label ID="moCodeLabel" runat="server" Font-Bold="false" Width="100%">Code</asp:Label>
                                                            </td>
                                                            <td valign="middle" align="left" width="35%">
                                                                &nbsp;
                                                                <asp:TextBox ID="moCodeText_WRITE" runat="server" Width="134px" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                            </td>
                                                            <td valign="middle" align="right" width="15%">
                                                                <asp:Label ID="moDescriptionLabel" runat="server" Font-Bold="false" Width="100%">Description</asp:Label>
                                                            </td>
                                                            <td valign="middle" align="left" width="35%">
                                                                &nbsp;
                                                                <asp:TextBox ID="moDescriptionText_WRITE" runat="server" Width="189px" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle" align="center">
                                                <hr>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle" align="left" colspan="4">
                                                    <div id="tabs" class="style-tabs">
                                                        <ul>
                                                            <li><a href="#tabsBestReplacement">
                                                                <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">BEST_REPLACEMENT</asp:Label></a></li>
                                                        </ul>

                                                        <div id="tabsBestReplacement">
                                                            <div id="scroller" style="overflow: auto; width: 99.53%; height: 150px" align="center">
                                                                <table id="tblDetail" style="width: 100%; height: 100%" cellspacing="2" cellpadding="2"
                                                                    rules="cols" background="" border="0">
                                                                    <tr>
                                                                        <td valign="top" align="left">
                                                                            <asp:DataGrid ID="moEquipmentDataGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                BorderStyle="Solid" BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999"
                                                                                CellPadding="1" AllowPaging="True" AllowSorting="True">
                                                                                <SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
                                                                                <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
                                                                                <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                                                <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                                                <HeaderStyle></HeaderStyle>
                                                                                <Columns>
                                                                                    <asp:BoundColumn Visible="False"></asp:BoundColumn>
                                                                                    <asp:TemplateColumn>
                                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="EditButton" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"
                                                                                                CommandName="ViewRecord"></asp:ImageButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:BoundColumn HeaderText="MANUFACTURER">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="22%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:BoundColumn>
                                                                                    <asp:BoundColumn HeaderText="MODEL">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="22%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:BoundColumn>
                                                                                    <asp:BoundColumn HeaderText="REPLACEMENT_MANUFACTURER">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="22%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:BoundColumn>
                                                                                    <asp:BoundColumn HeaderText="REPLACEMENT_MODEL">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="22%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:BoundColumn>
                                                                                    <asp:BoundColumn HeaderText="PRIORITY">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:BoundColumn>
                                                                                </Columns>
                                                                                <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                                                    PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                    <tr valign="bottom">
                                                                        <td align="left">
                                                                            <asp:Button ID="btnAddNewChildFromGrid_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                                runat="server" Font-Bold="false"
                                                                                Width="90px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Panel ID="PanelAllEditDetail" runat="server" Width="100%" Height="100%">
                                                                                <table cellspacing="0" cellpadding="0" border="0" width="100%" height="100%">
                                                                                    <tr valign="top">
                                                                                        <td>
                                                                                            <asp:Panel ID="PanelEditDetail_Write" runat="server" Width="100%" Height="100%">
                                                                                                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td width="20%" nowrap style="text-align: right">
                                                                                                            <asp:Label ID="moMakeLabel" runat="server" Font-Bold="false">MANUFACTURER</asp:Label>:
                                                                                                        </td>
                                                                                                        <td width="30%">
                                                                                                            <asp:DropDownList ID="moMakeDropdown" runat="server" AutoPostBack="true" Width="70%">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td width="20%" nowrap style="text-align: right">
                                                                                                            <asp:Label ID="moModelLabel" runat="server" Font-Bold="false">MODEL</asp:Label>:
                                                                                                        </td>
                                                                                                        <td width="30%">
                                                                                                            <asp:DropDownList ID="moModelDropdown" runat="server" Width="80%">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="4">&nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td nowrap style="text-align: right">
                                                                                                            <asp:Label ID="moReplacementMakeLabel" runat="server" Font-Bold="false">REPLACEMENT_MANUFACTURER</asp:Label>:
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="moReplacementMakeDropDown" runat="server" AutoPostBack="true" Width="70%">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td nowrap style="text-align: right">
                                                                                                            <asp:Label ID="moReplacementModelLabel" runat="server" Font-Bold="false">REPLACEMENT_MODEL</asp:Label>:
                                                                                                        </td>
                                                                                                        <td>
                                                                                                            <asp:DropDownList ID="moReplacementModelDropDown" runat="server" Width="80%">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="4">&nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width="20%" nowrap style="text-align: right">
                                                                                                            <asp:Label ID="moPriorityLabel" runat="server" Font-Bold="false">PRIORITY</asp:Label>:
                                                                                                        </td>
                                                                                                        <td width="30%">
                                                                                                            <asp:TextBox runat="server" ID="moPriorityText"></asp:TextBox>
                                                                                                        </td>
                                                                                                        <td colspan="2">&nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="4">&nbsp;
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </asp:Panel>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr valign="bottom">
                                                                                        <td>
                                                                                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <asp:Button ID="btnBackChild" Style="background-image: url(../Navigation/images/icons/back_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                                                            TabIndex="140" runat="server" Font-Bold="false"
                                                                                                            Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Back"></asp:Button>&nbsp;
                                                                                                    <asp:Button ID="btnOkChild_Write" Style="background-image: url(../Navigation/images/icons/save_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                                                        TabIndex="145" runat="server" Font-Bold="false"
                                                                                                        Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;&nbsp;
                                                                                                    <asp:Button ID="btnCancelChild" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                                                        TabIndex="150" runat="server" Font-Bold="false"
                                                                                                        Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Undo"></asp:Button>
                                                                                                        <asp:Button ID="btnAddNewChild_Write" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                                                            TabIndex="155" runat="server" Font-Bold="false"
                                                                                                            Width="90px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp;
                                                                                                    <asp:Button ID="btnAddChildWithCopy_Write" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                                                        TabIndex="160" runat="server" Height="20px"
                                                                                                        Width="136px" CssClass="FLATBUTTON" Text="NEW_WITH_COPY" CausesValidation="False"></asp:Button>
                                                                                                        <asp:Button ID="btnDeleteChild_Write" Style="background-image: url(../Navigation/images/icons/clear_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                                                            TabIndex="165" runat="server" Font-Bold="false"
                                                                                                            Width="100px" CssClass="FLATBUTTON" Text="Delete" Height="20px"></asp:Button>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </asp:Panel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>

                                                    </div>
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
                                    Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Back"></asp:Button>&nbsp;
                                <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="190" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;
                                <asp:Button ID="btnUndo_Write" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="195" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Undo"></asp:Button>&nbsp;
                                <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="200" runat="server" Font-Bold="false"
                                    Width="81px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp;&nbsp;
                                <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="205" runat="server" Width="136px"
                                    Height="20px" CssClass="FLATBUTTON" Text="NEW_WITH_COPY" CausesValidation="False">
                                </asp:Button>
                                <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="210" runat="server" Font-Bold="false"
                                    Width="100px" CssClass="FLATBUTTON" Height="20px" Text="Delete"></asp:Button>
                            </td>
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
        resizeScroller(document.getElementById("scroller"));

        function resizeScroller(item) {
            var browseWidth, browseHeight;

            if (document.layers) {
                browseWidth = window.outerWidth;
                browseHeight = window.outerHeight;
            }
            if (document.all) {
                browseWidth = document.body.clientWidth;
                browseHeight = document.body.clientHeight;
            }

            if (screen.width == "800" && screen.height == "600") {
                newHeight = browseHeight - 220;
            }
            else {
                newHeight = browseHeight - 975;
            }

            if (newHeight < 270) {
                newHeight = 270;
            }

            item.style.height = String(newHeight) + "px";

            return newHeight;
        }
    </script>
</body>
</html>
