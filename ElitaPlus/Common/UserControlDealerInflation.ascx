<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlDealerInflation.ascx.vb" 
Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlDealerInflation" %>
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
    <asp:GridView ID="DealerInflationGrid" runat="server" Width="51%" DataKeyNames="DEALER_INFLATION_ID,DEALER_ID,INFLATION_MONTH,INFLATION_YEAR,INFLATION_PCT"
        AllowPaging="True" AllowSorting="False" AutoGenerateColumns="False" SkinID="DetailPageGridView" CssClass = "grid-view"
        PageSize="2">
        <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
        <EditRowStyle CssClass="EDITROW"></EditRowStyle>
        <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
        <RowStyle CssClass="ROW"></RowStyle>
        <HeaderStyle CssClass="HEADER"></HeaderStyle>
        <Columns>
            <asp:BoundField Visible="false" DataField="DEALER_INFLATION_ID" />
            <asp:BoundField Visible="false" DataField="DEALER_ID" />
            <asp:TemplateField Visible="True" HeaderText="INFLATION_MONTH">
                <ItemTemplate>
                    <asp:Label ID="lblInflationMonth" runat="server" Text='<%#Container.DataItem("INFLATION_MONTH")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="cboInflationMonth" runat="server" Visible="True" SkinID="SmallDropDown">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="INFLATION_YEAR">
                <ItemTemplate>
                    <asp:Label ID="lblInflationYear" runat="server" Text='<%#Container.DataItem("INFLATION_YEAR")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="cboInflationYear" runat="server" Visible="True" SkinID="SmallDropDown">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="INFLATION_PCT">
                <ItemTemplate>
                    <asp:Label ID="lblInflationPct" runat="server" Text='<%#Container.DataItem("INFLATION_PCT")%>'></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtInflationPct" runat="server" Visible="true" SkinID="exSmallTextBox"
                        onkeypress="return numericOnly(this)" onblur="return chkForZero(this)"></asp:TextBox><br />
                    <asp:Label ID="lblValidPctMsg" runat="server"></asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="false">
                <ItemTemplate>
                    <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="SelectAction"
                        ImageUrl="~/App_Themes/Default/Images/edit.png" CommandArgument="<%#Container.DisplayIndex %>" />&nbsp;&nbsp;&nbsp;&nbsp;
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
    function chkForZero(obj) {
        var obj1 = obj.id.replace('txtInflationPct', 'lblValidPctMsg');

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