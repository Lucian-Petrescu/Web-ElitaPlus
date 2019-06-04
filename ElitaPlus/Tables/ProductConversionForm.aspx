<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProductConversionForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ProductConversionForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<%@ Register Src="../Common/MultipleColumnDDLabelControl_new.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js">
    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid">
        <tr>
            <td align="left">
                <table width="100%">
                    <uc1:MultipleColumnDDLabelControl ID="DealerMultipleDrop" runat="server" />
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td align="left">
                            <asp:Label ID="lblAssurantProdCode" runat="server">Assurant_Product_Code</asp:Label>
                        </td>
                        <td align="left">
                            <asp:DropDownList ID="dpAssurantProdCode" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                        </td>       
                        <td align="left">
                            <asp:Label ID="lblExternalProdCode" runat="server">Dealer_Product_Code</asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtExternalProductCode" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>                        
                        <td>
                            <br />
                            <asp:Button ID="moBtnClear" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                            </asp:Button>
                            <asp:Button ID="moBtnSearch" runat="server" Text="Search" SkinID="SearchLeftButton">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>              
<asp:Content ID="Content5" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!-- new layout start -->
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_PRODUCTS</asp:Label>
        </h2>
        <div>
            <table width="100%" class="dataGrid">
                 <tr id="trPageSize" runat="server">
                    <td valign="top" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px"  SkinID="SmallDropDown" AutoPostBack="true">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
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
                    <td class="bor" align="right">
                         <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
          <div style="width: 100%">
            <asp:GridView id="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" OnRowCommand="RowCommand"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView" PageSize="15">
                <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                <EditRowStyle Wrap="True"></EditRowStyle>
                <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                <RowStyle Wrap="True"></RowStyle>
                <Columns>                  
                    <asp:TemplateField SortExpression="DEALER_NAME" HeaderText="DEALER_NAME">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="SelectAction" Style="cursor: hand" runat="server" Visible="true"
                                CommandName="SelectAction"  CommandArgument='<%# Container.DisplayIndex%>'>
                            </asp:LinkButton>							
						</ItemTemplate>						
                    </asp:TemplateField>                    
                    <asp:TemplateField SortExpression="EXTERNAL_PROD_CODE" HeaderText="DEALER_PRODUCT_CODE">
                        <HeaderStyle></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label ID="moExternalProductCodeLabelGrid" runat="server" Visible="True">
                            </asp:Label>
                        </ItemTemplate>                       
                    </asp:TemplateField>
                       <asp:TemplateField SortExpression="PRODUCT_CODE" HeaderText="ASSURANT_PRODUCT_CODE">
                        <HeaderStyle></HeaderStyle>
                           <ItemTemplate>
                               <asp:Label ID="moAsssurantProductCodeDropGrid" runat="server" Visible="True">
                               </asp:Label>
                           </ItemTemplate>                          
                       </asp:TemplateField>
                    <asp:TemplateField Visible="False">
						<HeaderStyle></HeaderStyle>
						<ItemTemplate>						
                            <asp:Label id="moProductConversionIdLabel" runat="server">
						    </asp:Label>
						</ItemTemplate>
					</asp:TemplateField>
                </Columns>
               <PagerSettings PageButtonCount="15" Mode="Numeric" Position ="TopAndBottom"></PagerSettings>
               <PagerStyle />
            </asp:GridView>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnNew_WRITE" runat="server" SkinID="PrimaryLeftButton" Text="New"
            CommandName="WRITE"></asp:Button>
    </div>
</asp:Content>

	
												
											