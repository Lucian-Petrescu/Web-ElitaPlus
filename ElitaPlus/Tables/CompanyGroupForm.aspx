
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CompanyGroupForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.CompanyGroupForm" MasterPageFile ="~/Navigation/masters/ElitaBase.Master" Theme= "Default"%>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
 <script type="text/javascript" language="javascript">

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
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">  
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
<table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
    
            <td colspan="3">
                <table width="100%" border="0">
           
	
						 <tr>
                            <td>
                                <asp:Label ID="lblcompgrpname" runat="server">COMPANY_GROUP_NAME</asp:Label><br />
                                <asp:TextBox ID="SearchDescriptionTextBox" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                        
                            <td>
                                <asp:Label ID="lblcompgroupcode" runat="server">COMPANY_GROUP_CODE</asp:Label><br />
                                <asp:TextBox ID="SearchCodeTextBox" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                         </tr>

                      <tr>
                        <td colspan="4" align="right">
                            <asp:Button ID="ClearButton" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                                </asp:Button>
                            <asp:Button ID="SearchButton" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                         
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
 <asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
   <%-- <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="Server" />--%>
    <!-- new layout start -->
    <div id="SearchResults" runat="server" class="dataContainer" visible="false">
        <h2 class="dataGridHeader">
          Search Results For Company Groups</h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor" runat="server">:</asp:Label> &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true"
                            SkinID="SmallDropDown">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10" >10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="30" Selected="True">30</asp:ListItem>
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
        <div >
             <asp:DataGrid ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                        SkinID="DetailPageDataGrid" AllowSorting="True" 
                         OnItemCreated="ItemCreated" OnItemCommand="ItemCommand" >
                        <SelectedItemStyle Wrap="true" />
                        <EditItemStyle Wrap="true" />
                        <AlternatingItemStyle Wrap="true" />
                        <ItemStyle Wrap="true" />
                        <HeaderStyle />
                        <columns>
                        <asp:TemplateColumn SortExpression="Code" HeaderText="COMPANY_GROUP_CODE" >
                                <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditCode" runat="server" CommandName="SelectAction" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                          
                            <asp:BoundColumn   SortExpression="Description" HeaderText="COMPANY_GROUP_NAME">
                                <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                       
                            <asp:BoundColumn SortExpression="Claim_Numbering" HeaderText="CLAIM_NUMBERING">
                                <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn SortExpression="INVOICE_NUMBERING_DESCRIPTION" HeaderText="INVOICE_NUMBERING">
                                <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>

                            <asp:BoundColumn SortExpression="ftp_site" HeaderText="FTP_SITE">
                                <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                             <asp:BoundColumn Visible="False" DataField="COMPANY_GROUP_ID"></asp:BoundColumn>
                         </columns>
                           
				            <PagerStyle HorizontalAlign="Center" PageButtonCount="15" Mode="NumericPages" Position="TopAndBottom" />
                    </asp:DataGrid>
          
        </div>
       
    </div>
    <div class="btnZone">
        <asp:Button ID="btnNew" skinID="AlternateLeftButton" TabIndex = "5" runat="server" Text="ADD_NEW"></asp:Button>
    </div>
 
</asp:Content>
