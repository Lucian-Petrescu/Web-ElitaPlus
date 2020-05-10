<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlLogisticalInfo.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlLogisticalInfo" %>
<link href="../App_Themes/Default/Default.css" rel="stylesheet" />
<asp:Panel ID="moClaimCloseRulesTabPanel_WRITE" runat="server" Width="100%">

    <asp:GridView ID="LogisticsGrid" runat="server" Width="100%" DataKeyNames="claim_shipping_id" ShowHeaderWhenEmpty="true"
        AllowPaging="False" AllowSorting="False" AutoGenerateColumns="False"  CssClass="dataGrid"
        PageSize="30">
        <SelectedRowStyle Wrap="True"></SelectedRowStyle>
        <EditRowStyle Wrap="True"></EditRowStyle>
        <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
        <RowStyle Wrap="True"></RowStyle>
        <Columns>
            <asp:BoundField Visible="false" DataField="claim_shipping_id" />
            <asp:BoundField Visible="true" DataField="shipping_type" ReadOnly="true" HeaderText="SHIPPING_TYPE" />

            <asp:TemplateField Visible="True" HeaderText="SHIPPING_DATE">
                <ItemTemplate>
                    <asp:TextBox ID="textboxShippingDate" runat="server" CssClass="small" ReadOnly="true"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField Visible="True" HeaderText="TRACKING_NUMBER">
                <ItemTemplate>
                    <asp:Label ID="lblTrackingNumber" runat="server" Text='<%#Container.DataItem("TRACKING_NUMBER")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextboxTrackingNumber" TabIndex="83" runat="server" CssClass="small"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="RECEIVED_DATE">
                <ItemTemplate>
                    <asp:Label ID="lblReceivedDate" runat="server" Text='<%#Container.DataItem("received_Date")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="TextboxReceivedDate" TabIndex="83" runat="server" CssClass="exsmall"></asp:TextBox>
                    <asp:ImageButton ID="ImageButtonRecdDate" TabIndex="84" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                        ImageAlign="AbsMiddle"></asp:ImageButton>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="false">
                <ItemTemplate>
                    <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="SelectAction"
                        ImageUrl="~/App_Themes/Default/Images/edit.png" CommandArgument="<%#Container.DisplayIndex %>" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <%--<asp:ImageButton ID="DeleteButton_WRITE" ImageUrl="~/App_Themes/Default/Images/icon_delete.png"
                        runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>" />--%>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="primaryBtn floatR"></asp:Button>
                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="altBtn floatR"></asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
    </asp:GridView>
    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
</asp:Panel>
