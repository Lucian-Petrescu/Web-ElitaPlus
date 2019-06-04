<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AppleGBIFileFormDetail.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.AppleGBIFileFormDetail"
    Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid">
        <tr>
            <td align="right">
                <asp:Label ID="moRecordTypeLabel" runat="server">STATUS</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="moStatusType" runat="server" SkinID="MediumTextBox" Visible="True" ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
            <td align="right" width="10%">
                <asp:Label ID="moFileNameLabel" runat="server" Visible="True">FILENAME:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="moFileNameText" runat="server" SkinID="MediumTextBox" Visible="True"
                    ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="moStartDateLabel" runat="server">START_DATE:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="moStartDateText" runat="server" SkinID="MediumTextBox" Visible="True" ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="moEndDateLabel" runat="server" Visible="True">END_DATE:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="moEndDateText" runat="server" SkinID="MediumTextBox" Visible="True" ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!-- new layout start -->
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>

    <script type="text/javascript">
        function Test(obj) {

        }

        function setDirty() {
            var inpId = document.getElementById('<%= HiddenIsPageDirty.ClientID %>')
            inpId.value = "YES"
        }

        function setBundlesDirty() {
            var inpId = document.getElementById('<%= HiddenIsBundlesPageDirty.ClientID %>')
            inpId.value = "YES"
        }

        function UpdateDropDownCtr(obj, oField) {
            document.getElementById(oField).value = obj.value
        }

        function UpdateCtr(oDropDown, oField) {
            document.getElementById(oField).value = oDropDown.value
            setDirty()
        }

    </script>
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">RECORDS_FOR_GBI_FILE</asp:Label>
        </h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="bor" align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                        <input id="HiddenSavePagePromptResponse" type="hidden" runat="server" />
                        <input id="HiddenIsPageDirty" type="hidden" runat="server" />
                        <input id="HiddenIfComingFromBundlesScreen" type="hidden" runat="server" />
                        <input id="HiddenIsBundlesPageDirty" type="hidden" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div>

            <div id="div-datagrid" style="overflow: auto; width: 100%; height: 500px;">
                <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="Grid_RowCreated" OnRowCommand="Grid_RowCommand"
                    AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView" PageSize="15">
                    <SelectedRowStyle Wrap="true" />
                    <EditRowStyle Wrap="true" />
                    <AlternatingRowStyle Wrap="true" />
                    <RowStyle Wrap="true" />
                    <Columns>
                        <asp:TemplateField Visible="False" HeaderText="BEN_GBICLAIM_QUEUE_ID">
                            <ItemTemplate>
                                <asp:Label ID="moReconWrkId" runat="server" Width="40px" Visible="True"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField SortExpression="REJECT_CODE" HeaderText="REJECT_CODE">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moRejectCode" ReadOnly="true" runat="server" Width="40px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="REJECT_REASON" HeaderText="REJECT_REASON">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moRejectReason" ReadOnly="true" runat="server" Width="140px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="CUSTOMER_ID" HeaderText="CUSTOMER_ID">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moCustomerId" ReadOnly="true" runat="server" Width="80px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="SHIP_TO_ID" HeaderText="SHIP_TO_ID">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moShipToId" ReadOnly="true" runat="server" Width="80px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="AGREEMENT_ID" HeaderText="AGREEMENT_ID">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moAgreementId" ReadOnly="true" runat="server" Width="100px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UNIQUE_IDENTIFIER">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moUniqueIdentifier" ReadOnly="true" runat="server" Width="100px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ORIGINAL_SERIAL_NUMBER">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="20%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moOriginialSerialNumber" ReadOnly="true" runat="server" Width="120px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ORIGINAL_IMEI_NUMBER">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moOriginalImeiNumber" ReadOnly="true" runat="server" Width="120px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NEW_SERIAL_NUMBER">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moNewSerialNumber" ReadOnly="true" runat="server" Width="120px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NEW_IMEI_NUMBER">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moNewImeiNumber" ReadOnly="true" runat="server" Width="120px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="REPAIR_COMPLETION_DATE">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moRepairCompletionDateText" runat="server" Visible="True"></asp:TextBox>
                                <asp:ImageButton ID="moRepairCompletionDateImage" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CLAIM_TYPE">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moClaimType" ReadOnly="true" runat="server" Width="20px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CHANNEL">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moChannel" ReadOnly="true" runat="server" Width="70px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="INCIDENT_FEE">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moIncidentFee" ReadOnly="true" runat="server" Width="60px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="NOTIF_CREATE_DATE">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moNotifCreateDateText" runat="server" Visible="True"></asp:TextBox>
                                <asp:ImageButton ID="moNotifCreateDateImage" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="REPAIR_COMPLETED">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moRepairCompleted" ReadOnly="true" runat="server" Width="20px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="REPAIR_COMPLETED_DATE">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moRepairCompletedDateText" runat="server" Visible="True"></asp:TextBox>
                                <asp:ImageButton ID="moRepairCompletedDateImage" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CLAIM_CANCELLED">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moClaimCancelled" ReadOnly="true" runat="server" Width="20px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DESCRIPTION">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moDescription" ReadOnly="true" runat="server" Width="100px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="DEVICE_TYPE">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moDeviceType" ReadOnly="true" runat="server" Width="100px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="CLAIM_NUMBER">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moClaimNumber" ReadOnly="true" runat="server" Width="100px" Visible="True"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings PageButtonCount="15" Mode="Numeric" Position="TopAndBottom" />
                    <PagerStyle HorizontalAlign="left" CssClass="PAGER_LEFT"></PagerStyle>
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="False"
             SkinID="AlternateLeftButton"></asp:Button>&nbsp;
        <asp:Button ID="SaveButton_WRITE" runat="server" Text="Save" CausesValidation="False"
             SkinID="AlternateLeftButton"></asp:Button>&nbsp;
         <asp:Button ID="btnReprocess" runat="server" Text="Reprocess" CausesValidation="False"
             SkinID="AlternateLeftButton"></asp:Button>&nbsp;
        <%--<asp:Button ID="btnUndo_WRITE" runat="server" Text="Undo" CausesValidation="False"
             SkinID="AlternateLeftButton"></asp:Button>&nbsp;--%>
        
    </div>
</asp:Content>
