<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InstallmentFactorForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.InstallmentFactorForm" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>TemplateForm</title>
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
<body onresize="resizeForm(document.getElementById('scroller'));" leftmargin="0"
    topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->
        <table class="TABLETITLE" cellspacing="0" cellpadding="0" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <asp:Label ID="TablesLabel" runat="server" CssClass="TITLELABEL">Tables</asp:Label>:
                                <asp:Label ID="LabelTables" runat="server" CssClass="TITLELABELTEXT">INSTALLMENT_FACTOR</asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="moTableOuter" style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            height="93%"
            cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td style="height: 8px" valign="top" align="center"></td>
            </tr>
            <tr>
                <td valign="top" align="center" height="100%">
                    <table id="moTableMain" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                        height="98%"
                        cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea"
                        border="0">
                        <tr>
                            <td valign="top" align="center" height="1">
                                <uc1:ErrorController ID="ErrController" runat="server"></uc1:ErrorController>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Panel ID="EditPanel" runat="server">
                                    <table style="width: 100%" height="20%" cellspacing="2" cellpadding="0" width="100%"
                                        border="0">
                                        <asp:Panel ID="moDepreciationScheduleEditPanel_WRITE" runat="server" Width="100%"
                                            Height="98%">
                                            <tbody>
                                                <tr>
                                                    <td nowrap colspan="4" align="center">
                                                        <div style="width: 74%; height: 39px">
                                                            <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 25%; height: 13px" align="right">*
                                                        <asp:Label ID="LabelEffective" runat="server">EFFECTIVE</asp:Label>:</td>
                                                    <td style="width: 25%; height: 13px">&nbsp;
	    <asp:TextBox ID="TextboxEffective" TabIndex="3" runat="server" CssClass="FLATTEXTBOX"
            Width="150px"></asp:TextBox>
                                                        <asp:ImageButton ID="ImageButtonStartDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton></td>
                                                    <td style="width: 10%" align="right">*
                                                        <asp:Label ID="LabelExpiration" runat="server">EXPIRATION</asp:Label>:</td>
                                                    <td style="width: 25%">&nbsp;
	    <asp:TextBox ID="TextboxExpiration" TabIndex="7" runat="server" CssClass="FLATTEXTBOX"
            Width="150px"></asp:TextBox>
                                                        <asp:ImageButton ID="ImageButtonEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton></td>


                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <hr>
                                                    </td>
                                                </tr>
                                        </asp:Panel>
                                        <tr>
                                            <td align="left" width="100%" colspan="5">
                                               
                                                <%--     changes for tab--%>
                                                <div id="tabs" class="style-tabs">
                                                    <ul>
                                                        <li><a href="#tabsInstallmentFactor" rel="noopener noreferrer">
                                                            <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">INSTALLMENT_FACTOR</asp:Label></a></li>
                                                    </ul>

                                                    <div id="tabsInstallmentFactor">
                                                        <table id="tblOpportunities" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                                                            cellspacing="2"
                                                            cellpadding="2" rules="cols" width="100%" background="" border="0">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <uc1:ErrorController ID="ErrorControllerDS" runat="server"></uc1:ErrorController>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="middle" colspan="2">
                                                                    <div id="scroller" style="width: 98%" align="center">
                                                                        <asp:DataGrid ID="moDataGrid" runat="server" Width="97%" OnItemCommand="ItemCommand"
                                                                            AutoGenerateColumns="False" BorderStyle="Solid" BorderWidth="1px" BackColor="#DEE3E7"
                                                                            BorderColor="#999999" CellPadding="1" AllowPaging="True" AllowSorting="True"
                                                                            PageSize="15" OnItemCreated="ItemCreated">
                                                                            <SelectedItemStyle Wrap="False" BackColor="Magenta"></SelectedItemStyle>
                                                                            <EditItemStyle Wrap="False"></EditItemStyle>
                                                                            <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                                            <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                            <Columns>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"
                                                                                            CommandName="EditRecord"></asp:ImageButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn>
                                                                                    <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/trash.gif"
                                                                                            runat="server" CommandName="DeleteRecord"></asp:ImageButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn Visible="False">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="moInstallmentFactor_ID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("INSTALLMENT_FACTOR_ID"))%>'
                                                                                            runat="server">
                                                                                        </asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn Visible="True" HeaderText="LOW_PAYMENTS">
                                                                                    <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="False"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="moLowPaymentLabel" Text='<%# Container.DataItem("LOW_NUMBER_OF_PAYMENTS")%>' runat="server">
                                                                                        </asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="moLowPaymentText" runat="server" Visible="true" Width="75"></asp:TextBox>
                                                                                    </EditItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn Visible="True" HeaderText="HIGH_PAYMENT">
                                                                                    <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="False"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="moHighPaymentLabel" Text='<%# Container.DataItem("HIGH_NUMBER_OF_PAYMENTS")%>' runat="server">
                                                                                        </asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="moHighPaymentText" runat="server" Visible="True" Width="75"></asp:TextBox>
                                                                                    </EditItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn Visible="True" HeaderText="FACTOR">
                                                                                    <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Wrap="False"></HeaderStyle>
                                                                                    <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="moFactorLabel" Text='<%# GetAmountFormattedDoubleString(Container.DataItem("FACTOR")) %>' runat="server">
                                                                                        </asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="moFactorText" runat="server" Visible="True" Width="75"></asp:TextBox>
                                                                                    </EditItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                                <asp:TemplateColumn Visible="False">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="moDealer_ID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("DEALER_ID"))%>'
                                                                                            runat="server">
                                                                                        </asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateColumn>
                                                                            </Columns>
                                                                            <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                                                Mode="NumericPages"></PagerStyle>
                                                                        </asp:DataGrid>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Button ID="BtnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                        runat="server"
                                                                        Width="100px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp;
                                                                    <asp:Button ID="BtnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                        runat="server"
                                                                        Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;
                                                                    <asp:Button ID="BtnCancel" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                                        runat="server"
                                                                        Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Cancel"></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>

                                                </div>

                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr height="30px">
                            <td align="right" colspan="2">
                                <hr style="height: 1px">
                                <table id="Table2" style="width: 678px;" cellspacing="1" cellpadding="1" align="left"
                                    border="0">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                runat="server" Width="90px" CssClass="FLATBUTTON"
                                                Height="20px" Text="BACK"></asp:Button></td>
                                        <td>
                                            <asp:Button ID="btnApply_WRITE" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                runat="server" Width="90px" CssClass="FLATBUTTON"
                                                Height="20px" Text="SAVE"></asp:Button></td>
                                        <td>
                                            <asp:Button ID="btnButtomNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                runat="server" Width="100px" CssClass="FLATBUTTON"
                                                Height="20px" Text="New"></asp:Button></td>
                                        <td>
                                            <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                runat="server" Width="140px" CssClass="FLATBUTTON"
                                                Height="20px" Text="New_With_Copy"></asp:Button></td>
                                        <td width="140px">&nbsp;</td>
                                        <td width="140px">&nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <script>

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
                    newHeight = browseHeight - 350;
                }
                else {
                    newHeight = browseHeight - 570;
                }

                if (newHeight < 75) {
                    newHeight = 75;
                }

                item.style.height = String(newHeight) + "px";

                item.style.width = String(browseWidth - 80) + "px";
            }

            resizeForm(document.getElementById("scroller"));
            //document.getElementById("scroller").scrollBy(0,-50); // horizontal and vertical scroll increments

        </script>

        <input type="hidden" id="CMD" value="" runat="server" />
        <input type="hidden" id="CopyDealerId" value="" runat="server" />
    </form>
</body>
</html>
