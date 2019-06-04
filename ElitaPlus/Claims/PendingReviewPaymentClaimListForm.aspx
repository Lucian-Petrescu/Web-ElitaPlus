<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PendingReviewPaymentClaimListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PendingReviewPaymentClaimListForm" 
 MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>
<%@ Register TagPrefix="Elita" TagName="FieldSearchCriteriaNumber" Src="~/Common/FieldSearchCriteriaControl.ascx" %>
    
<asp:Content  ContentPlaceHolderID="HeadPlaceHolder" runat="server">
<script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js" >
</script>
</asp:Content>
<asp:Content  ContentPlaceHolderID="MessagePlaceHolder" runat="server"> 
 
</asp:Content>
<asp:Content  ContentPlaceHolderID="SummaryPlaceHolder" runat="server">

           <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
              <tr >
                  <td colspan="3">
                        <table width="100%" border="0">
                           <tr>
                                <td>
                                     <asp:Label ID="labelCountry" runat="server">COUNTRY</asp:Label> <br />
                                     <asp:DropDownList ID="ddlcountry" runat="server" SkinID="SmallDropDown" AutoPostBack="true"></asp:DropDownList>
                                </td>
                                <td >
                                     <asp:Label ID="LabelSearchServiceCenter" runat="server">SERVICE_CENTER</asp:Label><br />
                                      <asp:DropDownList ID="ddlservicecenter" runat="server" SkinID="SmallDropDown"></asp:DropDownList>
                                </td>
                                <td >
                                    <asp:Label ID="LabelSerialNumber" runat="server">SERIAL_NUMBER</asp:Label> <br />
                                       <asp:TextBox ID="TextBoxSearchSerialNumber" runat="server" SkinID="SmallTextBox" ></asp:TextBox>
                                </td>
                                <td>
                                    <Elita:FieldSearchCriteriaNumber runat="server" ID="moClaimCreatedDate" DataType="Date" Text="CLAIM_CREATED_DATE"/>
                                 
                                </td>
                           </tr>
                    
                            <tr>
                                    <td >
                                        <asp:Label ID="lblServicelevel" runat="server">SERVICE_LEVEL</asp:Label>
                                    </td>
                                    <td >
                                        <asp:Label ID="LabelSearchClaimNumber" runat="server">CLAIM_NUMBER</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="labelclaimstatus" runat="server">CLAIM_STATUS</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="labelclaimexttatus" runat="server">CLAIM_EXTENDED_STATUS</asp:Label>
                                    </td>
                            </tr>
                            <tr>
                                    <td>
                                       <asp:DropDownList ID="ddlservicelevel" runat="server" SkinID="SmallDropDown" ></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxSearchClaimNumber" runat="server" SkinID="SmallTextBox" ></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:DropDownList ID="ddlclaimstatus" runat="server" SkinID="SmallDropDown" ></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlclaimextstatus" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                    </td>
                                  
                                   
                           </tr>
                            <tr>
                                    <td>
                                        <asp:Label ID="LabelSearchCustomerCertificate" runat="server">CERTIFICATE_NUMBER</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblrisktype" runat="server">RISK_TYPE</asp:Label>
                                    </td>
                                    <td>
                                    <asp:Label ID="labelCoverageType" runat="server">COVERAGE_TYPE</asp:Label>
                                    </td>
                                   <td>
                                   </td>
                           </tr>
                           <tr>
                                   <td>
                                        <asp:TextBox ID="TextBoxSearchCertificate" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                     <asp:DropDownList ID="ddlrisktype" runat="server" SkinID="SmallDropDown"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlcoveragetype" runat="server" SkinID="SmallDropDown"></asp:DropDownList>
                                    </td>
                                    <td>
                                    </td>
                          </tr>
                           <tr>
                                    <td >
                                        <asp:Label ID="lblSKUclaimed" runat="server">SKU_CLAIMED_PRODUCT</asp:Label>
                                    </td>
                                    <td >
                                        <asp:Label ID="lblreppartsku" runat="server">SKU_REPLACEMENT_PART</asp:Label>
                                    </td>
                                   <td >
                                        <asp:Label ID="lblSKUReplaced" runat="server">SKU_REPLACED_PRODUCT</asp:Label>
                                    </td>
                                     <td >
                                        <asp:Label ID="lblReplacementType" runat="server">REPLACEMENT_TYPE</asp:Label>
                                    </td>
                           </tr>
                           <tr>
                                    <td>
                                       <asp:TextBox ID="txtSKUClaimed" runat="server" SkinID="SmallTextBox" ></asp:TextBox>
                                    </td>
                                   <td>
                                      <asp:TextBox ID="txtskureppart" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                      <asp:TextBox ID="txtSKUReplaced" runat="server" SkinID="SmallTextBox" ></asp:TextBox>
                                     </td>
                                     <td>
                                       <asp:DropDownList ID="ddlReplacementType" runat="server" SkinID="SmallDropDown"></asp:DropDownList> 
                                     </td>
                                      <td>
                                    </td>
                         </tr>
                         <tr>
                                    <td>
                                        <asp:Label ID="lblmake" runat = "server">MAKE</asp:Label>
                                    </td>
                                    <td>
                                       <asp:Label ID="lblmodel" runat="server">MODEL</asp:Label>
                                    </td>
                                    
                        </tr>
                        <tr>
                                    <td>
                                       <asp:DropDownList ID="ddlmake" runat="server" SkinID="SmallDropdown"></asp:DropDownList>
                                    </td>
                                    <td>
                                       <asp:TextBox ID="TextBoxmodel" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td colspan="4" align="left">
                                
                                        <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear"></asp:Button>
                                         <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                                   </td>          
                       </tr> 
                      </table>
               </td>
           </tr>
    </table>
  </asp:Content>
           <%-- </td>
        </tr>--%>
       <asp:Content  ContentPlaceHolderID="BodyPlaceHolder" runat="server">
   <%-- <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="Server" />--%>
    <!-- new layout start -->
    <div class="dataContainer" runat="server" id="mogridresults" Visible="false">
        <h2 class="dataGridHeader">
          Search Results For Pending Review payments</h2>
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
        <div style="width: 100%" >
                
                            <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCommand="RowCommand" OnRowCreated="ItemCreated"
                               AllowSorting="True" SkinID="DetailPageGridView" AutoGenerateColumns="False" AllowPaging="True">
                                <SelectedRowStyle Wrap="True" />
                               <EditRowStyle Wrap="True" />
                               <AlternatingRowStyle Wrap="True" />
                               <RowStyle Wrap="True" />
                               <HeaderStyle />
                                <Columns>
                                     <asp:TemplateField ShowHeader="false">
                                        <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                        <ItemTemplate>
                                          <asp:CheckBox ID ="chkbxtoappclaim" Enabled ="false" checked="false" runat="server"/>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAllClaims" runat="server"></asp:CheckBox>
                                       </HeaderTemplate>
                                     </asp:TemplateField>
                                      <asp:TemplateField HeaderText="SERVICE_CENTER_NAME" SortExpression="1">
                                        <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="COUNTRY">
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CLAIM_NUMBER" SortExpression="2">
                                        <HeaderStyle HorizontalAlign="Center" Width="7%"></HeaderStyle>
                                        <ItemTemplate>
                                          <asp:LinkButton ID="btnclaimedit" runat="server" CommandName="Select" CommandArgument="<%#Container.displayIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SERIAL_NUMBER">
                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="CLAIM_STATUS">
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                    </asp:TemplateField>
                                      <asp:TemplateField  HeaderText="CLAIM_EXTENDED_STATUS">
                                        <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="MAKE">
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="MODEL">
                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="SKU_CLAIMED_PRODUCT">
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="SKU_REPLACED_PRODUCT">
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="COVERAGE_TYPE">
                                        <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                                    </asp:TemplateField>
                                  <%--  <asp:TemplateField HeaderText="CUSTOMER_NAME">
                                        <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                    </asp:TemplateField>--%>
                                   <asp:TemplateField HeaderText="CLAIM_CREATED_DATE">
                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="REPAIR_REPLACEMENT_DATE">
                                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                    </asp:TemplateField>
                                <%--    <asp:TemplateField ShowHeader="false">
                                        <ItemStyle HorizontalAlign="Center"  Height="15px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnView" runat="server" CausesValidation="False" CommandName="Select"
                                                ImageUrl="~/App_Themes/Default/Images/edit.png" CommandArgument="<%# Container.DisplayIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField Visible="False" HeaderText="Claim_Id"></asp:TemplateField>
                                </Columns>
                                 <PagerSettings  Visible="true" PageButtonCount="10" Mode="Numeric" Position="TopAndBottom"  />
                                 <PagerStyle HorizontalAlign="Center"    />
                            </asp:GridView>
                      
                     </div>
                <div class="btnZone" id="divbtns" runat="server">
                  <asp:Button ID="btnapproveclaims" runat="server" SkinID="AlternateLeftButton" Text="APPROVE_CLAIMS" Visible="False"/>
                  <asp:Button ID="btnexport" runat="server" SkinID="AlternateLeftButton" Text="EXPORT" />
                </div>
           </div>
    
   <script type="text/javascript">
              function SelectAll(id)
        {
            //get reference of GridView control
            var grid = document.getElementById("<%= Grid.ClientID %>");
            //variable to contain the cell of the grid
            var cell;
            
            if (grid.rows.length > 0)
            {
                //loop starts from 1. rows[0] points to the header.
                for (i=1; i<grid.rows.length; i++)
                {
                    //get the reference of first column
                    cell = grid.rows[i].cells[0];
                    
                    //loop according to the number of childNodes in the cell
                   // for (j=0; j<cell.childNodes.length; j++)
                   // {           
                        //if childNode type is CheckBox
                        if (cell.childNodes[0].type =="checkbox")
                        {
                            //assign the status of the Select All checkbox to the cell checkbox within the grid
                           cell.childNodes[0].checked = document.getElementById(id).checked;
                        }
                   // }
                }
            }
        }
       </script>
             <input type="hidden" id="checkRecords" value="" runat="server" />
                <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"  runat="server" designtimedragdrop="261" />
</asp:Content>
