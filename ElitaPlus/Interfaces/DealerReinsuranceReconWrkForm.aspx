
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DealerReinsuranceReconWrkForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.DealerReinsuranceReconWrkForm"
   Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js" />
    <script language="JavaScript" type="text/javascript" src="../Navigation/scripts/GlobalHeader.js" />


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
										<asp:Label id="moDealerReconWrkIdLabel" text='<%# GetGuidStringFromByteArray(Container.DataItem("dealer_reins_recon_wrk_id"))%>' runat="server">
										</asp:Label>
									</ItemTemplate>
								</asp:TemplateField>
								<asp:TemplateField SortExpression="RECORD_TYPE" HeaderText="RECORD_TYPE">
									<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<%--<asp:TextBox id="moRecordTypeTextGrid" Width="40px" runat="server" visible="True"></asp:TextBox>--%>
                                        <asp:DropDownList ID="moRecordTypeDrop" runat="server" Width="75px" Visible="True">
                                      </asp:DropDownList>
									</ItemTemplate>
								</asp:TemplateField>

                            
								<asp:TemplateField SortExpression="REJECT_REASON" HeaderText="REJECT_REASON">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="214px" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="RejectReasonTextGrid" 	runat="server" Width="214px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>

                                	<asp:TemplateField SortExpression="CERTIFICATE" HeaderText="CERTIFICATE">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="170px" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moCertificateTextGrid" runat="server" width="170px" Visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
						<asp:TemplateField SortExpression="REINSURANCE_REJECT_REASON" HeaderText="Reinsurance_Reject_Reason">
									<HeaderStyle  Wrap="False" HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle Wrap="False" HorizontalAlign="Left" Width="100%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:TextBox id="moReinsuranceRejectReasonGrid" runat="server" Width="214px" visible="True"></asp:TextBox>
									</ItemTemplate>
								</asp:TemplateField>
							                                                                       
								<asp:TemplateField Visible="False">
									<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="moModifiedDateLabel" runat="server" text='<%# Container.DataItem("modified_date")%>'>
										</asp:Label>
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
