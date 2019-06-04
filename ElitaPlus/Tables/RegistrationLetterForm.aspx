<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master" ValidateRequest="false"
    CodeBehind="RegistrationLetterForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.RegistrationLetterForm" %>

<%@ Register Src="../Common/MultipleColumnDDLabelControl.ascx" TagName="MultipleColumnDDLabelControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
<asp:Panel ID="EditPanel_WRITE" runat="server">
<script type="text/javascript" src="../Common/Editor/fckeditor/fckeditor.js"></script>
<script type="text/javascript">
function CopyToClipboard(content) 
{ 
    if (content == null)
    {
        alert("ERROR - control not found - " + content); 
    }
    else
    { 
        //copy to clipboard 
        window.clipboardData.setData('Text', content);
    }
    return false;
} 
</script>
<table cellpadding="0" cellspacing="0" border="0">
         <tr>
            <td class="BLANKROW">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="left" valign="middle">
                <uc1:MultipleColumnDDLabelControl ID="multipleDealerDropControl" runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="2"><hr style="height: 1px" /></td>
        </tr>      
        <tr>
            <td class="LABELCOLUMN">
                 *<asp:Label ID="moLetterTypeLabel" runat="server">Letter_Type</asp:Label>:
            </td>
            <td>
                 <asp:TextBox ID="moLetterTypeText" runat="server"></asp:TextBox> 
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
               *<asp:Label ID="moNumberOfDaysLabel" runat="server">Number_Of_Days</asp:Label>:
            </td>
            <td>
                <asp:TextBox ID="moNumberOfDaysText" runat="server"></asp:TextBox>
            </td>
        </tr>
         <tr>
            <td class="LABELCOLUMN">
                *<asp:Label ID="lblEmailFrom" runat="server">SENDER</asp:Label>:
            </td>
            <td>
                <asp:TextBox ID="txtEmailFrom" runat="server" Columns="100" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                *<asp:Label ID="lblEmailTo" runat="server">RECIPIENT</asp:Label>:
            </td>
            <td>
                <asp:TextBox ID="txtEmailTo" runat="server" Columns="100" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
               *<asp:Label ID="moEmailSubjectLabel" runat="server">Email_Subject</asp:Label>:
            </td>
            <td>
                <asp:TextBox ID="moEmailSubjectText" runat="server" Columns="100" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
               <asp:Label ID="lblAttachment" runat="server">ATTACHMENT</asp:Label>:
            </td>
            <td noWrap>
                <asp:FileUpload ID="fileAttachment" runat="server" Width="350" />
                <asp:TEXTBOX ID="txtAttachment" runat="server" Columns="75" MaxLength="100" ReadOnly="true"></asp:TEXTBOX>
                <asp:Button ID="btnDeleteAttach" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_UNDO" Text="Delete"></asp:Button>
            </td>
        </tr>
        <tr>
            <td colspan="2"><hr style="height: 1px" /></td>
        </tr>
        <tr>
            <td class="LABELCOLUMN" colspan="2" style="text-align:left">
                  *<asp:Label ID="moEmailTextLabel" runat="server">Email</asp:Label>:
                <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server"/>
            </td>
         </tr>
         <tr>
            <td colspan="2" style="width:90%; text-align:center;" >
                <%=RenderTextEditor()%>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="width:90%; text-align:center;" >
                <asp:GridView id="gridVariable" runat="server" Width="100%" AutoGenerateColumns="False" 
                    CellPadding="1" CssClass="DATAGRID">
                    <RowStyle CssClass="ROW" />
                    <AlternatingRowStyle CssClass="ALTROW" />
                    <HeaderStyle CssClass="HEADER"/>
                    <Columns>
                        <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" />
                        <asp:BoundField DataField="CODE" HeaderText="CODE" />
                        <asp:TemplateField>
                            <ItemStyle Width="90px"></ItemStyle>
                            <ItemTemplate>
                                <asp:Button ID="btnCopyToClipboard" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat" 
                                    runat="server" Width="90px" Text='<%# TranlateLabel("COPY")%>' CssClass="FLATBUTTON" Height="20px" CausesValidation="False" 
                                    OnClientClick='<%# GetOnClickScript(Container.DataItem("CODE").ToString) %>'>
                                </asp:Button>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
  </asp:Panel>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <table id="Table2" cellspacing="1" cellpadding="1" width="300" align="left" border="0">
        <tr>
            <td>
                <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                    cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Text="BACK"
                    CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
            </td>
            <td>
                <asp:Button ID="btnApply_WRITE" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif);
                    cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Text="SAVE"
                    CssClass="FLATBUTTON" Height="20px"></asp:Button>
            </td>
            <td>
                <asp:Button ID="btnUndo_WRITE" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                    cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Text="UNDO"
                    CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
            </td>
            <td>
                <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                    cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Text="New"
                    CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
            </td>
            <td>
                <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                    cursor: hand; background-repeat: no-repeat" runat="server" Width="135px" Text="New_With_Copy"
                    CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
            </td>
            <td>
                <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                    cursor: hand; background-repeat: no-repeat" runat="server" Width="140px" Text="Delete"
                    CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
