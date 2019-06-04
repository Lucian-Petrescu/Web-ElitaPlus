<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/ElitaBase.Master"
    CodeBehind="AuditListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AuditListForm"
    Theme="Default" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js" />
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
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <%--<table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <%--<td class="bor">
                Browse Logs by
                <br />
                <asp:DropDownList ID="ddlType" runat="server" SkinID="SmallDropDown">
                    <asp:ListItem Text="Error_Unexpected" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Oracle" Value="2"></asp:ListItem>
                </asp:DropDownList>
                <br />
            </td>
            <td colspan="3">--%>
                <table width="100%" class="searchGrid" runat="server" id="searchGrid" border="0"
                    cellspacing="0" cellpadding="0">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblDateRange1" runat="server">AUDIT_DATE</asp:Label>&nbsp                                
                                <asp:TextBox ID="txtAuditBeginDate" runat="server" SkinID="exSmallTextBox" Width="150px"
                                    AutoPostBack="False"></asp:TextBox>
                                <asp:ImageButton ID="BtnAuditBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                    Width="20px" />
                                &nbsp<asp:Label ID="lblEventDateTo" runat="server">To</asp:Label>&nbsp
                                <asp:TextBox ID="txtAuditEndDate" runat="server" SkinID="exSmallTextBox" Width="150px"
                                    AutoPostBack="False"></asp:TextBox>
                                <asp:ImageButton ID="BtnAuditEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                    Width="20px" /></td>
                        <td>
                            <asp:Label ID="lblAuditSource" runat="server">AUDIT_SOURCE</asp:Label><br />
                                <asp:DropDownList ID="ddlAuditSource" runat="server" SkinID="SmallDropDown" Width="290px" AutoPostBack="False">
                                </asp:DropDownList>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblAuditSecurityTypeCode" runat="server">AUDIT_SECURITY_TYPE_CODE</asp:Label><br />
                            <asp:TextBox ID="txtAuditSecurityTypeCode" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblIPAddress" runat="server">IP_ADDRESS</asp:Label><br />
                            <asp:TextBox ID="txtIPAddress" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblUserName" runat="server">USER_NAME</asp:Label><br />
                            <asp:TextBox ID="txtUserName" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td align="right">
                               <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                                </asp:Button>
                                <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search" ></asp:Button>
                        </td>
                    </tr>
                </table>
          <%--  </td>
        </tr>
    </table>--%>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
    </asp:ScriptManager>
    <!-- new layout start -->
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label ID="lblSearchResultsText" runat="server"></asp:Label>
        </h2>
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
                SkinID="DetailPageGridView" AllowSorting="true" DataKeyNames="X509_CERTIFICATE">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <Columns>
                    <asp:BoundField DataField="AUDIT_DATE" SortExpression="AUDIT_DATE"
                        ReadOnly="true" HeaderText="AUDIT_DATE" HeaderStyle-HorizontalAlign="Center"
                        HtmlEncode="false" />
                    <asp:BoundField DataField="LOG_SOURCE" SortExpression="LOG_SOURCE" ReadOnly="true"
                        HeaderText="AUDIT_SOURCE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField SortExpression="AUDIT_SECURITY_TYPE_CODE" HeaderText="AUDIT_SECURITY_TYPE_CODE" DataField="AUDIT_SECURITY_TYPE_CODE"
                        HtmlEncode="false" />
                    <asp:BoundField DataField="CLIENT_IP_ADDRESS" SortExpression="CLIENT_IP_ADDRESS" ReadOnly="true" HeaderText="IP_ADDRESS"
                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="USER_NAME" SortExpression="USER_NAME" ReadOnly="true" HeaderText="USER_NAME"
                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="REQUEST_URL" SortExpression="REQUEST_URL" ReadOnly="true" HeaderText="REQUEST_URL"
                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField SortExpression="ACTION_NAME" DataField="ACTION_NAME" HeaderText="ACTION_NAME"
                        HtmlEncode="false"></asp:BoundField>
                    <asp:TemplateField ShowHeader="false" HeaderText="CERTIFICATE">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnCERTIFICATE" Style="cursor: hand" runat="server" ImageUrl="~/App_Themes/Default/Images/edit.png"
                                Visible="true" CommandName="ShowCertificateContent" ImageAlign="AbsMiddle" CommandArgument="<%#Container.DisplayIndex %>">
                            </asp:ImageButton></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>
        <asp:LinkButton runat="server" ID="dummybutton"></asp:LinkButton>
        <div id="AddNewContainer">
            <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup" TargetControlID="dummybutton"
                PopupControlID="pnlPopup" DropShadow="True" BackgroundCssClass="ModalBackground"
                BehaviorID="addNewModal" PopupDragHandleControlID="pnlPopup" RepositionMode="RepositionOnWindowScroll">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pnlPopup" runat="server" Style="display: none; width: 500px;">
                <div id="light" class="overlay_message_content">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" RenderMode="inline">
                        <ContentTemplate>
                            <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtCertificateContent" runat="server" ReadOnly="true" Width="650px"
                                            TextMode="MultiLine" Height="200px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Button ID="btnCERTIFICATEPopupCancel" runat="server" CssClass="popWindowCancelbtn floatR"
                                    Text="CANCEL" />
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </div>
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
</asp:Content>
