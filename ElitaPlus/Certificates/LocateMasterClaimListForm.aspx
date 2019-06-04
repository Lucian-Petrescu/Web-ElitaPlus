<%@ Page Language="vb" AutoEventWireup="false" Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" 
    CodeBehind="LocateMasterClaimListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.LocateMasterClaimListForm" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer" runat="server" id="moSearchResults">
        <div>
        <table width="100%" class="dataGrid">
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
                <td style="height: 22px; text-align: right">
                    <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        </div>
        <div style="width: 100%">
            <asp:datagrid id="Grid" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnItemCreated="ItemCreated"
													OnItemCommand="ItemCommand" Width="100%" SkinID="DetailPageDataGrid">
				<EditItemStyle Wrap="False"></EditItemStyle>
				<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
				<ItemStyle Wrap="False" BackColor="White" HorizontalAlign="Left"></ItemStyle>
				<HeaderStyle></HeaderStyle>
				<Columns>
					<asp:TemplateColumn>
						<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="2%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
						<ItemTemplate>
						    <asp:ImageButton style="cursor:hand;" id="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
							    runat="server" CommandName="SelectAction"></asp:ImageButton>
						</ItemTemplate>
					</asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="MASTER_CLAIM_NUMBER" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="15%">
                        <ItemTemplate>
                            <asp:Label ID="lblMasterClaimNumber" runat="server" Visible="True" Text='<%# Container.DataItem("master_claim_number")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="CLAIM_#" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="10%">
                        <ItemTemplate>
                            <asp:Label ID="lblClaimNumber" runat="server" Visible="True" Text='<%# Container.DataItem("claim_number")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="DATE_OF_LOSS" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="20%">
                        <ItemTemplate>
                            <asp:Label ID="lblDateOfLoss" runat="server" Visible="True" Text='<%# Container.DataItem("loss_date")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="SERVICE_CENTER" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-Width="25%">
                        <ItemTemplate>
                            <asp:Label ID="lblServiceCenter" runat="server" Visible="True" Text='<%# Container.DataItem("description")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
<%--                    <asp:TemplateColumn HeaderText="" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblClaimId" runat="server" Visible="False" Text='<%# GetGuidStringFromByteArray(Container.DataItem("claim_id"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>--%>
				</Columns>
                <PagerStyle CssClass="PAGER" PageButtonCount="15" Mode="NumericPages"></PagerStyle>
            </asp:datagrid>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button runat="server" ID="btnBack" Text="Back" SkinID="AlternateLeftButton" />
        <asp:Button runat="server" ID="btnNew_WRITE" Text="New" SkinID="AlternateLeftButton" />
    </div>
</asp:Content>