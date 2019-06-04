<%@ Control Language="vb" AutoEventWireup="false" Codebehind="TestUserControlAvailableSelected.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Generic.TestUserControlAvailableSelected" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<DIV align="center" ms_positioning="GridLayout" style="POSITION: relative">
	<TABLE id="moOutTable" borderColor="#ffffff" cellSpacing="1" cellPadding="1" align="center"
		border="0" bgColor="#fef9ea" runat="server" style="WIDTH: 98%; HEIGHT: 160px"> <!--#99eb69-->
		<TR>
			<TD align="center">
				<asp:Label id="moAvailableTitle" ForeColor="#12135b" runat="server"  Width="100%">Available</asp:Label></TD>
			<TD align="center" style="WIDTH: 30px"></TD>
			<TD align="center">
				<asp:Label id="moSelectedTitle" ForeColor="#12135b" runat="server"  Width="100%">Selected</asp:Label></TD>
		</TR>
		<TR>
			<TD align="center" vAlign="top" width="45%">
				<asp:ListBox id="moAvailableList" runat="server" Height="135px" Width="100%" SelectionMode="Multiple"
					Rows="6"></asp:ListBox></TD>
			<TD valign="middle" width="10%">
				<TABLE id="moButtonsTable" cellSpacing="2" cellPadding="2" width="100%" border="1" borderColor="#ffffff"
					runat="server">
					<TR>
						<TD align="center">
							<asp:Button id="BtnAdd_WRITE" style="CURSOR: hand" runat="server" Width="26px" Text=">" CssClass="FLATBUTTON"></asp:Button></TD>
					</TR>
					<TR>
						<TD align="center">
							<asp:Button id="BtnRemove_WRITE" style="CURSOR: hand" runat="server" Width="25px" Text="<" CssClass="FLATBUTTON"></asp:Button></TD>
					</TR>
					<TR>
						<TD align="center">
							<asp:Button id="BtnRemoveAll_WRITE" style="CURSOR: hand" runat="server" Width="27px" Text="<<"
								CssClass="FLATBUTTON"></asp:Button></TD>
					</TR>
					<TR>
						<TD align="center">
							<asp:Button id="BtnAddAll_WRITE" style="CURSOR: hand" runat="server" Width="27px" Text=">>"
								CssClass="FLATBUTTON"></asp:Button></TD>
					</TR>
				</TABLE>
			</TD>
			<TD align="center" vAlign="top" width="45%">
				<asp:ListBox id="moSelectedList" runat="server" Height="135px" Width="100%" SelectionMode="Multiple"
					Rows="6"></asp:ListBox></TD>
		</TR>
	</TABLE>
</DIV>
