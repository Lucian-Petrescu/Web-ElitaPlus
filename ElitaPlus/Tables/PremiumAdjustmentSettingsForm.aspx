<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>


<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PremiumAdjustmentSettingsForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PremiumAdjustmentSettingsForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>TemplateForm</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js">
    </script>

    <script language="javascript">
    function fabDDOnChange(dropdown) {

            //alert('Hello');                         
                       
            var hdFinAdjustByAmt = document.getElementById("hdFinAdjustByAmt");
            var hdFinAdjustByPct = document.getElementById("hdFinAdjustByPct");

            var myindex = dropdown.selectedIndex;
            var SelValue = dropdown.options[myindex].value;
           
            var lblAdjPctID = '<%= LabelAdjustmentPercentage.ClientID %>';
            var lblAdjPct = document.getElementById(lblAdjPctID);
            var txtAdjPctID = '<%= TextBoxAdjustmentPercentage.ClientID %>';
            var txtAdjPct = document.getElementById(txtAdjPctID);

            var lblAdjAmtID = '<%= LabelAdjustmentAmount.ClientID %>';
            var lblAdjAmt = document.getElementById(lblAdjAmtID);
            var txtAdjAmtID = '<%= TextBoxAdjustmentAmount.ClientID %>';
            var txtAdjAmt = document.getElementById(txtAdjAmtID);

            var lblAdjBasedOnID = '<%= LabelAdjustmentBasedOn.ClientID %>';
            var lblAdjBasedOn = document.getElementById(lblAdjBasedOnID);
            var cboAdjBasedOnID = '<%= cboAdjustmentBasedOn.ClientID %>';
            var cboAdjBasedOn = document.getElementById(cboAdjBasedOnID);

            if (SelValue == hdFinAdjustByAmt.value) {

                if (lblAdjPct) { lblAdjPct.style.display = 'none'; }
                if (txtAdjPct) { txtAdjPct.style.display = 'none'; }

                if (lblAdjAmt) { lblAdjAmt.style.display = 'inline'; }
                if (txtAdjAmt) { txtAdjAmt.style.display = 'inline'; }

                if (lblAdjBasedOn) { lblAdjBasedOn.style.display = 'inline'; }
                if (cboAdjBasedOn) { cboAdjBasedOn.style.display = 'inline'; }

            }            
            else if (SelValue == hdFinAdjustByPct.value) {
                if (lblAdjPct) { lblAdjPct.style.display = 'inline'; }
                if (txtAdjPct) { txtAdjPct.style.display = 'inline'; }

                if (lblAdjAmt) { lblAdjAmt.style.display = 'none'; }
                if (txtAdjAmt) { txtAdjAmt.style.display = 'none'; }

                if (lblAdjBasedOn) { lblAdjBasedOn.style.display = 'none'; }
                if (cboAdjBasedOn) { cboAdjBasedOn.style.display = 'none'; }
            }
            else {

                if (lblAdjPct) { lblAdjPct.style.display = 'none'; }
                if (txtAdjPct) { txtAdjPct.style.display = 'none'; }

                if (lblAdjAmt) { lblAdjAmt.style.display = 'none'; }
                if (txtAdjAmt) { txtAdjAmt.style.display = 'none'; }

                if (lblAdjBasedOn) { lblAdjBasedOn.style.display = 'none'; }
                if (cboAdjBasedOn) { cboAdjBasedOn.style.display = 'none'; }

            }
            return true;
        }
    </script>
