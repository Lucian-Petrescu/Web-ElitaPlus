<%@ Page Language="vb" AutoEventWireup="false" Codebehind="NonBusinessCalendarForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.NonBusinessCalendarForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Choose a date</title>
    <asp:literal id="Literal1" runat="server"></asp:literal>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

</head>
<body leftmargin="0" topmargin="0" ms_positioning="GridLayout" border="0">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->        
		<TABLE class="TABLETITLE" cellSpacing="0" cellPadding="0" border="0">
			<TR>
				<TD vAlign="top">
					<TABLE width="100%" border="0">
						<TR>
							<TD height="20">
                                <asp:Label ID="LabelTables" runat="server" Cssclass="TITLELABEL">Tables</asp:Label>:
                                <asp:Label ID="Label40" runat="server" Cssclass="TITLELABELTEXT">NONBUSINESS_DAYS</asp:Label>                                    
						</TR>
					</TABLE>
				</TD>
			</TR>
		</TABLE>
			<TABLE id="tblOuter2" class="TABLEOUTER" height="93%" cellSpacing="0" cellPadding="0" rules="none"
				border="0">
            <!--d5d6e4-->
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr valign="top">
                <td valign="top" align="center">
                    <asp:Panel ID="WorkingPanel" runat="server" Width="98%" Height="98%">
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                            border-left: #999999 1px solid; width: 100%; border-bottom: #999999 1px solid;
                            height: 100%" cellspacing="0" cellpadding="6" rules="cols" width="100%" align="center"
                            bgcolor="#fef9ea" border="0">
                            <tr>
                                <td align="center">
                                    &nbsp;&nbsp;
                                    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td valign="top">
                                    <div id="scroller" style="overflow: auto; width: 98%;" align="center">
                                        <asp:Panel ID="EditPanel_WRITE" runat="server" Width="98%">
                                            <table id="Table1" style="width: 100%; height: 98%" cellspacing="1" cellpadding="0"
                                                border="0">
                                                <tr valign="top">
                                                    <td nowrap width="20%" colspan="4">
                                                        <table id="Table2" cellspacing="1" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td nowrap align="left" colspan="4">
                                                                    <div style="width: 82%" align="right">
                                                                        <table id="Table3" cellspacing="1" cellpadding="1" width="100%" bgcolor="#fef4e8"
                                                                            border="0">
                                                                            <tr>
                                                                                <td nowrap align="right">
                                                                                    <asp:Label ID="LabelMonth" runat="server" Width="53px" >Month</asp:Label><strong>:</strong></td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="cboMonthList" runat="server" Width="55px" 
                                                                                        AutoPostBack="True">
                                                                                    </asp:DropDownList></td>
                                                                                <td nowrap align="right">
                                                                                    <asp:Label ID="LabelYear" runat="server" Width="27px" >Year</asp:Label><strong>:</strong></td>
                                                                                <td>
                                                                                    <asp:DropDownList ID="cboYearList" runat="server" Width="57px"  AutoPostBack="True">
                                                                                    </asp:DropDownList></td>
                                                                            </tr>
                                                                        </table>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="top">
                                                                    <table id="Table4" cellspacing="1" cellpadding="1" width="100%" border="1">
                                                                        <tr>
                                                                            <td>
                                                                                <asp:Calendar ID="MyCalendar" runat="server" Width="100%" Height="400px" ShowGridLines="True"
                                                                                    BackColor="#f0f0fa" NextMonthText="<img runat='server' id='btnNextMonth' width='16' Height='14' src='./images/calendar_foward.gif' border='0'>"
                                                                                    PrevMonthText="<img runat='server' id='btnPrevMonth' width='16' Height='14' src='./images/calendar_back.gif' border='0'>">
                                                                                    <TodayDayStyle  ForeColor="#12135B"></TodayDayStyle>
                                                                                    <DayStyle ></DayStyle>
                                                                                    <NextPrevStyle  ForeColor="#FBE6A5"></NextPrevStyle>
                                                                                    <DayHeaderStyle   ForeColor="#12135B"></DayHeaderStyle>
                                                                                    <SelectedDayStyle BackColor="Orange"></SelectedDayStyle>
                                                                                    <TitleStyle Height="15px"   
                                                                                        BorderWidth="4px" ForeColor="#FBE6A5" BorderStyle="Groove" BackColor="#A1BD08"></TitleStyle>
                                                                                    <OtherMonthDayStyle ForeColor="#FBE6A5" BackColor="#F0F0FA"></OtherMonthDayStyle>
                                                                                </asp:Calendar>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
							<TR>
								<TD>
									<INPUT id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server">
								</TD>
								<TD>
									<INPUT id="isBtnBackClicked" type="hidden" value="N" name="isBtnBackClicked" runat="server">
								</TD>
							</TR>
                            
                            <tr>
                                <td align="right" colspan="2">
                                    <hr style="height: 1px">
                                    <table id="Table6" cellspacing="1" cellpadding="1" width="300" align="left" border="0">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                                    cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" CausesValidation="False"
                                                    Height="20px" CssClass="FLATBUTTON" Text="RETURN"></asp:Button></td>
                                            <td>
                                                <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif);
                                                    cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Height="20px"
                                                    CssClass="FLATBUTTON" Text="SAVE"></asp:Button></td>
                                            <td>
                                                <asp:Button ID="btnUndo_WRITE" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                                    cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" CausesValidation="False"
                                                    Height="20px" CssClass="FLATBUTTON" Text="UNDO"></asp:Button></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <input type="hidden" id="checkDates" value="" runat="server" />
        <input type="hidden" id="uncheckDates" value="" runat="server" />
    </form>

    <script>
		    function CheckboxAction(cbDate, cbClientId)
		    {
		        var objCbId = document.getElementById(cbClientId);
		        
		        if(objCbId != null && cbDate != null)
		        {
		            var objCheckDates = document.getElementById('checkDates');
		            var objUncheckDate = document.getElementById('uncheckDates');
		            
		            if (objCbId.checked)
		            {
		                objCheckDates.value =  cbDate + '|' + objCheckDates.value;
                        objUncheckDate.value = objUncheckDate.value.replace(cbDate, '');		                
		            }
		            else
		            {
                        objUncheckDate.value =  cbDate + '|' + objUncheckDate.value;		            
		                objCheckDates.value = objCheckDates.value.replace(cbDate, '');
                    }
                
		        }
		    }	 	    
    </script>

</body>
</html>
