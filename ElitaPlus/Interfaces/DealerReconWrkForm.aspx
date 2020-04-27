<%@ Page ValidateRequest="false" Language="vb" AutoEventWireup="false" CodeBehind="DealerReconWrkForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.DealerReconWrkForm" 
    Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js" />
 
    <script language="JavaScript" type="text/javascript" src="../Navigation/scripts/GlobalHeader.js" />


    <script type="text/javascript">
        function TABLE1_onclick() {
        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid">
        <tr>
            <td align="right" width="10%">
                <asp:Label ID="moDealerNameLabel" runat="server">DEALER_NAME:</asp:Label>
            </td>            
            <td>
                <asp:TextBox ID="moDealerNameText" runat="server" Visible="True" ReadOnly="True"
                    SkinID="MediumTextBox" Enabled="False"></asp:TextBox>
            </td>            
            <td align="right" width="10%">
                <asp:Label ID="moFileNameLabel" runat="server" Visible="True">FILENAME:</asp:Label>
            </td>            
            <td>
                <asp:TextBox ID="moFileNameText" runat="server" SkinID="MediumTextBox" Visible="True"
                    ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="moRecordTypeLabel" runat="server">RECORD_TYPE:</asp:Label>
            </td>            
            <td>
                <asp:DropDownList ID="moRecordTypeSearchDrop" runat="server" Visible="True" Width="55px" 
                    SkinID="MediumDropDown"></asp:DropDownList>
            </td>            
            <td align="right">
                <asp:Label ID="moRejectReasonLabel" runat="server" Visible="True">REJECT_REASON:</asp:Label>
            </td>            
            <td>
                <asp:TextBox ID="moRejectReasonText" runat="server" 
                    SkinID="MediumTextBox" Visible="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="moRejectCodeLabel" runat="server">REJECT_CODE:</asp:Label>
            </td>            
            <td>
                <asp:TextBox ID="moRejectCodeText" runat="server" Visible="True" Width="50px" 
                    SkinID="MediumTextBox"></asp:TextBox>
            </td>
            <td></td>
            <td>
                    <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                    </asp:Button>            
                    <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>                
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!-- new layout start -->
    <asp:ScriptManager ID="ScriptManager2" runat="server"> </asp:ScriptManager>

     <script type="text/javascript">
         function Test(obj) {

         }

         function setDirty() {
             var inpId = document.getElementById('<%= HiddenIsPageDirty.ClientID %>')
             inpId.value = "YES"
         }

         function setBundlesDirty() {
             var inpId = document.getElementById('<%= HiddenIsBundlesPageDirty.ClientID %>')
             inpId.value = "YES"
         }

         function UpdateDropDownCtr(obj, oField) {
             document.getElementById(oField).value = obj.value
         }

         function UpdateCtr(oDropDown, oField) {
             document.getElementById(oField).value = oDropDown.value
             setDirty()
         }

    </script>



   <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_DEALER</asp:Label>
        </h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="tr1" runat="server">
                    <td align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>                            
                        </asp:DropDownList>
                    </td>
                    <td class="bor" align="right">
                    <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    <input id="HiddenSavePagePromptResponse" type="hidden" runat="server" />                     
                    <input id="HiddenIsPageDirty" type="hidden" runat="server" />
                    <input id="HiddenIfComingFromBundlesScreen" type="hidden" runat="server" />
                    <input id="HiddenIsBundlesPageDirty" type="hidden" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
              <asp:UpdatePanel ID="updatePanel1" runat="server">
                  <ContentTemplate>
                    <div id="div-datagrid" style="overflow: auto; width:100%; height:500px;">
                      <asp:GridView ID="moDataGrid" runat="server"
                          OnRowCreated="ItemCreated"
                          AllowPaging="True" AllowCustomPaging="true" AllowSorting="True" AutoGenerateColumns="False"
                          OnSelectedIndexChanged="BtnViewBundles_Click" PageSize="30"
                          SkinID="DetailPageGridView" >                          
                          <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                          <EditRowStyle Wrap="True"></EditRowStyle>
                          <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                          <RowStyle Wrap="True"></RowStyle>
                          <HeaderStyle Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                          <PagerStyle HorizontalAlign ="left" />
                          <Columns>
                              <asp:TemplateField Visible="False">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Center" Width="30px" BackColor="White">
                                    </ItemStyle>
                                    <ItemTemplate><asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"
                                            CommandName="EditRecord"></asp:ImageButton></ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField Visible="False">
                                  <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:Label ID="moDealerReconWrkIdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("dealer_recon_wrk_id"))%>'>
                                      </asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="RECORD_TYPE" HeaderText="RECORD_TYPE">
                                  <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                  <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:DropDownList ID="moRecordTypeDrop" runat="server" Width="55px" Visible="True">
                                      </asp:DropDownList>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="REJECT_CODE" HeaderText="REJECT_CODE">
                                  <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                  <ItemStyle Wrap="False" HorizontalAlign="Left" Width="40px"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moRejectCode" ReadOnly="true" runat="server" Width="40px" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="REJECT_REASON" HeaderText="REJECT_REASON">
                                  <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                  <ItemStyle Wrap="False" HorizontalAlign="Left" Width="214px"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="RejectReasonTextGrid" ReadOnly="true" runat="server" Width="214px" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
							<asp:TemplateField SortExpression="DEALER" HeaderText="DEALER">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="100px" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moDealerTextGrid" runat="server" Width="100px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
                              <asp:TemplateField SortExpression="CANCELLATION_CODE" HeaderText="CANCELLATION_CODE">
                                  <HeaderStyle  Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                                  <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moCancelCodeTextGrid" runat="server" Visible="True" Width="30px"></asp:TextBox>
                                        <asp:DropDownList ID="ddl1" runat="server" Width="250px" DataSource="<%# TempDataView %>"
                                            DataValueField="CODE" DataTextField="DESCRIPTION">
                                        </asp:DropDownList>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="CERTIFICATE" HeaderText="CERTIFICATE">
                                  <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"  Width="20%"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moCertificateTextGrid" runat="server" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="DATE_COMP" HeaderText="PRODUCT_SALES_DATE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>                                    
                                    <ItemTemplate>
                                        <asp:TextBox ID="moDateCompTextGrid" runat="server" Visible="True"></asp:TextBox>
                                        <asp:ImageButton ID="moDateCompImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="EXTWARR_SALEDATE" HeaderText="WARR_SALES_DATE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moExtWarrSaleDateTextGrid" runat="server" Visible="True"></asp:TextBox>
                                        <asp:ImageButton ID="moExtWarrSaleDateImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="PRODUCT_CODE" HeaderText="PRODUCT_CODE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moProductCodeTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="NEW_PRODUCT_CODE" HeaderText="NEW_PRODUCT_CODE">
                                    <HeaderStyle  Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moNewProdCodeTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="PRODUCT_PRICE" HeaderText="PRODUCT_PRICE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moProductPriceTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="ORIGINAL_RETAIL_PRICE" HeaderText="ORIGINAL_RETAIL_PRICE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moOriginalRetailPriceTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="PRICE_POL" HeaderText="WARRANTY_PRICE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moPricePolTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="SALES_TAX" HeaderText="SALES_TAX">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moSalesTaxTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="MAN_WARRANTY" HeaderText="MAN_WARRANTY">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moManWarrantyTextGrid" runat="server" align="right" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="EXT_WARRANTY" HeaderText="EXT_WARRANTY">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moExtWarrantyTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="BRANCH_CODE" HeaderText="BRANCH_CODE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moBranchCodeTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="NEW_BRANCH_CODE" HeaderText="NEW_BRANCH_CODE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moNewBranchCodeTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="MANUFACTURER" HeaderText="MANUFACTURER">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moManufacturerTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="SR" HeaderText="SALES_REP">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moSrTextGrid" runat="server"
                                            Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="MODEL" HeaderText="MODEL">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moModelTextGrid" runat="server"
                                            Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="SERIAL_NUMBER" HeaderText="SERIAL_NO_LABEL">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moSerialNumTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="IMEI_NUMBER" HeaderText="IMEI_NUMBER">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moIMEINumTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="ITEM_CODE" HeaderText="ITEM_CODE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moItemCodeTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="ITEM" HeaderText="ITEM">
                                    <HeaderStyle  Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moItemTextGrid" runat="server" Width="275px" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="sku_number" HeaderText="SKU">
                                    <HeaderStyle  Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moSkuNumberTextGrid" runat="server" Width="125px" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="SALUTATION" HeaderText="SALUTATION">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moSalutationTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moCustNameTextGrid" runat="server" Width="200px" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="IDENTIFICATION_NUMBER" HeaderText="IDENTIFICATION_NUMBER">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moIdNumTextGrid" runat="server"
                                            Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="LANGUAGE_PREF" HeaderText="LANGUAGE_PREF">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moLangPrefTextGrid" runat="server" Width="200px" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                  
                                <asp:TemplateField SortExpression="EMAIL" HeaderText="EMAIL">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moEmailTextGrid" runat="server"
                                            Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="ADDRESS1" HeaderText="ADDRESS1">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="100%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moAddressTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="ADDRESS2" HeaderText="ADDRESS2">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="100%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moAddress2TextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="ADDRESS3" HeaderText="ADDRESS3">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="100%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moAddress3TextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>        
                                <asp:TemplateField SortExpression="CITY" HeaderText="CITY">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moCityTextGrid" runat="server" 
                                            Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="ZIP" HeaderText="ZIP">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="20%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moZipTextGrid" runat="server"
                                            Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="STATE_PROVINCE" HeaderText="STATE_PROVINCE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moStateProvTextGrid" runat="server"
                                            Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="CUST_COUNTRY" HeaderText="CUST_COUNTRY">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moCustCountryTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="HOME_PHONE" HeaderText="HOME_PHONE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moHomePhoneTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="WORK_PHONE" HeaderText="WORK_PHONE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moWorkPhoneTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="COUNTRY_PURCH" HeaderText="COUNTRY_PURCH">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moCountryPurchTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="ISO_CODE" HeaderText="CURRENCY_OF_PURCHASE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moCurrencyTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="NUMBER_COMP" HeaderText="INVOICE_NUMBER">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moNumberCompTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="TYPE_PAYMENT" HeaderText="PAYMENT_TYPE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moTypePaymentTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="DOCUMENT_TYPE" HeaderText="DOCUMENT_TYPE">
                                    <HeaderStyle  Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moDocumentTypeTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="DOCUMENT_AGENCY" HeaderText="DOCUMENT_AGENCY">
                                    <HeaderStyle  Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moDocumentAgencyTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="DOCUMENT_ISSUE_DATE" HeaderText="DOCUMENT_ISSUE_DATE">
                                    <HeaderStyle  Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moDocumentIssueDateTextGrid" runat="server" Visible="True"></asp:TextBox>
                                        <asp:ImageButton ID="moDocumentIssueDateImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="RG_NUMBER" HeaderText="RG_NUMBER">
                                    <HeaderStyle  Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moRGNumberTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="ID_TYPE" HeaderText="ID_TYPE">
                                    <HeaderStyle  Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moIDTypeTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="BILLING_FREQUENCY" HeaderText="BILLING_FREQUENCY">
                                    <HeaderStyle  Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moBillFreqTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField SortExpression="NUMBER_OF_INSTALLMENTS" HeaderText="NUMBER_OF_INSTALLMENTS">
                                    <HeaderStyle  Wrap="true" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moTextNoOfInstGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="INSTALLMENT_AMOUNT" HeaderText="INSTALLMENT_AMOUNT">
                                    <HeaderStyle  Wrap="true" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moInstAmtTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="BANK_RTN_NUMBER" HeaderText="BANK_RTN_NUMBER">
                                    <HeaderStyle  Wrap="true" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moBnkRtnNoTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="BANK_ACCOUNT_NUMBER" HeaderText="BANK_ACCOUNT_NUMBER">
                                    <HeaderStyle  Wrap="true" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moBnkAcctNoTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="BANK_ACCT_OWNER_NAME" HeaderText="BANK_ACCT_OWNER_NAME">
                                    <HeaderStyle  Wrap="true" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moBnkAcctOwnerNameTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="BANK_BRANCH_NUMBER" HeaderText="BANK_BRANCH_NUMBER">
                                    <HeaderStyle  Wrap="true" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moBnkBranchNumberTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="POST_PRE_PAID" HeaderText="POSTPRE_PAID">
                                    <HeaderStyle  Wrap="true" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moPostPrePaidTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="BILLING_PLAN" HeaderText="BILLING_PLAN">
                                    <HeaderStyle  Wrap="true" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moBillingPlanTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="BILLING_CYCLE" HeaderText="BILLING_CYCLE">
                                    <HeaderStyle  Wrap="true" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moBillingCycleTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="DATE_PAID_FOR" HeaderText="DATE_PAID_FOR">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moDatePaidForTextGrid" runat="server" Visible="True"></asp:TextBox>
                                        <asp:ImageButton ID="moDatePaidForImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="False">
                                    <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="moModifiedDateLabel" runat="server" Text='<%# Container.DataItem("modified_date")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MEMBERSHIP_NUMBER">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moMembershipNumTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SUBSCRIBER_STATUS">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moSubscriberStatusTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SUSPENDED_REASON">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moSuspendedReasonTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField SortExpression="MOBILE_TYPE" HeaderText="MOBILE_TYPE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moMobileTypeTextGrid" runat="server" Width="40px" Visible="True" ></asp:TextBox>
                                        <%--<asp:DropDownList ID="moMobileTypeDrop" runat="server" Width="40px" Visible="True">
                                        </asp:DropDownList>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="FIRST_USE_DATE" HeaderText="FIRST_USE_DATE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moFirstUseDateTextGrid" runat="server" Visible="True"></asp:TextBox>
                                        <asp:ImageButton ID="moFirstUseDateImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="LAST_USE_DATE" HeaderText="LAST_USE_DATE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moLastUseDateTextGrid" runat="server" Visible="True"></asp:TextBox>
                                        <asp:ImageButton ID="moLastUseDateImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SIM_CARD_NUMBER">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moSimCardNumTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="REGION">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moRegionTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                <asp:TemplateField HeaderText="MEMBERSHIP_TYPE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moMembershipTypeTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                                                                                                                                                                                
                                <asp:TemplateField HeaderText="CESS_OFFICE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="15%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moCessOfficeTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                                                                                                                                                                                
                                <asp:TemplateField HeaderText="CESS_SALESREP">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="20%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moCessSalesrepTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                                                                                                                                                                                
                                <asp:TemplateField HeaderText="BUSINESSLINE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="15%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moBusinesslineTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                                                                                                                                                                                
                                <asp:TemplateField HeaderText="SALES_DEPARTMENT">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="20%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moSalesDepartmentTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                                                                                                                                                                                
                                <asp:TemplateField HeaderText="LINKED_CERT_NUMBER">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moLinkedCertNumberTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                                                                                                                                                                                
                                <asp:TemplateField HeaderText="ADDITIONAL_INFO">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moAdditionalInfoTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>    
                                <asp:TemplateField HeaderText="CREDITCARD_LAST_FOUR_DIGIT">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moCreditCardLast4DigitTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                                                                                                                                                                                 
                                <asp:TemplateField HeaderText="Finance_Amount">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moFinanceAmount" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                                                                                                                                                                                 
                                <asp:TemplateField HeaderText="Finance_Frequency">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moFinanceFrequency" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                                                                                                                                                                                 
                                <asp:TemplateField HeaderText="Finance_Number_of_Installments">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moFinanceInstallmentNum" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                                                                                                                                                                                 
                                <asp:TemplateField HeaderText="Finance_Installment_Amount">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moFinanceInstallmentAmount" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FINANCE_DATE_FORMAT">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="35%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moFinanceDateTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DOWN_PAYMENT">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="20%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moDownPaymentTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ADVANCE_PAYMENT">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="25%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moAdvancePaymentTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UPGRADE_TERM">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="20%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moUpgradeTermTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="BILLING_ACCOUNT_NUMBER">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="40%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moBillingAccountNumberTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField HeaderText="GENDER">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="20%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moGenderTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="MARITAL_STATUS">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="20%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="momaritalstatusTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>  
                                <asp:TemplateField HeaderText="NATIONALITY">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="20%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moNationalityTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                                                                                                                                                                                                   
                                <asp:TemplateField HeaderText="DATE_OF_BIRTH">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moDateOfBirthTextGrid" runat="server" Visible="True"></asp:TextBox>
                                        <asp:ImageButton ID="moDateOfBirthImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PLACE_OF_BIRTH">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moPlaceOfBirthTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CUIT_CUIL">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moCUIT_CUILTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PERSON_TYPE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moPersonTypeTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SERVICE_LINE_NUMBER">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moServiceLineNumberTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                    <asp:TemplateField HeaderText="NUM_OF_CONSECUTIVE_PAYMENTS">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moNumOfConsecutivePaymentsTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                              <asp:TemplateField HeaderText="LOAN_CODE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moLoanCodeTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PAYMENT_SHIFT_NUMBER">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moPaymentShiftNumberTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DEALER_CURRENT_PLAN_CODE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moDealerCurrentPlanCodeTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>                               
                                <asp:TemplateField HeaderText="DEALER_SCHEDULED_PLAN_CODE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moDealerScheduledPlanCodeTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                 </asp:TemplateField>    
                                 <asp:TemplateField HeaderText="DEALER_UPDATE_REASON">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moDealerUpdateReasonTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>   
                                <asp:TemplateField HeaderText="UPGRADE_DATE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moUpgradeDateTextGrid" ReadOnly="True" runat="server" Visible="True"></asp:TextBox>
                                        <asp:ImageButton ID="moUpgradeDateImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>   
                                <asp:TemplateField HeaderText="VOUCHER_NUMBER">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moVoucherNumberTextGrid" ReadOnly="True" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                               <asp:TemplateField HeaderText="RMA">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moRMATextGrid" ReadOnly="True" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OUTSTANDING_BALANCE_AMOUNT">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moOutstandingBalanceAmountTextGrid" ReadOnly="True" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="OUTSTANDING_BALANCE_DUE_DATE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moOutstandingBalanceDueDateTextGrid" ReadOnly="True" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="REFUND_AMOUNT" HeaderText="REFUND_AMOUNT">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moRefundAmountTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>          
                                <asp:TemplateField SortExpression="DEVICE_TYPE" HeaderText="DEVICE_TYPE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moDeviceTypeTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="APPLECARE_FEE" HeaderText="APPLECARE_FEE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moAppleCareFeeTextGrid" ReadOnly="True" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                <asp:TemplateField SortExpression="Occupation" HeaderText="Occupation">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moOccupationTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField> 
                                                                                     
                                <asp:CommandField SelectText="Bundle" ShowSelectButton="true" ButtonType="Button"
                                    ControlStyle-CssClass="FLATBUTTON" ShowCancelButton="false" />
                          </Columns>
                          <PagerSettings PageButtonCount="15" Mode="Numeric" Position="TopAndBottom" />
                          <PagerStyle HorizontalAlign="left" CssClass="PAGER_LEFT"></PagerStyle>
                      </asp:GridView>
                    </div>
                  </ContentTemplate>
              </asp:UpdatePanel>              
        </div>
   </div>
   <div class="btnZone">       
        <asp:Button ID="SaveButton_WRITE" runat="server" Text="Save" CausesValidation="False"
             SkinID="AlternateLeftButton"></asp:Button>&nbsp;
        <asp:Button ID="btnUndo_WRITE" runat="server" Text="Undo" CausesValidation="False"
             SkinID="AlternateLeftButton"></asp:Button>&nbsp;
        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="False"
             SkinID="AlternateLeftButton"></asp:Button>&nbsp;
    </div>  
    
    
    <div class = "dataContainer">
     <div id="ModalProgressBar">
        <asp:Button ID="hiddenTargetControlForModalPopup" runat="server" style="display:none" />
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup"  
            TargetControlID="hiddenTargetControlForModalPopup"
            PopupControlID="pnlPopup" 
            BackgroundCssClass="ModalBackground"
            DropShadow="True"
            PopupDragHandleControlID="programmaticPopupDragHandle"
            RepositionMode="RepositionOnWindowScroll" 
            >
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server"  CssClass="modalPopup" style="display:none">
            <asp:Panel runat="Server" ID="programmaticPopupDragHandle">                
            </asp:Panel>
            <div id="light" class="overlay_message_content" style="width:1050px">
            <asp:UpdatePanel ID="updPnlBundles" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView runat="server" ID="gvPop" AutoGenerateColumns="false" SkinID="DetailPageGridView">
                        <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                        <EditRowStyle Wrap="True"></EditRowStyle>
                        <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                        <RowStyle Wrap="True"></RowStyle>
                        <HeaderStyle Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Item_Number">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtItemNumber" Text='<%#Bind("ITEM_NUMBER")%>' ReadOnly="true"
                                        Style="text-align: center; width: 100%" SkinId="SmallTextBox"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Make">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtMake" MaxLength="50" Text='<%#Bind("ITEM_MANUFACTURER")%>'
                                        onchange="javascript:setBundlesDirty();" SkinId="SmallTextBox"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Model">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtModel" MaxLength="30" Text='<%#Bind("ITEM_MODEL")%>'
                                        onchange="javascript:setBundlesDirty();" SkinId="SmallTextBox"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Serial_Number">                                
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="0%" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtSerNum" MaxLength="20" Text='<%#Bind("ITEM_SERIAL_NUMBER")%>'
                                        onchange="javascript:setBundlesDirty();" Style="width: 100%" SkinId="SmallTextBox"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <HeaderStyle />
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtDesc" MaxLength="50" Text='<%#Bind("ITEM_DESCRIPTION")%>'
                                        onchange="javascript:setBundlesDirty();" SkinId="SmallTextBox"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="0%" />
                                <HeaderStyle />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtPrice" EnableViewState="true" Text='<%#Bind("ITEM_PRICE")%>'
                                        MaxLength="11" onchange="javascript:setBundlesDirty();" Style="text-align: center;
                                        width: 60px" SkinId="SmallTextBox"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bundle_Value">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="0%" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtBundlVal" Text='<%#Bind("ITEM_BUNDLE_VAL")%>'
                                        MaxLength="20" onchange="javascript:setBundlesDirty();" Style="width: 60px" SkinId="SmallTextBox"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manufacturer_Warranty">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtManWarr" MaxLength="5" Text='<%#Bind("ITEM_MAN_WARRANTY")%>'
                                        onchange="javascript:setBundlesDirty();" Style="text-align: center; width: 100%" SkinId="SmallTextBox"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                    </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="btnZone">       
                <asp:Button ID="btnClose" runat="server" Text="Back" 
                    SkinID="AlternateLeftButton" OnClick="btnClose_Click"></asp:Button>&nbsp;
                <asp:Button ID="btnApply" runat="server" Text="Apply" CausesValidation="True"
                    SkinID="AlternateLeftButton" OnClick="btnApply_Click"></asp:Button>&nbsp;
            </div>
            </div>
        </asp:Panel>       
     </div>
    </div>  
       
</asp:Content>
