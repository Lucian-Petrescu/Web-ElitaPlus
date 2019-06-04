<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BranchListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.BranchListForm"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_New.ascx" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js">        function TABLE1_onclick() {

        }

    </script>
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid">
        <tr>
            <td align="left" style="height: 40px" width="30%" nowrap="nowrap">
                <table width="1%">
                    <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server" />
                </table>
            </td>
         </tr>
        <tr>
            <td valign="middle" width="30%" style="height: 40px">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblCode" runat="server" Height="25px">BRANCH_CODE:</asp:Label>
                            <asp:TextBox ID="SearchCodeTextBox" runat="server" SkinID="SmallTextBox"> </asp:TextBox>						
                        </td>
						<td valign="middle" align="right" nowrap="nowrap">                        
							<asp:Label ID="Label1" runat="server" Height="25px">BRANCH_NAME:</asp:Label>
                            <asp:TextBox ID="SearchDescriptionTextBox" runat="server" SkinID="SmallTextBox"> </asp:TextBox>
                        </td>
                        <td style="height: 40px" valign="middle" align="right" width="40%" nowrap="nowrap">
                            <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                            </asp:Button>
                            <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="SearchLeftButton">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container">
        <div class="contentZoneHome">
            <div class="dataContainer">
                <h2 class="dataGridHeader">Search results for Dealer Branch</h2>
                <table border="0" align="center" width="100%" class="dataGrid">
                    <tr id="trPageSize" runat="server">
                        <td valign="top" align="left">
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
                        <td align="right">
                            <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div>
                    <asp:DataGrid ID="moDataGrid" runat="server" Width="100%" OnItemCreated="ItemCreated"
                        AutoGenerateColumns="False" SkinID="DetailPageDataGrid" AllowPaging="True" AllowSorting="True"
                        OnItemCommand="ItemCommand">
                        <SelectedItemStyle Wrap="True"></SelectedItemStyle>
                        <EditItemStyle Wrap="True"></EditItemStyle>
                        <AlternatingItemStyle Wrap="True"></AlternatingItemStyle>
                        <ItemStyle Wrap="True"></ItemStyle>
                        <HeaderStyle></HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton Style="cursor: hand;" ID="btnEdit" CommandName="BtnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
                                        runat="server"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="branch_id" Text='<%# GetGuidStringFromByteArray(Container.DataItem("BRANCH_ID"))%>'
                                        runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn DataField="BRANCH_CODE" SortExpression="BRANCH_CODE" HeaderText="BRANCH_CODE">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="BRANCH_NAME" SortExpression="BRANCH_NAME" HeaderText="BRANCH_NAME">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                        </Columns>
                        <PagerStyle PageButtonCount="15" Mode="NumericPages" Position="TopAndBottom"></PagerStyle>
                    </asp:DataGrid>
                </div>
            </div>
        </div>
    </div>

    <table border="0" class="btnZone">
        <tr>
            <td align="left">
                <asp:Button ID="BtnNew_WRITE" runat="server" Text="New" SkinID="PrimaryLeftButton">
                </asp:Button>&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>