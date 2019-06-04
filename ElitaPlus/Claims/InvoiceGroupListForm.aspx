<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InvoiceGroupListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.InvoiceGroupListForm"  MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>


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
                                <asp:Label ID="lblinvgrpnumber" runat="server">GROUP_NUMBER</asp:Label><br />
                                <asp:TextBox ID="txtinvgroupnumber" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                        
                            <td>
                                <asp:Label ID="lblclaimnumber" runat="server">CLAIM_NUMBER</asp:Label><br />
                                <asp:TextBox ID="txtclaimnumber" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                        
                            <td>
                                <asp:Label ID="lblCountry" runat="server">COUNTRY</asp:Label><br />
                            
                                <asp:DropDownList ID="ddlcountry" runat="server" SkinID="SmallDropDown" AutoPostBack="False">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblgrpnumbfromdate" runat="server">GROUP_NO_FROM</asp:Label>
                                <br />
                                <asp:TextBox ID="txtgrpnumbfromdate" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                                 <asp:ImageButton ID="ImageGroupnumfromDate" TabIndex="2" runat="server" Visible="True"
                                        ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                            </td>
                          
                        </tr>
                        <tr>
                          <td>
                                <asp:Label ID="lblMobilenumber" runat="server">MOBILE_NUMBER</asp:Label><br />
                                <asp:TextBox ID="txtMobilenumber" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblDuedate" runat="server">DUE_DATE</asp:Label>
                                <br />
                                <asp:TextBox ID="txtDuedate" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                               <asp:ImageButton ID="ImageButtonDueDate" TabIndex="2" runat="server" Visible="True"
                                        ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                            </td>
                           
                            <td>
                                <asp:Label ID="lblservicecntrname" runat="server">SERVICE_CENTER_NAME</asp:Label><br />
                                <asp:TextBox ID="txtservicecentername" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>

                             <td>
                                <asp:Label ID="lblgrpnumbertodate" runat="server">GROUP_NO_TO</asp:Label><br />
                                <asp:TextBox ID="txtgrpnumbertodate" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                                 <asp:ImageButton ID="ImageGroupnumtoDate" TabIndex="2" runat="server" Visible="True"
                                        ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                            </td>
                        </tr>
                        <tr>
                         <td>
                                <asp:Label ID="lblInvoiceNum" runat="server">INVOICE_NUMBER</asp:Label><br />
                                <asp:TextBox ID="txtInvoiceNum" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                                <td>
                                <asp:Label ID="lblinvStatus" runat="server">INVOICE_STATUS</asp:Label><br />
                                <asp:DropDownList ID="ddlStatus" runat="server" SkinID="SmallDropDown" AutoPostBack="False">
                                    <%--<asp:ListItem Text="" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="Cancelled" Value="C"></asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                            <td>
                                 <asp:Label ID="lblMembershipnumber" runat="server">ACCOUNT_NUMBER</asp:Label><br />
                                <asp:TextBox ID="txtbxmembershipnumb" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblCertificate" runat="server">CERTIFICATE_NUMBER</asp:Label><br />
                                <asp:TextBox ID="txtCertificatenumb" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                        </tr>
         
                    <tr>
                       <td>
                         &nbsp;
                       </td>
                       <td>
                        &nbsp;
                       </td>
                       <td>
                        &nbsp;
                       </td>
                    
                    
                        <td colspan="4" align="left">
                            <label>
                                <asp:Button ID="btnClearSearch" runat="server" TabIndex = "1" SkinID="AlternateLeftButton" Text="Clear">
                                </asp:Button>
                                <asp:Button ID="btnSearch" runat="server" TabIndex = "2" SkinID="SearchButton" Text="Search"></asp:Button>
                            </label>
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
    <div class="dataContainer" id="SearchResults" runat="server" visible="false">
        <h2 class="dataGridHeader">
          Search Results For Invoice Groups</h2>
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
                        <Columns>
                           
                           <asp:TemplateColumn SortExpression="1" HeaderText="Group_Number" >
                                <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditCode" runat="server" CommandName="SelectAction" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                          
                            <asp:BoundColumn   SortExpression="3" HeaderText="SERVICE_CENTER_NAME">
                                <HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                       
                            <asp:BoundColumn SortExpression="2" HeaderText="GROUP_TIME">
                                <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn SortExpression="4" HeaderText="USER">
                                <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                         
                            <asp:BoundColumn Visible="False" HeaderText="INVOICE_GROUP_ID"></asp:BoundColumn>
                          
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" PageButtonCount="15" Mode="NumericPages" Position="TopAndBottom" />
                    </asp:DataGrid>
          
        </div>
       
    </div>
    <div class="btnZone">
        <asp:Button ID="btnNew" skinID="AlternateLeftButton" TabIndex = "5" runat="server" Text="NEW"></asp:Button>
    </div>
 
</asp:Content>
