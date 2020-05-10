<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ReportCeHistoryForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ReportCeHistoryForm" Theme="Default" %>

<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc2" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>ReportCeHistoryForm</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

</head>
<body leftmargin="0" topmargin="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
            border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
            cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                &nbsp;
                                <asp:Label ID="LabelReports" runat="server" cssclass="TITLELABEL">Reports</asp:Label>:&nbsp;
                                <asp:Label ID="Label7" runat="server"  cssclass="TITLELABELTEXT">REPORT_HISTORY</asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="moTablelOuter" style="border-right: black 1px solid; border-top: black 1px solid;
            margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
            cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
            <!--d5d6e4-->
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td valign="top" align="center" height="100%">
                    <asp:Panel ID="moPanel" runat="server" Height="100px" Width="98%">
                        <table id="moTablelMain" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                            border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="0"
                            cellpadding="6" width="100%" align="center" bgcolor="#fef9ea" border="0">
                            <tr>
                                <td align="center" height="1%">
                                    <uc2:ErrorController ID="ErrorCtrl" runat="server"></uc2:ErrorController>
                                    <uc1:ReportCeInputControl ID="moReportCeInputControl" runat="server"></uc1:ReportCeInputControl>
                                </td>
                            </tr>
                            <asp:Panel ID="moGridPanel" runat="server" Visible="True">
                                <tr>
                                    <td valign="top">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td colspan="2">
                                                    <table id="moTableSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                        border-left: #999999 1px solid; width: 100%; border-bottom: #999999 1px solid;
                                                        height: 82px" cellspacing="0" cellpadding="4" width="98%" align="center" bgcolor="#f1f1f1"
                                                        border="0">
                                                        <tr>
                                                            <td valign="middle">
                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                    <tr>
                                                                        <td nowrap align="center">
                                                                            &nbsp;&nbsp;
                                                                            <asp:Label ID="moReportLabel" runat="server">Report</asp:Label>:&nbsp;&nbsp;
                                                                            <asp:DropDownList ID="moReportDrop" runat="server" AutoPostBack="True">
                                                                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="mpReportKey" runat="server">REPORT_KEY</asp:Label>:&nbsp;&nbsp;
                                                                             <asp:textbox id="moReportSearchText" tabindex="1" runat="server" SkinID="LargeTextBox" Width="300px"></asp:textbox>
                                                                            <asp:Button ID="btnReportSearch" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH" height="20px" Text=" Report Search" OnClick="btnReportSearch_Click" Width="150px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    &nbsp;&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <hr style="height: 1px">
                                                </td>
                                            </tr>
                                            <tr id="trPageSize" runat="server">
                                                <td style="height: 22px" valign="top" align="left">
                                                    <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                                                    <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                        <asp:ListItem Value="25">25</asp:ListItem>
                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                        <asp:ListItem Value="35">35</asp:ListItem>
                                                        <asp:ListItem Value="40">40</asp:ListItem>
                                                        <asp:ListItem Value="45">45</asp:ListItem>
                                                        <asp:ListItem Value="50">50</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td style="height: 22px; text-align: right; vertical-align: middle">
                                                    <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                 <asp:GridView ID="moDataGrid" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
                                                    AllowPaging="True" AllowSorting="True" CssClass="DATAGRID">
                                                    <SelectedRowStyle Wrap="False" CssClass="SELECTED" />
                                                    <EditRowStyle Wrap="False" CssClass="EDITROW" />
                                                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
                                                    <RowStyle Wrap="False" CssClass="ROW" />
                                                    <HeaderStyle CssClass="HEADER" />
                                                        <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton Style="cursor: hand;" ID="btnEdit" CommandArgument='<%# GetGuidStringFromByteArray(Container.DataItem("ID"))%>'  CommandName="SelectUser"  ImageUrl="../Navigation/images/icons/edit2.gif"
                                                                        runat="server"></asp:ImageButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="moReportId" Text='<%# GetGuidStringFromByteArray(Container.DataItem("ID"))%>'
                                                                        runat="server">
                                                                    </asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            
                                                             <asp:TemplateField SortExpression="INSTANCE_KEY" HeaderText="KEY">
                                                                <ItemStyle HorizontalAlign="Left" ></ItemStyle>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                             </asp:TemplateField>
                                                             
                                                              <asp:TemplateField SortExpression="START_INSTANCE_TIMESTAMP" HeaderText="START_TIMESTAMP">
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                              </asp:TemplateField>
                                                              
                                                               <asp:TemplateField SortExpression="END_INSTANCE_TIMESTAMP" HeaderText="END_TIMESTAMP">
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                              </asp:TemplateField>
                                                              
                                                               <asp:TemplateField SortExpression="FORMAT" HeaderText="FORMAT">
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                               </asp:TemplateField>
                                                               
                                                                <asp:TemplateField SortExpression="FILENAME" HeaderText="FILENAME">
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                               </asp:TemplateField>
                                                               
                                                               <asp:TemplateField SortExpression="STATUS" HeaderText="STATUS">
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                               </asp:TemplateField>
                                                               
                                                               <asp:TemplateField SortExpression="DESTINATION" HeaderText="DESTINATION" Visible="false">
                                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                               </asp:TemplateField>
                                                                                                                                                                                                                                                                                                                                                   
                                                        </Columns>
                                                          <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>                                                    
                                                    </asp:GridView></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td align="left">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>

    <script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>

</body>
</html>
