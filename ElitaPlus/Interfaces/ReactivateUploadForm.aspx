<%@ Page Language="vb" MasterPageFile="../Navigation/masters/ElitaBase.Master" AutoEventWireup="false"
    CodeBehind="ReactivateUploadForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ReactivateUploadForm"
    Title="Reactivate Upload" Theme="Default" %>

<%@ Register TagPrefix="uc1" TagName="InterfaceProgressControl" Src="InterfaceProgressControl.ascx" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <td style="text-align: center" align="center">
                <table style="width: 75%;" class="formGrid">
                    <tr>
                        <td style="text-align: right">
                            *<asp:Label ID="lblUploadType" runat="server">UPLOAD_TYPE</asp:Label>:
                        </td>
                        <td style="text-align: left">
                            <asp:DropDownList ID="ddlUploadType" runat="server" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            *<asp:Label ID="lblFileName" runat="server">Filename</asp:Label>:
                        </td>
                        <td style="text-align: left;">
                            <asp:FileUpload ID="InputFile" CssClass="popupBtn" Width="350" runat="server"></asp:FileUpload>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnLoadFile_WRITE" runat="server" Text="UPLOAD_FILE" width="100" SkinID="ActionButton">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table class="dataGrid">
                    <asp:Panel ID="panelIntProgControl" runat="server">
                        <uc1:InterfaceProgressControl ID="moInterfaceProgressControl" runat="server"></uc1:InterfaceProgressControl>
                        <asp:Button ID="btnAfterProgressBar" Style="display: none" runat="server" Width="0"
                            Height="0"></asp:Button>
                    </asp:Panel>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server" ID="cntMain">
    <div class="dataContainer">
        <div>
            <asp:Panel ID="panelResult" runat="server" Visible="false">
                <table width="100%" class="dataGrid">
                    <tr id="trPageSize" runat="server">
                        <td class="bor" align="left">
                            <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor"
                                runat="server">:</asp:Label>
                            &nbsp;
                            <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="50px"
                                SkinID="SmallDropDown">
                                <asp:ListItem Value="2">2</asp:ListItem>
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
                        <td class="bor" align="right">
                            <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
        <div style="width: 100%">
            <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                SkinID="DetailPageGridView" AllowSorting="true">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <Columns>
                    <asp:BoundField HeaderText="PROCESSING_LOG" DataField="Error_Message" />
                </Columns>
                <PagerSettings PageButtonCount="15" Mode="Numeric" />
                <PagerStyle HorizontalAlign="Center" CssClass="PAGER" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
