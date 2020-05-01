<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlInvoiceRegionTaxes.ascx.vb" 
Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlInvoiceRegionTaxes" %>
<asp:Panel ID="moDealerInflationTabPanel" runat="server" Width="100%">
    <div class="dataContainer">
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor"
                            runat="server">:</asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="cboDiPageSize" runat="server" Width="50px" AutoPostBack="true"
                            SkinID="SmallDropDown">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="45">45</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="bor" align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
<asp:GridView ID="GridIIBBTaxes" runat="server" Width="51%" DataKeyNames="INVOICE_REGION_TAX_ID,INVOICE_TRANS_ID,REGION_DESCRIPTION,REGION_ID,TAX_AMOUNT"
        AllowPaging="True" AllowSorting="true" AutoGenerateColumns="False" SkinID="DetailPageGridView" OnRowEditing="OnRowEditing">
        <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
        <EditRowStyle CssClass="EDITROW"></EditRowStyle>
        <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
        <RowStyle CssClass="ROW"></RowStyle>
        <HeaderStyle CssClass="HEADER"></HeaderStyle>
        <Columns>
            <asp:BoundField Visible="false" DataField="INVOICE_TRANS_ID" />
            <asp:BoundField Visible="false" DataField="INVOICE_REGION_TAX_ID" />
            <asp:BoundField Visible="false" DataField="REGION_ID" />
           
            <asp:TemplateField Visible="true" HeaderText="REGION">
                <ItemTemplate>
                    <asp:Label ID="lblRegion" runat="server" Text='<%#Container.DataItem("REGION_ID")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="cboRegion" runat="server" Visible="True" SkinID="SmallDropDown">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="TAX TYPE">
                <ItemTemplate>
                    <asp:Label ID="lblinvoicetype" runat="server" Text='<%#Container.DataItem("TAX_TYPE_XCD")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="cboinvoicetype" runat="server" Visible="True" SkinID="SmallDropDown">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField Visible="True" HeaderText="AMOUNT">
                <ItemTemplate>
                    <asp:Label ID="lblIIBTax" runat="server" Text='<%#Container.DataItem("TAX_AMOUNT")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtIIBBTax" runat="server" Visible="true" SkinID="exSmallTextBox"
                        onkeypress="return numericOnly(this)"></asp:TextBox><br />
                    <asp:Label ID="lblValidRiskPctMsg" runat="server"></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="false">
                <ItemTemplate>
                    <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="SelectAction"
                        ImageUrl="~/App_Themes/Default/Images/edit.png" CommandArgument="<%#Container.DisplayIndex %>" OnClientClick="" />&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="DeleteButton_WRITE" ImageUrl="~/App_Themes/Default/Images/icon_delete.png"
                        runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>" />
                </ItemTemplate>
                <EditItemTemplate>
                    <table><tr><td style=" align-items: end ">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" SkinID="PrimaryRightButton"></asp:Button></td>
                    <td><asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" SkinID="AlternateRightButton"></asp:LinkButton>
                    </td></tr></table>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
    </asp:GridView>
    <input id="HiddenDIDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
    <br/>
    <asp:Button ID="NewButton_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="100px" Text="New"
        Height="20px" CssClass="FLATBUTTON"></asp:Button>
</asp:Panel>
<script type="text/javascript">
    function chkForZeroForRiskTolerance(obj) {
        var obj1 = obj.id.replace('txtTolerancePct', 'lblValidRiskPctMsg');

        if (obj.value < 0 || obj.value > 9999.99) {
            document.getElementById(obj1).innerText = "Please enter number between 0 and 9999.99";
            document.getElementById(obj1).style.color = "Red";
        }
        else {
            document.getElementById(obj1).innerText = "";
        }
        return true;
    }

    function numericOnly(elementRef) {
        var keyCodeEntered = (event.which) ? event.which : (window.event.keyCode) ? window.event.keyCode : -1;
        if ((keyCodeEntered >= 48) && (keyCodeEntered <= 57)) {
            return true;
        }
        else if (keyCodeEntered == 43) {
            if ((elementRef.value) && (elementRef.value.indexOf('+') >= 0))
                return false;
            else
                return true;
        }
        else if (keyCodeEntered == 45) {
            if ((elementRef.value) && (elementRef.value.indexOf('-') >= 0))
                return false;
            else
                return true;
        }
        else if (keyCodeEntered == 46) {
            if ((elementRef.value) && (elementRef.value.indexOf('.') >= 0))
                return false;
            else
                return true;
        }

        return false;
    }

</script>
