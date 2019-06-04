<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="ClaimStatusByGroupListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimStatusByGroupListForm"
    Title="Untitled Page" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td colspan="2" style="text-align: center">
                <table cellpadding="2" cellspacing="0" border="0" style="border: #999999 1px solid;
                    background-color: #f1f1f1; height: 98%; width: 100%">
                    <tr>
                        <td colspan="4" align="center">
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="rdoDealer" Visible="false" AutoPostBack="true" OnCheckedChanged="onchange_rdoDealer" Width="15%" runat="server" Text="DEALER" Checked="false" GroupName="rdoSearchOnGroup" />
                                    </td>
                                    <td align="left" Width="85%">
                                        <div style="width: 50%" align="left">
                                            <uc1:MultipleColumnDDLabelControl Visible="false" ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                        </div>
                                    </td>
                                </tr>
                                <!--tr>
                                    <td colspan="2" align="center">
                                        <hr style="height: 1px" />
                                    </td>
                                </tr-->                                
                                <tr>
                                    <td colspan="2">
                                        <asp:RadioButton ID="rdoCompanyGroup" AutoPostBack="true" OnCheckedChanged="onchange_rdoCompanyGroup" runat="server" Style="margin-right: 10px;"
                                            Text="COMPANY_GROUP" Checked="true" GroupName="rdoSearchOnGroup" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                <tr>
                                    <td style="white-space: nowrap" align="left" colspan="2">
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Button ID="btnClearSearch" Visible="false" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" runat="server"
                                            Text="Clear"></asp:Button>&nbsp;
                                        <asp:Button ID="btnSearch" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH"
                                            Text="Search"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="height: 5px;">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;<br />
                <hr style="height: 1px" />
            </td>
        </tr>
        <tr id="trPageSize" runat="SERVER" visible="False">
            <td align="left">
                <asp:Label ID="lblPageSize" runat="server">Page_Size:</asp:Label>&nbsp;
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
            <td style="text-align: right">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:DataGrid ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="#DEE3E7"
                    BorderColor="#999999" BorderStyle="Solid" CellPadding="1" BorderWidth="1px" AllowPaging="True"
                    AllowSorting="False" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand"
                    CssClass="DATAGRID">
                    <SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue">
                    </SelectedItemStyle>
                    <EditItemStyle Wrap="False"></EditItemStyle>
                    <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                    <ItemStyle Wrap="False" BackColor="White" HorizontalAlign="Center"></ItemStyle>
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn>
                            <HeaderStyle ForeColor="#12135B"></HeaderStyle>
                            <ItemStyle CssClass="CenteredTD" Width="30px"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton Style="cursor: hand;" ID="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif"
                                    runat="server" CommandName="SelectAction"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="ClaimStatusByGroupID" runat="server" Text='<%# Container.DataItem("id")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="COMPANY_GROUP_NAME">
                            <HeaderStyle CssClass="CenteredTD" />
                            <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label ID="lblCompanyGroupName" runat="server" Text='<%# Container.DataItem("company_group_name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="COMPANY_GROUP_CODE" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle CssClass="CenteredTD" />
                            <HeaderStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label ID="lblCompanyGroupCode" runat="server" Text='<%# Container.DataItem("company_group_code")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15" Mode="NumericPages">
                    </PagerStyle>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="BtnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" Text="New" Height="20px" CssClass="FLATBUTTON"></asp:Button>

    <script type="text/javascript">
        function UpdateList(dest){
            var objS = event.srcElement
            var val = objS.options[objS.selectedIndex].value
            //alert("source: " + val);
            var objD = document.getElementById(dest)
            // alert("Desctination: " + objD.options.length);
            for(i=0; i<objD.options.length; i++){
                if (objD.options[i].value == val){
                    objD.selectedIndex = i;
                    //alert("found i:" + i);
                    break;
                }
            }
        }
    </script>

</asp:Content>
