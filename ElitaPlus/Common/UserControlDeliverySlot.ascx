<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlDeliverySlot.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlDeliverySlot" %>
<tr runat="server" id="trCourierProduct">
    <td align="right" nowrap="nowrap">
        <asp:Label runat="server" ID="lblCourierProduct" Visible="false">COURIER_PRODUCT</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlCourierProduct" runat="server" Visible="false" SkinID="MediumDropDown" AutoPostBack="true" OnSelectedIndexChanged="ddlCourierProduct_SelectedIndexChanged">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" nowrap="nowrap">
    </td>
    <td align="left" nowrap="nowrap">
     <asp:CheckBox ID="chkNotSpecify" runat="server" Checked="true" AutoPostBack="true"/>
    </td>
</tr>
<tr>
    <td align="right" nowrap="nowrap">
        <asp:Label runat="server" ID="lblAvailableDeliveryTiming">AVAILABLE_DELIVERY_TIMING</asp:Label>:
    </td>
    <td align="left" nowrap="nowrap">
        <asp:Label runat="server" ID="lblAvailableDeliveryTimingData" />
    </td>
</tr>
<tr>
    <td align="right" nowrap="nowrap">
        <asp:Label runat="server" ID="lblDesiredDate">DESIRED_DELIVERY_DATE</asp:Label>:
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtDeliveryDate" runat="server" AutoPostBack ="true" OnTextChanged="txtDeliveryDate_TextChanged"></asp:TextBox>
        <asp:ImageButton ID="imageBtnDeliveryDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
    </td>
</tr>
<tr runat="server" id="trDeliverySlot">
    <td align="right" nowrap="nowrap">
        <asp:Label runat="server" ID="lblDeliverySlot">DESIRED_DELIVERY_TIME</asp:Label>:
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlDeliverySlots" runat="server" SkinID="SmallDropDown">
        </asp:DropDownList>
    </td>
</tr>
