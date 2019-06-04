<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CountryContactInfoForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CountryContactInfoForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WebForm2</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR"/>
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET" />
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	    <style type="text/css">
            .style1
            {
                height: 39px;
            }
        </style>
	</HEAD>
	<body oncontextmenu="return false" style="WIDTH: 555px; BACKGROUND-REPEAT: repeat; HEIGHT: 360px"
		bottomMargin="0" leftMargin="0" background="../Common/images/back_spacer.jpg" topMargin="0"
		scroll="no" onload="changeScrollbarColor();" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<input id="imgURL" type="hidden" name="imgURL" runat="server">
			<input type="hidden" id="hiddenContactInfoReqFields" name="hiddenContactInfoReqFields" value=""/>
			<asp:panel id="panelForm" runat="server" Height="519px" Width="654px">
<TABLE cellSpacing="0" cellPadding="0" width="100%" bgColor="#f4f3f8" border="0">
					<TR>
						<TD vAlign="top" width="100%" height="264">
							<TABLE style="BACKGROUND-REPEAT: repeat" height="100%" cellSpacing="0" cellPadding="0"
								width="100%" border="0">
								<TR>
									<TD width="2" height="3"><IMG height="3" src="../Navigation/images/body_top_left.jpg" width="2"></TD>
									<TD width="100%" background="../Navigation/images/body_top_back.jpg" height="3"><IMG height="3" src="../Navigation/images/trans_spacer.gif" width="100%"></TD>
									<TD width="2" height="3"><IMG height="3" src="../Navigation/images/body_top_right.jpg" width="2"></TD>
								</TR>
								<TR>
									<TD width="2"><IMG height="100%" src="../Navigation/images/body_mid_left.jpg" width="2"></TD>
									<TD vAlign="top" width="100%" background="../Navigation/images/body_mid_back.jpg" height="100%">
										<TABLE style="BACKGROUND-REPEAT: repeat" cellSpacing="2" cellPadding="2" width="100%" background=""
											border="0">
											<TR>
												<TD vAlign="top"><!--<div id="scroller" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 368px">-->
													<TABLE id="tblMain" style="BACKGROUND-REPEAT: repeat" cellSpacing="2" cellPadding="2" width="100%"
														bgColor="#f4f3f8" border="0">
														<TR>
															<TD vAlign="top" height="100%">
																<DIV id="scroller4" style="OVERFLOW-Y: auto; WIDTH: 100%; HEIGHT: 488px">
																	<TABLE style="BACKGROUND-REPEAT: repeat" height="100%" cellSpacing="2" cellPadding="2"
																		width="100%" background="" border="0">
																		<TR>
																			<TD>
																				<TABLE style="BACKGROUND-REPEAT: repeat" height="100%" cellSpacing="0" cellPadding="0"
																					width="100%" background="" border="0">
																					<TR>
																						<TD align="left">
																							<asp:Label id="Label2" runat="server" Width="169px" >Available Contact Info 
                                                                                            Fields</asp:Label></TD>
																						<TD style="WIDTH: 18px"></TD>
																						<TD style="WIDTH: 230px" align="left" nowrap="nowrap">
																							<asp:Label id="Label1" runat="server" >Selected Contact Info Fields To Be 
                                                                                            Required</asp:Label></TD>
																						<TD></TD>
																					</TR>
																					<TR>
																						<TD align="right">
																							<asp:ListBox id="AvailList" runat="server" Width="275px" Height="200px">
                                                                                 				<asp:ListItem Value="[SALU]">Salutation,[SALU]</asp:ListItem>
                                                                                                <asp:ListItem Value="[NAME]">Name, [NAME]</asp:ListItem>
                                                                                                <asp:ListItem Value="[HPHONE]">Home Phone, [HPHONE]</asp:ListItem>
                                                                                                <asp:ListItem Value="[WPHONE]">Work Phone,[WPHONE]</asp:ListItem>
                                                                                                <asp:ListItem Value="[CPHONE]">Cell Phone,[CPHONE]</asp:ListItem>
                                                                                                <asp:ListItem Value="[EMAIL]">Email,[EMAIL]</asp:ListItem>
                                                                                                <asp:ListItem Value="[ADR1]">address1,[ADR1]</asp:ListItem>
                                                                                                <asp:ListItem Value="[ADR2]">Address2, [ADR2]</asp:ListItem>
                                                                                                <asp:ListItem Value="[ADR3]">Address3, [ADR3]</asp:ListItem>
                                                                                                <asp:ListItem Value="[REGION]">Region ,[REGION]</asp:ListItem>
                                                                                 				<asp:ListItem Value="[CITY]" Selected="True">City,[CITY]</asp:ListItem>
																								<asp:ListItem Value="[ZIP]">PostalCode,[ZIP]</asp:ListItem>
																							</asp:ListBox></TD>
																						<TD style="WIDTH: 18px" align="center">
																							<asp:Button id="btnAdd" style="BACKGROUND-POSITION: center 50%; BACKGROUND-IMAGE: url(../Navigation/images/arrow_foward.gif); CURSOR: hand; COLOR: #000000; BACKGROUND-REPEAT: no-repeat"
																								runat="server" Width="25" Height="21" cssclass="FLATBUTTON" ToolTip="Add selected items"
																								Text=""></asp:Button><BR>
																							<BR>
																							<asp:Button id="btnRemove" style="BACKGROUND-POSITION: center 50%; BACKGROUND-IMAGE: url(../Navigation/images/arrow_back.gif); CURSOR: hand; COLOR: #000000; BACKGROUND-REPEAT: no-repeat"
																								runat="server" Width="25" Height="21" cssclass="FLATBUTTON" ToolTip="Remove selected items"
																								Text=""></asp:Button>
																						</TD>
																						<TD style="WIDTH: 230px" align="left">
																							<asp:ListBox id="ContactInfoReqFieldsList" runat="server" Width="275px" 
                                                                                                Height="200px" BorderWidth="0"
																								BorderStyle="Inset" BackColor="White"></asp:ListBox>
																						</TD>
																						<TD>
																							<asp:Button id="btnMoveUp" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/up.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																								runat="server" Width="25px" cssclass="FLATBUTTON" ToolTip="Move up"></asp:Button><BR>
																							<BR>
																							<asp:Button id="btnMoveDown" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/down.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																								runat="server" Width="25px" cssclass="FLATBUTTON" ToolTip="Move down"></asp:Button>
																						</TD>
																					</TR>
																					<tr>
																					    <td colspan="2"></td>
																					    <td>&nbsp;</td>
																					    <td></td>
																					</tr>
																					<TR>
																						<TD style="WIDTH: 271px" align="left" colSpan="2">
																							&nbsp;</TD>
																						<TD align="left" colSpan="2"></TD>
																					</TR>
																					<TR>
																						<TD align="left" colSpan="4">
																							&nbsp;</TD>
																					</TR>
																				</TABLE>
																			</TD>
																		</TR>
																		<TR>
																			<TD height="2"></TD>
																		</TR>
																		<TR>
																			<TD width="100%" height="2"><IMG height="2" src="../Common/images/div_spacer2.jpg" width="100%"></TD>
																		</TR>
																		<TR>
																			<TD height="2"></TD>
																		</TR>
																		<TR>
																			<TD vAlign="bottom" noWrap align="left" colSpan="4" height="25">
																				<asp:button id="btnSave_WRITE" style="BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																					runat="server" Font-Bold="false" Height="21" ToolTip="Save changes to database" Text="&nbsp;&nbsp;Save"
																					CssClass="FLATBUTTON" width="100" ForeColor="#000000"></asp:button>&nbsp; <INPUT class="FLATBUTTON" id="btnUndo_Write" title="Cancel" style="BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); WIDTH: 100px; CURSOR: hand; COLOR: #000000; BACKGROUND-REPEAT: no-repeat; HEIGHT: 21px"
																					onclick="parent.CloseCountryContactInfo();" type="button" value=" Cancel" Height="21" width="100">
																			</TD>
																		</TR>
																	</TABLE>
																</DIV>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE> <!--</div>--></TD>
								</TR>
							</TABLE>
						</TD>
						<TD width="2" height="100%"><IMG height="100%" src="../Navigation/images/body_mid_right.jpg" width="2"></TD>
					</TR>
					<TR>
						<TD width="2" height="3"><IMG height="3" src="../Navigation/images/body_bot_left.jpg" width="2"></TD>
						<TD style="BACKGROUND-REPEAT: repeat" width="100%" background="../Common/images/body_bot_back.jpg"
							height="3"><IMG height="3" src="../Navigation/images/trans_spacer.gif" width="100%"></TD>
						<TD width="2" height="3"><IMG height="3" src="../Navigation/images/body_bot_right.jpg" width="2"></TD>
					</TR>
				</TABLE></TD></TR></TABLE>
			</asp:panel>
			<asp:Literal ID="literalJS" runat="server"></asp:Literal>
		</form>
	</body>
	<script type="text/javascript" language="javascript">
	    function SetSelectedItemOptional(objList) {
	        //alert(document.getElementById("hiddenContactInfoReqFields").value);
	        var objchk = event.srcElement;
	        var val = objList.options[objList.selectedIndex]; //.text;
	        var txt = TrimString(val.text);
	        if (objchk.checked) {
	            val.text = txt + "*";
	        }
	        else {
	            if (txt.charAt(txt.length - 1) == "*")
	                val.text = txt.substring(0, (txt.length - 1))
	        }
	        document.getElementById("hiddenContactInfoReqFields").value = GetFormatString(objList);
	    }

	    function TrimString(str) {
	        return str.replace(/^\s*/, "").replace(/\s*$/, "");
	    }

	    function SaveFormat(objList) {
	        var txt = GetFormatString(objList);
	        parent.SaveCountryContactInfo(txt);
	    }

	    function GetFormatString(objList) {
	        var s = "";
	        for (i = 0; i < objList.length; i++) {
	            s = s + objList.options[i].text; //.replace("\\", "\\\\");
	        }
	        return s;
	    }

	    function SetOptionalFlag(objChk) {
	        var objList = event.srcElement;
	        //var val = TrimString(objList.options[objList.selectedIndex].text);
	        SyncOptionalFlag(objChk, objList);
	        //alert(objList.options[objList.selectedIndex].text);
	        //alert(objChk.checked);
	    }
	    function SyncOptionalFlag(objChk, objList) {
	        var txt = TrimString(objList.options[objList.selectedIndex].text);
	        if ((txt.indexOf("[CITY]") >= 0) || (txt.indexOf("[RGNAME]") >= 0) || (txt.indexOf("[RGCODE]") >= 0) || (txt.indexOf("[ZIP]") >= 0) || (txt.indexOf("[COU]") >= 0)) {
	            objChk.disabled = false;
	            if (txt.charAt(txt.length - 1) == "*") {
	                objChk.checked = true;
	            } else {
	                objChk.checked = false;
	            }
	        } else {//hide optional check box
	            objChk.checked = false;
	            objChk.disabled = true;
	            //objChk.style.display="none";
	        }
	    }
	</script>
</HTML>
