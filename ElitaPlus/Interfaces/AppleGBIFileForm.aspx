<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AppleGBIFileForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.AppleGBIFileForm" Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register Src="../Common/MultipleColumnDDLabelControl_new.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc1" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">

    <table width="100%" border="0" class="searchGrid" id="searchTable1" runat="server">
        <tr>
            <td valign="middle" width="30%" style="height: 40px">
                <table>
                    <tr>
                        <td nowrap align="left" width="1%">
                            <span class="mandate">* </span>
                            <asp:Label ID="lblbegindate" runat="server">BEGIN_DATE</asp:Label>
                            <br />
                            <asp:TextBox ID="txtBeginDate" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                            <asp:ImageButton ID="ImageBtnBeginDate" TabIndex="2" runat="server" Visible="True"
                                ImageUrl="~/App_Themes/Default/Images/calendar.png" ImageAlign="AbsMiddle"></asp:ImageButton>
                        </td>
                        <td nowrap align="left" width="1%">
                            <span class="mandate">* </span>
                            <asp:Label ID="lblEnddate" runat="server">END_DATE</asp:Label>
                            <br />
                            <asp:TextBox ID="txtEndDate" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                            <asp:ImageButton ID="ImageBbtnEndDate" TabIndex="2" runat="server" Visible="True"
                                ImageUrl="~/App_Themes/Default/Images/calendar.png" ImageAlign="AbsMiddle"></asp:ImageButton>
                        </td>
                        <td style="height: 40px" valign="middle" align="right" width="40%" nowrap="nowrap">
                            <br />
                            <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear"></asp:Button>
                            <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_GBI_FILE</asp:Label>
        </h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor" runat="server">:</asp:Label>
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
            <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="Grid_RowCreated" OnRowCommand="Grid_RowCommand"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView" PageSize="15">
                <SelectedRowStyle Wrap="true" />
                <EditRowStyle Wrap="true" />
                <AlternatingRowStyle Wrap="true" />
                <RowStyle Wrap="true" />
                <Columns>
                    <asp:TemplateField SortExpression="FILE_NAME" HeaderText="FILE_NAME">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="FILE_DATE" HeaderText="FILE_DATE">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CREATED_DATE" HeaderText="FILE_CREATED_DATE">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="RECEIVED" HeaderText="RECEIVED">
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="COUNTED" HeaderText="COUNTED">
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="NEW_CLAIMS" HeaderText="NEW_CLAIMS">
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CLAIM_UPDATE" HeaderText="CLAIM_UPDATE">
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="PROCESSED" HeaderText="PROCESSED">
                        <HeaderStyle></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="BtnShowProcessed" runat="server" Visible="true" CommandName="ShowProcessed" CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CANCELLED" HeaderText="CANCELLED">
                        <HeaderStyle></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="BtnShowCancelled" runat="server" Visible="true" CommandName="ShowCancelled" CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="PENDING_VALIDATION" HeaderText="PENDING_VALIDATION">
                        <HeaderStyle></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="BtnShowPendingValidation" runat="server" Visible="true" CommandName="ShowPendingValidation" CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="FAILED" HeaderText="FAILED_STATUS">
                        <HeaderStyle></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="BtnShowFail" runat="server" Visible="true" CommandName="ShowFail" CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="REPROCESS" HeaderText="REPROCESS_STATUS">
                        <HeaderStyle></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="BtnShowReprocess" runat="server" Visible="true" CommandName="ShowReprocess" CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="PENDING_CLAIM_CREATION" HeaderText="PENDING_CLAIM_CREATION">
                        <HeaderStyle></HeaderStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="BtnShowPendingCreation" runat="server" Visible="true" CommandName="ShowPendingCreation" CommandArgument="<%#Container.DisplayIndex %>" CausesValidation="false"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False" HeaderText="FILE_PROCESSED_ID"></asp:TemplateField>

                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle HorizontalAlign="Center" />
            </asp:GridView>
        </div>
        <div class="btnZone">
            &nbsp;
        </div>
        <div class="uploadcontainer">
            <asp:Panel ID="moUpLoadPanel" runat="server" Visible="false">
                <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                    width="100%">
                    <tr>
                        <td>
                            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                                width="100%">
                                <tr>
                                    <td nowrap align="right">*<asp:Label ID="moFilenameLabel" runat="server">Filename</asp:Label>:</td>
                                    <td nowrap align="left">
                                        <input id="dealerFileInput" type="file" name="dealerFileInput" runat="server"/>&nbsp;                           
                                        <asp:Button ID="btnCopyDealerFile_WRITE" SkinID="PrimaryLeftButton" runat="server" Text="COPY_DEALER_FILE"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap align="right">
                                        <asp:Label ID="moExpectedFileLabel" runat="server">Expected_File</asp:Label>:
                                    </td>
                                    <td nowrap align="left">&nbsp;
                                        <asp:Label ID="moExpectedFileLabel_NO_TRANSLATE" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </div>

</asp:Content>
