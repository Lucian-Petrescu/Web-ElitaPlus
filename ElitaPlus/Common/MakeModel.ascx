<%@ Control Language="vb" AutoEventWireup="false" Codebehind="MakeModel.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.MakeModel" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
&nbsp;<asj:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr valign="middle">
                <td nowrap align="left" width="25%">
                    &nbsp;&nbsp;<asp:Label ID="MakeLabel" runat="server">MAKE</asp:Label>:</td>
                <td nowrap align="left" width="25%">
                    &nbsp;&nbsp;<asp:Label ID="ModelLabel" runat="server">MODEL</asp:Label>:</td>
                <td nowrap align="left" width="25%">
                    &nbsp;&nbsp;<asp:Label ID="TrimLabel" runat="server">ENGINE_VERSION</asp:Label>:</td>
                <td nowrap align="left" width="25%">
                    &nbsp;&nbsp;<asp:Label ID="YearLabel" runat="server">YEAR</asp:Label>:</td>
            </tr>
            <tr>
                <td nowrap align="left" style="height: 22px">
                    &nbsp;&nbsp;<asp:DropDownList ID="MakeDrop" runat="server" Width="93%" AutoPostBack="True"
                        DataValueField="MANUFACTURER_ID" DataTextField="DESCRIPTION" EnableViewState="True"
                        TabIndex="1" OnSelectedIndexChanged="MakeDrop_SelectedIndexChanged">
                    </asp:DropDownList></td>
                <td nowrap align="left" style="height: 22px">
                    &nbsp;&nbsp;<asp:DropDownList ID="ModelDrop" runat="server" Width="93%" AutoPostBack="True"
                        Enabled="False" DataValueField="CODE" DataTextField="DESCRIPTION" TabIndex="2"
                        OnSelectedIndexChanged="ModelDrop_SelectedIndexChanged">
                    </asp:DropDownList></td>
                <td nowrap align="left" style="height: 22px">
                    &nbsp;&nbsp;<asp:DropDownList ID="TrimDrop" runat="server" Width="93%" AutoPostBack="True"
                        Enabled="False" DataValueField="CODE" DataTextField="DESCRIPTION" TabIndex="3"
                        OnSelectedIndexChanged="TrimDrop_SelectedIndexChanged">
                    </asp:DropDownList></td>
                <td nowrap align="left" style="height: 22px">
                    &nbsp;&nbsp;<asp:DropDownList ID="YearDrop" runat="server" Width="93%" Enabled="False"
                        DataValueField="CODE" DataTextField="DESCRIPTION" TabIndex="4">
                    </asp:DropDownList></td>
            </tr>
        </table>
    </ContentTemplate>
</asj:UpdatePanel>
