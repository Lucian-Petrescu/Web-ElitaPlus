<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="CancellationRequestExceptionForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CancellationRequestExceptionForm"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <style type="text/css">
        tr.HEADER th
        {
            text-align: center;
        }
        tr.LeftAlign td
        {
            text-align: left;
            padding-right: 25px;
        }
        INPUT.BUTTONSTYLE_EDIT
        {
            background-image: url(../Navigation/images/icons/edit2.gif);
            width: 165px;
        }
    </style>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td colspan="2" style="text-align: center; padding-left: 8px; padding-right: 8px;">
                <table cellpadding="2" cellspacing="0" border="0" style="border: #999999 1px solid;
                    background-color: #f1f1f1; height: 40px; width: 100%; padding: 0px;">
                    <tr align="center" valign="top">
                        <td style="width: 100%;" colspan="3">
                        </td>
                    </tr>
                    <tr class="LeftAlign">
                        <td style="width: 25%;">
                            <asp:Label ID="lblTransactionType" runat="server">TRANSACTION_TYPE</asp:Label>:<br />
                            <asp:DropDownList ID="cboTrasnsactionType" runat="server" Width="80%">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 25%;">
                            <asp:Label ID="lblMobNumber" runat="server">MOBILE_NUMBER</asp:Label>:<br />
                            <asp:TextBox ID="txtMobNumber" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False"
                                Width="80%" TabIndex="2"></asp:TextBox>
                        </td>
                        <td style="padding-right: 9px; vertical-align: bottom; width: 31%;">
                            <asp:Label ID="lblFrom" runat="server">FROM</asp:Label>:
                            <asp:TextBox ID="txtFrom" runat="server" Width="90px" CssClass="FLATTEXTBOX_TAB"
                                onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                            <asp:ImageButton ID="imgBtnFrom" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                ImageAlign="AbsMiddle" />
                            &nbsp;&nbsp;
                            <asp:Label ID="lblTo" runat="server">TO</asp:Label>:
                            <asp:TextBox ID="txtTo" runat="server" Width="90px" onFocus="setHighlighter(this)"
                                onMouseover="setHighlighter(this)" CssClass="FLATTEXTBOX_TAB" Visible="True"></asp:TextBox>
                            <asp:ImageButton ID="imgBtnTo" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                ImageAlign="AbsMiddle" />
                        </td>
                    </tr>
                    <tr align="center" valign="top">
                        <td style="width: 100%;" colspan="3">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: text-bottom; padding-right: 4px; padding-bottom: 5px"
                            colspan="3">
                            <asp:Button ID="btnClearSearch" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" runat="server"
                                Text="Clear" TabIndex="5" />&nbsp;&nbsp;
                            <asp:Button ID="btnSearch" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH" runat="server"
                                Text="Search" TabIndex="4" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 5px; padding-bottom: 5px;" colspan="2">
                <hr style="height: 1px" />
            </td>
        </tr>
        <tr id="trPageSize" runat="server" visible="False">
            <td align="left">
                <asp:Label ID="lblPageSize" runat="server">Page_Size:</asp:Label>&nbsp;
                <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
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
            <td style="text-align: right">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="LeftAlign">
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
                    AllowPaging="True" AllowSorting="True" CssClass="DATAGRID">
                    <SelectedRowStyle Wrap="False" CssClass="SELECTED" />
                    <EditRowStyle Wrap="False" CssClass="EDITROW" />
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
                    <RowStyle Wrap="False" CssClass="ROW" Height="22px" />
                    <HeaderStyle CssClass="HEADER" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="18px">
                            <ItemStyle Width="18px" CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:ImageButton Style="cursor: hand;" ID="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif" Visible ="false"
                                    runat="server" CommandName="SelectAction"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBoxItemSel" runat="server" Checked="false"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblShowCheckboxID" runat="server" Text='<%# Container.DataItem("show_checkbox")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblTransTmxDeactivateId" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("trans_tmx_deactivate_id"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CERTIFICATE" SortExpression="CERT_NUMBER">
                            <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label ID="lblCertId" runat="server" Visible="True" Text='<%# Container.DataItem("CERT_NUMBER") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="MOBILE_NUMBER" SortExpression="mobile_number">
                            <ItemStyle CssClass="CenteredTD" Width="180px" />
                            <ItemTemplate>
                                <asp:Label ID="lblMobileNumber" runat="server" Text='<%#Container.DataItem("mobile_number")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TRANSACTION_TYPE" SortExpression="Trans_Type">
                            <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label ID="lblTransactionType" runat="server" Text='<%# Container.DataItem("Trans_Type") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PROCESSING_DATE" SortExpression="trans_scheduled_date">
                            <ItemTemplate>
                                <asp:Label ID="lblProcessingDate" runat="server" Text='<%#Container.DataItem("trans_scheduled_date")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NO_OF_ATTEMPTS" SortExpression="attempt">
                            <ItemTemplate>
                                <asp:Label ID="lblAttempt" runat="server" Text='<%#Container.DataItem("attempt")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ERROR_MSG" SortExpression="error_msg">
                            <ItemStyle Width="400px" />
                            <ItemTemplate>
                                <asp:Label ID="lblErrMsg" runat="server" Text='<%#Container.DataItem("error_msg")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings PageButtonCount="15" Mode="Numeric" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <p>
        <asp:Literal ID="CheckBoxIDsArray" runat="server"></asp:Literal>&nbsp;</p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" designtimedragdrop="261" />
    <asp:Button ID="btnHide" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Enabled="false" Width="140px" CssClass="FLATBUTTON" Height="20px" Text="Hide">
    </asp:Button>
    &nbsp;
    <asp:Button ID="btnProcessRecords" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Enabled="false" Width="140px" CssClass="FLATBUTTON" Height="20px" Text="ProcessRecords">
    </asp:Button>
    &nbsp;
    <asp:Button ID="btnExportResults" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Enabled="false" Width="140px" CssClass="FLATBUTTON" Height="20px" Text="ExportResults">
    </asp:Button>
    <input type="hidden" id="checkRecords" value="" runat="server" />
    <script>
        function CheckboxAction(cbValue, cbClientId, btnClientId, btnHideClientId, hiddenClientId) {
            var objCbId = document.getElementById(cbClientId);
            var objbtnId = document.getElementById(btnClientId);
            var objHidebtnId = document.getElementById(btnHideClientId);
            var objCheckRecords = document.getElementById(hiddenClientId);
            var tempValue = "";

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
                    objbtnId.disabled = false;
                    objHidebtnId.disabled = false;
                }
                else {
                    objbtnId.disabled = true;
                    objHidebtnId.disabled = true;
                }

                var intCheckedCount = 0;
                for (var i = 0; i < CheckBoxIDs.length; i++) {
                    objCbId = document.getElementById(CheckBoxIDs[i]);
                    if (objCbId != null) 
                    {
                        if (objCbId.checked == true) {
                            intCheckedCount = intCheckedCount + 1;
                            break;
                        }
                    }
                }
                if (intCheckedCount > 0) {
                    objbtnId.disabled = false;
                    objHidebtnId.disabled = false;
                }
                else {
                    objbtnId.disabled = true;
                    objHidebtnId.disabled = true;
                }


            }
        }

        function ChangeCheckBoxState(id, checkState) {
            var cb = document.getElementById(id);
            if (cb != null) {
                if (cb != null)
                    cb.checked = checkState;
            }
        }

        function ChangeAllCheckBoxStates(checkState, btnClientId, btnHideClientId) {
            // Toggles through all of the checkboxes defined in the CheckBoxIDs array
            // and updates their value to the checkState input parameter
            if (CheckBoxIDs != null) {
                for (var i = 0; i < CheckBoxIDs.length; i++)
                    ChangeCheckBoxState(CheckBoxIDs[i], checkState);
            }

            var objbtnId = document.getElementById(btnClientId);
            var objHidebtnId = document.getElementById(btnHideClientId);
            if (checkState == true) {
                objbtnId.disabled = false;
                objHidebtnId.disabled = false;
            }
            else {
                objbtnId.disabled = true;
                objHidebtnId.disabled = true;
            }


        }

        function ChangeHeaderAsNeeded() {
            // Whenever a checkbox in the GridView is toggled, we need to
            // check the Header checkbox if ALL of the GridView checkboxes are
            // checked, and uncheck it otherwise
            if (CheckBoxIDs != null) {
                // check to see if all other checkboxes are checked
                for (var i = 1; i < CheckBoxIDs.length; i++) {
                    var cb = document.getElementById(CheckBoxIDs[i]);
                    if (cb != null) {
                        if (!cb.checked) {
                            // Whoops, there is an unchecked checkbox, make sure
                            // that the header checkbox is unchecked
                            ChangeCheckBoxState(CheckBoxIDs[0], false);
                            return;
                        }
                    }
                }

                // If we reach here, ALL GridView checkboxes are checked
                ChangeCheckBoxState(CheckBoxIDs[0], true);
            }
        }	 	    
    </script>
</asp:Content>
