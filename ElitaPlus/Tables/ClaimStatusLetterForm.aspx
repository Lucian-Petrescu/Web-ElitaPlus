<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    ValidateRequest="false" CodeBehind="ClaimStatusLetterForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ClaimStatusLetterForm" %>

<%@ Register Src="../Common/MultipleColumnDDLabelControl.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">

    <script type="text/javascript" src="../Common/Editor/fckeditor/fckeditor.js"></script>

    <script language="javascript" type="text/javascript">
        function CopyToClipboard(content) {
            if (content == null) {
                alert("ERROR - control not found - " + content);
            }
            else {
                //copy to clipboard 
                window.clipboardData.setData('Text', content);

                //        var oTextarea = document.getElementById("EmailText");
                //        var PastedText;        
                //        PastedText = document.Form1.oTextarea.createTextRange();
                //        PastedText.execCommand("Paste");
                //        alert('Copied text to clipboard : ' + content); 
            }

            return false;
        }

//        function ValidateEmail(moRecipientTextId) {
//            var obj = document.getElementById(moRecipientTextId);
//            var val = obj.value;
//            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
//            if (reg.test(val) == false) {
//                alert('Invalid Email Address');
//            }
//        }

    </script>

    <style type="text/css">
        tr.HEADER th
        {
            text-align: center;
        }
    </style>
    <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
        margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
        cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <table id="moOutTable" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                    border-left: #999999 1px solid; border-bottom: #999999 1px solid" height="98%"
                    cellspacing="0" cellpadding="6" width="98%" align="center" bgcolor="#fef9ea"
                    border="0">
                    <tr>
                        <td valign="top" align="center" height="90%">
                            <div id="scroller" style="overflow: auto; width: 100%; height: 100%" align="center">
                                <asp:Panel ID="EditPanel_WRITE" runat="server" Height="50%">
                                    <table id="Table1" cellspacing="1" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td style="height: 17px; width: 70%" align="left" colspan="4">
                                                <uc1:MultipleColumnDDLabelControl ID="multipleDealerDropControl" runat="server">
                                                </uc1:MultipleColumnDDLabelControl>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 17px; width: 1%;" nowrap align="right">
                                                <asp:RadioButton ID="rbUseClaimStatus" runat="server" AutoPostBack="true" Checked="False"
                                                    GroupName="UseStatus" Text="EXTENDED_CLAIM_STATUS" TextAlign="left" />
                                            </td>
                                            <td style="height: 17px; width: 1%;" nowrap align="left">
                                                <asp:RadioButton ID="rbUseGroup" runat="server" AutoPostBack="true" Checked="False"
                                                    GroupName="UseStatus" Text="GROUP_OWNER" TextAlign="left" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" nowrap style="height: 17px; width: 1%;">
                                                *
                                                <asp:Label ID="moClaimStatusLabel" runat="server">EXTENDED_CLAIM_STATUS</asp:Label>
                                                <asp:Label ID="moGroupIdLabel" runat="server">GROUP</asp:Label>
                                            </td>
                                            <td style="height: 17px" width="40%">
                                                &nbsp;
                                                <asp:DropDownList ID="ExtendedClaimStatusDropdownList" runat="server" Width="405px">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="cboGroupId" runat="server" Width="405px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" nowrap style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                            <td style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                            <tr>
                                                <td id="testTD" align="right" nowrap style="height: 17px; width: 1%;">
                                                    <asp:Label ID="Mandatory1" runat="server">*</asp:Label>
                                                    <asp:Label ID="moNotificationTypeLabel" runat="server">NOTIFICATION_TYPE</asp:Label>
                                                </td>
                                                <td style="height: 17px" width="40%">
                                                    &nbsp;
                                                    <asp:DropDownList ID="cboNotificationTypeId" runat="server" AutoPostBack="false"
                                                        Width="405px">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        <tr>
                                            <td align="right" nowrap style="height: 17px; width: 1%;">
                                                <asp:Label ID="moActiveLabel" runat="server">Active</asp:Label>
                                            </td>
                                            <td style="height: 17px" width="40%">
                                                &nbsp;
                                                <asp:DropDownList ID="IsActiveDropdownlList" runat="server" Width="405px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" nowrap style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                            <td style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" nowrap style="height: 17px; width: 1%;">
                                                <asp:Label ID="moLetterTypeLabel" runat="server">Letter_Type</asp:Label>
                                            </td>
                                            <td style="height: 17px" width="40%">
                                                &nbsp;
                                                <asp:TextBox ID="moLetterTypeText" runat="server" TabIndex="1" Width="400px"></asp:TextBox>
                                            </td>
                                            <td align="right" nowrap style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                            <td style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" nowrap style="height: 17px; width: 1%;">
                                                <asp:Label ID="moNumberOfDaysLabel" runat="server">Number_Of_Days</asp:Label>
                                            </td>
                                            <td style="height: 17px" width="40%">
                                                &nbsp;
                                                <asp:TextBox ID="moNumberOfDaysText" runat="server" TabIndex="1" Width="400px"></asp:TextBox>
                                            </td>
                                            <td align="right" nowrap style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                            <td style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" nowrap style="height: 17px; width: 1%;">
                                                <asp:Label ID="moSenderLabel" runat="server">Sender</asp:Label>
                                            </td>
                                            <td style="height: 17px" width="40%">
                                                &nbsp;
                                                <asp:TextBox ID="moSenderText" runat="server" TabIndex="1" Width="400px"></asp:TextBox>
                                            </td>
                                            <td align="right" nowrap style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                            <td style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" nowrap style="height: 17px; width: 1%;">
                                                <asp:Label ID="moRecipientLabel" runat="server">Recipient</asp:Label>
                                            </td>
                                            <td style="height: 17px" width="40%">
                                                &nbsp;
                                                <asp:TextBox ID="moRecipientText" runat="server" TabIndex="1" Width="400px"></asp:TextBox>
                                            </td>
                                            <td align="right" nowrap style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                            <td style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" nowrap style="height: 17px; width: 1%;">
                                                <asp:Label ID="moServiceCenterRecipientLabel" runat="server">Service_Center_Recipient</asp:Label>
                                            </td>
                                            <td style="height: 17px" width="40%">
                                                &nbsp;
                                                <asp:DropDownList ID="ServiceCenterRecipientDropdown" runat="server" Width="405px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" nowrap style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                            <td style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" nowrap style="height: 17px; width: 1%;">
                                                <asp:Label ID="moEmailSubjectLabel" runat="server">Email_Subject</asp:Label>
                                            </td>
                                            <td style="height: 17px" width="40%">
                                                &nbsp;
                                                <asp:TextBox ID="moEmailSubjectText" runat="server" TabIndex="1" Width="400px"></asp:TextBox>
                                            </td>
                                            <td align="right" nowrap style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                            <td style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" nowrap style="height: 17px; width: 1%;">
                                                <asp:Label ID="moEmailTextLabel" runat="server">Email</asp:Label>
                                            </td>
                                            <td style="height: 17px" width="40%">
                                                &nbsp;
                                                <input id="HiddenSaveChangesPromptResponse" runat="server" name="HiddenSaveChangesPromptResponse"
                                                    type="hidden" />
                                            </td>
                                            <td align="right" nowrap style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                            <td style="height: 17px" width="1%">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4" style="width: 80%;">
                                                <%=RenderTextEditor()%>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="Panel1" runat="server" Height="50%">
                                    <table id="Table3" cellspacing="1" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td nowrap align="left" colspan="4">
                                                <asp:GridView ID="Grid" runat="server" Width="100%" CellPadding="1" AutoGenerateColumns="False"
                                                    CssClass="DATAGRID">
                                                    <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                                    <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                                                    <RowStyle CssClass="ROW"></RowStyle>
                                                    <HeaderStyle CssClass="HEADER"></HeaderStyle>
                                                    <Columns>
                                                        <asp:TemplateField ShowHeader="false" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblListItemId" runat="server" Visible="False" Text='<%# GetGuidStringFromByteArray(Container.DataItem("ID")) %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="DESCRIPTION" HeaderText="Description">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="60%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Code" HeaderText="Code">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField>
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="8%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Right" Width="90px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnCopyToClipboard" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                                    cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Text="COPY"
                                                                    CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
<input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnBack" runat="server" Width="80px" CssClass="FLATBUTTON" Text="BACK"
        Height="20px" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
        cursor: hand; background-repeat: no-repeat" Font-Bold="false"></asp:Button>
    <asp:Button ID="btnApply_WRITE" runat="server" Text="SAVE" Font-Bold="false" Width="80px"
        CssClass="FLATBUTTON" Height="20px" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
        cursor: hand; background-repeat: no-repeat"></asp:Button>
    <asp:Button ID="btnUndo_WRITE" Width="80px" CssClass="FLATBUTTON" Text="Undo" Height="20px"
        Style="background-image: url(../Navigation/images/icons/cancel_icon.gif); cursor: hand;
        background-repeat: no-repeat" TabIndex="195" runat="server" Font-Bold="false">
    </asp:Button>
    <asp:Button ID="btnNew_WRITE" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW"
        Text="NEW"></asp:Button>
    <asp:Button ID="btnCopy_WRITE" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW"
        Text="New_With_Copy" Width="136px"></asp:Button>
    <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="47" runat="server" Width="90px"
        CssClass="FLATBUTTON" Height="20px" Text="Delete"></asp:Button>
</asp:Content>
