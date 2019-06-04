<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports/content_Report.Master"
    CodeBehind="ServicePerformanceReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ServicePerformanceReportForm" %>

<%@ Register TagPrefix="uc3" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: top;">
        <tr>
            <td nowrap align="right" colspan="1" style="width: 20%">
            </td>
            <td align="center" colspan="2" width="60%">
                <table border="0" cellpadding="0" cellspacing="0" align="center" style="width: 70%">
                    <tr align="center">
                        <td align="right" nowrap valign="baseline">
                            *<asp:Label ID="moBeginDateLabel" runat="server">BEGIN_DATE</asp:Label>:
                        </td>
                        <td align="left" nowrap>
                            &nbsp;
                            <asp:TextBox ID="moBeginDateText" runat="server" AutoPostBack="false" CssClass="FLATTEXTBOX"
                                TabIndex="1"></asp:TextBox>
                            <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" />
                        </td>
                        <td align="right" nowrap valign="baseline">
                            *
                            <asp:Label ID="moEndDateLabel" runat="server">END_DATE</asp:Label>:
                        </td>
                        <td align="left" nowrap>
                            &nbsp;
                            <asp:TextBox ID="moEndDateText" runat="server" AutoPostBack="false" CssClass="FLATTEXTBOX"
                                TabIndex="1"></asp:TextBox>
                            <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3" style="vertical-align: top;">
            </td>
        </tr>
        <tr id="trcomp" runat="server">
            <td nowrap align="right" colspan="1" style="width: 15%">
            </td>
            <td class="LABELCOLUMN" style="vertical-align: bottom;">
                *&nbsp;<asp:Label ID="lblCompany" runat="server">SELECT_COMPANY:</asp:Label>
            </td>
            <td>
                <uc3:MultipleColumnDDLabelControl ID="CompanyDropControl" runat="server"></uc3:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td nowrap align="right" colspan="1" style="width: 15%">
            </td>
            <td class="LABELCOLUMN" style="vertical-align: bottom;" height="10px" align="right">
                <asp:RadioButton ID="rdealer" AutoPostBack="false" Text="SELECT_ALL_DEALERS" TextAlign="left"
                    runat="server" Checked="True"></asp:RadioButton>
            </td>
            <td align="left">
                &nbsp;&nbsp;<uc3:MultipleColumnDDLabelControl ID="DealerDropControl" runat="server">
                </uc3:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td nowrap align="right" colspan="1" style="width: 15%">
            </td>
            <td class="LABELCOLUMN" style="vertical-align: bottom;" height="10px" align="right">
                <asp:RadioButton ID="rAllSVC" AutoPostBack="false" Text="ALL_SERVICE_CENTERS" TextAlign="left"
                    runat="server" Checked="True" onclick="toggleOptionSelection('rAllSVC');"></asp:RadioButton>
            </td>
            <td align="left">
                &nbsp;&nbsp;<uc3:MultipleColumnDDLabelControl ID="SVCDropControl" runat="server">
                </uc3:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
            </td>
        </tr>
        <tr>
            <td nowrap align="right" colspan="1" style="width: 15%">
            </td>
            <td align="right" colspan="2" width="85%">
                <table border="0" cellpadding="0" cellspacing="0" width="95%" align="right">
                    <tr align ="right">
                        <td align="right" nowrap>
                            &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rAllSvcActivity" AutoPostBack="false" Text="ALL_SERVICE_ACTIVITY"
                                TextAlign="left" runat="server" GroupName="SVCActivity" CHECKED ="true"></asp:RadioButton>
                        </td>
                        <td align="right" nowrap>
                            <asp:RadioButton ID="rInProcess" AutoPostBack="false" Text="IN_PROCESS" TextAlign="left"
                                runat="server" GroupName="SVCActivity"></asp:RadioButton>
                        </td>
                        <td align="right" nowrap valign="baseline">
                            <asp:RadioButton ID="rRepaired" AutoPostBack="false" Text="REPAIRED" TextAlign="left"
                                runat="server" GroupName="SVCActivity"></asp:RadioButton>
                        </td>
                        <td align="right" nowrap>
                            &nbsp;
                            <asp:RadioButton ID="rCompleted" AutoPostBack="false" Text="COMPLETED" TextAlign="left"
                                runat="server" GroupName="SVCActivity"></asp:RadioButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td nowrap align="right" colspan="1" style="width: 15%">
            </td>
            <td nowrap align="right" colspan="1">
                <asp:RadioButton ID="moAllExtStatus" onclick="toggleOptionSelection('moAllExtStatus');"
                    AutoPostBack="false" runat="server" Text="SELECT_ALL_EXTENDED_STATUS" TextAlign="left"
                    Checked="False"></asp:RadioButton>
            </td>
            <td nowrap align="left">
                &nbsp;&nbsp;
                <asp:Label ID="lblSingleExtStatus" runat="server">OR_A_SINGLE_EXTENDED_STATUS</asp:Label>:&nbsp;&nbsp;
                <asp:DropDownList ID="moSingleExtStatus" runat="server" AutoPostBack="false" onchange="toggleOptionSelection('moSingleExtStatus');"
                    Width="230px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td nowrap align="right" colspan="1" style="width: 15%">
            </td>
            <td nowrap align="right" colspan="1">
                <asp:RadioButton ID="RadiobuttonTotalsOnly" groupname = "Detail"
                    Text="SHOW TOTALS ONLY" AutoPostBack="false" runat="server" TextAlign="left">
                </asp:RadioButton>
            </td>
            <td nowrap align="left">
                &nbsp;&nbsp;
                <asp:RadioButton ID="RadiobuttonDetail" groupname = "Detail" Text="OR SHOW DETAIL WITH TOTALS"
                    AutoPostBack="false" runat="server" TextAlign="left"></asp:RadioButton>
            </td>
        </tr>
    </table>

    <script> 
	
	function toggleOptionSelection(ctl)
	{
		//debugger;
		
		if (ctl == 'moSingleExtStatus')
		{		 
			    document.getElementById('<%=moAllExtStatus.ClientID%>').checked = false;			  			    
			    document.getElementById('<%=rAllSvcActivity.ClientID%>').checked = false;
			    document.getElementById('<%=rInProcess.ClientID%>').checked = false;
			    document.getElementById('<%=rRepaired.ClientID%>').checked = false;
			    document.getElementById('<%=rCompleted.ClientID%>').checked = false;
			    document.getElementById('<%=rAllSVC.ClientID%>').checked = false;
			    document.getElementById('<%=SVCDropControl.CodeDropDown.ClientID%>').selectedIndex = -1;
			    document.getElementById('<%=SVCDropControl.DescDropDown.ClientID%>').selectedIndex = -1;
		 }
		 else if (ctl == 'moAllExtStatus')
		 {
			    document.getElementById('<%=moSingleExtStatus.ClientID%>').selectedIndex = -1;		    
			    document.getElementById('<%=rAllSvcActivity.ClientID%>').checked = false;
			    document.getElementById('<%=rInProcess.ClientID%>').checked = false;
			    document.getElementById('<%=rRepaired.ClientID%>').checked = false;
			    document.getElementById('<%=rCompleted.ClientID%>').checked = false;
			    document.getElementById('<%=rAllSVC.ClientID%>').checked = false;
			    document.getElementById('<%=SVCDropControl.CodeDropDown.ClientID%>').selectedIndex = -1;
			    document.getElementById('<%=SVCDropControl.DescDropDown.ClientID%>').selectedIndex = -1;
		 }
		 else
		 {
		        document.getElementById('<%=moAllExtStatus.ClientID%>').checked = false;
		        document.getElementById('<%=moSingleExtStatus.ClientID%>').selectedIndex = -1;		    			    
			    document.getElementById('<%=SVCDropControl.CodeDropDown.ClientID%>').selectedIndex = -1;
			    document.getElementById('<%=SVCDropControl.DescDropDown.ClientID%>').selectedIndex = -1;
			    if (document.getElementById('<%=rAllSvcActivity.ClientID%>').checked == false &&
			        document.getElementById('<%=rInProcess.ClientID%>').checked == false &&
			        document.getElementById('<%=rRepaired.ClientID%>').checked == false &&
			        document.getElementById('<%=rCompleted.ClientID%>').checked == false)
			    {
			       document.getElementById('<%=rAllSvcActivity.ClientID%>').checked = true; 
			    } 
		 }		 		
	}
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Text="View" Width="100px"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
</asp:Content>
