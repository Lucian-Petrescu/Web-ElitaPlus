<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="WebServiceFunctionsListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Security.WebServiceFunctionsListForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <style type="text/css">
        tr.HEADER th
        {
            text-align: center;
        }
    </style>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td colspan="4" style="text-align: center; width: 100%">
                <table cellpadding="4" cellspacing="0" border="0" style="border: #999999 1px solid;
                    background-color: #f1f1f1; height: 98%; width: 100%">
                    <tr>
                        <td style="height: 7px;" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                            <asp:Label ID="SearchEnvironmentLabel" runat="server">Web_Service_Name</asp:Label>:
                        </td>
                        <td style="text-align: Left; white-space: nowrap; vertical-align: middle; width: 20%">
                            <asp:TextBox ID="txtWebServiceName" runat="server" Width="180px" AutoPostBack="False"
                                CssClass="FLATTEXTBOX" Enabled="False"></asp:TextBox>
                        </td>
                        <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                            <asp:Label ID="SearchDescriptionLabel" runat="server">On_Line_Status</asp:Label>:&nbsp;
                        </td>
                        <td style="text-align: left; white-space: nowrap; vertical-align: middle; width: 40%">
                            <asp:TextBox ID="txtOnLineStatus" runat="server" Width="180px" AutoPostBack="False"
                                CssClass="FLATTEXTBOX" Enabled="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap align="right">
                            &nbsp;
                        </td>
                        <td nowrap style="text-align: right" colspan="4">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <hr style="height: 1px" />
            </td>
        </tr>
        <tr id="trPageSize" runat="server">
            <td style="height: 22px; vertical-align: middle" align="left">
                <asp:Label ID="lblPageSize" runat="server">Page Size</asp:Label>: &nbsp;
                <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
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
            <td style="height: 22px; text-align: right; vertical-align: middle">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" OnRowCommand="RowCommand"
                    AllowPaging="True" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
                    CssClass="DATAGRID">
                    <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                    <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                    <RowStyle CssClass="ROW"></RowStyle>
                    <HeaderStyle CssClass="HEADER"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblWebservice_function_id" runat="server" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                    
                        <asp:TemplateField HeaderStyle-Width="28px">
                            <HeaderStyle Width="20px"></HeaderStyle>
                            <ItemStyle Width="20px" CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:ImageButton Style="cursor: hand;" ID="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
                                    runat="server" CommandName="EditRecord" CommandArgument="<%# Container.DisplayIndex %>">
                                </asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="false">
                            <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton_WRITE" runat="server" CausesValidation="False"
                                    CommandName="DeleteRecord" ImageUrl="~/Navigation/images/icons/trash.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField HeaderStyle-Width="28px">
                            <HeaderStyle Width="28px"></HeaderStyle>
                            <ItemStyle Width="28px" CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Button ID="btnActionStart" Width="70px" Height="20px" runat="server" Style="background-image: url(../Navigation/images/icons/Start_icon.png);
                                    cursor: hand; background-repeat: no-repeat" CommandName="Start" CssClass="FLATBUTTON" Text="Start" CommandArgument="<%#Container.DisplayIndex %>" >
                                </asp:Button>
                                <asp:Button ID="btnActionStop" Width="70px" Height="20px" runat="server" Style="background-image: url(../Navigation/images/icons/Stop_icon.png);
                                    cursor: hand; background-repeat: no-repeat" CommandName="Stop" CssClass="FLATBUTTON" Text="Stop" CommandArgument="<%#Container.DisplayIndex %>" >
                                </asp:Button>
                            </ItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="On_Line_Status">
                            <ItemStyle HorizontalAlign="CENTER"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="On_Line_StatusLabel" runat="server" Visible="True"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                            <asp:DropDownList id ="cboOn_Line_Status" runat="server" Style="width: 135px; overflow: hidden"></asp:DropDownList>
                            <asp:Label ID="On_Line_StatusLabelEdit" runat="server" Visible="True" ></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>                                                
                        <asp:TemplateField SortExpression="function_name" HeaderText="web_service_function_name">
                            <ItemStyle HorizontalAlign="CENTER"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="web_service_function_nameLabel" runat="server" Visible="True"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList id ="cboweb_service_function_name" runat="server" Style="width: 180px; overflow: hidden"></asp:DropDownList>
                                <asp:Label ID="web_service_function_nameLabelEdit" runat="server" Visible="True" ></asp:Label>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="last_operation_date">
                            <ItemStyle HorizontalAlign="CENTER"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="last_operation_dateLabel" runat="server" Visible="True"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="User Name">
                            <ItemStyle HorizontalAlign="CENTER"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="last_change_byLabel" runat="server" Visible="True"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  HeaderText="Off_Line_Message">
                            <ItemStyle HorizontalAlign="CENTER"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="Off_Line_MessageLabel" runat="server" Visible="True" ></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="Off_Line_MessageTextBox" CssClass="FLATTEXTBOX_TAB" runat="server"
                                    Visible="True" widht="180px"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
<asp:Button ID="btnBack" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_BACK" Text="BACK">
    </asp:Button>&nbsp;
<asp:Button ID="NewButton_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                    cursor: hand; background-repeat: no-repeat" runat="server" Width="100px" Text="New"
                    Height="20px" CssClass="FLATBUTTON"></asp:Button>
                <asp:Button ID="SaveButton_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                    cursor: hand; background-repeat: no-repeat" runat="server" Width="100px" Text="Save"
                    Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
                <asp:Button ID="CancelButton" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                    cursor: hand; background-repeat: no-repeat" runat="server" Width="100px" Text="Cancel"
                    Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
</asp:Content>
