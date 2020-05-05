<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PremiumAdjustmentSettingsListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PremiumAdjustmentSettingsListForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register Src="../Common/MultipleColumnDDLabelControl.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>PremiumAdjustmentSettingsListForm</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
    <meta content="JavaScript" name="vs_defaultClientScript"/>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
    <link href="../Styles.css" type="text/css" rel="STYLESHEET"/>

    <script type="text/javascript" language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"> </script>
    <script type="text/javascript">
    function TABLE1_onclick() {

        }

    </script>

</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" border="0" ms_positioning="GridLayout">
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
                                <asp:Label ID="LabelTables" runat="server" Cssclass="TITLELABEL">Tables</asp:Label>:&nbsp;<asp:Label
                                    ID="Label7" runat="server" Cssclass="TITLELABELTEXT">PREMIUM_ADJUSTMENT_SETTINGS</asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
            margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid; height: 93%"
            cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
            <!--d5d6e4-->
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td valign="top" align="center" height="100%">
                    <asp:Panel ID="WorkingPanel" runat="server">
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                            border-left: #999999 1px solid; border-bottom: #999999 1px solid" height="98%"
                            cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea"
                            border="0">
                            <tr>
                                <td height="1">
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 1px" align="center">
                                    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table style="height: 100%" cellspacing="0" cellpadding="0" width="100%" align="center"
                                        border="0">
                                        <tr>
                                            <td style="height: 84px" valign="top" colspan="2">
                                                <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                    border-left: #999999 1px solid; border-bottom: #999999 1px solid; height: 84px"
                                                    cellspacing="0" cellpadding="4" rules="cols" width="100%" align="center" bgcolor="#f1f1f1"
                                                    border="0">
                                                    <tr>
                                                        <td>
                                                            <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">                                                            
                                                                
                                                                <tr>
                                                                <td colspan="3"></td>
                                                                </tr>                                                           
                                                                <tr align="left" >
                                                                    <td>
                                                                    </td>                                                                    
                                                                    <td nowrap  colspan="2" >
                                                                        <div style="margin-left: 15px;">
                                                                       <uc2:MultipleColumnDDLabelControl   width="93%" ID="moDealerMultipleDrop" runat="server" />
                                                                        </div>
                                                                    </td>                                                                  
                                                                </tr>                           
                                                                <!--<tr>
                                                                    <td colspan="3">
                                                                        <hr/>
                                                                    </td>
                                                                </tr>-->
                                                               
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td style="text-align: right;" colspan="2">
                                                                        <asp:Button ID="btnClearSearch" runat="server" 
                                                                            CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" Text="Clear" />
                                                                        &nbsp;
                                                                        <asp:Button ID="btnSearch" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH"
                                                                            Text="Search"  ></asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 1px" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 14px" colspan="2">
                                                &nbsp;
                                                <hr style="height: 1px"/>
                                            </td>
                                        </tr>
                                        <TR id="trPageSize" runat="server">
												<TD style="HEIGHT: 22px" vAlign="top" align="left">
													<asp:label id="lblPageSize" runat="server">Page_Size</asp:label>: &nbsp;
													<asp:dropdownlist id="cboPageSize" runat="server" AutoPostBack="true" Width="50px">
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
													</asp:dropdownlist></TD>
												<TD style="HEIGHT:22px" align="right">
													<asp:label id="lblRecordCount" Runat="server"></asp:label></TD>
											</TR>
											<TR>
												<TD colSpan="2" valign="top">
													<asp:GridView id="Grid" runat="server" Width="100%" AutoGenerateColumns="False" 
														CellPadding="1" AllowPaging="True" CssClass="DATAGRID"
														AllowSorting="True" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand">
														<SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                                        <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                                        <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                                                        <RowStyle CssClass="ROW"></RowStyle>
                                                        <HeaderStyle CssClass="HEADER"></HeaderStyle>

														<Columns>
														   <asp:TemplateField ShowHeader="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnView" runat="server" CausesValidation="False"
                                                                            CommandName="SelectAction" ImageUrl="~/Navigation/images/icons/edit2.gif" 
                                                                            CommandArgument="<%#Container.DisplayIndex %>" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>     															
															<asp:TemplateField SortExpression="Dealer_Code" HeaderText="Dealer_Code">
																<HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:TemplateField>
															<asp:TemplateField SortExpression="Adjustment_By" HeaderText="Financial_Adjustment_By">
																<HeaderStyle HorizontalAlign="Center" Width="55%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left"></ItemStyle>
															</asp:TemplateField>
															<asp:TemplateField SortExpression="Effective_Date" HeaderText="Effective_Date">
																<HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:TemplateField>
															<asp:TemplateField Visible="False" HeaderText="Premium_Adjustment_Setting_Id"></asp:TemplateField>
														</Columns>
														<PagerSettings PageButtonCount="15" Mode="Numeric" />														
                                                        <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
													</asp:GridView></TD>
											</TR>
											<TR>
												<TD></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD>
										<HR style="HEIGHT: 1px">
									</TD>
								</TR>
								<TR>
									<TD align="left">
										<asp:button id="btnAdd_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" CssClass="FLATBUTTON" Width="100px" Text="New" height="20px"
											CommandName="WRITE"></asp:button>&nbsp;
									</TD>
								</TR>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>