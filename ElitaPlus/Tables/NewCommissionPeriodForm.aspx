<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="NewCommissionPeriodForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.NewCommissionPeriodForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>TemplateForm</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

</head>
<body onresize="resizeForm(document.getElementById('scroller'));" leftmargin="0"
    topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
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
                                <asp:Label ID="moTitleLabel1" runat="server" Cssclass="TITLELABEL">Tables</asp:Label>:
                                <asp:Label ID="moTitleLabel2" runat="server"  Cssclass="TITLELABELTEXT">Commission_Breakdown</asp:Label></td>
                            <td align="right" height="20">
                                <strong>*</strong>
                                <asp:Label ID="Label9" runat="server" Font-Bold="false">INDICATES_REQUIRED_FIELDS</asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="moTableOuter" style="border-right: black 1px solid; border-top: black 1px solid;
            margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
            cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
            <!--d5d6e4-->
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td valign="top" align="center" height="100%">
                    <table id="moTableMain" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; border-bottom: #999999 1px solid" height="98%"
                        cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea"
                        border="0">
                        <!--ededd5-->
                        <tr>
                            <td height="1">
                                <uc1:ErrorController ID="moErrorController" runat="server"></uc1:ErrorController>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="EditPanel" runat="server">
                                    <table style="width: 100%; height: 98%" cellspacing="0" cellpadding="0" width="100%"
                                        border="0">
                                        <asp:Panel ID="moPeriodPanel_WRITE" runat="server">
                                            <tbody>
                                                <tr>
                                                    <td align="right" colspan="1">
                                                        <asp:Label ID="moDealerLabel" runat="server" Font-Bold="false">Dealer</asp:Label>&nbsp;
                                                    </td>
                                                    <td colspan="1" rowspan="1">
                                                        <asp:DropDownList ID="moDealerDrop_WRITE" TabIndex="40" runat="server" AutoPostBack="True"
                                                            Width="210px">
                                                        </asp:DropDownList></td>
                                                    <td align="right" colspan="1">
                                                    </td>
                                                    <td colspan="1">
                                                        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                                                            runat="server"></td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="1">
                                                        <asp:Label ID="moEffectiveLabel" runat="server" Font-Bold="false">Effective</asp:Label>&nbsp;
                                                    </td>
                                                    <td colspan="1">
                                                        <asp:TextBox ID="moEffectiveText_WRITE" TabIndex="10" runat="server" Width="95px"
                                                            CssClass="FLATTEXTBOX" ReadOnly="True"></asp:TextBox>
                                                        <asp:ImageButton ID="BtnEffectiveDate_WRITE" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                        </asp:ImageButton></td>
                                                    <td align="right" colspan="1">
                                                        <asp:Label ID="moExpirationLabel" runat="server" Font-Bold="false">Expiration</asp:Label>&nbsp;
                                                    </td>
                                                    <td colspan="1">
                                                        <asp:TextBox ID="moExpirationText_WRITE" TabIndex="10" runat="server" Width="115px"
                                                            CssClass="FLATTEXTBOX" ReadOnly="True"></asp:TextBox>
                                                        <asp:ImageButton ID="BtnExpirationDate_WRITE" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                        </asp:ImageButton></td>
                                                </tr>
                                        </asp:Panel>
                                        <tr>
                                            <td colspan="5">
                                                <hr>
                                            </td>
                                        </tr>
                                        <asp:Panel ID="moRestrictDetailPanel" runat="server">
                                            <tr>
                                                <td style="height: 6px" valign="middle" align="right">
                                                    <asp:Label ID="moAllowedMarkupPctDetailLabel" runat="server" Font-Bold="false">ALLOWED_MARKUP_PCT</asp:Label>&nbsp;</td>
                                                <td style="height: 6px">
                                                    <asp:TextBox ID="moAllowedMarkupPctDetailText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox></td>
                                                <td style="height: 6px" valign="middle" align="right">
                                                    <asp:Label ID="moToleranceDetailLabel" runat="server" Font-Bold="false">TOLERANCE</asp:Label>&nbsp;</td>
                                                <td style="height: 6px">
                                                    <asp:TextBox ID="moToleranceDetailText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox></td>
                                            </tr>
                                        </asp:Panel>
                                        <asp:Panel ID="Panel1" runat="server">
                                            <tr>
                                                <td style="height: 6px" valign="middle" align="right">
                                                    <asp:Label ID="moDealerMarkupPctDetailLabel" runat="server" Font-Bold="false">DEALER_MARKUP_PCT</asp:Label>&nbsp;</td>
                                                <td style="height: 6px">
                                                    <asp:TextBox ID="moDealerMarkupPctDetailText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox></td>
                                                <td style="height: 6px" valign="middle" align="right">
                                                    <asp:Label ID="moDealerCommPctDetailLabel" runat="server" Font-Bold="false">DEALER_COMM_PCT</asp:Label>&nbsp;</td>
                                                <td style="height: 6px">
                                                    <asp:TextBox ID="moDealerCommPctDetailText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="height: 2px" valign="middle" align="center" colspan="4">
                                                    <hr style="width: 100%; height: 1px" size="1">
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                        <tr>
                                            <td valign="top" align="left" width="100%" colspan="5">
                                                <table id="tblOpportunities" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                    border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="2"
                                                    cellpadding="2" rules="cols" width="100%" background="" border="0">
                                                    <tr>
                                                        <td colspan="2">
                                                            <uc1:ErrorController ID="moErrorControllerGrid" runat="server"></uc1:ErrorController>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" width="100%" colspan="2">
                                                            <div id="scroller" style="overflow: auto; width: 100%; height: 50px" align="center">
                                                                <asp:Panel ID="moGridPanel" runat="server">
                                                                    <table id="Table1" style="width: 100%; height: 273px" height="273" cellspacing="0"
                                                                        cellpadding="0" border="0">
                                                                        <tr id="trPageSize" runat="server">
                                                                            <td valign="top" align="left">
                                                                                <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                                                                                <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="50px">
                                                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                                                    <asp:ListItem Selected="True" Value="10">10</asp:ListItem>
                                                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                                                    <asp:ListItem Value="25">25</asp:ListItem>
                                                                                    <asp:ListItem Value="30">30</asp:ListItem>
                                                                                    <asp:ListItem Value="35">35</asp:ListItem>
                                                                                    <asp:ListItem Value="40">40</asp:ListItem>
                                                                                    <asp:ListItem Value="45">45</asp:ListItem>
                                                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                                                </asp:DropDownList></td>
                                                                            <td align="right">
                                                                                <asp:Label ID="lblRecordCount" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td colspan="2">
                                                                                <asp:DataGrid ID="Grid" runat="server" Width="100%" 
                                                                                    BorderStyle="Solid" BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1"
                                                                                    AllowSorting="True" BorderWidth="1px" AutoGenerateColumns="False" AllowPaging="True">
                                                                                    <SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
                                                                                    <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
                                                                                    <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                                                    <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                                                    <HeaderStyle></HeaderStyle>
                                                                                    <Columns>
                                                                                        <asp:TemplateColumn>
                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"
                                                                                                    CommandName="EditRecord"></asp:ImageButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateColumn>
                                                                                        <asp:TemplateColumn>
                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/trash.gif"
                                                                                                    runat="server" CommandName="DeleteRecord"></asp:ImageButton>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateColumn>
                                                                                        <asp:BoundColumn Visible="False" HeaderText="Id"></asp:BoundColumn>
                                                                                        <asp:TemplateColumn HeaderText="BROKER_MARKUP_PCT">
                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left" Width="28%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                <asp:TextBox ID="TextBoxGridBkMkUpPct" CssClass="FLATTEXTBOX_TAB" runat="server"></asp:TextBox>
                                                                                            </EditItemTemplate>
                                                                                        </asp:TemplateColumn>
                                                                                        <asp:TemplateColumn HeaderText="BROKER_COMMISSION_PCT">
                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            <ItemStyle HorizontalAlign="Left" Width="28%"></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                <asp:TextBox ID="TextBoxGridBkCommPct" CssClass="FLATTEXTBOX_TAB" runat="server"></asp:TextBox>
                                                                                            </EditItemTemplate>
                                                                                        </asp:TemplateColumn>
                                                                                        <asp:TemplateColumn HeaderText="MARKUP_ENTITY">
                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            <ItemTemplate>
                                                                                                MarkupEntityInGridLabel
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                <asp:DropDownList ID="cboMarkupEntityInGrid" runat="server">
                                                                                                </asp:DropDownList>
                                                                                            </EditItemTemplate>
                                                                                        </asp:TemplateColumn>
                                                                                        <asp:TemplateColumn HeaderText="COMMISSION_ENTITY">
                                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                            <ItemTemplate>
                                                                                                CommissionEntityInGridLabel
                                                                                            </ItemTemplate>
                                                                                            <EditItemTemplate>
                                                                                                <asp:DropDownList ID="cboCommissionEntityInGrid" runat="server">
                                                                                                </asp:DropDownList>
                                                                                            </EditItemTemplate>
                                                                                        </asp:TemplateColumn>
                                                                                    </Columns>
                                                                                    <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                                                        PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                                                                                </asp:DataGrid></td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <asp:Panel ID="moDetailButtonPanel" runat="server">
                                                        <tr>
                                                            <td colspan="2">
                                                                <hr style="height: 1px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="BtnNewGrid_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                    Width="100px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp;
                                                                <asp:Button ID="BtnSaveGrid_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                    Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;
                                                                <asp:Button ID="BtnUndoGrid" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                    Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Undo"></asp:Button></td>
                                                        </tr>
                                                    </asp:Panel>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <hr style="width: 100%; height: 1px" size="1">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:Panel ID="moPeriodButtonPanel" runat="server">
                                    <table id="Table2" cellspacing="1" cellpadding="1" width="300" align="left" border="0">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                                    cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" CssClass="FLATBUTTON"
                                                    Height="20px" Text="BACK"></asp:Button></td>
                                            <td>
                                                <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif);
                                                    cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" CssClass="FLATBUTTON"
                                                    Height="20px" Text="SAVE"></asp:Button></td>
                                            <td>
                                                <asp:Button ID="btnUndo_WRITE" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                                    cursor: hand; background-repeat: no-repeat" runat="server" Width="110px" CssClass="FLATBUTTON"
                                                    Height="20px" Text="UNDO"></asp:Button></td>
                                            <td>
                                                <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                    cursor: hand; background-repeat: no-repeat" runat="server" Width="100px" CssClass="FLATBUTTON"
                                                    Height="20px" Text="New"></asp:Button></td>
                                            <td>
                                                <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                    cursor: hand; background-repeat: no-repeat" runat="server" Width="140px" CssClass="FLATBUTTON"
                                                    Height="20px" Text="New_With_Copy"></asp:Button></td>
                                            <td>
                                                <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                                    cursor: hand; background-repeat: no-repeat" runat="server" Width="100px" CssClass="FLATBUTTON"
                                                    Height="20px" Text="Delete"></asp:Button></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>

    <script>
			
			
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
					newHeight = browseHeight - 350;
				}
				else
				{
					newHeight = browseHeight - 370;
				}
					
				item.style.height = String(newHeight) + "px";
				
				item.style.width = String(browseWidth - 80) + "px";
				
			}	
			
			resizeForm(document.getElementById("scroller"));
    </script>

</body>
</html>
