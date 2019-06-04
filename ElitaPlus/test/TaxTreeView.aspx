<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TaxTreeView.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.TaxTreeView"%>
<%@ Register TagPrefix="uc1" TagName="TreeController" Src="../Common/TreeController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TaxTreeView</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<DIV style="LEFT: 5px; WIDTH: 10px; POSITION: absolute; TOP: 5px; HEIGHT: 10px" ms_positioning="text2D">
			<FORM id="Form1" method="post" runat="server"> <!--Start Header-->
				<TABLE id="Table1" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; WIDTH: 620px; BORDER-BOTTOM: black 1px solid; HEIGHT: 46px"
					cellSpacing="0" cellPadding="0" width="620" bgColor="#d5d6e4" border="0">
					<TR>
						<TD vAlign="top">
							<TABLE id="Table2" style="WIDTH: 635px; HEIGHT: 25px" width="635" border="0">
								<TR>
									<TD height="20">&nbsp;
										<asp:label id="Label7" runat="server" Font-Bold="false">
											<font color="black">Admin: <b><font color="12135b" size="1px">Maintain Taxes</font></b></font></asp:label></TD>
								</TR>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
				<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
					height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
					border="0" frame="void">
					<TR>
						<TD>&nbsp;</TD>
					</TR>
					<TR>
						<TD vAlign="top">
							<TABLE id="moOutTable" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 636px; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 219px"
								cellSpacing="0" cellPadding="4" rules="cols" width="636" align="center" bgColor="#fef9ea"
								border="0"> <!--<TABLE id="moOutTable2" cellSpacing="1" cellPadding="1" width="95%" border="1">-->
								<TBODY>
									<TR>
										<TD vAlign="top" colSpan="2"><FONT color="#12135b"><I><B>*</B> Click on +/- to expand or 
													collapse nodes .</I></FONT></TD>
									</TR>
									<TR>
										<TD colSpan="2">
											<HR>
										</TD>
									</TR>
									<TR>
										<td vAlign="top" width="10"></td>
										<TD align="left"><uc1:treecontroller id="moTree" runat="server"></uc1:treecontroller></TD>
									</TR>
									<TR>
										<TD colSpan="2">
											<HR>
										</TD>
									</TR>
									<TR>
										<TD style="HEIGHT: 33px" align="left" colSpan="2">&nbsp;
											<asp:button id="BtnSave" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_trans_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
												runat="server" ToolTip="The UnChecked Roles will be in the DataBase tab exclusion table"
												Text="Save" Width="90px" CssClass="FLATBUTTON" height="21px"></asp:button>&nbsp;
											<asp:button id="BtnReset" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/reset_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
												runat="server" ToolTip="Load The Tabs and Roles from the DataBase" Text="Reset" Width="90px"
												CssClass="FLATBUTTON" height="21px"></asp:button></TD>
									</TR>
									<TR>
										<TD align="left" colSpan="2"></TD>
									</TR>
								</TBODY>
							</TABLE>
						</TD>
					</TR>
				</TABLE>
			</FORM>
		</DIV>
	</body>
</HTML>
