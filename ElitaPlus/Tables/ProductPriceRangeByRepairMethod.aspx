<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProductPriceRangeByRepairMethod.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ProductPriceRangeByRepairMethod" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
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

</head>
<body onresize="resizeScroller(document.getElementById('scroller'));" leftmargin="0"
    topmargin="0" onload="changeScrollbarColor();" border = "0" ms_positioning="GridLayout">
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
                                    <asp:Label ID="LabelTables" runat="server"  Cssclass="TITLELABEL">Tables</asp:Label>:
                                    <asp:Label ID="Label40" runat="server" Cssclass="TITLELABELTEXT">Method_Of_Repair_By_Price</asp:Label></p>
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
                <td valign="top" align="center">
                    <asp:Panel ID="WorkingPanel" runat="server" Height="98%" Width="100%">
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                            border-left: #999999 1px solid; border-bottom: #999999 1px solid; height: 95%"
                            cellspacing="0" cellpadding="6" rules="cols" width="95%" align="center" bgcolor="#fef9ea"
                            border="0">
                            <tr>
                                <td valign="middle" align="center" colspan="4">
                                    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:Panel ID="EditPanel" runat="server" Width="100%">
                                        <table id="Table1" style="width: 100%" cellspacing="1" cellpadding="0" width="710"
                                            border="0">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="PanelMasterFields_Write" runat="server" Width="100%" HorizontalAlign="Center" >
                                                        <table cellspacing="0" cellpadding="0" width="80%" border="0">
                                                            <tr id="TRPrdCode" runat="server" align="center"  >
                                                              <td align="right">
                                                                    <asp:Label ID="moProductCodeLabel" runat="server">Product_Code</asp:Label>:</td>
                                                                <td align="left">
                                                                    &nbsp;<asp:TextBox ID="moProductCodeText" TabIndex="1" CssClass="FLATTEXTBOX" Enabled="false" runat="server" Width="110px"></asp:TextBox>&nbsp;
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="moDescriptionLabel" runat="server">Description</asp:Label>:</td>
                                                                <td align="left">
                                                                    &nbsp;<asp:TextBox ID="moDescriptionText" TabIndex="1" Enabled="false" CssClass="FLATTEXTBOX" runat="server" Width="250px"></asp:TextBox>
                                                                    &nbsp;&nbsp;</td>

                                                            </tr>
                                                            <tr>
                                                            <td colspan="4"> &nbsp;
                                                            </td></tr>
                                                            <tr>
                                                               <td align="right">
                                                                    <asp:Label ID="moRiskGroupLabel" runat="server">Risk_Group</asp:Label>:</td>
                                                                <td align="left">
                                                                    &nbsp;<asp:DropDownList ID="moRiskGroupDrop" runat="server" Enabled="false" TabIndex="1" Width="180px">
                                                                    </asp:DropDownList>&nbsp;</td>
                                                                <td colspan="2">&nbsp;&nbsp;</td>
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
                                                    <mytab:TabStrip ID="tsHoriz"  runat="server" TargetID="mpHoriz"
                                                        SepDefaultStyle="border-bottom:solid 1px #000000;" TabSelectedStyle="border:solid 1px black;border-bottom:none;background:#d5d6e4;padding-left:7px;padding-right:7px;"
                                                        TabHoverStyle="background:#faecc2" TabDefaultStyle="border:solid 1px black;background:#f1f1f1;padding-top:2px;padding-bottom:2px;padding-left:7px;padding-right:7px;">
                                                        <mytab:Tab Text="PRICE_RANGE_DETAILS" DefaultImageUrl=""></mytab:Tab>
                                                    </mytab:TabStrip>
                                                    <mytab:MultiPage ID="mpHoriz" Style="border-right: #000000 1px solid; padding-right: 5px;
                                                        border-top: #000000 1px solid; padding-left: 5px; background: #d5d6e4; padding-bottom: 5px;
                                                        border-left: #000000 1px solid; padding-top: 5px; border-bottom: #000000 1px solid"
                                                        runat="server" Width="100%">
                                                        <mytab:PAGEVIEW>
                                                            <!-- Tab begin -->
                                                            <div id="scroller" style="overflow: auto; width: 99.53%; height: 150px" align="center">
                                                                <table id="tblDetail" style="width: 100%; height: 100%" cellspacing="2" cellpadding="2"
                                                                    rules="cols" background="" border="0">
                                                                    <tr>
                                                                        <td valign="top" align="left">
                                                                            <asp:DataGrid ID="DataGridDetail" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                                BorderStyle="Solid" BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999"
                                                                                CellPadding="1" AllowPaging="True" AllowSorting="True" OnItemCreated="ItemCreated">
                                                                                <SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
                                                                                <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
                                                                                <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                                                <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                                                <HeaderStyle></HeaderStyle>
                                                                                <Columns>
                                                                                    <asp:TemplateColumn>
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                                                        <ItemTemplate>
                                                                                            <asp:ImageButton ID="EditButton" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"
                                                                                                CommandName="ViewRecord"></asp:ImageButton>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateColumn>
                                                                                    <asp:BoundColumn HeaderText="Low_Price">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:BoundColumn>
                                                                                    <asp:BoundColumn HeaderText="High_Price">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:BoundColumn>
                                                                                    <asp:BoundColumn HeaderText="Method_of_Repair">
                                                                                        <HeaderStyle HorizontalAlign="Center" Width="40%"></HeaderStyle>
                                                                                    </asp:BoundColumn>
                                                                                   <asp:BoundColumn Visible="False"></asp:BoundColumn>
                                                                                </Columns>
                                                                                <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                                                    PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                                                                            </asp:DataGrid>
                                                                        </td>
                                                                    </tr>
                                                                    <tr valign="bottom">
                                                                        <td align="left">
                                                                            <asp:Button ID="btnAddNewChildFromGrid_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                                                cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                                Width="90px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Panel ID="PanelAllEditDetail" runat="server" Width="100%" Height="100%">
                                                                                <table cellspacing="0" cellpadding="0" border="0" width="100%" height="100%">
                                                                                    <tr valign="top">
                                                                                        <td>
                                                                                            <asp:Panel ID="PanelEditDetail_Write" runat="server" Width="100%" Height="100%" >
                                                                                                <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                                                                                    <tr>
                                                                                                        <td colspan="4">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width="10%" nowrap></td> 
                                                                                                        <td width="15%" nowrap>
                                                                                                            <asp:Label ID="LabelMethodOfRepair" runat="server" Font-Bold="false">Method_of_Repair</asp:Label></td>
                                                                                                        <td width="60%">
                                                                                                            <asp:DropDownList ID="DropdownlistMethodOfRepair" TabIndex="10" runat="server" Width="50%">
                                                                                                            </asp:DropDownList>
                                                                                                        </td>
                                                                                                        <td width="15%" colspan="1">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="4">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td width="10%" nowrap></td> 
                                                                                                        <td width="15%" nowrap>
                                                                                                            <asp:Label ID="LabelPriceBandRangeFrom" runat="server" Font-Bold="false">Low_Price</asp:Label></td>
                                                                                                        <td nowrap width="60%">
                                                                                                            <asp:TextBox ID="TextboxPriceBandRangeFrom" TabIndex="10" runat="server" CssClass="FLATTEXTBOX"
                                                                                                                Width="50%"></asp:TextBox></td>
                                                                                                        <td width="15%" colspan="2">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                        <td colspan="4">
                                                                                                            &nbsp;</td>
                                                                                                    </tr>
                                                                                                    <tr>
                                                                                                    <td width="10%" nowrap></td> 
                                                                                                        <td width="15%" nowrap>
                                                                                                            <asp:Label ID="LabelPriceBandRangeTo" runat="server" Font-Bold="false">High_Price</asp:Label></td>
                                                                                                        <td width="60%">
                                                                                                            <asp:TextBox ID="TextboxPriceBandRangeTo" TabIndex="10" runat="server" CssClass="FLATTEXTBOX"
                                                                                                                Width="50%"></asp:TextBox></td>
                                                                                                        <td width="15%" colspan="1">
                                                                                                            &nbsp;</td>       
                                                                                                    </tr>                                                                                                                                                                                                        
                                                                                                    <tr>
                                                                                                        <td colspan="4">
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
                                                                                                        <asp:Button ID="btnBackChild" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                                                                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                                                            Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Back"></asp:Button>&nbsp;
                                                                                                        <asp:Button ID="btnOkChild_Write" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                                                                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                                                            Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;&nbsp;
                                                                                                        <asp:Button ID="btnCancelChild" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                                                                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                                                            Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Undo"></asp:Button>
                                                                                                        <asp:Button ID="btnAddNewChild_Write" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                                                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                                                            Width="90px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp;
                                                                                                        <asp:Button ID="btnAddChildWithCopy_Write" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                                                                            cursor: hand; background-repeat: no-repeat" TabIndex="205" runat="server" Height="20px"
                                                                                                            Width="136px" CssClass="FLATBUTTON" Text="NEW_WITH_COPY" CausesValidation="False">
                                                                                                        </asp:Button>
                                                                                                        <asp:Button ID="btnDeleteChild_Write" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                                                                                            cursor: hand; background-repeat: no-repeat" TabIndex="210" runat="server" Font-Bold="false"
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
                                                            <!-- Tab end -->
                                                        </mytab:PAGEVIEW>
                                                    </mytab:MultiPage></td>
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
                                 </td>
                            </tr>
                        </table>
                        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                            runat="server" designtimedragdrop="261">
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>

    <script>
			resizeScroller(document.getElementById("scroller"));
			
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
					newHeight = browseHeight - 975;
				}
				
				if (newHeight < 270)
				{
					newHeight = 270;
				}
					
				item.style.height = String(newHeight) + "px";
				
				return newHeight;
			}
    </script>    
</body>
</html>
