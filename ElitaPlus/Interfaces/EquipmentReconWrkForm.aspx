<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EquipmentReconWrkForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.EquipmentReconWrkForm" %>
   
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>EquipmentReconWrkForm</title>
    
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

</head>
<body onresize="resizeForm(document.getElementById('scroller'));" leftmargin="0"
    topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <!--Start Header-->
    <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
        border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
        cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td valign="top">
                <table width="100%" border="0">
                    <tr>
                        <td height="20">
                            &nbsp;<asp:Label ID="Label7" runat="server" CssClass="TITLELABEL">INTERFACES</asp:Label>:
                            <asp:Label ID="Label3" runat="server"  CssClass="TITLELABELTEXT">EQUIPMENT_FILE</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
        margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
        cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
        <!--d5d6e4-->
        <tr>
            <td height="5">
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" height="100%">
                <asp:Panel ID="WorkingPanel" runat="server" Width="98%" Height="100px">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="0"
                        cellpadding="6" rules="cols" width="100%" align="center" bgcolor="#fef9ea" border="0">
                        <tr>
                            <td height="1">
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px" align="center" width="75%" colspan="2">
                                <uc1:ErrorController ID="ErrController" runat="server"></uc1:ErrorController>
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px" align="center" width="75%" colspan="2">
                                 <asp:Label ID="moCompanyGroupLabel" runat="server" Visible="True">COMPANY_GROUP_NAME</asp:Label>:
                                <asp:TextBox ID="moCompanyGroupText" runat="server" Width="200px" Visible="True" ReadOnly="True"
                                    Enabled="False"></asp:TextBox>
                                <asp:Label ID="moFileNameLabel" runat="server" Visible="True">FILENAME</asp:Label>:
                                <asp:TextBox ID="moFileNameText" runat="server" Width="200px" Visible="True" ReadOnly="True"
                                    Enabled="False"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid;
                                    border-bottom: black 1px solid" cellspacing="1" cellpadding="1" width="100%"
                                    bgcolor="#d5d6e4" border="0">
                                    <tr>
                                        <td style="height: 19px" height="19">
                                        </td>
                                    </tr>
                                    <tr id="trPageSize" runat="server">
                                        <td style="width: 627px" valign="top" align="left">
                                            <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>:&nbsp;
                                            <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
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
                                            </asp:DropDownList>
                                            <input id="HiddenSavePagePromptResponse" style="width: 8px; height: 18px" type="hidden"
                                                size="1" runat="server" /><input id="HiddenIsPageDirty" style="width: 8px; height: 18px"
                                                    type="hidden" size="1" runat="server" />
                                            <input id="HiddenIfComingFromBundlesScreen" style="width: 8px; height: 18px" type="hidden"
                                                size="1" runat="server" /><input id="HiddenIsBundlesPageDirty" style="width: 8px;
                                                    height: 18px" type="hidden" size="1" runat="server" />
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                        </td>
                                    </tr>          
                                                      
                                    <tr>
                                                                            <td colspan="3">
                                            <div id="scroller" style="overflow: auto; width: 710px" align="center">
                                                <table id="Table1" cellspacing="0" cellpadding="0" border="0">
                                                    <tr>
                                                        <td nowrap>
                                                            <asp:UpdatePanel ID="updatePanel" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="moDataGrid" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                                                                        BorderWidth="1px" AllowSorting="True" CellPadding="1" BackColor="#DEE3E7" OnRowCreated="ItemCreated"
                                                                        BorderColor="#999999" BorderStyle="Solid">
                                                                        <SelectedRowStyle Wrap="False" BackColor="Transparent"></SelectedRowStyle>
                                                                        <EditRowStyle Wrap="False" BackColor="AliceBlue"></EditRowStyle>
                                                                        <AlternatingRowStyle Wrap="False" BackColor="#F1F1F1"></AlternatingRowStyle>
                                                                        <RowStyle Wrap="False" BackColor="White"></RowStyle>
                                                                        <HeaderStyle  Wrap="False" ForeColor="#003399"></HeaderStyle>
                                                                        <Columns>
                                                                            <asp:TemplateField Visible="False">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Center" Width="30px" BackColor="White">
                                                                                </ItemStyle>
                                                                                <ItemTemplate><asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"
                                                                                        CommandName="EditRecord"></asp:ImageButton></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="False">
                                                                                <ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="moEquipmentReconWrkIdLabel" runat="server" Text='<%# Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage.GetGuidStringFromByteArray(Container.DataItem("equipment_recon_wrk_id"))%>'>
                                                                                    </asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField SortExpression="RECORD_TYPE" HeaderText="RECORD_TYPE">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <%--<asp:TextBox ID="moRecordTypeTextGrid" runat="server" Width="40px" onFocus="setHighlighter(this)"
                                                                                        onmouseover="setHighlighter(this)" Visible="True" ReadOnly="true"></asp:TextBox>--%>
                                                                                    <asp:DropDownList ID="moRecordTypeDrop" runat="server" Width="40px" Visible="True">
                                                                                    </asp:DropDownList>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>                                                                           
                                                                            <asp:TemplateField SortExpression="REJECT_CODE" HeaderText="REJECT_CODE">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="40px" BackColor="White" ></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="moRejectCode" runat="server" Width="40px" onFocus="setHighlighter(this)"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField SortExpression="REJECT_REASON" HeaderText="REJECT_REASON">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="214px" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="RejectReasonTextGrid" runat="server" Width="214px" onFocus="setHighlighter(this)"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                           
                                                                            <asp:TemplateField SortExpression="MANUFACTURER" HeaderText="MANUFACTURER">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="moManufacturerTextGrid" runat="server" onFocus="setHighlighter(this)"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField SortExpression="MODEL" HeaderText="MODEL">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="moModelTextGrid" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
                                                                                        Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                             <asp:TemplateField SortExpression="DESCRIPTION" HeaderText="DESCRIPTION">
                                                                                <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="moDescriptionTextGrid" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
                                                                                        Visible="True"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                           <asp:TemplateField HeaderText="IS_MASTER" SortExpression="IS_MASTER">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle BackColor="White" HorizontalAlign="Left" Width="30%" 
                                                                                            Wrap="False" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="moIsMasterTextGrid" runat="server" 
                                                                                                onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)" 
                                                                                                Visible="True"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="MASTER_EQIPMENT_DESCRIPTION" SortExpression="MASTER_EQIPMENT_DESCRIPTION">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle BackColor="White" HorizontalAlign="Left" Width="30%" 
                                                                                            Wrap="False" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="moMasterEquipmentDescriptionTextGrid" runat="server" 
                                                                                                onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)" 
                                                                                                Visible="True"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="REPAIRABLE" SortExpression="REPAIRABLE">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle BackColor="White" HorizontalAlign="Left" Width="30%" 
                                                                                            Wrap="False" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="moRepairableTextGrid" runat="server" 
                                                                                                onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)" 
                                                                                                Visible="True"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="EQUIPMENT_CLASS" SortExpression="EQUIPMENT_CLASS">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle BackColor="White" HorizontalAlign="Left" Width="30%" Wrap="False" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="moEquipmentClassTextGrid" runat="server" 
                                                                                                onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)" 
                                                                                                Visible="True" ></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="EQUIPMENT_TYPE" SortExpression="EQUIPMENT_TYPE">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle BackColor="White" HorizontalAlign="Left" Width="30%" Wrap="False" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="moEquipmentTypeTextGrid" runat="server" 
                                                                                                onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)" 
                                                                                                Visible="True"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="MANUFACTURER_WARRANTY" SortExpression="MANUFACTURER_WARRANTY">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle BackColor="White" HorizontalAlign="Left" Width="30%" Wrap="False" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="moManufacturerWarentyTextGrid" runat="server" Width="214px" 
                                                                                                onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)" 
                                                                                                Visible="True" ></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="ATTRIBUTES" SortExpression="ATTRIBUTES">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle BackColor="White" HorizontalAlign="Left" Width="30%" Wrap="False" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="moAttributesTextGrid" runat="server" 
                                                                                                onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)" 
                                                                                                Visible="True" ></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="NOTE1" SortExpression="NOTE1">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle BackColor="White" HorizontalAlign="Left" Width="30%" Wrap="False" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="moNote1TextGrid" runat="server" onFocus="setHighlighter(this)" 
                                                                                                onMouseover="setHighlighter(this)" Visible="True" ></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="NOTE2" SortExpression="NOTE2">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle BackColor="White" HorizontalAlign="Left" Width="214px" Wrap="False" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="moNote2TextGrid" runat="server" onFocus="setHighlighter(this)" 
                                                                                                onMouseover="setHighlighter(this)" Visible="True" Width="214px"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="NOTE3" SortExpression="NOTE3">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle BackColor="White" HorizontalAlign="Left" Width="214px" Wrap="False" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="moNote3TextGrid" runat="server" onFocus="setHighlighter(this)" 
                                                                                                onMouseover="setHighlighter(this)" Visible="True" Width="214px"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="NOTE4" SortExpression="NOTE4">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle BackColor="White" HorizontalAlign="Left" Width="214px" Wrap="False" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="moNote4TextGrid" runat="server" onFocus="setHighlighter(this)" 
                                                                                                onMouseover="setHighlighter(this)" Visible="True" Width="214px"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="NOTE5" SortExpression="NOTE5">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle BackColor="White" HorizontalAlign="Left" Width="214px" Wrap="False" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="moNote5TextGrid" runat="server" onFocus="setHighlighter(this)" 
                                                                                                onMouseover="setHighlighter(this)" Visible="True" Width="214px"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="NOTE6" SortExpression="NOTE6">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle BackColor="White" HorizontalAlign="Left" Width="214px" Wrap="False" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="moNote6TextGrid" runat="server" onFocus="setHighlighter(this)" 
                                                                                                onMouseover="setHighlighter(this)" Visible="True" Width="214px"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="NOTE7" SortExpression="NOTE7">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle BackColor="White" HorizontalAlign="Left" Width="214px" Wrap="False" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="moNote7TextGrid" runat="server" onFocus="setHighlighter(this)" 
                                                                                                onMouseover="setHighlighter(this)" Visible="True" Width="214px"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="NOTE8" SortExpression="NOTE8">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle BackColor="White" HorizontalAlign="Left" Width="214px" Wrap="False" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="moNote8TextGrid" runat="server" onFocus="setHighlighter(this)" 
                                                                                                onMouseover="setHighlighter(this)" Visible="True" Width="214px"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="NOTE9" SortExpression="NOTE9">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle BackColor="White" HorizontalAlign="Left" Width="214px" Wrap="False" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="moNote9TextGrid" runat="server" onFocus="setHighlighter(this)" 
                                                                                                onMouseover="setHighlighter(this)" Visible="True" Width="214px"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="NOTE10" SortExpression="NOTE10">
                                                                                        <HeaderStyle HorizontalAlign="Center" />
                                                                                        <ItemStyle BackColor="White" HorizontalAlign="Left" Width="214px" Wrap="False" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="moNote10TextGrid" runat="server" 
                                                                                                onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)" 
                                                                                                Visible="True" Width="214px"></asp:TextBox>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                            <asp:TemplateField Visible="False">
                                                                                <ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="moModifiedDateLabel" runat="server" Text='<%# Container.DataItem("modified_date")%>'>
                                                                                    </asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>                                                                                                                                      </Columns>
                                                                        <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                                                        <PagerStyle HorizontalAlign="Left" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                                            CssClass="PAGER_LEFT"></PagerStyle>
                                                                    </asp:GridView>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3" height="28">
                                            <asp:Button ID="SaveButton_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                                cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                Width="100px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;
                                            <asp:Button ID="btnUndo_WRITE" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                                cursor: hand; background-repeat: no-repeat" TabIndex="195" runat="server" Font-Bold="false"
                                                Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Undo" CausesValidation="False">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom" align="left" height="50">
                                <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Back"></asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
   
    <script>

        function Test(obj) {

        }

        function setDirty() {
            document.getElementById("HiddenIsPageDirty").value = "YES"
        }

        function UpdateDropDownCtr(obj, oField) {
            document.getElementById(oField).value = obj.value
        }

        function UpdateCtr(oDropDown, oField) {
            document.getElementById(oField).value = oDropDown.value
            setDirty()
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

        resizeForm(document.getElementById("scroller"));
    </script>
    </form>
</body>
</html>
