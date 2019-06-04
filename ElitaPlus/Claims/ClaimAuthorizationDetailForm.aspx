<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimAuthorizationDetailForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimAuthorizationDetailForm" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAuthorizationInfo" Src="UserControlAuthorizationInfo.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlSelectServiceCenter" Src="~/Certificates/UserControlSelectServiceCenter.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table border="0" class="summaryGrid" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right" nowrap="nowrap">
                <asp:Label ID="lblClaimNumber" runat="server" SkinID="SummaryLabel">CLAIM_#</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight" width="160px">
                <asp:Label ID="lblClaimNumberValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblDateOfLoss" runat="server" SkinID="SummaryLabel">DATE_OF_LOSS</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight" width="160px">
                <asp:Label ID="lblDateOfLossValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">  
                <asp:Label ID="lblClaimStatus" runat="server" SkinID="SummaryLabel">CLAIM_STATUS</asp:Label>:
            </td>
            <td id="ClaimStatusTD" runat="server" align="left" nowrap="nowrap" class="padRight">
                <asp:Label ID="lblClaimStatusValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" nowrap="nowrap">
                <asp:Label ID="lblClaimAuthNumber" runat="server" SkinID="SummaryLabel">CLAIM_AUTHORIZATION_NUMBER</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight">
                <asp:Label ID="lblClaimAuthNumberValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblClaimAuthStatus" runat="server" SkinID="SummaryLabel">CLAIM_AUTH_STATUS</asp:Label>:
            </td>
            <td id="ClaimAuthStatusTD" runat="server" align="left" nowrap="nowrap" class="bor padRight">
                <asp:Label ID="lblClaimAuthStatusValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblServiceCenter" runat="server" SkinID="SummaryLabel">SERVICE_CENTER</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="padRight">
                <asp:Label ID="lblServiceCenterValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" nowrap="nowrap">
                <asp:Label ID="lblClaimAuthFulfillmentType" runat="server" SkinID="SummaryLabel">CLAIM_AUTHORIZATION_FULFILLMENT_TYPE</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight">
                <asp:Label ID="lblClaimAuthFulfillmentTypeValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label>
            </td>
            <td colspan="4"></td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManagerMaster" runat="server" />
    <div id="ModalServiceCenter" class="overlay">
        <div id="light" class="overlay_message_content" style="left: 5%; right: 5%; top: 5%; max-height: 80%">
            <p class="modalTitle">
                <asp:Label ID="lblModalTitle" runat="server" Text="SEARCH_SERVICE_CENTER"></asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('ModalServiceCenter');">
                    <img id="Img1" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                        width="16" height="18" align="absmiddle" class="floatR" /></a>
            </p>
            <Elita:UserControlSelectServiceCenter ID="ucSelectServiceCenter" runat="server" />
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>

    <div class="dataContainer" id="dvClaimAuthorizationDetails" runat="server">
        <div class="dataContainer">
            <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
            <div id="tabs" class="style-tabs">
                <ul>
                    <li><a href="#tabsClaimAuthItemDetail">
                        <asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">CLAIM_AUTHORIZATION_ITEM</asp:Label></a></li>
                    <li><a href="#tabsClaimAuthFulfillmentIssues">
                        <asp:Label ID="Label7" runat="server" CssClass="tabHeaderText">CLAIM_AUTHORIZATION_FULFILLMENT_ISSUE</asp:Label></a></li>
                    <li><a href="#tabsClaimAuthStatusHistory">
                        <asp:Label ID="Label8" runat="server" CssClass="tabHeaderText">CLAIM_AUTHORIZATION_STATUS_HISTORY</asp:Label></a></li>
                </ul>
                <div id="tabsClaimAuthItemDetail">
                    <Elita:UserControlAuthorizationInfo ID="ucClaimAuthItemDetail" runat="server"></Elita:UserControlAuthorizationInfo>
                </div>
                <div id="tabsClaimAuthFulfillmentIssues">
                    <div id="ClaimAuthFulfillmentIssues" align="center">
                        <asp:GridView ID="GridViewClaimAuthFulfillmentIssues" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="false"
                            SkinID="DetailPageGridView" AllowSorting="false">
                            <SelectedRowStyle Wrap="True" />
                            <EditRowStyle Wrap="True" />
                            <AlternatingRowStyle Wrap="True" />
                            <RowStyle Wrap="True" />
                            <Columns>
                                <asp:BoundField DataField="IssueId" Visible="false" />
                                <asp:BoundField DataField="IssueDescription" HeaderText="IssueDescription" />
                                <asp:BoundField DataField="IssueStatusDate" HeaderText="IssueStatusDate" />
                                <asp:BoundField DataField="IssueStatusDescription" HeaderText="IssueStatus" />
                                <asp:TemplateField HeaderText="IssueActionDescription">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonIssueActionDescription" runat="server" CommandName="SelectAction" CommandArgument="<%#Container.DisplayIndex %>"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle />
                        </asp:GridView>
                    </div>
                </div>
                <div id="tabsClaimAuthStatusHistory">
                    <div id="ClaimAuthStatusHistory" align="center">
                        <asp:GridView ID="GridClaimAuthStatusHistory" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="false"
                            SkinID="DetailPageGridView" AllowSorting="false">
                            <SelectedRowStyle Wrap="True" />
                            <EditRowStyle Wrap="True" />
                            <AlternatingRowStyle Wrap="True" />
                            <RowStyle Wrap="True" />
                            <Columns>
                                <asp:BoundField DataField="StatusDescription" HeaderText="StatusDescription" />
                                <asp:BoundField DataField="SubStatusDescription" HeaderText="SubStatusDescription" />
                                <asp:BoundField DataField="SubStatusDate" HeaderText="SubStatusDate" />
                                <asp:BoundField DataField="SubStatusReasonDescription" HeaderText="SubStatusReasonDescription" />
                            </Columns>
                            <PagerStyle />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="LabelSource" runat="server">Source</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="TextboxSource" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="LabelVisitDate" runat="server">Visit_Date</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="TextboxVisitDate" TabIndex="2" runat="server" ReadOnly="true" SkinID="MediumTextBox"></asp:TextBox>
                            <asp:ImageButton ID="ImageButtonVisitDate" TabIndex="2" runat="server" Visible="True"
                                ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="LabelRepairDate" runat="server">Repair_Date</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="TextboxRepairDate" TabIndex="2" runat="server" SkinID="MediumTextBox"
                                ReadOnly="true"></asp:TextBox>
                            <asp:ImageButton ID="ImageButtonRepairDate" TabIndex="2" runat="server" Visible="True"
                                ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="LabelInvoiceDate" runat="server">INVOICE_DATE</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="TextboxInvoiceDate" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="LabelBatchNumber" runat="server">BATCH_NUMBER</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="TextboxBatchNumber" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="LabelPickUpDate" runat="server">PickUp_Date</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="TextboxPickupDate" TabIndex="2" runat="server" SkinID="MediumTextBox"
                                ReadOnly="true"></asp:TextBox>
                            <asp:ImageButton ID="ImageButtonPickupDate" TabIndex="2" runat="server" Visible="True"
                                ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="LabelDefectReason" runat="server">DEFECT_REASON</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="TextboxDefectReason" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="LabelExpectedRepairDate" runat="server">EXPECTED_REPAIR_DATE</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="TextboxExpectedRepairDate" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="LabelSCReferenceNumber" runat="server">SVC_REFERENCE_NUMBER</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="TextboxSCReferenceNumber" TabIndex="-1" runat="server" SkinID="MediumTextBox" ReadOnly="True"></asp:TextBox>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="LabelVerificationNumber" runat="server">VERIFICATION_NUMBER</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="TextboxVerificationNumber" TabIndex="-1" runat="server" SkinID="MediumTextBox" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="LabelWhoPays" runat="server">Who_Pays</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="cboWhoPays" TabIndex="1" runat="server" SkinID="MediumDropDown"
                                Enabled="false">
                            </asp:DropDownList>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblCashPaymemtMethod" runat="server">CASH_PAYMENT_METHOD:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="txtCashPaymentMethod" TabIndex="-1" runat="server" SkinID="MediumTextBox" ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>                        
                        <td nowrap="nowrap" align="right" width="15%">
                            <asp:Label ID="LabelTechnicalReport" runat="server">TECHNICAL_REPORT</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="TextboxTechnicalReport" TabIndex="4" runat="server" SkinID="LargeTextBox"
                                TextMode="MultiLine" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td nowrap="nowrap" align="right" width="15%">
                            <asp:Label ID="LabelSpecialInstruction" runat="server">SPECIAL_INSTRUCTIONS</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="TextboxSpecialInstruction" runat="server" TextMode="MultiLine" Wrap="True" style="white-space: pre-wrap;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblAuthTypeXcd" runat="server">AUTH_TYPE</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="cboAuthTypeXcd" TabIndex="1" runat="server" SkinID="MediumDropDown"
                                Enabled="false">
                            </asp:DropDownList>
                        </td>

                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblPartyTypeXcd" runat="server">PARTY_TYPE</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="cboPartyTypeXcd" TabIndex="1" runat="server" SkinID="MediumDropDown"
                                Enabled="false">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblPartyReference" runat="server">PARTY_REFERENCE</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="txtPartyReference" TabIndex="-1" runat="server" SkinID="MediumTextBox"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
     <div id="ModalReshipment" class="overlay">
        
          <div id="light" class="overlay_message_content" style="align-content:center; max-height: 80%;width:80%">
            <p class="modalTitle">
                <asp:Label ID="Label1" runat="server" Text="Reshipment"></asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('ModalReshipment');">
                    <img id="Img2" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                        width="16" height="18" align="absmiddle" class="floatR" /></a>
            </p>
           <table style="width:40%;text-align:center" class="formGrid" >
                 <tr>                        
                        <td nowrap="nowrap" width="15%">
                            <asp:Label ID="Label2" runat="server">RESHIPMENT_REASON</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                             <asp:DropDownList ID="reshipmentReasonDrop" TabIndex="1" runat="server" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                       
                    </tr>
               
               
           </table>
            <table class="formGrid"  style="width:80%;text-align:center" border="0" cellpadding="0" cellspacing="0">
                <tbody>
                    
                </tbody>
            </table>
              <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label ID="lblDeviceSearchResults" runat="server" Text="SELECT_LIKE_FOR_LIKE_DEVICE" Visible="false" ></asp:Label>
        </h2>
      <div style="width: 100%">
                    <table id="tblDeviceSelection" class="dataGrid" border="0" rules="cols" width="100%">
                        <tr>
                            <td align="center">
                                <asp:GridView ID="GridViewDeviceSelection" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                                    SkinID="DetailPageGridView" AllowSorting="false">
                                    <SelectedRowStyle Wrap="True" />
                                    <EditRowStyle Wrap="True" />
                                    <AlternatingRowStyle Wrap="True" />
                                    <RowStyle Wrap="True" />
                                    <Columns>
                                        <asp:BoundField DataField="Make" HeaderText="Make" />
                                        <asp:BoundField DataField="Model" HeaderText="Model" />
                                        <asp:BoundField DataField="Color" HeaderText="Color" />
                                        <asp:BoundField DataField="Memory" HeaderText="Memory" />
                                        <asp:BoundField DataField="VendorSku" HeaderText="VendorSku" />
                                        <asp:BoundField DataField="InventoryQuantity" HeaderText="Quantity" />
                                        <asp:TemplateField HeaderText="select_device">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdoDevice" runat="server"  Visible="True" Checked="true" AutoPostBack="true"
                                                     OnCheckedChanged="rdoDevice_CheckedChanged" />   
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                </div>

            <div class="dataContainer">
                <h2 class="dataGridHeader">
                    <asp:Label ID="lblBestReplacementDevice" runat="server" Visible="false" Text="SELECT_BEST_REPLACEMENT_DEVICE"></asp:Label></h2>
                <div style="width: 100%">
                    <table id="tblDeviceSelection1" class="dataGrid" border="0" rules="cols" width="100%">
                        <tr>
                            <td align="center">
                                <asp:GridView ID="GridViewBestDeviceSelection" runat="server" Width="100%" AutoGenerateColumns="False" 
                                    SkinID="DetailPageGridView" AllowSorting="false">
                                    <SelectedRowStyle Wrap="True" />
                                    <EditRowStyle Wrap="True" />
                                    <AlternatingRowStyle Wrap="True" />
                                    <RowStyle Wrap="True" />
                                    <Columns>
                                      <asp:BoundField DataField="Make" HeaderText="Make" />
                                        <asp:BoundField DataField="Model" HeaderText="Model" />
                                        <asp:BoundField DataField="Color" HeaderText="Color" />
                                        <asp:BoundField DataField="Memory" HeaderText="Memory" />
                                        <asp:BoundField DataField="VendorSku" HeaderText="VendorSku" />
                                        <asp:BoundField DataField="InventoryQuantity" HeaderText="Quantity" />
                                        <asp:TemplateField HeaderText="select_device">
                                            <ItemTemplate>
                                                <asp:RadioButton ID="rdoDevice" runat="server" GroupName="rdo"  Visible="True" AutoPostBack="true" 
                                                     OnCheckedChanged="rdoDevice_CheckedChanged1" Checked="false"></asp:RadioButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>          
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
              <table style="width:50%;text-align:center" class="formGrid">
                  
               <tr>
                        <td runat="server" id="tdClearButton">
                   
                        </td>
                        <td runat="server" id="tdSearchButton" align="right">
                            <asp:Button ID="btnReshipmentProceed" runat="server" Text="Process" SkinID="SearchButton"></asp:Button>&nbsp
                            <asp:Button ID="btnReshipmentCancel" runat="server"  Text="Cancel" SkinID="AlternateRightButton"></asp:Button>
                        </td>
                    </tr>

              </table>
        </div>
        <div id="fade" class="black_overlay">
        </div>      
         </div>
    
