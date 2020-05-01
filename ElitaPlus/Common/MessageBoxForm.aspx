<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MessageBoxForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.MessageBoxForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MessageBoxForm</title>
		<base target="_self">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body oncontextmenu="return true" style="BACKGROUND-REPEAT: repeat" leftMargin="0" background="Images/back_spacer.jpg"
		topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" align="right" border="0" style="BACKGROUND-REPEAT: repeat">
				<tr>
					<td colspan="2"><img src="../Navigation/images/trans_spacer.gif" height="8" width="1"></td>
				</tr>
				<TR>
					<td><img src="../Navigation/images/trans_spacer.gif" width="15" height="1"></td>
					<TD align="center" vAlign="middle" width="100%" height="50">
						<TABLE height="50" cellSpacing="0" cellPadding="0" width="100%" background="images/body_mid_back.jpg"
							border="0" style="BACKGROUND-REPEAT: repeat">
							<TR>
								<TD width="2" height="3"><IMG height="3" src="images/body_top_left.jpg" width="2"></TD>
								<TD width="100%" background="images/body_top_back.jpg" height="3"><IMG height="3" src="../Navigation/images/trans_spacer.gif" width="100%"></TD>
								<TD width="2" height="3"><IMG height="3" src="images/body_top_right.jpg" width="2"></TD>
							</TR>
							<TR>
								<TD width="2" style="BACKGROUND-REPEAT: repeat" height="100%"><IMG height="100%" src="Images/body_mid_left.jpg" width="2"></TD>
								<TD vAlign="middle" width="100%" background="images/body_mid_back.jpg">
									<TABLE cellSpacing="2" style="BACKGROUND-REPEAT: repeat" cellPadding="2" width="100%" background=""
										border="0">
										<TR>
											<TD vAlign="top"><!--<div id="scroller" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 368px">-->
												<TABLE id="tblMain2" height="100%" cellSpacing="2" cellPadding="2" width="100%" bgColor="#f4f3f8"
													border="0">
													<TR>
														<TD vAlign="middle" colSpan="4" height="100%">
															<!--<DIV id="scroller" style="OVERFLOW-Y: auto; WIDTH: 100%; HEIGHT: 110px">-->
															<TABLE cellSpacing="0" cellPadding="0" width="100%" background="" border="0">
																<TR>
																	<TD valign="middle" align="center">
																		<TABLE cellSpacing="4" cellPadding="4" width="100%" background="" border="0" height="105">
																			<TR>
                                                                                <td valign="middle" align="center"><img runat="server" name="imgMsgIcon" id="imgMsgIcon" width="37" height="35"/></td>
																				<TD runat="server" valign="middle" id="tdBody" align="left" width="100%" height="105">
																					<!--<asp:textbox id="txtErrorMessage" style="BORDER-RIGHT:0px; BORDER-TOP:0px; BORDER-LEFT:0px; BORDER-BOTTOM:0px"
																							Height="100%" width="95%" ReadOnly="True" Runat="server" TextMode="MultiLine" 
																							BackColor="whitesmoke" Font-Name="Verdana"></asp:textbox>-->
																				</TD>
																			</TR>
																		</TABLE>
																	</TD>
																</TR>
															</TABLE>
															<!--</DIV>-->
														</TD>
													</TR>
												</TABLE> <!--</div>--></TD>
										</TR>
										<TR>
											<TD height="2"></TD>
										</TR>
										<TR>
											<TD width="100%" height="2"><IMG height="2" src="Images/div_spacer2.jpg" width="100%"></TD>
										</TR>
										<TR>
											<TD height="2"></TD>
										</TR>
										<TR>
											<TD runat="server" id="tdButtons" align="center"></TD>
										</TR>
									</TABLE>
								</TD>
								<TD width="2" height="100%"><IMG height="100%" src="Images/body_mid_right.jpg" width="2"></TD>
							</TR>
							<TR>
								<TD width="2" height="3"><IMG height="3" src="Images/body_bot_left.jpg" width="2"></TD>
								<TD width="100%" background="Images/body_bot_back.jpg" height="3"><IMG height="3" src="../Navigation/images/trans_spacer.gif" width="100%"></TD>
								<TD width="2" height="3"><IMG height="3" src="Images/body_bot_right.jpg" width="2"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
		<script>
		
	//resizeForm(document.getElementById('scroller'));
	//debugger;
    var argArr = dialogArguments.split("~");
    document.title = argArr[1];
    //document.write("<p>"+argArr[0]+"</p>");
    //document.write("<br>");
    
    /*if (argArr[3] == "0")
    {	
    document.all("imgMsgIcon").src = "../Navigation/images/infoIcon.gif";
    }
    else if (argArr[3] == "1")
    {
    document.all("imgMsgIcon").src = "../Navigation/images/questionIcon.gif";
    }
    else if (argArr[3] == "2")
    {
    document.all("imgMsgIcon").src = "../Navigation/images/warningIcon.gif";
    }
    
   // document.all("txtErrorMessage").value = argArr[0];
    
    document.getElementById("tdBody").innerHTML = argArr[0];
    
    if (argArr[2] == "1")
       document.getElementById("tdButtons").innerHTML ="<input id='button1' type=button name=OK value=OK onClick='window.returnValue = 1;window.close();' style='Height:22px;Width=100px;Color=#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/yes_icon2.gif); CURSOR: hand' Class='FLATBUTTON'>"
    if (argArr[2] == "2")
       document.getElementById("tdButtons").innerHTML ="<input id='button1' type=button name=Cancel value=Cancel onClick='window.returnValue =0;window.close();' style='Height:22px;Width=100px;Color=#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand' Class='FLATBUTTON'>"
    if (argArr[2] == "3")
    {
       document.getElementById("tdButtons").innerHTML = "<input id='button1' type=button name=OK value=OK onClick='window.returnValue = 1;window.close();' style='Height:22px;Width=100px;Color=#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/yes_icon2.gif); CURSOR: hand' Class='FLATBUTTON'>"
       document.getElementById("tdButtons").innerHTML = "<input id='button2' type=button name=Cancel value=Cancel onClick='window.returnValue = 0;window.close();' style='Height:2px;Width=100px;Color=#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand' Class='FLATBUTTON'>"
    }
    if (argArr[2] == "4")
    {
	   document.getElementById("tdButtons").innerHTML = "<input id='button1' type=button name=YES value=Yes style='Height:22px;Width=100px;Color=#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/yes_icon2.gif); CURSOR: hand' Class='FLATBUTTON' onClick='window.returnValue = 2;window.close();'>&nbsp;&nbsp;<input id='button2' type=button name=NO value=No style='font-bold:true;Height:22px;Width=100px;Color=#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/no_icon2.gif); CURSOR: hand' Class='FLATBUTTON' onClick='window.returnValue = 3;window.close();'>"
    }
    if (argArr[2] == "5")
    {
	   document.getElementById("tdButtons").innerHTML = "<input id='button1' type=button name=YES value=Yes style='Height:22px;Width=100px;Color=#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/yes_icon2.gif); CURSOR: hand' Class='FLATBUTTON' onClick='window.returnValue = 2;window.close();'>&nbsp;&nbsp;<input id='button2' type=button name=NO value=No style='font-bold:true;Height:22px;Width=100px;Color=#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/no_icon2.gif); CURSOR: hand' Class='FLATBUTTON' onClick='window.returnValue = 3;window.close();'>&nbsp;&nbsp;<input id='button3' type=button name=Cancel value=Cancel onClick='window.returnValue = 0;window.close();' style='font-weight:bold;Height:22px;Width=100px;Color=#000000;BACKGROUND-REPEAT:no-repeat;BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand' Class='FLATBUTTON'>"
    }*/
    
    //document.all("button1").blur();
    
    function resizeForm(item)
	{
		//debugger;
		
		var browseWidth, browseHeight;
		
		if (document.layers)
		{
			browseWidth=window.outerWidth;
			browseHeight=window.outerHeight;
		}
		if (document.all)
		{
			browseWidth=document.body.clientWidth;
			browseHeight=document.body.clientHeight;
		}
		
		if (screen.width == "800" && screen.height == "600") 
		{
			newHeight = browseHeight - 100;
		}
		else
		{
			newHeight = browseHeight - 80;
		}
		
		if ((newHeight) < 100)
		{
			item.style.height = "100px";
		}
		else
		{
			item.style.height = String(newHeight) + "px";		
		}
			
		newHeight = newHeight - 5;
		if ((newHeight) < 100)
		{
			document.getElementById("txtErrorMessage").style.height = "100px";
		}
		else
		{
			document.getElementById("txtErrorMessage").style.height = String(newHeight) + "px";
		}
		
		return newHeight;
	}
		</script>
	</body>
</HTML>
