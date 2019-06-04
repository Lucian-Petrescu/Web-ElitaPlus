<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RiskTypeForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.RiskTypeForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>RiskTypeForm</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../Styles.css" type="text/css" rel="STYLESHEET" />

    <script type="text/javascript" language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

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
                            &nbsp;<asp:Label ID="Label7" runat="server" Font-Bold="False">Tables</asp:Label>:
                            <asp:Label ID="Label3" runat="server" ForeColor="#12135B">RISK_TYPE</asp:Label>
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
            <td valign="top" align="center" height="100%">
                <asp:Panel ID="WorkingPanel" runat="server">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; border-bottom: #999999 1px solid" height="98%"
                        cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea"
                        border="0">
                        <tr>
                            <td height="1">
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <uc1:ErrorController ID="RiskTypeErrorController" runat="server"></uc1:ErrorController>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td valign="top" colspan="2">
                                            <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                border-left: #999999 1px solid; width: 98%; border-bottom: #999999 1px solid;
                                                height: 76px" cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center"
                                                bgcolor="#f1f1f1" border="0">
                                                <tr>
                                                    <td>
                                                        <table cellspacing="2" cellpadding="2" width="100%" border="0">
                                                            <tr>
                                                                <td style="width: 200px" align="left">
                                                                    <asp:Label ID="DescriptionLabel" runat="server">Risk_Type</asp:Label>:
                                                                </td>
                                                                <td style="width: 200px" align="left">
                                                                    <asp:Label ID="RiskTypeEnglishLabel" runat="server">Risk_Type_English</asp:Label>:
                                                                </td>
                                                                <td style="width: 200px" align="left">
                                                                    <asp:Label ID="RiskGroupLabel" runat="server">Risk_Group</asp:Label>:
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 200px" nowrap align="left">
                                                                    <asp:TextBox ID="RiskTypeTextBox" runat="server" Width="98%" AutoPostBack="False"
                                                                        CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 200px" nowrap align="left">
                                                                    <asp:TextBox ID="RiskTypeEnglishTextBox" runat="server" Width="98%" AutoPostBack="False"
                                                                        CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 200px" nowrap align="left">
                                                                    <asp:DropDownList ID="RiskGroupList" runat="server" Width="98%" AutoPostBack="False">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    &nbsp;
                                                                </td>
                                                                <td align="right" colspan="2">
                                                                    &nbsp;&nbsp;
                                                                    <asp:Button ID="ClearButton" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                        Width="90px" Text="Clear" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;&nbsp;
                                                                    <asp:Button ID="SearchButton" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                                                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                        Width="90px" Text="Search" Height="20px" CssClass="FLATBUTTON"></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </table>
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
                                    <tr id="trPageSize" runat="server">
                                        <td style="height: 22px" valign="top" align="left">
                                            <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                                            <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
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
                                            </asp:DropDownList>
                                        </td>
                                        <td style="height: 22px" align="right">
                                            <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:GridView ID="RiskGrid" runat="server" Width="100%" OnRowCommand="ItemCommand"
                                                OnRowCreated="ItemCreated" CellPadding="1" AllowSorting="True" AutoGenerateColumns="False"
                                                AllowPaging="True" CssClass="DATAGRID">
                                                <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                                <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                                <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                                                <RowStyle CssClass="ROW"></RowStyle>
                                                <HeaderStyle CssClass="HEADER"></HeaderStyle>
                                                <Columns>
                                                    <asp:ButtonField ButtonType="Image" CommandName="EditRecord" ImageUrl="../Navigation/images/icons/edit2.gif"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                    </asp:ButtonField>
                                                    <asp:ButtonField ButtonType="Image" CommandName="DeleteRecord" ImageUrl="../Navigation/images/icons/trash.gif"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30px">
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                    </asp:ButtonField>
                                                    <asp:TemplateField Visible="False" HeaderText="Risk_Type_Id">
                                                        <ItemTemplate>
                                                            &gt;
                                                            <asp:Label ID="RiskTypeIdLabel" runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Description" HeaderText="Risk_Type">
                                                        <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Visible="True">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox CssClass="FLATTEXTBOX_TAB" ID="DescriptionTextBox" runat="server" Visible="True"></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Risk_Type_English" HeaderText="Risk_Type_English">
                                                        <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="RiskTypeEngLabel" runat="server" Visible="True" >
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox CssClass="FLATTEXTBOX_TAB" ID="RiskTypeEngTextBox" runat="server" Visible="True"></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Product_Tax_Type" HeaderText="Product_Tax_Type">
                                                        <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                                                        <HeaderStyle HorizontalAlign="Center" ></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="moProductTaxTypeLabel" runat="server" Visible="True" >
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ProductTaxTypeDropdown" runat="server" Visible="True">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Risk_Group" HeaderText="Risk_Group">
                                                        <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                                                        <HeaderStyle HorizontalAlign="Center" ></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Visible="True" >
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="RiskGroupDropdown" runat="server" Visible="True">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                                <PagerStyle HorizontalAlign="Center" CssClass="PAGER"/>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr style="height: 1px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="NewButton_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="100px" Text="New" Height="20px" CssClass="FLATBUTTON"></asp:Button>
                                <asp:Button ID="SaveButton_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="100px" Text="Save" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
                                <asp:Button ID="CancelButton" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="100px" Text="Cancel" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
