<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimFulfillmentMileStonesFileForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.ClaimFulfillmentMileStonesFileForm" Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register Src="../Common/MultipleColumnDDLabelControl_new.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc1" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">

    <table width="100%" border="0" class="searchGrid" id="searchTable1" runat="server">
        <tr>
            <td valign="middle" width="30%" style="height: 40px">
                <table>
                    <tr>
                    <td>
                        <asp:Label ID="lblCountry" runat="server">COUNTRY:</asp:Label><br />
                        <asp:DropDownList ID="ddlCountry" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label ID="lblSvcCntrName" runat="server">SERVICE_CENTER_NAME:</asp:Label><br />
                        <asp:DropDownList ID="ddlServiceCenter" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                        </asp:DropDownList>
                    </td>
                        <td style="height: 40px" valign="middle" align="right" width="40%" nowrap="nowrap">
                            <br />
                            <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear"></asp:Button>
                            <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_CLAIM_FULFILLMENT_MILESTONES_FILE</asp:Label>
        </h2>
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
        <div style="width: 100%">
            <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="Grid_RowCreated" OnRowCommand="Grid_OnRowCommand"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView" PageSize="15">
                <SelectedRowStyle Wrap="true" />
                <EditRowStyle Wrap="true" />
                <AlternatingRowStyle Wrap="true" />
                <RowStyle Wrap="true" />
                <Columns>
                    <asp:TemplateField SortExpression="LogicalFileName" HeaderText="FILE_NAME" >
                        <ItemTemplate>
                             <asp:LinkButton ID="btnGridShow" runat="server" CommandArgument="<%#Container.DisplayIndex %>"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>          
                    <asp:BoundField DataField="ReadTimestampUtc" HeaderText="FILE_DATE" />
                    <asp:BoundField DataField="ReceivedRecords" HeaderText="RECEIVED" />
                    <asp:BoundField DataField="CountedRecords" HeaderText="COUNTED" />
                    <asp:BoundField DataField="ValidatedRecords" HeaderText="VALIDATED" />
                    <asp:BoundField DataField="ProcessedRecords" HeaderText="PROCESSED" />                 
                    <asp:TemplateField Visible="False" HeaderText="FILE_PROCESSED_ID"></asp:TemplateField>

                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" />
            </asp:GridView>
        </div>
    </div>

</asp:Content>
