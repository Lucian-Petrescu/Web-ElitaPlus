<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ScheduleSearchForm.aspx.vb" Theme="Default" 
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ScheduleSearchForm"
    EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" type="text/javascript">
        window.latestClick = '';

        function isNotDblClick() {
            if (window.latestClick != "clicked") {
                window.latestClick = "clicked";
                return true;
            } else {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
    <Elita:MessageController runat="server" ID="moMessageController" />
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
   <asp:PlaceHolder ID="PlaceHolder1" runat="server">
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <td>
            <asp:Label runat="server" ID="moScheduleCodeLabel">Code</asp:Label> <br />
            </td>
            <td>
            <asp:Label runat="server" ID="moScheduleDescriptionLabel">Description</asp:Label> <br />
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:TextBox ID="moScheduleCode" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
            </td>
            <td align="left">
                <asp:TextBox ID="moScheduleDescription" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
            </td>
            <td align="left">
                <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                </asp:Button>
                <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
            </td>
        </tr>
    </table>
   </asp:PlaceHolder>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
 <asp:PlaceHolder ID="PlaceHolder2" runat="server">
   <div ID="divDataContainer" class="dataContainer" runat="server">
        <h2 class="dataGridHeader"> 
            <asp:Label ID="lblSearchResults"  runat="server" Text = "SEARCH_RESULTS_FOR_SCHEDULES"></asp:Label>
        </h2> 
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor" runat="server">:</asp:Label> &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true"
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
            <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" AutoGenerateColumns="False" AllowPaging="True"
                SkinID="DetailPageGridView" AllowSorting="true">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <HeaderStyle   />
                <Columns>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:Label ID="lblScheduleID" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("schedule_id"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CODE" SortExpression="CODE">
                        <ItemTemplate>
                            <asp:LinkButton CommandName="SelectAction" ID="btnEditSchedule" runat="server" 
                                OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DESCRIPTION" SortExpression="DESCRIPTION">
                        <ItemTemplate>
                         <asp:Label ID="lblDescription" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Description") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                 </Columns>
                <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle/>
            </asp:GridView>          
        </div>         
    </div>
    <div class="btnZone">                       
        <asp:Button ID="BtnNew_WRITE" runat="server" Text="Add_New" SkinID="AlternateLeftButton"></asp:Button>
    </div>       
    <!-- end new layout -->
    <script language="javascript" type="text/javascript">
        function resizeScroller(item) {
            var browseWidth, browseHeight;

            if (document.layers) {
                browseWidth = window.outerWidth;
                browseHeight = window.outerHeight;
            }
            if (document.all) {
                browseWidth = document.body.clientWidth;
                browseHeight = document.body.clientHeight;
            }

            if (screen.width == "800" && screen.height == "600") {
                newHeight = browseHeight - 220;
            }
            else {
                newHeight = browseHeight - 975;
            }

            if (newHeight < 470) {
                newHeight = 470;
            }

            item.style.height = String(newHeight) + "px";

            return newHeight;
        }

        //resizeScroller(document.getElementById("scroller"));
    </script>
  </asp:PlaceHolder>
</asp:Content>