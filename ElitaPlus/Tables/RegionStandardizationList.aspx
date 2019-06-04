<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RegionStandardizationList.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.RegionStandardizationList" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE id="Table3" width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="LabelTables" runat="server" Cssclass="TITLELABEL">Tables</asp:label>:
									<asp:label id="moTitle2Label" runat="server"  Cssclass="TITLELABELTEXT">REGION_STANDARDIZATION</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<!--d5d6e4-->
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0">
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="100%"><asp:panel id="moRegionStandardization_Panel" runat="server">
      <TABLE id=tblMain1 
      style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid" 
      height="98%" cellSpacing=0 cellPadding=6 rules=cols width="98%" 
      align=center bgColor=#fef9ea border=0>
        <TR>
          <TD height=1></TD></TR>
        <TR>
          <TD align=center>
<uc1:ErrorController id=moErrorController runat="server"></uc1:ErrorController></TD></TR>
        <TR>
          <TD vAlign=top colSpan=2>
            <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
              <TR>
                <TD style="WIDTH: 100%; HEIGHT: 52px" colSpan=2>
                  <TABLE id=tblRegionStandardization_Search 
                  style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 98%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 76px" 
                  cellSpacing=2 cellPadding=2 rules=cols width="98%" 
                  align=center bgColor=#f1f1f1 border=0>
                    <TR>
                      <TD noWrap align=right width="1%">
<asp:label id=moCountryLabel runat="server">Country:</asp:label>
</TD>
                      <TD width="40%">
<asp:DropDownList id=moCountryDrop runat="server" Width="95%"></asp:DropDownList></TD>
                      <TD noWrap width="1%"></TD>
                      <TD width="40%"></TD></TR>
                    <TR>
                      <TD noWrap align=right width="1%">
<asp:label id=moLblRegionStandard runat="server" Font-Bold="false">Region Alias</asp:label>:</TD>
                      <TD width="40%">
<asp:textbox id=moTxtRegionStandardSearch runat="server" CssClass="FLATTEXTBOX" width="95%"></asp:textbox></TD>
                      <TD noWrap align=right width="1%">
<asp:label id=moLblRegion runat="server" Font-Bold="false">Region</asp:label>:</TD>
                      <TD width="40%">
<asp:DropDownList id=moDropdownRegion tabIndex=1 runat="server" Width="98%" Height="20px"></asp:DropDownList></TD></TR>
                    <TR>
                      <TD align=right colSpan=4>
<asp:button id=moBtnClearSearch style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" tabIndex=2 runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Text="Clear" height="20px"></asp:button>&nbsp;&nbsp; 
<asp:button id=moBtnSearch style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" tabIndex=3 runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Text="Search" height="20px"></asp:button></TD></TR></TABLE></TD></TR>
              <TR>
                <TD colSpan=2>
                  <HR style="HEIGHT: 1px">
                </TD></TR>
              <TR id=trPageSize runat="server">
                <TD style="HEIGHT: 22px" vAlign=top align=left>
<asp:label id=lblPageSize runat="server">Page_Size</asp:label>: 
                  &nbsp; 
<asp:dropdownlist id=cboPageSize runat="server" Width="50px" AutoPostBack="true">
														<asp:ListItem Value="5">5</asp:ListItem>
														<asp:ListItem Selected="True" Value="10">10</asp:ListItem>
														<asp:ListItem Value="15">15</asp:ListItem>
														<asp:ListItem Value="20">20</asp:ListItem>
														<asp:ListItem Value="25">25</asp:ListItem>
														<asp:ListItem Value="30">30</asp:ListItem>
														<asp:ListItem Value="35">35</asp:ListItem>
														<asp:ListItem Value="40">40</asp:ListItem>
														<asp:ListItem Value="45">45</asp:ListItem>
														<asp:ListItem Value="50">50</asp:ListItem>
													</asp:dropdownlist></TD>
                <TD style="HEIGHT: 22px" align=right>
<asp:label id=lblRecordCount Runat="server"></asp:label></TD></TR>
              <TR>
                <TD style="WIDTH: 100%" colSpan=2>&nbsp; 
<asp:datagrid id=moRegionGrid runat="server" Width="100%" OnItemCommand="ItemCommand" OnItemCreated="ItemCreated" BorderStyle="Solid" BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1" AllowSorting="True" BorderWidth="1px" AutoGenerateColumns="False" AllowPaging="True">
														<SelectedItemStyle Wrap="False" BackColor="AliceBlue"></SelectedItemStyle>
														<EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
														<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
														<ItemStyle Wrap="False" Height="12px" BackColor="White"></ItemStyle>
														<HeaderStyle></HeaderStyle>
														<Columns>
															<asp:TemplateColumn>
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="3%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton style="cursor:hand;" id="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif" runat="server" CommandName="EditButton"></asp:ImageButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn>
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="3%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" Width="7%"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton style="cursor:hand;" id="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/trash.gif" runat="server" CommandName="DeleteButton"></asp:ImageButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn Visible="False" >
																<HeaderStyle Width="0%"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id="moRegionAliasId" text='<%# GetGuidStringFromByteArray(Container.DataItem("REGION_STANDARDIZATION_ID"))%>' runat="server">
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn Visible="False" HeaderText="Country_Id">
																<HeaderStyle Width="0%"></HeaderStyle>
																<ItemTemplate>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="COUNTRY" HeaderText="COUNTRY">
																<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left"></ItemStyle>
																<ItemTemplate>
																</ItemTemplate>
																<EditItemTemplate>
                                                                <!--DEF-1692-->
																	<asp:DropDownList id="cboCountryInGrid" runat="server" OnSelectedIndexChanged ='cboCountryInGrid_SelectedIndexChanged'></asp:DropDownList>
																<!--End of DEF-1692-->
                                                                </EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="Region_Alias" HeaderText="Region Alias">
																<HeaderStyle HorizontalAlign="Center" Width="45%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Width="40%"></ItemStyle>
																<ItemTemplate>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox cssclass="FLATTEXTBOX_TAB" id="moRegionAliasText" runat="server"></asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="Region" HeaderText="Region">
																<HeaderStyle HorizontalAlign="Center" Width="45%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Width="46%"></ItemStyle>
																<ItemTemplate>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:DropDownList id="moRegionDropdown" runat="server"></asp:DropDownList>
																</EditItemTemplate>
															</asp:TemplateColumn>
														</Columns>
														<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
															Mode="NumericPages"></PagerStyle>
													</asp:datagrid></TD></TR></TABLE></TD></TR>
        <TR>
          <TD>
            <HR style="HEIGHT: 1px">
          </TD></TR>
        <TR>
          <TD style="WIDTH: 100%" align=left>
<asp:button id=moBtnAdd_WRITE style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Text="New" height="20px"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:button id=moBtnSave_WRITE style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Text="Save" height="20px"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:button id=moBtnCancel style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Text="Cancel" height="20px"></asp:button></TD></TR></TABLE>
						</asp:panel></td>
				</tr>
			</TABLE></TD></TR></TABLE></form>
	</body>
</HTML>