</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <!--Start Header-->
    <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
        border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
        cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td valign="top">
                <table width="100%" border="0">
                    <tr>
                        <td height="20">
                            <p>
                                &nbsp;
                                <asp:Label ID="LabelTables" runat="server" Cssclass="TITLELABEL">Tables</asp:Label>:
                                <asp:Label ID="Label40" runat="server" Cssclass="TITLELABELTEXT">Premium_Adjustment_Settings</asp:Label></p>
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
    <table id="tblOuter2" style="border: 1px solid black; margin: 5px; height: 601px;"
        cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" 
        border="0">
        <!--d5d6e4-->
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <asp:Panel ID="WorkingPanel" runat="server" Height="483px">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; width: 98%; border-bottom: #999999 1px solid;
                        height: 298px" cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center"
                        bgcolor="#fef9ea" border="0">
                        <tr>
                            <td>
                                <asp:Panel ID="EditPanel_WRITE" runat="server">
                                    <table id="Table1" cellspacing="1" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td style="height: 1px" valign="middle" align="center" colspan="2">
                                                <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap align="left" width="1%" colspan="6">
                                                <uc1:MultipleColumnDDLabelControl ID="multipleDropControl"  AutoPostBackDD="false" runat="server"></uc1:MultipleColumnDDLabelControl>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap align="left" width="1%" colspan="6">
                                                <hr style="height: 1px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap align="right" width="1%">
                                                *
                                                <asp:Label ID="LabeFinancialAdjustmentBy" runat="server" Font-Bold="false">Financial_Adjustment_By</asp:Label>:&nbsp;&nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboFinancialAdjustmentBy"  TabIndex="35" runat="server" Width="240px">
                                                </asp:DropDownList>
                                            </td>
                                            <td  nowrap style="text-align: right;  vertical-align: middle">
                                                <asp:Label ID="LabelAdjustmentPercentage"  Font-Bold="false" runat="server">Adjustment_Percentage:</asp:Label>
                                            </td>                  
                                            <td align="left">&nbsp;            
                                                <asp:TextBox ID="TextBoxAdjustmentPercentage" TabIndex="1" Width="180px" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
                                            </td>
                                            <td  nowrap style="text-align: right; vertical-align: middle">
                                                <asp:Label ID="LabelAdjustmentAmount"  Font-Bold="false" runat="server">Adjustment_Amount:</asp:Label>
                                            </td>                  
                                            <td align="left">&nbsp;            
                                                <asp:TextBox ID="TextBoxAdjustmentAmount" TabIndex="1" Width="180px" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td nowrap align="right" width="1%">
                                               
                                                <asp:Label ID="LabelAdjustmentBasedOn"  runat="server" Font-Bold="false">Adjustment_Based_On:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboAdjustmentBasedOn"  TabIndex="35" runat="server" Width="240px">
                                                </asp:DropDownList>
                                            </td>
                                            
                                        </tr>                                        
                                        <tr>
                                            <td align="right">                                              
                                                <asp:Label ID="LabelEffectiveDate" runat="server" Font-Bold="false">Effective_Date</asp:Label>:&nbsp;&nbsp;
                                            </td>
                                            <td align="left">
                                                 <asp:TextBox ID="TextBoxEffectiveDate" runat="server" onFocus="setHighlighter(this)"
                                                                onMouseover="setHighlighter(this)" Visible="True" ></asp:TextBox>
                                                 <asp:ImageButton ID="moEffectiveDateImage" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                                            </td>
                                            <td>
                                            </td>
                                            <td></td>
                                             <td>
                                            </td>
                                            <td></td>                                            
                                        </tr>    
                                        <tr>
                                        <td align="right">                                              
                                                <asp:Label ID="LabelExpirationDate" runat="server" Font-Bold="false">Expiration_Date</asp:Label>:&nbsp;&nbsp;
                                            </td>
                                            <td align="left">
                                                 <asp:TextBox ID="TextBoxExpirationDate" runat="server" onFocus="setHighlighter(this)"
                                                                onMouseover="setHighlighter(this)" Visible="True" ></asp:TextBox>
                                                 <asp:ImageButton ID="moExpirationDateImage" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                                            </td>
                                            <td>
                                            </td>
                                            <td></td>
                                             <td>
                                            </td>
                                            <td></td>
                                         </tr>                                    
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom" align="left">
                                <table cellspacing="6" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td width="100%" colspan="7" height="1">
                                            <hr style="height: 1px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                                cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
                                                Width="90px" CssClass="FLATBUTTON" CausesValidation="False" Text="Back" Height="20px">
                                            </asp:Button>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                                cursor: hand; background-repeat: no-repeat" TabIndex="190" runat="server" Font-Bold="false"
                                                Width="90px" CssClass="FLATBUTTON" Text="Save" Height="20px" CommandName="WRITE">
                                            </asp:Button>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnUndo_WRITE" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                                cursor: hand; background-repeat: no-repeat" TabIndex="195" runat="server" Font-Bold="false"
                                                Width="90px" CssClass="FLATBUTTON" CausesValidation="False" Text="Undo" Height="20px">
                                            </asp:Button>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                cursor: hand; background-repeat: no-repeat" TabIndex="200" runat="server" Font-Bold="false"
                                                Width="90px" CssClass="FLATBUTTON" Text="New" Height="20px" CommandName="WRITE">
                                            </asp:Button>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                cursor: hand; background-repeat: no-repeat" TabIndex="205" runat="server" Width="140px"
                                                CssClass="FLATBUTTON" Text="NEW_WITH_COPY" CommandName="WRITE" Height="20px">
                                            </asp:Button>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                                cursor: hand; background-repeat: no-repeat" TabIndex="210" runat="server" Font-Bold="false"
                                                Width="100px" CssClass="FLATBUTTON" CausesValidation="False" Text="Delete" Height="20px"
                                                CommandName="WRITE"></asp:Button>
                                        </td>
                                        <td width="100%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                        runat="server" designtimedragdrop="261">
                        <input id="hdFinAdjustByPct" type="hidden" name="hdFinAdjustByPct" runat="server" />
                        <input id="hdFinAdjustByAmt" type="hidden" name="hdFinAdjustByAmt" runat="server" /></asp:Panel>
            </td>
            <%--</TD>--%>
        </tr>
    </table>
    </form>
</body>
</html>
