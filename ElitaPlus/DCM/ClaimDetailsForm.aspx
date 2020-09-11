<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimDetailsForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimDetailsForm" Theme="Default"
    EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script type="text/javascript">        
        $(function () {
            var disabledTabs = $("input[id$='hdnDisabledTabs']").val().split(',');
            var disabledTabsIndexArr = [];
            var isCertHistLoaded = 0;
            $.each(disabledTabs, function () {
                var tabIndex = parseInt(this);
                if (tabIndex != NaN) {
                    disabledTabsIndexArr.push(tabIndex);
                }
            });
            $("#tabs").tabs({
                activate: function() {
                    var selectedTab = $('#tabs').tabs('option', 'active');
                    $("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);

                },
                active: document.getElementById('<%= hdnSelectedTab.ClientID %>').value,            
                disabled: disabledTabsIndexArr
            });
            $("#tabs").removeAttr('style');
        });

        function UpdatePanelLoaded(sender, args) {            
            if (_isSubmitting) {
                _isSubmitting = false;
                document.body.style.cursor = '';
            } 
        }
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
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table border="0" class="summaryGrid" cellpadding="0" cellspacing="0">
        <tr>
            <td align="right" nowrap="nowrap">
                <asp:Label ID="lblCustomerName" runat="server" SkinID="SummaryLabel">CUSTOMER_NAME</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" colspan="5"> 
                <asp:Label ID="lblCustomerNameValue" runat="server" Font-Bold="true"><%#NoData%></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" nowrap="nowrap">
                <asp:Label ID="lblClaimNumber" runat="server" SkinID="SummaryLabel">CLAIM_#</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight">
                <asp:Label ID="lblClaimNumberValue" runat="server" SkinID="SummaryLabel"><%#NoData%></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblDealerName" runat="server" SkinID="SummaryLabel">DEALER_NAME</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight">
                <asp:Label ID="lblDealerNameValue" runat="server" SkinID="SummaryLabel"><%#NoData%></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblClaimStatus" runat="server" SkinID="SummaryLabel">CLAIM_STATUS</asp:Label>:
            </td>
            <td id="ClaimStatusTD" runat="server" align="left" nowrap="nowrap" class="padRight">
                <asp:Label ID="lblClaimStatusValue" runat="server" SkinID="SummaryLabel"><%#NoData%></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" nowrap="nowrap">
                <asp:Label ID="lblWorkPhoneNumber" runat="server" SkinID="SummaryLabel">WORK_CELL_NUMBER</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight">
                <asp:Label ID="lblWorkPhoneNumberValue" runat="server" SkinID="SummaryLabel"><%#NoData%></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblDealerGroup" runat="server" SkinID="SummaryLabel">DEALER_GROUP</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight">
                <asp:Label ID="lblDealerGroupValue" runat="server" SkinID="SummaryLabel"><%#NoData%></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblSubscriberStatus" runat="server" SkinID="SummaryLabel">SUBSCRIBER_STATUS</asp:Label>:
            </td>
            <td id="SubStatusTD" align="left" nowrap="nowrap" class="padRight" runat="server">
                <asp:Label ID="lblSubscriberStatusValue" runat="server" SkinID="SummaryLabel"><%#NoData%></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblCertificateNumber" runat="server" SkinID="SummaryLabel">CERTIFICATE_NUMBER</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight">
                <asp:Label ID="lblCertificateNumberValue" runat="server" SkinID="SummaryLabel"><%#NoData%></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblSerialNumberImei" runat="server" SkinID="SummaryLabel">SERIAL_NUMBER_IMEI</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="bor padRight">
                <asp:Label ID="lblSerialNumberImeiValue" runat="server" SkinID="SummaryLabel"><%#NoData%></asp:Label>
            </td>
            <td align="right" nowrap="nowrap" class="padLeft">
                <asp:Label ID="lblDateOfLoss" runat="server" SkinID="SummaryLabel">DATE_OF_LOSS</asp:Label>:
            </td>
            <td align="left" nowrap="nowrap" class="padRight">
                <asp:Label ID="lblDateOfLossValue" runat="server" SkinID="SummaryLabel"><%#NoData%></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js"></asp:ScriptReference>
        </Scripts>
    </asp:ScriptManager>
        <div class="dataContainer">
            <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0"></asp:HiddenField>
            <asp:HiddenField ID="hdnDisabledTabs" runat="server" Value=""></asp:HiddenField>            
            <div id="tabs" class="style-tabs" style="display: none;">
                <ul>
                    <li><a href="#tabsCaseList" rel="noopener noreferrer"><asp:Label ID="Label8" runat="server" CssClass="tabHeaderText">CASE_LIST</asp:Label></a></li>
                    <li><a href="#tabsActionInfo" rel="noopener noreferrer"><asp:Label ID="Label17" runat="server" CssClass="tabHeaderText">CASE_ACTION</asp:Label></a></li>
                </ul>
                <div id="tabsCaseList">
                    <table class="dataGrid" border="0" width="100%">
                        <tr>
                            <td width="40%" align="right">
                                <asp:Label ID="lblClaimCaseRecordFound" class="bor" runat="server"></asp:Label>
                            </td>
                        </tr>
                     </table>
                     <div class="Page" runat="server" id="ClaimCaseListTabPanel" style="display: block;
                            height: 300px; overflow: auto">
                            <asp:GridView ID="ClaimCaseListGrid" runat="server" Width="100%" AllowPaging="false" AllowSorting="false" SkinID="DetailPageGridView">
                                <Columns>
                                   <asp:TemplateField HeaderText="case_number" SortExpression="case_number">
                                        <ItemTemplate>
                                            <asp:LinkButton CommandName="SelectAction" ID="btnSelectCase" runat="server" OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="case_purpose" DataField="case_purpose" SortExpression="case_purpose" HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="case_open_date" DataField="case_open_date" SortExpression="case_open_date" HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="case_status" DataField="case_status" SortExpression="case_status" HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="case_close_date" DataField="case_close_date" SortExpression="case_close_date" HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="first_name" DataField="first_name" SortExpression="first_name" HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="last_name" DataField="last_name" SortExpression="last_name" HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="channel" DataField="channel" SortExpression="channel" HtmlEncode="false">
                                    </asp:BoundField>
                                </Columns>
                                <PagerSettings PageButtonCount="30" Mode="Numeric"></PagerSettings>
                            </asp:GridView>
                        </div>
                </div>
                                
                <div id="tabsActionInfo">
                    <table class="dataGrid" border="0" width="100%">
                        <tr>
                            <td width="40%" align="right">
                                <asp:Label ID="lblClaimActionRecordFound" class="bor" runat="server"></asp:Label>
                            </td>
                        </tr>
                     </table>
                     <div class="Page" runat="server" id="ClaimActionTabPanel" style="display: block;
                            height: 300px; overflow: auto">
                            
                            <asp:GridView ID="ClaimActionGrid" runat="server" Width="100%" AllowPaging="false" AllowSorting="false" SkinID="DetailPageGridView">
                                <Columns>
                                    <asp:BoundField HeaderText="action_owner" DataField="action_owner" SortExpression="action_owner" HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="action_type" DataField="action_type" SortExpression="action_type" HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="document_type_descr" DataField="document_type_descr" SortExpression="document_type_descr" HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="status" DataField="status" SortExpression="status" HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="created_date" DataField="created_date" SortExpression="created_date" HtmlEncode="false">
                                    </asp:BoundField>
                                </Columns>
                                <PagerSettings PageButtonCount="30" Mode="Numeric"></PagerSettings>
                            </asp:GridView>
                     </div>
                </div> 
            </div>
        </div>
        <div class="btnZone">
            <div class="">
                <asp:Button ID="btnBack" runat="server" Text="Back" SkinID="AlternateLeftButton">
                </asp:Button>
            </div>
        </div>
</asp:Content>
ent>