<div id="ModalRefundFee" class="overlay">
    <div id="light" class="overlay_message_content" style="width: 540px">
       <p class="modalTitle">
            <asp:Label ID="Label3" Text="REFUND_FEE" runat="server"></asp:Label>
            <a href="javascript:void(0)" onclick="hideModal('ModalRefundFee');">
                <img id="Img9" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                     width="16" height="18" align="absmiddle" class="floatR" /></a>
        </p>
        <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <span class="mandatory">*</span><asp:Label ID="lblRefundReason" runat="server">Refund_Reason</asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="cboRefundReason" runat="server" SkinID="MediumDropDown"
                                      AutoPostBack="false">
                    </asp:DropDownList>
                </td>
            </tr>            
        </table>
        <div class="btnZone">
            <asp:Button ID="btnRefundFeeSave" runat="server" Text="SAVE" SkinID="PrimaryRightButton"></asp:Button>
        </div>
    </div>
    <div id="fade" class="black_overlay">
    </div>
</div>
    

    <div class="btnZone">
        <div>
            <asp:Button ID="btnSave_WRITE" TabIndex="5" runat="server" SkinID="PrimaryRightButton"
                Text="Save" />
            <asp:Button ID="btnEdit_WRITE" TabIndex="5" runat="server" SkinID="PrimaryRightButton"
                Text="Edit" />&nbsp;
            <asp:LinkButton ID="btnUndo_Write" TabIndex="5" runat="server" SkinID="AlternateRightButton"
                Text="Undo" />
            <asp:Button ID="btnBack" SkinID="AlternateLeftButton" TabIndex="5" runat="server"
                Text="Back" />
            <asp:Button ID="ActionButton" runat="server" TabIndex="5" SkinID="ActionButton" Text="LabelActionButtonMenu" />
            <ajaxToolkit:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="ActionButton"
                PopupControlID="PanButtonsHidden" PopupPosition="top" PopDelay="25" HoverCssClass="popupBtnHover">
            </ajaxToolkit:HoverMenuExtender>
            <asp:Panel ID="PanButtonsHidden" runat="server" SkinID="PopUpMenuPanel">
                <asp:Button ID="btnRefundFee" runat="server" Text="Refund_Fee" SkinID="PopMenuButton" />
                <asp:Button ID="btnServiceCenterInfo" runat="server" Text="Center_Info" SkinID="PopMenuButton" />
                 <asp:Button ID="btnCancelShipment" runat="server" Text="Cancel_Shipment" SkinID="PopMenuButton"  />
                <asp:Button ID="btnNewServiceCenter" runat="server" Text="New_Center" SkinID="PopMenuButton" />
                <asp:Button ID="btnClaimAuthHistory" runat="server" Text="History" SkinID="PopMenuButton" />
                <asp:Button ID="btnPrint" runat="server" Text="SO_PRINT" SkinID="PopMenuButton" />
                 <asp:Button ID="btnReshipment" runat="server" Text="RESHIPMENT" SkinID="PopMenuButton" />
                <asp:Button ID="btnPayCash" runat="server" Text="PAY_CASH" SkinID="PopMenuButton" />
            </asp:Panel>
        </div>
    </div>
    <input type="hidden" id="HiddenSaveChangesPromptResponse" runat="server" />
</asp:Content>
