<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PostalCodeFormatForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PostalCodeFormatForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>TemplateForm</title>
    <!-- ************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebForm.cst (9/23/2004)  ******************** -->
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

</head>
<body leftmargin="0" topmargin="0" onresize="resizeForm(document.getElementById('scroller'));"
    onload="changeScrollbarColor();" border="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <input id="isPostalCodeVisible" type="hidden" name="isPostalCodeVisible" runat="server">
    <input id="TextboxSampleFormat" type="hidden" name="TextboxSampleFormat" runat="server">
    <!--Start Header-->
    <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
        border-left: black 1px solid; border-bottom: black 1px solid; height: 39px" cellspacing="0"
        cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td valign="top">
                <table width="100%" border="0">
                    <tr>
                        <td height="20">
                            <p>
                                &nbsp;
                                <asp:Label ID="LabelTables" runat="server" Cssclass="TITLELABEL">Admin</asp:Label>:
                                <asp:Label ID="Label40" runat="server" Cssclass="TITLELABELTEXT">PostalCodeFormat</asp:Label></p>
                        </td>
                        <td align="right" height="20">
                            <strong>*</strong>
                            <asp:Label ID="Label9" runat="server" Font-Bold="false">INDICATES_REQUIRED_FIELDS</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
        margin: 5px; border-left: black 1px solid; width: 98%; border-bottom: black 1px solid;
        height: 98%" cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4"
        border="0">
        <!--d5d6e4-->
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <asp:Panel ID="WorkingPanel" runat="server" Height="50%" Width="98%">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; width: 98%; border-bottom: #999999 1px solid;
                        height: 100%" cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center"
                        bgcolor="#fef9ea" border="0">
                        <tr>
                            <td style="height: 4px" valign="middle" align="center" colspan="4">
                                <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 98%">
                                <asp:Panel ID="EditPanel_WRITE" runat="server" Width="98%" Height="50%">
                                    <table id="Table1" style="width: 98%; height: 50%" cellspacing="1" cellpadding="0"
                                        border="0">
                                        <tr>
                                            <td style="width: 50%" valign="middle" align="right">
                                                <asp:Label ID="LabelDescription" runat="server" Font-Bold="false">Description</asp:Label>
                                            </td>
                                            <td style="width: 50%" valign="middle" align="left">
                                                &nbsp;
                                                <asp:TextBox ID="TextboxDescription" TabIndex="10" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%" valign="middle" align="right">
                                                <asp:Label ID="LabelFormat" runat="server" Font-Bold="false">Format</asp:Label>
                                            </td>
                                            <td style="width: 50%" valign="middle" align="left">
                                                &nbsp;
                                                <asp:TextBox ID="TextboxFormat" TabIndex="5" runat="server" Width="45%" CssClass="FLATTEXTBOX" />
                                                &nbsp;<asp:Button ID="btnFormatPostalCode" TabIndex="11" runat="server" Text="..."></asp:Button>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 50%" valign="middle">
                                                <asp:Label ID="LabelFormatManual" runat="server" Font-Bold="False">Format_Manual</asp:Label>
                                            </td>
                                            <td align="left" style="width: 50%" valign="middle">
                                                &nbsp;
                                                <asp:TextBox ID="TextboxFormatManual" runat="server" CssClass="FLATTEXTBOX" 
                                                    onclick="clearTextBox('TextboxFormat')" TabIndex="5" Width="50%" />
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%; height: 32px" valign="middle" align="right">
                                                <asp:Label ID="LabelLocatorStartPosition" runat="server" Font-Bold="false">LocatorStartPosition</asp:Label>
                                            </td>
                                            <td style="width: 50%; height: 32px" valign="middle" align="left">
                                                &nbsp;
                                                <asp:TextBox ID="TextboxLocatorStartPosition" TabIndex="12" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 50%" valign="middle" align="right">
                                                <asp:Label ID="LabelLocatorLength" runat="server" Font-Bold="false">LocatorLength</asp:Label>
                                            </td>
                                            <td style="width: 50%" valign="middle" align="left">
                                                &nbsp;
                                                <asp:TextBox ID="TextboxLocatorLength" TabIndex="13" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td style="width: 50%" valign="middle" align="left">
                                                &nbsp;
                                                <asp:CheckBox ID="CheckBoxReformatFileInputFlag" TabIndex="14" runat="server" Text="ReformatFileInputFlag">
                                                </asp:CheckBox>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td style="width: 50%" valign="middle" align="left">
                                                &nbsp;
                                                <asp:CheckBox ID="CheckBoxComunaEnabled" TabIndex="14" runat="server" Text="ComunaEnabled">
                                                </asp:CheckBox>
                                            </td>                                                
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 16px" align="left" colspan="4">
                                <hr style="width: 100%; height: 1px" size="1">
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom" nowrap align="left" height="20">
                                <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Text="Back" Height="20px"></asp:Button>&nbsp;
                                <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="190" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Text="Save" Height="20px"></asp:Button>&nbsp;
                                <asp:Button ID="btnUndo_Write" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="195" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Text="Undo" Height="20px"></asp:Button>&nbsp;
                                <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="200" runat="server" Font-Bold="false"
                                    Width="81px" CssClass="FLATBUTTON" Text="New" Height="20px"></asp:Button>&nbsp;&nbsp;
                                <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="205" runat="server" Width="136px"
                                    Height="20px" CssClass="FLATBUTTON" Text="NEW_WITH_COPY" CausesValidation="False">
                                </asp:Button>
                                <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="210" runat="server" Font-Bold="false"
                                    Width="100px" CssClass="FLATBUTTON" Text="Delete" Height="20px"></asp:Button>
                            </td>
                        </tr>
                    </table>
                    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                        runat="server" designtimedragdrop="261">
                </asp:Panel>
                <iframe id="iframe1" style="left: 15px; visibility: hidden; overflow: hidden; width: 725px;
                    padding-top: 0px; position: absolute; top: 70px; height: 470px; 
                    name="iframe1" marginwidth="0" src="" scrolling="no"></iframe>
            </td>
        </tr>
    </table>
    </form>

    <script>
		//debugger;
			if (document.all("isPostalCodeVisible").value != "True")
			{
				document.all.item('iframe1').style.display = 'none';
				document.all.item('iframe1').style.visibility = 'hidden';
				document.all.item('iframe1').src = "";
				document.all("TextboxFormat").readOnly = "True";
			}
			else
			{
				document.all.item('iframe1').style.display = '';
				document.all.item('iframe1').style.visibility = 'visible';
				document.all.item('iframe1').src = "PostalCodeRegExFormatForm.aspx";
				document.all("TextboxFormat").readOnly = "False";
			}
			
			function LoadPostalCodeFormat()
			{
				//debugger;
				var url = "PostalCodeRegExFormatForm.aspx;"
				var frame = document.all.item('iframe1');
				frame.src = url;
				frame.style.display='';
				frame.visibility = 'visible';
				document.all("isPostalCodeVisible").value = "True";
			}
			
			function SavePostalCodeFormat(postalCodeFormat, sampleFormat)
			{
				document.all("TextboxFormat").value = postalCodeFormat;
				document.all("TextboxSampleFormat").value = sampleFormat;
				document.getElementById('TextboxFormatManual').value = '';
				ClosePostalCodeFormat();
			}

			function ClosePostalCodeFormat()
			{
				var frame = document.all.item('iframe1');
				frame.src='';
				frame.style.display='none';
				frame.style.visibility = 'hidden';
				document.all("isPostalCodeVisible").value = "False";
			}

			function clearTextBox(ctrl) {
			    document.getElementById(ctrl).value = '';
			}
    </script>

</body>
</html>
