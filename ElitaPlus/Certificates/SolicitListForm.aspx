<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SolicitListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.SolicitListForm" 
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
 
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js" ></script>
    <script language="JavaScript" type="text/javascript" src="../Navigation/Scripts/GlobalHeader.js"></script>
    <script language="JavaScript" type="text/javascript">
        window.latestClick = '';

        function isNotDblClick()
        {
            if (window.latestClick != "clicked")
            {
                window.latestClick = "clicked";
                return true;
            } else
            {
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
   
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <td colspan="3"  width="85%">
                <table width="100%" border="0">
                    <asp:Panel ID="SOLICITNUM" runat="server" Visible="true">
                        <tr>
                            <td>
                                <asp:Label ID="lblddlDealer" runat="server">Dealer</asp:Label><br />
                                <asp:DropDownList ID="ddlDealer" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                                </asp:DropDownList>
                            </td>
                            <td>
                                
                                <asp:Label ID="lblInitialSalesOrder" runat="server">INTIAL_SALES_ORDER</asp:Label><br />
                                <asp:TextBox ID="txtInitialSalesOrder" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                            <%--<td>
                                <asp:Label ID="lblStatus" runat="server">STATUS</asp:Label><br />
                                <asp:DropDownList ID="moStatusDrop" runat="server" SkinID="SmallDropDown" AutoPostBack="False">
                                    <asp:ListItem Text="" Value=""></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="Expired" Value="E"></asp:ListItem>
                                    <asp:ListItem Text="Converted" Value="E"></asp:ListItem>
                                </asp:DropDownList>
                            </td>--%>
                            <td>
                                <asp:Label ID="lblCustomerId" runat="server">CUSTOMER_ID</asp:Label><br />
                                <asp:TextBox ID="txtCustomerId" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td>
                                <asp:Label ID="lblDateOfBirth" runat="server">DATE_OF_BIRTH</asp:Label><br />                           
                                <asp:TextBox ID="txtDateOfBirth" TabIndex="1" runat="server" SkinID="smallTextBox"></asp:TextBox>                           
                                <asp:ImageButton ID="btnDateOfBirth" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                    valign="bottom"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:Label ID="lblSimPhoneNumber" runat="server">SIM_PHONE_NUMBER</asp:Label><br />
                                <asp:TextBox ID="txtSimPhoneNumber" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            </td>
                        </tr>
                    </asp:Panel>
                    
                    <tr align="right">
                         <td>&nbsp;</td>
                         <td>&nbsp;</td>
                         <td>
                      
                            <label>
                                <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                                </asp:Button>
                                <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                            </label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js" ></script>
    <script language="JavaScript" type="text/javascript" src="../Navigation/Scripts/GlobalHeader.js"></script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="Server" />
    <!-- new layout start -->
  
            <div class="dataContainer" runat="server" id="DataGridPlaceHolder">
                <div style="width: 100%">
                    <asp:Xml ID="xmlSource" runat="server"></asp:Xml>
                </div>
           <div>
               <asp:HiddenField ID="selectedSolicitId" runat="server" Value="XXXX" />
               <asp:HiddenField ID="CurrentPage" runat="server" Value="1" />
           </div>
    </div>

    
    <!-- end new layout -->
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
        function SelectSolicitId(theID) {
            var selectedSolicitId = '<%=selectedSolicitId.ClientID%>';;
            document.getElementById(selectedServiceCenterId).value = theID;
        }

        function openClose(theID) {
            if (document.getElementById(theID).style.display == "block") {
                document.getElementById(theID).style.display = "none";
                document.getElementById("tick_" + theID).innerHTML = "+";
            }
            else {
                document.getElementById(theID).style.display = "block";
                document.getElementById("tick_" + theID).innerHTML = "-";
            }
        }

        function showHidePage(newPageNumber, recordCount, pageSize) {
            var newPage = parseInt(newPageNumber);
            var currentPageId = '<%=CurrentPage.ClientID%>';
            var currentPage = parseInt(document.getElementById(currentPageId).value);
            var tempLocation;
            if (newPageNumber == document.getElementById(currentPageId).value) { return; }
            document.getElementById('pg1_' + document.getElementById(currentPageId).value).style.cursor = 'pointer';
            document.getElementById('pg1_' + document.getElementById(currentPageId).value).setAttribute('class', '');
            document.getElementById('pg2_' + document.getElementById(currentPageId).value).style.cursor = 'pointer';
            document.getElementById('pg2_' + document.getElementById(currentPageId).value).setAttribute('class', '');
            document.getElementById(currentPageId).value = newPageNumber;
            document.getElementById('pg1_' + document.getElementById(currentPageId).value).style.cursor = 'text';
            document.getElementById('pg1_' + document.getElementById(currentPageId).value).setAttribute('class', 'selected_page');
            document.getElementById('pg2_' + document.getElementById(currentPageId).value).style.cursor = 'text';
            document.getElementById('pg2_' + document.getElementById(currentPageId).value).setAttribute('class', 'selected_page');
            for (i = 1; i <= pageSize; i++) {
                tempLocation = (((currentPage - 1) * pageSize) + i);
                if (tempLocation < recordCount) {
                    document.getElementById('tr_' + tempLocation).style.display = "none";
                    document.getElementById('trd_' + tempLocation).style.display = "none";
                }
                tempLocation = (((newPage - 1) * pageSize) + i);
                if (tempLocation < recordCount) {
                    document.getElementById('tr_' + tempLocation).style.display = "block";
                    document.getElementById('trd_' + tempLocation).style.display = "block";
                }
            }
            SelectServiceCenter('XXXX');
        }

        if (document.getElementById('pg1_1') != null) {
            document.getElementById('pg1_1').style.cursor = 'text';
            document.getElementById('pg1_1').setAttribute('class', 'selected_page');
        }

        if (document.getElementById('pg2_1') != null) {
            document.getElementById('pg2_1').style.cursor = 'text';
            document.getElementById('pg2_1').setAttribute('class', 'selected_page');
        }
        //resizeScroller(document.getElementById("scroller"));
    </script>
</asp:Content>
