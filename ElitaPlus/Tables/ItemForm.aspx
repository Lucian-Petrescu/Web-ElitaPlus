<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ItemForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ItemForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>ItemForm</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
    <script language="javascript" type="text/javascript">
        function showExtendOptionalItemCode() {
            var objcboOptionalItem = document.getElementById("<% =moOptionalItemDrop.ClientID %>");
            if (objcboOptionalItem.options[objcboOptionalItem.selectedIndex].value == "<% =GetNoID() %>") {
                document.getElementById("<% =moOptionalItemCodeText.ClientID %>").style.display = "none";
                document.getElementById("<% =moOptionalItemCode.ClientID %>").style.display = "none";
            } else {
                document.getElementById("<% =moOptionalItemCodeText.ClientID %>").style.display = "";
                document.getElementById("<% =moOptionalItemCode.ClientID %>").style.display = "";
            }
        }

        function ShowOptionalItem() {
            var objcboProdCode = document.getElementById("<% =moProductCodeDrop.ClientID %>");
            var objcboBundled = document.getElementById("<% =moProductCodeDropBundledFlag.ClientID %>");

            var val = objcboProdCode.options[objcboProdCode.selectedIndex].text;
            var bundledFlag = '';
            for (var i = 0; i < objcboBundled.options.length; i++) {
                if (objcboBundled.options[i].text == val) {
                    bundledFlag = objcboBundled.options[i].value;
                    break;
                }
            }
            
            if (bundledFlag == 'Y') {
                document.getElementById("<% =moOptionalItemCodeText.ClientID %>").style.display = "none";
                document.getElementById("<% =moOptionalItemCode.ClientID %>").style.display = "none";
                document.getElementById("<% =moOptionalItem.ClientID %>").style.display = "none";
                document.getElementById("<% =moOptionalItemDrop.ClientID %>").style.display = "none";
            }
            else {
                document.getElementById("<% =moOptionalItem.ClientID %>").style.display = "";
                document.getElementById("<% =moOptionalItemDrop.ClientID %>").style.display = "";
                showExtendOptionalItemCode();
            }
        } 
    </script>
</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
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
                            <asp:Label ID="moTitleLabel1" runat="server" CssClass="TITLELABEL">Tables</asp:Label>:
                            <asp:Label ID="moTitleLabel2" runat="server" CssClass="TITLELABELTEXT">Item</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
        margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
        cellspacing="0" cellpadding="4" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td>
                <table id="moOutTable" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                    border-left: #999999 1px solid; border-bottom: #999999 1px solid" height="98%"
                    cellspacing="0" cellpadding="6" width="98%" align="center" bgcolor="#fef9ea"
                    border="0">
                    <tr>
                        <td align="center" colspan="2" height="1">
                            <uc1:ErrorController ID="moErrorController" runat="server"></uc1:ErrorController>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <asp:Panel ID="EditPanel_WRITE" runat="server">
                                <table id="Table1" style="width: 100%" cellspacing="1" cellpadding="0" width="100%"
                                    align="left" border="0">
                                    <tr>
                                        <td colspan="2">
                                            <table cellpadding="0" width="100%" align="center" border="0">
                                                <tr>
                                                    <td align="center" width="20%">
                                                    </td>
                                                    <td align="left">
                                                        <div style="width: 60.3%" align="left">
                                                            <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <hr style="height: 1px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            &nbsp;
                                            <asp:Label ID="moProductCodeLabel" runat="server">Product_Code</asp:Label>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                            <asp:DropDownList ID="moProductCodeDrop" runat="server" Width="116px" onchange="ShowOptionalItem();">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="moItemNumberLabel" runat="server">ITEM_NUMBER:</asp:Label>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                            <asp:TextBox ID="moItemNumberText" runat="server" Enabled="False" ReadOnly="True"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            &nbsp;
                                            <asp:Label ID="moRiskTypeLabel" runat="server">Risk_Type</asp:Label>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                            <asp:DropDownList ID="moRiskTypeDrop" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            &nbsp;
                                            <asp:Label ID="moMaxReplacementCostLabel" runat="server">Max_Replacement_Cost</asp:Label>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                            <asp:TextBox ID="moMaxReplacementCostText" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            &nbsp;
                                            <asp:Label ID="moOptionalItem" runat="server">OPTIONAL_ITEM:</asp:Label>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                            <asp:DropDownList ID="moOptionalItemDrop" runat="server" onchange="showExtendOptionalItemCode();" Width="116px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            &nbsp;
                                            <asp:Label ID="moOptionalItemCode" runat="server">OPTIONAL_ITEM_CODE:</asp:Label>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                            <asp:TextBox ID="moOptionalItemCodeText" runat="server" MaxLength="5" Width="116px"></asp:TextBox>
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="moIsNewItemLabel" runat="server" Visible="False"></asp:Label>
                                        </td>
                                        <td align="left">
                                            &nbsp;
                                            <asp:Label ID="moItemIdLabel" runat="server" Visible="False"></asp:Label>
                                            <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />
                                            <asp:DropDownList ID="moProductCodeDropBundledFlag" runat="server" style="display:none; visibility:hidden"></asp:DropDownList>
                                            <asp:Label ID="moActionLabel" runat="server" Visible="False"></asp:Label>
                                        </td>
                                    </tr>                                    
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" colspan="2">
                            <table id="Table2" cellspacing="1" cellpadding="1" width="100%" align="left" border="0">
                                <tr>
                                    <td colspan="2">
                                        <hr style="width: 100%; height: 1px" size="1">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" runat="server" Width="80px" Height="20px"
                                            CssClass="FLATBUTTON" Text="BACK" CausesValidation="False"></asp:Button>&nbsp;
                                        <asp:Button ID="btnApply_WRITE" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" runat="server" Width="100px" Height="20px"
                                            CssClass="FLATBUTTON" Text="SAVE"></asp:Button>&nbsp;
                                        <asp:Button ID="btnUndo_WRITE" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Height="20px"
                                            CssClass="FLATBUTTON" Text="UNDO" CausesValidation="False"></asp:Button>&nbsp;
                                        <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" runat="server" Width="75px" Height="20px"
                                            CssClass="FLATBUTTON" Text="New" CausesValidation="False"></asp:Button>&nbsp;
                                        <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" runat="server" Width="135px" Height="20px"
                                            CssClass="FLATBUTTON" Text="New_With_Copy" CausesValidation="False"></asp:Button>&nbsp;
                                        <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" runat="server" Width="100px" Height="20px"
                                            CssClass="FLATBUTTON" Text="Delete" CausesValidation="False"></asp:Button>&nbsp;
                                        <script type="text/javascript">ShowOptionalItem();</script>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
