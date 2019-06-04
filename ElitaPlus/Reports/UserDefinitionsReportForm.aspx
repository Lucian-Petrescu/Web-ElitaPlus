
<%@ Page Language="vb" AutoEventWireup="false"  MasterPageFile="~/Reports/content_Report.Master" 
     Codebehind="UserDefinitionsReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.UserDefinitionsReportForm" %>     
 
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server" >      
            <table cellpadding="0" cellspacing="0" border="0" width="98%" align = "center"">
                <tr>
                    <td align="center" colspan="2" style="vertical-align: top;">
                        <table cellspacing="2" cellpadding="0" width="100%" border="0">
                            <tr>
                             <td nowrap align="right" colspan="1" style="width: 25%">
                             </td>
                                <td nowrap align="right" colspan="1" style="width: 25%">
                                    <asp:RadioButton ID="rrole" TextAlign="left" Text="PLEASE SELECT ALL ROLES" runat="server"
                                        Checked="False" AutoPostBack="false"></asp:RadioButton>
                                </td>
                                <td nowrap align="left" colspan="2">
                                    &nbsp;
                                    <asp:Label ID="moRoleLabel" runat="server">OR A SINGLE ROLE</asp:Label>&nbsp;&nbsp;
                                    <asp:DropDownList ID="cboRoles" runat="server" AutoPostBack="false" Width="250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" width="100%" colspan="4" style="height: 13px">
                                </td>
                            </tr>
                            <tr>
                                <td nowrap align="right" colspan="1" style="width: 25%">
                                </td>
                                <td nowrap align="right" colspan="1" style="width: 25%">
                                    <asp:RadioButton ID="rIntern" Text="INTERNAL_USER" runat="server" AutoPostBack="false"
                                        TextAlign="left" GroupName="InternalExternal"></asp:RadioButton>
                                </td>
                                <td nowrap align="left" style="width: 25%">
                                    &nbsp;
                                    <asp:RadioButton ID="rExtern" TextAlign="left" Text="EXTERNAL_USER" runat="server"
                                        AutoPostBack="false" GroupName="InternalExternal"></asp:RadioButton>
                                </td>
                                <td nowrap align="left" style="width: 25%">
                                    &nbsp;
                                    <asp:RadioButton ID="rAll" TextAlign="Left" Text="ALL" GroupName="InternalExternal" runat="server" AutoPostBack="false" ></asp:RadioButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" width="100%" colspan="4" style="height: 13px">
                                </td>
                            </tr>
                            <tr>
                                <td nowrap align="right" colspan="1" style="width: 25%">
                                </td>
                                <td nowrap align="right" colspan="1">
                                    <asp:RadioButton ID="rActive" Text="ACTIVE" runat="server" AutoPostBack="false"
                                        TextAlign="left" GroupName="ActiveInactive"></asp:RadioButton>
                                </td>
                                <td nowrap align="left">
                                    &nbsp;
                                    <asp:RadioButton ID="rInActive" TextAlign="left" Text="INACTIVE" runat="server"
                                        AutoPostBack="false" GroupName="ActiveInactive"></asp:RadioButton>
                                </td>
                                <td nowrap align="left">
                                    &nbsp;
                                    <asp:RadioButton ID="rActiveInactive" TextAlign="Left" Text="ALL" runat="server" AutoPostBack="false" GroupName="ActiveInactive"></asp:RadioButton>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" width="100%" colspan="4">
                                    <hr style="width: 100%; height: 1px">
                                </td>
                            </tr>
                        
                            <tr>
                                <td nowrap align="right" colspan="1" style="width: 25%" style="vertical-align: top;">
                                </td>
                                <td style="vertical-align: top;" nowrap align="left" >
                                    <table cellspacing="0" cellpadding="0" border="0">      
                                        <tr>
                                            <td style="vertical-align: top;">                                                
                                            </td>
                                        </tr>                      
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <asp:Label ID="moSortByLabel" runat="server">SELECT_REPORT_SORT_ORDER</asp:Label>:
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td align="left" style="vertical-align: top;">
                                    <table cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td style="vertical-align: top;">
                                                <asp:RadioButtonList ID="rdReportSortOrder" runat="server" RepeatDirection="VERTICAL">
                                                    <asp:ListItem Value="U" Selected="True">USER NAME</asp:ListItem>
                                                    <asp:ListItem Value="R">ROLE (CODE)</asp:ListItem>
                                                    <asp:ListItem Value="N">NETWORK ID</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td nowrap align="right" colspan="1" style="width: 25%">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>                  
            </table>

            <script>

                function toggleRoleSelection(isSingleRole) {
                    //debugger;
                    if (isSingleRole) {
                        document.getElementById('<%#rrole.ClientID%>').checked = false;
                    }
                    else {
                        document.getElementById('<%#cboRoles.ClientID%>').selectedIndex = -1;
                    }
                }

                function toggleExternalSelection(isInternal) {
                    //debugger;
                    if (isInternal) {
                        document.getElementById('<%#rExtern.ClientID%>').checked = true;
                        document.getElementById('<%#rIntern.ClientID%>').checked = false;
                    }
                    else {
                        document.getElementById('<%#rIntern.ClientID%>').checked = true;
                        document.getElementById('<%#rExtern.ClientID%>').checked = false;
                    }
                }
		
            </script>      
  
</asp:Content>      
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">    
        <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Text="View" Width="100px"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
</asp:Content>   

                           
           

   
