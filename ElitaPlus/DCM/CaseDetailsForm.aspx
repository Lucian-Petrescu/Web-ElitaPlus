<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CaseDetailsForm.aspx.vb" 
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CaseDetailsForm" 
    MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default"%>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script type="text/javascript">        
    $(function () {
            var disabledTabs = $("input[id$='hdnDisabledTabs']").val().split(',');
            var disabledTabsIndexArr = [];
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
    </script>
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">

   <table border="0" class="summaryGrid" cellpadding="0" cellspacing="0">


                    <tr>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="LabelCompany" runat="server">COMPANY</asp:Label>:
                        </td>
                        <td align="left" nowrap="nowrap" class="bor padRight">
                            <asp:Label ID="LabelCompanyValue" runat="server"><%#NoData%></asp:Label>
                        </td>
                        <td colspan="2" align="left" nowrap="nowrap" class="padRight">&nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">
                             <asp:Label ID="LabelCaseNumber" runat="server">CASE_NUMBER</asp:Label>:
                        </td>
                        <td align="left" nowrap="nowrap" class="bor padRight">
                            <asp:Label ID="LabelCaseNumberValue" runat="server"><%#NoData%></asp:Label>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="LabelCaseOpenDate" runat="server">CASE_OPEN_DATE</asp:Label>:
                        </td>
                        <td align="left" nowrap="nowrap" class="padRight">
                            <asp:Label ID="LabelCaseOpenDateValue" runat="server"><%#NoData%></asp:Label>
                        </td>
                    </tr>
					<tr>
                       
                         <td align="right" nowrap="nowrap">
                             <asp:Label ID="LabelCasePurpose" runat="server">CASE_PURPOSE</asp:Label>:
                        </td>
                        <td align="left" nowrap="nowrap" class="bor padRight">
                            <asp:Label ID="LabelCasePurposeValue" runat="server"><%#NoData%></asp:Label>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="LabelCaseStatus" runat="server">CASE_STATUS</asp:Label>:
                        </td>
                        <td id="CaseStatusTD" runat="server" align="left" nowrap="nowrap" class="padRight">
                            <asp:Label ID="LabelCaseStatusValue" runat="server"><%#NoData%></asp:Label>
                        </td>
                    </tr>
					<tr>
                        <td align="right" nowrap="nowrap">
                             <asp:Label ID="LabelCaseCloseDate" runat="server">CASE_CLOSE_DATE</asp:Label>:
                        </td>
                        <td align="left" nowrap="nowrap" class="bor padRight">
                            <asp:Label ID="LabelCaseCloseDateValue" runat="server"><%#NoData%></asp:Label>
                        </td>
                        <td align="right" nowrap="nowrap">
                             <asp:Label ID="LabelCaseClose" runat="server">CASE_CLOSE_REASON</asp:Label>:
                        </td>
                        <td align="left" nowrap="nowrap" class="padRight">
                            <asp:Label ID="LabelCaseCloseValue" runat="server"><%#NoData%></asp:Label>
                        </td>

                    </tr>


                     <tr>
                         <td align="right" nowrap="nowrap">
                             <asp:Label ID="LabelCertificateNumber" runat="server">CERTIFICATE_NUMBER</asp:Label>:
                        </td>
                        <td align="left" nowrap="nowrap" class="bor padRight">
                             <asp:Label ID="LabelCertificateNumberValue" runat="server"><%#NoData%></asp:Label>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="LabelClaimNumber" runat="server">CLAIM_NUMBER</asp:Label>:
                        </td>
                        <td align="left" nowrap="nowrap" class="padRight">
                            <asp:Label ID="LabelClaimNumberValue" runat="server"><%#NoData%></asp:Label>
                        </td>
                         
                    </tr>

    </table>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
        <div class="dataContainer">
            <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
            <asp:HiddenField ID="hdnDisabledTabs" runat="server" Value="" />            
            <div id="tabs" class="style-tabs" style="display: none;">
                <ul>
                    <li><a href="#tabsInteractionInfo"><asp:Label ID="Label8" runat="server" CssClass="tabHeaderText">CASE_INTERACTION</asp:Label></a></li>
                    <li><a href="#tabsActionInfo"><asp:Label ID="Label17" runat="server" CssClass="tabHeaderText">CASE_ACTION</asp:Label></a></li>
                    <li><a href="#tabsQuestionAnswerInfo"><asp:Label ID="Label1" runat="server" CssClass="tabHeaderText">CASE_QUESTION_ANSWER</asp:Label></a></li>
                    <li><a href="#tabsDeniedReasonInfo"><asp:Label ID="LabelCaseDeniedReasons" runat="server" CssClass="tabHeaderText">DENIED_REASONS</asp:Label></a></li>
                    <li><a href="#tabsCaseNotes"><asp:Label ID="LabelCaseNotes" runat="server" CssClass="tabHeaderText">CASE_NOTES</asp:Label></a></li>
                </ul>
                
                <div id="tabsInteractionInfo">
                      <table class="dataGrid" border="0" width="100%">
                        <tr>
                            <td width="40%" align="right">
                                <asp:Label ID="lblInteractionRecordFound" class="bor" runat="server"></asp:Label>
                            </td>
                        </tr>
                     </table>
                     <div class="Page" runat="server" id="CaseInteractionTabPanel" style="display: block;
                            height: 300px; overflow: auto">
                            <asp:GridView ID="CaseInteractionGrid" runat="server" Width="100%"  AllowPaging="false" AllowSorting="false"
                                SkinID="DetailPageGridView">
                                <Columns>
                                    <asp:BoundField HeaderText="interaction_number"  DataField="interaction_number" SortExpression="interaction_number"  HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="interaction_purpose"  DataField="interaction_purpose" SortExpression="interaction_purpose"  HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="channel"  DataField="channel" SortExpression="channel"  HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="first_name"  DataField="first_name" SortExpression="first_name"  HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="last_name"  DataField="last_name" SortExpression="last_name"  HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="caller_phone"  DataField="caller_phone" SortExpression="caller_phone"  HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="caller_email"  DataField="caller_email" SortExpression="caller_email"  HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="is_caller_authenticated"  DataField="is_caller_authenticated" SortExpression="is_caller_authenticated"  HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="caller_authentication_method"  DataField="caller_authentication_method" SortExpression="caller_authentication_method"  HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="interaction_date"  DataField="interaction_date" SortExpression="interaction_date"  HtmlEncode="false">
                                    </asp:BoundField>
                                </Columns>
                                <PagerSettings PageButtonCount="30" Mode="Numeric" />
                            </asp:GridView>
                    </div>
                </div>
                                
                <div id="tabsActionInfo">
                       <table class="dataGrid" border="0" width="100%">
                           <tr>
                              <td width="40%" align="right">
                                     <asp:Label ID="lblActionRecordFound" class="bor" runat="server"></asp:Label>
                              </td>
                           </tr>
                      </table>

                     <div class="Page" runat="server" id="CaseActionTabPanel" style="display: block;
                            height: 300px; overflow: auto">
                            <asp:GridView ID="CaseActionGrid" runat="server" Width="100%"  AllowPaging="false" AllowSorting="false"
                                SkinID="DetailPageGridView">
                                <Columns>
                                    <asp:BoundField HeaderText="action_owner"  DataField="action_owner" SortExpression="action_owner"  HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="action_type"  DataField="action_type" SortExpression="action_type"  HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="status"  DataField="status" SortExpression="status"  HtmlEncode="false">
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="created_date"  DataField="created_date" SortExpression="created_date"  HtmlEncode="false">
                                    </asp:BoundField>
                                </Columns>
                                <PagerSettings PageButtonCount="30" Mode="Numeric" />
                            </asp:GridView>
                     </div>
                </div> 
                <div id="tabsQuestionAnswerInfo">
                     <table class="dataGrid" border="0" width="100%">
                        <tr>
                            <td width="40%" align="right">
                                <asp:Label ID="lblQuestionRecordFound" class="bor" runat="server"></asp:Label>
                            </td>
                        </tr>
                     </table>
                     <div class="Page" runat="server" id="CaseQuestionAnswerTabPanel" style="display: block;
                            height: 300px; overflow: auto">
                                <asp:GridView ID="CaseQuestionAnswerGrid" runat="server" Width="100%"  AllowPaging="false" AllowSorting="false"
                                    SkinID="DetailPageGridView">
                                    <Columns>
                                        <asp:BoundField HeaderText="interaction_number"  DataField="interaction_number" SortExpression="interaction_number"  HtmlEncode="false">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Question"  DataField="Question" SortExpression="Question"  HtmlEncode="false">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="answer"  DataField="answer" SortExpression="answer"  HtmlEncode="false">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="created_date"  DataField="created_date" SortExpression="created_date"  HtmlEncode="false">
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerSettings PageButtonCount="30" Mode="Numeric" />
                                </asp:GridView>
                      </div>
                    </div>  
         
              <div id="tabsDeniedReasonInfo">
                     <table class="dataGrid" border="0" width="100%">
                        <tr>
                            <td width="40%" align="right">
                                <asp:Label ID="lblDeniedReasonsRecordFound" class="bor" runat="server"></asp:Label>
                            </td>
                        </tr>
                     </table>
                     <div class="Page" runat="server" id="CaseDeniedReasonsTabPanel" style="display: block;
                            height: 300px; overflow: auto">
                                <asp:GridView ID="CaseDeniedReasonsGrid" runat="server" Width="100%"  AllowPaging="false" AllowSorting="false"
                                    SkinID="DetailPageGridView">
                                    <Columns>
                                        <asp:BoundField HeaderText="denied_reason"  DataField="denied_reason" SortExpression="denied_reason"  HtmlEncode="false">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="created_by"  DataField="created_by" SortExpression="created_by"  HtmlEncode="false">
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="created_date"  DataField="created_date" SortExpression="created_date"  HtmlEncode="false">
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerSettings PageButtonCount="30" Mode="Numeric" />
                                </asp:GridView>
                      </div>
                    </div>
                <div id="tabsCaseNotes">
                    <table class="dataGrid" border="0" width="100%">
                        <tr>
                            <td width="40%" align="right">
                                <asp:Label ID="LabelCaseNotesRecordFound" class="bor" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div class="Page" runat="server" id="CaseNotesTab" style="display: block; height: 300px; overflow: auto">
                        <asp:GridView ID="GridViewCaseNotes" runat="server" Width="100%" AllowPaging="false" AllowSorting="false"
                            SkinID="DetailPageGridView">
                            <Columns>
                                <asp:BoundField HeaderText="CASE_NOTE" DataField="notes" SortExpression="notes" HtmlEncode="false"></asp:BoundField>
                                <asp:BoundField HeaderText="created_date" DataField="created_date" SortExpression="created_date" HtmlEncode="false"></asp:BoundField>
                            </Columns>
                            <PagerSettings PageButtonCount="30" Mode="Numeric" />
                        </asp:GridView>
                    </div>
                </div>
            </div>              
        </div>
        <div class="btnZone">
            <div class="">
                <asp:Button ID="btnBack" runat="server" Text="Back" SkinID="AlternateLeftButton">
                </asp:Button>
                <asp:Button ID="btnOutboundCommHistory" runat="server" Text="Outbound_Comm_History" Visible="false" SkinID="AlternateLeftButton">
                </asp:Button>
            </div>
        </div>
</asp:Content>
