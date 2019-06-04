<%@ Control Language="vb" AutoEventWireup="false" Codebehind="ErrorController.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ErrorController" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<DIV style="POSITION: relative" align="center" ms_positioning="GridLayout">
	<TABLE id="Table1" cellSpacing="0" borderColorDark="black" cellPadding="0" width="562"
		border="0">
		<TR>
			<TD>
				<TABLE id="moOutTable" borderColor="#ffffff" cellSpacing="1" cellPadding="1" width="609"
					align="center" bgColor="#fef9ea" border="0"> <!--#99eb69-->
					<TR>
						<td vAlign="middle" width="30"><IMG src="../Common/Images/showhide.gif">&nbsp;</td>
						<TD vAlign="middle" align="left" width="20%"><asp:label id="moTitle"  runat="server" ForeColor="Red">Errors_found</asp:label></TD>
						<td noWrap width="100%">
							<hr>
						</td>
						<td vAlign="middle" align="right" width="10"><asp:imagebutton id="btnShowHide" Runat="server" imageurl="../Common/Images/showhide.gif"></asp:imagebutton></td>
					</TR>
					<TR>
						<TD colSpan="4"><asp:textbox id="txtErrorMsg" runat="server" Height="41px"  Font-Name="verdana,arial"
								TextMode="MultiLine" width="100%" cssclass="FLATTEXTBOX_TAB"></asp:textbox></TD>
					</TR>
				</TABLE>
			</TD>
		</TR>
		<tr id="footer" runat="server">
			<td></td>
		</tr>
	</TABLE>
</DIV>
<HR style="HEIGHT: 1px" width="100%" SIZE="1" id="ErrorHorLine">
