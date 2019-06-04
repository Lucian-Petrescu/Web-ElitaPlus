<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimIndixSearchForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimIndixSearchForm"  
        Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" EnableSessionState="True"%>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">   
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    
        <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
            <tr>
                <td>
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td>
                                <span class="mandatory">*</span>
                                <asp:Label ID="LabelSearchterm" runat="server">SEARCH_TERM</asp:Label>:
                            </td>
                            <td>
                                <span class="mandatory">*</span>
                                <asp:Label ID="LabelSearchOrder" runat="server">Sort By</asp:Label>:
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:TextBox ID="TextBoxSearchTerm" runat="server" SkinID="MediumTextBox"
                                    AutoPostBack="False"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="cboSortBy" runat="server" SkinID="MediumDropDown">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <label>
                                    <asp:Button ID="btnResetSearch" runat="server" SkinID="AlternateLeftButton" Text="Reset" Visible="true"></asp:Button>
                                    <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear"></asp:Button>
                                    <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                                </label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader"><asp:Label  runat="server" ID="LabelModelName" Text="SEARCH_RESULTS_FOR"></asp:Label></h2>
        <div>
                <table width="100%" class="dataGrid">
                    <tr id="trPageSize" runat="server">
                        <td class="bor" align="left">
                            <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="Label3"
                                runat="server">:</asp:Label>
                            &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="50px"
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
            <asp:Panel runat="server" ID="dataSectionContainer">
                
                <div style="width: 100%">
                    <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                        SkinID="DetailPageGridView" AllowSorting="true" >
                        <SelectedRowStyle Wrap="True" />
                        <EditRowStyle Wrap="True" />
                        <AlternatingRowStyle Wrap="True" />
                        <RowStyle Wrap="True" />
                        <Columns>
                            <asp:BoundField SortExpression="MK" HeaderText="MAKE" HtmlEncode="false" ></asp:BoundField>
                            <asp:BoundField SortExpression="TTL" HeaderText="MODEL_PRODUCT_SPECIFICATIONS" HtmlEncode="false" ></asp:BoundField>
                            <asp:BoundField SortExpression="MNP" HeaderText="MIN_SALE_PRICE" HtmlEncode="false"></asp:BoundField>
                            <asp:BoundField SortExpression="MXP" HeaderText="MAX_SALE_PRICE" HtmlEncode="false"></asp:BoundField>
                        </Columns>
                        <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                    </asp:GridView>

                    <div style="float: right; padding-top:10px;">
                        <asp:Button ID="btnMore" runat="server" SkinID="SearchButton" Text="MORE"></asp:Button>
                    </div>
                </div>
            </asp:Panel>

        </div>
        
    </div>

    <div class="btnZone">
        <div>
            <asp:Button ID="btnBack" SkinID="AlternateLeftButton" TabIndex="5" runat="server"
                Text="Back" />
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        function resizeScroller(item)
        {
            var browseWidth, browseHeight;

            if (document.layers)
            {
                browseWidth = window.outerWidth;
                browseHeight = window.outerHeight;
            }
            if (document.all)
            {
                browseWidth = document.body.clientWidth;
                browseHeight = document.body.clientHeight;
            }

            if (screen.width == "800" && screen.height == "600")
            {
                newHeight = browseHeight - 220;
            }
            else
            {
                newHeight = browseHeight - 975;
            }

            if (newHeight < 470)
            {
                newHeight = 470;
            }

            item.style.height = String(newHeight) + "px";

            return newHeight;
        }

        //resizeScroller(document.getElementById("scroller"));
    </script>
</asp:Content>
