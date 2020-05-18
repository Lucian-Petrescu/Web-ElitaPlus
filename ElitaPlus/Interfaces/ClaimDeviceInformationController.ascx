<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ClaimDeviceInformationController.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimDeviceInformationController" %>
<link href="../App_Themes/Default/Default.css" rel="stylesheet" />
<div id="dvClaimDeviceInformation" runat="server" class="dataContainer">
    <h2 class="dataGridHeader">
        <asp:Label runat="server" ID="lblClaimDeviceInformation">CLAIM_DEVICE_INFORMATION</asp:Label>
    </h2>
    <asp:GridView ID="GvClaimDeviceInformation" runat="server" Width="100%" AutoGenerateColumns="False"
        CellPadding="1" AllowPaging="False" CssClass="dataGrid"
        AllowSorting="False">
        <SelectedRowStyle Wrap="True" />
        <EditRowStyle Wrap="True" />
        <AlternatingRowStyle Wrap="True" />
        <RowStyle Wrap="True" />
        <HeaderStyle />
        <Columns>
            <asp:BoundField HeaderText="MAKE" DataField="Manufacturer" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="MODEL" DataField="Model" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="DEVICE_TYPE" DataField="DeviceType" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="PURCHASED_DATE" DataField="PurchasedDate" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="PURCHASE_PRICE" DataField="PurchasedPrice" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="IMEI_NUM" DataField="ImeiNumber" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="SERIAL_NUM" DataField="SerialNumber" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="REGISTERED_ITEM" DataField="RegisteredItemName" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" />
            <%--<asp:BoundField HeaderText="COLOR" DataField="Color" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="CAPACITY" DataField="Memory" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" />
            <asp:BoundField HeaderText="CARRIER" DataField="Carrier" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" />--%>
            <asp:BoundField HeaderText="EQUIPMENT_TYPE" DataField="EquipmentType" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" />
        </Columns>
    </asp:GridView>
</div>
