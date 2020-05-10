<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlClaimCloseRules.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlClaimCloseRules" %>
<link href="../App_Themes/Default/Default.css" rel="stylesheet" />
<asp:Panel ID="moClaimCloseRulesTabPanel_WRITE" runat="server" Width="100%">
    <div class="dataContainer">
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor"
                            runat="server">:</asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true"
                            CssClass="small">
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
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:GridView ID="CloseRulesGrid" runat="server" Width="100%" DataKeyNames="claim_close_rule_id,company_id, dealer_id, claim_status_by_group_id, reason_closed_id, parent_claim_close_rule_id, claim_issue_id, close_rule_based_on_id"
        AllowPaging="True" AllowSorting="False" AutoGenerateColumns="False" CssClass="small" CssClass = "grid-view"
        PageSize="2">
        <SelectedRowStyle Wrap="True"></SelectedRowStyle>
        <EditRowStyle Wrap="True"></EditRowStyle>
        <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
        <RowStyle Wrap="True"></RowStyle>
        <Columns>
            <asp:BoundField Visible="false" DataField="claim_close_rule_id" />
            <asp:TemplateField Visible="True" HeaderText="COMPANY_CODE">
                <ItemTemplate>
                    <asp:Label ID="lblCompany" runat="server"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="DEALER CODE">
                <ItemTemplate>
                    <asp:Label ID="lblDealer" runat="server">
                    </asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="RULE_BASED_ON">
                <ItemTemplate>
                    <asp:Label ID="lblCloseRuleBasedOn" runat="server" Text='<%#Container.DataItem("close_rule_based_on")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="cboRuleBasedOn" runat="server" Visible="True" CssClass="small" AutoPostBack="true" OnSelectedIndexChanged="cboRuleBasedOn_SelectedIndexChanged">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="EXTENDED_CLAIM_STATUS">
                <ItemTemplate>
                    <asp:Label ID="lblClaimStatusByGroup" runat="server" Text='<%#Container.DataItem("claim_status_by_group")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="cboClaimStatusByGroup" runat="server" Visible="True" CssClass="small">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="CLAIM_ISSUES">
                <ItemTemplate>
                    <asp:Label ID="lblClaimIssue" runat="server" Text='<%#Container.DataItem("claim_issue")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="cboClaimIssue" runat="server" Visible="True" CssClass="small">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="TIME_PERIOD">
                <ItemTemplate>
                    <asp:Label ID="lblTimePeriod" runat="server" Text='<%#Container.DataItem("time_period")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtTimePeriod" runat="server" Visible="true"  CssClass="exsmall"
                        onkeypress="return isNumber(event)" onblur="return chkForZero(this)"></asp:TextBox><br />
                    <asp:Label ID="lblTimePeriodMsg" runat="server"></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="REASON_CLOSED">
                <ItemTemplate>
                    <asp:Label ID="lblReasonClosed" runat="server" Text='<%#Container.DataItem("reason_closed")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="cboReasonClosed" runat="server" Visible="True" CssClass="medium">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:TextBox ID="txtActiveFlag" runat="server" Visible="false" CssClass="small"
                        Text="Y"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="false">
                <ItemTemplate>
                    <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="SelectAction"
                        ImageUrl="~/App_Themes/Default/Images/edit.png" CommandArgument="<%#Container.DisplayIndex %>" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="DeleteButton_WRITE" ImageUrl="~/App_Themes/Default/Images/icon_delete.png"
                        runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>" />
                </ItemTemplate>
                <EditItemTemplate>
                    <table><tr><td>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="primaryBtn floatR"></asp:Button></td>
                    <td><asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="altBtn floatR"></asp:LinkButton>
                    </td></tr></table>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
    </asp:GridView>
    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
    <asp:Button ID="NewButton_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="100px" Text="New"
        Height="20px" CssClass="FLATBUTTON"></asp:Button>
</asp:Panel>
<script type="text/javascript">
    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }

        return true;
    }

    function chkForZero(obj) {
        var obj1 = obj.id.replace('txtTimePeriod', 'lblTimePeriodMsg');
        
        if (obj.value == 0) {
            document.getElementById(obj1).innerText = "Are Your Sure?";
            document.getElementById(obj1).style.color = "Red";
        }
        else {
            document.getElementById(obj1).innerText = "";
        }
        return true;
    }
</script>
