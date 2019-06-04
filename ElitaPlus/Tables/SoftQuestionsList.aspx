<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SoftQuestionsList.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.SoftQuestionsList"%>
<%@ Register TagPrefix="uc1" TagName="UserControlCertificateInfo" Src="../Certificates/UserControlCertificateInfo.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Soft Questions</title> <!-- ************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebForm.cst (11/16/2004)  ******************** -->
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<input id="isSoftQuestionFormVisible" type="hidden" name="isSoftQuestionFormVisible" runat="server">
			<input id="NextAction" type="hidden" name="NextAction" runat="server">
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD><asp:label id="LabelTables" runat="server" Cssclass="TITLELABEL">Tables</asp:label>:&nbsp;
									<asp:label id="Label40" runat="server" Cssclass="TITLELABELTEXT">Soft Questions</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid; HEIGHT: 93%"
				height="98%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--d5d6e4-->
				<tr>
					<td style="HEIGHT: 4px">&nbsp;</td>
				</tr>
				<tr>
					<td style="HEIGHT: 100%" vAlign="top" align="center"><asp:panel id="WorkingPanel" runat="server" Width="98%" Height="98%">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 100%"
								height="100%" cellSpacing="0" cellPadding="6" rules="cols" width="100%" align="center"
								bgColor="#fef9ea" border="0">
								<TR id="ErrorRow" runat="server">
									<TD style="HEIGHT: 22px" vAlign="middle" align="center" colSpan="4"><INPUT id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
											runat="server">
										<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR id="CertRow" runat="server">
									<TD style="HEIGHT: 19px" align="center" colSpan="4">
										<uc1:UserControlCertificateInfo id="moCertificateInfoController" runat="server"></uc1:UserControlCertificateInfo></TD>
								</TR>
								<TR>
									<TD style="WIDTH: 100%; HEIGHT: 94%" vAlign="top" colSpan="3">
										<asp:Panel id="softQuestionTreePanel" runat="server" Height="94%" Width="100%">
											<asp:TreeView ID="tvQuestion" runat="server" Target="_self" ShowExpandCollapse="true" ShowLines="true" NodeStyle-HorizontalPadding="5" NodeStyle-VerticalPadding="0">
											    <SelectedNodeStyle BackColor="LightGray" />
											    <RootNodeStyle Font-Bold="true" />
											</asp:TreeView>										
										</asp:Panel>
									</TD>
								</TR>
								<TR>
									<TD style="WIDTH: 100%; HEIGHT: 15%" vAlign="top" rowSpan="1">
										<asp:Panel id="PanelSoftQEdit" runat="server" Height="10px" Width="100%" Visible="False">
											<TABLE id="ListTable" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 98%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 30px"
												cellSpacing="1" cellPadding="1" width="100%" bgColor="#f1f1f1" border="0" runat="server">
												<TR>
													<TD style="WIDTH: 214px; HEIGHT: 1px" vAlign="bottom">
														<asp:label id="Label2" runat="server" Width="152px">Soft Questions Group</asp:label></TD>
													<TD style="HEIGHT: 1px" vAlign="bottom">
														<asp:label id="Label1" runat="server">SOFT_QUESTIONS_DESCRIPTION</asp:label></TD>
												</TR>
												<TR>
													<TD style="WIDTH: 214px; HEIGHT: 18px" vAlign="middle" align="center">
														<asp:DropDownList id="cboSoftQuestionGroup" runat="server" Width="204px"></asp:DropDownList></TD>
													<TD style="HEIGHT: 18px" vAlign="top" align="left">
														<asp:textbox id="txtSoftQuestion" runat="server" Height="20px" Columns="100" MaxLength="100"></asp:textbox></TD>
												</TR>
												<TR>
													<TD style="WIDTH: 214px; HEIGHT: 1px" vAlign="top" align="center"></TD>
													<TD style="HEIGHT: 1px" vAlign="top" align="right">
														<asp:button id="btnSave" style="BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
															runat="server" Font-Bold="false" ForeColor="#000000" Height="21" Text="Save" CssClass="FLATBUTTON"
															ToolTip="Save changes to database" width="100"></asp:button>
														<asp:button id="btnCancel" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
															runat="server" Font-Bold="false" Width="100px" Text="Cancel" CssClass="FLATBUTTON" height="20px"></asp:button></TD>
												</TR>
											</TABLE>
										</asp:Panel>
										<asp:Panel id="btnPanel" runat="server" Width="672px">
											<asp:button id="btnNew_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
												tabIndex="200" runat="server" Font-Bold="false" Width="100px" Text="Add" CssClass="FLATBUTTON"
												height="20px"></asp:button>
											<asp:button id="btnModify_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/edit2.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
												tabIndex="205" runat="server" Height="20px" Width="100px" Text="Edit" CssClass="FLATBUTTON"
												CausesValidation="False"></asp:button>
											<asp:button id="btnDelete_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
												tabIndex="210" runat="server" Font-Bold="false" Width="100px" Text="Delete" CssClass="FLATBUTTON"
												height="20px"></asp:button>
											<asp:button id="btnClose" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
												tabIndex="200" runat="server" Font-Bold="false" Width="100px" Text="Back" CssClass="FLATBUTTON"
												height="20px"></asp:button>
										</asp:Panel></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
			<input id="HiddenSelectedNode" type="hidden" name="HiddenSelectedNode"/>
		</form>
		<SCRIPT type="text/javascript">

