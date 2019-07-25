<%@ Page Title="Claim File Management Search Form" Language="vb" ValidateRequest="false" AutoEventWireup="false" 
    MasterPageFile="~/Navigation/masters/ElitaBase.Master" CodeBehind="ClaimFileManagementSearchForm.aspx.vb" 
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimFileManagementSearchForm" Theme="Default" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">

</asp:Content>

<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">

</asp:Content>

<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">

</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
    </asp:ScriptManager>
    <div class="dataContainer" style="margin-left: 0%">
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor" runat="server">:</asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true"
                            SkinID="SmallDropDown">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10" Selected="True">10</asp:ListItem>
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
        <div class="dataContainer" style="margin-left: 0%">
            <h2 class="dataGridHeader">
                <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS</asp:Label>
            </h2>
            <div class="Page" runat="server" id="moDataGrid_WRITE1" style="height: 100%; overflow: auto; width: 100%;">
                <asp:GridView ID="DataGridView" runat="server" Width="100%" AllowPaging="True" AllowCustomPaging="true" AllowSorting="False"
                    CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView"> 
                    <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                    <EditRowStyle Wrap="False"></EditRowStyle>
                    <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                    <RowStyle Wrap="False"></RowStyle>
                    <HeaderStyle Wrap="False"></HeaderStyle>
                    <Columns>
                        <asp:BoundField Visible="False" DataField="FileIdentifier" HeaderText="File_Processed_Id"></asp:BoundField>     
                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgSelect" runat="server" ImageUrl="~/Navigation/images/icons/yes_icon.gif" CommandName="SelectRecord"
                                    CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false" Enabled="False" />
                                <asp:LinkButton ID="btnSelect" runat="server" CommandName="SelectRecord" 
                                    CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false">Select</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LogicalFileName" HeaderText="Filename">
                            <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ReceivedRecords" HeaderText="Received">
                            <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="1%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="CountedRecords" HeaderText="Counted">
                            <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="1%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Rejected">
                            <HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="BtnShowRejected" runat="server" CommandName="ShowRecordRejected" 
                                    CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="Validated">
                            <HeaderStyle Wrap="False" Width="1%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:LinkButton ID="BtnShowValidated" runat="server" CommandName="ShowRecordValidated" 
                                    CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="QueuedRecords" HeaderText="Requeued">
                            <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="1%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ProcessedRecords" HeaderText="Processed">
                            <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="1%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Status">
                            <HeaderStyle Wrap="False" Width="1%" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
		                    <ItemTemplate>
			                    <asp:Label ID="BtnShowStatus" runat="server"/>
		                    </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings PageButtonCount="15" Mode="Numeric" Position="TopAndBottom" />
               </asp:GridView>
            </div>
        </div>
    </div>
    <%-- <div class="btnZone"> --%>
    <div class="btnZone">
        <asp:Panel ID="moButtonPanel" runat="server" Visible="True">
            <asp:Button ID="BtnRefresh" runat="server" Enabled="true" CausesValidation="False"  
                Text="REFRESH" SkinID="AlternateLeftButton"></asp:Button>&nbsp;
            <asp:Button ID="BtnReprocessFile" runat="server" Enabled="false" CausesValidation="False"  
                Text="REPROCESS" SkinID="AlternateLeftButton"></asp:Button>&nbsp;
            <asp:Button ID="BtnDeleteFile" runat="server" Enabled="false" CausesValidation="False"
                Text="DELETE" SkinID="AlternateLeftButton"></asp:Button>
        </asp:Panel>
    </div>
</asp:Content>
