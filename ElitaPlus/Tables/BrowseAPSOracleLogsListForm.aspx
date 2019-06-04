<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/ElitaBase.Master"
    CodeBehind="BrowseAPSOracleLogsListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.BrowseAPSOracleLogsListForm"
    Theme="Default" %>


<%@ Register TagPrefix="Elita" TagName="FieldSearchCriteriaNumber" Src="~/Common/FieldSearchCriteriaControl.ascx" %>
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
                        <td>
                            <asp:Label ID="lblHeader" runat="server">HEADER</asp:Label><br />
                            <asp:TextBox ID="txtHeader" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblCode" runat="server">CODE</asp:Label><br />
                            <asp:TextBox ID="txtCode" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="lblMachineName" runat="server">MACHINE_NAME</asp:Label><br />
                            <asp:TextBox ID="txtMachineName" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblUserName" runat="server">User Name</asp:Label><br />
                            <asp:TextBox ID="txtUserName" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td colspan="2">
                            <Elita:FieldSearchCriteriaNumber runat="server" ID="moGenerationDate" DataType="DateTime"
                                Text="GENERATION_DATETIME" SearchType="Between"/>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="right">
                            <label>
                                <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                                </asp:Button>
                                <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search" ></asp:Button>
                            </label>
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
                SkinID="DetailPageGridView" AllowSorting="true" DataKeyNames="EXTENDED_CONTENT,EXTENDED_CONTENT2">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <Columns>
                    <asp:BoundField DataField="GENERATION_DATE_TIME" SortExpression="GENERATION_DATE_TIME"
                        ReadOnly="true" HeaderText="GENERATION_DATETIME" HeaderStyle-HorizontalAlign="Center"
                        HtmlEncode="false" />
                    <asp:BoundField DataField="MACHINE_NAME" SortExpression="MACHINE_NAME" ReadOnly="true"
                        HeaderText="MACHINE_NAME" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField SortExpression="USER_NAME" HeaderText="USER NAME" DataField="USER_NAME"
                        HtmlEncode="false" />
                    <asp:BoundField DataField="HEADER" SortExpression="HEADER" ReadOnly="true" HeaderText="HEADER"
                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="TYPE" SortExpression="TYPE" ReadOnly="true" HeaderText="TYPE"
                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField DataField="CODE" SortExpression="CODE" ReadOnly="true" HeaderText="CODE"
                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                    <asp:BoundField SortExpression="APPLICATION_NAME" DataField="APPLICATION_NAME" HeaderText="APPLICATION_NAME"
                        HtmlEncode="false"></asp:BoundField>
                    <asp:TemplateField ShowHeader="false" HeaderText="EXTENDED_CONTENT">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnExtendedContent" Style="cursor: hand" runat="server" ImageUrl="~/App_Themes/Default/Images/edit.png"
                                Visible="true" CommandName="ShowExtendedContent" ImageAlign="AbsMiddle" CommandArgument="<%#Container.DisplayIndex %>">
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
                                        <asp:TextBox ID="txtExtendedContent" runat="server" ReadOnly="true" Width="750px"
                                            TextMode="MultiLine" Height="200px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Button ID="btnExtendedContentPopupCancel" runat="server" CssClass="popWindowCancelbtn floatR"
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
