<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Control Language="vb" AutoEventWireup="false" Codebehind="UserControlPoliceReport.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlPoliceReport" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TR>
	<td width=4%></td>
	<TD style="HEIGHT: 13px" noWrap align="left" width="100%" colSpan="3">
		<uc1:multiplecolumnddlabelcontrol id=mPoliceMultipleColumnDropControl runat="server"></uc1:multiplecolumnddlabelcontrol>
		<asp:label id=LabelDummy Visible=False runat="server" Font-Bold="false"></asp:label>
		</TD>
	<%--<TD style="HEIGHT: 13px" colSpan="1" noWrap></td>--%>
</TR>
<TR>
	<TD noWrap align="left" colSpan="4"></TD>
</TR>
<TR>
	<TD noWrap align="left" colSpan="4"></TD>
</TR>
<TR>
	<TD noWrap align="left" colSpan="4"></TD>
</TR>
<TR>
	<TD noWrap align="left" colSpan="4"></TD>
</TR>
<TR>
	<td width=4%></td>
	<TD noWrap align="right" width="13%">
		<asp:label id=LabelReportNumber runat="server" Font-Bold="false">REPORT_NUMBER</asp:label>:	</TD> 
	<TD align="left">&nbsp;<asp:textbox id=TextboxReportNumber tabIndex=1 runat="server" CssClass="FLATTEXTBOX" Width="150px"></asp:textbox></TD>
</TR>
<TR>
	<TD noWrap align="left" colSpan="4"></TD>
</TR>
<TR>
	<TD noWrap align="left" colSpan="4"></TD>
</TR>
<TR>
	<td width=4%></td>
	<TD noWrap align="right"><asp:label id=LabelOfficerName runat="server" Font-Bold="false">OFFICER_NAME</asp:label>:</TD> 
	<TD>&nbsp;<asp:textbox id=TextboxOfficerName tabIndex=1 runat="server" CssClass="FLATTEXTBOX" Width="320px"></asp:textbox></TD>
</TR>