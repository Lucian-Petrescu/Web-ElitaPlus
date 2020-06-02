<%@ Page Title="" 
Language="vb" AutoEventWireup="false" 
MasterPageFile="~/Navigation/masters/ElitaBase.Master" 
CodeBehind="AddAPInvoiceForm.aspx.vb" 
EnableSessionState="True"
Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Claims.AddApInvoiceForm" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" type="text/javascript">
        window.latestClick = '';

        function isNotDblClick() {
            if (window.latestClick != "clicked") {
                window.latestClick = "clicked";
                return true;
            } else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
     <div class="dataEditBox">
        <table class="formGrid"  width="100%">
                    <tr>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moInvoiceNumberLabel" Text="INVOICE_NUMBER" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moInvoiceNumber" SkinID="MediumTextBox" />
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moInvoiceDateLabel" Text="INVOICE_DATE" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox ID="moInvoiceDate" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            <asp:ImageButton ID="btnInvoiceDate" runat="server" Style="vertical-align: bottom"
                                ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moServiceCenterLabel" Text="SERVICE_CENTER" />
                        </td>
                        <td nowrap="noWrap">
                             <asp:DropDownList runat="server" ID="moVendorDropDown" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
						 <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moInvoiceAmountLabel" Text="INVOICE_AMOUNT" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moInvoiceAmount" SkinID="SmallTextBox"  />
                        </td>
                        
                    </tr>
                    <tr>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moTermLabel" Text="TERM" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:DropDownList runat="server" ID="moAPInvoiceTerm" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                        <td align="right" nowrap="noWrap">
                            
                        </td>
                        <td nowrap="noWrap">
                           
                        </td>
                    </tr>
        </table>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!-- new layout start -->
<div class="dataContainer">
<h2 class="dataGridHeader">
    Line Items </h2>
    
<div id="tableLineItems">
                <table id="tblLineItems" class="dataGrid" border="0" rules="cols" width="98%">
                    <tr>
                        <td align="Center" colspan="1">
                            <div id="scroller" style="overflow: auto; width: 100%; height: 125px" align="center">
                                <asp:GridView ID="PoGrid" runat="server" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand"
                                    AllowPaging="False" PageSize="50" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
                                    SkinID="DetailPageGridView" Width="1334px">
                                    <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                                    <EditRowStyle Wrap="False"></EditRowStyle>
                                    <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                                    <RowStyle Wrap="False"></RowStyle>
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="moInvoiceLineId" Text='<%# GetGuidStringFromByteArray(Container.DataItem("po_invoice_lines_id"))%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="line_number">
                                            <ItemStyle HorizontalAlign="center" Width="5%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moLineNumber" Text='<%# Container.DataItem("Line_Number")%>' runat="server"> </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moLineNumberText" runat="server" Visible="True" Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
										 <asp:TemplateField Visible ="true" HeaderText="line_type">
                                            <ItemStyle HorizontalAlign="Center" Width="10%"> </ItemStyle>
                                            <ItemTemplate><asp:Label ID="moLineType" Text='<%#Container.DataItem("Line_Type")%>' runat="server"> </asp:Label>
                                             </ItemTemplate>
                                            <EditItemTemplate><asp:DropDownList ID="ddlLineType" runat="server"  Visible="True" Width="100%"></asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
								        <asp:TemplateField Visible="True" HeaderText="item_code">
                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moItemCode" Text='<%# Container.DataItem("Vendor_Item_Code")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moVendorItemCodeText" runat="server" Visible="True" Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
										 <asp:TemplateField Visible="True" HeaderText="description">
                                            <ItemStyle HorizontalAlign="center" Width="20%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moItemDescriptionLabel" Text='<%# Container.DataItem("Vendor_Item_Description")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moVendorItemDescriptionText" runat="server" Visible="True" Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
										<asp:TemplateField Visible="True" HeaderText="QUANTITY">
                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moQuantityLabel" Text='<%# Container.DataItem("quanitity")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moQuanitityText" runat="server" Visible="True" Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
										<asp:TemplateField Visible="True" HeaderText="UNIT_PRICE">
                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moUnitPriceLabel" Text='<%# Container.DataItem("unit_price")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moUnitPriceText" runat="server" Visible="True" Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
										<asp:TemplateField Visible="True" HeaderText="total_price">
                                            <ItemStyle HorizontalAlign="center" Width="10%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moTotalPriceLabel" Text='<%# Container.DataItem("total_price")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moTotalPriceText" runat="server" Visible="True" Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
										 <asp:TemplateField Visible ="true" HeaderText="unit_measurement">
                                            <ItemStyle HorizontalAlign="Center" Width="10%"> </ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moUnitOfMeasurement" Text='<%#Container.DataItem("uom_xcd")%>' runat="server"> </asp:Label>
                                             </ItemTemplate>
                                            <EditItemTemplate><asp:DropDownList ID="ddlUnitOfMeasurement" runat="server"  Visible="True" Width="100%"></asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
										<asp:TemplateField Visible="True" HeaderText="PO_Number">
                                            <ItemStyle HorizontalAlign="center"  Width="9%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moPoNumber" Text='<%# Container.DataItem("po_number")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moPoNumberText" runat="server" Visible="True" Width="100%"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="~/Navigation/images/icons/edit2.gif"
                                                    CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <asp:TemplateField ShowHeader="false">
                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="~/Navigation/images/icons/trash.gif"
                                                    runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="1" width="96%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="BtnNewLine" runat="server" Visible="false" SkinID="AlternateLeftButton" Text="New"></asp:Button>&nbsp;
                            <asp:Button ID="BtnSaveLines" runat="server"  Visible="false" SkinID="PrimaryRightButton" Text="Save"></asp:Button>&nbsp;
                            <asp:Button ID="BtnCancelLine" runat="server"  Visible="false" SkinID="AlternateLeftButton" Text="Cancel"></asp:Button>
                           
                        </td>
                    </tr>
		</table>
</div>
<br />
<div class="btnZone" width="70%">
    <table width="100%">
        <tr>
            <td width="60%">
                <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="BACK"></asp:Button>
            </td>
            <td width="30%" align="right">
                <asp:Button ID="btnApply_WRITE" runat="server" SkinID="PrimaryLeftButton" Text="SAVE" />
            </td>
            <td width="10%"></td>
        </tr>
    </table>
</div>
  
</asp:Content>