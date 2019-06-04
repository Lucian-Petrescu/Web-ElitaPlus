<%@ Register TagPrefix="uc1" TagName="FileProcessedController" Src="FileProcessedController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EquipmentFormWrk.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.EquipmentFormWrk" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>EquipmentFile</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
    <SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server" submitdisabledcontrols="True">
    <!--Start Header-->
    <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
        border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
        cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td valign="top">
                <table width="100%" border="0">
                    <tr>
                        <td height="20">
                            &nbsp;<asp:Label ID="moTitleLabel" runat="server" CssClass="TITLELABEL">INTERFACES</asp:Label>:
                            <asp:Label ID="moTitleLabel2" runat="server" CssClass="TITLELABELTEXT">EQUIPMENT_FILE</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="moTablelOuter" style="border-right: black 1px solid; border-top: black 1px solid;
        margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
        cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
        <!--d5d6e4-->
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" height="100%">
                <asp:Panel ID="moPanel" runat="server" Height="100px" Width="98%">
                    <table id="moTablelMain" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="0"
                        cellpadding="6" rules="cols" width="100%" align="right" bgcolor="#fef9ea" border="0">
                        <tr>
                            <td height="1">
                                <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <uc1:FileProcessedController ID="moFileController" runat="server" 
                                    ShowComapnyGroup="true" ShowReference="false" ShowDealer="false" 
                                    ShowComapny="false" ReferenceCaption="SELECT_COMPANY_GROUP"
                                    FileType="Equipment" FileNameCaption="EQUIPMENT_FILE"
                                    RejectReportFileName="RejectedEquipment-Exp_EN"
                                    ProcessedReportFileName="ProcessedEquipment-Exp_EN"></uc1:FileProcessedController>
                                 
                            </td>                           
                        </tr>
                        </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>
    <script language="JavaScript">
        function SetDualDropDownsValue(ctlCodeDropDown, ctlDecDropDown, change_Dec_Or_Code) {
            var objCodeDropDown = document.getElementById(ctlCodeDropDown); // "By Code" DropDown control
            var objDecDropDown = document.getElementById(ctlDecDropDown);   // "By Description" DropDown control 

            //Select Code or Dec drop down
            if (change_Dec_Or_Code == 'C') {
                objCodeDropDown.value = objDecDropDown.options[objDecDropDown.selectedIndex].value;
            }
            else {
                objDecDropDown.value = objCodeDropDown.options[objCodeDropDown.selectedIndex].value;
            }
        }
    </script>
</body>
</html>
