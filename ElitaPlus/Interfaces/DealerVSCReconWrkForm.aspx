<%@ Page ValidateRequest="false" Language="vb" AutoEventWireup="false" CodeBehind="DealerVSCReconWrkForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.DealerVSCReconWrkForm" 
    Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"/> 
    <script type="text/javascript">
        function TABLE1_onclick() {
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid">
        <tr>
            <td>
                <asp:Label ID="moDealerNameLabel" runat="server" Height="25px">DEALER_NAME:</asp:Label>
                <asp:TextBox ID="moDealerNameText" runat="server" Visible="True" ReadOnly="True"
                    SkinID="MediumTextBox" Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="moDealerGrpNameLabel" runat="server" Height="25px">DEALER_GROUP_NAME:</asp:Label>
                <asp:TextBox ID="moDealerGrpNameText" runat="server" Visible="True" ReadOnly="True"
                    SkinID="MediumTextBox" Enabled="False"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="moFileNameLabel" runat="server" Visible="True">FILENAME:</asp:Label>
                <asp:TextBox ID="moFileNameText" runat="server" SkinID="MediumTextBox" Visible="True"
                    ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!-- new layout start -->
    <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>

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
                        <asp:Label ID="Label1" runat="server">Page_Size</asp:Label>: &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
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
                    <input id="HiddenSavePagePromptResponse" type="hidden" runat="server" />                     
                    <input id="HiddenIsPageDirty" type="hidden" runat="server" />
                    <input id="HiddenIfComingFromBundlesScreen" type="hidden" runat="server" />
                    <input id="HiddenIsBundlesPageDirty" type="hidden" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%;  overflow: auto">
              <asp:UpdatePanel ID="updatePanel1" runat="server">
                  <ContentTemplate>
                      <asp:GridView ID="moDataGrid" runat="server" Width="100%" OnRowCreated="ItemCreated"
                          AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView"
                          OnSelectedIndexChanged="BtnViewBundles_Click" PageSize="30">
                          <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                          <EditRowStyle Wrap="True"></EditRowStyle>
                          <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                          <RowStyle Wrap="True"></RowStyle>
                          <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                          <PagerStyle HorizontalAlign ="Left" />
                          <Columns>
                              <asp:TemplateField Visible="False">
                                  <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:Label ID="moDealerVscReconWrkIdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("dealer_vsc_recon_wrk_id"))%>'>
                                      </asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="RECORD_TYPE" HeaderText="RECORD_TYPE">
                                  <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:DropDownList ID="moRecordTypeDrop" runat="server" Visible="True">
                                      </asp:DropDownList>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="REJECT_CODE" HeaderText="REJECT_CODE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moRejectCodeTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True" Enabled="false"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="REJECT_REASON" HeaderText="REJECT_REASON">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moRejectReasonTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True" Enabled="false"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="COMPANY_CODE" HeaderText="COMPANY_CODE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moCompanyCodeTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True" Enabled="false"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="DEALER_CODE" HeaderText="DEALER_CODE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moDealerCodeTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True" Enabled="true"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="BRANCH_CODE" HeaderText="BRANCH_CODE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moBranchCodeTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="CERTIFICATE_NUMBER" HeaderText="CERTIFICATE_NUMBER">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moCertificateTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="CUSTOMER_NAME" HeaderText="CUSTOMER_NAME">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moCustNameTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="ADDRESS1" HeaderText="ADDRESS1">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moAddressTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="CITY" HeaderText="CITY">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moCityTextGrid" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
                                          Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="POSTAL_CODE" HeaderText="POSTAL_CODE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moPostalCodeTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="REGION" HeaderText="REGION">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moRegionTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="COUNTRY" HeaderText="COUNTRY">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moCountryCodeTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="HOME_PHONE" HeaderText="HOME_PHONE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moHomePhoneTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="YEAR" HeaderText="YEAR">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moModelYearTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="MANUFACTURER" HeaderText="MANUFACTURER">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moManufacturerTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="MODEL" HeaderText="MODEL">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moModelTextGrid" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
                                          Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="VIN" HeaderText="VIN">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moVINTextGrid" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
                                          Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="ENGINE_VERSION" HeaderText="ENGINE_VERSION">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moEngineVersionTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="EXTERNAL_CAR_CODE" HeaderText="EXTERNAL_CAR_CODE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moExternalCarCodeTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="VEHICLE_LICENSE_TAG" HeaderText="VEHICLE_LICENSE_TAG">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="movVehicleLicenseTagTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="ODOMETER" HeaderText="ODOMETER">
                                  <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moOdometereTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="PURCHASE_PRICE" HeaderText="PURCHASE_PRICE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moPurchasePriceTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="PURCHASE_DATE" HeaderText="PURCHASE_DATE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moPurchaseDateTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                      <asp:ImageButton ID="moPurchaseDateImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                      </asp:ImageButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="IN_SERVICE_DATE" HeaderText="IN_SERVICE_DATE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moInServiceDateTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                      <asp:ImageButton ID="moInServiceDateImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                      </asp:ImageButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="DELIVERY_DATE" HeaderText="DELIVERY_DATE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moDeliveryDateTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                      <asp:ImageButton ID="moDeliveryDateImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                      </asp:ImageButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="PLAN_CODE" HeaderText="PLANCODE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moPlanCodeTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                                  <FooterStyle HorizontalAlign="Right"></FooterStyle>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="DEDUCTIBLE" HeaderText="DEDUCTIBLE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moDeductibleTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="TERM_MONTHS" HeaderText="TERM_MONTHS">
                                  <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moTermMonthsTextGrid" runat="server" align="right" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="TERM_KM_MI" HeaderText="TERM_KM_MI">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moTermKmMiTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="AGENT_NUMBER" HeaderText="AGENT_NUMBER">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moAgentNumberTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="WARRANTY_SALE_DATE" HeaderText="WARRANTY_SALE_DATE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moWarrantySaleDateTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                      <asp:ImageButton ID="moWarrantySaleDateImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                      </asp:ImageButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="PLAN_AMOUNT" HeaderText="PLAN_AMOUNT">
                                  <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moPlanAmountTextGrid" runat="server" Width="100%" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="DOCUMENT_TYPE" HeaderText="DOCUMENT_TYPE">
                                  <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moDocumentTypeTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="IDENTITY_DOC_NO" HeaderText="IDENTITY_DOC_NO">
                                  <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moIdentityDocNoTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="RG_NO" HeaderText="RG_NO">
                                  <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moRgNoTextGrid" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
                                          Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="ID_TYPE" HeaderText="ID_TYPE">
                                  <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moIdTypeTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="DOCUMENT_ISSUE_DATE" HeaderText="DOCUMENT_ISSUE_DATE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moDocumentIssueDateTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                      <asp:ImageButton ID="moDocumentIssueDateImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                      </asp:ImageButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="DOCUMENT_AGENCY" HeaderText="DOCUMENT_AGENCY">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moDocumentAgencyTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="NEW_USED" HeaderText="NEW_USED">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moNewUsedTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="OPTIONAL_COVERAGE" HeaderText="OPTIONAL_COVERAGE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moOptionalCoverageTextGrid" runat="server" Width="200px" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="DATE_OF_BIRTH" HeaderText="DATE_OF_BIRTH">
                                  <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moBirthDateTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                      <asp:ImageButton ID="moBirthDateImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                      </asp:ImageButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="WORK_PHONE" HeaderText="WORK_PHONE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moWorkPhoneTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="PAYMENT_TYPE" HeaderText="PAYMENT_TYPE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moPaymentTypeTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="PAYMENT_INSTRUMENT" HeaderText="PAYMENT_INSTRUMENT">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moPaymentInstrumentTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="INSTALLMENT_NUMBER" HeaderText="INSTALLMENT_NUMBER">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moInstallmentNumberTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="PLAN_AMOUNT_WITH_MARKUP" HeaderText="PLAN_AMOUNT_WITH_MARKUP">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moPlanAmtwithMarkupTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="PAYMENT_DATE" HeaderText="PAYMENT_DATE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moPaymentDateTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                      <asp:ImageButton ID="moPaymentDateImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                      </asp:ImageButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="CANCELLATION_DATE" HeaderText="CANCELLATION_DATE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moCancellationDateTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                      <asp:ImageButton ID="moCancellationDateImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                      </asp:ImageButton>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="CANCELLATION_REASON_CODE" HeaderText="CANCELLATION_REASON_CODE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moCancelReasonCodeTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="CANCEL_COMMENT_TYPE_CODE" HeaderText="CANCEL_COMMENT_TYPE_CODE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moCancelCommentTypeCodeTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="CANCELLATION_COMMENT" HeaderText="CANCELLATION_COMMENT">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moCancellationCommentTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField Visible="False">
                                  <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:Label ID="moModifiedDateLabel" runat="server" Text='<%# Container.DataItem("modified_date")%>'>
                                      </asp:Label>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="FINANCING_AGENCY" HeaderText="FINANCING_AGENCY">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moFinancingAgencyTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="BANK_ID" HeaderText="BANK_ID">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moBankIdTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="NC_PAYMENT_METHOD_CODE" HeaderText="NC_PAYMENT_METHOD_CODE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moNCPaymentMethodCodeTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="NAME_ON_ACCOUNT" HeaderText="NAME_ON_ACCOUNT">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moNameOnAccountTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="ACCOUNT_TYPE_CODE" HeaderText="ACCOUNT_TYPE_CODE">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moAccountTypeCodeTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="TAX_ID" HeaderText="TAX_ID">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moTaxIDTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="BRANCH_DIGIT" HeaderText="BRANCH_DIGIT">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moBranchDigitTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="ACCOUNT_DIGIT" HeaderText="ACCOUNT_DIGIT">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moAccountDigitTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                              <asp:TemplateField SortExpression="REFUND_AMOUNT" HeaderText="REFUND_AMOUNT">
                                  <ItemStyle Wrap="False" HorizontalAlign="Left"></ItemStyle>
                                  <ItemTemplate>
                                      <asp:TextBox ID="moRefundAmountTextGrid" runat="server" onFocus="setHighlighter(this)"
                                          onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                  </ItemTemplate>
                              </asp:TemplateField>
                          </Columns>
                          <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                           <PagerStyle HorizontalAlign="Left" CssClass="PAGER_LEFT"></PagerStyle>
                      </asp:GridView>
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
     <div id="ModalProgressBar" class="overlay">
        <asp:Button ID="hiddenTargetControlForModalPopup" runat="server" style="display:none" />
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup"  
            TargetControlID="hiddenTargetControlForModalPopup"
            PopupControlID="pnlPopup" 
            BackgroundCssClass="modalBackground"
            DropShadow="True"
            PopupDragHandleControlID="programmaticPopupDragHandle"
            RepositionMode="RepositionOnWindowScroll" 
            >
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server"  CssClass="modalPopup" style="display:none;width:750px">
            <asp:Panel runat="Server" ID="programmaticPopupDragHandle" Style="cursor: move;background-color:#DDDDDD;border:solid 1px Gray;color:Black;text-align:center;">                
            </asp:Panel>           
            <asp:UpdatePanel ID="updPnlBundles" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView runat="server" ID="gvPop" AutoGenerateColumns="false" BackColor="#DEE3E7"
                        BorderWidth="1px" BorderColor="#999999" BorderStyle="Solid">
                        <SelectedRowStyle Wrap="False" BackColor="Transparent"></SelectedRowStyle>
                        <EditRowStyle Wrap="False" BackColor="AliceBlue"></EditRowStyle>
                        <AlternatingRowStyle Wrap="False" BackColor="#F1F1F1"></AlternatingRowStyle>
                        <RowStyle Wrap="False" BackColor="White"></RowStyle>
                        <HeaderStyle  Wrap="False" ForeColor="#003399"></HeaderStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Item_Number">
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <HeaderStyle />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtItemNumber" Text='<%#Bind("ITEM_NUMBER")%>' ReadOnly="true"
                                        Style="text-align: center; width: 100%"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Make">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtMake" MaxLength="50" Text='<%#Bind("ITEM_MANUFACTURER")%>'
                                        onchange="javascript:setBundlesDirty();"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Model">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtModel" MaxLength="30" Text='<%#Bind("ITEM_MODEL")%>'
                                        onchange="javascript:setBundlesDirty();"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Serial_Number">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="0%" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtSerNum" MaxLength="20" Text='<%#Bind("ITEM_SERIAL_NUMBER")%>'
                                        onchange="javascript:setBundlesDirty();" Style="width: 100%"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtDesc" MaxLength="50" Text='<%#Bind("ITEM_DESCRIPTION")%>'
                                        onchange="javascript:setBundlesDirty();"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Price">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="0%" />
                                <HeaderStyle />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtPrice" EnableViewState="true" Text='<%#Bind("ITEM_PRICE")%>'
                                        MaxLength="11" onchange="javascript:setBundlesDirty();" Style="text-align: center;
                                        width: 60px"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bundle_Value">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="0%" />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtBundlVal" Text='<%#Bind("ITEM_BUNDLE_VAL")%>'
                                        MaxLength="20" onchange="javascript:setBundlesDirty();" Style="width: 60px"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manufacturer_Warranty">
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle />
                                <ItemTemplate>
                                    <asp:TextBox runat="server" ID="txtManWarr" MaxLength="5" Text='<%#Bind("ITEM_MAN_WARRANTY")%>'
                                        onchange="javascript:setBundlesDirty();" Style="text-align: center; width: 100%"></asp:TextBox></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>          
        </asp:Panel>

     </div>
    </div>  
       <div class="btnZone">  
       <div id="Div1" class="overlay">
        <asp:Button ID="btnClose" runat="server" Text="Back" CausesValidation="False"
            Enabled="False" SkinID="AlternateLeftButton" OnClick="btnClose_Click"></asp:Button>&nbsp;
        <asp:Button ID="btnApply" runat="server" Text="Apply" CausesValidation="False"
            Enabled="False" SkinID="AlternateLeftButton" OnClick="btnApply_Click"></asp:Button>&nbsp;
       </div>
       </div>
</asp:Content>