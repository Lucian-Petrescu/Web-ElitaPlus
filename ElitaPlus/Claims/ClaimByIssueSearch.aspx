<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimByIssueSearch.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimByIssueSearch"
    MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
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

        var claimIssueCtlId = '<%=cboClaimIssue.ClientId%>';

        function LoadIssueList(issueTypeId) {
            if (issueTypeId == "00000000-0000-0000-0000-000000000000") {
                $('#' + claimIssueCtlId).empty();
                $('#' + claimIssueCtlId).attr('disabled', 'disabled');
                return;
            } else {
                $('#' + claimIssueCtlId).removeAttr("disabled");
            }
           
            

            //debugger;
            $.ajax({
                type: "POST",
                url: "ClaimByIssueSearch.aspx/GetClaimIssueByType",
                data: '{ issueTypestr: "' + issueTypeId + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(r) {
                    //debugger;
                    var ddlClaimIssue = $('#' + claimIssueCtlId);
                    ddlClaimIssue.empty().append('<option selected="selected" value="">---------------</option>');
                    $.each(r.d,
                        function() {
                            ddlClaimIssue.append($("<option></option>").val(this['Value']).html(this['Text']));
                        });
                }
            });
        }
        
        $(function () {
            var issueTypeId = $('#' + '<%=cboSearchIssueType.ClientId%>' + ' option:selected').val();
            var ddlClaimIssue = $('#' + claimIssueCtlId);

            if (issueTypeId == "00000000-0000-0000-0000-000000000000") {                
                ddlClaimIssue.attr('disabled', 'disabled');
            } else {
                ddlClaimIssue.removeAttr("disabled");
            }
        });
    </script>
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <!-- first line -->
                    <tr>
                        <td align="left">
                            <asp:Label ID="LabelSearchIssueType" runat="server">ISSUE_TYPE</asp:Label>:<span class="mandatory">*</span>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="LabeL1" runat="server">CLAIM_ISSUES</asp:Label>:
                        </td>
                                         
                    </tr>
                    <tr>
                        <td align="left">                            
                            <asp:DropDownList ID="cboSearchIssueType" runat="server" SkinID="MediumDropDown" onchange="LoadIssueList(this.value);" />
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="cboClaimIssue" runat="server" SkinID="LargeDropDown" />
                        </td>
                    </tr>
                    <!-- second line -->
                    <tr>
                        <td>
                            <asp:Label ID="LabeLSearchClaimIssueStatus" runat="server">CLAIM_ISSUE_STATUS</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="LabeLSearchClaimStatus" runat="server">CLAIM_STATUS</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="LabelSearchSDealer" runat="server">DEALER</asp:Label>:
                        </td>                                                
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="cboSearchClaimIssueStatus" runat="server" SkinID="MediumDropDown" />
                        </td>
                        <td>
                            <asp:DropDownList ID="cboSearchClaimStatus" runat="server" SkinID="MediumDropDown" />
                        </td>
                        <td>
                            <asp:DropDownList ID="cboSearchDealer" runat="server" SkinID="MediumDropDown" />
                        </td>                                                
                    </tr>                    
                    <!-- fourth line -->
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server">ISSUE_DATE_FROM</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="Label4" runat="server">ISSUE_DATE_TO</asp:Label>:
                        </td>
                        <td>
                            <%--<asp:Label ID="lblSortBy" runat="server">SORT_BY</asp:Label>:--%>
                        </td>                        
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:TextBox ID="txtIssueAddedFromDate" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                            <asp:ImageButton ID="ImageIssueAddedFromDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" Visible="True" CausesValidation="False" ImageAlign="AbsMiddle"></asp:ImageButton>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtIssueAddedToDate" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                            <asp:ImageButton ID="ImageIssueAddedToDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" Visible="True" CausesValidation="False" ImageAlign="AbsMiddle"></asp:ImageButton>
                        </td>
                        <td align="left">
                            <%--<asp:DropDownList ID="cboSortBy" runat="server" AutoPostBack="False" SkinID="MediumDropDown">
                            </asp:DropDownList>--%>
                        </td>                        
                    </tr>
                    <!-- commands -->
                    <tr>
                        <td></td>
                        <td></td>
                        <td align="right" colspan="2">
                            <label>
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
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader">Search results for claims</h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="Label3" runat="server">:</asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="50px" SkinID="SmallDropDown">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Selected="True" Value="30">30</asp:ListItem>
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
                SkinID="DetailPageGridView" AllowSorting="true">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <Columns>
                    <asp:TemplateField HeaderText="Claim_Number" SortExpression="claim_number">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditClaim" runat="server" OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="Status" HtmlEncode="false" />
                    <asp:BoundField HeaderText="Authorization_Number" HtmlEncode="false" DataField="authorization_number" />
                    <asp:BoundField HeaderText="Service_Center" HtmlEncode="false" DataField="service_center_name"/>
                    <asp:BoundField HeaderText="DEALER" HtmlEncode="false" DataField="dealer_name"/>
                    <asp:BoundField HeaderText="CLAIM_ISSUE_TYPE" HtmlEncode="false" DataField="issue_type"/>
                    <asp:BoundField HeaderText="CLAIM_ISSUE_STATUS" HtmlEncode="false" DataField="issue_status"/>
                    <asp:BoundField HeaderText="ISSUE_DATE" HtmlEncode="false" SortExpression="issue_added_Date" />
                    <asp:BoundField HeaderText="CLAIM_ISSUE_DESCRIPTION" HtmlEncode="false" DataField="issue_description"/>   
                    <asp:BoundField Visible="False" DataField="claim_id"></asp:BoundField>
                    <asp:BoundField Visible="False" DataField="claim_auth_type_id"></asp:BoundField>
                    <asp:BoundField Visible="False" DataField="status_code"></asp:BoundField>
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
