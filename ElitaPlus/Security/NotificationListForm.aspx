<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NotificationListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.NotificationListForm" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" type="text/javascript">
        window.latestClick = '';

       // CloseDropDown('OK');
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
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
    <%--<table class="formGrid" id="tblMain1" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td align="center" colspan="4">
                <uc1:ErrorController ID="moErrorController" runat="server"></uc1:ErrorController>
            </td>
        </tr>
    </table>--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
    </asp:ScriptManager>
    <table width="100%" class="searchGrid" id="searchTable" runat="server" cellspacing="0"
        cellpadding="0" border="0">
        <tbody>
            <tr>
                <td colspan="3">
                    <table width="100%" border="0">
                        <tr>
                            <td>
                               <asp:Label ID="Label1" runat="server">NOTIFICATION_NAME:</asp:Label><br />
                                <asp:TextBox ID="txtNotificationName" runat="server" SkinID="LargeTextBox" Width="290px"
                                    AutoPostBack="False"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="Label3" runat="server">NOTIFICATION_DETAIL:</asp:Label><br />
                                <asp:TextBox ID="txtNotificationDetail" runat="server" SkinID="LargeTextBox" Width="290px"
                                    AutoPostBack="False"></asp:TextBox></td>
                            <td>
                                <asp:Label ID="lblNOTIFICATION_TYPE" runat="server">NOTIFICATION_TYPE:</asp:Label><br />
                                <asp:DropDownList ID="ddlNotificationType" runat="server" SkinID="SmallDropDown" Width="290px"
                                    AutoPostBack="False">
                                </asp:DropDownList></td>
                            <td>
                                <asp:Label ID="lblAUDIANCE_TYPE" runat="server">AUDIANCE_TYPE:</asp:Label><br />
                                <asp:DropDownList ID="ddlAudianceType" runat="server" SkinID="SmallDropDown" Width="290px" AutoPostBack="False">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td width="25%">
                                <asp:Label ID="lblDateRange1" runat="server">NOTIFICATION_DATE</asp:Label>&nbsp
                                <asp:Label ID="lblDateRange2" runat="server">Range:</asp:Label><br />
                                <asp:TextBox ID="txtBeginDate" runat="server" SkinID="exSmallTextBox" Width="100px"
                                    AutoPostBack="False"></asp:TextBox>
                                <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                    Width="20px" />
                                &nbsp<asp:Label ID="lblEventDateTo" runat="server">To</asp:Label>&nbsp
                                <asp:TextBox ID="txtEndDate" runat="server" SkinID="exSmallTextBox" Width="100px"
                                    AutoPostBack="False"></asp:TextBox>
                                <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                    Width="20px" />
                            </td>
                            <td width="25%">
                                <asp:Label ID="lblOutageDateRange1" runat="server">OUTAGE_DATE</asp:Label>&nbsp
                                <asp:Label ID="lblOutageDateRange2" runat="server">Range:</asp:Label><br />
                                <asp:TextBox ID="txtBeginDateOutage" runat="server" SkinID="exSmallTextBox" Width="100px"
                                    AutoPostBack="False"></asp:TextBox>
                                <asp:ImageButton ID="btnBeginDateOutage" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                    Width="20px" />
                                &nbsp<asp:Label ID="lblEventDateToOutage" runat="server">To</asp:Label>&nbsp
                                <asp:TextBox ID="txtEndDateOutage" runat="server" SkinID="exSmallTextBox" Width="100px"
                                    AutoPostBack="False"></asp:TextBox>
                                <asp:ImageButton ID="btnEndDateOutage" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                    Width="20px" />
                            </td>
                            <td>
                                <asp:Label ID="lblIncludeDisabled" runat="server">Include_Disabled:</asp:Label><br /><asp:CheckBox runat="server" Id="cbIncludeDisabled"/>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblSortBy" runat="server">Sort By</asp:Label>:<br />
                                <asp:DropDownList ID="cboSortBy" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False"
                                    TabIndex="8" SkinID="SmallDropDown" Width="290px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblSortOrder" runat="server">SORT_ORDER</asp:Label>
                                :<br />
                                <asp:DropDownList ID="cboSortOrder" runat="server" AutoPostBack="False" CssClass="FLATTEXTBOX_TAB"
                                    SkinID="SmallDropDown" TabIndex="8" Width="290px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                
                            </td>
                            <td>
                                <br />
                                <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear" />
                                <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div class="dataContainer" id="gridHeader" runat="server">
            <h2 class="dataGridHeader">
                Search results for System Notification List
            </h2>
        </div>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true"
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
            <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                AllowSorting="true" SkinID="DetailPageGridView">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <HeaderStyle />
                <Columns>
                    <asp:TemplateField SortExpression="NOTIFICATION_NAME" HeaderText="NOTIFICATION_NAME">
                        <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="SelectAction" Style="cursor: hand" runat="server" Visible="true"
                                CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="NOTIFICATION_TYPE" SortExpression="NOTIFICATION_TYPE"
                        ReadOnly="true" HeaderText="NOTIFICATION_TYPE" HeaderStyle-HorizontalAlign="Center"
                        HtmlEncode="false" />
                    <asp:BoundField DataField="AUDIANCE_TYPE" SortExpression="AUDIANCE_TYPE" ReadOnly="true"
                        HeaderText="AUDIANCE_TYPE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="NOTIFICATION_BEGIN_DATE" SortExpression="NOTIFICATION_BEGIN_DATE"
                        ReadOnly="true" HeaderText="BEGIN_DATE" HeaderStyle-HorizontalAlign="Center"
                        HtmlEncode="false" />
                    <asp:BoundField SortExpression="NOTIFICATION_END_DATE" DataField="NOTIFICATION_END_DATE"
                        HeaderText="END_DATE" ReadOnly="true" HtmlEncode="false" />
                    <asp:BoundField DataField="OUTAGE_BEGIN_DATE" SortExpression="OUTAGE_BEGIN_DATE"
                        ReadOnly="true" HeaderText="OUTAGE_BEGIN_DATE" HeaderStyle-HorizontalAlign="Center"
                        HtmlEncode="false" />
                    <asp:BoundField SortExpression="OUTAGE_END_DATE" DataField="OUTAGE_END_DATE" HeaderText="OUTAGE_END_DATE"
                        ReadOnly="true" HtmlEncode="false" />
                    <asp:BoundField SortExpression="SERIAL_NO" DataField="SERIAL_NO" HeaderText="SERIAL_NO"
                        ReadOnly="true" HtmlEncode="false" />
                    <asp:BoundField SortExpression="ENABLED" DataField="ENABLED" HeaderText="ACTIVE" ReadOnly="true"
                        HtmlEncode="false" />
                    <asp:TemplateField Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblListID" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("NOTIFICATION_id"))%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>
        <div class="btnZone">
            <asp:LinkButton runat="server" ID="dummybutton" Style = "display: none" ></asp:LinkButton>
            <asp:Button ID="addBtnNewListItem" runat="server" SkinID="AlternateLeftButton" Text="NEW" />            
            <asp:Button ID="BtnAcknowledge" runat="server" SkinID="AlternateLeftButton" Text="Acknowledge" /><asp:Label ID="lblAcknowlage" runat="server" Visible="false">ACKNOWLEDGE_INSTRUCTIONS</asp:Label>
        </div>
    </div>
    <div class="btnZone">
    </div>
    <div id="AddNewContainer">
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup" TargetControlID="dummybutton"
            PopupControlID="pnlPopup" DropShadow="True" BackgroundCssClass="ModalBackground"
            BehaviorID="addNewModal" PopupDragHandleControlID="pnlPopup" RepositionMode="RepositionOnWindowScroll">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" Style="display: none; width: 500px;">
            <div id="light" class="overlay_message_content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" RenderMode="inline"
                    ChildrenAsTriggers="True">
                    <ContentTemplate>
                        <p class="modalTitle">
                            <asp:Label ID="lblModalTitle" runat="server">NOTIFICATION_DETAIL</asp:Label>
                        </p>
                        <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="right" >
                                    <asp:Label ID="lblNOTIFICATION_NAME_Modal" runat="server">NOTIFICATION_NAME:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNOTIFICATION_NAME_Modal" runat="server" SkinID="MediumTextBox" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td align="right" >
                                    <asp:Label ID="lblNOTIFICATION_TYPE_Modal" runat="server">NOTIFICATION_TYPE:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNotificationType_Modal" runat="server" SkinID="MediumTextBox" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" >
                                    <asp:Label ID="lblBegin_date_Modal" runat="server">NOTIFICATION_BEGIN_DATE:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBeginDate_Modal" runat="server" SkinID="MediumTextBox" AutoPostBack="False"  ReadOnly="true"></asp:TextBox>
                                </td>
                                <td align="right" >
                                    <asp:Label ID="lblEnd_date_Modal" runat="server">NOTIFICATION_END_DATE:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEndDate_Modal" runat="server" SkinID="MediumTextBox" AutoPostBack="False" ReadOnly="true"></asp:TextBox>                        
                                </td>
                            </tr>
                            <tr>
                                <td align="right" >
                                    <asp:Label ID="lblOutageBegin_date_Modal" runat="server">OUTAGE_BEGIN_DATE:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOutageBeginDate_Modal" runat="server" SkinID="MediumTextBox" AutoPostBack="False" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td align="right" >
                                    <asp:Label ID="lblOutageEnd_date_Modal" runat="server">OUTAGE_END_DATE:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOutageEndDate_Modal" runat="server" SkinID="MediumTextBox" AutoPostBack="False" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblSerialNo_Modal" runat="server" >SERIAL_NO:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSerialNo_Modal" runat="server" SkinID="MediumTextBox" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td align="right">
                                </td>
                                <td>                             
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <asp:Label ID="lblContactInfo_Modal" runat="server" >CONTACT_INFO:</asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtContactInfo_Modal" runat="server" SkinID="MediumTextBox" TextMode="MultiLine" Height ="50px" Width="100%" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" valign="top">
                                    <asp:Label ID="lblNOTIFICATION_DETAILS_Modal" runat="server" >NOTIFICATION_DETAILS:</asp:Label>
                                </td>
                                <td colspan="3">
                                    <asp:TextBox ID="txtNOTIFICATION_DETAILS_Modal" runat="server" SkinID="MediumTextBox" TextMode="MultiLine" Height ="160px" Width="100%" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>    
                        </table>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
                <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:Button ID="btnNewItemCancel" runat="server" CssClass="popWindowCancelbtn floatL"
                                Text="BACK" />
                        </td>
                        <td colspan="3">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" />
</asp:Content>
