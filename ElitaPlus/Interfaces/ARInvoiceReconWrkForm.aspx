<%@ Page ValidateRequest="false" Language="vb" AutoEventWireup="false" CodeBehind="ARInvoiceReconWrkForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.ARInvoiceReconWrkForm" 
    Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js" />
    <script language="JavaScript" type="text/javascript" src="../Navigation/scripts/GlobalHeader.js">
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
                          AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                           PageSize="30"
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
                                      <asp:Label ID="moDealerReconWrkIdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("invoice_interface_id"))%>'>
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
							
                              <asp:TemplateField SortExpression="INVOICE_NUMBER" HeaderText="INVOICE_NUMBER">
                                  <HeaderStyle  Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                                  <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moCancelCodeTextGrid" runat="server" Visible="True" Width="130px"></asp:TextBox>
                                      <%--  <asp:DropDownList ID="ddl1" runat="server" Width="250px" DataSource="<%# TempDataView %>"
                                            DataValueField="CODE" DataTextField="DESCRIPTION">
                                        </asp:DropDownList>--%>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="CERTIFICATE" HeaderText="CERTIFICATE">
                                  <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"  Width="20%"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moCertificateTextGrid" runat="server" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="INVOICE_PERIOD_START_DATE" HeaderText="INVOICE_PERIOD_START_DATE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>                                    
                                    <ItemTemplate>
                                        <asp:TextBox ID="moDateCompTextGrid" runat="server" Visible="True"></asp:TextBox>
                                        <asp:ImageButton ID="moDateCompImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="INVOICE_PERIOD_END_DATE" HeaderText="INVOICE_PERIOD_END_DATE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moExtWarrSaleDateTextGrid" runat="server" Visible="True"></asp:TextBox>
                                        <asp:ImageButton ID="moExtWarrSaleDateImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="INVOICE_DATE" HeaderText="INVOICE_DATE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moProductCodeTextGrid" runat="server" Visible="True" ReadOnly="false"></asp:TextBox>
                                        <asp:ImageButton ID="moExtWarrSaleDateImageGrid2" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField SortExpression="INVOICE_DUE_DATE" HeaderText="INVOICE_DUE_DATE">
                                    <HeaderStyle  Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moNewProdCodeTextGrid" runat="server" Visible="True"></asp:TextBox>
                                        <asp:ImageButton ID="moExtWarrSaleDateImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                               <%-- <asp:TemplateField SortExpression="PRODUCT_PRICE" HeaderText="BILL_TO_ADDRESS">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moProductPriceTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:TemplateField SortExpression="INVOICE_LOADED" HeaderText="INVOICE_LOADED">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moOriginalRetailPriceTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="CURRENCY_CODE" HeaderText="CURRENCY_CODE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moPricePolTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right"></FooterStyle>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="EXCHANGE_RATE" HeaderText="EXCHANGE_RATE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moSalesTaxTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="LINE_TYPE" HeaderText="LINE_TYPE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moManWarrantyTextGrid" runat="server" align="right" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="ITEM_CODE" HeaderText="ITEM_CODE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moExtWarrantyTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="EARNING_PARTER" HeaderText="EARNING_PARTER">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moBranchCodeTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="SOURCE" HeaderText="SOURCE">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moNewBranchCodeTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="INSTALLMENT_NUMBER" HeaderText="INSTALLMENT_NUMBER">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moManufacturerTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="AMOUNT" HeaderText="AMOUNT">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moSrTextGrid" runat="server"
                                            Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="PARENT_LINE_NUMBER" HeaderText="PARENT_LINE_NUMBER">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moModelTextGrid" runat="server"
                                            Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="DESCRIPTION" HeaderText="DESCRIPTION">
                                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moSerialNumTextGrid" runat="server" Visible="True"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="ENTIRE_RECORD" HeaderText="ENTIRE_RECORD">
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
