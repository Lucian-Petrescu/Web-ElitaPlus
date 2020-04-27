<%@ Page Language="vb" MasterPageFile="~/Navigation/masters/content_default.Master"
    AutoEventWireup="false" CodeBehind="InvoiceReconWrkForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.InvoiceReconWrkForm"
    Title="InvoiceReconWrkForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<asp:Content ContentPlaceHolderID="ContentPanelMainContentBody" runat="server" ID="cntMain">
    <table id="tblOuter2" style="margin: 5px;" height="93%" cellspacing="0" cellpadding="0"
        rules="none" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td valign="top" align="center" height="100%">
                <asp:Panel ID="WorkingPanel" runat="server" Width="100%" BorderStyle="None">
                    <table id="tblMain1" cellspacing="0" cellpadding="6" rules="cols" width="100%" align="center"
                        bgcolor="#fef9ea" border="0">
                        <tr>
                            <td style="height: 30px; text-align: center">
                                <asp:Label ID="moFileNameLabel" runat="server" Visible="True">FILENAME:</asp:Label>
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
                                        <td style="height: 19px">
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
                                            <input id="HiddenSavePagePromptResponse" type="hidden" name="HiddenSavePagePromptResponse"
                                                   runat="server"/>
                                            <input id="HiddenIsPageDirty" type="hidden" name="HiddenIsPageDirty" runat="server"/>
                                            <input id="HiddenIsPartsPageDirty" type="hidden" runat="server" />
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <div id="scroller" style="overflow: auto;" align="center">
                                                <table id="Table1" cellspacing="0" cellpadding="0" border="0" width="100%">
                                                    <tr>
                                                        <td style="white-space: nowrap">
                                                            <asp:UpdatePanel ID="updatePanel" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:GridView ID="Grid" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                                                                        BorderWidth="1px" AllowSorting="false" CellPadding="1" BackColor="#DEE3E7" BorderColor="#999999"
                                                                        BorderStyle="Solid">
                                                                        <SelectedRowStyle Wrap="False" BackColor="Transparent"></SelectedRowStyle>
                                                                        <EditRowStyle Wrap="False" BackColor="AliceBlue"></EditRowStyle>
                                                                        <AlternatingRowStyle Wrap="False" BackColor="#F1F1F1"></AlternatingRowStyle>
                                                                        <RowStyle Wrap="False" BackColor="White"></RowStyle>
                                                                        <HeaderStyle Wrap="true" ForeColor="#003399"></HeaderStyle>
                                                                        <Columns>
                                                                            <asp:TemplateField Visible="False">
                                                                                <ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblReconWrkID" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("INVOICE_RECON_WRK_ID"))%>'>
                                                                                    </asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="RECORD_TYPE">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRecordType" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
                                                                                        Visible="True" Columns="2" MaxLength="2" ReadOnly ="true"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="REJECT_CODE">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRejectCode" runat="server" onFocus="setHighlighter(this)" Columns="5"
                                                                                        onMouseover="setHighlighter(this)" Visible="True" ReadOnly="true"></asp:TextBox></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="REJECT_REASON">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRejectReason" runat="server" onFocus="setHighlighter(this)" Columns="60"
                                                                                        onMouseover="setHighlighter(this)" Visible="True" ReadOnly="true"></asp:TextBox></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="COMPANY_CODE">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtCompanyCode" runat="server" onFocus="setHighlighter(this)" Columns="5"
                                                                                        onMouseover="setHighlighter(this)" Visible="True" ></asp:TextBox></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="INVOICE_NUMBER">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtInvoiceNumber" runat="server" onFocus="setHighlighter(this)"
                                                                                        Columns="10" onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="INVOICE_DATE">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtInvoiceDate" runat="server" onFocus="setHighlighter(this)" Columns="20"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                    <asp:ImageButton ID="imgInvoiceDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                                        ImageAlign="AbsMiddle"></asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="REPAIR_DATE">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtRepairDate" runat="server" onFocus="setHighlighter(this)" Columns="20"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                    <asp:ImageButton ID="imgRepairDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                                        ImageAlign="AbsMiddle"></asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="DUE_DATE">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtDueDate" runat="server" onFocus="setHighlighter(this)" Columns="20"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                                                                                    <asp:ImageButton ID="imgDueDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                                        ImageAlign="AbsMiddle"></asp:ImageButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="ATTRIBUTES">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAttributes" runat="server" onFocus="setHighlighter(this)" Columns="50"
                                                                                        TextMode="MultiLine" onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="SERVICE_CENTER">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtServiceCenter" runat="server" onFocus="setHighlighter(this)" Columns="20"
                                                                                        TextMode="SingleLine" onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="CLAIM_NUMBER">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtClaimNumber" runat="server" onFocus="setHighlighter(this)" Columns="10"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="AUTHORIZATION_NUMBER">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAuthorizationNumber" runat="server" onFocus="setHighlighter(this)"
                                                                                        Columns="10" onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="LINE_ITEM_NUMBER">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtLineItemNumber" runat="server" onFocus="setHighlighter(this)"
                                                                                        Columns="10" onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="VENDOR_SKU">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtSKU" runat="server" onFocus="setHighlighter(this)" Columns="30"
                                                                                        TextMode="MultiLine" onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="VENDOR_SKU_DESCRIPTION">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtSKUDescription" runat="server" onFocus="setHighlighter(this)"
                                                                                        Columns="30" TextMode="MultiLine" onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="SERVICE_LEVEL">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtServiceLevel" runat="server" onFocus="setHighlighter(this)" Columns="30"
                                                                                        TextMode="MultiLine" onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="AMOUNT">
                                                                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                                                <ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAmount" runat="server" onFocus="setHighlighter(this)" Columns="15"
                                                                                        onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox></ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblModifiedDate" runat="server" Text='<%# Container.DataItem("modified_date")%>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                        </Columns>
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
        function setDirty() {
            document.getElementById("<%= HiddenIsPageDirty.ClientID%>").value = "YES"
        }

        function setPartsDirty() {
            document.getElementById("HiddenIsPartsPageDirty").value = "YES"
        }

        function resizeControl(item) {
            var browseWidth, browseHeight;

            if (document.all) {
                browseWidth = document.body.clientWidth;
                browseHeight = document.body.clientHeight;
                //alert( browseWidth + " : " + browseHeight);
            }
            newHeight = parent.document.getElementById("Navigation_Content").clientHeight - 325;
            //alert(screen.height + " : " + newHeight);
            //alert("tblMain: " + parent.document.getElementById("Navigation_Content").clientHeight); 
            if (newHeight < 0)
                newHeight = newHeight * -1;

            browseWidth = browseWidth - 100;
            if (browseWidth < 0)
                browseWidth = browseWidth * -1;

            item.style.height = newHeight + "px";
            item.style.width = browseWidth + "px";
        }

        resizeControl(document.getElementById("scroller"));
    </script>
</asp:Content>
