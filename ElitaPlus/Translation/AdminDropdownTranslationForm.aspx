<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AdminDropdownTranslationForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Translation.AdminDropdownTranslationForm"%>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AdminDropdownTranslationForm</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>

		<script language="Javascript">
		function LoadMessage()
		{
	//	debugger;
	     
		 var Message = document.all["txtMessageResponse"].value;
		 var Result = window.confirm(Message);
		 if (Result == true)
		   {	
		   document.all["txtMessageResponse"].value = "Ok"
			Form1.submit();	
		   }
		   else 
		   {
		   document.all["txtMessageResponse"].value ="Cancel" 
		   }
		}
		
		function checkKey()
		{
			if (window.event.shiftKey)  
			{
				txtOutput.value = "true"; 
			}
		}

		</script>
	</HEAD>
	<body style="BACKGROUND-REPEAT: no-repeat" bottomMargin="1" bgColor="white" leftMargin="0"
		background="" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<!--bgcolor="#f0f2f5"-->
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<table style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<tr>
					<td vAlign="top">
						<table width="100%" border="0">
							<tr>
								<td height="20"><asp:label id="AdminLabel" runat="server"  Cssclass="TITLELABEL">Admin</asp:label>:
									<asp:label id="MaintainLabel" runat="server" Cssclass="TITLELABELTEXT">Dropdown_Translation</asp:label></td>
							</tr>
						</table>
						</td></tr> 
				<tr>
					<td vAlign="top">		
						<uc1:errorcontroller id="ErrorControl" runat="server" Visible="False"></uc1:errorcontroller>
					</td>
				</tr>
			</table>
			<table id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0" frame="void"> <!--d5d6e4-->
				<tr>
					<td vAlign="top" align="center"><asp:panel id="pnlTranslation" runat="server" Width="98%" Height="98%" >
							<table cellSpacing="0" cellPadding="0" width="100%" border="0">
								<tr>
									<td>&nbsp;
									</td>
								</tr>
							</table>
							<table id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
							cellSpacing="0" cellPadding="4" rules="cols" height="98%" width="98%" align="center" bgColor="#fef9ea"
							border="0">
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td noWrap align="right">
													<asp:Label id="TitleName" runat="server">Dropdown Item Name</asp:Label>:</td>
												<td>&nbsp;
													<asp:Label id="lblDropdownItemNam" runat="server" width="92px">DropdownItemName</asp:Label></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td style="HEIGHT: 6px" align="center" width="100%">
										<hr size="1"/>
									</td>
								</tr>
								<tr> <!--FCE7A5-->
									<td valign="top" align="center">
										<asp:datagrid id="grdTranslation" runat="server" Width="90%" PageSize="15" OnItemCreated="ItemCreated"
											AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False" BorderWidth="1px" CellPadding="1"
											BorderStyle="Solid" BorderColor="#999999" BackColor="#DEE3E7">
											<SelectedItemStyle Wrap="False"></SelectedItemStyle>
											<EditItemStyle Wrap="False"></EditItemStyle>
											<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
											<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
											<HeaderStyle></HeaderStyle>
											<Columns>
												<asp:TemplateColumn HeaderText="TRANSLATION">
												<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="60%"></HeaderStyle>
													<ItemTemplate>
														<asp:TextBox id="txtNewTranslation" runat="server" Width="350px" Text='<%# Container.DataItem("Translation")%>'>
														</asp:TextBox>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="False" DataField="DICT_ITEM_TRANSLATION_ID"></asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="LANGUAGE_ID"></asp:BoundColumn>
												<asp:BoundColumn DataField="DESCRIPTION" HeaderText="LANGUAGE" HeaderStyle-HorizontalAlign="Center"></asp:BoundColumn>
												<asp:BoundColumn Visible="False" DataField="DICT_ITEM_ID"></asp:BoundColumn>
												<asp:TemplateColumn Visible="False">
													<ItemTemplate>
														<asp:Label id="TransItemIDGuid" runat="server" text='<%# GetTransItemId(Container.DataItem("DICT_ITEM_TRANSLATION_ID"))%>'>
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn Visible="False" DataField="Translation"></asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
												Mode="NumericPages"></PagerStyle>
										</asp:datagrid></td>
								</tr> 
								<tr>
									<td valign="bottom" width="100%" >
										<hr size="1"/>
									</td>
								</tr>
								<tr>
									<td style="vertical-align: center;">
										<asp:button id="btnSave" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_trans_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="90px" Text="Save" height="20px" CssClass="FLATBUTTON"></asp:button>&nbsp;
										<asp:button id="btnCancel" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="90px" Text="Cancel" height="20px" CssClass="FLATBUTTON"></asp:button>&nbsp;&nbsp;&nbsp;
									</td>
								</tr>
							</table>
					</asp:panel></td>
				</tr>
			</table>
			<input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server"/></form>
	</body>
</HTML>
