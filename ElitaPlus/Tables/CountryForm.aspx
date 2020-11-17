<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CountryForm.aspx.vb" Theme="Default"
	Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CountryForm" EnableSessionState="True"
	MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
		<Scripts>
			<asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
		</Scripts>
	</asp:ScriptManager>
		<script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
	<div class="dataContainer">
		<table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
			width="100%">
			<tr>
				<td align="right" nowrap="nowrap" class="borderLeft">
					<asp:Label ID="LabelCode" runat="server" Font-Bold="false">Code</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:TextBox ID="TextboxCode" TabIndex="10" runat="server" Width="140px" CssClass="FLATTEXTBOX"></asp:TextBox></td>
				<td align="right" class="borderRight" nowrap="nowrap">
					<asp:Label ID="LabelDescription" runat="server" Font-Bold="false">Description</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:TextBox ID="TextboxDescription" TabIndex="5" runat="server" Width="140px" CssClass="FLATTEXTBOX"></asp:TextBox></td>
			</tr>
			<tr>
               <td colspan="4"></td>
            </tr>
			<tr>
				<td align="right" class="borderLeft" nowrap="nowrap">
					<asp:Label ID="LabelLanguage" runat="server" Font-Bold="false">Language</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:DropDownList ID="cboLanguageId" TabIndex="15" runat="server" Width="140px"></asp:DropDownList></td>
				<td align="right" nowrap="nowrap">
					<asp:Label ID="LabelRegulatoryReporting" class="borderRight" runat="server" Font-Bold="false">Regulatory_Reporting_ID</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:DropDownList ID="cboRegulatoryReporting" TabIndex="20" runat="server" 
                        Width="140px"></asp:DropDownList></td>
			</tr>
			<tr>
				<td align="right" nowrap="nowrap" class="borderLeft">
					<asp:Label ID="LabelEuropeanCountry" runat="server" Font-Bold="false">European_Country</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:DropDownList ID="cboEuropeanCountry" TabIndex="20" runat="server" Width="140px"></asp:DropDownList></td>
				<td align="right" nowrap="nowrap">
					<asp:Label ID="LabelRegulatoryNotifyGroupEmail"  class="borderRight" runat="server" Font-Bold="false">Notify_Group_Email</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:TextBox ID="TextboxRegulatoryNotifyGroupEmail" TabIndex="5" runat="server" 
                        Width="280px" CssClass="FLATTEXTBOX"></asp:TextBox></td>
			</tr>
			<tr>
				<td align="right" class="borderLeft" nowrap="nowrap">
					<asp:Label ID="LabelPrimaryCurrency" runat="server" Font-Bold="false">Primary_Currency</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:DropDownList ID="cboPrimaryCurrencyId" TabIndex="25" runat="server" Width="140px"></asp:DropDownList></td>
				<td align="right" nowrap="nowrap">
					<asp:Label ID="LabelLastRegulatoryExtractDate"  class="borderRight" runat="server" Font-Bold="false" ClientIDMode="Static">Last_Regulatory_Extract_Date</asp:Label></td>
				<td align="left" nowrap="nowrap"> 
					<asp:TextBox ID="TextboxLastRegulatoryExtractDate" TabIndex="5" Enabled="false" 
                        runat="server" Width="280px" CssClass="FLATTEXTBOX" ClientIDMode="Static"></asp:TextBox></td>
			</tr>
			<tr>
				<td align="right" nowrap="nowrap" class="borderLeft">
					<asp:Label ID="LabelSecondaryCurrency" runat="server" Font-Bold="false">Secondary_Currency</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:DropDownList ID="cboSecondaryCurrencyId" TabIndex="30" runat="server" Width="140px"></asp:DropDownList></td>
			    <td align="right" nowrap="nowrap">
					<asp:Label ID="LabelCreditScoringPct" runat="server" Font-Bold="false">Credit_Scoring_Pct</asp:Label></td>
				<td align="left" nowrap="nowrap"> 
					<asp:TextBox ID="TextboxCreditScoringPct" TabIndex="5" runat="server" Width="280px"></asp:TextBox></td>
			</tr>
			<tr>
				<td align="right" class="borderLeft" nowrap="nowrap">
					<asp:Label ID="LabelBankIDLength" runat="server" Font-Bold="false">BANK_ID_LENGTH</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:TextBox ID="TextboxBankIDLength" TabIndex="35" runat="server" Width="140px"></asp:TextBox></td>
		        <td align="right" nowrap="nowrap">
					<asp:Label ID="LabelAbnormalClmFrqNo" runat="server" Font-Bold="false">Abnormal_Clm_Frq_No</asp:Label></td>
				<td align="left" nowrap="nowrap"> 
					<asp:TextBox ID="TextboxAbnormalClmFrqNo" TabIndex="5" runat="server" Width="280px"></asp:TextBox></td>
			</tr>
			<tr>
				<td align="right" nowrap="nowrap" class="borderLeft">
					<asp:Label ID="LabelBankAcctNoLength" runat="server" Font-Bold="false">BANK_ACCTNO_LENGTH</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:TextBox ID="TextboxBankAcctNoLength" TabIndex="40" runat="server" Width="140px"></asp:TextBox></td>
		        <td align="right" nowrap="nowrap">
					<asp:Label ID="LabelCertCountSuspOp"  runat="server" Font-Bold="false">Cert_Count_Susp_Op</asp:Label></td>
				<td align="left" nowrap="nowrap"> 
					<asp:TextBox ID="TextboxCertCountSuspOp" TabIndex="5" runat="server" Width="280px"></asp:TextBox></td>
			</tr>
			<tr>
				<td align="right" class="borderLeft" nowrap="nowrap">
					<asp:Label ID="LabelAddrInfoReqFields" runat="server" Font-Bold="false">AddrInfoReqFields</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:TextBox ID="TextboxAddrInfoReqFields" TabIndex="45" runat="server" Width="337px" />
					<asp:Button ID="btnAddrInfoReqFields" Style="cursor: hand" runat="server" Text="..." OnClientClick="return RevealModalWithMessage('ModalIssue','1');"></asp:Button>
				</td>
				<td align="right" nowrap="nowrap">
					<asp:Label ID="LabelValidateBankInfo" runat="server" Font-Bold="false">Validate_Bank_Info</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:DropDownList ID="cboValidateBankInfo" TabIndex="50" runat="server" Width="140px"></asp:DropDownList></td>
			</tr>
			<tr>
				<td align="right" class="borderLeft" nowrap="nowrap">
					<asp:Label ID="LabelMailAddrFormat" runat="server" Font-Bold="false">MailAddrFormat</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:TextBox ID="TextboxMailAddrFormat" TabIndex="45" runat="server" Width="337px" />
					<asp:Button ID="btnMailAddrFormat" Style="cursor: hand" runat="server" Text="..." OnClientClick="return RevealModalWithMessage('ModalIssue','2');"></asp:Button>
				</td>
				<td align="right" nowrap="nowrap" >
					<asp:Label ID="LabelTaxByProductType" runat="server" Font-Bold="false">Tax_By_Product_Type</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:DropDownList ID="cboTaxByProductType" TabIndex="55" runat="server" Width="140px"></asp:DropDownList></td>
			</tr>
			<tr>
				<td align="right" class="borderLeft" nowrap="nowrap">
					<asp:Label ID="LabelCONTACT_INFO_REQ_FIELDS" runat="server" Font-Bold="False">CONTACT_INFO_REQ_FIELDS</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:TextBox ID="TextboxCONTACT_INFO_REQ_FIELDS" runat="server" TabIndex="45" Width="337px" />
					<asp:Button ID="btnCONTACT_INFO_REQ_FIELDS" runat="server" Style="cursor: hand" Text="..." OnClientClick="return RevealModalWithMessage('ModalIssue','3');"/>
				</td>
				<td align="right" nowrap="nowrap" >
					<asp:Label ID="lblUseBankList" runat="server" Font-Bold="false">Use_Bank_List</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:DropDownList ID="cboUseBankList" TabIndex="55" runat="server" Width="140px"></asp:DropDownList></td>
			</tr>
			<tr>
				<td align="right" class="borderLeft" nowrap="nowrap">
					<asp:Label ID="lblDefaultSCForDeniedClaims" runat="server" Font-Bold="false">DEFAULT_SC_FOR_DENIED_CLAIMS:</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:TextBox ID="txtDefaultSCForDeniedClaims" TabIndex="60" runat="server" Width="337px" CssClass="FLATTEXTBOX"></asp:TextBox>
					<input id="inpDefaultSCFDCId" type="hidden" name="inpDefaultSCFDCId" runat="server" />
					<input id="inpDefaultSCFDCDesc" type="hidden" name="inpDefaultSCFDCDesc" runat="server" />
					<cc1:AutoCompleteExtender ID="aDefaultSCFDC" OnClientItemSelected="comboSelectedDefaultSCFDC"
						runat="server" TargetControlID="txtDefaultSCForDeniedClaims" ServiceMethod="PopulateServiceCenterDropFDC"
						MinimumPrefixLength='1' CompletionListCssClass="autocomplete_completionListElement">
					</cc1:AutoCompleteExtender>
					<script language="javascript" type="text/javascript">
						function comboSelectedDefaultSCFDC(source, eventArgs) {
						    var inpId = document.getElementById('inpDefaultSCFDCId');
						    if (inpId == null) {
						        inpId = document.getElementById('<%=inpDefaultSCFDCId.ClientID %>');
                            }
						    var inpDesc = document.getElementById('inpDefaultSCFDCDesc');
						    if (inpDesc == null) {
						        inpDesc = document.getElementById('<%=inpDefaultSCFDCDesc.ClientID %>');
                            }
							inpId.value = eventArgs.get_value();
							inpDesc.value = eventArgs.get_text();
						}
					</script>
				</td>
				<td align="right" nowrap="nowrap">
					<asp:Label ID="lblRequireByteCheck" runat="server" Font-Bold="false">REQUIRE_BYTE_CONVERSION:</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:DropDownList ID="cboByteCheck" runat="server" Width="140px"></asp:DropDownList>
				</td>
			</tr>
			<tr>
				<td align="right" class="borderLeft" nowrap="nowrap">
					<asp:Label ID="lblDefaultSC" runat="server" Font-Bold="false">DEFAULT_SC:</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:TextBox ID="txtDefaultSC" runat="server" Width="337px" CssClass="FLATTEXTBOX"></asp:TextBox>
					<input id="inpDefaultSCId" type="hidden" name="inpDefaultSCId" runat="server" />
					<input id="inpDefaultSCDesc" type="hidden" name="inpDefaultSCDesc" runat="server" />
					<cc1:AutoCompleteExtender ID="aDefaultSC" OnClientItemSelected="comboSelectedDefaultSC"
						runat="server" TargetControlID="txtDefaultSC" ServiceMethod="PopulateServiceCenterDrop"
						MinimumPrefixLength='1' CompletionListCssClass="autocomplete_completionListElement">
					</cc1:AutoCompleteExtender>
					<script language="javascript" type="text/javascript">
					    function comboSelectedDefaultSC(source, eventArgs) {
					        var inpId = document.getElementById('inpDefaultSCId');
					        if (inpId == null) {
					            inpId = document.getElementById('<%=inpDefaultSCId.ClientID %>');
					        }
					        var inpDesc = document.getElementById('inpDefaultSCDesc');
					        if (inpDesc == null) {
					            inpDesc = document.getElementById('<%=inpDefaultSCDesc.ClientID %>');
					        }
					        inpId.value = eventArgs.get_value();
					        inpDesc.value = eventArgs.get_text();
					    }
					</script>
				</td>
			    <td align="right" nowrap="nowrap">
			        <asp:Label ID="lblISOCode" class="borderRight" runat="server" Font-Bold="false">ISO_CODE</asp:Label></td>
			    <td align="left" nowrap="nowrap">
			        <asp:TextBox ID="txtISOCode" runat="server" Width="280px"></asp:TextBox>
			    </td>
            </tr>
            <tr>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="lblUseAddressValidation" class="borderRight" runat="server" Font-Bold="false">USE_ADDRESS_VALIDATION</asp:Label></td>
                <td align="left" nowrap="nowrap">
                    <asp:DropDownList ID="cboUseAddressValidation" TabIndex="68" runat="server"  
                                      Width="140px"></asp:DropDownList></td>
                  <td align="right" nowrap="nowrap">
                    <asp:Label ID="lblAllowForget" class="borderRight" runat="server" Font-Bold="false">ALLOW_FORGET</asp:Label></td>
                <td align="left" nowrap="nowrap">
                    <asp:DropDownList ID="cboAllowForget" TabIndex="68" runat="server" Width="140px"></asp:DropDownList>               
				</td>
            </tr>
			<tr>				 
				<td align="right" nowrap="nowrap">
                    <asp:Label ID="lblPriceListApprovalNeeded" class="borderRight" runat="server" Font-Bold="false">PRICE_LIST_APPROVAL_NEEDED</asp:Label></td>
                <td align="left" nowrap="nowrap">
                    <asp:DropDownList ID="cboPriceListApprovalNeeded" TabIndex="68" runat="server" Width="140px"></asp:DropDownList>
                </td>
				<td align="right" nowrap="nowrap">
			        <asp:Label ID="lblPriceListApprovalEmail" class="borderRight" runat="server" Font-Bold="false">PRICE_LIST_APPROVAL_EMAIL</asp:Label></td>
			    <td align="left" nowrap="nowrap">
			        <asp:TextBox ID="txtPriceListApprovalEmail" runat="server" Width="280px"></asp:TextBox>
			    </td>
			</tr>
            <tr>
				<td align="right" class="borderLeft" nowrap="nowrap">
					<asp:Label ID="lblAllowForceAddress" runat="server" Font-Bold="false">ALLOW_FORCE_ADDRESS</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:DropDownList ID="cboAllowForceAddress" runat="server" Width="140px"></asp:DropDownList></td>
				<td align="right" nowrap="nowrap">
					<asp:Label ID="lblAddressConfidenceThreshold" class="borderRight" runat="server" Font-Bold="false">ADDRESS_CONFIDENCE_THRESHOLD</asp:Label></td>
				<td align="left" nowrap="nowrap">
                    <asp:TextBox ID="TextboxAddressConfidenceThreshold" runat="server" Width="140px"></asp:TextBox>
					</td>
			</tr>
		    <tr>
		        <td align="right" nowrap="nowrap">
		            <asp:Label ID="lblFullnameFormat" class="borderRight" runat="server" Font-Bold="false">FULL_NAME_FORMAT</asp:Label></td>
		        <td align="left" nowrap="nowrap">
                    <asp:DropDownList ID="cboFullNameFormat" runat="server" Width="280px"></asp:DropDownList></td>
                <%--PBI 604501 - Add a new field to manage the BIC value in SEPA FILE--%>
                <td align="right" nowrap="nowrap">
					<asp:Label ID="lblUseSepaBicCustomer" class="borderRight" runat="server" Font-Bold="false">USE_SEPA_BIC_CUSTOMER</asp:Label></td>
				<td align="left" nowrap="nowrap">
					<asp:DropDownList ID="cboUseSepaBicCustomer" TabIndex="69" runat="server" Width="140px"></asp:DropDownList></td>
            </tr>

			<tr>
				<td colspan="2" class="borderLeft">
					<table id="tblPostalCode" cellspacing="0" cellpadding="6" align="center" border="0">
						<tr>
							<td style="height: 13px" align="left">
								<asp:Label ID="AvailPostalCodeLabel" runat="server">AVAILABLE_POSTAL_CODE_FORMATS</asp:Label></td>
							<td style="height: 13px"></td>
							<td style="height: 13px" align="left">
								<asp:Label ID="SelectedPostalCodeLabel" runat="server">SELECTED_POSTAL_CODE_FORMATS</asp:Label></td>
						</tr>
						<tr>
							<td style="height: 118px" align="left">
								<asp:ListBox ID="AvailPostalCodeList" runat="server" Width="216px" Height="100px" AutoPostBack="false"></asp:ListBox></td>
							<td style="height: 118px" align="center">
								<asp:Button ID="AddFormat" Style="cursor: hand" runat="server" Text=">>" UseSubmitBehavior="false" OnClick="AddFormat_Click"></asp:Button><br /><br />
								<asp:Button ID="RemoveFormat" Style="cursor: hand" runat="server" Text="<<" UseSubmitBehavior="false" OnClick="RemoveFormat_Click"></asp:Button></td>
							<td style="height: 118px" align="left">
								<asp:ListBox ID="SelectedPostalCodeList" runat="server" Width="216px" Height="100px" AutoPostBack="false"></asp:ListBox></td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		 <div class="dataContainer">
            <div id="tabs" class="style-tabs">

                <ul>
                    <li><a href="#tabsLineOfBusiness">
                        <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">LINE_OF_BUSINESS</asp:Label></a></li>
                </ul>
                <div id="tabsLineOfBusiness">

                    <table id="tblLineOfBusiness" class="dataGrid" border="0" rules="cols" width="98%">
                        <tr>
                            <td colspan="2">
                                <uc1:ErrorController ID="ErrorControllerDS" runat="server"></uc1:ErrorController>
                            </td>
                        </tr>

                        <tr>
                        <td align="center" colspan="2">
                           <%-- <div id="scrollerlineOfBusinessGrid" style="overflow: auto; width: 96%; height: 125px" align="center">--%>
                                <asp:GridView ID="moLineOfBusinessGridView" runat="server" Width="100%"
                                                AllowPaging="False" AllowSorting="false" PageSize="50" CellPadding="1" AutoGenerateColumns="False"
                                                SkinID="DetailPageGridView"
                                                EnableViewState="true">
                                                <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                                                <EditRowStyle Wrap="False"></EditRowStyle>
                                                <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                                                <RowStyle Wrap="False"></RowStyle>
                                                <HeaderStyle Wrap="False"></HeaderStyle>
                                                <Columns>
                                                    <asp:TemplateField Visible="True"  HeaderText="CODE"> 
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLineBusinessCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "code")%>' Visible="True" SkinID="SmallTextBox">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtLineBusinessCode" runat="server" Visible="True" SkinID="SmallTextBox" />
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="True" HeaderText="DESCRIPTION">
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLineBusinessDescription" Text='<%# DataBinder.Eval(Container.DataItem, "Description")%>' runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtLineBusinessDescription" Width="220px" runat="server" Visible="True" SkinID="SmallTextBox" />

                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField Visible="True" HeaderText="BUSINESS_TYPE">
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBusinessType" Text='<%# DataBinder.Eval(Container.DataItem, "LineOfBusinessId")%>' runat="server">
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddlBusinessType" runat="server" Visible="true" Width="250px"></asp:DropDownList>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField Visible="True" HeaderText="IN_USE">
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        <ItemTemplate>
                                                                        <asp:CheckBox ID="chkLineBusinessInUse" Enabled="false"  Checked='<%# DataBinder.Eval(Container.DataItem, "InUse") = "Y"%>'  runat="server"></asp:CheckBox>
                                                        </ItemTemplate>


                                                    </asp:TemplateField>

                                                    <asp:TemplateField ItemStyle-Width="30px">
                                                        <HeaderStyle ForeColor="#12135B" Wrap="False"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="EditButton_WRITE"  Style="cursor: hand" runat="server" ImageUrl="~/Navigation/images/icons/edit2.gif"
                                                                CommandName="EditRecord" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:LinkButton ID="BtnCancel" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                                                Text="Cancel"></asp:LinkButton>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="30px">
                                                        <HeaderStyle ForeColor="#12135B" Wrap="False"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="~/Navigation/images/icons/trash.gif"
                                                                runat="server" CommandName="DeleteRecord" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'></asp:ImageButton>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:Button ID="BtnSave_WRITE" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                                                Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
                                                <PagerStyle />
                                            </asp:GridView>
                               <%-- </div>--%>
                            </td>
                            </tr>
                        <tr>
                        <td colspan="2" width="96%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  <asp:Button ID="btnNewBusinessInfo" runat="server" SkinID="AlternateLeftButton" Text="New"></asp:Button>                           
                        </td>
                    </tr>
                    </table>
                </div>
            </div>

        </div>
		<div id="ModalIssue" class="overlay">
			<div id="Div2" class="overlay_message_content" style="width: 630px;">
				<p class="modalTitle">
                    <label id="Label1"></label>
					<a href="javascript:void(0)" onclick="hideModal('ModalIssue');">
						<img id="img2" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
							width="16" height="18" align="middle" class="floatR" /></a></p>
				<table cellspacing="0" cellpadding="0" width="100%" bgcolor="#f4f3f8" border="0">
					<tr>
						<td valign="top" width="100%" height="264">
							<table style="BACKGROUND-REPEAT: repeat" height="100%" cellspacing="0" cellpadding="0" width="100%" border="0">
								<tr>
									<td width="2" height="3"></td>
									<td width="100%" background="../Navigation/images/body_top_back.jpg" height="3"><img height="3" src="../Navigation/images/trans_spacer.gif" width="100%" /></td>
									<td width="2" height="3"></td>
								</tr>
								<tr>
									<td width="2"></td>
									<td valign="top" width="100%" background="../Navigation/images/body_mid_back.jpg" height="100%">
										<table style="BACKGROUND-REPEAT: repeat" cellspacing="2" cellpadding="2" width="100%" border="0">
											<tr>
												<td valign="top">
													<table id="tblMain" style="BACKGROUND-REPEAT: repeat" cellspacing="2" cellpadding="2" width="100%"
														bgcolor="#f4f3f8" border="0">
														<tr>
															<td valign="top" colspan="4" height="100%">
																<div id="scroller4" style=" WIDTH: 100%; font-size: 13px;">
																	<table style="BACKGROUND-REPEAT: repeat" height="100%" cellspacing="2" cellpadding="2"
																		width="100%" border="0">
																		<tr>
																			<td>
																				<table style="BACKGROUND-REPEAT: repeat" height="100%" cellspacing="0" cellpadding="0"
																					width="100%" border="0">
																					<tr>
																						<td colspan="4">
																							<asp:Label id="Label7" runat="server" Width="608px" Font-Italic="True" Visible="False">COUNTRY_STATIC_MSG</asp:Label></td>
																					</tr>
																					<tr>
																						<td align="left">
                                                                                            <label id="lblAvailable" style="width:200px;">Available Fields</label></td>
																						<td style="WIDTH: 18px"></td>
																						<td style="WIDTH: 230px" align="left">
                                                                                            <label id="lblSelected" style="width:200px;">Selected Fields</label></td>
																						<td></td>
																					</tr>
																					<tr>
																						<td align="left" style="width: 230px;">
                                                                                            <select id="AvailList" style="width:275px; height:130px;" size="20" onchange="handleSpecialChar();"></select></td>
																						<td style="WIDTH: 18px" align="center">
																							<input type="button" id="btnAdd" value=">" onclick="addItem();" title="Add selected items"/><br />
																							<br />
																							<input type="button" id="btnRemove" value="<" onclick="removeItem();" title="Remove selected items"/>
																						</td>
																						<td style="WIDTH: 230px" align="left">
                                                                                            <select id="MailAddrFormatList" style="width:275px; height:130px;" size="20" onchange="handleOptionalListItem();"></select></td>
																						<td>
																							<input type="button" id="btnMoveUp" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/up.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat; width: 25px; height: 21px;"
																								onclick="moveItemUp();" class="FLATBUTTON" title="Move up"/>
																							<br />
																							<input type="button" id="btnMoveDown" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/down.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat; width: 25px; height: 21px;"
																								onclick="moveItemDown();" class="FLATBUTTON" title="Move down"/>
																						</td>
																					</tr>
																					<tr>
																						<td style="WIDTH: 271px" colspan="2">
                                                                                            <label id="SpecialCharLabel" style="display:none;">Enter Character and Press Tab:</label><input type="text" id="SpecialChar" maxlength="1" size="1" style="display:none;" onkeydown="AddSpecialChar();" />
																						    </td>
																						<td style="WIDTH: 271px" colspan="2">
                                                                                            <input type="checkbox" id="chkOptional" onclick="handleSelectedListItemForOptional();" value="Address token optional" />
                                                                                            <label id="chkText">Address token optional</label></td>
																					</tr>
																					<tr>
																						<td style="WIDTH: 271px" colspan="2">
																							<asp:Label id="Label3" runat="server">Preview</asp:Label></td>
																						<td align="left" colspan="2"></td>
																					</tr>
																					<tr>
																						<td style="WIDTH: 271px;" align="left" colspan="2" valign="top">
																							<asp:ListBox id="PreviewList" runat="server" Width="304px" Height="100px" BackColor="whitesmoke"></asp:ListBox></td>
																						<td align="left" colspan="2"><asp:TextBox id="TextBox2" runat="server" Width="180px" Height="58px" TextMode="MultiLine"
																								ReadOnly="True" style="display:none;"></asp:TextBox></td>
																					</tr>
																				</table>
																			</td>
																		</tr>
																		<tr>
																			<td valign="top" align="center" class="btnZone">
																				<input type="button" id="Button1" title="Save changes to database" class="altBtn" skinid="AlternateLeftButton" onclick="SubmitData('ModalIssue')" value = "Save"/>&nbsp; 
																				<input class="altBtn" id="Button2" title="Cancel" skinid="AlternateLeftButton" onclick="hideModal('ModalIssue')" type="button" value="Cancel"/>
                                                                                <input type="button" id="ClearButton" skinid="AlternateRightButton" class="altBtn" value="Clear" onclick="ResetSelectedItems();" />
																				<input type="button" id="RefreshButton" skinid="AlternateRightButton" class="altBtn" value="Refresh Preview" onclick="refreshPreview();" />
																			</td>
																		</tr>
																	</table>
																</div>
															</td>
														</tr>
													</table>
												</td>
											</tr>
										</table> <!--</div>--></td>
								</tr>
							</table>
						</td>
						<td width="2" height="100%"><IMG height="100%" src="../Navigation/images/body_mid_right.jpg" width="2"/></td>
					</tr>
					<tr>
						<td width="2" height="3"></td>
						<td style="BACKGROUND-REPEAT: repeat" width="100%" background="../Common/images/body_bot_back.jpg"
							height="3"><IMG height="3" src="../Navigation/images/trans_spacer.gif" width="100%"/></td>
						<td width="2" height="3"></td>
					</tr>
				</table>                
			</div>
			<div id="Div3" class="black_overlay">
			</div>
		</div>
		<input id="ModelType" type="hidden" name="ModelType" />
        <input type="hidden" id="calledDialog" name="calledDialog" />
		<input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" designtimedragdrop="261" />
	</div>
	<script language="JavaScript" type="text/javascript">        
        function AddSpecialChar() {
            if(event.keyCode == 9) {
                var SpecialCharTextBox = document.getElementById('SpecialChar');
                var SpecialCharLabel = document.getElementById('SpecialCharLabel');

                if(SpecialCharTextBox.length <= 0)
                    return;

                var to = document.getElementById('MailAddrFormatList');
                var from = document.getElementById('AvailList');

                for (var i = 0; i < from.options.length; i++) {
                    var o = from.options[i];
    
		            if (o.selected) {
		                if (!CheckListItems(to)) { 
                            var index = 0;
                        }
                        else {
                            var index = to.options.length;
                        }

                        // Add to destination list
                        to.options[index] = new Option(o.text + ',' + '[' + SpecialCharTextBox.value + ']', '[' + SpecialCharTextBox.value + ']', false, false);
                        break;
		            }
		        }

                SpecialCharLabel.style.display = 'none';
                SpecialCharTextBox.value = '';
                SpecialCharTextBox.style.display = 'none';
            }
        }

        function handleSpecialChar() {
            if ((document.getElementById('calledDialog').value == 'addrinforeqfields') || (document.getElementById('calledDialog').value == 'coninforeqfields')) {
	            return;
	        }

            var availableList = document.getElementById('AvailList');
            var SpecialCharTextBox = document.getElementById('SpecialChar');
            var SpecialCharLabel = document.getElementById('SpecialCharLabel');

            for (var i = availableList.options.length - 1; i >= 0; i--) {
                var o = availableList.options[i];

                if (o.selected) {
                    if (o.text.indexOf('Special Character') != -1) {
                        SpecialCharLabel.style.display = 'inline';
                        SpecialCharTextBox.style.display = 'block';
                    }
                    else {
                        SpecialCharLabel.style.display = 'none';
                        SpecialCharTextBox.style.display = 'none';
                    }
                    return;
                }
            }
        }

	    function handleOptionalListItem() {
	        if ((document.getElementById('calledDialog').value == 'addrinforeqfields') || (document.getElementById('calledDialog').value == 'coninforeqfields')) {
	            return;
	        }

            var selectedList = document.getElementById('MailAddrFormatList');
            var optionalCheckBox = document.getElementById('chkOptional');

            for (var i = 0; i < selectedList.options.length; i++) {
                var o = selectedList.options[i];

                if (o.selected) {
                    if(o.key == '[\n]' || o.key == '[Space]' || o.text.indexOf('Special') >= 0)
                        return;

                    if (o.text.indexOf('*') != -1) {
                        optionalCheckBox.checked = true;
                    }
                    else {
                        optionalCheckBox.checked = false;
                    }
                    return;
                }
            }
        }

        function handleSelectedListItemForOptional() {
            var selectedList = document.getElementById('MailAddrFormatList');
            var itemSelected = false;
            var optionalCheckBox = document.getElementById('chkOptional');

            // If no item selected
            for (var i = 0; i < selectedList.options.length && itemSelected == false; i++) {
                var o = selectedList.options[i];
                if (o.selected) {
                    itemSelected = true;
                    break;
                }
            }

            if (!itemSelected) {
                return;
            }

            if(optionalCheckBox.checked) {
                // When the check box is checked
                for (var i = 0; i < selectedList.options.length; i++) {
                    var o = selectedList.options[i];

		            if (o.selected && o.value != '[\\n]' && o.value != '[Space]' && o.text.indexOf('Special') < 0) {
                        selectedList.options[i] = new Option(o.text + '*', o.value + '*', false, false);
                        return;
                    }
		        }
		    }
            else {
                // When the check box is unchecked
                for (var i = 0; i < selectedList.options.length; i++) {
                    var o = selectedList.options[i];

		            if (o.selected && o.value != '[\\n]' && o.value != '[Space]' && o.text.indexOf('Special') < 0) {
                        selectedList.options[i] = new Option(o.text.replace("*", ""), o.value.replace("*", ""), false, false);
                        return;
                    }
		        }
            }
	    }

	    function addMailAddressFormatOptions(selectedValues) {
            // Changing Dialog Box Title
            var dialogTitle = document.getElementById('Label1');
            dialogTitle.Value = 'SET_MAIL_ADDRESS_FORMAT';
            // Changing List Box Titles
            var availableFieldsTitle = document.getElementById('lblAvailable');
            availableFieldsTitle.Value = 'AVAILABLE_MAIL_ADDRESS_FIELDS';
            var selectedFieldsTitle = document.getElementById('lblSelected');
            selectedFieldsTitle.Value = 'SELECTED_MAIL_ADDRESS_FIELDS';
            // Populating List Box with available values
	        var newListValues = new Array();
	        newListValues['[ADR1]'] = 'Address1,[ADR1]';
	        newListValues['[ADR2]'] = 'Address2,[ADR2]';
	        newListValues['[ADR3]'] = 'Address3,[ADR3]';
	        newListValues['[CITY]'] = 'City,[CITY]';
	        newListValues['[RGNAME]'] = 'Region Name,[RGNAME]';
	        newListValues['[RGCODE]'] = 'Region Code,[RGCODE]';
	        newListValues['[ZIP]'] = 'PostalCode,[ZIP]';
	        newListValues['[COU]'] = 'Country,[COU]';
	        newListValues['[\\n]'] = 'NewLine,[\\n]';
	        newListValues['[Space]'] = 'Space,[Space]';
	        newListValues['[*]'] = 'Special Character';
            // Populating List Box with Selected values
	        AddNewOptions(newListValues, selectedValues, 'false');
	    }

	    function addAddressReqFieldsOptions(selectedValues) {
            // Changing Dialog Box Title
            var dialogTitle = document.getElementById('Label1');
            dialogTitle.Value = 'SELECT_REQ_ADDRESS_FIELDS';
            // Changing List Box Titles
            var availableFieldsTitle = document.getElementById('lblAvailable');
            availableFieldsTitle.Value = 'AVAILABLE_ADDRESS_FIELDS';
            var selectedFieldsTitle = document.getElementById('lblSelected');
            selectedFieldsTitle.Value = 'SELECTED_ADDRESS_FIELDS';
            // Populating List Box with available values
            var newListValues = new Array();
            newListValues['[SALU]'] = 'Salutation,[SALU]';
            newListValues['[NAME]'] = 'Name,[NAME]';
	        newListValues['[ADR1]'] = 'Address1,[ADR1]';
	        newListValues['[ADR2]'] = 'Address2,[ADR2]';
	        newListValues['[ADR3]'] = 'Address3,[ADR3]';
	        newListValues['[CITY]'] = 'City,[CITY]';
	        newListValues['[ZIP]'] = 'PostalCode,[ZIP]';
	        newListValues['[PRO]'] = 'State / Province,[PRO]';
	        newListValues['[COU]'] = 'Country,[COU]';
	        newListValues['[EMAIL]'] = 'Email,[EMAIL]';
	        newListValues['[WPHONE]'] = 'Work Phone,[WPHONE]';
	        newListValues['[HPHONE]'] = 'Home Phone,[HPHONE]';
            newListValues['[RGN]'] = 'Region,[RGN]';

            // Populating List Box with Selected values
	        AddNewOptions(newListValues, selectedValues, 'true');
	    }

	    function addContactOptions(selectedValues){
            // Changing Dialog Box Title
            var dialogTitle = document.getElementById('Label1');
            dialogTitle.Value = 'SELECT_CONTACT_FIELDS';
            var availableFieldsTitle = document.getElementById('lblAvailable');
            availableFieldsTitle.Value = 'AVAILABLE_CONTACT_FIELDS';
            var selectedFieldsTitle = document.getElementById('lblSelected');
            selectedFieldsTitle.Value = 'SELECTED_CONTACT_FIELDS';
            // Populating List Box with available values
	        var newListValues = new Array();
            newListValues['[SALU]'] = 'Salutation,[SALU]';
            newListValues['[NAME]'] = 'Name,[NAME]';
            newListValues['[HPHONE]'] = 'Home Phone,[HPHONE]';
            newListValues['[WPHONE]'] = 'Work Phone,[WPHONE]';
            newListValues['[CPHONE]'] = 'Cell Phone,[CPHONE]';
            newListValues['[EMAIL]'] = 'Email,[EMAIL]';
            newListValues['[ADR1]'] = 'Address1,[ADR1]';
            newListValues['[ADR2]'] = 'Address2,[ADR2]';
            newListValues['[ADR3]'] = 'Address3,[ADR3]';
            newListValues['[RGN]'] = 'Region,[REGION]';
            newListValues['[CITY]'] = 'City,[CITY]';
            newListValues['[ZIP]'] = 'Postal Code,[ZIP]';
            // Populating List Box with Selected values
	        AddNewOptions(newListValues, selectedValues, 'true');
	    }

	    function AddNewOptions(newListValues, selectedValues, removeSelectedFromAvailable) {
	        var masterList = document.getElementById('AvailList');
	        var selectedList = document.getElementById('MailAddrFormatList');
            var formattedSelectedValues = replaceAll('][', '],[', selectedValues);
            formattedSelectedValues = replaceAll(']*[', ']*,[', formattedSelectedValues);
            var selectedValuesList = new Array();

            if (formattedSelectedValues.indexOf(',') >= 0)
                selectedValuesList = formattedSelectedValues.split(',');
            else if (formattedSelectedValues.length > 0)
                selectedValuesList.push(formattedSelectedValues);

	        masterList.options.length = 0;
	        selectedList.options.length = 0;

            var index = 0;
	        var existingIndex = 0;
            var searchedItem = '';

            for(var key in newListValues) {
                masterList.options[index] = new Option(newListValues[key], key, false, false);
                index++;
            }

            for (var i = 0; i < selectedValuesList.length; i++) {
                searchedItem = '';
                for(var key in newListValues) {
                    if (key == selectedValuesList[i].replace('*','')) {
                        searchedItem = newListValues[key];
                        break;
                    }
                }

                if(searchedItem != '') {
                    if(selectedValuesList[i].indexOf('*') >= 0) {
                        selectedList.options[existingIndex] = new Option(newListValues[key] + '*', key + '*', false, false);
                    }
                    else {
                        selectedList.options[existingIndex] = new Option(newListValues[key], key, false, false);
                    }

                    if(removeSelectedFromAvailable == 'true' && key != '[\n]' && key != '[Space]') {
                        for (var j = 0; j < masterList.options.length; j++) {
                            if(masterList.options[j].text == newListValues[key]) {
                                masterList.options[j] = null;
                                break;
                            }
                        }
                    }
                }
                else {
                    selectedList.options[existingIndex] = new Option(newListValues[key] + ',' + selectedValuesList[i], selectedValuesList[i], false, false);
                }
                existingIndex++;
	        }
	    }

	    function showHide(isShow) {
	        var sStyle = '';
	        var sInline = '';

	        if (isShow == true) {
	            sStyle = 'block';
	            sInline = 'inline';
	        }
	        else {
	            sStyle = 'none';
	            sInline = 'none';
	        }

	        document.getElementById('<%=PreviewList.ClientID %>').style.display = sStyle;
	        document.getElementById('<%=TextBox2.ClientID %>').style.display = sStyle;
	        document.getElementById('<%=Label3.ClientID %>').style.display = sInline;
	        document.getElementById('RefreshButton').style.display = sInline;
	        document.getElementById('chkOptional').style.display = sInline;
	        document.getElementById('chkText').style.display = sInline;
	        document.getElementById('btnMoveUp').style.display = sStyle;
	        document.getElementById('btnMoveDown').style.display = sStyle;
	    }

	    function RevealModalWithMessage(divId, module) {
	        debugger;
		    document.all('ModelType').value = module;
		    ClearSelectedItems();

			switch (module) {
			    case '1':
                    document.getElementById('calledDialog').value = 'addrinforeqfields';
			        addAddressReqFieldsOptions(document.all('<%=TextboxAddrInfoReqFields.ClientID %>').value);
			        showHide(false);
			        break;
			    case '2':
                    document.getElementById('calledDialog').value = 'mailaddrformat';
			        //document.getElementById(divId).style.height = '400px';
			        addMailAddressFormatOptions(document.all('<%=TextboxMailAddrFormat.ClientID %>').value);
			        showHide(true);
			        break;
			    case '3':
                    document.getElementById('calledDialog').value = 'coninforeqfields';
			        addContactOptions(document.all('<%=TextboxCONTACT_INFO_REQ_FIELDS.ClientID %>').value);
			        showHide(false);
			        break;
			}

			revealModal(divId);
			return false;
		}

		function SubmitData(divId) {
			switch (document.all('ModelType').value) {
			    case '1':
			        document.all('<%=TextboxAddrInfoReqFields.ClientID %>').value = getSelectedItems();
			        break;
			    case '2':
			        document.all('<%=TextboxMailAddrFormat.ClientID %>').value = getSelectedItems();
			        break;
			    case '3':
			        document.all('<%=TextboxCONTACT_INFO_REQ_FIELDS.ClientID %>').value = getSelectedItems();
			        break;
			}

			hideModal(divId);
        }

        function replaceAll(find, replace, str) 
        {
            while( str.indexOf(find) > 0 )
            {
                str = str.replace(find, replace);
            }
            return str;
        }

        function refreshPreview() {
            var text2 = document.all('<%=TextBox2.ClientID %>');

            if (text2 != null) {
                text2.value = '';
            }

            var previewList = document.all('<%=PreviewList.ClientID %>');
            previewList.options.length = 0;
            var formatList = document.all('MailAddrFormatList');
            var masterList = document.all('AvailList');

            for (var i = 0; i < formatList.options.length; i++) {
                var o = formatList.options[i];
                var strToken = o.value.replace("*", "");
                var listOption = null;

                for (var j = 0; j < masterList.options.length; j++) {
                    var mo = masterList.options[j];
                    if (mo.value == strToken) {
                        listOption = mo;
                        break;
                    }
                }

                if (listOption == null) {
                    var spChar = strToken.replace("[", "").replace("]", "");
                    if (spChar.length == 1) {
                        addValueToDisplay(spChar, strToken);
                    }
                    else {
                        if (text2 != null) {
                            text2.value = '';
                        }

                        var previewList = document.all('<%=PreviewList.ClientID %>');
                        previewList.options.length = 0;
                        addValueToDisplay("invalid token" + strToken, strToken);
                    }
                }
                else {
                    var valuesArray = listOption.text.split(',');
                    addValueToDisplay(valuesArray[0], listOption.value);
                }
            }
        }

        function addValueToDisplay(displayValue, tokenValue) {
            if (tokenValue == '[Space]') {
                displayValue = ' ';
            }

            var text2 = document.all('<%=TextBox2.ClientID %>');
            var previewList = document.all('<%=PreviewList.ClientID %>');
            var count = previewList.options.length - 1;

            if (count < 0) count = 0;

            if (tokenValue != '[\\n]') {
                if (text2 != null) {
                    text2.value = text2.value + displayValue;
                    previewList.options[count] = new Option(text2.value, text2.value, false, false);
                }
            }
            else {
                if (text2 != null) {
                    text2.value = '';
                }
                previewList.options.add(new Option(text2.value, text2.value, false, false));
            }
        }

        function ClearSelectedItems() {
            var to = document.getElementById('MailAddrFormatList');
            to.options.length = 0;
        }

        function CheckListItems(listInput) {
            if (listInput != null && listInput.options != null) { return true; }
		    return false;
		}

		function addItem() {
		    var from = document.getElementById('AvailList');
		    var to = document.getElementById('MailAddrFormatList');
            var moveItem = 'false';

		    if (!CheckListItems(from)) { return; }

            if((document.getElementById('calledDialog').value == 'addrinforeqfields') || (document.getElementById('calledDialog').value == 'coninforeqfields')) {
                moveItem = 'true';
            }
            else {
                moveItem = 'false';
            }

		    for (var i = 0; i < from.options.length; i++) {

		        var o = from.options[i];

		        if (o.selected) {
		            if (!CheckListItems(to)) { 
                        var index = 0;
                    }
                    else {
                        var index = to.options.length;
                    }

                    // Add to destination list
                    if(o.text.indexOf('Special') < 0)
                        to.options[index] = new Option(o.text, o.value, false, false);

                    if(moveItem == 'true' && o.value != '[\n]' && o.value != '[Space]' && o.text.indexOf('Special') < 0) { 
                        // Remove from source list
                        from.options[i] = null;
                    }

                    break;
		        }
		    }

		    from.selectedIndex = -1;
		    to.selectedIndex = -1;
		}

		function removeItem() {
		    var from = document.getElementById('MailAddrFormatList');
            var to = document.getElementById('AvailList');

		    if (!CheckListItems(from)) { return; }

		    for (var i = (from.options.length - 1); i >= 0; i--) {
		        var o = from.options[i];

		        if (o.selected) {
                    if (!CheckListItems(to)) { 
                        var index = 0;
                    }
                    else {
                        var index = to.options.length;
                    }

                    // Add to destination list
                    if(o.value != '[\n]' && o.value != '[Space]' && o.text.indexOf('Special') < 0)
                        to.options[index] = new Option(o.text, o.value, false, false);

                    // Remove from source list
		            from.options[i] = null;

                    break;
		        }
		    }

		    from.selectedIndex = -1;
            to.selectedIndex = -1;
		}

		function swapOptions(obj, i, j) {
		    var o = obj.options;
		    var i_selected = o[i].selected;
		    var j_selected = o[j].selected;
		    var temp = new Option(o[i].text, o[i].value, o[i].defaultSelected, o[i].selected);
		    var temp2 = new Option(o[j].text, o[j].value, o[j].defaultSelected, o[j].selected);
		    o[i] = temp2;
		    o[j] = temp;
		    o[i].selected = j_selected;
		    o[j].selected = i_selected;
		}

		function moveItemUp() {
		    var obj = document.getElementById('MailAddrFormatList');

		    if (!CheckListItems(obj)) { return; }

		    for (i = 0; i < obj.options.length; i++) {
		        if (obj.options[i].selected) {
		            if (i != 0 && !obj.options[i - 1].selected) {
		                swapOptions(obj, i, i - 1);
		                obj.options[i - 1].selected = true;
		            }
                    break;
		        }
		    }
		}

		function moveItemDown() {
		    var obj = document.getElementById('MailAddrFormatList');

		    if (!CheckListItems(obj)) { return; }

		    for (i = obj.options.length - 1; i >= 0; i--) {
		        if (obj.options[i].selected) {
		            if (i != (obj.options.length - 1) && !obj.options[i + 1].selected) {
		                swapOptions(obj, i, i + 1);
		                obj.options[i + 1].selected = true;
		            }
                    break;
		        }
		    }
		}

		function getSelectedItems() {
		    var selectedList = '';
		    var obj = document.getElementById('MailAddrFormatList');

		    if (!CheckListItems(obj)) { return; }

		    for (i = 0; i < obj.options.length; i++) {
		        selectedList = selectedList + obj.options[i].value;
		    }

		    return selectedList;
		}

		function ResetSelectedItems() {
		    var to = document.getElementById('MailAddrFormatList');
		    to.options.length = 0;

		    switch (document.getElementById('calledDialog').value) {
		        case 'addrinforeqfields':
		            var newListValues = new Array();
		            newListValues['[SALU]'] = 'Salutation,[SALU]';
		            newListValues['[NAME]'] = 'Name,[NAME]';
		            newListValues['[ADR1]'] = 'Address1,[ADR1]';
		            newListValues['[ADR2]'] = 'Address2,[ADR2]';
		            newListValues['[ADR3]'] = 'Address3,[ADR3]';
		            newListValues['[CITY]'] = 'City,[CITY]';
		            newListValues['[ZIP]'] = 'PostalCode,[ZIP]';
		            newListValues['[PRO]'] = 'State / Province,[PRO]';
		            newListValues['[COU]'] = 'Country,[COU]';
		            newListValues['[EMAIL]'] = 'Email,[EMAIL]';
		            newListValues['[WPHONE]'] = 'Work Phone,[WPHONE]';
		            newListValues['[HPHONE]'] = 'Home Phone,[HPHONE]';
		            newListValues['[RGN]'] = 'Region,[RGN]';
		            // Populating List Box with Selected values
		            AddNewOptions(newListValues, '', 'true');
		            break;
		        case 'mailaddrformat':
		            var newListValues = new Array();
		            newListValues['[ADR1]'] = 'Address1,[ADR1]';
		            newListValues['[ADR2]'] = 'Address2,[ADR2]';
		            newListValues['[ADR3]'] = 'Address3,[ADR3]';
		            newListValues['[CITY]'] = 'City,[CITY]';
		            newListValues['[RGNAME]'] = 'Region Name,[RGNAME]';
		            newListValues['[RGCODE]'] = 'Region Code,[RGCODE]';
		            newListValues['[ZIP]'] = 'PostalCode,[ZIP]';
		            newListValues['[COU]'] = 'Country,[COU]';
		            newListValues['[\\n]'] = 'NewLine,[\\n]';
		            newListValues['[Space]'] = 'Space,[Space]';
		            newListValues['[*]'] = 'Special Character';
		            // Populating List Box with Selected values
		            AddNewOptions(newListValues, '', 'false');
		            break;
		        case 'coninforeqfields':
		            var newListValues = new Array();
		            newListValues['[SALU]'] = 'Salutation,[SALU]';
		            newListValues['[NAME]'] = 'Name,[NAME]';
		            newListValues['[HPHONE]'] = 'Home Phone,[HPHONE]';
		            newListValues['[WPHONE]'] = 'Work Phone,[WPHONE]';
		            newListValues['[CPHONE]'] = 'Cell Phone,[CPHONE]';
		            newListValues['[EMAIL]'] = 'Email,[EMAIL]';
		            newListValues['[ADR1]'] = 'Address1,[ADR1]';
		            newListValues['[ADR2]'] = 'Address2,[ADR2]';
		            newListValues['[ADR3]'] = 'Address3,[ADR3]';
		            newListValues['[RGN]'] = 'Region,[REGION]';
		            newListValues['[CITY]'] = 'City,[CITY]';
		            newListValues['[ZIP]'] = 'Postal Code,[ZIP]';
		            // Populating List Box with Selected values
		            AddNewOptions(newListValues, selectedValues, 'true');
		            break;
		    }
		}

		var Yes_Guid = '<%=Me.State.yesId%>';
		function ShowHideRegulatoryExtractDate() {

		    var objS = event.srcElement
		    var val = objS.options[objS.selectedIndex].value

		    var objLbl = document.getElementById('<%=LabelLastRegulatoryExtractDate.ClientID%>');
		    var objTxt = document.getElementById('<%=TextboxLastRegulatoryExtractDate.ClientID%>');

		    //alert("valu=" + val + "  objLbl=" + objLbl + " objTxt=" + objTxt + " YesGuid=" + Yes_Guid)
            if (val == Yes_Guid) {
                objLbl.style.display = 'block';
                objTxt.style.display = 'inline';
            }
            else {
                objLbl.style.display = 'none';
                objTxt.style.display = 'none';
            }


        }
        var Yes_String = '<%=Me.State.yesCode%>';
	    function ShowHideAllowForceAddressANDAddressConfidenceThreshold() {

		    var objS = event.srcElement
		    var val = objS.options[objS.selectedIndex].value

		    var objLblAFA = document.getElementById('<%=lblAllowForceAddress.ClientID%>');
            var objTxtAFA = document.getElementById('<%=cboAllowForceAddress.ClientID%>');

            var objLblACT = document.getElementById('<%=lblAddressConfidenceThreshold.ClientID%>');
            var objTxtACT = document.getElementById('<%=TextboxAddressConfidenceThreshold.ClientID%>');

            //alert("valu=" + val + " YesString=" + Yes_String)
            if (val == Yes_String) {
                objLblAFA.style.display = 'block';
                objTxtAFA.style.display = 'inline';
                objLblACT.style.display = 'block';
                objTxtACT.style.display = 'inline';
            }
            else
            {
                objLblAFA.style.display = 'none';
                objTxtAFA.style.display = 'none';
                objLblACT.style.display = 'none';
                objTxtACT.style.display = 'none';
            }
        }
	</script>
	<div class="btnZone">
		<asp:Button ID="btnSave_WRITE" runat="server" Text="Save" CausesValidation="False"
			SkinID="PrimaryRightButton"></asp:Button>
        <asp:Button ID="btnUndo_Write" runat="server" Text="Undo" CausesValidation="False"
			SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="False"
			SkinID="AlternateLeftButton"></asp:Button>
		<asp:Button ID="btnNew_WRITE" runat="server" Text="New" CausesValidation="False"
			SkinID="AlternateLeftButton"></asp:Button>
		<asp:Button ID="btnCopy_WRITE" runat="server" Text="NEW_WITH_COPY" CausesValidation="False"
			SkinID="AlternateRightButton"></asp:Button>
		<asp:Button ID="btnDelete_WRITE" runat="server" Text="Delete" CausesValidation="False"
			SkinID="CenterButton"></asp:Button>
	</div>
</asp:Content>
