<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TransExceptionDetail.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.TransExceptionDetail" %>

<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>TransactionExceptionDetail</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
     <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet"/>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js" > </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js" > </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/Tabs.js" > </script>  

</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
        border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
        cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td valign="top">
                <table width="100%" border="0">
                    <tr>
                        <td height="20">
                            &nbsp;<asp:Label ID="TablesLabel" runat="server" CssClass="TITLELABEL">INTERFACES</asp:Label>:
                            <asp:Label ID="MaintainAuthDetailLabel" runat="server" CssClass="TITLELABELTEXT">TRANSACTION_EXCEPTION_DETAIL</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
        margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid; height: 93%"
        cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
        <!--d5d6e4-->
        <tr>
            <td style="height: 8px">
            </td>
        </tr>        
        <tr>
            <td valign="top" align="center" height="100%">
                <asp:Panel ID="WorkingPanel" runat="server" Height="98%" Width="98%">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; width: 100%; border-bottom: #999999 1px solid;
                        height: 100%" cellspacing="0" cellpadding="6" rules="cols" width="100%" align="center"
                        bgcolor="#fef9ea" border="0">
                        <tr>
                            <td align="center" width="75%" colspan="2" valign="top" height="1">
                                <uc1:ErrorController ID="ErrController" runat="server"></uc1:ErrorController>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 90%" valign="top" colspan="2" >
                                <div id="scroller" style="overflow: auto; width: 100%; height: 100%" align="center">
                                    <table id="Table3" style="width: 100%" cellspacing="1" cellpadding="0" border="0">
                                        <asp:Panel ID="EditPanel_WRITE" runat="server" Height="100%" Width="100%">
                                            <tr>
                                                <td valign="top">
                                                    <table id="tblHeader" cellspacing="0" cellpadding="0" rules="cols" width="100%" align="center"
                                                        bgcolor="#fef9ea" border="0">                                                      
                                                        <tr>
                                                            <td valign="top" style="height: 20px;" align="left" colspan="3">
                                                                <%=rejectionMsgLbl%>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 33%" align="left">
                                                                <asp:Label ID="LabelOriginator" runat="server">Originator</asp:Label>:
                                                            </td>
                                                            <td style="width: 34%" align="left">
                                                                <asp:Label ID="LabelFunction" runat="server">Function</asp:Label>:
                                                            </td>
                                                            <td style="width: 33%" align="left">
                                                                <asp:Label ID="LabelServiceType" runat="server">ServiceType</asp:Label>:
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="left">
                                                                <asp:TextBox ID="TextboxOriginator" Style="background-color: whitesmoke" TabIndex="1"
                                                                    runat="server" ReadOnly="True" Width="95%" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="TextboxFunction" Style="background-color: whitesmoke" TabIndex="1"
                                                                    runat="server" ReadOnly="True" Width="95%" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                            </td>
                                                            <td align="left">
                                                                <asp:TextBox ID="TextboxServiceType" Style="background-color: whitesmoke" TabIndex="1"
                                                                    runat="server" ReadOnly="True" Width="95%" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                <asp:DropDownList ID="cboServiceType" TabIndex="1" runat="server" Width="95%" Visible ="false"></asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td valign="bottom" nowrap align="left" colspan="3">
                                                                <hr style="width: 100%; height: 1px" size="1">
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table id="Table2" cellspacing="0" cellpadding="0" rules="cols" width="100%" align="center"
                                                        bgcolor="#fef9ea" border="0">
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelCUSTOMER_NAME" runat="server">CUSTOMER_NAME</asp:Label>:
                                                            </td>
                                                            <td style="width: 75%; height: 17px" colspan="2">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxCUSTOMER_NAME" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="320px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 23%; height: 17px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelTAX_ID" runat="server">TAX_ID</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxTAX_ID" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelADDRESS1" runat="server">ADDRESS1</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxADDRESS1" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelADDRESS2" runat="server">ADDRESS2</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxADDRESS2" TabIndex="6" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelPOSTAL_CODE" runat="server">POSTAL_CODE</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxPOSTAL_CODE" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                                                                <asp:Label ID="LabelComuna" runat="server">Comuna:</asp:Label>&nbsp;
                                                                <asp:DropDownList ID="moComunaDropdown" TabIndex="2" runat="server" Width="160px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelSTATE" runat="server">STATE</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxSTATE" TabIndex="6" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelPHONE1" runat="server">PHONE1</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxPHONE1" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelPHONE2" runat="server">PHONE2</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxPHONE2" TabIndex="6" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelEMAIL" runat="server">EMAIL</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxEMAIL" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelRETAILER" runat="server">RETAILER</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxRETAILER" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelINVOICE_NUMBER" runat="server">INVOICE_NUMBER</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxINVOICE_NUMBER" TabIndex="6" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelPRODUCT_SALES_DATE" runat="server">PRODUCT_SALES_DATE</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxPRODUCT_SALES_DATE" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelSALES_PRICE" runat="server">SALES_PRICE</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxSALES_PRICE" TabIndex="6" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelMODEL" runat="server">MODEL</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxMODEL" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelSERIAL_NUMBER" runat="server">SERIAL_NUMBER</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxSERIAL_NUMBER" TabIndex="6" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelCLAIM_NUMBER" runat="server">CLAIM_NUMBER</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxCLAIM_NUMBER" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelGVS_SERVICE_ORDER_NUMBER" runat="server">GVS_SERVICE_ORDER_NUMBER</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxGVS_SERVICE_ORDER_NUMBER" TabIndex="6" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelSERVICE_CENTER_CODE" runat="server">SERVICE_CENTER_CODE</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxSERVICE_CENTER_CODE" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelMETHOD_OF_REPAIR" runat="server">METHOD_OF_REPAIR</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxMETHOD_OF_REPAIR" TabIndex="6" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelDATE_CLAIM_OPENED" runat="server">DATE CLAIM OPENED</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxDATE_CLAIM_OPENED" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelPROBLEM_DESCRIPTION" runat="server">PROBLEM_DESCRIPTION</asp:Label>:
                                                            </td>
                                                            <td style="width: 75%; height: 17px" colspan="2">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxPROBLEM_DESCRIPTION" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="320px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 23%; height: 17px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelTECHNICAL_REPORT" runat="server">TECHNICAL_REPORT</asp:Label>:
                                                            </td>
                                                            <td style="width: 75%; height: 17px" colspan="2">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxTECHNICAL_REPORT" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="320px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 23%; height: 17px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelCLAIM_ACTIVITY" runat="server">CLAIM_ACTIVITY</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxCLAIM_ACTIVITY" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelREPAIR_DATE" runat="server">REPAIR DATE</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxREPAIR_DATE" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelPICKUP_DATE" runat="server">PICKUP_DATE</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxPICKUP_DATE" TabIndex="6" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelSCHEDULED_VISIT_DATE" runat="server">SCHEDULED_VISIT_DATE</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxSCHEDULED_VISIT_DATE" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelEXPECTED_REPAIR_DATE" runat="server">EXPECTED_REPAIR_DATE</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxEXPECTED_REPAIR_DATE" TabIndex="6" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelVISIT_DATE" runat="server">VISIT_DATE</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxVISIT_DATE" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelDEFECT_REASON" runat="server">DEFECT_REASON</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxDEFECT_REASON" TabIndex="6" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelLIABILITY_LIMIT" runat="server">LIABILITY_LIMIT</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxLIABILITY_LIMIT" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelLIABILITY_LIMIT_PERCENT" runat="server">LIABILITY_LIMIT_PERCENT</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxLIABILITY_LIMIT_PERCENT" TabIndex="6" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelDEDUCTIBLE_AMOUNT" runat="server">DEDUCTIBLE_AMOUNT</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxDEDUCTIBLE_AMOUNT" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelDEDUCTIBLE_PERCENT" runat="server">DEDUCTIBLE_PERCENT</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxDEDUCTIBLE_PERCENT" TabIndex="6" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelLABOR_AMT" runat="server">LABOR_AMT</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxLABOR_AMT" TabIndex="2" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="160px"></asp:TextBox>
                                                            </td>
                                                            <td style="width: 1%; height: 17px" valign="middle" nowrap align="right">
                                                                <asp:Label ID="LabelSHIPPING" runat="server">SHIPPING</asp:Label>:
                                                            </td>
                                                            <td style="width: 50%; height: 17px">
                                                                &nbsp;
                                                                <asp:TextBox ID="TextboxSHIPPING" TabIndex="6" runat="server" CssClass="FLATTEXTBOX"
                                                                    Width="220px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <hr style="height: 2px" size="2">
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="bottom" align="left" width="100%" colspan="2">
                                                     <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
                                                            <div id="tabs" class="style-tabs">
                                                                <ul>
                                                                    <li><a href="#tabsStatus">
                                                                        <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">STATUS</asp:Label></a></li>
                                                                    <li><a href="#tabsParts">
                                                                        <asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">PARTS</asp:Label></a></li>
                                                                    <li><a href="#tabsFollowUP">
                                                                        <asp:Label ID="Label8" runat="server" CssClass="tabHeaderText">FOLLOW_UP</asp:Label></a></li>
                                                                </ul>

                                                                <div id="tabsStatus">
                                                                    <asp:Panel ID="moPartsInfoTabPanel_WRITE" runat="server" Width="100%">
                                                                        <table id="tblPartsInfo" style="border-right: #999999 0px solid; border-top: #999999 0px solid; border-left: #999999 0px solid; border-bottom: #999999 0px solid; width: 100%; height: 178px"
                                                                            cellspacing="4" cellpadding="4" rules="cols" background="" border="0">
                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                        <tr>
                                                                                            <td colspan="3">
                                                                                                <asp:GridView ID="StatusGridView" runat="server" Width="100%" OnRowCreated="ItemCreated_StatusGridView"
                                                                                                    AllowPaging="True" AllowSorting="False" CellPadding="1" AutoGenerateColumns="False"
                                                                                                    CssClass="DATAGRID">
                                                                                                    <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                                                                                    <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                                                                                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                                                                                                    <RowStyle CssClass="ROW"></RowStyle>
                                                                                                    <HeaderStyle CssClass="HEADER"></HeaderStyle>
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField Visible="False" HeaderText="Id">
                                                                                                            <ItemTemplate>
                                                                                                                &gt;
                                                                                                        <asp:Label ID="IdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("transaction_log_header_id"))%>'>
                                                                                                        </asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="STATUS">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="StatusLabel" runat="server" Visible="True" Text='<%# Container.DataItem("extended_status_code")%>'>
                                                                                                                </asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="DATE">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="DateLabel" runat="server" Visible="True" Text='<%# Container.DataItem("extended_status_date")%>'>
                                                                                                                </asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="COMMENT">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="COMMENTLabel" runat="server" Visible="True" Text='<%# Container.DataItem("extended_status_comment")%>'>
                                                                                                                </asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                    <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                                                                                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                                                                                </asp:GridView>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </div>

                                                                <div id="tabsParts">
                                                                    <asp:Panel ID="Panel1" runat="server" Width="100%">
                                                                        <table id="Table1" style="border-right: #999999 0px solid; border-top: #999999 0px solid; border-left: #999999 0px solid; border-bottom: #999999 0px solid; width: 100%; height: 178px"
                                                                            cellspacing="4" cellpadding="4" rules="cols" background="" border="0">
                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                        <tr>
                                                                                            <td colspan="3">
                                                                                                <asp:GridView ID="PartsGridView" runat="server" Width="100%" OnRowCreated="ItemCreated_PartsGridView"
                                                                                                    AllowPaging="True" AllowSorting="False" CellPadding="1" AutoGenerateColumns="False"
                                                                                                    CssClass="DATAGRID">
                                                                                                    <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                                                                                    <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                                                                                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                                                                                                    <RowStyle CssClass="ROW"></RowStyle>
                                                                                                    <HeaderStyle CssClass="HEADER"></HeaderStyle>
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField Visible="False" HeaderText="Id">
                                                                                                            <ItemTemplate>
                                                                                                                &gt;
                                                                                                        <asp:Label ID="IdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("transaction_log_header_id"))%>'>
                                                                                                        </asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="PARTS_CODE">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="PARTS_CODELabel" runat="server" Visible="True" Text='<%# Container.DataItem("part_code")%>'>
                                                                                                                </asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="COST">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="PART_COSTLabel" runat="server" Visible="True" Text='<%# Container.DataItem("part_cost")%>'>
                                                                                                                </asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="PART_DEFECT">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="PARTS_DEFECTLabel" runat="server" Visible="True" Text='<%# Container.DataItem("part_defect")%>'>
                                                                                                                </asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="INSTOCK">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="INSTOCKLabel" runat="server" Visible="True" Text='<%# Container.DataItem("in_stock")%>'>
                                                                                                                </asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                    <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                                                                                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                                                                                </asp:GridView>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </div>

                                                                <div id="tabsFollowUP">
                                                                    <asp:Panel ID="Panel2" runat="server" Width="100%">
                                                                        <table id="Table4" style="border-right: #999999 0px solid; border-top: #999999 0px solid; border-left: #999999 0px solid; border-bottom: #999999 0px solid; width: 100%; height: 178px"
                                                                            cellspacing="4" cellpadding="4" rules="cols" background="" border="0">
                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                                        <tr>
                                                                                            <td colspan="3">
                                                                                                <asp:GridView ID="FollowUpGridView" runat="server" Width="100%" OnRowCreated="ItemCreated_FollowUpGridView"
                                                                                                    AllowPaging="True" AllowSorting="False" CellPadding="1" AutoGenerateColumns="False"
                                                                                                    CssClass="DATAGRID">
                                                                                                    <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                                                                                    <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                                                                                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                                                                                                    <RowStyle CssClass="ROW"></RowStyle>
                                                                                                    <HeaderStyle CssClass="HEADER"></HeaderStyle>
                                                                                                    <Columns>
                                                                                                        <asp:TemplateField Visible="False" HeaderText="Id">
                                                                                                            <ItemTemplate>
                                                                                                                &gt;
                                                                                                        <asp:Label ID="IdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("transaction_log_header_id"))%>'>
                                                                                                        </asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="CREATED_DATE">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="CREATED_DATELabel" runat="server" Visible="True" Text='<%# Container.DataItem("comment_created_date")%>'>
                                                                                                                </asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="COMMENT_TYPE">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="COMMENT_TYPELabel" runat="server" Visible="True" Text='<%# Container.DataItem("comment_type_code")%>'>
                                                                                                                </asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="COMMENTS">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="COMMENTSLabel" runat="server" Visible="True" Text='<%# Container.DataItem("comments")%>'>
                                                                                                                </asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                        <asp:TemplateField HeaderText="NAME_OF_CALLER">
                                                                                                            <ItemTemplate>
                                                                                                                <asp:Label ID="NAME_OF_CALLERLabel" runat="server" Visible="True" Text='<%# Container.DataItem("caller_name")%>'>
                                                                                                                </asp:Label>
                                                                                                            </ItemTemplate>
                                                                                                        </asp:TemplateField>
                                                                                                    </Columns>
                                                                                                    <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                                                                                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                                                                                </asp:GridView>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </asp:Panel>
                                                                </div>
                                                            </div>
                                                </td>
                                            </tr>
                                        </asp:Panel>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom" colspan="2" >
                                <hr style="width: 100%; height: 2px;" size="2" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" nowrap align="left" height="20">
                                &nbsp;
                                <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="42" runat="server" Width="90px"
                                    CssClass="FLATBUTTON" Height="20px" Text="Back"></asp:Button>&nbsp;
                                <asp:Button ID="btnEdit_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="43" runat="server" Width="90px"
                                    CssClass="FLATBUTTON" Height="20px" Text="Edit"></asp:Button>&nbsp;
                                <asp:Button ID="btnUndo_Write" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="45" runat="server" Width="90px"
                                    CssClass="FLATBUTTON" Height="20px" Text="Undo"></asp:Button>&nbsp;
                                <asp:Button ID="btnAdd_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); 
                                    CURSOR: hand; BACKGROUND-REPEAT: no-repeat" TabIndex="46" runat="server" Width="150px" 
                                    CssClass="FLATBUTTON" height="20px" Text="ADD_COMUNA_ALIAS"></asp:Button>&nbsp;
                            </td>
                            <td align="right">
                                <asp:Button ID="btnResend_WRITE" runat="server" CssClass="FLATBUTTON" Height="20px"
                                    Style="cursor: hand; background-repeat: no-repeat" TabIndex="5" Text="RESEND"
                                    Width="90px" />
                                &nbsp;<asp:Button ID="btnHide_Write" Style="cursor: hand; background-repeat: no-repeat"
                                    runat="server" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="HIDE">
                                </asp:Button>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td height="5" colspan="2">
                            </td>
                        </tr>
                    </table>
                    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                        runat="server" designtimedragdrop="261" />
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
