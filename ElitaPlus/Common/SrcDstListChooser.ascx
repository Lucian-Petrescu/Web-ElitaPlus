<%@ Control Language="vb" AutoEventWireup="false" Codebehind="SrcDstListChooser.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Generic.SrcDstListChooser" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<DIV align="center" ms_positioning="GridLayout" style="POSITION: relative">
	<TABLE id="moOutTable" borderColor="#ffffff" cellSpacing="1" cellPadding="1" width="443"
		align="center" border="0" bgColor="#fef9ea" runat =server > <!--#99eb69-->
		<TR>
			<TD align="center" colSpan="5">
				<asp:Label id="moTitle"  Width="100%" runat="server">List Chooser</asp:Label></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 46px; HEIGHT: 23px"></TD>
			<TD style="WIDTH: 202px; HEIGHT: 23px" align="center">
				<asp:Label id="moAvailableTitle" ForeColor="#12135b" runat="server" >Available</asp:Label></TD>
			<TD style="WIDTH: 110px"></TD>
			<TD style="WIDTH: 158px; HEIGHT: 23px" align="center">
				<asp:Label id="moSelectedTitle" ForeColor="#12135b" runat="server" >Selected</asp:Label></TD>
			<TD style="HEIGHT: 23px"></TD>
		</TR>
		<TR>
			<TD style="WIDTH: 46px; HEIGHT: 153px"></TD>
			<TD style="WIDTH: 202px; HEIGHT: 153px" align="center">
				<asp:ListBox id="moAvailableList" runat="server" Height="125px" Width="170px" SelectionMode="Multiple"
					Rows="6" AutoPostBack="True"></asp:ListBox></TD>
			<TD valign="middle" style="WIDTH: 110px; HEIGHT: 153px">
				<TABLE id="moButtonsTable" cellSpacing="2" cellPadding="2" width="60" border="1" borderColor="#ffffff"
					style="WIDTH: 60px; HEIGHT: 112px" runat="server">
					<TR>
						<TD align="center">
							<asp:Button id="BtnAdd" style="CURSOR: hand" runat="server" Width="100%" Text=">" CssClass="FLATBUTTON"></asp:Button></TD>
					</TR>
					<TR>
						<TD align="center">
							<asp:Button id="BtnRemove" style="CURSOR: hand" runat="server" Width="100%" Text="<" CssClass="FLATBUTTON"></asp:Button></TD>
					</TR>
					<TR>
						<TD align="center">
							<asp:Button id="BtnRemoveAll" style="CURSOR: hand" runat="server" Width="100%" Text="<<" CssClass="FLATBUTTON"></asp:Button></TD>
					</TR>
					<TR>
						<TD align="center">
							<asp:Button id="BtnAddAll" style="CURSOR: hand" runat="server" Width="100%" Text=">>" CssClass="FLATBUTTON"></asp:Button></TD>
					</TR>
				</TABLE>
			</TD>
			<TD style="WIDTH: 158px; HEIGHT: 153px" align="center">
				<asp:ListBox id="moSelectedList" runat="server" Height="125px" Width="170px" SelectionMode="Multiple"
					Rows="6" AutoPostBack="True"></asp:ListBox></TD>
			<TD style="HEIGHT: 153px">&nbsp;</TD>
		</TR>
	</TABLE>
</DIV>
