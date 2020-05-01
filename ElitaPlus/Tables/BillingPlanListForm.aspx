<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="BillingPlanListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.BillingPlanListForm" %>

<%@ Register Src="../Common/MultipleColumnDDLabelControl.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>BillingPlanListForm</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
    <meta content="JavaScript" name="vs_defaultClientScript"/>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
    <link href="../Styles.css" type="text/css" rel="STYLESHEET"/>

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js">
        
    </script>
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
                                    ID="Label7" runat="server" Cssclass="TITLELABELTEXT">BILLING_PLAN</asp:Label></td>
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
                                                                 <tr align="left">                                                                 
                                                                    <td>
                                                                    </td>
                                                                    <td nowrap  colspan="2"   >                                                                      
                                                                        <uc2:MultipleColumnDDLabelControl width="95%" ID="moDealeGrouprMultipleDrop"   runat="server" />                                                                      
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                <td colspan="3"></td>
                                                                </tr>                                                            
                                                                <tr align="left" >
                                                                    <td>
                                                                    </td>                                                                    
                                                                    <td nowrap  colspan="2" >
                                                                        <div style="margin-left: 35px;">
                                                                       <uc2:MultipleColumnDDLabelControl   width="93%" ID="moDealerMultipleDrop" runat="server" />
                                                                        </div>
                                                                    </td>                                                                  
                                                                </tr>                           
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <hr/>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap align="left">
                                                                        &nbsp;</td>
                                                                    <td nowrap align="left">                                                                        
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:Label ID="Label1" runat="server">Billing_Plan_Code:</asp:Label>
                                                                     </td>
                                                                    <td nowrap align="left">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap align="left">
                                                                     </td>
                                                                    <td nowrap align="left">                                                                        
                                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                        <asp:textbox id="BillingPlanTextBox" CssClass="FLATTEXTBOX_TAB" runat="server" AutoPostBack="False" Width="180px"
																				Height="20px"></asp:textbox>
                                                                     </td>
                                                                    <td nowrap align="left">
                                                                    </td>
                                                                </tr>
                                                                    
                                                                
                                                                <tr>
                                                                    <td>
                                                                        &nbsp;</td>
                                                                    <td style="text-align: right;" colspan="2">
                                                                        <asp:Button ID="moBtnClearSearch" runat="server" 
                                                                            CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" Text="Clear" />
                                                                        &nbsp;
                                                                        <asp:Button ID="moBtnSearch" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH"
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
                                            <td style="height: 22px" align="right">
                                                <asp:Label ID="lblRecordCount" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="center" colspan="2">
                                            <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" 
                                            AllowPaging="True" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
                                            CssClass="DATAGRID">
                                            <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                            <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                            <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                                            <RowStyle CssClass="ROW"></RowStyle>
                                            <HeaderStyle CssClass="HEADER"></HeaderStyle>
                                            <Columns>   
                                                    <asp:TemplateField ShowHeader="false">
                                                        <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="EditRecord"
                                                                ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
						                                <asp:TemplateField HeaderStyle-Width="3%">
                                                            <ItemStyle Width="3%" CssClass="CenteredTD" />
							                                <ItemTemplate>
								                                <asp:ImageButton style="cursor:hand;" id="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/trash.gif" runat="server" CommandName="DeleteAction"></asp:ImageButton>
							                                </ItemTemplate>
						                                </asp:TemplateField>                                                                        
                                                        <asp:TemplateField SortExpression="Dealer_Group_Code" HeaderText="DEALER_GROUP_CODE">
                                                         <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                         <ItemStyle HorizontalAlign="Center"></ItemStyle> 
                                                             <ItemTemplate>
                                                                <asp:Label ID="DealerGroupCodeLabel" runat="server" Visible="True" Text='<%# Container.DataItem("dealer_group_code")%>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="DealerGroupDropdown"  Width="110PX" runat="server"></asp:DropDownList>
                                                            </EditItemTemplate>                                                 
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Dealer_Code" HeaderText="DEALER_CODE">
                                                            <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="DealerCodeLabel" runat="server" Visible="True" Text='<%# Container.DataItem("dealer_code")%>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="DealerDropdown"  Width="110PX" runat="server"></asp:DropDownList>
                                                            </EditItemTemplate>                                                          
                                                        </asp:TemplateField>             
                                                        <asp:TemplateField SortExpression="Billing_Plan_Code" HeaderText="BILLING_PLAN_CODE">
                                                          <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle> 
                                                            <ItemTemplate>
                                                                <asp:Label ID="BillingPlanCodeLabel" runat="server" Visible="True" Text='<%# Container.DataItem("billing_plan_code")%>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="BillingPlanCodeTextBox" runat="server" Visible="True"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Billing_Plan" HeaderText="BILLING_PLAN">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="BillingPlanLabel" runat="server" Visible="True" Text='<%# Container.DataItem("billing_plan")%>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="BillingPlanTextBox" runat="server" Visible="True"></asp:TextBox>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="BILLING_PLAN_ID" Visible="False" >
                                                           <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" ></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="BillingPlanIdLabel" runat="server" Visible="True" Text='<%# GetGuidStringFromByteArray(Container.DataItem("billing_plan_id"))%>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                                <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                                </asp:GridView>
                                                <asp:GridView ID="Grid1" runat="server" Width="100%" OnRowCreated="RowCreated" 
                                            AllowPaging="True" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
                                            CssClass="DATAGRID">
                                            <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                            <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                            <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                                            <RowStyle CssClass="ROW"></RowStyle>
                                            <HeaderStyle CssClass="HEADER"></HeaderStyle>
                                            <Columns>   
                                                    <asp:TemplateField ShowHeader="false" HeaderStyle-Width="1%">
                                                        <ItemStyle HorizontalAlign="Center" Width="1%" Height="15px" />
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="EditRecord"
                                                                ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
						                                <asp:TemplateField HeaderStyle-Width="1%">
                                                            <ItemStyle Width="1%" CssClass="CenteredTD" />
							                                <ItemTemplate>
								                                <asp:ImageButton style="cursor:hand;" id="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/trash.gif" runat="server" CommandName="DeleteAction"></asp:ImageButton>
							                                </ItemTemplate>
						                                </asp:TemplateField>                                                                        
                                                        <asp:TemplateField SortExpression="Dealer_Group_Code" HeaderText="DEALER_GROUP_CODE">
                                                         <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                         <ItemStyle HorizontalAlign="Center"></ItemStyle>                                                                                    
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Dealer_Code" HeaderText="DEALER_CODE">
                                                            <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>                                                                                                                                                                      
                                                        </asp:TemplateField>             
                                                        <asp:TemplateField SortExpression="Billing_Plan_Code" HeaderText="BILLING_PLAN_CODE">
                                                          <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>                                                             
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Billing_Plan" HeaderText="BILLING_PLAN">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>                                                            
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="BILLING_PLAN_ID" Visible="False" >
                                                           <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" ></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>                                                            
                                                        </asp:TemplateField>

                                                    </Columns>
                                                <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                                <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                                </asp:GridView></td>
                                        </tr>
                                        
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr valign="bottom">
                                <td align="left">
                                <hr style="width: 100%; height: 1px" size="1"/>
                                    <asp:Button ID="btnAdd_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                        Width="100px" Height="20px" CssClass="FLATBUTTON" Text="New" CommandName="WRITE">
                                    </asp:Button>&nbsp;
                                    <asp:Button ID="SaveButton_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="100px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;
                                    <asp:Button ID="CancelButton" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="100px" CssClass="FLATBUTTON" Height="20px" Text="Cancel"></asp:Button>&nbsp;
                                </td>
                            </tr>
                             <!--<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>-->
                                                            
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
