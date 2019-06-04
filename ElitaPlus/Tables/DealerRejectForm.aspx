<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DealerRejectForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.DealerRejectForm" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register Src="../Common/MultipleColumnDDLabelControl.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc2" %>

<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <!--Start Header-->
    <table class="searchGrid" id="searchTable" runat="server" cellspacing="0" cellpadding="0"
        border="0">
        <tr>
            <td align="left" colspan="4">
                <uc2:MultipleColumnDDLabelControl ID="moDealerMultipleDrop" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4">&nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 24%">
                <asp:Label ID="Label1" runat="server">MSG_TYPE</asp:Label>&nbsp;</td>
            <td align="left" style="width: 24%">
                <asp:Label ID="lblRejectCode" runat="server">REJECT_CODE</asp:Label>&nbsp;</td>
            <td align="left" style="width: 24%">
                <asp:Label ID="lblRejectReason" runat="server">REJECT_REASON</asp:Label>&nbsp;</td>
            <td align="left" style="width: 24%">
                <asp:Label ID="lblRecordType" runat="server">RECORD_TYPE</asp:Label>&nbsp;</td>
            <td align="left" style="width: 28%">&nbsp;</td>

        </tr>
        <tr>
            <td align="left">
                <asp:DropDownList ID="ddlRectionMsgType" runat="server" SkinID="MediumDropDown" AutoPostBack="true">
                </asp:DropDownList>
            </td>
            <td align="left">
                <asp:TextBox ID="TextboxRejectCode" TabIndex="1"
                    runat="server" SkinID="MediumTextBox"></asp:TextBox>
            </td>
            <td align="left">
                <asp:TextBox ID="TextboxRejectReason" TabIndex="2"
                    runat="server" SkinID="MediumTextBox"></asp:TextBox>
            </td>
            <td align="left">
                <asp:DropDownList ID="ddlRecordType" runat="server" SkinID="SmallDropDown">
                </asp:DropDownList>
            </td>
            <td align="left">&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: right;" colspan="4">&nbsp;                                            
            </td>
        </tr>
        <tr>
            <td style="text-align: right;" colspan="4">
                <asp:Button ID="mobtnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear"></asp:Button>
                <asp:Button ID="mobtnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="4">&nbsp;
            </td>
        </tr>
    </table>

</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader">Search results for Price List
        </h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
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
                    </td>
                    <td class="bor" align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%">
            <asp:DataGrid ID="DataGridRejectCode" runat="server" SkinID="DetailPageDataGrid" Width="100%"
                AutoGenerateColumns="False" AllowPaging="True" AllowSorting="True" OnItemCreated="ItemCreated"
                OnItemCommand="DataGridRejectCode_ItemCommand">
                <SelectedItemStyle Wrap="true" />
                 <EditItemStyle Wrap="true" />
                <AlternatingItemStyle Wrap="true" />
                <HeaderStyle />
                <Columns>
                    <asp:TemplateColumn Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblDealerRejectCodeId" runat="server" Visible="False" Text='<%# CheckNull(Container.DataItem("DEALER_REJECT_CODE_ID")) %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="DEALER_BYPASS">
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:CheckBox Width="100%" ID="CheckBoxItemSel" runat="server" AutoPostBack="true"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="REJECT_CODE">
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="25%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label Width="100%" ID="lblDealerRejCode" runat="server" Text='<%# IIf(Container.DataItem("REJECT_CODE") is dbNull.Value, "", Container.DataItem("REJECT_CODE")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="REJECT_REASON">
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="65%"></HeaderStyle>
                        <ItemTemplate>
                            <asp:Label Width="100%" ID="lblDealerRejReason" runat="server" Text='<%# IIf(Container.DataItem("REJECT_REASON") is dbNull.Value, "", Container.DataItem("REJECT_REASON")) %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblDealerId" runat="server" Visible="False">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblMsgCodeId" runat="server" Visible="False" Text='<%# CheckNull(Container.DataItem("msg_code_id")) %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblRecordTypeId" runat="server" Visible="False" Text='<%# CheckNull(Container.DataItem("record_type_id")) %>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                
                <PagerStyle PageButtonCount="30" Mode="NumericPages" Position="TopAndBottom"></PagerStyle>
            </asp:DataGrid>
        </div>
    </div>
    <div class="btnZone">
        <div class="">
            <asp:Button ID="btnSave" runat="server" SkinID="PrimaryRightButton" Text="SAVE" />
            <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="BACK" />
            <asp:Button ID="btnCancel" runat="server" CssClass="popWindowCancelbtn floatR" Text="CANCEL" />
            <asp:Button ID="btnAdd_WRITE" runat="server" SkinID="PrimaryLeftButton" Text="New" />
        </div>
    </div>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" />

</asp:Content>
