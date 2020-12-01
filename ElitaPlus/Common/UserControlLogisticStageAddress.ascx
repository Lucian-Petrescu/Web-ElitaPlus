<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlLogisticStageAddress.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlLogisticStageAddress" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAddressInfo" Src="../Common/UserControlAddress_New.ascx" %>

<table class="formGrid" style="border-collapse: collapse; border: 0;">
    <tr>
        <td>
            <asp:Repeater ID="repLogisticStageAddress" runat="server" OnItemDataBound="repLogisticStageAddress_OnItemDataBound" Visible="True">
                <ItemTemplate>
                    <table style="border-collapse: collapse; border: 0;">
                        <tr>
                            <td>
                                <asp:HiddenField ID="hdnLogisticStageCode" runat="server" Visible="False"/>
                                <asp:Label ID="lblLogisticStageName" runat="server" />:
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <Elita:UserControlAddressInfo ID="moAddressController" runat="server" Visible="True" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
        </td>
    </tr>
</table>