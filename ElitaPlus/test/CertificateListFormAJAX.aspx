<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CertificateListFormAJAX.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.CertificateListFormAJAX" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
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
<xml id="SrchResults" ></xml>
</HEAD>
	<body onload="BodyLoad();" onresize="" leftMargin="0" topMargin="0" >
		<form id="Form1" runat="server" >
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; WIDTH: 98%; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
				cellSpacing="0" cellPadding="0" width="869" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="moTitleLabel" runat="server" Font-Bold="false">Certificates</asp:label>:&nbsp;<asp:label id="moTitle2Label" runat="server"  ForeColor="#12135B">Certificate</asp:label>&nbsp;<asp:label id="Label1a" runat="server"  ForeColor="#12135B">Search</asp:label>&nbsp;AJAX</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--d5d6e4-->
				<tr>
					<td style="HEIGHT: 8px"></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="100%" >
            <asp:panel id="WorkingPanel" runat="server" Height="100%" Width="100%">
      <TABLE id=tblMain1 
      style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid" 
      height="98%" cellSpacing=0 cellPadding=6 rules=cols width="98%" 
      align=center bgColor=#fef9ea border=0>
        <TR>
          <TD vAlign=top height=1><uc1:ErrorController id=ErrorCtrl runat="server"></uc1:ErrorController></TD>
        </TR>
        <TR>
          <TD vAlign=top align=center width="100%">
            <TABLE cellSpacing=2 cellPadding=2 width="100%" align=center border=0>
              <TR>
                <TD align=center colSpan=2>
                  <TABLE id=tblSearch 
                  style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 98%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 76px" 
                  cellSpacing=0 cellPadding=6 rules=cols width="98%" 
                  align=center bgColor=#f1f1f1 border=0><!--fef9ea-->
                    <TR>
                      <TD align=center>
                        <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
                          <TR>
                            <TD style="HEIGHT: 12px" noWrap align=left width="1%">
                            <asp:label id=moCertificateLabel runat="server">Certificate</asp:label>:</TD>
                            <TD style="HEIGHT: 12px" noWrap align=left width="1%">
                            <asp:label id=moCustomerNameLabel runat="server">Customer_Name</asp:label>:</TD>
                            <TD style="HEIGHT: 12px" noWrap align=left width="1%">
                            <asp:label id=moAddressLabel runat="server">Address</asp:label>:</TD></TR>
                          <TR>
                            <TD noWrap align=left>
                            <asp:textbox id=moCertificateText runat="server" Width="75%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False"></asp:textbox></TD>
                            <TD noWrap align=left>
                            <asp:textbox id=moCustomerNameText runat="server" Width="75%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False"></asp:textbox></TD>
                            <TD noWrap align=left>
                            <asp:textbox id=moAddressText runat="server" Width="100%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False"></asp:textbox></TD>
                          <TR>
                          <TR>
                            <TD noWrap align=left width="1%">
                            <asp:label id=moZipLabel runat="server">Zip</asp:label>:</TD>
                            <TD noWrap align=left width="1%">
                            <asp:label id=moTaxIdLabel runat="server">Tax ID</asp:label>:</TD>
                            <TD noWrap align=left width="1%">
                            <asp:label id=moDealerLabel runat="server">Dealer</asp:label>:</TD></TR>
                          <TR>
                            <TD noWrap align=left>
                            <asp:textbox id=moZipText runat="server" Width="75%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False"></asp:textbox></TD>
                            <TD noWrap align=left>
                            <asp:textbox id=moTaxIdText runat="server" Width="75%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False"></asp:textbox></TD>
                            <TD noWrap align=left>
                            <asp:dropdownlist id=moDealerDrop runat="server" Width="100%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False"></asp:dropdownlist></TD></TR>
                          <TR>
                            <TD style="HEIGHT: 12px" colSpan=3>
                            <asp:label id=lblSearchSortBy runat="server">Sort By</asp:label>:</TD></TR>
                          <TR>
                            <TD noWrap align=left>
                            <asp:dropdownlist id=cboSortBy runat="server" Width="75%" AutoPostBack="False"></asp:dropdownlist></TD>
                            <TD noWrap align=right colSpan=3>
                            <input type=button id=btnClearSearch onclick="ClearForm();" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; width: 90px; BACKGROUND-REPEAT: no-repeat" Class="FLATBUTTON" title="Clear Form" value="Clear"/>
                            <input type=button id=btnAJAXSearch onclick="GetList('GetCertificateList');" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; width: 90px; BACKGROUND-REPEAT: no-repeat" Class="FLATBUTTON" title="AJAX Search" value="Search"/></TD>
                          </TR>
                          <TR>
                            <TD colSpan=3>
                            </TD>
                          </TR>
                          <TR id=trPageSize runat="server">
                            <TD id=tdPageSize style="HEIGHT: 22px" vAlign=top align=left>
                                <asp:label id=lblPageSize runat="server">Page_Size</asp:label>: 
                              &nbsp; 
                                            <asp:dropdownlist id=cboPageSize runat="server" Width="50px">
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
				            </TD>
                            <TD style="HEIGHT: 22px" align=right colSpan=3>No. of Certificates Found: 
                            <asp:label id=lblRecordCount Runat="server"></asp:label></TD></TR>
                          <TR>
                            <TD vAlign=top align=center colSpan=3 height="100%">								</TD>
						  </TR>
						</TABLE>
						</TD>
					</TR>
				</TABLE>
                  <HR style="HEIGHT: 1px">
                <input id="lblTK" type="hidden" runat="server" />&nbsp;<br />
				<table id="SrchResultsHdr" class="DATAGRID" cellspacing="0" cellpadding="1" rules="all" border="1" style="background-color:#DEE3E7;border-color:#999999;border-width:1px;border-style:Solid;height:100%;width:100%;border-collapse:collapse;">
		            <tr >
			            <td align="center" style="color:#12135B;width:30px;"><span id=hdr1 >&nbsp;</span></td>
			            <td align="center" style="color:#12135B;width:15%;"><span id=hdr2 >Certificate</span></td>
			            <td align="center" style="color:#12135B;width:15px;"><span id=hdr3 >&nbsp;</span></td>
			            <td align="center" style="color:#12135B;width:25%;white-space:nowrap;"><span id=hdr4 >Customer Name</span></td>
			            <td align="center" style="color:#12135B;width:35%;"><span id=hdr5 >Address</span></td>
			            <td align="center" style="color:#12135B;width:5%;"><span id=hdr6 >ZipP</span></td>
			            <td align="center" style="color:#12135B;width:5%;"><span id=hdr7 >Tax ID</span></td>
			            <td align="center" style="color:#12135B;width:10%;"><span id=hdr8 >Dealer</span></td>
			            <td align="center" style="color:#12135B;width:1%;"><span id=hdr9 >Product Code</span></td>
		            </tr>
				</TABLE>
				<table id=SrchResultsGrid class="DATAGRID" datasrc="#SrchResults" cellspacing="0" cellpadding="1" rules="all" border="1" style="background-color:#DEE3E7;border-color:#999999;border-width:1px;border-style:Solid;height:100%;width:100%;border-collapse:collapse;">
		            <tr style="background-color:White;white-space:nowrap;">
			            <td align="center" style="width:6px;" onclick="GetCertificateDetail(this);" onmouseover="this.style.cursor='hand';" onmouseout="this.style.cursor='default';">
						<input Type="hidden" id="Hidden8" name="CERT_ID" datafld="CERT_ID" style="width: 3px" />
			            </td>
			            <td align="left" style="width:1px;"><span id="Span1" datafld="CERT_NUMBER"></span><input Type="hidden" id="Hidden9" name="CERT_ID" datafld="CERT_ID" /></td>
			            <td align="center" style="width:1px;"></td>
			            <td align="center" style="width:68px;white-space:nowrap;"><span id="Span8" datafld="CUSTOMER_NAME"></span><input Type="hidden" id="Hidden16" name="CERT_ID" datafld="CERT_ID" /></td>
			            <td align="left" style="width:98px;white-space:nowrap;">
						    <span id="Span3" datafld="ADDRESS1">&nbsp;</span>
						    <input Type="hidden" id="Hidden11" name="CERT_ID" datafld="CERT_ID" />
			            </td>
			            <td align="left" style="width:10px;">
						    <span id="Span4" datafld="POSTAL_CODE">&nbsp;</span>
						    <input Type="hidden" id="Hidden12" name="CERT_ID" datafld="CERT_ID" />
			            </td>
			            <td align="center" style="width:10px;white-space:nowrap;">
						    <span id="Span5" datafld="IDENTIFICATION_NUMBER">&nbsp;</span>
						    <input Type="hidden" id="Hidden13" name="CERT_ID" datafld="CERT_ID" />
			            </td>
			            <td align="center" style="width:26px;white-space:nowrap;">
						    <span id="Span6" datafld="DEALER">&nbsp;</span>
						    <input Type="hidden" id="Hidden14" name="CERT_ID" datafld="CERT_ID" />
			            </td>
			            <td align="center" style="width:11px;white-space:nowrap;">
						    <span id="Span7" datafld="PRODUCT_CODE">&nbsp;</span>
						    <input Type="hidden" id="Hidden15" name="CERT_ID" datafld="CERT_ID" />
			            </td>
		            </tr>
	            </table>		
			            </td>
		            </tr>
            </TABLE>
			            </td>
		            </tr>
            </TABLE>
			</asp:panel>
			</td>
		</tr>
	</TABLE>
        <asj:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asj:ServiceReference />
            </Services>
        </asj:ScriptManager>
		</form>
        <script type="text/javascript"> 
            function BodyLoad()
            {
                //ShowHideCTRL('ErrorCtrl_txtErrorMsg','hide');
                //ShowHideCTRL('ErrorCtrl_moTitle','hide');
                //ShowHideCTRL('ErrorCtrl_btnShowHide','hide');
                ShowHideCTRL('Table1','hide');
                ShowHideCTRL('SrchResultsHdr','hide');
                ShowHideCTRL('SrchResultsGrid','hide');
                ShowHideCTRL('ErrorHorLine','hide');
                ShowHideCTRL('lblPageSize','hide');
                ShowHideCTRL('cboPageSize','hide');
                ShowHideCTRL('trPageSize','hide');
                changeScrollbarColor();
            }
                      
            function GetList(methodToInvoke)
            {
              showProgressFrame("Searching for Certificates", '../Common/MessageProgressForm.aspx');
              var p = document.getElementById("lblTK").value;
              var certnum = document.getElementById("moCertificateText").value;
              var custname = document.getElementById("moCustomerNameText").value;
              var address = document.getElementById("moAddressText").value;
              var zip = document.getElementById("moZipText").value;
              var taxid = document.getElementById("moTaxIdText").value;
              var dealercode = document.getElementById("moDealerDrop").value;
              
              if (dealercode == "00000000-0000-0000-0000-000000000000")
              {
                 dealercode = "";
              }
              
              var sortby = document.getElementById("cboSortBy").value;
              var limitby = 10
			  if (document.getElementById("cboPageSize")!=undefined)
			  {
			    limitby = document.getElementById("cboPageSize").value;
			  }
              
              var pval = p.value;
              var xmlParamsInput = "<" + methodToInvoke + "Input>"
              xmlParamsInput = xmlParamsInput + "<CertificateNumber>" + certnum + "</CertificateNumber>";
              xmlParamsInput = xmlParamsInput + "<CustomerName>" + custname + "</CustomerName>";
              xmlParamsInput = xmlParamsInput + "<Address>" + address + "</Address>";
              xmlParamsInput = xmlParamsInput + "<ZIP>" + zip + "</ZIP>";
              xmlParamsInput = xmlParamsInput + "<TaxID>" + taxid + "</TaxID>";
              xmlParamsInput = xmlParamsInput + "<DealerCode>" + dealercode + "</DealerCode>";
              //xmlParamsInput = xmlParamsInput + "<LimitResultset>10</LimitResultset>";
              xmlParamsInput = xmlParamsInput + "<LimitResultset>" + limitby + "</LimitResultset>";
              xmlParamsInput = xmlParamsInput + "<SortBy>" + sortby + "</SortBy>";
              xmlParamsInput = xmlParamsInput + "</" + methodToInvoke + "Input>";
              
              //var xmlParamsInput = "<GetCertificateListInput><CertificateNumber>*</CertificateNumber><CustomerName></CustomerName><Address></Address><ZIP></ZIP><TaxID></TaxID><DealerCode></DealerCode><LimitResultset>10</LimitResultset><SortBy>dealer</SortBy></GetCertificateListInput>"
              //alert("token: " + pval + " method to invoke: " + methodToInvoke + " -- params: " + xmlParamsInput + " certnum: " + certnum);
              Assurant.ElitaPlus.ElitaInternalWS.UtilityWS.ProcessRequest(p, methodToInvoke, xmlParamsInput, SucceededCallback);
              //Assurant.ElitaPlus.ElitaInternalWS.UtilityWS.HelloWorld(SucceededCallback);
              //alert("fin GetList");
            }
            
            function SucceededCallback(result, eventArgs)
            {
                //alert(result);
                closeProgressFrame();
                SrchResults.loadXML(result);
                //alert("XML island loaded.");
                //alert("GetCertificateListResultset: " + SrchResults.selectSingleNode("//GetCertificateListResultset").xml);
                var errCheck = result.toUpperCase();
                //alert(errCheck);
                if (errCheck.indexOf('ERROR') > 0)
                {
                    ShowHideCTRL('ErrorCtrl_txtErrorMsg','show');
                    ShowHideCTRL('ErrorCtrl_moTitle','show');
                    ShowHideCTRL('ErrorCtrl_btnShowHide','show');
                    ShowHideCTRL('Table1','show');
                    ShowHideCTRL('ErrorHorLine','show');
                    var datanode = SrchResults.selectSingleNode("//Error");
                    document.getElementById("ErrorCtrl_txtErrorMsg").innerText = datanode.text;
                }
                else
                {
                    ShowHideCTRL('ErrorCtrl_txtErrorMsg','hide');
                    ShowHideCTRL('ErrorCtrl_moTitle','hide');
                    ShowHideCTRL('ErrorCtrl_btnShowHide','hide');
                    ShowHideCTRL('Table1','hide');
                    ShowHideCTRL('ErrorHorLine','hide');
                    if (result.indexOf('GetCertificateListResultset') > 0)
                    {
                        var datanode = SrchResults.selectSingleNode("//GetCertificateListResultset");
                        document.getElementById("lblRecordCount").innerHTML = datanode.childNodes.length;
                        if (datanode.childNodes.length > 0)
                        {
                            document.getElementById("SrchResultsHdr").style.display = "";
                            document.getElementById("SrchResultsGrid").style.display = "";
                            ShowHideCTRL('lblPageSize','show');
                            ShowHideCTRL('cboPageSize','show');
                            ShowHideCTRL('trPageSize','show');
                            
                            if (datanode.childNodes.length > 100)
                            {
                                showMessage('More Than 100 Records Were Found. Please Refine Your Search Criteria And Try Again', '', '1', '0', 'null');
                            }
                        }
                    
                    }
                    else
                    {
                            //alert('no result data');
                            ShowHideCTRL('trPageSize','show');
                            ShowHideCTRL('tdPageSize','hide');
                            document.getElementById("lblRecordCount").innerHTML = "0";
                    }
                }
            }
            
            function OnError(result)
            {
              alert("Error: " + result.get_message());
            }
            
            function GetCertificateDetail(obj)
            {
            	showProgressFrame("Loading_Certificates", '../Common/MessageProgressForm.aspx');
            	var oCertNum = obj.children(1);
            	var vCertNumber = oCertNum.value;
            	//alert(vCertNumber);
            	//window.location = "CertificateForm.aspx?"
            	window.open("CertificateForm.aspx?ID=" + encodeURIComponent(vCertNumber),"Navigation_Content");
            	//encodeURIComponent(msg);
            }
            
            function ClearForm()
            {
              document.getElementById("moCertificateText").value = "";
              document.getElementById("moCustomerNameText").value = "";
              document.getElementById("moAddressText").value = "";
              document.getElementById("moZipText").value = "";
              document.getElementById("moTaxIdText").value = "";
              document.getElementById("moDealerDrop").value = "";
            }
            
            function ShowHideCTRL(ctrl, todo)
            {
                if (todo == 'show')
                {
                    document.getElementById(ctrl).style.display = "";
                }
                else
                {
                    document.getElementById(ctrl).style.display = "none";
                }
            }
                        
            </script>

	</body>
</HTML>
