<%@ Control Language="vb" EnableTheming="true" AutoEventWireup="false" CodeBehind="ReplacementOptions.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ReplacementOptions" %>
<div id="dvReplacementOption" runat="server" class="dataContainer">
    <h2 class="dataGridHeader">
        <asp:Label runat="server" ID="lblReplacmentOptionsr">BEST_REPLACEMENT_OPTIONS</asp:Label>
    </h2>
    <div class="stepformZone">
        <asp:GridView ID="GVReplacementOptions" runat="server" Width="98%" AutoGenerateColumns="False" 
            CellPadding="1" AllowPaging="False" CssClass="DATAGRID"
            AllowSorting="False">
            <SelectedRowStyle Wrap="False" CssClass="SELECTED" />
            <EditRowStyle Wrap="False" CssClass="EDITROW" />
            <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
            <RowStyle Wrap="False" CssClass="ROW" />
            <HeaderStyle CssClass="HEADER" />
            <Columns>
                <asp:BoundField HeaderText="MAKE" DataField="Manufacturer" />
                <asp:BoundField HeaderText="MODEL" DataField="Model" />
                <asp:BoundField HeaderText="PRIORITY" DataField="Priority" />
            </Columns>
        </asp:GridView>
    </div>
</div>