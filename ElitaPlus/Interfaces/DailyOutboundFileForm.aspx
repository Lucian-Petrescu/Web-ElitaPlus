<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    EnableSessionState="True" CodeBehind="DailyOutboundFileForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.DailyOutboundFileForm"
    Theme="Default" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">
        function DealerChangeDescription() {
            var dealerdropdownlist = document.getElementById('ctl00_SummaryPlaceHolder_drpDealer');
            var dealerdescription = dealerdropdownlist.options[dealerdropdownlist.selectedIndex].value;
            document.getElementById('ctl00_SummaryPlaceHolder_txtDeaDescription').value = dealerdescription;
            return false;
        }
        function CompanyChangeDescription() {
            var companydropdownlist = document.getElementById('ctl00_SummaryPlaceHolder_drpCompany');
            var companydescription = companydropdownlist.options[companydropdownlist.selectedIndex].value;
            document.getElementById('ctl00_SummaryPlaceHolder_txtComDescription').value = companydescription;
            return false;
        }

        //        if (document.getElementById("checkboxParentHidden").value == "selectAllChecked") {
        //            document.getElementById("grid_ctl01_chkAll").checked = true;

        //            SelectAllCheckboxes(document.getElementById("grid_ctl01_chkAll"));

        //        }

        function SelectAllCheckboxes(selectAllCheckbox) {

            document.getElementById("ctl00_BodyPlaceHolder_HiddenIsCheckBoxEdited").value = '0';

            if (selectAllCheckbox.checked) {
                document.getElementById("ctl00_BodyPlaceHolder_checkboxParentHidden").value = "selectAllChecked";
                document.getElementById("ctl00_BodyPlaceHolder_Grid_ctl02_HeaderChkBx").checked = true;
            }
            else {
                document.getElementById("ctl00_BodyPlaceHolder_checkboxParentHidden").value = "selectAllNotChecked";
                document.getElementById("ctl00_BodyPlaceHolder_Grid_ctl02_HeaderChkBx").checked = false;
            }
            document.getElementById("ctl00_BodyPlaceHolder_HiddenIsCheckBoxEdited").value = '0';
            var grid = document.getElementById("ctl00_BodyPlaceHolder_Grid");
            var cell;
            if (grid.rows.length > 0) {

                for (i = 1; i < grid.rows.length; i++) {

                    cell = grid.rows[i].cells[0];

                    for (j = 0; j < cell.childNodes.length; j++) {
                        //if childNode type is CheckBox                 
                        if (cell.childNodes[j].type == "checkbox") {
                            //assign the status of the Select All checkbox to the cell checkbox within the grid
                            cell.childNodes[j].checked = document.getElementById("ctl00_BodyPlaceHolder_Grid_ctl02_HeaderChkBx").checked;
                        }
                    }


                }

            }
            return true;
        }

        function selectUnchecked() {
            document.getElementById("ctl00_BodyPlaceHolder_HiddenIsCheckBoxEdited").value = '1';
            //alert('10');
            return true;
        }
         

    </script>
    <table width="100%" border="0" class="searchGrid" id="searchTable1" runat="server">
        <%--<tr>
            <td>
                <table width="100%" border="0">--%>
        <tr>
            <td>
                <asp:Label ID="moCompany" runat="server">COMPANY</asp:Label><br />
                <asp:DropDownList ID="drpCompany" runat="server" SkinID="SmallDropDown" onchange="return CompanyChangeDescription();">
                </asp:DropDownList>
                <br />
            </td>
            <td>
                <asp:Label ID="lblComDescription" runat="server">DESCRIPTION</asp:Label><br />
                <asp:TextBox ID="txtComDescription" runat="server" SkinID="MediumTextBox" AutoPostBack="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="moDealer" runat="server">DEALER</asp:Label><br />
                <asp:DropDownList ID="drpDealer" runat="server" SkinID="SmallDropDown" onchange="return DealerChangeDescription();">
                </asp:DropDownList>
                <br />
            </td>
            <td>
                <asp:Label ID="lblDeaDescription" runat="server">DESCRIPTION</asp:Label><br />
                <asp:TextBox ID="txtDeaDescription" runat="server" SkinID="MediumTextBox" AutoPostBack="false"></asp:TextBox>
            </td>
        </tr>
        <%-- </table>
            </td>
        </tr>--%>
        <%-- <tr>
        <td style="border-bottom-style:dashed; border-bottom-color:#999";></td>
    </tr>--%>
    </table>
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <td>
                <asp:Label ID="lblcertificate" runat="server">CERTIFICATE</asp:Label><br />
                <asp:TextBox ID="txtCertificatenumb" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
            </td>
            <td colspan="3">
                <table width="100%" border="0">
                    <tr>
                        <td nowrap="nowrap" align="left">
                            <span class="mandate">* </span>
                            <asp:Label ID="lblbegindate" runat="server">BEGIN_DATE</asp:Label>
                            <br />
                            <asp:TextBox ID="txtbegindate" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                            <asp:ImageButton ID="Imagebtnbegindate" TabIndex="2" runat="server" Visible="True"
                                ImageUrl="~/App_Themes/Default/Images/calendar.png" ImageAlign="AbsMiddle"></asp:ImageButton>
                        </td>
                        <td nowrap="nowrap" align="left">
                            <span class="mandate">* </span>
                            <asp:Label ID="lblEnddate" runat="server">END_DATE</asp:Label>
                            <br />
                            <asp:TextBox ID="txtEnddate" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                            <asp:ImageButton ID="ImageBbtnEndDate" TabIndex="2" runat="server" Visible="True" 
                                ImageUrl="~/App_Themes/Default/Images/calendar.png" ImageAlign="AbsMiddle"></asp:ImageButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblnewenrolment" runat="server">NEW_ENROLLMENTS</asp:Label>
                            <asp:CheckBox ID="chknewenrolment" runat="server" AutoPostBack="False"></asp:CheckBox>
                        </td>
                        <td>
                            <asp:Label ID="lblCancellations" runat="server">CANCELLATIONS</asp:Label>
                            <asp:CheckBox ID="chkcancellations" runat="server" AutoPostBack="False"></asp:CheckBox>
                        </td>
                        <td>
                            <asp:Label ID="Label3" runat="server">BILLING</asp:Label>
                            <asp:CheckBox ID="chkbilling" runat="server" AutoPostBack="False"></asp:CheckBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label>
                                <asp:Button ID="btnView" runat="server" SkinID="AlternateLeftButton" Text="ViewSelectedRecords" />
                            </label>
                        </td>
                        <td colspan="4" align="right">
                            <label>
                                <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                                </asp:Button>
                                <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                                <%--<asp:Button ID= "btnview" runat="server" SkinID="AlternateRightButton" Text="ViewSelectedRecords" />--%>
                            </label>
                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="4" align="left">
                            <label>
                                <asp:Button ID="btnView" runat="server" SkinID="AlternateLeftButton" Text="ViewSelectedRecords" />
                            </label>
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            Search results for Outbound File</h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor"
                            runat="server">:</asp:Label>
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
            <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                DataKeyNames="Company_id, Dealer_id,cert_id,Record_type,created_date,created_by,File_Detail_Id,Billing_Detail_Id,File_Detail_Temp_Id"
                SkinID="DetailPageGridView" AllowSorting="true" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
                <SelectedRowStyle Wrap="true" />
                <EditRowStyle Wrap="true" />
                <AlternatingRowStyle Wrap="true" />
                <RowStyle Wrap="true" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Center" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="HeaderChkBx" runat="server" Text="Select/UnselectAll" onclick="SelectAllCheckboxes(this)"
                                OnCheckedChanged="chkbxHeader_checkChanged" AutoPostBack="true" />
                            <%--<input id="chkAll" onclick="SelectAllCheckboxes(this);" runat="server" type="checkbox" />--%>
                        </HeaderTemplate>
                        <asp:ItemStyle HorizontalAlign="Center" Width="5%" />
                        <ItemTemplate>
                            <asp:CheckBox ID="SelectChkBx" runat="server" onclick="javascript:return selectUnchecked();" 
                            OnCheckedChanged="RememberOldValues" AutoPostBack="true"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CERT_NUMBER" HeaderText="Certificate" SortExpression="CERT_NUMBER"
                        HtmlEncode="false" />
                    <asp:BoundField DataField="CERT_CREATED_DATE" HeaderText="Created Date" SortExpression="CERT_CREATED_DATE"
                        HtmlEncode="false" />
                    <asp:BoundField DataField="REC_NEW_BUSINESS" HeaderText="New Enrollments" SortExpression="REC_NEW_BUSINESS"
                        HtmlEncode="false" />
                    <asp:BoundField DataField="REC_CANCEL" HeaderText="Cancelations" SortExpression="REC_CANCEL"
                        HtmlEncode="false" />
                    <asp:BoundField DataField="REC_BILLING" HeaderText="Billing" SortExpression="REC_BILLING"
                        HtmlEncode="false" />
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" />
            </asp:GridView>
        </div>
        <div class="btnZone">
            <asp:Button ID="btnSaveRecords" TabIndex="11" runat="server" SkinID="PrimaryRightButton"
                Text="Save"></asp:Button>
            <asp:Button ID="btnDelectRecords" TabIndex="11" runat="server" SkinID="PrimaryLeftButton"
                Text="Delete"></asp:Button>
        </div>
    </div>
    <input type="hidden" name="checkboxParentHidden" id="checkboxParentHidden" runat="server" />
    <input type="hidden" name="checkboxParentHidden" id="HiddenIsCheckBoxEdited" runat="server" />
</asp:Content>
