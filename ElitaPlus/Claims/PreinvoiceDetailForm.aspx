<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PreinvoiceDetailForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PreinvoiceDetailForm" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style type="text/css">
        .dataEditBox
        {
            border-left: 10px solid #0066CC;
        }
    </style>
    <script language="JavaScript" type="text/javascript">
        window.latestClick = '';

        function isNotDblClick() {
            if (window.latestClick != "clicked") {
                window.latestClick = "clicked";
                return true;
            } else {
                return false;
            }
        }

        function comboSelectedMasterCenter(source, eventArgs) {
            //put the selected value in a hidden textbox - runat server so you can read it on postback
            // alert( " Key : "+ eventArgs.get_text() +"  Value :  "+eventArgs.get_value());
            var inpId = document.getElementById('<%=inpMasterCenterId.ClientID%>');
            var inpDesc = document.getElementById('<%=inpMasterCenterDesc.ClientID%>');
            inpId.value = eventArgs.get_value();
            inpDesc.value = eventArgs.get_text();
        }

        function comboSelectedServiceCenter(source, eventArgs) {
            //put the selected value in a hidden textbox - runat server so you can read it on postback
            // alert( " Key : "+ eventArgs.get_text() +"  Value :  "+eventArgs.get_value());
            var inpId = document.getElementById('<%=inpServiceCenterId.ClientID%>');
            var inpDesc = document.getElementById('<%=inpServiceCenerDesc.ClientID%>');
            inpId.value = eventArgs.get_value();
            inpDesc.value = eventArgs.get_text();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <div class="dataEditBox">
        <table border="0" class="formGrid" cellpadding="0" cellspacing="0" width="70%">
            <tr runat="server" id="trCompany">
                <td nowrap="nowrap">
                    <asp:Label ID="Label1" runat="server" Text="COMPANY_CODE"></asp:Label>:
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="txtCompanyCode" TabIndex="-1" runat="server" SkinID="SmallTextBox"
                        ReadOnly="True"></asp:TextBox>
                </td>
                <td nowrap="nowrap">
                    <asp:Label ID="Label2" runat="server" Text="COMPANY_DESCRIPTION"></asp:Label>:
                </td>
                <td>
                    <asp:TextBox ID="txtCompanyDesc" TabIndex="-1" runat="server" SkinID="SmallTextBox"
                        ReadOnly="True"></asp:TextBox>
                </td>
                <td nowrap="nowrap">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap">
                    <asp:Label ID="lblBatchNumber" runat="server" Text="BATCH_NUMBER"></asp:Label>:
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="txtBatchNumber" TabIndex="-1" runat="server" SkinID="SmallTextBox"
                        ReadOnly="True"></asp:TextBox>
                </td>
                <td nowrap="nowrap">
                    <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>:
                </td>
                <td>
                    <asp:TextBox ID="txtStatus" TabIndex="-1" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                </td>
                <td nowrap="nowrap">
                    <asp:Label ID="lblCreatedDate" runat="server" Text="CREATED_DATE"></asp:Label>:
                </td>
                <td>
                    <asp:TextBox ID="txtCreatedDate" TabIndex="-1" runat="server" SkinID="SmallTextBox"
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap">
                    <asp:Label ID="lblDisplayDate" runat="server" Text="DISPLAY_DATE"></asp:Label>:
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="txtDisplayDate" TabIndex="-1" runat="server" SkinID="SmallTextBox"
                        ReadOnly="True"></asp:TextBox>
                </td>
                <td nowrap="nowrap">
                    <asp:Label ID="lblClaims" runat="server" Text="#_OF_CLAIMS"></asp:Label>:
                </td>
                <td>
                    <asp:TextBox ID="txtClaimsCount" TabIndex="-1" runat="server" SkinID="SmallTextBox"
                        ReadOnly="True"></asp:TextBox>
                </td>
                <td nowrap="nowrap">
                    <asp:Label ID="lblAmount" runat="server" Text="TOTAL_AMOUNT"></asp:Label>:
                </td>
                <td>
                    <asp:TextBox ID="txtTotalAmount" TabIndex="-1" runat="server" SkinID="SmallTextBox"
                        ReadOnly="True"></asp:TextBox>
                </td>
                <td nowrap="nowrap">
                    <asp:Label ID="lblTotalBonusAmount" runat="server" Text="TOTAL_BONUS_AMOUNT"></asp:Label>:
                </td>
                <td>
                    <asp:TextBox ID="txtTotalBonusAmount" runat="server" SkinID="SmallTextBox"
                        ReadOnly="True"></asp:TextBox>
                </td>
                 <td nowrap="nowrap">
                    <asp:Label ID="lblDeductible" runat="server" Text="DEDUCTIBLE"></asp:Label>:
                </td>
                <td>
                    <asp:TextBox ID="txtDeductible" runat="server" SkinID="SmallTextBox"
                        ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <hr />
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="lblMasterCtrName" runat="server" Text="MASTER_CENTER_NAME"></asp:Label>
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="txtMasterCtrName" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                    <input id="inpMasterCenterId" type="hidden" name="inpMasterCenterId" runat="server" />
                    <input id="inpMasterCenterDesc" type="hidden" name="inpMasterCenterDesc" runat="server" />
                    <cc1:AutoCompleteExtender ID="aCompMasterCenter" OnClientItemSelected="comboSelectedMasterCenter"
                        runat="server" TargetControlID="txtMasterCtrName" ServiceMethod="PopulateMasterCenterDrop"
                        MinimumPrefixLength='1' CompletionListCssClass="autocomplete_completionListElement">
                    </cc1:AutoCompleteExtender>
                    <cc1:TextBoxWatermarkExtender ID="tMasterCenter" Enabled="true" runat="server" TargetControlID="txtMasterCtrName"
                        WatermarkText="Enter text to Search..">
                    </cc1:TextBoxWatermarkExtender>
                </td>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="lblServiceCtrName" runat="server" Text="SERVICE_CENTER_NAME"></asp:Label>
                </td>
                <td colspan="3">
                    <asp:TextBox ID="txtServiceCtrName" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                    <input id="inpServiceCenterId" type="hidden" name="inpServiceCenterId" runat="server" />
                    <input id="inpServiceCenerDesc" type="hidden" name="inpServiceCenerDesc" runat="server" />
                    <cc1:AutoCompleteExtender ID="aCompServiceCenter" OnClientItemSelected="comboSelectedServiceCenter"
                        runat="server" TargetControlID="txtServiceCtrName" ServiceMethod="PopulateMasterCenterDrop"
                        MinimumPrefixLength='1' CompletionListCssClass="autocomplete_completionListElement">
                    </cc1:AutoCompleteExtender>
                    <cc1:TextBoxWatermarkExtender ID="tServiceCenter" Enabled="true" runat="server" TargetControlID="txtServiceCtrName"
                        WatermarkText="Enter text to Search..">
                    </cc1:TextBoxWatermarkExtender>
                    &nbsp;&nbsp;
                    <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                    </asp:Button>
                    <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <hr />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!-- new layout start -->
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            Search results for Pre-invoice Details</h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor"
                            runat="server">:</asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true"
                            SkinID="SmallDropDown">
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
                        <input id="HiddenIsPageDirty" style="width: 8px; height: 18px" type="hidden" size="1"
                            runat="server" />
                    </td>
                    <td class="bor" align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%">
            <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                DataKeyNames="Claim_Id" SkinID="DetailPageGridView" AllowSorting="true">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <Columns>
                    <asp:TemplateField Visible="False"></asp:TemplateField>
                    <asp:TemplateField ItemStyle-CssClass="FrozenCell" HeaderStyle-CssClass="FrozenHeader"
                        HeaderText="">
                        <HeaderTemplate>
                            <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" />
                        </HeaderTemplate>
                        <ItemStyle Wrap="False" HorizontalAlign="Left" Width="10px" BackColor="White"></ItemStyle>
                        <ItemTemplate>
                            <asp:CheckBox ID="btnSelected" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Claim_Number" SortExpression="Claim_Number">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditClaimNumber" runat="server" CommandName="SelectAction"
                                CommandArgument="<%#Container.DisplayIndex %>" OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="MASTER_CENTER_NAME" SortExpression="MASTER_CENTER_NAME"
                        HeaderText="MASTER_CENTER_NAME" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="SERVICE_CENTER_NAME" SortExpression="SERVICE_CENTER_NAME"
                        ReadOnly="true" HeaderText="SERVICE_CENTER_NAME" HeaderStyle-HorizontalAlign="Center"
                        HtmlEncode="false" />
                    <asp:BoundField DataField="CLAIM_TYPE" SortExpression="CLAIM_TYPE" ReadOnly="true"
                        HeaderText="CLAIM_TYPE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="AUTHORIZATION_AMOUNT" SortExpression="AUTHORIZATION_AMOUNT"
                        ReadOnly="true" HeaderText="AUTH_AMOUNT" HeaderStyle-HorizontalAlign="Center"
                        HtmlEncode="false" />
                    <asp:BoundField DataField="BONUS_AMOUNT" SortExpression="BONUS_AMOUNT"
                        ReadOnly="true" HeaderText="BONUS_AMOUNT" HeaderStyle-HorizontalAlign="Center"
                        HtmlEncode="false" />
                      <asp:BoundField DataField="DEDUCTIBLE" SortExpression="DEDUCTIBLE" 
                        ReadOnly="true" HeaderText="DEDUCTIBLE" HeaderStyle-HorizontalAlign="Center" 
                        HtmlEncode="false" />
                    <asp:BoundField DataField="TOTAL_AMOUNT" SortExpression="TOTAL_AMOUNT"
                        ReadOnly="true" HeaderText="TOTAL_AMOUNT" HeaderStyle-HorizontalAlign="Center"
                        HtmlEncode="false" />
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>
        <div class="btnZone">
            <asp:Button ID="btnBack" SkinID="AlternateLeftButton" TabIndex="35" runat="server"
                FCausesValidation="False" Text="Back"></asp:Button>
            <asp:Button ID="btnApprove" runat="server" Text="APPROVE_ALL_CLAIMS" SkinID="AlternateRightButton"
                Enabled="false"></asp:Button>
            <asp:Button ID="btnReject" runat="server" Text="REJECT_SELECTED_CLAIMS" SkinID="AlternateRightButton"
                Enabled="false"></asp:Button>
        </div>
        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
            runat="server" />
        <p>
            <asp:Literal ID="CheckBoxIDsArray" runat="server"></asp:Literal>&nbsp;</p>
        <input type="hidden" id="checkRecords" value="" runat="server" />
    </div>
    <asp:ModalPopupExtender runat="server" ID="mdlPopup" TargetControlID="btnReject"
            PopupControlID="pnlCommentsPopup" BackgroundCssClass="ModalBackground" DropShadow="false" Enabled="true"
            RepositionMode="RepositionOnWindowScroll"   CancelControlID ="btnCancel"> 
    </asp:ModalPopupExtender>
    <asp:Panel ID="pnlCommentsPopup" runat="server" CssClass="modalpopup" Style="display: none; width: 75%; ">
         <div id="dvcomments" runat="server" class="overlay_message_content" style="width: 50%; top: 25px; overflow: hidden;"> 
           <p class="modalTitle">
                    <asp:Label ID="lblCommentsTitle" runat="server" Text="Comments"></asp:Label>
           </p>
           <table  width="100%" border="0" cellpadding="0" cellspacing="0" class="formGrid" >
                <tr>
                    <td>
                        <asp:Label ID="lblRejectComments" runat="server" Text="Comments"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRejectComments" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnOk" runat="server" SkinID="PrimaryRightButton" Text="Ok" />
                        <asp:Button ID="btnCancel" runat="server" SkinID="AlternateRightButton" Text="Cancel"></asp:Button>
                    </td>
                </tr>
         </table>
        </div> 
        </asp:Panel>
    <!-- end new layout -->
    <script language="javascript" type="text/javascript">
        function resizeScroller(item) {
            var browseWidth, browseHeight;

            if (document.layers) {
                browseWidth = window.outerWidth;
                browseHeight = window.outerHeight;
            }
            if (document.all) {
                browseWidth = document.body.clientWidth;
                browseHeight = document.body.clientHeight;
            }

            if (screen.width == "800" && screen.height == "600") {
                newHeight = browseHeight - 220;
            }
            else {
                newHeight = browseHeight - 975;
            }

            if (newHeight < 470) {
                newHeight = 470;
            }

            item.style.height = String(newHeight) + "px";

            return newHeight;
        }

        function clearControls() {
            document.getElementById('ctl00_SummaryPlaceHolder_txtMasterCtrName').value = "";
            document.getElementById('ctl00_SummaryPlaceHolder_txttxtServiceCtrName').value = "";
            return false;
        }
        //resizeScroller(document.getElementById("scroller"));
        function setDirty() {
            document.getElementById("ctl00_BodyPlaceHolder_HiddenIsPageDirty").value = "YES"
        }

        function CheckboxAction(cbValue, cbClientId, btnClientId1, btnClientId2, hiddenClientId) {
            var objCbId = document.getElementById(cbClientId);
            var objbtnId1 = document.getElementById(btnClientId1);
            var objbtnId2 = document.getElementById(btnClientId2);
            var objCheckRecords = document.getElementById(hiddenClientId);
            var tempValue = "";
            var objHiddenIsPageDirtyId = document.getElementById('ctl00_BodyPlaceHolder_HiddenIsPageDirty');

            if (objCbId != null && cbValue != null) {
                if (objCbId.checked) {
                    objCheckRecords.value = cbValue + ':' + objCheckRecords.value;
                }
                else {
                    objCheckRecords.value = objCheckRecords.value.replace(cbValue, '');
                }

                tempValue = objCheckRecords.value;
                tempValue = tempValue.replace(/:/g, '');

                if (tempValue != null && tempValue != '') {
                    //                    objbtnId1.disabled = false;
                    objbtnId2.disabled = false;
                    objHiddenIsPageDirtyId.value = 'YES';
                }
                else {
                    //                    objbtnId1.disabled = true;
                    objbtnId2.disabled = true;
                    objHiddenIsPageDirtyId.value = 'NO';
                }

                var intCheckedCount = 0;
                for (var i = 1; i < CheckBoxIDs.length; i++) {
                    objCbId = document.getElementById(CheckBoxIDs[i]);
                    if (objCbId.checked == true) {
                        intCheckedCount = intCheckedCount + 1;
                        break;
                    }
                }
                if (intCheckedCount > 0) {
                    //                    objbtnId1.disabled = false;
                    objbtnId2.disabled = false;
                    objHiddenIsPageDirtyId.value = 'YES';
                }
                else {
                    //                    objbtnId1.disabled = true;
                    objbtnId2.disabled = true;
                    objHiddenIsPageDirtyId.value = 'NO';
                }
                __doPostBack(btnClientId2, 1);

            }
        }

        function ChangeCheckBoxState(id, checkState) {
            var cb = document.getElementById(id);
            if (cb != null)
                if (cb.disabled == false)
                    cb.checked = checkState;
        }

        function ChangeAllCheckBoxStates(checkState, btnClientId1, btnClientId2) {
            // Toggles through all of the checkboxes defined in the CheckBoxIDs array
            // and updates their value to the checkState input parameter
            if (CheckBoxIDs != null) {
                for (var i = 1; i < CheckBoxIDs.length; i++)
                    ChangeCheckBoxState(CheckBoxIDs[i], checkState);
            }

            var objbtnId1 = document.getElementById(btnClientId1);
            var objbtnId2 = document.getElementById(btnClientId2);
            var objHiddenIsPageDirtyId = document.getElementById('ctl00_BodyPlaceHolder_HiddenIsPageDirty');

            if (checkState == true) {
                //                objbtnId1.disabled = false;
                objbtnId2.disabled = false;
                objHiddenIsPageDirtyId.value = 'YES';
            }
            else {
                //                objbtnId1.disabled = true;
                objbtnId2.disabled = true;
                objHiddenIsPageDirtyId.value = 'NO';
            }
           __doPostBack(btnClientId2, 1); 

        }

        function ChangeHeaderAsNeeded() {
            // Whenever a checkbox in the GridView is toggled, we need to
            // check the Header checkbox if ALL of the GridView checkboxes are
            // checked, and uncheck it otherwise
            if (CheckBoxIDs != null) {
                // check to see if all other checkboxes are checked
                for (var i = 1; i < CheckBoxIDs.length; i++) {
                    var cb = document.getElementById(CheckBoxIDs[i]);
                    if (!cb.checked) {
                        // Whoops, there is an unchecked checkbox, make sure
                        // that the header checkbox is unchecked
                        ChangeCheckBoxState(CheckBoxIDs[0], false);
                        return;
                    }
                }

                // If we reach here, ALL GridView checkboxes are checked
                ChangeCheckBoxState(CheckBoxIDs[0], true);
            }
        }

    </script>
</asp:Content>
