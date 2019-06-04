<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CompanyCreditCardScheduleForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.CompanyCreditCardScheduleForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>ProductConversionForm</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

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
                            &nbsp;<asp:Label ID="LabelTables" CssClass="TITLELABEL" runat="server">Tables</asp:Label>:
                            <asp:Label ID="Label40" runat="server" CssClass="TITLELABELTEXT">CREDIT_CARD_BILLING_SCHEDULE</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
        margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid; height: 524px"
        height="524" cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4"
        border="0">
        <!--d5d6e4-->
        <tr>
            <td height="5">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" width="100%" height="100%">
                <asp:Panel ID="WorkingPanel" runat="server" Width="100%" Height="568px">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; border-bottom: #999999 1px solid; height: 528px"
                        cellspacing="0" cellpadding="6" rules="cols" width="97%" align="center" bgcolor="#fef9ea"
                        border="0">
                        <tr>
                            <td height="1">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px" align="center" width="98%" colspan="2">
                                <uc1:ErrorController ID="ErrController" runat="server"></uc1:ErrorController>
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px" align="left" width="100%" colspan="3" >
                             <table>
                                <tr>                                   
                                    <td> 
                                        &nbsp;&nbsp;&nbsp;&nbsp;                                       
                                        <asp:Label ID="moByCompanyLabel" runat="server" Visible="True">SELECT_COMPANY</asp:Label>
                                        <asp:Label ID="lblColon" runat="server" Visible="True">:</asp:Label>
                                    </td>                                   
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:DropDownList ID="moCompanyDropDownList" runat="server" Visible="True" Width="200px" enableviewstate ="true"  AutoPostBack="True" >
                                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp; 
                                    </td>
                                     <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="moYearLabel" runat="server" Visible="True">BILLING_SCHEDULE_YEAR</asp:Label>:
                                        <asp:DropDownList ID="moYearDropDownList" runat="server" Width="100px" AutoPostBack="True">
                                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <input id="HiddenSavePagePromptResponse" style="width: 8px; height: 18px" type="hidden" size="1" runat="server" />
                                    </td> 
                                </tr>
                              </table>
                            </td>                           
                        </tr>
                        <tr>
                            <td style="height: 404px" align="center">
                                <table id = "tblContainer" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                    border-bottom: black 1px solid; height: 416px" cellspacing="1" cellpadding="1"
                                    width="98%" bgcolor="#d5d6e4" border="0" runat ="server">
                                    <tr>
                                        <td style="width: 29px; height: 19px" height="19">
                                        </td>
                                    </tr>
                                    <tr id="trPageSize" runat="server">
                                        <td style="width: 29px; height: 15px" valign="top" align="left">
                                            &nbsp;                                            
                                                <input id="HiddenIsPageDirty" style="width: 8px; height: 18px"
                                                    type="hidden" size="1" runat="server" /> 
                                        </td>
                                        <td style="height: 17px" align="left">
                                            <asp:Label ID="lblRecordCount" runat="server" align="left"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 98%" colspan="3">
                                            <div id="scroller" style="overflow: auto; width: 100%; height: 100%" align="justify">
                                                <table id="Table1" cellspacing="0" cellpadding="0" border="0">
                                                    <tr>
                                                        <td nowrap>
                                                            <asp:DataGrid ID="moDataGrid" runat="server" Width="100%" PageSize="12" AutoGenerateColumns="False"
                                                                BorderWidth="1px" AllowSorting="True" CellPadding="1" BackColor="#DEE3E7" OnItemCreated="ItemCreated"
                                                                OnItemCommand="ItemCommand" BorderColor="#999999" BorderStyle="Solid">
                                                                <SelectedItemStyle Wrap="False" BackColor="Transparent"></SelectedItemStyle>
                                                                <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
                                                                <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                                <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                                <HeaderStyle Wrap="False" ForeColor="#003399"></HeaderStyle>
                                                                <Columns>
                                                                    <asp:TemplateColumn Visible="False">
                                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/trash.gif"
                                                                                runat="server" CommandName="DeleteRecord"></asp:ImageButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn Visible="False">
                                                                        <ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
                                                                    </asp:TemplateColumn>
                                                                    <asp:TemplateColumn SortExpression="BILLING_DATE" HeaderText="BILLING_DATE">
                                                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                        <ItemStyle Wrap="False" HorizontalAlign="Right" Width="30%" BackColor="White"></ItemStyle>
                                                                        <ItemTemplate>
                                                                            <asp:TextBox ID="moDateCompTextGrid" runat="server" Width="90px" onFocus="setHighlighter(this)"
                                                                                onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                            <asp:ImageButton ID="moDateCompImageGrid" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                                            </asp:ImageButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateColumn>
                                                                    <asp:BoundColumn Visible="False"></asp:BoundColumn>
                                                                </Columns>
                                                                <PagerStyle HorizontalAlign="Left" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                                    PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                                                            </asp:DataGrid>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap align="left" colspan="2" height="28">
                                            <asp:Button ID="SaveButton_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                                cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                Width="100px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>
                                            <asp:Button ID="btnUndo_WRITE" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                                cursor: hand; background-repeat: no-repeat" TabIndex="195" runat="server" Font-Bold="false"
                                                Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Undo" CausesValidation="False">
                                            </asp:Button>
                                            <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                cursor: hand; background-repeat: no-repeat" TabIndex="200" runat="server" Font-Bold="false"
                                                Width="81px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>
                                        </td>
                                        <td nowrap align="right" height="28">
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom" align="left" height="50">
                                <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Back"></asp:Button>&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>

    <script>

        function setDirty() {
            document.getElementById("HiddenIsPageDirty").value = "YES"
        }

        function resizeForm(item) {
            var browseWidth, browseHeight;

            if (document.layers) {
                browseWidth = window.outerWidth;
                browseHeight = window.outerHeight;
            }
            if (document.all) {
                browseWidth = document.body.clientWidth;
                browseHeight = document.body.clientHeight;
            }

            if (screen.width == "800" && screen.height == "600") {
                newHeight = browseHeight - 280;
            }
            else {
                newHeight = browseHeight - 260;
            }

            item.style.height = String(newHeight) + "px";

            item.style.width = String(browseWidth - 50) + "px";

        }

        //resizeForm(document.getElementById("scroller"));
    </script>

</body>
</html>