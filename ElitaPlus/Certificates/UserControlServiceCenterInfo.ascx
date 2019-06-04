<%@ Control Language="vb" AutoEventWireup="false" Codebehind="UserControlServiceCenterInfo.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlServiceCenterInfo" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" align="center" bgColor="#fef9ea"
	border="0">
	<TR>
		<TD vAlign="middle" noWrap align="right" width="1%"><asp:label id="LabelCode" runat="server" Font-Bold="false">SERVICE_CENTER_CODE</asp:label>:</TD>
		<TD style="WIDTH: 50%">&nbsp;
			<asp:textbox id="TextboxSCCode" tabIndex="9" runat="server" CssClass="FLATTEXTBOX" Width="75%"></asp:textbox></TD>
		<TD style="WIDTH: 1%" vAlign="middle" noWrap align="right"><asp:label id="LabelContactName" runat="server" Font-Bold="false">CONTACT_NAME</asp:label>:</TD>
		<TD style="WIDTH: 50%" colSpan="3">&nbsp;
			<asp:textbox id="TextboxContactName" tabIndex="19" runat="server" CssClass="FLATTEXTBOX" Width="95%"></asp:textbox></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 1%" vAlign="middle" noWrap align="right"><asp:label id="LabelDescription" runat="server" Font-Bold="false">SERVICE_CENTER_NAME</asp:label>:</TD>
		<TD style="WIDTH: 50%">&nbsp;
			<asp:textbox id="TextboxDescription" tabIndex="2" runat="server" CssClass="FLATTEXTBOX" Width="95%"></asp:textbox></TD>
		<TD style="WIDTH: 1%" vAlign="middle" noWrap align="right"><asp:label id="LabelPhone1" runat="server" Font-Bold="false">PHONE1</asp:label>:</TD>
		<TD style="WIDTH: 50%" vAlign="middle" colSpan="3">&nbsp;
			<asp:textbox id="TextboxPhone1" tabIndex="5" runat="server" CssClass="FLATTEXTBOX" Width="75%"></asp:textbox></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 1%" vAlign="middle" noWrap align="right"><asp:label id="LabelAddress" runat="server" Font-Bold="false">ADDRESS</asp:label>:</TD>
		<TD style="WIDTH: 50%">&nbsp;
			<asp:textbox id="TextboxAddress" tabIndex="2" runat="server" CssClass="FLATTEXTBOX" Width="95%"></asp:textbox></TD>
		<TD style="WIDTH: 1%" vAlign="middle" noWrap align="right"><asp:label id="LabelPhone2" runat="server" Font-Bold="false">PHONE2</asp:label>:</TD>
		<TD style="WIDTH: 50%" vAlign="middle" colSpan="3">&nbsp;
			<asp:textbox id="TextboxPhone2" tabIndex="7" runat="server" CssClass="FLATTEXTBOX" Width="75%"></asp:textbox></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 1%" vAlign="middle" noWrap align="right"><asp:label id="LabelAddress2" runat="server" Font-Bold="false">ADDRESS2</asp:label>:</TD>
		<TD style="WIDTH: 50%">&nbsp;
			<asp:textbox id="TextboxAddress2" tabIndex="2" runat="server" CssClass="FLATTEXTBOX" Width="95%"></asp:textbox></TD>
		<TD style="WIDTH: 1%" vAlign="middle" noWrap align="right"><asp:label id="LabelFax" runat="server" Font-Bold="false">FAX</asp:label>:</TD>
		<TD style="WIDTH: 50%" vAlign="middle" colSpan="3">&nbsp;
			<asp:textbox id="TextboxFax" tabIndex="11" runat="server" CssClass="FLATTEXTBOX" Width="75%"></asp:textbox></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 1%" vAlign="middle" noWrap align="right"><asp:label id="LabelCity" runat="server" Font-Bold="false">CITY</asp:label>:</TD>
		<TD style="WIDTH: 50%">&nbsp;
			<asp:textbox id="TextboxCity" tabIndex="9" runat="server" CssClass="FLATTEXTBOX" Width="75%"></asp:textbox></TD>
		<TD style="WIDTH: 1%" vAlign="middle" noWrap align="right"><asp:label id="LabelBusinessHours" runat="server" Font-Bold="false">BUSINESS_HOURS</asp:label>:</TD>
		<TD style="WIDTH: 50%" vAlign="middle" colSpan="3">&nbsp;
			<asp:textbox id="TextboxBusinessHours" tabIndex="20" runat="server" CssClass="FLATTEXTBOX" Width="95%"></asp:textbox></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 1%" vAlign="middle" noWrap align="right"><asp:label id="LabelStateProvince" runat="server" Font-Bold="false">STATE_PROVINCE</asp:label>:</TD>
		<TD style="WIDTH: 50%">&nbsp;
			<asp:textbox id="TextboxStateProvince" tabIndex="9" runat="server" CssClass="FLATTEXTBOX" Width="75%"></asp:textbox></TD>
		<TD style="WIDTH: 1%" vAlign="middle" align="right"><asp:label id="LabelProcessingFee" runat="server" Font-Bold="false">PROCESSING_FEE</asp:label>:</TD>
		<TD style="WIDTH: 50%" vAlign="middle" colSpan="3">&nbsp;
			<asp:textbox id="TextboxPROCESSING_FEE" tabIndex="15" runat="server" CssClass="FLATTEXTBOX" Width="95%"></asp:textbox></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 1%" vAlign="middle" align="right"><asp:label id="LabelZip" runat="server" Font-Bold="false">ZIP</asp:label>:</TD>
		<TD style="WIDTH: 50%" vAlign="middle">&nbsp;
			<asp:textbox id="TextboxZip" tabIndex="9" runat="server" CssClass="FLATTEXTBOX" Width="75%"></asp:textbox>
		<TD style="WIDTH: 1%" vAlign="middle" align="right"><asp:label id="LabelEmail" runat="server" Font-Bold="false">EMAIL</asp:label>:</TD>
		<TD style="WIDTH: 50%" vAlign="middle" colSpan="3">&nbsp;
			<asp:textbox id="TextboxEmail" tabIndex="14" runat="server" CssClass="FLATTEXTBOX" Width="95%"></asp:textbox></TD>
		</TD></TR>
	<TR>
		<TD style="WIDTH: 1%" vAlign="middle" align="right">
			<asp:label id="lblOriginalDealerLABEL" Font-Bold="false" runat="server">ORIGINAL_DEALER:</asp:label></TD>
		<TD style="WIDTH: 50%" vAlign="middle">&nbsp;
			<asp:textbox id="txtOriginalDealer" tabIndex="9" runat="server" Width="75%" CssClass="FLATTEXTBOX"></asp:textbox></TD>
		<TD style="WIDTH: 1%" vAlign="middle" align="right">
			<asp:label id="LabelCcEmail" Font-Bold="false" runat="server">CC_EMAIL</asp:label>:</TD>
		<TD style="WIDTH: 50%" vAlign="middle" colSpan="3">&nbsp;
			<asp:textbox id="TextboxCcEmail" tabIndex="14" runat="server" Width="95%" CssClass="FLATTEXTBOX"></asp:textbox></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 1%" vAlign="middle" align="right"></TD>
		<TD style="WIDTH: 50%" vAlign="middle"></TD>
		<TD style="WIDTH: 1%" vAlign="middle" noWrap align="right"><asp:label id="LabelDefaultToEmail" runat="server" Font-Bold="false">DEFAULT_TO_EMAIL</asp:label>:</TD>
		<TD style="PADDING-LEFT: 3px; WIDTH: 20%" vAlign="middle"><asp:checkbox id="CheckBoxDefaultToEmail" runat="server"></asp:checkbox></TD>
		<TD style="WIDTH: 10%" vAlign="middle" align="right">
			<asp:label id="LabelSHIPPING" Font-Bold="false" runat="server">SHIPPING</asp:label>:</TD>
		<TD style="WIDTH: 20%" vAlign="middle">
			<asp:checkbox id="CheckBoxShipping" tabIndex="16" runat="server"></asp:checkbox></TD>
	</TR>
	<TR>
		<TD style="WIDTH: 1%" vAlign="top" align="right">
			<HR style="WIDTH: 100%; HEIGHT: 1px" SIZE="1">
			&nbsp;
			<asp:label id="LabelComment1" runat="server" Font-Bold="false">COMMENT</asp:label>:</TD>
		<TD style="WIDTH: 50%" colSpan="5">
			<HR style="WIDTH: 100%; HEIGHT: 1px" SIZE="1">
			&nbsp;
			<asp:textbox id="TextboxComment" tabIndex="180" runat="server" Width="98%" TextMode="MultiLine"
				Rows="7"  ></asp:textbox></TD>
	</TR>
</TABLE>
