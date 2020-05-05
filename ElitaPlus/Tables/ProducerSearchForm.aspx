<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProducerSearchForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ProducerSearchForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script type="text/javascript" language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js">  </script>
    
    <script type="text/javascript">
    function TABLE1_onclick() {

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <!--Start Header-->
    <table id="tblOuter2" border="0" class="searchGrid">
        <tr>
            <td valign="top" align="center" height="100%">
                <asp:Panel ID="WorkingPanel" runat="server">
                    <table id="tblSearch" border="0">
                        <tr>
                            <td valign="top">
                                <table cellspacing="2" cellpadding="2" width="100%" border="0">
                                    <tr>
                                        <td nowrap align="left">
                                            <asp:Label ID="lblDescription" runat="server">Description</asp:Label>:
                                        </td>
                                        <td nowrap align="left">
                                            <asp:Label ID="lblCode" runat="server">Code</asp:Label>:
                                        </td>
                                        <td nowrap align="right">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap align="left">
                                            <asp:TextBox ID="SearchDescriptionTextBox" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                                        </td>
                                        <td nowrap align="left">
                                            <asp:TextBox ID="SearchCodeTextBox" runat="server" AutoPostBack="False" SkinID="SmallTextBox"></asp:TextBox>
                                        </td>
                                        <td nowrap align="right">
                                            <asp:Button ID="btnClearSearch" Text="Clear" SkinID="AlternateLeftButton" runat="server">
                                            </asp:Button>&nbsp;
                                            <asp:Button ID="btnSearch" Text="Search" SkinID="SearchButton" runat="server"></asp:Button>&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container">
        <div class="contentZoneHome">
            <div class="dataContainer">
                <h2 class="dataGridHeader">
                    Search results for Producer</h2>
                <table class="dataGrid" border="0" width="100%">
                    <tr id="trPageSize" runat="server">
                        <td width="60%">
                            <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>:
                            <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" SkinID="ExtraSmallDropDown">
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
                        <td width="40%" align="right">
                            <asp:Label ID="lblRecordCount" class="bor" runat="server"></asp:Label>
                        </td>
                    </tr>
                    </table>
       <div>
                <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
                    AllowPaging="True" SkinID="DetailPageGridView" AllowSorting="True" OnRowCreated="ItemCreated"
                    OnRowCommand="ItemCommand">
                    <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                    <EditRowStyle Wrap="True"></EditRowStyle>
                    <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                    <RowStyle Wrap="True"></RowStyle>
                    <HeaderStyle></HeaderStyle>
                    <Columns>
                        
                        <asp:TemplateField SortExpression="Description" HeaderText="Description">
                            <HeaderStyle HorizontalAlign="Center" Width="55%"></HeaderStyle>
                             <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="SelectAction" Style="cursor: hand" runat="server" Visible="true"
                           OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"    CommandName="SelectAction" CommandArgument="<%#Container.DisplayIndex %>">
                            </asp:LinkButton>
                        </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Code" HeaderText="Code">
                            <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="producer_type" HeaderText="Producer Type">
                            <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False" HeaderText="producer_id"></asp:TemplateField>
                    </Columns>
                    <PagerSettings PageButtonCount="15" Mode="Numeric" Position="TopAndBottom" />
                    <PagerStyle></PagerStyle>
                </asp:GridView>
           
        </div>
        </div></div></div>
        <table>
        <tr>
            <td align="left" class="btnZone" colspan="2">
                <asp:Button ID="btnAdd_WRITE" runat="server" Text="New" SkinID="PrimaryLeftButton"
                    CommandName="WRITE"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
