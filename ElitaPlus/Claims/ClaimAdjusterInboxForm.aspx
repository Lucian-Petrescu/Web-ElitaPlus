<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.Common" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ClaimAdjusterInboxForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimAdjusterInboxForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TemplateForm</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
        <style type="text/css">
            /* A scrolable div */
            .GridViewContainer
            {           
                overflow: auto;
            }           
            #modataGrid td { 
                height: 23px; 
                white-space: nowrap;
                overflow:hidden;
            }
            #modataGrid th { 
                height: 23px; 
                white-space: nowrap;
                overflow:hidden;
            }            
    </style>
        <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js" > </script>
        <script type="text/javascript">
             
            $(window).on('resize', function () {
                if ($("#modataGrid").length) {
                    $("#GridViewContainer").css("width", $(window).width() * 0.95);
                    var gh = $(window).height() - 340;
                    if (gh < 290) {
                        gh = 290;
                    }
                    $("#GridViewContainer").css("height", gh);
                    var sdw = $("#GridViewContainer").width() - $("#FrozenCols").width() - 20;
                    $("#ScrollCols").css("width", sdw);
                }                 
             });

             $(document).ready(function () {
               if ($("#modataGrid").length) {
                     var gvv = $("#modataGrid");
                     $("#GridViewContainer").css("width", $(window).width() * 0.95);
                     var gh = $(window).height() - 340;
                     if (gh < 290) {
                         gh = 290;
                     }
                     $("#GridViewContainer").css("height", gh);
                    //$("#GridViewContainer").css("height", $(window).height() * 0.6);

                    var tab = $("#<%=modataGrid.ClientID%>").clone(true);
                    var tabFreeze = $("#<%=modataGrid.ClientID%>").clone(true);

                    var totalWidth = $("#<%=modataGrid.ClientID%>").outerWidth();
                    var frozenColsWidth = $("#<%=modataGrid.ClientID%> th:first-child").outerWidth();

                    var frozenColsWidth = 0;
                    $("th.FrozenHeader").each(function () {
                        frozenColsWidth = frozenColsWidth + this.scrollWidth;
                    });

                    tabFreeze.width(frozenColsWidth);
                    tab.width(totalWidth - frozenColsWidth + 500);

                    var pf = tab.find('tr.PAGER_LEFT').clone();
                    var pftd = tab.find("td.GRD_PAGER").clone();

                    tabFreeze.find('th:not(".FrozenHeader")').remove();
                    tabFreeze.find('td:not(".FrozenCell")').filter(':not(".GRD_PAGER")').remove();
                    tabFreeze.find('td.GRD_PAGER').replaceWith(pftd);

                    tab.find("th.FrozenHeader").remove();
                    tab.find("td.FrozenCell").remove();
                    tab.find('tr.PAGER_LEFT').remove();
		        

                    var container = $('<table border="0" cellpadding="0" cellspacing="0"><tr><td style="vertical-align:top;"><div id="FrozenCols"></div></td><td style="vertical-align:top;"><div id="ScrollCols" style="width:320px;overflow:auto"></div></td></tr></table>');
                    $("#FrozenCols", container).html($(tabFreeze));
                    $("#ScrollCols", container).html($(tab));
                    $("#ScrollCols", container).css("width", $(GridViewContainer).width() - frozenColsWidth - 20);
                
                    $("#GridViewContainer").html('');
                    $("#GridViewContainer").append(container);
                 }
	        });
        </script>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout" 
		border="0">
		<form id="Form1" method="post" runat="server">
            <asp:ToolkitScriptManager ID="ScriptManager1" runat="server">
            </asp:ToolkitScriptManager>
			<!--Start Header-->
			<TABLE style="BORDER: black 1px solid; MARGIN: 5px; WIDTH: 99%; HEIGHT: 20px"
				cellSpacing="0" cellPadding="0" width="99%" bgColor="#d5d6e4" border="0">
				<tr>
					<td vAlign="top">
						<TABLE width="100%" border="0">
							<tr>
								<td height="20"><asp:label id="LabelTables" runat="server"  CssClass="TITLELABEL">Claims</asp:label>:&nbsp;<asp:label id="Label1a" runat="server"  CssClass="TITLELABELTEXT">Adjuster_Inbox</asp:label>&nbsp;
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; MARGIN: 5px; height:93%;"
				cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--d5d6e4-->
				<tr>
					<td style="HEIGHT: 8px"></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="100%"><asp:panel id="WorkingPanel" runat="server">
							<TABLE id="tblMain1" style="BORDER: #999999 1px solid; height:98%;"
								cellSpacing="0" cellPadding="6" width="98%" align="center" bgColor="#fef9ea"
								border="0">
								<tr>
									<td vAlign="top" height="1">
										<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController></td>
								</tr>
								<tr>
									<td vAlign="top" align="center" width="100%">
										<TABLE cellSpacing="2" cellPadding="2" width="100%" align="center" border="0">
											<tr>
												<td align="center" colSpan="2">
													<TABLE id="tblSearch" style="BORDER: #999999 1px solid; WIDTH: 98%; HEIGHT: 76px"
														cellSpacing="0" cellPadding="6" width="98%" align="center" bgColor="#f1f1f1" border="0"> <!--fef9ea-->
														<tr>
															<td align="center">
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
                                                                    <tr class="LeftAlign">
                                                                        <td style="width:25%;">
                                                                            <asp:Label ID="lblClaimNumber" runat="server">CLAIM_NUMBER</asp:Label>:<br />
                                                                            <asp:TextBox ID="txtClaimNumber" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" Width="90%" TabIndex="0"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width:25%;">
                                                                            <asp:Label ID="lblAuthorizationNumber" runat="server">Authorization_Number</asp:Label>:<br />
                                                                            <asp:TextBox ID="txtAuthorizationNumber" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" Width="90%" TabIndex="1"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width:25%;">
                                                                            <asp:Label ID="lblClaimStatus" runat="server">Claim_Status</asp:Label>:<br />
                                                                            <asp:dropdownlist id="cboClaimStatus" runat="server" Width="90%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" TabIndex="2">
                                                                                 <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                                <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                                                <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                                                <asp:ListItem Text="D" Value="D"></asp:ListItem>
                                                                            </asp:dropdownlist>
                                                                        </td>
                                                                        <td style="width:25%;">
                                                                            <asp:Label ID="lblClaimType" runat="server">Claim_Type</asp:Label>:<br />
                                                                            <asp:dropdownlist id="cboClaimType" runat="server" Width="90%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" TabIndex="3"></asp:dropdownlist>
                                                                        </td>
                                                                    </tr>
                                                                    <tr class="LeftAlign">
                                                                        <td style="width:25%;">
                                                                            <asp:Label ID="lblClaimExtendedStatus" runat="server">EXTENDED_CLAIM_STATUS</asp:Label>:<br />
                                                                            <asp:dropdownlist id="cboClaimExtendedStatus" runat="server" Width="90%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" TabIndex="4"></asp:dropdownlist>
                                                                        </td>
                                                                        <td style="width:25%;">
                                                                            <asp:Label ID="lblStatusOwner" runat="server">OWNER</asp:Label>:<br />
                                                                            <asp:dropdownlist id="cboStatusOwner" runat="server" Width="90%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" TabIndex="5"></asp:dropdownlist>
                                                                        </td>
                                                                        <td style="width:25%;">
                                                                            <asp:Label ID="lblSCTurnAroundTime" runat="server">SC_Turn_Around_Time</asp:Label>:<br />
                                                                            <asp:dropdownlist id="cboSCTurnAroundTime" runat="server" Width="90%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" TabIndex="6"></asp:dropdownlist>
                                                                        </td>
                                                                        <td style="width:25%;">
                                                                            <asp:Label ID="lblServiceCenterName" runat="server">SERVICE_CENTER_NAME</asp:Label>:<br />
                                                                            <asp:TextBox ID="txtServiceCenterName" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" Width="90%" TabIndex="7"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:25%;">
                                                                            <asp:Label ID="lblExt_Sts_Date_From" runat="server">Ext_Sts_Date_From</asp:Label>:<br />                                                                            
                                                                            <asp:TextBox ID="txtExt_Sts_Date_From" runat="server" AutoPostBack="False" SkinID="exSmallTextBox" Width="100px"></asp:TextBox>
                                                                            <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" Width="20px" />
                                                                        </td>
                                                                        <td style="width:25%;">
                                                                            <asp:Label ID="lblExt_Sts_Date_To" runat="server">Ext_Sts_Date_To</asp:Label>:<br />                                                                            
                                                                            <asp:TextBox ID="txtExt_Sts_Date_To" runat="server" AutoPostBack="False" SkinID="exSmallTextBox" Width="100px"></asp:TextBox>
                                                                            <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" Width="20px" />
                                                                        </td>
                                                                        <td style="width:25%;">
                                                                            <asp:Label ID="lblAutoApproved" runat="server">Auto_Approved</asp:Label>:<br />
                                                                            <asp:dropdownlist id="cboAutoApproved" runat="server" Width="90%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" TabIndex="6"></asp:dropdownlist>
                                                                        </td>
                                                                        <td style="width:25%;">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width:25%;">
                                                                            <asp:Label ID="lblClaimAdjuster" runat="server">CLAIM_ADJUSTER</asp:Label>:<br />                                                                            
                                                                            <asp:TextBox ID="txtClaimAdjuster" runat="server" AutoPostBack="False" SkinID="FLATTEXTBOX_TAB"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width:25%;">
                                                                            <asp:Label ID="lblCreatedBy" runat="server">CREATED_BY</asp:Label>:<br />                                                                            
                                                                            <asp:TextBox ID="txtCreatedBy" runat="server" AutoPostBack="False" SkinID="FLATTEXTBOX_TAB"></asp:TextBox>
                                                                        </td>
                                                                        <td style="width:25%;">
                                                                        </td>
                                                                        <td style="width:25%;">
                                                                        </td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td style="width:25%;">
                                                                            <asp:Label ID="lblSortBy" runat="server">Sort By</asp:Label>:<br />
                                                                            <asp:DropDownList id="cboSortBy" runat="server" Width="90%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" TabIndex="8"></asp:DropDownList>
                                                                        </td>
                                                                        <td style="width:25%;">
                                                                            <asp:Label ID="lblSortOrder" runat="server">SORT_ORDER</asp:Label>:<br />
                                                                            <asp:DropDownList id="cboSortOrder" runat="server" Width="90%" 
                                                                                CssClass="FLATTEXTBOX_TAB" AutoPostBack="False" TabIndex="8"></asp:DropDownList>
                                                                        </td>
                                                                        <td style="width:25%;">
                                                                        </td>
                                                                        <td style="text-align:left;vertical-align:text-bottom; padding-right:4px;padding-bottom:5px"><br />
                                                                            <asp:Button ID="btnClearSearch" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" runat="server" Text="Clear" TabIndex="11" />&nbsp;&nbsp;
                                                                            <asp:Button ID="btnSearch" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH" runat="server" Text="Search" TabIndex="10" />
                                                                        </td>
                                                                    </tr>	
                                                                 </TABLE>
															</td>
														</tr>
													</TABLE>
												</td>
											</tr>
                                           	<TR>
												<TD colSpan="2">
													<HR style="HEIGHT: 1px">
												</TD>
											</TR>
											<TR id="trPageSize" runat="server">
												<td id="tDDataGrid" runat="server">
                                                    <table style="border: black 0px solid;" cellspacing="1" cellpadding="1" width="100%" border="0">
                                                        <tr>
                                                            <td style="width: 627px" valign="top" align="left">
													            <asp:label id="lblPageSize" runat="server">Page_Size</asp:label>: &nbsp;
													            <asp:dropdownlist id="cboPageSize" runat="server" AutoPostBack="true" Width="50px">
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
													            </asp:dropdownlist>
                                                                <input id="HiddenSavePagePromptResponse" style="width: 8px; height: 18px" type="hidden" size="1" runat="server" />
                                                                <input id="HiddenIsPageDirty" style="width: 8px; height: 18px" type="hidden" size="1" runat="server" />
                                                            </td>
												            <td align="right">
                                                                 <asp:label id="lblRecordCount" Runat="server"></asp:label>
                                                            </td>
                                                        </tr>
                                                        </table>
                                                  </td>
                                             </tr>
                                             <tr>
												<td id="tDDataGrid1" runat="server">
                                                    <table style="border: black 1px solid;" cellspacing="1" cellpadding="1" width="100%" border="0">
									            		<tr>
												            <td colspan="3">
                                                                <div id="GridViewContainer" class="GridViewContainer" style="overflow:auto;" align="left">                                                                    
        											                            <asp:GridView id="modataGrid" runat="server" Style="z-index: 0; left: 0px; position: relative; top: 0px; "
                                                                                 Width="100%"  AutoGenerateColumns="False" BorderStyle="Solid"
														                        BorderWidth="1px" BorderColor="Black" CellPadding="1" AllowPaging="True" AllowSorting="True"
                                                                                OnRowCreated="ItemCreated" OnRowCommand="ItemCommand" CssClass="myGrid" >
                                                                                    <SelectedRowStyle Wrap="False" BackColor="Transparent"></SelectedRowStyle>
                                                                                    <EditRowStyle Wrap="False" BackColor="AliceBlue"></EditRowStyle>
                                                                                    <AlternatingRowStyle Wrap="False" BackColor="#F1F1F1"></AlternatingRowStyle>
                                                                                    <RowStyle Wrap="False" BackColor="White"></RowStyle>
                                                                                    <HeaderStyle Wrap="False" Height="25px" ForeColor="#12135B"></HeaderStyle>
														                            <Columns>
                                                                                        <asp:BoundField Visible="False" ItemStyle-CssClass="FrozenCell" HeaderStyle-CssClass="FrozenCell" HeaderText="Claim_Id"></asp:BoundField>
                                                                                        <asp:TemplateField ItemStyle-CssClass="FrozenCell" HeaderStyle-CssClass="FrozenHeader">
                                                                                            <HeaderTemplate>
                                                                                                <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" />
                                                                                            </HeaderTemplate>
                                                                                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="10px" BackColor="White" ></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:checkbox ID="btnSelected" runat="server"></asp:checkbox> 
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField ItemStyle-CssClass="FrozenCell" HeaderStyle-CssClass="FrozenHeader">
                                                                                            <ItemStyle Wrap="False" HorizontalAlign="Left" Width="10px" BackColor="White" ></ItemStyle>
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="btnEdit" runat="server" CommandName="EditRecord" 
                                                                                                    ImageUrl="../Navigation/images/icons/edit2.gif" style="cursor:hand;" CommandArgument="<%#Container.DisplayIndex %>"  /> 
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField HeaderText="Claim_TAT" ItemStyle-Width="185px" ItemStyle-CssClass="FrozenCell" HeaderStyle-CssClass="FrozenHeader" HeaderStyle-Wrap="true" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" >
                                                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField HeaderText="SC_TAT" ItemStyle-Width="150px" ItemStyle-CssClass="FrozenCell" HeaderStyle-CssClass="FrozenHeader" HeaderStyle-Wrap="true" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" /> 	
                                                                                        <asp:BoundField HeaderText="Claim_Number" ItemStyle-Width="150px" ItemStyle-CssClass="FrozenCell" HeaderStyle-CssClass="FrozenHeader" ItemStyle-VerticalAlign="Middle" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                                                                        <asp:BoundField HeaderText="Status_Code" ItemStyle-Width="100px" ItemStyle-CssClass="FrozenCell" HeaderStyle-CssClass="FrozenHeader" ItemStyle-VerticalAlign="Middle" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />                                                                                        
                                                                                        <asp:BoundField HeaderText="claim_adjuster" 
                                                                                            ItemStyle-Width="170px" HeaderStyle-Wrap="false" HeaderStyle-CssClass="NormalCell" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                                                                                        <asp:BoundField HeaderText="CREATED_BY" 
                                                                                            ItemStyle-Width="170px" HeaderStyle-Wrap="false" HeaderStyle-CssClass="NormalCell" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                                                                                        <asp:BoundField HeaderText="AUTO_APPROVED" 
                                                                                            ItemStyle-Width="170px" HeaderStyle-Wrap="false" HeaderStyle-CssClass="NormalCell" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                                                                                        <asp:BoundField HeaderText="LAST_CLAIM_EXTENDED_STATUS" 
                                                                                            ItemStyle-Width="170px" HeaderStyle-Wrap="false" HeaderStyle-CssClass="NormalCell" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" />
                                                                                        <asp:BoundField HeaderText="OWNER" 
                                                                                            ItemStyle-Width="170px" ItemStyle-VerticalAlign="Middle" HeaderStyle-Wrap="false" HeaderStyle-CssClass="NormalCell" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                                                                        <asp:BoundField HeaderText="Authorization_Number" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                                                                        <asp:BoundField HeaderText="SERVICE_CENTER_NAME" ItemStyle-Width="300px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="false" />
                                                                                        <asp:BoundField HeaderText="CLAIM_TYPE" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                                                                        <asp:BoundField HeaderText="PRODUCT" ItemStyle-Width="300px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="false" />
                                                                                        
                                                                                        <asp:BoundField HeaderText="Sales_Price"  HtmlEncode="false" HtmlEncodeFormatString="false" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                                                                        <asp:BoundField HeaderText="Coverage_Type" ItemStyle-Width="200px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="false" />
                                                                                        <asp:BoundField HeaderText="End_Date" DataFormatString="{0:d}" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                                                                        <asp:BoundField HeaderText="Payment_Amount" ItemStyle-Width="200px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="left" ItemStyle-Wrap="false" />
                                                                                        
                                                                                        <asp:BoundField HeaderText="Authorized_Amount" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                                                                        <asp:BoundField HeaderText="Proposed_Amount" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                                                                        <asp:BoundField HeaderText="LABOR" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                                                                        <asp:BoundField HeaderText="Parts" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                                                                        <asp:BoundField HeaderText="Other" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                                                                        <asp:BoundField HeaderText="Shipping" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                                                                        <asp:BoundField HeaderText="TRIP_AMOUNT" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                                                                        <asp:BoundField HeaderText="SERVICE_CHARGE" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
														                                <asp:BoundField HeaderText="risk_type_id" Visible="False" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                                                                        
                                                                                        <asp:TemplateField ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"  HeaderText="Problem_Description" ItemStyle-Width="10px">
                                                                                             <ItemTemplate> 
                                                                                                <asp:Image ID="Image1" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"  /> 
                                                                                                <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" 
                                                                                                   PopupControlID="Panel1" 
                                                                                                   TargetControlID="Image1" 
                                                                                                   DynamicContextKey= '<%# New Guid(CType(Eval("CLAIM_ID"), Byte())).ToString() %>'
                                                                                                   DynamicControlID="Panel1" 
                                                                                                   DynamicServiceMethod="GetProblemDescription" Position="Bottom"></asp:PopupControlExtender> 
                                                                                             </ItemTemplate> 
                                                                                        </asp:TemplateField> 

                                                                                        <asp:TemplateField ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center"  HeaderText="Technical_Report" ItemStyle-Width="10px">
                                                                                             <ItemTemplate> 
                                                                                                <asp:Image ID="Image2" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"  /> 
                                                                                                <asp:PopupControlExtender ID="PopupControlExtender2" runat="server" 
                                                                                                   PopupControlID="Panel1" 
                                                                                                   TargetControlID="Image2" 
                                                                                                   DynamicContextKey= '<%# New Guid(CType(Eval("CLAIM_ID"), Byte())).ToString() %>'
                                                                                                   DynamicControlID="Panel1" 
                                                                                                   DynamicServiceMethod="GetTechnicalReport" Position="Bottom"></asp:PopupControlExtender> 
                                                                                             </ItemTemplate> 
                                                                                        </asp:TemplateField> 
                                                                                        
                                                                                        <asp:TemplateField  HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="false"  HeaderText="Extended_Status_Comment" >
                                                                                             <ItemTemplate> 
                                                                                                <asp:Image ID="Image3" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"  /> 
                                                                                                <asp:PopupControlExtender ID="PopupControlExtender3" runat="server" 
                                                                                                   PopupControlID="Panel1" 
                                                                                                   TargetControlID="Image3" 
                                                                                                   DynamicContextKey= '<%# New Guid(CType(Eval("CLAIM_ID"), Byte())).ToString() %>'
                                                                                                   DynamicControlID="Panel1" 
                                                                                                   DynamicServiceMethod="GetExtendedStatusComment" Position="Bottom"></asp:PopupControlExtender> 
                                                                                             </ItemTemplate> 
                                                                                        </asp:TemplateField>
														                                <asp:BoundField HeaderText="INBOUND_TRACKING_NUMBER" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
														                                <asp:BoundField HeaderText="OUTBOUND_TRACKING_NUMBER" ItemStyle-Width="100px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
														                                <asp:BoundField HeaderText="REPLACEMENT_DEVICE" ItemStyle-Width="300px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
														                                <asp:BoundField HeaderText="REPLACEMENT_DEVICE_COMMENTS" ItemStyle-Width="300px" ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell" HeaderStyle-Wrap="true" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="false" />
                                                                                        
                                                                                        <asp:TemplateField HeaderText="Comment" HeaderStyle-CssClass="NormalCell">
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="txtComment" runat="server" MaxLength="950" ></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                    </Columns>
														                            <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                                                                    <PagerStyle HorizontalAlign="Left" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" CssClass="PAGER_LEFT"></PagerStyle>
													                            </asp:GridView>                                                                            
                                                                    <asp:Literal ID="CheckBoxIDsArray" runat="server"></asp:Literal>
                                                                </div>
                                                            </td>
											            </tr>
                                                    </table>
                                                </td>
                                            </tr>
											<tr>
									            <td align="left">
										            <asp:button id="ApproveButton_WRITE" Enabled="False" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											            runat="server"  Width="100px" Text="Approve" height="20px" CssClass="FLATBUTTON" Visible="False"></asp:button>&nbsp;&nbsp;
										            <asp:button id="RejectButton_WRITE" Enabled="False" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											            runat="server"  Width="100px" Text="Reject" height="20px" CssClass="FLATBUTTON" Visible="False"></asp:button>&nbsp;
									            </td>
											</tr>
										</TABLE>
									</td>
								</tr>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
            <asp:Panel ID="Panel1" runat="server"> </asp:Panel>
            <input type="hidden" id="checkRecords" value="" runat="server" />
            <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" designtimedragdrop="261" />
        <script>

        function setDirty() {
            document.getElementById("HiddenIsPageDirty").value = "YES"
        }

        function CheckboxAction(cbValue, cbClientId, btnClientId1, btnClientId2, hiddenClientId) {
            var objCbId = document.getElementById(cbClientId);
            var objbtnId1 = document.getElementById(btnClientId1);
            var objbtnId2 = document.getElementById(btnClientId2);
            var objCheckRecords = document.getElementById(hiddenClientId);
            var tempValue = "";
            var objHiddenIsPageDirtyId = document.getElementById('HiddenIsPageDirty');

            if (objCbId != null && cbValue != null) {
                if (objCbId.checked) {
                    objCheckRecords.value = cbValue + ':' + objCheckRecords.value;
                }
                else {
                    objCheckRecords.value = objCheckRecords.value.replace(cbValue, '');
                }

                tempValue = objCheckRecords.value;
                tempValue = tempValue.replace(/:/g, '');

                if (tempValue != null && tempValue != '') {
                    objbtnId1.disabled = false;
                    objbtnId2.disabled = false;
                    objHiddenIsPageDirtyId.value = 'YES';
                }
                else {
                    objbtnId1.disabled = true;
                    objbtnId2.disabled = true;
                    objHiddenIsPageDirtyId.value = 'NO';
                }

                var intCheckedCount = 0;
                for (var i = 0; i < CheckBoxIDs.length; i++) {
                    objCbId = document.getElementById(CheckBoxIDs[i]);
                    if (objCbId.checked == true) {
                        intCheckedCount = intCheckedCount + 1;
                        break;
                    }
                }
                if (intCheckedCount > 0) {
                    objbtnId1.disabled = false;
                    objbtnId2.disabled = false;
                    objHiddenIsPageDirtyId.value = 'YES';
                }
                else {
                    objbtnId1.disabled = true;
                    objbtnId2.disabled = true;
                    objHiddenIsPageDirtyId.value = 'NO';
                }


            }
        }

        function ChangeCheckBoxState(id, checkState) {
            var cb = document.getElementById(id);
            if (cb != null)
                if (cb.disabled == false) 
                    cb.checked = checkState;
        }

        function ChangeAllCheckBoxStates(checkState, btnClientId1, btnClientId2) {
            // Toggles through all of the checkboxes defined in the CheckBoxIDs array
            // and updates their value to the checkState input parameter
            if (CheckBoxIDs != null) {
                for (var i = 0; i < CheckBoxIDs.length; i++)
                    ChangeCheckBoxState(CheckBoxIDs[i], checkState);
            }

            var objbtnId1 = document.getElementById(btnClientId1);
            var objbtnId2 = document.getElementById(btnClientId2);
            var objHiddenIsPageDirtyId = document.getElementById('HiddenIsPageDirty');

            if (checkState == true) {
                objbtnId1.disabled = false;
                objbtnId2.disabled = false;
                objHiddenIsPageDirtyId.value = 'YES';
            }
            else {
                objbtnId1.disabled = true;
                objbtnId2.disabled = true;
                objHiddenIsPageDirtyId.value = 'NO';
            }


        }

        function ChangeHeaderAsNeeded() {
            // Whenever a checkbox in the GridView is toggled, we need to
            // check the Header checkbox if ALL of the GridView checkboxes are
            // checked, and uncheck it otherwise
            if (CheckBoxIDs != null) {
                // check to see if all other checkboxes are checked
                for (var i = 1; i < CheckBoxIDs.length; i++) {
                    var cb = document.getElementById(CheckBoxIDs[i]);
                    if (!cb.checked) {
                        // Whoops, there is an unchecked checkbox, make sure
                        // that the header checkbox is unchecked
                        ChangeCheckBoxState(CheckBoxIDs[0], false);
                        return;
                    }
                }

                // If we reach here, ALL GridView checkboxes are checked
                ChangeCheckBoxState(CheckBoxIDs[0], true);
            }
        }
        function ToggleSelection1(ctlDropDown1, ctlDropDown2) {
            var objDropDown1 = document.getElementById(ctlDropDown1);
            var objDropDown2 = document.getElementById(ctlDropDown2);

            if (objDropDown2.value != -1)
                objDropDown2.value = -1;
        }
        function ToggleSelection2(ctlDropDown1, ctlDropDown2) {
            var objDropDown1 = document.getElementById(ctlDropDown1);
            var objDropDown2 = document.getElementById(ctlDropDown2);

            if (objDropDown1.value != -1)
                objDropDown2.value = -1;
        }
         </script>
		</form>
	</body>
</HTML>
