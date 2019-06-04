<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CommPCodeForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.CommPCodeForm" MasterPageFile="~/Navigation/masters/content_default.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Common/ErrorController.ascx" TagName="ErrorController" TagPrefix="uc1" %>
<%@ Register src="../Common/MultipleColumnDDLabelControl.ascx" tagname="MultipleColumnDDLabelControl" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                
                    <asp:Panel ID="EditPanel_WRITE" runat="server" Width="100%">
                        <table style="vertical-align: middle" cellpadding="0" cellspacing="0" border="0"
                            width="100%">
                            <tr style="vertical-align: middle" valign="middle">
                                <td style="vertical-align: middle" valign="middle">
                                    <table id="Table1" style="width: 100%;" cellspacing="1" cellpadding="0" width="100%"
                                        border="0">
                                        <asp:Panel ID="moPCodePanel_WRITE" runat="server" >
                                        
                                        <tr>
                                            <td valign="top" align="left" colspan="6">
                                                <table cellspacing="0" cellpadding="0" border="0" width="85%">
                                                    <tr>
                                                        <td align="left" valign="middle">
                                                            <uc2:MultipleColumnDDLabelControl ID="moDealerMultipleDrop" runat="server"  />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td colspan="6">
                                            <hr style="height: 1px" />
                                            <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                                                runat="server">
                                        </td>
                                    </tr>
                                        <tr>
                                            <td class="LABELCOLUMN" align="right">
                                                *
                                                <asp:Label ID="moProductCodeLabel" runat="server" >PRODUCT_CODE</asp:Label>:&nbsp;
                                            </td>
                                            <td colspan="3" align="left">
                                                <asp:DropDownList ID="moProductCodeDrop" runat="server" Width="100px" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                        <td colspan="6" height="10px">
                                            
                                        </td>
                                        
                                    </tr>
                                    </tr>
                                    <tr>
                                            <td class="LABELCOLUMN" align="right">
                                                *
                                                <asp:Label ID="moEffectiveLabel" runat="server">Effective</asp:Label>:&nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="moEffectiveText_WRITE" runat="server" CssClass="FLATTEXTBOX" Width="80px" ></asp:TextBox>
                                                <asp:ImageButton ID="BtnEffectiveDate_WRITE" runat="server" ImageAlign="AbsMiddle"
                                                ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                            <td class="LABELCOLUMN" align="right">
                                                *
                                                <asp:Label ID="moExpirationLabel" runat="server">Expiration</asp:Label>:&nbsp;
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="moExpirationText_WRITE" runat="server" CssClass="FLATTEXTBOX" Width="80px" ></asp:TextBox>
                                                <asp:ImageButton ID="BtnExpirationDate_WRITE" runat="server" ImageAlign="AbsMiddle"
                                                ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>&nbsp;&nbsp;&nbsp;&nbsp;
                                            </td>
                                        </tr>
                                        </asp:Panel>
                                        <tr>
                                        <td height="10px"/>
                                        <td />
                                        <td />
                                        <td />
                                        <td />
                                        <td />   
                                        
                                        
                                    </tr>
                                        
                                        <tr>
                                            <td colspan="6">
                                                <cc1:TabContainer ID="moEntityTabContainer" runat="server" CssClass="TABS">
                                                    <cc1:TabPanel ID="moEntityTabPanel" runat="server" HeaderText="Entities" BorderColor="Black" BorderStyle="Solid">
                                                        
                                                        <ContentTemplate>
                                                            <table border="0" cellpadding="0" cellspacing="1" width="100%">
                                                                <tr>
                                                            <td colspan="2">
                                                                <uc1:ErrorController ID="moErrorControllerEntity" runat="server" />
                                                            </td>
                                                        </tr>
                                                                <tr>
                                                                    <td >
                                                                    <div id="scroller" style="overflow: auto; width: 98%" align="center">
                                                                    
                                                            <asp:Panel ID="moEntityGridPanel" runat="server" Width="100%">
                                                                    <table id="Table2" style="width: 98%; height: 100%" cellspacing="0" cellpadding="0"
                                                                        border="0">
                                                                        <tr>
                                                                            <td valign="top" align="left">
                                                                                <asp:GridView ID="moEntityGrid" runat="server" Width="100%" AllowSorting="True" ShowFooter="True"
                                                                                    OnRowCreated="RowCreated" CellPadding="1" AutoGenerateColumns="False" 
                                                                                    CssClass="DATAGRID">
                                                                                    <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                                                                    <AlternatingRowStyle CssClass="ALTROW" Wrap="False" />
                                                                                    <FooterStyle CssClass="SELECTED"  />
                                                                                    <Columns>
                                                                                        
                                                                                        <asp:TemplateField>
                                                                                            <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="30px" 
                                                                                                Wrap="False" />
                                                                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                                                                            <ItemTemplate>
                                                                                                <asp:ImageButton ID="DeleteButton_WRITE" runat="server" 
                                                                                                    CommandArgument="<%#Container.DisplayIndex %>" CommandName="DeleteRecord" 
                                                                                                    ImageUrl="~/Navigation/images/icons/trash.gif" />
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField Visible="False">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="moCommPCodeEntityId" runat="server" 
                                                                                                    Text='<%# GetGuidStringFromByteArray(Container.DataItem("comm_p_code_entity_id"))%>'>
                                                                                                </asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                     
                                                                                        <asp:TemplateField HeaderText="PAYEE_TYPE">
                                                                                            <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Wrap="False" />
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:DropDownList ID="moPayeeTypeDrop" runat="server" AutoPostBack="True" 
                                                                                                                    OnSelectedIndexChanged="moPayeeTypeDrop_SelectedIndexChanged">
                                                                                                </asp:DropDownList>
                                                                                            </ItemTemplate>
                                                                                            
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="ENTITY">
                                                                                            <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Wrap="False" />
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:DropDownList ID="moEntityDrop" runat="server">
                                                                                                </asp:DropDownList>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        
                                                                                        <asp:TemplateField HeaderText="COMMISSION_FIXED">
                                                                                            <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Wrap="False" />
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:DropDownList ID="moIsCommFixedDrop" runat="server">
                                                                                                </asp:DropDownList>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        
                                                                                        <asp:TemplateField HeaderText="COMMISSION_SCHEDULE">
                                                                                            <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Wrap="False" />
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:DropDownList ID="moCommScheduleDrop" runat="server">
                                                                                                </asp:DropDownList>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        
                                                                                        <asp:TemplateField HeaderText="COMMISSION">
                                                                                            <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Wrap="False" />
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="moCommissionAmount" runat="server" Visible="True" 
                                                                                                    Width="75px"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                 <asp:TextBox  ID="moTotalCommText" runat="server" Visible="True" 
                                                                                                     Width="75px"></asp:TextBox>
                                                                                            </FooterTemplate>
                                                                                        
                                                                                        </asp:TemplateField>                                                                                        
                                                                                        <asp:TemplateField HeaderText="MARKUP_COMMISSION">
                                                                                            <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Wrap="False" />
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="moMarkupAmountText" runat="server" Visible="True" 
                                                                                                    Width="75px"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                            <FooterTemplate>
                                                                                                <asp:TextBox  ID="moTotalMarkupText" runat="server" Visible="True" 
                                                                                                    Width="75px" ></asp:TextBox>
                                                                                            </FooterTemplate>
                                                                                        </asp:TemplateField>
                                                                                     <%--    REQ-976 Added new column--%>
                                                                                         <asp:TemplateField HeaderText="NUMBER_OF_DAYS_TO_CLAWBACK">
                                                                                            <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Wrap="False" />
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                            <ItemTemplate>
                                                                                                <asp:TextBox ID="modaystoclawbackText" runat="server" Visible="True" 
                                                                                                    Width="75px"></asp:TextBox>
                                                                                            </ItemTemplate>
                                                                                                 </asp:TemplateField>
                                                                                       
                                                                                        
                                                                                        <%-- End  REQ-976--%>
                                                                                    </Columns>
                                                                                  <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                                                                  <HeaderStyle CssClass="HEADER" />
                                                                                  <RowStyle CssClass="ROW"></RowStyle>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                        <tr >
                                                                            <td align="left" >
                                                                                <asp:Label ID="moEntityCTotalPLabel" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp
                                                                                <asp:TextBox ID="moEntityCTotalPText"  runat="server" Visible="False"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                <asp:Label ID="moEntityMTotalPLabel" runat="server" Visible="False"></asp:Label>&nbsp;&nbsp
                                                                                <asp:TextBox ID="moEntityMTotalPText" runat="server" Visible="False"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>
                                                             </div>
                                                                </td>
                                                                </tr>
                                                                <tr style="height: 10px">
                                                            <td colspan="2">
                                                                </td>
                                                                </tr>
                                                                <tr>
                                                            <td colspan="2">
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                <asp:Button ID="BtnNewEntity_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="False"
                                                                    Width="100px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp;
                                                 
                                                            </td>
                                                        </tr>
                                                                </table>
                                                           
                                                        </ContentTemplate>
                                                    </cc1:TabPanel>
                                                   
                                                </cc1:TabContainer>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    
                
            </td>
        </tr>
    </table>
    
    <script type ='text/jscript' language='JavaScript'>
var decSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
var groupSep = '<%=System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';
var isCommFixedArr = new Array();
var isMarkupFixedArr = new Array();
var commAmountArr = new Array();
var markupAmountArr = new Array();

function setCommTotal(oText, index) {
 //   debugger;    
    commAmountArr[index] = oText.value;
    setTotals();
}
function setMarkupTotal(oText, index) {
  //  alert('setMarkupTotal');
    markupAmountArr[index] = oText.value;
    setTotals();
}
function setCommDrop(oDrop, index) {
  //  debugger;
 //   alert('setCommDrop');
    isCommFixedArr[index] = oDrop.value;
    setTotals();
 }
function setMarkupDrop(oDrop, index) {
 //   alert('setMarkupDrop');
    isMarkupFixedArr[index] = oDrop.value;
    setTotals();
}
function setTotals() {
    var i;
    var commT = 0;
    var markupT = 0;
    var commTotalText, markupTotalText;
    
   for (i = 0; i < isCommFixedArr.length; i++) {
        if (isCommFixedArr[i] == 'N') {
            commT = convertNumberToCulture(commT, decSep, groupSep);
            commT = parseFloat(setJsFormat(commT, '.')) + parseFloat(setJsFormat(commAmountArr[i], '.'));

            markupT = convertNumberToCulture(markupT, decSep, groupSep);
            markupT = parseFloat(setJsFormat(markupT, '.')) + parseFloat(setJsFormat(markupAmountArr[i], '.'));
        }
        if (isMarkupFixedArr[i] == 'N') {
            markupT = convertNumberToCulture(markupT, decSep, groupSep);
            markupT = parseFloat(setJsFormat(markupT, '.')) + parseFloat(setJsFormat(markupAmountArr[i], '.'));
        }
    }
    // debugger;
    if (typeof GetCommFixedId == 'function') {
        commTotalText = document.getElementById(GetCommFixedId());
        markupTotalText = document.getElementById(GetMarkupFixedId());
        if (commTotalText != null) {
            commTotalText.value = convertNumberToCulture(commT, decSep, groupSep);
        }
        
        if (markupTotalText != null) {
            markupTotalText.value = convertNumberToCulture(markupT, decSep, groupSep);
        }
    }
    if (typeof GetMarkupFixedId == 'function') {
        markupTotalText = document.getElementById(GetMarkupFixedId());
        if (markupTotalText != null) {
            markupTotalText.value = convertNumberToCulture(markupT, decSep, groupSep);
        }
    }
    
//    var commTotalText = document.getElementById(GetCommFixedId());
//    commTotalText.value = commT;
//    var markupTotalText = document.getElementById(GetMarkupFixedId());
//    markupTotalText.value = markupT;
}

</script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="14" runat="server" Width="90px"
        CausesValidation="False" Height="20px" CssClass="FLATBUTTON" Text="BACK"></asp:Button>
    <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="15" runat="server" Width="80px"
        CausesValidation="False" Height="20px" CssClass="FLATBUTTON" Text="SAVE"></asp:Button>
    <asp:Button ID="btnUndo_WRITE" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="16" runat="server" Width="90px"
        CausesValidation="False" Height="20px" CssClass="FLATBUTTON" Text="UNDO"></asp:Button>
    <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="17" runat="server" Width="80px"
        CausesValidation="False" Height="20px" CssClass="FLATBUTTON" Text="New"></asp:Button>
    <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="18" runat="server" Width="135px"
        CausesValidation="False" Height="20px" CssClass="FLATBUTTON" Text="New_With_Copy">
    </asp:Button>
    <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="19" runat="server" Width="80px"
        CausesValidation="False" Height="20px" CssClass="FLATBUTTON" Text="Delete"></asp:Button>
                        </asp:Content>