//		    function SetSelectedNodes(link, vValue) {
//		        alert('event souce:' + ((link) ? link.href : 'undefined'));
//		        var obj = document.getElementById("HiddenSelectedNode");
//		        obj.value = vValue;
//		    }

		    function setAction(vValue) {
		        document.getElementById("isSoftQuestionFormVisible").value = vValue;
		        return true;
		    }
		    
			//SetScrollAreaCtrTreeV("softQuestionTree_moTree","100%","100%");
			
			function TreeCtr_OnClickEvent(elem)
			{   
			 	document.getElementById("btnPanel").disabled = false
			}
			
			function ShowSoftQuestionForm()
			{
			    document.all.item('SoftQForm').style.display = '';
			}
		
			function HideSoftQuestionForm()
			{
			    document.all.item('SoftQForm').style.display = 'none';
			}
			 			
//			function LoadSoftQuestion()
//			{
//				//debugger;
//				//var url = "SoftQuestionForm.aspx;"
//				//var frame = document.all.item('iframe1');
//				//frame.src = url;
//				//frame.style.display='';
//				//document.all("isSoftQuestionFormVisible").value = "True";
//				//document.all.item("btnModify_WRITE").disabled = true;
//				//document.all.item("btnNew_WRITE").disabled = true;
//				//document.all.item("btnDelete_WRITE").disabled = true;
//				//document.all.item("btnMoveDown").disabled = true;
//				//document.all.item("btnMoveUp").disabled = true;
//				//document.all.item("WorkingPanel").disabled = true;
//			}
//			
//			function SaveSoftQuestion()
//			{
//				//reload the grid...
//			//	document.all("NextAction").value = "RefreshTree";
//			//	CloseSoftQuestion();
//			}

//			function CloseSoftQuestion()
//			{
//				//var frame = document.all.item('iframe1');
//				//frame.src='';
//				//frame.style.display='none';
//				//document.all("isSoftQuestionFormVisible").value = "False";
//				
//				//document.all.item("btnModify_WRITE").disabled = false;
//				//document.all.item("btnNew_WRITE").disabled = false;
//				//document.all.item("btnDelete_WRITE").disabled = false;
//				//document.all.item("btnMoveDown").disabled = false;
//				//document.all.item("btnMoveUp").disabled = false;
//				//document.all.item("WorkingPanel").disabled = false;
//				//document.Form1.submit();
//			}

		</SCRIPT>
	</body>
</HTML>
