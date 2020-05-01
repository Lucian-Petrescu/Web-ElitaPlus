<%@ Page ValidateRequest="false" Language="vb" AutoEventWireup="false" CodeBehind="ARPaymentReconWrkForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.ARPaymentReconWrkForm" 
    Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js" />
<script language="JavaScript" type="text/javascript" src="../Navigation/scripts/GlobalHeader.js"/>
      
	
	
    <script type="text/javascript">
        function TABLE1_onclick() {
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" align ="center" class="searchGrid">
     <tr>
            <td align="right" width="10%">
				<asp:Label id="moDealerNameLabel" runat="server" visible="True">DEALER_NAME</asp:Label>
            </td>            
            <td>
				<asp:TextBox id="moDealerNameText" runat="server" Visible="True" ReadOnly="True" SkinID="MediumTextBox" Enabled="False"></asp:TextBox>
           </td>            
            <td align="right" width="10%">
                <asp:Label ID="moFileNameLabel" runat="server" Visible="True">FILENAME:</asp:Label>
            </td>            
            <td>
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
                        <asp:Label ID="Label1" runat="server">Page_Size</asp:Label>: &nbsp;
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
                          SkinID="DetailPageGridView">                          
                          <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                          <EditRowStyle Wrap="True"></EditRowStyle>
                          <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                          <RowStyle Wrap="True"></RowStyle>
                          <HeaderStyle Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                          <PagerStyle HorizontalAlign ="left" />
                          <Columns>							
								<asp:TemplateField Visible="False">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Center" Width="0px"></ItemStyle>
									<ItemTemplate>
										<asp:ImageButton id="EditButton_WRITE" style="CURSOR: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"
											CommandName="EditRecord"></asp:ImageButton>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField Visible="False">
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="moDealerReconWrkIdLabel" text='<%#GetGuidStringFromByteArray(Container.DataItem("Payment_Interface_Id"))%>' runat="server">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField SortExpression="RECORD_TYPE" HeaderText="RECORD_TYPE">
									<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<%--<asp:TextBox id="moRecordTypeTextGrid" Width="40px" runat="server" visible="True"></asp:TextBox>--%>
                                        <asp:DropDownList ID="moRecordTypeDrop" runat="server" Width="55px" Visible="True">
                                      </asp:DropDownList>
									</ItemTemplate>
								</asp:TemplateField>
                              <asp:TemplateField SortExpression="REJECT_CODE" HeaderText="REJECT_CODE">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="214px" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moRejectCodeGrid" ReadOnly="true" 	runat="server" Width="30px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField SortExpression="REJECT_REASON" HeaderText="REJECT_REASON">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="214px" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="RejectReasonTextGrid" ReadOnly="true" 	runat="server" Width="214px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField SortExpression="CREDIT_CARD" HeaderText="CREDIT_CARD">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="100px" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moCreditCardGrid" runat="server" Width="100px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField SortExpression="CERTIFICATE" HeaderText="CERTIFICATE">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moCertificateTextGrid" runat="server" Width="100px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField SortExpression="SUBSCRIBER_NUMBER" HeaderText="SUBSCRIBER_NUMBER">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moSubscriberNumberGrid"  
											runat="server" Width="135px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField SortExpression="PAYMENT_AMOUNT" HeaderText="PAYMENT_AMOUNT">
									<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moPaymentAmountTextGrid" runat="server" Width="50px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField SortExpression="PAYMENT_DATE" HeaderText="PAYMENT_DATE">
									<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moPaymentDateGrid"  
											runat="server" Width="90px" visible="True"></asp:TextBox>
										<asp:ImageButton id="moDateOfPayImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField SortExpression="INVOICE_DATE" HeaderText="INVOICE_DATE">
									<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moInvoiceDateGrid" runat="server"  
											Width="90px" visible="True"></asp:TextBox>
										<asp:ImageButton id="moDatePaidForImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField SortExpression="INVOICE_PERIOD_START_DATE" HeaderText="INVOICE_PERIOD_START_DATE">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moInvoicePeriodStartDateGrid" runat="server" Width="135px" visible="True"></asp:TextBox>
                                        <asp:ImageButton id="moDatePaidForImageGrid1" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField SortExpression="INVOICE_PERIOD_END_DATE" HeaderText="INVOICE_PERIOD_END_DATE">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moInvoicePeriodEndDateGrid" runat="server"  
											visible="True"></asp:TextBox>
                                        <asp:ImageButton id="moDatePaidForImageGrid2" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField SortExpression="INVOICE_NUMBER" HeaderText="INVOICE_NUMBER">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moInvoiceNumberGrid" runat="server"  width="140px"
											visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>                                                                            
								<%--<asp:TemplateField Visible="False">
									<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="moModifiedDateLabel" runat="server" text='<%# Container.DataItem("modified_date")%>'>
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateField>--%>
                                <asp:TemplateField SortExpression="POST_PRE_PAID" HeaderText="POST_PRE_PAID">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moPostPrePaidGrid"  
											runat="server" Width="135px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
                                <asp:TemplateField SortExpression="PAYMENT_METHOD" HeaderText="PAYMENT_METHOD">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moPaymentMethodGrid"  
											runat="server" Width="135px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
                                <asp:TemplateField SortExpression="PAYMENT_ENTITY_CODE" HeaderText="PAYMENT_ENTITY_CODE">
									<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moPaymentEntityCodeGrid"  
											runat="server" Width="130px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
                                <asp:TemplateField SortExpression="PAYMENT_LOADED" HeaderText="PAYMENT_LOADED">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moPaymentLoadedGrid"  
											runat="server" Width="135px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
                                <asp:TemplateField SortExpression="APPLICATION_MODE" HeaderText="APPLICATION_MODE">
									<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moApplicationModeGrid"  
											runat="server" Width="50px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
                                <asp:TemplateField SortExpression="INSTALLMENT_NUMBER" HeaderText="INSTALLMENT_NUMBER">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moInstallmentNumTextGrid"  
											runat="server" Width="50px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
                                <asp:TemplateField SortExpression="CURRENCY_CODE" HeaderText="CURRENCY_CODE">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moFeeIncomeTextGrid"  
											runat="server" Width="50px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
                              <asp:TemplateField SortExpression="ENTIRE_RECORD" HeaderText="ENTIRE_RECORD">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moEntireRecordGrid"  
											runat="server" Width="50px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
                              <asp:TemplateField SortExpression="REFERENCE" HeaderText="REFERENCE">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moReferenceGrid"  
											runat="server" Width="50px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
                              <asp:TemplateField SortExpression="SOURCE" HeaderText="SOURCE">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moSourceGrid"  
											runat="server" Width="50px" visible="True"></asp:TextBox>
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
        <asp:Button ID="btnSave_WRITE" runat="server" Text="Save" CausesValidation="False"
             SkinID="AlternateLeftButton"></asp:Button>&nbsp;
        <asp:Button ID="btnUndo_WRITE" runat="server" Text="Undo" CausesValidation="False"
             SkinID="AlternateLeftButton"></asp:Button>&nbsp;
        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="False"
             SkinID="AlternateLeftButton"></asp:Button>&nbsp;
    </div>  										
	</asp:Content>	