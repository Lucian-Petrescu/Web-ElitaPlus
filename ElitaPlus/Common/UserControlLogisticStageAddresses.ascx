<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlLogisticStageAddresses.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlLogisticStageAddresses" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAddressInfo" Src="~/Common/UserControlAddress_New.ascx" %>

<table style="width: 100%; border-collapse: collapse; border: 0;">
    <tr>
        <td>
            <h2 class="dataGridHeader">
                <asp:Label ID="lblLogisticStageAddress" runat="server">LOGISTIC_STAGE_ADDRESSES</asp:Label>
            </h2>
            <div class="stepformZone">
                <table class="formGrid" style="border-collapse: collapse; border: 0;">
                    <tr>
                        <td>
                            <asp:Repeater ID="repLogisticStageAddress" runat="server" OnItemDataBound="repLogisticStageAddress_OnItemDataBound" Visible="True">
                                <ItemTemplate>
                                    <table style="border-collapse: collapse; border: 0;">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblLogisticStageName" runat="server" />:
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <Elita:UserControlAddressInfo ID="moAddressController" runat="server" Visible="True"/>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>


