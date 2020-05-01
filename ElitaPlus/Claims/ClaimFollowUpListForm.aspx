<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ClaimFollowUpListForm.aspx.vb" Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Claims.ClaimFollowUpListForm"
    EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script type="text/javascript">
        function preventBackspace(e) {
            var evt = e || window.event;
            if (evt) {
                var keyCode = evt.charCode || evt.keyCode;
                if (keyCode === 8) {
                    if (evt.preventDefault) {
                        evt.preventDefault();
                    } else {
                        evt.returnValue = false;
                    }
                }
            }
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
	 <tr>
         <td>
             <table cellspacing="0" cellpadding="0" width="100%" border="0">
              <tr>
                  <td align="left">
                      <asp:label id="LabelSearchClaimNumber" runat="server">CLAIM_NUMBER</asp:label>:
                  </td>
                  <td align="left">
                      <asp:label id="LabelSearchCustomerName" runat="server">CUSTOMER_NAME</asp:label>:
                  </td>
                  <td align="left">
                     
                      <asp:label id="LabelSearchClaimExtendedStatus" runat="server">CLAIM_EXTENDED_STATUS:</asp:label>
                  </td>
              </tr>
                  <tr>
                  <td align="left">
                      <asp:textbox id="TextBoxSearchClaimNumber" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:textbox>
                  </td>
                  <td align="left">
                      <asp:textbox id="TextBoxSearchCustomerName" runat="server" AutoPostBack="False"  SkinID="MediumTextBox"></asp:textbox>
                  </td>
                  <td align="left">
                      
                      <asp:dropdownlist id="moClaimExtendedStatusDD" runat="server" AutoPostBack="False" SkinID="MediumDropDown"></asp:dropdownlist>
                  </td>
              </tr>
                  <tr>
                  <td align="left">
                      <asp:label id="LabelSearchServiceCenter" runat="server">SERVICE_CENTER</asp:label>:
                  </td>
                  <td align="left">
                      <asp:Label ID="LabelSearchClaimAdjuster" runat="server">CLAIM_ADJUSTER</asp:Label>:
                  </td>
                  <td align="left">
                      
                       <asp:label id="lblOwner" runat="server">OWNER:</asp:label>
                  </td>
              </tr>
                  <tr>
                  <td align="left">
                      <asp:textbox id="moServiceCenterText" runat="server" AutoPostBack="False"  SkinID="MediumTextBox"></asp:textbox>
                  </td>
                  <td align="left">
                      <asp:TextBox ID="TextBoxSearchClaimAdjuster" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                  </td>
                  <td align="left">
                        <asp:dropdownlist id="moOwnerDD" runat="server" AutoPostBack="False" SkinID="MediumDropDown"></asp:dropdownlist>
                  </td>
              </tr>
              <tr>
                  <td align="left">
                       <asp:label id="LabelFollowUpDate" runat="server">NEXT_DATE</asp:label>:
                  </td>
                  <td align="left">
                     <asp:label id="lblClaimStatus" runat="server">CLAIM_STATUS</asp:label>:
                  </td>
                  <td align="left">
                     <asp:label id="LabelSearchClaimTATRange" runat="server">CLAIM_TAT_RANGE:</asp:label>
                  </td>
              </tr>
                  <tr>
                  <td align="left">
                     <asp:textbox id="TextBoxFollowUpDate" runat="server" AutoPostBack="False" SkinID="SmallTextBox" ></asp:textbox>
                                <asp:imagebutton id="BtnFollowUpDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="Baseline"></asp:imagebutton>
                  </td>
                  <td align="left">
                      
                      <asp:dropdownlist id="cboClaimStatus" runat="server" AutoPostBack="False"  SkinID="MediumDropDown"></asp:dropdownlist>
                  </td>
                  <td align="left">
                    <asp:dropdownlist id="moClaimTATRangeDD" runat="server" AutoPostBack="False" SkinID="MediumDropDown"></asp:dropdownlist>
                  </td>
              </tr>
                  <tr>
                  <td align="left">
                      <asp:label id="lblSearchSortBy" runat="server">Sort By</asp:label>:
                  </td>
                  <td align="left">
                      
                      <asp:label id="LabelSearchDealer" runat="server">DEALER</asp:label>:
                  </td>
                  <td>
                      <asp:label id="LabelSearchLastActivity" runat="server">NO_ACTIVITY_TIME_RANGE:</asp:label>
                  </td>
              </tr>
                  <tr>
                  <td align="left">
                      <asp:dropdownlist id="cboSortBy" runat="server" AutoPostBack="False" SkinID="MediumDropDown"></asp:dropdownlist>
                  </td>
                  <td align="left" >
                     
                      <asp:dropdownlist id="moDealerDrop" runat="server" AutoPostBack="False" SkinID="MediumDropDown"></asp:dropdownlist>
                  </td>
                  <td align="left">
                      <asp:dropdownlist id="moLastActivityDD" runat="server" AutoPostBack="False" SkinID="MediumDropDown"></asp:dropdownlist>  
                  </td>
              </tr>
              <tr>
                  <td align="left">
                      <asp:label id="Label" runat="server">NON_OPERATED_CLAIMS</asp:label>:
                  </td>
                  <td align="left">
                  </td>
                  <td>
                  </td>
              </tr>
            
            <tr>
                  <td align="left">
                      <asp:dropdownlist id="cboNonOperatedClaims" runat="server" AutoPostBack="True" SkinID="MediumDropDown"></asp:dropdownlist>
                  </td>
                  <td align="left" >
                  </td>
                  <td align="left">
                  </td>
            </tr>
            <tr>
                  <td align="left">
                      &nbsp;
                  </td>
                  <td align="left" >
                     &nbsp;
                  </td>
                  <td align="right">
                     <label>
                        <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear"></asp:Button>
                        <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                      </label>
                  </td>
              </tr>
                 </table>
             </td>
             
         </tr>
        </table>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
     <!-- new layout start -->
    <div class="dataContainer">
        <h2 class="dataGridHeader">Search results for claim follow up</h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="Label3"
                            runat="server">:</asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="50px"
                            SkinID="SmallDropDown">
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
                    <td class="bor" align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%">
            <asp:DataGrid ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                        SkinID="DetailPageDataGrid" AllowSorting="True" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
                        <SelectedItemStyle Wrap="true" />
                        <EditItemStyle Wrap="true" />
                        <AlternatingItemStyle Wrap="true" />
                        <ItemStyle Wrap="true" />
                        <HeaderStyle />
                        <Columns>
                            <asp:TemplateColumn Visible="false">
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton Style="cursor: hand;" ID="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
                                        runat="server" CommandName="SelectAction"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Claim_Number">
                                <HeaderStyle HorizontalAlign="Center"  Width="5%"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditCode" runat="server" CommandName="SelectAction"  ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
							<asp:BoundColumn  HeaderText="NEXT_DATE">
                                <HeaderStyle HorizontalAlign="Center"  Width="5%" ></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
							<asp:BoundColumn  HeaderText="Claim_TAT" >
                                <HeaderStyle HorizontalAlign="Center"   Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
							<asp:BoundColumn HeaderText="SC_TAT">
                                <HeaderStyle HorizontalAlign="Center"  Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
							<asp:BoundColumn  HeaderText="NO_ACTIVITY">
                                <HeaderStyle HorizontalAlign="Center"  Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
							<asp:BoundColumn  HeaderText="Claim_Status">
                                <HeaderStyle HorizontalAlign="Center"  Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
							<asp:BoundColumn  HeaderText="Last_Claim_Extended_Status">
                                <HeaderStyle HorizontalAlign="Center"  Width="15%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
							<asp:BoundColumn  HeaderText="Owner">
                                <HeaderStyle HorizontalAlign="Center"  Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
							<asp:BoundColumn HeaderText="Service_Center_Name">
                                <HeaderStyle HorizontalAlign="Center"  Width="15%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
							<asp:BoundColumn HeaderText="Claim_Adjuster">
                                <HeaderStyle HorizontalAlign="Center"  Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
							<asp:BoundColumn  HeaderText="Customer_Name">
                                <HeaderStyle HorizontalAlign="Center"  Width="15%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
							<asp:BoundColumn SortExpression="Dealer_Code" HeaderText="Dealer">
                                <HeaderStyle HorizontalAlign="Center"  Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
							<asp:BoundColumn Visible="False"  HeaderText="Denied_Reason">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn Visible="False"  HeaderText="Comment_Type">
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							</asp:BoundColumn>
                            <asp:BoundColumn HeaderText="NUM_OF_REMINDERS_SENT">
                                <HeaderStyle HorizontalAlign="Center"  Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn HeaderText="LAST_REMINDER_SENT_ON">
                                <HeaderStyle HorizontalAlign="Center"  Width="5%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn Visible="False" HeaderText="Claim_Id"></asp:BoundColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Center" PageButtonCount="15" Mode="NumericPages" Position="TopAndBottom" />
            </asp:DataGrid>
        </div>
        </div>
</asp:Content>