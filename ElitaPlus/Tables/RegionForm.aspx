<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RegionForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.RegionForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>RegionForm</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <!--Start Header-->
   <script language="javascript">
       function populateAcctCode(fld1, fld2) {
           if (fld2.value == '') {
               fld2.value = fld1.value;
           }
       }
   </script>
    <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
        border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
        cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td valign="top">
                <table width="100%" border="0">
                    <tr>
                        <td height="20">
                            &nbsp;<asp:Label ID="TablesLabel" runat="server" CssClass="TITLELABEL">Tables:</asp:Label>
                            <asp:Label ID="MaintainRegionLabel" runat="server" CssClass="TITLELABELTEXT">Region</asp:Label>
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
                                <uc1:ErrorController ID="moErrorController" runat="server"></uc1:ErrorController>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="2">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td colspan="2">
                                            <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                border-left: #999999 1px solid; width: 98%; border-bottom: #999999 1px solid;
                                                height: 76px" cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center"
                                                bgcolor="#f1f1f1" border="0">
                                                <tr>
                                                    <td>
                                                        <table cellspacing="2" cellpadding="2" width="100%" border="0">
                                                            <tr>
                                                                <td style="width: 1%" align="left">
                                                                    <asp:Label ID="moCountryLabel" runat="server">Country:</asp:Label>
                                                                </td>
                                                                <td style="width: 50%" align="left">
                                                                    <asp:DropDownList ID="moCountryDrop" runat="server" Width="95%">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="width: 1%" align="right">
                                                                </td>
                                                                <td style="width: 50%" align="left">
                                                                </td>
                                                                <td nowrap align="right">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="width: 1%;white-space:nowrap;" align="right">
                                                                    <asp:Label ID="SearchDescriptionLabel" runat="server">Region</asp:Label>:
                                                                </td>
                                                                <td style="width: 49%" align="left">
                                                                    <asp:TextBox ID="SearchDescriptionTextBox" runat="server" Width="95%" AutoPostBack="False"
                                                                        CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                </td>
                                                                <td style="width: 1%;white-space:nowrap;" align="right">
                                                                    <asp:Label ID="SearchCodeLabel" runat="server">Code:</asp:Label>
                                                                </td>
                                                                <td style="width: 49%" align="left">
                                                                    <asp:TextBox ID="SearchCodeTextbox" runat="server" AutoPostBack="False" CssClass="FLATTEXTBOX"
                                                                        Width="75%"></asp:TextBox>
                                                                </td>
                                                                <td nowrap align="right">
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
                                        <td style="height: 21px" valign="top" align="left">
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
                                        <td style="height: 21px" align="right">
                                            <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                        <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" OnRowCommand="RowCommand" 
                                            AllowPaging="True" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
                                            CssClass="DATAGRID">
                                            <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                            <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                            <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                                            <RowStyle CssClass="ROW"></RowStyle>
                                            <HeaderStyle CssClass="HEADER"></HeaderStyle>
                                            <Columns>
                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="EditRecord"
                                                                ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="DeleteButton_WRITE" ImageUrl="~/Navigation/images/icons/trash.gif"
                                                                runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>">
                                                            </asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="LABELRegionId" runat="server" >
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="COUNTRY">
                                                        <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="CountryLabel" runat="server" >
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="cboCountryInGrid" runat="server" DataValueField="country_id"
                                                                DataTextField="country_name">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="description" HeaderText="REGION">
                                                        <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="DescriptionLabel" runat="server" Visible="True">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBoxGridDescription" runat="server" Visible="True"></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField SortExpression="SHORT_DESC" HeaderText="REGION_CODE">
                                                        <ItemStyle HorizontalAlign="Left" Width="18%"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="ShortDescriptionLabel" runat="server" Visible="True">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBoxShortDesc" runat="server" Visible="True"></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField SortExpression="ACCOUNTING_CODE" HeaderText="ACCOUNTING_CODE">
                                                        <ItemStyle HorizontalAlign="Left" Width="18%"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="AccountingCodeLabel" runat="server" Visible="True">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBoxAcctCode" runat="server" Visible="True"></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField SortExpression="invoicetax_gl_acct" HeaderText="INVOICE_TAX_GL">
                                                        <ItemStyle HorizontalAlign="Left" Width="18%"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="InvoiceTaxGLLabel" runat="server" Visible="True">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBoxInvoiceTaxGL" runat="server" Visible="True"></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField SortExpression="Extended_Code" HeaderText="EXTENDED_CODE">
                                                        <ItemStyle HorizontalAlign="Left" Width="18%"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="ExtendedCodeLabel" runat="server" Visible="True">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBoxExtendedCode" runat="server" Visible="True"></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                                <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                            </asp:GridView>
                                           
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr style="height: 1px">
                            </td>
                        </tr>
                    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261"/>
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
