<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SplitPremiumFileForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.SplitPremiumFileForm" %>

<%@ Register TagPrefix="uc1" TagName="InterfaceProgressControl" Src="InterfaceProgressControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>SplitPremiumFile</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

    <script language="JavaScript" src="../Navigation/Scripts/ReportCeScripts.js"></script>

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
                                &nbsp;<asp:Label ID="moTitleLabel" runat="server" CssClass="TITLELABEL">INTERFACES</asp:Label>:
                                <asp:Label ID="moTitleLabel2" runat="server"  CssClass="TITLELABELTEXT">SPLIT_PREMIUM_FILE</asp:Label></td>
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
                    &nbsp;</td>
            </tr>
            <tr>
                <td valign="top" align="center" height="100%">
                    <asp:Panel ID="moPanel" runat="server" Width="98%" >
                        <table id="moTablelMain" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                            border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                            cellspacing="0" cellpadding="6" rules="cols" width="100%" align="center" bgcolor="#fef9ea"
                            border="0">
                            <tr>
                                <td height="1">
                                    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table id="TABLE4" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                        border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="0"
                                        cellpadding="6" rules="cols" width="100%" align="center" bgcolor="#fef9ea" border="0">
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td style="height: 70px">
                                                            <table id="moTableSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                                border-left: #999999 1px solid; width: 692px; border-bottom: #999999 1px solid;
                                                                height: 30px" cellspacing="0" cellpadding="0" rules="cols" width="98%" align="center"
                                                                bgcolor="#fef9ea" border="0">
                                                                <tr>
                                                                    <td style="width: 539px">
                                                                        <table id="Table2" style="width: 680px; height: 30px" cellspacing="0" cellpadding="0"
                                                                            width="680" border="0">
                                                                            <tr>
                                                                                <td style="width: 170px; height: 30px" nowrap align="right">
                                                                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                                                                    <asp:Label ID="moDealerLabel" runat="server" Width="137px">SPLIT_INTERFACE</asp:Label>:</td>
                                                                                <td style="height: 30px" nowrap align="left">
                                                                                    <asp:DropDownList ID="ddSplit" runat="server" Width="242px" AutoPostBack="True">
                                                                                    </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                    <asp:Button ID="moBtnClear" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                                                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                                        Width="90px" Visible="False" CssClass="FLATBUTTON" Height="20px" Text="Clear"
                                                                                        CausesValidation="False"></asp:Button>&nbsp;&nbsp;&nbsp;
                                                                                    <asp:Button ID="moBtnSearch" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                                                                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                                        Width="90px" Visible="False" CssClass="FLATBUTTON" Height="20px" Text="Search"
                                                                                        CausesValidation="False" Enabled="False"></asp:Button></td>
                                                                                <td style="height: 30px" nowrap align="left">
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center"  height="250">
                                                            <table id="Table1" style="width: 488px;height: 98%">
                                                                <tr>
                                                                    <td valign="top" align="center">
                                                                        <asp:DataGrid ID="dgSplitFiles" runat="server" Width="344px" OnItemCreated="ItemCreated"
                                                                            AutoGenerateColumns="False" BorderStyle="Solid" BorderWidth="1px" BackColor="#DEE3E7"
                                                                            BorderColor="#999999" CellPadding="1" AllowPaging="True" AllowSorting="True"
                                                                            OnItemCommand="ItemCommand">
                                                                            <SelectedItemStyle Wrap="False" BackColor="Magenta"></SelectedItemStyle>
                                                                            <EditItemStyle Wrap="False"></EditItemStyle>
                                                                            <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                                            <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                                            <HeaderStyle  HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                                                                            <Columns>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton Style="cursor: hand;" ID="btnSelect" CommandName="SelectRecord"
                                                                                            ImageUrl="../Navigation/images/icons/yes_icon.gif" runat="server" CausesValidation="false">
                                                                                        </asp:ImageButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:BoundColumn Visible="False" HeaderText="claimfile_processed_id"></asp:BoundColumn>
                                                                                <asp:BoundColumn DataField="FILENAME" HeaderText="FILENAME">
                                                                                    <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                </asp:BoundColumn>
                                                                                <asp:BoundColumn DataField="RECEIVED" HeaderText="RECEIVED">
                                                                                    <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="1%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                </asp:BoundColumn>
                                                                                <asp:BoundColumn DataField="COUNTED" HeaderText="COUNTED">
                                                                                    <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="1%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                </asp:BoundColumn>
                                                                                <asp:BoundColumn DataField="SPLIT" HeaderText="SPLIT">
                                                                                    <HeaderStyle Wrap="False" HorizontalAlign="Center" Width="1%"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                </asp:BoundColumn>
                                                                            </Columns>
                                                                            <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                                                Mode="NumericPages"></PagerStyle>
                                                                        </asp:DataGrid></td>
                                                                    <td>                                                                                                                                              <div id="scroller" style="overflow: auto; height: 100%" align="center" medium black border:solid>
                                                                            <asp:DataGrid ID="dgSplitFileRecords" runat="server" Width="100%" OnItemCreated="ItemCreated"
                                                                                AutoGenerateColumns="False" BorderStyle="Solid" BorderWidth="1px" BackColor="#DEE3E7"
                                                                                BorderColor="#999999" CellPadding="1" AllowSorting="True" OnItemCommand="ItemCommand">
                                                                                <SelectedItemStyle Wrap="False" BackColor="Magenta"></SelectedItemStyle>
                                                                                <EditItemStyle Wrap="False"></EditItemStyle>
                                                                                <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                                                <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                                                <HeaderStyle  HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                                                                                <Columns>
                                                                                    <asp:BoundColumn DataField="FILENAME" HeaderText="FILENAME">
                                                                                        <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                                    </asp:BoundColumn>
                                                                                    <asp:BoundColumn DataField="RECORDS" HeaderText="RECORDS">
                                                                                        <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                                                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                                    </asp:BoundColumn>
                                                                                </Columns>
                                                                                <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                                                    Mode="NumericPages"></PagerStyle>
                                                                            </asp:DataGrid></div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center">
                                                <hr style="width: 100%; height: 1px" size="1">
                                                &nbsp;</td>
                                        </tr>                                       
                                        <tr>
                                            <td align="center">
                                                <asp:Panel ID="Panel1" runat="server" Width="512px">
                                                    <asp:Button ID="BtnSplit_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                        Width="121px" CssClass="FLATBUTTON" Height="20px" Text="SPLIT" CausesValidation="False"
                                                        Enabled="False"></asp:Button>&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="BtnDeleteOriginalFile_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                        Width="185px" CssClass="FLATBUTTON" Height="20px" Text="DELETE_ORIGINAL_FILE"
                                                        CausesValidation="False" Enabled="False"></asp:Button>&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="BtnDownLoadFiles" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                        Width="160px" CssClass="FLATBUTTON" Height="20px" Text="DOWNLOAD_FILES" CausesValidation="False"
                                                        Enabled="False"></asp:Button></asp:Panel>
                                            </td>
                                        </tr>
                                                                         
                                        <tr>
                                            <td>
                                                <asp:Panel ID="moUpLoadPanel" runat="server" Visible="true">
                                                    <table id="moTableSearch2" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                        border-left: #999999 1px solid; width: 693px; border-bottom: #999999 1px solid;
                                                        height: 67px" cellspacing="0" cellpadding="0" rules="cols" width="693" align="center"
                                                        bgcolor="#fef9ea" border="0">
                                                        <tr>
                                                            <td style="width: 539px">
                                                                <table id="Table3" style="width: 680px" cellspacing="0" cellpadding="0" width="680"
                                                                    border="0">
                                                                    <tr>
                                                                        <td style="width: 159px; height: 22px" nowrap align="left">
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;
                                                                        </td>
                                                                        <td style="height: 10px" nowrap align="center">
                                                                            &nbsp;&nbsp;&nbsp;
                                                                        </td>
                                                                        <td>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 159px; height: 22px" nowrap align="right">
                                                                            *
                                                                            <asp:Label ID="moFilenameLabel" runat="server">Filename</asp:Label>:</td>
                                                                        <td style="height: 10px" nowrap align="left">
                                                                            <input id="claimFileInput" style="width: 269px; height: 19px" type="file" size="25"
                                                                                name="claimFileInput" runat="server"></td>
                                                                        <td>
                                                                            <asp:Button ID="btnCopyDealerFile_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                                                cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                                Width="236px" CssClass="FLATBUTTON" Height="20px" Text="COPY_SPLIT_PREMIUM_INTERFACE_FILE">
                                                                            </asp:Button></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 159px; height: 22px" nowrap align="right">
                                                                            <asp:Label ID="moExpectedFileLabel" runat="server">Expected_File</asp:Label>:</td>
                                                                        <td style="height: 10px" nowrap align="left">
                                                                            &nbsp;
                                                                            <asp:Label ID="moExpectedFileLabel_NO_TRANSLATE" runat="server"></asp:Label></td>
                                                                        <td></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                  </asp:Panel>  
                                            </td>
                                        </tr>
                                    </table>
                                     <uc1:InterfaceProgressControl ID="moInterfaceProgressControl" runat="server"></uc1:InterfaceProgressControl>
        <asp:Button ID="btnAfterProgressBar" Style="background-color: #fef9ea" runat="server"
            Height="0" Width="0"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
       </form>

    <script> 
   // var otree = document.getElementById("moClaimController_dgTotalRowByFileName");
   // otree.style.width = "50px";
   // otree.style.height = "50px";
   //SetScrollAreaCtrTreeV("moClaimController_dgTotalRowByFileName","100%","100%");
    </script>

</body>
</html>
