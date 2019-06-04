<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="ClaimHistoryListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimHistoryListForm" %>

 <asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody"    runat="server">
 
 <table><tr><td>d<asp:DataGrid ID="Grid" runat="server" AllowPaging="True"  PagerStyle-HorizontalAlign="Center"
            AutoGenerateColumns="False" BackColor="#DEE3E7" BorderColor="#999999" 
            BorderStyle="Solid" BorderWidth="1px" CellPadding="1" Height="16px" 
            PageSize="5" Width="100%" >
            <SelectedItemStyle BackColor="LightSteelBlue" BorderColor="Orange" 
                BorderStyle="Solid" Wrap="true" />
            <EditItemStyle Wrap="False" />
            <AlternatingItemStyle BackColor="#F1F1F1" />
            <ItemStyle BackColor="White" />
            <HeaderStyle  />
            <Columns>
                <asp:TemplateColumn>
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="5%" />
                    <ItemStyle HorizontalAlign="Center" Width="30px" />
                    <ItemTemplate>
                        <asp:ImageButton ID="btnEdit" runat="server" CommandName="SelectAction" 
                            ImageUrl="../Navigation/images/icons/edit2.gif" style="cursor:hand;" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="CLAIM_NUMBER" HeaderText="Claim_Number" 
                    Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="2%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CREATED_DATE" HeaderText="Created_Date">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CREATED_BY" HeaderText="Created_By">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="STATUS_CODE_OLD" HeaderText="Claim_Status_Old">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="STATUS_CODE_NEW" HeaderText="Claim_Status_New">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AUTHORIZED_AMOUNT_OLD" 
                    HeaderText="Authorized_Amt_Old">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="AUTHORIZED_AMOUNT_NEW" 
                    HeaderText="Authorized_Amt_New">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CLAIM_CLOSED_DATE_OLD" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CLAIM_CLOSED_DATE_NEW" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="REPAIR_DATE_OLD" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="REPAIR_DATE_NEW" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="MODIFIED_DATE" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="MODIFIED_BY" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="67px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="LIABILITY_LIMIT_OLD" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="67px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="LIABILITY_LIMIT_NEW" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="67px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CERT_ITEM_COVERAGE_ID_OLD" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="67px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CERT_ITEM_COVERAGE_ID_NEW" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="67px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CLAIM_MODIFIED_DATE_NEW" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CLAIM_MODIFIED_DATE_OLD" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="136px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CLAIM_MODIFIED_BY_NEW" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="12%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CLAIM_MODIFIED_BY_OLD" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="12%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DEDUCTIBLE_NEW" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="12%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DEDUCTIBLE_OLD" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="12%" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DESCRIPTION_OLD" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="67px" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DESCRIPTION_NEW" Visible="False">
                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="67px" />
                </asp:BoundColumn>
            </Columns>
            <PagerStyle    BackColor="#DEE3E7" ForeColor="DarkSlateBlue" 
                 Mode="NumericPages" PageButtonCount="5" 
                Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                Font-Strikeout="False" Font-Underline="False" VerticalAlign="Bottom" />
        </asp:DataGrid>
     </td></tr></table>
 
 </asp:Content>